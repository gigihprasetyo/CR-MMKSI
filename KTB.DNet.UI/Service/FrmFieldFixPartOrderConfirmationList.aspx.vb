Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports System.IO
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Text

Public Class FrmFieldFixPartOrderConfirmationList
    Inherits System.Web.UI.Page

    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = Session("DEALER")

        If Not IsPostBack Then
            ViewState("EditID") = Nothing
            ViewState("HiddenField1") = Nothing
            ViewState("CurrentSortColumn") = "RequestDate"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            bindDDLStatus(ddlStatus)
            ClearAll()
            bindGrid(0)
        End If
    End Sub

    Private Sub bindDDLStatus(ByVal varDDLStatus As DropDownList)
        varDDLStatus.Items.Clear()
        varDDLStatus.DataSource = EnumFieldFixPartOrderStatus.RetrieveFieldFixPartOrderStatus()
        varDDLStatus.DataValueField = "Code"
        varDDLStatus.DataTextField = "Desc"
        varDDLStatus.DataBind()
        varDDLStatus.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        varDDLStatus.SelectedIndex = 0
    End Sub

    Private Sub bindGrid(ByVal currentPageIndex As Integer)

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)

        Dim totalRow As Integer = 0
        '-- Retrieve recordset
        Dim arrSPFCO As ArrayList = New SparePartForecastDetailFacade(User).RetrieveActiveList(criterias, currentPageIndex, dtgSentPart.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If arrSPFCO.Count = 0 Then
            '-- Bind and display
            dtgSentPart.DataSource = New ArrayList

            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Field Fix"))
            End If
        Else
            '-- Bind and display
            dtgSentPart.DataSource = arrSPFCO

        End If

        dtgSentPart.VirtualItemCount = totalRow
        dtgSentPart.DataBind()
    End Sub

    Private Sub CreateCriteria(ByRef criterias As CriteriaComposite)
        If txtKdDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKdDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        If txtPartNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastMaster.SparePartMaster.PartNumber", MatchType.InSet, "('" & txtPartNo.Text.Trim().Replace(",", "','") & "')"))
        End If

        If txtNoPO.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastHeader.PoNumber", MatchType.InSet, "('" & txtNoPO.Text.Trim().Replace(",", "','") & "')"))
        End If

        If chkTglReq.Checked Then
            Dim StartProses As New DateTime(CInt(icStartDateReq.Value.Year), CInt(icStartDateReq.Value.Month), CInt(icStartDateReq.Value.Day), 0, 0, 0)
            Dim EndProses As New DateTime(CInt(icEndDateReq.Value.Year), CInt(icEndDateReq.Value.Month), CInt(icEndDateReq.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "RequestDate", MatchType.GreaterOrEqual, Format(StartProses, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "RequestDate", MatchType.LesserOrEqual, Format(EndProses, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkTglKirim.Checked Then
            Dim StartKirim As New DateTime(CInt(icStartDateKirim.Value.Year), CInt(icStartDateKirim.Value.Month), CInt(icStartDateKirim.Value.Day), 0, 0, 0)
            Dim EndKirim As New DateTime(CInt(icEndDateKirim.Value.Year), CInt(icEndDateKirim.Value.Month), CInt(icEndDateKirim.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SendDate", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SendDate", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))
        End If

        If ddlStatus.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        sessionHelper.SetSession("FrmFieldFixPartOrderConfirmationList", criterias)
    End Sub

    Private Sub dtgSentPart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSentPart.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim rowValue As SparePartForecastDetail = CType(e.Item.DataItem, SparePartForecastDetail)
            CType(e.Item.FindControl("lblNo"), Label).Text = (dtgSentPart.CurrentPageIndex * dtgSentPart.PageSize + e.Item.ItemIndex + 1).ToString()
            CType(e.Item.FindControl("lbliKdDealer"), Label).Text = rowValue.SparePartForecastHeader.Dealer.DealerCode
            CType(e.Item.FindControl("lbliPartNo"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartNumber
            CType(e.Item.FindControl("lbliPartName"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartName
            CType(e.Item.FindControl("lblNOPo"), Label).Text = rowValue.SparePartForecastHeader.PoNumber
            CType(e.Item.FindControl("lbliNoBulletin"), Label).Text = rowValue.SparePartForecastMaster.NoBulletinService
            CType(e.Item.FindControl("lblRequestDate"), Label).Text = rowValue.RequestDate.ToString("dd/MM/yyyy")
            CType(e.Item.FindControl("lblRequestAmount"), Label).Text = rowValue.RequestQty.ToString("N0")
            CType(e.Item.FindControl("lblJumlahDisetujui"), Label).Text = rowValue.ApprovedQty.ToString("N0")
            CType(e.Item.FindControl("lbliStatus"), Label).Text = EnumFieldFixPartOrderStatus.GetStringValue(rowValue.Status)
            If rowValue.SendDate.Year > 2000 Then
                CType(e.Item.FindControl("lblPengirimanDate"), Label).Text = rowValue.SendDate.ToString("dd/MM/yyyy")
            End If
            CType(e.Item.FindControl("lblNoAWB"), Label).Text = rowValue.NoAWB

            If rowValue.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Baru, Short) OrElse _
                rowValue.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Batal, Short) Then
                CType(e.Item.FindControl("lnkbtnEdit"), LinkButton).Visible = False
            End If
            CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = False
            If rowValue.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Validasi, Short) Then
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = True
            End If

        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim rowValue As SparePartForecastDetail = CType(e.Item.DataItem, SparePartForecastDetail)
            CType(e.Item.FindControl("lblNoEdit"), Label).Text = (dtgSentPart.CurrentPageIndex * dtgSentPart.PageSize + e.Item.ItemIndex + 1).ToString()
            CType(e.Item.FindControl("lbliKdDealerEdit"), Label).Text = rowValue.SparePartForecastHeader.Dealer.DealerCode
            CType(e.Item.FindControl("lbliPartNoEdit"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartNumber
            CType(e.Item.FindControl("lbliPartNameEdit"), Label).Text = rowValue.SparePartForecastMaster.SparePartMaster.PartName
            CType(e.Item.FindControl("lblNOPoEdit"), Label).Text = rowValue.SparePartForecastHeader.PoNumber
            CType(e.Item.FindControl("lbliNoBulletinEdit"), Label).Text = rowValue.SparePartForecastMaster.NoBulletinService
            CType(e.Item.FindControl("lblRequestDateEdit"), Label).Text = rowValue.RequestDate.ToString("dd/MM/yyyy")
            CType(e.Item.FindControl("lblRequestAmountEdit"), Label).Text = rowValue.RequestQty.ToString("N0")
            CType(e.Item.FindControl("lbliStatusEdit"), Label).Text = EnumFieldFixPartOrderStatus.GetStringValue(rowValue.Status)
            CType(e.Item.FindControl("txtNoAWBEdit"), TextBox).Text = rowValue.NoAWB

            If rowValue.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Validasi, Short) Then
                CType(e.Item.FindControl("lblJumlahDisetujuiEdit"), Label).Visible = False
                CType(e.Item.FindControl("txtJumlahDisetujuiEdit"), TextBox).Text = rowValue.ApprovedQty.ToString("N0")
                CType(e.Item.FindControl("icPengirimanDateEdit"), KTB.DNet.WebCC.IntiCalendar).Visible = False
                CType(e.Item.FindControl("lblPengirimanDateEdit"), Label).Visible = False

            ElseIf rowValue.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Proses, Short) Then
                CType(e.Item.FindControl("lblPengirimanDateEdit"), Label).Visible = False
                CType(e.Item.FindControl("txtJumlahDisetujuiEdit"), TextBox).Visible = False
                CType(e.Item.FindControl("lblJumlahDisetujuiEdit"), Label).Text = rowValue.ApprovedQty.ToString("N0")
                If rowValue.SendDate.Year < 2000 Then
                    CType(e.Item.FindControl("icPengirimanDateEdit"), KTB.DNet.WebCC.IntiCalendar).Value = Date.Now.Date
                Else
                    CType(e.Item.FindControl("icPengirimanDateEdit"), KTB.DNet.WebCC.IntiCalendar).Value = rowValue.SendDate.Date
                End If

            ElseIf rowValue.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Batal, Short) Then
                CType(e.Item.FindControl("lnkbtnEdit"), LinkButton).Visible = False

            Else
                CType(e.Item.FindControl("txtJumlahDisetujuiEdit"), TextBox).Visible = False
                CType(e.Item.FindControl("icPengirimanDateEdit"), KTB.DNet.WebCC.IntiCalendar).Visible = False

                If rowValue.SendDate.Year > 2000 Then
                    CType(e.Item.FindControl("lblPengirimanDateEdit"), Label).Text = rowValue.SendDate.Date.ToString("dd/MM/yyyy")
                End If
                CType(e.Item.FindControl("lblJumlahDisetujuiEdit"), Label).Text = rowValue.ApprovedQty.ToString("N0")

            End If
        End If
    End Sub

    Private Sub dtgSentPart_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSentPart.ItemCommand
        If e.CommandName = "Edit" Then
            dtgSentPart.EditItemIndex = e.Item.ItemIndex
            ViewState("EditID") = e.Item.Cells(0).Text

        ElseIf e.CommandName = "Cancel" Then
            dtgSentPart.EditItemIndex = -1

        ElseIf e.CommandName = "Inactive" Then
            Dim oSparePartForecastDetail As SparePartForecastDetail = New SparePartForecastDetailFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Short))
            oSparePartForecastDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Batal, Short)
            Dim result As Integer = New SparePartForecastDetailFacade(User).Update(oSparePartForecastDetail)

            UpdateHeader(oSparePartForecastDetail.SparePartForecastHeader, result)

            If result > -1 Then
                MessageBox.Show("Pembatalan data berhasil")
            Else
                MessageBox.Show("Pembatalan data gagal")
            End If

        ElseIf e.CommandName = "Save" Then
            Dim oSparePartForecastDetail As SparePartForecastDetail = New SparePartForecastDetailFacade(User).Retrieve(CType(ViewState("EditID"), Integer))
            Dim txtJumlahDisetujuiEdit As TextBox = CType(e.Item.FindControl("txtJumlahDisetujuiEdit"), TextBox)
            Dim ddlStatusEdit As DropDownList = CType(e.Item.FindControl("ddlStatusEdit"), DropDownList)
            Dim icPengirimanDateEdit As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icPengirimanDateEdit"), KTB.DNet.WebCC.IntiCalendar)
            Dim txtNoAWBEdit As TextBox = CType(e.Item.FindControl("txtNoAWBEdit"), TextBox)

            oSparePartForecastDetail.NoAWB = txtNoAWBEdit.Text
            Dim result As Integer = -1

            If oSparePartForecastDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Validasi, Short) Then
                If txtJumlahDisetujuiEdit.Text.Trim.Length = 0 Then
                    MessageBox.Show("Jumlah disetujui masih kosong")
                    Return
                ElseIf CInt(txtJumlahDisetujuiEdit.Text) = 0 Then
                    MessageBox.Show("Jumlah disetujui tidak boleh 0")
                    Return
                End If

                If CInt(txtJumlahDisetujuiEdit.Text) > oSparePartForecastDetail.RequestQty Then
                    MessageBox.Show("Jumlah disetujui melebihi Permintaan")
                    Return
                End If

                Dim oSparePartForecastMaster As SparePartForecastMaster = New SparePartForecastMasterFacade(User).RetrieveByPartNumber(oSparePartForecastDetail.SparePartForecastMaster.SparePartMaster.PartNumber)
                Dim apprvQty As Integer = 0
                If oSparePartForecastMaster.ID > 0 Then
                    If (oSparePartForecastMaster.Stock - CInt(txtJumlahDisetujuiEdit.Text)) < 0 Then
                        If (HiddenField1.Value = "-1") Then
                            ViewState("HiddenField1") = oSparePartForecastDetail.ID
                            MessageBox.Confirm("Stock Part Tersisa " & oSparePartForecastMaster.Stock & "\nApakah Proses akan dibatalkan?", "HiddenField1")
                            Exit Sub
                        End If
                    ElseIf oSparePartForecastMaster.Stock < CInt(txtJumlahDisetujuiEdit.Text) Then
                        If (HiddenField1.Value = "-1") Then
                            ViewState("HiddenField1") = oSparePartForecastDetail.ID
                            MessageBox.Confirm("Stock Part Tersisa " & oSparePartForecastMaster.Stock & "\nApakah Proses akan dibatalkan?", "HiddenField1")
                            Exit Sub
                        End If
                    Else
                        apprvQty = CInt(txtJumlahDisetujuiEdit.Text)
                    End If
                Else
                    MessageBox.Confirm("Stock Part Tersisa 0\nApakah Proses akan dibatalkan?", "HiddenField1")
                    Exit Sub
                End If
                oSparePartForecastDetail.ApprovedQty = apprvQty
                oSparePartForecastDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Proses, Short)
                oSparePartForecastMaster.Stock = (oSparePartForecastMaster.Stock - CInt(txtJumlahDisetujuiEdit.Text))

                Dim oSPFCHeader As SparePartForecastHeader = oSparePartForecastDetail.SparePartForecastHeader
                result = New SparePartForecastDetailFacade(User).UpdateTransaction(oSparePartForecastDetail, oSparePartForecastMaster)

                UpdateHeader(oSparePartForecastDetail.SparePartForecastHeader, result)
            Else
                If oSparePartForecastDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Proses, Short) Then
                    If icPengirimanDateEdit.Value.Date <= oSparePartForecastDetail.RequestDate.Date Then
                        MessageBox.Show("Tanggal pengiriman tidak boleh kurang dari tanggal permintaan")
                        Return
                    End If
                    oSparePartForecastDetail.SendDate = icPengirimanDateEdit.Value.Date
                    oSparePartForecastDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Kirim, Short)
                End If
                result = New SparePartForecastDetailFacade(User).Update(oSparePartForecastDetail)

                UpdateHeader(oSparePartForecastDetail.SparePartForecastHeader, result)
            End If
            dtgSentPart.EditItemIndex = -1

            If result > -1 Then
                MessageBox.Show("Simpan data berhasil")
            Else
                MessageBox.Show("Simpan data gagal")
            End If
            ViewState("EditID") = Nothing

        End If
        bindGrid(dtgSentPart.CurrentPageIndex)
    End Sub

    Private Sub UpdateHeader(ByVal oSparePartForecastHeader As SparePartForecastHeader, ByRef result As Integer)
        Dim Proc As Integer = 0, Send As Integer = 0, Cancel As Integer = 0
        For Each oDetail As SparePartForecastDetail In oSparePartForecastHeader.SPFDetails
            If oDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Proses, Short) Then
                Proc = Proc + 1
            ElseIf oDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Kirim, Short) Then
                Send = Send + 1
            ElseIf oDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Batal, Short) Then
                Cancel = Cancel + 1
            End If
        Next

        If Proc = oSparePartForecastHeader.SPFDetails.Count Then
            oSparePartForecastHeader.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Proses, Short)
        ElseIf Send = oSparePartForecastHeader.SPFDetails.Count Then
            oSparePartForecastHeader.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Kirim, Short)
        ElseIf Cancel = oSparePartForecastHeader.SPFDetails.Count Then
            oSparePartForecastHeader.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Stok_Kosong, Short)
        Else
            oSparePartForecastHeader.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Pemenuhan_Sebagian, Short)
        End If

        result = New SparePartForecastHeaderFacade(User).Update(oSparePartForecastHeader)
    End Sub

    Protected Sub HiddenField1_ValueChanged(sender As Object, e As EventArgs) Handles HiddenField1.ValueChanged
        If HiddenField1.Value = "1" Then
            dtgSentPart.EditItemIndex = -1
            Dim oSparePartForecastDetail As SparePartForecastDetail = New SparePartForecastDetailFacade(User).Retrieve(CType(ViewState("HiddenField1"), Integer))
            oSparePartForecastDetail.Status = CType(EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Batal, Short)
            Dim result As Integer = New SparePartForecastDetailFacade(User).Update(oSparePartForecastDetail)

            UpdateHeader(oSparePartForecastDetail.SparePartForecastHeader, result)

            bindGrid(dtgSentPart.CurrentPageIndex)

            If result > -1 Then
                MessageBox.Show("Simpan data berhasil")
            Else
                MessageBox.Show("Simpan data gagal")
            End If

            HiddenField1.Value = "-1"
            ViewState("HiddenField1") = Nothing
            ViewState("EditID") = Nothing
        ElseIf HiddenField1.Value = "0" Then
            dtgSentPart.EditItemIndex = -1
            ViewState("HiddenField1") = Nothing
            ViewState("EditID") = Nothing
            HiddenField1.Value = "-1"
            'Response.Redirect("FrmFieldFixStockControl.aspx?Mode=New")
            bindGrid(dtgSentPart.CurrentPageIndex)
        End If
    End Sub

    Private Sub ClearAll()
        ViewState("EditID") = Nothing
        ViewState("HiddenField1") = Nothing
        HiddenField1.Value = "-1"
        txtKdDealer.Text = ""
        txtPartNo.Text = ""
        txtNoPO.Text = ""
        icStartDateReq.Value = Date.Now.Date
        icEndDateReq.Value = Date.Now.Date
        icStartDateKirim.Value = Date.Now.Date
        icEndDateKirim.Value = Date.Now.Date
        ddlStatus.SelectedIndex = 0
        chkTglKirim.Checked = False
        chkTglReq.Checked = True
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Not chkTglReq.Checked Then
            MessageBox.Show("Tanggal Permintaan harus di pilih")
            Exit Sub
        End If
        bindGrid(0)
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
        dtgSentPart.DataSource = New ArrayList
        dtgSentPart.DataBind()
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim arrListToDownload As New ArrayList
        If Not sessionHelper.GetSession("FrmFieldFixPartOrderConfirmationList") Is Nothing Then
            arrListToDownload = New SparePartForecastDetailFacade(User).Retrieve(CType(sessionHelper.GetSession("FrmFieldFixPartOrderConfirmationList"), CriteriaComposite))
        End If
        If arrListToDownload.Count > 0 Then
            DoDownload(arrListToDownload)
        Else
            MessageBox.Show("Tidak ada data yang di download")
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "ListPermintaan" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim FrmFieldFixPartOrderConfirmationList As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(FrmFieldFixPartOrderConfirmationList)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(FrmFieldFixPartOrderConfirmationList, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteSAPKonsumenData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
            Exit Sub
        End Try

        'Response.Write("<script language='javascript'>window.open('../downloadlocal.aspx?file=" & "DataTemp/" & sFileName & ".xls" & "');</script>")

        Response.Redirect("../downloadlocal.aspx?file=" & "DataTemp\" & sFileName & ".xls", False)
    End Sub

    Private Sub WriteSAPKonsumenData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("List Permintaan")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nomor Part" & tab)
            itemLine.Append("Nama Part" & tab)
            itemLine.Append("No. Service Bulletin" & tab)
            itemLine.Append("Tanggal Permintaan" & tab)
            itemLine.Append("Jumlah Permintaan" & tab)
            itemLine.Append("Jumlah Disetujui" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Tanggal Pengiriman" & tab)
            itemLine.Append("No. AWB" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As SparePartForecastDetail In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.SparePartForecastHeader.Dealer.DealerCode.ToString() & tab)
                itemLine.Append(item.SparePartForecastMaster.SparePartMaster.PartNumber & tab)
                itemLine.Append(item.SparePartForecastMaster.SparePartMaster.PartName & tab)
                itemLine.Append(item.SparePartForecastMaster.NoBulletinService & tab)
                If item.RequestDate.Year > 2000 Then
                    itemLine.Append(item.RequestDate.ToString("dd/MM/yyyy" & tab))
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(item.RequestQty.ToString & tab)
                itemLine.Append(item.ApprovedQty.ToString & tab)
                itemLine.Append(EnumFieldFixPartOrderStatus.GetStringValue(item.Status) & tab)
                If item.SendDate.Year > 2000 Then
                    itemLine.Append(item.SendDate.ToString("dd/MM/yyyy" & tab))
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(item.NoAWB.ToString & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub
End Class