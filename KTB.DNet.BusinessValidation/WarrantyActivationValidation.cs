using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using B = DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Globalization;

namespace KTB.DNet.BusinessValidation
{
    public class WarrantyActivationValidation
    {
        private const string sp_PKTGetInfoTemplate = "sp_PKTGetInfoTemplate";
        private readonly string _SANString;
        private readonly string _user;
        private readonly string _password;
        private readonly string _webServer;

        public WarrantyActivationValidation(string SANstring, string user, string password, string webServer)
        {
            _SANString = SANstring;
            _user = user;
            _password = password;
            _webServer = webServer;
        }

        public bool GenerateCertificate(WarrantyActivation wr, ref bool isUpdate, ref string filename, ref FileStream fs, List<ValidResult> validationResults, bool isEncrypted, bool isFromUI = false)
        {
            try
            {
                isUpdate = !string.IsNullOrEmpty(wr.FileName) ? !IsFileExist(wr.FileName) : true;
                DataSet ds = GetParameter(wr.ChassisMaster.ID);
                MemoryStream docFile = null;
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[1].Rows.Count == 0){
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Sertifikat PKT dengan Chassis Number [{0}] belum memiliki Master Data PKT Template atau Specimen Customer. Mohon untuk melakukan input terlebih dahulu.", wr.ChassisMaster.ChassisNumber)
                    };
                    validationResults.Add(validResult);
                }
                else if (isUpdate)
                    GenerateTemplate(wr, ref filename, ref docFile, ds, validationResults);

