
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
	<Serializable(), TableInfo("DepositAPencairanH")> _
	public class DepositAPencairanH
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
		private _noSurat as string = String.Empty 		
		private _dNNumber as string = String.Empty 		
		private _assignmentNumber as string = String.Empty 		
        Private _type As Byte
        Private _dealerAmount As Decimal
        Private _approvalAmount As Decimal
        Private _kTBReason As String = String.Empty
        Private _status As Byte
        Private _noReg As String        
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _dealerBankAccount As DealerBankAccount
        Private _depositAInterestH As DepositAInterestH
        Private _productCategory As ProductCategory
        Private _AnnualDepositAHeader As AnnualDepositAHeader


		private _depositAPencairanDs as System.Collections.ArrayList = new System.Collections.ArrayList()


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
		

        <ColumnInfo("NoSurat", "'{0}'")> _
       Public Property NoSurat() As String
            Get
                Return _noSurat
            End Get
            Set(ByVal value As String)
                _noSurat = value
            End Set
        End Property


        <ColumnInfo("DNNumber", "'{0}'")> _
        Public Property DNNumber() As String
            Get
                Return _dNNumber
            End Get
            Set(ByVal value As String)
                _dNNumber = value
            End Set
        End Property


        <ColumnInfo("AssignmentNumber", "'{0}'")> _
       Public Property AssignmentNumber() As String
            Get
                Return _assignmentNumber
            End Get
            Set(ByVal value As String)
                _assignmentNumber = value
            End Set
        End Property


        <ColumnInfo("Type", "{0}")> _
        Public Property Type() As Byte
            Get
                Return _type
            End Get
            Set(ByVal value As Byte)
                _type = value
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

        <ColumnInfo("NoReg", "'{0}'")> _
       Public Property NoReg() As String
            Get
                Return _noReg
            End Get
            Set(ByVal value As String)
                _noReg = value
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
               RelationInfo("Dealer", "ID", "DepositAPencairanH", "DealerID")> _
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
        RelationInfo("DealerBankAccount", "ID", "DepositAPencairanH", "DealerBankAccountID")> _
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


        <RelationInfo("DepositAPencairanH", "ID", "DepositAPencairanD", "HeaderID")> _
        Public ReadOnly Property DepositAPencairanDs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositAPencairanDs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositAPencairanD), "DepositAPencairanH", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositAPencairanD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositAPencairanDs = DoLoadArray(GetType(DepositAPencairanD).ToString, criterias)
                    End If

                    Return Me._depositAPencairanDs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("DepositAInterestHID", "{0}"), _
     RelationInfo("DepositAInterestH", "ID", "DepositAPencairanH", "DepositAInterestHID")> _
     Public Property DepositAInterestH() As DepositAInterestH
            Get
                Try
                    If Not IsNothing(Me._depositAInterestH) AndAlso (Not Me._depositAInterestH.IsLoaded) Then

                        Me._depositAInterestH = CType(DoLoad(GetType(DepositAInterestH).ToString(), _depositAInterestH.ID), DepositAInterestH)
                        Me._depositAInterestH.MarkLoaded()

                    End If

                    Return Me._depositAInterestH

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositAInterestH)

                Me._depositAInterestH = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositAInterestH.MarkLoaded()
                End If
            End Set
        End Property




        <ColumnInfo("AnnualDepositAHeaderID", "{0}"), _
    RelationInfo("AnnualDepositAHeader", "ID", "DepositAPencairanH", "AnnualDepositAHeaderID")> _
        Public Property AnnualDepositAHeader() As AnnualDepositAHeader
            Get
                Try
                    If Not IsNothing(Me._AnnualDepositAHeader) AndAlso (Not Me._AnnualDepositAHeader.IsLoaded) Then

                        Me._AnnualDepositAHeader = CType(DoLoad(GetType(AnnualDepositAHeader).ToString(), _AnnualDepositAHeader.ID), AnnualDepositAHeader)
                        Me._AnnualDepositAHeader.MarkLoaded()

                    End If

                    Return Me._AnnualDepositAHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AnnualDepositAHeader)

                Me._AnnualDepositAHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._AnnualDepositAHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ProductCategoryID", "{0}"), _
             RelationInfo("ProductCategory", "ID", "DepositAPencairanH", "ProductCategoryID")> _
        Public Property ProductCategory() As ProductCategory
            Get
                Try
                    If Not IsNothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

                        Me._productCategory = CType(DoLoad(GetType(ProductCategory).ToString(), _productCategory.ID), ProductCategory)
                        Me._productCategory.MarkLoaded()

                    End If

                    Return Me._productCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProductCategory)

                Me._productCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
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

