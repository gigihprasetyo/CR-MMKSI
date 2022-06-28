
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPAFDoc_UploadDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 5/25/2010 - 4:21:44 PM
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
    <Serializable(), TableInfo("SPAFDoc_UploadDetail")> _
    Public Class SPAFDoc_UploadDetail
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
        'Private _sPAFDoc_UploadHeaderID As Integer		
        Private _sPAFFType As String = String.Empty
        'Private _dealerID As Short
        Private _status As Short
        Private _docType As Short
        Private _orderDealer As String = String.Empty
        'Private _chassisMasterID As Integer
        Private _postingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _reffLetter As String = String.Empty
        Private _dateLetter As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerName As String = String.Empty
        Private _retailPrice As Decimal
        Private _sPAF As Decimal
        Private _subsidi As Decimal
        Private _tglSetuju As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _uploadFile As String = String.Empty
        Private _uploadBy As String = String.Empty
        Private _uploadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _alasanPenolakan As String = String.Empty
        Private _dealerLeasing As String = String.Empty
        Private _sellingType As Short
        Private _pPhPercent As Decimal
        Private _errorMessage As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sPAFDoc_UploadHeader As SPAFDoc_UploadHeader = New SPAFDoc_UploadHeader
        Private _chassisMaster As ChassisMaster = New ChassisMaster
        Private _dealer As Dealer


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


        '<ColumnInfo("SPAFDoc_UploadHeaderID", "{0}")> _
        'Public Property SPAFDoc_UploadHeaderID() As Integer
        '    Get
        '        Return _sPAFDoc_UploadHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sPAFDoc_UploadHeaderID = value
        '    End Set
        'End Property

        <ColumnInfo("SPAFFType", "'{0}'")> _
        Public Property SPAFFType() As String
            Get
                Return _sPAFFType
            End Get
            Set(ByVal value As String)
                _sPAFFType = value
            End Set
        End Property


        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID() As Short
        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Short)
        '        _dealerID = value
        '    End Set
        'End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("DocType", "{0}")> _
        Public Property DocType() As Short
            Get
                Return _docType
            End Get
            Set(ByVal value As Short)
                _docType = value
            End Set
        End Property


        <ColumnInfo("OrderDealer", "'{0}'")> _
        Public Property OrderDealer() As String
            Get
                Return _orderDealer
            End Get
            Set(ByVal value As String)
                _orderDealer = value
            End Set
        End Property


        '<ColumnInfo("ChassisMasterID", "{0}")> _
        'Public Property ChassisMasterID() As Integer
        '    Get
        '        Return _chassisMasterID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _chassisMasterID = value
        '    End Set
        'End Property


        <ColumnInfo("PostingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PostingDate() As DateTime
            Get
                Return _postingDate
            End Get
            Set(ByVal value As DateTime)
                _postingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ReffLetter", "'{0}'")> _
        Public Property ReffLetter() As String
            Get
                Return _reffLetter
            End Get
            Set(ByVal value As String)
                _reffLetter = value
            End Set
        End Property


        <ColumnInfo("DateLetter", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateLetter() As DateTime
            Get
                Return _dateLetter
            End Get
            Set(ByVal value As DateTime)
                _dateLetter = value
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName() As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("RetailPrice", "{0}")> _
        Public Property RetailPrice() As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
            End Set
        End Property


        <ColumnInfo("SPAF", "{0}")> _
        Public Property SPAF() As Decimal
            Get
                Return _sPAF
            End Get
            Set(ByVal value As Decimal)
                _sPAF = value
            End Set
        End Property


        <ColumnInfo("Subsidi", "{0}")> _
        Public Property Subsidi() As Decimal
            Get
                Return _subsidi
            End Get
            Set(ByVal value As Decimal)
                _subsidi = value
            End Set
        End Property


        <ColumnInfo("TglSetuju", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TglSetuju() As DateTime
            Get
                Return _tglSetuju
            End Get
            Set(ByVal value As DateTime)
                _tglSetuju = value
            End Set
        End Property


        <ColumnInfo("UploadFile", "'{0}'")> _
        Public Property UploadFile() As String
            Get
                Return _uploadFile
            End Get
            Set(ByVal value As String)
                _uploadFile = value
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


        <ColumnInfo("UploadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property UploadDate() As DateTime
            Get
                Return _uploadDate
            End Get
            Set(ByVal value As DateTime)
                _uploadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("AlasanPenolakan", "'{0}'")> _
        Public Property AlasanPenolakan() As String
            Get
                Return _alasanPenolakan
            End Get
            Set(ByVal value As String)
                _alasanPenolakan = value
            End Set
        End Property


        <ColumnInfo("DealerLeasing", "'{0}'")> _
        Public Property DealerLeasing() As String
            Get
                Return _dealerLeasing
            End Get
            Set(ByVal value As String)
                _dealerLeasing = value
            End Set
        End Property


        <ColumnInfo("SellingType", "{0}")> _
        Public Property SellingType() As Short
            Get
                Return _sellingType
            End Get
            Set(ByVal value As Short)
                _sellingType = value
            End Set
        End Property


        <ColumnInfo("PPhPercent", "#,##0")> _
        Public Property PPhPercent() As Decimal
            Get
                Return _pPhPercent
            End Get
            Set(ByVal value As Decimal)
                _pPhPercent = value
            End Set
        End Property


        <ColumnInfo("ErrorMessage", "'{0}'")> _
        Public Property ErrorMessage() As String
            Get
                Return _errorMessage
            End Get
            Set(ByVal value As String)
                _errorMessage = value
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

        <ColumnInfo("SPAFDoc_UploadHeaderID", "{0}"), _
        RelationInfo("SPAFDoc_UploadHeader", "ID", "SPAFDoc_UploadDetail", "SPAFDoc_UploadHeaderID")> _
        Public Property SPAFDoc_UploadHeader() As SPAFDoc_UploadHeader
            Get
                Try
                    If Not IsNothing(Me._sPAFDoc_UploadHeader) AndAlso (Not Me._sPAFDoc_UploadHeader.IsLoaded) Then

                        Me._sPAFDoc_UploadHeader = CType(DoLoad(GetType(SPAFDoc_UploadHeader).ToString(), _sPAFDoc_UploadHeader.ID), SPAFDoc_UploadHeader)
                        Me._sPAFDoc_UploadHeader.MarkLoaded()

                    End If

                    Return Me._sPAFDoc_UploadHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPAFDoc_UploadHeader)

                Me._sPAFDoc_UploadHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPAFDoc_UploadHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "SPAFDoc", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then
                        If Me._chassisMaster.ID < 1 Then
                        Else
                            Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                            Me._chassisMaster.MarkLoaded()
                        End If

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SPAFDoc", "DealerID")> _
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

