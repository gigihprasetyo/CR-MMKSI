#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO

#End Region


Public Class PopUpFreeServicePartDetail
    Inherits System.Web.UI.Page

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FSID As Integer = CType(Request.QueryString("FSID"), Integer)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeServicePartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServicePartDetail), "FreeService.ID", MatchType.Exact, FSID))

        Dim arrFSPartDetail As ArrayList = New FreeServicePartDetailFacade(User).Retrieve(criterias)
        'If arrFSPartDetail.Count > 0 Then

        'End If
        dtgFreeServiceDetail.DataSource = arrFSPartDetail
        dtgFreeServiceDetail.DataBind()
    End Sub
#End Region

#Region "Custom Method"

#End Region
    
End Class