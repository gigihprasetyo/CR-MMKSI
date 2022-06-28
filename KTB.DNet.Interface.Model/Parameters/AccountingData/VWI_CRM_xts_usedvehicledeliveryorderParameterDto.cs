#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_usedvehicledeliveryorderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitid { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string xts_locationidname { get; set; }
        [AntiXss]
        public string xts_status { get; set; }
        [AntiXss]
        public Guid xts_customerid { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public bool xts_vehicledeliveryplace { get; set; }
        [AntiXss]
        public Guid processid { get; set; }
        [AntiXss]
        public Guid xts_personinchargeid { get; set; }
        [AntiXss]
        public string xjp_recognizedmodelidname { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string xts_platenumber { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public string xts_vehiclemodelidname { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public Guid xts_vehiclemodelid { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public Int32 statuscode { get; set; }
        [AntiXss]
        public DateTime xjp_mileageexchangedate { get; set; }
        [AntiXss]
        public string xts_customeridyominame { get; set; }
        [AntiXss]
        public Guid xts_vehicletransactionhistoryid { get; set; }
        [AntiXss]
        public string xts_usedvehicleexteriorcoloridname { get; set; }
        [AntiXss]
        public string xts_productidname { get; set; }
        [AntiXss]
        public Guid xts_usedvehicleexteriorcolorid { get; set; }
        [AntiXss]
        public Int32 statecode { get; set; }
        [AntiXss]
        public string xts_driver { get; set; }
        [AntiXss]
        public Int32 timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Int32 xts_mileage { get; set; }
        [AntiXss]
        public string xts_customercontactidname { get; set; }
        [AntiXss]
        public string xts_customercontactidyominame { get; set; }
        [AntiXss]
        public Guid xts_customercontactid { get; set; }
        [AntiXss]
        public string xts_customernumber { get; set; }
        [AntiXss]
        public Guid xts_usedvehicledeliveryorderid { get; set; }
        [AntiXss]
        public string traversedpath { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_personinchargeidname { get; set; }
        [AntiXss]
        public Guid xts_productid { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Int32 utcconversiontimezonecode { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public Guid xjp_recognizedmodelid { get; set; }
        [AntiXss]
        public string xts_statusname { get; set; }
        [AntiXss]
        public string xts_handling { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public Int32 importsequencenumber { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public Guid xjp_servicestoreid { get; set; }
        [AntiXss]
        public Int32 xjp_mileagebeforeexchange { get; set; }
        [AntiXss]
        public string xts_customeridname { get; set; }
        [AntiXss]
        public DateTime xts_transactiondate { get; set; }
        [AntiXss]
        public Guid xts_siteid { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string xjp_servicestoreidname { get; set; }
        [AntiXss]
        public Guid stageid { get; set; }
        [AntiXss]
        public Guid xts_usedvehiclesalesorderid { get; set; }
        [AntiXss]
        public string xts_vehicledeliveryplacename { get; set; }
        [AntiXss]
        public string xts_warehouseidname { get; set; }
        [AntiXss]
        public string xts_customerlookupname { get; set; }
        [AntiXss]
        public string xts_vehicletransactionhistoryidname { get; set; }
        [AntiXss]
        public Guid xts_locationid { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public Int32 xts_customerlookuptype { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationnumber { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public Guid xts_stockid { get; set; }
        [AntiXss]
        public string xts_stockidname { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitidname { get; set; }
        [AntiXss]
        public string xjp_productionyearjapan { get; set; }
        [AntiXss]
        public string xts_vehicledeliverynumber { get; set; }
        [AntiXss]
        public string xts_usedvehiclesalesorderidname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_siteidname { get; set; }
        [AntiXss]
        public Guid xts_warehouseid { get; set; }
        [AntiXss]
        public string xts_handlingname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public Guid xts_ownerid { get; set; }
        [AntiXss]
        public string xts_owneridname { get; set; }
        [AntiXss]
        public string xts_exteriorcolor { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
