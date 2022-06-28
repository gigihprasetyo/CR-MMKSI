using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;

namespace KTB.DNet.BusinessValidation
{
    public static class TCHelper
    {
        public static bool GetActiveTCResult(int dealerId, int valueId)
        {
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(TransactionControl).ToString());
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(TransactionControl), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(TransactionControl), "Dealer.ID", MatchType.Exact, dealerId));
            criteria.opAnd(new Criteria(typeof(TransactionControl), "Kind", MatchType.Exact, valueId.ToString()));
            criteria.opAnd(new Criteria(typeof(TransactionControl), "Status", MatchType.Exact, "1"));

            var tc = _mapper.RetrieveByCriteria(criteria);

            return tc.Count > 0;
        }
    }
}
