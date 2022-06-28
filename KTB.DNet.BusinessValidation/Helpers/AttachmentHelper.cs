using KTB.DNet.Domain;
using Spire.Doc;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpireDoc = Spire.Doc;
using SpireDocument = Spire.Doc.Document;

namespace KTB.DNet.BusinessValidation
{
    public static class AttachmentHelper
    { 
        public static string SaveEmployeePhotoAndKTPFile(string SANstring, string stringKTPEmployeeDealerToOCR, string stringDealerEmployeePhoto, string stringMaximumFileSize, string stringMaximumEmployeePhotoSize, string fileName, string base64OfStream, string dealerCode, List<ValidResult> validationResults, bool isKTP, string user, string password, string webServer)
        {
            var destFile = String.Empty;

            if (isKTP)
            {
                //destFile = AppConfigs.GetString("SAN") + AppConfigs.GetString("KTPEmployeeDealerToOCR") + "\\" + dealerCode + "\\" + Guid.NewGuid().ToString().Substring(0, 5);
                destFile = SANstring + stringKTPEmployeeDealerToOCR + "\\" + dealerCode + "\\" + Guid.NewGuid().ToString().Substring(0, 5);
            }
            else
            {
                destFile = SANstring +stringDealerEmployeePhoto + "\\" + dealerCode + "\\" + Guid.NewGuid().ToString().Substring(0, 5);
            }

            var finfo = new FileInfo(destFile);
            var fileExt = System.IO.Path.GetExtension(fileName);
            var fileLocation = destFile + fileExt;
            int maxSize = isKTP ? Convert.ToInt32(stringMaximumFileSize) : Convert.ToInt32(stringMaximumEmployeePhotoSize);
            byte[] file = Convert.FromBase64String(base64OfStream);

            if (fileExt.ToUpper() != ".JPG" && fileExt.ToUpper() != ".JPEG" && fileExt.ToUpper() != ".PNG")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Format file hanya .jpg, .jpeg, dan .png"
                };
                validationResults.Add(validResult);
                return string.Empty;
            }

