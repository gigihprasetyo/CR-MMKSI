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
    public class PDIValidation
    {
        private const string sp_PDIGetInfoTemplate = "sp_PDIGetInfoTemplate";
        private readonly string _SANString;
        private readonly string _user;
        private readonly string _password;
        private readonly string _webServer;

        public PDIValidation(string SANstring, string user, string password, string webServer)
        {
            _SANString = SANstring;
            _user = user;
            _password = password;
            _webServer = webServer;
        }

        public bool GenerateCertificate(PDI wr, ref bool isUpdate, ref string filename, ref FileStream fs, List<ValidResult> validationResults, bool isEncrypted, bool isFromUI = false)
        {
            try
            {
                isUpdate = !string.IsNullOrEmpty(wr.FileName) ? !IsFileExist(wr.FileName) : true;
                DataSet ds = GetParameter(wr.ChassisMaster.ID);
                MemoryStream docFile = null;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Sertifikat PDI untuk Chassis Number [{0}] belum memiliki Master Data PDI Template. Mohon untuk melakukan input terlebih dahulu.", wr.ChassisMaster.ChassisNumber)
                    };
                    validationResults.Add(validResult);
                }
                else if (isUpdate)
                    GenerateTemplate(wr, ref filename, ref docFile, ds, validationResults);

                if (validationResults.Count == 0)
                    AttachmentHelper.GetCertificatePDI(wr, _SANString, ref filename, ref fs, docFile, isUpdate, isEncrypted, validationResults, _user, _password, _webServer, isFromUI);
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

        private bool GenerateTemplate(PDI wr, ref string filename, ref MemoryStream docFile, DataSet ds, List<ValidResult> validationResults)
        {
            string msgErr = string.Empty;
            byte[] file = null;
            FileStream fs = null;
            DataTable dtTemplate = ds.Tables[0];

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
                            case "<ModelKendaraan>":
                            case "ModelKendaraan":
                                word.Text = dtTemplate.Rows[0]["IndDescription"].ToString();
                                break;
                            case "<NomorRangka>":
                            case "NomorRangka":
                                word.Text = wr.ChassisMaster.ChassisNumber;
                                break;
                            case "<TglPelaksanaan>":
                            case "TglPelaksanaan":
                                word.Text = wr.PDIDate.ToString("dd MMMM yyyy", cultureInfo);
                                break;
                            case "<NamaDealer>":
                            case "NamaDealer":
                                word.Text = string.Format("{0} / {1}", wr.Dealer.DealerName, wr.Dealer.City.CityName);
                                break;
                            case "<WONumber>":
                            case "WONumber":
                                word.Text = wr.WorkOrderNumber;
                                break;
                        }
                    }
                }

                docFile = ms;
                //filename = string.Format("{0}_{1}_{2}.pdf", wr.Dealer.DealerCode, wr.ChassisMaster.ChassisNumber, wr.PDIDate.ToString("ddMMyyyy"));
                filename = string.Format("{0}_{1}.pdf", wr.ChassisMaster.ChassisNumber.Substring(0, 4), Guid.NewGuid().ToString().Substring(0, 6));
            }

            return true;
        }

        private DataSet GetParameter(int chassisID)
        {
            var _m_pdi = MapperFactory.GetInstance().GetMapper(typeof(PDI).ToString());
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
            ds = _m_pdi.RetrieveDataSet(PDIValidation.sp_PDIGetInfoTemplate, parameters);

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
