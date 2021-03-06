Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class FrmSalesmanUniformReceiver
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanName As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPosisi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnPilih As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanUniformReceiver As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblKodePesanan As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox

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
    'Private _SalesmanAreaFacade As New SalesmanAreaFacade(User)
    'Private _DealerFacade As New DealerFacade(User)
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private sessHelper As New SessionHelper
    Private _arrSalesmanUniform As New ArrayList
    Private _strSalesmanUniformID As String

#End Region

#Region "PrivateCustomMethods"
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "Dealer.DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub ClearData()
        txtSalesmanName.Text = String.Empty
        ddlPosisi.SelectedIndex = 0
    End Sub
    Private Sub BindPositionDropDownLists()
        ddlPosisi.Items.Clear()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.No, KTB.DNet.Lib.WebConfig.GetValue("BManCode")))
        'criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.No, KTB.DNet.Lib.WebConfig.GetValue("SManCode")))

        ddlPosisi.DataSource = New JobPositionFacade(User).Retrieve(criterias)
        ddlPosisi.DataTextField = "Description"
        ddlPosisi.DataValueField = "Code"
        ddlPosisi.DataBind()
        ddlPosisi.Items.Insert(0, "Pilih salah satu")
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)

        Dim AssignedSalesmanString As String = String.Empty
        Dim AssignedSalesman As New ArrayList
        'Dim objUniform As New SalesmanUniform
        
        'objUniform = New SalesmanUniformFacade(User).Retrieve(CInt(ViewState("UniformCode")))
        '''Dim fCriterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '''fCriterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.ID", MatchType.InSet, "(" & sessHelper.GetSession("_strSalesmanUniformID") & ")"))
        '''    fCriterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "CreatedTime", MatchType.GreaterOrEqual, New Date(CInt(Date.Today.Year), 1, 1)))
        '''    fCriterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "CreatedTime", MatchType.Lesser, New Date(CInt(Date.Today.Year) + 1, 1, 1)))
        '''    AssignedSalesman = New SalesmanUniformAssignedFacade(User).Retrieve(fCriterias)
        '''    '        fCriterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.ID", MatchType.Exact, objUniform.ID))

        Dim TotalCode As Integer = lblKodePesanan.Text.Split(";").Length

        '''If AssignedSalesman.Count > 0 Then
        '''    For Each item As SalesmanUniformAssigned In AssignedSalesman
        '''        Dim criteriaAssigned As New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '''        criteriaAssigned.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "CreatedTime", MatchType.GreaterOrEqual, New Date(CInt(Date.Today.Year), 1, 1)))
        '''        criteriaAssigned.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "CreatedTime", MatchType.Lesser, New Date(CInt(Date.Today.Year) + 1, 1, 1)))
        '''        criteriaAssigned.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.Exact, item.SalesmanHeader.ID))
        '''        criteriaAssigned.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.ID", MatchType.InSet, "(" & sessHelper.GetSession("_strSalesmanUniformID") & ")"))

        '''        Dim arrAssigned As ArrayList = New SalesmanUniformAssignedFacade(User).Retrieve(criteriaAssigned)
        '''        If TotalCode = arrAssigned.Count Then
        '''            AssignedSalesmanString = AssignedSalesmanString & item.SalesmanHeader.ID.ToString & ";"
        '''        End If
        '''    Next
        '''    If AssignedSalesmanString.Length > 0 Then
        '''        AssignedSalesmanString = AssignedSalesmanString.Substring(0, AssignedSalesmanString.Length - 1)
        '''    End If
        '''Else
        '''    AssignedSalesmanString = String.Empty
        '''End If

        ' menampilkan data salesman yang akan di assign
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        ' default criteria
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, CShort(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif)))

        If lblKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" & lblKodeDealer.Text.Trim.Replace(";", "','") & "')"))
        End If

        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtSalesmanName.Text))
        If ddlPosisi.SelectedIndex = 0 Then
            'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.Code", MatchType.No, KTB.DNet.Lib.WebConfig.GetValue("BManCode")))
            'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.Code", MatchType.No, KTB.DNet.Lib.WebConfig.GetValue("SManCode")))
        Else
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.Code", MatchType.Exact, ddlPosisi.SelectedValue.ToString))
        End If

        'No Branch/SalesManager
        'Start  :CR-Rina:ErrorConverting:string is too long:dna:20091210
        'If AssignedSalesmanString <> String.Empty Then
        '    'Todo Inset
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.NotInSet, "'" & AssignedSalesmanString.Replace(";", "','") & "'"))
        'End If
        AssignedSalesmanString = "sql"
        If AssignedSalesmanString <> String.Empty Then
            Dim Sql As String = ""

            Sql &= " select sh.ID  "
            Sql &= " from SalesmanUniformAssigned sua "
            Sql &= " , SalesmanHeader sh "
            Sql &= " where 1=1  "
            Sql &= " and sua.SalesmanHeaderID=sh.ID "
            Sql &= " and sua.RowStatus=0 "
            Sql &= " and sua.CreatedTime>='" & Format(DateSerial(Now.Year, 1, 1), "yyyy.MM.dd") & "' "
            Sql &= " and sua.CreatedTime<'" & Format(DateSerial(Now.Year + 1, 1, 1), "yyyy.MM.dd") & "' "
            Sql &= " and sua.SalesmanUniformID in (" & sessHelper.GetSession("_strSalesmanUniformID") & ") "

            Sql &= " group by sh.ID having count(*)=" & TotalCode & " "

            'Sql &= " and sh.ID in "
            'Sql &= " ( "
            'Sql &= " select sua.SalesmanHeaderID  "
            'Sql &= " from SalesmanUniformAssigned sua "
            'Sql &= " , SalesmanUniform su "
            'Sql &= " where 1=1 "
            'Sql &= " and sua.SalesmanUniformID=su.ID "
            'Sql &= " and sua.RowStatus=0 "
            'Sql &= " and sua.CreatedTime>='" & Format(DateSerial(Now.Year, 1, 1), "yyyy.MM.dd") & "' "
            'Sql &= " and sua.CreatedTime<'" & Format(DateSerial(Now.Year + 1, 1, 1), "yyyy.MM.dd") & "' "
            'Sql &= " and su.ID in (" & sessHelper.GetSession("_strSalesmanUniformID") & ")"
            'Sql &= " ) "


            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.NotInSet, "(" & Sql & ")" ))
        End If
        'End:CR-Rina:ErrorConverting:string is too long:dna:20091210
        arrList = _SalesmanHeaderFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanUniformReceiver.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanUniformReceiver.DataSource = arrList
        dgSalesmanUniformReceiver.VirtualItemCount = totalRow
        dgSalesmanUniformReceiver.DataBind()

        For Each item As DataGridItem In dgSalesmanUniformReceiver.Items
            Dim lblGenderCode As Label = CType(item.FindControl("lblGenderCode"), Label)
            Dim lblGender As Label = CType(item.FindControl("lblGender"), Label)
            If Not lblGenderCode.Text Is Nothing Then
                If lblGenderCode.Text = "1" Then
                    lblGender.Text = "Pria"
                Else
                    lblGender.Text = "Wanita"
                End If
            End If
        Next
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            lblKodeDealer.Text = Request.Params("d")
            txtDealerCode.Text = Request.Params("d")
            Dim objSalesmanUniform As New SalesmanUniform
            If Request.Params("u") <> "" Then
                If Request.Params("u").Length >= 4 Then 'Multiple
                    lblKodePesanan.Text = Request.Params("u")
                Else
                    Dim objSalesmanUnifDistribution As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(CType(Request.Params("u"), Integer))
                    If Not IsNothing(objSalesmanUnifDistribution) Then
                        lblKodePesanan.Text = objSalesmanUnifDistribution.SalesmanUnifDistributionCode
                    End If

                End If
            End If

            Dim arrTmp As ArrayList
            Dim strSalesmanUniformId As String
            ' mengambil data uniform yang bersangkutan
            Dim CriteriasTmp As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Request.Params("u").Length >= 4 Then 'Multiple
                CriteriasTmp.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.SalesmanUnifDistributionCode", MatchType.InSet, "('" & lblKodePesanan.Text.Trim.Replace(";", "','") & "')"))
            Else
                CriteriasTmp.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.SalesmanUnifDistributionCode", MatchType.Exact, lblKodePesanan.Text))
            End If
            arrTmp = New SalesmanUniformFacade(User).Retrieve(CriteriasTmp)
            If arrTmp.Count > 0 Then
                For Each item As SalesmanUniform In arrTmp
                    _arrSalesmanUniform.Add(item)
                    _strSalesmanUniformID &= item.ID & ","
                Next
                _strSalesmanUniformID = Left(_strSalesmanUniformID, _strSalesmanUniformID.Length - 1)
            End If
            sessHelper.SetSession("_strSalesmanUniformID", _strSalesmanUniformID)
            sessHelper.SetSession("_arrSalesmanUniform", _arrSalesmanUniform)
            BindPositionDropDownLists()
            Initialize()
            BindDataGrid(0)
        End If
    End Sub
    Private Sub dgSalesmanUniformReceiver_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanUniformReceiver.SortCommand
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
        dgSalesmanUniformReceiver.SelectedIndex = -1
        dgSalesmanUniformReceiver.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanUniformReceiver.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanUniformReceiver_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanUniformReceiver.PageIndexChanged
        dgSalesmanUniformReceiver.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanUniformReceiver.CurrentPageIndex)
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgSalesmanUniformReceiver.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub btnPilih_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPilih.Click
        Dim objSalesmanHeader As New SalesmanHeader
        Dim objSalesmanUniform As New SalesmanUniform
        Dim i As Integer = 0
        Dim arrList As ArrayList = New ArrayList

        ''objSalesmanUniform = New SalesmanUniformFacade(User).Retrieve(CType(viewstate("UniformCode"), Integer))
        'Dim Criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "SalesmanUniformCode", MatchType.InSet, "('" & lblKodePesanan.Text.Replace(";", "','") & "')"))

        'Dim arlSalesmanUniform As ArrayList = New SalesmanUniformFacade(User).Retrieve(Criterias)

        For Each item As DataGridItem In dgSalesmanUniformReceiver.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelection"), CheckBox)
            If (chk.Checked) Then
                objSalesmanHeader = _SalesmanHeaderFacade.Retrieve(CInt(dgSalesmanUniformReceiver.DataKeys().Item(i)))
                arrList.Add(objSalesmanHeader)
            End If
            i = i + 1
        Next

        If (arrList.Count > 0) Then
            If objSalesmanUniform Is Nothing Then
                MessageBox.Show("Silakan pilih kode pesanan yg akan di assign ")
                Return
            Else
                If (New SalesmanUniformAssignedFacade(User).AssignUniform(arrList, sessHelper.GetSession("_arrSalesmanUniform")) = -1) Then
                    MessageBox.Show(SR.SaveFail)

                Else
                    Response.Write("<script language=javascript>")
                    Response.Write("alert('Data berhasil dipilih');")
                    Response.Write("window.close();")
                    Response.Write("</script>")

                    'Server.Transfer("~/Salesman/FrmSalesmanUniformAssign.aspx")
                End If
            End If
        Else
            MessageBox.Show("Tidak ada data yg di pilih.")
        End If
    End Sub
#End Region

End Class


