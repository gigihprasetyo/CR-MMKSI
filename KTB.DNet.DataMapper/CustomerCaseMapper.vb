
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerCase Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 7/10/2017 - 10:22:56 AM
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

    Public Class CustomerCaseMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCustomerCase"
        Private m_UpdateStatement As String = "up_UpdateCustomerCase"
        Private m_RetrieveStatement As String = "up_RetrieveCustomerCase"
        Private m_RetrieveListStatement As String = "up_RetrieveCustomerCaseList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCustomerCase"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim customerCase As CustomerCase = Nothing
            While dr.Read

                customerCase = Me.CreateObject(dr)

            End While

            Return customerCase

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim customerCaseList As ArrayList = New ArrayList

            While dr.Read
                Dim customerCase As CustomerCase = Me.CreateObject(dr)
                customerCaseList.Add(customerCase)
            End While

            Return customerCaseList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerCase As CustomerCase = CType(obj, CustomerCase)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerCase.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerCase As CustomerCase = CType(obj, CustomerCase)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesforceID", DbType.String, customerCase.SalesforceID)
            DbCommandWrapper.AddInParameter("@CaseNumber", DbType.String, customerCase.CaseNumber)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.String, customerCase.CustomerName)
            DbCommandWrapper.AddInParameter("@Phone", DbType.String, customerCase.Phone)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, customerCase.Email)
            DbCommandWrapper.AddInParameter("@Category", DbType.String, customerCase.Category)
            DbCommandWrapper.AddInParameter("@SubCategory1", DbType.String, customerCase.SubCategory1)
            DbCommandWrapper.AddInParameter("@SubCategory2", DbType.String, customerCase.SubCategory2)
            DbCommandWrapper.AddInParameter("@SubCategory3", DbType.String, customerCase.SubCategory3)
            DbCommandWrapper.AddInParameter("@SubCategory4", DbType.String, customerCase.SubCategory4)
            DbCommandWrapper.AddInParameter("@CallerType", DbType.StringFixedLength, customerCase.CallerType)
            DbCommandWrapper.AddInParameter("@CarType", DbType.String, customerCase.CarType)
            DbCommandWrapper.AddInParameter("@Variant", DbType.String, customerCase.Variants)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.String, customerCase.EngineNumber)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.String, customerCase.ChassisNumber)
            DbCommandWrapper.AddInParameter("@Odometer", DbType.Int32, customerCase.Odometer)
            DbCommandWrapper.AddInParameter("@PlateNumber", DbType.String, customerCase.PlateNumber)
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int16, customerCase.Priority)
            DbCommandWrapper.AddInParameter("@CaseNumberReff", DbType.String, customerCase.CaseNumberReff)
            DbCommandWrapper.AddInParameter("@CaseDate", DbType.DateTime, customerCase.CaseDate)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, customerCase.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, customerCase.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, customerCase.Status)
            DbCommandWrapper.AddInParameter("@ReservationNumber", DbType.String, customerCase.ReservationNumber)
            DbCommandWrapper.AddInParameter("@ServiceType", DbType.String, customerCase.ServiceType)
            DbCommandWrapper.AddInParameter("@BookingDatetime", DbType.DateTime, customerCase.BookingDatetime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerCase.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, customerCase.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, customerCase.LastUpdateTIme)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(customerCase.Dealer))

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

            Dim customerCase As CustomerCase = CType(obj, CustomerCase)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerCase.ID)
            DbCommandWrapper.AddInParameter("@SalesforceID", DbType.String, customerCase.SalesforceID)
            DbCommandWrapper.AddInParameter("@CaseNumber", DbType.String, customerCase.CaseNumber)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.String, customerCase.CustomerName)
            DbCommandWrapper.AddInParameter("@Phone", DbType.String, customerCase.Phone)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, customerCase.Email)
            DbCommandWrapper.AddInParameter("@Category", DbType.String, customerCase.Category)
            DbCommandWrapper.AddInParameter("@SubCategory1", DbType.String, customerCase.SubCategory1)
            DbCommandWrapper.AddInParameter("@SubCategory2", DbType.String, customerCase.SubCategory2)
            DbCommandWrapper.AddInParameter("@SubCategory3", DbType.String, customerCase.SubCategory3)
            DbCommandWrapper.AddInParameter("@SubCategory4", DbType.String, customerCase.SubCategory4)
            DbCommandWrapper.AddInParameter("@CallerType", DbType.StringFixedLength, customerCase.CallerType)
            DbCommandWrapper.AddInParameter("@CarType", DbType.String, customerCase.CarType)
            DbCommandWrapper.AddInParameter("@Variant", DbType.String, customerCase.Variants)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.String, customerCase.EngineNumber)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.String, customerCase.ChassisNumber)
            DbCommandWrapper.AddInParameter("@Odometer", DbType.Int32, customerCase.Odometer)
            DbCommandWrapper.AddInParameter("@PlateNumber", DbType.String, customerCase.PlateNumber)
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int16, customerCase.Priority)
            DbCommandWrapper.AddInParameter("@CaseNumberReff", DbType.String, customerCase.CaseNumberReff)
            DbCommandWrapper.AddInParameter("@CaseDate", DbType.DateTime, customerCase.CaseDate)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, customerCase.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, customerCase.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, customerCase.Status)
            DbCommandWrapper.AddInParameter("@ReservationNumber", DbType.String, customerCase.ReservationNumber)
            DbCommandWrapper.AddInParameter("@ServiceType", DbType.String, customerCase.ServiceType)
            DbCommandWrapper.AddInParameter("@BookingDatetime", DbType.DateTime, customerCase.BookingDatetime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerCase.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, customerCase.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, customerCase.LastUpdateTIme)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(customerCase.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CustomerCase

            Dim customerCase As CustomerCase = New CustomerCase

            customerCase.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesforceID")) Then customerCase.SalesforceID = dr("SalesforceID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CaseNumber")) Then customerCase.CaseNumber = dr("CaseNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then customerCase.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then customerCase.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then customerCase.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then customerCase.Category = dr("Category").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory1")) Then customerCase.SubCategory1 = dr("SubCategory1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory2")) Then customerCase.SubCategory2 = dr("SubCategory2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory3")) Then customerCase.SubCategory3 = dr("SubCategory3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory4")) Then customerCase.SubCategory4 = dr("SubCategory4").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CallerType")) Then customerCase.CallerType = dr("CallerType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CarType")) Then customerCase.CarType = dr("CarType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Variant")) Then customerCase.Variants = dr("Variant").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then customerCase.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then customerCase.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Odometer")) Then customerCase.Odometer = CType(dr("Odometer"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PlateNumber")) Then customerCase.PlateNumber = dr("PlateNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Priority")) Then customerCase.Priority = CType(dr("Priority"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CaseNumberReff")) Then customerCase.CaseNumberReff = dr("CaseNumberReff").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CaseDate")) Then customerCase.CaseDate = CType(dr("CaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then customerCase.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then customerCase.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then customerCase.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ReservationNumber")) Then customerCase.ReservationNumber = dr("ReservationNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceType")) Then customerCase.ServiceType = dr("ServiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BookingDatetime")) Then customerCase.BookingDatetime = CType(dr("BookingDatetime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then customerCase.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then customerCase.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then customerCase.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then customerCase.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTIme")) Then customerCase.LastUpdateTIme = CType(dr("LastUpdateTIme"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                customerCase.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return customerCase

        End Function

        Private Sub SetTableName()

            If Not (GetType(CustomerCase) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CustomerCase), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CustomerCase).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

