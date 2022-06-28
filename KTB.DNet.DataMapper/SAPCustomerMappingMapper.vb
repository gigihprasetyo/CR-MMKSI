#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPCustomerMapping Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 10:42:15 AM
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

    Public Class SAPCustomerMappingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSAPCustomerMapping"
        Private m_UpdateStatement As String = "up_UpdateSAPCustomerMapping"
        Private m_RetrieveStatement As String = "up_RetrieveSAPCustomerMapping"
        Private m_RetrieveListStatement As String = "up_RetrieveSAPCustomerMappingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSAPCustomerMapping"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim SAPCustomerMapping As SAPCustomerMapping = Nothing
            While dr.Read

                SAPCustomerMapping = Me.CreateObject(dr)

            End While

            Return SAPCustomerMapping

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim SAPCustomerMappingList As ArrayList = New ArrayList

            While dr.Read
                Dim SAPCustomerMapping As SAPCustomerMapping = Me.CreateObject(dr)
                SAPCustomerMappingList.Add(SAPCustomerMapping)
            End While

            Return SAPCustomerMappingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim SAPCustomerMapping As SAPCustomerMapping = CType(obj, SAPCustomerMapping)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, SAPCustomerMapping.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim SAPCustomerMapping As SAPCustomerMapping = CType(obj, SAPCustomerMapping)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SourceLead", DbType.String, SAPCustomerMapping.SourceLead)
            DbCommandWrapper.AddInParameter("@SourceInformation", DbType.String, SAPCustomerMapping.SourceInformation)
            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, Me.GetRefObject(SAPCustomerMapping.SAPCustomer))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, SAPCustomerMapping.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, SAPCustomerMapping.LastUpdateBy)
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

            Dim SAPCustomerMapping As SAPCustomerMapping = CType(obj, SAPCustomerMapping)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, SAPCustomerMapping.ID)
            DbCommandWrapper.AddInParameter("@SourceLead", DbType.String, SAPCustomerMapping.SourceLead)
            DbCommandWrapper.AddInParameter("@SourceInformation", DbType.String, SAPCustomerMapping.SourceInformation)
            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, Me.GetRefObject(SAPCustomerMapping.SAPCustomer))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, SAPCustomerMapping.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, SAPCustomerMapping.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SAPCustomerMapping

            Dim SAPCustomerMapping As SAPCustomerMapping = New SAPCustomerMapping

            SAPCustomerMapping.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SourceLead")) Then SAPCustomerMapping.SourceLead = CType(dr("SourceLead"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("SourceInformation")) Then SAPCustomerMapping.SourceInformation = CType(dr("SourceInformation"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then SAPCustomerMapping.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then SAPCustomerMapping.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then SAPCustomerMapping.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then SAPCustomerMapping.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then SAPCustomerMapping.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SAPCustomerID")) Then
                SAPCustomerMapping.SAPCustomer = New SAPCustomer(CType(dr("SAPCustomerID"), Integer))
            End If
            Return SAPCustomerMapping

        End Function

        Private Sub SetTableName()

            If Not (GetType(SAPCustomerMapping) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SAPCustomerMapping), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SAPCustomerMapping).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

