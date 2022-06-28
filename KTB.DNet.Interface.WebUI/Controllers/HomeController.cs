#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Home controller class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Configuration;
using System.Web.Mvc;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IMsAppVersionRepository<MsAppVersion, int> _msAppVersionRepo;
        private string ApiRoot = ConfigurationManager.AppSettings["ApiRoot"];
        private string TokenIssuer = ConfigurationManager.AppSettings["TokenUrl"];

        public HomeController(
            IMsAppVersionRepository<MsAppVersion, int> msAppVersionRepo)
        {
            _msAppVersionRepo = msAppVersionRepo;

            ViewBag.ApiRoot = ApiRoot;
            ViewBag.TokenIssuer = TokenIssuer;
            MsAppVersion appVersion = _msAppVersionRepo.GetCurrentDeploymentVersionApp(new Guid(AppConfigs.GetString("AppId")));
            ViewBag.Version = appVersion == null ? "-" : appVersion.Version;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}