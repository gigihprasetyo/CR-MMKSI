#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq
#End Region


Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC

Public Class PopUpESRUTDownloadLog
    Inherits System.Web.UI.Page

    Dim gridColNo As Integer = 0
    Dim esrutItemID As Integer = 0
    Dim sourcePage As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                InitPage()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub InitPage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        sourcePage = Page.Request.QueryString("Source")

        If Not IsNothing(Page.Request.QueryString("ID")) Then
            esrutItemID = CType(Page.Request.QueryString("ID"), Integer)

            Dim esrutItem As ESRUTItem = New ESRUTItemFacade(User).Retrieve(esrutItemID)

            lblChassisNumber.Text = esrutItem.ChassisNumber

        End If

        BindDataGrid(0)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridColNo = dgList.PageSize * indexPage
        dgList.DataSource = New ESRUTDownloadLogFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dgList.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgList.VirtualItemCount = totalRow
        dgList.CurrentPageIndex = indexPage
        dgList.DataBind()
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ESRUTDownloadLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ESRUTDownloadLog), "ESRUTItem.ID", MatchType.Exact, esrutItemID))

        If sourcePage = "dealer" Then
            criterias.opAnd(New Criteria(GetType(ESRUTDownloadLog), "CreatedBy", MatchType.NotLike, "000002"))
        End If

        Return criterias
    End Function

    Private Sub dgList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgList.ItemDataBound
        Try
            If Not e.Item.DataItem Is Nothing Then
                Dim data As ESRUTDownloadLog = CType(e.Item.DataItem, ESRUTDownloadLog)

                gridColNo += 1

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblCreatedTime As Label = CType(e.Item.FindControl("lblCreatedTime"), Label)
                Dim lblCreatedBy As Label = CType(e.Item.FindControl("lblCreatedBy"), Label)

                lblNo.Text = gridColNo
                lblCreatedTime.Text = data.CreatedTime
                lblCreatedBy.Text = data.CreatedBy

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgList.SortCommand
        Try
            If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
                Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                    Case Sort.SortDirection.ASC
                        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                    Case Sort.SortDirection.DESC
                        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("CurrentSortColumn") = e.SortExpression
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End If

            dgList.CurrentPageIndex = 0
            BindDataGrid(dgList.CurrentPageIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        Try
            'dgList.CurrentPageIndex = e.NewPageIndex
            BindDataGrid(e.NewPageIndex)
        Catch ex As Exception

        End Try
    End Sub

End Class