
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RedemptionHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 4/16/2010 - 9:31:19 AM
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
    <Serializable(), TableInfo("RedemptionHeader")> _
    Public Class RedemptionHeader
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
        Private _periodDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _estimationStock As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _vehicleColorID As Short

        Private _vechileColor As VechileColor
        Private _redemptionDetails As New ArrayList
        Private _totalRequest As Integer = 0
        Private _totalRespond As Integer = 0



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


        <ColumnInfo("PeriodDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PeriodDate() As DateTime
            Get
                Return _periodDate
            End Get
            Set(ByVal value As DateTime)
                _periodDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("EstimationStock", "{0}")> _
        Public Property EstimationStock() As Integer
            Get
                Return _estimationStock
            End Get
            Set(ByVal value As Integer)
                _estimationStock = value
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


        '<ColumnInfo("VehicleColorID", "{0}")> _
        'Public Property VehicleColorID() As Short

        '    Get
        '        Return _vehicleColorID
        '    End Get
        '    Set(ByVal value As Short)
        '        _vehicleColorID = value
        '    End Set
        'End Property


        <ColumnInfo("VehicleColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "RedemptionHeader", "VehicleColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

                        Me._vechileColor = CType(DoLoad(GetType(VechileColor).ToString(), _vechileColor.ID), VechileColor)
                        Me._vechileColor.MarkLoaded()

                    End If

                    Return Me._vechileColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vechileColor = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
                End If
            End Set
        End Property



        '<RelationInfo("RedemptionHeader", "ID", "RedemptionDetail", "RedemptionHeaderID")> _
        Public Property RedemptionDetails(Optional ByVal DealerID As Integer = 0) As System.Collections.ArrayList
            Get
                Try
                    If (Me._redemptionDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(RedemptionDetail), "RedemptionHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(RedemptionDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


                        Me._redemptionDetails = DoLoadArray(GetType(RedemptionDetail).ToString, criterias)
                    End If
                    Dim arlTemp As New ArrayList
                    For Each oRD As RedemptionDetail In _redemptionDetails
                        If DealerID > 0 Then
                            If DealerID = oRD.Dealer.ID Then
                                arlTemp.Add(oRD)
                            End If
                        Else
                            arlTemp.Add(oRD)
                        End If
                    Next
                    Return arlTemp ' Me._redemptionDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
            Set(ByVal Value As System.Collections.ArrayList)
                Me._redemptionDetails = Value
            End Set
        End Property


        Public ReadOnly Property TotalRequest(Optional ByVal DealerID As Integer = 0, Optional ByVal IsIncludeManualAlloc As Boolean = True) As Integer

            Get
                Me._totalRequest = 0
                If DealerID = 0 Then
                    For Each oRD As RedemptionDetail In Me.RedemptionDetails
                        Me._totalRequest += oRD.RequestQty
                        If Not IsIncludeManualAlloc AndAlso ord.IsManualAlloc = 1 Then
                            Me._totalRequest -= oRD.RequestQty
                        End If
                    Next
                Else
                    For Each oRD As RedemptionDetail In Me.RedemptionDetails
                        If ord.Dealer.ID = DealerID Then
                            Me._totalRequest += oRD.RequestQty
                            If Not IsIncludeManualAlloc AndAlso ord.IsManualAlloc = 1 Then
                                Me._totalRequest -= oRD.RequestQty
                            End If
                        End If
                    Next
                End If
                Return Me._totalRequest
            End Get
        End Property
        Public ReadOnly Property TotalRespond(Optional ByVal DealerID As Integer = 0) As Integer

            Get
                Me._totalRespond = 0
                If DealerID = 0 Then
                    For Each oRD As RedemptionDetail In Me.RedemptionDetails
                        Me._totalRespond += oRD.RespondQty
                    Next
                Else
                    For Each oRD As RedemptionDetail In Me.RedemptionDetails
                        If ord.Dealer.ID = DealerID Then
                            Me._totalRespond += oRD.RespondQty
                        End If
                    Next
                End If
                Return Me._totalRespond
            End Get
        End Property

        Public ReadOnly Property TotalRespondManual(Optional ByVal DealerID As Integer = 0) As Integer

            Get
                Me._totalRespond = 0
                If DealerID = 0 Then
                    For Each oRD As RedemptionDetail In Me.RedemptionDetails
                        If ord.IsManualAlloc = 1 Then Me._totalRespond += oRD.RespondQty
                    Next
                Else
                    For Each oRD As RedemptionDetail In Me.RedemptionDetails
                        If ord.Dealer.ID = DealerID Then
                            If ord.IsManualAlloc = 1 Then Me._totalRespond += oRD.RespondQty
                        End If
                    Next
                End If
                Return Me._totalRespond
            End Get
        End Property

        Public Sub SetRedemptionDetail(ByVal oRD As RedemptionDetail, ByVal Idx As Integer)
            If Idx >= 0 Then
                Me._redemptionDetails(Idx) = oRD
            Else
                Me._redemptionDetails.Add(oRD)
            End If
        End Sub

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

