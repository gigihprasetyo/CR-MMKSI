using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IDSFCeilingBL : IBaseViewInterface<DSFCeilingFilterDto, DSFCeilingDto>
    {
    }

    public interface IDSFCeilingUpdateBL : IBaseInterface<DSFCeilingParameterDto, DSFCeilingFilterDto, DSFCeilingUpdateDto>
    {
    }
}
