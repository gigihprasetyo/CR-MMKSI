#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCParameterDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 4/23/2018 - 8:30:19 AM
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
    <Serializable(), TableInfo("WSCParameterDetail")> _
    Public Class WSCParameterDetail
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
        Private _kind As Integer
        Private _operator As Integer
        Private _value As String = String.Empty
        Private _reasonCode As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _wSCParameterHeader As WSCParameterHeader
        Private _wSCParameterCondition As WSCParameterCondition

        Private _conditionIndex As Integer = -1


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

        <ColumnInfo("WSCParameterHeaderID", "{0}"), _
        RelationInfo("WSCParameterHeader", "ID", "WSCParameterDetail", "WSCParameterHeaderID")> _
        Public Property WSCParameterHeader() As WSCParameterHeader
            Get
                Try
                    If Not IsNothing(Me._wSCParameterHeader) AndAlso (Not Me._wSCParameterHeader.IsLoaded) Then

                        Me._wSCParameterHeader = CType(DoLoad(GetType(WSCParameterHeader).ToString(), _wSCParameterHeader.ID), WSCParameterHeader)
                        Me._wSCParameterHeader.MarkLoaded()

                    End If

                    Return Me._wSCParameterHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As WSCParameterHeader)

                Me._wSCParameterHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._wSCParameterHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("Kind", "{0}")> _
        Public Property Kind As Integer
            Get
                Return _kind
            End Get
            Set(ByVal value As Integer)
                _kind = value
            End Set
        End Property


        <ColumnInfo("Operator", "{0}")> _
        Public Property Operators As Integer
            Get
                Return _operator
            End Get
            Set(ByVal value As Integer)
                _operator = value
            End Set
        End Property


        <ColumnInfo("Value", "'{0}'")> _
        Public Property Value As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
            End Set
        End Property


        <ColumnInfo("ReasonCode", "'{0}'")> _
        Public Property ReasonCode As String
            Get
                Return _reasonCode
            End Get
            Set(ByVal value As String)
                _reasonCode = value
            End Set
        End Property

        <ColumnInfo("WSCParameterConditionID", "{0}"), _
        RelationInfo("WSCParameterCondition", "ID", "WSCParameterDetail", "WSCParameterConditionID")> _
        Public Property WSCParameterCondition() As WSCParameterCondition
            Get
                Try
                    If Not IsNothing(Me._wSCParameterCondition) AndAlso (Not Me._wSCParameterCondition.IsLoaded) Then

                        Me._wSCParameterCondition = CType(DoLoad(GetType(WSCParameterCondition).ToString(), _wSCParameterCondition.ID), WSCParameterCondition)
                        Me._wSCParameterCondition.MarkLoaded()

                    End If

                    Return Me._wSCParameterCondition

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As WSCParameterCondition)

                Me._wSCParameterCondition = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._wSCParameterCondition.MarkLoaded()
                End If
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

        Public Property ConditionIndex As String
            Get
                Return _conditionIndex
            End Get
            Set(ByVal value As String)
                _conditionIndex = value
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

