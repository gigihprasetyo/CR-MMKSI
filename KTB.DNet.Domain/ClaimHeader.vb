#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2007 - 10:41:25 AM
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
    <Serializable(), TableInfo("ClaimHeader")> _
    Public Class ClaimHeader
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
        Private _claimNo As String = String.Empty
        Private _claimDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _description As String = String.Empty
        Private _uploadFileName As String = String.Empty
        Private _status As Byte
        Private _statusKTB As Byte
        Private _kTBNote As String = String.Empty
        Private _fakturRetur As String = String.Empty
        Private _fakturReturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dONumber As String = String.Empty
        Private _deliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _receivedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _receivedDescription As String = String.Empty

        Private _sORetur As String = String.Empty
        Private _sOReturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _claimGoodCondition As ClaimGoodCondition
        Private _claimReason As ClaimReason
        Private _dealer As Dealer
        Private _sparePartPOStatus As SparePartPOStatus
        Private _claimProgress As ClaimProgress

        Private _claimDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _claimStatusHistorys As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("ClaimNo", "'{0}'")> _
        Public Property ClaimNo() As String
            Get
                Return _claimNo
            End Get
            Set(ByVal value As String)
                _claimNo = value
            End Set
        End Property


        <ColumnInfo("ClaimDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ClaimDate() As DateTime
            Get
                Return _claimDate
            End Get
            Set(ByVal value As DateTime)
                _claimDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("UploadFileName", "'{0}'")> _
        Public Property UploadFileName() As String
            Get
                Return _uploadFileName
            End Get
            Set(ByVal value As String)
                _uploadFileName = value
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


        <ColumnInfo("StatusKTB", "{0}")> _
        Public Property StatusKTB() As Byte
            Get
                Return _statusKTB
            End Get
            Set(ByVal value As Byte)
                _statusKTB = value
            End Set
        End Property


        <ColumnInfo("KTBNote", "'{0}'")> _
        Public Property KTBNote() As String
            Get
                Return _kTBNote
            End Get
            Set(ByVal value As String)
                _kTBNote = value
            End Set
        End Property


        <ColumnInfo("FakturRetur", "'{0}'")> _
        Public Property FakturRetur() As String
            Get
                Return _fakturRetur
            End Get
            Set(ByVal value As String)
                _fakturRetur = value
            End Set
        End Property


        <ColumnInfo("FakturReturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FakturReturDate() As DateTime
            Get
                Return _fakturReturDate
            End Get
            Set(ByVal value As DateTime)
                _fakturReturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber() As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property


        <ColumnInfo("DeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DeliveryDate() As DateTime
            Get
                Return _deliveryDate
            End Get
            Set(ByVal value As DateTime)
                _deliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ReceivedDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReceivedDate() As DateTime
            Get
                Return _receivedDate
            End Get
            Set(ByVal value As DateTime)
                _receivedDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ReceivedDescription", "'{0}'")> _
        Public Property ReceivedDescription() As String
            Get
                Return _receivedDescription
            End Get
            Set(ByVal value As String)
                _receivedDescription = value
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


        <ColumnInfo("ReceivedGoodsConditionID", "{0}"), _
        RelationInfo("ClaimGoodCondition", "ID", "ClaimHeader", "ReceivedGoodsConditionID")> _
        Public Property ClaimGoodCondition() As ClaimGoodCondition
            Get
                Try
                    If Not IsNothing(Me._claimGoodCondition) AndAlso (Not Me._claimGoodCondition.IsLoaded) Then

                        Me._claimGoodCondition = CType(DoLoad(GetType(ClaimGoodCondition).ToString(), _claimGoodCondition.ID), ClaimGoodCondition)
                        Me._claimGoodCondition.MarkLoaded()

                    End If

                    Return Me._claimGoodCondition

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ClaimGoodCondition)

                Me._claimGoodCondition = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimGoodCondition.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ClaimReasonHeaderID", "{0}"), _
        RelationInfo("ClaimReason", "ID", "ClaimHeader", "ClaimReasonHeaderID")> _
        Public Property ClaimReason() As ClaimReason
            Get
                Try
                    If Not IsNothing(Me._claimReason) AndAlso (Not Me._claimReason.IsLoaded) Then

                        Me._claimReason = CType(DoLoad(GetType(ClaimReason).ToString(), _claimReason.ID), ClaimReason)
                        Me._claimReason.MarkLoaded()

                    End If

                    Return Me._claimReason

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ClaimReason)

                Me._claimReason = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimReason.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ClaimHeader", "DealerID")> _
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

        <ColumnInfo("SparePartPOStatusID", "{0}"), _
        RelationInfo("SparePartPOStatus", "ID", "ClaimHeader", "SparePartPOStatusID")> _
        Public Property SparePartPOStatus() As SparePartPOStatus
            Get
                Try
                    If Not IsNothing(Me._sparePartPOStatus) AndAlso (Not Me._sparePartPOStatus.IsLoaded) Then

                        Me._sparePartPOStatus = CType(DoLoad(GetType(SparePartPOStatus).ToString(), _sparePartPOStatus.ID), SparePartPOStatus)
                        Me._sparePartPOStatus.MarkLoaded()

                    End If

                    Return Me._sparePartPOStatus

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPOStatus)

                Me._sparePartPOStatus = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPOStatus.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ClaimProgressID", "{0}"), _
  RelationInfo("ClaimProgress", "ID", "ClaimHeader", "ClaimProgressID")> _
        Public Property ClaimProgress() As ClaimProgress
            Get
                Try
                    If Not IsNothing(Me._claimProgress) AndAlso (Not Me._claimProgress.IsLoaded) Then

                        Me._claimProgress = CType(DoLoad(GetType(ClaimProgress).ToString(), _claimProgress.ID), ClaimProgress)
                        Me._claimProgress.MarkLoaded()

                    End If

                    Return Me._claimProgress

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ClaimProgress)

                Me._claimProgress = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimProgress.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("ClaimHeader", "ID", "ClaimDetail", "ClaimHeaderID")> _
        Public ReadOnly Property ClaimDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._claimDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ClaimDetail), "ClaimHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._claimDetails = DoLoadArray(GetType(ClaimDetail).ToString, criterias)
                    End If

                    Return Me._claimDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ClaimHeader", "ID", "ClaimStatusHistory", "ClaimHeaderID")> _
        Public ReadOnly Property ClaimStatusHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._claimStatusHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ClaimStatusHistory), "ClaimHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._claimStatusHistorys = DoLoadArray(GetType(ClaimStatusHistory).ToString, criterias)
                    End If

                    Return Me._claimStatusHistorys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <ColumnInfo("SORetur", "'{0}'")> _
        Public Property SORetur() As String
            Get
                Return _sORetur
            End Get
            Set(ByVal value As String)
                _sORetur = value
            End Set
        End Property


        <ColumnInfo("SOReturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SOReturDate() As DateTime
            Get
                Return _sOReturDate
            End Get
            Set(ByVal value As DateTime)
                _sOReturDate = New DateTime(value.Year, value.Month, value.Day)
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

