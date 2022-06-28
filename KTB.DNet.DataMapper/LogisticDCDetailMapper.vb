﻿
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LogisticDCDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 9/11/2017 - 10:25:21 AM
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

    Public Class LogisticDCDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertLogisticDCDetail"
        Private m_UpdateStatement As String = "up_UpdateLogisticDCDetail"
        Private m_RetrieveStatement As String = "up_RetrieveLogisticDCDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveLogisticDCDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteLogisticDCDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim logisticDCDetail As LogisticDCDetail = Nothing
            While dr.Read

                logisticDCDetail = Me.CreateObject(dr)

            End While

            Return logisticDCDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim logisticDCDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim logisticDCDetail As LogisticDCDetail = Me.CreateObject(dr)
                logisticDCDetailList.Add(logisticDCDetail)
            End While

            Return logisticDCDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticDCDetail As LogisticDCDetail = CType(obj, LogisticDCDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticDCDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticDCDetail As LogisticDCDetail = CType(obj, LogisticDCDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@LogisticDCHeaderID", DbType.Int32, Me.GetRefObject(logisticDCDetail.logisticDCHeader))
            DbCommandWrapper.AddInParameter("@SAPModel", DbType.String, logisticDCDetail.SAPModel)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, logisticDCDetail.Quantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticDCDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, logisticDCDetail.LastUpdateBy)
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

            Dim logisticDCDetail As LogisticDCDetail = CType(obj, LogisticDCDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticDCDetail.ID)
            DbCommandWrapper.AddInParameter("@LogisticDCHeaderID", DbType.Int32, Me.GetRefObject(logisticDCDetail.logisticDCHeader))
            DbCommandWrapper.AddInParameter("@SAPModel", DbType.String, logisticDCDetail.SAPModel)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, logisticDCDetail.Quantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticDCDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, logisticDCDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As LogisticDCDetail

            Dim logisticDCDetail As LogisticDCDetail = New LogisticDCDetail

            logisticDCDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SAPModel")) Then logisticDCDetail.SAPModel = dr("SAPModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then logisticDCDetail.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then logisticDCDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then logisticDCDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then logisticDCDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then logisticDCDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticDCHeaderID")) Then
                logisticDCDetail.logisticDCHeader = New LogisticDCHeader(CType(dr("LogisticDCHeaderID"), Integer))
            End If
            Return logisticDCDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(LogisticDCDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(LogisticDCDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(LogisticDCDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

