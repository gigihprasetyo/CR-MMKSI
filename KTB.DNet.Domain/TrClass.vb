
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrClass Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 11/2/2006 - 3:55:17 PM
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
    <Serializable(), TableInfo("TrClass")> _
    Public Class TrClass
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
        Private _classCode As String = String.Empty
        Private _className As String = String.Empty
        Private _location As String = String.Empty
        Private _lodging As String = String.Empty
        Private _locationName As String = String.Empty
        Private _trainer1 As String = String.Empty
        Private _trainer2 As String = String.Empty
        Private _trainer3 As String = String.Empty
        Private _startDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _finishDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmDueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _capacity As Integer
        Private _description As String = String.Empty
        Private _status As String = String.Empty
        Private _submitStatus As Short
        Private _category As Integer
        Private _paidDay As Integer
        Private _pricePerDay As Decimal
        Private _priceTotal As Decimal
        Private _trCertificateConfig As TrCertificateConfig
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'additional Property
        Private _namaKhusus As String = String.Empty
        'end add

        Private _trCourse As TrCourse
        Private _trClassRegistrations As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _trMRTC As TrMRTC
        Private _city As City
        Private _classType As Short
        Private _fiscalYear As String = String.Empty
        Private _filePath As String = String.Empty
        Private _urlPath As String = String.Empty
        Private _fileCertificatePath As String = String.Empty

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


        <ColumnInfo("ClassCode", "'{0}'")> _
        Public Property ClassCode() As String
            Get
                Return _classCode
            End Get
            Set(ByVal value As String)
                _classCode = value
            End Set
        End Property

        <ColumnInfo("PaidDay", "'{0}'")> _
        Public Property PaidDay() As String
            Get
                Return _paidDay
            End Get
            Set(ByVal value As String)
                _paidDay = value
            End Set
        End Property

        <ColumnInfo("PricePerDay", "'{0}'")> _
        Public Property PricePerDay() As Decimal
            Get
                Return _pricePerDay
            End Get
            Set(ByVal value As Decimal)
                _pricePerDay = value
            End Set
        End Property

        <ColumnInfo("PriceTotal", "'{0}'")> _
        Public Property PriceTotal() As Decimal
            Get
                Return _priceTotal
            End Get
            Set(ByVal value As Decimal)
                _priceTotal = value
            End Set
        End Property


        <ColumnInfo("ClassName", "'{0}'")> _
        Public Property ClassName() As String
            Get
                Return _className
            End Get
            Set(ByVal value As String)
                _className = value
            End Set
        End Property


        <ColumnInfo("Location", "'{0}'")> _
        Public Property Location() As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property

        <ColumnInfo("LocationName", "'{0}'")> _
        Public Property LocationName() As String
            Get
                Return _locationName
            End Get
            Set(ByVal value As String)
                _locationName = value
            End Set
        End Property

        <ColumnInfo("Lodging", "'{0}'")> _
        Public Property Lodging() As String
            Get
                Return _lodging
            End Get
            Set(ByVal value As String)
                _lodging = value
            End Set
        End Property


        <ColumnInfo("Trainer1", "'{0}'")> _
        Public Property Trainer1() As String
            Get
                Return _trainer1
            End Get
            Set(ByVal value As String)
                _trainer1 = value
            End Set
        End Property


        <ColumnInfo("Trainer2", "'{0}'")> _
        Public Property Trainer2() As String
            Get
                Return _trainer2
            End Get
            Set(ByVal value As String)
                _trainer2 = value
            End Set
        End Property


        <ColumnInfo("Trainer3", "'{0}'")> _
        Public Property Trainer3() As String
            Get
                Return _trainer3
            End Get
            Set(ByVal value As String)
                _trainer3 = value
            End Set
        End Property


        <ColumnInfo("StartDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartDate() As DateTime
            Get
                Return _startDate
            End Get
            Set(ByVal value As DateTime)
                _startDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FinishDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FinishDate() As DateTime
            Get
                Return _finishDate
            End Get
            Set(ByVal value As DateTime)
                _finishDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ConfirmDueDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ConfirmDueDate() As DateTime
            Get
                Return _confirmDueDate
            End Get
            Set(ByVal value As DateTime)
                _confirmDueDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property



        <ColumnInfo("Capacity", "{0}")> _
        Public Property Capacity() As Integer
            Get
                Return _capacity
            End Get
            Set(ByVal value As Integer)
                _capacity = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        <ColumnInfo("SubmitStatus", "'{0}'")> _
        Public Property SubmitStatus() As Short
            Get
                Return _submitStatus
            End Get
            Set(ByVal value As Short)
                _submitStatus = value
            End Set
        End Property

        <ColumnInfo("FilePath", "'{0}'")> _
        Public Property FilePath() As String
            Get
                Return _filePath
            End Get
            Set(ByVal value As String)
                _filePath = value
            End Set
        End Property

        <ColumnInfo("UrlPath", "'{0}'")> _
        Public Property UrlPath() As String
            Get
                Return _urlPath
            End Get
            Set(ByVal value As String)
                _urlPath = value
            End Set
        End Property

        <ColumnInfo("FileCertificatePath", "'{0}'")> _
        Public Property FileCertificatePath() As String
            Get
                Return _fileCertificatePath
            End Get
            Set(ByVal value As String)
                _fileCertificatePath = value
            End Set
        End Property

        <ColumnInfo("FiscalYear", "'{0}'")> _
        Public Property FiscalYear() As String
            Get
                Return _fiscalYear
            End Get
            Set(ByVal value As String)
                _fiscalYear = value
            End Set
        End Property

        <ColumnInfo("ClassType", "'{0}'")> _
        Public Property ClassType() As Short
            Get
                Return _classType
            End Get
            Set(ByVal value As Short)
                _classType = value
            End Set
        End Property


        <ColumnInfo("Category", "{0}")> _
        Public Property Category() As Integer
            Get
                Return _category
            End Get
            Set(ByVal value As Integer)
                _category = value
            End Set
        End Property

        <ColumnInfo("CityID", "{0}"), _
          RelationInfo("City", "ID", "TrClass", "CityID")> _
        Public Property City() As City
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

        <ColumnInfo("TrMRTCID", "{0}"), _
          RelationInfo("TrMRTC", "ID", "TrClass", "TrMRTCID")> _
        Public Property TrMRTC() As TrMRTC
            Get
                Try
                    If Not IsNothing(Me._trMRTC) AndAlso (Not Me._trMRTC.IsLoaded) Then

                        Me._trMRTC = CType(DoLoad(GetType(TrMRTC).ToString(), _trMRTC.ID), TrMRTC)
                        Me._trMRTC.MarkLoaded()

                    End If

                    Return Me._trMRTC

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrMRTC)

                Me._trMRTC = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trMRTC.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TrCertificateConfigID", "{0}"), _
        RelationInfo("TrCertificateConfig", "ID", "TrClass", "TrCertificateConfigID")> _
        Public Property TrCertificateConfig() As TrCertificateConfig
            Get
                Try
                    If Not IsNothing(Me._trCertificateConfig) AndAlso (Not Me._trCertificateConfig.IsLoaded) Then

                        Me._trCertificateConfig = CType(DoLoad(GetType(TrCertificateConfig).ToString(), _trCertificateConfig.ID), TrCertificateConfig)
                        Me._trCertificateConfig.MarkLoaded()

                    End If

                    Return Me._trCertificateConfig

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrCertificateConfig)

                Me._trCertificateConfig = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trCertificateConfig.MarkLoaded()
                End If
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


        <ColumnInfo("CourseID", "{0}"), _
        RelationInfo("TrCourse", "ID", "TrClass", "CourseID")> _
        Public Property TrCourse() As TrCourse
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


        <RelationInfo("TrClass", "ID", "TrClassRegistration", "ClassID")> _
        Public ReadOnly Property TrClassRegistrations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trClassRegistrations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrClassRegistration), "TrClass", Me.ID)
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


        'Additional Property
        Public ReadOnly Property NamaKhusus(ByVal _classId As String, ByVal _trCourseEvaluationId As Integer) As String
            Get
                Dim _lst As New ArrayList
                Dim crtParam As CriteriaComposite

                crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrCourseEvaluation", MatchType.Exact, _trCourseEvaluationId))
                crtParam.opAnd(New Criteria(GetType(TrClassNumEvaluation), "TrClass", MatchType.Exact, _classId))

                _lst = DoLoadArray(GetType(TrClassNumEvaluation).ToString, crtParam)
                '_lst = _trCourseNumEvalFacade.Retrieve(crtParam)
                If (_lst.Count > 0) Then
                    Return CType(_lst(0), TrClassNumEvaluation).SpecialName()
                Else
                    Return ""
                End If

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

    End Class
End Namespace

