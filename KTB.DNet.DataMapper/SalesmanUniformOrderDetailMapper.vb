#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUniformOrderDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/21/2007 - 4:47:37 PM
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

    Public Class SalesmanUniformOrderDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanUniformOrderDetail"
        Private m_UpdateStatement As String = "up_UpdateSalesmanUniformOrderDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanUniformOrderDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanUniformOrderDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanUniformOrderDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanUniformOrderDetail As SalesmanUniformOrderDetail = Nothing
            While dr.Read

                salesmanUniformOrderDetail = Me.CreateObject(dr)

            End While

            Return salesmanUniformOrderDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanUniformOrderDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanUniformOrderDetail As SalesmanUniformOrderDetail = Me.CreateObject(dr)
                salesmanUniformOrderDetailList.Add(salesmanUniformOrderDetail)
            End While

            Return salesmanUniformOrderDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUniformOrderDetail As SalesmanUniformOrderDetail = CType(obj, SalesmanUniformOrderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUniformOrderDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUniformOrderDetail As SalesmanUniformOrderDetail = CType(obj, SalesmanUniformOrderDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@UniformSize", DbType.Byte, salesmanUniformOrderDetail.UniformSize)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, salesmanUniformOrderDetail.Qty)
            DbCommandWrapper.AddInParameter("@IsValidate", DbType.Byte, salesmanUniformOrderDetail.IsValidate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUniformOrderDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanUniformOrderDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanUniformOrderHeaderID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderDetail.SalesmanUniformOrderHeader))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderDetail.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@SalesmanUniformID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderDetail.SalesmanUniform))

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

            Dim salesmanUniformOrderDetail As SalesmanUniformOrderDetail = CType(obj, SalesmanUniformOrderDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUniformOrderDetail.ID)
            DbCommandWrapper.AddInParameter("@UniformSize", DbType.Byte, salesmanUniformOrderDetail.UniformSize)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, salesmanUniformOrderDetail.Qty)
            DbCommandWrapper.AddInParameter("@IsValidate", DbType.Byte, salesmanUniformOrderDetail.IsValidate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUniformOrderDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanUniformOrderDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanUniformOrderHeaderID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderDetail.SalesmanUniformOrderHeader))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderDetail.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@SalesmanUniformID", DbType.Int32, Me.GetRefObject(salesmanUniformOrderDetail.SalesmanUniform))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanUniformOrderDetail

            Dim salesmanUniformOrderDetail As SalesmanUniformOrderDetail = New SalesmanUniformOrderDetail

            salesmanUniformOrderDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformSize")) Then salesmanUniformOrderDetail.UniformSize = CType(dr("UniformSize"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then salesmanUniformOrderDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsValidate")) Then salesmanUniformOrderDetail.IsValidate = CType(dr("IsValidate"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanUniformOrderDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanUniformOrderDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanUniformOrderDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanUniformOrderDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanUniformOrderDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanUniformOrderHeaderID")) Then
                salesmanUniformOrderDetail.SalesmanUniformOrderHeader = New SalesmanUniformOrderHeader(CType(dr("SalesmanUniformOrderHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanUniformOrderDetail.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanUniformID")) Then
                salesmanUniformOrderDetail.SalesmanUniform = New SalesmanUniform(CType(dr("SalesmanUniformID"), Integer))
            End If

            Return salesmanUniformOrderDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanUniformOrderDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanUniformOrderDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanUniformOrderDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

