Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Text
Imports System.IO

Public Class FrmTransferPMToSAP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents icPMRelease As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgListPMHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList

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
    Dim arlPMHeader As ArrayList = New ArrayList
    Dim arlPMHeaderTotal As ArrayList = New ArrayList
    Dim arlPMHeaderFilter As ArrayList = New ArrayList
    Dim Total As Integer = 0
    Const V_ChassisMaster_Category_ID As String = "ChassisMaster.Category.ID"
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PMRilisView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PERIODICAL MAINTENANCE - Transfer PM")
        End If
    End Sub

    Dim bCheckBtnPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PMRilisDownload_Privilege)
#End Region

#Region "Custome Method"

    Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As PMHeader In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Sub BindPMHeader()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtDealerCode.Text.Trim() <> String.Empty) Then
            Dim strDealerCode As String() = txtDealerCode.Text.Split(";")
            Dim sCode As String
            For Each item As String In strDealerCode
                sCode += item & "','"
            Next
            sCode = sCode.Trim("'")
            sCode = sCode.Trim(",")

            sCode = "('" + sCode
            sCode += ")"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "Dealer.DealerCode", MatchType.InSet, sCode))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ReleaseDate", MatchType.LesserOrEqual, icPMRelease.Value))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "PMStatus", MatchType.No, CType(EnumPMStatus.PMStatus.Baru, Short)))
        'Modfied by Ikhsan, 25 Juli 2008
        'Requested by Peggy
        'Untuk memastikan bahwa jumlah yang keluar bukan yang baru atau selesai.
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "PMStatus", MatchType.No, CType(EnumPMStatus.PMStatus.Selesai, Short)))
        If Me.ddlCategory.SelectedValue > -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))
            ViewState(V_ChassisMaster_Category_ID) = Me.ddlCategory.SelectedValue
        Else
            ViewState(V_ChassisMaster_Category_ID) = Nothing

        End If
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PMHeader), ViewState("currSortColumn"), ViewState("currSortDirection")))
        Else
            sortColl = Nothing
        End If

        arlPMHeader = New Service.PMHeaderFacade(User).Retrieve(criterias, sortColl)
        Dim DealerCode As String = String.Empty
        For Each item As PMHeader In arlPMHeader
            If (Not IsExist(item.Dealer.DealerCode, arlPMHeaderFilter)) Then
                arlPMHeaderFilter.Add(item)
            End If
        Next
        If (arlPMHeaderFilter.Count > 0) Then
            'Todo session
            Session("PMHeader") = arlPMHeader
            btnDownload.Visible = bCheckBtnPriv
            dtgListPMHeader.Visible = True
            dtgListPMHeader.DataSource = arlPMHeaderFilter
            dtgListPMHeader.DataBind()
        Else
            dtgListPMHeader.Visible = False
            btnDownload.Visible = False
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Function GetTotalPM(ByVal DealerCode As String) As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ReleaseDate", MatchType.LesserOrEqual, icPMRelease.Value))
        'Modfied by Ikhsan, 25 Juli 2008
        'Requested by Peggy
        'Untuk memastikan bahwa jumlah yang keluar bukan yang baru atau selesai.
        '--------------------------------------------------------------------------------------------------------------------------------------
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "PMStatus", MatchType.No, CType(EnumPMStatus.PMStatus.Baru, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "PMStatus", MatchType.No, CType(EnumPMStatus.PMStatus.Selesai, Short)))
        '--------------------------------------------------------------------------------------------------------------------------------------

        ''Append by ali
        ''Ubah Total By Kategori
        '' viewState(V_ChassisMaster_Category_ID)

        If Not IsNothing(viewstate(V_ChassisMaster_Category_ID)) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.Category.ID", MatchType.Exact, viewstate(V_ChassisMaster_Category_ID)))
        End If


        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PMHeader), ViewState("currSortColumn"), ViewState("currSortDirection")))
        Else
            sortColl = Nothing
        End If
        arlPMHeaderTotal = New Service.PMHeaderFacade(User).Retrieve(criterias, sortColl)
        Return arlPMHeaderTotal.Count
    End Function

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If (New Service.PMHeaderFacade(User).UpdateTransaction(arl) <> -1) Then
                    sw = New StreamWriter(DestFile)
                    sw.WriteLine(Val)
                    sw.Close()
                Else
                    success = False
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If (Not IsPostBack) Then
            Me.bindDdlCategory()
            ViewState("currSortColumn") = "Dealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        End If
    End Sub

    Private Sub bindDdlCategory()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim aCs As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        Me.ddlCategory.Items.Clear()
        'Me.ddlCategory.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oC As Category In aCs
            Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
        Next
    End Sub

    Private Sub dtgListPMHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgListPMHeader.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            Dim oPM As PMHeader = CType(e.Item.DataItem, PMHeader)
            Dim lblTotalPM As Label = CType(e.Item.FindControl("lblTotalPM"), Label)
            lblTotalPM.Text = GetTotalPM(oPM.Dealer.DealerCode).ToString()
            Total = Total + Convert.ToInt32(lblTotalPM.Text)
        End If
        If (e.Item.ItemType = ListItemType.Footer) Then
            Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)
            lblTotal.Text = Total.ToString()
        End If
    End Sub

    Private Sub dtgListPMHeader_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgListPMHeader.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        BindPMHeader()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindPMHeader()
    End Sub
    Private Function GetProductCategoryCode() As String
        Dim aPMs As ArrayList = CType(Session("PMHeader"), ArrayList)
        Dim product As String = ""

        For Each oPM As PMHeader In aPMs
            If product = "" Then
                product = oPM.ChassisMaster.Category.ProductCategory.Code
            Else
                If product <> oPM.ChassisMaster.Category.ProductCategory.Code Then
                    Return ""
                End If
            End If
        Next
        Return product
    End Function

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arl As ArrayList = CType(Session("PMHeader"), ArrayList)
        Dim NewArl As ArrayList = New ArrayList
        Dim sb As StringBuilder = New StringBuilder
        'Dim filename = String.Format("{0}{1}{2}", "pmdata", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        ' Modified by Ikhsan 17 Juli 2008
        ' Requested by Peggy
        ' The text is switched between Daftar Status PM and Transfer PM to SAP
        ' Format File : nomor rangka;tanggalpm;KM;dealer;kind
        Dim product As String = Me.GetProductCategoryCode()
        Dim filename = String.Format("{0}{1}{2}{3}", "statusPM", Date.Now.ToString("ddMMyyyyHHmmss"), "_" & product.ToLower(), ".txt")
        Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\" & filename  '-- Destination file to local"

        'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\" & filename  '-- Destination file to SAP

        Dim arlCount As Integer = arl.Count
        Dim iLoop As Integer = 1
        If product = "" Then
            MessageBox.Show("Produk yang akan didownload ulang harus sama")
            Exit Sub
        End If
        For Each item As PMHeader In arl
            item.PMStatus = EnumPMStatus.PMStatus.Proses
            NewArl.Add(item)
            'Dim Str As String = String.Empty
            'For Each item2 As PMDetail In item.PMDetails
            '    Str += item2.ReplecementPartMaster.Code & "-"
            'Next
            'If (Str <> String.Empty) Then
            '    sb.Append(item.ChassisMaster.ChassisNumber & ";" & item.ServiceDate.ToString("dd/MM/yyyy") & ";" & item.StandKM & ";" & Str.Remove(Str.Length - 1, 1) & Chr(13) & Chr(10))
            'Else
            '    sb.Append(item.ChassisMaster.ChassisNumber & ";" & item.ServiceDate.ToString("dd/MM/yyyy") & ";" & item.StandKM & ";" & Chr(13) & Chr(10))
            'End If
            'sb.Append(item.ChassisMaster.ChassisNumber & ";" & item.ServiceDate.ToString("dd/MM/yyyy") & ";" & item.StandKM & ";" & item.PMKindCode & ";" & Chr(13) & Chr(10))
            'sb.Append(item.Dealer.DealerCode & ";" & item.ChassisMaster.ChassisNumber & ";" & item.ServiceDate.ToString("ddMMyyyy") & ";" & item.StandKM & ";" & Chr(13) & Chr(10))
            ' Modified by Ikhsan 17 Juli 2008
            ' Requested by Peggy
            ' The text is switched between Daftar Status PM and Transfer PM to SAP
            ' Format File : nomor rangka;tanggalpm;KM;dealer;kind
            If iLoop <> arlCount Then
                sb.Append(item.ChassisMaster.ChassisNumber & Chr(9) & item.ServiceDate.ToString("ddMMyyyy") & Chr(9) & item.StandKM & Chr(9) & item.Dealer.DealerCode & Chr(9) & "PM" & item.PMKindCode & Chr(9) & item.ReleaseDate.ToString("ddMMyyyy") & Chr(9) & item.VisitType & Chr(13) & Chr(10))
            Else
                sb.Append(item.ChassisMaster.ChassisNumber & Chr(9) & item.ServiceDate.ToString("ddMMyyyy") & Chr(9) & item.StandKM & Chr(9) & item.Dealer.DealerCode & Chr(9) & "PM" & item.PMKindCode & Chr(9) & item.ReleaseDate.ToString("ddMMyyyy") & Chr(9) & item.VisitType & Chr(9))
            End If
            iLoop = iLoop + 1
        Next
        '---modified by ronny 
        '-- try to save file to local 
        Dim objFileStream As New FileStream(DestFile, FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)
        objStreamWriter.WriteLine(sb)
        objStreamWriter.Close()

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & filename)

        'If (Transfer(DestFile, sb.ToString(), NewArl)) Then
        '    MessageBox.Show("Data berhasil di transfer")
        'Else
        '    MessageBox.Show("Data gagal di transfer")
        'End If

        '--end modief
        BindPMHeader()
    End Sub
#End Region



End Class
