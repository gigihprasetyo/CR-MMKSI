#Region " Summary "
'--------------------------------------------------'
'-- Program Code : FrmFSCampaign.aspx            --'
'-- Program Name : Daftar Parameter Free Service --'
'-- Description  :                               --'
'--------------------------------------------------'
'-- Programmer   : Anna Nurhayanto               --'
'-- Start Date   : Aug 27, 2010                  --'
'-- Update By    :                               --'
'-- Last Update  : Aug 27, 2010                  --'
'--------------------------------------------------'

#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO.FileInfo
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text

#End Region



Public Class FrmServiceReminder
    Inherits System.Web.UI.Page

#Region " Private Variables"
    Private sessHelper As SessionHelper = New SessionHelper
    Private m_bFormPrivilege As Boolean = False
    Private m_bCreatePrivilege As Boolean = False
    Private m_bUpdatePrivilege As Boolean = False
    Private m_bActivatePrivilege As Boolean = False
    Private strOpenFakturFrom As String = ""
    Private strOpenFakturto As String = ""
    Private strPKTdateFrom As String = ""
    Private strPKTdateTo As String = ""
#End Region

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        checkprivilege()

        If Not IsPostBack Then
            ddlFSKindBind()
            cleardata()
            ViewState("CurrentSortColumn") = "KindDescription"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Dim cri As New CriteriaComposite(New Criteria(GetType(up_RetrieveFreeService_Service_Reminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If setcriteria(cri) Then
                bindGrid(0)
            End If
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Dim cri As New CriteriaComposite(New Criteria(GetType(up_RetrieveFreeService_Service_Reminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If setcriteria(cri) Then
            bindGrid(0)
        End If
        bindGrid(0)
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim cri As New CriteriaComposite(New Criteria(GetType(up_RetrieveFreeService_Service_Reminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arr As ArrayList
        If setcriteria(cri) Then
            bindGrid(0)
        End If
        arr = retrievealldata()
        DoDownload(arr)
    End Sub

    Private Sub dtgFSCampaign_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgFSReminder.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objRS As up_RetrieveFreeService_Service_Reminder = CType(e.Item.DataItem, up_RetrieveFreeService_Service_Reminder)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                Dim lblCustomerName As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                Dim lblPhoneNo As Label = CType(e.Item.FindControl("lblPhoneNo"), Label)
                Dim lblchassisNumber As Label = CType(e.Item.FindControl("lblchassisNumber"), Label)
                Dim lblFSKind As Label = CType(e.Item.FindControl("lblFSKind"), Label)
                Dim lblOpenFakturDate As Label = CType(e.Item.FindControl("lblOpenFakturDate"), Label)
                Dim lblFakturDate As Label = CType(e.Item.FindControl("lblFakturDate"), Label)
                Dim lblPKTDate As Label = CType(e.Item.FindControl("lblPKTDate"), Label)
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblKM As Label = CType(e.Item.FindControl("lblKM"), Label)
                Dim lblExpiredDateByOpenFakturDate As Label = CType(e.Item.FindControl("lblExpiredDateByOpenFakturDate"), Label)
                Dim lblExpiredDateByPKTDate As Label = CType(e.Item.FindControl("lblExpiredDateByPKTDate"), Label)

                lblNo.Text = (e.Item.ItemIndex + 1) + (dtgFSReminder.CurrentPageIndex * dtgFSReminder.PageSize)
                If Year(objRS.PKTDate) = 1753 Then
                    lblPKTDate.Text = ""
                Else
                    lblPKTDate.Text = objRS.PKTDate.ToString("dd/MM/yyyy")
                End If

                lblchassisNumber.Text = objRS.ChassisNumber
                lblOpenFakturDate.Text = objRS.OpenFakturDate.ToString("dd/MM/yyyy")
                lblCustomerName.Text = objRS.Name
                lblFSKind.Text = objRS.KindDescription
                lblPhoneNo.Text = objRS.NoHP
                lblCity.Text = objRS.CityName
                lblKM.Text = objRS.KM_LAST
                lblFakturDate.Text = objRS.FakturDate.ToString("dd/MM/yyyy")
                lblExpiredDateByOpenFakturDate.Text = objRS.ExpiredDateByOpenFakturDate.ToString("dd/MM/yyyy")
                If Year(objRS.PKTDate) = 1753 Then
                    lblExpiredDateByPKTDate.Text = ""
                Else
                    lblExpiredDateByPKTDate.Text = objRS.ExpiredDateByPKTDate.ToString("dd/MM/yyyy")
                End If


            End If

        End If
    End Sub

    Private Sub dtgFSCampaign_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgFSReminder.SortCommand
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
        dtgFSReminder.SelectedIndex = -1
        dtgFSReminder.CurrentPageIndex = 0
        bindGrid(dtgFSReminder.CurrentPageIndex)
        cleardata()
    End Sub

    Private Sub dtgFSReminder_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFSReminder.PageIndexChanged
        dtgFSReminder.CurrentPageIndex = e.NewPageIndex
        bindGrid(dtgFSReminder.CurrentPageIndex)
    End Sub

#End Region

#Region "custom"

    Private Sub ddlFSKindBind()
        ddlFSKind.Items.Clear()


        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(FSKind), "KindCode", Sort.SortDirection.ASC))
        Dim objCategory As ArrayList = New FSKindFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each oneCategory As FSKind In objCategory
            li = New ListItem(oneCategory.KindCode + " - " + oneCategory.KindDescription, oneCategory.ID)
            ddlFSKind.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih kategori", "0")
        ddlFSKind.Items.Insert(0, li)
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        Dim strFileNm As String
        Dim strFileNmHeader As String
        strFileNm = "Daftar Free Service Reminder"

        If Not IsNothing(sessHelper.GetSession("strFileNmHeader")) Then
            strFileNmHeader = CType(sessHelper.GetSession("strFileNmHeader"), String)
        End If

        sFileName = strFileNm & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim ListData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        Dim _user As String
        _user = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String
        _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String
        _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            'If imp.Start() Then

            Dim finfo As FileInfo = New FileInfo(ListData)
            If finfo.Exists Then
                finfo.Delete()
            End If

            Dim fs As FileStream = New FileStream(ListData, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)

            WriteListData(sw, data, strFileNmHeader)
            sw.Close()
            fs.Close()

            'imp.StopImpersonate()
            'imp = Nothing
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub

    Private Sub WriteListData(ByVal sw As StreamWriter, ByVal data As ArrayList, ByVal strFileNmHeader As String)
        Dim tab As Char = Chr(9)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Dim err As String
        Dim itemLine As StringBuilder = New StringBuilder
        Dim objSmanFacade As New up_RetrieveFreeService_Service_ReminderFacade(User)

        Try


            If Not IsNothing(data) Then
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(strFileNmHeader)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(" " & tab & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("No" & tab)
                itemLine.Append("Nama Konsumen" & tab)
                itemLine.Append("No HP " & tab)
                itemLine.Append("Kota" & tab)
                itemLine.Append("Nomor Chassis" & tab)
                itemLine.Append("Tanggal Buka Faktur" & tab)
                itemLine.Append("Tanggal PKT" & tab)
                itemLine.Append("Tipe FS" & tab)
                itemLine.Append("Tgl Expired dr tgl Buka Faktur" & tab)
                itemLine.Append("Tgl Expired dr PKT Date" & tab)
                itemLine.Append("KM" & tab)
                sw.WriteLine(itemLine.ToString())

                Dim i As Integer = 1
                For Each item As up_RetrieveFreeService_Service_Reminder In data
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.Name & tab)
                    itemLine.Append(item.NoHP & tab)
                    itemLine.Append(item.CityName & tab)
                    itemLine.Append(item.ChassisNumber & tab)
                    itemLine.Append(Format(item.OpenFakturDate, "dd/MM/yyyy").ToString & tab)
                    itemLine.Append(Format(item.PKTDate, "dd/MM/yyyy").ToString & tab)
                    itemLine.Append(Format(item.KindDescription).ToString & tab)
                    itemLine.Append(Format(item.ExpiredDateByOpenFakturDate, "dd/MM/yyyy").ToString & tab)
                    itemLine.Append(Format(item.ExpiredDateByPKTDate, "dd/MM/yyyy").ToString & tab)
                    itemLine.Append(Format(item.KM_LAST).ToString & tab)
                    
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1

                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message + "  //  " + " Data Berikut :  " + err + "  Invalid")
        End Try
    End Sub

    Private Sub checkprivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FreeService_Service_Reminder_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Service")
        End If
    End Sub

    Private Function setcriteria(ByRef cri As CriteriaComposite) As Boolean

        If ddlFSKind.SelectedIndex > 0 Then
            cri.opAnd(New Criteria(GetType(up_RetrieveFreeService_Service_Reminder), "FSKindID", MatchType.Exact, ddlFSKind.SelectedValue))
        End If

        If chkFactureDate.Checked Then
            If icOpenFacturDateFrom.Value > icOpenFacturDateTo.Value Then
                MessageBox.Show("Pilihan Anda pada Tanggal Open Faktur tidak valid")
                Return False
            End If
            strOpenFakturFrom = icOpenFacturDateFrom.Value.ToString("yyyyMMdd")
            strOpenFakturto = icOpenFacturDateTo.Value.ToString("yyyyMMdd")
        Else
            'strOpenFakturFrom = DateAdd(DateInterval.Day, -90, Date.Now).ToString("yyyyMMdd")
            'strOpenFakturto = Date.Now.ToString("yyyyMMdd")
        End If

        If chkPKTDate.Checked Then
            If icPktFrom.Value > icPKTTo.Value Then
                MessageBox.Show("Pilihan Anda pada Tanggal PKT tidak valid")
                Return False
            End If
            strPKTdateFrom = icPktFrom.Value.ToString("yyyyMMdd")
            strPKTdateTo = icPKTTo.Value.ToString("yyyyMMdd")
        Else
            'strPKTdateFrom = DateAdd(DateInterval.Day, -90, Date.Now).ToString("yyyyMMdd")
            'strPKTdateTo = Date.Now.ToString("yyyyMMdd")
        End If
        Return True
    End Function

    Private Sub bindGrid(ByVal pageindex As Integer)
        Dim strChassisnumber As String = txtChassissNumber.Text.Trim
        Dim totalRow As Integer = 0
        If (pageindex >= 0) Then
            Dim _ListserviceReminder As New ArrayList
            _ListserviceReminder = New up_RetrieveFreeService_Service_ReminderFacade(User).retrieve_SP(CType(sessHelper.GetSession("DEALER"), Dealer).ID,
                                                                                                       ddlFSKind.SelectedValue,
                                                                                                       strchassisnumber,
                                                                                                                        strOpenFakturFrom,
                                                                                                                        strOpenFakturto,
                                                                                                                        strPKTdateFrom,
                                                                                                                        strPKTdateTo,
                                                                                                                        pageindex + 1,
                                                                                                                        dtgFSReminder.PageSize,
                                                                                                                        totalRow,
                                                                                                                        CType(ViewState("CurrentSortColumn"), String),
                                                                                                                        CType(ViewState("currSortDirection"), Sort.SortDirection))

            sessHelper.SetSession("listReminder", _ListserviceReminder)
            dtgFSReminder.DataSource = _ListserviceReminder
            dtgFSReminder.VirtualItemCount = totalRow
            dtgFSReminder.DataBind()
        
        End If
    End Sub

    Private Function retrievealldata() As ArrayList
        Dim strChassisnumber As String = txtChassissNumber.Text.Trim
        Dim totalRow As Integer = 0

        Dim _ListserviceReminder As New ArrayList
        _ListserviceReminder = New up_RetrieveFreeService_Service_ReminderFacade(User).retrieve_SP_ALL(CType(sessHelper.GetSession("DEALER"), Dealer).ID,
                                                                                                   ddlFSKind.SelectedValue,
                                                                                                   strChassisnumber,
                                                                                                                    strOpenFakturFrom,
                                                                                                                    strOpenFakturto,
                                                                                                                    strPKTdateFrom,
                                                                                                                    strPKTdateTo,
                                                                                                                    0,
                                                                                                                    dtgFSReminder.PageSize,
                                                                                                                    totalRow,
                                                                                                                    CType(ViewState("CurrentSortColumn"), String),
                                                                                                                    CType(ViewState("currSortDirection"), Sort.SortDirection))
        Return _ListserviceReminder
    End Function

    Private Sub cleardata()
        txtChassissNumber.Text = String.Empty
        icOpenFacturDateFrom.Value = Date.Now
        icOpenFacturDateTo.Value = Date.Now
        'icPktFrom.Value = DateAdd(DateInterval.Day, -90, Date.Now)
        icPktFrom.Value = Date.Now
        icPKTTo.Value = Date.Now
    End Sub



#End Region
End Class