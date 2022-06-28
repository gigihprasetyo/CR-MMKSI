#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PaymentRegDoc Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2007 - 2:40:46 PM
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
    <Serializable(), TableInfo("PaymentRegDoc")> _
    Public Class PaymentRegDoc
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
        Private _createTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _ipAddress As String = String.Empty
        Private _bORNumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _userInfo As UserInfo
        Private _dealer As Dealer

        Private _paymentObligations As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("CreateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreateTime() As DateTime
            Get
                Return _createTime
            End Get
            Set(ByVal value As DateTime)
                _createTime = value
            End Set
        End Property


        <ColumnInfo("IpAddress", "'{0}'")> _
        Public Property IpAddress() As String
            Get
                Return _ipAddress
            End Get
            Set(ByVal value As String)
                _ipAddress = value
            End Set
        End Property


        <ColumnInfo("BORNumber", "'{0}'")> _
        Public Property BORNumber() As String
            Get
                Return _bORNumber
            End Get
            Set(ByVal value As String)
                _bORNumber = value
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


        <ColumnInfo("UserInfoID", "{0}"), _
        RelationInfo("UserInfo", "ID", "PaymentRegDoc", "UserInfoID")> _
        Public Property UserInfo() As UserInfo
            Get
                Try
                    If Not isnothing(Me._userInfo) AndAlso (Not Me._userInfo.IsLoaded) Then

                        Me._userInfo = CType(DoLoad(GetType(UserInfo).ToString(), _userInfo.ID), UserInfo)
                        Me._userInfo.MarkLoaded()

                    End If

                    Return Me._userInfo

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As UserInfo)

                Me._userInfo = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._userInfo.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PaymentRegDoc", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("PaymentRegDoc", "ID", "PaymentObligation", "PaymentRegDocID")> _
        Public ReadOnly Property PaymentObligations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._paymentObligations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PaymentObligation), "PaymentRegDoc", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._paymentObligations = DoLoadArray(GetType(PaymentObligation).ToString, criterias)
                    End If

                    Return Me._paymentObligations

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

#End Region

    End Class
End Namespace

