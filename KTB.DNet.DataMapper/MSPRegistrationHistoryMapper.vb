
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPRegistrationHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2017 - 1:51:19 PM
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

	public class MSPRegistrationHistoryMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMSPRegistrationHistory"
		private m_UpdateStatement as string = "up_UpdateMSPRegistrationHistory"
		private m_RetrieveStatement as string = "up_RetrieveMSPRegistrationHistory"
		private m_RetrieveListStatement as string = "up_RetrieveMSPRegistrationHistoryList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMSPRegistrationHistory"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim mSPRegistrationHistory as MSPRegistrationHistory = nothing
			while dr.Read
			
				mSPRegistrationHistory = me.CreateObject(dr)
			            
			end while        					
			
			return mSPRegistrationHistory
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim mSPRegistrationHistoryList as ArrayList = new ArrayList
			
			while dr.Read
					dim mSPRegistrationHistory as MSPRegistrationHistory = me.CreateObject(dr)
					mSPRegistrationHistoryList.Add(mSPRegistrationHistory)
			end while
			     
			return mSPRegistrationHistoryList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim mSPRegistrationHistory as MSPRegistrationHistory = ctype(obj, MSPRegistrationHistory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPRegistrationHistory.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim mSPRegistrationHistory as MSPRegistrationHistory = ctype(obj, MSPRegistrationHistory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@MSPRegistrationID", DbType.Int32, mSPRegistrationHistory.MSPRegistration.ID)
            DbCommandWrapper.AddInParameter("@MSPMasterID", DbType.Int32, mSPRegistrationHistory.MSPMaster.ID)
			DbCommandWrapper.AddInParameter("@BenefitMasterHeaderID",DbType.Int32,mSPRegistrationHistory.BenefitMasterHeaderID)
			DbCommandWrapper.AddInParameter("@RegistrationDate",DbType.DateTime,mSPRegistrationHistory.RegistrationDate)
			DbCommandWrapper.AddInParameter("@RequestType",DbType.AnsiString,mSPRegistrationHistory.RequestType)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,mSPRegistrationHistory.Status)
			DbCommandWrapper.AddInParameter("@PrintedBy",DbType.AnsiString,mSPRegistrationHistory.PrintedBy)
			DbCommandWrapper.AddInParameter("@PrintedDate",DbType.DateTime,mSPRegistrationHistory.PrintedDate)
            DbCommandWrapper.AddInParameter("@SFDate", DbType.DateTime, mSPRegistrationHistory.SFDate)
            DbCommandWrapper.AddInParameter("@SoldBy", DbType.AnsiString, mSPRegistrationHistory.SoldBy)
            DbCommandWrapper.AddInParameter("@IsTransferToSF", DbType.Boolean, mSPRegistrationHistory.IsTransferToSF)
            DbCommandWrapper.AddInParameter("@IsDownloadCertificate", DbType.Boolean, mSPRegistrationHistory.IsDownloadCertificate)
            DbCommandWrapper.AddInParameter("@DebitChargeNo", DbType.AnsiString, mSPRegistrationHistory.DebitChargeNo)
            DbCommandWrapper.AddInParameter("@SelisihAmount", DbType.Decimal, mSPRegistrationHistory.SelisihAmount)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int16, mSPRegistrationHistory.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPRegistrationHistory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,mSPRegistrationHistory.LastUpdateBy)
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
		
			dim mSPRegistrationHistory as MSPRegistrationHistory = ctype(obj, MSPRegistrationHistory)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPRegistrationHistory.ID)
            DbCommandWrapper.AddInParameter("@MSPRegistrationID", DbType.Int32, mSPRegistrationHistory.MSPRegistration.ID)
            DbCommandWrapper.AddInParameter("@MSPMasterID", DbType.Int32, mSPRegistrationHistory.MSPMaster.ID)
			DbCommandWrapper.AddInParameter("@BenefitMasterHeaderID",DbType.Int32,mSPRegistrationHistory.BenefitMasterHeaderID)
			DbCommandWrapper.AddInParameter("@RegistrationDate",DbType.DateTime,mSPRegistrationHistory.RegistrationDate)
			DbCommandWrapper.AddInParameter("@RequestType",DbType.AnsiString,mSPRegistrationHistory.RequestType)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,mSPRegistrationHistory.Status)
			DbCommandWrapper.AddInParameter("@PrintedBy",DbType.AnsiString,mSPRegistrationHistory.PrintedBy)
			DbCommandWrapper.AddInParameter("@PrintedDate",DbType.DateTime,mSPRegistrationHistory.PrintedDate)
            DbCommandWrapper.AddInParameter("@SFDate", DbType.DateTime, mSPRegistrationHistory.SFDate)
            DbCommandWrapper.AddInParameter("@SoldBy", DbType.AnsiString, mSPRegistrationHistory.SoldBy)
            DbCommandWrapper.AddInParameter("@IsTransferToSF", DbType.Boolean, mSPRegistrationHistory.IsTransferToSF)
            DbCommandWrapper.AddInParameter("@IsDownloadCertificate", DbType.Boolean, mSPRegistrationHistory.IsDownloadCertificate)
            DbCommandWrapper.AddInParameter("@DebitChargeNo", DbType.AnsiString, mSPRegistrationHistory.DebitChargeNo)
            DbCommandWrapper.AddInParameter("@SelisihAmount", DbType.Decimal, mSPRegistrationHistory.SelisihAmount)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int16, mSPRegistrationHistory.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPRegistrationHistory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,mSPRegistrationHistory.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MSPRegistrationHistory
		
			dim mSPRegistrationHistory as MSPRegistrationHistory = new MSPRegistrationHistory
			
			mSPRegistrationHistory.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("MSPMasterID")) Then
                mSPRegistrationHistory.MSPMaster = New MSPMaster(CType(dr("MSPMasterID"), Integer))
            End If
			if not dr.IsDBNull(dr.GetOrdinal("BenefitMasterHeaderID")) then mSPRegistrationHistory.BenefitMasterHeaderID = ctype(dr("BenefitMasterHeaderID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("RegistrationDate")) then mSPRegistrationHistory.RegistrationDate = ctype(dr("RegistrationDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("RequestType")) then mSPRegistrationHistory.RequestType = dr("RequestType").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then mSPRegistrationHistory.Status = ctype(dr("Status"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("PrintedBy")) then mSPRegistrationHistory.PrintedBy = dr("PrintedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("PrintedDate")) then mSPRegistrationHistory.PrintedDate = ctype(dr("PrintedDate"), DateTime) 
            If Not dr.IsDBNull(dr.GetOrdinal("SFDate")) Then mSPRegistrationHistory.SFDate = CType(dr("SFDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldBy")) Then mSPRegistrationHistory.SoldBy = dr("SoldBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransferToSF")) Then mSPRegistrationHistory.IsTransferToSF = CType(dr("IsTransferToSF"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("IsDownloadCertificate")) Then mSPRegistrationHistory.IsDownloadCertificate = CType(dr("IsDownloadCertificate"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitChargeNo")) Then mSPRegistrationHistory.DebitChargeNo = dr("DebitChargeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SelisihAmount")) Then mSPRegistrationHistory.SelisihAmount = CType(dr("SelisihAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then mSPRegistrationHistory.Sequence = CType(dr("Sequence"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mSPRegistrationHistory.RowStatus = CType(dr("RowStatus"), Short)
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then mSPRegistrationHistory.CreatedBy = dr("CreatedBy").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mSPRegistrationHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then mSPRegistrationHistory.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then mSPRegistrationHistory.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("MSPRegistrationID")) Then
                mSPRegistrationHistory.MSPRegistration = New MSPRegistration(CType(dr("MSPRegistrationID"), Integer))
            End If

			return mSPRegistrationHistory
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MSPRegistrationHistory) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MSPRegistrationHistory),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MSPRegistrationHistory).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

