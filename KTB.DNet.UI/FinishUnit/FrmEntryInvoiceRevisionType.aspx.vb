
#Region "Custom Namespace Imports"

Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.Utility
Imports KTB.DNET.Security
Imports KTB.DNET.Domain.Search

#End Region

Public Class FrmEntryInvoiceRevisionType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        initForm()
    End Sub

    Private Sub InitiateAuthorization()

        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturInput_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=REVISI FAKTUR - Permohonan Revisi Faktur Kendaraan")
        End If
    End Sub

#End Region

#Region " Custom Variable Declaration "

    Private _sesshelper As SessionHelper = New SessionHelper
    Private _objChassisMaster As ChassisMaster
    Private _objDealer As Dealer
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            BindRevisionTypeDropDownList(ddlRevisionType)

            LoadPage()

            btnCancel.Visible = False
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmInvoiceRevision.aspx")
    End Sub

    Private Sub ddlRevisionPenalty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRevisionType.SelectedIndexChanged
        Dim mess As String = String.Empty
        If ddlRevisionType.SelectedValue = 2 AndAlso Not CommonFunction.RevFakturIsValidData(_objChassisMaster, mess) Then
            MessageBox.Show("Revisi tidak bisa dilakukan " & mess)
            Exit Sub
        End If

        If IsValidPrice() Then
            '-- Store the calling page
            _sesshelper.SetSession("FrmEntryInvoiceRevision_CalledBy", "FrmInvoiceRevision.aspx")

            Response.Redirect("FrmEntryInvoiceRevision.aspx?t=" & ddlRevisionType.SelectedValue & "&mode=" & EnumDNET.enumFormMode.Add)
        Else
            MessageBox.Show("Harga untuk revisi yang di pilih tidak valid.")
        End If

    End Sub

#Region " Custom Method "

    Private Sub initForm()
        _objChassisMaster = CType(GetFromSession("ChassisMaster"), ChassisMaster)

    End Sub

    Private Sub BindRevisionTypeDropDownList(ByRef objDropDownList As DropDownList)

        Dim objRevisionTypeFacade As RevisionTypeFacade = New RevisionTypeFacade(User)

        objDropDownList.DataSource = objRevisionTypeFacade.RetrieveActiveList()
        objDropDownList.DataTextField = "Description"
        objDropDownList.DataValueField = "ID"
        objDropDownList.DataBind()
        objDropDownList.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            'Todo session
            Return Session(sObject)
        End If
    End Function

    Private Sub LoadPage()
        _sesshelper.SetSession("disabledSave", False)
        _objDealer = CType(GetFromSession("DEALER"), Dealer)
    End Sub

    Private Function IsValidPrice() As Boolean
        Dim result As Boolean = False
        Dim objRevisionPriceFacade As RevisionPriceFacade = New RevisionPriceFacade(User)
        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(RevisionPrice), "Category.CategoryCode", MatchType.Exact, _objChassisMaster.Category.CategoryCode))
        criterias.opAnd(New Criteria(GetType(RevisionPrice), "RevisionType.ID", MatchType.Exact, ddlRevisionType.SelectedValue))
        criterias.opAnd(New Criteria(GetType(RevisionPrice), "ValidFrom", MatchType.LesserOrEqual, Now.AddDays(1)))
        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(RevisionPrice), "ValidFrom", Sort.SortDirection.DESC))

        Dim RevisionPriceList As ArrayList = New ArrayList
        RevisionPriceList = objRevisionPriceFacade.RetrieveByCriteria(criterias, sortColl)
        If RevisionPriceList.Count > 0 Then
            result = True
        End If

        Return result
    End Function
#End Region
End Class