#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessValidation.Helpers
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

#Region " .NET Namespace "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmDepB_KewajibanInput
    Inherits System.Web.UI.Page

#Region " Private Variables"
    'Private _arlKewajiban As ArrayList
    Private _sesshelper As SessionHelper = New SessionHelper
    Private _mode As enumMode.Mode
    Private _vstKewajiban As String = "_vstKewajiban"
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
#End Region

#Region "Event Handler"
    Private Sub InitiateAuthorization()
        Dim objDealer As Dealer = Me._sesshelper.GetSession("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then

            Dim _lihat_daftar_kewajiban_Privilege As Boolean = False
            Dim _input_daftar_kewajiban_Privilege As Boolean = False
            _lihat_daftar_kewajiban_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_kewajiban_Privilege)
            _input_daftar_kewajiban_Privilege = SecurityProvider.Authorize(Context.User, SR.input_daftar_kewajiban_Privilege)

            If _input_daftar_kewajiban_Privilege = False AndAlso _lihat_daftar_kewajiban_Privilege = False Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Input Kewajiban")
            End If


            Dim strMode As String = ""
            If Not IsNothing(_sesshelper.GetSession("DepositBKewajibanHeaderMode")) Then
                If _sesshelper.GetSession("DepositBKewajibanHeaderMode").ToString().ToLower() = "view" Then
                    If _lihat_daftar_kewajiban_Privilege = False Then
                        Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Input Kewajiban")
                    End If
                End If
            End If

            If Not _input_daftar_kewajiban_Privilege Then
                btnSave.Visible = False
                btnCancel.Visible = False
            End If

        Else
            If Not SecurityProvider.Authorize(Context.User, SR.lihat_daftar_kewajiban_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Input Kewajiban")
            End If
            btnSave.Visible = False
            btnCancel.Visible = False

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsPostBack() Then
            BindTipeKewajiban()
            ddlKewajiban_SelectedIndexChanged(sender, e)
            BindYear()
            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, False, companyCode)
            InitialPage()
        End If
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        _sesshelper.SetSession("ItemError", 0)
        Dim NamaFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)
        If (fileUpload.PostedFile Is Nothing) OrElse fileUpload.PostedFile.ContentLength <= 0 Then
            MessageBox.Show("Pilih file yang akan di-upload.")
            Return
        Else
            If fileUpload.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
            fileUpload.PostedFile.ContentType.ToString <> "application/octet-stream" And _
            fileUpload.PostedFile.ContentType.ToString <> "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then

                MessageBox.Show("Bukan file EXCEL. File anda " & fileUpload.PostedFile.ContentType)
                Return

            Else
                'cek maxFileSize First
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If fileUpload.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                Else
                    Dim filename As String = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
                    Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & filename
                    Dim arlParseResult As New ArrayList

                    If IsFileExist(targetFile) Then
                        MessageBox.Show(SR.UploadFail("Kewajiban Deposit B"))
                        Return
                    End If

                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

                    Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                    sapImp.Start()

                    Try
                        SavingToFolder(targetFile, fileUpload.PostedFile)

                        Dim objDepositBUploadKewajibanParser As DepositBUploadKewajibanParser = New DepositBUploadKewajibanParser
                        Dim arSheet As ArrayList
                        arSheet = objDepositBUploadKewajibanParser.GetSheet(targetFile)
                        arlParseResult = objDepositBUploadKewajibanParser.ParsingExcel(targetFile, "[" & CType(arSheet(0), String) & "]", "User", CInt(ddlKewajiban.SelectedValue))
                        _sesshelper.SetSession("ArrKewajibanDetail", arlParseResult)
                        dgUpload.Columns(6).Visible = True
                        'dgUpload.Columns(3).Visible = False
                        dgUpload.DataSource = arlParseResult
                        dgUpload.DataBind()
                    Catch
                        Throw

                    Finally
                        sapImp.StopImpersonate()
                    End Try
                End If
            End If
        End If

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ClearSession()
        Response.Redirect("FrmDepB_KewajibanList.aspx")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtKodeDealer.Text = String.Empty
        ddlKewajiban.SelectedIndex = 0
        ddlYear.SelectedIndex = 0
        dgUpload.DataSource = Nothing
        dgUpload.DataBind()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim strMessage As String = String.Empty
        strMessage = Validation()
        If strMessage = String.Empty Then
            SimpanKewajiban()
            'Page_Load(sender, e)
        Else
            MessageBox.Show(strMessage)
        End If
    End Sub

    Private Sub ddlKewajiban_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKewajiban.SelectedIndexChanged
        'If ddlKewajiban.SelectedIndex = 1 Then
        '    dgUpload.Visible = False
        'Else
        'dgUpload.Visible = True

        _sesshelper.RemoveSession("SessKewajibanHeader")
        _sesshelper.RemoveSession("ArrKewajibanDetail")

        Dim objPencairan As DataTable = New DataTable
        dgUpload.DataSource = objPencairan
        dgUpload.DataBind()
        'End If
    End Sub

    Private Sub dgUpload_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUpload.ItemCommand

        Select Case e.CommandName
            Case "Add"
                dgUpload_AddCommand(e)
            Case "Edit"
                dgUpload_EditCommand(e)
            Case "Update"
                dgUpload_UpdateCommand(e)
            Case "Cancel"
                dgUpload_CancelCommand(e)
            Case "Delete"
                dgUpload_DeleteCommand(e)

        End Select
    End Sub

    Private Sub dgUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetDtgKewajibanItemFooter(e)
        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgKewajibanItemEdit(e)
        Else
            SetDtgKewajibanItem(e)
        End If
    End Sub

    Private Sub dgUpload_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgUpload.SortCommand

    End Sub

