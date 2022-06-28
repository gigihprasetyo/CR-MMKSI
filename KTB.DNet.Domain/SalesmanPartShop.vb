
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanPartShop Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 9/28/2011 - 4:06:43 PM
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
	<Serializable(), TableInfo("SalesmanPartShop")> _
	public class SalesmanPartShop
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
		private _rowStatus as byte 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
		private _salesmanHeader as SalesmanHeader
		private _partShop as PartShop


		
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
		public property RowStatus as byte
			get
				return _rowStatus
			end get
			set(byval value as byte)
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
		
		
		<ColumnInfo("SalesmanHeaderID","{0}"), _
		RelationInfo("SalesmanHeader","ID","SalesmanPartShop","SalesmanHeaderID")> _
		public property SalesmanHeader as SalesmanHeader
			get
				try
					if not isnothing(me._salesmanHeader) AndAlso (not me._salesmanHeader.IsLoaded) then
					
						me._salesmanHeader = ctype(DoLoad(gettype(SalesmanHeader).ToString(),_salesmanHeader.ID), SalesmanHeader)
						me._salesmanHeader.MarkLoaded()
						
					end if
					
					return me._salesmanHeader
				
				catch ex as Exception
				
					dim rethrow as boolean = ExceptionPolicy.HandleException(ex,"Domain Policy")
					
					if rethrow then
						throw
					end if
					
				end try
				
				return nothing
			end get
			
			set(byval value as SalesmanHeader)
			
				me._salesmanHeader = value
				if( not isnothing(value)) AndAlso (ctype(value, DomainObject)).IsLoaded then
					me._salesmanHeader.MarkLoaded()
				end if
			end set
		end property
		
		<ColumnInfo("PartShopID","{0}"), _
		RelationInfo("PartShop","ID","SalesmanPartShop","PartShopID")> _
		public property PartShop as PartShop
			get
				try
					if not isnothing(me._partShop) AndAlso (not me._partShop.IsLoaded) then
					
						me._partShop = ctype(DoLoad(gettype(PartShop).ToString(),_partShop.ID), PartShop)
						me._partShop.MarkLoaded()
						
					end if
					
					return me._partShop
				
				catch ex as Exception
				
					dim rethrow as boolean = ExceptionPolicy.HandleException(ex,"Domain Policy")
					
					if rethrow then
						throw
					end if
					
				end try
				
				return nothing
			end get
			
			set(byval value as PartShop)
			
				me._partShop = value
				if( not isnothing(value)) AndAlso (ctype(value, DomainObject)).IsLoaded then
					me._partShop.MarkLoaded()
				end if
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

