using AutoMapper;
namespace ParkingIoT2.Profiles
{
    public class ParkingAreaProfiles : Profile
    {
        public ParkingAreaProfiles()
        {
            CreateMap<Models.Domain.ParkingArea, Models.DTO.ParkingAreaDTO>()
                .ReverseMap();
            CreateMap<Models.Domain.ParkingSlot, Models.DTO.ParkingSlotDTO>()
                .ReverseMap();
            CreateMap<Models.Domain.Customer, Models.DTO.CustomerDTO>()
                .ReverseMap();
            CreateMap<Models.Domain.RFIDCode, Models.DTO.RfidDTO>()
                .ReverseMap();
        }
    }
}
