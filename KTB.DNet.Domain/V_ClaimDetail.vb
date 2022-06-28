
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_ClaimDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 08/16/2017 - 3:26:26 PM
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
    <Serializable(), TableInfo("V_ClaimDetail")> _
    Public Class V_ClaimDetail
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
        'Private _claimHeaderID As Integer
        'Private _sparePartPOStatusDetailId As Integer
        Private _qty As Integer
        Private _approvedQty As Integer
        'Private _claimGoodConditionID As Integer
        Private _statusDetail As Byte
        Private _statusDetailKTB As Byte
        Private _keterangan As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _qtyClaim As Integer


        Private _claimHeader As ClaimHeader
        Private _claimGoodCondition As ClaimGoodCondition
        Private _sparePartPOStatusDetail As SparePartPOStatusDetail



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


        '<ColumnInfo("ClaimHeaderID", "{0}")> _
        'Public Property ClaimHeaderID As Integer
        '    Get
        '        Return _claimHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _claimHeaderID = value
        '    End Set
        'End Property


        '<ColumnInfo("SparePartPOStatusDetailId", "{0}")> _
        'Public Property SparePartPOStatusDetailId As Integer
        '    Get
        '        Return _sparePartPOStatusDetailId
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sparePartPOStatusDetailId = value
        '    End Set
        'End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property


        <ColumnInfo("ApprovedQty", "{0}")> _
        Public Property ApprovedQty As Integer
            Get
                Return _approvedQty
            End Get
            Set(ByVal value As Integer)
                _approvedQty = value
            End Set
        End Property


        '<ColumnInfo("ClaimGoodConditionID", "{0}")> _
        'Public Property ClaimGoodConditionID As Integer
        '    Get
        '        Return _claimGoodConditionID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _claimGoodConditionID = value
        '    End Set
        'End Property


        <ColumnInfo("StatusDetail", "{0}")> _
        Public Property StatusDetail As Byte
            Get
                Return _statusDetail
            End Get
            Set(ByVal value As Byte)
                _statusDetail = value
            End Set
        End Property


        <ColumnInfo("StatusDetailKTB", "{0}")> _
        Public Property StatusDetailKTB As Byte
            Get
                Return _statusDetailKTB
            End Get
            Set(ByVal value As Byte)
                _statusDetailKTB = value
            End Set
        End Property


        <ColumnInfo("Keterangan", "'{0}'")> _
        Public Property Keterangan As String
            Get
                Return _keterangan
            End Get
            Set(ByVal value As String)
                _keterangan = value
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

        <ColumnInfo("ClaimHeaderID", "{0}"), _
      RelationInfo("ClaimHeader", "ID", "V_ClaimDetail", "ClaimHeaderID")> _
        Public Property ClaimHeader() As ClaimHeader
            Get
                Try
                    If Not IsNothing(Me._claimHeader) AndAlso (Not Me._claimHeader.IsLoaded) Then

                        Me._claimHeader = CType(DoLoad(GetType(ClaimHeader).ToString(), _claimHeader.ID), ClaimHeader)
                        Me._claimHeader.MarkLoaded()

                    End If

                    Return Me._claimHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ClaimHeader)

                Me._claimHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ClaimGoodConditionID", "{0}"), _
        RelationInfo("ClaimGoodCondition", "ID", "V_ClaimDetail", "ClaimGoodConditionID")> _
        Public Property ClaimGoodCondition() As ClaimGoodCondition
            Get
                Try
                    If Not IsNothing(Me._claimGoodCondition) AndAlso (Not Me._claimGoodCondition.IsLoaded) Then

                        Me._claimGoodCondition = CType(DoLoad(GetType(ClaimGoodCondition).ToString(), _claimGoodCondition.ID), ClaimGoodCondition)
                        Me._claimGoodCondition.MarkLoaded()

                    End If

                    Return Me._claimGoodCondition

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ClaimGoodCondition)

                Me._claimGoodCondition = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimGoodCondition.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartPOStatusDetailId", "{0}"), _
        RelationInfo("SparePartPOStatusDetail", "ID", "V_ClaimDetail", "SparePartPOStatusDetailId")> _
        Public Property SparePartPOStatusDetail() As SparePartPOStatusDetail
            Get
                Try
                    If Not IsNothing(Me._sparePartPOStatusDetail) AndAlso (Not Me._sparePartPOStatusDetail.IsLoaded) Then

                        Me._sparePartPOStatusDetail = CType(DoLoad(GetType(SparePartPOStatusDetail).ToString(), _sparePartPOStatusDetail.ID), SparePartPOStatusDetail)
                        Me._sparePartPOStatusDetail.MarkLoaded()

                    End If

                    Return Me._sparePartPOStatusDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPOStatusDetail)

                Me._sparePartPOStatusDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPOStatusDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("QtyClaim", "{0}")> _
        Public Property QtyClaim As Integer
            Get
                Return _qtyClaim
            End Get
            Set(ByVal value As Integer)
                _qtyClaim = value
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

