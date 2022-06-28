#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : view_DealerWSC Objects Mapper.
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

    Public Class view_DealerWSCMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_Retrieveview_DealerWSC"
        Private m_RetrieveListStatement As String = "up_Retrieveview_DealerWSCList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim view_DealerWSC As view_DealerWSC = Nothing
            While dr.Read

                view_DealerWSC = Me.CreateObject(dr)

            End While

            Return view_DealerWSC

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim view_DealerWSCList As ArrayList = New ArrayList

            While dr.Read
                Dim view_DealerWSC As view_DealerWSC = Me.CreateObject(dr)
                view_DealerWSCList.Add(view_DealerWSC)
            End While

            Return view_DealerWSCList

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

        Private Function CreateObject(ByVal dr As IDataReader) As view_DealerWSC

            Dim view_DealerWSC As view_DealerWSC = New view_DealerWSC


            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then view_DealerWSC.DealerCode = dr("DealerCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then view_DealerWSC.DealerName = dr("DealerName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then view_DealerWSC.CityName = dr("CityName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedDate")) Then view_DealerWSC.CreatedDate = CType(dr("CreatedDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then view_DealerWSC.ReleaseDate = CType(dr("ReleaseDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ClaimType")) Then view_DealerWSC.ClaimType = dr("ClaimType").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("WSCCount")) Then view_DealerWSC.WSCCount = CType(dr("WSCCount"), Integer)

            Return view_DealerWSC

        End Function

        Private Sub SetTableName()

            If Not (GetType(view_DealerWSC) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(view_DealerWSC), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(view_DealerWSC).ToString + " does not have TableInfoAttribute.")
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

