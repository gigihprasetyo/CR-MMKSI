#Region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : SAPCustomerProfile Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2021 - 1:36:10 PM
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
    <Serializable(), TableInfo("SAPCustomerProfile")> _
    Public Class SAPCustomerProfile
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
        Private _profileID As Integer
        Private _leadStatus As Byte
        Private _value As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _SAPCustomer As SAPCustomer
        Private _profileGroup As ProfileGroup
        Private _profileHeader As ProfileHeader



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


        <ColumnInfo("ProfileID", "{0}")> _
        Public Property ProfileID As Integer
            Get
                Return _profileID
            End Get
            Set(ByVal value As Integer)
                _profileID = value
            End Set
        End Property


        <ColumnInfo("LeadStatus", "{0}")> _
        Public Property LeadStatus As Byte
            Get
                Return _leadStatus
            End Get
            Set(ByVal value As Byte)
                _leadStatus = value
            End Set
        End Property


        <ColumnInfo("Value", "'{0}'")> _
        Public Property Value As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
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


        <ColumnInfo("SAPCustomerID", "{0}"), _
        RelationInfo("SAPCustomer", "ID", "SAPCustomerProfile", "SAPCustomerID")> _
        Public Property SAPCustomer() As SAPCustomer
            Get
                Try
                    If Not IsNothing(Me._SAPCustomer) AndAlso (Not Me._SAPCustomer.IsLoaded) Then

                        Me._SAPCustomer = CType(DoLoad(GetType(SAPCustomer).ToString(), _SAPCustomer.ID), SAPCustomer)
                        Me._SAPCustomer.MarkLoaded()

                    End If

                    Return Me._SAPCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SAPCustomer)

                Me._SAPCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._SAPCustomer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("GroupID", "{0}"), _
        RelationInfo("ProfileGroup", "ID", "SAPCustomerProfile", "GroupID")> _
        Public Property ProfileGroup As ProfileGroup
            Get
                Try
                    If Not isnothing(Me._profileGroup) AndAlso (Not Me._profileGroup.IsLoaded) Then

                        Me._profileGroup = CType(DoLoad(GetType(ProfileGroup).ToString(), _profileGroup.ID), ProfileGroup)
                        Me._profileGroup.MarkLoaded()

                    End If

                    Return Me._profileGroup

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProfileGroup)

                Me._profileGroup = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._profileGroup.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ProfileHeaderID", "{0}"), _
        RelationInfo("ProfileHeader", "ID", "SAPCustomerProfile", "ProfileHeaderID")> _
        Public Property ProfileHeader As ProfileHeader
            Get
                Try
                    If Not isnothing(Me._profileHeader) AndAlso (Not Me._profileHeader.IsLoaded) Then

                        Me._profileHeader = CType(DoLoad(GetType(ProfileHeader).ToString(), _profileHeader.ID), ProfileHeader)
                        Me._profileHeader.MarkLoaded()

                    End If

                    Return Me._profileHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProfileHeader)

                Me._profileHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._profileHeader.MarkLoaded()
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
