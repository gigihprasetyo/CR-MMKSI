
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MonthlyDocumentToFakturEvidance Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 29/07/2019 - 9:48:55
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
    <Serializable(), TableInfo("MonthlyDocumentToFakturEvidance")> _
    Public Class MonthlyDocumentToFakturEvidance
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
        Private _monthlyDocumentID As Integer
        Private _fakturNumber As String = String.Empty
        Private _uploadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _paymentDescription As String = String.Empty
        Private _evidancePath As String = String.Empty
        Private _fileNamePath As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _planningTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _monthlyDocument As MonthlyDocument


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


        <ColumnInfo("MonthlyDocumentID", "{0}")> _
        Public Property MonthlyDocumentID As Integer
            Get
                Return _monthlyDocumentID
            End Get
            Set(ByVal value As Integer)
                _monthlyDocumentID = value
            End Set
        End Property


        <ColumnInfo("FakturNumber", "'{0}'")> _
        Public Property FakturNumber As String
            Get
                Return _fakturNumber
            End Get
            Set(ByVal value As String)
                _fakturNumber = value
            End Set
        End Property

        <ColumnInfo("PlanningTransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanningTransferDate As DateTime
            Get
                Return _planningTransferDate
            End Get
            Set(ByVal value As DateTime)
                _planningTransferDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("UploadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property UploadDate As DateTime
            Get
                Return _uploadDate
            End Get
            Set(ByVal value As DateTime)
                _uploadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PaymentDescription", "'{0}'")> _
        Public Property PaymentDescription As String
            Get
                Return _paymentDescription
            End Get
            Set(ByVal value As String)
                _paymentDescription = value
            End Set
        End Property


        <ColumnInfo("EvidancePath", "'{0}'")> _
        Public Property EvidancePath As String
            Get
                Return _evidancePath
            End Get
            Set(ByVal value As String)
                _evidancePath = value
            End Set
        End Property

        <ColumnInfo("FileNamePath", "'{0}'")> _
        Public Property FileNamePath As String
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

        <ColumnInfo("MonthlyDocumentID", "{0}"), _
        RelationInfo("MonthlyDocument", "ID", "MonthlyDocumentToFakturEvidance", "MonthlyDocumentID")> _
        Public Property MonthlyDocument As MonthlyDocument
            Get
                Try
                    If Not IsNothing(Me._monthlyDocument) AndAlso (Not Me._monthlyDocument.IsLoaded) Then

                        Me._monthlyDocument = CType(DoLoad(GetType(MonthlyDocument).ToString(), _monthlyDocument.id), MonthlyDocument)
                        Me._monthlyDocument.MarkLoaded()

                    End If

                    Return Me._monthlyDocument

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MonthlyDocument)

                Me._monthlyDocument = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._monthlyDocument.MarkLoaded()
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

