#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 2:21:36 PM
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

	public class PQRHeaderMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertPQRHeader"
		private m_UpdateStatement as string = "up_UpdatePQRHeader"
		private m_RetrieveStatement as string = "up_RetrievePQRHeader"
		private m_RetrieveListStatement as string = "up_RetrievePQRHeaderList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeletePQRHeader"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim pQRHeader as PQRHeader = nothing
			while dr.Read
			
				pQRHeader = me.CreateObject(dr)
			            
			end while        					
			
			return pQRHeader
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim pQRHeaderList as ArrayList = new ArrayList
			
			while dr.Read
					dim pQRHeader as PQRHeader = me.CreateObject(dr)
					pQRHeaderList.Add(pQRHeader)
			end while
			     
			return pQRHeaderList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim pQRHeader as PQRHeader = ctype(obj, PQRHeader)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pQRHeader.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim pQRHeader as PQRHeader = ctype(obj, PQRHeader)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,0)
			DbCommandWrapper.AddInParameter("@PQRNo",DbType.AnsiString,pQRHeader.PQRNo)
			DbCommandWrapper.AddInParameter("@RefPQRNo",DbType.AnsiString,pQRHeader.RefPQRNo)
			DbCommandWrapper.AddInParameter("@DealerID",DbType.AnsiString,pQRHeader.DealerID)
			DbCommandWrapper.AddInParameter("@Year",DbType.DateTime,pQRHeader.Year)
			DbCommandWrapper.AddInParameter("@SeqNo",DbType.Int32,pQRHeader.SeqNo)
			DbCommandWrapper.AddInParameter("@CategoryID",DbType.AnsiString,pQRHeader.CategoryID)
			DbCommandWrapper.AddInParameter("@DocumentDate",DbType.DateTime,pQRHeader.DocumentDate)
			DbCommandWrapper.AddInParameter("@SoldDate",DbType.DateTime,pQRHeader.SoldDate)
			DbCommandWrapper.AddInParameter("@ChassisMasterID",DbType.AnsiString,pQRHeader.ChassisMasterID)
			DbCommandWrapper.AddInParameter("@PQRDate",DbType.DateTime,pQRHeader.PQRDate)
			DbCommandWrapper.AddInParameter("@OdoMeter",DbType.Int32,pQRHeader.OdoMeter)
			DbCommandWrapper.AddInParameter("@Velocity",DbType.Int32,pQRHeader.Velocity)
			DbCommandWrapper.AddInParameter("@CustomerName",DbType.AnsiString,pQRHeader.CustomerName)
			DbCommandWrapper.AddInParameter("@CustomerAddress",DbType.AnsiString,pQRHeader.CustomerAddress)
			DbCommandWrapper.AddInParameter("@ValidationTime",DbType.DateTime,pQRHeader.ValidationTime)
			DbCommandWrapper.AddInParameter("@ConfirmBy",DbType.AnsiString,pQRHeader.ConfirmBy)
			DbCommandWrapper.AddInParameter("@ConfirmTime",DbType.DateTime,pQRHeader.ConfirmTime)
			DbCommandWrapper.AddInParameter("@RealeseTime",DbType.DateTime,pQRHeader.RealeseTime)
			DbCommandWrapper.AddInParameter("@IntervalProcess",DbType.DateTime,pQRHeader.IntervalProcess)
			DbCommandWrapper.AddInParameter("@Complexity",DbType.Int16,pQRHeader.Complexity)
			DbCommandWrapper.AddInParameter("@Subject",DbType.AnsiString,pQRHeader.Subject)
			DbCommandWrapper.AddInParameter("@Symptomps",DbType.AnsiString,pQRHeader.Symptomps)
			DbCommandWrapper.AddInParameter("@Causes",DbType.AnsiString,pQRHeader.Causes)
			DbCommandWrapper.AddInParameter("@Results",DbType.AnsiString,pQRHeader.Results)
			DbCommandWrapper.AddInParameter("@Notes",DbType.AnsiString,pQRHeader.Notes)
			DbCommandWrapper.AddInParameter("@Solutions",DbType.AnsiString,pQRHeader.Solutions)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pQRHeader.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,pQRHeader.LastUpdateBy)
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
		
			dim pQRHeader as PQRHeader = ctype(obj, PQRHeader)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pQRHeader.ID)
			DbCommandWrapper.AddInParameter("@PQRNo",DbType.AnsiString,pQRHeader.PQRNo)
			DbCommandWrapper.AddInParameter("@RefPQRNo",DbType.AnsiString,pQRHeader.RefPQRNo)
			DbCommandWrapper.AddInParameter("@DealerID",DbType.AnsiString,pQRHeader.DealerID)
			DbCommandWrapper.AddInParameter("@Year",DbType.DateTime,pQRHeader.Year)
			DbCommandWrapper.AddInParameter("@SeqNo",DbType.Int32,pQRHeader.SeqNo)
			DbCommandWrapper.AddInParameter("@CategoryID",DbType.AnsiString,pQRHeader.CategoryID)
			DbCommandWrapper.AddInParameter("@DocumentDate",DbType.DateTime,pQRHeader.DocumentDate)
			DbCommandWrapper.AddInParameter("@SoldDate",DbType.DateTime,pQRHeader.SoldDate)
			DbCommandWrapper.AddInParameter("@ChassisMasterID",DbType.AnsiString,pQRHeader.ChassisMasterID)
			DbCommandWrapper.AddInParameter("@PQRDate",DbType.DateTime,pQRHeader.PQRDate)
			DbCommandWrapper.AddInParameter("@OdoMeter",DbType.Int32,pQRHeader.OdoMeter)
			DbCommandWrapper.AddInParameter("@Velocity",DbType.Int32,pQRHeader.Velocity)
			DbCommandWrapper.AddInParameter("@CustomerName",DbType.AnsiString,pQRHeader.CustomerName)
			DbCommandWrapper.AddInParameter("@CustomerAddress",DbType.AnsiString,pQRHeader.CustomerAddress)
			DbCommandWrapper.AddInParameter("@ValidationTime",DbType.DateTime,pQRHeader.ValidationTime)
			DbCommandWrapper.AddInParameter("@ConfirmBy",DbType.AnsiString,pQRHeader.ConfirmBy)
			DbCommandWrapper.AddInParameter("@ConfirmTime",DbType.DateTime,pQRHeader.ConfirmTime)
			DbCommandWrapper.AddInParameter("@RealeseTime",DbType.DateTime,pQRHeader.RealeseTime)
			DbCommandWrapper.AddInParameter("@IntervalProcess",DbType.DateTime,pQRHeader.IntervalProcess)
			DbCommandWrapper.AddInParameter("@Complexity",DbType.Int16,pQRHeader.Complexity)
			DbCommandWrapper.AddInParameter("@Subject",DbType.AnsiString,pQRHeader.Subject)
			DbCommandWrapper.AddInParameter("@Symptomps",DbType.AnsiString,pQRHeader.Symptomps)
			DbCommandWrapper.AddInParameter("@Causes",DbType.AnsiString,pQRHeader.Causes)
			DbCommandWrapper.AddInParameter("@Results",DbType.AnsiString,pQRHeader.Results)
			DbCommandWrapper.AddInParameter("@Notes",DbType.AnsiString,pQRHeader.Notes)
			DbCommandWrapper.AddInParameter("@Solutions",DbType.AnsiString,pQRHeader.Solutions)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pQRHeader.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,pQRHeader.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as PQRHeader
		
			dim pQRHeader as PQRHeader = new PQRHeader
			
			pQRHeader.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("PQRNo")) then pQRHeader.PQRNo = dr("PQRNo").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RefPQRNo")) then pQRHeader.RefPQRNo = dr("RefPQRNo").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DealerID")) then pQRHeader.DealerID = dr("DealerID").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Year")) then pQRHeader.Year = ctype(dr("Year"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("SeqNo")) then pQRHeader.SeqNo = ctype(dr("SeqNo"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("CategoryID")) then pQRHeader.CategoryID = dr("CategoryID").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DocumentDate")) then pQRHeader.DocumentDate = ctype(dr("DocumentDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("SoldDate")) then pQRHeader.SoldDate = ctype(dr("SoldDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) then pQRHeader.ChassisMasterID = dr("ChassisMasterID").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("PQRDate")) then pQRHeader.PQRDate = ctype(dr("PQRDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("OdoMeter")) then pQRHeader.OdoMeter = ctype(dr("OdoMeter"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Velocity")) then pQRHeader.Velocity = ctype(dr("Velocity"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("CustomerName")) then pQRHeader.CustomerName = dr("CustomerName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CustomerAddress")) then pQRHeader.CustomerAddress = dr("CustomerAddress").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ValidationTime")) then pQRHeader.ValidationTime = ctype(dr("ValidationTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("ConfirmBy")) then pQRHeader.ConfirmBy = dr("ConfirmBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ConfirmTime")) then pQRHeader.ConfirmTime = ctype(dr("ConfirmTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("RealeseTime")) then pQRHeader.RealeseTime = ctype(dr("RealeseTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("IntervalProcess")) then pQRHeader.IntervalProcess = ctype(dr("IntervalProcess"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("Complexity")) then pQRHeader.Complexity = ctype(dr("Complexity"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("Subject")) then pQRHeader.Subject = dr("Subject").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Symptomps")) then pQRHeader.Symptomps = dr("Symptomps").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Causes")) then pQRHeader.Causes = dr("Causes").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Results")) then pQRHeader.Results = dr("Results").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Notes")) then pQRHeader.Notes = dr("Notes").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Solutions")) then pQRHeader.Solutions = dr("Solutions").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then pQRHeader.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then pQRHeader.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then pQRHeader.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then pQRHeader.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then pQRHeader.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return pQRHeader
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (PQRHeader) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(PQRHeader),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(PQRHeader).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

