#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ElmahError repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:42
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Elmah;
using KTB.DNet.Interface.Repository.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class ElmahErrorRepository : BaseRepository<ELMAH_Error>, IElmahErrorRepository<ELMAH_Error, Guid>
    {
        #region Constructor
        public ElmahErrorRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public List<ELMAH_Error> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, string appName)
        {
            List<ELMAH_Error> data = new List<ELMAH_Error>();
            string filterXml = string.Empty;
            string filterHost = string.Empty;
            string filterType = string.Empty;
            string filterMessage = string.Empty;
            string filterAppName = string.IsNullOrEmpty(appName) ? string.Empty : appName;

            if (model.searchParams != null)
            {
                foreach (KeyValuePair<object, object> entry in model.searchParams)
                {
                    string paramValue = entry.Value.ToString();
                    string paramKey = entry.Key.ToString().ToLower();

                    if (!string.IsNullOrEmpty(paramValue))
                    {

                        string paramSubstringKey = paramKey.Substring(6);
                        switch (paramSubstringKey)
                        {
                            case "url":
                                filterXml = paramValue;
                                break;
                            case "host":
                                filterHost = paramValue;
                                break;
                            case "type":
                                filterType = paramValue;
                                break;
                            case "message":
                                filterMessage = paramValue;
                                break;
                            default:
                                break;
                        }

                    }
                }
            }

            var parameters = new
            {
                Application = filterAppName,
                AllXml = filterXml,
                Host = filterHost,
                Type = filterType,
                Message = filterMessage
            };

            string keyword = string.Empty;
            List<string> orderBy = null;
            filteredResultsCount = 0;

            GetPostModelData(model, "TimeUtc DESC", out keyword, out orderBy);

            List<ELMAH_Error> result = Search<ELMAH_Error>((connection, query, sqlParams) =>
            {
                return connection.Query<ELMAH_Error>(query, sqlParams).ToList();
            }, Connection, ElmahErrorQuery.SearchElmahError
            , "TimeUtc DESC", parameters, orderBy, out filteredResultsCount, model.Start, model.Length);

            totalResultsCount = filteredResultsCount;

            return result;

        }
        #endregion

        #region GetAsync
        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ELMAH_Error> GetAsync(Guid id)
        {
            try
            {
                using (var cn = Connection)
                {
                    var data = await cn.QueryAsync<ELMAH_Error>(ElmahErrorQuery.GetElmahErrorById, new { Id = id });
                    return data.SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region GetErrorDetailAsync
        /// <summary>
        /// GetErrorDetailAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        public async Task<string> GetErrorDetailAsync(Guid id, string application)
        {
            try
            {
                using (var cn = Connection)
                {
                    var errorDetail = await cn.QueryAsync<string>("ELMAH_GetErrorXml", new { Application = application, ErrorId = id }, commandType: CommandType.StoredProcedure);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(errorDetail.FirstOrDefault());
                    return JsonConvert.SerializeXmlNode(doc);
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
        #endregion

        #region Non implemented
        public List<ELMAH_Error> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public ELMAH_Error Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ELMAH_Error> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(ELMAH_Error entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(ELMAH_Error entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public ResponseMessage Delete(DateTime from, DateTime to)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                from = from.Date;
                to = to.Date.AddDays(1); // date < to

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(ElmahErrorQuery.DeleteElmahErrorWithInterval, new
                    {
                        From = from,
                        To = to

                    }, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Elmah log has been successfully deleted");

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete Elmah log " + GetInnerException(ex).Message;
            }

            return responseMessage;

        }
        #endregion

        #region Get Error Log for dashboard component

        #region GetErrorLogMainInfo
        /// <summary>
        /// GetErrorLogMainInfo
        /// </summary>
        /// <returns></returns>
        public async Task<JToken> GetErrorLogMainInfo()
        {
            try
            {
                JArray result = new JArray();
                int totalError = -1;
                IEnumerable<ELMAH_Error> listOfError = null;

                using (var connection = Connection)
                {
                    listOfError = await connection.QueryAsync<ELMAH_Error>(ElmahErrorQuery.GetErrorSummaryPerApplication);
                    totalError = await connection.ExecuteScalarAsync<int>(ElmahErrorQuery.GetTotalElmahError);
                }

                if (listOfError != null && listOfError.Count() > 0)
                {
                    foreach (ELMAH_Error error in listOfError)
                    {
                        JObject errorSumarryInfo = new JObject();
                        errorSumarryInfo.Add("description", error.Application);
                        errorSumarryInfo.Add("stats", error.Total);
                        errorSumarryInfo.Add("percent", decimal.Round((decimal)error.Total / totalError * 100, 2, MidpointRounding.AwayFromZero));

                        result.Add(errorSumarryInfo);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new JArray();
            }
        }
        #endregion

        #region GetErrorLogSummaryPerApplication
        /// <summary>
        /// GetErrorLogSummaryPerApplication
        /// </summary>
        /// <returns></returns>
        public async Task<JToken> GetErrorLogSummaryPerApplication()
        {
            try
            {
                JArray result = new JArray();
                IEnumerable<ELMAH_Error> listOfError = null;

                using (var connection = Connection)
                {
                    listOfError = await connection.QueryAsync<ELMAH_Error>(ElmahErrorQuery.GetErrorSummaryPerApplicationAndStatusCode);
                }

                if (listOfError != null && listOfError.Count() > 0)
                {
                    string appName = string.Empty;
                    string statusCode = string.Empty;

                    foreach (ELMAH_Error error in listOfError)
                    {
                        JObject errorSumarryPerApp = new JObject();
                        errorSumarryPerApp.Add("Application", error.Application);

                        switch (error.StatusCode)
                        {
                            case 0:
                                errorSumarryPerApp.Add("Log", error.Total);
                                break;
                            case 404:
                                errorSumarryPerApp.Add("Warning", error.Total);
                                break;
                            default:
                                errorSumarryPerApp.Add("Fatal", error.Total);
                                break;
                        }
                        result.Add(errorSumarryPerApp);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new JArray();
            }
        }
        #endregion

        #region GetLatestErrorLog
        /// <summary>
        /// GetLatestErrorLog
        /// </summary>
        /// <param name="take"></param>
        /// <param name="applicationName"></param>
        /// <param name="severity"></param>
        /// <returns></returns>
        public async Task<List<ELMAH_Error>> GetLatestErrorLog(int take, string applicationName, int severity)
        {
            try
            {
                IEnumerable<ELMAH_Error> listOfError = null;
                take = take <= 0 ? 6 : take;
                int filterStatusCode = -1;


                switch (severity)
                {
                    case 0:
                        filterStatusCode = 0;
                        break;
                    case 404:
                        filterStatusCode = 404;
                        break;
                    default:
                        // do nothing
                        break;
                }

                applicationName = string.IsNullOrEmpty(applicationName) ? string.Empty : applicationName;

                using (var cn = Connection)
                {
                    listOfError = await cn.QueryAsync<ELMAH_Error>(ElmahErrorQuery.GetLatestError, new { Take = take, StatusCode = filterStatusCode, AppName = applicationName });
                }

                if (listOfError != null && listOfError.Count() > 0)
                {
                    return listOfError.ToList();
                }

                return new List<ELMAH_Error>();
            }
            catch (Exception)
            {
                return new List<ELMAH_Error>();
            }
        }
        #endregion

        #region GetListOfApplication
        /// <summary>
        /// GetListOfApplication
        /// </summary>
        /// <returns></returns>
        public async Task<JToken> GetListOfApplication()
        {
            JArray result = new JArray();
            JObject defaultData = new JObject();
            defaultData.Add("label", "All application config");
            defaultData.Add("value", "");
            result.Add(defaultData);

            try
            {
                IEnumerable<string> listOfApplication = null;

                using (var cn = Connection)
                {
                    listOfApplication = await cn.QueryAsync<string>(ElmahErrorQuery.GetListOfApplication);
                }

                if (listOfApplication != null && listOfApplication.Count() > 0)
                {
                    foreach (string app in listOfApplication)
                    {
                        JObject appData = new JObject();
                        appData.Add("label", app);
                        appData.Add("value", app);
                        result.Add(appData);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        #endregion
        #endregion

    }
}
