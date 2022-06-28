
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : TrTrainee Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/08/2018 - 13:45:07
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
Imports System.Collections.Generic
Imports System.Linq
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("TrTrainee")> _
    Public Class TrTrainee
        Inherits DomainObject

#Region "Public Constanta"
        Public Const MAX_PHOTO_SIZE As Integer = 20480  'max 20 kb
        Public Const VALID_IMAGE_TYPE As String = "IMAGE"
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _name As String = String.Empty
        Private _birthDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gender As Byte
        Private _noKTP As String = String.Empty
        Private _email As String = String.Empty
        Private _startWorkingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As String = String.Empty
        Private _jobPosition As String = String.Empty
        Private _educationLevel As String = String.Empty
        Private _photo As Byte()
        Private _shirtSize As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _salesmanHeader As SalesmanHeader
        Private _trClassRegistrations As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _refJobPosition As JobPosition

        Private _listtrTraineeSalesmanHeader As New List(Of TrTraineeSalesmanHeader)
        Private _trTraineeSalesmanHeader As TrTraineeSalesmanHeader


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


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        <ColumnInfo("BirthDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BirthDate As DateTime
            Get
                Return _birthDate
            End Get
            Set(ByVal value As DateTime)
                _birthDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Gender", "{0}")> _
        Public Property Gender As Byte
            Get
                Return _gender
            End Get
            Set(ByVal value As Byte)
                _gender = value
            End Set
        End Property


        <ColumnInfo("NoKTP", "'{0}'")> _
        Public Property NoKTP As String
            Get
                Return _noKTP
            End Get
            Set(ByVal value As String)
                _noKTP = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("StartWorkingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartWorkingDate As DateTime
            Get
                Return _startWorkingDate
            End Get
            Set(ByVal value As DateTime)
                _startWorkingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("JobPosition", "'{0}'")> _
        Public Property JobPosition As String
            Get
                Return _jobPosition
            End Get
            Set(ByVal value As String)
                _jobPosition = value
            End Set
        End Property


        <ColumnInfo("EducationLevel", "'{0}'")> _
        Public Property EducationLevel As String
            Get
                Return _educationLevel
            End Get
            Set(ByVal value As String)
                _educationLevel = value
            End Set
        End Property


        <ColumnInfo("Photo", "{0}")> _
        Public Property Photo As Byte()
            Get
                Return _photo
            End Get
            Set(ByVal value As Byte())
                _photo = value
            End Set
        End Property


        <ColumnInfo("ShirtSize", "'{0}'")> _
        Public Property ShirtSize As String
            Get
                Return _shirtSize
            End Get
            Set(ByVal value As String)
                _shirtSize = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "TrTrainee", "DealerID")> _
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

        <ColumnInfo("DealerBranchID", "{0}"), _
        RelationInfo("DealerBranch", "ID", "TrTrainee", "DealerBranchID")> _
        Public Property DealerBranch As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "TrTrainee", "SalesmanHeaderID")> _
        Public Property SalesmanHeader As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeader) AndAlso (Not Me._salesmanHeader.IsLoaded) Then

                        Me._salesmanHeader = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeader.ID), SalesmanHeader)
                        Me._salesmanHeader.MarkLoaded()

                    End If

                    Return Me._salesmanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("JobPosition", "{0}"), _
        RelationInfo("JobPosition", "Code", "TrTrainee", "JobPosition")> _
        Public ReadOnly Property RefJobPosition As JobPosition
            Get
                Try
                    Dim isNotValid As Boolean = False
                    If (Me._refJobPosition Is Nothing) Then
                        isNotValid = True
                    Else
                        If Me._refJobPosition.ID.Equals(0) Then
                            isNotValid = True
                        End If
                    End If
                    If isNotValid Then
                        Dim _criteria As Criteria = New Criteria(GetType(JobPosition), "Code", Me.JobPosition)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._refJobPosition = DoLoadArray(GetType(JobPosition).ToString, criterias)(0)
                    End If

                    Return Me._refJobPosition

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("TrTrainee", "ID", "TrClassRegistration", "TraineeID")> _
        Public ReadOnly Property TrClassRegistrations As System.Collections.ArrayList
            Get
                Try
                    If (Me._trClassRegistrations.Count.Equals(0)) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrClassRegistration), "TrTrainee", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._trClassRegistrations = DoLoadArray(GetType(TrClassRegistration).ToString, criterias)
                    End If

                    Return Me._trClassRegistrations

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("TrTraineeSalesmanHeader", "TrTraineeID", "TrTrainee", "ID")> _
        Public Property TrTraineeSalesmanHeader As TrTraineeSalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._trTraineeSalesmanHeader) AndAlso (Not Me._trTraineeSalesmanHeader.IsLoaded) Then

                        Me._trTraineeSalesmanHeader = CType(DoLoad(GetType(TrTraineeSalesmanHeader).ToString(), _trTraineeSalesmanHeader.ID), TrTraineeSalesmanHeader)
                        Me._trTraineeSalesmanHeader.MarkLoaded()

                    Else
                        Dim arrTrSalesmanHeader As ArrayList = New ArrayList()
                        Dim _criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "SalesmanHeader.ID", Me._salesmanHeader.ID))
                        _criteria.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        arrTrSalesmanHeader = DoLoadArray(GetType(TrTraineeSalesmanHeader).ToString, _criteria)

                        If arrTrSalesmanHeader.Count > 0 Then
                            Me._trTraineeSalesmanHeader = CType(arrTrSalesmanHeader(0), TrTraineeSalesmanHeader)
                        End If

                    End If

                    Return Me._trTraineeSalesmanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTraineeSalesmanHeader)

                Me._trTraineeSalesmanHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTraineeSalesmanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("TrTrainee", "ID", "TrTraineeSalesmanHeader", "TrTraineeID")> _
        Public ReadOnly Property ListTrTraineeSalesmanHeader() As List(Of TrTraineeSalesmanHeader)
            Get
                Try
                    Dim arrTrSalesmanHeader As ArrayList = New ArrayList()
                    If (Me._listtrTraineeSalesmanHeader.Count < 1) Then
                        Dim _criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "TrTrainee.ID", Me.ID))
                        arrTrSalesmanHeader = DoLoadArray(GetType(TrTraineeSalesmanHeader).ToString, _criteria)
                        If arrTrSalesmanHeader.Count > 0 Then
                            Me._listtrTraineeSalesmanHeader = arrTrSalesmanHeader.Cast(Of TrTraineeSalesmanHeader).ToList()
                        End If

                    End If

                    Return Me._listtrTraineeSalesmanHeader

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

#Region "Non_generated Properties"
        Private _IsTraineeRegistered As Boolean = True

        Public Property IsTraineeRegistered() As Boolean
            Get
                Return _IsTraineeRegistered
            End Get
            Set(ByVal value As Boolean)
                _IsTraineeRegistered = value
            End Set
        End Property

        Private _IsJobPositionNotMatch As Boolean = False
        Public Property IsJobPositionNotMatch() As Boolean
            Get
                Return _IsJobPositionNotMatch
            End Get
            Set(ByVal value As Boolean)
                _IsJobPositionNotMatch = value
            End Set
        End Property

        Private _grade As Integer = 0
        Public Property Grade() As Integer
            Get
                Return _grade
            End Get
            Set(ByVal value As Integer)
                _grade = value
            End Set
        End Property

#End Region
    End Class
End Namespace

