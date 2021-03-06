Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls

Public Class FrmSalesDocumentList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgSalesDocumentList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlDepartement As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlJenisSurat As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatusDownload As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents icMinDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icMaxDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblDealerInfo As System.Web.UI.WebControls.Label
    Protected WithEvents txtKepada As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchUserGroup As System.Web.UI.WebControls.Label
    Protected WithEvents pnlSearchKepada As System.Web.UI.WebControls.Panel

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
    Dim sHelper As New SessionHelper
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SalesDocListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Daftar Dokumen")
        End If
    End Sub

    Private Function CmdBtnDownloadPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SalesDocListDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Property SesDealer() As Dealer
        Get
            Return _sessHelper.GetSession("DEALER")
        End Get
        Set(ByVal Value As Dealer)
            _sessHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Property SesLoginUserInfo() As UserInfo
        Get
            Return _sessHelper.GetSession("LOGINUSERINFO")
        End Get
        Set(ByVal Value As UserInfo)
            _sessHelper.SetSession("LOGINUSERINFO", Value)
        End Set
    End Property


    Private _sessHelper As New SessionHelper
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            Initialize()
        End If
    End Sub
    Private Sub Initialize()
        Dim _arrDep As ArrayList = New DepartmentFacade(User).RetrieveActiveList("Code", Sort.SortDirection.ASC)
        For Each item As Department In _arrDep
            Dim _list As New ListItem(item.Code, item.Code)
            ddlDepartement.Items.Add(_list)
        Next
        Dim _listDep As New ListItem("Pilih Departemen", "invalid")
        _listDep.Selected = True
        ddlDepartement.Items.Insert(0, _listDep)
        'remark by ery
        'refer bug 1064
        '------------------
        'For Each item As Department In _arrDep
        '    Dim _list As New ListItem(item.Description, item.Code)
        '    ddlDepartement.Items.Add(_list)
        'Next
        'Dim _listDep As New ListItem("Pilih Departemen", "invalid")
        '_listDep.Selected = True
        'ddlDepartement.Items.Insert(0, _listDep)

        Dim _arrSurat As ArrayList = New KindOfLetterFacade(User).RetrieveActiveList()
        For Each item As KindOfLetter In _arrSurat
            Dim _listSrt As New ListItem(item.Description, item.Code)
            ddlJenisSurat.Items.Add(_listSrt)
        Next
        Dim _lsurat As New ListItem("Pilih Jenis Surat", "invalid")
        _lsurat.Selected = True
        ddlJenisSurat.Items.Insert(0, _lsurat)

        CommonFunction.BindFromEnum("SPLEnum", ddlStatusDownload, Me.User, False, "Desc", "Code")
        ddlStatusDownload.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

        Dim objDealer As Dealer = SesDealer
        If objDealer Is Nothing Then
            MessageBox.Show("Failed to get dealer from session.")
            Return
        End If
        If objDealer.TitleDealer.ToLower() = EnumDealerTittle.DealerTittle.KTB.ToString().ToLower() Then
            txtDealer.Visible = True
            lblSearchDealer.Visible = True
            pnlSearchKepada.Visible = True

            lblDealerInfo.Visible = False



        Else
            txtDealer.Visible = False
            lblSearchDealer.Visible = False
            pnlSearchKepada.Visible = False

            lblDealerInfo.Visible = True
            lblDealerInfo.Text = objDealer.DealerCode.Trim() + " - " + objDealer.DealerName.Trim()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch(0)
    End Sub
    Private Sub BindSearch(ByVal indexPage As Integer)
        Dim TotalRow As Integer

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Letter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlDepartement.SelectedValue <> "invalid" Then
            criterias.opAnd(New Criteria(GetType(Letter), "Department.Code", MatchType.Exact, ddlDepartement.SelectedValue))
        End If
        If ddlJenisSurat.SelectedValue <> "invalid" Then
            criterias.opAnd(New Criteria(GetType(Letter), "KindOfLetter.Code", MatchType.Exact, ddlJenisSurat.SelectedValue))
        End If
        If ddlStatusDownload.SelectedValue <> "-1" Then
            If ddlStatusDownload.SelectedValue = 0 Then
                criterias.opAnd(New Criteria(GetType(Letter), "LastDownloadDate", MatchType.LesserOrEqual, New Date(1900, 1, 1)))
            Else
                criterias.opAnd(New Criteria(GetType(Letter), "LastDownloadDate", MatchType.GreaterOrEqual, New Date(1900, 1, 1)))
            End If
        End If

        If lblDealerInfo.Visible Then
            'specific dealer login
            'criterias.opAnd(New Criteria(GetType(Letter), "Dealer.DealerCode", MatchType.Exact, SesDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(Letter), "Penerima", MatchType.[Partial], SesLoginUserInfo.Dealer.DealerCode & "-" & SesLoginUserInfo.UserName))
        Else
            'KTB login
            If txtDealer.Text.Trim <> String.Empty Then
                Dim strDealers As String = String.Empty
                For Each item As String In txtDealer.Text.Split(";")
                    strDealers = strDealers + "'" + item + "'" + ","
                Next
                strDealers = strDealers.Substring(0, strDealers.Length - 1)
                strDealers = "(" + strDealers + ")"
                criterias.opAnd(New Criteria(GetType(Letter), "Dealer.DealerCode", MatchType.InSet, strDealers))
            End If
        End If

        If txtKepada.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(Letter), "Penerima", MatchType.InSet, "('" & txtKepada.Text.Replace(";", "','") & "')"))
        End If

        '06-Nov-2007    Deddy H     Fix refer bug 1333, today date can be counted
        'If DateDiff(DateInterval.Day, icMinDate.Value, icMaxDate.Value, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) > 0 Then
        criterias.opAnd(New Criteria(GetType(Letter), "UploadDate", MatchType.GreaterOrEqual, icMinDate.Value))
        criterias.opAnd(New Criteria(GetType(Letter), "UploadDate", MatchType.LesserOrEqual, icMaxDate.Value))
        'End If

        Dim _KindOfLetter As ArrayList = New LetterFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgSalesDocumentList.PageSize, TotalRow)
        dtgSalesDocumentList.CurrentPageIndex = indexPage
        dtgSalesDocumentList.DataSource = _KindOfLetter
        dtgSalesDocumentList.VirtualItemCount = TotalRow
        dtgSalesDocumentList.DataBind()
        _sessHelper.SetSession("SortViewLetter", criterias)
    End Sub

    Private Sub dtgSalesDocumentList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSalesDocumentList.PageIndexChanged
        dtgSalesDocumentList.CurrentPageIndex = e.NewPageIndex
        BindSearch(dtgSalesDocumentList.CurrentPageIndex)
    End Sub

    Private Sub dtgSalesDocumentList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSalesDocumentList.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dtgSalesDocumentList.SelectedIndex = -1
        dtgSalesDocumentList.CurrentPageIndex = 0
        bindGridSorting(dtgSalesDocumentList.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgSalesDocumentList.DataSource = New LetterFacade(User).RetrieveActiveList(_sessHelper.GetSession("SortViewLetter"), indexPage + 1, dtgSalesDocumentList.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dtgSalesDocumentList.VirtualItemCount = totalRow
            dtgSalesDocumentList.DataBind()
        End If

    End Sub

    Private Sub dtgSalesDocumentList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSalesDocumentList.ItemCommand
        Select Case e.CommandName
            'Case "View"
            '    'MessageBox.Show("Underconstruction")
            '    Dim id As Integer = CInt(e.Item.Cells(0).Text)
            '    sHelper.SetSession("ViewHistoryID", id)
        Case "Download"
                UpdateLastDownload(Integer.Parse(e.CommandArgument))
        End Select
    End Sub
    Private Sub UpdateLastDownload(ByVal id As Integer)
        Dim obj As New Letter
        Dim _now As Date = Now
        obj = New LetterFacade(User).Retrieve(id)
        obj.LastDownloadBy = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo).UserName
        obj.LastDownloadDate = _now

        Dim objHistory As New LetterHistory
        objHistory.Letter = obj
        objHistory.DownloadBy = obj.LastDownloadBy
        objHistory.DownloadDate = _now

        Dim result As Integer = New LetterFacade(User).UpdateTransaction(obj, objHistory)
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SALESDOCUMENT") & obj.FileName         '-- Destination file

        _sessHelper.SetSession("DownloadStatus", id)
        Response.Redirect("../Download.aspx?file=" & DestFile)

    End Sub

    Private Sub dtgSalesDocumentList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSalesDocumentList.ItemDataBound

        If Not e.Item.DataItem Is Nothing Then
            Dim lblRed As Label = CType(e.Item.FindControl("lblRed"), Label)
            Dim lblGreen As Label = CType(e.Item.FindControl("lblGreen"), Label)
            Dim obj As New Letter
            obj = CType(e.Item.DataItem, Letter)
            If Not obj.LastDownloadBy = "" Then
                lblRed.Visible = False
            Else
                lblGreen.Visible = False
            End If

            Dim id As Integer = CInt(e.Item.Cells(0).Text)
            Dim lbtnview As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lblLastDownloadDate As Label = CType(e.Item.FindControl("lblLastDownloadDate"), Label)
            If obj.LastDownloadDate < New Date(1900, 1, 1) Then
                lblLastDownloadDate.Text = ""
            Else
                lblLastDownloadDate.Text = obj.LastDownloadDate.ToString("dd/MM/yyyy")
            End If
            lbtnview.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpSalesDocumentList.aspx?id=" & id & " ','',400,500,null);")

            'cek privilige
            Dim lbtnDpwnload As LinkButton = CType(e.Item.FindControl("lbtnDpwnload"), LinkButton)
            If CmdBtnDownloadPriv() = False Then
                lbtnDpwnload.Visible = False
            Else
                lbtnDpwnload.Visible = True
            End If
        End If
    End Sub
End Class
