
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanPartHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/17/2011 - 4:08:11 PM
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
	<Serializable(), TableInfo("SalesmanPartHistory")> _
	public class SalesmanPartHistory
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
        'private _salesmanHeaderID as integer 		
        'private _salesmanCategoryLevelID as integer 		
        Private _salesmanLevel As Integer
        Private _salesmanCode As String = String.Empty
		private _changedDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _status as integer 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
        Private _salesmanHeaderID As SalesmanHeader
        Private _salesmanCategoryLevelID As SalesmanCategoryLevel
        Private _dealerID As Dealer

		
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
		

        '<ColumnInfo("SalesmanHeaderID","{0}")> _
        'public property SalesmanHeaderID as integer
        '	get
        '		return _salesmanHeaderID
        '	end get
        '	set(byval value as integer)
        '		_salesmanHeaderID= value
        '	end set
        'end property
        <ColumnInfo("SalesmanHeaderID", "{0}"), _
              RelationInfo("SalesmanHeader", "ID", "SalesmanPartHistory", "SalesmanHeaderID")> _
              Public Property SalesmanHeader() As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeaderID) AndAlso (Not Me._salesmanHeaderID.IsLoaded) Then

                        Me._salesmanHeaderID = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeaderID.ID), SalesmanHeader)
                        Me._salesmanHeaderID.MarkLoaded()

                    End If

                    Return Me._salesmanHeaderID

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeaderID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeaderID.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("SalesmanCategoryLevelID", "{0}")> _
        'Public Property SalesmanCategoryLevelID() As Integer
        '    Get
        '        Return _salesmanCategoryLevelID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _salesmanCategoryLevelID = value
        '    End Set
        'End Property

        <ColumnInfo("SalesmanCategoryLevelID", "{0}"), _
        RelationInfo("SalesmanCategoryLevel", "ID", "SalesmanPartHistory", "SalesmanCategoryLevelID")> _
        Public Property SalesmanCategoryLevel() As SalesmanCategoryLevel
            Get
                Try
                    If Not IsNothing(Me._salesmanCategoryLevelID) AndAlso (Not Me._salesmanCategoryLevelID.IsLoaded) Then

                        Me._salesmanCategoryLevelID = CType(DoLoad(GetType(SalesmanCategoryLevel).ToString(), _salesmanCategoryLevelID.ID), SalesmanCategoryLevel)
                        Me._salesmanCategoryLevelID.MarkLoaded()

                    End If

                    Return Me._salesmanCategoryLevelID

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanCategoryLevel)

                Me._salesmanCategoryLevelID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanCategoryLevelID.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanLevel", "{0}")> _
        Public Property SalesmanLevel() As Integer
            Get
                Return _salesmanLevel
            End Get
            Set(ByVal value As Integer)
                _salesmanLevel = value
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

        <ColumnInfo("ChangedDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ChangedDate() As DateTime
            Get
                Return _changedDate
            End Get
            Set(ByVal value As DateTime)
                _changedDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
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

        <ColumnInfo("DealerID", "{0}"), _
                      RelationInfo("Dealer", "ID", "SalesmanPartHistory", "DealerID")> _
                      Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealerID) AndAlso (Not Me._dealerID.IsLoaded) Then

                        Me._dealerID = CType(DoLoad(GetType(Dealer).ToString(), _dealerID.ID), Dealer)
                        Me._dealerID.MarkLoaded()

                    End If

                    Return Me._dealerID

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealerID = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerID.MarkLoaded()
                End If
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

