#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text
Imports KTB.DNet.Lib
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SAP
Imports System.Text.RegularExpressions
Imports System.Linq
Imports System.Collections.Generic

#End Region

Public Class FrmSPKDaftar
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dtgcari As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents Textbox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents Textbox4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox5 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlSPKMonth1 As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlSPKYear1 As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlSPKMonth2 As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlSPKYear2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipeWarna As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPropinsi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKota As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHarga As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlProses As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents tblOperator As System.Web.UI.HtmlControls.HtmlTable
    'Protected WithEvents ddlFakturMonth1 As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlFakturYear1 As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlFakturMonth2 As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlFakturYear2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSPKRegistered As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSPKDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSetKonsumen As System.Web.UI.WebControls.Button
    Protected WithEvents txt1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRed As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtYellow As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWhiteSmoke As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGainsboro As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkValidate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblStatusFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusFakturSpr As System.Web.UI.WebControls.Label
    Protected WithEvents txtIndentNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlAlokasi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkKonsumen As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlStatusKonsumen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerBranch As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerBranchID As System.Web.UI.WebControls.Label
    Protected WithEvents icDealerSPKDateStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDealerSPKDateEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDealerSPKInputDateStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDealerSPKInputDateEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkFilterDealerSPKDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkFilterDealerSPKInputDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents HFKonsumenConfirmation As System.Web.UI.WebControls.HiddenField
    Protected WithEvents HFKonsumenVerivy As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdConfirm As Global.System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private arlSPKHeader As ArrayList
    Private arlSPK As ArrayList

    Private objSPKHeader As SPKHeader
    Private sessionHelper As New SessionHelper
    Private objUserInfo As UserInfo
    Private objDealer As Dealer
#End Region

