
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetCustomerToCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 9/22/2017 - 10:04:16 AM
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

    Public Class FleetCustomerToCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleetCustomerToCustomer"
        Private m_UpdateStatement As String = "up_UpdateFleetCustomerToCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveFleetCustomerToCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetCustomerToCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleetCustomerToCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fleetCustomerToCustomer As FleetCustomerToCustomer = Nothing
            While dr.Read

                fleetCustomerToCustomer = Me.CreateObject(dr)

            End While

            Return fleetCustomerToCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fleetCustomerToCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim fleetCustomerToCustomer As FleetCustomerToCustomer = Me.CreateObject(dr)
                fleetCustomerToCustomerList.Add(fleetCustomerToCustomer)
            End While

            Return fleetCustomerToCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomerToCustomer As FleetCustomerToCustomer = CType(obj, FleetCustomerToCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomerToCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomerToCustomer As FleetCustomerToCustomer = CType(obj, FleetCustomerToCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FleetCustomerID", DbType.Int32, fleetCustomerToCustomer.FleetCustomerID)
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, fleetCustomerToCustomer.CustomerID.ID)
            DbCommandWrapper.AddInParameter("@IsDefault", DbType.Boolean, fleetCustomerToCustomer.IsDefault)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomerToCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, fleetCustomerToCustomer.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, fleetCustomerToCustomer.LastUpdatedTime)


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

            Dim fleetCustomerToCustomer As FleetCustomerToCustomer = CType(obj, FleetCustomerToCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomerToCustomer.ID)
            DbCommandWrapper.AddInParameter("@FleetCustomerID", DbType.Int32, fleetCustomerToCustomer.FleetCustomerID)
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, fleetCustomerToCustomer.CustomerID.ID)
            DbCommandWrapper.AddInParameter("@IsDefault", DbType.Boolean, fleetCustomerToCustomer.IsDefault)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomerToCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fleetCustomerToCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, fleetCustomerToCustomer.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetCustomerToCustomer

            Dim fleetCustomerToCustomer As FleetCustomerToCustomer = New FleetCustomerToCustomer

            fleetCustomerToCustomer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCustomerID")) Then fleetCustomerToCustomer.FleetCustomerID = CType(dr("FleetCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerID")) Then fleetCustomerToCustomer.CustomerID = New Customer(ID:=CType(dr("CustomerID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("IsDefault")) Then fleetCustomerToCustomer.IsDefault = CType(dr("IsDefault"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fleetCustomerToCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fleetCustomerToCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fleetCustomerToCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then fleetCustomerToCustomer.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then fleetCustomerToCustomer.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            Return fleetCustomerToCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetCustomerToCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetCustomerToCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetCustomerToCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

