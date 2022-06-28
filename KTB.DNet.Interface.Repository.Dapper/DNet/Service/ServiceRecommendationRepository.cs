using KTB.DNet.Domain.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.ServiceRecommendation;
using KTB.DNet.Interface.Framework;

namespace KTB.DNet.Interface.Repository.Dapper.DNet.Service
{
    public class ServiceRecommendationRepository : BaseDNetRepository<ServiceRecommendation>, IServiceRecommendationRepository<ServiceRecommendation, int>
    {
        #region Constructor
        public ServiceRecommendationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        public List<ServiceRecommendation> GetRecommendation(int chassisMasterID, string phoneNumber)
        {
            List<ServiceRecommendation> result = new List<ServiceRecommendation>();
            try
            {
                using (var connection = Connection)
                {
                    var data = connection.Query<ServiceRecommendation>(ServiceRecommendationQuery.GetServiceRecommendation,
                        new
                        {
                            ChassisMasterID = chassisMasterID,
                            PhoneNumber = phoneNumber,
                        });

                    return data != null ? data.ToList() : result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Not Implemented

        public ServiceRecommendation Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(ServiceRecommendation entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(ServiceRecommendation entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ServiceRecommendation> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ServiceRecommendation> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
