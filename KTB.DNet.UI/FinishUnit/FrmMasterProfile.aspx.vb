Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Public Class FrmMasterProfile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents pnlMasterProfile As System.Web.UI.WebControls.Panel
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

    Private Function GetGroupCode() As String
        Dim cat As String = Request.QueryString.Item("Cat")
        Dim GroupCode As String
        If cat = "CV" Then
            GroupCode = "cust_prf_cv"
        Else
            If cat = "PC" Then
                GroupCode = "cust_prf_pc"
            Else
                If cat = "LCV" Then
                    GroupCode = "cust_prf_lcv"
                Else
                    GroupCode = ""
                End If
            End If
        End If
        Return GroupCode
    End Function

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
        Dim list As ArrayList = GetListChassisID()
        'Dim id As Integer = CInt(Request.QueryString.Item("hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423"))
        Dim Group As Integer = CInt(Request.QueryString.Item("kjhadshkf9784323247832493ihdufiue"))
        Dim mode As Integer = CInt(Request.QueryString.Item("adsfadfadfw32342412412412424"))
        Dim isReadOnly As Boolean
        Dim GroupCode As String
        If mode = 0 Then
            isReadOnly = True
            btnSimpan.Visible = False
        Else
            isReadOnly = False
            btnSimpan.Visible = True
        End If
        If GetGroupCode() <> "" Then
            GroupCode = GetGroupCode()
        Else
            MessageBox.Show("Kategori Chassis tidak terdaftar")
            Return
        End If

        Dim objGroupProfile As ProfileGroup = New ProfileGroupFacade(User).Retrieve(GroupCode)

        If objGroupProfile.ID > 0 Then
            Group = objGroupProfile.ID
            If list.Count > 1 Then
                LoadProfile(Nothing, objGroupProfile, False)
            Else
                LoadProfile(list(0), objGroupProfile, isReadOnly)
            End If
        Else
            MessageBox.Show("Profile Group tidak terdaftar")
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub LoadProfile(ByVal id As Integer, ByVal group As ProfileGroup, ByVal mode As Boolean)
        RenderProfilePanel(New ChassisMaster(id), group, pnlMasterProfile, mode)
    End Sub

    Private Sub RenderProfilePanel(ByVal objChassisMaster As ChassisMaster, ByVal objGroup As ProfileGroup, ByVal objPanel As Panel, ByVal mode As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(mode)
        objRenderPanel.GeneratePanel(objChassisMaster.ID, objPanel, objGroup, EnumProfileType.ProfileType.CHASSISMASTER, User)
    End Sub

    Private Sub BtnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTutup.Click
        Dim url As String = CType(Session("PREVPAGE"), String)
        Response.Redirect(url)
        'Response.Redirect("FrmChassisMasterProfile.aspx")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        SaveProfile()
    End Sub

    Private Sub SaveProfile()
        Dim objRenderPanel As RenderingProfile
        objRenderPanel = New RenderingProfile
        Dim GroupCode As String = GetGroupCode()
        If GroupCode = "" Then
            MessageBox.Show("Kategori Chassis tidak terdaftar")
            Return
        Else
            GroupCode = GetGroupCode()
        End If
        Dim list As ArrayList = GetListChassisID()
        For Each id As Integer In list
            Dim profileList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(GroupCode), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
            Dim objfacade As ChassisMasterProfileFacade = New ChassisMasterProfileFacade(User)
            Dim objChasisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)
            For Each item As ChassisMasterProfile In profileList
                item.ChassisMaster = objChasisMaster
                item.CreatedTime = Date.Now
                item.CreatedBy = CType(Session("LOGINUSERINFO"), UserInfo).UserName
                item.LastUpdateTime = Date.Now
                item.LastUpdateBy = CType(Session("LOGINUSERINFO"), UserInfo).UserName
            Next
            objfacade.Update(profileList, objChasisMaster)
        Next
      
    End Sub
End Class