#Region "Event handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = sessionHelper.GetSession("DEALER")
        UserPrivilege()
        If Not IsPostBack Then
            Dim objUserInfo As UserInfo = sessionHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtKodeDealer.Attributes.Add("readonly", "readonly")
                txtSalesmanCode.Attributes.Add("readonly", "readonly")
            End If

            BindToDropDownList()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlTipe, ddlKategori.SelectedItem.Text)

            ddlKategori_SelectedIndexChanged(sender, e)
            InitiatePage()
            If GetSessionCriteria() Then
                BindGrid()
            End If
            'SetControlAccess(False)
            txtSalesmanCode.Attributes.Add("readonly", "readonly")

            Dim AutoCustomerStatus As Boolean = False
            'cek transactionControl
            Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(sessionHelper.GetSession("DEALER").ID, CInt(EnumDealerTransType.DealerTransKind.AutoCustomer).ToString)
            If Not (objTransaction Is Nothing) Then
                If objTransaction.Status = 1 Then
                    AutoCustomerStatus = True
                End If
            End If
            sessionHelper.SetSession("AutoCustomerStatus", AutoCustomerStatus)
        Else
            If Me.HFKonsumenConfirmation.Value = "1" Then
                Me.btnSetKonsumen_Click(Nothing, Nothing)
            End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblShowSalesman.Attributes("onClick") = "ShowSalesmanSelection();"
        lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlTipe, ddlKategori.SelectedItem.Text)

        ddlTipeWarna.Enabled = True
        Select Case ddlKategori.SelectedItem.Text
            Case "PC"
                ddlTipeWarna.Items.Clear()
                ddlTipeWarna.Enabled = False
            Case "CV"
                BindDdlTipeWarna("CBU_BODYTYPE1")
            Case "LCV"
                BindDdlTipeWarna("CBU_BODYTYPELCV1")
        End Select

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If Not Page.IsValid Then
            Return
        End If
        dtgcari.CurrentPageIndex = 0
        BindGrid()
        'SetControlAccess(True)
    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        ddlKota.Items.Clear()
        If ddlPropinsi.SelectedIndex <> 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ddlPropinsi.SelectedValue))
            criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
            ddlKota.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
            ddlKota.DataTextField = "CityName".ToUpper
            ddlKota.DataValueField = "ID"
            ddlKota.DataBind()
        End If
        ddlKota.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlKota.SelectedIndex = 0
    End Sub



    Private Sub dtgcari_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgcari.ItemDataBound
        Dim lblKodeKategori As String = String.Empty
        Dim strPemenuhanSebagian As String = "Pemenuhan Sebagian"

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not (arlSPKHeader Is Nothing) Then
                objSPKHeader = arlSPKHeader(e.Item.ItemIndex)
                e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgcari.PageSize * dtgcari.CurrentPageIndex)).ToString

                Dim chkCustReq As CheckBox = CType(e.Item.FindControl("chkCustReq"), CheckBox)
                If objSPKHeader.CustomerRequestID > 0 Then
                    chkCustReq.Checked = True
                Else
                    chkCustReq.Checked = False
                End If
                chkCustReq.Enabled = False

                'Dim EnumStatus As EnumStatusSPK.Status = objSPKHeader.Status
                Dim lblStatusString As Label = CType(e.Item.FindControl("lblStatusString"), Label)
                lblStatusString.Text = EnumStatusSPK.GetStringValueStatus(objSPKHeader.Status)

                Dim lblStatusAlokasi As Label = CType(e.Item.FindControl("lblStatusAlokasi"), Label)
                If objSPKHeader.SPKFakturs.Count > 0 Then
                    Dim qty As Integer = 0
                    For Each spkDtl As SPKDetail In objSPKHeader.SPKDetails
                        If spkDtl.Status <> 1 And spkDtl.Status <> 3 Then
                            qty = qty + spkDtl.Quantity
                        End If
                    Next

                    If Not (objSPKHeader.SPKFakturs Is Nothing) And objSPKHeader.SPKFakturs.Count > 0 Then
                        Dim iQtyC As Integer = 0
                        Dim iQtyNol As Integer = 0
                        For Each objSPKFaktur As SPKFaktur In objSPKHeader.SPKFakturs
                            If Not objSPKFaktur.EndCustomer Is Nothing Then
                                If Not objSPKFaktur.EndCustomer.ChassisMaster Is Nothing Then
                                    If objSPKFaktur.EndCustomer.ChassisMaster.FakturStatus.ToString <> "0" Then
                                        iQtyC = iQtyC + 1
                                    Else
                                        iQtyNol = iQtyNol + 1
                                    End If
                                End If
                            End If
                        Next

                        If (iQtyC < objSPKHeader.SPKFakturs.Count) And (iQtyNol < objSPKHeader.SPKFakturs.Count) Then
                            lblStatusAlokasi.Text = strPemenuhanSebagian
                        Else
                            If (objSPKHeader.SPKFakturs.Count < qty) And (iQtyNol < objSPKHeader.SPKFakturs.Count) Then
                                lblStatusAlokasi.Text = strPemenuhanSebagian
                            End If
                        End If
                    End If
                End If

                Dim lblDealerSPKInputDate As Label = CType(e.Item.FindControl("lblDealerSPKInputDate"), Label)
                If Not IsNothing(lblDealerSPKInputDate) Then
                    lblDealerSPKInputDate.Text = objSPKHeader.CreatedTime.ToString("dd-MM-yyyy")
                End If
                Dim lblDealerSPKDate As Label = CType(e.Item.FindControl("lblDealerSPKDate"), Label)
                If Not IsNothing(lblDealerSPKInputDate) Then
                    lblDealerSPKDate.Text = objSPKHeader.DealerSPKDate.ToString("dd-MM-yyyy")
                End If

                'e.Item.Cells(10).Text = objSPKHeader.CreatedTime.ToString("dd-MM-yyyy")
                'e.Item.Cells(11).Text = objSPKHeader.DealerSPKDate.ToString("dd-MM-yyyy")
                'e.Item.Cells(7).Text = objSPKHeader.PlanDeliveryMonth.ToString() + "-" + objSPKHeader.PlanDeliveryYear.ToString() 'Periode Pengajuan SPK
                'e.Item.Cells(8).Text = objSPKHeader.CreatedTime.Month.ToString() + "-" + objSPKHeader.CreatedTime.Year.ToString() 'Periode Pengajuan SPK
                'e.Item.Cells(9).Text = objSPKHeader.PlanInvoiceMonth.ToString() + "-" + objSPKHeader.PlanInvoiceYear.ToString() 'Rencana Pengajuan Faktur
                Dim _category As String = String.Empty
                Dim lblCategoryCode As Label = CType(e.Item.FindControl("lblCategoryCode"), Label)
                If Not IsNothing(lblCategoryCode) Then
                    For Each detail As SPKDetail In objSPKHeader.SPKDetails
                        If ddlKategori.SelectedIndex > 0 Then
                            If detail.Category.CategoryCode = ddlKategori.SelectedItem.Text Then
                                lblCategoryCode.Text = detail.Category.CategoryCode
                            End If
                        Else
                            If Not (_category.IndexOf(detail.Category.CategoryCode) >= 0) Then
                                _category = _category & IIf(_category.Trim = String.Empty, "", ",") & detail.Category.CategoryCode
                            End If
                            lblCategoryCode.Text = _category
                        End If
                    Next
                End If

                Dim strUnitAmount As String = GetTotalUnitAmountSPK(objSPKHeader)
                Dim arr As String() = strUnitAmount.Split(";")
                Dim lblTotalUnit As Label = CType(e.Item.FindControl("lblTotalUnit"), Label)
                If Not IsNothing(lblTotalUnit) Then
                    lblTotalUnit.Text = FormatNumber(CType(arr(0), Integer), 0, TriState.UseDefault, TriState.True, TriState.UseDefault)
                End If
                'e.Item.Cells(14).Text = FormatNumber(CType(arr(0), Integer), 0, TriState.UseDefault, TriState.True, TriState.UseDefault)
                'If Not objSPKHeader.SPKCustomer Is Nothing Then
                '    e.Item.Cells(13).Text = objSPKHeader.SPKCustomer.City.Province.ProvinceName 'Propinsi
                'Else
                '    e.Item.Cells(13).Text = String.Empty 'Propinsi
                'End If
                'If Not objSPKHeader.SPKCustomer Is Nothing Then
                '    e.Item.Cells(14).Text = objSPKHeader.SPKCustomer.City.CityName 'Kota
                'Else
                '    e.Item.Cells(15).Text = String.Empty 'Kota
                'End If

                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                If Not IsNothing(lblDealer) Then
                    lblDealer.ToolTip = objSPKHeader.Dealer.SearchTerm1
                End If

                Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
                lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistory.aspx?DocType=" & LookUp.DocumentType.Surat_Pesanan_Kendaraan & "&DocNumber=" & objSPKHeader.SPKNumber, "", 400, 400, "DealerSelection")

                Dim linkbtnEdit As LinkButton = e.Item.FindControl("lbnEdit")
                linkbtnEdit.Text = "<img src=""../images/Edit.gif"" border=""0"" alt=""Ubah"">"

                Dim linkbtnView As LinkButton = e.Item.FindControl("lbnView")
                linkbtnView.Text = "<img src=""../images/detail.gif"" border=""0"" alt=""Lihat"">"

                Dim lbnProfile As LinkButton = e.Item.FindControl("lbnProfile")
                lbnProfile.Text = "<img src=""../images/dok.gif"" border=""0"" alt=""Profile"">"

                Dim lnkFile As LinkButton = e.Item.FindControl("lnkFile")
                If objSPKHeader.EvidenceFile.Trim = "" Then
                    lnkFile.Visible = False
                End If

                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    linkbtnEdit.Visible = False
                    linkbtnView.Visible = True
                    lbnProfile.Visible = False
                Else
                    If objSPKHeader.Dealer.DealerGroup.ID <> objDealer.DealerGroup.ID Then
                        linkbtnEdit.Visible = False
                        linkbtnView.Visible = True
                        lbnProfile.Visible = False
                    Else
                        'If ((objSPKHeader.Status <> EnumStatusSPK.Status.Closed) _
                        '    AndAlso (objSPKHeader.Status <> EnumStatusSPK.Status.Selesai)) _
                        '    OrElse ((Not (objSPKHeader.Dealer Is Nothing)) _
                        '    AndAlso (objSPKHeader.Dealer.DealerGroup.ID <> objDealer.DealerGroup.ID)) Then
                        'If ((objSPKHeader.Status = EnumStatusSPK.Status.Closed) OrElse (objSPKHeader.Status = EnumStatusSPK.Status.Batal) OrElse (objSPKHeader.Status = EnumStatusSPK.Status.Selesai)) _
                        If ((objSPKHeader.Status = EnumStatusSPK.Status.Batal) OrElse (objSPKHeader.Status = EnumStatusSPK.Status.Selesai)) _
                            OrElse ((Not (objSPKHeader.Dealer Is Nothing)) AndAlso (objSPKHeader.Dealer.DealerGroup.ID <> objDealer.DealerGroup.ID)) Then
                            lbnProfile.Visible = False
                            linkbtnEdit.Visible = False
                            linkbtnView.Visible = True
                        Else
                            linkbtnEdit.Visible = True
                            linkbtnView.Visible = False
                            lbnProfile.Visible = True
                        End If
                    End If
                End If

                If (objSPKHeader.FlagUpdate = 1) AndAlso Not (objSPKHeader.Status = EnumStatusSPK.Status.Batal Or objSPKHeader.Status = EnumStatusSPK.Status.Selesai) Then
                    e.Item.BackColor = System.Drawing.Color.Yellow
                End If

                'Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criteria.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CType(LookUp.DocumentType.Surat_Pesanan_Kendaraan, Integer)))
                'criteria.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, objSPKHeader.SPKNumber))

                'Dim sortColl As SortCollection = New SortCollection
                'sortColl.Add(New Sort(GetType(StatusChangeHistory), "CreatedTime", Sort.SortDirection.DESC))

                'Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                'Dim arlStatusHistory As ArrayList = objStatusChangeHistoryFacade.Retrieve(criteria, sortColl)
                'If arlStatusHistory.Count > 0 Then
                '    Dim objStatusChangeHistory As StatusChangeHistory = arlStatusHistory(0)
                '    Dim diffDay As Integer = DateDiff(DateInterval.Day, objStatusChangeHistory.CreatedTime, Date.Now)
                '    If diffDay >= 7 And diffDay <= 14 Then
                '        If Not (objSPKHeader.Status = EnumStatusSPK.Status.Selesai Or objSPKHeader.Status = EnumStatusSPK.Status.Batal) Then
                '            e.Item.BackColor = System.Drawing.Color.Yellow
                '        End If
                '    ElseIf diffDay > 14 Then
                '        If Not (objSPKHeader.Status = EnumStatusSPK.Status.Selesai Or objSPKHeader.Status = EnumStatusSPK.Status.Batal) Then
                '            e.Item.BackColor = System.Drawing.Color.Red
                '        End If
                '    End If
                'End If

                'If CType(objSPKHeader.Status, Integer) = EnumStatusSPK.Status.Selesai Then
                '    If objSPKHeader.CustomerRequestID > 0 Then
                '        e.Item.BackColor = System.Drawing.Color.LightGreen
                '    Else
                '        e.Item.BackColor = System.Drawing.Color.Orange
                '    End If
                'End If
                'If e.Item.BackColor.ToString = System.Drawing.Color.White.ToString Then
                '    If Not IsNothing(objSPKHeader.SPKCustomer) AndAlso objSPKHeader.SPKCustomer.ID > 0 Then
                '        If objSPKHeader.SPKCustomer.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP OrElse objSPKHeader.SPKCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP Then
                '            e.Item.BackColor = System.Drawing.Color.Gainsboro
                '        End If
                '    End If
                'End If

                'start rudi
                Dim ddlRejectedReason As DropDownList = CType(e.Item.FindControl("ddlRejectedReason"), DropDownList)
                If Not IsNothing(ddlRejectedReason) Then
                    BindDDLRejectedReason(ddlRejectedReason, objSPKHeader.RejectedReason)
                    ddlRejectedReason.SelectedValue = objSPKHeader.RejectedReason
                    Dim iStatus As Integer = CType(e.Item.FindControl("lblStatus"), Label).Text
                    If iStatus = EnumStatusSPK.Status.Batal Then
                        ddlRejectedReason.Enabled = False
                    End If
                End If
                'end rudi

                Dim lblTotalKonsumenFaktur As Label = CType(e.Item.FindControl("lblTotalKonsumenFaktur"), Label)
                Dim lblTotalJadiKonsumen As Label = CType(e.Item.FindControl("lblTotalJadiKonsumen"), Label)

                Dim TotKonsumenFaktur As Integer = 0
                Dim TotJadiKonsumen As Integer = 0
                For Each oSPKDetail As SPKDetail In objSPKHeader.SPKDetails
                    For Each oSPKDetailCustomer As SPKDetailCustomer In oSPKDetail.SPKDetailCustomers
                        If oSPKDetail.Status <> 1 Then
                            TotKonsumenFaktur += 1
                            If Not IsNothing(oSPKDetailCustomer.CustomerRequest) Then
                                TotJadiKonsumen += 1
                            End If
                        End If
                    Next
                Next
                lblTotalKonsumenFaktur.Text = TotKonsumenFaktur
                lblTotalJadiKonsumen.Text = TotJadiKonsumen

            End If

        End If
    End Sub

    Private Sub dtgcari_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgcari.ItemCommand
        sessionHelper.SetSession("PrevPage", Request.Url.ToString())
        SetSessionCriteria()
        If e.CommandName = "Edit" Then
            Response.Redirect("FrmSPKHeader.aspx?Id=" & e.Item.Cells(1).Text & "&Mode=1&isBack=0")
        ElseIf e.CommandName = "View" Then
            Response.Redirect("FrmSPKHeader.aspx?Id=" & e.Item.Cells(1).Text & "&Mode=2&isBack=0")
        ElseIf e.CommandName = "Profile" Then
            Response.Redirect("FrmSPKHeaderProfile.aspx?Id=" & e.Item.Cells(1).Text)
        ElseIf e.CommandName = "DownloadFile" Then
            Dim objSPKHeader As SPKHeader = New SPKHeaderFacade(User).Retrieve(CInt(e.Item.Cells(1).Text))
            If Not (objSPKHeader Is Nothing) Then
                Dim fileInfox As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & objSPKHeader.EvidenceFile.Trim)
                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & objSPKHeader.EvidenceFile.Trim)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail("Download file SPK gagal"))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If
            End If
        End If

    End Sub

    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function

    Private Sub dtgcari_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgcari.SortCommand
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

        dtgcari.SelectedIndex = -1
        dtgcari.CurrentPageIndex = 0
        BindDataToGrid(dtgcari.CurrentPageIndex)
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Dim listSPK As ArrayList = New ArrayList
        Dim iStatus As Integer
        Dim iProfileChecking As Boolean = False
        Dim arrPreStatus As New ArrayList

        If CInt(ddlProses.SelectedValue) <= 0 Then
            MessageBox.Show("Pilih status terlebih dahulu")
            Exit Sub
        End If

        If ddlProses.SelectedValue = EnumStatusSPK.Status.Batal Then
            Dim confMsg As String = String.Empty
            If hdConfirm.Value = "-1" Then
                If IsSPKMatching(confMsg) Then
                    RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnProses');</script>", confMsg))
                    Return
                End If
            Else
                hdConfirm.Value = "-1"
            End If
        End If

        iStatus = CInt(ddlProses.SelectedValue)
        arrPreStatus = GetPreStatus(iStatus)
        listSPK = PopulateSPKToProcess(arrPreStatus, iStatus)

        If listSPK Is Nothing OrElse listSPK.Count = 0 Then
            Dim preStatusMsg As String = String.Empty
            For Each item As Integer In arrPreStatus
                If item <> EnumStatusSPK.Status.Pending_Konsumen Then
                    preStatusMsg = preStatusMsg & "- " & EnumStatusSPK.GetStringValueStatus(item) & "\n"
                End If
            Next
            MessageBox.Show("Syarat status " & EnumStatusSPK.GetStringValueStatus(iStatus) & " adalah : \n" & preStatusMsg)
            'Exit Sub
        Else
            Dim listSPKFilter As ArrayList = New ArrayList
            Dim listSPKYana As ArrayList = New ArrayList

            For Each objListSPK As SPKHeader In listSPK
                Dim isSPKValid As Boolean = True
                Select Case iStatus
                    Case 3
                        iProfileChecking = False
                        If iStatus = EnumStatusSPK.Status.Batal Then
                            'For Each objListSPK As SPKHeader In listSPK
                            If objListSPK.RejectedReason.Trim = "" Then
                                MessageBox.Show("Alasan pembatalan harap diisi")
                                isSPKValid = False
                                'Exit Sub
                            End If

                            If objListSPK.SPKFakturs.Count > 0 Then
                                MessageBox.Show("Proses Batal gagal. SPK sudah dibuatkan faktur.")
                                isSPKValid = False
                                'Exit Sub
                            End If
                            'Next
                        End If
                    Case 4, 5, 6
                        iProfileChecking = False
                    Case Else
                        iProfileChecking = True
                End Select

                'validasi tunggu unit dan status selanjutnya,  tidak boleh ke status yang sudah pernah diambil.
                'tambahan validasi dalam 1 bln tudak boleh ubah dari tunggu unit x ke tunggu unit x+1
                'For Each objListSPK As SPKHeader In listSPK
                If iStatus >= EnumStatusSPK.Status.Tunggu_Unit Then 'If iStatus >= EnumStatusSPK.Status.Tunggu_Unit Then

                    '--add code by rudi
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(StatusChangeHistory), "CreatedTime", Sort.SortDirection.ASC))
                    Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, objListSPK.SPKNumber))
                    Dim objFacade2 As StatusChangeHistoryFacade = New StatusChangeHistoryFacade(User)
                    Dim arlStatus2 As ArrayList = objFacade2.Retrieve(criterias2, sortColl)
                    If Not IsNothing(arlStatus2) AndAlso arlStatus2.Count > 0 Then
                        Dim objStatusChangeHistory As StatusChangeHistory = CType(arlStatus2(arlStatus2.Count - 1), StatusChangeHistory)
                        If objStatusChangeHistory.NewStatus = EnumStatusSPK.Status.Pending_Konsumen Or objListSPK.Status = EnumStatusSPK.Status.Pending_Konsumen Then
                            Dim objStatusChangeHistory2 As StatusChangeHistory
                            For i As Integer = (arlStatus2.Count - 1) To 0 Step -1
                                objStatusChangeHistory2 = CType(arlStatus2(i), StatusChangeHistory)
                                If objStatusChangeHistory2.NewStatus <> EnumStatusSPK.Status.Pending_Konsumen AndAlso objStatusChangeHistory2.NewStatus >= EnumStatusSPK.Status.Tunggu_Unit Then
                                    '--Jump to next code
                                    Exit For
                                Else
                                    objStatusChangeHistory2 = Nothing
                                End If
                            Next

                            If Not objStatusChangeHistory2 Is Nothing Then
                                If iStatus <> objStatusChangeHistory2.NewStatus Then
                                    If objStatusChangeHistory.CreatedTime.ToString("yyyyMM") = Date.Now.ToString("yyyyMM") Then
                                        If iStatus < objStatusChangeHistory2.NewStatus Then
                                            MessageBox.Show("Update status gagal ke status " & EnumStatusSPK.GetStringValueStatus(iStatus) & ". \nSebelumnya sudah pernah di set status " & EnumStatusSPK.GetStringValueStatus(objStatusChangeHistory2.NewStatus))
                                            isSPKValid = False
                                            'Exit Sub
                                        End If
                                    ElseIf Date.Now.ToString("yyyyMM") > objStatusChangeHistory.CreatedTime.ToString("yyyyMM") Then
                                        If iStatus > (objStatusChangeHistory2.NewStatus + 1) Then
                                            MessageBox.Show("Update status gagal. \nSyarat SPK " & EnumStatusSPK.GetStringValueStatus(iStatus) & ", sudah pernah di set status " & EnumStatusSPK.GetStringValueStatus(iStatus - 1))
                                            isSPKValid = False
                                            'Exit Sub
                                        ElseIf iStatus < objStatusChangeHistory2.NewStatus Then
                                            MessageBox.Show("Update status gagal ke status " & EnumStatusSPK.GetStringValueStatus(iStatus) & ". \nSebelumnya sudah pernah di set status " & EnumStatusSPK.GetStringValueStatus(objStatusChangeHistory2.NewStatus))
                                            isSPKValid = False
                                            'Exit Sub
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                    '-- end by rudi


                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, objListSPK.SPKNumber))
                    Dim objFacade As StatusChangeHistoryFacade = New StatusChangeHistoryFacade(User)
                    Dim arlStatus As ArrayList = objFacade.Retrieve(criterias, sortColl)
                    If Not IsNothing(arlStatus) AndAlso arlStatus.Count > 0 Then
                        Dim iMaxStatus As Integer = 0
                        Dim iMaxDate As DateTime = New DateTime(1900, 1, 1)
                        Dim iPreStatus As Integer = 0
                        Dim iPreStatus2 As Integer = 0
                        For Each status As StatusChangeHistory In arlStatus
                            'If (status.NewStatus >= EnumStatusSPK.Status.Tunggu_Unit) AndAlso (iStatus = status.NewStatus) Then
                            '    MessageBox.Show("Update status gagal. Ada SPK yang sudah pernah di set status " & EnumStatusSPK.GetStringValueStatus(iStatus))
                            '    Exit Sub
                            'End If
                            If (iStatus > EnumStatusSPK.Status.Tunggu_Unit) AndAlso (status.NewStatus = iStatus - 1) Then
                                iPreStatus = iPreStatus + 1
                            End If
                            'If (iStatus >= EnumStatusSPK.Status.Tunggu_Unit) AndAlso (status.NewStatus >= EnumStatusSPK.Status.Tunggu_Unit) Then
                            If (iStatus >= EnumStatusSPK.Status.Tunggu_Unit) Then
                                If (iStatus > status.NewStatus) Then
                                    If status.CreatedTime.ToString("yyyyMM") = Date.Now.ToString("yyyyMM") Then
                                        iPreStatus2 = iPreStatus2 + 1
                                    End If
                                End If
                            End If
                            If status.NewStatus > iMaxStatus Then
                                iMaxStatus = status.NewStatus
                                iMaxDate = status.CreatedTime
                            End If
                        Next

                        If (iStatus > EnumStatusSPK.Status.Tunggu_Unit) AndAlso (iPreStatus = 0) Then
                            MessageBox.Show("Update status gagal. Syarat SPK " & EnumStatusSPK.GetStringValueStatus(iStatus) & ", sudah pernah di set status " & EnumStatusSPK.GetStringValueStatus(iStatus - 1))
                            isSPKValid = False
                            'Exit Sub
                        End If
                        If iMaxDate <> New DateTime(1900, 1, 1) Then
                            If iMaxDate.ToString("yyyyMM") = Date.Now.ToString("yyyyMM") Then
                                If (iMaxStatus > EnumStatusSPK.Status.Pending_Konsumen) AndAlso (iStatus > iMaxStatus) Then
                                    MessageBox.Show("Update status gagal.\n Perubahan status SPK dari 'Tunggu Unit' - 'Tunggu Unit (VII)' tidak boleh pada bulan yang sama.")
                                    isSPKValid = False
                                    'Exit Sub
                                End If
                                If (iStatus > EnumStatusSPK.Status.Pending_Konsumen) AndAlso (iStatus = iMaxStatus - 1) Then
                                    MessageBox.Show("Update status gagal.\n Perubahan status SPK dari 'Tunggu Unit' - 'Tunggu Unit (VII)' tidak boleh pada bulan yang sama.")
                                    isSPKValid = False
                                    'Exit Sub
                                End If
                            End If
                        End If
                        'If (iStatus > EnumStatusSPK.Status.Tunggu_Unit) AndAlso (iPreStatus2 > 0) Then
                        '    MessageBox.Show("Update status gagal.\n Perubahan status SPK dari 'Tunggu Unit' - 'Tunggu Unit (V)' tidak boleh pada bulan yang sama.\n Perubahan status hanya diperbolehkan ke status SPK sebelum 'Pending Konsumen'")
                        '    Exit Sub
                        'End If

                    End If
                End If
                'Next

                If iProfileChecking Then
                    Dim msg As New StringBuilder
                    'For Each spkHeader As SPKHeader In listSPK
                    'For Each spkDetail As SPKDetail In objListSPK.SPKDetails
                    '    If spkDetail.SPKProfiles.Count = 0 Then
                    '        msg.Append(spkDetail.SPKHeader.SPKNumber.ToString & "\n")
                    '    End If
                    'Next
                    ''Next
                    'If msg.Length > 0 Then
                    '    MessageBox.Show("Nomor SPK berikut : \n" & msg.ToString & " belum ada profile. Mohon diupdate profile terlebih dahulu")
                    '    isSPKValid = False
                    '    'Exit Sub
                    'End If

                    For Each spkDetail As SPKDetail In objListSPK.SPKDetails
                        For Each oSPKDetailCust As SPKDetailCustomer In spkDetail.SPKDetailCustomers
                            If oSPKDetailCust.SPKDetailCustomerProfiles.Count = 0 Then
                                msg.Append(spkDetail.SPKHeader.SPKNumber.ToString & "\n")
                            End If
                        Next
                    Next
                    'Next
                    If msg.Length > 0 Then
                        MessageBox.Show("Nomor SPK berikut : \n" & msg.ToString & " belum ada profile customer. Mohon diupdate profile customernya terlebih dahulu")
                        isSPKValid = False
                        'Exit Sub
                    End If
                End If

                Dim objSPKHeader As SPKHeader

                '-- Farid additional 20810903
                For x As Integer = 0 To listSPK.Count - 1
                    objSPKHeader = New SPKHeader
                    objSPKHeader = CType(listSPK(x), SPKHeader)
                    If iStatus <> objSPKHeader.IsSend Then
                        CType(listSPK(x), SPKHeader).IsSend = 0
                    End If
                Next
                '-- Farid additional 20810903

                '--Penambahan pengecekan isSPKDnet dan perbandingan GoliveDate dgn CreatedDate
                'For Each objSPKHeaderFilter As SPKHeader In listSPK
                Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, objListSPK.Dealer.ID))
                Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
                Dim arlDealerSystem As ArrayList = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
                For Each objDealerSystem As DealerSystems In arlDealerSystem
                    If objDealerSystem.isSPKDNET Then
                        'listSPKFilter.Add(objListSPK)
                    Else
                        If CType(objListSPK.CreatedTime, Date) < objDealerSystem.GoLiveDate Then
                            'listSPKFilter.Add(objListSPK)
                        Else
                            isSPKValid = False
                            MessageBox.Show("SPK " + objListSPK.SPKNumber + " tidak bisa diproses di DNET")
                            'listSPKYana.Add(objListSPK)
                        End If
                    End If
                Next
                '--Penambahan pengecekan isSPKDnet dan perbandingan GoliveDate dgn CreatedDate
                'Next
                If isSPKValid Then
                    listSPKFilter.Add(objListSPK)
                End If
            Next

            Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
            'Dim iReturn As Integer = objSPKHeaderFacade.UpdateSPKStatus(listSPK, iStatus)
            Dim iReturn As Integer = objSPKHeaderFacade.UpdateSPKStatus(listSPKFilter, iStatus)

            If iReturn <> -1 Then
                'RecordStatusChangeHistory(listSPK, iStatus)
                RecordStatusChangeHistory(listSPKFilter, iStatus)
                BindGrid()

                'If iStatus = EnumStatusSPK.Status.Indent AndAlso iReturn <> -1 Then
                '    MessageBox.Show("Proses Registrasi Indent Number berhasil dilakukan")
                'End If

                'Kalau spk batal, Prospek Konsumen status berubah, dari dealer/spk -> Hot Prospek
                'start
                'If iStatus >= EnumStatusSPK.Status.Batal Then
                '    For Each item As SPKHeader In listSPK
                '        If Not IsNothing(item.SPKCustomer.SAPCustomer) AndAlso item.SPKCustomer.SAPCustomer.ID > 0 Then
                '            Dim objCust As SAPCustomer = item.SPKCustomer.SAPCustomer
                '            objCust.Status = EnumSAPCustomerStatus.SAPCustomerStatus.Hot_Prospect

                '            Dim iReturn2 As Integer = New SAPCustomerFacade(User).Update(objCust)

                '            Dim objRespon As SAPCustomerResponse = New SAPCustomerResponse
                '            objRespon.SAPCustomer = item.SPKCustomer.SAPCustomer
                '            objRespon.Status = EnumSAPCustomerStatus.SAPCustomerStatus.Hot_Prospect
                '            objRespon.Description = "Batal SPK"
                '            objRespon.IsSend = 0

                '            Dim iReturn3 As Integer = New SAPCustomerResponseFacade(User).Insert(objRespon)
                '        End If
                '    Next
                'End If
                'end

                If iStatus = EnumStatusSPK.Status.Tunggu_Unit AndAlso iReturn <> -1 Then
                    'remark by anh 20170825 move to simpan SPK
                    'For Each spk As SPKHeader In listSPK
                    '    Dim result As String = String.Empty
                    '    Dim rndGen As RandomGenerator = New RandomGenerator
                    '    result = rndGen.GetActivationCode(8)
                    '    spk.ValidationKey = result.ToUpper
                    'Next
                    'objSPKHeaderFacade.UpdateSPKs(listSPK)
                    'farid 20180606 
                    'SendDataToSF(listSPK)
                End If

            End If

        End If
    End Sub

    Private Function GetNewType() As ArrayList
        Dim _arrNewType As ArrayList
        Try
            Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objappconfig As AppConfig = objfacade.Retrieve("SPKModelCodeFilter")
            If Not IsNothing(objappconfig) Then
                Dim modeCode() As String = objappconfig.Value.Trim.Split(";")
                Dim strCode As String = ""
                For iCode As Integer = 0 To modeCode.Length - 1
                    If strCode = "" Then
                        strCode = "'" & modeCode(iCode) & "'"
                    Else
                        strCode = strCode & ",'" & modeCode(iCode).ToString & "'"
                    End If
                Next
                strCode = "(" & strCode & ")"

                Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.VechileModelCode", MatchType.InSet, strCode))
                _arrNewType = New VechileTypeFacade(User).Retrieve(criterias)


            End If
        Catch ex As Exception

        End Try
        Return _arrNewType
    End Function

    Private Sub SendDataToSF(ByVal arrSPK As ArrayList)
        Try
            Dim _arrNewType As ArrayList = GetNewType()
            For Each _spkheader As SPKHeader In arrSPK
                Dim isSms As Boolean = False
                'Dim msg As String = String.Empty
                Dim vSFreturn As Boolean = False
                Try
                    If Not IsNothing(_spkheader.SPKCustomer.SAPCustomer) Then
                        Dim objFacade As SAPCustomerResponseFacade = New SAPCustomerResponseFacade(User)
                        Dim objresponse As SAPCustomerResponse = New SAPCustomerResponse
                        objresponse.SAPCustomer = _spkheader.SPKCustomer.SAPCustomer
                        objresponse.Status = CInt(EnumSAPCustomerResponse.SAPCustomerResponse.SPK)
                        objresponse.IsSend = 1 'sent
                        Dim iresult As Integer = objFacade.Insert(objresponse)

                        If iresult <> -1 Then
                            Dim sf As SalesForceInterface = New SalesForceInterface()

                            For Each _spkDetail As SPKDetail In _spkheader.SPKDetails
                                If _arrNewType.Count > 0 Then
                                    For Each newType As VechileType In _arrNewType
                                        If newType.VechileTypeCode = _spkDetail.VehicleTypeCode Then
                                            isSms = True
                                            Exit For
                                        End If
                                    Next
                                End If
                            Next

                            If _spkheader.SPKCustomer.SAPCustomer.SalesforceID.Trim <> "" Then
                                'vSFreturn = sf.UpdateOportunity(objresponse, CInt(EnumSAPCustomerResponse.SAPCustomerResponse.SPK), isSms)
                                vSFreturn = sf.UpdateOportunity(_spkheader, isSms)
                            Else
                                vSFreturn = sf.InsertOportunity(_spkheader, isSms)
                            End If

                        End If
                    Else
                        Dim sf As SalesForceInterface = New SalesForceInterface()
                        Dim msg As String = String.Empty
                        vSFreturn = sf.InsertOportunity(_spkheader, isSms)
                    End If
                    If 1 = 1 Then 'If vSFreturn = True Then
                        '_spkheader.IsSend = 1
                        Dim objSPKHeaderFacade As SPKHeaderFacade = New SPKHeaderFacade(User)
                        objSPKHeaderFacade.Update(_spkheader)
                    End If
                Catch ex As Exception

                End Try
            Next
        Catch ex As Exception
            MessageBox.Show("Error : " & ex.Message)
        End Try
    End Sub

    Private Function GetPreStatus(ByVal iStatus As Integer) As ArrayList
        Dim arrPreStatus As New ArrayList
        'Tanda_Jadi 1
        'Lunas 2
        'Batal 3
        'Pending 4
        'Selesai 5
        'Closed 6
        'Indent 7
        'Pending_Konsumen 8
        'Tunggu_Unit 9
        'Tunggu_Unit_I 10
        'Tunggu_Unit_II 11
        'Tunggu_Unit_III 12
        'Tunggu_Unit_IV 13
        'Tunggu_Unit_V 14
        'Tunggu_Unit_VI 15
        'Tunggu_Unit_VII 16
        If (iStatus = 3) Or (iStatus = 8) Then
            arrPreStatus.Add(0) 'EnumStatusSPK.Status.Awal)
            'arrPreStatus.Add(7) 'EnumStatusSPK.Status.Indent)
            'arrPreStatus.Add(1) 'EnumStatusSPK.Status.Tanda_Jadi)
            'arrPreStatus.Add(2) 'EnumStatusSPK.Status.Lunas)
            'arrPreStatus.Add(4) 'EnumStatusSPK.Status.Pending)
            arrPreStatus.Add(9) 'EnumStatusSPK.Status.Tunggu_Unit)
            'arrPreStatus.Add(10) 'EnumStatusSPK.Status.Tunggu_Unit_I)
            'arrPreStatus.Add(11) 'EnumStatusSPK.Status.Tunggu_Unit_II)
            'arrPreStatus.Add(12) 'EnumStatusSPK.Status.Tunggu_Unit_III)
            'arrPreStatus.Add(13) 'EnumStatusSPK.Status.Tunggu_Unit_IV)
            'arrPreStatus.Add(14) 'EnumStatusSPK.Status.Tunggu_Unit_V)
            'arrPreStatus.Add(15) 'EnumStatusSPK.Status.Tunggu_Unit_VI)
            'arrPreStatus.Add(16) 'EnumStatusSPK.Status.Tunggu_Unit_VII)
        End If
        If (iStatus <> 8) Then
            arrPreStatus.Add(8) 'EnumStatusSPK.Status.Pending_Konsumen
        End If
        If (iStatus = 9) Then
            arrPreStatus.Add(0) 'EnumStatusSPK.Status.Awal
            'arrPreStatus.Add(4) 'EnumStatusSPK.Status.Pending
            arrPreStatus.Add(2) 'EnumStatusSPK.Status.Lunas
        End If
        If (iStatus >= 10) Then
            'arrPreStatus.Remove(8)
            arrPreStatus.Add(iStatus - 1)
        End If

        Return arrPreStatus
    End Function


    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlSPKChecked As New ArrayList
        arlSPKChecked = PopulateSPKCheckedList()

        Dim arlSPK As New ArrayList
        arlSPK = GetDataSPKDetail(chkValidate.Checked) 'GetDataSPK()

        If arlSPKChecked.Count <> 0 Then
            Dim arlIntersection As New ArrayList
            If chkValidate.Checked Then
                For Each head As SPKHeader In arlSPKChecked
                    For Each item As V_SPKChassis In arlSPK
                        If head.SPKNumber = item.SPKNumber Then
                            arlIntersection.Add(item)
                            'Exit For           'Note 2018/02/01  di skip untuk download semua data detail
                        End If
                    Next
                Next
            Else
                For Each head As SPKHeader In arlSPKChecked
                    For Each item As V_SPKDetailInfo In arlSPK
                        If head.ID = item.SPKHeaderID Then
                            arlIntersection.Add(item)
                            'Exit For           'Note 2018/02/01  di skip untuk download semua data detail
                        End If
                    Next
                Next
            End If
            DoDownload(arlIntersection, chkValidate.Checked)
        Else
            DoDownload(arlSPK, chkValidate.Checked)
        End If
    End Sub

    Private Sub btnSetKonsumen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetKonsumen.Click
        'Dim listSPKHeader As ArrayList = PopulateSPKHeaderList()
        'If listSPKHeader.Count = 0 Then
        '    Dim msg As String = String.Empty
        '    For i As Integer = CInt(EnumStatusSPK.Status.Tunggu_Unit) To CInt(EnumStatusSPK.Status.Tunggu_Unit_V)
        '        msg = "- " & EnumStatusSPK.GetStringValueStatus(CInt(i)) & "\n " & msg
        '    Next
        '    If msg <> String.Empty Then
        '        MessageBox.Show("Proses jadi konsumen gagal, syarat status jadi konsumen : \n " & msg)
        '    Else
        '        MessageBox.Show(SR.DataNotSelectedByStatus("SPK", "Tidak ada data untuk dijadikan konsumen"))
        '    End If
        '    Exit Sub
        'Else
        '    For Each _header As SPKHeader In listSPKHeader
        '        If _header.CustomerRequestID < 1 Then
        '            CopyToCustomerRequest(_header)
        '        End If
        '    Next
        '    BindGrid()
        'End If       

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim arrListTrue As New System.Collections.ArrayList
        Dim arlCustRequest As New System.Collections.ArrayList
        Dim arrListFalse As New System.Collections.ArrayList
        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
        Dim iFalse As Integer = 0
        Dim i As Integer = 0

        objUserInfo = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        objDealer = sessionHelper.GetSession("DEALER")

        Dim MsgTungguUnit As String = String.Empty
        Dim MsgProfileKendaraan As String = String.Empty
        For Each oDataGridItem In dtgcari.Items
            chkExport = oDataGridItem.FindControl("cbCheck")
            If chkExport.Checked Then
                i = i + 1
                Dim iStatus As Integer = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                Dim SPKNumber As String = CType(oDataGridItem.FindControl("lblSPKNumber"), Label).Text
                If iStatus <> EnumStatusSPK.Status.Tunggu_Unit Then
                    MsgTungguUnit += "SPK " + SPKNumber + " Statusnya belum tunggu unit."
                    Continue For
                End If
                'If Not ((iStatus >= EnumStatusSPK.Status.Tunggu_Unit AndAlso iStatus <= EnumStatusSPK.Status.Tunggu_Unit_VII) Or iStatus <= EnumStatusSPK.Status.Selesai) Then
                If Not ((iStatus >= EnumStatusSPK.Status.Tunggu_Unit) Or iStatus <= EnumStatusSPK.Status.Selesai) Then
                    iFalse = iFalse + 1
                Else
                    Dim _spk As New KTB.DNet.Domain.SPKHeader
                    _spk.ID = oDataGridItem.Cells(1).Text
                    _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text

                    Dim isSPKDValid As Boolean = False
                    For Each dr As SPKDetail In _spk.SPKDetails
                        For Each drr As SPKDetailCustomer In dr.SPKDetailCustomers
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomer.ID", MatchType.Exact, drr.ID))
                            Dim arrSPKProfile As ArrayList = New SPKProfileFacade(User).Retrieve(criterias)

                            If arrSPKProfile.Count > 0 Then
                                If IsNothing(drr.CustomerRequest) OrElse drr.CustomerRequest.ID < 1 Then
                                    isSPKDValid = True
                                    Exit For
                                End If
                            Else
                                MsgProfileKendaraan += "SPK " + SPKNumber + ": Konsumen " + drr.Name1 + "  belum mengisi profile kendaraan. \n"
                                Exit For
                            End If
                        Next
                        If isSPKDValid Then
                            Exit For
                        End If
                    Next
                    If (_spk.Status >= EnumStatusSPK.Status.Tunggu_Unit Or _spk.Status = EnumStatusSPK.Status.Selesai) AndAlso (isSPKDValid) Then
                        _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
                        arrListTrue.Add(_spk)
                    End If
                End If
            End If
        Next

        Dim msg As String = String.Empty
        'For iStatus As Integer = CInt(EnumStatusSPK.Status.Tunggu_Unit) To CInt(EnumStatusSPK.Status.Tunggu_Unit_VII)
        '    msg = "- " & EnumStatusSPK.GetStringValueStatus(CInt(iStatus)) & "\n " & msg
        'Next
        msg = "- " & EnumStatusSPK.GetStringValueStatus(CInt(EnumStatusSPK.Status.Tunggu_Unit))
        If i = iFalse Then
            MessageBox.Show("Proses jadi konsumen gagal, syarat status jadi konsumen : \n " & msg & " \n & Mengisi Detail Konsumen Faktur")
            Exit Sub
        End If

        If MsgTungguUnit <> String.Empty Then
            MessageBox.Show(MsgTungguUnit)
            Exit Sub
        End If

        If MsgProfileKendaraan <> String.Empty Then
            MessageBox.Show(MsgProfileKendaraan)
            Exit Sub
        End If

        If Not GetKonsumenConfirmation(IsNothing(sender)) Then
            Exit Sub
        End If

        If ValidateSave() Then
            Dim UpdateMsg As String = String.Empty
            Dim arrGenerateFile As ArrayList = New ArrayList

            If arrListTrue.Count > 0 Then
                For Each _header As SPKHeader In arrListTrue
                    'If 1 = 1 Then
                    CopyToCustomerRequest(_header, arlCustRequest, UpdateMsg)
                    If arlCustRequest.Count > 0 Then
                        For Each oCustReq As CustomerRequest In arlCustRequest
                            arrGenerateFile.Add(oCustReq)
                        Next
                    End If
                    ' End If
                Next

                If UpdateMsg <> String.Empty Then
                    UpdateMsg = UpdateMsg.ToString().Trim()
                    MessageBox.Show(UpdateMsg)
                End If

                Dim _fileHelper As New FileHelper
                '--------------------------------------------------------------------
                If arrGenerateFile.Count > 0 Then
                    'by pass to transfer process to SAP if status = validasi and transaction control = aktif
                    If CType(sessionHelper.GetSession("AutoCustomerStatus"), Boolean) = True Then
                        _fileHelper.TransferProcess(arrGenerateFile, objUserInfo, UpdateMsg)

                        If UpdateMsg <> String.Empty Then
                            MessageBox.Show(UpdateMsg)
                        End If
                    End If
                End If
                '--------------------------------------------------------------------

                BindGrid()
            End If

            If iFalse > 0 And iFalse < i Then
                MessageBox.Show("Sebagian proses jadi konsumen gagal, syarat status jadi konsumen : \n " & msg)
            End If
        End If

    End Sub

