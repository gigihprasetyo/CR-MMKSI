
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceParameter Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/6/2018 - 12:27:20 PM
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
    <Serializable(), TableInfo("CcCSPerformanceParameter")> _
    Public Class CcCSPerformanceParameter
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
        Private _ccCSPerformanceMasterID As Integer
        Private _parentID As Integer
        Private _code As String = String.Empty
        Private _name As String = String.Empty
        Private _weight As Decimal
        Private _sequence As Integer
        Private _level As Short
        Private _functionName As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _ccCSPerformanceMaster As CcCSPerformanceMaster
        Private _ccCustomerCategoryID As Integer
        Private _CcCustomerCategory As CcCustomerCategory
        Private _dealer As Dealer
        Private _status As Short

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

        <ColumnInfo("FunctionName", "{0}")> _
        Public Property FunctionName As Short
            Get
                Return _functionName
            End Get
            Set(ByVal value As Short)
                _functionName = value
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
        RelationInfo("CcCSPerformanceMaster", "ID", "CcCSPerformanceParameter", "CcCSPerformanceMasterID")> _
        Public Property CcCSPerformanceMaster() As CcCSPerformanceMaster
            Get
                Try
                    If Not IsNothing(Me._ccCSPerformanceMaster) AndAlso (Not Me._ccCSPerformanceMaster.IsLoaded) Then

                        Me._ccCSPerformanceMaster = CType(DoLoad(GetType(CcCSPerformanceMaster).ToString(), _ccCSPerformanceMasterID), CcCSPerformanceMaster)
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


        <ColumnInfo("ParentID", "{0}")> _
        Public Property ParentID As Integer
            Get
                Return _parentID
            End Get
            Set(ByVal value As Integer)
                _parentID = value
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


        <ColumnInfo("Weight", "'{0}'")> _
        Public Property Weight As Decimal
            Get
                Return _weight
            End Get
            Set(ByVal value As Decimal)
                _weight = value
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


        <ColumnInfo("level", "{0}")> _
        Public Property level As Short
            Get
                Return _level
            End Get
            Set(ByVal value As Short)
                _level = value
            End Set
        End Property

        <ColumnInfo("CcCustomerCategoryID", "{0}")> _
        Public Property CcCustomerCategoryID As Integer
            Get
                Return _ccCustomerCategoryID
            End Get
            Set(ByVal value As Integer)
                _ccCustomerCategoryID = value
            End Set
        End Property

        <ColumnInfo("CcCustomerCategoryID", "{0}"), _
        RelationInfo("CcCustomerCategory", "ID", "CcCSPerformanceParameter", "CcCustomerCategoryID")> _
        Public Property CcCustomerCategory() As CcCustomerCategory
            Get
                Try
                    If Not IsNothing(Me._CcCustomerCategory) AndAlso (Not Me._CcCustomerCategory.IsLoaded) Then

                        Me._CcCustomerCategory = CType(DoLoad(GetType(CcCustomerCategory).ToString(), _ccCustomerCategoryID), CcCustomerCategory)
                        Me._CcCustomerCategory.MarkLoaded()

                    End If

                    Return Me._CcCustomerCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCustomerCategory)

                Me._CcCustomerCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._CcCustomerCategory.MarkLoaded()
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

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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
                Case Else
                    result = "Status tidak terdaftar"
            End Select

            Return result
        End Function

#End Region

    End Class

End Namespace

