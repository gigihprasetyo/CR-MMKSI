using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KTB.DNet.BusinessValidation
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Validate passed dealer code parameter
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="loginDealerCode"></param>
        /// <param name="isValid"></param>
        /// <param name="dealer"></param>
        //+++++++++++++++++++++++++++++++++++++
        public static bool ValidateDealer(string dealerCode, List<ValidResult> validationResults, string loginDealerCode, ref Dealer dealer, bool isCompareToLoginDealerCode = true)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", dealerCode));
            if (masters.Count > 0)
            {
                // cast the object
                dealer = masters[0] as Dealer;

                // validate dealer code against login dealer code
                if (isCompareToLoginDealerCode && !dealerCode.Equals(loginDealerCode, System.StringComparison.OrdinalIgnoreCase))
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = "Dealer Code tidak sesuai dengan dealer code user login"
                    };
                    validationResults.Add(validResult);
                }
            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Dealer Code tidak valid"
                };
                validationResults.Add(validResult);
            }

            return validationResults.Count == 0;
        }


        /// <summary>
        /// Validate dealer branch code
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="dealerBranchCode"></param>
        /// <param name="dealerBranch"></param>
        /// <returns></returns>
        public static bool ValidateDealerBranch(string dealerCode, List<ValidResult> validationResults, string dealerBranchCode, ref DealerBranch dealerBranch)
        {
            // no need to validate
            if (string.IsNullOrEmpty(dealerBranchCode))
            {
                dealerBranch = null;
                return true;
            }

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(DealerBranch).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerBranch), "DealerBranchCode", "Status", dealerBranchCode, "1"));
            if (masters.Count > 0)
            {
                // cast the object
                dealerBranch = masters[0] as DealerBranch;

                // validate dealer branch against dealer
                if (!dealerBranch.Dealer.DealerCode.Equals(dealerCode, System.StringComparison.OrdinalIgnoreCase))
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = "Dealer Branch Code tidak sesuai dengan Dealer Code"
                    };
                    validationResults.Add(validResult);
                }
            }
            else
            {
                dealerBranch = null;
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Dealer Branch Code tidak valid dengan dealer code user login"
                };
                validationResults.Add(validResult);
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate FS Kind On Vehicle Type Code
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="validResultList"></param>
        /// <param name="fsKind"></param>
        /// <param name="serviceDate"></param>
        /// <returns></returns>
        public static bool ValidateFSKindOnVehicleType(ChassisMaster chassisMaster, List<ValidResult> validResultList, FSKind fsKind, DateTime serviceDate)
        {
            #region initialize variable
            var chassismaster = new ChassisMaster();
            var vehiclecolor = new VechileColor();
            var fskindonvehicletype = new FSKindOnVechileType();
            var mspMaster = new MSPMaster();
            #endregion

            #region initialize mapper
            var _chassisMaster = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            var _vehicleColor = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            var _fsKindOnVehicleType = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            var _chassismasterPKTMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
            #endregion

            //GET CHASSISMASTER 
            var criterias_chassis = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_chassis.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            var chassis = _chassisMaster.RetrieveByCriteria(criterias_chassis);
            if (chassis.Count > 0)
            {
                chassismaster = (ChassisMaster)chassis[0];
                //GET VEHICLE COLOR
                var criterias_vehiclecolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias_vehiclecolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, chassismaster.VechileColor.ID));
                var vehicleColor = _vehicleColor.RetrieveByCriteria(criterias_vehiclecolor);
                if (vehicleColor.Count > 0)
                {
                    vehiclecolor = (VechileColor)vehicleColor[0];
                    //GET FSKIND ON VEHICLE TYPE

                    var criterias_fskindonvehicletype = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    criterias_fskindonvehicletype.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKind.KindCode));
                    criterias_fskindonvehicletype.opAnd(new Criteria(typeof(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, vehiclecolor.VechileType.ID));
                    var fsKindOnVehicle = _fsKindOnVehicleType.RetrieveByCriteria(criterias_fskindonvehicletype);
                    if (fsKindOnVehicle.Count > 0)
                    {
                        fskindonvehicletype = (FSKindOnVechileType)fsKindOnVehicle[0];

                        //GET CHASSISMASTER PKT DATE
                        var chassismasterPKT = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                        chassismasterPKT.opAnd(new Criteria(typeof(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
                        var chassisPKTDate = _chassismasterPKTMapper.RetrieveByCriteria(chassismasterPKT);
                        if (chassisPKTDate.Count > 0)
                        {
                            //Centralize 
                            //var chassismasterpktdate = (ChassisMasterPKT)chassisPKTDate[0];

                            ////COMPARE PKT DATE + DURATION > SERVICE DATE
                            //var maxClaimDate = chassismasterpktdate.PKTDate.AddDays(fskindonvehicletype.Duration);
                            //if (serviceDate > maxClaimDate)
                            //{
                            //    var validResult = new ValidResult()
                            //    {
                            //        IsValid = false,
                            //        ErrorCode = 20009,
                            //        Message = string.Format("Tanggal PKT Date {0} dan Duration {1} melebihi Tanggal Service Date {2}", chassismasterpktdate.PKTDate.ToString("dd MMM yyyy"), fskindonvehicletype.Duration, serviceDate.ToString("dd MMM yyyy"))
                            //    };
                            //    validResultList.Add(validResult);
                            //    return false;
                            //}
                        }
                        else
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = string.Format("PKT Date dengan Chassis {0} tidak ditemukan", chassisMaster.ChassisNumber)
                            };
                            validResultList.Add(validResult);
                            return false;
                        }
                    }
                    else
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("PKT Date dengan Kind Code {0} dan Vehicle {1} tidak ditemukan", fsKind.KindCode, vehiclecolor.VechileType.VechileTypeCode)
                        };
                        validResultList.Add(validResult);
                        return false;
                    }
                }
                else
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Vehicle Color dengan Chassis {0} tidak ditemukan", chassisMaster.ChassisNumber)
                    };
                    validResultList.Add(validResult);
                    return false;
                }
            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Chassis {0} tidak ditemukan", chassisMaster.ChassisNumber)
                };
                validResultList.Add(validResult);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate visit type
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        public static bool ValidateVisitType(FreeService objCreate, List<ValidResult> validationResults)
        {
            var result = StandardCodeHelper.GetCharByCategoryAndValue("VisitType", objCreate.VisitType);
            if (result == null)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Visit Type tidak valid"
                };
                validationResults.Add(validResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate visit type
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        public static bool ValidateVisitType(FreeServiceBB objCreate, List<ValidResult> validationResults)
        {
            var result = StandardCodeHelper.GetCharByCategoryAndValue("VisitType", objCreate.VisitType);
            if (result == null)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Visit Type tidak valid"
                };
                validationResults.Add(validResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate chassis and engine
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="engineNumber"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateChassisAndEngine(string chassisNumber, string engineNumber, List<ValidResult> validationResults, ref ChassisMaster chassisMaster)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMaster), "ChassisNumber", "Category.ProductCategory.Code", chassisNumber, ConfigurationManager.AppSettings["CompanyCode"]));
            if (masters.Count == 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Nomor rangka {0} tidak ditemukan", chassisNumber)
                };
                validationResults.Add(validResult);
            }
            else
            {
                var tempCM = masters[0] as ChassisMaster;
                if (!tempCM.EngineNumber.Equals(engineNumber, StringComparison.OrdinalIgnoreCase))
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Nomor mesin {0} tidak cocok dengan nomor rangka {1}", engineNumber, chassisNumber)
                    };
                    validationResults.Add(validResult);
                }
                else
                {
                    chassisMaster = tempCM;
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate chassis and engine for Chassis Master BB
        /// </summary>
        /// <param name="paramDto"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        public static bool ValidateChassisAndEngineBB(FreeServiceBB paramDto, List<ValidResult> validationResults, ref ChassisMasterBB chassisMasterBB)
        {
            var _chassisMasterBBMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterBB).ToString());
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterBB), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(ChassisMasterBB), "ChassisNumber", MatchType.Exact, paramDto.ChassisMasterBB.ChassisNumber));

            ArrayList masters = _chassisMasterBBMapper.RetrieveByCriteria(criterias);
            if (masters.Count == 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Nomor rangka {0} tidak ditemukan", paramDto.ChassisMasterBB.ChassisNumber)
                };
                validationResults.Add(validResult);
            }
            else
            {
                var chassis = masters[0] as ChassisMasterBB;
                if (!chassis.EngineNumber.Equals(paramDto.ChassisMasterBB.EngineNumber, StringComparison.OrdinalIgnoreCase))
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Nomor mesin {0} tidak cocok dengan nomor rangka {1}", paramDto.ChassisMasterBB.EngineNumber, paramDto.ChassisMasterBB.ChassisNumber)
                    };
                    validationResults.Add(validResult);
                }
                else
                {
                    chassisMasterBB = chassis;
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate if valid for fs special
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool IsValidCMForFSSpecial(string chassisNumber, List<ValidResult> validationResults)
        {
            var _chassisMasterBBMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterBB).ToString());
            CriteriaComposite cCMBB = new CriteriaComposite(new Criteria(typeof(ChassisMasterBB), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            cCMBB.opAnd(new Criteria(typeof(ChassisMasterBB), "ChassisNumber", MatchType.Exact, chassisNumber));
            var aCMBBs = _chassisMasterBBMapper.RetrieveByCriteria(cCMBB);
            if (aCMBBs.Count == 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Chassis {0} tidak boleh Free Service Special", chassisNumber)
                };
                validationResults.Add(validResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate fleet
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="objFleetFaktur"></param>
        /// <returns></returns>
        public static bool ValidateFleetFaktur(FreeService objCreate, ref FleetFaktur objFleetFaktur)
        {
            var _fleetFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(FleetFaktur).ToString());

            CriteriaComposite critFleetFaktur = new CriteriaComposite(new Criteria(typeof(FleetFaktur), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critFleetFaktur.opAnd(new Criteria(typeof(FleetFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisMaster.ChassisNumber));
            ArrayList arrFleetFaktur = _fleetFakturMapper.RetrieveByCriteria(critFleetFaktur);
            if (arrFleetFaktur.Count > 0)
            {
                objFleetFaktur = arrFleetFaktur[0] as FleetFaktur;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validate fs kind
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="fsKind"></param>
        /// <param name="isFleetExists"></param>
        /// <returns></returns>
        public static bool ValidateFSKindCode(FreeService objCreate, List<ValidResult> validationResults, ref FSKind fsKind, bool isFleetExists)
        {
            var _fsKindMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKind).ToString());
            // validasi km ke 4 dan validasi jenis FS (FSKInd)
            CriteriaComposite critFS = new CriteriaComposite(new Criteria(typeof(FSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critFS.opAnd(new Criteria(typeof(FSKind), "KindCode", MatchType.Exact, objCreate.FSKind.KindCode));

            ArrayList fsKinds = _fsKindMapper.RetrieveByCriteria(critFS);
            if (fsKinds.Count > 0)
            {
                fsKind = fsKinds[0] as FSKind;
                if (objCreate.MileAge > fsKind.KM && !isFleetExists)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = "Jarak tempuh melampaui batas jenis free service"
                    };
                    validationResults.Add(validResult);
                }
            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Kode Jenis Free Servis tidak terdaftar !")
                };
                validationResults.Add(validResult);
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate fs kind bb
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="fsKind"></param>
        /// <returns></returns>
        public static bool ValidateFSKindBB(FreeServiceBB objCreate, List<ValidResult> validationResults, ref FSKind fsKind)
        {
            var _fsKindMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKind).ToString());
            // validasi km ke 4 dan validasi jenis FS (FSKInd)
            CriteriaComposite critFS = new CriteriaComposite(new Criteria(typeof(FSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critFS.opAnd(new Criteria(typeof(FSKind), "KindCode", MatchType.Exact, objCreate.FSKind.KindCode));
            ArrayList fsKinds = _fsKindMapper.RetrieveByCriteria(critFS);
            if (fsKinds.Count > 0)
            {
                fsKind = fsKinds[0] as FSKind;
                if (objCreate.MileAge > fsKind.KM)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = "Jarak tempuh melampaui batas jenis free service"
                    };
                    validationResults.Add(validResult);
                }
                else
                {
                    CriteriaComposite crtFS = new CriteriaComposite(new Criteria(typeof(FSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    crtFS.opAnd(new Criteria(typeof(FSKind), "KM", MatchType.Lesser, fsKind.KM));
                    SortCollection srtFS = new SortCollection();
                    srtFS.Add(new Sort(typeof(FSKind), "KM", Sort.SortDirection.DESC));

                    var arlFS = _fsKindMapper.RetrieveByCriteria(crtFS, srtFS);
                    int MinValue = 0;
                    if (arlFS.Count > 0)
                    {
                        MinValue = (arlFS[0] as FSKind).KM + 1;
                    }

                    if (objCreate.MileAge < MinValue)
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Jarak tempuh tidak sesuai dengan batas jenis free service"
                        };
                        validationResults.Add(validResult);
                    }
                }
            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Kode Jenis Free Servis tidak terdaftar !"
                };
                validationResults.Add(validResult);
            }

            return validationResults.Count == 0;
        }
        /// <summary>
        /// Validate if already PM
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="fsType"></param>
        /// <param name="isValid"></param>
        public static bool ValidateAlreadyPM(FreeService objCreate, List<ValidResult> validationResults, string fsType)
        {
            var _pmHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(PMHeader).ToString());
            int _fsType;
            int.TryParse(fsType, out _fsType);
            if (_fsType == 2)
            {
                ArrayList arlPMHeader;
                CriteriaComposite criteriasPMHeader = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criteriasPMHeader.opAnd(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisMaster.ChassisNumber));

                arlPMHeader = _pmHeaderMapper.RetrieveByCriteria(criteriasPMHeader);
                if (arlPMHeader.Count == 0)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Data nomor rangka {0} dengan FSType 2 belum ada di tabel PMHeader", objCreate.ChassisMaster.ChassisNumber)
                    };
                    validationResults.Add(validResult);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validate service date
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isBB"></param>
        /// <returns></returns>
        public static bool ValidateServiceDate(DateTime serviceDate, List<ValidResult> validationResults)
        {
            if ((serviceDate <= System.Data.SqlTypes.SqlDateTime.MinValue.Value) || (serviceDate >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value))
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Format tanggal {0}  {1} salah. Tanggal harus dalam format ddmmyyyy.", "Tanggal Service", serviceDate)
                };
                validationResults.Add(validResult);
            }

            if (serviceDate.Date > DateTime.Now.Date)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Tanggal Service {0} melebihi hari ini {1}", serviceDate, DateTime.Now.ToString("ddMMyyyy"))
                };
                validationResults.Add(validResult);
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// validate fs dealer
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMasterID"></param>
        /// <param name="soldDealer"></param>
        /// <param name="fsDealer"></param>
        public static bool ValidateFSDealer(string dealerCode, List<ValidResult> validationResults, ChassisMaster chassisMaster, Dealer soldDealer, ref Dealer fsDealer, ref bool isSoldSameWithFSDealer)
        {
            var _pdiMapper = MapperFactory.GetInstance().GetMapper(typeof(PDI).ToString());
            // validate dealer code
            var isValid = false;
            // check if sold dealer and free service dealer is the same
            if (soldDealer.DealerCode == fsDealer.DealerCode)
            {
                CriteriaComposite critIsPDI = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                critIsPDI.opAnd(new Criteria(typeof(PDI), "PDIStatus", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("EnumFSStatus.FSStatus", "Selesai").ValueId));
                critIsPDI.opAnd(new Criteria(typeof(PDI), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
                ArrayList pdis = _pdiMapper.RetrieveByCriteria(critIsPDI);
                if (pdis.Count == 0)
                {
                    // klo sudah PDI boleh insert
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("No. Rangka {0} belum PDI", chassisMaster.ChassisNumber)
                    };
                    validationResults.Add(validResult);
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }

                isSoldSameWithFSDealer = true;
            }
            else
            {
                isValid = true;

                isSoldSameWithFSDealer = false;
            }

            return isValid;
        }

        /// <summary>
        /// Validate sold date
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        public static bool ValidateSoldDate(FreeService objCreate, ValidResult validResult, DateTime pKTDate)
        {
            if (pKTDate > objCreate.ServiceDate)
            {
                validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Tanggal Penjualan {0} melebihi Tanggal Service {1}", pKTDate, objCreate.ServiceDate)
                };
                return false;
            }

            return true;
        }
        /// <summary>
        /// Validate sold date
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        public static bool ValidateSoldDate(FreeServiceBB objCreate, List<ValidResult> validationResults)
        {
            if (objCreate.SoldDate > objCreate.ServiceDate)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Tanggal Penjualan {0} melebihi Tanggal Service {1}", objCreate.SoldDate, objCreate.ServiceDate)
                };
                validationResults.Add(validResult);
            }

            return validationResults.Count == 0;
        }
        /// <summary>
        /// Validate for Claims in the Free service nothing more than MSPExRegistration
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="fsKind"></param>
        /// <param name="validResultList"></param>
        /// <returns></returns>
        public static bool IsAllowToInsertMSPEx(int mileage, ChassisMaster chassisMaster, FSKind fsKind, List<ValidResult> validResultList, DateTime serviceDate)
        {
            #region initialize variable
            bool vReturn = true;
            var registration_count = 0;
            var freeservice_count = 0;
            var mspexKM = 0;
            var isDiffReg = false;
            var lastMSPExKM = 0;
            var id_last = 0;
            var mspextypemappingtofskind = new MSPExMappingtoFSKind();
            #endregion


            #region initialize mapper
            var _chassisMaster = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            var _mspExRegis = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            var _freeService = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var _mspexmastermapper = MapperFactory.GetInstance().GetMapper(typeof(MSPExMaster).ToString());
            var _fsKindOnVehicleType = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            var _mspExMappingToFsKind = MapperFactory.GetInstance().GetMapper(typeof(MSPExMappingtoFSKind).ToString());

            #endregion

            #region CHECKING MSPEX ON MAPPING TO FSKIND
            var criterias_mspexmapping = new CriteriaComposite(new Criteria(typeof(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_mspexmapping.opAnd(new Criteria(typeof(MSPExMappingtoFSKind), "FSKind.KindCode", MatchType.Exact, fsKind.KindCode));
            var mspExMapping = _mspExMappingToFsKind.RetrieveByCriteria(criterias_mspexmapping);
            if (mspExMapping.Count > 0)
            {
                mspextypemappingtofskind = (MSPExMappingtoFSKind)mspExMapping[0];
            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("KindCode {0} belum ada pada config MSPExMappingtoFSKind", fsKind.KindCode)
                };
                validResultList.Add(validResult);
                return false;
            }
            #endregion CHECKING MSPEX ON MAPPING TO FSKIND

            //COUNT REGISTRATION
            var created_date = DateTime.Now;
            var validdateto_notexpired = DateTime.Now;
            var criterias_registration = new CriteriaComposite(new Criteria(typeof(MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId));
            criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "ValidDateTo", MatchType.GreaterOrEqual, serviceDate));
            criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "MSPExMaster.MSPExType.ID", MatchType.Exact, mspextypemappingtofskind.MSPExType.ID));
            var data = _mspExRegis.RetrieveByCriteria(criterias_registration);
            if (data.Count == 1)
            {
                var regis_mspex = data[0] as MSPExRegistration;
                if (mileage > regis_mspex.ValidKMTo)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Program dengan No. Rangka {0} dan jenis servis {1} tidak ditemukan atau telah expired", chassisMaster.ChassisNumber, fsKind.ID)
                    };
                    validResultList.Add(validResult);
                    vReturn = false;
                }
                else
                {
                    registration_count = data.Count;
                    created_date = regis_mspex.CreatedTime;
                    validdateto_notexpired = regis_mspex.ValidDateTo;
                }
            }
            else if (data.Count > 1)
            {
                foreach (var i in data)
                {
                    var mspexregis = i as MSPExRegistration;
                    if (mspexregis.ValidDateTo > DateTime.Now)
                    {
                        if (mileage <= mspexregis.ValidKMTo)
                        {
                            registration_count += 1;
                            if (mspexregis.ValidDateTo > validdateto_notexpired)
                            {
                                created_date = mspexregis.CreatedTime;
                                validdateto_notexpired = mspexregis.ValidDateTo;
                                id_last = mspexregis.ID;
                            }
                        }
                    }
                    var criterias_mspexmaster = new CriteriaComposite(new Criteria(typeof(MSPExMaster), "ID", MatchType.Exact, mspexregis.MSPExMaster.ID));
                    var mspexmaster = _mspexmastermapper.RetrieveByCriteria(criterias_mspexmaster);
                    var mastermspex = mspexmaster[0] as MSPExMaster;
                    if (mspexKM == 0)
                    {
                        mspexKM = mastermspex.MSPExKM;
                    }
                    else
                    {
                        if (mspexKM != mastermspex.MSPExKM)
                        {
                            isDiffReg = true;
                        }
                    }
                    if (mspexregis.ID == id_last && id_last != 0)
                    {
                        lastMSPExKM = mastermspex.MSPExKM;
                    }
                }

            }


            //COUNT FREE SERVICE
            var criterias_freeservice = new CriteriaComposite(new Criteria(typeof(FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_freeservice.opAnd(new Criteria(typeof(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            criterias_freeservice.opAnd(new Criteria(typeof(FreeService), "FSKind.KindCode", MatchType.Exact, fsKind.KindCode));
            var freeService = _freeService.RetrieveByCriteria(criterias_freeservice);
            if (freeService.Count > 0)
            {
                foreach (var dataFS in freeService)
                {
                    var freeservice = (FreeService)dataFS;
                    var criterias_fskindonvehicletype = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    criterias_fskindonvehicletype.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, freeservice.FSKind.ID));
                    var fsKindOnVehicleType = _fsKindOnVehicleType.RetrieveByCriteria(criterias_fskindonvehicletype);
                    if (fsKindOnVehicleType.Count > 0)
                    {
                        if (data.Count > 1)
                        {
                            if (isDiffReg)
                            {
                                freeservice_count += 1;
                            }
                            else
                            {
                                if (freeservice.ServiceDate >= created_date && freeservice.ServiceDate <= validdateto_notexpired)
                                {
                                    if (freeService.Count >= data.Count)
                                    {
                                        var validResult = new ValidResult()
                                        {
                                            IsValid = false,
                                            ErrorCode = 20009,
                                            Message = string.Format("No. Rangka {0} dengan jenis servis {1} tidak dapat di claim", chassisMaster.ChassisNumber, fsKind.ID)
                                        };
                                        validResultList.Add(validResult);
                                        vReturn = false;
                                    }
                                }
                            }

                        }
                        else if (data.Count == 1)
                        {
                            if (freeservice.ServiceDate >= created_date && freeservice.ServiceDate <= validdateto_notexpired)
                            {
                                freeservice_count += 1;
                            }
                        }

                    }

                }
            }
            else
            {
                if (isDiffReg)
                {
                    var fskindawal = (fsKind.KindCode).Substring(0, 1);
                    var mspexkmawal = lastMSPExKM.ToString().Substring(0, 1);
                    if (!fskindawal.Contains(mspexkmawal))
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("No. Rangka {0} dengan jenis servis {1} tidak dapat di claim", chassisMaster.ChassisNumber, fsKind.ID)
                        };
                        validResultList.Add(validResult);
                        vReturn = false;
                    }
                }
            }

            if (freeservice_count >= registration_count)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Chassis Number {0} dengan kode Jenis Free Servis {1} telah digunakan, harap pilih kode jenis free servis lain", chassisMaster.ChassisNumber, fsKind.KindCode)
                };
                validResultList.Add(validResult);
                vReturn = false;
            }

            return vReturn;
        }

        /// <summary>
        /// Validate to prevent duplicate key
        /// </summary>
        /// <param name="ChassisID"></param>
        /// <param name="FSKindID"></param>
        /// <returns></returns>
        public static bool IsExistCodeForInsertMSPEx(int mileage, ChassisMaster chassisMaster, int FSKindID, List<ValidResult> validationResults)
        {
            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var _mspexRegisMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            var isNotExpired = false;
            var totalnotexpired = 0;
            var totalfs = 0;
            var created_date = DateTime.Now;
            var validdateto_notexpired = DateTime.Now;
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "FSKind.ID", MatchType.Exact, FSKindID));
            var data = _freeServiceMapper.RetrieveByCriteria(criterias);
            if (data.Count > 0)
            {

                CriteriaComposite criteriasmspex = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criteriasmspex.opAnd(new Criteria(typeof(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
                criteriasmspex.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId));
                criteriasmspex.opAnd(new Criteria(typeof(MSPExRegistration), "ValidDateTo", MatchType.GreaterOrEqual, DateTime.Now));
                var mspexregisList = _mspexRegisMapper.RetrieveByCriteria(criteriasmspex).Cast<MSPExRegistration>().ToList();

                if (mspexregisList.Count > 1)
                {

                    foreach (var mspexlist in mspexregisList)
                    {
                        if (mspexlist.ValidDateTo > DateTime.Now)
                        {
                            if (mileage <= mspexlist.ValidKMTo)
                            {
                                isNotExpired = true;
                                totalnotexpired += 1;
                                if (mspexlist.ValidDateTo > validdateto_notexpired)
                                {
                                    created_date = mspexlist.CreatedTime;
                                    validdateto_notexpired = mspexlist.ValidDateTo;
                                }

                            }
                        }
                    }

                    if (isNotExpired == false)
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("No. Rangka {0} dengan jenis servis {1} sudah ada", chassisMaster.ChassisNumber, FSKindID)
                        };
                        validationResults.Add(validResult);
                        return false;
                    }
                    else if (isNotExpired == true)
                    {
                        foreach (var fs in data.Cast<FreeService>().ToList())
                        {
                            if (fs.ServiceDate >= created_date && fs.ServiceDate <= validdateto_notexpired)
                            {
                                if (data.Count >= mspexregisList.Count)
                                {
                                    var validResult = new ValidResult()
                                    {
                                        IsValid = false,
                                        ErrorCode = 20009,
                                        Message = string.Format("No. Rangka {0} dengan jenis servis {1} tidak dapat di claim", chassisMaster.ChassisNumber, fs.FSKind.ID)
                                    };
                                    validationResults.Add(validResult);
                                    return false;
                                }
                            }
                        }
                        if (totalfs >= totalnotexpired)
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = string.Format("Program dengan No. Rangka {0} dan jenis servis {1} tidak ditemukan atau telah expired", chassisMaster.ChassisNumber, FSKindID)
                            };
                            validationResults.Add(validResult);
                            return false;
                        }
                    }

                }
                else if (mspexregisList.Count == 1)
                {
                    var mspexregis = mspexregisList[0] as MSPExRegistration;
                    if (mileage <= mspexregis.ValidKMTo)
                    {
                        foreach (var i in data.Cast<FreeService>().ToList())
                        {
                            if (i.ServiceDate >= mspexregis.CreatedTime && i.ServiceDate <= mspexregis.ValidDateTo)
                            {
                                var validResult = new ValidResult()
                                {
                                    IsValid = false,
                                    ErrorCode = 20009,
                                    Message = string.Format("No. Rangka {0} dengan jenis servis {1} sudah ada", chassisMaster.ChassisNumber, FSKindID)
                                };
                                validationResults.Add(validResult);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("No. Rangka {0} dengan jenis servis {1} sudah ada", chassisMaster.ChassisNumber, FSKindID)
                    };
                    validationResults.Add(validResult);
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// Validate to prevent duplicate key
        /// </summary>
        /// <param name="ChassisID"></param>
        /// <param name="FSKindID"></param>
        /// <returns></returns>
        public static bool IsExistCodeForInsert(ChassisMaster chassisMaster, int FSKindID, List<ValidResult> validationResults)
        {
            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "FSKind.ID", MatchType.Exact, FSKindID));
            if (_freeServiceMapper.RetrieveByCriteria(criterias).Count > 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("No. Rangka {0} dengan jenis servis {1} sudah ada", chassisMaster.ChassisNumber, FSKindID)
                };
                validationResults.Add(validResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// to validate fs request can't more then 1x (by chassis number & service date)
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="serviceDate"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool IsExistMoreThan1xInAMonth(ChassisMaster chassisMaster, DateTime serviceDate, List<ValidResult> validationResults)
        {
            var firstDayOfMonth = new DateTime(serviceDate.Year, serviceDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.GreaterOrEqual, firstDayOfMonth));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.LesserOrEqual, lastDayOfMonth));
            if (_freeServiceMapper.RetrieveByCriteria(criterias).Count > 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("No. Rangka {0} sudah pernah claim pada bulan servis {1}", chassisMaster.ChassisNumber, serviceDate.ToString("MMMM yyyy"))
                };
                validationResults.Add(validResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// to validate fs request can't more then 1x (by chassis vehicle type & service date)
        /// PC > 30 days & lcv > 14 days
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="serviceDate"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool IsExistMoreThan1xByVehicleCategory(ChassisMaster chassisMaster, DateTime serviceDate, List<ValidResult> validationResults)
        {
            //var firstDayOfMonth = new DateTime(serviceDate.Year, serviceDate.Month, 1);
            //var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            string vehicleCategory = chassisMaster.Category.CategoryCode;
            double duration = 0;
            if (vehicleCategory.ToLower() == "pc")
            {
                duration = -30.0;
            }
            else if (vehicleCategory.ToLower() == "lcv")
            {
                duration = -14.0;
            }

            var startDate = serviceDate.AddDays(duration);

            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.GreaterOrEqual, startDate));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.LesserOrEqual, serviceDate));
            var fs = _freeServiceMapper.RetrieveByCriteria(criterias);
            if (fs.Count > 0)
            {
                FreeService dt = (FreeService)fs[0];
                var validResult = new ValidResult();
                validResult.IsValid = false;
                validResult.ErrorCode = 20009;

                if (duration == -30.0)
                {
                    validResult.Message = string.Format("Chassis {0} kategori PC. Tanggal Service kurang dari 30 Hari dari service sebelumnya.", chassisMaster.ChassisNumber);
                }
                else if (duration == -14.0)
                {
                    validResult.Message = string.Format("Chassis {0} kategori LCV. Tanggal Service kurang dari 14 Hari dari service sebelumnya.", chassisMaster.ChassisNumber);
                }
                else
                {
                    validResult.Message = string.Format("No. Rangka {0} sudah pernah claim pada tanggal {1}", chassisMaster.ChassisNumber, dt.ServiceDate.ToString("dd MMM yyyy"));
                }

                validationResults.Add(validResult);
                return false;
            }

            return true;
        }


        /// <summary>
        /// Validate chassis and fskind code
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isFleetExists"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="fsDealer"></param>
        /// <param name="fsKind"></param>
        public static bool ValidateChassisAndKindCode(FreeService objCreate, List<ValidResult> validationResults, bool isFleetExists, ChassisMaster chassisMaster, Dealer fsDealer, FSKind fsKind)
        {
            if (fsKind.KindCode.Length == 1)
            {
                if (objCreate.FSKind.KindCode == "1" || objCreate.FSKind.KindCode == "2")
                {
                    return true;
                }
            }

            bool isAllowed = IsChassisAllowed(objCreate.ChassisMaster.ChassisNumber) || isFleetExists || IsAllowFreeService(chassisMaster, fsKind, fsDealer.DealerCode);
            if (!isAllowed)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Simpan gagal, Chassis  {0} tidak mendapatkan kupon Free Service", objCreate.ChassisMaster.ChassisNumber)
                };
                validationResults.Add(validResult);
            }

            return validationResults.Count == 0;
        }


        /// <summary>
        /// Validate to prevent duplicate key
        /// </summary>
        /// <param name="ChassisID"></param>
        /// <param name="FSKindID"></param>
        /// <returns></returns>
        public static bool ValidateFreeServiceBBDuplicate(ChassisMasterBB chassisMasterBB, int FSKindID, List<ValidResult> validationResults)
        {
            var _freeServiceBBMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeServiceBB).ToString());
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeServiceBB), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeServiceBB), "ChassisMasterBB.ID", MatchType.Exact, chassisMasterBB.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeServiceBB), "FSKind.ID", MatchType.Exact, FSKindID));
            if (_freeServiceBBMapper.RetrieveByCriteria(criterias).Count > 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("No. Rangka {0} dengan jenis servis {1} sudah ada", chassisMasterBB.ChassisNumber, FSKindID)
                };
                validationResults.Add(validResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate FS Kind Code Leading Digit
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="fsKindCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateFSKindCodeLeadingDigit(string chassisNumber, string fsKindCode, List<ValidResult> validationResults)
        {
            var fsMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var critFS = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            critFS.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));

            var freeServiceList = fsMapper.RetrieveByCriteria(critFS).Cast<FreeService>().ToList();

            foreach (var freeService in freeServiceList)
            {
                if (freeService.FSKind != null)
                {
                    if (Regex.Replace(freeService.FSKind.KindCode, "[^0-9]", "") == Regex.Replace(fsKindCode, "[^0-9]", ""))
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("Anda tidak bisa memilih kode free service {0} karena telah menggunakan kode free service {1}", fsKindCode, freeService.FSKind.KindCode)
                        };
                        validationResults.Add(validResult);
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// Validate FS MSP Ex Kind Code Leading Digit
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="fsKindCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateFSMSPExKindCodeLeadingDigit(int mileage, string chassisNumber, string fsKindCode, List<ValidResult> validationResults)
        {
            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var _mspexRegisMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            var freeservice_claim = 0;
            var isNotExpired = false;
            var totalnotexpired_regis = 0;
            var totalfs = 0;
            var created_date = DateTime.Now;
            var validdateto_notexpired = DateTime.Now;

            var fsMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var critFS = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            critFS.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));

            var freeServiceList = fsMapper.RetrieveByCriteria(critFS).Cast<FreeService>().ToList();

            foreach (var freeService in freeServiceList)
            {
                if (freeService.FSKind != null)
                {
                    if (Regex.Replace(freeService.FSKind.KindCode, "[^0-9]", "") == Regex.Replace(fsKindCode, "[^0-9]", ""))
                    {
                        CriteriaComposite criteriasmspex = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                        criteriasmspex.opAnd(new Criteria(typeof(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));
                        criteriasmspex.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId));
                        criteriasmspex.opAnd(new Criteria(typeof(MSPExRegistration), "ValidDateTo", MatchType.GreaterOrEqual, DateTime.Now));

                        var mspexregisList = _mspexRegisMapper.RetrieveByCriteria(criteriasmspex).Cast<MSPExRegistration>().ToList();
                        if (isNotExpired == false)
                        {
                            foreach (var mspexlist in mspexregisList)
                            {
                                if (mspexlist.ValidDateTo > DateTime.Now)
                                {
                                    if (mileage <= mspexlist.ValidKMTo)
                                    {
                                        isNotExpired = true;
                                        totalnotexpired_regis += 1;
                                        if (mspexlist.ValidDateTo > validdateto_notexpired)
                                        {
                                            created_date = mspexlist.CreatedTime;
                                            validdateto_notexpired = mspexlist.ValidDateTo;
                                        }

                                    }
                                }
                            }
                        }
                        if (isNotExpired == false)
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = string.Format("Anda tidak bisa memilih kode free service {0} karena telah menggunakan kode free service {1}", fsKindCode, freeService.FSKind.KindCode)
                            };
                            validationResults.Add(validResult);
                            return false;
                        }
                        else if (isNotExpired == true)
                        {

                            if (freeService.ServiceDate >= created_date && freeService.ServiceDate <= validdateto_notexpired)
                            {
                                freeservice_claim += 1;
                                if (freeservice_claim >= mspexregisList.Count)
                                {
                                    totalfs = freeServiceList.Count;
                                    var validResult = new ValidResult()
                                    {
                                        IsValid = false,
                                        ErrorCode = 20009,
                                        Message = string.Format("Anda tidak bisa memilih kode free service {0} karena telah menggunakan kode free service {1}", fsKindCode, freeService.FSKind.KindCode)
                                    };
                                    validationResults.Add(validResult);
                                    return false;
                                }
                            }

                        }
                    }
                }
            }
            if (totalnotexpired_regis != 0 && totalfs != 0)
            {
                if (totalfs >= totalnotexpired_regis)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Anda tidak bisa memilih kode free service {0} karena telah menggunakan kode free service {1}", fsKindCode, fsKindCode)
                    };
                    validationResults.Add(validResult);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get Date From DB for Default 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDefaultFakturDateTime()
        {
            var appConfigMapper = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());

            int yearDuration = 2019;
            int monthDuration = 9;
            int dayDuration = 1;
            var critAppConf = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            critAppConf.opAnd(new Criteria(typeof(KTB.DNet.Domain.AppConfig), "Name", MatchType.InSet, "(" + "'YearFSDurationValidation','MonthFSDurationValidation','DayFSDurationValidation'" + ")"));
            var sortAppConf = new SortCollection();
            sortAppConf.Add(new Sort(typeof(AppConfig), "Name", Sort.SortDirection.ASC));

            var appConfigList = appConfigMapper.RetrieveByCriteria(critAppConf, sortAppConf).Cast<AppConfig>().ToList();
            if (appConfigList.Count > 0)
            {
                dayDuration = Convert.ToInt32(appConfigList[0].Value);
                monthDuration = Convert.ToInt32(appConfigList[1].Value);
                yearDuration = Convert.ToInt32(appConfigList[2].Value);
            }

            return new DateTime(yearDuration, monthDuration, dayDuration);
        }

        /// <summary>
        /// Get Date From Faktur
        /// </summary>
        /// <param name="endCustomerData"></param>
        /// <returns>
        /// Boolean Data
        /// </returns>
        public static DateTime GetDataDateFaktur(EndCustomer endCustomerData)
        {
            DateTime data = DateTime.MinValue;
            try
            {
                if (endCustomerData.FakturDate.Year != 1753)
                {
                    data = endCustomerData.FakturDate;
                }
                else
                {
                    if (endCustomerData.OpenFakturDate.Year != 1753)
                    {
                        data = endCustomerData.OpenFakturDate;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return data;
        }

        /// <summary>
        /// validate 
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="fsKindCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateServicePKTDate(string chassisNumber, FreeService freeService, string fsKindCode, bool isSoldSameWithFSDealer, List<ValidResult> validationResults)
        {
            DateTime tglService = freeService.ServiceDate;
            ChassisMaster chassisMasterData = null;
            EndCustomer endCustomerData = null;
            ChassisMasterPKT chassisMasterPKTData = null;
            DateTime tanggalTemp = DateTime.MinValue;

            var chassisMasterPKTMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
            var chassisMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            var fsKindOnVehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            var endCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(EndCustomer).ToString());

            var datePembanding = new DateTime();
            var critChassisMaster = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            critChassisMaster.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber));

            var chassisMasterArray = chassisMasterMapper.RetrieveByCriteria(critChassisMaster);

            if (chassisMasterArray.Count > 0)
            {
                chassisMasterData = (ChassisMaster)chassisMasterArray[0];

                var endCustomerCriteria = new CriteriaComposite(new Criteria(typeof(EndCustomer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                endCustomerCriteria.opAnd(new Criteria(typeof(EndCustomer), "ID", MatchType.Exact, chassisMasterData.EndCustomerID));

                var endCustomerArray = endCustomerMapper.RetrieveByCriteria(endCustomerCriteria);

                if (endCustomerArray.Count > 0)
                {
                    endCustomerData = (EndCustomer)endCustomerArray[0];
                }

                var critChassisMasterPKT = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                critChassisMasterPKT.opAnd(new Criteria(typeof(ChassisMasterPKT), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));

                var chassisMasterPKTList = chassisMasterPKTMapper.RetrieveByCriteria(critChassisMasterPKT).Cast<ChassisMasterPKT>().ToList();
                if (chassisMasterPKTList.Count > 0)
                {
                    chassisMasterPKTData = (ChassisMasterPKT)chassisMasterPKTList[0];
                }

                //if (isSoldSameWithFSDealer)
                //{
                if (chassisMasterPKTData == null)
                {
                    bool isSameValidation = false;
                    foreach (ValidResult item in validationResults)
                    {
                        if (item.Message == string.Format("PKT Date dengan Chassis {0} tidak ditemukan", chassisNumber))
                        {
                            isSameValidation = true;
                        }
                    }

                    if (!isSameValidation)
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("PKT Date dengan Chassis {0} tidak ditemukan", chassisNumber)
                        };
                        validationResults.Add(validResult);
                    }
                    return false;
                }
                else
                {
                    // sold date
                    ValidResult validResult = null;
                    //return ValidationHelper.ValidateSoldDate(freeService, validResult, chassisMasterPKTData.PKTDate);
                    bool soldDate = ValidationHelper.ValidateSoldDate(freeService, validResult, chassisMasterPKTData.PKTDate);
                    if (!soldDate && validResult != null)
                    {
                        validationResults.Add(validResult);
                        return false;
                    }

                    if (chassisMasterPKTData.PKTDate < GetDefaultFakturDateTime())
                    {
                        try
                        {
                            tanggalTemp = GetDataDateFaktur(endCustomerData);
                            if (tanggalTemp != DateTime.MinValue)
                            {
                                datePembanding = tanggalTemp;
                            }
                            else
                            {
                                var validResults = new ValidResult()
                                {
                                    IsValid = false,
                                    ErrorCode = 20009,
                                    Message = "Faktur Kendaraan ini tidak memiliki Faktur Date maupun Open Faktur Date"
                                };
                                validationResults.Add(validResults);
                                return false;
                            }
                        }
                        catch
                        {
                            datePembanding = chassisMasterPKTData.PKTDate;
                        }

                    }
                    else
                    {
                        datePembanding = chassisMasterPKTData.PKTDate;
                    }
                }
                //}

                #region old method
                /*
                if (chassisMasterPKTData != null)
                {
                    if (!isSoldSameWithFSDealer || (isSoldSameWithFSDealer && endCustomerData != null))
                    {
                        if (chassisMasterPKTData.PKTDate < GetDefaultFakturDateTime())
                        {
                            try
                            {
                                tanggalTemp = GetDataDateFaktur(endCustomerData);
                                if (tanggalTemp != DateTime.MinValue)
                                {
                                    datePembanding = tanggalTemp;
                                }
                                else
                                {
                                    var validResult = new ValidResult()
                                    {
                                        IsValid = false,
                                        ErrorCode = 20009,
                                        Message = "Faktur Kendaraan ini tidak memiliki Faktur Date maupun Open Faktur Date"
                                    };
                                    validationResults.Add(validResult);
                                    return false;
                                }
                            }
                            catch
                            {
                                datePembanding = chassisMasterPKTData.PKTDate;
                            }

                        }
                        else
                        {
                            datePembanding = chassisMasterPKTData.PKTDate;
                        }
                    }
                    else
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Kendaraan ini tidak memiliki Faktur maupun Tanggal PKT. Kendaraan ini masih kendaraan Stock Dealer"
                        };
                        validationResults.Add(validResult);
                        return false;
                    }

                }
                else
                {
                    try
                    {
                        datePembanding = GetDataDateFaktur(endCustomerData);
                        if (datePembanding == DateTime.MinValue)
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = "Faktur Kendaraan ini tidak memiliki Faktur Date maupun Open Faktur Date"
                            };
                            validationResults.Add(validResult);
                            return false;
                        }
                        else
                        {
                            // sold date
                            ValidResult validResult = null;
                            bool soldDate = ValidationHelper.ValidateSoldDate(freeService, validResult, datePembanding);
                            if (!soldDate && validResult != null)
                            {
                                validationResults.Add(validResult);
                                return false;
                            }
                        }
                    }
                    catch
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Kendaraan ini tidak memiliki Faktur maupun Tanggal PKT. Kendaraan ini masih kendaraan Stock Dealer"
                        };
                        validationResults.Add(validResult);
                        return false;
                    }
                }
                */
                #endregion

                if (datePembanding != DateTime.MinValue)
                {
                    var critFSKindOnVehicleType = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    critFSKindOnVehicleType.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode));
                    critFSKindOnVehicleType.opAnd(new Criteria(typeof(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, chassisMasterData.VechileColor.VechileType.ID));

                    var fsKindOnVehicleTypeArray = fsKindOnVehicleTypeMapper.RetrieveByCriteria(critFSKindOnVehicleType);

                    if (fsKindOnVehicleTypeArray.Count > 0)
                    {
                        var fsKindOnVehicleType = (FSKindOnVechileType)fsKindOnVehicleTypeArray[0];
                        var timeSpan = tglService.Subtract(datePembanding);

                        var dayDifference = Convert.ToInt32(timeSpan.Days);
                        if (dayDifference > fsKindOnVehicleType.Duration)
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = "Tanggal service melebihi tanggal yang seharusnya"
                            };
                            validationResults.Add(validResult);
                            return false;
                        }
                    }
                    else
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Kendaraan tidak berhak mendapatkan Free Service"
                        };
                        validationResults.Add(validResult);
                        return false;
                    }
                }
                else
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = "Faktur Kendaraan ini tidak memiliki Faktur Date maupun Open Faktur Date"
                    };
                    validationResults.Add(validResult);
                    return false;
                }
            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Nomor Chassis tidak ditemukan"
                };
                validationResults.Add(validResult);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate PKT Date
        /// </summary>
        /// <param name="pktDate"></param>
        /// <param name="validResultList"></param>
        public static bool ValidatePKTDate(DateTime pktDate, ref List<ValidResult> validResultList)
        {
            if (pktDate > DateTime.Now.Date)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Tanggal PKT tidak boleh melebihi tanggal hari ini"
                };
                validResultList.Add(validResult);
                return false;
            }
            return true;
        }

        public static bool ValidateDelaerChassisPKT(string dealerCode, ref List<ValidResult> validResultList)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(TransactionControl).ToString());

            // get by criteria
            var crt = new CriteriaComposite(new Criteria(typeof(TransactionControl), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            crt.opAnd(new Criteria(typeof(TransactionControl), "Kind", MatchType.Exact, "25"));
            crt.opAnd(new Criteria(typeof(TransactionControl), "Status", MatchType.Exact, "1"));
            crt.opAnd(new Criteria(typeof(TransactionControl), "Dealer.DealerCode", MatchType.Exact, dealerCode));

            var obj = _mapper.RetrieveByCriteria(crt);
            if (obj.Count > 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Update PKT tidak dapat dilakukan, PKT harus diinput melalui aplikasi SFID"
                };
                validResultList.Add(validResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate passed chassis number parameter
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMaster"></param>
        public static bool ValidateChassisMaster(string chassisNumber, ref List<ValidResult> validResultList, ref ChassisMaster chassisMaster, bool isForPKT = false, ChassisMasterPKT chassisMasterPKT = null)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            var _pdiMapper = MapperFactory.GetInstance().GetMapper(typeof(PDI).ToString());
            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var _wscHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(WSCHeader).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMaster), "ChassisNumber", "Category.ProductCategory.Code", chassisNumber, ConfigurationManager.AppSettings["CompanyCode"]));
            if (masters.Count > 0)
            {
                // cast the object
                chassisMaster = masters[0] as ChassisMaster;
                // special validation for chassis master pkt
                if (isForPKT)
                {
                    if (chassisMasterPKT == null)
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Tanggal PKT tidak valid"
                        };
                        validResultList.Add(validResult);
                        return false;
                    }
                    var handoverDate = chassisMasterPKT.PKTDate;
                    var critPDI = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    critPDI.opAnd(new Criteria(typeof(PDI), "PDIStatus", MatchType.Exact, (int)EnumFSStatus.FSStatus.Selesai));
                    critPDI.opAnd(new Criteria(typeof(PDI), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));

                    var arlPDI = _pdiMapper.RetrieveByCriteria(critPDI);
                    if (arlPDI.Count > 0)
                    {
                        var objPDI = (PDI)arlPDI[0];

                        var diffMonth = handoverDate.Subtract(objPDI.PDIDate).Days / 30;
                        if (handoverDate < objPDI.PDIDate)
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = "Tanggal PKT tidak boleh kurang dari tanggal PDI"
                            };
                            validResultList.Add(validResult);
                            return false;
                        }
                        else if (diffMonth > 0)
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = "Tanggal PKT tidak boleh lebih dari 1 bulan dari tanggal PDI"
                            };
                            validResultList.Add(validResult);
                            return false;
                        }
                    }
                    else
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Chassis ini belum memiliki PDI dengan status selesai"
                        };
                        validResultList.Add(validResult);
                        return false;
                    }

                    var checkFS = false;
                    var checkWSC = false;
                    var objFS = new FreeService();
                    var objWSC = new WSCHeader();

                    var critFS = new CriteriaComposite(new Criteria(typeof(FreeService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    critFS.opAnd(new Criteria(typeof(FreeService), "FSKind.KindCode", MatchType.Exact, 1)); //request user hanya untuk fs kind 1
                    critFS.opAnd(new Criteria(typeof(FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
                    var arlFS = _freeServiceMapper.RetrieveByCriteria(critFS);
                    if (arlFS.Count > 0)
                    {
                        checkFS = true;
                        objFS = (FreeService)arlFS[0];
                    }

                    var critWSC = new CriteriaComposite(new Criteria(typeof(WSCHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    critWSC.opAnd(new Criteria(typeof(WSCHeader), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
                    var arlWSC = _wscHeaderMapper.RetrieveByCriteria(critWSC);
                    if (arlWSC.Count > 0)
                    {
                        checkWSC = true;
                        objWSC = (WSCHeader)arlWSC[0];
                    }

                    if (checkFS)
                    {
                        if (objFS != null)
                        {
                            if (objFS.ServiceDate.Date < handoverDate.Date)
                            {
                                var validResult = new ValidResult()
                                {
                                    IsValid = false,
                                    ErrorCode = 20009,
                                    Message = "Tanggal PKT tidak boleh lebih besar daripada tanggal Free Service Kind Code 1"
                                };
                                validResultList.Add(validResult);
                                return false;
                            }
                        }
                    }
                    if (checkWSC)
                    {
                        if (objWSC != null)
                        {
                            if (objWSC.ServiceDate.Date < handoverDate.Date)
                            {
                                var validResult = new ValidResult()
                                {
                                    IsValid = false,
                                    ErrorCode = 20009,
                                    Message = "Tanggal PKT tidak boleh lebih besar daripada tanggal WSC"
                                };
                                validResultList.Add(validResult);
                                return false;
                            }
                        }
                    }
                }

            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Data Chassis Master dengan Chassis Number {0} tidak ditemukan di database", chassisNumber)
                };
                validResultList.Add(validResult);
                return false;
            }

            return validResultList.Count == 0;
        }

        /// <summary>
        /// Validate passed chassis number parameter
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMasterPKT"></param>
        public static bool ValidateChassisMasterPKT(string chassisNumber, List<ValidResult> validResultList, ref ChassisMasterPKT chassisMasterPKT)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());

            // get by criteria
            var chassismasterPKT = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            chassismasterPKT.opAnd(new Criteria(typeof(ChassisMasterPKT), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));
            var masters = _mapper.RetrieveByCriteria(chassismasterPKT);
            if (masters.Count > 0)
            {
                // cast the object
                chassisMasterPKT = masters[0] as ChassisMasterPKT;
            }
            else
            {
                chassisMasterPKT = new ChassisMasterPKT();
                return true;
            }

            return validResultList.Count == 0;
        }


        /// <summary>Validates the date cannot more than today.</summary>
        /// <param name="dateData">The date data.</param>
        /// <param name="tanggalDesc">The tanggal desc.</param>
        /// <param name="validResultList">The valid result list.</param>
        /// <returns></returns>
        public static void ValidateDateCannotMoreThanToday(DateTime dateData, string tanggalDesc, ref List<ValidResult> validResultList)
        {
            if (dateData.Date > DateTime.Now.Date)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("{0} {1} melebihi hari ini {2}", tanggalDesc, dateData, DateTime.Now.ToString("ddMMyyyy"))
                };
                validResultList.Add(validResult);
            }

        }

        /// <summary>Validates the date validy.</summary>
        /// <param name="dateData">The date data.</param>
        /// <param name="tanggalDesc">The tanggal desc.</param>
        /// <param name="validResultList">The valid result list.</param>
        /// <returns></returns>
        public static void ValidateDateValid(DateTime dateData, string tanggalDesc, ref List<ValidResult> validResultList)
        {
            if ((dateData <= System.Data.SqlTypes.SqlDateTime.MinValue.Value) || (dateData >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value))
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("{0} {1} bukan merupakan tanggal yang valid.", tanggalDesc, dateData)
                };
                validResultList.Add(validResult);
            }
        }

        /// <summary>ValidateSold/PKTDateInput</summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        public static void ValidateServiceDateToLastServiceDate(FreeService objCreate, ChassisMaster chassisMaster, List<ValidResult> validationResults)
        {
            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.GreaterOrEqual, objCreate.ServiceDate));
            var fs = _freeServiceMapper.RetrieveByCriteria(criterias);
            if (fs.Count > 0)
            {
                FreeService dt = (FreeService)fs[0];
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("“Tgl. Service tidak boleh lebih kecil dari Tgl. Service Terakhir {0}", dt.ServiceDate.ToString("dd MMM yyyy"))
                };
                validationResults.Add(validResult);
            }
        }

        /// <summary>
        /// Validate FS
        /// </summary>
        /// <param name="ChassisMaster"></param>
        /// <param name="FSKind"></param>
        /// <param name="validationResults"></param>
        public static void ValidateFSKindUsed(ChassisMaster cm, FSKind fk, List<ValidResult> validationResults)
        {
            #region initialize mapper
            var _FreeService = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            #endregion
            var critFS = new CriteriaComposite(new Criteria(typeof(FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critFS.opAnd(new Criteria(typeof(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, cm.ChassisNumber));
            var arlFreeService = _FreeService.RetrieveByCriteria(critFS);
            if (arlFreeService.Count > 0)
            {
                string usedFS = "";
                int fCode = Int32.Parse(string.Join("", fk.KindCode.ToCharArray().Where(Char.IsDigit)));
                foreach (FreeService dataFS in arlFreeService)
                {
                    int curFCode = Int32.Parse(string.Join("", dataFS.FSKind.KindCode.ToCharArray().Where(Char.IsDigit)));
                    if (curFCode >= fCode)
                    {
                        usedFS = dataFS.FSKind.KindCode;
                    }
                }
                if (usedFS.Trim().Length > 0)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Terakhir FS adalah {0}. Silahkan Ajukan FS Berikutnya.", usedFS)
                    };
                    validationResults.Add(validResult);
                }
            }
        }

        /// <summary>
        /// Validate ServiceDate And Today
        /// </summary>
        /// <param name="ChassisMaster"></param>
        /// <param name="FSKind"></param>
        /// <param name="validationResults"></param>
        public static void ValidateServiceDateAndToday(bool Simpan, DateTime serviceDate, List<ValidResult> validationResults)
        {
            int totalDays = Math.Abs((serviceDate.Date - DateTime.Now.Date).Days);
            if (totalDays > 14)
            {
                string addMess = "";
                if (Simpan)
                {
                    addMess = "Input ";
                }

                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("{0}Pengajuan tidak dapat dilakukan karena periode pengajuan melebihi 14 hari dari tanggal service", addMess)
                };
                validationResults.Add(validResult);
            }
        }

        /// <summary>
        /// Validate for Claims in the Free service nothing more than MSPExRegistration
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="fsKind"></param>
        /// <param name="validResultList"></param>
        /// <returns></returns>
        public static bool IsAllowToInsertMSPEx(ChassisMaster chassisMaster, FSKind fsKind, List<ValidResult> validResultList)
        {
            #region initialize variable
            bool vReturn = true;
            var registration_count = 0;
            var freeservice_count = 0;

            #endregion


            #region initialize mapper
            var _chassisMaster = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            var _mspExRegis = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            var _freeService = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var _fsKindOnVehicleType = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());

            #endregion

            //ISEXPIRED
            var criterias_isexpired = new CriteriaComposite(new Criteria(typeof(MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_isexpired.opAnd(new Criteria(typeof(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            criterias_isexpired.opAnd(new Criteria(typeof(MSPExRegistration), "ValidDateTo", MatchType.Greater, DateTime.Now));
            criterias_isexpired.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId));
            var data_isexpired = _mspExRegis.RetrieveByCriteria(criterias_isexpired);
            if (data_isexpired.Count == 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Program dengan No. Rangka {0} dan jenis servis {1} tidak ditemukan atau telah expired", chassisMaster.ChassisNumber, fsKind.ID)
                };
                validResultList.Add(validResult);
                vReturn = false;
            }

            //COUNT REGISTRATION
            var criterias_registration = new CriteriaComposite(new Criteria(typeof(MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId));
            var data = _mspExRegis.RetrieveByCriteria(criterias_registration);
            registration_count = data.Count;


            //COUNT FREE SERVICE
            var criterias_freeservice = new CriteriaComposite(new Criteria(typeof(FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_freeservice.opAnd(new Criteria(typeof(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            criterias_freeservice.opAnd(new Criteria(typeof(FreeService), "FSKind.KindCode", MatchType.Exact, fsKind.KindCode));
            var freeService = _freeService.RetrieveByCriteria(criterias_freeservice);
            if (freeService.Count > 0)
            {
                foreach (var dataFS in freeService)
                {
                    var freeservice = (FreeService)dataFS;
                    var criterias_fskindonvehicletype = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    criterias_fskindonvehicletype.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, freeservice.FSKind.ID));
                    var fsKindOnVehicleType = _fsKindOnVehicleType.RetrieveByCriteria(criterias_fskindonvehicletype);
                    if (fsKindOnVehicleType.Count > 0)
                    {
                        freeservice_count += 1;
                    }
                }
            }


            if (freeservice_count > registration_count)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("No. Rangka {0} dengan jenis servis {1} tidak dapat di claim", chassisMaster.ChassisNumber, fsKind.ID)
                };
                validResultList.Add(validResult);
                vReturn = false;
            }

            return vReturn;
        }

        /// <summary>ValidateFSKindMSPExt</summary>
        /// <param name="chassisMaster"></param>
        /// <param name="fsKind"></param>
        /// <param name="validationResults"></param>
        public static void ValidateFSKindMSPExt(ChassisMaster chassisMaster, FSKind fsKind, List<ValidResult> validationResults)
        {
            #region initialize mapper
            var _mspExRegis = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            var _mspExMaster = MapperFactory.GetInstance().GetMapper(typeof(MSPExMaster).ToString());
            var _fSKindOnVechileType = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            #endregion

            string CodeMSPEx1 = string.Empty, CodeMSPEx2 = string.Empty;
            var critRegis = new CriteriaComposite(new Criteria(typeof(MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critRegis.opAnd(new Criteria(typeof(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            var sortColl = new SortCollection();
            sortColl.Add(new Sort(typeof(MSPExRegistration), "ID", Sort.SortDirection.DESC));
            var arlFSSts = _mspExRegis.RetrieveByCriteria(critRegis, sortColl);
            if (arlFSSts.Count > 0)
            {
                int MspId = ((MSPExRegistration)arlFSSts[0]).MSPExMaster.ID;
                var critMSPExMaster = new CriteriaComposite(new Criteria(typeof(MSPExMaster), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                critMSPExMaster.opAnd(new Criteria(typeof(MSPExMaster), "ID", MatchType.Exact, MspId));
                sortColl = new SortCollection();
                sortColl.Add(new Sort(typeof(MSPExMaster), "ID", Sort.SortDirection.DESC));
                var arlMSPExMaster = _mspExMaster.RetrieveByCriteria(critMSPExMaster, sortColl);


                var critFSKindOnVechileType = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                critFSKindOnVechileType.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKind.KindCode));
                sortColl = new SortCollection();
                sortColl.Add(new Sort(typeof(FSKindOnVechileType), "ID", Sort.SortDirection.DESC));
                var arlFSKindOnVechileType = _fSKindOnVechileType.RetrieveByCriteria(critFSKindOnVechileType, sortColl);

                if (arlMSPExMaster.Count > 0 && arlFSKindOnVechileType.Count > 0)
                {
                    CodeMSPEx1 = ((MSPExMaster)arlMSPExMaster[0]).MSPExType.Code;
                    FSKindOnVechileType objFSKindOnVechileType = (FSKindOnVechileType)arlFSKindOnVechileType[0];
                    CodeMSPEx2 = objFSKindOnVechileType.FSType;

                    if (CodeMSPEx1 == "2XPM" && CodeMSPEx2 != "7")
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis {0}", objFSKindOnVechileType.FSKind.KindDescription)
                        };
                        validationResults.Add(validResult);
                    }
                    if (CodeMSPEx1 == "4XPM" && CodeMSPEx2 != "8")
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis {0}", objFSKindOnVechileType.FSKind.KindDescription)
                        };
                        validationResults.Add(validResult);
                    }
                    if (CodeMSPEx1 == "6XPM" && CodeMSPEx2 != "9")
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis {0}", objFSKindOnVechileType.FSKind.KindDescription)
                        };
                        validationResults.Add(validResult);
                    }
                }
                else
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis {0}", fsKind.KindDescription)
                    };
                    validationResults.Add(validResult);
                }
            }
        }

        /// <summary>
        /// Validate MSP Extended ServiceDate And Today
        /// </summary>
        /// <param name="ChassisMaster"></param>
        /// <param name="FSKind"></param>
        /// <param name="validationResults"></param>
        public static void ValidateMSPExtServiceDateAndToday(bool Simpan, DateTime serviceDate, List<ValidResult> validationResults)
        {
            int totalDays = Math.Abs((serviceDate.Date - DateTime.Now.Date).Days);
            if (totalDays > 61)
            {
                string addMess = "";
                if (Simpan)
                {
                    addMess = "Input ";
                }

                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("{0}Pengajuan tidak dapat dilakukan karena periode pengajuan melebihi 61 hari dari tanggal service", addMess)
                };
                validationResults.Add(validResult);
            }
        }

        /// <summary>ValidateMSPExtPayment</summary>
        /// <param name="chassisMaster"></param>
        /// <param name="validationResults"></param>
        public static void ValidateMSPExtPayment(ChassisMaster chassisMaster, List<ValidResult> validationResults)
        {
            #region initialize mapper
            var _mspRegistrationHistory = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            var _mspTransferPaymentDetail = MapperFactory.GetInstance().GetMapper(typeof(MSPExPaymentDetail).ToString());
            #endregion

            var critRegHist = new CriteriaComposite(new Criteria(typeof(MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critRegHist.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, ((int)EnumStatusMSP.Status.Selesai)));
            //critRegHist.opAnd(new Criteria(typeof(MSPExRegistration), "BenefitMasterHeaderID", MatchType.Exact, 0));
            critRegHist.opAnd(new Criteria(typeof(MSPExRegistration), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            critRegHist.opAnd(new Criteria(typeof(MSPExRegistration), "Dealer.ID", MatchType.Exact, chassisMaster.Dealer.ID));

            SortCollection srtRegHist = new SortCollection();
            srtRegHist.Add(new Sort(typeof(MSPExRegistration), "ID", Sort.SortDirection.DESC));

            var arlMSPRegHistory = _mspRegistrationHistory.RetrieveByCriteria(critRegHist, srtRegHist);
            if (arlMSPRegHistory.Count > 0)
            {
                MSPExRegistration oMSPRegHistory = new MSPExRegistration();
                oMSPRegHistory = (MSPExRegistration)arlMSPRegHistory[0];

                var critMSPTransPaymentD = new CriteriaComposite(new Criteria(typeof(MSPExPaymentDetail), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                critMSPTransPaymentD.opAnd(new Criteria(typeof(MSPExPaymentDetail), "MSPExPayment.Status", MatchType.Exact, ((int)EnumStatusMSP.Status.Selesai)));
                critMSPTransPaymentD.opAnd(new Criteria(typeof(MSPExPaymentDetail), "MSPExRegistration.ID", MatchType.Exact, oMSPRegHistory.ID));

                var arlMSPTransferPaymentDetail = _mspTransferPaymentDetail.RetrieveByCriteria(critMSPTransPaymentD);
                if (arlMSPTransferPaymentDetail.Count == 0)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Mohon melakukan pembayaran MSP Extended terlebih dahulu")
                    };
                    validationResults.Add(validResult);
                }
            }
        }

        /// <summary>
        /// Validate to prevent duplicate key
        /// </summary>
        /// <param name="ChassisID"></param>
        /// <param name="FSKindID"></param>
        /// <returns></returns>
        public static bool IsExistCodeForInsertMSPEx(ChassisMaster chassisMaster, string FSKindCode, List<ValidResult> validationResults)
        {
            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var _mspexRegisMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            var _mspexMappingtoFSKindMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPExMappingtoFSKind).ToString());
            //var isExpired = false;
            //var isNotExpired = true;
            var MSPExValidStatus = false;
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, FSKindCode));
            ArrayList arlFS = _freeServiceMapper.RetrieveByCriteria(criterias);
            if (arlFS.Count > 0)
            {
                CriteriaComposite criteriasmspex = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criteriasmspex.opAnd(new Criteria(typeof(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
                criteriasmspex.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId));
                var sortAppConf = new SortCollection();
                sortAppConf.Add(new Sort(typeof(MSPExRegistration), "ID", Sort.SortDirection.ASC));
                var mspexregisList = _mspexRegisMapper.RetrieveByCriteria(criteriasmspex, sortAppConf).Cast<MSPExRegistration>().ToList();
                foreach (var mspexlist in mspexregisList)
                {
                    //if (mspexlist.ValidDateTo >= DateTime.Now)
                    //{
                    //    isNotExpired = true;
                    //}
                    //else if (mspexlist.ValidDateTo < DateTime.Now)
                    //{
                    //    isExpired = true;
                    //}
                    MSPExRegistration lastMSPEx = (MSPExRegistration)mspexregisList[mspexregisList.Count - 1]; //ambil registrasi MPS terakhir
                    if (mspexlist.ValidDateTo >= DateTime.Now)
                    {
                        int FSHistoryAfterLastMSPCounter = 0;
                        bool isUsedFSKind = false;
                        foreach (FreeService f in arlFS)
                        {
                            if (f.CreatedTime >= lastMSPEx.CreatedTime)
                            {
                                FSHistoryAfterLastMSPCounter += 1;
                                if (FSKindCode == f.FSKind.KindCode)
                                {
                                    isUsedFSKind = true;
                                }
                            }
                        }
                        CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                        crit.opAnd(new Criteria(typeof(MSPExMappingtoFSKind), "MSPExType.ID", MatchType.Exact, lastMSPEx.MSPExMaster.MSPExType.ID));
                        var MaxPM = _mspexMappingtoFSKindMapper.RetrieveByCriteria(crit);
                        if (FSHistoryAfterLastMSPCounter < MaxPM.Count && !isUsedFSKind)
                        {
                            MSPExValidStatus = true;
                        }
                    }
                }
                //if (isNotExpired == true && isExpired == true)
                if (MSPExValidStatus)
                {
                    return true;
                }
                else
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("No. Rangka {0} dengan jenis servis {1} sudah ada", chassisMaster.ChassisNumber, FSKindCode)
                    };
                    validationResults.Add(validResult);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Validate FS MSP Ex Kind Code Leading Digit
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="fsKindCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateFSMSPExKindCodeLeadingDigit(string chassisNumber, string fsKindCode, List<ValidResult> validationResults)
        {
            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var _mspexRegisMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            var _mspexMappingtoFSKindMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPExMappingtoFSKind).ToString());
            var MSPExValidStatus = false;
            var isExpired = false;
            var isNotExpired = true;

            var fsMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            var critFS = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            critFS.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));

            var freeServiceList = fsMapper.RetrieveByCriteria(critFS).Cast<FreeService>().ToList();

            foreach (var freeService in freeServiceList)
            {
                if (freeService.FSKind != null)
                {
                    if (Regex.Replace(freeService.FSKind.KindCode, "[^0-9]", "") == Regex.Replace(fsKindCode, "[^0-9]", ""))
                    {
                        CriteriaComposite criteriasmspex = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                        criteriasmspex.opAnd(new Criteria(typeof(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));
                        criteriasmspex.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId));

                        var mspexregisList = _mspexRegisMapper.RetrieveByCriteria(criteriasmspex).Cast<MSPExRegistration>().ToList();

                        foreach (var mspexlist in mspexregisList)
                        {
                            //if (mspexlist.ValidDateTo > DateTime.Now)
                            //{
                            //    isNotExpired = true;
                            //}
                            //else if (mspexlist.ValidDateTo < DateTime.Now)
                            //{
                            //    isExpired = true;
                            //}

                            MSPExRegistration lastMSPEx = (MSPExRegistration)mspexregisList[mspexregisList.Count - 1]; //ambil registrasi MPS terakhir
                            if (mspexlist.ValidDateTo >= DateTime.Now)
                            {
                                int FSHistoryAfterLastMSPCounter = 0;
                                bool isUsedFSKind = false;
                                foreach (FreeService f in freeServiceList)
                                {
                                    if (f.CreatedTime >= lastMSPEx.CreatedTime)
                                    {
                                        FSHistoryAfterLastMSPCounter += 1;
                                        if (fsKindCode == f.FSKind.KindCode)
                                        {
                                            isUsedFSKind = true;
                                        }
                                    }
                                }
                                CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                                crit.opAnd(new Criteria(typeof(MSPExMappingtoFSKind), "MSPExType.ID", MatchType.Exact, lastMSPEx.MSPExMaster.MSPExType.ID));
                                var MaxPM = _mspexMappingtoFSKindMapper.RetrieveByCriteria(crit);
                                if (FSHistoryAfterLastMSPCounter < MaxPM.Count && !isUsedFSKind)
                                {
                                    MSPExValidStatus = true;
                                }
                            }
                        }
                        //if (isNotExpired == true && isExpired == true)
                        if (MSPExValidStatus)
                        {
                            return true;
                        }
                        else
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = string.Format("Anda tidak bisa memilih kode free service {0} karena telah menggunakan kode free service {1}", fsKindCode, freeService.FSKind.KindCode)
                            };
                            validationResults.Add(validResult);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Validate to prevent duplicate key
        /// </summary>
        /// <param name="ChassisID"></param>
        /// <param name="FSKindID"></param>
        /// <returns></returns>
        public static bool IsExistCodeForInsert(ChassisMaster chassisMaster, string FSKindCode, List<ValidResult> validationResults)
        {
            var _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, FSKindCode));
            if (_freeServiceMapper.RetrieveByCriteria(criterias).Count > 0)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("No. Rangka {0} dengan jenis servis {1} sudah ada", chassisMaster.ChassisNumber, FSKindCode)
                };
                validationResults.Add(validResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// validate 
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="fsKindCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateServicePKTDate(string chassisNumber, DateTime tglService, string fsKindCode, bool isSoldSameWithFSDealer, List<ValidResult> validationResults)
        {
            ChassisMaster chassisMasterData = null;
            EndCustomer endCustomerData = null;
            ChassisMasterPKT chassisMasterPKTData = null;
            DateTime tanggalTemp = DateTime.MinValue;

            var chassisMasterPKTMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
            var chassisMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            var fsKindOnVehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            var endCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(EndCustomer).ToString());

            var datePembanding = new DateTime();
            var critChassisMaster = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            critChassisMaster.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber));

            var chassisMasterArray = chassisMasterMapper.RetrieveByCriteria(critChassisMaster);

            if (chassisMasterArray.Count > 0)
            {
                chassisMasterData = (ChassisMaster)chassisMasterArray[0];

                var endCustomerCriteria = new CriteriaComposite(new Criteria(typeof(EndCustomer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                endCustomerCriteria.opAnd(new Criteria(typeof(EndCustomer), "ID", MatchType.Exact, chassisMasterData.EndCustomerID));

                var endCustomerArray = endCustomerMapper.RetrieveByCriteria(endCustomerCriteria);

                if (endCustomerArray.Count > 0)
                {
                    endCustomerData = (EndCustomer)endCustomerArray[0];
                }

                var critChassisMasterPKT = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                critChassisMasterPKT.opAnd(new Criteria(typeof(ChassisMasterPKT), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));

                var chassisMasterPKTList = chassisMasterPKTMapper.RetrieveByCriteria(critChassisMasterPKT).Cast<ChassisMasterPKT>().ToList();
                if (chassisMasterPKTList.Count > 0)
                {
                    chassisMasterPKTData = (ChassisMasterPKT)chassisMasterPKTList[0];
                }

                if (isSoldSameWithFSDealer)
                {
                    if (chassisMasterPKTData == null)
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Nomor Chassis belum memiliki tanggal PKT"
                        };
                        validationResults.Add(validResult);
                        return false;
                    }
                }

                if (chassisMasterPKTData != null)
                {
                    if (!isSoldSameWithFSDealer || (isSoldSameWithFSDealer && endCustomerData != null))
                    {
                        if (chassisMasterPKTData.PKTDate < GetDefaultFakturDateTime())
                        {
                            try
                            {
                                tanggalTemp = GetDataDateFaktur(endCustomerData);
                                if (tanggalTemp != DateTime.MinValue)
                                {
                                    datePembanding = tanggalTemp;
                                }
                                else
                                {
                                    var validResult = new ValidResult()
                                    {
                                        IsValid = false,
                                        ErrorCode = 20009,
                                        Message = "Faktur Kendaraan ini tidak memiliki Faktur Date maupun Open Faktur Date"
                                    };
                                    validationResults.Add(validResult);
                                    return false;
                                }
                            }
                            catch
                            {
                                datePembanding = chassisMasterPKTData.PKTDate;
                            }

                        }
                        else
                        {
                            datePembanding = chassisMasterPKTData.PKTDate;
                        }
                    }
                    else
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Kendaraan ini tidak memiliki Faktur maupun Tanggal PKT. Kendaraan ini masih kendaraan Stock Dealer"
                        };
                        validationResults.Add(validResult);
                        return false;
                    }

                }
                else
                {
                    try
                    {
                        datePembanding = GetDataDateFaktur(endCustomerData);
                        if (datePembanding == DateTime.MinValue)
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = "Faktur Kendaraan ini tidak memiliki Faktur Date maupun Open Faktur Date"
                            };
                            validationResults.Add(validResult);
                            return false;
                        }
                    }
                    catch
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Kendaraan ini tidak memiliki Faktur maupun Tanggal PKT. Kendaraan ini masih kendaraan Stock Dealer"
                        };
                        validationResults.Add(validResult);
                        return false;
                    }
                }

                if (datePembanding != DateTime.MinValue)
                {
                    var critFSKindOnVehicleType = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    critFSKindOnVehicleType.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode));
                    critFSKindOnVehicleType.opAnd(new Criteria(typeof(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, chassisMasterData.VechileColor.VechileType.ID));

                    var fsKindOnVehicleTypeArray = fsKindOnVehicleTypeMapper.RetrieveByCriteria(critFSKindOnVehicleType);

                    if (fsKindOnVehicleTypeArray.Count > 0)
                    {
                        var fsKindOnVehicleType = (FSKindOnVechileType)fsKindOnVehicleTypeArray[0];
                        var timeSpan = tglService.Subtract(datePembanding);

                        var dayDifference = Convert.ToInt32(timeSpan.Days);
                        if (dayDifference > fsKindOnVehicleType.Duration)
                        {
                            var validResult = new ValidResult()
                            {
                                IsValid = false,
                                ErrorCode = 20009,
                                Message = "Tanggal service melebihi tanggal yang seharusnya"
                            };
                            validationResults.Add(validResult);
                            return false;
                        }
                    }
                    else
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = "Kendaraan tidak berhak mendapatkan Free Service"
                        };
                        validationResults.Add(validResult);
                        return false;
                    }
                }
                else
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = "Faktur Kendaraan ini tidak memiliki Faktur Date maupun Open Faktur Date"
                    };
                    validationResults.Add(validResult);
                    return false;
                }
            }

            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Nomor Chassis tidak ditemukan"
                };
                validationResults.Add(validResult);
                return false;
            }
            return true;
        }


        #region Private Methods
        /// <summary>
        /// Check if chassis is allowed
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <returns></returns>
        private static bool IsChassisAllowed(string chassisNumber)
        {
            string[] objType ={
            "MMBJNKB40AD026535",
            "MMBJNKB40AD033824",
            "MMBJNKB40AD042965",
            "MMBJNKB40AD043166",
            "MMBJNKB40AD020735",
            "MMBJNKB40AD030855",
            "MMBJNKB40AD030954",
            "MMBJNKB40AD038483",
            "MMBJNKB40AD038487",
            "MMBJNKB40AD038974"};

            return objType.Any(x => x == chassisNumber);
        }


        /// <summary>
        /// Check if FS allowed
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="fsKind"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        private static bool IsAllowFreeService(ChassisMaster chassisMaster, FSKind fsKind, string dealerCode)
        {
            if (ChassisException(fsKind, chassisMaster.ChassisNumber) && IsAllowFSCampaign(fsKind, chassisMaster, dealerCode))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get FSType based on kind code and vehicle type
        /// </summary>
        /// <param name="fSKind"></param>
        /// <param name="chassisMaster"></param>
        /// <returns></returns>
        public static string GetFSTypebyKindVehicleType(FSKind fSKind, ChassisMaster chassisMaster)
        {
            #region initialize variable
            string result = string.Empty;
            List<FSKindOnVechileType> listFSKindOnVechileType = new List<FSKindOnVechileType>();
            #endregion

            #region initialize mapper
            var _fsKindOnVehicleType = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            #endregion

            var crt = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            crt.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, fSKind.ID));
            crt.opAnd(new Criteria(typeof(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, chassisMaster.VechileColor.VechileType.ID));

            var data = _fsKindOnVehicleType.RetrieveByCriteria(crt);
            if (data.Count > 0)
            {
                var lst = data.Cast<FSKindOnVechileType>().ToList();
                if (lst.Where(x => x.FSType == "7" || x.FSType == "8" || x.FSType == "9").Count() > 0)
                {
                    result = "MSPExtended";
                }
                else if (lst.Where(x => x.FSType == "4" || x.FSType == "5" || x.FSType == "6").Count() > 0)
                {
                    result = "MSP";
                }
            }

            return result;
        }

        /// <summary>
        /// CHassis exception list
        /// </summary>
        /// <param name="fsKind"></param>
        /// <param name="chassisNumber"></param>
        /// <returns></returns>
        private static bool ChassisException(FSKind fsKind, string chassisNumber)
        {
            bool IsAllowInsert = true;
            // Start  :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
            if (fsKind.KindCode == "3" || fsKind.KindCode == "4" || fsKind.KindCode == "5" || fsKind.KindCode == "6" || fsKind.KindCode == "7")
            {
                string[] sForbiddenCMs = { "MHMFE71P1AK018514", "MHMFE73P2AK014642", "MHMFE73P2AK014643", "MHMFE73P2AK014715", "MHMFE73P2AK014760" };

                return !sForbiddenCMs.Any(x => x.ToUpper() == chassisNumber.ToUpper());
            }

            return IsAllowInsert;
            // End    :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
        }

        /// <summary>
        /// Check if MSP Extended is allowed
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="fsKind"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        public static bool IsAllowMSPExt(ChassisMaster chassisMaster, FSKind fsKind, string dealerCode, out int mspExStatus, DateTime? ServiceDate = null)
        {
            #region initialize variable
            bool vReturn = true;
            var mspextendedregis = new MSPExRegistration();
            var chassismaster = new ChassisMaster();
            var vehiclecolor = new VechileColor();
            var fskindonvehicletype = new FSKindOnVechileType();
            var mspextype = new MSPExMaster();
            var mspextypemappingtofskind = new MSPExMappingtoFSKind();
            mspExStatus = 0;
            #endregion


            #region initialize mapper
            var _chassisMaster = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            var _vehicleColor = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            var _fsKindOnVehicleType = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            var _mspExMappingToFsKind = MapperFactory.GetInstance().GetMapper(typeof(MSPExMappingtoFSKind).ToString());
            var _mspExMaster = MapperFactory.GetInstance().GetMapper(typeof(MSPExMaster).ToString());
            var _mspExtended = MapperFactory.GetInstance().GetMapper(typeof(MSPExRegistration).ToString());
            #endregion

            #region CHECKING MSPEX ON MAPPING TO FSKIND
            var criterias_mspexmapping = new CriteriaComposite(new Criteria(typeof(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_mspexmapping.opAnd(new Criteria(typeof(MSPExMappingtoFSKind), "FSKind.KindCode", MatchType.Exact, fsKind.KindCode));
            var mspExMapping = _mspExMappingToFsKind.RetrieveByCriteria(criterias_mspexmapping);
            if (mspExMapping.Count > 0)
            {
                mspextypemappingtofskind = (MSPExMappingtoFSKind)mspExMapping[0];
            }
            else
            {
                return false;
            }
            #endregion CHECKING MSPEX ON MAPPING TO FSKIND

            //CHECKING DATA MSPEX ON REGISTRATION
            var criterias_registration = new CriteriaComposite(new Criteria(typeof(MSPExRegistration), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            //criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "Status", MatchType.Exact, StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId));
            if (ServiceDate != null)
            {
                criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "ValidDateTo", MatchType.GreaterOrEqual, Convert.ToDateTime(ServiceDate).Date));
            }
            else
            {
                criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "ValidDateTo", MatchType.GreaterOrEqual, DateTime.Now.Date));
            }
            // this msptypeid get from Kindcode on MSPExMappingtoFSKind
            criterias_registration.opAnd(new Criteria(typeof(MSPExRegistration), "MSPExMaster.MSPExType.ID", MatchType.Exact, mspextypemappingtofskind.MSPExType.ID));

            var data = _mspExtended.RetrieveByCriteria(criterias_registration);
            if (data.Count > 0)
            {
                var data1 = data[0] as MSPExRegistration;
                var creatednew = data1.CreatedTime;
                foreach (var i in data)
                {
                    var mspex = i as MSPExRegistration;
                    if (mspex.CreatedTime >= creatednew)
                    {
                        mspextendedregis = mspex;
                    }
                }
                mspExStatus = mspextendedregis.Status;

            }
            else
            {
                vReturn = false;
            }

            //CHECKING MSP EX ON FSKINDONVEHICLETYPE
            if (vReturn == true)
            {
                //GET VEHICLE COLOR BASED ON CHASSIS NUMBER
                var criterias_chassis = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias_chassis.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
                var chassis = _chassisMaster.RetrieveByCriteria(criterias_chassis);
                if (chassis.Count > 0)
                {
                    chassismaster = (ChassisMaster)chassis[0];
                    //GET VEHICLE TYPE BASED ON VEHICLE COLOR
                    var criterias_vehiclecolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    criterias_vehiclecolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, chassismaster.VechileColor.ID));
                    var vehicleColor = _vehicleColor.RetrieveByCriteria(criterias_vehiclecolor);
                    if (vehicleColor.Count > 0)
                    {
                        vehiclecolor = (VechileColor)vehicleColor[0];
                        var criterias_fskindonvehicletype = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                        criterias_fskindonvehicletype.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKind.KindCode));
                        criterias_fskindonvehicletype.opAnd(new Criteria(typeof(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, vehiclecolor.VechileType.ID));
                        var fsKindOnVehicle = _fsKindOnVehicleType.RetrieveByCriteria(criterias_fskindonvehicletype);
                        if (fsKindOnVehicle.Count > 0)
                        {
                            fskindonvehicletype = (FSKindOnVechileType)fsKindOnVehicle[0];
                        }
                        else
                        {
                            vReturn = false;
                        }
                    }
                    else
                    {
                        vReturn = false;
                    }
                }
                else
                {
                    vReturn = false;
                }
            }

            return vReturn;
        }

        /// <summary>
        /// Cek MSP 
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="fsKind"></param>
        /// <param name="dealerCode"></param>
        /// <param name="mspStatus"></param>
        /// <returns></returns>
        public static bool IsAllowMSP(ChassisMaster chassisMaster, FSKind fsKind, DateTime serviceDate, List<ValidResult> validationResults)
        {
            #region initialize variable
            bool vReturn = true;
            var mspRegHistory = new MSPRegistrationHistory();
            var chassismaster = new ChassisMaster();
            var vehiclecolor = new VechileColor();
            var fskindonvehicletype = new FSKindOnVechileType();
            var mspMaster = new MSPMaster();
            #endregion


            #region initialize mapper
            var _chassisMaster = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            var _vehicleColor = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            var _fsKindOnVehicleType = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            var _mspMaster = MapperFactory.GetInstance().GetMapper(typeof(MSPMaster).ToString());
            var _chassismasterPKTMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
            var _mspRegHistory = MapperFactory.GetInstance().GetMapper(typeof(MSPRegistrationHistory).ToString());
            #endregion

            //CHECKING DATA MSP on Registration History
            var criterias_registration = new CriteriaComposite(new Criteria(typeof(MSPRegistrationHistory), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias_registration.opAnd(new Criteria(typeof(MSPRegistrationHistory), "MSPRegistration.ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            criterias_registration.opAnd(new Criteria(typeof(MSPRegistrationHistory), "Status", MatchType.Exact, 6));
            SortCollection sort_registration = new SortCollection();
            sort_registration.Add(new Sort(typeof(MSPRegistrationHistory), "RegistrationDate", Sort.SortDirection.DESC));

            var data = _mspRegHistory.RetrieveByCriteria(criterias_registration, sort_registration);
            if (data.Count > 0)
            {
                mspRegHistory = (MSPRegistrationHistory)data[0];
            }
            else
            {
                vReturn = false;

                // add error message request for msp with status not complete registration
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format(" No. Rangka {0} dan FS Kind Code {1} belum selesai registrasi MSP", chassisMaster.ChassisNumber, fsKind.KindCode)
                };
                validationResults.Add(validResult);
            }


            // check on method  ValidationHelper.ValidateFSKindOnVehicleType
            //CHECKING MSP  ON FSKINDONVEHICLETYPE
            //if (vReturn == true)
            //{
            //    //GET VEHICLE COLOR BASED ON CHASSIS NUMBER
            //    var criterias_chassis = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            //    criterias_chassis.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            //    var chassis = _chassisMaster.RetrieveByCriteria(criterias_chassis);
            //    if (chassis.Count > 0)
            //    {
            //        chassismaster = (ChassisMaster)chassis[0];
            //        //GET VEHICLE TYPE BASED ON VEHICLE COLOR
            //        var criterias_vehiclecolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            //        criterias_vehiclecolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, chassismaster.VechileColor.ID));
            //        var vehicleColor = _vehicleColor.RetrieveByCriteria(criterias_vehiclecolor);
            //        if (vehicleColor.Count > 0)
            //        {
            //            vehiclecolor = (VechileColor)vehicleColor[0];
            //            var criterias_fskindonvehicletype = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            //            criterias_fskindonvehicletype.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKind.KindCode));
            //            criterias_fskindonvehicletype.opAnd(new Criteria(typeof(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, vehiclecolor.VechileType.ID));
            //            var fsKindOnVehicle = _fsKindOnVehicleType.RetrieveByCriteria(criterias_fskindonvehicletype);
            //            if (fsKindOnVehicle.Count > 0)
            //            {
            //                fskindonvehicletype = (FSKindOnVechileType)fsKindOnVehicle[0];
            //            }
            //            else
            //            {
            //                vReturn = false;
            //            }
            //        }
            //        else
            //        {
            //            vReturn = false;
            //        }
            //    }
            //    else
            //    {
            //        vReturn = false;
            //    }
            //}

            // check on method  ValidationHelper.ValidateFSKindOnVehicleType
            //CHECKING MSP Duration
            //if (vReturn == true)
            //{
            //    //GET MSP Type Based On MSPExMaster
            //    var crt_MSPType = new CriteriaComposite(new Criteria(typeof(MSPMaster), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            //    crt_MSPType.opAnd(new Criteria(typeof(MSPMaster), "ID", MatchType.Exact, mspRegHistory.MSPMaster.ID));
            //    var mspType = _mspMaster.RetrieveByCriteria(crt_MSPType);
            //    if (mspType.Count > 0)
            //    {
            //        mspMaster = (MSPMaster)mspType[0];

            //        //GET PKT DATE
            //        var chassismasterPKT = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            //        chassismasterPKT.opAnd(new Criteria(typeof(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            //        var chassisPKTDate = _chassismasterPKTMapper.RetrieveByCriteria(chassismasterPKT);
            //        if (chassisPKTDate.Count > 0)
            //        {
            //            var chassismasterpktdate = (ChassisMasterPKT)chassisPKTDate[0];

            //            var maxClaimDate = chassismasterpktdate.PKTDate.AddYears(mspMaster.Duration);
            //            if (maxClaimDate < serviceDate)
            //            {
            //                var validResult = new ValidResult()
            //                {
            //                    IsValid = false,
            //                    ErrorCode = 20009,
            //                    Message = string.Format("Tanggal Service {0} melebihi Tanggal Maksimal Claim {1}", serviceDate, maxClaimDate.ToString("dd MMM yyyy"))
            //                };
            //                validationResults.Add(validResult);
            //                vReturn = false; ;
            //            }
            //        }
            //        else
            //        {
            //            var validResult = new ValidResult()
            //            {
            //                IsValid = false,
            //                ErrorCode = 20009,
            //                Message = string.Format("PKT Date dengan Chassis {0} tidak ditemukan", chassisMaster.ChassisNumber)
            //            };
            //            validationResults.Add(validResult);
            //            vReturn = false; ;
            //        }


            //    }
            //    else
            //    {
            //        vReturn = false;
            //    }

            //}

            return vReturn;
        }


        /// <summary>
        /// Validate if fs campaign allowed
        /// </summary>
        /// <param name="fsKind"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        private static bool IsAllowFSCampaign(FSKind fsKind, ChassisMaster chassisMaster, string dealerCode)
        {
            var _fsChassisCampaignMapper = MapperFactory.GetInstance().GetMapper(typeof(FSChassisCampaign).ToString());
            bool vReturn = false;
            FSChassisCampaign ObjFsChassisCampaign = new FSChassisCampaign();
            bool ObjIsByPass = false;
            CriteriaComposite criteriasFsChassisCampaign = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FSChassisCampaign), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criteriasFsChassisCampaign.opAnd(new Criteria(typeof(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            criteriasFsChassisCampaign.opAnd(new Criteria(typeof(KTB.DNet.Domain.FSChassisCampaign), "FSKind.ID", MatchType.Exact, fsKind.ID));
            ArrayList arrFsChassisCampaign = _fsChassisCampaignMapper.RetrieveByCriteria(criteriasFsChassisCampaign);
            ObjFsChassisCampaign.IsAllow = false;
            if (arrFsChassisCampaign.Count > 0)
            {
                ObjIsByPass = true;
                ObjFsChassisCampaign = ((FSChassisCampaign)(arrFsChassisCampaign[0]));
            }

            if (ObjIsByPass)
            {
                return ObjFsChassisCampaign.IsAllow;
            }

            // 'End of Bypass modul
            ArrayList arlFSCampaign = RetrieveFSCampaign();
            if ((arlFSCampaign.Count > 0))
            {
                foreach (FSCampaign objFSCampaign in arlFSCampaign)
                {
                    // Dealer checking
                    bool bDealer = true;
                    if (objFSCampaign.DealerChecked)
                    {
                        bDealer = false;
                        foreach (FSCampaignDealer objFSCampaignDealer in objFSCampaign.FSCampaignDealers)
                        {
                            if (objFSCampaignDealer.DealerCode == dealerCode)
                            {
                                bDealer = true;
                            }
                        }
                    }

                    // FSKind checking
                    bool bFSKind = true;
                    bool mspEx = true;
                    int mspExStatus = 0;
                    if (objFSCampaign.FSTypeChecked)
                    {
                        bFSKind = false;
                        mspEx = false;
                        foreach (FSCampaignKind objFSCampaignKind in objFSCampaign.FSCampaignKinds)
                        {
                            if (objFSCampaignKind.FSKind.KindCode == fsKind.KindCode)
                            {
                                bFSKind = true;
                                if (IsAllowMSPExt(chassisMaster, fsKind, dealerCode, out mspExStatus))
                                {
                                    if (mspExStatus == StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId)
                                    {
                                        mspEx = true;
                                    }
                                }
                            }
                        }
                    }

                    // VehicleType checking
                    bool bVehicle = true;
                    if (objFSCampaign.VehicleTypeChecked)
                    {
                        bVehicle = false;
                        foreach (FSCampaignVehicle objFSCampaignVehicle in objFSCampaign.FSCampaignVehicles)
                        {
                            if ((objFSCampaignVehicle.VechileType.ID == chassisMaster.VechileColor.VechileType.ID))
                            {
                                bVehicle = true;
                            }
                        }
                    }

                    // Faktur Validation checking
                    bool bFaktur = true;
                    if (objFSCampaign.FakturDateChecked)
                    {
                        bFaktur = false;
                        if (chassisMaster.EndCustomer != null)
                        {
                            if (objFSCampaign.DateFrom <= chassisMaster.EndCustomer.ValidateTime && objFSCampaign.DateTo >= chassisMaster.EndCustomer.ValidateTime)
                            {
                                bFaktur = true;
                            }
                        }
                        else
                        {
                            bFaktur = true;
                        }
                    }

                    // Combine value above
                    if ((bDealer && (bFSKind && (bVehicle && bFaktur))) || mspEx)
                    {
                        vReturn = true;
                        break;
                    }
                }
            }

            return vReturn;
        }

        /// <summary>
        /// Get fs campaign
        /// </summary>
        /// <returns></returns>
        private static ArrayList RetrieveFSCampaign()
        {
            var _fsCampaignMapper = MapperFactory.GetInstance().GetMapper(typeof(FSCampaign).ToString());
            ArrayList arlFSCampaign = new ArrayList();
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(FSCampaign), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(FSCampaign), "Status", MatchType.Exact, 0));
            arlFSCampaign = _fsCampaignMapper.RetrieveByCriteria(criterias);
            return arlFSCampaign;
        }
        #endregion

    }



}
