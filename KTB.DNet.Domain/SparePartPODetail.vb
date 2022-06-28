#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPODetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 19/10/2005 - 8:51:27
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
    <Serializable(), TableInfo("SparePartPODetail")> _
    Public Class SparePartPODetail
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
        Private _checkListStatus As String = String.Empty
        Private _quantity As Integer
        Private _retailPrice As Decimal
        Private _estimateStatus As String = String.Empty
        Private _stopMark As Short
        Private _totalForecast As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartPO As SparePartPO
        Private _sparePartMaster As SparePartMaster



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


        <ColumnInfo("CheckListStatus", "'{0}'")> _
        Public Property CheckListStatus() As String
            Get
                Return _checkListStatus
            End Get
            Set(ByVal value As String)
                _checkListStatus = value
            End Set
        End Property


        <ColumnInfo("TotalForecast", "{0}")> _
        Public Property TotalForecast() As Integer
            Get
                Return _totalForecast
            End Get
            Set(ByVal value As Integer)
                _totalForecast = value
            End Set
        End Property


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("RetailPrice", "{0}")> _
        Public Property RetailPrice() As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
            End Set
        End Property


        <ColumnInfo("EstimateStatus", "'{0}'")> _
        Public Property EstimateStatus() As String
            Get
                Return _estimateStatus
            End Get
            Set(ByVal value As String)
                _estimateStatus = value
            End Set
        End Property


        <ColumnInfo("StopMark", "{0}")> _
        Public Property StopMark() As Short
            Get
                Return _stopMark
            End Get
            Set(ByVal value As Short)
                _stopMark = value
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
        RelationInfo("SparePartPO", "ID", "SparePartPODetail", "SparePartPOID")> _
        Public Property SparePartPO() As SparePartPO
            Get
                Try
                    If Not IsNothing(Me._sparePartPO) AndAlso (Not Me._sparePartPO.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPO.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "SparePartPODetail", "SparePartMasterID")> _
        Public Property SparePartMaster() As SparePartMaster
            Get
                Try
                    If Not IsNothing(Me._sparePartMaster) AndAlso (Not Me._sparePartMaster.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartMaster.MarkLoaded()
                End If
            End Set
        End Property




#End Region

#Region "Custom Method"

#End Region

#Region "Custom Properties"
        Dim _notExistPartNumber As String = String.Empty

        <ColumnInfo("Amount", "{0}")> _
        Public ReadOnly Property Amount() As Decimal
            Get
                Return _quantity * _retailPrice
            End Get
        End Property
        Public ReadOnly Property PartNumberTemp() As String
            Get
                If Not IsNothing(SparePartMaster) Then
                    Return _sparePartMaster.PartNumber
                Else
                    Return _notExistPartNumber
                End If
            End Get
        End Property

        Public ReadOnly Property PartNameTemp() As String
            Get
                If Not IsNothing(SparePartMaster) Then
                    Return _sparePartMaster.PartName
                Else
                    Return ""
                End If
            End Get
        End Property
        Public WriteOnly Property NotExistPartNumber() As String
            Set(ByVal Value As String)
                _notExistPartNumber = Value
            End Set
        End Property

        Public ReadOnly Property CheckListStatusDesc() As String
            Get
                If _checkListStatus = "1" Then
                    Return "Terpenuhi"
                ElseIf _checkListStatus = "0" Then
                    Return "Tdk Terpenuhi"
                Else
                    Return String.Empty
                End If
            End Get
        End Property

#End Region

    End Class

End Namespace
