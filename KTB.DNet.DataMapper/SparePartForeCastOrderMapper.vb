#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartForeCastOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/18/2021 - 3:12:27 PM
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

    Public Class SparePartForeCastOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartForeCastOrder"
        Private m_UpdateStatement As String = "up_UpdateSparePartForeCastOrder"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartForeCastOrder"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartForeCastOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartForeCastOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartForeCastOrder As SparePartForeCastOrder = Nothing
            While dr.Read

                sparePartForeCastOrder = Me.CreateObject(dr)

            End While

            Return sparePartForeCastOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartForeCastOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartForeCastOrder As SparePartForeCastOrder = Me.CreateObject(dr)
                sparePartForeCastOrderList.Add(sparePartForeCastOrder)
            End While

            Return sparePartForeCastOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForeCastOrder As SparePartForeCastOrder = CType(obj, SparePartForeCastOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartForeCastOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForeCastOrder As SparePartForeCastOrder = CType(obj, SparePartForeCastOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PoNumber", DbType.AnsiString, sparePartForeCastOrder.PoNumber)
            DbCommandWrapper.AddInParameter("@PoDate", DbType.DateTime, sparePartForeCastOrder.PoDate)
            DbCommandWrapper.AddInParameter("@RequestQty", DbType.Int32, sparePartForeCastOrder.RequestQty)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.Date, sparePartForeCastOrder.RequestDate)
            DbCommandWrapper.AddInParameter("@ApprovedQty", DbType.Int32, sparePartForeCastOrder.ApprovedQty)
            DbCommandWrapper.AddInParameter("@SendDate", DbType.Date, sparePartForeCastOrder.SendDate)
            DbCommandWrapper.AddInParameter("@NoAWB", DbType.AnsiString, sparePartForeCastOrder.NoAWB)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sparePartForeCastOrder.SONumber)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, sparePartForeCastOrder.DONumber)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, sparePartForeCastOrder.BillingNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartForeCastOrder.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartForeCastOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, sparePartForeCastOrder.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, sparePartForeCastOrder.LastUpdatedTime)


            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, sparePartForeCastOrder.DealerID)
            'DbCommandWrapper.AddInParameter("@SparePartForecastMasterID", DbType.Int32, sparePartForeCastOrder.SparePartForecastMasterID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(sparePartForeCastOrder.Dealer))
            DbCommandWrapper.AddInParameter("@SparePartForecastMasterID", DbType.Int32, Me.GetRefObject(sparePartForeCastOrder.SparePartForecastMaster))

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

            Dim sparePartForeCastOrder As SparePartForeCastOrder = CType(obj, SparePartForeCastOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartForeCastOrder.ID)
            DbCommandWrapper.AddInParameter("@PoNumber", DbType.AnsiString, sparePartForeCastOrder.PoNumber)
            DbCommandWrapper.AddInParameter("@PoDate", DbType.DateTime, sparePartForeCastOrder.PoDate)
            DbCommandWrapper.AddInParameter("@RequestQty", DbType.Int32, sparePartForeCastOrder.RequestQty)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.Date, sparePartForeCastOrder.RequestDate)
            DbCommandWrapper.AddInParameter("@ApprovedQty", DbType.Int32, sparePartForeCastOrder.ApprovedQty)
            DbCommandWrapper.AddInParameter("@SendDate", DbType.Date, sparePartForeCastOrder.SendDate)
            DbCommandWrapper.AddInParameter("@NoAWB", DbType.AnsiString, sparePartForeCastOrder.NoAWB)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sparePartForeCastOrder.SONumber)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, sparePartForeCastOrder.DONumber)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, sparePartForeCastOrder.BillingNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartForeCastOrder.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartForeCastOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartForeCastOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, sparePartForeCastOrder.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, sparePartForeCastOrder.DealerID)
            'DbCommandWrapper.AddInParameter("@SparePartForecastMasterID", DbType.Int32, sparePartForeCastOrder.SparePartForecastMasterID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(sparePartForeCastOrder.Dealer))
            DbCommandWrapper.AddInParameter("@SparePartForecastMasterID", DbType.Int32, Me.GetRefObject(sparePartForeCastOrder.SparePartForecastMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartForeCastOrder

            Dim sparePartForeCastOrder As SparePartForeCastOrder = New SparePartForeCastOrder

            sparePartForeCastOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PoNumber")) Then sparePartForeCastOrder.PoNumber = dr("PoNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PoDate")) Then sparePartForeCastOrder.PoDate = CType(dr("PoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestQty")) Then sparePartForeCastOrder.RequestQty = CType(dr("RequestQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then sparePartForeCastOrder.RequestDate = CType(dr("RequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedQty")) Then sparePartForeCastOrder.ApprovedQty = CType(dr("ApprovedQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SendDate")) Then sparePartForeCastOrder.SendDate = CType(dr("SendDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NoAWB")) Then sparePartForeCastOrder.NoAWB = dr("NoAWB").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then sparePartForeCastOrder.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then sparePartForeCastOrder.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then sparePartForeCastOrder.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartForeCastOrder.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartForeCastOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartForeCastOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartForeCastOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then sparePartForeCastOrder.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then sparePartForeCastOrder.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then sparePartForeCastOrder.DealerID = CType(dr("DealerID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SparePartForecastMasterID")) Then sparePartForeCastOrder.SparePartForecastMasterID = CType(dr("SparePartForecastMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sparePartForeCastOrder.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartForecastMasterID")) Then
                sparePartForeCastOrder.SparePartForecastMaster = New SparePartForecastMaster(CType(dr("SparePartForecastMasterID"), Integer))
            End If
            Return sparePartForeCastOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartForeCastOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartForeCastOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartForeCastOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
