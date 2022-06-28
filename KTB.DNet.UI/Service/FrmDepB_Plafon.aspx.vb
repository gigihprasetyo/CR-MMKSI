Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Security
Imports System.Text
Imports System.IO

Public Class FrmDepB_Plafon
    Inherits System.Web.UI.Page

#Region "Private Variables"
    Dim sessHelper As New SessionHelper
#End Region

    Dim _input_daftar_plafon_Privilege As Boolean = False
    Dim _lihat_daftar_plafon_Privilege As Boolean = False

#Region "Event Handler"
    Private Sub InitiateAuthorization()
        Dim objDealer As Dealer = Me.sessHelper.GetSession("DEALER")

        _lihat_daftar_plafon_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_plafon_Privilege)

        If Not _lihat_daftar_plafon_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Daftar Plafon")
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then

            _input_daftar_plafon_Privilege = SecurityProvider.Authorize(Context.User, SR.input_daftar_plafon_Privilege)
            If Not _input_daftar_plafon_Privilege Then
                btnSimpan.Visible = False
            End If

        Else
            btnSimpan.Visible = False

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            Me.hdnMCPConfirmation.Value = "-1"
            Initialize()
            BindProductCategory()
            BindYear()
            BindTipeKewajiban()
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
        Else
            If Me.hdnMCPConfirmation.Value = "1" Then
                If Not IsNothing(sessHelper.GetSession("DepositBPlafonIdToDelete")) Then
                    Dim plafonID As Integer = CInt(sessHelper.GetSession("DepositBPlafonIdToDelete"))
                    DeletePlafon(plafonID)
                    ClearData()
                    Me.hdnMCPConfirmation.Value = "-1"
                End If
            End If
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        dgPlafonList.CurrentPageIndex = 0
        BindDataGrid(dgPlafonList.CurrentPageIndex)
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim strMessage As String = String.Empty
        strMessage = Validation()
        If strMessage = String.Empty Then
            SimpanPlafon()
        Else
            MessageBox.Show(strMessage)
        End If
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dgPlafonList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPlafonList.ItemCommand
        If (e.CommandName = "View") Then
            txtKodeDealer.ReadOnly = True
            ViewState.Add("vsProcess", "View")
            ViewPlafon(CInt(e.Item.Cells(0).Text), False)
            dgPlafonList.SelectedIndex = e.Item.ItemIndex
            ViewState.Add("idxRow", e.Item.ItemIndex)
        ElseIf e.CommandName = "Edit" Then
            txtKodeDealer.ReadOnly = True
            ViewState.Add("vsProcess", "Edit")
            ViewPlafon(CInt(e.Item.Cells(0).Text), True)
            dgPlafonList.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then

            If Me.hdnMCPConfirmation.Value = "-1" Then
                MessageBox.Confirm("Apakah anda yakin akan menhapus data ini?", "hdnMCPConfirmation")
                sessHelper.SetSession("DepositBPlafonIdToDelete", e.Item.Cells(0).Text)
                'Else
                '    DeletePlafon(e.Item.Cells(0).Text)
                '    ClearData()
            End If

        End If
    End Sub

    Private Sub dgPlafonList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPlafonList.ItemDataBound
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objDepositBPlafon As DepositBPlafon = CType(e.Item.DataItem, DepositBPlafon)
            If Not IsNothing(objDepositBPlafon) Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgPlafonList.CurrentPageIndex * dgPlafonList.PageSize)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = objDepositBPlafon.Dealer.DealerCode
                Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
                lblDealerName.Text = objDepositBPlafon.Dealer.DealerName
                Dim lblProduct As Label = CType(e.Item.FindControl("lblProduct"), Label)
                lblProduct.Text = objDepositBPlafon.ProductCategory.Code
                Dim lblGrade As Label = CType(e.Item.FindControl("lblGrade"), Label)
                lblGrade.Text = DepositBEnum.GetStringValueGradeDealer(objDepositBPlafon.GradeDealer)
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    lbtnEdit.Visible = False
                    lbtnDelete.Visible = False
                Else
                    If Not _input_daftar_plafon_Privilege Then
                        lbtnEdit.Visible = False
                        lbtnDelete.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dgPlafonList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgPlafonList.PageIndexChanged
        dgPlafonList.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgPlafonList.CurrentPageIndex)
    End Sub

    Private Sub dgPlafonList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgPlafonList.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgPlafonList.SelectedIndex = -1
        dgPlafonList.CurrentPageIndex = 0
        BindDataGrid(dgPlafonList.CurrentPageIndex)
    End Sub
#End Region

