Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.IO
Imports System.Text

Public Class FrmPreCustomerEntry
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnSearchSalesman As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDateFrom As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDateUntil As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dgSAPCustomer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtSalesmanCode As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtPeriod As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblTotalRow As System.Web.UI.WebControls.Label
    Protected WithEvents btnNoSales As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SAPCustomerFacade As New SAPCustomerFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
#End Region

#Region "Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'InitiateAuthorization()


        If Not IsPostBack Then
            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            lblDealerCode.Text = objuser.Dealer.DealerCode
            lblDealerName.Text = objuser.Dealer.DealerName
            If Request.QueryString("isBack") <> "1" Then
                Initialize()
                BindControlsAttribute()
                BindDataGrid(0)
                dgSAPCustomer.ShowFooter = True
            Else
                Dim arlDataGet As ArrayList = sessHelper.GetSession("DataForSAPReturn")
                txtSalesmanID.Text = arlDataGet(1)
                txtSalesmanName.Text = arlDataGet(2)
                'lblDateFrom.Text = arlDataGet(3)
                'lblDateUntil.Text = arlDataGet(4)
                txtSalesmanCode.Value = arlDataGet(3)
                txtPeriod.Value = arlDataGet(4)
                dgSAPCustomer.CurrentPageIndex = 0
                BindDataGrid(0)
                BindControlsAttribute()
                dgSAPCustomer.ShowFooter = True
            End If
        Else
            'Postback from jscript
            If Request("__EVENTARGUMENT") = "searchsalesman" Then
                btnSearch_Click(Me, System.EventArgs.Empty)
            End If
        End If

        txtSalesmanID.Attributes.Add("readonly", "readonly")
        txtSalesmanName.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        sessHelper.SetSession("CurrentSalesmanHeader", txtSalesmanID.Text)
        dgSAPCustomer.CurrentPageIndex = 0
        dgSAPCustomer.ShowFooter = True
        dgSAPCustomer.EditItemIndex = -1
        BindDataGrid(0)
        'If txtPeriod.Value <> "" Then
        '    ' mengembalikan value hidden field ke label ybs
        '    Dim strTmp As String() = txtPeriod.Value.Split(";")
        '    'lblDateFrom.Text = strTmp(0)
        '    'lblDateUntil.Text = strTmp(1)
        'End If

        ' add security
        'If Not CekProspekCreatePrivilege() Then
        '    dgSAPCustomer.ShowFooter = False
        '    dgSAPCustomer.Columns(8).Visible = False    'kolom aksi
        'End If
    End Sub

    Private Sub dgSAPCustomer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSAPCustomer.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSAPCustomer As SAPCustomer = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSAPCustomer.CurrentPageIndex * dgSAPCustomer.PageSize)
            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                ' mengisi value

                Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDeleteNew.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                lbtnDeleteNew.CommandArgument = objSAPCustomer.ID

                Dim lblNameNew As Label = CType(e.Item.FindControl("lblName"), Label)
                'lblNameNew.Text = objSAPCustomer.SAPRegister.SalesmanHeader.Name

                Dim lblSalesmanCodeNew As Label = CType(e.Item.FindControl("lblSalesmanCode"), Label)
                'lblSalesmanCodeNew.Text = objSAPCustomer.SAPRegister.SalesmanHeader.SalesmanCode

                Dim lblVechileTypeCodeNew As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                lblVechileTypeCodeNew.Text = objSAPCustomer.VechileType.VechileTypeCode

                Dim lblCustomerNameNew As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                lblCustomerNameNew.Text = objSAPCustomer.CustomerName

                Dim lblCustomerAddressNew As Label = CType(e.Item.FindControl("lblCustomerAddress"), Label)
                lblCustomerAddressNew.Text = objSAPCustomer.CustomerAddress

                Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatusNew.Text = CType(objSAPCustomer.Status, EnumSAPCustomerStatus.SAPCustomerStatus).ToString.Replace("_", " ")

                Dim lblPhone As Label = CType(e.Item.FindControl("lblPhone"), Label)
                lblPhone.Text = objSAPCustomer.Phone

                Dim lblGender As Label = CType(e.Item.FindControl("lblGender"), Label)
                lblGender.Text = EnumGender.GetStringGender(objSAPCustomer.Sex)

                Dim lblAge As Label = CType(e.Item.FindControl("lblAge"), Label)
                lblAge.Text = EnumAgeSegment.GetStringAgeSegment(objSAPCustomer.AgeSegment)

                Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
                lblType.Text = EnumInformationType.GetStringInformationType(objSAPCustomer.InformationType)

                Dim lblPurpose As Label = CType(e.Item.FindControl("lblPurpose"), Label)
                lblPurpose.Text = EnumCustomerPurpose.GetStringCustomerPurpose(objSAPCustomer.CustomerPurpose)

                Dim lblSource As Label = CType(e.Item.FindControl("lblSource"), Label)
                lblSource.Text = EnumInformationSource.GetStringInformationSource(objSAPCustomer.InformationSource)
            End If

            ' untuk bagian edit item
            If e.Item.ItemType = ListItemType.EditItem Then

                Dim lbtnSaveNew As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                lbtnSaveNew.CommandArgument = objSAPCustomer.ID

                Dim lblEditVechileTypeCodeNew As Label = CType(e.Item.FindControl("lblEditVechileTypeCode"), Label)
                lblEditVechileTypeCodeNew.Attributes("onclick") = "ShowPopUpVechileType();"

                Dim txtEditVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtEditVechileTypeCode"), TextBox)
                txtEditVechileTypeCodeNew.Text = objSAPCustomer.VechileType.VechileTypeCode

                Dim txtEditCustomerNameNew As TextBox = CType(e.Item.FindControl("txtEditCustomerName"), TextBox)
                txtEditCustomerNameNew.Text = objSAPCustomer.CustomerName

                Dim txtEditCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtEditCustomerAddress"), TextBox)
                txtEditCustomerAddressNew.Text = objSAPCustomer.CustomerAddress

                Dim ddlEditStatusNew As DropDownList = CType(e.Item.FindControl("ddlEditStatus"), DropDownList)
                CommonFunction.BindFromEnum("SAPCustomerStatus", ddlEditStatusNew, User, True, "NameStatus", "ValStatus")
                ddlEditStatusNew.SelectedValue = objSAPCustomer.Status

                Dim txtPhoneE As TextBox = CType(e.Item.FindControl("txtPhoneE"), TextBox)
                txtPhoneE.Text = objSAPCustomer.Phone

                Dim ddlGenderE As DropDownList = CType(e.Item.FindControl("ddlGenderE"), DropDownList)
                BindGender(ddlGenderE)
                If objSAPCustomer.Sex > 0 Then
                    ddlGenderE.SelectedValue = objSAPCustomer.Sex
                Else
                    ddlGenderE.SelectedIndex = 1
                End If


                Dim ddlAgeE As DropDownList = CType(e.Item.FindControl("ddlAgeE"), DropDownList)
                BindAgeSegment(ddlAgeE)
                ddlAgeE.SelectedValue = objSAPCustomer.AgeSegment

                Dim ddlTypeE As DropDownList = CType(e.Item.FindControl("ddlTypeE"), DropDownList)
                BindInformationType(ddlTypeE)
                ddlTypeE.SelectedValue = objSAPCustomer.InformationType

                Dim ddlPurposeE As DropDownList = CType(e.Item.FindControl("ddlPurposeE"), DropDownList)
                BindCustomerPurpose(ddlPurposeE)
                ddlPurposeE.SelectedValue = objSAPCustomer.CustomerPurpose

                Dim ddlSourceE As DropDownList = CType(e.Item.FindControl("ddlSourceE"), DropDownList)
                BindInformationSource(ddlSourceE)
                ddlSourceE.SelectedValue = objSAPCustomer.InformationSource

            End If
        End If

        ' untuk bagian footer
        If e.Item.ItemType = ListItemType.Footer Then

            Dim ArrCopy As New ArrayList
            If Not sessHelper.GetSession("DataCopy") Is Nothing Then
                ArrCopy = sessHelper.GetSession("DataCopy")
            End If


            Dim lblAddVechileTypeCodeNew As Label = CType(e.Item.FindControl("lblAddVechileTypeCode"), Label)
            lblAddVechileTypeCodeNew.Attributes("onclick") = "ShowPopUpVechileType();"

            Dim txtAddVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
            txtAddVechileTypeCodeNew.Text = ""

            Dim txtAddCustomerNameNew As TextBox = CType(e.Item.FindControl("txtAddCustomerName"), TextBox)
            Dim txtAddCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtAddCustomerAddress"), TextBox)
            Dim txtPhoneF As TextBox = CType(e.Item.FindControl("txtPhoneF"), TextBox)
            If (ArrCopy.Count > 0) Then
                txtAddCustomerNameNew.Text = ArrCopy(1).ToString()
                txtAddCustomerAddressNew.Text = ArrCopy(2).ToString()
                txtPhoneF.Text = ArrCopy(4).ToString()
            Else
                txtAddCustomerNameNew.Text = ""
                txtAddCustomerAddressNew.Text = ""
                txtPhoneF.Text = ""
            End If

            Dim ddlAddStatusNew As DropDownList = CType(e.Item.FindControl("ddlAddStatus"), DropDownList)
            CommonFunction.BindFromEnum("SAPCustomerStatus", ddlAddStatusNew, User, True, "NameStatus", "ValStatus")
            If (ArrCopy.Count > 0) Then
                ddlAddStatusNew.SelectedIndex = CInt(ArrCopy(3).ToString())
            Else
                ddlAddStatusNew.SelectedIndex = -1
            End If

            Dim ddlGenderF As DropDownList = CType(e.Item.FindControl("ddlGenderF"), DropDownList)
            BindGender(ddlGenderF)
            If ddlGenderF.Items.Count > 0 Then
                If (ArrCopy.Count > 0) Then
                    ddlGenderF.SelectedValue = CInt(ArrCopy(5).ToString())
                Else
                    ddlGenderF.SelectedIndex = 0
                End If
            End If

            Dim ddlAgeF As DropDownList = CType(e.Item.FindControl("ddlAgeF"), DropDownList)
            BindAgeSegment(ddlAgeF)
            If ddlAgeF.Items.Count > 0 Then
                If (ArrCopy.Count > 0) Then
                    ddlAgeF.SelectedValue = CInt(ArrCopy(6).ToString())
                Else
                    ddlAgeF.SelectedIndex = 0
                End If
            Else
                ddlAgeF.ClearSelection()
            End If
            '
            Dim ddlTypeF As DropDownList = CType(e.Item.FindControl("ddlTypeF"), DropDownList)
            BindInformationType(ddlTypeF)
            If ddlTypeF.Items.Count > 0 Then
                If (ArrCopy.Count > 0) Then
                    ddlTypeF.SelectedValue = CInt(ArrCopy(7).ToString())
                Else
                    ddlTypeF.SelectedIndex = 0
                End If
            Else
                ddlTypeF.ClearSelection()
            End If

            Dim ddlPurposeF As DropDownList = CType(e.Item.FindControl("ddlPurposeF"), DropDownList)
            BindCustomerPurpose(ddlPurposeF)
            If ddlPurposeF.Items.Count > 0 Then
                If (ArrCopy.Count > 0) Then
                    ddlPurposeF.SelectedValue = CInt(ArrCopy(8).ToString())
                Else
                    ddlPurposeF.SelectedIndex = 0
                End If
            Else
                ddlPurposeF.ClearSelection()
            End If

            Dim ddlSourceF As DropDownList = CType(e.Item.FindControl("ddlSourceF"), DropDownList)
            BindInformationSource(ddlSourceF)
            If ddlSourceF.Items.Count > 0 Then
                If (ArrCopy.Count > 0) Then
                    ddlSourceF.SelectedValue = CInt(ArrCopy(9).ToString())
                Else
                    ddlSourceF.SelectedIndex = 0
                End If
            Else
                ddlSourceF.ClearSelection()
            End If
        End If
    End Sub

    Private Sub BindGender(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumGenderOp.RetriveSalesGender(True)
        For Each item As EnumGenderOp In arrList
            Dim listItem As New listItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindAgeSegment(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumAgeSegmentOp.RetriveAgeSegment(True)
        For Each item As EnumAgeSegmentOp In arrList
            Dim listItem As New listItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindInformationType(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumInformationTypeOp.RetriveInformationType(True)
        For Each item As EnumInformationTypeOp In arrList
            Dim listItem As New listItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next

    End Sub

    Private Sub BindInformationSource(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumInformationSourceOp.RetriveInformationSource(True)
        For Each item As EnumInformationSourceOp In arrList
            Dim listItem As New listItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindCustomerPurpose(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumCustomerPurposeOp.RetriveCustomerPurpose(True)
        For Each item As EnumCustomerPurposeOp In arrList
            Dim listItem As New listItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub AddRawData(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, Optional ByVal IsCopy As Boolean = False)
        Dim facade As SAPCustomerFacade = New SAPCustomerFacade(User)
        Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        Dim txtAddQty As TextBox = CType(e.Item.FindControl("txtAddQty"), TextBox)
        Dim txtAddCustomerNameNew As TextBox = CType(e.Item.FindControl("txtAddCustomerName"), TextBox)
        Dim txtAddCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtAddCustomerAddress"), TextBox)
        Dim txtAddVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
        Dim txtPhoneF As TextBox = CType(e.Item.FindControl("txtPhoneF"), TextBox)
        Dim ddlAddStatusNew As DropDownList = CType(e.Item.FindControl("ddlAddStatus"), DropDownList)
        Dim ddlGenderF As DropDownList = CType(e.Item.FindControl("ddlGenderF"), DropDownList)
        Dim ddlAgeF As DropDownList = CType(e.Item.FindControl("ddlAgeF"), DropDownList)
        Dim ddlTypeF As DropDownList = CType(e.Item.FindControl("ddlTypeF"), DropDownList)
        Dim ddlPurposeF As DropDownList = CType(e.Item.FindControl("ddlPurposeF"), DropDownList)
        Dim ddlSourceF As DropDownList = CType(e.Item.FindControl("ddlSourceF"), DropDownList)

        Dim strMessage As String = ""

        'If txtSalesmanID.Text = "" Then
        '    MessageBox.Show("Salesman ID harus diisi dahulu !")
        '    Return
        'End If

        If txtAddCustomerNameNew.Text = "" Then
            'MessageBox.Show("Nama Customer harus diisi dahulu !")
            'Return
            strMessage &= "Nama customer harus diisi. \n"
        End If

        If txtAddCustomerAddressNew.Text = "" Then
            'MessageBox.Show("Alamat Customer harus diisi dahulu !")
            'Return
            strMessage &= "Alamat customer harus diisi. \n"
        End If

        If ddlAddStatusNew.SelectedItem.Text = "" Then
            'MessageBox.Show("Silakan memilih status terlebih dahulu !")
            'Return
            strMessage &= "Status harus dipilih.  \n"
        End If

        If ddlGenderF.SelectedIndex = -1 Then
            'MessageBox.Show("Silakan memilih gender terlebih dahulu !")
            'Return
            strMessage &= "Gender harus dipilih. \n"
        End If

        If ddlTypeF.SelectedIndex = 0 Then
            'MessageBox.Show("Silakan memilih tipe informasi terlebih dahulu !")
            'Return
            strMessage &= "Tipe informasi harus dipilih. \n"
        End If

        If ddlPurposeF.SelectedIndex = 0 Then
            'MessageBox.Show("Silakan memilih tujuan konsumen terlebih dahulu !")
            'Return
            strMessage &= "Tujuan konsumen harus dipilih. \n"
        End If

        If txtAddVechileTypeCodeNew.Text = "" Then
            'MessageBox.Show("Tipe kendaraan harus diisi dahulu !")
            'Return
            strMessage &= "Tipe kendaraan harus dipilih. \n"
        Else
            ' cek apakah data valid saat diinput
            Dim arrVechileType As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtAddVechileTypeCodeNew.Text))
            arrVechileType = New VechileTypeFacade(User).Retrieve(criterias)
            If Not IsNothing(arrVechileType) Then
                If arrVechileType.Count < 1 Then
                    'MessageBox.Show("Tipe kendaraan harus diisi dengan data yang valid, gunakan pop up !")
                    'Return
                    strMessage &= "Tipe kendaraan tidak valid. \n"
                End If
            End If
        End If


        If Val(txtAddQty.Text) = 0 Then
            'MessageBox.Show("Silakan isi qty terlebih dahulu !")
            'Return
            strMessage &= "Qty harus diisi. \n"
        End If

        If ddlSourceF.SelectedIndex = 0 Then
            'MessageBox.Show("Silakan memilih sumber informasi terlebih dahulu !")
            'Return
            strMessage &= "Sumber informasi harus dipilih. \n"
        End If

        If strMessage.Trim <> "" Then
            MessageBox.Show(strMessage)
            Return
        End If

        Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtAddVechileTypeCodeNew.Text.Trim)

        Dim arrSAPCustomer As ArrayList
        Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "CustomerName", MatchType.Exact, txtAddCustomerNameNew.Text))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "CustomerAddress", MatchType.Exact, txtAddCustomerAddressNew.Text))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "Phone", MatchType.Exact, txtPhoneF.Text))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlAddStatusNew.SelectedValue))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "VechileType.ID", MatchType.Exact, objVechileType.ID))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "Sex", MatchType.Exact, ddlGenderF.SelectedValue))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "InformationType", MatchType.Exact, ddlTypeF.SelectedValue))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "InformationSource", MatchType.Exact, ddlSourceF.SelectedValue))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "CustomerPurpose", MatchType.Exact, ddlPurposeF.SelectedValue))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "AgeSegment", MatchType.Exact, ddlAgeF.SelectedValue))
        criteria.opAnd(New criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Exact, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)))
        If txtSalesmanID.Text.Trim <> String.Empty Then
            criteria.opAnd(New criteria(GetType(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanID.Text.Trim))
        End If
        If Not IsNothing(objuser) Then
            criteria.opAnd(New criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, objuser.Dealer.ID))
        End If

        arrSAPCustomer = New SAPCustomerFacade(User).Retrieve(criteria)
        If Not IsNothing(arrSAPCustomer) Then
            If arrSAPCustomer.Count > 0 Then
                MessageBox.Show("Data sudah ada, tidak bisa di masukan lagi !")
                Return
            End If
        End If


        Dim objSAPCustomer As SAPCustomer = New SAPCustomer
        Dim objSalesmanHeader As SalesmanHeader
        If txtSalesmanID.Text.Trim <> String.Empty Then
            objSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            objSAPCustomer.SalesmanHeader = objSalesmanHeader
        Else
            objSAPCustomer.SalesmanHeader = Nothing
        End If

        If Not IsNothing(objuser) Then
            objSAPCustomer.Dealer = objuser.Dealer
        End If
        objSAPCustomer.Qty = Val(txtAddQty.Text)
        objSAPCustomer.VechileType = objVechileType
        objSAPCustomer.CustomerName = txtAddCustomerNameNew.Text
        objSAPCustomer.CustomerAddress = txtAddCustomerAddressNew.Text
        objSAPCustomer.Status = ddlAddStatusNew.SelectedValue
        objSAPCustomer.Phone = txtPhoneF.Text.Trim
        objSAPCustomer.Sex = ddlGenderF.SelectedValue
        objSAPCustomer.AgeSegment = ddlAgeF.SelectedValue
        objSAPCustomer.InformationType = ddlTypeF.SelectedValue
        objSAPCustomer.InformationSource = ddlSourceF.SelectedValue
        objSAPCustomer.CustomerPurpose = ddlPurposeF.SelectedValue
        objSAPCustomer.ProspectDate = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)


        Dim result As Integer = facade.Insert(objSAPCustomer)

        If result = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
            If IsCopy Then
                Dim ArrCopy As New ArrayList
                ArrCopy.Add(txtAddQty.Text)
                ArrCopy.Add(txtAddCustomerNameNew.Text)
                ArrCopy.Add(txtAddCustomerAddressNew.Text)
                ArrCopy.Add(ddlAddStatusNew.SelectedValue)
                ArrCopy.Add(txtPhoneF.Text)
                ArrCopy.Add(ddlGenderF.SelectedValue)
                ArrCopy.Add(ddlAgeF.SelectedValue)
                ArrCopy.Add(ddlTypeF.SelectedValue)
                ArrCopy.Add(ddlPurposeF.SelectedValue)
                ArrCopy.Add(ddlSourceF.SelectedValue)

                sessHelper.SetSession("DataCopy", ArrCopy)
            Else
                sessHelper.SetSession("DataCopy", Nothing)
            End If
        End If

        If dgSAPCustomer.Items.Count = dgSAPCustomer.PageSize And dgSAPCustomer.CurrentPageIndex = (dgSAPCustomer.PageCount - 1) Then
            BindDataGrid(dgSAPCustomer.PageCount)
        Else
            If dgSAPCustomer.PageCount > 1 Then
                BindDataGrid(dgSAPCustomer.PageCount - 1)
            Else
                BindDataGrid(0)
            End If
        End If
    End Sub

    Private Sub dgSAPCustomer_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSAPCustomer.ItemCommand
        Dim facade As SAPCustomerFacade = New SAPCustomerFacade(User)

        If e.CommandName = "Delete" Then
            Dim objSAPCustomer As SAPCustomer = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSAPCustomer)
            BindDataGrid(0)
        End If
        If e.CommandName = "Edit" Then
            dgSAPCustomer.ShowFooter = False
            dgSAPCustomer.EditItemIndex = e.Item.ItemIndex
            BindDataGrid(dgSAPCustomer.CurrentPageIndex)
            lblSearchSalesman.Visible = True
            btnSearch.Enabled = False

        End If

        If e.CommandName = "Cancel" Then
            dgSAPCustomer.ShowFooter = True
            dgSAPCustomer.EditItemIndex = -1
            lblSearchSalesman.Visible = True
            BindDataGrid(dgSAPCustomer.CurrentPageIndex)
        End If

        If e.CommandName = "Add" Then
            AddRawData(e)
        End If

        If e.CommandName = "AddCopy" Then
            AddRawData(e, True)
        End If

        If e.CommandName = "Save" Then
            lblSearchSalesman.Visible = True
            Dim objSAPCustomer As SAPCustomer = New SAPCustomerFacade(User).Retrieve(CInt(e.CommandArgument))

            'If txtSalesmanID.Text = "" Then
            '    MessageBox.Show("Salesman ID harus diisi dahulu !")
            '    Return
            'End If

            Dim txtEditQty As TextBox = CType(e.Item.FindControl("txtEditQty"), TextBox)
            Dim txtEditCustomerNameNew As TextBox = CType(e.Item.FindControl("txtEditCustomerName"), TextBox)
            Dim txtEditCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtEditCustomerAddress"), TextBox)
            Dim txtEditVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtEditVechileTypeCode"), TextBox)
            Dim ddlEditStatusNew As DropDownList = CType(e.Item.FindControl("ddlEditStatus"), DropDownList)
            Dim txtPhoneE As TextBox = CType(e.Item.FindControl("txtPhoneE"), TextBox)
            Dim ddlGenderE As DropDownList = CType(e.Item.FindControl("ddlGenderE"), DropDownList)
            Dim ddlAgeE As DropDownList = CType(e.Item.FindControl("ddlAgeE"), DropDownList)
            Dim ddlTypeE As DropDownList = CType(e.Item.FindControl("ddlTypeE"), DropDownList)
            Dim ddlPurposeE As DropDownList = CType(e.Item.FindControl("ddlPurposeE"), DropDownList)
            Dim ddlSourceE As DropDownList = CType(e.Item.FindControl("ddlSourceE"), DropDownList)


            Dim strMessage As String = ""

            If txtEditCustomerNameNew.Text = "" Then
                'MessageBox.Show("Nama Customer harus diisi dahulu !")
                'Return
                strMessage &= "Nama customer harus diisi. \n"
            End If

            If txtEditCustomerAddressNew.Text = "" Then
                'MessageBox.Show("Alamat Customer harus diisi dahulu !")
                'Return
                strMessage &= "Alamat customer harus diisi. \n"
            End If

            If Val(txtEditQty.Text) = 0 Then
                'MessageBox.Show("Qty harus diisi dahulu !")
                'Return
                strMessage &= "Qty harus diisi. \n"
            End If

            If txtEditVechileTypeCodeNew.Text = "" Then
                'MessageBox.Show("Tipe kendaraan harus diisi dahulu !")
                'Return
                strMessage &= "Tipe kendaraan harus dipilih. \n"
            Else
                ' cek apakah data valid saat diinput
                Dim arrVechileType As ArrayList
                Dim criteriax As CriteriaComposite = New CriteriaComposite(New criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriax.opAnd(New criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtEditVechileTypeCodeNew.Text))
                arrVechileType = New VechileTypeFacade(User).Retrieve(criteriax)
                If Not IsNothing(arrVechileType) Then
                    If arrVechileType.Count < 1 Then
                        'MessageBox.Show("Tipe kendaraan harus diisi dengan data yang valid, gunakan pop up !")
                        'Return
                        strMessage &= "Tipe kendaraan tidak valid. \n"
                    End If
                End If
            End If

            If ddlEditStatusNew.SelectedItem.Text = "" Then
                'MessageBox.Show("Silakan memilih status terlebih dahulu !")
                'Return
                strMessage &= "Status harus dipilih. \n"
            End If

            If ddlGenderE.SelectedIndex = -1 Then
                'MessageBox.Show("Silakan memilih gender terlebih dahulu !")
                'Return
                strMessage &= "Gender harus dipilih. \n"
            End If

            If ddlTypeE.SelectedIndex = 0 Then
                'MessageBox.Show("Silakan memilih tipe informasi terlebih dahulu !")
                'Return
                strMessage &= "Tipe informasi harus dipilih. \n"
            End If

            If ddlPurposeE.SelectedIndex = 0 Then
                'MessageBox.Show("Silakan memilih tujuan konsumen terlebih dahulu !")
                'Return
                strMessage &= "Tujuan konsumen harus dipilih. \n"
            End If

            If ddlSourceE.SelectedIndex = 0 Then
                'MessageBox.Show("Silakan memilih sumber informasi terlebih dahulu !")
                'Return
                strMessage &= "Sumber informasi harus dipilih. \n"
            End If

            If strMessage.Trim <> "" Then
                MessageBox.Show(strMessage)
                Return
            End If

            Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtEditVechileTypeCodeNew.Text.Trim)
            Dim arrSAPCustomer As ArrayList
            Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "CustomerName", MatchType.Exact, txtEditCustomerNameNew.Text.Trim))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "CustomerAddress", MatchType.Exact, txtEditCustomerAddressNew.Text.Trim))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "Phone", MatchType.Exact, txtPhoneE.Text.Trim))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlEditStatusNew.SelectedValue))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "VechileType.ID", MatchType.Exact, objVechileType.ID))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "Sex", MatchType.Exact, ddlGenderE.SelectedValue))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "InformationType", MatchType.Exact, ddlTypeE.SelectedValue))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "InformationSource", MatchType.Exact, ddlSourceE.SelectedValue))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "CustomerPurpose", MatchType.Exact, ddlPurposeE.SelectedValue))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "AgeSegment", MatchType.Exact, ddlAgeE.SelectedValue))
            'criteria.opAnd(New criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Exact, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)))
            If txtSalesmanID.Text.Trim <> String.Empty Then
                criteria.opAnd(New criteria(GetType(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanID.Text.Trim))
            End If

            arrSAPCustomer = New SAPCustomerFacade(User).Retrieve(criteria)
            If Not IsNothing(arrSAPCustomer) Then
                If arrSAPCustomer.Count > 0 Then
                    MessageBox.Show("Data sudah ada, tidak bisa di masukan lagi !")
                    Return
                End If
            End If

            'Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            'objSAPCustomer.SAPRegister = objSAPRegister
            Dim objSalesmanHeader As SalesmanHeader
            If txtSalesmanID.Text.Trim <> String.Empty Then
                objSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
                objSAPCustomer.SalesmanHeader = objSalesmanHeader
            Else
                objSAPCustomer.SalesmanHeader = Nothing
            End If
            objSAPCustomer.qty = Val(txtEditQty.Text)
            objSAPCustomer.VechileType = objVechileType
            objSAPCustomer.CustomerName = txtEditCustomerNameNew.Text
            objSAPCustomer.CustomerAddress = txtEditCustomerAddressNew.Text
            objSAPCustomer.Status = ddlEditStatusNew.SelectedValue
            objSAPCustomer.Phone = txtPhoneE.Text.Trim
            objSAPCustomer.Sex = ddlGenderE.SelectedValue
            objSAPCustomer.AgeSegment = ddlAgeE.SelectedValue
            objSAPCustomer.InformationType = ddlTypeE.SelectedValue
            objSAPCustomer.InformationSource = ddlSourceE.SelectedValue
            objSAPCustomer.CustomerPurpose = ddlPurposeE.SelectedValue

            Dim result As Integer = facade.Update(objSAPCustomer)
            If result = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
            End If
            dgSAPCustomer.ShowFooter = True
            dgSAPCustomer.EditItemIndex = -1
            BindDataGrid(dgSAPCustomer.CurrentPageIndex)

        End If

        'add by ery for direct register
        If e.CommandName = "Register" Then
            Dim arlData As New ArrayList
            Dim id As Integer = CInt(e.CommandArgument)

            arlData.Add(id)
            arlData.Add(txtSalesmanID.Text)
            arlData.Add(txtSalesmanName.Text)
            'arlData.Add(lblDateFrom.Text)
            'arlData.Add(lblDateUntil.Text)
            arlData.Add(txtSalesmanCode.Value)
            arlData.Add(txtPeriod.Value)
            sessHelper.SetSession("DataForSAPReturn", arlData)

            Response.Redirect("../Customer/FrmCustomerRequest.aspx?isSAP=true&SAPID=" & id)
        End If
    End Sub

    Private Sub dgSAPCustomer_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSAPCustomer.PageIndexChanged
        BindDataGrid(e.NewPageIndex)
    End Sub

    Private Sub dgSAPCustomer_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSAPCustomer.SortCommand
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
        dgSAPCustomer.SelectedIndex = -1
        BindDataGrid(dgSAPCustomer.CurrentPageIndex)
    End Sub
#End Region

#Region "Custom"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.CustomerProspekView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SAP - Konsumen Prospek")
        End If
    End Sub

    Private Function CekProspekCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.CustomerProspekCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub BindControlsAttribute()
        lblSearchSalesman.Attributes("onclick") = "ShowPopUpSAPRegisterSalesman();"
    End Sub

    Private Sub Initialize()
        ClearData()
        'ViewState("CurrentSortColumn") = "AreaCode"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub ClearData()
        'txtSalesmanID.Enabled = True
        'txtSalesmanName.Enabled = True
        txtSalesmanID.Text = String.Empty
        txtSalesmanName.Text = String.Empty
        'lblDateFrom.Text = String.Empty
        'lblDateUntil.Text = String.Empty
        txtSalesmanCode.Value = String.Empty
        txtPeriod.Value = String.Empty

        'If dgSAPCustomer.Items.Count > 0 Then
        '    dgSAPCustomer.SelectedIndex = -1
        '    dgSAPCustomer.DataSource = New ArrayList
        '    dgSAPCustomer.DataBind()
        'End If
        dgSAPCustomer.DataSource = New ArrayList
        dgSAPCustomer.DataBind()
        lblSearchSalesman.Visible = True
        lblTotalRow.Text = String.Empty
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        
        If txtSalesmanID.Text = "" Then
            'arrList = New ArrayList
            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, objuser.Dealer.ID))
            Dim startdate As DateTime = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, 0, 0, 0)
            Dim enddate As DateTime = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.GreaterOrEqual, startdate))
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Lesser, enddate.AddDays(1)))
        Else
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanID.Text.Trim()))
            'arrList = _SAPCustomerFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSAPCustomer.PageSize, totalRow, _
            'CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        End If
        arrList = _SAPCustomerFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSAPCustomer.PageSize, totalRow, _
                    CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSAPCustomer.CurrentPageIndex = idxPage
        dgSAPCustomer.DataSource = arrList
        dgSAPCustomer.VirtualItemCount = totalRow
        dgSAPCustomer.DataBind()
        lblTotalRow.Text = "Jumlah record : " & totalRow.ToString
    End Sub
#End Region

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        btnSearch.Enabled = True
        sessHelper.SetSession("CurrentSalesmanHeader", Nothing)
    End Sub

    Private Sub btnNoSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoSales.Click
        ClearData()

        'txtSalesmanID.Enabled = False
        'txtSalesmanName.Enabled = False

        dgSAPCustomer.SelectedIndex = -1
        dgSAPCustomer.DataSource = New ArrayList
        dgSAPCustomer.DataBind()
    End Sub
End Class
