#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Utils  class
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
using System;
using System.Globalization;
using System.Text.RegularExpressions;
#endregion

namespace KTB.DNet.Interface.Model
{
    public static class Utils
    {
        /// <summary>
        /// CHeck if the date format is valid
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool IsValidDateByFormat(string date, string format, out DateTime dt)
        {
            string[] formats = { format };
            if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Get date in string format
        /// </summary>
        /// <param name="dateInput"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static string GetStrDate(DateTime dateInput, string dateFormat)
        {
            if (dateInput == DateTime.MinValue)
            {
                return "";
            }
            else
            {
                return String.Format(dateFormat, dateInput);
            }
        }

        /// <summary>
        /// String field validation
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsDefaultString(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue)) return true;

            return stringValue.Equals("string", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Birthday validation
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public static bool IsBirthDateValid(string birthDate)
        {
            if (string.IsNullOrEmpty(birthDate))
                return true;

            DateTime _birthDate;
            if (DateTime.TryParse(birthDate, out _birthDate))
            {
                return IsBirthDateValid(_birthDate);
            }

            return false;
        }

        /// <summary>
        /// Birthday validation
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public static bool IsBirthDateValid(DateTime birthDate, bool isEmployeeService = false)
        {
            // year age
            int age = DateTime.Now.Year - birthDate.Year;

            // validate month and day
            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
                age--;

            // validate minimum age
            bool result = isEmployeeService ? age >= 17 : age >= 15;

            return result;
        }

        /// <summary>
        /// Phone validation
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsNoHPValid(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                return Regex.IsMatch(phone, "^08[0-9]{5,}$");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Phone validation
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsNoHPValidSPK(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                return Regex.IsMatch(phone, "^8[0-9]{5,}$");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Month input validation
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool IsMonthValid(int month)
        {
            return ((month >= 1) & (month <= 12)) ? true : false;
        }

        /// <summary>
        /// Validate no telp based on DNET validation
        /// </summary>
        /// <param name="notelp"></param>
        /// <returns></returns>
        public static bool IsNOTelpValid(string notelp)
        {
            if (!string.IsNullOrEmpty(notelp))
            {
                if (notelp.Length < 7 || notelp.Substring(0, 2) == "00")
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Phone validation
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsOfficeNoValid(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                return Regex.IsMatch(phone, "^0[0-9-]{6,}$");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Email validation
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmailValid(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return Regex.IsMatch(email, @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            }
            else
            {
                return true;
            }
        }

        public static bool IsNumeric(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                return Regex.IsMatch(character, "^[0-9]*$");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Alphanumeric validation
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(string character)
        {
            return IsAlphaNumericIncludeRegex(character, null);
        }

        /// <summary>
        /// Alphanumeric for name
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsAlphanumericForName(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                return Regex.IsMatch(character, @"^[a-zA-Z0-9'.,\(\) ]+$");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Alpanumeric validation include space
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsAlphanumericIncludeSpace(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                return Regex.IsMatch(character, "^[a-zA-Z0-9 ]+$");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Alphanumeric validation include special character
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsAlphanumericIncludeSpecialCharacter(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                return Regex.IsMatch(character, @"^[A-Za-z0-9'\.\-\s\,]+$");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validate Job Position
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsValidJobPosition(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                return Regex.IsMatch(character, @"^[A-Za-z_ ]+$");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validate alpha numeric
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsAlphaNumericPlusUniv(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                if (IsAlphaNumeric(character))
                {
                    return Regex.IsMatch(character, "^([0-9]|[A-z]|\x2D|\x8)+$");
                }
            }

            return false;
        }

        /// <summary>
        /// Is Alphanumeric except any regex
        /// </summary>
        /// <param name="str"></param>
        /// <param name="exceptionRegex"></param>
        /// <returns></returns>
        public static bool IsAlphaNumericIncludeRegex(string str, string includedRegex)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return Regex.IsMatch(str, string.Format("^[a-zA-Z0-9{0}]*$", includedRegex ?? string.Empty));
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Exclude regex 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="excludedRegex"></param>
        /// <returns></returns>
        public static bool ExcludedRegexIsNotExist(string str, string excludedRegex)
        {
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(excludedRegex))
            {
                return Regex.IsMatch(str, string.Format("^[^{0}]*$", excludedRegex ?? string.Empty));
            }
            else
            {
                return true;
            }
        }
    }
}
