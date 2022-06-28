
Namespace KTB.DNet.Domain

    Public Class DepositBEnum

#Region "Pengajuan"
        Public Enum TipePengajuan
            Transfer = 1
            ProjectService = 2
            Interest = 3
            Offset_SP = 4
            KewajibanReguler = 5
            Kewajiban_NonReguler = 6

        End Enum

        Public Function RetrieveTipePengajuan(Optional ByVal isAll As Boolean = True) As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumProperty
            If isAll = True Then
                sts = New EnumProperty(0, "Silahkan Pilih")
                al.Add(sts)
            End If
            sts = New EnumProperty(1, "Transfer")
            al.Add(sts)
            sts = New EnumProperty(2, "Project Service")
            al.Add(sts)
            sts = New EnumProperty(3, "Interest")
            al.Add(sts)
            sts = New EnumProperty(4, "Offset SP")
            al.Add(sts)
            sts = New EnumProperty(5, "Kewajiban Reguler")
            al.Add(sts)
            sts = New EnumProperty(6, "Kewajiban Non Reguler")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValueTipePengajuan(ByVal iType As Integer) As String
            Dim str As String = ""
            If iType = 0 Then str = "Semua"
            If iType = 1 Then str = "Transfer"
            If iType = 2 Then str = "Project Service"
            If iType = 3 Then str = "Interest"
            If iType = 4 Then str = "Offset SP"
            If iType = 5 Then str = "Kewajiban Reguler"
            If iType = 6 Then str = "Kewajiban Non Reguler"
            Return str
        End Function

        Public Enum StatusPengajuan
            Baru = 0 'Belum diajukan
            Proses = 1
            Selesai = 2
            Transfer = 9 'Transfer / Estimasi
        End Enum

        Public Function RetrieveStatusPengajuan(Optional ByVal isAll As Boolean = False) As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumProperty
            If isAll = True Then
                sts = New EnumProperty(-1, "Silahkan Pilih")
                al.Add(sts)
            End If
            sts = New EnumProperty(0, "Belum diajukan")
            al.Add(sts)
            sts = New EnumProperty(1, "Proses")
            al.Add(sts)
            sts = New EnumProperty(2, "Selesai")
            al.Add(sts)
            sts = New EnumProperty(9, "Transfer/Estimasi")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValueStatusPengajuan(ByVal iType As Integer) As String
            Dim str As String = ""
            If iType = 0 Then str = "Belum diajukan"
            If iType = 1 Then str = "Proses"
            If iType = 2 Then str = "Selesai"
            If iType = 9 Then str = "Transfer/Estimasi"
            Return str
        End Function

#End Region

#Region "Grade"
        Public Enum GradeDealer
            NonGrade = 0
            Diamond_1
            Diamond_2
            Diamond_3
            Diamond_4
            Diamond_5
        End Enum

        Public Function RetrieveGradeDealer() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumProperty
            sts = New EnumProperty(0, "Non Grade")
            al.Add(sts)
            sts = New EnumProperty(1, "Diamond 1")
            al.Add(sts)
            sts = New EnumProperty(2, "Diamond 2")
            al.Add(sts)
            sts = New EnumProperty(3, "Diamond 3")
            al.Add(sts)
            sts = New EnumProperty(4, "Diamond 4")
            al.Add(sts)
            sts = New EnumProperty(5, "Diamond 5")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValueGradeDealer(ByVal iGrade As Integer) As String
            Dim str As String = ""
            If iGrade = 0 Then str = "Non Grade"
            If iGrade = 1 Then str = "Diamond 1"
            If iGrade = 2 Then str = "Diamond 2"
            If iGrade = 3 Then str = "Diamond 3"
            If iGrade = 4 Then str = "Diamond 4"
            If iGrade = 5 Then str = "Diamond 5"
            Return str
        End Function
#End Region

