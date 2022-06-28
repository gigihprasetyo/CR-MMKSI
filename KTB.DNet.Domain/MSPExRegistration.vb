
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExRegistration Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 12:13:55 PM
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
    <Serializable(), TableInfo("MSPExRegistration")> _
    Public Class MSPExRegistration
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
        Private _dealerID As Short
        Private _mSPCustomerID As Integer
        Private _chassisMasterID As Integer
        Private _mileAge As Integer
        Private _mSPExMasterID As Integer
        Private _regNumber As String = String.Empty
        Private _validDateTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validKMTo As Integer
        Private _status As Short
        Private _isTransfertoSAP As Short
        Private _warrantyValidDateTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _warrantyValidKMTo As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _prefix As String = String.Empty

        Private _dealer As Dealer
        Private _mSPCustomer As MSPCustomer
        Private _chassisMaster As ChassisMaster
        Private _mSPExMaster As MSPExMaster


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


        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID As Short
        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Short)
        '        _dealerID = value
        '    End Set
        'End Property


        '<ColumnInfo("MSPCustomerID", "{0}")> _
        'Public Property MSPCustomerID As Integer
        '    Get
        '        Return _mSPCustomerID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _mSPCustomerID = value
        '    End Set
        'End Property


        '<ColumnInfo("ChassisMasterID", "{0}")> _
        'Public Property ChassisMasterID As Integer
        '    Get
        '        Return _chassisMasterID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _chassisMasterID = value
        '    End Set
        'End Property


        <ColumnInfo("MileAge", "{0}")> _
        Public Property MileAge As Integer
            Get
                Return _mileAge
            End Get
            Set(ByVal value As Integer)
                _mileAge = value
            End Set
        End Property


        '<ColumnInfo("MSPExMasterID", "{0}")> _
        'Public Property MSPExMasterID As Integer
        '    Get
        '        Return _mSPExMasterID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _mSPExMasterID = value
        '    End Set
        'End Property



        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property

        <ColumnInfo("ValidDateTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidDateTo As DateTime
            Get
                Return _validDateTo
            End Get
            Set(ByVal value As DateTime)
                _validDateTo = value
            End Set
        End Property


        <ColumnInfo("ValidKMTo", "{0}")> _
        Public Property ValidKMTo As Integer
            Get
                Return _validKMTo
            End Get
            Set(ByVal value As Integer)
                _validKMTo = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("IsTransfertoSAP", "{0}")> _
        Public Property IsTransfertoSAP As Short
            Get
                Return _isTransfertoSAP
            End Get
            Set(ByVal value As Short)
                _isTransfertoSAP = value
            End Set
        End Property


        <ColumnInfo("WarrantyValidDateTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property WarrantyValidDateTo As DateTime
            Get
                Return _warrantyValidDateTo
            End Get
            Set(ByVal value As DateTime)
                _warrantyValidDateTo = value
            End Set
        End Property


        <ColumnInfo("WarrantyValidKMTo", "{0}")> _
        Public Property WarrantyValidKMTo As Integer
            Get
                Return _warrantyValidKMTo
            End Get
            Set(ByVal value As Integer)
                _warrantyValidKMTo = value
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

        Public Property Prefix As String
            Get
                Return _Prefix
            End Get
            Set(ByVal value As String)
                _Prefix = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MSPExRegistration", "DealerID")> _
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


        <ColumnInfo("MSPCustomerID", "{0}"), _
        RelationInfo("MSPCustomer", "ID", "MSPExRegistration", "MSPCustomerID")> _
        Public Property MSPCustomer() As MSPCustomer
            Get
                Try
                    If Not IsNothing(Me._mSPCustomer) AndAlso (Not Me._mSPCustomer.IsLoaded) Then

                        Me._mSPCustomer = CType(DoLoad(GetType(MSPCustomer).ToString(), _mSPCustomer.ID), MSPCustomer)
                        Me._mSPCustomer.MarkLoaded()

                    End If

                    Return Me._mSPCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPCustomer)

                Me._mSPCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mSPCustomer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "MSPExRegistration", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        Me._chassisMaster.MarkLoaded()

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("MSPExMasterID", "{0}"), _
        RelationInfo("MSPExMaster", "ID", "MSPExRegistration", "MSPExMasterID")> _
        Public Property MSPExMaster() As MSPExMaster
            Get
                Try
                    If Not IsNothing(Me._mSPExMaster) AndAlso (Not Me._mSPExMaster.IsLoaded) Then

                        Me._mSPExMaster = CType(DoLoad(GetType(MSPExMaster).ToString(), _mSPExMaster.ID), MSPExMaster)
                        Me._mSPExMaster.MarkLoaded()

                    End If

                    Return Me._mSPExMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPExMaster)

                Me._mSPExMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mSPExMaster.MarkLoaded()
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

