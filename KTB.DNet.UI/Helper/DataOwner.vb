
#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports System.Security.Principal
Imports System.Text
#End Region

Namespace KTB.DNet.UI.Helper
    Public Class DataOwner
        Inherits System.Web.UI.Page

#Region "Constructor"
        Public Sub New()

        End Sub
#End Region

#Region "Method"
        Public Function IsdealerExistInGroup(ByVal strDealer As String, ByVal objDealer As Dealer) As Boolean
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim ArlUserOrgAssignment As ArrayList = New UserOrgAssignmentFacade(User).Retrieve(criterias)
            Dim found As Boolean = False
            If ArlUserOrgAssignment.Count > 0 Then
                For Each stritem As String In strDealer.Split(";")
                    found = False
                    For Each item As UserOrgAssignment In ArlUserOrgAssignment

                        If stritem = item.Dealer.DealerCode Then
                            found = True
                        End If
                    Next
                    If found = False Then
                        If stritem <> objDealer.DealerCode Then
                            Return False
                        End If
                    End If
                Next
            End If
            Return True
        End Function

        Public Function GetDealerExistInGroup(ByVal strDealer As String, ByVal objDealer As Dealer) As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim ArlUserOrgAssignment As ArrayList = New UserOrgAssignmentFacade(User).Retrieve(criterias)
            Dim arrResult As New ArrayList

            If ArlUserOrgAssignment.Count > 0 Then
                For Each stritem As String In strDealer.Split(";")
                    For Each item As UserOrgAssignment In ArlUserOrgAssignment

                        If stritem = item.Dealer.DealerCode Then
                            arrResult.Add(item.Dealer)
                        End If
                    Next
                Next
            End If
            Return arrResult
        End Function


        Public Function GenerateDealerCodeSelection(ByVal objDealer As Dealer, ByVal User As IPrincipal) As String
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim ArlUserOrgAssignment As ArrayList = New UserOrgAssignmentFacade(User).Retrieve(criterias)
            Dim strDealerCodeColection As String = "('" & objDealer.DealerCode
            If ArlUserOrgAssignment.Count > 0 Then
                For Each item As UserOrgAssignment In ArlUserOrgAssignment
                    strDealerCodeColection = strDealerCodeColection & "','" & item.Dealer.DealerCode
                Next
            End If
            strDealerCodeColection = strDealerCodeColection & "')"
            Return strDealerCodeColection
        End Function

        Public Function GenerateDealerIDSelection(ByVal objDealer As Dealer, ByVal User As IPrincipal) As String
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim ArlUserOrgAssignment As ArrayList = New UserOrgAssignmentFacade(User).Retrieve(criterias)
            Dim strDealerIDColection As String = "(" & objDealer.ID
            If ArlUserOrgAssignment.Count > 0 Then
                For Each item As UserOrgAssignment In ArlUserOrgAssignment
                    strDealerIDColection = strDealerIDColection & "," & item.Dealer.ID
                Next
            End If
            strDealerIDColection = strDealerIDColection & ")"
            Return strDealerIDColection
        End Function
#End Region

    End Class
End Namespace

