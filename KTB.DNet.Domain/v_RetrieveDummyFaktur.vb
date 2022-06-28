#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_DealerStock Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 18/4/2019 - 9:18:34 AM
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
    <Serializable(), TableInfo("v_RetrieveDummyFaktur")> _
    Public Class v_RetrieveDummyFaktur
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
        Private _chassisNumber As String = String.Empty
        Private _dODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dONumber As String = String.Empty
        Private _alreadySaled As Int16
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturStatus As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _projectName As String = String.Empty
        Private _fakturNumber As String = String.Empty
        Private _fakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _openFakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _categoryID As Category
        Private _vehicleTypeID As VechileType
        Private _vehicleColorID As VechileColor
        Private _vehicleModelID As VechileModel

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


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber() As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        <ColumnInfo("DODate", "'{0:yyyy/MM/dd}'")> _
        Public Property DODate() As DateTime
            Get
                Return _dODate
            End Get
            Set(ByVal value As DateTime)
                _dODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber() As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property


        <ColumnInfo("AlreadySaled", "{0}")> _
        Public Property AlreadySaled() As Byte
            Get
                Return _alreadySaled
            End Get
            Set(ByVal value As Byte)
                _alreadySaled = value
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


        <ColumnInfo("FakturStatus", "'{0}'")> _
        Public Property FakturStatus() As String
            Get
                Return _fakturStatus
            End Get
            Set(ByVal value As String)
                _fakturStatus = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("ProjectName", "'{0}'")> _
        Public Property ProjectName() As String
            Get
                Return _projectName
            End Get
            Set(ByVal value As String)
                _projectName = value
            End Set
        End Property


        <ColumnInfo("FakturNumber", "'{0}'")> _
        Public Property FakturNumber() As String
            Get
                Return _fakturNumber
            End Get
            Set(ByVal value As String)
                _fakturNumber = value
            End Set
        End Property


        <ColumnInfo("FakturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FakturDate() As DateTime
            Get
                Return _fakturDate
            End Get
            Set(ByVal value As DateTime)
                _fakturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("OpenFakturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property OpenFakturDate() As DateTime
            Get
                Return _openFakturDate
            End Get
            Set(ByVal value As DateTime)
                _openFakturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ValidateTime", "'{0:yyyy/MM/dd}'")> _
        Public Property ValidateTime() As DateTime
            Get
                Return _validateTime
            End Get
            Set(ByVal value As DateTime)
                _validateTime = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "v_RetrieveDummyFaktur", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._categoryID) AndAlso (Not Me._categoryID.IsLoaded) Then
                        Me._categoryID = CType(DoLoad(GetType(Category).ToString(), _categoryID.ID), Category)
                        Me._categoryID.MarkLoaded()
                    End If
                    Return Me._categoryID
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As Category)
                Me._categoryID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._categoryID.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("VehicleTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "v_RetrieveDummyFaktur", "VehicleTypeID")> _
        Public Property VehicleType() As VechileType
            Get
                Try
                    If Not IsNothing(Me._vehicleTypeID) AndAlso (Not Me._vehicleTypeID.IsLoaded) Then
                        Me._vehicleTypeID = CType(DoLoad(GetType(VechileType).ToString(), _vehicleTypeID.ID), VechileType)
                        Me._vehicleTypeID.MarkLoaded()
                    End If
                    Return Me._vehicleTypeID
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As VechileType)
                Me._vehicleTypeID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehicleTypeID.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("VehicleColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "v_RetrieveDummyFaktur", "VehicleColorID")> _
        Public Property VehicleColor() As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vehicleTypeID) AndAlso (Not Me._vehicleColorID.IsLoaded) Then
                        Me._vehicleColorID = CType(DoLoad(GetType(VechileColor).ToString(), _vehicleColorID.ID), VechileColor)
                        Me._vehicleColorID.MarkLoaded()
                    End If
                    Return Me._vehicleColorID
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As VechileColor)
                Me._vehicleColorID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehicleColorID.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("VehicleModelID", "{0}"), _
        RelationInfo("VechileModel", "ID", "v_RetrieveDummyFaktur", "VehicleModelID")> _
        Public Property VehicleModel() As VechileModel
            Get
                Try
                    If Not IsNothing(Me._vehicleTypeID) AndAlso (Not Me._vehicleModelID.IsLoaded) Then
                        Me._vehicleModelID = CType(DoLoad(GetType(VechileModel).ToString(), _vehicleModelID.ID), VechileModel)
                        Me._vehicleModelID.MarkLoaded()
                    End If
                    Return Me._vehicleModelID
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As VechileModel)
                Me._vehicleModelID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehicleModelID.MarkLoaded()
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
        Private _aCMReturns As ArrayList = Nothing
        Public ReadOnly Property isValidToCreateFaktur() As Boolean
            Get
                If IsNothing(Me._aCMReturns) Then
                    Dim cCM As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    cCM.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.Exact, Me.ID))
                    cCM.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Lesser, ChassisMaster.queryValidToCreateFaktur(Me.ID.ToString())))
                    _aCMReturns = DoLoadArray(GetType(ChassisMaster).ToString, cCM)
                    If (IsNothing(Me._aCMReturns)) Then Me._aCMReturns = New ArrayList
                End If

                Return (Me._aCMReturns.Count > 0)
            End Get
        End Property

        Public ReadOnly Property ChassisMaster() As ChassisMaster
            Get
                Dim cCM As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim aCMs As New ArrayList
                Dim oCM As New ChassisMaster

                cCM.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, Me.ChassisNumber))
                aCMs = DoLoadArray(GetType(ChassisMaster).ToString(), cCM)
                If (aCMs.Count > 0) Then
                    oCM = aCMs(0)
                End If

                Return oCM
            End Get
        End Property
#End Region

    End Class

End Namespace
