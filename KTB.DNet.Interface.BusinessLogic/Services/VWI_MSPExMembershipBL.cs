#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_MSPExMembershipBL : AbstractBusinessLogic, IVWI_MSPExMembershipBL
    {
        #region Variables
        private readonly IMapper _mspExmembershipMapper;
        #endregion

        #region Constructor
        public VWI_MSPExMembershipBL()
        {
            _mspExmembershipMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_MSPExMembership).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get MSPExMembership by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_MSPExMembershipDto>> Read(VWI_MSPExMembershipFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only                     
            var result = new ResponseBase<List<VWI_MSPExMembershipDto>>();
            var sortColl = new SortCollection();

            try
            {
                // get sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_MSPExMembership), filterDto, sortColl);

                // get data
                var data = _mspExmembershipMapper.RetrieveSP("SELECT * FROM VWI_MSPExMembership " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_MSPExMembership> list = new List<VWI_MSPExMembership>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_MSPExMembership>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_MSPExMembership>().OrderBy(x => x.MSPCustomerID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_MSPExMembershipDto> listData = list.ConvertList<VWI_MSPExMembership, VWI_MSPExMembershipDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_MSPExMembership), filterDto);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }
        #endregion
    }
}
