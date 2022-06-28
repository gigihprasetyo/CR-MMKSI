#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FileUtility class
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
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public static class FileUtility
    {
        #region Public Methods
        /// <summary>
        /// Validate identity file
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static List<DNetValidationResult> ValidateEvidenceOrIdentityFile(AttachmentParameterDto attachment, AutoMapper.IMapper mapper, out byte[] bytes, string fileType)
        {
            bytes = null;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();

            // return error if the attachment is null
            if (attachment == null || string.IsNullOrEmpty(attachment.FileName) || string.IsNullOrEmpty(attachment.Base64OfStream))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, fileType)));
                return validationResults;
            }

            // get the file extension
            string ext = Path.GetExtension(attachment.FileName);

            // check the file type is allowed 
            if (!Constants.ALLOWED_FILE_EXT.Contains(ext, StringComparer.OrdinalIgnoreCase))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.InvalidFileType, fileType, "(" + string.Join(",", Constants.ALLOWED_FILE_EXT) + ")")));
            }
            else
            {
                try
                {
                    // get maximum file size
                    int fileSize = GetMaximumFileSize(mapper);

                    // convert from base64 string of stream to stream
                    bytes = Convert.FromBase64String(attachment.Base64OfStream);

                    // check if the file exceed the maximum file size
                    if (bytes.Length > fileSize)
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.MaximumFileSize, fileType, fileSize)));
                    }
                }
                catch
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgParseFailed, fileType)));
                }
            }

            return validationResults;
        }

        /// <summary>
        /// Validate identity file
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static List<DNetValidationResult> ValidateEvidenceOrIdentityFileFS(AttachmentParameterDto attachment, AutoMapper.IMapper mapper, out byte[] bytes, string fileType)
        {
            bytes = null;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();

            // return error if the attachment is null
            if (attachment == null || string.IsNullOrEmpty(attachment.FileName) || string.IsNullOrEmpty(attachment.Base64OfStream))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, fileType)));
                return validationResults;
            }

            // get the file extension
            string ext = Path.GetExtension(attachment.FileName);

            // check the file type is allowed 
            if (!Constants.ALLOWED_FILE_EXT_FS.Contains(ext, StringComparer.OrdinalIgnoreCase))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.InvalidFileType, fileType, "(" + string.Join(",", Constants.ALLOWED_FILE_EXT_FS) + ")")));
            }
            else
            {
                try
                {
                    // get maximum file size
                    int fileSize = GetMaximumFileSize(mapper);

                    // convert from base64 string of stream to stream
                    bytes = Convert.FromBase64String(attachment.Base64OfStream);

                    // check if the file exceed the maximum file size
                    if (bytes.Length > fileSize)
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.MaximumFileSize, fileType, fileSize)));
                    }
                }
                catch
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgParseFailed, fileType)));
                }
            }

            return validationResults;
        }

        /// <summary>
        /// Save Customer Request File
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="requestNo"></param>
        /// <param name="bytes"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SaveCustomerRequestFile(AttachmentParameterDto attachment, string requestNo, byte[] bytes, out string filePath)
        {
            // set temporary file name
            string filename = "CSR" + requestNo + attachment.FileName;

            // get destination folder from web config
            string destFolder = Path.Combine(AppConfigs.GetString("SAN"), Path.Combine(AppConfigs.GetString("CustomerRequestDir"), DateTime.Now.Year.ToString()));

            // set the file path
            filePath = Path.Combine(destFolder, filename);

            // call the save process
            return SaveFile(bytes, destFolder, filePath);
        }

        /// <summary>
        /// Save evidence file
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="bytes"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SaveSPKEvidenceFile(AttachmentParameterDto attachment, byte[] bytes, out string filePath)
        {
            // set temporary file name
            string filename = string.Format(Constants.SPKConfig.TEMP_FILENAME, DateTime.Now.ToString("yyyyMMddHHmmssffff"), attachment.FileName);

            // get destination folder from web config
            string destFolder = Path.Combine(AppConfigs.GetString("SAN"), Path.Combine(AppConfigs.GetString("SPKFileDirectory"), DateTime.Now.Year.ToString()));

            // set the file path
            filePath = Path.Combine(destFolder, filename);

            // call the save process
            return SaveFile(bytes, destFolder, filePath);
        }

        /// <summary>
        /// Save evidence file FS
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="bytes"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SaveFSEvidenceFile(string newfilename, byte[] bytes, out string filePath, bool isUploaded)
        {
            // get destination folder from web config
            string destFolder = Path.Combine(AppConfigs.GetString("SAN"), AppConfigs.GetString("FSEvidenceDirectory"));
            destFolder = destFolder+DateTime.Now.Year+@"\"+ DateTime.Now.Month.ToString("d2");

            // set the file path
            filePath = Path.Combine(destFolder, newfilename);

            if(isUploaded)
            {
                // call the save process
                return SaveFile(bytes, destFolder, filePath);
            }else
            {
                return DeleteFileFS(bytes, destFolder, filePath);
            }
        }

        /// <summary>
        /// Save evidence file
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="bytes"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SaveEvidenceFile(AttachmentParameterDto attachment, byte[] bytes, out string filePath)
        {
            // set temporary file name
            string filename = string.Format(Constants.CustomerCase.TEMP_CUSTOMER_CASE_FILENAME, DateTime.Now.ToString("yyyyMMddHHmmssffff"), attachment.FileName);

            // get destination folder from web config
            string destFolder = Path.Combine(AppConfigs.GetString("SAN"), Path.Combine(AppConfigs.GetString("EvidenceFileDirectory"), DateTime.Now.Year.ToString()));

            // set the file path
            filePath = Path.Combine(destFolder, filename);

            // call the save process
            return SaveFile(bytes, destFolder, filePath);
        }

        /// <summary>
        /// Save identity file
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="dealerCode"></param>
        /// <param name="bytes"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SaveIdentityFile(AttachmentParameterDto attachment, string dealerCode, byte[] bytes, out string filePath)
        {
            string[] splitedFilename = attachment.FileName.Split('.');
            string ext = splitedFilename[splitedFilename.Length - 1];

            // set temporary file name
            string filename = string.Format("{0}_{1}{2}.{3}", dealerCode, DateTime.Now.ToString("yyyyMMddHHmmss"), Guid.NewGuid().ToString().Substring(0, 3), ext);

            // get destination folder from web config
            string sapFolder = AppConfigs.GetString("SAPFolder");
            string sapDir = Path.Combine(Path.Combine(AppConfigs.GetString("SAPFileDirectory"), @"OCR\"), DateTime.Now.Year.ToString());
            string destFolder = Path.Combine(sapFolder, sapDir);
            filePath = Path.Combine(DateTime.Now.Year.ToString(), filename);

            // set the file path
            string fileSavedPath = Path.Combine(destFolder, filename);

            // call the save process
            return SaveFile(bytes, destFolder, fileSavedPath);
        }

        /// <summary>
        ///  save identity file customer for api UploadImage
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="dealerCode"></param>
        /// <param name="bytes"></param>
        /// <param name="docType"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SaveIdentityFileCustom(AttachmentParameterDto attachment, string dealerCode, byte[] bytes, string docType, out string filePath)
        {
            string[] splitedFilename = attachment.FileName.Split('.');
            string ext = splitedFilename[splitedFilename.Length - 1];

            string destFolder = string.Empty;
            string filename = string.Empty;
            if (docType.ToUpper() == "KTP")
            {
                // set temporary file name
                filename = string.Format("{0}\\{1}_{2}{3}.{4}", DateTime.Now.Year.ToString(), dealerCode, DateTime.Now.ToString("yyyyMMddHHmmss"), Guid.NewGuid().ToString().Substring(0, 3), ext);

                // get destination folder from web config
                string sapFolder = AppConfigs.GetString("SAPFolder");
                string sapDir = Path.Combine(AppConfigs.GetString("SAPFileDirectory"), @"OCR\");
                destFolder = Path.Combine(sapFolder, sapDir);
            }else if(docType.ToUpper() == "CLAIMDOCUMENT")
            {
                //2020\VehicleClaim\AttachmentClaim\100001\CLM92020013\20200917154910b7d7cc9c-5ca1-4e01-9611-6970bcceb378..jpg
                // set temporary file name
                filename = string.Format("{0}\\VehicleClaim\\AttachmentClaim\\{1}\\{2}.{3}", DateTime.Now.ToString("yyyy"), dealerCode, DateTime.Now.ToString("yyyyMMddHHmm") + Guid.NewGuid().ToString(), ext);

                // get destination folder from web config
                string sapFolder = AppConfigs.GetString("SAPFolder");
                string sapDir = AppConfigs.GetString("CBUReturnClaimDirectory");
                destFolder = Path.Combine(sapFolder, sapDir);
            }
            else if (docType == "claimsparepart")
            {
                // set temporary file name
                filename = string.Format("{0}\\{1}", DateTime.Now.ToString("yyyy"), dealerCode+"_"+DateTime.Now.ToString("yyyyMMddHHmm") + attachment.FileName);

                // get destination folder from web config
                string sapFolder = AppConfigs.GetString("SAPFolder");
                destFolder = Path.Combine(sapFolder, @"Claim-Evidence\");
            }
            if (docType.ToUpper() == "LKPP")
            {
                // set temporary file name
                filename = attachment.FileName;

                // get destination folder from web config
                destFolder = Path.Combine(AppConfigs.GetString("SAPFolder"), @"LKPP\");
                
            }

            filePath = filename;
            // set the file path
            string fileSavedPath = Path.Combine(destFolder, filename);

            // validate directory
            var targetInfo = new FileInfo(fileSavedPath);
            if (!targetInfo.Directory.Exists)
            {
                // set the credentials to access the repository server
                string user = AppConfigs.GetString("User");
                string password = AppConfigs.GetString("Password");
                string webServer = AppConfigs.GetString("WebServer");
                UserImpersonater imp = new UserImpersonater(user, password, webServer);
                bool success = false;

                try
                {
                    success = imp.Start();
                    if (success)
                    {
                        Directory.CreateDirectory(targetInfo.DirectoryName);
                        imp.Stop();
                    }
                    else
                    {
                        fileSavedPath = string.Empty;
                    }
                }
                catch
                {
                    fileSavedPath = string.Empty;
                }
                finally
                {
                    imp.Dispose();
                }                
            }

            // call the save process
            return SaveFile(bytes, destFolder, fileSavedPath);
        }

        /// <summary>
        /// to validate imagePath from users
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static bool CheckExistsImagePath(string imagePath)
        {
            // set the credentials to access the repository server
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            bool success = false;
            var result = false;

            try
            {
                success = imp.Start();
                if (success)
                {
                    // cek exists
                    string path = imagePath;
                    result = File.Exists(path);

                    imp.Stop();
                }
                else
                {
                    return result;
                }
            }
            catch
            {
                return result;
            }
            finally
            {
                imp.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Save Photo file
        /// </summary>
        /// <param name="attachment"></param>        
        /// <param name="bytes"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SavePhotoFile(AttachmentParameterDto attachment, byte[] bytes, out string filePath)
        {
            // set temporary file name
            string filename = string.Format(Constants.EmployeeService.TEMP_MECHANIC_FILENAME, DateTime.Now.ToString("yyyyMMddHHmmssffff"), attachment.FileName);

            // get destination folder from web config
            string destFolder = Path.Combine(AppConfigs.GetString("SAN"), Path.Combine(AppConfigs.GetString("PhotoFileDirectory"), DateTime.Now.Year.ToString()));

            // set the file path
            filePath = Path.Combine(destFolder, filename);

            // call the save process
            return SaveFile(bytes, destFolder, filePath);
        }

        /// <summary>
        /// Save OCR file in local drive
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        public static string SaveOCRFileToLocal(HttpPostedFile postedFile)
        {
            // set temporary file name
            string filename = string.Format(Constants.TEMP_OCR_FILENAME, DateTime.Now.ToString("yyyyMMddHHmmssffff"), postedFile.FileName);

            // get destination folder from web config
            string destFolder = AppDomain.CurrentDomain.BaseDirectory + "Temp";

            // set file path
            string filePath = Path.Combine(destFolder, filename);

            try
            {
                // create forlder if it is not exists
                if (!Directory.Exists(destFolder))
                {
                    Directory.CreateDirectory(destFolder);
                }

                // write the file
                postedFile.SaveAs(filePath);

                // return the path
                return filePath;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Delete OCR file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string DeleteOCRLocalFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            try
            {
                // delete the file
                File.Delete(filePath);

                return string.Empty;
            }
            catch
            {
                return MessageResource.ErrorMsgOnDeleteFile;
            }
        }

        /// <summary>
        /// Delete evidence file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string DeleteEvidenceFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            // set credentials to access repository server
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            bool success = false;

            try
            {
                success = imp.Start();
                if (success)
                {
                    // delete the file
                    File.Delete(filePath);

                    imp.Stop();
                }
                else
                {
                    return MessageResource.ErrorMsgOnDeleteFile;
                }
            }
            catch
            {
                return MessageResource.ErrorMsgOnDeleteFile;
            }
            finally
            {
                imp.Dispose();
            }

            return string.Empty;
        }

        /// <summary>
        /// Get SPK Customer Identity absolute filepath
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public static string GetSPKCustomerIdentityAbsoluteFilePath(string relativeFilePath)
        {
            string sapFolder = AppConfigs.GetString("SAPFolder");
            string filepath = Path.Combine(sapFolder, relativeFilePath);
            return filepath;
        }

        /// <summary>
        /// Move temporary uploaded file to a specified folder after the spk has already saved
        /// </summary>
        /// <param name="spk"></param>
        /// <param name="tempFilePath"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string MoveSPKEvidenceFile(SPKHeader spk, string tempFilePath, string filename)
        {
            // set the credentials to access the repository server
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            bool success = false;

            // set the default destination forlder from web config
            string destFolder = Path.Combine(AppConfigs.GetString("SAN"), Path.Combine(AppConfigs.GetString("SPKFileDirectory"), DateTime.Now.Year.ToString()));

            string newFilePath = Path.Combine(destFolder, filename);

            try
            {
                success = imp.Start();
                if (success)
                {
                    // create forlder if it is not exists
                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }

                    if (File.Exists(newFilePath))
                    {
                        File.Delete(newFilePath);
                    }

                    // move the file
                    File.Move(tempFilePath, newFilePath);

                    imp.Stop();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                imp.Dispose();
            }

            // update spk evidence file url
            string evidenceFilePath = Path.Combine(Path.Combine(AppConfigs.GetString("SPKFileDirectory"), DateTime.Now.Year.ToString()), filename);

            return evidenceFilePath;
        }

        /// <summary>
        /// Create file for KTB
        /// </summary>
        /// <param name="sparePartPO"></param>
        /// <param name="isEmergency"></param>
        public static void CreateTextFileForKTB(SparePartPO sparePartPO, bool isEmergency = false)
        {
            string FOLDER_NAME = AppConfigs.GetString("DNetServerFolder") + sparePartPO.PONumber.Substring(1, 4);
            string FILE_NAME = FOLDER_NAME + ("\\" + (sparePartPO.PONumber + (isEmergency ? ".EOD" : ".DAT")));
            string _user = AppConfigs.GetString("User");
            string _password = AppConfigs.GetString("Password");
            string _sapServer = AppConfigs.GetString("SAPServer");

            UserImpersonater imp = new UserImpersonater(_user, _password, _sapServer);
            var _sparepartpoMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartPO).ToString());
            try
            {
                bool succes = imp.Start();
                if (succes)
                {
                    string OldCode = sparePartPO.ProcessCode;
                    sparePartPO.ProcessCode = "S";
                    sparePartPO.SentPODate = DateTime.Now;
                    int nResult = _sparepartpoMapper.Update(sparePartPO, sparePartPO.CreatedBy);
                    if ((nResult != -1))
                    {
                        var _dataHistoryMapper = MapperFactory.GetInstance().GetMapper(typeof(DataHistory).ToString());
                        DataHistory dataHistory = new DataHistory();
                        dataHistory.DataTable = "SparePartPO";
                        dataHistory.DataID = sparePartPO.ID;
                        dataHistory.ID = _dataHistoryMapper.Insert(dataHistory, sparePartPO.CreatedBy);
                        if (dataHistory.ID > 0)
                        {
                            DataHistoryDetail oDHD = new DataHistoryDetail();
                            oDHD.DataHistory = dataHistory;
                            oDHD.FieldName = "ProcessCode";
                            oDHD.Description = "Status";
                            oDHD.OldValue = OldCode;
                            oDHD.NewValue = sparePartPO.ProcessCode;

                            var _dataHistoryDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(DataHistoryDetail).ToString());
                            oDHD.ID = _dataHistoryDetailMapper.Insert(oDHD, sparePartPO.CreatedBy);
                        }

                        if (!Directory.Exists(FOLDER_NAME))
                        {
                            Directory.CreateDirectory(FOLDER_NAME);
                        }

                        if (File.Exists(FILE_NAME))
                        {
                            File.Delete(FILE_NAME);
                        }

                        bool isNotRorZ = !(sparePartPO.OrderType.Equals("R", StringComparison.OrdinalIgnoreCase) ||
                                           sparePartPO.OrderType.Equals("Z", StringComparison.OrdinalIgnoreCase));
                        if (isNotRorZ)
                        {
                            FileStream fs = new FileStream(FILE_NAME, FileMode.CreateNew);
                            StreamWriter w = new StreamWriter(fs);
                            WritePOHeaderToFile(sparePartPO, ref w);
                            WritePODetailToFile(sparePartPO, ref w);
                            w.Close();
                            fs.Close();
                        }

                        imp.Stop();

                        // update the process code to S Sudah Dikirim
                        sparePartPO.ProcessCode = "S";
                        // update is transfer status
                        sparePartPO.IsTransfer = 1;

                        // update
                        _sparepartpoMapper.Update(sparePartPO, sparePartPO.CreatedBy);
                    }
                }
            }
            catch
            {
                // do nothing for now
            }
            finally
            {
                imp.Dispose();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Write PO Header To File
        /// </summary>
        /// <param name="sparePartPO"></param>
        /// <param name="w"></param>
        private static void WritePOHeaderToFile(SparePartPO sparePartPO, ref StreamWriter w)
        {
            StringBuilder sbSetARecord = new StringBuilder();
            char pad = ' ';
            sbSetARecord.Append("T");
            sbSetARecord.Append(sparePartPO.PONumber.PadRight(15, pad));
            sbSetARecord.Append(sparePartPO.Dealer.DealerName.Substring(0, 25).PadRight(25, pad));
            sbSetARecord.Append(string.Format("{0:yyyyMMdd}", sparePartPO.PODate));
            sbSetARecord.Append(sparePartPO.SparePartPODetails.Count.ToString().PadLeft(4, '0'));
            w.WriteLine(sbSetARecord.ToString());
        }

        /// <summary>
        /// Write PO Detail to file
        /// </summary>
        /// <param name="sparePartPO"></param>
        /// <param name="w"></param>
        private static void WritePODetailToFile(SparePartPO sparePartPO, ref StreamWriter w)
        {
            StringBuilder sbSetARecord = new StringBuilder();
            char pad = ' ';
            int index = 0;
            foreach (SparePartPODetail objPODetail in sparePartPO.SparePartPODetails)
            {
                index++;
                sbSetARecord.Remove(0, sbSetARecord.Length);
                sbSetARecord.Append("D");
                sbSetARecord.Append(objPODetail.SparePartPO.PONumber.PadRight(15, pad));
                sbSetARecord.Append(objPODetail.SparePartMaster.PartNumber.PadRight(20, pad));
                sbSetARecord.Append(objPODetail.Quantity.ToString().PadLeft(5, '0'));
                sbSetARecord.Append(string.Format("{0:yyyyMMdd}", objPODetail.SparePartPO.PODate));
                sbSetARecord.Append(index.ToString().PadLeft(4, '0'));
                w.WriteLine(sbSetARecord.ToString());
            }
        }

        /// <summary>
        /// Get Maximum File Size
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        private static int GetMaximumFileSize(AutoMapper.IMapper mapper)
        {
            // get maximum size from AppConfig
            IAppConfigBL appConfigBL = new AppConfigBL(mapper);
            ResponseBase<AppConfigDto> response = appConfigBL.GetByName("MaximumFileSize");

            try
            {
                if (response.success)
                {
                    if (response.lst.ID > 0)
                    {
                        return int.Parse(response.lst.Value.Trim());
                    }
                }

                return 0;

            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Save certain file on server
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="destFolder"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string SaveFile(byte[] bytes, string destFolder, string filePath)
        {
            // set credentials to access repository server
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");

            // create impersonater
            UserImpersonater imp = new UserImpersonater(user, password, webServer);

            try
            {
                // start it
                bool success = imp.Start();
                if (success)
                {
                    // create directory if it is not exists
                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }

                    // existence validation
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    // write the file
                    File.WriteAllBytes(filePath, bytes);

                    // stop 
                    imp.Stop();
                }
                else
                {
                    return MessageResource.ErrorMsgOnSaveFile;
                }
            }
            catch
            {
                return MessageResource.ErrorMsgOnSaveFile;
            }
            finally
            {
                imp.Dispose();
            }

            return string.Empty;
        }
        /// <summary>
        /// Save certain file on server
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="destFolder"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string DeleteFileFS(byte[] bytes, string destFolder, string filePath)
        {
            // set credentials to access repository server
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");

            // create impersonater
            UserImpersonater imp = new UserImpersonater(user, password, webServer);

            try
            {
                // start it
                bool success = imp.Start();
                if (success)
                {

                    // existence validation
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    // stop 
                    imp.Stop();
                }
                else
                {
                    return MessageResource.ErrorMsgOnSaveFile;
                }
            }
            catch
            {
                return MessageResource.ErrorMsgOnSaveFile;
            }
            finally
            {
                imp.Dispose();
            }

            return string.Empty;
        }
        #endregion
    }
}
