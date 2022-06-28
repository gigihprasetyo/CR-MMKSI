#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MessageBase  class
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
    public class MessageBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageBase() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public MessageBase(ErrorCode code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }

        /// <summary>
        /// Message for the error
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Code for the error
        /// </summary>
        public ErrorCode ErrorCode { get; set; }
    }
}
