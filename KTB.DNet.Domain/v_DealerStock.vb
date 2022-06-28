
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_DealerStock Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 12/3/2009 - 9:18:34 AM
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
    <Serializable(), TableInfo("v_DealerStock")> _
    Public Class v_DealerStock
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
        Private _fakturStatus As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _searchTerm1 As String = String.Empty
        Private _groupName As String = String.Empty
        Private _vechileTypeCode As String = String.Empty
        Private _colorCode As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _description As String = String.Empty
        Private _projectName As String = String.Empty
        Private _name1 As String = String.Empty
        Private _provinceName As String = String.Empty
        Private _dODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturNumber As String = String.Empty

        Private _fakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _openFakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _printedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _tOPID As Integer
        Private _sONumber As String = String.Empty
        Private _dONumber As String = String.Empty
        Private _categoryID As Byte
        Private _alreadySaled As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _ProductionYear As Integer

        Private _modelDescription As String = String.Empty

        Private _ConfirmFakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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


        <ColumnInfo("SearchTerm1", "'{0}'")> _
        Public Property SearchTerm1() As String
            Get
                Return _searchTerm1
            End Get
            Set(ByVal value As String)
                _searchTerm1 = value
            End Set
        End Property


        <ColumnInfo("GroupName", "'{0}'")> _
        Public Property GroupName() As String
            Get
                Return _groupName
            End Get
            Set(ByVal value As String)
                _groupName = value
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


        <ColumnInfo("ColorCode", "'{0}'")> _
        Public Property ColorCode() As String
            Get
                Return _colorCode
            End Get
            Set(ByVal value As String)
                _colorCode = value
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


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
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


        <ColumnInfo("Name1", "'{0}'")> _
        Public Property Name1() As String
            Get
                Return _name1
            End Get
            Set(ByVal value As String)
                _name1 = value
            End Set
        End Property


        <ColumnInfo("ProvinceName", "'{0}'")> _
        Public Property ProvinceName() As String
            Get
                Return _provinceName
            End Get
            Set(ByVal value As String)
                _provinceName = value
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


        <ColumnInfo("PrintedTime", "'{0:yyyy/MM/dd}'")> _
        Public Property PrintedTime() As DateTime
            Get
                Return _printedTime
            End Get
            Set(ByVal value As DateTime)
                _printedTime = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("TOPID", "{0}")> _
        Public Property TOPID() As Integer
            Get
                Return _tOPID
            End Get
            Set(ByVal value As Integer)
                _tOPID = value
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


        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber() As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}")> _
        Public Property CategoryID() As Byte
            Get
                Return _categoryID
            End Get
            Set(ByVal value As Byte)
                _categoryID = value
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


        <ColumnInfo("ProductionYear", "{0}")> _
        Public Property ProductionYear() As Integer
            Get
                Return _ProductionYear
            End Get
            Set(ByVal value As Integer)
                _ProductionYear = value
            End Set
        End Property

        <ColumnInfo("ModelDescription", "'{0}'")> _
        Public Property ModelDescription() As String
            Get
                Return _modelDescription
            End Get
            Set(ByVal value As String)
                _modelDescription = value
            End Set
        End Property


        <ColumnInfo("ConfirmFakturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ConfirmFakturDate() As DateTime
            Get
                Return _ConfirmFakturDate
            End Get
            Set(ByVal value As DateTime)
                _ConfirmFakturDate = value
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