                if (validationResults.Count == 0)
                    AttachmentHelper.GetCertificateWA(wr, _SANString, ref filename, ref fs, docFile, isUpdate, isEncrypted, validationResults, _user, _password, _webServer, isFromUI);
            }
            catch (Exception e)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = e.Message
                };
                validationResults.Add(validResult);
            }

            if (isFromUI && fs != null)
                fs.Close();

            return validationResults.Count == 0;
        }

        private bool GenerateTemplate(WarrantyActivation wr, ref string filename, ref MemoryStream docFile, DataSet ds, List<ValidResult> validationResults)
        {
            string msgErr = string.Empty;
            byte[] file = null;
            FileStream fs = null;
            DataTable dtTemplate = ds.Tables[0];
            DataTable dtSpecimen = ds.Tables[1];

            string docPath = Path.Combine(_SANString, dtTemplate.Rows[0]["FileName"].ToString());
            msgErr = ReadFile(docPath, ref file, ref fs);
            if (!string.IsNullOrEmpty(msgErr))
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = msgErr
                };
                validationResults.Add(validResult);
                return false;
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(file, 0, file.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(ms, true))
                {
                    var body = doc.MainDocumentPart.Document.Body;
                    byte[] image = null;
                    string imagePath1 = Path.Combine(_SANString, dtSpecimen.Rows[0]["DSFilePath"].ToString());

                    int index = 1;
                    var listPics = doc.MainDocumentPart.Document.Body.Descendants<B.Drawing>().ToList();
                    foreach (var pic in listPics)
                    {
                        string embed = null;

                        if (pic != null)
                        {
                            var blip = pic.Descendants<A.Blip>().FirstOrDefault();
                            if (blip != null) embed = blip.Embed;
                        }

                        if (embed != null)
                        {
                            var idpp = doc.MainDocumentPart.Parts.Where(x => x.RelationshipId == embed).FirstOrDefault();
                            if (idpp != null)
                            {
                                var ip = (ImagePart)idpp.OpenXmlPart;
                                string imgSrc = string.Empty;
                                switch (index)
                                {
                                    case 2 :
                                        imgSrc = imagePath1;
                                        break;
                                }

                                if (!string.IsNullOrEmpty(imgSrc) && imgSrc != _SANString)
                                {
                                    ReadFile(imgSrc, ref image, ref fs);
                                    ip.FeedData(fs);
                                }
                                //else
                                //    pic.Remove();
                            }
                        }

                        index++;
                    }

                    var cultureInfo = new CultureInfo("id-ID");
                    var words = body.Elements<B.Table>()
                                .SelectMany(s => s.Elements<B.TableRow>())
                                .SelectMany(s => s.Elements<B.TableCell>())
                                .SelectMany(s => s.Elements<B.Paragraph>())
                                .SelectMany(s => s.Elements<B.Run>())
                                .SelectMany(s => s.Elements<B.Text>()).ToList();

                    foreach (var word in words)
                    {
                        switch (word.Text.Trim())
                        {
                            case ">":
                            case "<":
                                word.Text = "";
                                break;
                            case "<CustomerName>":
                            case "CustomerName":
                                word.Text = dtTemplate.Rows[0]["CustomerName"].ToString();
                                break;
                            case "<CustAddr>":
                            case "CustAddr":
                                word.Text = dtTemplate.Rows[0]["Address"].ToString();
                                break;
                            case "<PhoneNumber>":
                            case "PhoneNumber":
                                word.Text = dtTemplate.Rows[0]["PhoneNo"].ToString();
                                break;
                            case "<ModelDesc>":
                            case "ModelDesc":
                                word.Text = dtTemplate.Rows[0]["ModelDescription"].ToString();
                                break;
                            case "<ChassisNumber>":
                            case "ChassisNumber":
                                word.Text = dtTemplate.Rows[0]["ChassisNumber"].ToString();
                                break;
                            case "<EngineNumber>":
                            case "EngineNumber":
                                word.Text = dtTemplate.Rows[0]["EngineNumber"].ToString();
                                break;
                            case "<FakturDate>":
                            case "FakturDate":
                                word.Text = string.IsNullOrEmpty(dtTemplate.Rows[0]["FakturDate"].ToString()) ? "" : Convert.ToDateTime(dtTemplate.Rows[0]["FakturDate"]).ToString("dd MMMM yyyy", cultureInfo);
                                break;
                            case "<PKTDate>":
                            case "PKTDate":
                                word.Text = string.IsNullOrEmpty(dtTemplate.Rows[0]["PKTDate"].ToString()) ? "" : Convert.ToDateTime(dtTemplate.Rows[0]["PKTDate"]).ToString("dd MMMM yyyy", cultureInfo);
                                break;
                        }
                    }
                }

                docFile = ms;
                //filename = String.Format(@"{0}_{1}_{2}.pdf", wr.PDI.Dealer.DealerCode, wr.ChassisMaster.ChassisNumber, wr.WADate.ToString("ddMMyyyy"));
                filename = string.Format("{0}_{1}.pdf", wr.ChassisMaster.ChassisNumber.Substring(0, 4), Guid.NewGuid().ToString().Substring(0, 6));
            }

            return true;
        }

        private DataSet GetParameter(int chassisID)
        {
            var _m_warrantyActivation = MapperFactory.GetInstance().GetMapper(typeof(WarrantyActivation).ToString());
            DataSet ds = new DataSet();
            ArrayList parameters = new ArrayList();

            SqlParameter parameter = new SqlParameter
            {
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = chassisID,
                ParameterName = "@ChassisId"
            };
            parameters.Add(parameter);
            ds = _m_warrantyActivation.RetrieveDataSet(WarrantyActivationValidation.sp_PKTGetInfoTemplate, parameters);

            return ds;
        }

        private string ReadFile(string path, ref byte[] file, ref FileStream fs)
        {
            UserImpersonater imp = new UserImpersonater(_user, _password, _webServer);

            try
            {
                // start it
                bool success = imp.Start();
                if (success)
                {
                    file = File.ReadAllBytes(path);
                    fs = File.Open(path, FileMode.Open, FileAccess.Read);

                    // stop 
                    imp.Stop();
                }
                else
                {
                    return "Error saat membaca file. Harap hubungi administrator";
                }
            }
            catch
            {
                return "Error saat membaca file. Harap hubungi administrator";
            }
            finally
            {
                imp.Dispose();
            }

            return string.Empty;
        }

        private bool IsFileExist(string path)
        {
            UserImpersonater imp = new UserImpersonater(_user, _password, _webServer);
            bool isExist = false;
            try
            {
                // start it
                bool success = imp.Start();
                if (success)
                {
                    FileInfo file1 = new FileInfo(Path.Combine(_SANString, path));
                    FileInfo file2 = new FileInfo(Path.Combine(_SANString, path.Replace(".pdf", "-pwd.pdf")));
                    if (file1.Exists && file2.Exists)
                        isExist = true;
                    else
                        isExist = false;

                    // stop 
                    imp.Stop();
                }
                else
                {
                    isExist = false;
                }
            }
            catch
            {
                isExist = false;
            }
            finally
            {
                imp.Dispose();
            }

            return isExist;
        }
    }
}
