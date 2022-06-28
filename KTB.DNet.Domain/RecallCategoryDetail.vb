#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("RecallCategoryDetail")> _
    Public Class RecallCategoryDetail
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
        Private _recallCategoryID As Integer
        Private _laborMasterID As Integer
        Private _status As Integer
        Private _positionCode As String
        Private _workCode As String

        Private _recallRegNo As String = String.Empty
        Private _description As String = String.Empty
        Private _validStartDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _buletinDescription As String = String.Empty

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _typeCode As String = String.Empty

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


        <ColumnInfo("RecallCategoryID", "{0}")> _
        Public Property RecallCategoryID As Integer
            Get
                Return _recallCategoryID
            End Get
            Set(ByVal value As Integer)
                _recallCategoryID = value
            End Set
        End Property

        Private _recallCategory As RecallCategory
        Private _laborMaster As LaborMaster

        <ColumnInfo("RecallCategoryID", "{0}"), _
        RelationInfo("RecallCategory", "ID", "RecallCategoryDetail", "RecallCategoryID")> _
        Public Property RecallCategory() As RecallCategory
            Get
                Dim objRecallCategory As RecallCategory
                Dim objRecallCategoryList As ArrayList
                Dim RecallCategoryCriteria As Criteria = New Criteria(GetType(RecallCategory), "ID", Me.RecallCategoryID)
                Dim RecallCategoryCriterias As CriteriaComposite = New CriteriaComposite(RecallCategoryCriteria)
                RecallCategoryCriterias.opAnd(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                objRecallCategoryList = DoLoadArray(GetType(RecallCategory).ToString, RecallCategoryCriterias)
                If (Not IsNothing(objRecallCategoryList) And objRecallCategoryList.Count > 0) Then
                    objRecallCategory = objRecallCategoryList(0)
                    Return objRecallCategory
                End If

                Return Nothing
            End Get
            Set(ByVal value As RecallCategory)
                Me._RecallCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._RecallCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("LaborMasterID", "{0}"), _
        RelationInfo("LaborMaster", "ID", "RecallCategoryDetail", "LaborMasterID")> _
        Public Property LaborMaster() As LaborMaster
            Get
                Dim objLaborMaster As LaborMaster
                Dim objLaborMasterList As ArrayList
                Dim laborMasterCriteria As Criteria = New Criteria(GetType(LaborMaster), "ID", Me.LaborMasterID)
                Dim laborMasterCriterias As CriteriaComposite = New CriteriaComposite(laborMasterCriteria)
                laborMasterCriterias.opAnd(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                objLaborMasterList = DoLoadArray(GetType(LaborMaster).ToString, laborMasterCriterias)
                If (Not IsNothing(objLaborMasterList) And objLaborMasterList.Count > 0) Then
                    objLaborMaster = objLaborMasterList(0)
                    Return objLaborMaster
                End If

                Return Nothing
            End Get
            Set(ByVal value As LaborMaster)
                Me._laborMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._laborMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("LaborMasterID", "{0}")> _
        Public Property LaborMasterID As Integer
            Get
                Return _laborMasterID
            End Get
            Set(ByVal value As Integer)
                _laborMasterID = value
            End Set
        End Property

       

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
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

        Public Property PositionCode As String
            Get
                Return _positionCode
            End Get
            Set(ByVal value As String)
                _positionCode = value
            End Set
        End Property

        Public Property WorkCode As String
            Get
                Return _workCode
            End Get
            Set(ByVal value As String)
                _workCode = value
            End Set
        End Property

        Public Property VehicleTypeCode As String
            Get
                Dim objVehicleType As VechileType = Me.VehicleType

                If (Not IsNothing(objVehicleType)) Then
                    Return objVehicleType.VechileTypeCode
                End If

                Return Nothing
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Public ReadOnly Property CategoryMaster() As Category
            Get
                Dim objCategory As Category
                Dim objCategoryList As ArrayList
                Dim categoryCriteria As Criteria = New Criteria(GetType(Category), "ID", Me.VehicleType.Category.ID)
                Dim categoryCriterias As CriteriaComposite = New CriteriaComposite(categoryCriteria)
                categoryCriterias.opAnd(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                objCategoryList = DoLoadArray(GetType(Category).ToString, categoryCriterias)
                If (Not IsNothing(objCategoryList) And objCategoryList.Count > 0) Then
                    objCategory = objCategoryList(0)

                    Return objCategory
                End If

                Return Nothing
            End Get
        End Property

        Public ReadOnly Property VehicleType As VechileType
            Get
                Dim objLaborMaster As LaborMaster
                Dim objLaborMasterList As ArrayList
                Dim laborMasterCriteria As Criteria = New Criteria(GetType(LaborMaster), "ID", Me.LaborMasterID)
                Dim laborMasterCriterias As CriteriaComposite = New CriteriaComposite(laborMasterCriteria)
                laborMasterCriterias.opAnd(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                objLaborMasterList = DoLoadArray(GetType(LaborMaster).ToString, laborMasterCriterias)
                If (Not IsNothing(objLaborMasterList) And objLaborMasterList.Count > 0) Then
                    objLaborMaster = objLaborMasterList(0)

                    Dim objVehicleType As VechileType
                    Dim objVehicleTypeList As ArrayList
                    Dim vehicleTypeCriteria As Criteria = New Criteria(GetType(VechileType), "ID", objLaborMaster.VechileType.ID)
                    Dim vehicleTypeCriterias As CriteriaComposite = New CriteriaComposite(vehicleTypeCriteria)
                    vehicleTypeCriterias.opAnd(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    objVehicleTypeList = DoLoadArray(GetType(VechileType).ToString, vehicleTypeCriterias)
                    If (Not IsNothing(objVehicleTypeList) And objVehicleTypeList.Count > 0) Then
                        objVehicleType = objVehicleTypeList(0)
                        Return objVehicleType
                    End If

                    Return Nothing
                End If

                Return Nothing
            End Get
        End Property

        Public Property VehicleCategoryCode As String
            Get
                Dim objVehicleType As VechileType = Me.VehicleType

                If (Not IsNothing(objVehicleType)) Then
                    Return objVehicleType.Category.CategoryCode
                End If

                Return Nothing
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Public Property VehicleTypeDescription As String
            Get
                Dim objVehicleType As VechileType = Me.VehicleType

                If (Not IsNothing(objVehicleType)) Then
                    Return objVehicleType.Description
                End If

                Return Nothing
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Public Property VehicleTypeCodeTemp As String
            Get
                Return _typeCode
            End Get
            Set(ByVal value As String)
                _typeCode = value
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

