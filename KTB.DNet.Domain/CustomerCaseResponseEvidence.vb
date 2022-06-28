
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerCaseResponseEvidence Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 7/10/2017 - 10:10:16 AM
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
    <Serializable(), TableInfo("CustomerCaseResponseEvidence")> _
    Public Class CustomerCaseResponseEvidence
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
        Private _evidenceFile As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTIme As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _customerCaseResponse As CustomerCaseResponse
        Private _AttachmentData As System.Web.HttpPostedFile
        Private _FileName As String
        Private _attachment As String = String.Empty
        Private _newItem As Boolean = False
        Private _isDeleted As Boolean = False

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

        <ColumnInfo("EvidenceFile", "'{0}'")> _
        Public Property EvidenceFile As String
            Get
                Return _evidenceFile
            End Get
            Set(ByVal value As String)
                _evidenceFile = value
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


        <ColumnInfo("LastUpdateTIme", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTIme As DateTime
            Get
                Return _lastUpdateTIme
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTIme = value
            End Set
        End Property


        <ColumnInfo("CustomerCaseResponseID", "{0}"), _
        RelationInfo("CustomerCaseResponse", "ID", "CustomerCaseResponseEvidence", "CustomerCaseResponseID")> _
        Public Property CustomerCaseResponse As CustomerCaseResponse
            Get
                Try
                    If Not IsNothing(Me._customerCaseResponse) AndAlso (Not Me._customerCaseResponse.IsLoaded) Then

                        Me._customerCaseResponse = CType(DoLoad(GetType(CustomerCaseResponse).ToString(), _customerCaseResponse.ID), CustomerCaseResponse)
                        Me._customerCaseResponse.MarkLoaded()

                    End If

                    Return Me._customerCaseResponse

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CustomerCaseResponse)

                Me._customerCaseResponse = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._customerCaseResponse.MarkLoaded()
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

        Public Property Attachment() As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property

        Public Property AttachmentData() As System.Web.HttpPostedFile
            Get
                Return _AttachmentData
            End Get

            Set(ByVal value As System.Web.HttpPostedFile)
                _AttachmentData = value
            End Set
        End Property

        Public ReadOnly Property FileName() As String
            Get
                Dim len As Integer

                _FileName = ""
                If _attachment <> String.Empty Then
                    len = _attachment.Split("\").Length
                    _FileName = _attachment.Split("\")(0)
                    len = _FileName.Split("_").Length
                    _FileName = _FileName.Split("_")(0)
                    Return _FileName
                End If

                Return _FileName
            End Get

        End Property

        Public Property NewItem() As Boolean
            Get
                Return _newItem
            End Get

            Set(ByVal value As Boolean)
                _newItem = value
            End Set
        End Property


        Public Property isDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
            Set(ByVal Value As Boolean)
                _isDeleted = Value
            End Set
        End Property
#End Region

    End Class
End Namespace

