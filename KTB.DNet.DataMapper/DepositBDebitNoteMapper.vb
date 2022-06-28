
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBDebitNote Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/16/2016 - 10:06:52 AM
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

    Public Class DepositBDebitNoteMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositBDebitNote"
        Private m_UpdateStatement As String = "up_UpdateDepositBDebitNote"
        Private m_RetrieveStatement As String = "up_RetrieveDepositBDebitNote"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositBDebitNoteList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositBDebitNote"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositBDebitNote As DepositBDebitNote = Nothing
            While dr.Read

                depositBDebitNote = Me.CreateObject(dr)

            End While

            Return depositBDebitNote

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositBDebitNoteList As ArrayList = New ArrayList

            While dr.Read
                Dim depositBDebitNote As DepositBDebitNote = Me.CreateObject(dr)
                depositBDebitNoteList.Add(depositBDebitNote)
            End While

            Return depositBDebitNoteList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBDebitNote As DepositBDebitNote = CType(obj, DepositBDebitNote)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBDebitNote.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBDebitNote As DepositBDebitNote = CType(obj, DepositBDebitNote)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DNNumber", DbType.AnsiStringFixedLength, depositBDebitNote.DNNumber)
            DbCommandWrapper.AddInParameter("@Assignment", DbType.AnsiString, depositBDebitNote.Assignment)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, depositBDebitNote.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, depositBDebitNote.Description)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, depositBDebitNote.PostingDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, depositBDebitNote.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBDebitNote.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositBDebitNote.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBDebitNote.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBDebitNote.ProductCategory))

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

            Dim depositBDebitNote As DepositBDebitNote = CType(obj, DepositBDebitNote)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBDebitNote.ID)
            DbCommandWrapper.AddInParameter("@DNNumber", DbType.AnsiStringFixedLength, depositBDebitNote.DNNumber)
            DbCommandWrapper.AddInParameter("@Assignment", DbType.AnsiString, depositBDebitNote.Assignment)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, depositBDebitNote.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, depositBDebitNote.Description)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, depositBDebitNote.PostingDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, depositBDebitNote.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBDebitNote.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositBDebitNote.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBDebitNote.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBDebitNote.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositBDebitNote

            Dim depositBDebitNote As DepositBDebitNote = New DepositBDebitNote

            depositBDebitNote.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DNNumber")) Then depositBDebitNote.DNNumber = dr("DNNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Assignment")) Then depositBDebitNote.Assignment = dr("Assignment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then depositBDebitNote.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then depositBDebitNote.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then depositBDebitNote.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then depositBDebitNote.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositBDebitNote.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositBDebitNote.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositBDebitNote.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositBDebitNote.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositBDebitNote.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBDebitNote.Dealer))
            'DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBDebitNote.ProductCategory))
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                depositBDebitNote.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                depositBDebitNote.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If
            Return depositBDebitNote

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositBDebitNote) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositBDebitNote), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositBDebitNote).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

