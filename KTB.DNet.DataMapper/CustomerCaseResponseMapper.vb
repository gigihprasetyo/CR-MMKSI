
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerCaseResponse Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 7/10/2017 - 10:23:33 AM
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

    Public Class CustomerCaseResponseMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCustomerCaseResponse"
        Private m_UpdateStatement As String = "up_UpdateCustomerCaseResponse"
        Private m_RetrieveStatement As String = "up_RetrieveCustomerCaseResponse"
        Private m_RetrieveListStatement As String = "up_RetrieveCustomerCaseResponseList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCustomerCaseResponse"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim customerCaseResponse As CustomerCaseResponse = Nothing
            While dr.Read

                customerCaseResponse = Me.CreateObject(dr)

            End While

            Return customerCaseResponse

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim customerCaseResponseList As ArrayList = New ArrayList

            While dr.Read
                Dim customerCaseResponse As CustomerCaseResponse = Me.CreateObject(dr)
                customerCaseResponseList.Add(customerCaseResponse)
            End While

            Return customerCaseResponseList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerCaseResponse As CustomerCaseResponse = CType(obj, CustomerCaseResponse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerCaseResponse.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerCaseResponse As CustomerCaseResponse = CType(obj, CustomerCaseResponse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, customerCaseResponse.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, customerCaseResponse.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, customerCaseResponse.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, customerCaseResponse.Status)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Int16, customerCaseResponse.IsSend)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerCaseResponse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, customerCaseResponse.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, customerCaseResponse.LastUpdateTIme)

            DbCommandWrapper.AddInParameter("@CustomerCaseID", DbType.Int32, Me.GetRefObject(customerCaseResponse.CustomerCase))
            'DbCommandWrapper.AddInParameter("@EvidenceFile", DbType.AnsiString, customerCaseResponse.EvidenceFile)

            DbCommandWrapper.AddInParameter("@ServiceBookingID", DbType.Int32, Me.GetRefObject(customerCaseResponse.ServiceBooking))
            DbCommandWrapper.AddInParameter("@Response", DbType.Int16, customerCaseResponse.Response)
            DbCommandWrapper.AddInParameter("@BookingDatetime", DbType.DateTime, customerCaseResponse.BookingDatetime)

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

            Dim customerCaseResponse As CustomerCaseResponse = CType(obj, CustomerCaseResponse)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerCaseResponse.ID)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, customerCaseResponse.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, customerCaseResponse.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, customerCaseResponse.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, customerCaseResponse.Status)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Int16, customerCaseResponse.IsSend)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerCaseResponse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, customerCaseResponse.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, DateTime.Now)

            DbCommandWrapper.AddInParameter("@CustomerCaseID", DbType.Int32, Me.GetRefObject(customerCaseResponse.CustomerCase))
            'DbCommandWrapper.AddInParameter("@EvidenceFile", DbType.AnsiString, customerCaseResponse.EvidenceFile)

            DbCommandWrapper.AddInParameter("@ServiceBookingID", DbType.Int32, Me.GetRefObject(customerCaseResponse.ServiceBooking))
            DbCommandWrapper.AddInParameter("@Response", DbType.Int16, customerCaseResponse.Response)
            DbCommandWrapper.AddInParameter("@BookingDatetime", DbType.DateTime, customerCaseResponse.BookingDatetime)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CustomerCaseResponse

            Dim customerCaseResponse As CustomerCaseResponse = New CustomerCaseResponse

            customerCaseResponse.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then customerCaseResponse.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then customerCaseResponse.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then customerCaseResponse.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then customerCaseResponse.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSend")) Then customerCaseResponse.IsSend = CType(dr("IsSend"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then customerCaseResponse.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then customerCaseResponse.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then customerCaseResponse.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then customerCaseResponse.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTIme")) Then customerCaseResponse.LastUpdateTIme = CType(dr("LastUpdateTIme"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCaseID")) Then
                customerCaseResponse.CustomerCase = New CustomerCase(CType(dr("CustomerCaseID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("EvidenceFile")) Then customerCaseResponse.EvidenceFile = dr("EvidenceFile").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ServiceBookingID")) Then
                customerCaseResponse.ServiceBooking = New ServiceBooking(CType(dr("ServiceBookingID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("Response")) Then customerCaseResponse.Response = CType(dr("Response"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingDatetime")) Then customerCaseResponse.BookingDatetime = CType(dr("BookingDatetime"), DateTime)


            Return customerCaseResponse

        End Function

        Private Sub SetTableName()

            If Not (GetType(CustomerCaseResponse) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CustomerCaseResponse), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CustomerCaseResponse).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

