
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimDeductedHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 16/12/2019 - 14:17:38
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
    <Serializable(), TableInfo("BenefitClaimDeductedHistory")> _
    Public Class BenefitClaimDeductedHistory
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Short)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Short
        Private _amountDeducted As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _benefitClaimDeducted As BenefitClaimDeducted
        Private _benefitClaimHeader As BenefitClaimHeader



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Short
            Get
                Return _iD
            End Get
            Set(ByVal value As Short)
                _iD = value
            End Set
        End Property


        <ColumnInfo("AmountDeducted", "{0}")> _
        Public Property AmountDeducted As Decimal
            Get
                Return _amountDeducted
            End Get
            Set(ByVal value As Decimal)
                _amountDeducted = value
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


        <ColumnInfo("BenefitClaimDeductedID", "{0}"), _
        RelationInfo("BenefitClaimDeducted", "ID", "BenefitClaimDeductedHistory", "BenefitClaimDeductedID")> _
        Public Property BenefitClaimDeducted As BenefitClaimDeducted
            Get
                Try
                    If Not isnothing(Me._benefitClaimDeducted) AndAlso (Not Me._benefitClaimDeducted.IsLoaded) Then

                        Me._benefitClaimDeducted = CType(DoLoad(GetType(BenefitClaimDeducted).ToString(), _benefitClaimDeducted.ID), BenefitClaimDeducted)
                        Me._benefitClaimDeducted.MarkLoaded()

                    End If

                    Return Me._benefitClaimDeducted

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitClaimDeducted)

                Me._benefitClaimDeducted = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitClaimDeducted.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BenefitClaimHeaderID", "{0}"), _
        RelationInfo("BenefitClaimHeader", "ID", "BenefitClaimDeductedHistory", "BenefitClaimHeaderID")> _
        Public Property BenefitClaimHeader As BenefitClaimHeader
            Get
                Try
                    If Not isnothing(Me._benefitClaimHeader) AndAlso (Not Me._benefitClaimHeader.IsLoaded) Then

                        Me._benefitClaimHeader = CType(DoLoad(GetType(BenefitClaimHeader).ToString(), _benefitClaimHeader.ID), BenefitClaimHeader)
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

