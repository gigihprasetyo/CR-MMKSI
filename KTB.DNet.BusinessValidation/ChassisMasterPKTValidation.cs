using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.BusinessValidation
{
    public class ChassisMasterPKTValidation
    {
        
        #region Constructor
        public ChassisMasterPKTValidation()
        {
        }
        #endregion

        #region Public Method
        public List<ValidResult> ValidateChassisMasterPKT(ref ChassisMasterPKT chassisMasterPKT, ref ChassisMaster chassisMaster)
        {
            #region Declare Variables
            var validResultList = new List<ValidResult>();
            var isValid = false;
            var dealer = new Dealer();
            #endregion

            // Validate PKT Date
            isValid = ValidationHelper.ValidatePKTDate(chassisMasterPKT.PKTDate, ref validResultList);

            // Validate Chassis Master
            isValid = ValidationHelper.ValidateChassisMaster(chassisMasterPKT.ChassisMaster.ChassisNumber, ref validResultList, ref chassisMaster, true, chassisMasterPKT);

            // Check if Chassis Master PKT already in database
            if (chassisMaster.ID != 0 && validResultList.Count == 0)
            {
                isValid = ValidationHelper.ValidateChassisMasterPKT(chassisMaster.ChassisNumber, validResultList, ref chassisMasterPKT);
            }

            return validResultList;
        }

        public List<ValidResult> ValidateDealerChassisPKT(string dealerCode)
        {
            #region Declare Variables
            var validResultList = new List<ValidResult>();
            var isValid = false;
            #endregion

            // check if dealer chassis in transaction control
            // impacted to cr pdi pkt partial go live
            isValid = ValidationHelper.ValidateDelaerChassisPKT(dealerCode, ref validResultList);

            return validResultList;
        }
        #endregion


    }
}
