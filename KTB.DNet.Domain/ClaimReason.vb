#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimReason Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/19/2007 - 9:32:16 AM
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
    <Serializable(), TableInfo("ClaimReason")> _
    Public Class ClaimReason
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
        Private _code As String = String.Empty
        Private _isHeader As Short
        Private _reason As String = String.Empty
        Private _timeLimit As Integer
        Private _status As Short
        Private _incharge As String = String.Empty
        Private _prerequisite As String = String.Empty
        Private _isMandatoryUpload As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _claimHeaders As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("IsHeader", "{0}")> _
        Public Property IsHeader() As Short
            Get
                Return _isHeader
            End Get
            Set(ByVal value As Short)
                _isHeader = value
            End Set
        End Property


        <ColumnInfo("Reason", "'{0}'")> _
        Public Property Reason() As String
            Get
                Return _reason
            End Get
            Set(ByVal value As String)
                _reason = value
            End Set
        End Property


        <ColumnInfo("TimeLimit", "{0}")> _
        Public Property TimeLimit() As Integer
            Get
                Return _timeLimit
            End Get
            Set(ByVal value As Integer)
                _timeLimit = value
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


        <ColumnInfo("incharge", "'{0}'")> _
        Public Property incharge() As String
            Get
                Return _incharge
            End Get
            Set(ByVal value As String)
                _incharge = value
            End Set
        End Property


        <ColumnInfo("Prerequisite", "'{0}'")> _
        Public Property Prerequisite() As String
            Get
                Return _prerequisite
            End Get
            Set(ByVal value As String)
                _prerequisite = value
            End Set
        End Property


        <ColumnInfo("IsMandatoryUpload", "{0}")> _
        Public Property IsMandatoryUpload() As Byte
            Get
                Return _isMandatoryUpload
            End Get
            Set(ByVal value As Byte)
                _isMandatoryUpload = value
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



        <RelationInfo("ClaimReason", "ID", "ClaimHeader", "ClaimReasonHeaderID")> _
        Public ReadOnly Property ClaimHeaders() As System.Collections.ArrayList
            Get
                Try
                    If (Me._claimHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ClaimHeader), "ClaimReason", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._claimHeaders = DoLoadArray(GetType(ClaimHeader).ToString, criterias)
                    End If

                    Return Me._claimHeaders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

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
        Public ReadOnly Property StatusDesc() As String
            Get
                If Me.Status = 0 Then
                    Return "Tidak Aktif"
                ElseIf Me.Status = 1 Then
                    Return "Aktif"
                End If
            End Get
        End Property
#End Region

    End Class
End Namespace

