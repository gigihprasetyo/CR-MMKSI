#Region " Summary "
'--------------------------------------------------------'
'-- Program Code : FrmDisplayDeposit.aspx              --'
'-- Program Name : DEPOSIT-Daftar Deposit              --'
'-- Description  :                                     --'
'--------------------------------------------------------'
'-- Programmer   : Agus Pirnadi                        --'
'-- Start Date   : Nov 14 2005                         --'
'-- Update By    :                                     --'
'-- Last Update  : Jan 06 2006                         --'
'--------------------------------------------------------'
'-- Copyright © 2005 by Intimedia                      --'
'--------------------------------------------------------'
#End Region

#Region " Custom Namespace Imports "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports Ktb.DNet.Security
Imports KTB.DNet.Utility
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

Public Class FrmPrintDeposit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblBegBalc As System.Web.UI.WebControls.Label
    Protected WithEvents lblDr As System.Web.UI.WebControls.Label
    Protected WithEvents lblCr As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndBalc As System.Web.UI.WebControls.Label
    Protected WithEvents lblBeginBalance As System.Web.UI.WebControls.Label
    Protected WithEvents lblDebit As System.Web.UI.WebControls.Label
    Protected WithEvents lblCredit As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndingBalance As System.Web.UI.WebControls.Label
    Protected WithEvents dgDepositList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDepositAwal As System.Web.UI.WebControls.Label
    Protected WithEvents lblRO As System.Web.UI.WebControls.Label
    Protected WithEvents lblGiroService As System.Web.UI.WebControls.Label
    Protected WithEvents lblService As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables "
    Private _Dealer As Dealer    '-- Dealer object
#End Region

#Region " Custom Method "
    Private Sub DisplayDealer()
        '-- Display dealer info
        If Not IsNothing(Session("DepositDEALER")) Then
            _Dealer = CType(Session("DepositDEALER"), Dealer)
            lblDealerCode.Text = _Dealer.DealerCode
            lblDealerName.Text = _Dealer.DealerName & " - " & _Dealer.SearchTerm2
        End If
    End Sub

    Private Function GetPeriod(ByVal strPeriod As String) As String
        Dim strYear As String = Right(strPeriod.Trim, 4)
        Select Case (Left(strPeriod, 2))
            Case "01"
                Return "Januari " + strYear
            Case "02"
                Return "Februari " + strYear
            Case "03"
                Return "Maret " + strYear
            Case "04"
                Return "April " + strYear
            Case "05"
                Return "Mei " + strYear
            Case "06"
                Return "Juni " + strYear
            Case "07"
                Return "Juli " + strYear
            Case "08"
                Return "Agustus " + strYear
            Case "09"
                Return "September " + strYear
            Case "10"
                Return "Oktober " + strYear
            Case "11"
                Return "November " + strYear
            Case "12"
                Return "Desember " + strYear
            Case Else
                Return strYear
        End Select
    End Function



    Private Sub BindDataToPage()
        Dim oDeposit As Deposit = CType(Session("Deposit"), Deposit)
        '-- Display header
        If Not IsNothing(oDeposit) Then
            lblBeginBalance.Text = Format(oDeposit.BegBalance, "#,##0")   '-- Saldo awal
            lblDebit.Text = Format(oDeposit.TotalDebit, "#,##0")          '-- Total debet
            lblCredit.Text = Format(oDeposit.TotalCredit, "#,##0")        '-- Total credit
            lblEndingBalance.Text = Format(oDeposit.EndBalance, "#,##0")  '-- Saldo akhir
            lblPeriod.Text = GetPeriod(oDeposit.Period)

            lblDepositAwal.Text = Format(oDeposit.AvailableDeposit, "#,##0")  '-- Deposit awal
            lblRO.Text = Format(oDeposit.RO, "#,##0")  '-- RO
            lblService.Text = Format(oDeposit.Service, "#,##0")  '-- Service
            lblGiroService.Text = Format(oDeposit.GiroReceive, "#,##0")  '-- Giro Service
        End If
        dgDepositList.DataSource = Nothing
        dgDepositList.DataSource = CType(Session("DepositList"), ArrayList)
        dgDepositList.DataBind()

    End Sub

#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then

            'If Not SecurityProvider.Authorize(Context.User, SR.ViewDeposit_Privilege) Then
            '    Server.Transfer("../FrmAccessDenied.aspx?modulName=Cetak Deposit")
            'End If

            DisplayDealer()     '-- Display dealer from login

            '-- Display grid column headers
            BindDataToPage()

        End If

        'ActivateUserPrivilege()  '-- Assign privileges
    End Sub



    Private Sub dgDepositList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDepositList.ItemDataBound
        '-- Handle data binding of template columns

        If Not e.Item.DataItem Is Nothing Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1.ToString()  '-- Column No
            End If

        End If
    End Sub


    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = (SortDirection = Sort.SortDirection.ASC)

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub dgDepositList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDepositList.SortCommand
        '-- Sort datagrid

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

        '-- Retrieve Deposit lines from session
        Dim DepositList As ArrayList = CType(Session("DepositList"), ArrayList)

        If Not IsNothing(DepositList) AndAlso DepositList.Count <> 0 Then
            SortListControl(DepositList, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            BindDataToPage()
        End If

    End Sub



#End Region

End Class