#Region "Kewajiban"
        Public Enum TipeKewajiban
            Regular = 1
            NonReguler = 2
        End Enum

        Public Function RetrieveTipeKewajiban(Optional ByVal isAll As Boolean = True) As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumProperty
            If isAll Then
                sts = New EnumProperty(0, "Silahkan Pilih")
                al.Add(sts)
            End If
            sts = New EnumProperty(1, "Reguler")
            al.Add(sts)
            sts = New EnumProperty(2, "Non Reguler")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValueKewajiban(ByVal iType As Integer) As String
            Dim str As String = ""
            If iType = 0 Then str = "Semua"
            If iType = 1 Then str = "Reguler"
            If iType = 2 Then str = "Non Reguler"
            Return str
        End Function


#End Region

#Region "Pencairan"
        Public Enum StatusPencairan
            Baru = 0
            Validasi = 1
            Batal_Validasi = 2
            Tolak = 3
            Konfirmasi = 4
            Batal_Konfirmasi = 5
            Proses = 6
            Selesai = 7
            Cair = 8
        End Enum


        Public Function RetrieveStatuspencairan(ByVal dealerTitle As Integer, Optional isAll As Boolean = False, Optional isAutoProcessInclude As Boolean = False) As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumProperty
            If isAll Then
                sts = New EnumProperty(-1, "Silahkan Pilih")
                al.Add(sts)
            End If
            Select Case dealerTitle
                Case EnumDealerTittle.DealerTittle.DEALER
                    'sts = New EnumProperty(0, "Baru")
                    'al.Add(sts)
                    sts = New EnumProperty(1, "Validasi")
                    al.Add(sts)
                    sts = New EnumProperty(2, "Batal Validasi")
                    al.Add(sts)
                Case EnumDealerTittle.DealerTittle.KTB
                    sts = New EnumProperty(3, "Tolak")
                    al.Add(sts)
                    sts = New EnumProperty(4, "Konfirmasi")
                    al.Add(sts)
                    sts = New EnumProperty(5, "Batal Konfirmasi")
                    al.Add(sts)
                    If isAutoProcessInclude = True Then
                        sts = New EnumProperty(6, "Proses")
                        al.Add(sts)
                        sts = New EnumProperty(7, "Selesai")
                        al.Add(sts)
                        sts = New EnumProperty(8, "Cair")
                        al.Add(sts)
                    End If

                Case EnumDealerTittle.DealerTittle.KTB_DEALER
                    sts = New EnumProperty(0, "Baru")
                    al.Add(sts)
                    sts = New EnumProperty(1, "Validasi")
                    al.Add(sts)
                    'sts = New EnumProperty(2, "Batal Validasi")
                    'al.Add(sts)
                    sts = New EnumProperty(3, "Tolak")
                    al.Add(sts)
                    sts = New EnumProperty(4, "Konfirmasi")
                    al.Add(sts)
                    'sts = New EnumProperty(5, "Batal Konfirmasi")
                    'al.Add(sts)
                    sts = New EnumProperty(6, "Proses")
                    al.Add(sts)
                    sts = New EnumProperty(7, "Selesai")
                    al.Add(sts)
                    sts = New EnumProperty(8, "Cair")
                    al.Add(sts)
            End Select
            Return al
        End Function

        Public Function RetrieveStatuspencairanForMenuKuitansi() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumProperty
            sts = New EnumProperty(-1, "Silahkan Pilih")
            al.Add(sts)
            sts = New EnumProperty(4, "Konfirmasi")
            al.Add(sts)
            sts = New EnumProperty(6, "Proses")
            al.Add(sts)
            sts = New EnumProperty(7, "Selesai")
            al.Add(sts)
            sts = New EnumProperty(8, "Cair")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValueStatusPencairan(ByVal iStatus As Integer) As String
            Select Case iStatus
                Case 0
                    Return "Baru"
                Case 1
                    Return "Validasi"
                Case 2
                    Return "Batal Validasi"
                Case 3
                    Return "Tolak"
                Case 4
                    Return "Konfirmasi"
                Case 5
                    Return "Batal Konfirmasi"
                Case 6
                    Return "Proses"
                Case 7
                    Return "Selesai"
                Case 8
                    Return "Cair"
                Case Else
                    Return ""
            End Select
        End Function
#End Region

#Region "StatusType"
        Public Enum StatusType
            Pencairan = 1
            DebitNote = 2
            Interest = 3
            Kewajiban = 4
        End Enum
#End Region

    End Class

    Public Class EnumProperty
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
