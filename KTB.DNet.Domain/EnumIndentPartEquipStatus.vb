
Namespace KTB.DNet.Domain
    Public Class EnumIndentPartEquipStatus

#Region "Enumeration"

        Public Enum EnumStatusDealer
            Baru = 0
            Batal_Order = 1
            Kirim = 2
            Batal = 3
            Tolak = 4
            Proses = 5
            Selesai = 6
            Expired = 99
        End Enum

        Public Enum EnumStatusKTB
            Baru = 1
            Rilis = 2
            Tolak = 3
            Proses_Order = 4
            Batal = 5
            Selesai = 6
            Expired = 99
        End Enum

        Public Enum EnumUpdateStatusDealer
            Kirim = 0 'pengajuan order (B or C)
            Batal = 1 '
        End Enum

        Public Enum EnumUpdateStatusKTB
            Rilis = 0 'Rilisi Deposit B
            Proses = 1 '
            Tolak = 2 'Tolak Dep B
            Batal = 3 '
            Selesai = 4 '
        End Enum

#End Region

#Region "Custom Methods"

        Public Shared Function GetStatusDealerDesc(ByVal Status As Short) As String
            Dim Rsl As String = ""

            If Status = EnumIndentPartEquipStatus.EnumStatusDealer.Baru Then Rsl = EnumIndentPartEquipStatus.EnumStatusDealer.Baru.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusDealer.Batal_Order Then Rsl = EnumIndentPartEquipStatus.EnumStatusDealer.Batal_Order.ToString() '.Replace("_", " ")
            If Status = EnumIndentPartEquipStatus.EnumStatusDealer.Kirim Then Rsl = EnumIndentPartEquipStatus.EnumStatusDealer.Kirim.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusDealer.Batal Then Rsl = EnumIndentPartEquipStatus.EnumStatusDealer.Batal.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusDealer.Tolak Then Rsl = EnumIndentPartEquipStatus.EnumStatusDealer.Tolak.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusDealer.Proses Then Rsl = EnumIndentPartEquipStatus.EnumStatusDealer.Proses.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusDealer.Selesai Then Rsl = EnumIndentPartEquipStatus.EnumStatusDealer.Selesai.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusDealer.Expired Then Rsl = EnumIndentPartEquipStatus.EnumStatusDealer.Expired.ToString()
            Return Rsl
        End Function

        Public Shared Function GetStatusKTBDesc(ByVal Status As Short) As String
            Dim Rsl As String = ""

            If Status = EnumIndentPartEquipStatus.EnumStatusKTB.Baru Then Rsl = EnumIndentPartEquipStatus.EnumStatusKTB.Baru.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusKTB.Rilis Then Rsl = EnumIndentPartEquipStatus.EnumStatusKTB.Rilis.ToString() '.Replace("_", " ")
            If Status = EnumIndentPartEquipStatus.EnumStatusKTB.Tolak Then Rsl = EnumIndentPartEquipStatus.EnumStatusKTB.Tolak.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order Then Rsl = EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusKTB.Batal Then Rsl = EnumIndentPartEquipStatus.EnumStatusKTB.Batal.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusKTB.Selesai Then Rsl = EnumIndentPartEquipStatus.EnumStatusKTB.Selesai.ToString()
            If Status = EnumIndentPartEquipStatus.EnumStatusKTB.Expired Then Rsl = EnumIndentPartEquipStatus.EnumStatusKTB.Expired.ToString()
            Return Rsl
        End Function

        Public Shared Function GetUpdateStatusDealerDesc(ByVal Status As Short) As String
            Dim Rsl As String = ""
            If Status = EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Kirim Then Rsl = EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Kirim.ToString()
            If Status = EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Batal Then Rsl = EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Batal.ToString()
            Return Rsl
        End Function

        Public Shared Function GetUpdateStatusKTBDesc(ByVal Status As Short) As String
            Dim Rsl As String = ""
            If Status = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Rilis Then Rsl = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Rilis.ToString()
            If Status = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Proses Then Rsl = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Proses.ToString()
            If Status = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak Then Rsl = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak.ToString()
            If Status = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Batal Then Rsl = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Batal.ToString()
            If Status = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Selesai Then Rsl = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Selesai.ToString()
            Return Rsl
        End Function

        Public Shared Function GetStatusDealerList() As ArrayList
            Dim arlRsl As New ArrayList
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusDealer.Baru & ";" & EnumIndentPartEquipStatus.GetStatusDealerDesc(EnumIndentPartEquipStatus.EnumStatusDealer.Baru))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusDealer.Batal_Order & ";" & EnumIndentPartEquipStatus.GetStatusDealerDesc(EnumIndentPartEquipStatus.EnumStatusDealer.Batal_Order))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusDealer.Kirim & ";" & EnumIndentPartEquipStatus.GetStatusDealerDesc(EnumIndentPartEquipStatus.EnumStatusDealer.Kirim))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusDealer.Batal & ";" & EnumIndentPartEquipStatus.GetStatusDealerDesc(EnumIndentPartEquipStatus.EnumStatusDealer.Batal))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusDealer.Tolak & ";" & EnumIndentPartEquipStatus.GetStatusDealerDesc(EnumIndentPartEquipStatus.EnumStatusDealer.Tolak))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusDealer.Proses & ";" & EnumIndentPartEquipStatus.GetStatusDealerDesc(EnumIndentPartEquipStatus.EnumStatusDealer.Proses))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusDealer.Selesai & ";" & EnumIndentPartEquipStatus.GetStatusDealerDesc(EnumIndentPartEquipStatus.EnumStatusDealer.Selesai))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusDealer.Expired & ";" & EnumIndentPartEquipStatus.GetStatusDealerDesc(EnumIndentPartEquipStatus.EnumStatusDealer.Expired))
            Return arlRsl
        End Function

        Public Shared Function GetStatusKTBList() As ArrayList
            Dim arlRsl As New ArrayList
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusKTB.Baru & ";" & EnumIndentPartEquipStatus.GetStatusKTBDesc(EnumIndentPartEquipStatus.EnumStatusKTB.Baru))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusKTB.Rilis & ";" & EnumIndentPartEquipStatus.GetStatusKTBDesc(EnumIndentPartEquipStatus.EnumStatusKTB.Rilis))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusKTB.Tolak & ";" & EnumIndentPartEquipStatus.GetStatusKTBDesc(EnumIndentPartEquipStatus.EnumStatusKTB.Tolak))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order & ";" & EnumIndentPartEquipStatus.GetStatusKTBDesc(EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusKTB.Batal & ";" & EnumIndentPartEquipStatus.GetStatusKTBDesc(EnumIndentPartEquipStatus.EnumStatusKTB.Batal))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumStatusKTB.Selesai & ";" & EnumIndentPartEquipStatus.GetStatusKTBDesc(EnumIndentPartEquipStatus.EnumStatusKTB.Selesai))
            Return arlRsl
        End Function

        Public Shared Function GetUpdateStatusDealerList() As ArrayList
            Dim arlRsl As New ArrayList
            arlRsl.Add("-1;Silahkan Pilih")
            arlRsl.Add(EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Kirim & ";" & EnumIndentPartEquipStatus.GetUpdateStatusDealerDesc(EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Kirim))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Batal & ";" & EnumIndentPartEquipStatus.GetUpdateStatusDealerDesc(EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Batal))
            Return arlRsl
        End Function

        Public Shared Function GetUpdateStatusKTBList() As ArrayList
            Dim arlRsl As New ArrayList
            arlRsl.Add("-1;Silahkan Pilih")
            arlRsl.Add(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Rilis & ";" & EnumIndentPartEquipStatus.GetUpdateStatusKTBDesc(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Rilis))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Proses & ";" & EnumIndentPartEquipStatus.GetUpdateStatusKTBDesc(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Proses))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak & ";" & EnumIndentPartEquipStatus.GetUpdateStatusKTBDesc(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Batal & ";" & EnumIndentPartEquipStatus.GetUpdateStatusKTBDesc(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Batal))
            arlRsl.Add(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Selesai & ";" & EnumIndentPartEquipStatus.GetUpdateStatusKTBDesc(EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Selesai))
            Return arlRsl
        End Function

        Public Shared Function IsValidUpdateDealer(ByVal UpdateStatusDealer As Short, ByRef OldDealerStatus As Short, ByRef NewDealerStatus As Short, ByRef OldKTBStatus As Short, ByRef NewKTBStatus As Short, ByVal PaymentType As Short, Optional ByRef strMessage As String = "") As Boolean
            Dim IsValid As Boolean = False

            If UpdateStatusDealer = EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Kirim Then
                If OldDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Baru Then
                    IsValid = True
                    NewDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Kirim
                    If PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                        NewKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Baru
                    Else
                        NewKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Rilis
                    End If
                End If
            ElseIf UpdateStatusDealer = EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Batal Then
                If OldDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Baru Then
                    IsValid = True
                    NewDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Batal
                End If
            End If

            If Not IsValid Then
                strMessage = "Update status " & EnumIndentPartEquipStatus.GetUpdateStatusDealerDesc(UpdateStatusDealer) & " gagal. "
                strMessage &= "Perubahan status " & EnumIndentPartEquipStatus.GetStatusDealerDesc(OldDealerStatus) & " menjadi " & EnumIndentPartEquipStatus.GetStatusDealerDesc(NewDealerStatus) & " tidak valid"
            End If
            Return IsValid
        End Function

        Public Shared Function IsValidUpdateKTB(ByVal UpdateStatusKTB As Short, ByRef OldKTBStatus As Short, ByRef NewKTBStatus As Short, ByRef OldDealerStatus As Short, ByRef NewDealerStatus As Short, ByVal PaymentType As Short, Optional ByRef strMessage As String = "") As Boolean
            Dim IsValid As Boolean = False

            If UpdateStatusKTB = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Rilis Then
                If OldKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Baru And PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                    IsValid = True
                    NewKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Rilis
                    NewDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Proses
                End If
            ElseIf UpdateStatusKTB = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Proses Then
                If OldKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Rilis Then
                    IsValid = True
                    NewKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order
                    NewDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Proses
                End If
            ElseIf UpdateStatusKTB = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Tolak Then
                If (PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B And OldKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Baru) _
                Or (PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_C And OldKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Rilis) Then
                    IsValid = True
                    NewKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Tolak
                    NewDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Tolak
                End If
            ElseIf UpdateStatusKTB = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Batal Then
                If OldKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order Then
                    IsValid = True
                    NewKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Batal
                    NewDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Batal
                End If
            ElseIf UpdateStatusKTB = EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Selesai Then
                If OldKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order Then
                    IsValid = True
                    NewKTBStatus = EnumIndentPartEquipStatus.EnumStatusKTB.Selesai
                    NewDealerStatus = EnumIndentPartEquipStatus.EnumStatusDealer.Selesai
                End If
            End If
            If Not IsValid Then
                strMessage = "Update status " & EnumIndentPartEquipStatus.GetUpdateStatusKTBDesc(UpdateStatusKTB) & " gagal. "
                strMessage &= "Perubahan status " & EnumIndentPartEquipStatus.GetStatusKTBDesc(OldKTBStatus) & " menjadi " & EnumIndentPartEquipStatus.GetStatusKTBDesc(NewKTBStatus) & " tidak valid"
            End If
            Return IsValid
        End Function

#End Region
    End Class
End Namespace
