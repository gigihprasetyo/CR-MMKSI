using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain.Search;
using System.Web.Configuration;
using KTB.DNet.DataMapper.Framework;
using System.Collections;
using KTB.DNET.BusinessFacade.Service;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class GSRRilisLogic
    {
        private const string sp_GetGSRStaging = "sp_GetGSRStaging";
        private const string sp_UpdateGSRStaging = "sp_UpdateGSRStaging";
        private const string sp_GSRRilisPM = "sp_GSRRilisPM";
        private const string sp_GSRRilisFS = "sp_GSRRilisFS";
        private const string sp_GSRRilisSIU = "sp_GSRRilisSIU";
        private const string updatedBy = "DNetHangfire";

        public static void Process()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            var _m_ServiceReminder = MapperFactory.GetInstance().GetMapper(typeof(ServiceReminder).ToString());
            var srFacade = new ServiceReminderFacade(User);

            try
            {
                //int status = 0;
                DataTable dtStaging = _m_ServiceReminder.RetrieveDataSet(sp_GetGSRStaging).Tables[0];
                foreach (DataRow dr in dtStaging.Rows)
                {
                    try
                    {
                        //status = string.IsNullOrEmpty(dr["Status"].ToString()) ? 0 : Convert.ToInt32(dr["Status"]);
                        if (dr["Tipe"].ToString().ToUpper().Trim() == "FS")
                            srFacade.RetrieveSp(string.Format("EXEC {0} @ID={1}, @LastUpdatedBy='{2}'",
                                sp_GSRRilisFS, Convert.ToInt32(dr["RilisID"]), updatedBy));
                        else if (dr["Tipe"].ToString().ToUpper().Trim() == "PM")
                            srFacade.RetrieveSp(string.Format("EXEC {0} @ID={1}, @LastUpdatedBy='{2}'",
                                sp_GSRRilisPM, Convert.ToInt32(dr["RilisID"]), updatedBy));
                        else if (dr["Tipe"].ToString().ToUpper().Trim() == "SIU")
                            srFacade.RetrieveSp(string.Format("EXEC {0} @ID={1}, @LastUpdatedBy='{2}'",
                                sp_GSRRilisSIU, Convert.ToInt32(dr["RilisID"]), updatedBy));

                        //srFacade.RetrieveSp(string.Format("EXEC {0} @ID={0}, @LastUpdatedBy='{1}', @Status=1, @Remark='Success'",
                        //    sp_UpdateGSRStaging, Convert.ToInt32(dr["ID"]), updatedBy));
                    }
                    catch (Exception e)
                    {
                        srFacade.RetrieveSp(string.Format("EXEC {0} @ID={1}, @LastUpdatedBy='{2}', @Status={3}, @Remark='{4}'",
                            sp_UpdateGSRStaging, Convert.ToInt32(dr["ID"]), updatedBy, 2, string.Format("Error : {0}", e.Message)));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
