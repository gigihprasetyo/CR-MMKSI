#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalProgramReguler Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/15/2020 - 2:08:10 PM
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
    <Serializable(), TableInfo("DiscountProposalProgramReguler")> _
    Public Class DiscountProposalProgramReguler
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
        Private _assyYear As Short
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _discountAmount As Decimal
        Private _programBased As String = String.Empty
        Private _modelYear As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _discountProposalParameter As DiscountProposalParameter
        Private _vechileTypeGeneral As VechileTypeGeneral



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


        <ColumnInfo("AssyYear", "{0}")> _
        Public Property AssyYear As Short
            Get
                Return _assyYear
            End Get
            Set(ByVal value As Short)
                _assyYear = value
            End Set
        End Property


        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property


        <ColumnInfo("ValidTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidTo As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
            End Set
        End Property


        <ColumnInfo("DiscountAmount", "{0}")> _
        Public Property DiscountAmount As Decimal
            Get
                Return _discountAmount
            End Get
            Set(ByVal value As Decimal)
                _discountAmount = value
            End Set
        End Property


        <ColumnInfo("ProgramBased", "{0}")> _
        Public Property ProgramBased As String
            Get
                Return _programBased
            End Get
            Set(ByVal value As String)
                _programBased = value
            End Set
        End Property


        <ColumnInfo("ModelYear", "{0}")> _
        Public Property ModelYear As Short
            Get
                Return _modelYear
            End Get
            Set(ByVal value As Short)
                _modelYear = value
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
        RelationInfo("DiscountProposalParameter", "ID", "DiscountProposalProgramReguler", "DiscountProposalParameterID")> _
        Public Property DiscountProposalParameter As DiscountProposalParameter
            Get
                Try
                    If Not isnothing(Me._discountProposalParameter) AndAlso (Not Me._discountProposalParameter.IsLoaded) Then
                        If _discountProposalParameter.ID > 0 Then
                            Me._discountProposalParameter = CType(DoLoad(GetType(DiscountProposalParameter).ToString(), _discountProposalParameter.ID), DiscountProposalParameter)
                            Me._discountProposalParameter.MarkLoaded()
                        End If
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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalParameter.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VechileTypeGeneralID", "{0}"), _
        RelationInfo("VechileTypeGeneral", "ID", "DiscountProposalProgramReguler", "VechileTypeGeneralID")> _
        Public Property VechileTypeGeneral As VechileTypeGeneral
            Get
                Try
                    If Not IsNothing(Me._vechileTypeGeneral) AndAlso (Not Me._vechileTypeGeneral.IsLoaded) Then

                        Me._vechileTypeGeneral = CType(DoLoad(GetType(VechileTypeGeneral).ToString(), _vechileTypeGeneral.ID), VechileTypeGeneral)
                        Me._vechileTypeGeneral.MarkLoaded()

                    End If

                    Return Me._vechileTypeGeneral

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileTypeGeneral)

                Me._vechileTypeGeneral = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileTypeGeneral.MarkLoaded()
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
