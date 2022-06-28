#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : SalesOrderInterest Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2021 - 10:43:15 AM
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

    Public Class VW_SalesOrderInterestMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesOrderInterest"
        Private m_UpdateStatement As String = "up_UpdateSalesOrderInterest"
        Private m_RetrieveStatement As String = "up_Retrieve_VW_SalesOrderInterest"
        Private m_RetrieveListStatement As String = "up_Retrieve_VW_SalesOrderInterestList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesOrderInterest"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesOrderInterest As VW_SalesOrderInterest = Nothing
            While dr.Read

                salesOrderInterest = Me.CreateObject(dr)

            End While

            Return salesOrderInterest

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesOrderInterestList As ArrayList = New ArrayList

            While dr.Read
                Dim salesOrderInterest As VW_SalesOrderInterest = Me.CreateObject(dr)
                salesOrderInterestList.Add(salesOrderInterest)
            End While

            Return salesOrderInterestList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesOrderInterest As SalesOrderInterest = CType(obj, SalesOrderInterest)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesOrderInterest.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesOrderInterest As SalesOrderInterest = CType(obj, SalesOrderInterest)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, salesOrderInterest.SONumber)
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(salesOrderInterest.SalesOrder))
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, salesOrderInterest.BillingNumber)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, salesOrderInterest.BillingDate)
            DbCommandWrapper.AddInParameter("@DocReference", DbType.AnsiString, salesOrderInterest.DocReference)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(salesOrderInterest.Dealer))
            DbCommandWrapper.AddInParameter("@DPPAmount", DbType.Currency, salesOrderInterest.DPPAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, salesOrderInterest.PPHAmount)
            DbCommandWrapper.AddInParameter("@AdditionalAmount", DbType.Currency, salesOrderInterest.AdditionalAmount)
            DbCommandWrapper.AddInParameter("@TrType", DbType.AnsiString, salesOrderInterest.TrType)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, salesOrderInterest.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesOrderInterest.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesOrderInterest.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, salesOrderInterest.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@InterestPPHHeaderID", DbType.Int32, Me.GetRefObject(salesOrderInterest.InterestPPHHeader))



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

            Dim salesOrderInterest As SalesOrderInterest = CType(obj, SalesOrderInterest)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesOrderInterest.ID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, salesOrderInterest.SONumber)
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(salesOrderInterest.SalesOrder))
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, salesOrderInterest.BillingNumber)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, salesOrderInterest.BillingDate)
            DbCommandWrapper.AddInParameter("@DocReference", DbType.AnsiString, salesOrderInterest.DocReference)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(salesOrderInterest.Dealer))
            DbCommandWrapper.AddInParameter("@DPPAmount", DbType.Currency, salesOrderInterest.DPPAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, salesOrderInterest.PPHAmount)
            DbCommandWrapper.AddInParameter("@AdditionalAmount", DbType.Currency, salesOrderInterest.AdditionalAmount)
            DbCommandWrapper.AddInParameter("@TrType", DbType.AnsiString, salesOrderInterest.TrType)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, salesOrderInterest.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesOrderInterest.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesOrderInterest.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesOrderInterest.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, salesOrderInterest.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@InterestPPHHeaderID", DbType.Int32, Me.GetRefObject(salesOrderInterest.InterestPPHHeader))



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VW_SalesOrderInterest

            Dim salesOrderInterest As VW_SalesOrderInterest = New VW_SalesOrderInterest

            salesOrderInterest.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then salesOrderInterest.SONumber = dr("SONumber").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderID")) Then salesOrderInterest.SalesOrderID = CType(dr("SalesOrderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then salesOrderInterest.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then salesOrderInterest.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DocReference")) Then salesOrderInterest.DocReference = dr("DocReference").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then salesOrderInterest.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DPPAmount")) Then salesOrderInterest.DPPAmount = CType(dr("DPPAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHAmount")) Then salesOrderInterest.PPHAmount = CType(dr("PPHAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AdditionalAmount")) Then salesOrderInterest.AdditionalAmount = CType(dr("AdditionalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TrType")) Then salesOrderInterest.TrType = dr("TrType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocNumber")) Then salesOrderInterest.DocNumber = dr("DocNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then salesOrderInterest.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesOrderInterest.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesOrderInterest.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesOrderInterest.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesOrderInterest.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesOrderInterest.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                salesOrderInterest.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderID")) Then
                salesOrderInterest.SalesOrder = New SalesOrder(CType(dr("SalesOrderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("InterestPPHHeaderID")) Then
                salesOrderInterest.InterestPPHHeader = New InterestPPHHeader(CType(dr("InterestPPHHeaderID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("NoReg")) Then salesOrderInterest.NoReg = dr("NoReg").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubmissionStatus")) Then salesOrderInterest.SubmissionStatus = dr("SubmissionStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then salesOrderInterest.PONumber = dr("PONumber").ToString

            Return salesOrderInterest

        End Function

        Private Sub SetTableName()

            If Not (GetType(VW_SalesOrderInterest) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VW_SalesOrderInterest), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VW_SalesOrderInterest).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
