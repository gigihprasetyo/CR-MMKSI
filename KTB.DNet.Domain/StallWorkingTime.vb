#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : StallWorkingTime Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:01:07 PM
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
    <Serializable(), TableInfo("StallWorkingTime")> _
    Public Class StallWorkingTime
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
        Private _tanggal As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _timeStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _timeEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _restTimeStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _restTimeEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isHoliday As Short
        Private _visitType As Short
        Private _notes As String
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _stallMaster As StallMaster
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

        <ColumnInfo("Tanggal", "'{0:yyyy/MM/dd}'")> _
        Public Property Tanggal() As DateTime
            Get
                Return _tanggal
            End Get
            Set(ByVal value As DateTime)
                _tanggal = value
            End Set
        End Property

        <ColumnInfo("TimeStart", "{0:yyyy/MM/dd HH:mm:ss}")> _
        Public Property TimeStart() As DateTime
            Get
                Return _timeStart
            End Get
            Set(ByVal value As DateTime)
                _timeStart = value
            End Set
        End Property

        <ColumnInfo("TimeEnd", "{0:yyyy/MM/dd HH:mm:ss}")> _
        Public Property TimeEnd() As DateTime
            Get
                Return _timeEnd
            End Get
            Set(ByVal value As DateTime)
                _timeEnd = value
            End Set
        End Property

        <ColumnInfo("RestTimeStart", "{0:yyyy/MM/dd HH:mm:ss}")> _
        Public Property RestTimeStart() As DateTime
            Get
                Return _restTimeStart
            End Get
            Set(ByVal value As DateTime)
                _restTimeStart = value
            End Set
        End Property

        <ColumnInfo("RestTimeEnd", "{0:yyyy/MM/dd HH:mm:ss}")> _
        Public Property RestTimeEnd() As DateTime
            Get
                Return _restTimeEnd
            End Get
            Set(ByVal value As DateTime)
                _restTimeEnd = value
            End Set
        End Property

        <ColumnInfo("IsHoliday", "{0}")> _
        Public Property IsHoliday() As Short
            Get
                Return _isHoliday
            End Get
            Set(ByVal value As Short)
                _isHoliday = value
            End Set
        End Property

        <ColumnInfo("VisitType", "{0}")> _
        Public Property VisitType() As Short
            Get
                Return _visitType
            End Get
            Set(ByVal value As Short)
                _visitType = value
            End Set
        End Property

        <ColumnInfo("Notes", "{0}")> _
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy() As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime() As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "StallWorkingTime", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("StallMasterID", "{0}"), _
        RelationInfo("StallMaster", "ID", "StallWorkingTime", "StallMasterID")> _
        Public Property StallMaster() As StallMaster
            Get
                Try
                    If Not IsNothing(Me._stallMaster) AndAlso (Not Me._stallMaster.IsLoaded) Then

                        Me._stallMaster = CType(DoLoad(GetType(StallMaster).ToString(), _stallMaster.ID), StallMaster)
                        Me._stallMaster.MarkLoaded()

                    End If

                    Return Me._stallMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As StallMaster)

                Me._stallMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
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

#Region "Non_generated Properties"
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

