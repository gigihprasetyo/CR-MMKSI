#Region " Summary "
'-------------------------------------------------'
'-- Program Code : FrmLogisticPrice.aspx         --'
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
Imports KTB.DNet.BusinessFacade.General
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

Public Class FrmLogisticPrice
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgLogisticPrice As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents txtRegion As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkAll As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calCalendar As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtModel As System.Web.UI.WebControls.TextBox
    Protected WithEvents trModel As System.Web.UI.HtmlControls.HtmlTableRow
    'Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchModel As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    'Private Sub BindDDL()

    '    Dim _arrStatus As ArrayList = New EnumVehicleTypeStatus().RetrieveVehicleTypeStatus()
    '    ddlStatus.DataSource = _arrStatus
    '    ddlStatus.DataTextField = "NameStatus"
    '    ddlStatus.DataValueField = "ValStatus"
    '    ddlStatus.DataBind()
    '    ddlStatus.Items.Insert(0, New ListItem("Semua", -1))

    'End Sub

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
        dgLogisticPrice.DataSource = Nothing
        dgLogisticPrice.Visible = False
    End Sub

    Private Function WriteLogisticPriceData(ByRef sw As StreamWriter)

        '-- Retrieve vehicle color list from session
        Dim LogisticPriceList As ArrayList = CType(sessHelp.GetSession("LogisticPriceList"), ArrayList)

        Dim poDesLine As StringBuilder = New StringBuilder
        Dim x As Integer
        Try
            For Each objPODes As LogisticPrice In LogisticPriceList
                poDesLine.Remove(0, poDesLine.Length)
                poDesLine.Append(objPODes.RegionCode & ";")
                poDesLine.Append(objPODes.RegionDescription & ";")
                poDesLine.Append(objPODes.SAPModel & ";")
                'poDesLine.Append(objPODes.Status & ";")
                poDesLine.Append(objPODes.EffectiveDate & ";")
                poDesLine.Append(Math.Round(objPODes.LogisticPrice, 0) & ";")
                poDesLine.Append(objPODes.TotalPPn & ";")
                poDesLine.Append(Math.Round(objPODes.TotalLogisticPrice, 0) & ";")
                sw.WriteLine(poDesLine.ToString())
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

        dgLogisticPrice.Visible = True
        btnDnLoad.Enabled = False  '-- Init: Disable <Download> button

        Dim criteriadest As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODestination), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oDealer As Dealer = Session("DEALER")
        If oDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, Short) Then
            If Me.txtKodeDealer.Text.Trim <> "" Then
                Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
                sCodes = "'" & sCodes.Replace(";", "','") & "'"
                criteriadest.opAnd(New Criteria(GetType(PODestination), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
            Else
                criteriadest.opAnd(New Criteria(GetType(PODestination), "Dealer.ID", MatchType.Exact, oDealer.ID))
            End If
        Else
            If Me.txtKodeDealer.Text.Trim <> "" Then
                Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
                sCodes = "'" & sCodes.Replace(";", "','") & "'"
                criteriadest.opAnd(New Criteria(GetType(PODestination), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
            End If
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(PODestination), "ID", Sort.SortDirection.DESC))

        Dim PODestinationList As ArrayList = New PODestinationFacade(User).RetrieveByCriteria(criteriadest, sortColl)
        Dim regioncodes As String = "'"
        Dim arr As New ArrayList

        For Each obj As PODestination In PODestinationList
            If Not arr.Contains(obj.RegionCode) Then
                arr.Add(obj.RegionCode)
                regioncodes = regioncodes + obj.RegionCode & "','"
            End If
        Next
        If regioncodes.Length > 3 Then
            regioncodes = regioncodes.Substring(0, regioncodes.Length - 2)
        End If

        'LogisticPrice
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtRegion.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticPrice), "RegionDescription", MatchType.[Partial], txtRegion.Text.Trim))
        End If

        'If ddlStatus.SelectedIndex <> 0 Then
        '    criterias.opAnd(New Criteria(GetType(LogisticPrice), "Status", MatchType.Exact, ddlStatus.SelectedValue.ToString))
        'End If

        If oDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, Short) Then
            criterias.opAnd(New Criteria(GetType(LogisticPrice), "RegionCode", MatchType.InSet, "(" & regioncodes & ")"))
        Else
            If Me.txtKodeDealer.Text.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(LogisticPrice), "RegionCode", MatchType.InSet, "(" & regioncodes & ")"))
            End If
        End If

        If Not chkAll.Checked Then
            criterias.opAnd(New Criteria(GetType(LogisticPrice), "EffectiveDate", MatchType.Exact, calCalendar.Value))        
        End If

        If Me.txtModel.Text.Trim <> "" Then
            Dim sCodes As String = Me.txtModel.Text.Trim()
            sCodes = "'" & sCodes.Replace(";", "','") & "'"
            criterias.opAnd(New Criteria(GetType(LogisticPrice), "SAPModel", MatchType.InSet, "(" & sCodes & ")"))
        End If

        Dim sortColl2 As SortCollection = New SortCollection
        sortColl2.Add(New Sort(GetType(LogisticPrice), "ID", Sort.SortDirection.DESC))

        Dim LogisticPriceList As ArrayList = New LogisticPriceFacade(User).RetrieveByCriteria(criterias, sortColl2)

        'For Each objLogistic As LogisticPrice In LogisticPriceList
        '    If objLogistic.Status = "A" Then
        '        objLogistic.Status = "Aktif"
        '    ElseIf objLogistic.Status = "X" Then
        '        objLogistic.Status = "Tidak Aktif"
        '    Else : objLogistic.Status = ""
        '    End If
        'Next

        '-- Store vehicle color list into session for later use by Download
        sessHelp.SetSession("LogisticPriceList", LogisticPriceList)

        If LogisticPriceList.Count > 0 Then
            btnDnLoad.Enabled = True  '-- Enable <Download> button
        Else
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read LogisticPriceList from session
        Dim LogisticPriceList As ArrayList = CType(sessHelp.GetSession("LogisticPriceList"), ArrayList)

        If Not IsNothing(LogisticPriceList) AndAlso LogisticPriceList.Count <> 0 Then
            Try
                '-- Sort first
                SortListControl(LogisticPriceList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Catch ex As Exception
            End Try
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(LogisticPriceList, pageIndex, dgLogisticPrice.PageSize)
            dgLogisticPrice.DataSource = PagedList
            dgLogisticPrice.VirtualItemCount = LogisticPriceList.Count
            dgLogisticPrice.CurrentPageIndex = pageIndex
            dgLogisticPrice.DataBind()
        Else
            '-- Display datagrid header only
            dgLogisticPrice.DataSource = New ArrayList
            dgLogisticPrice.VirtualItemCount = 0
            dgLogisticPrice.CurrentPageIndex = 0
            dgLogisticPrice.DataBind()
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
        btnDnLoad.Visible = SecurityProvider.Authorize(Context.User, SR.ViewLogisticPriceList_Privilege)
    End Sub


#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewLogisticPriceList_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum-Master Logistic Price")
            End If

            lblSearchModel.Attributes("onclick") = "ShowPPModelSelection();"
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            Initialization()

            '-- Init sorting column and sort direction default
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            '-- Display grid column headers
            dgLogisticPrice.DataSource = New ArrayList
            dgLogisticPrice.DataBind()
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
        Dim PriceData As String = Server.MapPath("") & "\..\DataTemp\LogPrice" & sSuffix & ".txt"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(PriceData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(PriceData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteLogisticPriceData(sw)

                '-- Close stream & file
                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download color data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\LogPrice" & sSuffix & ".txt")

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try

    End Sub

    Private Sub dgLogisticPrice_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgLogisticPrice.SortCommand
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

    Private Sub dgLogisticPrice_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgLogisticPrice.PageIndexChanged
        '-- Change datagrid page
        BindPage(e.NewPageIndex)
    End Sub



    Private Sub dgLogisticPrice_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgLogisticPrice.ItemDataBound

        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgLogisticPrice.CurrentPageIndex * dgLogisticPrice.PageSize)
        End If
    End Sub

#End Region
End Class
