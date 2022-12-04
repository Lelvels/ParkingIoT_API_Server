using ParkingIoT2.Models.Domain;
namespace ParkingIoT2.Repository
{
    public interface IRFIDRepository
    {
        Task<IEnumerable<RFIDCode>> GetAllAsync();
        Task<RFIDCode> GetByIdAsync(Guid id, bool includeCus);
        Task<RFIDCode> AddAsync(RFIDCode code);
        Task<RFIDCode> UpdateAsync(Guid id, Guid CustomerId, RFIDCode code);
        Task<RFIDCode> DeleteAsync(Guid id);
    }
}
