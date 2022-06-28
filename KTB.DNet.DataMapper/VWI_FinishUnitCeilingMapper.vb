#region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : VWI_FinishUnitCeiling Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2021 - 4:26:18 PM
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

	public class VWI_FinishUnitCeilingMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertVWI_FinishUnitCeiling"
		private m_UpdateStatement as string = "up_UpdateVWI_FinishUnitCeiling"
		private m_RetrieveStatement as string = "up_RetrieveVWI_FinishUnitCeiling"
		private m_RetrieveListStatement as string = "up_RetrieveVWI_FinishUnitCeilingList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteVWI_FinishUnitCeiling"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim vWI_FinishUnitCeiling as VWI_FinishUnitCeiling = nothing
			while dr.Read
			
				vWI_FinishUnitCeiling = me.CreateObject(dr)
			            
			end while        					
			
			return vWI_FinishUnitCeiling
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim vWI_FinishUnitCeilingList as ArrayList = new ArrayList
			
			while dr.Read
					dim vWI_FinishUnitCeiling as VWI_FinishUnitCeiling = me.CreateObject(dr)
					vWI_FinishUnitCeilingList.Add(vWI_FinishUnitCeiling)
			end while
			     
			return vWI_FinishUnitCeilingList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim vWI_FinishUnitCeiling as VWI_FinishUnitCeiling = ctype(obj, VWI_FinishUnitCeiling)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,vWI_FinishUnitCeiling.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim vWI_FinishUnitCeiling as VWI_FinishUnitCeiling = ctype(obj, VWI_FinishUnitCeiling)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@CreditAccount",DbType.String,vWI_FinishUnitCeiling.CreditAccount)
			DbCommandWrapper.AddInParameter("@DealerName",DbType.String,vWI_FinishUnitCeiling.DealerName)
            'DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.String, vWI_FinishUnitCeiling.ProductCategoryID)
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, vWI_FinishUnitCeiling.ProductCategoryID)
			DbCommandWrapper.AddInParameter("@StandardCeiling",DbType.Single,vWI_FinishUnitCeiling.StandardCeiling)
			DbCommandWrapper.AddInParameter("@FactoringCeiling",DbType.Single,vWI_FinishUnitCeiling.FactoringCeiling)
			DbCommandWrapper.AddInParameter("@Outstanding",DbType.Single,vWI_FinishUnitCeiling.Outstanding)
			DbCommandWrapper.AddInParameter("@AvailableCeiling",DbType.Single,vWI_FinishUnitCeiling.AvailableCeiling)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,vWI_FinishUnitCeiling.Status)
			DbCommandWrapper.AddInParameter("@MaxTOPDate",DbType.DateTime,vWI_FinishUnitCeiling.MaxTOPDate)

						
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
		
			dim vWI_FinishUnitCeiling as VWI_FinishUnitCeiling = ctype(obj, VWI_FinishUnitCeiling)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,vWI_FinishUnitCeiling.ID)
			DbCommandWrapper.AddInParameter("@CreditAccount",DbType.String,vWI_FinishUnitCeiling.CreditAccount)
			DbCommandWrapper.AddInParameter("@DealerName",DbType.String,vWI_FinishUnitCeiling.DealerName)
            'DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.String, vWI_FinishUnitCeiling.ProductCategoryID)
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, vWI_FinishUnitCeiling.ProductCategoryID)
			DbCommandWrapper.AddInParameter("@StandardCeiling",DbType.Single,vWI_FinishUnitCeiling.StandardCeiling)
			DbCommandWrapper.AddInParameter("@FactoringCeiling",DbType.Single,vWI_FinishUnitCeiling.FactoringCeiling)
			DbCommandWrapper.AddInParameter("@Outstanding",DbType.Single,vWI_FinishUnitCeiling.Outstanding)
			DbCommandWrapper.AddInParameter("@AvailableCeiling",DbType.Single,vWI_FinishUnitCeiling.AvailableCeiling)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,vWI_FinishUnitCeiling.Status)
			DbCommandWrapper.AddInParameter("@MaxTOPDate",DbType.DateTime,vWI_FinishUnitCeiling.MaxTOPDate)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as VWI_FinishUnitCeiling
		
			dim vWI_FinishUnitCeiling as VWI_FinishUnitCeiling = new VWI_FinishUnitCeiling
			
            'vWI_FinishUnitCeiling.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) then vWI_FinishUnitCeiling.CreditAccount = dr("CreditAccount").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DealerName")) then vWI_FinishUnitCeiling.DealerName = dr("DealerName").ToString 
            'If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then vWI_FinishUnitCeiling.ProductCategoryID = dr("ProductCategoryID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then vWI_FinishUnitCeiling.ProductCategoryID = CType(dr("ProductCategoryID"), Short)
			if not dr.IsDBNull(dr.GetOrdinal("StandardCeiling")) then vWI_FinishUnitCeiling.StandardCeiling = ctype(dr("StandardCeiling"), single) 
			if not dr.IsDBNull(dr.GetOrdinal("FactoringCeiling")) then vWI_FinishUnitCeiling.FactoringCeiling = ctype(dr("FactoringCeiling"), single) 
			if not dr.IsDBNull(dr.GetOrdinal("Outstanding")) then vWI_FinishUnitCeiling.Outstanding = ctype(dr("Outstanding"), single) 
			if not dr.IsDBNull(dr.GetOrdinal("AvailableCeiling")) then vWI_FinishUnitCeiling.AvailableCeiling = ctype(dr("AvailableCeiling"), single) 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then vWI_FinishUnitCeiling.Status = ctype(dr("Status"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("MaxTOPDate")) then vWI_FinishUnitCeiling.MaxTOPDate = ctype(dr("MaxTOPDate"), DateTime) 
			
			return vWI_FinishUnitCeiling
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (VWI_FinishUnitCeiling) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(VWI_FinishUnitCeiling),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(VWI_FinishUnitCeiling).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
