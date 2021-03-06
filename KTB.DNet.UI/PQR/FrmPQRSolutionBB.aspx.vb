Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
Public Class FrmPQRSolutionBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeKerusakan As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKodePart As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents pnlResult As System.Web.UI.WebControls.Panel
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PQRListSolutionView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PRODUCT QUALITY REPORT - Solusi PQR")
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            RetrieveMaster()
        End If
    End Sub
    Private Sub RetrieveMaster()

        Dim _arrCat As ArrayList = New PQRHeaderBBFacade(User).RetrieveListCategory()
        For Each _item As Category In _arrCat
            Dim _listCat As ListItem = New ListItem(_item.Description, _item.CategoryCode)
            _listCat.Selected = False
            ddlKategori.Items.Add(_listCat)
        Next
        ddlKategori.ClearSelection()
        Dim _listK As ListItem = New ListItem("Pilih Kategori", "null")
        _listK.Selected = True
        ddlKategori.Items.Add(_listK)


        Dim _arrTipe As ArrayList = New PQRHeaderBBFacade(User).RetrieveListType()
        For Each _item As VechileColor In _arrTipe
            Dim _listType As ListItem = New ListItem(_item.MaterialDescription, _item.ColorCode)
            _listType.Selected = False
            ddlTipe.Items.Add(_listType)
        Next
        ddlTipe.ClearSelection()
        Dim _listT As ListItem = New ListItem("Pilih Tipe", "null")
        _listT.Selected = True
        ddlTipe.Items.Add(_listT)


    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Dim i As Integer = 0
        Dim _arrResult As ArrayList = ResultCollection()
        With pnlResult.Controls
            If Not _arrResult.Count = 0 Then
                .Add(New LiteralControl("<table width='100%' border='0' cellSpacing='1' cellPadding='2'><TR><TD colspan='7'>Hasil pencarian sekitar '" + _arrResult.Count.ToString + "'<br>&nbsp;</TD></TR>"))
                For Each _item As PQRHeaderBB In _arrResult
                    .Add(New LiteralControl("<tr bgcolor=#dedede><td class='titleField'>No. PQR &nbsp;&nbsp;</td><td> : "))

                    Dim lblNoPQRs As New Label
                    lblNoPQRs.ID = "lblNoPQRs" + i.ToString
                    lblNoPQRs.Text = _item.PQRNo
                    .Add(lblNoPQRs)

                    .Add(New LiteralControl("</td><td class='titleField'>Kategori &nbsp;&nbsp;</td><TD> : "))

                    Dim lblKategori As New Label
                    lblKategori.ID = "lblKategori" + i.ToString
                    lblKategori.Text = _item.ChassisMasterBB.Category.Description
                    .Add(lblKategori)

                    .Add(New LiteralControl("</TD><TD class='titleField'>&nbsp;&nbsp;Tipe &nbsp;&nbsp;</TD><TD colspan='2'> : "))

                    Dim lblTipe As New Label
                    lblTipe.ID = "lblTipe" + i.ToString
                    lblTipe.Text = _item.ChassisMasterBB.VechileColor.MaterialDescription
                    .Add(lblTipe)

                    .Add(New LiteralControl("</TD></tr>"))

                    .Add(New LiteralControl("<tr><td class='titleField'>Subject &nbsp;&nbsp;</td><td colSpan='5'> : "))

                    Dim lblSubject As New Label
                    lblSubject.ID = "lblSubject" + i.ToString
                    lblSubject.Text = _item.Subject

                    .Add(lblSubject)

                    .Add(New LiteralControl("</td><TD></TD></tr>"))
                    .Add(New LiteralControl("<TR valign=top><TD class='titleField' valign=top>Gejala &nbsp;&nbsp;</TD><TD colSpan='5'>"))

                    Dim txtGejala As New TextBox
                    txtGejala.ID = "txtGejala" + i.ToString
                    txtGejala.Text = _item.Symptomps
                    txtGejala.TextMode = TextBoxMode.MultiLine
                    txtGejala.Rows = 4
                    txtGejala.Width = WebControls.Unit.Pixel(350)
                    .Add(txtGejala)

                    .Add(New LiteralControl("</TD><TD class='titleField'>Kode Kerusakan<br>&nbsp;"))

                    Dim x As Integer = 0

                    Dim criDamage As New CriteriaComposite(New Criteria(GetType(PQRDamageCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criDamage.opAnd(New Criteria(GetType(PQRDamageCodeBB), "PQRHeaderBB.ID", MatchType.Exact, _item.ID))
                    Dim _resultDamage2 As ArrayList = New PQRDamageCodeBBFacade(User).Retrieve(criDamage)
                    Dim lblDamageCode As New LinkButton
                    If _resultDamage2.Count > 0 Then
                        For Each _itemDamage As PQRDamageCodeBB In _resultDamage2
                            lblDamageCode.ID = "lKode" + i.ToString + x.ToString
                            lblDamageCode.Text = _itemDamage.DeskripsiKodePosisi.KodePosition
                            .Add(lblDamageCode)
                            .Add(New LiteralControl("<br>&nbsp;"))
                            x = x + 1
                        Next
                    Else
                        lblDamageCode.ID = "lKode" + i.ToString
                        lblDamageCode.Text = "tidak ada"
                        .Add(lblDamageCode)
                        .Add(New LiteralControl("<br>&nbsp;"))
                    End If


                    .Add(New LiteralControl("</TD></TR><TR><TD class='titleField' valign=top>Solusi &nbsp;&nbsp;"))
                    .Add(New LiteralControl("</TD><TD colSpan='5'>"))

                    Dim txtSolusi As New TextBox
                    txtSolusi.ID = "txtSolusi" + i.ToString
                    txtSolusi.Text = _item.Solutions
                    txtSolusi.TextMode = TextBoxMode.MultiLine
                    txtSolusi.Rows = 4
                    txtSolusi.Width = WebControls.Unit.Pixel(350)
                    .Add(txtSolusi)

                    .Add(New LiteralControl("</TD><TD class='titleField'>Kode Parts<br>"))

                    Dim criParts As New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criParts.opAnd(New Criteria(GetType(PQRPartsCodeBB), "PQRHeaderBB.ID", MatchType.Exact, _item.ID))
                    Dim _resultParts2 As ArrayList = New PQRPartsCodeBBFacade(User).Retrieve(criParts)
                    Dim lblPartCode As New Label
                    If _resultParts2.Count > 0 Then
                        For Each _itemParts As PQRPartsCodeBB In _resultParts2
                            lblPartCode.ID = "lKode" + i.ToString + x.ToString
                            lblPartCode.Text = _itemParts.SparePartMaster.PartCode
                            .Add(lblPartCode)
                            .Add(New LiteralControl("<br>&nbsp;"))
                            x = x + 1
                        Next
                    Else
                        lblPartCode.ID = "lKode" + i.ToString
                        lblPartCode.Text = "tidak ada"
                        .Add(lblPartCode)
                        .Add(New LiteralControl("<br>&nbsp;"))
                    End If
                    .Add(New LiteralControl("</TD><TD></TD></TR><tr><td colspan='6'>&nbsp;&nbsp;</td></tr>"))
                Next
                .Add(New LiteralControl("</table>"))
            End If
        End With
    End Sub
    Private Function CollectionPQRHeaderBB() As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not txtPQRNo.Text.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRNo", MatchType.[Partial], txtPQRNo.Text))
        End If
        If Not ddlKategori.SelectedValue = "null" Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ChassisMasterBB.Category.CategoryCode", MatchType.Exact, ddlKategori.SelectedValue))
        End If
        If Not ddlTipe.SelectedValue = "null" Then
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ChassisMasterBB.VechileColor.ColorCode", MatchType.Exact, ddlTipe.SelectedValue))
        End If

        Dim tempSbj() As String = txtSubject.Text.Trim.Split(" ")
        If tempSbj.Length > 1 Then
            For Each _item As String In tempSbj
                criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Subject", MatchType.[Partial], _item))
            Next
        Else
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Subject", MatchType.[Partial], txtSubject.Text.Trim))
        End If

        Dim _result As ArrayList = New PQRHeaderBBFacade(User).RetrieveByCriteria(criterias)
        Return _result
    End Function
    Private Function CollectionDamage() As ArrayList
        Dim _arrPQRDamage As New ArrayList

        If Not txtKodeKerusakan.Text.Trim() = "" Then
            Dim criDamage As New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criDamage.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.Exact, txtKodeKerusakan.Text))
            Dim _resultDamage As ArrayList = New DeskripsiKodePosisiFacade(User).Retrieve(criDamage)


            For Each _item As DeskripsiKodePosisi In _resultDamage
                Dim cri As New CriteriaComposite(New Criteria(GetType(PQRDamageCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cri.opAnd(New Criteria(GetType(PQRDamageCodeBB), "DeskripsiKodePosisi.ID", MatchType.Exact, _item.ID))
                Dim _arrTemp As ArrayList = New PQRDamageCodeBBFacade(User).Retrieve(cri)
                For Each _itemPQR As PQRDamageCodeBB In _arrTemp
                    _arrPQRDamage.Add(_itemPQR)
                Next
            Next
        End If
        Return _arrPQRDamage
    End Function
    Private Function CollectionParts() As ArrayList
        Dim _arrPQRParts As New ArrayList
        If Not txtKodePart.Text.Trim() = "" Then
            Dim criParts As New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criParts.opAnd(New Criteria(GetType(SparePartMaster), "PartCode", MatchType.Exact, txtKodePart.Text))
            Dim _resultParts As ArrayList = New SparePartMasterFacade(User).Retrieve(criParts)
            For Each _item As SparePartMaster In _resultParts
                Dim cri As New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cri.opAnd(New Criteria(GetType(PQRPartsCodeBB), "SparePartMaster.ID", MatchType.Exact, _item.ID))
                Dim _arrTemp As ArrayList = New PQRPartsCodeBBFacade(User).Retrieve(cri)
                For Each _itemPQR As PQRPartsCodeBB In _arrTemp
                    _arrPQRParts.Add(_itemPQR)
                Next
            Next
        End If

        Return _arrPQRParts
    End Function
    Private Function ResultCollection() As ArrayList
        Dim _resultPQRHeaderBB As ArrayList = CollectionPQRHeaderBB()
        Dim _resultDamage As ArrayList = CollectionDamage()
        Dim _resultParts As ArrayList = CollectionParts()
        Dim _result As New ArrayList

        For Each itemheader As PQRHeaderBB In _resultPQRHeaderBB
            _result.Add(itemheader)
        Next

        For Each _itemDamage As PQRDamageCodeBB In _resultDamage
            _result.Add(_itemDamage.PQRHeaderBB)
        Next

        For Each _itemParts As PQRPartsCodeBB In _resultParts
            _result.Add(_itemParts.PQRHeaderBB)
        Next

        Dim i As Integer
        Dim j As Integer
        Dim _resultFinal As New ArrayList
        Dim _tempArr As New ArrayList
        For i = 0 To _result.Count - 1

            Dim bcheck As Boolean = False
            For j = 0 To _result.Count - 1

                Dim obj1 As PQRHeaderBB = CType(_result(i), PQRHeaderBB)
                Dim obj2 As PQRHeaderBB = CType(_result(j), PQRHeaderBB)
                If i <> j Then
                    If obj1.ID <> obj2.ID Then
                        bcheck = True
                    Else
                        bcheck = False
                    End If
                Else
                    bcheck = True
                End If
                j = j + 1
            Next
            If bcheck Then
                _resultFinal.Add(CType(_result(i), PQRHeaderBB))
            End If
            i = i + 1
        Next
        Return _resultFinal
    End Function
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmPQRAdditionalInfoBB.aspx?PQRID=25")
    End Sub
End Class
