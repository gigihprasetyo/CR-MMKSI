using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.VWI_ChassisStatusFaktur;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.VWI_OpenFaktur;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Dealer;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_ChassisStatusFakturRepository : BaseDNetRepository<VWI_ChassisStatusFaktur>, IVWI_ChassisStatusFakturRepository<VWI_ChassisStatusFaktur, int>
    {
        public VWI_ChassisStatusFakturRepository(string connectionString)
           : base(connectionString)
        { }

        /// <summary>Searches the specified chassis number.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <returns></returns>
        public VWI_ChassisStatusFaktur Search(string chassisNumber, out int filteredResultsCount, out int totalResultsCount)
        {
            VWI_ChassisStatusFaktur result = null;
            filteredResultsCount = 0;
            totalResultsCount = 0;
            try
            {
                result = this.GetData(chassisNumber, ref filteredResultsCount, ref totalResultsCount);
            }
            catch (Exception ex)
            {
                
            }

            return result;
        }

        public List<VWI_ChassisStatusFaktur> SearchLastUpdateTime(DateTime lastUpdateTime, string dealerCode , out int filteredResultsCount, out int totalResultsCount)
        {
            List<VWI_ChassisStatusFaktur> result = null;
            filteredResultsCount = 0;
            totalResultsCount = 0;
            try
            {
                using (var cn = Connection)
                {
                    result = this.GetIRChassisByLastUpdate(lastUpdateTime, dealerCode, cn);

                    if (result != null)
                    {
                        filteredResultsCount += result.Count;
                        totalResultsCount += result.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }

        /// <summary>Searches the list.</summary>
        /// <param name="listChassisNumber">The list chassis number.</param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <returns></returns>
        public List<VWI_ChassisStatusFaktur> SearchList(List<string> listChassisNumber, out int filteredResultsCount, out int totalResultsCount, out List<string> listChassisNumberNull)
        {
            List<VWI_ChassisStatusFaktur> listChassisFaktur = null;
            filteredResultsCount = 0;
            totalResultsCount = 0;
            listChassisNumberNull = null;
            VWI_ChassisStatusFaktur chassisFaktur = null;
            
            foreach (string chassisNumber in listChassisNumber)
            {
                chassisFaktur = null;
                chassisFaktur = this.GetData(chassisNumber, ref filteredResultsCount, ref totalResultsCount);

                if (chassisFaktur != null)
                {
                    if (listChassisFaktur == null)
                    {
                        listChassisFaktur = new List<VWI_ChassisStatusFaktur>();
                    }

                    listChassisFaktur.Add(chassisFaktur);
                }
                else
                {
                    if (listChassisNumberNull == null)
                    {
                        listChassisNumberNull = new List<string>();
                    }

                    listChassisNumberNull.Add(chassisNumber);
                }
            }

            return listChassisFaktur;
        }

        /// <summary>Searches the list asynchronous.</summary>
        /// <param name="listChassisNumber">The list chassis number.</param>
        /// <param name="listChassisStatusFaktur">The list chassis status faktur.</param>
        public async Task SearchListAsync(List<string> listChassisNumber, List<VWI_ChassisStatusFaktur> listChassisStatusFaktur)
        {
            foreach(string chassis in listChassisNumber)
            {
                this.GetDataAsync(chassis, listChassisStatusFaktur);
            }
        }

        /// <summary>Gets the data asynchronous.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <param name="listChassisStatusFaktur">The list chassis status faktur.</param>
        private async Task GetDataAsync (string chassisNumber, List<VWI_ChassisStatusFaktur> listChassisStatusFaktur)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            listChassisStatusFaktur.Add(this.GetData(chassisNumber, ref filteredResultsCount, ref totalResultsCount));
        }

        /// <summary>Gets the data.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <returns></returns>
        private VWI_ChassisStatusFaktur GetData (string chassisNumber, ref int filteredResultsCount, ref int totalResultsCount)
        {
            int chassisMasterId = 0;
            VWI_ChassisStatusFaktur result = new VWI_ChassisStatusFaktur();

            if (!string.IsNullOrEmpty(chassisNumber))
            {

                using (var cn = Connection)
                {
                    chassisMasterId = cn.ExecuteScalar<int>(VWI_ChassisStatusFakturQuery.SelectQueryChassisID,
                        new
                        {
                            @ChassisNumber = chassisNumber
                        }
                    );
                    if (chassisMasterId > 0)
                    {
                        // check for Dummy Chassis
                        var dummyChassisId = cn.ExecuteScalar<int>(VWI_ChassisStatusFakturQuery.SelectQueryChassisDummy,
                            new
                            {
                                @ChassisMasterId = chassisMasterId
                            });

                        if (dummyChassisId > 0)
                        {
                            result = GetDummyChassis(chassisMasterId, cn);
                        }
                        else
                        {
                            // check for IR Chassis
                            var revisionStatus = cn.ExecuteScalar<short>(VWI_ChassisStatusFakturQuery.SelectQueryChassisIRDone,
                                new
                                {
                                    @ChassisMasterId = chassisMasterId
                                });

                            if (revisionStatus > 0)
                            {
                                // check for IR Chassis
                                result = GetIRChassis(chassisMasterId, revisionStatus, cn);
                            }

                            else
                            {
                                // check for Normal Faktur Chassis -- if not found return -100
                                var isTemporary = cn.ExecuteScalar<short>(VWI_ChassisStatusFakturQuery.SelectQueryChassisNormal,
                                    new
                                    {
                                        @ChassisMasterId = chassisMasterId
                                    });

                                // check for Normal Non Faktur Chassis -- if not found return -100
                                if (isTemporary == -100)
                                {
                                    isTemporary = cn.ExecuteScalar<short>(VWI_ChassisStatusFakturQuery.SelectQueryChassisNormalNonFaktur,
                                        new
                                        {
                                            @ChassisMasterId = @chassisMasterId
                                        });
                                }
                                if (isTemporary != -100)
                                {
                                    result = GetNormalChassis(chassisMasterId, isTemporary, cn);
                                }
                            }
                        }
                    }

                }
            }

            if (result != null && result.ID != 0)
            {
                filteredResultsCount += 1;
                totalResultsCount += 1;
            }
            else
            {
                //filteredResultsCount = 0;
                //totalResultsCount = 0;
                result = new VWI_ChassisStatusFaktur();
            }

            return result;
        }

        #region Not Implemented
        public VWI_ChassisStatusFaktur Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(VWI_ChassisStatusFaktur entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(VWI_ChassisStatusFaktur entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_ChassisStatusFaktur> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VWI_ChassisStatusFaktur> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public List<VWI_ChassisStatusFaktur> Search(ICriteria criteria, ICriteria chassisQueryCriteria, ICriteria lastUpdateQueryCriteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Method
        private VWI_ChassisStatusFaktur GetDummyChassis(int chassisMasterId, IDbConnection connection)
        {
            try
            {
                return connection.Query<VWI_ChassisStatusFaktur>(VWI_ChassisStatusFakturQuery.SelectQueryResultChassisDummy,
                    new
                    {
                        @ChassisMasterId = chassisMasterId
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new VWI_ChassisStatusFaktur();
            }
        }

        private VWI_ChassisStatusFaktur GetIRChassis(int chassisMasterId, short revisionStatus, IDbConnection connection)
        {
            try
            {
                if (revisionStatus == 4)
                {
                    return connection.Query<VWI_ChassisStatusFaktur>(VWI_ChassisStatusFakturQuery.SelectQueryResultChassisIRDone,
                        new
                        {
                            @ChassisMasterId = chassisMasterId
                        }).FirstOrDefault();
                }
                else
                {
                    return connection.Query<VWI_ChassisStatusFaktur>(VWI_ChassisStatusFakturQuery.SelectQueryResultChassisIRProcess,
                        new
                        {
                            @ChassisMasterId = chassisMasterId
                        }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new VWI_ChassisStatusFaktur();
            }
        }

        private VWI_ChassisStatusFaktur GetNormalChassis(int chassisMasterId, short isTemporary, IDbConnection connection)
        {
            try
            {
                if (isTemporary == -1)
                {
                    return connection.Query<VWI_ChassisStatusFaktur>(VWI_ChassisStatusFakturQuery.SelectQueryResultChassisNormalTemporaryBatal,
                        new
                        {
                            @ChassisMasterId = chassisMasterId
                        }).FirstOrDefault();
                }
                else
                {
                    return connection.Query<VWI_ChassisStatusFaktur>(VWI_ChassisStatusFakturQuery.SelectQueryResultChassisNormal,
                        new
                        {
                            @ChassisMasterId = chassisMasterId
                        }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new VWI_ChassisStatusFaktur();
            }
        }

        private List<VWI_ChassisStatusFaktur> GetIRChassisByLastUpdate(DateTime lastUpdateTime, string dealerCode, IDbConnection connection)
        {
            try
            {
                return connection.Query<VWI_ChassisStatusFaktur>(VWI_ChassisStatusFakturQuery.SelectQueryResultChassisIRLastUpdateTime,
                    new
                    {
                        @LastUpdateTime = lastUpdateTime,
                        @DealerCode = dealerCode
                    },null,true, 300, null).ToList();
            }
            catch (Exception ex)
            {
                return new List<VWI_ChassisStatusFaktur>();
            }
        }

        #endregion


    }
}
