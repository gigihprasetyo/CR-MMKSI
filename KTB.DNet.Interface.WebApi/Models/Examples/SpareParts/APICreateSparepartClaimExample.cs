using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateSparepartClaimExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ClaimDate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                DealerCode = "100170",
                ClaimReasonid = 1,
                StatusID = 1,
                SONumber = "1625090480",
                NoFaktur = "1627089662",
                Status =1,
                StatusKTB =1,
                ClaimProgressId=1,
                Description = "Testing",
                ClaimDetails = new List<object>{
                    new {
                         NoBarang= "Broken 1",
                         QtyClaim= 4,
                         Keterangan="Salah Harga",
                         UpdatedBy= "DealerUser",
                         StatusDetail =1,
                         StatusDetailKTB =1,
                         ClaimGoodConditionId=1,
                        }

                },
                DocumentUpload = new List<object>{
                    new {
                        UpdatedBy= "DealerUser",
                        Type= 3,
                        FileName= "Faktur.jpg",
                        Path= "2020\\VehicleClaim\\AttachmentClaim\\100170\\2020091792602f742c3c6-bdcb-42d9-a996-a94bee08b2c0..jpg"
                        }
                }
            };
            return obj;
        }
    }
}