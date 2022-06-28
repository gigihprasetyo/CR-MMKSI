#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateCustomerVehicleExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Swashbuckle.Examples;
using System;
using System.Globalization;
#endregion

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateCustomerVehicleExample : IExamplesProvider
    {
        public object GetExamples()
        {
            DateTime tglProcess = DateTime.ParseExact("19750101", "yyyyMMdd", CultureInfo.InvariantCulture);
            var obj = new
            {
                //ID = 1,
                UpdatedBy = "DealerUser",
                //RequestType = 0,
                //CustomerCode = "",
                //RequestNo = "",
                //RefRequestNo = "",
                DealerCode = "100109",
                //Status = 0,
                //ReffCode = "30321203",
                //Name1 = "MADE SHINTA 2",
                //Alamat = "JL. PULOMAS 100",
                //Kelurahan = "GALUR",
                //Kecamatan = "JOHAR BARU",
                //PostalCode = "93002",
                //PreArea = "KOTA",
                //CityCode = "JBKNG",
                //PhoneNo = "081234567891",
                //Email = "madeayu@test.com",
                //Status1 = 0,
                //TipePerusahaan = 0,
                //JenisKelamin = "LK",
                //KTP = "0100000100000000",
                //Pendidikan = "S2",
                //KategoriGroup = 1,
                //ProcessDate = tglProcess.ToString("yyyy-MM-dd"),
                GUID = "2109000002",
                GUIDUpdate = "2109000001",
                CustomerNo ="xxxxx",
                CustomerType ="",
                ClassType ="",
                LevelData="",
                CustomerClass="",
                InterfaceStatus=1,
                InterfaceMessage= "xxxx",
                CustomerSubClass=0,
                ParentCustomerNo = "xxxx",
                FirstName= "xxxx",
                LastName= "xxxx",
                CountryCode="62",
                PhoneNo ="xxxx",
                OtherPhoneNo="",
                Email = "xxxx",
                Gedung = "xxxx",
                Alamat = "xxxx",
                Kelurahan = "xxxx",
                Kecamatan = "xxxx",
                PostalCode = "xxxx",
                CityCode = "xxxx",
                POBox = "xxxx",
                Birthdate="26/07/1997",
                IdentificationType=0,
                IdentificationNo = "xxxx",
                NPWPNo = "xxxx",
                NPWPName = "xxxx",
                PreArea = "xxxx",
                PrintRegion=0,
                InterfaceCustSales=0,
                Notes = "xxxx"
            };

            return obj;
        }
    }
}