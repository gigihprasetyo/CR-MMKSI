
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistPartStock Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 10:28:45 AM
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
    <Serializable(), TableInfo("AssistPartStock")> _
    Public Class AssistPartStock
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
        Private _month As String = String.Empty
        Private _year As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
        Private _noParts As String = String.Empty
        Private _jumlahStokAwal As Decimal
        Private _jumlahDatang As Decimal
        Private _hargaBeli As Decimal
        Private _remarksSystem As String = String.Empty
        Private _statusAktif As Short
        Private _validateSystemStatus As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparepartMasterID As Integer
        Private _assistuploadLogID As Integer
        Private _dealerID As Integer
        Private _dealerBranchID As Integer

        Private _assistuploadLog As AssistUploadLog
        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
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

        <ColumnInfo("Month", "'{0}'")> _
        Public Property Month As String
            Get
                Return _month
            End Get
            Set(ByVal value As String)
                _month = value
            End Set
        End Property


        <ColumnInfo("Year", "'{0}'")> _
        Public Property Year As String
            Get
                Return _year
            End Get
            Set(ByVal value As String)
                _year = value
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

        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
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


        <ColumnInfo("JumlahStokAwal", "{0}")> _
        Public Property JumlahStokAwal As Decimal
            Get
                Return _jumlahStokAwal
            End Get
            Set(ByVal value As Decimal)
                _jumlahStokAwal = value
            End Set
        End Property

        <ColumnInfo("JumlahDatang", "{0}")> _
        Public Property JumlahDatang As Decimal
            Get
                Return _jumlahDatang
            End Get
            Set(ByVal value As Decimal)
                _jumlahDatang = value
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
       RelationInfo("Dealer", "ID", "AssistPartStock", "DealerID")> _
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
       RelationInfo("DealerBranch", "ID", "AssistPartStock", "DealerBranchID")> _
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
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("AssistUploadLogID", "{0}"), _
        RelationInfo("AssistUploadLog", "ID", "AssistPartStock", "AssistUploadLogID")> _
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

        <ColumnInfo("SparepartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "AssistPartStock", "SparepartMasterID")> _
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

