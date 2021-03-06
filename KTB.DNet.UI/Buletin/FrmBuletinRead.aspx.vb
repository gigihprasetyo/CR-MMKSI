#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade.Buletin
Imports KTB.DNET.Utility
Imports KTB.DNET.Parser
Imports KTB.DNET.Security
Imports System.Drawing.Color
Imports KTB.DNET.BusinessFacade.Helper
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.UserManagement

#End Region
Public Class FrmBuletinRead
    Inherits System.Web.UI.Page
    Private TypedKeyword As String
    Dim sessHelp As SessionHelper = New SessionHelper
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJudul As System.Web.UI.WebControls.TextBox
    Dim ArlRead As ArrayList = New ArrayList

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents DdlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DdlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgBuletinList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKeyWords As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.BuletinViewList_Privilege) Then '-- Check User Privilege
                Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Buletin")
            End If
            PopulateParent()
            PopulateSelectionCategory(0)
            ReadCriteria()
        End If
    End Sub


    'Private Sub PopulatePeriodMonth()
    '    DdlMonth.Items.Add(New ListItem("Pilih Bulan", 0))
    '    For Each item As ListItem In LookUp.ArrayMonth
    '        Me.DdlMonth.Items.Add(item)
    '    Next
    'End Sub
    Private Sub PopulateParent()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim list As ArrayList = _BuletinFacade.RetrieveParentList(org.Title)
        Dim li As New ListItem("Pilih Kategori", "0")
        DdlCategory.Items.Add(li)
        If list.Count > 0 Then
            For Each item As BuletinCategory In list
                If item.Status = 0 Then
                    li = New ListItem
                    li.Text = item.Code
                    li.Value = item.ID
                    DdlCategory.Items.Add(li)
                End If

            Next
        End If
    End Sub
    Private Sub DdlCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DdlCategory.SelectedIndexChanged
        If DdlCategory.Items.Count > 0 Then
            Dim iSelect As Integer = CInt(DdlCategory.SelectedValue)
            PopulateSelectionCategory(iSelect)
        Else
            'MessageBox.Show(SR.DataNotFound("Data"))
        End If
    End Sub
    Private Function BuildLeadingSpace(ByVal count As Integer) As String
        Dim space As String = String.Empty
        If count > 1 Then
            For i As Integer = 0 To count - 2
                space += "--"
            Next
            space = space & ">"
        End If
        Return space
    End Function
    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)
        '-- Sort arraylist

        'If SortColumn.Trim <> "" Then
        '    Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
        '    Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
        '    pCompletelist.Sort(objListComparer)
        'End If

    End Sub
    Private Sub BindDatagrid(ByVal pageIndex As Integer)
        Dim BuletinStat As Integer = 1
        Dim sortColl As SortCollection = New SortCollection
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim strBuletinID As String = ""

        'Dim criteriaMember As New CriteriaComposite(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criteriaMember.opAnd(New Criteria(GetType(BuletinMember), "UserInfo.ID", MatchType.Exact, objUserInfo.ID))
        'Dim arlBuletinMember As ArrayList = New BuletinMemberFacade(User).Retrieve(criteriaMember)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select BuletinCategoryID from BuletinDetail where UserGroupID in (Select UserGroupID from UserGroupMember where UserID=" & objUserInfo.ID & "))"))
        'Dim arlBuletinMember As ArrayList = New BuletinFacade(User).Retrieve(criteriaMember)

        'For Each item As BuletinMember In arlBuletinMember
        '    strBuletinID = strBuletinID & item.Buletin.ID.ToString & ","
        'Next

        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If strBuletinID <> "" Then
        '    strBuletinID = Left(strBuletinID, strBuletinID.Length - 1)
        '    criterias.opAnd(New Criteria(GetType(Buletin), "ID", MatchType.InSet, "(" & strBuletinID & ")"))
        'End If

        sortColl.Add(New Sort(GetType(Buletin), "UploadDate", Sort.SortDirection.ASC))
        Dim ArlBuletinList As ArrayList = New BuletinFacade(User).Retrieve(criterias, sortColl)
        Me.dgBuletinList.DataSource = ArlBuletinList
        Me.dgBuletinList.VirtualItemCount = 5
        Me.dgBuletinList.CurrentPageIndex = pageIndex
        Me.dgBuletinList.DataBind()
    End Sub
    'Private Sub ReadSignCriteria()
    '    Dim ObjDealer As Dealer = sessHelp.GetSession("DEALER")
    '    Dim AllEverRead As ArrayList
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(BuletinOrganization), "Dealer.ID", MatchType.Exact, ObjDealer.ID))
    '    AllEverRead = New BuletinOrganizationFacade(User).Retrieve(criterias)
    '    sessHelp.SetSession("BuletinRead", AllEverRead)
    'End Sub
    Private Sub dgBuletinList_ItemDataBound1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBuletinList.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(2).Text = (dgBuletinList.CurrentPageIndex * dgBuletinList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            Dim BuletinList As ArrayList = CType(sessHelp.GetSession("BuletinList"), ArrayList)
            Dim objBuletinList As Buletin = BuletinList(e.Item.ItemIndex)
            'indikator jika tidak ada filename-nya
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim lbtnIsRead As Label = CType(e.Item.FindControl("lbtnIsRead"), Label)
            Dim lbtnIsNotRead As Label = CType(e.Item.FindControl("lbtnIsNotRead"), Label)
            If objBuletinList.FileName = "" Then
                lbtnDownload.Visible = False
                lbtnIsRead.Visible = False
                lbtnIsNotRead.Visible = False
            Else
                lbtnDownload.Visible = True

                'add by ery for indicator
                UpdateIndicatorStatus(CInt(e.Item.Cells(0).Text), e)
            End If
        End If
    End Sub

    Private Sub UpdateIndicatorStatus(ByVal BuletinID As Integer, ByVal e As DataGridItemEventArgs)
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BuletinHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BuletinHistory), "Buletin.ID", MatchType.Exact, BuletinID))

        Dim lbtnIsRead As Label = CType(e.Item.FindControl("lbtnIsRead"), Label)
        Dim lbtnIsNotRead As Label = CType(e.Item.FindControl("lbtnIsNotRead"), Label)
        Dim arrBuletinHistory As New ArrayList
        Dim facade As BuletinHistoryFacade = New BuletinHistoryFacade(User)
        arrBuletinHistory = facade.Retrieve(criterias)
        If arrBuletinHistory.Count > 0 Then
            Dim objBuletinHistory As BuletinHistory = arrBuletinHistory(0)

            If objBuletinHistory.ReadCount = 0 Then
                lbtnIsNotRead.Visible = True
                lbtnIsRead.Visible = False
            Else
                lbtnIsNotRead.Visible = False
                lbtnIsRead.Visible = True
            End If
        Else
            lbtnIsNotRead.Visible = True
            lbtnIsRead.Visible = False
        End If
    End Sub


    Private Sub UpdateIndicatorStatusOld(ByVal BuletinID As Integer, ByVal e As DataGridItemEventArgs)
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BuletinMember), "Buletin.ID", MatchType.Exact, BuletinID))
        criterias.opAnd(New Criteria(GetType(BuletinMember), "UserInfo.ID", MatchType.Exact, objUserInfo.ID))

        Dim lbtnIsRead As Label = CType(e.Item.FindControl("lbtnIsRead"), Label)
        Dim lbtnIsNotRead As Label = CType(e.Item.FindControl("lbtnIsNotRead"), Label)
        Dim arrBuletinMember As New ArrayList
        Dim facade As BuletinMemberFacade = New BuletinMemberFacade(User)
        arrBuletinMember = facade.Retrieve(criterias)
        If arrBuletinMember.Count > 0 Then
            Dim objBuletinMember As BuletinMember = arrBuletinMember(0)

            If objBuletinMember.ReadStatus = 0 Then
                lbtnIsNotRead.Visible = True
                lbtnIsRead.Visible = False
            Else
                lbtnIsNotRead.Visible = False
                lbtnIsRead.Visible = True
            End If
        Else
            lbtnIsNotRead.Visible = True
            lbtnIsRead.Visible = False
        End If
    End Sub

    Private Sub dgUserList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBuletinList.ItemCommand
        If e.CommandName = "Download" Then
            Dim objBuletin As Buletin
            objBuletin = New Buletin

            Dim objBuletinCat As BuletinCategory
            Dim ObjBuletinFac As BuletinFacade = New BuletinFacade(User)
            Dim objBuletinCatFac As BuletinCategoryFacade = New BuletinCategoryFacade(User)
            Dim ObjCatCode As Integer
            objBuletin = ObjBuletinFac.Retrieve(CInt(e.Item.Cells(0).Text))
            ObjCatCode = objBuletin.BuletinCategory.ID
            objBuletinCat = objBuletinCatFac.Retrieve(ObjCatCode)

            Dim SrcFile As String = e.Item.Cells(1).Text.Trim
            Dim DestFolder As String
            Dim listParentCategory As ArrayList = objBuletinCatFac.RetrieveAllParentCategory(objBuletinCat)
            If listParentCategory.Count > 0 Then
                For i As Integer = 0 To listParentCategory.Count - 1
                    DestFolder = DestFolder & "/" & CType(listParentCategory.Item(listParentCategory.Count - 1 - i), BuletinCategory).Code
                Next
            End If
            DestFolder = DestFolder & "/" & objBuletinCat.Code & "/" & SrcFile
            DestFolder = Replace(DestFolder, "/", "\")

            DownloadFile(DestFolder, True, e.Item.Cells(0).Text)

            'BindDatagrid(dgBuletinList.CurrentPageIndex)
        End If

    End Sub


    Private Sub DownloadFile(ByVal fname As String, ByVal forceDownload As Boolean, ByVal BuletinID As Integer)

        Dim path As Path
        'comment by heru for path SAN
        'Dim fullpath As String = Server.MapPath("../" + _
        '    Replace(KTB.DNet.Lib.WebConfig.GetValue("BuletinDestFileDirectory"), "/", "\"))
        Dim fullpath As String = KTB.DNET.Lib.WebConfig.GetValue("SAN") + _
            Replace(KTB.DNET.Lib.WebConfig.GetValue("BuletinDestFileDirectory"), "/", "\")

        fullpath = fullpath & Replace(fname, "/", "\")
        Dim name = path.GetFileName(fullpath)
        Dim ext = path.GetExtension(fullpath)
        Dim type As String = ""
        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start() Then
            Dim FILENAME As String = fullpath
            Dim FileCheck As FileInfo = New FileInfo(fullpath)
            If FileCheck.Exists = True Then
                If SaveOrgReader(BuletinID) = 1 Then
                    If Not IsDBNull(ext) Then
                        ext = LCase(ext)
                    End If
                    Select Case ext
                        Case ".htm", ".html"
                            type = "text/HTML"
                        Case ".txt"
                            type = "text/plain"
                        Case ".doc", ".rtf"
                            type = "Application/msword"
                        Case ".csv", ".xls"
                            type = "Application/x-msexcel"
                        Case ".exe"
                            MessageBox.Show("File bermasalah tidak dapat di download")
                            Exit Sub
                        Case Else
                            type = "application/x-download"
                    End Select
                    If (forceDownload) Then
                        Response.AppendHeader("content-disposition", _
                        "attachment; filename=" + name)
                    End If
                    If type <> "" Then
                        Response.ContentType = type
                    End If
                    Response.WriteFile(fullpath)
                    UpdateReadStatusMember(BuletinID)
                    Response.End()
                    'MessageBox.Show("Berhasil meng-copy file")
                    imp.StopImpersonate()
                    imp = Nothing
                Else
                    MessageBox.Show("User tidak dikenali")
                End If
            Else
                MessageBox.Show("File tidak tersedia")
            End If
        Else
            MessageBox.Show("File tidak dapat diakses")
        End If
    End Sub

    Private Sub UpdateReadStatusMember(ByVal BuletinID As Integer)
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BuletinHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BuletinHistory), "Buletin.ID", MatchType.Exact, BuletinID))

        Dim facade As BuletinHistoryFacade = New BuletinHistoryFacade(User)
        Dim result As Integer = 0

        Dim objBuletinHistory As BuletinHistory
        Dim arrBuletin As New ArrayList

        arrBuletin = New BuletinHistoryFacade(User).Retrieve(criterias)
        If Not arrBuletin Is Nothing Then
            If arrBuletin.Count > 0 Then
                objBuletinHistory = arrBuletin(0)

                objBuletinHistory.ReadCount = objBuletinHistory.ReadCount + 1

                If objBuletinHistory.ID = 0 Then
                    objBuletinHistory.Buletin = New BuletinFacade(User).Retrieve(BuletinID)
                    objBuletinHistory.ReadCount = 1
                    result = facade.Insert(objBuletinHistory)
                Else
                    'If objBuletinMember.ReadStatus = 0 Then
                    result = facade.Update(objBuletinHistory)
                    'End 'If
                End If

                If result = -1 Then
                    MessageBox.Show("Status Read Member Gagal Diupdate")
                End If
            End If
        End If
    End Sub

    Private Sub UpdateReadStatusMemberOld(ByVal BuletinID As Integer)
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BuletinMember), "Buletin.ID", MatchType.Exact, BuletinID))
        criterias.opAnd(New Criteria(GetType(BuletinMember), "UserInfo.ID", MatchType.Exact, objUserInfo.ID))

        Dim facade As BuletinMemberFacade = New BuletinMemberFacade(User)
        Dim result As Integer = 0

        Dim objBuletinMember As BuletinMember
        Dim arrBuletin As New ArrayList

        arrBuletin = New BuletinMemberFacade(User).Retrieve(criterias)
        If Not arrBuletin Is Nothing Then
            If arrBuletin.Count > 0 Then
                objBuletinMember = arrBuletin(0)

                objBuletinMember.ReadStatus = 1
                objBuletinMember.ReadTime = Date.Now

                If objBuletinMember.ID = 0 Then
                    objBuletinMember.Buletin = New BuletinFacade(User).Retrieve(BuletinID)
                    objBuletinMember.UserInfo = objUserInfo
                    result = facade.Insert(objBuletinMember)
                Else
                    'If objBuletinMember.ReadStatus = 0 Then
                    result = facade.Update(objBuletinMember)
                    'End 'If
                End If

                If result = -1 Then
                    MessageBox.Show("Status Read Member Gagal Diupdate")
                End If
            End If
        End If
    End Sub

    Private Function SaveOrgReader(ByVal BuletinID As Integer) As Integer
        Dim ReaderIdentity As BuletinOrganization = New BuletinOrganization
        Dim ObjDealer As Dealer = sessHelp.GetSession("DEALER")
        Dim _BuletinOrgFacade As BuletinOrganizationFacade
        Dim Nresult As Integer
        ReaderIdentity.Dealer = ObjDealer
        ReaderIdentity.Buletin = New BuletinFacade(User).Retrieve(BuletinID)
        ReaderIdentity.OpenBy = User.Identity.Name
        ReaderIdentity.OpenDate = Date.Now
        ReaderIdentity.RowStatus = DBRowStatus.Active
        ReaderIdentity.CreatedBy = User.Identity.Name
        ReaderIdentity.CreatedTime = Date.Now
        ReaderIdentity.LastUpdateBy = User.Identity.Name
        ReaderIdentity.LastUpdateTime = Date.Now
        Nresult = New BuletinOrganizationFacade(User).Insert(ReaderIdentity)
        Return Nresult
    End Function
    Private Sub ReadData2()
        'Get Buletin assigned to this user
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim strBuletinID As String = ""
        Dim isAssigned As Boolean = False
        Dim strUserGroupID As String = String.Empty
        Dim strBuletinCategory As String = String.Empty
        Dim arlCategory As ArrayList = GetBuletinCategoryList(objUserInfo)

        Dim BuletinList As ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.Status", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(Buletin), "ID", MatchType.InSet, "(select buletinid from buletinmember where userid=" & objUserInfo.ID & ")"))

        criterias.opAnd(New Criteria(GetType(Buletin), "ValidFrom", MatchType.LesserOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 23, 59, 59)))
        criterias.opAnd(New Criteria(GetType(Buletin), "ValidTo", MatchType.GreaterOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 0, 0, 0)))

        '-- keywords
        If txtKeyWords.Text.Trim() <> "" Then
            Dim item As String
            For Each item In Split(Me.txtKeyWords.Text, ",")
                criterias.opAnd(New Criteria(GetType(Buletin), "Keywords", MatchType.[Partial], item))
            Next
        End If

        '--description
        If txtDescription.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Description", MatchType.[Partial], txtDescription.Text.Trim))
        End If

        '--judul
        If txtJudul.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Title", MatchType.[Partial], txtJudul.Text.Trim))
        End If

        Dim iSelected As Integer = 0
        If DdlCategory.Items.Count > 0 Then
            iSelected = CInt(DdlCategory.SelectedValue)
            If iSelected > 0 Then
                'criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select id from buletincategory where topparent=" & iSelected & ")"))
                criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.TopParent", MatchType.Exact, iSelected), "(", True)
                criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, iSelected), ")", False)
            End If
        End If


        Dim iSelectedSub As Integer = 0
        'If DdlSubCategory.Items.Count > 0 Then
        '    iSelectedSub = CInt(DdlSubCategory.SelectedValue)
        '    If iSelectedSub > 0 Then
        '        criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select id from buletincategory where id=" & iSelectedSub & ")"))
        '    End If
        'End If

        If DdlSubCategory.Items.Count > 0 Then
            iSelectedSub = CInt(DdlSubCategory.SelectedValue)
            If iSelectedSub > 0 Then
                Dim ArlCatID As ArrayList
                Dim ArlAllList As ArrayList = New ArrayList
                ArlCatID = New BuletinCategoryFacade(User).RetrieveAllCategory(iSelectedSub, ArlAllList)
                Dim BuletinCatSelection As BuletinCategory = New BuletinCategoryFacade(User).Retrieve(iSelectedSub)
                ArlCatID.Add(BuletinCatSelection)
                If ArlCatID.Count > 0 Then
                    Dim Intloop As Integer = 1
                    For Each item As BuletinCategory In ArlCatID
                        If Intloop = 1 And ArlCatID.Count = 1 Then
                            criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID))
                        ElseIf Intloop = 1 And ArlCatID.Count > 1 And Intloop <> ArlCatID.Count Then
                            criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID), "(", True)
                        ElseIf Intloop = ArlCatID.Count And ArlCatID.Count > 1 Then
                            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID), ")", False)
                        Else
                            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID))
                        End If
                        Intloop = Intloop + 1
                    Next
                End If
            End If
        End If

        criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select BuletinCategoryID from BuletinDetail where UserGroupID in (Select UserGroupID from UserGroupMember where UserID=" & objUserInfo.ID & "))"))
        '-- Sorted bycriterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select BuletinCategoryID from BuletinDetail where UserGroupID in (Select UserGroupID from UserGroupMember where UserID=" & objUserInfo.ID & "))"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Buletin), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))

        BuletinList = New BuletinFacade(User).Retrieve(criterias, sortColl)
        If BuletinList.Count = 0 Then
            BuletinList = New ArrayList
        End If

        '-- Store UserList into session for later use
        sessHelp.SetSession("BuletinList", BuletinList)

        If BuletinList.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub ReadData()
        'Get Buletin assigned to this user
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim strBuletinID As String = ""
        Dim strUserGroupID As String = String.Empty
        Dim strBuletinCategory As String = String.Empty

        Dim criteriaGroupMember As New CriteriaComposite(New Criteria(GetType(BuletinGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaGroupMember.opAnd(New Criteria(GetType(BuletinGroupMember), "UserGroup.ID", MatchType.InSet, "(select UserGroupID from UserGroupMember where userid=" & objUserInfo.ID & ")"))
        Dim arlBuletinGroupMember As ArrayList = New BuletinGroupMemberFacade(User).Retrieve(criteriaGroupMember)

        If strBuletinID <> "" Then  'User has Buletin Member
            If arlBuletinGroupMember.Count > 0 Then
                strBuletinID = strBuletinID.Remove(strBuletinID.Length - 1, 1)
            End If
        End If

        Dim BuletinList As ArrayList

        Dim CurrentDate As Date = Date.Now.Date
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.Status", MatchType.Exact, 0))

        If arlBuletinGroupMember.Count > 0 Then
            criterias.opAnd(New Criteria(GetType(Buletin), "ID", MatchType.InSet, "(select buletinid from buletingroupmember where usergroupid in (Select UserGroupID from UserGroupMember where UserID=" & objUserInfo.ID & "))"), "(", True)
            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select BuletinCategoryID from BuletinDetail where UserGroupID in (Select UserGroupID from UserGroupMember where UserID=" & objUserInfo.ID & "))"), ")", False)
        Else
            criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select BuletinCategoryID from BuletinDetail where UserGroupID in (Select UserGroupID from UserGroupMember where UserID=" & objUserInfo.ID & "))"))
        End If

        '-- Read all data selected
        '-- Row status = activehttp://localhost/KTB.DNet/Buletin/FrmBuletinRead.aspx

        '-- Include main category

        Dim iSelectedSub As Integer = 0

        If DdlSubCategory.Items.Count > 0 Then
            iSelectedSub = CInt(DdlSubCategory.SelectedValue)
            If iSelectedSub > 0 Then
                Dim ArlCatID As ArrayList
                Dim ArlAllList As ArrayList = New ArrayList
                ArlCatID = New BuletinCategoryFacade(User).RetrieveAllCategory(iSelectedSub, ArlAllList)
                Dim BuletinCatSelection As BuletinCategory = New BuletinCategoryFacade(User).Retrieve(iSelectedSub)
                ArlCatID.Add(BuletinCatSelection)
                If ArlCatID.Count > 0 Then
                    Dim Intloop As Integer = 1
                    For Each item As BuletinCategory In ArlCatID
                        If Intloop = 1 And ArlCatID.Count = 1 Then
                            criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID))
                        ElseIf Intloop = 1 And ArlCatID.Count > 1 And Intloop <> ArlCatID.Count Then
                            criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID), "(", True)
                        ElseIf Intloop = ArlCatID.Count And ArlCatID.Count > 1 Then
                            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID), ")", False)
                        Else
                            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID))
                        End If
                        Intloop = Intloop + 1
                    Next
                End If
            End If
        End If

        Dim iSelected As Integer = 0
        'Dim IselectedTopParent As Integer = 0
        If DdlCategory.Items.Count > 0 Then
            iSelected = CInt(DdlCategory.SelectedValue)
            If iSelectedSub = 0 Then
                If iSelected > 0 Then
                    'Dim BuletinCategory As BuletinCategory = New BuletinCategoryFacade(User).Retrieve(iSelected)
                    'IselectedTopParent = BuletinCategory.TopParent
                    criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.TopParent", MatchType.Exact, iSelected), "(", True)
                    criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, iSelected), ")", False)
                End If
            End If
        End If

        '-- keywords
        If txtKeyWords.Text.Trim() <> "" Then
            Dim item As String
            For Each item In Split(Me.txtKeyWords.Text, ",")
                criterias.opAnd(New Criteria(GetType(Buletin), "Keywords", MatchType.[Partial], item))
            Next
        End If

        '--description
        If txtDescription.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Description", MatchType.[Partial], txtDescription.Text.Trim))
        End If

        If txtJudul.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Title", MatchType.[Partial], txtJudul.Text.Trim))
        End If

        '--validation Date
        criterias.opAnd(New Criteria(GetType(Buletin), "ValidFrom", MatchType.LesserOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 23, 59, 59)))
        criterias.opAnd(New Criteria(GetType(Buletin), "ValidTo", MatchType.GreaterOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 0, 0, 0)))

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Buletin), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))

        BuletinList = New BuletinFacade(User).Retrieve(criterias, sortColl)
        If BuletinList.Count = 0 Then
            'ReadData2()            
            BuletinList = New ArrayList
        End If
        ' Else 'User doesn't have Buletin Member

        ' End If


        '-- Store UserList into session for later use
        sessHelp.SetSession("BuletinList", BuletinList)

        If BuletinList.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Function GetBuletinCategoryList(ByVal userInfo As UserInfo)
        Dim arlCategory As New ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BuletinCategory), "ID", MatchType.InSet, "(select BuletinCategoryID from BuletinDetail where UserGroupID in (Select UserGroupID from UserGroupMember where UserID=" & userInfo.ID & "))"))
        arlCategory = New BuletinCategoryFacade(User).Retrieve(criterias)
        Return arlCategory
    End Function

    Private Function GetBuletinMemberList(ByVal userInfo As UserInfo)
        Dim arlBuletin As New ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BuletinMember), "UserInfo.ID", MatchType.Exact, userInfo.ID))
        arlBuletin = New BuletinMemberFacade(User).Retrieve(criterias)
        Return arlBuletin
    End Function

    Private Function IsBuletinCategoryAssigned(ByVal userInfo As UserInfo, ByVal idSelected As Integer)
        Dim isAssigned As Boolean = False
        Dim arlCategory As ArrayList = GetBuletinCategoryList(userInfo)
        For Each buletinCategory As BuletinCategory In arlCategory
            If buletinCategory.ID = idSelected Then
                isAssigned = True
            End If
        Next
        Return isAssigned
    End Function

    Private Function IsBuletinAssigned(ByVal userInfo As UserInfo, ByVal idSelected As Integer)
        Dim isAssigned As Boolean = False
        Dim arlBuletin As ArrayList = GetBuletinMemberList(userInfo)
        For Each buletinMember As BuletinMember In arlBuletin
            If Not buletinMember.Buletin Is Nothing Then
                If buletinMember.Buletin.ID = idSelected Then
                    isAssigned = True
                End If
            End If
        Next
        Return isAssigned
    End Function

    Private Sub ReadDataOld()
        'Get Buletin assigned to this user
        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim strBuletinID As String = ""
        Dim strUserGroupID As String = String.Empty
        Dim strBuletinCategory As String = String.Empty

        'Dim criteriaMember As New CriteriaComposite(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criteriaMember.opAnd(New Criteria(GetType(BuletinMember), "UserInfo.ID", MatchType.Exact, objUserInfo.ID))
        'Dim arlBuletinMember As ArrayList = New BuletinMemberFacade(User).Retrieve(criteriaMember)
        'For Each item As BuletinMember In arlBuletinMember
        '    If Not item.Buletin Is Nothing Then
        '        strBuletinID = strBuletinID & item.Buletin.ID.ToString & ","
        '    End If
        'Next

        'If strBuletinID <> "" Then  'User has Buletin Member
        '    If arlBuletinMember.Count > 0 Then
        '        strBuletinID = strBuletinID.Remove(strBuletinID.Length - 1, 1)
        '    End If
        'End If

        Dim BuletinList As ArrayList

        Dim CurrentDate As Date = Date.Now.Date
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.Status", MatchType.Exact, 0))
        'criterias.opAnd(New Criteria(GetType(Buletin), "ValidTo", MatchType.GreaterOrEqual, CurrentDate))
        'criterias.opAnd(New Criteria(GetType(Buletin), "ValidFrom", MatchType.LesserOrEqual, CurrentDate))

        If strBuletinID <> "" Then
            'criterias.opAnd(New Criteria(GetType(Buletin), "ID", MatchType.InSet, "(" & strBuletinID & ")"), "(", True)
            criterias.opAnd(New Criteria(GetType(Buletin), "ID", MatchType.InSet, "(select buletinid from BuletinMember where UserID=" & objUserInfo.ID & ")"), "(", True)
            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select BuletinCategoryID from BuletinDetail where UserGroupID in (Select UserGroupID from UserGroupMember where UserID=" & objUserInfo.ID & "))"), ")", False)
        Else
            criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(select BuletinCategoryID from BuletinDetail where UserGroupID in (Select UserGroupID from UserGroupMember where UserID=" & objUserInfo.ID & "))"))
        End If


        '-- Read all data selected
        '-- Row status = activehttp://localhost/KTB.DNet/Buletin/FrmBuletinRead.aspx

        '-- Include main category

        Dim iSelectedSub As Integer = 0

        If DdlSubCategory.Items.Count > 0 Then
            iSelectedSub = CInt(DdlSubCategory.SelectedValue)
            If iSelectedSub > 0 Then
                Dim ArlCatID As ArrayList
                Dim ArlAllList As ArrayList = New ArrayList
                ArlCatID = New BuletinCategoryFacade(User).RetrieveAllCategory(iSelectedSub, ArlAllList)
                Dim BuletinCatSelection As BuletinCategory = New BuletinCategoryFacade(User).Retrieve(iSelectedSub)
                ArlCatID.Add(BuletinCatSelection)
                If ArlCatID.Count > 0 Then
                    Dim Intloop As Integer = 1
                    For Each item As BuletinCategory In ArlCatID
                        If Intloop = 1 And ArlCatID.Count = 1 Then
                            criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID))
                        ElseIf Intloop = 1 And ArlCatID.Count > 1 And Intloop <> ArlCatID.Count Then
                            criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID), "(", True)
                        ElseIf Intloop = ArlCatID.Count And ArlCatID.Count > 1 Then
                            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID), ")", False)
                        Else
                            criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, item.ID))
                        End If
                        Intloop = Intloop + 1
                    Next
                End If
            End If
        End If

        Dim iSelected As Integer = 0
        'Dim IselectedTopParent As Integer = 0
        If DdlCategory.Items.Count > 0 Then
            iSelected = CInt(DdlCategory.SelectedValue)
            If iSelectedSub = 0 Then
                If iSelected > 0 Then
                    'Dim BuletinCategory As BuletinCategory = New BuletinCategoryFacade(User).Retrieve(iSelected)
                    'IselectedTopParent = BuletinCategory.TopParent
                    criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.TopParent", MatchType.Exact, iSelected), "(", True)
                    criterias.opOr(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.Exact, iSelectedSub), ")", False)
                End If
            End If
        End If







        'If txtYearPeriods.Text.Trim() <> "" Then
        '    If IsNumeric(txtYearPeriods.Text.Trim()) Then
        '        Dim iYear As Integer = CInt(txtYearPeriods.Text.Trim())
        '        criterias.opAnd(New Criteria(GetType(Buletin), "PeriodYear", MatchType.Exact, iYear))
        '    End If
        'End If

        '-- month periods
        'If DdlMonth.SelectedIndex > 0 Then
        '    criterias.opAnd(New Criteria(GetType(Buletin), "PeriodMonth", MatchType.Exact, DdlMonth.SelectedIndex))
        'End If

        '-- keywords
        If txtKeyWords.Text.Trim() <> "" Then
            Dim item As String
            For Each item In Split(Me.txtKeyWords.Text, ",")
                criterias.opAnd(New Criteria(GetType(Buletin), "Keywords", MatchType.[Partial], item))
            Next
        End If

        '--description
        If txtDescription.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Description", MatchType.[Partial], txtDescription.Text.Trim))
        End If

        If txtJudul.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Title", MatchType.[Partial], txtJudul.Text.Trim))
        End If

        '--validation Date
        criterias.opAnd(New Criteria(GetType(Buletin), "ValidFrom", MatchType.LesserOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 23, 59, 59)))
        criterias.opAnd(New Criteria(GetType(Buletin), "ValidTo", MatchType.GreaterOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 0, 0, 0)))

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Buletin), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))

        BuletinList = New BuletinFacade(User).Retrieve(criterias, sortColl)
        If BuletinList.Count = 0 Then
            'ReadData2()            
            BuletinList = New ArrayList
        End If
        ' Else 'User doesn't have Buletin Member

        ' End If


        '-- Store UserList into session for later use
        sessHelp.SetSession("BuletinList", BuletinList)

        If BuletinList.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub






    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        'crits.Add("PeriodYear", txtYearPeriods.Text.Trim())
        'crits.Add("PeriodMonth", DdlMonth.SelectedIndex)

        crits.Add("BuletinCategory.TopParent", DdlCategory.SelectedValue)
        crits.Add("BuletinCategory.ID", DdlSubCategory.SelectedValue)

        If txtKeyWords.Text <> "" Then
            crits.Add("Keywords", txtKeyWords.Text.Trim())
        End If

        If txtDescription.Text <> "" Then
            crits.Add("Description", txtDescription.Text.Trim)
        End If

        If txtJudul.Text <> "" Then
            crits.Add("Title", txtJudul.Text.Trim)
        End If

        'add for date validation
        crits.Add("ValidFrom", Date.Now)

        sessHelp.SetSession("FrmBuletinManageCrits", crits)
    End Sub
    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read BuletinList from session
        Dim BuletinList As ArrayList = CType(sessHelp.GetSession("BuletinList"), ArrayList)

        If BuletinList.Count <> 0 Then
            '-- Sort first
            ''SortListControl(BuletinList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))

            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(BuletinList, pageIndex, dgBuletinList.PageSize)
            dgBuletinList.DataSource = PagedList
            dgBuletinList.VirtualItemCount = BuletinList.Count()
            dgBuletinList.CurrentPageIndex = pageIndex
            dgBuletinList.DataKeyField = "ID"
            dgBuletinList.DataBind()
        Else
            '-- Display datagrid header only
            dgBuletinList.DataSource = New ArrayList
            dgBuletinList.VirtualItemCount = 0
            dgBuletinList.CurrentPageIndex = 0
            dgBuletinList.DataKeyField = "ID"
            dgBuletinList.DataBind()
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '-- Search Buletin
        dgBuletinList.CurrentPageIndex = 0
        SaveCriteria()  '-- Save selection criteria
        ReadData()      '-- Read all data matching criteria
        BindPage(0)     '-- Bind page-1
    End Sub
    Private Sub PopulateSelectionCategory(ByVal parent As Integer)
        DdlSubCategory.Items.Clear()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)

        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim list As ArrayList = _BuletinFacade.PopulateListView(parent, org.Title)
        Dim _item As New ListItem("Pilih Sub Kategori", "0")
        Dim space As String = String.Empty
        DdlSubCategory.Items.Add(_item)
        If list.Count > 0 Then
            For Each item As BuletinCategory In list
                If item.Status = 0 Then
                    space = BuildLeadingSpace(item.Leveling)
                    _item = New ListItem
                    _item.Value = item.ID
                    _item.Text = space & item.Code
                    DdlSubCategory.Items.Add(_item)
                    space = String.Empty
                End If
            Next
        End If
    End Sub
    Private Sub ReadCriteria()
        '-- Read selection criteria

        '-- Init sorting column and sort direction default
        ViewState("CurrentSortColumn") = "UploadDate"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        '-- Restore selection criteria
        Dim crits As Hashtable
        crits = CType(sessHelp.GetSession("FrmBuletinManageCrits"), Hashtable)
        If Not IsNothing(crits) Then
            'txtYearPeriods.Text = CInt(crits.Item("PeriodYear"))
            'DdlMonth.SelectedIndex = CInt(crits.Item("PeriodMonth"))
            DdlCategory.SelectedValue = CInt(crits.Item("BuletinCategory.TopParent"))
            DdlSubCategory.SelectedValue = CInt(crits.Item("BuletinCategory.Parent"))
            txtKeyWords.Text = CStr(crits.Item("Keywords"))
            txtDescription.Text = CStr(crits.Item("Description"))
            txtJudul.Text = CStr(crits.Item("Title"))

        End If

    End Sub

    Private Sub dgBuletinList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBuletinList.PageIndexChanged
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub dgBuletinList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgBuletinList.SortCommand
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

        dgBuletinList.SelectedIndex = -1
        dgBuletinList.CurrentPageIndex = 0
        ReadData()
        BindPage(0)
    End Sub
End Class
