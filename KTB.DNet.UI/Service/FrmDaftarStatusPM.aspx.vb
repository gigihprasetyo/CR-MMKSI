#Region " Customer Name Space "

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

Public Class FrmDaftarStatusPM
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealerBranch As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ICDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar

    Dim dt As DateTime = DateTime.Now
    Dim Suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dgPMStatus As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList

    Protected WithEvents txtChassisNo As System.Web.UI.WebControls.TextBox

    Protected WithEvents ddlPMKind As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    'Added by Julius
    Private objDealer As Dealer
    Private _sessHelper As SessionHelper = New SessionHelper

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PMStatusView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PERIODICAL MAINTENANCE - Status PM")
        End If
    End Sub

    Dim bCheckPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PMStatusDownload_Privilege)
#End Region

#Region " subs & function "
    Private Sub download()
        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim delimiter As String = ";"
        checkFileExistenceToDownload()

        objAl = CType(_sessHelper.GetSession("objPMStatus"), ArrayList)

        If objAl Is Nothing Then
            MessageBox.Show("Tidak ada data yg bisa di download")
            Return
        End If

        For count As Integer = 0 To objAl.Count - 1

            Dim objPMHeader As PMHeader = CType(objAl.Item(count), PMHeader)
            strText = New StringBuilder

            ''Chassis
            'If IsNothing(objPMHeader.ChassisMaster) Then
            '    strText.Append(delimiter)
            'Else
            '    strText.Append(objPMHeader.ChassisMaster.ChassisNumber)
            '    strText.Append(delimiter)
            'End If

            ''Service Date / Tgl PM
            'strText.Append(objPMHeader.ServiceDate.ToString("ddMMyyyy"))
            'strText.Append(delimiter)


            ''Stand KM
            'strText.Append(objPMHeader.StandKM.ToString)
            'strText.Append(delimiter)

            ''Replacement Part
            ''strText.Append(GetDetailString(objPMHeader))

            ''Dealer
            'If IsNothing(objPMHeader.Dealer) Then
            '    strText.Append(delimiter)
            'Else
            '    strText.Append(objPMHeader.Dealer.DealerCode)
            '    strText.Append(delimiter)
            'End If

            ''Jenis PM
            'If objPMHeader.PMKindCode.Trim = String.Empty Then
            '    strText.Append(objPMHeader.PMKindCode)
            'Else
            '    strText.Append("PM" & objPMHeader.PMKindCode)
            'End If

            '-----------------------------------------------------------------------
            ' Modified by Ikhsan 21 Juli 2008
            ' Requested by Peggy
            ' The text is switched between Daftar Status PM and Transfer PM to SAP
            ' Format file dealer;nomorrangka;tanggal rilis;km;
            '-----------------------------------------------------------------------

            'Dealer
            If IsNothing(objPMHeader.Dealer) Then
                strText.Append(delimiter)
            Else
                strText.Append(objPMHeader.Dealer.DealerCode)
                strText.Append(delimiter)
            End If

            'DealerBranch
            If IsNothing(objPMHeader.DealerBranch) Then
                strText.Append(delimiter)
            Else
                strText.Append(objPMHeader.DealerBranch.DealerBranchCode)
                strText.Append(delimiter)
            End If

            'Chassis
            If IsNothing(objPMHeader.ChassisMaster) Then
                strText.Append(delimiter)
            Else
                strText.Append(objPMHeader.ChassisMaster.ChassisNumber)
                strText.Append(delimiter)
            End If

            'VisitType
            If IsNothing(objPMHeader.VisitType) Then
                strText.Append(delimiter)
            Else
                strText.Append(objPMHeader.VisitType)
                strText.Append(delimiter)
            End If

            'Service Date / Tgl PM
            strText.Append(objPMHeader.ServiceDate.ToString("ddMMyyyy"))
            strText.Append(delimiter)


            'Stand KM
            strText.Append(objPMHeader.StandKM.ToString)
            strText.Append(delimiter)

            strText.Append(objPMHeader.ReleaseDate.ToString("ddMMyyyy"))
            strText.Append(delimiter)

            'WorkOrderNumber
            If IsNothing(objPMHeader.WorkOrderNumber) Then
                strText.Append(delimiter)
            Else
                strText.Append(objPMHeader.WorkOrderNumber)
                strText.Append(delimiter)
            End If

            Try
                saveToTextFile(strText.ToString())
            Catch
                MessageBox.Show("Persiapan Proses Download gagal")
                Return
            End Try
        Next

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\PMData" & Suffix & ".txt")

        MessageBox.Show("Data Telah Disimpan")
    End Sub
    Private Function GetDetailString(ByRef obj As PMHeader) As String
        Dim delim As String = "-"
        Dim result As String = String.Empty
        If Not obj.PMDetails Is Nothing Then
            For Each objDetail As PMDetail In obj.PMDetails
                result = result + objDetail.ReplecementPartMaster.Code + delim
            Next
        End If

        If result <> String.Empty Then
            result = result.Substring(0, result.Length - 1)
        End If

        Return result
    End Function
    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\PMData" & Suffix & ".txt")
        If finfo.Exists Then
            finfo.Delete()
        End If
    End Sub
    Private Sub saveToTextFile(ByVal str As String)

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\PMData" & Suffix & ".txt", FileMode.Append, FileAccess.Write)
                Dim objStreamWriter As New StreamWriter(objFileStream)

                objStreamWriter.WriteLine(str)
                objStreamWriter.Close()

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Private Function seperatePopUpReturn(ByVal sDealerCodeCollumn As String)
        Dim sDealerCodeTemp() As String = sDealerCodeCollumn.Split(New Char() {";"})
        Dim sDealerCode As String = ""
        For i As Integer = 0 To sDealerCodeTemp.Length - 1
            sDealerCode = sDealerCode & "'" & sDealerCodeTemp(i).Trim() & "'"

            If Not (i = sDealerCodeTemp.Length - 1) Then
                sDealerCode = sDealerCode & ","
            End If
        Next
        sDealerCode = "(" & sDealerCode & ")"
        Return sDealerCode
    End Function
    Private Sub DataBindGrid()
        Dim objPMHeaderCol As ArrayList = _sessHelper.GetSession("objPMStatus")
        If objPMHeaderCol Is Nothing Or objPMHeaderCol.Count <= 0 Then
            dgPMStatus.DataSource = Nothing
            dgPMStatus.DataBind()
            btnDownload.Enabled = False
            Exit Sub
        End If
        'Sort
        'Dim isAsc As Boolean = ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'Dim iCmp As IComparer = New ListComparer(isAsc, ViewState("CurrentSortColumn"))
        'objFreeService.Sort(iCmp)

        dgPMStatus.DataSource = objPMHeaderCol
        dgPMStatus.DataBind()
        btnDownload.Enabled = bCheckPriv
    End Sub
    Private Function RetriveDataAndSaveToCache() As Integer
        _sessHelper.RemoveSession("objPMStatus")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")

        'Dim dealerCollectionString As String = String.Empty
        'If txtKodeDealer.Text <> String.Empty Then
        '    dealerCollectionString = dealerCollectionString & txtKodeDealer.Text.Trim.Replace(";", "','")
        'End If
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    dealerCollectionString = dealerCollectionString & "','"
        'End If
        'If dealerCollectionString.Trim.Length > 0 Then
        '    If dealerCollectionString.Substring(0, 3) = "','" Then
        '        dealerCollectionString = dealerCollectionString.Substring(3, dealerCollectionString.Length - 3)
        '    End If
        'End If
        'If dealerCollectionString <> String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "Dealer.DealerCode", MatchType.InSet, "('" & dealerCollectionString & "')"))
        'End If
        criterias.opAnd(New Criteria(GetType(PMHeader), "PMStatus", MatchType.No, CType(EnumPMStatus.PMStatus.Baru, Short)))

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(PMHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtKodeDealer.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(PMHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtKodeDealer.Text, ";", "','") + "')"))
            End If
        Else
            If (txtKodeDealer.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtKodeDealer.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(PMHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtKodeDealer.Text, ";", "','") + "')"))
                Else
                    Return -3
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(PMHeader), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
        End If

        If (txtKodeDealerBranch.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(PMHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" + Replace(txtKodeDealerBranch.Text, ";", "','") + "')"))
        End If

        If Not validateCriteria(criterias) Then
            Return -1
        End If

        ''LOC 2014-09-04
        '' By ali
        '' Desc : Categori Code
        If CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.KTB AndAlso ddlCategory.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        ''END OF LOC 2014-09-04

        Dim objPMStatusCol As ArrayList

        Dim sortColl As SortCollection = New SortCollection
        'sortColl.Add(New Sort(GetType(ChassisMaster), "ChassisNumber", Sort.SortDirection.ASC))  '-- Nomor chassis
        sortColl.Add(New Sort(GetType(PMHeader), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))  '-- Nomor chassis

        objPMStatusCol = New PMHeaderFacade(User).Retrieve(criterias, sortColl)
        If objPMStatusCol.Count = 0 Then
            Return -2
        End If

        _sessHelper.SetSession("objPMStatus", objPMStatusCol)
        'ViewState("CurrentSortColumn") = "Status"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        btnDownload.Enabled = True
        Return 0


    End Function
    Private Function validateCriteria(ByRef criterias As CriteriaComposite) As Boolean

        If ICDari.Value.ToString <> "" And ICSampai.Value.ToString <> "" Then

            If ICSampai.Value.Subtract(ICDari.Value).Days < 0 Then
                MessageBox.Show("Kriteria tanggal Rilis tidak valid.")
                Return False
            End If

            If ICSampai.Value.Subtract(ICDari.Value).Days > 65 Then
                MessageBox.Show("Periode Rilis tidak boleh melebihi 65 hari")
                Return False
            End If
            Dim startDate As DateTime = New DateTime(ICDari.Value.Year, ICDari.Value.Month, ICDari.Value.Day, 0, 0, 0)
            Dim endDate As DateTime = New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(PMHeader), "ReleaseDate", MatchType.GreaterOrEqual, startDate))
            criterias.opAnd(New Criteria(GetType(PMHeader), "ReleaseDate", MatchType.LesserOrEqual, endDate))
        End If

        If ddlStatus.SelectedItem.Value.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(PMHeader), "PMStatus", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If txtChassisNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ChassisNumber", MatchType.Partial, txtChassisNo.Text.Trim()))
        End If



        If ddlPMKind.SelectedItem.Value.ToString().Trim() <> "" AndAlso ddlPMKind.SelectedItem.Value.ToString() <> "0" Then

            Dim Dtx As ListItemCollection = ddlPMKind.Items

            Dim MinKM As Integer = 0
            Dim MaxKM As Integer = Integer.MaxValue
            Dim Idx As Integer = 0

            For Idx = 0 To Dtx.Count - 1

                If Dtx.Item(Idx).Value.ToString() = ddlPMKind.SelectedItem.Value.ToString().Trim() Then
                    Exit For
                End If

            Next

            Select Case Idx   ' Must be a primitive data type
                Case 1
                    criterias.opAnd(New Criteria(GetType(PMHeader), "StandKM", MatchType.LesserOrEqual, Dtx.Item(Idx).Value.ToString()))
                Case Dtx.Count - 1

                    criterias.opAnd(New Criteria(GetType(PMHeader), "StandKM", MatchType.Greater, Dtx.Item(Idx - 1).Value.ToString()))

                Case Else
                    criterias.opAnd(New Criteria(GetType(PMHeader), "StandKM", MatchType.Greater, Dtx.Item(Idx - 1).Value.ToString()))
                    criterias.opAnd(New Criteria(GetType(PMHeader), "StandKM", MatchType.LesserOrEqual, Dtx.Item(Idx).Value.ToString()))
            End Select



        End If



        Return True
    End Function
    Private Sub bindDdlStatus()
        ddlStatus.Items.Clear()
        Dim al2 As ArrayList = New EnumPMStatus().RetrieveFSStatus
        ddlStatus.DataSource = al2
        ddlStatus.DataTextField = "Desc"
        ddlStatus.DataValueField = "Code"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Pilih Semua", ""))

        ddlStatus.Items.Remove(ddlStatus.Items.FindByValue(EnumPMStatus.PMStatus.Baru))

        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataBind()
        'ddlCategory.Items.Insert(0, "Pilih")


        Dim criteriasPM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCollPM As SortCollection = New SortCollection
        sortCollPM.Add(New Sort(GetType(PMKind), "KM", Sort.SortDirection.ASC))  '-- Sort by Category code

        Dim DtPmKind As DataTable = New DataTable()
         
        DtPmKind.Columns.Add("KM", GetType(Integer))
        DtPmKind.Columns.Add("Text", GetType(String))

        DtPmKind.Rows.Add(0, "Pilih")

        Dim ObjPmKnd As ArrayList = New PMKindFacade(User).Retrieve(criteriasPM, sortCollPM)

        For Each ObjPmKind As PMKind In ObjPmKnd
            DtPmKind.Rows.Add(ObjPmKind.KM, ObjPmKind.KindCode + " - " + ObjPmKind.KindDescription)
        Next


        ddlPMKind.Items.Clear()
        ddlPMKind.DataSource = DtPmKind 'New PMKindFacade(User).Retrieve(criterias, sortColl)
        ddlPMKind.DataValueField = "KM"
        ddlPMKind.DataTextField = "Text"
        ddlPMKind.DataBind()
        ' ddlPMKind.Items.Insert(0, "Pilih")


    End Sub
    Private Sub BindItemdgChassisNumber(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            ItemTypeDataBound(e)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            FooterTypeDataBound(e)
        End If

    End Sub
    Private Sub ItemTypeDataBound(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim RowValue As PMHeader = CType(e.Item.DataItem, PMHeader)

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblTglPM As Label = CType(e.Item.FindControl("lblTglPM"), Label)
        Dim lblTglRilis As Label = CType(e.Item.FindControl("lblTglRilis"), Label)
        Dim lblPopUp As Label = CType(e.Item.FindControl("lblPopUpDetail"), Label)
        Dim lblVisitType As Label = CType(e.Item.FindControl("lblVisitType"), Label)

        lblNo.Text = (e.Item.ItemIndex + 1) + (dgPMStatus.CurrentPageIndex * dgPMStatus.PageSize)
        lblVisitType.Text = String.Empty
        If RowValue.VisitType.ToUpper() = "WI" Then
            lblVisitType.Text = "Walk In"
        ElseIf RowValue.VisitType.ToUpper() = "BO" Then
            lblVisitType.Text = "Booking"
        End If

        If RowValue.ServiceDate <= "01/01/1900" Then
            lblTglPM.Text = ""
        Else
            lblTglPM.Text = RowValue.ServiceDate.ToString("dd/MM/yyyy")
        End If

        If RowValue.ReleaseDate <= "01/01/1900" Then
            lblTglRilis.Text = ""
        Else
            lblTglRilis.Text = RowValue.ReleaseDate.ToString("dd/MM/yyyy")
        End If

        If RowValue.PMStatus <> "" Then
            If RowValue.PMStatus = EnumPMStatus.PMStatus.Baru Then
                lblStatus.Text = "Baru"
            ElseIf RowValue.PMStatus = EnumPMStatus.PMStatus.Proses Then
                lblStatus.Text = "Proses"
            ElseIf RowValue.PMStatus = EnumPMStatus.PMStatus.Selesai Then
                lblStatus.Text = "Selesai"
            End If
        Else
            lblStatus.Text = ""
        End If

        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmPopUpPMDetail.aspx?id=" & RowValue.ID & "&index=-1", "", 310, 500, "ShowPopUp")

    End Sub
    Private Sub FooterTypeDataBound(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim objPMHeaderCol As ArrayList = CType(_sessHelper.GetSession("objPMStatus"), ArrayList)
        Dim currObj As PMHeader
        Dim cmpObj As PMHeader
        Dim DealerCount As Integer = 0

        Dim lblTotalDealer As Label = CType(e.Item.FindControl("lblTotalDealer"), Label)
        Dim lblTotalUnit As Label = CType(e.Item.FindControl("lblTotalUnit"), Label)

        SortListControl(objPMHeaderCol, "Dealer.DealerCode", Sort.SortDirection.ASC)

        If objPMHeaderCol.Count > 0 Then
            For i As Integer = 0 To objPMHeaderCol.Count - 1
                If i = 0 Then
                    cmpObj = objPMHeaderCol(i)
                    DealerCount = DealerCount + 1
                Else
                    currObj = objPMHeaderCol(i)
                    If currObj.Dealer.DealerCode <> cmpObj.Dealer.DealerCode Then
                        cmpObj = objPMHeaderCol(i)
                        DealerCount = DealerCount + 1
                    End If
                End If

            Next
        End If

        lblTotalDealer.Text = DealerCount.ToString
        lblTotalUnit.Text = objPMHeaderCol.Count.ToString

    End Sub
    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub


#End Region

#Region " control event handler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        objDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        If Not IsPostBack Then
            InitiatePage()
            bindDdlStatus()

            ''LOC 2014-09-04
            '' By ali
            '' Desc : Categori Code
            Dim isKTB As Boolean = IIf(CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

            lblCategory.Visible = isKTB
            lblCategory2.Visible = isKTB
            ddlCategory.Visible = isKTB


            For IC As Integer = 0 To dgPMStatus.Columns.Count - 1
                If dgPMStatus.Columns(IC).HeaderText.ToLower() = "kategori" Then
                    dgPMStatus.Columns(IC).Visible = isKTB
                End If

            Next
            ''END OF LOC 2014-09-04
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
        'btnDownload.Enabled = bCheckPriv
    End Sub
    Private Sub InitiatePage()
        viewstate("CurrentSortColumn") = "ChassisMaster.ChassisNumber"
        viewstate("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If ICSampai.Value = New DateTime Or ICDari.Value = New DateTime Then
            MessageBox.Show("Tanggal Harus Diisi")
            Exit Sub
        End If
        Dim nResult As Integer = RetriveDataAndSaveToCache()
        If nResult > -1 Then
            dgPMStatus.CurrentPageIndex = 0
            DataBindGrid()
            btnDownload.Enabled = bCheckPriv
        Else
            If nResult = -2 Then
                dgPMStatus.DataSource = Nothing
                dgPMStatus.DataBind()
                MessageBox.Show(SR.DataNotFound("Periodical Maintenance"))
            End If
            If nResult = -3 Then
                dgPMStatus.DataSource = Nothing
                dgPMStatus.DataBind()
                MessageBox.Show("Kode dealer tidak valid.")
            End If

            btnDownload.Enabled = False
        End If
    End Sub
    Private Sub dgPMStatus_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPMStatus.ItemDataBound
        BindItemdgChassisNumber(e)
    End Sub
    Private Sub dgPMStatus_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPMStatus.PageIndexChanged
        dgPMStatus.CurrentPageIndex = e.NewPageIndex
        DataBindGrid()
    End Sub
    Private Sub dgPMStatus_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPMStatus.SortCommand
        If CType(viewstate("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("CurrentSortColumn") = e.SortExpression
            viewstate("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dgPMStatus.SelectedIndex = -1
        RetriveDataAndSaveToCache()
        dgPMStatus.CurrentPageIndex = 0
        DataBindGrid()

    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        download()
    End Sub



#End Region

End Class
