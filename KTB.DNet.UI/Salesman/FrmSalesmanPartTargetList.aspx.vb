Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Salesman
Imports System.IO
Imports System.Text
Public Class FrmSalesmanPartTargetList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadExcel As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanTarget As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPeriodMonth1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriodYear1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriodMonth2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriodYear2 As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private sessHelper As New SessionHelper
    Private arlTarget As New ArrayList
    Private objDealer As Dealer
    Dim strFileNm As String
    Dim strFileNmHeader As String
#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Lihat_daftar_salesman_target_realisasi_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Part Employee - Daftar Salesman Target dan Realisasi")
        End If
    End Sub

    Dim Priv_All_Download As Boolean = SecurityProvider.Authorize(context.User, SR.Download_daftar_salesman_target_realisasi_privilege)

#End Region

#Region "Event handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        BindControlsAttribute()
        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
        If Not IsPostBack Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.Text = objDealer.DealerCode
                txtDealerCode.Enabled = False
                lblSearchDealer.Visible = False
            End If
            BindDropdownlist()
            BindSalesmanTarget()
        End If
        btnDownloadExcel.Enabled = Priv_All_Download
    End Sub

    Private Sub dgSalesmanTarget_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanTarget.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanPartTarget As SalesmanPartTarget = e.Item.DataItem
            Dim lblSearchTerm2 As Label = CType(e.Item.FindControl("lblSearchTerm2"), Label)
            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            Dim lblMonth As Label = CType(e.Item.FindControl("lblMonth"), Label)

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dgSalesmanTarget.CurrentPageIndex * dgSalesmanTarget.PageSize)
            lblSearchTerm2.Text = objSalesmanPartTarget.SalesmanHeader.Dealer.SearchTerm2
            lblKodeDealer.Text = objSalesmanPartTarget.SalesmanHeader.Dealer.DealerCode
            lblMonth.Text = enumMonthGet.GetName(objSalesmanPartTarget.Month) 'CType(objSalesmanPartTarget.Month, enumMonth.Month).ToString.Replace("_", " ")

            If objSalesmanPartTarget.Realization < objSalesmanPartTarget.Target Then
                e.Item.BackColor = System.Drawing.Color.Yellow
            End If
            Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
            lbtnHistory.Attributes.Add("onclick", String.Format("ShowPopUpHistory({0});return false;", objSalesmanPartTarget.ID))
        End If
    End Sub

    Private Sub dgSalesmanTarget_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanTarget.PageIndexChanged
        dgSalesmanTarget.CurrentPageIndex = e.NewPageIndex
        BindSalesmanTarget(dgSalesmanTarget.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanTarget_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanTarget.SortCommand
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
        dgSalesmanTarget.SelectedIndex = -1
        BindSalesmanTarget(dgSalesmanTarget.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindSalesmanTarget(0)
        arlTarget = CType(sessHelper.GetSession("TargetList"), ArrayList)
        If arlTarget.Count <= 0 Then
            MessageBox.Show("Tidak ada data")
        End If
    End Sub

    Private Sub btnDownloadExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

#End Region

#Region "Custom"

    Private Sub BindControlsAttribute()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblShowSalesman.Attributes("onclick") = "ShowSalesmanSelection();"
        sessHelper.SetSession("strFileNm", "SalesmanPartTarget")
        sessHelper.SetSession("strFileNmHeader", "Salesman Part Target")
    End Sub

    Private Sub BindDropdownlist()
        Try
            ddlPeriodMonth1.Items.Clear()
            ddlPeriodMonth1.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlPeriodMonth1.Items.Add(item)
            Next
            ddlPeriodMonth1.ClearSelection()
            ddlPeriodMonth1.SelectedIndex = CType(Format(DateTime.Now, "MM").ToString, Integer)
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlPeriodMonth1, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            ddlPeriodYear1.Items.Clear()
            ddlPeriodYear1.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlPeriodYear1.Items.Add(item)
            Next
            ddlPeriodYear1.ClearSelection()
            ddlPeriodYear1.SelectedValue = DateTime.Now.Year.ToString
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlPeriodYear1, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            ddlPeriodMonth2.Items.Clear()
            ddlPeriodMonth2.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlPeriodMonth2.Items.Add(item)
            Next
            ddlPeriodMonth2.ClearSelection()
            ddlPeriodMonth2.SelectedIndex = CType(Format(DateTime.Now, "MM").ToString, Integer)
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlPeriodMonth1, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            ddlPeriodYear2.Items.Clear()
            ddlPeriodYear2.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlPeriodYear2.Items.Add(item)
            Next
            ddlPeriodYear2.ClearSelection()
            ddlPeriodYear2.SelectedValue = DateTime.Now.Year.ToString
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlPeriodYear2, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    Private Sub BindSalesmanTarget()
        ViewState("CurrentSortColumn") = "SalesmanHeader.ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        If Not IsNothing(sessHelper.GetSession("idxPage")) Then
            BindSalesmanTarget(sessHelper.GetSession("idxPage"))
        Else
            BindSalesmanTarget(0)
        End If
    End Sub

    Private Sub BindSalesmanTarget(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criteriasDownload As New CriteriaComposite(New Criteria(GetType(V_SalesmanPartTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtDealerCode.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            criteriasDownload.opAnd(New Criteria(GetType(V_SalesmanPartTarget), "DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        End If
        If ddlPeriodMonth1.SelectedIndex <> 0 Then
            If ddlPeriodYear1.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "Period", MatchType.GreaterOrEqual, New DateTime(CType(ddlPeriodYear1.SelectedValue, Integer), CType(ddlPeriodMonth1.SelectedValue, Integer), 1, 0, 0, 0)))
                criteriasDownload.opAnd(New Criteria(GetType(V_SalesmanPartTarget), "Period", MatchType.GreaterOrEqual, New DateTime(CType(ddlPeriodYear1.SelectedValue, Integer), CType(ddlPeriodMonth1.SelectedValue, Integer), 1, 0, 0, 0)))
            Else
                MessageBox.Show("Periode awal (Tahun) belum di set")
                Exit Sub
            End If
        End If

        If ddlPeriodMonth2.SelectedIndex <> 0 Then
            If ddlPeriodYear2.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "Period", MatchType.LesserOrEqual, New DateTime(CType(ddlPeriodYear2.SelectedValue, Integer), CType(ddlPeriodMonth2.SelectedValue, Integer), 1, 23, 59, 59)))
                criteriasDownload.opAnd(New Criteria(GetType(V_SalesmanPartTarget), "Period", MatchType.LesserOrEqual, New DateTime(CType(ddlPeriodYear2.SelectedValue, Integer), CType(ddlPeriodMonth2.SelectedValue, Integer), 1, 23, 59, 59)))
            Else
                MessageBox.Show("Periode akhir (Tahun) belum di set")
                Exit Sub
            End If
        End If

        If txtSalesmanCode.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SalesmanPartTarget), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanCode.Text.Trim))
            criteriasDownload.opAnd(New Criteria(GetType(V_SalesmanPartTarget), "SalesmanCode", MatchType.Exact, txtSalesmanCode.Text.Trim))
        End If

        arlTarget = New SalesmanPartTargetFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanTarget.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        sessHelper.SetSession("criteriadownload", criteriasDownload)

        dgSalesmanTarget.CurrentPageIndex = idxPage
        dgSalesmanTarget.DataSource = arlTarget
        dgSalesmanTarget.VirtualItemCount = totalRow
        dgSalesmanTarget.DataBind()

        sessHelper.SetSession("idxPage", dgSalesmanTarget.CurrentPageIndex)
        sessHelper.SetSession("TargetList", arlTarget)

    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgSalesmanTarget.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New V_SalesmanPartTargetFacade(User).RetrieveByCriteria(crits)

        If arrData.Count > 0 Then
            DoDownload(arrData)
        End If

    End Sub
    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        If Not IsNothing(sessHelper.GetSession("strFileNm")) Then
            strFileNm = CType(sessHelper.GetSession("strFileNm"), String)
        End If
        If Not IsNothing(sessHelper.GetSession("strFileNmHeader")) Then
            strFileNmHeader = CType(sessHelper.GetSession("strFileNmHeader"), String)
        End If

        sFileName = strFileNm & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim ListData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        Dim _user As String
        _user = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String
        _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String
        _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            'If imp.Start() Then

            Dim finfo As FileInfo = New FileInfo(ListData)
            If finfo.Exists Then
                finfo.Delete()
            End If

            Dim fs As FileStream = New FileStream(ListData, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)
            WriteListData(sw, data)
            sw.Close()
            fs.Close()
            '    imp.StopImpersonate()
            '    imp = Nothing
            'End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub

    Private Sub WriteListData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)

        Dim itemLine As StringBuilder = New StringBuilder
        Dim objSmanFacade As New SalesmanHeaderFacade(User)

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(strFileNmHeader)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Search Term 2" & tab)
            itemLine.Append("Kode Dealer " & tab)
            itemLine.Append("Kode Employee" & tab)
            itemLine.Append("Nama" & tab)
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Posisi" & tab)
            itemLine.Append("Level" & tab)
            itemLine.Append("Area" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Tahun" & tab)
            itemLine.Append("Bulan" & tab)
            itemLine.Append("Target" & tab)
            itemLine.Append("Realisasi" & tab)
            itemLine.Append("Persentase" & tab)
            'itemLine.Append("Persentase Realisasi Dealer" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As V_SalesmanPartTarget In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.SearchTerm2 & tab)
                itemLine.Append(item.DealerCode.ToString & tab)
                itemLine.Append(item.SalesmanCode.ToString & tab)
                itemLine.Append(item.Name.ToString & tab)
                itemLine.Append(item.Kategori & tab)
                itemLine.Append(item.Posisi & tab)
                itemLine.Append(item.Level & tab)
                itemLine.Append(item.AreaDesc & tab)
                itemLine.Append(CType(item.Status, EnumSalesmanStatus.SalesmanStatus).ToString.Replace("_", " ") & tab)
                itemLine.Append(item.Year & tab)
                itemLine.Append(item.Month & tab)
                itemLine.Append(FormatNumber(item.Target, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
                itemLine.Append(FormatNumber(item.Realization, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
                itemLine.Append(FormatNumber(item.Persentage, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
                'itemLine.Append(FormatNumber((item.Realization / item.RealizationDealer) * 100, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next
        End If
    End Sub
#End Region


End Class
