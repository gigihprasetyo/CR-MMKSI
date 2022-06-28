Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.LKPP
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls

Public Class FrmLKPPDetail
    Inherits System.Web.UI.Page

#Region "PrivateVariables"

    Private _LKPPHeaderFacade As New LKPPHeaderFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private sessHelper As New SessionHelper
    'Private ObjLKPPHeader As New LKPPHeader
    'Private ObjLKPPDetail As New LKPPDetail
    'Private objLKPPDetaiLList As ArrayList
    Private Mode As enumMode
    Private IndexList As Integer

#End Region

#Region "Custom"
    Private Sub CheckPrivilege()
        Dim objDealer As Dealer = Session.Item("DEALER")
        Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)

        Try
            Dim objAppConfig As AppConfig = New AppConfigFacade(User).Retrieve("LKPPDMSGoLive")
            If CBool(objAppConfig.Value) Then
                If dealerSystems.SystemID <> 1 Then
                    Server.Transfer("../FrmAccessDenied.aspx?mess=Silahkan Input Master LKPP di DMS atau di System Dealer Anda.")
                End If
            End If
        Catch
        End Try

        If Not IsNothing(Request.QueryString("mode")) AndAlso Request.QueryString("mode").ToString().ToLower() = "new" Then
            If Not SecurityProvider.Authorize(Context.User, SR.LKPP_Input_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Detail LKPP")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.LKPP_List_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Detail LKPP")
            End If
        End If

        _create = SecurityProvider.Authorize(Context.User, SR.LKPP_Input_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.LKPP_Edit_Privilege)
        btnSave.Visible = _create And _edit
    End Sub

    Private Sub Initialize()
        hdnValidasi.Value = "-1"
        BindDdlStatus()
        BindDdlClassification()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnBack.Visible = True
        icLetterDate.Value = Now
        icLetterDate.Enabled = False
        If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            Dim idLKPPHeader As Integer = CInt(sessHelper.GetSession("IDLKPPHeader"))
            ViewState("IDLKPPHeader") = idLKPPHeader
            GetValueFromDataBase(idLKPPHeader)
            BindDetail(idLKPPHeader)
            GetEnableControl(True)
            getVisibleControl(True)
            'If ddlStatus.SelectedValue = 1 Then
            '    dgLKPPDetail.ShowFooter = False
            'Else
            '    dgLKPPDetail.ShowFooter = True
            'End If
        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            Dim idLKPPHeader As Integer = CInt(sessHelper.GetSession("IDLKPPHeader"))
            ViewState("IDLKPPHeader") = idLKPPHeader
            GetValueFromDataBase(idLKPPHeader)
            BindDetail(idLKPPHeader)
            GetEnableControl(False)
            getVisibleControl(False)
            dgLKPPDetail.ShowFooter = False
            inFileLocation.Disabled = True
            If ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Baru Then
                txtLKPPNumber.ReadOnly = False
                btnValidasi.Enabled = True
            End If
        Else
            txtLKPPNumber.Text = ""
            txtdealername.Text = ""
            txtCustName.Text = ""
            txtDescription.Text = ""
            lblAttachment.Text = ""
            GetEnableControl(True)
            tr1.Visible = False
            tr3.Visible = False
            ddlStatus.SelectedValue = 0
            ddlStatus.Enabled = False
            dgLKPPDetail.ShowFooter = False
            btnBack.Visible = False
            btnValidasi.Visible = False
            dgLKPPDetail.DataSource = New ArrayList
            dgLKPPDetail.DataBind()
            dgLKPPDetail.Columns(3).Visible = False
            dgLKPPDetail.ShowFooter = True
            ' btnNew.Visible = False
            sessHelper.SetSession("Status", "Insert")
        End If
    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusLKPP.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub

    Private Sub BindDdlClassification()
        ddlClassification.Items.Clear()
        ddlClassification.DataSource = EnumLKPPClassification.RetrieveClassification(True)
        ddlClassification.DataValueField = "ValStatus"
        ddlClassification.DataTextField = "NameStatus"
        ddlClassification.DataBind()
    End Sub

    Private Sub GetValueFromDataBase(ByVal idLKPPHeader As Integer)
        Dim ObjLKPPHeader As LKPPHeader = _LKPPHeaderFacade.Retrieve(idLKPPHeader)
        txtLKPPNumber.Text = ObjLKPPHeader.ReferenceNumber
        txtNote0.Text = ObjLKPPHeader.Notes
        Dim _LKPPDealer As ArrayList = _LKPPHeaderFacade.RetrieveDealerID(idLKPPHeader)
        sessHelper.SetSession("OldLKPPDealer", _LKPPDealer)

        'Dim LKPPDetailList As ArrayList = _LKPPHeaderFacade.RetrieveLKPPDetail(idLKPPHeader)
        'sessHelper.SetSession("OldLKPPDetailList", LKPPDetailList)
        'sessHelper.SetSession("LKPPDetaiLList", LKPPDetailList)

        Dim _tempDealer As String = ""
        If Not _LKPPDealer Is Nothing Then
            For Each item As LKPPDealer In _LKPPDealer
                If Not IsNothing(item.Dealer) Then
                    Dim objDealer As Dealer = New DealerFacade(User).Retrieve(item.Dealer.ID)
                    _tempDealer += objDealer.DealerCode + ";"
                End If
            Next
            If _tempDealer <> "" Then
                txtdealername.Text = _tempDealer.Substring(0, Len(_tempDealer) - 1)
            End If
        End If
        txtCustName.Text = ObjLKPPHeader.GovInstName
        txtDescription.Text = ObjLKPPHeader.Description
        ddlClassification.SelectedValue = ObjLKPPHeader.Classification
        ddlStatus.SelectedValue = ObjLKPPHeader.Status
        icLetterDate.Value = ObjLKPPHeader.LetterDate
        lblAttachment.Text = ObjLKPPHeader.Attachment
        hdnAttachment.Value = ObjLKPPHeader.Attachment
        lblDibuatOleh.Text = UserInfo.Convert(ObjLKPPHeader.CreatedBy)
        lblDibuatPada.Text = ObjLKPPHeader.CreatedTime.ToString()
        lblDiubahOleh.Text = UserInfo.Convert(ObjLKPPHeader.LastUpdateBy)
        lblDiubahPada.Text = ObjLKPPHeader.LastUpdateTime.ToString()
    End Sub

    Private Sub GetEnableControl(ByVal isEnabled As Boolean)
        Dim status As String = Convert.ToString(sessHelper.GetSession("Status")).ToLower()
        'txtDealerName.ReadOnly = Not isEnabled
        txtLKPPNumber.ReadOnly = Not isEnabled
        txtCustName.ReadOnly = Not isEnabled
        txtDescription.ReadOnly = Not isEnabled
        ddlClassification.Enabled = isEnabled
        ' ddlStatus.Enabled = isEnabled
        btnSave.Enabled = isEnabled
        lblSearchDealer.Visible = isEnabled
        ddlStatus.Enabled = False
        If status = "view" Then
            If ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Validasi Then
                btnCancelValidatsi.Enabled = Not isEnabled
            ElseIf ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Baru Then
                btnValidasi.Enabled = Not isEnabled
            Else
                btnCancelValidatsi.Enabled = False
                btnValidasi.Enabled = False
            End If
        End If
    End Sub

    Private Sub getEnableApproval(ByVal isEnable As Boolean)
        If ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Validasi Then
            btnApprove.Enabled = isEnable
            btnReject.Enabled = isEnable
            btnCancelApprove.Enabled = Not isEnable
            btnCancelReject.Enabled = Not isEnable
        ElseIf ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Setuju Then
            btnApprove.Enabled = Not isEnable
            btnReject.Enabled = Not isEnable
            btnCancelApprove.Enabled = isEnable
            btnCancelReject.Enabled = Not isEnable
        ElseIf ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Tolak Then
            btnApprove.Enabled = Not isEnable
            btnReject.Enabled = Not isEnable
            btnCancelApprove.Enabled = Not isEnable
            btnCancelReject.Enabled = isEnable
        End If
    End Sub

    Private Sub getVisibleApproval(ByVal isEnable As Boolean)
        If ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Validasi Then
            btnApprove.Visible = isEnable
            btnReject.Visible = isEnable
            btnCancelApprove.Visible = Not isEnable
            btnCancelReject.Visible = Not isEnable
        ElseIf ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Setuju Then
            btnApprove.Visible = Not isEnable
            btnReject.Visible = Not isEnable
            btnCancelApprove.Visible = isEnable
            btnCancelReject.Visible = Not isEnable
        ElseIf ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Tolak Then
            btnApprove.Visible = Not isEnable
            btnReject.Visible = Not isEnable
            btnCancelApprove.Visible = Not isEnable
            btnCancelReject.Visible = isEnable
        End If
    End Sub

    Private Sub getVisibleControl(ByVal isEnable As Boolean)
        Dim sta As String = Convert.ToString(sessHelper.GetSession("Status")).ToLower()
        If ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Baru Then
            If sta = "view" Then
                btnValidasi.Visible = Not isEnable
                btnCancelValidatsi.Visible = isEnable
            Else
                btnValidasi.Visible = isEnable
                btnCancelValidatsi.Visible = Not isEnable
            End If
        ElseIf ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Validasi Then
            If sta = "view" Then
                btnValidasi.Visible = isEnable
                btnCancelValidatsi.Visible = Not isEnable
            Else
                btnValidasi.Visible = Not isEnable
                btnCancelValidatsi.Visible = isEnable
            End If
        Else
            btnCancelValidatsi.Visible = Not isEnable
        End If
    End Sub

    Private Sub BindDetail(ByVal IDLKPPHeader As Integer)
        Dim objLKPPHeader As LKPPHeader = New LKPPHeaderFacade(User).Retrieve(IDLKPPHeader)
        Dim list As ArrayList = CType(sessHelper.GetSession("LKPPDetaiLList"), ArrayList)
        If list Is Nothing Then
            If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Or Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
                list = _LKPPHeaderFacade.RetrieveLKPPDetail(IDLKPPHeader)
                sessHelper.SetSession("OldLKPPDetailList", _LKPPHeaderFacade.RetrieveLKPPDetail(IDLKPPHeader))
                If list Is Nothing Then
                    list = New ArrayList
                End If
            ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
                ' list = New ArrayList
            End If
        End If

        'Dim _objLKPPHeader As New LKPPHeader
        'For Each item As LKPPDetail In list
        '    objLKPPHeader.LKPPDetails.Add(item)
        'Next

        sessHelper.SetSession("LKPPDetaiLList", list)
        sessHelper.SetSession("LKPPHeaderVIEW", list)
        dgLKPPDetail.DataSource = list
        dgLKPPDetail.DataBind()

        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            dgLKPPDetail.Columns(3).Visible = True
        Else
            dgLKPPDetail.Columns(3).Visible = False
        End If
    End Sub

    Private Sub RemoveALLSession()
        sessHelper.RemoveSession("OldLKPPDealer")
        sessHelper.RemoveSession("OldLKPPDetaiLList")
        sessHelper.RemoveSession("LKPPDetaiLList")
        sessHelper.RemoveSession("LKPPHeaderVIEW")
        sessHelper.RemoveSession("IDLKPPHeader")
    End Sub

    Private Function ValidateDealers(ByVal _dealers As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        'Dim items() As String = _dealers.Split(";")

        'For i = 0 To items.Length - 2
        '    Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(items(i))
        '    If objDealerTmp.ID = 0 Then
        '        MessageBox.Show("Dealer " + items(i) + "tidak valid")
        '        bcheck = False
        '        Exit For
        '    End If
        'Next
        Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(_dealers)
        If objDealerTmp.ID = 0 Then
            MessageBox.Show("Dealer " + Items(i) + "tidak valid")
            bcheck = False
        End If
        'If ValidateDealerDuplication(_dealers) <> String.Empty Then
        '    MessageBox.Show("Duplikasi Dealer " + ValidateDealerDuplication(_dealers))
        '    bcheck = False
        'End If
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

    Private Function IsExistLKPPNumber(ByVal LKPPNumber As String) As Boolean
        Dim isExist As Boolean = True
        Dim criterias As New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.Exact, LKPPNumber))

        Dim arrList As ArrayList = _LKPPHeaderFacade.Retrieve(criterias)
        If arrList.Count > 0 Then
            isExist = True
        Else
            isExist = False
        End If

        Return isExist
    End Function

    Private Sub GetValueToDatabase(ByRef ObjLKPPHeader As LKPPHeader)
        ObjLKPPHeader.ReferenceNumber = txtLKPPNumber.Text
        ObjLKPPHeader.LetterDate = New Date(icLetterDate.Value.Year, icLetterDate.Value.Month, icLetterDate.Value.Day)
        ObjLKPPHeader.GovInstName = txtCustName.Text
        ObjLKPPHeader.Description = txtDescription.Text
        ObjLKPPHeader.Classification = ddlClassification.SelectedValue

    End Sub

    Private Function UploadFile(ByRef ObjLKPPHeader As LKPPHeader) As Integer
        Dim retValue As Integer = 0
        If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
            hdnAttachment.Value = inFileLocation.PostedFile.FileName.ToString()
        End If
        If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            If ddlClassification.SelectedValue <> EnumLKPPClassification.LKPPClassification.E_Catalog Then
                If hdnAttachment.Value <> lblAttachment.Text Or hdnAttachment.Value = "" Then
                    hdnAttachment.Value = inFileLocation.PostedFile.FileName.ToString()
                End If
            End If
        End If

        If ddlClassification.SelectedValue <> EnumLKPPClassification.LKPPClassification.E_Catalog Then
            If hdnAttachment.Value.Length < 1 Then
                retValue = 5
                Return retValue
            End If
        End If

        If Not IsNothing(inFileLocation.PostedFile) AndAlso inFileLocation.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If inFileLocation.PostedFile.ContentLength <> inFileLocation.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 2
                    Return retValue
                End If


                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment") & "LKPP"
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)
                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(inFileLocation.PostedFile.FileName)

                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 3
                    Return retValue
                End If

                ' MessageBox.Show(inFileLocation.PostedFile.ContentLength.ToString())

                If inFileLocation.PostedFile.ContentLength > 2048000 Then
                    retValue = 4
                    Return retValue
                End If


                Dim filename As String = txtLKPPNumber.Text.Replace("/", "_") & ext
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
                Dim strFileSave As String = KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment") & "\LKPP\" & filename
                ObjLKPPHeader.Attachment = strFileSave
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
        If ext.ToUpper() = "PDF" Or ext.ToUpper() = "ZIP" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Function iSLVC(ByVal criteria As Integer) As Boolean
        Dim retValue As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileType), "VechileType.VehicleTypeID", MatchType.Exact, criteria))
        Dim ListVechile As ArrayList = New VechileTypeFacade(User).Retrieve(criterias)

    End Function

    Private Sub dgLKPPDetail_EditCommand(ByVal e As DataGridCommandEventArgs)
        dgLKPPDetail.ShowFooter = False
        dgLKPPDetail.EditItemIndex = CInt(e.Item.ItemIndex)
        BindDetail(CInt(sessHelper.GetSession("IDLKPPHeader")))
    End Sub

    Private Sub dgLKPPDetail_CancelCommand(ByVal E As DataGridCommandEventArgs)
        dgLKPPDetail.EditItemIndex = -1
        BindDetail(CInt(sessHelper.GetSession("IDLKPPHeader")))
        dgLKPPDetail.ShowFooter = True
    End Sub

    Private Sub dgLKPPDetail_Update(ByVal e As DataGridCommandEventArgs)
        UpdateCommand(e)
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If

        Dim txtKodeModel As TextBox = e.Item.FindControl("txtFooterKodeModel")
        Dim txtUnit As TextBox = e.Item.FindControl("txtFooterUnit")
        If txtKodeModel.Text.Trim <> "" And txtUnit.Text.Trim <> "" Then
            'If ValidateData(txtTop.Text.Trim, txtKodeModel.Text.Trim, txtPriceRefDate.Text.Trim, txtUnit.Text) Then

            Dim ObjLKPPDetail As New LKPPDetail
            Dim objVehicleFacade As VechileTypeFacade = New VechileTypeFacade(User)
            Dim objVecType As VechileType = objVehicleFacade.Retrieve(txtKodeModel.Text.Trim)

            If IsNothing(objVecType) OrElse objVecType.ID = 0 Then
                MessageBox.Show("Model Tidak Terdaftar")
                txtKodeModel.Text = ""
                txtUnit.Text = ""
                Exit Sub
            End If
            'Validasi Kategori LVC
            If objVecType.ProductCategory.ID <> 1 Then
                MessageBox.Show("Katergori tidak sesuai dengan ketentuan LKPP")
                txtKodeModel.Text = ""
                txtUnit.Text = ""
                Exit Sub
            End If
            Dim list As New ArrayList
            list = CType(sessHelper.GetSession("LKPPDetaiLList"), ArrayList)
            Dim k As Integer = dgLKPPDetail.Items.Count
            Dim GridItem As DataGridItem
            Dim strValue As String = ""
            Dim strTemp As String = ""
            If k > 0 Then
                Dim _LKPPDetailCheck As New LKPPDetail
                For Each item As LKPPDetail In list
                    If item.VechileType.VechileTypeCode = txtKodeModel.Text Then
                        MessageBox.Show("Unit sudah ada di detail LKPP")
                        txtKodeModel.Text = ""
                        txtUnit.Text = ""
                        Exit Sub
                    End If
                Next
            End If
            If Not IsNothing(objVecType) Then
                ObjLKPPDetail.VechileType = objVecType
                ObjLKPPDetail.Unit = Integer.Parse(txtUnit.Text.Trim.Replace(".", ""))
                ObjLKPPDetail.UnitRemain = ObjLKPPDetail.Unit
            End If


            If Not list Is Nothing Then
                list.Add(ObjLKPPDetail)
            Else
                list = New ArrayList
                list.Add(ObjLKPPDetail)
            End If

            sessHelper.SetSession("LKPPDetaiLList", list)

            BindDetail(CInt(sessHelper.GetSession("IDLKPPHeader")))
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
            dgLKPPDetail.EditItemIndex = -1
            BindDetail(CInt(sessHelper.GetSession("IDLKPPHeader")))
        Else
            Dim txtKodeModel As TextBox = e.Item.FindControl("txtEditKodeModel")
            Dim txtUnit As TextBox = e.Item.FindControl("txtEditUnit")
            If txtKodeModel.Text.Trim <> "" And txtUnit.Text.Trim <> "" Then
                'If ValidateData(txtTop.Text.Trim, txtKodeModel.Text.Trim, txtPriceRefDate.Text.Trim, txtUnit.Text) Then

                Dim listAll As ArrayList = CType(sessHelper.GetSession("LKPPDetaiLList"), ArrayList)
                Dim list As ArrayList = CType(sessHelper.GetSession("LKPPHeaderVIEW"), ArrayList)

                Dim ObjLKPPDetail As New LKPPDetail
                ObjLKPPDetail = CType(list(e.Item.ItemIndex), LKPPDetail)
                IndexList = GetIndexLKPPDetaiLList(listAll, ObjLKPPDetail)

                'If cekFaktur(ObjLKPPDetail.VechileType.VechileTypeCode, ObjLKPPDetail.LKPPHeader.ID) = True Then
                '    MessageBox.Show("TEST")
                'Else
                '    MessageBox.Show("TEST-TEST")

                'End If


                'Dim k As Integer = dgLKPPDetail.Items.Count
                'Dim GridItem As DataGridItem
                'Dim strValue As String = ""
                'Dim strTemp As String = ""
                'If k > 0 Then
                '    Dim _LKPPDetailCheck As New LKPPDetail
                '    For Each item As LKPPDetail In listAll
                '        If item.VechileType.VechileTypeCode = txtKodeModel.Text Then
                '            MessageBox.Show("Unit sudah ada di detail LKPP")
                '            dgLKPPDetail_CancelCommand(e)
                '            Exit Sub
                '        End If
                '    Next
                'End If
                If Not IsNothing(ObjLKPPDetail) AndAlso Not IsNothing(ObjLKPPDetail.LKPPHeader) Then
                    Dim iLKPPCreated As Integer = 0
                    iLKPPCreated = New EndCustomerFacade(User).GetCountLKPP(ObjLKPPDetail.LKPPHeader.ID)
                    If iLKPPCreated > 0 Then
                        If Integer.Parse(txtUnit.Text.Trim.Replace(".", "")) < iLKPPCreated Then
                            MessageBox.Show("Nominal unit tidak boleh lebih kecil dari jumlah faktur yang telah dibuat.")
                            Exit Sub
                        End If
                    End If
                End If


                Dim objVehicleFacade As VechileTypeFacade = New VechileTypeFacade(User)
                Dim objVecType As VechileType = objVehicleFacade.Retrieve(txtKodeModel.Text.Trim)
                ObjLKPPDetail.VechileType = objVecType
                ObjLKPPDetail.Unit = Integer.Parse(txtUnit.Text.Trim.Replace(".", ""))

                If ObjLKPPDetail.Unit < ObjLKPPDetail.UnitRemain Then
                    ObjLKPPDetail.UnitRemain = ObjLKPPDetail.Unit
                Else
                    'Dim OldList As ArrayList = CType(sessHelper.GetSession("OldLKPPDetaiLList"), ArrayList)
                    If ObjLKPPDetail.ID > 0 Then
                        Dim objOldLKPPDetail As LKPPDetail = New LKPPDetailFacade(User).Retrieve(ObjLKPPDetail.ID)
                        If Not IsNothing(objOldLKPPDetail) Then
                            If ObjLKPPDetail.Unit > objOldLKPPDetail.Unit Then
                                Dim iAdd As Integer = ObjLKPPDetail.Unit - objOldLKPPDetail.Unit
                                ObjLKPPDetail.UnitRemain = ObjLKPPDetail.UnitRemain + iAdd
                            End If
                        End If
                    End If
                End If

                dgLKPPDetail.EditItemIndex = -1
                BindDetail(CInt(sessHelper.GetSession("IDLKPPHeader")))
                dgLKPPDetail.ShowFooter = True
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
        Dim list As ArrayList = CType(sessHelper.GetSession("LKPPDetaiLList"), ArrayList)
        Dim _LKPPDetailDelete As New LKPPDetail

        For Each item As LKPPDetail In list
            If item.VechileType.VechileTypeCode & "-" & item.VechileType.Description = CType(e.Item.FindControl("lblNamaType"), Label).Text Then
                _LKPPDetailDelete = item
            End If
        Next
        list.Remove(_LKPPDetailDelete)
        sessHelper.SetSession("LKPPDetaiLList", list)
        BindDetail(CInt(sessHelper.GetSession("IDLKPPHeader")))
    End Sub

    Private Function GetIndexLKPPDetaiLList(ByVal _list As ArrayList, ByVal _obj As LKPPDetail) As Integer
        Dim i As Integer = 0
        For Each item As LKPPDetail In _list
            If item.VechileType.VechileTypeCode = _obj.VechileType.VechileTypeCode Then
                Exit For
            End If
            i = i + 1
        Next
        Return i
    End Function

    Private Sub SetDgLKPPDetailItemView(ByVal e As DataGridItemEventArgs, ByVal list As ArrayList)
        If Not list Is Nothing Then
            Dim _objLKPPTmp As New LKPPHeader
            Dim _objLKPPDetail As New LKPPDetail
            _objLKPPTmp = New LKPPHeaderFacade(User).Retrieve(CInt(sessHelper.GetSession("IDLKPPHeader")))

            Dim listAll As ArrayList = CType(sessHelper.GetSession("LKPPDetaiLList"), ArrayList)
            _objLKPPDetail = CType(list(e.Item.ItemIndex), LKPPDetail)
            IndexList = GetIndexLKPPDetaiLList(listAll, _objLKPPDetail)

            _objLKPPDetail.LKPPHeader = _objLKPPTmp
            Dim _lblNamaTipe As Label = CType(e.Item.FindControl("lblNamaType"), Label)
            _lblNamaTipe.Text = _objLKPPDetail.VechileType.VechileTypeCode & "-" & _objLKPPDetail.VechileType.Description
            '_lblNamaTipe.Text = New VechileTypeFacade(User).Retrieve(_objLKPPDetail.VechileType.ID).Description


            Dim _lblViewUnit As Label = CType(e.Item.FindControl("lblViewUnit"), Label)
            _lblViewUnit.Text = _objLKPPDetail.Unit.ToString

            Dim _lblViewUnitRemain As Label = CType(e.Item.FindControl("lblViewUnitRemain"), Label)
            _lblViewUnitRemain.Text = _objLKPPDetail.UnitRemain.ToString

            If Not IsNothing(_objLKPPDetail.LKPPHeader) Then
                Dim lblViewFaktur As Label = CType(e.Item.FindControl("lblViewFaktur"), Label)
                lblViewFaktur.Attributes("onclick") = "showPopUp('../LKPP/FrmLKPPFaktur.aspx?vehicleTypeCode=" & _objLKPPDetail.VechileType.VechileTypeCode & "&lkppid=" & _objLKPPDetail.LKPPHeader.ID & "','',400,500,'');"
            End If


            Dim _lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            _lblID.Text = _objLKPPDetail.ID.ToString

        End If
    End Sub

    Private Sub SetDgLKPPDetailItemFooter(ByVal e As DataGridItemEventArgs)

        Dim lblFooterKodeModel As Label = CType(e.Item.FindControl("lblFooterKodeModel"), Label)
        lblFooterKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"

        Dim txtFooterKodeModel As TextBox = CType(e.Item.FindControl("txtFooterKodeModel"), TextBox)
        'txtFooterKodeModel.Attributes.Add("readonly", "readonly")

        Dim txtFooterUnit As TextBox = CType(e.Item.FindControl("txtFooterUnit"), TextBox)
        'txtFooterUnit.Attributes.Add("readonly", "readonly")

    End Sub

    Private Sub SetDgLKPPDetailItemEdit(ByVal e As DataGridItemEventArgs, ByVal list As ArrayList)

        Dim _objLKPPDetail As LKPPDetail = list(e.Item.ItemIndex)

        Dim txtEditKodeModel As TextBox = CType(e.Item.FindControl("txtEditKodeModel"), TextBox)
        txtEditKodeModel.Attributes.Add("readonly", "readonly")

        Dim lblEditKodeModel As Label = CType(e.Item.FindControl("lblEditKodeModel"), Label)

        If _objLKPPDetail.UnitRemain < _objLKPPDetail.Unit Then
            lblEditKodeModel.Visible = False
        Else
            lblEditKodeModel.Visible = True
            lblEditKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"
        End If


        Dim txtEditUnit As TextBox = CType(e.Item.FindControl("txtEditUnit"), TextBox)
        txtEditUnit.Text = Convert.ToString(_objLKPPDetail.Unit)

        Dim _lblIDEdit As Label = CType(e.Item.FindControl("lblIDEdit"), Label)
        _lblIDEdit.Text = _objLKPPDetail.ID.ToString

    End Sub

