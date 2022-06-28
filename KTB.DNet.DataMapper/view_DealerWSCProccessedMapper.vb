#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : view_DealerWSCProccessed Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/28/2005 - 10:53:00 AM
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

    Public Class view_DealerWSCProccessedMapper
        Inherits AbstractMapper


#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_Retrieveview_DealerWSCProccessed"
        Private m_RetrieveListStatement As String = "up_Retrieveview_DealerWSCProccessedList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim view_DealerWSCProccessed As view_DealerWSCProccessed = Nothing
            While dr.Read

                view_DealerWSCProccessed = Me.CreateObject(dr)

            End While

            Return view_DealerWSCProccessed

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim view_DealerWSCProccessedList As ArrayList = New ArrayList

            While dr.Read
                Dim view_DealerWSCProccessed As view_DealerWSCProccessed = Me.CreateObject(dr)
                view_DealerWSCProccessedList.Add(view_DealerWSCProccessed)
            End While

            Return view_DealerWSCProccessedList

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

        Private Function CreateObject(ByVal dr As IDataReader) As view_DealerWSCProccessed

            Dim view_DealerWSCProccessed As view_DealerWSCProccessed = New view_DealerWSCProccessed


            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then view_DealerWSCProccessed.DealerCode = dr("DealerCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then view_DealerWSCProccessed.DealerName = dr("DealerName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then view_DealerWSCProccessed.CityName = dr("CityName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedDate")) Then view_DealerWSCProccessed.CreatedDate = CType(dr("CreatedDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then view_DealerWSCProccessed.ReleaseDate = CType(dr("ReleaseDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ClaimType")) Then view_DealerWSCProccessed.ClaimType = dr("ClaimType").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("WSCCount")) Then view_DealerWSCProccessed.WSCCount = CType(dr("WSCCount"), Integer)

            Return view_DealerWSCProccessed

        End Function

        Private Sub SetTableName()

            If Not (GetType(view_DealerWSCProccessed) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(view_DealerWSCProccessed), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(view_DealerWSCProccessed).ToString + " does not have TableInfoAttribute.")
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

