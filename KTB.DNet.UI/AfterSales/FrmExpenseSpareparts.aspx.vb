
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports MMKSI.DNetUpload.Utility
Imports System.Linq
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmExpenseSpareparts
    Inherits System.Web.UI.Page
    Dim sHRole As SessionHelper = New SessionHelper


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgTargetDealer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgBiayaKaryawan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgBiayaOperasional As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgBiayaPerawatan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgBiayaPenyusutan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgBiayaUmum As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgBiayaBunga As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgSaldoPiutang As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgPembayaranPiutang As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents hAssistUploadLogID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblKodeDealerMenu As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerDetailSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerDetailSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotalBiayaKaryawan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalBiayaOperasional As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalBiayaPerawatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalBiayaUmum As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalBiayaPenyusutan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalBiayaBunga As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalSaldoPiutang As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPembayaranPiutang As System.Web.UI.WebControls.Label
    Protected WithEvents hDownloadURL As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hQuery As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private m_SaveFormPrivilege As Boolean = False
    Private ListModule As ArrayList
    Dim objAssistUploadLog As AssistUploadLog
    Private totalBKaryawan As Double = 0
    Private totalBOperasional As Double = 0
    Private totalBPerawatan As Double = 0
    Private totalBUmum As Double = 0
    Private totalBPenyusutan As Double = 0
    Private totalBBunga As Double = 0
    Private totalSaldoPiutang As Double = 0
    Private totalPembayaranPiutang As Double = 0
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            GetAssistUploadLog()

            If hAssistUploadLogID.Value = "" Then 'jika dibuka dari menu maka empty
                btnBack.Visible = False
                btnDownload.Visible = True
                btnDownload.Enabled = False
                lblKodeDealerDetail.Visible = False
                lblKodeDealerDetailSeparator.Visible = False
                lblDealerCode.Visible = False
                lblNamaDealerDetail.Visible = False
                lblNamaDealerDetailSeparator.Visible = False
                lblDealerName.Visible = False
                lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            Else
                btnCari.Visible = False
                btnDownload.Visible = False
                BindDataGrid(dtgTargetDealer.CurrentPageIndex)
                txtKodeDealer.Visible = False
                lblSearchDealer.Visible = False
                lblKodeDealerMenu.Visible = False
                lblKodeDealerSeparator.Visible = False
            End If

        End If

    End Sub

    Private Sub GetAssistUploadLog()
        Dim LogID As String = Request.QueryString("id")
        If Not IsNothing(LogID) Then
            Dim objAssistLog As AssistUploadLog = New AssistUploadLogFacade(User).Retrieve(CInt(LogID))
            ddlMonth.SelectedValue = objAssistLog.Month
            ddlYear.SelectedValue = objAssistLog.Year
            ddlMonth.Enabled = False
            ddlYear.Enabled = False
            hAssistUploadLogID.Value = LogID
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sHRole.GetSession("PrevPage") Is Nothing AndAlso Not sHRole.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sHRole.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If

    End Sub

    Private Sub BindMonth()

        Dim al As ArrayList = enumMonthGet.RetriveMonth()
        ddlMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlMonth.DataValueField = "ValStatus"
        ddlMonth.DataTextField = "NameStatus"
        ddlMonth.DataBind()

        ddlMonth.SelectedValue = Now.Month

    End Sub

    Private Sub BindYear()

        Dim a As Integer
        Dim now As DateTime = DateTime.Now
        For a = -3 To 3
            ddlYear.Items.Insert(0, New ListItem(now.AddYears(a).Year, now.AddYears(a).Year))
        Next

        ddlYear.SelectedValue = now.Year
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtKodeDealer.Text.Contains(";") Then
            MessageBox.Show("Hanya boleh satu pilihan dealer")
            Return
        ElseIf txtKodeDealer.Text = "" Then
            MessageBox.Show("Kode dealer harus dipilih")
            Return
        Else
            SearchModuleByCriteria()
            btnDownload.Enabled = True
        End If
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        'btnUpload.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_DaftarUpload_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Data Upload - Expense Spareparts")
        End If

    End Sub

    Private Sub InitiatePage()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        If Not sHRole.GetSession("DEALER") Is Nothing Then
            Dim arDeal As ArrayList = New ArrayList
            Dim objDeal As Dealer
            objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
            If Not objDeal Is Nothing Then
                lblDealerCode.Text = objDeal.DealerCode & " / " & objDeal.SearchTerm1
                lblDealerName.Text = objDeal.DealerName
                Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")

            End If
        End If
        BindMonth()
        BindYear()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then

            Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistExpenseSpareparts), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If Not hAssistUploadLogID.Value = "" Then 'jika dibuka dari daftar upload
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistExpenseSpareParts), "AssistUploadLog.ID", MatchType.Exact, hAssistUploadLogID.Value))
            Else
                If txtKodeDealer.Text.Trim() <> "" Then
                    Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                    criterias.opAnd(New Criteria(GetType(AssistExpenseSpareparts), "AssistUploadLog.Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
                End If

                If ddlMonth.SelectedValue > 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistExpenseSpareparts), "AssistUploadLog.Month", MatchType.Exact, ddlMonth.SelectedValue()))
                End If

                If ddlYear.SelectedValue > 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistExpenseSpareparts), "AssistUploadLog.Year", MatchType.Exact, ddlYear.SelectedValue()))
                End If

                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistExpenseSpareparts), "StatusAktif", MatchType.Exact, 1))

            End If

            Dim _query As String = criterias.ToString()
            hQuery.Value = _query

            ListModule = New AssistExpenseSparepartsFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgTargetDealer.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            If ListModule.Count = 0 Then
                MessageBox.Show("Tidak ada data yang sudah dikonfirmasi MMKSI")
                Return
            End If

            Dim ObjTargetDealer = (From ObjES As AssistExpenseSpareParts In ListModule
            Where ObjES.ExpenseGroup = "TARGET DEALER SALES"
            Order By ObjES.ID
            Select ObjES).ToList()

            Dim ObjBiayaKaryawan = (From ObjES As AssistExpenseSpareparts In ListModule
            Where ObjES.ExpenseGroup = "BIAYA KARYAWAN"
            Order By ObjES.ID
            Select ObjES).ToList()

            Dim ObjBiayaOperasional = (From ObjES As AssistExpenseSpareparts In ListModule
            Where ObjES.ExpenseGroup = "BIAYA OPERASIONAL"
            Order By ObjES.ID
            Select ObjES).ToList()

            Dim ObjBiayaPerawatan = (From ObjES As AssistExpenseSpareparts In ListModule
            Where ObjES.ExpenseGroup = "BIAYA PERAWATAN"
            Order By ObjES.ID
            Select ObjES).ToList()

            Dim ObjBiayaPenyusutan = (From ObjES As AssistExpenseSpareparts In ListModule
            Where ObjES.ExpenseGroup = "BIAYA PENYUSUTAN"
            Order By ObjES.ID
            Select ObjES).ToList()

            Dim ObjBiayaUmum = (From ObjES As AssistExpenseSpareParts In ListModule
            Where ObjES.ExpenseGroup = "BIAYA UMUM"
            Order By ObjES.ID
            Select ObjES).ToList()

            Dim ObjBiayaBunga = (From ObjES As AssistExpenseSpareParts In ListModule
            Where ObjES.ExpenseGroup = "BIAYA BUNGA"
            Order By ObjES.ID
            Select ObjES).ToList()

            Dim ObjSaldoPiutang = (From ObjES As AssistExpenseSpareParts In ListModule
            Where ObjES.ExpenseGroup = "KETERANGAN"
            Order By ObjES.ID
            Select ObjES).ToList()

            Dim ObjPembayaranPiutang = (From ObjES As AssistExpenseSpareParts In ListModule
            Where ObjES.ExpenseGroup = "PEMBAYARAN PIUTANG"
            Order By ObjES.ID
            Select ObjES).ToList()

            dtgTargetDealer.DataSource = ObjTargetDealer
            dtgTargetDealer.DataBind()

            dtgBiayaKaryawan.DataSource = ObjBiayaKaryawan
            dtgBiayaKaryawan.DataBind()

            dtgBiayaOperasional.DataSource = ObjBiayaOperasional
            dtgBiayaOperasional.DataBind()

            dtgBiayaPerawatan.DataSource = ObjBiayaPerawatan
            dtgBiayaPerawatan.DataBind()

            dtgBiayaPenyusutan.DataSource = ObjBiayaPenyusutan
            dtgBiayaPenyusutan.DataBind()

            dtgBiayaUmum.DataSource = ObjBiayaUmum
            dtgBiayaUmum.DataBind()

            dtgBiayaBunga.DataSource = ObjBiayaBunga
            dtgBiayaBunga.DataBind()

            dtgSaldoPiutang.DataSource = ObjSaldoPiutang
            dtgSaldoPiutang.DataBind()

            dtgPembayaranPiutang.DataSource = ObjPembayaranPiutang
            dtgPembayaranPiutang.DataBind()

        End If
    End Sub

    Public Function IsNull(value As Double, defaultValue As Double) As String
        If String.IsNullOrEmpty(value) Then
            Return defaultValue
        Else
            Return value
        End If
    End Function

    Private Sub dtgTargetDealer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTargetDealer.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If ItemType = ListItemType.Footer Then
            e.Item.Visible = False
        End If

    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        If (hQuery.Value <> "") Then
            Dim objDeal As Dealer
            objDeal = CType(sHRole.GetSession("DEALER"), Dealer)
            Dim url As String = KTB.DNet.Lib.WebConfig.GetValue("AssistDownloadByQueryURL").ToString()
            Dim _key = InsertUpdateAssistKeyUpload()
            Dim enc As Encryptor = New Encryptor()
            Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
            url = url.Replace("[DEALERCODE]", objDeal.DealerCode)
            url = url.Replace("[ASSISTMODULETYPE]", "EXPENSESPAREPARTS")
            url = url.Replace("[QUERY]", enc.Encrypt(hQuery.Value, System.Text.Encoding.Unicode))
            url = url.Replace("[USERNAMELOGIN]", enc.Encrypt(objUserInfo.UserName, System.Text.Encoding.Unicode))
            url = url.Replace("[KEY]", _key)

            hDownloadURL.Value = url

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Script", "openWindowDownload();", True)
            btnDownload.Enabled = False
        End If
    End Sub

    Function InsertUpdateAssistKeyUpload() As String
        Dim objUserInfo As UserInfo = sHRole.GetSession("LOGINUSERINFO")
        Dim enc As Encryptor = New Encryptor()
        Dim nResult As Integer
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistKeyUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistKeyUpload), "UserName", MatchType.Exact, objUserInfo.UserName))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistKeyUpload), "ID", ViewState("CurrentSortDirect")))
        Dim moduleColl As ArrayList = New AssistKeyUploadFacade(User).Retrieve(criterias, sortColl)
        Dim _key As String = enc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd hh"), System.Text.Encoding.Unicode)

        If (moduleColl.Count > 0) Then
            'Update Key
            Dim assistkey As AssistKeyUpload = moduleColl(0)
            If (assistkey.Key <> _key) Then
                assistkey.Key = _key
                nResult = New AssistKeyUploadFacade(User).Update(assistkey)
            End If
        Else
            'Insert
            Dim objKeyFacade As AssistKeyUploadFacade = New AssistKeyUploadFacade(User)
            Dim objKey As AssistKeyUpload = New AssistKeyUpload

            objKey.UserName = objUserInfo.UserName
            objKey.Key = _key
            nResult = New AssistKeyUploadFacade(User).Insert(objKey)
        End If
        Return _key
    End Function

    Private Sub dtgBiayaKaryawan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBiayaKaryawan.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As AssistExpenseSpareparts = CType(e.Item.DataItem, AssistExpenseSpareparts)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                totalBKaryawan += RowValue.Value
            End If
            Dim value As Label = CType(e.Item.FindControl("lblValue"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = "Total Biaya Karyawan"
            e.Item.Cells(2).Text = "Rp."
            e.Item.Cells(3).Text = FormatNumber(totalBKaryawan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            lblTotalBiayaKaryawan.Text = "Rp. " & FormatNumber(totalBKaryawan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgBiayaOperasional_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBiayaOperasional.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As AssistExpenseSpareparts = CType(e.Item.DataItem, AssistExpenseSpareparts)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                totalBOperasional += RowValue.Value
            End If
            Dim value As Label = CType(e.Item.FindControl("lblValue"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = "Total Biaya Operasional"
            e.Item.Cells(2).Text = "Rp."
            e.Item.Cells(3).Text = FormatNumber(totalBOperasional, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            lblTotalBiayaOperasional.Text = "Rp. " & FormatNumber(totalBOperasional, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgBiayaPerawatan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBiayaPerawatan.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As AssistExpenseSpareparts = CType(e.Item.DataItem, AssistExpenseSpareparts)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                totalBPerawatan += RowValue.Value
            End If
            Dim value As Label = CType(e.Item.FindControl("lblValue"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = "Total Biaya Perawatan"
            e.Item.Cells(2).Text = "Rp."
            e.Item.Cells(3).Text = FormatNumber(totalBPerawatan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            lblTotalBiayaPerawatan.Text = "Rp. " & FormatNumber(totalBPerawatan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgBiayaPenyusutan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBiayaPenyusutan.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As AssistExpenseSpareparts = CType(e.Item.DataItem, AssistExpenseSpareparts)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                totalBPenyusutan += RowValue.Value
            End If
            Dim value As Label = CType(e.Item.FindControl("lblValue"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = "Total Biaya Penyusutan"
            e.Item.Cells(2).Text = "Rp."
            e.Item.Cells(3).Text = FormatNumber(totalBPenyusutan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            lblTotalBiayaPenyusutan.Text = "Rp. " & FormatNumber(totalBPenyusutan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgBiayaUmum_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBiayaUmum.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As AssistExpenseSpareparts = CType(e.Item.DataItem, AssistExpenseSpareparts)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                totalBUmum += RowValue.Value
            End If
            Dim value As Label = CType(e.Item.FindControl("lblValue"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = "Total Biaya Umum"
            e.Item.Cells(2).Text = "Rp."
            e.Item.Cells(3).Text = FormatNumber(totalBUmum, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            lblTotalBiayaUmum.Text = "Rp. " & FormatNumber(totalBUmum, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgBiayaBunga_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBiayaBunga.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As AssistExpenseSpareParts = CType(e.Item.DataItem, AssistExpenseSpareParts)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                totalBBunga += RowValue.Value
            End If
            Dim value As Label = CType(e.Item.FindControl("lblValue"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = "Total Biaya Bunga"
            e.Item.Cells(2).Text = "Rp."
            e.Item.Cells(3).Text = FormatNumber(totalBBunga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            lblTotalBiayaBunga.Text = "Rp. " & FormatNumber(totalBBunga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgBiayaSaldoPiutang_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSaldoPiutang.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As AssistExpenseSpareParts = CType(e.Item.DataItem, AssistExpenseSpareParts)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                If RowValue.Description.ToUpper() = "TERTAGIH BULAN INI" Then
                    If totalSaldoPiutang > 0 Then
                        totalSaldoPiutang = totalSaldoPiutang - RowValue.Value
                    End If
                Else
                    totalSaldoPiutang += RowValue.Value
                End If

            End If
            Dim value As Label = CType(e.Item.FindControl("lblValue"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = "Total Saldo Piutang"
            e.Item.Cells(2).Text = "Rp."
            e.Item.Cells(3).Text = FormatNumber(totalSaldoPiutang, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            lblTotalSaldoPiutang.Text = "Rp. " & FormatNumber(totalSaldoPiutang, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgBiayaPembayaranPiutang_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPembayaranPiutang.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As AssistExpenseSpareParts = CType(e.Item.DataItem, AssistExpenseSpareParts)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                totalPembayaranPiutang += RowValue.Value
            End If
            Dim value As Label = CType(e.Item.FindControl("lblValue"), Label)
            value.Text = FormatNumber(Convert.ToDouble(value.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(1).Text = "Total Saldo Piutang"
            e.Item.Cells(2).Text = "Rp."
            e.Item.Cells(3).Text = FormatNumber(totalPembayaranPiutang, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            lblTotalPembayaranPiutang.Text = "Rp. " & FormatNumber(totalPembayaranPiutang, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objModule As AssistUploadLog = New AssistUploadLogFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsUploadReport", objModule)

    End Sub

    Private Sub SearchModuleByCriteria()
        dtgTargetDealer.CurrentPageIndex = 0
        BindDataGrid(dtgTargetDealer.CurrentPageIndex)
    End Sub
#End Region

End Class
