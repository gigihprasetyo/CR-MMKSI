using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNET.SAP;
using KTB.DNet.Utility;
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KTB.DNet.BusinessValidation
{
    public static class CBUReturnValidation
    {
        public const string STATUS_CLAIM_NOT_VALID = "Claim tidak dapat di proses karena status tidak valid atau status telah berubah oleh proses lain.";
        public const string STATUS_PROSES_RETUR_NOT_VALID = "Claim tidak dapat di proses karena status proses retur tidak valid atau status proses retur telah berubah oleh proses lain.";
        public const string STATUS_STOCK_DMS_NOT_VALID = "Proses retur tidak dapat dilakukan, Status Stok DMS harus Available";
        private const string sp_CBUReturnGetBilingNumber = "sp_CBUReturnGetBilingNumber";
        private const string sp_CBUReturnValidateBilling = "sp_CBUReturnValidateBilling";

        public static bool IsValidFileUploadDocument(string appConfigName, string ext, ref string listExt)
        {
            var _m_AppConfig = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            AppConfig _config = new AppConfig();
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, appConfigName));
            bool isValid = false;

            _config = _m_AppConfig.RetrieveByCriteria(criteria).OfType<AppConfig>().FirstOrDefault();
            if (_config != null)
            {
                string[] validExt = _config.Value.Split(';');
                listExt = string.Join(", ", validExt).Replace(".", "");
                for (int i = 0; i < validExt.Length; i++)
                {
                    if (validExt[i].ToLower().Equals(ext.ToLower()))
                        isValid = true;
                }
            }

            return isValid;
        }

        public static bool IsValidFileUploadSize(string appConfigName, int fileSize, ref string result)
        {
            var _m_AppConfig = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            AppConfig _config = new AppConfig();
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, appConfigName));
            bool isValid = false;

            _config = _m_AppConfig.RetrieveByCriteria(criteria).OfType<AppConfig>().FirstOrDefault();
            if (_config != null)
            {
                result = _config.Value;
                int actualSize = Convert.ToInt32(_config.Value) * 1048576; //Mb -> byte
                if (fileSize < actualSize)
                    isValid = true;
            }

            return isValid;
        }

        public static bool IsDuplicateDataClaim(string chassisNumber, ref int id)
        {
            bool isValid = false;

            var _m_ChassisMasterClaimHeader = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterClaimHeader).ToString());
            ChassisMasterClaimHeader _chassisMasterClaimHeader = new ChassisMasterClaimHeader();
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(ChassisMasterClaimHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));
            criteria.opAnd(new Criteria(typeof(ChassisMasterClaimHeader), "StatusID", MatchType.NotInSet, string.Format("{0}", (int)EnumCBUReturn.StatusClaim.Tolak)));

            _chassisMasterClaimHeader = _m_ChassisMasterClaimHeader.RetrieveByCriteria(criteria).OfType<ChassisMasterClaimHeader>().FirstOrDefault();
            if (_chassisMasterClaimHeader != null)
            {
                id = _chassisMasterClaimHeader.ID;
                isValid = true;
            }

            return isValid;
        }

        public static bool IsValidRetur(CBUReturnSendSAP objCBUReturnSendSAP)
        {
            bool isValid = true;
            SAPDNet objSAP = new SAPDNet(objCBUReturnSendSAP.SAPConn, objCBUReturnSendSAP.Username, objCBUReturnSendSAP.Password);
            DataTable dt;
            string message = string.Empty;

            switch (objCBUReturnSendSAP.CurrentStatusRetur)
            {
                case (int)EnumCBUReturn.StatusProsesRetur.Cancel_Faktur:
                    objCBUReturnSendSAP.SAPParameters = GetParameters(objCBUReturnSendSAP);
                    if (!ValidateParameter(objCBUReturnSendSAP.SAPParameters, objCBUReturnSendSAP, ref message))
                    {
                        objCBUReturnSendSAP.Message = message;
                        isValid = false;
                    }
                    else if (!objSAP.CBUReturnStatusCancelFaktur(objCBUReturnSendSAP.ChassisClaimHeaders, ref objCBUReturnSendSAP))
                        isValid = false;
                    break;
                case (int)EnumCBUReturn.StatusProsesRetur.Cancel_Billing:
                    dt = GetParameters(objCBUReturnSendSAP, 0, true);
                    if (!dt.Rows[0][0].ToString().Trim().Equals(""))
                    {
                        objCBUReturnSendSAP.Message = string.Format("Terdapat claim lain ({0}) yg memiliki nomor billing yang sama. Harap menunggu claim lain untuk di proses Cancel Billing secara bersamaan", dt.Rows[0][0].ToString().Trim());
                        isValid = false;
                    }
                    else
                    {
                        objCBUReturnSendSAP.SAPParameters = GetParameters(objCBUReturnSendSAP);
                        if (!ValidateParameter(objCBUReturnSendSAP.SAPParameters, objCBUReturnSendSAP, ref message))
                        {
                            objCBUReturnSendSAP.Message = message;
                            isValid = false;
                        }
                        else if (!objSAP.CBUReturnStatusCancelBilling(objCBUReturnSendSAP.ChassisClaimHeaders, ref objCBUReturnSendSAP))
                            isValid = false;
                    }
                    break;
                case (int)EnumCBUReturn.StatusProsesRetur.Reverse_DO:
                    dt = GetParameters(objCBUReturnSendSAP, 0, true);
                    if (!dt.Rows[0][0].ToString().Trim().Equals(""))
                    {
                        objCBUReturnSendSAP.Message = string.Format("Terdapat claim lain ({0}) yg memiliki nomor billing yang sama. Harap menunggu claim lain untuk di proses Reverse DO secara bersamaan", dt.Rows[0][0].ToString().Trim());
                        isValid = false;
                    }
                    else
                    {
                        objCBUReturnSendSAP.SAPParameters = GetParameters(objCBUReturnSendSAP);
                        if (!ValidateParameter(objCBUReturnSendSAP.SAPParameters, objCBUReturnSendSAP, ref message))
                        {
                            objCBUReturnSendSAP.Message = message;
                            isValid = false;
                        }
                        else if (!objSAP.CBUReturnStatusReserveDO(objCBUReturnSendSAP.ChassisClaimHeaders, ref objCBUReturnSendSAP))
                            isValid = false;
                    }
                    break;
                case (int)EnumCBUReturn.StatusProsesRetur.Sales_Replacement:
                    dt = GetParameters(objCBUReturnSendSAP, 0, true);
                    if (!dt.Rows[0][0].ToString().Trim().Equals(""))
                    {
                        objCBUReturnSendSAP.Message = string.Format("Terdapat claim lain ({0}) yg memiliki nomor billing yang sama. Harap menunggu claim lain untuk di proses Sales Replacement secara bersamaan", dt.Rows[0][0].ToString().Trim());
                        isValid = false;
                    }
                    else
                    {
                        objCBUReturnSendSAP.SAPParameters = GetParameters(objCBUReturnSendSAP);
                        if (!ValidateParameter(objCBUReturnSendSAP.SAPParameters, objCBUReturnSendSAP, ref message))
                        {
                            objCBUReturnSendSAP.Message = message;
                            isValid = false;
                        }
                        else if (!objSAP.CBUReturnStatusSalesReplacement(objCBUReturnSendSAP.ChassisClaimHeaders, ref objCBUReturnSendSAP))
                            isValid = false;
                    }
                    break;
                default:
                    objCBUReturnSendSAP.SAPParameters = GetParameters(objCBUReturnSendSAP, (int)enumInvoice.InvoiceKind.VH);
                    if (!ValidateParameter(objCBUReturnSendSAP.SAPParameters, objCBUReturnSendSAP, ref message))
                    {
                        objCBUReturnSendSAP.Message = message;
                        isValid = false;
                    }
                    else if (!objSAP.CBUReturnCheckClaim(objCBUReturnSendSAP.ChassisClaimHeaders, ref objCBUReturnSendSAP))
                        isValid = false;
                    break;
            }

            return isValid;
        }

        public static bool IsValidClaim(ChassisMasterClaimHeader obj, List<ChassisMasterClaimDetail> objDets, List<DocumentUpload> objDocs, ref string result, int changeStatusTo, bool fromDaftar = false)
        {
            bool isValid = true;
            switch (changeStatusTo) //To be status
            {
                //lemparan Result dari Daftar Claim (1=Konfirmasi, 2=Revisi, 3=Tolak, 4=SendTOSAP)
                //lemparan fromDaftar dari daftar claim = true

                case (int)EnumCBUReturn.StatusClaim.Baru:
                case (int)EnumCBUReturn.StatusClaim.Validasi:
                    if (obj.StatusID != (int)EnumCBUReturn.StatusClaim.Baru && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Revisi && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Validasi)
                    {
                        result = STATUS_CLAIM_NOT_VALID;
                        isValid = false;
                        break;
                    }

                    if (!fromDaftar && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Validasi)
                    {
                        bool isTempClaim = IsTempClaim(obj);
                        result = VerifyData(obj, objDets, objDocs, isTempClaim);
                        if (string.IsNullOrEmpty(result) && !isTempClaim)
                            IsValidThisClaim(obj, objDocs, ref result);
                    }
                    break;
                case (int)EnumCBUReturn.StatusClaim.Konfirmasi:
                    if ((fromDaftar && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Validasi) ||
                        (!fromDaftar && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Validasi && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Konfirmasi && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Proses))
                    {
                        result = STATUS_CLAIM_NOT_VALID;
                        isValid = false;
                        break;
                    }

                    if (!fromDaftar && obj.StatusID == (int)EnumCBUReturn.StatusClaim.Konfirmasi)
                    {
                        bool isTempClaim = IsTempClaim(obj);
                        result = VerifyData(obj, objDets, objDocs, isTempClaim);
                    }
                    break;
                case (int)EnumCBUReturn.StatusClaim.Proses:
                    if ((fromDaftar && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Konfirmasi) || (!fromDaftar && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Proses && obj.StatusID != (int)EnumCBUReturn.StatusClaim.Konfirmasi))
                    {
                        result = STATUS_CLAIM_NOT_VALID;
                        isValid = false;
                        break;
                    }
                    if (!fromDaftar)
                    {
                        bool isTempClaim = IsTempClaim(obj);
                        result = VerifyData(obj, objDets, objDocs, isTempClaim);
                        if (string.IsNullOrEmpty(result))
                        {
                            switch (obj.ResponClaim)
                            {
                                case (int)EnumCBUReturn.RespondClaim.Ganti_Unit:
                                    if (obj.StatusStockDMS == (int)EnumCBUReturn.StatusStockDMS.Not_Available && obj.StatusProcessRetur == (int)EnumCBUReturn.StatusProsesRetur.Cancel_Billing)
                                    {
                                        result = STATUS_STOCK_DMS_NOT_VALID;
                                    }
                                    else if(string.IsNullOrEmpty(obj.ChassisNumberReplacement.Trim())){
                                        result = "Chassis Pengganti harus diisi.";
                                    }
                                    else if (!IsValidChassisReplacement(obj, ref result) && obj.StatusProcessRetur == (int)EnumCBUReturn.StatusProsesRetur.Send_To_SAP)
                                    {
                                        break;
                                    }
                                    break;
                                case (int)EnumCBUReturn.RespondClaim.Asuransi:
                                case (int)EnumCBUReturn.RespondClaim.Ganti_Uang:
                                case (int)EnumCBUReturn.RespondClaim.Perbaikan_Dealer:
                                    //if (obj.Nominal == 0)
                                    //{
                                    //    result = "Nominal harus disii dan lebih dari 0.";
                                    //}
                                    if (obj.TransferDate.ToString() == "01/01/0001 0:00:00")
                                    {
                                        result = "Tanggal Transfer harus dipilih.";
                                    }
                                    break;
                                case (int)EnumCBUReturn.RespondClaim.Perbaikan_MMKSI:
                                    if (obj.CompletionDate.ToString() == "01/01/0001 0:00:00")
                                    {
                                        result = "Actual Selesai harus dipilih.";
                                    }
                                    else if (obj.RepairEstimationDate.ToString() == "01/01/0001 0:00:00")
                                    {
                                        result = "Estimasi Perbaikan harus dipilih.";
                                    }
                                    break;
                                default:
                                    result = "Respon Claim belum dipilih.";
                                    break;
                            }
                        }
                    }
                    break;
                case (int)EnumCBUReturn.StatusClaim.Selesai:
                    if (obj.StatusID != (int)EnumCBUReturn.StatusClaim.Proses)
                    {
                        result = STATUS_CLAIM_NOT_VALID;
                        isValid = false;
                        break;
                    }

                    if (!fromDaftar)
                    {
                        if (string.IsNullOrEmpty(result))
                        {
                            switch (obj.ResponClaim)
                            {
                                case (int)EnumCBUReturn.RespondClaim.Ganti_Unit:
                                    break;
                                case (int)EnumCBUReturn.RespondClaim.Asuransi:
                                case (int)EnumCBUReturn.RespondClaim.Ganti_Uang:
                                case (int)EnumCBUReturn.RespondClaim.Perbaikan_Dealer:
                                    if (obj.Nominal == 0)
                                    {
                                        result = "Nominal harus disii dan lebih dari 0.";
                                    }
                                    else if (obj.TransferDate.ToString() == "01/01/0001 0:00:00")
                                    {
                                        result = "Tanggal Transfer harus dipilih.";
                                    }
                                    break;
                                case (int)EnumCBUReturn.RespondClaim.Perbaikan_MMKSI:
                                    if (obj.CompletionDate.ToString() == "01/01/0001 0:00:00")
                                    {
                                        result = "Actual Selesai harus dipilih.";
                                    }
                                    break;
                                default:
                                    result = "Respon Claim belum dipilih.";
                                    break;
                            }
                        }
                    }
                    break;
                case (int)EnumCBUReturn.StatusClaim.Tolak:
                    if (obj.StatusID != (int)EnumCBUReturn.StatusClaim.Konfirmasi)
                    {
                        result = STATUS_CLAIM_NOT_VALID;
                        isValid = false;
                        break;
                    }
                    break;
                case (int)EnumCBUReturn.StatusClaim.Revisi:
                    if (obj.StatusID != (int)EnumCBUReturn.StatusClaim.Konfirmasi)
                    {
                        result = STATUS_CLAIM_NOT_VALID;
                        isValid = false;
                        break;
                    }
                    break;
                default:
                    result = "Silahkan Pilih status terlebih dahulu";
                    break;
            }
            if (!fromDaftar)
            {
                isValid = result.Equals("");
            }
            return isValid;
        }

        public static bool IsValidConfirm(ChassisMasterClaimHeader obj, List<DocumentUpload> objDocs, ref string result)
        {
            bool _return = true;

            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ChassisMasterATA), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(ChassisMasterATA), "ChassisMaster.ID", MatchType.Exact, obj.ChassisMaster.ID));

            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterATA).ToString());
            var cmATA = _mapper.RetrieveByCriteria(criteria);
            AppConfig _config = new AppConfig();

            criteria = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "ChassisMasterClaim.MaxDayValidate"));
            var _m_AppConfig = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            _config = _m_AppConfig.RetrieveByCriteria(criteria).OfType<AppConfig>().FirstOrDefault();

            if (cmATA.Count > 0 && _config != null)
            {
                ChassisMasterATA cMasterATA = cmATA[0] as ChassisMasterATA;
                if (cMasterATA.ATA.ToString() == "01/01/1753 0:00:00")
                {
                    result = "Tanggal ATA harus diisi terlebih dahulu.";
                    _return = false;
                }
                else
                {
                    var _m_NationalHoliday = MapperFactory.GetInstance().GetMapper(typeof(NationalHoliday).ToString());
                    DateTime claimDate = obj.ClaimDate;

                    criteria = new CriteriaComposite(new Criteria(typeof(NationalHoliday), "RowStatus", MatchType.Exact, 0));
                    criteria.opAnd(new Criteria(typeof(NationalHoliday), "HolidayDateTime", MatchType.Exact, claimDate));
                    NationalHoliday _nationalHoliday = _m_NationalHoliday.RetrieveByCriteria(criteria).OfType<NationalHoliday>().FirstOrDefault();

                    if (_nationalHoliday != null)
                    {
                        claimDate = CommonFunction.AddNWorkingDay(claimDate, 1);
                    }

                    criteria = new CriteriaComposite(new Criteria(typeof(NationalHoliday), "RowStatus", MatchType.Exact, 0));
                    criteria.opAnd(new Criteria(typeof(NationalHoliday), "HolidayDateTime", MatchType.GreaterOrEqual, cMasterATA.ATA));
                    criteria.opAnd(new Criteria(typeof(NationalHoliday), "HolidayDateTime", MatchType.LesserOrEqual, claimDate));
                    var totalNationalHoliday = _m_NationalHoliday.RetrieveByCriteria(criteria).OfType<NationalHoliday>().Count();

                    TimeSpan dateDiff = claimDate - cMasterATA.ATA;
                    int NrOfDays = (int)dateDiff.TotalDays;
                    if ((NrOfDays - totalNationalHoliday) > int.Parse(_config.Value))
                    {
                        result = "Silahkan cek dokumen surat ATA pada attachment dealer";
                        _return = false;
                    }
                }
            }
            else
            {
                result = "Kendaraan belum dilakukan input ATA.";
                _return = false;
            }

            return _return;
        }

        public static bool IsValidThisClaim(ChassisMasterClaimHeader obj, List<DocumentUpload> objDocs, ref string result)
        {
            bool _return = true;

            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ChassisMasterATA), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(ChassisMasterATA), "ChassisMaster.ID", MatchType.Exact, obj.ChassisMaster.ID));

            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterATA).ToString());
            var cmATA = _mapper.RetrieveByCriteria(criteria);
            AppConfig _config = new AppConfig();

            criteria = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "ChassisMasterClaim.MaxDayValidate"));
            var _m_AppConfig = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            _config = _m_AppConfig.RetrieveByCriteria(criteria).OfType<AppConfig>().FirstOrDefault();

            if (cmATA.Count > 0 && _config != null)
            {
                ChassisMasterATA cMasterATA = cmATA[0] as ChassisMasterATA;
                if (cMasterATA.ATA.ToString() == "01/01/1753 0:00:00")
                {
                    result = "Tanggal ATA harus diisi terlebih dahulu.";
                    _return = false;
                }
                else
                {
                    bool isContain = objDocs.Count(c => c.FileDescription.ToLower().Equals("surat ata")) > 0;
                    if (!isContain)
                    {
                        var _m_NationalHoliday = MapperFactory.GetInstance().GetMapper(typeof(NationalHoliday).ToString());
                        DateTime claimDate = obj.ClaimDate;

                        criteria = new CriteriaComposite(new Criteria(typeof(NationalHoliday), "RowStatus", MatchType.Exact, 0));
                        criteria.opAnd(new Criteria(typeof(NationalHoliday), "HolidayDateTime", MatchType.Exact, claimDate));
                        NationalHoliday _nationalHoliday = _m_NationalHoliday.RetrieveByCriteria(criteria).OfType<NationalHoliday>().FirstOrDefault();

                        if (_nationalHoliday != null)
                        {
                            claimDate = CommonFunction.AddNWorkingDay(claimDate, 1);
                        }

                        criteria = new CriteriaComposite(new Criteria(typeof(NationalHoliday), "RowStatus", MatchType.Exact, 0));
                        criteria.opAnd(new Criteria(typeof(NationalHoliday), "HolidayDateTime", MatchType.GreaterOrEqual, cMasterATA.ATA));
                        criteria.opAnd(new Criteria(typeof(NationalHoliday), "HolidayDateTime", MatchType.LesserOrEqual, claimDate));
                        var totalNationalHoliday = _m_NationalHoliday.RetrieveByCriteria(criteria).OfType<NationalHoliday>().Count();

                        TimeSpan dateDiff = claimDate - cMasterATA.ATA;
                        int NrOfDays = (int)dateDiff.TotalDays;
                        if ((NrOfDays - totalNationalHoliday) > int.Parse(_config.Value))
                        {
                            result = "Tanggal claim sudah lewat dari ketentuan. Silahkan melampirkan surat perbaikan tanggal ATA dengan remark 'Surat ATA'";
                            _return = false;
                        }
                    }
                }
            }
            else
            {
                result = "Kendaraan belum dilakukan input ATA.";
                _return = false;
            }

            return _return;
        }

        public static bool IsValidChassisReplacement(ChassisMasterClaimHeader obj, ref string result)
        {
            bool isValid = true;

            var _m_ChassisMasterClaimHeader = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterClaimHeader).ToString());
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(ChassisMasterClaimHeader), "ChassisNumberReplacement", MatchType.Exact, obj.ChassisNumberReplacement));
            criteria.opAnd(new Criteria(typeof(ChassisMasterClaimHeader), "StatusID", MatchType.Exact, (int)EnumCBUReturn.StatusClaim.Proses));

            var data = _m_ChassisMasterClaimHeader.RetrieveByCriteria(criteria);

            var _m_ChassisMaster = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            criteria = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, obj.ChassisNumberReplacement));

            var chassis = _m_ChassisMaster.RetrieveByCriteria(criteria);

            if (data.Count > 0)
            {
                var temp = (ChassisMasterClaimHeader)data[0];
                if (temp.ID != obj.ID)
                {
                    result = string.Format("Chassis Pengganti : {0} untuk claim {1} sedang dalam proses claim lain.", obj.ChassisNumberReplacement, obj.ClaimNumber);
                    isValid = false;
                }
            }
            else if (chassis.Count > 0)
            {
                result = string.Format("Chassis Pengganti : {0} sudah ada di dnet untuk claim {1}.", obj.ChassisNumberReplacement, obj.ClaimNumber);
                isValid = false;
            }

            return isValid;
        }

        private static bool IsValidThisDoc(int attachmentCount)
        {
            bool _return = true;

            AppConfig _config = new AppConfig();
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "ChassisMasterClaim.MaxDocCount"));
            var _m_AppConfig = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            _config = _m_AppConfig.RetrieveByCriteria(criteria).OfType<AppConfig>().FirstOrDefault();

            if (_config != null)
            {
                if (attachmentCount > int.Parse(_config.Value))
                {
                    _return = false;
                }
            }
            return _return;
        }

        private static string VerifyData(ChassisMasterClaimHeader obj, List<ChassisMasterClaimDetail> objDets, List<DocumentUpload> objDocs, bool isTempClaim)
        {
            string result = "";
            int id = 0;
            bool isDuplicate = obj.ChassisMaster == null ? false : IsDuplicateDataClaim(obj.ChassisMaster.ChassisNumber, ref id);

            if (obj.Dealer == null)
            {
                result = "Dealer Name belum dipilih.";
            }
            //else if (obj.PODestination == null && !obj.IsFromUpload)
            //{
            //    result = "Kode Destinasi Claim belum dipilih.";
            //}
            else if (obj.ChassisMaster == null)
            {
                result = "Nomor Chassis/DO belum dipilih atau tidak ditemukan.";
            }
            else if (!isTempClaim && !obj.IsFromUpload && (obj.ChassisPODestination == null || (obj.ChassisPODestination != null && obj.ChassisPODestination.Code.Equals("600001"))))
            {
                result = string.Format("Claim tidak diizinkan untuk Kode Destinasi {0} - {1}.", obj.ChassisPODestination.Code, obj.ChassisPODestination.Nama);
            }
            else if (!objDets.Any())
            {
                result = "Tipe Claim belum ditambahkan.";
            }
            else if (!objDocs.Any())
            {
                result = "Belum ada Dokumen lampiran.";
            }
            else if (isDuplicate && (obj.StatusID == (int)EnumCBUReturn.StatusClaim.Baru || obj.StatusID == (int)EnumCBUReturn.StatusClaim.Revisi) && obj.ID != id)
            {
                result = string.Format("Nomor Chassis : {0} sedang dalam proses claim.", obj.ChassisMaster.ChassisNumber);
            }
            else if (objDocs.Any() && !IsValidThisDoc(objDocs.Count))
            {
                result = "Maximal attachment melebihi ketentuan.";
            }

            return result;
        }

        private static bool IsTempClaim(ChassisMasterClaimHeader obj)
        {
            bool isTempClaim = false;
            if (obj.ChassisMaster != null)
            {
                var _m_ChassisMasterClaimTemporary = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterClaimDMSReminder).ToString());
                var _ChassisMasterClaimTemporary = _m_ChassisMasterClaimTemporary.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMasterClaimDMSReminder), "ChassisMaster.ID", obj.ChassisMaster.ID));
                isTempClaim = _ChassisMasterClaimTemporary.Count > 0;
            }

            return isTempClaim;
        }

        private static DataTable GetParameters(CBUReturnSendSAP objCBUReturnSendSAP, int invoiceKind = -1, bool isForValidate = false)
        {
            var _m_claimHeader = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterClaimHeader).ToString());
            List<int> chassisIds = objCBUReturnSendSAP.ChassisClaimHeaders.Cast<ChassisMasterClaimHeader>().Select(s => s.ChassisMaster.ID).ToList();
            ArrayList parameters = new ArrayList();
            DataTable dt;

            SqlParameter parameter = new SqlParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = string.Join(",", chassisIds.Select(s => s.ToString())),
                ParameterName = "@ChassisNumber"
            };
            parameters.Add(parameter);

            if (invoiceKind != -1){
                parameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Input,
                    Value = invoiceKind,
                    ParameterName = "@InvoiceKind"
                };
                parameters.Add(parameter);
            }

            if (!isForValidate)
                dt = _m_claimHeader.RetrieveDataSet(CBUReturnValidation.sp_CBUReturnGetBilingNumber, parameters).Tables[0];
            else
            {
                parameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Input,
                    Value = objCBUReturnSendSAP.CurrentStatusRetur,
                    ParameterName = "@StatusRetur"
                };
                parameters.Add(parameter);
                dt = _m_claimHeader.RetrieveDataSet(CBUReturnValidation.sp_CBUReturnValidateBilling, parameters).Tables[0];
            }

            return dt;
        }

        private static bool ValidateParameter(DataTable parameters, CBUReturnSendSAP objCBUReturnSendSAP, ref string message)
        {
            List<string> chassisNotValids = new List<string>();
            var chassisList = objCBUReturnSendSAP.ChassisClaimHeaders.Cast<ChassisMasterClaimHeader>().ToList();
            bool isValid;

            foreach (var chassis in chassisList)
            {
                isValid = false;
                foreach (DataRow dr in parameters.Rows)
                {
                    if (chassis.ChassisMaster.ChassisNumber.Equals(dr["ChassisDefect"].ToString()))
                    {
                        isValid = true;
                        break;
                    }
                }

                if (!isValid)
                    chassisNotValids.Add(chassis.ChassisMaster.ChassisNumber);
            }
            message = string.Format("No Billing chassis {0} belum tersedia di D-NET", string.Join(", ", chassisNotValids));
            return chassisNotValids.Count == 0;
        }
    }
}
