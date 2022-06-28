
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MDPDealerDailyStock Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 27/11/2018 - 10:04:08
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

    Public Class MDPDealerDailyStockMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMDPDealerDailyStock"
        Private m_UpdateStatement As String = "up_UpdateMDPDealerDailyStock"
        Private m_RetrieveStatement As String = "up_RetrieveMDPDealerDailyStock"
        Private m_RetrieveListStatement As String = "up_RetrieveMDPDealerDailyStockList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMDPDealerDailyStock"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mDPDealerDailyStock As MDPDealerDailyStock = Nothing
            While dr.Read

                mDPDealerDailyStock = Me.CreateObject(dr)

            End While

            Return mDPDealerDailyStock

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mDPDealerDailyStockList As ArrayList = New ArrayList

            While dr.Read
                Dim mDPDealerDailyStock As MDPDealerDailyStock = Me.CreateObject(dr)
                mDPDealerDailyStockList.Add(mDPDealerDailyStock)
            End While

            Return mDPDealerDailyStockList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mDPDealerDailyStock As MDPDealerDailyStock = CType(obj, MDPDealerDailyStock)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mDPDealerDailyStock.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mDPDealerDailyStock As MDPDealerDailyStock = CType(obj, MDPDealerDailyStock)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, mDPDealerDailyStock.ProductionYear)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int16, mDPDealerDailyStock.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int16, mDPDealerDailyStock.PeriodYear)
            DbCommandWrapper.AddInParameter("@PeriodStartDate", DbType.DateTime, mDPDealerDailyStock.PeriodStartDate)
            DbCommandWrapper.AddInParameter("@PeriodEndDate", DbType.DateTime, mDPDealerDailyStock.PeriodEndDate)
            DbCommandWrapper.AddInParameter("@PeriodeDate", DbType.Int16, mDPDealerDailyStock.PeriodeDate)
            DbCommandWrapper.AddInParameter("@AllocationQuantity", DbType.Int32, mDPDealerDailyStock.AllocationQuantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mDPDealerDailyStock.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, mDPDealerDailyStock.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, mDPDealerDailyStock.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(mDPDealerDailyStock.Dealer))
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(mDPDealerDailyStock.VechileColor))

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

            Dim mDPDealerDailyStock As MDPDealerDailyStock = CType(obj, MDPDealerDailyStock)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mDPDealerDailyStock.ID)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, mDPDealerDailyStock.ProductionYear)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int16, mDPDealerDailyStock.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int16, mDPDealerDailyStock.PeriodYear)
            DbCommandWrapper.AddInParameter("@PeriodStartDate", DbType.DateTime, mDPDealerDailyStock.PeriodStartDate)
            DbCommandWrapper.AddInParameter("@PeriodEndDate", DbType.DateTime, mDPDealerDailyStock.PeriodEndDate)
            DbCommandWrapper.AddInParameter("@PeriodeDate", DbType.Int16, mDPDealerDailyStock.PeriodeDate)
            DbCommandWrapper.AddInParameter("@AllocationQuantity", DbType.Int32, mDPDealerDailyStock.AllocationQuantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mDPDealerDailyStock.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mDPDealerDailyStock.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, mDPDealerDailyStock.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(mDPDealerDailyStock.Dealer))
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(mDPDealerDailyStock.VechileColor))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MDPDealerDailyStock

            Dim mDPDealerDailyStock As MDPDealerDailyStock = New MDPDealerDailyStock

            mDPDealerDailyStock.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then mDPDealerDailyStock.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then mDPDealerDailyStock.PeriodMonth = CType(dr("PeriodMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then mDPDealerDailyStock.PeriodYear = CType(dr("PeriodYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStartDate")) Then mDPDealerDailyStock.PeriodStartDate = CType(dr("PeriodStartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEndDate")) Then mDPDealerDailyStock.PeriodEndDate = CType(dr("PeriodEndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeDate")) Then mDPDealerDailyStock.PeriodeDate = CType(dr("PeriodeDate"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationQuantity")) Then mDPDealerDailyStock.AllocationQuantity = CType(dr("AllocationQuantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mDPDealerDailyStock.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mDPDealerDailyStock.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mDPDealerDailyStock.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then mDPDealerDailyStock.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then mDPDealerDailyStock.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                mDPDealerDailyStock.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then
                mDPDealerDailyStock.VechileColor = New VechileColor(CType(dr("VehicleColorID"), Integer))
            End If

            Return mDPDealerDailyStock

        End Function

        Private Sub SetTableName()

            If Not (GetType(MDPDealerDailyStock) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MDPDealerDailyStock), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MDPDealerDailyStock).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

