#BoxCar
This is a collection ASP.NET Core Microservices designed for a customized vehicle manufacturer. The application relies heavily on an Azure Service Bus for service-to-service communication.



How to Setup and Run
1. Create an Azure Service Bus. In the zip folder you will an exported template that can help automate this process
2. Add the following user secrets. Please replace sample values here with actual values from your Azure Subscription
```
{
  "ServiceBusConnectionString": "Endpoint=sb://<your-namespace>.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=<your_key>",
  "ApplicationInsights:ConnectionString": "InstrumentationKey=

```
3. In Visual Studio click the Docker Compose Run button or you may navigate to the root directory via cmd and execute docker-compose up although this second approach has not been tested.

##Technology
- ASP.NET Core
- Microsoft SQL Server in a docker container
- Azure Service Bus
- Redis in a docker container
- Azure Application Insights

##Project Structure
Each high level solution folder contains it's own micro service. Here follows a description of what each folder contains
- BoxCar.Integration: Shared logic for reading and writing to a Service Bus
- BoxCar.Services.Administration: This is the main application. It is used for data entry and is the single source of truth for data about Vehicles, Engines, Chassis, Option Packs and options
  In this solution I used multiple sub projects each named according to its duty. Domain has the entities, Core is the application proper, Persistence for saving informtion and there should have been Infrastructure for Message Bus communication
- BoxCar.Services.Catalogue is supposed to be the endpoint that serves the frontend clients. Its job is to read data from administration and serve it out
- BoxCar.Services.Notifications is a worker service that will send out messages meant for the user like via SendGrid
- BoxCar.Services.Ordering handles orders. Saves them locally, posts payment request messages out and responds to them
- BoxCar.Services.


##Testing
I have included a Postman collection that contains the series tests used to verify that the system fulfills the basic requirements in the assessment. I followed these steps

1. Create Chassis

Target
- http://localhost:5010/api/chassis

- Sample Payload:
```
{
  "id": "b74e2eda-60a5-45c2-b43a-a92b614359fa",
  "name": "Chassis b74",
  "description": "Chassis b74 despcription",
  "price": 2345
}
```

2. Create Engine

Target
- http://localhost:5010/api/engines

Payload
```
{
  "id": "87f19e92-fa5b-4755-ace7-71575d6aa242",
  "name": "Engine 87f",
  "fuelType": 1,
  "ignitionMethod": 1,
  "strokes": 2,
  "price": 1350
}
```

3. Create Option Pack

Target
- http://localhost:5010/api/optionpacks

Payload
```
{
  "id": "8ece8ec2-b8d2-4e74-991d-21263b3a9df0",
  "name": "Option Pack 8ec"
}
```

I would have preferred a payload that contains a nested collection of Options, but I did not have time to write and test a custom model binder

4. Create Vehicle
Target
- http://localhost:5010/api/vehicles

Payload
```
{
  "id": "13da2043-2071-46ad-82d4-23dc4fdf9efc",
  "name": "Vehicle 13d",
  "chassisId": "b74e2eda-60a5-45c2-b43a-a92b614359fa",
  "engineId": "87f19e92-fa5b-4755-ace7-71575d6aa242",
  "optionPackId": "8ece8ec2-b8d2-4e74-991d-21263b3a9df0",
  "price": 4950
}
```

5. Create Basket
Target
- http://localhost:5006/api/baskets

Payload
```
{
  "userId": "4be8dfdc-7ae2-4cf4-9e0f-c19ea4c32b32"
}
```
The response will include your basket id for example
```
{
    "basketId": "77458c58-5157-446e-1b63-08dbd144e0bc", <--- your basket id
    "userId": "4be8dfdc-7ae2-4cf4-9e0f-c19ea4c32b32",
    "numberOfItems": 0
}
```
6. Add to basket
Target
- http://localhost:5006/api/baskets/{insert-your-basket-id-here}/basketlines

Payload
```
{
  "vehicleId": "13da2043-2071-46ad-82d4-23dc4fdf9efc",
  "chassisId": "b74e2eda-60a5-45c2-b43a-a92b614359fa",
  "engineId": "87f19e92-fa5b-4755-ace7-71575d6aa242",
  "optionPackId": "8ece8ec2-b8d2-4e74-991d-21263b3a9df0",
  "quantity":3,
  "price":2450
}
```
Sample Response
```
{
    "basketLineId": "0faf8917-b572-416a-f43c-08dbd14504bf",
    "basketId": "77458c58-5157-446e-1b63-08dbd144e0bc",
    "vehicleId": "13da2043-2071-46ad-82d4-23dc4fdf9efc",
    "price": 2450,
    "quantity": 3
}
```

