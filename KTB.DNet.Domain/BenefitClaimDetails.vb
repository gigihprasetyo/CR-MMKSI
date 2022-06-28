
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimDetails Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:54:29 AM
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
    <Serializable(), TableInfo("BenefitClaimDetails")> _
    Public Class BenefitClaimDetails
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
        Private _recLetterRegNo As String = String.Empty
        Private _detailStatus As Short
        Private _statusUpload As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _descDealer As String = String.Empty
        Private _descKtb As String = String.Empty

        Private _benefitClaimHeader As BenefitClaimHeader
        Private _chassisMaster As ChassisMaster

        Private _errorMessage As String = String.Empty

        Private _benefitMasterDetail As BenefitMasterDetail

        Private _leasingCompany As LeasingCompany

        Private _benefitClaimRecommendations As System.Collections.ArrayList = New System.Collections.ArrayList()

        Private _noBaris As Integer

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


        <ColumnInfo("RecLetterRegNo", "'{0}'")> _
        Public Property RecLetterRegNo As String
            Get
                Return _recLetterRegNo
            End Get
            Set(ByVal value As String)
                _recLetterRegNo = value
            End Set
        End Property


        <ColumnInfo("DetailStatus", "{0}")> _
        Public Property DetailStatus As Short
            Get
                Return _detailStatus
            End Get
            Set(ByVal value As Short)
                _detailStatus = value
            End Set
        End Property


        <ColumnInfo("StatusUpload", "{0}")> _
        Public Property StatusUpload As Short
            Get
                Return _statusUpload
            End Get
            Set(ByVal value As Short)
                _statusUpload = value
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

        <ColumnInfo("DescDealer", "'{0}'")> _
        Public Property DescDealer As String
            Get
                Return _descDealer
            End Get
            Set(ByVal value As String)
                _descDealer = value
            End Set
        End Property

        <ColumnInfo("DescKtb", "'{0}'")> _
        Public Property DescKtb As String
            Get
                Return _descKtb
            End Get
            Set(ByVal value As String)
                _descKtb = value
            End Set
        End Property

        <ColumnInfo("BenefitClaimHeaderID", "{0}"), _
        RelationInfo("BenefitClaimHeader", "ID", "BenefitClaimDetails", "BenefitClaimHeaderID")> _
        Public Property BenefitClaimHeader As BenefitClaimHeader
            Get
                Try
                    If Not isnothing(Me._benefitClaimHeader) AndAlso (Not Me._benefitClaimHeader.IsLoaded) Then
                        If _benefitClaimHeader.ID > 0 Then
                            Me._benefitClaimHeader = CType(DoLoad(GetType(BenefitClaimHeader).ToString(), _benefitClaimHeader.ID), BenefitClaimHeader)
                        Else
                            Me._benefitClaimHeader = _benefitClaimHeader
                        End If

                        Me._benefitClaimHeader.MarkLoaded()

                    End If

                    Return Me._benefitClaimHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitClaimHeader)

                Me._benefitClaimHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitClaimHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "BenefitClaimDetails", "ChassisMasterID")> _
        Public Property ChassisMaster As ChassisMaster
            Get
                Try
                    If Not isnothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then
                        If _chassisMaster.ID > 0 Then
                            Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        Else
                            Me._chassisMaster = _chassisMaster
                        End If

                        Me._chassisMaster.MarkLoaded()

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("BenefitClaimDetails", "ID", "BenefitClaimRecommendation", "BenefitClaimDetailsID")> _
        Public ReadOnly Property BenefitClaimRecommendations As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitClaimRecommendations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitClaimRecommendation), "BenefitClaimDetails", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitClaimRecommendation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitClaimRecommendations = DoLoadArray(GetType(BenefitClaimRecommendation).ToString, criterias)
                    End If

                    Return Me._benefitClaimRecommendations

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property




        <ColumnInfo("BenefitMasterDetailID", "{0}"), _
     RelationInfo("BenefitMasterDetail", "ID", "BenefitClaimDetails", "BenefitMasterDetailID")> _
        Public Property BenefitMasterDetail As BenefitMasterDetail
            Get
                Try
                    If Not IsNothing(Me._benefitMasterDetail) AndAlso (Not Me._benefitMasterDetail.IsLoaded) Then
                        If _benefitMasterDetail.ID > 0 Then
                            Me._benefitMasterDetail = CType(DoLoad(GetType(BenefitMasterDetail).ToString(), _benefitMasterDetail.ID), BenefitMasterDetail)
                        Else
                            Me._benefitMasterDetail = _benefitMasterDetail
                        End If

                        Me._benefitMasterDetail.MarkLoaded()

                    End If

                    Return Me._benefitMasterDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitMasterDetail)

                Me._benefitMasterDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitMasterDetail.MarkLoaded()
                End If
            End Set
        End Property




        <ColumnInfo("LeasingCompanyID", "{0}"), _
        RelationInfo("LeasingCompany", "ID", "BenefitClaimDetails", "LeasingCompanyID")> _
        Public Property LeasingCompany As LeasingCompany
            Get
                Try
                    If Not IsNothing(Me._leasingCompany) AndAlso (Not Me._leasingCompany.IsLoaded) Then
                        If _leasingCompany.ID > 0 Then
                            Me._leasingCompany = CType(DoLoad(GetType(LeasingCompany).ToString(), _leasingCompany.ID), LeasingCompany)
                        Else
                            Me._leasingCompany = _leasingCompany
                        End If

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

        Public Property NoBaris As Integer
            Get
                Return _noBaris
            End Get
            Set(ByVal value As Integer)
                _noBaris = value
            End Set
        End Property

#End Region

    End Class
End Namespace

