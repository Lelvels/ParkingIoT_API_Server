using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTelemetryProccessing.Models
{
    public class NotifyParkingResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int status { get; set; }  
        public string parkingArea { get; set; }
    }
}
