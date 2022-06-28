
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AlertConfirmation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2010 - 10:14:39 AM
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

    Public Class AlertConfirmationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAlertConfirmation"
        Private m_UpdateStatement As String = "up_UpdateAlertConfirmation"
        Private m_RetrieveStatement As String = "up_RetrieveAlertConfirmation"
        Private m_RetrieveListStatement As String = "up_RetrieveAlertConfirmationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAlertConfirmation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim alertConfirmation As AlertConfirmation = Nothing
            While dr.Read

                alertConfirmation = Me.CreateObject(dr)

            End While

            Return alertConfirmation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim alertConfirmationList As ArrayList = New ArrayList

            While dr.Read
                Dim alertConfirmation As AlertConfirmation = Me.CreateObject(dr)
                alertConfirmationList.Add(alertConfirmation)
            End While

            Return alertConfirmationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim alertConfirmation As AlertConfirmation = CType(obj, AlertConfirmation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, alertConfirmation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim alertConfirmation As AlertConfirmation = CType(obj, AlertConfirmation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@ConfirmDate", DbType.DateTime, alertConfirmation.ConfirmDate)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, alertConfirmation.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, alertConfirmation.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, alertConfirmation.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@AlertMasterID", DbType.Int16, Me.GetRefObject(alertConfirmation.AlertMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, alertConfirmation.DealerID)

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

            Dim alertConfirmation As AlertConfirmation = CType(obj, AlertConfirmation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, alertConfirmation.ID)
            DbCommandWrapper.AddInParameter("@ConfirmDate", DbType.DateTime, alertConfirmation.ConfirmDate)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, alertConfirmation.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, alertConfirmation.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, alertConfirmation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)


            DbCommandWrapper.AddInParameter("@AlertMasterID", DbType.Int16, Me.GetRefObject(alertConfirmation.AlertMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, alertConfirmation.DealerID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AlertConfirmation

            Dim alertConfirmation As AlertConfirmation = New AlertConfirmation

            alertConfirmation.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmDate")) Then alertConfirmation.ConfirmDate = CType(dr("ConfirmDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then alertConfirmation.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then alertConfirmation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then alertConfirmation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then alertConfirmation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then alertConfirmation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then alertConfirmation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AlertMasterID")) Then
                alertConfirmation.AlertMaster = New AlertMaster(CType(dr("AlertMasterID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                alertConfirmation.DealerID = CType(dr("DealerID"), Short)
            End If

            Return alertConfirmation

        End Function

        Private Sub SetTableName()

            If Not (GetType(AlertConfirmation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AlertConfirmation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AlertConfirmation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

