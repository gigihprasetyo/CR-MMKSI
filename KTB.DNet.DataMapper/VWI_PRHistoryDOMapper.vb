
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

    Public Class VWI_PRHistoryDOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPRHistoryDO"
        Private m_UpdateStatement As String = "up_UpdatePRHistoryDO"
        Private m_RetrieveStatement As String = "up_RetrievePRHistoryDO"
        Private m_RetrieveListStatement As String = "up_RetrievePRHistoryDOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePRHistoryDO"

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

            Dim pRHistoryDO As VWI_PRHistoryDO = Nothing
            While dr.Read

                pRHistoryDO = Me.CreateObject(dr)

            End While

            Return pRHistoryDO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pRHistoryDOList As ArrayList = New ArrayList

            While dr.Read
                Dim PRHistoryDO As VWI_PRHistoryDO = Me.CreateObject(dr)
                pRHistoryDOList.Add(PRHistoryDO)
            End While

            Return pRHistoryDOList

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

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_PRHistoryDO

            Dim pRHistoryDO As VWI_PRHistoryDO = New VWI_PRHistoryDO

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then pRHistoryDO.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then pRHistoryDO.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then pRHistoryDO.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then pRHistoryDO.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then pRHistoryDO.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then pRHistoryDO.DMSPRNo = dr("DMSPRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then pRHistoryDO.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then pRHistoryDO.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NomorDO")) Then pRHistoryDO.NomorDO = dr("NomorDO").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalDO")) Then pRHistoryDO.TanggalDO = CType(dr("TanggalDO"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then pRHistoryDO.SparePartMasterID = CType(dr("SparePartMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then pRHistoryDO.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then pRHistoryDO.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then pRHistoryDO.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EstimasiTanggalPengiriman")) Then pRHistoryDO.EstimasiTanggalPengiriman = CType(dr("EstimasiTanggalPengiriman"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PickingDate")) Then pRHistoryDO.PickingDate = CType(dr("PickingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PackingDate")) Then pRHistoryDO.PackingDate = CType(dr("PackingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GoodIssueDate")) Then pRHistoryDO.GoodIssueDate = CType(dr("GoodIssueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentDate")) Then pRHistoryDO.PaymentDate = CType(dr("PaymentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReadyForDeliveryDate")) Then pRHistoryDO.ReadyForDeliveryDate = CType(dr("ReadyForDeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ExpeditionNo")) Then pRHistoryDO.ExpeditionNo = dr("ExpeditionNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ExpeditionName")) Then pRHistoryDO.ExpeditionName = dr("ExpeditionName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ATD")) Then pRHistoryDO.ATD = CType(dr("ATD"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ETA")) Then pRHistoryDO.ETA = CType(dr("ETA"), DateTime)

            Return pRHistoryDO

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_PRHistoryDO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_PRHistoryDO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_PRHistoryDO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

