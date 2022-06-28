
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RevisionTransferActual Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 20/03/2020 - 9:21:38
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

    Public Class RevisionTransferActualMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRevisionTransferActual"
        Private m_UpdateStatement As String = "up_UpdateRevisionTransferActual"
        Private m_RetrieveStatement As String = "up_RetrieveRevisionTransferActual"
        Private m_RetrieveListStatement As String = "up_RetrieveRevisionTransferActualList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRevisionTransferActual"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim revisionTransferActual As RevisionTransferActual = Nothing
            While dr.Read

                revisionTransferActual = Me.CreateObject(dr)

            End While

            Return revisionTransferActual

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim revisionTransferActualList As ArrayList = New ArrayList

            While dr.Read
                Dim revisionTransferActual As RevisionTransferActual = Me.CreateObject(dr)
                revisionTransferActualList.Add(revisionTransferActual)
            End While

            Return revisionTransferActualList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionTransferActual As RevisionTransferActual = CType(obj, RevisionTransferActual)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionTransferActual.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionTransferActual As RevisionTransferActual = CType(obj, RevisionTransferActual)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@RevisionPaymentHeaderID", DbType.Int32, revisionTransferActual.RevisionPaymentHeaderID)
            DbCommandWrapper.AddInParameter("@RevisionPaymentHeaderID", DbType.Int32, Me.GetRefObject(revisionTransferActual.RevisionPaymentHeader))
            DbCommandWrapper.AddInParameter("@RefTransferBank", DbType.AnsiString, revisionTransferActual.RefTransferBank)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, revisionTransferActual.Amount)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, revisionTransferActual.PostingDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionTransferActual.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedby", DbType.AnsiString, revisionTransferActual.LastUpdatedby)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, revisionTransferActual.LastUpdatedTime)


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

            Dim revisionTransferActual As RevisionTransferActual = CType(obj, RevisionTransferActual)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionTransferActual.ID)
            'DbCommandWrapper.AddInParameter("@RevisionPaymentHeaderID", DbType.Int32, revisionTransferActual.RevisionPaymentHeaderID)
            DbCommandWrapper.AddInParameter("@RevisionPaymentHeaderID", DbType.Int32, Me.GetRefObject(revisionTransferActual.RevisionPaymentHeader))
            DbCommandWrapper.AddInParameter("@RefTransferBank", DbType.AnsiString, revisionTransferActual.RefTransferBank)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, revisionTransferActual.Amount)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, revisionTransferActual.PostingDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionTransferActual.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, revisionTransferActual.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedby", DbType.AnsiString, revisionTransferActual.LastUpdatedby)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RevisionTransferActual

            Dim revisionTransferActual As RevisionTransferActual = New RevisionTransferActual

            revisionTransferActual.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("RevisionPaymentHeaderID")) Then revisionTransferActual.RevisionPaymentHeaderID = CType(dr("RevisionPaymentHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RefTransferBank")) Then revisionTransferActual.RefTransferBank = dr("RefTransferBank").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then revisionTransferActual.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then revisionTransferActual.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then revisionTransferActual.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then revisionTransferActual.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then revisionTransferActual.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedby")) Then revisionTransferActual.LastUpdatedby = dr("LastUpdatedby").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then revisionTransferActual.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionPaymentHeaderID")) Then
                revisionTransferActual.RevisionPaymentHeader = New RevisionPaymentHeader(CType(dr("RevisionPaymentHeaderID"), Integer))
            End If

            Return revisionTransferActual

        End Function

        Private Sub SetTableName()

            If Not (GetType(RevisionTransferActual) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RevisionTransferActual), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RevisionTransferActual).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

