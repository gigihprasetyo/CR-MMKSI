using KTB.DNet.Interface.Framework.Common;
using KTB.DNet.Interface.Framework.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace KTB.DNet.Interface.Framework.Helper
{
    public static class ThrottleHelper
    {
        /// <summary>
        /// Create or Update throttle endpoints
        /// </summary>
        /// <param name="throttleInfo"></param>
        public static void Save(IThrottleInfo throttleInfo)
        {
            XDocument throttleConfigs = null;
            bool success = false;
            CustomUserImpersonater imp = null;

            try
            {
                string path = AppConfigs.GetString("ThrottleFilePath");

                imp = GetUserImpersonater();

                if (imp != null)
                {
                    success = imp.Start();
                    if (success)
                    {
                        throttleConfigs = XDocument.Load(path);

                        if (throttleConfigs != null)
                        {

                            var endpoints = throttleConfigs.Root.Elements("Endpoint");
                            string uri = throttleInfo.GetURI();
                            if (!string.IsNullOrEmpty(uri))
                            {
                                endpoints.Where(x => x.Value.Contains(uri)).Remove();

                                XElement newEndpoint = new XElement("Endpoint",
                                    new XElement("Id", throttleInfo.Id),
                                    new XElement("URI", throttleInfo.GetURI()),
                                    new XElement("Limit", throttleInfo.RequestLimit),
                                    new XElement("Time", throttleInfo.TimeInSeconds),
                                    new XElement("Enable", throttleInfo.Enable));

                                throttleConfigs.Descendants("ThrottleConfig").FirstOrDefault().Add(newEndpoint);

                                throttleConfigs.Save(path);
                            }
                        }

                        imp.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update the configuration file. " + ex.Message);
            }
            finally
            {
                if (imp != null)
                {
                    imp.Stop();
                    imp.Dispose();
                }
            }
        }

        /// <summary>
        /// Remove throttle from configuration file
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(IThrottleInfo throttleInfo)
        {
            XDocument throttleConfigs = null;
            bool success = false;
            CustomUserImpersonater imp = null;

            try
            {
                string path = AppConfigs.GetString("ThrottleFilePath");

                imp = GetUserImpersonater();
                if (imp != null)
                {
                    success = imp.Start();
                    if (success)
                    {
                        throttleConfigs = XDocument.Load(path);

                        if (throttleConfigs != null)
                        {
                            var endpoints = throttleConfigs.Root.Elements("Endpoint");
                            string uri = throttleInfo.GetURI();
                            if (!string.IsNullOrEmpty(uri))
                            {
                                endpoints.Where(x => x.Value.Contains(uri)).Remove();
                            }
                            throttleConfigs.Save(path);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete throttler from file configuration. " + ex.Message);
            }
            finally
            {
                if (imp != null)
                {
                    imp.Stop();
                    imp.Dispose();
                }
            }

        }

        /// <summary>
        /// Export throttle from database
        /// </summary>
        /// <param name="listOfThrottleInfo"></param>
        /// <returns></returns>
        public static bool Export<T>(List<T> listOfThrottleInfo) where T : IThrottleInfo
        {
            XDocument xDocument = null;
            bool success = false;
            CustomUserImpersonater imp = null;

            try
            {
                string path = AppConfigs.GetString("ThrottleFilePath");

                if (path != null)
                {
                    imp = GetUserImpersonater();
                    if (imp != null)
                    {
                        success = imp.Start();
                        if (success)
                        {

                            if (File.Exists(path))
                            {
                                xDocument = XDocument.Load(path);
                                var endpoints = xDocument.Root.Elements("Endpoint");
                                foreach (var item in endpoints.ToList())
                                {
                                    item.Remove();
                                }
                            }
                            else
                            {
                                xDocument = new XDocument(
                                    new XElement("ThrottleConfig"));
                            }
                           
                            for (int i = 0; i < listOfThrottleInfo.Count; i++)
                            {
                                string uri = listOfThrottleInfo[i].GetURI();
                                if (!string.IsNullOrEmpty(uri))
                                {
                                    XElement newEndPoint = new XElement("Endpoint",
                                                new XElement("Id", listOfThrottleInfo[i].Id),
                                                new XElement("URI", uri),
                                                new XElement("Limit", listOfThrottleInfo[i].RequestLimit),
                                                new XElement("Time", listOfThrottleInfo[i].TimeInSeconds),
                                                new XElement("Enable", listOfThrottleInfo[i].Enable));

                                    xDocument.Descendants("ThrottleConfig").FirstOrDefault().Add(newEndPoint);
                                }
                            }

                            xDocument.Save(path);
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (imp != null)
                {
                    imp.Stop();
                    imp.Dispose();
                }
            }

            return false;
        }

        public static IThrottleInfo GetThrottleInfo<T>(string uri) where T : class, new()
        {
            try
            {
                // Get throttle info from xml
                XDocument throttleConfigs = null;
                throttleConfigs = ThrottleHelper.GetThrottleConfigXml();

                if (throttleConfigs != null)
                {
                    foreach (XElement xe in throttleConfigs.Descendants("Endpoint"))
                    {
                        if (xe.Element("URI").Value == uri)
                        {
                            T throttleInfo = new T();
                            IThrottleInfo info = (IThrottleInfo)throttleInfo;
                            info.Enable = bool.Parse(xe.Element("Enable").Value);
                            info.RequestLimit = int.Parse(xe.Element("Limit").Value);
                            info.TimeInSeconds = int.Parse(xe.Element("Time").Value);

                            return info;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }

            return null;
        }

        #region Get Throttle Xml Config
        /// <summary>
        /// Get Throttle Config xml
        /// </summary>
        /// <returns></returns>
        public static XDocument GetThrottleConfigXml()
        {
            XDocument xDocument = null;
            bool success = false;
            CustomUserImpersonater imp = null;
            try
            {
                // set credentials to access repository server
                imp = GetUserImpersonater();

                if (imp != null)
                {
                    success = imp.Start();
                    if (success)
                    {
                        string filepath = AppConfigs.GetString("ThrottleFilePath");

                        xDocument = XDocument.Load(filepath);
                        imp.Stop();
                        imp.Dispose();
                    }
                }

                return xDocument;
            }
            catch
            {
                return xDocument;
            }
            finally
            {
                if (imp != null)
                {
                    imp.Stop();
                    imp.Dispose();
                }
            }
        }
        #endregion

        public static bool IsThrottleConfigXmlExist()
        {
            CustomUserImpersonater imp = GetUserImpersonater();
            bool isExist = false;
            if (imp != null)
            {
                bool success = imp.Start();
                if (success)
                {
                    isExist = File.Exists(AppConfigs.GetString("ThrottleFilePath"));
                }
            }

            return isExist;
        }

        /// <summary>
        /// Get user impersonater
        /// </summary>
        /// <returns></returns>
        private static CustomUserImpersonater GetUserImpersonater()
        {
            try
            {
                // set credentials to access repository server
                string user = AppConfigs.GetString("User");
                string password = AppConfigs.GetString("Password");
                string webServer = AppConfigs.GetString("WebServer");
                CustomUserImpersonater imp = new CustomUserImpersonater(user, password, webServer);

                return imp;
            }
            catch
            {
                return null;
            }
        }



    }
}
