#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPSalesParameter Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 05/09/2007 - 15:36:02
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

    Public Class SAPSalesParameterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSAPSalesParameter"
        Private m_UpdateStatement As String = "up_UpdateSAPSalesParameter"
        Private m_RetrieveStatement As String = "up_RetrieveSAPSalesParameter"
        Private m_RetrieveListStatement As String = "up_RetrieveSAPSalesParameterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSAPSalesParameter"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sAPSalesParameter As SAPSalesParameter = Nothing
            While dr.Read

                sAPSalesParameter = Me.CreateObject(dr)

            End While

            Return sAPSalesParameter

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sAPSalesParameterList As ArrayList = New ArrayList

            While dr.Read
                Dim sAPSalesParameter As SAPSalesParameter = Me.CreateObject(dr)
                sAPSalesParameterList.Add(sAPSalesParameter)
            End While

            Return sAPSalesParameterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPSalesParameter As SAPSalesParameter = CType(obj, SAPSalesParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPSalesParameter.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPSalesParameter As SAPSalesParameter = CType(obj, SAPSalesParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, sAPSalesParameter.Code)
            DbCommandWrapper.AddInParameter("@Grade", DbType.AnsiString, sAPSalesParameter.Grade)
            DbCommandWrapper.AddInParameter("@Bobot", DbType.Int16, sAPSalesParameter.Bobot)
            DbCommandWrapper.AddInParameter("@SalesmanPoint", DbType.Decimal, sAPSalesParameter.SalesmanPoint)
            DbCommandWrapper.AddInParameter("@SalesCounterPoint", DbType.Decimal, sAPSalesParameter.SalesCounterPoint)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPSalesParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sAPSalesParameter.LastUpdateBy)
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

            Dim sAPSalesParameter As SAPSalesParameter = CType(obj, SAPSalesParameter)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPSalesParameter.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, sAPSalesParameter.Code)
            DbCommandWrapper.AddInParameter("@Grade", DbType.AnsiString, sAPSalesParameter.Grade)
            DbCommandWrapper.AddInParameter("@Bobot", DbType.Int16, sAPSalesParameter.Bobot)
            DbCommandWrapper.AddInParameter("@SalesmanPoint", DbType.Decimal, sAPSalesParameter.SalesmanPoint)
            DbCommandWrapper.AddInParameter("@SalesCounterPoint", DbType.Decimal, sAPSalesParameter.SalesCounterPoint)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPSalesParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sAPSalesParameter.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SAPSalesParameter

            Dim sAPSalesParameter As SAPSalesParameter = New SAPSalesParameter

            sAPSalesParameter.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then sAPSalesParameter.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Grade")) Then sAPSalesParameter.Grade = dr("Grade").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Bobot")) Then sAPSalesParameter.Bobot = CType(dr("Bobot"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanPoint")) Then sAPSalesParameter.SalesmanPoint = CType(dr("SalesmanPoint"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesCounterPoint")) Then sAPSalesParameter.SalesCounterPoint = CType(dr("SalesCounterPoint"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sAPSalesParameter.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sAPSalesParameter.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sAPSalesParameter.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sAPSalesParameter.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sAPSalesParameter.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sAPSalesParameter

        End Function

        Private Sub SetTableName()

            If Not (GetType(SAPSalesParameter) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SAPSalesParameter), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SAPSalesParameter).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

