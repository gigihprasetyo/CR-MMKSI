#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalParameter Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/15/2020 - 2:12:24 PM
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

    Public Class DiscountProposalParameterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalParameter"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalParameter"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalParameter"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalParameterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalParameter"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalParameter As DiscountProposalParameter = Nothing
            While dr.Read

                discountProposalParameter = Me.CreateObject(dr)

            End While

            Return discountProposalParameter

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalParameterList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalParameter As DiscountProposalParameter = Me.CreateObject(dr)
                discountProposalParameterList.Add(discountProposalParameter)
            End While

            Return discountProposalParameterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalParameter As DiscountProposalParameter = CType(obj, DiscountProposalParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalParameter.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalParameter As DiscountProposalParameter = CType(obj, DiscountProposalParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ParameterType", DbType.Int32, discountProposalParameter.ParameterType)
            DbCommandWrapper.AddInParameter("@ParameterName", DbType.AnsiString, discountProposalParameter.ParameterName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, discountProposalParameter.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalParameter.LastUpdateBy)
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

            Dim discountProposalParameter As DiscountProposalParameter = CType(obj, DiscountProposalParameter)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalParameter.ID)
            DbCommandWrapper.AddInParameter("@ParameterType", DbType.Int32, discountProposalParameter.ParameterType)
            DbCommandWrapper.AddInParameter("@ParameterName", DbType.AnsiString, discountProposalParameter.ParameterName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, discountProposalParameter.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalParameter.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalParameter

            Dim discountProposalParameter As DiscountProposalParameter = New DiscountProposalParameter

            discountProposalParameter.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParameterType")) Then discountProposalParameter.ParameterType = CType(dr("ParameterType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParameterName")) Then discountProposalParameter.ParameterName = dr("ParameterName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then discountProposalParameter.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalParameter.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalParameter.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalParameter.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalParameter.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalParameter.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return discountProposalParameter

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalParameter) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalParameter), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalParameter).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
