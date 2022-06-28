#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DealerGroupDto  class
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
    public class DealerGroupDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public string DealerGroupCode { get; set; }

        public string GroupName { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }
}
