
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_ktb_daftardepositaBL : IBaseInterface<VWI_CRM_ktb_daftardepositaDtoParameter, VWI_CRM_ktb_daftardepositaFilterDto, VWI_CRM_ktb_daftardepositaDto>
    {
        ResponseBase<List<VWI_CRM_ktb_daftardepositaDto>> ReadList(VWI_CRM_ktb_daftardepositaFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
