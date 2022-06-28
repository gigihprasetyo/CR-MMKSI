
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SalesmanPartTarget Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 7/14/2011 - 10:16:53 AM
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
	<Serializable(), TableInfo("V_SalesmanPartTarget")> _
	public class V_SalesmanPartTarget
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
		private _salesmanHeaderID as short 		
		private _year as short 		
		private _month as short 		
        Private _period As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
		private _target as decimal 		
		private _realization as decimal 		
		private _persentage as decimal 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _dealerID as short 		
		private _searchTerm2 as string = String.Empty 		
		private _dealerCode as string = String.Empty 		
		private _salesmanCode as string = String.Empty 		
		private _areaDesc as string = String.Empty 		
		private _name as string = String.Empty 		
        Private _kategori As String = String.Empty
        Private _status As Short
		private _posisi as string = String.Empty 		
		private _level as string = String.Empty 		
		private _targetDealer as decimal 		
		private _realizationDealer as decimal 		
		


		
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
		

		<ColumnInfo("SalesmanHeaderID","{0}")> _
		public property SalesmanHeaderID as short
			get
				return _salesmanHeaderID
			end get
			set(byval value as short)
				_salesmanHeaderID= value
			end set
		end property
		

		<ColumnInfo("Year","{0}")> _
		public property Year as short
			get
				return _year
			end get
			set(byval value as short)
				_year= value
			end set
		end property
		

		<ColumnInfo("Month","{0}")> _
		public property Month as short
			get
				return _month
			end get
			set(byval value as short)
				_month= value
			end set
		end property
		

        '<ColumnInfo("Period","{0}")> _
        <ColumnInfo("Period", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property Period() As DateTime
            Get
                Return _period
            End Get
            Set(ByVal value As DateTime)
                _period = value
            End Set
        End Property


        <ColumnInfo("Target", "{0}")> _
        Public Property Target() As Decimal
            Get
                Return _target
            End Get
            Set(ByVal value As Decimal)
                _target = value
            End Set
        End Property


        <ColumnInfo("Realization", "{0}")> _
        Public Property Realization() As Decimal
            Get
                Return _realization
            End Get
            Set(ByVal value As Decimal)
                _realization = value
            End Set
        End Property


        <ColumnInfo("Persentage", "#,##0")> _
        Public Property Persentage() As Decimal
            Get
                Return _persentage
            End Get
            Set(ByVal value As Decimal)
                _persentage = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("SearchTerm2", "'{0}'")> _
        Public Property SearchTerm2() As String
            Get
                Return _searchTerm2
            End Get
            Set(ByVal value As String)
                _searchTerm2 = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode() As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property


        <ColumnInfo("AreaDesc", "'{0}'")> _
        Public Property AreaDesc() As String
            Get
                Return _areaDesc
            End Get
            Set(ByVal value As String)
                _areaDesc = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        <ColumnInfo("Kategori", "'{0}'")> _
        Public Property Kategori() As String
            Get
                Return _kategori
            End Get
            Set(ByVal value As String)
                _kategori = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property

        <ColumnInfo("Posisi", "'{0}'")> _
        Public Property Posisi() As String
            Get
                Return _posisi
            End Get
            Set(ByVal value As String)
                _posisi = value
            End Set
        End Property


        <ColumnInfo("Level", "'{0}'")> _
        Public Property Level() As String
            Get
                Return _level
            End Get
            Set(ByVal value As String)
                _level = value
            End Set
        End Property


        <ColumnInfo("TargetDealer", "{0}")> _
        Public Property TargetDealer() As Decimal
            Get
                Return _targetDealer
            End Get
            Set(ByVal value As Decimal)
                _targetDealer = value
            End Set
        End Property


        <ColumnInfo("RealizationDealer", "{0}")> _
        Public Property RealizationDealer() As Decimal
            Get
                Return _realizationDealer
            End Get
            Set(ByVal value As Decimal)
                _realizationDealer = value
            End Set
        End Property




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

