
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_CeilingPO Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 1/2/2012 - 4:36:17 PM
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
    <Serializable(), TableInfo("up_RetrieveFreeService_Service_Reminder")> _
    Public Class up_RetrieveFreeService_Service_Reminder
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"
        Private _rowStatus As Short
        Private _iD As Integer
        Private _KM_LAST As Integer
        Private _noHP As String = String.Empty
        Private _OpenFakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _PKTDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _chassisNumber As String = String.Empty
        Private _FSType As String = String.Empty
        Private _fskindID As Integer
        Private _name As String = String.Empty
        Private _cityName As String = String.Empty
        Private _KindDescription As String
        Private _fakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _expiredDateByOpenFakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _expiredDateByPKTDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        <ColumnInfo("FSKindID", "{0}")> _
        Public Property FSKindID() As Integer
            Get
                Return _fskindID
            End Get
            Set(ByVal value As Integer)
                _fskindID = value
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As String
            Get
                Return _iD
            End Get
            Set(ByVal value As String)
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

        <ColumnInfo("KM_LAST", "{0}")> _
        Public Property KM_LAST() As Integer
            Get
                Return _KM_LAST
            End Get
            Set(ByVal value As Integer)
                _KM_LAST = value
            End Set
        End Property

        <ColumnInfo("NoHP", "'{0}'")> _
        Public Property NoHP() As String
            Get
                Return _noHP
            End Get
            Set(ByVal value As String)
                _noHP = value
            End Set
        End Property

        <ColumnInfo("CityName", "'{0}'")> _
        Public Property CityName() As String
            Get
                Return _cityName
            End Get
            Set(ByVal value As String)
                _cityName = value
            End Set
        End Property

        <ColumnInfo("FakturDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturDate() As DateTime
            Get
                Return _fakturDate
            End Get
            Set(ByVal value As DateTime)
                _fakturDate = value
            End Set
        End Property

        <ColumnInfo("ExpiredDateByOpenFakturDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ExpiredDateByOpenFakturDate() As DateTime
            Get
                Return _expiredDateByOpenFakturDate
            End Get
            Set(ByVal value As DateTime)
                _expiredDateByOpenFakturDate = value
            End Set
        End Property

        <ColumnInfo("ExpiredDateByPKTDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ExpiredDateByPKTDate() As DateTime
            Get
                Return _expiredDateByPKTDate
            End Get
            Set(ByVal value As DateTime)
                _expiredDateByPKTDate = value
            End Set
        End Property

        <ColumnInfo("OpenFakturDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property OpenFakturDate() As DateTime
            Get
                Return _OpenFakturDate
            End Get
            Set(ByVal value As DateTime)
                _OpenFakturDate = value
            End Set
        End Property


        <ColumnInfo("PKTDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PKTDate() As DateTime
            Get
                Return _PKTDate
            End Get
            Set(ByVal value As DateTime)
                _PKTDate = value
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

        <ColumnInfo("FSType", "'{0}'")> _
        Public Property FSType() As String
            Get
                Return _FSType
            End Get
            Set(ByVal value As String)
                _FSType = value
            End Set
        End Property

        <ColumnInfo("KindDescription", "'{0}'")> _
        Public Property KindDescription() As String
            Get
                Return _KindDescription
            End Get
            Set(value As String)
                _KindDescription = value
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

