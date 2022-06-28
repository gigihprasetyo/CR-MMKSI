
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaxTOPDay Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2012 - 10:56:43 AM
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
    <Serializable(), TableInfo("MaxTOPDay")> _
    Public Class MaxTOPDay
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
        Private _dealerID As Integer
        Private _vechileTypeID As Integer
        Private _normal As Integer
        Private _factoring As Integer
        Private _isCOD As Integer
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        'Private _vechileType As VechileType
        'Private _dealer As Dealer


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


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("VechileTypeID", "{0}")> _
        Public Property VechileTypeID() As Integer
            Get
                Return _vechileTypeID
            End Get
            Set(ByVal value As Integer)
                _vechileTypeID = value
            End Set
        End Property


        <ColumnInfo("Normal", "{0}")> _
        Public Property Normal() As Integer
            Get
                Return _normal
            End Get
            Set(ByVal value As Integer)
                _normal = value
            End Set
        End Property


        <ColumnInfo("Factoring", "{0}")> _
        Public Property Factoring() As Integer
            Get
                Return _factoring
            End Get
            Set(ByVal value As Integer)
                _factoring = value
            End Set
        End Property


        <ColumnInfo("IsCOD", "{0}")> _
        Public Property IsCOD() As Integer
            Get
                Return _isCOD
            End Get
            Set(ByVal value As Integer)
                _isCOD = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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


        '<ColumnInfo("DealerID", "{0}"), _
        'RelationInfo("Dealer", "ID", "AccessoriesSale", "DealerID")> _
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

        '<ColumnInfo("VechileTypeID", "{0}"), _
        'RelationInfo("VechileType", "ID", "LaborMaster", "VechileTypeID")> _
        'Public Property VechileType() As VechileType
        '    Get
        '        Try
        '            If Not isnothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

        '                Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
        '                Me._vechileType.MarkLoaded()

        '            End If

        '            Return Me._vechileType

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As VechileType)

        '        Me._vechileType = value
        '        If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._vechileType.MarkLoaded()
        '        End If
        '    End Set
        'End Property

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

