#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Area2Dto  class
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
    public class Area2Dto : DtoBase
    {
        public int ID { get; set; }

        public string AreaCode { get; set; }

        public string Description { get; set; }

        public string ACFinishUnit { get; set; }

        public string ACSparePart { get; set; }

        public string ACService { get; set; }

        public Area1Dto Area1 { get; set; }
    }
}
