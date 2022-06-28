#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : OCRFamilyCard Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 7/21/2021 - 11:08:49 AM
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

	public class OCRFamilyCardMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertOCRFamilyCard"
		private m_UpdateStatement as string = "up_UpdateOCRFamilyCard"
		private m_RetrieveStatement as string = "up_RetrieveOCRFamilyCard"
		private m_RetrieveListStatement as string = "up_RetrieveOCRFamilyCardList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteOCRFamilyCard"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim oCRFamilyCard as OCRFamilyCard = nothing
			while dr.Read
			
				oCRFamilyCard = me.CreateObject(dr)
			            
			end while        					
			
			return oCRFamilyCard
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim oCRFamilyCardList as ArrayList = new ArrayList
			
			while dr.Read
					dim oCRFamilyCard as OCRFamilyCard = me.CreateObject(dr)
					oCRFamilyCardList.Add(oCRFamilyCard)
			end while
			     
			return oCRFamilyCardList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim oCRFamilyCard as OCRFamilyCard = ctype(obj, OCRFamilyCard)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,oCRFamilyCard.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim oCRFamilyCard as OCRFamilyCard = ctype(obj, OCRFamilyCard)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)

			DbCommandWrapper.AddInParameter("@Type",DbType.Int16,oCRFamilyCard.Type)
			DbCommandWrapper.AddInParameter("@ImageID",DbType.AnsiString,oCRFamilyCard.ImageID)
			DbCommandWrapper.AddInParameter("@ImagePath",DbType.AnsiString,oCRFamilyCard.ImagePath)
			DbCommandWrapper.AddInParameter("@FCRowNo",DbType.Int32,oCRFamilyCard.FCRowNo)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,oCRFamilyCard.Name)
			DbCommandWrapper.AddInParameter("@IdentityNumber",DbType.Int32,oCRFamilyCard.IdentityNumber)
			DbCommandWrapper.AddInParameter("@Gender",DbType.AnsiString,oCRFamilyCard.Gender)
			DbCommandWrapper.AddInParameter("@PlaceOfBirth",DbType.AnsiString,oCRFamilyCard.PlaceOfBirth)
			DbCommandWrapper.AddInParameter("@DateOfBirth",DbType.Date,oCRFamilyCard.DateOfBirth)
			DbCommandWrapper.AddInParameter("@Religion",DbType.AnsiString,oCRFamilyCard.Religion)
			DbCommandWrapper.AddInParameter("@Education",DbType.AnsiString,oCRFamilyCard.Education)
			DbCommandWrapper.AddInParameter("@Occupation",DbType.AnsiString,oCRFamilyCard.Occupation)
			DbCommandWrapper.AddInParameter("@BloodType",DbType.AnsiString,oCRFamilyCard.BloodType)
			DbCommandWrapper.AddInParameter("@TotalChars",DbType.Int32,oCRFamilyCard.TotalChars)
			DbCommandWrapper.AddInParameter("@ConfidenceChars",DbType.Int32,oCRFamilyCard.ConfidenceChars)
			DbCommandWrapper.AddInParameter("@ProcessingTime",DbType.Double,oCRFamilyCard.ProcessingTime)
			DbCommandWrapper.AddInParameter("@Errors",DbType.AnsiString,oCRFamilyCard.Errors)
			DbCommandWrapper.AddInParameter("@JSon",DbType.AnsiString,oCRFamilyCard.JSon)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,oCRFamilyCard.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdatedBy",DbType.AnsiString,oCRFamilyCard.LastUpdatedBy)
			DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,oCRFamilyCard.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, Me.GetRefObject(oCRFamilyCard.SPKHeaderID))
						
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
		
			dim oCRFamilyCard as OCRFamilyCard = ctype(obj, OCRFamilyCard)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,oCRFamilyCard.ID)

			DbCommandWrapper.AddInParameter("@Type",DbType.Int16,oCRFamilyCard.Type)
			DbCommandWrapper.AddInParameter("@ImageID",DbType.AnsiString,oCRFamilyCard.ImageID)
			DbCommandWrapper.AddInParameter("@ImagePath",DbType.AnsiString,oCRFamilyCard.ImagePath)
			DbCommandWrapper.AddInParameter("@FCRowNo",DbType.Int32,oCRFamilyCard.FCRowNo)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,oCRFamilyCard.Name)
			DbCommandWrapper.AddInParameter("@IdentityNumber",DbType.Int32,oCRFamilyCard.IdentityNumber)
			DbCommandWrapper.AddInParameter("@Gender",DbType.AnsiString,oCRFamilyCard.Gender)
			DbCommandWrapper.AddInParameter("@PlaceOfBirth",DbType.AnsiString,oCRFamilyCard.PlaceOfBirth)
			DbCommandWrapper.AddInParameter("@DateOfBirth",DbType.Date,oCRFamilyCard.DateOfBirth)
			DbCommandWrapper.AddInParameter("@Religion",DbType.AnsiString,oCRFamilyCard.Religion)
			DbCommandWrapper.AddInParameter("@Education",DbType.AnsiString,oCRFamilyCard.Education)
			DbCommandWrapper.AddInParameter("@Occupation",DbType.AnsiString,oCRFamilyCard.Occupation)
			DbCommandWrapper.AddInParameter("@BloodType",DbType.AnsiString,oCRFamilyCard.BloodType)
			DbCommandWrapper.AddInParameter("@TotalChars",DbType.Int32,oCRFamilyCard.TotalChars)
			DbCommandWrapper.AddInParameter("@ConfidenceChars",DbType.Int32,oCRFamilyCard.ConfidenceChars)
			DbCommandWrapper.AddInParameter("@ProcessingTime",DbType.Double,oCRFamilyCard.ProcessingTime)
			DbCommandWrapper.AddInParameter("@Errors",DbType.AnsiString,oCRFamilyCard.Errors)
			DbCommandWrapper.AddInParameter("@JSon",DbType.AnsiString,oCRFamilyCard.JSon)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,oCRFamilyCard.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,oCRFamilyCard.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdatedBy",DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, oCRFamilyCard.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@SPKHeaderD", DbType.Int32, Me.GetRefObject(oCRFamilyCard.SPKHeaderID))
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as OCRFamilyCard
		
			dim oCRFamilyCard as OCRFamilyCard = new OCRFamilyCard
			
			oCRFamilyCard.ID = ctype(dr("ID"), integer) 

			if not dr.IsDBNull(dr.GetOrdinal("Type")) then oCRFamilyCard.Type = ctype(dr("Type"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("ImageID")) then oCRFamilyCard.ImageID = dr("ImageID").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ImagePath")) then oCRFamilyCard.ImagePath = dr("ImagePath").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("FCRowNo")) then oCRFamilyCard.FCRowNo = ctype(dr("FCRowNo"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Name")) then oCRFamilyCard.Name = dr("Name").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("IdentityNumber")) then oCRFamilyCard.IdentityNumber = ctype(dr("IdentityNumber"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Gender")) then oCRFamilyCard.Gender = dr("Gender").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("PlaceOfBirth")) then oCRFamilyCard.PlaceOfBirth = dr("PlaceOfBirth").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DateOfBirth")) then oCRFamilyCard.DateOfBirth = ctype(dr("DateOfBirth"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("Religion")) then oCRFamilyCard.Religion = dr("Religion").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Education")) then oCRFamilyCard.Education = dr("Education").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Occupation")) then oCRFamilyCard.Occupation = dr("Occupation").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("BloodType")) then oCRFamilyCard.BloodType = dr("BloodType").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("TotalChars")) then oCRFamilyCard.TotalChars = ctype(dr("TotalChars"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("ConfidenceChars")) then oCRFamilyCard.ConfidenceChars = ctype(dr("ConfidenceChars"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("ProcessingTime")) then oCRFamilyCard.ProcessingTime = ctype(dr("ProcessingTime"), double) 
			if not dr.IsDBNull(dr.GetOrdinal("Errors")) then oCRFamilyCard.Errors = dr("Errors").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("JSon")) then oCRFamilyCard.JSon = dr("JSon").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then oCRFamilyCard.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then oCRFamilyCard.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then oCRFamilyCard.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) then oCRFamilyCard.LastUpdatedBy = dr("LastUpdatedBy").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then oCRFamilyCard.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("SPKHeaderID")) Then
                oCRFamilyCard.SPKHeaderID = New SPKHeader(CType(dr("SPKHeaderID"), Integer))
            End If
			
			return oCRFamilyCard
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (OCRFamilyCard) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(OCRFamilyCard),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(OCRFamilyCard).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
