namespace BoxCar.Catalogue.Domain
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        
        public Address(string street, string city, string state, string psstalCode, string country) 
        { 
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            PostalCode = psstalCode ?? throw new ArgumentNullException(nameof(psstalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }

        public override bool Equals(object? obj)
        {
            return obj is Address o
                && Street.Equals(o.Street, StringComparison.InvariantCultureIgnoreCase)
                && State.Equals(o.State, StringComparison.InvariantCultureIgnoreCase)
                && PostalCode.Equals(o.PostalCode, StringComparison.InvariantCultureIgnoreCase)
                && Country.Equals(o.Country, StringComparison.InvariantCultureIgnoreCase)
                && City.Equals(o.City, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return $"{Street}{City}{State}{PostalCode}{Country}".ToLower().GetHashCode();
        }
    }
}