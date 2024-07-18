using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotAPI.Models
{
    public class Slot
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public bool IsOccupied { get; set; }
        public int FloorId { get; set; }
        [ForeignKey("FloorId")]
        public Floor Floor { get; set; }
        public int? ParkedVehicleId { get; set; }
        [ForeignKey("ParkedVehicleId")]
        public Vehicle ParkedVehicle { get; set; }
    }
}
