Imports System.Web.UI.WebControls
Namespace KTB.DNet.Domain

    Public Class LookUp

        Public Enum DocumentType
            Pesanan_Kendaraan
            PO_Harian
            Pembelian_Equipment
            Estimation_Equip
            Indent_Part
            SparePartPO
            Surat_Pesanan_Kendaraan
            Gyro
            MSP_Registration
            MSP_TransferPayment
            PKT


            SAPCustomer_Status
            SAPCustomer_LeadStatus
            SAPCustomer_StatusCode
            SAPCustomer_StateCode

            SalesCampaign_ClaimStatus
            TrAdditionalClass
            Tagihan_Training

            VechileColorIsActiveOnPKStatus

            CBUReturn_ClaimStatus
            CBUReturn_ReturStatus
        End Enum


        Private Shared _documentTypeArrayList As ArrayList
        Public Shared ReadOnly Property ArrayDocumentType()
            Get
                If (_documentTypeArrayList Is Nothing) Then
                    _documentTypeArrayList = New ArrayList
                    Dim listItemArrayDocumentType1 As New ListItem("Pesanan_Kendaraan", 0)
                    Dim listItemArrayDocumentType2 As New ListItem("PO_Harian", 1)
                    Dim listItemArrayDocumentType3 As New ListItem("Pembelian_Equipment", 2)
                    Dim listItemArrayDocumentType4 As New ListItem("Estimation_Equip", 3)
                    Dim listItemArrayDocumentType5 As New ListItem("Indent_Part", 4)
                    Dim listItemArrayDocumentType6 As New ListItem("SparePartPO", 5)
                    Dim listItemArrayDocumentType7 As New ListItem("Surat_Pesanan_Kendaraan", 6)
                    Dim listItemArrayDocumentType8 As New ListItem("Gyro", 7)
                    Dim listItemArrayDocumentType9 As New ListItem("MSP_Registration", 8)
                    Dim listItemArrayDocumentType10 As New ListItem("MSP_TransferPayment", 9)
                    Dim listItemArrayDocumentType11 As New ListItem("PKT", 10)
                    _documentTypeArrayList.Add(listItemArrayDocumentType1)
                    _documentTypeArrayList.Add(listItemArrayDocumentType2)
                    _documentTypeArrayList.Add(listItemArrayDocumentType3)
                    _documentTypeArrayList.Add(listItemArrayDocumentType4)
                    _documentTypeArrayList.Add(listItemArrayDocumentType5)
                    _documentTypeArrayList.Add(listItemArrayDocumentType6)
                    _documentTypeArrayList.Add(listItemArrayDocumentType7)
                    _documentTypeArrayList.Add(listItemArrayDocumentType8)
                    _documentTypeArrayList.Add(listItemArrayDocumentType9)
                    _documentTypeArrayList.Add(listItemArrayDocumentType10)
                    _documentTypeArrayList.Add(listItemArrayDocumentType11)

                    _documentTypeArrayList.Add(New ListItem("SAPCustomer_Status", 11))
                    _documentTypeArrayList.Add(New ListItem("SAPCustomer_LeadStatus", 12))
                    _documentTypeArrayList.Add(New ListItem("SAPCustomer_StatusCode", 13))
                    _documentTypeArrayList.Add(New ListItem("SAPCustomer_StateCode", 14))
                    _documentTypeArrayList.Add(New ListItem("SalesCampaign_ClaimStatus", 15))

                    _documentTypeArrayList.Add(New ListItem("TrAdditionalClass", 16))
                    _documentTypeArrayList.Add(New ListItem("Tagihan_Training", 17))

                    _documentTypeArrayList.Add(New ListItem("VechileColorIsActiveOnPKStatus", 16))
                    _documentTypeArrayList.Add(New ListItem("CBUReturn_ClaimStatus", 19))
                    _documentTypeArrayList.Add(New ListItem("CBUReturn_ReturStatus", 20))
                End If
                Return _documentTypeArrayList
            End Get
        End Property

        Private Shared _arrayListYear As ArrayList
        Public Shared ReadOnly Property ArraylistYear(ByVal Now As Boolean, ByVal Back As Integer, ByVal Forward As Integer, ByVal YearNow As String) As ArrayList
            Get
                _arrayListYear = New ArrayList
                If Forward <> 0 Then
                    For i As Integer = Forward To 1 Step -1
                        Dim item As New ListItem((CType(YearNow, Integer) + i).ToString)
                        item.Selected = False
                        _arrayListYear.Add(item)
                    Next
                End If

                If Now Then
                    Dim item As New ListItem(YearNow)
                    item.Selected = False
                    _arrayListYear.Add(item)
                End If

                If Back <> 0 Then
                    For i As Integer = 1 To Back
                        Dim item As New ListItem((CType(YearNow, Integer) - i).ToString)
                        item.Selected = False
                        _arrayListYear.Add(item)
                    Next
                End If
                Return _arrayListYear
            End Get
        End Property

        Public Shared ReadOnly Property ArrayYear(ByVal Now As Boolean, ByVal Back As Integer, ByVal Forward As Integer, ByVal YearNow As String)
            Get
                Dim _arrayListYear = New ArrayList

                If Back <> 0 Then
                    For i As Integer = Back To 1 Step -1
                        Dim item As New ListItem((CType(YearNow, Integer) - i).ToString)
                        _arrayListYear.Add(item)
                    Next
                End If
                If Now Then
                    Dim item As New ListItem(YearNow)
                    _arrayListYear.Add(item)
                End If
                If Forward <> 0 Then
                    For i As Integer = 1 To Forward
                        Dim item As New ListItem((CType(YearNow, Integer) + i).ToString)
                        _arrayListYear.Add(item)
                    Next
                End If

                Return _arrayListYear
            End Get
        End Property

        Public Shared ReadOnly Property ArrayYearWithValue(ByVal Now As Boolean, ByVal Back As Integer, ByVal Forward As Integer, ByVal YearNow As String)
            Get
                Dim _arrayListYear = New ArrayList

                If Back <> 0 Then
                    For i As Integer = Back To 1 Step -1
                        Dim item As New ListItem((CType(YearNow, Integer) - i).ToString, CType(YearNow, Integer) - i)
                        _arrayListYear.Add(item)
                    Next
                End If
                If Now Then
                    Dim item As New ListItem(YearNow, CType(YearNow, Integer))
                    _arrayListYear.Add(item)
                End If
                If Forward <> 0 Then
                    For i As Integer = 1 To Forward
                        Dim item As New ListItem((CType(YearNow, Integer) + i).ToString, CType(YearNow, Integer) + i)
                        _arrayListYear.Add(item)
                    Next
                End If

                Return _arrayListYear
            End Get
        End Property

        'Public Shared Function getName(ByVal int As Integer) As String
        '    Dim str As String = _arrayListCategory.Item(int)
        'End Function

        Private Shared _arrayListMonth As ArrayList
        Public Shared ReadOnly Property ArraylistMonth(ByVal Now As Boolean, ByVal Back As Integer, ByVal Forward As Integer, ByVal MonthNow As DateTime)
            Get
                _arrayListMonth = New ArrayList
                Dim j As Integer = 0
                If Forward <> 0 Then
                    For i As Integer = Forward To 1 Step -1
                        Dim item As New ListItem((Format(MonthNow.AddMonths(i), "MMM yyyy")).ToString)
                        _arrayListMonth.Add(item)
                        j = j + 1
                    Next
                End If

                If Now Then
                    Dim item As New ListItem((Format(MonthNow, "MMM yyyy")).ToString)
                    _arrayListMonth.Add(item)
                    j = j + 1
                End If

                If Back <> 0 Then
                    For i As Integer = 1 To Back
                        Dim item As New ListItem(Format(MonthNow.AddMonths(-i), "MMM yyyy").ToString)
                        _arrayListMonth.Add(item)
                        j = j + 1
                    Next
                End If
                Return _arrayListMonth
            End Get
        End Property

        Private Shared _arrayListMonthCalendar As ArrayList
        Public Shared ReadOnly Property ArraylistMonthCalendar()
            Get
                _arrayListMonthCalendar = New ArrayList

                'For i As Integer = 1 To 12 Step +1
                '    Dim item As New ListItem((Format(DateTime.Now.AddMonths(i), "MMMM")).ToString)
                '    _arrayListMonthCalendar.Add(item)
                'Next

                _arrayListMonthCalendar.Add(New ListItem("Januari"))
                _arrayListMonthCalendar.Add(New ListItem("Februari"))
                _arrayListMonthCalendar.Add(New ListItem("Maret"))
                _arrayListMonthCalendar.Add(New ListItem("April"))
                _arrayListMonthCalendar.Add(New ListItem("Mei"))
                _arrayListMonthCalendar.Add(New ListItem("Juni"))
                _arrayListMonthCalendar.Add(New ListItem("Juli"))
                _arrayListMonthCalendar.Add(New ListItem("Agustus"))
                _arrayListMonthCalendar.Add(New ListItem("September"))
                _arrayListMonthCalendar.Add(New ListItem("Oktober"))
                _arrayListMonthCalendar.Add(New ListItem("November"))
                _arrayListMonthCalendar.Add(New ListItem("Desember"))

                Return _arrayListMonthCalendar
            End Get
        End Property

        Public Enum EnumJenisPesanan
            Bulanan
            Tambahan
        End Enum


        Private Shared _arrayListJenisPesanan As ArrayList
        Public Shared ReadOnly Property ArrayJenisPesanan()
            Get
                If (_arrayListJenisPesanan Is Nothing) Then
                    _arrayListJenisPesanan = New ArrayList
                    Dim listItemArrayJenisPesanan1 As New ListItem("Bulanan", 0)
                    Dim listItemArrayJenisPesanan2 As New ListItem("Tambahan", 1)
                    _arrayListJenisPesanan.Add(listItemArrayJenisPesanan1)
                    _arrayListJenisPesanan.Add(listItemArrayJenisPesanan2)
                End If
                Return _arrayListJenisPesanan
            End Get
        End Property

        Public Enum EnumJenisOrder
            Harian
            Tambahan
        End Enum

        Private Shared _arrayListJenisPO As ArrayList
        Public Shared ReadOnly Property ArrayJenisPO()
            Get
                If (_arrayListJenisPO Is Nothing) Then
                    _arrayListJenisPO = New ArrayList
                    Dim listItemPO1 As New ListItem("Harian", 0)
                    Dim listItemPO2 As New ListItem("Tambahan", 1)
                    _arrayListJenisPO.Add(listItemPO1)
                    _arrayListJenisPO.Add(listItemPO2)
                End If
                Return _arrayListJenisPO
            End Get
        End Property

        Public Enum enumPurpose
            Khusus
            Biasa
            Semua
        End Enum

        Private Shared _arrayListPurpose As ArrayList
        Public Shared ReadOnly Property ArrayPurpose()
            Get
                If (_arrayListPurpose Is Nothing) Then
                    _arrayListPurpose = New ArrayList
                    Dim listItemPurpose1 As New ListItem("Khusus", 0)
                    Dim listItemPurpose2 As New ListItem("Biasa", 1)
                    _arrayListPurpose.Add(listItemPurpose1)
                    _arrayListPurpose.Add(listItemPurpose2)
                End If
                Return _arrayListPurpose
            End Get
        End Property

        Private Shared _arrayListStatusPK As ArrayList
        Public Shared ReadOnly Property ArrayStatusPK()
            Get
                If (_arrayListStatusPK Is Nothing) Then
                    _arrayListStatusPK = New ArrayList
                    Dim listItemStatus1 As New ListItem("Baru", 0)
                    Dim listItemStatus2 As New ListItem("Batal", 1)
                    Dim listItemStatus3 As New ListItem("Validasi", 2)
                    Dim listItemStatus4 As New ListItem("Konfirmasi", 3)
                    Dim listItemStatus5 As New ListItem("Rilis", 4)
                    Dim listItemStatus6 As New ListItem("Ditolak", 5)
                    Dim listItemStatus7 As New ListItem("Setuju", 6)
                    Dim listItemStatus8 As New ListItem("Tidak Setuju", 7)
                    Dim listItemStatus9 As New ListItem("DiBlok", 8)
                    Dim listItemStatus10 As New ListItem("Selesai", 9)
                    Dim listItemStatus11 As New ListItem("Tunggu Diskon", 10)

                    With _arrayListStatusPK
                        .Add(listItemStatus1)
                        .Add(listItemStatus2)
                        .Add(listItemStatus3)
                        .Add(listItemStatus4)
                        .Add(listItemStatus11)
                        .Add(listItemStatus5)
                        .Add(listItemStatus6)
                        .Add(listItemStatus7)
                        .Add(listItemStatus8)
                        .Add(listItemStatus9)
                        .Add(listItemStatus10)
                    End With

                End If
                Return _arrayListStatusPK
            End Get
        End Property

        Private Shared _arrayListStatusPO As ArrayList
        Public Shared ReadOnly Property ArrayStatusPO()
            Get
                If (_arrayListStatusPO Is Nothing) Then
                    _arrayListStatusPO = New ArrayList
                    Dim listItemStatus1 As New ListItem("Baru", 0)
                    Dim listItemStatus2 As New ListItem("Batal", 1)
                    Dim listItemStatus3 As New ListItem("Konfirmasi", 2)
                    Dim listItemStatus4 As New ListItem("Ditolak", 3)
                    Dim listItemStatus5 As New ListItem("Rilis", 4)
                    Dim listItemStatus6 As New ListItem("DiBlok", 5)
                    Dim listItemStatus7 As New ListItem("Setuju", 6)
                    Dim listItemStatus8 As New ListItem("Tidak Setuju", 7)
                    Dim listItemStatus9 As New ListItem("Selesai", 8)

                    With _arrayListStatusPO
                        .Add(listItemStatus1)
                        .Add(listItemStatus2)
                        .Add(listItemStatus3)
                        .Add(listItemStatus4)
                        .Add(listItemStatus5)
                        .Add(listItemStatus6)
                        .Add(listItemStatus7)
                        .Add(listItemStatus8)
                        .Add(listItemStatus9)
                    End With

                End If
                Return _arrayListStatusPO
            End Get
        End Property

        Private Shared _arrayListBulan As ArrayList
        Public Shared ReadOnly Property ArrayBulan()
            Get
                If (_arrayListBulan Is Nothing) Then
                    _arrayListBulan = New ArrayList
                    Dim listItemBulan1 As New ListItem("Januari", 1)
                    Dim listItemBulan2 As New ListItem("Februari", 2)
                    Dim listItemBulan3 As New ListItem("Maret", 3)
                    Dim listItemBulan4 As New ListItem("April", 4)
                    Dim listItemBulan5 As New ListItem("Mei", 5)
                    Dim listItemBulan6 As New ListItem("Juni", 6)
                    Dim listItemBulan7 As New ListItem("Juli", 7)
                    Dim listItemBulan8 As New ListItem("Agustus", 8)
                    Dim listItemBulan9 As New ListItem("September", 9)
                    Dim listItemBulan10 As New ListItem("Oktober", 10)
                    Dim listItemBulan11 As New ListItem("November", 11)
                    Dim listItemBulan12 As New ListItem("Desember", 12)

                    With _arrayListBulan
                        .Add(listItemBulan1)
                        .Add(listItemBulan2)
                        .Add(listItemBulan3)
                        .Add(listItemBulan4)
                        .Add(listItemBulan5)
                        .Add(listItemBulan6)
                        .Add(listItemBulan7)
                        .Add(listItemBulan8)
                        .Add(listItemBulan9)
                        .Add(listItemBulan10)
                        .Add(listItemBulan11)
                        .Add(listItemBulan12)
                    End With

                End If
                Return _arrayListBulan
            End Get
        End Property

        Public Shared ReadOnly Property ArrayMonth()
            Get
                Dim _arrayListBulan As New ArrayList

                Dim listItemBulan1 As New ListItem("Januari", 1)
                Dim listItemBulan2 As New ListItem("Februari", 2)
                Dim listItemBulan3 As New ListItem("Maret", 3)
                Dim listItemBulan4 As New ListItem("April", 4)
                Dim listItemBulan5 As New ListItem("Mei", 5)
                Dim listItemBulan6 As New ListItem("Juni", 6)
                Dim listItemBulan7 As New ListItem("Juli", 7)
                Dim listItemBulan8 As New ListItem("Agustus", 8)
                Dim listItemBulan9 As New ListItem("September", 9)
                Dim listItemBulan10 As New ListItem("Oktober", 10)
                Dim listItemBulan11 As New ListItem("November", 11)
                Dim listItemBulan12 As New ListItem("Desember", 12)

                With _arrayListBulan
                    .Add(listItemBulan1)
                    .Add(listItemBulan2)
                    .Add(listItemBulan3)
                    .Add(listItemBulan4)
                    .Add(listItemBulan5)
                    .Add(listItemBulan6)
                    .Add(listItemBulan7)
                    .Add(listItemBulan8)
                    .Add(listItemBulan9)
                    .Add(listItemBulan10)
                    .Add(listItemBulan11)
                    .Add(listItemBulan12)
                End With

                Return _arrayListBulan
            End Get
        End Property

        Private Shared _arrayListKondisiPesanan As ArrayList
        Public Shared ReadOnly Property ArrayKondisiPesanan()
            Get
                If (_arrayListKondisiPesanan Is Nothing) Then
                    _arrayListKondisiPesanan = New ArrayList
                    Dim listItemCategory1 As New ListItem("Biasa", 1)
                    Dim listItemCategory2 As New ListItem("Khusus", 0)
                    _arrayListKondisiPesanan.Add(listItemCategory1)
                    _arrayListKondisiPesanan.Add(listItemCategory2)
                End If
                Return _arrayListKondisiPesanan
            End Get
        End Property

        Public Enum EnumHari
            Minggu
            Senin
            Selasa
            Rabu
            Kamis
            Jumat
            Sabtu
        End Enum

        Public Enum EnumBulan
            Month
            Januari
            Februari
            Maret
            April
            Mei
            Juni
            Juli
            Agustus
            September
            Oktober
            Nopember
            Desember
        End Enum

        Private Shared _arrayListStatusDraftPO As ArrayList
        Public Shared ReadOnly Property StatusDraftPO()
            Get
                If (_arrayListStatusDraftPO Is Nothing) Then
                    _arrayListStatusDraftPO = New ArrayList
                    Dim listItemDraftPO1 As New ListItem("Baru", 0)
                    Dim listItemDraftPO2 As New ListItem("Submit PO", 1)
                    Dim listItemDraftPO3 As New ListItem("Batal", 2)
                    _arrayListStatusDraftPO.Add(listItemDraftPO1)
                    _arrayListStatusDraftPO.Add(listItemDraftPO2)
                    _arrayListStatusDraftPO.Add(listItemDraftPO3)
                End If
                Return _arrayListStatusDraftPO
            End Get
        End Property


