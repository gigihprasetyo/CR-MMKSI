#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRSolutionReferences Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 2:01:34 PM
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
	<Serializable(), TableInfo("PQRSolutionReferences")> _
	public class PQRSolutionReferences
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
		private _pQRRef as string = String.Empty 		
		private _rowStatus as integer 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
		private _pQRHeader as PQRHeader


		
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
		

		<ColumnInfo("PQRRef","'{0}'")> _
		public property PQRRef as string
			get
				return _pQRRef
			end get
			set(byval value as string)
				_pQRRef= value
			end set
		end property
		

		<ColumnInfo("RowStatus","{0}")> _
		public property RowStatus as integer
			get
				return _rowStatus
			end get
			set(byval value as integer)
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
		
		
		<ColumnInfo("PQRHeaderID","{0}"), _
		RelationInfo("PQRHeader","ID","PQRSolutionReferences","PQRHeaderID")> _
		public property PQRHeader as PQRHeader
			get
				try
					if not isnothing(me._pQRHeader) AndAlso (not me._pQRHeader.IsLoaded) then
					
						me._pQRHeader = ctype(DoLoad(gettype(PQRHeader).ToString(),_pQRHeader.ID), PQRHeader)
						me._pQRHeader.MarkLoaded()
						
					end if
					
					return me._pQRHeader
				
				catch ex as Exception
				
					dim rethrow as boolean = ExceptionPolicy.HandleException(ex,"Domain Policy")
					
					if rethrow then
						throw
					end if
					
				end try
				
				return nothing
			end get
			
			set(byval value as PQRHeader)
			
				me._pQRHeader = value
				if( not isnothing(value)) AndAlso (ctype(value, DomainObject)).IsLoaded then
					me._pQRHeader.MarkLoaded()
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

