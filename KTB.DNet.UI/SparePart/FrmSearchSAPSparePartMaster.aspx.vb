#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.SAP

#End Region

Public Class FrmSearchSAPSparePartMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "
#Region "Custom Variable Declaration"
    Private _arlPartMaster As ArrayList = New ArrayList
    Private _sesshelper As SessionHelper = New SessionHelper
    Private _connectionString As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionString")
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Private _MaxItem As Integer = CType(KTB.DNet.Lib.WebConfig.GetValue("SAPMaxItems"), Integer)

#End Region

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgPODetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CheckViewPrivilege()
        If Not IsPostBack Then
            ViewState("vsItemNo") = "0"
            _sesshelper.SetSession("sessPartMaster", _arlPartMaster)
            DataGridInitialization(True)
            dtgPODetail.DataSource = _arlPartMaster
            dtgPODetail.DataBind()


        End If
    End Sub

    Private Sub CheckViewPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewSparePartStock_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER BARANG - Lihat Stok")
        End If
    End Sub
    Private Sub DataGridInitialization(ByVal bValue As Boolean)
        dtgPODetail.ShowFooter = bValue
        dtgPODetail.Columns(9).Visible = bValue
        dtgPODetail.Columns(10).Visible = Not bValue
    End Sub
    Private Sub dtgPODetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPODetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetDtgPODetailItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetDtgPODetailItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgPODetailItemEdit(e)
        End If
    End Sub

    Private Sub SetDtgPODetailItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblFPopUpSparePart"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx", "", 500, 760, "SparePart")
        Dim lbtnAdd As LinkButton = CType(e.Item.Cells(1).FindControl("lbtnAdd"), LinkButton)
        'If Not lbtnAdd Is Nothing Then
        '    lbtnAdd.Attributes("onclick") = "disableBackButton();"
        'End If
    End Sub

    Private Sub SetDtgPODetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)
        ViewState("vsItemNo") = e.Item.ItemIndex + 1

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If btnCancel.Text = "Baru" Then
            Dim objSparePartMaster As SparePartMaster = New SparePartMaster
            objSparePartMaster = CType(CType(dtgPODetail.DataSource, ArrayList)(e.Item.ItemIndex), SparePartMaster)
            Dim lblView As Label = CType(e.Item.FindControl("lblView"), Label)
            If objSparePartMaster.PartCode = "O" OrElse objSparePartMaster.PartCode = "N" Then
                lblView.Visible = True
                lblView.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                      "../SparePart/FrmSUBPartMasterSAP.aspx?NoSparePartAlt=" & IIf(objSparePartMaster.PartCode = "O", objSparePartMaster.AltPartNumber, objSparePartMaster.PartNumber) + "", "", 600, 800, "null")
            Else
                lblView.Visible = False
            End If
        End If
    End Sub

    Private Sub SetDtgPODetailItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Clear()
        e.Item.Cells(0).Controls.Add(lNum)
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblEPopUpSparePart"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx", "", 710, 700, "SparePart")
    End Sub

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlPartMaster As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If Not IsNothing(arlPartMaster) Then
            For Each PartItem As SparePartMaster In arlPartMaster
                If PartItem.PartNumber.Trim.ToUpper = partNumber.Trim.ToUpper Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlPartMaster As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer
        Dim bResult As Boolean = False
        If Not arlPartMaster Is Nothing Then
            For i = 0 To arlPartMaster.Count - 1
                If CType(arlPartMaster(i), SparePartMaster).PartNumber.Trim = partNumber.Trim AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function


    Private Function GetMaxOrder(ByVal partNumber As Integer) As Integer
        Dim _SparepartMaxOrderFacade As SparePartMaxOrderFacade = New SparePartMaxOrderFacade(User)
        Dim _sp As SparepartMaxOrder = _SparepartMaxOrderFacade.RetrieveBySPID(partNumber)
        If _sp.ID > 0 Then
            Return _sp.MaxRequest
        Else
            Return 0
        End If
    End Function

    Private Function GetStockFromAppSetting() As Integer
        Dim objAppSetting As AppSetting = New AppSettingFacade(User).Retrieve("SAPMaxMaterial")
        Dim nilai As Integer = 8
        If Not objAppSetting Is Nothing Then
            Try
                nilai = CInt(objAppSetting.Nilai)
            Catch ex As Exception
                nilai = 8
            End Try
        End If
        Return nilai
    End Function


    Private Sub dtgPODetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPODetail.ItemCommand
        Dim strIndicator As String = ""

        Try
            _arlPartMaster = CType(Session("sessPartMaster"), ArrayList)
            'Dim maxStockNonSetIteam As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("SAPMaxMaterial"))
            Dim maxStockNonSetIteam As Integer = GetStockFromAppSetting()
            strIndicator = "Before Select Case"
            Select Case e.CommandName
                Case "add" 'Insert New item to datagrid
                    strIndicator = "Case Add"
                    If CType(ViewState("vsItemNo"), Integer) >= _MaxItem Then
                        MessageBox.Show("Maaf: Maksimal item yang diijinkan hanya 20")
                    Else
                        Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtFPartNumber"), TextBox)

                        Dim txtQty As TextBox = CType(e.Item.FindControl("txtFQTY"), TextBox)
                        Dim lblFAltPartNumber As Label = CType(e.Item.FindControl("lblFAltPartNumber"), Label)
                        Dim objPart As SparePartMaster
                        Dim order As Integer

                        If IsNothing(txtPartNumber) OrElse txtPartNumber.Text = String.Empty Then
                            MessageBox.Show("No.Part masih kosong")
                            Return
                        Else
                            objPart = New SparePartMasterFacade(User).Retrieve(txtPartNumber.Text.Replace("'", ""))
                            lblFAltPartNumber.Text = objPart.AltPartNumber

                            'Check ProductCategory
                            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")


                            If IsNothing(objPart.ID) OrElse IsNothing(objPart.ProductCategory) Then
                                MessageBox.Show("Sparepart tidak ada untuk produk " + companyCode)
                                Return
                            Else
                                If objPart.ProductCategory.Code <> companyCode Then
                                    MessageBox.Show("Sparepart tidak ada untuk produk " + companyCode)
                                    Return
                                End If
                            End If
                            'End Check ProductCategory
                        End If

                        If IsNothing(txtQty) OrElse txtQty.Text = String.Empty Then
                            MessageBox.Show("Quantity tidak boleh kosong")
                            Return
                        Else
                            Try
                                order = CInt(txtQty.Text.Trim)
                            Catch ex As Exception
                                MessageBox.Show("Quantity harus berupa angka")
                                Return
                            End Try
                        End If

                        strIndicator = "Case Add-1"

                        Dim maxOrder = GetMaxOrder(objPart.ID)
                        If maxOrder = 0 Then
                            maxOrder = maxStockNonSetIteam
                        End If
                        If order > maxOrder Then
                            MessageBox.Show("Maksimum Qty untuk part : " & txtPartNumber.Text.Trim & " adalah : " & maxOrder)
                            Return
                        End If
                        If IsNothing(txtQty) OrElse txtQty.Text = String.Empty Then
                            objPart.MaxStock = 0
                        Else
                            objPart.MaxStock = CInt(txtQty.Text.Trim)
                        End If
                        strIndicator = "Case Add-2"
                        If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                            If Not PartIsExist(objPart.PartNumber, _arlPartMaster) Then
                                If IsNothing(_arlPartMaster) Then 'ckaka
                                    _arlPartMaster = New ArrayList
                                End If
                                _arlPartMaster.Add(objPart)
                            Else
                                MessageBox.Show(SR.DataIsExist("Spare Part"))
                            End If
                        Else
                            MessageBox.Show(SR.DataNotFound("Spare Part"))
                        End If
                    End If
                Case "edit" 'Edit mode activated
                        strIndicator = "Case Edit"
                        dtgPODetail.ShowFooter = False
                        dtgPODetail.EditItemIndex = e.Item.ItemIndex
                Case "delete" 'Delete this datagrid item 
                        strIndicator = "Case Delete"
                        _arlPartMaster.RemoveAt(e.Item.ItemIndex)

                Case "save" 'Update this datagrid item                 
                        Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtEPartNumber"), TextBox)
                        Dim txtMaxQty As TextBox = CType(e.Item.FindControl("txtEQty"), TextBox)

                        strIndicator = "Case Save"
                        Dim objPart As SparePartMaster
                        If IsNothing(txtPartNumber) OrElse txtPartNumber.Text = String.Empty Then
                            MessageBox.Show("No.Part masih kosong")
                            Return
                        Else
                            Dim objPartNew As SparePartMaster = New SparePartMasterFacade(User).Retrieve(txtPartNumber.Text.Replace("'", ""))
                            strIndicator = "Case Save-1"
                            If Not (IsNothing(objPartNew) Or objPartNew.ID = 0) Then
                                Dim maxOrder = GetMaxOrder(objPartNew.ID)
                                Dim _odr As Integer
                                If maxOrder = 0 Then
                                    maxOrder = maxStockNonSetIteam
                                End If
                                Try
                                    _odr = CInt(txtMaxQty.Text.Trim)
                                Catch ex As Exception
                                    MessageBox.Show("Quantity order tidak valid")
                                    Return
                                End Try
                                If _odr > maxOrder Then
                                    MessageBox.Show("Maksimum order untuk part : " & txtPartNumber.Text.Trim & " adalah : " & maxOrder)
                                    Return
                                End If
                                strIndicator = "Case Save-2"
                                If Not PartIsExist(objPartNew.PartNumber, _arlPartMaster, e.Item.ItemIndex) Then
                                    strIndicator = "Case Save-3"
                                    objPart = CType(_arlPartMaster(e.Item.ItemIndex), SparePartMaster)
                                    strIndicator = "Case Save-4"
                                    objPart.ID = objPartNew.ID
                                    objPart.PartNumber = objPartNew.PartNumber
                                    objPart.PartName = objPartNew.PartName
                                    objPart.PartCode = objPartNew.PartCode
                                    objPart.ModelCode = objPartNew.ModelCode
                                    objPart.RetalPrice = objPartNew.RetalPrice
                                    objPart.MaxStock = txtMaxQty.Text
                                    objPart.AltPartNumber = objPartNew.AltPartNumber
                                    dtgPODetail.EditItemIndex = -1
                                    dtgPODetail.ShowFooter = True
                                Else
                                    MessageBox.Show(SR.DataIsExist("Spare Part"))
                                End If
                            Else
                                MessageBox.Show(SR.DataNotFound("Spare Part"))
                            End If
                        End If


                Case "cancel" 'Cancel Update this datagrid item 
                        strIndicator = "Case Cancel"
                        dtgPODetail.EditItemIndex = -1
                        dtgPODetail.ShowFooter = True
            End Select
            strIndicator = "End Select Case"
            _sesshelper.SetSession("sessPartMaster", _arlPartMaster)
            BindPODetail()
            strIndicator = "After ReBinding"
        Catch ex As Exception
            Response.Write("<h2>Error: Mohon capture pesan error ini dan kirimkan ke Admin D-Net.</h2><br>Error Message=" & strIndicator & "<br>Original Error=" & ex.Message)
            'MessageBox.Show("Error: Mohon capture pesan error ini dan kirimkan ke Admin D-Net. Error Message=" & strIndicator)
        End Try
    End Sub

    Private Sub BindPODetail()
        _arlPartMaster = CType(Session("sessPartMaster"), ArrayList)
        dtgPODetail.DataSource = _arlPartMaster
        dtgPODetail.DataBind()
    End Sub

    Private Function PopulateSparePartMasterList(ByVal oPartMasterSAP As ZSPST0028_01) As SparePartMaster
        Dim oPartMaster As SparePartMaster = New SparePartMaster
        oPartMaster.PartNumber = oPartMasterSAP.MATNR
        oPartMaster.PartName = oPartMasterSAP.MAKTX
        oPartMaster.PartCode = oPartMasterSAP.PCODE
        oPartMaster.ModelCode = oPartMasterSAP.MATKL
        oPartMaster.AltPartNumber = oPartMasterSAP.SUBNR

        oPartMaster.Pesan = GetBlockMaterial(oPartMasterSAP.NORMT)
        If oPartMaster.Pesan = String.Empty Then
            oPartMaster.StockSAP = CType(oPartMasterSAP.STOCK, String)
            oPartMaster.RetalPrice = CType(oPartMasterSAP.RTLPR, Decimal)
        Else
            oPartMaster.StockSAP = oPartMaster.Pesan
            oPartMaster.RetalPrice = 0
        End If
        oPartMaster.MaxStock = oPartMasterSAP.RQQTY

        Return oPartMaster
    End Function

    Private Function GetBlockMaterial(ByVal code As String) As String
        Dim _SettingBlockMaterialFacade As SettingBlockMaterialFacade = New SettingBlockMaterialFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SettingBlockMaterial), "Code", MatchType.Exact, code.ToUpper))
        Dim pesanList As ArrayList = _SettingBlockMaterialFacade.Retrieve(criterias)
        If pesanList.Count > 0 Then
            Return CType(pesanList(0), SettingBlockMaterial).Description
        Else
            Return String.Empty
        End If
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If btnCancel.Text = "Cari" Then

            _arlPartMaster = CType(Session("sessPartMaster"), ArrayList)
            If _arlPartMaster.Count < 1 Then
                MessageBox.Show("Tidak ada Material yang dipilih.")
                Return
            End If
            Try
                Dim oSAPDnet As SAPDNet = New SAPDNet(_connectionString)

                '--Check Material on SAP 
                'Dim arlSAPResult As ArrayList = oSAPDnet.CheckMaterial(_arlPartMaster)
                Dim arrMainPart As ArrayList = New ArrayList()
                Dim arrSubtitutionPart As ArrayList = New ArrayList()
                Dim strReturn As String = oSAPDnet.GetMaterial(_arlPartMaster, arrMainPart, arrSubtitutionPart)
                If strReturn = String.Empty Then 'If arlSAPResult.Count > 0 Then
                    '--Store Alt Spare Part master collection to session
                    '_sesshelper.SetSession("sessSAP_AltPartMaster", arlSAPResult(1))
                    _sesshelper.SetSession("sessSAP_AltPartMaster", arrSubtitutionPart)
                    '--Reset arraylist of materials
                    _arlPartMaster.Clear()
                    '--
                    'For Each oPartMasterSAP As ZSPST0028_01 In arlSAPResult(0)
                    For Each oPartMasterSAP As ZSPST0028_01 In arrMainPart
                        Dim oPartMaster As SparePartMaster = PopulateSparePartMasterList(oPartMasterSAP)
                        _arlPartMaster.Add(oPartMaster)
                    Next
                    '-- Store the result data to session
                    _sesshelper.SetSession("sessPartMaster", _arlPartMaster)
                    DataGridInitialization(False)
                    btnCancel.Text = "Baru"
                Else
                    MessageBox.Show("Pengecekan data di SAP GAGAL")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message.Trim)
            End Try
        Else
            _arlPartMaster.Clear()
            ViewState("vsItemNo") = "0"
            _sesshelper.SetSession("sessPartMaster", _arlPartMaster)
            DataGridInitialization(True)
            btnCancel.Text = "Cari"
        End If
        BindPODetail()
    End Sub
End Class
