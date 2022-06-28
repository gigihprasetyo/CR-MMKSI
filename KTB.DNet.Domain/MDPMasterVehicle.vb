
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MDPMasterVehicle Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 20/02/2019 - 16:33:30
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
	<Serializable(), TableInfo("MDPMasterVehicle")> _
	public class MDPMasterVehicle
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
        'private _vehicleColorID as integer 		
		private _status as short 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _vehicleColor As VechileColor
        Private _MDPMasterVehicleHistory As System.Collections.ArrayList = New System.Collections.ArrayList
		
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
		

        '<ColumnInfo("VehicleColorID","{0}")> _
        'public property VehicleColorID as integer
        '	get
        '		return _vehicleColorID
        '	end get
        '	set(byval value as integer)
        '		_vehicleColorID= value
        '	end set
        'end property
		

		<ColumnInfo("Status","{0}")> _
		public property Status as short
			get
				return _status
			end get
			set(byval value as short)
				_status= value
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
		
        <ColumnInfo("VehicleColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "MDPMasterVehicle", "VehicleColorID")> _
        Public Property VehicleColor() As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vehicleColor) AndAlso (Not Me._vehicleColor.IsLoaded) Then

                        Me._vehicleColor = CType(DoLoad(GetType(VechileColor).ToString(), _vehicleColor.ID), VechileColor)
                        Me._vehicleColor.MarkLoaded()

                    End If

                    Return Me._vehicleColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vehicleColor = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehicleColor.MarkLoaded()
                End If
            End Set
        End Property
		
        <RelationInfo("MDPMasterVehicle", "ID", "MDPMasterVehicleHistory", "MDPMasterVehicleID")> _
        Public ReadOnly Property MDPMasterVehicleHistory() As System.Collections.ArrayList
            Get
                Try
                    If (Me._MDPMasterVehicleHistory.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MDPMasterVehicleHistory), "MDPMasterVehicle", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MDPMasterVehicleHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._MDPMasterVehicleHistory = DoLoadArray(GetType(MDPMasterVehicleHistory).ToString, criterias)
                    End If

                    Return Me._MDPMasterVehicleHistory

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

