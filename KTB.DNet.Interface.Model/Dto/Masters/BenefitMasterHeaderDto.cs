#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterHeaderDto  class
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
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class BenefitMasterHeaderDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public string NomorSurat { get; set; }

        public short Status { get; set; }

        public string BenefitRegNo { get; set; }

        public string Remarks { get; set; }

        public string Formula { get; set; }

        #endregion

        #region Custom Properties
        private List<BenefitMasterDealerDto> _benefitMasterDealerList = new List<BenefitMasterDealerDto>();

        public List<BenefitMasterDealerDto> BenefitMasterDealerList
        {
            get
            {
                return _benefitMasterDealerList;
            }
            set
            {
                _benefitMasterDealerList = value;
            }
        }

        private List<BenefitMasterDetailDto> _benefitMasterDetailList = new List<BenefitMasterDetailDto>();

        public List<BenefitMasterDetailDto> BenefitMasterDetailList
        {
            get
            {
                return _benefitMasterDetailList;
            }
            set
            {
                _benefitMasterDetailList = value;
            }
        }

        private List<BenefitEventHeaderDto> _benefitEventHeaderList = new List<BenefitEventHeaderDto>();

        public List<BenefitEventHeaderDto> BenefitEventHeaderList
        {
            get
            {
                return _benefitEventHeaderList;
            }
            set
            {
                _benefitEventHeaderList = value;
            }
        }

        #endregion
    }
}
