#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports System.Drawing.Color
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Text
Imports System.IO

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Configuration
Imports System.Globalization

#End Region


Public Class FrmPODate
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lbldate As KTB.DNet.WebCC.IntiCalendar
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    'Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.POEOM_INPUT_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Setting Tanggal PO")
        End If
      
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            'untuk memfilter query 
            Dim criterias As New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, SR.PODateAllowed))
            'selalu new untuk inisiasi : belajar oop
            Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objappconfig As New AppConfig
            Dim objappconfiglist As ArrayList = objfacade.Retrieve(criterias)
            objappconfig = objappconfiglist(0)

            Try
                lbldate.Value = DateTime.ParseExact(objappconfig.Value, "yyyyMMdd", Nothing)
            Catch ex As Exception
            End Try

        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim srcDate As New DateTime(lbldate.Value.Year, lbldate.Value.Month, lbldate.Value.Day)

            Dim objappconfig As New AppConfig
            Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, SR.PODateAllowed))

            Dim objappconfiglist As ArrayList = objfacade.Retrieve(criterias)
            objappconfig = objappconfiglist(0)
            objappconfig.Value = srcDate.ToString("yyyyMMdd")
            objfacade.Update(objappconfig)
            MessageBox.Show(SR.UpdateSucces)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub


End Class