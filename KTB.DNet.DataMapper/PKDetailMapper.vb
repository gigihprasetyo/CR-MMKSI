#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 8:48:18 AM
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

    Public Class PKDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPKDetail"
        Private m_UpdateStatement As String = "up_UpdatePKDetail"
        Private m_RetrieveStatement As String = "up_RetrievePKDetail"
        Private m_RetrieveListStatement As String = "up_RetrievePKDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePKDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pKDetail As PKDetail = Nothing
            While dr.Read

                pKDetail = Me.CreateObject(dr)

            End While

            Return pKDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pKDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim pKDetail As PKDetail = Me.CreateObject(dr)
                pKDetailList.Add(pKDetail)
            End While

            Return pKDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKDetail As PKDetail = CType(obj, PKDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pKDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKDetail As PKDetail = CType(obj, PKDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int32, pKDetail.LineItem)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, pKDetail.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, pKDetail.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleColorName", DbType.AnsiString, pKDetail.VehicleColorName)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, pKDetail.MaterialNumber)
            DbCommandWrapper.AddInParameter("@TargetQty", DbType.Int32, pKDetail.TargetQty)
            DbCommandWrapper.AddInParameter("@TargetAmount", DbType.Currency, pKDetail.TargetAmount)
            DbCommandWrapper.AddInParameter("@TargetPPh22", DbType.Currency, pKDetail.TargetPPh22)
            DbCommandWrapper.AddInParameter("@ResponseQty", DbType.Int32, pKDetail.ResponseQty)
            DbCommandWrapper.AddInParameter("@ResponseDiscount", DbType.Currency, pKDetail.ResponseDiscount)
            DbCommandWrapper.AddInParameter("@ResponseAmount", DbType.Currency, pKDetail.ResponseAmount)
            DbCommandWrapper.AddInParameter("@ResponsePPh22", DbType.Currency, pKDetail.ResponsePPh22)
            DbCommandWrapper.AddInParameter("@AgreeQty", DbType.Int32, pKDetail.AgreeQty)
            DbCommandWrapper.AddInParameter("@AgreeDiscount", DbType.Currency, pKDetail.AgreeDiscount)
            DbCommandWrapper.AddInParameter("@AgreeAmount", DbType.Currency, pKDetail.AgreeAmount)
            DbCommandWrapper.AddInParameter("@AgreePPh22", DbType.Currency, pKDetail.AgreePPh22)
            DbCommandWrapper.AddInParameter("@ResponseSalesSurcharge", DbType.Currency, pKDetail.ResponseSalesSurcharge)
            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, pKDetail.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, pKDetail.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, pKDetail.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pKDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(pKDetail.VechileColor))
            DbCommandWrapper.AddInParameter("@PKHeaderID", DbType.Int32, Me.GetRefObject(pKDetail.PKHeader))

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

            Dim pKDetail As PKDetail = CType(obj, PKDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pKDetail.ID)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int32, pKDetail.LineItem)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, pKDetail.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, pKDetail.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleColorName", DbType.AnsiString, pKDetail.VehicleColorName)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, pKDetail.MaterialNumber)
            DbCommandWrapper.AddInParameter("@TargetQty", DbType.Int32, pKDetail.TargetQty)
            DbCommandWrapper.AddInParameter("@TargetAmount", DbType.Currency, pKDetail.TargetAmount)
            DbCommandWrapper.AddInParameter("@TargetPPh22", DbType.Currency, pKDetail.TargetPPh22)
            DbCommandWrapper.AddInParameter("@ResponseQty", DbType.Int32, pKDetail.ResponseQty)
            DbCommandWrapper.AddInParameter("@ResponseDiscount", DbType.Currency, pKDetail.ResponseDiscount)
            DbCommandWrapper.AddInParameter("@ResponseAmount", DbType.Currency, pKDetail.ResponseAmount)
            DbCommandWrapper.AddInParameter("@ResponsePPh22", DbType.Currency, pKDetail.ResponsePPh22)
            DbCommandWrapper.AddInParameter("@AgreeQty", DbType.Int32, pKDetail.AgreeQty)
            DbCommandWrapper.AddInParameter("@AgreeDiscount", DbType.Currency, pKDetail.AgreeDiscount)
            DbCommandWrapper.AddInParameter("@AgreeAmount", DbType.Currency, pKDetail.AgreeAmount)
            DbCommandWrapper.AddInParameter("@AgreePPh22", DbType.Currency, pKDetail.AgreePPh22)
            DbCommandWrapper.AddInParameter("@ResponseSalesSurcharge", DbType.Currency, pKDetail.ResponseSalesSurcharge)
            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, pKDetail.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, pKDetail.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, pKDetail.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pKDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(pKDetail.VechileColor))
            DbCommandWrapper.AddInParameter("@PKHeaderID", DbType.Int32, Me.GetRefObject(pKDetail.PKHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PKDetail

            Dim pKDetail As PKDetail = New PKDetail

            pKDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LineItem")) Then pKDetail.LineItem = CType(dr("LineItem"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FreeDays")) Then pKDetail.FreeDays = CType(dr("FreeDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then pKDetail.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorCode")) Then pKDetail.VehicleColorCode = dr("VehicleColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorName")) Then pKDetail.VehicleColorName = dr("VehicleColorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then pKDetail.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TargetQty")) Then pKDetail.TargetQty = CType(dr("TargetQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TargetAmount")) Then pKDetail.TargetAmount = CType(dr("TargetAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TargetPPh22")) Then pKDetail.TargetPPh22 = CType(dr("TargetPPh22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseQty")) Then pKDetail.ResponseQty = CType(dr("ResponseQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseDiscount")) Then pKDetail.ResponseDiscount = CType(dr("ResponseDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseAmount")) Then pKDetail.ResponseAmount = CType(dr("ResponseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponsePPh22")) Then pKDetail.ResponsePPh22 = CType(dr("ResponsePPh22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AgreeQty")) Then pKDetail.AgreeQty = CType(dr("AgreeQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AgreeDiscount")) Then pKDetail.AgreeDiscount = CType(dr("AgreeDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AgreeAmount")) Then pKDetail.AgreeAmount = CType(dr("AgreeAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AgreePPh22")) Then pKDetail.AgreePPh22 = CType(dr("AgreePPh22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseSalesSurcharge")) Then pKDetail.ResponseSalesSurcharge = CType(dr("ResponseSalesSurcharge"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDay")) Then pKDetail.MaxTOPDay = CType(dr("MaxTOPDay"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then pKDetail.Sequence = CType(dr("Sequence"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pKDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pKDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pKDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pKDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pKDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then
                pKDetail.VechileColor = New VechileColor(CType(dr("VehicleColorID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PKHeaderID")) Then
                pKDetail.PKHeader = New PKHeader(CType(dr("PKHeaderID"), Integer))
            End If

            Return pKDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(PKDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PKDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PKDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

