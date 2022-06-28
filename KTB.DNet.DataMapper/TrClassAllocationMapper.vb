
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrClassAllocation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/14/2005 - 10:31:57 AM
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

    Public Class TrClassAllocationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrClassAllocation"
        Private m_UpdateStatement As String = "up_UpdateTrClassAllocation"
        Private m_RetrieveStatement As String = "up_RetrieveTrClassAllocation"
        Private m_RetrieveListStatement As String = "up_RetrieveTrClassAllocationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrClassAllocation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trClassAllocation As TrClassAllocation = Nothing
            While dr.Read

                trClassAllocation = Me.CreateObject(dr)

            End While

            Return trClassAllocation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trClassAllocationList As ArrayList = New ArrayList

            While dr.Read
                Dim trClassAllocation As TrClassAllocation = Me.CreateObject(dr)
                trClassAllocationList.Add(trClassAllocation)
            End While

            Return trClassAllocationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trClassAllocation As TrClassAllocation = CType(obj, TrClassAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trClassAllocation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trClassAllocation As TrClassAllocation = CType(obj, TrClassAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@Allocated", DbType.Int32, trClassAllocation.Allocated)
            DBCommandWrapper.AddInParameter("@LastAllocated", DbType.Int32, trClassAllocation.LastAllocated)
            DBCommandWrapper.AddInParameter("@CancelReason", DbType.AnsiString, trClassAllocation.CancelReason)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trClassAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trClassAllocation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, Me.GetRefObject(trClassAllocation.TrClass))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(trClassAllocation.Dealer))

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

            Dim trClassAllocation As TrClassAllocation = CType(obj, TrClassAllocation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trClassAllocation.ID)
            DBCommandWrapper.AddInParameter("@Allocated", DbType.Int32, trClassAllocation.Allocated)
            DBCommandWrapper.AddInParameter("@LastAllocated", DbType.Int32, trClassAllocation.LastAllocated)
            DBCommandWrapper.AddInParameter("@CancelReason", DbType.AnsiString, trClassAllocation.CancelReason)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trClassAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trClassAllocation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, Me.GetRefObject(trClassAllocation.TrClass))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(trClassAllocation.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrClassAllocation

            Dim trClassAllocation As TrClassAllocation = New TrClassAllocation

            trClassAllocation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Allocated")) Then trClassAllocation.Allocated = CType(dr("Allocated"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastAllocated")) Then trClassAllocation.LastAllocated = CType(dr("LastAllocated"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CancelReason")) Then trClassAllocation.CancelReason = dr("CancelReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trClassAllocation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trClassAllocation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trClassAllocation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trClassAllocation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trClassAllocation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ClassID")) Then
                trClassAllocation.TrClass = New TrClass(CType(dr("ClassID"), Integer))
                trClassAllocation.ClassID = CType(dr("ClassID"), Integer)
            Else
                trClassAllocation.ClassID = 0
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                trClassAllocation.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return trClassAllocation

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrClassAllocation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrClassAllocation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrClassAllocation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

