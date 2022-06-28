
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventCategoryDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 14/05/2019 - 15:51:14
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
    <Serializable(), TableInfo("EventCategoryDetail")> _
    Public Class EventCategoryDetail
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
        Private _documentType As Integer
        Private _documentName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _EventDealerHeaderID As Integer
        Private _eventDealerHeader As EventDealerHeader



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


        <ColumnInfo("DocumentType", "{0}")> _
        Public Property DocumentType As Integer
            Get
                Return _documentType
            End Get
            Set(ByVal value As Integer)
                _documentType = value
            End Set
        End Property


        <ColumnInfo("DocumentName", "'{0}'")> _
        Public Property DocumentName As String
            Get
                Return _documentName
            End Get
            Set(ByVal value As String)
                _documentName = value
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


        '<ColumnInfo("EventDealerHeaderID", "{0}")> _
        'Public Property EventDealerHeaderID As Integer

        '    Get
        '        Return _EventDealerHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _EventDealerHeaderID = value
        '    End Set
        'End Property

        <ColumnInfo("EventDealerHeaderID", "{0}"), _
        RelationInfo("EventDealerHeader", "ID", "EventCategoryDetail", "EventDealerHeaderID")> _
        Public Property EventDealerHeader() As EventDealerHeader
            Get
                Try
                    If Not IsNothing(Me._eventDealerHeader) AndAlso (Not Me._eventDealerHeader.IsLoaded) Then

                        Me._eventDealerHeader = CType(DoLoad(GetType(EventDealerHeader).ToString(), _eventDealerHeader.ID), EventDealerHeader)
                        Me._eventDealerHeader.MarkLoaded()

                    End If

                    Return Me._eventDealerHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
            Set(ByVal Value As EventDealerHeader)
                _eventDealerHeader = Value
                If (Not IsNothing(Value)) AndAlso (CType(Value, DomainObject)).IsLoaded Then
                    Me._eventDealerHeader.MarkLoaded()
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

