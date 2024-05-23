using System.ComponentModel.DataAnnotations;

namespace CarInfoManagement.Models
{
    public class CarDetails
    {
        public int Id { get; set; }
        public string carName { get; set; }
        public string carImage { get; set; }
        public string carModel { get; set; }
        public int manifactureId { get; set; }
        public int typeId { get; set; }
        public string engine { get; set; }//unique
        public int BHP { get; set; }
        public int carTransmissionId { get; set; }
        public int mileage { get; set; }
        public int seat { get; set; }
        public string airBagDetails { get; set; }
        public string bootspace { get; set; }
        public decimal price { get; set; }
        public CarType CarType { get; set; }
        public CarTransmissionType carTransmissionType { get; set; }
        public Manufacturer manufacturer { get; set; }
    }
}
