#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections;
using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.GetServiceType;
using System.Data;
using KTB.DNet.Interface.Model;
using KTB.DNet.Utility;

#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet.Service
{
    public class GetServiceTypeRepository : BaseDNetRepository<GetServiceType>, IGetServiceTypeRepository<GetServiceType, int>
    {
        #region Constructor
        public GetServiceTypeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        public List<GetServiceType> GetServiceType(string chassisNumber, int dealerID)
        {
            List<GetServiceType> result = new List<GetServiceType>();
            try
            {
                using (var connection = Connection)
                {
                    var data =  connection.Query<GetServiceType>(GetServiceTypeQuery.GetServiceType,
                        new
                        {
                            ChassisNumber = chassisNumber,
                            DealerID = dealerID
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

        public GetServiceType Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(GetServiceType entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(GetServiceType entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<GetServiceType> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<GetServiceType> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
