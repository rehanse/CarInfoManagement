using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarInfoManagement.Models
{
    public class CarReferenceViewModel
    {
        public List<CarTransmissionType> cartransmisionTypes { get; set; }
        public List<Manufacturer> manufacturer { get; set; }
        public List<CarType> carType { get; set; }
        //public List<SelectListItem> manufacturerSelectList { get; set; }
       // public List<SelectListItem> cartransmisionTypesSelectList { get; set; }
       // public List<SelectListItem> carTypeSelectList { get; set; }
    }
}
