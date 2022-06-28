#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VechileType Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 25/09/2007 - 12:34:48
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
    <Serializable(), TableInfo("VechileType")> _
    Public Class VechileType
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
        Private _vechileTypeCode As String = String.Empty
        Private _description As String = String.Empty
        Private _descriptionDealer As String = String.Empty
        Private _status As String = String.Empty
        Private _isVehicleKind1 As Byte
        Private _isVehicleKind2 As Byte
        Private _isVehicleKind3 As Byte
        Private _isVehicleKind4 As Byte
        Private _maxTOPDays As Integer
        Private _segmentType As String = String.Empty
        Private _variantType As String = String.Empty
        Private _transmitType As String = String.Empty
        Private _driveSystemType As String = String.Empty
        Private _speedType As String = String.Empty
        Private _fuelType As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _category As Category
        Private _productCategory As ProductCategory
        Private _vechileModel As VechileModel
        Private _vehicleClass As VehicleClass
        Private _salesVechileModel As SalesVechileModel

        Private _pameranDisplays As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sAPCustomers As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _conditionMasters As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _eventSaless As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _SAPModel As String = String.Empty

        Private _sPLDetail As SPLDetail = New SPLDetail(0)

        Private _isActiveOnPK As Byte


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


        <ColumnInfo("VechileTypeCode", "'{0}'")> _
        Public Property VechileTypeCode() As String
            Get
                Return _vechileTypeCode
            End Get
            Set(ByVal value As String)
                _vechileTypeCode = value
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


        <ColumnInfo("DescriptionDealer", "'{0}'")> _
        Public Property DescriptionDealer() As String
            Get
                Return _descriptionDealer
            End Get
            Set(ByVal value As String)
                _descriptionDealer = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        <ColumnInfo("IsVehicleKind1", "{0}")> _
        Public Property IsVehicleKind1() As Byte
            Get
                Return _isVehicleKind1
            End Get
            Set(ByVal value As Byte)
                _isVehicleKind1 = value
            End Set
        End Property

        <ColumnInfo("IsVehicleKind2", "{0}")> _
        Public Property IsVehicleKind2() As Byte
            Get
                Return _isVehicleKind2
            End Get
            Set(ByVal value As Byte)
                _isVehicleKind2 = value
            End Set
        End Property


        <ColumnInfo("IsVehicleKind3", "{0}")> _
        Public Property IsVehicleKind3() As Byte
            Get
                Return _isVehicleKind3
            End Get
            Set(ByVal value As Byte)
                _isVehicleKind3 = value
            End Set
        End Property


        <ColumnInfo("IsVehicleKind4", "{0}")> _
        Public Property IsVehicleKind4() As Byte
            Get
                Return _isVehicleKind4
            End Get
            Set(ByVal value As Byte)
                _isVehicleKind4 = value
            End Set
        End Property

        <ColumnInfo("SegmentType", "'{0}'")> _
        Public Property SegmentType() As String
            Get
                Return _segmentType
            End Get
            Set(ByVal value As String)
                _segmentType = value
            End Set
        End Property

        <ColumnInfo("VariantType", "'{0}'")> _
        Public Property VariantType() As String
            Get
                Return _variantType
            End Get
            Set(ByVal value As String)
                _variantType = value
            End Set
        End Property

        <ColumnInfo("TransmitType", "'{0}'")> _
        Public Property TransmitType() As String
            Get
                Return _transmitType
            End Get
            Set(ByVal value As String)
                _transmitType = value
            End Set
        End Property

        <ColumnInfo("DriveSystemType", "'{0}'")> _
        Public Property DriveSystemType() As String
            Get
                Return _driveSystemType
            End Get
            Set(ByVal value As String)
                _driveSystemType = value
            End Set
        End Property

        <ColumnInfo("SpeedType", "'{0}'")> _
        Public Property SpeedType() As String
            Get
                Return _speedType
            End Get
            Set(ByVal value As String)
                _speedType = value
            End Set
        End Property

        <ColumnInfo("FuelType", "'{0}'")> _
        Public Property FuelType() As String
            Get
                Return _fuelType
            End Get
            Set(ByVal value As String)
                _fuelType = value
            End Set
        End Property


        <ColumnInfo("MaxTOPDays", "{0}")> _
        Public Property MaxTOPDays() As Integer
            Get
                Return _maxTOPDays
            End Get
            Set(ByVal value As Integer)
                _maxTOPDays = value
            End Set
        End Property


        <ColumnInfo("SAPModel", "'{0}'")> _
        Public Property SAPModel() As String
            Get
                Return _SAPModel
            End Get
            Set(ByVal value As String)
                _SAPModel = value
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


        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "VechileType", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "VechileType", "ProductCategoryID")> _
        Public Property ProductCategory() As ProductCategory
            Get
                Try
                    If Not IsNothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

                        Me._productCategory = CType(DoLoad(GetType(ProductCategory).ToString(), _productCategory.ID), ProductCategory)
                        Me._productCategory.MarkLoaded()

                    End If

                    Return Me._productCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProductCategory)

                Me._productCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("ModelID", "{0}"), _
        RelationInfo("VechileModel", "ID", "VechileType", "ModelID")> _
        Public Property VechileModel() As VechileModel
            Get
                Try
                    If Not IsNothing(Me._vechileModel) AndAlso (Not Me._vechileModel.IsLoaded) Then

                        Me._vechileModel = CType(DoLoad(GetType(VechileModel).ToString(), _vechileModel.ID), VechileModel)
                        Me._vechileModel.MarkLoaded()

                    End If

                    Return Me._vechileModel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileModel)

                Me._vechileModel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileModel.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SalesVechileModelID", "{0}"), _
        RelationInfo("SalesVechileModel", "ID", "VechileType", "SalesVechileModelID")> _
        Public Property SalesVechileModel() As SalesVechileModel
            Get
                Try
                    If Not IsNothing(Me._salesVechileModel) AndAlso (Not Me._salesVechileModel.IsLoaded) Then

                        Me._salesVechileModel = CType(DoLoad(GetType(SalesVechileModel).ToString(), _salesVechileModel.ID), SalesVechileModel)
                        Me._salesVechileModel.MarkLoaded()

                    End If

                    Return Me._salesVechileModel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesVechileModel)

                Me._salesVechileModel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesVechileModel.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("VehicleClassID", "{0}"), _
        RelationInfo("VehicleClass", "ID", "VechileType", "VehicleClassID")> _
        Public Property VehicleClass() As VehicleClass
            Get
                Try
                    If Not IsNothing(Me._vehicleClass) AndAlso (Not Me._vehicleClass.IsLoaded) Then

                        Me._vehicleClass = CType(DoLoad(GetType(VehicleClass).ToString(), _vehicleClass.ID), VehicleClass)
                        Me._vehicleClass.MarkLoaded()

                    End If

                    Return Me._vehicleClass

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VehicleClass)

                Me._vehicleClass = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehicleClass.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("VechileType", "ID", "PameranDisplay", "VechileTypeID")> _
        Public ReadOnly Property PameranDisplays() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pameranDisplays.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PameranDisplay), "VechileType", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PameranDisplay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pameranDisplays = DoLoadArray(GetType(PameranDisplay).ToString, criterias)
                    End If

                    Return Me._pameranDisplays

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("VechileType", "ID", "SAPCustomer", "VechileTypeID")> _
        Public ReadOnly Property SAPCustomers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sAPCustomers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SAPCustomer), "VechileType", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sAPCustomers = DoLoadArray(GetType(SAPCustomer).ToString, criterias)
                    End If

                    Return Me._sAPCustomers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("VechileType", "ID", "ConditionMaster", "VechileTypeID")> _
        Public ReadOnly Property ConditionMasters() As System.Collections.ArrayList
            Get
                Try
                    If (Me._conditionMasters.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ConditionMaster), "VechileType", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ConditionMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._conditionMasters = DoLoadArray(GetType(ConditionMaster).ToString, criterias)
                    End If

                    Return Me._conditionMasters

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("VechileType", "ID", "EventSales", "VechileTypeID")> _
        Public ReadOnly Property EventSaless() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventSaless.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventSales), "VechileType", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventSales), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventSaless = DoLoadArray(GetType(EventSales).ToString, criterias)
                    End If

                    Return Me._eventSaless

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <ColumnInfo("ID", "{0}"), _
        RelationInfo("VechileType", "ID", "SPLDetail", "VehicleTypeID")> _
        Public ReadOnly Property SPLDetail() As SPLDetail
            Get
                Try
                    If Not IsNothing(Me._sPLDetail) AndAlso (Not Me._sPLDetail.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPLDetail), "VechileType", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(SPLDetail).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._sPLDetail = CType(tempColl(0), SPLDetail)
                        Else
                            Me._sPLDetail = Nothing
                        End If
                    End If

                    Return Me._sPLDetail

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property

        <ColumnInfo("IsActiveOnPK", "{0}")> _
        Public Property IsActiveOnPK() As Byte
            Get
                Return _isActiveOnPK
            End Get
            Set(ByVal value As Byte)
                _isActiveOnPK = value
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

        Public ReadOnly Property VechileCodeDesc()
            Get
                Return _vechileTypeCode & " - " & _description
            End Get
        End Property

#End Region

    End Class
End Namespace

