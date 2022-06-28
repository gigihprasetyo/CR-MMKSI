
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPAFDoc_UploadHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 5/25/2010 - 4:19:07 PM
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
    <Serializable(), TableInfo("SPAFDoc_UploadHeader")> _
    Public Class SPAFDoc_UploadHeader
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
        Private _filename As String = String.Empty
        'Private _dealerID As Short
        Private _numberOfData As Integer
        Private _numberOfValid As Integer
        Private _numberOfError As Integer
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _sPAFDoc_UploadDetails As New ArrayList
        Private _sPAFDocs As New ArrayList

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


        <ColumnInfo("Filename", "'{0}'")> _
        Public Property Filename() As String
            Get
                Return _filename
            End Get
            Set(ByVal value As String)
                _filename = value
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


        <ColumnInfo("NumberOfData", "{0}")> _
        Public Property NumberOfData() As Integer
            Get
                Return _numberOfData
            End Get
            Set(ByVal value As Integer)
                _numberOfData = value
            End Set
        End Property


        <ColumnInfo("NumberOfValid", "{0}")> _
        Public Property NumberOfValid() As Integer
            Get
                Return _numberOfValid
            End Get
            Set(ByVal value As Integer)
                _numberOfValid = value
            End Set
        End Property


        <ColumnInfo("NumberOfError", "{0}")> _
        Public Property NumberOfError() As Integer
            Get
                Return _numberOfError
            End Get
            Set(ByVal value As Integer)
                _numberOfError = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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
        RelationInfo("Dealer", "ID", "SPAFDoc_UploadHeader", "DealerID")> _
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

        <RelationInfo("SPAFDoc_UploadHeader", "ID", "SPAFDoc_UploadDetail", "SPAFDoc_UploadHeaderID")> _
        Public ReadOnly Property SPAFDoc_UploadDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPAFDoc_UploadDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPAFDoc_UploadDetail), "SPAFDoc_UploadHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPAFDoc_UploadDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPAFDoc_UploadDetails = DoLoadArray(GetType(SPAFDoc_UploadDetail).ToString, criterias)
                    End If

                    Return Me._sPAFDoc_UploadDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        Public ReadOnly Property SPAFDocs() As ArrayList
            Get
                Dim objSD As SPAFDoc

                _sPAFDocs = New ArrayList
                For Each oSDUD As SPAFDoc_UploadDetail In Me.SPAFDoc_UploadDetails
                    objSD = New SPAFDoc
                    With objSD
                        .AlasanPenolakan = oSDUD.AlasanPenolakan
                        .ChassisMaster = oSDUD.ChassisMaster
                        .CreatedBy = oSDUD.CreatedBy
                        .CreatedTime = oSDUD.CreatedTime
                        .CustomerName = oSDUD.CustomerName
                        .DateLetter = oSDUD.DateLetter
                        .Dealer = oSDUD.Dealer
                        .DealerLeasing = oSDUD.DealerLeasing
                        .DocType = oSDUD.DocType
                        .LastUpdateBy = oSDUD.LastUpdateBy
                        .LastUpdateTime = oSDUD.LastUpdateTime
                        .OrderDealer = oSDUD.OrderDealer
                        .PostingDate = oSDUD.PostingDate
                        .PPhPercent = oSDUD.PPhPercent
                        .ReffLetter = oSDUD.ReffLetter
                        .RetailPrice = oSDUD.RetailPrice
                        .RowStatus = oSDUD.RowStatus
                        .SellingType = oSDUD.SellingType
                        .SPAF = oSDUD.SPAF
                        '.SPAFDoc_UploadHeader = oSDUH
                        .SPAFFType = oSDUD.SPAFFType
                        .Status = oSDUD.Status
                        .Subsidi = oSDUD.Subsidi
                        .TglSetuju = oSDUD.TglSetuju
                        .UploadBy = oSDUD.UploadBy
                        .UploadDate = oSDUD.UploadDate
                        .UploadFile = oSDUD.UploadFile
                        .ErrorMessage = oSDUD.ErrorMessage
                    End With
                    _sPAFDocs.Add(objSD)
                Next
                Return _sPAFDocs
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

