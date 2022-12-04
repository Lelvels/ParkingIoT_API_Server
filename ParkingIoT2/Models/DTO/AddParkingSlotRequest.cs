namespace ParkingIoT2.Models.DTO
{
    public class AddParkingSlotRequest
    {
        public string? Name { get; set; }
        public Guid ParkingAreaId { get; set; }
    }
}
