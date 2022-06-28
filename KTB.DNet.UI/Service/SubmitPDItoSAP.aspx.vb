#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class SubmitPDItoSAP
    Inherits System.Web.UI.Page
    
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSubmPDISAP As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Private bPrivilegeSubmitPDItoSAP As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim total, dealerName As String
    Dim totRow As Integer
    'Dim sSuffix As String = New Random().Next(10000).ToString()
    Dim dt As DateTime = DateTime.Now
    Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Dim sHDownPDI As SessionHelper = New SessionHelper
    Private objDealer As Dealer
    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
#End Region

#Region "Custom Method"

    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub

    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Sub bindGrid()
        Dim arlist As ArrayList
        Dim arlist2 As New ArrayList
        Dim arlist3 As New ArrayList
        Dim objPDI As PDI
        Dim hit As Integer
        Dim srcDate As New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59, 999)
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'agus puts code
        'If chkdownload.Checked Then
        criterias2.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Proses, String)))
        'Else
        'criterias2.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Rilis, String)))
        'End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If

        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ReleaseDate", MatchType.LesserOrEqual, srcDate))
        If Me.ddlCategory.SelectedValue > -1 Then
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))
            ViewState("ChassisMaster.Category.ID") = Me.ddlCategory.SelectedValue
        Else
            ViewState("ChassisMaster.Category.ID") = Nothing
        End If
        arlist = New PDIFacade(User).Retrieve(criterias2)
        For count As Integer = 0 To arlist.Count - 1
            hit += 1
            objPDI = arlist.Item(count)
            arlist3.Add(objPDI.Dealer.ID)
            Dim retDat As String = arlist3.Item(count).ToString().Trim + " "
            Dim itemFound As Integer = 0
            For count2 As Integer = 0 To arlist2.Count - 1
                If objPDI.Dealer.ID = arlist2.Item(count2).ToString().Trim Then
                    itemFound += 1
                End If
            Next

            'If Me.ddlCategory.SelectedIndex > 0 Then
            '    Dim ObjPDI As PDI = New PDI

            '    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))
            'End If

            If itemFound <= 0 Then
                arlist2.Add(objPDI.Dealer.ID)
            End If
        Next
        total = hit.ToString.Trim
        ViewState("total") = total
        Dim whereCond As String
        If hit = 0 Then
            'agus puts code
            'If chkdownload.Checked Then
            'Mess'ageBox.Show(SR.DataNotFoundByStatus("PDI", "Proses"))
            'Else
            'MessageBox.Show(SR.DataNotFoundByStatus("PDI", "Rilis"))
            'End If
            whereCond = "('')"
        Else
            whereCond = "("
            For count As Integer = 0 To arlist2.Count - 1
                whereCond += arlist2.Item(count).ToString.Trim + ","
            Next
            whereCond = whereCond.Substring(0, whereCond.Length - 1) + ")"
        End If


        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.InSet, whereCond))
        sHDownPDI.SetSession("DealerFac", criterias)
        dtgSubmPDISAP.DataSource = New DealerFacade(User).Retrieve(criterias)
        dtgSubmPDISAP.DataBind()

    End Sub

    Private Function hitungPDI(ByVal id As Integer) As Integer
        Dim arlist As ArrayList
        Dim srcDate As New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59, 999)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "Dealer.ID", MatchType.Exact, id))
        'agus puts code
        'If chkdownload.Checked Then
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Proses, String)))
        'Else
        ' criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Rilis, String)))
        'End If
        If Not IsNothing(ViewState("ChassisMaster.Category.ID")) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.Category.ID", MatchType.Exact, ViewState("ChassisMaster.Category.ID")))

        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ReleaseDate", MatchType.LesserOrEqual, srcDate))
        arlist = New PDIFacade(User).Retrieve(criterias)
        Return arlist.Count - 1
    End Function

    Private Sub bindPDI()
        Dim srcDate As New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59, 999)
        Dim arListPDI As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'agus puts code
        'If chkdownload.Checked Then
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Proses, String)))
        'Else
        'criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Rilis, String)))
        'End If

        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PDI), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If
        If Me.ddlCategory.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ReleaseDate", MatchType.LesserOrEqual, srcDate))
        arListPDI = New PDIFacade(User).Retrieve(criterias)
        totRow = arListPDI.Count - 1
        sHDownPDI.SetSession("DowloadPDI", arListPDI)
    End Sub

    Private Sub checkFileExistenceToDownload()
        Dim product As String = Me.GetProductCategoryCode()
        Dim finfo As FileInfo = New FileInfo(Server.MapPath("") & "\..\DataTemp\PDIData" & sSuffix & "_" & product.ToLower() & ".txt")
        If finfo.Exists Then
            finfo.Delete()
        End If
    End Sub

    Private Function download(ByVal str As String)
        Dim product As String = Me.GetProductCategoryCode()
        Dim PDIData As String = Server.MapPath("") & "\..\DataTemp\PDIData" & sSuffix & "_" & product.ToLower() & ".txt"
        Dim objFileStream As New FileStream(PDIData, FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)
        objStreamWriter.WriteLine(str)
        objStreamWriter.Close()
    End Function

    Private Sub saveToFile()
        Dim product As String = Me.GetProductCategoryCode()
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\PDIData" & sSuffix & "_" & product.ToLower() & ".txt")
    End Sub

    Private Sub appendText()
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim ObjPDIFacade As PDIFacade = New PDIFacade(User)

        If totRow <> -1 Then
            checkFileExistenceToDownload()
            For count As Integer = 0 To totRow
                objAl = CType(sHDownPDI.GetSession("DowloadPDI"), ArrayList)
                Dim RowValue As PDI = CType(objAl.Item(count), PDI)
                strText = New StringBuilder

                Dim arlistDealer As ArrayList
                Dim objDealer As Dealer
                Dim criteriasDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, RowValue.Dealer.ID))
                arlistDealer = New DealerFacade(User).Retrieve(criteriasDealer)
                Dim listTitle As New EnumDealerTittle
                Dim al2 As ArrayList = listTitle.RetrieveTitle
                For Each objDealer In arlistDealer
                    'For Each item As EnumTitle In al2
                    'If item.ValTitle = objDealer.Title Then
                    'strText.Append(item.NameTitle + ". " + objDealer.DealerCode)
                    strText.Append(objDealer.DealerCode)
                    'End If
                    ' Next
                Next
                strText.Append(",")

                Dim arlistChassis As ArrayList
                Dim objChassisMaster As ChassisMaster
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.Exact, RowValue.ChassisMaster.ID))
                arlistChassis = New ChassisMasterFacade(User).Retrieve(criterias)
                For Each objChassisMaster In arlistChassis
                    strText.Append(objChassisMaster.ChassisNumber)
                Next
                strText.Append(",")

                strText.Append(RowValue.Kind.ToString)
                strText.Append(",")

                'strText.Append(RowValue.PDIDate.Date.ToString("ddMMyyyy"))
                'Modifikasi, untuk format tanggal service ddmmyyyy
                Dim tgl As Date = RowValue.PDIDate.Date '.ToShortDateString
                Dim tahun As String = tgl.Year.ToString
                Dim bulan As String = tgl.Month.ToString
                Dim tanggal As String = tgl.Day.ToString

                'Dim delimStr As String = "/"
                'Dim delimeter As Char() = delimStr.ToCharArray
                'Dim strtmp As String() = tgl.Split(delimeter)


                If Len(bulan) = 1 Then bulan = "0" & bulan
                If Len(tanggal) = 1 Then tanggal = "0" & tanggal
                strText.Append(tanggal & bulan & tahun)

                download(strText.ToString())
            Next

            Dim objPDIColl As ArrayList = CType(sHDownPDI.GetSession("DowloadPDI"), ArrayList)
            If objPDIColl.Count > 0 Then
                For Each ObjPDI As PDI In objPDIColl
                    ObjPDI.PDIStatus = "2"
                    ObjPDIFacade.Update(ObjPDI)
                Next

            End If
            bindGrid()
            btnDownload.Enabled = False
            saveToFile()
        End If


    End Sub


    Private Function GetProductCategoryCode() As String
        Dim aPDIs As ArrayList = CType(sHDownPDI.GetSession("DowloadPDI"), ArrayList)
        Dim product As String = ""

        For Each oPDI As PDI In aPDIs
            If product = "" Then
                product = oPDI.ChassisMaster.Category.ProductCategory.Code
            Else
                If product <> oPDI.ChassisMaster.Category.ProductCategory.Code Then
                    Return ""
                End If
            End If
        Next
        Return product
    End Function

    Private Sub appendTextToDowload()
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim ObjPDIFacade As PDIFacade = New PDIFacade(User)

        If totRow <> -1 Then
            checkFileExistenceToDownload()
            If Me.GetProductCategoryCode() = "" Then
                MessageBox.Show("Produk yang akan didownload ulang harus sama")
                Exit Sub
            End If
            For count As Integer = 0 To totRow
                objAl = CType(sHDownPDI.GetSession("DowloadPDI"), ArrayList)
                Dim RowValue As PDI = CType(objAl.Item(count), PDI)
                strText = New StringBuilder

                Dim arlistDealer As ArrayList
                Dim objDealer As Dealer
                Dim criteriasDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, RowValue.Dealer.ID))
                arlistDealer = New DealerFacade(User).Retrieve(criteriasDealer)
                Dim listTitle As New EnumDealerTittle
                Dim al2 As ArrayList = listTitle.RetrieveTitle
                For Each objDealer In arlistDealer
                    'For Each item As EnumTitle In al2
                    'If item.ValTitle = objDealer.Title Then
                    'strText.Append(item.NameTitle + ". " + objDealer.DealerCode)
                    strText.Append(objDealer.DealerCode)
                    'End If
                    ' Next
                Next
                strText.Append(",")

                Dim arlistChassis As ArrayList
                Dim objChassisMaster As ChassisMaster
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.Exact, RowValue.ChassisMaster.ID))
                arlistChassis = New ChassisMasterFacade(User).Retrieve(criterias)
                For Each objChassisMaster In arlistChassis
                    strText.Append(objChassisMaster.ChassisNumber)
                Next
                strText.Append(",")

                strText.Append(RowValue.Kind.ToString)
                strText.Append(",")

                'strText.Append(RowValue.PDIDate.Date.ToString("ddMMyyyy"))
                'Modifikasi, untuk format tanggal service ddmmyyyy
                Dim tgl As Date = RowValue.PDIDate.Date '.ToShortDateString
                Dim tahun As String = tgl.Year.ToString
                Dim bulan As String = tgl.Month.ToString
                Dim tanggal As String = tgl.Day.ToString
                If Len(bulan) = 1 Then bulan = "0" & bulan
                If Len(tanggal) = 1 Then tanggal = "0" & tanggal
                strText.Append(tanggal & bulan & tahun)

                strText.Append(",")
                Dim releaseDate As Date = RowValue.ReleaseDate.Date
                Dim tahunRelease As String = releaseDate.Year.ToString
                Dim bulanRelease As String = releaseDate.Month.ToString
                Dim tanggalRelease As String = releaseDate.Day.ToString
                If Len(bulanRelease) = 1 Then bulanRelease = "0" & bulanRelease
                If Len(tanggalRelease) = 1 Then tanggalRelease = "0" & tanggalRelease
                strText.Append(tanggalRelease & bulanRelease & tahunRelease)

                download(strText.ToString())
            Next

            'Dim objPDIColl As ArrayList = CType(sHDownPDI.GetSession("DowloadPDI"), ArrayList)
            'If objPDIColl.Count > 0 Then
            '    For Each ObjPDI As PDI In objPDIColl
            '        ObjPDI.PDIStatus = "2"
            '        ObjPDIFacade.Update(ObjPDI)
            '    Next

            'End If
            bindGrid()
            btnDownload.Enabled = False
            saveToFile()
        End If


    End Sub


