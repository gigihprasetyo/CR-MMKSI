#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DealerBranchProfile mapper class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
using AutoMapper;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
#endregion

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public class DealerBranchProfile : Profile
    {
        public DealerBranchProfile()
        {
            CreateMap<DealerBranch, DealerBranchDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.DealerID, opt => opt.MapFrom(src => src.Dealer.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.TypeBranch, opt => opt.MapFrom(src => src.TypeBranch))
                .ForMember(dest => dest.DealerBranchCode, opt => opt.MapFrom(src => src.DealerBranchCode))
                .ForMember(dest => dest.Term1, opt => opt.MapFrom(src => src.Term1))
                .ForMember(dest => dest.Term2, opt => opt.MapFrom(src => src.Term2))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.BranchAssignmentNo, opt => opt.MapFrom(src => src.BranchAssignmentNo))
                .ForMember(dest => dest.BranchAssignmentDate, opt => opt.MapFrom(src => src.BranchAssignmentDate))
                .ForMember(dest => dest.SalesUnitFlag, opt => opt.MapFrom(src => src.SalesUnitFlag))
                .ForMember(dest => dest.ServiceFlag, opt => opt.MapFrom(src => src.ServiceFlag))
                .ForMember(dest => dest.SparepartFlag, opt => opt.MapFrom(src => src.SparepartFlag))
                .ForMember(dest => dest.RowStatus, opt => opt.MapFrom(src => src.RowStatus))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdateBy))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
