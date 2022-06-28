#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanGrade Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 11/15/2019 - 1:59:11 PM
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

    Public Class SalesmanGradeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanGrade"
        Private m_UpdateStatement As String = "up_UpdateSalesmanGrade"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanGrade"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanGradeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanGrade"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanGrade As SalesmanGrade = Nothing
            While dr.Read

                salesmanGrade = Me.CreateObject(dr)

            End While

            Return salesmanGrade

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanGradeList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanGrade As SalesmanGrade = Me.CreateObject(dr)
                salesmanGradeList.Add(salesmanGrade)
            End While

            Return salesmanGradeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanGrade As SalesmanGrade = CType(obj, SalesmanGrade)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanGrade.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanGrade As SalesmanGrade = CType(obj, SalesmanGrade)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanGrade.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, salesmanGrade.Year)
            DbCommandWrapper.AddInParameter("@Period", DbType.Int16, salesmanGrade.Period)
            DbCommandWrapper.AddInParameter("@Grade", DbType.Int16, salesmanGrade.Grade)
            DbCommandWrapper.AddInParameter("@Score", DbType.Decimal, salesmanGrade.Score)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, salesmanGrade.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanGrade.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, salesmanGrade.LastUpdatedBy)
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

            Dim salesmanGrade As SalesmanGrade = CType(obj, SalesmanGrade)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanGrade.ID)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanGrade.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, salesmanGrade.Year)
            DbCommandWrapper.AddInParameter("@Period", DbType.Int16, salesmanGrade.Period)
            DbCommandWrapper.AddInParameter("@Grade", DbType.Int16, salesmanGrade.Grade)
            DbCommandWrapper.AddInParameter("@Score", DbType.Decimal, salesmanGrade.Score)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, salesmanGrade.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanGrade.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanGrade.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, salesmanGrade.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanGrade

            Dim salesmanGrade As SalesmanGrade = New SalesmanGrade

            salesmanGrade.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then salesmanGrade.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then salesmanGrade.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then salesmanGrade.Period = CType(dr("Period"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Grade")) Then salesmanGrade.Grade = CType(dr("Grade"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Score")) Then salesmanGrade.Score = CType(dr("Score"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then salesmanGrade.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanGrade.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanGrade.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanGrade.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then salesmanGrade.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanGrade.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return salesmanGrade

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanGrade) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanGrade), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanGrade).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
