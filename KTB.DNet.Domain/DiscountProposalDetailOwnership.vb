
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailOwnership Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:44:48
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
    <Serializable(), TableInfo("DiscountProposalDetailOwnership")> _
    Public Class DiscountProposalDetailOwnership
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
        Private _vehicleBrandCategory As Short
        Private _vehicleBrandName As String = String.Empty
        Private _vehicleModel As String = String.Empty
        Private _quantity As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _discountProposalHeader As DiscountProposalHeader
        Private _numberRow As Short

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


        <ColumnInfo("VehicleBrandCategory", "{0}")> _
        Public Property VehicleBrandCategory As Short
            Get
                Return _vehicleBrandCategory
            End Get
            Set(ByVal value As Short)
                _vehicleBrandCategory = value
            End Set
        End Property


        <ColumnInfo("VehicleBrandName", "'{0}'")> _
        Public Property VehicleBrandName As String
            Get
                Return _vehicleBrandName
            End Get
            Set(ByVal value As String)
                _vehicleBrandName = value
            End Set
        End Property


        <ColumnInfo("VehicleModel", "'{0}'")> _
        Public Property VehicleModel As String
            Get
                Return _vehicleModel
            End Get
            Set(ByVal value As String)
                _vehicleModel = value
            End Set
        End Property


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
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


        <ColumnInfo("DiscountProposalHeaderID", "{0}"), _
        RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetailOwnership", "DiscountProposalHeaderID")> _
        Public Property DiscountProposalHeader As DiscountProposalHeader
            Get
                Try
                    If Not isnothing(Me._discountProposalHeader) AndAlso (Not Me._discountProposalHeader.IsLoaded) Then
                        If _discountProposalHeader.ID > 0 Then '
                            Me._discountProposalHeader = CType(DoLoad(GetType(DiscountProposalHeader).ToString(), _discountProposalHeader.ID), DiscountProposalHeader)
                            Me._discountProposalHeader.MarkLoaded()
                        End If
                    End If

                    Return Me._discountProposalHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DiscountProposalHeader)

                Me._discountProposalHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalHeader.MarkLoaded()
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
        Public Property NumberRow As Short
            Get
                Return _numberRow
            End Get
            Set(ByVal value As Short)
                _numberRow = value
            End Set
        End Property

#End Region

    End Class
End Namespace

