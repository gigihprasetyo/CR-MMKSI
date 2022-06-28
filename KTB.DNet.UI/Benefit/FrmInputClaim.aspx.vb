Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessValidation.Helpers
Imports System.Linq
Imports System.Data
Imports Excel

Imports System.IO
Imports System.Text

Imports Microsoft.CSharp
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports KTB.DNet.BusinessFacade
Imports System.Collections.Generic

Public Class FrmInputClaim
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRegClaimNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRegEvent As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIdDetailMasterShow As System.Web.UI.WebControls.TextBox
    Protected WithEvents fileUploadExcel As System.Web.UI.WebControls.FileUpload

    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents icClaimDate As KTB.DNet.WebCC.IntiCalendar

    Protected WithEvents ddlReferensiClaim As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPilihanClaim As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLeasing As System.Web.UI.WebControls.DropDownList

    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblNilaiClaim As System.Web.UI.WebControls.Label
    Protected WithEvents lblPpn As System.Web.UI.WebControls.Label
    Protected WithEvents lblPph As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalReduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblChassisNumberReduksi As System.Web.UI.WebControls.Label

    Protected WithEvents cbRefClaim As System.Web.UI.WebControls.CheckBox
    Protected WithEvents dgGridDetil As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnRefClaim As System.Web.UI.WebControls.Button
    Protected WithEvents panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents txtIdDetailMaster As System.Web.UI.WebControls.HiddenField

    Protected WithEvents lblMessage As System.Web.UI.WebControls.Label

    Protected WithEvents cbNorangka As System.Web.UI.WebControls.CheckBox
    Protected WithEvents panel3 As System.Web.UI.WebControls.Panel
    Protected WithEvents dgNoRangka As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNoRangkaPanel3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsiPanel3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoMesinPanel3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCariPanel3 As System.Web.UI.WebControls.Button
    Protected WithEvents icFakturPanel3 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtCustomerPanel3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cbFakterPanel3 As System.Web.UI.WebControls.CheckBox

    Protected WithEvents btnReload As System.Web.UI.WebControls.Button

    Protected WithEvents panel4 As System.Web.UI.WebControls.Panel
    Protected WithEvents lblPopUpRegEvent As System.Web.UI.WebControls.LinkButton
    Protected WithEvents dgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hfIIdEvent As System.Web.UI.WebControls.HiddenField

    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button

    Protected WithEvents Panel5 As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnStatus As System.Web.UI.WebControls.Button

    Protected WithEvents arrayCheck As System.Web.UI.WebControls.HiddenField


    Protected WithEvents lblDelerSession As System.Web.UI.WebControls.Label


    Protected WithEvents LinkDownload As System.Web.UI.WebControls.LinkButton

    Protected WithEvents txtFormula As System.Web.UI.WebControls.HiddenField


    Protected WithEvents Panel8 As System.Web.UI.WebControls.Panel
    Protected WithEvents txtIdDetailForKet As System.Web.UI.WebControls.HiddenField
    Protected WithEvents txtKeterangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSaveKet As System.Web.UI.WebControls.Button

    Protected WithEvents lblPopUpMMKSINotes As System.Web.UI.WebControls.Label
    Protected WithEvents txtMMKSINotes As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnBenefitClaimHeaderID As System.Web.UI.WebControls.HiddenField


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
    Private objDomain As BenefitClaimHeader = New BenefitClaimHeader
    Private objDomainFacade As BenefitClaimHeaderFacade = New BenefitClaimHeaderFacade(User)

    Private objDomainDetail As BenefitClaimDetails = New BenefitClaimDetails
    Private objDomainDetailFacade As BenefitClaimDetailsFacade = New BenefitClaimDetailsFacade(User)
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Private inputbenefitclaim_privillage As Boolean
    Private prosesdaftarclaimktb_privillage As Boolean
    Private Viewdaftarclaim_privillage As Boolean

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
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=ALERT MANAGEMENT - Alert Managemen List")
        'End If
        inputbenefitclaim_privillage = False

        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then

            If IsNothing(Request.QueryString("Mode")) OrElse Request.QueryString("Mode").ToString() = "" Then
                inputbenefitclaim_privillage = SecurityProvider.Authorize(Context.User, SR.inputbenefitclaim_privillage)

                If Not inputbenefitclaim_privillage Then

                    Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Input Claim")
                End If
            End If


        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            prosesdaftarclaimktb_privillage = SecurityProvider.Authorize(Context.User, SR.prosesdaftarclaimktb_privillage)
            If Not prosesdaftarclaimktb_privillage Then
                btnStatus.Visible = False

            End If

        End If




    End Sub

    Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege)
