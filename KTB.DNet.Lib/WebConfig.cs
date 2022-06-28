using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System;
using System.Collections;

namespace KTB.DNet.Lib
{
    public class WebConfig
    {
        /// <summary>
        /// Return Key Value From DataBase By KeyName
        /// </summary>
        /// <param name="KeyName">KeyName</param>
        /// <returns>AppCOnfig.Value</returns>
        public static string GetValue(string KeyName)
        {
            try
            {


                string AppID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID");
                string value = System.Configuration.ConfigurationSettings.AppSettings.Get(KeyName);
                return value;
                CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                crit.opAnd(new Criteria(typeof(KTB.DNet.Domain.AppConfig), "Name", MatchType.Exact, KeyName));
                crit.opAnd(new Criteria(typeof(KTB.DNet.Domain.AppConfig), "AppID", MatchType.Exact, AppID));

                ArrayList DataRow;
                IMapper mapper = MapperFactory.GetInstance().GetMapper(typeof(KTB.DNet.Domain.AppConfig).ToString());
                DataRow = mapper.RetrieveByCriteria(crit);

                //DataRow = new AppConfigFacade(null).RetrieveByCriteria(crit);
                if (DataRow.Count == 0)
                {
                    KTB.DNet.Domain.AppConfig ObjAppConfig = new AppConfig();
                    ObjAppConfig.AppID = AppID;
                    ObjAppConfig.Value = string.Empty;
                    ObjAppConfig.Name = KeyName;
                    ObjAppConfig.RowStatus = 0;
                    ObjAppConfig.Status = 0;
                    ObjAppConfig.LastUpdateBy = "Not FOund";
                    ObjAppConfig.CreatedBy = "Not Found";
                    ObjAppConfig.CreatedTime = DateTime.Now;
                    ObjAppConfig.LastUpdateTime = DateTime.Now;
                    mapper.Insert(ObjAppConfig, "KTB.DNet.Lib");

                    //mapper.Insert()
                    return string.Empty;

                }

                // throw new Exception("Key Name (" + KeyName + ") is Not Found....");
                else
                {
                    AppConfig Row = (AppConfig)(DataRow[0]);

                    return Row.Value;
                }

            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public static string GetString(string KeyName)
        {
            try
            {
                //string AppID = System.Configuration.ConfigurationSettings.AppSettings.Get("AppID");
                //string value = System.Configuration.ConfigurationSettings.AppSettings.Get(KeyName);
                //return value;
                CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                crit.opAnd(new Criteria(typeof(KTB.DNet.Domain.AppConfig), "Name", MatchType.Exact, KeyName));
                //crit.opAnd(new Criteria(typeof(KTB.DNet.Domain.AppConfig), "AppID", MatchType.Exact, AppID));

                ArrayList DataRow;
                IMapper mapper = MapperFactory.GetInstance().GetMapper(typeof(KTB.DNet.Domain.AppConfig).ToString());
                DataRow = mapper.RetrieveByCriteria(crit);

                //DataRow = new AppConfigFacade(null).RetrieveByCriteria(crit);
                if (DataRow.Count == 0)
                {
                    KTB.DNet.Domain.AppConfig ObjAppConfig = new AppConfig();
                    ObjAppConfig.AppID = "";
                    ObjAppConfig.Value = string.Empty;
                    ObjAppConfig.Name = KeyName;
                    ObjAppConfig.RowStatus = 0;
                    ObjAppConfig.Status = 0;
                    ObjAppConfig.LastUpdateBy = "Not FOund";
                    ObjAppConfig.CreatedBy = "Not Found";
                    ObjAppConfig.CreatedTime = DateTime.Now;
                    ObjAppConfig.LastUpdateTime = DateTime.Now;
                    mapper.Insert(ObjAppConfig, "KTB.DNet.Lib");

                    //mapper.Insert()
                    return string.Empty;

                }

                // throw new Exception("Key Name (" + KeyName + ") is Not Found....");
                else
                {
                    AppConfig Row = (AppConfig)(DataRow[0]);

                    return Row.Value;
                }

            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}
