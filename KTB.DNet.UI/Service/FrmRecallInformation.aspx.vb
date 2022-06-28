#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
#End Region

Public Class FrmRecallInformation
    Inherits System.Web.UI.Page

#Region "variable"
    Private ReadOnly varSession As String = "sessfrmPresentationList"
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
    Dim IsKTB As Boolean
#End Region

#Region "Function"

    Private Sub CheckPriv()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If IsNothing(objDealer) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Campaign - Informasi Kendaraan")
        End If

        If Not SecurityProvider.Authorize(Context.User, SR.InformasiKendaraanView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Campaign - Informasi Kendaraan")
        End If


        Dim LastIdx As Integer = dtgServiceData.Columns.Count - 1
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            dtgServiceData.Columns(LastIdx).Visible = True
            lblNoEngine.Visible = True
            lblNoEngineColon.Visible = True
            lblNoEngineTitle.Visible = True
        Else
            dtgServiceData.Columns(LastIdx).Visible = False
            lblNoEngine.Visible = False
            lblNoEngineColon.Visible = False
            lblNoEngineTitle.Visible = False
        End If

    End Sub

     

    Private Sub Searchdata()
     


        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Partial, Me.txtChassisNumber.Text.Trim()))

        Dim ObjChassis As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)

        If ObjChassis.Count <= 0 Then

            criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Partial, Me.txtChassisNumber.Text.Trim()))
            ObjChassis = New ChassisMasterBBFacade(User).Retrieve(criterias)

            Me.dtgServiceData.DataSource = New ArrayList
            Me.dtgServiceData.DataBind()

            If Not IsNothing(ObjChassis) AndAlso ObjChassis.Count > 0 AndAlso Not IsNothing(CType(ObjChassis(0), ChassisMasterBB).VechileColor) Then

                If Not IsNothing(CType(ObjChassis(0), ChassisMasterBB).VechileColor) Then
                    lblMaterial.Text = CType(ObjChassis(0), ChassisMasterBB).VechileColor.MaterialNumber & " - " & CType(ObjChassis(0), ChassisMasterBB).VechileColor.MaterialDescription
                End If


                lblNoChassis.Text = CType(ObjChassis(0), ChassisMasterBB).ChassisNumber
                lblNoEngine.Text = CType(ObjChassis(0), ChassisMasterBB).EngineNumber
                If Not IsNothing(CType(ObjChassis(0), ChassisMasterBB).Dealer) Then
                    Me.lblDealerSold.Text = CType(ObjChassis(0), ChassisMasterBB).Dealer.DealerCode
                Else
                    Me.lblDealerSold.Text = ""
                End If

                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ChassisMasterBB.ID", MatchType.Exact, CType(ObjChassis(0), ChassisMasterBB).ID))
                Dim sparepartColl As ArrayList = New RecallServiceFacade(User).Retrieve(criterias)

                If Not IsNothing(sparepartColl) Then
                    Me.dtgServiceData.DataSource = sparepartColl
                    Me.dtgServiceData.DataBind()
                Else
                    Me.dtgServiceData.DataSource = New ArrayList
                    Me.dtgServiceData.DataBind()

                End If

                Dim strQuery As String = "(SELECT a.[RecallCategoryID] FROM [dbo].[RecallChassisMaster] a WHERE a.[RowStatus]=0 AND [a].[ChassisNo]='{0}')"
                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "ID", MatchType.InSet, String.Format(strQuery, lblNoChassis.Text.Trim)))
                Dim dts As New ArrayList

                dts = New RecallCategoryFacade(User).Retrieve(criterias)
                ltrListRCM.Text = ""
                If Not IsNothing(dts) AndAlso dts.Count > 0 Then

                    Dim strHTML As String = ""
                    For Each dtt As RecallCategory In dts
                        strHTML = strHTML & "<span>" & dtt.RecallRegNo & " / " & dtt.BuletinDescription & "</span></br>"
                    Next

                    ltrListRCM.Text = strHTML
                End If

            Else
                Me.dtgServiceData.DataSource = New ArrayList
                Me.dtgServiceData.DataBind()
                lblNoChassis.Text = ""
                Me.lblDealerSold.Text = ""
                lblNoChassis.Text = ""
                lblNoEngine.Text = ""
                lblMaterial.Text = ""
                ltrListRCM.Text = ""

                'MessageBox.Show("Chassis Tidak Terdaftar")
                'Return
            End If

        Else
            Me.dtgServiceData.DataSource = New ArrayList
            Me.dtgServiceData.DataBind()

            If Not IsNothing(ObjChassis) AndAlso ObjChassis.Count > 0 AndAlso Not IsNothing(CType(ObjChassis(0), ChassisMaster).VechileColor) Then

                If Not IsNothing(CType(ObjChassis(0), ChassisMaster).VechileColor) Then
                    lblMaterial.Text = CType(ObjChassis(0), ChassisMaster).VechileColor.MaterialNumber & " - " & CType(ObjChassis(0), ChassisMaster).VechileColor.MaterialDescription
                End If


                lblNoChassis.Text = CType(ObjChassis(0), ChassisMaster).ChassisNumber
                lblNoEngine.Text = CType(ObjChassis(0), ChassisMaster).EngineNumber
                If Not IsNothing(CType(ObjChassis(0), ChassisMaster).Dealer) Then
                    Me.lblDealerSold.Text = CType(ObjChassis(0), ChassisMaster).Dealer.DealerCode
                Else
                    Me.lblDealerSold.Text = ""
                End If

                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ChassisMaster.ID", MatchType.Exact, CType(ObjChassis(0), ChassisMaster).ID))
                Dim sparepartColl As ArrayList = New RecallServiceFacade(User).Retrieve(criterias)

                If Not IsNothing(sparepartColl) Then
                    Me.dtgServiceData.DataSource = sparepartColl
                    Me.dtgServiceData.DataBind()
                Else
                    Me.dtgServiceData.DataSource = New ArrayList
                    Me.dtgServiceData.DataBind()

                End If

                Dim strQuery As String = "(SELECT a.[RecallCategoryID] FROM [dbo].[RecallChassisMaster] a WHERE a.[RowStatus]=0 AND [a].[ChassisNo]='{0}')"
                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "ID", MatchType.InSet, String.Format(strQuery, lblNoChassis.Text.Trim)))
                Dim dts As New ArrayList

                dts = New RecallCategoryFacade(User).Retrieve(criterias)
                ltrListRCM.Text = ""
                If Not IsNothing(dts) AndAlso dts.Count > 0 Then

                    Dim strHTML As String = ""
                    For Each dtt As RecallCategory In dts
                        strHTML = strHTML & "<span>" & dtt.RecallRegNo & " / " & dtt.BuletinDescription & "</span></br>"
                    Next

                    ltrListRCM.Text = strHTML
                End If

            Else
                Me.dtgServiceData.DataSource = New ArrayList
                Me.dtgServiceData.DataBind()
                lblNoChassis.Text = ""
                Me.lblDealerSold.Text = ""
                lblNoChassis.Text = ""
                lblNoEngine.Text = ""
                lblMaterial.Text = ""
                ltrListRCM.Text = ""

                'MessageBox.Show("Chassis Tidak Terdaftar")
                'Return
            End If

        End If

        


        

       

        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallChassisMaster), "ChassisNo", MatchType.Exact, txtChassisNumber.Text.Trim()))
        Dim CCC As ArrayList = New RecallChassisMasterFacade(User).Retrieve(criterias)

        If (Not IsNothing(CCC) AndAlso CCC.Count > 0) Then

        Else
            Me.dtgServiceData.DataSource = New ArrayList
            Me.dtgServiceData.DataBind()
            lblNoChassis.Text = ""
            Me.lblDealerSold.Text = ""
            lblNoChassis.Text = ""
            lblNoEngine.Text = ""
            lblMaterial.Text = ""
            MessageBox.Show("Tidak termasuk ke Dalam Field Fix Campaign")
        End If

    End Sub

    Private Sub CommandDelete(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try


            Dim ObjPresentattionFa As RecallServiceFacade = New RecallServiceFacade(User)
            Dim ObjPresentation As New RecallService
            ObjPresentation = ObjPresentattionFa.Retrieve(CInt(e.Item.Cells(0).Text))

            ObjPresentation.RowStatus = DBRowStatus.Deleted
            ObjPresentattionFa.Update(ObjPresentation)
            Searchdata()
            MessageBox.Show(SR.DeleteSucces)
            'BindDataGridMember(0)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try


    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPriv()

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Not Page.IsValid Then
            Return
        End If
        Searchdata()
    End Sub

    Private Sub dtgServiceData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceData.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgServiceData.CurrentPageIndex * dtgServiceData.PageSize)
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As RecallService = CType(e.Item.DataItem, RecallService)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)

                If Not IsNothing(RowValue.Dealer) Then
                    lblDealer.Text = RowValue.Dealer.DealerCode & " - " & RowValue.Dealer.SearchTerm1
                End If


                Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)
                If Not IsNothing(RowValue.CreatedTime) Then
                    If RowValue.ServiceDate <= "1/1/1900" Then
                        lblTglPro.Text = ""
                    Else
                        lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
                    End If
                End If

                Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)

                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                    _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")


                End If



            End If
        End If
    End Sub

    Protected Sub dtgServiceData_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgServiceData.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "Delete".ToLower()
                CommandDelete(e)
        End Select
    End Sub
End Class