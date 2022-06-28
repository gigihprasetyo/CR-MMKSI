
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPTransferPaymentDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/5/2018 - 4:49:26 PM
'//
'// ===========================================================================	
#end region

#region ".NET Base Class Namespace Imports"
imports System
imports System.Collections
#end region

#region "Custom Namespace Imports"
imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#end region

namespace KTB.DNet.Domain
	<Serializable(), TableInfo("MSPTransferPaymentDetail")> _
	public class MSPTransferPaymentDetail
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval ID as integer )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
		private _iD as integer 			
		private _amount as decimal 		
		private _isCanceled as boolean 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _objMSPTransferPayment As MSPTransferPayment
        Private _objRegistrationHistory As MSPRegistrationHistory

		
		#end region
		
#Region "Public Properties"

        <ColumnInfo("MSPTransferPaymentID", "{0}"), _
        RelationInfo("MSPTransferPayment", "ID", "MSPTransferPaymentDetail", "MSPTransferPaymentID")> _
        Public Property MSPTransferPayment() As MSPTransferPayment
            Get
                Try
                    If Not IsNothing(Me._objMSPTransferPayment) AndAlso (Not Me._objMSPTransferPayment.IsLoaded) Then
                        Me._objMSPTransferPayment = CType(DoLoad(GetType(MSPTransferPayment).ToString(), _objMSPTransferPayment.ID), MSPTransferPayment)
                        Me._objMSPTransferPayment.MarkLoaded()
                    End If

                    Return Me._objMSPTransferPayment
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPTransferPayment)

                Me._objMSPTransferPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objMSPTransferPayment.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MSPRegistrationHistoryID", "{0}"), _
        RelationInfo("MSPRegistrationHistory", "ID", "MSPTransferPaymentDetail", "MSPRegistrationHistoryID")> _
        Public Property MSPRegistrationHistory() As MSPRegistrationHistory
            Get
                Try
                    If Not IsNothing(Me._objRegistrationHistory) AndAlso (Not Me._objRegistrationHistory.IsLoaded) Then
                        Me._objRegistrationHistory = CType(DoLoad(GetType(MSPRegistrationHistory).ToString(), _objRegistrationHistory.ID), MSPRegistrationHistory)
                        Me._objRegistrationHistory.MarkLoaded()
                    End If

                    Return Me._objRegistrationHistory
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPRegistrationHistory)

                Me._objRegistrationHistory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objRegistrationHistory.MarkLoaded()
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

        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("IsCanceled", "{0}")> _
        Public Property IsCanceled As Boolean
            Get
                Return _isCanceled
            End Get
            Set(ByVal value As Boolean)
                _isCanceled = value
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
		
	end class
end namespace

