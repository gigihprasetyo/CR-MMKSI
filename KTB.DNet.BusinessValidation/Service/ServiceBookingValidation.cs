using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KTB.DNet.BusinessValidation
{
    public static class ServiceBookingValidation
    {
        private const string up_IsValidServiceBooking = "up_IsValidServiceBooking";

        public static bool ValidateServiceBooking(ref List<ValidResult> validResultList, ref ServiceBooking svcBooking, 
            ref List<ServiceBookingActivity> svcBookingActivities, bool isUpdate = false)
        {
            if (svcBooking.Dealer == null)
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Kode dealer tidak ditemukan." });
            }
            else if (!svcBooking.CustomerPhoneNumber.All(Char.IsNumber))
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Nomor telp. konsumen tidak valid." });
            }
            else if (svcBooking.CustomerPhoneNumber.Length < 5 && svcBooking.CustomerPhoneNumber.Length > 12)
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Nomor telp. konsumen minimal 5 digit dan maksimal 12 digit." });
            }
            else if (svcBooking.PickupType == 0)
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Rencana kedatangan harus dipilih." });
            }
            else if (svcBooking.StallServiceType == 0)
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Tipe Stall tidak ditemukan." });
            }
            else if (svcBooking.WorkingTimeStart == null || svcBooking.WorkingTimeStart.Year < 1900)
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Rencana pengerjaan harus diisi." });
            }
            else if (svcBooking.IsMitsubishi != 1)
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Service Booking hanya untuk merk kendaraan Mitsubishi." });
            }
            else if (svcBooking.IsMitsubishi == 1)
            {
                if (svcBooking.VechileModel == null)
                {
                    validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Model kendaraan tidak ditemukan." });
                }
                else if (svcBooking.VechileType == null)
                {
                    validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Kode tipe kendaraan tidak ditemukan." });
                }
            }

            if (svcBookingActivities.Count == 0)
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Jenis Kegiatan dan Jenis Service harus dipilih." });
            }
            else if (svcBooking.PickupType == (int)EnumStallMaster.PickupType.DiTinggal &&
                    (svcBooking.IncomingDateStart == null || svcBooking.IncomingDateStart.Year < 1900 ||
                    svcBooking.IncomingDateEnd == null || svcBooking.IncomingDateEnd.Year < 1900))
            {
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Waktu kedatangan dari dan sampai harus diisi." });
            }
            //else if ((!isUpdate && !string.IsNullOrEmpty(svcBooking.ServiceBookingCode)) || (isUpdate && string.IsNullOrEmpty(svcBooking.ServiceBookingCode)))
            //{
            //    validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Invalid request" });
            //}

            ValidateServiceBookingActivity(ref svcBookingActivities, ref validResultList);

            if (IsBooked(svcBooking) && validResultList.Count == 0)
                validResultList.Add(new ValidResult { ErrorCode = 20009, Message = "Terdapat jadwal yang sudah di reservasi pada jam tersebut. Mohon untuk memilih jadwal yang lain." });

            return validResultList.Count == 0;
        }

        private static void ValidateServiceBookingActivity(ref List<ServiceBookingActivity> svcBookingActivities, ref List<ValidResult> validResultList)
        {
            var _m_StdMapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
            var _m_FSMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKind).ToString());
            var _m_PMMapper = MapperFactory.GetInstance().GetMapper(typeof(PMKind).ToString());
            var _m_RecallMapper = MapperFactory.GetInstance().GetMapper(typeof(RecallCategory).ToString());
            var _m_GRMapper = MapperFactory.GetInstance().GetMapper(typeof(GRKind).ToString());

            if (validResultList.Count == 0)
            {
                foreach (var item in svcBookingActivities)
                {

                    var servicetypeSB = _m_StdMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StandardCode), "Category", "ValueId", "ServiceBooking.ServiceType", item.ServiceTypeID.ToString())).Cast<StandardCode>().SingleOrDefault();
                    if (servicetypeSB != null)
                    {
                        if (servicetypeSB.ValueCode == "FS")
                        {
                            var fskind = _m_FSMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(FSKind), "KindCode", item.KindCode));
                            if (fskind.Count > 0)
                            {
                                var fskindresult = fskind[0] as FSKind;
                                item.KindCode = fskindresult.ID.ToString();
                            }
                        }
                        else if (servicetypeSB.ValueCode == "PM")
                        {
                            var pmkind = _m_PMMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(PMKind), "KindCode", item.KindCode));
                            if (pmkind.Count > 0)
                            {
                                var pmkindresult = pmkind[0] as PMKind;
                                item.KindCode = pmkindresult.ID.ToString();
                            }
                        }
                        else if (servicetypeSB.ValueCode == "FF")
                        {
                            var recallregcategory = _m_RecallMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(RecallCategory), "RecallRegNo", item.KindCode));
                            if (recallregcategory.Count > 0)
                            {
                                var recallregcategoryresult = recallregcategory[0] as RecallCategory;
                                item.KindCode = recallregcategoryresult.ID.ToString();
                            }
                        }
                        else if (servicetypeSB.ValueCode == "OTH")
                        {
                            var grKind = _m_GRMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(GRKind), "KindCode", item.KindCode));
                            if (grKind.Count > 0)
                            {
                                var grKindresult = grKind[0] as GRKind;
                                item.KindCode = grKindresult.ID.ToString();
                            }
                        }
                    }
                    else
                    {
                        validResultList.Add(new ValidResult { ErrorCode = 20009, Message = string.Format("Service Type : {0} tidak valid.", item.ServiceTypeID.ToString()) });
                    }
                }
            }
        }

        private static bool IsBooked(ServiceBooking svcBooking)
        {
            var _m_serviceBooking = MapperFactory.GetInstance().GetMapper(typeof(ServiceBooking).ToString());
            var _m_stallMaster = MapperFactory.GetInstance().GetMapper(typeof(StallMaster).ToString());

            ArrayList parameters = new ArrayList();
            parameters.Add(new SqlParameter {
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = svcBooking.ID,
                ParameterName = "@ServiceBookingID"
            });

            parameters.Add(new SqlParameter
            {
                DbType = DbType.Int16,
                Direction = ParameterDirection.Input,
                Value = svcBooking.Dealer.ID,
                ParameterName = "@DealerID"
            });

            parameters.Add(new SqlParameter
            {
                DbType = DbType.DateTime,
                Direction = ParameterDirection.Input,
                Value = svcBooking.WorkingTimeStart,
                ParameterName = "@StartTime"
            });

            parameters.Add(new SqlParameter
            {
                DbType = DbType.DateTime,
                Direction = ParameterDirection.Input,
                Value = svcBooking.WorkingTimeEnd,
                ParameterName = "@EndTime"
            });

            parameters.Add(new SqlParameter
            {
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Input,
                Value = 1,
                ParameterName = "@IsFromAPI"
            });

            DataTable dt = _m_serviceBooking.RetrieveDataSet(ServiceBookingValidation.up_IsValidServiceBooking, parameters).Tables[0];
            if (dt.Rows.Count > 0)
            {
                svcBooking.StallMaster = (StallMaster)_m_stallMaster.Retrieve(Convert.ToInt32(dt.Rows[0]["ID"].ToString()));
                svcBooking.StandardTime = Convert.ToDecimal(dt.Rows[0]["StandardTime"].ToString());
            }

            return dt.Rows.Count == 0;
        }
    }
}
