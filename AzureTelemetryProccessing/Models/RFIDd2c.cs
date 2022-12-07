using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTelemetryProccessing.Models
{
    public class RFIDd2c
    {
        public RFIDd2c(bool result, int id)
        {
            Result = result;
            Id = id;
        }

        public bool Result { get; set; }
        public int Id { get; set; }
    }
}
