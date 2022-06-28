#Region ".Net Base Class NameSpace Imports"
Imports System.IO
#End Region
#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class FrmDisplayAnnualDiscountAchivement
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTotalAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalAmountValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgAnnualDiscountAchivement As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPeriode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPoint As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSetDropDown As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "custom Declaration"
    Private ObjAnnualDiscountAchivement As AnnualDiscountAchievement
    Private ArlAnnual As ArrayList
    Private arlAnnualDiskon As ArrayList
    Private sessionHelper As New sessionHelper
#End Region

#Region "Custom Method"

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        txtDealerCode.Attributes.Add("readonly", "readonly")
        CheckUserPrivilege()
        If Not IsPostBack Then
            Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionOne()"
            btnSetDropDown.Style("display") = "none"
            RetrieveMaster()
            InitiatePage()
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lblKodeDealerValue.Visible = True
                txtDealerCode.Visible = False
                lblSearchDealer.Visible = False
                BindToDropDownList()
            Else
                lblKodeDealerValue.Visible = False
                txtDealerCode.Visible = True
                lblSearchDealer.Visible = True
                BindToDropDownList()
            End If
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewAnnualDiscountAchivementList_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Pencapaian Annual Discount ")
            End If
            '--exclude  this privilege from Asra (BA)
            'btnCari.Visible = SecurityProvider.Authorize(Context.User, SR.SearchAnnualDiscountAchivementList_Privilege)
            btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.DownloadAnnualDiscountAchivementList_Privilege)
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.ENHAnuallDiscountKTB_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Pencapaian Annual Discount ")
            End If
        End If

    End Sub

    Private Sub RetrieveMaster()
        Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lblKodeDealerValue.Text = objDealer.DealerCode
                lblNamaDealer.Text = objDealer.DealerName
                lblSearchTerm2.Text = objDealer.SearchTerm2
                viewstate("DealerCode") = objDealer.DealerCode
            End If
        Else
            'Response.Redirect("../SessionExpired.htm")
        End If
    End Sub

    Private Sub BindToDropDownList()
        Dim ArrayListForDDl As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievementHeader), "DealerCode", MatchType.Exact, viewstate("DealerCode")))

        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("ValidateDateFrom")) Then
            sortColl.Add(New Sort(GetType(AnnualDiscountAchievementHeader), "ValidateDateFrom", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If

        ArrayListForDDl = New AnnualDiscountAchievementHeaderFacade(User).Retrieve(criterias, sortColl)
        If Not IsNothing(ArrayListForDDl) Then
            If ArrayListForDDl.Count > 0 Then
                ddlPeriode.Items.Clear()
                ddlPeriode.Enabled = True
                btnCari.Enabled = True
                Dim year As Integer = DateTime.Now.Year
                For Each item As AnnualDiscountAchievementHeader In ArrayListForDDl
                    'If item.ValidateDateFrom.Year = year Or item.ValidateDateFrom.Year = year - 1 Or item.ValidateDateFrom.Year = year + 1 Then
                    Dim listItem As New ListItem(Format(item.ValidateDateFrom, "dd/MM/yyyy"), item.ID)
                    ' & "-" & item.DealerCode & "-" & item.ID
                    ddlPeriode.Items.Add(listItem)
                    'End If
                Next
                If ddlPeriode.Items.Count = 0 Then
                    ddlPeriode.Enabled = False
                    btnCari.Enabled = False
                Else
                    ddlPeriode.Enabled = True
                    btnCari.Enabled = True
                End If
            Else
                ddlPeriode.Enabled = False
                btnCari.Enabled = False
            End If
        End If

        'Dim year As Integer = DateTime.Now.Year
        'For Each item As AnnualDiscountAchievementHeader In ArrayListForDDl
        '    If item.ValidateDateFrom.Year = year Or item.ValidateDateFrom.Year = year - 1 Or item.ValidateDateFrom.Year = year + 1 Then
        '        Dim listItem As New ListItem(Format(item.ValidateDateFrom, "dd-MM-yyyy"), item.ID)
        '        ddlPeriode.Items.Add(listItem)
        '    End If
        'Next

        Dim _id As Integer
        Dim CurrentDate As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
        For Each item As listItem In ddlPeriode.Items
            Dim strPeriode As String() = item.ToString.Split("/")
            Dim tglPeriode As New DateTime(CInt(strPeriode(2)), CInt(strPeriode(1)), CInt(strPeriode(0)))
            If tglPeriode >= CurrentDate Then
                _id = item.Value
                Exit For
            Else
                _id = item.Value
            End If
        Next
        If _id > 0 Then
            Dim obj As AnnualDiscountAchievementHeader
            ddlPeriode.SelectedValue = _id
            obj = New AnnualDiscountAchievementHeaderFacade(User).Retrieve(_id)
            txtPeriode.Text = Format(obj.ValidateDateTo, "dd/MM/yyyy")
            viewstate("ValidateTo") = txtPeriode.Text
            viewstate("ValidateFrom") = Format(obj.ValidateDateFrom, "dd/MM/yyyy")

        End If
        '     ddlPeriode_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub InitiatePage()
        txtPeriode.ReadOnly = True
        ViewState("CurrentSortColumn") = "MaterialCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub dtgAnnualDiscountAchivement_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAnnualDiscountAchivement.SortCommand
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

        dtgAnnualDiscountAchivement.SelectedIndex = -1
        dtgAnnualDiscountAchivement.CurrentPageIndex = 0
        BindDataToGrid(dtgAnnualDiscountAchivement.CurrentPageIndex)

    End Sub

    Private Sub BindDataToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievement), "AnnualDiscountAchievementHeader.DealerCode", MatchType.Exact, viewstate("DealerCode")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievement), "AnnualDiscountAchievementHeader.ID", MatchType.Exact, ddlPeriode.SelectedValue))
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievement), "AnnualDiscountAchievementHeader.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievement), "AnnualDiscountAchievementHeader.ID", MatchType.Exact, ddlPeriode.SelectedValue))
        End If

        ArlAnnual = New AnnualDiscountAchievementFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgAnnualDiscountAchivement.PageSize, _
              total, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgAnnualDiscountAchivement.DataSource = ArlAnnual
        dtgAnnualDiscountAchivement.VirtualItemCount = total
        dtgAnnualDiscountAchivement.DataBind()
        arlAnnualDiskon = New AnnualDiscountAchievementFacade(User).Retrieve(criterias)
        If arlAnnualDiskon.Count > 0 Then
            sessionHelper.SetSession("AnnualDiskon", arlAnnualDiskon)
            btnDownload.Enabled = True
        Else
            btnDownload.Enabled = False
        End If

    End Sub

    Private Sub TotalAmount()
        Dim tot As Double = 0
        For Each item As AnnualDiscountAchievement In arlAnnualDiskon
            tot = tot + item.RebateAmountThisPeriod
        Next
        lblTotalAmountValue.Text = FormatNumber(tot, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Sub

    Private Sub TotalPoint()
        Dim totPoint As Double = 0
        For Each item As AnnualDiscountAchievement In arlAnnualDiskon
            totPoint = totPoint + item.RebateQtyThisPeriod
        Next
        lblTotalPoint.Text = FormatNumber(totPoint, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgAnnualDiscountAchivement.CurrentPageIndex = 0
        BindDataToGrid(dtgAnnualDiscountAchivement.CurrentPageIndex)
        TotalAmount()
        TotalPoint()
    End Sub

    Private Sub dtgAnnualDiscountAchivement_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAnnualDiscountAchivement.PageIndexChanged
        dtgAnnualDiscountAchivement.CurrentPageIndex = e.NewPageIndex
        BindDataToGrid(dtgAnnualDiscountAchivement.CurrentPageIndex)
    End Sub

    Private Sub ddlPeriode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriode.SelectedIndexChanged
        Dim arl As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlPeriode.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievementHeader), "ID", MatchType.Exact, ddlPeriode.SelectedValue))
        End If
        arl = New AnnualDiscountAchievementHeaderFacade(User).Retrieve(criterias)
        If arl.Count > 0 Then
            If ddlPeriode.SelectedValue <> "" Then
                If Not (arl) Is Nothing Then
                    txtPeriode.Text = Format(arl(0).ValidateDateTo, "dd/MM/yyyy")
                    ViewState("ValidateTo") = txtPeriode.Text
                    viewstate("ValidateFrom") = Format(arl(0).ValidateDateFrom, "dd/MM/yyyy")
                End If
            Else
                txtPeriode.Text = String.Empty
            End If
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        arlAnnualDiskon = sessionHelper.GetSession("AnnualDiskon")
        Dim _fileHelper As New FileHelper
        Dim _fileinfo As New FileInfo(Server.MapPath(""))
        Dim str As FileInfo
        Dim success As Boolean = False
        Try
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Try
                success = imp.Start()
                If success Then
                    str = _fileHelper.TransferAnnualtofile(arlAnnualDiskon, _fileinfo, viewstate("ValidateFrom"), viewstate("ValidateTo"))
                    If Not imp Is Nothing Then
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                    Response.Redirect("../Downloadlocal.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("AnnualDiscountAchivementFileDirectory").ToString & "\" & str.Name)
                End If
            Catch ex As Exception
                MessageBox.Show("Gagal Login Ke Web Server, ")
            End Try
        Catch ex As Exception
            MessageBox.Show("Gagal Download File.")
        End Try
    End Sub

    Private Sub dtgAnnualDiscountAchivement_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAnnualDiscountAchivement.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgAnnualDiscountAchivement.CurrentPageIndex * dtgAnnualDiscountAchivement.PageSize)

            If e.Item.Cells(11).Text > 0 Then
                e.Item.Cells(11).BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF99")
            End If

            e.Item.Cells(2).Text = CType(e.Item.DataItem, AnnualDiscountAchievement).AnnualDiscountAchievementHeader.DealerCode

        End If
    End Sub

    Private Sub btnSetDropDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDropDown.Click
        viewstate("DealerCode") = txtDealerCode.Text
        Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
        lblNamaDealer.Text = ObjDealer.DealerName
        lblSearchTerm2.Text = ObjDealer.SearchTerm2
        BindToDropDownList()
    End Sub

#End Region

    
End Class