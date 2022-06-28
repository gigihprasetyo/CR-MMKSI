
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPendingOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/22/2015 - 7:38:09 AM
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

    Public Class SparePartPendingOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPendingOrder"
        Private m_UpdateStatement As String = "up_UpdateSparePartPendingOrder"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPendingOrder"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPendingOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPendingOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPendingOrder As SparePartPendingOrder = Nothing
            While dr.Read

                sparePartPendingOrder = Me.CreateObject(dr)

            End While

            Return sparePartPendingOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPendingOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPendingOrder As SparePartPendingOrder = Me.CreateObject(dr)
                sparePartPendingOrderList.Add(sparePartPendingOrder)
            End While

            Return sparePartPendingOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPendingOrder As SparePartPendingOrder = CType(obj, SparePartPendingOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPendingOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPendingOrder As SparePartPendingOrder = CType(obj, SparePartPendingOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sparePartPendingOrder.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, sparePartPendingOrder.SODate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, sparePartPendingOrder.Amount)
            DbCommandWrapper.AddInParameter("@Tax", DbType.Currency, sparePartPendingOrder.Tax)
            DbCommandWrapper.AddInParameter("@Total", DbType.Currency, sparePartPendingOrder.Total)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.StringFixedLength, sparePartPendingOrder.DocumentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPendingOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPendingOrder.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(sparePartPendingOrder.SparePartPO))

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

            Dim sparePartPendingOrder As SparePartPendingOrder = CType(obj, SparePartPendingOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPendingOrder.ID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sparePartPendingOrder.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, sparePartPendingOrder.SODate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, sparePartPendingOrder.Amount)
            DbCommandWrapper.AddInParameter("@Tax", DbType.Currency, sparePartPendingOrder.Tax)
            DbCommandWrapper.AddInParameter("@Total", DbType.Currency, sparePartPendingOrder.Total)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.StringFixedLength, sparePartPendingOrder.DocumentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPendingOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPendingOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(sparePartPendingOrder.SparePartPO))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPendingOrder

            Dim sparePartPendingOrder As SparePartPendingOrder = New SparePartPendingOrder

            sparePartPendingOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then sparePartPendingOrder.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SODate")) Then sparePartPendingOrder.SODate = CType(dr("SODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then sparePartPendingOrder.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Tax")) Then sparePartPendingOrder.Tax = CType(dr("Tax"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Total")) Then sparePartPendingOrder.Total = CType(dr("Total"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then sparePartPendingOrder.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPendingOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPendingOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPendingOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPendingOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPendingOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOID")) Then
                sparePartPendingOrder.SparePartPO = New SparePartPO(CType(dr("SparePartPOID"), Integer))
            End If

            Return sparePartPendingOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPendingOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPendingOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPendingOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

