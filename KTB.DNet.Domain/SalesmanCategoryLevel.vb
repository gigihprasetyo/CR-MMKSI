
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanCategoryLevel Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/6/2011 - 9:38:40 AM
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
	<Serializable(), TableInfo("SalesmanCategoryLevel")> _
	public class SalesmanCategoryLevel
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
        Public Sub New()

        End Sub
        
		public sub new(byval ID as integer )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
        Private _iD As Integer
        Private _Kode As String
        'private _parentID as integer 		
		private _levelNumber as short 		
		private _positionName as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		

        Private _parentID As SalesmanCategoryLevel

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
		
        <ColumnInfo("Kode", "{0}")> _
        Public Property Kode As String
            Get
                Return _Kode
            End Get
            Set(ByVal value As String)
                _Kode = value
            End Set
        End Property

        '<ColumnInfo("ParentID","{0}")> _
        'public property ParentID as integer
        '	get
        '		return _parentID
        '	end get
        '	set(byval value as integer)
        '		_parentID= value
        '	end set
        'end property


        <ColumnInfo("ParentID", "{0}"), _
        RelationInfo("SalesmanCategoryLevel", "ID", "SalesmanCategoryLevel", "ParentID")> _
        Public Property SalesmanCategoryLevel() As SalesmanCategoryLevel
            Get
                Try
                    If Not IsNothing(Me._parentID) AndAlso (Not Me._parentID.IsLoaded) Then

                        Me._parentID = CType(DoLoad(GetType(SalesmanCategoryLevel).ToString(), _parentID.ID), SalesmanCategoryLevel)
                        Me._parentID.MarkLoaded()

                    End If

                    Return Me._parentID

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanCategoryLevel)

                Me._parentID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._parentID.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("LevelNumber", "{0}")> _
        Public Property LevelNumber() As Short
            Get
                Return _levelNumber
            End Get
            Set(ByVal value As Short)
                _levelNumber = value
            End Set
        End Property


        <ColumnInfo("PositionName", "'{0}'")> _
        Public Property PositionName() As String
            Get
                Return _positionName
            End Get
            Set(ByVal value As String)
                _positionName = value
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

