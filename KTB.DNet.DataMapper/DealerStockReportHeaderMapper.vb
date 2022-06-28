#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerStockReportHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2007 - 01:15:44 PM
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

    Public Class DealerStockReportHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerStockReportHeader"
        Private m_UpdateStatement As String = "up_UpdateDealerStockReportHeader"
        Private m_RetrieveStatement As String = "up_RetrieveDealerStockReportHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerStockReportHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerStockReportHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerStockReportHeader As DealerStockReportHeader = Nothing
            While dr.Read

                dealerStockReportHeader = Me.CreateObject(dr)

            End While

            Return dealerStockReportHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerStockReportHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerStockReportHeader As DealerStockReportHeader = Me.CreateObject(dr)
                dealerStockReportHeaderList.Add(dealerStockReportHeader)
            End While

            Return dealerStockReportHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerStockReportHeader As DealerStockReportHeader = CType(obj, DealerStockReportHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerStockReportHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerStockReportHeader As DealerStockReportHeader = CType(obj, DealerStockReportHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@CaptureDate", DbType.DateTime, dealerStockReportHeader.CaptureDate)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int16, dealerStockReportHeader.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.AnsiStringFixedLength, dealerStockReportHeader.PeriodYear)
            DbCommandWrapper.AddInParameter("@CaptureBy", DbType.AnsiString, dealerStockReportHeader.CaptureBy)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, dealerStockReportHeader.Status)
            DbCommandWrapper.AddInParameter("@SalesVolume", DbType.Int32, dealerStockReportHeader.SalesVolume)
            DbCommandWrapper.AddInParameter("@CarryOver", DbType.Int32, dealerStockReportHeader.CarryOver)
            DbCommandWrapper.AddInParameter("@CarryOver_Min1", DbType.Int32, dealerStockReportHeader.CarryOver_Min1)
            DbCommandWrapper.AddInParameter("@NewOrder", DbType.Int32, dealerStockReportHeader.NewOrder)
            DbCommandWrapper.AddInParameter("@BeginingStock", DbType.Int32, dealerStockReportHeader.BeginingStock)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerStockReportHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerStockReportHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerStockReportHeader.Dealer))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(dealerStockReportHeader.VechileType))

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

            Dim dealerStockReportHeader As DealerStockReportHeader = CType(obj, DealerStockReportHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerStockReportHeader.ID)
            DbCommandWrapper.AddInParameter("@CaptureDate", DbType.DateTime, dealerStockReportHeader.CaptureDate)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int16, dealerStockReportHeader.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.AnsiStringFixedLength, dealerStockReportHeader.PeriodYear)
            DbCommandWrapper.AddInParameter("@CaptureBy", DbType.AnsiString, dealerStockReportHeader.CaptureBy)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, dealerStockReportHeader.Status)
            DbCommandWrapper.AddInParameter("@SalesVolume", DbType.Int32, dealerStockReportHeader.SalesVolume)
            DbCommandWrapper.AddInParameter("@CarryOver", DbType.Int32, dealerStockReportHeader.CarryOver)
            DbCommandWrapper.AddInParameter("@CarryOver_Min1", DbType.Int32, dealerStockReportHeader.CarryOver_Min1)
            DbCommandWrapper.AddInParameter("@NewOrder", DbType.Int32, dealerStockReportHeader.NewOrder)
            DbCommandWrapper.AddInParameter("@BeginingStock", DbType.Int32, dealerStockReportHeader.BeginingStock)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerStockReportHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerStockReportHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerStockReportHeader.Dealer))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(dealerStockReportHeader.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerStockReportHeader

            Dim dealerStockReportHeader As DealerStockReportHeader = New DealerStockReportHeader

            dealerStockReportHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CaptureDate")) Then dealerStockReportHeader.CaptureDate = CType(dr("CaptureDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then dealerStockReportHeader.PeriodMonth = CType(dr("PeriodMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then dealerStockReportHeader.PeriodYear = dr("PeriodYear").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CaptureBy")) Then dealerStockReportHeader.CaptureBy = dr("CaptureBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then dealerStockReportHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesVolume")) Then dealerStockReportHeader.SalesVolume = CType(dr("SalesVolume"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CarryOver")) Then dealerStockReportHeader.CarryOver = CType(dr("CarryOver"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CarryOver_Min1")) Then dealerStockReportHeader.CarryOver_Min1 = CType(dr("CarryOver_Min1"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NewOrder")) Then dealerStockReportHeader.NewOrder = CType(dr("NewOrder"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BeginingStock")) Then dealerStockReportHeader.BeginingStock = CType(dr("BeginingStock"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerStockReportHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerStockReportHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerStockReportHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerStockReportHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerStockReportHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerStockReportHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                dealerStockReportHeader.VechileType = New VechileType(CType(dr("VechileTypeID"), Integer))
            End If

            Return dealerStockReportHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerStockReportHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerStockReportHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerStockReportHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

