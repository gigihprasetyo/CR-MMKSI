#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformGuide Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 10:47:33 AM
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
    <Serializable(), TableInfo("UniformGuide")> _
    Public Class UniformGuide
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
        Private _uniformImage As Byte()
        Private _information As String = String.Empty
        Private _s As Decimal
        Private _m As Decimal
        Private _l As Decimal
        Private _xL As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _uniformDistribution As UniformDistribution



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


        <ColumnInfo("UniformImage", "{0}")> _
        Public Property UniformImage() As Byte()
            Get
                Return _uniformImage
            End Get
            Set(ByVal value As Byte())
                _uniformImage = value
            End Set
        End Property


        <ColumnInfo("Information", "'{0}'")> _
        Public Property Information() As String
            Get
                Return _information
            End Get
            Set(ByVal value As String)
                _information = value
            End Set
        End Property


        <ColumnInfo("S", "#,##0")> _
        Public Property S() As Decimal
            Get
                Return _s
            End Get
            Set(ByVal value As Decimal)
                _s = value
            End Set
        End Property


        <ColumnInfo("M", "#,##0")> _
        Public Property M() As Decimal
            Get
                Return _m
            End Get
            Set(ByVal value As Decimal)
                _m = value
            End Set
        End Property


        <ColumnInfo("L", "#,##0")> _
        Public Property L() As Decimal
            Get
                Return _l
            End Get
            Set(ByVal value As Decimal)
                _l = value
            End Set
        End Property


        <ColumnInfo("XL", "#,##0")> _
        Public Property XL() As Decimal
            Get
                Return _xL
            End Get
            Set(ByVal value As Decimal)
                _xL = value
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
        RelationInfo("UniformDistribution", "ID", "UniformGuide", "UniformDistributionId")> _
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

