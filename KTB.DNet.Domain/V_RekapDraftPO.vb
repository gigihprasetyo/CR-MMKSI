#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_RekapDraftPO Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 5/5/2009 - 8:57:50 AM
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
    <Serializable(), TableInfo("V_RekapDraftPO")> _
    Public Class V_RekapDraftPO
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
        Private _totalHarga As Decimal
        Private _totalHargaPP As Decimal
        Private _totalQuantity As Integer
        Private _totalHargaIT As Decimal
        Private _totalHargaLC As Decimal

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pODraftHeader As PODraftHeader



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


        <ColumnInfo("TotalHarga", "{0}")> _
        Public Property TotalHarga() As Decimal
            Get
                Return _totalHarga
            End Get
            Set(ByVal value As Decimal)
                _totalHarga = value
            End Set
        End Property


        <ColumnInfo("TotalHargaPP", "{0}")> _
        Public Property TotalHargaPP() As Decimal
            Get
                Return _totalHargaPP
            End Get
            Set(ByVal value As Decimal)
                _totalHargaPP = value
            End Set
        End Property


        <ColumnInfo("TotalQuantity", "{0}")> _
        Public Property TotalQuantity() As Integer
            Get
                Return _totalQuantity
            End Get
            Set(ByVal value As Integer)
                _totalQuantity = value
            End Set
        End Property


        <ColumnInfo("TotalHargaIT", "{0}")> _
        Public Property TotalHargaIT() As Decimal
            Get
                Return _totalHargaIT
            End Get
            Set(ByVal value As Decimal)
                _totalHargaIT = value
            End Set
        End Property


        <ColumnInfo("TotalHargaLC", "{0}")> _
        Public Property TotalHargaLC() As Decimal
            Get
                Return _totalHargaLC
            End Get
            Set(ByVal value As Decimal)
                _totalHargaLC = value
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
        Public ReadOnly Property PODraftID() As Integer
            Get
                Return Me.ID
            End Get
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("PODraftHeader", "ID", "V_RekapDraftPO", "ID")> _
        Public Property PODraftHeader() As PODraftHeader
            Get
                Try 'If IsNothing(Me._pOHeader) Then '
                    If Not IsNothing(Me._pODraftHeader) AndAlso (Not Me._pODraftHeader.IsLoaded) Then
                        Me._pODraftHeader = CType(DoLoad(GetType(PODraftHeader).ToString(), Me._pODraftHeader.ID), PODraftHeader)
                        Me._pODraftHeader.MarkLoaded()

                    End If

                    Return Me._pODraftHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PODraftHeader)

                Me._pODraftHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pODraftHeader.MarkLoaded()
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

