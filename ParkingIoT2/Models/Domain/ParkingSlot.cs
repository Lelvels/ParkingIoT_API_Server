namespace ParkingIoT2.Models.Domain
{
    public class ParkingSlot
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
        //Navigation Property
        public ParkingArea? ParkingArea { get; set; }
    }
}
