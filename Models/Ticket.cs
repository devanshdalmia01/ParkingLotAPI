using System.ComponentModel.DataAnnotations;

namespace ParkingLotAPI.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string TicketId { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
