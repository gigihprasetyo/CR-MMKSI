
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitEventHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:58:25 AM
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
    <Serializable(), TableInfo("BenefitEventHeader")> _
    Public Class BenefitEventHeader
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
        Private _eventRegNo As String = String.Empty
        Private _eventName As String = String.Empty
        Private _eventDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _benefitMasterHeader As BenefitMasterHeader

        Private _dealer As Dealer

        Private _benefitEventDetails As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _benefitClaimHeaders As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("EventRegNo", "'{0}'")> _
        Public Property EventRegNo As String
            Get
                Return _eventRegNo
            End Get
            Set(ByVal value As String)
                _eventRegNo = value
            End Set
        End Property


        <ColumnInfo("EventName", "'{0}'")> _
        Public Property EventName As String
            Get
                Return _eventName
            End Get
            Set(ByVal value As String)
                _eventName = value
            End Set
        End Property


        <ColumnInfo("EventDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EventDate As DateTime
            Get
                Return _eventDate
            End Get
            Set(ByVal value As DateTime)
                _eventDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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

        <ColumnInfo("DealerID", "{0}"), _
  RelationInfo("Dealer", "ID", "BenefitEventHeader", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("BenefitMasterHeaderID", "{0}"), _
        RelationInfo("BenefitMasterHeader", "ID", "BenefitEventHeader", "BenefitMasterHeaderID")> _
        Public Property BenefitMasterHeader As BenefitMasterHeader
            Get
                Try
                    If Not isnothing(Me._benefitMasterHeader) AndAlso (Not Me._benefitMasterHeader.IsLoaded) Then

                        Me._benefitMasterHeader = CType(DoLoad(GetType(BenefitMasterHeader).ToString(), _benefitMasterHeader.ID), BenefitMasterHeader)
                        Me._benefitMasterHeader.MarkLoaded()

                    End If

                    Return Me._benefitMasterHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitMasterHeader)

                Me._benefitMasterHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitMasterHeader.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("BenefitEventHeader", "ID", "BenefitEventDetail", "BenefitEventHeaderID")> _
        Public ReadOnly Property BenefitEventDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitEventDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitEventDetail), "BenefitEventHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitEventDetails = DoLoadArray(GetType(BenefitEventDetail).ToString, criterias)
                    End If

                    Return Me._benefitEventDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BenefitEventHeader", "ID", "BenefitClaimHeader", "BenefitEventHeaderID")> _
        Public ReadOnly Property BenefitClaimHeaders As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitClaimHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitClaimHeader), "BenefitEventHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitClaimHeaders = DoLoadArray(GetType(BenefitClaimHeader).ToString, criterias)
                    End If

                    Return Me._benefitClaimHeaders

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

