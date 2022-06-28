#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceMMS Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/13/2005 - 11:17:37 AM
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
    <Serializable(), TableInfo("ServiceMMS")> _
    Public Class ServiceMMS
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
        Private _wONumber As String = String.Empty
        Private _serviceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _plateNo As String = String.Empty
        Private _nextEstimatedServiceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _notes As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _chassisMaster As ChassisMaster
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

        <ColumnInfo("DealerID", "{0}"), _
            RelationInfo("Dealer", "ID", "ServiceMMS", "DealerID")> _
        Public Property Dealer As Dealer
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

        <ColumnInfo("DealerBranchID", "{0}"), _
            RelationInfo("DealerBranch", "ID", "ServiceMMS", "DealerBranchID")> _
        Public Property DealerBranch As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) AndAlso _dealerBranch.ID > 0 Then
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
                If value.ID = 0 Then
                    Dim i As Integer = 0
                    Me._dealerBranch = Nothing
                End If
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("WONumber", "'{0}'")> _
        Public Property WONumber() As String
            Get
                Return _wONumber
            End Get
            Set(ByVal value As String)
                _wONumber = value
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

        <ColumnInfo("ChassisMasterID", "{0}"), _
            RelationInfo("ChassisMaster", "ID", "ServiceMMS", "ChassisMasterID")> _
        Public Property ChassisMaster As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then
                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _dealerBranch.ID), ChassisMaster)
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

        <ColumnInfo("PlateNo", "'{0}'")> _
        Public Property PlateNo() As String
            Get
                Return _plateNo
            End Get
            Set(ByVal value As String)
                _plateNo = value
            End Set
        End Property

        <ColumnInfo("NextEstimatedServiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property NextEstimatedServiceDate As DateTime
            Get
                Return _nextEstimatedServiceDate
            End Get
            Set(ByVal value As DateTime)
                _nextEstimatedServiceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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

#Region "Custom Method"

#End Region

    End Class
End Namespace



