
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_trClass Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 6/11/2009 - 1:59:59 PM
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
    <Serializable(), TableInfo("v_trClass")> _
    Public Class v_trClass
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
        Private _classID As Integer
        Private _classCode As String = String.Empty
        Private _className As String = String.Empty
        Private _startDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _finishDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _capacity As Integer
        Private _dealerID As Short
        Private _dealerCode As String = String.Empty
        Private _allocated As Integer
        Private _numOfTrainee As Integer
        Private _rowStatus As Short
        Private _areaID As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("ClassID", "{0}")> _
        Public Property ClassID() As Integer
            Get
                Return _classID
            End Get
            Set(ByVal value As Integer)
                _classID = value
            End Set
        End Property


        <ColumnInfo("ClassCode", "'{0}'")> _
        Public Property ClassCode() As String
            Get
                Return _classCode
            End Get
            Set(ByVal value As String)
                _classCode = value
            End Set
        End Property


        <ColumnInfo("ClassName", "'{0}'")> _
        Public Property ClassName() As String
            Get
                Return _className
            End Get
            Set(ByVal value As String)
                _className = value
            End Set
        End Property


        <ColumnInfo("StartDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartDate() As DateTime
            Get
                Return _startDate
            End Get
            Set(ByVal value As DateTime)
                _startDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FinishDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FinishDate() As DateTime
            Get
                Return _finishDate
            End Get
            Set(ByVal value As DateTime)
                _finishDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Capacity", "{0}")> _
        Public Property Capacity() As Integer
            Get
                Return _capacity
            End Get
            Set(ByVal value As Integer)
                _capacity = value
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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("Allocated", "{0}")> _
        Public Property Allocated() As Integer
            Get
                Return _allocated
            End Get
            Set(ByVal value As Integer)
                _allocated = value
            End Set
        End Property


        <ColumnInfo("NumOfTrainee", "{0}")> _
        Public Property NumOfTrainee() As Integer
            Get
                Return _numOfTrainee
            End Get
            Set(ByVal value As Integer)
                _numOfTrainee = value
            End Set
        End Property

        <ColumnInfo("AreaID", "{0}")> _
        Public Property AreaID() As Short
            Get
                Return _areaID
            End Get
            Set(ByVal value As Short)
                _areaID = value
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

