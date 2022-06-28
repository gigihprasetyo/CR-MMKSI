#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Collections.Generic

#End Region

Public Class PopUpChangesResigneDate
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private ReadOnly Property salesmanHeaderID As Integer
        Get
            Try
                Return CInt(Request.QueryString("shID"))
            Catch
                Return 0
            End Try
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            lblPageTitle.Text = "CS Team - Perubahan Pengunduran Diri"
            InitPage()
        End If
    End Sub

    Private Sub InitPage()
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(Me.User).Retrieve(Me.salesmanHeaderID)
        lblEmpID.Text = objSalesmanHeader.SalesmanCode
        lblName.Text = objSalesmanHeader.Name
        lblPosition.Text = CommonFunction.GetEnumDescription(objSalesmanHeader.JobPosition.ID, "EMP_POS_CS")
       
        icResignDate.Value = objSalesmanHeader.ResignDate
        txtResignReason.Text = objSalesmanHeader.ResignReason

    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            If Not Page.IsValid Then
                Return
            End If
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeader
            Dim objSalesmanHeaderFacade As New SalesmanHeaderFacade(User)
            Dim nResult As Integer = -1

            objSalesmanHeader = objSalesmanHeaderFacade.Retrieve(Me.salesmanHeaderID)
            objSalesmanHeader.ResignDate = icResignDate.Value
            objSalesmanHeader.ResignReason = txtResignReason.Text

            nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                lblErrorMessage.ForeColor = Color.Red
                lblErrorMessage.Text = "Simpan perubahan data gagal"
            Else
                lblErrorMessage.Text = "Simpan perubahan data berhasil"
            End If

            If objSalesmanHeader.JobPosition.Code = "CSO" Then
                AddSalesmanDealerHistory(objSalesmanHeader)
            End If

        Catch ex As Exception
            lblErrorMessage.Text = "Simpan perubahan data gagal"
            lblErrorMessage.ForeColor = Color.Red

        End Try
    End Sub
    Private Sub AddSalesmanDealerHistory(ByVal salesmanHeader As SalesmanHeader)
        Try
            Dim func As New SalesmanDealerHistoryFacade(Me.User)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanDealerHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "SalesmanHeader.ID", MatchType.Exact, salesmanHeader.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "Dealer.ID", MatchType.Exact, salesmanHeader.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "DateIn", MatchType.Exact, salesmanHeader.HireDate)) ' --pengajuan

            Dim arrList As ArrayList = func.Retrieve(criterias)
            Dim objDealerHist As New SalesmanDealerHistory
            If arrList.Count > 0 Then
                objDealerHist = arrList(0)
                objDealerHist.DateOut = salesmanHeader.ResignDate
                func.Update(objDealerHist)
            Else
                objDealerHist.SalesmanHeader = salesmanHeader
                objDealerHist.Dealer = salesmanHeader.Dealer
                objDealerHist.DateIn = salesmanHeader.HireDate
                objDealerHist.DateOut = salesmanHeader.ResignDate
                func.Insert(objDealerHist)
            End If

        Catch ex As Exception
            'MessageBox.Show("Update historikal gagal")
        End Try

    End Sub

End Class