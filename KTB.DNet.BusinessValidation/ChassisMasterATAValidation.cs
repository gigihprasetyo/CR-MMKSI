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
    public class ChassisMasterATAValidation
    {
        #region Constructor
        public ChassisMasterATAValidation()
        {
        }
        #endregion

        public List<ValidResult> ValidateChassisMasterATAUpdateATA(string chassisNumber, DateTime aTADate, ref ChassisMasterATA chassisMasterATA)
        {
            List<ValidResult> listValidResult = new List<ValidResult>();

            ChassisMaster chassisMasterData = new ChassisMaster();
            ChassisMasterValidation chassisMasterValidation = new ChassisMasterValidation();

            if (chassisMasterValidation.ValidateChassisNumber(chassisNumber, ref listValidResult, ref chassisMasterData))
            {
                this.ChassisMasterATAExist(chassisMasterData, ref listValidResult, ref chassisMasterATA);
                ValidationHelper.ValidateDateValid(aTADate, "Tanggal ATA", ref listValidResult);
                ValidationHelper.ValidateDateCannotMoreThanToday(aTADate, "Tanggal ATA", ref listValidResult);
            }

            return listValidResult;
        }

        /// <summary>Chassises the master ata exist.</summary>
        /// <param name="chassisMaster">The chassis master.</param>
        /// <param name="validResultList">The valid result list.</param>
        /// <param name="chassisMasterATA">The chassis master ata.</param>
        /// <returns></returns>
        public void ChassisMasterATAExist(ChassisMaster chassisMaster, ref List<ValidResult> validResultList, ref ChassisMasterATA chassisMasterATA)
        {
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterATA).ToString());

            var chassisMasterATAData = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMasterATA), "ChassisMaster.ID", chassisMaster.ID));

            if (chassisMasterATAData.Count > 0)
            {
                chassisMasterATA = chassisMasterATAData[0] as ChassisMasterATA;
            }
            else
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = string.Format("Data Chassis Master ATA dengan Chassis Number {0} tidak ditemukan di database", chassisMaster.ChassisNumber)
                };
                validResultList.Add(validResult);
            }
        }
    }
}
