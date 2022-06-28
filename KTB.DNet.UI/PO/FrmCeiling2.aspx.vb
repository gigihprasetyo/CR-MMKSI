#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class FrmCeiling2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesOrg As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents txtReportDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalCredit As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents icReqDelivery As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIsSpanned As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSaveMaxTOPDate As System.Web.UI.WebControls.Button
    Protected WithEvents lblProductCategory As System.Web.UI.WebControls.Label
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgMain2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Private _sessHelper As New SessionHelper
    Private _sessCriterias As String = "FrmCeiling2._sessCriterias"
    Private _sessData As String = "FrmCeiling2._sessData"
    Private _sessData2 As String = "FrmCeiling2._sessData2"
    Private _vstTotalCeiling As String = "FrmCeiling2._vstTotalCeiling"
#End Region

#Region "Methods"

    Private Sub Initialization()
        Dim objDealer = Me._sessHelper.GetSession("DEALER")
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        Me.txtReportDate.Text = Now.ToString("dd/MMM/yyyy")
        Me.icReqDelivery.Value = Now
        Me.dtgMain2.Visible = False
        lblTotal.Visible = Me.dtgMain2.Visible
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            txtCreditAccount.Text = objDealer.CreditAccount
            txtDealerName.Text = objDealer.DealerName
            txtCreditAccount.Enabled = False
            lblSearchDealer.Visible = False
            btnSaveMaxTOPDate.Visible = False
        End If
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode)
        ddlProductCategory.Items.RemoveAt(0) ' remove pilihan "Semua"
    End Sub


    Private Sub BindLastCriteria()
        Dim sCrits As String = CType(Me._sessHelper.GetSession(Me._sessCriterias), String)

        Me.txtCreditAccount.Text = sCrits.Split("#")(0)
        Me.lblDealer.Text = sCrits.Split("#")(1)
        Me.icReqDelivery.Value = CType(sCrits.Split("#")(2), Date)
    End Sub

    Private Sub BindDTG()
        Dim oSCFac As New sp_CeilingFacade(User)
        Dim aSCs As ArrayList

        aSCs = oSCFac.RetrieveFromSP(New ProductCategory(CType(Me.ddlProductCategory.SelectedValue, Short)), Now.ToString("yyyy/MM/dd"), Me.icReqDelivery.Value.ToString("yyyy/MM/dd"), Me.txtCreditAccount.Text, 0)
        'aSCs = oSCFac.RetrieveFromSP(Now.ToString("yyyy/MM/dd"), Me.icReqDelivery.Value.ToString("yyyy/MM/dd"), "('" & txtCreditAccount.Text.Trim().Replace(";", "','") & "')") 
        Me._sessHelper.SetSession(Me._sessData, aSCs)
        Me.dtgMain2.Visible = False
        Dim aSCs2 As New ArrayList
        If 1 = 1 AndAlso aSCs.Count = 6 AndAlso CType(Me.ddlProductCategory.SelectedValue, Integer) < 1 AndAlso Me.txtCreditAccount.Text.Trim <> "" Then
            aSCs2 = InitData2()
            Me._sessHelper.SetSession(Me._sessData2, aSCs2)
        End If

        Me.dtgMain.DataSource = aSCs
        Me.dtgMain.DataBind()
        If 1 = 1 AndAlso aSCs.Count = 6 AndAlso CType(Me.ddlProductCategory.SelectedValue, Integer) < 1 AndAlso Me.txtCreditAccount.Text.Trim <> "" Then
            Me.dtgMain2.Visible = True
            Me.dtgMain2.DataSource = aSCs2
            Me.dtgMain2.DataBind()
        End If
        Me.lblTotal.Visible = Me.dtgMain2.Visible
        SaveCriteria()
        RegisterStartupScript("OpenWindow", "<script>Spanning();</script>")
    End Sub

    Private Function InitData2() As ArrayList
        Dim aSCs As ArrayList = Me._sessHelper.GetSession(Me._sessData)
        Dim aSCs2 As New ArrayList
        Dim aSCs2Ok As New ArrayList
        Dim oSC As sp_Ceiling

        For i As Integer = 0 To 2
            Dim oSC2 As sp_Ceiling
            oSC2 = New sp_Ceiling
            oSC = CType(aSCs(i), sp_Ceiling)
            oSC2.ID = oSC.ID
            oSC2.CreditAccount = oSC.CreditAccount
            oSC2.PaymentType = oSC.PaymentType
            oSC2.Ceiling = 0 ' osc.Ceiling
            oSC2.ProposedPO = 0 'osc.ProposedPO
            oSC2.LiquifiedPO = 0 ' osc.LiquifiedPO
            oSC2.OutStanding = 0 'osc.OutStanding
            osc2.OutstandingSAP = 0

            aSCs2.Add(oSC2)
        Next
        Dim oCMFac As New CreditMasterFacade(User)
        Dim oCM As CreditMaster

        For Each oSCB As sp_Ceiling In aSCs2
            For Each oSCA As sp_Ceiling In aSCs
                If oSCA.CreditAccount = oSCB.CreditAccount AndAlso oSCA.PaymentType = oSCB.PaymentType Then
                    oSCB.Ceiling += oSCA.Ceiling
                    oSCB.LiquifiedPO += oSCA.LiquifiedPO
                    oSCB.ProposedPO += oSCA.ProposedPO
                    oSCB.OutStanding += oSCA.OutStanding
                    oCM = oCMFac.Retrieve(oSCA.ID)
                    oSCB.OutstandingSAP += oCM.OutStanding
                End If
            Next
            aSCs2Ok.Add(oSCB)
        Next
        Return aSCs2Ok
    End Function

    Private Sub SaveCriteria()
        Dim sCrits As String = ""
        sCrits = Me.txtCreditAccount.Text.Trim & "#" & Me.lblDealer.Text.Trim & "#" & Me.icReqDelivery.Value.ToString("yyyy/MM/dd")

        Me._sessHelper.SetSession(Me._sessCriterias, sCrits)
    End Sub


