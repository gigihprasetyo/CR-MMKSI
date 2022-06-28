#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Parser
Imports System.IO
#End Region

Public Class FrmEntryGyro2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEntryType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents lblColonKategori As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblBank As System.Web.UI.WebControls.Label
    Protected WithEvents ddlBank As System.Web.UI.WebControls.DropDownList
    Protected WithEvents calReqDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnCheckGyroDate As System.Web.UI.WebControls.Button
    Protected WithEvents txtGyroNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlGyroType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblGyroDate As System.Web.UI.WebControls.Label
    Protected WithEvents calBaseline As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlPaymentPurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblRef As System.Web.UI.WebControls.Label
    Protected WithEvents lblColonRef As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefSlipNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblAcc As System.Web.UI.WebControls.Label
    Protected WithEvents lblColonAccelerate As System.Web.UI.WebControls.Label
    Protected WithEvents calAccelerated As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnUpdateDTG As System.Web.UI.WebControls.Button
    Protected WithEvents lblAcceleratedDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalGyro As System.Web.UI.WebControls.Label
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents calTest As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblRegNumber As System.Web.UI.WebControls.Label

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

    Private _sessHelper As New SessionHelper
    Private _vstMode As String = "_vstMode"
    Private _vstEdit As String = "_vstEdit"
    Private _vstNew As String = "_vstNew"
    Private _sesData As String = "FrmEntryGyro.Data"
    Private _sesAssignment As String = "FrmEntryGyro.Assignment"
    Private _vstCalGyroValue As String = "calJatuhTempo.Value"
    Private _vstCalAccValue As String = "calAccelerated.Value"
    Private _vstCalBaselineValue As String = "calBaseline.Value"
    Private _AllowedDifference As Decimal = 1000 ' 5
