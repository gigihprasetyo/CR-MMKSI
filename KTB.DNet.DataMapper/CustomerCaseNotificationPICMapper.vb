#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerCaseNotificationPIC Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 3/12/2021 - 8:46:57 AM
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

    Public Class CustomerCaseNotificationPICMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCustomerCaseNotificationPIC"
        Private m_UpdateStatement As String = "up_UpdateCustomerCaseNotificationPIC"
        Private m_RetrieveStatement As String = "up_RetrieveCustomerCaseNotificationPIC"
        Private m_RetrieveListStatement As String = "up_RetrieveCustomerCaseNotificationPICList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCustomerCaseNotificationPIC"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim customerCaseNotificationPIC As CustomerCaseNotificationPIC = Nothing
            While dr.Read

                customerCaseNotificationPIC = Me.CreateObject(dr)

            End While

            Return customerCaseNotificationPIC

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim customerCaseNotificationPICList As ArrayList = New ArrayList

            While dr.Read
                Dim customerCaseNotificationPIC As CustomerCaseNotificationPIC = Me.CreateObject(dr)
                customerCaseNotificationPICList.Add(customerCaseNotificationPIC)
            End While

            Return customerCaseNotificationPICList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerCaseNotificationPIC As CustomerCaseNotificationPIC = CType(obj, CustomerCaseNotificationPIC)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerCaseNotificationPIC.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerCaseNotificationPIC As CustomerCaseNotificationPIC = CType(obj, CustomerCaseNotificationPIC)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Category", DbType.String, customerCaseNotificationPIC.Category)
            DbCommandWrapper.AddInParameter("@SubCategory1", DbType.String, customerCaseNotificationPIC.SubCategory1)
            DbCommandWrapper.AddInParameter("@SubCategory2", DbType.String, customerCaseNotificationPIC.SubCategory2)
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, customerCaseNotificationPIC.JobPosition)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerCaseNotificationPIC.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, customerCaseNotificationPIC.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, customerCaseNotificationPIC.LastUpdateTime)


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

            Dim customerCaseNotificationPIC As CustomerCaseNotificationPIC = CType(obj, CustomerCaseNotificationPIC)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerCaseNotificationPIC.ID)
            DbCommandWrapper.AddInParameter("@Category", DbType.String, customerCaseNotificationPIC.Category)
            DbCommandWrapper.AddInParameter("@SubCategory1", DbType.String, customerCaseNotificationPIC.SubCategory1)
            DbCommandWrapper.AddInParameter("@SubCategory2", DbType.String, customerCaseNotificationPIC.SubCategory2)
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, customerCaseNotificationPIC.JobPosition)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerCaseNotificationPIC.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, customerCaseNotificationPIC.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, customerCaseNotificationPIC.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, customerCaseNotificationPIC.LastUpdateTime)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CustomerCaseNotificationPIC

            Dim customerCaseNotificationPIC As CustomerCaseNotificationPIC = New CustomerCaseNotificationPIC

            customerCaseNotificationPIC.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then customerCaseNotificationPIC.Category = dr("Category").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory1")) Then customerCaseNotificationPIC.SubCategory1 = dr("SubCategory1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategory2")) Then customerCaseNotificationPIC.SubCategory2 = dr("SubCategory2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition")) Then customerCaseNotificationPIC.JobPosition = dr("JobPosition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then customerCaseNotificationPIC.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then customerCaseNotificationPIC.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then customerCaseNotificationPIC.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then customerCaseNotificationPIC.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then customerCaseNotificationPIC.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return customerCaseNotificationPIC

        End Function

        Private Sub SetTableName()

            If Not (GetType(CustomerCaseNotificationPIC) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CustomerCaseNotificationPIC), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CustomerCaseNotificationPIC).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
