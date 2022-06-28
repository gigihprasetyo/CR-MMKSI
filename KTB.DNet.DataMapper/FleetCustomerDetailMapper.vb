#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetCustomerDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/30/2020 - 12:43:13 PM
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

    Public Class FleetCustomerDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleetCustomerDetail"
        Private m_UpdateStatement As String = "up_UpdateFleetCustomerDetail"
        Private m_RetrieveStatement As String = "up_RetrieveFleetCustomerDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetCustomerDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleetCustomerDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fleetCustomerDetail As FleetCustomerDetail = Nothing
            While dr.Read

                fleetCustomerDetail = Me.CreateObject(dr)

            End While

            Return fleetCustomerDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fleetCustomerDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim fleetCustomerDetail As FleetCustomerDetail = Me.CreateObject(dr)
                fleetCustomerDetailList.Add(fleetCustomerDetail)
            End While

            Return fleetCustomerDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomerDetail As FleetCustomerDetail = CType(obj, FleetCustomerDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomerDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomerDetail As FleetCustomerDetail = CType(obj, FleetCustomerDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FleetDetailCode", DbType.AnsiString, fleetCustomerDetail.FleetDetailCode)
            DbCommandWrapper.AddInParameter("@FleetStatus", DbType.Int16, fleetCustomerDetail.FleetStatus)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, fleetCustomerDetail.Address)
            DbCommandWrapper.AddInParameter("@SubDistrict", DbType.AnsiString, fleetCustomerDetail.SubDistrict)
            DbCommandWrapper.AddInParameter("@Village", DbType.AnsiString, fleetCustomerDetail.Village)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, fleetCustomerDetail.PostalCode)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int16, fleetCustomerDetail.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, fleetCustomerDetail.IdentityNumber)
            DbCommandWrapper.AddInParameter("@NPWPNo", DbType.AnsiString, fleetCustomerDetail.NPWPNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomerDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, fleetCustomerDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(fleetCustomerDetail.City))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(fleetCustomerDetail.Dealer))
            DbCommandWrapper.AddInParameter("@FleetCustomerHeaderID", DbType.Int32, Me.GetRefObject(fleetCustomerDetail.FleetCustomerHeader))

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

            Dim fleetCustomerDetail As FleetCustomerDetail = CType(obj, FleetCustomerDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomerDetail.ID)
            DbCommandWrapper.AddInParameter("@FleetDetailCode", DbType.AnsiString, fleetCustomerDetail.FleetDetailCode)
            DbCommandWrapper.AddInParameter("@FleetStatus", DbType.Int16, fleetCustomerDetail.FleetStatus)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, fleetCustomerDetail.Address)
            DbCommandWrapper.AddInParameter("@SubDistrict", DbType.AnsiString, fleetCustomerDetail.SubDistrict)
            DbCommandWrapper.AddInParameter("@Village", DbType.AnsiString, fleetCustomerDetail.Village)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, fleetCustomerDetail.PostalCode)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int16, fleetCustomerDetail.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, fleetCustomerDetail.IdentityNumber)
            DbCommandWrapper.AddInParameter("@NPWPNo", DbType.AnsiString, fleetCustomerDetail.NPWPNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomerDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fleetCustomerDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(fleetCustomerDetail.City))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(fleetCustomerDetail.Dealer))
            DbCommandWrapper.AddInParameter("@FleetCustomerHeaderID", DbType.Int32, Me.GetRefObject(fleetCustomerDetail.FleetCustomerHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetCustomerDetail

            Dim fleetCustomerDetail As FleetCustomerDetail = New FleetCustomerDetail

            fleetCustomerDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetDetailCode")) Then fleetCustomerDetail.FleetDetailCode = dr("FleetDetailCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetStatus")) Then fleetCustomerDetail.FleetStatus = CType(dr("FleetStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then fleetCustomerDetail.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubDistrict")) Then fleetCustomerDetail.SubDistrict = dr("SubDistrict").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Village")) Then fleetCustomerDetail.Village = dr("Village").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then fleetCustomerDetail.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityType")) Then fleetCustomerDetail.IdentityType = CType(dr("IdentityType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityNumber")) Then fleetCustomerDetail.IdentityNumber = dr("IdentityNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NPWPNo")) Then fleetCustomerDetail.NPWPNo = dr("NPWPNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fleetCustomerDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fleetCustomerDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fleetCustomerDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fleetCustomerDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fleetCustomerDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                fleetCustomerDetail.City = New City(CType(dr("CityID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                fleetCustomerDetail.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCustomerHeaderID")) Then
                fleetCustomerDetail.FleetCustomerHeader = New FleetCustomerHeader(CType(dr("FleetCustomerHeaderID"), Integer))
            End If

            Return fleetCustomerDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetCustomerDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetCustomerDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetCustomerDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
