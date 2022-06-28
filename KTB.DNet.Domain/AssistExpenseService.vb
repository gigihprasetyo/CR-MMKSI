
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistExpenseService Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 10:29:34 AM
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
    <Serializable(), TableInfo("AssistExpenseService")> _
    Public Class AssistExpenseService
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
        Private _expenseGroup As String = String.Empty
        Private _description As String = String.Empty
        Private _unitType As String = String.Empty
        Private _value As Decimal
        Private _remarksSystem As String = String.Empty
        Private _validateSystemStatus As Short
        Private _statusAktif As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _assistuploadLog As AssistUploadLog


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


        <ColumnInfo("ExpenseGroup", "'{0}'")> _
        Public Property ExpenseGroup As String
            Get
                Return _expenseGroup
            End Get
            Set(ByVal value As String)
                _expenseGroup = value
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


        <ColumnInfo("UnitType", "'{0}'")> _
        Public Property UnitType As String
            Get
                Return _unitType
            End Get
            Set(ByVal value As String)
                _unitType = value
            End Set
        End Property


        <ColumnInfo("Value", "#,##0")> _
        Public Property Value As Decimal
            Get
                Return _value
            End Get
            Set(ByVal value As Decimal)
                _value = value
            End Set
        End Property


        <ColumnInfo("RemarksSystem", "'{0}'")> _
        Public Property RemarksSystem As String
            Get
                Return _remarksSystem
            End Get
            Set(ByVal value As String)
                _remarksSystem = value
            End Set
        End Property


        <ColumnInfo("ValidateSystemStatus", "{0}")> _
        Public Property ValidateSystemStatus As Short
            Get
                Return _validateSystemStatus
            End Get
            Set(ByVal value As Short)
                _validateSystemStatus = value
            End Set
        End Property


        <ColumnInfo("StatusAktif", "{0}")> _
        Public Property StatusAktif As Short
            Get
                Return _statusAktif
            End Get
            Set(ByVal value As Short)
                _statusAktif = value
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



        <ColumnInfo("AssistUploadLogID", "{0}"), _
        RelationInfo("AssistUploadLog", "ID", "AssistExpenseService", "AssistUploadLogID")> _
        Public Property AssistUploadLog() As AssistUploadLog
            Get
                Try
                    If Not IsNothing(Me._assistuploadLog) AndAlso (Not Me._assistuploadLog.IsLoaded) Then

                        Me._assistuploadLog = CType(DoLoad(GetType(AssistUploadLog).ToString(), _assistuploadLog.ID), AssistUploadLog)
                        Me._assistuploadLog.MarkLoaded()

                    End If

                    Return Me._assistuploadLog

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AssistUploadLog)

                Me._assistuploadLog = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._assistuploadLog.MarkLoaded()
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

#End Region

    End Class
End Namespace

