#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPL Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 3:41:42 PM
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
    <Serializable(), TableInfo("SPL")> _
    Public Class SPL
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
        Private _sPLNumber As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _customerName As String = String.Empty
        Private _description As String = String.Empty
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _attachment As String = String.Empty
        Private _numOfInstallment As Integer
        Private _maxTOPDay As Integer
        Private _status As Integer
        Private _isAutoApprovedDealer As Short
        Private _approvalStatus As Integer
        Private _finalApproval As Short
        Private _comment As String
        Private _isFromDP As String
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sPLDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sPLDealers As System.Collections.ArrayList = New System.Collections.ArrayList
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


        <ColumnInfo("SPLNumber", "'{0}'")> _
        Public Property SPLNumber() As String
            Get
                Return _sPLNumber
            End Get
            Set(ByVal value As String)
                _sPLNumber = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName() As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
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


        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom() As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property


        <ColumnInfo("ValidTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidTo() As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
            End Set
        End Property


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment() As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property


        <ColumnInfo("NumOfInstallment", "{0}")> _
        Public Property NumOfInstallment() As Integer
            Get
                Return _numOfInstallment
            End Get
            Set(ByVal value As Integer)
                _numOfInstallment = value
            End Set
        End Property

        <ColumnInfo("MaxTOPDay", "{0}")> _
        Public Property MaxTOPDay() As Integer
            Get
                Return _maxTOPDay
            End Get
            Set(ByVal value As Integer)
                _maxTOPDay = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("IsAutoApprovedDealer", "{0}")> _
        Public Property IsAutoApprovedDealer() As Short
            Get
                Return _isAutoApprovedDealer
            End Get
            Set(ByVal value As Short)
                _isAutoApprovedDealer = value
            End Set
        End Property


        <ColumnInfo("ApprovalStatus", "{0}")> _
        Public Property ApprovalStatus() As Integer
            Get
                Return _approvalStatus
            End Get
            Set(ByVal value As Integer)
                _approvalStatus = value
            End Set
        End Property


        <ColumnInfo("FinalApproval", "{0}")> _
        Public Property FinalApproval() As Short
            Get
                Return _finalApproval
            End Get
            Set(ByVal value As Short)
                _finalApproval = value
            End Set
        End Property


        <ColumnInfo("Comment", "'{0}'")> _
        Public Property Comment() As String
            Get
                Return _comment
            End Get
            Set(ByVal value As String)
                _comment = value
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


        <RelationInfo("SPL", "ID", "SPLDetail", "SPLID")> _
        Public ReadOnly Property SPLDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPLDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPLDetail), "SPL", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodMonth", MatchType.Exact, DateTime.Now.Month))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodYear", MatchType.Exact, DateTime.Now.Year))

                        Me._sPLDetails = DoLoadArray(GetType(SPLDetail).ToString, criterias)
                        If (Me._sPLDetails.Count < 1) Then
                            Dim _crit As Criteria = New Criteria(GetType(SPLDetail), "SPL", Me.ID)
                            Dim crits As CriteriaComposite = New CriteriaComposite(_crit)
                            crits.opAnd(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            Me._sPLDetails = DoLoadArray(GetType(SPLDetail).ToString, crits)
                        End If
                    End If

                    Return Me._sPLDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("SPL", "ID", "SPLDealer", "SPLID")> _
        Public ReadOnly Property SPLDealers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPLDealers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPLDealer), "SPL", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPLDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPLDealers = DoLoadArray(GetType(SPLDealer).ToString, criterias)
                    End If

                    Return Me._sPLDealers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("IsFromDP", "{0}")> _
        Public Property IsFromDP() As Short
            Get
                Return _isFromDP
            End Get
            Set(ByVal value As Short)
                _isFromDP = value
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

