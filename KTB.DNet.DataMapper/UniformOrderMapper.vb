#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:21:00 AM
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

    Public Class UniformOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertUniformOrder"
        Private m_UpdateStatement As String = "up_UpdateUniformOrder"
        Private m_RetrieveStatement As String = "up_RetrieveUniformOrder"
        Private m_RetrieveListStatement As String = "up_RetrieveUniformOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteUniformOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim uniformOrder As UniformOrder = Nothing
            While dr.Read

                uniformOrder = Me.CreateObject(dr)

            End While

            Return uniformOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim uniformOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim uniformOrder As UniformOrder = Me.CreateObject(dr)
                uniformOrderList.Add(uniformOrder)
            End While

            Return uniformOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformOrder As UniformOrder = CType(obj, UniformOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformOrder As UniformOrder = CType(obj, UniformOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@OrderNum", DbType.AnsiString, uniformOrder.OrderNum)
            DbCommandWrapper.AddInParameter("@OrderDate", DbType.DateTime, uniformOrder.OrderDate)
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, uniformOrder.SalesmanId)
            DbCommandWrapper.AddInParameter("@JobPositionId", DbType.Int32, uniformOrder.JobPositionId)
            DbCommandWrapper.AddInParameter("@UniformCode", DbType.AnsiString, uniformOrder.UniformCode)
            DbCommandWrapper.AddInParameter("@UniformSize", DbType.AnsiStringFixedLength, uniformOrder.UniformSize)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, uniformOrder.Qty)
            DbCommandWrapper.AddInParameter("@DealerPrice", DbType.Decimal, uniformOrder.DealerPrice)
            DbCommandWrapper.AddInParameter("@AmountPrice", DbType.Decimal, uniformOrder.AmountPrice)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, uniformOrder.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@UniformDistributionId", DbType.Int32, Me.GetRefObject(uniformOrder.UniformDistribution))

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

            Dim uniformOrder As UniformOrder = CType(obj, UniformOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformOrder.ID)
            DbCommandWrapper.AddInParameter("@OrderNum", DbType.AnsiString, uniformOrder.OrderNum)
            DbCommandWrapper.AddInParameter("@OrderDate", DbType.DateTime, uniformOrder.OrderDate)
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, uniformOrder.SalesmanId)
            DbCommandWrapper.AddInParameter("@JobPositionId", DbType.Int32, uniformOrder.JobPositionId)
            DbCommandWrapper.AddInParameter("@UniformCode", DbType.AnsiString, uniformOrder.UniformCode)
            DbCommandWrapper.AddInParameter("@UniformSize", DbType.AnsiStringFixedLength, uniformOrder.UniformSize)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, uniformOrder.Qty)
            DbCommandWrapper.AddInParameter("@DealerPrice", DbType.Decimal, uniformOrder.DealerPrice)
            DbCommandWrapper.AddInParameter("@AmountPrice", DbType.Decimal, uniformOrder.AmountPrice)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, uniformOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@UniformDistributionId", DbType.Int32, Me.GetRefObject(uniformOrder.UniformDistribution))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As UniformOrder

            Dim uniformOrder As UniformOrder = New UniformOrder

            uniformOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNum")) Then uniformOrder.OrderNum = dr("OrderNum").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderDate")) Then uniformOrder.OrderDate = CType(dr("OrderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanId")) Then uniformOrder.SalesmanId = CType(dr("SalesmanId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId")) Then uniformOrder.JobPositionId = CType(dr("JobPositionId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformCode")) Then uniformOrder.UniformCode = dr("UniformCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UniformSize")) Then uniformOrder.UniformSize = dr("UniformSize").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then uniformOrder.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPrice")) Then uniformOrder.DealerPrice = CType(dr("DealerPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AmountPrice")) Then uniformOrder.AmountPrice = CType(dr("AmountPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then uniformOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then uniformOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then uniformOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then uniformOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then uniformOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformDistributionId")) Then
                uniformOrder.UniformDistribution = New UniformDistribution(CType(dr("UniformDistributionId"), Integer))
            End If

            Return uniformOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(UniformOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(UniformOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(UniformOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