#Region "Custom"


    Private Sub Initialize()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'lblSearchDealer.Visible = False
            txtKodeDealer.Attributes.Add("readonly", "readonly")
            btnSimpan.Enabled = False
            btnSimpan.Visible = False
        End If
    End Sub

    Private Sub BindProductCategory()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, False, companyCode)
    End Sub

    Private Sub BindTipeKewajiban()
        ddlGrade.Items.Clear()

        Dim items As New ArrayList
        items.Add(New EnumProperty(-1, "Semua Grade"))
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveGradeDealer()
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next
        ddlGrade.DataSource = items
        ddlGrade.DataTextField = "NameType"
        ddlGrade.DataValueField = "ValType"
        ddlGrade.DataBind()
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

    Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim oDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        'If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    If txtKodeDealer.Text.Trim = String.Empty Then
        '        MessageBox.Show("Tentukan kode dealer terlebih dahulu")
        '        Exit Sub
        '    End If
        'End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        Else
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If oDealer.DealerGroup.ID = 21 Then 'single dealer
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "Dealer.ID", MatchType.Exact, oDealer.ID))
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "Dealer.DealerGroup.ID", MatchType.Exact, oDealer.DealerGroup.ID))
                End If

            End If
        End If
        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "ProductCategory.ID", MatchType.Exact, CInt(ddlProductCategory.SelectedValue)))
        End If
        If CInt(ddlGrade.SelectedValue) >= 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "GradeDealer", MatchType.Exact, CInt(ddlGrade.SelectedValue)))
        End If
        If CInt(ddlYear.SelectedValue) <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "PeriodePlafon", MatchType.Exact, CShort(ddlYear.SelectedValue)))
        End If

        Dim totalRow As Integer = 0
        Dim arlDepositBPlafon As New ArrayList
        arlDepositBPlafon = New DepositBPlafonFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgPlafonList.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))

        Dim arlDepositBPlafonToDownload As New ArrayList
        arlDepositBPlafonToDownload = New DepositBPlafonFacade(User).Retrieve(criterias)
        sessHelper.SetSession("DepositBPlafonToDownload", arlDepositBPlafonToDownload)

        If (arlDepositBPlafon.Count > 0) Then
            dgPlafonList.Visible = True
            dgPlafonList.DataSource = arlDepositBPlafon
            dgPlafonList.VirtualItemCount = totalRow
            dgPlafonList.DataBind()
        Else
            dgPlafonList.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private Function Validation() As String
        Dim msg As String = String.Empty
        If txtKodeDealer.Text.Trim = String.Empty Then
            msg = "Kode dealer tidak boleh kosong."
        End If
        If ddlProductCategory.SelectedValue.ToString() = "" OrElse ddlProductCategory.SelectedValue.ToString() = "-1" Then
            msg = msg + "\n" + " Tentukan Produk."
        End If
        If ddlGrade.SelectedIndex = 0 Then
            msg = msg + "\n" + " Tentukan Grade."
        End If

        If txtPlafon.Text.Trim = "" Then
            msg = msg + "\n" + " Nilai plafon tidak boleh kosong."
        End If

        If Not IsNumeric(txtPlafon.Text.Trim) Then
            msg = msg + "\n" + " Nilai plafon harus angka."
        End If

        Return msg
    End Function

    Private Sub SimpanPlafon()
        Try
            If CType(ViewState("vsProcess"), String) = "Edit" Then
                UpdatePlafon()
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
                BindDataGrid(dgPlafonList.CurrentPageIndex)
                If Not IsNothing(ViewState("idxRow")) Then
                    dgPlafonList.SelectedIndex = CType(ViewState("idxRow"), Integer)
                End If
                btnSimpan.Enabled = True
            Else
                InsertPlafon()
                BindDataGrid(dgPlafonList.CurrentPageIndex)
            End If

        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    Private Sub SavePlafon()
        Dim arlPlafonInsert As New ArrayList
        Dim arlPlafonUpdate As New ArrayList
        Dim objFacade As DepositBPlafonFacade = New DepositBPlafonFacade(User)
        Dim vResult As Integer
        Try
            Dim objProductCategory As ProductCategory = New KTB.DNet.BusinessFacade.FinishUnit.ProductCategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
            Dim arrDealer As String() = txtKodeDealer.Text.Split(";")
            For Each dealerCode As String In arrDealer
                Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(dealerCode)
                If Not IsNothing(objDealer) Then

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "Dealer.ID", MatchType.Exact, objDealer.ID))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "ProductCategory.ID", MatchType.Exact, objProductCategory.ID))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "GradeDealer", MatchType.Exact, CInt(ddlGrade.SelectedValue)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "PeriodePlafon", MatchType.Exact, CInt(ddlYear.SelectedValue)))

                    Dim arlDepositBPlafon As New ArrayList
                    arlDepositBPlafon = New DepositBPlafonFacade(User).Retrieve(criterias)
                    If arlDepositBPlafon.Count > 0 Then
                        Dim obj As DepositBPlafon = CType(arlDepositBPlafon(0), DepositBPlafon)
                        'Update
                        obj.JumlahPlafon = CDec(txtPlafon.Text)
                        arlPlafonUpdate.Add(obj)
                    Else
                        'Insert New
                        Dim obj As DepositBPlafon = New DepositBPlafon
                        obj.Dealer = objDealer
                        obj.ProductCategory = objProductCategory
                        obj.GradeDealer = CInt(ddlGrade.SelectedValue)
                        obj.PeriodePlafon = CShort(ddlYear.SelectedValue)
                        obj.JumlahPlafon = CDec(txtPlafon.Text)
                        arlPlafonInsert.Add(obj)
                    End If

                End If
            Next
            Dim errMsgNew As String = String.Empty
            Dim errMsgUpdate As String = String.Empty
            Dim iNew As Integer = 0
            Dim iUpdate As Integer = 0

            If arlPlafonInsert.Count > 0 Then
                'vResult = objFacade.InsertByList(arlPlafonInsert)
                For Each obj As DepositBPlafon In arlPlafonInsert
                    vResult = objFacade.Insert(obj)
                    If vResult > -1 Then
                        iNew = iNew + 1
                    Else
                        errMsgNew = errMsgNew + ";" + obj.Dealer.DealerCode
                    End If
                Next
            End If

            If arlPlafonUpdate.Count > 0 Then
                For Each obj As DepositBPlafon In arlPlafonInsert
                    vResult = objFacade.Update(obj)
                    If vResult > -1 Then
                        iUpdate = iUpdate + 1
                    Else
                        errMsgUpdate = errMsgUpdate + ";" + obj.Dealer.DealerCode
                    End If
                Next
            End If

            If (errMsgNew = String.Empty) And (errMsgUpdate = String.Empty) Then
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
                btnSimpan.Enabled = False
            Else
                Dim errMsg As String = String.Empty
                If errMsgNew <> String.Empty Then
                    errMsg = "Data gagal simpan : " + errMsgNew
                End If
                If errMsgUpdate <> String.Empty Then
                    If errMsgNew <> String.Empty Then
                        errMsg = errMsg + Environment.NewLine
                    End If
                    errMsg = errMsg + " Data gagal update : " + errMsgUpdate
                End If
                MessageBox.Show(errMsg)
            End If

        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try

    End Sub

    Private Sub InsertPlafon()
        Dim arlPlafon As New ArrayList
        Dim objFacade As DepositBPlafonFacade = New DepositBPlafonFacade(User)
        Dim vResult As Integer
        Try

            Dim objProductCategory As ProductCategory = New KTB.DNet.BusinessFacade.FinishUnit.ProductCategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
            Dim arrDealer As String() = txtKodeDealer.Text.Split(";")
            For Each dealerCode As String In arrDealer
                Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(dealerCode)
                If Not IsNothing(objDealer) Then
                    If Not IsPlafonExist(objDealer.ID, objProductCategory.ID, CInt(ddlGrade.SelectedValue), CInt(ddlYear.SelectedValue)) Then
                        Dim obj As DepositBPlafon = New DepositBPlafon
                        obj.Dealer = objDealer
                        obj.ProductCategory = objProductCategory
                        obj.GradeDealer = CInt(ddlGrade.SelectedValue)
                        obj.PeriodePlafon = CShort(ddlYear.SelectedValue)
                        obj.JumlahPlafon = CDec(txtPlafon.Text)
                        arlPlafon.Add(obj)
                    Else
                        MessageBox.Show("Plafon sudah ada untuk produk " & ddlProductCategory.SelectedItem.Text & " dengan grade " & ddlGrade.SelectedItem.Text & ", silahkan lakukan perubahan melalui update.")
                        Exit Sub
                        'UpdatePlafon()
                    End If
                End If
            Next
            If arlPlafon.Count > 0 Then
                vResult = objFacade.InsertByList(arlPlafon)
                If vResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    ClearData()
                    btnSimpan.Enabled = False
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try

    End Sub

    Private Sub UpdatePlafon()
        Try
            Dim objPlafon As DepositBPlafon = CType(Session.Item("vsDepositBPlafon"), DepositBPlafon)

            Dim objProductCategory As ProductCategory = New KTB.DNet.BusinessFacade.FinishUnit.ProductCategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
            If Not IsNothing(objPlafon) Then
                objPlafon.ProductCategory = objProductCategory
                objPlafon.GradeDealer = CInt(ddlGrade.SelectedValue)
                objPlafon.PeriodePlafon = CShort(ddlYear.SelectedValue)
                objPlafon.JumlahPlafon = CDec(txtPlafon.Text)
                objPlafon.ProductCategory = New ProductCategory(Me.ddlProductCategory.SelectedValue)
                Dim nResult = New DepositBPlafonFacade(User).Update(objPlafon)
                'If nResult <> -1 Then
                '    MessageBox.Show(SR.SaveSuccess)
                '    ClearData()
                '    btnSimpan.Enabled = False
                'Else
                '    MessageBox.Show(SR.SaveFail)
                'End If
            End If
        Catch ex As Exception
            'MessageBox.Show(SR.SaveFail)
        End Try

    End Sub

    Private Function IsPlafonExist(ByVal dealerID As Integer, ByVal category As Integer, ByVal grade As Integer, ByVal year As Integer) As Boolean
        Dim vReturn As Boolean = False
        Try
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "Dealer.ID", MatchType.Exact, dealerID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "ProductCategory.ID", MatchType.Exact, category))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "GradeDealer", MatchType.Exact, grade))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "PeriodePlafon", MatchType.Exact, year))

            Dim arlDepositBPlafon As New ArrayList
            arlDepositBPlafon = New DepositBPlafonFacade(User).Retrieve(criterias)
            If arlDepositBPlafon.Count > 0 Then
                Session.Add("vsDepositBPlafon", CType(arlDepositBPlafon(0), DepositBPlafon))
                vReturn = True
            End If
        Catch ex As Exception

        End Try

        Return vReturn
    End Function

    Private Sub ClearData()
        lblSearchDealer.Visible = True
        txtKodeDealer.Text = String.Empty
        ddlGrade.SelectedIndex = 0
        ddlProductCategory.SelectedIndex = 0
        ddlYear.SelectedValue = CInt(Date.Now.Year)
        txtPlafon.Text = ""
        btnSimpan.Enabled = True
        dgPlafonList.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
        Session.Add("vsDepositBPlafon", Nothing)
    End Sub

    Private Sub ViewPlafon(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objDepositBPlafon As DepositBPlafon = New DepositBPlafonFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsDepositBPlafon", objDepositBPlafon)
        txtKodeDealer.Text = objDepositBPlafon.Dealer.DealerCode
        ddlProductCategory.SelectedValue = objDepositBPlafon.ProductCategory.ID
        ddlGrade.SelectedValue = objDepositBPlafon.GradeDealer
        ddlYear.SelectedValue = objDepositBPlafon.PeriodePlafon
        txtPlafon.Text = FormatNumber(objDepositBPlafon.JumlahPlafon, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)

        lblSearchDealer.Visible = EditStatus
        txtKodeDealer.Enabled = EditStatus
        ddlProductCategory.Enabled = EditStatus
        ddlGrade.Enabled = EditStatus
        ddlYear.Enabled = EditStatus
        txtPlafon.Enabled = EditStatus
        lblSearchDealer.Visible = EditStatus
        btnSimpan.Enabled = EditStatus
        If CType(ViewState("vsProcess"), String) = "Edit" Then
            lblSearchDealer.Visible = False
            txtKodeDealer.Enabled = False
            ddlProductCategory.Enabled = False
        End If

    End Sub

    Private Sub DeletePlafon(ByVal nID As Integer)

        Try
            Dim facade As DepositBPlafonFacade = New DepositBPlafonFacade(User)
            Dim objDepositBPlafon As DepositBPlafon = New DepositBPlafonFacade(User).Retrieve(nID)
            objDepositBPlafon.RowStatus = DBRowStatus.Deleted
            facade.Delete(objDepositBPlafon)
            dgPlafonList.CurrentPageIndex = 0
            BindDataGrid(dgPlafonList.CurrentPageIndex)
            MessageBox.Show(SR.DeleteSucces)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try
    End Sub
#End Region

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim data As ArrayList = CType(sessHelper.GetSession("DepositBPlafonToDownload"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub


    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String = "DepositBPlafon"

        sFileName = sFileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim DepositBPlafon As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositBPlafon)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(DepositBPlafon, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)

                WriteDepositBPlafonData(sw, data)

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub


    Private Sub WriteDepositBPlafonData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Service - Daftar Plafon Deposit B")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Produk" & tab)
            itemLine.Append("Periode" & tab)
            itemLine.Append("Grade" & tab)
            itemLine.Append("Plafon" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            Dim DealerCode As String = ""
            Dim ProductCategoryCode As String = String.Empty

            For Each item As DepositBPlafon In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(item.Dealer.DealerCode & tab)
                itemLine.Append(item.Dealer.DealerName & tab)
                itemLine.Append(item.ProductCategory.Code & tab)
                itemLine.Append(item.PeriodePlafon.ToString() & tab)
                itemLine.Append(DepositBEnum.GetStringValueGradeDealer(item.GradeDealer) & tab)
                itemLine.Append(Val(item.JumlahPlafon).ToString & tab)
                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub
End Class