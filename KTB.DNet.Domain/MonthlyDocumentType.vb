Namespace KTB.DNet.Domain
    Public Class MonthlyDocumentType
        Public Enum MonthlyDocumentTypeEnum
            Deposit_B_Report
            Kwitansi_ESP_Warranty
            Kwitansi_Free_Service_Campaign
            PDI_Letter
            Free_Service_Regular_Letter
            Free_Service_Campaign_Letter
            Warranty_ESP_Letter
            Warranty_ESP_Status_List
            Periodical_Maintenance_Letter
            Deposit_B_Kwitansi
            Deposit_B_Interest
            Kend_Belum_PDI_List 'PDI_Letter2
            Free_ESP_Labour_Letter
            Kwitansi_ESP_Free_Labour
            Free_Maintenance_Letter
            Kwitansi_Free_Maintenance
            Billing_Create
            Billing_Update
            Free_service_regular_status_list
            Free_service_campaign_status_list
            Free_maintenance_status_list
            Free_labor_status_list
            Kwitansi_Warranty_Spare_Part_Accessories
            Kwitansi_Free_Maintenance_and_Campaign
            Free_Maintenance_and_campaign_letter
        End Enum

        Public Shared Function RetrieveDocumentType() As ArrayList
            Dim al As New ArrayList
            Dim odr As MonthlyDocumentTypeListItem
            odr = New MonthlyDocumentTypeListItem(0, CType(0, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(1, CType(1, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)

            odr = New MonthlyDocumentTypeListItem(3, CType(3, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(4, CType(4, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)

            odr = New MonthlyDocumentTypeListItem(2, CType(2, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)

            odr = New MonthlyDocumentTypeListItem(5, CType(5, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(6, CType(6, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(7, CType(7, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(8, CType(8, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(9, CType(9, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(10, CType(10, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            'ToDo //sementara di comment nunggu patch
            odr = New MonthlyDocumentTypeListItem(11, CType(11, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(12, CType(12, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(13, CType(13, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(14, CType(14, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(15, CType(15, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(18, CType(18, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(19, CType(19, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(20, CType(20, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(21, CType(21, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(22, CType(22, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(23, CType(23, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(24, CType(24, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            Return al
        End Function

        Public Shared Function RetrieveDocumentOutstandingType() As ArrayList
            Dim al As New ArrayList
            Dim odr As MonthlyDocumentTypeListItem
            odr = New MonthlyDocumentTypeListItem(1, CType(1, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(2, CType(2, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(4, CType(4, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(5, CType(5, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(6, CType(6, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(7, CType(7, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(23, CType(23, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            odr = New MonthlyDocumentTypeListItem(24, CType(24, MonthlyDocumentTypeEnum).ToString)
            al.Add(odr)
            Return al
        End Function
    End Class


    Public Class MonthlyDocumentTypeListItem

        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub

        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameStatus() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace

