
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : DMSWOWarrantyClaim Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 30/08/2018 - 12:43:38
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
    <Serializable(), TableInfo("DMSWOWarrantyClaim")> _
    Public Class DMSWOWarrantyClaim
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
        Private _chassisNumber As String = String.Empty
        Private _isBB As Boolean
        Private _workOrderNumber As String = String.Empty
        Private _failureDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _serviceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _owner As String = String.Empty
        Private _mileage As Integer
        Private _serviceBuletin As String = String.Empty
        Private _symptoms As String = String.Empty
        Private _causes As String = String.Empty
        Private _results As String = String.Empty
        Private _notes As String = String.Empty
        Private _rowStatus As Short
        Private _createBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch



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


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        <ColumnInfo("isBB", "{0}")> _
        Public Property isBB As Boolean
            Get
                Return _isBB
            End Get
            Set(ByVal value As Boolean)
                _isBB = value
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


        <ColumnInfo("FailureDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FailureDate As DateTime
            Get
                Return _failureDate
            End Get
            Set(ByVal value As DateTime)
                _failureDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("Owner", "'{0}'")> _
        Public Property Owner As String
            Get
                Return _owner
            End Get
            Set(ByVal value As String)
                _owner = value
            End Set
        End Property


        <ColumnInfo("Mileage", "{0}")> _
        Public Property Mileage As Integer
            Get
                Return _mileage
            End Get
            Set(ByVal value As Integer)
                _mileage = value
            End Set
        End Property


        <ColumnInfo("ServiceBuletin", "'{0}'")> _
        Public Property ServiceBuletin As String
            Get
                Return _serviceBuletin
            End Get
            Set(ByVal value As String)
                _serviceBuletin = value
            End Set
        End Property


        <ColumnInfo("Symptoms", "'{0}'")> _
        Public Property Symptoms As String
            Get
                Return _symptoms
            End Get
            Set(ByVal value As String)
                _symptoms = value
            End Set
        End Property


        <ColumnInfo("Causes", "'{0}'")> _
        Public Property Causes As String
            Get
                Return _causes
            End Get
            Set(ByVal value As String)
                _causes = value
            End Set
        End Property


        <ColumnInfo("Results", "'{0}'")> _
        Public Property Results As String
            Get
                Return _results
            End Get
            Set(ByVal value As String)
                _results = value
            End Set
        End Property


        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
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


        <ColumnInfo("CreateBy", "'{0}'")> _
        Public Property CreateBy As String
            Get
                Return _createBy
            End Get
            Set(ByVal value As String)
                _createBy = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DMSWOWarrantyClaim", "DealerID")> _
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
        RelationInfo("DealerBranch", "ID", "DMSWOWarrantyClaim", "DealerBranchID")> _
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

