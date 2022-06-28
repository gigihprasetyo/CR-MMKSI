#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LaborMasterDto  class
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
    public class LaborMasterDto : DtoBase
    {
        #region Public Properties

        public int ID { get; set; }

        public string LaborCode { get; set; }

        public string WorkCode { get; set; }

        public VehicleTypeDto VehicleType { get; set; }

        #endregion

        #region Custom Properties

        #endregion
    }
}
