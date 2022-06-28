
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPRegistrationHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2017 - 1:50:13 PM
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
	<Serializable(), TableInfo("MSPRegistrationHistory")> _
	public class MSPRegistrationHistory
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
		private _mSPRegistrationID as integer 	
		private _benefitMasterHeaderID as integer 		
		private _registrationDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _requestType as string = String.Empty 		
		private _status as short 		
		private _printedBy as string = String.Empty 		
		private _printedDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
        Private _sFDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _soldBy As String = String.Empty
        Private _isTransfertoSF As Boolean
        Private _isDownloadCertificate As Boolean
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _objMSPRegistration As MSPRegistration
        Private _objMSPMaster As MSPMaster
        Private _debitChargeNo As String = String.Empty
        Private _selisihAmount As Decimal
        Private _sequence As Short
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
		

        <ColumnInfo("MSPRegistrationID", "{0}"), _
        RelationInfo("MSPRegistration", "ID", "MSPRegistrationHistory", "MSPRegistrationID")> _
        Public Property MSPRegistration() As MSPRegistration
            Get
                Try
                    If Not IsNothing(Me._objMSPRegistration) AndAlso (Not Me._objMSPRegistration.IsLoaded) Then
                        Me._objMSPRegistration = CType(DoLoad(GetType(MSPRegistration).ToString(), _objMSPRegistration.ID), MSPRegistration)
                        Me._objMSPRegistration.MarkLoaded()
                    End If

                    Return Me._objMSPRegistration
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPRegistration)

                Me._objMSPRegistration = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objMSPRegistration.MarkLoaded()
                End If
            End Set
        End Property
		

        <ColumnInfo("MSPMasterID", "{0}"), _
        RelationInfo("MSPMaster", "ID", "MSPRegistrationHistory", "MSPMasterID")> _
        Public Property MSPMaster() As MSPMaster
            Get
                Try
                    If Not IsNothing(Me._objMSPMaster) AndAlso (Not Me._objMSPMaster.IsLoaded) Then
                        Me._objMSPMaster = CType(DoLoad(GetType(MSPMaster).ToString(), _objMSPMaster.ID), MSPMaster)
                        Me._objMSPMaster.MarkLoaded()
                    End If

                    Return Me._objMSPMaster
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPMaster)

                Me._objMSPMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objMSPMaster.MarkLoaded()
                End If
            End Set
        End Property
		

		<ColumnInfo("BenefitMasterHeaderID","{0}")> _
		public property BenefitMasterHeaderID as integer
			get
				return _benefitMasterHeaderID
			end get
			set(byval value as integer)
				_benefitMasterHeaderID= value
			end set
		end property
		

		<ColumnInfo("RegistrationDate","'{0:yyyy/MM/dd}'")> _
		public property RegistrationDate as DateTime
			get
				return _registrationDate
			end get
			set(byval value as DateTime)
				_registrationDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

		<ColumnInfo("RequestType","'{0}'")> _
		public property RequestType as string
			get
				return _requestType
			end get
			set(byval value as string)
				_requestType= value
			end set
		end property
		

		<ColumnInfo("Status","{0}")> _
		public property Status as short
			get
				return _status
			end get
			set(byval value as short)
				_status= value
			end set
		end property
		

		<ColumnInfo("PrintedBy","'{0}'")> _
		public property PrintedBy as string
			get
				return _printedBy
			end get
			set(byval value as string)
				_printedBy= value
			end set
		end property

        <ColumnInfo("SoldBy", "'{0}'")> _
        Public Property SoldBy As String
            Get
                Return _soldBy
            End Get
            Set(ByVal value As String)
                _soldBy = value
            End Set
        End Property

		<ColumnInfo("PrintedDate","'{0:yyyy/MM/dd}'")> _
		public property PrintedDate as DateTime
			get
				return _printedDate
			end get
			set(byval value as DateTime)
				_printedDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

		<ColumnInfo("SFDate","'{0:yyyy/MM/dd}'")> _
		public property SFDate as DateTime
			get
				return _sFDate
			end get
			set(byval value as DateTime)
				_sFDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property

        <ColumnInfo("IsTransferToSF", "'{0}'")> _
        Public Property IsTransferToSF As Boolean
            Get
                Return _isTransfertoSF
            End Get
            Set(ByVal value As Boolean)
                _isTransfertoSF = value
            End Set
        End Property

        <ColumnInfo("IsDownloadCertificate", "'{0}'")> _
        Public Property IsDownloadCertificate As Boolean
            Get
                Return _isDownloadCertificate
            End Get
            Set(ByVal value As Boolean)
                _isDownloadCertificate = value
            End Set
        End Property

        <ColumnInfo("DebitChargeNo", "'{0}'")> _
        Public Property DebitChargeNo As String
            Get
                Return _debitChargeNo
            End Get
            Set(ByVal value As String)
                _debitChargeNo = value
            End Set
        End Property

        <ColumnInfo("SelisihAmount", "{0}")> _
        Public Property SelisihAmount As Decimal
            Get
                Return _selisihAmount
            End Get
            Set(ByVal value As Decimal)
                _selisihAmount = value
            End Set
        End Property

        <ColumnInfo("Sequence", "{0}")> _
        Public Property Sequence As Short
            Get
                Return _sequence
            End Get
            Set(ByVal value As Short)
                _sequence = value
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

