
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportDocument Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 27/05/2019 - 9:50:46
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
    <Serializable(), TableInfo("BabitEventReportDocument")> _
    Public Class BabitEventReportDocument
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
        Private _fileName As String = String.Empty
        Private _fileDescription As String = String.Empty
        Private _path As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _AttachmentData As System.Web.HttpPostedFile

        Private _babitEventReportHeader As BabitEventReportHeader
        Private _eventDealerRequiredDocument As eventDealerRequiredDocument

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


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property


        <ColumnInfo("FileDescription", "'{0}'")> _
        Public Property FileDescription As String
            Get
                Return _fileDescription
            End Get
            Set(ByVal value As String)
                _fileDescription = value
            End Set
        End Property


        <ColumnInfo("Path", "'{0}'")> _
        Public Property Path As String
            Get
                Return _path
            End Get
            Set(ByVal value As String)
                _path = value
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


        <ColumnInfo("BabitEventReportHeaderID", "{0}"), _
        RelationInfo("BabitEventReportHeader", "ID", "BabitEventReportDocument", "BabitEventReportHeaderID")> _
        Public Property BabitEventReportHeader As BabitEventReportHeader
            Get
                Try
                    If Not IsNothing(Me._babitEventReportHeader) AndAlso (Not Me._babitEventReportHeader.IsLoaded) Then

                        Me._babitEventReportHeader = CType(DoLoad(GetType(BabitEventReportHeader).ToString(), _babitEventReportHeader.ID), BabitEventReportHeader)
                        Me._babitEventReportHeader.MarkLoaded()

                    End If

                    Return Me._babitEventReportHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitEventReportHeader)

                Me._babitEventReportHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitEventReportHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("EventDealerRequiredDocumentID", "{0}"), _
        RelationInfo("EventDealerRequiredDocument", "ID", "BabitEventReportDocument", "EventDealerRequiredDocumentID")> _
        Public Property EventDealerRequiredDocument As EventDealerRequiredDocument
            Get
                Try
                    If Not IsNothing(Me._eventDealerRequiredDocument) AndAlso (Not Me._eventDealerRequiredDocument.IsLoaded) Then

                        Me._eventDealerRequiredDocument = CType(DoLoad(GetType(EventDealerRequiredDocument).ToString(), _eventDealerRequiredDocument.ID), EventDealerRequiredDocument)
                        Me._eventDealerRequiredDocument.MarkLoaded()

                    End If

                    Return Me._eventDealerRequiredDocument

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EventDealerRequiredDocument)

                Me._eventDealerRequiredDocument = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._eventDealerRequiredDocument.MarkLoaded()
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
        Public Property AttachmentData() As System.Web.HttpPostedFile
            Get
                Return _AttachmentData
            End Get

            Set(ByVal value As System.Web.HttpPostedFile)
                _AttachmentData = value
            End Set
        End Property

#End Region

    End Class
End Namespace

