#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PaymentAssignmentType Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/25/2007 - 8:48:10 AM
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
    <Serializable(), TableInfo("PaymentAssignmentType")> _
    Public Class PaymentAssignmentType
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
        Private _description As String = String.Empty
        Private _status As Integer
        Private _sourceDocument As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _paymentObligationType As PaymentObligationType

        Private _paymentObligations As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _paymentAssignmentTypeReffs As System.Collections.ArrayList = New System.Collections.ArrayList


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
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("SourceDocument", "{0}")> _
        Public Property SourceDocument() As Integer
            Get
                Return _sourceDocument
            End Get
            Set(ByVal value As Integer)
                _sourceDocument = value
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


        <ColumnInfo("PaymentObligationTypeID", "{0}"), _
        RelationInfo("PaymentObligationType", "ID", "PaymentAssignmentType", "PaymentObligationTypeID")> _
        Public Property PaymentObligationType() As PaymentObligationType
            Get
                Try
                    If Not isnothing(Me._paymentObligationType) AndAlso (Not Me._paymentObligationType.IsLoaded) Then

                        Me._paymentObligationType = CType(DoLoad(GetType(PaymentObligationType).ToString(), _paymentObligationType.ID), PaymentObligationType)
                        Me._paymentObligationType.MarkLoaded()

                    End If

                    Return Me._paymentObligationType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PaymentObligationType)

                Me._paymentObligationType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._paymentObligationType.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("PaymentAssignmentType", "ID", "PaymentObligation", "PaymentAssignmentTypeID")> _
        Public ReadOnly Property PaymentObligations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._paymentObligations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PaymentObligation), "PaymentAssignmentType", Me.ID)
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

        <RelationInfo("PaymentAssignmentType", "ID", "PaymentAssignmentTypeReff", "PaymentAssignmentTypeID")> _
        Public ReadOnly Property PaymentAssignmentTypeReffs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._paymentAssignmentTypeReffs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PaymentAssignmentTypeReff), "PaymentAssignmentType", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PaymentAssignmentTypeReff), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._paymentAssignmentTypeReffs = DoLoadArray(GetType(PaymentAssignmentTypeReff).ToString, criterias)
                    End If

                    Return Me._paymentAssignmentTypeReffs

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

