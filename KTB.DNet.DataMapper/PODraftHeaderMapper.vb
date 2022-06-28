
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODraftHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 12/14/2018 - 2:59:37 PM
'//
'// ===========================================================================	
#end region


#region ".NET Base Class Namespace Imports"
imports System
imports System.Data
imports System.Collections
#end region

#region "Custom Namespace Imports"
imports Microsoft.Practices.EnterpriseLibrary.Data
imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
imports Microsoft.Practices.EnterpriseLibrary.Logging
imports KTB.DNet.DataMapper.Framework
imports KTB.DNet.Domain
imports KTB.DNet.Domain.Search
#end region

namespace KTB.DNet.DataMapper

	public class PODraftHeaderMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertPODraftHeader"
		private m_UpdateStatement as string = "up_UpdatePODraftHeader"
		private m_RetrieveStatement as string = "up_RetrievePODraftHeader"
		private m_RetrieveListStatement as string = "up_RetrievePODraftHeaderList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeletePODraftHeader"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim pODraftHeader as PODraftHeader = nothing
			while dr.Read
			
				pODraftHeader = me.CreateObject(dr)
			            
			end while        					
			
			return pODraftHeader
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim pODraftHeaderList as ArrayList = new ArrayList
			
			while dr.Read
					dim pODraftHeader as PODraftHeader = me.CreateObject(dr)
					pODraftHeaderList.Add(pODraftHeader)
			end while
			     
			return pODraftHeaderList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim pODraftHeader as PODraftHeader = ctype(obj, PODraftHeader)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pODraftHeader.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim pODraftHeader as PODraftHeader = ctype(obj, PODraftHeader)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@DealerID",DbType.Int32,pODraftHeader.DealerID)
			DbCommandWrapper.AddInParameter("@DraftPONumber",DbType.AnsiString,pODraftHeader.DraftPONumber)
			DbCommandWrapper.AddInParameter("@Status",DbType.AnsiString,pODraftHeader.Status)
            'DbCommandWrapper.AddInParameter("@ContractHeaderID",DbType.Int32,pODraftHeader.ContractHeaderID)
			DbCommandWrapper.AddInParameter("@ReqAllocationDate",DbType.Byte,pODraftHeader.ReqAllocationDate)
			DbCommandWrapper.AddInParameter("@ReqAllocationMonth",DbType.Byte,pODraftHeader.ReqAllocationMonth)
			DbCommandWrapper.AddInParameter("@ReqAllocationYear",DbType.Int16,pODraftHeader.ReqAllocationYear)
			DbCommandWrapper.AddInParameter("@ReqAllocationDateTime",DbType.DateTime,pODraftHeader.ReqAllocationDateTime)
			DbCommandWrapper.AddInParameter("@EffectiveDate",DbType.DateTime,pODraftHeader.EffectiveDate)
			DbCommandWrapper.AddInParameter("@DealerPONumber",DbType.AnsiString,pODraftHeader.DealerPONumber)
            'DbCommandWrapper.AddInParameter("@TermOfPaymentID",DbType.Int32,pODraftHeader.TermOfPaymentID)
			DbCommandWrapper.AddInParameter("@POType",DbType.AnsiStringFixedLength,pODraftHeader.POType)
			DbCommandWrapper.AddInParameter("@FreePPh22Indicator",DbType.Byte,pODraftHeader.FreePPh22Indicator)
			DbCommandWrapper.AddInParameter("@PassTOP",DbType.Byte,pODraftHeader.PassTOP)
			DbCommandWrapper.AddInParameter("@IsFactoring",DbType.Int16,pODraftHeader.IsFactoring)
            'DbCommandWrapper.AddInParameter("@SPLID",DbType.Int32,pODraftHeader.SPLID)
			DbCommandWrapper.AddInParameter("@IsTransfer",DbType.Int16,pODraftHeader.IsTransfer)
            'DbCommandWrapper.AddInParameter("@PODestinationID",DbType.Int32,pODraftHeader.PODestinationID)
            'DbCommandWrapper.AddInParameter("@POHeaderID",DbType.Int16,pODraftHeader.POHeaderID)
            DbCommandWrapper.AddInParameter("@SubmitPODate", DbType.DateTime, pODraftHeader.SubmitPODate)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pODraftHeader.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,pODraftHeader.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ContractHeaderID", DbType.Int32, Me.GetRefObject(pODraftHeader.ContractHeader))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(pODraftHeader.TermOfPayment))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(pODraftHeader.Dealer))
            DbCommandWrapper.AddInParameter("@SPLID", DbType.Int32, Me.GetRefObject(pODraftHeader.SPL))
            DbCommandWrapper.AddInParameter("@PODestinationID", DbType.Int32, Me.GetRefObject(pODraftHeader.PODestination))
            DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(pODraftHeader.POHeader))

			return DbCommandWrapper
			
		end function
	
		protected overrides function GetNewID(byval dbCommandWrapper as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) as integer

			return ctype(dbCommandWrapper.GetParameterValue("@ID"), integer)

		end function
		
		protected overrides function GetPagingRetrieveCommand as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_PagingQuery)
			DbCommandWrapper.AddInParameter("@Table",DbType.String,m_TableName)
			DbCommandWrapper.AddInParameter("@PK",DbType.String,"ID")
							
			return DbCommandWrapper
		
		end function
		
		protected overrides function GetRetrieveCommand as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DinamicQuery)
			DbCommandWrapper.AddInParameter("@sqlQuery",DbType.String,"SELECT " + m_TableName + ".* FROM " + m_TableName)
			
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetRetrieveListParameter as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_RetrieveListStatement)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetRetrieveParameter(byval id as integer) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_RetrieveStatement)
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,id)
				
			return DbCommandWrapper
			
		end function

		protected overrides function GetUpdateParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim pODraftHeader as PODraftHeader = ctype(obj, PODraftHeader)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pODraftHeader.ID)
            'DbCommandWrapper.AddInParameter("@DealerID",DbType.Int32,pODraftHeader.DealerID)
			DbCommandWrapper.AddInParameter("@DraftPONumber",DbType.AnsiString,pODraftHeader.DraftPONumber)
			DbCommandWrapper.AddInParameter("@Status",DbType.AnsiString,pODraftHeader.Status)
            'DbCommandWrapper.AddInParameter("@ContractHeaderID",DbType.Int32,pODraftHeader.ContractHeaderID)
			DbCommandWrapper.AddInParameter("@ReqAllocationDate",DbType.Byte,pODraftHeader.ReqAllocationDate)
			DbCommandWrapper.AddInParameter("@ReqAllocationMonth",DbType.Byte,pODraftHeader.ReqAllocationMonth)
			DbCommandWrapper.AddInParameter("@ReqAllocationYear",DbType.Int16,pODraftHeader.ReqAllocationYear)
			DbCommandWrapper.AddInParameter("@ReqAllocationDateTime",DbType.DateTime,pODraftHeader.ReqAllocationDateTime)
			DbCommandWrapper.AddInParameter("@EffectiveDate",DbType.DateTime,pODraftHeader.EffectiveDate)
			DbCommandWrapper.AddInParameter("@DealerPONumber",DbType.AnsiString,pODraftHeader.DealerPONumber)
            'DbCommandWrapper.AddInParameter("@TermOfPaymentID",DbType.Int32,pODraftHeader.TermOfPaymentID)
			DbCommandWrapper.AddInParameter("@POType",DbType.AnsiStringFixedLength,pODraftHeader.POType)
			DbCommandWrapper.AddInParameter("@FreePPh22Indicator",DbType.Byte,pODraftHeader.FreePPh22Indicator)
			DbCommandWrapper.AddInParameter("@PassTOP",DbType.Byte,pODraftHeader.PassTOP)
			DbCommandWrapper.AddInParameter("@IsFactoring",DbType.Int16,pODraftHeader.IsFactoring)
            'DbCommandWrapper.AddInParameter("@SPLID",DbType.Int32,pODraftHeader.SPLID)
			DbCommandWrapper.AddInParameter("@IsTransfer",DbType.Int16,pODraftHeader.IsTransfer)
            'DbCommandWrapper.AddInParameter("@PODestinationID",DbType.Int32,pODraftHeader.PODestinationID)
            'DbCommandWrapper.AddInParameter("@POHeaderID",DbType.Int16,pODraftHeader.POHeaderID)
            DbCommandWrapper.AddInParameter("@SubmitPODate", DbType.DateTime, pODraftHeader.SubmitPODate)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pODraftHeader.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,pODraftHeader.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
            DbCommandWrapper.AddInParameter("@ContractHeaderID", DbType.Int32, Me.GetRefObject(pODraftHeader.ContractHeader))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(pODraftHeader.TermOfPayment))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(pODraftHeader.Dealer))
            DbCommandWrapper.AddInParameter("@SPLID", DbType.Int32, Me.GetRefObject(pODraftHeader.SPL))
            DbCommandWrapper.AddInParameter("@PODestinationID", DbType.Int32, Me.GetRefObject(pODraftHeader.PODestination))
            DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(pODraftHeader.POHeader))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as PODraftHeader
		
			dim pODraftHeader as PODraftHeader = new PODraftHeader
			
			pODraftHeader.ID = ctype(dr("ID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("DealerID")) then pODraftHeader.DealerID = ctype(dr("DealerID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DraftPONumber")) then pODraftHeader.DraftPONumber = dr("DraftPONumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then pODraftHeader.Status = dr("Status").ToString 
            'if not dr.IsDBNull(dr.GetOrdinal("ContractHeaderID")) then pODraftHeader.ContractHeaderID = ctype(dr("ContractHeaderID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("ReqAllocationDate")) then pODraftHeader.ReqAllocationDate = ctype(dr("ReqAllocationDate"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("ReqAllocationMonth")) then pODraftHeader.ReqAllocationMonth = ctype(dr("ReqAllocationMonth"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("ReqAllocationYear")) then pODraftHeader.ReqAllocationYear = ctype(dr("ReqAllocationYear"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("ReqAllocationDateTime")) then pODraftHeader.ReqAllocationDateTime = ctype(dr("ReqAllocationDateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) then pODraftHeader.EffectiveDate = ctype(dr("EffectiveDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("DealerPONumber")) then pODraftHeader.DealerPONumber = dr("DealerPONumber").ToString 
            'if not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) then pODraftHeader.TermOfPaymentID = ctype(dr("TermOfPaymentID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("POType")) then pODraftHeader.POType = dr("POType").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("FreePPh22Indicator")) then pODraftHeader.FreePPh22Indicator = ctype(dr("FreePPh22Indicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("PassTOP")) then pODraftHeader.PassTOP = ctype(dr("PassTOP"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("IsFactoring")) then pODraftHeader.IsFactoring = ctype(dr("IsFactoring"), short) 
            'if not dr.IsDBNull(dr.GetOrdinal("SPLID")) then pODraftHeader.SPLID = ctype(dr("SPLID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("IsTransfer")) then pODraftHeader.IsTransfer = ctype(dr("IsTransfer"), short) 
            'if not dr.IsDBNull(dr.GetOrdinal("PODestinationID")) then pODraftHeader.PODestinationID = ctype(dr("PODestinationID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) then pODraftHeader.POHeaderID = ctype(dr("POHeaderID"), short) 
            If Not dr.IsDBNull(dr.GetOrdinal("SubmitPODate")) Then pODraftHeader.SubmitPODate = CType(dr("SubmitPODate"), DateTime)
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then pODraftHeader.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then pODraftHeader.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then pODraftHeader.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then pODraftHeader.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then pODraftHeader.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pODraftHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ContractHeaderID")) Then
                pODraftHeader.ContractHeader = New ContractHeader(CType(dr("ContractHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then
                pODraftHeader.TermOfPayment = New TermOfPayment(CType(dr("TermOfPaymentID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPLID")) Then
                pODraftHeader.SPL = New SPL(CType(dr("SPLID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PODestinationID")) Then
                pODraftHeader.PODestination = New PODestination(CType(dr("PODestinationID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) Then
                pODraftHeader.POHeader = New POHeader(CType(dr("POHeaderID"), Integer))
            End If

			return pODraftHeader
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (PODraftHeader) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(PODraftHeader),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(PODraftHeader).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

