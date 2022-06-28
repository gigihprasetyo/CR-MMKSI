using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Model.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISparePartClaimBL : IBaseInterface<SparePartClaimParameterDto, SparePartClaimFilterDto, SparePartClaimDto>
    {
        /// <summary>
        /// Filter Object 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        ResponseBase<List<SparePartClaimResponseDto>> ReadSparePartClaim(SparePartClaimFilterDto filterDto, int pageSize);

        ResponseBase<List<SparePartClaimResponseDto>> ReadSparePartClaim1(SparePartClaimFilterDto filterDto, int pageSize);

        ResponseBase<List<SparePartClaimResponseDto>> ReadSparePartClaim2(SparePartClaimFilterDto filterDto, int pageSize);

        ResponseBase<List<SparePartClaimResponseDto>> ReadSparePartClaimWithCriteria(SparePartClaimFilterDto filterDto, int pageSize);

    }
}
