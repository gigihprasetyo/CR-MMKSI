#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrBookingClass Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/5/2019 - 11:05:27 AM
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
    <Serializable(), TableInfo("TrBookingClass")> _
    Public Class TrBookingClass
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
        Private _trBookingCourse As TrBookingCourse
        Private _trClass As TrClass
        Private _trClassRegistration As TrClassRegistration
        Private _status As Short
        Private _reason As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("TrBookingCourseID", "{0}")> _
        <RelationInfo("TrBookingCourse", "ID", "TrBookingClass", "TrBookingCourseID")> _
        Public Property TrBookingCourse As TrBookingCourse
            Get
                Try
                    If Not IsNothing(Me._trBookingCourse) AndAlso (Not Me._trBookingCourse.IsLoaded) Then

                        Me._trBookingCourse = CType(DoLoad(GetType(TrBookingCourse).ToString(), _trBookingCourse.ID), TrBookingCourse)
                        Me._trBookingCourse.MarkLoaded()

                    End If

                    Return Me._trBookingCourse

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrBookingCourse)

                Me._trBookingCourse = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trBookingCourse.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("TrClassID", "{0}")> _
        <RelationInfo("TrClass", "ID", "TrBookingClass", "TrClassID")> _
        Public Property TrClass As TrClass
            Get
                Try
                    If Not IsNothing(Me._trClass) AndAlso (Not Me._trClass.IsLoaded) Then

                        Me._trClass = CType(DoLoad(GetType(TrClass).ToString(), _trClass.ID), TrClass)
                        Me._trClass.MarkLoaded()

                    End If

                    Return Me._trClass

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrClass)

                Me._trClass = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trClass.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("TrClassRegistrationID", "{0}")> _
        <RelationInfo("TrClassRegistration", "ID", "TrBookingClass", "TrClassRegistrationID")> _
        Public Property TrClassRegistration As TrClassRegistration
            Get
                Try
                    If Not IsNothing(Me._trClassRegistration) AndAlso (Not Me._trClassRegistration.IsLoaded) Then

                        Me._trClassRegistration = CType(DoLoad(GetType(TrClassRegistration).ToString(), _trClassRegistration.ID), TrClassRegistration)
                        Me._trClassRegistration.MarkLoaded()

                    End If

                    Return Me._trClassRegistration

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrClassRegistration)

                Me._trClassRegistration = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trClassRegistration.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("Reason", "'{0}'")> _
        Public Property Reason As String
            Get
                Return _reason
            End Get
            Set(ByVal value As String)
                _reason = value
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
