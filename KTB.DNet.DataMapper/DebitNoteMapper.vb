
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DebitNote Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/3/2008 - 3:56:50 PM
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

    Public Class DebitNoteMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDebitNote"
        Private m_UpdateStatement As String = "up_UpdateDebitNote"
        Private m_RetrieveStatement As String = "up_RetrieveDebitNote"
        Private m_RetrieveListStatement As String = "up_RetrieveDebitNoteList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDebitNote"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim debitNote As DebitNote = Nothing
            While dr.Read

                debitNote = Me.CreateObject(dr)

            End While

            Return debitNote

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim debitNoteList As ArrayList = New ArrayList

            While dr.Read
                Dim debitNote As DebitNote = Me.CreateObject(dr)
                debitNoteList.Add(debitNote)
            End While

            Return debitNoteList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim debitNote As DebitNote = CType(obj, DebitNote)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, debitNote.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim debitNote As DebitNote = CType(obj, DebitNote)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DNNumber", DbType.AnsiStringFixedLength, debitNote.DNNumber)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, debitNote.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, debitNote.Description)
            DbCommandWrapper.AddInParameter("@Assignment", DbType.AnsiString, debitNote.Assignment)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, debitNote.PostingDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, debitNote.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, debitNote.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(debitNote.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(debitNote.ProductCategory))
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

            Dim debitNote As DebitNote = CType(obj, DebitNote)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, debitNote.ID)
            DbCommandWrapper.AddInParameter("@DNNumber", DbType.AnsiStringFixedLength, debitNote.DNNumber)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, debitNote.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, debitNote.Description)
            DbCommandWrapper.AddInParameter("@Assignment", DbType.AnsiString, debitNote.Assignment)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, debitNote.PostingDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, debitNote.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, debitNote.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(debitNote.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(debitNote.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DebitNote

            Dim debitNote As DebitNote = New DebitNote

            debitNote.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DNNumber")) Then debitNote.DNNumber = dr("DNNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then debitNote.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then debitNote.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Assignment")) Then debitNote.Assignment = dr("Assignment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then debitNote.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then debitNote.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then debitNote.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then debitNote.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then debitNote.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then debitNote.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                debitNote.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                debitNote.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If
            Return debitNote

        End Function

        Private Sub SetTableName()

            If Not (GetType(DebitNote) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DebitNote), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DebitNote).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

