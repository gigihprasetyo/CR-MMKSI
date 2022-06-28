
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDODetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2016 - 4:22:32 PM
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
    <Serializable(), TableInfo("SparePartDODetail")> _
    Public Class SparePartDODetail
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
        Private _itemNoDO As Integer
        Private _itemNoSO As Integer
        Private _qty As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartMaster As SparePartMaster
        Private _sparePartDO As SparePartDO
        Private _sparePartPOEstimate As SparePartPOEstimate

        'Private _sparePartBillingDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("ItemNoDO", "{0}")> _
        Public Property ItemNoDO As Integer
            Get
                Return _itemNoDO
            End Get
            Set(ByVal value As Integer)
                _itemNoDO = value
            End Set
        End Property


        <ColumnInfo("ItemNoSO", "{0}")> _
        Public Property ItemNoSO As Integer
            Get
                Return _itemNoSO
            End Get
            Set(ByVal value As Integer)
                _itemNoSO = value
            End Set
        End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
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


        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "SparePartDODetail", "SparePartMasterID")> _
        Public Property SparePartMaster As SparePartMaster
            Get
                Try
                    If Not isnothing(Me._sparePartMaster) AndAlso (Not Me._sparePartMaster.IsLoaded) Then

                        Me._sparePartMaster = CType(DoLoad(GetType(SparePartMaster).ToString(), _sparePartMaster.ID), SparePartMaster)
                        Me._sparePartMaster.MarkLoaded()

                    End If

                    Return Me._sparePartMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartMaster)

                Me._sparePartMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartDOID", "{0}"), _
        RelationInfo("SparePartDO", "ID", "SparePartDODetail", "SparePartDOID")> _
        Public Property SparePartDO As SparePartDO
            Get
                Try
                    If Not isnothing(Me._sparePartDO) AndAlso (Not Me._sparePartDO.IsLoaded) Then

                        Me._sparePartDO = CType(DoLoad(GetType(SparePartDO).ToString(), _sparePartDO.ID), SparePartDO)
                        Me._sparePartDO.MarkLoaded()

                    End If

                    Return Me._sparePartDO

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartDO)

                Me._sparePartDO = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartDO.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartPOEstimateID", "{0}"), _
        RelationInfo("SparePartPOEstimate", "ID", "SparePartDODetail", "SparePartPOEstimateID")> _
        Public Property SparePartPOEstimate As SparePartPOEstimate
            Get
                Try
                    If Not isnothing(Me._sparePartPOEstimate) AndAlso (Not Me._sparePartPOEstimate.IsLoaded) Then

                        Me._sparePartPOEstimate = CType(DoLoad(GetType(SparePartPOEstimate).ToString(), _sparePartPOEstimate.ID), SparePartPOEstimate)
                        Me._sparePartPOEstimate.MarkLoaded()

                    End If

                    Return Me._sparePartPOEstimate

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPOEstimate)

                Me._sparePartPOEstimate = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPOEstimate.MarkLoaded()
                End If
            End Set
        End Property


        '<RelationInfo("SparePartDODetail", "ID", "SparePartBillingDetail", "SparePartDODetailID")> _
        'Public ReadOnly Property SparePartBillingDetails As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._sparePartBillingDetails.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(SparePartBillingDetail), "SparePartDODetail", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(SparePartBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._sparePartBillingDetails = DoLoadArray(GetType(SparePartBillingDetail).ToString, criterias)
        '            End If

        '            Return Me._sparePartBillingDetails

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property


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

