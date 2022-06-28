
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFPurchaseTransaction Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/07/2018 - 2:47:47 PM
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
	<Serializable(), TableInfo("SFPurchaseTransaction")> _
	public class SFPurchaseTransaction
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
		private _isSynchronize as boolean 		
		private _synchronizeDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _isActive as boolean 		
        Private _sFID As String = String.Empty
        Private _pDIDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pKTDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _warrantyRequestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _salesmanCode As String = String.Empty
        Private _salesmanName As String = String.Empty
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _objEndCustomer As EndCustomer

		
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
		

        <ColumnInfo("EndCustomerID", "{0}"), _
        RelationInfo("EndCustomer", "ID", "SFPurchaseTransaction", "EndCustomerID")> _
        Public Property EndCustomer As EndCustomer
            Get
                Try
                    If Not IsNothing(Me._objEndCustomer) AndAlso (Not Me._objEndCustomer.IsLoaded) Then
                        Me._objEndCustomer = CType(DoLoad(GetType(EndCustomer).ToString(), _objEndCustomer.ID), EndCustomer)
                        Me._objEndCustomer.MarkLoaded()
                    End If

                    Return Me._objEndCustomer
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As EndCustomer)

                Me._objEndCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objEndCustomer.MarkLoaded()
                End If
            End Set
        End Property
		

		<ColumnInfo("IsSynchronize","{0}")> _
		public property IsSynchronize as boolean
			get
				return _isSynchronize
			end get
			set(byval value as boolean)
				_isSynchronize= value
			end set
		end property
		

		<ColumnInfo("SynchronizeDate","'{0:yyyy/MM/dd}'")> _
		public property SynchronizeDate as DateTime
			get
				return _synchronizeDate
			end get
			set(byval value as DateTime)
				_synchronizeDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

		<ColumnInfo("IsActive","{0}")> _
		public property IsActive as boolean
			get
				return _isActive
			end get
			set(byval value as boolean)
				_isActive= value
			end set
		end property
		

		<ColumnInfo("SFID","'{0}'")> _
		public property SFID as string
			get
				return _sFID
			end get
			set(byval value as string)
				_sFID= value
			end set
		end property

        <ColumnInfo("PDIDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PDIDate As DateTime
            Get
                Return _pDIDate
            End Get
            Set(ByVal value As DateTime)
                _pDIDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("PKTDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PKTDate As DateTime
            Get
                Return _pKTDate
            End Get
            Set(ByVal value As DateTime)
                _pKTDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("WarrantyRequestDate", "'{0:yyyy/MM/dd}'")> _
        Public Property WarrantyRequestDate As DateTime
            Get
                Return _warrantyRequestDate
            End Get
            Set(ByVal value As DateTime)
                _warrantyRequestDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property

        <ColumnInfo("SalesmanName", "'{0}'")> _
        Public Property SalesmanName As String
            Get
                Return _salesmanName
            End Get
            Set(ByVal value As String)
                _salesmanName = value
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
		

		<ColumnInfo("LastUpdateBy","'{0}'")> _
		public property LastUpdateBy as string
			get
				return _lastUpdateBy
			end get
			set(byval value as string)
				_lastUpdateBy= value
			end set
		end property
		

		<ColumnInfo("LastUpdateTime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property LastUpdateTime as DateTime
			get
				return _lastUpdateTime
			end get
			set(byval value as DateTime)
				_lastUpdateTime= value
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

