
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerCase Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 7/10/2017 - 10:09:23 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("CustomerCase")> _
    Public Class CustomerCase
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _salesforceID As String = String.Empty
        Private _caseNumber As String = String.Empty
        Private _customerName As String = String.Empty
        Private _phone As String = String.Empty
        Private _email As String = String.Empty
        Private _category As String = String.Empty
        Private _subCategory1 As String = String.Empty
        Private _subCategory2 As String = String.Empty
        Private _subCategory3 As String = String.Empty
        Private _subCategory4 As String = String.Empty
        Private _callerType As String = String.Empty
        Private _carType As String = String.Empty
        Private _variants As String = String.Empty
        Private _engineNumber As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _odometer As Integer
        Private _plateNumber As String = String.Empty
        Private _priority As Short
        Private _caseNumberReff As String = String.Empty
        Private _caseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _subject As String = String.Empty
        Private _description As String = String.Empty
        Private _status As Short
        Private _statusdesc As String = String.Empty
        Private _rowStatus As Short
        Private _reservationNumber As String = String.Empty
        Private _serviceType As String = String.Empty
        Private _bookingDatetime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTIme As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        Private _customerCaseResponses As System.Collections.ArrayList = New System.Collections.ArrayList()



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("SalesforceID", "'{0}'")> _
        Public Property SalesforceID As String
            Get
                Return _salesforceID
            End Get
            Set(ByVal value As String)
                _salesforceID = value
            End Set
        End Property


        <ColumnInfo("CaseNumber", "'{0}'")> _
        Public Property CaseNumber As String
            Get
                Return _caseNumber
            End Get
            Set(ByVal value As String)
                _caseNumber = value
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("Phone", "'{0}'")> _
        Public Property Phone As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("Category", "'{0}'")> _
        Public Property Category As String
            Get
                Return _category
            End Get
            Set(ByVal value As String)
                _category = value
            End Set
        End Property


        <ColumnInfo("SubCategory1", "'{0}'")> _
        Public Property SubCategory1 As String
            Get
                Return _subCategory1
            End Get
            Set(ByVal value As String)
                _subCategory1 = value
            End Set
        End Property


        <ColumnInfo("SubCategory2", "'{0}'")> _
        Public Property SubCategory2 As String
            Get
                Return _subCategory2
            End Get
            Set(ByVal value As String)
                _subCategory2 = value
            End Set
        End Property


        <ColumnInfo("SubCategory3", "'{0}'")> _
        Public Property SubCategory3 As String
            Get
                Return _subCategory3
            End Get
            Set(ByVal value As String)
                _subCategory3 = value
            End Set
        End Property


        <ColumnInfo("SubCategory4", "'{0}'")> _
        Public Property SubCategory4 As String
            Get
                Return _subCategory4
            End Get
            Set(ByVal value As String)
                _subCategory4 = value
            End Set
        End Property


        <ColumnInfo("CallerType", "'{0}'")> _
        Public Property CallerType As String
            Get
                Return _callerType
            End Get
            Set(ByVal value As String)
                _callerType = value
            End Set
        End Property


        <ColumnInfo("CarType", "'{0}'")> _
        Public Property CarType As String
            Get
                Return _carType
            End Get
            Set(ByVal value As String)
                _carType = value
            End Set
        End Property


        <ColumnInfo("Variant", "'{0}'")> _
        Public Property Variants As String
            Get
                Return _variants
            End Get
            Set(ByVal value As String)
                _variants = value
            End Set
        End Property


        <ColumnInfo("EngineNumber", "'{0}'")> _
        Public Property EngineNumber As String
            Get
                Return _engineNumber
            End Get
            Set(ByVal value As String)
                _engineNumber = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        <ColumnInfo("Odometer", "{0}")> _
        Public Property Odometer As Integer
            Get
                Return _odometer
            End Get
            Set(ByVal value As Integer)
                _odometer = value
            End Set
        End Property


        <ColumnInfo("PlateNumber", "'{0}'")> _
        Public Property PlateNumber As String
            Get
                Return _plateNumber
            End Get
            Set(ByVal value As String)
                _plateNumber = value
            End Set
        End Property


        <ColumnInfo("Priority", "{0}")> _
        Public Property Priority As Short
            Get
                Return _priority
            End Get
            Set(ByVal value As Short)
                _priority = value
            End Set
        End Property


        <ColumnInfo("CaseNumberReff", "'{0}'")> _
        Public Property CaseNumberReff As String
            Get
                Return _caseNumberReff
            End Get
            Set(ByVal value As String)
                _caseNumberReff = value
            End Set
        End Property


        <ColumnInfo("CaseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property CaseDate As DateTime
            Get
                Return _caseDate
            End Get
            Set(ByVal value As DateTime)
                _caseDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("Subject", "'{0}'")> _
        Public Property Subject As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property

        <ColumnInfo("StatusDesc", "'{0}'")> _
        Public Property StatusDesc As String
            Get
                Return _statusdesc
            End Get
            Set(ByVal value As String)
                _statusdesc = value
            End Set
        End Property

        <ColumnInfo("ReservationNumber", "'{0}'")> _
        Public Property ReservationNumber As String
            Get
                Return _reservationNumber
            End Get
            Set(ByVal value As String)
                _reservationNumber = value
            End Set
        End Property


        <ColumnInfo("ServiceType", "'{0}'")> _
        Public Property ServiceType As String
            Get
                Return _serviceType
            End Get
            Set(ByVal value As String)
                _serviceType = value
            End Set
        End Property


        <ColumnInfo("BookingDatetime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property BookingDatetime As DateTime
            Get
                Return _bookingDatetime
            End Get
            Set(ByVal value As DateTime)
                _bookingDatetime = New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second)
            End Set
        End Property



        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTIme", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTIme As DateTime
            Get
                Return _lastUpdateTIme
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTIme = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "CustomerCase", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("CustomerCase", "ID", "CustomerCaseResponse", "CustomerCaseID")> _
        Public ReadOnly Property CustomerCaseResponses As System.Collections.ArrayList
            Get
                Try
                    If (Me._customerCaseResponses.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CustomerCaseResponse), "CustomerCase", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CustomerCaseResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._customerCaseResponses = DoLoadArray(GetType(CustomerCaseResponse).ToString, criterias)
                    End If

                    Return Me._customerCaseResponses

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property



        Public Property PriorityValue As String
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
        Private _dealerCode As String

        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property
#End Region

    End Class
End Namespace

