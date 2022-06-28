#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : JobPositionDto  class
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
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class JobPositionDto : DtoBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int Category { get; set; }

        public int SalesTarget { get; set; }

        public ArrayList UserInfos { get; set; }

        public ArrayList SalesmanHeaders { get; set; }

        ArrayList JobPositionToMenu { get; set; }
    }
}
