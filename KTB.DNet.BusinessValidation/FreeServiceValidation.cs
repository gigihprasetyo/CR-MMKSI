using KTB.DNet.Domain;
using System;
using System.Collections.Generic;

namespace KTB.DNet.BusinessValidation
{
    public class FreeServiceValidation
    {
        //+++++++++++++++++++++++++++++++++

        #region Constructor
        public FreeServiceValidation()
        {
        }
        #endregion

        #region Public Method
        public List<ValidResult> ValidateFreeService(ref FreeService freeService, string dealerCode = "", DateTime? ServiceDate = null)
        {
            var validResultList = new List<ValidResult>();
            //var soldDealer = new Dealer();
            //var fsKind = new FSKind();
            //var chassisMaster = new ChassisMaster();
            //var fsDealer = new Dealer();
            var isValid = false;
            var IsMSPEx = false;
            int mspExStatus = 0;

            ChassisMaster chassisMaster = null;
            FleetFaktur fleetFaktur = null;
            FSKind fsKind = null;
            ChassisMasterPKT chassismasterpkt = null;
            Dealer soldDealer = null;
            Dealer fsDealer = null;
            DealerBranch dealerBranch = null;
            bool isSoldSameWithFSDealer = false;

            // visit type
            isValid = ValidationHelper.ValidateVisitType(freeService, validResultList);

            // chassis and engine            
            isValid = ValidationHelper.ValidateChassisAndEngine(freeService.ChassisMaster.ChassisNumber, freeService.ChassisMaster.EngineNumber, validResultList, ref chassisMaster);
            if (chassisMaster != null)
            {
                freeService.ChassisMaster = chassisMaster;
            }

            // check fleet faktur exist
            var isFleetExist = ValidationHelper.ValidateFleetFaktur(freeService, ref fleetFaktur);
            if (fleetFaktur != null)
            {
                freeService.FleetRequest = fleetFaktur.FleetRequest;
            }
            // fs kind and check already PM or not
            isValid = ValidationHelper.ValidateFSKindCode(freeService, validResultList, ref fsKind, isFleetExist);
            if (fsKind != null)
            {
                freeService.FSKind = fsKind;
                isValid = ValidationHelper.ValidateAlreadyPM(freeService, validResultList, fsKind.FSType);
            }

            //validate chassismasterPKT Date and pkt date + duration > service date 
            if (chassisMaster != null)
            {
                isValid = ValidationHelper.ValidateFSKindOnVehicleType(chassisMaster, validResultList, fsKind, freeService.ServiceDate);
            }

            // service date
            isValid = ValidationHelper.ValidateServiceDate(freeService.ServiceDate, validResultList);

            // chassis dealer
            if (chassisMaster != null)
            {
                isValid = ValidationHelper.ValidateDealer(chassisMaster.Dealer.DealerCode, validResultList, "", ref soldDealer, false);
            }

            // fs dealer
            if (soldDealer != null)
            {
                fsDealer = freeService.Dealer;
                if (!string.IsNullOrEmpty(dealerCode))
                {
                    isValid = ValidationHelper.ValidateDealer(dealerCode, validResultList, dealerCode, ref fsDealer);
                }
                isValid = ValidationHelper.ValidateFSDealer(freeService.Dealer.DealerCode, validResultList, chassisMaster, soldDealer, ref fsDealer, ref isSoldSameWithFSDealer);
                if (fsDealer != null)
                {
                    freeService.Dealer = fsDealer;
                }
            }

            // dealer branch
            if (freeService.DealerBranch != null)
            {
                isValid = ValidationHelper.ValidateDealerBranch(freeService.Dealer.DealerCode, validResultList, freeService.DealerBranch.DealerBranchCode, ref dealerBranch);
                if (dealerBranch != null)
                {
                    if (dealerBranch.ID == 0)
                    {
                        freeService.DealerBranch = null;
                    }
                    else
                    {
                        freeService.DealerBranch = dealerBranch;
                    }
                }
                else
                {
                    freeService.DealerBranch = null;
                }
            }

            // is exist code for insert
            if (fsKind != null && chassisMaster != null)
            {
                // cek fs kind and vehicletype FSType on FSKindOnVechileType
                var fsType = ValidationHelper.GetFSTypebyKindVehicleType(fsKind, chassisMaster);

                IsMSPEx = ValidationHelper.IsAllowMSPExt(chassisMaster, fsKind, fsDealer.DealerCode, out mspExStatus, ServiceDate);
                if (IsMSPEx == true)
                {
                    if (mspExStatus == StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId)
                    {
                        isValid = ValidationHelper.IsAllowToInsertMSPEx(freeService.MileAge, chassisMaster, fsKind, validResultList, freeService.ServiceDate);
                    }
                    else
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format(" No. Rangka {0} dan FS Kind Code {1} belum selesai registrasi MSP Extended", chassisMaster.ChassisNumber, fsKind.KindCode)
                        };
                        validResultList.Add(validResult);
                        isValid = false;
                    }
                }
                else
                {
                    if (fsType == "MSPExtended")
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format(" No. Rangka {0} dan FS Kind Code {1} tidak registrasi MSP Extended", chassisMaster.ChassisNumber, fsKind.KindCode)
                        };
                        validResultList.Add(validResult);
                        isValid = false;
                    }
                    else if (fsType == "MSP")
                    {
                        isValid = ValidationHelper.IsAllowMSP(chassisMaster, fsKind, freeService.ServiceDate, validResultList);
                    }
                    else
                    {
                        // validate chassis and kind code
                        if (chassisMaster != null && fsDealer != null && fsKind != null)
                        {
                            isValid = ValidationHelper.ValidateChassisAndKindCode(freeService, validResultList, isFleetExist, chassisMaster, fsDealer, fsKind);
                        }
                    }
                }

                // validate fs claim can't more than 1x in by vehicle category
                if (isValid = ValidationHelper.IsExistMoreThan1xByVehicleCategory(chassisMaster, freeService.ServiceDate, validResultList))
                {
                    if (fsType == "MSPExtended")
                    {
                        // validate duplicate by chassis & fskind
                        isValid = ValidationHelper.IsExistCodeForInsertMSPEx(freeService.MileAge, chassisMaster, fsKind.ID, validResultList);
                    }
                    else
                    {
                        // validate duplicate by chassis & fskind
                        isValid = ValidationHelper.IsExistCodeForInsert(chassisMaster, fsKind.ID, validResultList);
                    }
                }
            }

            // validate fs code leading digit and service PKT Date
            if (chassisMaster != null && fsKind != null)
            {
                if (IsMSPEx == true)
                {
                    // validate duplicate by chassis & fskind
                    ValidationHelper.ValidateFSMSPExKindCodeLeadingDigit(freeService.MileAge, chassisMaster.ChassisNumber, fsKind.KindCode, validResultList);
                }
                else
                {
                    ValidationHelper.ValidateFSKindCodeLeadingDigit(chassisMaster.ChassisNumber, fsKind.KindCode, validResultList);
                }

                ValidationHelper.ValidateServicePKTDate(chassisMaster.ChassisNumber, freeService, fsKind.KindCode, isSoldSameWithFSDealer, validResultList);
            }

            return validResultList;
        }

        public List<ValidResult> CentalizeValidateInputFS(string kindCode, string chassisNumber, string engineNumber, string mileage, string stringServiceDate, string stringSoldPKTDate, string visitType, string woNumber)
        {
            var validResultList = new List<ValidResult>();

            #region KindCode Check
            if (kindCode.ToLower() == "-1")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Silahkan Pilih FsKind")
                };
                validResultList.Add(validResult);
            }
            #endregion

            #region ChassisNumber Check
            if (chassisNumber.Trim() == "")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Silahkan isi Nomor Rangka")
                };
                validResultList.Add(validResult);
            }
            #endregion

            #region EngineNumber Check
            if (engineNumber.Trim() == "")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Silahkan isi Nomor Mesin")
                };
                validResultList.Add(validResult);
            }
            #endregion

            #region Mileage Check
            if (mileage.Trim() == "")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Silahkan isi Jarak tempuh")
                };
                validResultList.Add(validResult);
            }
            #endregion

            #region ServiceDate Check
            if (stringServiceDate.Trim() == "")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Silahkan isi Tanggal Service")
                };
                validResultList.Add(validResult);
            }
            else
            {
                DateTime serviceDate = new DateTime();
                try
                {
                    serviceDate = Convert.ToDateTime(DateTime.ParseExact(stringServiceDate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture));
                    if ((serviceDate <= System.Data.SqlTypes.SqlDateTime.MinValue.Value) || (serviceDate >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value))
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("Format tanggal {0}  {1} salah. Tanggal harus dalam format ddmmyyyy.", "Tanggal Service", serviceDate)
                        };
                        validResultList.Add(validResult);
                    }
                }
                catch
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Format tanggal {0}  {1} salah. Tanggal harus dalam format ddmmyyyy.", "Tanggal Service", stringServiceDate)
                    };
                    validResultList.Add(validResult);
                }
            }
            #endregion

            #region SoldDate Check
            if (stringSoldPKTDate.Trim() == "")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Silahkan isi Tanggal penjualan/Tanggal PKT")
                };
                validResultList.Add(validResult);
            }
            else
            {
                DateTime soldDate = new DateTime();
                try
                {
                    soldDate = Convert.ToDateTime(DateTime.ParseExact(stringSoldPKTDate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture));
                    if ((soldDate <= System.Data.SqlTypes.SqlDateTime.MinValue.Value) || (soldDate >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value))
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format("Format tanggal {0}  {1} salah. Tanggal harus dalam format ddmmyyyy.", "Tanggal Penjualan/PKT", soldDate)
                        };
                        validResultList.Add(validResult);
                    }
                }
                catch
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Format tanggal {0}  {1} salah. Tanggal harus dalam format ddmmyyyy.", "Tanggal Penjualan/PKT", stringSoldPKTDate)
                    };
                    validResultList.Add(validResult);
                }
            }
            #endregion

            #region VisitType Check
            if (visitType == "")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Visit Type tidak valid"
                };
                validResultList.Add(validResult);
            }
            #endregion

            #region WONumber Check
            if (woNumber == "")
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Silahkan isi WO Number"
                };
                validResultList.Add(validResult);
            }
            #endregion

            #region Chassis Engine Check
            ChassisMaster chassisMaster = null;
            ValidationHelper.ValidateChassisAndEngine(chassisNumber, engineNumber, validResultList, ref chassisMaster);
            #endregion

            return validResultList;
        }

        public List<ValidResult> ValidateFreeServiceCentralize(ref FreeService freeService, string dealerCode = "", DateTime? ServiceDate = null)
        {
            var validResultList = new List<ValidResult>();
            //var soldDealer = new Dealer();
            //var fsKind = new FSKind();
            //var chassisMaster = new ChassisMaster();
            //var fsDealer = new Dealer();
            var isValid = false;
            var IsMSPEx = false;
            int mspExStatus = 0;

            ChassisMaster chassisMaster = null;
            FleetFaktur fleetFaktur = null;
            FSKind fsKind = null;
            ChassisMasterPKT chassismasterpkt = null;
            Dealer soldDealer = null;
            Dealer fsDealer = null;
            DealerBranch dealerBranch = null;
            bool isSoldSameWithFSDealer = false;

            //isMSP

            // visit type
            isValid = ValidationHelper.ValidateVisitType(freeService, validResultList);

            // chassis and engine            
            isValid = ValidationHelper.ValidateChassisAndEngine(freeService.ChassisMaster.ChassisNumber, freeService.ChassisMaster.EngineNumber, validResultList, ref chassisMaster);
            if (chassisMaster != null)
            {
                freeService.ChassisMaster = chassisMaster;
            }

            // check fleet faktur exist
            var isFleetExist = ValidationHelper.ValidateFleetFaktur(freeService, ref fleetFaktur);
            if (fleetFaktur != null)
            {
                freeService.FleetRequest = fleetFaktur.FleetRequest;
            }

            // fs kind and check already PM or not
            isValid = ValidationHelper.ValidateFSKindCode(freeService, validResultList, ref fsKind, isFleetExist);
            if (fsKind != null)
            {
                freeService.FSKind = fsKind;
                isValid = ValidationHelper.ValidateAlreadyPM(freeService, validResultList, fsKind.FSType);
            }


            #region Centralize Addition
            //ValidateSold/PKTDateInput
            // chassis dealer
            if (chassisMaster != null)
            {
                ValidationHelper.ValidateServiceDateToLastServiceDate(freeService, chassisMaster, validResultList);
            }

            ////FS Used Check
            //if (chassisMaster != null && fsKind != null)
            //{
            //    ValidationHelper.ValidateFSKindUsed(chassisMaster, fsKind, validResultList);
            //}

            #endregion

            //validate chassismasterPKT Date and pkt date + duration > service date 
            if (chassisMaster != null)
            {
                isValid = ValidationHelper.ValidateChassisMasterPKT(chassisMaster.ChassisNumber, validResultList, ref chassismasterpkt);
                if (chassismasterpkt == null || chassismasterpkt.ID == 0)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20009,
                        Message = string.Format("Chassis {0} pada Chassis Master PKT tidak ditemukan", chassisMaster.ChassisNumber)
                    };
                    validResultList.Add(validResult);
                    isValid = false;
                }
                else
                {
                    isValid = ValidationHelper.ValidateFSKindOnVehicleType(chassisMaster, validResultList, fsKind, freeService.ServiceDate);
                }
            }

            // service date
            isValid = ValidationHelper.ValidateServiceDate(freeService.ServiceDate, validResultList);

            // chassis dealer
            if (chassisMaster != null)
            {
                isValid = ValidationHelper.ValidateDealer(chassisMaster.Dealer.DealerCode, validResultList, "", ref soldDealer, false);
            }

            // fs dealer
            if (soldDealer != null)
            {
                fsDealer = freeService.Dealer;
                if (!string.IsNullOrEmpty(dealerCode))
                {
                    isValid = ValidationHelper.ValidateDealer(dealerCode, validResultList, dealerCode, ref fsDealer);
                }
                isValid = ValidationHelper.ValidateFSDealer(freeService.Dealer.DealerCode, validResultList, chassisMaster, soldDealer, ref fsDealer, ref isSoldSameWithFSDealer);
                if (fsDealer != null)
                {
                    freeService.Dealer = fsDealer;
                }
            }

            // dealer branch
            if (freeService.DealerBranch != null)
            {
                isValid = ValidationHelper.ValidateDealerBranch(freeService.Dealer.DealerCode, validResultList, freeService.DealerBranch.DealerBranchCode, ref dealerBranch);
                if (dealerBranch != null)
                {
                    if (dealerBranch.ID == 0)
                    {
                        freeService.DealerBranch = null;
                    }
                    else
                    {
                        freeService.DealerBranch = dealerBranch;
                    }
                }
                else
                {
                    freeService.DealerBranch = null;
                }
            }

            // sold date, remove karena sudah dicover di ValidateServicePKTDate
            //isValid = ValidationHelper.ValidateSoldDate(freeService, validResultList,pktdaet);

            // is exist code for insert
            if (fsKind != null && chassisMaster != null)
            {
                // cek fs kind and vehicletype FSType on FSKindOnVechileType
                var fsType = ValidationHelper.GetFSTypebyKindVehicleType(fsKind, chassisMaster);

                IsMSPEx = ValidationHelper.IsAllowMSPExt(chassisMaster, fsKind, fsDealer.DealerCode, out mspExStatus, ServiceDate);
                if (IsMSPEx == true)
                {
                    if (mspExStatus == StandardCodeHelper.GetByCategoryAndCode("MSPExtended.StatusMSPExRegistration", "Selesai").ValueId)
                    {
                        isValid = ValidationHelper.IsAllowToInsertMSPEx(chassisMaster, fsKind, validResultList);

                        //Centralize Addition
                        //FSKind Extended must Match and registered
                        ValidationHelper.ValidateFSKindMSPExt(chassisMaster, fsKind, validResultList);

                        //60Days after last Servicedate
                        ValidationHelper.ValidateMSPExtServiceDateAndToday(true, freeService.ServiceDate, validResultList);

                        //MSP Extended complete Payment
                        ValidationHelper.ValidateMSPExtPayment(chassisMaster, validResultList);
                    }
                    else
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format(" No. Rangka {0} dan FS Kind Code {1} belum selesai registrasi MSP Extended", chassisMaster.ChassisNumber, fsKind.KindCode)
                        };
                        validResultList.Add(validResult);
                    }

                }
                else
                {
                    if (fsType == "MSPExtended")
                    {
                        var validResult = new ValidResult()
                        {
                            IsValid = false,
                            ErrorCode = 20009,
                            Message = string.Format(" No. Rangka {0} dan FS Kind Code {1} tidak registrasi MSP Extended", chassisMaster.ChassisNumber, fsKind.KindCode)
                        };
                        validResultList.Add(validResult);
                    }
                    else if (fsType == "MSP")
                    {
                        isValid = ValidationHelper.IsAllowMSP(chassisMaster, fsKind, freeService.ServiceDate, validResultList);

                    }
                    else
                    {
                        // validate chassis and kind code
                        if (chassisMaster != null && fsDealer != null && fsKind != null)
                        {
                            isValid = ValidationHelper.ValidateChassisAndKindCode(freeService, validResultList, isFleetExist, chassisMaster, fsDealer, fsKind);
                        }
                        //FS Used Check
                        if (chassisMaster != null && fsKind != null)
                        {
                            ValidationHelper.ValidateFSKindUsed(chassisMaster, fsKind, validResultList);
                        }
                    }
                }
                               

                // validate fs claim can't more than 1x in by vehicle category
                if (isValid = ValidationHelper.IsExistMoreThan1xByVehicleCategory(chassisMaster, freeService.ServiceDate, validResultList))
                {
                    if (fsType == "MSPExtended")
                    {
                        // validate duplicate by chassis & fskind
                        isValid = ValidationHelper.IsExistCodeForInsertMSPEx(chassisMaster, fsKind.KindCode, validResultList);
                    }
                    else
                    {
                        // validate duplicate by chassis & fskind
                        isValid = ValidationHelper.IsExistCodeForInsert(chassisMaster, fsKind.KindCode, validResultList);
                    }
                }
            }

            if (!IsMSPEx)
            {
                // today to service date
                ValidationHelper.ValidateServiceDateAndToday(true, freeService.ServiceDate, validResultList);
            }


            // validate fs code leading digit and service PKT Date
            if (chassisMaster != null && fsKind != null)
            {
                if (IsMSPEx == true)
                {
                    // validate duplicate by chassis & fskind
                    ValidationHelper.ValidateFSMSPExKindCodeLeadingDigit(chassisMaster.ChassisNumber, fsKind.KindCode, validResultList);
                }
                else
                {
                    ValidationHelper.ValidateFSKindCodeLeadingDigit(chassisMaster.ChassisNumber, fsKind.KindCode, validResultList);
                }
                ValidationHelper.ValidateServicePKTDate(chassisMaster.ChassisNumber, freeService.ServiceDate, fsKind.KindCode, isSoldSameWithFSDealer, validResultList);
            }

            return validResultList;
        }

        public List<ValidResult> ValidateFreeServiceCentralizeRelease(ref FreeService freeService, string dealerCode = "", DateTime? ServiceDate = null)
        {
            var validResultList = new List<ValidResult>();
            int mspExStatus = 0;

            var fsType = ValidationHelper.GetFSTypebyKindVehicleType(freeService.FSKind, freeService.ChassisMaster);

            bool IsMSPEx = ValidationHelper.IsAllowMSPExt(freeService.ChassisMaster, freeService.FSKind, freeService.Dealer.DealerCode, out mspExStatus, freeService.ServiceDate);
            if (IsMSPEx == true)
            {
                //60Days after last Servicedate
                ValidationHelper.ValidateMSPExtServiceDateAndToday(false, freeService.ServiceDate, validResultList);
            }
            else
            {
                // today to service date
                ValidationHelper.ValidateServiceDateAndToday(false, freeService.ServiceDate, validResultList);
            }

            return validResultList;
        }


        public List<ValidResult> ValidateFreeServiceBB(ref FreeServiceBB freeServiceBB, string dealerCode)
        {
            var validResultList = new List<ValidResult>();
            var soldDealer = new Dealer();
            //var fsKind = new FSKind();
            //var chassisMasterBB = new ChassisMasterBB();
            //var fsDealer = new Dealer();
            //var dealerBranch = new DealerBranch();

            var isValid = false;
            ChassisMasterBB chassisMasterBB = null;
            FSKind fsKind = null;
            Dealer fsDealer = null;
            DealerBranch dealerBranch = null;
            // chassis and engine bb
            isValid = ValidationHelper.ValidateChassisAndEngineBB(freeServiceBB, validResultList, ref chassisMasterBB);
            if (chassisMasterBB != null)
            {
                freeServiceBB.ChassisMasterBB = chassisMasterBB;
            }
            // visit type
            isValid = ValidationHelper.ValidateVisitType(freeServiceBB, validResultList);

            // Valid CM for FS Special
            if (chassisMasterBB != null)
            {
                isValid = ValidationHelper.IsValidCMForFSSpecial(chassisMasterBB.ChassisNumber, validResultList);
            }

            // fs kind bb
            isValid = ValidationHelper.ValidateFSKindBB(freeServiceBB, validResultList, ref fsKind);
            if (fsKind != null)
            {
                freeServiceBB.FSKind = fsKind;
            }

            // service date
            isValid = ValidationHelper.ValidateServiceDate(freeServiceBB.ServiceDate, validResultList);

            // chassis dealer -- only for API call, not from DNet
            if (chassisMasterBB != null)
            {
                if (!string.IsNullOrEmpty(dealerCode))
                {
                    isValid = ValidationHelper.ValidateDealer(chassisMasterBB.Dealer.DealerCode, validResultList, dealerCode, ref soldDealer, false);
                }
                else
                {
                    // from DNet
                    isValid = ValidationHelper.ValidateDealer(chassisMasterBB.Dealer.DealerCode, validResultList, "", ref soldDealer, false);
                }
            }

            // fs dealer
            if (!string.IsNullOrEmpty(dealerCode))
            {
                isValid = ValidationHelper.ValidateDealer(freeServiceBB.Dealer.DealerCode, validResultList, dealerCode, ref fsDealer);
            }
            else
            {
                isValid = ValidationHelper.ValidateDealer(freeServiceBB.Dealer.DealerCode, validResultList, "", ref fsDealer, false);
            }

            if (fsDealer != null)
            {
                freeServiceBB.Dealer = fsDealer;
            }

            // dealer branch
            if (freeServiceBB.DealerBranch != null)
            {
                isValid = ValidationHelper.ValidateDealerBranch(freeServiceBB.Dealer.DealerCode, validResultList, freeServiceBB.DealerBranch.DealerBranchCode, ref dealerBranch);
                if (dealerBranch != null)
                {
                    if (dealerBranch.ID == 0)
                    {
                        freeServiceBB.DealerBranch = null;
                    }
                    else
                    {
                        freeServiceBB.DealerBranch = dealerBranch;
                    }
                }
                else
                {
                    freeServiceBB.DealerBranch = null;
                }
            }

            // sold date
            isValid = ValidationHelper.ValidateSoldDate(freeServiceBB, validResultList);

            // free service BB duplicate
            if (chassisMasterBB != null && fsKind != null)
            {
                isValid = ValidationHelper.ValidateFreeServiceBBDuplicate(chassisMasterBB, fsKind.ID, validResultList);
            }

            return validResultList;
        }
        #endregion


    }
}
