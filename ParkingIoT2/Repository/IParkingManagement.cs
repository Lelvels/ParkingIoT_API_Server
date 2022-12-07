using ParkingIoT2.Models.Domain;
namespace ParkingIoT2.Repository
{
    public interface IParkingManagement
    {
        Task<RFIDCode>? onRFID(string code, Guid parkingAreaId);
    }
}
