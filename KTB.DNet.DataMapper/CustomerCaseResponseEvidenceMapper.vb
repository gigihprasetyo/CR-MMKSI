
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerCaseResponseEvidence Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 7/10/2017 - 10:23:33 AM
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

    Public Class CustomerCaseResponseEvidenceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCustomerCaseResponseEvidence"
        Private m_UpdateStatement As String = "up_UpdateCustomerCaseResponseEvidence"
        Private m_RetrieveStatement As String = "up_RetrieveCustomerCaseResponseEvidence"
        Private m_RetrieveListStatement As String = "up_RetrieveCustomerCaseResponseEvidenceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCustomerCaseResponseEvidence"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim CustomerCaseResponseEvidence As CustomerCaseResponseEvidence = Nothing
            While dr.Read

                CustomerCaseResponseEvidence = Me.CreateObject(dr)

            End While

            Return CustomerCaseResponseEvidence

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim CustomerCaseResponseEvidenceList As ArrayList = New ArrayList

            While dr.Read
                Dim CustomerCaseResponseEvidence As CustomerCaseResponseEvidence = Me.CreateObject(dr)
                CustomerCaseResponseEvidenceList.Add(CustomerCaseResponseEvidence)
            End While

            Return CustomerCaseResponseEvidenceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim CustomerCaseResponseEvidence As CustomerCaseResponseEvidence = CType(obj, CustomerCaseResponseEvidence)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, CustomerCaseResponseEvidence.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim CustomerCaseResponseEvidence As CustomerCaseResponseEvidence = CType(obj, CustomerCaseResponseEvidence)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@EvidenceFile", DbType.AnsiString, CustomerCaseResponseEvidence.EvidenceFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, CustomerCaseResponseEvidence.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, CustomerCaseResponseEvidence.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, CustomerCaseResponseEvidence.LastUpdateTIme)

            DbCommandWrapper.AddInParameter("@CustomerCaseResponseID", DbType.Int32, Me.GetRefObject(CustomerCaseResponseEvidence.CustomerCaseResponse))

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

            Dim CustomerCaseResponseEvidence As CustomerCaseResponseEvidence = CType(obj, CustomerCaseResponseEvidence)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, CustomerCaseResponseEvidence.ID)
            DbCommandWrapper.AddInParameter("@EvidenceFile", DbType.AnsiString, CustomerCaseResponseEvidence.EvidenceFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, CustomerCaseResponseEvidence.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, CustomerCaseResponseEvidence.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, DateTime.Now)

            DbCommandWrapper.AddInParameter("@CustomerCaseResponseID", DbType.Int32, Me.GetRefObject(CustomerCaseResponseEvidence.CustomerCaseResponse))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CustomerCaseResponseEvidence

            Dim CustomerCaseResponseEvidence As CustomerCaseResponseEvidence = New CustomerCaseResponseEvidence

            CustomerCaseResponseEvidence.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then CustomerCaseResponseEvidence.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then CustomerCaseResponseEvidence.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then CustomerCaseResponseEvidence.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then CustomerCaseResponseEvidence.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTIme")) Then CustomerCaseResponseEvidence.LastUpdateTIme = CType(dr("LastUpdateTIme"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCaseResponseID")) Then
                CustomerCaseResponseEvidence.CustomerCaseResponse = New CustomerCaseResponse(CType(dr("CustomerCaseResponseID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceFile")) Then CustomerCaseResponseEvidence.EvidenceFile = dr("EvidenceFile").ToString

            Return CustomerCaseResponseEvidence

        End Function

        Private Sub SetTableName()

            If Not (GetType(CustomerCaseResponseEvidence) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CustomerCaseResponseEvidence), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CustomerCaseResponseEvidence).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

