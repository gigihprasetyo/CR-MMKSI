#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WarrantyActivation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:01:07 PM
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
    <Serializable(), TableInfo("WarrantyActivation")> _
    Public Class WarrantyActivation
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
        Private _status As Short
        Private _wADate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fileName As String
        Private _dSFilePath As String
        Private _customerName As String
        Private _handphoneNo As String
        Private _plateNumber As String
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pDI As PDI
        Private _dealer As Dealer
        Private _chassisMaster As ChassisMaster
        Private _chassisMasterPKT As ChassisMasterPKT



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

        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property

        <ColumnInfo("WADate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property WADate() As DateTime
            Get
                Return _wADate
            End Get
            Set(ByVal value As DateTime)
                _wADate = value
            End Set
        End Property

        <ColumnInfo("FileName", "{0}")> _
        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property

        <ColumnInfo("DSFilePath", "{0}")> _
        Public Property DSFilePath() As String
            Get
                Return _dSFilePath
            End Get
            Set(ByVal value As String)
                _dSFilePath = value
            End Set
        End Property

        <ColumnInfo("CustomerName", "{0}")> _
        Public Property CustomerName() As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property

        <ColumnInfo("HandphoneNo", "{0}")> _
        Public Property HandphoneNo() As String
            Get
                Return _handphoneNo
            End Get
            Set(ByVal value As String)
                _handphoneNo = value
            End Set
        End Property

        <ColumnInfo("PlateNumber", "{0}")> _
        Public Property PlateNumber() As String
            Get
                Return _plateNumber
            End Get
            Set(ByVal value As String)
                _plateNumber = value
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy() As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime() As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "WarrantyActivation", "ChassisMasterID")> _
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

        <ColumnInfo("PDIID", "{0}"), _
        RelationInfo("PDI", "ID", "WarrantyActivation", "PDIID")> _
        Public Property PDI() As PDI
            Get
                Try
                    If Not IsNothing(Me._pDI) AndAlso (Not Me._pDI.IsLoaded) Then

                        Me._pDI = CType(DoLoad(GetType(PDI).ToString(), _pDI.ID), PDI)
                        Me._pDI.MarkLoaded()

                    End If

                    Return Me._pDI

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PDI)

                Me._pDI = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pDI.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PDI", "DealerID")> _
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


        <ColumnInfo("ChassisMasterPKTID", "{0}"), _
        RelationInfo("ChassisMasterPKT", "ID", "WarrantyActivation", "ChassisMasterPKTID")> _
        Public Property ChassisMasterPKT() As ChassisMasterPKT
            Get
                Try
                    If Not IsNothing(Me._chassisMasterPKT) AndAlso (Not Me._chassisMasterPKT.IsLoaded) Then

                        Me._chassisMasterPKT = CType(DoLoad(GetType(ChassisMasterPKT).ToString(), _chassisMasterPKT.ID), ChassisMasterPKT)
                        Me._chassisMasterPKT.MarkLoaded()

                    End If

                    Return Me._chassisMasterPKT

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMasterPKT)

                Me._chassisMasterPKT = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMasterPKT.MarkLoaded()
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

#Region "Non_generated Properties"
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

