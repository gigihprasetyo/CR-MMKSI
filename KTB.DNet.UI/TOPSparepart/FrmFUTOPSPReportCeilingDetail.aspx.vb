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


Public Class FrmFUTOPSPReportCeilingDetail
    Inherits System.Web.UI.Page



#Region "Variables"
    Private _TC As TOPSPTransferCeiling
    Private _sessHelper As New SessionHelper()

    Private _sessData As String = "FrmFUTOPSPReportCeilingDetail._sessData"
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
        ds = oTCFac.RetrieveGabunganCeilingStatus(ProductCategoryID, CreditAccount, PaymentType, StartDate, EndDate, True, True)
        Dim dt1 As DataTable

        Dim dt As DataTable
        If Not IsNothing(ds) AndAlso ds.Tables.Count >= 1 Then
            dt1 = ds.Tables(0)
            'Dim dr As DataRow = dt1.Rows(6)

            'dt1 = Sorting(dt1)

            'AvCeiling = dr.Item("D" & EndDate.Day.ToString())
            Return dt1 ' ds.Tables(1)
        Else
            MessageBox.Show("Error Reading Ceiling Data.")
            Return Nothing
        End If
    End Function


    Private Sub BindData()
        Dim ID As Integer = 0 ' Request.QueryString("ID")
        Dim oTC As New TOPSPTransferCeiling
        Dim oTCFac As New TOPSPTransferCeilingFacade(User)
        Dim dt As DataTable
        Dim AvCeiling As Decimal = 0

        '   oTC = oTCFac.Retrieve(ID)

        If Not IsNothing(oTC) AndAlso oTC.ID > 0 OrElse 1 = 1 Then
            Me.ddlProductCategory.SelectedValue = Request.QueryString("ProductCategoryID")
            Me.ddlPaymentType.SelectedValue = Request.QueryString("PaymentType")
            Me.txtCreditAccount.Text = Request.QueryString("CreditAccount")

            Dim yy As Integer = Strings.Left(Request.QueryString("EffectiveDate").ToString(), 4)
            Dim mm As Integer = Request.QueryString("EffectiveDate").ToString().Substring(4, 2)
            Dim dd As Integer = Strings.Right(Request.QueryString("EffectiveDate").ToString(), 2)

            Me.calEffective.Value = New DateTime(yy, mm, dd)
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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmFUTOPSPReportCeiling.aspx?IsBack=1")
    End Sub

    Private Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound

        If e.Item.ItemType = ListItemType.Header Then
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dt As DataTable = Me._sessHelper.GetSession(Me._sessData)
            Dim dr As DataRow = dt.Rows(e.Item.ItemIndex)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblDescription As Label = e.Item.FindControl("lblDescription")
            Dim lbtn As LinkButton
            'lblNo.Text = e.Item.ItemIndex + 1
            lblDescription.Text = dr.Item("Data")
            For i As Integer = 1 To 31
                lbtn = e.Item.FindControl("Day" & i.ToString())

                lbtn.Text = FormatNumber(dr.Item("D" & i.ToString()), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lbtn.CommandName = "Day" & i.ToString()
            Next
        ElseIf e.Item.ItemType = ListItemType.Footer Then

        End If
    End Sub

    Private Function Sorting(dt1 As DataTable) As DataTable
        For Each dr As DataRow In dt1.Rows
            Dim data As String = dr.Item("Data").ToString
            data = data.Split(".")(1)
            dr.Item("Data") = data
            Select Case dr.Item("Data").ToString.Trim
                Case Is = "Plafon FU".Trim
                    dt1.Rows.InsertAt(dr, 1)

                Case Is = "Plafon Spare Part".Trim
                    dt1.Rows.InsertAt(dr, 5)

                Case Is = "Total SO FU".Trim
                    dt1.Rows.InsertAt(dr, 2)

                Case Is = "Total Billing Spare Part".Trim
                    dt1.Rows.InsertAt(dr, 6)

                Case Is = "Payment SO FU".Trim
                    dt1.Rows.InsertAt(dr, 3)

                Case Is = "Payment Billing Spare Part".Trim
                    dt1.Rows.InsertAt(dr, 7)

                Case Is = "Available Ceiling FU".Trim
                    dt1.Rows.InsertAt(dr, 4)

                Case Is = "Available Ceiling Spare Part".Trim
                    dt1.Rows.InsertAt(dr, 8)

            End Select
        Next
        Return dt1
    End Function

End Class