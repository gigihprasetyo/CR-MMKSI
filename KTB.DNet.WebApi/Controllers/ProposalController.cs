using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.WebApi.Models;
using KTB.DNET.BusinessFacade;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;

namespace KTB.DNet.WebApi.Controllers
{
    [RoutePrefix("api/proposal")]
    public class ProposalController : BaseApiController
    {
        [HttpGet]
        [Route("Babits")]
        public IHttpActionResult Babits()
        {
            GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
            //if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);
            //if (!IsLogin()) return Unauthorized(new System.Net.Http.Headers.AuthenticationHeaderValue(" Your Token is invalid."));
            //if (!IsLogin()) return Unauthorized();
            if (!IsLogin()) return Content(HttpStatusCode.Unauthorized, "Your Token is invalid");



            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(BabitHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(BabitHeader), "BabitStatus", MatchType.Exact, 4));
            var _Result = new BabitHeaderFacade(user).Retrieve(criterias);
            var jsonForm = new MasterBabit();
            try
            {
                List<Models.EventProposal> mep = new List<Models.EventProposal>();
                List<Models.BABITAdvProposal> bap = new List<Models.BABITAdvProposal>();
                List<Models.BABITEvtProposal> bep = new List<Models.BABITEvtProposal>();
                foreach (BabitHeader oBH in _Result)
                {
                    var arrDoc = new ArrayList();
                    #region Switch V1

                    CriteriaComposite criBabitDealerAllocation = new CriteriaComposite(new Criteria(typeof(BabitDealerAllocation), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criBabitDealerAllocation.opAnd(new Criteria(typeof(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, oBH.ID));
                    ArrayList _arrBabitDealerAllocation = new BabitDealerAllocationFacade(user).Retrieve(criBabitDealerAllocation);

                    switch (oBH.BabitMasterEventType.FormType)
                    {
                        case 1: //Event
                            Models.EventProposal oMep = new Models.EventProposal();
                            CriteriaComposite criBabitEventDetail = new CriteriaComposite(new Criteria(typeof(BabitEventDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitEventDetail.opAnd(new Criteria(typeof(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, oBH.ID));
                            ArrayList _ArrEventResult = new BabitEventDetailFacade(user).Retrieve(criBabitEventDetail);
                            BabitEventDetail _EventResult = (BabitEventDetail)_ArrEventResult[0];
                            oMep.ProposalCode = _EventResult.BabitHeader.BabitRegNumber.ToString();
                            oMep.DealerName = _EventResult.BabitHeader.Dealer.DealerName.ToString();
                            oMep.DealerCode = _EventResult.BabitHeader.Dealer.DealerCode.ToString();
                            if (oBH.BabitDealerGroup != null && oBH.BabitDealerGroup.ToString() != "")
                            {
                                List<string> lBabitDealerGroup = new List<string>();
                                foreach (string dealerID in oBH.BabitDealerGroup.Split(';'))
                                {
                                    Dealer _DealerResult = (Dealer)new DealerFacade(user).Retrieve(Int32.Parse(dealerID));
                                    lBabitDealerGroup.Add(_DealerResult.DealerCode);
                                }
                                oMep.CollaborateDealer = lBabitDealerGroup;
                            }

                            int biayaEvent = 0;
                            foreach (BabitDealerAllocation amount in _arrBabitDealerAllocation)
                            {
                                biayaEvent += Convert.ToInt32(amount.SubsidyAmount);
                            }
                            oMep.ApprovedBudget = biayaEvent;

                            var oBDAEvent = (from BabitDealerAllocation objBabitAlloc in _arrBabitDealerAllocation
                                             select objBabitAlloc.Dealer.DealerCode).Distinct().ToList();
                            List<ApprovedBudgetDetails> objABDEvent = new List<ApprovedBudgetDetails>();
                            foreach (string dealer in oBDAEvent)
                            {
                                ApprovedBudgetDetails oABD = new ApprovedBudgetDetails();
                                oABD.DealerBudget = dealer;
                                oABD.isEdit = "";

                                try
                                {
                                    oABD.PC = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                    where a.Dealer.DealerCode == dealer && a.BabitCategory == "PC"
                                                    select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.PC > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "PC,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.PC = 0;
                                }

                                try
                                {
                                    oABD.LCV = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                     where a.Dealer.DealerCode == dealer && a.BabitCategory == "LCV"
                                                     select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.LCV > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "LCV,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.LCV = 0;
                                }

                                try
                                {
                                    oABD.XPANDER = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                         where a.Dealer.DealerCode == dealer && a.BabitCategory == "XPANDER"
                                                         select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.XPANDER > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "XPANDER,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.XPANDER = 0;
                                }

                                try
                                {
                                    oABD.SPESIAL = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                         where a.Dealer.DealerCode == dealer && a.BabitCategory == "SPESIAL"
                                                         select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.SPESIAL > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "SPESIAL,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.SPESIAL = 0;
                                }

                                if (oABD.isEdit != "")
                                {
                                    oABD.isEdit = oABD.isEdit.Remove(oABD.isEdit.Length - 1, 1);
                                }
                                objABDEvent.Add(oABD);
                            }
                            oMep.ApprovedBudgetDetails = objABDEvent;
                            //oMep.Description = nothing
                            ArrayList arrDocEvent = new BabitDocumentFacade(user).RetrieveByBabitHeader(_EventResult.BabitHeader.ID);
                            List<Attachments> lDocEvent = new List<Attachments>();
                            foreach (BabitDocument doc in arrDocEvent)
                            {
                                Attachments oEA = new Attachments();
                                oEA.Filename = doc.FileName;
                                oEA.Description = doc.FileDescription;
                                oEA.Path = doc.Path;
                                lDocEvent.Add(oEA);
                            }
                            oMep.Attachments = lDocEvent;

                            oMep.DealerAddress = _EventResult.BabitHeader.Dealer.Address.ToString();
                            oMep.DealerCityDistrict = _EventResult.BabitHeader.Dealer.City.CityName.ToString();
                            oMep.EventDateBegin = _EventResult.BabitHeader.PeriodStart;
                            oMep.EventDateEnd = _EventResult.BabitHeader.PeriodEnd;
                            oMep.EventLocation = _EventResult.BabitHeader.Location.ToString();
                            //New
                            oMep.TOCode = _EventResult.BabitHeader.DealerBranch != null ? _EventResult.BabitHeader.DealerBranch.DealerBranchCode.ToString() : "";
                            oMep.LetterNo = _EventResult.BabitHeader.BabitDealerNumber.ToString();

                            var oBAUEvent = (from BabitDealerAllocation objBabitAlloc in _arrBabitDealerAllocation
                                             select objBabitAlloc.BabitCategory).Distinct().ToList();
                            oMep.BabitAllocationUsed = _arrBabitDealerAllocation.Count > 0 ? String.Join(",", oBAUEvent.ToArray()) : "";
                            oMep.EventCategory = _EventResult.BabitHeader.BabitMasterEventType.TypeName.ToString();
                            oMep.TargetedSPK = _EventResult.BabitHeader.SPKTarget;
                            //oMep.TargetedSPK = _EventResult.BabitHeader.ProspectTarget;
                            //01-10-2019
                            criBabitEventDetail.opAnd(new Criteria(typeof(BabitEventDetail), "BabitParameterDetail.BabitParameterHeader.ParameterCategory", MatchType.Exact, 0));
                            criBabitEventDetail.opAnd(new Criteria(typeof(BabitEventDetail), "BabitHeader.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitEventDetail.opAnd(new Criteria(typeof(BabitEventDetail), "BabitParameterDetail.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitEventDetail.opAnd(new Criteria(typeof(BabitEventDetail), "BabitParameterDetail.BabitParameterHeader.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            _ArrEventResult = new BabitEventDetailFacade(user).Retrieve(criBabitEventDetail);
                            int pBudget = 0;
                            foreach (BabitEventDetail q in _ArrEventResult)
                            {
                                pBudget += (int)(q.Price * q.Qty);
                            }
                            oMep.ProposedBudget = pBudget;
                            //

                            mep.Add(oMep);
                            break;
                        case 2: //Pameran
                            Models.BABITEvtProposal oBep = new Models.BABITEvtProposal();
                            CriteriaComposite criBabitPameranDetail = new CriteriaComposite(new Criteria(typeof(BabitPameranDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitPameranDetail.opAnd(new Criteria(typeof(BabitPameranDetail), "BabitHeader.ID", MatchType.Exact, oBH.ID));
                            BabitPameranDetail _PameranResult = (BabitPameranDetail)new BabitPameranDetailFacade(user).Retrieve(criBabitPameranDetail)[0];
                            oBep.ProposalCode = _PameranResult.BabitHeader.BabitRegNumber;
                            oBep.DealerName = _PameranResult.BabitHeader.Dealer.DealerName;
                            oBep.DealerCode = _PameranResult.BabitHeader.Dealer.DealerCode;
                            if (oBH.BabitDealerGroup != null && oBH.BabitDealerGroup.ToString() != "")
                            {
                                List<string> lBabitDealerGroup = new List<string>();
                                foreach (string dealerID in oBH.BabitDealerGroup.Split(';'))
                                {
                                    Dealer _DealerResult = (Dealer)new DealerFacade(user).Retrieve(Int32.Parse(dealerID));
                                    lBabitDealerGroup.Add(_DealerResult.DealerCode);
                                }
                                oBep.CollaborateDealer = lBabitDealerGroup;
                            }

                            int expense = 0;
                            foreach (BabitDealerAllocation amount in _arrBabitDealerAllocation)
                            {
                                expense += Convert.ToInt32(amount.SubsidyAmount);
                            }
                            oBep.ApprovedBudget = expense;

                            var oBDAPameran = (from BabitDealerAllocation objBabitAlloc in _arrBabitDealerAllocation
                                               select objBabitAlloc.Dealer.DealerCode).Distinct().ToList();
                            List<ApprovedBudgetDetails> objABDPameran = new List<ApprovedBudgetDetails>();
                            foreach (string dealer in oBDAPameran)
                            {
                                ApprovedBudgetDetails oABD = new ApprovedBudgetDetails();
                                oABD.DealerBudget = dealer;
                                oABD.isEdit = "";

                                try
                                {
                                    oABD.PC = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                    where a.Dealer.DealerCode == dealer && a.BabitCategory == "PC"
                                                    select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.PC > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "PC,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.PC = 0;
                                }

                                try
                                {
                                    oABD.LCV = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                     where a.Dealer.DealerCode == dealer && a.BabitCategory == "LCV"
                                                     select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.LCV > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "LCV,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.LCV = 0;
                                }

                                try
                                {
                                    oABD.XPANDER = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                         where a.Dealer.DealerCode == dealer && a.BabitCategory == "XPANDER"
                                                         select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.XPANDER > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "XPANDER,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.XPANDER = 0;
                                }

                                try
                                {
                                    oABD.SPESIAL = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                         where a.Dealer.DealerCode == dealer && a.BabitCategory == "SPESIAL"
                                                         select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.SPESIAL > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "SPESIAL,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.SPESIAL = 0;
                                }

                                if (oABD.isEdit != "")
                                {
                                    oABD.isEdit = oABD.isEdit.Remove(oABD.isEdit.Length - 1, 1);
                                }
                                objABDPameran.Add(oABD);
                            }
                            oBep.ApprovedBudgetDetails = objABDPameran;
                            //oBep.Description = nothing
                            var arrDocPameran = new BabitDocumentFacade(user).RetrieveByBabitHeader(_PameranResult.BabitHeader.ID);
                            List<Attachments> lDocPameran = new List<Attachments>();
                            foreach (BabitDocument doc in arrDocPameran)
                            {
                                Attachments oEA = new Attachments();
                                oEA.Filename = doc.FileName;
                                oEA.Description = doc.FileDescription;
                                oEA.Path = doc.Path;
                                lDocPameran.Add(oEA);
                            }
                            oBep.Attachments = lDocPameran;
                            oBep.LetterNo = _PameranResult.BabitHeader.BabitDealerNumber;
                            oBep.ParentDealer = _PameranResult.BabitHeader.Dealer.DealerCode;
                            oBep.Venue = _PameranResult.BabitHeader.Location;
                            oBep.ScheduleBegin = _PameranResult.BabitHeader.PeriodStart;
                            oBep.ScheduleEnd = _PameranResult.BabitHeader.PeriodEnd;
                            oBep.TargetedSPK = _PameranResult.BabitHeader.SPKTarget.ToString();
                            //oBep.TargetedSPK = _PameranResult.BabitHeader.ProspectTarget.ToString();
                            oBep.ActivityType = _PameranResult.BabitHeader.BabitMasterEventType.TypeName;

                            var oBAUPameran = (from BabitDealerAllocation objBabitAlloc in _arrBabitDealerAllocation
                                             select objBabitAlloc.BabitCategory).Distinct().ToList();
                            oBep.BABITAllocationUsed = _arrBabitDealerAllocation.Count > 0 ? String.Join(",", oBAUPameran.ToArray()) : "";
                            //01-10-2019
                            CriteriaComposite criBabitPameranExpense = new CriteriaComposite(new Criteria(typeof(BabitPameranExpense), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitPameranExpense.opAnd(new Criteria(typeof(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, oBH.ID));
                            criBabitPameranExpense.opAnd(new Criteria(typeof(BabitPameranExpense), "BabitParameterDetail.BabitParameterHeader.ParameterCategory", MatchType.Exact, 0));
                            criBabitPameranExpense.opAnd(new Criteria(typeof(BabitPameranExpense), "BabitHeader.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitPameranExpense.opAnd(new Criteria(typeof(BabitPameranExpense), "BabitParameterDetail.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitPameranExpense.opAnd(new Criteria(typeof(BabitPameranExpense), "BabitParameterDetail.BabitParameterHeader.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            ArrayList _PameranExpense = new BabitPameranExpenseFacade(user).Retrieve(criBabitPameranExpense);
                            int tempBiaya = 0;
                            foreach (BabitPameranExpense amount in _PameranExpense)
                            {
                                tempBiaya += (int)(amount.Qty * Convert.ToInt32(amount.Price));
                            }
                            oBep.ProposedBudget = tempBiaya;

                            bep.Add(oBep);
                            break;
                        case 3: //Iklan
                            Models.BABITAdvProposal oBap = new Models.BABITAdvProposal();
                            CriteriaComposite criBabitIklanDetail = new CriteriaComposite(new Criteria(typeof(BabitIklanDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitIklanDetail.opAnd(new Criteria(typeof(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, oBH.ID));
                            ArrayList _arrIklanResults = new BabitIklanDetailFacade(user).Retrieve(criBabitIklanDetail);
                            BabitIklanDetail _IklanResult = (BabitIklanDetail)_arrIklanResults[0];
                            oBap.ProposalCode = _IklanResult.BabitHeader.BabitRegNumber;
                            oBap.DealerName = _IklanResult.BabitHeader.Dealer.DealerName;
                            oBap.DealerCode = _IklanResult.BabitHeader.Dealer.DealerCode;
                            if (oBH.BabitDealerGroup != null && oBH.BabitDealerGroup.ToString() != "")
                            {
                                List<string> lBabitDealerGroup = new List<string>();
                                foreach (string dealerID in oBH.BabitDealerGroup.Split(';'))
                                {
                                    Dealer _DealerResult = (Dealer)new DealerFacade(user).Retrieve(Int32.Parse(dealerID));
                                    lBabitDealerGroup.Add(_DealerResult.DealerCode);
                                }
                                oBap.CollaborateDealer = lBabitDealerGroup;
                            }
                            int biaya = 0;
                            foreach (BabitDealerAllocation amount in _arrBabitDealerAllocation)
                            {
                                biaya += Convert.ToInt32(amount.SubsidyAmount);
                            }
                            oBap.ApprovedBudget = biaya;
                            //oBap.Description = nothing
                            var arrDocIklan = new BabitDocumentFacade(user).RetrieveByBabitHeader(_IklanResult.BabitHeader.ID);

                            List<Attachments> lDocIklan = new List<Attachments>();
                            foreach (BabitDocument doc in arrDocIklan)
                            {
                                Attachments oEA = new Attachments();
                                oEA.Filename = doc.FileName;
                                oEA.Description = doc.FileDescription;
                                oEA.Path = doc.Path;
                                lDocIklan.Add(oEA);
                            }
                            oBap.Attachments = lDocIklan;
                            oBap.LetterNo = _IklanResult.BabitHeader.BabitDealerNumber;
                            oBap.MediaBroadcastPeriodBegin = _IklanResult.BabitHeader.PeriodStart;
                            oBap.MediaBroadcastPeriodEnd = _IklanResult.BabitHeader.PeriodEnd;

                            var oBDAIklan = (from BabitDealerAllocation objBabitAlloc in _arrBabitDealerAllocation
                                             select objBabitAlloc.Dealer.DealerCode).Distinct().ToList();
                            List<ApprovedBudgetDetails> objABDIklan = new List<ApprovedBudgetDetails>();
                            foreach (string dealer in oBDAIklan)
                            {
                                ApprovedBudgetDetails oABD = new ApprovedBudgetDetails();
                                oABD.DealerBudget = dealer;
                                oABD.isEdit = "";

                                try
                                {
                                    oABD.PC = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                    where a.Dealer.DealerCode == dealer && a.BabitCategory == "PC"
                                                    select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.PC > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "PC,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.PC = 0;
                                }

                                try
                                {
                                    oABD.LCV = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                     where a.Dealer.DealerCode == dealer && a.BabitCategory == "LCV"
                                                     select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.LCV > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "LCV,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.LCV = 0;
                                }

                                try
                                {
                                    oABD.XPANDER = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                         where a.Dealer.DealerCode == dealer && a.BabitCategory == "XPANDER"
                                                         select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.XPANDER > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "XPANDER,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.XPANDER = 0;
                                }

                                try
                                {
                                    oABD.SPESIAL = (int)(from BabitDealerAllocation a in _arrBabitDealerAllocation
                                                         where a.Dealer.DealerCode == dealer && a.BabitCategory == "SPESIAL"
                                                         select a).FirstOrDefault().SubsidyAmount;
                                    if (oABD.SPESIAL > 0)
                                    {
                                        oABD.isEdit = oABD.isEdit + "SPESIAL,";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    oABD.SPESIAL = 0;
                                }

                                if (oABD.isEdit != "")
                                {
                                    oABD.isEdit = oABD.isEdit.Remove(oABD.isEdit.Length - 1, 1);
                                }
                                objABDIklan.Add(oABD);
                            }
                            oBap.ApprovedBudgetDetails = objABDIklan;

                            //01-10-2019
                            criBabitIklanDetail.opAnd(new Criteria(typeof(BabitIklanDetail), "BabitParameterDetail.BabitParameterHeader.ParameterCategory", MatchType.Exact, 4));
                            criBabitIklanDetail.opAnd(new Criteria(typeof(BabitIklanDetail), "BabitHeader.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitIklanDetail.opAnd(new Criteria(typeof(BabitIklanDetail), "BabitParameterDetail.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criBabitIklanDetail.opAnd(new Criteria(typeof(BabitIklanDetail), "BabitParameterDetail.BabitParameterHeader.RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            _arrIklanResults = new BabitIklanDetailFacade(user).Retrieve(criBabitIklanDetail);
                            int pBgt = 0;
                            foreach (BabitIklanDetail amount in _arrIklanResults)
                            {
                                pBgt += Convert.ToInt32(amount.SubmissionAmount);
                            }
                            oBap.ProposedBudget = pBgt;

                            //07-10-2019
                            CriteriaComposite criMedia = new CriteriaComposite(new Criteria(typeof(BabitIklanDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criMedia.opAnd(new Criteria(typeof(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, oBH.ID));
                            criMedia.opAnd(new Criteria(typeof(BabitIklanDetail), "BabitParameterHeader.ParameterCategory", MatchType.Exact, 4));
                            ArrayList arrMedia = new BabitIklanDetailFacade(user).Retrieve(criMedia);
                            List<MediaDetail> lMed = new List<MediaDetail>();
                            foreach (BabitIklanDetail mDetail in arrMedia)
                            {
                                MediaDetail mediaDetail = new MediaDetail();
                                mediaDetail.Media = mDetail.BabitParameterHeader.ParameterName;
                                mediaDetail.MediaName = mDetail.MediaName;
                                lMed.Add(mediaDetail);
                            }
                            oBap.MediaDetails = lMed;

                            bap.Add(oBap);
                            break;
                    }

                    #endregion Switch V1
                }
                jsonForm.Event = mep;
                jsonForm.Pameran = bep;
                jsonForm.Iklan = bap;
            }
            catch (Exception ex)
            {
                string debug = ex.Message;
            }
            return Ok(jsonForm);
            //return this.result(success, "-1", 0, message, null);
        }

        [HttpPost]
        [Route("ProcessBabit")]
        public IDictionary<string, object> ProcessBabit()
        {
            GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
            bool success = false; string message = ""; int nCount = 0;
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);

            try
            {
                var jsonBody = Request.Content.ReadAsStringAsync().Result;
                var sapcustomer = JsonConvert.DeserializeObject<UpdateStatus>(jsonBody);
                int i = 0;
                foreach (var pc in sapcustomer.BabitData)
                {
                    try
                    {
                        BabitHeader oBH = new BabitHeaderFacade(user).Retrieve(pc.RegNumber);
                        if (oBH.BabitStatus == 4)
                        {
                            oBH.BabitStatus = 7; //Process by GW
                            oBH.ApprovalNumber = pc.FolioNumber;
                            oBH.LastUpdateBy = "Groupware";
                            oBH.LastUpdateTime = DateTime.Now;
                            var _result = new BabitHeaderFacade(user).Update(oBH);
                            if (_result <= 0)
                            {
                                success = false;
                                if (message.Length == 0)
                                {
                                    message += "Update " + pc.RegNumber + " Failed";
                                }
                                else
                                {
                                    message += "\n" + "Update " + pc.RegNumber + " Failed";
                                }
                            }
                            else { success = true; nCount += 1; }
                        }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        if (message.Length == 0)
                        {
                            message += ex.Message;
                        }
                        else
                        {
                            message += "\n" + ex.Message;
                        }
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                success = false;
                if (message.Length == 0)
                {
                    message += ex.Message;
                }
                else
                {
                    message += "\n" + ex.Message;
                }
            }

            return this.result(success, "-1", nCount, message, null);
            //return Content(HttpStatusCode.OK, state);
        }

        [HttpPost]
        [Route("UpdateBabit")]
        public IDictionary<string, object> UpdateBabit()
        {
            GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
            bool success = false; string message = "";
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);
            int i = 0;

            try
            {
                var jsonBody = Request.Content.ReadAsStringAsync().Result;
                var sapcustomer = JsonConvert.DeserializeObject<List<MasterBabit>>(jsonBody);
                foreach (var pc in sapcustomer)
                {
                    if (pc.Iklan != null)
                    {
                        foreach (var iklan in pc.Iklan)
                        {
                            updateIkan(pc, ref message);
                        }
                    }
                    if (pc.Pameran != null)
                    {
                        foreach (var pameran in pc.Pameran)
                        {
                            updatePameran(pc, ref message);
                        }
                    }
                    if (pc.Event != null)
                    {
                        foreach (var even in pc.Event)
                        {
                            updateEvent(pc, ref message);
                        }
                    }
                    i++;
                }
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                if (message.Length == 0)
                {
                    message += ex.Message;
                }
                else
                {
                    message += "\n" + ex.Message;
                }
            }

            return this.result(success, "-1", i, message, null);
            //return Content(HttpStatusCode.OK, state);
        }
        private void updateIkan(MasterBabit pc, ref string message)
        {
            try
            {
                GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
                foreach (var iklan in pc.Iklan)
                {
                    CriteriaComposite cr = new CriteriaComposite(new Criteria(typeof(BabitHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    cr.opAnd(new Criteria(typeof(BabitHeader), "BabitRegNumber", MatchType.Exact, iklan.ProposalCode.Trim()));
                    cr.opAnd(new Criteria(typeof(BabitHeader), "BabitStatus", MatchType.Exact, 7));
                    ArrayList arrBid = new BabitHeaderFacade(user).Retrieve(cr);
                    BabitHeader oBid = new BabitHeader();
                    ArrayList arrBDA = new ArrayList();
                    ArrayList arrBDoc = new ArrayList();
                    if (arrBid.Count > 0)
                    {
                        oBid = (BabitHeader)arrBid[0];
                        oBid.BabitStatus = Convert.ToInt16(iklan.Status);
                        oBid.ApprovalNumber = iklan.ApprovalNumber;
                        //BabitDealerAllocation oBDA = new BabitDealerAllocationFacade(user).RetrieveByBabitHeader(oBid.ID);
                        //oBDA.SubsidyAmount = iklan.ApprovedBudget;
                        foreach (var a in iklan.ApprovedBudgetDetails)
                        {
                            CriteriaComposite crBabitDealerAllocation = new CriteriaComposite(new Criteria(typeof(BabitDealerAllocation), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            crBabitDealerAllocation.opAnd(new Criteria(typeof(BabitDealerAllocation), "BabitHeader.BabitRegNumber", MatchType.Exact, iklan.ProposalCode));
                            crBabitDealerAllocation.opAnd(new Criteria(typeof(BabitDealerAllocation), "Dealer.DealerCode", MatchType.Exact, a.DealerBudget));
                            ArrayList arrBabitDealerAllocation = new BabitDealerAllocationFacade(user).Retrieve(crBabitDealerAllocation);
                            if (arrBabitDealerAllocation.Count > 0)
                            {
                                foreach (var item in arrBabitDealerAllocation)
                                {
                                    BabitDealerAllocation bda = (BabitDealerAllocation)item;
                                    bda.BabitHeader = oBid;

                                    if (bda.BabitCategory == "PC")
                                    {
                                        bda.SubsidyAmount = a.PC;
                                    }
                                    if (bda.BabitCategory == "LCV")
                                    {
                                        bda.SubsidyAmount = a.LCV;
                                    }
                                    if (bda.BabitCategory == "XPANDER")
                                    {
                                        bda.SubsidyAmount = a.XPANDER;
                                    }
                                    if (bda.BabitCategory == "SPESIAL")
                                    {
                                        bda.SubsidyAmount = a.SPESIAL;
                                    }
                                    arrBDA.Add(bda);
                                }
                            }
                        }
                        if (iklan.Attachments.Count > 0)
                        {
                            foreach (var attach in iklan.Attachments)
                            {
                                BabitApprovalDocument bDoc = new BabitApprovalDocument();
                                bDoc.BabitHeader = oBid;
                                bDoc.FileName = attach.Filename;
                                bDoc.FileDescription = attach.Description;
                                bDoc.Path = attach.Path;
                                arrBDoc.Add(bDoc);
                            }
                        }
                        //ApprovedBudgetDetails
                        var _result = new BabitHeaderFacade(user).UpdateTransaction(oBid, arrBDA, arrBDoc);
                    }
                }
            }
            catch (Exception ex)
            {
                if (message.Length == 0)
                {
                    message += ex.Message;
                }
                else
                {
                    message += "\n" + ex.Message;
                }
            }
        }
        private void updatePameran(MasterBabit pc, ref string message)
        {
            try
            {
                GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
                foreach (var pameran in pc.Pameran)
                {
                    CriteriaComposite cr = new CriteriaComposite(new Criteria(typeof(BabitHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    cr.opAnd(new Criteria(typeof(BabitHeader), "BabitRegNumber", MatchType.Exact, pameran.ProposalCode.Trim()));
                    cr.opAnd(new Criteria(typeof(BabitHeader), "BabitStatus", MatchType.Exact, 7));
                    ArrayList arrBid = new BabitHeaderFacade(user).Retrieve(cr);
                    ArrayList arrBDoc = new ArrayList();
                    ArrayList arrBDA = new ArrayList();

                    if (arrBid.Count > 0)
                    {
                        BabitHeader oBH = (BabitHeader)arrBid[0];
                        oBH.BabitStatus = Convert.ToInt16(pameran.Status);
                        oBH.ApprovalNumber = pameran.ApprovalNumber;
                        //oBH.ProspectTarget = Convert.ToInt32(pameran.TargetedSPK);
                        oBH.SPKTarget = Convert.ToInt32(pameran.TargetedSPK);
                        if (pameran.Attachments.Count > 0)
                        {
                            foreach (var attach in pameran.Attachments)
                            {
                                BabitApprovalDocument bDoc = new BabitApprovalDocument();
                                bDoc.BabitHeader = oBH;
                                bDoc.FileName = attach.Filename;
                                bDoc.FileDescription = attach.Description;
                                bDoc.Path = attach.Path;
                                arrBDoc.Add(bDoc);
                            }
                        }

                        ArrayList listBDA = new BabitDealerAllocationFacade(user).RetrieveByBabitHeader(oBH.ID);
                        foreach (BabitDealerAllocation oBDA in listBDA)
                        {
                            foreach (var item in pameran.ApprovedBudgetDetails)
                            {
                                if (oBDA.Dealer.DealerCode == item.DealerBudget && oBDA.BabitCategory == "PC")
                                {
                                    oBDA.SubsidyAmount = item.PC;
                                    continue;
                                }
                                if (oBDA.Dealer.DealerCode == item.DealerBudget && oBDA.BabitCategory == "LCV")
                                {
                                    oBDA.SubsidyAmount = item.LCV;
                                    continue;
                                }
                                if (oBDA.Dealer.DealerCode == item.DealerBudget && oBDA.BabitCategory == "XPANDER")
                                {
                                    oBDA.SubsidyAmount = item.XPANDER;
                                    continue;
                                }
                                if (oBDA.Dealer.DealerCode == item.DealerBudget && oBDA.BabitCategory == "SPESIAL")
                                {
                                    oBDA.SubsidyAmount = item.SPESIAL;
                                    continue;
                                }
                            }
                            arrBDA.Add(oBDA);
                        }
                        var _result = new BabitHeaderFacade(user).UpdateTransaction(oBH, arrBDA, arrBDoc);
                    }
                }
            }
            catch (Exception ex)
            {
                if (message.Length == 0)
                {
                    message += ex.Message;
                }
                else
                {
                    message += "\n" + ex.Message;
                }
            }
        }
        private void updateEvent(MasterBabit pc, ref string message)
        {
            try
            {
                GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
                foreach (var even in pc.Event)
                {
                    CriteriaComposite cr = new CriteriaComposite(new Criteria(typeof(BabitHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    cr.opAnd(new Criteria(typeof(BabitHeader), "BabitRegNumber", MatchType.Exact, even.ProposalCode.Trim()));
                    cr.opAnd(new Criteria(typeof(BabitHeader), "BabitStatus", MatchType.Exact, 7));
                    ArrayList arrBid = new BabitHeaderFacade(user).Retrieve(cr);
                    ArrayList arrBDoc = new ArrayList();
                    ArrayList arrBDA = new ArrayList();

                    if (arrBid.Count > 0)
                    {
                        BabitHeader oBH = (BabitHeader)arrBid[0];
                        oBH.BabitStatus = Convert.ToInt16(even.Status);
                        oBH.ApprovalNumber = even.ApprovalNumber;
                        //oBH.ProspectTarget = even.TargetedSPK;
                        oBH.SPKTarget = even.TargetedSPK;
                        //BabitDealerAllocation oBDA = (BabitDealerAllocation)new BabitDealerAllocationFacade(user).RetrieveByBabitHeader(oBH.ID)[0];

                        if (even.Attachments.Count > 0)
                        {
                            foreach (var attach in even.Attachments)
                            {
                                BabitApprovalDocument bDoc = new BabitApprovalDocument();
                                bDoc.BabitHeader = oBH;
                                bDoc.FileName = attach.Filename;
                                bDoc.FileDescription = attach.Description;
                                bDoc.Path = attach.Path;
                                arrBDoc.Add(bDoc);
                            }
                        }

                        ArrayList listBDA = new BabitDealerAllocationFacade(user).RetrieveByBabitHeader(oBH.ID);
                        foreach (BabitDealerAllocation oBDA in listBDA)
                        {
                            foreach (var item in even.ApprovedBudgetDetails)
                            {
                                if (oBDA.Dealer.DealerCode == item.DealerBudget && oBDA.BabitCategory == "PC")
                                {
                                    oBDA.SubsidyAmount = item.PC;
                                    continue;
                                }
                                if (oBDA.Dealer.DealerCode == item.DealerBudget && oBDA.BabitCategory == "LCV")
                                {
                                    oBDA.SubsidyAmount = item.LCV;
                                    continue;
                                }
                                if (oBDA.Dealer.DealerCode == item.DealerBudget && oBDA.BabitCategory == "XPANDER")
                                {
                                    oBDA.SubsidyAmount = item.XPANDER;
                                    continue;
                                }
                                if (oBDA.Dealer.DealerCode == item.DealerBudget && oBDA.BabitCategory == "SPESIAL")
                                {
                                    oBDA.SubsidyAmount = item.SPESIAL;
                                    continue;
                                }
                            }
                            arrBDA.Add(oBDA);
                        }
                        var _result = new BabitHeaderFacade(user).UpdateTransaction(oBH, arrBDA, arrBDoc);
                    }
                }
            }
            catch (Exception ex)
            {
                if (message.Length == 0)
                {
                    message += ex.Message;
                }
                else
                {
                    message += "\n" + ex.Message;
                }
            }
        }

        [HttpGet]
        [Route("Events")]
        public IHttpActionResult Events()
        {
            GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
            if (!IsLogin()) return Content(HttpStatusCode.Unauthorized, "Your Token is invalid");



            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(BabitEventReportHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(BabitEventReportHeader), "Status", MatchType.Exact, 6));
            var _Result = new BabitEventReportHeaderFacade(user).Retrieve(criterias);
            var jsonForm = new List<MasterEvent>();
            try
            {
                foreach (BabitEventReportHeader oBH in _Result)
                {
                    var arrDoc = new ArrayList();
                    MasterEvent oMep = new MasterEvent();
                    CriteriaComposite criBabitEventProposalHeader = new CriteriaComposite(new Criteria(typeof(BabitEventProposalHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criBabitEventProposalHeader.opAnd(new Criteria(typeof(BabitEventProposalHeader), "ID", MatchType.Exact, oBH.BabitEventProposalHeader.ID));
                    BabitEventProposalHeader _EventProposal = (BabitEventProposalHeader)new BabitEventProposalHeaderFacade(user).Retrieve(criBabitEventProposalHeader)[0];
                    CriteriaComposite criBabitEventDetail = new CriteriaComposite(new Criteria(typeof(BabitEventProposalDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criBabitEventDetail.opAnd(new Criteria(typeof(BabitEventProposalDetail), "BabitEventProposalHeader.ID", MatchType.Exact, _EventProposal.ID));
                    ArrayList _ArrEventResult = new BabitEventProposalDetailFacade(user).Retrieve(criBabitEventDetail);
                    oMep.EventRegNumber = _EventProposal.EventRegNumber.ToString();
                    //oMep.EventProposalName = oBH.EventReportName.ToString();
                    oMep.EventProposalName = oBH.BabitEventProposalHeader.EventProposalName;
                    oMep.PeriodStart = oBH.PeriodStart;
                    oMep.PeriodEnd = oBH.PeriodEnd;
                    oMep.EventStatus = oBH.Status.ToString();
                    oMep.Notes = oBH.Notes.ToString();
                    oMep.DealerAddress = oBH.Dealer.Address.ToString();
                    oMep.City = oBH.Dealer.City.CityName.ToString();
                    oMep.Location = oBH.LocationName.ToString();
                    oMep.DealerName = oBH.Dealer.DealerName.ToString();
                    oMep.DealerCode = oBH.Dealer.DealerCode.ToString();
                    int biaya = 0;
                    foreach (BabitEventProposalDetail amount in _ArrEventResult)
                    {
                        int tempBiaya = amount.Qty * Convert.ToInt32(amount.Price);
                        biaya += tempBiaya;
                    }
                    oMep.ProposedBudget = biaya;
                    List<string> collDealer = new List<string>();
                    if (oBH.CollaborateDealer.Length != 0)
                    {
                        foreach (string oD in oBH.CollaborateDealer.Split(';'))
                        {
                            collDealer.Add(new DealerFacade(user).Retrieve(Convert.ToInt32(oD)).DealerCode);
                        }
                    }
                    oMep.CollaborateDealer = collDealer;
                    //oMep.Description = nothing
                    CriteriaComposite criBabitEventReportDocument = new CriteriaComposite(new Criteria(typeof(BabitEventReportDocument), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criBabitEventReportDocument.opAnd(new Criteria(typeof(BabitEventReportDocument), "BabitEventReportHeader.ID", MatchType.Exact, oBH.ID));
                    ArrayList arrDocEvent = new BabitEventReportDocumentFacade(user).Retrieve(criBabitEventReportDocument);
                    List<Attachments> lDocEvent = new List<Attachments>();
                    foreach (BabitEventReportDocument doc in arrDocEvent)
                    {
                        Attachments oEA = new Attachments();
                        oEA.Filename = doc.FileName;
                        oEA.Description = doc.FileDescription;
                        oEA.Path = doc.Path;
                        lDocEvent.Add(oEA);
                    }
                    oMep.ApprovedBudget = oBH.ConfirmedBudget;
                    oMep.Attachments = lDocEvent;
                    jsonForm.Add(oMep);
                }
            }
            catch (Exception ex)
            {
                string debug = ex.Message;
            }
            return Ok(jsonForm);
            //return this.result(success, "-1", 0, message, null);
        }

        [HttpPost]
        [Route("ProcessEvent")]
        public IDictionary<string, object> ProcessEvent()
        {
            GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
            bool success = false; string message = ""; int nCount = 0;
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);

            try
            {
                var jsonBody = Request.Content.ReadAsStringAsync().Result;
                var sapcustomer = JsonConvert.DeserializeObject<UpdateStatus>(jsonBody);
                int i = 0;
                foreach (var pc in sapcustomer.BabitData)
                {
                    try
                    {
                        CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(BabitEventReportHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        criterias.opAnd(new Criteria(typeof(BabitEventReportHeader), "BabitEventProposalHeader.EventRegNumber", MatchType.Exact, pc.RegNumber.Trim()));
                        criterias.opAnd(new Criteria(typeof(BabitEventReportHeader), "Status", MatchType.Exact, 6));
                        ArrayList arrBH = new BabitEventReportHeaderFacade(user).Retrieve(criterias);
                        if (arrBH.Count > 0)
                        {
                            BabitEventReportHeader oBH = (BabitEventReportHeader)arrBH[0];
                            oBH.Status = 7; //Process by GW
                            oBH.ApprovalNumber = pc.FolioNumber;
                            var _result = new BabitEventReportHeaderFacade(user).Update(oBH);
                            if (_result <= 0)
                            {
                                success = false;
                                if (message.Length == 0)
                                {
                                    message += "Update " + pc.RegNumber + " Failed";
                                }
                                else
                                {
                                    message += "\n" + "Update " + pc.RegNumber + " Failed";
                                }
                            }
                            else { success = true; nCount += 1; }
                        }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        if (message.Length == 0)
                        {
                            message += ex.Message;
                        }
                        else
                        {
                            message += "\n" + ex.Message;
                        }
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                success = false;
                if (message.Length == 0)
                {
                    message += ex.Message;
                }
                else
                {
                    message += "\n" + ex.Message;
                }
            }

            return this.result(success, "-1", nCount, message, null);
            //return Content(HttpStatusCode.OK, state);
        }

        [HttpPost]
        [Route("UpdateEvent")]
        public IDictionary<string, object> UpdateEvent()
        {
            GenericPrincipal user = new GenericPrincipal(new GenericIdentity("Groupware"), null);
            bool success = false; string message = "";
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);

            try
            {
                var jsonBody = Request.Content.ReadAsStringAsync().Result;
                var sapcustomer = JsonConvert.DeserializeObject<List<MasterEvent>>(jsonBody);
                ArrayList attach;
                BabitEventProposalHeader oBH;
                BabitEventReportHeader oBDA;
                foreach (var pc in sapcustomer)
                {
                    oBH = new BabitEventProposalHeader();
                    oBDA = new BabitEventReportHeader();
                    attach = new ArrayList();
                    try
                    {
                        CriteriaComposite cr = new CriteriaComposite(new Criteria(typeof(BabitEventProposalHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        cr.opAnd(new Criteria(typeof(BabitEventProposalHeader), "EventRegNumber", MatchType.Exact, pc.EventRegNumber.Trim()));
                        //cr.opAnd(new Criteria(typeof(BabitEventProposalHeader), "EventStatus", MatchType.Exact, 7));
                        ArrayList arrBid = new BabitEventProposalHeaderFacade(user).Retrieve(cr);
                        CriteriaComposite cr2 = new CriteriaComposite(new Criteria(typeof(BabitEventReportHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        cr2.opAnd(new Criteria(typeof(BabitEventReportHeader), "BabitEventProposalHeader.EventRegNumber", MatchType.Exact, pc.EventRegNumber.Trim()));
                        cr2.opAnd(new Criteria(typeof(BabitEventReportHeader), "Status", MatchType.Exact, 7));
                        ArrayList arroBDA = new BabitEventReportHeaderFacade(user).Retrieve(cr2);
                        if (arrBid.Count > 0)
                        {
                            oBDA = (BabitEventReportHeader)arroBDA[0];
                            oBH = (BabitEventProposalHeader)arrBid[0];
                            oBH.EventStatus = Convert.ToInt16(pc.EventStatus);
                            oBDA.ApprovalNumber = pc.ApprovalNumber;
                            oBDA.Status = Convert.ToInt16(pc.EventStatus);
                            oBDA.ApprovedBudget = pc.ApprovedBudget;
                            if (oBDA.ID == 0)
                            {
                                message += "There is no Data for " + oBH.EventRegNumber + " on Babit Event Report";
                                continue;
                            }
                            EventDealerRequiredDocument oEDRD = new EventDealerRequiredDocument();
                            oEDRD.EventDealerHeader = oBH.EventDealerHeader;
                            oEDRD.DocumentType = 3;
                            oEDRD.DocumentName = new StandardCodeFacade(user).GetByCategoryValue("EnumBabit.EventDocumentType", "3").ValueCode;
                            //var _rets = new EventDealerRequiredDocumentFacade(user).Insert(oEDRD);

                            foreach (var oEA in pc.Attachments)
                            {
                                //BabitEventReportDocument temp = new BabitEventReportDocument();
                                //temp.EventDealerRequiredDocument = new EventDealerRequiredDocumentFacade(user).Retrieve(_rets);
                                BabitEventReportApprovalDoc temp = new BabitEventReportApprovalDoc();
                                temp.BabitEventReportHeader = oBDA;
                                temp.FileName = oEA.Filename;
                                temp.FileDescription = oEA.Description;
                                temp.Path = oEA.Path;
                                attach.Add(temp);
                                //var lala = new BabitEventReportDocumentFacade(user).Insert(temp);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (message.Length == 0)
                        {
                            message += ex.Message;
                        }
                        else
                        {
                            message += "\n" + ex.Message;
                        }
                    }
                    int _resut = new BabitEventProposalHeaderFacade(user).UpdateTransaction(oBH, oBDA, attach);
                    if (_resut > 0) success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                if (message.Length == 0)
                {
                    message += ex.Message;
                }
                else
                {
                    message += "\n" + ex.Message;
                }
            }

            return this.result(success, "-1", 0, message, null);
            //return Content(HttpStatusCode.OK, state);
        }
    }
}
