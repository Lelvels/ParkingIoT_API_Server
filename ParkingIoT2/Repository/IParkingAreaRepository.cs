using ParkingIoT2.Models.Domain;
namespace ParkingIoT2.Repository
{
    public interface IParkingAreaRepository
    {
        Task<IEnumerable<ParkingArea>> GetAllAsync();
        Task<ParkingArea> GetByIdAsync(Guid id);
        Task<ParkingArea> AddAsync(ParkingArea parkingArea);
        Task<ParkingArea> DeleteAsync(Guid id);
        Task<ParkingArea> UpdateAsync(Guid id, ParkingArea parkingArea); 
    }
}
