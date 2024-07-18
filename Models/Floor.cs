using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotAPI.Models
{
    public class Floor
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int ParkingLotId { get; set; }
        public List<Slot> Slots { get; set; }

        public Floor()
        {
            Slots = new List<Slot>();
        }
    }
}
