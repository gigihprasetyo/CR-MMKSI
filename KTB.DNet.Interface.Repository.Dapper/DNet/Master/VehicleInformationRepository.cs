using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VehicleInformation;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VehicleInformationRepository : BaseDNetRepository<VWI_VehicleInformation_IF>, IVehicleInformationRepository<VWI_VehicleInformation_IF, int>
    {
        public VehicleInformationRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_VehicleInformation_IF> Search(string criterias, string innerQueryCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string[] ctrs = criterias.Split('|');
                string criteria = ctrs[0];
                string fltr1 = ctrs[1];
                string fltr2 = ctrs[2];
                string fltr3 = ctrs[3];
                string fltr4 = ctrs[4];

                string xx = criteria.ToLower().Replace("and", "|");
                string[] x = xx.Split('|');
                for (int i = 0; i < x.Count(); i++)
                {
                    if (x[i].ToLower().Contains("lastupdatetime"))
                    {
                        x[i] = " 1=1 ";
                    }
                }
                criteria = string.Join(" and ", x);
                string zz = criteria.ToLower().Replace(" or ", " | ");
                string[] z = zz.Split('|');
                for (int i = 0; i < z.Count(); i++)
                {
                    if (z[i].ToLower().Contains("lastupdatetime"))
                    {
                        z[i] = " 1=1 ";
                    }
                }
                criteria = string.Join(" or ", z);

                string innerTotal = innerQueryCriteria.ToLower().Replace(" order ", "|");
                string[] innerTtl = innerTotal.Split('|');
                string innerQueryCriteriaTotal = innerTtl[0].Replace("#", fltr4);
                if (!innerQueryCriteriaTotal.ToLower().Contains("where")) { innerQueryCriteriaTotal = "where 1=1 " + innerQueryCriteriaTotal; }

                innerQueryCriteria = innerQueryCriteria.Replace("#", fltr4);
                if (!innerQueryCriteria.ToLower().Contains("where")) { innerQueryCriteria = "where 1=1 " + innerQueryCriteria; }

                string orderBy = !string.IsNullOrEmpty(sortColumns) ? sortColumns.ToString() : null;
                if (orderBy != null)
                {
                    orderBy = orderBy.Replace("VWI_VehicleInformation_IF", "VWI_VehicleInformation");
                }

                filteredResultsCount = 0;
                criteria = criteria.ToLower().Replace("vwi_vehicleinformation_if", "VWI_VehicleInformation");

                List<VWI_VehicleInformation_IF> result = SearchFetchPaging<VWI_VehicleInformation_IF>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_VehicleInformation_IF>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VehicleInformationQuery.SelectQuery,
                                                innerQueryCriteria == null ? string.Empty : innerQueryCriteria.ToString(),
                                                criteria == null ? string.Empty : criteria.ToString(),
                                                fltr1,
                                                fltr2,
                                                fltr3)
                , "VWI_VehicleInformation.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VehicleInformationQuery.GetTotalQuery,
                                                innerQueryCriteriaTotal,
                                                criteria == null ? string.Empty : criteria.ToString(),
                                                fltr1,
                                                fltr2,
                                                fltr3), "ID");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_VehicleInformation_IF>();
            }
        }

        public List<VWI_VehicleInformation_IF> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_VehicleInformation_IF>();
        }

        public List<VWI_VehicleInformationDto> GetByQuery(string query, ICriteria criteria, VWI_VehicleInformationFilterDto filterDto, int pageSize, out int total)
        {
            List<VWI_VehicleInformationDto> data = null;
            total = 0;
            // proceed with dapper
            using (var cn = Connection)
            {
                using (var multi = cn.QueryMultiple(query))
                {
                    // get total count
                    total = multi.Read<int>().Single();

                    if (criteria != null)
                    {
                        // calculate skip
                        int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                        // get filtered data
                        data = multi.Read<VWI_VehicleInformationDto>().Skip(skip).Take(pageSize).ToList();
                    }
                    else
                    {
                        data = multi.Read<VWI_VehicleInformationDto>().ToList();
                    }
                }
            }

            return data;
        }

        #region Not Implemented
        public void SetUserLogin(string username)
        {
            throw new NotImplementedException();
        }

        public VWI_VehicleInformation_IF Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(VWI_VehicleInformation_IF entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(VWI_VehicleInformation_IF entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_VehicleInformation_IF> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
