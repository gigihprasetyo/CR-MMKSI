#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIChassisMasterClaim class
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

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateChassisMasterClaimExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var objek = new
            {
                UpdatedBy = "DealerUser",
                ID = 240,
                ClaimDate = "2020-09-15",
                DealerCode = "100170",
                ReporterIssue = "Dealer",
                DealerPIC = "User",
                StatusID = 1,
                DateOccur = "2020-09-15",
                PlaceOccur = "FB69CB18-10AC-49F5-",
                ChassisNumber = "MHMFE73P29K010785",
                StatusStockDMS = 1,
                ChassisMasterClaimDetails = new List<object>{
                    new {
                        ID = 1,
                        ClaimPoint = "Broken 1",
                        ClaimType = 4,
                        UpdatedBy = "DealerUser"
                    },
                    new {
                        ID = 2,
                        ClaimPoint = "Broken 1",
                        ClaimType = 4,
                        UpdatedBy = "DealerUser"
                    }
                },
                DocumentUpload = new List<object>{
                    new {
                        ID = 1,
                        UpdatedBy = "DealerUser",
                        Type = 1,
                        FileName = "Mirage.jpg",
                        FileDescription = "oke",
                        Path = "2020\\VehicleClaim\\AttachmentClaim\\100170\\2020091792602f742c3c6-bdcb-42d9-a996-a94bee08b2c0..jpg"
                    },
                    new {
                        ID =2,
                        UpdatedBy = "DealerUser",
                        Type = 1,
                        FileName = "Mirage.jpg",
                        FileDescription = "oketest",
                        Path = "2020\\VehicleClaim\\AttachmentClaim\\100170\\2020091792602f742c3c6-bdcb-42d9-a996-a94bee08b2c0..jpg"
                    }
                }
            };

            return objek;
        }
    }
}