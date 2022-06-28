
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetRequest Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2016 - 2:36:05 PM
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
    <Serializable(), TableInfo("FleetRequest")> _
    Public Class FleetRequest
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _noRegRequest As String = String.Empty
        Private _tanggalPengajuan As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _namaKonsumen As String = String.Empty
        Private _statusKonsumen As Short
        Private _profilBisnis As String = String.Empty
        Private _kebutuhanUnit As String = String.Empty
        Private _mulaiPengadaan As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _selesaiPengadaan As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Byte
        Private _batalKonfirmasiNote As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _attachment As String = String.Empty

        Private _fleetMasterDealer As FleetMasterDealer
        Private _iD As Integer

        Private _freeServices As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _fleetFakturs As System.Collections.ArrayList = New System.Collections.ArrayList()


#End Region

#Region "Public Properties"

        <ColumnInfo("NoRegRequest", "'{0}'")> _
        Public Property NoRegRequest As String
            Get
                Return _noRegRequest
            End Get
            Set(ByVal value As String)
                _noRegRequest = value
            End Set
        End Property


        <ColumnInfo("TanggalPengajuan", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TanggalPengajuan As DateTime
            Get
                Return _tanggalPengajuan
            End Get
            Set(ByVal value As DateTime)
                _tanggalPengajuan = value
            End Set
        End Property


        <ColumnInfo("NamaKonsumen", "'{0}'")> _
        Public Property NamaKonsumen As String
            Get
                Return _namaKonsumen
            End Get
            Set(ByVal value As String)
                _namaKonsumen = value
            End Set
        End Property


        <ColumnInfo("StatusKonsumen", "{0}")> _
        Public Property StatusKonsumen As Short
            Get
                Return _statusKonsumen
            End Get
            Set(ByVal value As Short)
                _statusKonsumen = value
            End Set
        End Property


        <ColumnInfo("ProfilBisnis", "'{0}'")> _
        Public Property ProfilBisnis As String
            Get
                Return _profilBisnis
            End Get
            Set(ByVal value As String)
                _profilBisnis = value
            End Set
        End Property


        <ColumnInfo("KebutuhanUnit", "'{0}'")> _
        Public Property KebutuhanUnit As String
            Get
                Return _kebutuhanUnit
            End Get
            Set(ByVal value As String)
                _kebutuhanUnit = value
            End Set
        End Property


        <ColumnInfo("MulaiPengadaan", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property MulaiPengadaan As DateTime
            Get
                Return _mulaiPengadaan
            End Get
            Set(ByVal value As DateTime)
                _mulaiPengadaan = value
            End Set
        End Property


        <ColumnInfo("SelesaiPengadaan", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SelesaiPengadaan As DateTime
            Get
                Return _selesaiPengadaan
            End Get
            Set(ByVal value As DateTime)
                _selesaiPengadaan = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
            End Set
        End Property


        <ColumnInfo("BatalKonfirmasiNote", "'{0}'")> _
        Public Property BatalKonfirmasiNote As String
            Get
                Return _batalKonfirmasiNote
            End Get
            Set(ByVal value As String)
                _batalKonfirmasiNote = value
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


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property


        <ColumnInfo("FleetMasterDealerID", "{0}"), _
        RelationInfo("FleetMasterDealer", "ID", "FleetRequest", "FleetMasterDealerID")> _
        Public Property FleetMasterDealer As FleetMasterDealer
            Get
                Try
                    If Not isnothing(Me._fleetMasterDealer) AndAlso (Not Me._fleetMasterDealer.IsLoaded) Then

                        Me._fleetMasterDealer = CType(DoLoad(GetType(FleetMasterDealer).ToString(), _fleetMasterDealer.ID), FleetMasterDealer)
                        Me._fleetMasterDealer.MarkLoaded()

                    End If

                    Return Me._fleetMasterDealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As FleetMasterDealer)

                Me._fleetMasterDealer = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._fleetMasterDealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer

            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <RelationInfo("FleetRequest", "ID", "FreeService", "FleetRequestID")> _
        Public ReadOnly Property FreeServices As System.Collections.ArrayList
            Get
                Try
                    If (Me._freeServices.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(FreeService), "FleetRequest", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._freeServices = DoLoadArray(GetType(FreeService).ToString, criterias)
                    End If

                    Return Me._freeServices

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("FleetRequest", "ID", "FleetFaktur", "FleetRequestID")> _
        Public ReadOnly Property FleetFakturs As System.Collections.ArrayList
            Get
                Try
                    If (Me._fleetFakturs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(FleetFaktur), "FleetRequest", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._fleetFakturs = DoLoadArray(GetType(FleetFaktur).ToString, criterias)
                    End If

                    Return Me._fleetFakturs

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

