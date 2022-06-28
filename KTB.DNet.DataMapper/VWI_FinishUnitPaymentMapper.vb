#region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : VWI_FinishUnitPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2021 - 10:42:30 AM
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

	public class VWI_FinishUnitPaymentMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertVWI_FinishUnitPayment"
		private m_UpdateStatement as string = "up_UpdateVWI_FinishUnitPayment"
		private m_RetrieveStatement as string = "up_RetrieveVWI_FinishUnitPayment"
		private m_RetrieveListStatement as string = "up_RetrieveVWI_FinishUnitPaymentList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteVWI_FinishUnitPayment"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim vWI_FinishUnitPayment as VWI_FinishUnitPayment = nothing
			while dr.Read
			
				vWI_FinishUnitPayment = me.CreateObject(dr)
			            
			end while        					
			
			return vWI_FinishUnitPayment
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim vWI_FinishUnitPaymentList as ArrayList = new ArrayList
			
			while dr.Read
					dim vWI_FinishUnitPayment as VWI_FinishUnitPayment = me.CreateObject(dr)
					vWI_FinishUnitPaymentList.Add(vWI_FinishUnitPayment)
			end while
			     
			return vWI_FinishUnitPaymentList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim vWI_FinishUnitPayment as VWI_FinishUnitPayment = ctype(obj, VWI_FinishUnitPayment)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,vWI_FinishUnitPayment.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim vWI_FinishUnitPayment as VWI_FinishUnitPayment = ctype(obj, VWI_FinishUnitPayment)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@FactoringStatus",DbType.Int16,vWI_FinishUnitPayment.FactoringStatus)
			DbCommandWrapper.AddInParameter("@RegPO",DbType.String,vWI_FinishUnitPayment.RegPO)
			DbCommandWrapper.AddInParameter("@SalesOrderNumber",DbType.String,vWI_FinishUnitPayment.SalesOrderNumber)
			DbCommandWrapper.AddInParameter("@GiroNumber",DbType.String,vWI_FinishUnitPayment.GiroNumber)
			DbCommandWrapper.AddInParameter("@RegNumber",DbType.String,vWI_FinishUnitPayment.RegNumber)
			DbCommandWrapper.AddInParameter("@DueDate",DbType.DateTime,vWI_FinishUnitPayment.DueDate)
			DbCommandWrapper.AddInParameter("@EffectiveDate",DbType.DateTime,vWI_FinishUnitPayment.EffectiveDate)
			DbCommandWrapper.AddInParameter("@Amount",DbType.Single,vWI_FinishUnitPayment.Amount)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,vWI_FinishUnitPayment.Status)

						
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
		
			dim vWI_FinishUnitPayment as VWI_FinishUnitPayment = ctype(obj, VWI_FinishUnitPayment)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,vWI_FinishUnitPayment.ID)
			DbCommandWrapper.AddInParameter("@FactoringStatus",DbType.Int16,vWI_FinishUnitPayment.FactoringStatus)
			DbCommandWrapper.AddInParameter("@RegPO",DbType.String,vWI_FinishUnitPayment.RegPO)
			DbCommandWrapper.AddInParameter("@SalesOrderNumber",DbType.String,vWI_FinishUnitPayment.SalesOrderNumber)
			DbCommandWrapper.AddInParameter("@GiroNumber",DbType.String,vWI_FinishUnitPayment.GiroNumber)
			DbCommandWrapper.AddInParameter("@RegNumber",DbType.String,vWI_FinishUnitPayment.RegNumber)
			DbCommandWrapper.AddInParameter("@DueDate",DbType.DateTime,vWI_FinishUnitPayment.DueDate)
			DbCommandWrapper.AddInParameter("@EffectiveDate",DbType.DateTime,vWI_FinishUnitPayment.EffectiveDate)
			DbCommandWrapper.AddInParameter("@Amount",DbType.Single,vWI_FinishUnitPayment.Amount)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,vWI_FinishUnitPayment.Status)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as VWI_FinishUnitPayment
		
			dim vWI_FinishUnitPayment as VWI_FinishUnitPayment = new VWI_FinishUnitPayment
			
            'vWI_FinishUnitPayment.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("FactoringStatus")) then vWI_FinishUnitPayment.FactoringStatus = ctype(dr("FactoringStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("RegPO")) then vWI_FinishUnitPayment.RegPO = dr("RegPO").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SalesOrderNumber")) then vWI_FinishUnitPayment.SalesOrderNumber = dr("SalesOrderNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("GiroNumber")) then vWI_FinishUnitPayment.GiroNumber = dr("GiroNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RegNumber")) then vWI_FinishUnitPayment.RegNumber = dr("RegNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DueDate")) then vWI_FinishUnitPayment.DueDate = ctype(dr("DueDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) then vWI_FinishUnitPayment.EffectiveDate = ctype(dr("EffectiveDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("Amount")) then vWI_FinishUnitPayment.Amount = ctype(dr("Amount"), single) 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then vWI_FinishUnitPayment.Status = ctype(dr("Status"), short) 
			
			return vWI_FinishUnitPayment
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (VWI_FinishUnitPayment) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(VWI_FinishUnitPayment),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(VWI_FinishUnitPayment).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
