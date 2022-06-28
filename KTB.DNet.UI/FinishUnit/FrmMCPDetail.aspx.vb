Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
'Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls

Public Class FrmMCPDetail
    Inherits System.Web.UI.Page


#Region "PrivateVariables"
    Private _MCPHeaderFacade As New MCPHeaderFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private sessHelper As New SessionHelper
    'Private ObjMCPHeader As New MCPHeader
    'Private ObjMCPDetail As New MCPDetail
    'Private objMCPDetaiLList As ArrayList
    Private Mode As enumMode
    Private IndexList As Integer

#End Region

#Region "Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
        End If
        txtDealerName.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim _MCPDetails As New ArrayList
        Dim _MCPDealers As New ArrayList
        Dim i As Integer

        If txtDealerName.Text.Trim = "" Then
            MessageBox.Show("Dealer tidak boleh kosong")
            Exit Sub
        End If

        If ValidateDealers(txtDealerName.Text.Trim) Then
            If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
                If IsExistMCPNumber(txtMCPNumber.Text.Trim()) Then
                    MessageBox.Show(SR.DataIsExist("MCPNumber"))
                Else
                    If Not sessHelper.GetSession("MCPDetaiLList") Is Nothing Then
                        _MCPDetails = CType(sessHelper.GetSession("MCPDetaiLList"), ArrayList)
                    End If

                    Dim ObjMCPHeader As New MCPHeader
                    'ObjMCPHeader.Status = EnumStatusMCP.StatusMCP.Aktif
                    ObjMCPHeader.Status = CInt(ddlStatus.SelectedValue)

                    GetValueToDatabase(ObjMCPHeader)

                    Dim dealers() As String = txtDealerName.Text.Split(";")
                    For Each dealerCode As String In dealers
                        Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(dealerCode)
                        Dim ObjMCPDealer As MCPDealer = New MCPDealer
                        ObjMCPDealer.Dealer = objDealerTmp
                        _MCPDealers.Add(ObjMCPDealer)
                    Next

                    If UploadFile(ObjMCPHeader) = 1 Then
                        Dim n As Integer = _MCPHeaderFacade.InsertMCPHeader(ObjMCPHeader, _MCPDetails, _MCPDealers)
                        If n = -1 Then
                            MessageBox.Show(SR.SaveFail)
                        Else
                            RemoveALLSession()
                            sessHelper.SetSession("Status", "Update")
                            sessHelper.SetSession("IDMCPHeader", ObjMCPHeader.ID)
                            Response.Redirect("FrmMCPDetail.aspx")
                            MessageBox.Show("Simpan Berhasil")
                        End If
                    Else
                        MessageBox.Show("File gagal disimpan di Server. Harap hubungi Administrator")
                    End If
                End If
            ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
                Dim idMCPHeader As Integer = CInt(sessHelper.GetSession("IDMCPHeader"))
                Dim ObjMCPHeader As MCPHeader = _MCPHeaderFacade.Retrieve(idMCPHeader)
                If Not IsNothing(ObjMCPHeader) Then
                    'updated 20150722 by anh
                    If txtMCPNumber.Text.Trim() <> ObjMCPHeader.ReferenceNumber Then
                        If IsExistMCPNumber(txtMCPNumber.Text.Trim()) Then
                            MessageBox.Show(SR.DataIsExist("MCPNumber"))
                            Return
                        Else
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(EndCustomer), "MCPHeader.ID", MatchType.Exact, ObjMCPHeader.ID))
                            criterias.opAnd(New Criteria(GetType(EndCustomer), "Customer.ID", MatchType.Greater, 0))
                            Dim arlEndCustomer As ArrayList = New EndCustomerFacade(User).Retrieve(criterias)
                            Dim isAllow As Boolean = True
                            For Each objEC As EndCustomer In arlEndCustomer
                                If objEC.ChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Selesai Then
                                    isAllow = False
                                End If
                                If isAllow = False Then Exit For
                            Next
                            If isAllow = False Then
                                MessageBox.Show("Status Faktur sudah selesai, No. MCP tidak bisa dirubah")
                                Return
                            End If
                        End If
                    End If

                    'end update

                    If ddlStatus.SelectedValue = EnumStatusMCP.StatusMCP.Tidak_Aktif Then
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(EndCustomer), "MCPHeader.ID", MatchType.Exact, ObjMCPHeader.ID))
                        criterias.opAnd(New Criteria(GetType(EndCustomer), "Customer.ID", MatchType.Greater, 0))
                        Dim arlEndCustomer As ArrayList = New EndCustomerFacade(User).Retrieve(criterias)
                        If arlEndCustomer.Count > 0 Then
                            MessageBox.Show("Aplikasi sudah di gunakan di Faktur")
                            Return
                        Else
                            ObjMCPHeader.Status = EnumStatusMCP.StatusMCP.Tidak_Aktif
                        End If
                    Else
                        ObjMCPHeader.Status = EnumStatusMCP.StatusMCP.Aktif
                    End If

                    Dim _OldMCPDetail As ArrayList = CType(sessHelper.GetSession("OldMCPDetaiLList"), ArrayList)
                    Dim _OldMCPDealer As ArrayList = CType(sessHelper.GetSession("OldMCPDealer"), ArrayList)

                    If Not _OldMCPDetail Is Nothing Then
                        Dim _delDetail As Integer = _MCPHeaderFacade.DeleteOLDMCPDetail(_OldMCPDetail)
                    End If

                    If Not _OldMCPDealer Is Nothing Then
                        Dim _delDealer As Integer = _MCPHeaderFacade.DeleteOLDDealer(_OldMCPDealer)
                    End If

                    If Not sessHelper.GetSession("MCPDetaiLList") Is Nothing Then
                        _MCPDetails = CType(sessHelper.GetSession("MCPDetaiLList"), ArrayList)
                        If _MCPDetails.Count > 0 Then
                            For Each mcpDet As MCPDetail In _MCPDetails
                                mcpDet.MCPHeader = ObjMCPHeader
                                ObjMCPHeader.MCPDetails.Add(mcpDet)
                            Next
                        End If
                    End If

                    Dim dealers() As String = txtDealerName.Text.Split(";")
                    For Each dealerCode As String In dealers
                        Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(dealerCode)
                        If Not IsNothing(objDealerTmp) Then
                            Dim ObjMCPDealer As MCPDealer = New MCPDealer
                            ObjMCPDealer.Dealer = objDealerTmp
                            ObjMCPDealer.MCPHeader = ObjMCPHeader
                            _MCPDealers.Add(ObjMCPDealer)
                        End If
                    Next

                    If _MCPDealers.Count > 0 Then
                        For Each mcpDealer As MCPDealer In _MCPDealers
                            ObjMCPHeader.MCPDealers.Add(mcpDealer)
                        Next
                    End If


                    GetValueToDatabase(ObjMCPHeader)

                    If UploadFile(ObjMCPHeader) = 1 Then
                        'Dim n As Integer = _MCPHeaderFacade.UpdateMCPHeader(ObjMCPHeader, _MCPDetails, _MCPDealers)
                        Dim n As Integer = _MCPHeaderFacade.UpdateMCPHeader(ObjMCPHeader)
                        If n = -1 Then
                            MessageBox.Show(SR.UpdateFail)
                        Else
                            RemoveALLSession()
                            sessHelper.SetSession("Status", "Update")
                            sessHelper.SetSession("IDMCPHeader", ObjMCPHeader.ID)
                            btnSave.Enabled = False
                            MessageBox.Show("Simpan Berhasil")
                            'Response.Redirect("FrmMCPDetail.aspx")
                        End If
                    Else
                        MessageBox.Show("File gagal disimpan di Server. Harap hubungi Administrator")
                    End If
                End If
                End If
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveALLSession()
        Response.Redirect("FrmMCPHeader.aspx")
    End Sub

    Private Sub dgMCPDetail_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgMCPDetail.ItemCommand
        Select Case (e.CommandName)
            Case "Delete"
                Dim lShouldReturn As Boolean
                DeleteCommand(e, lShouldReturn)
                If lShouldReturn Then
                    Return
                End If
            Case "Add"
                AddCommand(e)
            Case "Edit"
                dgMCPDetail_EditCommand(e)
            Case "Update"
                dgMCPDetail_Update(e)
            Case "Cancel"
                dgMCPDetail_CancelCommand(e)
        End Select
    End Sub

    Private Sub dgMCPDetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgMCPDetail.ItemDataBound
        Dim list As ArrayList = CType(sessHelper.GetSession("MCPHeaderVIEW"), ArrayList)

        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                SetDgMCPDetailItemView(e, list)
                Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                lbtnDelete.Visible = False
                Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
                lbtnEdit.Visible = False
            End If
        Else
            If e.Item.ItemType = ListItemType.Footer Then
                SetDgMCPDetailItemFooter(e)
            ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                SetDgMCPDetailItemEdit(e, list)
            ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                SetDgMCPDetailItemView(e, list)
            End If
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Anda yakin data ini ingin dihapus?');")
            End If
        End If
    End Sub
