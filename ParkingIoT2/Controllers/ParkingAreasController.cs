using Microsoft.AspNetCore.Mvc;
using ParkingIoT2.Repository;
using AutoMapper;
using ParkingIoT2.Models.DTO;

namespace ParkingIoT2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingAreasController : Controller
    {
        private readonly IParkingAreaRepository parkingAreaRepository;
        private readonly IMapper mapper;

        public ParkingAreasController
            (ParkingIoT2.Repository.IParkingAreaRepository parkingAreaRepository,
            IMapper mapper)
        {
            this.parkingAreaRepository = parkingAreaRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllParkingAreas()
        {
            var parkingAreas = await this.parkingAreaRepository.GetAllAsync();
            var parkingDTO = mapper.Map<List<Models.DTO.ParkingAreaDTO>>(parkingAreas);
            return Ok(parkingDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetParkingArea")]
        public async Task<IActionResult> GetParkingArea(Guid id)
        {
            var parkingArea = await this.parkingAreaRepository.GetByIdAsync(id);
            if (parkingArea == null)
                return NotFound();
            var parkingAreaDTO = mapper.Map<Models.DTO.ParkingAreaDTO>(parkingArea);
            return Ok(parkingAreaDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateParkingArea([FromRoute] Guid id, AddParkingAreaRequest addParkingAreaRequest)
        {
            var parkingArea = new Models.Domain.ParkingArea()
            {
                Name = addParkingAreaRequest.Name,
                Address = addParkingAreaRequest.Address
            };
            var updateParkingArea = await parkingAreaRepository
                .UpdateAsync(id, parkingArea);
            if (updateParkingArea == null)
                return NotFound();
            var parkingAreaDTO = mapper.Map<Models.DTO.ParkingAreaDTO>(updateParkingArea);
            return Ok(parkingAreaDTO);          
        }
        [HttpPost]
        public async Task<IActionResult> CreateParkingArea(AddParkingAreaRequest addParkingAreaRequest)
        {
            var parkingArea = new Models.Domain.ParkingArea()
            {
                Name = addParkingAreaRequest.Name,
                Address = addParkingAreaRequest.Address,
                NumberOfServes = 0
            };
            parkingArea = await parkingAreaRepository.AddAsync(parkingArea);
            var parkingAreaDTO = mapper.Map<Models.DTO.ParkingAreaDTO>(parkingArea);
            return CreatedAtAction(nameof(GetParkingArea),
                new {id = parkingAreaDTO.Id},
                parkingAreaDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteParkingArea(Guid id)
        {
            //Get region from database
            var region = await parkingAreaRepository.GetByIdAsync(id);
            //If null NotFound
            if (region == null)
            {
                return NotFound();
            }
            //Delete the object
            region = await parkingAreaRepository.DeleteAsync(id);
            //covert back to DTO
            var regionDTO = mapper.Map<Models.DTO.ParkingAreaDTO>(region);
            return Ok(regionDTO);
        }
    }
}
