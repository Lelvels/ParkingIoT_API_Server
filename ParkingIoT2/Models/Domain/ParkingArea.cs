namespace ParkingIoT2.Models.Domain
{
    public class ParkingArea
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int NumberOfServes { get; set; }
        //Navigation Property
        public IEnumerable<ParkingSlot> ? ParkingSlots { get; set; }
    }
}
