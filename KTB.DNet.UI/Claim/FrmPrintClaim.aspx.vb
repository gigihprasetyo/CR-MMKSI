Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports KTB.DNet.Security

Public Class FrmPrintClaim
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNoClaim1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoClaim2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimDate As System.Web.UI.WebControls.Label
    Protected WithEvents dgClaimDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblInvoiceNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblInvoiceDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDepHead As System.Web.UI.WebControls.Label
    Protected WithEvents lblFooter As System.Web.UI.WebControls.Label
    Protected WithEvents pnlPrint As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Private Variables"

    Private ClaimHeaderID As Integer
    Private CD As ArrayList = New ArrayList

#End Region

#Region "Custom Method"

    Sub BindLabel(ByVal ID As Integer)
        Dim CH As ClaimHeader = New ClaimHeader
        CH = New Claim.ClaimHeaderFacade(User).Retrieve(ID)
        lblNoClaim1.Text = CH.ClaimNo

        Dim isAllDetailRejected As Boolean = True

        Dim arl As ArrayList = CH.ClaimDetails

        For Each itemDetail As ClaimDetail In arl
            If itemDetail.StatusDetailKTB <> EnumClaimStatusDetail.ClaimStatusDetailKTB.Ditolak Then
                isAllDetailRejected = False
            End If
        Next

        If isAllDetailRejected And (CH.Status = EnumClaimStatus.ClaimStatus.Selesai Or CH.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai) Then
            Dim arlHistory As ArrayList = New ArrayList
            arlHistory = CH.ClaimStatusHistorys

            For Each item As ClaimStatusHistory In arlHistory
                If item.NewStatus = EnumClaimProgress.ClaimProgressKTB.Selesai Or item.NewStatus = EnumClaimProgress.ClaimProgressKTB.Complete_Selesai Then
                    lblDate.Text = item.CreatedTime.ToString("dd MMMM yyyy")
                End If
            Next
        Else
            If CH.FakturReturDate.Year > 1753 Then
                lblDate.Text = CH.FakturReturDate.ToString("dd MMMM yyyy")
            End If

        End If

        Dim LetterDate As Date
        If CH.StatusKTB <> EnumClaimProgress.ClaimProgressKTB.Selesai Then
        Else
        End If

        'lblDate.Text = Date.Now.ToString("dd MMMM yyyy")

        lblHeader.Text = String.Format("Kepada,<br>{0} ({1} / {2})<br>{3}<br>{4}", CH.Dealer.DealerName, CH.Dealer.DealerCode, CH.Dealer.SearchTerm2, CH.Dealer.Address, CH.Dealer.City.CityName)
        lblNoClaim2.Text = CH.ClaimNo
        lblClaimDate.Text = CH.ClaimDate.ToString("dd MMMM yyyy")

        Dim DepHead As ClaimSignofLetter = New ClaimSignofLetter
        Dim Arr1 As ArrayList = New ArrayList
        Arr1 = New Claim.ClaimSignofLetterFacade(User).RetrieveActiveList()
        If (Arr1.Count > 0) Then
            DepHead = CType(Arr1(0), ClaimSignofLetter)
            lblDepHead.Text = String.Format("<U>{0}</U><br>{1}", DepHead.Name, DepHead.Position)
        End If

        Dim Arr2 As ArrayList = New ArrayList
        Arr2 = New Claim.ClaimCCofLetterFacade(User).RetrieveActiveList()
        If (Arr2.Count > 0) Then
            Dim i As Integer = 0
            Dim sb As StringBuilder = New StringBuilder
            For Each item As ClaimCCofLetter In Arr2
                sb.Append(item.CCList & "<br>")
            Next
            lblFooter.Text = sb.ToString()
        End If
    End Sub

    Sub BindClaimDetails(ByVal ID As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ID", MatchType.Exact, ID.ToString()))
        CD = New Claim.ClaimDetailFacade(User).RetrieveActiveList(criterias, CType(ViewState("CurrentSortColumnView"), String), CType(ViewState("CurrentSortDirectView"), Sort.SortDirection))
        dgClaimDetail.DataSource = CD
        dgClaimDetail.DataBind()
    End Sub

#End Region

    Private Function CekPrivCtkFormClaim() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.StatusListCetakFormClaim_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If CekPrivCtkFormClaim() Then
            pnlPrint.Visible = True
        Else
            pnlPrint.Visible = False
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ClaimHeaderID = Request.QueryString("ID")
        BindLabel(ClaimHeaderID)
        BindClaimDetails(ClaimHeaderID)
    End Sub

    Private Sub dgClaimDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgClaimDetail.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            Dim Ch As ClaimDetail = e.Item.DataItem
            Dim lblTglFaktur As Label = e.Item.FindControl("lblTglFaktur")
            Dim lblAnswer As Label = e.Item.FindControl("lblAnswer")
            Dim ltrInvoice As Literal = e.Item.FindControl("ltrInvoice")


            If Ch.StatusDetail <> EnumClaimStatusDetail.ClaimStatusDetail.Ditolak Then
                If Ch.ClaimHeader.FakturReturDate.Year <> 1753 Then
                    lblTglFaktur.Text = Ch.ClaimHeader.FakturReturDate.ToString("dd-MM-yyyy")
                End If
                ltrInvoice.Text = Ch.ClaimHeader.FakturRetur
            Else
                lblTglFaktur.Text = ""
                ltrInvoice.Text = ""
            End If

            Select Case Ch.StatusDetail
                Case EnumClaimStatusDetail.ClaimStatusDetail.Baru
                    lblAnswer.Text = EnumClaimStatusDetail.ClaimStatusDetail.Baru.ToString
                Case EnumClaimStatusDetail.ClaimStatusDetail.Ditolak
                    lblAnswer.Text = EnumClaimStatusDetail.ClaimStatusDetail.Ditolak.ToString
                Case EnumClaimStatusDetail.ClaimStatusDetail.Retur
                    lblAnswer.Text = EnumClaimStatusDetail.ClaimStatusDetail.Retur.ToString
                Case EnumClaimStatusDetail.ClaimStatusDetail.Ditagih
                    lblAnswer.Text = EnumClaimStatusDetail.ClaimStatusDetail.Ditagih.ToString


            End Select

        End If
    End Sub
#End Region


End Class
