#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : POOtherVendorProfile mapper class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using AutoMapper;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class POOtherVendorProfile : Profile
    {
        public POOtherVendorProfile()
        {
            CreateMap<POOtherVendor, POOtherVendorDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
            .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
            .ForMember(dest => dest.Address3, opt => opt.MapFrom(src => src.Address3))
            .ForMember(dest => dest.AllocationPeriod, opt => opt.MapFrom(src => src.AllocationPeriod))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.CloseRespon, opt => opt.MapFrom(src => src.CloseRespon))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DownPayment, opt => opt.MapFrom(src => src.DownPayment))
            .ForMember(dest => dest.DownPaymentAmountPaid, opt => opt.MapFrom(src => src.DownPaymentAmountPaid))
            .ForMember(dest => dest.DownPaymentIsPaid, opt => opt.MapFrom(src => src.DownPaymentIsPaid))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
            .ForMember(dest => dest.ExternalDocNo, opt => opt.MapFrom(src => src.ExternalDocNo))
            .ForMember(dest => dest.FormSource, opt => opt.MapFrom(src => src.FormSource))
            .ForMember(dest => dest.GrandTotal, opt => opt.MapFrom(src => src.GrandTotal))
            .ForMember(dest => dest.PaymentGroup, opt => opt.MapFrom(src => src.PaymentGroup))
            .ForMember(dest => dest.PersonInCharge, opt => opt.MapFrom(src => src.PersonInCharge))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
            .ForMember(dest => dest.PRPOType, opt => opt.MapFrom(src => src.PRPOType))
            .ForMember(dest => dest.PurchaseOrderNo, opt => opt.MapFrom(src => src.PurchaseOrderNo))
            .ForMember(dest => dest.SONo, opt => opt.MapFrom(src => src.SONo))
            .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.StockReferenceNo, opt => opt.MapFrom(src => src.StockReferenceNo))
            .ForMember(dest => dest.Taxable, opt => opt.MapFrom(src => src.Taxable))
            .ForMember(dest => dest.TermsOfPayment, opt => opt.MapFrom(src => src.TermsOfPayment))
            .ForMember(dest => dest.TotalAmountBeforeDiscount, opt => opt.MapFrom(src => src.TotalAmountBeforeDiscount))
            .ForMember(dest => dest.TotalBaseAmount, opt => opt.MapFrom(src => src.TotalBaseAmount))
            .ForMember(dest => dest.TotalConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalConsumptionTaxAmount))
            .ForMember(dest => dest.TotalDiscountAmount, opt => opt.MapFrom(src => src.TotalDiscountAmount))
            .ForMember(dest => dest.TotalTitleRegistrationFee, opt => opt.MapFrom(src => src.TotalTitleRegistrationFee))
            .ForMember(dest => dest.PurchaseOrderDate, opt => opt.MapFrom(src => src.PurchaseOrderDate))
            .ForMember(dest => dest.VendorDescription, opt => opt.MapFrom(src => src.VendorDescription))
            .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.WONo, opt => opt.MapFrom(src => src.WONo))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<POOtherVendorParameterDto, POOtherVendor>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
            .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
            .ForMember(dest => dest.Address3, opt => opt.MapFrom(src => src.Address3))
            .ForMember(dest => dest.AllocationPeriod, opt => opt.MapFrom(src => src.AllocationPeriod))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.DealerCode, opt => opt.MapFrom(src => src.DealerCode))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.CloseRespon, opt => opt.MapFrom(src => src.CloseRespon))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DownPayment, opt => opt.MapFrom(src => src.DownPayment))
            .ForMember(dest => dest.DownPaymentAmountPaid, opt => opt.MapFrom(src => src.DownPaymentAmountPaid))
            .ForMember(dest => dest.DownPaymentIsPaid, opt => opt.MapFrom(src => src.DownPaymentIsPaid))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
            .ForMember(dest => dest.ExternalDocNo, opt => opt.MapFrom(src => src.ExternalDocNo))
            .ForMember(dest => dest.FormSource, opt => opt.MapFrom(src => src.FormSource))
            .ForMember(dest => dest.GrandTotal, opt => opt.MapFrom(src => src.GrandTotal))
            .ForMember(dest => dest.PaymentGroup, opt => opt.MapFrom(src => src.PaymentGroup))
            .ForMember(dest => dest.PersonInCharge, opt => opt.MapFrom(src => src.PersonInCharge))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
            .ForMember(dest => dest.PRPOType, opt => opt.MapFrom(src => src.PRPOType))
            .ForMember(dest => dest.PurchaseOrderNo, opt => opt.MapFrom(src => src.PurchaseOrderNo))
            .ForMember(dest => dest.SONo, opt => opt.MapFrom(src => src.SONo))
            .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.StockReferenceNo, opt => opt.MapFrom(src => src.StockReferenceNo))
            .ForMember(dest => dest.Taxable, opt => opt.MapFrom(src => src.Taxable))
            .ForMember(dest => dest.TermsOfPayment, opt => opt.MapFrom(src => src.TermsOfPayment))
            .ForMember(dest => dest.TotalAmountBeforeDiscount, opt => opt.MapFrom(src => src.TotalAmountBeforeDiscount))
            .ForMember(dest => dest.TotalBaseAmount, opt => opt.MapFrom(src => src.TotalBaseAmount))
            .ForMember(dest => dest.TotalConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalConsumptionTaxAmount))
            .ForMember(dest => dest.TotalDiscountAmount, opt => opt.MapFrom(src => src.TotalDiscountAmount))
            .ForMember(dest => dest.TotalTitleRegistrationFee, opt => opt.MapFrom(src => src.TotalTitleRegistrationFee))
            .ForMember(dest => dest.PurchaseOrderDate, opt => opt.MapFrom(src => src.PurchaseOrderDate))
            .ForMember(dest => dest.VendorDescription, opt => opt.MapFrom(src => src.VendorDescription))
            .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.WONo, opt => opt.MapFrom(src => src.WONo))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
