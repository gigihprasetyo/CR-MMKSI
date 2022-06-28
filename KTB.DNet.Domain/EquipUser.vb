#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipUser Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/3/2009 - 11:57:22
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
    <Serializable(), TableInfo("EquipUser")> _
    Public Class EquipUser
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Integer)
            _id = id
        End Sub

#End Region

#Region "Private Variables"

        Private _id As Integer
        Private _userName As String = String.Empty
        Private _email As String = String.Empty
        Private _tipe As String = String.Empty
        Private _positionCC As String = String.Empty
        Private _groupType As Short
        Private _note As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




#End Region

#Region "Public Properties"

        <ColumnInfo("id", "{0}")> _
        Public Property id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property


        <ColumnInfo("UserName", "'{0}'")> _
        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                _userName = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("Tipe", "'{0}'")> _
        Public Property Tipe() As String
            Get
                Return _tipe
            End Get
            Set(ByVal value As String)
                _tipe = value
            End Set
        End Property


        <ColumnInfo("PositionCC", "'{0}'")> _
        Public Property PositionCC() As String
            Get
                Return _positionCC
            End Get
            Set(ByVal value As String)
                _positionCC = value
            End Set
        End Property


        <ColumnInfo("GroupType", "{0}")> _
        Public Property GroupType() As Short
            Get
                Return _groupType
            End Get
            Set(ByVal value As Short)
                _groupType = value
            End Set
        End Property


        <ColumnInfo("Note", "'{0}'")> _
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
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

        Public ReadOnly Property EquipUserGroupDesc() As String
            
            Get
                'If (_groupType = EquipUserGroup.Approved) Then
                '    Return "Approve Pengajuan SPPO"
                'ElseIf (_groupType = EquipUserGroup.Deposit_B) Then
                '    Return "Pengajuan SPPO Deposit B"
                'ElseIf (_groupType = EquipUserGroup.Deposit_C) Then
                '    Return "Pemindahan SPPO Deposit B ke C"
                'ElseIf (_groupType = EquipUserGroup.Reject) Then
                '    Return "Tolak Pengajuan SPPO"
                'ElseIf (_groupType = EquipUserGroup.Pengajuan_Estimasi) Then
                '    Return "Pengajuan Estimasi"
                'ElseIf (_groupType = EquipUserGroup.Konfirmasi_Harga_Estimasi) Then
                '    Return "Konfirmasi Harga Estimasi"
                'Else : Return ""
                'End If
                If (_groupType = EquipUserGroup.Approved) Then
                    Return "Rilis Deposit B"
                ElseIf (_groupType = EquipUserGroup.Deposit_B) Then
                    Return "Pengajuan Order Deposit B"
                ElseIf (_groupType = EquipUserGroup.Deposit_C) Then
                    Return "Pengajuan Order Deposit C"
                ElseIf (_groupType = EquipUserGroup.Reject) Then
                    Return "Tolak Deposit B"
                ElseIf (_groupType = EquipUserGroup.Pengajuan_Estimasi) Then
                    Return "Pengajuan Estimasi"
                ElseIf (_groupType = EquipUserGroup.Konfirmasi_Harga_Estimasi) Then
                    Return "Konfirmasi Harga Estimasi"
                Else : Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property EquipUserTipeDesc() As String
            Get
                If (_tipe = EquipUserTipe.CC_TO) Then
                    Return "CC" 'EquipUserTipe.CC_TO.ToString()
                ElseIf (_tipe = EquipUserTipe.TO_SENT) Then
                    Return "To" 'EquipUserTipe.TO_SENT.ToString()
                Else : Return ""
                End If
            End Get
        End Property

        Public Enum EquipUserTipe
            TO_SENT
            CC_TO
        End Enum

        Public Enum EquipUserGroup
            Approved '5. Rilis Deposit B
            Reject '6. Reject Dep B
            Deposit_C '4. Pengajuan Order DepC
            Deposit_B '3. Pengajuan Order DepB (waktu order) 
            Pengajuan_Estimasi '1. Pengajuan Estimasi (waktu kirim) 
            Konfirmasi_Harga_Estimasi '2. Konfirmasi Harga Estimasi(setelah update harga) 

            '1. Pengajuan Estimasi (waktu kirim) 
            '2. Konfirmasi Harga Estimasi(setelah update harga) 
            '3. Pengajuan Order DepB (waktu order) 
            '4. Pengajuan Order DepC
            '5. Rilis Deposit B
            '6. Reject Dep B

        End Enum

#End Region

    End Class
End Namespace