            if (file.Length > maxSize)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Ukuran file tidak boleh melebihi {0} KB", maxSize)
                };
                validationResults.Add(validResult);
                return string.Empty;
            }

            if (SaveFile(file, destFile, fileLocation, user, password, webServer) == String.Empty)
            {
                fileLocation = fileLocation.Replace(SANstring, String.Empty);
                return fileLocation;
            }

            return string.Empty;
        }

        public static void GetCertificateWA(WarrantyActivation wr, string SANstring, ref string filename, ref FileStream fs, MemoryStream docFile, bool isUpdate, bool isEncrypt, List<ValidResult> validationResults, string user, string password, string webServer, bool isFromUI = false)
        {
            string filePath = Path.Combine(SANstring, string.IsNullOrEmpty(wr.FileName) ? "" : wr.FileName);
            string msgErr = string.Empty;
            if (isUpdate)
            {
                string destFolder = string.Format(@"PKT-Certificate\{0}\{1}", DateTime.Now.Year, DateTime.Now.ToString("MM"));
                msgErr = SavePDFFile(wr.ChassisMaster.ChassisNumber, isEncrypt, SANstring, docFile, destFolder, ref filename, ref fs, user, password, webServer);
            }
            else if (!isFromUI)
                msgErr = GetFile(filePath, isEncrypt, ref filename, ref fs, user, password, webServer);
            else
                filename = isEncrypt ? wr.FileName.Replace(".pdf", "-pwd.pdf") : wr.FileName;

            if (!string.IsNullOrEmpty(msgErr))
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = msgErr
                };
                validationResults.Add(validResult);
            }
        }

        public static void GetCertificatePDI(PDI wr, string SANstring, ref string filename, ref FileStream fs, MemoryStream docFile, bool isUpdate, bool isEncrypt, List<ValidResult> validationResults, string user, string password, string webServer, bool isFromUI = false)
        {
            string filePath = Path.Combine(SANstring, string.IsNullOrEmpty(wr.FileName) ? "" : wr.FileName);
            string msgErr = string.Empty;
            if (isUpdate)
            {
                string destFolder = string.Format(@"PDI-PDF\{0}\{1}", DateTime.Now.Year, DateTime.Now.ToString("MM"));
                msgErr = SavePDFFile(wr.ChassisMaster.ChassisNumber, isEncrypt, SANstring, docFile, destFolder, ref filename, ref fs, user, password, webServer);
            }
            else if (!isFromUI)
                msgErr = GetFile(filePath, isEncrypt, ref filename, ref fs, user, password, webServer);
            else
                filename = isEncrypt ? wr.FileName.Replace(".pdf", "-pwd.pdf") : wr.FileName;

            if (!string.IsNullOrEmpty(msgErr))
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = msgErr
                };
                validationResults.Add(validResult);
            }
        }

        /// <summary>
        /// Save certain file on server
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="destFolder"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string SaveFile(byte[] bytes, string destFolder, string filePath, string user, string password, string webServer)
        {
            // set credentials to access repository server
            //string user = AppConfigs.GetString("User");
            //string password = AppConfigs.GetString("Password");
            //string webServer = AppConfigs.GetString("WebServer");

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
                    return "Error saat menyimpan file. Harap hubungi administrator";
                }
            }
            catch
            {
                return "Error saat menyimpan file. Harap hubungi administrator";
            }
            finally
            {
                imp.Dispose();
            }

            return string.Empty;
        }

        private static string SavePDFFile(string chassisNumber, bool isEncrypt, string SANString, MemoryStream doc, string destFolder, ref string filename, ref FileStream fs, string user, string password, string webServer)
        {
            UserImpersonater imp = new UserImpersonater(user, password, webServer);

            try
            {
                // start it
                bool success = imp.Start();
                if (success)
                {
                    // create directory if it is not exists
                    if (!Directory.Exists(Path.Combine(SANString, destFolder)))
                    {
                        Directory.CreateDirectory(Path.Combine(SANString, destFolder));
                    }

                    // write the file
                    string fullPath = Path.Combine(SANString, destFolder, filename);
                    string fullpathEnc = fullPath.Replace(".pdf", "-pwd.pdf");

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }

                    if (File.Exists(fullpathEnc))
                    {
                        File.Delete(fullpathEnc);
                    }

                    SpireDoc.Document pdfDoc = new SpireDoc.Document();
                    ToPdfParameterList toPdf = new ToPdfParameterList();
                    toPdf.UsePSCoversion = true;

                    //toPdf.EmbeddedFontNameList.Add("MMC");
                    //pdfDoc.EmbedSystemFonts = true;

                    pdfDoc.LoadFromStream(doc, Spire.Doc.FileFormat.Docx);
                    pdfDoc.BuiltinDocumentProperties.Author = "D-Net MMKSI";
                    pdfDoc.SaveToFile(fullPath, toPdf);

                    toPdf.PdfSecurity.UserPassword = chassisNumber.Substring(chassisNumber.Length - 6, 6);

                    pdfDoc.SaveToFile(fullpathEnc, toPdf);

                    fs = new FileStream(isEncrypt ? fullpathEnc : fullPath, FileMode.Open, FileAccess.Read);
                    filename = Path.Combine(destFolder, filename);

                    doc.Close();
                    doc.Dispose();

                    // stop 
                    imp.Stop();
                }
                else
                {
                    return "Error saat menyimpan file. Harap hubungi administrator";
                }
            }
            catch
            {
                return "Error saat menyimpan file. Harap hubungi administrator";
            }
            finally
            {
                imp.Dispose();
            }

            return string.Empty;
        }

        private static string GetFile(string filePath, bool isEncrypt, ref string filename, ref FileStream fs, string user, string password, string webServer)
        {
            UserImpersonater imp = new UserImpersonater(user, password, webServer);

            try
            {
                // start it
                bool success = imp.Start();
                if (success)
                {
                    filePath = isEncrypt ? filePath.Replace(".pdf", "-pwd.pdf") : filePath;
                    if (!File.Exists(filePath))
                    {
                        return null;
                    }
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    filename = Path.GetFileName(filePath);

                    // stop 
                    imp.Stop();
                }
                else
                {
                    return "Error saat mengambil file. Harap hubungi administrator";
                }
            }
            catch
            {
                return "Error saat mengambil file. Harap hubungi administrator";
            }
            finally
            {
                imp.Dispose();
            }

            return string.Empty;
        }
    }
}
