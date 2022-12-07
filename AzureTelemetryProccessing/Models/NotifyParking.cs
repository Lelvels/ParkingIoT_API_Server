using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTelemetryProccessing.Models
{
    public class NotifyParking
    {
        public NotifyParking(string method, string parkingSlotId)
        {
            Method = method;
            ParkingSlotId = parkingSlotId;
        }

        public string Method { get; set; }
        public string ParkingSlotId { get; set; }
    }
}
