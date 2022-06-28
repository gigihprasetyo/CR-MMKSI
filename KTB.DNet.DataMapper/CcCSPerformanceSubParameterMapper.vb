
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceSubParameter Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/6/2018 - 1:43:28 PM
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

    Public Class CcCSPerformanceSubParameterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcCSPerformanceSubParameter"
        Private m_UpdateStatement As String = "up_UpdateCcCSPerformanceSubParameter"
        Private m_RetrieveStatement As String = "up_RetrieveCcCSPerformanceSubParameter"
        Private m_RetrieveListStatement As String = "up_RetrieveCcCSPerformanceSubParameterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcCSPerformanceSubParameter"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccCSPerformanceSubParameter As CcCSPerformanceSubParameter = Nothing
            While dr.Read

                ccCSPerformanceSubParameter = Me.CreateObject(dr)

            End While

            Return ccCSPerformanceSubParameter

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccCSPerformanceSubParameterList As ArrayList = New ArrayList

            While dr.Read
                Dim ccCSPerformanceSubParameter As CcCSPerformanceSubParameter = Me.CreateObject(dr)
                ccCSPerformanceSubParameterList.Add(ccCSPerformanceSubParameter)
            End While

            Return ccCSPerformanceSubParameterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceSubParameter As CcCSPerformanceSubParameter = CType(obj, CcCSPerformanceSubParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceSubParameter.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceSubParameter As CcCSPerformanceSubParameter = CType(obj, CcCSPerformanceSubParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceParameterID", DbType.Int32, ccCSPerformanceSubParameter.CcCSPerformanceParameter.ID)
            'DbCommandWrapper.AddInParameter("@CcCSPerformanceParameterID", DbType.Int32, Me.GetRefObject(ccCSPerformanceSubParameter.CcCSPerformanceParameter))
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, ccCSPerformanceSubParameter.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, ccCSPerformanceSubParameter.Name)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, ccCSPerformanceSubParameter.Sequence)
            'DbCommandWrapper.AddInParameter("@MaxValue", DbType.Int32, ccCSPerformanceSubParameter.MaxValue)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, ccCSPerformanceSubParameter.Type)
            DbCommandWrapper.AddInParameter("@FunctionName", DbType.AnsiString, ccCSPerformanceSubParameter.FunctionName)
            DbCommandWrapper.AddInParameter("@Weight", DbType.Decimal, ccCSPerformanceSubParameter.Weight)

            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ccCSPerformanceSubParameter.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceSubParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccCSPerformanceSubParameter.LastUpdateBy)
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

            Dim ccCSPerformanceSubParameter As CcCSPerformanceSubParameter = CType(obj, CcCSPerformanceSubParameter)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceSubParameter.ID)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceParameterID", DbType.Int32, ccCSPerformanceSubParameter.CcCSPerformanceParameter.ID)
            'DbCommandWrapper.AddInParameter("@CcCSPerformanceParameterID", DbType.Int32, Me.GetRefObject(ccCSPerformanceSubParameter.CcCSPerformanceParameter))
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, ccCSPerformanceSubParameter.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, ccCSPerformanceSubParameter.Name)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, ccCSPerformanceSubParameter.Sequence)
            'DbCommandWrapper.AddInParameter("@MaxValue", DbType.Int32, ccCSPerformanceSubParameter.MaxValue)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, ccCSPerformanceSubParameter.Type)
            DbCommandWrapper.AddInParameter("@FunctionName", DbType.AnsiString, ccCSPerformanceSubParameter.FunctionName)
            DbCommandWrapper.AddInParameter("@Weight", DbType.Decimal, ccCSPerformanceSubParameter.Weight)

            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ccCSPerformanceSubParameter.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceSubParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccCSPerformanceSubParameter.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcCSPerformanceSubParameter

            Dim ccCSPerformanceSubParameter As CcCSPerformanceSubParameter = New CcCSPerformanceSubParameter

            ccCSPerformanceSubParameter.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceParameterID")) Then
                ccCSPerformanceSubParameter.CcCSPerformanceParameter = New CcCSPerformanceParameter(CType(dr("CcCSPerformanceParameterID"), Integer))
            End If


            'If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceMasterID")) Then ccCSPerformanceSubParameter.CcCSPerformanceMasterID = CType(dr("CcCSPerformanceMasterID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceMasterID")) Then
                ccCSPerformanceSubParameter.CcCSPerformanceMaster = New CcCSPerformanceMaster(CType(dr("CcCSPerformanceMasterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then ccCSPerformanceSubParameter.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then ccCSPerformanceSubParameter.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then ccCSPerformanceSubParameter.Sequence = CType(dr("Sequence"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("MaxValue")) Then ccCSPerformanceSubParameter.MaxValue = CType(dr("MaxValue"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then ccCSPerformanceSubParameter.Type = CType(dr("Type"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FunctionName")) Then ccCSPerformanceSubParameter.FunctionName = dr("FunctionName").ToString()
            If Not dr.IsDBNull(dr.GetOrdinal("Weight")) Then ccCSPerformanceSubParameter.Weight = CType(dr("Weight"), Decimal)

            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then ccCSPerformanceSubParameter.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccCSPerformanceSubParameter.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccCSPerformanceSubParameter.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccCSPerformanceSubParameter.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccCSPerformanceSubParameter.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccCSPerformanceSubParameter.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccCSPerformanceSubParameter

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcCSPerformanceSubParameter) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcCSPerformanceSubParameter), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcCSPerformanceSubParameter).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

