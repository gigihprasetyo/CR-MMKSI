Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General


Imports Microsoft.CSharp
Imports System.CodeDom.Compiler
Imports System.Reflection

Public Class FrmNewInputMasterList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRefBenefit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoSurat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRemark As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpRefBenefit As System.Web.UI.WebControls.Label

    Protected WithEvents formula As System.Web.UI.WebControls.Panel
    Protected WithEvents hfformula As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents btnReload As System.Web.UI.WebControls.Button
    Protected WithEvents hdnBenefitMasterID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents HiddenFieldFormula As System.Web.UI.WebControls.HiddenField




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
    Private objDomain As BenefitMasterHeader = New BenefitMasterHeader
    Private objDomainFacade As BenefitMasterHeaderFacade = New BenefitMasterHeaderFacade(User)

    Private ObjBenefitMasterDealer As New BenefitMasterDealer
    Private ObjBenefitMasterDetail As New BenefitMasterDetail

    Private ObjBenefitMasterDealerList As ArrayList
    Private ObjBenefitMasterDetailList As ArrayList

    Private inputmasterbenefit_privillage As Boolean

    Dim constabjad As String = String.Empty
#Region "Private Property"
    Private Property SesType() As EnumAlertManagement.AlertManagementType
        Get
            Return CType(sessHelper.GetSession("ListAlertMasterType"), EnumAlertManagement.AlertManagementType)
        End Get
        Set(ByVal Value As EnumAlertManagement.AlertManagementType)
            sessHelper.SetSession("ListAlertMasterType", Value)
        End Set
    End Property
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=BENEFIT - List")
        'End If

        inputmasterbenefit_privillage = False

        inputmasterbenefit_privillage = SecurityProvider.Authorize(Context.User, SR.inputmasterbenefit_privillage)

        If Not inputmasterbenefit_privillage Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BENEFIT - Input Benefit")
        End If

    End Sub

    'Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege)
