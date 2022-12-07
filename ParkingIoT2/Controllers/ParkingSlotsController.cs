using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingIoT2.Repository;
using ParkingIoT2.Models.Constants;
using AutoMapper;

namespace ParkingIoT2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSlotsController : ControllerBase
    {
        private readonly IParkingSlotRepository parkingSlotRepository;
        private readonly IParkingAreaRepository parkingAreaRepository;
        private readonly IMapper mapper;

        public ParkingSlotsController(IParkingSlotRepository parkingSlotRepository,
            IParkingAreaRepository parkingAreaRepository,
            IMapper mapper)
        {
            this.parkingSlotRepository = parkingSlotRepository;
            this.parkingAreaRepository = parkingAreaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParkingSlotsAsync()
        {
            var parkingSlots = await parkingSlotRepository.GetAllAsync();
            var parkingSlotsDTO = mapper.Map<List<Models.DTO.ParkingSlotDTO>>(parkingSlots);
            return Ok(parkingSlotsDTO);
        }
        [HttpGet]
        [Route("paid/{paId:guid}")]
        [ActionName("GetParkingSlotsByPaIdAsync")]
        public async Task<IActionResult> GetParkingSlotsByPaIdAsync([FromRoute] Guid paId)
        {
            var parkingSlots = await parkingSlotRepository.GetAllParkingSlotsByParkingAreaId(paId);
            var parkingSlotsDTO = mapper.Map<List<Models.DTO.ParkingSlotDTO>>(parkingSlots);
            return Ok(parkingSlotsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetParkingSlotByIdAsync")]
        public async Task<IActionResult> GetParkingSlotByIdAsync(Guid id)
        {
            var parkingSlot = await parkingSlotRepository.GetByIdAsync(id);
            if (parkingSlot == null)
                return NotFound();
            var parkingSlotDTO = mapper.Map<Models.DTO.ParkingSlotDTO>(parkingSlot);
            return Ok(parkingSlotDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewParkingSlot(Models.DTO.AddParkingSlotRequest addParkingSlotRequest)
        {
            Models.Domain.ParkingArea parkingArea = await parkingAreaRepository
                .GetByIdAsync(addParkingSlotRequest.ParkingAreaId);
            if (parkingArea == null)
                return NotFound("Parking Area not found!");
            //Binding object
            var parkingSlot = new Models.Domain.ParkingSlot
            {
                Name = addParkingSlotRequest.Name,
                Status = Constants.PARKING_SLOT_EMPTY
            };
            parkingSlot.ParkingArea = parkingArea;
            parkingSlot = await parkingSlotRepository.AddAsync(parkingSlot);
            var parkingSlotDTO = mapper.Map<Models.DTO.ParkingSlotDTO>(parkingSlot);
            return CreatedAtAction(nameof(GetParkingSlotByIdAsync),
                new {id = parkingSlotDTO.Id, includePA = true},
                parkingSlotDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateParkingSlot([FromRoute] Guid id, [FromBody] Models.DTO.UpdateParkingSlotRequest updateParkingSlotRequest)
        {
            var parkingSlot = new Models.Domain.ParkingSlot
            {
                Name = updateParkingSlotRequest.Name,
                Status = updateParkingSlotRequest.Status
            };
            parkingSlot = await parkingSlotRepository.UpdateAsync(id, parkingSlot);
            if (parkingSlot != null)
            {
                var parkingSlotDTO = mapper.Map<Models.DTO.ParkingSlotDTO>(parkingSlot);
                return Ok(parkingSlotDTO);
            }
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteParkingSlot(Guid id)
        {
            var parkingSlot = await parkingSlotRepository.DeleteAsync(id);
            if(parkingSlot != null)
            {
                var parkingDTO = mapper.Map<Models.DTO.ParkingSlotDTO>(parkingSlot);
                return Ok(parkingDTO);
            } else
            {
                return NotFound();
            }
        }
    }
}
