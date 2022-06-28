
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOScheduleTime Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2016 - 11:20:52
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
	<Serializable(), TableInfo("SparePartPOScheduleTime")> _
	public class SparePartPOScheduleTime
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
        Private _scheduleTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Boolean = True
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _sparePartPOSchedule As SparePartPOSchedule


		
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
		

        <ColumnInfo("ScheduleTime", "'{0:yyyy/MM/dd HH:mm}'")> _
        Public Property ScheduleTime As DateTime
            Get
                Return _scheduleTime
            End Get
            Set(ByVal value As DateTime)
                _scheduleTime = value
            End Set
        End Property
		
        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Boolean
            Get
                Return _status
            End Get
            Set(ByVal value As Boolean)
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
		
		

        <ColumnInfo("SparePartPOScheduleID", "{0}"), _
     RelationInfo("SparePartPOSchedule", "ID", "SparePartPOScheduleTime", "SparePartPOScheduleID")> _
        Public Property SparePartPOSchedule() As SparePartPOSchedule
            Get
                Try
                    If Not IsNothing(Me._sparePartPOSchedule) AndAlso (Not Me._sparePartPOSchedule.IsLoaded) Then

                        Me._sparePartPOSchedule = CType(DoLoad(GetType(SparePartPOSchedule).ToString(), _sparePartPOSchedule.ID), SparePartPOSchedule)
                        Me._sparePartPOSchedule.MarkLoaded()

                    End If

                    Return Me._sparePartPOSchedule

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPOSchedule)

                Me._sparePartPOSchedule = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPOSchedule.MarkLoaded()
                End If
            End Set
        End Property


        '      <ColumnInfo("ScheduleTime", "'{0:yyyy/MM/dd HH:mm}'")> _
        '      Public ReadOnly Property ScheduleTimes As String
        '          Get
        '              Return _scheduleTime.ToString("HH:mm")
        '          End Get
        '      End Property
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

