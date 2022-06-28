#Region "Summary"
'------------------------------------------------------------------'
'-- Author Name   : Agus Pirnadi                                 --'
'-- PURPOSE       :                                              --'
'-- SPECIAL NOTES :                                              --'
'------------------------------------------------------------------'
'-- Copyright © 2005                                             --'
'------------------------------------------------------------------'
'-- $History      : $                                            --'
'-- Generated on 26/10/2005                                      --'
'------------------------------------------------------------------'
#End Region

#Region ".NET Namespace"
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmWSCDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon6 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents Label23 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents chkDamagePart As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkKwitansi As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkPhoto As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblColo18 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl1 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl2 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl3 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl4 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl5 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl6 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl7 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl8 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblServiceDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimType As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefClaimNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblServiceDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDecideDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblMileage As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents lbl10 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl11 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl12 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl13 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl14 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl15 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl16 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl17 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl18 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSoldDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblVehicleType As System.Web.UI.WebControls.Label
    Protected WithEvents lblChassisNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblEngineNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblSerialNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblDeliveryDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblNotification As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblReason As System.Web.UI.WebControls.Label
    Protected WithEvents lbl19 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl20 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl21 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeA As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeB As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeC As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl22 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl23 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl24 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl25 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCreateBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblCreateDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblPartReceiveBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblPartReceiveDate As System.Web.UI.WebControls.Label
    Protected WithEvents dgWSCDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents btnSendEmail As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dbBukti As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPQR As System.Web.UI.WebControls.LinkButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private sessHelp As SessionHelper = New SessionHelper
    Dim WSCHead As WSCHeader  '-- WSC header and its details
#End Region

#Region "Custom Method"


    Private Function GetPositionCode(ByVal tipe As String, ByVal val As String) As String
        Dim positionFacade As PositionWSCFacade = New PositionWSCFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PositionWSC), "PositionCategory", MatchType.Exact, tipe))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PositionWSC), "PositionCode", MatchType.Exact, val))
        Dim list As ArrayList = positionFacade.Retrieve(criterias)
        If list.Count > 0 Then
            Return CType(list(0), PositionWSC).Description
        Else
            Return String.Empty
        End If

    End Function

    Private Sub BindData()
        '-- Bind header fields & its detail grid
        Dim _date As Date = New Date(1900, 1, 1)

        '-- Assign header fields
        With WSCHead

            If Not IsNothing(.Dealer) Then
                '-- Service Dealer
                lblServiceDealer.Text = .Dealer.DealerCode & " / " & .Dealer.DealerName
            End If
            lblClaimType.Text = .ClaimType      '-- Claim Type
            lblClaimNumber.Text = .ClaimNumber  '-- Claim Number
            lblRefClaimNumber.Text = .RefClaimNumber
            If .ServiceDate > _date Then
                lblServiceDate.Text = IIf(Format(.ServiceDate, "dd/MM/yyyy") <> "01/01/1753", Format(.ServiceDate, "dd/MM/yyyy"), "")
            Else
                lblServiceDate.Text = ""
            End If

            If .DecideDate > _date Then
                lblDecideDate.Text = IIf(Format(.DecideDate, "dd/MM/yyyy") <> "01/01/1753", Format(.DecideDate, "dd/MM/yyyy"), "")
            Else
                lblDecideDate.Text = ""
            End If
            lblMileage.Text = .Miliage
            lblPQR.Text = .PQR
            'Dim objPQR As PQRHeader = New PQRHeaderFacade(User).Retrieve(.PQR)
            'If Not objPQR Is Nothing Then
            '    If objPQR.ID > 0 Then
            '        lblPQR.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpPQRDetails.aspx?PQRNo=" & .PQR & "', '', 720, 720, DummyFunction);return false;")
            '    End If
            'End If

            lblDescription.Text = .Description

            If Not IsNothing(.ChassisMaster) AndAlso Not IsNothing(.ChassisMaster.Dealer) Then
                lblSoldDealer.Text = .ChassisMaster.Dealer.DealerCode & " / " & .ChassisMaster.Dealer.DealerName
            End If
            If Not IsNothing(.ChassisMaster) AndAlso Not IsNothing(.ChassisMaster.VechileColor) Then
                lblVehicleType.Text = .ChassisMaster.VechileColor.MaterialNumber & " / " & .ChassisMaster.VechileColor.MaterialDescription
            End If
            If Not IsNothing(.ChassisMaster) Then
                lblChassisNumber.Text = .ChassisMaster.ChassisNumber
                lblEngineNumber.Text = .ChassisMaster.EngineNumber
                lblSerialNumber.Text = .ChassisMaster.SerialNumber
                If .ChassisMaster.DODate > _date Then
                    lblDeliveryDate.Text = IIf(Format(.ChassisMaster.DODate, "dd/MM/yyyy") <> "01/01/1753", Format(.ChassisMaster.DODate, "dd/MM/yyyy"), "")
                Else
                    lblDeliveryDate.Text = ""
                End If
            End If

            lblNotification.Text = .NotificationNumber
            Try
                lblStatus.Text = CType(.Status, enumStatusWSC.Status).ToString
            Catch ex As Exception
                lblStatus.Text = String.Empty
            End Try

            If Not IsNothing(.Reason) Then lblReason.Text = .Reason.Description

            lblKodeA.Text = " " & .CodeA
            lblKodeA.ToolTip = GetPositionCode("A", .CodeA)
            lblKodeB.Text = " " & .CodeB
            lblKodeB.ToolTip = GetPositionCode("B", .CodeB)
            lblKodeC.Text = " " & .CodeC
            lblKodeC.ToolTip = GetPositionCode("C", .CodeC)
            chkDamagePart.Checked = (.EvidenceDmgPart = "X")
            chkKwitansi.Checked = (.EvidenceInvoice = "X")
            chkPhoto.Checked = (.EvidencePhoto = "X")

            lblCreateBy.Text = IIf(.CreatedBy <> "", UserInfo.Convert(.CreatedBy), "")
            lblCreateDate.Text = IIf(Format(.CreatedTime, "dd/MM/yyyy") <> "01/01/1753", Format(.CreatedTime, "dd/MM/yyyy hh:mm"), "")

            lblPartReceiveBy.Text = IIf(.PartReceiveBy <> "", UserInfo.Convert(.PartReceiveBy), "")
            lblPartReceiveDate.Text = IIf(Format(.PartReceiveTime, "dd/MM/yyyy") <> "01/01/1753", Format(.PartReceiveTime, "dd/MM/yyyy hh:mm"), "")

            '-- Bind details
            dgWSCDetail.DataSource = WSCHead.WSCDetails
            dgWSCDetail.DataBind()
        End With

    End Sub

