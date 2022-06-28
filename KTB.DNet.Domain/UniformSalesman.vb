#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformSalesman Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 10:28:19 AM
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
    <Serializable(), TableInfo("UniformSalesman")> _
    Public Class UniformSalesman
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
        Private _dealerId As Integer
        Private _salesmanLevel As String = String.Empty
        Private _validation As String = String.Empty
        Private _personInCharge As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _uniformDistribution As UniformDistribution
        Private _salesmanHeader As SalesmanHeader



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


        <ColumnInfo("DealerId", "{0}")> _
        Public Property DealerId() As Integer
            Get
                Return _dealerId
            End Get
            Set(ByVal value As Integer)
                _dealerId = value
            End Set
        End Property


        <ColumnInfo("SalesmanLevel", "'{0}'")> _
        Public Property SalesmanLevel() As String
            Get
                Return _salesmanLevel
            End Get
            Set(ByVal value As String)
                _salesmanLevel = value
            End Set
        End Property


        <ColumnInfo("Validation", "'{0}'")> _
        Public Property Validation() As String
            Get
                Return _validation
            End Get
            Set(ByVal value As String)
                _validation = value
            End Set
        End Property


        <ColumnInfo("PersonInCharge", "'{0}'")> _
        Public Property PersonInCharge() As String
            Get
                Return _personInCharge
            End Get
            Set(ByVal value As String)
                _personInCharge = value
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


        <ColumnInfo("UniformDistributionId", "{0}"), _
        RelationInfo("UniformDistribution", "ID", "UniformSalesman", "UniformDistributionId")> _
        Public Property UniformDistribution() As UniformDistribution
            Get
                Try
                    If Not isnothing(Me._uniformDistribution) AndAlso (Not Me._uniformDistribution.IsLoaded) Then

                        Me._uniformDistribution = CType(DoLoad(GetType(UniformDistribution).ToString(), _uniformDistribution.ID), UniformDistribution)
                        Me._uniformDistribution.MarkLoaded()

                    End If

                    Return Me._uniformDistribution

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As UniformDistribution)

                Me._uniformDistribution = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._uniformDistribution.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanId", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "UniformSalesman", "SalesmanId")> _
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

