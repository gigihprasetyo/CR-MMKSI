#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : Price Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2005 - 2:48:06 PM
'// Modified on 8/25/2014 - 10:02:58 AM
'//     Change Log :    -> Append DiscountReward Column
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
    <Serializable(), TableInfo("Price")> _
    Public Class Price
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
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _basePrice As Decimal
        Private _optionPrice As Decimal
        Private _pPN_BM As Decimal
        Private _pPN As Decimal
        Private _pPh22 As Decimal
        Private _interest As Decimal
        Private _factoringInt As Decimal
        Private _pPh23 As Decimal
        Private _status As String = String.Empty
        '' CR Sirkular Rewards
        '' by : ali Akbar
        '' 2014-09-24
        Private _discountReward As Decimal
        '' END OF CR Sirkular Rewards

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _vechileColor As VechileColor
        Private _dealer As Dealer

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

        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom() As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property

        <ColumnInfo("BasePrice", "{0}")> _
        Public Property BasePrice() As Decimal
            Get
                Return _basePrice
            End Get
            Set(ByVal value As Decimal)
                _basePrice = value
            End Set
        End Property

        <ColumnInfo("OptionPrice", "{0}")> _
        Public Property OptionPrice() As Decimal
            Get
                Return _optionPrice
            End Get
            Set(ByVal value As Decimal)
                _optionPrice = value
            End Set
        End Property

        <ColumnInfo("PPN_BM", "{0}")> _
        Public Property PPN_BM() As Decimal
            Get
                Return _pPN_BM
            End Get
            Set(ByVal value As Decimal)
                _pPN_BM = value
            End Set
        End Property

        <ColumnInfo("PPN", "{0}")> _
        Public Property PPN() As Decimal
            Get
                Return _pPN
            End Get
            Set(ByVal value As Decimal)
                _pPN = value
            End Set
        End Property

        <ColumnInfo("PPh22", "{0}")> _
        Public Property PPh22() As Decimal
            Get
                Return _pPh22
            End Get
            Set(ByVal value As Decimal)
                _pPh22 = value
            End Set
        End Property

        <ColumnInfo("Interest", "{0}")> _
        Public Property Interest() As Decimal
            Get
                Return _interest
            End Get
            Set(ByVal value As Decimal)
                _interest = value
            End Set
        End Property

        <ColumnInfo("FactoringInt", "{0}")> _
        Public Property FactoringInt() As Decimal
            Get
                Return _factoringInt
            End Get
            Set(ByVal value As Decimal)
                _factoringInt = value
            End Set
        End Property

        <ColumnInfo("PPh23", "{0}")> _
        Public Property PPh23() As Decimal
            Get
                Return _pPh23
            End Get
            Set(ByVal value As Decimal)
                _pPh23 = value
            End Set
        End Property

        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        '' CR Sirkular Rewards
        '' by : ali Akbar
        '' 2014-09-24
        <ColumnInfo("DiscountReward", "{0}")> _
        Public Property DiscountReward() As Decimal
            Get
                Return _discountReward
            End Get
            Set(ByVal value As Decimal)
                _discountReward = value
            End Set
        End Property
        '' END OF CR Sirkular Rewards

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

        <ColumnInfo("VechileColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "Price", "VechileColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vechileColor) And (Not Me._vechileColor.IsLoaded) Then

                        Me._vechileColor = CType(DoLoad(GetType(VechileColor).ToString(), _vechileColor.ID), VechileColor)
                        Me._vechileColor.MarkLoaded()

                    End If

                    Return Me._vechileColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vechileColor = value
                If (Not IsNothing(value)) And (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
                End If
            End Set
        End Property
        <ColumnInfo("DealerID", "{0}"), _
                RelationInfo("Dealer", "ID", "Price", "DealerID")> _
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

#End Region

#Region "Custom Properties"

        Public ReadOnly Property VehiclePrice() As Decimal
            Get
                Return _basePrice * (1 + PPN_BM / 100 + PPN / 100) + _optionPrice
            End Get
        End Property

        Public ReadOnly Property PPN_BMAmount() As Decimal
            Get
                Return _basePrice * PPN_BM / 100
            End Get
        End Property

        Public ReadOnly Property PPNAmount() As Decimal
            Get
                Return _basePrice * PPN / 100
            End Get
        End Property

        Public ReadOnly Property PPh22Amount() As Decimal
            Get
                Return _basePrice * PPh22 / 100
            End Get
        End Property

        Public ReadOnly Property InterestAmount() As Decimal
            Get
                Return _basePrice * Interest / 100
            End Get
        End Property

        Public ReadOnly Property PPh23Amount() As Decimal
            Get
                Return _basePrice * PPh23 / 100
            End Get
        End Property

        Public ReadOnly Property RowStatusX() As String
            Get
                Return IIf(_rowStatus = DBRowStatus.Deleted, "X", "")
            End Get
        End Property

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
