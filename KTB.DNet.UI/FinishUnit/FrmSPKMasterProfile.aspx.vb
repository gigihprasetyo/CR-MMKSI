Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General

Public Class FrmSPKMasterProfile
    Inherits System.Web.UI.Page

    Private CVGroup As String = "cust_prf_cv"
    Private PCGroup As String = "cust_prf_pc"
    Private LCVGroup As String = "cust_prf_lcv"
    Private isCV As Boolean = False
    Private isPC As Boolean = False
    Private isLCV As Boolean = False
    Protected WithEvents pnlMasterProfilePC As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlMasterProfileCV As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlMasterProfileLCV As System.Web.UI.WebControls.Panel
    Protected WithEvents lblCV As System.Web.UI.WebControls.Label
    Protected WithEvents lblPC As System.Web.UI.WebControls.Label
    Protected WithEvents lblLCV As System.Web.UI.WebControls.Label


    Private _sesshelper As SessionHelper = New SessionHelper

    Private mode As enumMode.Mode
    Private _vstSPKHeader As String = "_vstSPKHeader"
    Private _vstSPKDetail As String = "_vstSPKDetail"

    Private _objSPKDetail As SPKDetail
    Private _objSPKHeader As SPKHeader

    Private _ctlJenisKendaraan As String = ""
    Protected WithEvents btnBindModel As System.Web.UI.WebControls.Button
    Protected WithEvents btnDisableLeasing As System.Web.UI.WebControls.Button
    Protected WithEvents txtNewKindID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNewModelID As System.Web.UI.WebControls.TextBox
    Private _ctlModelKendaraan As String = ""
    Private _ctlLeasing As String = ""
    Private _ctlKaroseri As String = ""
    Private SPKDCID As Integer
    Private _strPrevString As String = "_strPrevString"

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents BtnTutup As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        LoadingData()
    End Sub

