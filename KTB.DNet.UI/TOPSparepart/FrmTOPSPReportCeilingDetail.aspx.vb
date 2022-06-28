#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Parser
Imports System.IO
#End Region


Public Class FrmTOPSPReportCeilingDetail
    Inherits System.Web.UI.Page


#Region "Variables"
    Private _TC As TOPSPTransferCeiling
    Private _sessHelper As New SessionHelper()

    Private _sessData As String = "FrmTOPSPReportCeilingDetail._sessData"
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

        StartDate = DateAdd(DateInterval.Day, 1 - StartDate.Day, StartDate)
        EndDate = StartDate.AddMonths(1).AddDays(-1)
        ds = oTCFac.RetrieveCeilingStatus(ProductCategoryID, CreditAccount, PaymentType, StartDate, EndDate, True, True)
        Dim dt1 As DataTable

        Dim dt As DataTable
        If Not IsNothing(ds) AndAlso ds.Tables.Count >= 1 Then
            dt1 = ds.Tables(0)
            Try
                Dim dr As DataRow = dt1.Rows(4)

                AvCeiling = dr.Item("D" & EndDate.Day.ToString())
            Catch ex As Exception

            End Try
            
            Return dt1 ' ds.Tables(1)
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

    Private Sub dtgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Dim cmdName As String = e.CommandName.Trim.ToLower()

        If cmdName.StartsWith("day") Then
            Dim dt As DataTable = Me._sessHelper.GetSession(Me._sessData)
            Dim dr As DataRow = dt.Rows(e.Item.ItemIndex)
            Dim Day As Integer = Integer.Parse(cmdName.Replace("day", ""))

            Dim cTC As New CriteriaComposite(New Criteria(GetType(TOPSPTransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim oTCFac As New TOPSPTransferCeilingFacade(User)
            Dim aTCs As ArrayList
            Dim oTC As TOPSPTransferCeiling
            Dim dtTime As DateTime = DateSerial(Me.calEffective.Value.Year, Me.calEffective.Value.Month, Day)

            cTC.opAnd(New Criteria(GetType(TOPSPTransferCeiling), "ProductCategory.ID", MatchType.Exact, Me.ddlProductCategory.SelectedValue))
            cTC.opAnd(New Criteria(GetType(TOPSPTransferCeiling), "CreditAccount", MatchType.Exact, Me.txtCreditAccount.Text))
            cTC.opAnd(New Criteria(GetType(TOPSPTransferCeiling), "PaymentType", MatchType.Exact, Me.ddlPaymentType.SelectedValue))
            cTC.opAnd(New Criteria(GetType(TOPSPTransferCeiling), "EffectiveDate", MatchType.Exact, dtTime))

            aTCs = oTCFac.Retrieve(cTC)
            If aTCs.Count > 0 Then
                oTC = aTCs(0)

                Response.Redirect("FrmTOPSPReportCeilingSubDetail.aspx?ID=" & oTC.ID.ToString())
            End If
        End If
    End Sub

    Private Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound

        If e.Item.ItemType = ListItemType.Header Then
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dt As DataTable = Me._sessHelper.GetSession(Me._sessData)
            Dim dr As DataRow = dt.Rows(e.Item.ItemIndex)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblDescription As Label = e.Item.FindControl("lblDescription")
            Dim lbtn As LinkButton

            lblDescription.Text = dr.Item("Data")
            For i As Integer = 1 To 31
                lbtn = e.Item.FindControl("Day" & i.ToString())

                lbtn.Text = FormatNumber(dr.Item("D" & i.ToString()), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lbtn.CommandName = "Day" & i.ToString()
            Next
        ElseIf e.Item.ItemType = ListItemType.Footer Then

        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmTOPSPReportCeiling.aspx?IsBack=1")
    End Sub
End Class