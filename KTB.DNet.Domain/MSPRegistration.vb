
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPRegistration Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2017 - 1:49:49 PM
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
	<Serializable(), TableInfo("MSPRegistration")> _
	public class MSPRegistration
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
        Private _mSPCode As String = String.Empty
        Private _oldMSPCode As String = String.Empty
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _mspRegistrationHistorys As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _mspCustomer As MSPCustomer
        Private _objChassisMaster As ChassisMaster
        Private _objDealer As Dealer

        ' Temp String
        Private _remarks As String
        Private _benefitType As String
		
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
		
        <ColumnInfo("MSPCode", "'{0}'")> _
        Public Property MSPCode As String
            Get
                Return _mSPCode
            End Get
            Set(ByVal value As String)
                _mSPCode = value
            End Set
        End Property

        <ColumnInfo("OldMSPCode", "'{0}'")> _
        Public Property OldMSPCode As String
            Get
                Return _oldMSPCode
            End Get
            Set(ByVal value As String)
                _oldMSPCode = value
            End Set
        End Property

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
		
        <RelationInfo("MSPRegistration", "ID", "MSPRegistrationHistory", "MSPRegistrationID")> _
        Public ReadOnly Property MSPRegistrationHistorys() As System.Collections.ArrayList
            Get
                Try
                    If (Me._mspRegistrationHistorys.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MSPRegistrationHistory), "MSPRegistration", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Sort(GetType(MSPRegistrationHistory), "ID", Sort.SortDirection.ASC))
                        Me._mspRegistrationHistorys = DoLoadArray(GetType(MSPRegistrationHistory).ToString, criterias, sortColl)
                    End If

                    Return Me._mspRegistrationHistorys

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property

        <ColumnInfo("MSPCustomerID", "{0}"), _
        RelationInfo("MSPCustomer", "ID", "MSPRegistration", "MSPCustomerID")> _
        Public Property MSPCustomer() As MSPCustomer
            Get
                Try
                    If Not IsNothing(Me._mspCustomer) AndAlso (Not Me._mspCustomer.IsLoaded) Then
                        Me._mspCustomer = CType(DoLoad(GetType(MSPCustomer).ToString(), _mspCustomer.ID), MSPCustomer)
                        Me._mspCustomer.MarkLoaded()
                    End If

                    Return Me._mspCustomer
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPCustomer)

                Me._mspCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mspCustomer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MSPRegistration", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._objDealer) AndAlso (Not Me._objDealer.IsLoaded) Then
                        Me._objDealer = CType(DoLoad(GetType(Dealer).ToString(), _objDealer.ID), Dealer)
                        Me._objDealer.MarkLoaded()
                    End If

                    Return Me._objDealer
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._objDealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objDealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "MSPRegistration", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._objChassisMaster) AndAlso (Not Me._objChassisMaster.IsLoaded) Then
                        Me._objChassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _objChassisMaster.ID), ChassisMaster)
                        Me._objChassisMaster.MarkLoaded()
                    End If

                    Return Me._objChassisMaster
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._objChassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objChassisMaster.MarkLoaded()
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
        Public Property Remarks As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
            End Set
        End Property

        Public Property BenefitType As String
            Get
                Return _benefitType
            End Get
            Set(ByVal value As String)
                _benefitType = value
            End Set
        End Property
#End Region
		
	end class
end namespace