#End Region

#Region "EventHandler"

    Private Sub BindEvidance(ByVal _wscHead As WSCHeader)
        If Not _wscHead Is Nothing Then
            Dim evidanceFacade As WSCEvidenceFacade = New WSCEvidenceFacade(User)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.WSCEvidence), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidence), "WSCHeader.ID", MatchType.Exact, WSCHead.ID))
            Dim evidanceList As ArrayList = evidanceFacade.Retrieve(criterias)
            dbBukti.DataSource = evidanceList
            dbBukti.DataBind()
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then

            If Not IsNothing(sessHelp.GetSession("WSCHead")) Then
                'Check User Privilege
                UserPrivilege()

                '-- Retrieve WSC header and its details from session
                WSCHead = CType(sessHelp.GetSession("WSCHead"), WSCHeader)
                BindEvidance(WSCHead)
                BindData()  '-- Bind header fields & its detail grid
            End If
        End If
        If Not WSCHead Is Nothing Then
            If Not WSCHead.CreatedBy.ToUpper = "WSM" Then
                btnSendEmail.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmWSCSendEmail.aspx", "", 600, 600, "DummyFunction")
            Else
                btnSendEmail.Attributes("onclick") = "alert('Email tidak valid');"
            End If
        Else
            btnSendEmail.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmWSCSendEmail.aspx", "", 600, 600, "DummyFunction")
        End If
    End Sub

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.WSCStatusDetailView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Rincian Status WSC")
        End If
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.WSCStatusDetailSave_Privilege)
        btnSendEmail.Visible = SecurityProvider.Authorize(Context.User, SR.WSCStatusDetailMintaBukti_Privilege)
        chkDamagePart.Enabled = SecurityProvider.Authorize(Context.User, SR.WSCStatusDetailSave_Privilege)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("FrmWSCStatusList.aspx")
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '-- Update WSC header's change

        WSCHead = CType(sessHelp.GetSession("WSCHead"), WSCHeader)  '-- Retrieve WSC header and its details

        '-- Assign change
        WSCHead.EvidenceDmgPart = IIf(chkDamagePart.Checked, "X", "")
        WSCHead.PartReceiveBy = User.Identity.Name
        WSCHead.PartReceiveTime = Now.Date

        Dim objHeader As WSCHeaderFacade = New WSCHeaderFacade(User)

        Try
            objHeader.Update(WSCHead)  '-- Update WSC header
            sessHelp.SetSession("WSCHead", WSCHead)  '-- Restore WSC header and its details
            BindData()  '-- Re-bind header fields & its detail grid

            MessageBox.Show(SR.UpdateSucces)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try

    End Sub

    Private Function GetKodeKerja(ByVal code As String) As String
        Dim _kerjaFacade As DeskripsiWorkPositionFacade = New DeskripsiWorkPositionFacade(User)
        Dim obj As DeskripsiKodeKerja = _kerjaFacade.Retrieve(code)
        If obj.ID > 0 Then
            Return obj.Description
        Else
            Return "Belum di difinisikan"
        End If
    End Function

    Private Function GetKodePosisi(ByVal code As String) As String
        Dim _posisiFacade As DeskripsiPositionCodeFacade = New DeskripsiPositionCodeFacade(User)
        Dim obj As DeskripsiKodePosisi = _posisiFacade.Retrieve(code)
        If obj.ID > 0 Then
            Return obj.Description
        Else
            Return "Belum di difinisikan"
        End If
    End Function

    Private Sub dgWSCDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgWSCDetail.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgWSCDetail.CurrentPageIndex * dgWSCDetail.PageSize)
            Dim row As WSCDetail = CType(e.Item.DataItem, WSCDetail)
            Dim lblKerja As Label = CType(e.Item.FindControl("lblKerja"), Label)
            Dim lblCode As Label = CType(e.Item.FindControl("lblCode"), Label)
            'Dim chkMainPart As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            'Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
            Dim lblQuantityReceived As Label = CType(e.Item.FindControl("lblQuantityReceived"), Label)
            Dim lblReceivedBy As Label = CType(e.Item.FindControl("lblReceivedBy"), Label)
            Dim lblReceivedDate As Label = CType(e.Item.FindControl("lblReceivedDate"), Label)

            If row.Type.Trim.ToUpper = "LABOR" Then
                lblKerja.Text = row.WorkQty
                lblKerja.ToolTip = GetKodeKerja(lblKerja.Text.Trim)
                lblCode.ToolTip = GetKodePosisi(lblCode.Text.Trim)
                'lblAmount.Text = Format(row.WSCHeader.LaborAmount, "#,##0")
            Else
                lblKerja.Text = String.Empty
                Dim sp As SparePartMaster = row.SparePartMaster
                If Not sp Is Nothing Then
                    lblKerja.ToolTip = ""
                    lblCode.ToolTip = row.SparePartMaster.PartName
                Else
                    lblKerja.ToolTip = ""
                    lblCode.ToolTip = "Sparepart tidak ditemukan"
                End If
                'lblAmount.Text = Format(row.WSCHeader.PartAmount, "#,##0")
                If row.QuantityReceived >= 0 Then
                    lblQuantityReceived.Text = row.QuantityReceived
                Else
                    lblQuantityReceived.Text = ""
                End If
                lblReceivedBy.Text = row.ReceivedBy
                If row.ReceivedDate > New Date(1900, 1, 1) Then
                    lblReceivedDate.Text = Format(row.ReceivedDate, "dd/MM/yyyy")
                Else

                    lblReceivedDate.Text = ""
                End If

                'Belum di apply, tunggu sap
                Dim chkItemChecked As CheckBox = New CheckBox
                chkItemChecked.Enabled = False
                If row.MainPart = 1 Then
                    chkItemChecked.Checked = True
                Else
                    chkItemChecked.Checked = False
                End If
                e.Item.Cells(1).Controls.Add(chkItemChecked)
            End If
        End If
    End Sub
