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



Public Class FrmSAPCustomer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents dgSAPCustomer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDateFrom As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanCode As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtPeriod As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblDateUntil As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanID As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnSearchSalesman As System.Web.UI.WebControls.LinkButton

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

#Region "PrivateCustomMethods"




#End Region

#Region "EventHandlers"


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        sessHelper.SetSession("CurrentSalesmanHeader", txtSalesmanID.Text)
        dgSAPCustomer.CurrentPageIndex = 0
        BindDataGrid(0)
        If txtPeriod.Value <> "" Then
            ' mengembalikan value hidden field ke label ybs
            Dim strTmp As String() = txtPeriod.Value.Split(";")
            lblDateFrom.Text = strTmp(0)
            lblDateUntil.Text = strTmp(1)
        End If

        ' add security
        If Not CekProspekCreatePrivilege() Then
            dgSAPCustomer.ShowFooter = False
            dgSAPCustomer.Columns(8).Visible = False    'kolom aksi
        End If
    End Sub

    Private Sub BindDropDown()


    End Sub

    Private Function BindDataSalesman(ByVal ddlSalesmanHeader As DropDownList)
        ddlSalesmanHeader.Items.Clear()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPRegister), "IsCancelled", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, txtSalesmanID.Text.Trim))
        Dim arrSAPRegister As ArrayList = New SAPRegisterFacade(User).Retrieve(criterias)
        Dim strSalesmanHeaderId As String
        strSalesmanHeaderId = ""
        If Not IsNothing(arrSAPRegister) Then
            If arrSAPRegister.Count > 0 Then
                For Each item As SAPRegister In arrSAPRegister
                    If strSalesmanHeaderId = "" Then
                        strSalesmanHeaderId = item.SalesmanHeader.ID
                    Else
                        strSalesmanHeaderId = strSalesmanHeaderId & ";" & item.SalesmanHeader.ID
                    End If
                Next
            End If
        End If

        ' mengambil salesman yg register pd periode bersangkutan
        Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If strSalesmanHeaderId <> "" Then
            'Todo Inset
            crits.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, CommonFunction.GetStrValue(strSalesmanHeaderId, ";", ",")))
        End If
        crits.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))
        Dim arrSalesmanHeader As ArrayList = New SalesmanHeaderFacade(User).Retrieve(crits)

        'Dim list As New ListItem("Silakan Pilih", "")
        'list.Selected = True
        'ddlSalesmanLevel.Items.Add(list)

        For Each item As SalesmanHeader In arrSalesmanHeader
            Dim _list As New ListItem(item.Name, item.SalesmanCode)
            ddlSalesmanHeader.Items.Add(_list)
        Next
        ddlSalesmanHeader.DataBind()
    End Function

    Private Sub dgSAPCustomer_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSAPCustomer.ItemDataBound
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

                Dim lblProspectDateNew As Label = CType(e.Item.FindControl("lblProspectDate"), Label)
                lblProspectDateNew.Text = objSAPCustomer.ProspectDate.ToString("dd/MM/yyyy")


            End If

            ' untuk bagian edit item
            If e.Item.ItemType = ListItemType.EditItem Then

                Dim lbtnSaveNew As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                lbtnSaveNew.CommandArgument = objSAPCustomer.ID

                Dim lblEditVechileTypeCodeNew As Label = CType(e.Item.FindControl("lblEditVechileTypeCode"), Label)
                lblEditVechileTypeCodeNew.Attributes("onclick") = "ShowPopUpVechileType();"

                'Dim ddlEditNameNew As DropDownList = CType(e.Item.FindControl("ddlEditName"), DropDownList)
                'BindDataSalesman(ddlEditNameNew)
                'If objSAPCustomer.SAPRegister.SalesmanHeader.RowStatus = CType(DBRowStatus.Active, Short) Then
                '    ddlEditNameNew.SelectedValue = objSAPCustomer.SAPRegister.SalesmanHeader.SalesmanCode
                'End If
                'ddlEditNameNew.Attributes.Add("onchange", "SetSalesmanCode(this,'Edit')")

                Dim txtEditNameNew As TextBox = CType(e.Item.FindControl("txtEditName"), TextBox)
                'txtEditNameNew.Text = objSAPCustomer.SAPRegister.SalesmanHeader.Name
                'Dim lblEditNameNew As Label = CType(e.Item.FindControl("lblEditName"), Label)
                'lblEditNameNew.Attributes("onclick") = "ShowPopUpSAPRegisterSalesman();"

                'Dim lblEditSalesmanCodeNew As Label = CType(e.Item.FindControl("lblEditSalesmanCode"), Label)
                'lblEditSalesmanCodeNew.Text = objSAPCustomer.SAPRegister.SalesmanHeader.SalesmanCode

                Dim txtEditVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtEditVechileTypeCode"), TextBox)
                txtEditVechileTypeCodeNew.Text = objSAPCustomer.VechileType.VechileTypeCode

                Dim txtEditCustomerNameNew As TextBox = CType(e.Item.FindControl("txtEditCustomerName"), TextBox)
                txtEditCustomerNameNew.Text = objSAPCustomer.CustomerName

                Dim txtEditCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtEditCustomerAddress"), TextBox)
                txtEditCustomerAddressNew.Text = objSAPCustomer.CustomerAddress

                Dim ddlEditStatusNew As DropDownList = CType(e.Item.FindControl("ddlEditStatus"), DropDownList)
                CommonFunction.BindFromEnum("SAPCustomerStatus", ddlEditStatusNew, User, True, "NameStatus", "ValStatus")
                ddlEditStatusNew.SelectedValue = objSAPCustomer.Status

                Dim icEditProspectDateNew As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icEditProspectDate"), KTB.DNet.WebCC.IntiCalendar)
                icEditProspectDateNew.Value = objSAPCustomer.ProspectDate
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

            'Dim ddlAddNameNew As DropDownList = CType(e.Item.FindControl("ddlAddName"), DropDownList)
            'BindDataSalesman(ddlAddNameNew)
            'ddlAddNameNew.SelectedIndex = -1
            'ddlAddNameNew.Attributes.Add("onchange", "SetSalesmanCode(this,'Add')")

            Dim txtAddVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
            txtAddVechileTypeCodeNew.Text = ""

            Dim txtAddCustomerNameNew As TextBox = CType(e.Item.FindControl("txtAddCustomerName"), TextBox)
            If (ArrCopy.Count > 0) Then
                txtAddCustomerNameNew.Text = ArrCopy(1).ToString()
            Else
                txtAddCustomerNameNew.Text = ""
            End If


            Dim txtAddCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtAddCustomerAddress"), TextBox)
            If (ArrCopy.Count > 0) Then
                txtAddCustomerAddressNew.Text = ArrCopy(2).ToString()
            Else
                txtAddCustomerAddressNew.Text = ""
            End If


            Dim ddlAddStatusNew As DropDownList = CType(e.Item.FindControl("ddlAddStatus"), DropDownList)
            CommonFunction.BindFromEnum("SAPCustomerStatus", ddlAddStatusNew, User, True, "NameStatus", "ValStatus")
            If (ArrCopy.Count > 0) Then
                ddlAddStatusNew.SelectedIndex = CInt(ArrCopy(3).ToString())
            Else
                ddlAddStatusNew.SelectedIndex = -1
            End If


            Dim icAddProspectDateNew As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icAddProspectDate"), KTB.DNet.WebCC.IntiCalendar)
            If (ArrCopy.Count > 0) Then
                icAddProspectDateNew.Value = CDate(ArrCopy(4).ToString())
            Else
                icAddProspectDateNew.Value = Now
            End If
        End If
    End Sub
    Private Sub dgSAPCustomer_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSAPCustomer.ItemCommand
        Dim facade As SAPCustomerFacade = New SAPCustomerFacade(User)

        If e.CommandName = "Delete" Then
            Dim objSAPCustomer As SAPCustomer = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSAPCustomer)
            BindDataGrid(0)
        End If
        If e.CommandName = "Edit" Then
            lblSearchSalesman.Visible = False
            dgSAPCustomer.ShowFooter = False
            dgSAPCustomer.EditItemIndex = e.Item.ItemIndex
            BindDataGrid(dgSAPCustomer.CurrentPageIndex)
        End If

        If e.CommandName = "Cancel" Then
            dgSAPCustomer.ShowFooter = True
            dgSAPCustomer.EditItemIndex = -1
            lblSearchSalesman.Visible = True
            BindDataGrid(dgSAPCustomer.CurrentPageIndex)
        End If

        If e.CommandName = "Add" Then

            ' start check validation #########################
            If txtSalesmanID.Text = "" Then
                MessageBox.Show("Salesman ID harus diisi dahulu !")
                Return
            End If


            Dim txtAddQty As TextBox = CType(e.Item.FindControl("txtAddQty"), TextBox)
            Dim txtAddCustomerNameNew As TextBox = CType(e.Item.FindControl("txtAddCustomerName"), TextBox)
            Dim txtAddCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtAddCustomerAddress"), TextBox)
            Dim txtAddVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
            Dim ddlAddStatusNew As DropDownList = CType(e.Item.FindControl("ddlAddStatus"), DropDownList)
            Dim icAddProspectDateNew As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icAddProspectDate"), KTB.DNet.WebCC.IntiCalendar)                        

            If txtAddCustomerNameNew.Text = "" Then
                MessageBox.Show("Nama Customer harus diisi dahulu !")
                Return
            End If

            If txtAddCustomerAddressNew.Text = "" Then
                MessageBox.Show("Alamat Customer harus diisi dahulu !")
                Return
            End If

            If txtAddVechileTypeCodeNew.Text = "" Then
                MessageBox.Show("Tipe kendaraan harus diisi dahulu !")
                Return
            Else
                ' cek apakah data valid saat diinput
                Dim arrVechileType As ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtAddVechileTypeCodeNew.Text))
                arrVechileType = New VechileTypeFacade(User).Retrieve(criterias)
                If Not IsNothing(arrVechileType) Then
                    If arrVechileType.Count < 1 Then
                        MessageBox.Show("Tipe kendaraan harus diisi dengan data yang valid, gunakan pop up !")
                        Return
                    End If
                End If
            End If

            If ddlAddStatusNew.SelectedItem.Text = "" Then
                MessageBox.Show("Silakan memilih status terlebih dahulu !")
                Return
            End If

            If Val(txtAddQty.Text) = 0 Then
                MessageBox.Show("Silakan isi qty terlebih dahulu !")
                Return
            End If

            Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtAddVechileTypeCodeNew.Text.Trim)
            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

            Dim arrSAPCustomer As ArrayList
            'remarked by anh 20140417 biar gak muncul di modul Marketing>Konsumen
            'Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, 3))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "CustomerName", MatchType.Exact, txtAddCustomerNameNew.Text))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "CustomerAddress", MatchType.Exact, txtAddCustomerAddressNew.Text))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlAddStatusNew.SelectedValue))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Exact, Format(icAddProspectDateNew.Value, "yyyy-MM-dd HH:mm:ss")))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "VechileType.ID", MatchType.Exact, objVechileType.ID))
            If Not IsNothing(objuser) Then
                criteria.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, objuser.Dealer.ID))
            End If
            arrSAPCustomer = New SAPCustomerFacade(User).Retrieve(criteria)
            If Not IsNothing(arrSAPCustomer) Then
                If arrSAPCustomer.Count > 0 Then
                    MessageBox.Show("Data sudah ada, tidak bisa di masukan lagi !")
                    Return
                End If
            End If


            Dim objSAPCustomer As SAPCustomer = New SAPCustomer
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            If Not IsNothing(objuser) Then
                objSAPCustomer.Dealer = objuser.Dealer
            End If
            objSAPCustomer.SalesmanHeader = objSalesmanHeader
            objSAPCustomer.Qty = Val(txtAddQty.Text)
            objSAPCustomer.VechileType = objVechileType
            objSAPCustomer.CustomerName = txtAddCustomerNameNew.Text
            objSAPCustomer.CustomerAddress = txtAddCustomerAddressNew.Text
            objSAPCustomer.Status = ddlAddStatusNew.SelectedValue
            objSAPCustomer.ProspectDate = icAddProspectDateNew.Value

            Dim result As Integer = facade.Insert(objSAPCustomer)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                Dim ArrCopy As New ArrayList
                sessHelper.SetSession("DataCopy", ArrCopy)
            End If

            If dgSAPCustomer.Items.Count = 25 And dgSAPCustomer.CurrentPageIndex = (dgSAPCustomer.PageCount - 1) Then
                BindDataGrid(dgSAPCustomer.PageCount)
            Else
                BindDataGrid(dgSAPCustomer.PageCount - 1)
            End If

        End If


        If e.CommandName = "AddCopy" Then

            ' start check validation #########################
            If txtSalesmanID.Text = "" Then
                MessageBox.Show("Salesman ID harus diisi dahulu !")
                Return
            End If


            Dim txtAddQty As TextBox = CType(e.Item.FindControl("txtAddQty"), TextBox)
            Dim txtAddCustomerNameNew As TextBox = CType(e.Item.FindControl("txtAddCustomerName"), TextBox)
            Dim txtAddCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtAddCustomerAddress"), TextBox)
            Dim txtAddVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
            Dim ddlAddStatusNew As DropDownList = CType(e.Item.FindControl("ddlAddStatus"), DropDownList)
            Dim icAddProspectDateNew As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icAddProspectDate"), KTB.DNet.WebCC.IntiCalendar)

            If txtAddCustomerNameNew.Text = "" Then
                MessageBox.Show("Nama Customer harus diisi dahulu !")
                Return
            End If

            If txtAddCustomerAddressNew.Text = "" Then
                MessageBox.Show("Alamat Customer harus diisi dahulu !")
                Return
            End If

            If txtAddVechileTypeCodeNew.Text = "" Then
                MessageBox.Show("Tipe kendaraan harus diisi dahulu !")
                Return
            Else
                ' cek apakah data valid saat diinput
                Dim arrVechileType As ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtAddVechileTypeCodeNew.Text))
                arrVechileType = New VechileTypeFacade(User).Retrieve(criterias)
                If Not IsNothing(arrVechileType) Then
                    If arrVechileType.Count < 1 Then
                        MessageBox.Show("Tipe kendaraan harus diisi dengan data yang valid, gunakan pop up !")
                        Return
                    End If
                End If
            End If

            If ddlAddStatusNew.SelectedItem.Text = "" Then
                MessageBox.Show("Silakan memilih status terlebih dahulu !")
                Return
            End If

            If Val(txtAddQty.Text) = 0 Then
                MessageBox.Show("Silakan isi qty terlebih dahulu !")
                Return
            End If


            Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtAddVechileTypeCodeNew.Text.Trim)
            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

            Dim arrSAPCustomer As ArrayList
            'remarked by anh 20140417 biar gak muncul di modul Marketing>Konsumen
            'Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, 3))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerName", MatchType.Exact, txtAddCustomerNameNew.Text))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerAddress", MatchType.Exact, txtAddCustomerAddressNew.Text))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlAddStatusNew.SelectedValue))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Exact, Format(icAddProspectDateNew.Value, "yyyy-MM-dd HH:mm:ss")))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "VechileType.ID", MatchType.Exact, objVechileType.ID))
            If Not IsNothing(objuser) Then
                criteria.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, objuser.Dealer.ID))
            End If

            arrSAPCustomer = New SAPCustomerFacade(User).Retrieve(criteria)
            If Not IsNothing(arrSAPCustomer) Then
                If arrSAPCustomer.Count > 0 Then
                    MessageBox.Show("Data sudah ada, tidak bisa di masukan lagi !")
                    Return
                End If
            End If


            Dim objSAPCustomer As SAPCustomer = New SAPCustomer
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            If Not IsNothing(objuser) Then
                objSAPCustomer.Dealer = objuser.Dealer
            End If
            objSAPCustomer.SalesmanHeader = objSalesmanHeader
            objSAPCustomer.Qty = Val(txtAddQty.Text)
            objSAPCustomer.VechileType = objVechileType
            objSAPCustomer.CustomerName = txtAddCustomerNameNew.Text
            objSAPCustomer.CustomerAddress = txtAddCustomerAddressNew.Text
            objSAPCustomer.Status = ddlAddStatusNew.SelectedValue
            objSAPCustomer.ProspectDate = icAddProspectDateNew.Value

            Dim result As Integer = facade.Insert(objSAPCustomer)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                Dim ArrCopy As New ArrayList
                ArrCopy.Add(txtAddQty.Text)
                ArrCopy.Add(txtAddCustomerNameNew.Text)
                ArrCopy.Add(txtAddCustomerAddressNew.Text)
                ArrCopy.Add(ddlAddStatusNew.SelectedValue)
                ArrCopy.Add(icAddProspectDateNew.Value)
                sessHelper.SetSession("DataCopy", ArrCopy)
            End If
            ' Next
            'BindDataGrid(0)
            If dgSAPCustomer.Items.Count = 25 And dgSAPCustomer.CurrentPageIndex = (dgSAPCustomer.PageCount - 1) Then
                BindDataGrid(dgSAPCustomer.PageCount)
            Else
                BindDataGrid(dgSAPCustomer.PageCount - 1)
            End If

        End If

        If e.CommandName = "Save" Then
            lblSearchSalesman.Visible = True
            Dim objSAPCustomer As SAPCustomer = New SAPCustomerFacade(User).Retrieve(CInt(e.CommandArgument))

            ' start check validation #########################
            If txtSalesmanID.Text = "" Then
                MessageBox.Show("Salesman ID harus diisi dahulu !")
                Return
            End If

            'remark by ERY
            '--------------
            'Dim arrSAPRegister As ArrayList
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, txtSalesmanID.Text.Trim))
            'arrSAPRegister = New SAPRegisterFacade(User).Retrieve(criterias)
            'If Not IsNothing(arrSAPRegister) Then
            '    If arrSAPRegister.Count < 1 Then
            '        MessageBox.Show("SAP No harus diisi dengan data yang valid, gunakan pop up !")
            '        Return
            '    End If
            'End If

            Dim txtEditQty As TextBox = CType(e.Item.FindControl("txtEditQty"), TextBox)
            Dim txtEditCustomerNameNew As TextBox = CType(e.Item.FindControl("txtEditCustomerName"), TextBox)
            Dim txtEditCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtEditCustomerAddress"), TextBox)
            Dim txtEditVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtEditVechileTypeCode"), TextBox)
            Dim ddlEditStatusNew As DropDownList = CType(e.Item.FindControl("ddlEditStatus"), DropDownList)
            Dim icEditProspectDateNew As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icEditProspectDate"), KTB.DNet.WebCC.IntiCalendar)


            If txtEditCustomerNameNew.Text = "" Then
                MessageBox.Show("Nama Customer harus diisi dahulu !")
                Return
            End If

            If txtEditCustomerAddressNew.Text = "" Then
                MessageBox.Show("Alamat Customer harus diisi dahulu !")
                Return
            End If

            If Val(txtEditQty.Text) = 0 Then
                MessageBox.Show("Qty harus diisi dahulu !")
                Return
            End If


            If txtEditVechileTypeCodeNew.Text = "" Then
                MessageBox.Show("Tipe kendaraan harus diisi dahulu !")
                Return
            Else
                ' cek apakah data valid saat diinput
                Dim arrVechileType As ArrayList
                Dim criteriax As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriax.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtEditVechileTypeCodeNew.Text))
                arrVechileType = New VechileTypeFacade(User).Retrieve(criteriax)
                If Not IsNothing(arrVechileType) Then
                    If arrVechileType.Count < 1 Then
                        MessageBox.Show("Tipe kendaraan harus diisi dengan data yang valid, gunakan pop up !")
                        Return
                    End If
                End If
            End If

            If ddlEditStatusNew.SelectedItem.Text = "" Then
                MessageBox.Show("Silakan memilih status terlebih dahulu !")
                Return
            End If

            Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtEditVechileTypeCodeNew.Text.Trim)
            Dim arrSAPCustomer As ArrayList
            'remarked by anh 20140417 biar gak muncul di modul Marketing>Konsumen
            'Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, 3))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerName", MatchType.Exact, txtEditCustomerNameNew.Text))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerAddress", MatchType.Exact, txtEditCustomerAddressNew.Text))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlEditStatusNew.SelectedValue))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Exact, Format(icEditProspectDateNew.Value, "yyyy-MM-dd HH:mm:ss")))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "VechileType.ID", MatchType.Exact, objVechileType.ID))
            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If Not IsNothing(objuser) Then
                criteria.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, objuser.Dealer.ID))
            End If

            arrSAPCustomer = New SAPCustomerFacade(User).Retrieve(criteria)
            If Not IsNothing(arrSAPCustomer) Then
                If arrSAPCustomer.Count > 0 Then
                    MessageBox.Show("Data sudah ada, tidak bisa di masukan lagi !")
                    Return
                End If
            End If

            ' end check validation #########################


            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            If Not IsNothing(objuser) Then
                objSAPCustomer.Dealer = objuser.Dealer
            End If
            'objSAPCustomer.SAPRegister = objSAPRegister
            objSAPCustomer.qty = Val(txtEditQty.Text)
            objSAPCustomer.VechileType = objVechileType
            objSAPCustomer.CustomerName = txtEditCustomerNameNew.Text
            objSAPCustomer.CustomerAddress = txtEditCustomerAddressNew.Text
            objSAPCustomer.Status = ddlEditStatusNew.SelectedValue
            objSAPCustomer.ProspectDate = icEditProspectDateNew.Value

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
            arlData.Add(lblDateFrom.Text)
            arlData.Add(lblDateUntil.Text)
            arlData.Add(txtSalesmanCode.Value)
            arlData.Add(txtPeriod.Value)
            sessHelper.SetSession("DataForSAPReturn", arlData)

            Response.Redirect("../Customer/FrmCustomerRequest.aspx?isSAP=true&SAPID=" & id)
        End If
    End Sub

    Private Sub dgSAPCustomer_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSAPCustomer.PageIndexChanged
        'dgSAPCustomer.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(e.NewPageIndex)
    End Sub

    Private Sub dgSAPCustomer_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSAPCustomer.SortCommand
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

