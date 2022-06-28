
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LogisticDCHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 9/11/2017 - 10:22:30 AM
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
    <Serializable(), TableInfo("LogisticDCHeader")> _
    Public Class LogisticDCHeader
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
        Private _debitChargeNo As String = String.Empty
        Private _totalLogisticCost As Decimal

        Private _dCType As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _logisticDCDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _poDestinationid As PODestination

        Private _salesOrder As SalesOrder
        Private _POBlockedStatus As String
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


        <ColumnInfo("DebitChargeNo", "'{0}'")> _
        Public Property DebitChargeNo As String
            Get
                Return _debitChargeNo
            End Get
            Set(ByVal value As String)
                _debitChargeNo = value
            End Set
        End Property


        <ColumnInfo("TotalLogisticCost", "{0}")> _
        Public Property TotalLogisticCost As Decimal
            Get
                Return _totalLogisticCost
            End Get
            Set(ByVal value As Decimal)
                _totalLogisticCost = value
            End Set
        End Property

        <ColumnInfo("DCType", "'{0}'")> _
        Public Property DCType As String
            Get
                Return _dCType
            End Get
            Set(ByVal value As String)
                _dCType = value
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

        <ColumnInfo("PODestinationID", "{0}"), _
        RelationInfo("PODestination", "ID", "LogisticDCHeader", "PODestinationID")> _
        Public Property PODestination() As PODestination
            Get
                Try
                    If Not IsNothing(Me._poDestinationid) AndAlso (Not Me._poDestinationid.IsLoaded) Then

                        Me._poDestinationid = CType(DoLoad(GetType(PODestination).ToString(), _poDestinationid.ID), PODestination)
                        Me._poDestinationid.MarkLoaded()

                    End If

                    Return Me._poDestinationid

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PODestination)

                Me._poDestinationid = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._poDestinationid.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("LogisticDCHeader", "ID", "LogisticDCDetail", "LogisticDCHeaderID")> _
        Public ReadOnly Property LogisticDCDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._logisticDCDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(LogisticDCDetail), "LogisticDCHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(LogisticDCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._logisticDCDetails = DoLoadArray(GetType(LogisticDCDetail).ToString, criterias)
                    End If

                    Return Me._logisticDCDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        Public Property SalesOrder As SalesOrder
            Get
                Return _salesOrder
            End Get
            Set(ByVal value As SalesOrder)
                _salesOrder = value
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

        Public Property POBlockedStatus As String
            Get
                Return _POBlockedStatus
            End Get
            Set(value As String)
                _POBlockedStatus = value
            End Set
        End Property

        Private _LogisticDN As LogisticDN
        Public ReadOnly Property LogisticDN() As LogisticDN
            Get
                If IsNothing(_LogisticDN) Then
                    Dim aLDNs As ArrayList
                    Dim cLDN As New CriteriaComposite(New Criteria(GetType(LogisticDN), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    cLDN.opAnd(New Criteria(GetType(LogisticDN), "LogisticDCHeader.ID", MatchType.Exact, Me.ID))
                    'cLDN.opAnd(New Criteria(GetType(LogisticDN), "", MatchType.Exact, ""))

                    aLDNs = DoLoadArray(GetType(LogisticDN).ToString(), cLDN)
                    If aLDNs.Count > 0 Then
                        _LogisticDN = aLDNs(0)
                    Else
                        _LogisticDN = New LogisticDN
                    End If
                End If

                Return _LogisticDN
            End Get
        End Property
#End Region

    End Class
End Namespace

