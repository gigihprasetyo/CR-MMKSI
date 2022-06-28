#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.SPAF
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Drawing.Color
#End Region

Public Class FrmMasterKondisiList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents dgMasterKondisi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents chkTglBerlaku As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icTglBerlaku As Intimedia.WebCC.IntiCalendar
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dfMasterKondisi As System.Web.UI.HtmlControls.HtmlInputFile
    Private bIsError As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Custom Method"

    Private Sub BindDropDownTipe()
        ddlTipe.Items.Clear()
        ddlTipe.DataSource = New VechileTypeFacade(User).RetrieveActiveList()
        ddlTipe.DataTextField = "VechileCodeDesc"
        ddlTipe.DataValueField = "ID"
        ddlTipe.DataBind()
        ddlTipe.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub BindDropDownStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Active", DBRowStatus.Active.ToString))
        ddlStatus.Items.Add(New ListItem("Not Active", DBRowStatus.Deleted.ToString))
    End Sub
    Private Sub Initialize()
        btnSimpan.Enabled = False
        btnDownload.Enabled = False
        icTglBerlaku.Value = DateTime.Today
    End Sub

    Private Sub BindUpload()
        ViewState.Add("dgType", "Upload")

        If (Not dfMasterKondisi.PostedFile Is Nothing) And (dfMasterKondisi.PostedFile.ContentLength > 0) And _
        ((dfMasterKondisi.PostedFile.ContentType.ToLower() = "text/plain") Or (dfMasterKondisi.PostedFile.ContentType.ToLower() = "text/csv") _
        Or (dfMasterKondisi.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (dfMasterKondisi.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) Then
            Dim Extension As String = Path.GetExtension(dfMasterKondisi.PostedFile.FileName)

            If Extension.ToUpper = ".TXT" Then

                Dim SrcFile As String = Path.GetFileName(dfMasterKondisi.PostedFile.FileName)
                Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile
                Try

                    ''---------- Pake UploadToWebServer saja -----------
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(dfMasterKondisi.PostedFile.InputStream, DestFile)

                    Dim parser As IParser = New ConditionMasterParser
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)

                    'periksa tidak boleh ada chassis# dan kind# yang sama di tabel ConditionMaster
                    Dim NewList As ArrayList = New ArrayList
                    NewList = CheckExistingInMasterKondisiList(arList)
                    AddUserCreator(NewList)

                    'periksa kalo ada record rangkap (chassis# dan kind# yang sama) di arraylist
                    CheckDoubleRows(NewList)

                    _sessHelper.SetSession("sessMasterKondisi", NewList)
                    BinddgMasterKondisi(0)


                Catch Exc As Exception
                    MessageBox.Show(SR.UploadFail(SrcFile))
                End Try
            Else
                MessageBox.Show("Jenis file tidak sesuai")
            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If

    End Sub

    Private Sub AddUserCreator(ByRef arrL As ArrayList)
        For Each objConditionMaster As ConditionMaster In arrL
            objConditionMaster.CreatedBy = CType(User.Identity.Name, String)
        Next
    End Sub
    Private Sub CheckDoubleRows(ByRef NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each objConditionMaster As ConditionMaster In NewList
            If Not IsNothing(objConditionMaster.VechileType) And Not IsNothing(objConditionMaster.ValidFrom) Then
                For nIndex = nIterate To NewList.Count - 1
                    Dim objConditionMaster2 As ConditionMaster
                    objConditionMaster2 = NewList(nIndex)
                    If Not IsNothing(objConditionMaster2.VechileType) And Not IsNothing(objConditionMaster2.ValidFrom) Then
                        Dim sVechileTypeCode2 = objConditionMaster2.VechileType.VechileTypeCode
                        Dim sVechileTypeCode1 = objConditionMaster.VechileType.VechileTypeCode

                        Dim sValidDate1 = objConditionMaster.ValidFrom
                        Dim sValidDate2 = objConditionMaster2.ValidFrom

                        If sVechileTypeCode1 = sVechileTypeCode2 And sValidDate1 = sValidDate2 Then
                            If objConditionMaster2.ErrorMessage = "" Then
                                objConditionMaster2.ErrorMessage = "Data FS Ganda dg Record " + CType(nIterate, String)
                            Else
                                objConditionMaster2.ErrorMessage = objConditionMaster2.ErrorMessage + ";<br> Data FS Ganda dg Record " + CType(nIterate, String)
                            End If
                        End If
                    End If
                Next
            End If
            nIterate = nIterate + 1
        Next

    End Sub
    Private Function CheckExistingInMasterKondisiList(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        For Each objConditionMaster As ConditionMaster In arList
            If Not IsNothing(objConditionMaster.VechileType) And Not IsNothing(objConditionMaster.ValidFrom) Then
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "VechileType.ID", MatchType.Exact, objConditionMaster.VechileType.ID))
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "ValidFrom", MatchType.Exact, objConditionMaster.ValidFrom))

                Dim ConditionMasterCollection As ArrayList = New ConditionMasterFacade(User).Retrieve(criterias2)

                If ConditionMasterCollection.Count > 0 Then
                    If objConditionMaster.ErrorMessage = "" Then
                        objConditionMaster.ErrorMessage = "Data FS Sudah Terdaftar"
                    Else
                        objConditionMaster.ErrorMessage = objConditionMaster.ErrorMessage + ";<br> Data FS Sudah Terdaftar"
                    End If
                    TmpList.Add(objConditionMaster)
                Else
                    TmpList.Add(objConditionMaster)
                End If

            Else
                TmpList.Add(objConditionMaster)
            End If
        Next
        Return TmpList

    End Function
    Private Sub BinddgMasterKondisi(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0
        If (CType(_sessHelper.GetSession("dgType"), String) = "Upload") Then
            totalRow = CType(_sessHelper.GetSession("sessMasterKondisi"), ArrayList).Count
            dgMasterKondisi.DataSource = CType(_sessHelper.GetSession("sessMasterKondisi"), ArrayList)
            dgMasterKondisi.Columns(dgMasterKondisi.Columns.Count - 2).Visible = True
            dgMasterKondisi.Columns(dgMasterKondisi.Columns.Count - 1).Visible = False
            btnDownload.Enabled = False
        ElseIf (CType(_sessHelper.GetSession("dgType"), String) = "Retrieve") Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(ConditionMaster), "RowStatus", MatchType.Exact, ddlStatus.SelectedValue))
            If ddlTipe.SelectedValue <> -1 Then
                criterias.opAnd(New Criteria(GetType(ConditionMaster), "VechileType.ID", MatchType.Exact, ddlTipe.SelectedValue))
            End If
            If chkTglBerlaku.Checked = True Then
                criterias.opAnd(New Criteria(GetType(ConditionMaster), "ValidFrom", MatchType.GreaterOrEqual, icTglBerlaku.Value))
            End If

            Dim ConditionMasterColl As ArrayList = New ConditionMasterFacade(User).Retrieve(criterias)
            totalRow = ConditionMasterColl.Count
            dgMasterKondisi.DataSource = ConditionMasterColl
            dgMasterKondisi.Columns(dgMasterKondisi.Columns.Count - 2).Visible = False
            dgMasterKondisi.Columns(dgMasterKondisi.Columns.Count - 1).Visible = True
            btnDownload.Enabled = True
        End If
        dgMasterKondisi.VirtualItemCount = totalRow
        dgMasterKondisi.CurrentPageIndex = pageIndeks
        dgMasterKondisi.DataBind()
    End Sub
    Private Function IsExistCode(ByVal VechileTypeID As Integer, ByVal ValidDate As DateTime) As Boolean
        Dim objConditionMasterFacade As New ConditionMasterFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "VechileType.ID", MatchType.Exact, VechileTypeID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ValidFrom", MatchType.Exact, ValidDate))
        Dim TestExist As ArrayList = objConditionMasterFacade.Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function