#Region "Cek Privilege"
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
#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            If Request.QueryString("isBack") <> "1" Then
                Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                lblDealerCode.Text = objuser.Dealer.DealerCode
                lblDealerName.Text = objuser.Dealer.DealerName
                Initialize()
                BindControlsAttribute()
                ' 12-Nov-2007   Deddy H     Fix bug 1430
                'BindDataGrid(0)
                dgSAPCustomer.ShowFooter = True
            Else
                Dim arlDataGet As ArrayList = sessHelper.GetSession("DataForSAPReturn")
                txtSalesmanID.Text = arlDataGet(1)
                txtSalesmanName.Text = arlDataGet(2)
                'lblDateFrom.Text = arlDataGet(3)
                'lblDateUntil.Text = arlDataGet(4)
                txtSalesmanCode.Value = arlDataGet(3)
                txtPeriod.Value = arlDataGet(4)

                Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                lblDealerCode.Text = objuser.Dealer.DealerCode
                lblDealerName.Text = objuser.Dealer.DealerName
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
    End Sub

    Private Sub BindControlsAttribute()
        lblSearchSalesman.Attributes("onclick") = "ShowPopUpSAPRegisterSalesman();"
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()

    End Sub
#Region "Need To Add"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        txtSalesmanID.Text = String.Empty
        txtSalesmanName.Text = String.Empty
        lblDateFrom.Text = String.Empty
        lblDateUntil.Text = String.Empty
        txtSalesmanCode.Value = String.Empty
        txtPeriod.Value = String.Empty

        If dgSAPCustomer.Items.Count > 0 Then
            dgSAPCustomer.SelectedIndex = -1
            dgSAPCustomer.DataSource = New ArrayList
            dgSAPCustomer.DataBind()
        End If
        lblSearchSalesman.Visible = True

    End Sub



    Private Sub Initialize()
        ClearData()
        'ViewState("CurrentSortColumn") = "AreaCode"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'ViewState.Add("vsProcess", "Insert")
    End Sub

    ' ini perlu set security
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar SPL")
        End If

        _create = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)

        'lbtnNew.Visible = _create
        btnSearch.Visible = _view

    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        'remarked by anh 20140417 biar gak muncul di modul Marketing>Konsumen
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, 3))
        criterias.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanID.Text.Trim()))

        If txtSalesmanID.Text = "" Then
            arrList = New ArrayList
        Else
            arrList = _SAPCustomerFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSAPCustomer.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        End If

        dgSAPCustomer.CurrentPageIndex = idxPage
        dgSAPCustomer.DataSource = arrList
        dgSAPCustomer.VirtualItemCount = totalRow
        dgSAPCustomer.DataBind()

    End Sub

    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True
        'If (txtAreaCode.Text = "") Then
        '    blnValid = False
        '    MessageBox.Show("Kode Area harus diinput terlebih dahulu")
        '    Return (blnValid)
        'End If

        Return blnValid
    End Function
#End Region

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lbtnSearchSalesman_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSearchSalesman.Click
        'If txtSalesmanID.Text <> sessHelper.GetSession("CurrentSalesmanHeader") Then
        btnSearch_Click(Me, System.EventArgs.Empty)

        'End If


    End Sub



End Class
