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
Imports KTB.DNet.Parser
Imports System.IO
#End Region

Public Class FrmTransferCeiling
    Inherits System.Web.UI.Page

#Region "Variables"

    Private _sessHelper As New SessionHelper()

    'Private _sessTransferControls As String = "FrmTransferCeiling._sessTransferControls"

    Private _vstCritProductCategory As String = "FrmTransferCeiling._vstCritProductCategory"
    Private _vstCritCreditAccount As String = "FrmTransferCeiling._vstCritCreditAccount"
    Private _vstCritEffectiveStart As String = "FrmTransferCeiling._vstCritEffectiveStart"
    Private _vstCritEffectiveEnd As String = "FrmTransferCeiling._vstCritEffectiveEnd"
    Private _vstCritPaymentType As String = "FrmTransferCeiling._vstCritPaymentType"


    Private _sessDtCeiling As String = "FrmTransferCeiling._sessDtCeiling"
#End Region

#Region "Custom"
    Private Sub initPage()

        checkPrivilege()
        initControl()
        BindGrid()

    End Sub

    Private Sub initControl()
        Dim dt As Date = DateSerial(Now.Year, Now.Month, 1)

        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
        Me.txtCreditAccount.Text = ""
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

        Me.ViewState.Add(Me._vstCritProductCategory, Me.ddlProductCategory.SelectedValue)
        Me.ViewState.Add(Me._vstCritCreditAccount, Me.txtCreditAccount.Text.Trim)
        Me.ViewState.Add(Me._vstCritPaymentType, Me.ddlPaymentType.SelectedValue)
        Me.ViewState.Add(Me._vstCritEffectiveStart, Me.calEffectiveStart.Value.ToString("yyyy.MM.dd"))
        Me.ViewState.Add(Me._vstCritEffectiveEnd, Me.calEffectiveEnd.Value.ToString("yyyy.MM.dd"))
    End Sub

    Private Sub LoadCriteria()
        Me.ddlProductCategory.SelectedValue = Me.ViewState.Item(Me._vstCritProductCategory)
        Me.txtCreditAccount.Text = Me.ViewState.Item(Me._vstCritCreditAccount)
        Dim dt As Date
        Try
            dt = CType(Me.ViewState.Item(Me._vstCritEffectiveStart), Date)

        Catch ex As Exception
            dt = Now
        End Try
        Me.calEffectiveStart.Value = dt
        Try
            dt = CType(Me.ViewState.Item(Me._vstCritEffectiveEnd), Date)

        Catch ex As Exception
            dt = Now
        End Try
        Me.calEffectiveEnd.Value = dt

        Me.ddlPaymentType.SelectedValue = Me.ViewState.Item(Me._vstCritPaymentType)

        Me.ddlProductCategory.Enabled = True
        Me.txtCreditAccount.Enabled = True
        Me.ddlPaymentType.Enabled = True

        Me.btnCari.Visible = True
        Me.dtgMain.Visible = True
    End Sub

    Private Function GetData() As DataTable
        Dim oTCFac As New TransferCeilingFacade(User)
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
        StartDate = Me.calEffectiveStart.Value
        EndDate = Me.calEffectiveEnd.Value

        ds = oTCFac.RetrieveCeilingStatus(ProductCategoryID, CreditAccount, PaymentType, StartDate, EndDate, True)

        Dim dt As DataTable
        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            MessageBox.Show("Error Reading Ceiling Data.")
            Return Nothing
        End If
    End Function

    
    Private Sub BindGrid()
        Dim dt As DataTable = Me.GetData()

        Me._sessHelper.SetSession(Me._sessDtCeiling, dt)

        Me.dtgMain.DataSource = dt
        Me.dtgMain.DataBind()
        Me.dtgMain.Visible = True
    End Sub

    Private Sub checkPrivilege()
        Dim objDealer As Dealer = Me._sessHelper.GetSession("DEALER")

        If objDealer.Title <> 1 Then '1=KTB

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
    End Sub

    Private Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dt As DataTable = Me._sessHelper.GetSession(Me._sessDtCeiling)

            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblID As Label = e.Item.FindControl("lblID")
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblEffectiveDate As Label = e.Item.FindControl("lblEffectiveDate")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            Dim lblAvailableCeiling As Label = e.Item.FindControl("lblAvailableCeiling")

            Dim dr As DataRow = dt.Rows(e.Item.ItemIndex)

            lblNo.Text = (e.Item.ItemIndex + 1)
            lblID.Text = dr.Item("ID")

            lblProductCategory.Text = dr.Item("ProductCategory")
            lblCreditAccount.Text = dr.Item("CreditAccount")
            lblPaymentType.Text = CType(dr.Item("PaymentType"), enumPaymentType.PaymentType).ToString()

            lblEffectiveDate.Text = CType(dr.Item("EffectiveDate"), Date).ToString("dd MMM yyyy")
            lblAvailableCeiling.Text = FormatNumber(CType(dr.Item("AvailableCeiling"), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        End If
    End Sub


    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Me.BindGrid()
    End Sub

#End Region

End Class