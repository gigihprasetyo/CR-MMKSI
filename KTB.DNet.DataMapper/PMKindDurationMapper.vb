﻿
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PMKindDuration Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 29/06/2020 - 23:43:23
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

    Public Class PMKindDurationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPMKindDuration"
        Private m_UpdateStatement As String = "up_UpdatePMKindDuration"
        Private m_RetrieveStatement As String = "up_RetrievePMKindDuration"
        Private m_RetrieveListStatement As String = "up_RetrievePMKindDurationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePMKindDuration"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pMKindDuration As PMKindDuration = Nothing
            While dr.Read

                pMKindDuration = Me.CreateObject(dr)

            End While

            Return pMKindDuration

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pMKindDurationList As ArrayList = New ArrayList

            While dr.Read
                Dim pMKindDuration As PMKindDuration = Me.CreateObject(dr)
                pMKindDurationList.Add(pMKindDuration)
            End While

            Return pMKindDurationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pMKindDuration As PMKindDuration = CType(obj, PMKindDuration)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pMKindDuration.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pMKindDuration As PMKindDuration = CType(obj, PMKindDuration)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, pMKindDuration.PMKindID)
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(pMKindDuration.PMKind))
            'DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, pMKindDuration.CategoryID)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(pMKindDuration.Category))
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, pMKindDuration.Duration)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pMKindDuration.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pMKindDuration.LastUpdateBy)
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

            Dim pMKindDuration As PMKindDuration = CType(obj, PMKindDuration)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pMKindDuration.ID)
            'DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, pMKindDuration.PMKindID)
            'DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, pMKindDuration.CategoryID)
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(pMKindDuration.PMKind))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(pMKindDuration.Category))
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, pMKindDuration.Duration)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pMKindDuration.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pMKindDuration.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PMKindDuration

            Dim pMKindDuration As PMKindDuration = New PMKindDuration

            pMKindDuration.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then pMKindDuration.PMKindID = CType(dr("PMKindID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then pMKindDuration.CategoryID = CType(dr("CategoryID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then pMKindDuration.Duration = CType(dr("Duration"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pMKindDuration.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pMKindDuration.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pMKindDuration.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pMKindDuration.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pMKindDuration.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then
                pMKindDuration.PMKind = New PMKind(CType(dr("PMKindID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                pMKindDuration.Category = New Category(CType(dr("CategoryID"), Integer))
            End If

            Return pMKindDuration

        End Function

        Private Sub SetTableName()

            If Not (GetType(PMKindDuration) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PMKindDuration), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PMKindDuration).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


