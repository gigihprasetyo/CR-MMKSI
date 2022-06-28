
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RecallService Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 19/04/2016 - 11:31:46
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
    <Serializable(), TableInfo("RecallService")> _
    Public Class RecallService
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
        Private _mileAge As Integer
        Private _serviceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _workOrderNumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _chassisBBID As Integer
        Private _chassisID As Integer
        'Private _chassisMasterID As Integer
        Private _chassisMaster As ChassisMaster

        'Private _recallChassisMasterID As Integer
        Private _recallChassisMaster As RecallChassisMaster

        'Add support for ChassisMasterBB
        Private _chassisMasterBB As ChassisMasterBB

        'Private _serviceDealerID As Short
        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch


#End Region

#Region "Public Properties"

        <ColumnInfo("ChassisMasterBBID", "{0}")> _
        Public Property ChassisBBID As Integer
            Get
                Return _chassisBBID
            End Get
            Set(ByVal value As Integer)
                _chassisBBID = value
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}")> _
        Public Property ChassisID As Integer
            Get
                Return _chassisID
            End Get
            Set(ByVal value As Integer)
                _chassisID = value
            End Set
        End Property

        '<ColumnInfo("RecallChassisMasterID", "{0}")> _
        'Public Property RecallChassisMasterID As Integer
        '    Get
        '        Return _recallChassisMasterID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _recallChassisMasterID = value
        '    End Set
        'End Property


        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("MileAge", "{0}")> _
        Public Property MileAge As Integer
            Get
                Return _mileAge
            End Get
            Set(ByVal value As Integer)
                _mileAge = value
            End Set
        End Property


        <ColumnInfo("ServiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ServiceDate As DateTime
            Get
                Return _serviceDate
            End Get
            Set(ByVal value As DateTime)
                _serviceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("WorkOrderNumber", "'{0}'")> _
        Public Property WorkOrderNumber As String
            Get
                Return _workOrderNumber
            End Get
            Set(ByVal value As String)
                _workOrderNumber = value
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

        '<ColumnInfo("ServiceDealerID", "{0}")> _
        'Public Property ServiceDealerID As Short

        '    Get
        '        Return _serviceDealerID
        '    End Get
        '    Set(ByVal value As Short)
        '        _serviceDealerID = value
        '    End Set
        'End Property

        <ColumnInfo("ServiceDealerID", "{0}"), _
       RelationInfo("Dealer", "ID", "RecallService", "ServiceDealerID")> _
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

        <ColumnInfo("ChassisMasterID", "{0}"), _
            RelationInfo("ChassisMaster", "ID", "RecallService", "ChassisMasterID")> _
     Public Property ChassisMaster As ChassisMaster
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

        <ColumnInfo("ChassisMasterBBID", "{0}"), _
            RelationInfo("ChassisMasterBB", "ID", "RecallService", "ChassisMasterBBID")> _
        Public Property ChassisMasterBB As ChassisMasterBB
            Get
                Try
                    If Not IsNothing(Me._chassisMasterBB) AndAlso (Not Me._chassisMasterBB.IsLoaded) Then

                        Me._chassisMasterBB = CType(DoLoad(GetType(ChassisMasterBB).ToString(), _chassisMasterBB.ID), ChassisMasterBB)
                        Me._chassisMasterBB.MarkLoaded()

                    End If

                    Return Me._chassisMasterBB

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
            Set(ByVal value As ChassisMasterBB)
                Me._chassisMasterBB = value

                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMasterBB.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("RecallChassisMasterID", "{0}"), _
            RelationInfo("RecallChassisMaster", "ID", "RecallService", "RecallChassisMasterID")> _
        Public Property RecallChassisMaster As RecallChassisMaster
            Get
                Try
                    If Not IsNothing(Me._recallChassisMaster) AndAlso (Not Me._recallChassisMaster.IsLoaded) Then
                        Me._recallChassisMaster = CType(DoLoad(GetType(RecallChassisMaster).ToString(), _recallChassisMaster.ID), RecallChassisMaster)
                        Me._recallChassisMaster.MarkLoaded()
                    End If

                    Return Me._recallChassisMaster

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
            Set(ByVal value As RecallChassisMaster)
                Me._recallChassisMaster = value

                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._recallChassisMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerBranchID", "{0}"), _
            RelationInfo("DealerBranch", "ID", "RecallService", "DealerBranchID")> _
     Public Property DealerBranch As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        Private _RecallRegNo As String

        <ColumnInfo("LastUpdateBy", "'{0}'")>
        Public Property RecallRegNo As String

            Set(value As String)
                _RecallRegNo = value
            End Set
            Get
                Return _RecallRegNo
            End Get
        End Property

        Private _buletinNo As String

        <ColumnInfo("LastUpdateBy", "'{0}'")>
        Public Property BuletinNo As String

            Set(value As String)
                _buletinNo = value
            End Set
            Get
                Return _buletinNo
            End Get
        End Property

        Private _chassisNumber As String

        <ColumnInfo("LastUpdateBy", "'{0}'")>
        Public Property ChassisNumber As String

            Set(value As String)
                _chassisNumber = value
            End Set
            Get
                Return _chassisNumber
            End Get
        End Property

        Private _serviceDealerID As String

        <ColumnInfo("LastUpdateBy", "'{0}'")>
        Public Property ServiceDealerID As String

            Set(value As String)
                _serviceDealerID = value
            End Set
            Get
                Return _serviceDealerID
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

