#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : view_DealerWSCBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 27/10/2005 - 14:04:42
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

    Public Class view_DealerWSCBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_Retrieveview_DealerWSCBB"
        Private m_RetrieveListStatement As String = "up_Retrieveview_DealerWSCBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim view_DealerWSCBB As view_DealerWSCBB = Nothing
            While dr.Read

                view_DealerWSCBB = Me.CreateObject(dr)

            End While

            Return view_DealerWSCBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim view_DealerWSCBBList As ArrayList = New ArrayList

            While dr.Read
                Dim view_DealerWSCBB As view_DealerWSCBB = Me.CreateObject(dr)
                view_DealerWSCBBList.Add(view_DealerWSCBB)
            End While

            Return view_DealerWSCBBList

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

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As view_DealerWSCBB

            Dim view_DealerWSCBB As view_DealerWSCBB = New view_DealerWSCBB


            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then view_DealerWSCBB.DealerCode = dr("DealerCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then view_DealerWSCBB.DealerName = dr("DealerName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then view_DealerWSCBB.CityName = dr("CityName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedDate")) Then view_DealerWSCBB.CreatedDate = CType(dr("CreatedDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then view_DealerWSCBB.ReleaseDate = CType(dr("ReleaseDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ClaimType")) Then view_DealerWSCBB.ClaimType = dr("ClaimType").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("WSCCount")) Then view_DealerWSCBB.WSCCount = CType(dr("WSCCount"), Integer)

            Return view_DealerWSCBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(view_DealerWSCBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(view_DealerWSCBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(view_DealerWSCBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal user As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal user As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function
    End Class
End Namespace

