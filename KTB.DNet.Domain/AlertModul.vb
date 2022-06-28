#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AlertModul Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 21/09/2007 - 8:46:11
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
    <Serializable(), TableInfo("AlertModul")> _
    Public Class AlertModul
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
        Private _code As String = String.Empty
        Private _description As String = String.Empty
        Private _enumClassName As String = String.Empty
        Private _enumAssemblyName As String = String.Empty
        Private _enumMethodToCall As String = String.Empty
        Private _enumStatusIDPropertName As String = String.Empty
        Private _enumStatusNamePropertyName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _alertCategory As AlertCategory


        Private _alertMaster As AlertMaster = New AlertMaster(0)

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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
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


        <ColumnInfo("EnumClassName", "'{0}'")> _
        Public Property EnumClassName() As String
            Get
                Return _enumClassName
            End Get
            Set(ByVal value As String)
                _enumClassName = value
            End Set
        End Property


        <ColumnInfo("EnumAssemblyName", "'{0}'")> _
        Public Property EnumAssemblyName() As String
            Get
                Return _enumAssemblyName
            End Get
            Set(ByVal value As String)
                _enumAssemblyName = value
            End Set
        End Property


        <ColumnInfo("EnumMethodToCall", "'{0}'")> _
        Public Property EnumMethodToCall() As String
            Get
                Return _enumMethodToCall
            End Get
            Set(ByVal value As String)
                _enumMethodToCall = value
            End Set
        End Property


        <ColumnInfo("EnumStatusIDPropertName", "'{0}'")> _
        Public Property EnumStatusIDPropertName() As String
            Get
                Return _enumStatusIDPropertName
            End Get
            Set(ByVal value As String)
                _enumStatusIDPropertName = value
            End Set
        End Property


        <ColumnInfo("EnumStatusNamePropertyName", "'{0}'")> _
        Public Property EnumStatusNamePropertyName() As String
            Get
                Return _enumStatusNamePropertyName
            End Get
            Set(ByVal value As String)
                _enumStatusNamePropertyName = value
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


        <ColumnInfo("AlertCategoryID", "{0}"), _
        RelationInfo("AlertCategory", "ID", "AlertModul", "AlertCategoryID")> _
        Public Property AlertCategory() As AlertCategory
            Get
                Try
                    If Not isnothing(Me._alertCategory) AndAlso (Not Me._alertCategory.IsLoaded) Then

                        Me._alertCategory = CType(DoLoad(GetType(AlertCategory).ToString(), _alertCategory.ID), AlertCategory)
                        Me._alertCategory.MarkLoaded()

                    End If

                    Return Me._alertCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AlertCategory)

                Me._alertCategory = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._alertCategory.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("ID", "{0}"), _
        RelationInfo("AlertModul", "ID", "AlertMaster", "AlertModulID")> _
        Public ReadOnly Property AlertMaster() As AlertMaster
            Get
                Try
                    If Not isnothing(Me._alertMaster) AndAlso (Not Me._alertMaster.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AlertMaster), "AlertModul", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(AlertMaster).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._alertMaster = CType(tempColl(0), AlertMaster)
                        Else
                            Me._alertMaster = Nothing
                        End If
                    End If

                    Return Me._alertMaster

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

