﻿#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PDILog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/25/2020 - 2:29:08 PM
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

    Public Class PDILogMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPDILog"
        Private m_UpdateStatement As String = "up_UpdatePDILog"
        Private m_RetrieveStatement As String = "up_RetrievePDILog"
        Private m_RetrieveListStatement As String = "up_RetrievePDILogList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePDILog"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PDILog As PDILog = Nothing
            While dr.Read

                PDILog = Me.CreateObject(dr)

            End While

            Return PDILog

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PDILogList As ArrayList = New ArrayList

            While dr.Read
                Dim PDILog As PDILog = Me.CreateObject(dr)
                PDILogList.Add(PDILog)
            End While

            Return PDILogList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PDILog As PDILog = CType(obj, PDILog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PDILog.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PDILog As PDILog = CType(obj, PDILog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ExpiredPDIDate", DbType.DateTime, PDILog.ExpiredPDIDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PDILog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, PDILog.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, PDILog.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@PDIID", DbType.Int32, Me.GetRefObject(PDILog.PDI))

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

            Dim PDILog As PDILog = CType(obj, PDILog)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PDILog.ID)
            DbCommandWrapper.AddInParameter("@ExpiredPDIDate", DbType.DateTime, PDILog.ExpiredPDIDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PDILog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PDILog.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, PDILog.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PDIID", DbType.Int32, Me.GetRefObject(PDILog.PDI))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PDILog

            Dim PDILog As PDILog = New PDILog

            PDILog.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ExpiredPDIDate")) Then PDILog.ExpiredPDIDate = CType(dr("ExpiredPDIDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PDILog.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PDILog.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PDILog.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then PDILog.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then PDILog.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PDIID")) Then
                PDILog.PDI = New PDI(CType(dr("PDIID"), Integer))
            End If

            Return PDILog

        End Function

        Private Sub SetTableName()

            If Not (GetType(PDILog) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PDILog), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PDILog).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
