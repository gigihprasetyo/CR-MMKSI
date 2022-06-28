#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StatusChangeHistoryParameterDto  class
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
    public class StatusChangeHistoryParameterDto : ParameterDtoBase
    {
        private int ID { get; set; }
        private int DocumentType { get; set; }
        private string DocumentRegNumber { get; set; }
        private int OldStatus { get; set; }
        private int NewStatus { get; set; }
    }
}
