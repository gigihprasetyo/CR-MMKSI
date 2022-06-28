
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOSchedule Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2016 - 11:18:38
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
	<Serializable(), TableInfo("SparePartPOSchedule")> _
	public class SparePartPOSchedule
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
		private _orderType as string = String.Empty 		
        Private _orderDay As Short
        Private _status As Boolean = True
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		

        Private _sparePartPOScheduleDealer As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sparePartPOScheduleTime As System.Collections.ArrayList = New System.Collections.ArrayList
		
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
		

		<ColumnInfo("OrderType","'{0}'")> _
		public property OrderType as string
			get
				return _orderType
			end get
			set(byval value as string)
				_orderType= value
			end set
		end property
		

		<ColumnInfo("OrderDay","{0}")> _
		public property OrderDay as short
			get
				return _orderDay
			end get
			set(byval value as short)
				_orderDay= value
			end set
		end property
		
        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
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
		
        <RelationInfo("SparePartPOSchedule", "ID", "SparePartPOScheduleDealer", "SparePartPOScheduleID")> _
        Public ReadOnly Property SparePartPOScheduleDealers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartPOScheduleDealer.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPOScheduleDealer), "SparePartPOSchedule.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPOScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartPOScheduleDealer = DoLoadArray(GetType(SparePartPOScheduleDealer).ToString, criterias)
                    End If

                    Return Me._sparePartPOScheduleDealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property
		

        <RelationInfo("SparePartPOSchedule", "ID", "SparePartPOScheduleTime", "SparePartPOScheduleID")> _
        Public ReadOnly Property SparePartPOScheduleTimes() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartPOScheduleTime.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPOScheduleTime), "SparePartPOSchedule.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPOScheduleTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartPOScheduleTime = DoLoadArray(GetType(SparePartPOScheduleTime).ToString, criterias)
                    End If

                    Return Me._sparePartPOScheduleTime

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

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

