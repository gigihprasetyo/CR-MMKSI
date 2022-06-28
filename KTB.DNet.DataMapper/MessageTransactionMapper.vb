#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MessageTransaction Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/18/2020 - 8:43:07 AM
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

    Public Class MessageTransactionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMessageTransaction"
        Private m_UpdateStatement As String = "up_UpdateMessageTransaction"
        Private m_RetrieveStatement As String = "up_RetrieveMessageTransaction"
        Private m_RetrieveListStatement As String = "up_RetrieveMessageTransactionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMessageTransaction"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim messageTransaction As MessageTransaction = Nothing
            While dr.Read

                messageTransaction = Me.CreateObject(dr)

            End While

            Return messageTransaction

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim messageTransactionList As ArrayList = New ArrayList

            While dr.Read
                Dim messageTransaction As MessageTransaction = Me.CreateObject(dr)
                messageTransactionList.Add(messageTransaction)
            End While

            Return messageTransactionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim messageTransaction As MessageTransaction = CType(obj, MessageTransaction)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, messageTransaction.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim messageTransaction As MessageTransaction = CType(obj, MessageTransaction)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TemplateID", DbType.Int32, messageTransaction.TemplateID)
            DbCommandWrapper.AddInParameter("@Data", DbType.AnsiString, messageTransaction.Data)
            DbCommandWrapper.AddInParameter("@Nomer", DbType.Int16, messageTransaction.Nomer)
            DbCommandWrapper.AddInParameter("@ReffSource", DbType.AnsiString, messageTransaction.ReffSource)
            DbCommandWrapper.AddInParameter("@FID", DbType.Int32, messageTransaction.FID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, messageTransaction.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, messageTransaction.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, messageTransaction.LastUpdateBy)


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

            Dim messageTransaction As MessageTransaction = CType(obj, MessageTransaction)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, messageTransaction.ID)
            DbCommandWrapper.AddInParameter("@TemplateID", DbType.Int32, messageTransaction.TemplateID)
            DbCommandWrapper.AddInParameter("@Data", DbType.AnsiString, messageTransaction.Data)
            DbCommandWrapper.AddInParameter("@Nomer", DbType.Int16, messageTransaction.Nomer)
            DbCommandWrapper.AddInParameter("@ReffSource", DbType.AnsiString, messageTransaction.ReffSource)
            DbCommandWrapper.AddInParameter("@FID", DbType.Int32, messageTransaction.FID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, messageTransaction.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, messageTransaction.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, messageTransaction.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MessageTransaction

            Dim messageTransaction As MessageTransaction = New MessageTransaction

            messageTransaction.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TemplateID")) Then messageTransaction.TemplateID = CType(dr("TemplateID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Data")) Then messageTransaction.Data = dr("Data").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Nomer")) Then messageTransaction.Nomer = CType(dr("Nomer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ReffSource")) Then messageTransaction.ReffSource = dr("ReffSource").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FID")) Then messageTransaction.FID = CType(dr("FID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then messageTransaction.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then messageTransaction.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then messageTransaction.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then messageTransaction.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then messageTransaction.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then messageTransaction.LastUpdateBy = dr("LastUpdateBy").ToString

            Return messageTransaction

        End Function

        Private Sub SetTableName()

            If Not (GetType(MessageTransaction) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MessageTransaction), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MessageTransaction).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
