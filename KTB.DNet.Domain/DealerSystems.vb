
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerSystems Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/09/2018 - 14:33:41
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
    <Serializable(), TableInfo("DealerSystems")> _
    Public Class DealerSystems
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
        Private _systemID As Integer
        Private _isSPKMatchFaktur As Boolean
        Private _isOnlyUploadPhotoTenagaPenjual As Boolean
        'Private _isSalesFunnelValidate As Boolean
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        Private _isSPKDNET As Boolean
        Private _goLiveDate As Date = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)

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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DealerSystems", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SystemID", "{0}")> _
        Public Property SystemID As Integer
            Get
                Return _systemID
            End Get
            Set(ByVal value As Integer)
                _systemID = value
            End Set
        End Property


        <ColumnInfo("isSPKMatchFaktur", "{0}")> _
        Public Property isSPKMatchFaktur As Boolean
            Get
                Return _isSPKMatchFaktur
            End Get
            Set(ByVal value As Boolean)
                _isSPKMatchFaktur = value
            End Set
        End Property

        <ColumnInfo("isOnlyUploadPhotoTenagaPenjual", "{0}")> _
        Public Property isOnlyUploadPhotoTenagaPenjual As Boolean
            Get
                Return _isOnlyUploadPhotoTenagaPenjual
            End Get
            Set(ByVal value As Boolean)
                _isOnlyUploadPhotoTenagaPenjual = value
            End Set
        End Property

        '<ColumnInfo("isSalesFunnelValidate", "{0}")> _
        'Public Property isSalesFunnelValidate As Boolean
        '    Get
        '        Return _isSalesFunnelValidate
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _isSalesFunnelValidate = value
        '    End Set
        'End Property

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

        <ColumnInfo("isSPKDNET", "{0}")> _
        Public Property isSPKDNET As Boolean
            Get
                Return _isSPKDNET
            End Get
            Set(ByVal value As Boolean)
                _isSPKDNET = value
            End Set
        End Property

        <ColumnInfo("goLiveDate", "'{0:yyyy/MM/dd}'")> _
        Public Property GoLiveDate As Date
            Get
                Return _goLiveDate
            End Get
            Set(ByVal value As Date)
                _goLiveDate = value
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

