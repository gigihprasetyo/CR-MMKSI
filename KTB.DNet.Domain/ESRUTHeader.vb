#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ESRUTHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/29/2019 - 10:44:25 AM
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
    <Serializable(), TableInfo("ESRUTHeader")> _
    Public Class ESRUTHeader
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
        Private _noPengajuan As String = String.Empty
        Private _perusahaan As String = String.Empty
        Private _excelFilePath As String = String.Empty
        Private _pdfFilePath As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _listOfItem As System.Collections.ArrayList = New System.Collections.ArrayList




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


        <ColumnInfo("NoPengajuan", "'{0}'")> _
        Public Property NoPengajuan As String
            Get
                Return _noPengajuan
            End Get
            Set(ByVal value As String)
                _noPengajuan = value
            End Set
        End Property


        <ColumnInfo("Perusahaan", "'{0}'")> _
        Public Property Perusahaan As String
            Get
                Return _perusahaan
            End Get
            Set(ByVal value As String)
                _perusahaan = value
            End Set
        End Property


        <ColumnInfo("ExcelFilePath", "'{0}'")> _
        Public Property ExcelFilePath As String
            Get
                Return _excelFilePath
            End Get
            Set(ByVal value As String)
                _excelFilePath = value
            End Set
        End Property


        <ColumnInfo("PdfFilePath", "'{0}'")> _
        Public Property PdfFilePath As String
            Get
                Return _pdfFilePath
            End Get
            Set(ByVal value As String)
                _pdfFilePath = value
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


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
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


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property

        <RelationInfo("ESRUTHeader", "ID", "ESRUTItem", "ESRUTHeaderID")> _
        Public Property ListOfItem() As System.Collections.ArrayList
            Get
                Try
                    If (Me._listOfItem.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ESRUTItem), "ESRUTHeader.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ESRUTItem), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._listOfItem = DoLoadArray(GetType(ESRUTItem).ToString, criterias)
                    End If

                    Return Me._listOfItem

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get

            Set(value As System.Collections.ArrayList)
                _listOfItem = value
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
