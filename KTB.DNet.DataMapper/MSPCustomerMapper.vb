
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2017 - 1:50:40 PM
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

	public class MSPCustomerMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMSPCustomer"
		private m_UpdateStatement as string = "up_UpdateMSPCustomer"
		private m_RetrieveStatement as string = "up_RetrieveMSPCustomer"
		private m_RetrieveListStatement as string = "up_RetrieveMSPCustomerList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMSPCustomer"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim mSPCustomer as MSPCustomer = nothing
			while dr.Read
			
				mSPCustomer = me.CreateObject(dr)
			            
			end while        					
			
			return mSPCustomer
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim mSPCustomerList as ArrayList = new ArrayList
			
			while dr.Read
					dim mSPCustomer as MSPCustomer = me.CreateObject(dr)
					mSPCustomerList.Add(mSPCustomer)
			end while
			     
			return mSPCustomerList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim mSPCustomer as MSPCustomer = ctype(obj, MSPCustomer)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPCustomer.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim mSPCustomer as MSPCustomer = ctype(obj, MSPCustomer)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            If Not IsNothing(mSPCustomer.RefCustomer) Then
                DbCommandWrapper.AddInParameter("@RefCustomerID", DbType.Int32, mSPCustomer.RefCustomer.ID)
            End If

			DbCommandWrapper.AddInParameter("@Name1",DbType.AnsiString,mSPCustomer.Name1)
			DbCommandWrapper.AddInParameter("@Name2",DbType.AnsiString,mSPCustomer.Name2)
			DbCommandWrapper.AddInParameter("@Name3",DbType.AnsiString,mSPCustomer.Name3)
			DbCommandWrapper.AddInParameter("@Alamat",DbType.AnsiString,mSPCustomer.Alamat)
			DbCommandWrapper.AddInParameter("@Kelurahan",DbType.AnsiString,mSPCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, mSPCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, Me.GetRefObject(mSPCustomer.Province))
			DbCommandWrapper.AddInParameter("@PostalCode",DbType.AnsiString,mSPCustomer.PostalCode)
			DbCommandWrapper.AddInParameter("@PreArea",DbType.AnsiString,mSPCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(mSPCustomer.City))
			DbCommandWrapper.AddInParameter("@PrintRegion",DbType.AnsiString,mSPCustomer.PrintRegion)
			DbCommandWrapper.AddInParameter("@PhoneNo",DbType.AnsiString,mSPCustomer.PhoneNo)
			DbCommandWrapper.AddInParameter("@Email",DbType.AnsiString,mSPCustomer.Email)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, mSPCustomer.Attachment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPCustomer.Status)
			DbCommandWrapper.AddInParameter("@DeletionMark",DbType.Int16,mSPCustomer.DeletionMark)
            DbCommandWrapper.AddInParameter("@CompleteName", DbType.AnsiString, mSPCustomer.CompleteName)
            DbCommandWrapper.AddInParameter("@KTPNo", DbType.AnsiString, mSPCustomer.KTPNo)
            DbCommandWrapper.AddInParameter("@Age", DbType.Int32, mSPCustomer.Age)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPCustomer.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,mSPCustomer.LastUpdateBy)
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
		
			dim mSPCustomer as MSPCustomer = ctype(obj, MSPCustomer)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPCustomer.ID)
            If Not IsNothing(mSPCustomer.RefCustomer) Then
                DbCommandWrapper.AddInParameter("@RefCustomerID", DbType.Int32, mSPCustomer.RefCustomer.ID)
            End If

			DbCommandWrapper.AddInParameter("@Name1",DbType.AnsiString,mSPCustomer.Name1)
			DbCommandWrapper.AddInParameter("@Name2",DbType.AnsiString,mSPCustomer.Name2)
			DbCommandWrapper.AddInParameter("@Name3",DbType.AnsiString,mSPCustomer.Name3)
			DbCommandWrapper.AddInParameter("@Alamat",DbType.AnsiString,mSPCustomer.Alamat)
			DbCommandWrapper.AddInParameter("@Kelurahan",DbType.AnsiString,mSPCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, mSPCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, Me.GetRefObject(mSPCustomer.Province))
			DbCommandWrapper.AddInParameter("@PostalCode",DbType.AnsiString,mSPCustomer.PostalCode)
			DbCommandWrapper.AddInParameter("@PreArea",DbType.AnsiString,mSPCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(mSPCustomer.City))

			DbCommandWrapper.AddInParameter("@PrintRegion",DbType.AnsiString,mSPCustomer.PrintRegion)
			DbCommandWrapper.AddInParameter("@PhoneNo",DbType.AnsiString,mSPCustomer.PhoneNo)
			DbCommandWrapper.AddInParameter("@Email",DbType.AnsiString,mSPCustomer.Email)
			DbCommandWrapper.AddInParameter("@Attachment",DbType.AnsiString,mSPCustomer.Attachment)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,mSPCustomer.Status)
			DbCommandWrapper.AddInParameter("@DeletionMark",DbType.Int16,mSPCustomer.DeletionMark)
            DbCommandWrapper.AddInParameter("@CompleteName", DbType.AnsiString, mSPCustomer.CompleteName)
            DbCommandWrapper.AddInParameter("@KTPNo", DbType.AnsiString, mSPCustomer.KTPNo)
            DbCommandWrapper.AddInParameter("@Age", DbType.Int32, mSPCustomer.Age)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPCustomer.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,mSPCustomer.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)	
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MSPCustomer
		
			dim mSPCustomer as MSPCustomer = new MSPCustomer
			
			mSPCustomer.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("RefCustomerID")) Then
                mSPCustomer.RefCustomer = New Customer(CType(dr("RefCustomerID"), Integer))
            End If
			if not dr.IsDBNull(dr.GetOrdinal("Name1")) then mSPCustomer.Name1 = dr("Name1").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Name2")) then mSPCustomer.Name2 = dr("Name2").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Name3")) then mSPCustomer.Name3 = dr("Name3").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Alamat")) then mSPCustomer.Alamat = dr("Alamat").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) then mSPCustomer.Kelurahan = dr("Kelurahan").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then mSPCustomer.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then mSPCustomer.PostalCode = dr("PostalCode").ToString
			if not dr.IsDBNull(dr.GetOrdinal("PreArea")) then mSPCustomer.PreArea = dr("PreArea").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("PrintRegion")) Then mSPCustomer.PrintRegion = dr("PrintRegion").ToString
			if not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) then mSPCustomer.PhoneNo = dr("PhoneNo").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Email")) then mSPCustomer.Email = dr("Email").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Attachment")) then mSPCustomer.Attachment = dr("Attachment").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then mSPCustomer.Status = ctype(dr("Status"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("DeletionMark")) then mSPCustomer.DeletionMark = ctype(dr("DeletionMark"), short) 
            If Not dr.IsDBNull(dr.GetOrdinal("CompleteName")) Then mSPCustomer.CompleteName = dr("CompleteName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KTPNo")) Then mSPCustomer.KTPNo = dr("KTPNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Age")) Then mSPCustomer.Age = dr("Age").ToString
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then mSPCustomer.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then mSPCustomer.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then mSPCustomer.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then mSPCustomer.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then mSPCustomer.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceID")) Then
                mSPCustomer.Province = New Province(CType(dr("ProvinceID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                mSPCustomer.City = New City(CType(dr("CityID"), Short))
            End If

			return mSPCustomer
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MSPCustomer) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MSPCustomer),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MSPCustomer).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

