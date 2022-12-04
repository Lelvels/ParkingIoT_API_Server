namespace ParkingIoT2.Models.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? LicensePlate { get; set; }
    }
}
