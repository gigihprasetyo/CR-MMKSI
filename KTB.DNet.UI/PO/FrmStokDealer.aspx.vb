#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region


Public Class FrmStokDealer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMO As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerPO As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents icCetakStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icCetakEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlWarna As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNoSO As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoDO As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNomerRangka As System.Web.UI.WebControls.TextBox
    Protected WithEvents lsbStatusFaktur As System.Web.UI.WebControls.ListBox
    Protected WithEvents lsbStatusStock As System.Web.UI.WebControls.ListBox
    Protected WithEvents chkTanggalCetak As System.Web.UI.WebControls.CheckBox
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents chkAll As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btn As System.Web.UI.WebControls.Button
    Protected WithEvents btnAlreadySaled As System.Web.UI.WebControls.Button
    Protected WithEvents btnReverseAlreadySaled As System.Web.UI.WebControls.Button
    Protected WithEvents lblHariIni As System.Web.UI.WebControls.Label
    Protected WithEvents txtTotalRow As System.Web.UI.WebControls.Label
    Protected WithEvents chkTglPengajuanFaktur As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icPengajuanStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPengajuanEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icValidasiStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icValidasiEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkTglValidasiFaktur As System.Web.UI.WebControls.CheckBox

    Protected WithEvents chkTglKonfirmasiFaktur As System.Web.UI.WebControls.CheckBox
    Protected WithEvents trPengajuan As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents icKnfirmasiStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icKnfirmasiEnd As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Varibles"

#End Region

