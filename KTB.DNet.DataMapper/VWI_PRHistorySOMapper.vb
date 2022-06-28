
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2018 - 21:59:25
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

    Public Class VWI_PRHistorySOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPRHistorySO"
        Private m_UpdateStatement As String = "up_UpdatePRHistorySO"
        Private m_RetrieveStatement As String = "up_RetrievePRHistorySO"
        Private m_RetrieveListStatement As String = "up_RetrievePRHistorySOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePRHistorySO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal user As String) As DBCommandWrapper
            Throw New NotImplementedException()
        End Function
        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal user As String) As DBCommandWrapper
            Throw New NotImplementedException()
        End Function
        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As DBCommandWrapper
            Throw New NotImplementedException()
        End Function

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pRHistorySO As VWI_PRHistorySO = Nothing
            While dr.Read

                pRHistorySO = Me.CreateObject(dr)

            End While

            Return pRHistorySO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pRHistorySOList As ArrayList = New ArrayList

            While dr.Read
                Dim PRHistorySO As VWI_PRHistorySO = Me.CreateObject(dr)
                pRHistorySOList.Add(PRHistorySO)
            End While

            Return pRHistorySOList

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



#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_PRHistorySO

            Dim pRHistorySO As VWI_PRHistorySO = New VWI_PRHistorySO

            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then pRHistorySO.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then pRHistorySO.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then pRHistorySO.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then pRHistorySO.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then pRHistorySO.DMSPRNo = dr("DMSPRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then pRHistorySO.OrderType = CType(dr("OrderType"), Char)
            If Not dr.IsDBNull(dr.GetOrdinal("SODate")) Then pRHistorySO.SODate = CType(dr("SODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NomorPenjualan")) Then pRHistorySO.NomorPenjualan = dr("NomorPenjualan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then pRHistorySO.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KodeBarang")) Then pRHistorySO.KodeBarang = dr("KodeBarang").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NamaBarang")) Then pRHistorySO.NamaBarang = dr("NamaBarang").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JumlahPesanan")) Then pRHistorySO.JumlahPesanan = CType(dr("JumlahPesanan"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JumlahPemenuhan")) Then pRHistorySO.JumlahPemenuhan = CType(dr("JumlahPemenuhan"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaEceran")) Then pRHistorySO.HargaEceran = CType(dr("HargaEceran"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalBaseAmountDetail")) Then pRHistorySO.TotalBaseAmountDetail = CType(dr("TotalBaseAmountDetail"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NomorPengganti")) Then pRHistorySO.NomorPengganti = dr("NomorPengganti").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Diskon")) Then pRHistorySO.Diskon = CType(dr("Diskon"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmountDetail")) Then pRHistorySO.TotalAmountDetail = CType(dr("TotalAmountDetail"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalBaseAmountHeader")) Then pRHistorySO.TotalBaseAmountHeader = CType(dr("TotalBaseAmountHeader"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalConsumptionTaxAmount")) Then pRHistorySO.TotalConsumptionTaxAmount = CType(dr("TotalConsumptionTaxAmount"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmountHeader")) Then pRHistorySO.TotalAmountHeader = CType(dr("TotalAmountHeader"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then pRHistorySO.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDesc")) Then pRHistorySO.StatusDesc = dr("StatusDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pRHistorySO.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return pRHistorySO

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_PRHistorySO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_PRHistorySO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_PRHistorySO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

