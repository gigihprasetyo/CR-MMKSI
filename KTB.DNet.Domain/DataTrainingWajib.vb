

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("Area1")> _
    Public Class DataTrainingWajib
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
        Private _CourseCategoryCode As String = String.Empty
        Private _CourseCode As String = String.Empty
        Private _IsPass As Short = 0

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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property CourseCategoryCode() As String
            Get
                Return _CourseCategoryCode
            End Get
            Set(ByVal value As String)
                _CourseCategoryCode = value
            End Set
        End Property


        <ColumnInfo("CourseCode", "'{0}'")> _
        Public Property CourseCode() As String
            Get
                Return _CourseCode
            End Get
            Set(ByVal value As String)
                _CourseCode = value
            End Set
        End Property

        <ColumnInfo("IsLulus", "{0}")> _
        Public Property IsPass() As Short
            Get
                Return _IsPass
            End Get
            Set(ByVal value As Short)
                _IsPass = value
            End Set
        End Property

#End Region

#Region "Custom Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

    End Class

    Public Class DetailTrainingWajib
        Private _CourseCategoryCode As String
        Public Property CourseCategoryCode() As String
            Get
                Return _CourseCategoryCode
            End Get
            Set(ByVal value As String)
                _CourseCategoryCode = value
            End Set
        End Property

        Private _CourseCategoryIsPass As String
        Public Property CourseCategoryIsPass() As String
            Get
                Return _CourseCategoryIsPass
            End Get
            Set(ByVal value As String)
                _CourseCategoryIsPass = value
            End Set
        End Property

        Private _CourseCategoryIsNotPass As String
        Public Property CourseCategoryIsNotPass() As String
            Get
                Return _CourseCategoryIsNotPass
            End Get
            Set(ByVal value As String)
                _CourseCategoryIsNotPass = value
            End Set
        End Property

    End Class
End Namespace

