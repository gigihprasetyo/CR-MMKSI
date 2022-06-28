
#Region " Summary "
'--------------------------------------------------------'
'-- Program Code : FrmDisplayDepositC2.aspx            --'
'-- Program Name : DEPOSIT-Daftar Deposit C2           --'
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

Public Class FrmPrintDepositC2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

  End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents dgDepositC2List As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotDepC2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotDepositC2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDocDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblNotes As System.Web.UI.WebControls.Label
    Protected WithEvents lblRangeDate As System.Web.UI.WebControls.Label
    Protected WithEvents btnPrint As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnClose As System.Web.UI.HtmlControls.HtmlInputButton


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

        If Not IsNothing(Session("Deposit2DEALER")) Then
            _Dealer = CType(Session("Deposit2DEALER"), Dealer)
            lblDealerCode.Text = _Dealer.DealerCode
            lblDealerName.Text = _Dealer.DealerName & " - " & _Dealer.SearchTerm2
        End If
        lblRangeDate.Text = CType(Session("sessRangeDate"), String)
    End Sub

#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewDepositC2_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit-Daftar Deposit C2")
            End If

            DisplayDealer()  '-- Display dealer from login
            BindDepositC2Lines()
            
        End If

    End Sub

    Private Sub BindDepositC2Lines()
        '-- Retrieve deposit lines from session
        Dim DepositC2Lines As ArrayList = CType(Session("DepositC2Lines"), ArrayList)
        If DepositC2Lines.Count > 0 Then


            '-- Calculate DepositC2 total amount
            Dim DepoC2Amnt As Double = 0
            For Each DepoC2Line As DepositC2Line In DepositC2Lines
                DepoC2Amnt += DepoC2Line.DepositC2Amnt  '-- Sum up deposit C2 amount
            Next
            lblTotDepositC2.Text = Format(DepoC2Amnt, "#,##0")  '-- Display total of deposit C2 amount

            '-- Display grid column headers
            dgDepositC2List.DataSource = Nothing
            dgDepositC2List.DataSource = DepositC2Lines
            dgDepositC2List.DataBind()
        End If
    End Sub
    Private Sub dgDepositC2List_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDepositC2List.ItemDataBound
        '-- Handle databinding of template columns

        If Not e.Item.DataItem Is Nothing Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (e.Item.ItemIndex + 1).ToString()  '-- Column No
            End If

        End If
    End Sub


    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = (SortDirection = Sort.SortDirection.ASC)

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub dgDepositC2List_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDepositC2List.SortCommand
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

        Dim arlCompletelist As ArrayList = CType(Session("DepositC2Lines"), ArrayList)

        If Not arlCompletelist Is Nothing Then
            SortListControl(arlCompletelist, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            dgDepositC2List.DataSource = arlCompletelist
            dgDepositC2List.DataBind()
        End If
    End Sub

#End Region

End Class
