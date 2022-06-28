
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_Fleet Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/08/2018 - 12:20:40
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

    Public Class VWI_FleetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_Fleet"
        Private m_UpdateStatement As String = "up_UpdateVWI_Fleet"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_Fleet"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_FleetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_Fleet"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_Fleet As VWI_Fleet = Nothing
            While dr.Read

                VWI_Fleet = Me.CreateObject(dr)

            End While

            Return VWI_Fleet

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_FleetList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_Fleet As VWI_Fleet = Me.CreateObject(dr)
                VWI_FleetList.Add(VWI_Fleet)
            End While

            Return VWI_FleetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Fleet As VWI_Fleet = CType(obj, VWI_Fleet)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_Fleet.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Fleet As VWI_Fleet = CType(obj, VWI_Fleet)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FleetCode", DbType.AnsiString, VWI_Fleet.FleetCode)
            DbCommandWrapper.AddInParameter("@FleetCustomerName", DbType.AnsiString, VWI_Fleet.FleetCustomerName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_Fleet.Status)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, VWI_Fleet.LastUpdateTime)


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

            Dim VWI_Fleet As VWI_Fleet = CType(obj, VWI_Fleet)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_Fleet.ID)
            DbCommandWrapper.AddInParameter("@FleetCode", DbType.AnsiString, VWI_Fleet.FleetCode)
            DbCommandWrapper.AddInParameter("@FleetCustomerName", DbType.AnsiString, VWI_Fleet.FleetCustomerName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_Fleet.Status)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_Fleet

            Dim VWI_Fleet As VWI_Fleet = New VWI_Fleet

            VWI_Fleet.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCode")) Then VWI_Fleet.FleetCode = dr("FleetCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCustomerName")) Then VWI_Fleet.FleetCustomerName = dr("FleetCustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityCode")) Then VWI_Fleet.CityCode = dr("CityCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then VWI_Fleet.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_Fleet.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return VWI_Fleet

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_Fleet) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_Fleet), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_Fleet).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

