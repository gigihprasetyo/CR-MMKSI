Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq
Imports System.IO

Public Class FrmMSPPayment
    Inherits System.Web.UI.Page

    Private _view As Boolean = False
    Private _input As Boolean = False
    Private _edit As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private _strSessMSPTransferPaymentID As String = "MSPTrfPaymentID"
    Private _strSessArrListMSPRegHistory As String = "ArrayListMSPRegHistory"
    Private _strSessStatusInput As String = "StatusInput"
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private objMSPPayment As MSPTransferPayment
    Dim crt As CriteriaComposite
    Dim arr As New ArrayList
    Dim sorts As SortCollection
    Dim strMsg As String = String.Empty
    Dim strMSPMasterID As String = String.Empty
    Dim totalTransfer As Double = 0
    Dim path As String = String.Empty

    Private Sub CheckPrivilege()
        _view = SecurityProvider.Authorize(Context.User, SR.MSPTrfPayment_view_privilege)
        _input = SecurityProvider.Authorize(Context.User, SR.MSPTrfPayment_input_dealer_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.MSPTrfPayment_edit_privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP - Pembayaran MSP")
        End If
        ' dealer bisa input dan edit, mks hanya bisa edit
        btnSave.Visible = _input
        btnSave.Visible = _edit
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        objLoginDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        objUserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsPostBack Then
            FillForm()
        End If
    End Sub

    Private Sub FillForm()
        SetHeaderData()
        Dim idMSPTrfPaymant As Integer = _sessHelper.GetSession(_strSessMSPTransferPaymentID)

        If Not IsNothing(_sessHelper.GetSession(_strSessMSPTransferPaymentID)) Then
            objMSPPayment = New MSPTransferPaymentFacade(User).Retrieve(idMSPTrfPaymant)
            If objMSPPayment.MSPTransferPaymentDetails.Count > 0 Then
                For Each item As MSPTransferPaymentDetail In objMSPPayment.MSPTransferPaymentDetails
                    arr.Add(item.MSPRegistrationHistory)
                Next
            End If

            'update header data if edit or view mode
            lblDealerCodeName.Text = objMSPPayment.Dealer.DealerCode & "/" & objMSPPayment.Dealer.SearchTerm1
            lblCreditAccount.Text = objMSPPayment.Dealer.CreditAccount
            lblCreatedDate.Text = objMSPPayment.CreatedTime.ToString("dd/MM/yyyy")
            lblStatus.Text = CType(objMSPPayment.Status, EnumStatusMSP.Status).ToString
            hdnDealerID.Value = objMSPPayment.Dealer.ID
            If objMSPPayment.Status <> EnumStatusMSP.Status.Baru Then
                lblPaymentRegNo.Text = objMSPPayment.RegNumber
            End If

            If Convert.ToString(_sessHelper.GetSession(_strSessStatusInput)) = "VIEW" Then
                lblSearchParameter.Visible = False
                lblDoubleDot.Visible = False
                lblParameterPencarianPopUp.Visible = False
                btnSave.Visible = False

                txtTransferDate.Visible = False
                lblTransferDate.Visible = True
                lblTransferDate.Text = objMSPPayment.PlanTransferDate.ToString("dd/MM/yyyy")

                If objLoginDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    btnNew.Visible = False
                End If
            End If

            If objLoginDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If lblStatus.Text = "Baru" Or lblStatus.Text = "Batal_Validasi" Then
                    btnValidasi.Visible = True
                End If
            Else
                If lblStatus.Text = "Validasi" Then
                    btnConfirm.Visible = True
                End If
            End If

        End If

        BindDatagrid(arr)
    End Sub

    Private Sub RemoveSession()
        _sessHelper.RemoveSession(_strSessArrListMSPRegHistory)
        _sessHelper.RemoveSession(_strSessMSPTransferPaymentID)
        _sessHelper.RemoveSession(_strSessSearch)
        _sessHelper.RemoveSession(_strSessStatusInput)
    End Sub

    Private Sub BindDatagrid(ByVal arrayList As ArrayList)
        txtChassisNumberList.Value = String.Empty
        lblTotalTransfer.Text = "0"

        dtgMSPPayment.DataSource = arrayList
        dtgMSPPayment.DataBind()

        _sessHelper.SetSession(_strSessArrListMSPRegHistory, arrayList)
    End Sub

    Private Sub SetHeaderData()
        lblDealerCodeName.Text = objLoginDealer.DealerCode & "/" & objLoginDealer.SearchTerm1
        lblCreditAccount.Text = objLoginDealer.CreditAccount
        lblCreatedDate.Text = Date.Now.ToString("dd/MM/yyyy")
        lblStatus.Text = EnumStatusMSP.Status.Baru.ToString()
        hdnDealerID.Value = objLoginDealer.ID
    End Sub

    Protected Sub btnLoaddtFromPopUp_Click(sender As Object, e As EventArgs) Handles btnLoaddtFromPopUp.Click
        Dim strSql As String = String.Empty
        Dim split() As String = txtChassisNumberList.Value.ToString.Split(";")
        If split.Count > 0 Then
            For i As Integer = 0 To split.Count - 1
                strSql += ",'" & split(i) & "'"
            Next
        End If
        If strSql <> String.Empty Then
            crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "ChassisMaster.ChassisNumber", MatchType.InSet, "(" & strSql.Substring(1, strSql.Length - 1) & ")"))
        End If

        arr = New MSPRegistrationFacade(User).Retrieve(crt)
        Dim newArr As ArrayList = CType(_sessHelper.GetSession(_strSessArrListMSPRegHistory), ArrayList)
        If arr.Count > 0 Then
            For Each item As MSPRegistration In arr
                If item.MSPRegistrationHistorys.Count > 0 Then
                    For Each itemDetail As MSPRegistrationHistory In item.MSPRegistrationHistorys
                        If itemDetail.BenefitMasterHeaderID > 0 Then
                            Continue For
                        End If

                        Dim isHavePayment As Boolean = False
                        crt = New CriteriaComposite(New Criteria(GetType(MSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(MSPTransferPaymentDetail), "MSPTransferPayment.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(MSPTransferPaymentDetail), "MSPRegistrationHistory.ID", MatchType.Exact, itemDetail.ID))
                        Dim arrTrfDetail As ArrayList = New MSPTransferPaymentDetailFacade(User).Retrieve(crt)
                        If Not IsNothing(arrTrfDetail) AndAlso arrTrfDetail.Count > 0 Then

                            For Each objTrfdetail As MSPTransferPaymentDetail In arrTrfDetail
                                If itemDetail.ID = objTrfdetail.MSPRegistrationHistory.ID Then
                                    isHavePayment = True
                                End If
                            Next
                        End If
                        If isHavePayment = False Then
                            newArr.Add(itemDetail)
                        End If

                    Next
                End If
            Next
        End If
        BindDatagrid(newArr)

    End Sub

    Private Sub dtgMSPPayment_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMSPPayment.ItemCommand
        If (e.CommandName.ToUpper = "DELETE") Then
            arr = CType(_sessHelper.GetSession(_strSessArrListMSPRegHistory), ArrayList)
            Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
            Dim selectedObjMSPRegHistory As MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(lblMSPRegistrationHistoryID.Text))

            If arr.Count > 0 Then
                txtChassisNumberList.Value = String.Empty
                For x As Integer = arr.Count - 1 To 0 Step -1
                    Dim objMSPRegHistory As MSPRegistrationHistory = CType(arr(x), MSPRegistrationHistory)
                    If objMSPRegHistory.MSPRegistration.ID = selectedObjMSPRegHistory.MSPRegistration.ID Then
                        arr.RemoveAt(x)
                    Else
                        txtChassisNumberList.Value += ";" & objMSPRegHistory.MSPRegistration.ChassisMaster.ChassisNumber
                    End If
                Next
            End If

            BindDatagrid(arr)
        ElseIf (e.CommandName.ToUpper = "DOWNLOADDC") Then
            CommandDownload(e, "DC")
        ElseIf (e.CommandName.ToUpper = "DOWNLOADDM") Then
            CommandDownload(e, "DM")
        End If
    End Sub

    Private Function CommandDownload(ByVal e As DataGridCommandEventArgs, ByVal downloadAs As String)
        Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
        Dim lblDebitChargeNo As Label = CType(e.Item.FindControl("lblDebitChargeNo"), Label)
        Dim selectedObjMSPRegHistory As MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(lblMSPRegistrationHistoryID.Text))

        If downloadAs.ToUpper = "DM" Then
            crt = New CriteriaComposite(New Criteria(GetType(MSPDM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(MSPDM), "MSPDC.DebitChargeNo", MatchType.Exact, lblDebitChargeNo.Text))
            arr = New MSPDMFacade(User).Retrieve(crt)

            If arr.Count > 0 Then
                Dim pathBaseDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("MSPDirectory")
                path = pathBaseDirectory & "\" & CType(arr(0), MSPDM).FileName
            End If
        ElseIf downloadAs.ToUpper = "DC" Then
            crt = New CriteriaComposite(New Criteria(GetType(MSPDC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(MSPDC), "MSPRegistrationHistory.ID", MatchType.Exact, lblMSPRegistrationHistoryID.Text))
            arr = New MSPDCFacade(User).Retrieve(crt)

            If arr.Count > 0 Then
                Dim pathBaseDirectory As String = KTB.DNET.Lib.WebConfig.GetValue("MSPDirectory")
                path = pathBaseDirectory & "\" & CType(arr(0), MSPDC).FileName
            End If
        End If

        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                Dim fileInfo As New FileInfo(path)
                If (fileInfo.Exists) Then
                    Response.Redirect("../Download.aspx?file=" & path)
                    imp.StopImpersonate()
                    imp = Nothing
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Download file tidak berhasil.")
        End Try

    End Function

    Private Sub dtgMSPPayment_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPPayment.ItemDataBound

        If Convert.ToString(_sessHelper.GetSession(_strSessStatusInput)) = "VIEW" Then
            dtgMSPPayment.Columns(dtgMSPPayment.Columns.Count - 1).Visible = False
        End If

        ' set lblNo
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMSPPayment.CurrentPageIndex * dtgMSPPayment.PageSize)
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPRegistrationHistory = CType(e.Item.DataItem, MSPRegistrationHistory)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then

                ' set txtChassisNumberList
                txtChassisNumberList.Value += ";" & rowValue.MSPRegistration.ChassisMaster.ChassisNumber

                ' set MSP Registration History ID
                Dim lblMSPRegistrationHistoryID As Label = CType(e.Item.FindControl("lblMSPRegistrationHistoryID"), Label)
                If Not IsNothing(lblMSPRegistrationHistoryID) Then
                    lblMSPRegistrationHistoryID.Text = rowValue.ID
                End If

                ' set No Rangka
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                If Not IsNothing(lblChassisNumber) Then
                    lblChassisNumber.Text = rowValue.MSPRegistration.ChassisMaster.ChassisNumber
                End If

                ' set dealer code
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                If Not IsNothing(lblDealerCode) Then
                    lblDealerCode.Text = rowValue.MSPRegistration.Dealer.DealerCode
                End If

                ' set MSP No
                Dim lblNoMSP As Label = CType(e.Item.FindControl("lblNoMSP"), Label)
                If Not IsNothing(lblNoMSP) Then
                    lblNoMSP.Text = rowValue.MSPRegistration.MSPCode
                End If

                ' set Request type
                Dim lblRequestType As Label = CType(e.Item.FindControl("lblRequestType"), Label)
                If Not IsNothing(lblRequestType) Then
                    lblRequestType.Text = CType(rowValue.RequestType, EnumStatusMSP.StatusType).ToString
                End If

                ' set lblMSPType
                Dim lblMSPType As Label = CType(e.Item.FindControl("lblMSPType"), Label)
                If Not IsNothing(lblMSPType) Then
                    lblMSPType.Text = rowValue.MSPMaster.MSPType.Description
                End If

                Dim pathBaseDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("MSPDirectory")
                ' set debit charge no
                Dim lblDebitChargeNo As Label = CType(e.Item.FindControl("lblDebitChargeNo"), Label)
                If Not IsNothing(lblDebitChargeNo) Then
                    crt = New CriteriaComposite(New Criteria(GetType(MSPDC), "MSPRegistrationHistory.ID", MatchType.Exact, rowValue.ID))
                    arr = New MSPDCFacade(User).Retrieve(crt)
                    If arr.Count > 0 Then
                        Dim objMSPDC As MSPDC = CType(arr(0), MSPDC)
                        lblDebitChargeNo.Text = objMSPDC.DebitChargeNo

                    End If
                End If

                ' set amount
                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                If Not IsNothing(lblAmount) Then

                    Dim crtMSPDM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPDM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtMSPDM.opAnd(New Criteria(GetType(MSPDM), "MSPDC.MSPRegistrationHistory.ID", MatchType.Exact, rowValue.ID))
                    Dim arrmspdm As ArrayList = New MSPDMFacade(User).Retrieve(crtMSPDM)
                    If arrmspdm.Count > 0 Then
                        Dim amount As Decimal = CType(arrmspdm(0), MSPDM).Amount
                        lblAmount.Text = amount.ToString("C")

                        ' set header data [Total Transfer]
                        totalTransfer += CType(lblAmount.Text, Decimal)
                        lblTotalTransfer.Text = (totalTransfer).ToString("C")
                        lblTotalTransferHdn.Text = totalTransfer
                    End If
                End If

                ' lbtndelete
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                If Not IsNothing(lbtnDelete) Then
                    lbtnDelete.Attributes.Add("OnClick", "return confirm('Perhatian!. Semua data dengan No MSP " & rowValue.MSPRegistration.MSPCode & " akan ikut terhapus.');")
                End If
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim str As String = ValidateData()
        If str = String.Empty Then
            objMSPPayment = New MSPTransferPayment
            objMSPPayment.Dealer = objLoginDealer
            objMSPPayment.PlanTransferDate = CDate(Format(txtTransferDate.Value, "yyyy-MM-dd"))
            objMSPPayment.TotalAmount = lblTotalTransferHdn.Text

            For Each item As DataGridItem In dtgMSPPayment.Items
                Dim objPaymentDetail As New MSPTransferPaymentDetail
                Dim lblMSPRegistrationHistoryID As Label = CType(item.FindControl("lblMSPRegistrationHistoryID"), Label)
                Dim objMSPRegHistory As MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(lblMSPRegistrationHistoryID.Text))

                objPaymentDetail.MSPRegistrationHistory = objMSPRegHistory
                Dim lblAmount As Label = CType(item.FindControl("lblAmount"), Label)
                If Not IsNothing(lblAmount) Then
                    If lblAmount.Text = "" Then
                        MessageBox.Show("Invalid Data")

                        Return
                    End If

                End If
                objPaymentDetail.Amount = If(Not IsNothing(lblAmount), CType(lblAmount.Text, Decimal), 0)
                ' add to MSPPaymentDetail to list
                objMSPPayment.MSPTransferPaymentDetails.Add(objPaymentDetail)
            Next

            Dim int As Integer
            If IsNothing(_sessHelper.GetSession(_strSessMSPTransferPaymentID)) Then
                ' insert
                objMSPPayment.Status = EnumStatusMSP.Status.Baru
                int = New MSPTransferPaymentFacade(User).Insert(objMSPPayment)
                If int > 0 Then
                    MessageBox.Show("Data pembayaran berhasil tersimpan.")
                    btnValidasi.Visible = True
                    ' set id MSP Payment untuk proses update
                    _sessHelper.SetSession(_strSessMSPTransferPaymentID, int)

                    ' add to history status
                    Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                    objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_TransferPayment), "", -1, objMSPPayment.Status)
                End If
            Else
                ' update
                Dim oldMSPPayment As MSPTransferPayment = New MSPTransferPaymentFacade(User).Retrieve(CInt(_sessHelper.GetSession(_strSessMSPTransferPaymentID)))
                oldMSPPayment.Dealer = objLoginDealer
                oldMSPPayment.PlanTransferDate = CDate(Format(txtTransferDate.Value, "yyyy-MM-dd"))
                oldMSPPayment.TotalAmount = lblTotalTransferHdn.Text
                int = New MSPTransferPaymentFacade(User).Update(oldMSPPayment)
                If int > 0 Then
                    ' delete all detail in old data
                    For Each itemOld As MSPTransferPaymentDetail In oldMSPPayment.MSPTransferPaymentDetails
                        Dim facTrfDetail As New MSPTransferPaymentDetailFacade(User)
                        facTrfDetail.Delete(itemOld)
                    Next

                    ' insert all new input detail data
                    For Each itemNew As MSPTransferPaymentDetail In objMSPPayment.MSPTransferPaymentDetails
                        itemNew.MSPTransferPayment = oldMSPPayment
                        int = New MSPTransferPaymentDetailFacade(User).Insert(itemNew)
                    Next
                    MessageBox.Show("Data pembayaran berhasil terupdate.")
                Else
                    MessageBox.Show("Gagal update data pembayaran.")
                End If
            End If
        Else
            MessageBox.Show(str)
        End If
    End Sub

    Private Function ValidateData() As String
        Dim str As String = String.Empty
        Dim now As DateTime = Date.Now().ToString("yyyyy-MM-dd")

        If txtTransferDate.Value = "#12:00:00 AM#" Then
            str += "\n" & "Tanggal transfer tidak boleh kosong."
        ElseIf (CDate(Format(txtTransferDate.Value, "yyyy-MM-dd")) < now) Then
            str += "\n" & "Tanggal transfer tidak boleh kurang dari tanggal saat ini."
        End If

        If dtgMSPPayment.Items.Count < 1 Then
            str += "\n" & "Belum ada no rangka yang dipilih."
        End If

        Return str
    End Function

    Private Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Dim objMSPPayment As MSPTransferPayment = New MSPTransferPaymentFacade(User).Retrieve(CInt(_sessHelper.GetSession(_strSessMSPTransferPaymentID)))

        If Not IsNothing(objMSPPayment) Then
            If (objMSPPayment.Status = EnumStatusMSP.Status.Baru Or objMSPPayment.Status = EnumStatusMSP.Status.Batal_Validasi) Then
                objMSPPayment.Status = EnumStatusMSP.Status.Validasi
                objMSPPayment.IsValidation = True

                If (New MSPTransferPaymentFacade(User).Update(objMSPPayment)) = -1 Then
                    MessageBox.Show("Gagal validasi data pembayaran MSP.")
                    Return
                End If

                ' add to history status
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_TransferPayment), objMSPPayment.RegNumber, -1, objMSPPayment.Status)

                MessageBox.Show("Sukses validasi data pembayaran MSP.")
                _sessHelper.SetSession(_strSessStatusInput, "VIEW")
                _sessHelper.SetSession(_strSessMSPTransferPaymentID, objMSPPayment.ID)
                FillForm()
                btnValidasi.Visible = False
                btnSave.Visible = False

            End If
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveSession()
        Response.Redirect("FrmMSPPaymentList.aspx")
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        RemoveSession()
        Response.Redirect("FrmMSPPayment.aspx")
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim objMSPPayment As MSPTransferPayment = New MSPTransferPaymentFacade(User).Retrieve(CInt(_sessHelper.GetSession(_strSessMSPTransferPaymentID)))

        If Not IsNothing(objMSPPayment) Then
            If (objMSPPayment.Status = EnumStatusMSP.Status.Validasi) Then
                objMSPPayment.Status = EnumStatusMSP.Status.Konfirmasi

                If (New MSPTransferPaymentFacade(User).Update(objMSPPayment)) = -1 Then
                    MessageBox.Show("Gagal validasi data pembayaran MSP.")
                    Return
                End If

                ' add to history status
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_TransferPayment), objMSPPayment.RegNumber, -1, objMSPPayment.Status)

                MessageBox.Show("Sukses validasi data pembayaran MSP.")
                _sessHelper.SetSession(_strSessStatusInput, "VIEW")
                Response.Redirect("FrmMSPPayment.aspx")
            End If
        End If
    End Sub
End Class