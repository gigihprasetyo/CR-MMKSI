
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetCustomerToDealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2017 - 1:38:25 PM
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
	<Serializable(), TableInfo("FleetCustomerToDealer")> _
	public class FleetCustomerToDealer
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
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdatedBy as string = String.Empty 		
		private _lastUpdatedTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
		private _fleetCustomerID as integer 
		private _dealerID as short 


		
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
		
		
		<ColumnInfo("FleetCustomerID","{0}")> _
		public property FleetCustomerID as integer
		
			get
                Return _fleetCustomerID
			end get
			set(byval value as integer)
				_fleetCustomerID= value
			end set			
		end property
		<ColumnInfo("DealerID","{0}")> _
		public property DealerID as short
		
			get
                Return _dealerID
			end get
			set(byval value as short)
				_dealerID= value
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