#End Region


    Private Sub LoadingData()
        If Not IsNothing(Request.QueryString("Mode")) Then
            ViewState("Mode") = CType(Request.QueryString("Mode"), enumMode.Mode)
            mode = ViewState("Mode")
        Else
            mode = enumMode.Mode.EditMode
        End If

        Dim isReadOnly As Boolean
        If mode = enumMode.Mode.ViewMode Then
            isReadOnly = True
            btnSimpan.Visible = False
        Else
            isReadOnly = False
            btnSimpan.Visible = True
        End If
        LoadProfile(False)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(Request.QueryString("SPKDCID")) AndAlso Request.QueryString("SPKDCID") <> "" Then
            SPKDCID = CInt(Request.QueryString("SPKDCID"))
        End If
        If Not Page.IsPostBack Then
            If Not Request.Item("spkHeader") Is Nothing Then
                If IsNothing(ViewState.Item(Me._vstSPKHeader)) Then
                    ViewState.Add(Me._vstSPKHeader, Request.Item("spkHeader"))
                Else
                    ViewState.Item(Me._vstSPKHeader) = Request.Item("spkHeader")
                End If
            Else
                If IsNothing(ViewState.Item(Me._vstSPKHeader)) Then
                    ViewState.Add(Me._vstSPKHeader, "FrmSPKHeader.SPKHeader" & Now.ToString("yyyyMMddhhmmssfff"))
                Else
                    ViewState.Item(Me._vstSPKHeader) = "FrmSPKHeader.SPKHeader" & Now.ToString("yyyyMMddhhmmssfff")
                End If
            End If
            _objSPKHeader = _sesshelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
            'ViewState("Mode") = CType(Request.QueryString("Mode"), enumMode.Mode)
            'mode = ViewState("Mode")
            'If Request.QueryString("Id") <> String.Empty And mode <> enumMode.Mode.NewItemMode Then
            '    _objSPKDetail = _objSPKHeader.SPKDetails(0)
            '    _sesshelper.SetSession(viewstate.Item(Me._vstSPKDetail), _objSPKDetail)
            'Else
            '    '_objSPKDetail = CType(_sesshelper.GetSession(viewstate.Item(Me._vstSPKDetail)), SPKDetail)
            '    _sesshelper.SetSession(viewstate.Item(Me._vstSPKDetail), _objSPKDetail)
            'End If
            Dim strPrevString As String = ""
            For Each Str As String In Request.QueryString.AllKeys
                If strPrevString = "" Then
                    strPrevString = String.Format("{0}={1}", Str, Request.QueryString(Str))
                Else
                    strPrevString = strPrevString & String.Format("&{0}={1}", Str, Request.QueryString(Str))
                End If
            Next

            ViewState(_strPrevString) = strPrevString

            If Not _objSPKHeader Is Nothing AndAlso Not IsNothing(_objSPKHeader.Dealer) Then

                Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, _objSPKHeader.Dealer.ID))
                Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
                Dim arlDealerSystem As ArrayList = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
                For Each objDealerSystem As DealerSystems In arlDealerSystem
                    If objDealerSystem.isSPKDNET Then
                    Else
                        If Not _objSPKHeader Is Nothing Then
                            If Not CType(_objSPKHeader.CreatedTime, Date) < objDealerSystem.GoLiveDate Then
                                ViewState("SPKDMS") = True
                                btnSimpan.Visible = False
                            End If
                        End If
                    End If
                Next
            Else
                Try
                    If SPKDCID > 0 Then
                        Dim ob As SPKDetailCustomer = New SPKDetailCustomer(SPKDCID)
                        ob = New SPKDetailCustomerFacade(User).Retrieve(SPKDCID)
                        If Not IsNothing(ob) Then
                            Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, ob.SPKDetail.SPKHeader.Dealer.ID))
                            Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
                            Dim arlDealerSystem As ArrayList = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
                            For Each objDealerSystem As DealerSystems In arlDealerSystem
                                If objDealerSystem.isSPKDNET Then
                                Else
                                    If Not CType(ob.SPKDetail.SPKHeader.CreatedTime, Date) < objDealerSystem.GoLiveDate Then
                                        ViewState("SPKDMS") = True
                                        btnSimpan.Visible = False
                                    End If

                                End If
                            Next

                        End If
                    End If
                Catch ex As Exception

                End Try

            End If

            If SPKDCID > 0 Then
                Dim ob As SPKDetailCustomer = New SPKDetailCustomer(SPKDCID)
                ob = New SPKDetailCustomerFacade(User).Retrieve(SPKDCID)
                If Not IsNothing(ob) AndAlso Not IsNothing(ob.CustomerRequest) Then
                    If ob.CustomerRequest.ID > 0 Then
                        btnSimpan.Visible = False
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub GetRenderControl()
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
        Dim oPHModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
        Dim oPHLeasing As ProfileHeader = oPHFac.Retrieve("CBU_LEASING")
        Dim oPHKaroseri As ProfileHeader = oPHFac.Retrieve("CBU_CARROSSERIE")

        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        oPG = oPGFac.Retrieve("cust_prf_" & _objSPKDetail.Category.CategoryCode.ToLower)
        Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        'cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileHeader.ID", MatchType.InSet, , oPH.ID))
        Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

        For Each phtg As ProfileHeaderToGroup In oPHTGList
            If phtg.ProfileHeader.ID = oPHJenis.ID Then
                _ctlJenisKendaraan = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlJenis", _ctlJenisKendaraan)
            ElseIf phtg.ProfileHeader.ID = oPHModel.ID Then
                _ctlModelKendaraan = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlModel", _ctlModelKendaraan)
            ElseIf phtg.ProfileHeader.ID = oPHLeasing.ID Then
                _ctlLeasing = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlLeasing", _ctlLeasing)
            ElseIf phtg.ProfileHeader.ID = oPHKaroseri.ID Then
                _ctlKaroseri = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlKaroseri", _ctlKaroseri)
            End If
        Next

    End Sub
    Private Sub ManageDDLControl(ByVal pnl As Panel)
        'DDLIST + ProfileHeaderToGroup.ID + "_" + Profilegroup.ID

        Dim ddlJenis As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim ddlLeasing As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
        Dim ddlKaroseri As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlKaroseri"), String))


        Dim oVKGFac As VehicleKindGroupFacade = New VehicleKindGroupFacade(User)

        Dim cVKG As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strGroup As String = ""
        If Not IsNothing(_objSPKDetail.VechileColor.VechileType) Then
            If _objSPKDetail.VechileColor.VechileType.IsVehicleKind1 = 1 Then
                strGroup &= "2,"
            End If
            If _objSPKDetail.VechileColor.VechileType.IsVehicleKind2 = 1 Then
                strGroup &= "3,"
            End If
            If _objSPKDetail.VechileColor.VechileType.IsVehicleKind3 = 1 Then
                strGroup &= "4,"
            End If
            If _objSPKDetail.VechileColor.VechileType.IsVehicleKind4 = 1 Then
                strGroup &= "5,"
            End If

        Else
            If Not IsNothing(_objSPKDetail.VehicleTypeCode) Then
                Dim objVT As VechileType = New VechileTypeFacade(User).Retrieve(_objSPKDetail.VehicleTypeCode)
                If Not IsNothing(objVT) Then
                    If objVT.IsVehicleKind1 = 1 Then
                        strGroup &= "2,"
                    End If
                    If objVT.IsVehicleKind2 = 1 Then
                        strGroup &= "3,"
                    End If
                    If objVT.IsVehicleKind3 = 1 Then
                        strGroup &= "4,"
                    End If
                    If objVT.IsVehicleKind4 = 1 Then
                        strGroup &= "5,"
                    End If
                End If
            End If
        End If

        If strGroup <> "" Then
            strGroup = Left(strGroup, strGroup.Length - 1)
            cVKG.opAnd(New Criteria(GetType(VehicleKindGroup), "ID", MatchType.InSet, "(" & strGroup & ")"))
        Else
            MessageBox.Show("Jenis kendaraan belum di setting. Silahkan hubungi pihak MMKSI.")
            btnSimpan.Enabled = False
            Exit Sub
        End If


        Dim aVKG As ArrayList
        Dim oVKofCM As VehicleKind
        aVKG = oVKGFac.Retrieve(cVKG)

        If Not IsNothing(ddlJenis) Then
            With ddlJenis.Items
                .Clear()
                For Each oVKG As VehicleKindGroup In aVKG
                    .Add(New ListItem(oVKG.Description, oVKG.ID))
                Next
            End With
        End If
        If Not IsNothing(_objSPKDetail.VehicleKind) Then
            If Not IsNothing(ddlJenis) Then
                ddlJenis.SelectedValue = _objSPKDetail.VehicleKind.VehicleKindGroupID
                Me.txtNewKindID.Text = ddlJenis.SelectedValue
            End If
            'BindDDLModel(pnl, True)
        End If

        BindDDLModel(pnl, True)
        btnBindModel_Click(Nothing, Nothing)
        If Not IsNothing(ddlJenis) And Not IsNothing(ddlModel) Then
            If _objSPKDetail.Category.CategoryCode = "CV" Then
                ddlJenis.Attributes.Add("OnChange", "RebindModel(" & 2 & ")")
                ddlModel.Attributes.Add("OnChange", "ChoosenModel(" & 2 & ")")
            End If
            If _objSPKDetail.Category.CategoryCode = "LCV" Then
                ddlJenis.Attributes.Add("OnChange", "RebindModel(" & 1 & ")")
                ddlModel.Attributes.Add("OnChange", "ChoosenModel(" & 1 & ")")
            End If
            If _objSPKDetail.Category.CategoryCode = "PC" Then
                ddlJenis.Attributes.Add("OnChange", "RebindModel(" & 0 & ")")
                ddlModel.Attributes.Add("OnChange", "ChoosenModel(" & 0 & ")")
            End If
            'ddlJenis.Attributes.Add("OnChange", "RebindModel()")
            'ddlModel.Attributes.Add("OnChange", "ChoosenModel()")
        End If
        'Me.btnKembali.Text = "Kembali."

        ''Bug Fix Model Kendaraan
        If SPKDCID > 0 Then
            Dim obP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            obP.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomer.ID", MatchType.Exact, SPKDCID))

            obP.opAnd(New Criteria(GetType(SPKProfile), "ProfileHeader.Code", MatchType.Exact, "CBU_MODELKEND"))
            Dim arrP As New ArrayList
            arrP = New SPKProfileFacade(User).Retrieve(obP)
            If arrP.Count > 0 Then
                Try
                    ddlModel.SelectedValue = CType(arrP(0), SPKProfile).ProfileValue
                Catch ex As Exception
                    Dim b = 0
                End Try

            End If
        End If

        If Not IsNothing(ddlLeasing) Then

            Dim ddlPayment As DropDownList = GetDDLProfile("CBU_WAYPAID1")
            ddlPayment.Attributes.Add("OnChange", "DisableLeasing()")

            If ddlPayment.SelectedItem.Text.Trim = "TUNAI" Then
                ddlLeasing.SelectedIndex = 0
                ddlLeasing.Enabled = False
            ElseIf ddlPayment.SelectedItem.Text = "KREDIT" Then
                ddlLeasing.Enabled = True
            End If
        End If

        If Not IsNothing(ddlLeasing) Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHLeasing As ProfileHeader = oPHFac.Retrieve("CBU_LEASING")
            Dim oLeasingFac As LeasingFacade = New LeasingFacade(User)
            Dim cLeasing As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cLeasing.opAnd(New Criteria(GetType(Leasing), "Status", MatchType.Exact, CType(1, Byte)))

            Dim aLeasing As ArrayList
            aLeasing = oLeasingFac.Retrieve(cLeasing)

            Dim strProfileValue As String = GetProfileValue(oPHLeasing)
            Dim intProfileID As Integer = 0
            If strProfileValue = "" Then strProfileValue = "0"

            With ddlLeasing.Items
                .Clear()
                For Each oLeasing As Leasing In aLeasing
                    .Add(New ListItem(oLeasing.LeasingName, oLeasing.LeasingCode))
                Next
            End With
            Dim listSilahkanPilih As ListItem = New ListItem("Silahkan Pilih", "")
            ddlLeasing.Items.Insert(0, listSilahkanPilih)

            Try
                ddlLeasing.SelectedValue = strProfileValue
            Catch ex As Exception

            End Try
        End If

        If Not IsNothing(ddlKaroseri) Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHKaroseri As ProfileHeader = oPHFac.Retrieve("CBU_CARROSSERIE")
            Dim oKaroseriFac As KaroseriFacade = New KaroseriFacade(User)
            Dim cKaroseri As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Karoseri), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cKaroseri.opAnd(New Criteria(GetType(Karoseri), "Status", MatchType.Exact, CType(1, Byte)))

            Dim aKaroseri As ArrayList
            aKaroseri = oKaroseriFac.Retrieve(cKaroseri)

            Dim strProfileValue As String = GetProfileValue(oPHKaroseri)
            Dim intProfileID As Integer = 0
            If strProfileValue = "" Then strProfileValue = "0"

            With ddlKaroseri.Items
                .Clear()
                For Each oKaroseri As Karoseri In aKaroseri
                    .Add(New ListItem(oKaroseri.Name, oKaroseri.Code))
                Next
            End With
            Dim listSilahkanPilih As ListItem = New ListItem("Silahkan Pilih", "")
            ddlKaroseri.Items.Insert(0, listSilahkanPilih)

            Try
                ddlKaroseri.SelectedValue = strProfileValue
            Catch ex As Exception

            End Try

        End If
    End Sub


    Private Function GetProfileValue(ByVal oPH As ProfileHeader) As String
        Dim objGroup As ProfileGroup
        Dim strProfileValue As String = ""

        Select Case _objSPKDetail.Category.CategoryCode
            Case "PC"
                objGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)
            Case "CV"
                objGroup = New ProfileGroupFacade(User).Retrieve(CVGroup)
            Case "LCV"
                objGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)
        End Select

        Dim objFacade As SPKProfileFacade = New SPKProfileFacade(System.Threading.Thread.CurrentPrincipal)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetail.ID", MatchType.Exact, _objSPKDetail.ID))
        criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
        criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
        Dim objListSPKProfile As ArrayList = objFacade.Retrieve(criterias)
        If objListSPKProfile.Count > 0 Then
            strProfileValue = CType(objListSPKProfile(0), SPKProfile).ProfileValue
        End If

        Return strProfileValue
    End Function

    Private Sub BindDDLModel(ByVal pnl As Panel, Optional ByVal IsNotFromClient As Boolean = False)
        Dim ddlJenis As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim oVKFac As New VehicleKindFacade(User)
        Dim aVK As ArrayList

        If Not IsNothing(ddlModel) Then
            With ddlModel.Items
                .Clear()
                If CType(ddlJenis.SelectedValue, Integer) > 0 Then

                    Dim strGroup As String = ""

                    If Not IsNothing(_objSPKDetail.VechileColor.VechileType) Then
                        If _objSPKDetail.VechileColor.VechileType.IsVehicleKind1 = 1 Then
                            strGroup &= "2,"
                        End If
                        If _objSPKDetail.VechileColor.VechileType.IsVehicleKind2 = 1 Then
                            strGroup &= "3,"
                        End If
                        If _objSPKDetail.VechileColor.VechileType.IsVehicleKind3 = 1 Then
                            strGroup &= "4,"
                        End If
                        If _objSPKDetail.VechileColor.VechileType.IsVehicleKind4 = 1 Then
                            strGroup &= "5,"
                        End If

                    Else
                        If Not IsNothing(_objSPKDetail.VehicleTypeCode) Then
                            Dim objVT As VechileType = New VechileTypeFacade(User).Retrieve(_objSPKDetail.VehicleTypeCode)
                            If Not IsNothing(objVT) Then
                                If objVT.IsVehicleKind1 = 1 Then
                                    strGroup &= "2,"
                                End If
                                If objVT.IsVehicleKind2 = 1 Then
                                    strGroup &= "3,"
                                End If
                                If objVT.IsVehicleKind3 = 1 Then
                                    strGroup &= "4,"
                                End If
                                If objVT.IsVehicleKind4 = 1 Then
                                    strGroup &= "5,"
                                End If
                            End If
                        End If
                    End If
                    If strGroup <> "" Then
                        strGroup = Left(strGroup, strGroup.Length - 1)
                        Dim cVK As New CriteriaComposite(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.InSet, "(" & strGroup & ")"))
                        aVK = oVKFac.Retrieve(cVK)
                    Else
                        MessageBox.Show("Jenis kendaraan belum di setting. Silahkan hubungi pihak MMKSI.")
                        btnSimpan.Enabled = False
                        Exit Sub
                    End If

                    For Each oVK As VehicleKind In aVK
                        If oVK.VehicleKindGroupID = CInt(ddlJenis.SelectedValue) Then
                            .Add(New ListItem(oVK.Description, oVK.ID))
                        End If
                    Next

                    'If IsNotFromClient AndAlso Not IsNothing(_objSPKDetail) AndAlso Not IsNothing(_objSPKDetail.VehicleKind) Then
                    '    ddlModel.SelectedValue = _objSPKDetail.VehicleKind.ID
                    'End If
                    'If Not IsNothing(_objSPKDetail) AndAlso Not IsNothing(_objSPKDetail.VehicleKind) Then
                    '    Try
                    '        ddlModel.SelectedValue = _objSPKDetail.VehicleKind.ID
                    '    Catch ex As Exception
                    '        ddlModel.SelectedIndex = 0
                    '    End Try

                    'End If
                    Me.txtNewModelID.Text = ddlModel.SelectedValue
                End If
            End With

        End If

    End Sub

    Private Sub LoadProfile(ByVal isAllow As Boolean)
        pnlMasterProfileCV.Visible = False
        pnlMasterProfilePC.Visible = False
        pnlMasterProfileLCV.Visible = False
        lblPC.Visible = False
        lblLCV.Visible = False
        lblCV.Visible = False

        Dim spkID As Integer = Val(Request.QueryString("spkDetailIdx"))
        'SPKDCID
        _objSPKDetail = New SPKDetailFacade(User).Retrieve(spkID)
        If Not IsNothing(Request.QueryString("SPKDCID")) AndAlso Request.QueryString("SPKDCID") <> "" Then
            SPKDCID = CInt(Request.QueryString("SPKDCID"))
        End If
        If Not IsNothing(_objSPKDetail.Category) Then
            If _objSPKDetail.Category.CategoryCode = "CV" Then
                isCV = True
            End If
            If _objSPKDetail.Category.CategoryCode = "LCV" Then
                isLCV = True
            End If
            If _objSPKDetail.Category.CategoryCode = "PC" Then
                isPC = True
            End If
        End If

        Dim objGroupProfileCV As ProfileGroup = New ProfileGroupFacade(User).Retrieve(CVGroup)
        Dim objGroupProfilePC As ProfileGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)
        Dim objGroupProfileLCV As ProfileGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)

        Dim IDtoDisplay As Integer = _objSPKDetail.ID
        'IDtoDisplay = Val(Request.QueryString("spkDetailIdx"))
        If SPKDCID > 0 Then
            IDtoDisplay = SPKDCID
        End If

        If objGroupProfileCV.ID > 0 And isCV Then
            pnlMasterProfileCV.Visible = isCV
            lblCV.Visible = isCV
            RenderProfilePanel(IDtoDisplay, objGroupProfileCV, pnlMasterProfileCV, isAllow)
        End If
        If objGroupProfilePC.ID > 0 And isPC Then
            pnlMasterProfilePC.Visible = isPC
            lblPC.Visible = isPC
            RenderProfilePanel(IDtoDisplay, objGroupProfilePC, pnlMasterProfilePC, isAllow)
        End If
        If objGroupProfileLCV.ID > 0 And isLCV Then
            pnlMasterProfileLCV.Visible = isLCV
            lblLCV.Visible = isLCV
            RenderProfilePanel(IDtoDisplay, objGroupProfileLCV, pnlMasterProfileLCV, isAllow)

        End If
    End Sub

    Private Sub RenderProfilePanel(ByVal ID As Integer, ByVal objGroup As ProfileGroup, ByVal objPanel As Panel, ByVal mode As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(mode)
        If SPKDCID > 0 Then
            objRenderPanel.GeneratePanel(ID, objPanel, objGroup, EnumProfileType.ProfileType.SPKDETAILCUSTOMERPROFILE, User)
        Else
            objRenderPanel.GeneratePanel(ID, objPanel, objGroup, EnumProfileType.ProfileType.SPKPROFILE, User)
        End If

        GetRenderControl()
        ManageDDLControl(objPanel)
    End Sub

    Private Sub BtnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTutup.Click
        Dim cat As String = _objSPKDetail.Category.CategoryCode
        If Not IsNothing(Request.QueryString("FromPage")) Then
            Dim strPrevString As String = ViewState(_strPrevString)
            strPrevString = strPrevString.Substring(0, strPrevString.IndexOf("&SPKDCID")) & "&Prof=1"
            Response.Redirect("FrmSPKDetailCustomers.aspx?" & strPrevString)
        Else
            Response.Redirect("FrmSPKHeaderProfile.aspx?Id=" & _objSPKDetail.SPKHeader.ID())
        End If
    End Sub

    Private Sub btnBindModel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBindModel.Click
        Dim objPanel As System.Web.UI.WebControls.Panel
        If pnlMasterProfilePC.Visible = True Then
            objPanel = pnlMasterProfilePC
        End If
        If pnlMasterProfileCV.Visible = True Then
            objPanel = pnlMasterProfileCV
        End If
        If pnlMasterProfileLCV.Visible = True Then
            objPanel = pnlMasterProfileLCV
        End If
        Me.BindDDLModel(objPanel, False)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Exit Sub
        End If
        Dim pnl As Panel
        If _objSPKDetail.Category.CategoryCode = "CV" Then
            pnl = pnlMasterProfileCV
        End If
        If _objSPKDetail.Category.CategoryCode = "LCV" Then
            pnl = pnlMasterProfileLCV
        End If
        If _objSPKDetail.Category.CategoryCode = "PC" Then
            pnl = pnlMasterProfilePC
        End If

        Dim ddlJenis As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim ddlLeasing As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))

        If ddlLeasing.SelectedIndex = 0 Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHPayment As ProfileHeader = oPHFac.Retrieve("CBU_WAYPAID1")

            Dim oPG As ProfileGroup
            Dim oPGFac As New ProfileGroupFacade(User)
            oPG = oPGFac.Retrieve("cust_prf_" & _objSPKDetail.Category.CategoryCode.ToLower)
            Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
            Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
            Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

            Dim _ctlPayment As String = ""

            For Each phtg As ProfileHeaderToGroup In oPHTGList
                If phtg.ProfileHeader.ID = oPHPayment.ID Then
                    _ctlPayment = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                End If
            Next

            Dim ddlPayment As DropDownList = pnl.FindControl(_ctlPayment)
        End If


        _objSPKDetail.VehicleKind = New VehicleKind(CType(ddlModel.SelectedValue, Integer))

        Dim oSPKFac As New SPKDetailFacade(User)
        If oSPKFac.Update(_objSPKDetail) > 0 Then
            SaveProfile()
            btnSimpan.Enabled = False
        End If

    End Sub

    Private Sub SaveProfile()
        Dim objRenderPanel As RenderingProfile
        Dim isGovt As Boolean = False
        Dim isLKPPFilled As Boolean = False
        'objRenderPanel = New RenderingProfile
        'Dim PCList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(PCGroup), CType(EnumProfileType.ProfileType.SPKPROFILE, Short), User)
        'objRenderPanel = New RenderingProfile
        'Dim CVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(CVGroup), CType(EnumProfileType.ProfileType.SPKPROFILE, Short), User)
        'objRenderPanel = New RenderingProfile
        'Dim LCVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(LCVGroup), CType(EnumProfileType.ProfileType.SPKPROFILE, Short), User)

        Dim objfacade As SPKProfileFacade
        If _objSPKDetail.Category.CategoryCode = "CV" Then
            objRenderPanel = New RenderingProfile
            Dim CVList As ArrayList
            If SPKDCID = 0 Then
                CVList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(CVGroup), CType(EnumProfileType.ProfileType.SPKPROFILE, Short), User)
            Else
                CVList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(CVGroup), CType(EnumProfileType.ProfileType.SPKDETAILCUSTOMERPROFILE, Short), User)
            End If

            For Each item As SPKProfile In CVList
                item.SPKDetail = _objSPKDetail
                If SPKDCID > 0 Then
                    item.SPKDetailCustomer = New SPKDetailCustomer(SPKDCID)
                    item.SPKDetailCustomerID = SPKDCID
                End If

                If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 5) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                    isGovt = True
                End If

                If Not IsNothing(item.SPKDetailCustomer.LKPPReference) Then
                    If item.SPKDetailCustomer.LKPPReference.Length > 0 Then
                        isLKPPFilled = True
                    End If
                End If
            Next
            objfacade = New SPKProfileFacade(User)
            objfacade.Update(CVList, _objSPKDetail)

            'Dim isMCP_Allowed As Boolean = True
            'For Each item As SPKProfile In CVList
            '    If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 5) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
            '        If _objSPKDetail.SPKHeader.CustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
            '            isMCP_Allowed = False
            '        End If
            '    End If
            '    item.SPKDetail = _objSPKDetail
            'Next

            'If isMCP_Allowed Then
            '    objfacade = New SPKProfileFacade(User)
            '    objfacade.Update(CVList, _objSPKDetail)
            'Else
            '    MessageBox.Show("Customer terdeteksi MCP. Update profile gagal")
            '    Exit Sub
            'End If
        End If
        If _objSPKDetail.Category.CategoryCode = "LCV" Then
            objRenderPanel = New RenderingProfile
            Dim LCVList As ArrayList

            If SPKDCID = 0 Then
                LCVList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(LCVGroup), CType(EnumProfileType.ProfileType.SPKPROFILE, Short), User)
            Else
                LCVList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(LCVGroup), CType(EnumProfileType.ProfileType.SPKDETAILCUSTOMERPROFILE, Short), User)
            End If

            For Each item As SPKProfile In LCVList
                item.SPKDetail = _objSPKDetail
                If SPKDCID > 0 Then
                    item.SPKDetailCustomer = New SPKDetailCustomer(SPKDCID)
                    item.SPKDetailCustomerID = SPKDCID
                End If

                If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 6) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                    isGovt = True
                End If

                If Not IsNothing(item.SPKDetailCustomer.LKPPReference) Then
                    If item.SPKDetailCustomer.LKPPReference.Length > 0 Then
                        isLKPPFilled = True
                    End If
                End If
            Next
            objfacade = New SPKProfileFacade(User)
            objfacade.Update(LCVList, _objSPKDetail)
        End If
        If _objSPKDetail.Category.CategoryCode = "PC" Then
            objRenderPanel = New RenderingProfile
            Dim PCList As ArrayList
            If SPKDCID = 0 Then
                PCList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(PCGroup), CType(EnumProfileType.ProfileType.SPKPROFILE, Short), User)
            Else
                PCList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(PCGroup), CType(EnumProfileType.ProfileType.SPKDETAILCUSTOMERPROFILE, Short), User)
            End If

            For Each item As SPKProfile In PCList
                item.SPKDetail = _objSPKDetail
                If SPKDCID > 0 Then
                    item.SPKDetailCustomer = New SPKDetailCustomer(SPKDCID)
                    item.SPKDetailCustomerID = SPKDCID
                End If

                If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 7) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                    isGovt = True
                End If

                If Not IsNothing(item.SPKDetailCustomer.LKPPReference) Then
                    If item.SPKDetailCustomer.LKPPReference.Length > 0 Then
                        isLKPPFilled = True
                    End If
                End If
            Next
            objfacade = New SPKProfileFacade(User)
            objfacade.Update(PCList, _objSPKDetail)
        End If

        Try
            Dim objAppConfig As AppConfig = New AppConfigFacade(User).Retrieve("LKPPDMSGoLive")
            If CBool(objAppConfig.Value) Then
                If isGovt And Not isLKPPFilled Then
                    MessageBox.Show("Customer terdeteksi LKPP silahkan Input Nomor Pengadaan")
                    Server.Transfer("FrmSPKDetailCustomer.aspx?Govt=true&mode=1&spkHeader=&SPKDCID=" & SPKDCID & "&Id=" & _objSPKDetail.SPKHeader.ID & "&spkDetailIdx=" & _objSPKDetail.ID & "&SPKDetailID=" & _objSPKDetail.ID & " &FromPage=FrmSPKMasterProfile")
                End If
            End If
        Catch ex As Exception

        End Try

        MessageBox.Show("Update Profile Berhasil.")
    End Sub

    Protected Sub btnDisableLeasing_Click(sender As System.Object, e As System.EventArgs) Handles btnDisableLeasing.Click

        Dim ddlPayment As DropDownList = GetDDLProfile("CBU_WAYPAID1")

        Dim ddlLeasing As DropDownList = New DropDownList

        If isCV Then
            ddlLeasing = pnlMasterProfileCV.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
        End If
        If isPC Then
            ddlLeasing = pnlMasterProfilePC.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
        End If
        If isLCV Then
            ddlLeasing = pnlMasterProfileLCV.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
        End If

        If ddlPayment.SelectedItem.Text.Trim = "TUNAI" Then
            ddlLeasing.SelectedIndex = 0
            ddlLeasing.Enabled = False
        ElseIf ddlPayment.SelectedItem.Text = "KREDIT" Then
            ddlLeasing.Enabled = True
        End If
    End Sub

    Private Function GetDDLProfile(ByVal strPHCode As String) As DropDownList
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHPayment As ProfileHeader = oPHFac.Retrieve(strPHCode)
        'Dim ddlResult As DropDownList = New DropDownList

        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        oPG = oPGFac.Retrieve("cust_prf_" & _objSPKDetail.Category.CategoryCode.ToLower)
        Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

        Dim _ctlPayment As String = ""

        For Each phtg As ProfileHeaderToGroup In oPHTGList
            If phtg.ProfileHeader.ID = oPHPayment.ID Then
                _ctlPayment = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
            End If
        Next

        If isCV Then
            Return pnlMasterProfileCV.FindControl(_ctlPayment)
        End If
        If isPC Then
            Return pnlMasterProfilePC.FindControl(_ctlPayment)
        End If
        If isLCV Then
            Return pnlMasterProfileLCV.FindControl(_ctlPayment)
        End If

    End Function
End Class
