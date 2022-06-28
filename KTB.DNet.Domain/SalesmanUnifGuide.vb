#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUnifGuide Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 22/10/2007 - 13:18:13
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
    <Serializable(), TableInfo("SalesmanUnifGuide")> _
    Public Class SalesmanUnifGuide
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
        Private _description As String = String.Empty
        Private _sSize As Integer
        Private _mSize As Integer
        Private _lSize As Integer
        Private _xLSize As Integer
        Private _xXLSize As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesmanUniform As SalesmanUniform



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


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("SSize", "{0}")> _
        Public Property SSize() As Integer
            Get
                Return _sSize
            End Get
            Set(ByVal value As Integer)
                _sSize = value
            End Set
        End Property


        <ColumnInfo("MSize", "{0}")> _
        Public Property MSize() As Integer
            Get
                Return _mSize
            End Get
            Set(ByVal value As Integer)
                _mSize = value
            End Set
        End Property


        <ColumnInfo("LSize", "{0}")> _
        Public Property LSize() As Integer
            Get
                Return _lSize
            End Get
            Set(ByVal value As Integer)
                _lSize = value
            End Set
        End Property


        <ColumnInfo("XLSize", "{0}")> _
        Public Property XLSize() As Integer
            Get
                Return _xLSize
            End Get
            Set(ByVal value As Integer)
                _xLSize = value
            End Set
        End Property


        <ColumnInfo("XXLSize", "{0}")> _
        Public Property XXLSize() As Integer
            Get
                Return _xXLSize
            End Get
            Set(ByVal value As Integer)
                _xXLSize = value
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
        RelationInfo("SalesmanUniform", "ID", "SalesmanUnifGuide", "SalesmanUniformID")> _
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

