
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceSubParameterAttribute Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/03/2020 - 13:28:46
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
    <Serializable(), TableInfo("CcCSPerformanceSubParameterAttribute")> _
    Public Class CcCSPerformanceSubParameterAttribute
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
        Private _ccCSPerformanceSubParameter As CcCSPerformanceSubParameter
        Private _ccAttribute As CcAttribute
        Private _ccPeriodFrom As CcPeriod
        Private _ccPeriodTo As CcPeriod
        Private _minimumScore As Decimal
        Private _status As Short
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



        <ColumnInfo("CcCSPerformanceSubParameterID", "{0}"), _
    RelationInfo("CcCSPerformanceSubParameter", "ID", "CcCSPerformanceSubParameterAttribute", "CcCSPerformanceSubParameterID")> _
        Public Property CcCSPerformanceSubParameter() As CcCSPerformanceSubParameter
            Get
                Try
                    If Not IsNothing(Me._ccCSPerformanceSubParameter) AndAlso (Not Me._ccCSPerformanceSubParameter.IsLoaded) Then

                        Me._ccCSPerformanceSubParameter = CType(DoLoad(GetType(CcCSPerformanceSubParameter).ToString(), _ccCSPerformanceSubParameter.ID), CcCSPerformanceSubParameter)
                        Me._ccCSPerformanceSubParameter.MarkLoaded()

                    End If

                    Return Me._ccCSPerformanceSubParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceSubParameter)

                Me._ccCSPerformanceSubParameter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCSPerformanceSubParameter.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcAttributeID", "{0}"), _
  RelationInfo("CcAttribute", "ID", "CcCSPerformanceSubParameterAttribute", "CcAttributeID")> _
        Public Property CcAttribute() As CcAttribute
            Get
                Try
                    If Not IsNothing(Me._ccAttribute) AndAlso (Not Me._ccAttribute.IsLoaded) Then

                        Me._ccAttribute = CType(DoLoad(GetType(CcAttribute).ToString(), _ccAttribute.ID), CcAttribute)
                        Me._ccAttribute.MarkLoaded()

                    End If

                    Return Me._ccAttribute

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcAttribute)

                Me._ccAttribute = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccAttribute.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcPeriodIDFrom", "{0}"), _
 RelationInfo("CcPeriod", "ID", "CcCSPerformanceSubParameterAttribute", "CcPeriodIDFrom")> _
        Public Property CcPeriodFrom() As CcPeriod
            Get
                Try
                    If Not IsNothing(Me._ccPeriodFrom) AndAlso (Not Me._ccPeriodFrom.IsLoaded) Then

                        Me._ccPeriodFrom = CType(DoLoad(GetType(CcPeriod).ToString(), _ccPeriodFrom.ID), CcPeriod)
                        Me._ccPeriodFrom.MarkLoaded()

                    End If

                    Return Me._ccPeriodFrom

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcPeriod)

                Me._ccPeriodFrom = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccPeriodFrom.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcPeriodIDTo", "{0}"), _
RelationInfo("CcPeriod", "ID", "CcCSPerformanceSubParameterAttribute", "CcPeriodIDTo")> _
        Public Property CcPeriodTo() As CcPeriod
            Get
                Try
                    If Not IsNothing(Me._ccPeriodTo) AndAlso (Not Me._ccPeriodTo.IsLoaded) Then

                        Me._ccPeriodTo = CType(DoLoad(GetType(CcPeriod).ToString(), _ccPeriodTo.ID), CcPeriod)
                        Me._ccPeriodTo.MarkLoaded()

                    End If

                    Return Me._ccPeriodTo

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcPeriod)

                Me._ccPeriodTo = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccPeriodTo.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MinimumPoint", "#,##0")> _
        Public Property MinimumScore As Decimal
            Get
                Return _minimumScore
            End Get
            Set(ByVal value As Decimal)
                _minimumScore = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
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

