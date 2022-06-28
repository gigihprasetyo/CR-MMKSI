#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDeliveryOrderProfile mapper class
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
    public class SparePartDeliveryOrderProfile : Profile
    {
        public SparePartDeliveryOrderProfile()
        {
            CreateMap<SparePartDeliveryOrder, SparePartDeliveryOrderDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
            .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
            .ForMember(dest => dest.Address3, opt => opt.MapFrom(src => src.Address3))
            .ForMember(dest => dest.Address4, opt => opt.MapFrom(src => src.Address4))
            .ForMember(dest => dest.BusinessPhone, opt => opt.MapFrom(src => src.BusinessPhone))
            .ForMember(dest => dest.BU, opt => opt.MapFrom(src => src.BU))
            .ForMember(dest => dest.CancellationDate, opt => opt.MapFrom(src => src.CancellationDate))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.CustomerContacts, opt => opt.MapFrom(src => src.CustomerContacts))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.CustomerNo, opt => opt.MapFrom(src => src.CustomerNo))
            .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
            .ForMember(dest => dest.DeliveryOrderNo, opt => opt.MapFrom(src => src.DeliveryOrderNo))
            .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.DeliveryType))
            .ForMember(dest => dest.ExternalReferenceNo, opt => opt.MapFrom(src => src.ExternalReferenceNo))
            .ForMember(dest => dest.GrandTotal, opt => opt.MapFrom(src => src.GrandTotal))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.MethodofPayment, opt => opt.MapFrom(src => src.MethodofPayment))
            .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.Salesperson, opt => opt.MapFrom(src => src.Salesperson))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.TermofPayment, opt => opt.MapFrom(src => src.TermofPayment))
            .ForMember(dest => dest.TotalAmountBeforeDiscount, opt => opt.MapFrom(src => src.TotalAmountBeforeDiscount))
            .ForMember(dest => dest.TotalBaseAmount, opt => opt.MapFrom(src => src.TotalBaseAmount))
            .ForMember(dest => dest.TotalDiscountAmount, opt => opt.MapFrom(src => src.TotalDiscountAmount))
            .ForMember(dest => dest.TotalMiscChargeBaseAmount, opt => opt.MapFrom(src => src.TotalMiscChargeBaseAmount))
            .ForMember(dest => dest.TotalMiscChargeConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalMiscChargeConsumptionTaxAmount))
            .ForMember(dest => dest.TotalReceipt, opt => opt.MapFrom(src => src.TotalReceipt))
            .ForMember(dest => dest.TotalConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalConsumptionTaxAmount))
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
            .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<SparePartDeliveryOrderParameterDto, SparePartDeliveryOrder>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
            .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
            .ForMember(dest => dest.Address3, opt => opt.MapFrom(src => src.Address3))
            .ForMember(dest => dest.Address4, opt => opt.MapFrom(src => src.Address4))
            .ForMember(dest => dest.BusinessPhone, opt => opt.MapFrom(src => src.BusinessPhone))
            .ForMember(dest => dest.BU, opt => opt.MapFrom(src => src.BU))
            .ForMember(dest => dest.CancellationDate, opt => opt.MapFrom(src => src.CancellationDate))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.CustomerContacts, opt => opt.MapFrom(src => src.CustomerContacts))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.CustomerNo, opt => opt.MapFrom(src => src.CustomerNo))
            .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
            .ForMember(dest => dest.DeliveryOrderNo, opt => opt.MapFrom(src => src.DeliveryOrderNo))
            .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.DeliveryType))
            .ForMember(dest => dest.ExternalReferenceNo, opt => opt.MapFrom(src => src.ExternalReferenceNo))
            .ForMember(dest => dest.GrandTotal, opt => opt.MapFrom(src => src.GrandTotal))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.MethodofPayment, opt => opt.MapFrom(src => src.MethodofPayment))
            .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType))
            .ForMember(dest => dest.ReferenceNo, opt => opt.MapFrom(src => src.ReferenceNo))
            .ForMember(dest => dest.Salesperson, opt => opt.MapFrom(src => src.Salesperson))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.TermofPayment, opt => opt.MapFrom(src => src.TermofPayment))
            .ForMember(dest => dest.TotalAmountBeforeDiscount, opt => opt.MapFrom(src => src.TotalAmountBeforeDiscount))
            .ForMember(dest => dest.TotalBaseAmount, opt => opt.MapFrom(src => src.TotalBaseAmount))
            .ForMember(dest => dest.TotalDiscountAmount, opt => opt.MapFrom(src => src.TotalDiscountAmount))
            .ForMember(dest => dest.TotalMiscChargeBaseAmount, opt => opt.MapFrom(src => src.TotalMiscChargeBaseAmount))
            .ForMember(dest => dest.TotalMiscChargeConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalMiscChargeConsumptionTaxAmount))
            .ForMember(dest => dest.TotalReceipt, opt => opt.MapFrom(src => src.TotalReceipt))
            .ForMember(dest => dest.TotalConsumptionTaxAmount, opt => opt.MapFrom(src => src.TotalConsumptionTaxAmount))
            .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))
            .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                //.ForMember(dest => dest.SparePartDeliveryOrderDetails, opt => opt.MapFrom(src => src.SparePartDeliveryOrderDetails))
            .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
