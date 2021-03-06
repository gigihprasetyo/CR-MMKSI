Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security



Public Class FrmSalesmanTrainingMember
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchJobPos As System.Web.UI.WebControls.Label
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJobPosition As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlOperator As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icHireDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dgSalesmanTraining As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnPilih As System.Web.UI.WebControls.Button
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlLevel As System.Web.UI.WebControls.DropDownList


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

    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _SalesmanTrainingParticipantFacade As New SalesmanTrainingParticipantFacade(User)
    Private _SalesmanLevelFacade As New SalesmanLevelFacade(User)

    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
    Private strId As String

#End Region

#Region "PrivateCustomMethods"




#End Region

#Region "EventHandlers"


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SaveCriteria()
        dgSalesmanTraining.CurrentPageIndex = 0
        sessHelper.RemoveSession("criteriaFM")
        BindDataGrid(0)
    End Sub
#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'CheckPrivilege()
            Initialize()
            BindDropDownLists()
            BindControlsAttribute()
            ReadCriteria()
            BindDataGrid(0)
            If Not IsNothing(Request.QueryString("dealer")) Then
                txtDealerCode.Text = Request.QueryString("dealer")
            End If
        End If
    End Sub

    Private Sub BindControlsAttribute()
        lblPopUpDealer.Attributes("onClick") = "showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);"
        lblSearchJobPos.Attributes("onClick") = "showPopUp('../PopUp/PopUpJobPositionMany.aspx?Menu=2','',600,600,JobPosSelection);"
    End Sub

    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        CommonFunction.BindFromEnum("Operator", ddlOperator, Me.User, True, "NameStatus", "ValStatus")
        BindDropDownListLevel()
    End Sub

    Private Sub BindDropDownListLevel()
        Dim arrLevel As New ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanLevel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        arrLevel = _SalesmanLevelFacade.Retrieve(criterias)

        ddlLevel.Items.Add(New ListItem("Silahkan pilih", String.Empty))
        For Each objSalesmanlevel As SalesmanLevel In arrLevel
            ddlLevel.Items.Add(New ListItem(objSalesmanlevel.Description, objSalesmanlevel.ID))
        Next

    End Sub


