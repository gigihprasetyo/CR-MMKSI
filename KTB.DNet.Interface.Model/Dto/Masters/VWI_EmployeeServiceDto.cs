#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeServiceDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class VWI_EmployeeServiceDto : ReadDtoBase
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string DealerCode { get; set; }

        public string DealerBranchCode { get; set; }

        public DateTime BirthDate { get; set; }

        public short Gender { get; set; }

        public string NoKTP { get; set; }

        public string Email { get; set; }

        public DateTime StartWorkingDate { get; set; }

        public string JobPosition { get; set; }

        public string EducationLevel { get; set; }

        public byte[] Photo { get; set; }

        public string ShirtSize { get; set; }

        public int Status { get; set; }

        public string LastUpdateBy { get; set; }
    }
}
