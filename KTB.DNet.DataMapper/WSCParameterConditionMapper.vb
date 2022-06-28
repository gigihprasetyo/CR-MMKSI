
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCParameterCondition Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/05/2020 - 14:01:56
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

	public class WSCParameterConditionMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertWSCParameterCondition"
		private m_UpdateStatement as string = "up_UpdateWSCParameterCondition"
		private m_RetrieveStatement as string = "up_RetrieveWSCParameterCondition"
		private m_RetrieveListStatement as string = "up_RetrieveWSCParameterConditionList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteWSCParameterCondition"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim wSCParameterCondition as WSCParameterCondition = nothing
			while dr.Read
			
				wSCParameterCondition = me.CreateObject(dr)
			            
			end while        					
			
			return wSCParameterCondition
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim wSCParameterConditionList as ArrayList = new ArrayList
			
			while dr.Read
					dim wSCParameterCondition as WSCParameterCondition = me.CreateObject(dr)
					wSCParameterConditionList.Add(wSCParameterCondition)
			end while
			     
			return wSCParameterConditionList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim wSCParameterCondition as WSCParameterCondition = ctype(obj, WSCParameterCondition)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int64,wSCParameterCondition.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim wSCParameterCondition as WSCParameterCondition = ctype(obj, WSCParameterCondition)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int64,8)
            'DbCommandWrapper.AddInParameter("@WSCParameterHeaderID",DbType.Int32,wSCParameterCondition.WSCParameterHeaderID)
            DbCommandWrapper.AddInParameter("@WSCParameterHeaderID", DbType.Int32, Me.GetRefObject(wSCParameterCondition.WSCParameterHeader))
			DbCommandWrapper.AddInParameter("@WSCParameterConditionID",DbType.Int32,wSCParameterCondition.WSCParameterConditionID)
			DbCommandWrapper.AddInParameter("@Kind",DbType.Int32,wSCParameterCondition.Kind)
            DbCommandWrapper.AddInParameter("@Operator", DbType.Int32, wSCParameterCondition.Operators)
			DbCommandWrapper.AddInParameter("@Value",DbType.AnsiString,wSCParameterCondition.Value)
			DbCommandWrapper.AddInParameter("@Functions",DbType.Int32,wSCParameterCondition.Functions)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,wSCParameterCondition.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,wSCParameterCondition.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

						
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
		
			dim wSCParameterCondition as WSCParameterCondition = ctype(obj, WSCParameterCondition)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int64,wSCParameterCondition.ID)
            'DbCommandWrapper.AddInParameter("@WSCParameterHeaderID",DbType.Int32,wSCParameterCondition.WSCParameterHeaderID)
            DbCommandWrapper.AddInParameter("@WSCParameterHeaderID", DbType.Int32, Me.GetRefObject(wSCParameterCondition.WSCParameterHeader))
			DbCommandWrapper.AddInParameter("@WSCParameterConditionID",DbType.Int32,wSCParameterCondition.WSCParameterConditionID)
			DbCommandWrapper.AddInParameter("@Kind",DbType.Int32,wSCParameterCondition.Kind)
            DbCommandWrapper.AddInParameter("@Operator", DbType.Int32, wSCParameterCondition.Operators)
			DbCommandWrapper.AddInParameter("@Value",DbType.AnsiString,wSCParameterCondition.Value)
			DbCommandWrapper.AddInParameter("@Functions",DbType.Int32,wSCParameterCondition.Functions)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,wSCParameterCondition.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,wSCParameterCondition.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as WSCParameterCondition
		
			dim wSCParameterCondition as WSCParameterCondition = new WSCParameterCondition
			
			wSCParameterCondition.ID = ctype(dr("ID"), long) 
            'If Not dr.IsDBNull(dr.GetOrdinal("WSCParameterHeaderID")) Then wSCParameterCondition.WSCParameterHeaderID = CType(dr("WSCParameterHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCParameterHeaderID")) Then
                wSCParameterCondition.WSCParameterHeader = New WSCParameterHeader(CType(dr("WSCParameterHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("WSCParameterConditionID")) Then wSCParameterCondition.WSCParameterConditionID = CType(dr("WSCParameterConditionID"), Integer)
			if not dr.IsDBNull(dr.GetOrdinal("Kind")) then wSCParameterCondition.Kind = ctype(dr("Kind"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("Operator")) Then wSCParameterCondition.Operators = CType(dr("Operator"), Integer)
			if not dr.IsDBNull(dr.GetOrdinal("Value")) then wSCParameterCondition.Value = dr("Value").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Functions")) then wSCParameterCondition.Functions = ctype(dr("Functions"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then wSCParameterCondition.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then wSCParameterCondition.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then wSCParameterCondition.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then wSCParameterCondition.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then wSCParameterCondition.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return wSCParameterCondition
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (WSCParameterCondition) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(WSCParameterCondition),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(WSCParameterCondition).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

