Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.SAP
Imports KTB.DNet.UI.Helper


Public Class FrmMSPExtendedPaymentList
    Inherits System.Web.UI.Page

#Region "Private variable"

    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private _strSessMSPTransferPaymentID As String = "MSPExTrfPaymentID"
    Private _strSessStatusInput As String = "StatusInput"
    Private _strSessTrfPaymentList As String = "MSPTrfPaymentList"
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sortCols As SortCollection
    Private objLoginDealer As Dealer
    Private objTrfPayment As MSPExPayment
    Private objTrfPaymentDetail As MSPExPaymentDetail
    Private HistoryFolderSAP As String = String.Empty
    Private _view As Boolean = False
    Private _input As Boolean = False
    Private _edit As Boolean = False

#End Region

#Region "Custom sub/func"

    Private Function GetMSPExPaymentIDList(ByVal strMSPRegisHistoryID As String) As String
        Dim strMSPTransferPaymentID As String = String.Empty
        If strMSPRegisHistoryID <> String.Empty Then
            Dim crtMSPTPDetail As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtMSPTPDetail.opAnd(New Criteria(GetType(MSPExPaymentDetail), "MSPExRegistration.ID", MatchType.InSet, "(" + strMSPRegisHistoryID + ")"))

            Dim arlMSPTPDetail As ArrayList = New MSPExPaymentDetailFacade(User).Retrieve(crtMSPTPDetail)
            If arlMSPTPDetail.Count > 0 Then
                For Each oMSPTPDetail As MSPExPaymentDetail In arlMSPTPDetail
                    If strMSPTransferPaymentID = String.Empty Then
                        strMSPTransferPaymentID = oMSPTPDetail.MSPExPayment.ID.ToString
                    Else
                        If Not strMSPTransferPaymentID.Split(",").Contains(oMSPTPDetail.MSPExPayment.ID.ToString) Then
                            'If Not strMSPTransferPaymentID.Contains(oMSPTPDetail.MSPExPayment.ID.ToString) Then
                            strMSPTransferPaymentID = strMSPTransferPaymentID & ", " & oMSPTPDetail.MSPExPayment.ID.ToString
                        End If
                    End If
                Next
            End If
        End If
        Return strMSPTransferPaymentID
    End Function

    Private Sub CheckPrivilege()
        _view = SecurityProvider.Authorize(Context.User, SR.MSPExtendedPayment_View)
        _input = SecurityProvider.Authorize(Context.User, SR.MSPExtendedPayment_Input)
        _edit = SecurityProvider.Authorize(Context.User, SR.MSPExtendedPayment_Ubah)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP Extended - Pembayaran MSP")
        End If

        'If Not _edit Then
        '    lblChangeStatus.Visible = False
        '    ddlProses.Visible = False
        '    btnProses.Visible = False
        'End If

    End Sub

    Private Sub BindDropDown()
        ' dropdown status
        lboxStatus.Items.Clear()
        'sementara di hardcode

        Dim arrStatus As ArrayList = New EnumMSPEx().RetrieveAllStatus()
        'If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    arrStatus.RemoveAt(0)
        'End If
        lboxStatus.DataSource = arrStatus
        lboxStatus.DataTextField = "Value"
        lboxStatus.DataValueField = "ID"
        lboxStatus.DataBind()



        ddlTipeProgram.Items.Clear()
        Dim sMSPProgram As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumMSPProgram")
        ddlTipeProgram.DataSource = sMSPProgram
        ddlTipeProgram.DataTextField = "ValueDesc"
        ddlTipeProgram.DataValueField = "ValueCode"
        ddlTipeProgram.DataBind()
        ddlTipeProgram.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSrcForm As ArrayList = Session("SessionTransferPaymentList")
        If Not objSrcForm Is Nothing Then
            txtCreditAccount.Text = objSrcForm.Item(0)
            txtPaymentRegNo.Text = objSrcForm.Item(1)
            chkRequestDate.Checked = objSrcForm.Item(2)
            DateFrom.Value = CType(objSrcForm.Item(3), Date)
            DateTo.Value = CType(objSrcForm.Item(4), Date)
            lboxStatus.SelectedValue = objSrcForm.Item(5)
            dtgMSPPaymentList.CurrentPageIndex = CType(objSrcForm.Item(6), Integer)
            ViewState("CurrentSortColumn") = objSrcForm.Item(7)
            ViewState("CurrentSortDirect") = objSrcForm.Item(8)

            Return True
        End If
        Return False
    End Function

    Private Sub SetSessionCriteria()
        Dim objSrcForm As ArrayList = New ArrayList
        objSrcForm.Add(txtCreditAccount.Text.Trim) '0
        objSrcForm.Add(txtPaymentRegNo.Text.Trim) '1
        objSrcForm.Add(chkRequestDate.Checked) '2
        objSrcForm.Add(DateFrom.Value) '3
        objSrcForm.Add(DateTo.Value) '4
        objSrcForm.Add(lboxStatus.SelectedValue) '5
        objSrcForm.Add(dtgMSPPaymentList.CurrentPageIndex) '6
        objSrcForm.Add(CType(ViewState("CurrentSortColumn"), String)) '7
        objSrcForm.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection)) '8

        Session("SessionTransferPaymentList") = objSrcForm
    End Sub

    Private Sub CreateCriteria()
        Dim str As String = String.Empty
        Dim isStrSql As Integer = 0
        Dim strSql As String = "SELECT ID FROM MSPExPayment WHERE RowStatus = 0"

        crt = New CriteriaComposite(New Criteria(GetType(MSPExPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtCreditAccount.Text.Trim <> String.Empty Then
                Dim strCredit As String() = txtCreditAccount.Text.Split(";")
                Dim creditFilter As String = String.Empty
                If strCredit.Count > 0 Then
                    For i As Integer = 0 To strCredit.Count - 1
                        creditFilter += "'" & strCredit(i) & "',"
                    Next
                    creditFilter = creditFilter.Substring(0, creditFilter.Count - 1)
                End If
                crt.opAnd(New Criteria(GetType(MSPExPayment), "Dealer.CreditAccount", MatchType.InSet, "(" & creditFilter & ")"))
            End If
        Else
            crt.opAnd(New Criteria(GetType(MSPExPayment), "Dealer.CreditAccount", MatchType.Exact, objLoginDealer.CreditAccount))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            Dim strDealer As String() = txtKodeDealer.Text.Split(";")
            Dim dealerFilter As String = String.Empty
            If strDealer.Count > 0 Then
                For i As Integer = 0 To strDealer.Count - 1
                    dealerFilter += "'" & strDealer(i) & "',"
                Next
                dealerFilter = dealerFilter.Substring(0, dealerFilter.Count - 1)
            End If
            crt.opAnd(New Criteria(GetType(MSPExPayment), "Dealer.DealerCode", MatchType.InSet, "(" & dealerFilter & ")"))
        End If

        If txtPaymentRegNo.Text.Trim <> String.Empty Then
            crt.opAnd(New Criteria(GetType(MSPExPayment), "RegNumber", MatchType.Partial, txtPaymentRegNo.Text))
        End If

        If chkRequestDate.Checked Then
            If DateFrom.Value <> "#12:00:00 AM#" And DateTo.Value <> "#12:00:00 AM#" Then
                crt.opAnd(New Criteria(GetType(MSPExPayment), "PlanTransferDate", MatchType.GreaterOrEqual, Format(DateFrom.Value, "yyyy-MM-dd")))
                crt.opAnd(New Criteria(GetType(MSPExPayment), "PlanTransferDate", MatchType.LesserOrEqual, Format(DateTo.Value, "yyyy-MM-dd")))
            ElseIf DateFrom.Value = "#12:00:00 AM#" Then
                str += "\n" + "Tanggal pengajuan mulai tidak boleh kosong."
            ElseIf DateTo.Value = "#12:00:00 AM#" Then
                str += "\n" + "Tanggal pengajuan sampai tidak boleh kosong."
            End If
        End If

        'If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    strSql += " AND Status in (0,1,2,3,4) "
        '    isStrSql += 1
        'End If

        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            strSql += " AND Status IN(" & SelectedStatus & ")"
            isStrSql += 1
        End If

        Dim strMSPRegisHistoryID As String = String.Empty
        Dim strMSPTransferPaymentID As String = String.Empty
        If txtNoDebitCharge.Text.Trim <> String.Empty Then
            Dim crtMSPDC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtMSPDC.opAnd(New Criteria(GetType(MSPExDebitCharge), "DebitChargeNo", MatchType.Partial, txtNoDebitCharge.Text))

            Dim arlMSPDC As ArrayList = New MSPExDebitChargeFacade(User).Retrieve(crtMSPDC)
            If arlMSPDC.Count > 0 Then
                For Each oMSPDC As MSPExDebitCharge In arlMSPDC
                    If strMSPRegisHistoryID = String.Empty Then
                        strMSPRegisHistoryID = oMSPDC.MSPExRegistration.ID.ToString
                    Else
                        If Not strMSPRegisHistoryID.Split(",").Contains(oMSPDC.MSPExRegistration.ID.ToString) Then
                            'If Not strMSPRegisHistoryID.Contains(oMSPDC.MSPExRegistration.ID.ToString) Then
                            strMSPRegisHistoryID = strMSPRegisHistoryID & "," & oMSPDC.MSPExRegistration.ID.ToString
                        End If
                    End If
                Next
            End If
            strMSPTransferPaymentID = GetMSPExPaymentIDList(strMSPRegisHistoryID)
        End If

        If txtNoDebitMemo.Text.Trim <> String.Empty Then
            Dim crtMSPDM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitMemo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtMSPDM.opAnd(New Criteria(GetType(MSPExDebitMemo), "DebitMemoNo", MatchType.Partial, txtNoDebitMemo.Text.Trim))

            Dim arlMSPDM As ArrayList = New MSPExDebitMemoFacade(User).Retrieve(crtMSPDM)
            If arlMSPDM.Count > 0 Then
                For Each oMSPDM As MSPExDebitMemo In arlMSPDM
                    If strMSPRegisHistoryID = String.Empty Then
                        strMSPRegisHistoryID = oMSPDM.MSPExRegistration.ID.ToString
                    Else
                        If Not strMSPRegisHistoryID.Split(",").Contains(oMSPDM.MSPExRegistration.ID.ToString) Then
                            'If Not strMSPRegisHistoryID.Contains(oMSPDM.MSPExRegistration.ID.ToString) Then
                            strMSPRegisHistoryID = strMSPRegisHistoryID & "," & oMSPDM.MSPExRegistration.ID.ToString
                        End If
                    End If
                Next
            End If
            strMSPTransferPaymentID = GetMSPExPaymentIDList(strMSPRegisHistoryID)
        End If

        If ddlTipeProgram.SelectedIndex <> 0 Then
            TipeMSPSearch(crt)
        End If

        If isStrSql > 0 Then
            crt.opAnd(New Criteria(GetType(MSPExPayment), "ID", MatchType.InSet, "(" + strSql + ")"))
        End If

        If (txtNoDebitMemo.Text.Trim <> String.Empty) OrElse txtNoDebitCharge.Text.Trim <> String.Empty Then
            If strMSPTransferPaymentID.Trim <> String.Empty Then
                crt.opAnd(New Criteria(GetType(MSPExPayment), "ID", MatchType.InSet, "(" + strMSPTransferPaymentID + ")"))
            Else
                crt.opAnd(New Criteria(GetType(MSPExPayment), "ID", MatchType.Exact, 0))
            End If

        End If

        If str <> String.Empty Then
            MessageBox.Show(str.Substring(2, str.Length - 2))
            Return
        End If

        _sessHelper.SetSession(_strSessSearch, crt)
    End Sub

    Private Sub TipeMSPSearch(ByRef crt As CriteriaComposite)
        Dim arrPrefix As ArrayList = New PrefixMSPRegistrationFacade(User).RetrieveList(ddlTipeProgram.SelectedValue)
        Dim strArrMSPExTypeID As String = String.Empty
        If arrPrefix.Count > 0 Then
            For Each oPrefixMSPRegistration As PrefixMSPRegistration In arrPrefix
                If strArrMSPExTypeID = String.Empty Then
                    strArrMSPExTypeID = oPrefixMSPRegistration.MSPExTypeID.ToString
                Else
                    If Not strArrMSPExTypeID.Split(",").Contains(oPrefixMSPRegistration.MSPExTypeID.ToString) Then
                        strArrMSPExTypeID = strArrMSPExTypeID & "," & oPrefixMSPRegistration.MSPExTypeID.ToString
                    End If
                End If
            Next
        Else
            MessageBox.Show("Tipe program " & ddlTipeProgram.SelectedValue & " tidak ditemukan")
            Exit Sub
        End If

        Dim strMSPRegisID As String = String.Empty
        Dim crtReg As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtReg.opAnd(New Criteria(GetType(MSPExRegistration), "MSPExMaster.MSPExType.ID", MatchType.InSet, "(" & strArrMSPExTypeID & ")"))
        Dim arrMSPRegis As ArrayList = New MSPExRegistrationFacade(User).Retrieve(crtReg)
        If arrMSPRegis.Count > 0 Then
            For Each oMSPExRegistration As MSPExRegistration In arrMSPRegis
                If strMSPRegisID = String.Empty Then
                    strMSPRegisID = oMSPExRegistration.ID.ToString
                Else
                    If Not strMSPRegisID.Split(",").Contains(oMSPExRegistration.ID.ToString) Then
                        strMSPRegisID = strMSPRegisID & "," & oMSPExRegistration.ID.ToString
                    End If
                End If
            Next
        End If

        Dim strArrMSPExPaymID As String = GetMSPExPaymentIDList(strMSPRegisID)
        If strArrMSPExPaymID.Trim <> String.Empty Then
            crt.opAnd(New Criteria(GetType(MSPExPayment), "ID", MatchType.InSet, "(" + strArrMSPExPaymID + ")"))
        End If
    End Sub

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub BindDatagrid(ByVal indexPage As Integer, Optional ByVal emptyMwssage As Boolean = True)
        Dim totalRow As Integer = 0
        Dim arrmspDTL As ArrayList
        Dim objmsptpFac As New MSPExPaymentFacade(User)
        Dim cri As CriteriaComposite
        Dim strIDHeader As String = String.Empty

        arr = New MSPExPaymentFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession(_strSessSearch), CriteriaComposite), indexPage + 1, dtgMSPPaymentList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If Not IsNothing(_sessHelper.GetSession(_strSessSearch)) And arr.Count = 0 Then
            If emptyMwssage Then
                MessageBox.Show("Data tidak ditemukan")
            End If
        End If

        If arr.Count > 0 Then
            SetProcessSection()
        End If
        dtgMSPPaymentList.DataSource = arr
        dtgMSPPaymentList.VirtualItemCount = totalRow
        dtgMSPPaymentList.DataBind()
        ' set button in process area

        ' set session
        _sessHelper.SetSession(_strSessTrfPaymentList, arr)
    End Sub

    Private Function SetProcessSection()
        'lblChangeStatus.Visible = True
        'btnProses.Visible = True
        'ddlProses.Visible = True
        lblChangeStatus.Visible = False
        btnProses.Visible = False
        ddlProses.Visible = False
        ddlProses.Items.Clear()
        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            btnTransfertoSAP.Visible = True
            Dim arrProses As ArrayList = New EnumMSPEx().RetrieveStatusProcessForKTB()
            ddlProses.DataSource = arrProses
        Else
            btnTransfertoSAP.Visible = False
            ddlProses.DataSource = New EnumMSPEx().RetrieveStatusProcessForDealer()
        End If

        btnDownload.Visible = True
        ddlProses.DataTextField = "Value"
        ddlProses.DataValueField = "ID"
        ddlProses.DataBind()
        ddlProses.Items.Insert(0, New ListItem("Silakan Pilih", -1))
        ddlProses.SelectedIndex = 0
    End Function

    Private Function CommandHistory(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPPaymentID As Label = CType(e.Item.FindControl("lblMSPPaymentID"), Label)
        _sessHelper.SetSession(_strSessMSPTransferPaymentID, lblMSPPaymentID.Text)
        Response.Redirect("FrmMSPPaymentHistory.aspx")
    End Function

    Private Function CommandViewData(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPPaymentID As Label = CType(e.Item.FindControl("lblMSPPaymentID"), Label)
        _sessHelper.SetSession(_strSessStatusInput, "VIEW")
        _sessHelper.SetSession(_strSessMSPTransferPaymentID, lblMSPPaymentID.Text)
        Response.Redirect("FrmMSPExtendedPayment.aspx")
    End Function

    Private Function CommandEditData(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPPaymentID As Label = CType(e.Item.FindControl("lblMSPPaymentID"), Label)
        _sessHelper.SetSession(_strSessStatusInput, "UPDATE")
        _sessHelper.SetSession(_strSessMSPTransferPaymentID, lblMSPPaymentID.Text)
        Response.Redirect("FrmMSPExtendedPayment.aspx")
    End Function

    Private Function CommandDeleteData(ByVal e As DataGridCommandEventArgs)
        Dim lblMSPPaymentID As Label = CType(e.Item.FindControl("lblMSPPaymentID"), Label)
        objTrfPayment = New MSPExPaymentFacade(User).Retrieve(CInt(lblMSPPaymentID.Text))
        Dim facMSPTrpPayment As New MSPExPaymentFacade(User)
        Dim arlDetail As ArrayList = New MSPExPaymentDetailFacade(User).Retrieve(objTrfPayment)

        If objTrfPayment.Status = EnumStatusMSP.Status.Baru Or objTrfPayment.Status = EnumStatusMSP.Status.Batal_Validasi Then
            objTrfPayment.RowStatus = -1
            facMSPTrpPayment.Update(objTrfPayment)
            For Each item As MSPExPaymentDetail In arlDetail
                item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Dim _res As Integer = New MSPExPaymentDetailFacade(User).Update(item)
            Next

            BindDatagrid(0)
            MessageBox.Show("Data berhasil dihapus.")
        End If
    End Function

    Private Function PopulateTrfPaymentSelected(ByVal status As Integer) As ArrayList
        Dim dtgItem As DataGridItem
        Dim enumMSPEx As New EnumMSPEx()
        Dim mSPExPaymentFacade As New MSPExPaymentFacade(User)
        arr = New ArrayList

        If status = 99 Then ' untuk download data
            arr = mSPExPaymentFacade.Retrieve(CType(_sessHelper.GetSession(_strSessSearch), CriteriaComposite))
            Return arr
        End If

        For Each dtgItem In dtgMSPPaymentList.Items
            Dim chkSelect As CheckBox = dtgItem.FindControl("chkSelect")
            If chkSelect.Checked Then
                Dim lblMSPPaymentID As Label = CType(dtgItem.FindControl("lblMSPPaymentID"), Label)
                objTrfPayment = mSPExPaymentFacade.Retrieve(CInt(lblMSPPaymentID.Text))

                Select Case status
                    Case enumMSPEx.MSPExStatusPayment.Validasi ' untuk validasi
                        If objTrfPayment.Status = enumMSPEx.MSPExStatusPayment.Baru Then
                            objTrfPayment.Status = enumMSPEx.MSPExStatusPayment.Validasi

                            arr.Add(objTrfPayment)
                        End If
                        'CR Service Sparepart Enhancement
                        'Case enumMSPEx.MSPExStatusPayment.Konfirmasi ' untuk konfirmasi
                        '    If objTrfPayment.Status = enumMSPEx.MSPExStatusPayment.Validasi Then
                        '        objTrfPayment.Status = enumMSPEx.MSPExStatusPayment.Konfirmasi

                        '        arr.Add(objTrfPayment)
                        '    End If
                    Case enumMSPEx.MSPExStatusPayment.Proses ' untuk transfer
                        If objTrfPayment.Status = enumMSPEx.MSPExStatusPayment.Validasi And objTrfPayment.IsTransfertoSAP <> 1 Then
                            objTrfPayment.Status = enumMSPEx.MSPExStatusPayment.Proses

                            arr.Add(objTrfPayment)
                        End If
                    Case 98 'untuk retransfer
                        If objTrfPayment.Status = enumMSPEx.MSPExStatusPayment.Validasi Then
                            arr.Add(objTrfPayment)
                        End If
                End Select
            End If
        Next
        Return arr
    End Function

    Private Sub DoDownload(ByVal MSPPaymentList As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar Payment [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim PaymentData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(PaymentData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(PaymentData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteMSPPaymentData(sw, MSPPaymentList)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub WriteMSPPaymentData(ByVal sw As StreamWriter, ByVal MSPPaymentList As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("MSP Extended - DAFTAR Pembayaran")
        sw.WriteLine(itemLine)

        '======MSP Payment Data=======
        If (MSPPaymentList.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("CREDIT ACCOUNT" & tab)
            itemLine.Append("TGL TRANSFER" & tab)
            itemLine.Append("NO REGISTRASI PEMBAYARAN" & tab)
            itemLine.Append("TOTAL AMOUNT" & tab)
            itemLine.Append("TGL AKTUAL TRANSFER" & tab)
            itemLine.Append("TOTAL AKTUAL AMOUNT" & tab)
            itemLine.Append("STATUS" & tab)
            itemLine.Append("DEBIT CHARGE NO" & tab)
            itemLine.Append("DEBIT MEMO NO" & tab)
            itemLine.Append("NOMOR TR" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As MSPExPayment In MSPPaymentList
                    If item.MSPExPaymentDetails.Count > 0 Then
                        For Each itemDetail As MSPExPaymentDetail In item.MSPExPaymentDetails
                            itemLine.Remove(0, itemLine.Length)
                            itemLine.Append(i.ToString & tab) 'NO
                            itemLine.Append(item.Dealer.CreditAccount & tab) 'CREDIT ACCOUNT
                            itemLine.Append(item.PlanTransferDate.ToString("dd-MM-yyyy") & tab) 'TRANSFER DATE
                            itemLine.Append(item.RegNumber & tab) 'NO REGISTRASI PEMBAYARAN
                            itemLine.Append(item.TotalAmount.ToString("C") & tab) 'TOTAL AMOUNT
                            itemLine.Append(item.ActualTransferDate.ToString("dd-MM-yyyy") & tab) ' TGL AKTUAL TRANSFER
                            itemLine.Append(item.ActualTotalAmount.ToString("C") & tab) 'TOTAL AKTUAL AMOUNT
                            itemLine.Append(EnumMSPEx.GetStringValue(item.Status) & tab) 'STATUS
                            itemLine.Append(itemDetail.DebitChargeNo & tab) 'DebitChargeNo
                            Dim oDM As MSPExDebitMemo = New MSPExDebitMemoFacade(User).RetrieveByRegistration(itemDetail.MSPExRegistration)
                            itemLine.Append(oDM.DebitMemoNo & tab) 'DebitMemoNo
                            itemLine.Append(item.TRNumber & tab) 'TRNumber

                            sw.WriteLine(itemLine.ToString())
                            i = i + 1
                        Next
                    Else
                        itemLine.Remove(0, itemLine.Length)
                        itemLine.Append(i.ToString & tab) 'NO
                        itemLine.Append(item.Dealer.CreditAccount & tab) 'CREDIT ACCOUNT
                        itemLine.Append(item.PlanTransferDate.ToString("dd-MM-yyyy") & tab) 'TRANSFER DATE
                        itemLine.Append(item.RegNumber & tab) 'NO REGISTRASI PEMBAYARAN
                        itemLine.Append(item.TotalAmount.ToString("C") & tab) 'TOTAL AMOUNT
                        itemLine.Append(item.ActualTransferDate.ToString("dd-MM-yyyy") & tab) ' TGL AKTUAL TRANSFER
                        itemLine.Append(item.ActualTotalAmount.ToString("C") & tab) 'TOTAL AKTUAL AMOUNT
                        itemLine.Append(EnumMSPEx.GetStringValue(item.Status) & tab) 'STATUS
                        itemLine.Append(tab) 'DebitChargeNo
                        itemLine.Append(tab) 'DebitMemoNo
                        itemLine.Append(item.TRNumber & tab) 'TRNumber

                        sw.WriteLine(itemLine.ToString())
                        i = i + 1
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                If HistoryFolderSAP <> String.Empty Then
                    Dim directoryHistory As New DirectoryInfo(HistoryFolderSAP)
                    If Not directoryHistory.Exists Then
                        Directory.CreateDirectory(HistoryFolderSAP)
                    End If
                End If

                If (New MSPExPaymentFacade(User).UpdateTransaction(arl) <> -1) Then
                    sw = New StreamWriter(DestFile)
                    sw.Write(Val)
                    sw.Close()
                    BindDatagrid(0, False)
                Else
                    success = False
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

    Protected Function TransferToSAP(ByVal strType As String)
        Dim objMSPTrfPayment As MSPExPayment
        Dim sb As New StringBuilder
        Dim delimiter As String = ";"
        Dim newUpdateTfrArray As ArrayList = New ArrayList()
        Dim filename = String.Format("{0}{1}{2}", "PaymentConfMSPExt", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\Service\MSPEXT\Payment\" & filename  '-- Destination file
        HistoryFolderSAP = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\Service\MSPEXT\Payment\History"
        'Dim DestFile As String = "D:\RIDWAN\" & filename  '-- Destination file
        'HistoryFolderSAP = "D:\RIDWAN\"
        ' get all checked data
        arr = New ArrayList
        If strType.ToLower = "normal" Then
            arr = PopulateTrfPaymentSelected(EnumMSPEx.MSPExStatusPayment.Proses)
        Else
            arr = PopulateTrfPaymentSelected(98)
        End If

        If arr.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("Pembayaran MSP", "Validasi"))
        Else
            For j As Integer = 0 To arr.Count - 1
                objTrfPayment = CType(arr(j), MSPExPayment)
                objTrfPayment.Status = EnumMSPEx.MSPExStatusPayment.Proses

                Dim i As Integer = 1
                For Each detail As MSPExPaymentDetail In objTrfPayment.MSPExPaymentDetails
                    crt = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPExDebitCharge), "MSPExRegistration.ID", MatchType.Exact, detail.MSPExRegistration.ID))
                    Dim newArr As ArrayList = New MSPExDebitChargeFacade(User).Retrieve(crt)
                    Dim objDC As MSPExDebitCharge = CType(newArr(0), MSPExDebitCharge)
                    'If i > 1 Then
                    '    sb.AppendLine()
                    'End If

                    sb.Append(objTrfPayment.RegNumber)
                    sb.Append(delimiter)
                    sb.Append(If(newArr.Count > 0, objDC.DebitChargeNo, String.Empty))
                    sb.Append(delimiter)
                    sb.Append(objTrfPayment.PlanTransferDate.ToString("ddMMyyyy"))
                    sb.Append(delimiter)
                    sb.Append(CInt(objDC.Amount).ToString())
                    sb.Append(delimiter)
                    sb.Append("EX")
                    sb.AppendLine()
                    i += 1
                Next

                Dim tempObj As MSPExPayment = CType(arr(j), MSPExPayment)
                tempObj.IsTransfertoSAP = 1
                tempObj.Status = EnumMSPEx.MSPExStatusPayment.Proses
                newUpdateTfrArray.Add(tempObj)
            Next

            If (sb.Length > 0) Then
                Dim str As String = String.Empty
                If strType.ToLower = "normal" Then
                    str = "transfer"
                Else
                    str = "transfer ulang"
                End If

                If Transfer(DestFile, sb.ToString(), newUpdateTfrArray) Then
                    MessageBox.Show("Data berhasil di " & str & " ke SAP")
                Else
                    MessageBox.Show("Data gagal di " & str & " ke SAP")
                End If
            End If
        End If
    End Function


#End Region

#Region "Event Handler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        objLoginDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            txtCreditAccount.Text = objLoginDealer.CreditAccount
            txtCreditAccount.Enabled = False
            txtKodeDealer.Text = objLoginDealer.DealerCode
            txtKodeDealer.Enabled = False
            lblSearchDealer.Visible = False
        End If
        If Not IsPostBack Then
            BindDropDown()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = KTB.DNet.Domain.Search.Sort.SortDirection.DESC

            If GetSessionCriteria() Then
                CreateCriteria()
                BindDatagrid(dtgMSPPaymentList.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Sub dtgMSPPaymentList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMSPPaymentList.ItemCommand
        If (e.CommandName.ToUpper = "VIEW") Then
            SetSessionCriteria()
            CommandViewData(e)
        ElseIf (e.CommandName.ToUpper = "EDIT") Then
            SetSessionCriteria()
            CommandEditData(e)
        ElseIf (e.CommandName.ToUpper = "DELETE") Then
            CommandDeleteData(e)
        ElseIf (e.CommandName.ToUpper = "HISTORY") Then
            CommandHistory(e)
        End If
    End Sub

    Private Sub dtgMSPPaymentList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPPaymentList.ItemDataBound
        ' set lblNo
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMSPPaymentList.CurrentPageIndex * dtgMSPPaymentList.PageSize)
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            'Dim rowValue As MSPTransferPaymentDetail = CType(e.Item.DataItem, MSPTransferPaymentDetail)
            Dim rowValue As MSPExPayment = CType(e.Item.DataItem, MSPExPayment)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                ' set ID
                Dim lblMSPPaymentID As Label = CType(e.Item.FindControl("lblMSPPaymentID"), Label)
                Dim lblMSPPaymentDtlID As Label = CType(e.Item.FindControl("lblMSPPaymentDtlID"), Label)
                If Not IsNothing(lblMSPPaymentID) Then
                    lblMSPPaymentID.Text = rowValue.ID
                    lblMSPPaymentDtlID.Text = rowValue.ID
                End If

                ' set lblCreditAccount
                Dim lblCreditAccount As Label = CType(e.Item.FindControl("lblCreditAccount"), Label)
                If Not IsNothing(lblCreditAccount) Then
                    lblCreditAccount.Text = rowValue.Dealer.CreditAccount
                End If

                ' set lblDealerCode
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                If Not IsNothing(lblDealerCode) Then
                    lblDealerCode.Text = rowValue.Dealer.DealerCode
                End If

                ' set lblTransferDate
                Dim lblTransferDate As Label = CType(e.Item.FindControl("lblTransferDate"), Label)
                If Not IsNothing(lblTransferDate) Then
                    lblTransferDate.Text = rowValue.PlanTransferDate.ToString("dd/MM/yyyy")
                End If

                ' set lblPaymentPurpose
                Dim lblPaymentPurpose As Label = CType(e.Item.FindControl("lblPaymentPurpose"), Label)
                If Not IsNothing(lblPaymentPurpose) Then
                    lblPaymentPurpose.Text = "MSP"
                End If

                ' set lblPaymentRegNo
                Dim lblPaymentRegNo As Label = CType(e.Item.FindControl("lblPaymentRegNo"), Label)
                If Not IsNothing(lblPaymentRegNo) Then
                    lblPaymentRegNo.Text = rowValue.RegNumber
                End If

                Dim lblTRNumber As Label = CType(e.Item.FindControl("lblTRNumber"), Label)
                If Not IsNothing(lblTRNumber) Then
                    lblTRNumber.Text = rowValue.TRNumber
                End If

                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                Dim lblDebitChargeNo As Label = CType(e.Item.FindControl("lblDebitChargeNo"), Label)
                Dim lblDebitMemoNo As Label = CType(e.Item.FindControl("lblDebitMemoNo"), Label)
                If Not IsNothing(lblDebitChargeNo) Then
                    Dim MSPRegistrationHistoryIDList As String = String.Empty
                    For Each oTPDetail As MSPExPaymentDetail In rowValue.MSPExPaymentDetails
                        If MSPRegistrationHistoryIDList = String.Empty Then
                            MSPRegistrationHistoryIDList = oTPDetail.MSPExRegistration.ID.ToString
                        Else
                            MSPRegistrationHistoryIDList = MSPRegistrationHistoryIDList & "," & oTPDetail.MSPExRegistration.ID.ToString
                        End If
                    Next

                    If MSPRegistrationHistoryIDList <> String.Empty Then
                        crt = New CriteriaComposite(New Criteria(GetType(MSPExDebitCharge), "MSPExRegistration.ID", MatchType.InSet, "(" + MSPRegistrationHistoryIDList + ")"))
                        crt.opAnd(New Criteria(GetType(MSPExDebitCharge), "RowStatus", MatchType.Exact, 0))
                        arr = New MSPExDebitChargeFacade(User).Retrieve(crt)
                        If arr.Count > 0 Then
                            For Each objMSPDC As MSPExDebitCharge In arr
                                If lblDebitChargeNo.Text = String.Empty Then
                                    lblDebitChargeNo.Text = objMSPDC.DebitChargeNo
                                Else
                                    lblDebitChargeNo.Text = lblDebitChargeNo.Text & ", " & objMSPDC.DebitChargeNo
                                End If

                            Next

                        End If

                        crt = New CriteriaComposite(New Criteria(GetType(MSPExDebitMemo), "MSPExRegistration.ID", MatchType.InSet, "(" + MSPRegistrationHistoryIDList + ")"))
                        crt.opAnd(New Criteria(GetType(MSPExDebitMemo), "RowStatus", MatchType.Exact, 0))

                        arr = New MSPExDebitMemoFacade(User).Retrieve(crt)
                        If arr.Count > 0 Then
                            For Each oMSPDM As MSPExDebitMemo In arr
                                If lblDebitMemoNo.Text = String.Empty Then
                                    lblDebitMemoNo.Text = oMSPDM.DebitMemoNo
                                Else
                                    lblDebitMemoNo.Text = lblDebitMemoNo.Text & ", " & oMSPDM.DebitMemoNo
                                End If
                            Next
                        End If
                    End If
                End If

                'set lblTotalAmount
                Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblTotalAmount"), Label)
                If Not IsNothing(lblTotalAmount) Then
                    lblTotalAmount.Text = rowValue.TotalAmount.ToString("C")
                End If

                ' set lblActualTransferDate
                Dim lblActualTransferDate As Label = CType(e.Item.FindControl("lblActualTransferDate"), Label)
                If Not IsNothing(lblActualTransferDate) Then
                    lblActualTransferDate.Text = If(rowValue.ActualTransferDate <> "1753-01-01 00:00:00.000", rowValue.ActualTransferDate.ToString("dd/MM/yyyy"), "")
                End If

                ' set lblTotalActualAmount
                Dim lblTotalActualAmount As Label = CType(e.Item.FindControl("lblTotalActualAmount"), Label)
                If Not IsNothing(lblTotalActualAmount) Then
                    lblTotalActualAmount.Text = If(rowValue.ActualTotalAmount <> "0.00", rowValue.ActualTotalAmount.ToString("C"), "")
                End If

                ' set lblStatus
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                If Not IsNothing(lblStatus) Then
                    lblStatus.Text = EnumMSPEx.GetStringValue(rowValue.Status)
                    If rowValue.Status = EnumMSPEx.MSPExStatusPayment.Baru And objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        Dim chkSelect As CheckBox = e.Item.FindControl("chkSelect")
                        chkSelect.Enabled = False
                    End If
                End If

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                If rowValue.Status = EnumMSPEx.MSPExStatusPayment.Baru Then
                    ' button edit
                    If Not IsNothing(lbtnEdit) And objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        lbtnEdit.Visible = True
                    End If

                    ' button delete
                    If Not IsNothing(lbtnDelete) And objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        lbtnDelete.Visible = True
                        lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    End If
                Else
                    ' button edit
                    If Not IsNothing(lbtnEdit) And objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        lbtnEdit.Visible = False
                    End If

                    ' button delete
                    If Not IsNothing(lbtnDelete) And objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        lbtnDelete.Visible = False
                        lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    End If
                End If

                ' button view
                Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                If Not IsNothing(lbtnView) Then
                    lbtnView.Visible = True
                End If

                ' button history
                If rowValue.Status = EnumStatusMSP.Status.Proses Or rowValue.Status = EnumStatusMSP.Status.Selesai Then
                    Dim crtHistory As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPaymentHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtHistory.opAnd(New Criteria(GetType(MSPTransferPaymentHistory), "MSPTransferPayment.ID", MatchType.Exact, rowValue.ID))
                    Dim arrHistory As ArrayList = New MSPTransferPaymentHistoryFacade(User).Retrieve(crtHistory)
                    If arrHistory.Count > 0 Then
                        Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
                        If Not IsNothing(lbtnHistory) Then
                            lbtnHistory.Visible = True
                        End If
                    End If

                End If

                If Not _edit Then
                    lbtnEdit.Visible = False
                    lbtnDelete.Visible = False
                End If

            End If
        End If
    End Sub

    Private Sub dtgMSPPaymentList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMSPPaymentList.PageIndexChanged
        dtgMSPPaymentList.SelectedIndex = -1
        dtgMSPPaymentList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgMSPPaymentList.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CreateCriteria()
        dtgMSPPaymentList.CurrentPageIndex = 0
        BindDatagrid(dtgMSPPaymentList.CurrentPageIndex)
    End Sub

    Private Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Select Case ddlProses.SelectedValue
            Case 1 'Validasi
                btnValidasi_Click()
            Case 2 'Konfirmasi
                btnConfirm_Click()
            Case Else
                MessageBox.Show("Silahkan pilih proses terlebih dahulu!")
        End Select
    End Sub

    Private Function btnValidasi_Click()
        Dim strMsg As String = String.Empty
        Dim succesReg As String = String.Empty
        Dim failReg As String = String.Empty
        ' get all checked data
        arr = New ArrayList
        arr = PopulateTrfPaymentSelected(New EnumMSPEx().MSPExStatusPayment.Validasi)

        If arr.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("Pembayaran MSP", "Baru"))
        Else
            For i As Integer = 0 To arr.Count - 1
                objTrfPayment = CType(arr(i), MSPExPayment)
                Dim oldStatus As Integer = objTrfPayment.Status
                objTrfPayment.Status = New EnumMSPEx().MSPExStatusPayment.Validasi
                objTrfPayment.IsValidation = True

                If objLoginDealer.ID <> objTrfPayment.Dealer.ID Then
                    strMsg += "\n" & "Anda tidak punya akses Validasi"
                Else
                    If (New MSPExPaymentFacade(User).Update(objTrfPayment) = -1) Then
                        failReg += objTrfPayment.RegNumber & ", "
                    Else
                        succesReg += objTrfPayment.RegNumber & ", "

                        ' add to history status
                        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                        objStatusChangeHistoryFacade.Insert(22, objTrfPayment.RegNumber, oldStatus, objTrfPayment.Status)
                    End If
                End If

            Next
            BindDatagrid(0)
            If failReg <> String.Empty Then
                strMsg = "Data dengan nomor registrasi " & failReg & " gagal validasi.\n"
            End If
            If succesReg <> String.Empty Then
                strMsg = "Data dengan nomor registrasi " & succesReg & " berhasil divalidasi."
            End If
            If strMsg <> String.Empty Then
                MessageBox.Show(strMsg)
            End If
        End If
    End Function

    Private Function btnConfirm_Click()
        Dim strMsg As String = String.Empty
        Dim succesReg As String = String.Empty
        Dim failReg As String = String.Empty
        ' get all checked data
        arr = New ArrayList
        arr = PopulateTrfPaymentSelected(New EnumMSPEx().MSPExStatusPayment.Konfirmasi)

        If arr.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("Pembayaran MSP", "Validasi"))
        Else
            For i As Integer = 0 To arr.Count - 1
                objTrfPayment = CType(arr(i), MSPExPayment)
                Dim oldStatus As Integer = objTrfPayment.Status
                objTrfPayment.Status = New EnumMSPEx().MSPExStatusPayment.Konfirmasi

                If (New MSPExPaymentFacade(User).Update(objTrfPayment) = -1) Then
                    failReg += objTrfPayment.RegNumber & ", "
                Else
                    succesReg += objTrfPayment.RegNumber & ", "

                    ' add to history status
                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                    objStatusChangeHistoryFacade.Insert(22, objTrfPayment.RegNumber, oldStatus, objTrfPayment.Status)
                End If

            Next
            BindDatagrid(0)
            If failReg <> String.Empty Then
                strMsg = "Data dengan nomor registrasi " & failReg & " gagal konfirmasi.\n"
            End If
            If succesReg <> String.Empty Then
                strMsg = "Data dengan nomor registrasi " & succesReg & " berhasil dikonfirmasi."
            End If
            If strMsg <> String.Empty Then
                MessageBox.Show(strMsg)
            End If
        End If
    End Function

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        ' get all checked data
        arr = New ArrayList
        arr = PopulateTrfPaymentSelected(99)
        If arr.Count > 0 Then
            DoDownload(arr)
        Else
            MessageBox.Show("Tidak ada data yang dipilih.")
        End If
    End Sub

    Protected Sub btnTransfertoSAP_Click(sender As Object, e As EventArgs) Handles btnTransfertoSAP.Click
        TransferToSAP("normal")
    End Sub

    Private Sub dtgMSPPaymentList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMSPPaymentList.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        CreateCriteria()
        BindDatagrid(dtgMSPPaymentList.CurrentPageIndex)
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtCreditAccount.Text = String.Empty
        End If
        txtKodeDealer.Text = String.Empty
        txtPaymentRegNo.Text = String.Empty
        chkRequestDate.Checked = False
        DateFrom.Value = DateTime.Now
        DateTo.Value = DateTime.Now
        lboxStatus.ClearSelection()
        txtNoDebitCharge.Text = String.Empty
        txtNoDebitMemo.Text = String.Empty
        If dtgMSPPaymentList.Items.Count > 0 Then
            CreateCriteria()
            BindDatagrid(0)
        End If
    End Sub

#End Region
End Class