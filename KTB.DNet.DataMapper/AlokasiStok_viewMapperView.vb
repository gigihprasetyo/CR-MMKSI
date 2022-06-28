#Region "Summary"
'// ===========================================================================
'// AUTHOR        : 
'// PURPOSE       : AlokasiStok_view Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 13/10/2005 - 3:21:52 PM
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

    Public Class AlokasiStok_viewMapperView
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_RetrieveAlokasiStok_view"
        Private m_RetrieveListStatement As String = "up_RetrieveAlokasiStok_viewList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim alokasiStok_view As alokasiStok_view = Nothing
            While dr.Read

                alokasiStok_view = Me.CreateObject(dr)

            End While

            Return alokasiStok_view

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim alokasiStok_viewList As ArrayList = New ArrayList

            While dr.Read
                Dim alokasiStok_view As alokasiStok_view = Me.CreateObject(dr)
                alokasiStok_viewList.Add(alokasiStok_view)
            End While

            Return alokasiStok_viewList

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

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper
        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal user As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AlokasiStok_view

            Dim alokasiStok_view As alokasiStok_view = New alokasiStok_view


            If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then alokasiStok_view.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("IDDealer")) Then alokasiStok_view.IDDealer = CType(dr("IDDealer"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then alokasiStok_view.VechileColorID = CType(dr("VechileColorID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then alokasiStok_view.VechileTypeID = CType(dr("VechileTypeID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then alokasiStok_view.DealerName = dr("DealerName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then alokasiStok_view.DealerCode = dr("DealerCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then alokasiStok_view.DealerID = CType(dr("DealerID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("ReqQty")) Then alokasiStok_view.ReqQty = CType(dr("ReqQty"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) Then alokasiStok_view.POHeaderID = CType(dr("POHeaderID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then alokasiStok_view.MaterialNumber = dr("MaterialNumber").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ProposeQty")) Then alokasiStok_view.ProposeQty = CType(dr("ProposeQty"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then alokasiStok_view.MaterialDescription = dr("MaterialDescription").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ReqAllocationDateTime")) Then alokasiStok_view.ReqAllocationDateTime = CType(dr("ReqAllocationDateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then alokasiStok_view.Description = dr("Description").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then alokasiStok_view.RowStatus = CType(dr("RowStatus"), Short)

            Return alokasiStok_view

        End Function

        Private Sub SetTableName()

            If Not (GetType(AlokasiStok_view) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AlokasiStok_view), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AlokasiStok_view).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace