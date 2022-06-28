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

    Public Class V_MDPMasterVehicleMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SalesmanDownload"
        Private m_UpdateStatement As String = "up_UpdateV_SalesmanDownload"
        Private m_RetrieveStatement As String = "up_RetrieveV_SalesmanDownload"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SalesmanDownloadList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SalesmanDownload"

#End Region

        Protected Overrides Function DoRetrieve(dr As IDataReader) As Object

        End Function

        Protected Overrides Function DoRetrieveList(dr As IDataReader) As ArrayList

        End Function

        Protected Overrides Function GetDeleteParameter(obj As Object) As DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(obj As Object, user As String) As DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(dbCommandWrapper As DBCommandWrapper) As Integer

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(id As Integer) As DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(obj As Object, user As String) As DBCommandWrapper

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_MDPMasterVehicle) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_MDPMasterVehicle), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_MDPMasterVehicle).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

    End Class

End Namespace