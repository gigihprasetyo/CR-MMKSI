#region "Namespace Imports"
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
    public class VWI_SFDMobileSalesmanBL : AbstractBusinessLogic, IVWI_SFDMobileSalesmanBL
    {
         #region Variables
        private readonly IMapper _VWI_SFDMobileSalesmanMapper;
       
        #endregion

        #region Constructor
        public VWI_SFDMobileSalesmanBL()
        {
            _VWI_SFDMobileSalesmanMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_SFDMobileSalesman).ToString());
          
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read Vehicle Code master data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_SFDMobileSalesmanDto>> Read(VWI_SFDMobileSalesmanFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_SFDMobileSalesman), "ID", MatchType.Greater,  0));
            var result = new ResponseBase<List<VWI_SFDMobileSalesmanDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_SFDMobileSalesman), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_SFDMobileSalesman), filterDto, sortColl);

                // get data
                var data = _VWI_SFDMobileSalesmanMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_SFDMobileSalesman>().ToList();
                    List<VWI_SFDMobileSalesmanDto> listData = list.ConvertList<VWI_SFDMobileSalesman, VWI_SFDMobileSalesmanDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_SFDMobileSalesman), filterDto);
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
