
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LogisticPrice Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 8/28/2017 - 11:51:09 AM
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
    <Serializable(), TableInfo("LogisticPrice")> _
    Public Class LogisticPrice
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _regionCode As String = String.Empty
        Private _regionDescription As String = String.Empty
        Private _sAPModel As String = String.Empty
        Private _effectiveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _logisticPrice As Decimal
        Private _pPn As Decimal
        Private _totalPPn As Decimal
        Private _totalLogisticCost As Decimal
        Private _status As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _iD As Integer



#End Region

#Region "Public Properties"

        <ColumnInfo("RegionCode", "'{0}'")> _
        Public Property RegionCode As String
            Get
                Return _regionCode
            End Get
            Set(ByVal value As String)
                _regionCode = value
            End Set
        End Property


        <ColumnInfo("RegionDescription", "'{0}'")> _
        Public Property RegionDescription As String
            Get
                Return _regionDescription
            End Get
            Set(ByVal value As String)
                _regionDescription = value
            End Set
        End Property


        <ColumnInfo("SAPModel", "'{0}'")> _
        Public Property SAPModel As String
            Get
                Return _sAPModel
            End Get
            Set(ByVal value As String)
                _sAPModel = value
            End Set
        End Property


        <ColumnInfo("EffectiveDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EffectiveDate As DateTime
            Get
                Return _effectiveDate
            End Get
            Set(ByVal value As DateTime)
                _effectiveDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("LogisticPrice", "{0}")> _
        Public Property LogisticPrice As Decimal
            Get
                Return _logisticPrice
            End Get
            Set(ByVal value As Decimal)
                _logisticPrice = value
            End Set
        End Property


        <ColumnInfo("PPn", "#,##0")> _
        Public Property PPn As Decimal
            Get
                Return _pPn
            End Get
            Set(ByVal value As Decimal)
                _pPn = value
            End Set
        End Property

        Public Property TotalPPn As Decimal
            Get
                Return Math.Round(_logisticPrice * (_pPn / 100), 0)
            End Get
            Set(ByVal value As Decimal)
                _totalPPn = Math.Round(_logisticPrice * (_pPn / 100), 0)
            End Set
        End Property

        Public Property TotalLogisticPrice As Decimal
            Get
                Return LogisticPrice + TotalPPn
            End Get
            Set(ByVal value As Decimal)
                _totalLogisticCost = LogisticPrice + TotalPPn
            End Set
        End Property

        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
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


        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer

            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
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

