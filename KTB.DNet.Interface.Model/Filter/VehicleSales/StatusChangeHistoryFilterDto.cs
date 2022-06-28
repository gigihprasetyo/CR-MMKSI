#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StatusChangeHistoryFilterDto  class
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class StatusChangeHistoryFilterDto : FilterDtoBase
    {
        private int ID { get; set; }
        private int DocumentType { get; set; }
        private string DocumentRegNumber { get; set; }
        private int OldStatus { get; set; }
        private int NewStatus { get; set; }
    }
}
