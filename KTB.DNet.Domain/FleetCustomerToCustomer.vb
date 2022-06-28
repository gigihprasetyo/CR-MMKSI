
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetCustomerToCustomer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 9/22/2017 - 9:59:18 AM
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
	<Serializable(), TableInfo("FleetCustomerToCustomer")> _
	public class FleetCustomerToCustomer
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
		private _fleetCustomerID as integer 		
        Private _fleetCustomer As FleetCustomer
        Private _customerID As Customer
        Private _isDefault As Boolean
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdatedBy as string = String.Empty 		
		private _lastUpdatedTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		


		
		#end region
		
		#region "Public Properties"
		
		<ColumnInfo("ID","{0}")> _
		public property ID as integer
			get
				return _iD
			end get
			set(byval value as integer)
				_iD= value
			end set
		end property
		

		<ColumnInfo("FleetCustomerID","{0}")> _
		public property FleetCustomerID as integer
			get
				return _fleetCustomerID
			end get
			set(byval value as integer)
				_fleetCustomerID= value
			end set
		end property
		
        <ColumnInfo("CustomerID", "{0}"), _
        RelationInfo("Customer", "ID", "FleetCustomerToCustomer", "CustomerID")> _
        Public Property CustomerID() As Customer
            Get
                Try
                    If Not IsNothing(Me._customerID) AndAlso (Not Me._customerID.IsLoaded) Then

                        Me._customerID = CType(DoLoad(GetType(Customer).ToString(), _customerID.ID), Customer)
                        Me._customerID.MarkLoaded()

                    End If

                    Return Me._customerID

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Customer)

                Me._customerID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._customerID.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("FleetCustomerID", "{0}"), _
        RelationInfo("FleetCustomer", "ID", "FleetCustomerToCustomer", "FleetCustomerID")> _
        Public Property FleetCustomer() As FleetCustomer
            Get
                Try
                    If Not IsNothing(Me._fleetCustomer) AndAlso (Not Me._fleetCustomer.IsLoaded) Then
                        If _fleetCustomer.ID > 0 Then
                            Me._fleetCustomer = CType(DoLoad(GetType(FleetCustomer).ToString(), _fleetCustomer.ID), FleetCustomer)
                            Me._fleetCustomer.MarkLoaded()
                        End If
                    End If

                    Return Me._fleetCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As FleetCustomer)

                Me._fleetCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._fleetCustomer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("IsDefault", "{0}")> _
        Public Property IsDefault As Boolean
            Get
                Return _isDefault
            End Get
            Set(ByVal value As Boolean)
                _isDefault = value
            End Set
        End Property

		<ColumnInfo("RowStatus","{0}")> _
		public property RowStatus as short
			get
				return _rowStatus
			end get
			set(byval value as short)
				_rowStatus= value
			end set
		end property
		

		<ColumnInfo("CreatedBy","'{0}'")> _
		public property CreatedBy as string
			get
				return _createdBy
			end get
			set(byval value as string)
				_createdBy= value
			end set
		end property
		

		<ColumnInfo("CreatedTime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property CreatedTime as DateTime
			get
				return _createdTime
			end get
			set(byval value as DateTime)
				_createdTime= value
			end set
		end property
		

		<ColumnInfo("LastUpdatedBy","'{0}'")> _
		public property LastUpdatedBy as string
			get
				return _lastUpdatedBy
			end get
			set(byval value as string)
				_lastUpdatedBy= value
			end set
		end property
		

		<ColumnInfo("LastUpdatedTime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property LastUpdatedTime as DateTime
			get
				return _lastUpdatedTime
			end get
			set(byval value as DateTime)
				_lastUpdatedTime= value
			end set
		end property
		
		
		

		#end region
		
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