#End Region

#Region "Custom Method"
    Private Function IsFileExist(ByVal filename As String) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

        Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        sapImp.Start()

        Try
            '---Mode using sapImpersonate---
            Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(filename)

            'Return fInfo.Exists
            If fInfo.Exists Then
                fInfo.Delete()
                Return False
            End If

        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(filename)))
        Finally
            sapImp.StopImpersonate()
        End Try
    End Function

    Private Sub SavingToFolder(ByVal targetFile As String, ByVal postedFile As HttpPostedFile)

        Try
            postedFile.SaveAs(targetFile)
            'fInfo.MoveTo(targetFile)

            Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
            If Not trgInfo.Exists Then
                Throw New IO.IOException("File gagal disimpan di Server. Harap hubungi Administrator")
            End If
        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(targetFile)))
        End Try
    End Sub

    Private Sub InitialPage()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

        'Dim objUserInfo As UserInfo = _sesshelper.GetSession("LOGINUSERINFO")
        'If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    txtKodeDealer.Attributes.Remove("readonly")
        'Else
        '    txtKodeDealer.Attributes.Add("readonly", "readonly")
        'End If

        If Not IsNothing(_sesshelper.GetSession("DepositBKewajibanHeaderID")) Then
            Dim HeaderID As Integer = CType(_sesshelper.GetSession("DepositBKewajibanHeaderID"), Integer)
            Dim objHeader As DepositBKewajibanHeader = New DepositBKewajibanHeaderFacade(User).Retrieve(HeaderID)
            If Not IsNothing(objHeader) Then
                _sesshelper.SetSession("SessKewajibanHeader", objHeader)
                _sesshelper.SetSession("ArrKewajibanDetail", objHeader.DepositBKewajibanDetails)
                BindKewajibanHeader(objHeader)
            End If
        End If
        If Not IsNothing(_sesshelper.GetSession("ArrKewajibanDetail")) Then
            BindKewajibanDetail()
        End If



        SetControlMode()

    End Sub

    Private Sub SetControlMode()
        If Not IsNothing(_sesshelper.GetSession("DepositBKewajibanHeaderMode")) Then
            Dim mode As String = _sesshelper.GetSession("DepositBKewajibanHeaderMode")
            If mode = "edit" Then
                'txtKodeDealer.Attributes.Remove("readonly")
                'txtKodeDealer.Enabled = True
                'lblSearchDealer.Visible = True
                txtKodeDealer.Attributes.Add("readonly", "readonly")
                txtKodeDealer.Enabled = False
                lblSearchDealer.Visible = False
                btnCancel.Enabled = True
                btnSave.Enabled = True
                btnUpload.Enabled = False
                fileUpload.Disabled = True
                dgUpload.ShowFooter = True
                dgUpload.Columns(7).Visible = True
            End If
            If mode = "view" Then
                txtKodeDealer.Attributes.Add("readonly", "readonly")
                txtKodeDealer.Enabled = False
                lblSearchDealer.Visible = False
                btnCancel.Enabled = False
                btnSave.Enabled = False
                btnUpload.Enabled = False
                fileUpload.Disabled = True
                dgUpload.ShowFooter = False
                dgUpload.Columns(7).Visible = False
            End If
            ddlKewajiban.Enabled = False
            ddlYear.Enabled = False
            btnCancel.Visible = False
        Else
            btnBack.Visible = False
        End If
    End Sub

 

    Private Sub BindTipeKewajiban()
        Dim items As New ArrayList
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveTipeKewajiban(False)
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next

        ddlKewajiban.DataSource = items
        ddlKewajiban.DataTextField = "NameType"
        ddlKewajiban.DataValueField = "ValType"
        ddlKewajiban.DataBind()
    End Sub

    Private Sub BindYear()
        Dim curYear As Integer = Date.Now.Year
        Dim startYear As Integer = curYear - 3
        Dim EndYear As Integer = curYear + 5
        Dim intYear As Integer = 0
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))

        ddlYear.Items.Clear()
        For intYear = startYear To EndYear
            ddlYear.Items.Add(intYear.ToString)
        Next

        ddlYear.Items.FindByValue(Date.Now.Year.ToString).Selected = True
    End Sub

    Private Sub BindKewajibanHeader(ByVal objHeader As DepositBKewajibanHeader)
        Try
            txtKodeDealer.Text = objHeader.Dealer.DealerCode
            ddlKewajiban.SelectedValue = objHeader.TipeKewajiban
            ddlYear.SelectedValue = objHeader.PeriodYear
            If Not IsNothing(objHeader.ProductCategory) Then
                ddlProductCategory.SelectedValue = objHeader.ProductCategory.ID
            End If

        Catch ex As Exception
            MessageBox.Show("Error Data Kewajiban")
        End Try
    End Sub

    Private Sub BindKewajibanDetail()
        Dim _arlKewajiban As ArrayList
        _arlKewajiban = CType(Session("ArrKewajibanDetail"), ArrayList)

        'Dim _arlFiltered As New ArrayList
        'For Each obj As DepositBKewajibanDetail In _arlKewajiban
        '    If obj.RowStatus = DBRowStatus.Active Then
        '        _arlFiltered.Add(obj)
        '    End If
        'Next

        ''-- If any error exists then unable to save into DNet database
        'Dim bError As Boolean = False  '-- True if any error exists
        'If bError OrElse dgUpload.EditItemIndex <> -1 Then
        '    btnSave.Enabled = False
        'Else
        '    btnSave.Enabled = True
        'End If

        dgUpload.DataSource = _arlKewajiban
        dgUpload.DataBind()

        'For Each item As DepositBKewajibanDetail In _arlKewajiban
        '    If item.ErrorMessage <> "" Then
        '        btnSave.Enabled = False
        '    End If
        'Next

    End Sub

    Private Sub ClearSession()
        _sesshelper.RemoveSession("DepositBKewajibanHeaderID")
        _sesshelper.RemoveSession("DepositBKewajibanHeaderMode")
        _sesshelper.RemoveSession("ArrKewajibanDetail")
    End Sub

    Private Sub SimpanKewajiban()
        Try
            If Not IsNothing(_sesshelper.GetSession("DepositBKewajibanHeaderMode")) Then
                Dim mode As String = _sesshelper.GetSession("DepositBKewajibanHeaderMode")
                If mode = "edit" Then
                    UpdateData()
                Else
                    AddNewData()
                End If
            Else
                AddNewData()
            End If
            MessageBox.Show(SR.SaveSuccess)
            btnCancel_Click(Nothing, Nothing)
            ddlKewajiban_SelectedIndexChanged(Nothing, Nothing)
            'btnCancel.Enabled = False
            'btnSave.Enabled = False
            'dgUpload.ShowFooter = False
            '_sesshelper.SetSession("DepositBKewajibanHeaderMode", "view")
            'SetControlMode()
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    Private Sub AddNewData()
        Dim _arlDetails As ArrayList
        _arlDetails = CType(Session("ArrKewajibanDetail"), ArrayList)

        Dim dealers As String()
        dealers = txtKodeDealer.Text.Split(";")
        If dealers.Length > 0 Then
            For Each dealerCode As String In dealers
                Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(dealerCode)
                If Not IsNothing(objDealer) Then
                    Dim objKewajiban As DepositBKewajibanHeader
                    objKewajiban = CType(Session("SessKewajibanHeader"), DepositBKewajibanHeader)
                    If IsNothing(objKewajiban) Then
                        objKewajiban = New DepositBKewajibanHeader
                    End If
                    Dim objProdCat As ProductCategory = New KTB.DNet.BusinessFacade.FinishUnit.ProductCategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
                    If Not IsNothing(objProdCat) Then
                        objKewajiban.ProductCategory = objProdCat
                    End If
                    objKewajiban.TipeKewajiban = CInt(ddlKewajiban.SelectedValue)
                    objKewajiban.PeriodYear = CShort(ddlYear.SelectedValue)

                    objKewajiban.Dealer = objDealer
                    If _arlDetails.Count > 0 Then
                        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateTime.Now, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                        For Each detail As DepositBKewajibanDetail In _arlDetails
                            detail.Tax = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=(detail.Harga * detail.Qty))
                            detail.DepositBKewajibanHeader = objKewajiban
                        Next
                    End If
                    Dim vReturn As Integer = New DepositBKewajibanHeaderFacade(User).InsertTransaction(objKewajiban, _arlDetails)
                End If
            Next
        End If
        'MessageBox.Show(SR.SaveSuccess)
    End Sub

    Private Sub UpdateData()
        Dim objKewajiban As DepositBKewajibanHeader = _sesshelper.GetSession("SessKewajibanHeader")
        If Not IsNothing(objKewajiban) Then
            Dim _arlDetails As ArrayList
            _arlDetails = CType(Session("ArrKewajibanDetail"), ArrayList)
            Dim dealers As String()
            dealers = txtKodeDealer.Text.Split(";")
            If dealers.Length > 0 Then
                For Each dealerCode As String In dealers
                    Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(dealerCode)
                    If Not IsNothing(objDealer) Then
                        Dim objProdCat As ProductCategory = New KTB.DNet.BusinessFacade.FinishUnit.ProductCategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
                        If Not IsNothing(objProdCat) Then
                            objKewajiban.ProductCategory = objProdCat
                        End If
                        objKewajiban.TipeKewajiban = CInt(ddlKewajiban.SelectedValue)
                        objKewajiban.PeriodYear = CShort(ddlYear.SelectedValue)
                        objKewajiban.Dealer = objDealer
                        If _arlDetails.Count > 0 Then
                            Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objKewajiban.CreatedTime, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                            For Each detail As DepositBKewajibanDetail In _arlDetails
                                detail.Tax = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=(detail.Harga * detail.Qty))
                                detail.DepositBKewajibanHeader = objKewajiban
                            Next
                        End If
                        Dim vReturn As Integer = New DepositBKewajibanHeaderFacade(User).UpdateTransaction(objKewajiban, _arlDetails)
                    End If
                Next
            End If
            'MessageBox.Show(SR.SaveSuccess)
        End If
    End Sub

    Private Function Validation() As String
        Dim msg As String = String.Empty
        If txtKodeDealer.Text.Trim = String.Empty Then
            msg = "Kode dealer tidak boleh kosong."
        End If
        If ddlProductCategory.SelectedValue = -1 Then
            msg = msg + "\n" + " Tentukan kategory produk ."
        End If
        Dim _arlKewajiban As ArrayList = _sesshelper.GetSession("ArrKewajibanDetail")
        If (Not IsNothing(_arlKewajiban) AndAlso _arlKewajiban.Count = 0) Or IsNothing(_arlKewajiban) Then
            msg = msg + "\n" + " Data barang tidak boleh kosong."
        End If

        If dgUpload.Items.Count > 0 Then
            For Each item As DataGridItem In dgUpload.Items
                Dim lblMsg As Label = item.FindControl("lblMessage")
                If lblMsg.Text <> "" Then
                    msg = msg + "\n" + " Data detail error, lihat kolom Pesan Kesalahan."
                End If
            Next
        End If

        Return msg
    End Function

    Private Sub SetDtgKewajibanItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not e.Item.DataItem Is Nothing Then
            Dim objDepositBKewajibanDetail As DepositBKewajibanDetail = CType(e.Item.DataItem, DepositBKewajibanDetail)
            If objDepositBKewajibanDetail.RowStatus = DBRowStatus.Deleted Then
                e.Item.Visible = False
            Else
                Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                    Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                    Dim lblPartCode As Label = CType(e.Item.FindControl("lblPartCode"), Label)
                    Dim lblPartName As Label = CType(e.Item.FindControl("lblPartName"), Label)
                    Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                    Dim lblQty As Label = CType(e.Item.FindControl("lblQty"), Label)


                    lblNo.Text = (dgUpload.CurrentPageIndex * dgUpload.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

                    If Not IsNothing(objDepositBKewajibanDetail.SparePartMaster) Then
                        lblPartCode.Text = objDepositBKewajibanDetail.SparePartMaster.PartNumber
                        lblPartName.Text = objDepositBKewajibanDetail.SparePartMaster.PartName
                        lblAmount.Text = FormatNumber(objDepositBKewajibanDetail.SparePartMaster.RetalPrice, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                    End If
                    If Not IsNothing(objDepositBKewajibanDetail.EquipmentMaster) Then
                        lblPartCode.Text = objDepositBKewajibanDetail.EquipmentMaster.EquipmentNumber
                        lblPartName.Text = objDepositBKewajibanDetail.EquipmentMaster.Description
                        'lblAmount.Text = FormatNumber(objDepositBKewajibanDetail.EquipmentMaster.Price, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                        lblAmount.Text = FormatNumber(objDepositBKewajibanDetail.Harga, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                    End If
                    lblQty.Text = objDepositBKewajibanDetail.Qty

                    If e.Item.Cells(6).Visible = True Then
                        Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
                        lblMessage.Text = objDepositBKewajibanDetail.ErrorMessage
                    End If

                    'Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                    'Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

                    If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                        CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                            New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
                    End If
                    If objDepositBKewajibanDetail.ErrorMessage <> "" Then
                        e.Item.BackColor = Color.Red
                    End If
                End If
            End If
            
        End If
    End Sub

    Private Sub SetDtgKewajibanItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblPartCodeFooter As Label = CType(e.Item.FindControl("lblPartCodeFooter"), Label)
        lblPartCodeFooter.Attributes("onclick") = "ShowEquipPart(this);"
    End Sub

    Private Sub SetDtgKewajibanItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblPartCodeEdit As Label = CType(e.Item.FindControl("lblPartCodeEdit"), Label)
        lblPartCodeEdit.Attributes("onclick") = "ShowEquipPartEdit(this);"

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblPartIDEdit As TextBox = CType(e.Item.FindControl("lblPartIDEdit"), TextBox)
        Dim txtPartCodeEdit As TextBox = CType(e.Item.FindControl("txtPartCodeEdit"), TextBox)
        Dim lblPartNameEdit As Label = CType(e.Item.FindControl("lblPartNameEdit"), Label)
        Dim lblAmountEdit As Label = CType(e.Item.FindControl("lblAmountEdit"), Label)
        Dim txtQtyEdit As TextBox = CType(e.Item.FindControl("txtQtyEdit"), TextBox)

        lblNo.Text = (dgUpload.CurrentPageIndex * dgUpload.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
        Dim objDepositBKewajibanDetail As DepositBKewajibanDetail = CType(e.Item.DataItem, DepositBKewajibanDetail)
        If Not IsNothing(objDepositBKewajibanDetail) Then
            If Not IsNothing(objDepositBKewajibanDetail.SparePartMaster) Then
                lblPartIDEdit.Text = objDepositBKewajibanDetail.SparePartMaster.ID
                txtPartCodeEdit.Text = objDepositBKewajibanDetail.SparePartMaster.PartNumber
                lblPartNameEdit.Text = objDepositBKewajibanDetail.SparePartMaster.PartName
                lblAmountEdit.Text = FormatNumber(objDepositBKewajibanDetail.SparePartMaster.RetalPrice, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            End If
            If Not IsNothing(objDepositBKewajibanDetail.EquipmentMaster) Then
                lblPartIDEdit.Text = objDepositBKewajibanDetail.EquipmentMaster.ID
                txtPartCodeEdit.Text = objDepositBKewajibanDetail.EquipmentMaster.EquipmentNumber
                lblPartNameEdit.Text = objDepositBKewajibanDetail.EquipmentMaster.Description
                lblAmountEdit.Text = FormatNumber(objDepositBKewajibanDetail.EquipmentMaster.Price, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
            End If
            txtQtyEdit.Text = objDepositBKewajibanDetail.Qty
        End If
    End Sub

    Private Sub dgUpload_AddCommand(ByVal e As DataGridCommandEventArgs)
        Dim txtQtyFooter As TextBox = CType(e.Item.FindControl("txtQtyFooter"), TextBox)
        If txtQtyFooter.Text.Trim = "" Then
            MessageBox.Show("Quantity harus diisi lebih besar atau sama dengan 1")
            Exit Sub
        End If
        If (CInt(ddlKewajiban.SelectedValue) = DepositBEnum.TipeKewajiban.Regular) Then
            AddEquipmentMaster(e)
        End If
        If (CInt(ddlKewajiban.SelectedValue) = DepositBEnum.TipeKewajiban.NonReguler) Then
            AddSparePartMaster(e)
        End If
    End Sub

    Private Sub dgUpload_EditCommand(ByVal e As DataGridCommandEventArgs)
        btnSave.Enabled = False
        dgUpload.ShowFooter = False
        dgUpload.EditItemIndex = CInt(e.Item.ItemIndex)
        BindKewajibanDetail()

    End Sub

    Private Sub dgUpload_CancelCommand(ByVal E As DataGridCommandEventArgs)
        btnSave.Enabled = True
        dgUpload.EditItemIndex = -1
        BindKewajibanDetail()
        dgUpload.ShowFooter = True
    End Sub

    Private Sub dgUpload_UpdateCommand(ByVal e As DataGridCommandEventArgs)

        Dim txtQty As TextBox = CType(e.Item.FindControl("txtQtyEdit"), TextBox)
        If txtQty.Text.Trim = "" Then
            MessageBox.Show("Quantity harus diisi lebih besar atau sama dengan 1")
            Exit Sub
        End If
        UpdateCommand(e)
    End Sub

    Private Sub dgUpload_DeleteCommand(ByVal e As DataGridCommandEventArgs)
        If (CInt(ddlKewajiban.SelectedValue) = DepositBEnum.TipeKewajiban.Regular) Then
            DeleteEquipmentMaster(e)
        End If
        If (CInt(ddlKewajiban.SelectedValue) = DepositBEnum.TipeKewajiban.NonReguler) Then
            DeleteSparePartMaster(e)
        End If
    End Sub

    Private Sub AddSparePartMaster(ByVal e As DataGridCommandEventArgs)
        Try
            Dim lblPartIDFooter As TextBox = CType(e.Item.FindControl("lblPartIDFooter"), TextBox)
            Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtPartCodeFooter"), TextBox)
            Dim txtQtyFooter As TextBox = CType(e.Item.FindControl("txtQtyFooter"), TextBox)

            Dim objDetail As New DepositBKewajibanDetail
            Dim _arlKewajiban As ArrayList = _sesshelper.GetSession("ArrKewajibanDetail")
            If IsNothing(_arlKewajiban) Then
                _arlKewajiban = New ArrayList
            End If
            Dim objSparePartMaster As SparePartMaster = New KTB.DNet.BusinessFacade.SparePart.SparePartMasterFacade(User).Retrieve(CInt(lblPartIDFooter.Text))

            If Not IsNothing(objSparePartMaster) Then
                For Each item As DepositBKewajibanDetail In _arlKewajiban
                    If item.SparePartMaster.PartNumber = objSparePartMaster.PartNumber Then
                        MessageBox.Show("Kode barang sudah ada dalam daftar. Silahkan update quantity.")
                        Exit Sub
                    End If
                Next
                objDetail.SparePartMaster = objSparePartMaster
                objDetail.Harga = objSparePartMaster.RetalPrice
                objDetail.Qty = CShort(txtQtyFooter.Text)
                'objDetail.Tax = (CShort(txtQtyFooter.Text) * objSparePartMaster.RetalPrice) * 0.1
                _arlKewajiban.Add(objDetail)
            End If

            _sesshelper.SetSession("ArrKewajibanDetail", _arlKewajiban)

            dgUpload.DataSource = _arlKewajiban
            dgUpload.DataBind()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub AddEquipmentMaster(ByVal e As DataGridCommandEventArgs)
        Try
            Dim lblPartIDFooter As TextBox = CType(e.Item.FindControl("lblPartIDFooter"), TextBox)
            Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtPartCodeFooter"), TextBox)
            Dim txtQtyFooter As TextBox = CType(e.Item.FindControl("txtQtyFooter"), TextBox)

            If Not IsNumeric(txtQtyFooter.Text.Trim) Then
                MessageBox.Show("Qty harus angka")
                Exit Sub
            End If

            Dim objDetail As New DepositBKewajibanDetail
            Dim _arlKewajiban As ArrayList = _sesshelper.GetSession("ArrKewajibanDetail")
            If IsNothing(_arlKewajiban) Then
                _arlKewajiban = New ArrayList
            End If
            Dim objEquipmentMaster As EquipmentMaster = New KTB.DNet.BusinessFacade.EquipmentMasterFacade(User).Retrieve(CInt(lblPartIDFooter.Text))

            If Not IsNothing(objEquipmentMaster) Then
                For Each item As DepositBKewajibanDetail In _arlKewajiban
                    If item.EquipmentMaster.EquipmentNumber = objEquipmentMaster.EquipmentNumber Then
                        MessageBox.Show("Kode barang sudah ada dalam daftar. Silahkan update quantity.")
                        Exit Sub
                    End If
                Next
                objDetail.EquipmentMaster = objEquipmentMaster
                objDetail.Harga = objEquipmentMaster.Price
                objDetail.Qty = CShort(txtQtyFooter.Text)
                'objDetail.Tax = (CShort(txtQtyFooter.Text) * objEquipmentMaster.Price) * 0.1
                _arlKewajiban.Add(objDetail)
            End If

            _sesshelper.SetSession("ArrKewajibanDetail", _arlKewajiban)

            dgUpload.DataSource = _arlKewajiban
            dgUpload.DataBind()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        Try
            Dim _arlKewajiban As ArrayList = CType(_sesshelper.GetSession("ArrKewajibanDetail"), ArrayList)
            Dim objDetail As DepositBKewajibanDetail = New DepositBKewajibanDetail
            objDetail = CType(_arlKewajiban(e.Item.ItemIndex), DepositBKewajibanDetail)

            Dim lblPartID As TextBox = CType(e.Item.FindControl("lblPartIDEdit"), TextBox)
            Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtPartCodeEdit"), TextBox)
            Dim txtQty As TextBox = CType(e.Item.FindControl("txtQtyEdit"), TextBox)

            If (CInt(ddlKewajiban.SelectedValue) = DepositBEnum.TipeKewajiban.Regular) Then
                Dim objEquipmentMaster As EquipmentMaster = New KTB.DNet.BusinessFacade.EquipmentMasterFacade(User).Retrieve(CInt(lblPartID.Text))
                If Not IsNothing(objEquipmentMaster) Then
                    objDetail.EquipmentMaster = objEquipmentMaster
                    objDetail.Harga = objEquipmentMaster.Price
                End If
            End If
            If (CInt(ddlKewajiban.SelectedValue) = DepositBEnum.TipeKewajiban.NonReguler) Then
                Dim objSparePartMaster As SparePartMaster = New KTB.DNet.BusinessFacade.SparePart.SparePartMasterFacade(User).Retrieve(CInt(lblPartID.Text))
                If Not IsNothing(objSparePartMaster) Then
                    objDetail.SparePartMaster = objSparePartMaster
                    objDetail.Harga = objSparePartMaster.RetalPrice
                End If
            End If

            objDetail.Qty = CShort(txtQty.Text)
            'objDetail.Tax = (objDetail.Qty * objDetail.Harga) * 0.1
            objDetail.ErrorMessage = ""

            _sesshelper.SetSession("ArrKewajibanDetail", _arlKewajiban)

            dgUpload.EditItemIndex = -1
            BindKewajibanDetail()
            dgUpload.ShowFooter = True
            btnSave.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DeleteSparePartMaster(ByVal e As DataGridCommandEventArgs)
        Try
            Dim _arlKewajiban As ArrayList = CType(_sesshelper.GetSession("ArrKewajibanDetail"), ArrayList)
            Dim objDetail As DepositBKewajibanDetail = New DepositBKewajibanDetail
            objDetail = CType(_arlKewajiban(e.Item.ItemIndex), DepositBKewajibanDetail)
            If objDetail.ID > 0 Then
                objDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                'Dim vReturn As Integer = New DepositBKewajibanDetailFacade(User).Update(objDetail)
            Else
                _arlKewajiban.Remove(objDetail)
            End If

            _sesshelper.SetSession("ArrKewajibanDetail", _arlKewajiban)

            dgUpload.DataSource = _arlKewajiban
            dgUpload.DataBind()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DeleteEquipmentMaster(ByVal e As DataGridCommandEventArgs)
        Try
            Dim _arlKewajiban As ArrayList = CType(_sesshelper.GetSession("ArrKewajibanDetail"), ArrayList)
            Dim objDetail As DepositBKewajibanDetail = New DepositBKewajibanDetail
            If e.Item.ItemIndex <= dgUpload.Items.Count - 1 Then
                objDetail = CType(_arlKewajiban(e.Item.ItemIndex), DepositBKewajibanDetail)
                If objDetail.ID > 0 Then
                    objDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                    'Dim vReturn As Integer = New DepositBKewajibanDetailFacade(User).Update(objDetail)
                Else
                    _arlKewajiban.Remove(objDetail)
                End If
                _sesshelper.SetSession("ArrKewajibanDetail", _arlKewajiban)

                dgUpload.DataSource = _arlKewajiban
                dgUpload.DataBind()
            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

    Private Sub LinkDownload_Click(sender As Object, e As EventArgs) Handles LinkDownload.Click
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\UploadKewajiban.xlsx")
    End Sub
End Class