#End Region

#Region "Handler"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 600
        If Not IsPostBack() Then
            Initialization()
            If Not IsNothing(Request.Item("IsBack")) AndAlso Request.Item("IsBack") = "1" Then
                BindLastCriteria()
                BindDTG()
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtCreditAccount.Text.Trim <> "" Then
            'If Not txtCreditAccount.Text.Trim.IsInterned(";") Then
            Dim objD As Dealer = New DealerFacade(User).Retrieve(txtCreditAccount.Text.Trim)
            If Not objD Is Nothing Then
                If objD.ID > 0 Then
                    txtDealerName.Text = objD.DealerName
                End If
            End If
            'Else
            '    txtDealerName.Text = String.Empty
            'End If
        End If
        
        BindDTG()
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            If IsNothing(Me.ViewState.Item(Me._vstTotalCeiling)) Then
                Me.ViewState.Add(Me._vstTotalCeiling, 0)
            Else
                Me.ViewState.Item(Me._vstTotalCeiling) = 0
            End If
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim aSCs As ArrayList = CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)
            Dim oSC As sp_Ceiling = CType(aSCs(e.Item.ItemIndex), sp_Ceiling)
            Dim oCM As CreditMaster = New CreditMasterFacade(User).Retrieve(oSC.ID)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            Dim lblPlafon As Label = e.Item.FindControl("lblPlafon")
            Dim lblOutStanding As Label = e.Item.FindControl("lblOutStanding")
            Dim lblOutStandingSAP As Label = e.Item.FindControl("lblOutStandingSAP")
            Dim lblAvailablePlafon As Label = e.Item.FindControl("lblAvailablePlafon")
            Dim lblPOInPropose As Label = e.Item.FindControl("lblPOInPropose")
            Dim lblLiquefiedPO As Label = e.Item.FindControl("lblLiquefiedPO")
            Dim lblAcceleratedGyro As Label = e.Item.FindControl("lblAcceleratedGyro")
            Dim lblRemainPlafon As Label = e.Item.FindControl("lblRemainPlafon")
            Dim calMaxTOPDate As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("calMaxTOPDate")
            Dim lblKeterangan As Label = e.Item.FindControl("lblKeterangan")
            Dim AvCeiling As Decimal = oSC.Ceiling - oSC.OutStanding - oSC.ProposedPO + oSC.LiquifiedPO
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")

            If oSC.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Byte) Then AvCeiling = 0
            Dim RowNumber As Integer = e.Item.ItemIndex

            lblNo.Text = (e.Item.ItemIndex + 1).ToString
            If RowNumber Mod 3 = 0 Then
                lblNo.Text = ((RowNumber / 3) + 1).ToString()
            End If
            lblCreditAccount.Text = oSC.CreditAccount
            lblProductCategory.Text = oCM.ProductCategory.Code
            lblPaymentType.Text = enumPaymentType.GetStringValue(oSC.PaymentType)
            lblPlafon.Text = FormatNumber(oSC.Ceiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblOutStanding.Text = FormatNumber(oSC.OutStanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            If Not IsNothing(oCM) AndAlso oCM.ID > 0 Then
                If oCM.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Short) Then
                    lblOutStandingSAP.Text = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else
                    lblOutStandingSAP.Text = FormatNumber(oCM.OutStanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If
            Else
                lblOutStandingSAP.Text = FormatNumber(0, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
            lblAvailablePlafon.Text = FormatNumber(oSC.Ceiling - oSC.OutStanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblPOInPropose.Text = FormatNumber(oSC.ProposedPO, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblLiquefiedPO.Text = FormatNumber(oSC.LiquifiedPO, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblRemainPlafon.Text = FormatNumber(AvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            calMaxTOPDate.Value = oSC.MaxTOPDate
            lblKeterangan.Text = ""

            If CType(oSC.PaymentType, enumPaymentType.PaymentType) = enumPaymentType.PaymentType.TOP Then
                calMaxTOPDate.Visible = True
            Else
                calMaxTOPDate.Visible = False
            End If

            Me.ViewState.Item(Me._vstTotalCeiling) = CType(Me.ViewState.Item(Me._vstTotalCeiling), Decimal) + AvCeiling
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Me.lblTotalCredit.Text = FormatNumber(CType(Me.ViewState.Item(Me._vstTotalCeiling), Decimal), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If

    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If e.CommandName.Trim.ToUpper = "Detail".ToUpper Then
            Dim aSCs As ArrayList = CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)
            Dim oSC As sp_Ceiling = CType(aSCs(e.Item.ItemIndex), sp_Ceiling)
            Dim oCM As CreditMaster = New CreditMasterFacade(User).Retrieve(oSC.ID)

            Response.Redirect("FrmCeilingDetail2.aspx?ProductCategoryID=" & oCM.ProductCategory.ID.ToString() & "&CreditAccount=" & oSC.CreditAccount & "&PaymentType=" & oSC.PaymentType & "&ReportDate=" & txtReportDate.Text & "&ReqDeliveryDate=" & icReqDelivery.Value)

            'Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            'Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            'Dim lblLiquefiedPO As Label = e.Item.FindControl("lblLiquefiedPO")

            ''Make Session with current criteria/parameters
            ''sHelper.SetSession("FrmCeiling.IsAutoBind", True)
            ''sHelper.SetSession("FrmCeiling.CreditAccount", txtCreditAccount.Text)
            ''sHelper.SetSession("FrmCeiling.ReqDeliveryDate", Me.icReqDelivery.Value)
            ''sHelper.SetSession("FrmCeiling.ReportDate", txtReportDate.Text)
            ''sHelper.SetSession("FrmCeiling.PageIndex", dtgMain.CurrentPageIndex)

            ''sHelper.SetSession("FrmCeilingDetail.LiquefiedPO", lblLiquefiedPO.Text)
            ''lblLiquefiedPO

            'Response.Redirect("FrmCeilingDetail.aspx?CreditAccount=" & lblCreditAccount.Text & "&PaymentType=" & (New enumPaymentType).GetEnumValue(lblPaymentType.Text) & "&ReportDate=" & txtReportDate.Text & "&ReqDeliveryDate=" & icReqDelivery.Value)
        End If
    End Sub

    Private Sub btnSaveMaxTOPDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveMaxTOPDate.Click
        Dim aSCs As ArrayList = Me._sessHelper.GetSession(Me._sessData)
        Dim oSC As sp_Ceiling
        Dim isAllow As Boolean
        Dim nError As Integer = 0
        Dim nSaved As Integer = 0
        Dim oCM As CreditMaster
        Dim oCMFac As New CreditMasterFacade(User)

        For Each di As DataGridItem In Me.dtgMain.Items
            Dim calMaxTOPDate As KTB.DNet.WebCC.IntiCalendar = di.FindControl("calMaxTOPDate")
            Dim lblKeterangan As Label = di.FindControl("lblKeterangan")

            isAllow = False
            oSC = aSCs(di.ItemIndex)
            If CType(oSC.PaymentType, Byte) = CType(enumPaymentType.PaymentType.TOP, Integer) Then
                If calMaxTOPDate.Value < Date.Now Then
                    lblKeterangan.Text = "Simpan gagal, tanggal validitas < tanggal hari ini"
                    nError += 1
                ElseIf calMaxTOPDate.Value >= Date.Now And calMaxTOPDate.Value < Date.Now.AddDays(42) Then
                    lblKeterangan.Text = "Simpan berhasil, tanggal validitas kurang dari 6 minggu."
                    isAllow = True
                Else
                    isAllow = True
                End If
                If isAllow Then
                    If Not IsNothing(oSC) AndAlso oSC.ID > 0 Then
                        oCM = oCMFac.Retrieve(oSC.ID)
                        oCM.MaxTOPDate = calMaxTOPDate.Value

                        If oCMFac.Update(oCM) = -1 Then
                            nError += 1
                        Else
                            nSaved += 1
                        End If
                    End If
                End If
            End If
        Next
        If aSCs.Count > 0 Then
            If nSaved > 0 Then
                MessageBox.Show(SR.SaveSuccess & IIf(nError > 0, ". " & nError & " data gagal disimpan", ""))
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub

    Private Sub dtgMain2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim aSCs As ArrayList = CType(Me._sessHelper.GetSession(Me._sessData2), ArrayList)
            Dim oSC As sp_Ceiling = CType(aSCs(e.Item.ItemIndex), sp_Ceiling)
            Dim oCM As CreditMaster = New CreditMasterFacade(User).Retrieve(oSC.ID)
            Dim lblNo As Label = e.Item.FindControl("lblNo2")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount2")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType2")
            Dim lblPlafon As Label = e.Item.FindControl("lblPlafon2")
            Dim lblOutStanding As Label = e.Item.FindControl("lblOutStanding2")
            Dim lblOutStandingSAP As Label = e.Item.FindControl("lblOutStandingSAP2")
            Dim lblAvailablePlafon As Label = e.Item.FindControl("lblAvailablePlafon2")
            Dim lblPOInPropose As Label = e.Item.FindControl("lblPOInPropose2")
            Dim lblLiquefiedPO As Label = e.Item.FindControl("lblLiquefiedPO2")
            Dim lblAcceleratedGyro As Label = e.Item.FindControl("lblAcceleratedGyro2")
            Dim lblRemainPlafon As Label = e.Item.FindControl("lblRemainPlafon2")
            Dim calMaxTOPDate As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("calMaxTOPDate2")
            Dim lblKeterangan As Label = e.Item.FindControl("lblKeterangan2")
            Dim AvCeiling As Decimal = oSC.Ceiling - oSC.OutStanding - oSC.ProposedPO + oSC.LiquifiedPO
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory2")

            If oSC.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Byte) Then AvCeiling = 0
            lblNo.Text = (e.Item.ItemIndex + 1).ToString
            lblCreditAccount.Text = oSC.CreditAccount
            Dim ProdCats As String = ""
            For Each li As ListItem In Me.ddlProductCategory.Items
                If li.Value > 0 Then
                    ProdCats &= IIf(ProdCats = "", "", "+") & li.Text
                End If
            Next
            ProdCats = "*"
            lblProductCategory.Text = ProdCats
            lblPaymentType.Text = enumPaymentType.GetStringValue(oSC.PaymentType)
            lblPlafon.Text = FormatNumber(oSC.Ceiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblOutStanding.Text = FormatNumber(oSC.OutStanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblOutStandingSAP.Text = FormatNumber((oSC.OutstandingSAP), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblAvailablePlafon.Text = FormatNumber(oSC.Ceiling - oSC.OutStanding, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblPOInPropose.Text = FormatNumber(oSC.ProposedPO, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblLiquefiedPO.Text = FormatNumber(oSC.LiquifiedPO, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblRemainPlafon.Text = FormatNumber(AvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            calMaxTOPDate.Value = oSC.MaxTOPDate
            lblKeterangan.Text = ""

            If CType(oSC.PaymentType, enumPaymentType.PaymentType) = enumPaymentType.PaymentType.TOP Then
                calMaxTOPDate.Visible = True
            Else
                calMaxTOPDate.Visible = False
            End If
            calMaxTOPDate.Enabled = False
        End If
    End Sub


#End Region
End Class


