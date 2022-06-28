#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PartIncidentalHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/16/2006 - 2:40:15 PM
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
    <Serializable(), TableInfo("PartIncidentalHeader")> _
    Public Class PartIncidentalHeader
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
        Private _requestNumber As String = String.Empty
        Private _phone As String = String.Empty
        Private _policeNumber As String = String.Empty
        Private _workOrder As String = String.Empty
        Private _status As String = String.Empty
        Private _pIC As String = String.Empty
        Private _emailStatus As Integer
        Private _kTBRemark As String = String.Empty
        Private _kTBStatus As Integer
        Private _dealerMailNumber As String = String.Empty
        Private _incidentalDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _chassisNumber As String = String.Empty
        Private _vehicleType As String = String.Empty
        Private _assemblyYear As String = String.Empty

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        Private _partIncidentalDetails As System.Collections.ArrayList = New System.Collections.ArrayList


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("RequestNumber", "'{0}'")> _
        Public Property RequestNumber() As String
            Get
                Return _requestNumber
            End Get
            Set(ByVal value As String)
                _requestNumber = value
            End Set
        End Property


        <ColumnInfo("Phone", "'{0}'")> _
        Public Property Phone() As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property


        <ColumnInfo("PoliceNumber", "'{0}'")> _
        Public Property PoliceNumber() As String
            Get
                Return _policeNumber
            End Get
            Set(ByVal value As String)
                _policeNumber = value
            End Set
        End Property


        <ColumnInfo("WorkOrder", "'{0}'")> _
        Public Property WorkOrder() As String
            Get
                Return _workOrder
            End Get
            Set(ByVal value As String)
                _workOrder = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("PIC", "'{0}'")> _
        Public Property PIC() As String
            Get
                Return _pIC
            End Get
            Set(ByVal value As String)
                _pIC = value
            End Set
        End Property


        <ColumnInfo("EmailStatus", "{0}")> _
        Public Property EmailStatus() As Integer
            Get
                Return _emailStatus
            End Get
            Set(ByVal value As Integer)
                _emailStatus = value
            End Set
        End Property


        <ColumnInfo("KTBRemark", "'{0}'")> _
        Public Property KTBRemark() As String
            Get
                Return _kTBRemark
            End Get
            Set(ByVal value As String)
                _kTBRemark = value
            End Set
        End Property


        <ColumnInfo("KTBStatus", "{0}")> _
        Public Property KTBStatus() As Integer
            Get
                Return _kTBStatus
            End Get
            Set(ByVal value As Integer)
                _kTBStatus = value
            End Set
        End Property


        <ColumnInfo("DealerMailNumber", "'{0}'")> _
        Public Property DealerMailNumber() As String
            Get
                Return _dealerMailNumber
            End Get
            Set(ByVal value As String)
                _dealerMailNumber = value
            End Set
        End Property


        <ColumnInfo("IncidentalDate", "'{0:yyyy/MM/dd}'")> _
        Public Property IncidentalDate() As DateTime
            Get
                Return _incidentalDate
            End Get
            Set(ByVal value As DateTime)
                _incidentalDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ChassisNumber", "{0}")> _
        Public Property ChassisNumber() As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("VehicleType", "{0}")> _
        Public Property VehicleType() As String
            Get
                Return _vehicleType
            End Get
            Set(ByVal value As String)
                _vehicleType = value
            End Set
        End Property

        <ColumnInfo("AssemblyYear", "{0}")> _
        Public Property AssemblyYear() As String
            Get
                Return _assemblyYear
            End Get
            Set(ByVal value As String)
                _assemblyYear = value
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
        RelationInfo("Dealer", "ID", "PartIncidentalHeader", "DealerID")> _
        Public Property Dealer() As Dealer
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


        <RelationInfo("PartIncidentalHeader", "ID", "PartIncidentalDetail", "PartIncidentalHeaderID")> _
        Public ReadOnly Property PartIncidentalDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._partIncidentalDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PartIncidentalDetail), "PartIncidentalHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._partIncidentalDetails = DoLoadArray(GetType(PartIncidentalDetail).ToString, criterias)
                    End If

                    Return Me._partIncidentalDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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

    End Class
End Namespace

