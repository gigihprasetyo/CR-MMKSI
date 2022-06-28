using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.Domain;
using System.Data;

namespace KTB.DNet.SFIntegration.Parser
{
    public class CustomerCaseResponseParser
    {
        public static CustomerCaseResponse Parse(DataRow row)
        {
            CustomerCase tempCustomerCase = new CustomerCase();
            ServiceBooking tempServiceBooking = new ServiceBooking();
            CustomerCaseResponse ccRes = new CustomerCaseResponse();

            if (row["ID"] != DBNull.Value) { ccRes.ID = (int)row["ID"]; }
            if (row["CustomerCaseID"] != DBNull.Value) { tempCustomerCase.ID = (int)row["CustomerCaseID"]; ccRes.CustomerCase = tempCustomerCase; }
            if (row["ServiceBookingID"] != DBNull.Value) { tempServiceBooking.ID = (int)row["ServiceBookingID"]; ccRes.ServiceBooking = tempServiceBooking; }
            if (row["WorkOrderNumber"] != DBNull.Value) { ccRes.WorkOrderNumber = row["WorkOrderNumber"].ToString(); }
            if (row["Subject"] != DBNull.Value) { ccRes.Subject = row["Subject"].ToString(); }
            if (row["Description"] != DBNull.Value) { ccRes.Description = row["Description"].ToString(); }
            if (row["Response"] != DBNull.Value) { ccRes.Response = (short)row["Response"]; }
            if (row["BookingDatetime"] != DBNull.Value) { ccRes.BookingDatetime = Convert.ToDateTime(row["BookingDatetime"].ToString()); }
            if (row["Status"] != DBNull.Value) { ccRes.Status = (short)row["Status"]; }
            if (row["IsSend"] != DBNull.Value) { ccRes.IsSend = (short)row["IsSend"]; }
            if (row["RowStatus"] != DBNull.Value) { ccRes.RowStatus = (short)row["RowStatus"]; }
            if (row["CreatedBy"] != DBNull.Value) { ccRes.CreatedBy = row["CreatedBy"].ToString(); }
            if (row["CreatedTime"] != DBNull.Value) { ccRes.CreatedTime = Convert.ToDateTime(row["CreatedTime"]); }
            if (row["LastUpdateBy"] != DBNull.Value) { ccRes.LastUpdateBy = row["LastUpdateBy"].ToString(); }
            if (row["LastUpdateTime"] != DBNull.Value) { ccRes.LastUpdateTIme = Convert.ToDateTime(row["LastUpdateTIme"]); }

            return ccRes;
        }
    }
}
