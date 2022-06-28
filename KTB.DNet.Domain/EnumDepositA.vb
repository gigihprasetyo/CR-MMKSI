Namespace KTB.DNet.Domain

    Public Class EnumDepositA

        Public Enum TipePengajuan
            Offset = 1
            CashAnnual = 2
            ' CashTahunan = 2
            CashIncidental = 3
            CashInterest = 4
        End Enum

        Public Enum TipePengajuanPencairan
            Offset = 1
            CashTahunan = 2
            CashIncidental = 3
            CashInterest = 4
        End Enum

#Region "Interset Deposit"
        Public Enum StatusPencairanInterest
            BelumCair = 1
            Proses = 2
            ProsesCair = 3
            SudahCair = 4
            All = 0

        End Enum
   Public Shared Function RetrieveInterest(ByVal Year As Integer, Optional ByVal Status As StatusPencairanInterest = StatusPencairanInterest.All, Optional ByVal ID As Integer = 0) As String

            Return String.Format("SELECT ID FROM dbo.fn_StatusDepositAInterestH( {0},{1},{2} )", Year.ToString(), CInt(Status).ToString(), ID.ToString())

        End Function

#End Region



#Region "Debit Note"
        Public Enum StatusPencairanDebitNote
            BelumCair = 1
            Proses = 2
            ProsesCair = 3
            SudahCair = 4
            All = 0

        End Enum
      Public Shared Function RetrieveDebitNote(ByVal StartPostingDate As DateTime, ByVal EndPostingDate As DateTime, Optional ByVal Status As StatusPencairanDebitNote = StatusPencairanDebitNote.All, Optional ByVal ID As Integer = 0) As String

            Return String.Format("SELECT ID FROM dbo.fn_StatusDepositADebitNote( '{0}','{1}',{2},{3} )", StartPostingDate.ToString("yyyyMMdd"), EndPostingDate.ToString("yyyyMMdd"), CInt(Status).ToString(), ID.ToString())

        End Function
#End Region

#Region "Annual Deposit"
        Public Enum StatusPencairanAnnual
            BelumCair = 1
            Proses = 2
            ProsesCair = 3
            SudahCair = 4
            All = 0

        End Enum


        Public Shared Function RetrieveAnnual(ByVal FromDate As DateTime, ByVal ToDate As DateTime, Optional ByVal Status As StatusPencairanAnnual = StatusPencairanAnnual.All, Optional ByVal ID As Integer = 0) As String

            Return String.Format("SELECT ID FROM dbo.fn_StatusDepositAAnnual( '{0}','{1}',{2},{3} )", FromDate.ToString("yyyyMMdd"), ToDate.ToString("yyyyMMdd"), CInt(Status).ToString(), ID.ToString())

        End Function
#End Region

#Region "Status Pengajuan PEncairan"
        Public Enum StatusPencairanDealer
            Baru = 0
            Validasi = 1
            Konfirmasi = 10
            Setuju = 11
            Tolak = 12
            Blok = 14
            Selesai = 16
        End Enum

        Public Enum StatusPencairanKTB
            Baru = 0
            Validasi = 1
            Konfirmasi = 10
            Setuju = 11
            Tolak = 12
            Blok = 14
            Selesai = 16
        End Enum

        
#End Region

#Region "Status Kuintansi"
        Public Enum StatusKuitansi
            Baru = 0
            Validasi = 1
            Konfirmasi = 10
            Printed = 11
            Selesai = 12
            Hapus = 13
            'belum naik
            Proses = 14
            CancelJV = 15
            Cair = 16
        End Enum

        Public Enum StatusKuitansiDealer
            Baru = 0
            Validasi = 1
            Konfirmasi = 10
            Printed = 11
            Selesai = 12
            Hapus = 13
            'belum naik
            Proses = 14
            CancelJV = 15
            Cair = 16 '- -New Status
        End Enum

        Public Enum StatusKuitansiKTB
            Validasi = 1
            Konfirmasi = 10
            Printed = 11
            Selesai = 12
            'belum naik
            Proses = 14
            CancelJV = 15
            Cair = 16
        End Enum


        Public Enum StatusPencairan
            Baru = 0
            Validasi = 1
            BatalValidasi = 2
            Konfirmasi = 3
            Setuju = 4
            Pending = 5
            Tolak = 6
        End Enum
#End Region



    End Class


End Namespace
