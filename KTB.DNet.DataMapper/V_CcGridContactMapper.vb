
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_CcGridContact Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 6/19/2013 - 4:10:36 PM
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

    Public Class V_CcGridContactMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_CcGridContact"
        Private m_UpdateStatement As String = "up_UpdateV_CcGridContact"
        Private m_RetrieveStatement As String = "up_RetrieveV_CcGridContact"
        Private m_RetrieveListStatement As String = "up_RetrieveV_CcGridContactList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_CcGridContact"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim _v_CcGridContact As V_CcGridContact = Nothing
            While dr.Read

                _v_CcGridContact = Me.CreateObject(dr)

            End While

            Return _v_CcGridContact

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_CcGridContactList As ArrayList = New ArrayList

            While dr.Read
                Dim _v_CcGridContact As V_CcGridContact = Me.CreateObject(dr)
                v_CcGridContactList.Add(_v_CcGridContact)
            End While

            Return v_CcGridContactList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim _v_CcGridContact As V_CcGridContact = CType(obj, V_CcGridContact)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int64, _v_CcGridContact.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim _v_CcGridContact As V_CcGridContact = CType(obj, V_CcGridContact)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DBCommandWrapper.AddInParameter("@CcPeriodID", DbType.Int32, _v_CcGridContact.CcPeriodID)
            DBCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, _v_CcGridContact.CcCustomerCategoryID)
            DBCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, _v_CcGridContact.CcVehicleCategoryID)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, _v_CcGridContact.DealerID)
            DBCommandWrapper.AddInParameter("@NonAuthorizedDealerID", DbType.Int32, _v_CcGridContact.NonAuthorizedDealerID)
            DBCommandWrapper.AddInParameter("@Sex", DbType.AnsiString, _v_CcGridContact.Sex)
            DBCommandWrapper.AddInParameter("@ConsumerName", DbType.AnsiString, _v_CcGridContact.ConsumerName)
            DBCommandWrapper.AddInParameter("@HandphoneNo", DbType.AnsiString, _v_CcGridContact.HandphoneNo)
            DBCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, _v_CcGridContact.VehicleType)
            DBCommandWrapper.AddInParameter("@NameSTNK", DbType.AnsiString, _v_CcGridContact.NameSTNK)
            DBCommandWrapper.AddInParameter("@AddressSTNK", DbType.AnsiString, _v_CcGridContact.AddressSTNK)
            DBCommandWrapper.AddInParameter("@City", DbType.AnsiString, _v_CcGridContact.City)
            DBCommandWrapper.AddInParameter("@ChassisNo", DbType.AnsiString, _v_CcGridContact.ChassisNo)
            DBCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, _v_CcGridContact.TransactionDate)
            DBCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, _v_CcGridContact.CustomerID)
            DBCommandWrapper.AddInParameter("@Odometer", DbType.AnsiString, _v_CcGridContact.Odometer)
            DBCommandWrapper.AddInParameter("@HomePhoneAreaCode", DbType.AnsiString, _v_CcGridContact.HomePhoneAreaCode)
            DBCommandWrapper.AddInParameter("@HomePhoneNo", DbType.AnsiString, _v_CcGridContact.HomePhoneNo)
            DBCommandWrapper.AddInParameter("@OfficePhoneAreaCode", DbType.AnsiString, _v_CcGridContact.OfficePhoneAreaCode)
            DBCommandWrapper.AddInParameter("@OfficePhoneNo", DbType.AnsiString, _v_CcGridContact.OfficePhoneNo)
            DBCommandWrapper.AddInParameter("@OfficePhoneNoExt", DbType.AnsiString, _v_CcGridContact.OfficePhoneNoExt)
            DBCommandWrapper.AddInParameter("@ServiceType", DbType.AnsiString, _v_CcGridContact.ServiceType)
            DBCommandWrapper.AddInParameter("@CcContactStatusID", DbType.Int16, _v_CcGridContact.CcContactStatusID)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, _v_CcGridContact.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, _v_CcGridContact.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, _v_CcGridContact.DealerName)
            DBCommandWrapper.AddInParameter("@DealerCityName", DbType.AnsiString, _v_CcGridContact.DealerCityName)
            DBCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, _v_CcGridContact.DealerGroupID)
            DBCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, _v_CcGridContact.Area1ID)
            DBCommandWrapper.AddInParameter("@CustomerCategory", DbType.AnsiString, _v_CcGridContact.CustomerCategory)
            DBCommandWrapper.AddInParameter("@CustomerCategoryCode", DbType.AnsiString, _v_CcGridContact.CustomerCategoryCode)
            DBCommandWrapper.AddInParameter("@YearMonth", DbType.AnsiString, _v_CcGridContact.YearMonth)
            DBCommandWrapper.AddInParameter("@VehicleCategoryCode", DbType.AnsiString, _v_CcGridContact.VehicleCategoryCode)
            DBCommandWrapper.AddInParameter("@VehicleCategory", DbType.AnsiString, _v_CcGridContact.VehicleCategory)
            DBCommandWrapper.AddInParameter("@Gelar", DbType.AnsiString, _v_CcGridContact.Gelar)
            DBCommandWrapper.AddInParameter("@Area", DbType.AnsiString, _v_CcGridContact.Area)
            DBCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, _v_CcGridContact.GroupName)
            DBCommandWrapper.AddInParameter("@StatusDescription", DbType.AnsiString, _v_CcGridContact.StatusDescription)
            DBCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, _v_CcGridContact.ProvinceName)
            DBCommandWrapper.AddInParameter("@DealerAddress", DbType.AnsiString, _v_CcGridContact.DealerAddress)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, _v_CcGridContact.DealerCode)


            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim _v_CcGridContact As V_CcGridContact = CType(obj, V_CcGridContact)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int64, _v_CcGridContact.ID)
            DBCommandWrapper.AddInParameter("@CcPeriodID", DbType.Int32, _v_CcGridContact.CcPeriodID)
            DBCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, _v_CcGridContact.CcCustomerCategoryID)
            DBCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, _v_CcGridContact.CcVehicleCategoryID)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, _v_CcGridContact.DealerID)
            DBCommandWrapper.AddInParameter("@NonAuthorizedDealerID", DbType.Int32, _v_CcGridContact.NonAuthorizedDealerID)
            DBCommandWrapper.AddInParameter("@Sex", DbType.AnsiString, _v_CcGridContact.Sex)
            DBCommandWrapper.AddInParameter("@ConsumerName", DbType.AnsiString, _v_CcGridContact.ConsumerName)
            DBCommandWrapper.AddInParameter("@HandphoneNo", DbType.AnsiString, _v_CcGridContact.HandphoneNo)
            DBCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, _v_CcGridContact.VehicleType)
            DBCommandWrapper.AddInParameter("@NameSTNK", DbType.AnsiString, _v_CcGridContact.NameSTNK)
            DBCommandWrapper.AddInParameter("@AddressSTNK", DbType.AnsiString, _v_CcGridContact.AddressSTNK)
            DBCommandWrapper.AddInParameter("@City", DbType.AnsiString, _v_CcGridContact.City)
            DBCommandWrapper.AddInParameter("@ChassisNo", DbType.AnsiString, _v_CcGridContact.ChassisNo)
            DBCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, _v_CcGridContact.TransactionDate)
            DBCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, _v_CcGridContact.CustomerID)
            DBCommandWrapper.AddInParameter("@Odometer", DbType.AnsiString, _v_CcGridContact.Odometer)
            DBCommandWrapper.AddInParameter("@HomePhoneAreaCode", DbType.AnsiString, _v_CcGridContact.HomePhoneAreaCode)
            DBCommandWrapper.AddInParameter("@HomePhoneNo", DbType.AnsiString, _v_CcGridContact.HomePhoneNo)
            DBCommandWrapper.AddInParameter("@OfficePhoneAreaCode", DbType.AnsiString, _v_CcGridContact.OfficePhoneAreaCode)
            DBCommandWrapper.AddInParameter("@OfficePhoneNo", DbType.AnsiString, _v_CcGridContact.OfficePhoneNo)
            DBCommandWrapper.AddInParameter("@OfficePhoneNoExt", DbType.AnsiString, _v_CcGridContact.OfficePhoneNoExt)
            DBCommandWrapper.AddInParameter("@ServiceType", DbType.AnsiString, _v_CcGridContact.ServiceType)
            DBCommandWrapper.AddInParameter("@CcContactStatusID", DbType.Int16, _v_CcGridContact.CcContactStatusID)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, _v_CcGridContact.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, _v_CcGridContact.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, _v_CcGridContact.DealerName)
            DBCommandWrapper.AddInParameter("@DealerCityName", DbType.AnsiString, _v_CcGridContact.DealerCityName)
            DBCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, _v_CcGridContact.DealerGroupID)
            DBCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, _v_CcGridContact.Area1ID)
            DBCommandWrapper.AddInParameter("@CustomerCategory", DbType.AnsiString, _v_CcGridContact.CustomerCategory)
            DBCommandWrapper.AddInParameter("@CustomerCategoryCode", DbType.AnsiString, _v_CcGridContact.CustomerCategoryCode)
            DBCommandWrapper.AddInParameter("@YearMonth", DbType.AnsiString, _v_CcGridContact.YearMonth)
            DBCommandWrapper.AddInParameter("@VehicleCategoryCode", DbType.AnsiString, _v_CcGridContact.VehicleCategoryCode)
            DBCommandWrapper.AddInParameter("@VehicleCategory", DbType.AnsiString, _v_CcGridContact.VehicleCategory)
            DBCommandWrapper.AddInParameter("@Gelar", DbType.AnsiString, _v_CcGridContact.Gelar)
            DBCommandWrapper.AddInParameter("@Area", DbType.AnsiString, _v_CcGridContact.Area)
            DBCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, _v_CcGridContact.GroupName)
            DBCommandWrapper.AddInParameter("@StatusDescription", DbType.AnsiString, _v_CcGridContact.StatusDescription)
            DBCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, _v_CcGridContact.ProvinceName)
            DBCommandWrapper.AddInParameter("@DealerAddress", DbType.AnsiString, _v_CcGridContact.DealerAddress)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, _v_CcGridContact.DealerCode)



            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_CcGridContact

            Dim _v_CcGridContact As V_CcGridContact = New V_CcGridContact

            _v_CcGridContact.ID = CType(dr("ID"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodID")) Then _v_CcGridContact.CcPeriodID = CType(dr("CcPeriodID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then _v_CcGridContact.CcCustomerCategoryID = CType(dr("CcCustomerCategoryID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CcVehicleCategoryID")) Then _v_CcGridContact.CcVehicleCategoryID = CType(dr("CcVehicleCategoryID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then _v_CcGridContact.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("NonAuthorizedDealerID")) Then _v_CcGridContact.NonAuthorizedDealerID = CType(dr("NonAuthorizedDealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Sex")) Then _v_CcGridContact.Sex = dr("Sex").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumerName")) Then _v_CcGridContact.ConsumerName = dr("ConsumerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HandphoneNo")) Then _v_CcGridContact.HandphoneNo = dr("HandphoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then _v_CcGridContact.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NameSTNK")) Then _v_CcGridContact.NameSTNK = dr("NameSTNK").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AddressSTNK")) Then _v_CcGridContact.AddressSTNK = dr("AddressSTNK").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then _v_CcGridContact.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNo")) Then _v_CcGridContact.ChassisNo = dr("ChassisNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then _v_CcGridContact.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerID")) Then _v_CcGridContact.CustomerID = CType(dr("CustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Odometer")) Then _v_CcGridContact.Odometer = dr("Odometer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HomePhoneAreaCode")) Then _v_CcGridContact.HomePhoneAreaCode = dr("HomePhoneAreaCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HomePhoneNo")) Then _v_CcGridContact.HomePhoneNo = dr("HomePhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficePhoneAreaCode")) Then _v_CcGridContact.OfficePhoneAreaCode = dr("OfficePhoneAreaCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficePhoneNo")) Then _v_CcGridContact.OfficePhoneNo = dr("OfficePhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficePhoneNoExt")) Then _v_CcGridContact.OfficePhoneNoExt = dr("OfficePhoneNoExt").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceType")) Then _v_CcGridContact.ServiceType = dr("ServiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CcContactStatusID")) Then _v_CcGridContact.CcContactStatusID = CType(dr("CcContactStatusID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then _v_CcGridContact.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then _v_CcGridContact.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then _v_CcGridContact.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then _v_CcGridContact.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then _v_CcGridContact.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then _v_CcGridContact.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCityName")) Then _v_CcGridContact.DealerCityName = dr("DealerCityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerGroupID")) Then _v_CcGridContact.DealerGroupID = CType(dr("DealerGroupID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Area1ID")) Then _v_CcGridContact.Area1ID = CType(dr("Area1ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCategory")) Then _v_CcGridContact.CustomerCategory = dr("CustomerCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCategoryCode")) Then _v_CcGridContact.CustomerCategoryCode = dr("CustomerCategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("YearMonth")) Then _v_CcGridContact.YearMonth = dr("YearMonth").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleCategoryCode")) Then _v_CcGridContact.VehicleCategoryCode = dr("VehicleCategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleCategory")) Then _v_CcGridContact.VehicleCategory = dr("VehicleCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Gelar")) Then _v_CcGridContact.Gelar = dr("Gelar").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Area")) Then _v_CcGridContact.Area = dr("Area").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GroupName")) Then _v_CcGridContact.GroupName = dr("GroupName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDescription")) Then _v_CcGridContact.StatusDescription = dr("StatusDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) Then _v_CcGridContact.ProvinceName = dr("ProvinceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerAddress")) Then _v_CcGridContact.DealerAddress = dr("DealerAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then _v_CcGridContact.DealerCode = dr("DealerCode").ToString

            Return _v_CcGridContact

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_CcGridContact) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_CcGridContact), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_CcGridContact).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

