
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SPKChassis Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 11/23/2011 - 1:18:38 PM
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

	public class V_SPKChassisMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertV_SPKChassis"
		private m_UpdateStatement as string = "up_UpdateV_SPKChassis"
		private m_RetrieveStatement as string = "up_RetrieveV_SPKChassis"
		private m_RetrieveListStatement as string = "up_RetrieveV_SPKChassisList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteV_SPKChassis"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim v_SPKChassis as V_SPKChassis = nothing
			while dr.Read
			
				v_SPKChassis = me.CreateObject(dr)
			            
			end while        					
			
			return v_SPKChassis
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim v_SPKChassisList as ArrayList = new ArrayList
			
			while dr.Read
					dim v_SPKChassis as V_SPKChassis = me.CreateObject(dr)
					v_SPKChassisList.Add(v_SPKChassis)
			end while
			     
			return v_SPKChassisList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim v_SPKChassis as V_SPKChassis = ctype(obj, V_SPKChassis)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@EndCustomerID",DbType.Int32,v_SPKChassis.EndCustomerID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim v_SPKChassis as V_SPKChassis = ctype(obj, V_SPKChassis)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,v_SPKChassis.ID)
			DbCommandWrapper.AddInParameter("@DealerID",DbType.Int16,v_SPKChassis.DealerID)
			DbCommandWrapper.AddInParameter("@Status",DbType.AnsiString,v_SPKChassis.Status)
            DBCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, v_SPKChassis.SPKNumber)
            DBCommandWrapper.AddInParameter("@IndentNumber", DbType.AnsiString, v_SPKChassis.IndentNumber)
			DbCommandWrapper.AddInParameter("@DealerSPKNumber",DbType.AnsiString,v_SPKChassis.DealerSPKNumber)
			DbCommandWrapper.AddInParameter("@PlanDeliveryMonth",DbType.Byte,v_SPKChassis.PlanDeliveryMonth)
			DbCommandWrapper.AddInParameter("@PlanDeliveryYear",DbType.Int16,v_SPKChassis.PlanDeliveryYear)
			DbCommandWrapper.AddInParameter("@PlanDeliveryDate",DbType.DateTime,v_SPKChassis.PlanDeliveryDate)
			DbCommandWrapper.AddInParameter("@PlanInvoiceMonth",DbType.Byte,v_SPKChassis.PlanInvoiceMonth)
			DbCommandWrapper.AddInParameter("@PlanInvoiceYear",DbType.Int16,v_SPKChassis.PlanInvoiceYear)
			DbCommandWrapper.AddInParameter("@PlanInvoiceDate",DbType.DateTime,v_SPKChassis.PlanInvoiceDate)
            DBCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, v_SPKChassis.CustomerRequestID)
            'DbCommandWrapper.AddInParameter("@SPKCustomerID",DbType.Int32,v_SPKChassis.SPKCustomerID)
			DbCommandWrapper.AddInParameter("@ValidateTime",DbType.DateTime,v_SPKChassis.ValidateTime)
			DbCommandWrapper.AddInParameter("@ValidateBy",DbType.String,v_SPKChassis.ValidateBy)
			DbCommandWrapper.AddInParameter("@RejectedReason",DbType.String,v_SPKChassis.RejectedReason)
            'DbCommandWrapper.AddInParameter("@SalesmanHeaderID",DbType.Int16,v_SPKChassis.SalesmanHeaderID)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,v_SPKChassis.RowStatus)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.String,User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.String,v_SPKChassis.LastUpdateBy)
			DbCommandWrapper.AddInParameter("@DealerName",DbType.AnsiString,v_SPKChassis.DealerName)
			DbCommandWrapper.AddInParameter("@DealerCode",DbType.AnsiString,v_SPKChassis.DealerCode)
			DbCommandWrapper.AddInParameter("@ChassisNumber",DbType.AnsiString,v_SPKChassis.ChassisNumber)
			DbCommandWrapper.AddOutParameter("@EndCustomerID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@FakturValidateBy",DbType.AnsiString,v_SPKChassis.FakturValidateBy)
            DBCommandWrapper.AddInParameter("@FakturValidateTime", DbType.DateTime, v_SPKChassis.FakturValidateTime)
            DBCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, v_SPKChassis.FakturNumber)
			DbCommandWrapper.AddInParameter("@ChassisMasterID",DbType.Int32,v_SPKChassis.ChassisMasterID)
			DbCommandWrapper.AddInParameter("@FakturStatus",DbType.AnsiString,v_SPKChassis.FakturStatus)
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(v_SPKChassis.SalesmanHeader))
            DBCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, Me.GetRefObject(v_SPKChassis.SPKCustomer))
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, v_SPKChassis.DealerSPKDate)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@EndCustomerID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "EndCustomerID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPKChassis As V_SPKChassis = CType(obj, V_SPKChassis)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SPKChassis.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SPKChassis.DealerID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_SPKChassis.Status)
            DBCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, v_SPKChassis.SPKNumber)
            DBCommandWrapper.AddInParameter("@IndentNumber", DbType.AnsiString, v_SPKChassis.IndentNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, v_SPKChassis.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@PlanDeliveryMonth", DbType.Byte, v_SPKChassis.PlanDeliveryMonth)
            DbCommandWrapper.AddInParameter("@PlanDeliveryYear", DbType.Int16, v_SPKChassis.PlanDeliveryYear)
            DbCommandWrapper.AddInParameter("@PlanDeliveryDate", DbType.DateTime, v_SPKChassis.PlanDeliveryDate)
            DbCommandWrapper.AddInParameter("@PlanInvoiceMonth", DbType.Byte, v_SPKChassis.PlanInvoiceMonth)
            DbCommandWrapper.AddInParameter("@PlanInvoiceYear", DbType.Int16, v_SPKChassis.PlanInvoiceYear)
            DbCommandWrapper.AddInParameter("@PlanInvoiceDate", DbType.DateTime, v_SPKChassis.PlanInvoiceDate)
            DbCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, v_SPKChassis.CustomerRequestID)
            'DbCommandWrapper.AddInParameter("@SPKCustomerID",DbType.Int32,v_SPKChassis.SPKCustomerID)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, v_SPKChassis.ValidateTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.String, v_SPKChassis.ValidateBy)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.String, v_SPKChassis.RejectedReason)
            'DbCommandWrapper.AddInParameter("@SalesmanHeaderID",DbType.Int16,v_SPKChassis.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_SPKChassis.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, v_SPKChassis.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SPKChassis.DealerName)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SPKChassis.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_SPKChassis.ChassisNumber)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, v_SPKChassis.EndCustomerID)
            DbCommandWrapper.AddInParameter("@FakturValidateBy", DbType.AnsiString, v_SPKChassis.FakturValidateBy)
            DBCommandWrapper.AddInParameter("@FakturValidateTime", DbType.DateTime, v_SPKChassis.FakturValidateTime)
            DBCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, v_SPKChassis.FakturNumber)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_SPKChassis.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_SPKChassis.FakturStatus)
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(v_SPKChassis.SalesmanHeader))
            DBCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, Me.GetRefObject(v_SPKChassis.SPKCustomer))
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, v_SPKChassis.DealerSPKDate)


            Return DbCommandWrapper

        End Function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as V_SPKChassis
		
			dim v_SPKChassis as V_SPKChassis = new V_SPKChassis
			
			if not dr.IsDBNull(dr.GetOrdinal("ID")) then v_SPKChassis.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DealerID")) then v_SPKChassis.DealerID = ctype(dr("DealerID"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then v_SPKChassis.Status = dr("Status").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) then v_SPKChassis.SPKNumber = dr("SPKNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DealerSPKNumber")) then v_SPKChassis.DealerSPKNumber = dr("DealerSPKNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryMonth")) then v_SPKChassis.PlanDeliveryMonth = ctype(dr("PlanDeliveryMonth"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryYear")) then v_SPKChassis.PlanDeliveryYear = ctype(dr("PlanDeliveryYear"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryDate")) then v_SPKChassis.PlanDeliveryDate = ctype(dr("PlanDeliveryDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceMonth")) then v_SPKChassis.PlanInvoiceMonth = ctype(dr("PlanInvoiceMonth"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceYear")) then v_SPKChassis.PlanInvoiceYear = ctype(dr("PlanInvoiceYear"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceDate")) then v_SPKChassis.PlanInvoiceDate = ctype(dr("PlanInvoiceDate"), DateTime) 
            'If Not dr.IsDBNull(dr.GetOrdinal("CustomerRequestID")) Then v_SPKChassis.CustomerRequest = CType(dr("CustomerRequestID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPKCustomerID")) Then v_SPKChassis.SPKCustomerID = CType(dr("SPKCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateTime")) Then v_SPKChassis.ValidateTime = CType(dr("ValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateBy")) Then v_SPKChassis.ValidateBy = dr("ValidateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RejectedReason")) Then v_SPKChassis.RejectedReason = dr("RejectedReason").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then v_SPKChassis.SalesmanHeader = CType(dr("SalesmanHeaderID"), Int32)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_SPKChassis.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_SPKChassis.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_SPKChassis.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_SPKChassis.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_SPKChassis.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_SPKChassis.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SPKChassis.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then v_SPKChassis.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturValidateBy")) Then v_SPKChassis.FakturValidateBy = dr("FakturValidateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturValidateTime")) Then v_SPKChassis.FakturValidateTime = CType(dr("FakturValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturNumber")) Then v_SPKChassis.FakturNumber = dr("FakturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then v_SPKChassis.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then v_SPKChassis.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then v_SPKChassis.EndCustomerID = CType(dr("EndCustomerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKDate")) Then v_SPKChassis.DealerSPKDate = CType(dr("DealerSPKDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                v_SPKChassis.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPKCustomerID")) Then
                v_SPKChassis.SPKCustomer = New SPKCustomer(CType(dr("SPKCustomerID"), Integer))
            End If
            Return v_SPKChassis
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (V_SPKChassis) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(V_SPKChassis),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(V_SPKChassis).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

