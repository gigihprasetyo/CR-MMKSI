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


Public Class FrmEntryTest
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtSAPNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSAPNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgSAPList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnFieldTemp As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

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

        'If txtDealerCode.Text <> String.Empty Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        'End If

        'If ddlKategori.SelectedIndex <> 0 Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, ddlKategori.SelectedValue))
        'End If

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
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.JobPosition.ID", MatchType.Exact, ddlKategori.SelectedValue))
            'crits.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, ddlKategori.SelectedValue))
        End If


    End Sub
#End Region
#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.WritingEntryTestResultView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SAP - Entry Result Writing Test")
        End If
    End Sub

    Private Function CekTestResultCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.WritingEntryTestResultCrete_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            CommonFunction.BindJobPosition(ddlKategori, User, True, False)

            ViewState("currSortColLP") = "SalesmanHeader.Dealer.DealerCode"
            ViewState("currSortDirLP") = Sort.SortDirection.ASC

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

            ' add security
            If Not CekTestResultCreatePrivilege() Then
                btnSimpan.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        If txtSAPNo.Text = String.Empty Then
            MessageBox.Show("Masukkan No SAP terlebih dahulu")
            Exit Sub
        End If

        If hdnFieldTemp.Value = "" Then
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
        Else
            Dim strData() As String = hdnFieldTemp.Value.Split(";")
            Dim strStartPeriod As String = strData(0)
            Dim strEndPeriod As String = strData(1)

            lblStartPeriod.Text = strStartPeriod
            lblEndPeriod.Text = strEndPeriod
            dtgSAPList.CurrentPageIndex = 0
        End If
        BindToGrid(dtgSAPList.CurrentPageIndex)
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
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click


        Dim arrToUpdate As ArrayList = New ArrayList
        For Each item As DataGridItem In dtgSAPList.Items
            Dim objRegister As SAPRegister = CType(sHelper.GetSession("arlRegister"), ArrayList)(item.ItemIndex)
            Dim TxtNilai As TextBox = item.FindControl("TxtNilai")
            If TxtNilai.Text <> "" Then
                Dim nilai As Decimal
                Try
                    nilai = CDec(TxtNilai.Text.Replace(" ", "").Replace(".", ","))
                Catch
                    MessageBox.Show("Format Nilai Salah (Record ke " & item.ItemIndex + 1 & ")")
                    Exit Sub
                End Try

                If nilai > 100 Then
                    MessageBox.Show("Nilai Tidak Boleh Lebih Besar 100 (Record ke " & item.ItemIndex + 1 & ")")
                    Exit Sub
                End If
                objRegister.IsEntryTestScore = 1
                objRegister.WritingTestScore = nilai
            Else
                objRegister.IsEntryTestScore = 0
                objRegister.WritingTestScore = 0
            End If
            arrToUpdate.Add(objRegister)
        Next


        If arrToUpdate.Count = 0 Then
            MessageBox.Show("Tidak ada data")
            Exit Sub
        End If

        Dim result As Integer = New SAPRegisterFacade(User).UpdateNilai(arrToUpdate)

        If result = 1 Then
            MessageBox.Show(SR.SaveSuccess)
            BindToGrid(0)
        Else
            MessageBox.Show(SR.SaveFail)
        End If


    End Sub
End Class
