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

Imports System.IO
Imports System.Text
#End Region

Public Class FrmSAPInputDataResult
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dtgSAPList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblEndPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblSAPNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtSAPNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents hdnFieldTemp As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList
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

#Region "Deklarasi"
    Private sHelper As New SessionHelper
    Private criterias As CriteriaComposite
    Private arrGrid As ArrayList
#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer

        If indexPage >= 0 Then

            CreateCriteria()
            arrGrid = New SAPRegisterFacade(User).RetrieveActiveList(indexPage + 1, dtgSAPList.PageSize, totalRow, viewstate("currSortColLP"), viewstate("currSortDirLP"), criterias)
            sHelper.SetSession("arlRegister", arrGrid)
            dtgSAPList.DataSource = arrGrid

            If arrGrid.Count > 0 Then
                dtgSAPList.VirtualItemCount = totalRow
            Else
                MessageBox.Show("Data Tidak ditermukan")
            End If

            If indexPage = 0 Then
                dtgSAPList.CurrentPageIndex = 0
            End If

            dtgSAPList.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, txtSAPNo.Text.Trim))

        'Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ''get salesman header id that assign to spesific salesarea
        'If ddlArea.SelectedIndex <> 0 Then
        '    'get salesman that assign to this area
        '    Dim critsSA As New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    critsSA.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanArea.ID", MatchType.Exact, ddlArea.SelectedValue))
        '    Dim arlSA As ArrayList = New SalesmanAreaAssignFacade(User).Retrieve(critsSA)
        '    Dim salesmanHeaderID As String = ""
        '    If arlSA.Count > 0 Then
        '        For Each item As SalesmanAreaAssign In arlSA
        '            salesmanHeaderID &= item.SalesmanHeader.ID & ","
        '        Next
        '    End If
        '    If salesmanHeaderID.Trim <> String.Empty Then
        '        salesmanHeaderID = Left(salesmanHeaderID, salesmanHeaderID.Length - 1)
        '    Else
        '        salesmanHeaderID = "0"
        '    End If
        '    'Todo Inset
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, "(" & salesmanHeaderID & ")"))
        'End If

        'If txtDealerCode.Text <> String.Empty Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        'End If

        'If ddlKategori.SelectedIndex <> 0 Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.Code", MatchType.Exact, ddlKategori.SelectedValue))
        'End If

        ''last criteria used to get the final salesheaderid that already filtered
        'Dim arlSalesHeader As ArrayList = New SalesmanHeaderFacade(User).Retrieve(crits)
        'Dim strSalesHeaderId As String = ""
        'For Each item As SalesmanHeader In arlSalesHeader
        '    strSalesHeaderId &= item.ID & ","
        'Next

        'If strSalesHeaderId <> "" Then
        '    strSalesHeaderId = Left(strSalesHeaderId, strSalesHeaderId.Length - 1)
        'Else
        '    strSalesHeaderId = "0"
        'End If

        'Todo Inset
        'criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.InSet, "(" & strSalesHeaderId & ")"))
        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
            'crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        End If

        If ddlKategori.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.JobPosition.Code", MatchType.Exact, ddlKategori.SelectedValue))
            'crits.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.Code", MatchType.Exact, ddlKategori.SelectedValue))
        End If

        If ddlArea.SelectedIndex <> 0 Then
            'FrameworkLimitation SQL Query Too Long
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.InSet, "(select SalesmanHeaderID from SalesmanAreaAssign where salesmanareaid=" & ddlArea.SelectedValue & ")"))
        End If


    End Sub

    Private Sub BindJobPosition(ByVal ddlKategori As DropDownList)
        Dim strJobPosition As String = ""
        strJobPosition &= KTB.DNet.Lib.WebConfig.GetValue("SlmanCode") & "','" & _
            KTB.DNet.Lib.WebConfig.GetValue("SCntCode") & "','" & _
            KTB.DNet.Lib.WebConfig.GetValue("SSpvCode") & "','" & _
            KTB.DNet.Lib.WebConfig.GetValue("SManCode")

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.InSet, "('" & strJobPosition & "')"))

        Dim arlKategori As ArrayList = New JobPositionFacade(User).Retrieve(crits)
        Dim objJP As New JobPosition
        objJP.Description = "Silahkan Pilih"
        objJP.Code = "0"

        arlKategori.Insert(0, objJP)

        ddlKategori.DataSource = arlKategori
        ddlKategori.DataTextField = "Description"
        ddlKategori.DataValueField = "Code"

        ddlKategori.DataBind()
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

    Private Sub SetDownload(ByVal arlData As ArrayList)
        Dim strFileName As String = "SAPInputDataResult" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim SAPInputDataResultFile As String = Server.MapPath("..\DataTemp\" & strFileName & ".xls")

        Try
            Dim finfo As FileInfo = New FileInfo(SAPInputDataResultFile)
            If finfo.Exists Then
                finfo.Delete()
            End If

            Dim fs As FileStream = New FileStream(SAPInputDataResultFile, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)
            WriteSAPInputDataResult(sw, arlData)
            sw.Close()
            fs.Close()

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & strFileName & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteSAPInputDataResult(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("SAP - SAP Input Data Result")
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
            itemLine.Append("Salesman ID" & tab)
            itemLine.Append("Nama Salesman" & tab)
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Presentasi Produk dan SWAP dll" & tab)
            itemLine.Append("Presentasi Produk" & tab)
            itemLine.Append("Konsistensi Peserta SAP" & tab)
            itemLine.Append("Kelengkapan dan Validitas Data" & tab)
            itemLine.Append("Frekuensi In House Training" & tab)
            itemLine.Append("Jumlah Peserta Training" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As SAPRegister In data
                Try
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.SalesmanHeader.SalesmanCode & tab)
                    itemLine.Append(item.SalesmanHeader.Name & tab)
                    If Not IsNothing(item.SalesmanHeader.JobPosition) Then
                        itemLine.Append(item.SalesmanHeader.JobPosition.Description & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                    End If


                    'itemLine.Append(item.SalesmanCode & tab)
                    'itemLine.Append(item.Name & tab)
                    'itemLine.Append(item.JobPosition.Description & tab)

                    Dim crits As New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.Exact, item.SalesmanHeader.ID))
                    crits.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, item.SAPPeriod.SAPNumber.Trim))
                    Dim objSAPRegister As SAPRegister = New SAPRegisterFacade(User).Retrieve(crits)(0)
                    If IsNothing(objSAPRegister) Then
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                    Else
                        itemLine.Append(objSAPRegister.GradeSWAP & tab)
                        itemLine.Append(objSAPRegister.GradePresentasi & tab)
                        itemLine.Append(objSAPRegister.GradeKonsistensi & tab)
                        itemLine.Append(objSAPRegister.GradeKelengkapan & tab)
                        itemLine.Append(objSAPRegister.GradeFrekuensi & tab)
                        itemLine.Append(objSAPRegister.JumlahPeserta & tab)
                    End If

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Catch ex As Exception
                    Dim str As String = ex.Message
                End Try
            Next
        End If
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SAPInputDataResultView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SAP - Input Data Result")
        End If
    End Sub

    Private Function CekResultCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SAPInputDataResultCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekResultDownloadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SAPInputDataResultDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            'CommonFunction.BindJobPosition(ddlKategori, User, True, False)
            BindJobPosition(ddlKategori)
            BindArea(ddlArea)

            ViewState("currSortColLP") = "SalesmanHeader.Dealer.DealerCode"
            ViewState("currSortDirLP") = Sort.SortDirection.ASC
            dtgSAPList.DataSource = New ArrayList
            dtgSAPList.DataBind()

            Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title <> 1 Then
                lblDealerCode.Attributes.Add("onclick", "")
                lblSAPNo.Attributes.Add("onclick", "ShowSAPSelection();")
                txtDealerCode.Text = objUserInfo.Dealer.DealerCode
                txtDealerCode.Enabled = False
            Else
                lblDealerCode.Attributes.Add("onclick", "ShowPPDealerSelection();")
                lblSAPNo.Attributes.Add("onclick", "ShowSAPSelection();")
                txtDealerCode.Enabled = True
            End If

            'add security
            If Not CekResultCreatePrivilege() Then
                btnSimpan.Enabled = False
            End If

        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgSAPList.CurrentPageIndex = 0

        If txtSAPNo.Text = String.Empty Then
            MessageBox.Show("Masukkan No SAP terlebih dahulu")
            Exit Sub
        End If


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
                hdnFieldTemp.Value = objSAPPeriod.StartDate & ";" & objSAPPeriod.EndDate
                lblStartPeriod.Text = objSAPPeriod.StartDate
                lblEndPeriod.Text = objSAPPeriod.EndDate
            End If
        End If

        BindToGrid(dtgSAPList.CurrentPageIndex)
        btnDownload.Enabled = False
    End Sub

    Private Sub dtgSAPList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSAPList.PageIndexChanged
        dtgSAPList.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgSAPList.CurrentPageIndex)
    End Sub

    Private Sub dtgSAPList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSAPList.SortCommand
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
        BindToGrid(dtgSAPList.CurrentPageIndex)
    End Sub

    Private Sub dtgSAPList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSAPList.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgSAPList.PageSize * dtgSAPList.CurrentPageIndex)

            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objSAPRegister As New SAPRegister
            objSAPRegister = New SAPRegisterFacade(User).Retrieve(CInt(lblID.Text))

            Dim ddlSWAPPKT As DropDownList = CType(e.Item.FindControl("ddlSWAP"), DropDownList)
            Dim ddlPresentasi As DropDownList = CType(e.Item.FindControl("ddlPresentasi"), DropDownList)
            Dim ddlKonsistensi As DropDownList = CType(e.Item.FindControl("ddlKonsistensi"), DropDownList)

            Dim ddlValiditasData As DropDownList = CType(e.Item.FindControl("ddlValiditasData"), DropDownList)
            Dim txtFrekInHouseTr As TextBox = CType(e.Item.FindControl("txtFrekInHouseTr"), TextBox)
            Dim txtJmlPeserta As TextBox = CType(e.Item.FindControl("txtJmlPeserta"), TextBox)

            Dim lblSCode As Label = CType(e.Item.FindControl("lblSCode"), Label)
            Select Case lblSCode.Text
                Case KTB.DNet.Lib.WebConfig.GetValue("SlmanCode")  'salesman code
                    ddlSWAPPKT.Enabled = True
                    ddlValiditasData.Enabled = True
                Case KTB.DNet.Lib.WebConfig.GetValue("SCntCode")  'salesman counter
                    ddlSWAPPKT.Enabled = True
                    ddlValiditasData.Enabled = True
                Case KTB.DNet.Lib.WebConfig.GetValue("SSpvCode")  'supervisor
                    txtFrekInHouseTr.Enabled = True
                    txtJmlPeserta.Enabled = True
                    ddlPresentasi.Enabled = True
                Case KTB.DNet.Lib.WebConfig.GetValue("SManCode")  'salesman manager
                    ddlKonsistensi.Enabled = True
            End Select

            ddlSWAPPKT.SelectedValue = objSAPRegister.GradeSWAP
            ddlPresentasi.SelectedValue = objSAPRegister.GradePresentasi
            ddlKonsistensi.SelectedValue = objSAPRegister.GradeKonsistensi
            ddlValiditasData.SelectedValue = objSAPRegister.GradeKelengkapan
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        dtgSAPList.CurrentPageIndex = 0

        Dim arlUpdatedData As New ArrayList
        For Each items As DataGridItem In dtgSAPList.Items
            Dim lblID As Label = CType(items.FindControl("lblID"), Label)
            Dim lblSalesmanCode As Label = CType(items.FindControl("lblSalesmanCode"), Label)

            'get the value
            Dim ddlSWAPPKT As DropDownList = CType(items.FindControl("ddlSWAP"), DropDownList)
            Dim ddlPresentasi As DropDownList = CType(items.FindControl("ddlPresentasi"), DropDownList)
            Dim ddlKonsistensi As DropDownList = CType(items.FindControl("ddlKonsistensi"), DropDownList)
            Dim ddlValiditasData As DropDownList = CType(items.FindControl("ddlValiditasData"), DropDownList)
            Dim txtFrekInHouseTr As TextBox = CType(items.FindControl("txtFrekInHouseTr"), TextBox)
            Dim txtJmlPeserta As TextBox = CType(items.FindControl("txtJmlPeserta"), TextBox)

            'get SalesmanHeader
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(lblSalesmanCode.Text)

            'get the SAPRegister Object
            Dim objSAPRegister As New SAPRegister
            objSAPRegister = New SAPRegisterFacade(User).Retrieve(CInt(lblID.Text))
            objSAPRegister.SalesmanHeader = objSalesmanHeader

            If ddlSWAPPKT.SelectedIndex <> 0 Then
                objSAPRegister.GradeSWAP = ddlSWAPPKT.SelectedValue
            Else
                MessageBox.Show("Silahkan pilih penilaian SWAP yang diinginkan")
            End If

            If ddlPresentasi.SelectedIndex <> 0 Then
                objSAPRegister.GradePresentasi = ddlPresentasi.SelectedValue
            Else
                MessageBox.Show("Silahkan pilih penilaian Presentasi yang diinginkan")
            End If

            If ddlKonsistensi.SelectedIndex <> 0 Then
                objSAPRegister.GradeKonsistensi = ddlKonsistensi.SelectedValue
            Else
                MessageBox.Show("Silahkan pilih penilaian Konsistensi yang diinginkan")
            End If

            If ddlValiditasData.SelectedIndex <> 0 Then
                objSAPRegister.GradeKelengkapan = Val(ddlValiditasData.SelectedValue)
            Else
                objSAPRegister.GradeKelengkapan = 0.0
            End If

            If txtFrekInHouseTr.Text = "" Then
                objSAPRegister.GradeFrekuensi = 0.0
            Else
                objSAPRegister.GradeFrekuensi = Val(txtFrekInHouseTr.Text.Replace(",", "."))
            End If

            If txtJmlPeserta.Text = "" Then
                objSAPRegister.JumlahPeserta = 0.0
            Else
                objSAPRegister.JumlahPeserta = Val(txtJmlPeserta.Text.Replace(",", "."))
            End If
            arlUpdatedData.Add(objSAPRegister)
        Next

        If (New SAPRegisterFacade(User).UpdateTransaction(arlUpdatedData) <> -1) Then
            btnDownload.Enabled = True
            ' add security
            If Not CekResultDownloadPrivilege() Then
                btnDownload.Enabled = False
            End If
            sHelper.SetSession("ForDownload", arlUpdatedData)
            MessageBox.Show("Data berhasil disimpan")
            BindToGrid(dtgSAPList.CurrentPageIndex)
        Else
            MessageBox.Show("Data gagal disimpan")
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlData As ArrayList = CType(sHelper.GetSession("ForDownload"), ArrayList)
        btnDownload.Enabled = False
        SetDownload(arlData)
    End Sub
End Class
