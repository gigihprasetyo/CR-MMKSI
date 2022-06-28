#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimDebitNote Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/6/2020 - 6:26:20 PM
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

    Public Class ClaimDebitNoteMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertClaimDebitNote"
        Private m_UpdateStatement As String = "up_UpdateClaimDebitNote"
        Private m_RetrieveStatement As String = "up_RetrieveClaimDebitNote"
        Private m_RetrieveListStatement As String = "up_RetrieveClaimDebitNoteList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteClaimDebitNote"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim claimDebitNote As ClaimDebitNote = Nothing
            While dr.Read

                claimDebitNote = Me.CreateObject(dr)

            End While

            Return claimDebitNote

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim claimDebitNoteList As ArrayList = New ArrayList

            While dr.Read
                Dim claimDebitNote As ClaimDebitNote = Me.CreateObject(dr)
                claimDebitNoteList.Add(claimDebitNote)
            End While

            Return claimDebitNoteList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimDebitNote As ClaimDebitNote = CType(obj, ClaimDebitNote)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimDebitNote.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimDebitNote As ClaimDebitNote = CType(obj, ClaimDebitNote)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, claimDebitNote.ClaimHeaderID)
            'DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, claimDebitNote.SparePartBillingID)
            DbCommandWrapper.AddInParameter("@DebitNoteNumber", DbType.AnsiString, claimDebitNote.DebitNoteNumber)
            DbCommandWrapper.AddInParameter("@DebitNoteDate", DbType.DateTime, claimDebitNote.DebitNoteDate)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, claimDebitNote.TotalAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimDebitNote.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, claimDebitNote.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(claimDebitNote.ClaimHeader))
            'DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(claimDebitNote.SparePartBilling))
            DbCommandWrapper.AddInParameter("@ClaimSPBillingReturID", DbType.Int32, Me.GetRefObject(claimDebitNote.ClaimSPBillingRetur))


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

            Dim claimDebitNote As ClaimDebitNote = CType(obj, ClaimDebitNote)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimDebitNote.ID)
            'DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, claimDebitNote.ClaimHeaderID)
            'DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, claimDebitNote.SparePartBillingID)
            DbCommandWrapper.AddInParameter("@DebitNoteNumber", DbType.AnsiString, claimDebitNote.DebitNoteNumber)
            DbCommandWrapper.AddInParameter("@DebitNoteDate", DbType.DateTime, claimDebitNote.DebitNoteDate)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, claimDebitNote.TotalAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimDebitNote.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, claimDebitNote.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(claimDebitNote.ClaimHeader))
            'DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(claimDebitNote.SparePartBilling))
            DbCommandWrapper.AddInParameter("@ClaimSPBillingReturID", DbType.Int32, Me.GetRefObject(claimDebitNote.ClaimSPBillingRetur))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ClaimDebitNote

            Dim claimDebitNote As ClaimDebitNote = New ClaimDebitNote

            claimDebitNote.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("ClaimHeaderID")) Then claimDebitNote.ClaimHeaderID = CType(dr("ClaimHeaderID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SparePartBillingID")) Then claimDebitNote.SparePartBillingID = CType(dr("SparePartBillingID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitNoteNumber")) Then claimDebitNote.DebitNoteNumber = dr("DebitNoteNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DebitNoteDate")) Then claimDebitNote.DebitNoteDate = CType(dr("DebitNoteDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then claimDebitNote.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then claimDebitNote.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then claimDebitNote.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then claimDebitNote.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then claimDebitNote.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then claimDebitNote.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            'If Not dr.IsDBNull(dr.GetOrdinal("ClaimHeaderID")) Then
            '    claimDebitNote.ClaimHeader = New ClaimHeader(CType(dr("ClaimHeaderID"), Integer))
            'End If
            'If Not dr.IsDBNull(dr.GetOrdinal("SparePartBillingID")) Then
            '    claimDebitNote.SparePartBilling = New SparePartBilling(CType(dr("SparePartBillingID"), Integer))
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimSPBillingReturID")) Then
                claimDebitNote.ClaimSPBillingRetur = New ClaimSPBillingRetur(CType(dr("ClaimSPBillingReturID"), Integer))
            End If

            Return claimDebitNote

        End Function

        Private Sub SetTableName()

            If Not (GetType(ClaimDebitNote) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ClaimDebitNote), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ClaimDebitNote).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
