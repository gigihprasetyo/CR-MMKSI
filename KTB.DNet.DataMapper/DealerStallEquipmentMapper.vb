
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerStallEquipment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 26/05/2020 - 23:23:07
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

	public class DealerStallEquipmentMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertDealerStallEquipment"
		private m_UpdateStatement as string = "up_UpdateDealerStallEquipment"
		private m_RetrieveStatement as string = "up_RetrieveDealerStallEquipment"
		private m_RetrieveListStatement as string = "up_RetrieveDealerStallEquipmentList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteDealerStallEquipment"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim dealerStallEquipment as DealerStallEquipment = nothing
			while dr.Read
			
				dealerStallEquipment = me.CreateObject(dr)
			            
			end while        					
			
			return dealerStallEquipment
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim dealerStallEquipmentList as ArrayList = new ArrayList
			
			while dr.Read
					dim dealerStallEquipment as DealerStallEquipment = me.CreateObject(dr)
					dealerStallEquipmentList.Add(dealerStallEquipment)
			end while
			     
			return dealerStallEquipmentList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim dealerStallEquipment as DealerStallEquipment = ctype(obj, DealerStallEquipment)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,dealerStallEquipment.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim dealerStallEquipment as DealerStallEquipment = ctype(obj, DealerStallEquipment)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@DealerID",DbType.Int16,dealerStallEquipment.DealerID)
			DbCommandWrapper.AddInParameter("@StallEquipment",DbType.Int32,dealerStallEquipment.StallEquipment)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,dealerStallEquipment.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,dealerStallEquipment.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerStallEquipment.Dealer))

						
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
		
			dim dealerStallEquipment as DealerStallEquipment = ctype(obj, DealerStallEquipment)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,dealerStallEquipment.ID)
			DbCommandWrapper.AddInParameter("@StallEquipment",DbType.Int32,dealerStallEquipment.StallEquipment)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,dealerStallEquipment.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,dealerStallEquipment.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerStallEquipment.Dealer))
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as DealerStallEquipment
		
			dim dealerStallEquipment as DealerStallEquipment = new DealerStallEquipment
			
			dealerStallEquipment.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DealerID")) then dealerStallEquipment.DealerID = ctype(dr("DealerID"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("StallEquipment")) then dealerStallEquipment.StallEquipment = ctype(dr("StallEquipment"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then dealerStallEquipment.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then dealerStallEquipment.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then dealerStallEquipment.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then dealerStallEquipment.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then dealerStallEquipment.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerStallEquipment.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
			return dealerStallEquipment
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (DealerStallEquipment) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(DealerStallEquipment),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(DealerStallEquipment).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

