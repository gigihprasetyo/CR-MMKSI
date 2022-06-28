#Region "Custom Namespace Imports"

Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DiscountProposal
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Diagnostics
Imports KTB.DNet.BusinessFacade

#End Region

Public Class ResponsePKOrderDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDeskripsiSPL As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProjectNameTitle As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProjectNameTtk As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents Label23 As System.Web.UI.WebControls.Label
    Protected WithEvents Label24 As System.Web.UI.WebControls.Label
    Protected WithEvents Label25 As System.Web.UI.WebControls.Label
    Protected WithEvents Label26 As System.Web.UI.WebControls.Label
    Protected WithEvents Label27 As System.Web.UI.WebControls.Label
    Protected WithEvents Label28 As System.Web.UI.WebControls.Label
    Protected WithEvents Label29 As System.Web.UI.WebControls.Label
    Protected WithEvents Label30 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPKStatus As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tblOperator As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents dgPKOrderDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtKTBResponse As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPKDate As System.Web.UI.WebControls.Label
    ' Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchPenjelasan As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchKonfirmasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblProjectName As System.Web.UI.WebControls.Label
    Protected WithEvents lblPKNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblProductionYear As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderPlan As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPKValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeadPKNumberTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeadPKNumberTtk As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeadPKNumberValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label31 As System.Web.UI.WebControls.Label
    Protected WithEvents Label32 As System.Web.UI.WebControls.Label
    Protected WithEvents Label35 As System.Web.UI.WebControls.Label
    Protected WithEvents Label36 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnitValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaUnitPD As System.Web.UI.WebControls.Label
    Protected WithEvents trPenjelasanKonfirmasi As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents txtNomorSPL As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchSPL As System.Web.UI.WebControls.Label
    Protected WithEvents trSPL As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents btnRefresh As System.Web.UI.WebControls.Button
    Protected WithEvents rbtnDate As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtMaxDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbtnDay As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbtnNone As System.Web.UI.WebControls.RadioButton
    Protected WithEvents icMaxDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents TOP As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlInterest As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RbtnKonfirmasi1 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents RbtnKonfirmasi2 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents RbtnKonfirmasi3 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents RbtnKonfirmasi4 As System.Web.UI.WebControls.RadioButton
    Protected WithEvents lblKonfirmasi1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKonfirmasi2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKonfirmasi3 As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnSearchSPL As System.Web.UI.WebControls.LinkButton
    Protected WithEvents chkDeposit As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblGuarantee As System.Web.UI.WebControls.Label
    Protected WithEvents lblColonGuarantee As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents lblKodeDiskonFleet As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDiskonFleet2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtFleetDiscountCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lnkReloadSPL As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblNumOfInstallment As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaxTOPDay As System.Web.UI.WebControls.Label
    Protected WithEvents cbxIsConfirmation As System.Web.UI.WebControls.CheckBox

    Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents lblEvidencePath As Label
    Protected WithEvents lblFileName As LinkButton
    Protected WithEvents lblUploadDok As Label
    Protected WithEvents lbltitik2Upload As Label
    Protected WithEvents lnkbtnFileName As LinkButton
    Protected WithEvents lbtnDeleteFile As LinkButton

    Protected WithEvents tdPenjelasan As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents tdTitik2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents tdDescription As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents lblPenjelasan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitik2 As System.Web.UI.WebControls.Label

    Protected WithEvents TOPTitle1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents TOPTitle2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents TOPTitle3 As System.Web.UI.HtmlControls.HtmlTableCell

    Protected WithEvents TOPTtk1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents TOPTtk2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents TOPTtk3 As System.Web.UI.HtmlControls.HtmlTableCell

    Protected WithEvents TOPCol1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents TOPCol2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents TOPCol3 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents trProjectName As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trUploadDok As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trGuarantee As System.Web.UI.HtmlControls.HtmlTableRow

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
    Private arrListPKdetail As ArrayList
    Private arrListPKDetailtoDiscount As ArrayList
    Private _sessionHelper As New SessionHelper
    Private Vtype As String
    Private FU As String = "UploadFile"
    Private FU_NAME As String = "FU_FileName"
    Private objDealer As Dealer

    Private Const sessArrListPKDetail = "ResponsePKOrderDetail.sessArrListPKDetail"
    Private Const sessArrListPKDetailtoDiscount = "ResponsePKOrderDetail.sessArrListPKDetailtoDiscount"
    Private Const sessDeleteArrListPKDetail = "ResponsePKOrderDetail.sessDeleteArrListPKDetail"
    Private Const sessDeleteArrListPKDetailtoDiscount = "ResponsePKOrderDetail.sessDeleteArrListPKDetailtoDiscount"
#End Region

