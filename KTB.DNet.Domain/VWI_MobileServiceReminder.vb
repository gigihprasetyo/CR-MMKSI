#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_MobileServiceReminder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:23:06 PM
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
    <Serializable(), TableInfo("VWI_MobileServiceReminder")> _
    Public Class VWI_MobileServiceReminder
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
        Private _dealerCode As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _serviceReminderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _kindCode As String = String.Empty
        Private _kindDescription As String = String.Empty
        Private _remark As String = String.Empty
        Private _reminderType As Integer
        Private _reminderDelta As Integer




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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        <ColumnInfo("ServiceReminderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ServiceReminderDate As DateTime
            Get
                Return _serviceReminderDate
            End Get
            Set(ByVal value As DateTime)
                _serviceReminderDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("KindCode", "'{0}'")> _
        Public Property KindCode As String
            Get
                Return _kindCode
            End Get
            Set(ByVal value As String)
                _kindCode = value
            End Set
        End Property


        <ColumnInfo("KindDescription", "'{0}'")> _
        Public Property KindDescription As String
            Get
                Return _kindDescription
            End Get
            Set(ByVal value As String)
                _kindDescription = value
            End Set
        End Property


        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
            End Set
        End Property


        <ColumnInfo("ReminderType", "{0}")> _
        Public Property ReminderType As Integer
            Get
                Return _reminderType
            End Get
            Set(ByVal value As Integer)
                _reminderType = value
            End Set
        End Property


        <ColumnInfo("ReminderDelta", "{0}")> _
        Public Property ReminderDelta As Integer
            Get
                Return _reminderDelta
            End Get
            Set(ByVal value As Integer)
                _reminderDelta = value
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
