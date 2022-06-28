
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOStatus Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/29/2015 - 1:39:55 PM
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
    <Serializable(), TableInfo("SparePartPOStatus")> _
    Public Class SparePartPOStatus
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
        Private _sONumber As String = String.Empty
        Private _sODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _packingStatus As String = String.Empty
        Private _billingNumber As String = String.Empty
        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _deliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _documentType As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartPO As SparePartPO

        Private _sparePartPOStatusDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property


        <ColumnInfo("SODate", "'{0:yyyy/MM/dd}'")> _
        Public Property SODate() As DateTime
            Get
                Return _sODate
            End Get
            Set(ByVal value As DateTime)
                _sODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PackingStatus", "'{0}'")> _
        Public Property PackingStatus() As String
            Get
                Return _packingStatus
            End Get
            Set(ByVal value As String)
                _packingStatus = value
            End Set
        End Property


        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber() As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property


        <ColumnInfo("BillingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BillingDate() As DateTime
            Get
                Return _billingDate
            End Get
            Set(ByVal value As DateTime)
                _billingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DeliveryDate() As DateTime
            Get
                Return _deliveryDate
            End Get
            Set(ByVal value As DateTime)
                _deliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DocumentType", "'{0}'")> _
        Public Property DocumentType() As String
            Get
                Return _documentType
            End Get
            Set(ByVal value As String)
                _documentType = value
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


        <ColumnInfo("SparePartPOID", "{0}"), _
        RelationInfo("SparePartPO", "ID", "SparePartPOStatus", "SparePartPOID")> _
        Public Property SparePartPO() As SparePartPO
            Get
                Try
                    If Not isnothing(Me._sparePartPO) AndAlso (Not Me._sparePartPO.IsLoaded) Then

                        Me._sparePartPO = CType(DoLoad(GetType(SparePartPO).ToString(), _sparePartPO.ID), SparePartPO)
                        Me._sparePartPO.MarkLoaded()

                    End If

                    Return Me._sparePartPO

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPO)

                Me._sparePartPO = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPO.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("SparePartPOStatus", "ID", "SparePartPOStatusDetail", "SparePartPOStatusID")> _
        Public ReadOnly Property SparePartPOStatusDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartPOStatusDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPOStatusDetail), "SparePartPOStatus", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartPOStatusDetails = DoLoadArray(GetType(SparePartPOStatusDetail).ToString, criterias)
                    End If

                    Return Me._sparePartPOStatusDetails

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
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Property"
        Public ReadOnly Property PackingStatusDesc() As String
            Get
                If _packingStatus = "C" Then
                    Return "Selesai"
                ElseIf _packingStatus = "P" Then
                    Return "Sedang Diproses"
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Private _SparePartPOID
        <ColumnInfo("SparePartPOID", "'{0}'")> _
        Public Property SparePartPOID() As String
            Get
                Return _SparePartPOID
            End Get
            Set(ByVal value As String)
                _SparePartPOID = value
            End Set
        End Property

        Private _IsChangedWSM As Boolean

        Public Property IsChangedWSM() As Boolean
            Get
                Return _IsChangedWSM
            End Get
            Set(ByVal value As Boolean)
                _IsChangedWSM = value
            End Set
        End Property

#End Region

    End Class
End Namespace

