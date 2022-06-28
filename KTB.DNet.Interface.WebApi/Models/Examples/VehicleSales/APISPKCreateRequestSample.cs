#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APISPKCreateRequestSample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APISPKCreateRequestSample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new
            {
                ID = 0,
                DealerCode = "100109",
                DealerSPKNumber = "YUKI OH",
                SPKReferenceNumber = "",
                SalesmanCode = "S-100005",
                DealerSPKDate = "2018-02-15",
                Status = 11,
                RejectedReason = "",
                EventType = 0,
                CampaignName = "P032100174",
                EvidenceFile = new
                {
                    FileName = "contoh.jpg",
                    Base64OfStream = "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"
                },
                OCRFamilyIdentity = new
                {
                    IdentityFile = new
                    {
                        FileName = "",
                        Base64OfStream = "",
                        ImagePath = "",
                        ImageID = ""
                    },
                    FamilyMembers = new List<object>() {
                    new {
                        ID = 0,
                        FCRowNo = 1,
                        Name = "string",
                        IdentityNumber = "string",
                        Gender = "string",
                        PlaceOfBirth = "string",
                        DateOfBirth = "2000-02-15",
                        Religion = "string",
                        Education = "string",
                        Occupation = "string",
                        BloodType = "string"
                        }
                    }
                },
                SPKCustomer = new
                {
                    ID = 0,
                    CustomerName = "Miyuki Hasanuddin",
                    CustomerName2 = "Trump",
                    BusinessSectorDetailID = 1,
                    Email = "mail@mail.com",
                    HomeNo = "08378473834",
                    OfficeNo = "08378473834",
                    CityCode = "ACABD",
                    Kelurahan = "kelurahan",
                    Kecamatan = "kecamatan",
                    Alamat = "alamat",
                    Gedung = "Gedung",
                    PostalCode = "34532",
                    CountryCode = "62",
                    HpNo = "8378473834",
                    PhoneNo = "08378473834",
                    TipeCustomer = 1,
                    TipePerusahaan = 1,
                    TypePerorangan = 0,
                    TypeIdentitas = 0,
                    ReffCode = "",
                    PreArea = "KAB",
                    LKPPReference = "",
                    SAPCustomerID = 374197,
                    EMAIL = "mail@mail.com",
                    JK = "",
                    KODEPOS = "51712",
                    NOKTP = "0239420934923049",
                    NOTELP = "084375638573",
                    PENDIDIKAN = "",
                    BRANCH_MGR = "",
                    PART_MGR = "",
                    POBOX = "11111",
                    SERVICE_MGR = "",
                    SALES_MGR = "",
                    //TGLLAHIR = "1990-01-30",
                    ImagePath = "2020\\100170_2020090411003299xf.jpg",
                    OCRIdentity = new
                    {
                        IdentityType = 0,
                        ImageID = "string",
                        ImagePath = "string",
                        IdentityNumber = "string",
                        Name = "string",
                        BirthOfDate = "2000-02-15",
                        BirthOfPlace = "string",
                        Gender = "string",
                        Height = "string",
                        Address = "string",
                        RtRw = "string",
                        District = "string",
                        Subdistrict = "string",
                        Regency = "string",
                        Province = "string",
                        Religion = "string",
                        MaritalStatus = "string",
                        Occupation = "string",
                        Citizenship = "string",
                        ValidUntil = "2038-02-15",
                        Polda = "string",
                        JSon = "string",
                        IdentityFile = new
                        {
                            FileName = "",
                            Base64OfStream = ""
                        }
                    }
                },
                SPKDetails = new List<object>() {
                new {
                   ID = 0,
                   CategoryCode = "PC",
                   LineItem = 2,
                   VehicleTypeCode = "WQ01",
                   VehicleColorCode = "MCRM",
                   Additional = 2,
                   Remarks = "",
                   Quantity = 1,
                   Amount = 1,
                   Status = 0,
                   SPKHeaderID = 0,
                   RejectedReason = "",
                   CBU_BODYTYPE1 = "",
                   CBU_BODYTYPELCV1 = "",
                   CBU_LOADPROFILE1 = "",
                   CBU_MEDANOPERASI1 = "",
                   CBU_OWNERSHIP1 = "P",
                   CBU_PURCSTAT = "1A",
                   CBU_PURPOSE1 = "HO",
                   CBU_USERAGE1 = "P5~",
                   CBU_WAYPAID1 = "K",
                   CBU_JENISKEND = "",
                   CBU_MODELKEND = "",
                   CBU_PURCSTAT2 = "1A",
                   CBU_PURPOSE2 = "",
                   CBU_LEASING = "L00001",
                   CBU_CARROSSERIE = "K00001",
                   EventType = 0,
                   CampaignName = "P032100174",
                   SPKDetailCustomer = new
                   {
                       ID = 0,
                        CustomerName = "Miyuki Hasanuddin",
                        CustomerName2 = "Trump",
                        BusinessSectorDetailID = 1,
                        Email = "mail@mail.com",
                        HomeNo = "08378473834",
                        OfficeNo = "08378473834",
                        CityCode = "ACABD",
                        Kelurahan = "kelurahan",
                        Kecamatan = "kecamatan",
                        Alamat = "alamat",
                        Gedung = "Gedung",
                        PostalCode = "34532",
                        CountryCode = "62",
                        HpNo = "8378473834",
                        PhoneNo = "08378473834",
                        TipeCustomer = 1,
                        TipePerusahaan = 1,
                        TypePerorangan = 0,
                        TypeIdentitas = 0,
                        ReffCode = "",
                        PreArea = "KAB",
                        LKPPReference = "",
                        EMAIL = "mail@mail.com",
                        JK = "",
                        KODEPOS = "51712",
                        NOKTP = "0239420934923049",
                        NOTELP = "084375638573",
                        PENDIDIKAN = "",
                        BRANCH_MGR = "",
                        PART_MGR = "",
                        POBOX = "11111",
                        SERVICE_MGR = "",
                        SALES_MGR = "",
                        PrintRegion = 0,
                        TGLLAHIR = "1990-01-30",
                        ImagePath = "2020\\100170_2020090411003299xf.jpg",
                        DMSSPKDetailNo = "SPKD001",
                        LastUpdateCustomer = "2018-02-15 16:02:00"
                   }
                }

               },
                UpdatedBy = "test"
            };
        }
    }
}