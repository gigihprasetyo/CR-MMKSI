#Region "Custom Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.Utility
#End Region

Public Class FrmAuditScheduleSingle
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents ddlAuditor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPhotoList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblAuditorName As System.Web.UI.WebControls.Label
    Protected WithEvents lblAuditScheduleEndDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblAuditScheduleSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblAuditScheduleStartDate As System.Web.UI.WebControls.Label
    Protected WithEvents phScript As System.Web.UI.WebControls.PlaceHolder

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Const SES_objAuditScheduleDealer As String = "SES_objAuditScheduleDealer"
#End Region
#Region "Deklarasi"
    Dim sHelper As New SessionHelper
#End Region


#Region "Sessions"
    Private Property IDEdit() As String
        Get
            Dim objValue = sHelper.GetSession("IDEdit")
            If objValue Is Nothing Then
                objValue = String.Empty
            End If

            Return objValue
        End Get
        Set(ByVal Value As String)
            sHelper.SetSession("IDEdit", Value)
        End Set
    End Property
    Private Property SesPhotoList() As ArrayList
        Get
            Dim arlListPhoto As ArrayList = sHelper.GetSession("arlListPhoto")
            If arlListPhoto Is Nothing Then
                arlListPhoto = New ArrayList
                sHelper.SetSession("arlListPhoto", arlListPhoto)
            End If

            Return arlListPhoto
        End Get
        Set(ByVal Value As ArrayList)
            sHelper.SetSession("arlListPhoto", Value)
        End Set
    End Property

    Private Property SesAuditScheduleDealer() As AuditScheduleDealer
        Get
            Return CType(sHelper.GetSession(SES_objAuditScheduleDealer), AuditScheduleDealer)
        End Get
        Set(ByVal Value As AuditScheduleDealer)
            sHelper.SetSession(SES_objAuditScheduleDealer, Value)
        End Set
    End Property
#End Region

