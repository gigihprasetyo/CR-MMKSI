#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text
#End Region

Public Class FrmListPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents icPODateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents dgPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTransferStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents TxtRequestNo As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sHelper As SessionHelper = New SessionHelper

#Region "Custom Method"
    'Private Sub BindDDL()
    '    Dim li As ListItem
    '    Dim al As New ArrayList
    '    li = New ListItem
    '    li.Text = "Silahkan Pilih"
    '    al.Add(li)
    '    li = New ListItem
    '    li.Text = "Part"
    '    al.Add(li)
    '    li = New ListItem
    '    li.Text = "Tools"
    '    al.Add(li)
    '    li = New ListItem
    '    li.Text = "Accessories"
    '    al.Add(li)

    '    ddlTipeBarang.DataSource = al
    '    ddlTipeBarang.DataBind()
    'End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.POIndentPartViewList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART - Daftar PO Indent Part")
        End If
    End Sub

    Dim bCek As Boolean = SecurityProvider.Authorize(Context.User, SR.POIndentPartListTransferSAP_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            sHelper.SetSession("SortCol", "PODate")
            sHelper.SetSession("SortDirect", Sort.SortDirection.ASC)
            bindDDL()
            BindDdlPaymentType()
            btnSubmit.Enabled = bCek
            dgPO.Columns(1).Visible = bCek
        End If
    End Sub

    Private Sub bindDDL()
        ddlTransferStatus.Items.Add(New ListItem("Silakan Pilih", ""))
        ddlTransferStatus.Items.Add(New ListItem("Belum Transfer", "0"))
        ddlTransferStatus.Items.Add(New ListItem("Sudah Transfer", "1"))

        ddlTransferStatus.SelectedIndex = 0


    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteria()
        BindDataGrid(0)
    End Sub

    Private Sub CreateCriteria()


        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SpPO_Indent), "OrderType", MatchType.Exact, "I"))

        If txtPONumber.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "PONumber", MatchType.[Partial], txtPONumber.Text.Trim))
        End If

        If TxtRequestNo.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "RequestNo", MatchType.[Partial], TxtRequestNo.Text.Trim))
        End If

        If txtDealerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        If ddlTransferStatus.SelectedValue = "1" Then
            criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "IndentTransfer", MatchType.Exact, 1))
        ElseIf ddlTransferStatus.SelectedValue = "0" Then
            criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "IndentTransfer", MatchType.No, 1))
        End If

        If Not ddlTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "TermOfPaymentID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If


        criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "PODate", MatchType.GreaterOrEqual, icPODateFrom.Value))
        criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "PODate", MatchType.Lesser, icPODateUntil.Value.AddDays(1)))

        sHelper.SetSession("crits", criterias)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        sHelper.SetSession("arlPO", New KTB.DNet.BusinessFacade.IndentPart.V_SpPO_IndentFacade(User).RetrieveActiveListByCriteria(sHelper.GetSession("crits"), indexPage + 1, dgPO.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirect")))
        dgPO.CurrentPageIndex = indexPage
        dgPO.DataSource = sHelper.GetSession("arlPO")
        dgPO.VirtualItemCount = totalRow
        dgPO.DataBind()

    End Sub

    Private Sub dgPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPO.ItemDataBound
        If e.Item.ItemIndex > -1 Then
            e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dgPO.CurrentPageIndex * dgPO.PageSize)
            Dim itemPO As V_SpPO_Indent = e.Item.DataItem
            Dim lblNoPO As Label = e.Item.FindControl("lblNoPO")
            lblNoPO.Attributes.Add("OnClick", "ShowPODetail(" & itemPO.ID.ToString & ");")

            Dim lblRequestNo As Label = e.Item.FindControl("lblRequestNo")
            lblRequestNo.Attributes.Add("OnClick", "showPopUp('FrmIndentPart.aspx?IndentPartHeaderID=" & itemPO.IndentPartHeaderID & "&view=true&ispopup=1','',600,780,null);")
            'btnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dibatalkan?');")


        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        TransferSAP(False)
        CreateCriteria()
        BindDataGrid(0)
    End Sub

    Private Sub TransferSAP(ByVal isReTransfer As Boolean)
        Dim IpFacade As SparePartPOFacade = New SparePartPOFacade(User)
        Dim arlToSubmit As ArrayList = New ArrayList
        For Each item As DataGridItem In dgPO.Items
            Dim chkItemChecked As CheckBox = item.FindControl("chkItemChecked")

            If chkItemChecked.Checked Then
                Dim objView As V_SpPO_Indent = CType(sHelper.GetSession("arlPO"), ArrayList)(item.ItemIndex)
                Dim obj As SparePartPO = IpFacade.Retrieve(objView.ID)

                If IsNothing(obj.TermOfPayment) Then
                    MessageBox.Show("Cara pembayaran harus diisi")
                    Return
                End If

                If isReTransfer Then
                    If obj.IndentTransfer = 1 Then
                        arlToSubmit.Add(obj)
                    End If
                Else
                    If obj.IndentTransfer <> 1 Then
                        arlToSubmit.Add(obj)
                    End If
                End If
            End If

        Next

        If arlToSubmit.Count = 0 Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Return
        End If

        Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\Sparepart\IndentPart" '& txtPONumber.Text.Substring(1, 4)
        Dim FILE_NAME As String = "" '= FOLDER_NAME + "\E" + txtPONumber.Text + IIf(ddlOrderType.SelectedValue = "E", ".EOD", ".DAT")

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Dim msg As String = String.Empty


        succes = imp.Start()

        If succes Then


            Dim counter As Integer = 0
            Dim result As Integer = 0

            Dim objSparePartPOFacade As New SparePartPOFacade(User)
            For Each item As SparePartPO In arlToSubmit
                counter += 1
                'item.SubmitFile = "request"
                'result = IpFacade.Update(item)
                Dim objInserted As SparePartPO = IpFacade.Retrieve(item.ID)
                FILE_NAME = FOLDER_NAME + "\I" + objInserted.PONumber + DateTime.Now.ToString("MMyymmss") + ".IDP"
                CreateFolder(FOLDER_NAME)
                Dim fs As FileStream = New FileStream(FILE_NAME, FileMode.CreateNew)
                Dim w As StreamWriter = New StreamWriter(fs)

                WritePOHeaderToFile(w, item)
                WritePODetailToFile(w, item)

                w.Close()
                fs.Close()

                item.ProcessCode = "S"
                item.IndentTransfer = 1
                item.SentPODate = Now.Date
                item.IsTransfer = True
                Dim rslt As Integer = objSparePartPOFacade.Update(item)

            Next

            imp.StopImpersonate()
            imp = Nothing

            'MessageBox.Show(arlToSubmit.Count.ToString & " file berhasil dibuat")
            If isReTransfer Then
                MessageBox.Show(counter.ToString & " file berhasil dibuat ulang")
            Else
                MessageBox.Show(counter.ToString & " file berhasil dibuat")
            End If

        Else
            MessageBox.Show("Proses Gagal, Login Server SAP Failed")
        End If
    End Sub

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub


    Private Sub WritePOHeaderToFile(ByRef w As StreamWriter, ByVal objHeader As SparePartPO)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(objHeader.PONumber.PadRight(15, pad))
        sbSetARecord.Append(Left(objHeader.Dealer.DealerName, 25).PadRight(25, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objHeader.PODate))
        sbSetARecord.Append(objHeader.SparePartPODetails.Count.ToString.PadLeft(4, "0"))

        If objHeader.OrderType = "R" Or objHeader.OrderType = "I" Or objHeader.OrderType = "Z" Then
            sbSetARecord.Append(objHeader.TermOfPayment.TermOfPaymentCode)
        End If
        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Sub BindDdlPaymentType()
        Dim listOfPayments As ArrayList = New TermOfPaymentFacade(User).RetrieveActivePaymentTypeList()
        ddlTermOfPayment.DataSource = listOfPayments
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
    End Sub

    Private Function WritePODetailToFile(ByRef w As StreamWriter, ByVal objHeader As SparePartPO)
        Dim _arlPODetail As ArrayList = objHeader.SparePartPODetails
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        Dim indek As Integer = 0
        For Each objPODetail As SparePartPODetail In _arlPODetail
            indek = indek + 1
            sbSetARecord.Remove(0, sbSetARecord.Length)
            sbSetARecord.Append("D")
            sbSetARecord.Append(objPODetail.SparePartPO.PONumber.PadRight(15, pad))
            sbSetARecord.Append(objPODetail.SparePartMaster.PartNumber.PadRight(20, pad))
            sbSetARecord.Append(objPODetail.Quantity.ToString.PadLeft(5, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objPODetail.SparePartPO.PODate)) '(objPODetail.SparePartPO.PODate.ToString.Format("{0:yyyyMMdd}"))
            sbSetARecord.Append(indek.ToString.PadLeft(4, "0"))
            w.WriteLine(sbSetARecord.ToString)
        Next
    End Function

    Private Sub dgPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPO.PageIndexChanged
        dgPO.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgPO.CurrentPageIndex)
    End Sub

    Private Sub dgPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPO.SortCommand
        If CType(sHelper.GetSession("SortCol"), String) = e.SortExpression Then
            Select Case CType(sHelper.GetSession("SortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    sHelper.SetSession("SortDirect", Sort.SortDirection.DESC)

                Case Sort.SortDirection.DESC
                    sHelper.SetSession("SortDirect", Sort.SortDirection.ASC)
            End Select
        Else
            sHelper.SetSession("SortCol", e.SortExpression)
            sHelper.SetSession("SortDirect", Sort.SortDirection.ASC)
        End If
        BindDataGrid(dgPO.CurrentPageIndex)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TransferSAP(True)
        CreateCriteria()
        BindDataGrid(0)
    End Sub
End Class
