using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.BusinessValidation
{
    public static class Helper
    {
        /// <summary>
        /// Get data based on its code
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static CriteriaComposite GenerateCriteria(Type type, string columnName, object columnValue)
        {
            var criterias = new CriteriaComposite(new Criteria(type, "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(type, columnName, MatchType.Exact, columnValue));

            return criterias;
        }

        /// <summary>
        /// Generate criteria
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <param name="columnName2"></param>
        /// <param name="columnValue"></param>
        /// <param name="columnValue2"></param>
        /// <returns></returns>
        public static CriteriaComposite GenerateCriteria(Type type, string columnName, string columnName2, object columnValue, object columnValue2)
        {
            var criterias = new CriteriaComposite(new Criteria(type, "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(type, columnName, MatchType.Exact, columnValue));
            criterias.opAnd(new Criteria(type, columnName2, MatchType.Exact, columnValue2));

            return criterias;
        }
    }
}
