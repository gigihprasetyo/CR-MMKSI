Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports System.IO
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmServiceDueDateNotification
    Inherits System.Web.UI.Page

    Private _sesUpload As String = "FrmServiceDueDateNotification.UploadData"
    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objDealer = Session("DEALER")

        If Not IsPostBack Then
            lblSearchDealer.Attributes("onClick") = "ShowPPDealerSelection();"
            BindDDLPos()
            BindDDLEmailNotificationKind()
            InitiatePage()
            BindGrid(0)
        End If
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, Short) Then
            lblDealerCode.Visible = False
        Else
            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCode.Text = objDealer.DealerCode
        End If

    End Sub

    Sub BindGrid(ByVal pageindex As Integer)
        Dim PagedList As New ArrayList
        Dim crit As CriteriaComposite = CriteriaSearch()
        Dim oSorts As New SortCollection
        oSorts.Add(New Sort(GetType(ServiceDueDateNotification), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
        Dim arrDueDate As ArrayList = New ServiceDueDateNotificationFacade(User).Retrieve(crit, oSorts)
        If arrDueDate.Count > 0 Then
            PagedList = ArrayListPager.DoPage(arrDueDate, pageindex, dtgSPPO.PageSize)
        Else
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Due Date Reminder"))
            End If
        End If
        dtgSPPO.DataSource = PagedList
        dtgSPPO.VirtualItemCount = arrDueDate.Count()
        dtgSPPO.DataBind()
        dtgSPPO.CurrentPageIndex = 0
        If ViewState("vsUpload") <> "InsertUpload" Then
            dtgSPPO.Columns(8).Visible = False
        End If
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        BindGrid(0)
        btnFind.Enabled = True
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim nResult As Integer = -1
        Dim Ndata As Integer = 0
        Dim NSucces As Integer = 0
        Dim NError As String = ""

        If ViewState("vsUpload") = "InsertUpload" Then
            Ndata = CType(sessionHelper.GetSession(_sesUpload), ArrayList).Count()
            For Each ObjServiceDueDateNotification As ServiceDueDateNotification In sessionHelper.GetSession(_sesUpload)
                'cek row if exist in ServiceDueDateNotification -> update else insert
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceDueDateNotification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ServiceDueDateNotification), "Dealer.DealerCode", MatchType.Exact, ObjServiceDueDateNotification.Dealer.DealerCode))
                criterias.opAnd(New Criteria(GetType(ServiceDueDateNotification), "EmailDealer", MatchType.Exact, ObjServiceDueDateNotification.EmailDealer))
                criterias.opAnd(New Criteria(GetType(ServiceDueDateNotification), "EmailNotificationKind", MatchType.Exact, ObjServiceDueDateNotification.EmailNotificationKind))
                'criterias.opAnd(New Criteria(GetType(ServiceDueDateNotification), "PositionRecipient", MatchType.Exact, ObjServiceDueDateNotification.PositionRecipient))

                Dim data As ArrayList = New ArrayList
                data = New ServiceDueDateNotificationFacade(User).Retrieve(criterias)
                Dim ObjServiceDueDateNotificationFacade = New ServiceDueDateNotificationFacade(User)
                Try
                    If data.Count > 0 Then
                        ObjServiceDueDateNotification = CType(data(0), ServiceDueDateNotification)
                        nResult = ObjServiceDueDateNotificationFacade.Update(ObjServiceDueDateNotification)
                    Else
                        nResult = ObjServiceDueDateNotificationFacade.Insert(ObjServiceDueDateNotification)
                    End If
                    NSucces = NSucces + 1
                Catch ex2 As Exception
                    If NError.Trim.Length = 0 Then
                        NError = ObjServiceDueDateNotification.EmailDealer
                    Else
                        NError = NError & ",\n" & ObjServiceDueDateNotification.EmailDealer
                    End If
                End Try
            Next
            If NError.Trim.Length = 0 Then
                MessageBox.Show("Simpan Berhasil")
                ClearAll()
                btnFind_Click(Nothing, Nothing)
                ViewState("vsUpload") = ""
            Else
                MessageBox.Show("Berikut data yang Gagal disimpan \n " & NError)
            End If
        ElseIf ViewState("vsUpload") = "Edit" Then
            Dim editID As Integer = CInt(ViewState("editID"))
            Dim email As ServiceDueDateNotification = New ServiceDueDateNotificationFacade(User).Retrieve(editID)
            email.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
            email.EmailDealer = txtEmail.Text
            email.NameRecipient = txtNamaPenerima.Text
            email.PositionRecipient = ddlPosisiPenerima.SelectedValue
            email.EmailNotificationKind = ddlEmailNotificationKind.SelectedValue

            Dim _return As Integer = New ServiceDueDateNotificationFacade(User).Update(email)
            If _return > 0 Then
                MessageBox.Show("Simpan Berhasil")
                ClearAll()
                btnFind_Click(Nothing, Nothing)
            Else
                MessageBox.Show("Simpan Gagal")
            End If

        Else
            Dim errMessage As String = ""
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If txtDealerCode.Text.Trim.Length = 0 Then
                    errMessage = errMessage & "Kode Dealer tidak boleh kosong \n"
                End If
            End If

            Dim oDealer As Dealer = Nothing
            Try
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    oDealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
                Else
                    oDealer = New DealerFacade(User).Retrieve(lblDealerCode.Text.Trim)
                End If

                If oDealer.ID = 0 Then
                    errMessage = errMessage & "Dealer tidak ditemukan \n"
                End If
            Catch
                errMessage = errMessage & "Dealer tidak ditemukan \n"
            End Try

            If txtEmail.Text.Trim.Length = 0 Then
                errMessage = errMessage & "Email tidak boleh kosong \n"
            End If
            If Not CommonFunction.ValidateEmail(txtEmail.Text.Trim) Then
                errMessage = errMessage & "Email tidak valid \n"
            End If
            If txtNamaPenerima.Text.Trim.Length = 0 Then
                errMessage = errMessage & "Nama Penerima tidak boleh kosong \n"
            End If
            If ddlPosisiPenerima.SelectedIndex = 0 Then
                errMessage = errMessage & "Harap isi Posisi Penerima \n"
            End If
            If ddlEmailNotificationKind.SelectedIndex = 0 Then
                errMessage = errMessage & "Harap isi Jenis Notifikasi \n"
            End If

            If errMessage.Trim.Length > 0 Then
                MessageBox.Show(errMessage)
                Exit Sub
            End If

            Dim email As New ServiceDueDateNotification
            email.Dealer = oDealer
            email.EmailDealer = txtEmail.Text
            email.NameRecipient = txtNamaPenerima.Text
            email.PositionRecipient = ddlPosisiPenerima.SelectedValue
            email.EmailNotificationKind = ddlEmailNotificationKind.SelectedValue

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceDueDateNotification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ServiceDueDateNotification), "Dealer.DealerCode", MatchType.Exact, oDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(ServiceDueDateNotification), "EmailDealer", MatchType.Exact, txtEmail.Text))
            criterias.opAnd(New Criteria(GetType(ServiceDueDateNotification), "EmailNotificationKind", MatchType.Exact, ddlEmailNotificationKind.SelectedValue))

            Dim data As ArrayList = New ArrayList
            data = New ServiceDueDateNotificationFacade(User).Retrieve(criterias)
            Dim _return As Integer = -1
            If data.Count > 0 Then
                Dim stdCode As StandardCode = New StandardCodeFacade(User).GetByCategoryValue("EnumServiceEmailNotificationKind", ddlEmailNotificationKind.SelectedValue)
                MessageBox.Show(String.Format("Dealer {0} dengan email {1} dengan Jenis Notifikasi {2} sudah terdaftar ", email.Dealer.DealerCode, email.EmailDealer, stdCode.ValueDesc))
                Exit Sub
            Else
                _return = New ServiceDueDateNotificationFacade(User).Insert(email)
            End If

            If _return > 0 Then
                MessageBox.Show("Simpan Berhasil")
                ClearAll()
                btnFind_Click(Nothing, Nothing)
            Else
                MessageBox.Show("Simpan Gagal")
            End If
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        inputControl(True)
        ClearAll()
        btnFind_Click(Nothing, Nothing)
    End Sub

    Private Sub ClearAll()
        txtDealerCode.Text = ""
        txtEmail.Text = ""
        txtNamaPenerima.Text = ""
        ddlPosisiPenerima.SelectedIndex = 0
        ddlEmailNotificationKind.SelectedIndex = 0
        btnUpload.Enabled = True
        ViewState("vsUpload") = ""
        ViewState("editID") = ""
    End Sub

    Private Sub BindDDLPos()
        With ddlPosisiPenerima.Items
            .Add(New ListItem("Silahkan Pilih", "-1"))
            .Add(New ListItem("To", "to"))
            .Add(New ListItem("Cc", "cc"))
        End With
    End Sub

    Private Sub BindDDLEmailNotificationKind()
        With ddlEmailNotificationKind
            .Items.Clear()
            Dim criteriasTipePelanggan As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasTipePelanggan.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumServiceEmailNotificationKind"))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(StandardCode), "Sequence", Sort.SortDirection.ASC))
            .DataSource = New StandardCodeFacade(User).Retrieve(criteriasTipePelanggan, sortColl)
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceDueDateNotification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, Short) Then
            If txtDealerCode.Text.Trim.Length > 0 Then
                crit.opAnd(New Criteria(GetType(ServiceDueDateNotification), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
            End If
        Else
            crit.opAnd(New Criteria(GetType(ServiceDueDateNotification), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text))
        End If

        If txtEmail.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(ServiceDueDateNotification), "EmailDealer", MatchType.Exact, txtEmail.Text))
        End If

        If txtNamaPenerima.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(ServiceDueDateNotification), "NameRecipient", MatchType.Exact, txtNamaPenerima.Text))
        End If

        If ddlPosisiPenerima.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(ServiceDueDateNotification), "PositionRecipient", MatchType.Exact, ddlPosisiPenerima.SelectedValue))
        End If
        If ddlEmailNotificationKind.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(ServiceDueDateNotification), "EmailNotificationKind", MatchType.Exact, ddlEmailNotificationKind.SelectedValue))
        End If

        Return crit
    End Function

    Private Sub dtgSPPO_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgSPPO.PageIndexChanged
        dtgSPPO.CurrentPageIndex = e.NewPageIndex
        BindGrid(dtgSPPO.CurrentPageIndex)
    End Sub

    Private Sub dtgSPPO_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgSPPO.SortCommand
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
        dtgSPPO.SelectedIndex = -1
        dtgSPPO.CurrentPageIndex = 0
        BindGrid(dtgSPPO.CurrentPageIndex)
    End Sub

    Protected Sub dtgSPPO_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSPPO.ItemCommand
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim data As ServiceDueDateNotification = New ServiceDueDateNotificationFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
            Select Case e.CommandName
                Case "Edit" 'Insert New item to datagrid
                    txtDealerCode.Text = data.Dealer.DealerCode
                    txtEmail.Text = data.EmailDealer
                    txtNamaPenerima.Text = data.NameRecipient
                    ddlPosisiPenerima.SelectedValue = data.PositionRecipient.ToLower
                    ddlEmailNotificationKind.SelectedValue = data.EmailNotificationKind
                    btnUpload.Enabled = False
                    dfPartShop.Attributes("disable") = True
                    ViewState("vsUpload") = "Edit"
                    ViewState("editID") = e.Item.Cells(0).Text

                Case "Detail"
                    txtDealerCode.Text = data.Dealer.DealerCode
                    txtDealerCode.Enabled = False
                    txtEmail.Text = data.EmailDealer
                    txtEmail.Enabled = False
                    txtNamaPenerima.Text = data.NameRecipient
                    txtNamaPenerima.Enabled = False
                    ddlPosisiPenerima.SelectedValue = data.PositionRecipient.ToLower
                    ddlEmailNotificationKind.SelectedValue = data.EmailNotificationKind
                    ddlPosisiPenerima.Enabled = False
                    ddlEmailNotificationKind.Enabled = False
                    btnUpload.Enabled = False
                    dfPartShop.Attributes("disable") = True
                    btnFind.Enabled = False
                    btnSave.Enabled = False

                Case "Delete"
                    If ViewState("vsUpload") = "InsertUpload" Then
                        Dim arlData As ArrayList = CType(sessionHelper.GetSession(_sesUpload), ArrayList)
                        arlData.RemoveAt(e.Item.ItemIndex)
                        btnSave.Enabled = True
                        BindUpload(arlData)
                        sessionHelper.SetSession(_sesUpload, arlData)
                        Exit Sub
                    Else
                        Dim fac As New ServiceDueDateNotificationFacade(User)
                        fac.Delete(data)
                    End If
            End Select

            btnFind_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub inputControl(ByVal va As Boolean)
        txtDealerCode.Enabled = va
        txtEmail.Enabled = va
        txtNamaPenerima.Enabled = va
        ddlPosisiPenerima.Enabled = va
        ddlEmailNotificationKind.Enabled = va
        btnSave.Enabled = va
        btnFind.Enabled = va
        btnUpload.Enabled = va
        dfPartShop.Attributes("disable") = Not va
    End Sub

    Protected Sub dtgSPPO_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgSPPO.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objServiceDueDateNotification As New ServiceDueDateNotification
            objServiceDueDateNotification = e.Item.DataItem

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgSPPO.CurrentPageIndex * dtgSPPO.PageSize)
            If ViewState("vsUpload") = "InsertUpload" Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnDetail"), LinkButton).Visible = False
                If objServiceDueDateNotification.ErrorMessage.Trim.Length <> 0 Then
                    CType(e.Item.FindControl("lblNote"), Label).Text = objServiceDueDateNotification.ErrorMessage
                Else
                    CType(e.Item.FindControl("lblNote"), Label).Text = "OK"
                End If
            End If
            Dim lblEmailNotificationKind As Label = CType(e.Item.FindControl("lblEmailNotificationKind"), Label)
            lblEmailNotificationKind.Text = CommonFunction.GetEnumDescription(objServiceDueDateNotification.EmailNotificationKind, "EnumServiceEmailNotificationKind")
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If (Not dfPartShop.PostedFile Is Nothing) AndAlso (dfPartShop.PostedFile.ContentLength > 0) Then
            ViewState("vsUpload") = "InsertUpload"

            Dim fileExt As String = Path.GetExtension(dfPartShop.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If

            dtgSPPO.DataSource = New ArrayList
            dtgSPPO.DataBind()
            dtgSPPO.Visible = False

            Me.btnSave.Enabled = False

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Dim msg As String = ""
            Try
                Dim SrcFile As String = Path.GetFileName(dfPartShop.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(dfPartShop.PostedFile.InputStream, targetFile)
                    Dim parser As UploadServiceDueDateNotificationParser = New UploadServiceDueDateNotificationParser(dfPartShop.PostedFile.ContentType.ToString)

                    '-- Parse data file and store result into arraylist
                    Dim arlSPDueDateNotification As ArrayList = CType(parser.ParseExcelNoTransaction(targetFile, "[Sheet1$A1:C152]", "User"), ArrayList)

                    Dim i As Integer
                    btnSave.Enabled = True
                    If arlSPDueDateNotification.Count <= 0 Then
                        btnSave.Enabled = False
                    End If

                    sessionHelper.SetSession(_sesUpload, arlSPDueDateNotification)
                    dtgSPPO.DataSource = arlSPDueDateNotification '-- Reset datagrid first
                    dtgSPPO.CurrentPageIndex = 0
                    BindUpload(arlSPDueDateNotification)
                    dtgSPPO.Visible = True
                End If
            Catch ex As Exception
                MessageBox.Show("Fail To Process " & ex.Message)
            Finally
                imp.StopImpersonate()
                imp = Nothing
            End Try
        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If
    End Sub

    Private Sub BindUpload(ByVal _arlData As ArrayList)
        Dim totalRow As Integer = 0

        Try
            If Not IsNothing(_arlData) Then

                For Each item As ServiceDueDateNotification In _arlData
                    If item.ErrorMessage.Trim.Length > 0 Then
                        btnSave.Enabled = False
                        Exit For
                    End If
                Next
                totalRow = _arlData.Count
                dtgSPPO.DataSource = _arlData
                dtgSPPO.VirtualItemCount = totalRow
                dtgSPPO.DataBind()
                btnFind.Enabled = False
                dtgSPPO.Columns(8).Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub btnSample_Click(sender As Object, e As EventArgs) Handles btnSample.Click
        Dim strName As String = "ServiceNotificationTemplate.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\" & strName)
    End Sub
End Class