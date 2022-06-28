using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateChassisMasterClaimExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ClaimDate = "2020-09-15",
                DealerCode = "100170",
                ReporterIssue = "Dealer",
                PODestinationCode = "600108",
                DealerPIC = "User",
                StatusID = 1,
                DateOccur = "2020-09-15",
                PlaceOccur = "FB69CB18-10AC-49F5-",
                ChassisNumber = "MHMFE73P29K010785",
                StatusStockDMS = 1,
                ChassisMasterClaimDetails = new List<object>{ 
                    new {
                        ClaimPoint = "Broken 1",
                        ClaimType = 4,
                        UpdatedBy = "DealerUser"
                    },
                    new {
                        ClaimPoint = "Broken 1",
                        ClaimType = 4,
                        UpdatedBy = "DealerUser"
                    }
                },
                DocumentUpload = new List<object>{
                    new {
                        UpdatedBy = "DealerUser",
                        Type = 1,
                        FileName = "Mirage.jpg",
                        FileDescription = "oke",
                        Path = "2020\\VehicleClaim\\AttachmentClaim\\100170\\2020091792602f742c3c6-bdcb-42d9-a996-a94bee08b2c0..jpg"
                    },
                    new {
                        UpdatedBy = "DealerUser",
                        Type = 1,
                        FileName = "Mirage.jpg",
                        FileDescription = "oketest",
                        Path = "2020\\VehicleClaim\\AttachmentClaim\\100170\\2020091792602f742c3c6-bdcb-42d9-a996-a94bee08b2c0..jpg"
                    }
                }
            };
            return obj;
        }
    }
}