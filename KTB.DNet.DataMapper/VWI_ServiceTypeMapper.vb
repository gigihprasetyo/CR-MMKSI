
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_ServiceType Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/11/2018 - 11:11:31
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

    Public Class VWI_ServiceTypeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_ServiceType"
        Private m_UpdateStatement As String = "up_UpdateVWI_ServiceType"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_ServiceType"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_ServiceTypeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_ServiceType"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_ServiceType As VWI_ServiceType = Nothing
            While dr.Read

                VWI_ServiceType = Me.CreateObject(dr)

            End While

            Return VWI_ServiceType

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_ServiceTypeList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_ServiceType As VWI_ServiceType = Me.CreateObject(dr)
                VWI_ServiceTypeList.Add(VWI_ServiceType)
            End While

            Return VWI_ServiceTypeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceType As VWI_ServiceType = CType(obj, VWI_ServiceType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ServiceType.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceType As VWI_ServiceType = CType(obj, VWI_ServiceType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ServiceTypeCode", DbType.AnsiString, VWI_ServiceType.ServiceTypeCode)
            DbCommandWrapper.AddInParameter("@ServiceTypeDescription", DbType.AnsiString, VWI_ServiceType.ServiceTypeDescription)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_ServiceType.Status)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim VWI_ServiceType As VWI_ServiceType = CType(obj, VWI_ServiceType)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ServiceType.ID)
            DbCommandWrapper.AddInParameter("@ServiceTypeCode", DbType.AnsiString, VWI_ServiceType.ServiceTypeCode)
            DbCommandWrapper.AddInParameter("@ServiceTypeDescription", DbType.AnsiString, VWI_ServiceType.ServiceTypeDescription)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_ServiceType.Status)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_ServiceType

            Dim VWI_ServiceType As VWI_ServiceType = New VWI_ServiceType

            VWI_ServiceType.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeCode")) Then VWI_ServiceType.ServiceTypeCode = dr("ServiceTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeDescription")) Then VWI_ServiceType.ServiceTypeDescription = dr("ServiceTypeDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then VWI_ServiceType.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_ServiceType.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return VWI_ServiceType

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_ServiceType) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_ServiceType), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_ServiceType).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

