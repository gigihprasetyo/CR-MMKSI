#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUniformAssigned Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/10/2007 - 1:08:54 PM
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
    <Serializable(), TableInfo("SalesmanUniformAssigned")> _
    Public Class SalesmanUniformAssigned
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
        Private _uniformSize As Byte
        Private _isReleased As Byte
        Private _isValidate As Byte
        Private _qty As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesmanUniform As SalesmanUniform
        Private _salesmanHeader As SalesmanHeader

        Private _salesmanUniformOrderDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("UniformSize", "{0}")> _
        Public Property UniformSize() As Byte
            Get
                Return _uniformSize
            End Get
            Set(ByVal value As Byte)
                _uniformSize = value
            End Set
        End Property


        <ColumnInfo("IsReleased", "{0}")> _
        Public Property IsReleased() As Byte
            Get
                Return _isReleased
            End Get
            Set(ByVal value As Byte)
                _isReleased = value
            End Set
        End Property


        <ColumnInfo("IsValidate", "{0}")> _
        Public Property IsValidate() As Byte
            Get
                Return _isValidate
            End Get
            Set(ByVal value As Byte)
                _isValidate = value
            End Set
        End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty() As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
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


        <ColumnInfo("SalesmanUniformID", "{0}"), _
        RelationInfo("SalesmanUniform", "ID", "SalesmanUniformAssigned", "SalesmanUniformID")> _
        Public Property SalesmanUniform() As SalesmanUniform
            Get
                Try
                    If Not isnothing(Me._salesmanUniform) AndAlso (Not Me._salesmanUniform.IsLoaded) Then

                        Me._salesmanUniform = CType(DoLoad(GetType(SalesmanUniform).ToString(), _salesmanUniform.ID), SalesmanUniform)
                        Me._salesmanUniform.MarkLoaded()

                    End If

                    Return Me._salesmanUniform

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanUniform)

                Me._salesmanUniform = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanUniform.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "SalesmanUniformAssigned", "SalesmanHeaderID")> _
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


        <RelationInfo("SalesmanUniformAssigned", "ID", "SalesmanUniformOrderDetail", "SalesmanUniformAssignedID")> _
        Public ReadOnly Property SalesmanUniformOrderDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesmanUniformOrderDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanUniformAssigned", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesmanUniformOrderDetails = DoLoadArray(GetType(SalesmanUniformOrderDetail).ToString, criterias)
                    End If

                    Return Me._salesmanUniformOrderDetails

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

