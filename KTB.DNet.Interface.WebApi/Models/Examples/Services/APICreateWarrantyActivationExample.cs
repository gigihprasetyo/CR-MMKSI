﻿using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateWarrantyActivationExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ChassisNumber = "MK2L0PU39HK013747",
                RequestDate = "2020-12-04",
                CustomerName = "Dian Sastro",
                HandphoneNo = "08158747383",
                PlateNumber = "B 4 RUU",
                DigitalSignature = new {
                    FileName = "test.png",
                    Base64OfStream = "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"
                } 
            };

            return obj;
        }
    }
}