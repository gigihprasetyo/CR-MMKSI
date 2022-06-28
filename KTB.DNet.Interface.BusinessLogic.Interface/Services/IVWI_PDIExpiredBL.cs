
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_PDIExpiredBL : IBaseInterface<VWI_PDIExpiredParameterDto, VWI_PDIExpiredFilterDto, VWI_PDIExpiredDto>
    {
        ResponseBase<List<VWI_PDIExpiredDto>> ReadList(VWI_PDIExpiredFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
