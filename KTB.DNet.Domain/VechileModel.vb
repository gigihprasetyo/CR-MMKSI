
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VechileModel Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 01/10/2018 - 12:59:43
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
    <Serializable(), TableInfo("VechileModel")> _
    Public Class VechileModel
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
        'Private _sAPCode As String = String.Empty
        Private _vechileModelCode As String = String.Empty
        Private _description As String = String.Empty
        Private _vechileModelIndCode As String = String.Empty
        Private _indDescription As String = String.Empty
        Private _salesFlag As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _category As Category

        Private _vechileTypes As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _subCategoryVehicleToModels As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _subCategoryVehicleToModel As SubCategoryVehicleToModel
        Private _indDescriptionVehicleModelCode As String = String.Empty

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


        '<ColumnInfo("SAPCode", "'{0}'")> _
        'Public Property SAPCode As String
        '    Get
        '        Return _sAPCode
        '    End Get
        '    Set(ByVal value As String)
        '        _sAPCode = value
        '    End Set
        'End Property


        <ColumnInfo("VechileModelCode", "'{0}'")> _
        Public Property VechileModelCode As String
            Get
                Return _vechileModelCode
            End Get
            Set(ByVal value As String)
                _vechileModelCode = value
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


        <ColumnInfo("VechileModelIndCode", "'{0}'")> _
        Public Property VechileModelIndCode As String
            Get
                Return _vechileModelIndCode
            End Get
            Set(ByVal value As String)
                _vechileModelIndCode = value
            End Set
        End Property


        <ColumnInfo("IndDescription", "'{0}'")> _
        Public Property IndDescription As String
            Get
                Return _indDescription
            End Get
            Set(ByVal value As String)
                _indDescription = value
            End Set
        End Property


        <ColumnInfo("SalesFlag", "'{0}'")> _
        Public Property SalesFlag As Integer
            Get
                Return _salesFlag
            End Get
            Set(ByVal value As Integer)
                _salesFlag = value
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


        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "VechileModel", "CategoryID")> _
        Public Property Category As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("VechileModel", "ID", "VechileType", "ModelID")> _
        Public ReadOnly Property VechileTypes As System.Collections.ArrayList
            Get
                Try
                    If (Me._vechileTypes.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(VechileType), "VechileModel", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._vechileTypes = DoLoadArray(GetType(VechileType).ToString, criterias)
                    End If

                    Return Me._vechileTypes

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("VechileModelID", "{0}"), _
RelationInfo("VechileModel", "ID", "SubCategoryVehicleToModel", "VechileModelID")> _
        Public Property SubCategoryVehicleToModel() As SubCategoryVehicleToModel
            Get
                Try
                    If IsNothing(Me._subCategoryVehicleToModel) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._subCategoryVehicleToModels = DoLoadArray(GetType(SubCategoryVehicleToModel).ToString, criterias)
                        Me._subCategoryVehicleToModel = CType(_subCategoryVehicleToModels(0), SubCategoryVehicleToModel)
                    End If

                    'If Not IsNothing(Me._subCategoryVehicleToModel) AndAlso (Not Me._subCategoryVehicleToModel.IsLoaded) Then

                    '    Me._subCategoryVehicleToModel = CType(DoLoad(GetType(SubCategoryVehicleToModel).ToString(), _subCategoryVehicleToModel.ID), SubCategoryVehicleToModel)
                    '    Me._subCategoryVehicleToModel.MarkLoaded()

                    'End If

                    Return Me._subCategoryVehicleToModel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SubCategoryVehicleToModel)

                Me._subCategoryVehicleToModel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._subCategoryVehicleToModel.MarkLoaded()
                End If
            End Set
        End Property


        Public Property IndDescriptionVehicleModelCode As String
            Get
                Return String.Format("{0} ({1})", IndDescription, VechileModelCode)
            End Get
            Set(ByVal value As String)
                _indDescriptionVehicleModelCode = value
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

