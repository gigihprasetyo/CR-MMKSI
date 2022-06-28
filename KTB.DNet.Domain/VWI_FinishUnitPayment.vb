#region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : VWI_FinishUnitPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2021 - 10:40:42 AM
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
	<Serializable(), TableInfo("VWI_FinishUnitPayment")> _
	public class VWI_FinishUnitPayment
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
		private _factoringStatus as short 		
		private _regPO as string = String.Empty 		
		private _salesOrderNumber as string = String.Empty 		
		private _giroNumber as string = String.Empty 		
		private _regNumber as string = String.Empty 		
		private _dueDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _effectiveDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
        Private _amount As Decimal
		private _status as short 		
		


		
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
		

		<ColumnInfo("FactoringStatus","{0}")> _
		public property FactoringStatus as short
			get
				return _factoringStatus
			end get
			set(byval value as short)
				_factoringStatus= value
			end set
		end property
		

		<ColumnInfo("RegPO","'{0}'")> _
		public property RegPO as string
			get
				return _regPO
			end get
			set(byval value as string)
				_regPO= value
			end set
		end property
		

		<ColumnInfo("SalesOrderNumber","'{0}'")> _
		public property SalesOrderNumber as string
			get
				return _salesOrderNumber
			end get
			set(byval value as string)
				_salesOrderNumber= value
			end set
		end property
		

		<ColumnInfo("GiroNumber","'{0}'")> _
		public property GiroNumber as string
			get
				return _giroNumber
			end get
			set(byval value as string)
				_giroNumber= value
			end set
		end property
		

		<ColumnInfo("RegNumber","'{0}'")> _
		public property RegNumber as string
			get
				return _regNumber
			end get
			set(byval value as string)
				_regNumber= value
			end set
		end property
		

		<ColumnInfo("DueDate","'{0:yyyy/MM/dd}'")> _
		public property DueDate as DateTime
			get
				return _dueDate
			end get
			set(byval value as DateTime)
				_dueDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

		<ColumnInfo("EffectiveDate","'{0:yyyy/MM/dd}'")> _
		public property EffectiveDate as DateTime
			get
				return _effectiveDate
			end get
			set(byval value as DateTime)
				_effectiveDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property
		

		<ColumnInfo("Status","{0}")> _
		public property Status as short
			get
				return _status
			end get
			set(byval value as short)
				_status= value
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