#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        Dim oD As Dealer = CType(Session("DEALER"), Dealer)

        lblDealerCode.Text = oD.DealerCode & "/" & oD.DealerName
        Me.BindGyroType()
        BindEntry()
        BindBank(True)
        BindCategory()
        BindPaymentPurpose()
        ClearData()
        Me.ViewState.Add(Me._vstCalGyroValue, Me.calReqDate.Value)
        Me.ViewState.Add(Me._vstCalAccValue, Me.calAccelerated.Value)
        Me.ViewState.Add(Me._vstCalBaselineValue, Me.calBaseline.Value)

        Me._sessHelper.SetSession(_sesData, New ArrayList)
        Me._sessHelper.SetSession(Me._sesAssignment, Nothing)
        Me.ViewState.Add(_vstMode, _vstNew)

        Me.ViewState.Add("TotalAmount", 0)

        Dim Url As String = Me._sessHelper.GetSession("FrmEntryGyro.PageOpener")
        btnBack.Visible = (Url <> "")
        SetControl("UserLogin")

        If Not IsNothing(Request.Item("DPID")) AndAlso Request.Item("DPID") > 0 Then
            Dim oDP As DailyPayment = New DailyPaymentFacade(User).Retrieve(CType(Request.Item("DPID"), Integer))
            Dim IsAccelerate As Boolean = Me.IsAcceleration()

            Me.btnSave.Enabled = True
            'If Not IsAccelerate And Val(Request.Item("DPID")) > 0 Then
            '    Dim cPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "AcceleratorID", MatchType.Exact, Val(Request.Item("DPID"))))
            '    Dim arl As ArrayList = New DailyPaymentFacade(User).Retrieve(cPP)
            '    If arl.Count > 0 Then
            '        IsAccelerate = True
            '    End If
            'End If
            SetControl("GyroStatus", oDP)
            If Not IsNothing(oDP) AndAlso oDP.ID > 0 AndAlso IsAccelerate = False Then
                'Dim sTemps() As String = oDP.SlipNumber.Split(" ")
                Dim i As Integer = 0
                Dim sBankName As String = ""
                Dim sGyroNumber As String = ""

                Me.btnSave.Enabled = True
                Me.ddlGyroType.SelectedValue = oDP.GyroType ' EnumGyroType.GyroType.Normal
                Me.ddlGyroType_SelectedIndexChanged(Nothing, Nothing)
                Me.lblRegNumber.Text = oDP.DailyPaymentHeader.RegNumber
                Dim intSpacePos As Integer = oDP.SlipNumber.IndexOf(" ")
                Dim strFirst As String = ""
                Dim strSecond As String = ""
                strFirst = Left(oDP.SlipNumber, intSpacePos).Trim
                strSecond = Right(oDP.SlipNumber, oDP.SlipNumber.Length - intSpacePos).Trim

                'Posisi kode bank terbalik antara transfer dan giro
                Try
                    Me.ddlBank.SelectedValue = strFirst
                Catch ex As Exception
                    Try
                        Me.ddlBank.SelectedValue = strSecond
                    Catch exx As Exception
                        Me.ddlBank.SelectedValue = oDP.Bank.BankCode
                    End Try
                End Try
                Try
                    Me.ddlBank.SelectedValue = oDP.Bank.BankCode
                Catch ex As Exception
                    If Me.ddlBank.Items.Count > 0 Then
                        Me.ddlBank.SelectedIndex = 0
                    End If
                End Try
                Me.ddlEntryType.SelectedValue = oDP.EntryType
                'ddlEntryType_SelectedIndexChanged(Me, System.EventArgs.Empty)
                SetGyroNumberControl()

                ddlBank_SelectedIndexChanged(Me, System.EventArgs.Empty)

                If ddlEntryType.SelectedValue = CShort(EnumGyroEntryType.EntryType.Gyro).ToString Then
                    Me.txtGyroNumber.Text = strSecond
                Else
                    Me.txtGyroNumber.Text = oDP.SlipNumber
                End If

                Me.calReqDate.Value = oDP.POHeader.ReqAllocationDateTime
                Me.calBaseline.Value = oDP.BaselineDate
                Me.ddlPaymentPurpose.SelectedValue = oDP.PaymentPurpose.ID
                If Not IsNothing(oDP.OldDailyPayment()) AndAlso oDP.OldDailyPayment.ID > 0 Then 'Gyro ini Percepatan, Mode=Edit
                    Me.lblAcc.Visible = False
                    Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                    Me.calAccelerated.Visible = False
                    'Me.btnUpdateDTG.Visible = Me.calAccelerated.Visible
                End If
                Me.ViewState.Item(_vstMode) = _vstEdit
                Me._sessHelper.SetSession(Me._sesData, Me.GetDataFromDB(oDP.EntryType, oDP.SlipNumber, oDP.BaselineDate, oDP.PaymentPurpose.ID, Val(Request.Item("DPID"))))
                Me.BindData()
            Else
                Me.lblRefSlipNumber.Text = oDP.SlipNumber
                'Accelerate
                If Not IsNothing(oDP) AndAlso oDP.ID > 0 AndAlso IsAccelerate Then
                    Me.calReqDate.Value = oDP.POHeader.ReqAllocationDateTime
                    Me.calBaseline.Value = oDP.BaselineDate
                    ddlPaymentPurpose.SelectedValue = oDP.PaymentPurpose.ID
                    Me.ddlPaymentPurpose.Enabled = False
                    Me.lblRegNumber.Text = oDP.DailyPaymentHeader.RegNumber
                    'If oDP.GyroType = 0 Then
                    '    Me.ddlGyroType.SelectedIndex = 0
                    'Else
                    '    Me.ddlGyroType.SelectedValue = oDP.GyroType
                    'End If
                    Try
                        ddlGyroType.SelectedValue = EnumGyroType.GyroType.GantiGyro
                    Catch ex As Exception
                        ddlGyroType.SelectedValue = oDP.GyroType
                    End Try
                    Me.ddlGyroType_SelectedIndexChanged(Nothing, Nothing)
                    ddlEntryType.SelectedValue = oDP.EntryType
                    ddlEntryType_SelectedIndexChanged(Me, System.EventArgs.Empty)

                    Dim intSpacePos As Integer = oDP.SlipNumber.IndexOf(" ")
                    Dim strFirst As String = ""
                    Dim strSecond As String = ""
                    strFirst = Left(oDP.SlipNumber, intSpacePos).Trim
                    strSecond = Right(oDP.SlipNumber, oDP.SlipNumber.Length - intSpacePos).Trim

                    'Posisi kode bank terbalik antara transfer dan giro
                    BindBank(True)
                    Try
                        Me.ddlBank.SelectedValue = strFirst
                    Catch ex As Exception
                        Try
                            Me.ddlBank.SelectedValue = strSecond
                        Catch exx As Exception
                            Dim objBank As Bank = New BankFacade(User).Retrieve(oDP.BankID)
                            Me.ddlBank.SelectedValue = objBank.BankCode
                        End Try
                    End Try

                    ddlBank_SelectedIndexChanged(Me, System.EventArgs.Empty)

                    If ddlEntryType.SelectedValue = CShort(EnumGyroEntryType.EntryType.Gyro).ToString Then
                        Me.txtGyroNumber.Text = strSecond
                    Else
                        Me.txtGyroNumber.Text = oDP.SlipNumber
                    End If
                    Me._sessHelper.SetSession(Me._sesData, Me.GetDataFromDB(oDP.EntryType, oDP.SlipNumber, oDP.BaselineDate, oDP.PaymentPurpose.ID, Val(Request.Item("DPID"))))

                End If
            End If
        Else
            Me.ddlGyroType.SelectedValue = EnumGyroType.GyroType.Normal
            Me.ddlGyroType_SelectedIndexChanged(Nothing, Nothing)
        End If

        'Edit selalu satu row
        If Val(Request.Item("DPID")) <> 0 Then
            dtgMain.ShowFooter = False
        End If

    End Sub

    Private Function GetDataFromDB(ByVal EntryType As Short, ByVal SlipNumber As String, ByVal BaselineDate As Date, ByVal PaymentPurposeID As Integer, Optional ByVal DPID As Integer = 0) As ArrayList
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sDP As SortCollection = New SortCollection
        Dim aDP As New ArrayList
        Dim oDP As DailyPayment

        oDP = oDPFac.Retrieve(DPID)
        If Not IsNothing(oDP) AndAlso oDP.ID > 0 Then
            Return oDP.DailyPaymentHeader.DailyPayments
        Else
            Return New ArrayList
        End If
        'cDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.Exact, DPID))

        'cDP.opAnd(New Criteria(GetType(DailyPayment), "EntryType", MatchType.Exact, EntryType))
        'cDP.opAnd(New Criteria(GetType(DailyPayment), "SlipNumber", MatchType.Exact, SlipNumber))
        'cDP.opAnd(New Criteria(GetType(DailyPayment), "BaselineDate", MatchType.Exact, BaselineDate.ToString("yyyy/MM/dd")))
        'cDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.Exact, PaymentPurposeID))
        'If DPID <> 0 Then
        '    'cDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.Exact, DPID))
        'End If
        'sDP.Add(New Sort(GetType(DailyPayment), "ID", Sort.SortDirection.ASC))
        'aDP = oDPFac.Retrieve(cDP, sDP)

        Return aDP
    End Function

    Private Sub BindData()
        'If Me.ViewState.Item(Me._vstMode) = Me._vstNew Then
        '    Dim aTemp As New ArrayList
        '    Me._sessHelper.SetSession(Me._sesData, aTemp)
        'End If
        Me.ViewState.Item("TotalAmount") = 0
        Me._sessHelper.SetSession(Me._sesAssignment, Nothing)
        Me.dtgMain.DataSource = Me._sessHelper.GetSession(Me._sesData)
        Me.dtgMain.DataBind()
    End Sub

    Private Sub BindGyroType()
        Dim aItems As ArrayList = EnumGyroType.GetList()
        Dim IsAccelerate As Boolean = Me.IsAcceleration()
        If Not IsAccelerate And Val(Request.Item("DPID")) > 0 Then
            Dim cPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "AcceleratorID", MatchType.Exact, Val(Request.Item("DPID"))))
            Dim arl As ArrayList = New DailyPaymentFacade(User).Retrieve(cPP)
            If arl.Count > 0 Then
                IsAccelerate = True
            End If
        End If


        Me.ddlGyroType.Items.Clear()
        If IsAccelerate Then
            Me.ddlGyroType.Items.Add(New ListItem("Silahkan Pilih", -1))
        End If
        For Each li As ListItem In aItems
            If CType(li.Value, Short) = EnumGyroType.GyroType.Normal Then
                If Not IsAccelerate Then
                    Me.ddlGyroType.Items.Add(li)
                End If
            Else
                If IsAccelerate Then
                    Me.ddlGyroType.Items.Add(li)
                End If
            End If
            'Me.ddlGyroType.Items.Add(li)
        Next
        'Me.ddlEntryType.Items.Clear()
        'Me.ddlBank.Items.Clear()
        BindEntry()
    End Sub


    Private Sub BindCategory()
        Dim aC As ArrayList = New PKHeaderFacade(User).RetrieveListCategory

        Me.ddlCategory.Items.Clear()
        Me.ddlCategory.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each oC As Category In aC
            Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
        Next
    End Sub


    Private Sub BindEntry()
        Dim aItems As ArrayList = EnumGyroEntryType.GetList()
        Dim GyroType As Short = CType(Me.ddlGyroType.SelectedValue, Short)

        Me.ddlEntryType.Items.Clear()
        For Each li As ListItem In aItems
            If Not ((GyroType = EnumGyroType.GyroType.Percepatan OrElse GyroType = EnumGyroType.GyroType.Tolakan) _
            AndAlso CType(li.Value, Short) = EnumGyroEntryType.EntryType.Gyro) Then
                Me.ddlEntryType.Items.Add(li)
            End If
        Next
        Dim OldBankValue As String = "-1"
        If ddlBank.Items.Count > 0 Then OldBankValue = Me.ddlBank.SelectedValue
        Me.ddlBank.Items.Clear()
        BindBank()
        Try
            Me.ddlBank.SelectedValue = OldBankValue
        Catch ex As Exception

        End Try
    End Sub

    Private Function IsAcceleration() As Boolean

        Dim result As Boolean = False

        If Not IsNothing(Request.Item("IsAccelerate")) AndAlso Request.Item("IsAccelerate") = "1" Then
            result = True
        Else
        End If


        Return result
    End Function

    Private Function IsEditing() As Boolean
        If Not IsNothing(Request.Item("DPID")) AndAlso CType(Request.Item("DPID"), Integer) > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Function IsExistInList(ByVal sData As String, ByVal sList As String()) As Boolean
        Dim i As Integer
        For i = 0 To sList.Length - 1
            If sData.Trim.ToUpper = sList(i).Trim.ToUpper Then Return True
        Next
        Return False
    End Function

    Private Sub BindBank(Optional ByVal isAll As Boolean = False)
        Dim oBFac As BankFacade = New BankFacade(User)
        Dim aB As ArrayList = oBFac.RetrieveActiveList()
        Dim IsAccelerate As Boolean = Me.IsAcceleration()

        If Not IsAccelerate And Val(Request.Item("DPID")) > 0 Then
            Dim cPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "AcceleratorID", MatchType.Exact, Val(Request.Item("DPID"))))
            Dim arl As ArrayList = New DailyPaymentFacade(User).Retrieve(cPP)
            If arl.Count > 0 Then
                IsAccelerate = True
            End If
        End If

        Dim sNormalTransfer() As String = {"HSBC", "SUMI"}
        Dim sAccGanti() As String = {"HSBC", "SUMI"}
        Dim sAccPercepatan() As String = {"HSBC", "SUMI"}
        Dim sAccTolakan() As String = {"HSBC", "SUMI", "BCA", "MNDIRI"}
        Dim IsDisplayed As Boolean = False

        Me.ddlBank.Items.Clear()
        Me.ddlBank.Items.Add(New ListItem("Silahkan Pilih", EnumGyroType.GyroType.Normal))
        For Each oB As Bank In aB
            If isAll Then
                IsDisplayed = True
            Else
                IsDisplayed = False
                If Not IsAccelerate Then
                    If (CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Gyro) Then
                        IsDisplayed = True 'All
                    ElseIf (CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Transfer) Then
                        If Me.IsExistInList(oB.BankCode, sNormalTransfer) Then
                            IsDisplayed = True
                        End If
                    End If
                Else
                    If CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.GantiGyro Then
                        If CType(Me.ddlEntryType.SelectedValue, Integer) = CType(EnumGyroEntryType.EntryType.Transfer, Integer) Then
                            If Me.IsExistInList(oB.BankCode, sAccGanti) Then IsDisplayed = True
                        Else
                            IsDisplayed = True 'If Me.IsExistInList(oB.BankCode, sAccGanti) Then IsDisplayed = True
                        End If
                    ElseIf CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.Percepatan Then
                        If Me.IsExistInList(oB.BankCode, sAccPercepatan) Then IsDisplayed = True
                    ElseIf CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.Tolakan Then
                        If Me.IsExistInList(oB.BankCode, sAccTolakan) Then IsDisplayed = True
                    ElseIf CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.Normal Then 'baru akan input percepatan
                        If (CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Gyro) Then
                            IsDisplayed = True 'All
                        ElseIf (CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Transfer) Then
                            If Me.IsAcceleration = False Then
                                If Me.IsExistInList(oB.BankCode, sNormalTransfer) Then
                                    IsDisplayed = True
                                End If
                            Else
                                IsDisplayed = True
                            End If
                        End If
                    End If
                End If

            End If
            If IsDisplayed Then Me.ddlBank.Items.Add(New ListItem(oB.BankName, oB.BankCode))
        Next
    End Sub

    Private Sub BindPaymentPurpose()
        Dim oPPFac As PaymentPurposeFacade = New PaymentPurposeFacade(User)
        Dim cPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentPurpose), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aPP As ArrayList = oPPFac.Retrieve(cPP)

        Me.ddlPaymentPurpose.Items.Clear()
        Me.ddlPaymentPurpose.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oPP As PaymentPurpose In aPP
            Me.ddlPaymentPurpose.Items.Add(New ListItem(oPP.PaymentPurposeCode, oPP.ID))
        Next
    End Sub

    Private Sub ClearData()
        Me.ddlEntryType.SelectedIndex = 0
        Me.ddlBank.SelectedIndex = 0
        Me.ddlPaymentPurpose.SelectedIndex = 0

        Me.calReqDate.Value = Now
        Me.txtGyroNumber.Text = ""
        Me.lblTotalGyro.Text = "0"
        Me.dtgMain.DataSource = New ArrayList
        Me.dtgMain.DataBind()
    End Sub

    Private Function IsDealerHarianTambahan() As Boolean
        Dim IsBulananTambahan As Boolean = False
        Dim oD As Dealer = Session("DEALER")
        Dim oTC1 As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.POBulanan)
        Dim oTC2 As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.POTambahan)
        If Not IsNothing(oTC1) AndAlso oTC1.ID > 0 AndAlso oTC1.Status = 1 AndAlso Not IsNothing(oTC2) AndAlso oTC2.ID > 0 AndAlso oTC2.Status = 1 Then IsBulananTambahan = True

        Return IsBulananTambahan

    End Function

    Private Function IsEntryGyroActive() As Boolean
        Dim oD As Dealer = Session("DEALER")
        Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.InputGyro)
        If Not IsNothing(oTC) AndAlso oTC.ID > 0 AndAlso oTC.Status = 1 Then Return True

        Return False
    End Function

    Private Sub BindAssignment(ByRef ddl As DropDownList, Optional ByVal ThisPOHeaderID As Integer = 0, Optional ByVal DPID As Integer = 0)
        Dim oD As Dealer = Session("DEALER")
        Dim oUI As UserInfo = _sessHelper.GetSession("LOGINUSERINFO")
        Dim oTC1 As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.POBulanan)
        Dim oTC2 As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.POTambahan)
        Dim oPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim cPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aPOH As New ArrayList
        Dim DateStart As Date = DateSerial(Now.Year, Now.Month - 11, Now.Day)
        Dim Sql As String = ""

        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        aPOH = Me._sessHelper.GetSession(Me._sesAssignment)
        If IsNothing(aPOH) OrElse aPOH.Count = 0 Then
            Sql = "(select uoa.OrganizationID from UserOrgAssignment uoa where uoa.UserID=" & oUI.ID & ")"
            Dim sStatus As String = "(" & CType(enumStatusPO.Status.Baru, Short).ToString
            sStatus &= "," & CType(enumStatusPO.Status.Konfirmasi, Short).ToString
            sStatus &= "," & CType(enumStatusPO.Status.Rilis, Short).ToString
            sStatus &= "," & CType(enumStatusPO.Status.Setuju, Short).ToString
            sStatus &= "," & CType(enumStatusPO.Status.Selesai, Short).ToString & ")"

            cPOH.opAnd(New Criteria(GetType(POHeader), "Dealer.ID", MatchType.Exact, oD.ID), "(", True)
            cPOH.opOr(New Criteria(GetType(POHeader), "Dealer.ID", MatchType.InSet, "(" & Sql & ")"), ")", False)
            If IsEntryGyroActive() = True Then
                cPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, sStatus))
            Else
                sStatus = "(" & CType(enumStatusPO.Status.Setuju, Short).ToString
                sStatus &= "," & CType(enumStatusPO.Status.Selesai, Short).ToString & ")"

                cPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, sStatus))
            End If
            cPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, DateStart.ToString("yyyy/MM/dd")))
            If CType(Me.ddlCategory.SelectedValue, Short) <> -1 Then
                cPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Category.CategoryCode", MatchType.Exact, Me.ddlCategory.SelectedItem.Text))
            End If

            If ddlPaymentPurpose.SelectedValue = 7 Then
                cPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.Exact, 1))
            Else
                cPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.Exact, 0))
            End If
            aPOH = oPOHFac.Retrieve(cPOH)
        End If
        Me._sessHelper.SetSession(Me._sesAssignment, aPOH)
        For Each oPOH As POHeader In aPOH
            If (oPOH.ID = ThisPOHeaderID OrElse Not Me.IsExistInGrid(oPOH.ID)) _
            AndAlso Me.IsValidPOBasedOnGyroDate(oPOH) _
            AndAlso (IsEditing() OrElse Me.IsValidPOBasedOnFullyPaid(oPOH, DPID)) Then
                Dim IsAllowToAdd As Boolean = False
                If Not IsEditing() AndAlso Me.IsValidPOBasedOnFullyPaid(oPOH, DPID) Then
                    IsAllowToAdd = True
                ElseIf IsEditing() Then
                    If Me.IsExistInGrid(oPOH.ID) Then
                        IsAllowToAdd = True
                    Else
                        If Me.IsValidPOBasedOnFullyPaid(oPOH, DPID) Then
                            IsAllowToAdd = True
                        End If
                    End If
                End If
                If IsAllowToAdd Then
                    ddl.Items.Add(New ListItem(oPOH.DealerPONumber & IIf(oPOH.IsFactoring = 1, " (F)", ""), oPOH.ID))
                End If
            End If
        Next
    End Sub

    Private Function IsValidPOBasedOnGyroDate(ByRef oPOH As POHeader) As Boolean
        If oPOH.ReqAllocationDateTime = Me.calReqDate.Value Then
            Return True
        End If
        Return False
    End Function

    Private Function IsValidPOBasedOnFullyPaid(ByRef oPOH As POHeader, Optional ByVal DPID As Integer = 0) As Boolean
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oVRPO As V_RekapPO = New V_RekapPOFacade(User).Retrieve(oPOH.ID)
        Dim aDP As New ArrayList
        Dim aggDP As Aggregate = New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
        Dim Total As Decimal = 0
        Dim TotalPOH As Decimal = oVRPO.TotalHarga + oVRPO.TotalHargaIT + oVRPO.TotalHargaPP

        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, oPOH.ID))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        If DPID <> 0 Then
            cDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.No, DPID))
        End If
        Total = oDPFac.GetAggregateResult(aggDP, cDP)

        If Total >= TotalPOH Then Return False
        If (TotalPOH - Total) <= _AllowedDifference Then Return False
        Return True
        'If Math.Abs(TotalPOH - Total) <= _AllowedDifference Then Return False
        'If Total > (TotalPOH + _AllowedDifference) Then Return False
        'Return True
    End Function

    Private Function IsExistInGrid(ByVal POHID As Integer) As Boolean
        Dim aGrid As ArrayList = Me._sessHelper.GetSession(Me._sesData)
        If IsNothing(aGrid) OrElse aGrid.Count < 1 Then Return False
        For Each oDP As DailyPayment In aGrid
            If POHID = oDP.POHeader.ID Then Return True
        Next
        Return False
    End Function

    Private Function IsNewGyroDateValid() As Boolean
        Dim aGrid As ArrayList = Me._sessHelper.GetSession(Me._sesData)

        For Each oDP As DailyPayment In aGrid
            If Not IsValidPOBasedOnGyroDate(oDP.POHeader) Then Return False
        Next
        Return True
    End Function

    Private Function ConvertToDailyPayment(ByVal DPID As Integer, ByVal POID As Integer, ByVal Amount As Decimal, ByVal Ref1 As String, ByVal Ref2 As String, ByVal Ref3 As String, ByVal Reason As String) As DailyPayment
        Dim oDP As New DailyPayment

        If DPID > 0 Then oDP = New DailyPaymentFacade(User).Retrieve(DPID)

        oDP.POHeader = New POHeaderFacade(User).Retrieve(POID)
        oDP.Amount = Amount
        oDP.EntryType = Me.ddlEntryType.SelectedValue

        If ddlEntryType.SelectedValue = CShort(EnumGyroEntryType.EntryType.Gyro).ToString Then
            oDP.SlipNumber = ddlBank.SelectedValue & " " & Me.txtGyroNumber.Text.Trim
        Else
            oDP.SlipNumber = Me.txtGyroNumber.Text.Trim
        End If

        Dim oDate As Date = New Date(1990, 1, 1)
        oDP.DocDate = oDate
        oDate = New Date(Me.calBaseline.Value.Year, Me.calBaseline.Value.Month, Me.calBaseline.Value.Day)
        oDP.BaselineDate = oDate
        Dim oPP As PaymentPurpose = Me.GetPaymentPurpose()
        oDP.PaymentPurpose = oPP
        oDP.Ref1 = Ref1
        oDP.Ref2 = Ref2
        oDP.Ref3 = Ref3
        oDP.Reason = Reason
        If oDP.ID < 1 Then oDP.Status = EnumPaymentStatus.PaymentStatus.Baru

        Return oDP
    End Function

    Private Function IsValidBaselineDate(ByVal arlData As ArrayList, Optional ByRef sError As String = "") As Boolean
        Dim IsValid As Boolean = True
        Dim dtTemp As Date = Now
        For Each oDP As DailyPayment In arlData
            dtTemp = oDP.POHeader.ReqAllocationDateTime
            If dtTemp <> Me.calReqDate.Value Then
                IsValid = False
                sError &= IIf(sError.Trim = "", "", ";") & "Tgl permintaan kirim untuk PO/SO " & oDP.POHeader.PONumber & "/" & oDP.POHeader.SONumber & " " & dtTemp.ToString("dd MMM yy")
            End If
        Next

        Return IsValid
    End Function

    Private Function calJatuhTempo_Changed()
        If Me.ddlGyroType.SelectedValue = EnumGyroType.GyroType.Normal Then
            Me.btnCheckGyroDate_Click(Nothing, Nothing)
        End If
        Me.ViewState.Item(Me._vstCalGyroValue) = Me.calReqDate.Value
    End Function


    Private Function calAccelerated_Changed()
        Me.BindData() 'update value of selisih & PPh
        Me.ViewState.Item(Me._vstCalAccValue) = Me.calAccelerated.Value
    End Function

    Private Function calBaseline_Changed()
        Me.BindData() 'update value of selisih & PPh
        Me.ViewState.Item(Me._vstCalBaselineValue) = Me.calBaseline.Value
    End Function

