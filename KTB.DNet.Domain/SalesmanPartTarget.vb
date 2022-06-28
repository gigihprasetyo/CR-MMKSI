
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanPartTarget Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2013 - 1:26:19 PM
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
    <Serializable(), TableInfo("SalesmanPartTarget")> _
    Public Class SalesmanPartTarget
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
        Private _year As Short
        Private _month As Short
        Private _period As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _target As Decimal
        Private _realization As Decimal
        Private _persentage As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesmanHeader As SalesmanHeader

        Private _salesmanPartTargetHists As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Year", "{0}")> _
        Public Property Year() As Short
            Get
                Return _year
            End Get
            Set(ByVal value As Short)
                _year = value
            End Set
        End Property


        <ColumnInfo("Month", "{0}")> _
        Public Property Month() As Short
            Get
                Return _month
            End Get
            Set(ByVal value As Short)
                _month = value
            End Set
        End Property


        <ColumnInfo("Period", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property Period() As DateTime
            Get
                Return _period
            End Get
            Set(ByVal value As DateTime)
                _period = value
            End Set
        End Property


        <ColumnInfo("Target", "{0}")> _
        Public Property Target() As Decimal
            Get
                Return _target
            End Get
            Set(ByVal value As Decimal)
                _target = value
            End Set
        End Property


        <ColumnInfo("Realization", "{0}")> _
        Public Property Realization() As Decimal
            Get
                Return _realization
            End Get
            Set(ByVal value As Decimal)
                _realization = value
            End Set
        End Property


        <ColumnInfo("Persentage", "#,##0")> _
        Public Property Persentage() As Decimal
            Get
                Return _persentage
            End Get
            Set(ByVal value As Decimal)
                _persentage = value
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


        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "SalesmanPartTarget", "SalesmanHeaderID")> _
        Public Property SalesmanHeader() As SalesmanHeader
            Get
                Try
                    If Not isnothing(Me._salesmanHeader) AndAlso (Not Me._salesmanHeader.IsLoaded) Then

                        Me._salesmanHeader = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeader.ID), SalesmanHeader)
                        Me._salesmanHeader.MarkLoaded()

                    End If

                    Return Me._salesmanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeader.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("SalesmanPartTarget", "ID", "SalesmanPartTargetHist", "SalesmanPartTargetID")> _
        Public ReadOnly Property SalesmanPartTargetHists() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanPartTargetHists.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanPartTargetHist), "SalesmanPartTarget", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanPartTargetHist), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanPartTargetHists = DoLoadArray(GetType(SalesmanPartTargetHist).ToString, criterias)
                    End If

                    Return Me._salesmanPartTargetHists

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

