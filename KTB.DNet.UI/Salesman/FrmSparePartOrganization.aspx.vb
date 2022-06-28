Imports System.Text
#Region "Custom NameSpace"
Imports ktb.DNet.UI.Helper
Imports ktb.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
#End Region
Public Class FrmSparePartOrganization
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanID As System.Web.UI.WebControls.Label
    Protected WithEvents txtID As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadExcel As System.Web.UI.WebControls.Button
    Protected WithEvents ltStructure As System.Web.UI.WebControls.Literal
    Protected WithEvents valDealer As System.Web.UI.WebControls.RequiredFieldValidator
    'Protected WithEvents ltSales As System.Web.UI.WebControls.Literal
    'Protected WithEvents ltAdmin As System.Web.UI.WebControls.Literal
    'Protected WithEvents ltWarehouse As System.Web.UI.WebControls.Literal
    'Protected WithEvents ltManager As System.Web.UI.WebControls.Literal

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private declaration"
    Private sessHelper As New SessionHelper
    Private objDealer As Dealer
#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Lihat_organisasi_spare_part_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Part Employee - Organisasi Spare Part")
        End If
    End Sub

#End Region

#Region "Event handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        lblSearchDealer.Attributes("onclick") = "ShowDealerSelection();"
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If Not IsPostBack Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lblSearchDealer.Visible = True
            Else
                txtDealerCode.Text = objDealer.DealerCode.ToString.Trim
                lblNama.Text = objDealer.DealerName
                lblKota.Text = objDealer.City.CityName
                lblSearchDealer.Visible = False
                txtDealerCode.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtDealerCode.Text.Trim <> String.Empty Then
            objDealer = New DealerFacade(User).GetDealer(txtDealerCode.Text.Trim)
            If objDealer.ID > 0 Then
                lblNama.Text = objDealer.DealerName
                lblKota.Text = objDealer.City.CityName
                Dim arlDealerAdditional As ArrayList = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealer.ID)
                If arlDealerAdditional.Count > 0 Then
                    Dim oDealerAdditional As DealerAdditional = arlDealerAdditional(0)
                    If oDealerAdditional.SparePartGrade <> String.Empty Then
                        BindOrganizationStructure(objDealer)
                    Else
                        ltStructure.Text = String.Empty
                        MessageBox.Show("Grade dealer belum di tentukan")
                    End If
                Else
                    ltStructure.Text = String.Empty
                    MessageBox.Show("Grade dealer belum di tentukan")
                End If
            Else
                MessageBox.Show("Kode Dealer tidak valid")
            End If
        Else
            MessageBox.Show("Pilih kode dealer terlebih dahulu")
        End If

    End Sub
#End Region

