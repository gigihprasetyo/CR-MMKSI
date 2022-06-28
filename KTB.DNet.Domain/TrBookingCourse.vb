#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrBookingCourse Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/5/2019 - 11:03:47 AM
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
    <Serializable(), TableInfo("TrBookingCourse")> _
    Public Class TrBookingCourse
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
        Private _trTrainee As TrTrainee
        Private _dealer As Dealer
        Private _trCourse As TrCourse
        Private _fiscalYear As String = String.Empty
        Private _registrationDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _trClassRegistration As TrClassRegistration
        Private _trClassRegistrationID As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _prioritySequence As Short
        Private _trBillingDetail As TrBillingDetail


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

        <ColumnInfo("TrClassRegistrationID", "{0}")> _
        Public Property TrClassRegistrationID As Integer
            Get
                Return _trClassRegistrationID
            End Get
            Set(ByVal value As Integer)
                _trClassRegistrationID = value
            End Set
        End Property


        <ColumnInfo("TraineeID", "{0}")> _
        <RelationInfo("TrTrainee", "ID", "TrBookingCourse", "TraineeID")> _
        Public Property TrTrainee As TrTrainee
            Get
                Try
                    If Not IsNothing(Me._trTrainee) AndAlso (Not Me._trTrainee.IsLoaded) Then

                        Me._trTrainee = CType(DoLoad(GetType(TrTrainee).ToString(), _trTrainee.ID), TrTrainee)
                        Me._trTrainee.MarkLoaded()

                    End If

                    Return Me._trTrainee

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTrainee)

                Me._trTrainee = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTrainee.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        <RelationInfo("TrBillingDetail", "TrBookingCourseID", "TrBookingCourse", "ID")> _
        Public Property TrBillingDetail As TrBillingDetail
            Get
                Try
                    If Not IsNothing(Me._trBillingDetail) AndAlso (Not Me._trBillingDetail.IsLoaded) Then

                        Me._trBillingDetail = CType(DoLoad(GetType(TrTrainee).ToString(), _trBillingDetail.ID), TrBillingDetail)
                        Me._trBillingDetail.MarkLoaded()

                    End If

                    Return Me._trBillingDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrBillingDetail)

                Me._trBillingDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trBillingDetail.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        <RelationInfo("Dealer", "ID", "TrBookingCourse", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

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


        <ColumnInfo("TrCourseID", "{0}")> _
        <RelationInfo("TrCourse", "ID", "TrBookingCourse", "TrCourseID")> _
        Public Property TrCourse As TrCourse
            Get
                Try
                    If Not IsNothing(Me._trCourse) AndAlso (Not Me._trCourse.IsLoaded) Then

                        Me._trCourse = CType(DoLoad(GetType(TrCourse).ToString(), _trCourse.ID), TrCourse)
                        Me._trCourse.MarkLoaded()

                    End If

                    Return Me._trCourse

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrCourse)
                Me._trCourse = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trCourse.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("FiscalYear", "'{0}'")> _
        Public Property FiscalYear As String
            Get
                Return _fiscalYear
            End Get
            Set(ByVal value As String)
                _fiscalYear = value
            End Set
        End Property

        <ColumnInfo("PrioritySequence", "'{0}'")> _
        Public Property PrioritySequence As Short
            Get
                Return _prioritySequence
            End Get
            Set(ByVal value As Short)
                _prioritySequence = value
            End Set
        End Property


        <ColumnInfo("RegistrationDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RegistrationDate As DateTime
            Get
                Return _registrationDate
            End Get
            Set(ByVal value As DateTime)
                _registrationDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TrClassRegistrationID", "{0}")> _
        <RelationInfo("TrClassRegistration", "ID", "TrBookingCourse", "TrClassRegistrationID")> _
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

        <ColumnInfo("ValidateDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidateDate As DateTime
            Get
                Return _validateDate
            End Get
            Set(ByVal value As DateTime)
                _validateDate = value
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

        Public Function GetStatusPendaftaran() As String
            Dim strRest As String = "Belum Terdaftar"
            Try
                If IsNothing(TrClassRegistration) Then
                    If Not ID.Equals(0) Then
                        strRest = "Diajukan"
                    End If
                Else
                    Return "Terdaftar di kelas " + TrClassRegistration.TrClass.ClassCode
                End If
            Catch
            End Try
            Return strRest
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
