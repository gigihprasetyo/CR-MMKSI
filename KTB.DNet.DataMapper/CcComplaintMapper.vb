
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcComplaint Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 16/03/2020 - 13:40:57
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

    Public Class CcComplaintMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcComplaint"
        Private m_UpdateStatement As String = "up_UpdateCcComplaint"
        Private m_RetrieveStatement As String = "up_RetrieveCcComplaint"
        Private m_RetrieveListStatement As String = "up_RetrieveCcComplaintList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcComplaint"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccComplaint As CcComplaint = Nothing
            While dr.Read

                ccComplaint = Me.CreateObject(dr)

            End While

            Return ccComplaint

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccComplaintList As ArrayList = New ArrayList

            While dr.Read
                Dim ccComplaint As CcComplaint = Me.CreateObject(dr)
                ccComplaintList.Add(ccComplaint)
            End While

            Return ccComplaintList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccComplaint As CcComplaint = CType(obj, CcComplaint)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccComplaint.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccComplaint As CcComplaint = CType(obj, CcComplaint)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CcSurveyID", DbType.Int32, ccComplaint.CcSurveyID)
            DbCommandWrapper.AddInParameter("@CcSurveyQuetionnareID", DbType.Int32, ccComplaint.CcSurveyQuetionnareID)
            DbCommandWrapper.AddInParameter("@CcScenarioID", DbType.Int32, ccComplaint.CcScenarioID)
            DbCommandWrapper.AddInParameter("@CcFactorID", DbType.Int32, ccComplaint.CcFactorID)
            DbCommandWrapper.AddInParameter("@CcAttributeID", DbType.Int32, ccComplaint.CcAttributeID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ccComplaint.Description)
            DbCommandWrapper.AddInParameter("@Category", DbType.AnsiString, ccComplaint.Category)
            DbCommandWrapper.AddInParameter("@SubCategory1", DbType.AnsiString, ccComplaint.SubCategory1)
            DbCommandWrapper.AddInParameter("@SubCategory2", DbType.AnsiString, ccComplaint.SubCategory2)
            DbCommandWrapper.AddInParameter("@SubCategory3", DbType.AnsiString, ccComplaint.SubCategory3)
            DbCommandWrapper.AddInParameter("@SubCategory4", DbType.AnsiString, ccComplaint.SubCategory4)
            DbCommandWrapper.AddInParameter("@Salesforceid", DbType.String, ccComplaint.Salesforceid)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccComplaint.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccComplaint.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccComplaint.LastUpdateBy)
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

            Dim ccComplaint As CcComplaint = CType(obj, CcComplaint)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccComplaint.ID)
            DbCommandWrapper.AddInParameter("@CcSurveyID", DbType.Int32, ccComplaint.CcSurveyID)
            DbCommandWrapper.AddInParameter("@CcSurveyQuetionnareID", DbType.Int32, ccComplaint.CcSurveyQuetionnareID)
            DbCommandWrapper.AddInParameter("@CcScenarioID", DbType.Int32, ccComplaint.CcScenarioID)
            DbCommandWrapper.AddInParameter("@CcFactorID", DbType.Int32, ccComplaint.CcFactorID)
            DbCommandWrapper.AddInParameter("@CcAttributeID", DbType.Int32, ccComplaint.CcAttributeID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ccComplaint.Description)
            DbCommandWrapper.AddInParameter("@Category", DbType.AnsiString, ccComplaint.Category)
            DbCommandWrapper.AddInParameter("@SubCategory1", DbType.AnsiString, ccComplaint.SubCategory1)
            DbCommandWrapper.AddInParameter("@SubCategory2", DbType.AnsiString, ccComplaint.SubCategory2)
            DbCommandWrapper.AddInParameter("@SubCategory3", DbType.AnsiString, ccComplaint.SubCategory3)
            DbCommandWrapper.AddInParameter("@SubCategory4", DbType.AnsiString, ccComplaint.SubCategory4)
            DbCommandWrapper.AddInParameter("@Salesforceid", DbType.String, ccComplaint.Salesforceid)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccComplaint.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccComplaint.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccComplaint.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcComplaint

            Dim ccComplaint As CcComplaint = New CcComplaint

            ccComplaint.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcSurveyID")) Then ccComplaint.CcSurveyID = CType(dr("CcSurveyID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcSurveyQuetionnareID")) Then ccComplaint.CcSurveyQuetionnareID = CType(dr("CcSurveyQuetionnareID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcScenarioID")) Then ccComplaint.CcScenarioID = CType(dr("CcScenarioID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcFactorID")) Then ccComplaint.CcFactorID = CType(dr("CcFactorID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcAttributeID")) Then ccComplaint.CcAttributeID = CType(dr("CcAttributeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then ccComplaint.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then ccComplaint.Category = dr("Category").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory1")) Then ccComplaint.SubCategory1 = dr("SubCategory1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory2")) Then ccComplaint.SubCategory2 = dr("SubCategory2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory3")) Then ccComplaint.SubCategory3 = dr("SubCategory3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory4")) Then ccComplaint.SubCategory4 = dr("SubCategory4").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Salesforceid")) Then ccComplaint.Salesforceid = dr("Salesforceid").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ccComplaint.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccComplaint.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccComplaint.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccComplaint.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccComplaint.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccComplaint.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccComplaint

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcComplaint) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcComplaint), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcComplaint).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

