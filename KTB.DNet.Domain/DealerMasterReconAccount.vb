#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerMasterReconAccount Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 26/05/2020 - 13:52:05
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
	<Serializable(), TableInfo("DealerMasterReconAccount")> _
	public class DealerMasterReconAccount
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
		private _reconAccount as string = String.Empty 		
		private _reconDesc as string = String.Empty 		
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
		

		<ColumnInfo("ReconAccount","'{0}'")> _
		public property ReconAccount as string
			get
				return _reconAccount
			end get
			set(byval value as string)
				_reconAccount= value
			end set
		end property
		

		<ColumnInfo("ReconDesc","'{0}'")> _
		public property ReconDesc as string
			get
				return _reconDesc
			end get
			set(byval value as string)
				_reconDesc= value
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
