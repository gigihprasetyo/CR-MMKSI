#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("V_MDPMasterVehicle")> _
    Public Class V_MDPMasterVehicle
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
        Private _status As String = String.Empty
        Private _rowStatus As Short
        Private _categoryCode As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _vechileTypeCode As String = String.Empty
        Private _colorCode As String = String.Empty
        Private _categoryID As String = String.Empty

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


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CategoryCode", "{0}")> _
        Public Property CategoryCode() As Integer
            Get
                Return _categoryCode
            End Get
            Set(ByVal value As Integer)
                _categoryCode = value
            End Set
        End Property


        <ColumnInfo("MaterialDescription", "'{0}'")> _
        Public Property MaterialDescription() As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property


        <ColumnInfo("VechileTypeCode", "'{0}'")> _
        Public Property VechileTypeCode() As String
            Get
                Return _vechileTypeCode
            End Get
            Set(ByVal value As String)
                _vechileTypeCode = value
            End Set
        End Property


        <ColumnInfo("ColorCode", "{0}")> _
        Public Property ColorCode() As Byte
            Get
                Return _colorCode
            End Get
            Set(ByVal value As Byte)
                _colorCode = value
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}")> _
        Public Property CategoryID() As Byte
            Get
                Return _categoryID
            End Get
            Set(ByVal value As Byte)
                _categoryID = value
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
        'Private _dealer As Dealer

        '<ColumnInfo("DealerID", "{0}"), _
        'RelationInfo("Dealer", "ID", "POHeader", "DealerID")> _
        'Public Property Dealer() As Dealer
        '    Get
        '        Try
        '            If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

        '                Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
        '                Me._dealer.MarkLoaded()

        '            End If

        '            Return Me._dealer

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As Dealer)

        '        Me._dealer = value
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._dealer.MarkLoaded()
        '        End If
        '    End Set
        'End Property
#End Region

    End Class
End Namespace

