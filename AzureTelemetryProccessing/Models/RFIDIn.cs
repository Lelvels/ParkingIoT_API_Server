using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTelemetryProccessing.Models
{
    public class RFIDIn
    {
        public RFIDIn(string method, bool gate, string rFIDCode, string parkingAreaId, int id)
        {
            Method = method;
            Gate = gate;
            RFIDCode = rFIDCode;
            ParkingAreaId = parkingAreaId;
            Id = id;
        }
        public string Method { get; set; }
        public bool Gate { get; set; }
        public string RFIDCode { get; set; }
        public string ParkingAreaId { get; set; }
        public int Id { get; set; }

    }
}