#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            BindDropDownTipe()
            BindDropDownStatus()
            Initialize()
        End If
    End Sub
    Private Sub chkTglBerlaku_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTglBerlaku.CheckedChanged
        If chkTglBerlaku.Checked = True Then
            icTglBerlaku.Enabled = True
        Else
            icTglBerlaku.Enabled = True
        End If
    End Sub
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        bIsError = False
        BindUpload()
        bIsError = CType(_sessHelper.GetSession("sessError"), Boolean)
        If Not bIsError And Path.GetFileName(dfMasterKondisi.PostedFile.FileName).ToString.Trim <> String.Empty Then
            btnSimpan.Enabled = True
        Else
            btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub dgMasterKondisi_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMasterKondisi.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As ConditionMaster = CType(e.Item.DataItem, ConditionMaster)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgMasterKondisi.CurrentPageIndex * dgMasterKondisi.PageSize)

                Dim lblTglBerlaku As Label = CType(e.Item.FindControl("lblTglBerlaku"), Label)
                If RowValue.ValidFrom = New DateTime(1900, 1, 1) Or RowValue.ValidFrom = System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString() Then
                    lblTglBerlaku.Text = ""
                Else
                    If RowValue.ValidFrom < New Date(1900, 1, 1) Then
                        lblTglBerlaku.Text = ""
                    Else
                        lblTglBerlaku.Text = RowValue.ValidFrom
                    End If
                End If

                Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
                If Not RowValue.ErrorMessage <> "" Then
                    lblMessage.Text = "OK"
                    lblMessage.BackColor = GreenYellow
                Else
                    lblMessage.BackColor = LightSalmon
                    bIsError = True
                End If
            End If
            _sessHelper.SetSession("sessError", bIsError)
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim nResult As Integer
        Dim objConditionMasterFacade As New ConditionMasterFacade(User)
        Dim bCheckSuccess As Integer
        Dim arList As ArrayList = CType(_sessHelper.GetSession("sessMasterKondisi"), ArrayList)

        bCheckSuccess = objConditionMasterFacade.Insert(arList)

        If bCheckSuccess > 0 Then
            MessageBox.Show("Proses simpan berhasil!")
        Else
            MessageBox.Show("Proses simpan gagal!")
        End If
        btnSimpan.Enabled = False
    End Sub
#End Region


    '                    lblPopUp.Visible = True
    ''lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmPopUpPMDetail.aspx?id=0&dc=" & RowValue.Dealer.DealerCode & "&cn=" & RowValue.ChassisMaster.ChassisNumber & "&dt=" & RowValue.ServiceDate.ToString, "", 310, 500, "ShowPopUp")
    '                lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmPopUpPMDetail.aspx?id=0&index=" & e.Item.ItemIndex, "", 310, 500, "ShowPopUp")

End Class