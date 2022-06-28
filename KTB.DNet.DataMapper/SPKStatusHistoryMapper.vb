
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKStatusHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/3/2011 - 3:51:50 PM
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

    Public Class SPKStatusHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPKStatusHistory"
        Private m_UpdateStatement As String = "up_UpdateSPKStatusHistory"
        Private m_RetrieveStatement As String = "up_RetrieveSPKStatusHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveSPKStatusHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPKStatusHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPKStatusHistory As SPKStatusHistory = Nothing
            While dr.Read

                sPKStatusHistory = Me.CreateObject(dr)

            End While

            Return sPKStatusHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPKStatusHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim sPKStatusHistory As SPKStatusHistory = Me.CreateObject(dr)
                sPKStatusHistoryList.Add(sPKStatusHistory)
            End While

            Return sPKStatusHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKStatusHistory As SPKStatusHistory = CType(obj, SPKStatusHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKStatusHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKStatusHistory As SPKStatusHistory = CType(obj, SPKStatusHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@OldStatus", DbType.Byte, sPKStatusHistory.OldStatus)
            DbCommandWrapper.AddInParameter("@NewStatus", DbType.Byte, sPKStatusHistory.NewStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKStatusHistory.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, sPKStatusHistory.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, Me.GetRefObject(sPKStatusHistory.SPKHeader))

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

            Dim sPKStatusHistory As SPKStatusHistory = CType(obj, SPKStatusHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKStatusHistory.ID)
            DbCommandWrapper.AddInParameter("@OldStatus", DbType.Byte, sPKStatusHistory.OldStatus)
            DbCommandWrapper.AddInParameter("@NewStatus", DbType.Byte, sPKStatusHistory.NewStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKStatusHistory.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, sPKStatusHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)


            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, Me.GetRefObject(sPKStatusHistory.SPKHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPKStatusHistory

            Dim sPKStatusHistory As SPKStatusHistory = New SPKStatusHistory

            sPKStatusHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("OldStatus")) Then sPKStatusHistory.OldStatus = CType(dr("OldStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NewStatus")) Then sPKStatusHistory.NewStatus = CType(dr("NewStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPKStatusHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPKStatusHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPKStatusHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPKStatusHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPKStatusHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKHeaderID")) Then
                sPKStatusHistory.SPKHeader = New SPKHeader(CType(dr("SPKHeaderID"), Integer))
            End If

            Return sPKStatusHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPKStatusHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPKStatusHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPKStatusHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

