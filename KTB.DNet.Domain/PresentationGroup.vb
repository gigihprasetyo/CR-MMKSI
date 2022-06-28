
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PresentationGroup Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 25/02/2016 - 13:23:14
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
    <Serializable(), TableInfo("PresentationGroup")> _
    Public Class PresentationGroup
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
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _presentationID As Integer
        'Private _userGroupID As Short

        Private _presentation As Presentation
        Private _userGroup As UserGroup



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


        '    <ColumnInfo("PresentationID", "{0}")> _
        '    Public Property PresentationID As Integer

        '        Get
        'return _presentationID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _presentationID = value
        '        End Set
        '    End Property
        '    <ColumnInfo("UserGroupID", "{0}")> _
        '    Public Property UserGroupID As Short

        '        Get
        'return _userGroupID}
        '        End Get
        '        Set(ByVal value As Short)
        '            _userGroupID = value
        '        End Set
        '    End Property



        <ColumnInfo("PresentationID", "{0}"), _
        RelationInfo("Presentation", "ID", "PresentationGroup", "PresentationID")> _
        Public Property Presentation() As Presentation
            Get
                Try
                    If Not IsNothing(Me._presentation) AndAlso (Not Me._presentation.IsLoaded) Then

                        Me._presentation = CType(DoLoad(GetType(Presentation).ToString(), _presentation.ID), Presentation)
                        Me._presentation.MarkLoaded()

                    End If

                    Return Me._presentation

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Presentation)

                Me._presentation = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._presentation.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("UserGroupID", "{0}"), _
        RelationInfo("UserGroup", "ID", "PresentationGroup", "UserGroupID")> _
        Public Property UserGroup() As UserGroup
            Get
                Try
                    If Not IsNothing(Me._userGroup) AndAlso (Not Me._userGroup.IsLoaded) Then

                        Me._userGroup = CType(DoLoad(GetType(UserGroup).ToString(), _userGroup.ID), UserGroup)
                        Me._userGroup.MarkLoaded()

                    End If

                    Return Me._userGroup

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As UserGroup)

                Me._userGroup = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._userGroup.MarkLoaded()
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

