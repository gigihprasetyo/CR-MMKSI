#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : TrTraineeDto  class
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
    public class TrTraineeDto : DtoBase
    {
        public int ID { get; set; }

        public int SalesmanHeaderID { get; set; }

        public string SalesmanCode { get; set; }

        public string Name { get; set; }

        public int DealerID { get; set; }

        public string DealerCode { get; set; }

        public int DealerBranchID { get; set; }

        public string DealerBranchCode { get; set; }

        public DateTime BirthDate { get; set; }

        public short Gender { get; set; }

        public string NoKTP { get; set; }

        public string Email { get; set; }

        public DateTime StartWorkingDate { get; set; }

        public string Status { get; set; }

        public string JobPosition { get; set; }

        public string EducationLevel { get; set; }

        public byte[] Photo { get; set; }

        public string ShirtSize { get; set; }
    }
}

