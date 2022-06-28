Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmClaimProcess
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCr As System.Web.UI.WebControls.Label
    Protected WithEvents lblNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimDate2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblD As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategoryClaim As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblFakturDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoDO As System.Web.UI.WebControls.Label
    Protected WithEvents lblDeliveryDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDateCome As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents dgClaimDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents lblIncharge As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenjelasanDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenjelasanKTB As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents dtgClaimKeterangan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents LtrPenjelasan As System.Web.UI.WebControls.Literal

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

    Sub BindClaimHeader(ByVal ID As Integer)
        Dim CH As ClaimHeader = New ClaimHeader
        CH = New Claim.ClaimHeaderFacade(User).Retrieve(ID)
        lblClaimNo.Text = CH.ClaimNo
        lblClaimDate.Text = CH.ClaimDate.ToString("dd/MM/yyyy")
        lblDealer.Text = CH.Dealer.DealerCode
        lblCity.Text = CH.Dealer.City.CityName
        lblCategoryClaim.Text = CType(CH.ClaimReason, ClaimReason).Reason
        'lblKeterangan.Text = CH.KTBNote
        lblPenjelasanDealer.Text = CH.Description
        lblPenjelasanKTB.Text = CH.KTBNote
        lblIncharge.Text = CH.ClaimReason.incharge
        lblNoFaktur.Text = CH.SparePartPOStatus.BillingNumber
        lblFakturDate.Text = CH.SparePartPOStatus.BillingDate.ToString("dd/MM/yyyy")
        LtrPenjelasan.Text = CH.Description

        If (CH.SparePartPOStatus.DeliveryDate.ToString("dd/MM/yyyy") <> "01/01/1753") Then
            lblDeliveryDate.Text = CH.SparePartPOStatus.DeliveryDate.ToString("dd/MM/yyyy")
        End If

        If CH.Dealer.DealerAdditionals.Count > 0 Then
            lblDateCome.Text = CH.SparePartPOStatus.BillingDate.AddDays(CType(CH.Dealer.DealerAdditionals(0), DealerAdditional).ClaimETA).ToString("dd/MM/yyyy")
        End If

        If CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Baru Then
            lblStatus.Text = EnumClaimProgress.ClaimProgressKTB.Baru.ToString
        ElseIf CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.BelumDikirim Then
            lblStatus.Text = EnumClaimProgress.ClaimProgressKTB.BelumDikirim.ToString
        ElseIf CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Diproses Then
            lblStatus.Text = EnumClaimProgress.ClaimProgressKTB.Diproses.ToString
        ElseIf CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Selesai Then
            lblStatus.Text = EnumClaimProgress.ClaimProgressKTB.Selesai.ToString
        ElseIf CH.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Complete_Selesai Then
            lblStatus.Text = EnumClaimProgress.ClaimProgressKTB.Complete_Selesai.ToString
        End If

        Dim strDO As String = ""
        For Each item As ClaimDetail In CH.ClaimDetails
            If item.SparePartPOStatusDetail.DONumber <> "" Then
                strDO &= item.SparePartPOStatusDetail.DONumber & ";"
            End If
        Next

        If strDO.Length <> 0 Then
            Left(strDO, strDO.Length - 1)
        End If

        lblNoDO.Text = strDO



        'If (CH.ReceivedDate.ToString("dd/MM/yyyy") <> "01/01/1753") Then
        '    lblDateCome.Text = CH.ReceivedDate.ToString("dd/MM/yyyy")
        'End If


        'If (CH.Status = EnumClaimStatus.ClaimStatus.Baru) Then
        '    lblStatus.Text = "Baru"
        'ElseIf (CH.Status = EnumClaimStatus.ClaimStatus.Dikirim) Then
        '    lblStatus.Text = "Dikirim"
        '    'ElseIf (CH.Status = EnumClaimStatus.ClaimStatus.Ditolak) Then
        '    '    lblStatus.Text = "Ditolak"
        'ElseIf (CH.Status = EnumClaimStatus.ClaimStatus.Batal) Then
        '    lblStatus.Text = "Batal"
        'ElseIf (CH.Status = EnumClaimStatus.ClaimStatus.Proses) Then
        '    lblStatus.Text = "Proses"
        'ElseIf (CH.Status = EnumClaimStatus.ClaimStatus.Selesai) Then
        '    lblStatus.Text = "Selesai"
        'End If
    End Sub

    Sub BindClaimDetails(ByVal ID As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimDetail), "ClaimHeader.ID", MatchType.Exact, ID.ToString()))
        CD = New Claim.ClaimDetailFacade(User).RetrieveActiveList(criterias, CType(ViewState("CurrentSortColumnView"), String), CType(ViewState("CurrentSortDirectView"), Sort.SortDirection))
        dgClaimDetail.DataSource = CD
        dtgClaimKeterangan.DataSource = CD
        dgClaimDetail.DataBind()
        dtgClaimKeterangan.DataBind()
    End Sub

#End Region
    Private Function CekPrivCtkFormClaimProses() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.StatusListCetakFormClaimProgress_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If CekPrivCtkFormClaimProses() Then
            Button1.Visible = True
        Else
            Button1.Visible = False
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ClaimHeaderID = CInt(Request.QueryString("ID"))
        BindClaimHeader(ClaimHeaderID)
        BindClaimDetails(ClaimHeaderID)
    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objHeader As ClaimHeader = New KTB.DNet.BusinessFacade.Claim.ClaimHeaderFacade(User).Retrieve(CInt(Request.QueryString("ID")))


        If objHeader.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Baru Then
            'objHeader.StatusKTB = EnumClaimProgress.ClaimProgressKTB.Diproses
            'objHeader.Status = EnumClaimStatus.ClaimStatus.Proses

            Dim objHistory As ClaimStatusHistory = New ClaimStatusHistory
            objHistory.ClaimHeader = objHeader
            objHistory.Status = objHeader.StatusKTB
            objHistory.NewStatus = CType(EnumClaimProgress.ClaimProgressKTB.Diproses, Byte)

            Dim arllist As ArrayList = New ArrayList

            arllist.Add(objHistory)

            Dim result As Integer = New KTB.DNet.BusinessFacade.Claim.ClaimStatusHistoryFacade(User).UpdateCHTransaction(arllist, CType(EnumClaimProgress.ClaimProgressKTB.Diproses, Byte), CType(objHeader.StatusKTB, Byte))
            lblStatus.Text = EnumClaimProgress.ClaimProgressKTB.Diproses.ToString

        End If
        RegisterStartupScript("PrintWindow", "<script>window.print()</script>")

    End Sub
End Class
