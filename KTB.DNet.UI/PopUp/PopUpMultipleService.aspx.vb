Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation
Imports System.Text

Public Class PopUpMultipleService
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Private isDealerPiloting As Boolean = False
    Private stdFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private gRKindFacade As GRKindFacade = New GRKindFacade(User)
    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private freeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
    Private recallServiceFacade As RecallServiceFacade = New RecallServiceFacade(User)
    Private pmHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
    Private vechileColorFacade As VechileColorFacade = New VechileColorFacade(User)
    Private chassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
    Private srFUFacade As ServiceReminderFollowUpFacade = New ServiceReminderFollowUpFacade(User)
    Private ccResFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
    Private ccFacade As CustomerCaseFacade = New CustomerCaseFacade(User)
    Private appConfFacade As AppConfigFacade = New AppConfigFacade(User)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            initDDL()
        End If
    End Sub

    Protected Sub ddlJnsKegiatan_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlJnsService.Items.Clear()

        If Not IsNothing(sender) Then
            Dim ddl As DropDownList = sender
            Select Case ddl.SelectedValue
                Case 1
                    crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = fSKindFacade.Retrieve(crit)

                    With ddlJnsService.Items
                        For Each obj As FSKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                        Next
                    End With
                Case 2
                    crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = pMKindFacade.Retrieve(crit)

                    With ddlJnsService.Items
                        For Each obj As PMKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                        Next
                    End With
                Case 3
                    crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)

                    With ddlJnsService.Items
                        For Each obj As RecallCategory In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.RecallRegNo, obj.Description), obj.ID))
                        Next
                    End With
                Case 4
                    crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = gRKindFacade.Retrieve(crit)

                    With ddlJnsService.Items
                        For Each obj As GRKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.ID))
                        Next
                    End With
            End Select
        End If

        ddlJnsService.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub initDDL()
        Dim results As ArrayList

        ddlJnsKegiatan.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))

        results = stdFacade.Retrieve(crit)

        With ddlJnsKegiatan.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With

        ddlJnsKegiatan.Items.Insert(0, "Silahkan Pilih")
        ddlJnsKegiatan_SelectedIndexChanged(Nothing, Nothing)

    End Sub
End Class