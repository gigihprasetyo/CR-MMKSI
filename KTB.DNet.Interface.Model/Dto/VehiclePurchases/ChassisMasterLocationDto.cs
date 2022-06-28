#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterLocationDto  class
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
    public class ChassisMasterLocationDto : DtoBase
    {
        public int ID { get; set; }

        public ChassisMasterDto ChassisMaster { get; set; }

        public string Location { get; set; }

        //public PODestinationDto PODestination { get; set; }
    }
}
