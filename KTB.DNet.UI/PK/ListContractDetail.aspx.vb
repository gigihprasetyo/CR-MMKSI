#Region "Custom Namespace Imports"

Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Data

#End Region

Public Class ListContractDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgContractDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisMoValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodeKontrakValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorKontrakValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategoriValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblContractType As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaPesananKhusus As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaPesananKhususValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents chbxFreePPh As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblPPhLastUpdateBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPhLastUpateTime As System.Web.UI.WebControls.Label
    Protected WithEvents Label23 As System.Web.UI.WebControls.Label
    Protected WithEvents Label24 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPhLastUpdateByValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPhLastUpdateTimeValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorSPL As System.Web.UI.WebControls.Label
    Protected WithEvents ibtnDownload As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblTotalHargatebus As System.Web.UI.WebControls.Label
    Protected WithEvents Label25 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private objContractHeader As ContractHeader
    Private arlContract As ArrayList
    Private sessionHelper As New sessionHelper
    Private totalunit As Integer
    Private totalAmout As Double
    Private totalPPH As Double
    'Dim objContractDetail As ContractDetail
#End Region

#Region "Custom Method"

    Private Sub GetContractHeader()
        Dim _chid As String = Request.QueryString("id")
        objContractHeader = New ContractHeaderFacade(User).Retrieve(CInt(_chid))
        sessionHelper.SetSession("Contract", objContractHeader)
    End Sub

    Private Sub BindHeaderToForm()
        lblDealerCode.Text = objContractHeader.Dealer.DealerCode
        lblDealerName.Text = objContractHeader.Dealer.DealerName
        lblJenisMoValue.Text = CType(objContractHeader.Purpose, LookUp.enumPurpose).ToString
        lblPeriodeKontrakValue.Text = CType(CInt(objContractHeader.ContractPeriodMonth) - 1, enumMonth.Month).ToString & " " & objContractHeader.ContractPeriodYear.ToString
        lblNamaPesananKhususValue.Text = objContractHeader.ProjectName
        lblCityDealerValue.Text = objContractHeader.Dealer.City.CityName
        lblNomorKontrakValue.Text = objContractHeader.ContractNumber
        lblKategoriValue.Text = objContractHeader.Category.CategoryCode
        lblContractType.Text = CType(objContractHeader.ContractType, LookUp.EnumJenisPesanan).ToString
        chbxFreePPh.Checked = Not (CBool(objContractHeader.FreePPh22Indicator))
        If Not objContractHeader.FreePPh22LastUpdateBy Is Nothing AndAlso objContractHeader.FreePPh22LastUpdateBy <> String.Empty Then
            lblPPhLastUpdateByValue.Text = UserInfo.Convert(objContractHeader.LastUpdateBy)
        End If
        lblPPhLastUpdateTimeValue.Text = IIf(objContractHeader.FreePPh22LastUpdateTime > New Date(1900, 1, 1), Format(objContractHeader.FreePPh22LastUpdateTime, "dd MMM yyyy HH-mm-ss"), String.Empty)
        lblNomorSPL.Text = objContractHeader.SPLNumber
        Dim objDealer As Dealer = Session("DEALER")
        If objContractHeader.Purpose = LookUp.enumPurpose.Biasa OrElse objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Label20.Visible = False
            Label22.Visible = False
            lblNomorSPL.Visible = False
            ibtnDownload.Visible = False
        Else
            If lblNomorSPL.Text <> String.Empty Then
                Dim objSPL As SPL = New SPLFacade(User).Retrieve(lblNomorSPL.Text)
                ibtnDownload.Visible = (SecurityProvider.Authorize(Context.User, SR.ENHSalesGeneralApplikasiDownload_Previlege) AndAlso objSPL.SPLNumber <> String.Empty)
            Else
                ibtnDownload.Visible = False
            End If
        End If
    End Sub

    Private Sub BindDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ID", MatchType.Exact, objContractHeader.ID))

        arlContract = New ContractDetailFacade(User).Retrieve(criterias)
        dgContractDetail.DataSource = arlContract
        dgContractDetail.DataBind()
        TotalAmount()
    End Sub

    Private Sub TotalAmount()
        Dim tot As Double = 0
        For Each item As ContractDetail In arlContract
            tot += (((item.PPh22 * item.ContractHeader.FreePPh22Indicator) + item.Amount) * item.TargetQty)
            'Start  :RemainModule-DailyPO:FreePPh By:Doni N
            'tot += (((item.PPh22 * IIf(item.ContractHeader.FreePPh22Indicator = 1, 0, 1)) + item.Amount) * item.TargetQty)
            'End    :RemainModule-DailyPO:FreePPh By:Doni N
        Next
        lblTotal.Text = FormatNumber(tot, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then
            GetContractHeader()
            BindHeaderToForm()
            BindDataGrid()
        End If
        'btnBack.Attributes.Add("OnClick", "window.history.go(-1)")
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarMOViewDetail_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=M/O Detail")
        End If
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.ENHPKDetailMOPPh22Set_Previlege)
        Dim PRPPh22Set As Boolean = SecurityProvider.Authorize(Context.User, SR.ENHPKDetailMOPPh22Set_Previlege)
        chbxFreePPh.Enabled = PRPPh22Set
        chbxFreePPh.Visible = PRPPh22Set
        Label19.Visible = PRPPh22Set
        Label17.Visible = PRPPh22Set
        lblPPhLastUpdateBy.Visible = PRPPh22Set
        lblPPhLastUpdateByValue.Visible = PRPPh22Set
        lblPPhLastUpateTime.Visible = PRPPh22Set
        lblPPhLastUpdateTimeValue.Visible = PRPPh22Set
        Label23.Visible = PRPPh22Set
        Label24.Visible = PRPPh22Set

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label18.Visible = isPriceVisible
        Label25.Visible = isPriceVisible
        lblTotal.Visible = isPriceVisible
        lblTotalHargatebus.Visible = isPriceVisible
        dgContractDetail.Columns(4).Visible = isPriceVisible
        dgContractDetail.Columns(5).Visible = isPriceVisible
    End Sub

    Private Sub dgContractDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgContractDetail.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As ContractDetail = CType(e.Item.DataItem, ContractDetail)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblMaterialNumber As Label = CType(e.Item.FindControl("lblMaterialNumber"), Label)
                Dim lblMaterialDescription As Label = CType(e.Item.FindControl("lblMaterialDescription"), Label)
                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                Dim lblAmountString As Label = CType(e.Item.FindControl("lblAmountString"), Label)
                Dim lblPPh22 As Label = CType(e.Item.FindControl("lblPPh22"), Label)
                Dim lblPPh22String As Label = CType(e.Item.FindControl("lblPPh22String"), Label)

                Dim vc As KTB.DNet.Domain.VechileColor = New VechileColorFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(RowValue.VechileColor.ID) '("VechileColor.ID"))
                'Dim cd As KTB.DNet.Domain.ContractDetail = New ContractDetailFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(RowValue.ID)

                If IsNothing(vc) Then
                    lblAmountString.Text = Nothing
                    lblPPh22String.Text = Nothing
                    lblMaterialNumber.Text = Nothing
                    lblMaterialDescription.Text = Nothing
                Else
                    lblAmountString.Text = String.Format("{0:#,###}", RowValue.TargetQty * lblAmount.Text)
                    lblPPh22String.Text = String.Format("{0:#,###}", RowValue.ContractHeader.FreePPh22Indicator * RowValue.TargetQty * lblPPh22.Text)
                    'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                    'lblPPh22String.Text = String.Format("{0:#,###}", IIf(RowValue.ContractHeader.FreePPh22Indicator = 1, 0, 1) * RowValue.TargetQty * lblPPh22.Text)
                    'End    :RemainModule-DailyPO:FreePPh By:Doni N
                    lblMaterialNumber.Text = vc.MaterialNumber
                    lblMaterialDescription.Text = vc.MaterialDescription
                End If
                totalunit += RowValue.TargetQty
                totalAmout += RowValue.Amount * RowValue.TargetQty
                totalPPH += RowValue.ContractHeader.FreePPh22Indicator * RowValue.PPh22 * RowValue.TargetQty
                'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                'totalPPH += IIf(RowValue.ContractHeader.FreePPh22Indicator = 1, 0, 1) * RowValue.PPh22 * RowValue.TargetQty
                'End    :RemainModule-DailyPO:FreePPh By:Doni N
            
            End If
        End If

        If ItemType = ListItemType.Footer Then
            e.Item.Cells(3).Text = FormatNumber(totalunit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(4).Text = FormatNumber(totalAmout, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(5).Text = FormatNumber(totalPPH, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        objContractHeader = sessionHelper.GetSession("Contract")
        objContractHeader.FreePPh22Indicator = CInt(Not (chbxFreePPh.Checked)) * -1
        objContractHeader.FreePPh22LastUpdateBy = User.Identity.Name
        objContractHeader.FreePPh22LastUpdateTime = DateTime.Now

        Dim objContractFacade As New ContractHeaderFacade(User)
        objContractFacade.Update(objContractHeader)
        MessageBox.Show("Data Berhasil Disimpan")
        GetContractHeader()
        BindHeaderToForm()
        BindDataGrid()
    End Sub

    Private Sub ibtnDownload_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnDownload.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.Exact, lblNomorSPL.Text.Trim()))
        Dim arrList As ArrayList = New SPLFacade(User).Retrieve(criterias)
        If arrList.Count > 0 Then
            Dim ObjSPL As SPL = CType(arrList(0), SPL)
            Dim file As String = ObjSPL.Attachment
            Dim fInfo As New System.IO.FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & file)
            'If fInfo.Exists Then
            Try
                Response.Redirect("../Download.aspx?file=" & file)
            Catch ex As Exception
                MessageBox.Show(SR.DownloadFail(file))
            End Try
            'Else
            '    MessageBox.Show(SR.FileNotFound(fInfo.Name))
            'End If
        Else
        MessageBox.Show(SR.DataNotFound("SPL Number"))
        End If
    End Sub

#End Region

    
End Class