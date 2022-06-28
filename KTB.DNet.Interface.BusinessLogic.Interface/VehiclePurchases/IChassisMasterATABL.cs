#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterATA interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using System.Collections.Generic;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IChassisMasterATABL : IBaseInterface<ChassisMasterATAParameterDto, ChassisMasterATAFilterDto, ChassisMasterATADto>
    {
        /// <summary>Updates the ata.</summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        ResponseBase<ChassisMasterATADto> UpdateATA(ChassisMasterATAParameterDto param);

        /// <summary>Reads the specified chassis master ata.</summary>
        /// <param name="chassisMasterATA">The chassis master ata.</param>
        /// <returns></returns>
        ResponseBase<ChassisMasterATADto> Read(ChassisMasterATA chassisMasterATA);
    }
}
