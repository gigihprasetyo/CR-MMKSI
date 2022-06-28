#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DeliveryCustomerHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/20/2007 - 4:07:19 PM
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

    Public Class DeliveryCustomerHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDeliveryCustomerHeader"
        Private m_UpdateStatement As String = "up_UpdateDeliveryCustomerHeader"
        Private m_RetrieveStatement As String = "up_RetrieveDeliveryCustomerHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveDeliveryCustomerHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDeliveryCustomerHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim deliveryCustomerHeader As DeliveryCustomerHeader = Nothing
            While dr.Read

                deliveryCustomerHeader = Me.CreateObject(dr)

            End While

            Return deliveryCustomerHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim deliveryCustomerHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim deliveryCustomerHeader As DeliveryCustomerHeader = Me.CreateObject(dr)
                deliveryCustomerHeaderList.Add(deliveryCustomerHeader)
            End While

            Return deliveryCustomerHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim deliveryCustomerHeader As DeliveryCustomerHeader = CType(obj, DeliveryCustomerHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, deliveryCustomerHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim deliveryCustomerHeader As DeliveryCustomerHeader = CType(obj, DeliveryCustomerHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RegDONumber", DbType.AnsiString, deliveryCustomerHeader.RegDONumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, deliveryCustomerHeader.Status)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, deliveryCustomerHeader.PostingDate)
            DbCommandWrapper.AddInParameter("@ReffDONumber", DbType.AnsiString, deliveryCustomerHeader.ReffDONumber)
            DbCommandWrapper.AddInParameter("@SalesmanID", DbType.Int32, deliveryCustomerHeader.SalesmanID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, deliveryCustomerHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, deliveryCustomerHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DestinationCustomer", DbType.Int32, Me.GetRefObject(deliveryCustomerHeader.Customer))
            DbCommandWrapper.AddInParameter("@DestinationDealer", DbType.Int32, Me.GetRefObject(deliveryCustomerHeader.Dealer))
            DbCommandWrapper.AddInParameter("@FromDealer", DbType.Int32, deliveryCustomerHeader.FromDealer)

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

            Dim deliveryCustomerHeader As DeliveryCustomerHeader = CType(obj, DeliveryCustomerHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, deliveryCustomerHeader.ID)
            DbCommandWrapper.AddInParameter("@RegDONumber", DbType.AnsiString, deliveryCustomerHeader.RegDONumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, deliveryCustomerHeader.Status)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, deliveryCustomerHeader.PostingDate)
            DbCommandWrapper.AddInParameter("@ReffDONumber", DbType.AnsiString, deliveryCustomerHeader.ReffDONumber)
            DbCommandWrapper.AddInParameter("@SalesmanID", DbType.Int32, deliveryCustomerHeader.SalesmanID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, deliveryCustomerHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, deliveryCustomerHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DestinationCustomer", DbType.Int32, Me.GetRefObject(deliveryCustomerHeader.Customer))
            DbCommandWrapper.AddInParameter("@DestinationDealer", DbType.Int32, Me.GetRefObject(deliveryCustomerHeader.Dealer))
            DbCommandWrapper.AddInParameter("@FromDealer", DbType.Int32, deliveryCustomerHeader.FromDealer)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DeliveryCustomerHeader

            Dim deliveryCustomerHeader As DeliveryCustomerHeader = New DeliveryCustomerHeader

            deliveryCustomerHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegDONumber")) Then deliveryCustomerHeader.RegDONumber = dr("RegDONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then deliveryCustomerHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then deliveryCustomerHeader.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReffDONumber")) Then deliveryCustomerHeader.ReffDONumber = dr("ReffDONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanID")) Then deliveryCustomerHeader.SalesmanID = CType(dr("SalesmanID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then deliveryCustomerHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then deliveryCustomerHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then deliveryCustomerHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then deliveryCustomerHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then deliveryCustomerHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DestinationCustomer")) Then
                deliveryCustomerHeader.Customer = New Customer(CType(dr("DestinationCustomer"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DestinationDealer")) Then
                deliveryCustomerHeader.Dealer = New Dealer(CType(dr("DestinationDealer"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("FromDealer")) Then
                deliveryCustomerHeader.FromDealer = CType(dr("FromDealer"), Integer)
            End If

            Return deliveryCustomerHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(DeliveryCustomerHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DeliveryCustomerHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DeliveryCustomerHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