#Region "Custom Method"

    Private Sub SetDtgPesananKendaraanItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblEditKodeWarna As Label = CType(e.Item.Cells(1).FindControl("lblEditKodeWarna"), Label)
        lblEditKodeWarna.Attributes("onclick") = "ShowPPKodeWarnaSelection();"
    End Sub

    Private Sub _enable(ByVal enable As Boolean)
        ddlCategory.Enabled = enable
        ddlOrderType.Enabled = enable
        'ddlProductionYear.Enabled = enable
        'ddlOrderPlanMonth.Enabled = enable
        'ddlOrderPlanYear.Enabled = enable
    End Sub

    Private Sub BindToddlCategory()
        'ddlCategory.DataSource = New PKHeaderFacade(User).RetrieveListCategory
        'ddlCategory.DataTextField = "CategoryCode"
        'ddlCategory.DataValueField = "ID"
        'ddlCategory.DataBind()

        ddlCategory.Items.Clear()
        Dim arrayListDealer As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
        Dim Item As ListItem
        Item = New ListItem("Silahkan Pilih", -1)
        ddlCategory.Items.Add(Item)
        For Each _item As Category In arrayListDealer
            Dim _listItem As New ListItem(_item.CategoryCode, _item.ID)
            ddlCategory.Items.Add(_listItem)
        Next
    End Sub

    Private Sub BindToddl()

        Dim listitemBlank As ListItem

        'listitemBlank = New ListItem("Silahkan Pilih", -1)
        'ddlOrderPlanYear.Items.Add(listitemBlank)
        'For Each item As ListItem In LookUp.ArraylistYear(True, 0, 10, DateTime.Now.Year.ToString)
        '    ddlOrderPlanYear.Items.Add(item)
        'Next
        'ddlOrderPlanYear.SelectedValue = DateTime.Now.Year

        'listitemBlank = New ListItem("Silahkan Pilih", -1)
        'ddlOrderPlanMonth.Items.Add(listitemBlank)
        'For Each item As ListItem In LookUp.ArrayBulan
        '    ddlOrderPlanMonth.Items.Add(item)
        'Next

        ddlOrderType.Items.Clear()
        listitemBlank = New ListItem("Silahkan Pilih", -1)
        ddlOrderType.Items.Add(listitemBlank)
        For Each item As ListItem In LookUp.ArrayJenisPesanan
            ddlOrderType.Items.Add(item)
        Next

        'listitemBlank = New ListItem("Silahkan Pilih", -1)
        'ddlProductionYear.Items.Add(listitemBlank)
        'For Each item As ListItem In LookUp.ArraylistYear(True, 10, 1, DateTime.Now.Year.ToString)
        '    ddlProductionYear.Items.Add(item)
        'Next
        BindToddlCategory()

    End Sub

    Private Sub BindDataGrid(ByVal id As Integer)
        arrListPKdetail = _sessionHelper.GetSession(sessArrListPKDetail)
        arrListPKDetailtoDiscount = _sessionHelper.GetSession(sessArrListPKDetailtoDiscount)

        Dim objPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(id)
        If Not IsNothing(objPKHeader) AndAlso objPKHeader.ID > 0 Then
            Dim _obsSPL As SPL = New SPLFacade(User).Retrieve(objPKHeader.SPLNumber)
            If Not IsNothing(_obsSPL) Then
                If Not IsNothing(arrListPKdetail) Then
                    Dim strPKDetailID As String = ""
                    For Each oPKdetail As PKDetail In arrListPKdetail
                        If strPKDetailID = "" Then
                            strPKDetailID = oPKdetail.ID.ToString()
                        Else
                            strPKDetailID += "," & oPKdetail.ID.ToString()
                        End If
                    Next
                    If strPKDetailID <> "" Then
                        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetailtoDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetailtoDiscount), "PKDetail.ID", MatchType.InSet, "(" & strPKDetailID & ")"))
                        arrListPKDetailtoDiscount = New PKDetailtoDiscountFacade(User).Retrieve(criterias2)
                    End If
                End If
            End If
        End If
        _sessionHelper.SetSession(sessArrListPKDetailtoDiscount, arrListPKDetailtoDiscount)

        bindGrid()
        lblTotalUnitValue.Text = FormatNumber(Calculation.CountPKUnit(arrListPKdetail), 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " Unit"
        lblTotalHargaUnitPD.Text = "Rp " & FormatNumber(Calculation.CountPKHargaTotal(arrListPKdetail), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        tblOperator.Visible = True
    End Sub

    Private Sub bindGrid()
        dgPKOrderDetail.DataSource = arrListPKdetail
        dgPKOrderDetail.DataBind()
    End Sub

    Private Function PopulatePKDetailDiscount() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim txtDiskon As System.Web.UI.WebControls.TextBox
        Dim txtSurcharge As TextBox
        Dim oExArgs As New System.Collections.ArrayList
        'Dim status As New enumStatus

        Dim objPKDetailFacade As New PKDetailFacade(User)

        'New 2007 04 09
        Dim currrows As Integer = 0

        For Each oDataGridItem In dgPKOrderDetail.Items
            currrows = currrows + 1

            txtDiskon = oDataGridItem.FindControl("txtDiskon")
            txtSurcharge = oDataGridItem.FindControl("txtSurcharge")
            Dim _pk As KTB.DNet.Domain.PKDetail
            Dim pkID As Integer = CType(oDataGridItem.FindControl("lblID"), Label).Text
            _pk = New PKDetailFacade(User).Retrieve(pkID)

            If txtDiskon.Text.Trim = "" Then
                _pk.ResponseDiscount = 0
            Else
                _pk.ResponseDiscount = txtDiskon.Text
            End If

            If txtSurcharge.Text.Trim = "" Then
                _pk.ResponseSalesSurcharge = 0
            Else
                _pk.ResponseSalesSurcharge = txtSurcharge.Text
            End If
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, _pk.VechileColor.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, _pk.PKHeader.Dealer.ID))
            Dim sortColl As SortCollection = New SortCollection
            'If (Not IsNothing("ValidFrom")) Then
            'old
            'sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.ASC))
            'new 2007 04 09
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
            'Else
            'sortColl = Nothing
            'End If
            Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)
            If (objPriceArrayList.Count <> 0) Then
                Dim objPrice As Price
                For Each item As Price In objPriceArrayList
                    'old
                    'If item.ValidFrom <= System.Convert.ToDateTime(_pk.PKHeader.PKDate) Then
                    'new 2007 04 09
                    'OLD=>If item.ValidFrom <= New DateTime(CType(_pk.PKHeader.RequestPeriodeYear, Integer), CType(_pk.PKHeader.RequestPeriodeMonth, Integer), 1) Then
                    If item.ValidFrom <= New DateTime(CType(_pk.PKHeader.RequestPeriodeYear, Integer), CType(_pk.PKHeader.RequestPeriodeMonth, Integer), CType(_pk.PKHeader.RequestPeriodeDay, Integer)) Then
                        objPrice = item
                        'new 2007 04 09
                        Exit For
                    End If
                Next
                If IsNothing(objPrice) Then
                    'MessageBox.Show("Harga " + _pk.VehicleTypeCode + " " + _pk.VehicleColorCode + " tidak lengkap, mohon dilengkapi.")
                    If lblMessage.Text.Trim() = "" Then
                        lblMessage.Text = lblMessage.Text + "Harga " + _pk.VehicleTypeCode + " " + _pk.VehicleColorCode + " tidak lengkap, mohon dilengkapi (line " + currrows.ToString() + ")."
                    Else
                        lblMessage.Text = lblMessage.Text + "<br>Harga " + _pk.VehicleTypeCode + " " + _pk.VehicleColorCode + " tidak lengkap, mohon dilengkapi (line " + currrows.ToString() + ")."
                    End If
                Else
                    _pk.ResponseAmount = Calculation.CountPKVehiclePrice(CType(_pk.ResponseDiscount, Double), CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.BasePrice, Double), CType(objPrice.OptionPrice, Double), _pk.ResponseSalesSurcharge)
                    _pk.ResponsePPh22 = Calculation.CountPKPPh22(CType(objPrice.BasePrice, Double), CType(_pk.ResponseDiscount, Double), CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.PPh22, Double))
                End If
            End If
            '_pk.ResponseQty = CType(oDataGridItem.FindControl("txtUnit"), TextBox).Text

            Dim vc As New VechileColor(CInt(CType(oDataGridItem.FindControl("lblColor"), Label).Text))
            _pk.VechileColor = vc
            oExArgs.Add(_pk)
        Next
        Return oExArgs
    End Function

    Private Sub RetrievePKHeader(ByVal id As Integer)
        Dim objPKHeaderFacade As New PKHeaderFacade(User)
        Dim objPKHeader As New PKHeader

        objPKHeader = objPKHeaderFacade.Retrieve(id)

        lblPKDate.Text = Format(objPKHeader.PKDate, "dd/MM/yyyy")

        lblNomorPKValue.Text = objPKHeader.PKNumber.ToString


        If Not IsNothing(objPKHeader.DealerBranch) Then
            lblDealerBranch.Text = objPKHeader.DealerBranch.DealerBranchCode & " / " & objPKHeader.DealerBranch.Term1

        End If
        If (objPKHeader.HeadPKNumber <> 0) Then
            lblHeadPKNumberValue.Text = New PKHeaderFacade(User).Retrieve(objPKHeader.HeadPKNumber).PKNumber
        Else
            lblHeadPKNumberValue.Text = String.Empty
        End If

        Dim _obsSPL As New SPL
        lblDeskripsiSPL.Text = ""
        If Not IsNothing(objPKHeader) Then
            _obsSPL = New SPLFacade(User).Retrieve(objPKHeader.SPLNumber)

            If Not IsNothing(_obsSPL) Then
                lblDeskripsiSPL.Text = _obsSPL.Description
                lblNumOfInstallment.Text = _obsSPL.NumOfInstallment
                lblMaxTOPDay.Text = _obsSPL.MaxTOPDay
            End If
        End If

        lblProjectName.Text = objPKHeader.ProjectName.ToString
        txtDescription.Text = objPKHeader.Description.ToString
        lblPKNumber.Text = objPKHeader.DealerPKNumber.ToString
        txtKTBResponse.Text = objPKHeader.KTBResponse.ToString
        If txtKTBResponse.Text.ToUpper = lblKonfirmasi1.Text.ToUpper Then
            RbtnKonfirmasi1.Checked = True
        ElseIf txtKTBResponse.Text.ToUpper = lblKonfirmasi2.Text.ToUpper Then
            RbtnKonfirmasi2.Checked = True
        ElseIf txtKTBResponse.Text.ToUpper = lblKonfirmasi3.Text.ToUpper Then
            RbtnKonfirmasi3.Checked = True
        Else
            RbtnKonfirmasi4.Checked = True
        End If

        txtNomorSPL.Text = objPKHeader.SPLNumber

        lblGuarantee.Visible = False
        lblColonGuarantee.Visible = False
        chkDeposit.Enabled = False
        chkDeposit.Checked = False
        Dim IsEditable As Boolean = False
        Dim IsHidden As Boolean = False

        If objPKHeader.JaminanID > 0 Then
            chkDeposit.Checked = True
            IsHidden = False
            If objPKHeader.PKStatus = enumStatusPK.Status.Konfirmasi Then
                IsEditable = True
            Else
                IsEditable = False
            End If
        Else
            chkDeposit.Checked = False
            IsHidden = True
            IsEditable = False
            If objPKHeader.PKStatus = enumStatusPK.Status.Konfirmasi AndAlso JaminanForPKH(objPKHeader).ID > 0 Then
                IsHidden = False
                IsEditable = True
            End If
        End If
        If IsHidden = False And CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
            chkDeposit.Visible = True
            lblGuarantee.Visible = True
            lblColonGuarantee.Visible = True
        Else
            chkDeposit.Visible = False
            lblGuarantee.Visible = False
            lblColonGuarantee.Visible = False
        End If

        If objPKHeader.PKStatus = enumStatusPK.Status.Konfirmasi And CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
            lblKodeDiskonFleet.Visible = True
            lblKodeDiskonFleet2.Visible = True
            txtFleetDiscountCode.Visible = True
        Else
            lblKodeDiskonFleet.Visible = False
            lblKodeDiskonFleet2.Visible = False
            txtFleetDiscountCode.Visible = False
            txtFleetDiscountCode.Text = ""
        End If

        If IsEditable And CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
            chkDeposit.Enabled = True
        Else
            chkDeposit.Enabled = False
        End If
        'If objPKHeader.PKStatus = enumStatusPK.Status.Konfirmasi Then
        '    chkDeposit.Visible = True
        '    lblGuarantee.Visible = True
        '    lblColonGuarantee.Visible = True
        '    If objPKHeader.JaminanID > 0 Then
        '        chkDeposit.Checked = True
        '    End If
        '    chkDeposit.Enabled = True
        'Else
        '    If objPKHeader.JaminanID > 0 Then
        '        chkDeposit.Checked = True
        '        chkDeposit.Visible = True
        '        lblGuarantee.Visible = True
        '        lblColonGuarantee.Visible = True
        '    End If
        'End If

        Dim _status As enumStatusPK.Status = objPKHeader.PKStatus
        Dim tgl As New DateTime(objPKHeader.RequestPeriodeYear, objPKHeader.RequestPeriodeMonth, 1)
        lblStatus.Text = _status.ToString

        ddlCategory.SelectedValue = objPKHeader.Category.ID
        ddlOrderType.SelectedValue = objPKHeader.OrderType
        'ddlProductionYear.SelectedValue = objPKHeader.ProductionYear
        lblProductionYear.Text = objPKHeader.ProductionYear
        lblOrderPlan.Text = Format(tgl, "MMM yyyy")

        'ddlOrderPlanMonth.SelectedValue = objPKHeader.RequestPeriodeMonth
        'ddlOrderPlanYear.SelectedValue = objPKHeader.RequestPeriodeYear

        Dim _dealer As Dealer = New DealerFacade(User).Retrieve(objPKHeader.Dealer.ID)
        lblDealerCode.Text = _dealer.DealerCode & " / " & _dealer.SearchTerm1
        lblDealerName.Text = _dealer.DealerName

        Dim _city As City = New CityFacade(User).Retrieve(_dealer.City.ID)
        lblCity.Text = _city.CityName

        'If objPKHeader.FreeIntIndicator = 0 Then
        '    chkFreeInterest.Checked = True

        'ElseIf objPKHeader.FreeIntIndicator = 1 Then
        '    chkFreeInterest.Checked = False
        'End If
        ddlInterest.SelectedValue = objPKHeader.FreeIntIndicator
        rbtnDate.Checked = False
        rbtnDay.Checked = False
        rbtnNone.Checked = True
        If objPKHeader.MaxTopIndicator = 0 Then
            rbtnDate.Checked = True
            rbtnDay.Checked = False
            rbtnNone.Checked = False
            If objPKHeader.MaxTOPDate < New Date(1901, 1, 1) Then
                icMaxDate.Value = Now
            Else
                icMaxDate.Value = objPKHeader.MaxTOPDate
            End If
            icMaxDate.Enabled = True
            txtMaxDay.Text = ""
            txtMaxDay.Enabled = False
        ElseIf objPKHeader.MaxTopIndicator = 1 Then
            rbtnDate.Checked = False
            rbtnDay.Checked = True
            rbtnNone.Checked = False
            icMaxDate.Value = DateTime.Now
            icMaxDate.Enabled = False
            txtMaxDay.Text = Convert.ToString(objPKHeader.MaxTopDay)
            txtMaxDay.Enabled = True
        ElseIf objPKHeader.MaxTopIndicator = -1 Then
            rbtnDate.Checked = False
            rbtnDay.Checked = False
            rbtnNone.Checked = True
            icMaxDate.Value = DateTime.Now
            icMaxDate.Enabled = False
            txtMaxDay.Text = ""
            txtMaxDay.Enabled = False
        End If

        If objPKHeader.Purpose = LookUp.enumPurpose.Biasa Then
            trPenjelasanKonfirmasi.Visible = True

            lblPenjelasan.Attributes("style") = "display:none"
            lblTitik2.Attributes("style") = "display:none"
            txtDescription.Attributes("style") = "display:none"
            lblSearchPenjelasan.Attributes("style") = "display:none"

            'lblPenjelasan.Visible = False
            'lblTitik2.Visible = False
            'txtDescription.Visible = False
            'lblSearchPenjelasan.Visible = False

            lblProjectNameTitle.Visible = False
            lblProjectNameTtk.Visible = False
            lblProjectName.Visible = False
            lblHeadPKNumberTitle.Visible = False
            lblHeadPKNumberTtk.Visible = False
            lblHeadPKNumberValue.Visible = False
            trSPL.Visible = True
        End If

        If objPKHeader.PKType = 0 Then
            'TOP.Visible = True

            TOPTitle1.Attributes("style") = "display:table-row"
            TOPTitle2.Attributes("style") = "display:table-row"
            TOPTitle3.Attributes("style") = "display:table-row"
            TOPTtk1.Attributes("style") = "display:table-row"
            TOPTtk2.Attributes("style") = "display:table-row"
            TOPTtk3.Attributes("style") = "display:table-row"
            TOPCol1.Attributes("style") = "display:table-row"
            TOPCol2.Attributes("style") = "display:table-row"
            TOPCol3.Attributes("style") = "display:table-row"
        Else
            'TOP.Visible = False

            TOPTitle1.Attributes("style") = "display:none"
            TOPTitle2.Attributes("style") = "display:none"
            TOPTitle3.Attributes("style") = "display:none"
            TOPTtk1.Attributes("style") = "display:none"
            TOPTtk2.Attributes("style") = "display:none"
            TOPTtk3.Attributes("style") = "display:none"
            TOPCol1.Attributes("style") = "display:none"
            TOPCol2.Attributes("style") = "display:none"
            TOPCol3.Attributes("style") = "display:none"
        End If

        If lblProjectNameTitle.Visible = False AndAlso TOPTitle1.Attributes("style") = "display:none" Then
            trProjectName.Visible = False
        Else
            trProjectName.Visible = True
        End If
        If lblPenjelasan.Attributes("style") = "display:none" AndAlso TOPTitle2.Attributes("style") = "display:none" Then
            trPenjelasanKonfirmasi.Visible = False
        Else
            trPenjelasanKonfirmasi.Visible = True
        End If

        lblKodeDiskonFleet.Visible = False
        lblKodeDiskonFleet2.Visible = False
        txtFleetDiscountCode.Visible = False

        cbxIsConfirmation.Checked = If(objPKHeader.IsFormAConfirmation = 0, False, True)

        If lblUploadDok.Visible = False AndAlso lblKodeDiskonFleet.Visible = False AndAlso lblDeskripsiSPL.Visible = False Then
            trUploadDok.Visible = False
        Else
            trUploadDok.Visible = True
        End If
        If lblGuarantee.Visible = False AndAlso lblHeadPKNumberTitle.Visible = False Then
            trGuarantee.Visible = False
        Else
            trGuarantee.Visible = True
        End If

        'btnRilis.Visible = If(objPKHeader.PKStatus = "3", True, False)
        If objPKHeader.PKStatus = "3" Then
            btnRilis.Attributes("style") = "display:table-row"
        Else
            btnRilis.Attributes("style") = "display:none"
        End If

        '--CR PK 2020/02/03
        lblEvidencePath.Text = objPKHeader.EvidencePath
        Dim _fileName() As String = lblEvidencePath.Text.Split("\")
        lblFileName.Text = _fileName(_fileName.Length - 1)
        lnkbtnFileName.Visible = True
        If lblFileName.Text.Trim <> "" Then
            cbxIsConfirmation.Checked = True
        End If

        Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
        Dim DestFullFilePath As String = fileInfo1.Directory.FullName

        If objPKHeader.EvidencePath.Trim <> "" Then
            Dim dataFile As String = DestFullFilePath & "\" & objPKHeader.EvidencePath

            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" OrElse
                dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".png" OrElse
                dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpeg" Then

                lnkbtnFileName.Text = "<img src=""../images/detail.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" >"
            Else
                lnkbtnFileName.Text = "<img src=""../images/detail.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"">"
            End If
            lnkbtnFileName.Visible = True
            lnkbtnFileName.ToolTip = lblFileName.Text

            lblFileName.Visible = True
            _sessionHelper.SetSession(FU_NAME, dataFile)
            lblEvidencePath.Text = dataFile
            lblFileName.Text = Path.GetFileName(dataFile)
        End If
        cbxIsConfirmation_CheckedChanged(Nothing, Nothing)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.ID", MatchType.Exact, objPKHeader.ID))
        arrListPKdetail = New PKDetailFacade(User).Retrieve(criterias)
        _sessionHelper.SetSession(sessArrListPKDetail, arrListPKdetail)
        _sessionHelper.SetSession("objPKHeader", objPKHeader)
    End Sub

    Private Function PopulateRemindProcess() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgPKOrderDetail.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            Dim _pkDetail As PKDetail
            Dim ID As Integer = CType(oDataGridItem.FindControl("lblID"), Label).Text
            _pkDetail = New PKDetailFacade(User).Retrieve(ID)
            Dim targetQTy As Integer = CType(oDataGridItem.FindControl("lblUnitDealer"), Label).Text
            Dim respQTY As Integer = CType(oDataGridItem.FindControl("lblResponKTB"), Label).Text

            If targetQTy > respQTY Then
                _pkDetail.TargetQty = targetQTy - respQTY
                _pkDetail.ResponseQty = 0
                oExArgs.Add(_pkDetail)
            End If
        Next
        Return oExArgs
    End Function

    Private Function IsSPLRegisterForDealer(ByVal objDealer As Dealer, ByVal objSPL As SPL) As Boolean
        For Each item As SPLDealer In objSPL.SPLDealers
            If item.Dealer.DealerCode = objDealer.DealerCode Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function SaveConfirmation() As Boolean
        Dim _result As Boolean = False

        '--CR PK 2020/02/03
        If cbxIsConfirmation.Checked = True Then
            If lblFileName.Text.Trim = "" Then
                MessageBox.Show("Silahkan untuk upload file dahulu")
                Return False
            End If
        End If
        '----
        Dim arlPKDetail As ArrayList = PopulatePKDetailDiscount()
        Dim objPKHeaderFacade As New PKHeaderFacade(User)
        Dim id As Integer = Request.QueryString("master")
        Dim objPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(id)
        objPKHeader.Description = txtDescription.Text

        'objPKHeader.ProjectName = lblProjectName.Text
        'objPKHeader.Category = New CategoryFacade(User).Retrieve(CInt(ddlCategory.SelectedValue))
        'objPKHeader.OrderType = ddlOrderType.SelectedValue
        'objPKHeader.ProductionYear = ddlProductionYear.SelectedValue
        'objPKHeader.Description = txtDescription.Text
        'objPKHeader.KTBResponse = txtKTBResponse.Text
        'objPKHeader.RequestPeriodeMonth = ddlOrderPlanMonth.SelectedValue
        'objPKHeader.RequestPeriodeYear = ddlOrderPlanYear.SelectedValue
        'objPKHeader.DealerPKNumber = lblPKNumber.Text
        If chkDeposit.Checked = False Then
            objPKHeader.JaminanID = 0
        Else
            If objPKHeader.JaminanID = 0 Then
                Dim oJ As Jaminan = JaminanForPKH(objPKHeader)
                objPKHeader.JaminanID = oJ.ID
            End If
        End If
        objPKHeader.KTBResponse = txtKTBResponse.Text
        Dim objSPL As SPL
        If txtNomorSPL.Text <> String.Empty Then
            objSPL = New SPLFacade(User).Retrieve(txtNomorSPL.Text)
            If objSPL Is Nothing Then
                MessageBox.Show("Nomor SPL Tidak Terdaftar")
                _result = False
                Return _result
            Else
                If objSPL.ID < 1 Then
                    MessageBox.Show("Nomor SPL tidak ditemukan")
                    Return False
                Else
                    Dim pkHeader As PKHeader = New PKHeaderFacade(User).Retrieve(id)
                    If pkHeader.SPLNumber <> txtNomorSPL.Text.Trim Then
                        arrListPKdetail = CType(_sessionHelper.GetSession(sessArrListPKDetail), ArrayList)
                        If Not CheckQtySPL(arrListPKdetail, objSPL) Then
                            MessageBox.Show("Tipe tidak valid/Sisa unit lebih besar dari sisa Aplikasi " & Vtype)
                            Return False
                        End If
                    End If
                End If

                If IsSPLRegisterForDealer(objPKHeader.Dealer, objSPL) Then
                    If SPLDateValid(objSPL) Then
                        objPKHeader.SPLNumber = objSPL.SPLNumber
                    Else
                        MessageBox.Show("Nomor Aplikasi sudah tidak valid.")
                        _result = False
                        Return _result
                    End If
                Else
                    MessageBox.Show("Nomor Aplikasi " & txtNomorSPL.Text & " Tidak Terdaftar untuk Dealer " & objPKHeader.Dealer.SearchTerm1)
                    _result = False
                    Return _result
                End If
            End If
        Else
            objPKHeader.SPLNumber = ""
            ddlInterest.SelectedIndex = 1
            rbtnDate.Checked = False
            rbtnDay.Checked = False
            rbtnNone.Checked = True
            icMaxDate.Value = Now
            txtMaxDay.Text = "0"
            objPKHeader.MaxTopDay = 0
            objPKHeader.MaxTopIndicator = 0
            icMaxDate.Value = New DateTime(1900, 1, 1)
            objPKHeader.MaxTOPDate = New DateTime(1900, 1, 1)
        End If

        If rbtnDate.Checked And Not rbtnDay.Checked And Not rbtnNone.Checked Then
            objPKHeader.MaxTopIndicator = 0
            objPKHeader.MaxTOPDate = icMaxDate.Value
            objPKHeader.MaxTopDay = 0
        ElseIf Not rbtnDate.Checked And rbtnDay.Checked And Not rbtnNone.Checked Then
            objPKHeader.MaxTopIndicator = 1
            objPKHeader.MaxTOPDate = icMaxDate.Value
            If IsNumeric(txtMaxDay.Text) Then
                objPKHeader.MaxTopDay = CInt(txtMaxDay.Text)
            Else
                MessageBox.Show("Max Top Day tidak boleh kosong.")
                _result = False
                Return _result
            End If

        ElseIf Not rbtnDate.Checked And Not rbtnDay.Checked And rbtnNone.Checked Then
            objPKHeader.MaxTopIndicator = -1
            objPKHeader.MaxTopDay = 0
            txtMaxDay.Text = "0"
        End If
        'If chkFreeInterest.Checked = True Then
        '    objPKHeader.FreeIntIndicator = 0
        'Else
        '    objPKHeader.FreeIntIndicator = 1
        'End If
        objPKHeader.FreeIntIndicator = ddlInterest.SelectedValue
        objPKHeader.FleetDiscountCode = txtFleetDiscountCode.Text

        '----  add CR PK 2020/01/22
        If objPKHeader.PKStatus = enumStatusPK.Status.Konfirmasi Then
            If txtNomorSPL.Text.Trim <> "" Then
                If Not IsNothing(objSPL) AndAlso objSPL.ID > 0 Then
                    objPKHeader.IsAproveRilis = 1
                    If objSPL.IsAutoApprovedDealer = 1 Then
                        objPKHeader.IsAutoApprovedDealer = 1
                    Else
                        objPKHeader.IsAutoApprovedDealer = 0
                    End If
                Else
                    objPKHeader.IsAproveRilis = 0
                End If
            End If
        End If
        If (Not _sessionHelper.GetSession(FU_NAME) Is Nothing) OrElse (Not _sessionHelper.GetSession(FU) Is Nothing) Then
            SetEvidencePath(objPKHeader)
        Else
            objPKHeader.EvidencePath = ""
        End If
        '---------
        arrListPKDetailtoDiscount = CType(_sessionHelper.GetSession(sessArrListPKDetailtoDiscount), ArrayList)
        For Each objPKDetail As PKDetail In arlPKDetail
            If Not IsNothing(objSPL) Then
                If Not IsNothing(objSPL.SPLDetails) Then
                    For Each objSPLDetail As SPLDetail In objSPL.SPLDetails
                        Dim tmpDate As Date
                        Dim FrmDate As Date
                        tmpDate = DateSerial(objSPLDetail.PeriodYear, objSPLDetail.PeriodMonth, 1)
                        FrmDate = CDate("1 " & lblOrderPlan.Text)
                        If objPKDetail.VechileType.ID = objSPLDetail.VechileType.ID And tmpDate = FrmDate Then
                            If Not IsNothing(objSPLDetail.SPLDetailtoSPLs) Then
                                For Each objSPLDetailtoSPL As SPLDetailtoSPL In objSPLDetail.SPLDetailtoSPLs
                                    If Not IsNothing(arrListPKDetailtoDiscount) Then
                                        For Each objPKDetailtoDiscount As PKDetailtoDiscount In arrListPKDetailtoDiscount
                                            If objPKDetailtoDiscount.PKDetail.ID = objPKDetail.ID AndAlso objPKDetailtoDiscount.SPLDetailtoSPL.ID = objSPLDetailtoSPL.ID Then
                                                objPKDetailtoDiscount.Discount = objSPLDetailtoSPL.Discount
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
            End If
        Next
        '--------------------------------------------------------------

        Dim arlDeletePKDetail As ArrayList = CType(_sessionHelper.GetSession(sessDeleteArrListPKDetail), ArrayList)
        Dim arlDeletePKDetailtoDiscount As ArrayList = CType(_sessionHelper.GetSession(sessDeleteArrListPKDetailtoDiscount), ArrayList)

        Dim resultTrans As Integer = objPKHeaderFacade.UpdatePKHeaderDetail(objPKHeader, arlPKDetail, arlDeletePKDetail, arrListPKDetailtoDiscount, arlDeletePKDetailtoDiscount)

        Dim m2 = New PKHeaderFacade(User).RefreshDiscount(objPKHeader.ID)
        If resultTrans > -1 Then
            _result = True
            Dim iDI = objPKHeader.ID
            objPKHeader = objPKHeaderFacade.Retrieve(iDI)
            If Not _sessionHelper.GetSession(FU) Is Nothing Then
                Dim _filename As String = Path.GetFileName(CType(_sessionHelper.GetSession(FU_NAME), String))
                SaveFile(_filename)
            End If

            RetrievePKHeader(objPKHeader.ID)
            _sessionHelper.SetSession("objPKHeader", objPKHeader)
        End If


        Return _result
    End Function

    Private Function SetPKDetailtoDiscount(ByVal objSPL As SPL, ByVal arlPKDetail As ArrayList) As ArrayList
        Dim arlPKDetailtoDiscount As New ArrayList
        Dim objPKDetailtoDiscount As PKDetailtoDiscount
        If IsNothing(arlPKDetail) Then arlPKDetail = New ArrayList
        For Each objPKDetail As PKDetail In arlPKDetail
            If Not IsNothing(objSPL.SPLDetails) Then
                For Each objSPLDetail As SPLDetail In objSPL.SPLDetails
                    Dim tmpDate As Date
                    Dim FrmDate As Date
                    tmpDate = DateSerial(objSPLDetail.PeriodYear, objSPLDetail.PeriodMonth, 1)
                    FrmDate = CDate("1 " & lblOrderPlan.Text)
                    If objPKDetail.VechileType.ID = objSPLDetail.VechileType.ID And tmpDate = FrmDate Then
                        If Not IsNothing(objSPLDetail.SPLDetailtoSPLs) Then
                            For Each objSPLDetailtoSPL As SPLDetailtoSPL In objSPLDetail.SPLDetailtoSPLs
                                objPKDetailtoDiscount = New PKDetailtoDiscount
                                objPKDetailtoDiscount.PKDetail = objPKDetail
                                objPKDetailtoDiscount.SPLDetailtoSPL = objSPLDetailtoSPL
                                objPKDetailtoDiscount.Discount = objSPLDetailtoSPL.Discount
                                arlPKDetailtoDiscount.Add(objPKDetailtoDiscount)
                            Next
                        End If
                    End If
                Next
            End If
        Next
        Return arlPKDetailtoDiscount
    End Function

    Private Function GetDirUpload() As DirectoryInfo
        Return New DirectoryInfo(String.Format("{0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("PKFolder"), _
            KTB.DNet.Lib.WebConfig.GetValue("PKFileDirectory"), lblNomorPKValue.Text.Trim))
    End Function

    Function GetEvidencePath() As String
        Return String.Format("{0}\{1}", KTB.DNet.Lib.WebConfig.GetValue("PKFileDirectory"), Path.GetFileName(CType(_sessionHelper.GetSession(FU_NAME), String)))
    End Function

    Private Sub SetEvidencePath(ByVal objPkHeader As PKHeader)
        Dim _filename As String = Path.GetFileName(CType(_sessionHelper.GetSession(FU_NAME), String))
        If Not IsNothing(_filename) Then
            If _filename.Trim().Length > 0 Then
                objPkHeader.EvidencePath = GetEvidencePath()
            End If
        End If
    End Sub

    Private Function SaveFile(ByVal _filename As String) As Boolean
        If IsNothing(_filename) OrElse _filename.ToString.Trim = "" Then Exit Function

        Dim nResult As Boolean = False
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("PKFileDirectory") & "\" & _filename      '-- Destination file
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(DestFile)

                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                Dim ext As String = System.IO.Path.GetExtension(CType(_sessionHelper.GetSession(FU_NAME), String))
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fuStream As Stream = (CType(_sessionHelper.GetSession(FU), Stream))
                Dim ibytes As Long = fuStream.Length
                Dim buffer(ibytes - 1) As Byte
                fuStream.Read(buffer, 0, ibytes)
                fuStream.Close()

                Dim fs As FileStream = New FileStream(DestFile, FileMode.Create)
                fs.Write(buffer, 0, ibytes)
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing
                nResult = True
            End If
        Catch ex As Exception
            nResult = False
            Throw ex
        End Try
        Return nResult
    End Function

    Private Function SPLDateValid(ByVal objSPL As SPL) As Boolean
        Dim currentDate As Date = Now
        Dim sDate As Date = New Date(objSPL.ValidFrom.Year, objSPL.ValidFrom.Month, objSPL.ValidFrom.Day, 0, 0, 0)
        Dim eDate As Date = New Date(objSPL.ValidTo.Year, objSPL.ValidTo.Month, objSPL.ValidTo.Day, 23, 59, 59)
        If currentDate >= sDate And currentDate <= eDate Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ConstructPKHeader(ByVal id As Integer) As PKHeader
        Dim objPKHeaderSource As New PKHeader
        Dim objPKHeaderDestination As New PKHeader

        objPKHeaderSource = New PKHeaderFacade(User).Retrieve(id)

        objPKHeaderDestination.PKDate = objPKHeaderSource.PKDate
        objPKHeaderDestination.HeadPKNumber = objPKHeaderSource.ID
        objPKHeaderDestination.PKStatus = objPKHeaderSource.PKStatus
        objPKHeaderDestination.PKType = objPKHeaderSource.PKType


        objPKHeaderDestination.RequestPeriodeMonth = objPKHeaderSource.RequestPeriodeMonth + 1
        objPKHeaderDestination.RequestPeriodeYear = objPKHeaderSource.RequestPeriodeYear
        If objPKHeaderDestination.RequestPeriodeMonth = 13 Then
            objPKHeaderDestination.RequestPeriodeMonth = 1
            objPKHeaderDestination.RequestPeriodeYear = objPKHeaderDestination.RequestPeriodeYear + 1
        End If
        objPKHeaderDestination.ProjectDetail = objPKHeaderSource.ProjectDetail
        objPKHeaderDestination.ProjectName = lblProjectName.Text
        objPKHeaderDestination.Purpose = objPKHeaderSource.Purpose
        objPKHeaderDestination.ResponseBy = User.Identity.Name
        objPKHeaderDestination.Description = txtDescription.Text
        objPKHeaderDestination.ResponseTime = Now
        objPKHeaderDestination.Dealer = objPKHeaderSource.Dealer
        objPKHeaderDestination.DealerPKNumber = objPKHeaderSource.DealerPKNumber
        objPKHeaderDestination.Category = New CategoryFacade(User).Retrieve(CInt(ddlCategory.SelectedValue))
        objPKHeaderDestination.OrderType = ddlOrderType.SelectedValue
        'objPKHeaderDestination.ProductionYear = ddlProductionYear.SelectedValue
        objPKHeaderDestination.Description = txtDescription.Text
        objPKHeaderDestination.KTBResponse = txtKTBResponse.Text
        objPKHeaderDestination.DealerPKNumber = lblPKNumber.Text
        objPKHeaderDestination.KTBResponse = txtKTBResponse.Text
        Dim objPKHeaderRoot As PKHeader = RetrievePKRoot(objPKHeaderSource)
        objPKHeaderDestination.PricingPeriodeMonth = objPKHeaderRoot.RequestPeriodeMonth
        objPKHeaderDestination.PricingPeriodeYear = objPKHeaderRoot.RequestPeriodeYear

        Return objPKHeaderDestination
    End Function

    Private Function RetrievePKRoot(ByVal objPKHeader As PKHeader) As PKHeader
        Dim isNotPKRoot As Boolean = True
        Dim _PKHeader As Integer = 0
        If objPKHeader.HeadPKNumber = 0 Then
            Return objPKHeader
        End If

        _PKHeader = objPKHeader.HeadPKNumber
        While isNotPKRoot
            objPKHeader = New PKHeaderFacade(User).Retrieve(_PKHeader)
            If objPKHeader.HeadPKNumber = 0 Then
                Return objPKHeader
            End If
            _PKHeader = objPKHeader.HeadPKNumber
        End While

    End Function

    Sub dtgPKOrderDetail_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim lblColor As Label = CType(e.Item.FindControl("lblColorString"), Label)
        If Not lblColor Is Nothing Then
            If lblColor.Text.ToUpper = "ZZZZ" Then
                dgPKOrderDetail.EditItemIndex = CInt(e.Item.ItemIndex)
                BindDataGrid(CInt(ViewState("id")))
            Else
                MessageBox.Show(SR.ValidUpdateColor("ZZZZ"))
            End If
        Else
            MessageBox.Show("Terjadi kesalahan data")
        End If

        'If e.Item.Cells(15).Text.ToUpper = "ZZZZ" Then
        '    dgPKOrderDetail.EditItemIndex = CInt(e.Item.ItemIndex)
        '    BindDataGrid(CInt(ViewState("id")))
        'Else
        '    MessageBox.Show(SR.ValidUpdateColor("ZZZZ"))
        'End If
    End Sub

    Private Function ValidateItem(ByVal kodeModel As String, ByVal kodeWarna As String) As Boolean
        If (kodeWarna = String.Empty) Then
            lblMessage.Text = "Error : Kode Warna Tidak boleh Kosong"
            Return False
        Else
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ColorCode", MatchType.Exact, kodeWarna.ToString))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, kodeModel.ToString))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.Status", MatchType.No, "X"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "Status", MatchType.No, "x"))
            Dim ArrListVechileColor As ArrayList = New VechileColorFacade(User).Retrieve(criterias)
            If (ArrListVechileColor Is Nothing) OrElse (ArrListVechileColor.Count = 0) Then
                lblMessage.Text = "Error : Kode Warna dan Kode Tipe tidak Cocok"
                Return False
            End If
        End If

        Return True
    End Function

    Private Function ValidateDuplication(ByVal kodeModel As String, ByVal kodeWarna As String, ByVal Rowindex As Integer) As Boolean
        arrListPKdetail = _sessionHelper.GetSession(sessArrListPKDetail)
        Dim i As Integer = 0
        For Each item As PKDetail In arrListPKdetail
            If (item.VehicleTypeCode.ToString = kodeModel And item.VehicleColorCode.ToString = kodeWarna) Then
                If i <> Rowindex Then
                    lblMessage.Text = "Error : Duplikasi KodeTipe dan KodeWarna"
                    Return False
                End If
            End If

            i = i + 1
        Next
        Return True
    End Function

    Sub dtgPKOrderDetail_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        arrListPKdetail = _sessionHelper.GetSession(sessArrListPKDetail)
        Dim lbl1 As Label = e.Item.FindControl("lblEditType")
        Dim txt2 As TextBox = e.Item.FindControl("txtEditKodeWarna")
        If (ValidateItem(lbl1.Text.ToUpper, txt2.Text.ToUpper) And ValidateDuplication(lbl1.Text.ToUpper, txt2.Text.ToUpper, e.Item.ItemIndex)) Then
            Dim objPKDetail As PKDetail = arrListPKdetail(e.Item.ItemIndex)
            objPKDetail.VehicleTypeCode = lbl1.Text.ToUpper
            objPKDetail.VehicleColorCode = txt2.Text.ToUpper
            objPKDetail.VehicleColorName = New VechileColorFacade(User).Retrieve(objPKDetail.VehicleColorCode.ToString).ColorEngName
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "ColorCode", MatchType.Exact, txt2.Text))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, lbl1.Text))
            objPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(criterias2)(0)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objPKDetail.VechileColor.ID))
            Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias)
            If (objPriceArrayList.Count <> 0) Then
                Dim objPrice As Price
                For Each item As Price In objPriceArrayList
                    If item.ValidFrom <= System.Convert.ToDateTime(lblOrderPlan.Text) Then
                        objPrice = item
                    End If
                Next
                If Not IsNothing(objPrice) AndAlso objPrice.BasePrice <> 0 Then
                    objPKDetail.TargetAmount = Calculation.CountPKVehiclePrice(0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.BasePrice, Double), CType(objPrice.OptionPrice, Double), objPKDetail.ResponseSalesSurcharge)
                    objPKDetail.ResponseAmount = objPKDetail.TargetAmount
                    objPKDetail.TargetPPh22 = Calculation.CountPKPPh22(CType(objPrice.BasePrice, Double), 0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.PPh22, Double))
                    objPKDetail.ResponsePPh22 = objPKDetail.TargetAmount
                Else
                    MessageBox.Show("Harga Belum Ada")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Harga Belum Ada")
                Exit Sub
            End If
            _sessionHelper.SetSession(sessArrListPKDetail, arrListPKdetail)
            dgPKOrderDetail.EditItemIndex = -1
            Dim pKDetailFacade As New PKDetailFacade(User)
            pKDetailFacade.Update(arrListPKdetail(e.Item.ItemIndex))
            dgPKOrderDetail.DataSource = arrListPKdetail
            dgPKOrderDetail.DataBind()
            tblOperator.Visible = True
        End If
    End Sub

    Sub dtgPKOrderDetail_Cancel(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        dgPKOrderDetail.EditItemIndex = -1
        BindDataGrid(CInt(ViewState("id")))
    End Sub

    Private Sub DetailSPL()
        Dim objSPLFac As SPLFacade = New SPLFacade(User)
        Dim objSPL As SPL
        Dim objSPLDet As SPLDetail
        Dim i As Integer

        DefaultSPLControl()

        If txtNomorSPL.Text.Trim = "" Then
jumpResetSPL:
            ddlInterest.SelectedIndex = 1
            rbtnDate.Checked = False
            rbtnDay.Checked = False
            rbtnNone.Checked = True
            icMaxDate.Value = New DateTime(1900, 1, 1)
            lblNumOfInstallment.Text = ""
            lblMaxTOPDay.Text = ""
            ddlInterest.SelectedIndex = 0
            txtMaxDay.Text = "0"
            For Each dtgItem As DataGridItem In dgPKOrderDetail.Items
                Dim txtDiskon As TextBox = CType(dtgItem.FindControl("txtDiskon"), TextBox)
                Dim txtSurcharge As TextBox = CType(dtgItem.FindControl("txtSurcharge"), TextBox)
                txtDiskon.Text = 0
                txtSurcharge.Text = 0
            Next

            Dim arrDeletePKDetailtoDiscount As ArrayList = CType(_sessionHelper.GetSession(sessDeleteArrListPKDetailtoDiscount), ArrayList)
            If IsNothing(arrDeletePKDetailtoDiscount) Then arrDeletePKDetailtoDiscount = New ArrayList
            arrListPKDetailtoDiscount = _sessionHelper.GetSession(sessArrListPKDetailtoDiscount)
            If Not IsNothing(arrListPKDetailtoDiscount) Then
                For Each oPKDetailtoDiscount As PKDetailtoDiscount In arrListPKDetailtoDiscount
                    arrDeletePKDetailtoDiscount.Add(oPKDetailtoDiscount)
                Next
            End If
            'Set nothing arrayList PKDetailtoDiscount
            _sessionHelper.SetSession(sessDeleteArrListPKDetailtoDiscount, arrDeletePKDetailtoDiscount)
            _sessionHelper.SetSession(sessArrListPKDetailtoDiscount, Nothing)

            Exit Sub
        End If

        objSPL = objSPLFac.Retrieve(txtNomorSPL.Text)
        'If objSPL Is Nothing Then Exit Sub
        If Not IsNothing(objSPL) Then

            'Dim arlSPLDetail As ArrayList = New ArrayList
            'Dim critsSPLD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'critsSPLD.opAnd(New Criteria(GetType(SPLDetail), "SPLID", MatchType.Exact, objSPL.ID))
            'arlSPLDetail = New SPLDetailFacade(User).Retrieve(critsSPLD)
            'objSPL.SPLDetails = arlSPLDetail

            lblNumOfInstallment.Text = objSPL.NumOfInstallment
            lblMaxTOPDay.Text = objSPL.MaxTOPDay

            If objSPL.SPLDetails.Count > 0 Then

                'Dim PKDate As Date
                'Dim SPLDDate As Date
                'PKDate = CDate("1 " & lblOrderPlan.Text)
                'For Each objSPLD As SPLDetail In objSPL.SPLDetails
                '    SPLDDate = DateSerial(objSPLD.PeriodYear, objSPLD.PeriodMonth, 1)
                '    If SPLDDate = PKDate Then
                '        objSPLDet = objSPLD
                '    End If
                'Next
                objSPLDet = CType(objSPL.SPLDetails(0), SPLDetail)

                For Each ObjSPLtemp As SPLDetail In objSPL.SPLDetails
                    Dim tmpDate As Date
                    Dim FrmDate As Date
                    tmpDate = DateSerial(ObjSPLtemp.PeriodYear, ObjSPLtemp.PeriodMonth, 1)
                    FrmDate = CDate("1 " & lblOrderPlan.Text)
                    If tmpDate = FrmDate Then
                        objSPLDet = ObjSPLtemp
                        Exit For
                    End If
                Next

                ddlInterest.SelectedValue = IIf(objSPLDet.FreeIntIndicator = 1, 0, 1)
                If objSPLDet.MaxTopIndicator = 0 Then
                    rbtnDate.Checked = True
                    rbtnDay.Checked = False
                    rbtnNone.Checked = False
                    icMaxDate.Value = objSPLDet.MaxTopDate
                ElseIf objSPLDet.MaxTopIndicator = 1 Then
                    rbtnDate.Checked = False
                    rbtnDay.Checked = True
                    rbtnNone.Checked = False
                    txtMaxDay.Text = objSPLDet.MaxTopDay
                ElseIf objSPLDet.MaxTopIndicator = -1 Then
                    rbtnDate.Checked = False
                    rbtnDay.Checked = False
                    rbtnNone.Checked = True
                    txtMaxDay.Text = "0"
                End If
            End If

            _sessionHelper.SetSession(sessArrListPKDetailtoDiscount, Nothing)
            arrListPKdetail = _sessionHelper.GetSession(sessArrListPKDetail)
            arrListPKDetailtoDiscount = _sessionHelper.GetSession(sessArrListPKDetailtoDiscount)
            If IsNothing(arrListPKDetailtoDiscount) OrElse _
                (Not IsNothing(arrListPKDetailtoDiscount) AndAlso arrListPKDetailtoDiscount.Count = 0) Then
                If Not IsNothing(objSPL) Then
                    arrListPKDetailtoDiscount = SetPKDetailtoDiscount(objSPL, arrListPKdetail)
                    _sessionHelper.SetSession(sessArrListPKDetailtoDiscount, arrListPKDetailtoDiscount)
                End If
            End If

            Dim objVTFac As VechileTypeFacade = New VechileTypeFacade(User)
            Dim objVT As VechileType
            Dim IsExistVehicle As Boolean = False
            For Each dtgItem As DataGridItem In dgPKOrderDetail.Items
                Dim lblTypeCode As Label = CType(dtgItem.FindControl("lblType"), Label)
                objVT = objVTFac.Retrieve(lblTypeCode.Text)
                For Each objSPLDetail As SPLDetail In objSPL.SPLDetails
                    Dim tmpDate As Date
                    Dim FrmDate As Date
                    tmpDate = DateSerial(objSPLDetail.PeriodYear, objSPLDetail.PeriodMonth, 1)
                    FrmDate = CDate("1 " & lblOrderPlan.Text)
                    If objVT.ID = objSPLDetail.VechileType.ID And tmpDate = FrmDate Then
                        Dim txtDiskon As TextBox = CType(dtgItem.FindControl("txtDiskon"), TextBox)
                        Dim txtSurcharge As TextBox = CType(dtgItem.FindControl("txtSurcharge"), TextBox)
                        txtDiskon.Text = Format(objSPLDetail.Discount, "#,###")
                        txtSurcharge.Text = Format(objSPLDetail.Surcharge, "#,###")
                        IsExistVehicle = True
                        'Exit For
                    End If
                Next
            Next
            If Not IsExistVehicle Then
                MessageBox.Show("Detail aplikasi " & txtNomorSPL.Text & " tidak ada yang sama dengan dengan detail PK")
                txtNomorSPL.Text = ""
                GoTo jumpResetSPL
            End If
        Else
            MessageBox.Show("SPL " & txtNomorSPL.Text & " tidak terdaftar")
            txtNomorSPL.Text = ""
            GoTo jumpResetSPL
        End If
    End Sub
    Private Sub DefaultSPLControl()

        ddlInterest.Enabled = False
        rbtnDate.Enabled = False
        icMaxDate.Enabled = False
        rbtnDay.Enabled = False
        rbtnNone.Enabled = False

        ddlInterest.SelectedIndex = 1
        rbtnDate.Checked = False
        rbtnDay.Checked = False
        rbtnNone.Checked = True
        icMaxDate.Value = Now
        txtMaxDay.Text = ""
        btnRilis.Enabled = False

        For Each dtgItem As DataGridItem In dgPKOrderDetail.Items
            Dim txtDiskon As TextBox = CType(dtgItem.FindControl("txtDiskon"), TextBox)
            Dim txtSurcharge As TextBox = CType(dtgItem.FindControl("txtSurcharge"), TextBox)
            txtDiskon.Text = "0" 'Format(0, "#,###")
            txtSurcharge.Text = "0" ' Format(0, "#,###")
        Next
    End Sub


    Private Function JaminanForPKH(ByVal oPKH As PKHeader) As Jaminan
        Dim oJFac As JaminanFacade = New JaminanFacade(User)
        Dim crtJ As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dt As Date = DateSerial(oPKH.RequestPeriodeYear, oPKH.RequestPeriodeMonth, 1)
        Dim arlJ As ArrayList = New ArrayList

        crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidFrom", MatchType.LesserOrEqual, dt))
        crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidTo", MatchType.GreaterOrEqual, dt))
        crtJ.opAnd(New Criteria(GetType(Jaminan), "Status", MatchType.Exact, 0)) 'EnumStatusSPL.RetrieveStatus
        arlJ = oJFac.Retrieve(crtJ)
        'Return IIf(arlJ.Count = 0, New Jaminan, CType(arlJ(0), Jaminan))
        For Each oJ As Jaminan In arlJ
            If (" " & oJ.DealerCode).IndexOf(oPKH.Dealer.DealerCode) > 0 And IsJDExistInPKD(oJ, oPKH) Then
                Return oJ
            End If
        Next
        Return New Jaminan

    End Function


    Private Function IsJDExistInPKD(ByVal oJ As Jaminan, ByVal oPKH As PKHeader) As Boolean
        For Each oJD As JaminanDetail In oJ.JaminanDetailIn(oPKH.RequestPeriodeMonth, oPKH.RequestPeriodeYear)
            For Each oPKD As PKDetail In oPKH.PKDetails
                If oJD.VehicleTypeCode = oPKD.VehicleTypeCode And IIf(oJD.Purpose = LookUp.enumPurpose.Semua, True, oJD.Purpose = oPKH.Purpose) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    Private Function DoRilis(ByRef _Msg As String) As Boolean
        Dim result As Boolean = False
        Dim listPK As ArrayList = New ArrayList
        Dim status As New enumStatusPK

        If Not IsNothing(_sessionHelper.GetSession("objPKHeader")) Then
            listPK.Add(_sessionHelper.GetSession("objPKHeader"))
            Dim oldStatus As Integer
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            Dim listPK2 As New System.Collections.ArrayList
            For Each item As PKHeader In listPK
                Dim m2 = New PKHeaderFacade(User).RefreshDiscount(item.ID)
                oldStatus = item.PKStatus
                If item.PKStatus = enumStatusPK.Status.Konfirmasi Then
                    Dim RespQty As Integer = 0
                    If item.PKType = 1 Then
                        '---PK Biasa = 1
                        item = objPKHeaderFacade.Retrieve(item.ID)
                        If item.Dealer.FreePPh22Indicator = 0 Then
                            Dim requestDateTime As New DateTime(item.RequestPeriodeYear, item.RequestPeriodeMonth, 1)
                            If requestDateTime >= item.Dealer.FreePPh22From AndAlso requestDateTime < item.Dealer.FreePPh22To Then
                                item.FreePPh22Indicator = item.Dealer.FreePPh22Indicator
                            Else
                                item.FreePPh22Indicator = 1
                            End If
                        Else
                            item.FreePPh22Indicator = item.Dealer.FreePPh22Indicator
                        End If
                        item.PKStatus = status.Agree
                        For Each d As PKDetail In item.PKDetails
                            If d.VechileColor.ColorCode.ToUpper <> "ZZZZ" Then
                                d.PKHeader = item
                                d = CountPrice(d, item)
                            End If
                        Next
                        Dim totalAlokasi As Integer = 0
                        For Each d As PKDetail In item.PKDetails
                            totalAlokasi += d.ResponseQty
                        Next
                        If totalAlokasi <= 0 Then
                            item.PKStatus = status.Reject
                        End If
                    Else
                        '---PK Khusus = 0
                        item = objPKHeaderFacade.Retrieve(item.ID)
                        If item.Dealer.FreePPh22Indicator = 0 Then
                            Dim requestDateTime As New DateTime(item.RequestPeriodeYear, item.RequestPeriodeMonth, 1)
                            If requestDateTime >= item.Dealer.FreePPh22From AndAlso requestDateTime < item.Dealer.FreePPh22To Then
                                item.FreePPh22Indicator = item.Dealer.FreePPh22Indicator
                            Else
                                item.FreePPh22Indicator = 1
                            End If
                        Else
                            item.FreePPh22Indicator = item.Dealer.FreePPh22Indicator
                        End If
                        Dim IsValidColor As Boolean = True
                        'For Each item As PKDetail In item.PKDetails
                        '    If item.VehicleColorCode.ToUpper = "ZZZZ" Then
                        '        IsValidColor = False
                        '    End If
                        'Next
                        'If IsValidColor Then

                        item.PKStatus = status.Release
                        If item.IsAproveRilis = 1 Then
                            If item.IsAutoApprovedDealer = 1 Then
                                item.PKStatus = status.Agree
                            End If
                        Else
                            If item.SPLNumber = "" Then
                                item.PKStatus = status.Agree
                            End If
                        End If

                        For Each d As PKDetail In item.PKDetails
                            If d.VechileColor.ColorCode.ToUpper <> "ZZZZ" Then
                                'item.PKHeader = item
                                d = CountPrice(d, item)
                                RespQty += d.ResponseQty
                            End If
                        Next
                        Dim totalAlokasi As Integer = 0
                        For Each d As PKDetail In item.PKDetails
                            totalAlokasi += d.ResponseQty
                        Next
                    End If
                End If
                listPK2.Add(item)

                If IsAproveReleaseRequired(item, True) Then 'PK Khusus Konfirmasi
                    If item.IsAproveRilis = enumStatusPK.StatusSetujuRilis.Belum Then
                        _Msg = "Data PK khusus " & item.PKNumber & " belum disetujui untuk rilis, proses gagal"
                        result = False
                        GoTo end_of_for
                    End If
                End If
            Next

            If listPK2.Count = 0 Then
                _Msg = SR.InvalidData("PK")
                result = False
            Else
                objPKHeaderFacade.validatePK(listPK2)
                RecordStatusChangeHistory(listPK2, oldStatus)
                result = True
                Dim pkHeader As PKHeader = CType(listPK2.Item(0), PKHeader)
                Dim _status As String = (CType(oldStatus, enumStatusPK.Status)).ToString
                _Msg = If(pkHeader.PKStatus = status.Reject, String.Format("PK dengan Status {0} berhasil ditolak", _status), String.Format("PK dengan Status {0} berhasil dirilis", _status))
            End If
        Else
            _Msg = SR.InvalidData("PK")
            result = False
        End If
end_of_for:
        Return result
    End Function

    Private Function CountPrice(ByVal objPKDetail As PKDetail, ByVal objPKHeader As PKHeader) As PKDetail
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, objPKHeader.Dealer.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objPKDetail.VechileColor.ID))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("ValidFrom")) Then
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
        Else
            sortColl = Nothing
        End If
        Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)
        Dim objPrice As Price
        Dim rencanaPenebusan As New DateTime(CInt(objPKDetail.PKHeader.RequestPeriodeYear), CInt(objPKDetail.PKHeader.RequestPeriodeMonth), 1, 0, 0, 0)
        For Each item As Price In objPriceArrayList
            If item.ValidFrom <= GetValidFromDocument(objPKHeader) Then '  rencanaPenebusan Then
                objPrice = item
                Exit For
            End If
        Next

        If objPrice Is Nothing Then
            Throw New Exception("Daftar Harga tidak ada untuk material : " & objPKDetail.MaterialNumber)
        End If

        objPKDetail.TargetAmount = Calculation.CountPKVehiclePrice(0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.BasePrice, Double), CType(objPrice.OptionPrice, Double), objPKDetail.ResponseSalesSurcharge)
        objPKDetail.ResponseAmount = Calculation.CountPKVehiclePrice(CType(objPKDetail.ResponseDiscount, Double), CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.BasePrice, Double), CType(objPrice.OptionPrice, Double), objPKDetail.ResponseSalesSurcharge)
        objPKDetail.TargetPPh22 = Calculation.CountPKPPh22(CType(objPrice.BasePrice, Double), 0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.PPh22, Double))
        objPKDetail.ResponsePPh22 = Calculation.CountPKPPh22(CType(objPrice.BasePrice, Double), CType(objPKDetail.ResponseDiscount, Double), CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.PPh22, Double))
        Dim objPKDetailFacade As New PKDetailFacade(User)
        objPKDetailFacade.Update(objPKDetail)
        Return objPKDetail
    End Function

    Private Function GetValidFromDocument(ByVal oPKH As PKHeader)
        Dim Tanggal As DateTime

        Tanggal = New Date(oPKH.RequestPeriodeYear, oPKH.RequestPeriodeMonth, oPKH.RequestPeriodeDay)
        'apakah mmenungkinkan, bulan validasi berbeda dengan bulan Rencana Penebusan
        'info : rencana penebusan ada 2 : 
        '1. utk bulanan, bulan>thismonth
        '2. utk tambahan, bulan yg sama dengan tanggal yg beda
        'jika tanggal validasi bisa berbeda dengan tanggal rencana penebusan (BEFORE OR AFTER), maka code menjadi
        'Tanggal = New Date(Now.Year, Now.Year.Month, 1)

        If CType(oPKH.OrderType, Integer) = CInt(LookUp.EnumJenisPesanan.Bulanan) Then
            Tanggal = DateSerial(Tanggal.Year, Tanggal.Month, 1)
        ElseIf CType(oPKH.OrderType, Integer) = CInt(LookUp.EnumJenisPesanan.Tambahan) Then
            Tanggal = DateSerial(Tanggal.Year, Tanggal.Month, Now.Day)
        End If
        Return Tanggal
    End Function

    Private Function IsAproveReleaseRequired(ByVal objPKHeader As PKHeader, ByVal IsAlreadyChangeInClient As Boolean) As Boolean

        Dim result As Boolean = False

        If objPKHeader.Purpose = 0 And ((objPKHeader.PKStatus = enumStatusPK.Status.Konfirmasi And Not IsAlreadyChangeInClient) Or (objPKHeader.PKStatus = enumStatusPK.Status.Rilis And IsAlreadyChangeInClient)) Then
            If objPKHeader.SPLNumber.Trim <> "" OrElse objPKHeader.FreeIntIndicator = 0 OrElse objPKHeader.MaxTopIndicator <> -1 OrElse IsDetailNotZero(objPKHeader) Then
                result = True
            End If
        End If

        Return result
    End Function

    Private Sub RecordStatusChangeHistory(ByVal arrListPK As ArrayList, ByVal oldStatus As Integer)
        For Each item As PKHeader In arrListPK
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pesanan_Kendaraan), item.PKNumber, oldStatus, item.PKStatus)
        Next
    End Sub

    Private Function IsDetailNotZero(ByVal objPKHeader As PKHeader) As Boolean
        Dim result As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.ID", MatchType.Exact, objPKHeader.ID))

        Dim arrListPKdetail As ArrayList = New PKDetailFacade(User).Retrieve(criterias)
        For Each item As PKDetail In arrListPKdetail
            If item.ResponseDiscount > 0 Or item.ResponseSalesSurcharge > 0 Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function

