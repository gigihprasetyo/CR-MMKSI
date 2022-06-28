#Region " Summary "
'-------------------------------------------------'
'-- Program Code : FrmPODestination.aspx         --'
'-- Program Name : UMUM-Master Destination      --'
'-- Description  :                              --'
'-------------------------------------------------'
'-- Programmer   : Slem                         --'
'-- Start Date   : Aug 24 2017                  --'
'-- Update By    :                              --'
'-- Last Update  : Aug 24 2017                  --'
'-------------------------------------------------'
'-- Copyright © 2005 by Intimedia               --'
'-------------------------------------------------'
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmPODestination
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgPODestination As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents txtRegion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNamaDestination As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerDestination As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDestination As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRegNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents trDealer As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables "
    Private sessHelp As SessionHelper = New SessionHelper
#End Region

#Region " Custom Method "

    Private Sub Initialization()
        Dim objUserInfo As UserInfo = sessHelp.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")

        Else
            txtKodeDealer.ReadOnly = False

        End If

    End Sub

    Private Sub UnBindSearchGrid()
        dgPODestination.DataSource = Nothing
        dgPODestination.Visible = False
    End Sub

    Private Function WriteDestinationData(ByRef sw As StreamWriter)

        '-- Retrieve vehicle color list from session
        Dim PODestinationList As ArrayList = CType(sessHelp.GetSession("PODestinationList"), ArrayList)

        Dim poDesLine As StringBuilder = New StringBuilder  '-- Color line in text file
        Dim x As Integer
        Try
            For Each objPODes As PODestination In PODestinationList
                poDesLine.Remove(0, poDesLine.Length)  '-- Empty color line

                If (objPODes.Dealer Is Nothing) Then
                    poDesLine.Append(";")
                Else
                    poDesLine.Append(objPODes.Dealer.DealerCode & ";")
                End If
                poDesLine.Append(objPODes.RegionCode & ";")
                poDesLine.Append(objPODes.RegionDesc & ";")
                poDesLine.Append(objPODes.Code & ";")
                poDesLine.Append(objPODes.Nama & ";")
                poDesLine.Append(objPODes.Alamat & ";")
                If (objPODes.City Is Nothing) Then
                    poDesLine.Append(";")
                Else
                    poDesLine.Append(objPODes.City.CityName & ";")
                End If
                sw.WriteLine(poDesLine.ToString())  '-- Write color line into text file
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message + x.ToString())


        End Try
    End Function

    Private Sub BindSearchGrid()
        ReadData()   '-- Read all data matching criteria
        BindPage(0)  '-- Bind page-1
    End Sub

    Private Sub ReadData()
        '-- Read all data selected

        dgPODestination.Visible = True
        btnDnLoad.Enabled = False  '-- Init: Disable <Download> button

        '-- Search criteria:

        '-- Row status
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODestination), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        
        If txtRegion.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODestination), "RegionDesc", MatchType.[Partial], txtRegion.Text.Trim))
        End If

        If txtKodeDestination.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODestination), "Code", MatchType.[Partial], txtKodeDestination.Text.Trim))
        End If

        If txtNamaDestination.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODestination), "Nama", MatchType.[Partial], txtNamaDestination.Text.Trim))
        End If

        If txtDealerDestination.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODestination), "DealerDestinationCode.DealerCode", MatchType.[Partial], txtDealerDestination.Text.Trim))
        End If

        Dim oDealer As Dealer = Session("DEALER")
        If oDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, Short) Then
            If Me.txtKodeDealer.Text.Trim <> "" Then
                Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
                sCodes = "'" & sCodes.Replace(";", "','") & "'"
                criterias.opAnd(New Criteria(GetType(PODestination), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
            Else
                criterias.opAnd(New Criteria(GetType(PODestination), "Dealer.ID", MatchType.Exact, oDealer.ID))
            End If
        Else
            If Me.txtKodeDealer.Text.Trim <> "" Then
                Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
                sCodes = "'" & sCodes.Replace(";", "','") & "'"
                criterias.opAnd(New Criteria(GetType(PODestination), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
            End If
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(PODestination), "ID", Sort.SortDirection.DESC))

        '-- Retrieve color list
        Dim PODestinationList As ArrayList = New PODestinationFacade(User).RetrieveByCriteria(criterias, sortColl)

        '-- Store vehicle color list into session for later use by Download
        sessHelp.SetSession("PODestinationList", PODestinationList)

        If PODestinationList.Count > 0 Then
            btnDnLoad.Enabled = True  '-- Enable <Download> button
        Else
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read PODestinationList from session
        Dim PODestinationList As ArrayList = CType(sessHelp.GetSession("PODestinationList"), ArrayList)

        If Not IsNothing(PODestinationList) AndAlso PODestinationList.Count <> 0 Then
            Try
                '-- Sort first
                SortListControl(PODestinationList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Catch ex As Exception
            End Try
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(PODestinationList, pageIndex, dgPODestination.PageSize)
            dgPODestination.DataSource = PagedList
            dgPODestination.VirtualItemCount = PODestinationList.Count
            dgPODestination.CurrentPageIndex = pageIndex
            dgPODestination.DataBind()
        Else
            '-- Display datagrid header only
            dgPODestination.DataSource = New ArrayList
            dgPODestination.VirtualItemCount = 0
            dgPODestination.CurrentPageIndex = 0
            dgPODestination.DataBind()
        End If

    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)
        '-- Sort arraylist

        If SortColumn.Trim <> "" Then
            Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            pCompletelist.Sort(objListComparer)
        End If

    End Sub

    Private Sub ActivateUserPrivilege()
        '-- Assign privileges
        btnDnLoad.Visible = SecurityProvider.Authorize(Context.User, SR.ViewPODestinationList_Privilege)
    End Sub


#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewPODestinationList_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum-Master Destination")
            End If
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            Initialization()
            '-- Init sorting column and sort direction default
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            '-- Display grid column headers
            dgPODestination.DataSource = New ArrayList
            dgPODestination.DataBind()
        End If

        ActivateUserPrivilege()  '-- Assign privileges
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearchGrid()  '-- Bind Color datagrid
    End Sub

    Private Sub btnDnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDnLoad.Click
        '-- Download data in datagrid to text file

        '-- Generate timestamp
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)

        '-- Temp file must be a randomly named text file!
        Dim DestinationData As String = Server.MapPath("") & "\..\DataTemp\LogDestination" & sSuffix & ".txt"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DestinationData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(DestinationData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDestinationData(sw)

                '-- Close stream & file
                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download color data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\LogDestination" & sSuffix & ".txt")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try

    End Sub

    Private Sub dgPODestination_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPODestination.SortCommand
        '-- Sort datagrid rows based on a column header clicked

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

        BindPage(0)  '-- Display page-1

    End Sub

    Private Sub dgPODestination_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPODestination.PageIndexChanged
        '-- Change datagrid page
        BindPage(e.NewPageIndex)
    End Sub



    Private Sub dgPODestination_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPODestination.ItemDataBound

        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgPODestination.CurrentPageIndex * dgPODestination.PageSize)
        End If
    End Sub

#End Region
End Class
