
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKTChangeHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/05/2018 - 19:01:45
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

    Public Class PKTChangeHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPKTChangeHistory"
        Private m_UpdateStatement As String = "up_UpdatePKTChangeHistory"
        Private m_RetrieveStatement As String = "up_RetrievePKTChangeHistory"
        Private m_RetrieveListStatement As String = "up_RetrievePKTChangeHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePKTChangeHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pKTChangeHistory As PKTChangeHistory = Nothing
            While dr.Read

                pKTChangeHistory = Me.CreateObject(dr)

            End While

            Return pKTChangeHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pKTChangeHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim pKTChangeHistory As PKTChangeHistory = Me.CreateObject(dr)
                pKTChangeHistoryList.Add(pKTChangeHistory)
            End While

            Return pKTChangeHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKTChangeHistory As PKTChangeHistory = CType(obj, PKTChangeHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pKTChangeHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKTChangeHistory As PKTChangeHistory = CType(obj, PKTChangeHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DocType", DbType.Int32, pKTChangeHistory.DocType)
            DbCommandWrapper.AddInParameter("@ChangeType", DbType.Byte, pKTChangeHistory.ChangeType)
            DbCommandWrapper.AddInParameter("@DocNumber", DbType.AnsiString, pKTChangeHistory.DocNumber)
            DbCommandWrapper.AddInParameter("@OldValue", DbType.AnsiString, pKTChangeHistory.OldValue)
            DbCommandWrapper.AddInParameter("@NewValue", DbType.AnsiString, pKTChangeHistory.NewValue)
            DbCommandWrapper.AddInParameter("@OldDate", DbType.DateTime, pKTChangeHistory.OldDate)
            DbCommandWrapper.AddInParameter("@NewDate", DbType.DateTime, pKTChangeHistory.NewDate)
            DbCommandWrapper.AddInParameter("@OldAmount", DbType.Currency, pKTChangeHistory.OldAmount)
            DbCommandWrapper.AddInParameter("@NewAmount", DbType.Currency, pKTChangeHistory.NewAmount)
            DbCommandWrapper.AddInParameter("@OldQty", DbType.Int32, pKTChangeHistory.OldQty)
            DbCommandWrapper.AddInParameter("@NewQty", DbType.Int32, pKTChangeHistory.NewQty)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pKTChangeHistory.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKTChangeHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pKTChangeHistory.LastUpdateBy)
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

            Dim pKTChangeHistory As PKTChangeHistory = CType(obj, PKTChangeHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pKTChangeHistory.ID)
            DbCommandWrapper.AddInParameter("@DocType", DbType.Int32, pKTChangeHistory.DocType)
            DbCommandWrapper.AddInParameter("@ChangeType", DbType.Byte, pKTChangeHistory.ChangeType)
            DbCommandWrapper.AddInParameter("@DocNumber", DbType.AnsiString, pKTChangeHistory.DocNumber)
            DbCommandWrapper.AddInParameter("@OldValue", DbType.AnsiString, pKTChangeHistory.OldValue)
            DbCommandWrapper.AddInParameter("@NewValue", DbType.AnsiString, pKTChangeHistory.NewValue)
            DbCommandWrapper.AddInParameter("@OldDate", DbType.DateTime, pKTChangeHistory.OldDate)
            DbCommandWrapper.AddInParameter("@NewDate", DbType.DateTime, pKTChangeHistory.NewDate)
            DbCommandWrapper.AddInParameter("@OldAmount", DbType.Currency, pKTChangeHistory.OldAmount)
            DbCommandWrapper.AddInParameter("@NewAmount", DbType.Currency, pKTChangeHistory.NewAmount)
            DbCommandWrapper.AddInParameter("@OldQty", DbType.Int32, pKTChangeHistory.OldQty)
            DbCommandWrapper.AddInParameter("@NewQty", DbType.Int32, pKTChangeHistory.NewQty)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pKTChangeHistory.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKTChangeHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pKTChangeHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PKTChangeHistory

            Dim pKTChangeHistory As PKTChangeHistory = New PKTChangeHistory

            pKTChangeHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocType")) Then pKTChangeHistory.DocType = CType(dr("DocType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChangeType")) Then pKTChangeHistory.ChangeType = CType(dr("ChangeType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("DocNumber")) Then pKTChangeHistory.DocNumber = dr("DocNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OldValue")) Then pKTChangeHistory.OldValue = dr("OldValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NewValue")) Then pKTChangeHistory.NewValue = dr("NewValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OldDate")) Then pKTChangeHistory.OldDate = CType(dr("OldDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NewDate")) Then pKTChangeHistory.NewDate = CType(dr("NewDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OldAmount")) Then pKTChangeHistory.OldAmount = CType(dr("OldAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("NewAmount")) Then pKTChangeHistory.NewAmount = CType(dr("NewAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OldQty")) Then pKTChangeHistory.OldQty = CType(dr("OldQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NewQty")) Then pKTChangeHistory.NewQty = CType(dr("NewQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then pKTChangeHistory.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pKTChangeHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pKTChangeHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pKTChangeHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pKTChangeHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pKTChangeHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return pKTChangeHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(PKTChangeHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PKTChangeHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PKTChangeHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