7. Checkout
Target
- http://localhost:5006/api/baskets/checkout

Payload
```
{
  "basketId": "77458c58-5157-446e-1b63-08dbd144e0bc",
  "firstName": "Emem",
  "lastName": "Isaac",
  "email": "emem@isaac.com",
  "address": "1 Fun Avenue",
  "zipCode": "89345",
  "city": "Fairview",
  "country": "Nigeria",
  "userId": "62369021-0811-4f17-bbce-22fcd05d1b63",
  "cardNumber": "1111-2222-3333-4444",
  "cardName": "Emem Isaac",
  "cardExpiration": "0126",
  "cvvCode": "123"
}
```
Sample Response
```
{
    "basketId": "77458c58-5157-446e-1b63-08dbd144e0bc",
    "firstName": "Emem",
    "lastName": "Isaac",
    "email": "emem@isaac.com",
    "address": "1 Fun Avenue",
    "zipCode": "89345",
    "city": "Fairview",
    "country": "Nigeria",
    "userId": "62369021-0811-4f17-bbce-22fcd05d1b63",
    "cardNumber": "1111-2222-3333-4444",
    "cardName": "Emem Isaac",
    "cardExpiration": "0126",
    "basketLines": [
        {
            "basketLineId": "0faf8917-b572-416a-f43c-08dbd14504bf",
            "vehicleId": "13da2043-2071-46ad-82d4-23dc4fdf9efc",
            "engineId": "87f19e92-fa5b-4755-ace7-71575d6aa242",
            "chassisId": "b74e2eda-60a5-45c2-b43a-a92b614359fa",
            "optionPackId": "8ece8ec2-b8d2-4e74-991d-21263b3a9df0",
            "price": 2450,
            "quantity": 3,
            "amount": 7350
        }
    ],
    "basketTotal": 7350,
    "id": "00000000-0000-0000-0000-000000000000",
    "creationDateTime": "0001-01-01T00:00:00"
}
```

Calling the checkout endpoint triggers the workflow. You can keep repeating steps 6 and 7 to trigger multiple runs

##How the solution meets the assessment criteria
### Modularity, Maintainability and scalability - 
- One microservice for each key business function
- Each uses its own database, models and entities

### Containerisation
- All microservices run in containers, 
- Those which need to persist data do so to an sql server running in a container

### Mediator & CQRS Patterns
- This is well demonstrated in the BoxCar.Services.Administration solution folder. There you will find that the controllers use mediators to get work done
- Each feature in BoxCar.Services.Administration is implemented via Requests and Request Handlers, there you will find commands to write data and queries to read data
Unfortunately due to time constraints I could not replicte this pattern in all the microservices.

### DDD Patterns
- In the BoxCar.Services.Administration solution folder you wil find the BoxCar.Admin.Core project. Notice that the entities like Vehicle, Option, Engine and so on follow this pattern
- This is also demonstrated in the variance in Entity model design betweeen the Administration project and the Warehouse project. The warehouse deals with Items

### Event Based Patterns
The whole application architecture depends on events. When a vehicle or any of its components are created in the administration service, these events are published to their respective service bus topics to be picked up by any microservice which that needs that information. In this case the Catalogue, Shopping Basket and Warehouse services all use that information. The same thing happens when a checkout occurs, when a payment is requested, when an order can be fulfilled, when a component needs to be manufactured etc

### Structured Logging & Request Correlation
This is done in many places in the application like when beginning a checkout process. Serilog and a number of its Enrichers have been utilized to enrich log data and capture request trace id across microservices

### Error handling
Where necessary try catch blocks ensure that we catch and log exceptions and global exception middleware has been added

### Configuration
The application depends heavily on reading configuration values from environment variables or appsettings. It reads service bus topics, connection strings and the like from configuration

## Next Steps
1. Ensure all services use the recommend patterns above
2. Add a frontend
3. Add Identity
4. Add integration tests
