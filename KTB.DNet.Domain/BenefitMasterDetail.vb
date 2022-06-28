
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitMasterDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2015 - 8:49:22 AM
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
    <Serializable(), TableInfo("BenefitMasterDetail")> _
    Public Class BenefitMasterDetail
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
        Private _formulaID As String = String.Empty
        Private _description As String = String.Empty
        Private _amount As Decimal
        Private _fakturValidationStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturValidationEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _fakturOpenStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturOpenEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _assyYear As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _maxClaim As Short
        Private _wsDiscount As Short

        Private _benefitMasterHeader As BenefitMasterHeader
        Private _benefitType As BenefitType

        Private _benefitMasterVehicleTypes As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _benefitMasterLeasings As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _benefitClaimDetailss As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("FormulaID", "'{0}'")> _
        Public Property FormulaID As String
            Get
                Return _formulaID
            End Get
            Set(ByVal value As String)
                _formulaID = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("FakturValidationStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturValidationStart As DateTime
            Get
                Return _fakturValidationStart
            End Get
            Set(ByVal value As DateTime)
                _fakturValidationStart = value
            End Set
        End Property


        <ColumnInfo("FakturValidationEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturValidationEnd As DateTime
            Get
                Return _fakturValidationEnd
            End Get
            Set(ByVal value As DateTime)
                _fakturValidationEnd = value
            End Set
        End Property


        <ColumnInfo("FakturOpenStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturOpenStart As DateTime
            Get
                Return _fakturOpenStart
            End Get
            Set(ByVal value As DateTime)
                _fakturOpenStart = value
            End Set
        End Property


        <ColumnInfo("FakturOpenEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturOpenEnd As DateTime
            Get
                Return _fakturOpenEnd
            End Get
            Set(ByVal value As DateTime)
                _fakturOpenEnd = value
            End Set
        End Property


        <ColumnInfo("AssyYear", "{0}")> _
        Public Property AssyYear As Short
            Get
                Return _assyYear
            End Get
            Set(ByVal value As Short)
                _assyYear = value
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


        <ColumnInfo("MaxClaim", "{0}")> _
        Public Property MaxClaim As Short
            Get
                Return _maxClaim
            End Get
            Set(ByVal value As Short)
                _maxClaim = value
            End Set
        End Property

        <ColumnInfo("WSDiscount", "{0}")> _
        Public Property WSDiscount As Short
            Get
                Return _wsDiscount
            End Get
            Set(ByVal value As Short)
                _wsDiscount = value
            End Set
        End Property


        <ColumnInfo("BenefitMasterHeaderID", "{0}"), _
        RelationInfo("BenefitMasterHeader", "ID", "BenefitMasterDetail", "BenefitMasterHeaderID")> _
        Public Property BenefitMasterHeader As BenefitMasterHeader
            Get
                Try
                    If Not isnothing(Me._benefitMasterHeader) AndAlso (Not Me._benefitMasterHeader.IsLoaded) Then

                        Me._benefitMasterHeader = CType(DoLoad(GetType(BenefitMasterHeader).ToString(), _benefitMasterHeader.ID), BenefitMasterHeader)
                        ' Me._benefitMasterHeader = _benefitMasterHeader
                        Me._benefitMasterHeader.MarkLoaded()

                    End If

                    Return Me._benefitMasterHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitMasterHeader)

                Me._benefitMasterHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitMasterHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BenefitTypeID", "{0}"), _
        RelationInfo("BenefitType", "ID", "BenefitMasterDetail", "BenefitTypeID")> _
        Public Property BenefitType As BenefitType
            Get
                Try
                    If Not isnothing(Me._benefitType) AndAlso (Not Me._benefitType.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitType.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("BenefitMasterDetail", "ID", "BenefitMasterVehicleType", "BenefitMasterDetailID")> _
        Public ReadOnly Property BenefitMasterVehicleTypes As System.Collections.ArrayList
            'Public Property BenefitMasterVehicleTypes As System.Collections.ArrayList


            Get
                Try
                    If (Me._benefitMasterVehicleTypes.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitMasterVehicleType), "BenefitMasterDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitMasterVehicleType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitMasterVehicleTypes = DoLoadArray(GetType(BenefitMasterVehicleType).ToString, criterias)
                    End If

                    Return Me._benefitMasterVehicleTypes

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get

            'Set(ByVal value As System.Collections.ArrayList)

            '    Me._benefitMasterVehicleTypes = value

            'End Set

        End Property

        <RelationInfo("BenefitMasterDetail", "ID", "BenefitMasterLeasing", "BenefitMasterDetailID")> _
        Public ReadOnly Property BenefitMasterLeasings As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitMasterLeasings.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitMasterLeasing), "BenefitMasterDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitMasterLeasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitMasterLeasings = DoLoadArray(GetType(BenefitMasterLeasing).ToString, criterias)
                    End If

                    Return Me._benefitMasterLeasings

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BenefitMasterDetail", "ID", "BenefitClaimDetail", "BenefitMasterDetailID")> _
        Public ReadOnly Property BenefitClaimDetailss As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitClaimDetailss.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitClaimDetails), "BenefitMasterDetail", Me.ID)
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

