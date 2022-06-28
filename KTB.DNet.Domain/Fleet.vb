
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Fleet Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/02/2018 - 15:25:23
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
    <Serializable(), TableInfo("Fleet")> _
    Public Class Fleet
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
        Private _fleetCode As String = String.Empty
        Private _fleetName As String = String.Empty
        Private _fleetNickName As String = String.Empty
        Private _fleetGroup As String = String.Empty
        Private _address As String = String.Empty
        Private _identityType As Integer
        Private _identityNumber As String = String.Empty
        Private _fleetNote As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _city As City
        Private _province As Province
        Private _businessSectorHeader As BusinessSectorHeader
        Private _businessSectorDetail As BusinessSectorDetail



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


        <ColumnInfo("FleetCode", "'{0}'")> _
        Public Property FleetCode As String
            Get
                Return _fleetCode
            End Get
            Set(ByVal value As String)
                _fleetCode = value
            End Set
        End Property


        <ColumnInfo("FleetName", "'{0}'")> _
        Public Property FleetName As String
            Get
                Return _fleetName
            End Get
            Set(ByVal value As String)
                _fleetName = value
            End Set
        End Property


        <ColumnInfo("FleetNickName", "'{0}'")> _
        Public Property FleetNickName As String
            Get
                Return _fleetNickName
            End Get
            Set(ByVal value As String)
                _fleetNickName = value
            End Set
        End Property


        <ColumnInfo("FleetGroup", "'{0}'")> _
        Public Property FleetGroup As String
            Get
                Return _fleetGroup
            End Get
            Set(ByVal value As String)
                _fleetGroup = value
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


        <ColumnInfo("IdentityType", "{0}")> _
        Public Property IdentityType As Integer
            Get
                Return _identityType
            End Get
            Set(ByVal value As Integer)
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


        <ColumnInfo("FleetNote", "'{0}'")> _
        Public Property FleetNote As String
            Get
                Return _fleetNote
            End Get
            Set(ByVal value As String)
                _fleetNote = value
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


        <ColumnInfo("CityId", "{0}"), _
        RelationInfo("City", "ID", "Fleet", "CityId")> _
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

        <ColumnInfo("ProvinceId", "{0}"), _
        RelationInfo("Province", "ID", "Fleet", "ProvinceId")> _
        Public Property Province As Province
            Get
                Try
                    If Not IsNothing(Me._province) AndAlso (Not Me._province.IsLoaded) Then

                        Me._province = CType(DoLoad(GetType(Province).ToString(), _province.ID), Province)
                        Me._province.MarkLoaded()

                    End If

                    Return Me._province

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Province)

                Me._province = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._province.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BusinessSectorHeaderId", "{0}"), _
        RelationInfo("BusinessSectorHeader", "ID", "Fleet", "BusinessSectorHeaderId")> _
        Public Property BusinessSectorHeader As BusinessSectorHeader
            Get
                Try
                    If Not IsNothing(Me._businessSectorHeader) AndAlso (Not Me._businessSectorHeader.IsLoaded) Then

                        Me._businessSectorHeader = CType(DoLoad(GetType(BusinessSectorHeader).ToString(), _businessSectorHeader.ID), BusinessSectorHeader)
                        Me._businessSectorHeader.MarkLoaded()

                    End If

                    Return Me._businessSectorHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BusinessSectorHeader)

                Me._businessSectorHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._businessSectorHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BusinessSectorDetailId", "{0}"), _
        RelationInfo("BusinessSectorDetail", "ID", "Fleet", "BusinessSectorDetailId")> _
        Public Property BusinessSectorDetail As BusinessSectorDetail
            Get
                Try
                    If Not IsNothing(Me._businessSectorDetail) AndAlso (Not Me._businessSectorDetail.IsLoaded) Then

                        Me._businessSectorDetail = CType(DoLoad(GetType(BusinessSectorDetail).ToString(), _businessSectorDetail.ID), BusinessSectorDetail)
                        Me._businessSectorDetail.MarkLoaded()

                    End If

                    Return Me._businessSectorDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BusinessSectorDetail)

                Me._businessSectorDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._businessSectorDetail.MarkLoaded()
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

