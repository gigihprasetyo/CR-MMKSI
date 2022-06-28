#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SalesmanAreaDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System.Collections;

namespace KTB.DNet.Interface.Model
{
    public class SalesmanAreaDto : DtoBase
    {
        public int ID { get; set; }

        public string AreaCode { get; set; }

        public string AreaDesc { get; set; }

        public string City { get; set; }

        public ArrayList SalesmanHeaders { get; set; }
    }
}
