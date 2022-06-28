#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmFieldFixRequestPart
    Inherits System.Web.UI.Page

#Region "Variable"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private objSPFHeader As New SparePartForecastHeader
    Private objSPFHeaderFacade As New SparePartForecastHeaderFacade(User)
    Private objSPFDeatil As New SparePartForecastDetail
    Private objSPFDeatilFacade As New SparePartForecastDetailFacade(User)
    Private _arlPartMaster As ArrayList = New ArrayList
    Private arlStockControl As ArrayList
    Private sessCriterias As String = "FrmRequestPart.sessCriterias"
    Private _EditTable As Boolean = False

    Private Mode As enumMode.Mode

    Dim rilisItem As SparePartForecastMaster

#End Region

#Region "Custom Method"
    Private Sub InitiateAuthorization()
        Dim oD As Dealer = CType(Session("DEALER"), Dealer)

        'If Not SecurityProvider.Authorize(Context.User, SR.FieldFix_StockControl_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Managemen Stock Control")
        'End If
       

        If oD.Title <> EnumDealerTittle.DealerTittle.KTB Then
            _EditTable = False
        Else
            _EditTable = SecurityProvider.Authorize(Context.User, SR.Recall_InputCategory_Privilege)
        End If

        lblDealerCode.Text = oD.DealerCode
        lblDealerName.Text = oD.DealerName
        lblAlamat.Text = oD.Address
        lblKodePos.Text = oD.ZipCode
        'Dim dtgLast As Integer = dgMasterPart.Columns.Count - 1
        'dgMasterPart.Columns(dtgLast).Visible = False

        'btnSimpan.Visible = _EditTable
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        'If txtPartNo.Text.Trim().Length > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "SparePartMaster.PartNumber", MatchType.Partial, txtPartNo.Text().Trim()))
        'End If
        'If txtRecallRegNo.Text().Trim().Length > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "NoRecallCategory", MatchType.Partial, txtRecallRegNo.Text().Trim()))
        'End If
        'If txtNoBuletin.Text().Trim().Length > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastMaster), "NoBulletinService", MatchType.Partial, txtNoBuletin.Text().Trim()))
        'End If
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim TotalRow As Integer = 0
        If indexPage >= 0 Then
            arlStockControl = New SparePartForecastHeaderFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite), indexPage + 1, dgMasterPart.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dgMasterPart.DataSource = arlStockControl

            dgMasterPart.VirtualItemCount = TotalRow
            dgMasterPart.DataBind()
        End If
    End Sub

    Private Sub DataGridInitialization(ByVal bValue As Boolean)
        dgMasterPart.ShowFooter = bValue
    End Sub

   
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            Dim id As String = Request.QueryString("ID")
            Dim mode As String = Request.QueryString("MODE")
            Dim status As String = Request.QueryString("STATUS")
            Dim pono As String = Request.QueryString("PONO")
            Dim dtstart As String = Request.QueryString("DTSTART")
            Dim dtend As String = Request.QueryString("DTEND")
            hdID.Value = 0
            If Not String.IsNullorEmpty(id) And Not String.IsNullorEmpty(mode) Then
                ViewState("MODE") = mode
                hdID.Value = id
                HdPo.Value = pono
                HDStatus.Value = status
                HdDtStart.Value = dtstart
                HdDtEnd.Value = dtend
                LoadData(id)
            Else
                _sessHelper.SetSession("addNewParts", _arlPartMaster)
                DataGridInitialization(True)
                dgMasterPart.DataSource = _arlPartMaster
                dgMasterPart.DataBind()
            End If

        End If
    End Sub

    Private Sub LoadData(ByVal id As Integer)
        'Dim objDetail As SparePartForecastDetail = New SparePartForecastDetailFacade(User).Retrieve(id)
        Dim objHeader As SparePartForecastHeader = New SparePartForecastHeaderFacade(User).Retrieve(id)
        If Not IsNothing(objHeader) AndAlso objHeader.ID.ToString() <> "" Then
            hdID.Value = objHeader.ID
            HdDate.Value = objHeader.PoDate
            HDStatus.Value = objHeader.Status
            lblNoPO.Text = objHeader.PoNumber

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastDetail), "SparePartForecastHeader.ID", MatchType.Exact, objHeader.ID))
            Dim arrSFDetail As ArrayList = New SparePartForecastDetailFacade(User).Retrieve(criterias)
            If Not IsNothing(arrSFDetail) AndAlso arrSFDetail.Count > 0 Then
                dgMasterPart.DataSource = arrSFDetail
                dgMasterPart.CurrentPageIndex = 0
                dgMasterPart.ShowFooter = True
                dgMasterPart.DataBind()
                btnSimpan.Enabled = True
                btnValidasi.Enabled = True
                btnKembali.Visible = True
                _sessHelper.SetSession("DataParts", arrSFDetail)
            End If
        Else
            _sessHelper.SetSession("addNewParts", _arlPartMaster)
            DataGridInitialization(True)
            dgMasterPart.DataSource = _arlPartMaster
            dgMasterPart.DataBind()
        End If

    End Sub

    Private Sub Search()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartForecastHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)
        _sessHelper.SetSession(Me.sessCriterias, criterias)
        dgMasterPart.CurrentPageIndex = 0
        BindDatagrid(dgMasterPart.CurrentPageIndex)
        btnSimpan.Enabled = True
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim result As Integer
        Dim _arrAddSPFDetail As ArrayList = New ArrayList
        Dim objSPFHeader As SparePartForecastHeader = New SparePartForecastHeader
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)

        'check datagrid
        Dim objPartForecastMaster As SparePartForecastMaster
        LoopDgrid(objPartForecastMaster, "Add", Nothing)
        _arrAddSPFDetail = _sessHelper.GetSession("addNewParts")
        objSPFHeader.Dealer = objDealer

        If hdID.Value <> "" AndAlso hdID.Value <> 0 Then
            If _arrAddSPFDetail.Count > 0 Then
                objSPFHeader.ID = hdID.Value
                objSPFHeader.PoNumber = lblNoPO.Text()
                objSPFHeader.PoDate = HdDate.Value
                objSPFHeader.Status = HDStatus.Value
                result = objSPFHeaderFacade.UpdateTransaction(objSPFHeader, _arrAddSPFDetail)
            Else
                MessageBox.Show("Data Sparepart belum di input")
                Return
            End If
        Else
            If _arrAddSPFDetail.Count > 0 Then
                objSPFHeader.Status = EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Baru
                result = objSPFHeaderFacade.InsertTransaction(objSPFHeader, _arrAddSPFDetail)
            Else
                MessageBox.Show("Data Sparepart belum di input")
                Return
            End If
        End If

        If result = -1 Then
            MessageBox.Show("Simpan Gagal")
        Else
            hdID.Value = result
            lblNoPO.Text = objSPFHeaderFacade.Retrieve(result).PoNumber.Trim
            HdDate.Value = objSPFHeaderFacade.Retrieve(result).PoDate
            HDStatus.Value = objSPFHeaderFacade.Retrieve(result).Status
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartForecastDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartForecastDetail), "SparePartForecastHeader.ID", MatchType.Exact, result))
            Dim objDetail As ArrayList = New SparePartForecastDetailFacade(User).Retrieve(criterias)
            dgMasterPart.DataSource = objDetail
            dgMasterPart.DataBind()
            MessageBox.Show("Simpan Sukses")
            'btnSimpan.Enabled = False
            btnValidasi.Enabled = True
        End If

        'result = objSPFHeaderFacade.InsertTransaction(objSPFHeader, _arrAddSPFDetail)

    End Sub

    Private Sub dgMasterPart_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgMasterPart.ItemCommand
        Dim _arrAddParts As ArrayList = New ArrayList
        Dim _arrPartsCode As ArrayList = CType(_sessHelper.GetSession("DataParts"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrPartsCode = CType(_sessHelper.GetSession("addNewParts"), ArrayList)
        End If
        If (e.CommandName = "Add") Then
            Dim objPartForecastMaster As SparePartForecastMaster
            Dim txtPartsCode As TextBox = CType(e.Item.FindControl("txtKodePartsFooter"), TextBox)
            Dim txtQtyFooter As TextBox = CType(e.Item.FindControl("txtQtyFooter"), TextBox)

            If IsNothing(txtPartsCode) OrElse txtPartsCode.Text = String.Empty Then
                MessageBox.Show("Nomor Part masih kosong")
                Return
            ElseIf IsNothing(txtQtyFooter) OrElse txtQtyFooter.Text = String.Empty OrElse CType(txtQtyFooter.Text.Trim, Integer) = 0 Then
                MessageBox.Show("Quantity masih kosong atau masih nol")
                Return
            Else
                Dim ForecastMaster As SparePartForecastMaster = New SparePartForecastMasterFacade(User).Retrieve(txtPartsCode.Text.Trim())
                If Not IsNothing(ForecastMaster) Then
                    If ForecastMaster.Stock < CType(txtQtyFooter.Text.Trim, Integer) Then
                        MessageBox.Show("Jumlah Order melebihi Stock (" & ForecastMaster.Stock & ")")
                        Return
                    ElseIf ForecastMaster.MaxOrder < CType(txtQtyFooter.Text.Trim, Integer) Then
                        MessageBox.Show("Jumlah Order melebihi Maksimal Order (" & ForecastMaster.MaxOrder & ")")
                        Return
                    End If
                Else
                    MessageBox.Show("Part Number tidak terdaftar")
                    Return
                End If
            End If
            LoopDgrid(objPartForecastMaster, "Add", Nothing)
            
        ElseIf e.CommandName = "Edit" Then
            dgMasterPart.EditItemIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Dim objPartForecastMaster As SparePartForecastMaster
            LoopDgrid(objPartForecastMaster, "Add", Nothing)
            _arrPartsCode = _sessHelper.GetSession("addNewParts")
            Dim deletedPartDetail As SparePartForecastDetail = CType(_arrPartsCode(e.Item.ItemIndex), SparePartForecastDetail)
            If Mode = enumMode.Mode.NewItemMode Then
                _arrPartsCode.RemoveAt(e.Item.ItemIndex)
                If deletedPartDetail.ID > 0 Then
                    objSPFDeatilFacade.DeleteFromDB(deletedPartDetail)
                End If
                'ElseIf Mode = enumMode.Mode.EditMode Then
                '    'Dim deletedPartDetail As SparePartForecastDetail = CType(_arrPartsCode(e.Item.ItemIndex), SparePartForecastDetail)
                '    If deletedPartDetail.ID > 0 Then
                '        Dim deletedArrLst As ArrayList
                '        deletedArrLst = CType(_sessHelper.GetSession("DELETEDPARTS"), ArrayList)
                '        deletedArrLst.Add(deletedPartDetail)
                '        _sessHelper.SetSession("DELETEDPARTS", deletedArrLst)
                '    End If
                '    _arrPartsCode.RemoveAt(e.Item.ItemIndex)
            End If
        End If
        BindPartDetail()
    End Sub

    Private Function LoopDgrid(ByVal objSparePartMaster As SparePartForecastMaster, ByVal fromFunc As String, ByVal index As Integer)
        Dim _arrNewParts As ArrayList = New ArrayList
        Dim _arrNewPartsTemp As ArrayList = New ArrayList
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrNewPartsTemp = CType(_sessHelper.GetSession("addNewParts"), ArrayList)
            _sessHelper.RemoveSession("addNewParts")
            _sessHelper.SetSession("addNewParts", New ArrayList)
            _arrNewParts = CType(_sessHelper.GetSession("addNewParts"), ArrayList)
        Else
            _arrNewPartsTemp = CType(_sessHelper.GetSession("DataParts"), ArrayList)
            _sessHelper.RemoveSession("DataParts")
            _sessHelper.SetSession("DataParts", New ArrayList)
            _arrNewParts = CType(_sessHelper.GetSession("DataParts"), ArrayList)
        End If

        Dim i = 0
        If fromFunc = "Add" Then
            Dim e2 As Control
            For Each e2 In dgMasterPart.Controls
                For Each ct As Control In e2.Controls
                    If TypeOf ct Is System.Web.UI.WebControls.DataGridItem Then
                        Dim di As System.Web.UI.WebControls.DataGridItem = CType(ct, System.Web.UI.WebControls.DataGridItem)
                        Dim KodePart As String
                        Dim lblKodeParts As Label
                        Dim txtKodePartsFooter As TextBox
                        Dim txtQty As TextBox
                        Dim lblID As Label
                        Dim lblDate As Label
                        Dim lblStatus As Label
                        Dim lblNoBulletin As Label

                        If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem OrElse di.ItemType = ListItemType.Footer Then
                            If (di.ItemType = ListItemType.Footer) Then
                                txtKodePartsFooter = CType(di.FindControl("txtKodePartsFooter"), TextBox)
                                KodePart = txtKodePartsFooter.Text
                                txtQty = CType(di.FindControl("txtQtyFooter"), TextBox)
                                lblNoBulletin = CType(di.FindControl("lblNoBulletinFooter"), Label)
                                lblID = CType(di.FindControl("lblIDFooter"), Label)
                                lblDate = CType(di.FindControl("lblDateFooter"), Label)
                                lblStatus = CType(di.FindControl("lblStatusFooter"), Label)
                            ElseIf di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                lblKodeParts = CType(di.FindControl("lblKodeParts"), Label)
                                KodePart = lblKodeParts.Text
                                txtQty = CType(di.FindControl("txtQtyItem"), TextBox)
                                lblID = CType(di.FindControl("lblIDItem"), Label)
                                lblDate = CType(di.FindControl("lblDateItem"), Label)
                                lblStatus = CType(di.FindControl("lblStatusItem"), Label)
                                lblNoBulletin = CType(di.FindControl("lblNoBulletin"), Label)
                            End If

                            If KodePart.Trim <> "" Then
                                objSparePartMaster = New SparePartForecastMasterFacade(User).Retrieve(KodePart.Trim())
                                'validasi stock dan max order
                                If Not IsNothing(objSparePartMaster) OrElse objSparePartMaster.ID <> 0 Then
                                    If objSparePartMaster.Stock < txtQty.Text Then
                                        MessageBox.Show("Qty Request melebihi Stok (" & objSparePartMaster.Stock & ")")
                                        Exit Function
                                    End If
                                    If objSparePartMaster.MaxOrder < txtQty.Text Then
                                        MessageBox.Show("Qty Request melebihi batas Max Order (" & objSparePartMaster.MaxOrder & ")")
                                        Exit Function
                                    End If
                                    If PartsCodeIsExist(objSparePartMaster.SparePartMaster.ID, _arrNewParts) Then
                                        MessageBox.Show(SR.DataIsExist("Kode Part"))
                                        Exit Function
                                    End If
                                    If index = 2 Then
                                        lblStatus.Text = EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Validasi
                                    End If
                                    _arrNewParts = insertSessionParts(lblID.Text, lblDate.Text, lblStatus.Text, objSparePartMaster, txtQty.Text, _arrNewParts)
                                Else
                                    MessageBox.Show(SR.DataNotFound("Kode Part"))
                                    Exit Function
                                End If
                            End If

                        End If
                    End If
                Next
            Next
            _sessHelper.SetSession("addNewParts", _arrNewParts)
        End If

        If fromFunc = "Delete" Then
            If Mode = enumMode.Mode.NewItemMode Then
                _arrNewParts.RemoveAt(index)
            ElseIf Mode = enumMode.Mode.EditMode Then
                Dim oNewParts As SparePartForecastDetail = CType(_arrNewParts(index), SparePartForecastDetail)
                If oNewParts.ID > 0 Then
                    Dim deletedArrLst As ArrayList
                    deletedArrLst = CType(_sessHelper.GetSession("DELETEDPARTS"), ArrayList)
                    deletedArrLst.Add(oNewParts)
                    _sessHelper.SetSession("DELETEDPARTS", deletedArrLst)
                End If
                _arrNewParts.RemoveAt(index)
            End If

        End If

        Return True
    End Function

    Private Function insertSessionParts(ByVal intID As String, ByVal intDate As String, ByVal intStatus As String, ByVal objSparePartMaster As SparePartForecastMaster, _
                                    ByVal quantity As String, ByVal _arrNewParts As ArrayList) As ArrayList

        Dim objForecastDetail As SparePartForecastDetail = New SparePartForecastDetail
        intID = IIf(intID.Trim = "", 0, intID)
        If intDate.Trim <> "" Then
            objForecastDetail.RequestDate = intDate
        End If
        If intStatus.Trim <> "" Then
            objForecastDetail.Status = intStatus
        Else
            objForecastDetail.Status = EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Baru
        End If
        objForecastDetail.ID = intID
        objForecastDetail.SparePartForecastMaster = objSparePartMaster
        objForecastDetail.RequestQty = quantity

        _arrNewParts.Add(objForecastDetail)

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            _sessHelper.SetSession("addNewParts", _arrNewParts)
        Else
            _sessHelper.SetSession("DataParts", _arrNewParts)
        End If

        Return _arrNewParts
    End Function

    Private Function PartsCodeIsExist(ByVal SparePartID As Integer, ByVal arrPartsCodeCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If arrPartsCodeCollection.Count > 0 Then
            For Each _DetailPartsCode As SparePartForecastDetail In arrPartsCodeCollection
                If _DetailPartsCode.SparePartForecastMaster.SparePartMaster.ID = SparePartID Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function

    Private Sub BindPartDetail()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            dgMasterPart.DataSource = CType(_sessHelper.GetSession("addNewParts"), ArrayList)
            If CType(_sessHelper.GetSession("addNewParts"), ArrayList).Count = 0 Then
                btnSimpan.Enabled = False
                btnValidasi.Enabled = False
            Else
                btnSimpan.Enabled = True
            End If
            dgMasterPart.DataBind()
        Else
            dgMasterPart.DataSource = CType(_sessHelper.GetSession("DataParts"), ArrayList)
            dgMasterPart.DataBind()
            btnSimpan.Enabled = True
        End If

    End Sub

    Private Sub dgMasterPart_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgMasterPart.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetdgMasterPartItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetdgMasterPartItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetdgMasterPartItemEdit(e)
        End If
    End Sub

    Private Sub SetdgMasterPartItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim RowData As SparePartForecastDetail = CType(e.Item.DataItem, SparePartForecastDetail)
        If Not IsNothing(RowData) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgMasterPart.CurrentPageIndex * dgMasterPart.PageSize)
            CType(e.Item.FindControl("lblIDItem"), Label).Text = RowData.ID
            CType(e.Item.FindControl("lblDateItem"), Label).Text = RowData.RequestDate
            CType(e.Item.FindControl("lblStatusItem"), Label).Text = RowData.Status
            CType(e.Item.FindControl("lblKodeParts"), Label).Text = RowData.SparePartForecastMaster.SparePartMaster.PartNumber
            CType(e.Item.FindControl("lblNamaParts"), Label).Text = RowData.SparePartForecastMaster.SparePartMaster.PartName
            CType(e.Item.FindControl("lblNoBulletin"), Label).Text = RowData.SparePartForecastMaster.NoBulletinService
            CType(e.Item.FindControl("txtQtyItem"), TextBox).Text = RowData.RequestQty
            Dim strMsg As String = "Apa anda yakin akan menghapus " & CType(e.Item.FindControl("lblNamaParts"), Label).Text & "?"
            CType(e.Item.FindControl("lnkbtnDeleteParts"), LinkButton).Attributes.Add("OnClick", "return confirm('" & strMsg & "');")

            'CType(e.Item.FindControl("lnkbtnDeleteParts"), LinkButton).Attributes.Add("OnClick", "return confirm('Apa anda yakin akan menghapus  namapart??');")
            'CType(e.Item.FindControl("txtQtyItem"), TextBox).Visible = False
            'CType(e.Item.FindControl("lblQtyItem"), Label).Text = RowData.RequestQty
            'CType(e.Item.FindControl("lblQtyItem"), Label).Visible = True
        End If

    End Sub

    Private Sub SetdgMasterPartItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim RowData As SparePartForecastDetail = CType(e.Item.DataItem, SparePartForecastDetail)
        If Not IsNothing(RowData) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgMasterPart.CurrentPageIndex * dgMasterPart.PageSize)
            CType(e.Item.FindControl("lblIDItem"), Label).Text = RowData.ID
            CType(e.Item.FindControl("lblDateItem"), Label).Text = RowData.RequestDate
            CType(e.Item.FindControl("lblStatusItem"), Label).Text = RowData.Status
            CType(e.Item.FindControl("txtKodePartsFooter"), TextBox).Text = RowData.SparePartForecastMaster.SparePartMaster.PartNumber
            CType(e.Item.FindControl("lblNamaPartsFooter"), Label).Text = RowData.SparePartForecastMaster.SparePartMaster.PartName
            CType(e.Item.FindControl("lblNoBulletinFooter"), Label).Text = RowData.SparePartForecastMaster.NoBulletinService
            CType(e.Item.FindControl("txtQtyFooter"), TextBox).Text = RowData.RequestQty
            'CType(e.Item.FindControl("txtQtyFooter"), TextBox).Visible = False
            'CType(e.Item.FindControl("lblQtyItem"), Label).Text = RowData.RequestQty
            'CType(e.Item.FindControl("lblQtyItem"), TextBox).Visible = True
        End If
    End Sub

    Private Sub SetdgMasterPartItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim RowData As SparePartForecastDetail = CType(e.Item.DataItem, SparePartForecastDetail)
        If Not IsNothing(RowData) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgMasterPart.CurrentPageIndex * dgMasterPart.PageSize)
            CType(e.Item.FindControl("lblKodeParts"), Label).Text = RowData.SparePartForecastMaster.SparePartMaster.PartNumber
            CType(e.Item.FindControl("lblNamaParts"), Label).Text = RowData.SparePartForecastMaster.SparePartMaster.PartName
            CType(e.Item.FindControl("lblNoBulletin"), Label).Text = RowData.SparePartForecastMaster.NoBulletinService
            CType(e.Item.FindControl("txtQtyItem"), TextBox).Text = RowData.RequestQty
            'CType(e.Item.FindControl("txtQtyItem"), TextBox).Visible = True
            'CType(e.Item.FindControl("lblQtyItem"), Label).Text = RowData.RequestQty
            'CType(e.Item.FindControl("lblQtyItem"), Label).Visible = False

            'CType(e.Item.FindControl("lnkbtnEdit"), LinkButton).Visible = False
            'CType(e.Item.FindControl("lnkbtnDeleteParts"), LinkButton).Visible = False
            'CType(e.Item.FindControl("lnkbtnAddEditParts"), LinkButton).Visible = True
        End If
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

    Private Sub dgMasterPart_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgMasterPart.PageIndexChanged
        'dgMasterPart.SelectedIndex = -1
        'dgMasterPart.CurrentPageIndex = e.NewPageIndex
        'BindDatagrid(dgMasterPart.CurrentPageIndex)
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        'Response.Redirect("../Service/FrmFieldFixRequestPartList.aspx?MODE=New")
        Response.Redirect("../Service/FrmFieldFixRequestPartList.aspx?MODE=NEW&ID=" & hdID.Value & "&PONO=" & HdPo.Value _
                              & "&STATUS=" & HDStatus.Value & "&DTSTART=" & HdDtStart.Value & "&DTEND=" & HdDtEnd.Value)
    End Sub

    Private Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Dim result As Integer
        Dim _arrAddSPFDetail As ArrayList = New ArrayList
        Dim objSPFHeader As SparePartForecastHeader = New SparePartForecastHeader
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)

        'check datagrid
        Dim objPartForecastMaster As SparePartForecastMaster
        LoopDgrid(objPartForecastMaster, "Add", 2)
        _arrAddSPFDetail = _sessHelper.GetSession("addNewParts")

        If _arrAddSPFDetail.Count > 0 Then
            objSPFHeader.ID = hdID.Value
            objSPFHeader.PoNumber = lblNoPO.Text()
            objSPFHeader.PoDate = HdDate.Value
            objSPFHeader.Dealer = objDealer
            objSPFHeader.Status = EnumFieldFixPartOrderStatus.FieldFixPartOrderStatus.Validasi
            result = objSPFHeaderFacade.UpdateTransaction(objSPFHeader, _arrAddSPFDetail)
        Else
            MessageBox.Show("Data Sparepart belum di input")
            Return
        End If

        If result = -1 Then
            MessageBox.Show("Validasi Gagal")
        Else
            'lblNoPO.Text = objSPFHeaderFacade.Retrieve(result).PoNumber.Trim
            MessageBox.Show("Validasi Sukses")
            btnSimpan.Enabled = False
            btnValidasi.Enabled = False
        End If

    End Sub
End Class