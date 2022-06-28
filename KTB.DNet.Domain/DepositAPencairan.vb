
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositAPencairan Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/4/2008 - 8:49:41 AM
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
    <Serializable(), TableInfo("DepositAPencairan")> _
    Public Class DepositAPencairan
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
		private _dNNumber as string = String.Empty 		
		private _assignmentNumber as string = String.Empty 		
        Private _type As Byte
        Private _noSurat As String = String.Empty
        Private _dealerAmount As Decimal
        Private _approvalAmount As Decimal
		private _description as string = String.Empty 		
        Private _kTBReason As String = String.Empty
        Private _status As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
		private _dealerBankAccount as DealerBankAccount



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

		<ColumnInfo("DNNumber","'{0}'")> _
		public property DNNumber as string
			get
				return _dNNumber
			end get
			set(byval value as string)
				_dNNumber= value
			end set
		end property
		

		<ColumnInfo("AssignmentNumber","'{0}'")> _
		public property AssignmentNumber as string
			get
				return _assignmentNumber
			end get
			set(byval value as string)
				_assignmentNumber= value
			end set
		end property
		

        <ColumnInfo("Type", "{0}")> _
        Public Property Type() As Byte
            Get
                Return _type
            End Get
            Set(ByVal value As Byte)
                _type = value
            End Set
        End Property


        <ColumnInfo("NoSurat", "'{0}'")> _
        Public Property NoSurat() As String
            Get
                Return _noSurat
            End Get
            Set(ByVal value As String)
                _noSurat = value
            End Set
        End Property


        <ColumnInfo("DealerAmount", "{0}")> _
        Public Property DealerAmount() As Decimal
            Get
                Return _dealerAmount
            End Get
            Set(ByVal value As Decimal)
                _dealerAmount = value
            End Set
        End Property


        <ColumnInfo("ApprovalAmount", "{0}")> _
        Public Property ApprovalAmount() As Decimal
            Get
                Return _approvalAmount
            End Get
            Set(ByVal value As Decimal)
                _approvalAmount = value
            End Set
        End Property
		

        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("KTBReason", "'{0}'")> _
        Public Property KTBReason() As String
            Get
                Return _kTBReason
            End Get
            Set(ByVal value As String)
                _kTBReason = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DepositAPencairan", "DealerID")> _
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

        <ColumnInfo("DealerBankAccountID", "{0}"), _
        RelationInfo("DealerBankAccount", "ID", "DepositAPencairan", "DealerBankAccountID")> _
        Public Property DealerBankAccount() As DealerBankAccount
            Get
                Try
                    If Not IsNothing(Me._dealerBankAccount) AndAlso (Not Me._dealerBankAccount.IsLoaded) Then

                        Me._dealerBankAccount = CType(DoLoad(GetType(DealerBankAccount).ToString(), _dealerBankAccount.ID), DealerBankAccount)
                        Me._dealerBankAccount.MarkLoaded()

                    End If

                    Return Me._dealerBankAccount

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBankAccount)

                Me._dealerBankAccount = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBankAccount.MarkLoaded()
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

