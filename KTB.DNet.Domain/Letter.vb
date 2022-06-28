#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Letter Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2007 - 11:35:03 AM
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
    <Serializable(), TableInfo("Letter")> _
    Public Class Letter
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
        Private _nomorSurat As String = String.Empty
        Private _uploadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _penerima As String = String.Empty
        Private _perihal As String = String.Empty
        Private _uploadBy As String = String.Empty
        Private _lastDownloadBy As String = String.Empty
        Private _lastDownloadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fileName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _department As Department
        Private _kindOfLetter As KindOfLetter

        Private _letterHistorys As System.Collections.ArrayList = New System.Collections.ArrayList

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


        <ColumnInfo("NomorSurat", "'{0}'")> _
        Public Property NomorSurat() As String
            Get
                Return _nomorSurat
            End Get
            Set(ByVal value As String)
                _nomorSurat = value
            End Set
        End Property


        <ColumnInfo("UploadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property UploadDate() As DateTime
            Get
                Return _uploadDate
            End Get
            Set(ByVal value As DateTime)
                _uploadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Penerima", "'{0}'")> _
        Public Property Penerima() As String
            Get
                Return _penerima
            End Get
            Set(ByVal value As String)
                _penerima = value
            End Set
        End Property


        <ColumnInfo("Perihal", "'{0}'")> _
        Public Property Perihal() As String
            Get
                Return _perihal
            End Get
            Set(ByVal value As String)
                _perihal = value
            End Set
        End Property


        <ColumnInfo("UploadBy", "'{0}'")> _
        Public Property UploadBy() As String
            Get
                Return _uploadBy
            End Get
            Set(ByVal value As String)
                _uploadBy = value
            End Set
        End Property


        <ColumnInfo("LastDownloadBy", "'{0}'")> _
        Public Property LastDownloadBy() As String
            Get
                Return _lastDownloadBy
            End Get
            Set(ByVal value As String)
                _lastDownloadBy = value
            End Set
        End Property


        <ColumnInfo("LastDownloadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property LastDownloadDate() As DateTime
            Get
                Return _lastDownloadDate
            End Get
            Set(ByVal value As DateTime)
                _lastDownloadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "Letter", "DealerID")> _
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

        <ColumnInfo("DepartmentID", "{0}"), _
        RelationInfo("Department", "ID", "Letter", "DepartmentID")> _
        Public Property Department() As Department
            Get
                Try
                    If Not IsNothing(Me._department) AndAlso (Not Me._department.IsLoaded) Then

                        Me._department = CType(DoLoad(GetType(Department).ToString(), _department.ID), Department)
                        Me._department.MarkLoaded()

                    End If

                    Return Me._department

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Department)

                Me._department = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._department.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("KindOfLetterID", "{0}"), _
        RelationInfo("KindOfLetter", "ID", "Letter", "KindOfLetterID")> _
        Public Property KindOfLetter() As KindOfLetter
            Get
                Try
                    If Not IsNothing(Me._kindOfLetter) AndAlso (Not Me._kindOfLetter.IsLoaded) Then

                        Me._kindOfLetter = CType(DoLoad(GetType(KindOfLetter).ToString(), _kindOfLetter.ID), KindOfLetter)
                        Me._kindOfLetter.MarkLoaded()

                    End If

                    Return Me._kindOfLetter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As KindOfLetter)

                Me._kindOfLetter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._kindOfLetter.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("Letter", "ID", "LetterHistory", "LetterID")> _
          Public ReadOnly Property LetterHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._letterHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(LetterHistory), "Letter", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(LetterHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._letterHistorys = DoLoadArray(GetType(LetterHistory).ToString, criterias)
                    End If

                    Return Me._letterHistorys

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

    End Class
End Namespace

