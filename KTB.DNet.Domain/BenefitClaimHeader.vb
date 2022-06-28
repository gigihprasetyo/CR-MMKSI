
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:55:21 AM
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
    <Serializable(), TableInfo("BenefitClaimHeader")> _
    Public Class BenefitClaimHeader
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
        Private _claimRegNo As String = String.Empty
        Private _mmksiNotes As String = String.Empty
        Private _claimDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
       
        Private _jVNumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _isTransfer As Short
        Private _Status As Short

        Private _actualPaymentDate As Integer

        ' Private _benefitMasterDetail As BenefitMasterDetail
        Private _benefitType As BenefitType
        Private _benefitEventHeader As BenefitEventHeader

        Private _dealer As Dealer
        Private _leasingCompany As LeasingCompany

        Private _benefitClaimReceipts As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _benefitClaimDetailss As System.Collections.ArrayList = New System.Collections.ArrayList()

        Private _benefitClaimJVs As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _benefitClaimJV As New BenefitClaimJV

        Private _hasReceipt As Boolean
        Private _totalNilaiClaim As Decimal
        Private _totalPPh As Decimal
        Private _totalPPn As Decimal

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


        <ColumnInfo("ClaimRegNo", "'{0}'")> _
        Public Property ClaimRegNo As String
            Get
                Return _claimRegNo
            End Get
            Set(ByVal value As String)
                _claimRegNo = value
            End Set
        End Property

        <ColumnInfo("MMKSINotes", "'{0}'")> _
        Public Property MMKSINotes As String
            Get
                Return _mmksiNotes
            End Get
            Set(ByVal value As String)
                _mmksiNotes = value
            End Set
        End Property


        <ColumnInfo("ClaimDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ClaimDate As DateTime
            Get
                Return _claimDate
            End Get
            Set(ByVal value As DateTime)
                _claimDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property




        <ColumnInfo("JVNumber", "'{0}'")> _
        Public Property JVNumber As String
            Get
                Return _jVNumber
            End Get
            Set(ByVal value As String)
                _jVNumber = value
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

        <ColumnInfo("IsTransfer", "{0}")> _
        Public Property IsTransfer As Short
            Get
                Return _isTransfer
            End Get
            Set(ByVal value As Short)
                _isTransfer = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _Status
            End Get
            Set(ByVal value As Short)
                _Status = value
            End Set
        End Property


     

        <ColumnInfo("DealerID", "{0}"), _
      RelationInfo("Dealer", "ID", "BenefitClaimHeader", "DealerID")> _
        Public Property Dealer As Dealer
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


        <ColumnInfo("LeasingCompanyID", "{0}"), _
   RelationInfo("LeasingCompany", "ID", "BenefitClaimHeader", "LeasingCompanyID")> _
        Public Property LeasingCompany As LeasingCompany

            '<ColumnInfo("LeasingCompanyID", "{0}")> _
            'Public Property LeasingCompany As LeasingCompany
            Get
                Try
                    If Not IsNothing(Me._leasingCompany) AndAlso (Not Me._leasingCompany.IsLoaded) Then

                        Me._leasingCompany = CType(DoLoad(GetType(LeasingCompany).ToString(), _leasingCompany.ID), LeasingCompany)
                        Me._leasingCompany.MarkLoaded()

                    End If


                    Return Me._leasingCompany

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As LeasingCompany)

                Me._leasingCompany = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._leasingCompany.MarkLoaded()
                End If
            End Set
        End Property

        Public ReadOnly Property LeasingCompanyName As String
            Get
                If Not IsNothing(LeasingCompany) Then
                    Return LeasingCompany.LeasingName
                Else
                    Return ""
                End If
            End Get
        End Property


        <ColumnInfo("BenefitTypeID", "{0}"), _
      RelationInfo("BenefitType", "ID", "BenefitClaimHeader", "BenefitTypeID")> _
        Public Property BenefitType As BenefitType
            Get
                Try
                    If Not IsNothing(Me._benefitType) AndAlso (Not Me._benefitType.IsLoaded) Then

                        Me._benefitType = CType(DoLoad(GetType(BenefitType).ToString(), _benefitType.ID), BenefitType)
                        Me._benefitType.MarkLoaded()

                    End If

                    Return Me._benefitType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitType)

                Me._benefitType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitType.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("BenefitMasterDetailID", "{0}"), _
        'RelationInfo("BenefitMasterDetail", "ID", "BenefitClaimHeader", "BenefitMasterDetailID")> _
        'Public Property BenefitMasterDetail As BenefitMasterDetail
        '    Get
        '        Try
        '            If Not isnothing(Me._benefitMasterDetail) AndAlso (Not Me._benefitMasterDetail.IsLoaded) Then

        '                Me._benefitMasterDetail = CType(DoLoad(GetType(BenefitMasterDetail).ToString(), _benefitMasterDetail.ID), BenefitMasterDetail)
        '                Me._benefitMasterDetail.MarkLoaded()

        '            End If

        '            Return Me._benefitMasterDetail

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As BenefitMasterDetail)

        '        Me._benefitMasterDetail = value
        '        If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._benefitMasterDetail.MarkLoaded()
        '        End If
        '    End Set
        'End Property

        <ColumnInfo("BenefitEventHeaderID", "{0}"), _
        RelationInfo("BenefitEventHeader", "ID", "BenefitClaimHeader", "BenefitEventHeaderID")> _
        Public Property BenefitEventHeader As BenefitEventHeader
            Get
                Try
                    If Not IsNothing(Me._benefitEventHeader) AndAlso (Not Me._benefitEventHeader.IsLoaded) Then

                        Me._benefitEventHeader = CType(DoLoad(GetType(BenefitEventHeader).ToString(), _benefitEventHeader.ID), BenefitEventHeader)
                        Me._benefitEventHeader.MarkLoaded()

                    End If

                    Return Me._benefitEventHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitEventHeader)

                Me._benefitEventHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitEventHeader.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("BenefitClaimHeader", "ID", "BenefitClaimReceipt", "BenefitClaimHeaderID")> _
        Public ReadOnly Property BenefitClaimReceipts As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitClaimReceipts.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.ID", MatchType.Exact, Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitClaimReceipts = DoLoadArray(GetType(BenefitClaimReceipt).ToString, criterias)
                    End If

                    Return Me._benefitClaimReceipts

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BenefitClaimHeader", "ID", "BenefitClaimDetails", "BenefitClaimHeaderID")> _
        Public ReadOnly Property BenefitClaimDetailss As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitClaimDetailss.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitClaimDetails), "BenefitClaimHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitClaimDetailss = DoLoadArray(GetType(BenefitClaimDetails).ToString, criterias)
                    End If

                    Return Me._benefitClaimDetailss

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property



        <RelationInfo("BenefitClaimHeader", "ID", "BenefitClaimJV", "BenefitClaimHeaderID")> _
        Public ReadOnly Property BenefitClaimJVs As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitClaimJVs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitClaimJVs = DoLoadArray(GetType(BenefitClaimJV).ToString, criterias)
                    End If

                    Return Me._benefitClaimJVs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        Public ReadOnly Property BenefitClaimJV As BenefitClaimJV
            Get
                Try
                    If (Me._benefitClaimJVs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Me._benefitClaimJVs = DoLoadArray(GetType(BenefitClaimJV).ToString, criterias)
                    End If

                    If Not IsNothing(_benefitClaimJVs) AndAlso _benefitClaimJVs.Count > 0 Then
                        _benefitClaimJV = CType(_benefitClaimJVs(0), BenefitClaimJV)
                    End If

                    Return Me._benefitClaimJV

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

#Region "Custom Public Properties"
        Public Property TotalNilaiClaim As Decimal
            Get
                Return _totalNilaiClaim
            End Get
            Set(ByVal value As Decimal)
                _totalNilaiClaim = value
            End Set
        End Property

        Public Property TotalPPh As Decimal
            Get
                Return _totalPPh
            End Get
            Set(ByVal value As Decimal)
                _totalPPh = value
            End Set
        End Property

        Public Property TotalPPn As Decimal
            Get
                Return _totalPPn
            End Get
            Set(ByVal value As Decimal)
                _totalPPn = value
            End Set
        End Property

        Public Property HasReceipt As Boolean
            Get
                Return _hasReceipt
            End Get
            Set(ByVal value As Boolean)
                _hasReceipt = value
            End Set
        End Property

        Public Property ActualPaymentDate As Integer
            Get
                Return _actualPaymentDate
            End Get
            Set(ByVal value As Integer)
                _actualPaymentDate = value
            End Set
        End Property

#End Region

    End Class
End Namespace

