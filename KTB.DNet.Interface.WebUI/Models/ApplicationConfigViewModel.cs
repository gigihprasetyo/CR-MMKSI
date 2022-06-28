#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ApplicationConfig ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion


using KTB.DNet.Interface.Framework.Enums;
namespace KTB.DNet.Interface.WebUI.Models
{
    public class ApplicationConfigViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ConfigKey { get; set; }

        public string Value { get; set; }

        public ConfigDataType DataType { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}