using Microsoft.EntityFrameworkCore;

namespace ParkingIoT2.Models.Domain
{
    public class RFIDCode
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public Customer? Customer { get; set; }
    }
}
