
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFLastSuccess Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/05/2018 - 7:59:19 AM
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
	<Serializable(), TableInfo("SFLastSuccess")> _
	public class SFLastSuccess
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
		private _sFStagingLogID as integer 		
		private _lastSuccessTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdatedBy as string = String.Empty 		
		private _lastUpdatedTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _objSFMasterObject As SFMasterObject
        Private _objSFStagingLog As SFStagingLog

		
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
        RelationInfo("SFMasterObject", "ID", "SFLastSuccess", "SFMasterObjectID")> _
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


        <ColumnInfo("SFStagingLogID", "{0}"), _
        RelationInfo("SFStagingLog", "ID", "SFLastSuccess", "SFStagingLogID")> _
        Public Property SFStagingLog As SFStagingLog
            Get
                Try
                    If Not IsNothing(Me._objSFStagingLog) AndAlso (Not Me._objSFStagingLog.IsLoaded) Then
                        Me._objSFStagingLog = CType(DoLoad(GetType(SFStagingLog).ToString(), _objSFStagingLog.ID), SFStagingLog)
                        Me._objSFStagingLog.MarkLoaded()
                    End If

                    Return Me._objSFStagingLog
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As SFStagingLog)

                Me._objSFStagingLog = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objSFStagingLog.MarkLoaded()
                End If
            End Set
        End Property
		

		<ColumnInfo("LastSuccessTime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property LastSuccessTime as DateTime
			get
				return _lastSuccessTime
			end get
			set(byval value as DateTime)
				_lastSuccessTime= value
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
		

		<ColumnInfo("LastUpdatedBy","'{0}'")> _
		public property LastUpdatedBy as string
			get
				return _lastUpdatedBy
			end get
			set(byval value as string)
				_lastUpdatedBy= value
			end set
		end property
		

		<ColumnInfo("LastUpdatedTime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property LastUpdatedTime as DateTime
			get
				return _lastUpdatedTime
			end get
			set(byval value as DateTime)
				_lastUpdatedTime= value
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

