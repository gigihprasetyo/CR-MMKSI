#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPLDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 3:22:10 PM
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
    <Serializable(), TableInfo("SPLDetail")> _
    Public Class SPLDetail
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
        'Private _sPLID As Integer
        Private _periodMonth As Integer
        Private _periodYear As Integer
        Private _quantity As Integer
        Private _priceRefDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _discount As Integer
        Private _surcharge As Integer
        Private _maxTopDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _maxTopDay As Integer
        Private _maxTopIndicator As Integer
        Private _freeIntIndicator As Integer
        Private _creditCeiling As Short
        Private _deliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sPL As SPL
        Private _vechileType As VechileType
        Private _isUpdated As Boolean = False
        Private _modelID As Integer
        Private _vechileTypeID As Integer
        Private _numberRow As Integer

        Private _splDetailtoSPLs As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _sisaQty As Integer

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


        '<ColumnInfo("SPLID", "{0}")> _
        'Public Property SPLID() As Integer
        '    Get
        '        Return _sPLID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sPLID = value
        '    End Set
        'End Property

        <ColumnInfo("PeriodMonth", "{0}")> _
        Public Property PeriodMonth() As Integer
            Get
                Return _periodMonth
            End Get
            Set(ByVal value As Integer)
                _periodMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "{0}")> _
        Public Property PeriodYear() As Integer
            Get
                Return _periodYear
            End Get
            Set(ByVal value As Integer)
                _periodYear = value
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


        <ColumnInfo("PriceRefDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PriceRefDate() As DateTime
            Get
                Return _priceRefDate
            End Get
            Set(ByVal value As DateTime)
                _priceRefDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount() As Integer
            Get
                Return _discount
            End Get
            Set(ByVal value As Integer)
                _discount = value
            End Set
        End Property

        <ColumnInfo("Surcharge", "{0}")> _
        Public Property Surcharge() As Integer
            Get
                Return _surcharge
            End Get
            Set(ByVal value As Integer)
                _surcharge = value
            End Set
        End Property


        <ColumnInfo("MaxTopDate", "'{0:yyyy/MM/dd}'")> _
        Public Property MaxTopDate() As DateTime
            Get
                Return _maxTopDate
            End Get
            Set(ByVal value As DateTime)
                _maxTopDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MaxTopDay", "{0}")> _
        Public Property MaxTopDay() As Integer
            Get
                Return _maxTopDay
            End Get
            Set(ByVal value As Integer)
                _maxTopDay = value
            End Set
        End Property


        <ColumnInfo("MaxTopIndicator", "{0}")> _
        Public Property MaxTopIndicator() As Integer
            Get
                Return _maxTopIndicator
            End Get
            Set(ByVal value As Integer)
                _maxTopIndicator = value
            End Set
        End Property


        <ColumnInfo("FreeIntIndicator", "{0}")> _
        Public Property FreeIntIndicator() As Integer
            Get
                Return _freeIntIndicator
            End Get
            Set(ByVal value As Integer)
                _freeIntIndicator = value
            End Set
        End Property


        <ColumnInfo("CreditCeiling", "{0}")> _
        Public Property CreditCeiling() As Short
            Get
                Return _creditCeiling
            End Get
            Set(ByVal value As Short)
                _creditCeiling = value
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


        <ColumnInfo("SPLID", "{0}"), _
        RelationInfo("SPL", "ID", "SPLDetail", "SPLID")> _
        Public Property SPL() As SPL
            Get
                Try
                    If Not IsNothing(Me._sPL) AndAlso (Not Me._sPL.IsLoaded) Then

                        Me._sPL = CType(DoLoad(GetType(SPL).ToString(), _sPL.ID), SPL)
                        Me._sPL.MarkLoaded()

                    End If

                    Return Me._sPL

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPL)

                Me._sPL = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPL.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VehicleTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "SPLDetail", "VehicleTypeID")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If Not IsNothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

                        Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
                        Me._vechileType.MarkLoaded()

                    End If

                    Return Me._vechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._vechileType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("SPLDetail", "ID", "SPLDetailtoSPL", "SPLDetailID")> _
        Public ReadOnly Property SPLDetailtoSPLs As System.Collections.ArrayList
            Get
                Try
                    If (Me._splDetailtoSPLs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPLDetailtoSPL), "SPLDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._splDetailtoSPLs = DoLoadArray(GetType(SPLDetailtoSPL).ToString, criterias)
                    End If

                    Return Me._splDetailtoSPLs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        Public Property SisaQty() As Integer
            Get
                Return _sisaQty
            End Get
            Set(ByVal value As Integer)
                _sisaQty = value
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
        Public Property IsUpdated() As Boolean
            Get
                Return _isUpdated
            End Get
            Set(ByVal value As Boolean)
                _isUpdated = value
            End Set
        End Property

        Public Property NumberRow As Integer
            Get
                Return _numberRow
            End Get
            Set(ByVal value As Integer)
                _numberRow = value
            End Set
        End Property

        Public Property VechileTypeID As Integer
            Get
                If Not IsNothing(Me.VechileType) Then
                    _vechileTypeID = Me.VechileType.ID
                End If
                Return _vechileTypeID
            End Get
            Set(ByVal value As Integer)
                _vechileTypeID = value
            End Set
        End Property

        Public Property ModelID As Integer
            Get
                If Not IsNothing(Me.VechileType) Then
                    Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, Me.VechileType.VechileModel.ID))
                    Dim strSQL As String = "Select ID From SubCategoryVehicle where CategoryID in (1,2) and Status = ''"
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "SubCategoryVehicle.ID", MatchType.InSet, "(" & strSQL & ")"))
                    Dim arrSCVToModel As ArrayList = DoLoadArray(GetType(SubCategoryVehicleToModel).ToString, criterias)
                    If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                        objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                        _modelID = objSubCategoryVehicleToModel.SubCategoryVehicle.ID
                    End If
                End If
                Return _modelID
            End Get
            Set(ByVal value As Integer)
                _modelID = value
            End Set
        End Property

#End Region

#Region "Custom Public Properties"

#End Region


    End Class
End Namespace

