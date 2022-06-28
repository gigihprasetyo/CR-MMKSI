
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcVehicleCategory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/7/2011 - 10:46:25 AM
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
	<Serializable(), TableInfo("CcVehicleCategory")> _
	public class CcVehicleCategory
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval ID as short )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
        Private _iD As Short
		private _code as string = String.Empty 		
		private _description as string = String.Empty 		
		private _aliasDescription as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		

		private _ccReportDealers as System.Collections.ArrayList = new System.Collections.ArrayList()
        Private _productCategory As ProductCategory
		
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
        End Property

		<ColumnInfo("Code","'{0}'")> _
		public property Code as string
			get
				return _code
			end get
			set(byval value as string)
				_code= value
			end set
		end property
		

		<ColumnInfo("Description","'{0}'")> _
		public property Description as string
			get
				return _description
			end get
			set(byval value as string)
				_description= value
			end set
		end property
		

		<ColumnInfo("AliasDescription","'{0}'")> _
		public property AliasDescription as string
			get
				return _aliasDescription
			end get
			set(byval value as string)
				_aliasDescription= value
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
		
		
		
		<RelationInfo("CcVehicleCategory","ID","CcReportDealer","CcVehicleCategoryID")> _
		public readonly property CcReportDealers as System.Collections.ArrayList
			get
				try
					if (me._ccReportDealers.Count < 1) then
						dim _criteria as Criteria = new Criteria(gettype(CcReportDealer),"CcVehicleCategory",me.ID)
						dim criterias as CriteriaComposite = new CriteriaComposite(_criteria)
						criterias.opAnd(new Criteria(gettype(CcReportDealer),"RowStatus",MatchType.Exact,ctype(DBRowStatus.Active, short)))

						me._ccReportDealers = DoLoadArray(gettype(CcReportDealer).ToString,criterias)
					end if
					
					return me._ccReportDealers
				
				catch ex as Exception
				
					dim rethrow as boolean = ExceptionPolicy.HandleException(ex,"Domain Policy")
					
					if rethrow then
						throw
					end if
					
				end try
				
				return nothing
				
			end get		
		end property

        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "CcVehicleCategory", "ProductCategoryID")> _
        Public Property ProductCategory() As ProductCategory
            Get
                Try
                    If Not IsNothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

                        Me._productCategory = CType(DoLoad(GetType(ProductCategory).ToString(), _productCategory.ID), ProductCategory)
                        Me._productCategory.MarkLoaded()

                    End If

                    Return Me._productCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProductCategory)

                Me._productCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
                End If
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

