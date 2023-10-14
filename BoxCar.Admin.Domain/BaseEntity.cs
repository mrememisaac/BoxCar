namespace BoxCar.Admin.Domain
{

    public abstract class BaseEntity<TId> where TId : notnull
    {
        public TId Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set;}

        public string CreatedBy { get; set; } = string.Empty;

        public string UpdatedBy { get; set;} = string.Empty;

        public override bool Equals(object? obj)
        {
            return obj is BaseEntity<TId> other && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}