using System.ComponentModel.DataAnnotations;

namespace ParkingLotAPI.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }

        public Vehicle() { }

        public Vehicle(string type, string registrationNumber, string color)
        {
            Type = type;
            RegistrationNumber = registrationNumber;
            Color = color;
        }
    }
}
