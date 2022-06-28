#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : IndentPartAllocationDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/6/2007 - 9:51:54 AM
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
    <Serializable(), TableInfo("IndentPartAllocationDetail")> _
    Public Class IndentPartAllocationDetail
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
        Private _allocatedQty As Integer
        Private _pONumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _indentPartDetail As IndentPartDetail
        Private _indentPartAllocationHeader As IndentPartAllocationHeader




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


        <ColumnInfo("AllocatedQty", "{0}")> _
        Public Property AllocatedQty() As Integer
            Get
                Return _allocatedQty
            End Get
            Set(ByVal value As Integer)
                _allocatedQty = value
            End Set
        End Property


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
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


        '<ColumnInfo("IndentPartDetailID", "{0}")> _
        'Public Property IndentPartDetailID() As Integer

        '    Get
        '        Return _indentPartDetailID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _indentPartDetailID = value
        '    End Set
        'End Property
        '<ColumnInfo("IndentPartAllocationHeaderID", "{0}")> _
        'Public Property IndentPartAllocationHeaderID() As Integer

        '    Get
        '        Return _indentPartAllocationHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _indentPartAllocationHeaderID = value
        '    End Set
        'End Property

        <ColumnInfo("IndentPartDetailID", "{0}"), _
               RelationInfo("IndentPartDetail", "ID", "IndentPartAllocationDetail", "IndentPartDetailID")> _
               Public Property IndentPartDetail() As IndentPartDetail
            Get
                Try
                    If Not IsNothing(Me._indentPartDetail) AndAlso (Not Me._indentPartDetail.IsLoaded) Then

                        Me._indentPartDetail = CType(DoLoad(GetType(IndentPartDetail).ToString(), _indentPartDetail.ID), IndentPartDetail)
                        Me._indentPartDetail.MarkLoaded()


                    End If

                    Return Me._indentPartDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As IndentPartDetail)

                Me._indentPartDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._indentPartDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("IndentPartAllocationHeaderID", "{0}"), _
       RelationInfo("IndentPartAllocationHeader", "ID", "IndentPartAllocationDetail", "IndentPartAllocationHeaderID")> _
       Public Property IndentPartAllocationHeader() As IndentPartAllocationHeader
            Get
                Try
                    If Not IsNothing(Me._indentPartAllocationHeader) AndAlso (Not Me._indentPartAllocationHeader.IsLoaded) Then

                        Me._indentPartAllocationHeader = CType(DoLoad(GetType(IndentPartAllocationHeader).ToString(), _indentPartAllocationHeader.ID), IndentPartAllocationHeader)
                        Me._indentPartAllocationHeader.MarkLoaded()

                    End If

                    Return Me._indentPartAllocationHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As IndentPartAllocationHeader)

                Me._indentPartAllocationHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._indentPartAllocationHeader.MarkLoaded()
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
        Public ReadOnly Property ReaminQty() As Integer
            Get
                Dim xqty As Integer
                Dim ipd As IndentPartDetail
                ipd = Me.IndentPartDetail
                xqty = ipd.Qty - Me.AllocatedQty
                Return xqty
            End Get
        End Property
#End Region

    End Class
End Namespace

