Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Utility


Public Class PopupDataHistory
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblJenisDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblTableName As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblTableID As System.Web.UI.WebControls.Label
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTableName2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTableID2 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgMain2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents trData2 As System.Web.UI.HtmlControls.HtmlTableRow

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Variables"
    Private _vstTableName As String = "_vstTableName"
    Private _vstTableID As String = "_vstTableID"
    Private _vstTableCode As String = "_vstTableCode"
    Private _vstFieldName As String = "_vstFieldName"

    Private _vstTableName2 As String = "_vstTableName2"
    Private _vstTableID2 As String = "_vstTableID2"
    Private _vstTableCode2 As String = "_vstTableCode2"
    Private _vstFieldName2 As String = "_vstFieldName2"

    Private _sessDHDs As String = "PopupDataHistory._sessDHDs"
    Private _sessDHDs2 As String = "PopupDataHistory._sessDHDs2"
    Private _sessHelper As New SessionHelper
#End Region

#Region "Methods"

    Private Sub Initialization()
        'Data I
        Me.ViewState.Add(Me._vstTableName, Request.Item("TableName"))
        Me.ViewState.Add(Me._vstTableID, Request.Item("TableID"))
        If Not IsNothing(Request.Item("TableCode")) Then
            Me.ViewState.Add(Me._vstTableCode, Request.Item("TableCode"))
        Else
            Me.ViewState.Add(Me._vstTableCode, String.Empty)
        End If
        If Not IsNothing(Request.Item("FieldName")) Then
            Me.ViewState.Add(Me._vstFieldName, Request.Item("FieldName"))
        Else
            Me.ViewState.Add(Me._vstFieldName, String.Empty)
        End If


        'Data II
        Me.ViewState.Add(Me._vstTableName2, Request.Item("TableName2"))
        Me.ViewState.Add(Me._vstTableID2, Request.Item("TableID2"))
        If Not IsNothing(Request.Item("TableCode2")) Then
            Me.ViewState.Add(Me._vstTableCode2, Request.Item("TableCode2"))
        Else
            Me.ViewState.Add(Me._vstTableCode2, String.Empty)
        End If
        If Not IsNothing(Request.Item("FieldName2")) Then
            Me.ViewState.Add(Me._vstFieldName2, Request.Item("FieldName2"))
        Else
            Me.ViewState.Add(Me._vstFieldName2, String.Empty)
        End If

        BindData()
        BindData2()
    End Sub

    Private Sub BindData()

        Me.lblTableName.Text = Me.ViewState.Item(Me._vstTableName)
        Me.lblTableID.Text = Me.ViewState.Item(Me._vstTableID)
        If Me.ViewState.Item(Me._vstTableCode) <> String.Empty Then
            Me.lblTableID.Text &= " / " & Me.ViewState.Item(Me._vstTableCode)
        End If

        Dim cDH As New CriteriaComposite(New Criteria(GetType(DataHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sDH As New SortCollection
        Dim aDHs As ArrayList
        Dim aDHDs As New ArrayList
        Dim FieldName As String = Me.ViewState.Item(Me._vstFieldName)
        Dim Fields() As String = FieldName.Trim.ToLower().Split(";")

        cDH.opAnd(New Criteria(GetType(DataHistory), "DataTable", MatchType.Exact, Me.ViewState.Item(Me._vstTableName)))
        cDH.opAnd(New Criteria(GetType(DataHistory), "DataID", MatchType.Exact, CType(Me.ViewState.Item(Me._vstTableID), Integer)))
        sDH.Add(New Sort(GetType(DataHistory), "CreatedTime", Sort.SortDirection.DESC))
        aDHs = New DataHistoryFacade(User).Retrieve(cDH, sDH)
        For Each oDH As DataHistory In aDHs
            For Each oDHD As DataHistoryDetail In oDH.DataHistoryDetails
                If FieldName = String.Empty Then
                    aDHDs.Add(oDHD)
                Else
                    'If oDHD.FieldName.Trim.ToUpper = FieldName.Trim.ToUpper Then
                    '    aDHDs.Add(oDHD)
                    'End If
                    If Array.IndexOf(Fields, oDHD.FieldName.Trim().ToLower()) >= 0 Then
                        aDHDs.Add(oDHD)
                    End If
                End If
            Next
        Next
        Me._sessHelper.SetSession(Me._sessDHDs, aDHDs)
        Me.dtgMain.DataSource = aDHDs
        Me.dtgMain.DataBind()
    End Sub

    Private Sub BindData2()

        If Me.ViewState.Item(Me._vstTableName2) = String.Empty Then
            Me.trData2.Style.Add("display", "none")
            Exit Sub
        End If

        Me.lblTableName2.Text = Me.ViewState.Item(Me._vstTableName2)
        Me.lblTableID2.Text = Me.ViewState.Item(Me._vstTableID2)
        If Me.ViewState.Item(Me._vstTableCode2) <> String.Empty Then
            Me.lblTableID2.Text &= " / " & Me.ViewState.Item(Me._vstTableCode2)
        End If

        Dim cDH As New CriteriaComposite(New Criteria(GetType(DataHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sDH As New SortCollection
        Dim aDHs As ArrayList
        Dim aDHDs As New ArrayList
        Dim FieldName As String = Me.ViewState.Item(Me._vstFieldName2)
        Dim Fields() As String = FieldName.Trim.ToLower().Split(";")

        cDH.opAnd(New Criteria(GetType(DataHistory), "DataTable", MatchType.Exact, Me.ViewState.Item(Me._vstTableName2)))
        cDH.opAnd(New Criteria(GetType(DataHistory), "DataID", MatchType.Exact, CType(Me.ViewState.Item(Me._vstTableID2), Integer)))
        sDH.Add(New Sort(GetType(DataHistory), "CreatedTime", Sort.SortDirection.DESC))
        aDHs = New DataHistoryFacade(User).Retrieve(cDH, sDH)
        For Each oDH As DataHistory In aDHs
            For Each oDHD As DataHistoryDetail In oDH.DataHistoryDetails
                Dim isSkip As Boolean = False
                If FieldName = String.Empty Then
                    aDHDs.Add(oDHD)
                Else
                    'If oDHD.FieldName.Trim.ToUpper = FieldName.Trim.ToUpper Then
                    '    aDHDs.Add(oDHD)
                    'End If
                    If CType(Me.ViewState.Item(Me._vstTableName2), String).Trim.ToLower() = "IndentPartHeader".ToLower() Then
                        If oDHD.FieldName.Trim().ToLower() = "StatusKTB".ToLower() AndAlso oDHD.OldValue = "0" AndAlso oDHD.NewValue = "0" Then
                            isSkip = True
                        End If
                    End If
                    If Array.IndexOf(Fields, oDHD.FieldName.Trim().ToLower()) >= 0 Then
                        If isSkip = False Then aDHDs.Add(oDHD)
                    End If
                End If
            Next
        Next
        Me._sessHelper.SetSession(Me._sessDHDs2, aDHDs)
        Me.dtgMain2.DataSource = aDHDs
        Me.dtgMain2.DataBind()
    End Sub

#End Region

#Region "Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack() Then
            Initialization()
        End If
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim aDHDs As ArrayList = CType(Me._sessHelper.GetSession(Me._sessDHDs), ArrayList)
            Dim oDHD As DataHistoryDetail = aDHDs(e.Item.ItemIndex)
            Dim oDealer As Dealer = CType(Me._sessHelper.GetSession("DEALER"), Dealer)
            Dim statusA As String, statusB As String

            If Not IsNothing(oDHD) AndAlso oDHD.ID > 0 Then
                e.Item.Cells(1).Text = oDHD.FieldName
                e.Item.Cells(2).Text = oDHD.OldValue
                e.Item.Cells(3).Text = oDHD.NewValue
                Select Case Me.ViewState.Item(Me._vstTableName).Trim().ToLower()
                    Case "EstimationEquipHeader".ToLower()
                        If oDHD.FieldName.ToLower() = "Status".ToLower() Then
                            statusA = CType(oDHD.OldValue, EnumEstimationEquipStatus.EstimationEquipStatusHeader).ToString()
                            statusB = CType(oDHD.NewValue, EnumEstimationEquipStatus.EstimationEquipStatusHeader).ToString()

                            'If oDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                            '    If CType(oDHD.OldValue, EnumEstimationEquipStatus.EstimationEquipStatusHeader) = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                            '        statusA = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru.ToString()
                            '    End If
                            '    If CType(oDHD.NewValue, EnumEstimationEquipStatus.EstimationEquipStatusHeader) = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                            '        statusB = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru.ToString()
                            '    End If
                            'End If
                            e.Item.Cells(2).Text = statusA
                            e.Item.Cells(3).Text = statusB
                        End If
                    Case Else
                End Select

                e.Item.Cells(4).Text = oDHD.CreatedTime.ToString("yyyy/MM/dd HH:mm:ss")
                e.Item.Cells(5).Text = oDHD.CreatedBy
            End If
        End If
    End Sub

    Private Sub dtgMain2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim aDHDs As ArrayList = CType(Me._sessHelper.GetSession(Me._sessDHDs2), ArrayList)
            Dim oDHD As DataHistoryDetail = aDHDs(e.Item.ItemIndex)
            Dim oDealer As Dealer = CType(Me._sessHelper.GetSession("DEALER"), Dealer)
            Dim statusA As String, statusB As String

            If Not IsNothing(oDHD) AndAlso oDHD.ID > 0 Then
                e.Item.Cells(1).Text = oDHD.FieldName
                'e.Item.Cells(2).Text = oDHD.OldValue
                'e.Item.Cells(3).Text = oDHD.NewValue

                'e.Item.Cells(2).Text = oDHD.FieldName
                'e.Item.Cells(3).Text = Me.ViewState.Item(Me._vstTableName2).Trim().ToLower()

                If Me.ViewState.Item(Me._vstTableName2).Trim().ToLower() = "IndentPartHeader".ToLower() Then
                    If oDHD.FieldName.ToLower() = "Status".ToLower() OrElse oDHD.FieldName.ToLower() = "StatusKTB".ToLower() Then
                        If oDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                            statusA = CType(CType(oDHD.OldValue, Short), EnumIndentPartEquipStatus.EnumStatusKTB).ToString()
                            statusB = CType(CType(oDHD.NewValue, Short), EnumIndentPartEquipStatus.EnumStatusKTB).ToString()
                        Else
                            statusA = CType(CType(oDHD.OldValue, Short), EnumIndentPartEquipStatus.EnumStatusDealer).ToString()
                            statusB = CType(CType(oDHD.NewValue, Short), EnumIndentPartEquipStatus.EnumStatusDealer).ToString()
                        End If
                        If statusA = "0" Then statusA = "-"
                        If statusB = "0" Then statusB = "-"
                        e.Item.Cells(2).Text = statusA
                        e.Item.Cells(3).Text = statusB
                    End If
                End If
                'Select Case Me.ViewState.Item(Me._vstTableName2).Trim().ToLower()
                '    Case "IndentPartHeader".ToLower()
                '        e.Item.Cells(3).Text = "table"
                '        If oDHD.FieldName.ToLower() = "Status".ToLower() OrElse oDHD.FieldName.ToLower() = "StatusKTB".ToLower() Then
                '            e.Item.Cells(3).Text = "field"
                '            If oDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                '                statusA = CType(oDHD.OldValue, EnumIndentPartEquipStatus.EnumStatusKTB).ToString()
                '                statusB = CType(oDHD.NewValue, EnumIndentPartEquipStatus.EnumStatusKTB).ToString()
                '            Else
                '                statusA = CType(oDHD.OldValue, EnumIndentPartEquipStatus.EnumStatusDealer).ToString()
                '                statusB = CType(oDHD.NewValue, EnumIndentPartEquipStatus.EnumStatusDealer).ToString()
                '            End If
                '            e.Item.Cells(2).Text = statusA
                '            e.Item.Cells(3).Text = statusB
                '        End If
                '    Case Else
                'End Select

                e.Item.Cells(4).Text = oDHD.CreatedTime.ToString("yyyy/MM/dd HH:mm:ss")
                e.Item.Cells(5).Text = oDHD.CreatedBy
            End If
        End If
    End Sub

#End Region
End Class
