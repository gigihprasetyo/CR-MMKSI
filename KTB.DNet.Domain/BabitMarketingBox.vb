
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitMarketingBox Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 03/10/2019 - 14:04:58
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
    <Serializable(), TableInfo("BabitMarketingBox")> _
    Public Class BabitMarketingBox
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Long)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Long
        Private _subMissionID As String = String.Empty
        Private _dealercode As String = String.Empty
        Private _dealerID As Integer
        Private _babitType As String = String.Empty
        Private _startDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _endDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eventName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eventLocation As String = String.Empty
        Private _fileTimeLastModified As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Long
            Get
                Return _iD
            End Get
            Set(ByVal value As Long)
                _iD = value
            End Set
        End Property


        <ColumnInfo("SubMissionID", "'{0}'")> _
        Public Property SubMissionID As String
            Get
                Return _subMissionID
            End Get
            Set(ByVal value As String)
                _subMissionID = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealercode
            End Get
            Set(ByVal value As String)
                _dealercode = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("BabitType", "'{0}'")> _
        Public Property BabitType As String
            Get
                Return _babitType
            End Get
            Set(ByVal value As String)
                _babitType = value
            End Set
        End Property

        <ColumnInfo("FileTimeLastModified", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FileTimeLastModified As DateTime
            Get
                Return _fileTimeLastModified
            End Get
            Set(ByVal value As DateTime)
                _fileTimeLastModified = value
            End Set
        End Property


        <ColumnInfo("StartDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartDate As DateTime
            Get
                Return _startDate
            End Get
            Set(ByVal value As DateTime)
                _startDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EndDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EndDate As DateTime
            Get
                Return _endDate
            End Get
            Set(ByVal value As DateTime)
                _endDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EventName", "'{0}'")> _
        Public Property EventName As String
            Get
                Return _eventName
            End Get
            Set(ByVal value As String)
                _eventName = value
            End Set
        End Property


        <ColumnInfo("EventLocation", "'{0}'")> _
        Public Property EventLocation As String
            Get
                Return _eventLocation
            End Get
            Set(ByVal value As String)
                _eventLocation = value
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

