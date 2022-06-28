
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFConfig Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 11:29:34 AM
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
	<Serializable(), TableInfo("SFConfig")> _
	public class SFConfig
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
		private _name as string = String.Empty 		
		private _url as string = String.Empty 		
		private _username as string = String.Empty 		
		private _password as string = String.Empty 		
		private _consumerSecret as string = String.Empty 		
        Private _consumerKey As String = String.Empty
        Private _webProxy As String = String.Empty
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		


		
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
		

		<ColumnInfo("Name","'{0}'")> _
		public property Name as string
			get
				return _name
			end get
			set(byval value as string)
				_name= value
			end set
		end property
		

		<ColumnInfo("Url","'{0}'")> _
		public property Url as string
			get
				return _url
			end get
			set(byval value as string)
				_url= value
			end set
		end property
		

		<ColumnInfo("Username","'{0}'")> _
		public property Username as string
			get
				return _username
			end get
			set(byval value as string)
				_username= value
			end set
		end property
		

		<ColumnInfo("Password","'{0}'")> _
		public property Password as string
			get
				return _password
			end get
			set(byval value as string)
				_password= value
			end set
		end property
		

		<ColumnInfo("ConsumerSecret","'{0}'")> _
		public property ConsumerSecret as string
			get
				return _consumerSecret
			end get
			set(byval value as string)
				_consumerSecret= value
			end set
		end property
		

		<ColumnInfo("ConsumerKey","'{0}'")> _
		public property ConsumerKey as string
			get
				return _consumerKey
			end get
			set(byval value as string)
				_consumerKey= value
			end set
        End Property

        <ColumnInfo("WebProxy", "'{0}'")> _
        Public Property WebProxy As String
            Get
                Return _webProxy
            End Get
            Set(ByVal value As String)
                _webProxy = value
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

