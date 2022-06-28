
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FreeServicePartDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/06/2019 - 6:03:25 PM
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

	public class FreeServicePartDetailMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertFreeServicePartDetail"
		private m_UpdateStatement as string = "up_UpdateFreeServicePartDetail"
		private m_RetrieveStatement as string = "up_RetrieveFreeServicePartDetail"
		private m_RetrieveListStatement as string = "up_RetrieveFreeServicePartDetailList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteFreeServicePartDetail"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim freeServicePartDetail as FreeServicePartDetail = nothing
			while dr.Read
			
				freeServicePartDetail = me.CreateObject(dr)
			            
			end while        					
			
			return freeServicePartDetail
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim freeServicePartDetailList as ArrayList = new ArrayList
			
			while dr.Read
					dim freeServicePartDetail as FreeServicePartDetail = me.CreateObject(dr)
					freeServicePartDetailList.Add(freeServicePartDetail)
			end while
			     
			return freeServicePartDetailList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim freeServicePartDetail as FreeServicePartDetail = ctype(obj, FreeServicePartDetail)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,freeServicePartDetail.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim freeServicePartDetail as FreeServicePartDetail = ctype(obj, FreeServicePartDetail)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@FreeServiceID", DbType.Int32, freeServicePartDetail.FreeServiceID)
            DbCommandWrapper.AddInParameter("@PartPrice", DbType.Currency, freeServicePartDetail.PartPrice)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Decimal, freeServicePartDetail.Quantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, freeServicePartDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, freeServicePartDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FreeServiceID", DbType.Int32, Me.GetRefObject(freeServicePartDetail.FreeService))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(freeServicePartDetail.SparePartMaster))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

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
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim freeServicePartDetail As FreeServicePartDetail = CType(obj, FreeServicePartDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, freeServicePartDetail.ID)
            'DbCommandWrapper.AddInParameter("@FreeServiceID", DbType.Int32, freeServicePartDetail.FreeServiceID)
            DbCommandWrapper.AddInParameter("@PartPrice", DbType.Currency, freeServicePartDetail.PartPrice)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Decimal, freeServicePartDetail.Quantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, freeServicePartDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, freeServicePartDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FreeServiceID", DbType.Int32, Me.GetRefObject(freeServicePartDetail.FreeService))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(freeServicePartDetail.SparePartMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FreeServicePartDetail

            Dim freeServicePartDetail As FreeServicePartDetail = New FreeServicePartDetail

            freeServicePartDetail.ID = CType(dr("ID"), Integer)
            ' If Not dr.IsDBNull(dr.GetOrdinal("FreeServiceID")) Then freeServicePartDetail.FreeServiceID = CType(dr("FreeServiceID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartPrice")) Then freeServicePartDetail.PartPrice = CType(dr("PartPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then freeServicePartDetail.Quantity = CType(dr("Quantity"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then freeServicePartDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then freeServicePartDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then freeServicePartDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then freeServicePartDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then freeServicePartDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("FreeServiceID")) Then
                freeServicePartDetail.FreeService = New FreeService(CType(dr("FreeServiceID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                freeServicePartDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If

            Return freeServicePartDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(FreeServicePartDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FreeServicePartDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FreeServicePartDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

