
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportActivity Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 27/05/2019 - 9:52:00
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
    <Serializable(), TableInfo("BabitEventReportActivity")> _
    Public Class BabitEventReportActivity
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
        Private _activityType As Integer
        Private _description As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _babitEventReportHeader As BabitEventReportHeader

        Private _flagRow As String = String.Empty
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


        <ColumnInfo("ActivityType", "{0}")> _
        Public Property ActivityType As Integer
            Get
                Return _activityType
            End Get
            Set(ByVal value As Integer)
                _activityType = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
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


        <ColumnInfo("BabitEventReportHeaderID", "{0}"), _
        RelationInfo("BabitEventReportHeader", "ID", "BabitEventReportActivity", "BabitEventReportHeaderID")> _
        Public Property BabitEventReportHeader As BabitEventReportHeader
            Get
                Try
                    If Not IsNothing(Me._babitEventReportHeader) AndAlso (Not Me._babitEventReportHeader.IsLoaded) Then

                        Me._babitEventReportHeader = CType(DoLoad(GetType(BabitEventReportHeader).ToString(), _babitEventReportHeader.ID), BabitEventReportHeader)
                        Me._babitEventReportHeader.MarkLoaded()

                    End If

                    Return Me._babitEventReportHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitEventReportHeader)

                Me._babitEventReportHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitEventReportHeader.MarkLoaded()
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
        <ColumnInfo("RowStatus", "{0}")> _
        Public Property flagRow As String
            Get
                Return _flagRow
            End Get
            Set(ByVal value As String)
                _flagRow = value
            End Set
        End Property

#End Region

    End Class
End Namespace

