
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LogisticDN Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 9/12/2017 - 2:34:51 PM
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

    Public Class LogisticDNMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertLogisticDN"
        Private m_UpdateStatement As String = "up_UpdateLogisticDN"
        Private m_RetrieveStatement As String = "up_RetrieveLogisticDN"
        Private m_RetrieveListStatement As String = "up_RetrieveLogisticDNList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteLogisticDN"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim logisticDN As LogisticDN = Nothing
            While dr.Read

                logisticDN = Me.CreateObject(dr)

            End While

            Return logisticDN

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim logisticDNList As ArrayList = New ArrayList

            While dr.Read
                Dim logisticDN As LogisticDN = Me.CreateObject(dr)
                logisticDNList.Add(logisticDN)
            End While

            Return logisticDNList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticDN As LogisticDN = CType(obj, LogisticDN)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticDN.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticDN As LogisticDN = CType(obj, LogisticDN)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DebitMemoNo", DbType.AnsiString, logisticDN.DebitMemoNo)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, logisticDN.BillingDate)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, logisticDN.DocumentType)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, logisticDN.TotalAmount)
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(logisticDN.SalesOrder))
            DbCommandWrapper.AddInParameter("@LogisticDCHeaderID", DbType.Int32, Me.GetRefObject(logisticDN.LogisticDCHeader))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticDN.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, logisticDN.LastUpdateBy)
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

            Dim logisticDN As LogisticDN = CType(obj, LogisticDN)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticDN.ID)
            DbCommandWrapper.AddInParameter("@DebitMemoNo", DbType.AnsiString, logisticDN.DebitMemoNo)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, logisticDN.BillingDate)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, logisticDN.DocumentType)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, logisticDN.TotalAmount)
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(logisticDN.SalesOrder))
            DbCommandWrapper.AddInParameter("@LogisticDCHeaderID", DbType.Int32, Me.GetRefObject(logisticDN.LogisticDCHeader))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticDN.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, logisticDN.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As LogisticDN

            Dim logisticDN As LogisticDN = New LogisticDN

            logisticDN.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitMemoNo")) Then logisticDN.DebitMemoNo = dr("DebitMemoNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then logisticDN.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then logisticDN.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then logisticDN.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then logisticDN.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then logisticDN.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then logisticDN.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then logisticDN.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then logisticDN.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderID")) Then
                logisticDN.SalesOrder = New SalesOrder(CType(dr("SalesOrderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticDCHeaderID")) Then
                logisticDN.logisticDCHeader = New LogisticDCHeader(CType(dr("LogisticDCHeaderID"), Integer))
            End If


            Return logisticDN

        End Function

        Private Sub SetTableName()

            If Not (GetType(LogisticDN) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(LogisticDN), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(LogisticDN).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

