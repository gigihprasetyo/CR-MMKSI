Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.SAP

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class PopUpAddVehicleType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgSAPCustomer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sHelper As SessionHelper = New SessionHelper


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            BindDataGrid()
            dgSAPCustomer.ShowFooter = True
        End If
    End Sub
    Private Sub BindDataGrid()     
        dgSAPCustomer.DataSource = New SAPCustomerFacade(User).Retrieve(CriteriaSearch())
        dgSAPCustomer.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim arlSAPCustomer As New ArrayList

        If Not sHelper.GetSession("arlSAPCustomer") Is Nothing Then
            arlSAPCustomer = sHelper.GetSession("arlSAPCustomer")
        End If

        Dim id As Integer = CInt(Request.QueryString("id"))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomer), "ID", MatchType.Exact, id))

        If arlSAPCustomer.Count > 0 Then          
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "CustomerCode", MatchType.Exact, arlSAPCustomer(1).ToString()))
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.Exact, CByte(arlSAPCustomer(4))))
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Exact, CDate(arlSAPCustomer(5))))
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, -1))
        End If

        Return criterias
    End Function

    Private Sub dgSAPCustomer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSAPCustomer.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSAPCustomer As SAPCustomer = e.Item.DataItem            
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSAPCustomer.CurrentPageIndex * dgSAPCustomer.PageSize)
            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                ' mengisi value
                            
                Dim lblVechileTypeCode As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                lblVechileTypeCode.Text = objSAPCustomer.VechileType.VechileTypeCode

                Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
                lblDescription.Text = objSAPCustomer.VechileType.Description

                Dim lblQty As Label = CType(e.Item.FindControl("lblQty"), Label)
                lblQty.Text = objSAPCustomer.Qty.ToString()              
            End If

            ' untuk bagian edit item
            If e.Item.ItemType = ListItemType.EditItem Then
              
                Dim lbtnSaveNew As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                lbtnSaveNew.CommandArgument = objSAPCustomer.ID

                Dim txtEditVechileTypeCode As TextBox = CType(e.Item.FindControl("txtEditVechileTypeCode"), TextBox)
                txtEditVechileTypeCode.Text = objSAPCustomer.VechileType.VechileTypeCode

                Dim txtEditDescription As TextBox = CType(e.Item.FindControl("txtEditDescription"), TextBox)
                txtEditDescription.Text = objSAPCustomer.VechileType.Description

                Dim txtEditQty As TextBox = CType(e.Item.FindControl("txtEditQty"), TextBox)
                txtEditQty.Text = objSAPCustomer.Qty

            End If
        End If

        ' untuk bagian footer
        If e.Item.ItemType = ListItemType.Footer Then

            Dim txtAddVechileTypeCode As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
            txtAddVechileTypeCode.Text = ""

            Dim txtAddDescription As TextBox = CType(e.Item.FindControl("txtAddDescription"), TextBox)
            txtAddDescription.Text = ""

            Dim txtAddQty As TextBox = CType(e.Item.FindControl("txtAddQty"), TextBox)
            txtAddQty.Text = ""
            
        End If
    End Sub

    Private Sub dgSAPCustomer_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSAPCustomer.ItemCommand
        Dim facade As SAPCustomerFacade = New SAPCustomerFacade(User)

        If e.CommandName = "Add" Then

            ' start check validation #########################
        
            Dim txtAddVechileTypeCode As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
            Dim txtAddDescription As TextBox = CType(e.Item.FindControl("txtAddDescription"), TextBox)
            Dim txtAddQty As TextBox = CType(e.Item.FindControl("txtAddQty"), TextBox)
            
            If txtAddVechileTypeCode.Text = "" Then
                MessageBox.Show("VechileTypeCode harus diisi dahulu !")
                Return
            End If

            If txtAddDescription.Text = "" Then
                MessageBox.Show("Description harus diisi dahulu !")
                Return
            End If

            If Val(txtAddQty.Text) = 0 Then
                MessageBox.Show("Silakan isi qty terlebih dahulu !")
                Return
            End If

            Dim objSAPCustomer As SAPCustomer = New SAPCustomer
            Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtAddVechileTypeCode.Text.Trim)

            If objVechileType.ID > 0 Then
                MessageBox.Show("Tipe kendaraan harus diisi dengan data yang valid, gunakan pop up !")
                Return
            End If

            Dim arlSAPCustomer As New ArrayList

            If Not sHelper.GetSession("arlSAPCustomer") Is Nothing Then
                arlSAPCustomer = sHelper.GetSession("arlSAPCustomer")
            End If

            If arlSAPCustomer.Count > 0 Then

                objSAPCustomer.SalesmanHeader = arlSAPCustomer(0)
                objsapcustomer.CustomerCode = arlSAPCustomer(1).ToString()
                objSAPCustomer.Qty = Val(txtAddQty.Text)
                objSAPCustomer.VechileType = objVechileType
                objSAPCustomer.CustomerName = arlSAPCustomer(2).ToString()
                objSAPCustomer.CustomerAddress = arlSAPCustomer(3).ToString()
                objSAPCustomer.Status = CByte(arlSAPCustomer(4))
                objSAPCustomer.ProspectDate = CDate(arlSAPCustomer(5))
                Dim result As Integer = facade.Insert(objSAPCustomer)

                If result = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            End If
        End If
    End Sub    
End Class
