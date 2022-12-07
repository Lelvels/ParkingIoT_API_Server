using ParkingIoT2.Models.Domain;
namespace ParkingIoT2.Repository
{
    public interface IParkingSlotRepository
    {
        Task<IEnumerable<ParkingSlot>> GetAllAsync();
        Task<ParkingSlot> GetByIdAsync(Guid id);
        Task<IEnumerable<ParkingSlot>> GetAllParkingSlotsByParkingAreaId(Guid parkingAreaId);
        Task<ParkingSlot> AddAsync(ParkingSlot parkingSlot);
        Task<ParkingSlot> UpdateAsync(Guid id, ParkingSlot parkingSlot);
        Task<ParkingSlot> DeleteAsync(Guid id);
    }
}