#Region " SparePart "

        Public Shared Function GetDocumentTypeDescription(ByVal cDoc As Char) As String
            Select Case cDoc
                Case "N"
                    Return "Normal"
                Case "B"
                    Return "Back Order"
                Case Else
                    Return "-"
            End Select
        End Function

        Private Shared _arrayListSPDocumentTypeKTBDealer As ArrayList
        Public Shared ReadOnly Property ArraySPDocumentTypeKTBDealer()
            Get
                If (_arrayListSPDocumentTypeKTBDealer Is Nothing) Then
                    _arrayListSPDocumentTypeKTBDealer = New ArrayList
                    Dim listItem1 As New ListItem("Normal", "N")
                    Dim listItem2 As New ListItem("Back Order", "B")
                    _arrayListSPDocumentTypeKTBDealer.Add(listItem1)
                    _arrayListSPDocumentTypeKTBDealer.Add(listItem2)
                End If
                Return _arrayListSPDocumentTypeKTBDealer
            End Get
        End Property


        Private Shared _arrayListSPOrderTypeKTB As ArrayList
        Public Shared ReadOnly Property ArraySPOrderTypeKTB()
            Get
                If (_arrayListSPOrderTypeKTB Is Nothing) Then
                    _arrayListSPOrderTypeKTB = New ArrayList
                    Dim listItem1 As New ListItem("Others Reguler", "Z")
                    Dim listItem2 As New ListItem("Others Emergency", "Y")
                    _arrayListSPOrderTypeKTB.Add(listItem1)
                    _arrayListSPOrderTypeKTB.Add(listItem2)
                End If
                Return _arrayListSPOrderTypeKTB
            End Get
        End Property

        Private Shared _arrayListSPOrderTypeKTBDealer As ArrayList
        Public Shared ReadOnly Property ArraySPOrderTypeKTBDealer()
            Get
                If (_arrayListSPOrderTypeKTBDealer Is Nothing) Then
                    _arrayListSPOrderTypeKTBDealer = New ArrayList
                    Dim listItem1 As New ListItem("Others Reguler", "Z")
                    Dim listItem2 As New ListItem("Others Emergency", "Y")
                    Dim listItem3 As New ListItem("Emergency PQR", "P")
                    Dim listItem4 As New ListItem("Emergency Tanpa BackOrder", "X")
                    Dim listItem5 As New ListItem("Emergency", "E")
                    Dim listItem6 As New ListItem("Regular", "R")
                    Dim listItem7 As New ListItem("Khusus", "K")
                    Dim listItem8 As New ListItem("Indent", "I")
                    _arrayListSPOrderTypeKTBDealer.Add(listItem1)
                    _arrayListSPOrderTypeKTBDealer.Add(listItem2)
                    _arrayListSPOrderTypeKTBDealer.Add(listItem3)
                    _arrayListSPOrderTypeKTBDealer.Add(listItem4)
                    _arrayListSPOrderTypeKTBDealer.Add(listItem5)
                    _arrayListSPOrderTypeKTBDealer.Add(listItem6)
                    _arrayListSPOrderTypeKTBDealer.Add(listItem7)
                    _arrayListSPOrderTypeKTBDealer.Add(listItem8)
                End If
                Return _arrayListSPOrderTypeKTBDealer
            End Get
        End Property

        Private Shared _arrayListSPOrderType As ArrayList
        Public Shared ReadOnly Property ArraySPOrderType()
            Get
                If (_arrayListSPOrderType Is Nothing) Then
                    _arrayListSPOrderType = New ArrayList
                    Dim listItem1 As New ListItem("Emergency PQR", "P")
                    Dim listItem2 As New ListItem("Emergency Tanpa BackOrder", "X")
                    Dim listItem3 As New ListItem("Emergency", "E")
                    Dim listItem4 As New ListItem("Regular", "R")
                    Dim listItem5 As New ListItem("Khusus", "K")
                    Dim listItem6 As New ListItem("Indent", "I")
                    _arrayListSPOrderType.Add(listItem1)
                    _arrayListSPOrderType.Add(listItem2)
                    _arrayListSPOrderType.Add(listItem3)
                    _arrayListSPOrderType.Add(listItem4)
                End If
                Return _arrayListSPOrderType
            End Get
        End Property

        Private Shared _arrayListSPPackingStatus As ArrayList
        Public Shared ReadOnly Property ArraySPPackingStatus()
            Get
                If (_arrayListSPPackingStatus Is Nothing) Then
                    _arrayListSPPackingStatus = New ArrayList
                    Dim listItem1 As New ListItem("Sedang Diproses", "P")
                    Dim listItem2 As New ListItem("Selesai", "C")
                    _arrayListSPPackingStatus.Add(listItem1)
                    _arrayListSPPackingStatus.Add(listItem2)
                End If
                Return _arrayListSPPackingStatus
            End Get
        End Property

        Private Shared _arrayListSPPOProccessCode As ArrayList
        Public Shared ReadOnly Property ArraySPPOProccessCode()
            Get
                If (_arrayListSPPOProccessCode Is Nothing) Then
                    _arrayListSPPOProccessCode = New ArrayList
                    Dim listItem0 As New ListItem("Baru", "")
                    Dim listItem1 As New ListItem("Batal", "C")
                    Dim listItem2 As New ListItem("Telah Dikirim", "S")
                    Dim listItem3 As New ListItem("Telah Diproses", "P")
                    Dim listItem4 As New ListItem("Batal MMKSI", "X")
                    Dim listItem5 As New ListItem("Tidak Dipenuhi", "T")
                    _arrayListSPPOProccessCode.Add(listItem0)
                    _arrayListSPPOProccessCode.Add(listItem1)
                    _arrayListSPPOProccessCode.Add(listItem2)
                    _arrayListSPPOProccessCode.Add(listItem3)
                    _arrayListSPPOProccessCode.Add(listItem4)
                    _arrayListSPPOProccessCode.Add(listItem5)

                End If
                Return _arrayListSPPOProccessCode
            End Get
        End Property

        Public Enum enumSPStatus
            Baru = 0
            Batal
            Telah_Dikirim
            Telah_Diproses
            Batal_KTB
            Tidak_Dipenuhi
        End Enum

        Public Shared Function RetriveSPStatus()
            Dim _arrayListSPPO As New ArrayList
            Dim sts As enumStatStr

            sts = New enumStatStr("", "Baru")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr("C", "Batal")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr("S", "Telah Dikirim")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr("P", "Telah Diproses")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr("X", "Batal MMKSI")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr("T", "Tidak Dipenuhi")
            _arrayListSPPO.Add(sts)

            Return _arrayListSPPO
        End Function
        Public Shared Function RetriveSPStatusAlert()
            Dim _arrayListSPPO As New ArrayList
            Dim sts As enumStatStr

            sts = New enumStatStr(0, "Baru")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr(1, "Batal")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr(2, "Telah Dikirim")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr(3, "Telah Diproses")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr(4, "Batal MMKSI")
            _arrayListSPPO.Add(sts)
            sts = New enumStatStr(5, "Tidak Dipenuhi")
            _arrayListSPPO.Add(sts)

            Return _arrayListSPPO
        End Function

