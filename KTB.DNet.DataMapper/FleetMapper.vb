
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Fleet Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/02/2018 - 15:26:30
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

    Public Class FleetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleet"
        Private m_UpdateStatement As String = "up_UpdateFleet"
        Private m_RetrieveStatement As String = "up_RetrieveFleet"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleet"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fleet As Fleet = Nothing
            While dr.Read

                fleet = Me.CreateObject(dr)

            End While

            Return fleet

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fleetList As ArrayList = New ArrayList

            While dr.Read
                Dim fleet As Fleet = Me.CreateObject(dr)
                fleetList.Add(fleet)
            End While

            Return fleetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleet As Fleet = CType(obj, Fleet)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleet.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleet As Fleet = CType(obj, Fleet)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FleetCode", DbType.AnsiString, fleet.FleetCode)
            DbCommandWrapper.AddInParameter("@FleetName", DbType.AnsiString, fleet.FleetName)
            DbCommandWrapper.AddInParameter("@FleetNickName", DbType.AnsiString, fleet.FleetNickName)
            DbCommandWrapper.AddInParameter("@FleetGroup", DbType.AnsiString, fleet.FleetGroup)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, fleet.Address)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int32, fleet.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, fleet.IdentityNumber)
            DbCommandWrapper.AddInParameter("@FleetNote", DbType.AnsiString, fleet.FleetNote)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleet.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, fleet.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityId", DbType.Int16, Me.GetRefObject(fleet.City))
            DbCommandWrapper.AddInParameter("@ProvinceId", DbType.Int32, Me.GetRefObject(fleet.Province))
            DbCommandWrapper.AddInParameter("@BusinessSectorHeaderId", DbType.Int32, Me.GetRefObject(fleet.BusinessSectorHeader))
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailId", DbType.Int32, Me.GetRefObject(fleet.BusinessSectorDetail))

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

            Dim fleet As Fleet = CType(obj, Fleet)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleet.ID)
            DbCommandWrapper.AddInParameter("@FleetCode", DbType.AnsiString, fleet.FleetCode)
            DbCommandWrapper.AddInParameter("@FleetName", DbType.AnsiString, fleet.FleetName)
            DbCommandWrapper.AddInParameter("@FleetNickName", DbType.AnsiString, fleet.FleetNickName)
            DbCommandWrapper.AddInParameter("@FleetGroup", DbType.AnsiString, fleet.FleetGroup)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, fleet.Address)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int32, fleet.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, fleet.IdentityNumber)
            DbCommandWrapper.AddInParameter("@FleetNote", DbType.AnsiString, fleet.FleetNote)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleet.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fleet.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CityId", DbType.Int16, Me.GetRefObject(fleet.City))
            DbCommandWrapper.AddInParameter("@ProvinceId", DbType.Int32, Me.GetRefObject(fleet.Province))
            DbCommandWrapper.AddInParameter("@BusinessSectorHeaderId", DbType.Int32, Me.GetRefObject(fleet.BusinessSectorHeader))
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailId", DbType.Int32, Me.GetRefObject(fleet.BusinessSectorDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Fleet

            Dim fleet As Fleet = New Fleet

            fleet.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCode")) Then fleet.FleetCode = dr("FleetCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetName")) Then fleet.FleetName = dr("FleetName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetNickName")) Then fleet.FleetNickName = dr("FleetNickName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetGroup")) Then fleet.FleetGroup = dr("FleetGroup").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then fleet.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityType")) Then fleet.IdentityType = CType(dr("IdentityType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityNumber")) Then fleet.IdentityNumber = dr("IdentityNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetNote")) Then fleet.FleetNote = dr("FleetNote").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fleet.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fleet.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fleet.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fleet.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fleet.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityId")) Then
                fleet.City = New City(CType(dr("CityId"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceId")) Then
                fleet.Province = New Province(CType(dr("ProvinceId"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorHeaderId")) Then
                fleet.BusinessSectorHeader = New BusinessSectorHeader(CType(dr("BusinessSectorHeaderId"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorDetailId")) Then
                fleet.BusinessSectorDetail = New BusinessSectorDetail(CType(dr("BusinessSectorDetailId"), Integer))
            End If

            Return fleet

        End Function

        Private Sub SetTableName()

            If Not (GetType(Fleet) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Fleet), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Fleet).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

