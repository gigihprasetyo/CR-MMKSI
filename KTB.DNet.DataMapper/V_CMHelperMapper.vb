
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_CMHelper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 9/13/2011 - 9:56:07 AM
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

    Public Class V_CMHelperMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_CMHelper"
        Private m_UpdateStatement As String = "up_UpdateV_CMHelper"
        Private m_RetrieveStatement As String = "up_RetrieveV_CMHelper"
        Private m_RetrieveListStatement As String = "up_RetrieveV_CMHelperList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_CMHelper"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_CMHelper As V_CMHelper = Nothing
            While dr.Read

                v_CMHelper = Me.CreateObject(dr)

            End While

            Return v_CMHelper

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_CMHelperList As ArrayList = New ArrayList

            While dr.Read
                Dim v_CMHelper As V_CMHelper = Me.CreateObject(dr)
                v_CMHelperList.Add(v_CMHelper)
            End While

            Return v_CMHelperList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_CMHelper As V_CMHelper = CType(obj, V_CMHelper)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_CMHelper.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_CMHelper As V_CMHelper = CType(obj, V_CMHelper)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, v_CMHelper.EndCustomerID)
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, v_CMHelper.CustomerID)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_CMHelper.FakturStatus)
            DbCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, v_CMHelper.MCPStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_CMHelper.LastUpdateBy)
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

            Dim v_CMHelper As V_CMHelper = CType(obj, V_CMHelper)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_CMHelper.ID)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, v_CMHelper.EndCustomerID)
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, v_CMHelper.CustomerID)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_CMHelper.FakturStatus)
            DbCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, v_CMHelper.MCPStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_CMHelper.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_CMHelper

            Dim v_CMHelper As V_CMHelper = New V_CMHelper

            v_CMHelper.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then v_CMHelper.EndCustomerID = CType(dr("EndCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerID")) Then v_CMHelper.CustomerID = CType(dr("CustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then v_CMHelper.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MCPStatus")) Then v_CMHelper.MCPStatus = CType(dr("MCPStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_CMHelper.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_CMHelper.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_CMHelper.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_CMHelper.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_CMHelper

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_CMHelper) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_CMHelper), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_CMHelper).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

