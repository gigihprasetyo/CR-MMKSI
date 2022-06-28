
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBKewajibanDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/17/2016 - 8:19:52 AM
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
    <Serializable(), TableInfo("DepositBKewajibanDetail")> _
    Public Class DepositBKewajibanDetail
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
        Private _qty As Short
        Private _harga As Decimal
        Private _tax As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _depositBKewajibanHeader As DepositBKewajibanHeader
        Private _equipmentMaster As EquipmentMaster
        Private _sparePartMaster As SparePartMaster



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


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Short
            Get
                Return _qty
            End Get
            Set(ByVal value As Short)
                _qty = value
            End Set
        End Property


        <ColumnInfo("Harga", "{0}")> _
        Public Property Harga As Decimal
            Get
                Return _harga
            End Get
            Set(ByVal value As Decimal)
                _harga = value
            End Set
        End Property

        <ColumnInfo("Tax", "{0}")> _
        Public Property Tax As Decimal
            Get
                Return _tax
            End Get
            Set(ByVal value As Decimal)
                _tax = value
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


        <ColumnInfo("DepositBKewajibanHeaderID", "{0}"), _
        RelationInfo("DepositBKewajibanHeader", "ID", "DepositBKewajibanDetail", "DepositBKewajibanHeaderID")> _
        Public Property DepositBKewajibanHeader As DepositBKewajibanHeader
            Get
                Try
                    If Not isnothing(Me._depositBKewajibanHeader) AndAlso (Not Me._depositBKewajibanHeader.IsLoaded) Then

                        Me._depositBKewajibanHeader = CType(DoLoad(GetType(DepositBKewajibanHeader).ToString(), _depositBKewajibanHeader.ID), DepositBKewajibanHeader)
                        Me._depositBKewajibanHeader.MarkLoaded()

                    End If

                    Return Me._depositBKewajibanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBKewajibanHeader)

                Me._depositBKewajibanHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBKewajibanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EquipmentMasterID", "{0}"), _
        RelationInfo("EquipmentMaster", "ID", "DepositBKewajibanDetail", "EquipmentMasterID")> _
        Public Property EquipmentMaster As EquipmentMaster
            Get
                Try
                    If Not isnothing(Me._equipmentMaster) AndAlso (Not Me._equipmentMaster.IsLoaded) Then

                        Me._equipmentMaster = CType(DoLoad(GetType(EquipmentMaster).ToString(), _equipmentMaster.ID), EquipmentMaster)
                        Me._equipmentMaster.MarkLoaded()

                    End If

                    Return Me._equipmentMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EquipmentMaster)

                Me._equipmentMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._equipmentMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "DepositBKewajibanDetail", "SparePartMasterID")> _
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

