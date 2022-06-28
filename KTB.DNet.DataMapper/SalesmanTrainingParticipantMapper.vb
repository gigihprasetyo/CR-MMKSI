#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanTrainingParticipant Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2007 - 2:34:45 PM
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

    Public Class SalesmanTrainingParticipantMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanTrainingParticipant"
        Private m_UpdateStatement As String = "up_UpdateSalesmanTrainingParticipant"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanTrainingParticipant"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanTrainingParticipantList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanTrainingParticipant"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanTrainingParticipant As SalesmanTrainingParticipant = Nothing
            While dr.Read

                salesmanTrainingParticipant = Me.CreateObject(dr)

            End While

            Return salesmanTrainingParticipant

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanTrainingParticipantList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanTrainingParticipant As SalesmanTrainingParticipant = Me.CreateObject(dr)
                salesmanTrainingParticipantList.Add(salesmanTrainingParticipant)
            End While

            Return salesmanTrainingParticipantList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanTrainingParticipant As SalesmanTrainingParticipant = CType(obj, SalesmanTrainingParticipant)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanTrainingParticipant.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanTrainingParticipant As SalesmanTrainingParticipant = CType(obj, SalesmanTrainingParticipant)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@IsValidated", DbType.Byte, salesmanTrainingParticipant.IsValidated)
            DbCommandWrapper.AddInParameter("@IsCancelled", DbType.Byte, salesmanTrainingParticipant.IsCancelled)
            DbCommandWrapper.AddInParameter("@IsConfirm", DbType.Byte, salesmanTrainingParticipant.IsConfirm)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanTrainingParticipant.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanTrainingParticipant.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanMasterTrainingID", DbType.Int32, Me.GetRefObject(salesmanTrainingParticipant.SalesmanMasterTraining))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanTrainingParticipant.SalesmanHeader))

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

            Dim salesmanTrainingParticipant As SalesmanTrainingParticipant = CType(obj, SalesmanTrainingParticipant)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanTrainingParticipant.ID)
            DbCommandWrapper.AddInParameter("@IsValidated", DbType.Byte, salesmanTrainingParticipant.IsValidated)
            DbCommandWrapper.AddInParameter("@IsCancelled", DbType.Byte, salesmanTrainingParticipant.IsCancelled)
            DbCommandWrapper.AddInParameter("@IsConfirm", DbType.Byte, salesmanTrainingParticipant.IsConfirm)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanTrainingParticipant.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanTrainingParticipant.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanMasterTrainingID", DbType.Int32, Me.GetRefObject(salesmanTrainingParticipant.SalesmanMasterTraining))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanTrainingParticipant.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanTrainingParticipant

            Dim salesmanTrainingParticipant As SalesmanTrainingParticipant = New SalesmanTrainingParticipant

            salesmanTrainingParticipant.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsValidated")) Then salesmanTrainingParticipant.IsValidated = CType(dr("IsValidated"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCancelled")) Then salesmanTrainingParticipant.IsCancelled = CType(dr("IsCancelled"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsConfirm")) Then salesmanTrainingParticipant.IsConfirm = CType(dr("IsConfirm"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanTrainingParticipant.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanTrainingParticipant.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanTrainingParticipant.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanTrainingParticipant.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanTrainingParticipant.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanMasterTrainingID")) Then
                salesmanTrainingParticipant.SalesmanMasterTraining = New SalesmanMasterTraining(CType(dr("SalesmanMasterTrainingID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanTrainingParticipant.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            End If

            Return salesmanTrainingParticipant

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanTrainingParticipant) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanTrainingParticipant), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanTrainingParticipant).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