#End Region

    Private noabjad As String = ""

    Enum Status
        Aktif = 0
        Tidak = 1
    End Enum

    Public Sub EnumToListBox(EnumType As Type, TheListBox As ListControl)
        Dim Values As Array = System.Enum.GetValues(EnumType)
        For Each Value As Integer In Values
            Dim Display As String = [Enum].GetName(EnumType, Value)
            Dim Item As ListItem = New ListItem(Display, Value.ToString())
            TheListBox.Items.Add(Item)
        Next
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        InitiateAuthorization()
        If Not IsPostBack Then

            lblPopUpRefBenefit.Attributes("onclick") = "ShowPPRefBenefitSelection();"
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            '  EnumToListBox(GetType(Status), ddlStatus)
            InitializeForm()
        End If
    End Sub

    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "Add"
                AddCommand(e)
            Case "AddCopy"
                AddCopyCommand(e)
            Case "View"
                'Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=View;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
            Case "Edit"
                dgTable_EditCommand(e)
                'Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=Edit;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
            Case "Update"
                UpdateCommand(e)

            Case "Cancel"
                dgTable_CancelCommand(e)
            Case "Delete"
                DeleteCommand(e)
        End Select
    End Sub

    Private Sub dgTable_EditCommand(ByVal e As DataGridCommandEventArgs)

        dgTable.EditItemIndex = CInt(e.Item.ItemIndex)
        'BindDetail(CInt(sessHelper.GetSession("IDBenefitListHeader")))
        dgTable.ShowFooter = False
        ' dgTable.DataBind()
        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

        Dim Formula As Label = CType(e.Item.FindControl("lblNoGrid"), Label)
        'get BenefitMasterVehicleType
        Dim alBenefitMasterDetail As ArrayList = sessHelper.GetSession("DetailSession")
        For Each el As BenefitMasterDetail In alBenefitMasterDetail
            If el.FormulaID = Formula.Text Then
                sessHelper.SetSession("DetailIDSession", el)

                Exit For
            End If
        Next
    End Sub

    Private Sub dgTable_CancelCommand(ByVal E As DataGridCommandEventArgs)
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
            If e.Item.ItemType = ListItemType.Footer Then
                GenerateToFooter(e)
            End If
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
        RemoveALLSession()
        ' Response.Redirect("FrmBenefitList.aspx")

        If Convert.ToString(Request.QueryString("Mode")) = "View" Or
             Convert.ToString(Request.QueryString("Mode")) = "ViewSave" Or
               Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
               Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
            Response.Redirect("FrmBenefitList.aspx")
        Else
            Response.Redirect("FrmNewInputMasterList.aspx")
        End If

    End Sub

    Private Sub RemoveALLSession()
        'sessHelper.RemoveSession("OLDSPLDealer")
        sessHelper.RemoveSession("IDBenefitListHeader")
        sessHelper.RemoveSession("DetailSession")
        sessHelper.RemoveSession("addDetailSession")
        sessHelper.RemoveSession("OldDetailSession")
        sessHelper.RemoveSession("OldDealerSession")
        
    End Sub
    
    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusSPL.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub

    Private Sub GenerateToGrid(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitMasterDetail = CType(e.Item.DataItem, BenefitMasterDetail)
            ' If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then



                    If Convert.ToString(sessHelper.GetSession("Status")) = "View" _
                       Or Convert.ToString(sessHelper.GetSession("Status")) = "Delete" _
                        Or Convert.ToString(sessHelper.GetSession("Status")) = "ViewSave" Then
                        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                        lnkbtnEdit.Visible = False
                        Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
                        lnkbtnDelete.Visible = False
                        'Dim lbtnCopy As LinkButton = CType(e.Item.FindControl("lbtnCopy"), LinkButton)
                        'lbtnCopy.Visible = False
                    End If



                    Dim lblNoGrid As Label = CType(e.Item.FindControl("lblNoGrid"), Label)
                    'lblNoGrid.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString
                    lblNoGrid.Text = objDomain2.FormulaID

                    Dim lblTypeBenefitGrid As Label = CType(e.Item.FindControl("lblTypeBenefitGrid"), Label)
                    lblTypeBenefitGrid.Text = objDomain2.BenefitType.Name








                    Dim alBenefitMasterLeasings As ArrayList = objDomain2.BenefitMasterLeasings
                    Dim idLeasing As String = ""
                    For Each el As BenefitMasterLeasing In alBenefitMasterLeasings
                        idLeasing += el.LeasingCompany.LeasingName.ToString + "; "
                    Next
                    Dim lblLeasingGrid As Label = CType(e.Item.FindControl("lblLeasingGrid"), Label)
                    lblLeasingGrid.Text = idLeasing

                    Dim lblDescriptionGrid As Label = CType(e.Item.FindControl("lblDescriptionGrid"), Label)
                    lblDescriptionGrid.Text = objDomain2.Description

                    Dim lblAmountGrid As Label = CType(e.Item.FindControl("lblAmountGrid"), Label)


                    lblAmountGrid.Text = FormatNumber(objDomain2.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)



                    Dim lblPeriodeGrid As Label = CType(e.Item.FindControl("lblPeriodeGrid"), Label)
                    Dim lblOpenGrid As Label = CType(e.Item.FindControl("lblOpenGrid"), Label)


                    If objDomain2.BenefitType.EventValidation = 1 Then
                        lblOpenGrid.Text = objDomain2.FakturOpenStart.ToString("dd/MM/yyyy") + " s/d " + objDomain2.FakturOpenEnd.ToString("dd/MM/yyyy")
                    Else
                        lblPeriodeGrid.Text = objDomain2.FakturValidationStart.ToString("dd/MM/yyyy") + " s/d " + objDomain2.FakturValidationEnd.ToString("dd/MM/yyyy")

                    End If


                    Dim alBenefitMasterVehicleTypes As ArrayList = objDomain2.BenefitMasterVehicleTypes
                    Dim idModel As String = ""
                    Dim idType As String = ""
                    For Each el As BenefitMasterVehicleType In alBenefitMasterVehicleTypes
                        If Not el.VechileType.VechileModel Is Nothing Then
                            'If idModel.Contains(el.VechileType.VechileModel.Description) = False Then
                            '    idModel += el.VechileType.VechileModel.Description
                            'End If

                            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, el.VechileType.VechileModel.ID))
                            criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "SubCategoryVehicle.Category.ProductCategory.Code", MatchType.Exact, companyCode))

                            Dim arrSubCatVehicleToModel As ArrayList
                            arrSubCatVehicleToModel = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                            If Not IsNothing(arrSubCatVehicleToModel) AndAlso arrSubCatVehicleToModel.Count > 0 Then
                                Dim oSubCategoryVehicleToModel As SubCategoryVehicleToModel = CType(arrSubCatVehicleToModel(0), SubCategoryVehicleToModel)
                                If idModel.Contains(oSubCategoryVehicleToModel.SubCategoryVehicle.Name) = False Then
                                    idModel += oSubCategoryVehicleToModel.SubCategoryVehicle.Name + " ;"
                                End If
                            End If
                        End If

                        'idType += el.VechileType.Description + " ; "
                        idType += el.VechileType.VechileTypeCode + " ; "
                    Next
                    Dim lblModelGrid As Label = CType(e.Item.FindControl("lblModelGrid"), Label)
                    lblModelGrid.Text = idModel
                    Dim lblTypeGrid As Label = CType(e.Item.FindControl("lblTypeGrid"), Label)
                    lblTypeGrid.Text = idType

                    Dim lblAssyGrid As Label = CType(e.Item.FindControl("lblAssyGrid"), Label)
                    lblAssyGrid.Text = objDomain2.AssyYear.ToString

                    Dim lblMaxGrid As Label = CType(e.Item.FindControl("lblMaxGrid"), Label)
                    lblMaxGrid.Text = objDomain2.MaxClaim.ToString





                    Dim lblDiskonGrid As Label = CType(e.Item.FindControl("lblDiskonGrid"), Label)
                    If objDomain2.BenefitType.WSDiscount = 1 Then
                        Dim cbDiskonGrid As CheckBox = New CheckBox()
                        cbDiskonGrid.Enabled = False
                        If objDomain2.WSDiscount = 1 Then
                            cbDiskonGrid.Checked = True
                        Else
                            cbDiskonGrid.Checked = False
                        End If
                        lblDiskonGrid.Controls.Add(cbDiskonGrid)
                    End If




                Else
                    Dim objBenefitTypeFacade As BenefitTypeFacade = New BenefitTypeFacade(User)
                    Dim objVechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
                    Dim templeasingcode As String = ""
                    Dim templeasingname As String = ""


                    '   Dim lblTypeBenefitEditGrid As Label = CType(e.Item.FindControl("lblTypeBenefitEditGrid"), Label)
                    '   lblTypeBenefitEditGrid.Text = objDomain2.BenefitType.Name
                    Dim ddlTypeBenefitEditGrid As DropDownList = CType(e.Item.FindControl("ddlTypeBenefitEditGrid"), DropDownList)
                    Dim li As ListItem
                    For Each item As BenefitType In objBenefitTypeFacade.RetrieveActiveList()
                        li = New ListItem(item.Name, item.ID.ToString & ";" & item.WSDiscount & ";" & item.EventValidation & ";" & item.LeasingBox)
                        ddlTypeBenefitEditGrid.Items.Add(li)
                    Next
                    ddlTypeBenefitEditGrid.Attributes("onchange") = "showhideLeasingEdit();"
                    ddlTypeBenefitEditGrid.SelectedValue = objDomain2.BenefitType.ID & ";" & objDomain2.BenefitType.WSDiscount & ";" & objDomain2.BenefitType.EventValidation _
                        & ";" & objDomain2.BenefitType.LeasingBox



                    Dim lblNoEditGrid As Label = CType(e.Item.FindControl("lblNoEditGrid"), Label)
                    lblNoEditGrid.Text = objDomain2.FormulaID

                    Dim txtDescriptionEditGrid As TextBox = CType(e.Item.FindControl("txtDescriptionEditGrid"), TextBox)
                    txtDescriptionEditGrid.Text = objDomain2.Description

                    Dim txtAmountEditGrid As TextBox = CType(e.Item.FindControl("txtAmountEditGrid"), TextBox)
                    'txtAmountEditGrid.Text = objDomain2.Amount
                    'txtAmountEditGrid.Text = String.Format("{0:#.##0,00}", objDomain2.Amount)
                    'txtAmountEditGrid.Text = objDomain2.Amount.ToString("#,##0.00")
                    'txtAmountEditGrid.Text = objDomain2.Amount.ToString("#,##0.00").Replace(",", "-").Replace(".", ",").Replace("-", ".")
                    txtAmountEditGrid.Text = FormatNumber(objDomain2.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)


                    Dim clDariEditGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clDariEditGrid"), KTB.DNet.WebCC.IntiCalendar)
                    clDariEditGrid.Value = objDomain2.FakturValidationStart

                    Dim clSampaiEditGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clSampaiEditGrid"), KTB.DNet.WebCC.IntiCalendar)
                    clSampaiEditGrid.Value = objDomain2.FakturValidationEnd


                    Dim clDariOpenEditGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clDariOpenEditGrid"), KTB.DNet.WebCC.IntiCalendar)
                    clDariOpenEditGrid.Value = objDomain2.FakturOpenStart

                    Dim clSampaiOpenEditGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clSampaiOpenEditGrid"), KTB.DNet.WebCC.IntiCalendar)
                    clSampaiOpenEditGrid.Value = objDomain2.FakturOpenEnd

                    Dim txtAssyEditGrid As TextBox = CType(e.Item.FindControl("txtAssyEditGrid"), TextBox)
                    txtAssyEditGrid.Text = objDomain2.AssyYear

                    Dim txtMaxEditGrid As TextBox = CType(e.Item.FindControl("txtMaxEditGrid"), TextBox)
                    txtMaxEditGrid.Text = objDomain2.MaxClaim





                    templeasingcode = ""
                    templeasingname = ""

                    'Dim lblTypeEditGrid As Label = CType(e.Item.FindControl("lblTypeEditGrid"), Label)
                    'Dim lblModelEditGrid As Label = CType(e.Item.FindControl("lblModelEditGrid"), Label)
                    'For Each item As BenefitMasterVehicleType In objDomain2.BenefitMasterVehicleTypes
                    '    lblModelEditGrid.Text = item.VechileType.VechileModel.Description
                    '    templeasingcode += item.VechileType.VechileTypeCode.ToString + ";"
                    '    'templeasingname += item.VechileType.VechileCodeDesc + ";"
                    '    templeasingname += item.VechileType.VechileTypeCode.ToString + ";"
                    'Next
                    'lblTypeEditGrid.Text = templeasingname
                    Dim ddlModelEditGrid As DropDownList = CType(e.Item.FindControl("ddlModelEditGrid"), DropDownList)
                    BindVehicleSubCategoryToDDL(ddlModelEditGrid)

                    'Dim li1 As ListItem
                    'Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    'For Each item As VechileModel In objVechileModelFacade.RetrieveActiveList()
                    '    If item.Category.ProductCategory.Code.Trim = companyCode Then
                    '        li1 = New ListItem(item.Description, item.ID.ToString)
                    '        ddlModelEditGrid.Items.Add(li1)
                    '    End If
                    'Next

                    Dim shrVechileModelID As Short = 0
                    For Each item As BenefitMasterVehicleType In objDomain2.BenefitMasterVehicleTypes
                        shrVechileModelID = item.VechileType.VechileModel.ID
                        templeasingcode += item.VechileType.VechileTypeCode.ToString + ";"
                        'templeasingname += item.VechileType.VechileCodeDesc + ";"
                        templeasingname += item.VechileType.VechileTypeCode.ToString + ";"
                    Next
                    ddlModelEditGrid.SelectedValue = GetVehicleSubCategoryValue(shrVechileModelID)

                    Dim txtTypeEditGrid As TextBox = CType(e.Item.FindControl("txtTypeEditGrid"), TextBox)
                    txtTypeEditGrid.Text = templeasingname
                    Dim hfTypeEditGrid As TextBox = CType(e.Item.FindControl("hfTypeEditGrid"), TextBox)
                    hfTypeEditGrid.Text = templeasingcode
                    ddlModelEditGrid.Attributes("onchange") = "showhideModelTypeEdit();"

                    Dim lblTypeEditGrid As Label = CType(e.Item.FindControl("lblTypeEditGrid"), Label)
                    lblTypeEditGrid.Attributes("onclick") = "ShowPPTypeSelectionEdit();"


                    templeasingcode = ""
                    templeasingname = ""


                    For Each item As BenefitMasterLeasing In objDomain2.BenefitMasterLeasings
                        templeasingcode += item.LeasingCompany.ID.ToString + ";"
                        templeasingname += item.LeasingCompany.LeasingName + ";"
                    Next

                    'Dim lblLeasingEditGrid As Label = CType(e.Item.FindControl("lblLeasingEditGrid"), Label)
                    ' lblLeasingEditGrid.Text = templeasingname
                    Dim txtLeasingEditGrid As TextBox = CType(e.Item.FindControl("txtLeasingEditGrid"), TextBox)
                    txtLeasingEditGrid.Text = templeasingname
                    Dim hfLeasingEditGrid As TextBox = CType(e.Item.FindControl("hfLeasingEditGrid"), TextBox)
                    hfLeasingEditGrid.Text = templeasingcode

                    Dim lblLeasingEditGrid As Label = CType(e.Item.FindControl("lblLeasingEditGrid"), Label)
                    lblLeasingEditGrid.Attributes("onclick") = "ShowPPLeasingSelectionEdit();"

                    Dim arealeasing1 As Panel = CType(e.Item.FindControl("arealeasing1"), Panel)

                    Dim valueddlTypeBenefitGrid() As String = ddlTypeBenefitEditGrid.SelectedValue.Replace(" ", "").Split(";")

                    If valueddlTypeBenefitGrid(3) = "1" Then
                        arealeasing1.Attributes("style") = "display:;"
                    Else
                        arealeasing1.Attributes("style") = "display:none;"
                    End If

                    'Dim lblTypeGrid As Label = CType(e.Item.FindControl("lblTypeGrid"), Label)
                    'lblTypeGrid.Attributes("onclick") = "ShowPPTypeSelection();"


                    Dim cbDiskonEditGrid As CheckBox = CType(e.Item.FindControl("cbDiskonEditGrid"), CheckBox)
                    If objDomain2.WSDiscount = 1 Then
                        cbDiskonEditGrid.Checked = True
                    Else
                        cbDiskonEditGrid.Checked = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub GenerateToFooter(ByVal e As DataGridItemEventArgs)

        Dim lblLeasingGrid As Label = CType(e.Item.FindControl("lblLeasingGrid"), Label)
        lblLeasingGrid.Attributes("onclick") = "ShowPPLeasingSelection();"

        Dim lblTypeGrid As Label = CType(e.Item.FindControl("lblTypeGrid"), Label)
        lblTypeGrid.Attributes("onclick") = "ShowPPTypeSelection();"


        Dim objBenefitTypeFacade As BenefitTypeFacade = New BenefitTypeFacade(User)
        Dim objVechileModelFacade As VechileModelFacade = New VechileModelFacade(User)


        'Dim lblFooterKodeModel As Label = CType(e.Item.FindControl("lblFooterKodeModel"), Label)
        'lblFooterKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"

        'Dim lblFooterTop As Label = CType(e.Item.FindControl("lblFooterTop"), Label)
        'lblFooterTop.Attributes("onclick") = "ShowPPFasilitasTOPSelection();"

        Dim ddlTypeBenefitGrid As DropDownList = CType(e.Item.FindControl("ddlTypeBenefitGrid"), DropDownList)
        Dim li As ListItem
        For Each item As BenefitType In objBenefitTypeFacade.RetrieveActiveList()
            li = New ListItem(item.Name, item.ID.ToString & ";" & item.WSDiscount & ";" & item.EventValidation & ";" & item.LeasingBox)
            ddlTypeBenefitGrid.Items.Add(li)
        Next
        ddlTypeBenefitGrid.Attributes("onchange") = "showhideLeasing();"

        Dim ddlModelGrid As DropDownList = CType(e.Item.FindControl("ddlModelGrid"), DropDownList)
        BindVehicleSubCategoryToDDL(ddlModelGrid)

        'Dim li1 As ListItem
        'Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        'For Each item As VechileModel In objVechileModelFacade.RetrieveActiveList()
        '    If item.Category.ProductCategory.Code.Trim = companyCode Then
        '        li1 = New ListItem(item.Description, item.ID.ToString)
        '        ddlModelGrid.Items.Add(li1)
        '    End If
        'Next

        ddlModelGrid.Attributes("onchange") = "showhideModelType();"

        Dim ddlNoGrid As DropDownList = CType(e.Item.FindControl("ddlNoGrid"), DropDownList)
        constabjad = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        constabjad = New AppConfigFacade(User).Retrieve("AlphabeticalFormula").Value
        Dim arrConstAbjad As String() = constabjad.Split(";")
        For Each item As String In arrConstAbjad
            If noabjad.Contains(item) = False Then
                ddlNoGrid.Items.Add(item)
            End If
        Next
        dgTable.PageSize = arrConstAbjad.Length

      
    End Sub

    Private Sub BindVehicleSubCategoryToDDL(ByRef ddlSubCategory As DropDownList)
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        ddlSubCategory.Items.Clear()

        '-- SubCategoryVehicle criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ProductCategory.Code", MatchType.Exact, companyCode))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SubCategoryVehicle), "Name", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

        '-- Bind ddlSubCategory dropdownlist
        ddlSubCategory.DataSource = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
        ddlSubCategory.DataTextField = "Name"
        ddlSubCategory.DataValueField = "ID"
        ddlSubCategory.DataBind()

        ddlSubCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub

    Private Function GetVehicleSubCategoryValue(ByVal vehicleModelId As Short) As Short
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim subCategoryVehicleId As Short = 0

        '-- SubCategoryVehicle criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, vehicleModelId))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "SubCategoryVehicle.Category.ProductCategory.Code", MatchType.Exact, companyCode))

        Dim arrSubCatVehicleToModel As ArrayList
        arrSubCatVehicleToModel = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
        If Not IsNothing(arrSubCatVehicleToModel) AndAlso arrSubCatVehicleToModel.Count > 0 Then
            Dim oSubCategoryVehicleToModel As SubCategoryVehicleToModel = CType(arrSubCatVehicleToModel(0), SubCategoryVehicleToModel)
            subCategoryVehicleId = oSubCategoryVehicleToModel.SubCategoryVehicle.ID
        End If

        Return subCategoryVehicleId
    End Function

    Private Sub GetValueFromDataBase(ByVal id As Integer)
        Dim Obj As BenefitMasterHeader = objDomainFacade.Retrieve(id)

        If Not Obj Is Nothing Then
            txtNoSurat.Text = Obj.NomorSurat
            txtRegNo.Text = Obj.BenefitRegNo
            txtRemark.Text = Obj.Remarks
            ddlStatus.SelectedValue = Obj.Status.ToString

            Dim alBenefitMasterDealers As ArrayList = Obj.BenefitMasterDealers
            sessHelper.SetSession("alBenefitMasterDealers", alBenefitMasterDealers)
            Dim idDealer As String = ""
            For Each el As BenefitMasterDealer In alBenefitMasterDealers
                idDealer += el.Dealer.DealerCode + "; "
            Next
            txtKodeDealer.Text = idDealer
            hfformula.Value = addDelimiter(Obj.Formula)
        Else
            Obj = New BenefitMasterHeader
        End If

        Dim list As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        If list Is Nothing Then list = New ArrayList

        If list Is Nothing OrElse list.Count = 0 Then
            If Convert.ToString(Request.QueryString("Mode")) = "View" Or
                Convert.ToString(Request.QueryString("Mode")) = "ViewSave" Or
                Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
                Convert.ToString(Request.QueryString("Mode")) = "Delete" Or
                txtRefBenefit.Text.Trim <> "" Then

                list = Obj.BenefitMasterDetails

                If sessHelper.GetSession("OldDetailSession") Is Nothing Then
                    sessHelper.SetSession("OldDetailSession", list)
                End If

                If sessHelper.GetSession("OldDealerSession") Is Nothing Then
                    sessHelper.SetSession("OldDealerSession", Obj.BenefitMasterDealers)
                End If

                Dim tempnoabjad = ""
                For Each el As BenefitMasterDetail In list
                    tempnoabjad += el.FormulaID + ";"
                Next

                noabjad = tempnoabjad

                If list Is Nothing Then
                    list = New ArrayList
                End If
                ' ElseIf Convert.ToString(Request.QueryString("Mode")) = "Insert" Then
            Else
                list = New ArrayList
            End If
        Else

            Dim tempnoabjad = ""
            For Each el As BenefitMasterDetail In list
                tempnoabjad += el.FormulaID + ";"
            Next
            noabjad = tempnoabjad
        End If

        Dim list1 As BenefitMasterDetail = CType(sessHelper.GetSession("addDetailSession"), BenefitMasterDetail)

        If Not list1 Is Nothing Then
            noabjad += list1.FormulaID
            list.Add(list1)
        End If

        sessHelper.SetSession("DetailSession", list)
        sessHelper.RemoveSession("addDetailSession")
        constabjad = New AppConfigFacade(User).Retrieve("AlphabeticalFormula").Value
        Dim arrConstAbjad As String() = constabjad.Split(";")
        dgTable.PageSize = arrConstAbjad.Length
        dgTable.DataSource = list
        dgTable.DataBind()
    End Sub

    Private Function addDelimiter(ByVal formula As String) As String
        Dim result As String = String.Empty
        For i As Integer = 0 To formula.Length - 1
            If Char.IsLetter(formula(i)) And i < formula.Length - 1 Then
                If Char.IsLetter(formula(i + 1)) Then
                    result = result & formula(i) & formula(i + 1) & ";"
                    i = i + 1
                Else
                    result = result & formula(i) & ";"
                End If
            Else
                result = result & formula(i) & ";"
            End If
        Next

        Return result
    End Function

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs)
        Dim lblNoGrid As Label = CType(e.Item.FindControl("lblNoGrid"), Label)
        Dim arrFormula As String() = hfformula.Value.Split(";")
        For Each f As String In arrFormula
            If f = lblNoGrid.Text.Trim Then
                MessageBox.Show("Hapus terlebih dahulu formula.")
                Return
            End If
        Next
        'If hfformula.Value.Contains(lblNoGrid.Text) Then
        '    MessageBox.Show("Hapus terlebih dahulu formula.")
        '    Return
        'End If
        Dim lblDescriptionGrid As Label = CType(e.Item.FindControl("lblDescriptionGrid"), Label)
        Dim lblAssyGrid As Label = CType(e.Item.FindControl("lblAssyGrid"), Label)
        'Dim objDomain2 As BenefitMasterDetail = CType(e.Item.DataItem, BenefitMasterDetail)
        'Delete item yang index nya itu sesuai dengan index item yg di filter
        Dim list As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        Dim _DetailDelete As New BenefitMasterDetail
        For Each item As BenefitMasterDetail In list
            If lblNoGrid.Text.Replace(" ", "") = item.FormulaID.Replace(" ", "") _
                And lblDescriptionGrid.Text.Replace(" ", "") = item.Description.Replace(" ", "") _
                And lblAssyGrid.Text.Replace(" ", "") = item.AssyYear.ToString.Replace(" ", "") Then
                _DetailDelete = item
            End If
        Next

        Dim _arrList1 As New ArrayList
        Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias1.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitMasterDetail", MatchType.Exact, CInt(_DetailDelete.ID)))
        _arrList1 = New BenefitClaimDetailsFacade(User).Retrieve(criterias1)
        If _arrList1.Count > 0 Then
            MessageBox.Show("Benefit detail masih digunakan di modul lain (Claim).")
            Return
        End If


        list.Remove(_DetailDelete)
        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
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


            Dim ddlTypeBenefitEditGrid As DropDownList = e.Item.FindControl("ddlTypeBenefitEditGrid")

            Dim txtDescriptionEditGrid As TextBox = e.Item.FindControl("txtDescriptionEditGrid")
            Dim txtAmountEditGrid As TextBox = e.Item.FindControl("txtAmountEditGrid")
            Dim clDariEditGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clDariEditGrid"), KTB.DNet.WebCC.IntiCalendar)
            Dim clSampaiEditGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clSampaiEditGrid"), KTB.DNet.WebCC.IntiCalendar)

            Dim clDariOpenEditGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clDariOpenEditGrid"), KTB.DNet.WebCC.IntiCalendar)
            Dim clSampaiOpenEditGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clSampaiOpenEditGrid"), KTB.DNet.WebCC.IntiCalendar)

            Dim txtAssyEditGrid As TextBox = e.Item.FindControl("txtAssyEditGrid")
            Dim txtMaxEditGrid As TextBox = e.Item.FindControl("txtMaxEditGrid")


            Dim hfLeasingEditGrid As TextBox = e.Item.FindControl("hfLeasingEditGrid")
            Dim hfTypeEditGrid As TextBox = e.Item.FindControl("hfTypeEditGrid")

            Dim cbDiskonEditGrid As CheckBox = e.Item.FindControl("cbDiskonEditGrid")


            If ddlTypeBenefitEditGrid.SelectedItem.Text.ToLower.Contains("gift") = True Then
                If txtDescriptionEditGrid.Text = "" Then
                    MessageBox.Show("Isi deskripsi untuk tipe benefit Gift")
                    Return
                End If
            End If

            Dim valueddlTypeBenefitGrid() As String = ddlTypeBenefitEditGrid.SelectedValue.Replace(" ", "").Split(";")

            Dim objBenefitTypeFacade As BenefitTypeFacade = New BenefitTypeFacade(User)
            Dim objBenefitType As BenefitType = objBenefitTypeFacade.Retrieve(CShort(valueddlTypeBenefitGrid(0)))


            If valueddlTypeBenefitGrid(3) = "1" And _
         hfLeasingEditGrid.Text.Replace(" ", "") = "" Then
                MessageBox.Show("Isi Leasing Company")
                Return
            End If


            If txtAmountEditGrid.Text = "" Then
                MessageBox.Show("Isi Amount")
                Return
            End If
            If hfTypeEditGrid.Text.Replace(" ", "") = "" Then
                MessageBox.Show("Isi Tipe")
                Return
            End If

            If objBenefitType.AssyYearBox = 1 Then
                If txtAssyEditGrid.Text = "" Then
                    MessageBox.Show("Isi Assy Year")
                    Return
                End If

                Dim tahun As Integer = DateTime.Now.Year
                If CInt(txtAssyEditGrid.Text) < (tahun - 10) Or CInt(txtAssyEditGrid.Text) > (tahun + 10) Then
                    MessageBox.Show("Assy Year diantara tahun sekarang - 10 dan tahun sekarang + 10")
                    Return
                End If
            End If





            If txtMaxEditGrid.Text.Replace(" ", "") = "" Then
                MessageBox.Show("Isi Max Pengajuan")
                Return
            End If


            If clDariEditGrid.Value > clSampaiEditGrid.Value Then
                MessageBox.Show("Periode harus valid")
                Return
            End If

            If clDariOpenEditGrid.Value > clSampaiOpenEditGrid.Value Then
                MessageBox.Show("Open harus valid")
                Return
            End If




            Dim listAll As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
            ObjBenefitMasterDetail = New BenefitMasterDetail

            ObjBenefitMasterDetail = CType(listAll(e.Item.ItemIndex), BenefitMasterDetail)
            For Each item As BenefitMasterDetail In listAll

                If Not ObjBenefitMasterDetail.ID = item.ID Then

                    Dim temp As String = ""
                    Dim tempLaesing As String = ""
                    For Each item1 As BenefitMasterVehicleType In item.BenefitMasterVehicleTypes
                        temp += item1.VechileType.VechileTypeCode.Trim + ";"
                    Next
                    For Each item1 As BenefitMasterLeasing In item.BenefitMasterLeasings
                        tempLaesing += item1.LeasingCompany.ID.ToString + ";"
                    Next


                    Dim tempAssy As Boolean = False
                    Dim tempEventValidation As Boolean = False

                    'If item.BenefitType.ID = CShort(valueddlTypeBenefitGrid(0)) And _
                    '((clDariEditGrid.Value >= item.FakturValidationStart And _
                    '   clDariEditGrid.Value <= item.FakturValidationEnd) Or _
                    '(clSampaiEditGrid.Value >= item.FakturValidationStart And _
                    '    clSampaiEditGrid.Value <= item.FakturValidationEnd)) And _
                    '((clDariOpenEditGrid.Value >= item.FakturOpenStart And _
                    '   clDariOpenEditGrid.Value <= item.FakturOpenEnd) Or _
                    '(clSampaiOpenEditGrid.Value >= item.FakturOpenStart And _
                    '    clSampaiOpenEditGrid.Value <= item.FakturOpenEnd)) Then
                    If item.BenefitType.ID = CShort(valueddlTypeBenefitGrid(0)) Then


                        If objBenefitType.EventValidation = 1 Then
                            If ((clDariOpenEditGrid.Value >= item.FakturOpenStart And _
                                  clDariOpenEditGrid.Value <= item.FakturOpenEnd) Or _
                               (clSampaiOpenEditGrid.Value >= item.FakturOpenStart And _
                                   clSampaiOpenEditGrid.Value <= item.FakturOpenEnd)) Then
                                tempEventValidation = True
                            End If
                        Else
                            If ((clDariEditGrid.Value >= item.FakturValidationStart And _
                                  clDariEditGrid.Value <= item.FakturValidationEnd) Or _
                               (clSampaiEditGrid.Value >= item.FakturValidationStart And _
                                   clSampaiEditGrid.Value <= item.FakturValidationEnd)) Then
                                tempEventValidation = True
                            End If
                        End If


                        If objBenefitType.AssyYearBox = 1 Then
                            If item.AssyYear = CInt(txtAssyEditGrid.Text) Then
                                tempAssy = True
                            End If
                        Else
                            tempAssy = True
                        End If

                        If tempEventValidation = True And tempAssy = True And checkTipe(temp, hfTypeEditGrid.Text) = True And checkTipe(tempLaesing, hfLeasingEditGrid.Text) = True Then
                            MessageBox.Show("Data tidak boleh sama dengan yang sudah ada atau periode beririsan. (tipe benefit, Assy Year, Periode dan tipe)")
                            Return
                        End If
                    End If
                End If
            Next


            If txtAmountEditGrid.Text.Trim <> "" And txtAssyEditGrid.Text.Trim <> "" And _
                txtMaxEditGrid.Text.Trim <> "" Then



                Dim ObjBenefitMasterDetailTemp As BenefitMasterDetail = New BenefitMasterDetail

                'ObjBenefitMasterDetailTemp.ID = ObjBenefitMasterDetail.ID
                ObjBenefitMasterDetailTemp.FormulaID = ObjBenefitMasterDetail.FormulaID
                'ObjBenefitMasterDetailTemp.Amount = txtAmountEditGrid.Text.Replace(",", "").Replace(",", ".")
                ObjBenefitMasterDetailTemp.Amount = txtAmountEditGrid.Text.Replace(".", "").Replace(",", ".")
                ObjBenefitMasterDetailTemp.Description = txtDescriptionEditGrid.Text
                If Not txtAssyEditGrid.Text = "" Then
                    ObjBenefitMasterDetailTemp.AssyYear = txtAssyEditGrid.Text
                End If

                ObjBenefitMasterDetailTemp.MaxClaim = txtMaxEditGrid.Text


                ObjBenefitMasterDetailTemp.BenefitType = objBenefitType


                If objBenefitType.EventValidation = 1 Then
                    ObjBenefitMasterDetailTemp.FakturOpenStart = clDariOpenEditGrid.Value
                    ObjBenefitMasterDetailTemp.FakturOpenEnd = clSampaiOpenEditGrid.Value
                Else
                    ObjBenefitMasterDetailTemp.FakturValidationStart = clDariEditGrid.Value
                    ObjBenefitMasterDetailTemp.FakturValidationEnd = clSampaiEditGrid.Value
                End If

                'ObjBenefitMasterDetailTemp.FakturValidationStart = clDariEditGrid.Value
                'ObjBenefitMasterDetailTemp.FakturValidationEnd = clSampaiEditGrid.Value

                'ObjBenefitMasterDetailTemp.FakturOpenStart = clDariOpenEditGrid.Value
                'ObjBenefitMasterDetailTemp.FakturOpenEnd = clSampaiOpenEditGrid.Value

                ObjBenefitMasterDetailTemp.WSDiscount = -1
                If cbDiskonEditGrid.Checked = True Then
                    ObjBenefitMasterDetailTemp.WSDiscount = 1
                ElseIf cbDiskonEditGrid.Checked = False Then
                    ObjBenefitMasterDetailTemp.WSDiscount = 0
                End If

                Dim i As Integer = 0


                If Not valueddlTypeBenefitGrid(3) = "1" Then
                    hfLeasingEditGrid.Text = ""
                End If


                For Each item As String In hfLeasingEditGrid.Text.Replace(" ", "").Split(";")
                    If Not item Is Nothing And Not item = "" Then




                        Dim objLeasingCompanyFacade As LeasingCompanyFacade = New LeasingCompanyFacade(User)
                        Dim objLeasingCompany As LeasingCompany = objLeasingCompanyFacade.Retrieve(CShort(item))
                        If Not objLeasingCompany Is Nothing Then
                            Dim objBenefitMasterLeasing As BenefitMasterLeasing = New BenefitMasterLeasing
                            objBenefitMasterLeasing.LeasingCompany = objLeasingCompany
                            ObjBenefitMasterDetailTemp.BenefitMasterLeasings.Add(objBenefitMasterLeasing)
                        End If



                    End If
                    i = i + 1
                Next



                i = 0
                For Each item As String In hfTypeEditGrid.Text.Replace(" ", "").Split(";")
                    If Not item Is Nothing And Not item = "" Then

                        Dim objVechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
                        Dim objVechileType As VechileType = objVechileTypeFacade.Retrieve(item)
                        If Not objVechileType Is Nothing Then
                            Dim objBenefitMasterVehicleType As BenefitMasterVehicleType = New BenefitMasterVehicleType
                            objBenefitMasterVehicleType.VechileType = objVechileType
                            ObjBenefitMasterDetailTemp.BenefitMasterVehicleTypes.Add(objBenefitMasterVehicleType)
                        End If



                    End If
                    i = i + 1
                Next


                If Not listAll Is Nothing Then
                    listAll.RemoveAt(e.Item.ItemIndex)
                    listAll.Insert(e.Item.ItemIndex, ObjBenefitMasterDetailTemp)
                End If

                sessHelper.SetSession("DetailSession", listAll)

                dgTable.EditItemIndex = -1
                GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
                dgTable.ShowFooter = True
                dgTable.SelectedIndex = e.Item.ItemIndex

            Else
                MessageBox.Show("Isi detail dengan lengkap")
            End If
        End If

    End Sub

    Private Function checkTipe(ByVal exist As String, ByVal input As String) As Boolean
        Dim temp As Boolean = False
        For Each item As String In exist.Replace(" ", "").Split(";")
            If Not item = "" And Not item Is Nothing Then
                For Each item1 As String In input.Replace(" ", "").Split(";")
                    If Not item1 = "" And Not item1 Is Nothing Then
                        If item = item1 Then
                            Return True
                        End If
                    End If

                Next
            End If

        Next
        Return False
    End Function

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If

        Dim ddlTypeBenefitGrid As DropDownList = CType(e.Item.FindControl("ddlTypeBenefitGrid"), DropDownList)
        Dim ddlNoGrid As DropDownList = CType(e.Item.FindControl("ddlNoGrid"), DropDownList)

        'Dim hfLeasingGrid As HiddenField = CType(e.Item.FindControl("hfLeasingGrid"), HiddenField)
        Dim hfLeasingGrid As TextBox = e.Item.FindControl("hfLeasingGrid")
        Dim txtDescriptionGrid As TextBox = e.Item.FindControl("txtDescriptionGrid")
        Dim txtAmountGrid As TextBox = e.Item.FindControl("txtAmountGrid")
        Dim clDariGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clDariGrid"), KTB.DNet.WebCC.IntiCalendar)
        Dim clSampaiGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clSampaiGrid"), KTB.DNet.WebCC.IntiCalendar)

        Dim clDariOpenGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clDariOpenGrid"), KTB.DNet.WebCC.IntiCalendar)
        Dim clSampaiOpenGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("clSampaiOpenGrid"), KTB.DNet.WebCC.IntiCalendar)

        'Dim hfTypeGrid As HiddenField = CType(e.Item.FindControl("hfTypeGrid"), HiddenField)
        Dim hfTypeGrid As TextBox = e.Item.FindControl("hfTypeGrid")
        Dim txtAssyGrid As TextBox = e.Item.FindControl("txtAssyGrid")
        Dim txtMaxGrid As TextBox = e.Item.FindControl("txtMaxGrid")

        Dim cbDiskonGrid As CheckBox = e.Item.FindControl("cbDiskonGrid")

        If ddlTypeBenefitGrid.SelectedItem.Text.ToLower.Contains("gift") = True Then
            If txtDescriptionGrid.Text = "" Then
                MessageBox.Show("Isi deskripsi untuk tipe benefit Gift")
                Return
            End If
        End If


        Dim valueddlTypeBenefitGrid() As String = ddlTypeBenefitGrid.SelectedValue.Replace(" ", "").Split(";")

        Dim objBenefitTypeFacade As BenefitTypeFacade = New BenefitTypeFacade(User)
        Dim objBenefitType As BenefitType = objBenefitTypeFacade.Retrieve(CShort(valueddlTypeBenefitGrid(0)))

        '  If ddlTypeBenefitGrid.SelectedItem.Text.ToLower.Contains("leas") = True And _
        If valueddlTypeBenefitGrid(3) = "1" And _
            hfLeasingGrid.Text.Replace(" ", "") = "" Then
            MessageBox.Show("Isi Leasing Company")
            Return
        End If

        If ddlNoGrid.SelectedValue = "" Then
            MessageBox.Show("Pilih No")
            Return
        End If
        If txtAmountGrid.Text = "" Then
            MessageBox.Show("Isi Amount")
            Return
        End If
        If hfTypeGrid.Text.Replace(" ", "") = "" Then
            MessageBox.Show("Isi Tipe")
            Return
        End If

        If objBenefitType.AssyYearBox = 1 Then
            If txtAssyGrid.Text = "" Then
                MessageBox.Show("Isi Assy Year")
                Return
            End If

            Dim tahun As Integer = DateTime.Now.Year
            If CInt(txtAssyGrid.Text) < (tahun - 10) Or CInt(txtAssyGrid.Text) > (tahun + 10) Then
                MessageBox.Show("Assy Year diantara tahun sekarang - 10 dan tahun sekarang + 10")
                Return
            End If
        End If

        If txtKodeDealer.Text.Trim() = "" Then
            MessageBox.Show("Isi Dealer")
            Return
        End If


        If txtMaxGrid.Text.Replace(" ", "") = "" Then
            MessageBox.Show("Isi Max Pengajuan")
            Return
        End If

        If clDariGrid.Value > clSampaiGrid.Value Then
            MessageBox.Show("Periode harus valid")
            Return
        End If

        If Not valueddlTypeBenefitGrid(3) = "1" Then
            hfLeasingGrid.Text = ""
        End If



        If Not sessHelper.GetSession("DetailSession") Is Nothing Then


            Dim listAll As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)

            For Each item As BenefitMasterDetail In listAll
                Dim temp As String = ""
                Dim tempLaesing As String = ""
                For Each item1 As BenefitMasterVehicleType In item.BenefitMasterVehicleTypes
                    temp += item1.VechileType.VechileTypeCode.Trim + ";"
                Next
                For Each item1 As BenefitMasterLeasing In item.BenefitMasterLeasings
                    tempLaesing += item1.LeasingCompany.ID.ToString + ";"
                Next


                Dim tempAsst As Boolean = False
                Dim tempEventValidation As Boolean = False

                'If item.BenefitType.ID = CShort(valueddlTypeBenefitGrid(0)) And _
                '  ((clDariGrid.Value >= item.FakturValidationStart And _
                '   clDariGrid.Value <= item.FakturValidationEnd) Or _
                '(clSampaiGrid.Value >= item.FakturValidationStart And _
                '    clSampaiGrid.Value <= item.FakturValidationEnd)) Then
                If item.BenefitType.ID = CShort(valueddlTypeBenefitGrid(0)) Then

                    If objBenefitType.EventValidation = 1 Then
                        If ((clDariGrid.Value >= item.FakturOpenStart And _
                             clDariGrid.Value <= item.FakturOpenEnd) Or _
                          (clSampaiGrid.Value >= item.FakturOpenStart And _
                              clSampaiGrid.Value <= item.FakturOpenEnd)) Then
                            tempEventValidation = True
                        End If
                    Else
                        If ((clDariGrid.Value >= item.FakturValidationStart And _
                             clDariGrid.Value <= item.FakturValidationEnd) Or _
                          (clSampaiGrid.Value >= item.FakturValidationStart And _
                              clSampaiGrid.Value <= item.FakturValidationEnd)) Then
                            tempEventValidation = True
                        End If
                    End If

                    If objBenefitType.AssyYearBox = 1 Then
                        If item.AssyYear = CInt(txtAssyGrid.Text) Then
                            tempAsst = True
                        End If
                    Else
                        tempAsst = True
                    End If
                    If tempEventValidation = True And tempAsst = True And checkTipe(temp, hfTypeGrid.Text) = True And checkTipe(tempLaesing, hfLeasingGrid.Text) = True Then
                        MessageBox.Show("Data tidak boleh sama dengan yang sudah ada atau periode beririsan. (tipe benefit, Assy Year, Periode dan tipe)")
                        Return
                    End If
                End If

            Next
        End If


        Dim assyYear As Integer = 0
        If Not txtAssyGrid.Text = "" Then
            assyYear = txtAssyGrid.Text
        End If
        Dim _splDealers As New ArrayList
        For Each item As String In txtKodeDealer.Text.Replace(" ", "").Split(";")
            'If i < txtKodeDealer.Text.Split(";").Length - 1 Or i = 0 Then
            If Not item Is Nothing And Not item = "" Then
                Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(item)
                If Not objDealerTmp Is Nothing AndAlso objDealerTmp.ID > 0 Then
                    _splDealers.Add(objDealerTmp)
                End If



            End If

        Next


        Dim objCheckTransaction As ArrayList

        If objBenefitType.EventValidation = 1 Then
            objCheckTransaction = New BenefitMasterDetailFacade(User).CheckTransaction(objBenefitType, _
                                                                                                 CShort(assyYear), _
                                                                                                 clDariOpenGrid.Value, _
                                                                                                 clSampaiOpenGrid.Value, _
                                                                                                 hfTypeGrid.Text, _
                                                                                                 hfLeasingGrid.Text, _splDealers)
        Else
            objCheckTransaction = New BenefitMasterDetailFacade(User).CheckTransaction(objBenefitType, _
                                                                                                   CShort(assyYear), _
                                                                                                   clDariGrid.Value, _
                                                                                                   clSampaiGrid.Value, _
                                                                                                   hfTypeGrid.Text, _
                                                                                                   hfLeasingGrid.Text, _splDealers)
        End If


        If objCheckTransaction.Count > 0 Then
            MessageBox.Show("Data sudah ada di transaksi benefit master lainnya")
            Return
        End If



        ObjBenefitMasterDetail = New BenefitMasterDetail

        ObjBenefitMasterDetail.FormulaID = ddlNoGrid.SelectedItem.Value


        ObjBenefitMasterDetail.BenefitType = objBenefitType


        If objBenefitType.WSDiscount = 1 Then
            If cbDiskonGrid.Checked = True Then
                ObjBenefitMasterDetail.WSDiscount = 1
            Else
                ObjBenefitMasterDetail.WSDiscount = 0
            End If
        End If



        Dim i As Integer = 0
        For Each item As String In hfLeasingGrid.Text.Replace(" ", "").Split(";")
            ' If i < hfLeasingGrid.Text.Split(";").Length - 1 Or i = 0 Then
            If Not item Is Nothing And Not item = "" Then
                Dim objLeasingCompanyFacade As LeasingCompanyFacade = New LeasingCompanyFacade(User)
                Dim objLeasingCompany As LeasingCompany = objLeasingCompanyFacade.Retrieve(CShort(item))
                If Not objLeasingCompany Is Nothing Then
                    Dim objBenefitMasterLeasing As BenefitMasterLeasing = New BenefitMasterLeasing
                    objBenefitMasterLeasing.LeasingCompany = objLeasingCompany
                    'objBenefitMasterLeasing.BenefitMasterDetail = ObjBenefitMasterDetail
                    ObjBenefitMasterDetail.BenefitMasterLeasings.Add(objBenefitMasterLeasing)
                End If

            End If
            i = i + 1
        Next



        i = 0
        For Each item As String In hfTypeGrid.Text.Replace(" ", "").Split(";")
            'If i < hfTypeGrid.Text.Split(";").Length - 1 Or i = 0 Then
            If Not item Is Nothing And Not item = "" Then
                Dim objVechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
                Dim objVechileType As VechileType = objVechileTypeFacade.Retrieve(item)
                If Not objVechileType Is Nothing Then
                    Dim objBenefitMasterVehicleType As BenefitMasterVehicleType = New BenefitMasterVehicleType
                    objBenefitMasterVehicleType.VechileType = objVechileType
                    'objBenefitMasterVehicleType.BenefitMasterDetail = ObjBenefitMasterDetail
                    ObjBenefitMasterDetail.BenefitMasterVehicleTypes.Add(objBenefitMasterVehicleType)
                End If

            End If
            i = i + 1
        Next





        ObjBenefitMasterDetail.Description = txtDescriptionGrid.Text
        ObjBenefitMasterDetail.Amount = txtAmountGrid.Text.Replace(".", "").Replace(",", ".")


        If objBenefitType.EventValidation = 1 Then
            ObjBenefitMasterDetail.FakturOpenStart = clDariOpenGrid.Value
            ObjBenefitMasterDetail.FakturOpenEnd = clSampaiOpenGrid.Value
        Else
            ObjBenefitMasterDetail.FakturValidationStart = clDariGrid.Value
            ObjBenefitMasterDetail.FakturValidationEnd = clSampaiGrid.Value
        End If



        'ObjBenefitMasterDetail.FakturOpenStart = cld.Value
        'ObjBenefitMasterDetail.FakturValidationEnd = clSampaiGrid.Value
        If Not txtAssyGrid.Text = "" Then
            ObjBenefitMasterDetail.AssyYear = txtAssyGrid.Text
        End If

        ObjBenefitMasterDetail.MaxClaim = txtMaxGrid.Text




        Dim itemToRemove As ListItem = ddlNoGrid.Items.FindByValue(ddlNoGrid.SelectedItem.Value)
        If Not itemToRemove Is Nothing Then
            ddlNoGrid.Items.Remove(itemToRemove)
        End If

        sessHelper.SetSession("addDetailSession", ObjBenefitMasterDetail)

        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))


    End Sub


    Private Sub AddCopyCommand(ByVal e As DataGridCommandEventArgs)

        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            dgTable.EditItemIndex = -1
            GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "ViewSave" Then
            dgTable.EditItemIndex = -1
            GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
        Else

            Dim lblNoGrid As Label = e.Item.FindControl("lblNoGrid")




            ' Dim e1 As DataGridItem

            Dim e1 As Control

            For Each e1 In dgTable.Controls

                ' e1 = dgTable.Items(dgTable.Items.Count - 1)

                For Each ct As Control In e1.Controls
                    If TypeOf ct Is System.Web.UI.WebControls.DataGridItem Then
                        '          System.Web.UI.WebControls.DataGridItem di = (System.Web.UI.WebControls.DataGridItem)ct;


                        'if(di.ItemType == ListItemType.Footer){


                        '     Control ctrl = di.FindControl("txtVrnAddDesc");


                        '}

                        Dim di As System.Web.UI.WebControls.DataGridItem = CType(ct, System.Web.UI.WebControls.DataGridItem)
                        If di.ItemType = ListItemType.Footer Then



                            Dim ddlTypeBenefitGrid As DropDownList = CType(di.FindControl("ddlTypeBenefitGrid"), DropDownList)


                            'Dim hfLeasingGrid As HiddenField = CType(e.Item.FindControl("hfLeasingGrid"), HiddenField)
                            Dim hfLeasingGrid As TextBox = di.FindControl("hfLeasingGrid")
                            Dim txtLeasingGrid As TextBox = di.FindControl("txtLeasingGrid")
                            Dim txtDescriptionGrid As TextBox = di.FindControl("txtDescriptionGrid")
                            Dim txtAmountGrid As TextBox = di.FindControl("txtAmountGrid")
                            Dim clDariGrid As KTB.DNet.WebCC.IntiCalendar = CType(di.FindControl("clDariGrid"), KTB.DNet.WebCC.IntiCalendar)
                            Dim clSampaiGrid As KTB.DNet.WebCC.IntiCalendar = CType(di.FindControl("clSampaiGrid"), KTB.DNet.WebCC.IntiCalendar)
                            'Dim hfTypeGrid As HiddenField = CType(e.Item.FindControl("hfTypeGrid"), HiddenField)
                            Dim hfTypeGrid As TextBox = di.FindControl("hfTypeGrid")
                            Dim txtTypeGrid As TextBox = di.FindControl("txtTypeGrid")

                            Dim ddlModelGrid As DropDownList = CType(di.FindControl("ddlModelGrid"), DropDownList)
                            Dim txtAssyGrid As TextBox = di.FindControl("txtAssyGrid")
                            Dim txtMaxGrid As TextBox = di.FindControl("txtMaxGrid")


                            'Dim objBenefitTypeFacade As BenefitTypeFacade = New BenefitTypeFacade(User)
                            'Dim li As ListItem
                            'For Each item As BenefitType In objBenefitTypeFacade.RetrieveActiveList()
                            '    li = New ListItem(item.Name, item.ID.ToString & ";" & item.WSDiscount & ";" & item.EventValidation & ";" & item.LeasingBox)
                            '    ddlTypeBenefitGrid.Items.Add(li)
                            'Next
                            'ddlTypeBenefitGrid.Attributes("onchange") = "showhideLeasing();"


                            If Not ddlTypeBenefitGrid Is Nothing Then
                                Dim listAll As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
                                '          ObjBenefitMasterDetail = New BenefitMasterDetail

                                '  ObjBenefitMasterDetail = CType(listAll(e.Item.ItemIndex), BenefitMasterDetail)

                                For Each item As BenefitMasterDetail In listAll
                                    If item.FormulaID = lblNoGrid.Text Then
                                        ' ObjBenefitMasterDetail = item

                                        '  Dim objBenefitType


                                        ddlTypeBenefitGrid.SelectedValue = item.BenefitType.ID & ";" & item.BenefitType.WSDiscount & ";" & item.BenefitType.EventValidation & ";" & item.BenefitType.LeasingBox
                                        Dim temp As String = ""
                                        Dim tempShow As String = ""
                                        For Each item11 As BenefitMasterLeasing In item.BenefitMasterLeasings
                                            temp = temp + ";" + item11.LeasingCompany.ID.ToString
                                            tempShow = tempShow + ";" + item11.LeasingCompany.LeasingName
                                        Next
                                        hfLeasingGrid.Text = temp
                                        txtLeasingGrid.Text = tempShow

                                        txtDescriptionGrid.Text = item.Description
                                        txtAmountGrid.Text = item.Amount.ToString("#,##0.00")
                                        clDariGrid.Value = item.FakturValidationStart
                                        clSampaiGrid.Value = item.FakturValidationStart
                                        temp = ""
                                        tempShow = ""

                                        Dim shrVechileModelID As Short = 0
                                        For Each item11 As BenefitMasterVehicleType In item.BenefitMasterVehicleTypes
                                            ' temp = temp + item11.VechileType.ID.ToString + ";"
                                            temp = temp + ";" + item11.VechileType.VechileTypeCode.ToString
                                            tempShow = tempShow + ";" + item11.VechileType.VechileTypeCode
                                            'ddlModelGrid.SelectedValue = item11.VechileType.VechileModel.ID.ToString
                                            shrVechileModelID = item11.VechileType.VechileModel.ID
                                        Next
                                        ddlModelGrid.SelectedValue = GetVehicleSubCategoryValue(shrVechileModelID)

                                        hfTypeGrid.Text = temp
                                        txtTypeGrid.Text = tempShow

                                        txtAssyGrid.Text = item.AssyYear
                                        txtMaxGrid.Text = item.MaxClaim



                                        Exit For
                                    End If

                                Next

                            End If


                        End If

                    End If

                    ' if(ct is System.Web.UI.WebControls.DataGridItem){
                Next






            Next
            dgTable.EditItemIndex = -1
            ' GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
            dgTable.ShowFooter = True



        End If

    End Sub




    Private Sub InitializeForm()
        BindDdlStatus()
        RemoveALLSession()
        If Request.QueryString("Mode") = "View" Then
            disableform(2)
            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"
            btnSimpan.Visible = False
            btnDelete.Visible = False
            lblPopUpDealer.Visible = False
            lblPopUpRefBenefit.Visible = False
            dgTable.ShowFooter = False
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "View")

            If Not Request.QueryString("Status") Is Nothing Then
                MessageBox.Show("Master Benefit telah dipakai di claim. Hanya dapat mengubah status.")
                'Dim _arrList As New ArrayList
                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'Dim strSql As String = ""
                'strSql += " select b.id from BenefitMasterHeader a "
                'strSql += "inner join BenefitMasterDetail b on a.ID = b.BenefitMasterHeaderID "
                'strSql += "where a.ID =  " & id
                'criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitMasterDetail", MatchType.InSet, "(" & strSql & ")"))
                '_arrList = New BenefitClaimDetailsFacade(User).Retrieve(criterias)
                'If _arrList.Count > 0 Then
                '    MessageBox.Show("Master Benefit telah dipakai di claim. Hanya dapat mengubah status.")
                '    disableform(2)
                '    dgTable.ShowFooter = False
                '    ddlStatus.Enabled = True
                'End If


                btnSimpan.Visible = True
                ddlStatus.Enabled = True
            End If

            GetValueFromDataBase(id)
        ElseIf Request.QueryString("Mode") = "ViewSave" Then
            disableform(2)
            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"
            btnSimpan.Visible = False
            btnDelete.Visible = False
            lblPopUpDealer.Visible = False
            lblPopUpRefBenefit.Visible = False
            dgTable.ShowFooter = False
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "ViewSave")
            GetValueFromDataBase(id)

        ElseIf Request.QueryString("Mode") = "Edit" Then
            disableform(1)

            Dim id As Integer = CInt(Request.QueryString("id"))
            btnSimpan.Visible = True
            btnDelete.Visible = False
            lblPopUpDealer.Visible = True
            lblPopUpRefBenefit.Visible = True
            btnBatal.Text = "Batal"
            txtRegNo.Enabled = False
            'txtKodeDealer.Enabled = False
            txtKodeDealer.Enabled = True
            dgTable.ShowFooter = True
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "Edit")
            GetValueFromDataBase(id)
            txtRefBenefit.Enabled = False
            lblPopUpRefBenefit.Visible = False

        ElseIf Request.QueryString("Mode") = "Delete" Then
            disableform(2)
            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"
            btnDelete.Visible = True
            btnSimpan.Visible = False
            lblPopUpDealer.Visible = False
            lblPopUpRefBenefit.Visible = False
            dgTable.ShowFooter = False
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "Delete")
            GetValueFromDataBase(id)


        Else
            disableform(1)
            txtRefBenefit.Enabled = True
            lblPopUpRefBenefit.Visible = True

            btnDelete.Visible = False
            btnSimpan.Visible = True
            lblPopUpDealer.Visible = True
            lblPopUpRefBenefit.Visible = True
            btnBatal.Text = "Batal"
            sessHelper.SetSession("status", "Insert")
            dgTable.ShowFooter = True
            Dim list As ArrayList = New ArrayList
            dgTable.DataSource = list
            dgTable.DataBind()
        End If


    End Sub


    Private Sub disableform(ByVal i As Integer)
        If i = 1 Then 'insert edit
            txtNoSurat.Enabled = True
            txtRegNo.Enabled = Not txtNoSurat.Enabled
            txtRemark.Enabled = txtNoSurat.Enabled
            ddlStatus.Enabled = txtNoSurat.Enabled
            txtKodeDealer.Enabled = txtNoSurat.Enabled
            txtRefBenefit.Enabled = True
            lblPopUpRefBenefit.Visible = True
        Else
            txtNoSurat.Enabled = False
            txtRegNo.Enabled = txtNoSurat.Enabled
            txtRemark.Enabled = txtNoSurat.Enabled
            ddlStatus.Enabled = txtNoSurat.Enabled
            txtKodeDealer.Enabled = txtNoSurat.Enabled
            txtRefBenefit.Enabled = False
            lblPopUpRefBenefit.Visible = False
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim list As New ArrayList
        Dim i As Integer

        Dim formulaWithDelimiter As String = HiddenFieldFormula.Value

        If Not Request.QueryString("Status") Is Nothing Then

            Dim IDBenefitListHeader1 As Integer = CInt(sessHelper.GetSession("IDBenefitListHeader"))

            Dim objDomainTemp As BenefitMasterHeader = New BenefitMasterHeaderFacade(User).Retrieve(IDBenefitListHeader1)

            If Not objDomainTemp Is Nothing Then
                objDomainTemp.Status = CShort(ddlStatus.SelectedValue)
                Dim n As Integer = objDomainFacade.UpdateStatus(objDomainTemp)

                If n = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    RemoveALLSession()

                    Response.Write("<script>alert('Data berhasil diubah')</script>")
                    Response.Write("<script>window.location.href='FrmBenefitList.aspx';</script>")

                End If
            End If

            Exit Sub

        End If





        If txtKodeDealer.Text.Replace(" ", "") = "" Then
            MessageBox.Show("Isi Dealer ")
            Return
        End If
        If txtNoSurat.Text = "" Then
            MessageBox.Show("Isi No Surat")
            Return
        End If


        If Not sessHelper.GetSession("DetailSession") Is Nothing Then
            list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        Else
            list = New ArrayList
        End If

        'If list.Count < 1 Then
        '    MessageBox.Show("Isi Detail minimal 1")
        '    Return
        'End If

        If Request.Form("rwwww") Is Nothing Then
            MessageBox.Show("Isi Formula")
            Return
        End If



        'Dim aaa As Integer = Evaluate(Request.Form("rwwww"))
        Dim constabjad As String = New AppConfigFacade(User).Retrieve("AlphabeticalFormula").Value
        'Dim aaa As Decimal = New BenefitMasterHeaderFacade(User).Evaluate(Request.Form("rwwww"), constabjad)
        Dim aaa As Decimal = New BenefitMasterHeaderFacade(User).Evaluate(formulaWithDelimiter, constabjad)
        If aaa = 0 Then
            MessageBox.Show("Formula tidak valid")
            Return
        ElseIf aaa = -1 Then
            MessageBox.Show("Terdapat formula yang beririsan")
            Return
        ElseIf aaa = -2 Then
            MessageBox.Show("Dalam satu formula (pemisah titik) tidak boleh formula id yang sama")
            Return
        End If




        If list.Count > 0 Then
            For Each item As BenefitMasterDetail In list
                If Request.Form("rwwww").ToString.Contains(item.FormulaID.Trim()) = False Then
                    MessageBox.Show("Formula Id " & item.FormulaID & " tidak terdapat di Formula")
                    Return
                End If
            Next
        End If



        If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then


            'objDomain.BenefitRegNo = txtRegNo.Text
            objDomain.NomorSurat = txtNoSurat.Text
            objDomain.Remarks = txtRemark.Text
            objDomain.Status = ddlStatus.SelectedItem.Value
            Dim _splDealers As New ArrayList
            For Each item As String In txtKodeDealer.Text.Replace(" ", "").Split(";")
                'If i < txtKodeDealer.Text.Split(";").Length - 1 Or i = 0 Then
                If Not item Is Nothing And Not item = "" Then
                    Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(item)
                    If Not objDealerTmp Is Nothing Then
                        Dim objDealer As BenefitMasterDealer = New BenefitMasterDealer
                        objDealer.Dealer = objDealerTmp
                        _splDealers.Add(objDealer)
                    End If

                End If
                i = i + 1
            Next





            For Each items As BenefitMasterDetail In list


                If items.BenefitMasterLeasings.Count > 0 Then
                    For Each items1 As BenefitMasterLeasing In items.BenefitMasterLeasings
                        ObjBenefitMasterDetail.BenefitMasterLeasings.Add(items1)
                    Next
                End If

                If items.BenefitMasterVehicleTypes.Count > 0 Then
                    For Each items2 As BenefitMasterVehicleType In items.BenefitMasterVehicleTypes
                        ObjBenefitMasterDetail.BenefitMasterVehicleTypes.Add(items2)
                    Next
                End If
                objDomain.BenefitMasterDetails.Add(items)

            Next



            Dim textFormula As String = ""
            For j As Integer = 0 To Request.Form("rwwww").Length - 1
                If Not Request.Form("rwwww")(j) = "" Then
                    textFormula += Request.Form("rwwww")(j)
                End If
            Next
            objDomain.Formula = textFormula.Replace(",", "")





            Dim n As Integer = objDomainFacade.Insert(objDomain, list, _splDealers)
            'Dim n As Integer = objDomainFacade.Insert(objDomain)
            If n = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                RemoveALLSession()
                ' Response.Redirect("FrmNewInputMasterList.aspx")
                'MessageBox.Show("Simpan Berhasil")
                'Response.Redirect("FrmBenefitList.aspx")
                Response.Write("<script>alert('Data berhasil disimpan')</script>")
                '                Response.Write("<script>window.location.href='FrmBenefitList.aspx';</script>")
                ' Response.Write("<script>window.location.href='FrmNewInputMasterList.aspx';</script>")
                If Convert.ToString(Request.QueryString("Mode")) = "View" Or
                    Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
                    Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
                    Response.Write("<script>window.location.href='FrmBenefitList.aspx';</script>")
                Else
                    Response.Write("<script>window.location.href='FrmNewInputMasterList.aspx?Mode=ViewSave&id=" & n & "';</script>")
                End If

            End If


        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Edit" Then

            Dim IDBenefitListHeader As Integer = CInt(sessHelper.GetSession("IDBenefitListHeader"))


            'Dim _arrList As New ArrayList
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            'Dim strSql As String = ""
            'strSql += "  select b.id from BenefitMasterHeader a "
            'strSql += "  inner join BenefitMasterDetail b on a.ID = b.BenefitMasterHeaderID"
            'strSql += "  and b.RowStatus = 0"
            'strSql += "  where a.RowStatus = 0 and b.BenefitTypeID is not null and b.BenefitTypeID <> ''"
            'strSql += "  and a.ID = '" & IDBenefitListHeader & "'"
            'criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "ID", MatchType.InSet, "(" & strSql & ")"))
            '_arrList = New BenefitMasterDetailFacade(User).Retrieve(criterias)
            'If _arrList.Count > 0 Then
            '    MessageBox.Show("Benefit masih digunakan di modul lain (Benefit Detail).")
            '    Return
            'End If

            'Dim _arrList2 As New ArrayList
            'Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias2.opAnd(New Criteria(GetType(BenefitEventHeader), "BenefitMasterHeader", MatchType.Exact, CInt(IDBenefitListHeader)))
            '_arrList2 = New BenefitEventHeaderFacade(User).Retrieve(criterias2)
            'If _arrList2.Count > 0 Then
            '    MessageBox.Show("Benefit masih digunakan di modul lain (Event).")
            '    Return
            'End If




            'Dim objDomain As BenefitMasterHeader = objDomainFacade.Retrieve(IDBenefitListHeader)

            Dim objDomainTemp As BenefitMasterHeader = New BenefitMasterHeader
            objDomainTemp.ID = IDBenefitListHeader
            objDomainTemp.BenefitRegNo = txtRegNo.Text
            objDomainTemp.NomorSurat = txtNoSurat.Text
            objDomainTemp.Remarks = txtRemark.Text
            objDomainTemp.Status = ddlStatus.SelectedItem.Value
            Dim _splDealers As New ArrayList
            For Each item As String In txtKodeDealer.Text.Replace(" ", "").Split(";")
                If Not item Is Nothing And Not item = "" Then
                    Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(item)
                    Dim objDealer As BenefitMasterDealer = New BenefitMasterDealer
                    objDealer.Dealer = objDealerTmp
                    _splDealers.Add(objDealer)

                End If
                i = i + 1
            Next
            '            objDomain.BenefitMasterDealers

            If Not sessHelper.GetSession("DetailSession") Is Nothing Then
                list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
            End If





            Dim textFormula As String = ""
            For j As Integer = 0 To Request.Form("rwwww").Length - 1
                textFormula += Request.Form("rwwww")(j)
            Next
            objDomainTemp.Formula = textFormula.Replace(",", "")



            Dim listOldDetailSession As ArrayList
            If Not sessHelper.GetSession("OldDetailSession") Is Nothing Then
                listOldDetailSession = CType(sessHelper.GetSession("OldDetailSession"), ArrayList)
            End If

            Dim listOldDealerSession As ArrayList
            If Not sessHelper.GetSession("OldDealerSession") Is Nothing Then
                listOldDealerSession = CType(sessHelper.GetSession("OldDealerSession"), ArrayList)
            End If




            Dim n As Integer = objDomainFacade.Update(objDomainTemp, list, _splDealers)

            If n = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                RemoveALLSession()
                ' Response.Redirect("FrmNewInputMasterList.aspx")
                'MessageBox.Show("Simpan Berhasil")
                'Response.Redirect("FrmBenefitList.aspx")
                Response.Write("<script>alert('Data berhasil diubah')</script>")
                '                Response.Write("<script>window.location.href='FrmBenefitList.aspx';</script>")
                'Response.Write("<script>window.location.href='FrmNewInputMasterList.aspx';</script>")
                If Convert.ToString(Request.QueryString("Mode")) = "View" Or
                  Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
                  Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
                    Response.Write("<script>window.location.href='FrmBenefitList.aspx';</script>")
                Else
                    Response.Write("<script>window.location.href='FrmNewInputMasterList.aspx';</script>")
                End If
            End If


        End If
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ' MessageBox.Confirm()

        Dim IDBenefitListHeader As Integer = CInt(sessHelper.GetSession("IDBenefitListHeader"))


        Dim _arrList As New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '  criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "BenefitMasterHeader", MatchType.Exact, CInt(IDBenefitListHeader)))


        Dim strSql As String = ""
        'strSql += "  select b.id from BenefitMasterHeader a "
        'strSql += "  inner join BenefitMasterDetail b on a.ID = b.BenefitMasterHeaderID"
        'strSql += "  and b.RowStatus = 0"
        'strSql += "  where a.RowStatus = 0 and b.BenefitTypeID is not null and b.BenefitTypeID <> ''"
        'strSql += "  and a.ID = '" & IDBenefitListHeader & "'"
        'criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "ID", MatchType.InSet, "(" & strSql & ")"))
        '_arrList = New BenefitMasterDetailFacade(User).Retrieve(criterias)
        'If _arrList.Count > 0 Then
        '    MessageBox.Show("Benefit masih digunakan di modul lain (Benefit Detail).")
        '    Return
        'End If

        Dim _arrList2 As New ArrayList
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(BenefitEventDetail), "BenefitEventHeader.BenefitMasterHeader.ID", MatchType.Exact, CInt(IDBenefitListHeader)))
        _arrList2 = New BenefitEventDetailFacade(User).Retrieve(criterias2)
        If _arrList2.Count > 0 Then
            MessageBox.Show("Benefit masih digunakan di modul lain (Event).")
            Return
        End If


        Dim _arrList3 As New ArrayList
        Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        strSql = ""
        strSql += "   select a.id from benefitmasterdetail a "
        strSql += "   inner join benefitmasterheader b on a.benefitmasterheaderid = b.id "
        strSql += "  where b.ID = '" & IDBenefitListHeader & "'"
        criterias3.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitMasterDetail.ID", MatchType.InSet, "(" & strSql & ")"))

        _arrList3 = New BenefitClaimDetailsFacade(User).Retrieve(criterias3)
        If _arrList3.Count > 0 Then
            MessageBox.Show("Benefit masih digunakan di modul lain (Claim).")
            Return
        End If







        Dim list As New ArrayList
        Dim i As Integer




        'Dim _arrList As New ArrayList
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim strSql As String = ""
        'criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "BenefitMasterHeader", MatchType.Exact, IDBenefitListHeader))

        '_arrList = New BenefitMasterDetailFacade(User).Retrieve(criterias)

        'For Each item As BenefitMasterDetail In _arrList
        '    If Not item.BenefitType Is Nothing Then

        '        MessageBox.Show("Tipe Benefit masih ada")
        '        Return
        '    End If
        'Next





        Dim objDomainTemp As BenefitMasterHeader = objDomainFacade.Retrieve(IDBenefitListHeader)


        Dim check As Boolean = False


        'For Each item As BenefitMasterDetail In objDomainTemp.BenefitMasterDetails
        '    Dim _arrList1 As New ArrayList
        '    Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias1.opAnd(New Criteria(GetType(BenefitClaimHeader), "BenefitMasterDetail.ID", MatchType.Exact, CInt(item.ID)))
        '    _arrList1 = New BenefitClaimHeaderFacade(User).Retrieve(criterias1)
        '    If _arrList1.Count > 0 Then
        '        MessageBox.Show("Benefit masih digunakan di modul lain.")
        '        Return
        '    End If
        'Next






        Dim n As Integer = objDomainFacade.Delete(objDomainTemp)
        'Dim n As Integer = objDomainFacade.Insert(objDomain)
        If n = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            RemoveALLSession()
            ' Response.Redirect("FrmNewInputMasterList.aspx")
            '  MessageBox.Show("Hapus Berhasil")
            '  Response.Redirect("FrmBenefitList.aspx")
            Response.Write("<script>alert('Data  berhasil dihapus.')</script>")
            Response.Write("<script>window.location.href='FrmBenefitList.aspx';</script>")

        End If



    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        If hdnBenefitMasterID.Value.Trim = "" Then
            If txtRefBenefit.Text.Trim = "" Then
                Exit Sub
            Else
                Dim objBMH As BenefitMasterHeader = New BenefitMasterHeaderFacade(User).Retrieve(txtRefBenefit.Text.Trim)
                hdnBenefitMasterID.Value = objBMH.ID
            End If
        End If

        disableform(1)
        Dim id As Integer = CInt(hdnBenefitMasterID.Value)
        btnSimpan.Visible = True
        btnDelete.Visible = False
        lblPopUpDealer.Visible = True
        lblPopUpRefBenefit.Visible = True
        btnBatal.Text = "Batal"
        txtRegNo.Enabled = False
        txtKodeDealer.Enabled = True
        dgTable.ShowFooter = True
        sessHelper.SetSession("IDBenefitListHeader", id)
        sessHelper.SetSession("status", "Insert")

        sessHelper.RemoveSession("DetailSession")
        sessHelper.RemoveSession("OldDetailSession")
        sessHelper.RemoveSession("OldDealerSession")
        hfformula.Value = ""

        GetValueFromDataBase(id)

        txtNoSurat.Text = ""
        txtRegNo.Text = ""
        txtRemark.Text = ""
        ddlStatus.SelectedIndex = 0
        sessHelper.SetSession("alBenefitMasterDealers", New ArrayList)
        sessHelper.SetSession("IDBenefitListHeader", 0)
        txtKodeDealer.Text = ""
    End Sub

End Class
