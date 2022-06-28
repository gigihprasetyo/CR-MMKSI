#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPLDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Model.CustomAttribute;
using System;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SPLDto : DtoBase
    {
        public int ID { get; set; }

        public string SPLNumber { get; set; }

        public string DealerName { get; set; }

        public string CustomerName { get; set; }

        public string Description { get; set; }

        [DateTimeDisplayFormatAttribute]
        public DateTime ValidFrom { get; set; }

        [DateTimeDisplayFormatAttribute]
        public DateTime ValidTo { get; set; }

        public string Attachtment { get; set; }

        public int NumOfInstallment { get; set; }

        public int MaxTOPDay { get; set; }

        public int Status { get; set; }

        public ArrayList SPLDetails { get; set; }
    }
}