#Region "Table Merge Methods"

    Public Sub RenderHeader(ByVal output As HtmlTextWriter, ByVal container As Control)
        For i As Integer = 0 To container.Controls.Count - 1
            Dim cell As TableCell = CType(container.Controls(i), TableCell)
            If (Not info.MergedColumns.Contains(i)) Then
                cell.Attributes("rowspan") = "2"
                cell.RenderControl(output)
            Else
                If (info.StartColumns.Contains(i)) Then
                    output.Write(String.Format("<th align='center' Class='titleTableSales' colspan='{0}'>{1}</th>", info.StartColumns(i), info.Titles(i)))
                End If
            End If
        Next
        output.RenderEndTag()

        dtgMain.HeaderStyle.AddAttributesToRender(output)
        output.RenderBeginTag("tr")

        For i As Integer = 0 To info.MergedColumns.Count - 1
            Dim cell As TableCell = CType(container.Controls(info.MergedColumns(i)), TableCell)
            cell.RenderControl(output)

        Next

    End Sub

    Public ReadOnly Property info() As MergedColumnsInfo
        Get
            If (ViewState("info") Is Nothing) Then

                ViewState("info") = New MergedColumnsInfo
            End If
            Return CType(ViewState("info"), MergedColumnsInfo)
        End Get
    End Property

    <Serializable()> _
        Public Class MergedColumnsInfo
        Public MergedColumns As New ArrayList
        Public StartColumns As New Hashtable
        '            // key-value pairs: key = first column index, value = common title of merged columns 
        Public Titles As New Hashtable
        '            //parameters: merged columns's indexes, common title of merged columns 

        Public Sub AddMergedColumns(ByVal columnsIndexes As Integer(), ByVal title As String)

            If Not StartColumns.ContainsKey(columnsIndexes(0)) Then
                MergedColumns.AddRange(columnsIndexes)
                StartColumns.Add(columnsIndexes(0), columnsIndexes.Length)
                Titles.Add(columnsIndexes(0), title)
            End If


        End Sub

        Public Sub ClearMergedColumns()
            MergedColumns = New ArrayList
            StartColumns = New Hashtable
            Titles = New Hashtable
        End Sub
    End Class
