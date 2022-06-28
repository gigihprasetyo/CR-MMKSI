
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKTChangeHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/05/2018 - 19:01:06
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
    <Serializable(), TableInfo("PKTChangeHistory")> _
    Public Class PKTChangeHistory
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
        Private _docType As Integer
        Private _changeType As Byte
        Private _docNumber As String = String.Empty
        Private _oldValue As String = String.Empty
        Private _newValue As String = String.Empty
        Private _oldDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _newDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _oldAmount As Decimal
        Private _newAmount As Decimal
        Private _oldQty As Integer
        Private _newQty As Integer
        Private _description As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("DocType", "{0}")> _
        Public Property DocType As Integer
            Get
                Return _docType
            End Get
            Set(ByVal value As Integer)
                _docType = value
            End Set
        End Property


        <ColumnInfo("ChangeType", "{0}")> _
        Public Property ChangeType As Byte
            Get
                Return _changeType
            End Get
            Set(ByVal value As Byte)
                _changeType = value
            End Set
        End Property


        <ColumnInfo("DocNumber", "'{0}'")> _
        Public Property DocNumber As String
            Get
                Return _docNumber
            End Get
            Set(ByVal value As String)
                _docNumber = value
            End Set
        End Property


        <ColumnInfo("OldValue", "'{0}'")> _
        Public Property OldValue As String
            Get
                Return _oldValue
            End Get
            Set(ByVal value As String)
                _oldValue = value
            End Set
        End Property


        <ColumnInfo("NewValue", "'{0}'")> _
        Public Property NewValue As String
            Get
                Return _newValue
            End Get
            Set(ByVal value As String)
                _newValue = value
            End Set
        End Property


        <ColumnInfo("OldDate", "'{0:yyyy/MM/dd}'")> _
        Public Property OldDate As DateTime
            Get
                Return _oldDate
            End Get
            Set(ByVal value As DateTime)
                _oldDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("NewDate", "'{0:yyyy/MM/dd}'")> _
        Public Property NewDate As DateTime
            Get
                Return _newDate
            End Get
            Set(ByVal value As DateTime)
                _newDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("OldAmount", "{0}")> _
        Public Property OldAmount As Decimal
            Get
                Return _oldAmount
            End Get
            Set(ByVal value As Decimal)
                _oldAmount = value
            End Set
        End Property


        <ColumnInfo("NewAmount", "{0}")> _
        Public Property NewAmount As Decimal
            Get
                Return _newAmount
            End Get
            Set(ByVal value As Decimal)
                _newAmount = value
            End Set
        End Property


        <ColumnInfo("OldQty", "{0}")> _
        Public Property OldQty As Integer
            Get
                Return _oldQty
            End Get
            Set(ByVal value As Integer)
                _oldQty = value
            End Set
        End Property


        <ColumnInfo("NewQty", "{0}")> _
        Public Property NewQty As Integer
            Get
                Return _newQty
            End Get
            Set(ByVal value As Integer)
                _newQty = value
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

