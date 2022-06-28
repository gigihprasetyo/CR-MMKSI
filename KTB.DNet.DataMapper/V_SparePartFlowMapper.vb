
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SparePartFlow Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/7/2016 - 4:06:05 PM
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

    Public Class V_SparePartFlowMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SparePartFlow"
        Private m_UpdateStatement As String = "up_UpdateV_SparePartFlow"
        Private m_RetrieveStatement As String = "up_RetrieveV_SparePartFlow"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SparePartFlowList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SparePartFlow"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SparePartFlow As V_SparePartFlow = Nothing
            While dr.Read

                v_SparePartFlow = Me.CreateObject(dr)

            End While

            Return v_SparePartFlow

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SparePartFlowList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SparePartFlow As V_SparePartFlow = Me.CreateObject(dr)
                v_SparePartFlowList.Add(v_SparePartFlow)
            End While

            Return v_SparePartFlowList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SparePartFlow As V_SparePartFlow = CType(obj, V_SparePartFlow)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@Row", DbType.Int32, v_SparePartFlow.Row)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SparePartFlow As V_SparePartFlow = CType(obj, V_SparePartFlow)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@Row", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@POID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SparePartFlow.PONumber)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, v_SparePartFlow.PODate)
            DbCommandWrapper.AddInParameter("@POSendDate", DbType.DateTime, v_SparePartFlow.POSendDate)
            DbCommandWrapper.AddInParameter("@SOID", DbType.Int32, v_SparePartFlow.SOID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_SparePartFlow.SONumber)
            DbCommandWrapper.AddInParameter("@SoDate", DbType.DateTime, v_SparePartFlow.SoDate)
            DbCommandWrapper.AddInParameter("@DOID", DbType.Int32, v_SparePartFlow.DOID)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, v_SparePartFlow.DONumber)
            DbCommandWrapper.AddInParameter("@DoDate", DbType.DateTime, v_SparePartFlow.DoDate)
            DbCommandWrapper.AddInParameter("@BillingID", DbType.Int32, v_SparePartFlow.BillingID)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, v_SparePartFlow.BillingNumber)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, v_SparePartFlow.BillingDate)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SparePartFlow.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SparePartFlow.DealerCode)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, v_SparePartFlow.OrderType)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, v_SparePartFlow.DocumentType)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_SparePartFlow.Status)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@Row"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "Row")

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
            DbCommandWrapper.AddInParameter("@Row", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SparePartFlow As V_SparePartFlow = CType(obj, V_SparePartFlow)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@Row", DbType.Int32, v_SparePartFlow.Row)
            DbCommandWrapper.AddInParameter("@POID", DbType.Int32, v_SparePartFlow.POID)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SparePartFlow.PONumber)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, v_SparePartFlow.PODate)
            DbCommandWrapper.AddInParameter("@POSendDate", DbType.DateTime, v_SparePartFlow.POSendDate)
            DbCommandWrapper.AddInParameter("@SOID", DbType.Int32, v_SparePartFlow.SOID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_SparePartFlow.SONumber)
            DbCommandWrapper.AddInParameter("@SoDate", DbType.DateTime, v_SparePartFlow.SoDate)
            DbCommandWrapper.AddInParameter("@DOID", DbType.Int32, v_SparePartFlow.DOID)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, v_SparePartFlow.DONumber)
            DbCommandWrapper.AddInParameter("@DoDate", DbType.DateTime, v_SparePartFlow.DoDate)
            DbCommandWrapper.AddInParameter("@BillingID", DbType.Int32, v_SparePartFlow.BillingID)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, v_SparePartFlow.BillingNumber)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, v_SparePartFlow.BillingDate)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SparePartFlow.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SparePartFlow.DealerCode)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, v_SparePartFlow.OrderType)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, v_SparePartFlow.DocumentType)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_SparePartFlow.Status)


            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SparePartFlow

            Dim v_SparePartFlow As V_SparePartFlow = New V_SparePartFlow

            v_SparePartFlow.Row = CType(dr("Row"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("POID")) Then v_SparePartFlow.POID = dr("POID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then v_SparePartFlow.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then v_SparePartFlow.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("POSendDate")) Then v_SparePartFlow.POSendDate = CType(dr("POSendDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SOID")) Then v_SparePartFlow.SOID = CType(dr("SOID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then v_SparePartFlow.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SoDate")) Then v_SparePartFlow.SoDate = CType(dr("SoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DOID")) Then v_SparePartFlow.DOID = CType(dr("DOID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then v_SparePartFlow.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DoDate")) Then v_SparePartFlow.DoDate = CType(dr("DoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingID")) Then v_SparePartFlow.BillingID = CType(dr("BillingID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then v_SparePartFlow.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then v_SparePartFlow.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_SparePartFlow.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SparePartFlow.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then v_SparePartFlow.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then v_SparePartFlow.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_SparePartFlow.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then v_SparePartFlow.TermOfPaymentID = CType(dr("TermOfPaymentID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TOPCeilingStatus")) Then v_SparePartFlow.TOPCeilingStatus = dr("TOPCeilingStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TOPDescription")) Then v_SparePartFlow.TOPDescription = dr("TOPDescription").ToString

            Return v_SparePartFlow

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SparePartFlow) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SparePartFlow), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SparePartFlow).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