#Region "Custom"
    Private Sub BindOrganizationStructure(ByVal oDealer As Dealer)
        Dim sb As StringBuilder = New StringBuilder("")
        Dim arlManager As ArrayList
        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr valign='bottom'>")
        sb.Append("<td colspan='3' style='width: 30%; border-style: none none none none'>")
        sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0'>")
        sb.Append("<tr>")
        sb.Append("<td colspan='3' style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan='3' style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none solid none none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</td>")

        'Col-2
        sb.Append("<td style='width: 5%; border-style: none none none none'>")
        sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0'>")
        sb.Append("<tr>")
        sb.Append("<td colspan='3' style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan='3' style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>") 'ooon
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</td>")

        sb.Append("<td colspan='3' style='width: 30%; border-style: none none none none'>")
        sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0'>")
        'Manager / Part Supervisor Title
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none'></td>")

        Dim oDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(oDealer.ID)(0)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_SparePartOrganization), "Grade", MatchType.Exact, oDealerAdditional.SparePartGrade))
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "LevelNumber", MatchType.Exact, 2))
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "ColoumnNumber", MatchType.Exact, 0))
        Dim arrSalesPosition As ArrayList = New V_SparePartOrganizationFacade(User).Retrieve(criterias)
        Dim spo As V_SparePartOrganization
        If arrSalesPosition.Count > 0 Then
            spo = arrSalesPosition(0)
        End If

        sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: solid solid solid solid' align='center' bgcolor='#CCCCCC'  nowrap='true'>" & spo.PositionName & "</td>")
        sb.Append("</tr>")

        'Manager / Part Supervisor Person

        arlManager = GetPartManager(oDealer, spo.SalesmanCategoryLevelID)
        Dim iCountManager As Integer = 0
        If arlManager.Count > 0 Then
            For Each mgr As SalesmanAdditionalInfo In arlManager
                iCountManager = iCountManager + 1
                sb.Append("<tr>")
                sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none'></td>")
                If iCountManager = arlManager.Count Then
                    sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid solid solid' align='center'  nowrap='true'>" & mgr.SalesmanHeader.Name & "</td>")
                Else
                    sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid none solid' align='center'  nowrap='true'>" & mgr.SalesmanHeader.Name & "</td>")
                End If
                sb.Append("</tr>")
            Next
        Else
            sb.Append("<tr>")
            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none'></td>")
            sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid solid solid' align='center'>&nbsp;&nbsp;</td>")
            sb.Append("</tr>")
        End If

        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none solid solid none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none solid none none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</td>")

        sb.Append("<td style='width: 5%; border-style: none none none none'>")
        sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0'>")
        sb.Append("<tr>")
        sb.Append("<td colspan='3' style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan='3' style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</td>")

        sb.Append("<td colspan='3' style='width: 30%; border-style: none none none none'>")
        sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0'>")
        sb.Append("<tr>")
        sb.Append("<td colspan='3' style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan='3' style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none solid none none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</td>")
        sb.Append("</tr>")

        sb.Append("<tr valign='top'>")
        sb.Append("<td colspan='3' style='width: 30%; border-style: none none none none'>" & RenderLiteralSPV(oDealer, 1) & "</td>")
        sb.Append("<td style='width: 5%; border-style: none none none none'></td>")
        sb.Append("<td colspan='3' style='width: 30%; border-style: none none none none'>" & RenderLiteralSPV(oDealer, 2) & "</td>")
        sb.Append("<td style='width: 5%; border-style: none none none none'></td>")
        sb.Append("<td colspan='3' style='width: 30%; border-style: none none none none'>" & RenderLiteralSPV(oDealer, 3) & "</td>")
        sb.Append("</tr>")

        sb.Append("</table>")

        ltStructure.Text = sb.ToString
    End Sub

    Private Function RenderLiteralSPV(ByVal oDealer As Dealer, ByVal iCol As Integer) As String
        Dim iCountPosition As Integer = 0
        Dim iCountPerson As Integer = 0
        Dim oDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(oDealer.ID)(0)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_SparePartOrganization), "Grade", MatchType.Exact, oDealerAdditional.SparePartGrade))
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "LevelNumber", MatchType.Exact, 2))
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "ColoumnNumber", MatchType.Exact, iCol))
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "ParentID", MatchType.Exact, 2))
        Dim arrSalesPosition As ArrayList = New V_SparePartOrganizationFacade(User).Retrieve(criterias)

        Dim sb As StringBuilder = New StringBuilder("")

        sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0'>")
        If arrSalesPosition.Count > 0 Then
            For Each item As V_SparePartOrganization In arrSalesPosition
                iCountPosition = iCountPosition + 1

                sb.Append("<tr>")
                sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none'></td>")
                sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: solid solid solid solid' align='center' bgcolor='#CCCCCC'  nowrap='true'>" & item.PositionName & "</td>")
                sb.Append("</tr>")

                Dim criteriaSales As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.Dealer.ID", MatchType.Exact, oDealer.ID))
                criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanCategoryLevel.ID", MatchType.Exact, item.SalesmanCategoryLevelID))
                criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.Status", MatchType.Exact, CType(EnumSalesmanStatus.SalesmanStatus.Aktif, Integer)))
                criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '
                Dim arrSalesmanAdditionalInfo As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criteriaSales)
                If arrSalesmanAdditionalInfo.Count > 0 Then
                    For Each sales As SalesmanAdditionalInfo In arrSalesmanAdditionalInfo
                        iCountPerson = iCountPerson + 1
                        sb.Append("<tr>")
                        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none'></td>")
                        If iCountPerson < arrSalesmanAdditionalInfo.Count Then
                            sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid none solid' align='center'  nowrap='true'>" & sales.SalesmanHeader.Name & "</td>")
                        Else
                            sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid solid solid' align='center'  nowrap='true'>" & sales.SalesmanHeader.Name & "</td>")
                        End If
                        sb.Append("</tr>")
                    Next
                Else
                    sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none'></td>")
                    sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid solid solid' align='center'>&nbsp;</td>")
                End If
            Next
        Else
            'jika level 2 is null
            sb.Append("<tr>")
            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 10%'>&nbsp;</td>")
            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none solid none none; width: 45%'>&nbsp;</td>")
            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 10%'>&nbsp;</td>")
            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none solid none none; width: 45%'>&nbsp;</td>")
            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
            sb.Append("</tr>")
            'end jika
        End If


        'CREATE LINE TO CHILD
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid none; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none solid solid none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none solid; width: 10%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
        sb.Append("</tr>")
        'END CREATE LINE TO CHILD

        sb.Append(RenderLiteral(oDealer, iCol))

        sb.Append("</table>")

        Return sb.ToString
    End Function

    Private Function RenderLiteral(ByVal oDealer As Dealer, ByVal iCol As Integer) As String
        Dim iCountPosition As Integer = 0
        Dim iCountPerson As Integer = 0
        Dim oDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(oDealer.ID)(0)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_SparePartOrganization), "Grade", MatchType.Exact, oDealerAdditional.SparePartGrade))
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "LevelNumber", MatchType.Exact, 2))
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "ColoumnNumber", MatchType.Exact, iCol))
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "ParentID", MatchType.Greater, 2))
        Dim arrSalesPosition As ArrayList = New V_SparePartOrganizationFacade(User).Retrieve(criterias)

        Dim sb As StringBuilder = New StringBuilder("")

        If arrSalesPosition.Count > 0 Then
            For Each item As V_SparePartOrganization In arrSalesPosition
                iCountPosition = iCountPosition + 1

                sb.Append("<tr>")
                sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none solid solid'>&nbsp;</td>")
                sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: solid solid solid solid' align='center' bgcolor='#CCCCCC'  nowrap='true'>" & item.PositionName & "</td>")
                sb.Append("</tr>")

                Dim criteriaSales As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.Dealer.ID", MatchType.Exact, oDealer.ID))
                criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanCategoryLevel.ID", MatchType.Exact, item.SalesmanCategoryLevelID))
                criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.Status", MatchType.Exact, CType(EnumSalesmanStatus.SalesmanStatus.Aktif, Integer)))
                '
                Dim arrSalesmanAdditionalInfo As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criteriaSales)
                If arrSalesmanAdditionalInfo.Count > 0 Then
                    iCountPerson = 0
                    For Each sales As SalesmanAdditionalInfo In arrSalesmanAdditionalInfo
                        iCountPerson = iCountPerson + 1
                        sb.Append("<tr>")
                        If iCountPosition < arrSalesPosition.Count Then
                            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none solid'>&nbsp;</td>")
                        Else
                            sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
                        End If

                        If iCountPerson < arrSalesmanAdditionalInfo.Count Then
                            sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid none solid' align='center' nowrap='true'>" & sales.SalesmanHeader.Name & "</td>")
                        Else
                            sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid solid solid' align='center' nowrap='true'>" & sales.SalesmanHeader.Name & "</td>")
                        End If
                        sb.Append("</tr>")
                    Next
                Else
                    sb.Append("<tr>")
                    If iCountPosition < arrSalesPosition.Count Then
                        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none solid'>&nbsp;</td>")
                    Else
                        sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none'>&nbsp;</td>")
                    End If
                    sb.Append("<td colspan='2' style='border-width: thin; border-color: #000000; border-style: none solid solid solid' align='center'>&nbsp;&nbsp;</td>")
                    sb.Append("</tr>")
                End If


                If iCountPosition < arrSalesPosition.Count Then
                    sb.Append("<tr>")
                    sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none solid; width: 10%'>&nbsp;</td>")
                    sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
                    sb.Append("<td style='border-width: thin; border-color: #000000; border-style: none none none none; width: 45%'>&nbsp;</td>")
                    sb.Append("</tr>")
                End If

            Next
        End If

        Return sb.ToString
    End Function

    Private Function GetPartManager(ByVal oDealer As Dealer, ByVal salesCategoryLevelID As Integer) As ArrayList
        Dim criteriaSales As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.Dealer.ID", MatchType.Exact, oDealer.ID))
        criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanCategoryLevel.ID", MatchType.Exact, salesCategoryLevelID)) 'Manager, get from table SalesmanCategoryLevel
        criteriaSales.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.Status", MatchType.Exact, CType(EnumSalesmanStatus.SalesmanStatus.Aktif, Integer)))
        Dim arrSalesmanAdditionalInfo As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criteriaSales)
        Return arrSalesmanAdditionalInfo
    End Function
#End Region


End Class
