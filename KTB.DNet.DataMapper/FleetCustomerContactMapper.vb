
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetCustomerContact Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2017 - 1:40:13 PM
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

    Public Class FleetCustomerContactMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleetCustomerContact"
        Private m_UpdateStatement As String = "up_UpdateFleetCustomerContact"
        Private m_RetrieveStatement As String = "up_RetrieveFleetCustomerContact"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetCustomerContactList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleetCustomerContact"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fleetCustomerContact As FleetCustomerContact = Nothing
            While dr.Read

                fleetCustomerContact = Me.CreateObject(dr)

            End While

            Return fleetCustomerContact

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fleetCustomerContactList As ArrayList = New ArrayList

            While dr.Read
                Dim fleetCustomerContact As FleetCustomerContact = Me.CreateObject(dr)
                fleetCustomerContactList.Add(fleetCustomerContact)
            End While

            Return fleetCustomerContactList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomerContact As FleetCustomerContact = CType(obj, FleetCustomerContact)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomerContact.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetCustomerContact As FleetCustomerContact = CType(obj, FleetCustomerContact)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, fleetCustomerContact.Name)
            DbCommandWrapper.AddInParameter("@Position", DbType.AnsiString, fleetCustomerContact.Position)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, fleetCustomerContact.PhoneNo)
            DbCommandWrapper.AddInParameter("@Handphone", DbType.AnsiString, fleetCustomerContact.Handphone)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, fleetCustomerContact.Email)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomerContact.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, fleetCustomerContact.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,fleetCustomerContact.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@FleetCustomerID", DbType.Int32, fleetCustomerContact.FleetCustomerID)

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

            Dim fleetCustomerContact As FleetCustomerContact = CType(obj, FleetCustomerContact)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetCustomerContact.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, fleetCustomerContact.Name)
            DbCommandWrapper.AddInParameter("@Position", DbType.AnsiString, fleetCustomerContact.Position)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, fleetCustomerContact.PhoneNo)
            DbCommandWrapper.AddInParameter("@Handphone", DbType.AnsiString, fleetCustomerContact.Handphone)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, fleetCustomerContact.Email)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetCustomerContact.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fleetCustomerContact.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, fleetCustomerContact.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, DateTime.Now)


            DbCommandWrapper.AddInParameter("@FleetCustomerID", DbType.Int32, fleetCustomerContact.FleetCustomerID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetCustomerContact

            Dim fleetCustomerContact As FleetCustomerContact = New FleetCustomerContact

            fleetCustomerContact.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then fleetCustomerContact.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Position")) Then fleetCustomerContact.Position = dr("Position").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then fleetCustomerContact.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Handphone")) Then fleetCustomerContact.Handphone = dr("Handphone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then fleetCustomerContact.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fleetCustomerContact.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fleetCustomerContact.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fleetCustomerContact.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then fleetCustomerContact.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then fleetCustomerContact.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCustomerID")) Then
                fleetCustomerContact.FleetCustomerID = CType(dr("FleetCustomerID"), Integer)
            End If

            Return fleetCustomerContact

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetCustomerContact) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetCustomerContact), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetCustomerContact).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

