#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EstimationEquipHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/18/2009 - 10:32:35
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
    <Serializable(), TableInfo("EstimationEquipHeader")> _
    Public Class EstimationEquipHeader
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
        Private _estimationNumber As String = String.Empty
        Private _status As Short
        Private _purpose As String = String.Empty
        Private _dMSPRNo As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerId As Integer = 0
        Private _dealer As Dealer
        Private _depositBKewajibanHeader As DepositBKewajibanHeader

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("EstimationNumber", "'{0}'")> _
        Public Property EstimationNumber() As String
            Get
                Return _estimationNumber
            End Get
            Set(ByVal value As String)
                _estimationNumber = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
            End Set
        End Property


        <ColumnInfo("DMSPRNo", "'{0}'")> _
        Public Property DMSPRNo() As String
            Get
                Return _dMSPRNo
            End Get
            Set(ByVal value As String)
                _dMSPRNo = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy() As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime() As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property

        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID() As Integer
        '    Get
        '        Return _dealerId
        '    End Get
        '    Set(ByVal value As Integer)
        '        _dealerId = value
        '    End Set
        'End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "EstimationEquipHeader", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    'If IsNothing(Me._dealer) Then
                    '    Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealerId), Dealer)
                    '    Me._dealer.MarkLoaded()
                    'End If

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

        <ColumnInfo("DepositBKewajibanHeaderID", "{0}"), _
        RelationInfo("DepositBKewajibanHeader", "ID", "EstimationEquipHeader", "DepositBKewajibanHeaderID")> _
        Public Property DepositBKewajibanHeader() As DepositBKewajibanHeader
            Get
                Try
                    If Not IsNothing(Me._depositBKewajibanHeader) AndAlso (Not Me._depositBKewajibanHeader.IsLoaded) Then
                        Me._depositBKewajibanHeader = CType(DoLoad(GetType(DepositBKewajibanHeader).ToString(), _depositBKewajibanHeader.ID), DepositBKewajibanHeader)
                        Me._depositBKewajibanHeader.MarkLoaded()
                    End If

                    Return Me._depositBKewajibanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBKewajibanHeader)

                Me._depositBKewajibanHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        Private _estimationEquipDetail = New ArrayList

        <RelationInfo("EstimationEquipHeader", "ID", "EstimationEquipDetail", "EstimationEquipHeaderID")> _
        Public Property EstimationEquipDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._estimationEquipDetail.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EstimationEquipDetail), "EstimationEquipHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EstimationEquipDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._estimationEquipDetail = DoLoadArray(GetType(EstimationEquipDetail).ToString, criterias)
                    End If

                    Return Me._estimationEquipDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get

            Set(ByVal Value As System.Collections.ArrayList)
                Me._estimationEquipDetail = Value
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

        Public ReadOnly Property TotalQty() As Integer
            Get
                'Todo Aggregate
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim Total As Integer = 0

                For Each item As EstimationEquipDetail In Me.EstimationEquipDetails
                    Total += item.EstimationUnit
                Next

                Return Total

            End Get
        End Property

        Public ReadOnly Property TotalAmount() As Decimal
            Get
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim Total As Decimal = 0

                For Each item As EstimationEquipDetail In Me.EstimationEquipDetails
                    Total += item.EstimationUnit * item.Harga
                Next
                Return Total
            End Get
        End Property

        'Dim _poQty As Integer = 0
        'Public ReadOnly Property POQty() As Integer
        '    Get
        '        Return _poQty
        '    End Get
        'End Property


        Public ReadOnly Property StatusDesc() As String
            Get
                Select Case _status
                    Case EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru
                        Return EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru.ToString()
                    Case EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal
                        Return EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal.ToString()
                    Case EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim
                        Return EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim.ToString()
                    Case EnumEstimationEquipStatus.EstimationEquipStatusHeader.Konfirmasi_Sebagian
                        Return EnumEstimationEquipStatus.EstimationEquipStatusHeader.Konfirmasi_Sebagian.ToString
                    Case EnumEstimationEquipStatus.EstimationEquipStatusHeader.Selesai
                        Return EnumEstimationEquipStatus.EstimationEquipStatusHeader.Selesai.ToString
                        'Case EnumEstimationEquipStatus.EstimationEquipStatusHeader.Tolak
                        '    Return EnumEstimationEquipStatus.EstimationEquipStatusHeader.Tolak.ToString
                End Select
            End Get
        End Property

#End Region

    End Class
End Namespace