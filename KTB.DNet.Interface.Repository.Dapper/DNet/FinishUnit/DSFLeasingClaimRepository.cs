using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.FinishUnit.SQLQuery.FinishUnit;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class DSFLeasingClaimDocumentRepository : BaseDNetRepository<DSFLeasingClaimDocument>, IDSFLeasingClaimDocumentRepository<DSFLeasingClaimDocument, int>
    {
        public DSFLeasingClaimDocumentRepository(string connectionString)
            : base(connectionString)
        { }

        public bool Save(DSFLeasingClaimDocument data)
        {
            bool b = false;
            using (var cn = Connection)
            {
                try
                {
                    var result = ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.ExecuteScalar<int>(
                            @"
INSERT INTO DSFLeasingClaimDocument
(
DSFLeasingClaimID,
FileName,
FileDescription,
Path,
SourceData,
RowStatus,
CreatedBy,
CreatedTime,
LastUpdateBy,
LastUpdateTime
)
OUTPUT INSERTED.ID
VALUES
(
@DSFLeasingClaimID,
@FileName,
@FileDescription,
@Path,
1,
@RowStatus,
@CreatedBy,
@CreatedTime,
@LastUpdateBy,
@LastUpdateTime
);
"
                            ,
                            new
                            {
                                @DSFLeasingClaimID = data.DSFLeasingClaim.ID,
                                @FileName = data.FileName,
                                @FileDescription = data.FileDescription,
                                @Path = data.Path,
                                @SourceData = data.SourceData,
                                @RowStatus = 0,
                                @CreatedBy = "SYSTEM",
                                @CreatedTime = DateTime.Now,
                                @LastUpdateBy = "SYSTEM",
                                @LastUpdateTime = DateTime.Now
                            },
                            transaction);
                    });
                    b = true;
                }
                catch
                {

                }
            }
            return b;
        }


        public List<DSFLeasingClaimDocument> Search(ICriteria criteria, ICriteria criteriaDealer, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public List<DSFLeasingClaimDocument> Search(ICriteria criteria)
        {
            throw new NotImplementedException();
        }


        public DSFLeasingClaimDocument Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(DSFLeasingClaimDocument entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(DSFLeasingClaimDocument entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DSFLeasingClaimDocument> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DSFLeasingClaimDocument> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }

    public class DSFLeasingClaimRepository : BaseDNetRepository<DSFLeasingClaim>, IDSFLeasingClaimRepository<DSFLeasingClaim, int>
    {
        public DSFLeasingClaimRepository(string connectionString)
            : base(connectionString)
        { }

        public bool Save(List<DSFLeasingClaim> data)
        {
            bool b = false;
            using (var cn = Connection)
            {
                data.ForEach(i => {
                    try
                    {
                        var result = ExecuteTransaction(Connection, (connection, transaction) =>
                        {
                            return connection.ExecuteScalar<int>(DSFLeasingClaimQuery.InsertQueryDSFLeasingClaim,
                                new
                                {
                                    @RegNumber = i.RegNumber,
                                    @DealerID = i.Dealer.ID,
                                    @ChassisMasterID = i.ChassisMaster.ID,
                                    @ClaimDate = i.ClaimDate,
                                    @AssetSeqNo = i.AssetSeqNo,
                                    @AgreementNo = i.AgreementNo,
                                    @SKDNumber = i.SKDNumber,
                                    @SKDDate = i.SKDDate,
                                    @SKDApprovalDate = i.SKDApprovalDate,
                                    @GoLiveDate = i.GoLiveDate,
                                    @CustomerName = i.CustomerName,
                                    @Unit = i.Unit,
                                    @ObjectLease = i.ObjectLease,
                                    @ATPMSubsidy = i.ATPMSubsidy,
                                    @SupplierName = i.SupplierName,
                                    @ProgramName = i.ProgramName,
                                    @CollectionPeriodMonth = i.CollectionPeriodMonth,
                                    @CollectionPeriodYear = i.CollectionPeriodYear,
                                    @TotalDP = i.TotalDP,
                                    @TotalAmountLease = i.TotalAmountLease,
                                    @PeriodLease = i.PeriodLease,
                                    @InterestLease = i.InterestLease,
                                    @Insurance = i.Insurance,
                                    @TypeInsurance = i.TypeInsurance,
                                    @Status = i.Status,
                                    @RemarkByDealer = i.RemarkByDealer,
                                    @RemarkByDSF = i.RemarkByDSF,
                                    @RowStatus = i.RowStatus,
                                    @CreatedBy = "SYSTEM",
                                    @CreatedTime = DateTime.Now,
                                    @LastUpdateBy = "SYSTEM",
                                    @LastUpdateTime = DateTime.Now
                                },
                                transaction);
                        });
                        b = true;
                    }
                    catch
                    {
                        
                    }
                    
                });
            }
            return b;
        }

        public List<DSFLeasingClaim> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            List<DSFLeasingClaim> result = null;
            filteredResultsCount = 0;
            using (var cn = Connection)
            {
                result = cn.Query<DSFLeasingClaim>(string.Format(DSFLeasingClaimQuery.SelectQueryDSFLeasingClaim, criteria.ToString())).ToList();
                if (result != null && result.Count > 0)
                {
                    filteredResultsCount = 1;
                }
                else
                {
                    filteredResultsCount = 0;
                }
            }
            
            totalResultsCount = filteredResultsCount;
            return result;
        }

        public List<DSFLeasingClaim> Search(ICriteria criteria)
        {
            List<DSFLeasingClaim> result = null;
            using (var cn = Connection)
            {
                result = cn.Query<DSFLeasingClaim>(string.Format(DSFLeasingClaimQuery.SelectQueryDSFLeasingClaim, criteria.ToString())).ToList();
            }
            return result;
        }

        public DSFLeasingClaim Get(int id)
        {
            DSFLeasingClaim result = null;
            using (var cn = Connection)
            {
                result = cn.Query<DSFLeasingClaim>(string.Format(DSFLeasingClaimQuery.SelectQueryDSFLeasingClaim, string.Format("where ID={0}",id))).ToList().SingleOrDefault();
            }
            return result;
        }

        public ResponseMessage Create(DSFLeasingClaim entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<int>(DSFLeasingClaimQuery.InsertQueryDSFLeasingClaim,
                        new
                        {
                            @RegNumber = entity.RegNumber,
                            @DealerID = entity.Dealer.ID,
                            @ChassisMasterID = entity.ChassisMaster.ID,
                            @ClaimDate = entity.ClaimDate,
                            @AssetSeqNo = entity.AssetSeqNo,
                            @AgreementNo = entity.AgreementNo,
                            @SKDNumber = entity.SKDNumber,
                            @SKDDate = entity.SKDDate,
                            @SKDApprovalDate = entity.SKDApprovalDate,
                            @GoLiveDate = entity.GoLiveDate,
                            @CustomerName = entity.CustomerName,
                            @Unit = entity.Unit,
                            @ObjectLease = entity.ObjectLease,
                            @ATPMSubsidy = entity.ATPMSubsidy,
                            @SupplierName = entity.SupplierName,
                            @ProgramName = entity.ProgramName,
                            @CollectionPeriodMonth = entity.CollectionPeriodMonth,
                            @CollectionPeriodYear = entity.CollectionPeriodYear,
                            @TotalDP = entity.TotalDP,
                            @TotalAmountLease = entity.TotalAmountLease,
                            @PeriodLease = entity.PeriodLease,
                            @InterestLease = entity.InterestLease,
                            @Insurance = entity.Insurance,
                            @TypeInsurance = entity.TypeInsurance,
                            @Status = entity.Status,
                            @RemarkByDealer = entity.RemarkByDealer,
                            @RemarkByMKS = entity.RemarkByDSF,
                            @RowStatus = entity.RowStatus,
                            @CreatedBy = "SYSTEM",
                            @CreatedTime = DateTime.Now,
                            @LastUpdateBy = "SYSTEM",
                            @LastUpdateTime = DateTime.Now
                        },
                        transaction);
                });
                entity.ID = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New DSFLeasingClaim has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Deal. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }

        public ResponseMessage Create(List<DSFLeasingClaim> entity)
        {
            List<DSFLeasingClaim> result = null;
            using (var cn = Connection)
            {
                cn.BeginTransaction();

                //result = cn.Query<DSFLeasingClaim>(string.Format(DSFLeasingClaimQuery.InsertQueryDSFLeasingClaim)).ToList();
            }
            return new ResponseMessage();
        }

        public ResponseMessage Update(DSFLeasingClaim entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                DSFLeasingClaim objDSFLeasingClaim = Get((int)entity.ID);
                if (objDSFLeasingClaim != null)
                {
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(
                            @"
UPDATE DSFLeasingClaim
	SET 
	 Status				= @Status
	 ,RemarkByDSF			= @RemarkByDSF
	 ,RowStatus				= @RowStatus
	 ,LastUpdateBy			= @LastUpdateBy
	 ,LastUpdateTime		= @LastUpdateTime
WHERE ID = @ID
",
                            new
                            {
                                @ID = entity.ID
                                //,@RegNumber              = objDSFLeasingClaim.RegNumber
                                //,@BenefitClaimHeaderID   = objDSFLeasingClaim.BenefitClaimHeader.ID
                                //,@DealerID               = objDSFLeasingClaim.Dealer.ID
                                //,@ChassisMasterID        = objDSFLeasingClaim.ChassisMaster.ID
                                //,@ClaimDate              = objDSFLeasingClaim.ClaimDate
                                //,@AssetSeqNo             = objDSFLeasingClaim.AssetSeqNo
                                //,@AgreementNo            = objDSFLeasingClaim.AgreementNo
                                //,@SKDNumber              = objDSFLeasingClaim.SKDNumber
                                //,@SKDDate                = objDSFLeasingClaim.SKDDate
                                //,@SKDApprovalDate        = objDSFLeasingClaim.SKDApprovalDate
                                //,@GoLiveDate             = objDSFLeasingClaim.GoLiveDate
                                //,@CustomerName           = objDSFLeasingClaim.CustomerName
                                //,@Unit                   = objDSFLeasingClaim.Unit
                                //,@ObjectLease            = objDSFLeasingClaim.ObjectLease
                                //,@ATPMSubsidy            = objDSFLeasingClaim.ATPMSubsidy
                                //,@SupplierName           = objDSFLeasingClaim.SupplierName
                                //,@ProgramName            = objDSFLeasingClaim.ProgramName
                                //,@CollectionPeriodMonth  = objDSFLeasingClaim.CollectionPeriodMonth
                                //,@CollectionPeriodYear   = objDSFLeasingClaim.CollectionPeriodYear
                                //,@TotalDP                = objDSFLeasingClaim.TotalDP
                                //,@TotalAmountLease       = objDSFLeasingClaim.TotalAmountLease
                                //,@PeriodLease            = objDSFLeasingClaim.PeriodLease
                                //,@InterestLease          = objDSFLeasingClaim.InterestLease
                                //,@Insurance              = objDSFLeasingClaim.Insurance
                                //,@TypeInsurance          = objDSFLeasingClaim.TypeInsurance
                                ,@Status                 = entity.Status
                                //,@RemarkByDealer         = objDSFLeasingClaim.RemarkByDealer
                                ,@RemarkByDSF            = entity.RemarkByDSF
                                ,@RowStatus = 0
                                ,@LastUpdateBy = entity.LastUpdateBy
                                ,@LastUpdateTime = DateTime.Now
                            },
                            transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("DSFLeasingClaim has been successfully updated");
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "DSFLeasingClaim does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update DSFLeasingClaim. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DSFLeasingClaim> GetAll()
        {
            var result = new List<DSFLeasingClaim>();
            using (var cn = Connection)
            {
                result = cn.Query<DSFLeasingClaim>(string.Format(DSFLeasingClaimQuery.SelectQueryDSFLeasingClaim, "")).ToList();
            }
            return result;
        }

        public List<DSFLeasingClaim> Search(ICriteria criteria, ICriteria criteriaDealer, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public List<ChassisMaster> ListChassisMaster(ICriteria criteria)
        {
            List<ChassisMaster> result = null;
            using (var cn = Connection)
            {
                result = cn.Query<ChassisMaster>(string.Format(DSFLeasingClaimQuery.SelectQueryChassisMaster, criteria.ToString())).ToList();
            }
            return result;
        }

        public List<DSFLeasingClaimDocument> ListDSFLeasingClaimDocument(ICriteria criteria)
        {
            List<DSFLeasingClaimDocument> result = null;
            using (var cn = Connection)
            {
                result = cn.Query<DSFLeasingClaimDocument>(string.Format("select * from dsfleasingclaimdocument {0}", criteria.ToString())).ToList();
            }
            return result;
        }

    }
}