#Region "Custom Method"
    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(SalesmanHeader.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= SalesmanHeader.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function
    Private Function UploadFile(ByVal file As HttpPostedFile) As Byte()
        Dim nResult() As Byte

        Try
            If IsValidPhoto(file) Then
                Dim inStream As System.IO.Stream = file.InputStream()
                Dim ByteRead(SalesmanHeader.MAX_PHOTO_SIZE) As Byte
                Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, SalesmanHeader.MAX_PHOTO_SIZE)
                If ReadCount = 0 Then
                    Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                End If
                ReDim nResult(ReadCount)
                Array.Copy(ByteRead, nResult, ReadCount)
            Else
                MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                        CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB")
            End If
        Catch
            'Throw
        End Try

        Return nResult
    End Function

    Private Sub GenerateJSUpdateAuditorInfo(ByVal objAuditScheduleDealer As AuditScheduleDealer)
        Dim objAuditDealer As AuditScheduleDealer = objAuditScheduleDealer
        Dim arlAuditors As ArrayList = objAuditDealer.AuditSchedule.AuditScheduleAuditors

        Dim sbArrJSObjectAuditorInfo As New System.Text.StringBuilder

        'We must build this javascript code string
        '{
        '"0":{"JadwalAuditStartDate": "31/12/2007", "JadwalAuditEndDate":"", "AuditorName":"AuditorName" }
        '}
        sbArrJSObjectAuditorInfo.Append("<script language='javascript'>")
        sbArrJSObjectAuditorInfo.Append("var hashAuditorInfo = {")

        sbArrJSObjectAuditorInfo.Append("'-1'")
        sbArrJSObjectAuditorInfo.Append(":")
        sbArrJSObjectAuditorInfo.Append("{'JadwalAuditStartDate':'',")
        sbArrJSObjectAuditorInfo.Append("'JadwalAuditEndDate':'',")
        sbArrJSObjectAuditorInfo.Append("'AuditorName':''}")

        For Each auditor As AuditScheduleAuditor In arlAuditors
            sbArrJSObjectAuditorInfo.Append(",")
            sbArrJSObjectAuditorInfo.Append("'" + auditor.ID.ToString + "'")
            sbArrJSObjectAuditorInfo.Append(":")
            sbArrJSObjectAuditorInfo.Append("{'JadwalAuditStartDate':" + "'" + auditor.StartDate.ToString("dd/MM/yyyy") + "',")
            sbArrJSObjectAuditorInfo.Append("'JadwalAuditEndDate':" + "'" + auditor.EndDate.ToString("dd/MM/yyyy") + "',")
            sbArrJSObjectAuditorInfo.Append("'AuditorName':" + "'" + auditor.Auditor + "'}")
        Next

        sbArrJSObjectAuditorInfo.Append("};")
        Dim strUpdateArrayInfo As String = "updateAuditorInfo(document.all." + ddlAuditor.ClientID + ", hashAuditorInfo, document.all." + _
                lblAuditScheduleStartDate.ClientID + ", document.all." + lblAuditScheduleSeparator.ClientID + _
                ", document.all." + lblAuditScheduleEndDate.ClientID + ", document.all." + _
                lblAuditorName.ClientID + ")"
        sbArrJSObjectAuditorInfo.Append(strUpdateArrayInfo)
        sbArrJSObjectAuditorInfo.Append("</script>")

        'Page.RegisterClientScriptBlock("sbArrJSObjectAuditorInfo", sbArrJSObjectAuditorInfo.ToString())

        phScript.Controls.Add(New LiteralControl(sbArrJSObjectAuditorInfo.ToString()))
    End Sub

    Private Sub BindDDLAuditor(ByVal ddlId As DropDownList, ByVal objAuditScheduleDealer As AuditScheduleDealer)
        Dim ddlAuditor As DropDownList = ddlId

        Dim objAuditDealer As AuditScheduleDealer = objAuditScheduleDealer
        Dim arlAuditors As ArrayList = objAuditDealer.AuditSchedule.AuditScheduleAuditors

        ddlAuditor.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

        Dim sbArrJSObjectAuditorInfo As New System.Text.StringBuilder


        For Each auditor As AuditScheduleAuditor In arlAuditors
            ddlAuditor.Items.Add(New ListItem(auditor.AuditorTypeDesc & " - " & auditor.Auditor, auditor.ID.ToString()))
        Next

        ddlAuditor.Attributes.Add("onchange", "updateAuditorInfo(this, hashAuditorInfo, document.all." + _
                lblAuditScheduleStartDate.ClientID + ", document.all." + lblAuditScheduleSeparator.ClientID + _
                ", document.all." + lblAuditScheduleEndDate.ClientID + ", document.all." + _
                lblAuditorName.ClientID + ")")

        If Not objAuditScheduleDealer.AuditScheduleAuditor Is Nothing Then
            ddlAuditor.SelectedValue = objAuditScheduleDealer.AuditScheduleAuditor.ID.ToString
        End If
    End Sub

    Private Sub BindToControl(ByVal IDEdit As Integer)
        Dim objAuditScheduleDealer As AuditScheduleDealer = New AuditScheduleDealerFacade(User).Retrieve(IDEdit)
        SesAuditScheduleDealer = objAuditScheduleDealer

        lblPeriode.Text = objAuditScheduleDealer.AuditSchedule.AuditParameter.Period.ToString()
        lblDealerCode.Text = objAuditScheduleDealer.Dealer.DealerCode.ToString() + " / " + objAuditScheduleDealer.Dealer.DealerName
        If Not objAuditScheduleDealer.AuditScheduleAuditor Is Nothing Then
            lblAuditScheduleStartDate.Text = objAuditScheduleDealer.AuditScheduleAuditor.StartDate.ToString("dd/MM/yyyy")
            lblAuditScheduleEndDate.Text = objAuditScheduleDealer.AuditScheduleAuditor.EndDate.ToString("dd/MM/yyyy")
            lblAuditorName.Text = objAuditScheduleDealer.AuditScheduleAuditor.Auditor
        Else
            lblAuditScheduleStartDate.Text = String.Empty
            lblAuditScheduleSeparator.Text = String.Empty
            lblAuditScheduleEndDate.Text = String.Empty
            lblAuditorName.Text = String.Empty
        End If

        BindDDLAuditor(ddlAuditor, objAuditScheduleDealer)

        If objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos Is Nothing Then
            Dim arl As ArrayList = New ArrayList
            dtgPhotoList.DataSource = arl
            SesPhotoList = arl
        Else
            Dim arl As ArrayList = objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos
            dtgPhotoList.DataSource = arl
            SesPhotoList = arl
        End If

        Me.IDEdit = IDEdit
        dtgPhotoList.DataBind()
    End Sub

    Private Sub AddDataToGrid(ByVal e As DataGridCommandEventArgs)
        Dim txtDesc As TextBox = e.Item.FindControl("txtFooterDesc")
        Dim txtItemDesc As TextBox = e.Item.FindControl("txtFooterItemDesc")

        'If IDEdit <> "" Then
        '    ''update
        '    'Dim objAuditParam As AuditParameter = sHelper.GetSession("objAuditParameter")
        '    'If txtDesc.Text = "" Then
        '    '    MessageBox.Show("Silahkan isi informasi Keterangan")
        '    'Else
        '    '    Dim objParamFoto As New AuditParameterPhoto
        '    '    objParamFoto.Description = txtDesc.Text
        '    '    objParamFoto.AuditParameter = objAuditParam
        '    '    If (New AuditParameterPhotoFacade(User).Insert(objParamFoto) <> -1) Then
        '    '        BindToControl(CInt(viewstate("IDEdit")))
        '    '    Else
        '    '        MessageBox.Show("Gagal insert deskripsi")
        '    '    End If
        '    'End If
        'Else
        'insert new
        If txtDesc.Text = "" Then
            MessageBox.Show("Silahkan isi informasi Keterangan")
            Return
        End If
        If txtItemDesc.Text.Trim().Length <= 0 Then
            MessageBox.Show("Silahkan isi informasi Keterangan Item")
            Return
        End If

        Dim arlListPhoto As ArrayList = SesPhotoList
        Dim objAuditfoto As New AuditParameterPhoto
        objAuditfoto.Description = txtDesc.Text

        Dim objAuditScheduleReport As New AuditScheduleDealerReport
        objAuditScheduleReport.ItemDesc = txtItemDesc.Text
        objAuditfoto.AuditScheduleDealerReports.Add(objAuditScheduleReport)

        arlListPhoto.Add(objAuditfoto)

        SesPhotoList = arlListPhoto
        BindToGridTemp()

        'End If

    End Sub

    Private Sub BindToGridTemp()
        Dim arlFotoTemp As ArrayList = SesPhotoList

        dtgPhotoList.DataSource = arlFotoTemp
        dtgPhotoList.DataBind()
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load        
        If Not IsPostBack Then
            Dim IDEdit As String = Me.IDEdit
            If IDEdit <> "" Then
                Me.IDEdit = IDEdit

                BindToControl(CInt(IDEdit))
            End If
        End If
        GenerateJSUpdateAuditorInfo(SesAuditScheduleDealer)
    End Sub

    Private Sub dtgPhotoList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPhotoList.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            'BindDDL(e, "ddlFAuditorType") '--bindAuditorType
            'CType(e.Item.FindControl("lblFPopUpDealer"), Label).Attributes("OnClick") = "ShowPopUpDealerOne();"

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                    New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
            End If

            Dim txtItemDesc As TextBox = e.Item.FindControl("txtItemDesc")
            Dim objAuditScheduleDealer As AuditScheduleDealer = SesAuditScheduleDealer
            Dim objAuditParameterPhoto As AuditParameterPhoto = CType(objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos(e.Item.ItemIndex), AuditParameterPhoto)
            If Not txtItemDesc Is Nothing Then
                If (objAuditParameterPhoto.AuditScheduleDealerReports.Count > 0) Then
                    txtItemDesc.Text = CType(objAuditParameterPhoto.AuditScheduleDealerReports(0), AuditScheduleDealerReport).ItemDesc
                End If

            End If

            Dim ctlImg As System.Web.UI.WebControls.Image = e.Item.FindControl("imgItemImage")
            If (objAuditParameterPhoto.AuditScheduleDealerReports.Count > 0) Then
                Dim objAuditReport As AuditScheduleDealerReport = CType(objAuditParameterPhoto.AuditScheduleDealerReports(0), AuditScheduleDealerReport)
                If Not objAuditReport.ItemImage Is Nothing Then
                    If (Not ctlImg Is Nothing) Then
                        ctlImg.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & e.Item.ItemIndex.ToString & "&type=" & "AuditScheduleDealer"
                    End If
                Else
                    If (Not ctlImg Is Nothing) Then
                        ctlImg.Visible = False
                    End If
                End If
            Else
                If (Not ctlImg Is Nothing) Then
                    ctlImg.Visible = False
                End If
            End If


        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim txtItemDesc As TextBox = e.Item.FindControl("txtEditItemDesc")
            Dim objAuditScheduleDealer As AuditScheduleDealer = SesAuditScheduleDealer
            Dim objAuditParameterPhoto As AuditParameterPhoto = CType(objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos(e.Item.ItemIndex), AuditParameterPhoto)
            If Not txtItemDesc Is Nothing Then
                If (objAuditParameterPhoto.AuditScheduleDealerReports.Count > 0) Then
                    txtItemDesc.Text = CType(objAuditParameterPhoto.AuditScheduleDealerReports(0), AuditScheduleDealerReport).ItemDesc
                End If

            End If

            Dim ctlImg As System.Web.UI.WebControls.Image = e.Item.FindControl("imgEditItemImage")

            If (objAuditParameterPhoto.AuditScheduleDealerReports.Count > 0) Then
                Dim objAuditReport As AuditScheduleDealerReport = CType(objAuditParameterPhoto.AuditScheduleDealerReports(0), AuditScheduleDealerReport)
                If Not objAuditReport.ItemImage Is Nothing Then
                    If (Not ctlImg Is Nothing) Then
                        ctlImg.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & e.Item.ItemIndex.ToString & "&type=" & "AuditScheduleDealer"
                    End If
                Else
                    If (Not ctlImg Is Nothing) Then
                        ctlImg.Visible = False
                    End If
                End If
            Else
                If (Not ctlImg Is Nothing) Then
                    ctlImg.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub dtgPhotoList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhotoList.ItemCommand
        If e.CommandName = "Add" Then
            AddDataToGrid(e)
        ElseIf e.CommandName = "Delete" Then
            'If viewstate("IDEdit") <> "" Then
            '    Dim objAuditParam As AuditParameter = sHelper.GetSession("objAuditParameter")
            '    Dim objFotoParam As AuditParameterPhoto = objAuditParam.AuditParameterPhotos(e.Item.ItemIndex)
            '    Dim facade As AuditParameterPhotoFacade = New AuditParameterPhotoFacade(User)
            '    facade.DeleteFromDB(objFotoParam)
            '    BindToControl(CInt(viewstate("IDEdit")))
            'Else
            '    Dim arlFotoTemp As ArrayList = sHelper.GetSession("arlListPhoto")
            '    arlFotoTemp.RemoveAt(e.Item.ItemIndex)
            '    sHelper.SetSession("arlListPhoto", arlFotoTemp)
            '    BindToGridTemp()
            'End If

        End If
    End Sub

    Private Sub dtgPhotoList_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhotoList.EditCommand
        dtgPhotoList.EditItemIndex = e.Item.ItemIndex
        dtgPhotoList.ShowFooter = False

        btnSimpan.Enabled = False

        BindToGridTemp()
    End Sub

    Private Sub dtgPhotoList_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhotoList.UpdateCommand
        dtgPhotoList.ShowFooter = True


        Dim arrDataUpd As ArrayList = SesPhotoList
        'Dim txtDesc As TextBox = CType(e.Item.FindControl("txtEditDesc"), TextBox)
        Dim txtItemDesc As TextBox = e.Item.FindControl("txtEditItemDesc")

        Dim objAuditfoto As AuditParameterPhoto = CType(arrDataUpd(e.Item.ItemIndex), AuditParameterPhoto)
        'objAuditfoto.Description = txtDesc.Text

        Dim objAuditScheduleReport As AuditScheduleDealerReport
        If (objAuditfoto.AuditScheduleDealerReports.Count > 0) Then
            objAuditScheduleReport = CType(objAuditfoto.AuditScheduleDealerReports(0), AuditScheduleDealerReport)
            objAuditScheduleReport.ItemDesc = txtItemDesc.Text
        Else
            objAuditScheduleReport = New AuditScheduleDealerReport
            objAuditScheduleReport.ItemDesc = txtItemDesc.Text
            objAuditfoto.AuditScheduleDealerReports.Add(objAuditScheduleReport)
        End If

        arrDataUpd(e.Item.ItemIndex) = objAuditfoto

        SesPhotoList = arrDataUpd
        dtgPhotoList.EditItemIndex = -1

        btnSimpan.Enabled = True

        BindToGridTemp()
    End Sub

    Private Sub dtgPhotoList_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhotoList.CancelCommand
        dtgPhotoList.EditItemIndex = -1
        dtgPhotoList.ShowFooter = True

        btnSimpan.Enabled = True

        BindToGridTemp()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        '--cekHeader
        Dim nresult As Integer = 0

        Dim strAuditorID As String = ddlAuditor.SelectedValue
        If strAuditorID = "-1" Then
            MessageBox.Show("Auditor harus dipilih")
            Return
        End If

        Dim auditorFacade As New AuditScheduleAuditorFacade(User)
        Dim auditorID As Integer = CInt(strAuditorID)
        Dim objAuditor As AuditScheduleAuditor = auditorFacade.Retrieve(auditorID)

        SesAuditScheduleDealer.AuditScheduleAuditor = objAuditor

        Dim facade As New AuditScheduleDealerFacade(User)

        facade.UpdateDaftarAudit(SesAuditScheduleDealer)

        If nresult <> -1 Then
            MessageBox.Show(SR.SaveSuccess)
            Response.Redirect("FrmListAuditSchedule.aspx")
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../ShowroomAudit/FrmListAuditSchedule.aspx?isBack=1", True)
    End Sub
End Class
