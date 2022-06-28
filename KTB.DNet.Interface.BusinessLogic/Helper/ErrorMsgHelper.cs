#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ErrorMsgHelper class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public static class ErrorMsgHelper
    {
        public static void SqlException(List<MessageBase> errorMessages, string errorMessage)
        {
            errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.DBSaveFailed, ErrorMessage = String.Format(MessageResource.ErrorMsgDBSave, errorMessage) });
        }

        public static void SqlExceptionRead(List<MessageBase> errorMessages, string errorMessage)
        {
            errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.DBRetrieveFailed, ErrorMessage = String.Format(MessageResource.ErrorMsgDBRetrive, errorMessage) });
        }

        public static void Exception(List<MessageBase> errorMessages, string errorMessage)
        {
            errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = String.Format(MessageResource.ErrorMsgPRGUnhandle, errorMessage) });
        }

        public static void DeleteNotAvailable(List<MessageBase> errorMessages)
        {
            errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.DataDeleteNotAvailable, ErrorMessage = MessageResource.ErrorMsgDataDeleteNotAvailable });
        }

        public static void UpdateNotAvailable(List<MessageBase> errorMessages)
        {
            errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = MessageResource.ErrorMsgDataUpdateNotAvailable });
        }

        public static void DataCorrupt(List<MessageBase> errorMessages)
        {
            errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.DataContentCorrupt, ErrorMessage = MessageResource.ErrorMsgDataCorrupt });
        }

        public static void ErrorMsgDBSave(List<MessageBase> errorMessages)
        {
            ErrorMsgDBSave(errorMessages, MessageResource.ErrorMsgDBSave);
        }

        public static void ErrorMsgDBSave(List<MessageBase> errorMessages, string fieldName)
        {
            errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.DBSaveFailed, ErrorMessage = string.Format(MessageResource.ErrorMsgDBSave, fieldName) });
        }

        public static void ErrorMsgDBSaveContactAdmin(List<MessageBase> errorMessages)
        {
            errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = string.Format(MessageResource.ErrorMsgPRGUnhandle, MessageResource.ErrorMsgOnSavingPleaseContactAdmin) });
        }

        public static void DataNotFound(List<MessageBase> errorMessages, Type type, FilterDtoBase filterDto, string colName = null, string colValue = null)
        {
            string typeName = type.Name.Replace("VWI_", "");
            if (!string.IsNullOrEmpty(colName) && !string.IsNullOrEmpty(colValue))
                errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.DataReadNotAvailable, ErrorMessage = String.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, typeName, Helper.GetCriteriasMessageFormat(type, null, colName, colValue)) });
            else
                errorMessages.Add(new MessageBase { ErrorCode = ErrorCode.DataReadNotAvailable, ErrorMessage = String.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, typeName, Helper.GetCriteriasMessageFormat(type, filterDto)) });
        }
    }
}