#Region "Need To Add"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        txtDealerCode.Text = String.Empty
        txtJobPosition.Text = String.Empty
        ddlOperator.SelectedIndex = -1
        icHireDate.Value = DateTime.Now
        sessHelper.RemoveSession("criteriaFM") ' handle session dengan criteria yg baru
    End Sub

    Private Sub Initialize()
        ClearData()
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
        'btnSearch.Visible = _view

    End Sub

    Private Function GetExcludeSalesID() As String
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim strSalesId As String

        ' membuat format salesmanTrainingParticipant
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ' harus exclude salesman yg sdh ikut sebelumnya
        If Request.QueryString("strTrainingId") <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanMasterTraining.ID", MatchType.Exact, CType(Request.QueryString("strTrainingId"), Integer)))
        End If

        If Not IsNothing(Request.QueryString("dealer")) Then
            If Request.QueryString("dealer") <> "" Then
                criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, CommonFunction.GetStrValue(Request.QueryString("dealer"), ";", ",")))
            End If
        End If

        criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'arrList = _SalesmanTrainingParticipantFacade.RetrieveByCriteria(criterias, 1, dgSalesmanTraining.PageSize, totalRow)
        arrList = _SalesmanTrainingParticipantFacade.Retrieve(criterias)

        If arrList.Count > 0 Then
            For Each item As SalesmanTrainingParticipant In arrList
                If strSalesId = "" Then
                    strSalesId = CType(item.SalesmanHeader.ID, String)
                Else
                    strSalesId = strSalesId & ";" & CType(item.SalesmanHeader.ID, String)
                End If
            Next
        End If
        Return strSalesId



    End Function


    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        If (IsNothing(sessHelper.GetSession("criteriaFM"))) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            ' kriteria default = harus sdh teregister
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, CShort(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif)))
            If txtDealerCode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, CommonFunction.GetStrValue(txtDealerCode.Text, ";", ",")))
            Else
                If Not IsNothing(Request.QueryString("dealer")) Then
                    If Request.QueryString("dealer") <> "" Then
                        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, CommonFunction.GetStrValue(Request.QueryString("dealer"), ";", ",")))
                    End If
                End If
            End If

            If txtJobPosition.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.Code", MatchType.InSet, CommonFunction.GetStrValue(txtJobPosition.Text, ";", ",")))
            End If

            If ddlLevel.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanLevel.ID", MatchType.Exact, ddlLevel.SelectedItem.Value))
            End If

            Dim strSalesmanId As String
            strSalesmanId = GetExcludeSalesID()

            If (strSalesmanId <> "") Then
                'Todo Inset
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.NotInSet, strSalesmanId.Replace(";", ",")))
            End If

            ' tergantung pada operator yg dimasukan , refer pada EnumOperator
            Select Case CType(ddlOperator.SelectedValue, Byte) - 1 ' krn ada include blank
                Case EnumOperator.[Operator].Equal
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.Exact, Format(icHireDate.Value, "yyyy-MM-dd HH:mm:ss")))
                Case EnumOperator.[Operator].Greater_Equal
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.GreaterOrEqual, Format(icHireDate.Value, "yyyy-MM-dd HH:mm:ss")))
                Case EnumOperator.[Operator].Greater_Than
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.Greater, Format(icHireDate.Value, "yyyy-MM-dd HH:mm:ss")))
                Case EnumOperator.[Operator].Less_Equal
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.LesserOrEqual, Format(icHireDate.Value, "yyyy-MM-dd HH:mm:ss")))
                Case EnumOperator.[Operator].Less_Than
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.Lesser, Format(icHireDate.Value, "yyyy-MM-dd HH:mm:ss")))
            End Select

            sessHelper.SetSession("criteriaFM", criterias)
        End If


        arrList = _SalesmanHeaderFacade.RetrieveByCriteria(CType(sessHelper.GetSession("criteriaFM"), CriteriaComposite), idxPage + 1, dgSalesmanTraining.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanTraining.DataSource = arrList
        dgSalesmanTraining.VirtualItemCount = totalRow
        dgSalesmanTraining.DataBind()
    End Sub

#End Region

    Private Sub dgSalesmanTraining_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanTraining.PageIndexChanged
        dgSalesmanTraining.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanTraining.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanTraining_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanTraining.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHeader As SalesmanHeader = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanTraining.CurrentPageIndex * dgSalesmanTraining.PageSize)

            Dim lblDealerCodeNew As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            lblDealerCodeNew.Text = objSalesmanHeader.Dealer.DealerCode

            Dim lblNameNew As Label = CType(e.Item.FindControl("lblName"), Label)
            lblNameNew.Text = objSalesmanHeader.Name

            Dim lblPositionNew As Label = CType(e.Item.FindControl("lblPosition"), Label)
            lblPositionNew.Text = objSalesmanHeader.JobPosition.Code

            Dim lblLevelNew As Label = CType(e.Item.FindControl("lblLevel"), Label)
            lblLevelNew.Text = objSalesmanHeader.SalesmanLevel.Description




            'Dim lblHireDateNew As Label = CType(e.Item.FindControl("lblHireDate"), Label)
            'lblHireDateNew.Text = Date.Parse(objSalesmanHeader.HireDate).ToString

            Dim lbtnViewNew As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            'lbtnViewNew.CommandArgument = objSalesmanHeader.SalesmanCode & ";" & objSalesmanHeader.Name
            lbtnViewNew.Attributes("onClick") = "showPopUp('../Salesman/FrmSalesmanTrainingList.aspx?SalesmanCode=" & objSalesmanHeader.SalesmanCode & "&SalesmanName=" & objSalesmanHeader.Name & "&Time=" & DateTime.Now.ToString & "','',500,760);"

            Dim lblSalesIdNew As Label = CType(e.Item.FindControl("lblSalesId"), Label)
            lblSalesIdNew.Text = objSalesmanHeader.ID

        End If

    End Sub

    Private Sub dgSalesmanTraining_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanTraining.ItemCommand
    End Sub

    Private Sub dgSalesmanTraining_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanTraining.SortCommand
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
        dgSalesmanTraining.SelectedIndex = -1
        dgSalesmanTraining.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanTraining.CurrentPageIndex)
    End Sub

    ' untuk menampung kriteria yg sebelumnya
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("JobPosition", txtJobPosition.Text)
        crits.Add("Operator", ddlOperator.SelectedValue)
        crits.Add("HireDate", icHireDate.Value)
        sessHelper.SetSession("FrmSalesmanTrainingMember", crits)
    End Sub

    ' untuk menampilkan data kriteria sebelumnya
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("FrmSalesmanTrainingMember"), Hashtable)

        If Not IsNothing(crits) Then
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            txtJobPosition.Text = CStr(crits.Item("JobPosition"))
            ddlOperator.SelectedValue = CStr(crits.Item("Operator"))
            icHireDate.Value = CStr(crits.Item("HireDate"))
        End If

    End Sub

    Private Sub btnPilih_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPilih.Click

        If Request.QueryString("strTrainingId") <> "" Then
            Dim intTrainingId As Integer
            intTrainingId = CType(Request.QueryString("strTrainingId"), Integer)

            ' menyimpan data langsung kedatabase
            Dim lblSalesITmp As Label
            Dim intResult As Integer
            Dim counter As Integer = 0

            Dim objSalesmanMasterTraining As SalesmanMasterTraining = New SalesmanMasterTrainingFacade(User).Retrieve(intTrainingId)

            For Each item As DataGridItem In dgSalesmanTraining.Items
                Dim chkTmp As CheckBox = CType(item.FindControl("chkItem"), CheckBox)
                If chkTmp.Checked Then
                    Dim objSalesmanTrainingParticipant As SalesmanTrainingParticipant = New SalesmanTrainingParticipant

                    lblSalesITmp = CType(item.FindControl("lblSalesId"), Label)
                    Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CType(lblSalesITmp.Text, Integer))

                    objSalesmanTrainingParticipant.SalesmanMasterTraining = objSalesmanMasterTraining
                    objSalesmanTrainingParticipant.SalesmanHeader = objSalesmanHeader
                    intResult = New SalesmanTrainingParticipantFacade(User).Insert(objSalesmanTrainingParticipant)
                    counter = counter + 1
                End If
            Next

            If counter = 0 Then
                MessageBox.Show("Silahkan Pilih Peserta Training Terlebih Dahulu")
            Else
                RegisterClientScriptBlock("Add", "<script language=JavaScript>AddPesertaTraining();</script>")
                'RegisterClientScriptBlock("Close", "<script language=JavaScript>opener.dialogWin.returnFunc('');window.close();</script>")
            End If

        End If


    End Sub

End Class
