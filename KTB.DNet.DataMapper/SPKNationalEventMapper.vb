#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKNationalEvent Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 4/27/2021 - 11:19:48 AM
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

    Public Class SPKNationalEventMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPKNationalEvent"
        Private m_UpdateStatement As String = "up_UpdateSPKNationalEvent"
        Private m_RetrieveStatement As String = "up_RetrieveSPKNationalEvent"
        Private m_RetrieveListStatement As String = "up_RetrieveSPKNationalEventList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPKNationalEvent"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPKNationalEvent As SPKNationalEvent = Nothing
            While dr.Read

                sPKNationalEvent = Me.CreateObject(dr)

            End While

            Return sPKNationalEvent

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPKNationalEventList As ArrayList = New ArrayList

            While dr.Read
                Dim sPKNationalEvent As SPKNationalEvent = Me.CreateObject(dr)
                sPKNationalEventList.Add(sPKNationalEvent)
            End While

            Return sPKNationalEventList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKNationalEvent As SPKNationalEvent = CType(obj, SPKNationalEvent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKNationalEvent.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKNationalEvent As SPKNationalEvent = CType(obj, SPKNationalEvent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, sPKNationalEvent.SPKNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, sPKNationalEvent.DealerSPKDate)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.String, sPKNationalEvent.CustomerName)
            DbCommandWrapper.AddInParameter("@AssyYear", DbType.Int32, sPKNationalEvent.AssyYear)
            DbCommandWrapper.AddInParameter("@EndCustomerPrintedTime", DbType.DateTime, sPKNationalEvent.EndCustomerPrintedTime)
            DbCommandWrapper.AddInParameter("@DownPayment", DbType.Int64, sPKNationalEvent.DownPayment)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sPKNationalEvent.Quantity)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, sPKNationalEvent.Remarks)
            DbCommandWrapper.AddInParameter("@Shift", DbType.Int64, sPKNationalEvent.Shift)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKNationalEvent.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, sPKNationalEvent.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPKNationalEvent.Dealer))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.VechileColor))
            DbCommandWrapper.AddInParameter("@NationalEventID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.NationalEvent))
            DbCommandWrapper.AddInParameter("@PaymentTypeID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.PaymentType))
            DbCommandWrapper.AddInParameter("@LeasingID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.Leasing))

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

            Dim sPKNationalEvent As SPKNationalEvent = CType(obj, SPKNationalEvent)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKNationalEvent.ID)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, sPKNationalEvent.SPKNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, sPKNationalEvent.DealerSPKDate)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.String, sPKNationalEvent.CustomerName)
            DbCommandWrapper.AddInParameter("@AssyYear", DbType.Int32, sPKNationalEvent.AssyYear)
            DbCommandWrapper.AddInParameter("@EndCustomerPrintedTime", DbType.DateTime, sPKNationalEvent.EndCustomerPrintedTime)
            DbCommandWrapper.AddInParameter("@DownPayment", DbType.Int64, sPKNationalEvent.DownPayment)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sPKNationalEvent.Quantity)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, sPKNationalEvent.Remarks)
            DbCommandWrapper.AddInParameter("@Shift", DbType.Int64, sPKNationalEvent.Shift)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKNationalEvent.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, sPKNationalEvent.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)


            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.VechileColor))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPKNationalEvent.Dealer))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@NationalEventID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.NationalEvent))
            DbCommandWrapper.AddInParameter("@PaymentTypeID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.PaymentType))
            DbCommandWrapper.AddInParameter("@LeasingID", DbType.Int32, Me.GetRefObject(sPKNationalEvent.Leasing))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPKNationalEvent

            Dim sPKNationalEvent As SPKNationalEvent = New SPKNationalEvent

            sPKNationalEvent.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then sPKNationalEvent.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKDate")) Then sPKNationalEvent.DealerSPKDate = CType(dr("DealerSPKDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then sPKNationalEvent.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AssyYear")) Then sPKNationalEvent.AssyYear = CType(dr("AssyYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerPrintedTime")) Then sPKNationalEvent.EndCustomerPrintedTime = CType(dr("EndCustomerPrintedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DownPayment")) Then sPKNationalEvent.DownPayment = CType(dr("DownPayment"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then sPKNationalEvent.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then sPKNationalEvent.Remarks = dr("Remarks").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Shift")) Then sPKNationalEvent.Shift = CType(dr("Shift"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPKNationalEvent.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPKNationalEvent.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPKNationalEvent.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPKNationalEvent.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPKNationalEvent.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sPKNationalEvent.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                sPKNationalEvent.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then
                sPKNationalEvent.VechileColor = New VechileColor(CType(dr("VechileColorID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("NationalEventID")) Then
                sPKNationalEvent.NationalEvent = New NationalEvent(CType(dr("NationalEventID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentTypeID")) Then
                sPKNationalEvent.PaymentType = New PaymentType(CType(dr("PaymentTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("LeasingID")) Then
                sPKNationalEvent.Leasing = New Leasing(CType(dr("LeasingID"), Integer))
            End If

            Return sPKNationalEvent

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPKNationalEvent) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPKNationalEvent), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPKNationalEvent).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
