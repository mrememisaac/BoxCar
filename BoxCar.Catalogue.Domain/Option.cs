﻿namespace BoxCar.Catalogue.Domain
{
    public class Option : Entity
    {
        public string Name { get; private set; } = null!;
        public string Value { get; private set; } = null!;

        public Option(Guid id, string name, string value)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
            ChangeName(name);
        }

        public void ChangeName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public void ChangeValue(string value)
        {
            Name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override bool Equals(object? obj)
        {
            return obj is Option o 
                && Name.Equals(o.Name, StringComparison.InvariantCultureIgnoreCase) 
                && Value.Equals(o.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return $"{Name}{Value}".ToLower().GetHashCode();
        }
    }
}