#region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : VWI_FinishUnitCeiling Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2021 - 4:22:33 PM
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
	<Serializable(), TableInfo("VWI_FinishUnitCeiling")> _
	public class VWI_FinishUnitCeiling
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
		private _creditAccount as string = String.Empty 		
		private _dealerName as string = String.Empty 		
        Private _productCategoryID As Short
        Private _standardCeiling As Decimal
        Private _factoringCeiling As Decimal
        Private _outstanding As Decimal
        Private _availableCeiling As Decimal
		private _status as short 		
		private _maxTOPDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		


		
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
		

		<ColumnInfo("CreditAccount","'{0}'")> _
		public property CreditAccount as string
			get
				return _creditAccount
			end get
			set(byval value as string)
				_creditAccount= value
			end set
		end property
		

		<ColumnInfo("DealerName","'{0}'")> _
		public property DealerName as string
			get
				return _dealerName
			end get
			set(byval value as string)
				_dealerName= value
			end set
		end property
		

        <ColumnInfo("ProductCategoryID", "'{0}'")> _
        Public Property ProductCategoryID As Short
            Get
                Return _productCategoryID
            End Get
            Set(ByVal value As Short)
                _productCategoryID = value
            End Set
        End Property
		

        <ColumnInfo("StandardCeiling", "{0}")> _
        Public Property StandardCeiling As Decimal
            Get
                Return _standardCeiling
            End Get
            Set(ByVal value As Decimal)
                _standardCeiling = value
            End Set
        End Property
		

        <ColumnInfo("FactoringCeiling", "{0}")> _
        Public Property FactoringCeiling As Decimal
            Get
                Return _factoringCeiling
            End Get
            Set(ByVal value As Decimal)
                _factoringCeiling = value
            End Set
        End Property
		

        <ColumnInfo("Outstanding", "{0}")> _
        Public Property Outstanding As Decimal
            Get
                Return _outstanding
            End Get
            Set(ByVal value As Decimal)
                _outstanding = value
            End Set
        End Property
		

        <ColumnInfo("AvailableCeiling", "{0}")> _
        Public Property AvailableCeiling As Decimal
            Get
                Return _availableCeiling
            End Get
            Set(ByVal value As Decimal)
                _availableCeiling = value
            End Set
        End Property
		

		<ColumnInfo("Status","{0}")> _
		public property Status as short
			get
				return _status
			end get
			set(byval value as short)
				_status= value
			end set
		end property
		

		<ColumnInfo("MaxTOPDate","'{0:yyyy/MM/dd}'")> _
		public property MaxTOPDate as DateTime
			get
				return _maxTOPDate
			end get
			set(byval value as DateTime)
				_maxTOPDate= new DateTime(value.Year,value.Month,value.Day)
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
