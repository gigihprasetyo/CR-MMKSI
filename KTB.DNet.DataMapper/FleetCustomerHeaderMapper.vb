#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetCustomerHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/30/2020 - 12:40:56 PM
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

    Public Class FleetCustomerHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleetCustomerHeader"
        Private m_UpdateStatement As String = "up_UpdateFleetCustomerHeader"
        Private m_RetrieveStatement As String = "up_RetrieveFleetCustomerHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetCustomerHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleetCustomerHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fleetCustomerHeader As FleetCustomerHeader = Nothing
            While dr.Read

                fleetCustomerHeader = Me.CreateObject(dr)

            End While

            Return fleetCustomerHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fleetCustomerHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim fleetCustomerHeader As FleetCustomerHeader = Me.CreateObject(dr)
                fleetCustomerHeaderList.Add(fleetCustomerHeader)
            End While

            Return fleetCustomerHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomerHeader As FleetCustomerHeader = CType(obj, FleetCustomerHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomerHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomerHeader As FleetCustomerHeader = CType(obj, FleetCustomerHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FleetCode", DbType.AnsiString, fleetCustomerHeader.FleetCode)
            DbCommandWrapper.AddInParameter("@FleetCustomerType", DbType.Int16, fleetCustomerHeader.FleetCustomerType)
            DbCommandWrapper.AddInParameter("@FleetCompanyCategory", DbType.Int16, fleetCustomerHeader.FleetCompanyCategory)
            DbCommandWrapper.AddInParameter("@FleetCustomerName", DbType.AnsiString, fleetCustomerHeader.FleetCustomerName)
            DbCommandWrapper.AddInParameter("@FleetCustomerGroupCompany", DbType.AnsiString, fleetCustomerHeader.FleetCustomerGroupCompany)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomerHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, fleetCustomerHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, Me.GetRefObject(fleetCustomerHeader.BusinessSectorDetail))

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

            Dim fleetCustomerHeader As FleetCustomerHeader = CType(obj, FleetCustomerHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomerHeader.ID)
            DbCommandWrapper.AddInParameter("@FleetCode", DbType.AnsiString, fleetCustomerHeader.FleetCode)
            DbCommandWrapper.AddInParameter("@FleetCustomerType", DbType.Int16, fleetCustomerHeader.FleetCustomerType)
            DbCommandWrapper.AddInParameter("@FleetCompanyCategory", DbType.Int16, fleetCustomerHeader.FleetCompanyCategory)
            DbCommandWrapper.AddInParameter("@FleetCustomerName", DbType.AnsiString, fleetCustomerHeader.FleetCustomerName)
            DbCommandWrapper.AddInParameter("@FleetCustomerGroupCompany", DbType.AnsiString, fleetCustomerHeader.FleetCustomerGroupCompany)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomerHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fleetCustomerHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, Me.GetRefObject(fleetCustomerHeader.BusinessSectorDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetCustomerHeader

            Dim fleetCustomerHeader As FleetCustomerHeader = New FleetCustomerHeader

            fleetCustomerHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCode")) Then fleetCustomerHeader.FleetCode = dr("FleetCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCustomerType")) Then fleetCustomerHeader.FleetCustomerType = CType(dr("FleetCustomerType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCompanyCategory")) Then fleetCustomerHeader.FleetCompanyCategory = CType(dr("FleetCompanyCategory"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCustomerName")) Then fleetCustomerHeader.FleetCustomerName = dr("FleetCustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCustomerGroupCompany")) Then fleetCustomerHeader.FleetCustomerGroupCompany = dr("FleetCustomerGroupCompany").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fleetCustomerHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fleetCustomerHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fleetCustomerHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fleetCustomerHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fleetCustomerHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorDetailID")) Then
                fleetCustomerHeader.BusinessSectorDetail = New BusinessSectorDetail(CType(dr("BusinessSectorDetailID"), Integer))
            End If

            Return fleetCustomerHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetCustomerHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetCustomerHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetCustomerHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
