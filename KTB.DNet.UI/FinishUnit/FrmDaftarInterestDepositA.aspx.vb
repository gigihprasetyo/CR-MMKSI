Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmDaftarInterestDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblPeriodeDescription As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgDaftarDepositAInterestH As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"

    Private arlDepositAInterestH As ArrayList = New ArrayList
    Private arlDepositAInterestHFilter As ArrayList = New ArrayList

    Dim TotIA As Long = 0
    Dim TotTA As Long = 0
    Dim TotNA As Long = 0

    Dim sHelper As New SessionHelper

#End Region

#Region "Custom Method"

    Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As DepositAInterestH In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Sub BindDataGridDepositAInterestH(ByVal IndexPage As Integer)
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtKodeDealer.Text.Trim() = String.Empty Then
                MessageBox.Show("Tentukan Dealer terlebih dahulu")
                Exit Sub
            End If
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        'CR Deposit A
        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If

        'Tambah criteria pencarian yang di inginkan dari UI
        If ddlPeriode.SelectedValue.ToString <> "0" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Periode", MatchType.Exact, ddlPeriode.SelectedValue.ToString))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "Year", MatchType.Exact, ddlYear.SelectedValue.ToString))

        If CInt(ddlStatus.SelectedValue) > 0 Then
            Dim strSql As String = ""

            strSql = EnumDepositA.RetrieveInterest(CInt(ddlYear.SelectedValue), CType(ddlStatus.SelectedValue, EnumDepositA.StatusPencairanInterest))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestH), "ID", MatchType.InSet, "(" & strSql & ")"))

        End If
        
        Dim totalRow As Integer = 0
        arlDepositAInterestH = New FinishUnit.DepositAInterestHFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgDaftarDepositAInterestH.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))


        If (arlDepositAInterestH.Count > 0) Then
            dgDaftarDepositAInterestH.Visible = True
            dgDaftarDepositAInterestH.DataSource = arlDepositAInterestH 'arlDepositAInterestHFilter
            dgDaftarDepositAInterestH.VirtualItemCount = totalRow
            dgDaftarDepositAInterestH.DataBind()
        Else
            dgDaftarDepositAInterestH.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub


    Sub BindDataGridDepositAInterestD(ByVal DealerCode As String, ByVal Year As Integer, ByVal dgDepositAInterestD As DataGrid)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestD), "DealerCode", MatchType.Exact, DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestD), "Year", MatchType.Exact, Year))

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAInterestD), "ID", Sort.SortDirection.ASC))

        Dim arlDepositAInterestD As ArrayList = New FinishUnit.DepositAInterestDFacade(User).Retrieve(criterias, sortColl)

        dgDepositAInterestD.DataSource = arlDepositAInterestD
        dgDepositAInterestD.DataBind()
    End Sub

    Sub BindDataGridDepositAInterestD(ByVal HeaderID As Integer, ByVal dgDepositAInterestD As DataGrid)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestD), "DepositAInterestD.HeaderID", MatchType.Exact, HeaderID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestD), "DepositAInterestH.ID", MatchType.Exact, HeaderID))

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAInterestD), "ID", Sort.SortDirection.ASC))

        Dim arlDepositAInterestD As ArrayList = New FinishUnit.DepositAInterestDFacade(User).Retrieve(criterias, sortColl)
        'Dim arlDepositAInterestD As ArrayList = New FinishUnit.DepositAInterestDFacade(User).Retrieve(criterias)

        dgDepositAInterestD.DataSource = arlDepositAInterestD
        dgDepositAInterestD.DataBind()
    End Sub

    Private Sub BindYear()
        Dim curYear As Integer = Date.Now.Year
        Dim startYear As Integer = curYear - 5
        Dim EndYear As Integer = curYear + 5
        Dim intYear As Integer = 0
        'Dim yearMax As Integer = Year(Date.Now) + 5
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))

        ddlYear.Items.Clear()
        For intYear = startYear To EndYear
            ddlYear.Items.Add(intYear.ToString)
        Next
        'If ddlYear.Items.Contains(New ListItem(Date.Now.Year.ToString)) Then
        'ddldemotime.Items.FindByText("texthere").Selected = true;
        'ddldemotime.Items.FindByValue("Valuehere").Selected = true;

        ddlYear.Items.FindByValue(Date.Now.Year.ToString).Selected = True
        'Else
        'ddlYear.SelectedIndex = -1
        'End If
    End Sub

    Private Sub BindPeriode()
        ddlPeriode.Items.Clear()
        ddlPeriode.Items.Insert(0, New ListItem("Please Select", "0"))
        ddlPeriode.Items.Insert(1, New ListItem("Jan - Mar", "1"))
        ddlPeriode.Items.Insert(2, New ListItem("Apr - Jun", "2"))
        ddlPeriode.Items.Insert(3, New ListItem("Jul - Sep", "3"))
        ddlPeriode.Items.Insert(4, New ListItem("Okt - Des", "4"))

    End Sub

    Private Sub BindStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Insert(0, New ListItem("Please Select", CInt(EnumDepositA.StatusPencairanInterest.All).ToString()))
        ddlStatus.Items.Insert(1, New ListItem("Belum Cair", CInt(EnumDepositA.StatusPencairanInterest.BelumCair).ToString()))
        ddlStatus.Items.Insert(2, New ListItem("Proses", CInt(EnumDepositA.StatusPencairanInterest.Proses).ToString()))

        ddlStatus.Items.Insert(3, New ListItem("Proses Cair", CInt(EnumDepositA.StatusPencairanInterest.ProsesCair).ToString()))
        ddlStatus.Items.Insert(4, New ListItem("Sudah Cair", CInt(EnumDepositA.StatusPencairanInterest.SudahCair).ToString()))

        'ddlStatus.Items.Insert(0, New ListItem("Please Select", "0"))
        'ddlStatus.Items.Insert(1, New ListItem("Belum Cair", "1"))
        'ddlStatus.Items.Insert(2, New ListItem("Proses", "2"))

        'ddlStatus.Items.Insert(3, New ListItem("Proses Cair", "3"))
        'ddlStatus.Items.Insert(4, New ListItem("Sudah Cair", "4"))

    End Sub

    Private Sub Initialize()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")

        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            'txtKodeDealer.ReadOnly = False
            txtKodeDealer.Attributes.Remove("readonly")
        Else
            ' txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If


        'TODO:   'Load Periode
        BindPeriode()

        'Load Tahun
        BindYear()

        'Get Status
        BindStatus()
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        
        'mod by Ery

        'Dim val As Object = Request.Form("__EVENTTARGET")
        'If (val = Nothing) Then
        '    val = String.Empty
        'End If
        'Dim clientID As String = val.ToString()
        'If clientID = "lblDealer" Then
        '    Dim sortExpression As String = Request.Form("__EVENTARGUMENT")
        '    If CType(ViewState("CurrentSortColumn"), String) = sortExpression Then
        '        Select Case CType(ViewState("CurrentSortDirection"), Sort.SortDirection)
        '            Case Sort.SortDirection.ASC
        '                ViewState("CurrentSortDirection") = Sort.SortDirection.DESC
        '            Case Sort.SortDirection.DESC
        '                ViewState("CurrentSortDirection") = Sort.SortDirection.ASC
        '        End Select
        '    Else
        '        ViewState("CurrentSortColumn") = sortExpression
        '        ViewState("CurrentSortDirection") = Sort.SortDirection.ASC
        '    End If
        '    BindDataGridDepositAInterestH(0)
        'End If

        If Not IsPostBack Then
            Initialize()
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGridDepositAInterestH(0)
    End Sub

    Private Sub dgDaftarDepositAInterestH_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarDepositAInterestH.PageIndexChanged
        dgDaftarDepositAInterestH.CurrentPageIndex = e.NewPageIndex
        BindDataGridDepositAInterestH(dgDaftarDepositAInterestH.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarDepositAInterestH_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarDepositAInterestH.ItemDataBound
        Dim container As Control = e.Item

        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objDepositAInterestH As DepositAInterestH = CType(e.Item.DataItem, DepositAInterestH)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarDepositAInterestH.CurrentPageIndex * dgDaftarDepositAInterestH.PageSize)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "DepositAInterestH.ID", MatchType.Exact, objDepositAInterestH.ID))

            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAPencairanH), "NoReg", Sort.SortDirection.DESC))

            Dim arlDepositAPencarianH As ArrayList = New FinishUnit.DepositAPencairanHFacade(User).Retrieve(criterias, sortColl)

            Dim objDepositPencairanH As DepositAPencairanH
            If arlDepositAPencarianH.Count > 0 Then
                objDepositPencairanH = arlDepositAPencarianH(0)
            End If
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            If Not IsNothing(objDepositPencairanH) Then
                If Not IsNothing(objDepositPencairanH.Status) Then
                    If objDepositPencairanH.Status = EnumDepositA.StatusPencairanDealer.Selesai Then
                        Dim critKuitansi As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critKuitansi.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "NoReg", MatchType.Exact, objDepositPencairanH.NoReg))
                        Dim arlKuitansi As ArrayList = New FinishUnit.DepositAKuitansiPencairanFacade(User).Retrieve(critKuitansi)
                        Dim objKuitansi As DepositAKuitansiPencairan
                        If arlKuitansi.Count > 0 Then
                            objKuitansi = arlKuitansi(0)
                            If objKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Selesai Then
                                lblStatus.Text = "Proses Cair"
                            ElseIf objKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Cair Then
                                lblStatus.Text = "Sudah Cair"
                            Else
                                lblStatus.Text = "Proses"
                            End If
                        Else
                            lblStatus.Text = "Proses"
                        End If

                    ElseIf objDepositPencairanH.Status = EnumDepositA.StatusPencairanDealer.Blok Or objDepositPencairanH.Status = EnumDepositA.StatusPencairanDealer.Tolak Then
                        lblStatus.Text = "Belum Cair"
                    Else
                        lblStatus.Text = "Proses"
                    End If
                Else
                    lblStatus.Text = "Belum Cair"
                End If
            Else
                lblStatus.Text = "Belum Cair"
            End If


            Dim btnExpandButton As HtmlImage = CType(container.FindControl("image_"), HtmlImage)
            If (Not (btnExpandButton) Is Nothing) Then
                'btnExpandButton.Attributes.Add("OnClick", "Toggle('dgDepositAInterestH__ctl' & (e.Item.ItemIndex + 2) & '_divDepositAInterestD', 'dgDepositAInterestH__ctl' & (e.Item.ItemIndex + 2) & '_image_');")
                Dim temp As String = "Toggle('dgDaftarDepositAInterestH__ctl"
                temp = temp & e.Item.ItemIndex + 3 & "_divDepositAInterestD'"
                temp = temp & ", '" & "dgDaftarDepositAInterestH__ctl"
                temp = temp & e.Item.ItemIndex + 3
                temp = temp & "_image_');"
                btnExpandButton.Attributes.Add("OnClick", temp)
            End If

            Dim dgDepositAInterestD As DataGrid = CType(container.FindControl("dgDepositAInterestD"), DataGrid)
            Dim DepositAInterestHID As String = String.Empty
            If (Not (dgDepositAInterestD) Is Nothing) Then
                Dim lblDepositAInterestHID As Label = CType(e.Item.FindControl("lblDepositAInterestHID"), Label)
                DepositAInterestHID = lblDepositAInterestHID.Text.Trim
                Dim btnDepositAInterestHID As Button = CType(container.FindControl("btnDepositAInterestHID"), Button)
                If (Not (btnDepositAInterestHID) Is Nothing) Then
                    btnDepositAInterestHID.CommandArgument = DepositAInterestHID
                    Me.ViewState("DepositAInterestHID") = DepositAInterestHID
                End If
                'If ddlPeriode.SelectedValue.ToString = "0" Then
                '    BindDataGridDepositAInterestD(objDepositAInterestH.Dealer.DealerCode, objDepositAInterestH.Year, dgDepositAInterestD)
                'Else
                BindDataGridDepositAInterestD(CInt(DepositAInterestHID), dgDepositAInterestD)
                'End If

            End If
        End If
    End Sub

    Private Sub dgDaftarDepositAInterestH_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDaftarDepositAInterestH.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgDaftarDepositAInterestH.SelectedIndex = -1
        dgDaftarDepositAInterestH.CurrentPageIndex = 0
        BindDataGridDepositAInterestH(dgDaftarDepositAInterestH.CurrentPageIndex)

    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(context.User, SR.DepositAView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - UMUM - Daftar Interest Deposit A")
        'End If

        If Not SecurityProvider.Authorize(Context.User, SR.DepositA_daftar_interest_depositA_lihat_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Interest Deposit A")
            Me.btnSearch.Visible = False
        End If
    End Sub
#End Region

    '<asp:Label id="Label3" runat="server" Text='<%# MonthName(DataBinder.Eval(Container, "DataItem.Month")) %>'></asp:Label>
End Class
