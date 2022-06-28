
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MDPDealerMonthlyStock Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/11/2018 - 10:47:54
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

    Public Class MDPDealerMonthlyStockMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMDPDealerMonthlyStock"
        Private m_UpdateStatement As String = "up_UpdateMDPDealerMonthlyStock"
        Private m_RetrieveStatement As String = "up_RetrieveMDPDealerMonthlyStock"
        Private m_RetrieveListStatement As String = "up_RetrieveMDPDealerMonthlyStockList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMDPDealerMonthlyStock"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mDPDealerMonthlyStock As MDPDealerMonthlyStock = Nothing
            While dr.Read

                mDPDealerMonthlyStock = Me.CreateObject(dr)

            End While

            Return mDPDealerMonthlyStock

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mDPDealerMonthlyStockList As ArrayList = New ArrayList

            While dr.Read
                Dim mDPDealerMonthlyStock As MDPDealerMonthlyStock = Me.CreateObject(dr)
                mDPDealerMonthlyStockList.Add(mDPDealerMonthlyStock)
            End While

            Return mDPDealerMonthlyStockList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mDPDealerMonthlyStock As MDPDealerMonthlyStock = CType(obj, MDPDealerMonthlyStock)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mDPDealerMonthlyStock.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mDPDealerMonthlyStock As MDPDealerMonthlyStock = CType(obj, MDPDealerMonthlyStock)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, mDPDealerMonthlyStock.DealerID)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int16, mDPDealerMonthlyStock.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int16, mDPDealerMonthlyStock.PeriodYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, mDPDealerMonthlyStock.ProductionYear)
            DbCommandWrapper.AddInParameter("@PeriodStartDate", DbType.DateTime, mDPDealerMonthlyStock.PeriodStartDate)
            DbCommandWrapper.AddInParameter("@PeriodEndDate", DbType.DateTime, mDPDealerMonthlyStock.PeriodEndDate)
            'DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, mDPDealerMonthlyStock.VehicleColorID)
            DbCommandWrapper.AddInParameter("@CarryOverStock", DbType.Int32, mDPDealerMonthlyStock.CarryOverStock)
            DbCommandWrapper.AddInParameter("@PlanStock", DbType.Int32, mDPDealerMonthlyStock.PlanStock)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mDPDealerMonthlyStock.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, mDPDealerMonthlyStock.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, mDPDealerMonthlyStock.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(mDPDealerMonthlyStock.Dealer))
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(mDPDealerMonthlyStock.VechileColor))

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

            Dim mDPDealerMonthlyStock As MDPDealerMonthlyStock = CType(obj, MDPDealerMonthlyStock)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mDPDealerMonthlyStock.ID)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, mDPDealerMonthlyStock.DealerID)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int16, mDPDealerMonthlyStock.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int16, mDPDealerMonthlyStock.PeriodYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, mDPDealerMonthlyStock.ProductionYear)
            DbCommandWrapper.AddInParameter("@PeriodStartDate", DbType.DateTime, mDPDealerMonthlyStock.PeriodStartDate)
            DbCommandWrapper.AddInParameter("@PeriodEndDate", DbType.DateTime, mDPDealerMonthlyStock.PeriodEndDate)
            'DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, mDPDealerMonthlyStock.VehicleColorID)
            DbCommandWrapper.AddInParameter("@CarryOverStock", DbType.Int32, mDPDealerMonthlyStock.CarryOverStock)
            DbCommandWrapper.AddInParameter("@PlanStock", DbType.Int32, mDPDealerMonthlyStock.PlanStock)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mDPDealerMonthlyStock.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mDPDealerMonthlyStock.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, mDPDealerMonthlyStock.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(mDPDealerMonthlyStock.Dealer))
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(mDPDealerMonthlyStock.VechileColor))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MDPDealerMonthlyStock

            Dim mDPDealerMonthlyStock As MDPDealerMonthlyStock = New MDPDealerMonthlyStock

            mDPDealerMonthlyStock.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then mDPDealerMonthlyStock.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then mDPDealerMonthlyStock.PeriodMonth = CType(dr("PeriodMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then mDPDealerMonthlyStock.PeriodYear = CType(dr("PeriodYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then mDPDealerMonthlyStock.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStartDate")) Then mDPDealerMonthlyStock.PeriodStartDate = CType(dr("PeriodStartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEndDate")) Then mDPDealerMonthlyStock.PeriodEndDate = CType(dr("PeriodEndDate"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then mDPDealerMonthlyStock.VehicleColorID = CType(dr("VehicleColorID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CarryOverStock")) Then mDPDealerMonthlyStock.CarryOverStock = CType(dr("CarryOverStock"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanStock")) Then mDPDealerMonthlyStock.PlanStock = CType(dr("PlanStock"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mDPDealerMonthlyStock.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mDPDealerMonthlyStock.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mDPDealerMonthlyStock.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then mDPDealerMonthlyStock.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then mDPDealerMonthlyStock.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                mDPDealerMonthlyStock.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then
                mDPDealerMonthlyStock.VechileColor = New VechileColor(CType(dr("VehicleColorID"), Integer))
            End If

            Return mDPDealerMonthlyStock

        End Function

        Private Sub SetTableName()

            If Not (GetType(MDPDealerMonthlyStock) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MDPDealerMonthlyStock), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MDPDealerMonthlyStock).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