#End Region

#Region "EventHandler"

    Private Sub LoadData()
        Dim id As Integer = Request.QueryString("master")
        icMaxDate.Value = Now
        'ViewState("Count") = 0
        ViewState("id") = id
        BindToddl()
        RetrievePKHeader(id)
        BindDataGrid(id)
        _enable(False)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckUserPrivilege()
        If Not IsPostBack Then
            UploadFile.Attributes("onchange") = "UploadFiles(this)"

            LoadData()
        End If
        lblSearchPenjelasan.Attributes("onclick") = "ShowPPPenjelasan();"
        lblSearchKonfirmasi.Attributes("onclick") = "ShowPPKonfirmasi();"
        lblSearchSPL.Attributes("onclick") = "ShowPPSPLSelection();"
        txtKTBResponse.Attributes.Add("readonly", "readonly")
        txtDescription.Attributes.Add("readonly", "readonly")
        cbxIsConfirmation.Enabled = False
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ResponsePKViewDetail_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pesanan Kendaraan")
        End If
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.ResponsePKSaveDetail_Privilege)
        dgPKOrderDetail.Columns(14).Visible = SecurityProvider.Authorize(Context.User, SR.ResponsePKUpdateDetail_Privilege)

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label35.Visible = isPriceVisible
        Label36.Visible = isPriceVisible
        lblTotalHargaUnitPD.Visible = isPriceVisible
        dgPKOrderDetail.Columns(7).Visible = isPriceVisible
        dgPKOrderDetail.Columns(8).Visible = isPriceVisible
        dgPKOrderDetail.Columns(12).Visible = isPriceVisible
        dgPKOrderDetail.Columns(13).Visible = isPriceVisible
    End Sub

    Private Sub dgPKOrderDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPKOrderDetail.ItemDataBound
        Dim i As Integer
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As PKDetail = CType(e.Item.DataItem, PKDetail)
            Dim _vehicleColor As VechileColor = New VechileColorFacade(User).Retrieve("ZZZZ")

            If _vehicleColor Is Nothing Then
                i = 0
            Else
                i = _vehicleColor.ID
            End If
            Dim txtDiscount As TextBox = CType(e.Item.FindControl("txtDiskon"), TextBox)
            Dim txtSurcharge As TextBox = CType(e.Item.FindControl("txtSurcharge"), TextBox)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblColorString As Label = CType(e.Item.FindControl("lblColorString"), Label)
                Dim lblColor As Label = CType(e.Item.FindControl("lblColor"), Label)
                Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
                Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
                'Dim lblUnitDealer As Label = CType(e.Item.FindControl("lblUnitDealer"), Label)

                'Dim txtUnit As TextBox = CType(e.Item.FindControl("txtUnit"), TextBox)
                'Dim txtTempUnitDealer As TextBox = CType(e.Item.FindControl("txtTempUnitDealer"), TextBox)
                If i = RowValue.VechileColor.ID Then
                    lblColorString.Text = RowValue.VehicleColorCode
                    lblType.Text = RowValue.VehicleTypeCode
                    lblModel.Text = RowValue.VehicleColorName
                Else
                    Dim EnumColor As VechileColor = RowValue.VechileColor
                    Dim EnumType As VechileType = EnumColor.VechileType
                    Dim EnumModel As VechileModel = EnumType.VechileModel

                    lblColorString.Text = EnumColor.ColorCode
                    lblType.Text = EnumType.VechileTypeCode
                    lblModel.Text = EnumColor.MaterialDescription 'EnumModel.Description
                End If

                e.Item.Cells(11).Text = RowValue.ResponseQty
                e.Item.Cells(7).Text = String.Format("{0:#,###}", RowValue.TargetQty * RowValue.TargetAmount)
                'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                e.Item.Cells(8).Text = String.Format("{0:#,###}", RowValue.TargetQty * RowValue.TargetPPh22 * RowValue.PKHeader.FreePPh22Indicator)
                'e.Item.Cells(8).Text = String.Format("{0:#,###}", RowValue.TargetQty * RowValue.TargetPPh22 * IIf(RowValue.PKHeader.FreePPh22Indicator = 1, 0, 1))
                'End    :RemainModule-DailyPO:FreePPh By:Doni N
                e.Item.Cells(12).Text = String.Format("{0:#,###}", RowValue.ResponseQty * RowValue.ResponseAmount)
                'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                e.Item.Cells(13).Text = String.Format("{0:#,###}", RowValue.ResponseQty * RowValue.ResponsePPh22 * RowValue.PKHeader.FreePPh22Indicator)
                'e.Item.Cells(13).Text = String.Format("{0:#,###}", RowValue.ResponseQty * RowValue.ResponsePPh22 * IIf(RowValue.PKHeader.FreePPh22Indicator = 1, 0, 1))
                'End    :RemainModule-DailyPO:FreePPh By:Doni N
                'TODO GET Default Discount
                Dim defaultDiscount As Integer = GetDefaultDiscount(RowValue.VehicleTypeCode)
                If RowValue.ResponseDiscount > 0 Then
                    txtDiscount.Text = FormatNumber(RowValue.ResponseDiscount, 0, TriState.False, TriState.False, TriState.True)
                Else
                    txtDiscount.Text = defaultDiscount
                End If
                If RowValue.ResponseSalesSurcharge > 0 Then
                    txtSurcharge.Text = FormatNumber(RowValue.ResponseSalesSurcharge, 0, TriState.False, TriState.False, TriState.True)
                Else
                    txtSurcharge.Text = RowValue.ResponseSalesSurcharge.ToString
                End If
                lblColor.Text = RowValue.VechileColor.ID
                'txtUnit.Text = RowValue.ResponseQty
                'txtTempUnitDealer.Text = RowValue.TargetQty
            Else
                If ItemType = ListItemType.EditItem Then
                    SetDtgPesananKendaraanItemEdit(e)
                    Dim lblType As Label = CType(e.Item.FindControl("lblEditType"), Label)
                    Dim lblModel As Label = CType(e.Item.FindControl("lblEditModel"), Label)
                    lblType.Text = RowValue.VehicleTypeCode
                    lblModel.Text = RowValue.VehicleColorName
                    txtDiscount.Text = FormatNumber(RowValue.ResponseDiscount, 0, TriState.False, TriState.False, TriState.False)
                End If
            End If
            Dim lbtnHapus As LinkButton = e.Item.FindControl("lbtnHapus")
            lbtnHapus.Visible = SecurityProvider.Authorize(Context.User, SR.ResponsePKDeleteDetail_Privilege)
            lbtnHapus.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        'If Not e.Item.FindControl("lbtnHapus") Is Nothing Then
        '    CType(e.Item.FindControl("lbtnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        'End If
    End Sub

    Private Function GetDefaultDiscount(ByVal VecTypeCode As String) As Integer
        Dim result As Integer = 0
        Dim objSPL As SPL = New SPLFacade(User).Retrieve(txtNomorSPL.Text)
        Dim id As Integer = Request.QueryString("master")
        Dim objPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(id)
        If (Not objSPL Is Nothing) And (Not objPKHeader Is Nothing) Then
            For Each item As SPLDetail In objSPL.SPLDetails
                If ((item.VechileType.VechileTypeCode = VecTypeCode) And (item.PeriodMonth = objPKHeader.RequestPeriodeMonth) And (item.PeriodYear = objPKHeader.RequestPeriodeYear)) Then
                    Return item.Discount
                End If
            Next
        End If
        Return result
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then
            Return
        End If

        If dgPKOrderDetail.EditItemIndex <> -1 Then
            dgPKOrderDetail.EditItemIndex = -1
            BindDataGrid(CInt(ViewState("id")))
        End If

        If SaveConfirmation() Then
            Dim id As Integer = Request.QueryString("master")
            BindDataGrid(id)
            btnRilis.Enabled = True
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub btnProsesAlokasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim id As Integer = Request.QueryString("master")
        Dim objPKHeader As PKHeader = ConstructPKHeader(id)
        If objPKHeader.PKStatus = enumStatusPK.Status.Rilis Then
            objPKHeader.PKStatus = enumStatusPK.Status.Konfirmasi
            Dim al As ArrayList = PopulateRemindProcess()
            If al.Count > 0 Then
                For Each item As PKDetail In al
                    objPKHeader.PKDetails.Add(item)
                Next
                Dim i As Integer = New PKHeaderFacade(User).Insert(objPKHeader)
                Response.Redirect("../PK/ResponsePKOrder.aspx")
            Else
                'MessageBox.Show("Tidak ada item untuk remind alokasi")
                MessageBox.Show(SR.DataProcessNotFound("PK", "Remind Alokasi"))
            End If
        Else
            'MessageBox.Show("Status PK tidak sama dengan Di kirim.")
            MessageBox.Show(SR.DataNotFoundByStatus("PK", "Rilis"))
        End If
    End Sub

    'Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
    '    'Response.Redirect("../PK\ResponsePKOrder.aspx")
    'End Sub

    Sub dtgPKOrderDetail_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        Select Case (e.CommandName)
            Case "Hapus"
                Try
                    arrListPKdetail = _sessionHelper.GetSession(sessArrListPKDetail)
                    Dim intID As Integer = arrListPKdetail(0).PKHeader.ID
                    If IsNothing(arrListPKdetail) Then arrListPKdetail = New ArrayList
                    Dim oPKDetail As PKDetail = CType(arrListPKdetail(e.Item.ItemIndex), PKDetail)
                    If oPKDetail.ID > 0 Then
                        Dim arrDeleteArrListPKDetail As ArrayList = CType(_sessionHelper.GetSession(sessDeleteArrListPKDetail), ArrayList)
                        If IsNothing(arrDeleteArrListPKDetail) Then arrDeleteArrListPKDetail = New ArrayList
                        arrDeleteArrListPKDetail.Add(oPKDetail)
                        _sessionHelper.SetSession(sessDeleteArrListPKDetail, arrDeleteArrListPKDetail)

                        arrListPKDetailtoDiscount = _sessionHelper.GetSession(sessArrListPKDetailtoDiscount)
                        If Not IsNothing(arrListPKDetailtoDiscount) Then
                            Dim arrDeleteArrListPKDetailtoDiscount As ArrayList = CType(_sessionHelper.GetSession(sessDeleteArrListPKDetailtoDiscount), ArrayList)
                            If IsNothing(arrDeleteArrListPKDetailtoDiscount) Then arrDeleteArrListPKDetailtoDiscount = New ArrayList
                            Dim j As Integer = 0
                            Dim idx As String = ""
                            For Each oPKDetailtoDiscount As PKDetailtoDiscount In arrListPKDetailtoDiscount
                                If oPKDetailtoDiscount.ID > 0 Then
                                    If oPKDetailtoDiscount.PKDetail.ID = oPKDetail.ID Then
                                        arrDeleteArrListPKDetailtoDiscount.Add(oPKDetailtoDiscount)
                                        If idx = "" Then
                                            idx = j.ToString()
                                        Else
                                            idx += "," & j.ToString()
                                        End If
                                    End If
                                End If
                                j += 1
                            Next
                            If idx.Split(",").Length > 0 Then
                                For Each strID As String In idx.Split(",")
                                    arrListPKDetailtoDiscount.RemoveAt(CInt(strID))
                                Next
                            End If
                            _sessionHelper.SetSession(sessDeleteArrListPKDetailtoDiscount, arrDeleteArrListPKDetailtoDiscount)
                        End If
                    End If
                    arrListPKdetail.RemoveAt(e.Item.ItemIndex)
                    _sessionHelper.SetSession(sessArrListPKDetail, arrListPKdetail)
                    BindDataGrid(intID)
                Catch
                    MessageBox.Show("Hapus data gagal")
                End Try
        End Select
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not _sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not _sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(_sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Private Sub lnkReloadSPL_Click(sender As Object, e As EventArgs) Handles lnkReloadSPL.Click
        If txtNomorSPL.Text.Trim <> String.Empty Then
            LoadData()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        DetailSPL()
    End Sub

    Private Sub rbtnDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnDate.CheckedChanged
        If rbtnDate.Checked = True Then
            icMaxDate.Enabled = True
            txtMaxDay.Enabled = False
        Else
            icMaxDate.Enabled = False
        End If
    End Sub

    Private Sub rbtnDay_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnDay.CheckedChanged
        If rbtnDay.Checked = True Then
            txtMaxDay.Enabled = True
            icMaxDate.Enabled = False
        Else
            txtMaxDay.Enabled = False
        End If
    End Sub

    Private Sub rbtnNone_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnNone.CheckedChanged
        If rbtnNone.Checked = True Then
            txtMaxDay.Enabled = False
            icMaxDate.Enabled = False
        End If
    End Sub

    Private Function CheckQtySPL(ByVal listPKDetail As ArrayList, ByVal objSPL As SPL) As Boolean
        Dim list As ArrayList = New ArrayList
        Dim listDetail As ArrayList = New ArrayList
        Dim obj As PKSPL
        Dim match As Boolean = False
        For Each item As PKDetail In listPKDetail
            match = False
            If item.VechileColor.VechileType Is Nothing Then
                Return False
            End If
            If list.Count = 0 Then
                listDetail = New ArrayList
                listDetail.Add(item)
                obj = New PKSPL(item.VechileColor.VechileType.VechileTypeCode, listDetail)
                list.Add(obj)
            Else
                For i As Integer = 0 To list.Count - 1
                    Dim objPKSPL As PKSPL = CType(list(i), PKSPL)
                    If objPKSPL.Code = item.VechileColor.VechileType.VechileTypeCode Then
                        objPKSPL.Val.Add(item)
                        match = True
                        Exit For
                    End If
                Next
                If Not match Then
                    listDetail = New ArrayList
                    listDetail.Add(item)
                    obj = New PKSPL(item.VechileColor.VechileType.VechileTypeCode, listDetail)
                    list.Add(obj)
                End If
            End If
        Next

        If list.Count > 0 Then
            For Each item As PKSPL In list
                Dim listProcess As ArrayList = item.Val
                Dim VechType As VechileType
                Dim pMonth As Integer
                Dim pYear As Integer
                Dim totalQty As Integer = 0
                For Each items As PKDetail In listProcess
                    VechType = items.VechileColor.VechileType
                    pMonth = items.PKHeader.RequestPeriodeMonth
                    pYear = items.PKHeader.RequestPeriodeYear
                    totalQty += items.ResponseQty

                Next
                Dim splQty As Integer = 0
                Dim pkQty As Integer = 0
                pkQty = GetUsedQuantity(objSPL, VechType, pMonth, pYear)
                splQty = GetSPLQuantity(objSPL, VechType, pMonth, pYear)
                If (totalQty > (splQty - pkQty)) Then
                    Vtype = VechType.VechileTypeCode
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Function GetSPLQuantity(ByVal objSPL As SPL, ByVal typeCode As VechileType, ByVal periodMonth As Integer, ByVal periodYear As Integer) As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "SPL.SPLNumber", MatchType.Exact, objSPL.SPLNumber))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodMonth", MatchType.Exact, periodMonth))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodYear", MatchType.Exact, periodYear))
        criterias.opAnd(New Criteria(GetType(SPLDetail), "VechileType.ID", MatchType.Exact, typeCode.ID))
        Dim objSPLDetailFacade As SPLDetailFacade = New SPLDetailFacade(User)
        Dim objSPLList As ArrayList = objSPLDetailFacade.Retrieve(criterias)
        Dim objSPLDetail As SPLDetail
        If objSPLList.Count > 0 Then
            objSPLDetail = CType(objSPLList(0), SPLDetail)
            Return objSPLDetail.Quantity
        End If
        Return 0
    End Function
    Private Function GetUsedQuantity(ByVal objSPL As SPL, ByVal typeCode As VechileType, ByVal periodMonth As Integer, ByVal periodYear As Integer) As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.SPLNumber", MatchType.Exact, objSPL.SPLNumber))
        criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, periodMonth))
        criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, periodYear))
        criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.VechileType.ID", MatchType.Exact, typeCode.ID))
        Dim objPKDetailFacade As PKDetailFacade = New PKDetailFacade(User)
        Dim objPKList As ArrayList = objPKDetailFacade.Retrieve(criterias)
        Dim total As Integer = 0
        If objPKList.Count > 0 Then
            For Each item As PKDetail In objPKList
                total += item.ResponseQty
            Next
        End If
        Return total
    End Function

    Private Sub txtNomorSPL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNomorSPL.TextChanged
        DetailSPL()
    End Sub

    Protected Sub btnRilis_Click(sender As Object, e As EventArgs) Handles btnRilis.Click
        Dim _msg As String

        If DoRilis(_msg) Then
            Dim _PKHeader As PKHeader = _sessionHelper.GetSession("objPKHeader")
            RetrievePKHeader(_PKHeader.ID)
            MessageBox.Show(_msg)
        Else
            MessageBox.Show(_msg)
        End If

    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If (UploadFile.PostedFile Is Nothing) Then
            lbtnDeleteFile_Click(Nothing, Nothing)
            Return
        End If

        If (Not UploadFile.PostedFile Is Nothing) And UploadFile.PostedFile.ContentLength > 0 Then
            Dim ext As String = System.IO.Path.GetExtension(UploadFile.PostedFile.FileName)
            If Not (ext.ToUpper() = ".JPG" _
                    OrElse ext.ToUpper() = ".JPEG" _
                    OrElse ext.ToUpper() = ".TXT" _
                    OrElse ext.ToUpper() = ".DOC" _
                    OrElse ext.ToUpper() = ".DOCX" _
                    OrElse ext.ToUpper() = ".XLS" _
                    OrElse ext.ToUpper() = ".XLSX" _
                    OrElse ext.ToUpper() = ".PNG" _
                    OrElse ext.ToUpper() = ".PDF") Then
                MessageBox.Show("Hanya menerima file format (DOC/XLS/DOCX/XLSX/TXT/PDF/PNG/JPG/JPEG)")
                lbtnDeleteFile_Click(Nothing, Nothing)
                Return
            End If
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If UploadFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                lbtnDeleteFile_Click(Nothing, Nothing)
                Return
            End If

            _sessionHelper.SetSession(FU, UploadFile.PostedFile.InputStream)
            _sessionHelper.SetSession(FU_NAME, UploadFile.PostedFile.FileName)
            lblEvidencePath.Text = UploadFile.PostedFile.FileName
            lblFileName.Text = Path.GetFileName(UploadFile.PostedFile.FileName)
            If lblFileName.Text.Trim <> "" Then
                lbtnDeleteFile.Visible = True
                lnkbtnFileName.Visible = True
            Else
                lbtnDeleteFile.Visible = False
                lnkbtnFileName.Visible = False
            End If
        Else
            MessageBox.Show("Upload file belum diisi\n")
            lbtnDeleteFile_Click(Nothing, Nothing)
            Return
        End If

    End Sub

    Private Sub cbxIsConfirmation_CheckedChanged(sender As Object, e As EventArgs) Handles cbxIsConfirmation.CheckedChanged
        Dim isChecked As Boolean = cbxIsConfirmation.Checked
        Dim isEnabled As Boolean = cbxIsConfirmation.Enabled
        lblUploadDok.Visible = isChecked
        lbltitik2Upload.Visible = isChecked
        UploadFile.Visible = IIf(btnSave.Visible = False, False, isChecked)
        lblFileName.Visible = isChecked
        lnkbtnFileName.Visible = isChecked
        lbtnDeleteFile.Visible = isChecked

        If isChecked Then
            UploadFile.Visible = isEnabled
            If lblFileName.Text.Trim <> "" Then
                If btnSave.Visible AndAlso btnSave.Enabled Then
                    objDealer = _sessionHelper.GetSession("DEALER")
                    If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lbtnDeleteFile.Visible = False
                    Else
                        lbtnDeleteFile.Visible = True
                    End If
                Else
                    lbtnDeleteFile.Visible = False
                End If
                lnkbtnFileName.Visible = True
            Else
                lbtnDeleteFile.Visible = False
                lnkbtnFileName.Visible = False
            End If
        End If
    End Sub

    Private Sub lblFileName_Click(sender As Object, e As EventArgs) Handles lblFileName.Click
        If IsNothing(Request.QueryString("master")) Then Return

        Dim _fileName() As String = lblEvidencePath.Text.Split("\")
        Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("PKFileDirectory") & "\" & _fileName(_fileName.Length - 1)

        Response.Redirect("../Download.aspx?file=" & fileName)
    End Sub

    Private Sub lbtnDeleteFile_Click(sender As Object, e As EventArgs) Handles lbtnDeleteFile.Click
        Try
            _sessionHelper.RemoveSession(FU)
            _sessionHelper.RemoveSession(FU_NAME)
            lblEvidencePath.Text = ""
            lblFileName.Text = ""
            lbtnDeleteFile.Visible = False
            lnkbtnFileName.Visible = False
        Catch
        End Try
    End Sub

#End Region
End Class

