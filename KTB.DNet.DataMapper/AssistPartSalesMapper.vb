
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistPartSales Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 10:36:42 AM
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

    Public Class AssistPartSalesMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistPartSales"
        Private m_UpdateStatement As String = "up_UpdateAssistPartSales"
        Private m_RetrieveStatement As String = "up_RetrieveAssistPartSales"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistPartSalesList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistPartSales"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistPartSales As AssistPartSales = Nothing
            While dr.Read

                assistPartSales = Me.CreateObject(dr)

            End While

            Return assistPartSales

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistPartSalesList As ArrayList = New ArrayList

            While dr.Read
                Dim assistPartSales As AssistPartSales = Me.CreateObject(dr)
                assistPartSalesList.Add(assistPartSales)
            End While

            Return assistPartSalesList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistPartSales As AssistPartSales = CType(obj, AssistPartSales)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistPartSales.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistPartSales As AssistPartSales = CType(obj, AssistPartSales)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistPartSales.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@TglTransaksi", DbType.DateTime, assistPartSales.TglTransaksi)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistPartSales.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, assistPartSales.DealerCode)
            DbCommandWrapper.AddInParameter("@KodeCustomer", DbType.AnsiString, assistPartSales.KodeCustomer)
            DbCommandWrapper.AddInParameter("@SalesChannelID", DbType.Int32, Me.GetRefObject(assistPartSales.AssistSalesChannel))
            DbCommandWrapper.AddInParameter("@SalesChannelCode", DbType.AnsiString, assistPartSales.SalesChannelCode)
            DbCommandWrapper.AddInParameter("@TrTraineeSalesSparepartID", DbType.Int32, assistPartSales.TrTraineeSalesSparepartID)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, assistPartSales.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@KodeSalesman", DbType.AnsiString, assistPartSales.KodeSalesman)
            DbCommandWrapper.AddInParameter("@NoWorkOrder", DbType.AnsiString, assistPartSales.NoWorkOrder)
            DbCommandWrapper.AddInParameter("@SparepartMasterID", DbType.Int32, Me.GetRefObject(assistPartSales.SparePartMaster))
            DbCommandWrapper.AddInParameter("@NoParts", DbType.AnsiString, assistPartSales.NoParts)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Double, assistPartSales.Qty)
            DbCommandWrapper.AddInParameter("@HargaBeli", DbType.Currency, assistPartSales.HargaBeli)
            DbCommandWrapper.AddInParameter("@HargaJual", DbType.Currency, assistPartSales.HargaJual)
            DbCommandWrapper.AddInParameter("@IsCampaign", DbType.Boolean, assistPartSales.IsCampaign)
            DbCommandWrapper.AddInParameter("@CampaignNo", DbType.AnsiString, assistPartSales.CampaignNo)
            DbCommandWrapper.AddInParameter("@CampaignDescription", DbType.AnsiString, assistPartSales.CampaignDescription)
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(assistPartSales.DealerBranch))
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, assistPartSales.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistPartSales.RemarksSystem)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistPartSales.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistPartSales.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistPartSales.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistPartSales.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim assistPartSales As AssistPartSales = CType(obj, AssistPartSales)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistPartSales.ID)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistPartSales.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@TglTransaksi", DbType.DateTime, assistPartSales.TglTransaksi)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistPartSales.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, assistPartSales.DealerCode)
            DbCommandWrapper.AddInParameter("@KodeCustomer", DbType.AnsiString, assistPartSales.KodeCustomer)
            DbCommandWrapper.AddInParameter("@SalesChannelID", DbType.Int32, Me.GetRefObject(assistPartSales.AssistSalesChannel))
            DbCommandWrapper.AddInParameter("@SalesChannelCode", DbType.AnsiString, assistPartSales.SalesChannelCode)
            DbCommandWrapper.AddInParameter("@TrTraineeSalesSparepartID", DbType.Int32, assistPartSales.TrTraineeSalesSparepartID)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, assistPartSales.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@KodeSalesman", DbType.AnsiString, assistPartSales.KodeSalesman)
            DbCommandWrapper.AddInParameter("@NoWorkOrder", DbType.AnsiString, assistPartSales.NoWorkOrder)
            DbCommandWrapper.AddInParameter("@SparepartMasterID", DbType.Int32, Me.GetRefObject(assistPartSales.SparePartMaster))
            DbCommandWrapper.AddInParameter("@NoParts", DbType.AnsiString, assistPartSales.NoParts)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Double, assistPartSales.Qty)
            DbCommandWrapper.AddInParameter("@HargaBeli", DbType.Currency, assistPartSales.HargaBeli)
            DbCommandWrapper.AddInParameter("@HargaJual", DbType.Currency, assistPartSales.HargaJual)
            DbCommandWrapper.AddInParameter("@IsCampaign", DbType.Boolean, assistPartSales.IsCampaign)
            DbCommandWrapper.AddInParameter("@CampaignNo", DbType.AnsiString, assistPartSales.CampaignNo)
            DbCommandWrapper.AddInParameter("@CampaignDescription", DbType.AnsiString, assistPartSales.CampaignDescription)
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(assistPartSales.DealerBranch))
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, assistPartSales.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistPartSales.RemarksSystem)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistPartSales.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistPartSales.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistPartSales.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistPartSales.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistPartSales

            Dim assistPartSales As AssistPartSales = New AssistPartSales

            assistPartSales.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("TglTransaksi")) Then assistPartSales.TglTransaksi = CType(dr("TglTransaksi"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then assistPartSales.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KodeCustomer")) Then assistPartSales.KodeCustomer = dr("KodeCustomer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesChannelCode")) Then assistPartSales.SalesChannelCode = dr("SalesChannelCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineeSalesSparepartID")) Then assistPartSales.TrTraineeSalesSparepartID = CType(dr("TrTraineeSalesSparepartID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then assistPartSales.SalesmanHeaderID = CType(dr("SalesmanHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KodeSalesman")) Then assistPartSales.KodeSalesman = dr("KodeSalesman").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoWorkOrder")) Then assistPartSales.NoWorkOrder = dr("NoWorkOrder").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoParts")) Then assistPartSales.NoParts = dr("NoParts").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then assistPartSales.Qty = CType(dr("Qty"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaBeli")) Then assistPartSales.HargaBeli = CType(dr("HargaBeli"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaJual")) Then assistPartSales.HargaJual = CType(dr("HargaJual"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCampaign")) Then assistPartSales.IsCampaign = CType(dr("IsCampaign"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignNo")) Then assistPartSales.CampaignNo = dr("CampaignNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignDescription")) Then assistPartSales.CampaignDescription = dr("CampaignDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesChannelCode")) Then assistPartSales.SalesChannelCode = dr("SalesChannelCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSystem")) Then assistPartSales.RemarksSystem = dr("RemarksSystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusAktif")) Then assistPartSales.StatusAktif = CType(dr("StatusAktif"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateSystemStatus")) Then assistPartSales.ValidateSystemStatus = CType(dr("ValidateSystemStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistPartSales.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistPartSales.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistPartSales.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistPartSales.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistPartSales.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistUploadLogID")) Then
                assistPartSales.AssistUploadLog = New AssistUploadLog(CType(dr("AssistUploadLogID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                assistPartSales.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesChannelID")) Then
                assistPartSales.AssistSalesChannel = New AssistSalesChannel(CType(dr("SalesChannelID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartMasterID")) Then
                assistPartSales.SparePartMaster = New SparePartMaster(CType(dr("SparepartMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                assistPartSales.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            Return assistPartSales

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistPartSales) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistPartSales), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistPartSales).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

