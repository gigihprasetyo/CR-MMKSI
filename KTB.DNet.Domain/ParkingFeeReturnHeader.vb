
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ParkingFeeReturnHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 3/8/2012 - 8:59:31 AM
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
    <Serializable(), TableInfo("ParkingFeeReturnHeader")> _
    Public Class ParkingFeeReturnHeader
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
        Private _noReg As String = String.Empty
        Private _buktiPotongNumber As String = String.Empty
        Private _returnDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _returnAssignNumber As String = String.Empty
        Private _totalAmount As Decimal
        Private _pPHAmount As Decimal
        Private _description As String = String.Empty
        Private _status As Byte
        Private _kantorPajak As String = String.Empty
        Private _kotaDealer As String = String.Empty
        Private _pejabat As String = String.Empty
        Private _jabatan As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        Private _parkingFeeReturnDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _parkingFeeHistorys As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("NoReg", "'{0}'")> _
        Public Property NoReg() As String
            Get
                Return _noReg
            End Get
            Set(ByVal value As String)
                _noReg = value
            End Set
        End Property


        <ColumnInfo("BuktiPotongNumber", "'{0}'")> _
        Public Property BuktiPotongNumber() As String
            Get
                Return _buktiPotongNumber
            End Get
            Set(ByVal value As String)
                _buktiPotongNumber = value
            End Set
        End Property


        <ColumnInfo("ReturnDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReturnDate() As DateTime
            Get
                Return _returnDate
            End Get
            Set(ByVal value As DateTime)
                _returnDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ReturnAssignNumber", "'{0}'")> _
        Public Property ReturnAssignNumber() As String
            Get
                Return _returnAssignNumber
            End Get
            Set(ByVal value As String)
                _returnAssignNumber = value
            End Set
        End Property


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount() As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
            End Set
        End Property


        <ColumnInfo("PPHAmount", "{0}")> _
        Public Property PPHAmount() As Decimal
            Get
                Return _pPHAmount
            End Get
            Set(ByVal value As Decimal)
                _pPHAmount = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
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

        <ColumnInfo("KantorPajak", "'{0}'")> _
                Public Property KantorPajak() As String
            Get
                Return _kantorPajak
            End Get
            Set(ByVal value As String)
                _kantorPajak = value
            End Set
        End Property

        <ColumnInfo("KotaDealer", "'{0}'")> _
                Public Property KotaDealer() As String
            Get
                Return _kotaDealer
            End Get
            Set(ByVal value As String)
                _kotaDealer = value
            End Set
        End Property

        <ColumnInfo("Pejabat", "'{0}'")> _
                Public Property Pejabat() As String
            Get
                Return _pejabat
            End Get
            Set(ByVal value As String)
                _pejabat = value
            End Set
        End Property

        <ColumnInfo("Jabatan", "'{0}'")> _
                Public Property Jabatan() As String
            Get
                Return _jabatan
            End Get
            Set(ByVal value As String)
                _jabatan = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ParkingFeeReturnHeader", "DealerID")> _
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


        <RelationInfo("ParkingFeeReturnHeader", "ID", "ParkingFeeReturnDetail", "HeaderID")> _
        Public ReadOnly Property ParkingFeeReturnDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._parkingFeeReturnDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ParkingFeeReturnDetail), "ParkingFeeReturnHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ParkingFeeReturnDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._parkingFeeReturnDetails = DoLoadArray(GetType(ParkingFeeReturnDetail).ToString, criterias)
                    End If

                    Return Me._parkingFeeReturnDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ParkingFeeReturnHeader", "ID", "ParkingFeeHistory", "ParkingFeeReturnHeaderID")> _
        Public ReadOnly Property ParkingFeeHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._parkingFeeHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ParkingFeeHistory), "ParkingFeeReturnHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ParkingFeeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._parkingFeeHistorys = DoLoadArray(GetType(ParkingFeeHistory).ToString, criterias)
                    End If

                    Return Me._parkingFeeHistorys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


#End Region

#Region "Generated Method"
        
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

