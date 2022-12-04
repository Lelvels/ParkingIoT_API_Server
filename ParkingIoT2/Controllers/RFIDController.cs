using Microsoft.AspNetCore.Mvc;
using ParkingIoT2.Repository;
using AutoMapper;
using System.Net;

namespace ParkingIoT2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RFIDController : Controller
    {
        private readonly IRFIDRepository rFIDRepository;
        private readonly IMapper mapper;

        public RFIDController(IRFIDRepository rFIDRepository, IMapper mapper)
        {
            this.rFIDRepository = rFIDRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRFID()
        {
            var rfid = await rFIDRepository.GetAllAsync();
            return Ok(rfid);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRFIDByIdAsync")]
        public async Task<IActionResult> GetRFIDByIdAsync(Guid id, 
            [FromQuery(Name = "includePA")] bool includeCus)
        {
            var rfid = await rFIDRepository.GetByIdAsync(id, includeCus);
            return Ok(rfid);
        }
        [HttpPost]
        public async Task<ActionResult> CreateRFID(Models.DTO.AddRFIDRequest addRFIDRequest)
        {
            Models.Domain.RFIDCode rfid = new();
            rfid.Id = Guid.NewGuid();
            rfid.Code = addRFIDRequest.Code;
            rfid = await rFIDRepository.AddAsync(rfid);
            return CreatedAtAction(nameof(GetRFIDByIdAsync),
                new { id = rfid.Id, includeCus = false },
                rfid);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateRFID([FromRoute] Guid id, 
            [FromBody] Models.DTO.UpdateRFIDRequest updateRFIDRequest)
        {
            var rfid = new Models.Domain.RFIDCode
            {
                Code = updateRFIDRequest.Code
            };
            rfid = await rFIDRepository.UpdateAsync(id, updateRFIDRequest.CustomerId,
                rfid);
            if (rfid == null)
                return NotFound("RFID Guid or Custormer Guid Not Found in Database!");
            return Ok(rfid);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteRFID([FromRoute] Guid id)
        {
            var rfid = await rFIDRepository.DeleteAsync(id);
            if (rfid == null)
                return NotFound();
            return Ok(rfid);
        }
    }
}
