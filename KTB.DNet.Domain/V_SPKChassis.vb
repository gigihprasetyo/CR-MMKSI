
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SPKChassis Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 11/23/2011 - 1:17:39 PM
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
	<Serializable(), TableInfo("V_SPKChassis")> _
	public class V_SPKChassis
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval EndCustomerID as integer )
			_endCustomerID = EndCustomerID
		end sub
		
		#end region
		
		#region "Private Variables"
		
		private _iD as integer 		
		private _dealerID as short 		
		private _status as string = String.Empty 		
        Private _sPKNumber As String = String.Empty
        Private _indentNumber As String = String.Empty
		private _dealerSPKNumber as string = String.Empty 		
		private _planDeliveryMonth as byte 		
		private _planDeliveryYear as short 		
		private _planDeliveryDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _planInvoiceMonth as byte 		
		private _planInvoiceYear as short 		
		private _planInvoiceDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _customerRequestID as integer 		
        'Private _sPKCustomerID As Integer
		private _validateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _validateBy as string = String.Empty 		
		private _rejectedReason as string = String.Empty 		
        'private _salesmanHeaderID as short 		
		private _rowStatus as short 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _createdBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _dealerName as string = String.Empty 		
		private _dealerCode as string = String.Empty 		
		private _chassisNumber as string = String.Empty 		
		private _endCustomerID as integer 		
		private _fakturValidateBy as string = String.Empty 		
        Private _fakturValidateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturNumber As String = String.Empty
		private _chassisMasterID as integer 		
		private _fakturStatus as string = String.Empty 		
        Private _dealerSPKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sPKCustomer As SPKCustomer
        Private _salesmanHeader As SalesmanHeader


		
		#end region
		
		#region "Public Properties"
        <ColumnInfo("DealerSPKDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DealerSPKDate() As DateTime
            Get
                Return _dealerSPKDate
            End Get
            Set(ByVal value As DateTime)
                _dealerSPKDate = value
            End Set
        End Property

		<ColumnInfo("ID","{0}")> _
		public property ID as integer
			get
				return _iD
			end get
			set(byval value as integer)
				_iD= value
			end set
		end property
		

		<ColumnInfo("DealerID","{0}")> _
		public property DealerID as short
			get
				return _dealerID
			end get
			set(byval value as short)
				_dealerID= value
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
		

		<ColumnInfo("SPKNumber","'{0}'")> _
		public property SPKNumber as string
			get
				return _sPKNumber
			end get
			set(byval value as string)
				_sPKNumber= value
			end set
		end property

        <ColumnInfo("IndentNumber", "'{0}'")> _
        Public Property IndentNumber() As String
            Get
                Return _indentNumber
            End Get
            Set(ByVal value As String)
                _indentNumber = value
            End Set
        End Property

        <ColumnInfo("DealerSPKNumber", "'{0}'")> _
        Public Property DealerSPKNumber() As String
            Get
                Return _dealerSPKNumber
            End Get
            Set(ByVal value As String)
                _dealerSPKNumber = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryMonth", "{0}")> _
        Public Property PlanDeliveryMonth() As Byte
            Get
                Return _planDeliveryMonth
            End Get
            Set(ByVal value As Byte)
                _planDeliveryMonth = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryYear", "{0}")> _
        Public Property PlanDeliveryYear() As Short
            Get
                Return _planDeliveryYear
            End Get
            Set(ByVal value As Short)
                _planDeliveryYear = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanDeliveryDate() As DateTime
            Get
                Return _planDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _planDeliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PlanInvoiceMonth", "{0}")> _
        Public Property PlanInvoiceMonth() As Byte
            Get
                Return _planInvoiceMonth
            End Get
            Set(ByVal value As Byte)
                _planInvoiceMonth = value
            End Set
        End Property


        <ColumnInfo("PlanInvoiceYear", "{0}")> _
        Public Property PlanInvoiceYear() As Short
            Get
                Return _planInvoiceYear
            End Get
            Set(ByVal value As Short)
                _planInvoiceYear = value
            End Set
        End Property


        <ColumnInfo("PlanInvoiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanInvoiceDate() As DateTime
            Get
                Return _planInvoiceDate
            End Get
            Set(ByVal value As DateTime)
                _planInvoiceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerRequestID", "{0}")> _
        Public Property CustomerRequestID() As Integer
            Get
                Return _customerRequestID
            End Get
            Set(ByVal value As Integer)
                _customerRequestID = value
            End Set
        End Property

        <ColumnInfo("SPKCustomerID", "{0}"), _
              RelationInfo("SPKCustomer", "ID", "SPKHeader", "SPKCustomerID")> _
              Public Property SPKCustomer() As SPKCustomer
            Get
                Try
                    If Not IsNothing(Me._sPKCustomer) AndAlso (Not Me._sPKCustomer.IsLoaded) Then

                        Me._sPKCustomer = CType(DoLoad(GetType(SPKCustomer).ToString(), _sPKCustomer.ID), SPKCustomer)
                        Me._sPKCustomer.MarkLoaded()

                    End If

                    Return Me._sPKCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKCustomer)

                Me._sPKCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKCustomer.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("SPKCustomerID", "{0}")> _
        'Public Property SPKCustomerID() As Integer
        '    Get
        '        Return _sPKCustomer
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sPKCustomer = value
        '    End Set
        'End Property


        <ColumnInfo("ValidateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidateTime() As DateTime
            Get
                Return _validateTime
            End Get
            Set(ByVal value As DateTime)
                _validateTime = value
            End Set
        End Property


        <ColumnInfo("ValidateBy", "'{0}'")> _
        Public Property ValidateBy() As String
            Get
                Return _validateBy
            End Get
            Set(ByVal value As String)
                _validateBy = value
            End Set
        End Property


        <ColumnInfo("RejectedReason", "'{0}'")> _
        Public Property RejectedReason() As String
            Get
                Return _rejectedReason
            End Get
            Set(ByVal value As String)
                _rejectedReason = value
            End Set
        End Property


        '<ColumnInfo("SalesmanHeaderID","{0}")> _
        'public property SalesmanHeaderID as short
        '	get
        '		return _salesmanHeaderID
        '	end get
        '	set(byval value as short)
        '		_salesmanHeaderID= value
        '	end set
        'end property
        <ColumnInfo("SalesmanHeaderID", "{0}"), _
              RelationInfo("SalesmanHeader", "ID", "SPKHeader", "SalesmanHeaderID")> _
              Public Property SalesmanHeader() As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeader) AndAlso (Not Me._salesmanHeader.IsLoaded) Then

                        Me._salesmanHeader = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeader.ID), SalesmanHeader)
                        Me._salesmanHeader.MarkLoaded()

                    End If

                    Return Me._salesmanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeader.MarkLoaded()
                End If
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber() As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        <ColumnInfo("EndCustomerID", "{0}")> _
        Public Property EndCustomerID() As Integer
            Get
                Return _endCustomerID
            End Get
            Set(ByVal value As Integer)
                _endCustomerID = value
            End Set
        End Property


        <ColumnInfo("FakturValidateBy", "'{0}'")> _
        Public Property FakturValidateBy() As String
            Get
                Return _fakturValidateBy
            End Get
            Set(ByVal value As String)
                _fakturValidateBy = value
            End Set
        End Property


        <ColumnInfo("FakturValidateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturValidateTime() As DateTime
            Get
                Return _fakturValidateTime
            End Get
            Set(ByVal value As DateTime)
                _fakturValidateTime = value
            End Set
        End Property

        <ColumnInfo("FakturNumber", "'{0}'")> _
        Public Property FakturNumber() As String
            Get
                Return _fakturNumber
            End Get
            Set(ByVal value As String)
                _fakturNumber = value
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}")> _
        Public Property ChassisMasterID() As Integer
            Get
                Return _chassisMasterID
            End Get
            Set(ByVal value As Integer)
                _chassisMasterID = value
            End Set
        End Property


        <ColumnInfo("FakturStatus", "'{0}'")> _
        Public Property FakturStatus() As String
            Get
                Return _fakturStatus
            End Get
            Set(ByVal value As String)
                _fakturStatus = value
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

