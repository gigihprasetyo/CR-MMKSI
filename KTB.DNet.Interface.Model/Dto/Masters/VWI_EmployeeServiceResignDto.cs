#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeServiceResignDto  class
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
    public class VWI_EmployeeServiceResignDto : ReadDtoBase
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string NoKTP { get; set; }

    }
}
