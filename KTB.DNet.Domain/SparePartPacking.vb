
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPacking Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2016 - 9:22:42 AM
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
    <Serializable(), TableInfo("SparePartPacking")> _
    Public Class SparePartPacking
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
        Private _internalHUNo As String = String.Empty
        Private _packMaterial As String = String.Empty
        Private _packMaterialDesc As String = String.Empty
        Private _lotCase As String = String.Empty
        Private _weight As Decimal
        Private _volume As Decimal
        Private _totalItem As Decimal
        Private _totalQty As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartDOExpedition As SparePartDOExpedition

        Private _sparePartPackingDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("InternalHUNo", "'{0}'")> _
        Public Property InternalHUNo As String
            Get
                Return _internalHUNo
            End Get
            Set(ByVal value As String)
                _internalHUNo = value
            End Set
        End Property


        <ColumnInfo("PackMaterial", "'{0}'")> _
        Public Property PackMaterial As String
            Get
                Return _packMaterial
            End Get
            Set(ByVal value As String)
                _packMaterial = value
            End Set
        End Property


        <ColumnInfo("PackMaterialDesc", "'{0}'")> _
        Public Property PackMaterialDesc As String
            Get
                Return _packMaterialDesc
            End Get
            Set(ByVal value As String)
                _packMaterialDesc = value
            End Set
        End Property


        <ColumnInfo("LotCase", "'{0}'")> _
        Public Property LotCase As String
            Get
                Return _lotCase
            End Get
            Set(ByVal value As String)
                _lotCase = value
            End Set
        End Property


        <ColumnInfo("Weight", "#,##0")> _
        Public Property Weight As Decimal
            Get
                Return _weight
            End Get
            Set(ByVal value As Decimal)
                _weight = value
            End Set
        End Property


        <ColumnInfo("Volume", "#,##0")> _
        Public Property Volume As Decimal
            Get
                Return _volume
            End Get
            Set(ByVal value As Decimal)
                _volume = value
            End Set
        End Property


        <ColumnInfo("TotalItem", "#,##0")> _
        Public Property TotalItem As Decimal
            Get
                Return _totalItem
            End Get
            Set(ByVal value As Decimal)
                _totalItem = value
            End Set
        End Property


        <ColumnInfo("TotalQty", "#,##0")> _
        Public Property TotalQty As Decimal
            Get
                Return _totalQty
            End Get
            Set(ByVal value As Decimal)
                _totalQty = value
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


        <ColumnInfo("SparePartDOExpeditionID", "{0}"), _
        RelationInfo("SparePartDOExpedition", "ID", "SparePartPacking", "SparePartDOExpeditionID")> _
        Public Property SparePartDOExpedition As SparePartDOExpedition
            Get
                Try
                    If Not isnothing(Me._sparePartDOExpedition) AndAlso (Not Me._sparePartDOExpedition.IsLoaded) Then

                        Me._sparePartDOExpedition = CType(DoLoad(GetType(SparePartDOExpedition).ToString(), _sparePartDOExpedition.ID), SparePartDOExpedition)
                        Me._sparePartDOExpedition.MarkLoaded()

                    End If

                    Return Me._sparePartDOExpedition

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartDOExpedition)

                Me._sparePartDOExpedition = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartDOExpedition.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("SparePartPacking", "ID", "SparePartPackingDetail", "SparePartPackingID")> _
        Public ReadOnly Property SparePartPackingDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartPackingDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPackingDetail), "SparePartPacking", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPackingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartPackingDetails = DoLoadArray(GetType(SparePartPackingDetail).ToString, criterias)
                    End If

                    Return Me._sparePartPackingDetails

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

