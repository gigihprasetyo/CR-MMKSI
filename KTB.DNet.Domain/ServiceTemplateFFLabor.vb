#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceTemplateFFLabor Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:01:07 PM
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
    <Serializable(), TableInfo("ServiceTemplateFFLabor")> _
    Public Class ServiceTemplateFFLabor
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
        Private _material As String = String.Empty
        'Private _laborDuration As Decimal
        Private _laborCost As Decimal
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        'Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        'Private _recallCategory As RecallCategory
        'Private _vechileType As VechileType


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

        '<ColumnInfo("LaborDuration", "{0}")> _
        'Public Property LaborDuration() As Decimal
        '    Get
        '        Return _laborDuration
        '    End Get
        '    Set(ByVal value As Decimal)
        '        _laborDuration = value
        '    End Set
        'End Property

        <ColumnInfo("Material", "'{0}'")> _
        Public Property Material() As String
            Get
                Return _material
            End Get
            Set(ByVal value As String)
                _material = value
            End Set
        End Property

        <ColumnInfo("LaborCost", "{0}")> _
        Public Property LaborCost() As Decimal
            Get
                Return _laborCost
            End Get
            Set(ByVal value As Decimal)
                _laborCost = value
            End Set
        End Property

        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd}'")> _
        Public Property ValidFrom() As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property

        '<ColumnInfo("ValidTo", "'{0:yyyy/MM/dd}'")> _
        'Public Property ValidTo() As DateTime
        '    Get
        '        Return _validTo
        '    End Get
        '    Set(ByVal value As DateTime)
        '        _validTo = value
        '    End Set
        'End Property

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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy() As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime() As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ServiceTemplateFFLabor", "DealerID")> _
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

        '<ColumnInfo("RecallCategoryID", "{0}"), _
        'RelationInfo("RecallCategory", "ID", "ServiceTemplateFFLabor", "RecallCategoryID")> _
        'Public Property RecallCategory() As RecallCategory
        '    Get
        '        Try
        '            If Not IsNothing(Me._recallCategory) AndAlso (Not Me._recallCategory.IsLoaded) Then

        '                Me._recallCategory = CType(DoLoad(GetType(RecallCategory).ToString(), _recallCategory.ID), RecallCategory)
        '                If Not IsNothing(Me._recallCategory) Then
        '                    Me._recallCategory.MarkLoaded()
        '                End If

        '            End If

        '            Return Me._recallCategory

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As RecallCategory)

        '        Me._recallCategory = value
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._recallCategory.MarkLoaded()
        '        End If
        '    End Set
        'End Property

        '<ColumnInfo("VechileTypeID", "{0}"), _
        'RelationInfo("VechileType", "ID", "ServiceTemplateFFLabor", "VechileTypeID")> _
        'Public Property VechileType() As VechileType
        '    Get
        '        Try
        '            If Not IsNothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

        '                Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
        '                If Not IsNothing(Me._vechileType) Then
        '                    Me._vechileType.MarkLoaded()
        '                End If

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
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
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

#Region "Non_generated Properties"
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

