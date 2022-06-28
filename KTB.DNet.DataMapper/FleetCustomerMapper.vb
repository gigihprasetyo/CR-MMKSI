
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : FleetCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 06/06/2018 - 13:28:12
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

    Public Class FleetCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleetCustomer"
        Private m_UpdateStatement As String = "up_UpdateFleetCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveFleetCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleetCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fleetCustomer As FleetCustomer = Nothing
            While dr.Read

                fleetCustomer = Me.CreateObject(dr)

            End While

            Return fleetCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fleetCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim fleetCustomer As FleetCustomer = Me.CreateObject(dr)
                fleetCustomerList.Add(fleetCustomer)
            End While

            Return fleetCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomer As FleetCustomer = CType(obj, FleetCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomer As FleetCustomer = CType(obj, FleetCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CustomerGroupID", DbType.Int32, Me.GetRefObject(fleetCustomer.CustomerGroup))
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, fleetCustomer.ProvinceID)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, fleetCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(fleetCustomer.City))
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailId", DbType.Int32, fleetCustomer.BusinessSectorDetailId)
            DbCommandWrapper.AddInParameter("@RatioMatrixID", DbType.Int32, fleetCustomer.RatioMatrixID)
            DbCommandWrapper.AddInParameter("@CategoryIndex", DbType.Int32, fleetCustomer.CategoryIndex)
            DbCommandWrapper.AddInParameter("@TypeIndex", DbType.Int32, fleetCustomer.TypeIndex)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, fleetCustomer.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, fleetCustomer.Name)
            DbCommandWrapper.AddInParameter("@Gedung", DbType.AnsiString, fleetCustomer.Gedung)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, fleetCustomer.Alamat)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, fleetCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, fleetCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, fleetCustomer.PostalCode)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, fleetCustomer.Email)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, fleetCustomer.PhoneNo)
            DbCommandWrapper.AddInParameter("@TipeCustomer", DbType.Int32, fleetCustomer.TipeCustomer)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int32, fleetCustomer.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, fleetCustomer.IdentityNumber)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, fleetCustomer.Attachment)
            DbCommandWrapper.AddInParameter("@ClassificationIndex", DbType.Int32, fleetCustomer.ClassificationIndex)
            DbCommandWrapper.AddInParameter("@FleetNickName", DbType.AnsiString, fleetCustomer.FleetNickName)
            DbCommandWrapper.AddInParameter("@FleetNote", DbType.AnsiString, fleetCustomer.FleetNote)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, fleetCustomer.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, fleetCustomer.LastUpdatedTime)


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

            Dim fleetCustomer As FleetCustomer = CType(obj, FleetCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomer.ID)
            DbCommandWrapper.AddInParameter("@CustomerGroupID", DbType.Int32, Me.GetRefObject(fleetCustomer.CustomerGroup))
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, fleetCustomer.ProvinceID)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, fleetCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(fleetCustomer.City))
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailId", DbType.Int32, fleetCustomer.BusinessSectorDetailId)
            DbCommandWrapper.AddInParameter("@RatioMatrixID", DbType.Int32, fleetCustomer.RatioMatrixID)
            DbCommandWrapper.AddInParameter("@CategoryIndex", DbType.Int32, fleetCustomer.CategoryIndex)
            DbCommandWrapper.AddInParameter("@TypeIndex", DbType.Int32, fleetCustomer.TypeIndex)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, fleetCustomer.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, fleetCustomer.Name)
            DbCommandWrapper.AddInParameter("@Gedung", DbType.AnsiString, fleetCustomer.Gedung)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, fleetCustomer.Alamat)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, fleetCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, fleetCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, fleetCustomer.PostalCode)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, fleetCustomer.Email)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, fleetCustomer.PhoneNo)
            DbCommandWrapper.AddInParameter("@TipeCustomer", DbType.Int32, fleetCustomer.TipeCustomer)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int32, fleetCustomer.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, fleetCustomer.IdentityNumber)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, fleetCustomer.Attachment)
            DbCommandWrapper.AddInParameter("@ClassificationIndex", DbType.Int32, fleetCustomer.ClassificationIndex)
            DbCommandWrapper.AddInParameter("@FleetNickName", DbType.AnsiString, fleetCustomer.FleetNickName)
            DbCommandWrapper.AddInParameter("@FleetNote", DbType.AnsiString, fleetCustomer.FleetNote)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fleetCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, fleetCustomer.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetCustomer

            Dim fleetCustomer As FleetCustomer = New FleetCustomer

            fleetCustomer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceID")) Then fleetCustomer.ProvinceID = CType(dr("ProvinceID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PreArea")) Then fleetCustomer.PreArea = dr("PreArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorDetailId")) Then fleetCustomer.BusinessSectorDetailId = CType(dr("BusinessSectorDetailId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RatioMatrixID")) Then fleetCustomer.RatioMatrixID = CType(dr("RatioMatrixID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryIndex")) Then fleetCustomer.CategoryIndex = CType(dr("CategoryIndex"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TypeIndex")) Then fleetCustomer.TypeIndex = CType(dr("TypeIndex"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then fleetCustomer.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then fleetCustomer.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Gedung")) Then fleetCustomer.Gedung = dr("Gedung").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then fleetCustomer.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then fleetCustomer.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then fleetCustomer.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then fleetCustomer.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then fleetCustomer.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then fleetCustomer.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TipeCustomer")) Then fleetCustomer.TipeCustomer = CType(dr("TipeCustomer"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityType")) Then fleetCustomer.IdentityType = CType(dr("IdentityType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityNumber")) Then fleetCustomer.IdentityNumber = dr("IdentityNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then fleetCustomer.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassificationIndex")) Then fleetCustomer.ClassificationIndex = CType(dr("ClassificationIndex"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetNickName")) Then fleetCustomer.FleetNickName = dr("FleetNickName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetNote")) Then fleetCustomer.FleetNote = dr("FleetNote").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fleetCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fleetCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fleetCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then fleetCustomer.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then fleetCustomer.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)


            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                fleetCustomer.City = New City(CType(dr("CityID"), Short))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CustomerGroupID")) Then
                fleetCustomer.CustomerGroup = New CustomerGroup(CType(dr("CustomerGroupID"), Integer))
            End If

            Return fleetCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

