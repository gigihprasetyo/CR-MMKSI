
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanAdditionalInfo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/6/2011 - 9:43:00 AM
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
    <Serializable(), TableInfo("SalesmanAdditionalInfo")> _
    Public Class SalesmanAdditionalInfo
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            _iD = 0
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        'Private _salesmanHeaderID As Integer
        Private _religionID As String = String.Empty
        'private _salesmanCategoryLevelID as integer 		
        Private _salesmanLevel As Integer
        Private _salary As Decimal
        Private _ktpImagePath As String = String.Empty
        Private _CSOHireDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _appointmentLetterPath As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesmanHeaderID As SalesmanHeader
        Private _salesmanHeaderID_Ref As SalesmanHeader
        Private _salesmanCategoryLevelID As SalesmanCategoryLevel
        Private _birthCity As City
        Private _addressCity As City


        'Private _salesmanLevel As SalesmanLevel

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


        '<ColumnInfo("SalesmanHeaderID", "{0}")> _
        'Public Property SalesmanHeaderID() As Integer
        '    Get
        '        Return _salesmanHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _salesmanHeaderID = value
        '    End Set
        'End Property


        <ColumnInfo("BirthCityID", "{0}"), _
       RelationInfo("City", "ID", "SalesmanAdditionalInfo", "BirthCityID")> _
        Public Property BirthCity() As City
            Get
                Try
                    If Not IsNothing(Me._birthCity) AndAlso (Not Me._birthCity.IsLoaded) Then

                        Me._birthCity = CType(DoLoad(GetType(City).ToString(), _birthCity.ID), City)
                        Me._birthCity.MarkLoaded()

                    End If

                    Return Me._birthCity

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._birthCity = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._birthCity.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("AddressCityID", "{0}"), _
       RelationInfo("City", "ID", "SalesmanAdditionalInfo", "AddressCityID")> _
        Public Property AddressCity() As City
            Get
                Try
                    If Not IsNothing(Me._addressCity) AndAlso (Not Me._addressCity.IsLoaded) Then

                        Me._addressCity = CType(DoLoad(GetType(City).ToString(), _addressCity.ID), City)
                        Me._addressCity.MarkLoaded()

                    End If

                    Return Me._addressCity

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._addressCity = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._addressCity.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "SalesmanAdditionalInfo", "SalesmanHeaderID")> _
        Public Property SalesmanHeader() As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeaderID) AndAlso (Not Me._salesmanHeaderID.IsLoaded) Then

                        Me._salesmanHeaderID = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeaderID.ID), SalesmanHeader)
                        Me._salesmanHeaderID.MarkLoaded()

                    End If

                    Return Me._salesmanHeaderID

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeaderID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeaderID.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanHeaderID_Ref", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "SalesmanAdditionalInfo", "SalesmanHeaderID_Ref")> _
        Public Property SalesmanHeader_Ref() As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeaderID_Ref) AndAlso (Not Me._salesmanHeaderID_Ref.IsLoaded) Then

                        Me._salesmanHeaderID_Ref = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeaderID_Ref.ID), SalesmanHeader)
                        Me._salesmanHeaderID_Ref.MarkLoaded()

                    End If

                    Return Me._salesmanHeaderID_Ref

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeaderID_Ref = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeaderID_Ref.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("ReligionID", "'{0}'")> _
        Public Property ReligionID() As String
            Get
                Return _religionID
            End Get
            Set(ByVal value As String)
                _religionID = value
            End Set
        End Property


        '<ColumnInfo("SalesmanCategoryLevelID", "{0}")> _
        'Public Property SalesmanCategoryLevelID() As Integer
        '    Get
        '        Return _salesmanCategoryLevelID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _salesmanCategoryLevelID = value
        '    End Set
        'End Property

        <ColumnInfo("SalesmanCategoryLevelID", "{0}"), _
        RelationInfo("SalesmanCategoryLevel", "ID", "SalesmanAdditionalInfo", "SalesmanCategoryLevelID")> _
        Public Property SalesmanCategoryLevel() As SalesmanCategoryLevel
            Get
                Try
                    If Not IsNothing(Me._salesmanCategoryLevelID) AndAlso (Not Me._salesmanCategoryLevelID.IsLoaded) Then

                        Me._salesmanCategoryLevelID = CType(DoLoad(GetType(SalesmanCategoryLevel).ToString(), _salesmanCategoryLevelID.ID), SalesmanCategoryLevel)
                        Me._salesmanCategoryLevelID.MarkLoaded()

                    End If

                    Return Me._salesmanCategoryLevelID

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanCategoryLevel)

                Me._salesmanCategoryLevelID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanCategoryLevelID.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SalesmanLevel", "{0}")> _
        Public Property SalesmanLevel() As Integer
            Get
                Return _salesmanLevel
            End Get
            Set(ByVal value As Integer)
                _salesmanLevel = value
            End Set
        End Property


        '<ColumnInfo("SalesmanLevel", "{0}"), _
        'RelationInfo("SalesmanLevel", "ID", "SalesmanAdditionalInfo", "SalesmanLevel")> _
        'Public Property SalesmanLevel() As SalesmanLevel
        '    Get
        '        Try
        '            If Not IsNothing(Me._salesmanLevel) AndAlso (Not Me._salesmanLevel.IsLoaded) Then

        '                Me._salesmanLevel = CType(DoLoad(GetType(SalesmanLevel).ToString(), _salesmanLevel.ID), SalesmanLevel)
        '                Me._salesmanLevel.MarkLoaded()

        '            End If

        '            Return Me._salesmanLevel

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As SalesmanLevel)

        '        Me._salesmanLevel = value
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._salesmanLevel.MarkLoaded()
        '        End If
        '    End Set
        'End Property


        <ColumnInfo("Salary", "{0}")> _
        Public Property Salary() As Decimal
            Get
                Return _salary
            End Get
            Set(ByVal value As Decimal)
                _salary = value
            End Set
        End Property

        <ColumnInfo("KtpImagePath", "'{0}'")> _
        Public Property KtpImagePath() As String
            Get
                Return _ktpImagePath
            End Get
            Set(ByVal value As String)
                _ktpImagePath = value
            End Set
        End Property

        <ColumnInfo("CSOHireDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CSOHireDate() As DateTime
            Get
                Return _CSOHireDate
            End Get
            Set(ByVal value As DateTime)
                _CSOHireDate = value
            End Set
        End Property

        <ColumnInfo("AppointmentLetterPath", "'{0}'")> _
        Public Property AppointmentLetterPath() As String
            Get
                Return _appointmentLetterPath
            End Get
            Set(ByVal value As String)
                _appointmentLetterPath = value
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

