#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("SparepartNotaRetur")> _
    Public Class SparepartNotaRetur
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
        Private _noDoc As String = String.Empty
        Private _typeDoc As Short
        Private _periodeMonth As Short
        Private _periodeYear As Short
        Private _fileNamePath As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

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

        <ColumnInfo("NoDoc", "'{0}'")> _
        Public Property NoDoc As String
            Get
                Return _noDoc
            End Get
            Set(ByVal value As String)
                _noDoc = value
            End Set
        End Property

        <ColumnInfo("TypeDoc", "{0}")> _
        Public Property TypeDoc() As Short
            Get
                Return _typeDoc
            End Get
            Set(ByVal value As Short)
                _typeDoc = value
            End Set
        End Property

        <ColumnInfo("PeriodeMonth", "{0}")> _
        Public Property PeriodeMonth() As Short
            Get
                Return _periodeMonth
            End Get
            Set(ByVal value As Short)
                _periodeMonth = value
            End Set
        End Property

        <ColumnInfo("PeriodeYear", "{0}")> _
        Public Property PeriodeYear() As Short
            Get
                Return _periodeYear
            End Get
            Set(ByVal value As Short)
                _periodeYear = value
            End Set
        End Property

        <ColumnInfo("FileNamePath", "'{0}'")> _
        Public Property FileNamePath() As String
            Get
                Return _fileNamePath
            End Get
            Set(ByVal value As String)
                _fileNamePath = value
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
        RelationInfo("Dealer", "ID", "SparepartNotaRetur", "DealerID")> _
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

