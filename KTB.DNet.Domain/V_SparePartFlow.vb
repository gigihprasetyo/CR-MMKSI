
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SparePartFlow Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/7/2016 - 4:05:07 PM
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
Imports KTB.DNet.DataMapper.Framework

#End Region
Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("V_SparePartFlow")> _
    Public Class V_SparePartFlow
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal POID As Integer)
            _pOID = POID
        End Sub

#End Region

#Region "Private Variables"
        Private _row As Integer
        Private _pOID As Integer
        Private _pONumber As String = String.Empty
        Private _pODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pOSendDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sOID As Integer
        Private _sONumber As String = String.Empty
        Private _soDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dOID As Integer
        Private _dONumber As String = String.Empty
        Private _doDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _billingID As Integer
        Private _billingNumber As String = String.Empty
        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerID As Short
        Private _dealerCode As String = String.Empty
        Private _termOfPaymentID As Short
        Private _TOPDescription As String = String.Empty
        Private _TOPCeilingStatus As String = String.Empty
        Private _orderType As String = String.Empty
        Private _documentType As String = String.Empty
        Private _status As Short



#End Region

#Region "Public Properties"

        <ColumnInfo("Row", "{0}")> _
        Public Property Row As Integer
            Get
                Return _row
            End Get
            Set(ByVal value As Integer)
                _row = value
            End Set
        End Property

        <ColumnInfo("POID", "{0}")> _
        Public Property POID As Integer
            Get
                Return _pOID
            End Get
            Set(ByVal value As Integer)
                _pOID = value
            End Set
        End Property


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property


        <ColumnInfo("PODate", "'{0:yyyy/MM/dd}'")> _
        Public Property PODate As DateTime
            Get
                Return _pODate
            End Get
            Set(ByVal value As DateTime)
                _pODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("POSendDate", "'{0:yyyy/MM/dd}'")> _
        Public Property POSendDate As DateTime
            Get
                Return _pOSendDate
            End Get
            Set(ByVal value As DateTime)
                _pOSendDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SOID", "{0}")> _
        Public Property SOID As Integer
            Get
                Return _sOID
            End Get
            Set(ByVal value As Integer)
                _sOID = value
            End Set
        End Property


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property


        <ColumnInfo("SoDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SoDate As DateTime
            Get
                Return _soDate
            End Get
            Set(ByVal value As DateTime)
                _soDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DOID", "{0}")> _
        Public Property DOID As Integer
            Get
                Return _dOID
            End Get
            Set(ByVal value As Integer)
                _dOID = value
            End Set
        End Property


        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property


        <ColumnInfo("DoDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DoDate As DateTime
            Get
                Return _doDate
            End Get
            Set(ByVal value As DateTime)
                _doDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("BillingID", "{0}")> _
        Public Property BillingID As Integer
            Get
                Return _billingID
            End Get
            Set(ByVal value As Integer)
                _billingID = value
            End Set
        End Property


        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property


        <ColumnInfo("BillingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BillingDate As DateTime
            Get
                Return _billingDate
            End Get
            Set(ByVal value As DateTime)
                _billingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("TermOfPaymentID", "{0}")> _
        Public Property TermOfPaymentID As Short
            Get
                Return _termOfPaymentID
            End Get
            Set(ByVal value As Short)
                _termOfPaymentID = value
            End Set
        End Property


        <ColumnInfo("TOPDescription", "'{0}'")> _
        Public Property TOPDescription As String
            Get
                Return _TOPDescription
            End Get
            Set(ByVal value As String)
                _TOPDescription = value
            End Set
        End Property

        <ColumnInfo("TOPCeilingStatus", "'{0}'")> _
        Public Property TOPCeilingStatus As String
            Get
                Return _TOPCeilingStatus
            End Get
            Set(ByVal value As String)
                _TOPCeilingStatus = value
            End Set
        End Property

        <ColumnInfo("OrderType", "'{0}'")> _
        Public Property OrderType As String
            Get
                Return _orderType
            End Get
            Set(ByVal value As String)
                _orderType = value
            End Set
        End Property


        <ColumnInfo("DocumentType", "'{0}'")> _
        Public Property DocumentType As String
            Get
                Return _documentType
            End Get
            Set(ByVal value As String)
                _documentType = value
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

#Region "Custom Property"

        'Public ReadOnly Property GetExpedition() As ArrayList

        '    Get
        '        Dim arlReturn As ArrayList = New ArrayList
        '        Dim objSparePartDOExpedition As SparePartDOExpedition = New SparePartDOExpedition
        '        Try
        '            Dim objDO As SparePartDO = GetSparePartDO
        '            Dim objSparePartPacking As SparePartPacking = New SparePartPacking
        '            Dim objSparePartPackingDetail As SparePartPackingDetail = New SparePartPackingDetail

        '            Dim m_SparePartDOMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.SparePartPackingDetail).ToString)
        '            Dim strsql As String = ""
        '            strsql += "select expd.ID "
        '            strsql += "from SparePartDOExpedition expd "
        '            strsql += "left join SparePartPacking p on p.SparePartDOExpeditionID = expd.id "
        '            strsql += "left join SparePartPackingDetail det on det.SparePartPackingID = p.ID "
        '            strsql += "where det.SparePartDOID = " & objDO.ID
        '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPackingDetail), "ID", MatchType.InSet, "(" & strsql & ")"))
        '            Dim arlColl As ArrayList = m_SparePartDOMapper.RetrieveByCriteria(criterias)
        '            If (arlColl.Count > 0) Then
        '                For i As Integer = 0 To arlColl.Count - 1
        '                    Dim objPD As SparePartPackingDetail = CType(arlColl(i), SparePartPackingDetail)
        '                    If Not IsNothing(objPD.SparePartPacking.SparePartDOExpedition) Then
        '                        arlReturn.Add(objPD.SparePartPacking.SparePartDOExpedition)
        '                    End If
        '                Next
        '            End If
        '        Catch ex As Exception
        '            Return Nothing
        '        End Try
        '        Return arlReturn
        '    End Get
        'End Property

        'Public ReadOnly Property GetSparePartDO() As SparePartDO
        '    Get
        '        Dim obj As SparePartDO = New SparePartDO
        '        Try
        '            Dim m_SparePartDOMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.SparePartDO).ToString)
        '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDO), "ID", Me.DOID))
        '            Dim DOColl As ArrayList = m_SparePartDOMapper.RetrieveByCriteria(criterias)
        '            If (DOColl.Count > 0) Then
        '                obj = CType(DOColl(0), SparePartDO)
        '            End If
        '        Catch ex As Exception
        '            Return Nothing
        '        End Try
        '        Return obj
        '    End Get
        'End Property

        'Public ReadOnly Property PackingStatusID() As Integer
        '    Get
        '        Dim status As Integer

        '        If Me.PONumber <> String.Empty Then
        '            status = EnumSparePartStatus.SparePartStatus.PO
        '            If Me.SONumber <> String.Empty Then
        '                status = EnumSparePartStatus.SparePartStatus.Allocation
        '                If Me.DONumber <> String.Empty Then
        '                    Dim objDO As SparePartDO = GetSparePartDO
        '                    If Not IsNothing(objDO) AndAlso objDO.ID > 0 Then
        '                        If objDO.PickingDate.Year > 1900 Then
        '                            status = EnumSparePartStatus.SparePartStatus.Picking
        '                            If objDO.PackingDate.Year > 1900 Then
        '                                status = EnumSparePartStatus.SparePartStatus.Packing
        '                                If objDO.GoodIssueDate.Year > 1900 Then
        '                                    status = EnumSparePartStatus.SparePartStatus.Good_Issue
        '                                    If objDO.ReadyForDeliveryDate.Year > 1900 Then
        '                                        status = EnumSparePartStatus.SparePartStatus.Ready_For_Delivery
        '                                        Dim arlDOExpedition As ArrayList = GetExpedition()
        '                                        If arlDOExpedition.Count > 0 Then
        '                                            Dim iDeliver As Integer = 0
        '                                            Dim iReceived As Integer = 0
        '                                            For Each item As SparePartDOExpedition In arlDOExpedition
        '                                                iDeliver = iDeliver + 1
        '                                                If item.ATA.Date.Year > 1900 Then
        '                                                    iReceived = iReceived + 1
        '                                                End If
        '                                            Next
        '                                            If iDeliver > 0 Then
        '                                                status = EnumSparePartStatus.SparePartStatus.Delivery
        '                                                If iReceived > 0 Then
        '                                                    status = EnumSparePartStatus.SparePartStatus.Received_Partial
        '                                                End If
        '                                                If iReceived = arlDOExpedition.Count Then
        '                                                    status = EnumSparePartStatus.SparePartStatus.Received_Complete
        '                                                End If
        '                                            End If
        '                                        End If
        '                                    End If
        '                                End If
        '                            End If
        '                        End If

        '                    End If
        '                End If
        '            End If
        '        End If

        '        Return status

        '    End Get
        'End Property

        'Public ReadOnly Property PackingStatusDesc() As String
        '    Get
        '        Dim status As String = ""

        '        If Me.PONumber <> String.Empty Then
        '            status = EnumSparePartStatus.RetrieveName(EnumSparePartStatus.SparePartStatus.PO)
        '            If Me.SONumber <> String.Empty Then
        '                status = EnumSparePartStatus.RetrieveName(EnumSparePartStatus.SparePartStatus.Allocation)
        '                If Me.DONumber <> String.Empty Then
        '                    Dim objDO As SparePartDO = GetSparePartDO
        '                    If Not IsNothing(objDO) AndAlso objDO.ID > 0 Then
        '                        If objDO.PickingDate.Year > 1900 Then
        '                            status = EnumSparePartStatus.RetrieveName(EnumSparePartStatus.SparePartStatus.Picking)
        '                            If objDO.PackingDate.Year > 1900 Then
        '                                status = EnumSparePartStatus.RetrieveName(EnumSparePartStatus.SparePartStatus.Packing)
        '                                If objDO.GoodIssueDate.Year > 1900 Then
        '                                    status = EnumSparePartStatus.RetrieveName(EnumSparePartStatus.SparePartStatus.Good_Issue)
        '                                    If objDO.ReadyForDeliveryDate.Year > 1900 Then
        '                                        status = EnumSparePartStatus.RetrieveName(EnumSparePartStatus.SparePartStatus.Ready_For_Delivery)
        '                                        'If Me.PONumber <> String.Empty Then
        '                                        '    status = EnumSparePartStatus.RetrieveName(EnumSparePartStatus.SparePartStatus.Delivery)
        '                                        '    'If objDO..Year > 1900 Then
        '                                        '    '    status = EnumSparePartStatus.RetrieveName(EnumSparePartStatus.SparePartStatus.Received_By_Dealer)
        '                                        '    'End If
        '                                        'End If
        '                                    End If
        '                                End If
        '                            End If
        '                        End If

        '                    End If
        '                End If
        '            End If
        '        End If

        '        Return status

        '    End Get
        'End Property

#End Region

    End Class
End Namespace