#End Region

#Region "Button & Form"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            btnValidasi.Attributes.Add("OnClick", "return confirm('Simpan & Validasi Perubahan ?');")
        End If
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then

            txtdealername.Attributes.Add("readonly", "readonly")
            txtNote0.Attributes.Add("readonly", "readonly")
            txtNote0.Enabled = False
            txtNote0.BackColor = Color.FromName("E0E0E0")
            lblSearchDealer.Visible = False
            txtdealername.Text = objDealer.DealerCode
            lblNamaDealer.Text = objDealer.DealerName
            txtLKPPNumber.Focus()
            ddlStatus.Enabled = False
            btnApprove.Visible = False
            btnReject.Visible = False
            btnCancelApprove.Visible = False
            btnCancelReject.Visible = False
            If ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Baru And Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
                btnValidasi.Enabled = True
            End If
            If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
                btnApprove.Visible = False
                btnCancelApprove.Visible = False
                btnReject.Visible = False
                btnCancelReject.Visible = False
                btnValidasi.Visible = False
                btnCancelValidatsi.Visible = False
                btnSave.Visible = False
                btnCancelApprove.Visible = False
            End If

        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            btnApprove.Visible = True
            btnReject.Visible = True
            btnValidasi.Visible = False
            btnCancelValidatsi.Visible = False
            btnSave.Visible = False
            Dim Objarr As LKPPHeader = New LKPPHeaderFacade(User).Retrieve(CInt(ViewState("IDLKPPHeader")))
            If Not IsNothing(Objarr) Then
                ''CHecking Ke EndCustomer
                Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, CInt(ViewState("IDLKPPHeader"))))
                criterias.opAnd(New Criteria(GetType(EndCustomer), "ChassisMaster.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                Dim ArrEndCustomer As ArrayList = New EndCustomerFacade(User).Retrieve(criterias)
                If Not IsNothing(ArrEndCustomer) AndAlso ArrEndCustomer.Count > 0 Then

                    btnApprove.Visible = False
                    btnReject.Visible = False
                    btnCancelValidatsi.Visible = False
                    btnCancelApprove.Visible = False
                    btnCancelReject.Visible = False
                Else
                    getEnableApproval(_edit)
                    getVisibleApproval(_edit)
                End If

            End If

            If ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Baru Then
                btnApprove.Visible = False
                btnReject.Visible = False
                btnCancelValidatsi.Visible = False
                btnCancelApprove.Visible = False
                btnCancelReject.Visible = False
            End If


        End If

        If Convert.ToString(sessHelper.GetSession("Status")) = "View" AndAlso Not IsNothing(Request.QueryString("mode")) AndAlso Request.QueryString("mode").ToString().ToLower() = "view" Then
            btnApprove.Visible = False
            btnCancelApprove.Visible = False
            btnReject.Visible = False
            btnCancelReject.Visible = False
            btnValidasi.Visible = False
            btnCancelValidatsi.Visible = False
            btnSave.Visible = False
            btnCancelApprove.Visible = False
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Validation
        If Not Page.IsValid Then
            MessageBox.Show("Invalid Data")
            Return
        End If

        If Not IsLkppValid() Then
            Return
        End If


        SaveLKPP()


    End Sub


    Private Function IsLkppValid() As Boolean
        If txtdealername.Text.Trim = "" Then
            MessageBox.Show("Dealer tidak boleh kosong")
            Return False
        End If

        If ddlClassification.SelectedValue < 0 Then
            MessageBox.Show("Metode pengadaan belum dipilih")
            Return False
        End If

        If Not ValidateDealers(txtdealername.Text.Trim) Then
            MessageBox.Show("Dealer Tidak Valid")
            Return False
        End If

        If dgLKPPDetail.Items.Count < 1 Then
            MessageBox.Show("Detail LKPP belum ada")
            Return False
        End If

        If dgLKPPDetail.EditItemIndex >= 0 Then
            MessageBox.Show("Perubahan Detail Belum disimpan")
            Return False
        End If

        If dgLKPPDetail.ShowFooter Then
            Try
                Dim txtFooterKodeModel As TextBox = dgLKPPDetail.FindControl("txtFooterKodeModel")
                Dim txtFooterUnit As TextBox = dgLKPPDetail.FindControl("txtFooterUnit")

                If txtFooterKodeModel.Text.Trim() <> "" OrElse txtFooterUnit.Text <> "" Then
                    MessageBox.Show("Perubahan Detail Belum disimpan")
                    Return False
                End If
            Catch ex As Exception

            End Try

        End If


        'If Convert.ToString(sessHelper.GetSession("Status")).ToLower() = "update" Then
        '    If Not IsNothing(inFileLocation.PostedFile) AndAlso inFileLocation.PostedFile.FileName.Length > 0 Then
        '        Try
        '            If ddlClassification.SelectedValue <> EnumLKPPClassification.LKPPClassification.E_Catalog Then

        '            End If
        '        Catch ex As Exception

        '        End Try
        '    End If
        'End If

        Return True
    End Function

    Private Sub SaveLKPP(Optional ByVal SaveMode As String = "")
        Dim _lkppDetails As New ArrayList
        Dim _lkppdealers As New ArrayList


        'cek mode
        If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
            If IsExistLKPPNumber(txtLKPPNumber.Text.Trim()) Then
                MessageBox.Show(SR.DataIsExist("LKPPNumber"))
            Else
                If Not sessHelper.GetSession("LKPPDetailList") Is Nothing Then
                    _lkppDetails = CType(sessHelper.GetSession("LKPPDetailList"), ArrayList)
                End If
                Dim objLKPPHeader As New LKPPHeader
                objLKPPHeader.Status = CInt(ddlStatus.SelectedValue)

                GetValueToDatabase(objLKPPHeader)

                Dim dealers() As String = txtdealername.Text.Split(";")
                '  Dim dealercode As String = dealers(0)
                Dim dealercode As String = txtdealername.Text
                Dim objDealerTemp As Dealer = New DealerFacade(User).Retrieve(dealercode)
                Dim objLKPPDealer As LKPPDealer = New LKPPDealer
                objLKPPDealer.Dealer = objDealerTemp
                _lkppdealers.Add(objLKPPDealer)


                If UploadFile(objLKPPHeader) = 1 Then
                    Dim n As Integer = _LKPPHeaderFacade.InsertLKPPHeader(objLKPPHeader, _lkppDetails, _lkppdealers)
                    If n = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        RemoveALLSession()

                        sessHelper.SetSession("Status", "Update")
                        sessHelper.SetSession("IDLKPPHeader", objLKPPHeader.ID)
                        Response.Redirect("FrmLKPPDetail.aspx?Mode=Edit")
                        MessageBox.Show("Simpan Berhasil")
                        btnValidasi.Enabled = True
                    End If
                ElseIf UploadFile(objLKPPHeader) = 2 Then
                    MessageBox.Show("File Tidak Sama")
                ElseIf UploadFile(objLKPPHeader) = 3 Then
                    MessageBox.Show("Hanya menerima file format PDF atau Zip (Ukuran Maksimum File 2Mb)")
                ElseIf UploadFile(objLKPPHeader) = 4 Then
                    MessageBox.Show("File yang diupload tidak boleh lebih dari 2MB")
                ElseIf UploadFile(objLKPPHeader) = 5 Then
                    MessageBox.Show("Harus ada file yang diupload")
                Else
                    MessageBox.Show("File gagal simpan di Server. Harap hubungi Administrator")
                End If
            End If
            'MODE UPDATE
        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            Dim idLKPPHeader As Integer = CInt(sessHelper.GetSession("IDLKPPHeader"))
            Dim ObjLKPPHeader As LKPPHeader = _LKPPHeaderFacade.Retrieve(idLKPPHeader)

            '
            If Not IsNothing(ObjLKPPHeader) Then
                If txtLKPPNumber.Text.Trim <> ObjLKPPHeader.ReferenceNumber Then
                    If IsExistLKPPNumber(txtLKPPNumber.Text.Trim()) Then
                        MessageBox.Show(SR.DataIsExist("LKPPNumber"))
                        Return
                    Else
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, ObjLKPPHeader.ID))
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
                            MessageBox.Show("Status Faktur sudah selesai, No. LKPP tidak bisa dirubah")
                            Return
                        End If
                    End If
                End If
                'END  UPDATE
                If ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Baru OrElse SaveMode.ToLower() = "validasi" Then
                    '  ddlStatus.Enabled = False
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, ObjLKPPHeader.ID))
                    criterias.opAnd(New Criteria(GetType(EndCustomer), "Customer.ID", MatchType.Greater, 0))
                    Dim arlEndCustomer As ArrayList = New EndCustomerFacade(User).Retrieve(criterias)
                    If arlEndCustomer.Count > 0 Then
                        MessageBox.Show("Aplikasi sudah digunakan di Faktur")
                        Return
                    Else
                        If SaveMode.ToLower() = "validasi" Then
                            ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Validasi
                        Else
                            ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Baru
                        End If

                    End If

                    'Dim _OldLKPPDetail As ArrayList = CType(sessHelper.GetSession("OldLKPPDetaiLList"), ArrayList)
                    'Dim _OldLKPPDealer As ArrayList = CType(sessHelper.GetSession("OldLKPPDealer"), ArrayList)

                    'If Not _OldLKPPDetail Is Nothing Then
                    '    Dim _delDetail As Integer = _LKPPHeaderFacade.DeleteOLDLKPPDetail(_OldLKPPDetail)
                    'End If
                    'If Not _OldLKPPDealer Is Nothing Then
                    '    Dim _delDealer As Integer = _LKPPHeaderFacade.DeleteOLDDealer(_OldLKPPDealer)
                    'End If

                    If Not sessHelper.GetSession("LKPPDetaiLList") Is Nothing Then
                        _lkppDetails = CType(sessHelper.GetSession("LKPPDetaiLList"), ArrayList)
                        If _lkppDetails.Count > 0 Then
                            For Each LKPPDet As LKPPDetail In _lkppDetails
                                LKPPDet.LKPPHeader = ObjLKPPHeader
                                ObjLKPPHeader.LKPPDetails.Add(LKPPDet)
                            Next
                        End If
                    End If

                    Dim dealers() As String = txtdealername.Text.Split(";")
                    '  Dim dealercode As String = dealers(0)
                    Dim dealercode As String = txtdealername.Text
                    Dim objDealerTemp As Dealer = New DealerFacade(User).Retrieve(dealercode)
                    Dim objLKPPDealer As LKPPDealer = New LKPPDealer
                    objLKPPDealer.Dealer = objDealerTemp
                    _lkppdealers.Add(objLKPPDealer)

                    If _lkppdealers.Count > 0 Then
                        For Each LKPPDealer As LKPPDealer In _lkppdealers
                            ObjLKPPHeader.LKPPDealers.Add(LKPPDealer)
                        Next
                    End If

                    GetValueToDatabase(ObjLKPPHeader)

                    'UPLOAD FILE
                    If UploadFile(ObjLKPPHeader) = 1 Then

                        Dim _OldLKPPDetail As ArrayList = CType(sessHelper.GetSession("OldLKPPDetaiLList"), ArrayList)
                        Dim _OldLKPPDealer As ArrayList = CType(sessHelper.GetSession("OldLKPPDealer"), ArrayList)

                        If Not _OldLKPPDetail Is Nothing Then
                            Dim _delDetail As Integer = _LKPPHeaderFacade.DeleteOLDLKPPDetail(_OldLKPPDetail)
                        End If
                        If Not _OldLKPPDealer Is Nothing Then
                            Dim _delDealer As Integer = _LKPPHeaderFacade.DeleteOLDDealer(_OldLKPPDealer)
                        End If


                        Dim n As Integer = _LKPPHeaderFacade.UpdateLKPPHeader(ObjLKPPHeader, _lkppDetails, _lkppdealers)
                        If n = -1 Then
                            MessageBox.Show(SR.UpdateFail)
                        Else
                            RemoveALLSession()
                            RemoveALLSession()
                            sessHelper.SetSession("Status", "Update")
                            sessHelper.SetSession("IDLKPPHeader", ObjLKPPHeader.ID)
                            ' btnSave.Enabled = False
                            If SaveMode.ToLower() = "validasi" Then
                                sessHelper.SetSession("Status", "View")
                                GetEnableControl(False)
                                dgLKPPDetail.ShowFooter = False
                                inFileLocation.Disabled = True
                                btnValidasi.Enabled = False

                                MessageBox.Show("Validasi Berhasil")

                            Else
                                lblAttachment.Text = ObjLKPPHeader.Attachment
                                hdnAttachment.Value = ObjLKPPHeader.Attachment
                                MessageBox.Show("Simpan Berhasil")
                                btnValidasi.Enabled = True
                            End If

                        End If
                    ElseIf UploadFile(ObjLKPPHeader) = 2 Then
                        MessageBox.Show("File Tidak Sama")
                    ElseIf UploadFile(ObjLKPPHeader) = 3 Then
                        MessageBox.Show("Hanya menerima file format PDF atau Zip (Ukuran Maksimum File 2Mb)")
                    ElseIf UploadFile(ObjLKPPHeader) = 4 Then
                        MessageBox.Show("File yang diupload tidak boleh lebih dari 2MB")
                    ElseIf UploadFile(ObjLKPPHeader) = 5 Then
                        MessageBox.Show("Harus ada file yang diupload")
                    Else
                        MessageBox.Show("File gagal simpan di Server. Harap hubungi Administrator")
                    End If
                End If

            End If

        End If


    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveALLSession()
        Response.Redirect("FrmLKPPHeader.aspx")
    End Sub

    Protected Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click



        If Not Page.IsValid Then
            MessageBox.Show("Invalid Data")
            Return
        End If

        If dgLKPPDetail.Items.Count < 1 Then
            MessageBox.Show("Detail LKPP belum ada")
            Exit Sub
        End If

        If Not IsLkppValid() Then
            Return
        End If

        SaveLKPP("validasi")

    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Dim idLKPPHeader As Integer = CInt(sessHelper.GetSession("IDLKPPHeader"))

        Dim ObjLKPPHeader As LKPPHeader = _LKPPHeaderFacade.Retrieve(idLKPPHeader)
        ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Setuju
        ObjLKPPHeader.Notes = txtNote0.Text
        Dim n As Integer = _LKPPHeaderFacade.ValidateLKPPHeader(ObjLKPPHeader)
        If n = -1 Then
            MessageBox.Show("Tidak ada LKPP dengan status Baru yang dipilih")
        Else
            RemoveALLSession()
            RemoveALLSession()
            sessHelper.SetSession("Status", "View")
            ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Setuju
            GetEnableControl(False)
            dgLKPPDetail.ShowFooter = False
            inFileLocation.Disabled = True
            'btnSave.Enabled = False
            MessageBox.Show("Approve Berhasil")
            btnApprove.Enabled = False
            btnReject.Enabled = False
        End If
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Dim idLKPPHeader As Integer = CInt(sessHelper.GetSession("IDLKPPHeader"))
        Dim ObjLKPPHeader As LKPPHeader = _LKPPHeaderFacade.Retrieve(idLKPPHeader)
        ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Tolak
        ObjLKPPHeader.Notes = txtNote0.Text
        Dim n As Integer = _LKPPHeaderFacade.ValidateLKPPHeader(ObjLKPPHeader)
        If n = -1 Then
            MessageBox.Show("Tidak ada LKPP dengan status Baru yang dipilih")
        Else
            RemoveALLSession()
            RemoveALLSession()
            sessHelper.SetSession("Status", "View")
            ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Tolak
            GetEnableControl(False)
            dgLKPPDetail.ShowFooter = False
            inFileLocation.Disabled = True
            'btnSave.Enabled = False
            MessageBox.Show("Reject Berhasil")
            btnReject.Enabled = False
            btnApprove.Enabled = False
        End If
    End Sub

    Protected Sub btnCancelApprove_Click(sender As Object, e As EventArgs) Handles btnCancelApprove.Click
        Dim idLKPPHeader As Integer = CInt(sessHelper.GetSession("IDLKPPHeader"))
        Dim ObjLKPPHeader As LKPPHeader = _LKPPHeaderFacade.Retrieve(idLKPPHeader)
        ObjLKPPHeader.Notes = txtNote0.Text
        If ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Setuju Then

            Dim Objarr As LKPPHeader = New LKPPHeaderFacade(User).Retrieve(CInt(ViewState("IDLKPPHeader")))
            If Not IsNothing(Objarr) Then
                ''CHecking Ke EndCustomer
                Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, CInt(ViewState("IDLKPPHeader"))))
                criterias.opAnd(New Criteria(GetType(EndCustomer), "ChassisMaster.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                Dim ArrEndCustomer As ArrayList = New EndCustomerFacade(User).Retrieve(criterias)
                If Not IsNothing(ArrEndCustomer) AndAlso ArrEndCustomer.Count > 0 Then
                    MessageBox.Show("LKPP Sudah Terpakai di faktur")
                    Exit Sub
                End If
            End If
            ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Validasi
            Dim n As Integer = _LKPPHeaderFacade.ValidateLKPPHeader(ObjLKPPHeader)
            If n = -1 Then
                MessageBox.Show("Tidak ada LKPP dengan status Setuju yang dipilih")
            Else
                RemoveALLSession()
                RemoveALLSession()
                sessHelper.SetSession("Status", "View")
                ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Validasi
                GetEnableControl(False)
                dgLKPPDetail.ShowFooter = False
                inFileLocation.Disabled = True
                'btnSave.Enabled = False
                MessageBox.Show("Status setuju sudah dibatalkan, berubah menjadi Validasi")
                btnCancelApprove.Enabled = False

            End If
        End If
    End Sub

    Protected Sub btnCancelReject_Click(sender As Object, e As EventArgs) Handles btnCancelReject.Click
        Dim idLKPPHeader As Integer = CInt(sessHelper.GetSession("IDLKPPHeader"))
        Dim ObjLKPPHeader As LKPPHeader = _LKPPHeaderFacade.Retrieve(idLKPPHeader)
        ObjLKPPHeader.Notes = txtNote0.Text
        If ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Tolak Then
            ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Validasi
            Dim n As Integer = _LKPPHeaderFacade.ValidateLKPPHeader(ObjLKPPHeader)
            If n = -1 Then
                MessageBox.Show("Tidak ada LKPP dengan status Tolak yang dipilih")
            Else
                RemoveALLSession()
                RemoveALLSession()
                sessHelper.SetSession("Status", "View")
                ddlStatus.SelectedValue = EnumStatusLKPP.StatusLKPP.Validasi
                GetEnableControl(False)
                dgLKPPDetail.ShowFooter = False
                inFileLocation.Disabled = True
                'btnSave.Enabled = False
                MessageBox.Show("Status tolak sudah dibatalkan, berubah menjadi Validasi")
                btnCancelReject.Enabled = False

            End If
        End If
    End Sub

    Protected Sub btnCancelValidatsi_Click(sender As Object, e As EventArgs) Handles btnCancelValidatsi.Click
        Dim idLKPPHeader As Integer = CInt(sessHelper.GetSession("IDLKPPHeader"))
        Dim ObjLKPPHeader As LKPPHeader = _LKPPHeaderFacade.Retrieve(idLKPPHeader)
        If ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Validasi Then
            ObjLKPPHeader.Status = EnumStatusLKPP.StatusLKPP.Baru
            Dim n As Integer = _LKPPHeaderFacade.ValidateLKPPHeader(ObjLKPPHeader)
            If n = -1 Then
                MessageBox.Show("Tidak ada LKPP dengan status Validasi yang dipilih")
            Else
                RemoveALLSession()
                RemoveALLSession()
                sessHelper.SetSession("Status", "View")
                ddlStatus.SelectedValue = 0
                GetEnableControl(False)
                dgLKPPDetail.ShowFooter = False
                inFileLocation.Disabled = True
                'btnSave.Enabled = False
                MessageBox.Show("Status validasi sudah dibatalkan, berubah menjadi Baru")
                btnCancelValidatsi.Enabled = False
            End If
        End If
    End Sub

    Private Function cekFaktur(ByVal vehicletypecode As String, _
                            ByVal lkppid As Integer) As Boolean
        Dim result As Boolean = False
        Dim o As New EndCustomer
        Dim _sessHelper As New SessionHelper
        'o.Customer.ID
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, lkppid))
        criterias.opAnd(New Criteria(GetType(EndCustomer), "ChassisMaster.VechileColor.VechileType.VechileTypeCode", MatchType.Exact, vehicletypecode))
        criterias.opAnd(New Criteria(GetType(EndCustomer), "Customer.ID", MatchType.Greater, 0))
        Dim _result As ArrayList = New EndCustomerFacade(User).RetrieveByCriteria(criterias, CType(ViewState("currentSortColumn"), String), _
                                                                                  CType(ViewState("currentSortDirection"), Sort.SortDirection))
        If _result.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Grid"
    Private Sub dgLKPPDetail_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgLKPPDetail.ItemCommand
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
                dgLKPPDetail_EditCommand(e)
            Case "Update"
                dgLKPPDetail_Update(e)
            Case "Cancel"
                dgLKPPDetail_CancelCommand(e)
        End Select
    End Sub

    Private Sub dgLKPPDetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgLKPPDetail.ItemDataBound
        Dim list As ArrayList = CType(sessHelper.GetSession("LKPPHeaderVIEW"), ArrayList)

        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                SetDgLKPPDetailItemView(e, list)
                Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                lbtnDelete.Visible = False
                Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
                lbtnEdit.Visible = False
            End If
        Else
            If e.Item.ItemType = ListItemType.Footer Then
                SetDgLKPPDetailItemFooter(e)
            ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                SetDgLKPPDetailItemEdit(e, list)
            ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                SetDgLKPPDetailItemView(e, list)
            End If
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Anda yakin data ini ingin dihapus?');")
            End If
        End If
    End Sub
#End Region




End Class