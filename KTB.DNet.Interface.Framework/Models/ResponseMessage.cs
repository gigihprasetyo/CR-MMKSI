#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ResponseMessage  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan - Initial Checkin
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.Framework
{
    public class ResponseMessage
    {
        public bool Success { get; set; }
        public ResponseStatus Status { get; set; }
        public string StatusText { get { return Status.ToString(); } }
        public string Message { get; set; }
        public object Data { get; set; }
        public object ModelState { get; set; }
    }

    public class ResponseMessage<T>
    {
        public bool Success { get; set; }
        public ResponseStatus Status { get; set; }
        public string StatusText { get { return Status.ToString(); } }
        public string Message { get; set; }
        public T Data { get; set; }
        public object ModelState { get; set; }
    }

    public enum ResponseStatus
    {
        Success,
        Confirm,
        Warning,
        Error
    }
}
