using ParkingIoT2.Data;
using ParkingIoT2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ParkingIoT2.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ParkingIOTDBContext parkingIOTDBContext;

        public CustomerRepository(ParkingIoT2.Data.ParkingIOTDBContext parkingIOTDBContext)
        {
            this.parkingIOTDBContext = parkingIOTDBContext;
        }

        public Task<Customer> AddAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await parkingIOTDBContext.Customers
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await parkingIOTDBContext.Customers
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Customer> UpdateAsync(Guid id, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
