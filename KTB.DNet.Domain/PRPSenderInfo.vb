#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PRPSenderInfo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/23/2006 - 11:35:01 AM
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
    <Serializable(), TableInfo("PRPSenderInfo")> _
    Public Class PRPSenderInfo
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
        Private _pIC As String = String.Empty
        Private _filename As String = String.Empty
        Private _description As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _pRPReceiverInfos As System.Collections.ArrayList = New System.Collections.ArrayList

        Enum EnumSendStatus
            Baru
            Sukses
            Gagal
        End Enum
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


        <ColumnInfo("PIC", "'{0}'")> _
        Public Property PIC() As String
            Get
                Return _pIC
            End Get
            Set(ByVal value As String)
                _pIC = value
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


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
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





        <RelationInfo("PRPSenderInfo", "ID", "PRPReceiverInfo", "PRPSenderInfoID")> _
        Public ReadOnly Property PRPReceiverInfos() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pRPReceiverInfos.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PRPReceiverInfo), "PRPSenderInfo", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PRPReceiverInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pRPReceiverInfos = DoLoadArray(GetType(PRPReceiverInfo).ToString, criterias)
                    End If

                    Return Me._pRPReceiverInfos

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

#Region "Custom Method"
        Public Sub SuccessSend()
            _status = EnumSendStatus.Sukses
        End Sub

        Public Sub FailSend()
            _status = EnumSendStatus.Gagal
        End Sub

        Public Function GetEmailList() As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
            For Each receiver As PRPReceiverInfo In _pRPReceiverInfos
                If receiver.PRPUserEmail.Tipe = "TO" Then
                    sb.Append(receiver.PRPUserEmail.Email).Append(";")
                End If
            Next
            Return sb.ToString()
        End Function

        Public Function GetCCEmailList() As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
            For Each receiver As PRPReceiverInfo In _pRPReceiverInfos
                If receiver.PRPUserEmail.Tipe = "CC" Then
                    sb.Append(receiver.PRPUserEmail.Email).Append(";")
                End If
            Next
            Return sb.ToString()
        End Function
#End Region

    End Class
End Namespace

