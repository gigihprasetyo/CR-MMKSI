Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Web.UI.WebControls

Public Class FrmTOPSPNotaRetur
    Inherits System.Web.UI.Page

    Dim oDealer As Dealer
    Private arlNotaRetur As ArrayList
    Private objNotaRetur As SparepartNotaRetur
    Private _sesshelper As New SessionHelper

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Enabled = False
            txtDealerCode.BorderStyle = BorderStyle.None
            lblSearchDealer.Visible = False
        ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            txtDealerCode.Enabled = True
        End If

        If Not IsPostBack Then
            BindDropDown()
        End If
    End Sub

    Private Sub BindDropDown()
        Dim cri As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTOPSPNotaRetur.Document"))

        Dim arlList As ArrayList = New StandardCodeFacade(User).Retrieve(cri)
        With ddlTipeDokumen.Items
            For Each obj As StandardCode In arlList
                .Add(New ListItem(obj.ValueCode, obj.ValueId))
            Next
        End With
        ddlTipeDokumen.Items.Insert(0, New ListItem("Silahkan Pilih", -1))

        For Each item As ListItem In LookUp.ArraylistMonthCalendar()
            ddlPeriode.Items.Add(item)
        Next

        Dim yearDiff As Integer
        yearDiff = DateTime.Now.Year - 2006
        For Each item As ListItem In LookUp.ArraylistYear(True, yearDiff, 0, DateTime.Now.Year)
            ddlPeriodeYear.Items.Add(item)
        Next

    End Sub

    Private Function GetMonthValue(ByVal strMonth As String) As Integer
        Dim currentMonth As Integer = 0
        Dim arrMonth(12) As String
        arrMonth(0) = "Januari"
        arrMonth(1) = "Februari"
        arrMonth(2) = "Maret"
        arrMonth(3) = "April"
        arrMonth(4) = "Mei"
        arrMonth(5) = "Juni"
        arrMonth(6) = "Juli"
        arrMonth(7) = "Agustus"
        arrMonth(8) = "September"
        arrMonth(9) = "Oktober"
        arrMonth(10) = "November"
        arrMonth(11) = "Desember"

        For Each month As String In arrMonth
            currentMonth = currentMonth + 1
            If (month = strMonth) Then
                Exit For
            End If
        Next

        Return currentMonth
    End Function

    Private Function GetMonthName(ByVal i As Integer) As String
        Select Case i
            Case 0
                Return ""
            Case 1
                Return "Januari"
            Case 2
                Return "Februari"
            Case 3
                Return "Maret"
            Case 4
                Return "April"
            Case 5
                Return "Mei"
            Case 6
                Return "Juni"
            Case 7
                Return "Juli"
            Case 8
                Return "Agustus"
            Case 9
                Return "September"
            Case 10
                Return "Oktober"
            Case 11
                Return "November"
            Case 12
                Return "Desember"
            Case Else
                Return ""
        End Select

    End Function

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim idxPage As Integer = 0
        dtgNotaRetur.CurrentPageIndex = idxPage
        CreateCriteria()
        BindToGrid(idxPage)
    End Sub

    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparepartNotaRetur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        oDealer = Session("DEALER")
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then  '---User is login as Dealer
            criterias.opAnd(New Criteria(GetType(SparepartNotaRetur), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        Else
            If txtDealerCode.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(SparepartNotaRetur), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            End If
        End If

        If txtNoKwitansi.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(SparepartNotaRetur), "NoDoc", MatchType.InSet, "('" & txtNoKwitansi.Text.Replace(";", "','") & "')"))

        If ddlTipeDokumen.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(SparepartNotaRetur), "TypeDoc", MatchType.Exact, "'" & ddlTipeDokumen.SelectedValue & "'"))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparepartNotaRetur), "PeriodeMonth", MatchType.Exact, GetMonthValue(ddlPeriode.SelectedValue)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparepartNotaRetur), "PeriodeYear", MatchType.Exact, CType(ddlPeriodeYear.SelectedValue, Integer)))

        _sesshelper.SetSession("crits", criterias)
    End Sub

    Private Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim arrl As ArrayList

        arlNotaRetur = New SparepartNotaReturFacade(User).RetrieveActiveList(_sesshelper.GetSession("crits"), currentPageIndex + 1, dtgNotaRetur.PageSize, total, _
           CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If Not IsNothing(arlNotaRetur) AndAlso arlNotaRetur.Count > 0 Then
            dtgNotaRetur.VirtualItemCount = total
            dtgNotaRetur.DataSource = arlNotaRetur
            dtgNotaRetur.DataBind()
            _sesshelper.SetSession("ListNR", arlNotaRetur)
        Else
            dtgNotaRetur.DataSource = arrl
            dtgNotaRetur.DataBind()
            _sesshelper.SetSession("ListNR", arrl)
            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private Sub dtgNotaRetur_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgNotaRetur.ItemCommand
        If e.CommandName = "Simpan" Then
            Dim LblFullName As Label = e.Item.FindControl("lblFullName")
            Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
            Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))

            'Dim destFilePath As String = fileInfo1.Directory.FullName & "\DataFile\DepositInterest\" & LblFullName.Text
            Dim destFilePath As String = fileInfo.Directory.FullName & "\" & LblFullName.Text
            Dim newFileInfo As FileInfo = New FileInfo(destFilePath)

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim success As Boolean = False
            success = imp.Start()
            If success Then
                If (newFileInfo.Exists) Then
                    Try
                        imp.StopImpersonate()
                        imp = Nothing
                        Response.Redirect("../Download.aspx?file=" & destFilePath)

                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(LblFullName.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
            Else
                MessageBox.Show("Gagal download file.")
            End If
        End If
    End Sub

    Private Sub dtgNotaRetur_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgNotaRetur.ItemDataBound
        If Not IsNothing(arlNotaRetur) Then
            If (arlNotaRetur.Count > 0 And e.Item.ItemIndex <> -1) Then
                objNotaRetur = arlNotaRetur(e.Item.ItemIndex)
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
                Dim lblTipeDokumen As Label = CType(e.Item.FindControl("lblTipeDokumen"), Label)

                lblNo.Text = (dtgNotaRetur.CurrentPageIndex * dtgNotaRetur.PageSize + e.Item.ItemIndex + 1).ToString()
                lblPeriode.Text = GetMonthName(CInt(objNotaRetur.PeriodeMonth)) & " " & objNotaRetur.PeriodeYear

                Dim arlList As StandardCode = New StandardCodeFacade(User).GetByCategoryValue("EnumTOPSPNotaRetur.Document", objNotaRetur.TypeDoc)
                lblTipeDokumen.Text = arlList.ValueDesc
            End If
        End If
    End Sub

    Private Sub dtgNotaRetur_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgNotaRetur.SortCommand
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
        BindToGrid(dtgNotaRetur.CurrentPageIndex)
    End Sub
End Class