
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceClasification Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/6/2018 - 12:59:48 PM
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
    <Serializable(), TableInfo("CcCSPerformanceClasification")> _
    Public Class CcCSPerformanceClasification
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
        Private _ccCSPerformanceSubParameterID As Integer
        Private _ccCSPerformanceMasterID As Integer
        Private _code As String = String.Empty
        Private _name As String = String.Empty
        Private _weight As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _ccCSPerformanceSubParameter As CcCSPerformanceSubParameter
        Private _ccCSPerformanceMaster As CcCSPerformanceMaster


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

        <ColumnInfo("CcCSPerformanceMasterID", "{0}")> _
        Public Property CcCSPerformanceMasterID As Integer
            Get
                Return _ccCSPerformanceMasterID
            End Get
            Set(ByVal value As Integer)
                _ccCSPerformanceMasterID = value
            End Set
        End Property

        <ColumnInfo("CcCSPerformanceMasterID", "{0}"), _
       RelationInfo("cCSPerformanceMaster", "ID", "CcCSPerformanceClasification", "CcCSPerformanceMasterID")> _
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

        <ColumnInfo("CcCSPerformanceSubParameterID", "{0}"), _
       RelationInfo("CcCSPerformanceSubParameter", "ID", "CcCSPerformanceClasification", "CcCSPerformanceSubParameterID")> _
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

        <ColumnInfo("CcCSPerformanceSubParameterID", "{0}")> _
        Public Property CcCSPerformanceSubParameterID As Integer
            Get
                Return _ccCSPerformanceSubParameterID
            End Get
            Set(ByVal value As Integer)
                _ccCSPerformanceSubParameterID = value
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


        <ColumnInfo("Weight", "{0}")> _
        Public Property Weight As Integer
            Get
                Return _weight
            End Get
            Set(ByVal value As Integer)
                _weight = value
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

