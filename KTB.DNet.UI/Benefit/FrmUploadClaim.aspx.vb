Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Utility
Imports KTB.DNET.Security
Imports KTB.DNET.BusinessFacade.Benefit


Imports Excel

Imports System.IO
Imports KTB.DNET.BusinessFacade.FinishUnit

Public Class FrmUploadClaim
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents fileUploadExcel As System.Web.UI.WebControls.FileUpload
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents arrayCheck As System.Web.UI.WebControls.HiddenField

    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label

    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatusLeasing As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button

    Protected WithEvents PanelError As System.Web.UI.WebControls.Panel
    Protected WithEvents lblpanelError As System.Web.UI.WebControls.Label

    Protected WithEvents LinkDownload As System.Web.UI.WebControls.LinkButton

    Protected WithEvents ddlLeasing As System.Web.UI.WebControls.DropDownList

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

    Private objDomainDetail As BenefitClaimDetails = New BenefitClaimDetails
    Private objDomainDetailFacade As BenefitClaimDetailsFacade = New BenefitClaimDetailsFacade(User)
#Region "Private Property"

    Private inputuploadleasing_privillage As Boolean
#End Region





#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=BENEFIT - UPLOAD CLAIM")
        'End If
        inputuploadleasing_privillage = SecurityProvider.Authorize(Context.User, SR.inputuploadleasing_privillage)

        If Not inputuploadleasing_privillage Then
           
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Upload Leasing")
             
        End If
    End Sub

    Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege)
