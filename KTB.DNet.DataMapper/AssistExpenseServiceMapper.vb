
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistExpenseService Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 10:41:14 AM
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

    Public Class AssistExpenseServiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistExpenseService"
        Private m_UpdateStatement As String = "up_UpdateAssistExpenseService"
        Private m_RetrieveStatement As String = "up_RetrieveAssistExpenseService"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistExpenseServiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistExpenseService"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistExpenseService As AssistExpenseService = Nothing
            While dr.Read

                assistExpenseService = Me.CreateObject(dr)

            End While

            Return assistExpenseService

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistExpenseServiceList As ArrayList = New ArrayList

            While dr.Read
                Dim assistExpenseService As AssistExpenseService = Me.CreateObject(dr)
                assistExpenseServiceList.Add(assistExpenseService)
            End While

            Return assistExpenseServiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistExpenseService As AssistExpenseService = CType(obj, AssistExpenseService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistExpenseService.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistExpenseService As AssistExpenseService = CType(obj, AssistExpenseService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistExpenseService.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@ExpenseGroup", DbType.AnsiString, assistExpenseService.ExpenseGroup)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, assistExpenseService.Description)
            DbCommandWrapper.AddInParameter("@UnitType", DbType.AnsiString, assistExpenseService.UnitType)
            DbCommandWrapper.AddInParameter("@Value", DbType.Decimal, assistExpenseService.Value)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistExpenseService.RemarksSystem)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistExpenseService.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistExpenseService.StatusAktif)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistExpenseService.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistExpenseService.LastUpdateBy)
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

            Dim assistExpenseService As AssistExpenseService = CType(obj, AssistExpenseService)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistExpenseService.ID)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistExpenseService.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@ExpenseGroup", DbType.AnsiString, assistExpenseService.ExpenseGroup)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, assistExpenseService.Description)
            DbCommandWrapper.AddInParameter("@UnitType", DbType.AnsiString, assistExpenseService.UnitType)
            DbCommandWrapper.AddInParameter("@Value", DbType.Decimal, assistExpenseService.Value)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistExpenseService.RemarksSystem)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistExpenseService.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistExpenseService.StatusAktif)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistExpenseService.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistExpenseService.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistExpenseService

            Dim assistExpenseService As AssistExpenseService = New AssistExpenseService

            assistExpenseService.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ExpenseGroup")) Then assistExpenseService.ExpenseGroup = dr("ExpenseGroup").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then assistExpenseService.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UnitType")) Then assistExpenseService.UnitType = dr("UnitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Value")) Then assistExpenseService.Value = CType(dr("Value"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSystem")) Then assistExpenseService.RemarksSystem = dr("RemarksSystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateSystemStatus")) Then assistExpenseService.ValidateSystemStatus = CType(dr("ValidateSystemStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusAktif")) Then assistExpenseService.StatusAktif = CType(dr("StatusAktif"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistExpenseService.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistExpenseService.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistExpenseService.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistExpenseService.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistExpenseService.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistUploadLogID")) Then
                assistExpenseService.AssistUploadLog = New AssistUploadLog(CType(dr("AssistUploadLogID"), Integer))
            End If

            Return assistExpenseService

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistExpenseService) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistExpenseService), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistExpenseService).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

