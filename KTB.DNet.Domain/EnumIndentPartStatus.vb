Namespace KTB.DNet.Domain
    Public Class EnumIndentPartStatus

        Public Shared Function IndentPartStatusDesc(ByVal status As Object) As String
            If (status = IndentPartStatus.BARU) Then
                Return IndentPartStatus.BARU.ToString()
            ElseIf (status = IndentPartStatus.BATAL) Then
                Return IndentPartStatus.BATAL.ToString()
            ElseIf (status = IndentPartStatus.SENT) Then
                Return IndentPartStatus.SENT.ToString()
            ElseIf (status = IndentPartStatus.RILIS) Then
                Return IndentPartStatus.RILIS.ToString()
            ElseIf (status = IndentPartStatus.ORDER_TO_VENDOR) Then
                Return IndentPartStatus.ORDER_TO_VENDOR.ToString()
            ElseIf (status = IndentPartStatus.TOLAK) Then
                Return IndentPartStatus.TOLAK.ToString()
            ElseIf (status = IndentPartStatus.BATAL_ORDER) Then
                Return IndentPartStatus.BATAL_ORDER.ToString()
            ElseIf (status = IndentPartStatus.SELESAI) Then
                Return IndentPartStatus.SELESAI.ToString()
            ElseIf (status = IndentPartStatus.ALOKASI_SEBAGIAN) Then
                Return IndentPartStatus.ALOKASI_SEBAGIAN.ToString()
            ElseIf (status = IndentPartStatus.BATAL_KTB) Then
                Return IndentPartStatus.BATAL_KTB.ToString()
            Else
                Return ""
            End If
        End Function

        Public Enum IndentPartStatus
            BARU = 0
            BATAL = 1
            SENT = 2
            RILIS = 3
            ORDER_TO_VENDOR = 4
            TOLAK = 5
            BATAL_ORDER = 6
            SELESAI = 7
            ALOKASI_SEBAGIAN = 8
            BATAL_KTB = 9
        End Enum

        Public Const BARU As String = "BARU"
        Public Const BATAL As String = "BATAL"
        Public Const SENT As String = "SENT"
        Public Const RILIS As String = "RILIS"
        Public Const ORDER_TO_VENDOR As String = "ORDER_TO_VENDOR"
        Public Const TOLAK As String = "TOLAK"
        Public Const BATAL_ORDER As String = "BATAL_ORDER"
        Public Const SELESAI As String = "SELESAI"
        Public Const ALOKASI_SEBAGIAN As String = "ALOKASI_SEBAGIAN"
        Public Const BATAL_KTB As String = "BATAL_KTB"
        Public Const PROSSES As String = "PROSES"

        Public Enum IndentPartStatusDealer
            'Status Untuk Dealer
            Baru = 0
            Batal = 1
            Kirim = 2
            Proses = 8
            KTB_Konfirmasi = 3
            Batal_Order = 9
            Dealer_Konfirmasi = 4
            'Rilis = 5
            Tolak = 6
            Selesai = 7
        End Enum

        Public Enum IndentPartStatusKTB
            'Status Untuk KTB
            BelumValidasi = 0
            Baru = 1
            KTB_Konfirmasi = 2
            Dealer_Konfirmasi = 3
            Rilis = 4
            Tolak = 5
            Selesai = 6
            Proses = 7
            Batal_Order = 8
            Order_To_Vendor = 10
        End Enum

        Public Enum IndentPartPaymentType
            Silakan_Pilih
            Deposit_B
            Deposit_C
        End Enum

        Public Shared Function GetIndentPartStatusKTBString(ByVal Status As Integer) As String
            'objIPH.StatusKTB
            Dim Rsl As String = ""

            If Status = EnumIndentPartStatus.IndentPartStatusKTB.Baru Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.Baru.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.BelumValidasi Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.Baru.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.Order_To_Vendor Then Rsl = "Proses_Order" 'EnumIndentPartStatus.IndentPartStatusKTB.Order_To_Vendor.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.Proses Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.Proses.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.Rilis Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.Rilis.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.Selesai Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.Selesai.ToString
            If Status = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then Rsl = EnumIndentPartStatus.IndentPartStatusKTB.Tolak.ToString
            Return Rsl
        End Function

        Public Shared Function RetriveIndentPartHeaderPaymentType(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EmPaymentType As EnumIPStatus
            'If (isIncludeBlank) Then
            '    EmPaymentType = New EnumIPStatus(99, "")
            '    arr.Add(EmPaymentType)
            'End If

            EmPaymentType = New EnumIPStatus(IndentPartPaymentType.Silakan_Pilih, "")
            arr.Add(EmPaymentType)

            EmPaymentType = New EnumIPStatus(IndentPartPaymentType.Deposit_B, IndentPartPaymentType.Deposit_B.ToString.Replace("_", " "))
            arr.Add(EmPaymentType)

            EmPaymentType = New EnumIPStatus(IndentPartPaymentType.Deposit_C, IndentPartPaymentType.Deposit_C.ToString)
            arr.Add(EmPaymentType)
            Return arr
        End Function

        Public Function RetrieveTypeForDealer() As ArrayList

            Dim al As New ArrayList
            Dim IPStatus As EnumIPStatus
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Baru, IndentPartStatusDealer.Baru.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Batal, IndentPartStatusDealer.Batal.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Kirim, IndentPartStatusDealer.Kirim.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Proses, IndentPartStatusDealer.Proses.ToString)
            al.Add(IPStatus)
            'Remove Tools
            'IPStatus = New EnumIPStatus(IndentPartStatusDealer.KTB_Konfirmasi, IndentPartStatusDealer.KTB_Konfirmasi.ToString)
            'al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Batal_Order, IndentPartStatusDealer.Batal_Order.ToString)
            al.Add(IPStatus)
            'Remove Tools
            'IPStatus = New EnumIPStatus(IndentPartStatusDealer.Dealer_Konfirmasi, IndentPartStatusDealer.Dealer_Konfirmasi.ToString)
            'al.Add(IPStatus)

            'IPStatus = New EnumIPStatus(IndentPartStatusDealer.Rilis, IndentPartStatusDealer.Rilis.ToString)
            'al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Tolak, IndentPartStatusDealer.Tolak.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Selesai, IndentPartStatusDealer.Selesai.ToString)
            al.Add(IPStatus)
            Return al
        End Function

        Public Function RetrieveTypeUpdateForDealer() As ArrayList
            Dim al As New ArrayList
            Dim IPStatus As EnumIPStatus
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Baru, IndentPartStatusDealer.Baru.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Batal, IndentPartStatusDealer.Batal.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Kirim, IndentPartStatusDealer.Kirim.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusDealer.Batal_Order, IndentPartStatusDealer.Batal_Order.ToString)
            al.Add(IPStatus)

            'Remove Tools
            'IPStatus = New EnumIPStatus(IndentPartStatusDealer.Dealer_Konfirmasi, IndentPartStatusDealer.Dealer_Konfirmasi.ToString)
            'al.Add(IPStatus)
            Return al
        End Function

        Public Function RetrieveTypeUpdateForKTB() As ArrayList
            Dim al As New ArrayList
            Dim IPStatus As EnumIPStatus
            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Baru, IndentPartStatusKTB.Baru.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Proses, IndentPartStatusKTB.Proses.ToString)
            al.Add(IPStatus)

            'Remove Tools
            'IPStatus = New EnumIPStatus(IndentPartStatusKTB.KTB_Konfirmasi, IndentPartStatusKTB.KTB_Konfirmasi.ToString)
            'al.Add(IPStatus)

            'Remove Tools
            'IPStatus = New EnumIPStatus(IndentPartStatusKTB.Rilis, IndentPartStatusKTB.Rilis.ToString)
            'al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Tolak, IndentPartStatusKTB.Tolak.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Selesai, IndentPartStatusKTB.Selesai.ToString)
            al.Add(IPStatus)
            Return al
        End Function

        Public Function RetrieveTypeForKTB() As ArrayList
            Dim al As New ArrayList
            Dim IPStatus As EnumIPStatus
            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Baru, IndentPartStatusKTB.Baru.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Proses, IndentPartStatusKTB.Proses.ToString)
            al.Add(IPStatus)

            'Remove Tools
            'IPStatus = New EnumIPStatus(IndentPartStatusKTB.KTB_Konfirmasi, IndentPartStatusKTB.KTB_Konfirmasi.ToString)
            'al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Batal_Order, IndentPartStatusKTB.Batal_Order.ToString)
            al.Add(IPStatus)

            'Remove Tools
            'IPStatus = New EnumIPStatus(IndentPartStatusKTB.Dealer_Konfirmasi, IndentPartStatusKTB.Dealer_Konfirmasi.ToString)
            'al.Add(IPStatus)

            'Remove Tools
            'IPStatus = New EnumIPStatus(IndentPartStatusKTB.Rilis, IndentPartStatusKTB.Rilis.ToString)
            'al.Add(IPStatus)

            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Tolak, IndentPartStatusKTB.Tolak.ToString)
            al.Add(IPStatus)
            IPStatus = New EnumIPStatus(IndentPartStatusKTB.Selesai, IndentPartStatusKTB.Selesai.ToString)
            al.Add(IPStatus)
            Return al
        End Function

        Public Shared Sub RetrieveStatusForDealer(ByVal listbox As System.Web.UI.WebControls.ListBox)
            Dim arl As ArrayList = New ArrayList
            arl.Add(New EnumIPStatus(IndentPartStatus.BARU, "Baru"))
            arl.Add(New EnumIPStatus(IndentPartStatus.SENT, "Kirim"))
            arl.Add(New EnumIPStatus(IndentPartStatus.RILIS, "Process"))
            arl.Add(New EnumIPStatus(IndentPartStatus.SELESAI, "Selesai"))
            arl.Add(New EnumIPStatus(IndentPartStatus.BATAL, "Batal"))
            arl.Add(New EnumIPStatus(IndentPartStatus.BATAL_ORDER, "Batal Order"))
            listbox.DataTextField = "NameType"
            listbox.DataValueField = "ValType"
            listbox.DataSource = arl
            listbox.DataBind()
        End Sub

        Public Shared Sub RetrieveStatusUpdateForDealer(ByVal ddl As System.Web.UI.WebControls.DropDownList)
            Dim arl As ArrayList = New ArrayList
            arl.Add(New EnumIPStatus("0", "Silahkan Pilih"))
            arl.Add(New EnumIPStatus(IndentPartStatus.SENT, "Kirim"))
            arl.Add(New EnumIPStatus(IndentPartStatus.BATAL, "Batal"))
            arl.Add(New EnumIPStatus(IndentPartStatus.BATAL_ORDER, "Batal Order"))
            ddl.DataTextField = "NameType"
            ddl.DataValueField = "ValType"
            ddl.DataSource = arl
            ddl.DataBind()
        End Sub

        Public Shared Sub RetrieveStatusForKTB(ByVal listbox As System.Web.UI.WebControls.ListBox, Optional ByVal IsEquipment As Boolean = False)
            Dim arl As ArrayList = New ArrayList
            arl.Add(New EnumIPStatus(IndentPartStatus.BARU, "Baru"))
            arl.Add(New EnumIPStatus(IndentPartStatus.RILIS, "Rilis"))
            arl.Add(New EnumIPStatus(IndentPartStatus.SELESAI, "Selesai"))
            arl.Add(New EnumIPStatus(IndentPartStatus.BATAL, "Batal"))
            arl.Add(New EnumIPStatus(IndentPartStatus.BATAL_ORDER, "Batal Order"))
            arl.Add(New EnumIPStatus(IndentPartStatus.BATAL_KTB, "Batal MMKSI"))
            If IsEquipment Then
                arl.Add(New EnumIPStatus(IndentPartStatus.ORDER_TO_VENDOR, "Proses Order"))
            Else
                arl.Add(New EnumIPStatus(IndentPartStatus.ORDER_TO_VENDOR, "Order To Vendor"))
            End If
            arl.Add(New EnumIPStatus(IndentPartStatus.TOLAK, "Tolak"))
            listbox.DataTextField = "NameType"
            listbox.DataValueField = "ValType"
            listbox.DataSource = arl
            listbox.DataBind()
        End Sub

        Public Shared Sub RetrieveStatusUpdateForKTB(ByVal ddl As System.Web.UI.WebControls.DropDownList, Optional ByVal IsEquipment As Boolean = False)
            Dim arl As ArrayList = New ArrayList
            arl.Add(New EnumIPStatus("0", "Silahkan Pilih"))
            arl.Add(New EnumIPStatus(IndentPartStatus.RILIS, "Rilis"))
            If IsEquipment Then
                arl.Add(New EnumIPStatus(IndentPartStatus.ORDER_TO_VENDOR, "Proses Order"))
            Else
                arl.Add(New EnumIPStatus(IndentPartStatus.ORDER_TO_VENDOR, "Order To Vendor"))
            End If
            arl.Add(New EnumIPStatus(IndentPartStatus.TOLAK, "Tolak"))
            arl.Add(New EnumIPStatus(IndentPartStatus.BATAL_KTB, "Batal Selesai"))
            arl.Add(New EnumIPStatus(IndentPartStatus.SELESAI, "Selesai"))
            ddl.DataTextField = "NameType"
            ddl.DataValueField = "ValType"
            ddl.DataSource = arl
            ddl.DataBind()
        End Sub


    End Class

    Public Class EnumIPStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub

        Public Property ValType() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameType() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property
    End Class
End Namespace