#End Region

    Private Function IsValidBasedOnFactoringType(ByVal arlData As ArrayList, Optional ByRef sErrMessage As String = "") As Boolean
        If arlData.Count <= 1 Then Return True
        Dim IsFact As Integer = CType(arlData(0), DailyPayment).POHeader.IsFactoring
        For i As Integer = 1 To arlData.Count - 1
            If IsFact = 1 AndAlso CType(arlData(i), DailyPayment).POHeader.IsFactoring = IsFact Then
                sErrMessage = "Untuk PO Factoring hanya boleh satu assignment"
                Return False
            End If
            If CType(arlData(i), DailyPayment).POHeader.IsFactoring <> IsFact Then
                sErrMessage = "Assignment PO tidak boleh beda (factoring & non-factoring)"
                Return False
            End If
        Next
        Return True
    End Function

    Private Function IsAllowedTransferBank(ByVal sBankCode As String) As Boolean
        Dim AllowedBank(2) As String
        Dim i As Integer

        AllowedBank(0) = "BTMU"
        AllowedBank(1) = "SUMI"
        For i = 0 To 1
            If sBankCode.Trim.ToUpper = AllowedBank(i).Trim.ToUpper Then
                Return True
            End If
        Next
        Return False


    End Function

    Private Sub SetControl(ByVal TriggerType As String, Optional ByRef oDP As DailyPayment = Nothing)
        Select Case TriggerType.Trim.ToUpper
            Case "GyroType".ToUpper
                Select Case CType(Me.ddlGyroType.SelectedValue, Short)
                    Case EnumGyroType.GyroType.Normal
                        If Me.IsAcceleration() = False Then

                            Me.ddlEntryType.Enabled = True
                            Me.ddlBank.Enabled = True
                            'Me.txtGyroNumber.Enabled = True
                            Me.calReqDate.Enabled = True
                            Me.calBaseline.Enabled = True 'resolution
                            Me.ddlPaymentPurpose.Enabled = True
                            Me.lblRef.Visible = False
                            Me.lblColonRef.Visible = False
                            Me.lblRefSlipNumber.Visible = False
                            Me.lblAcc.Visible = False
                            Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                            Me.lblAcceleratedDate.Visible = False
                            Me.calAccelerated.Visible = False
                            'Me.btnUpdateDTG.Visible = Me.calAccelerated.Visible
                            dtgMain.Columns(6).Visible = False 'Selisih
                            dtgMain.Columns(7).Visible = False 'PPh
                            lblRef.Text = "Referensi"
                            lblGyroDate.Text = "Tanggal Gyro"
                            Me.ddlCategory.Visible = True
                            Me.lblKategori.Visible = Me.ddlCategory.Visible
                            Me.lblColonKategori.Visible = Me.ddlCategory.Visible
                        Else

                            Me.ddlEntryType.Enabled = True
                            Me.ddlBank.Enabled = True
                            'Me.txtGyroNumber.Enabled = True
                            Me.calReqDate.Enabled = True
                            Me.calBaseline.Enabled = True 'resolution
                            Me.ddlPaymentPurpose.Enabled = False
                            Me.lblRef.Visible = False
                            Me.lblColonRef.Visible = False
                            Me.lblRefSlipNumber.Visible = False
                            Me.lblAcc.Visible = False
                            Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                            Me.lblAcceleratedDate.Visible = False
                            Me.calAccelerated.Visible = False
                            'Me.btnUpdateDTG.Visible = Me.calAccelerated.Visible
                            Me.btnUpdateDTG.Visible = False
                            Me.lblAcc.Visible = False
                            Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                            dtgMain.Columns(6).Visible = False 'Selisih
                            dtgMain.Columns(7).Visible = False 'PPh
                            lblRef.Text = "Referensi"
                            lblGyroDate.Text = "Tanggal Gyro"
                            Me.ddlCategory.Visible = True
                            Me.lblKategori.Visible = Me.ddlCategory.Visible
                            Me.lblColonKategori.Visible = Me.ddlCategory.Visible
                        End If
                    Case EnumGyroType.GyroType.GantiGyro
                        Me.ddlEntryType.Enabled = True
                        Me.ddlBank.Enabled = True
                        'Me.txtGyroNumber.Enabled = True
                        Me.calReqDate.Enabled = False
                        Me.calBaseline.Enabled = False
                        Me.ddlPaymentPurpose.Enabled = False
                        Me.lblRef.Visible = True
                        Me.lblColonRef.Visible = True
                        Me.lblRefSlipNumber.Visible = True
                        Me.lblAcc.Visible = False
                        Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                        dtgMain.Columns(6).Visible = False 'Selisih
                        dtgMain.Columns(7).Visible = False 'PPh
                        Me.lblColonAccelerate.Visible = False
                        Me.lblAcceleratedDate.Visible = False
                        Me.calAccelerated.Visible = False
                        'Me.btnUpdateDTG.Visible = Me.calAccelerated.Visible
                        Me.btnUpdateDTG.Visible = False
                        Me.lblAcc.Visible = False
                        Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                        dtgMain.ShowFooter = False
                        lblRef.Text = "Ref. Gyro Awal"
                        lblGyroDate.Text = "Tanggal Jatuh Tempo"
                        Me.ddlCategory.Visible = False
                        Me.lblKategori.Visible = Me.ddlCategory.Visible
                        Me.lblColonKategori.Visible = Me.ddlCategory.Visible

                    Case EnumGyroType.GyroType.Percepatan
                        Me.ddlEntryType.Enabled = True
                        Me.ddlBank.Enabled = True
                        'Me.txtGyroNumber.Enabled = False
                        Me.calReqDate.Enabled = False 'True
                        Me.calBaseline.Enabled = False
                        Me.ddlPaymentPurpose.Enabled = False
                        Me.lblRef.Visible = True
                        Me.lblColonRef.Visible = True
                        Me.lblRefSlipNumber.Visible = True
                        Me.lblAcc.Visible = True
                        Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                        Me.lblAcceleratedDate.Visible = True
                        Me.calAccelerated.Visible = True
                        'Me.btnUpdateDTG.Visible = Me.calAccelerated.Visible
                        Me.btnUpdateDTG.Visible = True
                        If Me.IsAcceleration() = False Then 'means -> Editing Mode
                            Me.calAccelerated.Visible = False
                            'Me.btnUpdateDTG.Visible = Me.calAccelerated.Visible
                        End If
                        Me.lblAcc.Visible = Me.calAccelerated.Visible
                        Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                        dtgMain.ShowFooter = False
                        dtgMain.Columns(6).Visible = True 'Selisih
                        dtgMain.Columns(7).Visible = True 'PPh
                        lblRef.Text = "Ref. Gyro Awal"
                        lblGyroDate.Text = "Tanggal Jatuh Tempo"
                        Me.ddlCategory.Visible = False
                        Me.lblKategori.Visible = Me.ddlCategory.Visible
                        Me.lblColonKategori.Visible = Me.ddlCategory.Visible
                    Case EnumGyroType.GyroType.Tolakan
                        Me.ddlEntryType.Enabled = True
                        Me.ddlBank.Enabled = True
                        'Me.txtGyroNumber.Enabled = False
                        Me.calReqDate.Enabled = False 'True
                        Me.calBaseline.Enabled = True
                        Me.ddlPaymentPurpose.Enabled = False
                        Me.lblRef.Visible = True
                        Me.lblColonRef.Visible = True
                        Me.lblRefSlipNumber.Visible = True
                        Me.lblAcc.Visible = True
                        Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                        Me.lblAcceleratedDate.Visible = False
                        Me.calAccelerated.Visible = False
                        'Me.btnUpdateDTG.Visible = Me.calAccelerated.Visible
                        Me.btnUpdateDTG.Visible = True
                        Me.lblAcc.Visible = Me.calAccelerated.Visible
                        Me.lblColonAccelerate.Visible = Me.lblAcc.Visible
                        dtgMain.ShowFooter = False
                        dtgMain.Columns(6).Visible = True 'Selisih
                        dtgMain.Columns(7).Visible = True 'PPh
                        lblRef.Text = "Ref. Gyro Awal"
                        lblGyroDate.Text = "Tanggal Jatuh Tempo"
                        Me.ddlCategory.Visible = False
                        Me.lblKategori.Visible = Me.ddlCategory.Visible
                        Me.lblColonKategori.Visible = Me.ddlCategory.Visible
                End Select
            Case "UserLogin".ToUpper
                Dim oD As Dealer = CType(Session("DEALER"), Dealer)

                If oD.Title = EnumDealerTittle.DealerTittle.KTB Then
                    dtgMain.Columns(8).Visible = True 'Ref 1
                    dtgMain.Columns(9).Visible = True 'Ref 2
                    dtgMain.Columns(10).Visible = True 'Ref 3
                    dtgMain.Columns(11).Visible = True 'Reason'10
                    dtgMain.ShowFooter = False
                ElseIf oD.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    Dim IsAcc As Boolean = Me.IsAcceleration()
                    dtgMain.Columns(8).Visible = False 'Ref 1
                    dtgMain.Columns(9).Visible = False 'Ref 2
                    dtgMain.Columns(10).Visible = False 'Ref 3
                    dtgMain.Columns(11).Visible = False 'Reason
                    dtgMain.ShowFooter = True
                End If
            Case "GyroStatus".ToUpper
                Dim IsEnableEdit As Boolean = False
                'If Not IsNothing(oDP) AndAlso oDP.ID > 0 AndAlso Me.IsAcceleration() AndAlso oDP.Status = EnumPaymentStatus.PaymentStatus.Baru Then IsEnableEdit = True
                If Not IsNothing(oDP) AndAlso oDP.ID > 0 AndAlso (Not IsNothing(Request.Item("DPID") AndAlso CType(Request.Item("DPID"), Integer) > 0) OrElse Me.IsAcceleration()) AndAlso oDP.Status = EnumPaymentStatus.PaymentStatus.Baru Then IsEnableEdit = True
                dtgMain.Columns(dtgMain.Columns.Count - 1).Visible = IsEnableEdit
                dtgMain.Columns(dtgMain.Columns.Count - 2).Visible = IsEnableEdit
                btnSave.Enabled = IsEnableEdit
            Case Else
        End Select
    End Sub
    Private Function IsValidWithOldDP(ByVal NewDP As DailyPayment, Optional ByRef strMessage As String = "") As Boolean
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim aDP As New ArrayList
        Dim IsValid As Boolean = False
        Dim oDPOld As DailyPayment = oDPFac.Retrieve(CType(Request.Item("DPID"), Integer))

        aDP = oDPOld.OtherAssignments(True)

        For Each oDP As DailyPayment In aDP
            If NewDP.SlipNumber.Trim.ToUpper = oDP.SlipNumber.Trim.ToUpper AndAlso NewDP.PaymentPurpose.ID = oDP.PaymentPurpose.ID Then
                IsValid = True
                If CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.GantiGyro Then
                    If NewDP.Amount <> oDP.Amount Then
                        IsValid = False
                        strMessage = "Amount harus sama dengan amount sebelumnya. yaitu : " & oDP.Amount
                    End If
                ElseIf CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.Tolakan Then
                    If NewDP.Amount < oDP.Amount Then
                        IsValid = False
                        strMessage = "Amount tidak boleh kurang dari amount sebelumnya. yaitu : " & oDP.Amount
                    End If
                End If
                Exit For
            End If
        Next
        Return IsValid
    End Function

    Private Function GetDefaulAmount(ByVal oVRPO As V_RekapPO) As Decimal
        Dim Rsl As Decimal = 0
        Dim oPP As PaymentPurpose = Me.GetPaymentPurpose()
        Dim cDP As CriteriaComposite
        Dim aggDP As Aggregate
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim TotalPO As Decimal = oVRPO.TotalHarga + oVRPO.TotalHargaPP + oVRPO.TotalHargaIT
        Dim TotalPayment As Decimal = 0
        Dim aDP As ArrayList
        Dim IsNeverPaid As Boolean = True

        cDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.No, "1"))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, oVRPO.ID))
        'cDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.No, oDP.ID))
        aggDP = New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
        Try
            TotalPayment = oDPFac.GetAggregateResult(aggDP, cDP)
        Catch ex As Exception
            TotalPayment = 0
        End Try
        aDP = oDPFac.Retrieve(cDP)
        If Not IsNothing(oPP) AndAlso oPP.ID > 0 Then
            Select Case oPP.PaymentPurposeCode.Trim.ToUpper
                Case "IT"
                    Rsl = oVRPO.TotalHargaIT
                    For Each oDP As DailyPayment In aDP
                        If oDP.PaymentPurpose.PaymentPurposeCode.IndexOf("IT") >= 0 Then
                            Rsl -= oDP.Amount
                            IsNeverPaid = False
                        End If
                    Next
                Case "PP"
                    Rsl = oVRPO.TotalHargaPP
                    For Each oDP As DailyPayment In aDP
                        If oDP.PaymentPurpose.PaymentPurposeCode.IndexOf("PP") >= 0 Then
                            Rsl -= oDP.Amount
                            IsNeverPaid = False
                        End If
                    Next
                Case "VH"
                    Rsl = oVRPO.TotalHarga
                    For Each oDP As DailyPayment In aDP
                        If oDP.PaymentPurpose.PaymentPurposeCode.IndexOf("VH") >= 0 Then
                            Rsl -= oDP.Amount
                            IsNeverPaid = False
                        End If
                    Next
                Case "VH+PP"
                    Rsl = oVRPO.TotalHarga + oVRPO.TotalHargaPP
                    Dim arlTemp As New ArrayList
                    For Each oDP As DailyPayment In aDP
                        If oDP.PaymentPurpose.PaymentPurposeCode.IndexOf("VH") >= 0 Then
                            Rsl -= oDP.Amount
                            IsNeverPaid = False
                        Else
                            arlTemp.Add(odP)
                        End If
                    Next
                    For Each oDP As DailyPayment In arlTemp
                        If oDP.PaymentPurpose.PaymentPurposeCode.IndexOf("PP") >= 0 Then
                            Rsl -= oDP.Amount
                            IsNeverPaid = False
                        End If
                    Next
                Case "VH+PP+IT"
                    Rsl = oVRPO.TotalHarga + oVRPO.TotalHargaPP + oVRPO.TotalHargaIT
                    Dim arlTemp As New ArrayList
                    Dim arlTemp2 As New ArrayList
                    For Each oDP As DailyPayment In aDP
                        If oDP.PaymentPurpose.PaymentPurposeCode.IndexOf("VH") >= 0 Then
                            Rsl -= oDP.Amount
                            IsNeverPaid = False
                        Else
                            arlTemp.Add(odP)
                        End If
                    Next
                    For Each oDP As DailyPayment In arlTemp
                        If oDP.PaymentPurpose.PaymentPurposeCode.IndexOf("PP") >= 0 Then
                            Rsl -= oDP.Amount
                            IsNeverPaid = False
                        Else
                            arlTemp2.Add(oDP)
                        End If
                    Next
                    For Each oDP As DailyPayment In arlTemp2
                        If oDP.PaymentPurpose.PaymentPurposeCode.IndexOf("IT") >= 0 Then
                            Rsl -= oDP.Amount
                            IsNeverPaid = False
                        End If
                    Next
            End Select
        End If

        If (TotalPO - TotalPayment) > Rsl Then
            Rsl = Rsl
        Else
            If Math.Abs(Rsl - (TotalPO - TotalPayment)) <= Me._AllowedDifference Then
                Rsl = Rsl
            Else
                If IsNeverPaid Then
                    Rsl = Rsl 'TotalPO - TotalPayment
                Else
                    Rsl = TotalPO - TotalPayment
                End If
            End If
        End If

        If Rsl < 0 Then Rsl = 0
        Return Rsl
    End Function

    Private Sub SetInterestDiffOfAccelerate(ByVal oDP As DailyPayment, ByRef DiffInterest As Decimal, ByRef PPh As Decimal)
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        If Me.IsAcceleration() Then 'New
            If Not IsNothing(Request.Item("DPID")) AndAlso CType(Request.Item("DPID"), Integer) > 0 Then
                Dim oDPOld As DailyPayment

                oDPOld = New DailyPaymentFacade(User).Retrieve(CType(Request.Item("DPID"), Integer))
                oDPFac.SetInterestDiffOfAccelerate(oDPOld, DiffInterest, PPh, Me.ddlGyroType.SelectedValue, Me.calAccelerated.Value, Me.calBaseline.Value)
            End If
        Else
            If Not IsNothing(oDP) AndAlso oDP.ID > 0 AndAlso Not IsNothing(oDP.OldDailyPayment) AndAlso oDP.OldDailyPayment.ID > 0 Then
                oDPFac.SetInterestDiffOfAccelerate(oDP.OldDailyPayment, DiffInterest, PPh, oDP.GyroType, oDP.BaselineDate, Me.calBaseline.Value)
            End If
        End If
    End Sub

    Private Sub HandleNotPostBack()
        If Me.IsAcceleration Then
            lblTitle.Text = "Ganti Pembayaran"
        Else
            lblTitle.Text = "PURCHASE ORDER - Input Pembayaran"
        End If
        Initialization()
        BindData()
    End Sub

    Private Function IsPaymentOver(ByRef aDP As ArrayList, ByRef sError As String)
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim cDP As CriteriaComposite
        Dim aggDP As Aggregate
        Dim aDPTemp As New ArrayList

        For Each oDP As DailyPayment In aDP
            Dim TotalPO As Decimal = oDP.POHeader.TotalHarga + oDP.POHeader.TotalHargaPP + oDP.POHeader.TotalHargaIT
            Dim TotalPayment As Decimal = 0

            cDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.No, "1"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, oDP.POHeader.ID))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.No, oDP.ID))
            aggDP = New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
            Try
                TotalPayment = oDPFac.GetAggregateResult(aggDP, cDP)
            Catch ex As Exception
                TotalPayment = 0
            End Try
            TotalPayment += oDP.Amount

            If TotalPayment > TotalPO OrElse TotalPayment >= (TotalPO - _AllowedDifference) Then
                'End If
                'If TotalPayment > (TotalPO + _AllowedDifference) Then
                '    sError = "Total Pembayaran untuk PO " & oDP.POHeader.PONumber & " melebihi Total PO."
                '    Return True
                'ElseIf (TotalPayment >= TotalPO AndAlso TotalPayment <= (TotalPO + _AllowedDifference)) OrElse (TotalPayment >= (TotalPO - _AllowedDifference)) Then
                Dim nComponent As Integer = 0
                Dim sComps() As String = {"VH", "PP", "IT"}
                Dim nVH As Integer = 0, nPP As Integer = 0, nIT As Integer = 0
                Dim arlDP As ArrayList = oDPFac.Retrieve(cDP)
                Dim IsCompComplete As Boolean = False

                arlDP.Add(oDP)
                For Each oDPTemp As DailyPayment In arlDP
                    If oDPTemp.PaymentPurpose.PaymentPurposeCode = "VH" Then
                        nVH += 1
                    ElseIf oDPTemp.PaymentPurpose.PaymentPurposeCode = "PP" Then
                        nPP += 1
                    ElseIf oDPTemp.PaymentPurpose.PaymentPurposeCode = "IT" Then
                        nIT += 1
                    ElseIf oDPTemp.PaymentPurpose.PaymentPurposeCode = "VH+PP" Then
                        nVH += 1
                        nPP += 1
                    ElseIf oDPTemp.PaymentPurpose.PaymentPurposeCode = "VH+PP+IT" Then
                        nVH += 1
                        nPP += 1
                        nIT += 1
                    End If
                    If oDP.POHeader.TermOfPayment.PaymentType <> enumPaymentType.PaymentType.TOP Then nIT = 1
                    If oDP.POHeader.FreePPh22Indicator = 0 Then nPP = 1
                    If oDP.POHeader.ContractHeader.PKHeader.FreeIntIndicator = 0 Then nIT = 1 'not confirmed yet
                    If nVH > 0 AndAlso nPP > 0 AndAlso nIT > 0 Then
                        IsCompComplete = True
                        Exit For
                    End If
                Next
                Dim sUnComplete As String = ""
                If nVH = 0 Then sUnComplete = IIf(sUnComplete.Trim = "", "", ", ") & "VH"
                If nPP = 0 Then sUnComplete = IIf(sUnComplete.Trim = "", "", ", ") & "PP"
                If nIT = 0 Then sUnComplete = IIf(sUnComplete.Trim = "", "", ", ") & "IT"
                If Not IsCompComplete Then
                    sError = "Jumlah pembayaran lunas, tapi tidak semua komponen dibayar. (Komponen tidak dibayar : " & sUnComplete & ")"
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Function IsDuplicatePPExist(ByRef aDP As ArrayList, ByRef sError As String) As Boolean
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim cDP As CriteriaComposite
        Dim aDPTemp As New ArrayList
        Dim oPP As PaymentPurpose = Me.GetPaymentPurpose()
        If IsNothing(oPP) OrElse oPP.ID < 1 Then Return False
        Dim curPPs() As String = oPP.PaymentPurposeCode.Split("+")
        Dim i As Integer


        For Each oDP As DailyPayment In aDP
            cDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.No, "1"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, oDP.POHeader.ID))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.No, oDP.ID))

            aDPTemp = oDPFac.Retrieve(cDP)
            For Each oDPTemp As DailyPayment In aDPTemp
                Dim PPsTemp() As String = oDPTemp.PaymentPurpose.PaymentPurposeCode.Split("+")

                For i = 0 To curPPs.Length - 1
                    If Me.IsExistInList(curPPs(i), PPsTemp) Then
                        If curPPs.Length > 1 Then
                            sError = "Pembayaran " & curPPs(i) & " (" & oPP.PaymentPurposeCode & ") sudah pernah dilakukan (" & oDPTemp.PaymentPurpose.PaymentPurposeCode & ")"
                        Else
                            sError = "Pembayaran " & oPP.PaymentPurposeCode & " sudah pernah dilakukan (" & oDPTemp.PaymentPurpose.PaymentPurposeCode & ")"
                        End If
                        Return True
                    End If
                Next
            Next
        Next

        Return False
    End Function

    Private Function GetPaymentPurpose() As PaymentPurpose
        Dim oPP As PaymentPurpose = Nothing
        If ddlPaymentPurpose.SelectedValue > 0 Then
            oPP = New PaymentPurposeFacade(User).Retrieve(CType(Me.ddlPaymentPurpose.SelectedValue, Integer))
        End If
        Return oPP

    End Function

    Private Sub CheckPrivilege()
        Me.btnSave.Enabled = False
        If Not IsNothing(Request.Item("DPID")) AndAlso CType(Request.Item("DPID"), Integer) > 0 AndAlso CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
            Me.btnSave.Enabled = True
            Exit Sub
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.sales_gyroinput_buat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Input Gyro")
        Else
            Me.btnSave.Enabled = True
        End If
    End Sub

    Private Function IsValidAmount(ByRef txtAmount As TextBox) As Boolean
        Dim Amount As Decimal
        Dim Rsl As Boolean = True

        Try
            Amount = CType(txtAmount.Text.Trim, Decimal)
        Catch ex As Exception
            Amount = 0
        End Try
        If Amount <= 0 Then
            MessageBox.Show("Amount harus lebih besar dari 0")
            Rsl = False
        End If
        Return Rsl
    End Function

    Private Sub SetGyroNumberControl()

        If CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Transfer Then
            txtGyroNumber.Enabled = False
        Else
            txtGyroNumber.Enabled = True
        End If
        If Me.IsAcceleration = True Then
            If CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.Percepatan OrElse CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.Tolakan Then
                txtGyroNumber.Enabled = False
            Else
                If CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Transfer Then
                    txtGyroNumber.Enabled = False
                Else
                    txtGyroNumber.Enabled = True
                End If
                'txtGyroNumber.Enabled = True
            End If
        End If

        If CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Transfer Then
            lblBank.Text = "Bank Tujuan Transfer"
            Dim strBankcode As String = Me.ddlBank.SelectedValue
            If strBankcode = "-1" Then
                strBankcode = ""
            End If
            Me.txtGyroNumber.Text = "TRF " & strBankcode
            'txtGyroNumber.Enabled = False
        Else
            lblBank.Text = "Nama Bank Gyro"
            Me.txtGyroNumber.Text = ""
            'txtGyroNumber.Enabled = True
        End If
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 300
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckPrivilege()
        If Not IsNothing(viewstate.Item(Me._vstCalGyroValue)) Then
            If CType(viewstate.Item(Me._vstCalGyroValue), Date).Day <> Me.calReqDate.Value.Day AndAlso _
            CType(viewstate.Item(Me._vstCalGyroValue), Date).Month <> Me.calReqDate.Value.Month AndAlso _
            CType(viewstate.Item(Me._vstCalGyroValue), Date).Year <> Me.calReqDate.Value.Year Then
                calJatuhTempo_Changed()
            End If
        End If
        If Not IsNothing(viewstate.Item(Me._vstCalAccValue)) Then
            If CType(viewstate.Item(Me._vstCalAccValue), Date).Day <> Me.calAccelerated.Value.Day _
            AndAlso CType(viewstate.Item(Me._vstCalAccValue), Date).Month <> Me.calAccelerated.Value.Month _
            AndAlso CType(viewstate.Item(Me._vstCalAccValue), Date).Year <> Me.calAccelerated.Value.Year Then
                Me.calAccelerated_Changed()
            End If
        End If
        If Not IsNothing(viewstate.Item(Me._vstCalBaselineValue)) Then
            If CType(viewstate.Item(Me._vstCalBaselineValue), Date).Day <> Me.calBaseline.Value.Day _
            AndAlso CType(viewstate.Item(Me._vstCalBaselineValue), Date).Month <> Me.calBaseline.Value.Month _
            AndAlso CType(viewstate.Item(Me._vstCalBaselineValue), Date).Year <> Me.calBaseline.Value.Year Then
                Me.calBaseline_Changed()
            End If
        End If
        If Not IsPostBack Then
            Me.HandleNotPostBack()
        End If
        '

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Me.IsAcceleration() And ddlGyroType.SelectedValue = CShort(EnumGyroType.GyroType.Normal).ToString Then
            MessageBox.Show("Silahkan pilih tujuan entry")
            Exit Sub
        End If

        Dim sSlipNumber As String = ""

        If ddlBank.SelectedIndex = 0 Then
            MessageBox.Show("Silahkan pilih nama bank")
            Exit Sub
        End If
        If Me.txtGyroNumber.Text.Trim = "" Then
            MessageBox.Show("Nomer Gyro masih kosong")
            Exit Sub
        Else
            sSlipNumber = Me.txtGyroNumber.Text.Trim
            If sSlipNumber.Length > 16 Then
                MessageBox.Show("No Giro maksimal " & (16 - 1 - sSlipNumber.Length) & " karakter")
                Exit Sub
            End If
        End If
        If Me.ddlPaymentPurpose.SelectedIndex = 0 Then
            MessageBox.Show("Silahkan pilih tujuan pembayaran")
            Exit Sub
        End If

        Dim IsInserting As Boolean = False
        Dim aDP As ArrayList = Me._sessHelper.GetSession(Me._sesData)
        Dim sError As String = ""
        If Not IsValidBaselineDate(aDP, sError) Then
            MessageBox.Show("Simpan Gagal. " & sError)
            Exit Sub
        End If
        'Prevent duplicate payment purpose for each item 
        sError = ""
        If Me.IsAcceleration() = False AndAlso IsPaymentOver(aDP, sError) Then ' AndAlso Me.IsDuplicatePPExist(aDP, sError) Then
            MessageBox.Show(sError)
            Exit Sub
        End If

        Dim objBank As Bank = New BankFacade(User).Retrieve(ddlBank.SelectedValue)

        Dim aDPTemp As New ArrayList
        Dim oPP As PaymentPurpose = Me.GetPaymentPurpose()
        Dim oDate As Date = New Date(Me.calBaseline.Value.Year, Me.calBaseline.Value.Month, Me.calBaseline.Value.Day)

        For Each oDP As DailyPayment In aDP
            odp.PaymentPurpose = oPP
            If ddlEntryType.SelectedValue = CShort(EnumGyroEntryType.EntryType.Gyro).ToString Then
                oDP.SlipNumber = ddlBank.SelectedValue & " " & Me.txtGyroNumber.Text.Trim
            Else
                oDP.SlipNumber = Me.txtGyroNumber.Text.Trim
            End If
            odp.GyroType = Me.ddlGyroType.SelectedValue
            odp.EntryType = Me.ddlEntryType.SelectedValue
            odp.BaselineDate = oDate
            odp.EffectiveDate = odp.POHeader.EffectiveDate
            aDPTemp.Add(odp)
        Next
        aDP = aDPTemp
        If Not IsNothing(aDP) AndAlso aDP.Count > 0 Then
            Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
            Dim nStatus As Integer = -1
            Dim nError As Integer = 0
            Dim ThisDPID As Integer = 0
            Dim arlDPToInsert As New ArrayList
            Dim arlUpdatedDPBySuccessfullInsert As New ArrayList
            Dim IsInsertWithTrans As Boolean = False


            For Each oDP As DailyPayment In aDP
                oDPFac = New DailyPaymentFacade(User)
                If Me.IsAcceleration() Then
                    Dim oDPold As DailyPayment = oDPFac.Retrieve(odp.ID)
                    oDP.DocNumber = ""
                    oDp.FiscalYear = 0
                    oDp.ReceiptNumber = ""
                    oDp.SalesOrder = Nothing
                    oDP.DocDate = New Date(1990, 1, 1)
                    oDP.EffectiveDate = oDPold.POHeader.EffectiveDate '  New Date(1753, 1, 1)
                    oDP.SAPCreator = ""
                    oDP.PIC = ""
                    odP.Status = EnumPaymentStatus.PaymentStatus.Baru
                    oDP.BaselineDate = calAccelerated.Value
                    odp.AcceleratedDate = New Date(1753, 1, 1)
                    oDP.GyroType = ddlGyroType.SelectedValue
                    odp.BankID = objBank.ID
                    If odp.GyroType = EnumGyroType.GyroType.GantiGyro Then
                        odp.Reason = "RPLC GIRO (" & oDPold.SlipNumber & ") " & oDPold.BaselineDate.ToString("dd-MM-yyyy")
                    ElseIf odp.GyroType = EnumGyroType.GyroType.Percepatan Then
                        odp.Reason = "RPLC PCPT (" & oDPold.SlipNumber & ") " & oDPold.BaselineDate.ToString("dd-MM-yyyy")
                        odp.EffectiveDate = Me.calAccelerated.Value
                    ElseIf odp.GyroType = EnumGyroType.GyroType.Tolakan Then
                        odp.Reason = "RPLC TLKN (" & oDPold.SlipNumber & ") " & oDPold.BaselineDate.ToString("dd-MM-yyyy")
                    Else
                        odp.Reason = ""
                    End If
                    'oDPFac.Insert(oDP)
                    odp.NumOfTransfered = 0
                    arlDPToInsert.Add(odp)
                    IsInsertWithTrans = True
                    nStatus = 0

                    'Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DailyPayment), "ID", AggregateType.Max)
                    'Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "POHeader.Dealer.ID", MatchType.Exact, odp.POHeader.Dealer.ID))
                    'Dim MaxID As Integer = oDPFac.GetAggregateResult(agg, cDP)
                    'oDPold.AcceleratorID = MaxID
                    oDPold.AcceleratedGyro = 1
                    oDPold.AcceleratedDate = calAccelerated.Value

                    'oDPFac.Update(oDPold)
                    arlUpdatedDPBySuccessfullInsert.Add(oDPold)

                Else
                    If Val(Request.Item("DPID")) <> 0 And Me.IsAcceleration = False Then
                        odp.BankID = objBank.ID
                        oDPFac.Update(oDP)
                        nStatus = 0
                    Else
                        If odp.POHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
                            odp.Ref1 = "COD"
                        ElseIf odp.POHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
                            odp.Ref1 = "TR"
                        End If
                        odp.BankID = objBank.ID
                        'nStatus = oDPFac.insertDP(oDP)
                        odp.NumOfTransfered = 0
                        arlDPToInsert.Add(odp)
                        IsInsertWithTrans = True
                        IsInserting = True
                    End If

                End If
                'If nStatus = -1 Then
                '    nError += 1
                'End If
            Next
            'bind object
            Dim oDPHFac As DailyPaymentHeaderFacade = New DailyPaymentHeaderFacade(User)
            Dim oDPH As New DailyPaymentHeader
            For Each oDP As DailyPayment In arlDPToInsert
                oDPH.DailyPayments.Add(oDP)
            Next
            nError = 0
            Dim InsertedID As Integer = oDPHFac.Insert(oDPH)
            If InsertedID > 0 Then
                For Each oDP As DailyPayment In arlUpdatedDPBySuccessfullInsert
                    Dim NewlyInsertedID As Integer = 0
                    For Each oDPInserted As DailyPayment In oDPH.DailyPayments
                        If oDPInserted.POHeader.ID = odp.POHeader.ID Then
                            NewlyInsertedID = oDPInserted.ID
                            Exit For
                        End If
                    Next
                    odp.AcceleratorID = NewlyInsertedID
                    oDPFac.Update(odp)
                Next
                oDPH = oDPHFac.Retrieve(InsertedID)
                Me.lblRegNumber.Text = oDPH.RegNumber
            Else
                nError = 1
            End If

            If Me.IsAcceleration() Then
                btnSave.Enabled = False
                Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = False 'Save|Add
                Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 2).Visible = False 'Edit
                Me.dtgMain.DataBind()
            Else
                Me.ViewState.Item(Me._vstMode) = Me._vstEdit
                Dim strSlipNumber As String = ""
                If CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Transfer Then
                    strSlipNumber = txtGyroNumber.Text.Trim
                Else
                    strSlipNumber = ddlBank.SelectedValue & " " & txtGyroNumber.Text.Trim
                End If
                Me._sessHelper.SetSession(Me._sesData, GetDataFromDB(ddlEntryType.SelectedValue, strSlipNumber, calBaseline.Value, ddlPaymentPurpose.SelectedValue, Val(Request.Item("DPID"))))
                Me.BindData()
            End If

            If nError > 0 Then
                MessageBox.Show(nError.ToString & " data assignment gagal disimpan")
            Else
                Me.btnBack.Text = "autoback" 'handled by javascript
                'MessageBox.Show(SR.SaveSuccess)
                'Me.HandleNotPostBack()
            End If
        Else
            MessageBox.Show("Simpan Gagal, Tidak ada Assignment")
            Exit Sub
        End If

    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        Dim oD As Dealer = Session.Item("DEALER")
        If e.Item.ItemType = ListItemType.Header Then
            info.ClearMergedColumns()
            'info.AddMergedColumns(New Integer() {1, 2, 3}, "Harga Tebus (Rp)")
            info.AddMergedColumns(New Integer() {2, 3, 4}, "Harga Tebus (Rp)")

        ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oDP As DailyPayment = CType(Me._sessHelper.GetSession(Me._sesData), ArrayList)(e.Item.ItemIndex)
            Dim lblAssignment As Label = e.Item.FindControl("lblAssignment")
            Dim lblPO As Label = e.Item.FindControl("lblPO")
            Dim lblVH As Label = e.Item.FindControl("lblVH")
            Dim lblPP As Label = e.Item.FindControl("lblPP")
            Dim lblIT As Label = e.Item.FindControl("lblIT")
            Dim lblAmount As Label = e.Item.FindControl("lblAmount")
            Dim lblDiffInt As Label = e.Item.FindControl("lblDiffInt")
            Dim lblPPh As Label = e.Item.FindControl("lblPPh")
            Dim lblRef1 As Label = e.Item.FindControl("lblRef1")
            Dim lblRef2 As Label = e.Item.FindControl("lblRef2")
            Dim lblRef3 As Label = e.Item.FindControl("lblRef3")
            Dim lblReason As Label = e.Item.FindControl("lblReason")
            Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
            Dim DiffInt As Decimal = 0
            Dim PPh As Decimal = 0

            If Val(Request.Item("DPID")) = 0 Then
                lbtnDelete.Visible = True
            Else
                lbtnDelete.Visible = False
            End If

            lblAssignment.Text = odp.POHeader.DealerPONumber & IIf(odp.POHeader.IsFactoring = 1, " (F)", "")
            If oDP.POHeader.Status <> enumStatusPO.Status.Selesai Then ' Not IsDealerHarianTambahan() Then ' oDP.POHeader.Status = enumStatusPO.Status.Baru Then
                'lblAssignment.Text = oDP.POHeader.PONumber
                lblPO.Text = odp.POHeader.PONumber
            Else
                'lblAssignment.Text = oDP.POHeader.SONumber
                lblPO.Text = IIf(odp.POHeader.SONumber.Trim <> "", odp.POHeader.SONumber, odp.POHeader.PONumber)
            End If
            lblVH.Text = FormatNumber(oDP.POHeader.V_RekapPO.TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblPP.Text = FormatNumber(oDP.POHeader.V_RekapPO.TotalHargaPP, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblIT.Text = FormatNumber(oDP.POHeader.V_RekapPO.TotalHargaIT, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblAmount.Text = FormatNumber(oDP.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.SetInterestDiffOfAccelerate(odp, DiffInt, PPh)
            lblDiffInt.Text = FormatNumber(DiffInt, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblPPh.Text = FormatNumber(PPh, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblRef1.Text = odp.Ref1
            lblRef2.Text = odp.Ref2
            lblRef3.Text = odp.Ref3
            lblReason.Text = odp.Reason

            Me.ViewState.Item("TotalAmount") = CType(Me.ViewState.Item("TotalAmount"), Decimal) + odp.Amount


        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim oDP As DailyPayment = CType(Me._sessHelper.GetSession(Me._sesData), ArrayList)(e.Item.ItemIndex)
            Dim ddlAssignmentE As DropDownList = e.Item.FindControl("ddlAssignmentE")
            Dim lblPOE As Label = e.Item.FindControl("lblPOE")
            Dim lblVHE As Label = e.Item.FindControl("lblVHE")
            Dim lblPPE As Label = e.Item.FindControl("lblPPE")
            Dim lblITE As Label = e.Item.FindControl("lblITE")
            Dim txtAmountE As TextBox = e.Item.FindControl("txtAmountE")
            Dim txtRef1E As TextBox = e.Item.FindControl("txtRef1E")
            Dim txtRef2E As TextBox = e.Item.FindControl("txtRef2E")
            Dim txtRef3E As TextBox = e.Item.FindControl("txtRef3E")
            Dim txtReasonE As TextBox = e.Item.FindControl("txtReasonE")
            Dim lblDiffIntE As Label = e.Item.FindControl("lblDiffIntE")
            Dim lblPPhE As Label = e.Item.FindControl("lblPPhE")
            Dim DiffIntE As Decimal = 0
            Dim PPhE As Decimal = 0

            If oD.Title = EnumDealerTittle.DealerTittle.DEALER Then
                BindAssignment(ddlAssignmentE, odp.POHeader.ID, odp.ID)
            Else
                ddlAssignmentE.Items.Clear()
                If odp.POHeader.Status <> enumStatusPO.Status.Selesai Then ' Not IsDealerHarianTambahan() Then ' oDP.POHeader.Status = enumStatusPO.Status.Baru Then
                    'ddlAssignmentE.Items.Add(New ListItem(oDP.POHeader.PONumber, oDP.POHeader.ID))
                    ddlAssignmentE.Items.Add(New ListItem(oDP.POHeader.DealerPONumber, oDP.POHeader.ID))
                    lblPOE.Text = odp.POHeader.PONumber
                Else
                    'ddlAssignmentE.Items.Add(New ListItem(oDP.POHeader.SONumber, oDP.POHeader.ID))
                    ddlAssignmentE.Items.Add(New ListItem(oDP.POHeader.DealerPONumber, oDP.POHeader.ID))
                    lblPOE.Text = IIf(odp.POHeader.SONumber.Trim = "", odp.POHeader.SONumber, odp.POHeader.PONumber)
                End If
            End If

            'Dim oTC1 As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.POBulanan)
            'Dim oTC2 As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oD.ID, EnumDealerTransType.DealerTransKind.POTambahan)
            'Dim IsBulananTambahan As Boolean
            'If Not IsNothing(oTC1) AndAlso oTC1.ID > 0 AndAlso oTC1.Status = 1 AndAlso Not IsNothing(oTC2) AndAlso oTC2.ID > 0 AndAlso oTC2.Status = 1 Then IsBulananTambahan = True

            'If IsBulananTambahan Then
            'ddlAssignmentE.SelectedValue = oDP.POHeader.SONumber
            'Else
            ddlAssignmentE.SelectedValue = oDP.POHeader.ID
            'End If
            lblVHE.Text = FormatNumber(oDP.POHeader.V_RekapPO.TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblPPE.Text = FormatNumber(oDP.POHeader.V_RekapPO.TotalHargaPP, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblITE.Text = FormatNumber(oDP.POHeader.V_RekapPO.TotalHargaIT, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            txtRef1E.Text = oDP.Ref1
            txtRef2E.Text = oDP.Ref2
            txtRef3E.Text = oDP.Ref3
            txtReasonE.Text = oDP.Reason

            Dim IsAccelerate As Boolean = Me.IsAcceleration()

            If IsAccelerate Then
                ddlAssignmentE.Enabled = False
            Else
                ddlAssignmentE.Enabled = (oD.Title = EnumDealerTittle.DealerTittle.DEALER)
            End If
            txtAmountE.Enabled = (oD.Title = EnumDealerTittle.DealerTittle.DEALER)

            txtAmountE.Text = FormatNumber(oDP.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.SetInterestDiffOfAccelerate(odp, DiffIntE, PPhE)
            lblDiffIntE.Text = FormatNumber(DiffIntE, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblPPhE.Text = FormatNumber(PPhE, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            If oD.Title <> EnumDealerTittle.DealerTittle.DEALER Then Exit Sub
            Dim ddlAssignmentF As DropDownList = e.Item.FindControl("ddlAssignmentF")
            Dim lblPOF As Label = e.Item.FindControl("lblPOF")
            Dim lblVHF As Label = e.Item.FindControl("lblVHF")
            Dim lblPPF As Label = e.Item.FindControl("lblPPF")
            Dim lblITF As Label = e.Item.FindControl("lblITF")
            Dim txtAmountF As TextBox = e.Item.FindControl("txtAmountF")
            Dim lblDiffIntF As Label = e.Item.FindControl("lblDiffIntF")
            Dim lblPPhF As Label = e.Item.FindControl("lblPPhF")
            Dim DiffIntF As Decimal = 0
            Dim PPhF As Decimal = 0
            Dim DefaultPOID As Integer = 0
            Dim oVRPO As V_RekapPO = Nothing

            If ddlAssignmentF.Items.Count > 0 AndAlso CType(ddlAssignmentF.SelectedValue, Integer) <> -1 Then
                DefaultPOID = CType(ddlAssignmentF.SelectedValue, Integer)
            End If
            BindAssignment(ddlAssignmentF, DefaultPOID)
            oVRPO = New V_RekapPOFacade(User).Retrieve(CType(ddlAssignmentF.SelectedValue, Integer))
            If Not IsNothing(oVRPO) AndAlso oVRPO.ID > 0 Then
                If Not IsDealerHarianTambahan() Then ' oVRPO.POHeader.Status = enumStatusPO.Status.Baru Then
                    lblPOF.Text = oVRPO.POHeader.PONumber
                Else
                    lblPOF.Text = IIf(oVRPO.POHeader.SONumber.Trim <> "", oVRPO.POHeader.SONumber, oVRPO.POHeader.PONumber)
                End If
                lblVHF.Text = FormatNumber(oVRPO.TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPF.Text = FormatNumber(oVRPO.TotalHargaPP, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblITF.Text = FormatNumber(oVRPO.TotalHargaIT, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                txtAmountF.Text = FormatNumber(Me.GetDefaulAmount(oVRPO), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.SetInterestDiffOfAccelerate(Nothing, DiffIntF, PPhF)
                lblDiffIntF.Text = FormatNumber(DiffIntF, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPhF.Text = FormatNumber(PPhF, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Else
                lblPOF.Text = ""
                lblVHF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblITF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                txtAmountF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblDiffIntF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPhF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
            Me.lblTotalGyro.Text = FormatNumber(CType(Me.ViewState.Item("TotalAmount"), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub btnCheckGyroDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckGyroDate.Click
        btnSave.Enabled = IsNewGyroDateValid()
        If btnSave.Enabled Then
            Dim oPP As PaymentPurpose = Me.GetPaymentPurpose()
            If Not IsNothing(oPP) AndAlso oPP.ID > 0 Then
                Dim GyroDate As Date = Me.calReqDate.Value

                If oPP.PaymentPurposeCode.Trim.ToUpper = "PP" Then
                    GyroDate = New Date(Me.calReqDate.Value.Year, Me.calReqDate.Value.Month, 1).AddMonths(1).AddDays(-1)   'end of month
                    Me.calBaseline.Value = GyroDate
                    Me.calBaseline.Enabled = False
                ElseIf oPP.PaymentPurposeCode.Trim.ToUpper = "IT" Then
                    GyroDate = Me.calReqDate.Value
                    Me.calBaseline.Value = GyroDate
                    Me.calBaseline.Enabled = False
                Else 'VH
                    Me.calBaseline.Enabled = True
                End If
            End If
            'If Me.calBaseline.Enabled Then Me.calBaseline.Value = Me.calJatuhTempo.Value
            BindData() 'rebind to ddlAssignment
        End If
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If e.CommandName = "Add" Then
            Dim ddlAssignmentF As DropDownList = e.Item.FindControl("ddlAssignmentF")
            Dim txtAmountF As TextBox = e.Item.FindControl("txtAmountF")
            If Not IsValidAmount(txtamountf) Then Exit Sub
            If CType(ddlAssignmentF.SelectedValue, Integer) > 0 Then
                Dim oDP As DailyPayment = ConvertToDailyPayment(0, ddlAssignmentF.SelectedValue, txtAmountF.Text, "", "", "", "")
                Dim aTemp As ArrayList = Me._sessHelper.GetSession(Me._sesData)
                Dim sErrMessage As String = ""
                Dim IsNewInput As Boolean = False

                If aTemp.Count = 0 Then     'Input baru
                    'ddlPaymentPurpose_SelectedIndexChanged(Nothing, Nothing)
                    IsNewInput = True
                    'If ddlPaymentPurpose.SelectedValue <> 2 Then
                    '    Me.calBaseline.Value = DateAdd(DateInterval.Day, oDP.POHeader.TermOfPayment.TermOfPaymentValue, Me.calReqDate.Value)
                    'End If
                Else                        'Tambahan
                    Dim dtBaseline As DateTime = DateAdd(DateInterval.Day, oDP.POHeader.TermOfPayment.TermOfPaymentValue, Me.calReqDate.Value)
                    Dim oDPFirst As DailyPayment = CType(atemp(0), DailyPayment)
                    Dim dtBaselinePertama As DateTime = DateAdd(DateInterval.Day, oDPFirst.POHeader.TermOfPayment.TermOfPaymentValue, oDPFirst.POHeader.ReqAllocationDateTime)
                    If dtBaseline <> dtBaselinePertama Then ' If Me.calBaseline.Value <> dtBaseline Then
                        MessageBox.Show("Input Gagal.\n Tanggal jatuh tempo PO yang anda masukkan adalah " & dtBaseline.ToString("dd/MM/yyyy") & ".\n Tanggal jatuh tempo dalam 1 giro harus seragam")
                        Exit Sub
                    End If
                End If

                If Me.IsAcceleration() Then
                    'check amount with old gyro's assignment
                    If Not Me.IsValidWithOldDP(oDP, sErrMessage) Then
                        MessageBox.Show(sErrMessage)
                        Exit Sub
                    End If
                End If

                oDP.Status = EnumPaymentStatus.PaymentStatus.Baru
                Dim oDPTemp As DailyPayment = oDP
                Dim OriAmount As Decimal = oDP.Amount
                aTemp.Add(oDP)
                If IsNewInput Then
                    If Me.calBaseline.Enabled = False Then
                        Me.ddlPaymentPurpose_SelectedIndexChanged(Nothing, Nothing)
                    End If
                    'Dim OriBaselineDate As Date = Me.calBaseline.Value
                    'Me.calBaseline.Value = OriBaselineDate
                End If
                oDPTemp.Amount = OriAmount
                oDP.Amount = OriAmount
                If Not Me.IsValidBasedOnFactoringType(aTemp, sErrMessage) Then
                    aTemp.RemoveAt(aTemp.Count - 1)
                    MessageBox.Show(sErrMessage)
                    Exit Sub
                End If
                If oDP.POHeader.IsFactoring = 1 Then
                    dtgMain.ShowFooter = False
                Else
                    dtgMain.ShowFooter = True
                End If
                Me._sessHelper.SetSession(Me._sesData, aTemp)
                Me.BindData()
            Else
                MessageBox.Show("Silahkan pilih Nomer PO terlebih dahulu")
                Exit Sub
            End If

        ElseIf e.CommandName = "AssignmentChangeE" Then
            Dim ddlAssignmentE As DropDownList = e.Item.FindControl("ddlAssignmentE")
            Dim lblPOE As Label = e.Item.FindControl("lblPOE")
            Dim lblVHE As Label = e.Item.FindControl("lblVHE")
            Dim lblPPE As Label = e.Item.FindControl("lblPPE")
            Dim lblITE As Label = e.Item.FindControl("lblITE")
            Dim oVRPO As V_RekapPO = New V_RekapPOFacade(User).Retrieve(CType(ddlAssignmentE.SelectedValue, Integer))
            Dim txtAmountE As TextBox = e.Item.FindControl("txtAmountE")
            Dim lblDiffIntE As Label = e.Item.FindControl("lblDiffIntE")
            Dim lblPPhE As Label = e.Item.FindControl("lblPPhE")
            Dim DiffIntE As Decimal = 0
            Dim PPhE As Decimal = 0

            If Not IsNothing(oVRPO) AndAlso oVRPO.ID > 0 Then
                If Not IsDealerHarianTambahan() Then ' ovrpo.POHeader.Status = enumStatusPO.Status.Baru Then
                    lblPOE.Text = ovrpo.POHeader.PONumber
                Else
                    lblPOE.Text = IIf(ovrpo.POHeader.SONumber.Trim <> "", ovrpo.POHeader.SONumber, ovrpo.POHeader.PONumber)
                End If
                lblVHE.Text = FormatNumber(oVRPO.TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPE.Text = FormatNumber(oVRPO.TotalHargaPP, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblITE.Text = FormatNumber(oVRPO.TotalHargaIT, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                txtAmountE.Text = FormatNumber(Me.GetDefaulAmount(oVRPO), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.SetInterestDiffOfAccelerate(Nothing, DiffIntE, PPhE)
                lblDiffIntE.Text = FormatNumber(DiffIntE, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPhE.Text = FormatNumber(PPhE, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Else
                lblPOE.Text = ""
                lblVHE.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPE.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblITE.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                txtAmountE.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblDiffIntE.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPhE.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
        ElseIf e.CommandName = "AssignmentChangeF" Then
            Dim ddlAssignmentF As DropDownList = e.Item.FindControl("ddlAssignmentF")
            Dim lblPOF As Label = e.Item.FindControl("lblPOF")
            Dim lblVHF As Label = e.Item.FindControl("lblVHF")
            Dim lblPPF As Label = e.Item.FindControl("lblPPF")
            Dim lblITF As Label = e.Item.FindControl("lblITF")
            Dim oVRPO As V_RekapPO = New V_RekapPOFacade(User).Retrieve(CType(ddlassignmentf.SelectedValue, Integer))
            Dim txtAmountF As TextBox = e.Item.FindControl("txtAmountF")
            Dim lblDiffIntF As Label = e.Item.FindControl("lblDiffIntF")
            Dim lblPPhF As Label = e.Item.FindControl("lblPPhF")
            Dim DiffIntF As Decimal = 0
            Dim PPhF As Decimal = 0

            If Not IsNothing(oVRPO) AndAlso oVRPO.ID > 0 Then
                If Not IsDealerHarianTambahan() Then ' ovrpo.POHeader.Status = enumStatusPO.Status.Baru Then
                    lblPOF.Text = ovrpo.POHeader.PONumber
                Else
                    lblPOF.Text = IIf(ovrpo.POHeader.SONumber.Trim <> "", ovrpo.POHeader.SONumber, ovrpo.POHeader.PONumber)
                End If
                lblVHF.Text = FormatNumber(oVRPO.TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPF.Text = FormatNumber(oVRPO.TotalHargaPP, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblITF.Text = FormatNumber(oVRPO.TotalHargaIT, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                txtAmountF.Text = FormatNumber(Me.GetDefaulAmount(oVRPO), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.SetInterestDiffOfAccelerate(Nothing, DiffIntF, PPhF)
                lblDiffIntF.Text = FormatNumber(DiffIntF, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPhF.Text = FormatNumber(PPhF, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Else
                lblPOF.Text = ""
                lblVHF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblITF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                txtAmountF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblDiffIntF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPPhF.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
        ElseIf e.CommandName.ToUpper = "DEL" Then
            Dim aTemp As ArrayList = Me._sessHelper.GetSession(Me._sesData)
            atemp.RemoveAt(e.Item.ItemIndex)
            Me._sessHelper.SetSession(Me._sesData, aTemp)
            Me.BindData()

        End If
    End Sub

    Private Sub dtgMain_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.CancelCommand
        Me.dtgMain.EditItemIndex = -1
        Me.BindData()
        Me.btnSave.Enabled = True
    End Sub

    Private Sub dtgMain_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.EditCommand
        Me.btnSave.Enabled = False
        Me.dtgMain.EditItemIndex = CInt(e.Item.ItemIndex)
        Me.BindData()
    End Sub

    Private Sub dtgMain_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.UpdateCommand
        Dim aTemp As ArrayList = CType(Me._sessHelper.GetSession(Me._sesData), ArrayList)
        Dim oDP As DailyPayment = aTemp(e.Item.ItemIndex)
        Dim ddlAssignmentE As DropDownList = e.Item.FindControl("ddlAssignmentE")
        Dim txtAmountE As TextBox = e.Item.FindControl("txtAmountE")
        Dim txtRef1E As TextBox = e.Item.FindControl("txtRef1E")
        Dim txtRef2E As TextBox = e.Item.FindControl("txtRef2E")
        Dim txtRef3E As TextBox = e.Item.FindControl("txtRef3E")
        Dim txtReasonE As TextBox = e.Item.FindControl("txtReasonE")

        If CType(ddlAssignmentE.SelectedValue, Integer) < 1 Then
            MessageBox.Show("Silahkan pilih Nomer PO terlebih dahulu")
            Exit Sub
        End If
        Dim sErrMessage As String = ""
        If Not IsValidAmount(txtAmountE) Then Exit Sub
        If Me.IsAcceleration() Then
            'check amount with old gyro's assignment
            If Not Me.IsValidWithOldDP(oDP, sErrMessage) Then
                MessageBox.Show(sErrMessage)
                Exit Sub
            End If
        End If

        oDP = Me.ConvertToDailyPayment(oDP.ID, CType(ddlAssignmentE.SelectedValue, Integer), txtAmountE.Text, txtRef1E.Text, txtRef2E.Text, txtRef3E.Text, txtReasonE.Text)
        Dim oDPTemp As DailyPayment = oDP
        aTemp(e.Item.ItemIndex) = oDP
        If Not Me.IsValidBasedOnFactoringType(aTemp, sErrMessage) Then
            aTemp(e.Item.ItemIndex) = oDPTemp
            MessageBox.Show(sErrMessage)
            Exit Sub
        End If

        If oDP.POHeader.IsFactoring = 1 Then
            dtgMain.ShowFooter = False
        Else
            If Not Me.IsAcceleration() Then
                dtgMain.ShowFooter = True
            End If
        End If
        Me.dtgMain.EditItemIndex = -1
        Me.BindData()
        Me.btnSave.Enabled = True
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim Url As String = Me._sessHelper.GetSession("FrmEntryGyro.PageOpener")
        If Url <> "" Then
            Response.Redirect(Url)
        End If
    End Sub

    Private Sub dtgMain_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.SetRenderMethodDelegate(AddressOf RenderHeader)
        End If
    End Sub

    Private Sub ddlEntryType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEntryType.SelectedIndexChanged
        Me.BindBank()
        SetGyroNumberControl()
    End Sub

    Private Sub ddlGyroType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGyroType.SelectedIndexChanged
        Me.BindEntry()
        SetControl("GyroType")

        If CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.GantiGyro OrElse CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.Percepatan OrElse CType(Me.ddlGyroType.SelectedValue, Short) = EnumGyroType.GyroType.Tolakan Then
            If Not IsNothing(Request.Item("DPID")) AndAlso CType(Request.Item("DPID"), Integer) > 0 Then
                Dim oDP As DailyPayment = New DailyPaymentFacade(User).Retrieve(CType(Request.Item("DPID"), Integer))

                Me.calBaseline.Value = oDP.BaselineDate
            End If

        End If
        'Input dari ui
        If Not IsNothing(e) Then
            ddlEntryType_SelectedIndexChanged(Nothing, Nothing)
        End If

    End Sub

    Private Sub ddlPaymentPurpose_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPaymentPurpose.SelectedIndexChanged
        Dim oPP As PaymentPurpose = Me.GetPaymentPurpose()
        If Not IsNothing(oPP) AndAlso oPP.ID > 0 Then
            If oPP.PaymentPurposeCode.Trim.ToUpper = "PP" Then
                Dim GyroDate = New Date(Me.calReqDate.Value.Year, Me.calReqDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                Dim aDP As ArrayList = Me._sessHelper.GetSession(Me._sesData)
                If adp.Count > 0 Then
                    Dim objDP As DailyPayment = adp(0)
                    Me.calBaseline.Value = objDP.POHeader.ReqAllocationDateTime  ' DateAdd(DateInterval.Day, objDP.POHeader.TermOfPayment.TermOfPaymentValue, Me.calReqDate.Value)
                    GyroDate = New Date(objDP.POHeader.ReqAllocationDateTime.Year, objDP.POHeader.ReqAllocationDateTime.Month, 1).AddMonths(1).AddDays(-1)
                Else
                    GyroDate = New Date(Me.calReqDate.Value.Year, Me.calReqDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Me.calBaseline.Value = GyroDate
                Me.calBaseline.Enabled = True
            ElseIf oPP.PaymentPurposeCode.Trim.ToUpper = "IT" Then
                Dim aDP As ArrayList = Me._sessHelper.GetSession(Me._sesData)
                If adp.Count > 0 Then
                    Dim objDP As DailyPayment = adp(0)
                    Me.calBaseline.Value = objDP.POHeader.ReqAllocationDateTime  ' DateAdd(DateInterval.Day, objDP.POHeader.TermOfPayment.TermOfPaymentValue, Me.calReqDate.Value)
                Else
                    Me.calBaseline.Value = Me.calReqDate.Value
                End If
                Me.calBaseline.Enabled = False
            ElseIf oPP.PaymentPurposeCode.Trim.ToUpper = "VH" OrElse oPP.PaymentPurposeCode.Trim.ToUpper = "VH+PP" OrElse oPP.PaymentPurposeCode.Trim.ToUpper = "VH+PP+IT" Then
                Dim aDP As ArrayList = Me._sessHelper.GetSession(Me._sesData)
                If adp.Count > 0 Then
                    Dim objDP As DailyPayment = adp(0)
                    Me.calBaseline.Value = DateAdd(DateInterval.Day, objDP.POHeader.TermOfPayment.TermOfPaymentValue, objDP.POHeader.ReqAllocationDateTime)
                Else
                    Me.calBaseline.Value = Me.calReqDate.Value
                End If
                Me.calBaseline.Enabled = False
            End If
        End If
        If Me.btnSave.Visible AndAlso Me.btnSave.Enabled Then
            'Update Default Amount
            Dim aDP As ArrayList = Me._sessHelper.GetSession(Me._sesData)
            If Not IsNothing(aDP) AndAlso aDP.Count > 0 Then
                Dim aDPUpdated As New ArrayList
                Dim oVRPOFac As V_RekapPOFacade = New V_RekapPOFacade(User)

                For Each oDP As DailyPayment In aDP
                    oDP.Amount = Me.GetDefaulAmount(oVRPOFac.Retrieve(oDP.POHeader.ID))
                    aDPUpdated.Add(oDP)
                Next
                Me._sessHelper.SetSession(Me._sesData, aDPUpdated)
            End If
            Me.BindData()
        End If
    End Sub

    Private Sub ddlBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBank.SelectedIndexChanged
        'If Me.IsAcceleration = False Then
        If CType(Me.ddlEntryType.SelectedValue, Short) = EnumGyroEntryType.EntryType.Transfer Then
            Dim strBankcode As String = Me.ddlBank.SelectedValue
            If strBankcode = "-1" Then
                strBankcode = ""
            End If
            Me.txtGyroNumber.Text = "TRF " & strBankcode
        Else
            'Me.txtGyroNumber.Text = ""
        End If
        'End If

    End Sub

#End Region

    Private Sub btnUpdateDTG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateDTG.Click
        calAccelerated_Changed() 'Me.BindData()
    End Sub
End Class