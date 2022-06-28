Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

Public Class FrmMasterProfiles
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
    Protected WithEvents btnBindModel As System.Web.UI.WebControls.Button
    Protected WithEvents btnDisableLeasing As System.Web.UI.WebControls.Button
    Protected WithEvents txtNewKindID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNewModelID As System.Web.UI.WebControls.TextBox
    Private _sesshelper As SessionHelper = New SessionHelper
    Private _objEndCustomer As EndCustomer
    Private _objChassisMaster As ChassisMaster
    Private _ctlJenisKendaraan As String = ""
    Private _ctlModelKendaraan As String = ""
    Private _ctlLeasing As String = ""
    Private _ctlKaroseri As String = ""
    Private _isRevisionFaktur = False
    Private _objRevisionFaktur As RevisionFaktur

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

    Private Function GetListChassisID() As ArrayList
        Dim strID As String = Request.QueryString.Item("hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423")
        If strID.IndexOf("-") > 0 Then
            strID = strID.Substring(0, strID.Length - 1)
        Else
            strID = strID
        End If

        Dim list As ArrayList = New ArrayList
        If strID <> "" Then
            For Each item As String In strID.Split("-")
                list.Add(CInt(item))
            Next
        End If

        Return list
    End Function

    Private Sub LoadingData()
        If CType(Session("PREVPAGE"), String).Contains("FrmEntryInvoiceRevisionFaktur.aspx") Then
            _objRevisionFaktur = CType(Session("RevisionFaktur"), RevisionFaktur)

            ' if didn't have revisionfaktur data, that means revision didn't submit yet, so it considered as create faktur 
            If Not IsNothing(_objRevisionFaktur) Then
                _isRevisionFaktur = True
            End If
        End If
        Dim list As ArrayList = GetListChassisID()
        Dim mode As Integer = CInt(Request.QueryString.Item("adsfadfadfw32342412412412424"))
        Dim isReadOnly As Boolean
        If mode = 0 Then
            isReadOnly = True
            btnSimpan.Visible = False
        Else
            isReadOnly = False
            btnSimpan.Visible = True
        End If
        LoadProfile(False)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

        End If
        If CType(Session("PREVPAGE"), String).Contains("FrmPengajuanDummyFaktur.aspx") Then
            BtnTutup.Visible = False
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
        oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
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

        Dim ddlJenis As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim ddlLeasing As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
        Dim ddlKaroseri As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlKaroseri"), String))

        Dim oVKGFac As VehicleKindGroupFacade = New VehicleKindGroupFacade(User)

        Dim cVKG As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strGroup As String = ""
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind1 = 1 Then
            strGroup &= "2,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind2 = 1 Then
            strGroup &= "3,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind3 = 1 Then
            strGroup &= "4,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind4 = 1 Then
            strGroup &= "5,"
        End If
        If Not IsNothing(_objChassisMaster.VehicleKind) Then
            If _objChassisMaster.VehicleKind.ID = 1 Then
                strGroup &= "1,"
            End If
        End If

        strGroup = Left(strGroup, strGroup.Length - 1)
        cVKG.opAnd(New Criteria(GetType(VehicleKindGroup), "ID", MatchType.InSet, "(" & strGroup & ")"))

        Dim aVKG As ArrayList
        Dim oVKofCM As VehicleKind
        If Not IsNothing(_objChassisMaster.VehicleKind) Then
            If _objChassisMaster.VehicleKind.ID = 1 Then
                'aVKG = oVKGFac.RetrieveList()
                If CType(Session("PREVPAGE"), String).Contains("FrmPengajuanDummyFaktur.aspx") Then
                    aVKG = oVKGFac.RetrieveActiveList()
                Else
                    aVKG = oVKGFac.RetrieveList()
                End If
            Else
                aVKG = oVKGFac.Retrieve(cVKG)
            End If
        Else
            'aVKG = oVKGFac.RetrieveList()
            If CType(Session("PREVPAGE"), String).Contains("FrmPengajuanDummyFaktur.aspx") Then
                aVKG = oVKGFac.RetrieveActiveList()
            Else
                aVKG = oVKGFac.RetrieveList()
            End If
        End If

        If Not IsNothing(ddlJenis) Then
            With ddlJenis.Items
                .Clear()
                For Each oVKG As VehicleKindGroup In aVKG
                    .Add(New ListItem(oVKG.Description, oVKG.ID))
                Next
            End With
        End If
        If Not IsNothing(_objChassisMaster) Then
            If Not IsNothing(_objChassisMaster.VehicleKind) Then
                If Not IsNothing(ddlJenis) Then
                    ddlJenis.SelectedValue = _objChassisMaster.VehicleKind.VehicleKindGroup.ID
                End If
                BindDDLModel(pnl, True)
                btnBindModel_Click(Nothing, Nothing, True)
            End If

            If CType(Session("PREVPAGE"), String).Contains("FrmPengajuanDummyFaktur.aspx") Then
                If IsNothing(_objChassisMaster.VehicleKind) Then
                    BindDDLModel(pnl, True)
                    btnBindModel_Click(Nothing, Nothing, True)
                End If
            End If

        End If
        If Not IsNothing(ddlJenis) And Not IsNothing(ddlModel) Then
            If _objChassisMaster.Category.CategoryCode = "CV" Then
                ddlJenis.Attributes.Add("OnChange", "RebindModel(" & 2 & ")")
                ddlModel.Attributes.Add("OnChange", "ChoosenModel(" & 2 & ")")
            End If
            If _objChassisMaster.Category.CategoryCode = "LCV" Then
                ddlJenis.Attributes.Add("OnChange", "RebindModel(" & 1 & ")")
                ddlModel.Attributes.Add("OnChange", "ChoosenModel(" & 1 & ")")
            End If
            If _objChassisMaster.Category.CategoryCode = "PC" Then
                ddlJenis.Attributes.Add("OnChange", "RebindModel(" & 0 & ")")
                ddlModel.Attributes.Add("OnChange", "ChoosenModel(" & 0 & ")")
            End If

        End If

        ''Bug Fix Model Kendaraan
        If _objChassisMaster.ID > 0 Then
            Dim obP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            obP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))

            obP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.Code", MatchType.Exact, "CBU_MODELKEND"))
            Dim arrP As New ArrayList
            arrP = New ChassisMasterProfileFacade(User).Retrieve(obP)
            If arrP.Count > 0 AndAlso CType(arrP(0), ChassisMasterProfile).ProfileValue <> "" Then
                If IsNumeric(CType(arrP(0), ChassisMasterProfile).ProfileValue) Then
                    Try
                        ddlModel.SelectedValue = CType(arrP(0), ChassisMasterProfile).ProfileValue
                    Catch ex As Exception
                        Dim b = 0
                    End Try
                Else

                    Try

                        Dim obVK As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        obVK.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroupID", MatchType.Exact, ddlJenis.SelectedValue))

                        obVK.opAnd(New Criteria(GetType(VehicleKind), "Code", MatchType.Exact, ddlModel.SelectedValue = CType(arrP(0), ChassisMasterProfile).ProfileValue))

                        Dim arrM As New ArrayList
                        arrM = New VehicleKindFacade(User).Retrieve(obVK)

                        Try
                            ddlModel.SelectedValue = CType(arrM(0), VehicleKind).ID.ToString()
                        Catch ex As Exception
                            Dim b = 0
                        End Try

                    Catch ex As Exception
                        Dim aa = 0
                    End Try


                End If

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

            With ddlLeasing.Items
                .Clear()
                For Each oLeasing As Leasing In aLeasing
                    .Add(New ListItem(oLeasing.LeasingName, oLeasing.LeasingCode))
                    If oLeasing.LeasingCode = strProfileValue Then
                    End If

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

            With ddlKaroseri.Items
                .Clear()
                For Each oKaroseri As Karoseri In aKaroseri
                    .Add(New ListItem(oKaroseri.Name, oKaroseri.Code))

                Next
            End With
            Dim listSilahkanPilih As ListItem = New ListItem("Silahkan Pilih", "")
            ddlKaroseri.Items.Insert(0, listSilahkanPilih)

            ddlKaroseri.SelectedValue = strProfileValue
        End If

    End Sub

    Private Sub BindDDLModel(ByVal pnl As Panel, Optional ByVal IsNotFromClient As Boolean = False)
        Dim ddlJenis As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim oVKFac As New VehicleKindFacade(User)
        Dim aVK As ArrayList

        If Not IsNothing(ddlModel) Then
            With ddlModel.Items
                .Clear()
                If CType(ddlJenis.SelectedValue, Integer) > 0 Then
                    Dim cVK As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'cVK.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.Exact, CType(ddlJenis.SelectedValue, Integer)))
                    Dim strGroup As String = ""
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind1 = 1 Then
                        strGroup &= "2,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind2 = 1 Then
                        strGroup &= "3,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind3 = 1 Then
                        strGroup &= "4,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind4 = 1 Then
                        strGroup &= "5,"
                    End If

                    If Not IsNothing(_objChassisMaster.VehicleKind) AndAlso _objChassisMaster.VehicleKind.ID = 1 Then
                        strGroup &= "1,"
                    Else
                        If CType(Session("PREVPAGE"), String).Contains("FrmPengajuanDummyFaktur.aspx") Then
                            If Not IsNothing(ddlJenis) Then
                                strGroup = ddlJenis.SelectedValue & ","
                            End If
                        End If
                    End If

                    strGroup = Left(strGroup, strGroup.Length - 1)

                    'Dim cVK As New CriteriaComposite(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.InSet, "(" & strGroup & ")"))
                    cVK.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.InSet, "(" & strGroup & ")"))

                    aVK = oVKFac.Retrieve(cVK)

                    For Each oVK As VehicleKind In aVK
                        .Add(New ListItem(oVK.Description, oVK.ID))
                    Next

                    If IsNotFromClient AndAlso Not IsNothing(_objChassisMaster) AndAlso Not IsNothing(_objChassisMaster.VehicleKind) Then
                        ddlModel.SelectedValue = _objChassisMaster.VehicleKind.ID.ToString
                    End If
                End If
            End With

        End If

    End Sub

    Private Function GetChassisMaster(ByVal strProfile As String) As ChassisMaster
        Dim objChasisMaster As ChassisMaster = New ChassisMaster
        Dim strChassisID As String = ""
        Dim list As ArrayList = GetListChassisID()
        If Not IsNothing(list) AndAlso list.Count > 0 Then
            For Each id As Integer In list
                If strChassisID.Trim = "" Then
                    strChassisID = "('" & id
                Else
                    strChassisID += "','" & id
                End If
            Next
            If strChassisID.Trim <> "" Then
                strChassisID += "')"
            End If

            Dim objCMfacade As New ChassisMasterFacade(User)
            Dim cCM As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cCM.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, strChassisID))
            cCM.opAnd(New Criteria(GetType(ChassisMaster), "Category.CategoryCode", MatchType.Exact, strProfile))
            Dim arrCM As ArrayList = objCMfacade.Retrieve(cCM)
            If Not IsNothing(arrCM) AndAlso arrCM.Count > 0 Then
                objChasisMaster = CType(arrCM(0), ChassisMaster)
            End If
        End If

        Return objChasisMaster

    End Function

    Private Sub LoadProfile(ByVal mode As Boolean)
        Dim strProfile As String = ""

        pnlMasterProfileCV.Visible = False
        pnlMasterProfilePC.Visible = False
        pnlMasterProfileLCV.Visible = False
        lblPC.Visible = False
        lblLCV.Visible = False
        lblCV.Visible = False
        Dim list As ArrayList = GetListChassisID()
        For Each id As Integer In list
            Dim objfacade As ChassisMasterProfileFacade
            Dim objChasisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)
            If objChasisMaster.ID > 0 Then
                If objChasisMaster.Category.CategoryCode = "CV" Then
                    isCV = True
                End If
                If objChasisMaster.Category.CategoryCode = "LCV" Then
                    isLCV = True
                End If
                If objChasisMaster.Category.CategoryCode = "PC" Then
                    isPC = True
                End If
                _objChassisMaster = objChasisMaster
            End If
        Next

        Dim objGroupProfileCV As ProfileGroup = New ProfileGroupFacade(User).Retrieve(CVGroup)
        Dim objGroupProfilePC As ProfileGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)
        Dim objGroupProfileLCV As ProfileGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)

        Dim IDtoDisplay As Integer
        If Not IsNothing(Request.QueryString("hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423")) Then 'If Val(Request.QueryString("iseditsingle")) = 1 Then
            IDtoDisplay = Val(Request.QueryString("hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423"))
        Else
            IDtoDisplay = 0
        End If

        If objGroupProfileCV.ID > 0 And isCV Then
            strProfile = "CV"
            pnlMasterProfileCV.Visible = isCV
            lblCV.Visible = isCV
            _objChassisMaster = GetChassisMaster(strProfile)
            RenderProfilePanel(IDtoDisplay, objGroupProfileCV, pnlMasterProfileCV, mode)
        End If

        If objGroupProfilePC.ID > 0 And isPC Then
            strProfile = "PC"
            pnlMasterProfilePC.Visible = isPC
            lblPC.Visible = isPC
            _objChassisMaster = GetChassisMaster(strProfile)
            'RenderProfilePanel(0, objGroupProfilePC, pnlMasterProfilePC, mode)
            RenderProfilePanel(IDtoDisplay, objGroupProfilePC, pnlMasterProfilePC, mode)
        End If

        If objGroupProfileLCV.ID > 0 And isLCV Then
            strProfile = "LCV"
            pnlMasterProfileLCV.Visible = isLCV
            lblLCV.Visible = isLCV
            _objChassisMaster = GetChassisMaster(strProfile)
            RenderProfilePanel(IDtoDisplay, objGroupProfileLCV, pnlMasterProfileLCV, mode)
        End If
    End Sub

    Private Sub RenderProfilePanel(ByVal ID As Integer, ByVal objGroup As ProfileGroup, ByVal objPanel As Panel, ByVal mode As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(mode)
        objRenderPanel.GeneratePanel(ID, objPanel, objGroup, EnumProfileType.ProfileType.CHASSISMASTER, User)
        GetRenderControl()
        ManageDDLControl(objPanel)
    End Sub

    Private Sub BtnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTutup.Click
        Dim url As String = ""
        If CType(Session("PREVPAGE"), String).Contains("FrmPengajuanDummyFaktur.aspx") Then
            Dim str() As String = CType(Session("PREVPAGE"), String).Split("?")
            str(1) = "guid=" & Request.QueryString("guid") & "&GridPageCount=" & Request.QueryString("GridPageCount")

            url = str(0) & "?" & str(1)
        Else
            url = CType(Session("PREVPAGE"), String)
        End If

        Response.Redirect(url)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim blnResult As Boolean = False
        Dim strProfile As String
        Dim pnl As Panel

        Dim list As ArrayList = GetListChassisID()
        For Each id As Integer In list
            Dim objfacade As ChassisMasterProfileFacade
            Dim objChasisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)
            If objChasisMaster.ID > 0 Then
                If objChasisMaster.Category.CategoryCode = "CV" Then
                    isCV = True
                End If
                If objChasisMaster.Category.CategoryCode = "LCV" Then
                    isLCV = True
                End If
                If objChasisMaster.Category.CategoryCode = "PC" Then
                    isPC = True
                End If
            End If
        Next

        Dim objGroupProfileCV As ProfileGroup = New ProfileGroupFacade(User).Retrieve(CVGroup)
        Dim objGroupProfilePC As ProfileGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)
        Dim objGroupProfileLCV As ProfileGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)

        If objGroupProfileCV.ID > 0 And isCV Then
            strProfile = "CV"
            _objChassisMaster = GetChassisMaster(strProfile)
            pnl = pnlMasterProfileCV
            blnResult = FunctionSaveBaseOnPanel(pnl)
        End If
        If objGroupProfilePC.ID > 0 And isPC Then
            strProfile = "PC"
            _objChassisMaster = GetChassisMaster(strProfile)
            pnl = pnlMasterProfileLCV
            blnResult = FunctionSaveBaseOnPanel(pnl)
        End If
        If objGroupProfileLCV.ID > 0 And isLCV Then
            strProfile = "LCV"
            _objChassisMaster = GetChassisMaster(strProfile)
            pnl = pnlMasterProfilePC
            blnResult = FunctionSaveBaseOnPanel(pnl)
        End If

        If Not String.IsNullorEmpty(Request.QueryString("SecondCategory")) Then
            Dim cat As String = ""
            Dim guid As String = Request.QueryString("guid")
            Dim SecondCategory As String = Request.QueryString("SecondCategory")
            Response.Redirect("FrmMasterProfiles.aspx?Cat=" & cat & "&adsfadfadfw32342412412412424=1&kjhadshkf9784323247832493ihdufiue=17&hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423=" & SecondCategory &
                              "&guid=" & guid & "&SecondCategory=" & "", False)
        Else
            If Not String.IsNullorEmpty(Request.QueryString("guid")) Then
                BtnTutup_Click(Nothing, Nothing)
            End If
        End If


    End Sub

    Private Function FunctionSaveBaseOnPanel(ByVal pnl As Panel)

        Dim ddlJenis As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Try
            Dim ddlLeasing As DropDownList = pnl.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))

            If ddlLeasing.SelectedIndex = 0 Then
                Dim oPHFac As New ProfileHeaderFacade(User)
                Dim oPHPayment As ProfileHeader = oPHFac.Retrieve("CBU_WAYPAID1")

                Dim oPG As ProfileGroup
                Dim oPGFac As New ProfileGroupFacade(User)
                oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
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

        Catch ex As Exception
            Return False
        End Try

        _objChassisMaster.VehicleKind = New VehicleKind(CType(ddlModel.SelectedValue, Integer))

        Try
            If Not _isRevisionFaktur Then
                Dim oCMFac As New ChassisMasterFacade(User)
                If oCMFac.Update(_objChassisMaster) > 0 Then
                    SaveProfile(CType(ddlModel.SelectedValue, Integer))
                End If
            Else
                SaveRevisionProfile()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Return True
    End Function

    Private Sub btnBindModel_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal IsNotFromClient As Boolean = False) Handles btnBindModel.Click
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
        Me.BindDDLModel(objPanel, IsNotFromClient)
    End Sub

    Private Sub SaveProfile(Optional ByVal VehicleKindID As Integer = 0)
        Dim objRenderPanel As RenderingProfile
        Dim list As ArrayList = GetListChassisID()
        Dim errMCP As String = String.Empty
        For Each id As Integer In list
            'Dim profileList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(GroupCode), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)

            'objRenderPanel = New RenderingProfile
            'Dim PCList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(PCGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
            'objRenderPanel = New RenderingProfile
            'Dim CVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(CVGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
            'objRenderPanel = New RenderingProfile
            'Dim LCVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(LCVGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)

            Dim objfacade As ChassisMasterProfileFacade
            Dim objChasisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)
            If (VehicleKindID > 0) Then
                objChasisMaster.VehicleKind = New VehicleKind(VehicleKindID)
            End If

            'objChasisMaster.LastUpdateProfile = Now
            If objChasisMaster.ID > 0 Then
                If objChasisMaster.Category.CategoryCode = "CV" Then
                    objRenderPanel = New RenderingProfile
                    Dim CVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(CVGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)

                    'remarked by anh 20150827 
                    'For Each item As ChassisMasterProfile In CVList
                    '    item.ChassisMaster = objChasisMaster
                    'Next
                    'objfacade = New ChassisMasterProfileFacade(User)
                    'objfacade.Update(CVList, objChasisMaster)

                    'added by anh 20150827 'add mcp validation
                    Dim isMCP_Allowed As Boolean = True
                    For Each item As ChassisMasterProfile In CVList
                        If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 5) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            If Not objChasisMaster.EndCustomer.Customer Is Nothing AndAlso Not objChasisMaster.EndCustomer.Customer.MyCustomerRequest Is Nothing Then
                                If objChasisMaster.EndCustomer.Customer.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                                    If objChasisMaster.EndCustomer.MCPHeader Is Nothing Then
                                        isMCP_Allowed = False
                                    End If
                                End If
                            End If
                        End If
                        item.ChassisMaster = objChasisMaster
                        If item.ProfileHeader.ID = 44 Then
                            item.ProfileValue = objChasisMaster.VehicleKind.Code
                        End If
                    Next

                    If isMCP_Allowed Then
                        objfacade = New ChassisMasterProfileFacade(User)
                        objfacade.Update(CVList, objChasisMaster)
                    Else
                        errMCP = "Customer terdeteksi MCP. Update profile gagal. Silahkan tutup dan kembali ke pengajuan faktur untuk masukkan nomor MCP"
                        'MessageBox.Show("Customer terdeteksi MCP. Update profile gagal." + vbNewLine + "Silahkan tutup dan kembali ke pengajuan faktur untuk masukkan nomor MCP")
                        '_sesshelper.SetSession("IsSucceedProfileFaktur", 0)
                        Exit For
                    End If

                End If
                If objChasisMaster.Category.CategoryCode = "LCV" Then
                    objRenderPanel = New RenderingProfile
                    Dim LCVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(LCVGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)

                    For Each item As ChassisMasterProfile In LCVList
                        item.ChassisMaster = objChasisMaster
                        If item.ProfileHeader.ID = 44 Then
                            item.ProfileValue = objChasisMaster.VehicleKind.Code
                        End If
                    Next
                    objfacade = New ChassisMasterProfileFacade(User)
                    objfacade.Update(LCVList, objChasisMaster)
                End If
                If objChasisMaster.Category.CategoryCode = "PC" Then
                    objRenderPanel = New RenderingProfile
                    Dim PCList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(PCGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
                    For Each item As ChassisMasterProfile In PCList
                        item.ChassisMaster = objChasisMaster
                        If item.ProfileHeader.ID = 44 Then
                            item.ProfileValue = objChasisMaster.VehicleKind.Code
                        End If
                    Next
                    objfacade = New ChassisMasterProfileFacade(User)
                    objfacade.Update(PCList, objChasisMaster)
                End If
            End If
        Next
        If errMCP = String.Empty Then
            If CType(Session("PREVPAGE"), String).Contains("FrmPengajuanFaktur.aspx") Then
                refreshSeason()
            End If
            MessageBox.Show("Update Profile Berhasil.")
        Else
            _sesshelper.SetSession("IsSucceedProfileFaktur", 0)
            MessageBox.Show(errMCP)
        End If


    End Sub

    Private Sub refreshSeason()
        Try
            Dim list As ArrayList = GetListChassisID()
            Dim sessCM As ArrayList = CType(Session("sessCM"), ArrayList)
            Dim idx As Integer = 0
            For Each id As Integer In list
                For Each item As ChassisMaster In sessCM
                    If item.ID = id Then
                        sessCM.Item(idx) = New ChassisMasterFacade(User).Retrieve(id)
                    End If
                    idx = idx + 1
                Next
            Next
            Session("sessCM") = sessCM
        Catch
        End Try
    End Sub

    Private Sub SaveRevisionProfile()
        Dim objRenderPanel As RenderingProfile
        Dim list As ArrayList = GetListChassisID()
        Dim errMCP As String = String.Empty
        For Each id As Integer In list

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "EndCustomer.ID", MatchType.Exact, _objRevisionFaktur.EndCustomer.ID))
            Dim objListRevisionFaktur As ArrayList = New RevisionFakturFacade(User).Retrieve(criterias)
            If objListRevisionFaktur.Count > 0 Then
                Dim objfacade As RevisionChassisMasterProfileFacade = New RevisionChassisMasterProfileFacade(User)
                Dim objRevisionFaktur As RevisionFaktur = CType(objListRevisionFaktur(0), RevisionFaktur)
                Dim objChasisMaster As ChassisMaster = objRevisionFaktur.ChassisMaster
                Dim vkGFacade As VehicleKindGroupFacade = New VehicleKindGroupFacade(User)
                Dim vkFacade As VehicleKindFacade = New VehicleKindFacade(User)

                If objChasisMaster.Category.CategoryCode = "CV" Then
                    objRenderPanel = New RenderingProfile
                    Dim CVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(CVGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
                    Dim isMCP_Allowed As Boolean = True
                    Dim RevCVList As ArrayList = New ArrayList

                    For Each item As ChassisMasterProfile In CVList
                        If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 5) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            If Not objRevisionFaktur.EndCustomer.Customer Is Nothing AndAlso Not objRevisionFaktur.EndCustomer.Customer.MyCustomerRequest Is Nothing Then
                                If objRevisionFaktur.EndCustomer.Customer.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                                    If objRevisionFaktur.EndCustomer.MCPHeader Is Nothing Then
                                        isMCP_Allowed = False
                                    End If
                                End If
                            End If
                        End If
                        Dim revItem As RevisionChassisMasterProfile = New RevisionChassisMasterProfile
                        revItem.ChassisMaster = objChasisMaster
                        revItem.EndCustomer = objRevisionFaktur.EndCustomer
                        revItem.ProfileHeader = item.ProfileHeader
                        revItem.ProfileGroup = item.ProfileGroup
                        If item.ProfileHeader.Code.Equals("CBU_MODELKEND") Then
                            If Not String.IsNullorEmpty(item.ProfileValue) Then
                                revItem.ProfileValue = vkFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            Else
                                revItem.ProfileValue = item.ProfileValue
                            End If
                        ElseIf item.ProfileHeader.Code.Equals("CBU_JENISKEND") Then
                            If Not String.IsNullorEmpty(item.ProfileValue) Then
                                revItem.ProfileValue = vkGFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            Else
                                revItem.ProfileValue = item.ProfileValue
                            End If
                        Else
                            revItem.ProfileValue = item.ProfileValue
                        End If
                        RevCVList.Add(revItem)
                    Next

                    If isMCP_Allowed Then
                        objfacade.Update(RevCVList, objRevisionFaktur)
                    Else
                        errMCP = "Customer terdeteksi MCP. Update profile gagal. Silahkan tutup dan kembali ke pengajuan faktur untuk masukkan nomor MCP"
                        Exit For
                    End If
                End If
                If objChasisMaster.Category.CategoryCode = "LCV" Then
                    objRenderPanel = New RenderingProfile
                    Dim RevLCVList As ArrayList = New ArrayList
                    Dim LCVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(LCVGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)

                    For Each item As ChassisMasterProfile In LCVList
                        Dim revItem As RevisionChassisMasterProfile = New RevisionChassisMasterProfile
                        revItem.ChassisMaster = objChasisMaster
                        revItem.EndCustomer = objRevisionFaktur.EndCustomer
                        revItem.ProfileHeader = item.ProfileHeader
                        revItem.ProfileGroup = item.ProfileGroup
                        If item.ProfileHeader.Code.Equals("CBU_MODELKEND") Then
                            If Not String.IsNullorEmpty(item.ProfileValue) Then
                                revItem.ProfileValue = vkFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            Else
                                revItem.ProfileValue = item.ProfileValue
                            End If
                        ElseIf item.ProfileHeader.Code.Equals("CBU_JENISKEND") Then
                            If Not String.IsNullorEmpty(item.ProfileValue) Then
                                revItem.ProfileValue = vkGFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            Else
                                revItem.ProfileValue = item.ProfileValue
                            End If
                        Else
                            revItem.ProfileValue = item.ProfileValue
                        End If
                        RevLCVList.Add(revItem)
                    Next
                    objfacade.Update(RevLCVList, objRevisionFaktur)
                End If
                If objChasisMaster.Category.CategoryCode = "PC" Then
                    objRenderPanel = New RenderingProfile
                    Dim RevLCVList As ArrayList = New ArrayList
                    Dim PCList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(PCGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
                    For Each item As ChassisMasterProfile In PCList
                        Dim revItem As RevisionChassisMasterProfile = New RevisionChassisMasterProfile
                        revItem.ChassisMaster = objChasisMaster
                        revItem.EndCustomer = objRevisionFaktur.EndCustomer
                        revItem.ProfileHeader = item.ProfileHeader
                        revItem.ProfileGroup = item.ProfileGroup
                        If item.ProfileHeader.Code.Equals("CBU_MODELKEND") Then
                            If Not String.IsNullorEmpty(item.ProfileValue) Then
                                revItem.ProfileValue = vkFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            Else
                                revItem.ProfileValue = item.ProfileValue
                            End If
                        ElseIf item.ProfileHeader.Code.Equals("CBU_JENISKEND") Then
                            If Not String.IsNullorEmpty(item.ProfileValue) Then
                                revItem.ProfileValue = vkGFacade.Retrieve(CType(item.ProfileValue, Integer)).Code
                            Else
                                revItem.ProfileValue = item.ProfileValue
                            End If
                        Else
                            revItem.ProfileValue = item.ProfileValue
                        End If
                        RevLCVList.Add(revItem)
                    Next
                    objfacade.Update(RevLCVList, objRevisionFaktur)
                End If
            End If
        Next
        If errMCP = String.Empty Then
            MessageBox.Show("Update Profile Berhasil.")
        Else
            _sesshelper.SetSession("IsSucceedProfileFaktur", 0)
            MessageBox.Show(errMCP)
        End If
    End Sub

    Protected Sub btnDisableLeasing_Click(sender As Object, e As EventArgs) Handles btnDisableLeasing.Click
        Dim ddlPayment As DropDownList = GetDDLProfile("CBU_WAYPAID1")
        Dim ddlLeasing As DropDownList

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

        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
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

    Private Function GetProfileValue(ByVal oPH As ProfileHeader) As String
        Dim objGroup As ProfileGroup
        Dim strProfileValue As String = ""

        Select Case _objChassisMaster.Category.CategoryCode
            Case "PC"
                objGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)
            Case "CV"
                objGroup = New ProfileGroupFacade(User).Retrieve(CVGroup)
            Case "LCV"
                objGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)
        End Select

        If Not _isRevisionFaktur Then
            Dim objFacade As ChassisMasterProfileFacade = New ChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
            Dim objListSPKProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListSPKProfile.Count > 0 Then
                strProfileValue = CType(objListSPKProfile(0), ChassisMasterProfile).ProfileValue
            End If
        Else
            Dim objFacade As RevisionChassisMasterProfileFacade = New RevisionChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "EndCustomer.ID", MatchType.Exact, _objRevisionFaktur.EndCustomer.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
            Dim objListRevisionChassisMasterProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListRevisionChassisMasterProfile.Count > 0 Then
                strProfileValue = CType(objListRevisionChassisMasterProfile(0), RevisionChassisMasterProfile).ProfileValue
            End If
        End If

        Return strProfileValue
    End Function
End Class
