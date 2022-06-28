#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.Utility.CommonFunction
Imports KTB.DNet.DataMapper

#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text

#End Region

Public Class FrmSPKMatching
    Inherits System.Web.UI.Page

    Private objDealer As Dealer
    Dim dt As DateTime = DateTime.Now
    Dim suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)

#Region "Custom Method"

    Private Sub bindGrid()
        Dim lst As ArrayList = New ArrayList()
        dgSPKMatching.DataSource = lst
        dgSPKMatching.DataBind()
    End Sub

    Private Sub CheckUserPrivelege()
        If Not SecurityProvider.Authorize(Context.User, SR.SPKChassisMatchingView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SPK Chassis Matching Report")
        End If

        objDealer = Session("DEALER")
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.SPKChassisMatchingDownload_Privilege)
        btnMatch.Visible = SecurityProvider.Authorize(Context.User, SR.SPKChassisMatchingMatch_Privilege) And Not objDealer.IsDealerDMS
        btnUnMatch.Visible = SecurityProvider.Authorize(Context.User, SR.SPKChassisMatchingMatch_Privilege) And Not objDealer.IsDealerDMS
    End Sub

    Private Sub assignAttributeControl()
        lblPopUpDealer.Attributes("onclick") = "ShowPPDealer();"
        lblPopupVehicleType.Attributes("onclick") = "ShowPPTipe();"
        lblPopupKodeWarna.Attributes("onclick") = "ShowPPKodeWarna();"
        lblPopupKodeKonsumen.Attributes("onclick") = "ShowPPCustomerList();"
    End Sub

    Private Function seperatePopUpReturn(ByVal sDealerCodeCollumn As String)
        Dim sDealerCodeTemp() As String = sDealerCodeCollumn.Split(New Char() {";"})
        Dim sDealerCode As String = ""
        For i As Integer = 0 To sDealerCodeTemp.Length - 1
            sDealerCode = sDealerCode & "'" & sDealerCodeTemp(i).Trim() & "'"
            If Not (i = sDealerCodeTemp.Length - 1) Then
                sDealerCode = sDealerCode & ","
            End If
        Next
        sDealerCode = "(" & sDealerCode & ")"
        Return sDealerCode
    End Function

    Private Function validateCriteria(ByRef criterias As CriteriaComposite) As Boolean
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_SPKChassisMatching), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "DealerCode", MatchType.InSet, seperatePopUpReturn(txtKodeDealer.Text.Trim())))
        End If

        If txtKodeKonsumen.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "CustomerCode", MatchType.Exact, txtKodeKonsumen.Text.Trim()))
        End If

        If txtKodeWarna.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "ColorCode", MatchType.Exact, txtKodeWarna.Text.Trim()))
        End If

        If txtMatchNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "MatchingNumber", MatchType.Exact, txtMatchNo.Text.Trim()))
        End If

        If txtNama.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "Name", MatchType.[Partial], txtNama.Text.Trim()))
        End If

        If txtNoChassis.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "ChassisNumber", MatchType.Exact, txtNoChassis.Text.Trim()))
        End If

        If txtNoKunci.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "KeyNumber", MatchType.Exact, txtNoKunci.Text.Trim()))
        End If

        If txtNoMesin.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "EngineNumber", MatchType.Exact, txtNoMesin.Text.Trim()))
        End If

        If txtNoSPK.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "SPKNumber", MatchType.Exact, txtNoSPK.Text.Trim()))
        End If

        If txtRefNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "ReferenceNumber", MatchType.Exact, txtRefNo.Text.Trim()))
        End If

        If txtVehicleType.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "VehicleTypeCode", MatchType.Exact, txtVehicleType.Text.Trim()))
        End If

        If DateDiff(DateInterval.Day, CType(ICDari.Value, Date), CType(ICSampai.Value, Date)) >= 0 Then
            If ICSampai.Value.Subtract(ICDari.Value).Days > 65 Then
                MessageBox.Show("Periode tidak boleh melebihi 65 hari")
                Return False
            Else
                Dim dari As New DateTime(CInt(ICDari.Value.Year), CInt(ICDari.Value.Month), CInt(ICDari.Value.Day), 0, 0, 0)
                Dim sampai As New DateTime(CInt(ICSampai.Value.Year), CInt(ICSampai.Value.Month), CInt(ICSampai.Value.Day), 23, 59, 59)
                criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "MatchingDate", MatchType.GreaterOrEqual, Format(dari, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(VWI_SPKChassisMatching), "MatchingDate", MatchType.LesserOrEqual, Format(sampai, "yyyy-MM-dd HH:mm:ss")))
            End If
        Else
            MessageBox.Show("Periode dari tidak boleh lebih besar daripada periode sampai")
            Return False
        End If

        Return True
    End Function

    Private Sub bindDgSPKMatching(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VWI_SPKChassisMatching), "ID", MatchType.IsNotNull, Nothing))

        If validateCriteria(criterias) Then
            Dim objSessionHelper As New SessionHelper
            Dim sortCol As SortCollection = New SortCollection
            sortCol.Add(New Sort(GetType(VWI_SPKChassisMatching), "DealerCode", Sort.SortDirection.ASC))

            Dim lstResult As ArrayList = New ArrayList
            lstResult = New VWI_SPKChassisMatchingFacade(User).RetrieveByCriteria(criterias, sortCol)

            dgSPKMatching.CurrentPageIndex = indexPage
            dgSPKMatching.VirtualItemCount = lstResult.Count

            If lstResult.Count > 0 Then
                dgSPKMatching.DataSource = lstResult
                dgSPKMatching.DataBind()
                btnDownload.Enabled = True
            Else
                dgSPKMatching.DataSource = New ArrayList
                dgSPKMatching.DataBind()
                btnDownload.Enabled = False
            End If

            objSessionHelper.SetSession("SPKChassisMatchingAL", lstResult)

        End If
    End Sub


    Private Function download() As ArrayList
        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim delimiter As String = Chr(9)
        checkFileExistenceToDownload()

        objAl = CType(Session.Item("SPKChassisMatchingAL"), ArrayList)

        'Write Header
        strText = New StringBuilder
        strText.Append("Dealer")
        strText.Append(delimiter)
        strText.Append("Tanggal Matching")
        strText.Append(delimiter)
        'strText.Append("Kode Konsumen")
        'strText.Append(delimiter)
        'strText.Append("Nama")
        'strText.Append(delimiter)
        strText.Append("Nomor SPK")
        strText.Append(delimiter)
        strText.Append("Nomor Rangka")
        strText.Append(delimiter)
        strText.Append("Nomor Kunci")
        strText.Append(delimiter)
        strText.Append("Kode Tipe")
        strText.Append(delimiter)
        strText.Append("Kode Warna")
        strText.Append(delimiter)
        strText.Append("Deskripsi Produk")
        strText.Append(delimiter)
        strText.Append("Match No")
        strText.Append(delimiter)
        strText.Append("Referensi No")
        strText.Append(delimiter)
        saveToTextFile(strText.ToString())

        For count As Integer = 0 To objAl.Count - 1
            Dim RowValue As VWI_SPKChassisMatching = CType(objAl.Item(count), VWI_SPKChassisMatching)

            strText = New StringBuilder

            Try
                strText.Append(RowValue.DealerCode)
                strText.Append(delimiter)
                strText.Append(RowValue.MatchingDate)
                strText.Append(delimiter)
                'strText.Append(RowValue.CustomerCode)
                'strText.Append(delimiter)
                'strText.Append(RowValue.Name)
                'strText.Append(delimiter)
                strText.Append(RowValue.SPKNumber)
                strText.Append(delimiter)
                strText.Append(RowValue.ChassisNumber)
                strText.Append(delimiter)
                strText.Append(RowValue.KeyNumber)
                strText.Append(delimiter)
                strText.Append(RowValue.VehicleTypeCode)
                strText.Append(delimiter)
                strText.Append(RowValue.ColorCode)
                strText.Append(delimiter)
                strText.Append(RowValue.Description)
                strText.Append(delimiter)
                strText.Append(RowValue.MatchingNumber)
                strText.Append(delimiter)
                strText.Append(RowValue.ReferenceNumber)
                strText.Append(delimiter)
                saveToTextFile(strText.ToString())
            Catch
            End Try
        Next
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\spkchassismatching_" & suffix & ".txt")
        MessageBox.Show("Data Telah Disimpan")
    End Function
    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\spkchassismatching_" & suffix & ".txt")

        If finfo.Exists Then
            finfo.Delete()
        End If

    End Sub
    Private Sub saveToTextFile(ByVal str As String)

        Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\spkchassismatching_" & suffix & ".txt", FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)

        objStreamWriter.WriteLine(str)
        objStreamWriter.Close()

    End Sub

