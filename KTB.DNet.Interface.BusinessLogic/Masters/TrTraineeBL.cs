#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : TrTrainee business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class TrTraineeBL : AbstractBusinessLogic, ITrTraineeBL
    {
        #region Variables
        private readonly IMapper _trtraineeMapper;
        private readonly IMapper _classTraineeMapper;
        private readonly IMapper _jobPositionMapper;
        private readonly IMapper _profileHeaderMapper;
        private readonly IMapper _profileDetailMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public TrTraineeBL()
        {
            _trtraineeMapper = MapperFactory.GetInstance().GetMapper(typeof(TrTrainee).ToString());
            _classTraineeMapper = MapperFactory.GetInstance().GetMapper(typeof(TrClassRegistration).ToString());
            _jobPositionMapper = MapperFactory.GetInstance().GetMapper(typeof(JobPosition).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _profileDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new TrTrainee
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<TrTraineeDto> Create(TrTraineeParameterDto objCreate)
        {
            #region Initialize
            var result = new ResponseBase<TrTraineeDto>();
            Dealer dealer = null;
            byte[] fileBytes = null;
            SalesmanHeader salesmanHeader = null;
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            #endregion

            try
            {
                isValid = ValidateStatus(0, objCreate.Status, ref validationResults);

                isValid = ValidateEnum(objCreate, ref validationResults);

                isValid = ValidateJobPosition(objCreate.JobPosition, ref validationResults);

                isValid = ValidateEducationLevel(objCreate.EducationLevel, ref validationResults);

                isValid = ValidateBirthDate(objCreate.BirthDate, ref validationResults);

                isValid = ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer);

                if (!string.IsNullOrEmpty(objCreate.SalesmanCode))
                {
                    isValid = ValidationHelper.ValidateSalesmanHeader(objCreate.SalesmanCode, objCreate.DealerCode, validationResults, ref salesmanHeader);
                }

                if (isValid && objCreate.PhotoFile != null && !string.IsNullOrEmpty(objCreate.PhotoFile.FileName))
                {
                    validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFile(objCreate.PhotoFile, _mapper, out fileBytes, FieldResource.ImageAttachment));
                }

                if (isValid && IsTraineeExist(objCreate))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.DataSiswa)));
                }

                isValid = validationResults.Count == 0;

                if (isValid)
                {
                    // insert a new chassis master pkt object
                    TrTrainee newTrTrainee = new TrTrainee();
                    newTrTrainee.CreatedTime = DateTime.Now;
                    newTrTrainee.Name = objCreate.Name;
                    newTrTrainee.BirthDate = objCreate.BirthDate;
                    newTrTrainee.EducationLevel = objCreate.EducationLevel;
                    newTrTrainee.Gender = objCreate.Gender;
                    newTrTrainee.JobPosition = objCreate.JobPosition;
                    newTrTrainee.Status = objCreate.Status.ToString();
                    newTrTrainee.ShirtSize = objCreate.ShirtSize;
                    newTrTrainee.StartWorkingDate = objCreate.StartWorkingDate;
                    newTrTrainee.SalesmanHeader = salesmanHeader;
                    newTrTrainee.Dealer = dealer;
                    newTrTrainee.NoKTP = objCreate.NoKTP;
                    newTrTrainee.Email = objCreate.Email;

                    if (SaveTraineePhoto(objCreate, fileBytes, validationResults, newTrTrainee))
                    {
                        var succeed = _trtraineeMapper.Insert(newTrTrainee, DNetUserName);

                        result.success = succeed > 0;
                        if (!result.success) ErrorMsgHelper.DataCorrupt(result.messages);

                        // return output ID
                        result._id = succeed;
                        result.total = 1;
                    }
                    else
                    {
                        return PopulateValidationError<TrTraineeDto>(validationResults, null);
                    }
                }
                else
                {
                    return PopulateValidationError<TrTraineeDto>(validationResults, null);
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

        /// <summary>
        /// Update TrTrainee
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<TrTraineeDto> Update(TrTraineeParameterDto objUpdate)
        {
            #region Initialize
            var result = new ResponseBase<TrTraineeDto>();
            Dealer dealer = null;
            byte[] fileBytes = null;
            SalesmanHeader salesmanHeader = null;
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            TrTrainee trTrainee = null;
            #endregion

            try
            {
                if (!ValidateTraineeClasses(objUpdate, ref result)) { return result; }

                isValid = ValidateStatus(objUpdate.ID, objUpdate.Status, ref validationResults);

                isValid = ValidateEnum(objUpdate, ref validationResults);

                isValid = ValidateJobPosition(objUpdate.JobPosition, ref validationResults);

                isValid = ValidateEducationLevel(objUpdate.EducationLevel, ref validationResults);

                isValid = ValidateBirthDate(objUpdate.BirthDate, ref validationResults);

                //change validate dealercode to validate dealer group
                //isValid = ValidationHelper.ValidateDealer(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer);
                isValid = ValidateDealerGroup(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer);

                // Remove validate salesman code for now
                //isValid = ValidationHelper.ValidateSalesmanHeader(objUpdate.SalesmanCode, objUpdate.DealerCode, validationResults, ref salesmanHeader);

                // For now, Check if Salesman Code is the same as ID
                if (!string.IsNullOrEmpty(objUpdate.SalesmanCode))
                {
                    int salesmanCode = 0;
                    if (Int32.TryParse(objUpdate.SalesmanCode, out salesmanCode))
                    {
                        if (salesmanCode != objUpdate.ID)
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
                }

                if (isValid && objUpdate.PhotoFile != null)
                {
                    validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFile(objUpdate.PhotoFile, _mapper, out fileBytes, FieldResource.ImageAttachment));
                }

                //if (isValid && IsFieldValidationUpdated(objUpdate) && IsTraineeExist(objUpdate))
                //{
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.DataSiswa)));
                //}

                isValid = validationResults.Count == 0;

                //if (isValid) { isValid = ValidationHelper.ValidateTrTrainee(objUpdate.ID, validationResults, ref trTrainee, objUpdate.DealerCode); }
                if (isValid) { isValid = ValidateTrTraineeGroup(objUpdate.ID, validationResults, ref trTrainee, objUpdate.DealerCode, dealer); }
                
                if (isValid)
                {
                    // update ChassisMasterPKT object
                    var updatedTrTrainee = trTrainee;
                    updatedTrTrainee.CreatedBy = trTrainee.CreatedBy;
                    updatedTrTrainee.LastUpdateTime = DateTime.Now;
                    updatedTrTrainee.Name = objUpdate.Name;
                    updatedTrTrainee.BirthDate = objUpdate.BirthDate;
                    updatedTrTrainee.Dealer = dealer;
                    updatedTrTrainee.EducationLevel = objUpdate.EducationLevel;
                    updatedTrTrainee.Gender = objUpdate.Gender;
                    updatedTrTrainee.JobPosition = objUpdate.JobPosition;
                    updatedTrTrainee.Status = objUpdate.Status.ToString();
                    updatedTrTrainee.ShirtSize = objUpdate.ShirtSize;
                    updatedTrTrainee.StartWorkingDate = objUpdate.StartWorkingDate;
                    updatedTrTrainee.NoKTP = objUpdate.NoKTP;
                    updatedTrTrainee.Email = objUpdate.Email;

                    if (SaveTraineePhoto(objUpdate, fileBytes, validationResults, updatedTrTrainee))
                    {
                        var succeed = (int)_trtraineeMapper.Update(updatedTrTrainee, DNetUserName);

                        result.success = succeed > 0;
                        if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                        // return output ID
                        result._id = objUpdate.ID;
                        result.total = 1;
                    }
                    else
                    {
                        return PopulateValidationError<TrTraineeDto>(validationResults, null);
                    }
                }
                else
                {
                    return PopulateValidationError<TrTraineeDto>(validationResults, null);
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

        /// <summary>
        /// Delete TrTrainee by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<TrTraineeDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Get TrTrainee by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<TrTraineeDto> GetById(int id)
        {
            return null;
        }

        /// <summary>
        /// Get TrTrainee by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<TrTraineeDto>> Read(TrTraineeFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(TrTrainee), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            var sortColl = new SortCollection();
            int totalRow = 0;
            var result = new ResponseBase<List<TrTraineeDto>>();

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(TrTrainee), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(TrTrainee), filterDto, sortColl);

                // get data
                var data = _trtraineeMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<TrTrainee>().ToList();
                    var listData = list.Select(item => _mapper.Map<TrTraineeDto>(item)).ToList();

                    result.lst = listData;
                    result.total = listData.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(TrTrainee), filterDto);
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
        #endregion

        #region Private Methods
        /// <summary>
        /// Save trtrainee photo
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="fileBytes"></param>
        /// <param name="validationResults"></param>
        /// <param name="newTrTrainee"></param>
        private static bool SaveTraineePhoto(TrTraineeParameterDto objCreate, byte[] fileBytes, List<DNetValidationResult> validationResults, TrTrainee newTrTrainee)
        {
            if (fileBytes != null)
            {
                // set photo
                newTrTrainee.Photo = fileBytes;

                // save the file
                string filePath;
                string uploadErrorMessage = FileUtility.SavePhotoFile(objCreate.PhotoFile, fileBytes, out filePath);
                if (!string.IsNullOrEmpty(uploadErrorMessage))
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgDataType, uploadErrorMessage)));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validate trainee classes
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool ValidateTraineeClasses(TrTraineeParameterDto objUpdate, ref ResponseBase<TrTraineeDto> result)
        {
            // get trainee existing class list if any
            var arlRegClassColl = GetRegOfTrainee(objUpdate);
            if (arlRegClassColl.Count >= 0)
            {
                foreach (var trClass in arlRegClassColl)
                {
                    var trReg = trClass as TrClassRegistration;

                    // check if any active class in different dealer
                    if (trReg.TrClass.FinishDate >= DateTime.Now && trReg.Dealer.DealerCode != this.DealerCode)
                    {
                        result.success = false;
                        result.messages.Add(new MessageBase { ErrorMessage = String.Format(MessageResource.ErrorMsgTraineeHasActiveClass, trReg.TrClass.ClassCode) });
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Status validation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateStatus(int id, short status, ref List<DNetValidationResult> validationResults)
        {
            if (id == 0 && status != 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Status)));
            }
            else if (id > 0 && (status != 1 && status != 2))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Status)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate enum value
        /// </summary>
        /// <param name="model"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateEnum(TrTraineeParameterDto model, ref List<DNetValidationResult> validationResults)
        {
            if (!_enumBL.IsExistByCategoryAndValue(".Gender", ((int)(model.Gender)).ToString())) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Gender))); }

            if (!_enumBL.IsExistByCategoryAndValue(".ShirtSize", ((int)(model.ShirtSize.ToInt())).ToString())) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.ShirtSize))); }

            if (!_enumBL.IsExistByCategoryAndValue(".TrTraineeStatus", ((int)(model.Status)).ToString())) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.StatusMechanic))); }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Job Position
        /// </summary>
        /// <param name="positionCode"></param>
        /// <returns></returns>
        private bool ValidateJobPosition(string positionCode, ref List<DNetValidationResult> validationResults)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(JobPosition), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(JobPosition), "Code", MatchType.Exact, positionCode));
            criterias.opAnd(new Criteria(typeof(JobPosition), "Category", MatchType.Exact, 2));

            var jobPositionList = _jobPositionMapper.RetrieveByCriteria(criterias);
            if (jobPositionList.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataCodeNotFound, FieldResource.JobPosition, positionCode)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate Trainee birthdate
        /// </summary>
        /// <param name="birthdate"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateBirthDate(DateTime birthdate, ref List<DNetValidationResult> validationResults)
        {
            if ((DateTime.Now.Year - birthdate.Year < 17) || DateTime.Now.Year < 1930)
            {
                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgAgeInvalid));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate education level
        /// </summary>
        /// <param name="educationLevel"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateEducationLevel(string educationLevel, ref List<DNetValidationResult> validationResults)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(ProfileHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ProfileHeader), "Code", MatchType.Exact, "PENDIDIKAN"));
            var profileHeaders = _profileHeaderMapper.RetrieveByCriteria(criterias);
            if (profileHeaders.Count > 0)
            {
                var profileHeader = (profileHeaders[0] as ProfileHeader);

                var profileCriterias = new CriteriaComposite(new Criteria(typeof(ProfileDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                profileCriterias.opAnd(new Criteria(typeof(ProfileDetail), "ProfileHeader.ID", MatchType.Exact, profileHeader.ID));
                var profileDetails = _profileDetailMapper.RetrieveByCriteria(profileCriterias);
                if (profileDetails.Count > 0)
                {
                    return true;
                }
            }

            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.EducationLevel)));

            return false;
        }

        /// <summary>
        /// Get trainee registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ArrayList GetRegOfTrainee(TrTraineeParameterDto model)
        {
            var criteriasTrainee = new CriteriaComposite(new Criteria(typeof(TrClassRegistration), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriasTrainee.opAnd(new Criteria(typeof(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, model.ID));
            criteriasTrainee.opAnd(new Criteria(typeof(TrClassRegistration), "Status", MatchType.Exact, EnumTrClassRegistration.DataStatusType.Register));

            var trClassRegs = _classTraineeMapper.RetrieveByCriteria(criteriasTrainee);

            return trClassRegs;
        }

        /// <summary>
        /// Validate update employee
        /// </summary>
        /// <param name="traineeModel"></param>
        /// <returns></returns>
        private bool IsFieldValidationUpdated(TrTraineeParameterDto traineeModel)
        {
            //TrTrainee trainee = null;
            //var validationResults = new List<DNetValidationResult>();

            //if (ValidationHelper.ValidateTrTrainee(traineeModel.ID, validationResults, ref trainee))
            //{
            //    var isChange = !traineeModel.Name.Equals(trainee.Name, StringComparison.OrdinalIgnoreCase) |
            //                    traineeModel.StartWorkingDate != trainee.StartWorkingDate |
            //                   !traineeModel.DealerCode.Equals(trainee.Dealer.DealerCode, StringComparison.OrdinalIgnoreCase) |
            //                    traineeModel.Status != Convert.ToInt16(trainee.Status) | 
            //                    traineeModel.BirthDate != trainee.BirthDate | 
            //                   !traineeModel.DealerBranchCode.Equals(trainee.DealerBranch.DealerBranchCode, StringComparison.OrdinalIgnoreCase) |
            //                   !traineeModel.Email.Equals(trainee.Email, StringComparison.OrdinalIgnoreCase) |
            //                    traineeModel.JobPosition != trainee.JobPosition |
            //                    traineeModel.NoKTP != trainee.NoKTP;

            //    return isChange;
            //}

            return true;
        }

        /// <summary>
        /// Check is trainee exist
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsTraineeExist(TrTraineeParameterDto model)
        {
            var criteriasTrainee = new CriteriaComposite(new Criteria(typeof(TrTrainee), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriasTrainee.opAnd(new Criteria(typeof(TrTrainee), "Dealer.DealerCode", MatchType.Exact, model.DealerCode));
            criteriasTrainee.opAnd(new Criteria(typeof(TrTrainee), "Name", MatchType.Exact, model.Name));
            criteriasTrainee.opAnd(new Criteria(typeof(TrTrainee), "StartWorkingDate", MatchType.GreaterOrEqual, GenerateDateCriteria(model.StartWorkingDate, true)));
            criteriasTrainee.opAnd(new Criteria(typeof(TrTrainee), "StartWorkingDate", MatchType.LesserOrEqual, GenerateDateCriteria(model.StartWorkingDate, false)));

            var trTrainees = _trtraineeMapper.RetrieveByCriteria(criteriasTrainee);
            if (trTrainees.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Generate Date Criteria
        /// </summary>
        /// <param name="nDate"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private DateTime GenerateDateCriteria(DateTime nDate, bool startDate)
        {
            int Hour;
            int Minute;
            int Second;

            if (startDate)
            {
                Hour = 0;
                Minute = 0;
                Second = 0;
            }
            else
            {
                Hour = 23;
                Minute = 59;
                Second = 59;
            }

            return new DateTime(nDate.Year, nDate.Month, nDate.Day, Hour, Minute, Second);
        }

        public static bool ValidateDealerGroup(string dealerCode, List<DNetValidationResult> validationResults, string loginDealerCode, ref Dealer dealer)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", dealerCode));
            if (masters.Count > 0)
            {
                // cast the object
                dealer = masters[0] as Dealer;
                var logindeaalers = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", loginDealerCode));
                Dealer logindealer = logindeaalers[0] as Dealer;

                // validate dealer code against login dealer code
                if (dealer.DealerGroup.ID != logindealer.DealerGroup.ID)
                {
                    validationResults.Add(new DNetValidationResult("Dealer group data : " + dealer.DealerGroup.GroupName + " berbeda dengan dealer group login : " + logindealer.DealerGroup.GroupName));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.DealerCode, dealerCode)));
            }

            return validationResults.Count == 0;
        }

        public static bool ValidateTrTraineeGroup(int traineeID, List<DNetValidationResult> validationResults, ref TrTrainee trTrainee, string dealerCode = null, Dealer dealer = null)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(TrTrainee).ToString());

            // get by criteria
            CriteriaComposite criteriaMekanik = new CriteriaComposite(new Criteria(typeof(TrTrainee), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriaMekanik.opAnd(new Criteria(typeof(TrTrainee), "ID", MatchType.Exact, traineeID));
            if (dealerCode != null)
            {
                //criteriaMekanik.opAnd(new Criteria(typeof(TrTrainee), "Dealer.DealerCode", MatchType.Exact, dealerCode));
                criteriaMekanik.opAnd(new Criteria(typeof(TrTrainee), "Dealer.DealerGroup.ID", MatchType.Exact, dealer.DealerGroup.ID));
            }

            var masters = _mapper.RetrieveByCriteria(criteriaMekanik);
            if (masters.Count > 0)
            {
                // cast the object
                trTrainee = masters[0] as TrTrainee;
            }
            else
            {
                if (dealerCode == null)
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.EmployeeService, traineeID)));
                else
                    //validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrence, FieldResource.EmployeeService, traineeID, dealerCode)));
                    validationResults.Add(new DNetValidationResult("TrTrainee ID " + trTrainee.ID + " tidak ditemukan pada dealer Group " + dealer.DealerGroup.GroupName));
            }

            return validationResults.Count == 0;
        }

        #endregion
    }
}