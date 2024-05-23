using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarInfoManagement.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        [Required]
        public string carName { get; set; }
        public string? carImage { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required]
        public string carModel { get; set; }
        public int manifactureId { get; set; }
        public int typeId { get; set; }
        [Required]
        public string engine { get; set; }//unique
        public int BHP { get; set; }
        public int carTransmissionId { get; set; }
        [Required]
        public int mileage { get; set; }
        [Required]
        public int seat { get; set; }
        [Required]
        public string airBagDetails { get; set; }
        [Required]
        public string bootspace { get; set; }
        [Required]
        public decimal price { get; set; }
        public string? CarTransmissionTypeName { get; set; }
        public string? type { get; set; }
        public string? manufacturerName { get; set; }//unique
        public string? contactPerson { get; set; }//unique
        public string? registeredOffice { get; set; }
        public string? accessToken { get; set; }
        public CarReferenceViewModel? carReferenceViewModel { get; set; }
        public CarType? CarType { get; set; }
        public CarTransmissionType? carTransmissionType { get; set; }
        public Manufacturer? manufacturer { get; set; }
    }
}
