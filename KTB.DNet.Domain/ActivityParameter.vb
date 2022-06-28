#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ActivityParameter Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/25/2009 - 4:49:44 PM
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
    <Serializable(), TableInfo("ActivityParameter")> _
    Public Class ActivityParameter
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
        Private _activityDateStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _activityDateEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _activityName As String = String.Empty
        Private _activityYear As Integer
        Private _fileNameMaterial As String = String.Empty
        Private _fileNameJuklak As String = String.Empty
        Private _fileNamePendukung As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealerID As Short
        Private _salesmanAreaID As Integer
        Private _categoryID As Byte
        Private _vehicleTypeID As Short
        Private _activityTypeID As Integer



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


        <ColumnInfo("ActivityDateStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ActivityDateStart() As DateTime
            Get
                Return _activityDateStart
            End Get
            Set(ByVal value As DateTime)
                _activityDateStart = value
            End Set
        End Property


        <ColumnInfo("ActivityDateEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ActivityDateEnd() As DateTime
            Get
                Return _activityDateEnd
            End Get
            Set(ByVal value As DateTime)
                _activityDateEnd = value
            End Set
        End Property


        <ColumnInfo("ActivityName", "'{0}'")> _
        Public Property ActivityName() As String
            Get
                Return _activityName
            End Get
            Set(ByVal value As String)
                _activityName = value
            End Set
        End Property


        <ColumnInfo("ActivityYear", "{0}")> _
        Public Property ActivityYear() As Integer
            Get
                Return _activityYear
            End Get
            Set(ByVal value As Integer)
                _activityYear = value
            End Set
        End Property


        <ColumnInfo("FileNameMaterial", "'{0}'")> _
        Public Property FileNameMaterial() As String
            Get
                Return _fileNameMaterial
            End Get
            Set(ByVal value As String)
                _fileNameMaterial = value
            End Set
        End Property


        <ColumnInfo("FileNameJuklak", "'{0}'")> _
        Public Property FileNameJuklak() As String
            Get
                Return _fileNameJuklak
            End Get
            Set(ByVal value As String)
                _fileNameJuklak = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung", "'{0}'")> _
        Public Property FileNamePendukung() As String
            Get
                Return _fileNamePendukung
            End Get
            Set(ByVal value As String)
                _fileNamePendukung = value
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


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Short

            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property
        <ColumnInfo("SalesmanAreaID", "{0}")> _
        Public Property SalesmanAreaID() As Integer

            Get
                Return _salesmanAreaID
            End Get
            Set(ByVal value As Integer)
                _salesmanAreaID = value
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
        <ColumnInfo("VehicleTypeID", "{0}")> _
        Public Property VehicleTypeID() As Short

            Get
                Return _vehicleTypeID
            End Get
            Set(ByVal value As Short)
                _vehicleTypeID = value
            End Set
        End Property
        <ColumnInfo("ActivityTypeID", "{0}")> _
        Public Property ActivityTypeID() As Integer

            Get
                Return _activityTypeID
            End Get
            Set(ByVal value As Integer)
                _activityTypeID = value
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

