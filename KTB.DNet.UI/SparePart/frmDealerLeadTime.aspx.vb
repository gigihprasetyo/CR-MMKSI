#Region "Custom Namespace Imports"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region "DotNet Namespace"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmDealerLeadTime
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents dtgDealerLeadTime As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLeadTimeRO As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLeadTimeEO As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGetDealer As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Custom Variable Declaration "
    'Private _nDealerID As Integer
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _ListDealerLeadTime As ArrayList = New ArrayList
    Private _ListDealerLeadTime2 As ArrayList = New ArrayList
    'Private _isPrintAllowed As Boolean = False
    'Private _isShowDetailAllowed As Boolean = False
#End Region

#Region " Custom Method "

    Private Sub BindTodtgDealerLeadTime(ByVal pageIndex As Integer)

        If txtDealerCode.Text.Trim <> "" Then
            Dim objDealerFind As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
            lblDealerName.Text = objDealerFind.DealerName
            lblDealerTerm.Text = objDealerFind.SearchTerm2
        Else
            If txtDealerCode.Visible Then
                lblDealerName.Text = String.Empty
                lblDealerTerm.Text = String.Empty
            End If
        End If

        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SParepartLeadTime), "ID", MatchType.Greater, 0))

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(V_SParepartLeadTime), "DealerCode", MatchType.Exact, CType(Session("DEALER"), Dealer).DealerCode))
        ElseIf (txtDealerCode.Text.Trim <> "") Then
            criterias.opAnd(New Criteria(GetType(V_SParepartLeadTime), "DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        'If (txtDealerCode.Text.Trim <> "") Then
        '    criterias.opAnd(New Criteria(GetType(V_SParepartLeadTime), "DealerCode", MatchType.InSet, "('" & Replace(txtDealerCode.Text, ";", "','") & "')"))
        'End If
        If (txtLeadTimeRO.Text.Trim <> "") Then
            criterias.opAnd(New Criteria(GetType(V_SParepartLeadTime), "RO", MatchType.Exact, txtLeadTimeRO.Text))
        End If
        If (txtLeadTimeEO.Text.Trim <> "") Then
            criterias.opAnd(New Criteria(GetType(V_SParepartLeadTime), "EO", MatchType.Exact, txtLeadTimeEO.Text))
        End If


        _ListDealerLeadTime = New V_SParepartLeadTimeFacade(User).RetrieveList(criterias, pageIndex, dtgDealerLeadTime.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        _sessHelper.SetSession("frmDealerLeadTime_Download", Nothing)

        If _ListDealerLeadTime.Count > 0 Then
            dtgDealerLeadTime.DataSource = _ListDealerLeadTime
            dtgDealerLeadTime.VirtualItemCount = totalRow

            _ListDealerLeadTime2 = New V_SParepartLeadTimeFacade(User).RetrieveList(criterias, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
            _sessHelper.SetSession("frmDealerLeadTime_Download", _ListDealerLeadTime2)

            btnDownload.Enabled = True
        Else
            dtgDealerLeadTime.DataSource = New ArrayList
            dtgDealerLeadTime.VirtualItemCount = 0
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Dealer Lead Time"))
            End If

            btnDownload.Enabled = False
        End If
        dtgDealerLeadTime.DataBind()
    End Sub


    Private Sub BindHeader()
        Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)
        If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblDealerCode.Text = ObjMainDealer.DealerCode
            lblDealerName.Text = ObjMainDealer.DealerName
            lblDealerTerm.Text = ObjMainDealer.SearchTerm2
        Else
            lblDealerCode.Text = ""
            lblDealerName.Text = ""
            lblDealerTerm.Text = ""
        End If
    End Sub

    Private Sub WriteDealerLeadTimeData(ByRef sw As StreamWriter)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        '-- Read DealerLeadTime
        Dim arrList As ArrayList = _sessHelper.GetSession("frmDealerLeadTime_Download")

        If Not IsNothing(arrList) AndAlso arrList.Count <> 0 Then

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            sw.WriteLine(itemLine.ToString())    '-- Write blank line

            itemLine.Append("Kota" & tab)
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Kota" & tab)
            itemLine.Append("Area Dealer" & tab)
            itemLine.Append("RO" & tab)
            itemLine.Append("EO" & tab)
            sw.WriteLine(itemLine.ToString())      '-- Write header


            For Each iLine As V_SParepartLeadTime In arrList

                itemLine.Remove(0, itemLine.Length)  '-- Empty line

                itemLine.Append(iLine.DealerCode & tab)
                itemLine.Append(iLine.DealerName & tab)
                itemLine.Append(iLine.CityName & tab)
                itemLine.Append(iLine.Area & tab)
                itemLine.Append(iLine.RO & tab)
                itemLine.Append(iLine.EO & tab)

                sw.WriteLine(itemLine.ToString())  '-- Write line
            Next

        End If

    End Sub


    Private Sub Download()

        Dim sFileName As String  '-- File name
        sFileName = " DealerLeadTime"

        '-- Temp file must be a randomly named file!
        Dim DealerLeadTimeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DealerLeadTimeData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(DealerLeadTimeData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDealerLeadTimeData(sw)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show(ex.Message) '"Download data gagal")
        End Try
    End Sub

    Private Sub SaveLeadTime(ByVal idDealer As Integer, ByVal intROValue As Integer, ByVal intEOValue As Integer)

        Dim result As Integer
        Dim facade As New DealerLeadTimeFacade(User)

        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(idDealer)


        'for RO
        Dim objRO As DealerLeadTime = New DealerLeadTime
        objRO.Dealer = objDealer

        objRO.TransactionType = EnumLeadTimeType.LeadTimeType.RO
        objRO.Value = intROValue

        'cek ke database based on dealer and trnstyp
        Dim criteriasRO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerLeadTime), "Dealer.ID", MatchType.Exact, objDealer.ID))
        criteriasRO.opAnd(New Criteria(GetType(DealerLeadTime), "TransactionType", MatchType.Exact, CInt(EnumLeadTimeType.LeadTimeType.RO)))

        Dim arrListRO As ArrayList = New DealerLeadTimeFacade(User).Retrieve(criteriasRO)

        If arrListRO.Count > 0 Then
            Dim oldObj As DealerLeadTime = CType(arrListRO(0), DealerLeadTime)
            oldObj.Value = objRO.Value
            result = facade.Update(oldObj)
        Else
            result = facade.Insert(objRO)
        End If


        'for EO
        Dim objEO As DealerLeadTime = New DealerLeadTime
        objEO.Dealer = objDealer

        objEO.TransactionType = EnumLeadTimeType.LeadTimeType.EO
        objEO.Value = intEOValue

        'cek ke database based on dealer and trnstyp
        Dim criteriasEO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerLeadTime), "Dealer.ID", MatchType.Exact, objDealer.ID))
        criteriasEO.opAnd(New Criteria(GetType(DealerLeadTime), "TransactionType", MatchType.Exact, CInt(EnumLeadTimeType.LeadTimeType.EO)))

        Dim arrListEO As ArrayList = New DealerLeadTimeFacade(User).Retrieve(criteriasEO)

        If arrListEO.Count > 0 Then
            Dim oldObj As DealerLeadTime = CType(arrListEO(0), DealerLeadTime)
            oldObj.Value = objEO.Value
            result = facade.Update(oldObj)
        Else
            result = facade.Insert(objEO)
        End If

        If result <> -1 Then
            MessageBox.Show(SR.SaveSuccess)
            BindTodtgDealerLeadTime(dtgDealerLeadTime.CurrentPageIndex + 1)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub SetDealerLeadTimeButton()

        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.KTB Then
            dtgDealerLeadTime.Columns.Item(8).Visible = True
        Else
            dtgDealerLeadTime.Columns.Item(8).Visible = False
        End If

    End Sub
