
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcReportMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/7/2011 - 10:42:30 AM
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
	<Serializable(), TableInfo("CcReportMaster")> _
	public class CcReportMaster
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
		private _sequence as integer 		
		private _rptDesc as string = String.Empty 		
		private _rptType as short 		
		private _defaFileName as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		

		private _ccReportDealers as System.Collections.ArrayList = new System.Collections.ArrayList()

		
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
		

		<ColumnInfo("Sequence","{0}")> _
		public property Sequence as integer
			get
				return _sequence
			end get
			set(byval value as integer)
				_sequence= value
			end set
		end property
		

		<ColumnInfo("RptDesc","'{0}'")> _
		public property RptDesc as string
			get
				return _rptDesc
			end get
			set(byval value as string)
				_rptDesc= value
			end set
		end property
		

		<ColumnInfo("RptType","{0}")> _
		public property RptType as short
			get
				return _rptType
			end get
			set(byval value as short)
				_rptType= value
			end set
		end property
		

		<ColumnInfo("DefaFileName","'{0}'")> _
		public property DefaFileName as string
			get
				return _defaFileName
			end get
			set(byval value as string)
				_defaFileName= value
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
		
		
		
		<RelationInfo("CcReportMaster","ID","CcReportDealer","CcReportMasterID")> _
		public readonly property CcReportDealers as System.Collections.ArrayList
			get
				try
					if (me._ccReportDealers.Count < 1) then
						dim _criteria as Criteria = new Criteria(gettype(CcReportDealer),"CcReportMaster",me.ID)
						dim criterias as CriteriaComposite = new CriteriaComposite(_criteria)
						criterias.opAnd(new Criteria(gettype(CcReportDealer),"RowStatus",MatchType.Exact,ctype(DBRowStatus.Active, short)))

						me._ccReportDealers = DoLoadArray(gettype(CcReportDealer).ToString,criterias)
					end if
					
					return me._ccReportDealers
				
				catch ex as Exception
				
					dim rethrow as boolean = ExceptionPolicy.HandleException(ex,"Domain Policy")
					
					if rethrow then
						throw
					end if
					
				end try
				
				return nothing
				
			end get		
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

