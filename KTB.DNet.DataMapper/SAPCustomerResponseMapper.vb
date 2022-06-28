
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPCustomerResponse Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 7/10/2017 - 10:24:11 AM
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

    Public Class SAPCustomerResponseMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSAPCustomerResponse"
        Private m_UpdateStatement As String = "up_UpdateSAPCustomerResponse"
        Private m_RetrieveStatement As String = "up_RetrieveSAPCustomerResponse"
        Private m_RetrieveListStatement As String = "up_RetrieveSAPCustomerResponseList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSAPCustomerResponse"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sAPCustomerResponse As SAPCustomerResponse = Nothing
            While dr.Read

                sAPCustomerResponse = Me.CreateObject(dr)

            End While

            Return sAPCustomerResponse

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sAPCustomerResponseList As ArrayList = New ArrayList

            While dr.Read
                Dim sAPCustomerResponse As SAPCustomerResponse = Me.CreateObject(dr)
                sAPCustomerResponseList.Add(sAPCustomerResponse)
            End While

            Return sAPCustomerResponseList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPCustomerResponse As SAPCustomerResponse = CType(obj, SAPCustomerResponse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPCustomerResponse.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPCustomerResponse As SAPCustomerResponse = CType(obj, SAPCustomerResponse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, sAPCustomerResponse.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, sAPCustomerResponse.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sAPCustomerResponse.Status)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Int16, sAPCustomerResponse.IsSend)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPCustomerResponse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sAPCustomerResponse.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, sAPCustomerResponse.LastUpdateTIme)

            'DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, sAPCustomerResponse.SAPCustomerID)
            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, Me.GetRefObject(sAPCustomerResponse.SAPCustomer))

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

            Dim sAPCustomerResponse As SAPCustomerResponse = CType(obj, SAPCustomerResponse)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPCustomerResponse.ID)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, sAPCustomerResponse.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, sAPCustomerResponse.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sAPCustomerResponse.Status)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Int16, sAPCustomerResponse.IsSend)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPCustomerResponse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sAPCustomerResponse.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, sAPCustomerResponse.LastUpdateTIme)


            'DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, sAPCustomerResponse.SAPCustomerID)
            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, Me.GetRefObject(sAPCustomerResponse.SAPCustomer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SAPCustomerResponse

            Dim sAPCustomerResponse As SAPCustomerResponse = New SAPCustomerResponse

            sAPCustomerResponse.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then sAPCustomerResponse.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then sAPCustomerResponse.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sAPCustomerResponse.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSend")) Then sAPCustomerResponse.IsSend = CType(dr("IsSend"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sAPCustomerResponse.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sAPCustomerResponse.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sAPCustomerResponse.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sAPCustomerResponse.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTIme")) Then sAPCustomerResponse.LastUpdateTIme = CType(dr("LastUpdateTIme"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("SAPCustomerID")) Then
            '    sAPCustomerResponse.SAPCustomerID = CType(dr("SAPCustomerID"), Integer)
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("SAPCustomerID")) Then
                sAPCustomerResponse.SAPCustomer = New SAPCustomer(CType(dr("SAPCustomerID"), Integer))
            End If
            Return sAPCustomerResponse

        End Function

        Private Sub SetTableName()

            If Not (GetType(SAPCustomerResponse) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SAPCustomerResponse), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SAPCustomerResponse).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