#End Region


    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Dim Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

    Private Function Evaluate(ByVal MathExpression As String) As Decimal

        Dim tempMathExpression As String = ""

        For Each c As Char In MathExpression.Replace(",", "")
            ' Count c            
            If c = "&" Then
                ' tempMathExpression = tempMathExpression & "+"
                tempMathExpression = tempMathExpression & " && "
            ElseIf c = "|" Then
                'tempMathExpression = tempMathExpression & "*"
                tempMathExpression = tempMathExpression & " || "
            Else
                tempMathExpression = tempMathExpression & c
            End If


        Next

        Dim values As String() = tempMathExpression.Split({"."}, StringSplitOptions.RemoveEmptyEntries)
        Dim result As Decimal = 0
        For i As Integer = 0 To values.Length - 1
            If Not values(i) = "" Then
                Try
                    Dim codeProvider As CSharpCodeProvider = New CSharpCodeProvider()
                    Dim compilerParameters As CompilerParameters = New CompilerParameters
                    compilerParameters.GenerateExecutable = False
                    compilerParameters.GenerateInMemory = False


                    Dim tmpModuleSource As String = "namespace ns{"
                    tmpModuleSource = tmpModuleSource & "using System;"
                    tmpModuleSource = tmpModuleSource & "class class1{"
                    tmpModuleSource = tmpModuleSource & "    public static decimal Eval(){"
                    tmpModuleSource = tmpModuleSource & "          Boolean firstCheck = false;"
                    tmpModuleSource = tmpModuleSource & "          firstCheck = " & values(i) & ";"
                    tmpModuleSource = tmpModuleSource & "          if (firstCheck == true){ return 2; "
                    tmpModuleSource = tmpModuleSource & "          } else { return 1;"
                    tmpModuleSource = tmpModuleSource & "          } "
                    tmpModuleSource = tmpModuleSource & "     }"
                    tmpModuleSource = tmpModuleSource & "}} "

                    Dim CompilerResults As CompilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, tmpModuleSource)

                    If CompilerResults.Errors.Count > 0 Then
                        result = 0
                        Exit For
                    Else
                        Dim Methinfo As MethodInfo = CompilerResults.CompiledAssembly.GetType("ns.class1").GetMethod("Eval")
                        result = Convert.ToDecimal(Methinfo.Invoke(Nothing, Nothing))
                        If result = 2 Then
                            Exit For
                        End If
                    End If
                Catch ex As Exception
                    result = 0
                    Exit For
                End Try
            End If
        Next

        Return result


    End Function

    Private Sub InitializeForm()

        RemoveALLSession()
        Panel5.Visible = False
        hdnBenefitClaimHeaderID.Value = CInt(Request.QueryString("id"))

        lblPopUpMMKSINotes.Attributes("style") = "display:none;"
        If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
            If Request.QueryString("Mode") = "View" Then
                lblPopUpMMKSINotes.Attributes("style") = "display:;"
            End If
        End If

        If Request.QueryString("Mode") = "View" Then
            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"
            btnSimpan.Visible = False
            btnUpload.Visible = btnSimpan.Visible
            btnRefClaim.Visible = btnSimpan.Visible
            'lblPopUpRegEvent.Visible = btnSimpan.Visible
            lblPopUpDealer.Visible = btnSimpan.Visible
            btnDelete.Visible = btnSimpan.Visible
            'btnDelete.Visible = False
            lblPopUpDealer.Visible = False
            dgTable.ShowFooter = False
            txtKodeDealer.Enabled = True
            ddlPilihanClaim.Enabled = Not txtKodeDealer.Enabled
            ddlLeasing.Enabled = Not txtKodeDealer.Enabled
            txtIdDetailMasterShow.ReadOnly = txtKodeDealer.Enabled
            btnRefClaim.Visible = btnSimpan.Visible
            txtRegEvent.Enabled = txtKodeDealer.Enabled
            'Panel5.Visible = txtKodeDealer.Enabled
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "View")
            GetValueFromDataBase(id)
        ElseIf Request.QueryString("Mode") = "ViewSave" Then

            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"
            btnSimpan.Visible = False
            btnUpload.Visible = btnSimpan.Visible
            btnRefClaim.Visible = btnSimpan.Visible
            'lblPopUpRegEvent.Visible = btnSimpan.Visible

            btnDelete.Visible = btnSimpan.Visible
            'btnDelete.Visible = False
            lblPopUpDealer.Visible = False
            dgTable.ShowFooter = False
            txtKodeDealer.Enabled = True
            ddlPilihanClaim.Enabled = Not txtKodeDealer.Enabled
            ddlLeasing.Enabled = Not txtKodeDealer.Enabled
            txtIdDetailMasterShow.ReadOnly = txtKodeDealer.Enabled
            btnRefClaim.Visible = btnSimpan.Visible
            txtRegEvent.Enabled = txtKodeDealer.Enabled
            ' Panel5.Visible = txtKodeDealer.Enabled
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "ViewSave")
            GetValueFromDataBase(id)

        ElseIf Request.QueryString("Mode") = "Edit" Then

            Dim id As Integer = CInt(Request.QueryString("id"))
            btnSimpan.Visible = True
            'btnUpload.Visible = btnSimpan.Visible
            btnRefClaim.Visible = Not btnSimpan.Visible
            'lblPopUpRegEvent.Visible = Not btnSimpan.Visible

            btnDelete.Visible = Not btnSimpan.Visible
            btnBatal.Text = "Batal"
            txtKodeDealer.Enabled = False
            ddlPilihanClaim.Enabled = txtKodeDealer.Enabled
            ddlLeasing.Enabled = txtKodeDealer.Enabled
            txtIdDetailMasterShow.ReadOnly = txtKodeDealer.Enabled
            btnRefClaim.Visible = Not btnSimpan.Visible
            txtRegEvent.Enabled = txtKodeDealer.Enabled


            If lblDelerSession.Visible = True Then
                dgTable.ShowFooter = True
            Else
                dgTable.ShowFooter = False
            End If
            btnUpload.Visible = dgTable.ShowFooter

            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "Edit")
            GetValueFromDataBase(id)
        ElseIf Request.QueryString("Mode") = "Delete" Then

            Dim id As Integer = CInt(Request.QueryString("id"))
            btnBatal.Text = "Kembali"

            btnSimpan.Visible = False
            btnUpload.Visible = btnSimpan.Visible
            btnRefClaim.Visible = btnSimpan.Visible
            'lblPopUpRegEvent.Visible = btnSimpan.Visible

            dgTable.ShowFooter = False

            txtKodeDealer.Enabled = True


            btnDelete.Visible = True
            ddlPilihanClaim.Enabled = txtKodeDealer.Enabled
            ddlLeasing.Enabled = txtKodeDealer.Enabled
            txtIdDetailMasterShow.ReadOnly = txtKodeDealer.Enabled
            btnRefClaim.Visible = btnSimpan.Visible
            txtRegEvent.Enabled = txtKodeDealer.Enabled
            sessHelper.SetSession("IDBenefitListHeader", id)
            sessHelper.SetSession("status", "Delete")
            GetValueFromDataBase(id)


        Else


            btnSimpan.Visible = True
            btnDelete.Visible = False
            'btnUpload.Visible = btnSimpan.Visible
            btnRefClaim.Visible = btnSimpan.Visible
            'lblPopUpRegEvent.Visible = btnSimpan.Visible
            'lblPopUpRegEvent.Visible = btnSimpan.Visible

            btnBatal.Text = "Batal"
            sessHelper.SetSession("status", "Insert")
            If lblDelerSession.Visible = True Then
                dgTable.ShowFooter = True

            Else
                dgTable.ShowFooter = False

            End If
            btnUpload.Visible = dgTable.ShowFooter

            txtKodeDealer.Enabled = True
            ddlPilihanClaim.Enabled = txtKodeDealer.Enabled
            ddlLeasing.Enabled = txtKodeDealer.Enabled
            txtIdDetailMasterShow.Enabled = txtKodeDealer.Enabled
            btnRefClaim.Visible = btnSimpan.Visible
            txtRegEvent.Enabled = txtKodeDealer.Enabled
            Dim list As ArrayList = New ArrayList
            dgTable.DataSource = list
            dgTable.DataBind()

            dgNoRangka.DataSource = list
            dgNoRangka.DataBind()

            'dgNoRangka.DataSource = list
            'dgNoRangka.DataBind()
        End If


    End Sub

    Private Sub RetrieveDealer()
        Dim objSessionHelper As New SessionHelper
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then

            If Not objDealer.DealerGroup Is Nothing Then 'untuk dealer side
                lblDelerSession.Visible = True
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeDealer.Attributes("style") = "display:none"
                lblDelerSession.Text = objDealer.DealerCode & " / " & objDealer.DealerName
                txtKodeDealer.Text = objDealer.DealerCode

                '   Panel5.Visible = Not lblDelerSession.Visible

                dgTable.ShowFooter = lblDelerSession.Visible
                btnUpload.Visible = lblDelerSession.Visible
            Else
                lblDelerSession.Visible = False
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeDealer.Attributes("style") = "display:"
                ' Panel5.Visible = lblDelerSession.Visible
                If Request.QueryString("Mode") = "Edit" Then
                    '      Panel5.Visible = Not lblDelerSession.Visible
                End If


                dgTable.ShowFooter = lblDelerSession.Visible
                btnUpload.Visible = lblDelerSession.Visible
            End If

        Else

        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            ddlPilihanClaim.Attributes("onchange") = "showhideLeasing();"
            InitDisplayPilihanClaim()
            InitDisplayLeasing()
            ViewState("currentSortColumn") = "Name"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            'ViewState("currentSortDirection") = Sort.SortDirection.DESC

            lblPopUpMMKSINotes.Attributes("onclick") = "ShowPPMMKSINotesSelection();"

            RetrieveDealer()

            InitializeForm()
            ' dgTables.CurrentPageIndex = 0
            ' BindDataGrid(dgTables.CurrentPageIndex
        End If

        panel1.Attributes("style") = "display:;"
        panel2.Attributes("style") = "display:none;"
        panel3.Attributes("style") = "display:none;"
        panel4.Attributes("style") = "display:none;"


        Panel8.Attributes("style") = "display:none;"
    End Sub

    Private Sub InitDisplayPilihanClaim()
        Dim facade As New BenefitTypeFacade(User)
        Dim arlFacade As ArrayList = facade.RetrieveActiveList()

        ddlPilihanClaim.Items.Clear()

        For Each cat As BenefitType In arlFacade
            If cat.Status = 0 Then
                ddlPilihanClaim.Items.Add(New ListItem(cat.Name, cat.ID.ToString & ";" & cat.LeasingBox.ToString & ";" & cat.EventValidation.ToString))
            End If
        Next

    End Sub

    Private Sub InitDisplayLeasing()
        'Dim categoryId As Integer = CInt(ddlAlertCategory.SelectedValue)
        'Dim arlModul As ArrayList = New AlertModulFacade(User).RetrieveActiveListByCategoryID(categoryId)

        'ddlAlertModul.Items.Clear()
        'ddlAlertModul.Items.Add(New ListItem("Semua", 0))
        'For Each modul As AlertModul In arlModul
        '    ddlAlertModul.Items.Add(New ListItem(modul.Description, modul.ID))
        'Next
        Dim facade As New LeasingCompanyFacade(User)
        Dim arlFacade As ArrayList = facade.RetrieveActiveList()

        ddlLeasing.Items.Clear()
        'ddlPilihanClaim.Items.Add(New ListItem("Semua", 0))

        For Each cat As LeasingCompany In arlFacade
            ddlLeasing.Items.Add(New ListItem(cat.LeasingName, cat.ID.ToString))
        Next
        ddlLeasing.Style.Add("display", "none")
    End Sub


    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "Add"
                AddCommand(e)
            Case "View"
                'Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=View;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
            Case "Edit"
                'Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=Edit;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
            Case "Delete"
                DeleteCommand(e)
            Case "AddKet"
                EditCommand(e, CInt(e.CommandArgument))
        End Select
    End Sub


    '
    Private Sub btnSaveKet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveKet.Click
        UpdateCommand()
    End Sub

    Private Sub UpdateCommand()

        Dim tempInteger As Integer = 0
        Dim ObjBenefitClaimDetailsTemp As BenefitClaimDetails = New BenefitClaimDetails

        Dim listAll As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        For Each item As BenefitClaimDetails In listAll

            If CInt(txtIdDetailForKet.Value) = item.ID Then
                ObjBenefitClaimDetailsTemp = item
                If Not lblDelerSession.Visible = True Then 'ktbside
                    ObjBenefitClaimDetailsTemp.DescKtb = txtKeterangan.Text
                Else 'dealer side
                    ObjBenefitClaimDetailsTemp.DescDealer = txtKeterangan.Text
                End If

                Exit For
            End If
            tempInteger = tempInteger + 1
        Next

        If dgTable.ShowFooter = False Then
            Dim objBenefitClaimDetailsFacade As BenefitClaimDetailsFacade = New BenefitClaimDetailsFacade(User)
            Dim n As Integer = -1
            If Not ObjBenefitClaimDetailsTemp Is Nothing Then
                n = objBenefitClaimDetailsFacade.UpdateKeterangan(ObjBenefitClaimDetailsTemp)
            End If
        End If

        If Not listAll Is Nothing Then
            listAll.RemoveAt(tempInteger)
            listAll.Insert(tempInteger, ObjBenefitClaimDetailsTemp)
        End If

        '  objdealerFacade()



        sessHelper.SetSession("DetailSession", listAll)

        ' dgTable.EditItemIndex = -1
        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

        panel2.Attributes("style") = "display:none;"
        panel1.Attributes("style") = "display:;"
        panel3.Attributes("style") = "display:none;"
        panel4.Attributes("style") = "display:none;"
        Panel5.Attributes("style") = "display:;"


        Panel8.Attributes("style") = "display:none;"



    End Sub

    Private Sub EditCommand(ByVal e As DataGridCommandEventArgs, ByVal id As Integer)

        Dim objClaimDetail As BenefitClaimDetails = New BenefitClaimDetailsFacade(User).Retrieve(CInt(id))
        '
        If Not objClaimDetail Is Nothing Then
            If Not lblDelerSession.Visible = True Then 'ktbside
                txtKeterangan.Text = objClaimDetail.DescKtb
            Else 'dealer side
                txtKeterangan.Text = objClaimDetail.DescDealer
            End If

            txtIdDetailForKet.Value = objClaimDetail.ID.ToString

            'Else
            '    Dim listAll As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
            '    For Each item As BenefitClaimDetails In listAll
            '        item.ChassisMaster.ChassisNumber = 
            '    Next
        End If



        'txtKeterangan.Text = objClaimDetail.DescKtb



        panel2.Attributes("style") = "display:none;"
        panel1.Attributes("style") = "display:none;"
        panel3.Attributes("style") = "display:none;"
        panel4.Attributes("style") = "display:none;"
        Panel5.Attributes("style") = "display:none;"


        Panel8.Attributes("style") = "display:;"


        '    dgTable.EditItemIndex = CInt(e.Item.ItemIndex)
        '    dgTable.ShowFooter = False
        '    GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs)

        Dim lblNoRangkaGrid As Label = CType(e.Item.FindControl("lblNoRangkaGrid"), Label)
        Dim lblDecriptionGrid As Label = CType(e.Item.FindControl("lblDecriptionGrid"), Label)
        Dim lblNoMesinGrid As Label = CType(e.Item.FindControl("lblNoMesinGrid"), Label)
        Dim lblNoGrid As Label = CType(e.Item.FindControl("lblNoGrid"), Label)


        'Dim objDomain2 As BenefitMasterDetail = CType(e.Item.DataItem, BenefitMasterDetail)


        'Delete item yang index nya itu sesuai dengan index item yg di filter
        Dim list As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        Dim _DetailDelete As New BenefitClaimDetails
        For Each item As BenefitClaimDetails In list
            If lblNoRangkaGrid.Text.Replace(" ", "") = item.ChassisMaster.ChassisNumber.Replace(" ", "") _
                And lblDecriptionGrid.Text.Replace(" ", "") = item.ChassisMaster.VechileColor.MaterialDescription.Replace(" ", "") _
                And lblNoMesinGrid.Text.Replace(" ", "") = item.ChassisMaster.EngineNumber.ToString.Replace(" ", "") _
                And lblNoGrid.Text = item.NoBaris Then
                _DetailDelete = item
                Exit For
            End If
        Next
        list.Remove(_DetailDelete)
        ReloadGridAfterDeletingRow(list)
        Dim listFromAddCommand As ArrayList = CType(sessHelper.GetSession("listFromAddCommand"), ArrayList)
        If Not IsNothing(listFromAddCommand) Then
            listFromAddCommand.Remove(_DetailDelete)
            sessHelper.SetSession("listFromAddCommand", listFromAddCommand)
        End If
        'GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

    End Sub

    Private Sub ReloadGridAfterDeletingRow(ByVal newList As ArrayList)
        lblotal.Text = 0
        lblNilaiClaim.Text = 0
        lblPph.Text = 0
        lblPpn.Text = 0
        lblTotalReduksi.Text = 0

        If newList.Count = 0 Then
            sessHelper.SetSession("DetailSession", newList)
            dgTable.DataSource = newList
            dgTable.DataBind()

        Else
            sessHelper.RemoveSession("DetailSession")
            Dim i As Integer = 0
            Dim oDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            Dim varLeasingID As Integer = 0
            Dim objFacade As New BenefitClaimDetailsFacade(User)
            Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")
            Dim list As ArrayList = New ArrayList()
            Dim listForValidating As ArrayList = New ArrayList()
            listForValidating.AddRange(newList)
            If ddlLeasing.SelectedValue.ToString() <> "" Then
                varLeasingID = CInt(ddlLeasing.SelectedValue)
            End If

            sessHelper.SetSession("DetailSession", New ArrayList)
            Dim j As Integer = 0
            For Each item As BenefitClaimDetails In listForValidating
                Dim objBenefitClaimDetails_afterValidation As BenefitClaimDetails = New BenefitClaimDetails()
                sessHelper.RemoveSession("addDetailSession")
                objBenefitClaimDetails_afterValidation = objFacade.ChassisNumberValidation(oDealer.ID, CInt(item.ChassisMaster.ID), CShort(txtIdDetailMaster.Value), CInt(valuedddlPilihanClaim(0)), list, varLeasingID, False, CInt(IIf(hfIIdEvent.Value = "", 0, hfIIdEvent.Value)), item.ID)

                objBenefitClaimDetails_afterValidation.DescDealer = item.DescDealer
                objBenefitClaimDetails_afterValidation.NoBaris = j + 1

                sessHelper.SetSession("addDetailSession", objBenefitClaimDetails_afterValidation)
                GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
                list.Add(objBenefitClaimDetails_afterValidation)
                j += 1
            Next

            sessHelper.SetSession("DetailSession", list)
            dgTable.DataSource = list
            dgTable.DataBind()
        End If

    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If

        Dim list As ArrayList
        If Not sessHelper.GetSession("DetailSession") Is Nothing Then
            list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        Else
            list = New ArrayList
        End If

        '  Dim aaa As Decimal = 0
        Dim aaa As String = ""
        Dim isError As Boolean = False

        If txtIdDetailMaster.Value = "" Then
            MessageBox.Show("Pilih Referensi Claim terlebih dahulu.")
            Return
        End If

        Dim hfNoRangkaGrid As TextBox = e.Item.FindControl("hfNoRangkaGrid")

        If hfNoRangkaGrid.Text = "" Then
            MessageBox.Show("Pilih Nomor Rangka sesuai list.")
            Return
        End If


        'add by anh 20160220

        Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")

        If valuedddlPilihanClaim(2).ToString() = "1" AndAlso (txtRegEvent.Text.Trim() = "" OrElse hfIIdEvent.Value = "") Then
            MessageBox.Show("Pilih No Reg Event  terlebih dahulu.")
            Return
        End If

        Dim oDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        Dim varLeasingID As Integer = 0
        If ddlLeasing.SelectedValue.ToString() <> "" Then
            varLeasingID = CInt(ddlLeasing.SelectedValue)
        End If

        Dim objFacade As New BenefitClaimDetailsFacade(User)
        'objDomainDetail = objFacade.ChassisNumberValidation(CInt(hfNoRangkaGrid.Text), CShort(txtIdDetailMaster.Value), CInt(ddlPilihanClaim.Text), CInt(ddlLeasing.SelectedValue))
        'objDomainDetail = objFacade.ChassisNumberValidation(oDealer.ID, CInt(hfNoRangkaGrid.Text), CShort(txtIdDetailMaster.Value), CInt(valuedddlPilihanClaim(0)), list, CInt(ddlLeasing.SelectedValue), False, CInt(IIf(hfIIdEvent.Value = "", 0, hfIIdEvent.Value)))

        objDomainDetail = objFacade.ChassisNumberValidation(oDealer.ID, CInt(hfNoRangkaGrid.Text), CShort(txtIdDetailMaster.Value), CInt(valuedddlPilihanClaim(0)), list, varLeasingID, False, CInt(IIf(hfIIdEvent.Value = "", 0, hfIIdEvent.Value)))

        Dim txtKetDealer As TextBox = e.Item.FindControl("txtKetDealer")
        objDomainDetail.DescDealer = txtKetDealer.Text
        sessHelper.SetSession("addDetailSession", objDomainDetail)

        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

        Dim listFromAddCommand As ArrayList = sessHelper.GetSession("listFromAddCommand")
        If IsNothing(listFromAddCommand) Then
            listFromAddCommand = New ArrayList()
            sessHelper.SetSession("listFromAddCommand", listFromAddCommand)
        End If
        listFromAddCommand.Add(objDomainDetail)


        'Dim oSCFac As New sp_checkInputClaimFacade(User)
        'Dim aSCs As ArrayList

        'Dim oEndCustomerID As Integer = 0
        'Dim oBenefitClaimDetailID As Integer = 0
        'Dim oBenefitMasterHeaderID As Integer = 0
        'Dim oBenefitTypeID As Integer = 0
        'Dim oLeasingCompanyID As Integer = 0
        'Dim oIsDebug As Integer = 0

        'Dim objChassisMasterFacadeX As ChassisMasterFacade = New ChassisMasterFacade(User)
        'Dim objChassisMasterX As ChassisMaster = objChassisMasterFacadeX.Retrieve(CInt(hfNoRangkaGrid.Text))
        'If objChassisMasterX.ID > 0 Then
        '    oEndCustomerID = objChassisMasterX.EndCustomer.ID
        'End If

        'Dim objBenefitMasterHeaderX As BenefitMasterHeader = New BenefitMasterHeader
        'Dim objBenefitMasterHeaderFacadeX As BenefitMasterHeaderFacade = New BenefitMasterHeaderFacade(User)
        'objBenefitMasterHeaderX = objBenefitMasterHeaderFacadeX.Retrieve(CShort(txtIdDetailMaster.Value))
        'If objBenefitMasterHeaderX.ID > 0 Then
        '    oBenefitMasterHeaderID = objBenefitMasterHeaderX.ID
        'End If

        'Dim objBenefitMasterDetailX As BenefitMasterDetail = New BenefitMasterDetail
        'For Each item As BenefitMasterDetail In objBenefitMasterHeaderX.BenefitMasterDetails
        '    If Not item Is Nothing Then

        '        If item.BenefitType.ID = CInt(ddlPilihanClaim.SelectedValue) And _
        '           item.AssyYear = objChassisMasterX.ProductionYear And ((objChassisMasterX.EndCustomer.ValidateTime >= item.FakturValidationStart And _
        '             objChassisMasterX.EndCustomer.ValidateTime <= item.FakturValidationEnd)) Then
        '            Dim cekVechille As Boolean = False
        '            Dim cekLeasing As Boolean = False
        '            For Each itemVehicleType As BenefitMasterVehicleType In item.BenefitMasterVehicleTypes
        '                If itemVehicleType.VechileType.ID = objChassisMasterX.VechileColor.VechileType.ID Then
        '                    cekVechille = True
        '                    Exit For
        '                End If
        '            Next
        '            If item.BenefitMasterLeasings.Count > 1 Then
        '                For Each itemLeasing As BenefitMasterLeasing In item.BenefitMasterLeasings
        '                    If itemLeasing.LeasingCompany.ID = CInt(ddlLeasing.SelectedValue) Then
        '                        cekLeasing = True
        '                        Exit For
        '                    End If
        '                Next
        '            Else
        '                cekLeasing = True
        '            End If


        '            If cekVechille = True And cekLeasing = True Then
        '                objBenefitMasterDetailX = item
        '                Exit For
        '            End If

        '        End If


        '    End If
        'Next

        'Dim objBenefitTypeFacadeX As BenefitTypeFacade = New BenefitTypeFacade(User)
        'Dim objBenefitTypeX As BenefitType = objBenefitTypeFacadeX.Retrieve(CShort(ddlPilihanClaim.Text))
        'If objBenefitTypeX.ID > 0 Then
        '    oBenefitTypeID = objBenefitTypeX.ID
        'End If

        'If ddlPilihanClaim.SelectedItem.Text.ToLower.IndexOf("leas") > -1 Then
        '    oLeasingCompanyID = CInt(ddlLeasing.SelectedValue)
        'End If

        'aSCs = oSCFac.RetrieveFromSP_(oEndCustomerID, oBenefitClaimDetailID, oBenefitMasterHeaderID, oBenefitTypeID, oLeasingCompanyID, oIsDebug)

        'Dim objInputClaim As sp_checkInputClaim = CType(aSCs(0), sp_checkInputClaim)

        'If Not objInputClaim Is Nothing Then
        '    objDomainDetail = New BenefitClaimDetails
        '    objDomainDetail.ChassisMaster = objChassisMasterX
        '    objDomainDetail.BenefitMasterDetail = objBenefitMasterDetailX

        '    If objInputClaim.IsValid <> 1 Then
        '        'MessageBox.Show(objInputClaim.Message)
        '        objDomainDetail.ErrorMessage = objInputClaim.Message
        '    End If

        'End If

        'sessHelper.SetSession("addDetailSession", objDomainDetail)
        'GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

        Return
        'end add by anh 20160220


        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        Dim objChassisMaster As ChassisMaster = objChassisMasterFacade.Retrieve(CInt(hfNoRangkaGrid.Text))


        Dim objBenefitTypeFacade As BenefitTypeFacade = New BenefitTypeFacade(User)
        Dim objBenefitType As BenefitType = objBenefitTypeFacade.Retrieve(CShort(valuedddlPilihanClaim(0)))




        objDomainDetail = New BenefitClaimDetails
        objDomainDetail.ChassisMaster = objChassisMaster







        'Dim list As ArrayList
        'If Not sessHelper.GetSession("DetailSession") Is Nothing Then
        '    list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        'Else
        '    list = New ArrayList
        'End If



        Dim isExist As Boolean = New BenefitClaimDetailsFacade(User).isExist(list, CInt(hfNoRangkaGrid.Text))
        If isExist = True Then
            MessageBox.Show("Nomor Rangka sudah ada.")

            isError = True
            objDomainDetail.ErrorMessage = "Nomor Rangka sudah ada"
            'GoTo Line1
        End If

        Dim objBenefitMasterHeader As BenefitMasterHeader = New BenefitMasterHeader
        Dim objBenefitMasterHeaderFacade As BenefitMasterHeaderFacade = New BenefitMasterHeaderFacade(User)
        objBenefitMasterHeader = objBenefitMasterHeaderFacade.Retrieve(CShort(txtIdDetailMaster.Value))
        Dim noRegClaim As String = ""

        Dim valueLeasing As Integer
        'If ddlPilihanClaim.SelectedItem.Text.ToLower.IndexOf("leas") > -1 Then
        If valuedddlPilihanClaim(1) = 1 Then
            valueLeasing = CInt(ddlLeasing.SelectedValue)
        End If

        'check chassing masuk ke master detail benefit yg mana
        Dim objBenefitMasterDetail As BenefitMasterDetail = New BenefitMasterDetail
        For Each item As BenefitMasterDetail In objBenefitMasterHeader.BenefitMasterDetails
            If Not item Is Nothing Then

                ' If item.BenefitType.ID = CInt(ddlPilihanClaim.SelectedValue) And _
                If item.BenefitType.ID = CInt(valuedddlPilihanClaim(0)) And _
                   item.AssyYear = objChassisMaster.ProductionYear And ((objChassisMaster.EndCustomer.ValidateTime.Date >= item.FakturValidationStart And _
                     objChassisMaster.EndCustomer.ValidateTime.Date <= item.FakturValidationEnd)) Then
                    Dim cekVechille As Boolean = False
                    Dim cekLeasing As Boolean = False
                    For Each itemVehicleType As BenefitMasterVehicleType In item.BenefitMasterVehicleTypes
                        If itemVehicleType.VechileType.ID = objChassisMaster.VechileColor.VechileType.ID Then
                            cekVechille = True
                            Exit For
                        End If
                    Next
                    If item.BenefitMasterLeasings.Count > 1 Then
                        For Each itemLeasing As BenefitMasterLeasing In item.BenefitMasterLeasings
                            If itemLeasing.LeasingCompany.ID = CInt(ddlLeasing.SelectedValue) Then
                                cekLeasing = True
                                Exit For
                            End If
                        Next
                    Else
                        cekLeasing = True
                    End If


                    If cekVechille = True And cekLeasing = True Then
                        objBenefitMasterDetail = item
                        Exit For
                    End If

                End If


            End If
        Next

        If Not objBenefitMasterDetail.ID = Nothing Then

            objDomainDetail.BenefitMasterDetail = objBenefitMasterDetail

            If objBenefitType.WSDiscount = 1 Then
                If objBenefitMasterDetail.WSDiscount = 0 Then
                    If Not objChassisMaster.DiscountAmount = 0 Then
                        MessageBox.Show("Nomor Rangka tidak dapat diajukan claim karena sudah mendapatkan diskon.")

                        isError = True
                        objDomainDetail.ErrorMessage = "Nomor Rangka tidak dapat diajukan claim karena sudah mendapatkan diskon"
                    End If
                End If
            End If


            If isError = False Then
                Dim checkAnotherClaim As Short = New BenefitClaimDetailsFacade(User).checkAnotherClaim(txtFormula.Value, _
                               objChassisMaster, objBenefitMasterDetail)

                If checkAnotherClaim = 1 Then
                    MessageBox.Show("Nomor Rangka masih digunakan di claim lain.")

                    objDomainDetail.ErrorMessage = "Nomor Rangka masih digunakan di claim lain. " & noRegClaim & ""
                    isError = True
                ElseIf checkAnotherClaim = 2 Then
                    MessageBox.Show("Nomor Rangka tidak sesuai dengan formula Benefit.")

                    objDomainDetail.ErrorMessage = "Nomor Rangka tidak sesuai dengan formula Benefit"
                    isError = True
                ElseIf checkAnotherClaim = 3 Then
                    MessageBox.Show("formula Benefit sudah full.")

                    objDomainDetail.ErrorMessage = "formula Benefit sudah full"
                    isError = True
                End If
            End If
        Else
            MessageBox.Show("Nomor Rangka tidak sesuai dengan formula Benefit.")

            objDomainDetail.ErrorMessage = "Nomor Rangka tidak sesuai dengan formula Benefit"
        End If




        'line1:


        ' If isError = False Then


        'objDomainDetail.DetailStatus = 0
        sessHelper.SetSession("addDetailSession", objDomainDetail)
        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))
        '  End If













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




    Private Sub GenerateToGrid(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitClaimDetails = CType(e.Item.DataItem, BenefitClaimDetails)

            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then

                    Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
                    Dim lnkbtnPrint As LinkButton = e.Item.FindControl("lnkbtnPrint")
                    If Convert.ToString(sessHelper.GetSession("Status")) = "View" _
                       Or Convert.ToString(sessHelper.GetSession("Status")) = "Delete" _
                       Or Convert.ToString(sessHelper.GetSession("Status")) = "ViewSave" Then
                        lnkbtnDelete.Visible = False
                    End If



                    ' lnkbtnPrint.Attributes("idobjChassisMaster") = objChassisMaster.ID.ToString
                    lnkbtnPrint.Attributes("onclick") = "ShowRecomendation('" & objDomain2.ChassisMaster.ID.ToString & _
                        "','" & objDomain2.RecLetterRegNo & "')"

                    lnkbtnPrint.Attributes("onclick") = "ShowRecomendation('" & objDomain2.ChassisMaster.ID.ToString & _
                        "','" & objDomain2.RecLetterRegNo & "','" & objDomain2.ID.ToString() & "' );"

                    Dim lblNoGrid As Label = CType(e.Item.FindControl("lblNoGrid"), Label)
                    Dim lblNoRangkaGrid As Label = CType(e.Item.FindControl("lblNoRangkaGrid"), Label)
                    Dim lblDecriptionGrid As Label = CType(e.Item.FindControl("lblDecriptionGrid"), Label)
                    Dim lblNoMesinGrid As Label = CType(e.Item.FindControl("lblNoMesinGrid"), Label)
                    Dim lblFakturOpenGrid As Label = CType(e.Item.FindControl("lblFakturOpenGrid"), Label)
                    Dim lblValidasiFakturGrid As Label = CType(e.Item.FindControl("lblValidasiFakturGrid"), Label)
                    Dim lblCutomerGrid As Label = CType(e.Item.FindControl("lblCutomerGrid"), Label)
                    Dim lblKotaGrid As Label = CType(e.Item.FindControl("lblKotaGrid"), Label)


                    Try
                        lblNoGrid.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString
                        lblNoRangkaGrid.Text = objDomain2.ChassisMaster.ChassisNumber
                        lblDecriptionGrid.Text = objDomain2.ChassisMaster.VechileColor.MaterialDescription
                        lblNoMesinGrid.Text = objDomain2.ChassisMaster.EngineNumber
                        objDomain2.NoBaris = lblNoGrid.Text
                    Catch ex As Exception

                    End Try

                    Try
                        If Not objDomain2.ChassisMaster.EndCustomer Is Nothing Then
                            If CInt(objDomain2.ChassisMaster.EndCustomer.OpenFakturDate.ToString("yyyy")) > 1990 Then
                                lblFakturOpenGrid.Text = objDomain2.ChassisMaster.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
                            Else
                                lblFakturOpenGrid.Text = ""
                            End If
                        End If

                        If Not objDomain2.ChassisMaster.EndCustomer Is Nothing Then
                            If CInt(objDomain2.ChassisMaster.EndCustomer.ValidateTime.ToString("yyyy")) > 1990 Then
                                lblValidasiFakturGrid.Text = objDomain2.ChassisMaster.EndCustomer.ValidateTime.ToString("dd/MM/yyyy")
                            Else
                                lblValidasiFakturGrid.Text = ""
                            End If
                        End If

                        lblCutomerGrid.Text = objDomain2.ChassisMaster.EndCustNameText
                        lblKotaGrid.Text = objDomain2.ChassisMaster.AddressText

                    Catch ex As Exception

                    End Try


                    Dim lblStatusGrid As Label = CType(e.Item.FindControl("lblStatusGrid"), Label)
                    If objDomain2.DetailStatus = 1 Then
                        lblStatusGrid.Text = "OK"
                    ElseIf objDomain2.DetailStatus = 2 Then
                        lblStatusGrid.Text = "Tidak"
                    End If


                    Dim lblNilaiGrid As Label = CType(e.Item.FindControl("lblNilaiGrid"), Label)
                    Dim lblDurasiGrid As Label = CType(e.Item.FindControl("lblDurasiGrid"), Label)
                    Dim lblMaxClaimGrid As Label = CType(e.Item.FindControl("lblMaxClaimGrid"), Label)
                    Dim tempEventDate As Date
                    '  Dim objBenefitMasterDetail As BenefitMasterDetail = New BenefitMasterDetail
                    ' Dim objBenefitMasterHeader As BenefitMasterHeader = New BenefitMasterHeader
                    Try
                        lblNilaiGrid.Text = objDomain2.BenefitMasterDetail.Amount.ToString("#,##0.00")
                        lblMaxClaimGrid.Text = objDomain2.BenefitMasterDetail.MaxClaim.ToString
                    Catch ex As Exception

                    End Try

                    Dim lblKeterangan As Label = CType(e.Item.FindControl("lblKeterangan"), Label)
                    lblKeterangan.Text = objDomain2.ErrorMessage
                    lblDurasiGrid.Text = ""
                    Try
                        If Not objDomain2.ChassisMaster.EndCustomer Is Nothing Then
                            lblDurasiGrid.Text = DateTime.Now.Subtract(objDomain2.ChassisMaster.EndCustomer.ValidateTime).Days.ToString
                        End If

                        If Not objDomain2.BenefitClaimHeader Is Nothing Then
                            If Not objDomain2.BenefitClaimHeader.BenefitEventHeader Is Nothing Then
                                tempEventDate = objDomain2.BenefitClaimHeader.BenefitEventHeader.EventDate
                                If Not objDomain2.ChassisMaster.EndCustomer Is Nothing Then
                                    Dim ObjF As ArrayList = New ArrayList
                                    ObjF = New sp_checkInputClaimFacade(User).RetrieveKTP(objDomain2.ChassisMaster.EndCustomer.ID, objDomain2.BenefitClaimHeader.BenefitEventHeader.ID)
                                    If Not IsNothing(ObjF) AndAlso ObjF.Count > 0 Then
                                        lblKeterangan.Text = CType(ObjF(0), sp_checkInputClaim).Message
                                    End If
                                End If


                            End If
                        End If
                    Catch ex As Exception

                    End Try






                    'If Not tempEventDate = Nothing Then
                    '    Dim tempsurasi As Integer
                    '    tempsurasi = objDomain2.ChassisMaster.DODate.Subtract(tempEventDate).Days
                    '    If tempsurasi > 0 Then
                    '        lblDurasiGrid.Text = tempsurasi.ToString
                    '    End If

                    'End If






                    Dim lblKetDealer As Label = CType(e.Item.FindControl("lblKetDealer"), Label)
                    lblKetDealer.Text = objDomain2.DescDealer


                    Dim lblKetKtb As Label = CType(e.Item.FindControl("lblKetKtb"), Label)
                    lblKetKtb.Text = objDomain2.DescKtb


                    Dim lnkKet As LinkButton = CType(e.Item.FindControl("lnkKet"), LinkButton)


                    If Not objDomain2.ErrorMessage = String.Empty Then
                        e.Item.BackColor = Color.Red


                        lnkbtnPrint.Visible = False

                    End If


                    lnkKet.Visible = False

                    If dgTable.ShowFooter = False Then
                        lnkbtnDelete.Visible = False
                        lnkbtnPrint.Visible = lnkbtnDelete.Visible

                        If Not lblDelerSession.Visible = True Then 'ktbside
                            If objDomain2.BenefitClaimHeader.Status = 2 Or objDomain2.BenefitClaimHeader.Status = 4 Then
                                lnkKet.Visible = True
                            End If
                        Else 'dealer side
                            'If objDomain2.ErrorMessage = "" Then
                            '    '  lnkKet.Visible = True
                            '    If objDomain2.BenefitClaimHeader.Status = 0 Or objDomain2.BenefitClaimHeader.Status = 1 Then
                            '        lnkKet.Visible = True
                            '    End If
                            'End If
                        End If
                    Else
                        ' lnkKet.Visible = True

                        If Not lblDelerSession.Visible = True Then 'ktbside

                        Else 'dealer side
                            If objDomain2.ErrorMessage = "" Then
                                '  lnkKet.Visible = True
                                ' If objDomain2.BenefitClaimHeader.Status = 0 Or objDomain2.BenefitClaimHeader.Status = 1 Then
                                ' lnkKet.Visible = True
                                'End If
                            End If
                        End If
                    End If

                    Try
                        If Not objDomain2.BenefitMasterDetail.BenefitType Is Nothing Then
                            If Not objDomain2.BenefitMasterDetail.BenefitType.LeasingBox = 1 Then
                                lnkbtnPrint.Visible = False
                            Else
                                If Not objDomain2.BenefitClaimHeader Is Nothing Then
                                    If Not objDomain2.BenefitClaimHeader.LeasingCompany Is Nothing Then
                                        If objDomain2.BenefitClaimHeader.LeasingCompany.ID.ToString = "1" Then
                                            lnkbtnPrint.Visible = False
                                        Else
                                            If objDomain2.BenefitClaimHeader.LeasingCompany.LeasingName.Trim.ToUpper.Contains("DIPOSTARFINANCE") Then
                                                lnkbtnPrint.Visible = False
                                            Else
                                                lnkbtnPrint.Visible = True
                                            End If
                                        End If
                                    End If
                                Else
                                    If ddlLeasing.SelectedValue.ToString = "1" Then
                                        lnkbtnPrint.Visible = False
                                    Else
                                        If ddlLeasing.SelectedItem.Text.Replace(" ", "").ToUpper.ToUpper.Contains("DIPOSTARFINANCE") Then
                                            lnkbtnPrint.Visible = False
                                        Else
                                            lnkbtnPrint.Visible = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        lnkbtnPrint.Visible = False
                    End Try



                    Dim cbAllGrid As CheckBox = CType(e.Item.FindControl("cbAllGrid"), CheckBox)
                    If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then

                        If Not lblDelerSession.Visible = True Then 'ktbside
                            cbAllGrid.Visible = True
                        Else
                            cbAllGrid.Visible = False
                        End If


                    Else
                        If Not lblDelerSession.Visible = True Then 'ktbside
                            cbAllGrid.Visible = True
                        Else
                            cbAllGrid.Visible = False
                        End If

                    End If



                Else 'for edit


                End If


            End If
        End If
    End Sub

    Private Sub GenerateToFooter(ByVal e As DataGridItemEventArgs)

        Dim lblNoRangkaGrid As Label = CType(e.Item.FindControl("lblNoRangkaGrid"), Label)
        lblNoRangkaGrid.Attributes("onclick") = "ShowNoRangkaGrid();"

        If txtIdDetailMaster.Value = "" Then
            Dim Linkbutton1 As LinkButton = CType(e.Item.FindControl("Linkbutton1"), LinkButton)
            Linkbutton1.Attributes("style") = "display:none;"
        End If




    End Sub





    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        'Response.Redirect("~/Benefit/FrmBenefitClaimList.aspx")
        RemoveALLSession()
        ' Response.Redirect("FrmBenefitList.aspx")

        If Convert.ToString(Request.QueryString("Mode")) = "View" Or
             Convert.ToString(Request.QueryString("Mode")) = "ViewSave" Or
               Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
               Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
            Response.Redirect("FrmBenefitClaimList.aspx?SessionForDisplayDetail=1")
        Else
            Response.Redirect("FrmInputClaim.aspx")
        End If
    End Sub




    Private Sub RemoveALLSession()
        'sessHelper.RemoveSession("OLDSPLDealer")
        sessHelper.RemoveSession("IDBenefitListHeader")
        sessHelper.RemoveSession("DetailSession")
        sessHelper.RemoveSession("addDetailSession")
        sessHelper.RemoveSession("OldDetailSession")
        sessHelper.RemoveSession("OldDealerSession")
        sessHelper.RemoveSession("MasterDetailSession")
        sessHelper.RemoveSession("DetailClaimExcelSession")
        sessHelper.RemoveSession("EventListSession")
        sessHelper.RemoveSession("idBenefitMasterDetailSession")

        sessHelper.RemoveSession("idMasterDetailSession")
        sessHelper.RemoveSession("listFromAddCommand")
    End Sub


    Private Sub GetValueFromDataBase(ByVal id As Integer)
        Dim decAmountDeducted As Decimal = 0
        Dim sb As StringBuilder = New StringBuilder

        Dim Obj As BenefitClaimHeader = objDomainFacade.Retrieve(id)
        If Not Obj Is Nothing Then

            Dim critBCR As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critBCR.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.ID", MatchType.Exact, Obj.ID))
            Dim bcr As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).Retrieve(critBCR).Cast(Of BenefitClaimReceipt)().SingleOrDefault()
            Obj.HasReceipt = If(Not IsNothing(bcr), True, False)
            Obj.TotalNilaiClaim = If(Not IsNothing(bcr), bcr.ReceiptAmountDeducted, 0)
            Obj.TotalPPh = If(Not IsNothing(bcr), bcr.PPHTotal, 0)
            Obj.TotalPPn = If(Not IsNothing(bcr), bcr.VATTotal, 0)

            icClaimDate.Value = Obj.ClaimDate
            txtRegClaimNo.Text = Obj.ClaimRegNo
            txtRegEvent.Text = ""
            hfIIdEvent.Value = ""
            If Not Obj.BenefitEventHeader Is Nothing Then
                txtRegEvent.Text = Obj.BenefitEventHeader.EventRegNo
                hfIIdEvent.Value = Obj.BenefitEventHeader.ID.ToString
            End If

            If Not Obj.LeasingCompany Is Nothing Then
                ddlLeasing.SelectedValue = Obj.LeasingCompany.ID.ToString
            End If

            'Dim alBenefitMasterDealers As ArrayList = Obj.BenefitMasterDetail.BenefitMasterHeader.BenefitMasterDealers
            'Dim idDealer As String = ""
            'For Each el As BenefitMasterDealer In alBenefitMasterDealers
            '    idDealer += el.Dealer.DealerCode + "; "
            'Next
            'txtKodeDealer.Text = idDealer
            txtKodeDealer.Text = Obj.Dealer.DealerCode

            For Each el As BenefitClaimDetails In Obj.BenefitClaimDetailss
                If Not el.BenefitMasterDetail.BenefitMasterHeader Is Nothing Then
                    txtIdDetailMaster.Value = el.BenefitMasterDetail.BenefitMasterHeader.ID.ToString
                    txtIdDetailMasterShow.Text = el.BenefitMasterDetail.BenefitMasterHeader.NomorSurat
                End If
            Next

            'txtIdDetailMaster.Value = Obj.BenefitMasterDetail.BenefitMasterHeader.ID.ToString

            'If Not Obj.BenefitMasterDetail.BenefitMasterHeader Is Nothing Then
            '    txtIdDetailMasterShow.Text = Obj.BenefitMasterDetail.BenefitMasterHeader.NomorSurat
            'End If

            ' ddlPilihanClaim.SelectedValue = Obj.BenefitMasterDetail.BenefitType.ID.ToString
            ddlPilihanClaim.SelectedValue = Obj.BenefitType.ID.ToString & ";" & Obj.BenefitType.LeasingBox.ToString & ";" & Obj.BenefitType.EventValidation.ToString

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDeductedHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BenefitClaimDeductedHistory), "BenefitClaimHeader.ID", MatchType.Exact, Obj.ID))
            Dim aggregateSum As Aggregate = New Aggregate(GetType(BenefitClaimDeductedHistory), "AmountDeducted", AggregateType.Sum)
            decAmountDeducted = IsDBNull(New BenefitClaimDeductedHistoryFacade(User).RetrieveScalar(crit, aggregateSum), 0)

            Dim crit2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDeductedHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit2.opAnd(New Criteria(GetType(BenefitClaimDeductedHistory), "BenefitClaimHeader.ID", MatchType.Exact, Obj.ID))
            Dim arrDeductHistory As ArrayList = New BenefitClaimDeductedHistoryFacade(User).Retrieve(crit2)
            If Not IsNothing(arrDeductHistory) AndAlso arrDeductHistory.Count > 0 Then
                For Each ObjDeductHistory As BenefitClaimDeductedHistory In arrDeductHistory
                    sb.Append(ObjDeductHistory.BenefitClaimDeducted.DSFLeasingClaim.ChassisMaster.ChassisNumber + "<br />")
                Next
            End If

            txtMMKSINotes.Text = Obj.MMKSINotes
        End If
        lblTotalReduksi.Text = decAmountDeducted.ToString("#,##0.00")
        lblChassisNumberReduksi.Text = sb.ToString

        Dim list As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        If list Is Nothing Then
            If Convert.ToString(Request.QueryString("Mode")) = "View" Or
                Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
                  Convert.ToString(Request.QueryString("Mode")) = "ViewSave" Or
                Convert.ToString(Request.QueryString("Mode")) = "Delete" Then

                list = Obj.BenefitClaimDetailss

                If sessHelper.GetSession("OldDetailSession") Is Nothing Then
                    sessHelper.SetSession("OldDetailSession", list)
                End If

                If sessHelper.GetSession("OldDealerSession") Is Nothing Then
                    sessHelper.SetSession("OldDealerSession", Obj.BenefitEventHeader)
                End If

                If list Is Nothing Then
                    list = New ArrayList
                End If
            Else
                list = New ArrayList
            End If
        End If

        Dim list1 As BenefitClaimDetails = CType(sessHelper.GetSession("addDetailSession"), BenefitClaimDetails)
        If Not list1 Is Nothing Then
            list.Add(list1)
        End If

        Dim list2 As ArrayList = CType(sessHelper.GetSession("DetailClaimExcelSession"), ArrayList)
        If Not list2 Is Nothing AndAlso list2.Count > 0 Then
            list = New ArrayList
            For Each Items As BenefitClaimDetails In list2
                list.Add(Items)
            Next
        End If

        Dim totalDetail As Integer = 0
        Dim totalnialidetail As Decimal = 0
        If Not txtIdDetailMaster.Value = "" Then

            Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")

            Dim objBenefit As BenefitType = New BenefitTypeFacade(User).Retrieve(CShort(valuedddlPilihanClaim(0)))
            If Convert.ToString(Request.QueryString("Mode")) = "View" Then
                If lblDelerSession.Visible = False Then 'ktb side
                    Dim isEnablePanel As Boolean = CType(New AppConfigFacade(User).Retrieve("BenefitClaim.IsEnablePanelStatusOK").Value, Boolean)
                    If isEnablePanel And (Obj.Status = 2 Or Obj.Status = 3 Or Obj.Status = 4) Then
                        Panel5.Visible = True
                    Else
                        If CShort(objBenefit.LeasingBox) = 1 Then
                            Panel5.Visible = False
                        Else
                            'If Obj.Status = 2 Or Obj.Status = 3 Or Obj.Status = 4 Or Obj.Status = 5 Then
                            If Obj.Status = 2 Or Obj.Status = 3 Or Obj.Status = 4 Then
                                Panel5.Visible = True
                            End If

                        End If
                    End If

                    'If objBenefit.Name.ToLower.Contains("cashb") = True Then
                    '    Panel5.Visible = True
                    'Else
                    '    Panel5.Visible = False
                    'End If
                End If
            End If

            For Each el As BenefitClaimDetails In list
                totalDetail = totalDetail + 1
                'totalnialidetail = totalnialidetail + objBenefitMasterDetail.Amount
                If Not el.BenefitMasterDetail.ID = 0 Then
                    totalnialidetail = totalnialidetail + el.BenefitMasterDetail.Amount
                End If
            Next
            lblotal.Text = totalDetail.ToString & " unit"
            'If Not IsNothing(Obj) Then
            '    If Obj.HasReceipt Then
            '        totalnialidetail = Obj.TotalNilaiClaim
            '    End If
            'End If
            lblNilaiClaim.Text = totalnialidetail.ToString("#,##0.00")
            Dim nilaiPph As Decimal = 0

            Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            Dim fakturPajakDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            If Not Obj Is Nothing Then
                For Each el As BenefitClaimReceipt In Obj.BenefitClaimReceipts
                    cutOffDate = el.ReceiptDate
                    fakturPajakDate = el.FakturPajakDate
                Next

                If cutOffDate.Year < 1900 Then
                    cutOffDate = Obj.ClaimDate
                End If
            Else
                cutOffDate = DateTime.Now.Date
            End If

            Dim pphVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
            Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

            If Not totalnialidetail = 0 Then
                Dim pph As Decimal = 0
                Dim ppn As Decimal = 0
                'pph = (0.1 * (totalnialidetail))

                If objBenefit.LeasingBox = 1 Then
                    ' If objBenefit.Name.ToLower.Contains("leasi") = True Then
                    ' pph = (0.15 * (totalnialidetail))

                    'pph = Math.Round(((totalnialidetail / (1 - 0.15)) - totalnialidetail))
                    'ppn = Math.Round((0.1 * (totalnialidetail + pph)))

                    pph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pphVal, total:=totalnialidetail)
                    ppn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=CalcHelper.DPPCalculation(pphVal, totalnialidetail))
                Else
                    ' pph = (0.02 * (totalnialidetail))
                    'pph = Math.Round(((totalnialidetail / (1 - 0.15)) - totalnialidetail))

                    'pph = Math.Round(0.15 * totalnialidetail)

                    pph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pphVal, dpp:=totalnialidetail)
                    ppn = 0 'Math.Round(0.1 * totalnialidetail)
                End If
                'If Not IsNothing(Obj) Then
                '    If Obj.HasReceipt Then
                '        pph = Obj.TotalPPh
                '    End If
                'End If
                lblPph.Text = pph.ToString("#,##0.00")

                '  Dim ppn As Double = (0.15 * totalnialidetail)
                'Dim ppn As Double = Math.Round((0.1 * (totalnialidetail + pph)))
                'If Not IsNothing(Obj) Then
                '    If Obj.HasReceipt Then
                '        ppn = Obj.TotalPPn
                '    End If
                'End If
                lblPpn.Text = ppn.ToString("#,##0.00")
            Else
                'Dim pph As Double = (0.14999999999999999 * totalnialidetail)
                'Dim pph As Double = (0.15 * totalnialidetail)
                Dim pph As Decimal = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pphVal, dpp:=totalnialidetail)
                'If Not IsNothing(Obj) Then
                '    If Obj.HasReceipt Then
                '        pph = Obj.TotalPPh
                '    End If
                'End If
                lblPph.Text = pph.ToString("#,##0.00")
                lblPpn.Text = "0"

            End If

        End If

        sessHelper.SetSession("DetailSession", list)
        sessHelper.RemoveSession("addDetailSession")
        sessHelper.RemoveSession("DetailClaimExcelSession")
        dgTable.DataSource = list
        dgTable.DataBind()
    End Sub

    Private Sub BindRefClaim(ByVal pageIndex As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim oDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.InSet, "(0,-1)"))
        Dim strSql As String = ""
        strSql += "  select distinct bmh.id from BenefitMasterHeader bmh"
        strSql += "  inner join BenefitMasterDetail bmd on bmh.ID = bmd.BenefitMasterHeaderID "
        If (CommonFunction.GetTransControlStatus(oDealer, EnumDealerTransType.DealerTransKind.BenefitCashback) = False) Then
            strSql += " inner join BenefitType bt on bmd.BenefitTypeID = bt.ID and bt.Receiptbox = 0 "
        End If
        strSql += "  left join BenefitMasterDealer bmde on bmh.ID = bmde.BenefitMasterHeaderID "
        strSql += "  left join BenefitMasterLeasing bml on bmd.ID = bml.BenefitMasterDetailID "
        strSql += "  left join Dealer c on bmde.DealerID = c.ID and c.RowStatus = 0 "
        strSql += "  where bmh.status = 0 and bmde.rowStatus=0  "
        Dim value As String = ""
        If txtKodeDealer.Text <> "" Then
            value = ""
            For Each item As String In txtKodeDealer.Text.Replace(" ", "").Split(";")
                If Not item Is Nothing And Not item = "" Then
                    value = value + "'" + item + "',"
                End If
            Next
            value = value + "'--'"
            strSql += " and c.DealerCode in (" & value & ")"
        End If
        If ddlPilihanClaim.SelectedValue <> "" Then
            Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")
            strSql += " and bmd.BenefitTypeID = " & valuedddlPilihanClaim(0)
            'If ddlPilihanClaim.SelectedItem.Text.ToLower.IndexOf("leas") > -1 Then
            If valuedddlPilihanClaim(1) = "1" Then
                strSql += " and bml.LeasingCompanyID = " & ddlLeasing.SelectedValue
            End If
        End If

        criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        _arrList = New BenefitMasterHeaderFacade(User).RetrieveByCriteria(criterias, dgGridDetil.CurrentPageIndex + 1, dgGridDetil.PageSize, totalRow)

        dgGridDetil.VirtualItemCount = totalRow
        sessHelper.SetSession("MasterDetailSession", _arrList)

        dgGridDetil.DataSource = _arrList
        dgGridDetil.DataBind()
        cbRefClaim.Checked = True

        If _arrList.Count < 1 Then
            MessageBox.Show("Data tidak ditemukan.")
        End If

        'If cbRefClaim.Checked = True Then
        panel2.Attributes("style") = "display:;"
        panel1.Attributes("style") = "display:none;"
        panel3.Attributes("style") = "display:none;"
        panel4.Attributes("style") = "display:none;"
        'Else
        'panel1.Attributes("style") = "display:;"
        'panel2.Attributes("style") = "display:none;"
        'End If

        Panel8.Attributes("style") = "display:none;"
    End Sub

    '
    Private Sub btnRefClaim_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefClaim.Click
        dgGridDetil.CurrentPageIndex = 0
        BindRefClaim(dgGridDetil.CurrentPageIndex)
    End Sub
    Private Sub dgGridDetil_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgGridDetil.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitMasterHeader = CType(e.Item.DataItem, BenefitMasterHeader)
            ' If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then
                    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name='rb'>")
                    e.Item.Cells(0).Controls.Add(rdbChoice)

                    Dim lblIDoGridDetil As Label = CType(e.Item.FindControl("lblIDoGridDetil"), Label)
                    lblIDoGridDetil.Text = objDomain2.ID.ToString

                    Dim lblformula As Label = CType(e.Item.FindControl("lblformula"), Label)
                    lblformula.Text = objDomain2.Formula

                    Dim lblNoGridDetil As Label = CType(e.Item.FindControl("lblNoGridDetil"), Label)
                    lblNoGridDetil.Text = (e.Item.ItemIndex + 1 + (dgGridDetil.CurrentPageIndex * dgGridDetil.PageSize)).ToString

                    Dim lblnnosuratGridDetil As Label = CType(e.Item.FindControl("lblnnosuratGridDetil"), Label)
                    lblnnosuratGridDetil.Text = objDomain2.NomorSurat

                    Dim lblNoRegBenefitGridDetil As Label = CType(e.Item.FindControl("lblNoRegBenefitGridDetil"), Label)
                    lblNoRegBenefitGridDetil.Text = objDomain2.BenefitRegNo

                    Dim lbldeskripsiGridDetil As Label = CType(e.Item.FindControl("lbldeskripsiGridDetil"), Label)
                    lbldeskripsiGridDetil.Text = objDomain2.Remarks
                End If
            End If
        End If
    End Sub

    Private Sub BindDataGridNorangka()

        'sessHelper.GetSession("IDBenefitListHeader")
        If Not sessHelper.GetSession("idMasterDetailSession") Is Nothing Then
            If Not sessHelper.GetSession("idMasterDetailSession").ToString = txtIdDetailMaster.Value Then
                sessHelper.RemoveSession("DetailSession")
                Dim _arrList1 As New ArrayList
                dgTable.DataSource = _arrList1
                dgTable.DataBind()
                lblotal.Text = ""
                lblNilaiClaim.Text = lblotal.Text
                lblPpn.Text = lblotal.Text
                lblPph.Text = lblotal.Text
            End If
        End If
        sessHelper.SetSession("idMasterDetailSession", txtIdDetailMaster.Value)

        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.Exact, CInt(txtKodeDealer.Text)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))

        If txtNoRangkaPanel3.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoRangkaPanel3.Text.Trim))
            _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                                   totalRow)
            If _arrList.Count < 1 Then
                MessageBox.Show("Check Nomor Rangka pencarian (" & txtNoRangkaPanel3.Text & ") tidak sesuai dealer code.")
                panel3.Attributes("style") = "display:;"
                panel1.Attributes("style") = "display:none;"
                panel2.Attributes("style") = "display:none;"
                panel4.Attributes("style") = "display:none;"

                Panel8.Attributes("style") = "display:none;"

                dgNoRangka.DataSource = _arrList
                dgNoRangka.DataBind()
                Return
            End If
        End If

        If txtNoMesinPanel3.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Exact, txtNoMesinPanel3.Text.Trim))
            _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                                  totalRow)
            If _arrList.Count < 1 Then
                MessageBox.Show("Check Nomor Mesin pencarian (" & txtNoMesinPanel3.Text & ") tidak sesuai dealer code.")
                panel3.Attributes("style") = "display:;"
                panel1.Attributes("style") = "display:none;"
                panel2.Attributes("style") = "display:none;"
                panel4.Attributes("style") = "display:none;"

                Panel8.Attributes("style") = "display:none;"

                dgNoRangka.DataSource = _arrList
                dgNoRangka.DataBind()
                Return
            End If
        End If

        If txtDeskripsiPanel3.Text <> "" Then
            Dim strSql As String = ""
            strSql += "  select id from VechileColor a "
            strSql += " where  rowstatus = 0 and lower(MaterialDescription) like lower('" & txtDeskripsiPanel3.Text.Trim & "') "
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "VechileColor", MatchType.InSet, "(" & strSql & ")"))

            _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                              totalRow)
            If _arrList.Count < 1 Then
                MessageBox.Show("Check Keterangan pencarian (" & txtDeskripsiPanel3.Text & ") tidak sesuai dealer code.")
                panel3.Attributes("style") = "display:;"
                panel1.Attributes("style") = "display:none;"
                panel2.Attributes("style") = "display:none;"
                panel4.Attributes("style") = "display:none;"
                Panel8.Attributes("style") = "display:none;"

                dgNoRangka.DataSource = _arrList
                dgNoRangka.DataBind()
                Return
            End If
        End If

        If txtCustomerPanel3.Text <> "" Or (cbFakterPanel3.Checked = True And CInt(icFakturPanel3.Value.ToString("yyyy")) > 1900) Then
            Dim strSql As String = ""
            strSql += "  select a.ID from ChassisMaster a "
            strSql += "  inner join EndCustomer b on a.EndCustomerID = b.ID and b.rowstatus = 0"
            strSql += "  inner join Customer c on b.CustomerID = c.ID  and c.rowstatus = 0 "
            strSql += "  where 1=1 and a.rowstatus = 0 "
            If txtCustomerPanel3.Text <> "" Then
                strSql += " and lower(c.name1) like lower('" & txtCustomerPanel3.Text.Trim & "') "
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, "(" & strSql & ")"))
                _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                                 totalRow)
                If _arrList.Count < 1 Then
                    MessageBox.Show("Check Nama Customer pencarian (" & txtCustomerPanel3.Text & ") .")
                    panel3.Attributes("style") = "display:;"
                    panel1.Attributes("style") = "display:none;"
                    panel2.Attributes("style") = "display:none;"
                    panel4.Attributes("style") = "display:none;"

                    Panel8.Attributes("style") = "display:none;"

                    dgNoRangka.DataSource = _arrList
                    dgNoRangka.DataBind()
                    Return
                End If
            End If


            If cbFakterPanel3.Checked = True Then
                If CInt(icFakturPanel3.Value.ToString("yyyy")) > 1900 Then
                    'Dim tgl As New DateTime(CInt(icFakturPanel3.Value.Year), CInt(icFakturPanel3.Value.Month), CInt(icFakturPanel3.Value.Day), 0, 0, 0)

                    'strSql += " and  replace(convert(NVARCHAR,  b.ValidateTime, 103), '/', '-')  ='" & icFakturPanel3.Value.Day & "-" & icFakturPanel3.Value.Month & "-" & icFakturPanel3.Value.Year & "' "
                    strSql += " and  replace(convert(NVARCHAR,  b.ValidateTime, 103), '/', '-')  ='" & icFakturPanel3.Value.ToString("dd-MM-yyyy") & "' "
                    criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, "(" & strSql & ")"))
                    _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                                     totalRow)
                    If _arrList.Count < 1 Then
                        MessageBox.Show("Check Tanggal Faktur pencarian (" & icFakturPanel3.Value.ToString("dd/MM/yyyy") & ").")
                        panel3.Attributes("style") = "display:;"
                        panel1.Attributes("style") = "display:none;"
                        panel2.Attributes("style") = "display:none;"
                        panel4.Attributes("style") = "display:none;"

                        Panel8.Attributes("style") = "display:none;"

                        dgNoRangka.DataSource = _arrList
                        dgNoRangka.DataBind()
                        Return
                    End If
                End If
            End If


        End If

        If Not txtIdDetailMaster.Value = "" Then
            Dim strSql As String = ""
            strSql += "  select a.id from VechileColor a"
            strSql += "  inner join BenefitMasterVehicleType b on "
            strSql += "  a.VechileTypeID = b.VechileTypeID"
            strSql += "   inner join BenefitMasterDetail c on  b.BenefitMasterDetailID = c.id"
            strSql += "   inner join BenefitMasterheader d on  c.BenefitMasterHeaderID = d.id"
            strSql += "  where a.RowStatus = 0 and d.status = 0 "
            strSql += "  and  c.BenefitMasterHeaderID =  '" & txtIdDetailMaster.Value & "' "
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "VechileColor", MatchType.InSet, "(" & strSql & ")"))

            _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                                   totalRow)
            If _arrList.Count < 1 Then
                'MessageBox.Show("Check Benefit No")
                MessageBox.Show("Tipe Kendaraan tidak terdapat pada Benefit ini")
                panel3.Attributes("style") = "display:;"
                panel1.Attributes("style") = "display:none;"
                panel2.Attributes("style") = "display:none;"
                panel4.Attributes("style") = "display:none;"

                Panel8.Attributes("style") = "display:none;"

                dgNoRangka.DataSource = _arrList
                dgNoRangka.DataBind()
                Return
            End If

            If Not hfIIdEvent.Value.ToString = "" Then
                Dim query = "SELECT a.ChassisNumber " &
                            "FROM Chassismaster a " &
                            "JOIN EndCustomer b on b.ID = a.EndCustomerID " &
                            "JOIN Customer c on c.ID = b.CustomerID " &
                            "JOIN CustomerRequest d on d.CUstomerCode = c.Code " &
                            "JOIN SPKDetailCustomer e on e.CustomerRequestID = d.ID " &
                            "JOIN SPKDetail f on f.ID = e.SPKDetailID " &
                            "JOIN SPKHeader g on g.ID = f.SPKHeaderID " &
                            "WHERE g.SPKNumber IN (SELECT z.Remarks " &
                                                    "FROM BenefitEventHeader x " &
                                                    "JOIN BenefitEventDetail y on y.BenefitEventHeaderID = x.ID " &
                                                    "JOIN BenefitParticipant z on z.ID = y.BenefitParticipantID " &
                                                    "WHERE x.ID = " & CShort(hfIIdEvent.Value).ToString & ")"

                criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.InSet, "(" & query & ")"))

                _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                                       totalRow)

                If _arrList.Count < 1 Then
                    'MessageBox.Show("Check Benefit No")
                    MessageBox.Show("Tipe Kendaraan tidak terdapat pada No Reg Event ini")
                    panel3.Attributes("style") = "display:;"
                    panel1.Attributes("style") = "display:none;"
                    panel2.Attributes("style") = "display:none;"
                    panel4.Attributes("style") = "display:none;"

                    Panel8.Attributes("style") = "display:none;"

                    dgNoRangka.DataSource = _arrList
                    dgNoRangka.DataBind()
                    Return
                End If
            End If

            strSql = "  select  a.id from ChassisMaster a"
            strSql += "  inner join EndCustomer b on a.EndCustomerID = b.ID"
            strSql += "  cross join BenefitMasterDetail c"
            strSql += "  where c.BenefitMasterHeaderID = '" & txtIdDetailMaster.Value & "'  "

            '  strSql += "  and a.solddealerid = '" & txtIdDetailMaster.Value & "'"

            Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")

            If Not ddlPilihanClaim.SelectedValue = "" Then
                strSql += "  and c.benefittypeid = " & valuedddlPilihanClaim(0)
            End If


            'add new CR DSF
            If valuedddlPilihanClaim(1).ToString() <> "1" Then
                '- tambahan validasi
                strSql += "  AND b.IsTemporary = 0 "
            End If

            Dim objBenefit As BenefitType = New BenefitTypeFacade(User).Retrieve(CShort(valuedddlPilihanClaim(0)))
            If objBenefit.EventValidation = 1 Then
                'remarks by anh 20160427 change to OpenFakturDate
                'strSql += "  and (b.ValidateTime between c.FakturOpenStart and c.FakturOpenEnd)"
                strSql += "  and (b.OpenFakturDate between c.FakturOpenStart and c.FakturOpenEnd)"
            Else
                strSql += "  and (CONVERT(DATE,b.ValidateTime ) between c.FakturValidationStart and c.FakturValidationEnd)"
            End If

            'strSql += "  and (b.ValidateTime between c.FakturValidationStart and c.FakturValidationEnd)"
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, "(" & strSql & ")"))

            _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                                 totalRow)
            If _arrList.Count < 1 Then
                'MessageBox.Show("Check tanggal faktur di Master Benefit")
                MessageBox.Show("Nomor rangka belum diajukan faktur")
                panel3.Attributes("style") = "display:;"
                panel1.Attributes("style") = "display:none;"
                panel2.Attributes("style") = "display:none;"
                panel4.Attributes("style") = "display:none;"

                Panel8.Attributes("style") = "display:none;"

                dgNoRangka.DataSource = _arrList
                dgNoRangka.DataBind()
                Return
            End If

            If objBenefit.AssyYearBox = 1 Then
                strSql += "  and a.ProductionYear = c.AssyYear  "
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, "(" & strSql & ")"))
                _arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
                                                                    totalRow)
                If _arrList.Count < 1 Then
                    MessageBox.Show("Check Tahun perakitan di Master Benefit")
                    panel3.Attributes("style") = "display:;"
                    panel1.Attributes("style") = "display:none;"
                    panel2.Attributes("style") = "display:none;"
                    panel4.Attributes("style") = "display:none;"

                    Panel8.Attributes("style") = "display:none;"

                    dgNoRangka.DataSource = _arrList
                    dgNoRangka.DataBind()
                    Return
                End If
            End If

        End If

        '_arrList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, dgNoRangka.CurrentPageIndex + 1, dgNoRangka.PageSize,
        '                                                            totalRow)

        dgNoRangka.VirtualItemCount = totalRow
        dgNoRangka.DataSource = _arrList
        dgNoRangka.DataBind()



        'If cbRefClaim.Checked = True Then
        panel3.Attributes("style") = "display:;"
        panel1.Attributes("style") = "display:none;"
        panel2.Attributes("style") = "display:none;"
        panel4.Attributes("style") = "display:none;"

        Panel8.Attributes("style") = "display:none;"

        'Else
        'panel1.Attributes("style") = "display:;"
        'panel2.Attributes("style") = "display:none;"
        'End If
    End Sub


    Private Sub btnCariPanel3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCariPanel3.Click
        Dim benefitType() As String = ddlPilihanClaim.SelectedValue.Split(";")
        If benefitType(2) = "1" AndAlso hfIIdEvent.Value.ToString = "" Then
            MessageBox.Show("No Reg Event belum dipilih")
        Else
            dgNoRangka.CurrentPageIndex = 0
            BindDataGridNorangka()
        End If
    End Sub
    Private Sub dgNoRangka_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgNoRangka.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
            ' If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then
                    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name='rb1'>")
                    e.Item.Cells(0).Controls.Add(rdbChoice)

                    Dim lblIDoGridPanel3 As Label = CType(e.Item.FindControl("lblIDoGridPanel3"), Label)
                    lblIDoGridPanel3.Text = objDomain2.ID.ToString

                    Dim lblNoGridPanel3 As Label = CType(e.Item.FindControl("lblNoGridPanel3"), Label)
                    lblNoGridPanel3.Text = (e.Item.ItemIndex + 1 + (dgNoRangka.CurrentPageIndex * dgNoRangka.PageSize)).ToString

                    Dim lblNoChassisPanel3 As Label = CType(e.Item.FindControl("lblNoChassisPanel3"), Label)
                    lblNoChassisPanel3.Text = objDomain2.ChassisNumber

                    Dim lblDeskripsiPanel3 As Label = CType(e.Item.FindControl("lblDeskripsiPanel3"), Label)
                    lblDeskripsiPanel3.Text = objDomain2.VechileColor.MaterialDescription

                    Dim lblValidasiFakturPanel3 As Label = CType(e.Item.FindControl("lblValidasiFakturPanel3"), Label)
                    lblValidasiFakturPanel3.Text = ""
                    If Not objDomain2.EndCustomer Is Nothing Then
                        lblValidasiFakturPanel3.Text = objDomain2.EndCustomer.ValidateTime.ToString("dd/MM/yyyy")
                    End If



                    Dim lblNoMesinPanel3 As Label = CType(e.Item.FindControl("lblNoMesinPanel3"), Label)
                    lblNoMesinPanel3.Text = objDomain2.EngineNumber

                    Dim lblCustomerPanel3 As Label = CType(e.Item.FindControl("lblCustomerPanel3"), Label)
                    lblCustomerPanel3.Text = objDomain2.EndCustNameText





                End If
            End If
        End If
    End Sub


    Private Sub dgNoRangka_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgNoRangka.PageIndexChanged
        'dgTable.CurrentPageIndex = e.NewPageIndex
        'dgTable.PageIndexChanged()
        dgNoRangka.CurrentPageIndex = e.NewPageIndex

        BindDataGridNorangka()
    End Sub

    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "XLS" Then
            retValue = True
        ElseIf ext.ToUpper() = "XLSX" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        dgTable.DataSource = Nothing
        Dim list As ArrayList = New ArrayList
        Dim listGrid As ArrayList
        If Not sessHelper.GetSession("DetailSession") Is Nothing Then
            listGrid = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        Else
            listGrid = New ArrayList
        End If


        If txtIdDetailMaster.Value = "" Then
            MessageBox.Show("Pilih Referensi Claim terlebih dahulu.")
            Return
        End If

        'Checking Is EventValidation
        Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")


        If valuedddlPilihanClaim(2).ToString() = "1" AndAlso (txtRegEvent.Text.Trim() = "" OrElse hfIIdEvent.Value = "") Then
            MessageBox.Show("Pilih No Reg Event  terlebih dahulu.")
            Return
        End If

        Dim retValue As Integer = 0
        If fileUploadExcel.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If fileUploadExcel.PostedFile.ContentLength <> fileUploadExcel.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim datetimenow As String = Now.ToString("yyyy_MM_dd_H_mm_ss")

                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "SalesCampaign_Benefit"
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)



                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(fileUploadExcel.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0

                    MessageBox.Show("Salah Extention, hanya *.xls")
                    Return
                End If

                'Dim targetFile As String = New System.Text.StringBuilder(directory). _
                '    Append("\").Append(datetimenow + "_" + _
                '                       Path.GetFileName(fileUploadExcel.PostedFile.FileName)).ToString

                'fileUploadExcel.PostedFile.SaveAs(targetFile)

                Dim SrcFile As String = Path.GetFileName(fileUploadExcel.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.Year.ToString() & "\" & DateTime.Now.Month.ToString() & "\" & DateTime.Now.ToString("ddMMyyyHHmmss") & txtKodeDealer.Text & SrcFile

                'Dim targetFile As String = New System.Text.StringBuilder(directory). _
                '    Append("\").Append(datetimenow + "_" + _
                '                       Path.GetFileName(fileUploadParticipant.PostedFile.FileName)).ToString

                Dim objUpload As New UploadToWebServer
                objUpload.Upload(fileUploadExcel.PostedFile.InputStream, targetFile)
                Dim objReader As IExcelDataReader = Nothing

                Dim checkSalah As Boolean = False
                Dim checkKosong As Boolean = True
                Dim oDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)

                Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)
                    If ext.ToLower.Contains("xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    Else
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    End If
                    'objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    If (Not IsNothing(objReader)) Then
                        Dim temp As String = ""
                        Dim tempFormula As String = ""
                        Dim notValid As Integer = 0
                        Dim valid As Integer = 0
                        ' Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")

                        While objReader.Read()

                            If (i = 4) Then
                                Try
                                    If objReader.GetString(0).ToString().ToLower().Trim() = "no" Then
                                        checkSalah = True
                                    End If
                                Catch ex As Exception

                                End Try


                            End If

                            ' txtNoRegEvent.Text = txtNoRegEvent.Text & " _ " & i & "->" & objReader.GetString(0)

                            If (i > 4 AndAlso checkSalah) Then

                                checkKosong = False

                                Try
                                    'add by anh 20160220
                                    objDomainDetail = New BenefitClaimDetails
                                    Dim objFacade As New BenefitClaimDetailsFacade(User)

                                    Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                                    Dim objChassisMaster As ChassisMaster = objChassisMasterFacade.Retrieve(objReader.GetString(1).Trim())

                                    If Not objChassisMaster Is Nothing AndAlso objChassisMaster.ID > 0 AndAlso Not objChassisMaster.EndCustomer Is Nothing AndAlso objChassisMaster.EndCustomer.OpenFakturDate.Year > 0 Then
                                        objDomainDetail.ChassisMaster = objChassisMaster

                                        'begin update untuk check 1 claim 20150308
                                        objDomainDetail = objFacade.ChassisNumberValidation(oDealer.ID, objChassisMaster.ID, CShort(txtIdDetailMaster.Value), CInt(valuedddlPilihanClaim(0)), listGrid, CInt(IIf(ddlLeasing.SelectedValue = "", "0", ddlLeasing.SelectedValue)), False, CInt(IIf(hfIIdEvent.Value = "", 0, hfIIdEvent.Value)))
                                        'end

                                        Try
                                            objDomainDetail.DescDealer = objReader.GetString(4).Trim()
                                        Catch
                                        End Try

                                        objDomainDetail.NoBaris = j + 1

                                        If list.Count > 0 Then
                                            Dim _arrlist As List(Of BenefitClaimDetails) = (From ob As BenefitClaimDetails In list
                                                                       Where ob.ChassisMaster.ChassisNumber = objChassisMaster.ChassisNumber _
                                                                       And ob.ChassisMaster.EngineNumber = objChassisMaster.EngineNumber
                                                                       Select ob).ToList()
                                            If _arrlist.Count > 0 Then
                                                objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + " Duplikat upload No Rangka dan No Mesin,"
                                            End If
                                        End If

                                        If Not IsNothing(objDomainDetail.ErrorMessage) AndAlso Len(objDomainDetail.ErrorMessage.Trim) > 0 Then
                                            objDomainDetail.ErrorMessage = IIf(Right(objDomainDetail.ErrorMessage, 1) = ",", Left(objDomainDetail.ErrorMessage, Len(objDomainDetail.ErrorMessage) - 1), objDomainDetail.ErrorMessage)
                                        End If

                                        valid += 1
                                        list.Add(objDomainDetail)
                                        j = j + 1

                                    Else

                                        If objChassisMaster Is Nothing OrElse objChassisMaster.ID <= 0 Then
                                            objDomainDetail.ErrorMessage = "No Rangka Tidak Terdaftar,"
                                            objChassisMaster = New ChassisMaster(0)
                                            objChassisMaster.Dealer = New Dealer(0)
                                            objChassisMaster.VechileColor = New VechileColor(0)

                                        End If


                                        If objChassisMaster.EndCustomer Is Nothing OrElse objChassisMaster.EndCustomer.ID <= 0 Then
                                            objChassisMaster.EndCustomer = New EndCustomer(0)
                                            objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + " Customer Tidak Terdaftar,"
                                        Else
                                            If objChassisMaster.EndCustomer.OpenFakturDate.Year <= 1900 Then
                                                objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + " Belum Open Faktur,"
                                            End If

                                        End If

                                        objChassisMaster.ChassisNumber = objReader.GetString(1)

                                        objDomainDetail.ChassisMaster = objChassisMaster

                                        Dim objBenefitMasterHeaderX As BenefitMasterHeader = New BenefitMasterHeader
                                        Dim objBenefitMasterHeaderFacadeX As BenefitMasterHeaderFacade = New BenefitMasterHeaderFacade(User)
                                        objBenefitMasterHeaderX = objBenefitMasterHeaderFacadeX.Retrieve(CShort(txtIdDetailMaster.Value))
                                        If objBenefitMasterHeaderX.ID > 0 AndAlso objBenefitMasterHeaderX.BenefitMasterDetails.Count > 0 Then
                                            objDomainDetail.BenefitMasterDetail = objBenefitMasterHeaderX.BenefitMasterDetails(0)
                                            objDomainDetail.BenefitMasterDetail.BenefitMasterHeader = objBenefitMasterHeaderX
                                        End If

                                        Try
                                            objDomainDetail.DescDealer = objReader.GetString(4).Trim()
                                        Catch
                                        End Try

                                        'add new CR DSF
                                        If valuedddlPilihanClaim(1).ToString() <> "1" Then
                                            If Not IsNothing(objChassisMaster.EndCustomer) AndAlso objChassisMaster.EndCustomer.ID > 0 Then
                                                If objChassisMaster.EndCustomer.IsTemporary > 0 Then
                                                    objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + " Nomor rangka tidak dapat digunakan untuk pengajuan claim,"
                                                End If
                                            End If
                                        End If

                                        objDomainDetail.NoBaris = j + 1

                                        If list.Count > 0 Then
                                            Dim _arrlist As List(Of BenefitClaimDetails) = (From ob As BenefitClaimDetails In list
                                                                       Where ob.ChassisMaster.ChassisNumber = objChassisMaster.ChassisNumber _
                                                                       And ob.ChassisMaster.EngineNumber = objChassisMaster.EngineNumber
                                                                       Select ob).ToList()

                                            If _arrlist.Count > 0 Then
                                                objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + " Duplikat upload No Rangka dan No Mesin,"
                                            End If
                                        End If

                                        If Not IsNothing(objDomainDetail.ErrorMessage) AndAlso Len(objDomainDetail.ErrorMessage.Trim) > 0 Then
                                            objDomainDetail.ErrorMessage = IIf(Right(objDomainDetail.ErrorMessage, 1) = ",", Left(objDomainDetail.ErrorMessage, Len(objDomainDetail.ErrorMessage) - 1), objDomainDetail.ErrorMessage)
                                        End If

                                        notValid += 1
                                        list.Add(objDomainDetail)
                                        j = j + 1
                                    End If
                                Catch ex As Exception
                                    notValid += 1
                                End Try
                                'endd add by anh 20160220
                            End If
                            i = i + 1
                        End While
                        lblMessage.Text = "Jumlah data : " & (valid + notValid).ToString & " ( Valid : " & valid.ToString & " ; Tidak Valid : " & notValid.ToString & " )"
                    End If
                End Using

                If checkSalah = False Then
                    MessageBox.Show("Silakan gunakan template yang tersedia.")
                    Return
                End If
                If checkKosong = True Then
                    MessageBox.Show("Data Excel tidak boleh ada yang kosong.")
                    Return
                End If

                If list.Count > 0 Then
                    sessHelper.SetSession("DetailClaimExcelSession", list)
                End If
                list.AddRange(listGrid)

                GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

                retValue = 1
            Catch ex As Exception
                retValue = 0
                MessageBox.Show(ex.Message.ToString)
            Finally
                sapImp.StopImpersonate()
                sapImp = Nothing
            End Try
        End If
    End Sub







    Private Sub lblPopUpRegEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblPopUpRegEvent.Click
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        If txtIdDetailMaster.Value = "" Then
            MessageBox.Show("Tentukan no surat terlebih dahulu")

            dgTable.DataSource = _arrList
            dgTable.DataBind()

            Return
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "Status", MatchType.Exact, CInt(BenefitEventEnumStatus.Status.Disetujui)))

        Dim strSql As String = ""
        strSql += " select GETDATE() "

        Dim tgl As New DateTime(CInt(Now.Year), CInt(Now.Month), CInt(Now.Day), 0, 0, 0)
        'criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "EventDate", MatchType.GreaterOrEqual, tgl))
        criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "BenefitMasterHeader", MatchType.Exact, CInt(txtIdDetailMaster.Value)))
        criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "Dealer.DealerCode", MatchType.Exact, CInt(txtKodeDealer.Text)))

        _arrList = New BenefitEventHeaderFacade(User).RetrieveByCriteria(criterias, dgEvent.CurrentPageIndex + 1, dgEvent.PageSize, totalRow)
        sessHelper.SetSession("EventListSession", _arrList)
        dgEvent.VirtualItemCount = totalRow
        dgEvent.DataSource = _arrList
        dgEvent.DataBind()

        If _arrList.Count < 1 Then
            MessageBox.Show("Data tidak ditemukan.")
        End If

        'If cbRefClaim.Checked = True Then
        panel3.Attributes("style") = "display:none;"
        panel1.Attributes("style") = "display:none;"
        panel2.Attributes("style") = "display:none;"
        panel4.Attributes("style") = "display:;"

        Panel8.Attributes("style") = "display:none;"

        'Else
        'panel1.Attributes("style") = "display:;"
        'panel2.Attributes("style") = "display:none;"
        'End If

    End Sub
    Private Sub dgEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEvent.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitEventHeader = CType(e.Item.DataItem, BenefitEventHeader)
            ' If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then
                    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name='rb2'>")
                    e.Item.Cells(0).Controls.Add(rdbChoice)

                    Dim lblIDoGridPanel4 As Label = CType(e.Item.FindControl("lblIDoGridPanel4"), Label)
                    lblIDoGridPanel4.Text = objDomain2.ID.ToString

                    Dim lblNoGridPanel4 As Label = CType(e.Item.FindControl("lblNoGridPanel4"), Label)
                    lblNoGridPanel4.Text = (e.Item.ItemIndex + 1 + (dgEvent.CurrentPageIndex * dgEvent.PageSize)).ToString

                    Dim lblNoEventPanel4 As Label = CType(e.Item.FindControl("lblNoEventPanel4"), Label)
                    lblNoEventPanel4.Text = objDomain2.EventRegNo

                    Dim lblNamaEventPanel4 As Label = CType(e.Item.FindControl("lblNamaEventPanel4"), Label)
                    lblNamaEventPanel4.Text = objDomain2.EventName

                    Dim lblEventDatePanel4 As Label = CType(e.Item.FindControl("lblEventDatePanel4"), Label)
                    lblEventDatePanel4.Text = objDomain2.EventDate.ToString("dd/MM/yyyy")

                    Dim lblStatusPanel4 As Label = CType(e.Item.FindControl("lblStatusPanel4"), Label)
                    lblStatusPanel4.Text = BenefitEventEnumStatus.GetString(objDomain2.Status)
                    'Select Case objDomain2.Status
                    '    Case 1
                    '        lblStatusPanel4.Text = "Baru"
                    '    Case 2
                    '        lblStatusPanel4.Text = "Validasi"
                    '    Case 3
                    '        lblStatusPanel4.Text = "Konfirmasi"
                    '    Case 4
                    '        lblStatusPanel4.Text = "Selesai"
                    'End Select
                End If
            End If
        End If
    End Sub



    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim list As New ArrayList
        Dim i As Integer



        If dgTable.Items.Count < 1 Then
            MessageBox.Show("Isi No Rangka minimal 1")
            Return
        End If


        If txtIdDetailMaster.Value.Replace(" ", "") = "" Then
            MessageBox.Show("Isi Referensi Claim ")
            Return
        End If


        Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")


        If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
            objDomain = New BenefitClaimHeader
            objDomain.ClaimDate = icClaimDate.Value
            ' objDomain.Status = 0

            Dim objdealerFacade As DealerFacade = New DealerFacade(User)
            Dim objdealer As Dealer = objdealerFacade.Retrieve(txtKodeDealer.Text.Trim())
            objDomain.Dealer = objdealer

            'If ddlPilihanClaim.SelectedItem.
            If valuedddlPilihanClaim(1) = "1" Then
                If Not ddlLeasing.SelectedValue = "" Then
                    Dim objLeasingCompanyFacade As LeasingCompanyFacade = New LeasingCompanyFacade(User)
                    Dim objLeasingCompany As LeasingCompany = objLeasingCompanyFacade.Retrieve(CShort(ddlLeasing.SelectedValue))
                    objDomain.LeasingCompany = objLeasingCompany
                End If

            End If




            'objDomain.ClaimRegNo = txtRegClaimNo.Text

            Dim listMasterDetail As ArrayList = CType(sessHelper.GetSession("MasterDetailSession"), ArrayList)
            'If Not listMasterDetail Is Nothing Then
            '    For Each el As BenefitMasterDetail In listMasterDetail
            '        If el.ID = CShort(txtIdDetailMaster.Value) Then
            '            objDomain.BenefitMasterDetail = el
            '            Exit For
            '        End If
            '    Next
            'End If

            objDomain.BenefitType = New BenefitTypeFacade(User).Retrieve(CShort(valuedddlPilihanClaim(0)))

            Dim listEvent As ArrayList = CType(sessHelper.GetSession("EventListSession"), ArrayList)
            If Not listEvent Is Nothing Then
                For Each el As BenefitEventHeader In listEvent
                    If Not txtRegEvent.Text = "" And Not hfIIdEvent.Value = "" Then

                        If el.ID = CShort(hfIIdEvent.Value) Then
                            objDomain.BenefitEventHeader = el
                            Exit For
                        End If

                    End If


                Next
            End If

            If Not sessHelper.GetSession("DetailSession") Is Nothing Then
                list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
            End If

            For Each ObjCLaimDetail As BenefitClaimDetails In list
                If ObjCLaimDetail.ErrorMessage <> "" Then
                    MessageBox.Show("Masih Ada Data Bermasalah")
                    Return
                End If
            Next



            Dim n As Integer = objDomainFacade.Insert(objDomain, list)
            'Dim n As Integer = 0

            If n = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                UpdateBenefitClaimReceipt(n)
                RemoveALLSession()

                Response.Write("<script>alert('Data Claim berhasil disimpan')</script>")
                If Convert.ToString(Request.QueryString("Mode")) = "View" Or
                  Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
                  Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
                    Response.Write("<script>window.location.href='FrmBenefitClaimList.aspx';</script>")
                Else
                    Response.Write("<script>window.location.href='FrmInputClaim.aspx?Mode=ViewSave&id=" & n & "';</script>")
                End If



                '  Response.Write("<script>window.location.href='FrmBenefitClaimList.aspx';</script>")

            End If


        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Edit" Then

            Dim IDBenefitListHeader As Integer = CInt(sessHelper.GetSession("IDBenefitListHeader"))
            Dim objDomainTemp As BenefitClaimHeader = objDomainFacade.Retrieve(IDBenefitListHeader)

            objDomainTemp.ClaimDate = icClaimDate.Value
            'objDomain.ClaimRegNo = txtRegClaimNo.Text


            'Dim objdealerFacade As DealerFacade = New DealerFacade(User)
            'Dim objdealer As Dealer = objdealerFacade.Retrieve(txtKodeDealer.Text.Trim())
            'objDomain.Dealer = objdealer

            'If ddlPilihanClaim.SelectedItem.
            If valuedddlPilihanClaim(1) = "1" Then
                If Not ddlLeasing.SelectedValue = "" Then
                    Dim objLeasingCompanyFacade As LeasingCompanyFacade = New LeasingCompanyFacade(User)
                    Dim objLeasingCompany As LeasingCompany = objLeasingCompanyFacade.Retrieve(CShort(ddlLeasing.SelectedValue))
                    objDomainTemp.LeasingCompany = objLeasingCompany
                End If

            End If


            Dim listMasterDetail As ArrayList = CType(sessHelper.GetSession("MasterDetailSession"), ArrayList)
            'If Not listMasterDetail Is Nothing Then
            '    For Each el As BenefitMasterDetail In listMasterDetail
            '        If el.ID = CShort(txtIdDetailMaster.Value) Then
            '            objDomainTemp.BenefitMasterDetail = el
            '            Exit For
            '        End If
            '    Next
            'End If

            objDomain.BenefitType = New BenefitTypeFacade(User).Retrieve(CShort(valuedddlPilihanClaim(0)))

            Dim listEvent As ArrayList = CType(sessHelper.GetSession("EventListSession"), ArrayList)
            If Not listEvent Is Nothing Then
                For Each el As BenefitEventHeader In listEvent
                    If el.ID = CShort(hfIIdEvent.Value) Then
                        objDomainTemp.BenefitEventHeader = el
                        Exit For
                    End If
                Next
            End If



            If Not sessHelper.GetSession("DetailSession") Is Nothing Then
                list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
            End If


            Dim n As Integer = objDomainFacade.Update(objDomainTemp, list)
            If n = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                UpdateBenefitClaimReceipt(objDomainTemp.ID)

                'RemoveALLSession()

                'Response.Write("<script>alert('Data Claim berhasil diubah')</script>")
                'Response.Write("<script>window.location.href='FrmBenefitClaimList.aspx';</script>")
                RemoveALLSession()

                Response.Write("<script>alert('Data Claim berhasil diubah')</script>")
                If Convert.ToString(Request.QueryString("Mode")) = "View" Or
                  Convert.ToString(Request.QueryString("Mode")) = "Edit" Or
                  Convert.ToString(Request.QueryString("Mode")) = "Delete" Then
                    Response.Write("<script>window.location.href='FrmBenefitClaimList.aspx';</script>")
                Else
                    Response.Write("<script>window.location.href='FrmInputClaim.aspx';</script>")
                End If

            End If


        End If
    End Sub

    Private Sub UpdateBenefitClaimReceipt(ByVal objBenefitClaimHeaderID As Integer)
        Try
            Dim objBenefitClaimHeader As BenefitClaimHeader = New BenefitClaimHeaderFacade(User).Retrieve(objBenefitClaimHeaderID)

            Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            Dim fakturPajakDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            For Each el As BenefitClaimReceipt In objBenefitClaimHeader.BenefitClaimReceipts
                cutOffDate = el.ReceiptDate
                fakturPajakDate = el.FakturPajakDate
            Next

            If cutOffDate.Year < 1900 Then
                cutOffDate = objBenefitClaimHeader.ClaimDate
            End If

            Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
            Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

            Dim tempAmount As Decimal = 0, tempamount1 As Decimal = 0
            For Each el As BenefitClaimDetails In objBenefitClaimHeader.BenefitClaimDetailss
                tempAmount = tempAmount + el.BenefitMasterDetail.Amount
            Next
            tempamount1 = tempAmount

            Dim nilaiPph As Decimal = 0, nilaiVAT As Decimal = 0
            Dim total As Decimal = tempamount1
            If Not tempAmount = 0 Then
                If objBenefitClaimHeader.BenefitType.LeasingBox = 1 Then
                    'nilaiPph = Math.Round(((tempamount1 / (1 - 0.15)) - tempamount1))
                    'nilaiVAT = Math.Round((0.1 * (nilaiPph + tempamount1)))
                    'total = total + nilaiPph
                    'total = Math.Round(total + (0.1 * (nilaiPph + tempamount1)))

                    total = CalcHelper.DPPCalculation(pph, total)
                    nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=total)
                    nilaiVAT = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=total)
                    total += nilaiVAT
                Else
                    ''nilaiPph = Math.Round(((tempamount1 / (1 - 0.15)) - tempamount1))
                    'nilaiPph = Math.Round(0.15 * tempamount1)
                    'nilaiVAT = 0 'Math.Round(0.1 * tempamount1)
                    'total = Math.Round(total + (0.1 * tempamount1))

                    nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=total)
                    nilaiVAT = 0
                    total += CalcHelper.PPNCalculation(CalcSourceTypeEnum.Total, ppn, total:=total)
                End If
            End If

            Dim dblAmountDeducted As Double = GetAmountByDeducted(objBenefitClaimHeader, total)
            If dblAmountDeducted = 0 Then
                dblAmountDeducted = total
            End If
            Dim objBenefitClaimReceipt As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).RetrieveByClaimHeader(objBenefitClaimHeader.ID)
            If Not IsNothing(objBenefitClaimReceipt) AndAlso objBenefitClaimReceipt.ID > 0 Then
                objBenefitClaimReceipt.VATTotal = nilaiVAT
                objBenefitClaimReceipt.PPHTotal = nilaiPph
                objBenefitClaimReceipt.ReceiptAmount = total
                objBenefitClaimReceipt.ReceiptAmountDeducted = dblAmountDeducted
                Dim result As Integer = New BenefitClaimReceiptFacade(User).UpdateReceipt(objBenefitClaimReceipt)
            End If

        Catch
        End Try
    End Sub

    Private Function GetAmountByDeducted(ByVal objHeader As BenefitClaimHeader, ByVal decTotal As Decimal) As Decimal
        Dim decAmountByDeducted As Decimal = decTotal
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
        Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("SalesCampaignDeductedBenefitType")
        If Not IsNothing(objAppConfig) Then
            Dim strVal() As String = objAppConfig.Value.ToString().Split(";")
            For Each strCode As String In strVal
                If strCode = objHeader.BenefitType.ID Then
                    Dim objBenefitClaimDeductedFacade As New BenefitClaimDeductedFacade(User)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDeducted), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(BenefitClaimDeducted), "BenefitClaimHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(BenefitClaimDeducted), "CreatedTime", CType(Sort.SortDirection.ASC, Sort.SortDirection)))

                    Dim dblTotAmountDeducted As Double = 0
                    Dim dblUsage As Double = 0
                    Dim objBenefitClaimDeductedHistory As New BenefitClaimDeductedHistory
                    Dim arlBCD As ArrayList = objBenefitClaimDeductedFacade.Retrieve(criterias, sortColl)
                    If Not IsNothing(arlBCD) AndAlso arlBCD.Count > 0 Then
                        For Each objDeduct As BenefitClaimDeducted In arlBCD
                            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDeductedHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crit.opAnd(New Criteria(GetType(BenefitClaimDeductedHistory), "BenefitClaimHeader.ID", MatchType.Exact, objHeader.ID))
                            crit.opAnd(New Criteria(GetType(BenefitClaimDeductedHistory), "BenefitClaimDeducted.ID", MatchType.Exact, objDeduct.ID))
                            Dim aggregateSum As Aggregate = New Aggregate(GetType(BenefitClaimDeductedHistory), "AmountDeducted", AggregateType.Sum)
                            Dim decAmountDeducted As Decimal = IsDBNull(New BenefitClaimDeductedHistoryFacade(User).RetrieveScalar(crit, aggregateSum), 0)

                            dblTotAmountDeducted += decAmountDeducted

                            If objDeduct.RemainAmount > 0 Then
                                dblUsage += objDeduct.RemainAmount
                            End If
                        Next
                    End If

                    dblUsage += dblTotAmountDeducted
                    If dblUsage > decAmountByDeducted Then
                        decAmountByDeducted = 0
                    Else
                        decAmountByDeducted -= dblUsage
                    End If

                    Exit For
                End If
            Next
        End If

        Return decAmountByDeducted
    End Function

    Private Sub LinkDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkDownload.Click


        Response.Redirect("../downloadlocal.aspx?file=Benefit\Rangka.xls")

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim list As New ArrayList
        Dim i As Integer

        Dim IDBenefitListHeader As Integer = CInt(sessHelper.GetSession("IDBenefitListHeader"))

        Dim objDomainTemp As BenefitClaimHeader = objDomainFacade.Retrieve(IDBenefitListHeader)
        Dim n As Integer = objDomainFacade.Delete(objDomainTemp)
        'Dim n As Integer = objDomainFacade.Insert(objDomain)
        If n = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            RemoveALLSession()
            ' Response.Redirect("FrmNewInputMasterList.aspx")
            ' MessageBox.Show("Hapus Berhasil")
            ' Response.Redirect("FrmEventParticipantProcessList.aspx")
            Response.Write("<script>alert('Data berhasil dihapus.')</script>")
            Response.Write("<script>window.location.href='FrmBenefitClaimList.aspx';</script>")
        End If



    End Sub

    Private Function PopulateData_Validasi() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList

        'Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
        Dim i As Integer = 1

        For Each oDataGridItem In dgTable.Items
            chkExport = oDataGridItem.FindControl("cbAllGrid")
            If chkExport.Checked Then


                oExArgs.Add(i)

            End If
            i += 1
        Next

        Return oExArgs
    End Function

    Private Sub btnStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatus.Click

        Dim list As New ArrayList
        Dim listcheck As New ArrayList
        listcheck = PopulateData_Validasi()

        If Not sessHelper.GetSession("DetailSession") Is Nothing Then
            list = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        End If

        'For Each item As String In arrayCheck.Value.Replace(" ", "").Split(";")
        '    If Not item Is Nothing And Not item = "" Then
        '        listcheck.Add(item)
        '    End If
        'Next



        Dim n As Integer = objDomainFacade.UpdateStatus(list, listcheck, ddlStatus.SelectedValue)

        If n <> -1 Then
            MessageBox.Show("Data berhasil di proses")
        Else
            '  MessageBox.Show()
            MessageBox.Show(SR.SaveFail)
        End If

        GetValueFromDataBase(CInt(sessHelper.GetSession("IDBenefitListHeader")))

    End Sub

    Private Sub dgGridDetil_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgGridDetil.PageIndexChanged
        dgGridDetil.CurrentPageIndex = e.NewPageIndex
        BindRefClaim(e.NewPageIndex)
    End Sub

    Private Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
        If obj Is DBNull.Value Then
            Return replacement
        End If
        Return CType(obj, Decimal)
    End Function

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        Response.Redirect("~/Benefit/FrmInputClaim.aspx?Mode=View&id=" & CInt(Request.QueryString("id")))
    End Sub
End Class
