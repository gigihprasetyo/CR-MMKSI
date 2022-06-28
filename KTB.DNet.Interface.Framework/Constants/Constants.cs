#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Constants  class
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

#endregion
using System;
using System.Data.SqlTypes;

namespace KTB.DNet.Interface.Framework
{
    public partial class Constants
    {
        public const int NUMBER_DEFAULT_VALUE = 0;
        public const string STRING_DEFAULT_VALUE = null;
        public static readonly DateTime DATETIME_DEFAULT_VALUE = SqlDateTime.MinValue.Value;
        public const bool BOOL_DEFAULT_VALUE = false;
        public const string DATE_DEFAULT_FORMAT = "yyyy-MM-dd";
        public const string JSON_DATE_DEFAULT_FORMAT = "yyyy-MM-ddTHH:mm:ss";
        public const string TEMP_OCR_FILENAME = "OCR-{0}-{1}";
        public static readonly string[] ALLOWED_FILE_EXT = new string[6] { ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf" };
        public static readonly string[] ALLOWED_FILE_EXT_FS = new string[10] { ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".png", ".jpg", ".png", ".zip", ".jpeg" };

        public static class SPKConfig
        {
            public const string TEMP_FILENAME = "TempSPK-{0}-{1}";
        }

        public static class DBConfig
        {
            public const int DEFAULT_TIMEOUT = 10000;
        }

        public static class CustomerCase
        {
            public const string TEMP_CUSTOMER_CASE_FILENAME = "TempCustomerCase-{0}-{1}";
        }

        public static class EmployeeService
        {
            public const string TEMP_MECHANIC_FILENAME = "TempMechanic-{0}-{1}";
        }

    }
}
