
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcFactor Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/03/2020 - 11:03:48
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
    <Serializable(), TableInfo("CcFactor")> _
    Public Class CcFactor
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Short)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Short
        Private _ccCustomerCategoryID As Short
        Private _ccCustomerCategory As CcCustomerCategory
        Private _ccVehicleCategoryID As Short
        Private _ccVehicleCategory As CcVehicleCategory
        Private _factorNo As Short
        Private _description As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Short
            Get
                Return _iD
            End Get
            Set(ByVal value As Short)
                _iD = value
            End Set
        End Property


        <ColumnInfo("CcCustomerCategoryID", "{0}")> _
        Public Property CcCustomerCategoryID As Short
            Get
                Return _ccCustomerCategoryID
            End Get
            Set(ByVal value As Short)
                _ccCustomerCategoryID = value
            End Set
        End Property

        <ColumnInfo("CcCustomerCategoryID", "{0}"), _
     RelationInfo("CcCustomerCategory", "ID", "CcFactor", "CcCustomerCategoryID")> _
        Public Property CcCustomerCategory() As CcCustomerCategory
            Get
                Try
                    If Not IsNothing(Me._ccCustomerCategory) AndAlso (Not Me._ccCustomerCategory.IsLoaded) Then

                        Me._ccCustomerCategory = CType(DoLoad(GetType(CcCustomerCategory).ToString(), _ccCustomerCategoryID), CcCustomerCategory)
                        Me._ccCustomerCategory.MarkLoaded()

                    End If

                    Return Me._ccCustomerCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCustomerCategory)

                Me._ccCustomerCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCustomerCategory.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("CcVehicleCategoryID", "{0}")> _
        Public Property CcVehicleCategoryID As Short
            Get
                Return _ccVehicleCategoryID
            End Get
            Set(ByVal value As Short)
                _ccVehicleCategoryID = value
            End Set
        End Property

        <ColumnInfo("CcVehicleCategoryID", "{0}"), _
   RelationInfo("CcVehicleCategory", "ID", "CcFactor", "CcVehicleCategoryID")> _
        Public Property CcVehicleCategory() As CcVehicleCategory
            Get
                Try
                    If Not IsNothing(Me._ccVehicleCategory) AndAlso (Not Me._ccVehicleCategory.IsLoaded) Then

                        Me._ccVehicleCategory = CType(DoLoad(GetType(CcVehicleCategory).ToString(), _ccVehicleCategoryID), CcVehicleCategory)
                        Me._ccVehicleCategory.MarkLoaded()

                    End If

                    Return Me._ccVehicleCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcVehicleCategory)

                Me._ccVehicleCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccVehicleCategory.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("FactorNo", "{0}")> _
        Public Property FactorNo As Short
            Get
                Return _factorNo
            End Get
            Set(ByVal value As Short)
                _factorNo = value
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