#Region "Custom Methods"

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Dealer_stock_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Stock Dealer")
        End If
    End Sub

    Private Sub Initialization()
        viewstate.Add("SortColumn", "DealerCode")
        viewstate.Add("SortDirection", Sort.SortDirection.ASC)

        btnCari.Attributes.Add("OnClick", "return IsCheckedDateDuration();")
        BindDdlCategory()
        BindDdlType()
        BindDdlWarna()
        BindlsbStatusFaktur()
        BindlsbStatusStock()
        chkTanggalCetak.Checked = True
    End Sub

    Private Sub BindDdlCategory()
        Dim objCFac As CategoryFacade = New CategoryFacade(User)
        Dim crtC As CriteriaComposite
        Dim arlC As ArrayList = New ArrayList
        Dim srtC As SortCollection = New SortCollection

        crtC = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        srtC.Add(New Sort(GetType(Category), "CategoryCode"))
        arlC = objCFac.Retrieve(crtC, srtC)
        ddlKategori.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each objC As Category In arlC
            ddlKategori.Items.Add(New ListItem(objC.CategoryCode, objC.ID))
        Next
        ddlKategori.SelectedValue = -1
        FillModel(-1)
    End Sub


    Private Sub FillModel(ByVal CategoryID As Integer)
        ddlModel.Items.Clear()
        If CategoryID > -1 Then
            Dim objModel As New VechileModelFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileModel), "Category", CategoryID))
            ddlModel.DataSource = objModel.RetrieveList("Description", Sort.SortDirection.ASC, crit)
            ddlModel.DataTextField = "Description"
            ddlModel.DataValueField = "ID"
            ddlModel.DataBind()
            ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlModel.Items.Insert(0, New ListItem("Pilih Kategori", -1))
        End If
    End Sub

    Private Sub BindDdlType()
        ddlTipe.Items.Clear()
        ddlTipe.Items.Add(New ListItem("Silahkan Pilih", -1))
        If ddlKategori.SelectedValue = -1 Then
            ddlTipe.SelectedValue = -1
            BindDdlWarna()
            Exit Sub
        End If

        Dim objVTFac As VechileTypeFacade = New VechileTypeFacade(User)
        Dim crtVT As CriteriaComposite
        Dim arlVT As ArrayList = New ArrayList
        Dim srtVT As SortCollection = New SortCollection

        crtVT = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtVT.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
        crtVT.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        srtVT.Add(New Sort(GetType(VechileType), "VechileTypeCode"))
        arlVT = objVTFac.Retrieve(crtVT, srtVT)
        For Each objvt As VechileType In arlVT
            ddlTipe.Items.Add(New ListItem(objvt.VechileTypeCode, objvt.ID))
        Next
        ddlTipe.SelectedValue = -1
        BindDdlWarna()
    End Sub

    Private Sub BindDdlWarna()
        ddlWarna.Items.Clear()

        ddlWarna.Items.Add(New ListItem("Silahkan Pilih", -1))

        If ddlTipe.SelectedValue = -1 Then
            ddlWarna.SelectedValue = -1
            Exit Sub
        End If

        Dim objVCFac As VechileColorFacade = New VechileColorFacade(User)
        Dim crtVC As CriteriaComposite
        Dim arlVC As ArrayList = New ArrayList
        Dim srtVC As SortCollection = New SortCollection

        crtVC = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtVC.opAnd(New Criteria(GetType(VechileColor), "VechileType.ID", MatchType.Exact, CType(ddlTipe.SelectedValue, Integer)))
        srtVC.Add(New Sort(GetType(VechileColor), "ColorCode"))
        arlVC = objVCFac.Retrieve(crtVC, srtVC)
        For Each objvt As VechileColor In arlVC
            ddlWarna.Items.Add(New ListItem(objvt.ColorCode, objvt.ID))
        Next
        ddlWarna.SelectedValue = -1
    End Sub

    Private Sub BindlsbStatusFaktur()
        'Start  :OldStatus
        'lsbStatus.Items.Add(New ListItem("Dealer Stok", 0))
        'lsbStatus.Items.Add(New ListItem("Invoice Open", 1))
        'lsbStatus.Items.Add(New ListItem("Sudah Terjual", 2))
        'End    :OldStatus
        With lsbStatusFaktur.Items
            .Clear()
            '.Add(New ListItem("Belum Terjual", "-1")) 'FakturStatus=-1 ;Old->ChassisMaster.Name1=""->Tidak punya Faktur
            '.Add(New ListItem("Sudah Terjual", "-2")) 'FakturStatus=-2
            '.Add(New ListItem("Sudah Terjual", CType(EnumChassisMaster.FakturStatus.Baru, Short))) -Punya Faktur & FakturStatus=0 ->Baru

            .Add(New ListItem("", -1))

            .Add(New ListItem(EnumChassisMaster.FakturStatus.Baru.ToString, CType(EnumChassisMaster.FakturStatus.Baru, Short)))
            .Add(New ListItem(EnumChassisMaster.FakturStatus.Validasi.ToString, CType(EnumChassisMaster.FakturStatus.Validasi, Short)))
            .Add(New ListItem(EnumChassisMaster.FakturStatus.Konfirmasi.ToString, CType(EnumChassisMaster.FakturStatus.Konfirmasi, Short)))
            .Add(New ListItem(EnumChassisMaster.FakturStatus.Proses.ToString, CType(EnumChassisMaster.FakturStatus.Proses, Short)))
            .Add(New ListItem(EnumChassisMaster.FakturStatus.Selesai.ToString, CType(EnumChassisMaster.FakturStatus.Selesai, Short)))
        End With

    End Sub

    Private Sub BindlsbStatusStock()
        'Start  :OldStatus
        'lsbStatus.Items.Add(New ListItem("Dealer Stok", 0))
        'lsbStatus.Items.Add(New ListItem("Invoice Open", 1))
        'lsbStatus.Items.Add(New ListItem("Sudah Terjual", 2))
        'End    :OldStatus
        With lsbStatusStock.Items
            .Clear()
            .Add(New ListItem("Belum Terjual", "0")) 'FakturStatus=-1 ;Old->ChassisMaster.Name1=""->Tidak punya Faktur
            .Add(New ListItem("Sudah Terjual", "2")) 'FakturStatus=-2
        End With
    End Sub
    Private Function GetCriteria() As CriteriaComposite
        'Dim crt As CriteriaComposite
        'Dim str As String
        'Dim sql As String


        'crt = New CriteriaComposite(New Criteria(GetType(v_DealerStockID), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ''Dealer.ID
        'If txtKodeDealer.Text.Trim <> "" Then
        '    str = txtKodeDealer.Text.Trim
        '    If str.EndsWith(";") Then str = str.Substring(0, str.Length - 2)
        '    str = "'" & str.Replace(";", "','") & "'"
        '    sql = "select id from Dealer where DealerCode in (" & str & ")"
        '    crt.opAnd(New Criteria(GetType(v_DealerStockID), "dID", MatchType.InSet, "(" & sql & ")"))
        'End If

        ''ChassisMaster.ID
        'sql = ""
        'If chkTanggalCetak.Checked Then sql = sql & IIf(sql = "", "", " and ") & " (DODate>='" & Format(icCetakStart.Value, "yyyyMMdd") & "' and DODate<='" & Format(icCetakEnd.Value, "yyyyMMdd") & "') "
        'If txtNoSO.Text.Trim <> "" Then sql = sql & IIf(sql = "", "", " and ") & " SONumber like '%" & txtNoSO.Text.Trim & "%' "
        'If txtNoDO.Text.Trim <> "" Then sql = sql & IIf(sql = "", "", " and ") & " DONumber like '%" & txtNoDO.Text.Trim & "%' "
        'str = ""
        'For Each liStatus As ListItem In lsbStatus.Items
        '    If liStatus.Selected Then
        '        If liStatus.Text.Trim.ToUpper = "INVOICE OPEN" Then
        '            str = str & IIf(str = "", "", " or ") & " FakturStatus=3 or FakturStatus=4 "
        '        Else
        '            str = str & IIf(str = "", "", " or ") & " FakturStatus=0 or FakturStatus=1 or FakturStatus=2 "
        '        End If
        '    End If
        'Next
        'If sql <> "" Or str <> "" Then
        '    sql = "select id from ChassisMaster where 1=1 " & IIf(sql.Trim <> "", " and ", "") & sql & IIf(str <> "", " and (" & str & ") ", "")
        'End If

        'If sql <> "" Then crt.opAnd(New Criteria(GetType(v_DealerStockID), "ID", MatchType.InSet, "(" & sql & ")"))

        ''EndCustomer.ID
        'If chkTanggalFaktur.Checked Then
        '    sql = "select id from EndCustomer where OpenFakturDate>='" & Format(icFakturStart.Value, "yyyyMMdd") & "' and OpenFakturDate<='" & Format(icFakturEnd.Value, "yyyyMMdd") & "'"
        '    crt.opAnd(New Criteria(GetType(v_DealerStockID), "ecID", MatchType.InSet, "(" & sql & ")"))
        'End If
        ''Category.ID
        'If ddlKategori.SelectedValue <> -1 Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockID), "catID", MatchType.Exact, ddlKategori.SelectedValue))
        'End If
        ''VechileType.ID
        'If ddlTipe.SelectedValue <> -1 Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockID), "vtID", MatchType.Exact, ddlTipe.SelectedValue))
        'End If
        ''VechileColor.ID
        'sql = ""
        'If ddlWarna.SelectedValue <> -1 Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockID), "vcID", MatchType.Exact, ddlWarna.SelectedValue))
        'End If

        'Return crt



    End Function

    Private Function GetCriteriaNew()
        'Dim crt As CriteriaComposite
        'Dim str As String
        'Dim nSelected As Integer = 0
        'Dim FakturStatus As String = ""

        'crt = New CriteriaComposite(New Criteria(GetType(v_DealerStockDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If txtKodeDealer.Text.Trim <> "" Then
        '    str = txtKodeDealer.Text.Trim
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "DealerCode", MatchType.InSet, "('" & str.Replace(";", "','") & "')"))
        'End If
        'If chkTanggalCetak.Checked Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "DODate", MatchType.GreaterOrEqual, Format(icCetakStart.Value, "MM/dd/yyyy")))
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "DODate", MatchType.LesserOrEqual, Format(icCetakEnd.Value, "MM/dd/yyyy")))
        'End If
        'If chkTanggalFaktur.Checked Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "OpenFakturDate", MatchType.GreaterOrEqual, Format(icFakturStart.Value, "yyyyMMdd")))
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "OpenFakturDate", MatchType.LesserOrEqual, Format(icFakturEnd.Value, "MM/dd/yyyy")))
        'End If
        'If ddlKategori.SelectedValue <> -1 Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "CategoryCode", MatchType.Exact, ddlKategori.SelectedItem.Text))
        'End If
        'If ddlTipe.SelectedValue <> -1 Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "VechileTypeCode", MatchType.Exact, ddlTipe.SelectedItem.Text))
        'End If
        'If ddlWarna.SelectedValue <> -1 Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "ColorCode", MatchType.Exact, ddlTipe.SelectedItem.Text))
        'End If
        'If txtNoSO.Text.Trim <> "" Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "SONumber", MatchType.Partial, txtNoSO.Text.Trim))
        'End If
        'If txtNoDO.Text.Trim <> "" Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "DONumber", MatchType.Partial, txtNoDO.Text.Trim))
        'End If
        'For Each lsbItem As ListItem In lsbStatus.Items
        '    If lsbItem.Selected Then
        '        nSelected = nSelected + 1
        '        FakturStatus = lsbItem.Text
        '    End If
        'Next
        'If nSelected = 1 Then
        '    crt.opAnd(New Criteria(GetType(v_DealerStockDetail), "FakturStatus", MatchType.Exact, FakturStatus))
        'End If
        'Return crt
    End Function

    Private Function GetCriteriaVDS() As CriteriaComposite
        Dim crt As CriteriaComposite
        Dim str As String
        Dim nSelected As Integer = 0
        Dim FakturStatus As String = ""

        crt = New CriteriaComposite(New Criteria(GetType(v_DealerStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeDealer.Text.Trim <> "" Then
            str = txtKodeDealer.Text.Trim
            crt.opAnd(New Criteria(GetType(v_DealerStock), "DealerCode", MatchType.InSet, "('" & str.Replace(";", "','") & "')"))
        End If
        If chkTanggalCetak.Checked Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "DODate", MatchType.GreaterOrEqual, Format(icCetakStart.Value, "MM/dd/yyyy")))
            crt.opAnd(New Criteria(GetType(v_DealerStock), "DODate", MatchType.LesserOrEqual, Format(icCetakEnd.Value, "MM/dd/yyyy")))
        End If
        If chkTglPengajuanFaktur.Checked Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "OpenFakturDate", MatchType.GreaterOrEqual, Format(icPengajuanStart.Value, "yyyyMMdd")))
            crt.opAnd(New Criteria(GetType(v_DealerStock), "OpenFakturDate", MatchType.LesserOrEqual, Format(icPengajuanEnd.Value, "MM/dd/yyyy")))
        End If
        If chkTglValidasiFaktur.Checked Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "ValidateTime", MatchType.GreaterOrEqual, Format(icValidasiStart.Value, "yyyyMMdd")))
            crt.opAnd(New Criteria(GetType(v_DealerStock), "ValidateTime", MatchType.LesserOrEqual, Format(icValidasiEnd.Value.AddDays(1), "MM/dd/yyyy")))
        End If

        If chkTglKonfirmasiFaktur.Checked Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "ConfirmFakturDate", MatchType.GreaterOrEqual, Format(icKnfirmasiStart.Value, "yyyyMMdd")))
            crt.opAnd(New Criteria(GetType(v_DealerStock), "ConfirmFakturDate", MatchType.Lesser, Format(icKnfirmasiEnd.Value.AddDays(1), "yyyyMMdd")))
        End If


        If ddlKategori.SelectedValue <> -1 Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "CategoryID", MatchType.Exact, ddlKategori.SelectedValue))
        End If
        If ddlModel.SelectedValue <> -1 Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "ModelDescription", MatchType.Exact, ddlModel.SelectedItem.Text))
        End If
        If ddlTipe.SelectedValue <> -1 Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "VechileTypeCode", MatchType.Exact, ddlTipe.SelectedItem.Text))
        End If
        If ddlWarna.SelectedValue <> -1 Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "ColorCode", MatchType.Exact, ddlWarna.SelectedItem.Text))
        End If
        If txtNoSO.Text.Trim <> "" Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "SONumber", MatchType.[Partial], txtNoSO.Text.Trim))
        End If
        If txtNoDO.Text.Trim <> "" Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "DONumber", MatchType.[Partial], txtNoDO.Text.Trim))
        End If
        If txtNomerRangka.Text.Trim <> "" Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "ChassisNumber", MatchType.[Partial], txtNomerRangka.Text.Trim))

        End If

        'Start  :Old Status
        'Dim counter As Integer = 0
        'For Each lsbItem As ListItem In lsbStatus.Items
        '    counter += 1
        '    If lsbItem.Selected And counter <> 3 Then 'Tidak Termasuk status sudah terjual
        '        nSelected = nSelected + 1
        '        FakturStatus = lsbItem.Text
        '    End If
        'Next

        'If nSelected = 1 Then 'Salah Satu dari 2 status awal dipilih 
        '    If lsbStatus.Items(2).Selected Then
        '        If FakturStatus.Trim.ToUpper = "INVOICE OPEN" Then
        '            crt.opAnd(New Criteria(GetType(v_DealerStock), "FakturStatus", MatchType.InSet, "(3,4)"), "(", True)
        '        Else
        '            crt.opAnd(New Criteria(GetType(v_DealerStock), "FakturStatus", MatchType.NotInSet, "3,4"), "(", True)
        '        End If
        '        crt.opOr(New Criteria(GetType(v_DealerStock), "AlreadySaled", MatchType.Exact, 2), ")", False)

        '    Else
        '        If FakturStatus.Trim.ToUpper = "INVOICE OPEN" Then
        '            crt.opAnd(New Criteria(GetType(v_DealerStock), "FakturStatus", MatchType.InSet, "(3,4)"))
        '        Else
        '            crt.opAnd(New Criteria(GetType(v_DealerStock), "FakturStatus", MatchType.NotInSet, "3,4"))
        '        End If
        '        crt.opAnd(New Criteria(GetType(v_DealerStock), "AlreadySaled", MatchType.No, 2))
        '    End If
        'Else
        '    If lsbStatus.Items(2).Selected Then
        '        crt.opAnd(New Criteria(GetType(v_DealerStock), "AlreadySaled", MatchType.Exact, 2))
        '    Else
        '        crt.opAnd(New Criteria(GetType(v_DealerStock), "AlreadySaled", MatchType.No, 2))
        '    End If
        'End If
        'End    :Old Status
        Dim nSelectedStatus As Integer = 0
        Dim aStatusCrits As New ArrayList
        Dim oSC As clsStatusCriteria
        For i As Integer = 0 To Me.lsbStatusFaktur.Items.Count - 1
            If Me.lsbStatusFaktur.Items(i).Selected Then
                oSC = New clsStatusCriteria
                oSC.FieldName = "FakturStatus" ' IIf(i = 0, "Name1", "FakturStatus")
                oSC.FieldValue = Me.lsbStatusFaktur.Items(i).Value
                aStatusCrits.Add(oSC)
            End If
        Next
        If aStatusCrits.Count > 0 Then AppendStatusCriteria(crt, aStatusCrits)

        'do the same thing to lsbStatusStock
        aStatusCrits = New ArrayList
        For i As Integer = 0 To Me.lsbStatusStock.Items.Count - 1
            If Me.lsbStatusStock.Items(i).Selected Then
                oSC = New clsStatusCriteria
                oSC.FieldName = "AlreadySaled"
                oSC.FieldValue = Me.lsbStatusStock.Items(i).Value
                aStatusCrits.Add(oSC)
            End If
        Next
        If aStatusCrits.Count > 0 Then AppendStatusCriteria(crt, aStatusCrits)

        If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
            crt.opAnd(New Criteria(GetType(v_DealerStock), "DealerCode", MatchType.Exact, CType(Session("Dealer"), Dealer).DealerCode), "(", True)
            'allow current dealer see other dealer data as long as the dealer has the same dealergroup
            Dim Sql As String = String.Empty
            Dim oUI As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)

            Sql &= " select d.DealerCode from UserOrgAssignment uoa inner join Dealer d on d.ID=uoa.OrganizationID "
            Sql &= " and uoa.UserID=" & oUI.ID.ToString
            crt.opOr(New Criteria(GetType(v_DealerStock), "DealerCode", MatchType.InSet, "(" & Sql & ")"), ")", False)
        End If

        Return crt
    End Function

    Private Sub BindDTGNew(ByVal PgIdx As Integer)
        'Dim objVDSDFac As v_DealerStockDetailFacade = New v_DealerStockDetailFacade(User)
        'Dim arlVDSD As ArrayList = New ArrayList
        'Dim TotRow As Integer
        'Dim srtColumn As String
        'Dim srtDirection As Sort.SortDirection


        'srtColumn = viewstate.Item("SortColumn")
        'srtDirection = viewstate.Item("SortDirection")

        'arlVDSD = objVDSDFac.RetrieveActiveList(GetCriteriaNew(), PgIdx + 1, dtgMain.PageSize, TotRow, srtColumn, srtDirection)
        'dtgMain.DataSource = arlVDSD
        'dtgMain.VirtualItemCount = TotRow
        'dtgMain.DataBind()
    End Sub

    Private Sub BindDTGVDS(ByVal PgIdx As Integer)
        Dim objVDSFac As v_DealerStockFacade = New v_DealerStockFacade(User)
        Dim arlVDS As ArrayList = New ArrayList
        Dim TotRow As Integer
        Dim srtColumn As String
        Dim srtDirection As Sort.SortDirection


        srtColumn = viewstate.Item("SortColumn")
        srtDirection = viewstate.Item("SortDirection")

        arlVDS = objVDSFac.RetrieveActiveList(GetCriteriaVDS(), PgIdx + 1, dtgMain.PageSize, TotRow, srtColumn, srtDirection)
        dtgMain.DataSource = arlVDS
        dtgMain.CurrentPageIndex = PgIdx
        dtgMain.VirtualItemCount = TotRow
        txtTotalRow.Text = FormatNumber(TotRow, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        dtgMain.DataBind()
    End Sub

    Private Function CheckMandatoryField() As Boolean
        Dim _retVal As Boolean = False
        If chkTanggalCetak.Checked Then
            _retVal = True
        Else
            _retVal = False
        End If
        Return _retVal
    End Function
    Private Sub BindDTG(ByVal PgIdx As Integer)
        If chkTanggalCetak.Checked Then
            If icCetakStart.Value > icCetakEnd.Value Then
                MessageBox.Show("Periode tanggal cetak salah")
                Exit Sub
            End If
        End If
        

        If chkTglValidasiFaktur.Checked Then
            If icValidasiStart.Value > icValidasiEnd.Value Then
                MessageBox.Show("Periode tanggal validasi faktur salah")
                Exit Sub
            End If
        End If

       

        'BindDTGNew(PgIdx)
        BindDTGVDS(PgIdx)



        Exit Sub


        'Dim objVDSIFac As v_DealerStockIDFacade = New v_DealerStockIDFacade(User)
        'Dim arlVDSI As ArrayList = New ArrayList
        'Dim objVDSFac As v_DealerStockFacade = New v_DealerStockFacade(User)
        'Dim arlVDS As ArrayList = New ArrayList
        'Dim arlVDSToDisplay As ArrayList = New ArrayList
        'Dim TotRow As Integer
        'Dim srtColumn As String
        'Dim srtDirection As Sort.SortDirection



        'srtColumn = viewstate.Item("SortColumn")
        'srtDirection = viewstate.Item("SortDirection")
        'dtgMain.CurrentPageIndex = PgIdx
        'arlVDSI = objVDSIFac.RetrieveByCriteria(GetCriteria(), PgIdx + 1, dtgMain.PageSize, TotRow, srtColumn, srtDirection)
        'For Each objVDSI As v_DealerStockID In arlVDSI
        '    arlVDSToDisplay.Add(objVDSFac.Retrieve(GetVDSCriteriaFromVDSI(objVDSI))(0))
        'Next
        'dtgMain.DataSource = arlVDSToDisplay
        'dtgMain.VirtualItemCount = TotRow
        'dtgMain.DataBind()
    End Sub

    'Private Function GetVDSCriteriaFromVDSI(ByVal objVDSI As v_DealerStockID) As CriteriaComposite
    '    'Dim crt As CriteriaComposite

    '    'crt = New CriteriaComposite(New Criteria(GetType(v_DealerStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    'crt.opAnd(New Criteria(GetType(v_DealerStock), "ID", MatchType.Exact, objVDSI.ID))
    '    'crt.opAnd(New Criteria(GetType(v_DealerStock), "dID", MatchType.Exact, objVDSI.dID))
    '    'crt.opAnd(New Criteria(GetType(v_DealerStock), "vtID", MatchType.Exact, objVDSI.vtID))
    '    'crt.opAnd(New Criteria(GetType(v_DealerStock), "vcID", MatchType.Exact, objVDSI.vcID))
    '    'crt.opAnd(New Criteria(GetType(v_DealerStock), "pkhID", MatchType.Exact, objVDSI.pkhID))
    '    'crt.opAnd(New Criteria(GetType(v_DealerStock), "cID", MatchType.Exact, objVDSI.custID))
    '    'crt.opAnd(New Criteria(GetType(v_DealerStock), "ecID", MatchType.Exact, objVDSI.ecID))
    '    'Return crt

    'End Function

    Private Function GetAllData() As ArrayList
        Dim objVDSFac As v_DealerStockFacade = New v_DealerStockFacade(User)
        Dim arlVDS As ArrayList = New ArrayList
        Dim srtColumn As String
        Dim srtDirection As Sort.SortDirection
        Dim srtVDS As SortCollection = New SortCollection

        srtColumn = viewstate.Item("SortColumn")
        srtDirection = viewstate.Item("SortDirection")
        srtVDS.Add(New Sort(GetType(v_DealerStock), srtColumn, srtDirection))

        arlVDS = objVDSFac.Retrieve(GetCriteriaVDS, srtVDS)
        Return arlVDS

        'Dim objVDSDFac As v_DealerStockDetailFacade = New v_DealerStockDetailFacade(User)
        'Dim arlVDSD As ArrayList = New ArrayList
        'Dim srtColumn As String
        'Dim srtDirection As Sort.SortDirection
        'Dim srtVDSD As SortCollection = New SortCollection

        'srtColumn = viewstate.Item("SortColumn")
        'srtDirection = viewstate.Item("SortDirection")
        'srtVDSD.Add(New Sort(GetType(v_DealerStockDetail), srtColumn, srtDirection))

        'arlVDSD = objVDSDFac.Retrieve(GetCriteriaNew(), srtVDSD)
        'Return arlVDSD

        'Exit Function
        'Dim objVDSIDFac As v_DealerStockIDFacade = New v_DealerStockIDFacade(User)
        'Dim objVDSFac As v_DealerStockFacade = New v_DealerStockFacade(User)
        'Dim arlVDSID As ArrayList = New ArrayList
        'Dim arlVDS As ArrayList = New ArrayList

        'arlVDSID = objVDSIDFac.Retrieve(GetCriteria)
        'For Each objVDSID As v_DealerStockID In arlVDSID
        '    arlVDS.Add(objVDSFac.Retrieve(GetVDSCriteriaFromVDSI(objVDSID))(0))
        'Next
        'Return arlVDS

    End Function
    Private Sub DoDownload()
        Dim sFileName As String
        Dim fullFileName As String
        sFileName = "DealerStock" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls


        '-- Temp file must be a randomly named file!
        fullFileName = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(fullFileName)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(fullFileName, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteData(sw, GetAllData)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim i As Integer = 0

        If Not IsNothing(data) Then
            '-- Write column header
            'itemLine.Remove(0, itemLine.Length)  '-- Empty line
            'itemLine.Append("Dealer Stock")
            'sw.WriteLine(itemLine)
            'itemLine.Remove(0, itemLine.Length)
            'itemLine.Append(" " & tab & tab)
            'sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("SearchTerm1" & tab) 'NewInserted
            itemLine.Append("GroupName" & tab) 'NewInserted
            itemLine.Append("Model" & tab) 'new 151126
            itemLine.Append("Tipe" & tab)
            itemLine.Append("Warna" & tab)
            itemLine.Append("Nomor Rangka" & tab)
            itemLine.Append("Production Year" & tab) 'New on 20140414
            itemLine.Append("Description" & tab) 'NewInserted
            itemLine.Append("Nama PK" & tab)
            itemLine.Append("Nama Konsumen" & tab)
            itemLine.Append("ProvinceName" & tab) 'NewInserted
            itemLine.Append("Tgl Cetak DO" & tab)
            'itemLine.Append("Tgl Pengajuan" & tab)
            itemLine.Append("Tgl Konfirmasi Faktur" & tab)
            itemLine.Append("Tgl Validasi Faktur" & tab) 'new 151126
            itemLine.Append("Cara Pembayaran" & tab)
            itemLine.Append("Nomor SO" & tab)
            itemLine.Append("Nomor DO" & tab)
            itemLine.Append("Nomor Faktur" & tab) 'NewInserted_20100309
            itemLine.Append("Status Faktur" & tab) 'new 151126
            sw.WriteLine(itemLine.ToString())
            'lblJr.Text = 
            'lblSr.Text = GetPassDateByCategory(passClassCourse, "SENIOR")
            'lblMr.Text = GetPassDateByCategory(passClassCourse, "MASTER")
            i = 1
            'Dim oDFac As DealerFacade = New DealerFacade(User)
            'Dim oVTfac As VechileTypeFacade = New VechileTypeFacade(User)
            For Each item As v_DealerStock In data
                'Dim oD As Dealer = oDFac.Retrieve(item.DealerCode)
                'Dim oVT As VechileType = oVTfac.Retrieve(item.VechileTypeCode)

                itemLine.Remove(0, itemLine.Length)   '-- Empty line
                itemLine.Append(i & tab)
                'If item.FakturStatus = 3 Or item.FakturStatus = 4 Then
                '    itemLine.Append("Invoice Open" & tab)
                'Else
                '    itemLine.Append("Dealer Stok" & tab)
                'End If
                Dim sFakturStatus As String = String.Empty
                If item.FakturStatus = -1 Then
                    sFakturStatus = Me.lsbStatusFaktur.Items(0).Text
                Else
                    sFakturStatus = EnumChassisMaster.FakturStatusDesc(item.FakturStatus)
                End If
                itemLine.Append(IIf((item.AlreadySaled = 0), "Belum Terjual", "Sudah Terjual") & tab)

                itemLine.Append(item.DealerCode & tab)
                itemLine.Append(item.SearchTerm1 & tab) 'itemLine.Append(oD.SearchTerm1 & tab)
                itemLine.Append(item.GroupName & tab) 'itemLine.Append(oD.DealerGroup.GroupName & tab)
                itemLine.Append(item.ModelDescription & tab)
                itemLine.Append(item.VechileTypeCode & tab)
                itemLine.Append(item.ColorCode & tab)
                itemLine.Append(item.ChassisNumber & tab)
                'itemLine.Append(item.ChassisMaster.ProductionYear & tab) 'new on 20140414
                itemLine.Append(item.ProductionYear & tab) 'new on 20140414
                itemLine.Append(item.Description & tab) 'itemLine.Append(oVT.Description & tab)
                itemLine.Append(item.ProjectName & tab)
                itemLine.Append(item.Name1 & tab)
                itemLine.Append(item.ProvinceName & tab) 'itemLine.Append(oD.Province.ProvinceName & tab)
                itemLine.Append(IIf(item.DODate < CDate("1/1/1945"), "-", item.DODate.ToString("yyyy/MM/dd")) & tab)
                'itemLine.Append(IIf(item.OpenFakturDate < CDate("1/1/1945"), "-", item.OpenFakturDate.ToString("yyyy/MM/dd")) & tab)
                itemLine.Append(IIf(item.ConfirmFakturDate < CDate("1/1/1945"), "-", item.ConfirmFakturDate.ToString("yyyy/MM/dd")) & tab)
                itemLine.Append(IIf(item.ValidateTime < CDate("1/1/1945"), "-", item.ValidateTime.ToString("yyyy/MM/dd")) & tab)
                itemLine.Append(IIf(item.TOPID = 1, "COD", "TOP") & tab)
                itemLine.Append(item.SONumber & tab)
                itemLine.Append(item.DONumber & tab)
                itemLine.Append(item.FakturNumber & tab) 'NewInserted_20100309
                itemLine.Append(sFakturStatus & tab) '151126

                sw.WriteLine(itemLine.ToString())

                i = i + 1
            Next
        End If
    End Sub

    Private Sub AppendStatusCriteria(ByRef crt As CriteriaComposite, ByVal aStatusCrits As ArrayList)
        Dim oSC As clsStatusCriteria
        If aStatusCrits.Count = 1 Then
            oSC = CType(aStatusCrits(0), clsStatusCriteria)
            crt.opAnd(New Criteria(GetType(v_DealerStock), oSC.FieldName, MatchType.Exact, oSC.FieldValue))
        Else
            For i As Integer = 0 To aStatusCrits.Count - 1
                oSC = CType(aStatusCrits(i), clsStatusCriteria)
                If i = 0 Then
                    crt.opAnd(New Criteria(GetType(v_DealerStock), oSC.FieldName, MatchType.Exact, oSC.FieldValue), "(", True)
                ElseIf i > 0 AndAlso i < aStatusCrits.Count - 1 Then
                    crt.opOr(New Criteria(GetType(v_DealerStock), oSC.FieldName, MatchType.Exact, oSC.FieldValue))
                ElseIf i = aStatusCrits.Count - 1 Then
                    crt.opOr(New Criteria(GetType(v_DealerStock), oSC.FieldName, MatchType.Exact, oSC.FieldValue), ")", False)
                End If
            Next
        End If
    End Sub

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            Initialization()
            BindDTG(0)

            If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
                btnAlreadySaled.Visible = False
                btnReverseAlreadySaled.Visible = False
            Else
                btnAlreadySaled.Visible = True
                btnReverseAlreadySaled.Visible = False
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindDTG(0)
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblDODate As Label = e.Item.FindControl("lblDODate")
            Dim lblRequestDate As Label = e.Item.FindControl("lblRequestDate")
            Dim lblFakturStatus As Label = e.Item.FindControl("lblFakturStatus")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            Dim lblID As Label = e.Item.FindControl("lblID")
            Dim ChkSaled As CheckBox = e.Item.FindControl("ChkSaled")
            Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
            Dim lblName1 As Label = e.Item.FindControl("lblName1")

            Dim lblConfirmDate As Label = e.Item.FindControl("lblConfirmDate")

            Dim objDFac As DealerFacade = New DealerFacade(User)
            Dim objD As Dealer
            lblNo.Text = (dtgMain.PageSize * dtgMain.CurrentPageIndex) + e.Item.ItemIndex + 1



            If CDate(lblDODate.Text) < CDate("1/1/1945") Then lblDODate.Text = "-"
            If CDate(lblRequestDate.Text) < CDate("1/1/1945") Then lblRequestDate.Text = "-"
            If CDate(lblConfirmDate.Text) < CDate("1/1/1945") Then lblConfirmDate.Text = "-"
            objD = objDFac.Retrieve(lblDealerCode.Text)
            If Not IsNothing(objD) Then
                lblDealerCode.ToolTip = objD.DealerName & IIf(IsNothing(objD.City), "", ", " & objD.City.CityName)
            Else
                lblDealerCode.ToolTip = ""
            End If

            'Change enumeration of FakturStatus and PaymentType
            'If lblFakturStatus.Text = "3" Or lblFakturStatus.Text = "4" Then
            '    lblFakturStatus.Text = "Invoice Open"
            'Else
            '    lblFakturStatus.Text = "Dealer Stok"
            'End If
            'If lblName1.Text.Trim = "" Then
            '    lblFakturStatus.Text = Me.lsbStatus.Items(0).Text
            'Else
            '    lblFakturStatus.Text = EnumChassisMaster.FakturStatusDesc(CType(lblFakturStatus.Text, Integer))
            'End If
            If CType(lblFakturStatus.Text, Integer) = -1 Then
                lblFakturStatus.Text = Me.lsbStatusFaktur.Items(0).Text
            ElseIf CType(lblFakturStatus.Text, Integer) = -2 Then
                lblFakturStatus.Text = "Sudah Terjual"
            Else
                lblFakturStatus.Text = EnumChassisMaster.FakturStatusDesc(CType(lblFakturStatus.Text, Integer))
            End If
            lblPaymentType.Text = enumPaymentType.GetStringValue(lblPaymentType.Text)

            Dim obj_VDealerStock As v_DealerStock = CType(e.Item.DataItem, v_DealerStock)
            'If obj_VDealerStock.AlreadySaled = 2 Then
            '    ChkSaled.Checked = True
            '    lblFakturStatus.Text = "Sudah Terjual"
            'End If

            If obj_VDealerStock.FakturStatus = "-1" Then
                lblFakturStatus.Text = ""
            End If

            Dim boolAlreadySaled As Boolean = CType(obj_VDealerStock.AlreadySaled, Integer).Equals(2)
            If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
                ChkSaled.Enabled = boolAlreadySaled

            Else
                ChkSaled.Enabled = Not boolAlreadySaled

                If obj_VDealerStock.FakturStatus = CType(EnumChassisMaster.FakturStatus.Selesai, Short) Then
                    ChkSaled.Enabled = False
                End If
            End If

        End If
    End Sub

    Private Sub dtgMain_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        BindDTG(e.NewPageIndex)
    End Sub

    Private Sub dtgMain_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMain.SortCommand
        If viewstate.Item("SortColumn") = e.SortExpression Then
            If viewstate.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        Else
            viewstate.Item("SortColumn") = e.SortExpression
            viewstate.Item("SortDirection") = Sort.SortDirection.ASC
        End If
        BindDTG(0)

    End Sub

    Private Sub ddlTipe_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipe.SelectedIndexChanged
        BindDdlWarna()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        If dtgMain.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang akan didownload")
            Exit Sub
        Else
            DoDownload()
        End If
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        BindDdlType()
        FillModel(ddlKategori.SelectedValue)
    End Sub

#End Region

    Private Sub btnAlreadySaled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlreadySaled.Click

        Dim cmFacade As New ChassisMasterFacade(User)
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim strMSG As String = ""

        For Each item As DataGridItem In dtgMain.Items
            Dim lblID As Label = item.FindControl("lblID")
            Dim ChkSaled As CheckBox = item.FindControl("ChkSaled")

            Dim objtoUpdate As ChassisMaster = cmFacade.Retrieve(CInt(lblID.Text))
            If ChkSaled.Checked Then
                If objtoUpdate.Category.ProductCategory.Code.Trim = companyCode Then
                    objtoUpdate.AlreadySaled = 2
                    objtoUpdate.AlreadySaledTime = DateTime.Now
                    cmFacade.Update(objtoUpdate)
                Else
                    strMSG = strMSG & objtoUpdate.ChassisNumber & ", "
                End If
            End If
        Next

        If strMSG <> "" Then
            strMSG = Left(strMSG, Len(strMSG) - 2)
            strMSG = "Nomor Rangka " & strMSG & " gagal diupdate karena tidak terdapat pada Kategori Produk"
        Else
            strMSG = "Data sudah diupdate"
        End If

        MessageBox.Show(strMSG)
    End Sub
    Private Sub btnReverseAlreadySaled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseAlreadySaled.Click

        Dim cmFacade As New ChassisMasterFacade(User)
        For Each item As DataGridItem In dtgMain.Items
            Dim lblID As Label = item.FindControl("lblID")
            Dim ChkSaled As CheckBox = item.FindControl("ChkSaled")

            Dim objtoUpdate As ChassisMaster = cmFacade.Retrieve(CInt(lblID.Text))
            If ChkSaled.Checked Then
                objtoUpdate.AlreadySaled = 0
                objtoUpdate.AlreadySaledTime = DateTime.Now
                cmFacade.Update(objtoUpdate)
            End If
        Next

        MessageBox.Show("Data sudah diupdate")
    End Sub
#Region "Private Class"
    Class clsStatusCriteria
        Private _FieldName As String = ""
        Private _FieldValue As String = ""
        Property FieldName() As String
            Get
                Return _FieldName
            End Get
            Set(ByVal Value As String)
                _FieldName = Value
            End Set
        End Property
        Property FieldValue() As String
            Get
                Return _FieldValue
            End Get
            Set(ByVal Value As String)
                _FieldValue = Value
            End Set
        End Property
    End Class
#End Region

End Class
