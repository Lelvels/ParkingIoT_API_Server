using ParkingIoT2.Models.Domain;
namespace ParkingIoT2.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(Guid id);
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> UpdateAsync(Guid id, Customer customer);
        Task<Customer> DeleteAsync(Guid id);
    }
}
