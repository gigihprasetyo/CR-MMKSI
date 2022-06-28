
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BCPDynamicQuery Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 21/02/2019 - 15:02:33
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
    <Serializable(), TableInfo("BCPDynamicQuery")> _
    Public Class BCPDynamicQuery
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
        Private _bCPQueryID As Integer
        Private _originalFieldName As String = String.Empty
        Private _convertFieldName As String = String.Empty
        Private _fieldNameInAlias As String = String.Empty
        Private _defaultWhereClause As String = String.Empty
        Private _addtionalWhereClause As String = String.Empty
        Private _field3 As String = String.Empty
        Private _status As String = String.Empty
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


        <ColumnInfo("BCPQueryID", "{0}")> _
        Public Property BCPQueryID As Integer
            Get
                Return _bCPQueryID
            End Get
            Set(ByVal value As Integer)
                _bCPQueryID = value
            End Set
        End Property


        <ColumnInfo("OriginalFieldName", "'{0}'")> _
        Public Property OriginalFieldName As String
            Get
                Return _originalFieldName
            End Get
            Set(ByVal value As String)
                _originalFieldName = value
            End Set
        End Property


        <ColumnInfo("ConvertFieldName", "'{0}'")> _
        Public Property ConvertFieldName As String
            Get
                Return _convertFieldName
            End Get
            Set(ByVal value As String)
                _convertFieldName = value
            End Set
        End Property


        <ColumnInfo("FieldNameInAlias", "'{0}'")> _
        Public Property FieldNameInAlias As String
            Get
                Return _fieldNameInAlias
            End Get
            Set(ByVal value As String)
                _fieldNameInAlias = value
            End Set
        End Property


        <ColumnInfo("DefaultWhereClause", "'{0}'")> _
        Public Property DefaultWhereClause As String
            Get
                Return _defaultWhereClause
            End Get
            Set(ByVal value As String)
                _defaultWhereClause = value
            End Set
        End Property


        <ColumnInfo("AddtionalWhereClause", "'{0}'")> _
        Public Property AddtionalWhereClause As String
            Get
                Return _addtionalWhereClause
            End Get
            Set(ByVal value As String)
                _addtionalWhereClause = value
            End Set
        End Property


        <ColumnInfo("Field3", "'{0}'")> _
        Public Property Field3 As String
            Get
                Return _field3
            End Get
            Set(ByVal value As String)
                _field3 = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
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

