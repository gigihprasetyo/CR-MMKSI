
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcComplainFollowup Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2011 - 11:21:59 AM
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
	<Serializable(), TableInfo("CcComplainFollowup")> _
	public class CcComplainFollowup
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
		private _status as short 		
		private _note as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
		private _ccFollowupID as short 
		private _ccSurveyID as integer 

		private _ccComplainFollowupDetails as System.Collections.ArrayList = new System.Collections.ArrayList()

		
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
		

		<ColumnInfo("Status","{0}")> _
		public property Status as short
			get
				return _status
			end get
			set(byval value as short)
				_status= value
			end set
		end property
		

		<ColumnInfo("Note","'{0}'")> _
		public property Note as string
			get
				return _note
			end get
			set(byval value as string)
				_note= value
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
		
		
		<ColumnInfo("CcFollowupID","{0}")> _
		public property CcFollowupID as short
		
			get
                Return _ccFollowupID
			end get
			set(byval value as short)
				_ccFollowupID= value
			end set			
		end property
		<ColumnInfo("CcSurveyID","{0}")> _
		public property CcSurveyID as integer
		
			get
                Return _ccSurveyID
			end get
			set(byval value as integer)
				_ccSurveyID= value
			end set			
		end property
		
		<RelationInfo("CcComplainFollowup","ID","CcComplainFollowupDetail","CcComplainFollowupID")> _
		public readonly property CcComplainFollowupDetails as System.Collections.ArrayList
			get
				try
					if (me._ccComplainFollowupDetails.Count < 1) then
						dim _criteria as Criteria = new Criteria(gettype(CcComplainFollowupDetail),"CcComplainFollowup",me.ID)
						dim criterias as CriteriaComposite = new CriteriaComposite(_criteria)
						criterias.opAnd(new Criteria(gettype(CcComplainFollowupDetail),"RowStatus",MatchType.Exact,ctype(DBRowStatus.Active, short)))

						me._ccComplainFollowupDetails = DoLoadArray(gettype(CcComplainFollowupDetail).ToString,criterias)
					end if
					
					return me._ccComplainFollowupDetails
				
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

