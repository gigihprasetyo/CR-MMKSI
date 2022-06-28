
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistPartSales Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 10:27:39 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("AssistPartSales")> _
    Public Class AssistPartSales
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _tglTransaksi As Object
        Private _dealerCode As String = String.Empty
        Private _kodeCustomer As String = String.Empty
        Private _salesChannelCode As String = String.Empty
        Private _trTraineeSalesSparepartID As Integer
        Private _salesmanHeaderID As Integer
        Private _kodeSalesman As String = String.Empty
        Private _noWorkOrder As String = String.Empty
        Private _noParts As String = String.Empty
        Private _qty As Double
        Private _hargaBeli As Decimal
        Private _hargaJual As Decimal
        Private _isCampaign As Boolean
        Private _campaignNo As String = String.Empty
        Private _campaignDescription As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
        Private _remarksSystem As String = String.Empty
        Private _statusAktif As Short
        Private _validateSystemStatus As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _assistuploadLogID As Integer
        Private _dealerID As Integer
        Private _dealerBranchID As Integer
        Private _salesChannelID As Integer
        Private _sparepartMasterID As Integer

        Private _assistuploadLog As AssistUploadLog
        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _salesChannel As AssistSalesChannel
        Private _sparePartsMaster As SparePartMaster
#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("AssistUploadLogID", "{0}")> _
        Public Property AssistUploadLogID As Integer
            Get
                Return _assistuploadLogID
            End Get
            Set(ByVal value As Integer)
                _assistuploadLogID = value
            End Set
        End Property

        <ColumnInfo("TglTransaksi", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TglTransaksi As Object
            Get
                Return _tglTransaksi
            End Get
            Set(ByVal value As Object)
                _tglTransaksi = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("DealerBranchID", "{0}")> _
        Public Property DealerBranchID As Integer
            Get
                Return _dealerBranchID
            End Get
            Set(ByVal value As Integer)
                _dealerBranchID = value
            End Set
        End Property

        <ColumnInfo("DealerBranchCode", "{0}")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property

        <ColumnInfo("KodeCustomer", "'{0}'")> _
        Public Property KodeCustomer As String
            Get
                Return _kodeCustomer
            End Get
            Set(ByVal value As String)
                _kodeCustomer = value
            End Set
        End Property

        <ColumnInfo("SalesChannelID", "{0}")> _
        Public Property SalesChannelID As Integer
            Get
                Return _salesChannelID
            End Get
            Set(ByVal value As Integer)
                _salesChannelID = value
            End Set
        End Property

        <ColumnInfo("SalesChannelCode", "'{0}'")> _
        Public Property SalesChannelCode As String
            Get
                Return _salesChannelCode
            End Get
            Set(ByVal value As String)
                _salesChannelCode = value
            End Set
        End Property

        <ColumnInfo("TrTraineeSalesSparepartID", "{0}")> _
        Public Property TrTraineeSalesSparepartID As Integer
            Get
                Return _trTraineeSalesSparepartID
            End Get
            Set(ByVal value As Integer)
                _trTraineeSalesSparepartID = value
            End Set
        End Property

        <ColumnInfo("SalesmanHeaderID", "{0}")> _
        Public Property SalesmanHeaderID As Integer
            Get
                Return _salesmanHeaderID
            End Get
            Set(ByVal value As Integer)
                _salesmanHeaderID = value
            End Set
        End Property

        <ColumnInfo("KodeSalesman", "'{0}'")> _
        Public Property KodeSalesman As String
            Get
                Return _kodeSalesman
            End Get
            Set(ByVal value As String)
                _kodeSalesman = value
            End Set
        End Property

        <ColumnInfo("NoWorkOrder", "'{0}'")> _
        Public Property NoWorkOrder As String
            Get
                Return _noWorkOrder
            End Get
            Set(ByVal value As String)
                _noWorkOrder = value
            End Set
        End Property

        <ColumnInfo("SparepartMasterID", "{0}")> _
        Public Property SparepartMasterID As Integer
            Get
                Return _sparepartMasterID
            End Get
            Set(ByVal value As Integer)
                _sparepartMasterID = value
            End Set
        End Property

        <ColumnInfo("NoParts", "'{0}'")> _
        Public Property NoParts As String
            Get
                Return _noParts
            End Get
            Set(ByVal value As String)
                _noParts = value
            End Set
        End Property

        <ColumnInfo("Qty", "#,##0")> _
        Public Property Qty As Double
            Get
                Return _qty
            End Get
            Set(ByVal value As Double)
                _qty = value
            End Set
        End Property

        <ColumnInfo("HargaBeli", "{0}")> _
        Public Property HargaBeli As Decimal
            Get
                Return _hargaBeli
            End Get
            Set(ByVal value As Decimal)
                _hargaBeli = value
            End Set
        End Property

        <ColumnInfo("HargaJual", "{0}")> _
        Public Property HargaJual As Decimal
            Get
                Return _hargaJual
            End Get
            Set(ByVal value As Decimal)
                _hargaJual = value
            End Set
        End Property

        <ColumnInfo("IsCampaign", "{0}")> _
        Public Property IsCampaign() As Boolean
            Get
                Return _isCampaign
            End Get
            Set(ByVal value As Boolean)
                _isCampaign = value
            End Set
        End Property

        <ColumnInfo("CampaignNo", "{0}")> _
        Public Property CampaignNo As String
            Get
                Return _campaignNo
            End Get
            Set(ByVal value As String)
                _campaignNo = value
            End Set
        End Property

        <ColumnInfo("CampaignDescription", "{0}")> _
        Public Property CampaignDescription As String
            Get
                Return _campaignDescription
            End Get
            Set(ByVal value As String)
                _campaignDescription = value
            End Set
        End Property

        <ColumnInfo("RemarksSystem", "'{0}'")> _
        Public Property RemarksSystem As String
            Get
                Return _remarksSystem
            End Get
            Set(ByVal value As String)
                _remarksSystem = value
            End Set
        End Property

        <ColumnInfo("StatusAktif", "{0}")> _
        Public Property StatusAktif As Short
            Get
                Return _statusAktif
            End Get
            Set(ByVal value As Short)
                _statusAktif = value
            End Set
        End Property

        <ColumnInfo("ValidateSystemStatus", "{0}")> _
        Public Property ValidateSystemStatus As Short
            Get
                Return _validateSystemStatus
            End Get
            Set(ByVal value As Short)
                _validateSystemStatus = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
       RelationInfo("Dealer", "ID", "AssistPartSales", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerBranchID", "{0}"), _
       RelationInfo("DealerBranch", "ID", "AssistPartSales", "DealerBranchID")> _
        Public Property DealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("AssistUploadLogID", "{0}"), _
        RelationInfo("AssistUploadLog", "ID", "AssistPartSales", "AssistUploadLogID")> _
        Public Property AssistUploadLog() As AssistUploadLog
            Get
                Try
                    If Not IsNothing(Me._assistuploadLog) AndAlso (Not Me._assistuploadLog.IsLoaded) Then

                        Me._assistuploadLog = CType(DoLoad(GetType(AssistUploadLog).ToString(), _assistuploadLog.ID), AssistUploadLog)
                        Me._assistuploadLog.MarkLoaded()

                    End If

                    Return Me._assistuploadLog

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistUploadLog)

                Me._assistuploadLog = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._assistuploadLog.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesChannelID", "{0}"), _
       RelationInfo("AssistSalesChannel", "ID", "AssistPartSales", "SalesChannelID")> _
        Public Property AssistSalesChannel() As AssistSalesChannel
            Get
                Try
                    If Not IsNothing(Me._salesChannel) AndAlso (Not Me._salesChannel.IsLoaded) Then

                        Me._salesChannel = CType(DoLoad(GetType(AssistSalesChannel).ToString(), _salesChannel.ID), AssistSalesChannel)
                        Me._salesChannel.MarkLoaded()

                    End If

                    Return Me._salesChannel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistSalesChannel)

                Me._salesChannel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesChannel.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparepartMasterID", "{0}"), _
       RelationInfo("SparePartMaster", "ID", "AssistPartSales", "SparepartMasterID")> _
        Public Property SparePartMaster() As SparePartMaster
            Get
                Try
                    If Not IsNothing(Me._sparePartsMaster) AndAlso (Not Me._sparePartsMaster.IsLoaded) Then

                        Me._sparePartsMaster = CType(DoLoad(GetType(SparePartMaster).ToString(), _sparePartsMaster.ID), SparePartMaster)
                        Me._sparePartsMaster.MarkLoaded()

                    End If

                    Return Me._sparePartsMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartMaster)

                Me._sparePartsMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartsMaster.MarkLoaded()
                End If
            End Set
        End Property

#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