#End Region


    Private Sub dbBukti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dbBukti.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblLihat As LinkButton = CType(e.Item.FindControl("lblLihat"), LinkButton)
            Dim lblBukti As Label = CType(e.Item.FindControl("lblBukti"), Label)
            lblLihat.Text = "<img src=""../images/download.gif"" border=""0"" alt=""Download"">"
            lblBukti.Text = "Bukti " & e.Item.ItemIndex + 1
        End If
    End Sub



    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function

    Private Sub dbBukti_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dbBukti.ItemCommand

        Dim linkButton As linkButton = e.Item.FindControl("lbnLihat")
        Dim lblPath As Label = CType(e.Item.FindControl("lblPath"), Label)
        Dim fileInfox As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & lblPath.Text)
        Dim fileExist As Boolean = CheckFileExist(fileInfox)
        If fileExist Then
            Try
                Response.Redirect("../Download.aspx?file=" & lblPath.Text)
            Catch ex As Exception
                MessageBox.Show(SR.DownloadFail(fileInfox.Name))
            End Try
        Else
            MessageBox.Show(SR.FileNotFound(fileInfox.Name))
        End If


    End Sub

    Private Sub lblPQR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPQR.Click
        Dim objPQR As PQRHeader = New PQRHeaderFacade(User).Retrieve(lblPQR.Text)
        If Not objPQR Is Nothing Then
            If objPQR.ID < 1 Then
                MessageBox.Show("PQR tidak ditemukan.")
            Else
                sessHelp.SetSession("PQR", objPQR)
                Server.Transfer("~/PQR/FrmPQRHeader.aspx?Mode=View&Src=WSCDetail")
            End If
        Else
            MessageBox.Show("PQR tidak ditemukan.")
        End If
    End Sub
End Class

