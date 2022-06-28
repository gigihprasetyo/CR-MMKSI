
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistExpenseSpareParts Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 10:42:48 AM
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

    Public Class AssistExpenseSparePartsMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistExpenseSpareParts"
        Private m_UpdateStatement As String = "up_UpdateAssistExpenseSpareParts"
        Private m_RetrieveStatement As String = "up_RetrieveAssistExpenseSpareParts"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistExpenseSparePartsList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistExpenseSpareParts"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistExpenseSpareParts As AssistExpenseSpareParts = Nothing
            While dr.Read

                assistExpenseSpareParts = Me.CreateObject(dr)

            End While

            Return assistExpenseSpareParts

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistExpenseSparePartsList As ArrayList = New ArrayList

            While dr.Read
                Dim assistExpenseSpareParts As AssistExpenseSpareParts = Me.CreateObject(dr)
                assistExpenseSparePartsList.Add(assistExpenseSpareParts)
            End While

            Return assistExpenseSparePartsList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistExpenseSpareParts As AssistExpenseSpareParts = CType(obj, AssistExpenseSpareParts)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistExpenseSpareParts.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistExpenseSpareParts As AssistExpenseSpareParts = CType(obj, AssistExpenseSpareParts)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistExpenseSpareParts.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@ExpenseGroup", DbType.AnsiString, assistExpenseSpareParts.ExpenseGroup)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, assistExpenseSpareParts.Description)
            DbCommandWrapper.AddInParameter("@UnitType", DbType.AnsiString, assistExpenseSpareParts.UnitType)
            DbCommandWrapper.AddInParameter("@Value", DbType.Decimal, assistExpenseSpareParts.Value)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistExpenseSpareParts.RemarksSystem)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistExpenseSpareParts.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistExpenseSpareParts.StatusAktif)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistExpenseSpareParts.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistExpenseSpareParts.LastUpdateBy)
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

            Dim assistExpenseSpareParts As AssistExpenseSpareParts = CType(obj, AssistExpenseSpareParts)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistExpenseSpareParts.ID)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistExpenseSpareParts.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@ExpenseGroup", DbType.AnsiString, assistExpenseSpareParts.ExpenseGroup)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, assistExpenseSpareParts.Description)
            DbCommandWrapper.AddInParameter("@UnitType", DbType.AnsiString, assistExpenseSpareParts.UnitType)
            DbCommandWrapper.AddInParameter("@Value", DbType.Decimal, assistExpenseSpareParts.Value)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistExpenseSpareParts.RemarksSystem)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistExpenseSpareParts.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistExpenseSpareParts.StatusAktif)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistExpenseSpareParts.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistExpenseSpareParts.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistExpenseSpareParts

            Dim assistExpenseSpareParts As AssistExpenseSpareParts = New AssistExpenseSpareParts

            assistExpenseSpareParts.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ExpenseGroup")) Then assistExpenseSpareParts.ExpenseGroup = dr("ExpenseGroup").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then assistExpenseSpareParts.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UnitType")) Then assistExpenseSpareParts.UnitType = dr("UnitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Value")) Then assistExpenseSpareParts.Value = CType(dr("Value"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSystem")) Then assistExpenseSpareParts.RemarksSystem = dr("RemarksSystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateSystemStatus")) Then assistExpenseSpareParts.ValidateSystemStatus = CType(dr("ValidateSystemStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusAktif")) Then assistExpenseSpareParts.StatusAktif = CType(dr("StatusAktif"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistExpenseSpareParts.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistExpenseSpareParts.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistExpenseSpareParts.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistExpenseSpareParts.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistExpenseSpareParts.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistUploadLogID")) Then
                assistExpenseSpareParts.AssistUploadLog = New AssistUploadLog(CType(dr("AssistUploadLogID"), Integer))
            End If
            Return assistExpenseSpareParts

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistExpenseSpareParts) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistExpenseSpareParts), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistExpenseSpareParts).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

