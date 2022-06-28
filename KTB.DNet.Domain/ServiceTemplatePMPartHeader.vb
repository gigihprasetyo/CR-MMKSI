#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceTemplatePMPartHeader Domain Object.
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
    <Serializable(), TableInfo("ServiceTemplatePMPartHeader")> _
    Public Class ServiceTemplatePMPartHeader
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
        Private _varian As String = String.Empty
        'Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pmKind As PMKind
        'Private _vechileType As VechileType

        Private _serviceTemplatePMPartDetails As New ArrayList

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

        <ColumnInfo("Varian", "'{0}'")> _
        Public Property Varian() As String
            Get
                Return _varian
            End Get
            Set(ByVal value As String)
                _varian = value
            End Set
        End Property

        '<ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd}'")> _
        'Public Property ValidFrom() As DateTime
        '    Get
        '        Return _validFrom
        '    End Get
        '    Set(ByVal value As DateTime)
        '        _validFrom = value
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

        <ColumnInfo("PMKindID", "{0}"), _
        RelationInfo("PMKind", "ID", "ServiceTemplatePMPartHeader", "PMKindID")> _
        Public Property PMKind() As PMKind
            Get
                Try
                    If Not IsNothing(Me._pmKind) AndAlso (Not Me._pmKind.IsLoaded) Then

                        Me._pmKind = CType(DoLoad(GetType(PMKind).ToString(), _pmKind.ID), PMKind)
                        If Not IsNothing(Me._pmKind) Then
                            Me._pmKind.MarkLoaded()
                        End If
                    End If

                    Return Me._pmKind

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PMKind)

                Me._pmKind = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pmKind.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("VechileTypeID", "{0}"), _
        'RelationInfo("VechileType", "ID", "ServiceTemplatePMPartHeader", "VechileTypeID")> _
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
        <RelationInfo("ServiceTemplatePMPartHeader", "ID", "ServiceTemplatePMPartDetail", "ServiceTemplatePMPartHeaderID")> _
        Public ReadOnly Property ServiceTemplatePMPartDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._serviceTemplatePMPartDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ServiceTemplatePMPartDetail), "ServiceTemplatePMPartHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ServiceTemplatePMPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Me._serviceTemplatePMPartDetails = DoLoadArray(GetType(ServiceTemplatePMPartDetail).ToString, criterias)
                    End If
                    Return Me._serviceTemplatePMPartDetails
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property
#End Region

    End Class
End Namespace

