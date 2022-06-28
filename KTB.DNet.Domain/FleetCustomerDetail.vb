#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetCustomerDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/30/2020 - 12:37:37 PM
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
    <Serializable(), TableInfo("FleetCustomerDetail")> _
    Public Class FleetCustomerDetail
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
        Private _fleetStatus As Short
        Private _fleetDetailCode As String = String.Empty
        Private _address As String = String.Empty
        Private _subDistrict As String = String.Empty
        Private _village As String = String.Empty
        Private _postalCode As String = String.Empty
        Private _identityType As Short
        Private _identityNumber As String = String.Empty
        Private _nPWPNo As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _city As City
        Private _dealer As Dealer
        Private _fleetCustomerHeader As FleetCustomerHeader



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


        <ColumnInfo("FleetDetailCode", "'{0}'")> _
        Public Property FleetDetailCode As String
            Get
                Return _fleetDetailCode
            End Get
            Set(ByVal value As String)
                _fleetDetailCode = value
            End Set
        End Property


        <ColumnInfo("FleetStatus", "{0}")> _
        Public Property FleetStatus As Short
            Get
                Return _fleetStatus
            End Get
            Set(ByVal value As Short)
                _fleetStatus = value
            End Set
        End Property


        <ColumnInfo("Address", "'{0}'")> _
        Public Property Address As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property


        <ColumnInfo("SubDistrict", "'{0}'")> _
        Public Property SubDistrict As String
            Get
                Return _subDistrict
            End Get
            Set(ByVal value As String)
                _subDistrict = value
            End Set
        End Property


        <ColumnInfo("Village", "'{0}'")> _
        Public Property Village As String
            Get
                Return _village
            End Get
            Set(ByVal value As String)
                _village = value
            End Set
        End Property


        <ColumnInfo("PostalCode", "'{0}'")> _
        Public Property PostalCode As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
            End Set
        End Property


        <ColumnInfo("IdentityType", "{0}")> _
        Public Property IdentityType As Short
            Get
                Return _identityType
            End Get
            Set(ByVal value As Short)
                _identityType = value
            End Set
        End Property


        <ColumnInfo("IdentityNumber", "'{0}'")> _
        Public Property IdentityNumber As String
            Get
                Return _identityNumber
            End Get
            Set(ByVal value As String)
                _identityNumber = value
            End Set
        End Property


        <ColumnInfo("NPWPNo", "'{0}'")> _
        Public Property NPWPNo As String
            Get
                Return _nPWPNo
            End Get
            Set(ByVal value As String)
                _nPWPNo = value
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


        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "FleetCustomerDetail", "CityID")> _
        Public Property City As City
            Get
                Try
                    If Not isnothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

                        Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
                        Me._city.MarkLoaded()

                    End If

                    Return Me._city

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._city = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "FleetCustomerDetail", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("FleetCustomerHeaderID", "{0}"), _
        RelationInfo("FleetCustomerHeader", "ID", "FleetCustomerDetail", "FleetCustomerHeaderID")> _
        Public Property FleetCustomerHeader As FleetCustomerHeader
            Get
                Try
                    If Not isnothing(Me._fleetCustomerHeader) AndAlso (Not Me._fleetCustomerHeader.IsLoaded) Then

                        Me._fleetCustomerHeader = CType(DoLoad(GetType(FleetCustomerHeader).ToString(), _fleetCustomerHeader.ID), FleetCustomerHeader)
                        Me._fleetCustomerHeader.MarkLoaded()

                    End If

                    Return Me._fleetCustomerHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As FleetCustomerHeader)

                Me._fleetCustomerHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._fleetCustomerHeader.MarkLoaded()
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
