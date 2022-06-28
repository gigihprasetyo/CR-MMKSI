using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IDSFPaymentBL : IBaseViewInterface<DSFPaymentFilterDto, DSFPaymentDto>
    {
    }

    public interface IDSFPaymentUpdateBL : IBaseInterface<DSFPaymentParameterDto, DSFPaymentFilterDto, DSFPaymentUpdateDto>
    {
    }
}
