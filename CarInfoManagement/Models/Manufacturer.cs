using System.ComponentModel.DataAnnotations;

namespace CarInfoManagement.Models
{
    public class Manufacturer
    {
        public int id { get; set; }
        public string name { get; set; }//unique
        public string contactPerson { get; set; }//unique
        public string registeredOffice { get; set; }
    }
}
