#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrAdditionalClass Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2019 - 11:09:48 AM
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
    <Serializable(), TableInfo("TrAdditionalClass")> _
    Public Class TrAdditionalClass
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
        Private _dealer As Dealer
        Private _classCode As String = String.Empty
        Private _className As String = String.Empty
        Private _trCourse As TrCourse
        Private _location As String = String.Empty
        Private _locationName As String = String.Empty
        Private _trainer1 As String = String.Empty
        Private _trainer2 As String = String.Empty
        Private _trainer3 As String = String.Empty
        Private _startDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _finishDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _description As String = String.Empty
        Private _classType As Short
        Private _city As City
        Private _fileName As String = String.Empty
        Private _fileMateriPath As String = String.Empty
        Private _fileSiswaPath As String = String.Empty
        Private _fiscalYear As String = String.Empty
        Private _status As String = String.Empty
        Private _aPMResponse As String = String.Empty
        Private _submitStatus As Short
        Private _category As Integer
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

        <ColumnInfo("DealerID", "{0}"), _
         RelationInfo("Dealer", "ID", "TrAdditionalClass", "DealerID")> _
        Public Property Dealer() As Dealer
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

        <ColumnInfo("ClassCode", "'{0}'")> _
        Public Property ClassCode As String
            Get
                Return _classCode
            End Get
            Set(ByVal value As String)
                _classCode = value
            End Set
        End Property


        <ColumnInfo("ClassName", "'{0}'")> _
        Public Property ClassName As String
            Get
                Return _className
            End Get
            Set(ByVal value As String)
                _className = value
            End Set
        End Property


        <ColumnInfo("CourseID", "{0}"), _
         RelationInfo("TrCourse", "ID", "TrAdditionalClass", "CourseID")> _
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



        <ColumnInfo("Location", "'{0}'")> _
        Public Property Location As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                _location = value
            End Set
        End Property


        <ColumnInfo("LocationName", "'{0}'")> _
        Public Property LocationName As String
            Get
                Return _locationName
            End Get
            Set(ByVal value As String)
                _locationName = value
            End Set
        End Property


        <ColumnInfo("Trainer1", "'{0}'")> _
        Public Property Trainer1 As String
            Get
                Return _trainer1
            End Get
            Set(ByVal value As String)
                _trainer1 = value
            End Set
        End Property


        <ColumnInfo("Trainer2", "'{0}'")> _
        Public Property Trainer2 As String
            Get
                Return _trainer2
            End Get
            Set(ByVal value As String)
                _trainer2 = value
            End Set
        End Property


        <ColumnInfo("Trainer3", "'{0}'")> _
        Public Property Trainer3 As String
            Get
                Return _trainer3
            End Get
            Set(ByVal value As String)
                _trainer3 = value
            End Set
        End Property


        <ColumnInfo("StartDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartDate As DateTime
            Get
                Return _startDate
            End Get
            Set(ByVal value As DateTime)
                _startDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FinishDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FinishDate As DateTime
            Get
                Return _finishDate
            End Get
            Set(ByVal value As DateTime)
                _finishDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("ClassType", "{0}")> _
        Public Property ClassType As Short
            Get
                Return _classType
            End Get
            Set(ByVal value As Short)
                _classType = value
            End Set
        End Property


        <ColumnInfo("CityID", "{0}"), _
          RelationInfo("City", "ID", "TrAdditionalClass", "CityID")> _
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


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property

        <ColumnInfo("FileMateriPath", "'{0}'")> _
        Public Property FileMateriPath As String
            Get
                Return _fileMateriPath
            End Get
            Set(ByVal value As String)
                _fileMateriPath = value
            End Set
        End Property


        <ColumnInfo("FileSiswaPath", "'{0}'")> _
        Public Property FileSiswaPath As String
            Get
                Return _fileSiswaPath
            End Get
            Set(ByVal value As String)
                _fileSiswaPath = value
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


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("APMResponse", "'{0}'")> _
        Public Property APMResponse As String
            Get
                Return _aPMResponse
            End Get
            Set(ByVal value As String)
                _aPMResponse = value
            End Set
        End Property


        <ColumnInfo("SubmitStatus", "{0}")> _
        Public Property SubmitStatus As Short
            Get
                Return _submitStatus
            End Get
            Set(ByVal value As Short)
                _submitStatus = value
            End Set
        End Property


        <ColumnInfo("Category", "{0}")> _
        Public Property Category As Integer
            Get
                Return _category
            End Get
            Set(ByVal value As Integer)
                _category = value
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
