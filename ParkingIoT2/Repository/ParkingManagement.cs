using ParkingIoT2.Models.Domain;
using ParkingIoT2.Data;
using Microsoft.EntityFrameworkCore;

namespace ParkingIoT2.Repository
{
    public class ParkingManagement : IParkingManagement
    {
        private readonly ParkingIOTDBContext parkingIOTDBContext;

        public ParkingManagement(ParkingIOTDBContext parkingIOTDBContext)
        {
            this.parkingIOTDBContext = parkingIOTDBContext;
        }

        public async Task<RFIDCode>? onRFID(string code, Guid parkingAreaId)
        {
            var rFIDCode = parkingIOTDBContext.RFIDCodes
                .FirstOrDefault(x => x.Code.Equals(code));
            if(rFIDCode != null)
            {
                var parkingArea = await parkingIOTDBContext.ParkingAreas
                    .FirstOrDefaultAsync(x => x.Id == parkingAreaId);
                if(parkingArea != null)
                {
                    parkingArea.NumberOfServes = parkingArea.NumberOfServes + 1;
                    await parkingIOTDBContext.SaveChangesAsync();
                }
            }
            return rFIDCode;
        }
    }
}
