
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceParameter Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/6/2018 - 1:30:00 PM
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

    Public Class CcCSPerformanceParameterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcCSPerformanceParameter"
        Private m_UpdateStatement As String = "up_UpdateCcCSPerformanceParameter"
        Private m_RetrieveStatement As String = "up_RetrieveCcCSPerformanceParameter"
        Private m_RetrieveListStatement As String = "up_RetrieveCcCSPerformanceParameterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcCSPerformanceParameter"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccCSPerformanceParameter As CcCSPerformanceParameter = Nothing
            While dr.Read

                ccCSPerformanceParameter = Me.CreateObject(dr)

            End While

            Return ccCSPerformanceParameter

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccCSPerformanceParameterList As ArrayList = New ArrayList

            While dr.Read
                Dim ccCSPerformanceParameter As CcCSPerformanceParameter = Me.CreateObject(dr)
                ccCSPerformanceParameterList.Add(ccCSPerformanceParameter)
            End While

            Return ccCSPerformanceParameterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceParameter As CcCSPerformanceParameter = CType(obj, CcCSPerformanceParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceParameter.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceParameter As CcCSPerformanceParameter = CType(obj, CcCSPerformanceParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceMasterID", DbType.Int32, ccCSPerformanceParameter.CcCSPerformanceMasterID)
            DbCommandWrapper.AddInParameter("@FunctionName", DbType.Int16, ccCSPerformanceParameter.FunctionName)
            DbCommandWrapper.AddInParameter("@ParentID", DbType.Int32, ccCSPerformanceParameter.ParentID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, ccCSPerformanceParameter.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, ccCSPerformanceParameter.Name)
            DbCommandWrapper.AddInParameter("@Weight", DbType.Decimal, ccCSPerformanceParameter.Weight)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, ccCSPerformanceParameter.Sequence)
            DbCommandWrapper.AddInParameter("@level", DbType.Int16, ccCSPerformanceParameter.level)
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int32, ccCSPerformanceParameter.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccCSPerformanceParameter.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccCSPerformanceParameter.LastUpdateBy)
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

            Dim ccCSPerformanceParameter As CcCSPerformanceParameter = CType(obj, CcCSPerformanceParameter)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceParameter.ID)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceMasterID", DbType.Int32, ccCSPerformanceParameter.CcCSPerformanceMasterID)
            DbCommandWrapper.AddInParameter("@FunctionName", DbType.Int16, ccCSPerformanceParameter.FunctionName)
            DbCommandWrapper.AddInParameter("@ParentID", DbType.Int32, ccCSPerformanceParameter.ParentID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, ccCSPerformanceParameter.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, ccCSPerformanceParameter.Name)
            DbCommandWrapper.AddInParameter("@Weight", DbType.Decimal, ccCSPerformanceParameter.Weight)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, ccCSPerformanceParameter.Sequence)
            DbCommandWrapper.AddInParameter("@level", DbType.Int16, ccCSPerformanceParameter.level)
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int32, ccCSPerformanceParameter.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccCSPerformanceParameter.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccCSPerformanceParameter.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcCSPerformanceParameter

            Dim ccCSPerformanceParameter As CcCSPerformanceParameter = New CcCSPerformanceParameter

            ccCSPerformanceParameter.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceMasterID")) Then
                ccCSPerformanceParameter.CcCSPerformanceMaster = New CcCSPerformanceMaster(CType(dr("CcCSPerformanceMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceMasterID")) Then ccCSPerformanceParameter.CcCSPerformanceMasterID = CType(dr("CcCSPerformanceMasterID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("FunctionName")) Then ccCSPerformanceParameter.FunctionName = CType(dr("FunctionName"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ParentID")) Then ccCSPerformanceParameter.ParentID = CType(dr("ParentID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then ccCSPerformanceParameter.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then ccCSPerformanceParameter.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Weight")) Then ccCSPerformanceParameter.Weight = CType(dr("Weight"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then ccCSPerformanceParameter.Sequence = CType(dr("Sequence"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("level")) Then ccCSPerformanceParameter.level = CType(dr("level"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ccCSPerformanceParameter.Status = CType(dr("Status"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then
                ccCSPerformanceParameter.CcCustomerCategoryID = CType(dr("CcCustomerCategoryID"), Integer)
                ccCSPerformanceParameter.CcCustomerCategory = New CcCustomerCategory(ccCSPerformanceParameter.CcCustomerCategoryID)
            Else
                ccCSPerformanceParameter.CcCustomerCategoryID = 0
            End If


            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccCSPerformanceParameter.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccCSPerformanceParameter.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccCSPerformanceParameter.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccCSPerformanceParameter.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccCSPerformanceParameter.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccCSPerformanceParameter

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcCSPerformanceParameter) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcCSPerformanceParameter), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcCSPerformanceParameter).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

