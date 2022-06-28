#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKMasterCountryCodePhone Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 6/22/2021 - 11:35:55 AM
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
	<Serializable(), TableInfo("SPKMasterCountryCodePhone")> _
	public class SPKMasterCountryCodePhone
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
		private _countryName as string = String.Empty 		
		private _countryCode as string = String.Empty 		
		private _rowstatus as short 		
		private _createdby as string = String.Empty 		
		private _createdtime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdatedby as string = String.Empty 		
		private _lastUpdatedtime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		


		
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
		

		<ColumnInfo("CountryName","'{0}'")> _
		public property CountryName as string
			get
				return _countryName
			end get
			set(byval value as string)
				_countryName= value
			end set
		end property
		

		<ColumnInfo("CountryCode","'{0}'")> _
		public property CountryCode as string
			get
				return _countryCode
			end get
			set(byval value as string)
				_countryCode= value
			end set
		end property
		

        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowstatus
            End Get
            Set(ByVal value As Short)
                _rowstatus = value
            End Set
        End Property
		

        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdby
            End Get
            Set(ByVal value As String)
                _createdby = value
            End Set
        End Property
		

        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdtime
            End Get
            Set(ByVal value As DateTime)
                _createdtime = value
            End Set
        End Property
		

        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedby As String
            Get
                Return _lastUpdatedby
            End Get
            Set(ByVal value As String)
                _lastUpdatedby = value
            End Set
        End Property
		

        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime As DateTime
            Get
                Return _lastUpdatedtime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedtime = value
            End Set
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
