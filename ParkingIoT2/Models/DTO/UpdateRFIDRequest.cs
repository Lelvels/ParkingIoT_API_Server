namespace ParkingIoT2.Models.DTO
{
    public class UpdateRFIDRequest
    {
        public string Code { get; set; }
        public Guid CustomerId { get; set; }
    }
}
