#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerCaseDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion


namespace KTB.DNet.Interface.Model
{
    public class CustomerCaseDto : DtoBase
    {
        public int ID { get; set; }

        public int DealerID { get; set; }

        public string CaseNumber { get; set; }

        public CustomerCaseResponseDto Response { get; set; }

        public CustomerCaseResponseEvidenceDto ResponseEvidence { get; set; }
    }
}
