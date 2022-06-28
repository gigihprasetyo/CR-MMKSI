#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO

#End Region


Public Class PopUpFreeServiceReason
    Inherits System.Web.UI.Page

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID As Integer = CType(Request.QueryString("ID"), Integer)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeader), "ID", MatchType.Exact, ID))

        Dim arrFSPartDetail As ArrayList = New WSCHeaderFacade(User).Retrieve(criterias)
        'If arrFSPartDetail.Count > 0 Then

        'End If
        Dim notes As String = CType(arrFSPartDetail(0), WSCHeader).Notes
        txtNotes.Enabled = False
        txtNotes.Text = notes
    End Sub
#End Region

#Region "Custom Method"

#End Region

End Class