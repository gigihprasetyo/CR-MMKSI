#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : WarrantyActivation business logic class
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
using KTB.DNet.BusinessValidation;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class WarrantyActivationBL : AbstractBusinessLogic, IWarrantyActivationBL
    {
        private readonly IMapper _warrantyActivationMapper;
        private readonly IMapper _pdiMapper;
        private readonly IMapper _chassisMasterPKTMapper;
        private readonly IMapper _vWarrantyActivationMapper;
        private readonly IMapper _sPKChassisMapper;
        private readonly AutoMapper.IMapper _mapper;

        #region Constructor
        public WarrantyActivationBL()
        {
            _warrantyActivationMapper = MapperFactory.GetInstance().GetMapper(typeof(WarrantyActivation).ToString());
            _pdiMapper = MapperFactory.GetInstance().GetMapper(typeof(PDI).ToString());
            _chassisMasterPKTMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
            _vWarrantyActivationMapper = MapperFactory.GetInstance().GetMapper(typeof(V_WarrantyActivation).ToString());
            _sPKChassisMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKChassis).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        
        public ResponseBase<WarrantyActivationDto> Create(WarrantyActivationParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<WarrantyActivationDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            ChassisMaster chassisMaster = null;
            PDI pdi = null;
            ChassisMasterPKT chassisMasterPKT = null;

            #endregion

            try
            {
                isValid = ValidateWarrantyActivation(objCreate, validationResults, ref chassisMaster, ref pdi, ref chassisMasterPKT);

                if (isValid)
                {
                    WarrantyActivation obj = new WarrantyActivation
                    {
                        PDI = pdi,
                        ChassisMaster = chassisMaster,
                        //ChassisMasterPKT = chassisMasterPKT,
                        WADate = objCreate.RequestDate,
                        CustomerName = objCreate.CustomerName,
                        HandphoneNo = objCreate.HandphoneNo,
                        PlateNumber = objCreate.PlateNumber,
                        CreatedTime = DateTime.Now,
                        LastUpdatedBy = DNetUserName,
                        LastUpdatedTime = DateTime.Now,
                    };

                    if (chassisMasterPKT == null) obj.Status = (int)EnumWarrantyActivation.WAStatus.Proses;
                    else obj.Status = (int)EnumWarrantyActivation.WAStatus.Aktif;

                    if (objCreate.DigitalSignature != null && !string.IsNullOrEmpty(objCreate.DigitalSignature.FileName) && !string.IsNullOrEmpty(objCreate.DigitalSignature.Base64OfStream))
                    {
                        string filename = string.Empty;
                       PutFile(objCreate.ChassisNumber, objCreate.DigitalSignature.Base64OfStream, out filename);
                        obj.DSFilePath = filename;
                    }

                    var success = (int)_warrantyActivationMapper.Insert(obj, DNetUserName);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);

                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<WarrantyActivationDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<List<WarrantyActivationDto>> Read(WarrantyActivationFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<V_WarrantyActivationDto>> Read(V_WarrantyActivationFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(V_WarrantyActivation), "ID", MatchType.Greater, 0));
            var result = new ResponseBase<List<V_WarrantyActivationDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(V_WarrantyActivation), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(V_WarrantyActivation), filterDto, sortColl);

                // get data
                var data = _vWarrantyActivationMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<V_WarrantyActivation>().ToList();
                    List<V_WarrantyActivationDto> listData = list.ConvertList<V_WarrantyActivation, V_WarrantyActivationDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(V_WarrantyActivation), filterDto);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<WarrantyActivationDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<WarrantyActivationDto> Update(WarrantyActivationParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<WarrantyActivationDto> Delete(WarrantyActivationParameterDto paramDelete)
        {
            throw new NotImplementedException();
        }

        public FileStream GetFile(WarrantyActivationGetFileParameter parameter, out string filename)
        {
            filename = string.Empty;

            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(WarrantyActivation), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            crit.opAnd(new Criteria(typeof(WarrantyActivation), "ChassisMaster.ChassisNumber", MatchType.Exact, parameter.ChassisNumber.Trim()));
            crit.opAnd(new Criteria(typeof(WarrantyActivation), "ChassisMaster.RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            crit.opAnd(new Criteria(typeof(WarrantyActivation), "Status", MatchType.Exact, (int)EnumWarrantyActivation.WAStatus.Aktif));
            WarrantyActivation objWarrantyActivation = _warrantyActivationMapper.RetrieveByCriteria(crit).Cast<WarrantyActivation>().FirstOrDefault();

            if (objWarrantyActivation == null)
                return null;
            
            FileStream fs = null;
            bool isUpdate = false;
            List<ValidResult> validationResults = new List<ValidResult>();

            WarrantyActivationValidation waValidation = new WarrantyActivationValidation(AppConfigs.GetString("SAN"), AppConfigs.GetString("User"), AppConfigs.GetString("Password"), AppConfigs.GetString("WebServer"));
            if (waValidation.GenerateCertificate(objWarrantyActivation, ref isUpdate, ref filename, ref fs, validationResults, parameter.IsEncrypted))
            {
                if (isUpdate)
                {
                    objWarrantyActivation.FileName = filename;
                    _warrantyActivationMapper.Update(objWarrantyActivation, DNetUserName);
                    var arr = filename.Split('\\');
                    filename = arr[3];
                }
            }

            return fs;
        }

        #endregion

        #region Private Methods

        private bool ValidateWarrantyActivation(WarrantyActivationParameterDto objCreate, List<DNetValidationResult> validationResults, ref ChassisMaster chassisMaster, ref PDI pdi, ref ChassisMasterPKT chassisMasterPKT)
        {
            bool isValid = true;
            Dealer dealer = new Dealer();

            if (IsWarrantyActivationExist(objCreate))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.WarrantyActivation)));
            }
            else if (!IsValidPDIStatus(objCreate, ref pdi))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisPDINotSubmittedYet, objCreate.ChassisNumber)));
            }
            else if (!IsMatching(objCreate, ref dealer))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisNotMatch, objCreate.ChassisNumber)));
            }
            else if (!TCHelper.GetActiveTCResult(dealer.ID, (int)EnumDealerTransType.DealerTransKind.PilotingWA))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotDealerPilotting, dealer.DealerCode)));
            }
            else
            {
                chassisMaster = pdi.ChassisMaster;

                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(ChassisMasterPKT), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));

                chassisMasterPKT = _chassisMasterPKTMapper.RetrieveByCriteria(criterias).Cast<ChassisMasterPKT>().FirstOrDefault();
            }

            isValid = validationResults.Count == 0;

            return isValid;
        }

        private bool IsMatching(WarrantyActivationParameterDto objCreate, ref Dealer dealer)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(SPKChassis), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SPKChassis), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));
            criterias.opAnd(new Criteria(typeof(SPKChassis), "MatchingType", MatchType.Exact, 1));
            var result = _sPKChassisMapper.RetrieveByCriteria(criterias).Cast<SPKChassis>().ToList();

            if (result.Count == 0) return false;

            dealer = result.LastOrDefault().SPKDetail.SPKHeader.Dealer;
            return true;
        }

        private bool IsWarrantyActivationExist(WarrantyActivationParameterDto objCreate)
        {
            // Warranty Activation Validation         
            var criterias = new CriteriaComposite(new Criteria(typeof(WarrantyActivation), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(WarrantyActivation), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));

            var warrantyActivationResult = _warrantyActivationMapper.RetrieveByCriteria(criterias);
            if (warrantyActivationResult.Count == 0) return false;

            return true;
        }

        private bool IsValidPDIStatus(WarrantyActivationParameterDto objCreate, ref PDI pdi)
        {
            // PDI Status Validation         
            var criterias = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(PDI), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));
            criterias.opAnd(new Criteria(typeof(PDI), "PDIStatus", MatchType.InSet, string.Format("('{0}','{1}')", 
                        (int)EnumFSStatus.FSStatus.Proses, (int)EnumFSStatus.FSStatus.Selesai)));

            var pdiResult = _pdiMapper.RetrieveByCriteria(criterias);
            if (pdiResult.Count == 0) return false;

            pdi = pdiResult.Cast<PDI>().FirstOrDefault();

            return true;
        }

        private UserImpersonater GetImpersonater()
        {
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            return imp;
        }

        private void PutFile(string chassisNumber, string imgBase64String, out string filename)
        {
            UserImpersonater imp = GetImpersonater();
            filename = string.Empty;
            byte[] imageBytes = Convert.FromBase64String(imgBase64String);

            bool success = imp.Start();
            if (success)
            {
                Image img = null;

                using (MemoryStream ms1 = new MemoryStream(imageBytes))
                {
                    img = Image.FromStream(ms1);
                }

                if (img != null)
                {
                    string ext = new ImageFormatConverter().ConvertToString(img.RawFormat).ToLower();
                    DateTime now = DateTime.Now;
                    filename = string.Format("{0}_{1}.{2}", chassisNumber, Guid.NewGuid().ToString().Substring(0, 3), ext);
                    string path = string.Format(@"WarrantyActivation\{0}\{1}\", now.ToString("yyyy"), now.ToString("MM"));
                    string fullpath = Path.Combine(AppConfigs.GetString("SAN"), path);
                    if (!Directory.Exists(fullpath))
                    {
                        Directory.CreateDirectory(fullpath);
                    }

                    fullpath = Path.Combine(fullpath, filename);
                    filename = Path.Combine(path, filename);

                    File.WriteAllBytes(fullpath, imageBytes);
                }
            }

            imp.Stop();
        }

        #endregion
    }
}