#End Region

#Region " Event Handler"

    Private Sub InitiateAuthorization()
        Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)

        If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPO_Status_Privilege) Then
            '    Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Status Pesanan")
            'End If
            '--exclude  this privilege from Asra (BA)

            '_isShowDetailAllowed = SecurityProvider.Authorize(Context.User, SR.ViewSPPO_StatusDetail_Privilege)
            'If _isPrintAllowed = False And _isShowDetailAllowed = False Then
            '    Me.dtgDealerLeadTime.Columns(7).Visible = False
            'End If

            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCode.Visible = True

        Else
            'If Not SecurityProvider.Authorize(Context.User, SR.ENHStatusPemesananKTB_Privilege) Then
            '    Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Dealer Lead Time")
            'End If

            txtDealerCode.Visible = True
            lblSearchDealer.Visible = True
            lblDealerCode.Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
            btnGetDealer.Style("display") = "none"
            ViewState("currSortColumn") = "DealerCode"
            ViewState("currSortTable") = GetType(DealerLeadTime)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindHeader()
            dtgDealerLeadTime.DataSource = New ArrayList
            dtgDealerLeadTime.DataBind()
            BindTodtgDealerLeadTime(1)
            SetDealerLeadTimeButton()
        End If
    End Sub

    Sub dtgDealerLeadTime_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If (e.Item.ItemIndex <> -1) Then
            Dim objDealerLeadTime As V_SParepartLeadTime
            objDealerLeadTime = CType(_ListDealerLeadTime(e.Item.ItemIndex), V_SParepartLeadTime)
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgDealerLeadTime.PageSize * dtgDealerLeadTime.CurrentPageIndex)).ToString
            e.Item.Cells(2).Text = objDealerLeadTime.DealerCode
            e.Item.Cells(3).Text = objDealerLeadTime.DealerName
            e.Item.Cells(4).Text = objDealerLeadTime.CityName
            e.Item.Cells(5).Text = objDealerLeadTime.Area

            CType(e.Item.Cells(6).FindControl("txtRO"), TextBox).Text = IIf(IsDBNull(objDealerLeadTime.RO), 0, objDealerLeadTime.RO)
            CType(e.Item.Cells(7).FindControl("txtEO"), TextBox).Text = IIf(IsDBNull(objDealerLeadTime.EO), 0, objDealerLeadTime.EO)

        End If
    End Sub


    Private Sub dtgDealerLeadTime_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerLeadTime.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        dtgDealerLeadTime.CurrentPageIndex = 0
        BindTodtgDealerLeadTime(dtgDealerLeadTime.CurrentPageIndex + 1)
    End Sub

    Private Sub dtgDealerLeadTime_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerLeadTime.PageIndexChanged
        dtgDealerLeadTime.CurrentPageIndex = e.NewPageIndex
        BindTodtgDealerLeadTime(e.NewPageIndex + 1)
    End Sub

    Private Sub dtgDealerLeadTime_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDealerLeadTime.ItemCommand
        If e.CommandName = "Save" Then
            SaveLeadTime(e.Item.Cells(0).Text, CType(e.Item.Cells(6).FindControl("txtRO"), TextBox).Text, CType(e.Item.Cells(7).FindControl("txtEO"), TextBox).Text)
        End If
    End Sub

    Private Sub btnGetDealer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDealer.Click
        If txtDealerCode.Text.Length > 0 Then
            Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            lblDealerName.Text = ObjDealer.DealerName
            lblDealerTerm.Text = ObjDealer.SearchTerm2
        End If
    End Sub


    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click


        If (txtDealerCode.Text.Trim <> "") Then
            ViewState("currSortTable") = GetType(DealerLeadTime)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        Else

            ViewState("currSortColumn") = "DealerCode"
            ViewState("currSortTable") = GetType(DealerLeadTime)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        dtgDealerLeadTime.CurrentPageIndex = 0
        BindTodtgDealerLeadTime(1)


    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Download()
    End Sub

#End Region

End Class