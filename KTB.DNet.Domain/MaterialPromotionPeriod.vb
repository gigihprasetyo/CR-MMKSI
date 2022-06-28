#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionPeriod Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2007 - 3:05:35 PM
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
    <Serializable(), TableInfo("MaterialPromotionPeriod")> _
    Public Class MaterialPromotionPeriod
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
        Private _periodName As String = String.Empty
        Private _startDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _endDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _description As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _materialPromotionGIHeaders As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _materialPromotionUploadAllocs As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _materialPromotionAllocations As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _materialPromotionGIGRs As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("PeriodName", "'{0}'")> _
        Public Property PeriodName() As String
            Get
                Return _periodName
            End Get
            Set(ByVal value As String)
                _periodName = value
            End Set
        End Property


        <ColumnInfo("StartDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartDate() As DateTime
            Get
                Return _startDate
            End Get
            Set(ByVal value As DateTime)
                _startDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EndDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EndDate() As DateTime
            Get
                Return _endDate
            End Get
            Set(ByVal value As DateTime)
                _endDate = New DateTime(value.Year, value.Month, value.Day)
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



        '<RelationInfo("MaterialPromotionPeriod", "ID", "MaterialPromotionGIHeader", "MaterialPromotionPeriodID")> _
        'Public ReadOnly Property MaterialPromotionGIHeaders() As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._materialPromotionGIHeaders.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionGIHeader), "MaterialPromotionPeriod", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._materialPromotionGIHeaders = DoLoadArray(GetType(MaterialPromotionGIHeader).ToString, criterias)
        '            End If

        '            Return Me._materialPromotionGIHeaders

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property

        <RelationInfo("MaterialPromotionPeriod", "ID", "MaterialPromotionUploadAlloc", "MaterialPromotionPeriodID")> _
        Public ReadOnly Property MaterialPromotionUploadAllocs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionUploadAllocs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionUploadAlloc), "MaterialPromotionPeriod", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionUploadAlloc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionUploadAllocs = DoLoadArray(GetType(MaterialPromotionUploadAlloc).ToString, criterias)
                    End If

                    Return Me._materialPromotionUploadAllocs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("MaterialPromotionPeriod", "ID", "MaterialPromotionAllocation", "MaterialPromotionPeriodID")> _
        Public ReadOnly Property MaterialPromotionAllocations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionAllocations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionPeriod", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionAllocations = DoLoadArray(GetType(MaterialPromotionAllocation).ToString, criterias)
                    End If

                    Return Me._materialPromotionAllocations

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("MaterialPromotionPeriod", "ID", "MaterialPromotionGIGR", "MaterialPromotionPeriodID")> _
        Public ReadOnly Property MaterialPromotionGIGRs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionGIGRs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotionPeriod", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionGIGRs = DoLoadArray(GetType(MaterialPromotionGIGR).ToString, criterias)
                    End If

                    Return Me._materialPromotionGIGRs

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

