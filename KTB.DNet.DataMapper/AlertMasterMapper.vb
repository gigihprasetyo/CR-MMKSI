#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AlertMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2007 - 4:57:12 PM
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

    Public Class AlertMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAlertMaster"
        Private m_UpdateStatement As String = "up_UpdateAlertMaster"
        Private m_RetrieveStatement As String = "up_RetrieveAlertMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveAlertMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAlertMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim alertMaster As AlertMaster = Nothing
            While dr.Read

                alertMaster = Me.CreateObject(dr)

            End While

            Return alertMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim alertMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim alertMaster As AlertMaster = Me.CreateObject(dr)
                alertMasterList.Add(alertMaster)
            End While

            Return alertMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim alertMaster As AlertMaster = CType(obj, AlertMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, alertMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim alertMaster As AlertMaster = CType(obj, AlertMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, alertMaster.Name)
            DbCommandWrapper.AddInParameter("@AlertType", DbType.Int16, alertMaster.AlertType)
            DbCommandWrapper.AddInParameter("@AnnouncementAlertType", DbType.Int16, alertMaster.AnnouncementAlertType)
            DbCommandWrapper.AddInParameter("@Desc", DbType.AnsiString, alertMaster.Desc)
            DbCommandWrapper.AddInParameter("@FontEffect", DbType.Int16, alertMaster.FontEffect)
            DbCommandWrapper.AddInParameter("@DateValidFrom", DbType.DateTime, alertMaster.DateValidFrom)
            DbCommandWrapper.AddInParameter("@DateValidTo", DbType.DateTime, alertMaster.DateValidTo)
            DbCommandWrapper.AddInParameter("@UploadedBy", DbType.AnsiString, alertMaster.UploadedBy)
            DbCommandWrapper.AddInParameter("@IsIncludeHoliday", DbType.Boolean, alertMaster.IsIncludeHoliday)
            DbCommandWrapper.AddInParameter("@TimeStartFrom", DbType.DateTime, alertMaster.TimeStartFrom)
            DbCommandWrapper.AddInParameter("@TimeStartTo", DbType.DateTime, alertMaster.TimeStartTo)
            DbCommandWrapper.AddInParameter("@IsViaDashboard", DbType.Boolean, alertMaster.IsViaDashboard)
            DbCommandWrapper.AddInParameter("@ViaDashboardFrequency", DbType.Int32, alertMaster.ViaDashboardFrequency)
            DbCommandWrapper.AddInParameter("@ViaDashboardFreqType", DbType.AnsiStringFixedLength, alertMaster.ViaDashboardFreqType)
            DbCommandWrapper.AddInParameter("@IsViaAlertBox", DbType.Boolean, alertMaster.IsViaAlertBox)
            DbCommandWrapper.AddInParameter("@ViaAlertBoxFrequency", DbType.Int32, alertMaster.ViaAlertBoxFrequency)
            DbCommandWrapper.AddInParameter("@ViaAlertBoxFreqType", DbType.AnsiStringFixedLength, alertMaster.ViaAlertBoxFreqType)
            DbCommandWrapper.AddInParameter("@IsViaSMS", DbType.Boolean, alertMaster.IsViaSMS)
            DbCommandWrapper.AddInParameter("@ViaSMSFrequency", DbType.Int32, alertMaster.ViaSMSFrequency)
            DbCommandWrapper.AddInParameter("@ViaSMSFreqType", DbType.AnsiStringFixedLength, alertMaster.ViaSMSFreqType)
            DbCommandWrapper.AddInParameter("@IsViaEmail", DbType.Boolean, alertMaster.IsViaEmail)
            DbCommandWrapper.AddInParameter("@ViaEmailFrequency", DbType.Int32, alertMaster.ViaEmailFrequency)
            DbCommandWrapper.AddInParameter("@ViaEmailFreqType", DbType.AnsiStringFixedLength, alertMaster.ViaEmailFreqType)
            DbCommandWrapper.AddInParameter("@NextRunForDashboard", DbType.DateTime, alertMaster.NextRunForDashboard)
            DbCommandWrapper.AddInParameter("@NextRunForAlertBox", DbType.DateTime, alertMaster.NextRunForAlertBox)
            DbCommandWrapper.AddInParameter("@NextRunForSMS", DbType.DateTime, alertMaster.NextRunForSMS)
            DbCommandWrapper.AddInParameter("@NextRunForEmail", DbType.DateTime, alertMaster.NextRunForEmail)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, alertMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, alertMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AlertModulID", DbType.Int32, Me.GetRefObject(alertMaster.AlertModul))

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

            Dim alertMaster As AlertMaster = CType(obj, AlertMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, alertMaster.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, alertMaster.Name)
            DbCommandWrapper.AddInParameter("@AlertType", DbType.Int16, alertMaster.AlertType)
            DbCommandWrapper.AddInParameter("@AnnouncementAlertType", DbType.Int16, alertMaster.AnnouncementAlertType)
            DbCommandWrapper.AddInParameter("@Desc", DbType.AnsiString, alertMaster.Desc)
            DbCommandWrapper.AddInParameter("@FontEffect", DbType.Int16, alertMaster.FontEffect)
            DbCommandWrapper.AddInParameter("@DateValidFrom", DbType.DateTime, alertMaster.DateValidFrom)
            DbCommandWrapper.AddInParameter("@DateValidTo", DbType.DateTime, alertMaster.DateValidTo)
            DbCommandWrapper.AddInParameter("@UploadedBy", DbType.AnsiString, alertMaster.UploadedBy)
            DbCommandWrapper.AddInParameter("@IsIncludeHoliday", DbType.Boolean, alertMaster.IsIncludeHoliday)
            DbCommandWrapper.AddInParameter("@TimeStartFrom", DbType.DateTime, alertMaster.TimeStartFrom)
            DbCommandWrapper.AddInParameter("@TimeStartTo", DbType.DateTime, alertMaster.TimeStartTo)
            DbCommandWrapper.AddInParameter("@IsViaDashboard", DbType.Boolean, alertMaster.IsViaDashboard)
            DbCommandWrapper.AddInParameter("@ViaDashboardFrequency", DbType.Int32, alertMaster.ViaDashboardFrequency)
            DbCommandWrapper.AddInParameter("@ViaDashboardFreqType", DbType.AnsiStringFixedLength, alertMaster.ViaDashboardFreqType)
            DbCommandWrapper.AddInParameter("@IsViaAlertBox", DbType.Boolean, alertMaster.IsViaAlertBox)
            DbCommandWrapper.AddInParameter("@ViaAlertBoxFrequency", DbType.Int32, alertMaster.ViaAlertBoxFrequency)
            DbCommandWrapper.AddInParameter("@ViaAlertBoxFreqType", DbType.AnsiStringFixedLength, alertMaster.ViaAlertBoxFreqType)
            DbCommandWrapper.AddInParameter("@IsViaSMS", DbType.Boolean, alertMaster.IsViaSMS)
            DbCommandWrapper.AddInParameter("@ViaSMSFrequency", DbType.Int32, alertMaster.ViaSMSFrequency)
            DbCommandWrapper.AddInParameter("@ViaSMSFreqType", DbType.AnsiStringFixedLength, alertMaster.ViaSMSFreqType)
            DbCommandWrapper.AddInParameter("@IsViaEmail", DbType.Boolean, alertMaster.IsViaEmail)
            DbCommandWrapper.AddInParameter("@ViaEmailFrequency", DbType.Int32, alertMaster.ViaEmailFrequency)
            DbCommandWrapper.AddInParameter("@ViaEmailFreqType", DbType.AnsiStringFixedLength, alertMaster.ViaEmailFreqType)
            DbCommandWrapper.AddInParameter("@NextRunForDashboard", DbType.DateTime, alertMaster.NextRunForDashboard)
            DbCommandWrapper.AddInParameter("@NextRunForAlertBox", DbType.DateTime, alertMaster.NextRunForAlertBox)
            DbCommandWrapper.AddInParameter("@NextRunForSMS", DbType.DateTime, alertMaster.NextRunForSMS)
            DbCommandWrapper.AddInParameter("@NextRunForEmail", DbType.DateTime, alertMaster.NextRunForEmail)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, alertMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, alertMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@AlertModulID", DbType.Int32, Me.GetRefObject(alertMaster.AlertModul))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AlertMaster

            Dim alertMaster As AlertMaster = New AlertMaster

            alertMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then alertMaster.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AlertType")) Then alertMaster.AlertType = CType(dr("AlertType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AnnouncementAlertType")) Then alertMaster.AnnouncementAlertType = CType(dr("AnnouncementAlertType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Desc")) Then alertMaster.Desc = dr("Desc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FontEffect")) Then alertMaster.FontEffect = CType(dr("FontEffect"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DateValidFrom")) Then alertMaster.DateValidFrom = CType(dr("DateValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DateValidTo")) Then alertMaster.DateValidTo = CType(dr("DateValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadedBy")) Then alertMaster.UploadedBy = dr("UploadedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsIncludeHoliday")) Then alertMaster.IsIncludeHoliday = CType(dr("IsIncludeHoliday"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("TimeStartFrom")) Then alertMaster.TimeStartFrom = CType(dr("TimeStartFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TimeStartTo")) Then alertMaster.TimeStartTo = CType(dr("TimeStartTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsViaDashboard")) Then alertMaster.IsViaDashboard = CType(dr("IsViaDashboard"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ViaDashboardFrequency")) Then alertMaster.ViaDashboardFrequency = CType(dr("ViaDashboardFrequency"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ViaDashboardFreqType")) Then alertMaster.ViaDashboardFreqType = dr("ViaDashboardFreqType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsViaAlertBox")) Then alertMaster.IsViaAlertBox = CType(dr("IsViaAlertBox"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ViaAlertBoxFrequency")) Then alertMaster.ViaAlertBoxFrequency = CType(dr("ViaAlertBoxFrequency"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ViaAlertBoxFreqType")) Then alertMaster.ViaAlertBoxFreqType = dr("ViaAlertBoxFreqType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsViaSMS")) Then alertMaster.IsViaSMS = CType(dr("IsViaSMS"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ViaSMSFrequency")) Then alertMaster.ViaSMSFrequency = CType(dr("ViaSMSFrequency"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ViaSMSFreqType")) Then alertMaster.ViaSMSFreqType = dr("ViaSMSFreqType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsViaEmail")) Then alertMaster.IsViaEmail = CType(dr("IsViaEmail"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ViaEmailFrequency")) Then alertMaster.ViaEmailFrequency = CType(dr("ViaEmailFrequency"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ViaEmailFreqType")) Then alertMaster.ViaEmailFreqType = dr("ViaEmailFreqType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NextRunForDashboard")) Then alertMaster.NextRunForDashboard = CType(dr("NextRunForDashboard"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NextRunForAlertBox")) Then alertMaster.NextRunForAlertBox = CType(dr("NextRunForAlertBox"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NextRunForSMS")) Then alertMaster.NextRunForSMS = CType(dr("NextRunForSMS"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NextRunForEmail")) Then alertMaster.NextRunForEmail = CType(dr("NextRunForEmail"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then alertMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then alertMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then alertMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then alertMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then alertMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AlertModulID")) Then
                alertMaster.AlertModul = New AlertModul(CType(dr("AlertModulID"), Integer))
            End If

            Return alertMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(AlertMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AlertMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AlertMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

