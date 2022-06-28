#Region "Custom Namespace"
Imports ktb.DNet.UI.Helper
Imports ktb.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
#End Region

Imports System.IO
Imports System.Text

Public Class FrmSAPReport
    Inherits System.Web.UI.Page

#Region "Deklarasi"
    Private sHelper As New SessionHelper
    Protected WithEvents dtgSAPReport As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtPeriod As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnReCalculate As System.Web.UI.WebControls.Button
    Protected WithEvents ddlPeringkat As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList
    Private criterias As CriteriaComposite

#End Region


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlcategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchSAP As System.Web.UI.WebControls.Label
    Protected WithEvents txtSAPNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnFieldTemp As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblStartPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndPeriod As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SAPReportView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SAP - Recap Data")
        End If
    End Sub

    Private Function CekReportCountPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SAPReportCount_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekReportDownloadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SAPReportDonload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            CommonFunction.BindJobPosition(ddlcategori, User, True, False)
            bindPeringkat()
            BindArea(ddlArea)

            ViewState("currSortColLP") = "ID"
            ViewState("currSortDirLP") = Sort.SortDirection.ASC
            dtgSAPReport.DataSource = New ArrayList
            dtgSAPReport.DataBind()

            Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title <> 1 Then
                lblDealerCode.Attributes.Add("onclick", "")
                lblSearchSAP.Attributes.Add("onclick", "ShowSAPSelection();")
                txtDealerCode.Text = objUserInfo.Dealer.DealerCode
                txtDealerCode.Enabled = False
            Else
                lblDealerCode.Attributes.Add("onclick", "ShowPPDealerSelection();")
                lblSearchSAP.Attributes.Add("onclick", "ShowSAPSelection();")
                txtDealerCode.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtSAPNo.Text = String.Empty Then
            MessageBox.Show("Masukkan No SAP terlebih dahulu")
        Else
            If hdnFieldTemp.Value <> "" Then
                Dim strData() As String = hdnFieldTemp.Value.Split(";")
                Dim strStartPeriod As String = strData(0)
                Dim strEndPeriod As String = strData(1)

                lblStartPeriod.Text = strStartPeriod
                lblEndPeriod.Text = strEndPeriod
            Else
                Dim sapNo As String = txtSAPNo.Text.Trim
                Dim objSAPPeriod As SAPPeriod = New SAPPeriodFacade(User).Retrieve(sapNo)
                If objSAPPeriod Is Nothing Then
                    MessageBox.Show("No SAP tidak valid")
                    Exit Sub
                Else
                    lblStartPeriod.Text = objSAPPeriod.StartDate
                    lblEndPeriod.Text = objSAPPeriod.EndDate
                    hdnFieldTemp.Value = lblStartPeriod.Text & ";" & lblEndPeriod.Text
                End If
            End If
            dtgSAPReport.CurrentPageIndex = 0
            bindToGrid(0)
        End If
    End Sub

    Private Sub bindToGrid(ByVal idx As Integer)
        Dim totalRow As Integer = 0


        '----Dealer salesman
        'Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If txtDealerCode.Text.Trim <> String.Empty Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        'End If

        'If ddlcategori.SelectedIndex <> 0 Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, CInt(ddlcategori.SelectedValue)))
        'End If

        'If ddlArea.SelectedValue <> 0 Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanArea.ID", MatchType.Exact, CInt(ddlArea.SelectedValue)))
        'End If




        'Dim arlSalesHeader As ArrayList = New SalesmanHeaderFacade(User).Retrieve(crits)
        'Dim strSalesHeaderId As String = String.Empty
        'For Each item As SalesmanHeader In arlSalesHeader
        '    strSalesHeaderId &= item.ID & ","
        'Next

        'If strSalesHeaderId <> String.Empty Then
        '    strSalesHeaderId = Left(strSalesHeaderId, strSalesHeaderId.Length - 1)
        'Else
        '    strSalesHeaderId = "0"
        'End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtSAPNo.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, txtSAPNo.Text.Trim))
        End If

        'Todo Inset
        'criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.InSet, "(" & strSalesHeaderId & ")"))

        'Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtDealerCode.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
            'crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        End If

        If ddlcategori.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.JobPosition.ID", MatchType.Exact, CInt(ddlcategori.SelectedValue)))
            'crits.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, CInt(ddlcategori.SelectedValue)))
        End If

        If ddlArea.SelectedValue <> 0 Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.SalesmanArea.ID", MatchType.Exact, CInt(ddlArea.SelectedValue)))
            'crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanArea.ID", MatchType.Exact, CInt(ddlArea.SelectedValue)))
        End If


        If ddlPeringkat.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "IsWinner", MatchType.Exact, CInt(ddlPeringkat.SelectedValue)))
        End If

        Dim arlSAP As ArrayList = New SAPRegisterFacade(User).RetrieveActiveList(idx + 1, dtgSAPReport.PageSize, totalRow, viewstate("currSortColLP"), viewstate("currSortDirLP"), criterias)
        Dim arlSAPtoDownload As ArrayList = New SAPRegisterFacade(User).RetrieveActiveList(viewstate("currSortColLP"), viewstate("currSortDirLP"), criterias)
        If Not (arlSAP.Count > 0) Then
            MessageBox.Show("Data Tidak ditermukan")
            sHelper.SetSession("DataToCalculate", Nothing)
            sHelper.SetSession("DataToDownload", Nothing)
            btnReCalculate.Enabled = False
            btnDownload.Enabled = False
        Else
            sHelper.SetSession("DataToCalculate", arlSAP)
            sHelper.SetSession("DataToDownload", arlSAPtoDownload)
            btnReCalculate.Enabled = True
            btnDownload.Enabled = True

            ' add security
            If Not CekReportCountPrivilege() Then
                btnReCalculate.Enabled = False
            End If
            If Not CekReportDownloadPrivilege() Then
                btnDownload.Enabled = False
            End If
        End If
        dtgSAPReport.DataSource = arlSAP
        dtgSAPReport.VirtualItemCount = totalRow
        dtgSAPReport.DataBind()

    End Sub

    Private Sub dtgSAPReport_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSAPReport.PageIndexChanged
        dtgSAPReport.CurrentPageIndex = e.NewPageIndex
        bindToGrid(dtgSAPReport.CurrentPageIndex)
    End Sub

    Private Sub dtgSAPReport_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSAPReport.SortCommand
        If CType(ViewState("currSortColLP"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirLP"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirLP") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirLP") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColLP") = e.SortExpression
            ViewState("currSortDirLP") = Sort.SortDirection.ASC
        End If
        bindToGrid(dtgSAPReport.CurrentPageIndex)
    End Sub

    Private Sub dtgSAPReport_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSAPReport.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgSAPReport.PageSize * dtgSAPReport.CurrentPageIndex)

            Dim _obj As SAPRegister = e.Item.DataItem
            Select Case _obj.SalesmanHeader.JobPosition.Code
                Case KTB.DNet.Lib.WebConfig.GetValue("SManCode")

                Case KTB.DNet.Lib.WebConfig.GetValue("SSpvCode")

                Case KTB.DNet.Lib.WebConfig.GetValue("SmanCode")

                Case KTB.DNet.Lib.WebConfig.GetValue("SCntCode")

            End Select

        End If
    End Sub

    Private Sub btnReCalculate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReCalculate.Click
        Dim nresult As Integer = 0
        Dim arlSapPeriod As ArrayList = New ArrayList
        Dim a As SAPRegisterFacade = New SAPRegisterFacade(User)
        Dim _dealer As String = String.Empty
        Dim _kategori As String = String.Empty

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlcategori.SelectedIndex > 0 Then
            Dim _obj As JobPosition = New JobPositionFacade(User).Retrieve(CInt(ddlcategori.SelectedValue))
            _kategori = _obj.Code
        End If

        If txtSAPNo.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SAPPeriod), "SAPNumber", MatchType.Exact, txtSAPNo.Text))
        End If

        If txtDealerCode.Text.Trim <> String.Empty Then
            Try
                Dim critsDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critsDealer.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
                Dim arrDealer As ArrayList = New DealerFacade(User).Retrieve(critsDealer)
                If arrDealer.Count = 0 Then
                    MessageBox.Show("Kode Dealer Tidak Terdaftar")
                    Return
                End If
            Catch ex As Exception
                MessageBox.Show("Kode Dealer Tidak Terdaftar")
                Return
            End Try

        End If

        arlSapPeriod = New SAPPeriodFacade(User).Retrieve(criterias)
        If arlSapPeriod.Count > 0 Then
            For Each item As SAPPeriod In arlSapPeriod
                nresult = a.FillReportField(item.ID, txtDealerCode.Text, ddlArea.SelectedValue)
                btnSearch_Click(Nothing, Nothing)
            Next
        End If
    End Sub

    Private Sub rptProcess(ByRef _obj As SAPRegister)
        Dim _nresult As Integer = 0
        _obj.RptProsPek = 0
        _obj.RptHotProspek = 0

        Dim aggr As Aggregate = New Aggregate(GetType(SAPCustomer), "ID", AggregateType.Count)
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(SAPCustomer), "SAPRegister.ID", MatchType.Exact, _obj.ID))

        If _obj.SalesmanHeader.JobPosition.Code = KTB.DNet.Lib.WebConfig.GetValue("SCntCode") Then
            _nresult = New SAPCustomerFacade(User).RetrieveScalar(crits, aggr)
            Select Case _nresult
                Case Is >= 60
                    _obj.RptProsPek = 100
                Case Is <= 59
                    _obj.RptProsPek = 75
                Case Is <= 44
                    _obj.RptProsPek = 50
                Case Is <= 29
                    _obj.RptProsPek = 25
                Case Is < 15
                    _obj.RptProsPek = 0
            End Select
        End If
    End Sub

    Private Sub bindPeringkat()
        ddlPeringkat.Items.Add(New ListItem("Silahkan Pilih", 0))
        For i As Integer = 0 To 14
            ddlPeringkat.Items.Add(New ListItem((i + 1).ToString, i + 1))
        Next
        ddlPeringkat.SelectedIndex = 0
    End Sub

    Private Sub BindArea(ByVal ddlArea As DropDownList)
        Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlArea As ArrayList = New SalesmanAreaFacade(User).Retrieve(crits)

        Dim objSA As New SalesmanArea
        objSA.AreaDesc = "Silahkan Pilih"
        objSA.ID = 0
        arlArea.Insert(0, objSA)

        ddlArea.DataSource = arlArea
        ddlArea.DataTextField = "AreaDesc"
        ddlArea.DataValueField = "ID"
        ddlArea.DataBind()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        ' Dim arlData As ArrayList = CType(sHelper.GetSession("ForDownload"), ArrayList)
        SetDownload(CType(sHelper.GetSession("DataToDownload"), ArrayList))
    End Sub


    Private Sub SetDownload(ByVal arlData As ArrayList)
        Dim strFileName As String = "SAPReport" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim SAPReportFile As String = Server.MapPath("..\DataTemp\" & strFileName & ".xls")
        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SAPReportFile)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(SAPReportFile, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteSAPReport(sw, arlData)
                sw.Close()
                fs.Close()



                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & strFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try

    End Sub

    Private Sub WriteSAPReport(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder
        Try
            If Not IsNothing(data) Then
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("SAP - SAP Report")
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("Kode Dealer" & tab)
                itemLine.Append(txtDealerCode.Text & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("SAP Number" & tab)
                itemLine.Append(txtSAPNo.Text & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("Periode SAP" & tab)
                itemLine.Append(lblStartPeriod.Text & " s/d " & lblEndPeriod.Text)
                itemLine.Append(" " & tab & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("No" & tab)
                itemLine.Append("KodeDealer" & tab)
                itemLine.Append("Salesman ID" & tab)
                itemLine.Append("Nama Salesman" & tab)
                itemLine.Append("Kategori" & tab)
                itemLine.Append("Rpt Prospek" & tab)
                itemLine.Append("Rpt Hot Prospek" & tab)
                itemLine.Append("Faktur" & tab)
                itemLine.Append("PDI" & tab)
                itemLine.Append("Test Tulis" & tab)
                itemLine.Append("Presentasi Produk SWAP & PKT" & tab)
                itemLine.Append("Nilai Rata2 Salesman/SC" & tab)
                itemLine.Append("Efektivitas Training" & tab)
                itemLine.Append("Pencapaian Penjualan TIM" & tab)
                itemLine.Append("Presentasi Produk" & tab)
                itemLine.Append("Konsistensi Peserta" & tab)
                itemLine.Append("Komposisi Sales Force" & tab)
                itemLine.Append("Jumlah Pemenang SAP" & tab)
                itemLine.Append("Kelengkapan & Validitas Data" & tab)
                itemLine.Append("Nilai Final" & tab)

                sw.WriteLine(itemLine.ToString())

                Dim i As Integer = 1
                For Each item As SAPRegister In data
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.SalesmanHeader.Dealer.DealerCode & tab)
                    itemLine.Append(item.SalesmanHeader.SalesmanCode & tab)
                    itemLine.Append(item.SalesmanHeader.Name & tab)
                    itemLine.Append(item.SalesmanHeader.JobPosition.Description & tab)

                    'itemLine.Append(item.SalesmanCode & tab)
                    'itemLine.Append(item.Name & tab)
                    'itemLine.Append(item.JobPosition.Description & tab)

                    Dim crits As New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.Exact, item.SalesmanHeader.ID))
                    crits.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, item.SAPPeriod.SAPNumber.Trim))
                    Dim objSAPRegister As SAPRegister = New SAPRegisterFacade(User).Retrieve(crits)(0)

                    itemLine.Append(objSAPRegister.RptProsPek & tab)
                    itemLine.Append(objSAPRegister.RptHotProspek & tab)
                    itemLine.Append(objSAPRegister.RptFaktur & tab)
                    itemLine.Append(objSAPRegister.RptPDI & tab)
                    itemLine.Append(objSAPRegister.WritingTestScore & tab)
                    itemLine.Append(objSAPRegister.GradeSWAP & tab)
                    itemLine.Append(objSAPRegister.RptAvgScoreSubOrdinate & tab)
                    itemLine.Append(objSAPRegister.RptEffectivity & tab)
                    itemLine.Append(objSAPRegister.RptAchievement & tab)
                    itemLine.Append(objSAPRegister.GradePresentasi & tab)
                    itemLine.Append(objSAPRegister.GradeKonsistensi & tab)
                    itemLine.Append(objSAPRegister.RptKomposisi & tab)
                    itemLine.Append(objSAPRegister.RptWinnerAmount & tab)
                    itemLine.Append(objSAPRegister.GradeKelengkapan & tab)
                    itemLine.Append(objSAPRegister.GradeFinal & tab)
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next

            End If
        Catch ex As Exception
            Dim str As String
            str = String.Empty
        End Try
        
    End Sub
End Class
