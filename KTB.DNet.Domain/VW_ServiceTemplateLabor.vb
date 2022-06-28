
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VW_ServiceTemplateLabor Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 11/15/2016 - 9:11:51 AM
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
    <Serializable(), TableInfo("VW_ServiceTemplateLabor")> _
    Public Class VW_ServiceTemplateLabor
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
        Private _templateType As String = String.Empty
        Private _vehicleTypeCode As String = String.Empty
        Private _kindCode As String = String.Empty
        Private _kindID As Integer
        Private _vehicleTypeID As Integer
        Private _laborDuration As Decimal
        Private _laborCost As Decimal
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

#End Region

#Region "Public Properties"

        <ColumnInfo("TemplateType", "{0}")> _
        Public Property TemplateType As String
            Get
                Return _templateType
            End Get
            Set(ByVal value As String)
                _templateType = value
            End Set
        End Property

        <ColumnInfo("VechileTypeCode", "{0}")> _
        Public Property VechileTypeCode As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property

        <ColumnInfo("LaborDuration", "{0}")> _
        Public Property LaborDuration As Decimal
            Get
                Return _laborDuration
            End Get
            Set(ByVal value As Decimal)
                _laborDuration = value
            End Set
        End Property

        <ColumnInfo("KindCode", "{0}")> _
        Public Property KindCode As String
            Get
                Return _kindCode
            End Get
            Set(ByVal value As String)
                _kindCode = value
            End Set
        End Property

        <ColumnInfo("LaborCost", "{0}")> _
        Public Property LaborCost As Decimal
            Get
                Return _laborCost
            End Get
            Set(ByVal value As Decimal)
                _laborCost = value
            End Set
        End Property

        <ColumnInfo("KindID", "{0}")> _
        Public Property KindID As Integer
            Get
                Return _kindID
            End Get
            Set(ByVal value As Integer)
                _kindID = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "VW_ServiceTemplateLabor", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        If Not IsNothing(Me._dealer) Then
                            Me._dealer.MarkLoaded()
                        End If

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

        <ColumnInfo("VechileTypeID", "{0}")> _
        Public Property VechileTypeID As Decimal
            Get
                Return _vehicleTypeID
            End Get
            Set(ByVal value As Decimal)
                _vehicleTypeID = value
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("ValidFrom", "'{0}'")> _
        Public Property ValidFrom As String
            Get
                Return _validFrom
            End Get
            Set(ByVal value As String)
                _validFrom = value
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

