
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimDeducted Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 13/12/2019 - 10:04:27
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
    <Serializable(), TableInfo("BenefitClaimDeducted")> _
    Public Class BenefitClaimDeducted
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
        Private _deductedAmount As Decimal
        Private _remainAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _benefitClaimHeader As BenefitClaimHeader
        Private _dSFLeasingClaim As DSFLeasingClaim



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


        <ColumnInfo("DeductedAmount", "{0}")> _
        Public Property DeductedAmount As Decimal
            Get
                Return _deductedAmount
            End Get
            Set(ByVal value As Decimal)
                _deductedAmount = value
            End Set
        End Property


        <ColumnInfo("RemainAmount", "{0}")> _
        Public Property RemainAmount As Decimal
            Get
                Return _remainAmount
            End Get
            Set(ByVal value As Decimal)
                _remainAmount = value
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


        <ColumnInfo("BenefitClaimHeaderID", "{0}"), _
        RelationInfo("BenefitClaimHeader", "ID", "BenefitClaimDeducted", "BenefitClaimHeaderID")> _
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

        <ColumnInfo("DSFLeasingClaimID", "{0}"), _
        RelationInfo("DSFLeasingClaim", "ID", "BenefitClaimDeducted", "DSFLeasingClaimID")> _
        Public Property DSFLeasingClaim As DSFLeasingClaim
            Get
                Try
                    If Not IsNothing(Me._dSFLeasingClaim) AndAlso (Not Me._dSFLeasingClaim.IsLoaded) Then

                        Me._dSFLeasingClaim = CType(DoLoad(GetType(DSFLeasingClaim).ToString(), _dSFLeasingClaim.ID), DSFLeasingClaim)
                        Me._dSFLeasingClaim.MarkLoaded()

                    End If

                    Return Me._dSFLeasingClaim

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DSFLeasingClaim)

                Me._dSFLeasingClaim = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dSFLeasingClaim.MarkLoaded()
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

