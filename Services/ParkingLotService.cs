using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Models;

namespace ParkingLotAPI.Services
{
    public class ParkingLotService
    {
        private readonly ParkingLotContext _context;

        public ParkingLotService(ParkingLotContext context)
        {
            _context = context;
        }

        public async Task Initialize(string id, int numberOfFloors, int numberOfSlotsPerFloor)
        {
            for (int i = 1; i <= numberOfFloors; i++)
            {
                var floor = new Floor { Number = i };
                for (int j = 1; j <= numberOfSlotsPerFloor; j++)
                {
                    string slotType = (j == 1) ? "TRUCK" : (j <= 3) ? "BIKE" : "CAR";
                    var slot = new Slot { Number = j, Type = slotType, Floor = floor };
                    floor.Slots.Add(slot);
                }
                _context.Floors.Add(floor);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<string> ParkVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "Vehicle cannot be null");
            }

            var slots = await _context.Slots
                .Include(s => s.Floor)
                .Where(s => !s.IsOccupied && s.Type == vehicle.Type)
                .OrderBy(s => s.Floor.Number).ThenBy(s => s.Number)
                .ToListAsync();

            if (slots == null || !slots.Any())
            {
                return "Parking Lot Full";
            }

            var slot = slots.First();
            slot.IsOccupied = true;
            slot.ParkedVehicle = vehicle;

            _context.Vehicles.Add(vehicle);
            _context.Slots.Update(slot);

            await _context.SaveChangesAsync();

            string ticketId = $"PR1234_{slot.Floor.Number}_{slot.Number}";
            return ticketId;
        }

        public async Task<string> UnparkVehicle(string ticketId)
        {
            var parts = ticketId.Split('_');
            if (parts.Length != 3) return "Invalid Ticket";

            int floorNumber = int.Parse(parts[1]);
            int slotNumber = int.Parse(parts[2]);

            var slot = await _context.Slots
                .Include(s => s.ParkedVehicle)
                .FirstOrDefaultAsync(s => s.Floor.Number == floorNumber && s.Number == slotNumber);

            if (slot == null || !slot.IsOccupied) return "Invalid Ticket";

            var vehicle = slot.ParkedVehicle;
            slot.IsOccupied = false;
            slot.ParkedVehicle = null;

            _context.Slots.Update(slot);
            await _context.SaveChangesAsync();

            return $"Unparked vehicle with Registration Number: {vehicle.RegistrationNumber} and Color: {vehicle.Color}";
        }

        public async Task<List<string>> DisplayFreeCount(string vehicleType)
        {
            var result = new List<string>();
            var floors = await _context.Floors.Include(f => f.Slots).ToListAsync();
            foreach (var floor in floors)
            {
                int freeCount = floor.Slots.Count(s => !s.IsOccupied && s.Type == vehicleType);
                result.Add($"No. of free slots for {vehicleType} on Floor {floor.Number}: {freeCount}");
            }
            return result;
        }

        public async Task<List<string>> DisplayFreeSlots(string vehicleType)
        {
            var result = new List<string>();
            var floors = await _context.Floors.Include(f => f.Slots).ToListAsync();
            foreach (var floor in floors)
            {
                var freeSlots = floor.Slots.Where(s => !s.IsOccupied && s.Type == vehicleType).Select(s => s.Number);
                string freeSlotNumbers = string.Join(",", freeSlots);
                result.Add($"Free slots for {vehicleType} on Floor {floor.Number}: {freeSlotNumbers}");
            }
            return result;
        }

        public async Task<List<string>> DisplayOccupiedSlots(string vehicleType)
        {
            var result = new List<string>();
            var floors = await _context.Floors.Include(f => f.Slots).ToListAsync();
            foreach (var floor in floors)
            {
                var occupiedSlots = floor.Slots.Where(s => s.IsOccupied && s.Type == vehicleType).Select(s => s.Number);
                string occupiedSlotNumbers = string.Join(",", occupiedSlots);
                result.Add($"Occupied slots for {vehicleType} on Floor {floor.Number}: {occupiedSlotNumbers}");
            }
            return result;
        }
    }
}
