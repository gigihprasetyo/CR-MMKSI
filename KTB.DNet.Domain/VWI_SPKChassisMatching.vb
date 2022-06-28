
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_SPKChassisMatching Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 17/07/2018 - 8:52:00
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
    <Serializable(), TableInfo("VWI_SPKChassisMatching")> _
    Public Class VWI_SPKChassisMatching
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
        Private _dealerCode As String = String.Empty
        Private _matchingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerCode As String = String.Empty
        Private _name As String = String.Empty
        Private _sPKNumber As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _engineNumber As String = String.Empty
        Private _keyNumber As String = String.Empty
        Private _vehicleTypeCode As String = String.Empty
        Private _colorCode As String = String.Empty
        Private _description As String = String.Empty
        Private _matchingNumber As String = String.Empty
        Private _referenceNumber As String = String.Empty
        Private _matchingType As Integer

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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("MatchingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property MatchingDate As DateTime
            Get
                Return _matchingDate
            End Get
            Set(ByVal value As DateTime)
                _matchingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerCode", "'{0}'")> _
        Public Property CustomerCode As String
            Get
                Return _customerCode
            End Get
            Set(ByVal value As String)
                _customerCode = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("EngineNumber", "'{0}'")> _
        Public Property EngineNumber As String
            Get
                Return _engineNumber
            End Get
            Set(ByVal value As String)
                _engineNumber = value
            End Set
        End Property


        <ColumnInfo("KeyNumber", "'{0}'")> _
        Public Property KeyNumber As String
            Get
                Return _keyNumber
            End Get
            Set(ByVal value As String)
                _keyNumber = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property


        <ColumnInfo("ColorCode", "'{0}'")> _
        Public Property ColorCode As String
            Get
                Return _colorCode
            End Get
            Set(ByVal value As String)
                _colorCode = value
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


        <ColumnInfo("MatchingNumber", "'{0}'")> _
        Public Property MatchingNumber As String
            Get
                Return _matchingNumber
            End Get
            Set(ByVal value As String)
                _matchingNumber = value
            End Set
        End Property


        <ColumnInfo("ReferenceNumber", "'{0}'")> _
        Public Property ReferenceNumber As String
            Get
                Return _referenceNumber
            End Get
            Set(ByVal value As String)
                _referenceNumber = value
            End Set
        End Property

        <ColumnInfo("MatchingType", "{0}")> _
        Public Property MatchingType As Integer
            Get
                Return _matchingType
            End Get
            Set(ByVal value As Integer)
                _matchingType = value
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

