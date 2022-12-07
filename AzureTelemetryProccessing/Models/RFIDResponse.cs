using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTelemetryProccessing.Models
{
    public class RFIDResponse
    {
        public RFIDResponse() { }
        public Guid id { get; set; }
        public string code { get; set; }
        public string customer { get; set; }
    }
}
