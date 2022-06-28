
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCParameterCondition Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/05/2020 - 13:59:24
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
	<Serializable(), TableInfo("WSCParameterCondition")> _
	public class WSCParameterCondition
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval ID as long )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
		private _iD as long 		
        'private _wSCParameterHeaderID as integer 	
        Private _wSCParameterHeader As WSCParameterHeader
        Private _wSCParameterConditionID As Integer
		private _kind as integer 		
		private _operator as integer 		
		private _value as string = String.Empty 		
		private _functions as integer 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _dataStatus As Integer = 0
        Private _wSCParameterConditionIndex As Integer = -1

		
		#end region
		
		#region "Public Properties"
		
		<ColumnInfo("ID","{0}")> _
		public property ID as long
			get
				return _iD
			end get
			set(byval value as long)
				_iD= value
			end set
		end property
		

        '<ColumnInfo("WSCParameterHeaderID","{0}")> _
        'public property WSCParameterHeaderID as integer
        '	get
        '		return _wSCParameterHeaderID
        '	end get
        '	set(byval value as integer)
        '		_wSCParameterHeaderID= value
        '	end set
        'end property

        <ColumnInfo("WSCParameterHeaderID", "{0}"), _
        RelationInfo("WSCParameterHeader", "ID", "WSCParameterCondition", "WSCParameterHeaderID")> _
        Public Property WSCParameterHeader() As WSCParameterHeader
            Get
                Try
                    If Not IsNothing(Me._wSCParameterHeader) AndAlso (Not Me._wSCParameterHeader.IsLoaded) Then

                        Me._wSCParameterHeader = CType(DoLoad(GetType(WSCParameterHeader).ToString(), _wSCParameterHeader.ID), WSCParameterHeader)
                        Me._wSCParameterHeader.MarkLoaded()

                    End If

                    Return Me._wSCParameterHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As WSCParameterHeader)

                Me._wSCParameterHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._wSCParameterHeader.MarkLoaded()
                End If
            End Set
        End Property

		<ColumnInfo("WSCParameterConditionID","{0}")> _
		public property WSCParameterConditionID as integer
			get
				return _wSCParameterConditionID
			end get
			set(byval value as integer)
				_wSCParameterConditionID= value
			end set
		end property
		

		<ColumnInfo("Kind","{0}")> _
		public property Kind as integer
			get
				return _kind
			end get
			set(byval value as integer)
				_kind= value
			end set
		end property
		

        <ColumnInfo("Operator", "{0}")> _
        Public Property Operators As Integer
            Get
                Return _operator
            End Get
            Set(ByVal value As Integer)
                _operator = value
            End Set
        End Property
		

		<ColumnInfo("Value","'{0}'")> _
		public property Value as string
			get
				return _value
			end get
			set(byval value as string)
				_value= value
			end set
		end property
		

		<ColumnInfo("Functions","{0}")> _
		public property Functions as integer
			get
				return _functions
			end get
			set(byval value as integer)
				_functions= value
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
		

        Public Property DataStatus As Integer
            Get
                Return _dataStatus
            End Get
            Set(ByVal value As Integer)
                _dataStatus = value
            End Set
        End Property

        Public Property WSCParameterConditionIndex As Integer
            Get
                Return _wSCParameterConditionIndex
            End Get
            Set(ByVal value As Integer)
                _wSCParameterConditionIndex = value
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

