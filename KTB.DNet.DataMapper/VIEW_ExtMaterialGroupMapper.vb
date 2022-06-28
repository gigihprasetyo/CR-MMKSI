
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VIEW_ExtMaterialGroup Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 28/11/2005 - 12:36:42
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

    Public Class VIEW_ExtMaterialGroupMapper
        Inherits AbstractMapper


#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_RetrieveVIEW_ExtMaterialGroup"
        Private m_RetrieveListStatement As String = "up_RetrieveVIEW_ExtMaterialGroupList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vIEW_ExtMaterialGroup As vIEW_ExtMaterialGroup = Nothing
            While dr.Read

                vIEW_ExtMaterialGroup = Me.CreateObject(dr)

            End While

            Return vIEW_ExtMaterialGroup

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vIEW_ExtMaterialGroupList As ArrayList = New ArrayList

            While dr.Read
                Dim vIEW_ExtMaterialGroup As vIEW_ExtMaterialGroup = Me.CreateObject(dr)
                vIEW_ExtMaterialGroupList.Add(vIEW_ExtMaterialGroup)
            End While

            Return vIEW_ExtMaterialGroupList

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

        Private Function CreateObject(ByVal dr As IDataReader) As VIEW_ExtMaterialGroup

            Dim vIEW_ExtMaterialGroup As vIEW_ExtMaterialGroup = New vIEW_ExtMaterialGroup


            If Not dr.IsDBNull(dr.GetOrdinal("MonthPeriode")) Then vIEW_ExtMaterialGroup.MonthPeriode = CType(dr("MonthPeriode"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("YearPeriode")) Then vIEW_ExtMaterialGroup.YearPeriode = CType(dr("YearPeriode"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("ExtMaterialGroup")) Then vIEW_ExtMaterialGroup.ExtMaterialGroup = dr("ExtMaterialGroup").ToString

            Return vIEW_ExtMaterialGroup

        End Function

        Private Sub SetTableName()

            If Not (GetType(VIEW_ExtMaterialGroup) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VIEW_ExtMaterialGroup), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VIEW_ExtMaterialGroup).ToString + " does not have TableInfoAttribute.")
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


