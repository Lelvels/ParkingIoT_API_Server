using ParkingIoT2.Data;
using ParkingIoT2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ParkingIoT2.Repository
{
    public class ParkingAreaRepository : IParkingAreaRepository
    {
        private readonly ParkingIOTDBContext parkingIOTDBContext;

        public ParkingAreaRepository(ParkingIoT2.Data.ParkingIOTDBContext parkingIOTDBContext)
        {
            this.parkingIOTDBContext = parkingIOTDBContext;
        }

        public async Task<ParkingArea> AddAsync(ParkingArea parkingArea)
        {
            parkingArea.Id = Guid.NewGuid();
            await parkingIOTDBContext.AddAsync(parkingArea);
            await parkingIOTDBContext.SaveChangesAsync();
            return parkingArea;
        }

        public async Task<ParkingArea> DeleteAsync(Guid id)
        {
            var parkingArea = await parkingIOTDBContext
                .ParkingAreas
                .FirstOrDefaultAsync(x => x.Id == id);
            if(parkingArea == null)
                return null;
            else
            {
                parkingIOTDBContext.ParkingAreas.Remove(parkingArea);
                await parkingIOTDBContext.SaveChangesAsync();
                return parkingArea;
            }
        }

        public async Task<IEnumerable<ParkingArea>> GetAllAsync()
        {
            var parkingAreas = await parkingIOTDBContext
                .ParkingAreas
                .ToListAsync();
            return parkingAreas;
        }

        public async Task<ParkingArea> GetByIdAsync(Guid id)
        {
            var parkingArea = await parkingIOTDBContext
                .ParkingAreas
                .FirstOrDefaultAsync(x => x.Id == id);
            return parkingArea;
        }

        public async Task<ParkingArea> UpdateAsync(Guid id, ParkingArea parkingArea)
        {
            var existingParkingArea = await GetByIdAsync(id);
            if(existingParkingArea == null)
            {
                return null;
            }
            existingParkingArea.Name = parkingArea.Name;
            existingParkingArea.Address = parkingArea.Address;
            await parkingIOTDBContext.SaveChangesAsync();
            return existingParkingArea;
        }
    }
}
