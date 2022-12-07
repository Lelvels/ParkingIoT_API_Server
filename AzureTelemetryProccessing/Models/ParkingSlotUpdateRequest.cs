using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTelemetryProccessing.Models
{
    public class ParkingSlotUpdateRequest
    {
        public ParkingSlotUpdateRequest(string name, int status)
        {
            this.name = name;
            this.status = status;
        }
        public string name { get; set; }
        public int status { get; set; }
    }
}
