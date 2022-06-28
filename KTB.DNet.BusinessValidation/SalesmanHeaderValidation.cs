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
   public class SalesmanHeaderValidation
    {
       public SalesmanHeaderValidation()
       {
           var init = "new";
       }

       public ValidResult ValidateKTPSalesmanHeader(SalesmanHeader objSalesHeader)
       {
           ValidResult result = new ValidResult();
           result.IsValid = true;
           
           if(IsKTPUsedOnAnotherDealer(objSalesHeader))
           {
               result.IsValid = false;
               result.Message = "Mohon maaf no KTP yang diproses telah terdaftar pada siswa training aktif di dealer yang berbeda.";
           }

           return result;
       }

       private bool IsKTPUsedOnAnotherDealer(SalesmanHeader objSalesHeader)
       {
           bool result = false;
           string noKTP = GetNoKTP(objSalesHeader.ID);
           TrTrainee traineeData = GetTraineeDataByKTP(noKTP);

           if (traineeData !=null && traineeData.ID != 0)
           {
               if (traineeData.Dealer.ID != objSalesHeader.Dealer.ID && traineeData.TrTraineeSalesmanHeader.Status == (short)EnumTrTrainee.TrTraineeStatus.Active)
               {
                   result = true;
               }
           }
       
           return result;
       }

       private string GetNoKTP(int salesmanHeaderID)
       {
           var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanProfile).ToString());
           var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanProfile), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
           criterias.opAnd(new Criteria(typeof(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, 29)); //29=KTP

           ArrayList lstProfile = _mapper.RetrieveByCriteria(criterias);

           if(lstProfile.Count > 0)
           {
               SalesmanProfile profile = (SalesmanProfile)lstProfile[0];
               return profile.ProfileValue;
           }
           else
           {
               return "";
           }
       }

       private TrTrainee GetTraineeDataByKTP(string noKTP)
       {
           var _mapper = MapperFactory.GetInstance().GetMapper(typeof(TrTrainee).ToString());
           var criterias = new CriteriaComposite(new Criteria(typeof(TrTrainee), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
           criterias.opAnd(new Criteria(typeof(TrTrainee), "NoKTP", noKTP));

           ArrayList lstTrainee = _mapper.RetrieveByCriteria(criterias);

           if (lstTrainee.Count > 0)
           {
               TrTrainee trainee = (TrTrainee)lstTrainee[0];
               return trainee;
           }
           else
           {
             return new TrTrainee();
           }

       }



    }
}
