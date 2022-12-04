namespace ParkingIoT2.Models.DTO
{
    public class ParkingSlotDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
        public ParkingAreaDTO? ParkingArea { get; set; }
    }
}
