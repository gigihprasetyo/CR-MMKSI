
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitSpecialCity Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/23/2019 - 2:03:29 PM
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
    <Serializable(), TableInfo("BabitSpecialCity")> _
    Public Class BabitSpecialCity
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
        'private _cityID as integer 		
        'private _babitSpecialProvinceid as integer 		
        Private _status As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _city As City
        Private _babitSpecialProvince As BabitSpecialProvince

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


        '<ColumnInfo("CityID", "{0}")> _
        'Public Property CityID As Integer
        '    Get
        '        Return _cityID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _cityID = value
        '    End Set
        'End Property


        '<ColumnInfo("BabitSpecialProvinceid", "{0}")> _
        'Public Property BabitSpecialProvinceid As Integer
        '    Get
        '        Return _babitSpecialProvinceid
        '    End Get
        '    Set(ByVal value As Integer)
        '        _babitSpecialProvinceid = value
        '    End Set
        'End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
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
        RelationInfo("City", "ID", "BabitSpecialCity", "CityID")> _
        Public Property City As City
            Get
                Try
                    If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("BabitSpecialProvinceID", "{0}"), _
        RelationInfo("BabitSpecialProvince", "ID", "BabitSpecialCity", "BabitSpecialProvinceID")> _
        Public Property BabitSpecialProvince As BabitSpecialProvince
            Get
                Try
                    If Not IsNothing(Me._babitSpecialProvince) AndAlso (Not Me._babitSpecialProvince.IsLoaded) Then

                        Me._babitSpecialProvince = CType(DoLoad(GetType(BabitSpecialProvince).ToString(), _babitSpecialProvince.ID), BabitSpecialProvince)
                        Me._babitSpecialProvince.MarkLoaded()
                    End If
                    Return Me._babitSpecialProvince
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BabitSpecialProvince)
                Me._babitSpecialProvince = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitSpecialProvince.MarkLoaded()
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

