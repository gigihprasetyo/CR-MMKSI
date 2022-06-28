#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPRegister Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/21/2007 - 11:18:37 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
Imports System.Globalization
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("SAPRegister")> _
    Public Class SAPRegister
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
        Private _isCancelled As Byte
        Private _writingTestScore As Decimal
        Private _isEntryTestScore As Byte
        Private _gradeSWAP As Integer
        Private _gradePresentasi As Integer
        Private _gradeKonsistensi As Integer
        Private _gradeKelengkapan As Integer
        Private _gradeFrekuensi As Integer
        Private _jumlahPeserta As Short
        Private _rptProsPek As Integer
        Private _rptHotProspek As Integer
        Private _rptFaktur As Integer
        Private _rptPDI As Integer
        Private _rptAvgScoreSubOrdinate As Decimal
        Private _rptEffectivity As Integer
        Private _rptAchievement As Integer
        Private _rptAvgScoreNominator As Integer
        Private _rptWinnerAmount As Integer
        Private _rptKomposisi As Integer
        Private _gradeFinal As Integer
        Private _isWinner As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesmanHeader As SalesmanHeader
        Private _sAPPeriod As SAPPeriod



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


        <ColumnInfo("IsCancelled", "{0}")> _
        Public Property IsCancelled() As Byte
            Get
                Return _isCancelled
            End Get
            Set(ByVal value As Byte)
                _isCancelled = value
            End Set
        End Property


        <ColumnInfo("WritingTestScore", "#,##0")> _
        Public Property WritingTestScore() As Decimal
            Get
                Return _writingTestScore
            End Get
            Set(ByVal value As Decimal)
                _writingTestScore = value
            End Set
        End Property


        <ColumnInfo("IsEntryTestScore", "{0}")> _
        Public Property IsEntryTestScore() As Byte
            Get
                Return _isEntryTestScore
            End Get
            Set(ByVal value As Byte)
                _isEntryTestScore = value
            End Set
        End Property


        <ColumnInfo("GradeSWAP", "{0}")> _
        Public Property GradeSWAP() As Integer
            Get
                Return _gradeSWAP
            End Get
            Set(ByVal value As Integer)
                _gradeSWAP = value
            End Set
        End Property


        <ColumnInfo("GradePresentasi", "{0}")> _
        Public Property GradePresentasi() As Integer
            Get
                Return _gradePresentasi
            End Get
            Set(ByVal value As Integer)
                _gradePresentasi = value
            End Set
        End Property


        <ColumnInfo("GradeKonsistensi", "{0}")> _
        Public Property GradeKonsistensi() As Integer
            Get
                Return _gradeKonsistensi
            End Get
            Set(ByVal value As Integer)
                _gradeKonsistensi = value
            End Set
        End Property


        <ColumnInfo("GradeKelengkapan", "{0}")> _
  Public Property GradeKelengkapan() As Integer
            Get
                Return _gradeKelengkapan
            End Get
            Set(ByVal value As Integer)
                _gradeKelengkapan = value
            End Set
        End Property


        <ColumnInfo("GradeFrekuensi", "{0}")> _
        Public Property GradeFrekuensi() As Integer
            Get
                Return _gradeFrekuensi
            End Get
            Set(ByVal value As Integer)
                _gradeFrekuensi = value
            End Set
        End Property


        <ColumnInfo("JumlahPeserta", "{0}")> _
        Public Property JumlahPeserta() As Short
            Get
                Return _jumlahPeserta
            End Get
            Set(ByVal value As Short)
                _jumlahPeserta = value
            End Set
        End Property


        <ColumnInfo("RptProsPek", "{0}")> _
        Public Property RptProsPek() As Integer
            Get
                Return _rptProsPek
            End Get
            Set(ByVal value As Integer)
                _rptProsPek = value
            End Set
        End Property


        <ColumnInfo("RptHotProspek", "{0}")> _
        Public Property RptHotProspek() As Integer
            Get
                Return _rptHotProspek
            End Get
            Set(ByVal value As Integer)
                _rptHotProspek = value
            End Set
        End Property


        <ColumnInfo("RptFaktur", "{0}")> _
        Public Property RptFaktur() As Integer
            Get
                Return _rptFaktur
            End Get
            Set(ByVal value As Integer)
                _rptFaktur = value
            End Set
        End Property


        <ColumnInfo("RptPDI", "{0}")> _
        Public Property RptPDI() As Integer
            Get
                Return _rptPDI
            End Get
            Set(ByVal value As Integer)
                _rptPDI = value
            End Set
        End Property


        <ColumnInfo("RptAvgScoreSubOrdinate", "#,##0")> _
        Public Property RptAvgScoreSubOrdinate() As Decimal
            Get
                Return _rptAvgScoreSubOrdinate
            End Get
            Set(ByVal value As Decimal)
                _rptAvgScoreSubOrdinate = value
            End Set
        End Property


        <ColumnInfo("RptEffectivity", "{0}")> _
        Public Property RptEffectivity() As Integer
            Get
                Return _rptEffectivity
            End Get
            Set(ByVal value As Integer)
                _rptEffectivity = value
            End Set
        End Property


        <ColumnInfo("RptAchievement", "{0}")> _
        Public Property RptAchievement() As Integer
            Get
                Return _rptAchievement
            End Get
            Set(ByVal value As Integer)
                _rptAchievement = value
            End Set
        End Property


        <ColumnInfo("RptAvgScoreNominator", "{0}")> _
        Public Property RptAvgScoreNominator() As Integer
            Get
                Return _rptAvgScoreNominator
            End Get
            Set(ByVal value As Integer)
                _rptAvgScoreNominator = value
            End Set
        End Property


        <ColumnInfo("RptWinnerAmount", "{0}")> _
        Public Property RptWinnerAmount() As Integer
            Get
                Return _rptWinnerAmount
            End Get
            Set(ByVal value As Integer)
                _rptWinnerAmount = value
            End Set
        End Property


        <ColumnInfo("RptKomposisi", "{0}")> _
        Public Property RptKomposisi() As Integer
            Get
                Return _rptKomposisi
            End Get
            Set(ByVal value As Integer)
                _rptKomposisi = value
            End Set
        End Property


        <ColumnInfo("GradeFinal", "{0}")> _
        Public Property GradeFinal() As Integer
            Get
                Return _gradeFinal
            End Get
            Set(ByVal value As Integer)
                _gradeFinal = value
            End Set
        End Property


        <ColumnInfo("IsWinner", "{0}")> _
        Public Property IsWinner() As Byte
            Get
                Return _isWinner
            End Get
            Set(ByVal value As Byte)
                _isWinner = value
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


        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "SAPRegister", "SalesmanHeaderID")> _
        Public Property SalesmanHeader() As SalesmanHeader
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

        <ColumnInfo("SAPPeriodID", "{0}"), _
        RelationInfo("SAPPeriod", "ID", "SAPRegister", "SAPPeriodID")> _
        Public Property SAPPeriod() As SAPPeriod
            Get
                Try
                    If Not IsNothing(Me._sAPPeriod) AndAlso (Not Me._sAPPeriod.IsLoaded) Then

                        Me._sAPPeriod = CType(DoLoad(GetType(SAPPeriod).ToString(), _sAPPeriod.ID), SAPPeriod)
                        Me._sAPPeriod.MarkLoaded()

                    End If

                    Return Me._sAPPeriod

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SAPPeriod)

                Me._sAPPeriod = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sAPPeriod.MarkLoaded()
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

#Region "Custom Properties"

        Public ReadOnly Property TestScoreDisplay() As String
            Get
                If Me.IsEntryTestScore = 0 Then
                    Return ""
                Else
                    If Me.WritingTestScore = 0 Then
                        Return "0"
                    Else
                        Dim MyCulture As CultureInfo = New CultureInfo("id-ID")
                        Return Me.WritingTestScore.ToString("#,#.#", MyCulture)
                    End If
                End If
            End Get
        End Property

#End Region

    End Class
End Namespace

