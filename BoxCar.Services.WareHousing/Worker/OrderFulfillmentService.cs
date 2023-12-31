﻿using BoxCar.Integration.MessageBus;
using BoxCar.Services.WareHousing.Entities;
using BoxCar.Services.WareHousing.Messages;
using BoxCar.Services.WareHousing.Repositories;
using BoxCar.Services.WareHousing.Services;
using Microsoft.Azure.ServiceBus;
using System.ComponentModel;
using System.Text;

namespace BoxCar.Services.WareHousing.Worker
{

    public class OrderFulfillmentService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ItemsRepository _itemsRepository;
        private ISubscriptionClient _subscriptionClient;
        private readonly IMessageBus _messageBus;
        private readonly string _orderStatusUpdateTopic;
        private readonly string _productionRequestTopic;
        private readonly string _orderItemsAvailabilityUpdateMessageTopic;


        private readonly IVehicleCatalogService _vehicleCatalogService;
        private readonly IEngineCatalogService _engineCatalogService;
        private readonly IChassisCatalogService _chassisCatalogService;
        private readonly IOptionPackCatalogService _optionPackCatalogService;

        public OrderFulfillmentService(IConfiguration configuration, ILoggerFactory loggerFactory,
            ItemsRepository itemsRepository,
            IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _configuration = configuration;
            _itemsRepository = itemsRepository;
            _logger = loggerFactory.CreateLogger<OrderFulfillmentService>();
            _orderStatusUpdateTopic = configuration.GetValue<string>("OrderStatusUpdate");
            _productionRequestTopic = configuration.GetValue<string>("ProductionRequest");
            _orderItemsAvailabilityUpdateMessageTopic = _configuration.GetValue<string>("OrderItemsAvailabilityUpdate");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriptionClient = new SubscriptionClient(_configuration.GetValue<string>("ServiceBusConnectionString"),
                _configuration.GetValue<string>("OrderFullfillmentRequest"), _configuration.GetValue<string>("subscriptionName"));

            var messageHandlerOptions = new MessageHandlerOptions(e =>
            {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(OrderFulfillmentService)} stopping.");
            await _subscriptionClient.CloseAsync();
        }

        protected void ProcessError(Exception e)
        {
            _logger.LogError(e, "Error while processing queue item in {0} ServiceBusListener.", nameof(OrderFulfillmentService));
        }

        protected async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            FulfillOrderRequest fulfillRequest = System.Text.Json.JsonSerializer.Deserialize<FulfillOrderRequest>(messageBody);
            if (fulfillRequest == null) return;

            var productionRequest = new ProductionRequest { OrderId = fulfillRequest.OrderId, CreationDateTime = DateTime.UtcNow };
            var orderItemsAvailabilityUpdate = new OrderItemsAvailabilityUpdate { OrderId = fulfillRequest.OrderId, CreationDateTime = DateTime.UtcNow };
            try
            {
                await ProcessOrder(fulfillRequest, productionRequest, orderItemsAvailabilityUpdate);

                await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                await SendProductionRequest(productionRequest);
                await SendOrderUpdate(orderItemsAvailabilityUpdate);

                _logger.LogDebug($"{fulfillRequest.OrderId}: {nameof(OrderFulfillmentService)} received item.");
                await Task.Delay(20000);
                _logger.LogDebug($"{fulfillRequest.OrderId}: {nameof(OrderFulfillmentService)} processed item.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "unable to process order fulfillment request for order:{0}", fulfillRequest.OrderId);
                throw;
            }
        }

        private async Task SendOrderUpdate(OrderItemsAvailabilityUpdate orderItemsAvailabilityUpdate)
        {
            try
            {
                await _messageBus.PublishMessage(orderItemsAvailabilityUpdate, _orderStatusUpdateTopic);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while posting a fulfillment update message {1}", orderItemsAvailabilityUpdate);
            }
        }

        private async Task SendProductionRequest(ProductionRequest productionRequest)
        {
            if (productionRequest.Items.Count == 0) return;
            try
            {
                await _messageBus.PublishMessage(productionRequest, _productionRequestTopic);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while posting a production request to the production service queue. Request Details {1}", productionRequest);
            }
        }

        private async Task ProcessOrder(FulfillOrderRequest fulfillRequest, ProductionRequest productionRequest, OrderItemsAvailabilityUpdate orderItemsAvailabilityUpdate)
        {
            _logger.LogInformation("Checking if items in fulfill order request {0} are avaialble",fulfillRequest);
            foreach (var line in fulfillRequest.OrderItems)
            {

                var specificationKey = SpecificationKeyGenerator.GenerateSpecificationKey(line.VehicleId, line.ChassisId, line.EngineId, line.OptionPackId);
                var vehicleMatchingSpecification = await _itemsRepository.GetBySpecificationKey(specificationKey);
                if (vehicleMatchingSpecification == null || vehicleMatchingSpecification.Quantity == 0)
                {
                    var getMatchingComponents = await _itemsRepository.GetComponents(line);
                    foreach (var component in getMatchingComponents)
                    {
                        if (component.Quantity == 0)
                        {
                            productionRequest.Items.Add(new ProductionRequestLineItem
                            {
                                ItemType = component.ItemType,
                                ItemTypeId = component.ItemTypeId,
                                OrderId = line.OrderId,
                                OrderItemId = line.OrderItemId
                            });
                        }
                    }
                    orderItemsAvailabilityUpdate.Lines.Add(new OrderItemAvailabilityLine { OrderItemId = line.OrderItemId, Status = OrderItemAvailabilityStatus.InProduction });
                    return;
                }
                var quantityToOrder = line.Quantity > vehicleMatchingSpecification.Quantity ? line.Quantity - vehicleMatchingSpecification.Quantity : 0;
                productionRequest.Items.Add(new ProductionRequestLineItem
                {
                    ItemType = ItemType.Vehicle,
                    ItemTypeId = line.VehicleId,
                    OrderId = line.OrderId,
                    OrderItemId = line.OrderItemId,
                    Quantity = quantityToOrder,
                    SpecificationKey = specificationKey,
                });

                await _itemsRepository.ReduceStockCount(specificationKey, line.Quantity - quantityToOrder);
                orderItemsAvailabilityUpdate.Lines.Add(new OrderItemAvailabilityLine { OrderItemId = line.OrderItemId, Status = OrderItemAvailabilityStatus.Available });
            }
        }
    }
}
