#region Summary
// ===========================================================================
// AUTHOR        : Mitrais Team
// PURPOSE       : VWI_OpenFakturForPDI Dto class.
// SPECIAL NOTES : 
// ---------------------
// Copyright  2019 
// ---------------------
// $History      : $
// Generated on 09/01/2019 - 7:34:49
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class VWI_ChassisStatusFakturDto : ReadDtoBase
    {
        
        public string ChassisNumber { get; set; }
        
        public DateTime ConfirmDate { get; set; }
        
        public string DealerCode { get; set; }
        
        public string DealerName { get; set; }
        
        public DateTime DownloadDate { get; set; }
        
        public DateTime FakturDate { get; set; }
        
        public string FakturNumber { get; set; }
        
        public string FakturStatus { get; set; }
        
        public int ID { get; set; }
        
        public DateTime OpenFakturDate { get; set; }
        
        public DateTime PrintedDate { get; set; }
        
        public DateTime RevisionDate { get; set; }
        
        public string RevisionStatus { get; set; }
        
        public string RevisionType { get; set; }
        
        public string SPKNumber { get; set; }
        public string DealerSPKNumber { get; set; }

        public DateTime ValidateDate { get; set; }

        public DateTime ETDDate { get; set; }

        public DateTime ETADate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ATDDate { get; set; }

        public DateTime ATADate { get; set; }
    }
}

