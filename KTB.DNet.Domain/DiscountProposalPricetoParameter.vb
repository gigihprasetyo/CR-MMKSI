#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalPricetoParameter Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/24/2020 - 11:01:44 AM
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
    <Serializable(), TableInfo("DiscountProposalPricetoParameter")> _
    Public Class DiscountProposalPricetoParameter
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
        Private _amount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _discountProposalParameter As DiscountProposalParameter
        Private _discountProposalDetailPrice As DiscountProposalDetailPrice

        Private _numberRowParent As Short
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


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
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


        <ColumnInfo("DiscountProposalParameterID", "{0}"), _
        RelationInfo("DiscountProposalParameter", "ID", "DiscountProposalPricetoParameter", "DiscountProposalParameterID")> _
        Public Property DiscountProposalParameter As DiscountProposalParameter
            Get
                Try
                    If Not IsNothing(Me._discountProposalParameter) AndAlso (Not Me._discountProposalParameter.IsLoaded) Then

                        Me._discountProposalParameter = CType(DoLoad(GetType(DiscountProposalParameter).ToString(), _discountProposalParameter.ID), DiscountProposalParameter)
                        Me._discountProposalParameter.MarkLoaded()

                    End If

                    Return Me._discountProposalParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DiscountProposalParameter)

                Me._discountProposalParameter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalParameter.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DiscountProposalDetailPriceID", "{0}"), _
        RelationInfo("DiscountProposalDetailPrice", "ID", "DiscountProposalPricetoParameter", "DiscountProposalDetailPriceID")> _
        Public Property DiscountProposalDetailPrice As DiscountProposalDetailPrice
            Get
                Try
                    If Not IsNothing(Me._discountProposalDetailPrice) AndAlso (Not Me._discountProposalDetailPrice.IsLoaded) Then
                        Try
                            Me._discountProposalDetailPrice = CType(DoLoad(GetType(DiscountProposalDetailPrice).ToString(), _discountProposalDetailPrice.ID), DiscountProposalDetailPrice)
                            Me._discountProposalDetailPrice.MarkLoaded()
                        Catch
                        End Try
                    End If

                    Return Me._discountProposalDetailPrice

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DiscountProposalDetailPrice)

                Me._discountProposalDetailPrice = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalDetailPrice.MarkLoaded()
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
        Public Property NumberRowParent As Short
            Get
                Return _numberRowParent
            End Get
            Set(ByVal value As Short)
                _numberRowParent = value
            End Set
        End Property
#End Region

    End Class
End Namespace
