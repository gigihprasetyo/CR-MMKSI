
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetHasilSurveyHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2017 - 1:39:06 PM
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
	<Serializable(), TableInfo("FleetHasilSurveyHeader")> _
	public class FleetHasilSurveyHeader
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
		private _surveyDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _surveyPerson as string = String.Empty 		
		private _surveyOccupation as string = String.Empty 		
		private _customerOccupation as string = String.Empty 		
		private _suggestionSales as string = String.Empty 		
		private _suggestionService as string = String.Empty 		
		private _suggestionSparepart as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
		private _fleetCustomerID as integer 
		private _delaerID as short 
		private _receivedByFleetCustomerContactID as integer 


		
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
		

		<ColumnInfo("SurveyDate","'{0:yyyy/MM/dd}'")> _
		public property SurveyDate as DateTime
			get
				return _surveyDate
			end get
			set(byval value as DateTime)
				_surveyDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

		<ColumnInfo("SurveyPerson","'{0}'")> _
		public property SurveyPerson as string
			get
				return _surveyPerson
			end get
			set(byval value as string)
				_surveyPerson= value
			end set
		end property
		

		<ColumnInfo("SurveyOccupation","'{0}'")> _
		public property SurveyOccupation as string
			get
				return _surveyOccupation
			end get
			set(byval value as string)
				_surveyOccupation= value
			end set
		end property
		

		<ColumnInfo("CustomerOccupation","'{0}'")> _
		public property CustomerOccupation as string
			get
				return _customerOccupation
			end get
			set(byval value as string)
				_customerOccupation= value
			end set
		end property
		

		<ColumnInfo("SuggestionSales","'{0}'")> _
		public property SuggestionSales as string
			get
				return _suggestionSales
			end get
			set(byval value as string)
				_suggestionSales= value
			end set
		end property
		

		<ColumnInfo("SuggestionService","'{0}'")> _
		public property SuggestionService as string
			get
				return _suggestionService
			end get
			set(byval value as string)
				_suggestionService= value
			end set
		end property
		

		<ColumnInfo("SuggestionSparepart","'{0}'")> _
		public property SuggestionSparepart as string
			get
				return _suggestionSparepart
			end get
			set(byval value as string)
				_suggestionSparepart= value
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
		
		
		<ColumnInfo("FleetCustomerID","{0}")> _
		public property FleetCustomerID as integer
		
			get
                Return _fleetCustomerID
			end get
			set(byval value as integer)
				_fleetCustomerID= value
			end set			
		end property
		<ColumnInfo("DelaerID","{0}")> _
		public property DelaerID as short
		
			get
                Return _delaerID
			end get
			set(byval value as short)
				_delaerID= value
			end set			
		end property
		<ColumnInfo("ReceivedByFleetCustomerContactID","{0}")> _
		public property ReceivedByFleetCustomerContactID as integer
		
			get
                Return _receivedByFleetCustomerContactID
			end get
			set(byval value as integer)
				_receivedByFleetCustomerContactID= value
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

