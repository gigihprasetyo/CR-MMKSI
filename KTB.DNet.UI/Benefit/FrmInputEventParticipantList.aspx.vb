Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Parser

Imports Excel

Imports System.IO
Imports System.Data.OleDb
Imports System.Data


Public Class FrmInputEventParticipantList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtbenefitReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents fileUploadParticipant As System.Web.UI.WebControls.FileUpload
    Protected WithEvents txtNoRegEvent As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEventName As System.Web.UI.WebControls.TextBox
    Protected WithEvents icTanggalEvent As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTanggalEventEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    '

    Protected WithEvents lblPopUpBenefit As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpEvent As System.Web.UI.WebControls.Label
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button

    Protected WithEvents lblDelerSession As System.Web.UI.WebControls.Label

    Protected WithEvents LinkDownload As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtRemarks As System.Web.UI.WebControls.TextBox
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper
    Private objDomain As BenefitEventHeader = New BenefitEventHeader
    Private objDomainFacade As BenefitEventHeaderFacade = New BenefitEventHeaderFacade(User)

    Private objBenefitMasterHeaderFacade As BenefitMasterHeaderFacade = New BenefitMasterHeaderFacade(User)

    Private objBenefitEventDetail As BenefitEventDetail = New BenefitEventDetail
 

#Region "Private Property"
    Private ObjDealer As Dealer
    Private inputeventparticipant_privillage As Boolean
    Private ViewEventParticipant_privillage As Boolean

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=BENEFIT - Input Event Participant")
        'End If

        inputeventparticipant_privillage = False
        ViewEventParticipant_privillage = False
        inputeventparticipant_privillage = SecurityProvider.Authorize(Context.User, SR.inputeventparticipant_privillage)
        If Not IsNothing(Request.QueryString("Mode")) AndAlso Request.QueryString("Mode").ToString() = "View" Then
            ViewEventParticipant_privillage = SecurityProvider.Authorize(Context.User, SR.Vieweventparticipant_privillage)
            If Not ViewEventParticipant_privillage AndAlso Not inputeventparticipant_privillage Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Input Peserta Event")
            End If
        Else

            If Not inputeventparticipant_privillage Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Input Peserta Event")
            End If
        End If
       
    End Sub

    'Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege)
