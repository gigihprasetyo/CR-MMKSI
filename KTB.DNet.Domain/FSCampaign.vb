
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FSCampaign Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 9/6/2010 - 8:24:06 AM
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
    <Serializable(), TableInfo("FSCampaign")> _
    Public Class FSCampaign
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
        Private _description As String = String.Empty
        Private _errMessage As String = String.Empty
        Private _dateFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dateTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerChecked As Boolean
        Private _fSTypeChecked As Boolean
        Private _vehicleTypeChecked As Boolean
        Private _fakturDateChecked As Boolean
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _pKTDateChecked As Boolean
        Private _PKTDateFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _PKTDateTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _fSCampaignDealers As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _fSCampaignKinds As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _fSCampaignVehicles As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("ErrMessage", "'{0}'")> _
        Public Property ErrMessage() As String
            Get
                Return _errMessage
            End Get
            Set(ByVal value As String)
                _errMessage = value
            End Set
        End Property

        <ColumnInfo("PKTDateFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PKTDateFrom() As DateTime
            Get
                Return _PKTDateFrom
            End Get
            Set(ByVal value As DateTime)
                _PKTDateFrom = value
            End Set
        End Property


        <ColumnInfo("PKTDateTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PKTDateTo() As DateTime
            Get
                Return _PKTDateTo
            End Get
            Set(ByVal value As DateTime)
                _PKTDateTo = value
            End Set
        End Property

        <ColumnInfo("DateFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateFrom() As DateTime
            Get
                Return _dateFrom
            End Get
            Set(ByVal value As DateTime)
                _dateFrom = value
            End Set
        End Property


        <ColumnInfo("DateTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DateTo() As DateTime
            Get
                Return _dateTo
            End Get
            Set(ByVal value As DateTime)
                _dateTo = value
            End Set
        End Property


        <ColumnInfo("DealerChecked", "{0}")> _
        Public Property DealerChecked() As Boolean
            Get
                Return _dealerChecked
            End Get
            Set(ByVal value As Boolean)
                _dealerChecked = value
            End Set
        End Property


        <ColumnInfo("FSTypeChecked", "{0}")> _
        Public Property FSTypeChecked() As Boolean
            Get
                Return _fSTypeChecked
            End Get
            Set(ByVal value As Boolean)
                _fSTypeChecked = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeChecked", "{0}")> _
        Public Property VehicleTypeChecked() As Boolean
            Get
                Return _vehicleTypeChecked
            End Get
            Set(ByVal value As Boolean)
                _vehicleTypeChecked = value
            End Set
        End Property

        <ColumnInfo("PKTDateChecked", "{0}")> _
        Public Property PKTDateChecked() As Boolean
            Get
                Return _pKTDateChecked
            End Get
            Set(ByVal value As Boolean)
                _pKTDateChecked = value
            End Set
        End Property

        <ColumnInfo("FakturDateChecked", "{0}")> _
        Public Property FakturDateChecked() As Boolean
            Get
                Return _fakturDateChecked
            End Get
            Set(ByVal value As Boolean)
                _fakturDateChecked = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
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



        <RelationInfo("FSCampaign", "ID", "FSCampaignDealer", "CampaignID")> _
        Public ReadOnly Property FSCampaignDealers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._fSCampaignDealers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(FSCampaignDealer), "FSCampaign", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(FSCampaignDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._fSCampaignDealers = DoLoadArray(GetType(FSCampaignDealer).ToString, criterias)
                    End If

                    Return Me._fSCampaignDealers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("FSCampaign", "ID", "FSCampaignKind", "CampaignID")> _
        Public ReadOnly Property FSCampaignKinds() As System.Collections.ArrayList
            Get
                Try
                    If (Me._fSCampaignKinds.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(FSCampaignKind), "FSCampaign", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(FSCampaignKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._fSCampaignKinds = DoLoadArray(GetType(FSCampaignKind).ToString, criterias)
                    End If

                    Return Me._fSCampaignKinds

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("FSCampaign", "ID", "FSCampaignVehicle", "CampaignID")> _
        Public ReadOnly Property FSCampaignVehicles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._fSCampaignVehicles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(FSCampaignVehicle), "FSCampaign", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(FSCampaignVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._fSCampaignVehicles = DoLoadArray(GetType(FSCampaignVehicle).ToString, criterias)
                    End If

                    Return Me._fSCampaignVehicles

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

