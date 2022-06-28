Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls


Public Class FrmJaminan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCustName As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDesc As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblValidFrom As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents txtValidFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValidTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkFreeInterest As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents inFileLocation As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents lblAttachment As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDibuatOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblDibuatPada As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiubahOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiubahPada As System.Web.UI.WebControls.Label
    Protected WithEvents tr1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr3 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr4 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dgProfileGroup As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgSPDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lbtnPrevMonth As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnNextMonth As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblCurrentPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents PopUpDealer As System.Web.UI.WebControls.Image
    Protected WithEvents txtDepositInfo As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private sHelper As New SessionHelper
    Private oJFac As JaminanFacade = New JaminanFacade(User)
    Private oJ As Jaminan
    Private oJDFac As JaminanDetailFacade = New JaminanDetailFacade(User)
    Private oJD As JaminanDetail
    Private arlJ As New ArrayList
    Private arlJD As New ArrayList
    Private oDFac As DealerFacade = New DealerFacade(User)
    Private oD As Dealer
    Private arlTemp As New ArrayList

#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        viewstate.Add("Mode", "Add")
        viewstate.Add("ID", 0)
        viewstate.Add("CurrentPeriodMonth", 0)
        viewstate.Add("CurrentPeriodYear", 0)

        sHelper.SetSession("FrmJaminan.objJaminan", New Jaminan)
        sHelper.SetSession("FrmJaminan.arlJaminanDetail", New ArrayList)
        sHelper.SetSession("FrmJaminan.arlJaminanDetailOut", New ArrayList)
        If Not IsNothing(Request.QueryString("Mode")) Then
            viewstate.Item("Mode") = CType(Request.QueryString("Mode"), String)
        End If
        If Not IsNothing(Request.QueryString("ID")) Then
            viewstate.Item("ID") = CType(Request.QueryString("ID"), String)
        End If

        BindDdlStatus()

    End Sub

    Private Sub BindData()
        ClearData()
        Select Case CType(viewstate.Item("Mode"), String).Trim.ToUpper
            Case "Add".ToUpper
            Case "Edit".ToUpper
                BindDataToForm()
            Case "View".ToUpper
                BindDataToForm()
        End Select
        LockControls()
    End Sub

    Private Sub ClearData()
        Me.txtDealerName.Text = ""
        Me.txtDescription.Text = ""
        Me.txtDepositInfo.Text = ""
        Me.txtValidFrom.Text = ""
        Me.txtValidTo.Text = ""
        'Me.inFileLocation.Value = ""
        Me.lblAttachment.Text = ""
        Try
            Me.ddlStatus.SelectedIndex = 0
        Catch ex As Exception
        End Try
        Me.lblDibuatOleh.Text = ""
        Me.lblDibuatPada.Text = ""
        Me.lblDiubahOleh.Text = ""
        Me.lblDiubahPada.Text = ""
        sHelper.SetSession("FrmJaminan.objJaminan", New Jaminan)
        viewstate.Item("CurrentPeriodMonth") = 0
        viewstate.Item("CurrentPeriodYear") = 0
        sHelper.SetSession("FrmJaminan.arlJaminanDetail", New ArrayList)
        sHelper.SetSession("FrmJaminan.arlJaminanDetailOut", New ArrayList)
        BindDetail()
    End Sub

    Private Sub BindDataToForm()
        oJ = oJFac.Retrieve(CType(viewstate.Item("ID"), Integer))
        sHelper.SetSession("FrmJaminan.objJaminan", oJ)
        If Not IsNothing(oJ) AndAlso oJ.ID > 0 Then
            Me.txtDealerName.Text = oJ.DealerCode
            Me.txtDescription.Text = oJ.Description
            Me.txtDepositInfo.Text = oJ.DepositInfo
            Me.txtValidFrom.Text = Format(oJ.ValidFrom, "MMyyyy")
            Me.txtValidTo.Text = Format(oJ.ValidTo, "MMyyyy")
            'Me.inFileLocation.Value = "" 'oJ.Attachment
            Me.lblAttachment.Text = oJ.Attachment
            Try
                Me.ddlStatus.SelectedValue = oJ.Status
            Catch ex As Exception
            End Try
            Me.lblDibuatOleh.Text = oJ.CreatedBy
            Me.lblDibuatPada.Text = Format(oJ.CreatedTime, "dd/MMM/yy hh:mm:ss")
            Me.lblDiubahOleh.Text = oJ.LastUpdateBy
            Me.lblDiubahPada.Text = Format(oJ.LastUpdateTime, "dd/MMM/yy hh:mm:ss")

            viewstate.Item("CurrentPeriodMonth") = oJ.ValidFrom.Month
            viewstate.Item("CurrentPeriodYear") = oJ.ValidFrom.Year
            sHelper.SetSession("FrmJaminan.arlJaminanDetail", oJ.JaminanDetailIn(oJ.ValidFrom.Month, oJ.ValidFrom.Year))
            sHelper.SetSession("FrmJaminan.arlJaminanDetailOut", oJ.JaminanDetailOut(oJ.ValidFrom.Month, oJ.ValidFrom.Year))

            BindDetail()
        Else
            ClearData()
        End If
    End Sub

    Private Sub LockControls()
        Select Case CType(viewstate.Item("Mode"), String).Trim.ToUpper
            Case "Add".ToUpper
                btnBack.Visible = False
                btnSave.Visible = True
            Case "Edit".ToUpper
                BindDataToForm()
                If GetRelatedPK(True).Count <> 0 Then
                    Me.txtValidFrom.ReadOnly = True
                    Me.txtValidTo.ReadOnly = True
                Else
                    Me.txtValidFrom.ReadOnly = False
                    Me.txtValidTo.ReadOnly = False
                End If
                btnBack.Visible = True
                btnSave.Visible = True
            Case "View".ToUpper
                BindDataToForm()
                dgSPDetail.Columns(4).Visible = False
                btnBack.Visible = True
                btnSave.Visible = False
        End Select
    End Sub

    Private Function IsDataValid() As Boolean
        Dim sDealerCodes As String = Me.txtDealerName.Text.Trim
        If sDealerCodes = "" Then
            MessageBox.Show("Dealer masih kosong")
            Return False
        Else
            If sDealerCodes.EndsWith(";") Then sDealerCodes = sDealerCodes.Substring(0, sDealerCodes.Length - 1)
            Dim sErrDealerCodes As String = ""
            For Each str As String In sDealerCodes.Split(";")
                If Not IsDealerExist(str) Then
                    sErrDealerCodes &= IIf(sErrDealerCodes.Trim = "", "", ";") & str
                Else
                    If IsDataExist(str) Then
                        MessageBox.Show("Jaminan dengan dealer " & str & " dan periode ini sudah ada, data tidak bisa disimpan")
                        Return False
                    End If
                End If
            Next
            If sErrDealerCodes.Trim <> "" Then
                MessageBox.Show("Dealer " & sErrDealerCodes & " tidak exist")
                Return False
            End If
        End If
        If PeriodToDate(txtValidFrom.Text) = System.DateTime.MinValue Then
            MessageBox.Show("Periode Tebus Dari salah")
            Return False
        End If
        If PeriodToDate(txtValidTo.Text) = System.DateTime.MinValue Then
            MessageBox.Show("Periode Tebus Sampai salah")
            Return False
        End If
        Return True
    End Function

    Private Function IsDataExist(ByVal sDealerCode As String) As Boolean
        Dim dtStart As Date = Me.PeriodToDate(txtValidFrom.Text)
        Dim dtEnd As Date = Me.PeriodToDate(txtValidTo.Text)
        Dim Sql As String = ""
        Dim crtJ As CriteriaComposite ' = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlTemp As New ArrayList
        Dim Codes As String = ""

        For Each oJD As JaminanDetail In MergeJaminanDetailInOut()
            Sql = " select distinct(j.ID)   "
            Sql &= " from Jaminan j "
            Sql &= " where j.RowStatus=0 and j.Status=" & EnumStatusSPL.StatusSPL.Aktif & " and j.DealerCode='" & sDealerCode & "'   "
            Sql &= "  and "
            Sql &= " (select count(*) from JaminanDetail jd where jd.JaminanID=j.ID and jd.VehicleTypeCode ='" & oJD.VehicleTypeCode & "' "
            Sql &= "  and jd.PeriodMonth=" & ojd.PeriodMonth & " and jd.Periodyear=" & ojd.PeriodYear & " "
            Sql &= " ) >0 "
            If CType(viewstate.Item("ID"), Integer) > 0 Then
                Sql &= " and j.ID<>" & CType(viewstate.Item("ID"), Integer) & " "
            End If
            arlTemp = New ArrayList
            crtJ = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtJ.opAnd(New Criteria(GetType(Jaminan), "ID", MatchType.InSet, "(" & Sql & ")"))
            arlTemp = oJFac.Retrieve(crtJ)
            If arlTemp.Count > 0 Then
                Return True
            End If
        Next

        Return False

        Exit Function

        For Each oJD As JaminanDetail In MergeJaminanDetailInOut()
            Codes = Codes & IIf(Codes.Trim = "", "", ",") & "'" & oJD.VehicleTypeCode & "'"
        Next
        Sql &= " select distinct(j.ID)   "
        Sql &= " from Jaminan j   "
        Sql &= " where j.RowStatus=0 and j.DealerCode='" & sDealerCode & "'   "
        Sql &= "  and (  "
        Sql &= "   (j.ValidFrom>='" & Format(dtStart, "yyyy-MM-dd") & "' and j.ValidFrom<='" & Format(dtEnd, "yyyy-MM-dd") & "') "
        Sql &= "   or  "
        Sql &= "   (j.ValidTo>='" & Format(dtStart, "yyyy-MM-dd") & "' and j.ValidTo<='" & Format(dtEnd, "yyyy-MM-dd") & "') "
        Sql &= "   or  "
        Sql &= "   (j.ValidFrom<='" & Format(dtStart, "yyyy-MM-dd") & "' and j.ValidTo>='" & Format(dtEnd, "yyyy-MM-dd") & "')  "
        Sql &= "  )  "
        If Codes.Trim <> "" Then
            Sql &= "  and (select count(*) from JaminanDetail jd where jd.JaminanID=j.ID and VehicleTypeCode in (" & Codes & ") ) >0 "
        End If

        crtJ.opAnd(New Criteria(GetType(Jaminan), "ID", MatchType.InSet, "(" & Sql & ")"))
        arlTemp = oJFac.Retrieve(crtJ)
        Return IIf(arlTemp.Count = 0, False, True)

    End Function

    Private Function IsDealerExist(ByVal DealerCode As String) As Boolean
        oD = oDFac.Retrieve(DealerCode)
        If Not IsNothing(oD) AndAlso oD.ID > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusSPL.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub

    Private Function PeriodToDate(ByVal strPeriod As String) As Date
        Dim dtRsl As Date = System.DateTime.MinValue

        strPeriod = strPeriod.Trim
        Try
            If strPeriod.Length = 6 Then
                dtRsl = DateSerial(strPeriod.Substring(2), strPeriod.Substring(0, 2), 1)
            Else
                Dim i As Integer = "GoToException"
            End If
            Return dtRsl
        Catch ex As Exception
            Return dtRsl
        End Try
    End Function

    Private Sub BindDetail()
        'oJ = CType(sHelper.GetSession("FrmJaminan.objJaminan"), Jaminan)
        'If IsNothing(oJ) OrElse oJ.ID = 0 Then
        '    Me.dgSPDetail.DataSource = New ArrayList
        '    Me.dgSPDetail.DataBind()
        '    Exit Sub
        'End If
        Dim dt As Date = DateSerial(CType(viewstate.Item("CurrentPeriodYear"), Integer), CType(viewstate.Item("CurrentPeriodMonth"), Integer), 1)

        Me.lblCurrentPeriode.Text = Format(dt, "MMM yyyy")
        Me.dgSPDetail.DataSource = CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)
        Me.dgSPDetail.DataBind()
    End Sub

    Private Sub BindDTGEdit(ByVal e As DataGridItemEventArgs)
        Dim txtEditKodeModel As TextBox = e.Item.FindControl("txtEditKodeModel")
        Dim txtEditAmount As TextBox = e.Item.FindControl("txtEditAmount")
        Dim cboEditPurpose As DropDownList = e.Item.FindControl("cboEditPurpose")
        'Dim lblEditKodeModel As Label = CType(e.Item.FindControl("lblEditKodeModel"), Label)

        'lblEditKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"
        oJD = CType(CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)(e.Item.ItemIndex), JaminanDetail)
        txtEditKodeModel.Text = oJD.VehicleTypeCode
        txtEditAmount.Text = FormatNumber(oJD.Amount, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        BindDdlPurpose(cboEditPurpose)
        cboEditPurpose.SelectedValue = oJD.Purpose

    End Sub

    Private Sub BindDTGFooter(ByVal e As DataGridItemEventArgs)
        Dim txtFooterKodeModel As TextBox = e.Item.FindControl("txtFooterKodeModel")
        Dim txtFooterAmount As TextBox = e.Item.FindControl("txtFooterAmount")
        Dim cboFooterPurpose As DropDownList = e.Item.FindControl("cboFooterPurpose")
        'Dim lblFooterKodeModel As Label = CType(e.Item.FindControl("lblFooterKodeModel"), Label)

        'lblFooterKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"
        txtFooterKodeModel.Text = ""
        txtFooterAmount.Text = FormatNumber("0", 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        BindDdlPurpose(cboFooterPurpose)
    End Sub

    Private Sub DTGUpdate(ByVal e As DataGridCommandEventArgs)
        Dim txtEditKodeModel As TextBox = e.Item.FindControl("txtEditKodeModel")
        Dim txtEditAmount As TextBox = e.Item.FindControl("txtEditAmount")
        Dim cboEditPurpose As DropDownList = e.Item.FindControl("cboEditPurpose")

        
        oJD = CType(CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)(e.Item.ItemIndex), JaminanDetail)
        If txtEditKodeModel.Text.Trim = "" Then
            MessageBox.Show("Tipe tidak boleh kosong")
            Exit Sub
        End If
        'oJD = New JaminanDetail
        oJD.VehicleTypeCode = txtEditKodeModel.Text
        oJD.Amount = txtEditAmount.Text
        oJD.Purpose = cboEditPurpose.SelectedValue
        arlTemp = CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)
        arlTemp(e.Item.ItemIndex) = oJD
        sHelper.SetSession("FrmJaminan.arlJaminanDetail", arlTemp)

        dgSPDetail.EditItemIndex = -1
        BindDetail()
        dgSPDetail.ShowFooter = True
    End Sub

    Private Sub DTGAdd(ByVal e As DataGridCommandEventArgs)
        If CType(viewstate.Item("CurrentPeriodMonth"), Integer) = 0 Then
            Dim dt As Date = PeriodToDate(txtValidFrom.Text)
            viewstate.Item("CurrentPeriodMonth") = dt.Month
            viewstate.Item("CurrentPeriodYear") = dt.Year
            Me.lblCurrentPeriode.Text = Format(dt, "MMM yyyy")
        End If
        Dim txtFooterKodeModel As TextBox = e.Item.FindControl("txtFooterKodeModel")
        Dim txtFooterAmount As TextBox = e.Item.FindControl("txtFooterAmount")
        Dim cboFooterPurpose As DropDownList = e.Item.FindControl("cboFooterPurpose")

        If txtFooterKodeModel.Text.Trim = "" Then
            MessageBox.Show("Tipe tidak boleh kosong")
            Exit Sub
        End If
        oJD = New JaminanDetail
        oJD.VehicleTypeCode = txtFooterKodeModel.Text
        oJD.PeriodMonth = CType(viewstate.Item("CurrentPeriodMonth"), Integer)
        oJD.PeriodYear = CType(viewstate.Item("CurrentPeriodYear"), Integer)
        oJD.Amount = txtFooterAmount.Text
        oJD.Purpose = cboFooterPurpose.SelectedValue
        arlTemp = CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)
        arlTemp.Add(oJD)
        sHelper.SetSession("FrmJaminan.arlJaminanDetail", arlTemp)
        BindDetail()
    End Sub

    Private Sub DTGDelete(ByVal e As DataGridCommandEventArgs)
        arlTemp = CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)
        If arlTemp.Count > 0 Then
            'arlTemp.RemoveAt(e.Item.ItemIndex)
            CType(arlTemp(e.Item.ItemIndex), JaminanDetail).RowStatus = CType(DBRowStatus.Deleted, Short)
            sHelper.SetSession("FrmJaminan.arlJaminanDetail", arlTemp)
            BindDetail()
        End If
    End Sub

    Private Sub DTGEdit(ByVal e As DataGridCommandEventArgs)
        dgSPDetail.ShowFooter = False
        dgSPDetail.EditItemIndex = CInt(e.Item.ItemIndex)
        BindDetail()
    End Sub

    Private Sub DTGCancel(ByVal e As DataGridCommandEventArgs)
        dgSPDetail.EditItemIndex = -1
        BindDetail()
        dgSPDetail.ShowFooter = True
    End Sub

    Private Sub BindDdlPurpose(ByRef ddlPurpose As DropDownList)
        ddlPurpose.Items.Clear()
        For Each li As ListItem In LookUp.ArrayPurpose
            ddlPurpose.Items.Add(li)
        Next
        ddlPurpose.Items.Insert(0, New ListItem("Semua", 2))

    End Sub

    Private Function MergeJaminanDetailInOut() As ArrayList
        Dim arlTemp As New ArrayList

        For Each oJDOut As JaminanDetail In CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)
            Dim dt As Date = DateSerial(CType(viewstate.Item("CurrentPeriodYear"), Integer), CType(viewstate.Item("CurrentPeriodMonth"), Integer), 1)
            oJDOut.PeriodYear = dt.Year
            oJDOut.PeriodMonth = dt.Month
            arlTemp.Add(oJDOut)
        Next
        For Each oJDIn As JaminanDetail In CType(sHelper.GetSession("FrmJaminan.arlJaminanDetailOut"), ArrayList)
            arlTemp.Add(oJDIn)
        Next
        Return arlTemp
    End Function

    Private Function UploadFile(ByRef ObjJaminan As Jaminan) As Integer
        Dim retValue As Integer = 0
        If inFileLocation.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If inFileLocation.PostedFile.ContentLength <> inFileLocation.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") '& KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment")
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)
                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(inFileLocation.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0
                    Throw New Exception("Salah Extention")
                End If

                Dim filename As String = inFileLocation.PostedFile.FileName ' txtSPLNumber.Text.Replace("/", "_") & ext
                Dim fi As System.IO.FileInfo = New System.IO.FileInfo(filename)
                filename = fi.Name
                Dim targetFile As String = New System.Text.StringBuilder(directory). _
                    Append("\").Append(filename).ToString

                Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If fInfo.Exists Then
                    fInfo.Delete()
                End If

                inFileLocation.PostedFile.SaveAs(targetFile)
                Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If Not trgInfo.Exists Then
                    retValue = 0
                End If
                Dim strFileSave As String = filename ' KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment") & "\" & filename
                ObjJaminan.Attachment = strFileSave
                retValue = 1
            Catch ex As Exception
                retValue = 0
            Finally
                sapImp.StopImpersonate()
            End Try
            Return retValue
        Else
            retValue = 1
            Return retValue
        End If
    End Function

    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "PDF" Or ext.ToUpper() = "XLS" Or ext.ToUpper() = "DOC" Or ext.ToUpper() = "ZIP" Or ext.ToUpper() = "RAR" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Sub UpdatePKDepositStatus(ByVal oJ As Jaminan)
        Dim oPKHFac As PKHeaderFacade = New PKHeaderFacade(User)
        Dim crtPKH As CriteriaComposite
        Dim arlPKH As New ArrayList

        'delete first
        crtPKH = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPKH.opAnd(New Criteria(GetType(PKHeader), "JaminanID", MatchType.Exact, oJ.ID))
        arlPKH = oPKHFac.Retrieve(crtPKH)
        For Each oPKH As PKHeader In arlPKH
            oPKHFac.Delete(opkh)
        Next
        If oJ.Status = 0 Then
            For Each oPKH As PKHeader In GetRelatedPK(False)
                oPKH.JaminanID = oJ.ID
                oPKHFac.Update(oPKH)
            Next
        End If
    End Sub
    Private Function GetRelatedPK(Optional ByVal IsOnlyChecking As Boolean = False) As ArrayList
        Dim oPKHFac As PKHeaderFacade = New PKHeaderFacade(User)
        Dim crtPKH As CriteriaComposite
        Dim arlPKH As New ArrayList
        Dim arlResult As New ArrayList
        If IsOnlyChecking Then
            crtPKH = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPKH.opAnd(New Criteria(GetType(PKHeader), "JaminanID", MatchType.Exact, oJ.ID))
            arlPKH = oPKHFac.Retrieve(crtPKH)
            Return arlPKH
        End If
        For Each sDealerCode As String In oJ.DealerCodes
            Dim dtStart As Date = DateSerial(oJ.ValidFrom.Year, oJ.ValidFrom.Month, 1)
            Dim dtEnd As Date = DateSerial(oJ.ValidTo.Year, oJ.ValidTo.Month, 1)
            Dim Sql As String = ""
            Dim Purposes As String = ""
            Dim VehicleTypeCodes As String = ""

            For Each oJD As JaminanDetail In oJ.JaminanDetails
                Purposes &= IIf(Purposes.Trim = "", "", ", ") & oJD.Purpose
                If oJD.Purpose = 2 Then 'semua
                    VehicleTypeCodes &= IIf(VehicleTypeCodes.Trim = "", "", ", ") & "'" & oJD.VehicleTypeCode & ".0'"
                    VehicleTypeCodes &= IIf(VehicleTypeCodes.Trim = "", "", ", ") & "'" & oJD.VehicleTypeCode & ".1'"
                Else
                    VehicleTypeCodes &= IIf(VehicleTypeCodes.Trim = "", "", ", ") & "'" & oJD.VehicleTypeCode & "." & oJD.Purpose & "'"
                End If
            Next

            dtEnd = dtEnd.AddMonths(1).AddDays(-1)
            Sql &= "select distinct(pkh.ID)  "
            Sql &= "from PKHeader pkh  "
            Sql &= " , Dealer d "
            Sql &= " , PKDetail pkd "
            Sql &= " where pkh.ID=pkd.PKHeaderID and pkh.DealerID=d.ID and pkh.RowStatus=0 and pkh.PKStatus=0 "
            Sql &= " and pkh.PKStatus in(0,2,3) and d.DealerCode='" & sDealerCode & "' "
            'Sql &= " and pkh.Purpose in (" & Purposes & ") "
            Sql &= " and  cast(cast(pkh.RequestPeriodeYear as varchar(4)) "
            Sql &= "  +'.'+cast(pkh.RequestPeriodeMonth as varchar(2))"
            Sql &= "  +'.01' "
            Sql &= "  as datetime) between '" & Format(dtStart, "yyyy.MM.dd") & "' "
            Sql &= " and '" & Format(dtEnd, "yyyy.MM.dd") & "'"
            Sql &= " and pkd.VehicleTypeCode+'.'+cast(pkh.Purpose as varchar) in (" & VehicleTypeCodes & ")  "

            crtPKH = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPKH.opAnd(New Criteria(GetType(PKHeader), "ID", MatchType.InSet, "(" & Sql & ")"))
            arlPKH = oPKHFac.Retrieve(crtPKH)
            For Each oPKH As PKHeader In arlPKH
                arlResult.Add(oPKH)
                If IsOnlyChecking Then
                    Return arlResult
                End If
            Next
        Next

        Return arlResult
    End Function

    Private Sub CheckUserPrivilege()
        If SecurityProvider.Authorize(Context.User, SR.jaminan_lihat_privilege) _
            OrElse SecurityProvider.Authorize(Context.User, SR.jaminan_buat_privilege) _
            OrElse SecurityProvider.Authorize(Context.User, SR.jaminan_lihat_privilege) _
        Then
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Jaminan")
        End If
        If SecurityProvider.Authorize(Context.User, SR.jaminan_buat_privilege) _
            OrElse SecurityProvider.Authorize(Context.User, SR.jaminan_lihat_privilege) _
        Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            Initialization()
            BindData()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not IsDataValid() Then Exit Sub

        oJ = New Jaminan
        If CType(viewstate.Item("Mode"), String).Trim.ToUpper <> "Add".ToUpper Then
            oJ = oJFac.Retrieve(CType(viewstate.Item("ID"), Integer))
        End If
        oJ.DealerCode = Me.txtDealerName.Text
        oJ.Description = Me.txtDescription.Text
        oJ.DepositInfo = Me.txtDepositInfo.Text
        oJ.ValidFrom = PeriodToDate(txtValidFrom.Text)
        oJ.ValidTo = PeriodToDate(txtValidTo.Text).AddMonths(1).AddDays(-1)
        'Me.inFileLocation.Value = "" 'oJ.Attachment
        'Me.lblAttachment.Text = oJ.Attachment
        oJ.Status = Me.ddlStatus.SelectedValue

        If UploadFile(oJ) = 1 Then
            'oJFac.Update(oJ)
        End If
        oJ.JaminanDetails = MergeJaminanDetailInOut() ' CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)

        Try
            If CType(viewstate.Item("Mode"), String).Trim.ToUpper <> "Add".ToUpper Then
                oJFac.Update(oJ)
            Else
                oJFac.Insert(oJ)
                viewstate.Item("Mode") = "Edit"
                viewstate.Item("ID") = oJ.ID
            End If
            MessageBox.Show(SR.SaveSuccess)
            sHelper.SetSession("FrmJaminan.objJaminan", oJ)
            UpdatePKDepositStatus(oJ)
            oJ = oJFac.Retrieve(oJ.ID)
            sHelper.SetSession("FrmJaminan.objJaminan", oJ)
            BindDataToForm()
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    Private Sub dgSPDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSPDetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oJD As JaminanDetail = CType(CType(sHelper.GetSession("FrmJaminan.arlJaminanDetail"), ArrayList)(e.Item.ItemIndex), JaminanDetail)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblNamaType As Label = e.Item.FindControl("lblNamaType")
            Dim lblViewAmount As Label = e.Item.FindControl("lblViewAmount")
            Dim lblPurpose As Label = e.Item.FindControl("lblPurpose")

            lblNo.Text = e.Item.ItemIndex + 1
            lblNamaType.Text = oJD.VehicleTypeCode
            lblViewAmount.Text = FormatNumber(oJD.Amount, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            If oJD.Purpose = CType(LookUp.enumPurpose.Biasa, Short) Then lblPurpose.Text = "Biasa"
            If oJD.Purpose = CType(LookUp.enumPurpose.Khusus, Short) Then lblPurpose.Text = "Khusus"
            If oJD.Purpose = CType(LookUp.enumPurpose.Semua, Short) Then lblPurpose.Text = "Semua"
            If oJD.RowStatus = CType(DBRowStatus.Deleted, Short) Then e.Item.Visible = False
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            BindDTGEdit(e)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            BindDTGFooter(e)
        End If
    End Sub

    Private Sub dgSPDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSPDetail.ItemCommand
        Select Case (e.CommandName)
            Case "Delete"
                DTGDelete(e)
            Case "Add"
                DTGAdd(e)
            Case "Edit"
                DTGEdit(e)
            Case "Update"
                DTGUpdate(e)
            Case "Cancel"
                DTGCancel(e)
        End Select
    End Sub

    Private Sub lbtnPrevMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnPrevMonth.Click
        If CType(viewstate.Item("ID"), Integer) <= 0 Then Exit Sub
        Dim dt As Date = DateSerial(CType(viewstate.Item("CurrentPeriodYear"), Integer), CType(viewstate.Item("CurrentPeriodMonth"), Integer), 1)
        oJ = CType(sHelper.GetSession("FrmJaminan.objJaminan"), Jaminan)
        dt = dt.AddMonths(-1)
        If dt >= oJ.ValidFrom Then
            viewstate.Item("CurrentPeriodMonth") = dt.Month
            viewstate.Item("CurrentPeriodYear") = dt.Year
            sHelper.SetSession("FrmJaminan.arlJaminanDetail", oJ.JaminanDetailIn(dt.Month, dt.Year))
            sHelper.SetSession("FrmJaminan.arlJaminanDetailOut", oJ.JaminanDetailOut(dt.Month, dt.Year))
        End If
        BindDetail()
    End Sub

    Private Sub lbtnNextMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnNextMonth.Click
        If CType(viewstate.Item("ID"), Integer) <= 0 Then Exit Sub
        Dim dt As Date = DateSerial(CType(viewstate.Item("CurrentPeriodYear"), Integer), CType(viewstate.Item("CurrentPeriodMonth"), Integer), 1)
        oJ = CType(sHelper.GetSession("FrmJaminan.objJaminan"), Jaminan)
        dt = dt.AddMonths(1)
        If dt <= oJ.ValidTo Then
            viewstate.Item("CurrentPeriodMonth") = dt.Month
            viewstate.Item("CurrentPeriodYear") = dt.Year
            sHelper.SetSession("FrmJaminan.arlJaminanDetail", oJ.JaminanDetailIn(dt.Month, dt.Year))
            sHelper.SetSession("FrmJaminan.arlJaminanDetailOut", oJ.JaminanDetailOut(dt.Month, dt.Year))
        End If
        BindDetail()
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmJaminanList.aspx")
    End Sub

#End Region
End Class