#End Region

#Region "Custom Method"

    Private Sub UpdateStatusCustomerRequest(objCustomerRequest As CustomerRequest, arlCustRequest As ArrayList)
        Dim NoKTP As String, NoTelp As String

        'Cek status. Jika status <> Proses & autoCustomer = Aktif
        If objCustomerRequest.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses And CType(sessionHelper.GetSession("AutoCustomerStatus"), Boolean) = True Then
            MessageBox.Show("Data telah ditransfer")
            Exit Sub
        Else
            CommonFunction.GetKTPAndPhone(objCustomerRequest, NoKTP, NoTelp)
            If NoKTP = String.Empty OrElse NoKTP Is Nothing Then
                MessageBox.Show("No KTP konsumen ini tidak ada")
                Exit Sub
            End If

            Dim _oldStatus = objCustomerRequest.Status
            objCustomerRequest.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi
            Dim nResult As Integer = New CustomerRequestFacade(User).Update(objCustomerRequest)
            If nResult <> -1 Then
                sessionHelper.SetSession("CustomerRequest", New CustomerRequestFacade(User).Retrieve(nResult))
                objCustomerRequest = CType(sessionHelper.GetSession("CustomerRequest"), CustomerRequest)

                arlCustRequest.Add(objCustomerRequest)
            End If
        End If
    End Sub

    Private Sub TransferProcess(arlCustRequest As ArrayList)
        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim objUser As UserInfo = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "cusreq", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
        Dim tmp As Integer = 0
        Dim NoKTP As String, NoTelp As String
        Dim _oldStatus = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru

        For Each CR As CustomerRequest In arlCustRequest
            If CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi Then
                IsCheck = True
                CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses
                CR.ProcessUserID = objUser.ID

                Dim ObjCity As New City
                ObjCity = New General.CityFacade(User).Retrieve(CR.CityID)
                Dim preRegion As String
                If CR.PrintRegion = "0" Then
                    preRegion = "X"
                Else
                    preRegion = ""
                End If

                'handle sementara untuk prearea
                If CR.PreArea.ToLower = "blank" Then
                    CR.PreArea = ""
                End If

                'Untuk preArea dan kota dipisahkan dengan spasi dan bukan dengan Delimiter chr(13) (Enter)
                'Konfirmasi dari Heru
                'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(13) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & Chr(10))
                'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion )
                CommonFunction.GetKTPAndPhone(CR, NoKTP, NoTelp) 'CR:for:Rina;by:dna:on:20110323

                'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                'If (NoKTP.Trim <> "") Then
                '    If arl.Count > 0 Then sb.Append(vbNewLine)
                '    arl.Add(CR)
                '    sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
                'End If
                'cr spk
                If CR.Status1 > 0 Then
                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                    If CR.TypeIdentitas < 5 Then
                        If (NoKTP.Trim <> "") Then
                            If arl.Count > 0 Then sb.Append(vbNewLine)
                            arl.Add(CR)
                            sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "TDP" & Chr(9) & NoKTP)
                        End If
                    ElseIf CR.TypeIdentitas = 5 Then
                        If (NoKTP.Trim <> "") Then
                            If arl.Count > 0 Then sb.Append(vbNewLine)
                            arl.Add(CR)
                            sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "TDY" & Chr(9) & NoKTP)
                        End If
                    Else
                        If (NoKTP.Trim <> "") Then
                            If arl.Count > 0 Then sb.Append(vbNewLine)
                            arl.Add(CR)
                            sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "SIK" & Chr(9) & NoKTP)
                        End If

                    End If

                Else
                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                    'If (NoKTP.Trim <> "") Then
                    '    If arl.Count > 0 Then sb.Append(vbNewLine)
                    '    arl.Add(CR)
                    '    sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
                    'End If
                    If CR.TypeIdentitas = 0 Then 'KTP
                        If (NoKTP.Trim <> "") Then
                            If arl.Count > 0 Then sb.Append(vbNewLine)
                            arl.Add(CR)
                            sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
                        End If
                    ElseIf CR.TypeIdentitas = 1 Then 'SIM
                        If (NoKTP.Trim <> "") Then
                            If arl.Count > 0 Then sb.Append(vbNewLine)
                            arl.Add(CR)
                            sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "SIM" & Chr(9) & NoKTP)
                        End If
                    ElseIf CR.TypeIdentitas = 2 Then 'KITAS
                        If (NoKTP.Trim <> "") Then
                            If arl.Count > 0 Then sb.Append(vbNewLine)
                            arl.Add(CR)
                            sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KITAS" & Chr(9) & NoKTP)
                        End If
                    Else
                        If (NoKTP.Trim <> "") Then 'KITAP
                            If arl.Count > 0 Then sb.Append(vbNewLine)
                            arl.Add(CR)
                            sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KITAP" & Chr(9) & NoKTP)
                        End If
                    End If
                End If
                '
            End If
        Next

        If IsCheck Then
            If (sb.Length > 0) Then
                If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder

                    For Each CR As CustomerRequest In arlCustRequest
                        sessionHelper.SetSession("CustomerRequest", New CustomerRequestFacade(User).Retrieve(CR.ID))
                        Dim objCustomerRequest = CType(sessionHelper.GetSession("CustomerRequest"), CustomerRequest)

                        'Insert To CustomerStatusHistory status by proses
                        Dim custHistory As New CustomerStatusHistory
                        custHistory.CustomerRequest = objCustomerRequest
                        custHistory.OldStatus = _oldStatus
                        custHistory.NewStatus = objCustomerRequest.Status
                        custHistory.RowStatus = 0
                        Dim _custHistFacade2 As New CustomerStatusHistoryFacade(User)
                        _custHistFacade2.Insert(custHistory)
                    Next

                    MessageBox.Show("Data berhasil di upload ke SAP")
                Else
                    MessageBox.Show("Upload data to SAP gagal")
                End If
            End If
        End If
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                If (New Service.CustomerRequestFacade(User).UpdateTransaction(arl) <> -1) Then
                    sw = New StreamWriter(DestFile)
                    sw.Write(Val)
                    sw.Close()
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

    Private Sub CopyToCustomerRequest(ByVal _header As SPKHeader, ByRef arlCustRequest As ArrayList, ByRef Msg As String)
        Dim objCustomerRequestFacade As New CustomerRequestFacade(User)
        Dim objCustomerRequest As CustomerRequest = New CustomerRequest
        Dim _customer = _header.SPKCustomer
        objUserInfo = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        objDealer = sessionHelper.GetSession("DEALER")
        Dim oSPKDetailCustomerFac As New SPKDetailCustomerFacade(User)
        Dim _fileHelper As New FileHelper
        Dim AutoCustomerStatus As Boolean = sessionHelper.GetSession("AutoCustomerStatus")
        Dim arrListTrue As ArrayList = New ArrayList
        'arlCustRequest = _fileHelper.CopyToCustomerRequest(_header, Msg, objUserInfo, objDealer, AutoCustomerStatus)
        For Each oSPKDetail As SPKDetail In _header.SPKDetails
            For Each oSPKCustomer As SPKDetailCustomer In oSPKDetail.SPKDetailCustomers
                arrListTrue.Add(oSPKCustomer)
            Next
        Next
        arlCustRequest = _fileHelper.SendToCustomerRequest(objDealer, objUserInfo, AutoCustomerStatus, Msg, arrListTrue)
        'Try
        '    objCustomerRequest.CustomerCode = String.Empty
        '    objCustomerRequest.RequestNo = String.Empty
        '    objCustomerRequest.RequestDate = DateTime.Today
        '    objCustomerRequest.RequestType = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Baru
        '    objCustomerRequest.RequestUserID = objUserInfo.ID
        '    objCustomerRequest.Dealer = objDealer
        '    objCustomerRequest.RefRequestNo = String.Empty
        '    objCustomerRequest.Status1 = _header.SPKCustomer.TipeCustomer
        '    objCustomerRequest.Name1 = _customer.Name1
        '    objCustomerRequest.Name2 = _customer.Name2
        '    objCustomerRequest.Name3 = _customer.Name3
        '    objCustomerRequest.Alamat = _customer.Alamat
        '    objCustomerRequest.Kelurahan = _customer.Kelurahan
        '    objCustomerRequest.Kecamatan = _customer.Kecamatan
        '    objCustomerRequest.CityID = _customer.City.ID
        '    objCustomerRequest.PhoneNo = _customer.PhoneNo
        '    objCustomerRequest.ReffCode = String.Empty
        '    objCustomerRequest.PrintRegion = _customer.PrintRegion
        '    objCustomerRequest.Email = _customer.Email
        '    objCustomerRequest.PreArea = _customer.PreArea
        '    objCustomerRequest.TipePerusahaan = _customer.TipePerusahaan
        '    objCustomerRequest.PostalCode = _customer.PostalCode
        '    objCustomerRequest.Attachment = String.Empty

        '    Dim arrProfiles As ArrayList = _customer.SPKCustomerProfiles

        '    Dim iReturn As Integer = 0
        '    If _header.CustomerRequestID < 1 Then
        '        iReturn = objCustomerRequestFacade.InsertFromSPKCustomer(objCustomerRequest, arrProfiles)
        '    End If


        '    If iReturn > 0 Then
        '        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
        '        _header.CustomerRequestID = iReturn
        '        objSPKHeaderFacade.Update(_header)

        '        objCustomerRequest = New CustomerRequest
        '        objCustomerRequest = objCustomerRequestFacade.Retrieve(iReturn)
        '        _fileHelper.UpdateStatusCustomerRequest(objCustomerRequest, AutoCustomerStatus, Msg)
        '        If Not IsNothing(objCustomerRequest) Then
        '            arlCustRequest.Add(objCustomerRequest)
        '        End If
        '    End If

        '    For Each oSPKD As SPKDetail In _header.SPKDetails
        '        arlCustRequest = _fileHelper.SendToCustomerRequest(objDealer, objUserInfo, AutoCustomerStatus, Msg, oSPKD.SPKDetailCustomers)
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Function PopulateSPKCheckedList() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

        For Each oDataGridItem In dtgcari.Items
            chkExport = oDataGridItem.FindControl("cbCheck")
            If chkExport.Checked Then
                Dim _spk As New KTB.DNet.Domain.SPKHeader
                _spk.ID = oDataGridItem.Cells(1).Text
                _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
                oExArgs.Add(_spk)
            End If
        Next
        Return oExArgs
    End Function

    Private Function PopulateSPKHeaderList() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

        For Each oDataGridItem In dtgcari.Items
            chkExport = oDataGridItem.FindControl("cbCheck")
            If chkExport.Checked Then
                Dim _spk As New KTB.DNet.Domain.SPKHeader
                _spk.ID = oDataGridItem.Cells(1).Text
                _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _spk.Status >= EnumStatusSPK.Status.Tunggu_Unit And _spk.CustomerRequestID < 1 Then
                    _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
                    oExArgs.Add(_spk)
                End If
                'If _spk.Status = EnumStatusSPK.Status.Selesai And _spk.CustomerRequestID < 1 Then
                '    _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
                '    oExArgs.Add(_spk)
                'End If
            End If
        Next
        Return oExArgs
    End Function

    Private Sub DoDownload(ByVal dataSPK As ArrayList, ByVal isValidated As Boolean)
        Dim sFileName As String
        sFileName = "Daftar SPK [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim SPKData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPKData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPKData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                If isValidated Then
                    WriteSPKValidated(sw, dataSPK)
                Else
                    WriteSPKDetail2(sw, dataSPK)
                End If

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

    Private Function GetDataSPKDetail(ByVal isValidated As Boolean) As ArrayList
        Dim spkColl As New ArrayList
        Dim critsSPK As CriteriaComposite
        Try
            If isValidated Then
                If sessionHelper.GetSession("SearchSPK.critsChassis") Is Nothing Then
                    MessageBox.Show("Download data gagal ")
                    Exit Function
                End If
                critsSPK = CType(sessionHelper.GetSession("SearchSPK.critsChassis"), CriteriaComposite)
                spkColl = New V_SPKChassisFacade(User).Retrieve(critsSPK)
            Else
                If sessionHelper.GetSession("SearchSPK.critsSPK2") Is Nothing Then
                    MessageBox.Show("Download data gagal ")
                    Exit Function
                End If
                critsSPK = CType(sessionHelper.GetSession("SearchSPK.critsSPK2"), CriteriaComposite)
                spkColl = New V_SPKDetailInfoFacade(User).Retrieve(critsSPK)
            End If



        Catch ex As Exception

        End Try
        Return spkColl
    End Function

    Private Sub WriteSPKDetail(ByVal sw As StreamWriter, ByVal dataSPK As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("SPK - DAFTAR SPK")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        '======SPK DETAIL=======
        If (dataSPK.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("STATUS" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("KOTA DEALER" & tab)
            itemLine.Append("NOMOR SPK" & tab)
            itemLine.Append("NOMOR INDENT" & tab)
            itemLine.Append("TANGGAL PENGAJUAN SPK (dd-MM-yyyy)" & tab)
            itemLine.Append("PERIODE PENGAJUAN SPK" & tab)
            itemLine.Append("PERIODE PENGAJUAN FAKTUR" & tab)
            itemLine.Append("KATEGORI" & tab)
            itemLine.Append("UNIT" & tab)
            itemLine.Append("HARGA PER UNIT (OTR / DEALER PRICE)" & tab)
            itemLine.Append("KODE KENDARAAN" & tab)
            itemLine.Append("NAMA KENDARAAN" & tab)
            itemLine.Append("WARNA " & tab)
            itemLine.Append("MODEL BODY" & tab)
            itemLine.Append("NAMA CUSTOMER" & tab)
            itemLine.Append("ALAMAT CUSTOMER" & tab)     'add start rudi
            itemLine.Append("PROPINSI" & tab)
            itemLine.Append("KOTA" & tab)
            itemLine.Append("NO TELP" & tab)
            itemLine.Append("NAMA SALESMAN" & tab)
            itemLine.Append("POSISI SALESMAN" & tab)
            itemLine.Append("LEVEL SALESMAN" & tab)
            itemLine.Append("SUPERVISOR" & tab)
            itemLine.Append("MANAGER" & tab)
            'itemLine.Append("CARA PEMBAYARAN" & tab)
            'itemLine.Append("KEPEMILIKAN KENDARAAN" & tab)
            'itemLine.Append("KENDARAAN INI SBG. KEND." & tab)
            'itemLine.Append("USIA PEMILIK" & tab)
            'itemLine.Append("PENGGUNAAN UTAMA" & tab)
            'itemLine.Append("BIDAN USAHA KONSUMEN" & tab)
            'itemLine.Append("DAERAH UTAMA OPERASI" & tab)
            'itemLine.Append("JENIS KENDARAAN" & tab)
            'itemLine.Append("MODEL KENDARAAN" & tab)
            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            For Each header As SPKHeader In dataSPK
                For Each item As SPKDetail In header.SPKDetails
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(EnumStatusSPK.GetStringValueStatus(CInt(header.Status)).ToString & tab)
                    itemLine.Append(header.Dealer.DealerCode & tab)
                    itemLine.Append(header.Dealer.DealerName & tab)
                    itemLine.Append(header.Dealer.City.CityName & tab)
                    itemLine.Append(Decimal.Round(CDec(header.SPKNumber), 0) & tab)
                    itemLine.Append(header.IndentNumber & tab)
                    itemLine.Append(header.CreatedTime.ToString("dd-MM-yyyy") & tab)
                    itemLine.Append(header.PlanDeliveryDate.ToString("MM-yyyy") & tab)
                    itemLine.Append(header.PlanInvoiceDate.ToString("MM-yyyy") & tab)
                    If Not IsNothing(item.Category) Then
                        itemLine.Append(item.Category.CategoryCode & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                    End If
                    itemLine.Append(item.Quantity & tab)
                    itemLine.Append(Decimal.Round(item.Amount, 2) & tab)
                    If Not IsNothing(item.VechileColor) Then
                        If Not IsNothing(item.VechileColor.VechileType) Then
                            itemLine.Append(item.VechileColor.VechileType.VechileTypeCode & tab)
                            itemLine.Append(item.VechileColor.VechileType.Description & tab)
                            itemLine.Append(item.VechileColor.ColorEngName & tab)
                        Else
                            Dim objVT As VechileType = New VechileTypeFacade(User).Retrieve(item.VehicleTypeCode)
                            If Not IsNothing(objVT) Then
                                itemLine.Append(objVT.VechileTypeCode & tab)
                                itemLine.Append(objVT.Description & tab)
                                itemLine.Append("ZZZ" & tab)
                            Else
                                itemLine.Append(item.VehicleTypeCode & tab)
                                itemLine.Append(item.VehicleColorCode & tab)
                                itemLine.Append(item.VehicleColorName & tab)
                            End If

                        End If

                    Else
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                    End If

                    If Not IsNothing(item.ProfileDetail) Then
                        itemLine.Append(item.ProfileDetail.Description & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                    End If
                    If Not IsNothing(header.SPKCustomer) Then
                        itemLine.Append(header.SPKCustomer.Name1 & tab)
                        'add start rudi
                        Dim strAlamatCustomer As String = header.SPKCustomer.Alamat + " " + header.SPKCustomer.Kelurahan + " " + header.SPKCustomer.Kecamatan
                        itemLine.Append(strAlamatCustomer & tab)
                        'add end rudi
                        itemLine.Append(header.SPKCustomer.City.Province.ProvinceName & tab)
                        itemLine.Append(header.SPKCustomer.City.CityName & tab)
                        If header.SPKCustomer.PhoneNo.Trim = "" Then
                            itemLine.Append(header.SPKCustomer.HpNo & tab)
                        Else
                            itemLine.Append(header.SPKCustomer.PhoneNo & tab)
                        End If
                    Else
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                    End If
                    If Not IsNothing(header.SalesmanHeader) Then
                        itemLine.Append(header.SalesmanHeader.Name & tab)
                        itemLine.Append(header.SalesmanHeader.JobPosition.Description & tab)
                        itemLine.Append(header.SalesmanHeader.SalesmanLevel.Description & tab)
                        If Not IsNothing(header.SalesmanHeader.LeaderId) Then
                            Dim objSpv As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(header.SalesmanHeader.LeaderId)
                            If Not IsNothing(objSpv) Then
                                itemLine.Append(objSpv.Name & tab)
                                If Not IsNothing(objSpv.LeaderId) Then
                                    Dim objMgr As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(objSpv.LeaderId)
                                    If Not IsNothing(objMgr) Then
                                        itemLine.Append(objMgr.Name & tab)
                                    Else
                                        itemLine.Append(String.Empty & tab)
                                    End If
                                Else
                                    itemLine.Append(String.Empty & tab)
                                End If
                            Else
                                itemLine.Append(String.Empty & tab)
                            End If
                        Else
                            itemLine.Append(String.Empty & tab)
                            itemLine.Append(String.Empty & tab)
                        End If
                    Else
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                    End If

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Next
        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Sub WriteSPKDetail2(ByVal sw As StreamWriter, ByVal dataSPK As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("SPK - DAFTAR SPK")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        '======SPK DETAIL=======
        If (dataSPK.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("STATUS" & tab)
            itemLine.Append("ALASAN BATAL" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("KOTA DEALER" & tab)
            itemLine.Append("NOMOR SPK" & tab)
            itemLine.Append("NOMOR INDENT" & tab)
            itemLine.Append("TANGGAL PENGAJUAN SPK (dd-MM-yyyy)" & tab)
            itemLine.Append("TANGGAL SPK DEALER(dd-MM-yyyy)" & tab)
            itemLine.Append("PERIODE PENGAJUAN SPK" & tab)
            itemLine.Append("PERIODE PENGAJUAN FAKTUR" & tab)
            itemLine.Append("KATEGORI" & tab)
            itemLine.Append("UNIT" & tab)
            itemLine.Append("HARGA PER UNIT (OTR / DEALER PRICE)" & tab)
            itemLine.Append("KODE KENDARAAN" & tab)
            itemLine.Append("KODE WARNA" & tab)
            itemLine.Append("NAMA KENDARAAN" & tab)
            itemLine.Append("WARNA " & tab)
            itemLine.Append("MODEL BODY" & tab)
            itemLine.Append("NAMA CUSTOMER" & tab)
            itemLine.Append("ALAMAT CUSTOMER" & tab)     'add start rudi
            itemLine.Append("PROPINSI" & tab)
            itemLine.Append("KOTA" & tab)
            itemLine.Append("NO TELP" & tab)
            itemLine.Append("NAMA SALESMAN" & tab)
            itemLine.Append("POSISI SALESMAN" & tab)
            itemLine.Append("LEVEL SALESMAN" & tab)
            itemLine.Append("SUPERVISOR" & tab)
            itemLine.Append("MANAGER" & tab)
            itemLine.Append("CARA PEMBAYARAN" & tab)
            itemLine.Append("KEPEMILIKAN KENDARAAN" & tab)
            itemLine.Append("KENDARAAN INI SBG. KEND." & tab)
            itemLine.Append("USIA PEMILIK" & tab)
            itemLine.Append("PENGGUNAAN UTAMA" & tab)
            itemLine.Append("BIDAN USAHA KONSUMEN" & tab)
            itemLine.Append("DAERAH UTAMA OPERASI" & tab)
            itemLine.Append("JENIS KENDARAAN" & tab)
            itemLine.Append("MODEL KENDARAAN" & tab)
            itemLine.Append("NOMOR SPK DEALER " & tab)
            itemLine.Append("NO KTP CUSTOMER " & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As V_SPKDetailInfo In dataSPK
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(EnumStatusSPK.GetStringValueStatus(CInt(item.HeaderStatus)).ToString & tab)
                    itemLine.Append(enumRejectedReason.GetStringValueStatus(CInt(item.RejectedReasonHeader)).ToString & tab)
                    itemLine.Append(item.DealerCode & tab)
                    itemLine.Append(item.DealerName & tab)
                    itemLine.Append(item.CityName & tab)
                    itemLine.Append(Decimal.Round(CDec(item.SPKNumber), 0) & tab)
                    itemLine.Append(item.IndentNumber & tab)
                    itemLine.Append(item.CreatedTime.ToString("dd-MM-yyyy") & tab)
                    itemLine.Append(item.DealerSPKDate.ToString("dd-MM-yyyy") & tab)
                    itemLine.Append(item.CreatedTime.ToString("MM-yyyy") & tab)
                    itemLine.Append(item.PlanInvoiceDate.ToString("MM-yyyy") & tab)
                    itemLine.Append(item.CategoryCode & tab)
                    itemLine.Append(item.Quantity & tab)
                    itemLine.Append(Decimal.Round(item.Amount, 2) & tab)
                    itemLine.Append(item.VechileTypeCode & tab)
                    itemLine.Append(item.VehicleColorCode & tab)
                    itemLine.Append(item.Description & tab)
                    itemLine.Append(item.ColorEngName & tab)
                    itemLine.Append(item.ProfileDescription & tab)
                    itemLine.Append(item.Name1 & tab)
                    Dim strAlamatCustomerOri As String = item.Alamat + " " + item.Kelurahan + " " + item.Kecamatan
                    Dim strAlamatCustomer As String = strAlamatCustomerOri.Replace(vbCr, " ").Replace(vbLf, " ")
                    itemLine.Append(strAlamatCustomer & tab)
                    itemLine.Append(item.ProvinceName & tab)
                    itemLine.Append(item.CityCustomer & tab)
                    If item.PhoneNo.Trim = "" Then
                        itemLine.Append(item.HpNo & tab)
                    Else
                        itemLine.Append(item.PhoneNo & tab)
                    End If
                    itemLine.Append(item.SalesName & tab)
                    itemLine.Append(item.SalesPosition & tab)
                    itemLine.Append(item.SalesLevel & tab)
                    itemLine.Append(item.Supervisor & tab)
                    itemLine.Append(item.Manager & tab)
                    itemLine.Append(item.CaraPembayaran & tab)
                    itemLine.Append(item.KepemilikanKendaraan & tab)
                    itemLine.Append(item.KendaraanSebagai & tab)
                    itemLine.Append(item.UsiaPemilik & tab)
                    itemLine.Append(item.PenggunaanUtamaKendaraan & tab)
                    itemLine.Append(item.BidangUsaha & tab)
                    itemLine.Append(item.DaerahUtama & tab)
                    itemLine.Append(item.JenisKendaraan & tab)
                    itemLine.Append(item.ModelKendaraan & tab)
                    itemLine.Append(item.DealerSPKNumber & tab)
                    'Dim vaLues As String = New SPKCustomerProfileFacade(User).FindProfileValue(item.SPKNumber).ProfileValue
                    itemLine.Append(item.KTP & tab)
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Sub WriteSPKValidated(ByVal sw As StreamWriter, ByVal dataSPK As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("SPK - DAFTAR SPK FAKTUR")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        '======SPK DETAIL=======
        If (dataSPK.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("NOMOR SPK" & tab)
            itemLine.Append("NOMOR SPK DEALER" & tab)
            itemLine.Append("TANGGAL SPK DEALER" & tab)
            itemLine.Append("NOMOR INDENT" & tab)
            itemLine.Append("NOMOR CHASSIS" & tab)
            itemLine.Append("STATUS FAKTUR" & tab)
            itemLine.Append("NOMOR FAKTUR" & tab)
            itemLine.Append("TGL VALIDASI FAKTUR" & tab)
            itemLine.Append("VALIDASI OLEH" & tab)
            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As V_SPKChassis In dataSPK
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.DealerCode & tab)
                    itemLine.Append(item.DealerName & tab)
                    itemLine.Append(Decimal.Round(CDec(item.SPKNumber), 0) & tab)
                    itemLine.Append(item.DealerSPKNumber.ToString & tab)
                    itemLine.Append(item.DealerSPKDate.ToString("dd-MM-yyyy") & tab)
                    itemLine.Append(item.IndentNumber.ToString & tab)
                    itemLine.Append(item.ChassisNumber & tab)
                    itemLine.Append(EnumChassisMaster.FakturStatusDesc(item.FakturStatus.ToString) & tab)
                    itemLine.Append(item.FakturNumber & tab)
                    itemLine.Append(item.FakturValidateTime.ToString("dd-MM-yyyy") & tab)
                    itemLine.Append(item.FakturValidateBy & tab)

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Function IsStatusExist(ByVal _spkHeader As SPKHeader, ByVal newStatus As Integer) As Boolean
        Try
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CInt(LookUp.DocumentType.Surat_Pesanan_Kendaraan)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, _spkHeader.SPKNumber))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, newStatus))
            Dim arlHistStatus As ArrayList = objStatusChangeHistoryFacade.Retrieve(criterias)
            If Not IsNothing(arlHistStatus) AndAlso arlHistStatus.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub RecordStatusChangeHistory(ByVal arrListSPK As ArrayList, ByVal newStatus As Integer)
        For Each item As SPKHeader In arrListSPK
            If Not IsStatusExist(item, newStatus) Then
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Surat_Pesanan_Kendaraan), item.SPKNumber, item.Status, newStatus)
            End If
        Next
    End Sub

    'Private Sub SetIndent()
    '    Dim listSPK As ArrayList = PopulateSPKHeaderIndent()

    '    If listSPK.Count = 0 Then
    '        MessageBox.Show(SR.DataNotSelectedByStatus("SPK", "Indent"))
    '    Else
    '        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
    '        RecordStatusChangeHistory(listSPK, CType(EnumStatusSPK.Status.Indent, Integer))
    '        objSPKHeaderFacade.UpdateSPKStatus(listSPK, CType(EnumStatusSPK.Status.Indent, Integer))
    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub SetTandaJadi()
    '    Dim listSPK As ArrayList = PopulateSPKHeaderTandaJadi()

    '    If listSPK.Count = 0 Then
    '        MessageBox.Show(SR.DataNotSelectedByStatus("SPK", "Tanda Jadi"))
    '    Else
    '        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
    '        RecordStatusChangeHistory(listSPK, CType(EnumStatusSPK.Status.Tanda_Jadi, Integer))
    '        objSPKHeaderFacade.UpdateSPKStatus(listSPK, CType(EnumStatusSPK.Status.Tanda_Jadi, Integer))
    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub SetLunas()
    '    Dim listSPK As ArrayList = PopulateSPKHeaderLunas()

    '    If listSPK.Count = 0 Then
    '        MessageBox.Show(SR.DataNotSelectedByStatus("SPK", "Lunas"))
    '    Else
    '        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
    '        RecordStatusChangeHistory(listSPK, CType(EnumStatusSPK.Status.Lunas, Integer))
    '        objSPKHeaderFacade.UpdateSPKStatus(listSPK, CType(EnumStatusSPK.Status.Lunas, Integer))
    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub SetBatal()
    '    Dim listSPK As ArrayList = PopulateSPKHeaderBatal()

    '    If listSPK.Count = 0 Then
    '        MessageBox.Show(SR.DataNotSelectedByStatus("SPK", "Batal"))
    '    Else
    '        Dim isReasonExist As Boolean = True
    '        For Each objSPKHeader As SPKHeader In listSPK
    '            If objSPKHeader.RejectedReason = String.Empty Then
    '                isReasonExist = False
    '                Exit For
    '            End If
    '        Next
    '        If isReasonExist Then
    '            Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
    '            RecordStatusChangeHistory(listSPK, CType(EnumStatusSPK.Status.Batal, Integer))
    '            objSPKHeaderFacade.UpdateSPKStatus(listSPK, CType(EnumStatusSPK.Status.Batal, Integer))
    '            BindGrid()
    '        Else
    '            MessageBox.Show("Alasan pembatalan harus diisi")
    '        End If
    '    End If
    'End Sub

    'Private Sub SetPending()
    '    Dim listSPK As ArrayList = PopulateSPKHeaderPending()

    '    If listSPK.Count = 0 Then
    '        MessageBox.Show(SR.DataNotSelectedByStatus("SPK", "Pending"))
    '    Else
    '        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
    '        RecordStatusChangeHistory(listSPK, CType(EnumStatusSPK.Status.Pending, Integer))
    '        objSPKHeaderFacade.UpdateSPKStatus(listSPK, CType(EnumStatusSPK.Status.Pending, Integer))
    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub SetSelesai()
    '    Dim listSPK As ArrayList = PopulateSPKHeaderSelesai()

    '    If listSPK.Count = 0 Then
    '        MessageBox.Show(SR.DataNotSelectedByStatus("SPK", "Selesai"))
    '    Else
    '        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
    '        RecordStatusChangeHistory(listSPK, CType(EnumStatusSPK.Status.Selesai, Integer))
    '        objSPKHeaderFacade.UpdateSPKStatus(listSPK, CType(EnumStatusSPK.Status.Selesai, Integer))
    '        BindGrid()
    '    End If
    'End Sub

    'Private Sub SetClosed()
    '    Dim listSPK As ArrayList = PopulateSPKHeaderClosed()

    '    If listSPK.Count = 0 Then
    '        MessageBox.Show(SR.DataNotSelectedByStatus("SPK", "Closed"))
    '    Else
    '        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
    '        RecordStatusChangeHistory(listSPK, CType(EnumStatusSPK.Status.Closed, Integer))
    '        objSPKHeaderFacade.UpdateSPKStatus(listSPK, CType(EnumStatusSPK.Status.Closed, Integer))
    '        BindGrid()
    '    End If
    'End Sub

    'Private Function PopulateSPKHeaderTandaJadi() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EnumStatusSPK
    '    Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

    '    For Each oDataGridItem In dtgcari.Items
    '        chkExport = oDataGridItem.FindControl("cbCheck")
    '        If chkExport.Checked Then
    '            Dim _spk As New KTB.DNet.Domain.SPKHeader
    '            _spk.ID = oDataGridItem.Cells(1).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _spk.Status = EnumStatusSPK.Status.Awal Or _spk.Status = EnumStatusSPK.Status.Indent Then
    '                _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
    '                '_spk.Status = status.Status.Tanda_Jadi
    '                oExArgs.Add(_spk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateSPKHeaderIndent() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EnumStatusSPK
    '    Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

    '    For Each oDataGridItem In dtgcari.Items
    '        chkExport = oDataGridItem.FindControl("cbCheck")
    '        If chkExport.Checked Then
    '            Dim _spk As New KTB.DNet.Domain.SPKHeader
    '            _spk.ID = oDataGridItem.Cells(1).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _spk.Status = EnumStatusSPK.Status.Awal Then
    '                _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
    '                '_spk.Status = status.Status.Tanda_Jadi
    '                oExArgs.Add(_spk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateSPKHeaderLunas() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EnumStatusSPK
    '    Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

    '    For Each oDataGridItem In dtgcari.Items
    '        chkExport = oDataGridItem.FindControl("cbCheck")
    '        If chkExport.Checked Then
    '            Dim _spk As New KTB.DNet.Domain.SPKHeader
    '            _spk.ID = oDataGridItem.Cells(1).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _spk.Status = EnumStatusSPK.Status.Awal Or _spk.Status = EnumStatusSPK.Status.Indent Or _spk.Status = EnumStatusSPK.Status.Tanda_Jadi Or _spk.Status = EnumStatusSPK.Status.Pending Then
    '                _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
    '                '_spk.Status = status.Status.Lunas
    '                oExArgs.Add(_spk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateSPKHeaderBatal() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim ddlReason As System.Web.UI.WebControls.DropDownList
    '    Dim status As New EnumStatusSPK
    '    Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

    '    For Each oDataGridItem In dtgcari.Items
    '        chkExport = oDataGridItem.FindControl("cbCheck")
    '        ddlReason = oDataGridItem.FindControl("ddlRejectedReason")
    '        If chkExport.Checked Then
    '            Dim _spk As New KTB.DNet.Domain.SPKHeader
    '            _spk.ID = oDataGridItem.Cells(1).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _spk.Status = EnumStatusSPK.Status.Awal Or _spk.Status = EnumStatusSPK.Status.Tanda_Jadi Or _spk.Status = EnumStatusSPK.Status.Lunas Or _spk.Status = EnumStatusSPK.Status.Pending Then
    '                _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
    '                '_spk.Status = status.Status.Batal
    '                _spk.RejectedReason = ddlReason.SelectedValue
    '                oExArgs.Add(_spk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateSPKHeaderPending() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim ddlReason As System.Web.UI.WebControls.DropDownList
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EnumStatusSPK
    '    Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

    '    For Each oDataGridItem In dtgcari.Items
    '        chkExport = oDataGridItem.FindControl("cbCheck")
    '        ddlReason = oDataGridItem.FindControl("ddlRejectedReason")
    '        If chkExport.Checked Then
    '            Dim _spk As New KTB.DNet.Domain.SPKHeader
    '            _spk.ID = oDataGridItem.Cells(1).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _spk.Status = EnumStatusSPK.Status.Awal Or _
    '                _spk.Status = EnumStatusSPK.Status.Tanda_Jadi Or _
    '                _spk.Status = EnumStatusSPK.Status.Lunas Then
    '                _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
    '                '_spk.Status = status.Status.Batal
    '                _spk.RejectedReason = ddlReason.SelectedValue
    '                oExArgs.Add(_spk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateSPKHeaderSelesai() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EnumStatusSPK
    '    Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

    '    For Each oDataGridItem In dtgcari.Items
    '        chkExport = oDataGridItem.FindControl("cbCheck")
    '        If chkExport.Checked Then
    '            Dim _spk As New KTB.DNet.Domain.SPKHeader
    '            _spk.ID = oDataGridItem.Cells(1).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
    '            _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _spk.Status = EnumStatusSPK.Status.Lunas Then
    '                _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
    '                '_spk.Status = status.Status.Selesai
    '                oExArgs.Add(_spk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    'Private Function PopulateSPKHeaderClosed() As ArrayList
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox
    '    Dim oExArgs As New System.Collections.ArrayList
    '    Dim status As New EnumStatusSPK
    '    Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

    '    For Each oDataGridItem In dtgcari.Items
    '        chkExport = oDataGridItem.FindControl("cbCheck")
    '        If chkExport.Checked Then
    '            Dim _spk As New KTB.DNet.Domain.SPKHeader
    '            _spk.ID = oDataGridItem.Cells(1).Text
    '            _spk.Status = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
    '            If _spk.Status = EnumStatusSPK.Status.Selesai Then
    '                _spk = objSPKHeaderFacade.Retrieve(_spk.ID)
    '                oExArgs.Add(_spk)
    '            End If
    '        End If
    '    Next
    '    status = Nothing
    '    Return oExArgs
    'End Function

    Private Function PopulateSPKToProcess(ByVal preStatus As ArrayList, ByVal newStatus As Integer) As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)

        If preStatus.Count > 0 Then
            For Each oDataGridItem In dtgcari.Items
                chkExport = oDataGridItem.FindControl("cbCheck")
                If chkExport.Checked Then
                    Dim _spk As New KTB.DNet.Domain.SPKHeader
                    _spk = objSPKHeaderFacade.Retrieve(CInt(oDataGridItem.Cells(1).Text))
                    If _spk.ID > 0 Then
                        If newStatus = EnumStatusSPK.Status.Batal Then
                            _spk.RejectedReason = CType(oDataGridItem.FindControl("ddlRejectedReason"), DropDownList).SelectedValue
                        End If
                        For Each _status As Integer In preStatus
                            If _spk.Status = _status Then
                                oExArgs.Add(_spk)
                            End If
                        Next
                    End If
                End If
            Next
        End If

        Return oExArgs
    End Function

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Daftar_spk_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar SPK")
        End If
        Dim IsAuthorizedUbah As Boolean = SecurityProvider.Authorize(Context.User, SR.Daftar_spk_ubah_privilege)
        Dim IsAuthorizedDownload As Boolean = SecurityProvider.Authorize(Context.User, SR.Daftar_spk_download_privilege)
        Label7.Visible = IsAuthorizedUbah 'lbl ubah status
        ddlProses.Visible = IsAuthorizedUbah 'SecurityProvider.Authorize(Context.User, SR.daftar_spk_ubah_privilege)
        btnProses.Visible = IsAuthorizedUbah 'SecurityProvider.Authorize(Context.User, SR.daftar_spk_ubah_privilege)
        'btnSetKonsumen.Visible = IsAuthorizedUbah 'SecurityProvider.Authorize(Context.User, SR.daftar_spk_ubah_privilege)
        btnDownload.Visible = IsAuthorizedDownload ' SecurityProvider.Authorize(Context.User, SR.daftar_spk_download_privilege)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblStatusFaktur.Visible = False
            lblStatusFakturSpr.Visible = False
            chkValidate.Checked = False
            chkValidate.Visible = False
        End If

    End Sub

    Private Sub SetControlAccess(ByVal val As Boolean)
        objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If (CInt(objDealer.Title) = EnumDealerTittle.DealerTittle.KTB) Then
            Label7.Visible = False 'lbl ubah status
            ddlProses.Enabled = False
            btnProses.Enabled = False
            btnSetKonsumen.Enabled = False
        Else
            Label7.Visible = val 'lbl ubah status
            ddlProses.Enabled = val
            btnProses.Enabled = val
            btnSetKonsumen.Enabled = val
        End If
        btnDownload.Enabled = val

    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "SPKNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub BindDdlTipeWarna(ByVal kode As String)
        ddlTipeWarna.Items.Clear()
        Dim blankItem As New ListItem("Silahkan Pilih", 0)
        ddlTipeWarna.Items.Add(blankItem)

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.Code", MatchType.Exact, kode))
        Try
            Dim arrayListTipeWarna As ArrayList = New ProfileDetailFacade(User).Retrieve(criteria)
            For Each item As ProfileDetail In arrayListTipeWarna
                Dim listItem As New ListItem(item.Description, item.ID)
                listItem.Selected = False
                ddlTipeWarna.Items.Add(listItem)
            Next
            ddlTipeWarna.ClearSelection()

        Catch ex As Exception
            MessageBox.Show("Error Binding ddlKategori, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    'start rudi
    Private Sub BindDDLRejectedReason(ddl As DropDownList, strRejectReason As String)
        ''--DropDownList Rejected Reason
        Try
            ddl.Items.Clear()
            Dim al2 As ArrayList = New enumRejectedReason().RetrieveRejectedReason(strRejectReason)
            ddl.DataSource = al2
            ddl.DataTextField = "Desc"
            ddl.DataValueField = "Code"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("Silahkan Pilih", ""))

            ddl.Items.Remove(ddl.Items.FindByValue(enumRejectedReason.RejectedReason.Batal_Otomatis))

        Catch ex As Exception
            MessageBox.Show("Error Binding DropDownList Rejected Reason, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    'end rudi

    Private Sub BindToDropDownList()

        '--DropDownList Status
        Try
            Dim arlStatus As ArrayList = EnumStatusSPK.RetrieveStatus()
            lboxStatus.Items.Clear()
            For Each item As EnumItem In arlStatus 'LookUp.ArrayStatusSPK
                Dim li As New ListItem(item.Name, item.ID)
                lboxStatus.Items.Add(li)
            Next
            lboxStatus.ClearSelection()
            'lboxStatus.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Error Binding lboxStatus, silahkan kirim error ini ke dnet admin")
        End Try


        '--DropDownList Propinsi
        Dim Provice_criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Try
            ddlPropinsi.Items.Clear()
            ddlPropinsi.DataSource = New ProvinceFacade(User).RetrieveActiveList(Provice_criteria, "ProvinceName", Sort.SortDirection.ASC)
            ddlPropinsi.DataTextField = "ProvinceName"
            ddlPropinsi.DataValueField = "ID"
            ddlPropinsi.DataBind()
            ddlPropinsi.Items.Insert(0, New ListItem("Silakan Pilih", 0))
            ddlPropinsi.SelectedIndex = 0
            ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlPropinsi, silahkan kirim error ini ke dnet admin")
        End Try


        ''--DropDownList Kategori
        Try
            Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList()
            Dim blankItem As New ListItem("Silahkan Pilih", 0)
            ddlKategori.Items.Add(blankItem)
            For Each item As Category In arrayListCategory
                Dim listItem As New ListItem(item.CategoryCode, item.ID)
                listItem.Selected = False
                ddlKategori.Items.Add(listItem)

            Next
            If ddlKategori.Items.Count > 0 Then
                ddlKategori.SelectedIndex = 0
            Else
                ddlKategori.ClearSelection()
            End If


        Catch ex As Exception
            MessageBox.Show("Error Binding ddlKategori, silahkan kirim error ini ke dnet admin")
        End Try


    End Sub

    Private Sub SetSessionCriteria()
        Dim objSSPK As ArrayList = New ArrayList
        objSSPK.Add(txtKodeDealer.Text.Trim) '0
        objSSPK.Add(icDealerSPKDateStart.Value) '1
        objSSPK.Add(icDealerSPKDateEnd.Value) '2
        objSSPK.Add(String.Empty) '3
        objSSPK.Add(String.Empty) '4
        'objSSPK.Add(ddlSPKMonth1.SelectedValue) '1
        'objSSPK.Add(ddlSPKYear1.SelectedValue) '2
        'objSSPK.Add(ddlSPKMonth2.SelectedValue) '3
        'objSSPK.Add(ddlSPKYear2.SelectedValue) '4
        'objSSPK.Add(ddlFakturMonth1.SelectedValue) '5
        'objSSPK.Add(ddlFakturYear1.SelectedValue) '6
        'objSSPK.Add(ddlFakturMonth2.SelectedValue) '7
        'objSSPK.Add(ddlFakturYear2.SelectedValue) '8
        objSSPK.Add(String.Empty) '5
        objSSPK.Add(String.Empty) '6
        objSSPK.Add(String.Empty) '7
        objSSPK.Add(String.Empty) '8
        objSSPK.Add(ddlPropinsi.SelectedValue) '9
        objSSPK.Add(ddlKota.SelectedValue) '10
        objSSPK.Add(GetSelectedItem(lboxStatus)) '11
        objSSPK.Add(ddlKategori.SelectedValue) '12
        objSSPK.Add(ddlTipe.SelectedValue) '13
        objSSPK.Add(ddlTipeWarna.SelectedValue) '14
        objSSPK.Add(txtSalesmanCode.Text) '15
        objSSPK.Add(dtgcari.CurrentPageIndex) '16
        objSSPK.Add(CType(ViewState("CurrentSortColumn"), String)) '17
        objSSPK.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection)) '18
        objSSPK.Add(txtSPKRegistered.Text) '19
        objSSPK.Add(txtSPKDealer.Text) '20
        objSSPK.Add(icDealerSPKInputDateStart.Value) '21
        objSSPK.Add(icDealerSPKInputDateEnd.Value) '22
        objSSPK.Add(chkFilterDealerSPKDate.Checked)  '23
        objSSPK.Add(chkFilterDealerSPKInputDate.Checked)  '24
        sessionHelper.SetSession("SESSIONSEARCHSPK", objSSPK)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSSPK As ArrayList = sessionHelper.GetSession("SESSIONSEARCHSPK")
        If Not objSSPK Is Nothing Then
            txtKodeDealer.Text = objSSPK.Item(0)
            icDealerSPKDateStart.Value = objSSPK.Item(1)
            icDealerSPKDateEnd.Value = objSSPK.Item(2)
            'ddlSPKMonth1.SelectedValue = objSSPK.Item(1)
            'ddlSPKYear1.SelectedValue = objSSPK.Item(2)
            'ddlSPKMonth2.SelectedValue = objSSPK.Item(3)
            'ddlSPKYear2.SelectedValue = objSSPK.Item(4)
            'ddlFakturMonth1.SelectedValue = objSSPK.Item(5)
            'ddlFakturYear1.SelectedValue = objSSPK.Item(6)
            'ddlFakturMonth2.SelectedValue = objSSPK.Item(7)
            'ddlFakturYear2.SelectedValue = objSSPK.Item(8)
            ddlPropinsi.SelectedValue = objSSPK.Item(9)
            ddlKota.SelectedValue = objSSPK.Item(10)
            Dim str() As String = objSSPK.Item(11).ToString().Split(",")
            For Each item As ListItem In lboxStatus.Items
                For i As Integer = 0 To str.Length - 1
                    If item.Value.ToString = str(i).ToString Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            Next
            ddlKategori.SelectedValue = objSSPK.Item(12)
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlTipe, ddlKategori.SelectedItem.Text)

            ddlTipe.SelectedValue = objSSPK.Item(13)
            ddlTipeWarna.SelectedValue = objSSPK.Item(14)
            txtSalesmanCode.Text = objSSPK.Item(15)
            dtgcari.CurrentPageIndex = CType(objSSPK.Item(16), Integer)
            ViewState("CurrentSortColumn") = objSSPK.Item(17)
            ViewState("CurrentSortDirect") = objSSPK.Item(18)
            txtSPKRegistered.Text = objSSPK.Item(19)
            txtSPKDealer.Text = objSSPK.Item(20)
            icDealerSPKInputDateStart.Value = objSSPK.Item(21)
            icDealerSPKInputDateEnd.Value = objSSPK.Item(22)
            chkFilterDealerSPKDate.Checked = objSSPK.Item(23)
            chkFilterDealerSPKInputDate.Checked = objSSPK.Item(24)
            Return True
        End If
        Return False
    End Function

    Private Sub BindGrid()
        BindDataToGrid(dtgcari.CurrentPageIndex)
    End Sub

    Private Sub BindDataToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SPKDetailInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SPKDetailInfo), "RowStatus", MatchType.No, -1))
        Dim critChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SPKChassis), "RowStatus", MatchType.No, -1))
        Dim sqlFaktur As String = "(SELECT ID FROM V_SPKChassis WHERE 1 = 1 "
        'Dealer
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim strDealerIn As String = DealerFacade.GenerateDealerCodeSelection(objDealer, User)
            criterias.opAnd(New Criteria(GetType(SPKHeader), "Dealer.DealerCode", MatchType.InSet, strDealerIn))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            sqlFaktur &= " AND DealerCode in " & strDealerIn
        End If
        If txtKodeDealer.Text.Trim() <> "" Then
            Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
            criterias.opAnd(New Criteria(GetType(SPKHeader), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "DealerCode", MatchType.InSet, strKodeDealerIn))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "DealerCode", MatchType.InSet, strKodeDealerIn))
            sqlFaktur &= " AND DealerCode in " & strKodeDealerIn
        End If

        'pengajuan spk
        If txtSPKRegistered.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.[Partial], txtSPKRegistered.Text.Trim))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "SPKNumber", MatchType.[Partial], txtSPKRegistered.Text.Trim))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "SPKNumber", MatchType.[Partial], txtSPKRegistered.Text.Trim))
            sqlFaktur &= " AND SPKNumber like '%" & txtSPKRegistered.Text.Trim & "%'"
        End If
        'IndentNumber
        If txtIndentNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "IndentNumber", MatchType.[Partial], txtIndentNumber.Text.Trim))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "IndentNumber", MatchType.[Partial], txtIndentNumber.Text.Trim))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "IndentNumber", MatchType.[Partial], txtIndentNumber.Text.Trim))
            sqlFaktur &= " AND IndentNumber like '%" & txtIndentNumber.Text.Trim & "%'"
        End If
        If txtSPKDealer.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "DealerSPKNumber", MatchType.[Partial], txtSPKDealer.Text.Trim))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "DealerSPKNumber", MatchType.[Partial], txtSPKDealer.Text.Trim))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "DealerSPKNumber", MatchType.[Partial], txtSPKDealer.Text.Trim))
            sqlFaktur &= " AND DealerSPKNumber like '%" & txtSPKDealer.Text.Trim & "%'"
        End If

        Dim obj As New SPKHeader
        'obj.DealerSPKDate
        Dim dtStart As DateTime = New DateTime(icDealerSPKDateStart.Value.Year, icDealerSPKDateStart.Value.Month, _
                                    icDealerSPKDateStart.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icDealerSPKDateEnd.Value.Year, icDealerSPKDateEnd.Value.Month, _
                                    icDealerSPKDateEnd.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        If chkFilterDealerSPKDate.Checked Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "DealerSPKDate", MatchType.GreaterOrEqual, dtStart))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "DealerSPKDate", MatchType.GreaterOrEqual, dtStart))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "DealerSPKDate", MatchType.GreaterOrEqual, dtStart))
            sqlFaktur &= " AND CreatedTime >= '" & dtStart

            criterias.opAnd(New Criteria(GetType(SPKHeader), "DealerSPKDate", MatchType.Lesser, dtEnd))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "DealerSPKDate", MatchType.Lesser, dtEnd))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "DealerSPKDate", MatchType.Lesser, dtEnd))
            sqlFaktur &= " AND CreatedTime <= '" & dtEnd
        End If

        'If ddlSPKMonth1.SelectedIndex <> 0 And ddlSPKYear1.SelectedIndex <> 0 Then
        '    Dim spkDate1 As New DateTime(ddlSPKYear1.SelectedValue, ddlSPKMonth1.SelectedValue, 1)
        '    criterias.opAnd(New Criteria(GetType(SPKHeader), "CreatedTime", MatchType.GreaterOrEqual, spkDate1))
        '    criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "CreatedTime", MatchType.GreaterOrEqual, spkDate1))
        '    If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "CreatedTime", MatchType.GreaterOrEqual, spkDate1))
        '    sqlFaktur &= " AND CreatedTime >= '" & ddlSPKYear1.SelectedValue.ToString & "/" & ddlSPKMonth1.SelectedValue.ToString & "/01'"
        'End If

        'If ddlSPKMonth2.SelectedIndex <> 0 And ddlSPKYear2.SelectedIndex <> 0 Then
        '    Dim spkDate2 As New DateTime(ddlSPKYear2.SelectedValue, ddlSPKMonth2.SelectedValue, Date.DaysInMonth(ddlSPKYear2.SelectedValue, ddlSPKMonth2.SelectedValue))
        '    criterias.opAnd(New Criteria(GetType(SPKHeader), "CreatedTime", MatchType.Lesser, spkDate2.AddDays(1)))
        '    criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "CreatedTime", MatchType.Lesser, spkDate2.AddDays(1)))
        '    If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "CreatedTime", MatchType.Lesser, spkDate2.AddDays(1)))
        '    sqlFaktur &= " AND CreatedTime <= '" & ddlSPKYear2.SelectedValue.ToString & "/" & ddlSPKMonth2.SelectedValue.ToString & "/" & Date.DaysInMonth(ddlSPKYear2.SelectedValue, ddlSPKMonth2.SelectedValue).ToString & " 23:59:59'"
        'End If

        'pengajuan faktur
        'If ddlFakturMonth1.SelectedIndex <> 0 And ddlFakturYear1.SelectedIndex <> 0 And ddlFakturMonth2.SelectedIndex <> 0 And ddlFakturYear2.SelectedIndex <> 0 Then
        '    Dim fakturDate1 As New DateTime(ddlFakturYear1.SelectedValue, ddlFakturMonth1.SelectedValue, 1)
        '    criterias.opAnd(New Criteria(GetType(SPKHeader), "PlanInvoiceDate", MatchType.GreaterOrEqual, fakturDate1))
        '    criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "PlanInvoiceDate", MatchType.GreaterOrEqual, fakturDate1))
        '    If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "PlanInvoiceDate", MatchType.GreaterOrEqual, fakturDate1))
        '    sqlFaktur &= " AND PlanInvoiceDate >= '" & ddlFakturYear1.SelectedValue.ToString & "/" & ddlFakturMonth1.SelectedValue.ToString & "/01'"
        'End If

        'If ddlFakturMonth2.SelectedIndex <> 0 And ddlFakturYear2.SelectedIndex <> 0 Then
        '    Dim fakturDate2 As New DateTime(ddlFakturYear2.SelectedValue, ddlFakturMonth2.SelectedValue, 1)
        '    criterias.opAnd(New Criteria(GetType(SPKHeader), "PlanInvoiceDate", MatchType.Lesser, fakturDate2.AddDays(1)))
        '    criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "PlanInvoiceDate", MatchType.Lesser, fakturDate2.AddDays(1)))
        '    If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "PlanInvoiceDate", MatchType.Lesser, fakturDate2.AddDays(1)))
        '    sqlFaktur &= " AND CreatedTime <= '" & ddlFakturYear2.SelectedValue.ToString & "/" & ddlFakturMonth2.SelectedValue.ToString & "/" & Date.DaysInMonth(ddlFakturYear2.SelectedValue, ddlFakturMonth2.SelectedValue).ToString & " 23:59:59'"
        'End If

        'propinsi
        If ddlPropinsi.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKCustomer.City.Province.ID", MatchType.Exact, ddlPropinsi.SelectedValue))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "ProvinceID", MatchType.Exact, ddlPropinsi.SelectedValue))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "SPKCustomer.City.Province.ID", MatchType.Exact, ddlPropinsi.SelectedValue))
        End If
        'kota
        If ddlKota.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKCustomer.City.ID", MatchType.Exact, ddlKota.SelectedValue))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "CityID", MatchType.Exact, ddlKota.SelectedValue))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "SPKCustomer.City.ID", MatchType.Exact, ddlKota.SelectedValue))
        End If

        'status
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(SPKHeader), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "HeaderStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
            sqlFaktur &= " AND Status IN (" & SelectedStatus & ")"
        End If

        If ddlKategori.SelectedIndex > 0 Then
            'category
            Dim sqlCategory As String = ""
            sqlCategory &= "Select h.id "
            sqlCategory &= "from spkheader as h, spkdetail as det "
            sqlCategory &= "where(h.id = det.spkheaderid And det.CategoryId=" & ddlKategori.SelectedValue & ")"
            criterias.opAnd(New Criteria(GetType(SPKHeader), "ID", MatchType.InSet, "(" & sqlCategory & ")"))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "CategoryID", MatchType.Exact, ddlKategori.SelectedValue))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "ID", MatchType.InSet, "(" & sqlCategory & ")"))
            sqlFaktur &= " AND ID IN (" & sqlCategory & ")"
            'tipe
            If ddlTipe.SelectedValue <> -1 Then
                'Dim Sql As String = ""

                ''--add start rudi
                'If ddlTipe.SelectedValue = EnumVehicleSubCategory.SubOfPC.Pajero_PajeroSport Or _
                '    ddlTipe.SelectedValue = EnumVehicleSubCategory.SubOfPC.All_New_PajeroSport Or _
                '    ddlTipe.SelectedValue = EnumVehicleSubCategory.SubOfPC.StradaTriton Or _
                '    ddlTipe.SelectedValue = EnumVehicleSubCategory.SubOfPC.All_New_StradaTriton _
                '    Then

                '    Dim dtInputSPKStart As DateTime = New DateTime(icDealerSPKInputDateStart.Value.Year, icDealerSPKInputDateStart.Value.Month, _
                '                    icDealerSPKInputDateStart.Value.Day, 0, 0, 0)
                '    Dim dtInputSPKEnd As DateTime = New DateTime(icDealerSPKInputDateEnd.Value.Year, icDealerSPKInputDateEnd.Value.Month, _
                '                                icDealerSPKInputDateEnd.Value.Day, 0, 0, 0)
                '    dtInputSPKEnd = dtInputSPKEnd.AddDays(1)
                '    Dim strInputSPKStart As String = "1753-01-01"
                '    Dim strInputSPKEnd As String = DateTime.Now.ToString
                '    If chkFilterDealerSPKInputDate.Checked Then
                '        strInputSPKStart = dtInputSPKStart.Year & "-" & dtInputSPKStart.Month & "-" & dtInputSPKStart.Day
                '        strInputSPKEnd = dtInputSPKEnd.Year & "-" & dtInputSPKEnd.Month & "-" & dtInputSPKEnd.Day
                '    End If

                '    Dim objSPKHeaderFac As SPKHeaderFacade = New SPKHeaderFacade(User)
                '    Dim objEVSC As New EnumVehicleSubCategory
                '    Dim getSPKHeaderIDQuery As String = "EXEC up_getSPKHeaderID '" & objEVSC.GetSubOfPCString(ddlTipe.SelectedValue) & "', '" & strInputSPKStart & "', '" & strInputSPKEnd & "'"
                '    Dim ds As DataSet = objSPKHeaderFac.RetrieveDataset(getSPKHeaderIDQuery)
                '    Dim dtTable As New DataTable
                '    dtTable = ds.Tables(0)
                '    If dtTable.Rows.Count <> 0 Then
                '        For Each row As DataRow In dtTable.Rows
                '            If Sql = "" Then
                '                Sql = row("ID").ToString
                '            Else
                '                Sql = Sql & "," & row("ID").ToString
                '            End If
                '        Next row
                '    End If
                'Else
                '--add end rudi

                'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
                'Dim sVals As String = oEVSC.GetSQLValue(ddlKategori.SelectedItem.Text, ddlTipe.SelectedValue)

                'Sql &= " select distinct(head.id) from SPKDetail pkd, SPKHeader as head,  VechileType vt "
                'Sql &= " where pkd.SPKHeaderID = head.ID And pkd.VehicleTypeCode = vt.VechileTypeCode" ' And pkd.Status=0"
                'Dim i As Integer
                'For i = 0 To sVals.Split(";").Length - 1
                '    If i = 0 Then
                '        Sql &= " and (vt.Description like '" & sVals.Split(";")(i) & "' "
                '        If sVals.Split(";").Length = 1 Then Sql &= ")"
                '    ElseIf i = sVals.Split(";").Length - 1 Then
                '        Sql &= " or vt.Description like '" & sVals.Split(";")(i) & "') "
                '    Else
                '        Sql &= " or vt.Description like '" & sVals.Split(";")(i) & "'"
                '    End If
                'Next
                ''End If

                Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlTipe.SelectedValue
                Dim strSql2 As String = "select distinct a.SPKHeaderID from SPKDetail a join VechileType b on a.VehicleTypeCode = b.VechileTypeCode and b.RowStatus = 0 "
                strSql2 += " join VechileModel c on b.ModelID = c.ID and c.RowStatus = 0 where a.RowStatus = 0 and c.ID in (" & strSql & ")"
                criterias.opAnd(New Criteria(GetType(SPKHeader), "ID", MatchType.InSet, "(" & strSql2 & ")"))
                criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "SPKHeaderID", MatchType.InSet, "(" & strSql2 & ")"))
                If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "ID", MatchType.InSet, "(" & strSql2 & ")"))
                sqlFaktur &= " AND ID IN (" & strSql2 & ")"

            End If
            'tipe body
            If ddlTipeWarna.SelectedIndex > 0 Then
                Dim Sql As String = ""
                Sql &= " select distinct(head.id) from SPKDetail det, SPKHeader as head"
                'Sql &= " where det.SPKHeaderID = head.ID and det.Status = 0 And det.ProfileDetailID =" & ddlTipeWarna.SelectedValue
                Sql &= " where det.SPKHeaderID = head.ID And det.ProfileDetailID =" & ddlTipeWarna.SelectedValue
                criterias.opAnd(New Criteria(GetType(SPKHeader), "ID", MatchType.InSet, "(" & Sql & ")"))
                criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "ProfileDetailID", MatchType.Exact, ddlTipeWarna.SelectedValue))
                If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "ID", MatchType.InSet, "(" & Sql & ")"))
                sqlFaktur &= " AND ID IN (" & Sql & ")"
            End If
        End If
        Dim objSPKDetail As SPKDetail

        'salesman
        If txtSalesmanCode.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanCode.Text.Trim))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "SalesmanCode", MatchType.Exact, txtSalesmanCode.Text.Trim))
            If chkValidate.Checked Then critChassis.opAnd(New Criteria(GetType(V_SPKChassis), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanCode.Text.Trim))
        End If
        '
        sqlFaktur &= " )"

        If chkValidate.Checked And sqlFaktur <> "" Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "ID", MatchType.InSet, sqlFaktur))
        End If
        Select Case ddlStatusKonsumen.SelectedValue
            Case 0
                criterias.opAnd(New Criteria(GetType(SPKHeader), "CustomerRequestID", MatchType.LesserOrEqual, 0))
            Case 1
                criterias.opAnd(New Criteria(GetType(SPKHeader), "CustomerRequestID", MatchType.Greater, 0))
        End Select

        If lblDealerBranchID.Text <> "" Then
            Dim idBranch As Integer = CInt(lblDealerBranchID.Text)
            criterias.opAnd(New Criteria(GetType(SPKHeader), "SalesmanHeader.DealerBranch.ID", MatchType.Exact, idBranch))
        End If
        'If chkKonsumen.Checked Then
        '    criterias.opAnd(New Criteria(GetType(SPKHeader), "CustomerRequestID", MatchType.Greater, 0))
        'End If
        '
        Dim dtInputStart As DateTime = New DateTime(icDealerSPKInputDateStart.Value.Year, icDealerSPKInputDateStart.Value.Month, _
                                    icDealerSPKInputDateStart.Value.Day, 0, 0, 0)
        Dim dtInputEnd As DateTime = New DateTime(icDealerSPKInputDateEnd.Value.Year, icDealerSPKInputDateEnd.Value.Month, _
                                    icDealerSPKInputDateEnd.Value.Day, 0, 0, 0)
        dtInputEnd = dtInputEnd.AddDays(1)

        If chkFilterDealerSPKInputDate.Checked Then
            criterias.opAnd(New Criteria(GetType(SPKHeader), "CreatedTime", MatchType.GreaterOrEqual, dtInputStart))
            criterias.opAnd(New Criteria(GetType(SPKHeader), "CreatedTime", MatchType.Lesser, dtInputEnd))

            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "CreatedTime", MatchType.GreaterOrEqual, dtInputStart))
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "CreatedTime", MatchType.Lesser, dtInputEnd))
        End If

        sessionHelper.SetSession("SearchSPK.critsSPK", criterias)
        sessionHelper.SetSession("SearchSPK.critsSPK2", criterias2)
        sessionHelper.SetSession("SearchSPK.critsChassis", critChassis)

        arlSPKHeader = New ArrayList

        If CInt(ddlAlokasi.SelectedValue) = 0 Then     'Alokasi Sebagian
            criterias.opAnd(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            arlSPKHeader = New SPKHeaderFacade(User).Retrieve(criterias)

            Dim unitQty As Integer = 0
            Dim arlSPKHeaderTemp As ArrayList = New ArrayList
            For Each item As SPKHeader In arlSPKHeader
                Dim strUnitAmount As String = GetTotalUnitAmountSPK(item)
                Dim arr As String() = strUnitAmount.Split(";")
                unitQty = CType(arr(0), Integer)

                If Not (item.SPKFakturs Is Nothing) And item.SPKFakturs.Count > 0 Then
                    Dim iQtyC As Integer = 0
                    Dim iQtyNol As Integer = 0
                    For Each objSPKFaktur As SPKFaktur In item.SPKFakturs
                        If Not objSPKFaktur.EndCustomer Is Nothing Then
                            If Not objSPKFaktur.EndCustomer.ChassisMaster Is Nothing Then
                                If objSPKFaktur.EndCustomer.ChassisMaster.FakturStatus.ToString <> "0" Then
                                    iQtyC = iQtyC + 1
                                Else
                                    iQtyNol = iQtyNol + 1
                                End If
                            End If
                        End If
                    Next

                    If (iQtyC < item.SPKFakturs.Count) And (iQtyNol < item.SPKFakturs.Count) Then
                        arlSPKHeaderTemp.Add(item)
                    Else
                        If (item.SPKFakturs.Count < unitQty) And (iQtyNol < item.SPKFakturs.Count) Then
                            arlSPKHeaderTemp.Add(item)
                        End If
                    End If
                End If
            Next
            arlSPKHeader = New ArrayList
            arlSPKHeader = arlSPKHeaderTemp

            Dim unit As Integer = 0
            Dim amount As Decimal = 0
            Dim totalunit As Integer = 0
            Dim totalamount As Decimal = 0
            For Each _SPKHdr As SPKHeader In arlSPKHeader
                Dim strUnitAmount As String = GetTotalUnitAmountSPK(_SPKHdr)
                Dim arr As String() = strUnitAmount.Split(";")
                unit = CType(arr(0), Integer)
                amount = CType(arr(1), Integer)

                totalunit = totalunit + unit
                totalamount = totalamount + amount
            Next
            sessionHelper.SetSession("TotalAmount", amount)

            lblTotalUnit.Text = FormatNumber(totalunit, 0, TriState.False, TriState.True, TriState.True)
            lblTotalHarga.Text = FormatNumber(totalamount, 0, TriState.False, TriState.True, TriState.True)
        Else
            arlSPKHeader = New SPKHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgcari.PageSize, _
                    total, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            Dim spkDetailInfo As V_SPKDetailInfoFacade = New V_SPKDetailInfoFacade(User)
            Dim aggAmount As Aggregate = New Aggregate(GetType(V_SPKDetailInfo), "Amount", AggregateType.Sum)
            Dim aggUnit As Aggregate = New Aggregate(GetType(V_SPKDetailInfo), "Quantity", AggregateType.Sum)
            criterias2.opAnd(New Criteria(GetType(V_SPKDetailInfo), "DetailStatus", MatchType.NotInSet, 1))

            Dim TotalAmount As Decimal = spkDetailInfo.GetAggregateResult(aggAmount, criterias2)
            Dim TotalUnit As Decimal = spkDetailInfo.GetAggregateResult(aggUnit, criterias2)

            sessionHelper.SetSession("TotalAmount", TotalAmount)

            lblTotalUnit.Text = FormatNumber(TotalUnit, 0, TriState.False, TriState.True, TriState.True)
            lblTotalHarga.Text = FormatNumber(TotalAmount, 0, TriState.False, TriState.True, TriState.True)
        End If

        ' total = arlSPKHeader.Count
        dtgcari.DataSource = arlSPKHeader
        dtgcari.VirtualItemCount = total
        If arlSPKHeader.Count > 0 Then
            dtgcari.DataBind()
        Else
            Try
                dtgcari.DataBind()
            Catch ex As Exception
            End Try
            MessageBox.Show("Data Tidak Ditemukan")
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

    Private Function GetTotalUnitAmountSPK(ByVal objSPK As SPKHeader) As String
        Dim strReturn As String = String.Empty
        Dim unit As Integer = 0
        Dim amount As Decimal = 0
        For Each item As SPKDetail In objSPK.SPKDetails
            If (item.Status <> 1) Then  'status Batal
                unit += item.Quantity
                amount += item.TotalAmount
            End If
        Next
        strReturn = unit.ToString + ";" + amount.ToString
        Return strReturn
    End Function

    Private Function GetKonsumenConfirmation(ByVal isAfterConfirmation As Boolean) As Boolean
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim Total As Integer = 0

        For Each oDataGridItem In dtgcari.Items
            chkExport = oDataGridItem.FindControl("cbCheck")
            Dim TotKonsumenFaktur As Integer = 0
            Dim TotJadiKonsumen As Integer = 0
            If chkExport.Checked Then
                Dim lblTotalKonsumenFaktur As Label = CType(oDataGridItem.FindControl("lblTotalKonsumenFaktur"), Label)
                Dim lblTotalJadiKonsumen As Label = CType(oDataGridItem.FindControl("lblTotalJadiKonsumen"), Label)

                If lblTotalKonsumenFaktur.Text <> "" Then
                    TotKonsumenFaktur = CType(lblTotalKonsumenFaktur.Text, Integer)
                End If
                If lblTotalJadiKonsumen.Text <> "" Then
                    TotJadiKonsumen = CType(lblTotalJadiKonsumen.Text, Integer)
                End If

                Dim Selisih As Integer = 0
                Selisih = TotKonsumenFaktur - TotJadiKonsumen
                Total += Selisih
            End If
        Next

        If isAfterConfirmation Then
            If Me.HFKonsumenConfirmation.Value <> "1" Then 'User click No in confirmation box
                Return False
            Else
                Return True 'user click ok in confirmation box
            End If
        Else
            MessageBox.Confirm("Anda akan menjadikan konsumen " + Total.ToString() + " konsumen faktur, apakah anda yakin?", "HFKonsumenConfirmation")
            Return False
        End If
    End Function

    Private Function ValidateSave() As Boolean
        HFKonsumenConfirmation.Value = "-1"
        Return True
    End Function

    Private Function IsSPKMatching(ByRef msg As String) As Boolean

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim lblSpkNumber As Label
        Dim oExArgs As New System.Collections.ArrayList
        Dim objVWI_SPKChassisMatchingFacade As New VWI_SPKChassisMatchingFacade(User)
        Dim criterias As CriteriaComposite
        Dim spkMatchingList As List(Of String) = New List(Of String)

        For Each oDataGridItem In dtgcari.Items
            chkExport = oDataGridItem.FindControl("cbCheck")
            lblSpkNumber = oDataGridItem.FindControl("lblSPKNumber")
            If chkExport.Checked Then
                criterias = New CriteriaComposite(New Criteria(GetType(VWI_SPKChassisMatching), "ID", MatchType.IsNotNull, Nothing))
                criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "SPKNumber", MatchType.Exact, lblSpkNumber.Text.Trim()))
                criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "MatchingType", MatchType.InSet, "(1,3)"))
                oExArgs = objVWI_SPKChassisMatchingFacade.Retrieve(criterias, "ID", Sort.SortDirection.DESC)
                If oExArgs.Count > 0 Then
                    Dim _spk As VWI_SPKChassisMatching = DirectCast(oExArgs(0), VWI_SPKChassisMatching)
                    spkMatchingList.Add(_spk.SPKNumber)
                End If
            End If
        Next
        If spkMatchingList.Count > 0 Then
            msg = String.Format("Nomor SPK : [{0}] sudah proses matching. Apakah anda yakin untuk mengubah status menjadi batal ?", String.Join(", ", spkMatchingList))
        End If

        Return spkMatchingList.Count > 0
    End Function

#End Region

    Private Sub dtgcari_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgcari.PageIndexChanged
        dtgcari.CurrentPageIndex = e.NewPageIndex
        BindDataToGrid(dtgcari.CurrentPageIndex)
    End Sub
End Class

