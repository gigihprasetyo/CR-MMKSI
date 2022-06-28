#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPLDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 3:27:15 PM
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

    Public Class SPLDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPLDetail"
        Private m_UpdateStatement As String = "up_UpdateSPLDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSPLDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSPLDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPLDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPLDetail As SPLDetail = Nothing
            While dr.Read

                sPLDetail = Me.CreateObject(dr)

            End While

            Return sPLDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPLDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sPLDetail As SPLDetail = Me.CreateObject(dr)
                sPLDetailList.Add(sPLDetail)
            End While

            Return sPLDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPLDetail As SPLDetail = CType(obj, SPLDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPLDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPLDetail As SPLDetail = CType(obj, SPLDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, sPLDetail.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, sPLDetail.PeriodYear)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sPLDetail.Quantity)
            DbCommandWrapper.AddInParameter("@PriceRefDate", DbType.DateTime, sPLDetail.PriceRefDate)
            DBCommandWrapper.AddInParameter("@Discount", DbType.Int32, sPLDetail.Discount)
            DBCommandWrapper.AddInParameter("@Surcharge", DbType.Int32, sPLDetail.Surcharge)
            DbCommandWrapper.AddInParameter("@MaxTopDate", DbType.DateTime, sPLDetail.MaxTopDate)
            DbCommandWrapper.AddInParameter("@MaxTopDay", DbType.Int32, sPLDetail.MaxTopDay)
            DbCommandWrapper.AddInParameter("@MaxTopIndicator", DbType.Int32, sPLDetail.MaxTopIndicator)
            DbCommandWrapper.AddInParameter("@FreeIntIndicator", DbType.Int32, sPLDetail.FreeIntIndicator)
            DbCommandWrapper.AddInParameter("@CreditCeiling", DbType.Int16, sPLDetail.CreditCeiling)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, sPLDetail.DeliveryDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPLDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sPLDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@SPLID", DbType.Int32, Me.GetRefObject(sPLDetail.SPL))
            'DBCommandWrapper.AddInParameter("@SPLID", DbType.Int32, sPLDetail.SPLID)
            DbCommandWrapper.AddInParameter("@VehicleTypeID", DbType.Int32, Me.GetRefObject(sPLDetail.VechileType))

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

            Dim sPLDetail As SPLDetail = CType(obj, SPLDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPLDetail.ID)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, sPLDetail.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, sPLDetail.PeriodYear)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sPLDetail.Quantity)
            DbCommandWrapper.AddInParameter("@PriceRefDate", DbType.DateTime, sPLDetail.PriceRefDate)
            DBCommandWrapper.AddInParameter("@Discount", DbType.Int32, sPLDetail.Discount)
            DBCommandWrapper.AddInParameter("@Surcharge", DbType.Int32, sPLDetail.Surcharge)
            DbCommandWrapper.AddInParameter("@MaxTopDate", DbType.DateTime, sPLDetail.MaxTopDate)
            DbCommandWrapper.AddInParameter("@MaxTopDay", DbType.Int32, sPLDetail.MaxTopDay)
            DbCommandWrapper.AddInParameter("@MaxTopIndicator", DbType.Int32, sPLDetail.MaxTopIndicator)
            DbCommandWrapper.AddInParameter("@FreeIntIndicator", DbType.Int32, sPLDetail.FreeIntIndicator)
            DbCommandWrapper.AddInParameter("@CreditCeiling", DbType.Int16, sPLDetail.CreditCeiling)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, sPLDetail.DeliveryDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPLDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPLDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@SPLID", DbType.Int32, Me.GetRefObject(sPLDetail.SPL))
            'DBCommandWrapper.AddInParameter("@SPLID", DbType.Int32, sPLDetail.SPLID)
            DbCommandWrapper.AddInParameter("@VehicleTypeID", DbType.Int32, Me.GetRefObject(sPLDetail.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPLDetail

            Dim sPLDetail As SPLDetail = New SPLDetail

            sPLDetail.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPLID")) Then sPLDetail.SPLID = CType(dr("SPLID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then sPLDetail.PeriodMonth = CType(dr("PeriodMonth"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then sPLDetail.PeriodYear = CType(dr("PeriodYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then sPLDetail.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PriceRefDate")) Then sPLDetail.PriceRefDate = CType(dr("PriceRefDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then sPLDetail.Discount = CType(dr("Discount"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Surcharge")) Then sPLDetail.Surcharge = CType(dr("Surcharge"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTopDate")) Then sPLDetail.MaxTopDate = CType(dr("MaxTopDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTopDay")) Then sPLDetail.MaxTopDay = CType(dr("MaxTopDay"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTopIndicator")) Then sPLDetail.MaxTopIndicator = CType(dr("MaxTopIndicator"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FreeIntIndicator")) Then sPLDetail.FreeIntIndicator = CType(dr("FreeIntIndicator"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditCeiling")) Then sPLDetail.CreditCeiling = CType(dr("CreditCeiling"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryDate")) Then sPLDetail.DeliveryDate = CType(dr("DeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPLDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPLDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPLDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPLDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPLDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SPLID")) Then
                sPLDetail.SPL = New SPL(CType(dr("SPLID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeID")) Then
                sPLDetail.VechileType = New VechileType(CType(dr("VehicleTypeID"), Integer))
            End If

            Return sPLDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPLDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPLDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPLDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

