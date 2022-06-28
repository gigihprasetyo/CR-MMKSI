#Region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : InterestPPHHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2021 - 10:54:55 AM
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
    <Serializable(), TableInfo("InterestPPHHeader")> _
    Public Class InterestPPHHeader
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
        Private _noReg As String = String.Empty
        Private _witholdingNumber As String = String.Empty
        Private _witholdingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        'Private _dedalerID As Integer
        Private _taxPeriod As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerTaxName As String = String.Empty
        Private _dealerSignatureName As String = String.Empty
        Private _totalDPPAmount As Decimal
        Private _totalPPHAmount As Decimal
        Private _description As String = String.Empty
        Private _taxInformation As String = String.Empty
        Private _remark As String = String.Empty
        Private _jVReturn As String = String.Empty
        Private _submissionStatus As Short
        Private _evidencePDFName As String = String.Empty
        Private _evidencePDFPath As String = String.Empty
        Private _referenceDocName As String = String.Empty
        Private _referenceDocPath As String = String.Empty
        Private _referenceDocDescription As String = String.Empty
        Private _referenceDocType As String = String.Empty
        Private _referenceDocNumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _dealerNPWP As String = String.Empty

        Private _interestPPHDetails As ArrayList = New ArrayList()
        Private _interestPPHDetailNew As ArrayList = New ArrayList()
        Private _PPHSubmissionDetail As ArrayList = New ArrayList()
        Private _pembetulanKe As Short



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


        <ColumnInfo("NoReg", "'{0}'")> _
        Public Property NoReg As String
            Get
                Return _noReg
            End Get
            Set(ByVal value As String)
                _noReg = value
            End Set
        End Property


        <ColumnInfo("WitholdingNumber", "'{0}'")> _
        Public Property WitholdingNumber As String
            Get
                Return _witholdingNumber
            End Get
            Set(ByVal value As String)
                _witholdingNumber = value
            End Set
        End Property


        <ColumnInfo("WitholdingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property WitholdingDate As DateTime
            Get
                Return _witholdingDate
            End Get
            Set(ByVal value As DateTime)
                _witholdingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        '<ColumnInfo("DedalerID", "{0}")> _
        'Public Property DedalerID As Integer
        '    Get
        '        Return _dedalerID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _dedalerID = value
        '    End Set
        'End Property


        <ColumnInfo("TaxPeriod", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TaxPeriod As DateTime
            Get
                Return _taxPeriod
            End Get
            Set(ByVal value As DateTime)
                _taxPeriod = value
            End Set
        End Property


        <ColumnInfo("DealerTaxName", "'{0}'")> _
        Public Property DealerTaxName As String
            Get
                Return _dealerTaxName
            End Get
            Set(ByVal value As String)
                _dealerTaxName = value
            End Set
        End Property


        <ColumnInfo("DealerSignatureName", "'{0}'")> _
        Public Property DealerSignatureName As String
            Get
                Return _dealerSignatureName
            End Get
            Set(ByVal value As String)
                _dealerSignatureName = value
            End Set
        End Property


        <ColumnInfo("TotalDPPAmount", "{0}")> _
        Public Property TotalDPPAmount As Decimal
            Get
                Return _totalDPPAmount
            End Get
            Set(ByVal value As Decimal)
                _totalDPPAmount = value
            End Set
        End Property


        <ColumnInfo("TotalPPHAmount", "{0}")> _
        Public Property TotalPPHAmount As Decimal
            Get
                Return _totalPPHAmount
            End Get
            Set(ByVal value As Decimal)
                _totalPPHAmount = value
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


        <ColumnInfo("TaxInformation", "'{0}'")> _
        Public Property TaxInformation As String
            Get
                Return _taxInformation
            End Get
            Set(ByVal value As String)
                _taxInformation = value
            End Set
        End Property


        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
            End Set
        End Property


        <ColumnInfo("JVReturn", "'{0}'")> _
        Public Property JVReturn As String
            Get
                Return _jVReturn
            End Get
            Set(ByVal value As String)
                _jVReturn = value
            End Set
        End Property


        <ColumnInfo("SubmissionStatus", "{0}")> _
        Public Property SubmissionStatus As Short
            Get
                Return _submissionStatus
            End Get
            Set(ByVal value As Short)
                _submissionStatus = value
            End Set
        End Property


        <ColumnInfo("EvidencePDFName", "'{0}'")> _
        Public Property EvidencePDFName As String
            Get
                Return _evidencePDFName
            End Get
            Set(ByVal value As String)
                _evidencePDFName = value
            End Set
        End Property


        <ColumnInfo("EvidencePDFPath", "{0}")> _
        Public Property EvidencePDFPath As String
            Get
                Return _evidencePDFPath
            End Get
            Set(ByVal value As String)
                _evidencePDFPath = value
            End Set
        End Property


        <ColumnInfo("ReferenceDocName", "'{0}'")> _
        Public Property ReferenceDocName As String
            Get
                Return _referenceDocName
            End Get
            Set(ByVal value As String)
                _referenceDocName = value
            End Set
        End Property


        <ColumnInfo("ReferenceDocPath", "'{0}'")> _
        Public Property ReferenceDocPath As String
            Get
                Return _referenceDocPath
            End Get
            Set(ByVal value As String)
                _referenceDocPath = value
            End Set
        End Property


        '<ColumnInfo("EvidenceDocDescription", "'{0}'")> _
        'Public Property EvidenceDocDescription As String
        '    Get
        '        Return _evidenceDocDescription
        '    End Get
        '    Set(ByVal value As String)
        '        _evidenceDocDescription = value
        '    End Set
        'End Property


        <ColumnInfo("ReferenceDocType", "'{0}'")> _
        Public Property ReferenceDocType As String
            Get
                Return _referenceDocType
            End Get
            Set(ByVal value As String)
                _referenceDocType = value
            End Set
        End Property


        <ColumnInfo("ReferenceDocNumber", "'{0}'")> _
        Public Property ReferenceDocNumber As String
            Get
                Return _referenceDocNumber
            End Get
            Set(ByVal value As String)
                _referenceDocNumber = value
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
       RelationInfo("Dealer", "ID", "InterestPPHHeader", "DealerID")> _
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


        <ColumnInfo("DealerNPWP", "'{0}'")> _
        Public Property DealerNPWP As String
            Get
                Return _dealerNPWP
            End Get
            Set(ByVal value As String)
                _dealerNPWP = value
            End Set
        End Property




        <RelationInfo("InterestPPHHeader", "ID", "InterestPPHDetail", "InterestPPHHeaderID")> _
        Public ReadOnly Property PPHSubmissionDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._PPHSubmissionDetail.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(InterestPPHDetail), "InterestPPHHeader.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(InterestPPHDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._PPHSubmissionDetail = DoLoadArray(GetType(InterestPPHDetail).ToString, criterias)
                    End If

                    Return Me._PPHSubmissionDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        Public Property InterestPPHDetails As ArrayList
            Get
                Return _interestPPHDetails
            End Get
            Set(value As ArrayList)
                _interestPPHDetails = value
            End Set
        End Property


        Public Property InterestPPHDetailsNew As ArrayList
            Get
                Return _interestPPHDetailNew
            End Get
            Set(value As ArrayList)
                _interestPPHDetailNew = value
            End Set
        End Property


        <ColumnInfo("PembetulanKe", "{0}")> _
        Public Property PembetulanKe As Short
            Get
                Return _pembetulanKe
            End Get
            Set(ByVal value As Short)
                _pembetulanKe = value
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
