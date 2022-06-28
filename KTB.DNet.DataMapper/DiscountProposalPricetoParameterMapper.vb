#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailApprovaltoSPL Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/24/2020 - 11:04:49 AM
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

    Public Class DiscountProposalPricetoParameterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalPricetoParameter"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalPricetoParameter"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalPricetoParameter"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalPricetoParameterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalPricetoParameter"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim DiscountProposalPricetoParameter As DiscountProposalPricetoParameter = Nothing
            While dr.Read

                DiscountProposalPricetoParameter = Me.CreateObject(dr)

            End While

            Return DiscountProposalPricetoParameter

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim DiscountProposalPricetoParameterList As ArrayList = New ArrayList

            While dr.Read
                Dim DiscountProposalPricetoParameter As DiscountProposalPricetoParameter = Me.CreateObject(dr)
                DiscountProposalPricetoParameterList.Add(DiscountProposalPricetoParameter)
            End While

            Return DiscountProposalPricetoParameterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim DiscountProposalPricetoParameter As DiscountProposalPricetoParameter = CType(obj, DiscountProposalPricetoParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, DiscountProposalPricetoParameter.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim _discountProposalPricetoParameter As DiscountProposalPricetoParameter = CType(obj, DiscountProposalPricetoParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, _discountProposalPricetoParameter.Amount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, _discountProposalPricetoParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, _discountProposalPricetoParameter.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalParameterID", DbType.Int32, Me.GetRefObject(_discountProposalPricetoParameter.DiscountProposalParameter))
            DbCommandWrapper.AddInParameter("@DiscountProposalDetailPriceID", DbType.Int32, Me.GetRefObject(_discountProposalPricetoParameter.DiscountProposalDetailPrice))

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

            Dim _discountProposalPricetoParameter As DiscountProposalPricetoParameter = CType(obj, DiscountProposalPricetoParameter)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, _discountProposalPricetoParameter.ID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, _discountProposalPricetoParameter.Amount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, _discountProposalPricetoParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, _discountProposalPricetoParameter.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalParameterID", DbType.Int32, Me.GetRefObject(_discountProposalPricetoParameter.DiscountProposalParameter))
            DbCommandWrapper.AddInParameter("@DiscountProposalDetailPriceID", DbType.Int32, Me.GetRefObject(_discountProposalPricetoParameter.DiscountProposalDetailPrice))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalPricetoParameter

            Dim _discountProposalPricetoParameter As DiscountProposalPricetoParameter = New DiscountProposalPricetoParameter

            _discountProposalPricetoParameter.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then _discountProposalPricetoParameter.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then _discountProposalPricetoParameter.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then _discountProposalPricetoParameter.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then _discountProposalPricetoParameter.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then _discountProposalPricetoParameter.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then _discountProposalPricetoParameter.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalParameterID")) Then
                _discountProposalPricetoParameter.DiscountProposalParameter = New DiscountProposalParameter(CType(dr("DiscountProposalParameterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalDetailPriceID")) Then
                _discountProposalPricetoParameter.DiscountProposalDetailPrice = New DiscountProposalDetailPrice(CType(dr("DiscountProposalDetailPriceID"), Integer))
            End If

            Return _discountProposalPricetoParameter

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalPricetoParameter) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalPricetoParameter), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalPricetoParameter).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
