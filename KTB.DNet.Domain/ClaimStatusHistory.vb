#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimStatusHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/19/2007 - 11:03:55 AM
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
    <Serializable(), TableInfo("ClaimStatusHistory")> _
    Public Class ClaimStatusHistory
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
        Private _status As Byte
        Private _newStatus As Byte
        Private _progress As Byte
        Private _oldProgress As Integer
        Private _keterangan As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _claimHeader As ClaimHeader



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
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
            End Set
        End Property


        <ColumnInfo("NewStatus", "{0}")> _
        Public Property NewStatus() As Byte
            Get
                Return _newStatus
            End Get
            Set(ByVal value As Byte)
                _newStatus = value
            End Set
        End Property


        <ColumnInfo("Progress", "{0}")> _
        Public Property Progress() As Byte
            Get
                Return _progress
            End Get
            Set(ByVal value As Byte)
                _progress = value
            End Set
        End Property


        <ColumnInfo("OldProgress", "{0}")> _
        Public Property OldProgress() As Integer
            Get
                Return _oldProgress
            End Get
            Set(ByVal value As Integer)
                _oldProgress = value
            End Set
        End Property


        <ColumnInfo("Keterangan", "'{0}'")> _
        Public Property Keterangan() As String
            Get
                Return _keterangan
            End Get
            Set(ByVal value As String)
                _keterangan = value
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


        <ColumnInfo("ClaimHeaderID", "{0}"), _
        RelationInfo("ClaimHeader", "ID", "ClaimStatusHistory", "ClaimHeaderID")> _
        Public Property ClaimHeader() As ClaimHeader
            Get
                Try
                    If Not isnothing(Me._claimHeader) AndAlso (Not Me._claimHeader.IsLoaded) Then

                        Me._claimHeader = CType(DoLoad(GetType(ClaimHeader).ToString(), _claimHeader.ID), ClaimHeader)
                        Me._claimHeader.MarkLoaded()

                    End If

                    Return Me._claimHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ClaimHeader)

                Me._claimHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimHeader.MarkLoaded()
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