#End Region

    Private Sub BindeLeasing()
        Dim facade As New LeasingCompanyFacade(User)
        Dim arlFacade As ArrayList = facade.RetrieveActiveList()

        ddlLeasing.Items.Clear()

        For Each cat As LeasingCompany In arlFacade
            ddlLeasing.Items.Add(New ListItem(cat.LeasingName, cat.ID.ToString))
        Next
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then

            ViewState("currentSortColumn") = "Name"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            'ViewState("currentSortDirection") = Sort.SortDirection.DESC



            BindeLeasing()


            BindDataGrid()


        End If
    End Sub


    Private Sub BindDataGrid()
        Dim list As ArrayList = New ArrayList
        Dim list2 As ArrayList = CType(sessHelper.GetSession("DetailClaimExcelSession"), ArrayList)
        If Not list2 Is Nothing Then
            For Each Items As BenefitClaimDetails In list2
                list.Add(Items)
            Next
        End If
        dgTable.DataSource = list
        dgTable.DataBind()
    End Sub

    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitClaimDetails = CType(e.Item.DataItem, BenefitClaimDetails)

            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then
                    Dim temp As String = ""
                    'Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name='rb'>")
                    'e.Item.Cells(0).Controls.Add(rdbChoice)

                    'Dim lblIDoGridDetil As Label = CType(e.Item.FindControl("lblIDoGridDetil"), Label)
                    'lblIDoGridDetil.Text = objDomain2.ID.ToString

                    Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                    lblNo.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString

                    Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                    'For Each el As BenefitMasterDealer In objDomain2.BenefitClaimHeader.BenefitMasterDetail.BenefitMasterHeader.BenefitMasterDealers
                    '    temp += el.Dealer.DealerCode & ";"
                    'Next

                    Dim lblNoClaimReg As Label = CType(e.Item.FindControl("lblNoClaimReg"), Label)


                    Dim lblLeasing As Label = CType(e.Item.FindControl("lblLeasing"), Label)
                    'For Each el As BenefitMasterLeasing In objDomain2.BenefitClaimHeader.BenefitMasterDetail.BenefitMasterLeasings
                    '    temp += el.LeasingCompany.LeasingName & ";"
                    'Next

                    If Not IsNothing(objDomain2.BenefitClaimHeader) Then
                        temp = objDomain2.BenefitClaimHeader.Dealer.DealerCode
                        lblDealer.Text = temp
                        lblNoClaimReg.Text = objDomain2.BenefitClaimHeader.ClaimRegNo
                        temp = ""
                        If Not objDomain2.BenefitClaimHeader.LeasingCompany Is Nothing Then
                            temp = objDomain2.BenefitClaimHeader.LeasingCompany.LeasingName

                            lblLeasing.Text = temp
                        End If
                    Else
                        lblDealer.Text = objDomain2.ChassisMaster.Dealer.DealerCode
                    End If


                    Dim lblNoRangka As Label = CType(e.Item.FindControl("lblNoRangka"), Label)
                    lblNoRangka.Text = objDomain2.ChassisMaster.ChassisNumber

                    Dim lblDeskripsi As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)
                    lblDeskripsi.Text = objDomain2.ChassisMaster.VechileColor.MaterialDescription

                    Dim lblNoMesin As Label = CType(e.Item.FindControl("lblNoMesin"), Label)
                    lblNoMesin.Text = objDomain2.ChassisMaster.EngineNumber

                    Dim lblFakturDate As Label = CType(e.Item.FindControl("lblFakturDate"), Label)
                    lblFakturDate.Text = objDomain2.ChassisMaster.DODate.ToString("dd/MM/yyyy")

                    Dim lblValidasiDate As Label = CType(e.Item.FindControl("lblValidasiDate"), Label)
                    lblValidasiDate.Text = objDomain2.ChassisMaster.ValidateDateText

                    Dim lblCustomer As Label = CType(e.Item.FindControl("lblCustomer"), Label)
                    lblCustomer.Text = objDomain2.ChassisMaster.EndCustNameText

                    Dim lblKota As Label = CType(e.Item.FindControl("lblKota"), Label)
                    lblKota.Text = objDomain2.ChassisMaster.EndCustomer.Customer.City.CityName

                    Dim lblCheck As Label = CType(e.Item.FindControl("lblCheck"), Label)
                    lblCheck.Text = objDomain2.ErrorMessage
                    If objDomain2.StatusUpload <> 1 Then
                        e.Item.BackColor = Color.Red
                    End If

                    'If objDomain2.StatusUpload = 1 Then
                    '    lblCheck.Text = "OK"
                    'ElseIf objDomain2.StatusUpload = 2 Then
                    '    lblCheck.Text = "Double Claim"
                    '    e.Item.BackColor = Color.Red
                    'ElseIf objDomain2.StatusUpload = 3 Then
                    '    lblCheck.Text = "Beda Leasing"
                    '    e.Item.BackColor = Color.Red
                    'ElseIf objDomain2.StatusUpload = 4 Then
                    '    lblCheck.Text = "Belum pernah diajukan"
                    '    e.Item.BackColor = Color.Red
                    'End If

                End If
            End If
        End If
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

        Dim retValue As Integer = 0
        If fileUploadExcel.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If fileUploadExcel.PostedFile.ContentLength <> fileUploadExcel.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim datetimenow As String = Now.ToString("yyyy_MM_dd_H_mm_ss")

                Dim directory As String = KTB.DNET.Lib.WebConfig.GetValue("SAN") & "SalesCampaign_Benefit"
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)

                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(fileUploadExcel.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0
                    Throw New Exception("Salah Extention")
                End If

                Dim targetFile As String = New System.Text.StringBuilder(directory). _
                    Append("\").Append(datetimenow + "_" + _
                                       Path.GetFileName(fileUploadExcel.PostedFile.FileName)).ToString

                fileUploadExcel.PostedFile.SaveAs(targetFile)

                Dim objReader As IExcelDataReader = Nothing
                Dim list As ArrayList = New ArrayList
                Dim checkSalah As Boolean = False
                Dim checkKosong As Boolean = True
                Dim totalUpload As Integer = 0

                Dim i As Integer = 0

                Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)

                    '   objReader = ExcelReaderFactory.CreateBinaryReader(stream)

                    If ext.ToLower.Contains("xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    Else
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    End If


                    If (Not IsNothing(objReader)) Then
                        Dim temp As String = ""
                        PanelError.GroupingText = ""
                        lblpanelError.Text = ""

                        Dim objLeasingHeader As LeasingCompany = New LeasingCompanyFacade(User).Retrieve(CShort(ddlLeasing.SelectedValue))
                        Dim NoRangkaEmpty As String = ""
                        Dim loops As Integer = 0
                        While objReader.Read()

                            If (i = 3) Then
                                If objReader.GetString(0) = "bs1dnet" Then
                                    checkSalah = True
                                End If
                            End If

                            If (i > 4) Then
                                checkKosong = False

                                'add by anh 201601301 validasi upload leasing pake sp
                                If 1 = 1 Then
                                    objDomainDetail = New BenefitClaimDetails
                                    Dim objFacade As New BenefitClaimDetailsFacade(User)

                                    If Not objReader.GetString(1).Trim = "" Then

                                        Dim aSCs As ArrayList

                                        Dim objSP As sp_checkInputClaimFacade = New sp_checkInputClaimFacade(User)
                                        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                                        Dim objChassisMaster As ChassisMaster = objChassisMasterFacade.Retrieve(objReader.GetString(1))

                                        If Not objChassisMaster Is Nothing And objChassisMaster.ChassisNumber <> "" Then
                                            objDomainDetail.ChassisMaster = objChassisMaster
                                            objDomainDetail.ErrorMessage = ""
                                            aSCs = objSP.UploadLeasingValidation(CInt(ddlLeasing.SelectedValue), objChassisMaster.ChassisNumber, 0)
                                            If aSCs.Count > 0 Then
                                                Dim objInputClaim As sp_checkInputClaim = CType(aSCs(0), sp_checkInputClaim)
                                                If Not objInputClaim Is Nothing Then
                                                    Dim ObjLeasingCompany As New LeasingCompany
                                                    ObjLeasingCompany = New LeasingCompanyFacade(User).Retrieve(CShort(ddlLeasing.SelectedValue))
                                                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                                    criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "ChassisMaster.ChassisNumber", MatchType.Exact, objChassisMaster.ChassisNumber))
                                                    criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimHeader.BenefitType.EventValidation", MatchType.Exact, 0))
                                                    '   criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "LeasingCompany.ID", MatchType.Greater, 0))
                                                    Dim arrBenefitClaimDetails As New ArrayList
                                                    arrBenefitClaimDetails = New BenefitClaimDetailsFacade(User).Retrieve(criterias)
                                                    If arrBenefitClaimDetails.Count > 0 Then
                                                        'If CType(arrBenefitClaimDetails(0), BenefitClaimDetails).BenefitClaimHeader.Status <> BenefitClaimHeaderEnumStatus.Status.Selesai Then
                                                        objDomainDetail.BenefitClaimHeader = CType(arrBenefitClaimDetails(0), BenefitClaimDetails).BenefitClaimHeader
                                                        objDomainDetail.BenefitClaimHeader.LeasingCompany = ObjLeasingCompany
                                                        objDomainDetail.ID = CType(arrBenefitClaimDetails(0), BenefitClaimDetails).ID
                                                        objDomainDetail.BenefitMasterDetail = CType(arrBenefitClaimDetails(0), BenefitClaimDetails).BenefitMasterDetail
                                                        objDomainDetail.RecLetterRegNo = CType(arrBenefitClaimDetails(0), BenefitClaimDetails).RecLetterRegNo
                                                        objDomainDetail.CreatedBy = CType(arrBenefitClaimDetails(0), BenefitClaimDetails).CreatedBy
                                                        objDomainDetail.DescDealer = CType(arrBenefitClaimDetails(0), BenefitClaimDetails).DescDealer
                                                        objDomainDetail.DescKtb = CType(arrBenefitClaimDetails(0), BenefitClaimDetails).DescKtb
                                                        'End If

                                                    End If
                                                    '    objDomainDetail.BenefitClaimHeader = Nothing 'BenefitClaimHeader.ClaimRegNo


                                                    'objDomainDetail.LeasingCompany = 'RetriveddlEaseingValue
                                                    'objDomainDetail.BenefitClaimHeader =  get dari no rangka di benefit claim detail
                                                    objDomainDetail.StatusUpload = objInputClaim.IsValid
                                                    objDomainDetail.ErrorMessage = objInputClaim.Message
                                                End If
                                            End If
                                        Else
                                            If loops > 0 Then
                                                NoRangkaEmpty += ","
                                            End If
                                            NoRangkaEmpty += objReader.GetString(1)
                                            objDomainDetail.StatusUpload = 5
                                            objDomainDetail.ErrorMessage = "No rangka tidak sesuai"
                                            loops += 1
                                        End If
                                    Else
                                        objDomainDetail.StatusUpload = 6
                                        objDomainDetail.ErrorMessage = "No rangka tidak boleh kosong"
                                    End If
                                    list.Add(objDomainDetail)


                                End If

                                If 1 = 2 Then 'add by anh 20160301 'rencana mau pake sp
                                    If Not objReader.GetString(1).Trim = "" Then
                                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


                                        If Not objReader.GetString(1).Trim = "" Then
                                            Dim strSql As String = ""
                                            strSql += " select ID from ChassisMaster where ChassisNumber"
                                            strSql += " in ('" & objReader.GetString(1) & "')"
                                            criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "ChassisMaster", MatchType.InSet, "(" & strSql & ")"))

                                        End If

                                        If Not objReader.GetString(1).Trim = "" Then
                                            Dim strSql As String = ""
                                            strSql += " select a.id from BenefitClaimHeader a"
                                            strSql += " inner join BenefitClaimDetails aa on a.ID = aa.BenefitClaimHeaderID"
                                            strSql += " inner join BenefitMasterDetail b on aa.BenefitMasterDetailID = b.ID"
                                            strSql += " inner join BenefitMasterLeasing e on b.ID = e.BenefitMasterDetailID "
                                            strSql += " inner join BenefitMasterDealer c on b.BenefitMasterHeaderID = c.BenefitMasterHeaderID"
                                            strSql += " inner join Dealer d on c.DealerID = d.id"
                                            strSql += " where a.rowstatus = 0 and e.LeasingCompanyID is not null "
                                            strSql += " and  aa.rowstatus = 0 and b.rowstatus = 0 "
                                            'strSql += " and  e.rowstatus = 0 and c.rowstatus = 0 "
                                            'strSql += " and d.DealerCode in ('" & objReader.GetString(1) & "')"
                                            'strSql += " and d.DealerCode in ('" & objReader.GetString(1) & "')"
                                            criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimHeader", MatchType.InSet, "(" & strSql & ")"))

                                        End If

                                        Dim objDomainDetail1 As ArrayList = objDomainDetailFacade.Retrieve(criterias)
                                        If Not objDomainDetail1 Is Nothing And objDomainDetail1.Count > 0 Then
                                            For Each el As BenefitClaimDetails In objDomainDetail1
                                                el.StatusUpload = 1
                                                Dim objLeasing As BenefitClaimHeader = New BenefitClaimHeaderFacade(User).Retrieve(CInt(el.BenefitClaimHeader.ID))
                                                If Not objLeasing.LeasingCompany Is Nothing Then
                                                    If Not objLeasing.LeasingCompany.ID = CShort(ddlLeasing.SelectedValue) Then
                                                        el.StatusUpload = 3
                                                    End If
                                                Else
                                                    el.StatusUpload = 2
                                                End If

                                                If Not objLeasingHeader Is Nothing Then
                                                    el.LeasingCompany = objLeasingHeader
                                                End If
                                                list.Add(el)
                                            Next
                                        Else
                                            temp += "Nomor Rangka : " & objReader.GetString(1) & " <br />"
                                        End If
                                    Else
                                        temp += "Nomor Rangka : " & objReader.GetString(1) & " <br />"
                                    End If
                                End If

                            End If
                    i = i + 1
                        End While
                        If Not temp = "" Then
                            PanelError.GroupingText = "<b>No Rangka (excel) yang tidak ada dalam DATA CLAIM : </b><br /> "
                            lblpanelError.Text = temp
                        End If

                        If Not NoRangkaEmpty = "" Then
                            MessageBox.Show("No rangka " + NoRangkaEmpty + " tidak sesuai")
                        End If
                    End If

                End Using

                If checkSalah = False Then
                    MessageBox.Show("Silakan gunakan template yang tersedia.")
                    btnSimpan.Visible = False
                    btnProses.Visible = btnSimpan.Visible
                    Return
                End If
                If checkKosong = True Then
                    MessageBox.Show("Data Excel tidak boleh ada yang kosong.")
                    btnSimpan.Visible = False
                    btnProses.Visible = btnSimpan.Visible
                    Return
                End If

                lblTotalUnit.Text = i - 5 'list.Count

                btnSimpan.Visible = True
                btnProses.Visible = btnSimpan.Visible
                sessHelper.SetSession("DetailClaimExcelSession", list)

                'BindDataGrid(CInt(sessHelper.GetSession("IDBenefitListHeader")))
                BindDataGrid()

                retValue = 1
            Catch ex As Exception
                retValue = 0
            Finally
                sapImp.StopImpersonate()
                sapImp = Nothing
            End Try


        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim list As New ArrayList
        If Not sessHelper.GetSession("DetailClaimExcelSession") Is Nothing Then
            list = CType(sessHelper.GetSession("DetailClaimExcelSession"), ArrayList)
        End If
        If list.Count > 0 Then
            Dim n As Integer = New BenefitClaimDetailsFacade(User).UpdateStatusUpload(list)

            If n = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else

                MessageBox.Show("Sukses tersimpan")

            End If
        Else
            MessageBox.Show("Tidak ada data")
        End If




    End Sub

    Private Sub btnProses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Dim list As New ArrayList
        Dim listcheck As New ArrayList

        If Not sessHelper.GetSession("DetailClaimExcelSession") Is Nothing Then
            list = CType(sessHelper.GetSession("DetailClaimExcelSession"), ArrayList)
        End If

        For Each item As String In arrayCheck.Value.Replace(" ", "").Split(";")
            If Not item Is Nothing And Not item = "" Then
                listcheck.Add(item)
            End If
        Next



        'Dim n As Integer = New BenefitClaimHeaderFacade(User).UpdateStatus1(list, listcheck, ddlAccuered.SelectedValue, _
        '                                                              icDateBayar.Value, ddlStatusTransfer.SelectedValue)
        Dim n As Integer = New BenefitClaimDetailsFacade(User).UpdateStatus(list, listcheck, ddlStatusLeasing.SelectedValue)


    End Sub

    Private Sub LinkDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkDownload.Click


        Response.Redirect("../downloadlocal.aspx?file=Benefit\UploadClaim.xls")

    End Sub

End Class
