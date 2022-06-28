#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : view_PKList Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2005 - 4:52:16 PM
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

    Public Class view_PKListMapperView
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_Retrieveview_PKList"
        Private m_RetrieveListStatement As String = "up_Retrieveview_PKListList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim view_PKList As view_PKList = Nothing
            While dr.Read

                view_PKList = Me.CreateObject(dr)

            End While

            Return view_PKList

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim view_PKListList As ArrayList = New ArrayList

            While dr.Read
                Dim view_PKList As view_PKList = Me.CreateObject(dr)
                view_PKListList.Add(view_PKList)
            End While

            Return view_PKListList

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

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal user As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper
        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal user As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As view_PKList

            Dim view_PKList As view_PKList = New view_PKList


            If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then view_PKList.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("PKStatus")) Then view_PKList.PKStatus = dr("PKStatus").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("PKNumber")) Then view_PKList.PKNumber = dr("PKNumber").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("PKDate")) Then view_PKList.PKDate = CType(dr("PKDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrg")) Then view_PKList.SalesOrg = dr("SalesOrg").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then view_PKList.OrderType = CType(dr("OrderType"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then view_PKList.ProductionYear = CType(dr("ProductionYear"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then view_PKList.ProjectName = dr("ProjectName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("KTBResponse")) Then view_PKList.KTBResponse = dr("KTBResponse").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then view_PKList.Description = dr("Description").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RefPK")) Then view_PKList.RefPK = CType(dr("RefPK"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then view_PKList.DealerCode = dr("DealerCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("Purpose")) Then view_PKList.Purpose = CType(dr("Purpose"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then view_PKList.RowStatus = CType(dr("RowStatus"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("OrderPlan")) Then view_PKList.OrderPlan = CType(dr("OrderPlan"), Short)

            Return view_PKList

        End Function

        Private Sub SetTableName()

            If Not (GetType(view_PKList) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(view_PKList), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(view_PKList).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace