
#Region " .NET Base Class Namespace Imports "

Imports System.IO

#End Region

#Region " Custom Namespace Imports "

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper

#End Region

Public Class FrmListAnnualDiscount
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgAnnualDiscount As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPeriodeTo As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodeFrom As System.Web.UI.WebControls.Label
    Protected WithEvents lblsd As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadXls As System.Web.UI.WebControls.Button
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Declaration"
    Private objAnnualDiscount As AnnualDiscount
    Private ArlXlsDownload As ArrayList
#End Region

#Region "Custom Method"

#End Region

#Region "Event Handlers"

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PartNo"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub dtgAnnualDiscount_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAnnualDiscount.SortCommand
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

        dtgAnnualDiscount.SelectedIndex = -1
        dtgAnnualDiscount.CurrentPageIndex = 0
        BindToDataGrid(dtgAnnualDiscount.CurrentPageIndex)

    End Sub

    Private Sub dtgAnnualDiscount_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAnnualDiscount.PageIndexChanged
        dtgAnnualDiscount.CurrentPageIndex = e.NewPageIndex
        BindToDataGrid(dtgAnnualDiscount.CurrentPageIndex)
        'BindGrid()
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not SecurityProvider.Authorize(Context.User, SR.ViewAnnualDiscountItemList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar File Annual Discount")
        End If
        If Not IsPostBack Then

            RetriveMaster()
            BindToDataGrid(dtgAnnualDiscount.CurrentPageIndex)
        End If
        btnDownloadXls.Visible = SecurityProvider.Authorize(Context.User, SR.DownloadAnnualDiscount_Privilege)
    End Sub

    Private Sub RetriveMaster()
        If Request.QueryString("To") <> String.Empty Then         '--Check ValidateTo From QueryString
            Dim periodeTo As DateTime = Request.QueryString("To")
            lblPeriodeTo.Text = Format(periodeTo, "dd-MM-yyyy")
        End If
    End Sub

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim ArlAnnualDiscount As ArrayList
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Request.QueryString("From") <> String.Empty Then       '--Check ValidateFrom From QueryString
            Dim ValidateFromString As String = Request.QueryString("From")
            Dim ValidateFrom As DateTime = System.Convert.ToDateTime(ValidateFromString)
            lblPeriodeFrom.Text = Format(ValidateFrom, "dd-MM-yyyy")
            Dim tglValidateFromAwal As New Date(CInt(ValidateFrom.Year), CInt(ValidateFrom.Month), CInt(ValidateFrom.Day), 0, 0, 0)
            Dim tglValidateFromAkhir As New Date(CInt(ValidateFrom.Year), CInt(ValidateFrom.Month), CInt(ValidateFrom.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.GreaterOrEqual, Format(tglValidateFromAwal, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.LesserOrEqual, Format(tglValidateFromAkhir, "yyyy-MM-dd HH:mm:ss")))
        End If

        ArlAnnualDiscount = New AnnualDiscountFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgAnnualDiscount.PageSize, _
              total, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgAnnualDiscount.DataSource = ArlAnnualDiscount
        dtgAnnualDiscount.VirtualItemCount = total
        dtgAnnualDiscount.DataBind()
        If ArlAnnualDiscount.Count <= 0 Then
            btnDownload.Enabled = True
        End If
    End Sub

    Private Function DownloadXls() As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Request.QueryString("From") <> String.Empty Then       '--Check ValidateFrom From QueryString
            Dim ValidateFromString As String = Request.QueryString("From")
            Dim ValidateFrom As DateTime = System.Convert.ToDateTime(ValidateFromString)
            lblPeriodeFrom.Text = Format(ValidateFrom, "dd-MM-yyyy")
            Dim tglValidateFromAwal As New Date(CInt(ValidateFrom.Year), CInt(ValidateFrom.Month), CInt(ValidateFrom.Day), 0, 0, 0)
            Dim tglValidateFromAkhir As New Date(CInt(ValidateFrom.Year), CInt(ValidateFrom.Month), CInt(ValidateFrom.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.GreaterOrEqual, Format(tglValidateFromAwal, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.LesserOrEqual, Format(tglValidateFromAkhir, "yyyy-MM-dd HH:mm:ss")))
        End If

        ArlXlsDownload = New AnnualDiscountFacade(User).Retrieve(criterias)
        Return ArlXlsDownload
    End Function

    Private Sub dtgAnnualDiscount_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAnnualDiscount.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgAnnualDiscount.CurrentPageIndex * dtgAnnualDiscount.PageSize)
        End If
    End Sub

    Private Sub btnDownloadXls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadXls.Click
        Dim _fileHelper As New FileHelper
        Dim _fileInfo As New FileInfo(Server.MapPath(""))
        Try
            DownloadXls()
            Dim str As FileInfo = _fileHelper.TransferAnnualDiscountToXLS(ArlXlsDownload, _fileInfo, Request.QueryString("From").Replace("/", ""), Request.QueryString("To").Replace("/", ""))
            Response.Redirect("../Downloadlocal.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("AnnualDiscountFileDirectory").ToString & "\" & str.Name)
        Catch ex As Exception
            MessageBox.Show("Gagal Download File.")
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("../SparePart/FrmDisplayAnnualDiscount.aspx")
    End Sub
#End Region

    
   
End Class
