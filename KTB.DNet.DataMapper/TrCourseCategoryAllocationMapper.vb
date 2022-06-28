#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCourseCategoryAllocation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2019 - 9:41:30 AM
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class TrCourseCategoryAllocationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrCourseCategoryAllocation"
        Private m_UpdateStatement As String = "up_UpdateTrCourseCategoryAllocation"
        Private m_RetrieveStatement As String = "up_RetrieveTrCourseCategoryAllocation"
        Private m_RetrieveListStatement As String = "up_RetrieveTrCourseCategoryAllocationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrCourseCategoryAllocation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trCourseCategoryAllocation As TrCourseCategoryAllocation = Nothing
            While dr.Read

                trCourseCategoryAllocation = Me.CreateObject(dr)

            End While

            Return trCourseCategoryAllocation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trCourseCategoryAllocationList As ArrayList = New ArrayList

            While dr.Read
                Dim trCourseCategoryAllocation As TrCourseCategoryAllocation = Me.CreateObject(dr)
                trCourseCategoryAllocationList.Add(trCourseCategoryAllocation)
            End While

            Return trCourseCategoryAllocationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourseCategoryAllocation As TrCourseCategoryAllocation = CType(obj, TrCourseCategoryAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourseCategoryAllocation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCourseCategoryAllocation As TrCourseCategoryAllocation = CType(obj, TrCourseCategoryAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TrCourseCategoryID", DbType.Int32, Me.GetRefObject(trCourseCategoryAllocation.TrCourseCategory))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trCourseCategoryAllocation.Dealer))
            DbCommandWrapper.AddInParameter("@Allocated", DbType.Int32, trCourseCategoryAllocation.Allocated)
            DbCommandWrapper.AddInParameter("@LastAllocated", DbType.Int32, trCourseCategoryAllocation.LastAllocated)
            DbCommandWrapper.AddInParameter("@CancelReason", DbType.AnsiString, trCourseCategoryAllocation.CancelReason)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourseCategoryAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trCourseCategoryAllocation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim trCourseCategoryAllocation As TrCourseCategoryAllocation = CType(obj, TrCourseCategoryAllocation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCourseCategoryAllocation.ID)
            DbCommandWrapper.AddInParameter("@TrCourseCategoryID", DbType.Int32, Me.GetRefObject(trCourseCategoryAllocation.TrCourseCategory))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trCourseCategoryAllocation.Dealer))
            DbCommandWrapper.AddInParameter("@Allocated", DbType.Int32, trCourseCategoryAllocation.Allocated)
            DbCommandWrapper.AddInParameter("@LastAllocated", DbType.Int32, trCourseCategoryAllocation.LastAllocated)
            DbCommandWrapper.AddInParameter("@CancelReason", DbType.AnsiString, trCourseCategoryAllocation.CancelReason)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCourseCategoryAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trCourseCategoryAllocation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrCourseCategoryAllocation

            Dim trCourseCategoryAllocation As TrCourseCategoryAllocation = New TrCourseCategoryAllocation

            trCourseCategoryAllocation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrCourseCategoryID")) Then trCourseCategoryAllocation.TrCourseCategory = New TrCourseCategory(CType(dr("TrCourseCategoryID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then trCourseCategoryAllocation.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("Allocated")) Then trCourseCategoryAllocation.Allocated = CType(dr("Allocated"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastAllocated")) Then trCourseCategoryAllocation.LastAllocated = CType(dr("LastAllocated"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CancelReason")) Then trCourseCategoryAllocation.CancelReason = dr("CancelReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trCourseCategoryAllocation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trCourseCategoryAllocation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trCourseCategoryAllocation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trCourseCategoryAllocation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trCourseCategoryAllocation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trCourseCategoryAllocation

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrCourseCategoryAllocation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrCourseCategoryAllocation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrCourseCategoryAllocation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
