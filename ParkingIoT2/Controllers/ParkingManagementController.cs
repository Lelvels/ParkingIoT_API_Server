using Microsoft.AspNetCore.Mvc;
using ParkingIoT2.Repository;

namespace ParkingIoT2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingManagementController : Controller
    {
        private readonly IParkingManagement parkingManagement;

        public ParkingManagementController(ParkingIoT2.Repository.IParkingManagement parkingManagement)
        {
            this.parkingManagement = parkingManagement;
        }
        [HttpGet]
        public async Task<IActionResult> onRFIDIn([FromQuery(Name = "parkingAreaId")] Guid parkingAreaId, [FromQuery(Name = "RFIDCode")] string RFIDCode)
        {
            var result = await parkingManagement.onRFID(RFIDCode, parkingAreaId);
            if(result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
