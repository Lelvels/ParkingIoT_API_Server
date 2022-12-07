using ParkingIoT2.Data;
using ParkingIoT2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ParkingIoT2.Repository
{
    public class RFIDRepository : IRFIDRepository
    {
        private readonly ParkingIOTDBContext parkingIOTDBContext;

        public RFIDRepository(ParkingIoT2.Data.ParkingIOTDBContext parkingIOTDBContext)
        {
            this.parkingIOTDBContext = parkingIOTDBContext;
        }

        public async Task<RFIDCode> AddAsync(RFIDCode code)
        {
            //Assign new Guid
            code.Id = new Guid();
            await parkingIOTDBContext.AddAsync(code);
            await parkingIOTDBContext.SaveChangesAsync();
            return code;
        }

        public async Task<RFIDCode> DeleteAsync(Guid id)
        {
            RFIDCode code = await GetByIdAsync(id, false);
            if (code == null)
                return null;
            parkingIOTDBContext.RFIDCodes.Remove(code);
            await parkingIOTDBContext.SaveChangesAsync();
            return code;
        }

        public async Task<IEnumerable<RFIDCode>> GetAllAsync()
        {
            return await parkingIOTDBContext.RFIDCodes
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public async Task<RFIDCode> GetByCodeAsync(string code, bool includeCus)
        {
            if (includeCus)
                return await parkingIOTDBContext.RFIDCodes
                    .Include(x => x.Customer)
                    .FirstOrDefaultAsync(x => x.Code.Equals(code));
            return await parkingIOTDBContext.RFIDCodes
                .FirstOrDefaultAsync(x => x.Code.Equals(code));
        }

        public async Task<RFIDCode> GetByIdAsync(Guid id, bool includeCus)
        {
            if (includeCus)
                return await parkingIOTDBContext.RFIDCodes
                    .Include(x => x.Customer)
                    .FirstOrDefaultAsync(x => x.Id == id);
            return await parkingIOTDBContext.RFIDCodes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RFIDCode> UpdateAsync(Guid id, Guid CustomerId, RFIDCode code)
        {
            var oldCode = await GetByIdAsync(id, false);
            //If the RFID code not found then return null
            if (oldCode == null)
                return null;
            oldCode.Code = code.Code;
            oldCode.Customer = await
                parkingIOTDBContext.Customers
                .FirstOrDefaultAsync(x => x.Id == CustomerId);
            //If customer not found then return null
            if (oldCode.Customer == null)
                return null;
            await parkingIOTDBContext.SaveChangesAsync();
            return oldCode;
        }
    }
}
