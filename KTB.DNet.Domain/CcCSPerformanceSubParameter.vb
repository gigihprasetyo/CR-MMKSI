
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceSubParameter Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/6/2018 - 12:44:47 PM
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
Imports System.Collections.Generic
Imports KTB.DNet.DataMapper.Framework
Imports System.Security.Principal

#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("CcCSPerformanceSubParameter")> _
    Public Class CcCSPerformanceSubParameter
        Inherits DomainObject

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CcCSPerformanceSubParameterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _code As String = String.Empty
        Private _name As String = String.Empty
        Private _sequence As Integer
        Private _type As Short
        Private _functionName As String
        Private _weight As Decimal

        Private _description As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _ccCSPerformanceParameter As CcCSPerformanceParameter
        Private _ccCSPerformanceMaster As CcCSPerformanceMaster

        Private _listOfAttribute As New ArrayList

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

        Public ReadOnly Property EnumIDForDisplayCS As List(Of Object)
            Get

                'Dim objStandardCode As New StandardCode
                'Dim arlStandardCode As New ArrayList

                'Dim cri As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'cri.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "CSP_SPAR_FUNCTION"))

                'arlStandardCode = m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(cri)

                'Dim arrReturn As New List(Of Object)
                'With arrReturn
                '    For x As Integer = 0 To 99
                '        .Add(x)
                '    Next
                'End With
                'Return arrReturn

                Dim arrReturn As New List(Of Object)
                With arrReturn
                    .Add(0)
                    .Add(1)
                    .Add(2)
                    .Add(3)
                    .Add(99)
                End With
                Return arrReturn
            End Get
        End Property

        Public ReadOnly Property EnumIDForDisplayDR As List(Of Object)
            Get
                Dim arrReturn As New List(Of Object)
                With arrReturn
                    .Add(100)
                    .Add(101)
                    .Add(102)
                    .Add(103)
                    .Add(104)
                    .Add(105)
                End With
                Return arrReturn
            End Get
        End Property

        <ColumnInfo("CcCSPerformanceMasterID", "{0}"), _
        RelationInfo("CcCSPerformanceMaster", "ID", "CcCSPerformanceSubParameter", "CcCSPerformanceMasterID")> _
        Public Property CcCSPerformanceMaster() As CcCSPerformanceMaster
            Get
                Try
                    If Not IsNothing(Me._ccCSPerformanceMaster) AndAlso (Not Me._ccCSPerformanceMaster.IsLoaded) Then

                        Me._ccCSPerformanceMaster = CType(DoLoad(GetType(CcCSPerformanceMaster).ToString(), _ccCSPerformanceMaster.ID), CcCSPerformanceMaster)
                        Me._ccCSPerformanceMaster.MarkLoaded()

                    End If

                    Return Me._ccCSPerformanceMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceMaster)

                Me._ccCSPerformanceMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCSPerformanceMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcCSPerformanceParameterID", "{0}"), _
        RelationInfo("CcCSPerformanceParameter", "ID", "CcCSPerformanceSubParameter", "CcCSPerformanceParameterID")> _
        Public Property CcCSPerformanceParameter() As CcCSPerformanceParameter
            Get
                Try
                    If Not IsNothing(Me._ccCSPerformanceParameter) AndAlso (Not Me._ccCSPerformanceParameter.IsLoaded) Then

                        Me._ccCSPerformanceParameter = CType(DoLoad(GetType(CcCSPerformanceParameter).ToString(), _ccCSPerformanceParameter.ID), CcCSPerformanceParameter)
                        Me._ccCSPerformanceParameter.MarkLoaded()

                    End If

                    Return Me._ccCSPerformanceParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceParameter)

                Me._ccCSPerformanceParameter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCSPerformanceParameter.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        <ColumnInfo("Sequence", "{0}")> _
        Public Property Sequence As Integer
            Get
                Return _sequence
            End Get
            Set(ByVal value As Integer)
                _sequence = value
            End Set
        End Property




        <ColumnInfo("Type", "{0}")> _
        Public Property Type As Short
            Get
                Return _type
            End Get
            Set(ByVal value As Short)
                _type = value
            End Set
        End Property


        <ColumnInfo("FunctionName", "{0}")> _
        Public Property FunctionName As String
            Get
                Return _functionName
            End Get
            Set(ByVal value As String)
                _functionName = value
            End Set
        End Property


        <ColumnInfo("Weight", "{0,0}")> _
        Public Property Weight As Decimal
            Get
                Return _weight
            End Get
            Set(ByVal value As Decimal)
                _weight = value
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


        Public Property ListOfAttribute() As System.Collections.ArrayList
            Get
                Try
                    If (Me._listOfAttribute.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CcCSPerformanceSubParameterAttribute), "CcCSPerformanceSubParameter.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameterAttribute), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._listOfAttribute = DoLoadArray(GetType(CcCSPerformanceSubParameterAttribute).ToString, criterias)
                    End If

                    Return Me._listOfAttribute

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
            Set(ByVal value As System.Collections.ArrayList)
                _listOfAttribute = value
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
        Public Enum EnumStatus
            ACTIVE = 1
            NON_ACTIVE = 0
            LOCK = 2
            OPEN = 3
        End Enum


        Public Shared Function GetStatusDescription(ByVal status As Integer) As String
            Dim result As String = String.Empty
            Select Case status
                Case 0
                    result = "Tidak Aktif"
                Case 1
                    result = "Aktif"
                Case 2
                    result = "Lock"
                Case 3
                    result = "Open"
                Case Else
                    result = "Status tidak terdaftar"
            End Select

            Return result
        End Function
#End Region

#Region "PublicClass"
        'Public Enum SubParameterMaxValue
        '    Grade = 0
        'End Enum


#End Region

    End Class
End Namespace

