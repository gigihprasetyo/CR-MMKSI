#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOBillingRecap Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2005 - 9:54:08 AM
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

    Public Class SparePartPOBillingRecapMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPOBillingRecap"
        Private m_UpdateStatement As String = "up_UpdateSparePartPOBillingRecap"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPOBillingRecap"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPOBillingRecapList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPOBillingRecap"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPOBillingRecap As SparePartPOBillingRecap = Nothing
            While dr.Read

                sparePartPOBillingRecap = Me.CreateObject(dr)

            End While

            Return sparePartPOBillingRecap

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPOBillingRecapList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPOBillingRecap As SparePartPOBillingRecap = Me.CreateObject(dr)
                sparePartPOBillingRecapList.Add(sparePartPOBillingRecap)
            End While

            Return sparePartPOBillingRecapList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOBillingRecap As SparePartPOBillingRecap = CType(obj, SparePartPOBillingRecap)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOBillingRecap.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOBillingRecap As SparePartPOBillingRecap = CType(obj, SparePartPOBillingRecap)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, sparePartPOBillingRecap.BillingNumber)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, sparePartPOBillingRecap.BillingDate)
            DbCommandWrapper.AddInParameter("@BillingAmount", DbType.Currency, sparePartPOBillingRecap.BillingAmount)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, sparePartPOBillingRecap.PPN)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiStringFixedLength, sparePartPOBillingRecap.OrderType)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, sparePartPOBillingRecap.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, sparePartPOBillingRecap.PeriodYear)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOBillingRecap.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiStringFixedLength, sparePartPOBillingRecap.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.AnsiString,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(sparePartPOBillingRecap.Dealer))

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

            Dim sparePartPOBillingRecap As SparePartPOBillingRecap = CType(obj, SparePartPOBillingRecap)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOBillingRecap.ID)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, sparePartPOBillingRecap.BillingNumber)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, sparePartPOBillingRecap.BillingDate)
            DbCommandWrapper.AddInParameter("@BillingAmount", DbType.Currency, sparePartPOBillingRecap.BillingAmount)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, sparePartPOBillingRecap.PPN)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiStringFixedLength, sparePartPOBillingRecap.OrderType)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, sparePartPOBillingRecap.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, sparePartPOBillingRecap.PeriodYear)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOBillingRecap.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPOBillingRecap.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiStringFixedLength, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.AnsiString,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(sparePartPOBillingRecap.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPOBillingRecap

            Dim sparePartPOBillingRecap As SparePartPOBillingRecap = New SparePartPOBillingRecap

            sparePartPOBillingRecap.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then sparePartPOBillingRecap.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then sparePartPOBillingRecap.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingAmount")) Then sparePartPOBillingRecap.BillingAmount = CType(dr("BillingAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN")) Then sparePartPOBillingRecap.PPN = CType(dr("PPN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then sparePartPOBillingRecap.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then sparePartPOBillingRecap.PeriodMonth = CType(dr("PeriodMonth"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then sparePartPOBillingRecap.PeriodYear = CType(dr("PeriodYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPOBillingRecap.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPOBillingRecap.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPOBillingRecap.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPOBillingRecap.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPOBillingRecap.LastUpdateTime = dr("LastUpdateTime").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sparePartPOBillingRecap.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return sparePartPOBillingRecap

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPOBillingRecap) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPOBillingRecap), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPOBillingRecap).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