#End Region

#Region "Event Handler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            ViewState("currentSortColumn") = "DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            CheckUserPrivelege()
            assignAttributeControl()
            bindGrid()
        End If
    End Sub

    Protected Sub dgSPKMatching_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgSPKMatching.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblIndex As Label = CType(e.Item.FindControl("lblIndex"), Label)
            lblIndex.Text = e.Item.ItemIndex + 1 + (dgSPKMatching.CurrentPageIndex * dgSPKMatching.PageSize)
        End If

        Dim gridItem As DataGridItem = DirectCast(e.Item, DataGridItem)
        If Not (gridItem Is Nothing) Then
            Dim spkChassis As VWI_SPKChassisMatching = DirectCast(gridItem.DataItem, VWI_SPKChassisMatching)

            If Not (spkChassis Is Nothing) Then
                Select Case spkChassis.MatchingType
                    Case 1
                        gridItem.Cells(11).Text = "Matched"
                    Case 2
                        gridItem.Cells(11).Text = "Unmatched"
                    Case 3
                        gridItem.Cells(11).Text = "Rematched"
                End Select

            End If
        End If


        'If item.MatchingType Then

        'End If
    End Sub

    Protected Sub dgSPKMatching_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgSPKMatching.PageIndexChanged
        dgSPKMatching.SelectedIndex = -1
        dgSPKMatching.CurrentPageIndex = e.NewPageIndex
        bindDgSPKMatching(dgSPKMatching.CurrentPageIndex)
    End Sub



    Protected Sub dgSPKMatching_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgSPKMatching.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End If

        dgSPKMatching.SelectedIndex = -1
        dgSPKMatching.CurrentPageIndex = 0
        bindDgSPKMatching(dgSPKMatching.CurrentPageIndex)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Try
            download()
            bindDgSPKMatching(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        dgSPKMatching.CurrentPageIndex = 0
        bindDgSPKMatching(0)
    End Sub

#End Region

    Protected Sub btnMatch_Click(sender As Object, e As EventArgs) Handles btnMatch.Click
        'Request.QueryString.Remove("mode")
        Server.Transfer("~/FinishUnit/FrmSPKMatchingMatch.aspx?mode=match")
    End Sub

    Protected Sub btnUnMatch_Click(sender As Object, e As EventArgs) Handles btnUnMatch.Click
        Server.Transfer("~/FinishUnit/FrmSPKMatchingMatch.aspx?mode=unmatch")
    End Sub


End Class