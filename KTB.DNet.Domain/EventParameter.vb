#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventParameter Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/24/2009 - 2:07:02 PM
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
    <Serializable(), TableInfo("EventParameter")> _
    Public Class EventParameter
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
        Private _eventDateStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eventDateEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eventName As String = String.Empty
        Private _eventYear As Integer
        Private _dirTarget As String = String.Empty
        Private _fileNameMaterial As String = String.Empty
        Private _fileNameJuklak As String = String.Empty
        Private _fileNamePendukung1 As String = String.Empty
        Private _fileNamePendukung2 As String = String.Empty
        Private _fileNamePendukung3 As String = String.Empty
        Private _fileNamePendukung4 As String = String.Empty
        Private _fileNamePendukung5 As String = String.Empty
        Private _fileNamePendukung6 As String = String.Empty
        Private _fileNamePendukung7 As String = String.Empty
        Private _fileNamePendukung8 As String = String.Empty
        Private _fileNamePendukung9 As String = String.Empty
        Private _fileNamePendukung10 As String = String.Empty
        Private _eventStatus As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _activityType As ActivityType
        Private _salesmanArea As SalesmanArea
        Private _category As Category
        Private _vechileType As VechileType
        Private _dealer As Dealer

        Private _eventLaporanPenjualans As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _eventProposals As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("EventDateStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property EventDateStart() As DateTime
            Get
                Return _eventDateStart
            End Get
            Set(ByVal value As DateTime)
                _eventDateStart = value
            End Set
        End Property


        <ColumnInfo("EventDateEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property EventDateEnd() As DateTime
            Get
                Return _eventDateEnd
            End Get
            Set(ByVal value As DateTime)
                _eventDateEnd = value
            End Set
        End Property


        <ColumnInfo("EventName", "'{0}'")> _
        Public Property EventName() As String
            Get
                Return _eventName
            End Get
            Set(ByVal value As String)
                _eventName = value
            End Set
        End Property


        <ColumnInfo("EventYear", "{0}")> _
        Public Property EventYear() As Integer
            Get
                Return _eventYear
            End Get
            Set(ByVal value As Integer)
                _eventYear = value
            End Set
        End Property


        <ColumnInfo("DirTarget", "'{0}'")> _
        Public Property DirTarget() As String
            Get
                Return _dirTarget
            End Get
            Set(ByVal value As String)
                _dirTarget = value
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


        <ColumnInfo("FileNamePendukung1", "'{0}'")> _
        Public Property FileNamePendukung1() As String
            Get
                Return _fileNamePendukung1
            End Get
            Set(ByVal value As String)
                _fileNamePendukung1 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung2", "'{0}'")> _
        Public Property FileNamePendukung2() As String
            Get
                Return _fileNamePendukung2
            End Get
            Set(ByVal value As String)
                _fileNamePendukung2 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung3", "'{0}'")> _
        Public Property FileNamePendukung3() As String
            Get
                Return _fileNamePendukung3
            End Get
            Set(ByVal value As String)
                _fileNamePendukung3 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung4", "'{0}'")> _
        Public Property FileNamePendukung4() As String
            Get
                Return _fileNamePendukung4
            End Get
            Set(ByVal value As String)
                _fileNamePendukung4 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung5", "'{0}'")> _
        Public Property FileNamePendukung5() As String
            Get
                Return _fileNamePendukung5
            End Get
            Set(ByVal value As String)
                _fileNamePendukung5 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung6", "'{0}'")> _
        Public Property FileNamePendukung6() As String
            Get
                Return _fileNamePendukung6
            End Get
            Set(ByVal value As String)
                _fileNamePendukung6 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung7", "'{0}'")> _
        Public Property FileNamePendukung7() As String
            Get
                Return _fileNamePendukung7
            End Get
            Set(ByVal value As String)
                _fileNamePendukung7 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung8", "'{0}'")> _
        Public Property FileNamePendukung8() As String
            Get
                Return _fileNamePendukung8
            End Get
            Set(ByVal value As String)
                _fileNamePendukung8 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung9", "'{0}'")> _
        Public Property FileNamePendukung9() As String
            Get
                Return _fileNamePendukung9
            End Get
            Set(ByVal value As String)
                _fileNamePendukung9 = value
            End Set
        End Property


        <ColumnInfo("FileNamePendukung10", "'{0}'")> _
        Public Property FileNamePendukung10() As String
            Get
                Return _fileNamePendukung10
            End Get
            Set(ByVal value As String)
                _fileNamePendukung10 = value
            End Set
        End Property


        <ColumnInfo("EventStatus", "{0}")> _
        Public Property EventStatus() As Byte
            Get
                Return _eventStatus
            End Get
            Set(ByVal value As Byte)
                _eventStatus = value
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


        <ColumnInfo("ActivityTypeID", "{0}"), _
        RelationInfo("ActivityType", "ID", "EventParameter", "ActivityTypeID")> _
        Public Property ActivityType() As ActivityType
            Get
                Try
                    If Not isnothing(Me._activityType) AndAlso (Not Me._activityType.IsLoaded) Then

                        Me._activityType = CType(DoLoad(GetType(ActivityType).ToString(), _activityType.ID), ActivityType)
                        Me._activityType.MarkLoaded()

                    End If

                    Return Me._activityType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ActivityType)

                Me._activityType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._activityType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanAreaID", "{0}"), _
        RelationInfo("SalesmanArea", "ID", "EventParameter", "SalesmanAreaID")> _
        Public Property SalesmanArea() As SalesmanArea
            Get
                Try
                    If Not isnothing(Me._salesmanArea) AndAlso (Not Me._salesmanArea.IsLoaded) Then

                        Me._salesmanArea = CType(DoLoad(GetType(SalesmanArea).ToString(), _salesmanArea.ID), SalesmanArea)
                        Me._salesmanArea.MarkLoaded()

                    End If

                    Return Me._salesmanArea

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanArea)

                Me._salesmanArea = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanArea.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "EventParameter", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not isnothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VehicleTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "EventParameter", "VehicleTypeID")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If Not isnothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "EventParameter", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("EventParameter", "ID", "EventLaporanPenjualan", "EventParameterID")> _
        Public ReadOnly Property EventLaporanPenjualans() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventLaporanPenjualans.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventLaporanPenjualan), "EventParameter", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventLaporanPenjualan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventLaporanPenjualans = DoLoadArray(GetType(EventLaporanPenjualan).ToString, criterias)
                    End If

                    Return Me._eventLaporanPenjualans

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("EventParameter", "ID", "EventProposal", "EventParameterID")> _
        Public ReadOnly Property EventProposals() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventProposals.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventProposal), "EventParameter", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventProposals = DoLoadArray(GetType(EventProposal).ToString, criterias)
                    End If

                    Return Me._eventProposals

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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

