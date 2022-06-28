using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.BusinessValidation.Helpers
{
    public static class CalcHelper
    {
        public static decimal GetPPNMasterByTaxTypeId(DateTime tglTr, int TaxTypeId)
        {
            decimal result = 0;
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(PPNMaster).ToString());
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(PPNMaster), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(PPNMaster), "StartDate", MatchType.LesserOrEqual, tglTr.Date));
            criteria.opAnd(new Criteria(typeof(PPNMaster), "TaxTypeID", MatchType.Exact, TaxTypeId));
            var data = _mapper.RetrieveByCriteria(criteria).Cast<PPNMaster>().ToList().OrderByDescending(o => o.StartDate).FirstOrDefault();
            if (data != null)
                result = data.Percentage;
            return result;
        }

        public static decimal DPPCalculation(decimal pph, decimal total, decimal ppn = 0)
        {
            decimal result = total;
            if (pph != 0)
                result = Math.Round((1 / (1 - pph / 100)) * total);
            else if (ppn != 0)
                result = total / (1 + ppn / 100);
            return result;
        }

        public static decimal GetPercentage(decimal value, decimal total)
        {
            decimal result = 0;
            if (value != 0)
                result =  (value/total) * 100;
            return result;
        }

        public static decimal PPHCalculation(CalcSourceTypeEnum sourceType, decimal pph, decimal dpp = 0, decimal total = 0)
        {
            decimal result = 0;
            if (pph != 0)
                switch (sourceType)
                {
                    case CalcSourceTypeEnum.Total:
                        result = ((total / (1 - pph / 100)) - total);
                        break;
                    case CalcSourceTypeEnum.DPP:
                        result = pph / 100 * dpp;
                        break;
                }
            return Math.Round(result);
        }

        public static decimal PPNCalculation(CalcSourceTypeEnum sourceType, decimal ppn, decimal dpp = 0, decimal total = 0)
        {
            decimal result = 0;
            if (ppn != 0)
            {
                switch (sourceType)
                {
                    case CalcSourceTypeEnum.Total:
                        result = ppn / 100 * (total / (1 + ppn / 100));
                        break;
                    case CalcSourceTypeEnum.DPP:
                        result = ppn / 100 * dpp;
                        break;
                }
            }

            return Math.Round(result);
        }
    }

    public enum CalcSourceTypeEnum
    {
        Total = 1,
        DPP = 2
    }
}
