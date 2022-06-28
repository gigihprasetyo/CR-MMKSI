#Region " Summary "
'--------------------------------------------------------------'
'-- Program Code : FrmUploadSPItemSpecial.aspx               --'
'-- Program Name : SPECIAL ITEM - Upload Melalui File Excel  --'
'-- Description  :                                           --'
'--------------------------------------------------------------'
'-- Programmer   : Agus Pirnadi                              --'
'-- Start Date   : Oct 10 2005                               --'
'-- Update By    :                                           --'
'-- Last Update  : Dec 27 2005                               --'
'--------------------------------------------------------------'
'-- Copyright © 2005 by Intimedia                            --'
'--------------------------------------------------------------'
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmUploadSPItemSpecial
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeaderErr As System.Web.UI.WebControls.Label
    Protected WithEvents lblReference As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents REValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents dgSpecialItem As System.Web.UI.WebControls.DataGrid
    Protected WithEvents CopyFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnCopy As System.Web.UI.WebControls.Button
    Protected WithEvents REValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblRemark As System.Web.UI.WebControls.Label
    Protected WithEvents lblSIExists As System.Web.UI.WebControls.Label
    Protected WithEvents lblDataErr As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefError As System.Web.UI.WebControls.Label

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
    Private sessHelp As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub ExpandColumns(ByVal siHeader As SpecialItemHeader)
        '-- Add columns to accomodate diplaying packages

        Dim Packages As ArrayList = New ArrayList  '-- Max packages contained

        '-- Iterate every SI Detail
        For Each siDetail As SpecialItemDetail In siHeader.SpecialItemDetails

            '-- Iterate every SI Package
            For Each siPackage As SpecialItemPackage In siDetail.SpecialItemPackages

                '-- If a larger package number is found then add it into Packages
                If Not Packages.Contains(siPackage.PackageNo) Then
                    Packages.Add(siPackage.PackageNo)
                End If
            Next
        Next

        Packages.Sort()  '-- Sort packages

        '-- Add columns on datagrid
        For Each Package As String In Packages
            Dim Column As New BoundColumn
            Column.HeaderText = "Harga / Paket (Rp)"
            Column.HeaderStyle.CssClass = "titleTableParts"
            Column.HeaderStyle.Width = Unit.Percentage(10)
            Column.ItemStyle.HorizontalAlign = HorizontalAlign.Right
            dgSpecialItem.Columns.Add(Column)
        Next

        '-- Add error message column
        Dim Message As New BoundColumn
        Message.HeaderText = "Pesan"
        Message.HeaderStyle.CssClass = "titleTableParts"
        Message.HeaderStyle.Width = Unit.Percentage(15)
        Message.ItemStyle.HorizontalAlign = HorizontalAlign.Left
        dgSpecialItem.Columns.Add(Message)

    End Sub

    Private Function bDataValid(ByVal siHeader As SpecialItemHeader) As Boolean

        Dim bValid As Boolean = True  '-- True if no error exists, otherwise false

        '-- Check header error message
        If siHeader.ErrorMessage <> String.Empty Then
            lblHeaderErr.Text = siHeader.ErrorMessage  '-- Display header error message
            bValid = False
        Else
            lblHeaderErr.Text = ""  '-- Clear header error message

            '-- Check detail error messages
            For Each siDetail As SpecialItemDetail In siHeader.SpecialItemDetails
                If siDetail.ErrorMessage <> String.Empty Then
                    bValid = False
                    Exit For
                End If

                '-- Check package error messages
                For Each siPackage As SpecialItemPackage In siDetail.SpecialItemPackages
                    If siPackage.ErrorMessage <> String.Empty Then
                        bValid = False
                        Exit For
                    End If
                Next

                If Not bValid Then Exit For '-- Exit for if an error occurs
            Next
        End If

        Return bValid  '-- Return valid status
    End Function

    Private Sub BindHeader()
        '-- Bind header

        Dim siHeader As SpecialItemHeader = CType(sessHelp.GetSession("siHeader"), SpecialItemHeader)

        If Not IsNothing(siHeader) Then

            '-- Header info
            lblPeriod.Text = siHeader.MonthPeriode.ToString().PadLeft(2, "0") & _
                             " / " & siHeader.YearPeriode.ToString()
            lblRemark.Text = siHeader.Remark
            lblReference.Text = siHeader.Reference
            Dim refNumber As String = lblReference.Text.Trim
            Dim arrRefNumber As String() = refNumber.Split("-")
            If arrRefNumber.Length > 1 Then
                btnCopy.Enabled = True
                lblRefError.Visible = False
            Else
                btnCopy.Enabled = False
                lblRefError.Text = "Nomor Ref tidak valid (NomorRev-Ver)"
                lblRefError.Visible = True
            End If
            ExpandColumns(siHeader)  '-- Expand columns accordingly
        End If

    End Sub

    Private Function bSIExists(ByVal siHeader As SpecialItemHeader) As Boolean
        '-- Check to see if the Special Item header already exists

        Dim SIFacade As SpecialItemFacade = New SpecialItemFacade(System.Threading.Thread.CurrentPrincipal)
        Return Not IsNothing(SIFacade.RetrieveSIHeader(siHeader.MonthPeriode, siHeader.YearPeriode, siHeader.Reference))
    End Function

    Private Sub ActivateUserPrivilege()
        '-- Assign privileges

        DataFile.Visible = SecurityProvider.Authorize(Context.User, SR.BrowseSpecialItemUpload_Privilege)
        btnUpload.Visible = SecurityProvider.Authorize(Context.User, SR.UploadSpecialItemUpload_Privilege)
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.SaveUploadSpecialItem_Privilege)
        btnCopy.Visible = SecurityProvider.Authorize(Context.User, SR.CopySpecialItemUpload_Privilege)
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '-- Initialisation

        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSpecialItemUpload_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Special Item-Upload Melalui File Excel")
            End If

            '-- Display header
            dgSpecialItem.DataSource = New ArrayList
            dgSpecialItem.DataBind()
        End If

        ActivateUserPrivilege()  '-- Assign privileges
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        '-- Upload sparepart special item data from Excel file

        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If

        '-- Reset datagrid
        dgSpecialItem.DataSource = New ArrayList
        dgSpecialItem.DataBind()

        btnSave.Enabled = False  '-- Disable <Simpan>
        btnCopy.Enabled = False  '-- Disable <Copy>

        lblDataErr.Text = ""  '-- Reset error message

        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            'cek maxFileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            ' Modified by Ikhsan , 13 Nov 2008
            ' To Allow user to upload file more than 512 KB
            'If DataFile.PostedFile.ContentLength > maxFileSize Then
            '    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
            '    Exit Sub
            'End If

            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    '-- Copy file to server
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)

                    imp.StopImpersonate()
                    imp = Nothing

                    '-- Parse Excel file
                    Dim parser As IExcelParser = New UploadSPSpecialItemParser
                    Dim siHeader As SpecialItemHeader = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$]", "User"), SpecialItemHeader)

                    If IsNothing(siHeader) Then
                        Throw New System.Exception  '-- If no special item exists then throw exception
                    End If

                    '-- Check if this PO number already exists in DNet
                    If bSIExists(siHeader) Then
                        lblSIExists.Text = "Periode dan Referensi ini sudah ada di DNet"  '-- Display message
                    Else
                        lblSIExists.Text = ""
                    End If

                    If bDataValid(siHeader) And siHeader.SpecialItemDetails.Count <> 0 Then
                        '-- If no error found and the details exist then enable <Simpan> & <Copy> button
                        btnSave.Enabled = True
                        btnCopy.Enabled = True

                    Else
                        lblDataErr.Text = "Data error"
                    End If

                    '-- Store SI header
                    sessHelp.SetSession("siHeader", siHeader)
                    '-- Save SI detail list
                    sessHelp.SetSession("SpecItemList", siHeader.SpecialItemDetails)

                    If siHeader.SpecialItemDetails.Count = 0 Then
                        MessageBox.Show(SR.DataNotFound("Data"))  '-- No details
                    End If

                    BindData(0)  '-- Bind first page of detail list 
                End If

            Catch ex As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '-- Insert sparepart SI and its details

        '-- Retrieve sparepart SI from session
        Dim siHeader As SpecialItemHeader = CType(sessHelp.GetSession("siHeader"), SpecialItemHeader)

        Dim SPSIFacade As New SpecialItemFacade(System.Threading.Thread.CurrentPrincipal)
        Dim Status As Integer = SPSIFacade.InsertSPSI(siHeader)

        If Status = 1 Then
            MessageBox.Show(SR.SaveSuccess)  '-- Successfully save data
        Else
            MessageBox.Show(SR.SaveFail)   '-- Failed saving data
        End If

        btnSave.Enabled = False  '-- Disable <Simpan> button

        BindData(dgSpecialItem.CurrentPageIndex)  '-- Bind datagrid

    End Sub

    Private Sub dgSpecialItem_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSpecialItem.ItemDataBound
        '-- Handles data binding on datagrid
        If e.Item.ItemIndex <> -1 Then

            '-- No
            e.Item.Cells(0).Text = (dgSpecialItem.CurrentPageIndex * dgSpecialItem.PageSize + e.Item.ItemIndex + 1).ToString
            ''e.Item.Cells(0).Text = e.Item.Cells(0).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
            e.Item.Cells(0).Text = e.Item.Cells(0).Text & "<DIV style=""WIDTH: 100%"">&nbsp;</DIV>"
            e.Item.Cells(0).VerticalAlign = VerticalAlign.Top

            '-- Part number
            If Not IsNothing(CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster) Then
                e.Item.Cells(1).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
            Else
                e.Item.Cells(1).Text = "N/A"
            End If
            ''e.Item.Cells(1).Text = e.Item.Cells(1).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
            e.Item.Cells(1).Text = e.Item.Cells(1).Text & "<DIV style=""WIDTH: 100%"">&nbsp;</DIV>"
            e.Item.Cells(1).VerticalAlign = VerticalAlign.Top

            '-- Part name
            e.Item.Cells(2).Text = CType(e.Item.DataItem, SpecialItemDetail).PartName
            If e.Item.Cells(2).Text = String.Empty Then e.Item.Cells(2).Text = "N/A"
            ''e.Item.Cells(2).Text = e.Item.Cells(2).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
            e.Item.Cells(2).Text = e.Item.Cells(2).Text & "<DIV style=""WIDTH: 100%"">&nbsp;</DIV>"
            e.Item.Cells(2).VerticalAlign = VerticalAlign.Top

            '-- Model code
            e.Item.Cells(3).Text = CType(e.Item.DataItem, SpecialItemDetail).ModelCode
            If e.Item.Cells(3).Text = String.Empty Then e.Item.Cells(3).Text = "N/A"
            ''e.Item.Cells(3).Text = e.Item.Cells(3).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
            e.Item.Cells(3).Text = e.Item.Cells(3).Text & "<DIV style=""WIDTH: 100%"">&nbsp;</DIV>"
            e.Item.Cells(3).VerticalAlign = VerticalAlign.Top

            '-- Group
            e.Item.Cells(4).Text = CType(e.Item.DataItem, SpecialItemDetail).ExtMaterialGroup
            ''e.Item.Cells(4).Text = e.Item.Cells(4).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
            e.Item.Cells(4).Text = e.Item.Cells(4).Text & "<DIV style=""WIDTH: 100%"">&nbsp;</DIV>"
            e.Item.Cells(4).VerticalAlign = VerticalAlign.Top

            '-- Set background color
            If CType(e.Item.DataItem, SpecialItemDetail).ItemStatus = 1 Then
                '-- New part number
                e.Item.BackColor = Color.FromArgb(255, 255, 204)
            ElseIf CType(e.Item.DataItem, SpecialItemDetail).ItemStatus = 2 Then
                '-- New price
                e.Item.BackColor = Color.FromArgb(204, 255, 204)
            End If

            Dim sErrMessage As String  '-- Error message
            sErrMessage = CType(e.Item.DataItem, SpecialItemDetail).ErrorMessage  '-- Err message from SI Detail

            If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > 0 Then
                '-- If any package exists then display it on datagrid

                For iColumn As Integer = 5 To dgSpecialItem.Columns.Count - 1
                    Dim siPackage As SpecialItemPackage

                    If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > iColumn - 5 Then
                        '-- Display this package

                        siPackage = CType(CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Item(iColumn - 5), SpecialItemPackage)
                        e.Item.Cells(iColumn).Text = String.Format("{0:#,##0}", siPackage.PackagePrice)
                        ''e.Item.Cells(iColumn).Text = e.Item.Cells(iColumn).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">" & siPackage.PackageDescription & "</DIV>"
                        e.Item.Cells(iColumn).Text = e.Item.Cells(iColumn).Text & "<DIV style=""WIDTH: 100%"">" & siPackage.PackageDescription & "</DIV>"
                        e.Item.Cells(iColumn).VerticalAlign = VerticalAlign.Top

                        sErrMessage &= siPackage.ErrorMessage  '-- Err message from SI Package

                    Else
                        '-- No more packages, so display blank columns

                        e.Item.Cells(iColumn).Text = "&nbsp;<br>"
                        ''e.Item.Cells(iColumn).Text = e.Item.Cells(iColumn).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
                        e.Item.Cells(iColumn).Text = e.Item.Cells(iColumn).Text & "<DIV style=""WIDTH: 100%"">&nbsp;</DIV>"
                        e.Item.Cells(iColumn).VerticalAlign = VerticalAlign.Top
                    End If
                Next

            End If

            '-- Display error message
            e.Item.Cells(dgSpecialItem.Columns.Count - 1).Text = sErrMessage

        End If

    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        '-- Copy sparepart special item Excel file to Web server

        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If

        If (Not CopyFile.PostedFile Is Nothing) And (CopyFile.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(CopyFile.PostedFile.FileName)  '-- Source file name

            '-- Validate: source file extension must be ".xls"
            If UCase(Path.GetExtension(CopyFile.PostedFile.FileName)) <> ".XLS" Then
                MessageBox.Show("Harus berupa file excel (.xls)")
                BindData(dgSpecialItem.CurrentPageIndex)  '-- Bind datagrid
                Exit Sub
            End If

            '-- Validate: source file name must be "SpecialItemMMYYYY"
            If Len(Path.GetFileNameWithoutExtension(CopyFile.PostedFile.FileName)) <> 17 Or _
               UCase(Left(Path.GetFileNameWithoutExtension(CopyFile.PostedFile.FileName), 11)) <> "SPECIALITEM" Or _
               Not IsNumeric(Right(Path.GetFileNameWithoutExtension(CopyFile.PostedFile.FileName), 6)) Then
                MessageBox.Show("Nama file harus SpecialItemMMYYYY")
                BindData(dgSpecialItem.CurrentPageIndex)  '-- Bind datagrid
                Exit Sub
            End If

            Dim siHeader As SpecialItemHeader = CType(sessHelp.GetSession("siHeader"), SpecialItemHeader)
            If Not IsNothing(siHeader) Then

                '-- Validate: period of "SpecialItemMMYYYY" must equal open file on grid
                If Right(Path.GetFileNameWithoutExtension(CopyFile.PostedFile.FileName), 6) <> _
                   siHeader.MonthPeriode.ToString().PadLeft(2, "0") & siHeader.YearPeriode.ToString() Then
                    MessageBox.Show("Periode file data tidak sama")
                    BindData(dgSpecialItem.CurrentPageIndex)  '-- Bind datagrid
                    Exit Sub
                End If
            Else
                MessageBox.Show("File data harus dibuka dulu")
                BindData(dgSpecialItem.CurrentPageIndex)  '-- Bind datagrid
                Exit Sub
            End If
            'TODO
            Dim refNumber As String = lblReference.Text.Trim
            Dim arrRefNumber As String() = refNumber.Split("-")
          
            SrcFile = arrRefNumber(1).Trim & "-" & SrcFile
            Dim DestFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SPFileDirectory")  '-- Destination folder
            Dim DestFile As String = DestFolder & "\" & SrcFile  '-- Destination file

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    '-- Create folder if not exist
                    If Not IO.Directory.Exists(DestFolder) Then
                        IO.Directory.CreateDirectory(DestFolder)
                    End If

                    '-- Copy file to server
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(CopyFile.PostedFile.InputStream, DestFile)

                    imp.StopImpersonate()
                    imp = Nothing

                    MessageBox.Show("Berhasil meng-copy file")

                End If
            Catch ex As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If

        BindData(dgSpecialItem.CurrentPageIndex)  '-- Bind datagrid
    End Sub

    Private Sub dgSpecialItem_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSpecialItem.PageIndexChanged
        '-- Change datagrid page number

        BindData(e.NewPageIndex)
    End Sub

    Private Sub dgSpecialItem_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSpecialItem.SortCommand
        '-- Sort data based on selected column

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

        '-- Get all special item data from session
        Dim SpecItemList As ArrayList = CType(sessHelp.GetSession("SpecItemList"), ArrayList)

        If Not IsNothing(SpecItemList) Then
            '-- If exists then display its chosen page data

            '-- Sort SI detail list
            SortListControl(SpecItemList, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            '-- Replace SI detail list in session
            sessHelp.SetSession("SpecItemList", SpecItemList)

            BindData(0)  '-- Bind page-1
        End If

    End Sub

    Private Sub SortListControl(ByVal SpecItemList As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirect As Integer)
        '-- Sort SpecItemList arraylist

        Dim IsAsc As Boolean = (SortDirect = Sort.SortDirection.ASC)
        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        SpecItemList.Sort(objListComparer)

    End Sub

    Private Sub BindData(ByVal PageIdx As Integer)
        '-- Bind data

        BindHeader()  '-- Bind header first

        '-- Get all special item data from session
        Dim SpecItemList As ArrayList = CType(sessHelp.GetSession("SpecItemList"), ArrayList)

        If Not IsNothing(SpecItemList) Then

            Dim PageData As ArrayList  '-- The chosen page data
            PageData = ArrayListPager.DoPage(SpecItemList, PageIdx, dgSpecialItem.PageSize)

            dgSpecialItem.DataSource = PageData
            dgSpecialItem.VirtualItemCount = SpecItemList.Count
            dgSpecialItem.CurrentPageIndex = PageIdx
            dgSpecialItem.DataBind()
        End If

    End Sub

#End Region

End Class
