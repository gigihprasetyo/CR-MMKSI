#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrainingSales Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:18:22 AM
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

    Public Class TrainingSalesMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrainingSales"
        Private m_UpdateStatement As String = "up_UpdateTrainingSales"
        Private m_RetrieveStatement As String = "up_RetrieveTrainingSales"
        Private m_RetrieveListStatement As String = "up_RetrieveTrainingSalesList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrainingSales"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trainingSales As TrainingSales = Nothing
            While dr.Read

                trainingSales = Me.CreateObject(dr)

            End While

            Return trainingSales

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trainingSalesList As ArrayList = New ArrayList

            While dr.Read
                Dim trainingSales As TrainingSales = Me.CreateObject(dr)
                trainingSalesList.Add(trainingSales)
            End While

            Return trainingSalesList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trainingSales As TrainingSales = CType(obj, TrainingSales)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trainingSales.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trainingSales As TrainingSales = CType(obj, TrainingSales)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.AnsiStringFixedLength, trainingSales.DealerId)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trainingSales.Status)
            DbCommandWrapper.AddInParameter("@Validation", DbType.AnsiStringFixedLength, trainingSales.Validation)
            DbCommandWrapper.AddInParameter("@Cancellation", DbType.AnsiStringFixedLength, trainingSales.Cancellation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trainingSales.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trainingSales.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@TrainingCodeId", DbType.Int32, Me.GetRefObject(trainingSales.TrainingCode))
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, Me.GetRefObject(trainingSales.SalesmanHeader))

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

            Dim trainingSales As TrainingSales = CType(obj, TrainingSales)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trainingSales.ID)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.AnsiStringFixedLength, trainingSales.DealerId)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trainingSales.Status)
            DbCommandWrapper.AddInParameter("@Validation", DbType.AnsiStringFixedLength, trainingSales.Validation)
            DbCommandWrapper.AddInParameter("@Cancellation", DbType.AnsiStringFixedLength, trainingSales.Cancellation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trainingSales.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trainingSales.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@TrainingCodeId", DbType.Int32, Me.GetRefObject(trainingSales.TrainingCode))
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, Me.GetRefObject(trainingSales.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrainingSales

            Dim trainingSales As TrainingSales = New TrainingSales

            trainingSales.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then trainingSales.DealerId = dr("DealerId").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trainingSales.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Validation")) Then trainingSales.Validation = dr("Validation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Cancellation")) Then trainingSales.Cancellation = dr("Cancellation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trainingSales.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trainingSales.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trainingSales.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trainingSales.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trainingSales.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TrainingCodeId")) Then
                trainingSales.TrainingCode = New TrainingCode(CType(dr("TrainingCodeId"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanId")) Then
                trainingSales.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanId"), Integer))
            End If

            Return trainingSales

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrainingSales) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrainingSales), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrainingSales).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

