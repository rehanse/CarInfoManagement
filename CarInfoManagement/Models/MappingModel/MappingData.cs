using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;

namespace CarInfoManagement.Models.MappingModel
{
    public class MappingData
    {
        public List<CarViewModel> MapsCarData(List<CarDetails> carDetails)
        {
            List<CarViewModel> carViewModels = new List<CarViewModel>();
            carViewModels = carDetails.Select(x => new CarViewModel
            {
                Id = x.Id,
                carName = x.carName,
                carImage = x.carImage,
                carModel = x.carModel,
                manifactureId = x.manifactureId,
                typeId = x.typeId,
                engine = x.engine,
                BHP = x.BHP,
                carTransmissionId = x.carTransmissionId,
                mileage = x.mileage,
                seat = x.seat,
                airBagDetails = x.airBagDetails,
                bootspace = x.bootspace,
                price = x.price,
                manufacturerName = x.manufacturer.name,
                CarTransmissionTypeName = x.carTransmissionType.name,
                type = x.CarType.type,
                contactPerson = x.manufacturer.contactPerson,
                registeredOffice = x.manufacturer.registeredOffice,
            }).ToList();
            return carViewModels;
        }
        public void GetReferenceCarData(CarViewModel carViewModel, int id)
        {
            carViewModel.Id= id;
            carViewModel.manufacturerName = carViewModel.manufacturer.name;
            carViewModel.registeredOffice = carViewModel.manufacturer.registeredOffice;
            carViewModel.contactPerson = carViewModel.manufacturer.contactPerson;
            carViewModel.CarTransmissionTypeName = carViewModel.carTransmissionType.name;
            carViewModel.type = carViewModel.CarType.type;
        }
        //public CarReferenceViewModel ApplySelectListItem(CarReferenceViewModel carViewModel)  
        //{
        //    if (carViewModel != null)
        //    {
        //        carViewModel.manufacturerSelectList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        //        carViewModel.carTypeSelectList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        //        carViewModel.cartransmisionTypesSelectList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        //        foreach(var manuf in carViewModel.manufacturer)
        //        {
        //            carViewModel.manufacturerSelectList.Add(new SelectListItem
        //            {
        //                Value = manuf.id.ToString(),
        //                Text = manuf.name
        //            });

        //        }
        //        foreach (var carTypeSe in carViewModel.carType)
        //        {
        //            carViewModel.carTypeSelectList.Add(new SelectListItem
        //            {
        //                Value = carTypeSe.id.ToString(),
        //                Text = carTypeSe.type
        //            });

        //        }
        //        foreach (var carT in carViewModel.cartransmisionTypes)
        //        {
        //            carViewModel.cartransmisionTypesSelectList.Add(new SelectListItem
        //            {
        //                Value = carT.id.ToString(),
        //                Text = carT.name
        //            });

        //        }

        //    }
        //    return carViewModel;
        //}
    }
}
