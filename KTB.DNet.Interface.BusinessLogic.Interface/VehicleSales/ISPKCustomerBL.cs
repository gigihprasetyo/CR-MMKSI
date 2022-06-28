#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKCustomer interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using System.Collections.Generic;
using DNetDomain = KTB.DNet.Domain;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISPKCustomerBL : IBaseInterface<SPKCustomerParameterDto, SPKCustomerFilterDto, SPKCustomerDto>
    {
        SPKCustomer GetValidSPKCustomerDomain(SPKCustomerParameterDto spkCustomer, List<DNetValidationResult> validationResults, string dealerCode, out OCRIdentity oCRIdentiy, out List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile);
    }
}
