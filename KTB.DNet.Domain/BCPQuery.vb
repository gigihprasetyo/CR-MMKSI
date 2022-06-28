
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BCPQuery Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 12/17/2012 - 11:33:23 AM
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
    <Serializable(), TableInfo("BCPQuery")> _
    Public Class BCPQuery
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
        Private _isKTB As Byte
        Private _isDealer As Byte
        Private _title As String = String.Empty
        Private _description As String = String.Empty
        Private _viewName As String = String.Empty
        Private _spName As String = String.Empty
        Private _flName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _bCPRoless As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("IsKTB", "{0}")> _
        Public Property IsKTB() As Byte
            Get
                Return _isKTB
            End Get
            Set(ByVal value As Byte)
                _isKTB = value
            End Set
        End Property


        <ColumnInfo("IsDealer", "{0}")> _
        Public Property IsDealer() As Byte
            Get
                Return _isDealer
            End Get
            Set(ByVal value As Byte)
                _isDealer = value
            End Set
        End Property


        <ColumnInfo("Title", "'{0}'")> _
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("ViewName", "'{0}'")> _
        Public Property ViewName() As String
            Get
                Return _viewName
            End Get
            Set(ByVal value As String)
                _viewName = value
            End Set
        End Property

        <ColumnInfo("SPName", "'{0}'")> _
        Public Property SPName() As String
            Get
                Return _spName
            End Get
            Set(ByVal value As String)
                _spName = value
            End Set
        End Property

        <ColumnInfo("FlName", "'{0}'")> _
        Public Property FlName() As String
            Get
                Return _flName
            End Get
            Set(ByVal value As String)
                _flName = value
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



        <RelationInfo("BCPQuery", "ID", "BCPRoles", "BCPQueryID")> _
        Public ReadOnly Property BCPRoless() As System.Collections.ArrayList
            Get
                Try
                    If (Me._bCPRoless.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BCPRoles), "BCPQuery", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BCPRoles), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._bCPRoless = DoLoadArray(GetType(BCPRoles).ToString, criterias)
                    End If

                    Return Me._bCPRoless

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

