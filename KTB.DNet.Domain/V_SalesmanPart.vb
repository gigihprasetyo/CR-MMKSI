
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SalesmanPart Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/9/2011 - 10:59:52 AM
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
	<Serializable(), TableInfo("V_SalesmanPart")> _
	public class V_SalesmanPart
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval ID as short )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
		private _iD as short 		
		private _dealerId as short 		
		private _salesmanCode as string = String.Empty 		
		private _name as string = String.Empty 		
		private _image as byte() 		
		private _placeOfBirth as string = String.Empty 		
		private _dateOfBirth as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _gender as byte 		
		private _address as string = String.Empty 		
		private _city as string = String.Empty 		
		private _shopSiteNumber as integer 		
		private _hireDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _salesmanAreaId as integer 		
		private _jobPositionId_Main as integer 		
		private _salesmanLevelID as integer 		
		private _jobPositionId_Second as integer 		
		private _jobPositionId_Third as integer 		
		private _leaderId as integer 		
		private _jobPositionId_Leader as integer 		
		private _registerStatus as string = String.Empty 		
		private _marriedStatus as string = String.Empty 		
		private _resignDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _resignReason as string = String.Empty 		
		private _salesIndicator as byte 		
		private _salesUnitIndicator as byte 		
		private _mechanicIndicator as byte 		
		private _sparePartIndicator as byte 		
		private _sPAdminIndicator as byte 		
		private _sPWareHouseIndicator as byte 		
		private _sPCounterIndicator as byte 		
		private _sPSalesIndicator as byte 		
		private _isRequestID as byte 		
		private _status as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
        Private _dealerCode As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
		private _provinceName as string = String.Empty 		
		private _divisiID as integer 		
		private _divisiName as string = String.Empty 		
		private _posisiID as integer 		
		private _posisiName as string = String.Empty 		
		private _levelID as integer 		
		private _levelName as string = String.Empty 		
		private _salary as decimal 		
		private _leaderCode as string = String.Empty 		
		private _leaderName as string = String.Empty 		
		private _areaDesc as string = String.Empty 		
		private _pENDIDIKAN as string = String.Empty 		
		private _eMAIL as string = String.Empty 		
		private _nO_HP as string = String.Empty 		
		private _nOKTP as string = String.Empty 		
		


		
		#end region
		
		#region "Public Properties"
		
		<ColumnInfo("ID","{0}")> _
		public property ID as short
			get
				return _iD
			end get
			set(byval value as short)
				_iD= value
			end set
		end property
		

		<ColumnInfo("DealerId","{0}")> _
		public property DealerId as short
			get
				return _dealerId
			end get
			set(byval value as short)
				_dealerId= value
			end set
		end property
		

		<ColumnInfo("SalesmanCode","'{0}'")> _
		public property SalesmanCode as string
			get
				return _salesmanCode
			end get
			set(byval value as string)
				_salesmanCode= value
			end set
		end property
		

		<ColumnInfo("Name","'{0}'")> _
		public property Name as string
			get
				return _name
			end get
			set(byval value as string)
				_name= value
			end set
		end property
		

		<ColumnInfo("Image","{0}")> _
		public property Image as byte()
			get
				return _image
			end get
			set(byval value as byte())
				_image= value
			end set
		end property
		

		<ColumnInfo("PlaceOfBirth","'{0}'")> _
		public property PlaceOfBirth as string
			get
				return _placeOfBirth
			end get
			set(byval value as string)
				_placeOfBirth= value
			end set
		end property
		

		<ColumnInfo("DateOfBirth","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property DateOfBirth as DateTime
			get
				return _dateOfBirth
			end get
			set(byval value as DateTime)
				_dateOfBirth= value
			end set
		end property
		

		<ColumnInfo("Gender","{0}")> _
		public property Gender as byte
			get
				return _gender
			end get
			set(byval value as byte)
				_gender= value
			end set
		end property
		

		<ColumnInfo("Address","'{0}'")> _
		public property Address as string
			get
				return _address
			end get
			set(byval value as string)
				_address= value
			end set
		end property
		

		<ColumnInfo("City","'{0}'")> _
		public property City as string
			get
				return _city
			end get
			set(byval value as string)
				_city= value
			end set
		end property
		

		<ColumnInfo("ShopSiteNumber","{0}")> _
		public property ShopSiteNumber as integer
			get
				return _shopSiteNumber
			end get
			set(byval value as integer)
				_shopSiteNumber= value
			end set
		end property
		

		<ColumnInfo("HireDate","'{0:yyyy/MM/dd}'")> _
		public property HireDate as DateTime
			get
				return _hireDate
			end get
			set(byval value as DateTime)
				_hireDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

		<ColumnInfo("SalesmanAreaId","{0}")> _
		public property SalesmanAreaId as integer
			get
				return _salesmanAreaId
			end get
			set(byval value as integer)
				_salesmanAreaId= value
			end set
		end property
		

		<ColumnInfo("JobPositionId_Main","{0}")> _
		public property JobPositionId_Main as integer
			get
				return _jobPositionId_Main
			end get
			set(byval value as integer)
				_jobPositionId_Main= value
			end set
		end property
		

		<ColumnInfo("SalesmanLevelID","{0}")> _
		public property SalesmanLevelID as integer
			get
				return _salesmanLevelID
			end get
			set(byval value as integer)
				_salesmanLevelID= value
			end set
		end property
		

		<ColumnInfo("JobPositionId_Second","{0}")> _
		public property JobPositionId_Second as integer
			get
				return _jobPositionId_Second
			end get
			set(byval value as integer)
				_jobPositionId_Second= value
			end set
		end property
		

		<ColumnInfo("JobPositionId_Third","{0}")> _
		public property JobPositionId_Third as integer
			get
				return _jobPositionId_Third
			end get
			set(byval value as integer)
				_jobPositionId_Third= value
			end set
		end property
		

		<ColumnInfo("LeaderId","{0}")> _
		public property LeaderId as integer
			get
				return _leaderId
			end get
			set(byval value as integer)
				_leaderId= value
			end set
		end property
		

		<ColumnInfo("JobPositionId_Leader","{0}")> _
		public property JobPositionId_Leader as integer
			get
				return _jobPositionId_Leader
			end get
			set(byval value as integer)
				_jobPositionId_Leader= value
			end set
		end property
		

		<ColumnInfo("RegisterStatus","'{0}'")> _
		public property RegisterStatus as string
			get
				return _registerStatus
			end get
			set(byval value as string)
				_registerStatus= value
			end set
		end property
		

		<ColumnInfo("MarriedStatus","'{0}'")> _
		public property MarriedStatus as string
			get
				return _marriedStatus
			end get
			set(byval value as string)
				_marriedStatus= value
			end set
		end property
		

		<ColumnInfo("ResignDate","'{0:yyyy/MM/dd}'")> _
		public property ResignDate as DateTime
			get
				return _resignDate
			end get
			set(byval value as DateTime)
				_resignDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

		<ColumnInfo("ResignReason","'{0}'")> _
		public property ResignReason as string
			get
				return _resignReason
			end get
			set(byval value as string)
				_resignReason= value
			end set
		end property
		

		<ColumnInfo("SalesIndicator","{0}")> _
		public property SalesIndicator as byte
			get
				return _salesIndicator
			end get
			set(byval value as byte)
				_salesIndicator= value
			end set
		end property
		

		<ColumnInfo("SalesUnitIndicator","{0}")> _
		public property SalesUnitIndicator as byte
			get
				return _salesUnitIndicator
			end get
			set(byval value as byte)
				_salesUnitIndicator= value
			end set
		end property
		

		<ColumnInfo("MechanicIndicator","{0}")> _
		public property MechanicIndicator as byte
			get
				return _mechanicIndicator
			end get
			set(byval value as byte)
				_mechanicIndicator= value
			end set
		end property
		

		<ColumnInfo("SparePartIndicator","{0}")> _
		public property SparePartIndicator as byte
			get
				return _sparePartIndicator
			end get
			set(byval value as byte)
				_sparePartIndicator= value
			end set
		end property
		

		<ColumnInfo("SPAdminIndicator","{0}")> _
		public property SPAdminIndicator as byte
			get
				return _sPAdminIndicator
			end get
			set(byval value as byte)
				_sPAdminIndicator= value
			end set
		end property
		

		<ColumnInfo("SPWareHouseIndicator","{0}")> _
		public property SPWareHouseIndicator as byte
			get
				return _sPWareHouseIndicator
			end get
			set(byval value as byte)
				_sPWareHouseIndicator= value
			end set
		end property
		

		<ColumnInfo("SPCounterIndicator","{0}")> _
		public property SPCounterIndicator as byte
			get
				return _sPCounterIndicator
			end get
			set(byval value as byte)
				_sPCounterIndicator= value
			end set
		end property
		

		<ColumnInfo("SPSalesIndicator","{0}")> _
		public property SPSalesIndicator as byte
			get
				return _sPSalesIndicator
			end get
			set(byval value as byte)
				_sPSalesIndicator= value
			end set
		end property
		

		<ColumnInfo("IsRequestID","{0}")> _
		public property IsRequestID as byte
			get
				return _isRequestID
			end get
			set(byval value as byte)
				_isRequestID= value
			end set
		end property
		

		<ColumnInfo("Status","'{0}'")> _
		public property Status as string
			get
				return _status
			end get
			set(byval value as string)
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
		

		<ColumnInfo("DealerCode","'{0}'")> _
		public property DealerCode as string
			get
				return _dealerCode
			end get
			set(byval value as string)
				_dealerCode= value
			end set
        End Property

        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property
		

		<ColumnInfo("ProvinceName","'{0}'")> _
		public property ProvinceName as string
			get
				return _provinceName
			end get
			set(byval value as string)
				_provinceName= value
			end set
		end property
		

		<ColumnInfo("DivisiID","{0}")> _
		public property DivisiID as integer
			get
				return _divisiID
			end get
			set(byval value as integer)
				_divisiID= value
			end set
		end property
		

		<ColumnInfo("DivisiName","'{0}'")> _
		public property DivisiName as string
			get
				return _divisiName
			end get
			set(byval value as string)
				_divisiName= value
			end set
		end property
		

		<ColumnInfo("PosisiID","{0}")> _
		public property PosisiID as integer
			get
				return _posisiID
			end get
			set(byval value as integer)
				_posisiID= value
			end set
		end property
		

		<ColumnInfo("PosisiName","'{0}'")> _
		public property PosisiName as string
			get
				return _posisiName
			end get
			set(byval value as string)
				_posisiName= value
			end set
		end property
		

		<ColumnInfo("LevelID","{0}")> _
		public property LevelID as integer
			get
				return _levelID
			end get
			set(byval value as integer)
				_levelID= value
			end set
		end property
		

		<ColumnInfo("LevelName","'{0}'")> _
		public property LevelName as string
			get
				return _levelName
			end get
			set(byval value as string)
				_levelName= value
			end set
		end property
		

		<ColumnInfo("Salary","{0}")> _
		public property Salary as decimal
			get
				return _salary
			end get
			set(byval value as decimal)
				_salary= value
			end set
		end property
		

		<ColumnInfo("LeaderCode","'{0}'")> _
		public property LeaderCode as string
			get
				return _leaderCode
			end get
			set(byval value as string)
				_leaderCode= value
			end set
		end property
		

		<ColumnInfo("LeaderName","'{0}'")> _
		public property LeaderName as string
			get
				return _leaderName
			end get
			set(byval value as string)
				_leaderName= value
			end set
		end property
		

		<ColumnInfo("AreaDesc","'{0}'")> _
		public property AreaDesc as string
			get
				return _areaDesc
			end get
			set(byval value as string)
				_areaDesc= value
			end set
		end property
		

		<ColumnInfo("PENDIDIKAN","'{0}'")> _
		public property PENDIDIKAN as string
			get
				return _pENDIDIKAN
			end get
			set(byval value as string)
				_pENDIDIKAN= value
			end set
		end property
		

		<ColumnInfo("EMAIL","'{0}'")> _
		public property EMAIL as string
			get
				return _eMAIL
			end get
			set(byval value as string)
				_eMAIL= value
			end set
		end property
		

		<ColumnInfo("NO_HP","'{0}'")> _
		public property NO_HP as string
			get
				return _nO_HP
			end get
			set(byval value as string)
				_nO_HP= value
			end set
		end property
		

		<ColumnInfo("NOKTP","'{0}'")> _
		public property NOKTP as string
			get
				return _nOKTP
			end get
			set(byval value as string)
				_nOKTP= value
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

