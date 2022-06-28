
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBReceipt Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 5/27/2016 - 6:31:09 PM
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
    <Serializable(), TableInfo("DepositBReceipt")> _
    Public Class DepositBReceipt
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
        Private _noRegKuitansi As String = String.Empty
        Private _nomorKuitansi As String = String.Empty
        Private _tanggalKuitansi As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _tanggalTransfer As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _tanggalPelunasan As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _jVNumber As String = String.Empty
        Private _namaPejabat As String = String.Empty
        Private _jabatan As String = String.Empty
        Private _keterangan As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _depositBPencairanHeader As DepositBPencairanHeader



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


        <ColumnInfo("NoRegKuitansi", "'{0}'")> _
        Public Property NoRegKuitansi As String
            Get
                Return _noRegKuitansi
            End Get
            Set(ByVal value As String)
                _noRegKuitansi = value
            End Set
        End Property


        <ColumnInfo("NomorKuitansi", "'{0}'")> _
        Public Property NomorKuitansi As String
            Get
                Return _nomorKuitansi
            End Get
            Set(ByVal value As String)
                _nomorKuitansi = value
            End Set
        End Property


        <ColumnInfo("TanggalKuitansi", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TanggalKuitansi As DateTime
            Get
                Return _tanggalKuitansi
            End Get
            Set(ByVal value As DateTime)
                _tanggalKuitansi = value
            End Set
        End Property


        <ColumnInfo("TanggalTransfer", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TanggalTransfer As DateTime
            Get
                Return _tanggalTransfer
            End Get
            Set(ByVal value As DateTime)
                _tanggalTransfer = value
            End Set
        End Property


        <ColumnInfo("TanggalPelunasan", "'{0:yyyy/MM/dd}'")> _
        Public Property TanggalPelunasan As DateTime
            Get
                Return _tanggalPelunasan
            End Get
            Set(ByVal value As DateTime)
                _tanggalPelunasan = value
            End Set
        End Property

      


        <ColumnInfo("JVNumber", "'{0}'")> _
        Public Property JVNumber As String
            Get
                Return _jVNumber
            End Get
            Set(ByVal value As String)
                _jVNumber = value
            End Set
        End Property

        <ColumnInfo("NamaPejabat", "'{0}'")> _
        Public Property NamaPejabat As String
            Get
                Return _namaPejabat
            End Get
            Set(ByVal value As String)
                _namaPejabat = value
            End Set
        End Property


        <ColumnInfo("Jabatan", "'{0}'")> _
        Public Property Jabatan As String
            Get
                Return _jabatan
            End Get
            Set(ByVal value As String)
                _jabatan = value
            End Set
        End Property


        <ColumnInfo("Keterangan", "'{0}'")> _
        Public Property Keterangan As String
            Get
                Return _keterangan
            End Get
            Set(ByVal value As String)
                _keterangan = value
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


        <ColumnInfo("DepositBPencairanHeaderID", "{0}"), _
        RelationInfo("DepositBPencairanHeader", "ID", "DepositBReceipt", "DepositBPencairanHeaderID")> _
        Public Property DepositBPencairanHeader As DepositBPencairanHeader
            Get
                Try
                    If Not isnothing(Me._depositBPencairanHeader) AndAlso (Not Me._depositBPencairanHeader.IsLoaded) Then

                        Me._depositBPencairanHeader = CType(DoLoad(GetType(DepositBPencairanHeader).ToString(), _depositBPencairanHeader.ID), DepositBPencairanHeader)
                        Me._depositBPencairanHeader.MarkLoaded()

                    End If

                    Return Me._depositBPencairanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBPencairanHeader)

                Me._depositBPencairanHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBPencairanHeader.MarkLoaded()
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

