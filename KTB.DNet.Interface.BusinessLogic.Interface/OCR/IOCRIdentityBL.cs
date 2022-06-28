#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : OCRIdentity interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using System.Web;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IOCRIdentityBL
    {
        /// <summary>
        /// Upload KTP OCR file 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        ResponseBase<OCRIdentityKTPDto> UploadKTP(HttpPostedFile postedFile);

        /// <summary>
        /// Check KTP OCR Status based on its upload ID 
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        ResponseBase<OCRResultValueDto> ProgressKTP(string uploadID);

        /// <summary>
        /// Check KTP OCR Result Status based on its upload ID 
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        ResponseBase<OCRKTPDataDto> DataKTP(string uploadID);

        /// <summary>
        /// Upload SIM OCR file 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        ResponseBase<OCRIdentitySIMDto> UploadSIM(HttpPostedFile postedFile);

        /// <summary>
        /// Check SIM OCR Status based on its upload ID 
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        ResponseBase<OCRResultValueDto> ProgressSIM(string uploadID);

        /// <summary>
        /// Check SIM OCR Result Status based on its upload ID 
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        ResponseBase<OCRSIMDataDto> DataSIM(string uploadID);

        /// <summary>
        /// Setup the credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dealerCode"></param>
        void Initialize(string userName, string dealerCode);
    }
}
