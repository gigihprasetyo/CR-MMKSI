#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehiclepublicParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 10:12:00
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
    public class VWI_CRM_xts_vehiclepublicParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string xts_enginenumber { get; set; }
        [AntiXss]
        public string xjp_usecategory { get; set; }
        [AntiXss]
        public DateTime xts_deliverydate { get; set; }
        [AntiXss]
        public string ktb_kodefleet { get; set; }
        [AntiXss]
        public DateTime xjp_compulsoryinsuranceexpirationdate { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xjp_enginemodel { get; set; }
        [AntiXss]
        public string xjp_modifiedvehiclename { get; set; }
        [AntiXss]
        public int ktb_mspkm { get; set; }
        [AntiXss]
        public string xjp_recognizedmodelidname { get; set; }
        [AntiXss]
        public string xjp_usagecategoryname { get; set; }
        [AntiXss]
        public string xts_vehiclemodelidname { get; set; }
        [AntiXss]
        public int xjp_capacity2person { get; set; }
        [AntiXss]
        public Guid xts_productsegment4id { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public int xts_rearrearaxleweight { get; set; }
        [AntiXss]
        public Guid xjp_platenumbersegment1id { get; set; }
        [AntiXss]
        public Guid xjp_recognizedmodelid { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsurancevhcclassificationidname { get; set; }
        [AntiXss]
        public string ktb_dealernamednetmspex { get; set; }
        [AntiXss]
        public string xts_vehiclesizeclassidname { get; set; }
        [AntiXss]
        public Guid organizationid { get; set; }
        [AntiXss]
        public string xts_enginevolumeunit { get; set; }
        [AntiXss]
        public string xts_productidname { get; set; }
        [AntiXss]
        public decimal xjp_maximumload2 { get; set; }
        [AntiXss]
        public string ktb_speedtype { get; set; }
        [AntiXss]
        public string ktb_dealernamednet { get; set; }
        [AntiXss]
        public string ktb_isinterfacename { get; set; }
        [AntiXss]
        public DateTime ktb_fakturdate { get; set; }
        [AntiXss]
        public decimal xts_weight { get; set; }
        [AntiXss]
        public DateTime ktb_validuntil { get; set; }
        [AntiXss]
        public string xts_productsegment2idname { get; set; }
        [AntiXss]
        public decimal xts_height { get; set; }
        [AntiXss]
        public string ktb_description { get; set; }
        [AntiXss]
        public int xts_historymileage { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public int xjp_capacity1person { get; set; }
        [AntiXss]
        public bool ktb_isbackbone { get; set; }
        [AntiXss]
        public string xjp_compulsoryinsuranceexpirationdatejapan { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string ktb_isbackbonename { get; set; }
        [AntiXss]
        public decimal xjp_maximumload1 { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment1idname { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Guid xts_manufacturerid { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string ktb_solddealerid { get; set; }
        [AntiXss]
        public string ktb_vehiclebrandname { get; set; }
        [AntiXss]
        public Guid xts_vehiclemodelid { get; set; }
        [AntiXss]
        public Guid xts_productsegment1id { get; set; }
        [AntiXss]
        public decimal xts_enginevolume { get; set; }
        [AntiXss]
        public string xts_productexteriorcolor { get; set; }
        [AntiXss]
        public Guid xjp_bodyshapeid { get; set; }
        [AntiXss]
        public int xts_mileagebeforeexchanged { get; set; }
        [AntiXss]
        public string xts_fuelidname { get; set; }
        [AntiXss]
        public string xjp_bodyshapeidname { get; set; }
        [AntiXss]
        public string xjp_registrationmodel { get; set; }
        [AntiXss]
        public string ktb_vehicletypecodeidname { get; set; }
        [AntiXss]
        public string xts_vehiclename { get; set; }
        [AntiXss]
        public decimal xjp_grossweight2 { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment2 { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment3 { get; set; }
        [AntiXss]
        public string ktb_drivesystem { get; set; }
        [AntiXss]
        public string xjp_platenumbersegment4 { get; set; }
        [AntiXss]
        public int ktb_duration { get; set; }
        [AntiXss]
        public string xts_classificationnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string ktb_kodefsextended { get; set; }
        [AntiXss]
        public DateTime xts_registrationdate { get; set; }
        [AntiXss]
        public Guid ktb_productexteriorcolorid { get; set; }
        [AntiXss]
        public string xjp_automobilecategoryname { get; set; }
        [AntiXss]
        public string ktb_speedtypename { get; set; }
        [AntiXss]
        public decimal xts_length { get; set; }
        [AntiXss]
        public string xjp_vehicleinspectionperiod { get; set; }
        [AntiXss]
        public Guid xts_productsegment3id { get; set; }
        [AntiXss]
        public int xts_frontfrontaxleweight { get; set; }
        [AntiXss]
        public string ktb_descriptionmspex { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public Guid xjp_compulsoryinsurancevhcclassificationid { get; set; }
        [AntiXss]
        public string xts_chassisnumber { get; set; }
        [AntiXss]
        public string ktb_dealercodednetmspex { get; set; }
        [AntiXss]
        public string ktb_transmissionname { get; set; }
        [AntiXss]
        public string xjp_automobilecategory { get; set; }
        [AntiXss]
        public int ktb_durationmspex { get; set; }
        [AntiXss]
        public string ktb_transmission { get; set; }
        [AntiXss]
        public DateTime ktb_pktdate { get; set; }
        [AntiXss]
        public string ktb_mspnumber { get; set; }
        [AntiXss]
        public string xts_productdescription { get; set; }
        [AntiXss]
        public DateTime xts_customerreceiptdate { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string xts_keynumber { get; set; }
        [AntiXss]
        public Guid xts_productsegment2id { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationno { get; set; }
        [AntiXss]
        public string xjp_vehicleinspectionperiodname { get; set; }
        [AntiXss]
        public decimal xts_width { get; set; }
        [AntiXss]
        public Guid xts_fuelid { get; set; }
        [AntiXss]
        public string xts_chassismodel { get; set; }
        [AntiXss]
        public Guid xjp_insurancecategoryid { get; set; }
        [AntiXss]
        public string xjp_stampauthorityname { get; set; }
        [AntiXss]
        public DateTime ktb_validuntilmspex { get; set; }
        [AntiXss]
        public DateTime xts_lastmileagedate { get; set; }
        [AntiXss]
        public string xjp_infantcategoryname { get; set; }
        [AntiXss]
        public Guid xts_vehiclepublicid { get; set; }
        [AntiXss]
        public bool ktb_isinterface { get; set; }
        [AntiXss]
        public string ktb_dealercodednet { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string ktb_solddealerdesc { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string xjp_firstregistration { get; set; }
        [AntiXss]
        public string xts_productsegment1idname { get; set; }
        [AntiXss]
        public string xjp_lightvehiclecategoryname { get; set; }
        [AntiXss]
        public string ktb_drivesystemname { get; set; }
        [AntiXss]
        public string ktb_vehiclebrand { get; set; }
        [AntiXss]
        public bool xjp_modifiedvehicle { get; set; }
        [AntiXss]
        public DateTime xjp_vehicleinspectionexpirationdate { get; set; }
        [AntiXss]
        public string xjp_authorizedvehicletypeemissioncontrol { get; set; }
        [AntiXss]
        public int xts_frontrearaxleweight { get; set; }
        [AntiXss]
        public string xts_manufactureridname { get; set; }
        [AntiXss]
        public int xts_rearfrontaxleweight { get; set; }
        [AntiXss]
        public string xjp_insurancecategoryidname { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public string xts_platenumber { get; set; }
        [AntiXss]
        public DateTime ktb_openfaktur { get; set; }
        [AntiXss]
        public string xts_productsegment4idname { get; set; }
        [AntiXss]
        public string ktb_mspexnumber { get; set; }
        [AntiXss]
        public Guid xts_vehiclesizeclassid { get; set; }
        [AntiXss]
        public string xjp_stampauthoritynumber { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string xts_productsegment3idname { get; set; }
        [AntiXss]
        public string ktb_varianttype { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_productinteriorcolor { get; set; }
        [AntiXss]
        public string organizationidname { get; set; }
        [AntiXss]
        public string xts_productionyear { get; set; }
        [AntiXss]
        public string ktb_fakturstatusname { get; set; }
        [AntiXss]
        public bool xjp_infantcategory { get; set; }
        [AntiXss]
        public Guid ktb_vehicletypecodeid { get; set; }
        [AntiXss]
        public string xts_modelspecificationnumber { get; set; }
        [AntiXss]
        public string xjp_usagecategory { get; set; }
        [AntiXss]
        public decimal xjp_grossweight1 { get; set; }
        [AntiXss]
        public Guid xts_productid { get; set; }
        [AntiXss]
        public string xjp_usecategoryname { get; set; }
        [AntiXss]
        public DateTime xjp_completeinspectiondate { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public int ktb_mspexkm { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid xjp_usageid { get; set; }
        [AntiXss]
        public string xjp_lightvehiclecategory { get; set; }
        [AntiXss]
        public string xjp_deniedvehicletypeemissioncontrol { get; set; }
        [AntiXss]
        public string xts_enginevolumeunitname { get; set; }
        [AntiXss]
        public int xts_currentmileage { get; set; }
        [AntiXss]
        public string ktb_productexteriorcoloridname { get; set; }
        [AntiXss]
        public string xjp_usageidname { get; set; }
        [AntiXss]
        public string ktb_fakturstatus { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationnumber { get; set; }
        [AntiXss]
        public int statecode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
