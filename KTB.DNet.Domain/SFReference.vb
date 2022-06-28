
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFReference Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 19/12/2018 - 10:35:25
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
    <Serializable(), TableInfo("SFReference")> _
    Public Class SFReference
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
        Private _refID As Integer
        Private _refTable As String = String.Empty
        Private _salesForceID As String = String.Empty
        Private _freeField2 As String = String.Empty
        Private _isSend As Boolean
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




#End Region

#Region "Public Properties"

        <ColumnInfo("IsSend", "{0}")> _
        Public Property IsSend As Boolean
            Get
                Return _isSend
            End Get
            Set(ByVal value As Boolean)
                _isSend = value
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("RefID", "{0}")> _
        Public Property RefID As Integer
            Get
                Return _refID
            End Get
            Set(ByVal value As Integer)
                _refID = value
            End Set
        End Property


        <ColumnInfo("RefTable", "'{0}'")> _
        Public Property RefTable As String
            Get
                Return _refTable
            End Get
            Set(ByVal value As String)
                _refTable = value
            End Set
        End Property


        <ColumnInfo("SalesForceID", "'{0}'")> _
        Public Property SalesForceID As String
            Get
                Return _salesForceID
            End Get
            Set(ByVal value As String)
                _salesForceID = value
            End Set
        End Property


        <ColumnInfo("FreeField2", "'{0}'")> _
        Public Property FreeField2 As String
            Get
                Return _freeField2
            End Get
            Set(ByVal value As String)
                _freeField2 = value
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

