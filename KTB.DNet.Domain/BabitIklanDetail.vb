
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitIklanDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 09/05/2019 - 8:33:08
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
    <Serializable(), TableInfo("BabitIklanDetail")> _
    Public Class BabitIklanDetail
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
        Private _mediaName As String = String.Empty
        Private _size As String = String.Empty
        Private _viewNumber As Integer
        Private _submissionAmount As Decimal
        Private _periodIklanStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodIklanEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _babitHeaderID As Integer
        'Private _babitParameterDetailID As Integer

        Private _babitHeader As BabitHeader
        Private _babitParameterDetail As BabitParameterDetail
        Private _babitParameterHeader As BabitParameterHeader
        Private _category As Category

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


        <ColumnInfo("MediaName", "'{0}'")> _
        Public Property MediaName As String
            Get
                Return _mediaName
            End Get
            Set(ByVal value As String)
                _mediaName = value
            End Set
        End Property


        <ColumnInfo("Size", "'{0}'")> _
        Public Property Size As String
            Get
                Return _size
            End Get
            Set(ByVal value As String)
                _size = value
            End Set
        End Property


        <ColumnInfo("ViewNumber", "{0}")> _
        Public Property ViewNumber As Integer
            Get
                Return _viewNumber
            End Get
            Set(ByVal value As Integer)
                _viewNumber = value
            End Set
        End Property


        <ColumnInfo("SubmissionAmount", "{0}")> _
        Public Property SubmissionAmount As Decimal
            Get
                Return _submissionAmount
            End Get
            Set(ByVal value As Decimal)
                _submissionAmount = value
            End Set
        End Property


        <ColumnInfo("PeriodIklanStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodIklanStart As DateTime
            Get
                Return _periodIklanStart
            End Get
            Set(ByVal value As DateTime)
                _periodIklanStart = value
            End Set
        End Property

        <ColumnInfo("PeriodIklanEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodIklanEnd As DateTime
            Get
                Return _periodIklanEnd
            End Get
            Set(ByVal value As DateTime)
                _periodIklanEnd = value
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


        '    <ColumnInfo("BabitHeaderID", "{0}")> _
        '    Public Property BabitHeaderID As Integer

        '        Get
        'return _babitHeaderID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _babitHeaderID = value
        '        End Set
        '    End Property
        '    <ColumnInfo("BabitParameterDetailID", "{0}")> _
        '    Public Property BabitParameterDetailID As Integer

        '        Get
        'return _babitParameterDetailID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _babitParameterDetailID = value
        '        End Set
        '    End Property

        <ColumnInfo("BabitHeaderID", "{0}"), _
        RelationInfo("BabitHeader", "ID", "BabitIklanDetail", "BabitHeaderID")> _
        Public Property BabitHeader As BabitHeader
            Get
                Try
                    If Not IsNothing(Me._babitHeader) AndAlso (Not Me._babitHeader.IsLoaded) Then

                        Me._babitHeader = CType(DoLoad(GetType(BabitHeader).ToString(), _babitHeader.ID), BabitHeader)
                        Me._babitHeader.MarkLoaded()

                    End If

                    Return Me._babitHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitHeader)

                Me._babitHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BabitParameterDetailID", "{0}"), _
        RelationInfo("BabitParameterDetail", "ID", "BabitIklanDetail", "BabitParameterDetailID")> _
        Public Property BabitParameterDetail As BabitParameterDetail
            Get
                Try
                    If Not IsNothing(Me._babitParameterDetail) AndAlso (Not Me._babitParameterDetail.IsLoaded) Then

                        Me._babitParameterDetail = CType(DoLoad(GetType(BabitParameterDetail).ToString(), _babitParameterDetail.ID), BabitParameterDetail)
                        Me._babitParameterDetail.MarkLoaded()

                    End If

                    Return Me._babitParameterDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitParameterDetail)

                Me._babitParameterDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitParameterDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BabitParameterHeaderID", "{0}"), _
        RelationInfo("BabitParameterHeader", "ID", "BabitIklanDetail", "BabitParameterHeaderID")> _
        Public Property BabitParameterHeader As BabitParameterHeader
            Get
                Try
                    If Not IsNothing(Me._babitParameterHeader) AndAlso (Not Me._babitParameterHeader.IsLoaded) Then

                        Me._babitParameterHeader = CType(DoLoad(GetType(BabitParameterHeader).ToString(), _babitParameterHeader.ID), BabitParameterHeader)
                        Me._babitParameterHeader.MarkLoaded()

                    End If

                    Return Me._babitParameterHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitParameterHeader)

                Me._babitParameterHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitParameterHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "ChassisMaster", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
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

