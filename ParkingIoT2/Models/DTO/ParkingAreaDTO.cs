namespace ParkingIoT2.Models.DTO
{
    public class ParkingAreaDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int NumberOfServes { get; set; }
        
    }
}
