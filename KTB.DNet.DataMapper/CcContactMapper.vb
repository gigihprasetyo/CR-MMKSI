
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcContact Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 6/19/2013 - 2:50:30 PM
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

    Public Class CcContactMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcContact"
        Private m_UpdateStatement As String = "up_UpdateCcContact"
        Private m_RetrieveStatement As String = "up_RetrieveCcContact"
        Private m_RetrieveListStatement As String = "up_RetrieveCcContactList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcContact"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccContact As CcContact = Nothing
            While dr.Read

                ccContact = Me.CreateObject(dr)

            End While

            Return ccContact

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccContactList As ArrayList = New ArrayList

            While dr.Read
                Dim ccContact As CcContact = Me.CreateObject(dr)
                ccContactList.Add(ccContact)
            End While

            Return ccContactList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccContact As CcContact = CType(obj, CcContact)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, ccContact.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccContact As CcContact = CType(obj, CcContact)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            'DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, ccContact.DealerID)
            DBCommandWrapper.AddInParameter("@DealerId", DbType.Int32, Me.GetRefObject(ccContact.Dealer))
            DbCommandWrapper.AddInParameter("@NonAuthorizedDealerID", DbType.Int32, ccContact.NonAuthorizedDealerID)
            DbCommandWrapper.AddInParameter("@Sex", DbType.AnsiString, ccContact.Sex)
            DbCommandWrapper.AddInParameter("@ConsumerName", DbType.AnsiString, ccContact.ConsumerName)
            DbCommandWrapper.AddInParameter("@HandphoneNo", DbType.AnsiString, ccContact.HandphoneNo)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, ccContact.VehicleType)
            DbCommandWrapper.AddInParameter("@NameSTNK", DbType.AnsiString, ccContact.NameSTNK)
            DbCommandWrapper.AddInParameter("@AddressSTNK", DbType.AnsiString, ccContact.AddressSTNK)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, ccContact.City)
            DbCommandWrapper.AddInParameter("@ChassisNo", DbType.AnsiString, ccContact.ChassisNo)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, ccContact.TransactionDate)
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, ccContact.CustomerID)
            DbCommandWrapper.AddInParameter("@Odometer", DbType.AnsiString, ccContact.Odometer)
            DbCommandWrapper.AddInParameter("@HomePhoneAreaCode", DbType.AnsiString, ccContact.HomePhoneAreaCode)
            DbCommandWrapper.AddInParameter("@HomePhoneNo", DbType.AnsiString, ccContact.HomePhoneNo)
            DbCommandWrapper.AddInParameter("@OfficePhoneAreaCode", DbType.AnsiString, ccContact.OfficePhoneAreaCode)
            DbCommandWrapper.AddInParameter("@OfficePhoneNo", DbType.AnsiString, ccContact.OfficePhoneNo)
            DbCommandWrapper.AddInParameter("@OfficePhoneNoExt", DbType.AnsiString, ccContact.OfficePhoneNoExt)
            DbCommandWrapper.AddInParameter("@ServiceType", DbType.AnsiString, ccContact.ServiceType)
            DbCommandWrapper.AddInParameter("@CcContactStatusID", DbType.Int16, ccContact.CcContactStatusID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccContact.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccContact.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, ccContact.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@CcPeriodID", DbType.Int32, ccContact.CcPeriodID)
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, ccContact.CcVehicleCategoryID)

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

            Dim ccContact As CcContact = CType(obj, CcContact)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, ccContact.ID)
            'DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, ccContact.DealerID)
            DBCommandWrapper.AddInParameter("@DealerId", DbType.Int32, Me.GetRefObject(ccContact.Dealer))
            DbCommandWrapper.AddInParameter("@NonAuthorizedDealerID", DbType.Int32, ccContact.NonAuthorizedDealerID)
            DbCommandWrapper.AddInParameter("@Sex", DbType.AnsiString, ccContact.Sex)
            DbCommandWrapper.AddInParameter("@ConsumerName", DbType.AnsiString, ccContact.ConsumerName)
            DbCommandWrapper.AddInParameter("@HandphoneNo", DbType.AnsiString, ccContact.HandphoneNo)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, ccContact.VehicleType)
            DbCommandWrapper.AddInParameter("@NameSTNK", DbType.AnsiString, ccContact.NameSTNK)
            DbCommandWrapper.AddInParameter("@AddressSTNK", DbType.AnsiString, ccContact.AddressSTNK)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, ccContact.City)
            DbCommandWrapper.AddInParameter("@ChassisNo", DbType.AnsiString, ccContact.ChassisNo)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, ccContact.TransactionDate)
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, ccContact.CustomerID)
            DbCommandWrapper.AddInParameter("@Odometer", DbType.AnsiString, ccContact.Odometer)
            DbCommandWrapper.AddInParameter("@HomePhoneAreaCode", DbType.AnsiString, ccContact.HomePhoneAreaCode)
            DbCommandWrapper.AddInParameter("@HomePhoneNo", DbType.AnsiString, ccContact.HomePhoneNo)
            DbCommandWrapper.AddInParameter("@OfficePhoneAreaCode", DbType.AnsiString, ccContact.OfficePhoneAreaCode)
            DbCommandWrapper.AddInParameter("@OfficePhoneNo", DbType.AnsiString, ccContact.OfficePhoneNo)
            DbCommandWrapper.AddInParameter("@OfficePhoneNoExt", DbType.AnsiString, ccContact.OfficePhoneNoExt)
            DbCommandWrapper.AddInParameter("@ServiceType", DbType.AnsiString, ccContact.ServiceType)
            DbCommandWrapper.AddInParameter("@CcContactStatusID", DbType.Int16, ccContact.CcContactStatusID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccContact.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccContact.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, ccContact.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@CcPeriodID", DbType.Int32, ccContact.CcPeriodID)
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, ccContact.CcVehicleCategoryID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcContact

            Dim ccContact As CcContact = New CcContact

            ccContact.ID = CType(dr("ID"), Long)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then ccContact.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("NonAuthorizedDealerID")) Then ccContact.NonAuthorizedDealerID = CType(dr("NonAuthorizedDealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Sex")) Then ccContact.Sex = dr("Sex").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumerName")) Then ccContact.ConsumerName = dr("ConsumerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HandphoneNo")) Then ccContact.HandphoneNo = dr("HandphoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then ccContact.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NameSTNK")) Then ccContact.NameSTNK = dr("NameSTNK").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AddressSTNK")) Then ccContact.AddressSTNK = dr("AddressSTNK").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then ccContact.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNo")) Then ccContact.ChassisNo = dr("ChassisNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then ccContact.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerID")) Then ccContact.CustomerID = CType(dr("CustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Odometer")) Then ccContact.Odometer = dr("Odometer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HomePhoneAreaCode")) Then ccContact.HomePhoneAreaCode = dr("HomePhoneAreaCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HomePhoneNo")) Then ccContact.HomePhoneNo = dr("HomePhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficePhoneAreaCode")) Then ccContact.OfficePhoneAreaCode = dr("OfficePhoneAreaCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficePhoneNo")) Then ccContact.OfficePhoneNo = dr("OfficePhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficePhoneNoExt")) Then ccContact.OfficePhoneNoExt = dr("OfficePhoneNoExt").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceType")) Then ccContact.ServiceType = dr("ServiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CcContactStatusID")) Then ccContact.CcContactStatusID = CType(dr("CcContactStatusID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccContact.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccContact.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccContact.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccContact.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccContact.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then
                ccContact.CcCustomerCategoryID = CType(dr("CcCustomerCategoryID"), Short)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodID")) Then
                ccContact.CcPeriodID = CType(dr("CcPeriodID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcVehicleCategoryID")) Then
                ccContact.CcVehicleCategoryID = CType(dr("CcVehicleCategoryID"), Short)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then
                ccContact.Dealer = New Dealer(CType(dr("DealerId"), Integer))
            End If
            Return ccContact

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcContact) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcContact), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcContact).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

