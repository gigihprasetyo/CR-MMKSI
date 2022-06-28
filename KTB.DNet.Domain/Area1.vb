#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : Area1 Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/28/2005 - 5:57:51 PM
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
    <Serializable(), TableInfo("Area1")> _
    Public Class Area1
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
        Private _areaCode As String = String.Empty
        Private _description As String = String.Empty
        Private _pICSales As String = String.Empty
        Private _pICServices As String = String.Empty
        Private _pICSpareparts As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _mainArea As MainArea
        Private _dealers As System.Collections.ArrayList = New System.Collections.ArrayList

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


        <ColumnInfo("AreaCode", "'{0}'")> _
        Public Property AreaCode() As String
            Get
                Return _areaCode
            End Get
            Set(ByVal value As String)
                _areaCode = value
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

        <ColumnInfo("PICSales", "'{0}'")> _
        Public Property PICSales As String
            Get
                Return _pICSales
            End Get
            Set(ByVal value As String)
                _pICSales = value
            End Set
        End Property

        <ColumnInfo("PICServices", "'{0}'")> _
        Public Property PICServices As String
            Get
                Return _pICServices
            End Get
            Set(ByVal value As String)
                _pICServices = value
            End Set
        End Property

        <ColumnInfo("PICSpareparts", "'{0}'")> _
        Public Property PICSpareparts As String
            Get
                Return _pICSpareparts
            End Get
            Set(ByVal value As String)
                _pICSpareparts = value
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

        <ColumnInfo("MainAreaID", "{0}"), _
        RelationInfo("MainArea", "ID", "Area1", "MainAreaID")> _
              Public Property MainArea As MainArea
            Get
                Try
                    If Not isnothing(Me._mainArea) AndAlso (Not Me._mainArea.IsLoaded) Then

                        Me._mainArea = CType(DoLoad(GetType(MainArea).ToString(), _mainArea.ID), MainArea)
                        Me._mainArea.MarkLoaded()

                    End If

                    Return Me._mainArea

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MainArea)

                Me._mainArea = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mainArea.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("Area1", "ID", "Dealer", "Area1ID")> _
          Public ReadOnly Property Dealers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dealers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(Dealer), "Area1", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dealers = DoLoadArray(GetType(Dealer).ToString, criterias)
                    End If

                    Return Me._dealers

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

#Region "Custom Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

    End Class
End Namespace