#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not Page.IsPostBack Then
            bindDdlCategory()
            'btnPrint.Enabled = False
            btnDownload.Enabled = False
            btnDownload.Attributes.Add("onClick", "return userConfirm()")
            AssignAttributeControl()
            SetControlPrivilege()
        End If
        'bindGrid()
    End Sub

    Private Sub bindDdlCategory()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim aCs As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        Me.ddlCategory.Items.Clear()
        'Me.ddlCategory.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oC As Category In aCs
            Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
        Next
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeSubmitPDItoSAP = SecurityProvider.Authorize(Context.User, SR.PDIReleaseDownload_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.PDIReleaseView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Form Transfer PDI to SAP")
        End If
    End Sub
    Private Sub SetControlPrivilege()
        btnDownload.Visible = bPrivilegeSubmitPDItoSAP
    End Sub
    Private Sub dtgSubmPDISAP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSubmPDISAP.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)

            '-- get number
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgSubmPDISAP.CurrentPageIndex * dtgSubmPDISAP.PageSize)

            '-- get city
            Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
            If Not IsNothing(RowValue.City) Then
                lblCity.Text = RowValue.City.CityName
            End If

            '--get total PDI
            Dim RetHit As Integer = hitungPDI(RowValue.ID) + 1
            Dim lblTotalPDI As Label = CType(e.Item.FindControl("lblTotalPDI"), Label)
            lblTotalPDI.Text = RetHit.ToString.Trim

            '--Call enum title and return complete name of dealer
            Dim listTitle As New EnumDealerTittle
            Dim al2 As ArrayList = listTitle.RetrieveTitle(companyCode)
            For Each item As EnumTitle In al2
                If item.ValTitle = RowValue.Title Then
                    Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
                    lblName.Text = item.NameTitle + ". " + RowValue.DealerName
                End If
            Next
        End If

        '--Get footer item and fill
        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)
            lblTotal.Text = ViewState("total")
        End If
    End Sub

    Private Sub dtgSubmPDISAP_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSubmPDISAP.PageIndexChanged
        dtgSubmPDISAP.CurrentPageIndex = e.NewPageIndex
        dtgSubmPDISAP.DataBind()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        bindPDI()
        appendTextToDowload()
        'chkdownload.Checked = False
        btnSearch_Click(sender, e)

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindGrid()
        If dtgSubmPDISAP.Items.Count = 0 Then
            'btnPrint.Enabled = False
            btnDownload.Enabled = False
        Else
            'btnPrint.Enabled = True
            btnDownload.Enabled = True
        End If
    End Sub

#End Region

    Private Sub dtgSubmPDISAP_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSubmPDISAP.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.ASC
        End If

        dtgSubmPDISAP.SelectedIndex = -1
        dtgSubmPDISAP.CurrentPageIndex = 0
        bindGridSorting(dtgSubmPDISAP.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then

            dtgSubmPDISAP.DataSource = New DealerFacade(User).RetrieveActiveList(CType(sHDownPDI.GetSession("DealerFac"), CriteriaComposite), indexPage + 1, dtgSubmPDISAP.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dtgSubmPDISAP.VirtualItemCount = totalRow
            dtgSubmPDISAP.DataBind()

        End If

    End Sub
End Class