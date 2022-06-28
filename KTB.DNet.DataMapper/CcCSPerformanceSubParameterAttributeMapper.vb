﻿
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceSubParameterAttribute Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/03/2020 - 13:29:25
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

    Public Class CcCSPerformanceSubParameterAttributeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcCSPerformanceSubParameterAttribute"
        Private m_UpdateStatement As String = "up_UpdateCcCSPerformanceSubParameterAttribute"
        Private m_RetrieveStatement As String = "up_RetrieveCcCSPerformanceSubParameterAttribute"
        Private m_RetrieveListStatement As String = "up_RetrieveCcCSPerformanceSubParameterAttributeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcCSPerformanceSubParameterAttribute"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccCSPerformanceSubParameterAttribute As CcCSPerformanceSubParameterAttribute = Nothing
            While dr.Read

                ccCSPerformanceSubParameterAttribute = Me.CreateObject(dr)

            End While

            Return ccCSPerformanceSubParameterAttribute

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccCSPerformanceSubParameterAttributeList As ArrayList = New ArrayList

            While dr.Read
                Dim ccCSPerformanceSubParameterAttribute As CcCSPerformanceSubParameterAttribute = Me.CreateObject(dr)
                ccCSPerformanceSubParameterAttributeList.Add(ccCSPerformanceSubParameterAttribute)
            End While

            Return ccCSPerformanceSubParameterAttributeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceSubParameterAttribute As CcCSPerformanceSubParameterAttribute = CType(obj, CcCSPerformanceSubParameterAttribute)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceSubParameterAttribute.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceSubParameterAttribute As CcCSPerformanceSubParameterAttribute = CType(obj, CcCSPerformanceSubParameterAttribute)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceSubParameterID", DbType.Int32, ccCSPerformanceSubParameterAttribute.CcCSPerformanceSubParameter.ID)
            DbCommandWrapper.AddInParameter("@CcAttributeID", DbType.Int32, ccCSPerformanceSubParameterAttribute.CcAttribute.ID)
            DbCommandWrapper.AddInParameter("@CcPeriodIDFrom", DbType.Int32, ccCSPerformanceSubParameterAttribute.CcPeriodFrom.ID)
            DbCommandWrapper.AddInParameter("@CcPeriodIDTo", DbType.Int32, ccCSPerformanceSubParameterAttribute.CcPeriodTo.ID)
            DbCommandWrapper.AddInParameter("@MinimumScore", DbType.Decimal, ccCSPerformanceSubParameterAttribute.MinimumScore)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccCSPerformanceSubParameterAttribute.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceSubParameterAttribute.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccCSPerformanceSubParameterAttribute.LastUpdateBy)
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

            Dim ccCSPerformanceSubParameterAttribute As CcCSPerformanceSubParameterAttribute = CType(obj, CcCSPerformanceSubParameterAttribute)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceSubParameterAttribute.ID)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceSubParameterID", DbType.Int32, ccCSPerformanceSubParameterAttribute.CcCSPerformanceSubParameter.ID)
            DbCommandWrapper.AddInParameter("@CcAttributeID", DbType.Int32, ccCSPerformanceSubParameterAttribute.CcAttribute.ID)
            DbCommandWrapper.AddInParameter("@CcPeriodIDFrom", DbType.Int32, ccCSPerformanceSubParameterAttribute.CcPeriodFrom.ID)
            DbCommandWrapper.AddInParameter("@CcPeriodIDTo", DbType.Int32, ccCSPerformanceSubParameterAttribute.CcPeriodTo.ID)
            DbCommandWrapper.AddInParameter("@MinimumScore", DbType.Decimal, ccCSPerformanceSubParameterAttribute.MinimumScore)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccCSPerformanceSubParameterAttribute.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceSubParameterAttribute.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccCSPerformanceSubParameterAttribute.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcCSPerformanceSubParameterAttribute

            Dim ccCSPerformanceSubParameterAttribute As CcCSPerformanceSubParameterAttribute = New CcCSPerformanceSubParameterAttribute

            ccCSPerformanceSubParameterAttribute.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceSubParameterID")) Then
                ccCSPerformanceSubParameterAttribute.CcCSPerformanceSubParameter = New CcCSPerformanceSubParameter(CType(dr("CcCSPerformanceSubParameterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CcAttributeID")) Then
                ccCSPerformanceSubParameterAttribute.CcAttribute = New CcAttribute(CType(dr("CcAttributeID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MinimumScore")) Then ccCSPerformanceSubParameterAttribute.MinimumScore = CType(dr("MinimumScore"), Decimal)

            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodIDFrom")) Then
                ccCSPerformanceSubParameterAttribute.CcPeriodFrom = New CcPeriod(CType(dr("CcPeriodIDFrom"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodIDTo")) Then
                ccCSPerformanceSubParameterAttribute.CcPeriodTo = New CcPeriod(CType(dr("CcPeriodIDTo"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ccCSPerformanceSubParameterAttribute.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccCSPerformanceSubParameterAttribute.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccCSPerformanceSubParameterAttribute.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccCSPerformanceSubParameterAttribute.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccCSPerformanceSubParameterAttribute.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccCSPerformanceSubParameterAttribute.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccCSPerformanceSubParameterAttribute

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcCSPerformanceSubParameterAttribute) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcCSPerformanceSubParameterAttribute), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcCSPerformanceSubParameterAttribute).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
