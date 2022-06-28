
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceClasification Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/6/2018 - 1:57:52 PM
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

    Public Class CcCSPerformanceClasificationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcCSPerformanceClasification"
        Private m_UpdateStatement As String = "up_UpdateCcCSPerformanceClasification"
        Private m_RetrieveStatement As String = "up_RetrieveCcCSPerformanceClasification"
        Private m_RetrieveListStatement As String = "up_RetrieveCcCSPerformanceClasificationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcCSPerformanceClasification"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccCSPerformanceClasification As CcCSPerformanceClasification = Nothing
            While dr.Read

                ccCSPerformanceClasification = Me.CreateObject(dr)

            End While

            Return ccCSPerformanceClasification

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccCSPerformanceClasificationList As ArrayList = New ArrayList

            While dr.Read
                Dim ccCSPerformanceClasification As CcCSPerformanceClasification = Me.CreateObject(dr)
                ccCSPerformanceClasificationList.Add(ccCSPerformanceClasification)
            End While

            Return ccCSPerformanceClasificationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceClasification As CcCSPerformanceClasification = CType(obj, CcCSPerformanceClasification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceClasification.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceClasification As CcCSPerformanceClasification = CType(obj, CcCSPerformanceClasification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceSubParameterID", DbType.Int32, ccCSPerformanceClasification.CcCSPerformanceSubParameterID)


            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, ccCSPerformanceClasification.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, ccCSPerformanceClasification.Name)
            DbCommandWrapper.AddInParameter("@Weight", DbType.Int32, ccCSPerformanceClasification.Weight)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceClasification.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccCSPerformanceClasification.LastUpdateBy)
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

            Dim ccCSPerformanceClasification As CcCSPerformanceClasification = CType(obj, CcCSPerformanceClasification)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceClasification.ID)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceSubParameterID", DbType.Int32, ccCSPerformanceClasification.CcCSPerformanceSubParameterID)

            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, ccCSPerformanceClasification.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, ccCSPerformanceClasification.Name)
            DbCommandWrapper.AddInParameter("@Weight", DbType.Int32, ccCSPerformanceClasification.Weight)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceClasification.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccCSPerformanceClasification.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcCSPerformanceClasification

            Dim ccCSPerformanceClasification As CcCSPerformanceClasification = New CcCSPerformanceClasification

            ccCSPerformanceClasification.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceSubParameterID")) Then ccCSPerformanceClasification.CcCSPerformanceSubParameterID = CType(dr("CcCSPerformanceSubParameterID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceSubParameterID")) Then
                ccCSPerformanceClasification.CcCSPerformanceSubParameter = New CcCSPerformanceSubParameter(CType(dr("CcCSPerformanceSubParameterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceMasterID")) Then ccCSPerformanceClasification.CcCSPerformanceMasterID = CType(dr("CcCSPerformanceMasterID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceMasterID")) Then
                ccCSPerformanceClasification.CcCSPerformanceMaster = New CcCSPerformanceMaster(CType(dr("CcCSPerformanceMasterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then ccCSPerformanceClasification.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then ccCSPerformanceClasification.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Weight")) Then ccCSPerformanceClasification.Weight = CType(dr("Weight"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccCSPerformanceClasification.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccCSPerformanceClasification.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccCSPerformanceClasification.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccCSPerformanceClasification.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccCSPerformanceClasification.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccCSPerformanceClasification

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcCSPerformanceClasification) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcCSPerformanceClasification), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcCSPerformanceClasification).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

