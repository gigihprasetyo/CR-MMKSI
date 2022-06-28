﻿#Region "Custom Namespace Imports"
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
Imports KTB.DNet.Parser
Imports System.IO
#End Region

Public Class FrmTransferCeilingReport
    Inherits System.Web.UI.Page

#Region "Variables"
    Private _sessHelper As New SessionHelper()

    'Private _sessTransferControls As String = "FrmTransferCeilingReport._sessTransferControls"
    Private _vstCritProductCategory As String = "FrmTransferCeilingReport._vstCritProductCategory"
    Private _vstCritCreditAccount As String = "FrmTransferCeilingReport._vstCritCreditAccount"
    Private _vstCritEffectiveStart As String = "FrmTransferCeilingReport._vstCritEffectiveStart"
    Private _vstCritEffectiveEnd As String = "FrmTransferCeilingReport._vstCritEffectiveEnd"
    Private _vstCritPaymentType As String = "FrmTransferCeilingReport._vstCritPaymentType"
    Private _sessDtCeiling As String = "FrmTransferCeilingReport._sessDtCeiling"
#End Region

#Region "Custom"
    Private Sub initPage()
        checkPrivilege()
        initControl()
        If Not IsNothing(Request.Item("IsBack")) AndAlso Request.Item("IsBack") = "1" Then
            LoadCriteria()
        End If
        BindGrid()
    End Sub

    Private Sub initControl()
        Dim dt As Date = DateSerial(Now.Year, Now.Month, 1)
        Dim companyCode As String = KTB.DNET.Lib.WebConfig.GetValue("CompanyCode")

        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode)
        ddlProductCategory.Items.RemoveAt(0)

        Me.txtCreditAccount.Text = ""
        Dim oD As Dealer = _sessHelper.GetSession("DEALER")

        If oD.Title = 1 Then 'ktb
        Else
            Me.txtCreditAccount.Text = oD.CreditAccount
            Me.txtCreditAccount.Enabled = False
        End If

        Me.calEffectiveStart.Value = dt
        Me.calEffectiveEnd.Value = dt.AddMonths(1).AddDays(-1)

        Me.ddlPaymentType.Items.Clear()
        Me.ddlPaymentType.Items.Add(New ListItem("Semua", -1))
        Me.ddlPaymentType.Items.Add(New ListItem(enumPaymentType.PaymentType.TOP.ToString(), enumPaymentType.PaymentType.TOP))
        Me.ddlPaymentType.Items.Add(New ListItem(enumPaymentType.PaymentType.COD.ToString(), enumPaymentType.PaymentType.COD))
        Me.ddlPaymentType.SelectedValue = -1

        Me.SaveCriteria()

        Me.btnCari.Visible = True
    End Sub

    Private Sub SaveCriteria()


        _sessHelper.SetSession(Me._vstCritProductCategory, Me.ddlProductCategory.SelectedValue)
        _sessHelper.SetSession(Me._vstCritCreditAccount, Me.txtCreditAccount.Text.Trim)
        _sessHelper.SetSession(Me._vstCritPaymentType, Me.ddlPaymentType.SelectedValue)
        _sessHelper.SetSession(Me._vstCritEffectiveStart, Me.calEffectiveStart.Value.ToString("yyyy.MM.dd"))
        _sessHelper.SetSession(Me._vstCritEffectiveEnd, Me.calEffectiveEnd.Value.ToString("yyyy.MM.dd"))
    End Sub

    Private Sub LoadCriteria()
        Me.ddlProductCategory.SelectedValue = _sessHelper.GetSession(Me._vstCritProductCategory)
        Me.txtCreditAccount.Text = _sessHelper.GetSession(Me._vstCritCreditAccount)
        Dim dt As Date
        Try
            dt = CType(_sessHelper.GetSession(Me._vstCritEffectiveStart), Date)
        Catch ex As Exception
            dt = Now
        End Try
        Me.calEffectiveStart.Value = dt
        Try
            dt = CType(_sessHelper.GetSession(Me._vstCritEffectiveEnd), Date)

        Catch ex As Exception
            dt = Now
        End Try
        Me.calEffectiveEnd.Value = dt

        Me.ddlPaymentType.SelectedValue = _sessHelper.GetSession(Me._vstCritPaymentType)

        Me.ddlProductCategory.Enabled = True
        Me.txtCreditAccount.Enabled = True
        Me.ddlPaymentType.Enabled = True

        Me.btnCari.Visible = True
        Me.dtgMain.Visible = True
    End Sub

    Private Function GetData() As DataTable
        Dim oTCFac As New TransferCeilingFacade(User)
        Dim ds As DataSet = New DataSet()
        Dim ProductCategoryID As Integer = 0
        Dim CreditAccount As String = ""
        Dim PaymentType As Short = 0
        Dim StartDate As Date, EndDate As Date

        If Me.ddlProductCategory.SelectedIndex >= 0 Then
            ProductCategoryID = Me.ddlProductCategory.SelectedValue
        End If
        If Me.txtCreditAccount.Text.Trim <> "" Then
            CreditAccount = Me.txtCreditAccount.Text.Trim()
        End If
        If Me.ddlPaymentType.SelectedIndex > 0 Then
            PaymentType = Me.ddlPaymentType.SelectedValue
        End If
        StartDate = Me.calEffectiveStart.Value
        EndDate = Me.calEffectiveEnd.Value

        ds = oTCFac.RetrieveCeilingStatus(ProductCategoryID, CreditAccount, PaymentType, StartDate, EndDate, True, False)

        Dim dt As DataTable
        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            MessageBox.Show("Error Reading Ceiling Data.")
            Return Nothing
        End If
    End Function


    Private Sub BindGrid()
        Dim dt1 As Date = Me.calEffectiveStart.Value
        Dim dt2 As Date = Me.calEffectiveEnd.Value
        Dim MaxDay As Integer = 1
        Dim nCol As Integer = Me.dtgMain.Columns.Count
        Dim Day As Integer

        If Me.calEffectiveStart.Value.ToString("yyyyMM") <> Me.calEffectiveEnd.Value.ToString("yyyyMM") Then
            MessageBox.Show("Period Report Harus Dalam Bulan Yang Sama.")
            Exit Sub
        End If
        Me._sessHelper.RemoveSession(Me._sessDtCeiling)
        Me.dtgMain.DataSource = Nothing
        Me.dtgMain.DataBind()
        Me.dtgMain.Visible = False

        Dim dt As DataTable = New DataTable()
        dt = Me.GetData()

        Me._sessHelper.SetSession(Me._sessDtCeiling, dt)

        Me.dtgMain.DataSource = dt
        Me.dtgMain.DataBind()
        Me.dtgMain.Visible = True

        MaxDay = dt2.Day

        For i As Integer = nCol - 2 To nCol - 2 - 30 Step -1
            Day = 0
            Try
                Day = CType(Me.dtgMain.Columns(i).HeaderText.Trim(), Integer)
            Catch ex As Exception
                Day = 0
            End Try
            If Day > MaxDay Then
                Me.dtgMain.Columns(i).Visible = False
            Else
                Me.dtgMain.Columns(i).Visible = True
            End If
        Next

        If Me.ddlPaymentType.SelectedIndex = 0 Then
            RegisterStartupScript("OpenWindow", "<script>Spanning();</script>")
        End If

    End Sub

    Private Sub checkPrivilege()
        Dim objDealer As Dealer = Me._sessHelper.GetSession("DEALER")
        If Not SecurityProvider.Authorize(Context.User, SR.lihat_ceiling_master_transfer_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales - Transfer Payment")
        End If
   
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then '1=KTB

        End If
    End Sub


#End Region

#Region "Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            initPage()
        End If
    End Sub

    Private Sub dtgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Select Case e.CommandName.Trim().ToLower()
            Case "Detail".ToLower()

        End Select
        Dim cmdName As String = e.CommandName.Trim.ToLower()

        If cmdName.StartsWith("day") Then
            Dim dt As DataTable = Me._sessHelper.GetSession(Me._sessDtCeiling)
            Dim dr As DataRow = dt.Rows(e.Item.ItemIndex)
            Dim Day As Integer = Integer.Parse(cmdName.Replace("day", ""))
            Dim oTCFac As New TransferCeilingFacade(User)
            Dim aTCs As ArrayList
            Dim oTC As TransferCeiling
            Dim cTC As New CriteriaComposite(New Criteria(GetType(TransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim dtTime As Date = DateSerial(Me.calEffectiveStart.Value.Year, Me.calEffectiveStart.Value.Month, Day)

            cTC.opAnd(New Criteria(GetType(TransferCeiling), "ProductCategory.ID", MatchType.Exact, dr.Item("ProductCategoryID")))
            cTC.opAnd(New Criteria(GetType(TransferCeiling), "CreditAccount", MatchType.Exact, dr.Item("CreditAccount")))
            cTC.opAnd(New Criteria(GetType(TransferCeiling), "PaymentType", MatchType.Exact, dr.Item("PaymentType")))
            cTC.opAnd(New Criteria(GetType(TransferCeiling), "EffectiveDate", MatchType.Exact, dtTime.ToString("yyyy.MM.dd")))

            aTCs = oTCFac.Retrieve(cTC)
            If aTCs.Count > 0 Then
                oTC = aTCs(0)
                Me.SaveCriteria()
                Response.Redirect("FrmTransferCeilingReportDetail.aspx?ID=" & oTC.ID.ToString())
            End If
        End If
    End Sub

    Private Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dt As DataTable = Me._sessHelper.GetSession(Me._sessDtCeiling)

            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")

            Dim Day1 As LinkButton = e.Item.FindControl("Day1")
            Dim Day2 As LinkButton = e.Item.FindControl("Day2")
            Dim Day3 As LinkButton = e.Item.FindControl("Day3")
            Dim Day4 As LinkButton = e.Item.FindControl("Day4")
            Dim Day5 As LinkButton = e.Item.FindControl("Day5")
            Dim Day6 As LinkButton = e.Item.FindControl("Day6")
            Dim Day7 As LinkButton = e.Item.FindControl("Day7")
            Dim Day8 As LinkButton = e.Item.FindControl("Day8")
            Dim Day9 As LinkButton = e.Item.FindControl("Day9")
            Dim Day10 As LinkButton = e.Item.FindControl("Day10")
            Dim Day11 As LinkButton = e.Item.FindControl("Day11")
            Dim Day12 As LinkButton = e.Item.FindControl("Day12")
            Dim Day13 As LinkButton = e.Item.FindControl("Day13")
            Dim Day14 As LinkButton = e.Item.FindControl("Day14")
            Dim Day15 As LinkButton = e.Item.FindControl("Day15")
            Dim Day16 As LinkButton = e.Item.FindControl("Day16")
            Dim Day17 As LinkButton = e.Item.FindControl("Day17")
            Dim Day18 As LinkButton = e.Item.FindControl("Day18")
            Dim Day19 As LinkButton = e.Item.FindControl("Day19")
            Dim Day20 As LinkButton = e.Item.FindControl("Day20")
            Dim Day21 As LinkButton = e.Item.FindControl("Day21")
            Dim Day22 As LinkButton = e.Item.FindControl("Day22")
            Dim Day23 As LinkButton = e.Item.FindControl("Day23")
            Dim Day24 As LinkButton = e.Item.FindControl("Day24")
            Dim Day25 As LinkButton = e.Item.FindControl("Day25")
            Dim Day26 As LinkButton = e.Item.FindControl("Day26")
            Dim Day27 As LinkButton = e.Item.FindControl("Day27")
            Dim Day28 As LinkButton = e.Item.FindControl("Day28")
            Dim Day29 As LinkButton = e.Item.FindControl("Day29")
            Dim Day30 As LinkButton = e.Item.FindControl("Day30")
            Dim Day31 As LinkButton = e.Item.FindControl("Day31")

            Dim lbtn As LinkButton

            For i As Integer = 1 To 31
                lbtn = e.Item.FindControl("Day" & i.ToString())
                lbtn.CommandName = "Day" & i.ToString()
            Next

            Dim dr As DataRow = dt.Rows(e.Item.ItemIndex)

            lblNo.Text = (e.Item.ItemIndex + 1)
            If Me.ddlPaymentType.SelectedIndex = 0 Then
                If e.Item.ItemIndex Mod 2 = 0 Then
                    lblNo.Text = ((e.Item.ItemIndex / 2) + 1).ToString()
                End If
            End If
            

            lblProductCategory.Text = dr.Item("ProductCategory")
            lblCreditAccount.Text = dr.Item("CreditAccount")
            lblPaymentType.Text = CType(dr.Item("PaymentType"), enumPaymentType.PaymentType).ToString()

            Day1.Text = FormatNumber(dr.Item("D1"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day2.Text = FormatNumber(dr.Item("D2"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day3.Text = FormatNumber(dr.Item("D3"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day4.Text = FormatNumber(dr.Item("D4"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day5.Text = FormatNumber(dr.Item("D5"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day6.Text = FormatNumber(dr.Item("D6"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day7.Text = FormatNumber(dr.Item("D7"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day8.Text = FormatNumber(dr.Item("D8"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day9.Text = FormatNumber(dr.Item("D9"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day10.Text = FormatNumber(dr.Item("D10"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day11.Text = FormatNumber(dr.Item("D11"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day12.Text = FormatNumber(dr.Item("D12"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day13.Text = FormatNumber(dr.Item("D13"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day14.Text = FormatNumber(dr.Item("D14"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day15.Text = FormatNumber(dr.Item("D15"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day16.Text = FormatNumber(dr.Item("D16"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day17.Text = FormatNumber(dr.Item("D17"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day18.Text = FormatNumber(dr.Item("D18"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day19.Text = FormatNumber(dr.Item("D19"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day20.Text = FormatNumber(dr.Item("D20"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day21.Text = FormatNumber(dr.Item("D21"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day22.Text = FormatNumber(dr.Item("D22"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day23.Text = FormatNumber(dr.Item("D23"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day24.Text = FormatNumber(dr.Item("D24"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day25.Text = FormatNumber(dr.Item("D25"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day26.Text = FormatNumber(dr.Item("D26"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day27.Text = FormatNumber(dr.Item("D27"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day28.Text = FormatNumber(dr.Item("D28"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day29.Text = FormatNumber(dr.Item("D29"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day30.Text = FormatNumber(dr.Item("D30"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Day31.Text = FormatNumber(dr.Item("D31"), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        End If
    End Sub


    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Me.BindGrid()
    End Sub

#End Region

End Class