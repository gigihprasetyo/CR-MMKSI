
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExDebitMemo Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/11/2020 - 9:43:15 AM
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

    Public Class MSPExDebitMemoMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPExDebitMemo"
        Private m_UpdateStatement As String = "up_UpdateMSPExDebitMemo"
        Private m_RetrieveStatement As String = "up_RetrieveMSPExDebitMemo"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPExDebitMemoList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPExDebitMemo"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPExDebitMemo As MSPExDebitMemo = Nothing
            While dr.Read

                mSPExDebitMemo = Me.CreateObject(dr)

            End While

            Return mSPExDebitMemo

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPExDebitMemoList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPExDebitMemo As MSPExDebitMemo = Me.CreateObject(dr)
                mSPExDebitMemoList.Add(mSPExDebitMemo)
            End While

            Return mSPExDebitMemoList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExDebitMemo As MSPExDebitMemo = CType(obj, MSPExDebitMemo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExDebitMemo.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExDebitMemo As MSPExDebitMemo = CType(obj, MSPExDebitMemo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DebitMemoNo", DbType.AnsiString, mSPExDebitMemo.DebitMemoNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, mSPExDebitMemo.Amount)
            DbCommandWrapper.AddInParameter("@DocType", DbType.AnsiString, mSPExDebitMemo.DocType)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, mSPExDebitMemo.DocumentDate)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, mSPExDebitMemo.FileName)
            DbCommandWrapper.AddInParameter("@Rowstatus", DbType.Int16, mSPExDebitMemo.Rowstatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@Createdtime", DbType.DateTime, mSPExDebitMemo.Createdtime)
            DbCommandWrapper.AddInParameter("@LastUpdatedby", DbType.AnsiString, mSPExDebitMemo.LastUpdatedby)
            'DbCommandWrapper.AddInParameter("@LastUpdatedtime", DbType.DateTime, mSPExDebitMemo.LastUpdatedtime)

            'DbCommandWrapper.AddInParameter("@MSPExRegistrationID", DbType.Int32, mSPExDebitMemo.MSPExRegistrationID)
            DbCommandWrapper.AddInParameter("@MSPExRegistrationID", DbType.Int32, Me.GetRefObject(mSPExDebitMemo.MSPExRegistration))

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

            Dim mSPExDebitMemo As MSPExDebitMemo = CType(obj, MSPExDebitMemo)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExDebitMemo.ID)
            DbCommandWrapper.AddInParameter("@DebitMemoNo", DbType.AnsiString, mSPExDebitMemo.DebitMemoNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, mSPExDebitMemo.Amount)
            DbCommandWrapper.AddInParameter("@DocType", DbType.AnsiString, mSPExDebitMemo.DocType)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, mSPExDebitMemo.DocumentDate)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, mSPExDebitMemo.FileName)
            DbCommandWrapper.AddInParameter("@Rowstatus", DbType.Int16, mSPExDebitMemo.Rowstatus)
            DbCommandWrapper.AddInParameter("@Createdby", DbType.AnsiString, mSPExDebitMemo.Createdby)
            'DbCommandWrapper.AddInParameter("@Createdtime", DbType.DateTime, mSPExDebitMemo.Createdtime)
            DbCommandWrapper.AddInParameter("@LastUpdatedby", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedtime", DbType.DateTime, mSPExDebitMemo.LastUpdatedtime)


            'DbCommandWrapper.AddInParameter("@MSPExRegistrationID", DbType.Int32, mSPExDebitMemo.MSPExRegistrationID)
            DbCommandWrapper.AddInParameter("@MSPExRegistrationID", DbType.Int32, Me.GetRefObject(mSPExDebitMemo.MSPExRegistration))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPExDebitMemo

            Dim mSPExDebitMemo As MSPExDebitMemo = New MSPExDebitMemo

            mSPExDebitMemo.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitMemoNo")) Then mSPExDebitMemo.DebitMemoNo = dr("DebitMemoNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then mSPExDebitMemo.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DocType")) Then mSPExDebitMemo.DocType = dr("DocType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentDate")) Then mSPExDebitMemo.DocumentDate = CType(dr("DocumentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then mSPExDebitMemo.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Rowstatus")) Then mSPExDebitMemo.Rowstatus = CType(dr("Rowstatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Createdby")) Then mSPExDebitMemo.Createdby = dr("Createdby").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Createdtime")) Then mSPExDebitMemo.Createdtime = CType(dr("Createdtime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedby")) Then mSPExDebitMemo.LastUpdatedby = dr("LastUpdatedby").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedtime")) Then mSPExDebitMemo.LastUpdatedtime = CType(dr("LastUpdatedtime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExRegistrationID")) Then
                mSPExDebitMemo.MSPExRegistration = New MSPExRegistration(CType(dr("MSPExRegistrationID"), Integer))
            End If


            Return mSPExDebitMemo

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPExDebitMemo) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPExDebitMemo), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPExDebitMemo).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

