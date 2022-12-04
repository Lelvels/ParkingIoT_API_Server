namespace ParkingIoT2.Models.DTO
{
    public class AddParkingAreaRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
