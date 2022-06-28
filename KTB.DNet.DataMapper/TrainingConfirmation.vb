#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrainingConfirmation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:13:16 AM
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

    Public Class TrainingConfirmationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrainingConfirmation"
        Private m_UpdateStatement As String = "up_UpdateTrainingConfirmation"
        Private m_RetrieveStatement As String = "up_RetrieveTrainingConfirmation"
        Private m_RetrieveListStatement As String = "up_RetrieveTrainingConfirmationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrainingConfirmation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trainingConfirmation As TrainingConfirmation = Nothing
            While dr.Read

                trainingConfirmation = Me.CreateObject(dr)

            End While

            Return trainingConfirmation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trainingConfirmationList As ArrayList = New ArrayList

            While dr.Read
                Dim trainingConfirmation As TrainingConfirmation = Me.CreateObject(dr)
                trainingConfirmationList.Add(trainingConfirmation)
            End While

            Return trainingConfirmationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trainingConfirmation As TrainingConfirmation = CType(obj, TrainingConfirmation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trainingConfirmation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trainingConfirmation As TrainingConfirmation = CType(obj, TrainingConfirmation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.AnsiStringFixedLength, trainingConfirmation.DealerId)
            DbCommandWrapper.AddInParameter("@Confirmation", DbType.AnsiStringFixedLength, trainingConfirmation.Confirmation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trainingConfirmation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trainingConfirmation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@TrainingCodeId", DbType.Int32, Me.GetRefObject(trainingConfirmation.TrainingCode))
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, Me.GetRefObject(trainingConfirmation.SalesmanHeader))

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

            Dim trainingConfirmation As TrainingConfirmation = CType(obj, TrainingConfirmation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trainingConfirmation.ID)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.AnsiStringFixedLength, trainingConfirmation.DealerId)
            DbCommandWrapper.AddInParameter("@Confirmation", DbType.AnsiStringFixedLength, trainingConfirmation.Confirmation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trainingConfirmation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trainingConfirmation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@TrainingCodeId", DbType.Int32, Me.GetRefObject(trainingConfirmation.TrainingCode))
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, Me.GetRefObject(trainingConfirmation.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrainingConfirmation

            Dim trainingConfirmation As TrainingConfirmation = New TrainingConfirmation

            trainingConfirmation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then trainingConfirmation.DealerId = dr("DealerId").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Confirmation")) Then trainingConfirmation.Confirmation = dr("Confirmation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trainingConfirmation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trainingConfirmation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trainingConfirmation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trainingConfirmation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trainingConfirmation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TrainingCodeId")) Then
                trainingConfirmation.TrainingCode = New TrainingCode(CType(dr("TrainingCodeId"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanId")) Then
                trainingConfirmation.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanId"), Integer))
            End If

            Return trainingConfirmation

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrainingConfirmation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrainingConfirmation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrainingConfirmation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