#End Region

    Enum Status
        Baru = 0
        Validasi = 1
    End Enum

    Enum StatusKtb
        Tolak = 2
        Selesai = 3
    End Enum

    Private noabjad As String = ""

    Private Sub EnumToListBox(EnumType As Type, TheListBox As ListControl)
        Dim Values As Array = System.Enum.GetValues(EnumType)
        For Each Value As Integer In Values
            Dim Display As String = [Enum].GetName(EnumType, Value)
            Dim Item As ListItem = New ListItem(Display, Value.ToString())
            TheListBox.Items.Add(Item)
        Next
    End Sub

    Private Sub RetrieveDealer()
        Dim objSessionHelper As New SessionHelper
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then

            If Not objDealer.DealerGroup Is Nothing Then 'jika dealer
                'jika dia login sebagai dealer
                lblDelerSession.Visible = True
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeDealer.Attributes("style") = "display:none"
                lblDelerSession.Text = objDealer.DealerCode & " / " & objDealer.DealerName
                txtKodeDealer.Text = objDealer.DealerCode


                'EnumToListBox(GetType(Status), ddlStatus)
            Else
                lblDelerSession.Visible = False
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeDealer.Attributes("style") = "display:"

                'EnumToListBox(GetType(StatusKtb), ddlStatus)
            End If

        Else

        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        ObjDealer = sessHelper.GetSession("DEALER")
        If Not IsPostBack Then

            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpBenefit.Attributes("onclick") = "ShowPPBenefitSelection();"
            lblPopUpEvent.Attributes("onclick") = "ShowPopUpEvent();"
            InitializeForm()
            BindDdlStatus()
            RetrieveDealer()
        End If
        txtbenefitReg.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub BindDdlStatus()
        Dim _arrStatusDealer As ArrayList = New BenefitEventEnumStatus().RetrieveStatus()
        For Each item As BenefitEventStatus In _arrStatusDealer
            ddlStatus.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
        Next
    End Sub

 
    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs)

        Dim lblNamaGrid As Label = CType(e.Item.FindControl("lblNamaGrid"), Label)
        Dim lblKtpGrid As Label = CType(e.Item.FindControl("lblKtpGrid"), Label)
        Dim lblAlamatGrid As Label = CType(e.Item.FindControl("lblAlamatGrid"), Label)


        'Dim objDomain2 As BenefitMasterDetail = CType(e.Item.DataItem, BenefitMasterDetail)

        'Delete item yang index nya itu sesuai dengan index item yg di filter
        Dim list As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        Dim _DetailDelete As New BenefitEventDetail
        For Each item As BenefitEventDetail In list
            If lblNamaGrid.Text.Replace(" ", "") = item.BenefitParticipant.Nama.ToString.Replace(" ", "") _
                And lblKtpGrid.Text.Replace(" ", "") = item.BenefitParticipant.KTP.ToString.Replace(" ", "") _
                And lblAlamatGrid.Text.Replace(" ", "") = item.BenefitParticipant.Alamat.ToString.Replace(" ", "") Then
                _DetailDelete = item
            End If
        Next
        list.Remove(_DetailDelete)
        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
    End Sub



    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "Add"
                AddCommand(e)
            Case "Edit"
                dgTable_EditCommand(e)
                'Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=Edit;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
            Case "Update"
                UpdateCommand(e)
            Case "Cancel"
                dgSPDetail_CancelCommand(e)
            Case "Delete"
                DeleteCommand(e)
        End Select
    End Sub

    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            dgTable.EditItemIndex = -1
            GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "ViewSave" Then
            dgTable.EditItemIndex = -1
            GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
        Else

            Dim txtNamaGridEdit As TextBox = e.Item.FindControl("txtNamaGridEdit")
            Dim txtKtpGridEdit As TextBox = e.Item.FindControl("txtKtpGridEdit")
            Dim txtAlamatGridEdit As TextBox = e.Item.FindControl("txtAlamatGridEdit")
            Dim txtKeteranganEdit As TextBox = e.Item.FindControl("txtKeteranganEdit")

            Dim cbAllGridGridEdit As CheckBox = e.Item.FindControl("cbAllGridGridEdit")


            If (txtKeteranganEdit.Text = "") Or (txtNamaGridEdit.Text = "") Or (txtKtpGridEdit.Text = "") Or (txtAlamatGridEdit.Text = "") Then
                MessageBox.Show("Isi Lengkap Detail ")
                Return
            End If



            If txtKeteranganEdit.Text.Replace(" ", "") = "" Or txtNamaGridEdit.Text.Replace(" ", "") = "" Or txtKtpGridEdit.Text = "" Or txtAlamatGridEdit.Text.Replace(" ", "") = "" Then
                MessageBox.Show("Isi detail dengan lengkap")
                Return
            End If

            Dim listAll As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
            objBenefitEventDetail = New BenefitEventDetail

            objBenefitEventDetail = CType(listAll(e.Item.ItemIndex), BenefitEventDetail)
            objBenefitEventDetail.BenefitParticipant.Alamat = txtAlamatGridEdit.Text
            objBenefitEventDetail.BenefitParticipant.KTP = txtKtpGridEdit.Text
            objBenefitEventDetail.BenefitParticipant.Nama = txtNamaGridEdit.Text
            objBenefitEventDetail.BenefitParticipant.Remarks = txtKeteranganEdit.Text.Trim

            If cbAllGridGridEdit.Checked = True Then
                objBenefitEventDetail.Status = 1
            Else
                objBenefitEventDetail.Status = 0
            End If


            If Not listAll Is Nothing Then
                listAll.RemoveAt(e.Item.ItemIndex)
                listAll.Insert(e.Item.ItemIndex, objBenefitEventDetail)
            End If

            sessHelper.SetSession("DetailSession", listAll)

            dgTable.EditItemIndex = -1
            GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
            dgTable.ShowFooter = True

        End If

    End Sub

    Private Sub dgTable_EditCommand(ByVal e As DataGridCommandEventArgs)

        dgTable.EditItemIndex = CInt(e.Item.ItemIndex)
        'BindDetail(CInt(sessHelper.GetSession("IDBenefitListHeader")))
        dgTable.ShowFooter = False
        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
    End Sub

    Private Sub dgSPDetail_CancelCommand(ByVal E As DataGridCommandEventArgs)
        dgTable.EditItemIndex = -1
        ' BindDetail(CInt(sessHelper.GetSession("IDBenefitListHeader")))
        dgTable.ShowFooter = True
        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
    End Sub

    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound


        If Convert.ToString(Request.QueryString("Mode")) = "View" Then
            GenerateToGrid(e)
        ElseIf Convert.ToString(Request.QueryString("Mode")) = "ViewSave" Then
            GenerateToGrid(e)
        Else
            GenerateToGrid(e)

        End If


    End Sub

    Private Sub dgTable_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTable.PageIndexChanged
        'dgTable.CurrentPageIndex = e.NewPageIndex
        'BindDataGrid(dgTable.CurrentPageIndex)
    End Sub

    Private Sub dgTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTable.SortCommand
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

        dgTable.SelectedIndex = -1
        dgTable.CurrentPageIndex = 0
        'BindDataGrid(dgTable.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        'RemoveALLSession()
        ' Response.Redirect("FrmEventParticipantProcessList.aspx")

        If Convert.ToString(Request.QueryString("Mode")) = "View" Or
               Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
               Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
            Response.Redirect("FrmEventParticipantProcessList.aspx")
        Else
            Response.Redirect("FrmInputEventParticipantList.aspx")
        End If

    End Sub

    Private Sub RemoveALLSession()
        sessHelper.RemoveSession("DetailParticipantExcelSession")
        sessHelper.RemoveSession("DetailParticipantSession")
        sessHelper.RemoveSession("IDBenefitListHeader")
        sessHelper.RemoveSession("DetailSession")
        sessHelper.RemoveSession("addDetailSession")
    End Sub

   

    Private Sub GenerateToGrid(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitEventDetail = CType(e.Item.DataItem, BenefitEventDetail)
            ' If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then
                    Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                    Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
                    If Convert.ToString(sessHelper.GetSession("Status")) = "View" _
                      Or Convert.ToString(sessHelper.GetSession("Status")) = "Delete" _
                      Or Convert.ToString(sessHelper.GetSession("Status")) = "ViewSave" Then

                        lnkbtnEdit.Visible = False

                        lnkbtnDelete.Visible = False
                    End If

                    Dim cbAllGrid As CheckBox = CType(e.Item.FindControl("cbAllGrid"), CheckBox)
                    cbAllGrid.Enabled = False
                    If objDomain2.Status = 1 Then
                        cbAllGrid.Checked = True
                    Else
                        cbAllGrid.Checked = False
                    End If


                    Dim lblNoGrid As Label = CType(e.Item.FindControl("lblNoGrid"), Label)
                    lblNoGrid.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString

                    Dim lblNamaGrid As Label = CType(e.Item.FindControl("lblNamaGrid"), Label)
                    lblNamaGrid.Text = objDomain2.BenefitParticipant.Nama


                    Dim lblKtpGrid As Label = CType(e.Item.FindControl("lblKtpGrid"), Label)
                    lblKtpGrid.Text = objDomain2.BenefitParticipant.KTP


                    Dim lblAlamatGrid As Label = CType(e.Item.FindControl("lblAlamatGrid"), Label)
                    lblAlamatGrid.Text = objDomain2.BenefitParticipant.Alamat



                    Dim lblKeterangan As Label = CType(e.Item.FindControl("lblKeterangan"), Label)
                    lblKeterangan.Text = objDomain2.BenefitParticipant.Remarks


                    
                    '  If Not lblDelerSession.Visible = True Then 'jika ktb
                    'lnkbtnEdit.Visible = False
                    '  lnkbtnDelete.Visible = lnkbtnEdit.Visible
                    ' End If

                Else

                    Dim cbAllGridGridEdit As CheckBox = CType(e.Item.FindControl("cbAllGridGridEdit"), CheckBox)
                    cbAllGridGridEdit.Enabled = True
                    If objDomain2.Status = 1 Then
                        cbAllGridGridEdit.Checked = True
                    Else
                        cbAllGridGridEdit.Checked = False
                    End If

                    Dim txtNamaGridEdit As TextBox = CType(e.Item.FindControl("txtNamaGridEdit"), TextBox)
                    txtNamaGridEdit.Text = objDomain2.BenefitParticipant.Nama


                    Dim txtKtpGridEdit As TextBox = CType(e.Item.FindControl("txtKtpGridEdit"), TextBox)
                    txtKtpGridEdit.Text = objDomain2.BenefitParticipant.KTP


                    Dim txtAlamatGridEdit As TextBox = CType(e.Item.FindControl("txtAlamatGridEdit"), TextBox)
                    txtAlamatGridEdit.Text = objDomain2.BenefitParticipant.Alamat

                    Dim txtKeteranganEdit As TextBox = CType(e.Item.FindControl("txtKeteranganEdit"), TextBox)
                    txtKeteranganEdit.Text = objDomain2.BenefitParticipant.Remarks

                    Dim lblEditPopUpSPK As Label = CType(e.Item.FindControl("lblEditPopUpSPK"), Label)
                    lblEditPopUpSPK.Attributes("onclick") = "ShowPopUpSPK();"

                End If





            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblFooterPopUpSPK As Label = CType(e.Item.FindControl("lblFooterPopUpSPK"), Label)
            lblFooterPopUpSPK.Attributes("onclick") = "ShowPopUpSPK();"
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim lblEditPopUpSPK As Label = CType(e.Item.FindControl("lblEditPopUpSPK"), Label)
            lblEditPopUpSPK.Attributes("onclick") = "ShowPopUpSPK();"
        End If
    End Sub

    

    Private Sub GetValueFromDataBase(ByVal id As Integer)


        Dim Obj As BenefitEventHeader = objDomainFacade.Retrieve(id)

        If Not Obj Is Nothing Then
            txtbenefitReg.Text = Obj.BenefitMasterHeader.BenefitRegNo

            txtNoRegEvent.Text = Obj.EventRegNo
            txtEventName.Text = Obj.EventName
            icTanggalEvent.Value = Obj.EventDate
            ddlStatus.SelectedValue = Obj.Status.ToString

            Dim alBenefitMasterDealers As ArrayList = Obj.BenefitMasterHeader.BenefitMasterDealers
            Dim idDealer As String = ""
            For Each el As BenefitMasterDealer In alBenefitMasterDealers
                idDealer += el.Dealer.DealerCode + "; "
            Next
            txtKodeDealer.Text = idDealer

        End If

        Dim listParticipant As ArrayList = CType(sessHelper.GetSession("DetailParticipantSession"), ArrayList)
        Dim list As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        If list Is Nothing Then
            If Convert.ToString(Request.QueryString("Mode")) = "View" _
                Or Convert.ToString(Request.QueryString("Mode")) = "ViewSave" _
                Or Convert.ToString(Request.QueryString("Mode")) = "Edit" _
                Or Convert.ToString(Request.QueryString("Mode")) = "Delete" Then

                list = Obj.BenefitEventDetails

                listParticipant = New ArrayList
                For Each items As BenefitEventDetail In Obj.BenefitEventDetails
                    listParticipant.Add(items.BenefitParticipant)
                Next

                If list Is Nothing Then
                    list = New ArrayList
                End If
                ' ElseIf Convert.ToString(Request.QueryString("Mode")) = "Insert" Then
            Else
                list = New ArrayList
            End If
        End If

        If listParticipant Is Nothing Then
            listParticipant = New ArrayList
        End If

        Dim list1 As BenefitEventDetail = CType(sessHelper.GetSession("addDetailSession"), BenefitEventDetail)

        If Not list1 Is Nothing Then
            listParticipant.Add(list1.BenefitParticipant)
            list.Add(list1)
        End If

        Dim list2 As ArrayList = CType(sessHelper.GetSession("DetailParticipantExcelSession"), ArrayList)

        If Not list2 Is Nothing Then
            For Each Items As BenefitEventDetail In list2
                list.Add(Items)
            Next

        End If

        sessHelper.SetSession("DetailSession", list)
        sessHelper.SetSession("DetailParticipantSession", listParticipant)


        sessHelper.RemoveSession("DetailParticipantExcelSession")
        sessHelper.RemoveSession("addDetailSession")

        dgTable.DataSource = list
        dgTable.DataBind()
    End Sub


    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If

        Dim txtNamaGrid As TextBox = e.Item.FindControl("txtNamaGrid")
        Dim txtKtpGrid As TextBox = e.Item.FindControl("txtKtpGrid")
        Dim txtAlamatGrid As TextBox = e.Item.FindControl("txtAlamatGrid")
        Dim txtKeteranganFooter As TextBox = e.Item.FindControl("txtKeteranganFooter")
        Dim lblFooterPopUpSPK As Label = CType(e.Item.FindControl("lblFooterPopUpSPK"), Label)

        Dim cbAllGridGrid As CheckBox = e.Item.FindControl("cbAllGridGrid")

        lblFooterPopUpSPK.Attributes("onclick") = "ShowPopUpSPK();"

        If (txtKeteranganFooter.Text = "") Or (txtNamaGrid.Text = "") Or (txtKtpGrid.Text = "") Or (txtAlamatGrid.Text = "") Then
            MessageBox.Show("Isi Lengkap Detail ")
            Return
        Else
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, txtKeteranganFooter.Text.Trim))
            Dim arrSPKHeader As ArrayList = New SPKHeaderFacade(User).Retrieve(crit)
            If Not arrSPKHeader.Count > 0 Then
                MessageBox.Show("Nomor SPK DNet tidak terdaftar ")
                Return
            End If
        End If

        Dim listAll As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        If Not listAll Is Nothing Then
            For Each item As BenefitEventDetail In listAll
                If item.BenefitParticipant.Remarks = txtKeteranganFooter.Text.Trim Then
                    MessageBox.Show("Nomor SPK DNet sudah pernah diinput")
                    Return
                End If
            Next
        End If

        objBenefitEventDetail = New BenefitEventDetail
        Dim objBenefitParticipant As BenefitParticipant = New BenefitParticipant

        objBenefitParticipant.Nama = txtNamaGrid.Text
        objBenefitParticipant.Alamat = txtAlamatGrid.Text
        objBenefitParticipant.KTP = txtKtpGrid.Text
        objBenefitParticipant.Remarks = txtKeteranganFooter.Text.Trim

        objBenefitEventDetail.BenefitParticipant = objBenefitParticipant
        If cbAllGridGrid.Checked = True Then
            objBenefitEventDetail.Status = 1
        Else
            objBenefitEventDetail.Status = 0
        End If

        sessHelper.SetSession("addDetailSession", objBenefitEventDetail)

        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

    End Sub


    Private Sub InitializeForm()

        RemoveALLSession()
        If Request.QueryString("Mode") = "View" Then
            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"
            btnSimpan.Visible = False
            btnDelete.Visible = False
            lblPopUpDealer.Visible = False
            dgTable.ShowFooter = False
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "View")
            GetValueFromDataBase(id)
        ElseIf Request.QueryString("Mode") = "ViewSave" Then
            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"
            btnSimpan.Visible = False
            btnDelete.Visible = False
            lblPopUpDealer.Visible = False
            dgTable.ShowFooter = False
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "View")
            GetValueFromDataBase(id)

        ElseIf Request.QueryString("Mode") = "Edit" Then
            Dim id As Integer = CInt(Request.QueryString("id"))
            btnSimpan.Visible = True
            lblPopUpDealer.Visible = True
            btnBatal.Text = "Batal"
            txtKodeDealer.Enabled = False
            btnDelete.Visible = False
            txtbenefitReg.Enabled = txtKodeDealer.Enabled
            txtNoRegEvent.Enabled = txtKodeDealer.Enabled
            lblPopUpBenefit.Visible = txtKodeDealer.Enabled
            lblPopUpDealer.Visible = txtKodeDealer.Enabled
            dgTable.ShowFooter = True
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "Edit")
            GetValueFromDataBase(id)
        ElseIf Request.QueryString("Mode") = "Delete" Then

            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"
            txtKodeDealer.Enabled = False
            txtbenefitReg.Enabled = txtKodeDealer.Enabled
            txtNoRegEvent.Enabled = txtKodeDealer.Enabled
            lblPopUpBenefit.Visible = txtKodeDealer.Enabled
            lblPopUpDealer.Visible = txtKodeDealer.Enabled
            btnDelete.Visible = True
            btnSimpan.Visible = False
            lblPopUpDealer.Visible = False
            dgTable.ShowFooter = False
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "Delete")
            GetValueFromDataBase(id)

        Else

            btnSimpan.Visible = True
            lblPopUpDealer.Visible = True
            btnDelete.Visible = False
            btnBatal.Text = "Batal"
            sessHelper.SetSession("status", "Insert")
            dgTable.ShowFooter = True
            Dim list As ArrayList = New ArrayList
            dgTable.DataSource = list
            dgTable.DataBind()
        End If


    End Sub



    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        If txtbenefitReg.Text.Trim = "" Then
            MessageBox.Show("Isi Benefit Reg ")
            Return
        End If
       

        If Not icTanggalEvent.Value.ToString("yyyy") Is Nothing And CInt(icTanggalEvent.Value.ToString("yyyy")) < 1990 Then
            MessageBox.Show("Isi Tanggal Event")
            Return
        End If

        'If txtNoRegEvent.Text.Replace(" ", "") = "" Then
        '    MessageBox.Show("Isi No Event")
        '    Return
        'End If
        If txtEventName.Text.Replace(" ", "") = "" Then
            MessageBox.Show("Isi Nama Event")
            Return
        End If

        Dim list As New ArrayList
        If Not sessHelper.GetSession("DetailSession") Is Nothing Then
            list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        End If

        Dim listParticipant As New ArrayList
        If Not sessHelper.GetSession("DetailParticipantSession") Is Nothing Then
            listParticipant = CType(sessHelper.GetSession("DetailParticipantSession"), ArrayList)
        End If

        'If listParticipant.Count > 0 Then
        '    For Each item As BenefitParticipant In listParticipant
        '        listParticipant.Add(item)
        '    Next
        'End If

        If list.Count < 1 Then
            MessageBox.Show("Isi Participant minimal 1")
            Return
        End If


        Dim i As Integer
        If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then

            objDomain.EventDate = icTanggalEvent.Value
            objDomain.EventName = txtEventName.Text
            ' objDomain.EventRegNo = txtNoRegEvent.Text
            objDomain.Status = CShort(ddlStatus.SelectedValue)


            Dim objBenefitMasterHeader As BenefitMasterHeader = objBenefitMasterHeaderFacade.Retrieve(txtbenefitReg.Text.Replace(" ", ""))
            objDomain.BenefitMasterHeader = objBenefitMasterHeader

            Dim objSessionHelper As New SessionHelper
            Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
            objDomain.Dealer = objDealer

            If Not sessHelper.GetSession("DetailSession") Is Nothing Then
                list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
                For Each items As BenefitEventDetail In list
                    objDomain.BenefitEventDetails.Add(items)
                Next
            End If

            objDomain.Status = 0

            'For Each items As BenefitEventDetail In list
            '    'items.

            '    'If items.BenefitMasterLeasings.Count > 0 Then
            '    '    For Each items1 As BenefitMasterLeasing In items.BenefitMasterLeasings
            '    '        ObjBenefitMasterDetail.BenefitMasterLeasings.Add(items1)
            '    '    Next
            '    'End If

            '    'If items.BenefitMasterVehicleTypes.Count > 0 Then
            '    '    For Each items2 As BenefitMasterVehicleType In items.BenefitMasterVehicleTypes
            '    '        ObjBenefitMasterDetail.BenefitMasterVehicleTypes.Add(items2)
            '    '    Next
            '    'End If

            '    objDomain.BenefitEventDetails.Add(items)

            'Next

            'Return

            'Dim n As Integer = objDomainFacade.Insert(objDomain, list)
            'Dim n As Integer = objDomainFacade.Insert(objDomain, list, listParticipant)
            Dim n As Integer = objDomainFacade.Insert(objDomain)
            If n = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                RemoveALLSession()
                'Response.Redirect("FrmEventParticipantProcessList.aspx")
                'MessageBox.Show("Simpan Berhasil")
                Response.Write("<script>alert('Data berhasil disimpan')</script>")
                ' Response.Write("<script>window.location.href='FrmEventParticipantProcessList.aspx';</script>")
                If Convert.ToString(Request.QueryString("Mode")) = "View" Or
                   Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
                   Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
                    Response.Write("<script>window.location.href='FrmEventParticipantProcessList.aspx';</script>")
                Else
                    'Response.Write("<script>window.location.href='FrmInputEventParticipantList.aspx';</script>")
                    Response.Write("<script>window.location.href='FrmInputEventParticipantList.aspx?Mode=ViewSave&id=" & n & "';</script>")
                End If
            End If


        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Edit" Then

            Dim IDBenefitListHeader As Integer = CInt(sessHelper.GetSession("IDBenefitListHeader"))
            Dim objDomainTemp As BenefitEventHeader = objDomainFacade.Retrieve(IDBenefitListHeader)
            'objDomainTemp.ID = IDBenefitListHeader
            objDomainTemp.EventName = txtEventName.Text
            objDomainTemp.EventDate = icTanggalEvent.Value
            objDomainTemp.Status = CShort(ddlStatus.SelectedValue)

            Dim objSessionHelper As New SessionHelper
            Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
            objDomainTemp.Dealer = objDealer


            If Not sessHelper.GetSession("DetailSession") Is Nothing Then
                list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
            End If

            Dim n As Integer = objDomainFacade.Update(objDomainTemp, list)

            If n = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                RemoveALLSession()
                ' Response.Redirect("FrmNewInputMasterList.aspx")
                'MessageBox.Show("Simpan Berhasil")
                'Response.Redirect("FrmEventParticipantProcessList.aspx")
                Response.Write("<script>alert('Data berhasil diubah')</script>")
                'Response.Write("<script>window.location.href='FrmEventParticipantProcessList.aspx';</script>")
                If Convert.ToString(Request.QueryString("Mode")) = "View" Or
                  Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
                  Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
                    Response.Write("<script>window.location.href='FrmEventParticipantProcessList.aspx';</script>")
                Else
                    Response.Write("<script>window.location.href='FrmInputEventParticipantList.aspx';</script>")
                End If
            End If


        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim list As New ArrayList
        Dim i As Integer

        Dim IDBenefitListHeader As Integer = CInt(sessHelper.GetSession("IDBenefitListHeader"))

        Dim objDomainTemp As BenefitEventHeader = objDomainFacade.Retrieve(IDBenefitListHeader)
        Dim n As Integer = objDomainFacade.Delete(objDomainTemp)
        'Dim n As Integer = objDomainFacade.Insert(objDomain)
        If n = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            RemoveALLSession()
            ' Response.Redirect("FrmNewInputMasterList.aspx")
            'MessageBox.Show("Hapus Berhasil")
            'Response.Redirect("FrmEventParticipantProcessList.aspx")
            Response.Write("<script>alert('Data berhasil dihapus.')</script>")
            Response.Write("<script>window.location.href='FrmEventParticipantProcessList.aspx';</script>")
        End If


    End Sub

    Private Sub LinkDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkDownload.Click


        Response.Redirect("../downloadlocal.aspx?file=Benefit\Participant.xls")

    End Sub

    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "XLS" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim retValue As Integer = 0
        If fileUploadParticipant.PostedFile.FileName.Length > 0 Then
            Dim objSessionHelper As New SessionHelper
            Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If fileUploadParticipant.PostedFile.ContentLength <> fileUploadParticipant.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim datetimenow As String = Now.ToString("yyyy_MM_dd_H_mm_ss")

                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "SalesCampaign_Benefit"
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)



                'If Not directoryInfo.Exists Then
                '    directoryInfo.Create()
                'End If


                Dim ext As String = System.IO.Path.GetExtension(fileUploadParticipant.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0
                    MessageBox.Show("Hanya Format *.XLS/Excell 2003")
                    Return
                End If

                Dim SrcFile As String = Path.GetFileName(fileUploadParticipant.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.Year.ToString() & "\" & DateTime.Now.Month.ToString() & "\" & DateTime.Now.ToString("ddMMyyyHHmmss") & txtKodeDealer.Text & SrcFile

                'Dim targetFile As String = New System.Text.StringBuilder(directory). _
                '    Append("\").Append(datetimenow + "_" + _
                '                       Path.GetFileName(fileUploadParticipant.PostedFile.FileName)).ToString

                Dim objUpload As New UploadToWebServer
                objUpload.Upload(fileUploadParticipant.PostedFile.InputStream, targetFile)


                'fileUploadParticipant.PostedFile.SaveAs(targetFile)






                Dim objReader As IExcelDataReader = Nothing

                Dim list As ArrayList = New ArrayList

                Dim checkSalah As Boolean = False
                Dim checkKosong As Boolean = False
                Dim dataFailed As Integer = 0
                Dim dataSuccess As Integer = 0

                Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)
                    Dim is2007 As Integer = 0
                    If ext.ToLower.Contains("xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                        objReader.IsFirstRowAsColumnNames = False
                        is2007 = 0
                    Else
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    End If

                    'objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    Dim i As Integer = 0


                    If (Not IsNothing(objReader)) Then

                        While objReader.Read()


                            If (i = 4 + is2007) Then
                                If objReader.GetString(0).Trim().ToLower() = "no" AndAlso objReader.GetString(5).Trim().ToLower() = "data" Then
                                    checkSalah = True
                                End If

                            End If

                            'txtNoRegEvent.Text = txtNoRegEvent.Text & " _ " & i & "->" & objReader.GetString(0)

                            If (i > 4 + is2007) Then

                                objBenefitEventDetail = New BenefitEventDetail
                                Dim objBenefitParticipant As BenefitParticipant = New BenefitParticipant

                                If objReader.GetString(2) = "" OrElse _
                                    objReader.GetString(3) = "" OrElse objReader.GetString(4) = "" Then
                                    'checkKosong = True
                                    Continue While
                                End If

                                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crit.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, objReader.GetString(1)))
                                If Not txtNoRegEvent.Text.Trim.ToString = "" Then
                                    crit.opAnd(New Criteria(GetType(SPKHeader), "CampaignName", MatchType.Exact, txtNoRegEvent.Text.Trim.ToString))
                                End If
                                If Not objDealer Is Nothing Then
                                    crit.opAnd(New Criteria(GetType(SPKHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
                                End If

                                Dim arrSPKHeader As ArrayList = New SPKHeaderFacade(User).Retrieve(crit)
                                If arrSPKHeader.Count > 0 Then
                                    Dim objSPKHeader As SPKHeader = arrSPKHeader(0)
                                    objBenefitParticipant.Nama = objSPKHeader.SPKCustomer.Name1
                                    objBenefitParticipant.Alamat = objSPKHeader.SPKCustomer.Alamat
                                    objBenefitParticipant.Remarks = objSPKHeader.SPKNumber

                                    Dim crit2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    crit2.opAnd(New Criteria(GetType(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, objSPKHeader.SPKCustomer.ID))
                                    crit2.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileHeader.ID", MatchType.Exact, 29))

                                    Dim arrSPKCustomerProfile As ArrayList = New KTB.DNet.BusinessFacade.Profile.SPKCustomerProfileFacade(User).Retrieve(crit2)

                                    If arrSPKCustomerProfile.Count > 0 Then
                                        Dim objSPKCustomerProfile As SPKCustomerProfile = arrSPKCustomerProfile(0)
                                        objBenefitParticipant.KTP = objSPKCustomerProfile.ProfileValue
                                    End If

                                    objBenefitEventDetail.BenefitParticipant = objBenefitParticipant
                                    list.Add(objBenefitEventDetail)
                                    dataSuccess += 1
                                Else
                                    dataFailed += 1
                                End If
                            End If

                            i = i + 1
                        End While


                    End If

                End Using

                If checkSalah = False Then
                    MessageBox.Show("Silakan gunakan template yang tersedia.")
                    Return
                End If
                'If checkKosong = True OrElse list.Count = 0 Then
                '    MessageBox.Show("Data upload tidak valid.")
                '    Return
                'End If

                If list.Count > 0 Then
                    sessHelper.SetSession("DetailParticipantExcelSession", list)
                End If

                GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

                'objBenefitEventDetail = New BenefitEventDetail
                'Dim objBenefitParticipant As BenefitParticipant = New BenefitParticipant


                'objBenefitParticipant.Nama = txtNamaGrid.Text
                'objBenefitParticipant.Alamat = txtAlamatGrid.Text
                'objBenefitParticipant.KTP = txtKtpGrid.Text

                'objBenefitEventDetail.BenefitParticipant = objBenefitParticipant

                'sessHelper.SetSession("addDetailSession", objBenefitEventDetail)

                'GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

                MessageBox.Show("Data Success : " & dataSuccess.ToString & ", Data Failed : " & dataFailed.ToString)

                retValue = 1
            Catch ex As Exception
                retValue = 0
            Finally
                sapImp.StopImpersonate()
                sapImp = Nothing
            End Try


        End If
    End Sub

    Protected Sub txtNoRegEvent_TextChanged(sender As Object, e As EventArgs) Handles txtNoRegEvent.TextChanged
        Dim objNationalEvent As NationalEvent = New KTB.DNet.BusinessFacade.NationalEventFacade(User).Retrieve(txtNoRegEvent.Text)
        If Not IsNothing(objNationalEvent) AndAlso objNationalEvent.ID > 0 Then
            txtEventName.Text = objNationalEvent.NationalEventType.Name & " " & objNationalEvent.NationalEventCity.City.CityName
            icTanggalEvent.Value = objNationalEvent.PeriodStart
            icTanggalEventEnd.Value = objNationalEvent.PeriodEnd
            sessHelper.SetSession("NationalEvent", objNationalEvent)
            btnUpload.Enabled = True
        Else
            MessageBox.Show("Kode Event salah.")
        End If
    End Sub
End Class
