#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineeSalesmanHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 9/26/2019 - 1:30:18 PM
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

    Public Class TrTraineeSalesmanHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrTraineeSalesmanHeader"
        Private m_UpdateStatement As String = "up_UpdateTrTraineeSalesmanHeader"
        Private m_RetrieveStatement As String = "up_RetrieveTrTraineeSalesmanHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveTrTraineeSalesmanHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrTraineeSalesmanHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trTraineeSalesmanHeader As TrTraineeSalesmanHeader = Nothing
            While dr.Read

                trTraineeSalesmanHeader = Me.CreateObject(dr)

            End While

            Return trTraineeSalesmanHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trTraineeSalesmanHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim trTraineeSalesmanHeader As TrTraineeSalesmanHeader = Me.CreateObject(dr)
                trTraineeSalesmanHeaderList.Add(trTraineeSalesmanHeader)
            End While

            Return trTraineeSalesmanHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineeSalesmanHeader As TrTraineeSalesmanHeader = CType(obj, TrTraineeSalesmanHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineeSalesmanHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineeSalesmanHeader As TrTraineeSalesmanHeader = CType(obj, TrTraineeSalesmanHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TrTraineeID", DbType.Int32, Me.GetRefObject(trTraineeSalesmanHeader.TrTrainee))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(trTraineeSalesmanHeader.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, trTraineeSalesmanHeader.JobPosition)
            DbCommandWrapper.AddInParameter("@JobPositionAreaID", DbType.Int16, trTraineeSalesmanHeader.JobPositionAreaID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trTraineeSalesmanHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineeSalesmanHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trTraineeSalesmanHeader.LastUpdateBy)
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

            Dim trTraineeSalesmanHeader As TrTraineeSalesmanHeader = CType(obj, TrTraineeSalesmanHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineeSalesmanHeader.ID)
            DbCommandWrapper.AddInParameter("@TrTraineeID", DbType.Int32, Me.GetRefObject(trTraineeSalesmanHeader.TrTrainee))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(trTraineeSalesmanHeader.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, trTraineeSalesmanHeader.JobPosition)
            DbCommandWrapper.AddInParameter("@JobPositionAreaID", DbType.Int16, trTraineeSalesmanHeader.JobPositionAreaID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trTraineeSalesmanHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineeSalesmanHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trTraineeSalesmanHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrTraineeSalesmanHeader

            Dim trTraineeSalesmanHeader As TrTraineeSalesmanHeader = New TrTraineeSalesmanHeader

            trTraineeSalesmanHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineeID")) Then trTraineeSalesmanHeader.TrTrainee = New TrTrainee(CType(dr("TrTraineeID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then trTraineeSalesmanHeader.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))

            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition")) Then
                trTraineeSalesmanHeader.JobPosition = dr("JobPosition").ToString
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionAreaID")) Then trTraineeSalesmanHeader.JobPositionAreaID = CType(dr("JobPositionAreaID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trTraineeSalesmanHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trTraineeSalesmanHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trTraineeSalesmanHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trTraineeSalesmanHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trTraineeSalesmanHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trTraineeSalesmanHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trTraineeSalesmanHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrTraineeSalesmanHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrTraineeSalesmanHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrTraineeSalesmanHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
