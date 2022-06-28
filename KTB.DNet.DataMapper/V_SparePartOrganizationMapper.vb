
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SparePartOrganization Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2011 - 1:43:29 PM
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

    Public Class V_SparePartOrganizationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SparePartOrganization"
        Private m_UpdateStatement As String = "up_UpdateV_SparePartOrganization"
        Private m_RetrieveStatement As String = "up_RetrieveV_SparePartOrganization"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SparePartOrganizationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SparePartOrganization"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SparePartOrganization As V_SparePartOrganization = Nothing
            While dr.Read

                v_SparePartOrganization = Me.CreateObject(dr)

            End While

            Return v_SparePartOrganization

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SparePartOrganizationList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SparePartOrganization As V_SparePartOrganization = Me.CreateObject(dr)
                v_SparePartOrganizationList.Add(v_SparePartOrganization)
            End While

            Return v_SparePartOrganizationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SparePartOrganization As V_SparePartOrganization = CType(obj, V_SparePartOrganization)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, v_SparePartOrganization.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SparePartOrganization As V_SparePartOrganization = CType(obj, V_SparePartOrganization)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@Grade", DbType.AnsiString, v_SparePartOrganization.Grade)
            DbCommandWrapper.AddInParameter("@SalesmanCategoryLevelID", DbType.Int32, v_SparePartOrganization.SalesmanCategoryLevelID)
            DbCommandWrapper.AddInParameter("@ParentID", DbType.Int32, v_SparePartOrganization.ParentID)
            DbCommandWrapper.AddInParameter("@PositionName", DbType.AnsiString, v_SparePartOrganization.PositionName)
            DbCommandWrapper.AddInParameter("@LevelNumber", DbType.Int16, v_SparePartOrganization.LevelNumber)
            DbCommandWrapper.AddInParameter("@ColoumnNumber", DbType.Byte, v_SparePartOrganization.ColoumnNumber)
            DbCommandWrapper.AddInParameter("@OrderNUmber", DbType.Int16, v_SparePartOrganization.OrderNUmber)


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

            Dim v_SparePartOrganization As V_SparePartOrganization = CType(obj, V_SparePartOrganization)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, v_SparePartOrganization.ID)
            DbCommandWrapper.AddInParameter("@Grade", DbType.AnsiString, v_SparePartOrganization.Grade)
            DbCommandWrapper.AddInParameter("@SalesmanCategoryLevelID", DbType.Int32, v_SparePartOrganization.SalesmanCategoryLevelID)
            DbCommandWrapper.AddInParameter("@ParentID", DbType.Int32, v_SparePartOrganization.ParentID)
            DbCommandWrapper.AddInParameter("@PositionName", DbType.AnsiString, v_SparePartOrganization.PositionName)
            DbCommandWrapper.AddInParameter("@LevelNumber", DbType.Int16, v_SparePartOrganization.LevelNumber)
            DbCommandWrapper.AddInParameter("@ColoumnNumber", DbType.Byte, v_SparePartOrganization.ColoumnNumber)
            DbCommandWrapper.AddInParameter("@OrderNUmber", DbType.Int16, v_SparePartOrganization.OrderNUmber)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SparePartOrganization

            Dim v_SparePartOrganization As V_SparePartOrganization = New V_SparePartOrganization

            v_SparePartOrganization.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Grade")) Then v_SparePartOrganization.Grade = dr("Grade").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCategoryLevelID")) Then v_SparePartOrganization.SalesmanCategoryLevelID = CType(dr("SalesmanCategoryLevelID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParentID")) Then v_SparePartOrganization.ParentID = CType(dr("ParentID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PositionName")) Then v_SparePartOrganization.PositionName = dr("PositionName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LevelNumber")) Then v_SparePartOrganization.LevelNumber = CType(dr("LevelNumber"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ColoumnNumber")) Then v_SparePartOrganization.ColoumnNumber = CType(dr("ColoumnNumber"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNUmber")) Then v_SparePartOrganization.OrderNUmber = CType(dr("OrderNUmber"), Short)

            Return v_SparePartOrganization

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SparePartOrganization) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SparePartOrganization), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SparePartOrganization).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

