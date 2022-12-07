﻿using ParkingIoT2.Models.Domain;
using ParkingIoT2.Data;
using Microsoft.EntityFrameworkCore;

namespace ParkingIoT2.Repository
{
    public class ParkingSlotRepository : IParkingSlotRepository
    {
        private readonly ParkingIOTDBContext parkingIOTDBContext;

        public ParkingSlotRepository(ParkingIOTDBContext parkingIOTDBContext)
        {
            this.parkingIOTDBContext = parkingIOTDBContext;
        }
        public async Task<IEnumerable<ParkingSlot>> GetAllAsync()
        {
            return await parkingIOTDBContext.ParkingSlots
                .ToListAsync();
        }
        public Task<ParkingSlot> GetByIdAsync(Guid id)
        {
            return parkingIOTDBContext.ParkingSlots
                //.Include(x => x.ParkingArea)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ParkingSlot>> GetAllParkingSlotsByParkingAreaId(Guid parkingAreaId)
        {
            return await parkingIOTDBContext.ParkingSlots
                .Where(x => x.ParkingArea.Id == parkingAreaId)
                .ToListAsync();
        }
        public async Task<ParkingSlot> AddAsync(ParkingSlot parkingSlot)
        {
            parkingSlot.Id = new Guid();
            await parkingIOTDBContext.AddAsync(parkingSlot);
            await parkingIOTDBContext.SaveChangesAsync();
            return parkingSlot;
        }
        public async Task<ParkingSlot> UpdateAsync(Guid id, ParkingSlot parkingSlot)
        {
            ParkingSlot oldParkingSlot = await GetByIdAsync(id);
            if(oldParkingSlot == null)
            {
                return null;
            }
            oldParkingSlot.Name = parkingSlot.Name;
            oldParkingSlot.Status = parkingSlot.Status;
            await parkingIOTDBContext.SaveChangesAsync();
            return oldParkingSlot;
        }
        public async Task<ParkingSlot> DeleteAsync(Guid id)
        {
            ParkingSlot parkingSlot = await GetByIdAsync(id);
            if (parkingSlot == null)
                return null;
            parkingIOTDBContext.ParkingSlots.Remove(parkingSlot);
            await parkingIOTDBContext.SaveChangesAsync();
            return parkingSlot;
        }
    }
}
