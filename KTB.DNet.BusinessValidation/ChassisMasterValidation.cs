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
    public class ChassisMasterValidation
    {
        #region Constructor
        public ChassisMasterValidation()
        {
        }
        #endregion

        /// <summary>Validates the chassis number.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <param name="validResultList">The valid result list.</param>
        /// <param name="chassisMaster">The chassis master.</param>
        /// <returns></returns>
        public bool ValidateChassisNumber (string chassisNumber, ref List<ValidResult> validResultList, ref ChassisMaster chassisMaster)
        {
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            var chassisMasterData = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMaster), "ChassisNumber", "Category.ProductCategory.Code", chassisNumber, ConfigurationManager.AppSettings["CompanyCode"]));

            if (chassisMasterData.Count > 0)
            {
                chassisMaster = chassisMasterData[0] as ChassisMaster;
                return true;
            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Data Chassis Master dengan Chassis Number {0} tidak ditemukan di database", chassisNumber)
                };
                validResultList.Add(validResult);
                return false;
            }
        }
    }
}
