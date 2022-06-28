#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformValidation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:22:30 AM
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

    Public Class UniformValidationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertUniformValidation"
        Private m_UpdateStatement As String = "up_UpdateUniformValidation"
        Private m_RetrieveStatement As String = "up_RetrieveUniformValidation"
        Private m_RetrieveListStatement As String = "up_RetrieveUniformValidationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteUniformValidation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim uniformValidation As UniformValidation = Nothing
            While dr.Read

                uniformValidation = Me.CreateObject(dr)

            End While

            Return uniformValidation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim uniformValidationList As ArrayList = New ArrayList

            While dr.Read
                Dim uniformValidation As UniformValidation = Me.CreateObject(dr)
                uniformValidationList.Add(uniformValidation)
            End While

            Return uniformValidationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformValidation As UniformValidation = CType(obj, UniformValidation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformValidation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformValidation As UniformValidation = CType(obj, UniformValidation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, uniformValidation.DealerCode)
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, uniformValidation.SalesmanId)
            DbCommandWrapper.AddInParameter("@UniformCode", DbType.AnsiStringFixedLength, uniformValidation.UniformCode)
            DbCommandWrapper.AddInParameter("@UniformSize", DbType.AnsiStringFixedLength, uniformValidation.UniformSize)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, uniformValidation.Qty)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Decimal, uniformValidation.RetailPrice)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Decimal, uniformValidation.TotalAmount)
            DbCommandWrapper.AddInParameter("@RequestStatus", DbType.AnsiStringFixedLength, uniformValidation.RequestStatus)
            DbCommandWrapper.AddInParameter("@ValidationStatus", DbType.AnsiStringFixedLength, uniformValidation.ValidationStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformValidation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, uniformValidation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@UniformDistributionId", DbType.Int32, uniformValidation.UniformDistributionId)

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

            Dim uniformValidation As UniformValidation = CType(obj, UniformValidation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformValidation.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, uniformValidation.DealerCode)
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, uniformValidation.SalesmanId)
            DbCommandWrapper.AddInParameter("@UniformCode", DbType.AnsiStringFixedLength, uniformValidation.UniformCode)
            DbCommandWrapper.AddInParameter("@UniformSize", DbType.AnsiStringFixedLength, uniformValidation.UniformSize)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, uniformValidation.Qty)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Decimal, uniformValidation.RetailPrice)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Decimal, uniformValidation.TotalAmount)
            DbCommandWrapper.AddInParameter("@RequestStatus", DbType.AnsiStringFixedLength, uniformValidation.RequestStatus)
            DbCommandWrapper.AddInParameter("@ValidationStatus", DbType.AnsiStringFixedLength, uniformValidation.ValidationStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformValidation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, uniformValidation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@UniformDistributionId", DbType.Int32, uniformValidation.UniformDistributionId)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As UniformValidation

            Dim uniformValidation As UniformValidation = New UniformValidation

            uniformValidation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then uniformValidation.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanId")) Then uniformValidation.SalesmanId = CType(dr("SalesmanId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformCode")) Then uniformValidation.UniformCode = dr("UniformCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UniformSize")) Then uniformValidation.UniformSize = dr("UniformSize").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then uniformValidation.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then uniformValidation.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then uniformValidation.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestStatus")) Then uniformValidation.RequestStatus = dr("RequestStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidationStatus")) Then uniformValidation.ValidationStatus = dr("ValidationStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then uniformValidation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then uniformValidation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then uniformValidation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then uniformValidation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then uniformValidation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformDistributionId")) Then
                uniformValidation.UniformDistributionId = CType(dr("UniformDistributionId"), Integer)
            End If

            Return uniformValidation

        End Function

        Private Sub SetTableName()

            If Not (GetType(UniformValidation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(UniformValidation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(UniformValidation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

