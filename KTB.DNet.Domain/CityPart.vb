
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CityPart Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/24/2011 - 2:28:30 PM
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
	<Serializable(), TableInfo("CityPart")> _
	public class CityPart
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
		private _cityName as string = String.Empty 		
		private _cityCode as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
		private _province as Province

        	Private _partShops As System.Collections.ArrayList = New System.Collections.ArrayList()
        	Private _city As City

		
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

        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "CityPart", "CityID")> _
        Public Property City() As City
            Get
                Try
                    If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

                        Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
                        Me._city.MarkLoaded()

                    End If

                    Return Me._city

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._city = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property

		<ColumnInfo("CityName","'{0}'")> _
		public property CityName as string
			get
				return _cityName
			end get
			set(byval value as string)
				_cityName= value
			end set
		end property
		

		<ColumnInfo("CityCode","'{0}'")> _
		public property CityCode as string
			get
				return _cityCode
			end get
			set(byval value as string)
				_cityCode= value
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
		
		
		<ColumnInfo("ProvinceID","{0}"), _
		RelationInfo("Province","ID","CityPart","ProvinceID")> _
		public property Province as Province
			get
				try
					if not isnothing(me._province) AndAlso (not me._province.IsLoaded) then
					
						me._province = ctype(DoLoad(gettype(Province).ToString(),_province.ID), Province)
						me._province.MarkLoaded()
						
					end if
					
					return me._province
				
				catch ex as Exception
				
					dim rethrow as boolean = ExceptionPolicy.HandleException(ex,"Domain Policy")
					
					if rethrow then
						throw
					end if
					
				end try
				
				return nothing
			end get
			
			set(byval value as Province)
			
				me._province = value
				if( not isnothing(value)) AndAlso (ctype(value, DomainObject)).IsLoaded then
					me._province.MarkLoaded()
				end if
			end set
		end property
		
		
		<RelationInfo("CityPart","ID","PartShop","CityPartID")> _
		public readonly property PartShops as System.Collections.ArrayList
			get
				try
					if (me._partShops.Count < 1) then
						dim _criteria as Criteria = new Criteria(gettype(PartShop),"CityPart",me.ID)
						dim criterias as CriteriaComposite = new CriteriaComposite(_criteria)
						criterias.opAnd(new Criteria(gettype(PartShop),"RowStatus",MatchType.Exact,ctype(DBRowStatus.Active, short)))

						me._partShops = DoLoadArray(gettype(PartShop).ToString,criterias)
					end if
					
					return me._partShops
				
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

