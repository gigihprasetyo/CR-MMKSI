using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNET.BusinessFacade;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System.Security.Principal;

namespace KTB.DNet.Salesforce.Class
{
    class LogParser
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

        public int InsertStagingLog(int SFMasterObjectID, bool IsSuccess, String ErrorMsg) 
        {
            SFStagingLog obj = new SFStagingLog();
            obj.TransactionDate = DateTime.Now;
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.IsSuccess = IsSuccess;
            obj.ErrorMessage = ErrorMsg;
            
            return new SFStagingLogFacade(User).Insert(obj); 
        }

        public int UpdateStagingLog(int SFMasterObjectID, bool IsSuccess, String ErrorMsg)
        {
            SFStagingLog obj = new SFStagingLog();
            obj.TransactionDate = DateTime.Now;
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.IsSuccess = IsSuccess;
            obj.ErrorMessage = ErrorMsg;

            return new SFStagingLogFacade(User).Update(obj);
        }

        public void InsertSynchronizeLog(int SFStagingLogID, int TransactionID,bool IsSuccess, string ErrorMsg)
        {
            SFSynchronizeLog obj = new SFSynchronizeLog();
            obj.SFStagingLog = new SFStagingLog(ID: SFStagingLogID);
            obj.TransactionID = TransactionID;
            obj.SynchronizeDate = DateTime.Now;
            obj.IsSuccess = IsSuccess;
            obj.ErrorMessage = ErrorMsg;
            new SFSynchronizeLogFacade(User).Insert(obj);
        }

        public void InsertErrorLog(int SFMasterObjectID, Exception e)
        {
            SFErrorLog obj = new SFErrorLog();
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.ExceptionMessage = e.Message;
            obj.ExceptionStartTrace = e.StackTrace;
            obj.ErrorDate = DateTime.Now;
            new SFErrorLogFacade(User).Insert(obj);
        }
    }
}
