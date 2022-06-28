#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.Helper
Imports KTB.DNET.Utility
Imports KTB.DNET.Security
Imports KTB.DNET.BusinessFacade.Transfer
Imports KTB.DNET.BusinessFacade.PO
Imports KTB.DNET.BusinessFacade.PK
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.FinishUnit
Imports KTB.DNET.BusinessFacade.SparePart
Imports KTB.DNET.Parser
Imports System.IO
#End Region


Public Class FrmTOPSPReportCeilingSubDetail
    Inherits System.Web.UI.Page


#Region "Variables"
    Private _TC As TOPSPTransferCeiling
    Private _sessHelper As New SessionHelper()

    Private _sessData As String = "FrmTOPSPReportCeilingSubDetail._sessData"
    Private _vstTotalSO As String = "_vstTotalSO"
    Private _vstTotalPayment As String = "_vstTotalPayment"
    Private _vstTotalPO As String = "_vstTotalPO"
    Private _vstTotalPlus As String = "_vstTotalPlus"
    Private _vstTotalMinus As String = "_vstTotalMinus"
#End Region

#Region "Custom"

    Private Sub InitPage()
        checkPrivilege()
        initControl()
        BindData()
    End Sub


    Private Sub initControl()
        Dim dt As Date = DateSerial(Now.Year, Now.Month, 1)

        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
        Me.txtCreditAccount.Text = ""
        Me.calEffective.Value = dt
        Me.ddlPaymentType.Items.Clear()
        Me.ddlPaymentType.Items.Add(New ListItem("Semua", -1))
        Me.ddlPaymentType.Items.Add(New ListItem(enumPaymentType.PaymentType.TOP.ToString(), enumPaymentType.PaymentType.TOP))
        Me.ddlPaymentType.Items.Add(New ListItem(enumPaymentType.PaymentType.COD.ToString(), enumPaymentType.PaymentType.COD))
        Me.ddlPaymentType.SelectedValue = -1

    End Sub


    Private Sub BindGrid()

    End Sub

    Private Sub checkPrivilege()
        Dim objDealer As Dealer = Me._sessHelper.GetSession("DEALER")

        If objDealer.Title <> 1 Then '1=KTB

        End If
    End Sub


    Private Function GetData(ByRef AvCeiling As Decimal) As DataTable
        Dim oTCFac As New TOPSPTransferCeilingFacade(User)
        Dim ds As DataSet
        Dim ProductCategoryID As Integer = 0
        Dim CreditAccount As String = ""
        Dim PaymentType As Short = 0
        Dim StartDate As Date, EndDate As Date
        Dim TCID As Integer = Request.QueryString("ID")

        If Me.ddlProductCategory.SelectedIndex > 0 Then
            ProductCategoryID = Me.ddlProductCategory.SelectedValue
        End If
        If Me.txtCreditAccount.Text.Trim <> "" Then
            CreditAccount = Me.txtCreditAccount.Text.Trim()
        End If
        If Me.ddlPaymentType.SelectedIndex > 0 Then
            PaymentType = Me.ddlPaymentType.SelectedValue
        End If
        StartDate = Me.calEffective.Value
        EndDate = Me.calEffective.Value

        ds = oTCFac.RetrieveCeilingDetail(TCID)
        Dim dt1 As DataTable

        Dim dt As DataTable
        If Not IsNothing(ds) AndAlso ds.Tables.Count >= 1 Then
            dt1 = ds.Tables(0)
            Return dt1
        Else
            MessageBox.Show("Error Reading Ceiling Data.")
            Return Nothing
        End If
    End Function


    Private Sub BindData()
        Dim ID As Integer = Request.QueryString("ID")
        Dim oTC As TOPSPTransferCeiling
        Dim oTCFac As New TOPSPTransferCeilingFacade(User)
        Dim dt As DataTable
        Dim AvCeiling As Decimal = 0

        oTC = oTCFac.Retrieve(ID)

        If Not IsNothing(oTC) AndAlso oTC.ID > 0 Then
            Me.ddlProductCategory.SelectedValue = oTC.ProductCategory.ID
            Me.ddlPaymentType.SelectedValue = oTC.PaymentType
            Me.txtCreditAccount.Text = oTC.CreditAccount
            Me.calEffective.Value = oTC.EffectiveDate
            Me.txtPlafon.Text = FormatNumber(oTC.BalanceBefore, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            dt = Me.GetData(AvCeiling)

            txtAvailableCeiling.Text = FormatNumber(AvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Me._sessHelper.SetSession(Me._sessData, dt)

            Me.dtgMain.DataSource = dt
            Me.dtgMain.DataBind()
        Else
            Me.dtgMain.DataSource = New ArrayList()
            Me.dtgMain.DataBind()
        End If

    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.InitPage()
        End If
    End Sub

    Private Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound

        If e.Item.ItemType = ListItemType.Header Then
            ViewState.Add(Me._vstTotalSO, 0)
            ViewState.Add(Me._vstTotalPayment, 0)
            ViewState.Add(Me._vstTotalPO, 0)
            ViewState.Add(Me._vstTotalPlus, 0)
            ViewState.Add(Me._vstTotalMinus, 0)
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dt As DataTable = Me._sessHelper.GetSession(Me._sessData)
            Dim dr As DataRow = dt.Rows(e.Item.ItemIndex)
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMain.CurrentPageIndex * dtgMain.PageSize)

            Dim lblSONumber As Label = e.Item.FindControl("lblSONumber")
            Dim lblRegNumber As Label = e.Item.FindControl("lblRegNumber")
            Dim lblPONumber As Label = e.Item.FindControl("lblPONumber")

            Dim lblAmountPlus As Label = e.Item.FindControl("lblAmountPlus")
            Dim lblAmountMinus As Label = e.Item.FindControl("lblAmountMinus")
            Dim Amount As Decimal = dr.Item("Amount")

            lblSONumber.Text = dr.Item("SONumber")
            lblRegNumber.Text = dr.Item("RegNumber")
            lblPONumber.Text = dr.Item("PONumber")


            If lblSONumber.Text.Trim <> String.Empty Then
                ViewState.Item(_vstTotalSO) = CType(ViewState.Item(_vstTotalSO), Decimal) + Amount
            ElseIf lblRegNumber.Text.Trim <> String.Empty Then
                ViewState.Item(_vstTotalPayment) = CType(ViewState.Item(_vstTotalPayment), Decimal) + Amount
            ElseIf lblPONumber.Text.Trim <> String.Empty Then
                ViewState.Item(_vstTotalPO) = CType(ViewState.Item(_vstTotalPO), Decimal) + Amount
            End If

            If dr.Item("IsIncome") = 1 Then
                lblAmountPlus.Text = FormatNumber(Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAmountMinus.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                ViewState.Item(Me._vstTotalPlus) = CType(ViewState.Item(_vstTotalPlus), Decimal) + CType(Amount, Decimal)
            Else
                lblAmountPlus.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAmountMinus.Text = FormatNumber(Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                ViewState.Item(Me._vstTotalMinus) = CType(ViewState.Item(_vstTotalMinus), Decimal) + CType(Amount, Decimal)
            End If

            Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
            lblDealerCode.Text = dr.Item("DealerCode")

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            Dim lblTotalSO As Label = e.Item.FindControl("lblTotalSO")
            Dim lblTotalPayment As Label = e.Item.FindControl("lblTotalPayment")
            Dim lblTotalPO As Label = e.Item.FindControl("lblTotalPO")
            Dim lblTotalPlus As Label = e.Item.FindControl("lblTotalPlus")
            Dim lblTotalMinus As Label = e.Item.FindControl("lblTotalMinus")


            lblTotalSO.Text = FormatNumber(CType(ViewState.Item(_vstTotalSO), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalPayment.Text = FormatNumber(CType(ViewState.Item(_vstTotalPayment), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalPO.Text = FormatNumber(CType(ViewState.Item(_vstTotalPO), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalPlus.Text = FormatNumber(CType(ViewState.Item(_vstTotalPlus), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalMinus.Text = FormatNumber(CType(ViewState.Item(_vstTotalMinus), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmTOPSPReportCeilingDetail.aspx?IsBack=1&ID=" & Request.QueryString("ID"))
    End Sub
End Class