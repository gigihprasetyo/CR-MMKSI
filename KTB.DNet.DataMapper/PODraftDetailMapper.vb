
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODraftDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 12/17/2018 - 5:50:59 PM
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

	public class PODraftDetailMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertPODraftDetail"
		private m_UpdateStatement as string = "up_UpdatePODraftDetail"
		private m_RetrieveStatement as string = "up_RetrievePODraftDetail"
		private m_RetrieveListStatement as string = "up_RetrievePODraftDetailList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeletePODraftDetail"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim pODraftDetail as PODraftDetail = nothing
			while dr.Read
			
				pODraftDetail = me.CreateObject(dr)
			            
			end while        					
			
			return pODraftDetail
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim pODraftDetailList as ArrayList = new ArrayList
			
			while dr.Read
					dim pODraftDetail as PODraftDetail = me.CreateObject(dr)
					pODraftDetailList.Add(pODraftDetail)
			end while
			     
			return pODraftDetailList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim pODraftDetail as PODraftDetail = ctype(obj, PODraftDetail)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pODraftDetail.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim pODraftDetail as PODraftDetail = ctype(obj, PODraftDetail)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@PODraftHeaderID",DbType.Int32,pODraftDetail.PODraftHeaderID)
			DbCommandWrapper.AddInParameter("@LineItem",DbType.Int16,pODraftDetail.LineItem)
            'DbCommandWrapper.AddInParameter("@ContractDetailID",DbType.Int32,pODraftDetail.ContractDetailID)
			DbCommandWrapper.AddInParameter("@ReqQty",DbType.Int32,pODraftDetail.ReqQty)
			DbCommandWrapper.AddInParameter("@Price",DbType.Currency,pODraftDetail.Price)
			DbCommandWrapper.AddInParameter("@Discount",DbType.Currency,pODraftDetail.Discount)
			DbCommandWrapper.AddInParameter("@Interest",DbType.Currency,pODraftDetail.Interest)
			DbCommandWrapper.AddInParameter("@DiscountReward",DbType.Currency,pODraftDetail.DiscountReward)
			DbCommandWrapper.AddInParameter("@AmountReward",DbType.Currency,pODraftDetail.AmountReward)
			DbCommandWrapper.AddInParameter("@AmountRewardDepA",DbType.Currency,pODraftDetail.AmountRewardDepA)
			DbCommandWrapper.AddInParameter("@PPh22",DbType.Currency,pODraftDetail.PPh22)
			DbCommandWrapper.AddInParameter("@LogisticCost",DbType.Currency,pODraftDetail.LogisticCost)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pODraftDetail.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,pODraftDetail.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, pODraftDetail.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, pODraftDetail.MaxTOPDay)

            DbCommandWrapper.AddInParameter("@ContractDetailID", DbType.Int32, Me.GetRefObject(pODraftDetail.ContractDetail))
            DbCommandWrapper.AddInParameter("@PODraftHeaderID", DbType.Int32, Me.GetRefObject(pODraftDetail.PODraftHeader))
						
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
		
			dim pODraftDetail as PODraftDetail = ctype(obj, PODraftDetail)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pODraftDetail.ID)
            'DbCommandWrapper.AddInParameter("@PODraftHeaderID",DbType.Int32,pODraftDetail.PODraftHeaderID)
			DbCommandWrapper.AddInParameter("@LineItem",DbType.Int16,pODraftDetail.LineItem)
            'DbCommandWrapper.AddInParameter("@ContractDetailID",DbType.Int32,pODraftDetail.ContractDetailID)
			DbCommandWrapper.AddInParameter("@ReqQty",DbType.Int32,pODraftDetail.ReqQty)
			DbCommandWrapper.AddInParameter("@Price",DbType.Currency,pODraftDetail.Price)
			DbCommandWrapper.AddInParameter("@Discount",DbType.Currency,pODraftDetail.Discount)
			DbCommandWrapper.AddInParameter("@Interest",DbType.Currency,pODraftDetail.Interest)
			DbCommandWrapper.AddInParameter("@DiscountReward",DbType.Currency,pODraftDetail.DiscountReward)
			DbCommandWrapper.AddInParameter("@AmountReward",DbType.Currency,pODraftDetail.AmountReward)
			DbCommandWrapper.AddInParameter("@AmountRewardDepA",DbType.Currency,pODraftDetail.AmountRewardDepA)
			DbCommandWrapper.AddInParameter("@PPh22",DbType.Currency,pODraftDetail.PPh22)
			DbCommandWrapper.AddInParameter("@LogisticCost",DbType.Currency,pODraftDetail.LogisticCost)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pODraftDetail.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,pODraftDetail.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, pODraftDetail.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, pODraftDetail.MaxTOPDay)

            DbCommandWrapper.AddInParameter("@ContractDetailID", DbType.Int32, Me.GetRefObject(pODraftDetail.ContractDetail))
            DbCommandWrapper.AddInParameter("@PODraftHeaderID", DbType.Int32, Me.GetRefObject(pODraftDetail.PODraftHeader))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as PODraftDetail
		
			dim pODraftDetail as PODraftDetail = new PODraftDetail
			
			pODraftDetail.ID = ctype(dr("ID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("PODraftHeaderID")) then pODraftDetail.PODraftHeaderID = ctype(dr("PODraftHeaderID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("LineItem")) then pODraftDetail.LineItem = ctype(dr("LineItem"), short) 
            'if not dr.IsDBNull(dr.GetOrdinal("ContractDetailID")) then pODraftDetail.ContractDetailID = ctype(dr("ContractDetailID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("ReqQty")) then pODraftDetail.ReqQty = ctype(dr("ReqQty"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Price")) then pODraftDetail.Price = ctype(dr("Price"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("Discount")) then pODraftDetail.Discount = ctype(dr("Discount"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("Interest")) then pODraftDetail.Interest = ctype(dr("Interest"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("DiscountReward")) then pODraftDetail.DiscountReward = ctype(dr("DiscountReward"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("AmountReward")) then pODraftDetail.AmountReward = ctype(dr("AmountReward"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("AmountRewardDepA")) then pODraftDetail.AmountRewardDepA = ctype(dr("AmountRewardDepA"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("PPh22")) then pODraftDetail.PPh22 = ctype(dr("PPh22"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("LogisticCost")) then pODraftDetail.LogisticCost = ctype(dr("LogisticCost"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then pODraftDetail.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then pODraftDetail.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then pODraftDetail.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then pODraftDetail.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then pODraftDetail.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("FreeDays")) Then pODraftDetail.FreeDays = CType(dr("FreeDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDay")) Then pODraftDetail.MaxTOPDay = CType(dr("MaxTOPDay"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("PODraftHeaderID")) Then
                pODraftDetail.PODraftHeader = New PODraftHeader(CType(dr("PODraftHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ContractDetailID")) Then
                pODraftDetail.ContractDetail = New ContractDetail(CType(dr("ContractDetailID"), Integer))
            End If

			return pODraftDetail
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (PODraftDetail) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(PODraftDetail),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(PODraftDetail).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

