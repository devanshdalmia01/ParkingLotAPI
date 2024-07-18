using Microsoft.AspNetCore.Mvc;
using ParkingLotAPI.Models;
using ParkingLotAPI.Services;
using System.Threading.Tasks;

namespace ParkingLotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService _parkingLotService;

        public ParkingLotController(ParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateParkingLot([FromBody] ParkingLotRequest request)
        {
            await _parkingLotService.Initialize(request.ParkingLotId, request.NumberOfFloors, request.NumberOfSlotsPerFloor);
            return Ok(new { message = $"Created parking lot with {request.NumberOfFloors} floors and {request.NumberOfSlotsPerFloor} slots per floor" });
        }

        [HttpPost("park")]
        public async Task<IActionResult> ParkVehicle([FromBody] ParkVehicleRequest request)
        {
            string ticketId = await _parkingLotService.ParkVehicle(new Vehicle(request.VehicleType, request.RegistrationNumber, request.Color));
            if (ticketId == "Parking Lot Full")
            {
                return BadRequest(new { message = ticketId });
            }
            return Ok(new { ticketId });
        }

        [HttpPost("unpark")]
        public async Task<IActionResult> UnparkVehicle([FromBody] UnparkVehicleRequest request)
        {
            string result = await _parkingLotService.UnparkVehicle(request.TicketId);
            if (result == "Invalid Ticket")
            {
                return BadRequest(new { message = result });
            }
            return Ok(new { message = result });
        }

        [HttpGet("display/free_count/{vehicleType}")]
        public async Task<IActionResult> DisplayFreeCount(string vehicleType)
        {
            var result = await _parkingLotService.DisplayFreeCount(vehicleType);
            return Ok(result);
        }

        [HttpGet("display/free_slots/{vehicleType}")]
        public async Task<IActionResult> DisplayFreeSlots(string vehicleType)
        {
            var result = await _parkingLotService.DisplayFreeSlots(vehicleType);
            return Ok(result);
        }

        [HttpGet("display/occupied_slots/{vehicleType}")]
        public async Task<IActionResult> DisplayOccupiedSlots(string vehicleType)
        {
            var result = await _parkingLotService.DisplayOccupiedSlots(vehicleType);
            return Ok(result);
        }
    }

    public class ParkingLotRequest
    {
        public string ParkingLotId { get; set; }
        public int NumberOfFloors { get; set; }
        public int NumberOfSlotsPerFloor { get; set; }
    }

    public class ParkVehicleRequest
    {
        public string VehicleType { get; set; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
    }

    public class UnparkVehicleRequest
    {
        public string TicketId { get; set; }
    }
}