#End Region


#Region "Custom"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.MCP_List_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Detail MCP")
        End If
        _create = SecurityProvider.Authorize(Context.User, SR.MCP_Input_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.MCP_Edit_Privilege)

        btnSave.Visible = _create And _edit
    End Sub

    Private Sub Initialize()
        BindDdlStatus()
        BindDdlClassification()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnBack.Visible = True

        If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            Dim idMCPHeader As Integer = CInt(sessHelper.GetSession("IDMCPHeader"))
            GetValueFromDataBase(idMCPHeader)
            BindDetail(idMCPHeader)
            GetEnableControl(True)
            'If ddlStatus.SelectedValue = 1 Then
            '    dgMCPDetail.ShowFooter = False
            'Else
            '    dgMCPDetail.ShowFooter = True
            'End If
        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            Dim idMCPHeader As Integer = CInt(sessHelper.GetSession("IDMCPHeader"))
            GetValueFromDataBase(idMCPHeader)
            BindDetail(idMCPHeader)
            GetEnableControl(False)
            dgMCPDetail.ShowFooter = False
            inFileLocation.Disabled = True
        Else
            txtMCPNumber.Text = ""
            txtDealerName.Text = ""
            txtCustName.Text = ""
            txtDescription.Text = ""
            lblAttachment.Text = ""
            GetEnableControl(True)
            tr1.Visible = False
            tr3.Visible = False
            ddlStatus.SelectedValue = 1
            ddlStatus.Enabled = False
            dgMCPDetail.ShowFooter = False
            btnBack.Visible = False
            sessHelper.SetSession("Status", "Insert")
        End If
    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusMCP.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub

    Private Sub BindDdlClassification()
        ddlClassification.Items.Clear()
        ddlClassification.DataSource = EnumMCPClassification.RetrieveClassification
        ddlClassification.DataValueField = "ValStatus"
        ddlClassification.DataTextField = "NameStatus"
        ddlClassification.DataBind()
    End Sub

    Private Sub GetValueFromDataBase(ByVal idMCPHeader As Integer)
        Dim ObjMCPHeader As MCPHeader = _MCPHeaderFacade.Retrieve(idMCPHeader)
        txtMCPNumber.Text = ObjMCPHeader.ReferenceNumber
        Dim _MCPDealer As ArrayList = _MCPHeaderFacade.RetrieveDealerID(idMCPHeader)
        sessHelper.SetSession("OldMCPDealer", _MCPDealer)

        'Dim MCPDetailList As ArrayList = _MCPHeaderFacade.RetrieveMCPDetail(idMCPHeader)
        'sessHelper.SetSession("OldMCPDetailList", MCPDetailList)
        'sessHelper.SetSession("MCPDetaiLList", MCPDetailList)

        Dim _tempDealer As String = ""
        If Not _MCPDealer Is Nothing Then
            For Each item As MCPDealer In _MCPDealer
                If Not IsNothing(item.Dealer) Then
                    Dim objDealer As Dealer = New DealerFacade(User).Retrieve(item.Dealer.ID)
                    _tempDealer += objDealer.DealerCode + ";"
                End If
            Next
            If _tempDealer <> "" Then
                txtDealerName.Text = _tempDealer.Substring(0, Len(_tempDealer) - 1)
            End If
        End If
        txtCustName.Text = ObjMCPHeader.GovInstName
        txtDescription.Text = ObjMCPHeader.Description
        ddlClassification.SelectedValue = ObjMCPHeader.Classification
        ddlStatus.SelectedValue = ObjMCPHeader.Status
        icLetterDate.Value = ObjMCPHeader.LetterDate
        lblAttachment.Text = ObjMCPHeader.Attachment
        lblDibuatOleh.Text = UserInfo.Convert(ObjMCPHeader.CreatedBy)
        lblDibuatPada.Text = ObjMCPHeader.CreatedTime
        lblDiubahOleh.Text = UserInfo.Convert(ObjMCPHeader.LastUpdateBy)
        lblDiubahPada.Text = ObjMCPHeader.LastUpdateTime
    End Sub

    Private Sub GetEnableControl(ByVal isEnabled As Boolean)
        'If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
        '    txtMCPNumber.ReadOnly = True
        'Else
        '    txtMCPNumber.ReadOnly = Not isEnabled
        'End If
        'txtDealerName.ReadOnly = Not isEnabled
        txtCustName.ReadOnly = Not isEnabled
        txtDescription.ReadOnly = Not isEnabled
        ddlClassification.Enabled = isEnabled
        ddlStatus.Enabled = isEnabled
        btnSave.Enabled = isEnabled
        lblSearchDealer.Visible = isEnabled
    End Sub

    Private Sub BindDetail(ByVal IDMCPHeader As Integer)
        Dim objMCPHeader As MCPHeader = New MCPHeaderFacade(User).Retrieve(IDMCPHeader)
        Dim list As ArrayList = CType(sessHelper.GetSession("MCPDetaiLList"), ArrayList)
        If list Is Nothing Then
            If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Or Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
                list = _MCPHeaderFacade.RetrieveMCPDetail(IDMCPHeader)
                sessHelper.SetSession("OldMCPDetailList", _MCPHeaderFacade.RetrieveMCPDetail(IDMCPHeader))
                If list Is Nothing Then
                    list = New ArrayList
                End If
            ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
                list = New ArrayList
            End If
        End If

        'Dim _objMCPHeader As New MCPHeader
        'For Each item As MCPDetail In list
        '    objMCPHeader.MCPDetails.Add(item)
        'Next

        sessHelper.SetSession("MCPDetaiLList", list)
        sessHelper.SetSession("MCPHeaderVIEW", list)
        dgMCPDetail.DataSource = list
        dgMCPDetail.DataBind()

        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            dgMCPDetail.Columns(3).Visible = True
        Else
            dgMCPDetail.Columns(3).Visible = False
        End If
    End Sub

    Private Sub RemoveALLSession()
        sessHelper.RemoveSession("OldMCPDealer")
        sessHelper.RemoveSession("OldMCPDetaiLList")
        sessHelper.RemoveSession("MCPDetaiLList")
        sessHelper.RemoveSession("MCPHeaderVIEW")
        sessHelper.RemoveSession("IDMCPHeader")
    End Sub

    Private Function ValidateDealers(ByVal _dealers As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = _dealers.Split(";")
        For i = 0 To items.Length - 2
            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(items(i))
            If objDealerTmp.ID = 0 Then
                MessageBox.Show("Dealer " + items(i) + "tidak valid")
                bcheck = False
                Exit For
            End If

        Next
        If ValidateDealerDuplication(_dealers) <> String.Empty Then
            MessageBox.Show("Duplikasi Dealer " + ValidateDealerDuplication(_dealers))
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function ValidateDealerDuplication(ByVal _dealers As String) As String
        Dim bcheck As Boolean = True
        Dim _dealerDuplicate As String = String.Empty
        Dim i As Integer
        Dim j As Integer
        Dim list() As String = _dealers.Split(";")
        For i = 0 To list.Length - 2
            For j = i + 1 To list.Length - 1
                If list(i) = list(j) Then
                    bcheck = False
                    Exit For
                End If
            Next
            If bcheck = False Then
                _dealerDuplicate = list(i)
                Exit For
            End If
        Next
        Return _dealerDuplicate
    End Function

    Private Function IsExistMCPNumber(ByVal MCPNumber As String) As Boolean
        Dim isExist As Boolean = True
        Dim criterias As New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.Exact, MCPNumber))

        Dim arrList As ArrayList = _MCPHeaderFacade.Retrieve(criterias)
        If arrList.Count > 0 Then
            isExist = True
        Else
            isExist = False
        End If

        Return isExist
    End Function

    Private Sub GetValueToDatabase(ByRef ObjMCPHeader As MCPHeader)
        ObjMCPHeader.ReferenceNumber = txtMCPNumber.Text
        ObjMCPHeader.LetterDate = New Date(icLetterDate.Value.Year, icLetterDate.Value.Month, icLetterDate.Value.Day)
        ObjMCPHeader.GovInstName = txtCustName.Text
        ObjMCPHeader.Description = txtDescription.Text
        ObjMCPHeader.Classification = ddlClassification.SelectedValue

    End Sub

    Private Function UploadFile(ByRef ObjMCPHeader As MCPHeader) As Integer
        Dim retValue As Integer = 0
        If inFileLocation.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If inFileLocation.PostedFile.ContentLength <> inFileLocation.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment")
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)
                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(inFileLocation.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0
                    Throw New Exception("Salah Extention")
                End If

                Dim filename As String = txtMCPNumber.Text.Replace("/", "_") & ext
                Dim targetFile As String = New System.Text.StringBuilder(directory). _
                    Append("\").Append(filename).ToString

                Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If fInfo.Exists Then
                    fInfo.Delete()
                End If

                inFileLocation.PostedFile.SaveAs(targetFile)
                Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If Not trgInfo.Exists Then
                    retValue = 0
                End If
                Dim strFileSave As String = KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment") & "\" & filename
                ObjMCPHeader.Attachment = strFileSave
                retValue = 1
            Catch ex As Exception
                retValue = 0
            Finally
                sapImp.StopImpersonate()
            End Try
            Return retValue
        Else
            retValue = 1
            Return retValue
        End If
    End Function

    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "PDF" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Sub dgMCPDetail_EditCommand(ByVal e As DataGridCommandEventArgs)
        dgMCPDetail.ShowFooter = False
        dgMCPDetail.EditItemIndex = CInt(e.Item.ItemIndex)
        BindDetail(CInt(sessHelper.GetSession("IDMCPHeader")))
    End Sub

    Private Sub dgMCPDetail_CancelCommand(ByVal E As DataGridCommandEventArgs)
        dgMCPDetail.EditItemIndex = -1
        BindDetail(CInt(sessHelper.GetSession("IDMCPHeader")))
        dgMCPDetail.ShowFooter = True
    End Sub

    Private Sub dgMCPDetail_Update(ByVal e As DataGridCommandEventArgs)
        UpdateCommand(e)
    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If

        Dim txtKodeModel As TextBox = e.Item.FindControl("txtFooterKodeModel")
        Dim txtUnit As TextBox = e.Item.FindControl("txtFooterUnit")
        If txtKodeModel.Text.Trim <> "" And txtUnit.Text.Trim <> "" Then
            'If ValidateData(txtTop.Text.Trim, txtKodeModel.Text.Trim, txtPriceRefDate.Text.Trim, txtUnit.Text) Then

            Dim ObjMCPDetail As New MCPDetail

            Dim objVehicleFacade As VechileTypeFacade = New VechileTypeFacade(User)
            Dim objVecType As VechileType = objVehicleFacade.Retrieve(txtKodeModel.Text.Trim)
            If Not IsNothing(objVecType) Then
                ObjMCPDetail.VechileType = objVecType
                ObjMCPDetail.Unit = Integer.Parse(txtUnit.Text.Trim.Replace(".", ""))
                ObjMCPDetail.UnitRemain = ObjMCPDetail.Unit
            End If
            
            Dim list As New ArrayList
            list = CType(sessHelper.GetSession("MCPDetaiLList"), ArrayList)
            If Not list Is Nothing Then
                list.Add(ObjMCPDetail)
            End If

            sessHelper.SetSession("MCPDetaiLList", list)

            BindDetail(CInt(sessHelper.GetSession("IDMCPHeader")))
            'End If
        Else
            MessageBox.Show("Isi detail dengan lengkap")
        End If

    End Sub

    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            dgMCPDetail.EditItemIndex = -1
            BindDetail(CInt(sessHelper.GetSession("IDMCPHeader")))
        Else
            Dim txtKodeModel As TextBox = e.Item.FindControl("txtEditKodeModel")
            Dim txtUnit As TextBox = e.Item.FindControl("txtEditUnit")
            If txtKodeModel.Text.Trim <> "" And txtUnit.Text.Trim <> "" Then
                'If ValidateData(txtTop.Text.Trim, txtKodeModel.Text.Trim, txtPriceRefDate.Text.Trim, txtUnit.Text) Then

                Dim listAll As ArrayList = CType(sessHelper.GetSession("MCPDetaiLList"), ArrayList)
                Dim list As ArrayList = CType(sessHelper.GetSession("MCPHeaderVIEW"), ArrayList)

                Dim ObjMCPDetail As New MCPDetail
                ObjMCPDetail = CType(list(e.Item.ItemIndex), MCPDetail)
                IndexList = GetIndexMCPDetaiLList(listAll, ObjMCPDetail)

                Dim iMcpCreated As Integer = 0
                iMcpCreated = New EndCustomerFacade(User).GetCountMCP(ObjMCPDetail.MCPHeader.ID)
                If iMcpCreated > 0 Then
                    If Integer.Parse(txtUnit.Text.Trim.Replace(".", "")) < iMcpCreated Then
                        MessageBox.Show("Nominal unit tidak boleh lebih kecil dari jumlah faktur yang telah dibuat.")
                        Exit Sub
                    End If
                End If
                
                Dim objVehicleFacade As VechileTypeFacade = New VechileTypeFacade(User)
                Dim objVecType As VechileType = objVehicleFacade.Retrieve(txtKodeModel.Text.Trim)
                ObjMCPDetail.VechileType = objVecType
                ObjMCPDetail.Unit = Integer.Parse(txtUnit.Text.Trim.Replace(".", ""))

                If ObjMCPDetail.Unit < ObjMCPDetail.UnitRemain Then
                    ObjMCPDetail.UnitRemain = ObjMCPDetail.Unit
                Else
                    'Dim OldList As ArrayList = CType(sessHelper.GetSession("OldMCPDetaiLList"), ArrayList)
                    If ObjMCPDetail.ID > 0 Then
                        Dim objOldMCPDetail As MCPDetail = New MCPDetailFacade(User).Retrieve(ObjMCPDetail.ID)
                        If Not IsNothing(objOldMCPDetail) Then
                            If ObjMCPDetail.Unit > objOldMCPDetail.Unit Then
                                Dim iAdd As Integer = ObjMCPDetail.Unit - objOldMCPDetail.Unit
                                ObjMCPDetail.UnitRemain = ObjMCPDetail.UnitRemain + iAdd
                            End If
                        End If
                    End If
                End If

                dgMCPDetail.EditItemIndex = -1
                BindDetail(CInt(sessHelper.GetSession("IDMCPHeader")))
                dgMCPDetail.ShowFooter = True
                'End If
            Else
                MessageBox.Show("Isi detail dengan lengkap")
            End If
        End If

    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
        shouldReturn = False
        Dim lblNamaType As Label = CType(e.Item.FindControl("lblNamaType"), Label)

        'Delete item yang index nya itu sesuai dengan index item yg di filter
        Dim list As ArrayList = CType(sessHelper.GetSession("MCPDetaiLList"), ArrayList)
        Dim _MCPDetailDelete As New MCPDetail
        For Each item As MCPDetail In list
            If item.VechileType.VechileTypeCode & "-" & item.VechileType.Description = CType(e.Item.FindControl("lblNamaType"), Label).Text Then
                _MCPDetailDelete = item
            End If
        Next
        list.Remove(_MCPDetailDelete)
        sessHelper.SetSession("MCPDetaiLList", list)
        BindDetail(CInt(sessHelper.GetSession("IDMCPHeader")))
    End Sub

    Private Function GetIndexMCPDetaiLList(ByVal _list As ArrayList, ByVal _obj As MCPDetail) As Integer
        Dim i As Integer = 0
        For Each item As MCPDetail In _list
            If item.VechileType.VechileTypeCode = _obj.VechileType.VechileTypeCode Then
                Exit For
            End If
            i = i + 1
        Next
        Return i
    End Function

    Private Sub SetDgMCPDetailItemView(ByVal e As DataGridItemEventArgs, ByVal list As ArrayList)
        If Not list Is Nothing Then
            Dim _objMCPTmp As New MCPHeader
            Dim _objMCPDetail As New MCPDetail
            _objMCPTmp = New MCPHeaderFacade(User).Retrieve(CInt(sessHelper.GetSession("IDMCPHeader")))

            Dim listAll As ArrayList = CType(sessHelper.GetSession("MCPDetaiLList"), ArrayList)
            _objMCPDetail = CType(list(e.Item.ItemIndex), MCPDetail)
            IndexList = GetIndexMCPDetaiLList(listAll, _objMCPDetail)

            _objMCPDetail.MCPHeader = _objMCPTmp
            Dim _lblNamaTipe As Label = CType(e.Item.FindControl("lblNamaType"), Label)
            _lblNamaTipe.Text = _objMCPDetail.VechileType.VechileTypeCode & "-" & _objMCPDetail.VechileType.Description
            '_lblNamaTipe.Text = New VechileTypeFacade(User).Retrieve(_objMCPDetail.VechileType.ID).Description


            Dim _lblViewUnit As Label = CType(e.Item.FindControl("lblViewUnit"), Label)
            _lblViewUnit.Text = _objMCPDetail.Unit.ToString

            Dim _lblViewUnitRemain As Label = CType(e.Item.FindControl("lblViewUnitRemain"), Label)
            _lblViewUnitRemain.Text = _objMCPDetail.UnitRemain.ToString

            Dim lblViewFaktur As Label = CType(e.Item.FindControl("lblViewFaktur"), Label)
            lblViewFaktur.Attributes("onclick") = "showPopUp('../FinishUnit/FrmMCPFaktur.aspx?vehicleTypeCode=" & _objMCPDetail.VechileType.VechileTypeCode & "&mcpId=" & _objMCPDetail.MCPHeader.ID & "','',400,500,'');"

            Dim _lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            _lblID.Text = _objMCPDetail.ID.ToString

        End If
    End Sub

    Private Sub SetDgMCPDetailItemFooter(ByVal e As DataGridItemEventArgs)

        Dim lblFooterKodeModel As Label = CType(e.Item.FindControl("lblFooterKodeModel"), Label)
        lblFooterKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"

        Dim txtFooterKodeModel As TextBox = CType(e.Item.FindControl("txtFooterKodeModel"), TextBox)
        'txtFooterKodeModel.Attributes.Add("readonly", "readonly")

        Dim txtFooterUnit As TextBox = CType(e.Item.FindControl("txtFooterUnit"), TextBox)
        'txtFooterUnit.Attributes.Add("readonly", "readonly")

    End Sub

    Private Sub SetDgMCPDetailItemEdit(ByVal e As DataGridItemEventArgs, ByVal list As ArrayList)

        Dim _objMCPDetail As MCPDetail = list(e.Item.ItemIndex)

        Dim txtEditKodeModel As TextBox = CType(e.Item.FindControl("txtEditKodeModel"), TextBox)
        txtEditKodeModel.Attributes.Add("readonly", "readonly")

        Dim lblEditKodeModel As Label = CType(e.Item.FindControl("lblEditKodeModel"), Label)

        If _objMCPDetail.UnitRemain < _objMCPDetail.Unit Then
            lblEditKodeModel.Visible = False
        Else
            lblEditKodeModel.Visible = True
            lblEditKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"
        End If


        Dim txtEditUnit As TextBox = CType(e.Item.FindControl("txtEditUnit"), TextBox)
        txtEditUnit.Text = Convert.ToString(_objMCPDetail.Unit)

        Dim _lblIDEdit As Label = CType(e.Item.FindControl("lblIDEdit"), Label)
        _lblIDEdit.Text = _objMCPDetail.ID.ToString

    End Sub

#End Region

    
End Class