namespace ParkingIoT2.Models.DTO
{
    public class RfidDTO
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public CustomerDTO? CustomerDTO { get; set; }
    }
}
