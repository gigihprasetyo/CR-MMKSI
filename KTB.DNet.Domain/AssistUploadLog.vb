
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistUploadLog Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/2/2018 - 9:17:09 AM
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
    <Serializable(), TableInfo("AssistUploadLog")> _
    Public Class AssistUploadLog
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
        Private _moduleID As Integer
        Private _errorRatio As Decimal
        Private _performance As Decimal
        Private _uploadTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _originalFileName As String = String.Empty
        Private _fileName As String = String.Empty
        Private _status As Short
        Private _month As Integer
        Private _year As Integer
        Private _validateStatus As Short
        Private _errorMessage As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _period As String = String.Empty
        Private _dealer As Dealer
        Private _module As AssistModule

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

        <ColumnInfo("ModuleID", "{0}")> _
        Public Property ModuleID As Integer
            Get
                Return _moduleID
            End Get
            Set(ByVal value As Integer)
                _moduleID = value
            End Set
        End Property

        <ColumnInfo("Month", "{0}")> _
              Public Property Month As Integer
            Get
                Return _month
            End Get
            Set(ByVal value As Integer)
                _month = value
            End Set
        End Property


        <ColumnInfo("Year", "{0}")> _
        Public Property Year As Integer
            Get
                Return _year
            End Get
            Set(ByVal value As Integer)
                _year = value
            End Set
        End Property

        <ColumnInfo("ErrorRatio", "#,##0")> _
        Public Property ErrorRatio As Decimal
            Get
                Return _errorRatio
            End Get
            Set(ByVal value As Decimal)
                _errorRatio = value
            End Set
        End Property


        <ColumnInfo("Performance", "#,##0")> _
        Public Property Performance As Decimal
            Get
                Return _performance
            End Get
            Set(ByVal value As Decimal)
                _performance = value
            End Set
        End Property


        <ColumnInfo("UploadTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property UploadTime As DateTime
            Get
                Return _uploadTime
            End Get
            Set(ByVal value As DateTime)
                _uploadTime = value
            End Set
        End Property


        <ColumnInfo("OriginalFileName", "'{0}'")> _
        Public Property OriginalFileName As String
            Get
                Return _originalFileName
            End Get
            Set(ByVal value As String)
                _originalFileName = value
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

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("ValidateStatus", "{0}")> _
        Public Property ValidateStatus As Short
            Get
                Return _validateStatus
            End Get
            Set(ByVal value As Short)
                _validateStatus = value
            End Set
        End Property


        <ColumnInfo("ErrorMessage", "'{0}'")> _
        Public Property ErrorMessage As String
            Get
                Return _errorMessage
            End Get
            Set(ByVal value As String)
                _errorMessage = value
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

        Public Property StatusDescription As String
            Get
                Select Case ValidateStatus
                    Case 0
                        Return "Gagal Validasi System"
                    Case 1
                        Return "Menunggu Validasi"
                    Case 2
                        Return "Tolak Validasi"
                    Case 3
                        Return "Menunggu Konfirmasi"
                    Case 4
                        Return "Tolak Konfirmasi"
                    Case 5
                        Return "Selesai"
                End Select
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "AssistUploadLog", "DealerID")> _
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

        <ColumnInfo("ModuleID", "{0}"), _
        RelationInfo("AssistModule", "ID", "AssistUploadLog", "ModuleID")> _
        Public Property AssistModule() As AssistModule
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._module.IsLoaded) Then

                        Me._module = CType(DoLoad(GetType(AssistModule).ToString(), _module.ID), AssistModule)
                        Me._module.MarkLoaded()

                    End If

                    Return Me._module

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistModule)

                Me._module = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._module.MarkLoaded()
                End If
            End Set
        End Property

        Public Property Period As String
            Get
                Return String.Concat(enumMonthGet.GetName(Month).ToString(), " ", Year.ToString())
            End Get
            Set(ByVal value As String)
                _period = value
            End Set
        End Property

#End Region

    End Class
End Namespace

