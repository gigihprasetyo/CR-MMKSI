#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPAFDocHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2006 - 11:47:52 AM
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
    <Serializable(), TableInfo("SPAFDocHistory")> _
    Public Class SPAFDocHistory
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
        Private _status As Short
        Private _processBy As String = String.Empty
        Private _processDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sPAFDocID As Integer

        Private _sPAFDoc As SPAFDoc



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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("ProcessBy", "'{0}'")> _
        Public Property ProcessBy() As String
            Get
                Return _processBy
            End Get
            Set(ByVal value As String)
                _processBy = value
            End Set
        End Property


        <ColumnInfo("ProcessDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ProcessDate() As DateTime
            Get
                Return _processDate
            End Get
            Set(ByVal value As DateTime)
                _processDate = New DateTime(value.Year, value.Month, value.Day)
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

        <ColumnInfo("SPAFDocID", "{0}")> _
        Public Property SPAFDocID() As Integer
            Get
                Return _sPAFDocID
            End Get
            Set(ByVal Value As Integer)
                _sPAFDocID = Value
            End Set
        End Property

        <ColumnInfo("SPAFDocID", "{0}"), _
        RelationInfo("SPAFDoc", "ID", "SPAFDocHistory", "SPAFDocID")> _
        Public Property SPAFDoc() As SPAFDoc
            Get
                Try
                    If IsNothing(Me._sPAFDoc) Then

                        Me._sPAFDoc = CType(DoLoad(GetType(SPAFDoc).ToString(), _sPAFDocID), SPAFDoc)
                        Me._sPAFDoc.MarkLoaded()

                    End If

                    Return Me._sPAFDoc

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPAFDoc)

                Me._sPAFDoc = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPAFDoc.MarkLoaded()
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
        Private _OldStatus As String
        Public Property OldStatus() As String
            Get
                Return _OldStatus
            End Get
            Set(ByVal Value As String)
                _OldStatus = Value
            End Set
        End Property
        Public ReadOnly Property StatusDesc() As String
            Get
                Return CType(_status, EnumSPAFSubsidy.SPAFDocStatus).ToString
            End Get
        End Property
#End Region

    End Class
End Namespace

