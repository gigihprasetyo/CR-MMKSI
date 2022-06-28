
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPO Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 11/21/2011 - 11:04:25 AM
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

    Public Class SparePartPOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPO"
        Private m_UpdateStatement As String = "up_UpdateSparePartPO"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPO"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPO As SparePartPO = Nothing
            While dr.Read

                sparePartPO = Me.CreateObject(dr)

            End While

            Return sparePartPO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPOList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPO As SparePartPO = Me.CreateObject(dr)
                sparePartPOList.Add(sparePartPO)
            End While

            Return sparePartPOList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPO As SparePartPO = CType(obj, SparePartPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPO.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPO As SparePartPO = CType(obj, SparePartPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, sparePartPO.PONumber)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, sparePartPO.OrderType)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, sparePartPO.PODate)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, sparePartPO.DeliveryDate)
            DbCommandWrapper.AddInParameter("@ProcessCode", DbType.AnsiString, sparePartPO.ProcessCode)
            DbCommandWrapper.AddInParameter("@CancelRequestBy", DbType.AnsiString, sparePartPO.CancelRequestBy)
            DbCommandWrapper.AddInParameter("@IndentTransfer", DbType.Byte, sparePartPO.IndentTransfer)
            DbCommandWrapper.AddInParameter("@PickingTicket", DbType.AnsiString, sparePartPO.PickingTicket)
            DbCommandWrapper.AddInParameter("@SentPODate", DbType.DateTime, sparePartPO.SentPODate)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Boolean, sparePartPO.IsTransfer)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, sparePartPO.DMSPRNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPO.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparePartPO.Dealer))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int16, Me.GetRefObject(sparePartPO.TermOfPayment))
            DbCommandWrapper.AddInParameter("@TOPBlockStatusID", DbType.Int32, Me.GetRefObject(sparePartPO.TOPBlockStatus))
            DbCommandWrapper.AddInParameter("@PQRHeaderID", DbType.Int32, Me.GetRefObject(sparePartPO.PQRHeader))

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

            Dim sparePartPO As SparePartPO = CType(obj, SparePartPO)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPO.ID)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, sparePartPO.PONumber)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, sparePartPO.OrderType)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, sparePartPO.PODate)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, sparePartPO.DeliveryDate)
            DbCommandWrapper.AddInParameter("@ProcessCode", DbType.AnsiString, sparePartPO.ProcessCode)
            DbCommandWrapper.AddInParameter("@CancelRequestBy", DbType.AnsiString, sparePartPO.CancelRequestBy)
            DbCommandWrapper.AddInParameter("@IndentTransfer", DbType.Byte, sparePartPO.IndentTransfer)
            DbCommandWrapper.AddInParameter("@PickingTicket", DbType.AnsiString, sparePartPO.PickingTicket)
            DbCommandWrapper.AddInParameter("@SentPODate", DbType.DateTime, sparePartPO.SentPODate)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Boolean, sparePartPO.IsTransfer)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, sparePartPO.DMSPRNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPO.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparePartPO.Dealer))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int16, Me.GetRefObject(sparePartPO.TermOfPayment))
            DbCommandWrapper.AddInParameter("@TOPBlockStatusID", DbType.Int32, Me.GetRefObject(sparePartPO.TOPBlockStatus))
            DbCommandWrapper.AddInParameter("@PQRHeaderID", DbType.Int32, Me.GetRefObject(sparePartPO.PQRHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPO

            Dim sparePartPO As SparePartPO = New SparePartPO

            sparePartPO.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then sparePartPO.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then sparePartPO.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then sparePartPO.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryDate")) Then sparePartPO.DeliveryDate = CType(dr("DeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessCode")) Then sparePartPO.ProcessCode = dr("ProcessCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CancelRequestBy")) Then sparePartPO.CancelRequestBy = dr("CancelRequestBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IndentTransfer")) Then sparePartPO.IndentTransfer = CType(dr("IndentTransfer"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PickingTicket")) Then sparePartPO.PickingTicket = dr("PickingTicket").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SentPODate")) Then sparePartPO.SentPODate = CType(dr("SentPODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfer")) Then sparePartPO.IsTransfer = CType(dr("IsTransfer"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then sparePartPO.DMSPRNo = dr("DMSPRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPO.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPO.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPO.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPO.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPO.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sparePartPO.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then
                sparePartPO.TermOfPayment = New TermOfPayment(CType(dr("TermOfPaymentID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TOPBlockStatusID")) Then
                sparePartPO.TOPBlockStatus = New TOPBlockStatus(CType(dr("TOPBlockStatusID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderID")) Then
                sparePartPO.PQRHeader = New PQRHeader(CType(dr("PQRHeaderID"), Integer))
            End If
            Return sparePartPO

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

