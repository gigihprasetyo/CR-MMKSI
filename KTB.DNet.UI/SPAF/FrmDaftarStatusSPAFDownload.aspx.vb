#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SPAF
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class FrmDaftarStatusSPAFDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CustomValidator1 As System.Web.UI.WebControls.CustomValidator
    Protected WithEvents dtgSPAF As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblLeasing As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodePersetujuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label

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
    Private _ListSPAF As ArrayList
    Private _ListSPAFDoc As ArrayList
    Private sessionHelper As New sessionHelper
#End Region

#Region "Custom Method"
    Private Sub SetSPAFOrSubsidi()
        If Request.QueryString("DocType") = EnumSPAFSubsidy.DocumentType.SPAF Then
            lblTitle.Text = EnumSPAFSubsidy.DocumentType.SPAF.ToString
            dtgSPAF.Columns(11).HeaderText = "SPAF per Unit"
            dtgSPAF.Columns(13).HeaderText = "SPAF setelah PPh"
        Else
            lblTitle.Text = EnumSPAFSubsidy.DocumentType.Subsidi.ToString
            dtgSPAF.Columns(11).HeaderText = "Subsidi per Unit"
            dtgSPAF.Columns(13).HeaderText = "Subsidi setelah PPh"
        End If
    End Sub
    Private ReadOnly Property GetIdIn() As String()
        Get
            Return Request.QueryString("idin").Split(",")
        End Get
    End Property
    Private Sub BindTodtgSPAF()
        _ListSPAF = CType(sessionHelper.GetSession("SPAFDOC"), ArrayList)
        Dim newList As New ArrayList
        For Each sp As V_LeasingDaftarDokumen In _ListSPAF
            For Each id As String In GetIdIn
                If sp.ID = id Then
                    newList.Add(sp)
                    Exit For
                End If
            Next
        Next
        dtgSPAF.DataSource = newList
        dtgSPAF.DataBind()
    End Sub

#End Region

#Region "EventHandler"


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            SetTitle()
            SetSPAFOrSubsidi()
            BindTodtgSPAF()
            InitiatePage()
        End If
    End Sub

    Private Sub SetTitle()
        lblPeriodePersetujuan.Text = Request.QueryString("Periode")
    End Sub

    Private Sub InitiatePage()
        Response.Clear()
        'Response.Buffer = True
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)
        Me.Controls.Clear() 'ClearControls(dgUploadData)
        dtgSPAF.RenderControl(oHtmlTextWriter)
        Response.Write(oStringWriter.ToString())

        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("SPAF.xls").Append("""").ToString()
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Sub


#End Region

    Private Sub dtgSPAF_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPAF.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim objSPAFDoc As V_LeasingDaftarDokumen
            objSPAFDoc = CType(_ListSPAF(e.Item.ItemIndex), V_LeasingDaftarDokumen)
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgSPAF.PageSize * dtgSPAF.CurrentPageIndex)).ToString
            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(objSPAFDoc.OrderDealer)
            lblKodeDealer.Text = objDealer.DealerCode & " " & objDealer.DealerName
        End If
    End Sub
End Class