#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ErrorCode  enumeration
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 7/11/2018 13:48
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.Model
{
    public enum ErrorCode
    {
        Er_OK = 200,
        Er_10001 = 401,
        Er_10002 = 401,
        Er_10003 = 403,
        Er_20004 = 404,
        Er_20005 = 400,
        Er_20006 = 404,
        Er_20007 = 400,
        Er_20008 = 400,
        Er_20009 = 400,
        Er_200010 = 400,
        Er_200011 = 400,
        Er_200012 = 400,
        Er_300013 = 400,
        Er_300014 = 500,
        Er_400015 = 400,
        AuthInvalidUserNamePassword = 10001,
        AuthSessionExpired = 10002,
        AuthNoPrivilege = 10003,
        DataReadNotAvailable = 20004,
        DataJsonInvalidFormat = 20005,
        DataUpdateNotAvailable = 20006,
        DataDeleteNotAvailable = 20007,
        DataRequiredField = 20008,
        DataReferenceNotMatch = 20009,
        DataOptionNotMatch = 200010,
        DataTypeOrDataFormatInvalid = 200011,
        DataContentCorrupt = 200012,
        DBRetrieveFailed = 300013,
        DBSaveFailed = 300014,
        UnhandledException = 400015
    }
}