#End Region

#Region "Service"
        Private Shared _arrayListPosCategory As ArrayList
        Public Shared ReadOnly Property ArrayPosCategory()
            Get
                If (_arrayListPosCategory Is Nothing) Then
                    _arrayListPosCategory = New ArrayList

                    Dim Pos As EnumPositionCategoryWSC = New EnumPositionCategoryWSC
                    Dim listItem1 As New ListItem("A", Pos.PosCategory(Pos.PositionCategory.A))
                    Dim listItem2 As New ListItem("B", Pos.PosCategory(Pos.PositionCategory.B))
                    Dim listItem3 As New ListItem("C", Pos.PosCategory(Pos.PositionCategory.C))
                    _arrayListPosCategory.Add(listItem1)
                    _arrayListPosCategory.Add(listItem2)
                    _arrayListPosCategory.Add(listItem3)
                End If
                Return _arrayListPosCategory
            End Get
        End Property

        Private Shared _arrayListClaimStatus As ArrayList
        Public Shared ReadOnly Property ArrayClaimStatus()
            Get
                If IsNothing(_arrayListClaimStatus) Then
                    _arrayListClaimStatus = New ArrayList

                    Dim listItem1 As New ListItem("Baru", "0")
                    Dim listItem2 As New ListItem("Proses", "1")
                    Dim listItem3 As New ListItem("Selesai", "2")

                    _arrayListClaimStatus.Add(listItem1)
                    _arrayListClaimStatus.Add(listItem2)
                    _arrayListClaimStatus.Add(listItem3)

                End If
                Return _arrayListClaimStatus
            End Get
        End Property

        Public Enum enumWSCStatus
            Baru = 0
            Proses = 1
            Selesai = 2
        End Enum

        Public Shared Function RetriveWSCStatus() As ArrayList
            Dim _arrayListStatusWSC As New ArrayList
            Dim sts As enumStat

            sts = New enumStat(0, "Baru")
            _arrayListStatusWSC.Add(sts)
            sts = New enumStat(1, "Proses")
            _arrayListStatusWSC.Add(sts)
            sts = New enumStat(2, "Selesai")
            _arrayListStatusWSC.Add(sts)

            Return _arrayListStatusWSC

        End Function

        Public Class enumStat
            Private _Val As Integer
            Private _Name As String

            Public Sub New(ByVal val As Integer, ByVal name As String)
                _Val = val
                _Name = name
            End Sub

            Public Property ValStatus() As Integer
                Get
                    Return _Val
                End Get
                Set(ByVal Value As Integer)
                    _Val = Value
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

        Public Class enumStatStr
            Private _Val As String
            Private _Name As String

            Public Sub New(ByVal val As String, ByVal name As String)
                _Val = val
                _Name = name
            End Sub

            Public Property ValStatus() As String
                Get
                    Return _Val
                End Get
                Set(ByVal Value As String)
                    _Val = Value
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
#End Region

    End Class

End Namespace