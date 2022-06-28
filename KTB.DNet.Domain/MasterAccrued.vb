
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MasterAccrued Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/10/2019 - 9:14:10
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
	<Serializable(), TableInfo("MasterAccrued")> _
	public class MasterAccrued
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
		private _bussinessAreaCode as string = String.Empty 		
		private _accKey as string = String.Empty 		
		private _description as string = String.Empty 		
		private _type as string = String.Empty 		
		private _status as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _masterCostCenter As MasterCostCenter
        Private _masterInternalOrder As MasterInternalOrder
		
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
		

		<ColumnInfo("BussinessAreaCode","'{0}'")> _
		public property BussinessAreaCode as string
			get
				return _bussinessAreaCode
			end get
			set(byval value as string)
				_bussinessAreaCode= value
			end set
		end property
		

		<ColumnInfo("AccKey","'{0}'")> _
		public property AccKey as string
			get
				return _accKey
			end get
			set(byval value as string)
				_accKey= value
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
		

		<ColumnInfo("Type","'{0}'")> _
		public property Type as string
			get
				return _type
			end get
			set(byval value as string)
				_type= value
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
		
		
        <ColumnInfo("MasterInternalOrderID", "{0}"), _
        RelationInfo("MasterInternalOrder", "ID", "MasterAccrued", "MasterInternalOrderID")> _
        Public Property MasterInternalOrder() As MasterInternalOrder
            Get
                Try
                    If Not IsNothing(Me._masterInternalOrder) AndAlso (Not Me._masterInternalOrder.IsLoaded) Then

                        Me._masterInternalOrder = CType(DoLoad(GetType(MasterInternalOrder).ToString(), _masterInternalOrder.ID), MasterInternalOrder)
                        Me._masterInternalOrder.MarkLoaded()

                    End If

                    Return Me._masterInternalOrder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MasterInternalOrder)

                Me._masterInternalOrder = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._masterInternalOrder.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("MasterCostCenterID", "{0}"), _
        RelationInfo("MasterCostCenter", "ID", "MasterAccrued", "MasterCostCenterID")> _
        Public Property MasterCostCenter() As MasterCostCenter
            Get
                Try
                    If Not IsNothing(Me._masterCostCenter) AndAlso (Not Me._masterCostCenter.IsLoaded) Then

                        Me._masterCostCenter = CType(DoLoad(GetType(MasterCostCenter).ToString(), _masterCostCenter.ID), MasterCostCenter)
                        Me._masterCostCenter.MarkLoaded()

                    End If

                    Return Me._masterCostCenter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MasterCostCenter)

                Me._masterCostCenter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._masterCostCenter.MarkLoaded()
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

