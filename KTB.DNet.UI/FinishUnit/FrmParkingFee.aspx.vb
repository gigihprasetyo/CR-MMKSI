Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Collections
Imports KTB.DNet.BusinessValidation.Helpers


Public Class FrmParkingFee
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgParkingFee As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtGenerate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGenerate As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblGenerate As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Dim sHelper As New SessionHelper
    Private arlParkingFee As ArrayList = New ArrayList
    Private arlParkingFeeFilter As ArrayList = New ArrayList
    Private objDealer As Dealer
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
#End Region

#Region "Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = sHelper.GetSession("DEALER")
        InitiateAuthorization()
        If Not IsPostBack Then
            Initialize()
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            'txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ParkingFeeBind(0)
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click

        Dim objPFFacade As ParkingFeeFacade = New ParkingFeeFacade(User)
        Dim idPF As String = ""

        If txtGenerate.Text.Trim = "" Then
            MessageBox.Show("Nomor Bukti Potong tidak boleh kosong")
            Exit Sub
        End If

        Dim strCompanyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim ObjCatF As New ProductCategoryFacade(User)
        For Each item As DataGridItem In dtgParkingFee.Items
            Dim chk As CheckBox = item.FindControl("cbCheck")
            If chk.Checked Then
                Dim objParkingFee As New ParkingFee()
                ''objParkingFee = CType(item, ParkingFee)
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                Dim lblStatus As Label = CType(item.FindControl("lblStatus"), Label)
                Dim lblDealerCode As Label = CType(item.FindControl("lblDealerCode"), Label)
                Dim lblNoJV As Label = CType(item.FindControl("lblNoJV"), Label)
                'Dim lblhcategory As Label = CType(item.FindControl("lblhcategory"), Label)

                If lblStatus.Text <> "Baru" Then
                    MessageBox.Show("Status harus baru")
                    Exit Sub
                End If

                'If lblDealerCode.Text <> objDealer.DealerCode Then
                '    MessageBox.Show("Dealer harus sama")
                '    Exit Sub
                'End If

                If lblNoJV.Text = "" Then
                    MessageBox.Show("Belum ada No. JV")
                    Exit Sub
                End If
                'Dim _ObPC As New ProductCategory
                '_ObPC = ObjCatF.Retrieve(lblhcategory.Text)
                'If Not IsNothing(_ObPC) Then
                '    If _ObPC.Code.ToUpper() = strCompanyCode.ToUpper() Then
                idPF = idPF & lblID.Text.ToString & ","
                'End If
                '    End If


            End If
        Next

        If idPF = "" Then
            MessageBox.Show("Pilih Data Terlebih Dulu")
            Exit Sub
        Else
            Try
                idPF = Left(idPF, idPF.Length - 1)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFee), "ID", MatchType.InSet, "(" & idPF & ")"))
                Dim arlPF As ArrayList = objPFFacade.Retrieve(criterias)
                If arlPF.Count > 0 Then
                    Dim periode As Short = CType(arlPF(0), ParkingFee).Periode
                    Dim creditAccount As String = CType(arlPF(0), ParkingFee).Dealer.CreditAccount
                    Dim ObjMMC As Integer = 0
                    Dim ObjMFTBC As Integer = 0
                    If arlPF.Count > 0 Then
                        For Each pf As ParkingFee In arlPF


                            If pf.Periode <> periode Then
                                MessageBox.Show("Data Parking Fee harus dalam satu periode")
                                Exit Sub
                            End If
                            If pf.Dealer.CreditAccount <> creditAccount Then
                                MessageBox.Show("Data Parking Fee harus dalam satu Credit Account")
                                Exit Sub
                            End If
                            'If pf.Dealer.CreditAccount <> objDealer.CreditAccount Then
                            '    MessageBox.Show("Data Parking Fee harus dalam satu Credit Account")
                            '    Exit Sub
                            'End If


                        Next
                    End If

                    Dim ObjCheck = (From ObjPF As ParkingFee In arlPF
                      Where ObjPF.RowStatus = CType(DBRowStatus.Active, Short)
                      Group By ObjPF.Category.ProductCategory.Code Into Group
                      Select Code).Count()

                    If ObjCheck > 1 Then
                        MessageBox.Show("Data CV tidak bisa digabungkan dengan PC/LCV")

                        Exit Sub
                    End If

                    Dim ObjOtherCOmpany = (From ObjPF As ParkingFee In arlPF
                    Where ObjPF.RowStatus = CType(DBRowStatus.Active, Short) And ObjPF.Category.ProductCategory.Code.ToUpper().Trim() <> strCompanyCode.ToUpper.Trim()
                    Group By ObjPF.Category.ProductCategory.Code Into Group
                    Select Code).Count()
                    'strCompanyCode

                    If Not IsNothing(ObjOtherCOmpany) AndAlso ObjOtherCOmpany >= 1 Then
                        If strCompanyCode.ToUpper = "MMC" Then
                            MessageBox.Show("Data CV tidak bisa diproses")
                        Else
                            MessageBox.Show("Data PC/LCV tidak bisa diproses")
                        End If


                        Exit Sub
                    End If

                    Dim objParkingFeeReturnHeaderFacade As ParkingFeeReturnHeaderFacade = New ParkingFeeReturnHeaderFacade(User)
                    If objParkingFeeReturnHeaderFacade.GenerateNoRegBuktiPotongPPh(arlPF, txtGenerate.Text.Trim) = 1 Then
                        MessageBox.Show("Generate No. Bukti Potong PPh Dengan No " & txtGenerate.Text.Trim & " Berhasil")
                        txtGenerate.Text = ""
                        ParkingFeeBind(dtgParkingFee.CurrentPageIndex)
                    Else
                        MessageBox.Show("Generate No. Bukti Potong PPh Dengan No " & txtGenerate.Text.Trim & " Gagal !")
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlPF As New ArrayList
        arlPF = PopulateParkingFee()
        If arlPF.Count = 0 Then
            arlPF = CType(sHelper.GetSession("ARLPARKINGFEE"), ArrayList)
        End If
        DoDownload(arlPF)
    End Sub

    Private Sub dtgParkingFee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgParkingFee.ItemDataBound
        If Not (arlParkingFee Is Nothing) Then
            If Not (arlParkingFee.Count = 0 Or e.Item.ItemIndex = -1) Then
                Dim RowValue As ParkingFee = CType(e.Item.DataItem, ParkingFee)

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (dtgParkingFee.CurrentPageIndex * dtgParkingFee.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatus.Text = EnumParkingFeeStatus.GetStringValue(RowValue.Status)

                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = RowValue.Dealer.DealerCode
                lblDealerCode.ToolTip = RowValue.Dealer.DealerName

                Dim lblCreditAccount As Label = CType(e.Item.FindControl("lblCreditAccount"), Label)
                lblCreditAccount.Text = RowValue.Dealer.CreditAccount

                Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
                lblPeriode.Text = EnumParkingFeePeriod.GetStringValue(CType(RowValue.Periode, Integer))

                Dim lblPPN As Label = CType(e.Item.FindControl("lblPPN"), Label)
                'lblPPN.Text = CType(RowValue.Amount * 0.1, Decimal).ToString("#,###")

                Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(RowValue.CreatedTime, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                lblPPN.Text = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=RowValue.Amount).ToString("#,###")

                Dim lblDebitCharge As Label = CType(e.Item.FindControl("lblDebitCharge"), Label)
                lblDebitCharge.Text = RowValue.DebitChargeNumber

                Dim lblDebitMemo As Label = CType(e.Item.FindControl("lblDebitMemo"), Label)
                lblDebitMemo.Text = RowValue.DebitMemoNumber

                Dim lblFakturPajak As Label = CType(e.Item.FindControl("lblFakturPajak"), Label)
                lblFakturPajak.Text = RowValue.FakturPajakNo

                Dim lnkDebitCharge As LinkButton = CType(e.Item.FindControl("lnkDebitCharge"), LinkButton)
                If Not IsNothing(RowValue.FileNameParkingFee) And RowValue.FileNameParkingFee.ToString <> "" Then
                    lnkDebitCharge.Text = "<img src=""../images/download.gif"" border=""0""/> "
                Else
                    lnkDebitCharge.Text = String.Empty
                End If

                Dim lnkDebitMemo As LinkButton = CType(e.Item.FindControl("lnkDebitMemo"), LinkButton)
                If Not IsNothing(RowValue.FileNameDebitMemo) And RowValue.FileNameDebitMemo.ToString <> "" Then
                    lnkDebitMemo.Text = "<img src=""../images/download.gif"" border=""0""/> "
                Else
                    lnkDebitMemo.Text = String.Empty
                End If

                Dim lblSurat As Label = CType(e.Item.FindControl("lblSurat"), Label)
                lblSurat.ToolTip = "Penalty parkir"
                lblSurat.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSuratParkir.aspx?id=" & RowValue.ID, "scrollbars=auto", 800, 800, "DealerSelection")

                'Dim lblBuktiPotong As Label = CType(e.Item.FindControl("lblBuktiPotong"), Label)
                'lblBuktiPotong.ToolTip = "Bukti Potong"
                'lblBuktiPotong.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpBuktiPemotonganPPH.aspx?id=" & RowValue.ID, "scrollbars=auto", 800, 800, "DealerSelection")

                Dim lblDealerDeposit As Label = CType(e.Item.FindControl("lblDealerDeposit"), Label)
                If Not IsNothing(RowValue.DealerDepositA) Then
                    lblDealerDeposit.Text = RowValue.DealerDepositA.DealerCode
                Else
                    lblDealerDeposit.Text = String.empty
                End If


                Dim lblNoJV As Label = CType(e.Item.FindControl("lblNoJV"), Label)
                lblNoJV.Text = RowValue.AssignmentNumber

                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDelete.Attributes.Add("onclick", "return confirm('Apakah anda akan menghapus data penalty parkir ini ?');")
                lbtnDelete.CommandArgument = RowValue.ID.ToString

                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    If RowValue.Status = EnumParkingFeeStatus.ParkingFeeStatus.Baru Then
                        lbtnDelete.Visible = True
                    Else
                        lbtnDelete.Visible = False
                    End If
                Else
                    lbtnDelete.Visible = False
                End If

                Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
                lbtnHistory.Attributes("OnClick") = "showPopUp('../PopUp/PopUpHistoryParkingFee.aspx?Id=" & RowValue.ID & "&cat=" & RowValue.Category.CategoryCode & " ','',500,760,'');"
                lbtnHistory.ToolTip = "Lihat History"

            End If
        End If
    End Sub

    Private Sub dtgParkingFee_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgParkingFee.ItemCommand
        Select Case (e.CommandName)
            Case "lnkDebitCharge"
                Dim linkButton As LinkButton = e.Item.FindControl("lnkDebitCharge")

                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("ParkingFeeDirectory")
                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                Dim DestFullFilePath As String = fileInfo1.Directory.FullName & "\" & DestFile
                Dim dataFile As String = DestFullFilePath & "\" & e.Item.Cells(20).Text
                Dim fileInfox As New FileInfo(dataFile)

                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & dataFile)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(linkButton.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If

            Case "lnkDebitMemo"
                Dim linkButton As LinkButton = e.Item.FindControl("lnkDebitMemo")

                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("ParkingFeeDirectory")
                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                Dim DestFullFilePath As String = fileInfo1.Directory.FullName & "\" & DestFile
                Dim dataFile As String = DestFullFilePath & "\" & e.Item.Cells(21).Text
                Dim fileInfox As New FileInfo(dataFile)

                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & dataFile)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(linkButton.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If

                'Case "History"

            Case "Delete"
                DeleteParkingFee(CInt(e.CommandArgument))

        End Select
    End Sub


    Private Sub DeleteParkingFee(ByVal nID As Integer)
        Try
            Dim nresult As Integer = 0
            Dim oPFFacade As New ParkingFeeFacade(User)
            Dim oPF As ParkingFee = oPFFacade.Retrieve(nID)
            oPF.RowStatus = CType(DBRowStatus.Deleted, Short)
            nresult = oPFFacade.Update(oPF)
            If nresult > 0 Then
                ParkingFeeBind(0)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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

    Private Sub dtgParkingFee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgParkingFee.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dtgParkingFee.SelectedIndex = -1
        dtgParkingFee.CurrentPageIndex = 0
        ParkingFeeBind(dtgParkingFee.CurrentPageIndex)
    End Sub

    Private Sub dtgParkingFee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgParkingFee.PageIndexChanged
        dtgParkingFee.CurrentPageIndex = e.NewPageIndex
        ParkingFeeBind(dtgParkingFee.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.parking_fee_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Parking Fee")
        End If
    End Sub
    Private Sub Initialize()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnDownload.Enabled = False
        btnGenerate.Enabled = False
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
            lblGenerate.Visible = SecurityProvider.Authorize(Context.User, SR.parking_fee_generate_bukti_potong_privilege) 'True '
            txtGenerate.Visible = SecurityProvider.Authorize(Context.User, SR.parking_fee_generate_bukti_potong_privilege) 'True '
            btnGenerate.Visible = SecurityProvider.Authorize(Context.User, SR.parking_fee_generate_bukti_potong_privilege) 'True '
        Else
            txtKodeDealer.ReadOnly = False
            lblGenerate.Visible = False
            txtGenerate.Visible = False
            btnGenerate.Visible = False
        End If

        BindPeriode()
        BindYear()
        BindStatus()
        BindCategory()
    End Sub

    Private Sub BindYear()
        Dim curYear As Integer = Date.Now.Year
        Dim startYear As Integer = curYear - 5
        Dim EndYear As Integer = curYear + 5
        Dim intYear As Integer = 0
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))

        ddlYear.Items.Clear()
        For intYear = startYear To EndYear
            ddlYear.Items.Add(intYear.ToString)
        Next

        ddlYear.Items.FindByValue(Date.Now.Year.ToString).Selected = True
    End Sub

    Private Sub BindPeriode()
        ddlPeriode.Items.Clear()
        ddlPeriode.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As ListItem In EnumParkingFeePeriod.GetList()
            ddlPeriode.Items.Add(item)
        Next
        ddlPeriode.SelectedIndex = 0
    End Sub

    Private Sub BindCategory()
        Try
            Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList()
            Dim blankItem As New listItem("Silahkan Pilih", 0)
            ddlCategory.Items.Add(blankItem)
            For Each item As Category In arrayListCategory
                Dim listItem As New listItem(item.CategoryCode, item.ID)
                listItem.Selected = False
                ddlCategory.Items.Add(listItem)
            Next
            If ddlCategory.Items.Count > 0 Then
                ddlCategory.SelectedIndex = 0
            Else
                ddlCategory.ClearSelection()
            End If


        Catch ex As Exception
            MessageBox.Show("Error Binding ddlKategori, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    Private Sub BindStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As ListItem In EnumParkingFeeStatus.RetrieveParkingFeeStatus()
            ddlStatus.Items.Add(item)
        Next
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub ParkingFeeBind(ByVal idxPage As Integer)
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtKodeDealer.Text.Trim() = String.Empty Then
                MessageBox.Show("Tentukan Dealer terlebih dahulu")
                Exit Sub
            End If
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If
        If ddlPeriode.SelectedValue.ToString <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Periode", MatchType.Exact, ddlPeriode.SelectedValue))
        End If
        If ddlStatus.SelectedValue.ToString <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If ddlCategory.SelectedValue.ToString <> "0" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Year", MatchType.Exact, ddlYear.SelectedValue.ToString))

        Dim totalRow As Integer = 0
        arlParkingFee = New FinishUnit.ParkingFeeFacade(User).RetrieveActiveList(criterias, idxPage + 1, dtgParkingFee.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        Dim arlPFDownload As ArrayList = New FinishUnit.ParkingFeeFacade(User).Retrieve(criterias)
        If (arlParkingFee.Count > 0) Then
            dtgParkingFee.DataSource = arlParkingFee
            dtgParkingFee.VirtualItemCount = totalRow
            dtgParkingFee.DataBind()
            sHelper.SetSession("ARLPARKINGFEE", arlPFDownload)
            btnDownload.Enabled = True
            btnGenerate.Enabled = True
        Else
            dtgParkingFee.DataSource = Nothing
            dtgParkingFee.VirtualItemCount = 0
            dtgParkingFee.DataBind()
            btnDownload.Enabled = False
            btnGenerate.Enabled = False
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Function PopulateParkingFee() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objPFFacade As New FinishUnit.ParkingFeeFacade(User)

        For Each oDataGridItem In dtgParkingFee.Items
            If oDataGridItem.ItemType = ListItemType.Item Or oDataGridItem.ItemType = ListItemType.AlternatingItem Then
                chkExport = oDataGridItem.FindControl("cbCheck")
                Dim id As Integer = 0
                If chkExport.Checked Then
                    Dim _pf As New KTB.Dnet.Domain.ParkingFee
                    Dim lblID As Label = CType(oDataGridItem.FindControl("lblID"), Label)
                    id = CType(lblID.Text, Integer)
                    _pf = objPFFacade.Retrieve(id)
                    If Not IsNothing(_pf) AndAlso _pf.ID > 0 Then
                        oExArgs.Add(_pf)
                    End If
                End If
            End If
        Next
        Return oExArgs
    End Function

    Private Sub DoDownload(ByVal arlPF As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar Parking Fee [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim ParkingFeeFile As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(ParkingFeeFile)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(ParkingFeeFile, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDataToExcell(sw, arlPF)

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

    Private Sub WriteDataToExcell(ByVal sw As StreamWriter, ByVal arlPF As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("DO STATUS - PARKING FEE")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        If (arlPF.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("N0" & tab)
            itemLine.Append("STATUS" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("GROUP DEALER" & tab)
            itemLine.Append("PERIODE" & tab)
            itemLine.Append("TAHUN" & tab)
            itemLine.Append("KATEGORI" & tab)
            itemLine.Append("DEBIT CHARGE" & tab)
            itemLine.Append("DEBIT MEMO" & tab)
            itemLine.Append("FAKTUR PAJAK" & tab)
            itemLine.Append("NO. JV" & tab)
            itemLine.Append("TOTAL BIAYA" & tab)
            itemLine.Append("PPN" & tab)
            itemLine.Append("DESKRIPSI" & tab)
            itemLine.Append("DIPOTONG DARI DEPOSIT A DEALER" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                Dim ppn As Decimal = 0

                For Each item As ParkingFee In arlPF
                    ppn = CalcHelper.GetPPNMasterByTaxTypeId(item.CreatedTime, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(CType(CInt(item.Status), EnumParkingFeeStatus.ParkingFeeStatus).ToString & tab)
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    itemLine.Append(item.Dealer.DealerName & tab)
                    itemLine.Append(item.Dealer.DealerGroup.GroupName & tab)
                    'itemLine.Append(EnumParkingFeePeriod.GetStringValue(item.Periode, item.Year) & tab)
                    itemLine.Append(EnumParkingFeePeriod.GetStringValue(item.Periode) & tab)
                    itemLine.Append(item.Year & tab)
                    If Not IsNothing(item.Category) Then
                        itemLine.Append(item.Category.CategoryCode & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    itemLine.Append(item.DebitChargeNumber & tab)
                    itemLine.Append(item.DebitMemoNumber & tab)
                    itemLine.Append(item.FakturPajakNo & tab)
                    itemLine.Append(item.AssignmentNumber & tab)
                    itemLine.Append(Decimal.Round(item.Amount, 0) & tab)
                    'itemLine.Append(Decimal.Round(item.Amount * CDec(10 / 100), 0) & tab)
                    itemLine.Append(CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=item.Amount) & tab)
                    itemLine.Append(item.Description & tab)
                    If Not IsNothing(item.DealerDepositA) Then
                        itemLine.Append(item.DealerDepositA.DealerCode & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
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

#End Region

End Class
