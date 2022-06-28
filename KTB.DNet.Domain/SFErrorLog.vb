
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFErrorLog Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 11:30:18 AM
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
	<Serializable(), TableInfo("SFErrorLog")> _
	public class SFErrorLog
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
		private _exceptionMessage as string = String.Empty 		
		private _exceptionStartTrace as string = String.Empty 		
		private _errorDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _objSFMasterObject As SFMasterObject

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
		
        <ColumnInfo("SFMasterObjectID", "{0}"), _
        RelationInfo("SFMasterObject", "ID", "SFErrorLog", "SFMasterObjectID")> _
        Public Property SFMasterObject As SFMasterObject
            Get
                Try
                    If Not IsNothing(Me._objSFMasterObject) AndAlso (Not Me._objSFMasterObject.IsLoaded) Then
                        Me._objSFMasterObject = CType(DoLoad(GetType(SFMasterObject).ToString(), _objSFMasterObject.ID), SFMasterObject)
                        Me._objSFMasterObject.MarkLoaded()
                    End If

                    Return Me._objSFMasterObject
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As SFMasterObject)

                Me._objSFMasterObject = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objSFMasterObject.MarkLoaded()
                End If
            End Set
        End Property

		<ColumnInfo("ExceptionMessage","'{0}'")> _
		public property ExceptionMessage as string
			get
				return _exceptionMessage
			end get
			set(byval value as string)
				_exceptionMessage= value
			end set
		end property
		

		<ColumnInfo("ExceptionStartTrace","'{0}'")> _
		public property ExceptionStartTrace as string
			get
				return _exceptionStartTrace
			end get
			set(byval value as string)
				_exceptionStartTrace= value
			end set
		end property
		

		<ColumnInfo("ErrorDate","'{0:yyyy/MM/dd}'")> _
		public property ErrorDate as DateTime
			get
				return _errorDate
			end get
			set(byval value as DateTime)
				_errorDate= new DateTime(value.Year,value.Month,value.Day)
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

