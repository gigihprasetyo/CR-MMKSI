#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.UserManagement
    Public Class UserOrgAssignmentFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_UserOrgAssignmentMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_UserOrgAssignmentMapper = MapperFactory.GetInstance().GetMapper(GetType(UserOrgAssignment).ToString)
            Me.DomainTypeCollection.Add(GetType(UserOrgAssignment))
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As UserOrgAssignment
            Return CType(m_UserOrgAssignmentMapper.Retrieve(ID), UserOrgAssignment)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_UserOrgAssignmentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveActiveList(ByVal crit As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortTable As Type, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(sortTable, sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserOrgAssignmentMapper.RetrieveByCriteria(crit, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function IsExist(ByVal UserInfoID As Integer, ByVal objDealer As Dealer) As Boolean
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserOrgAssignment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(UserOrgAssignment), "Dealer.ID", MatchType.Exact, objDealer.ID))
            crit.opAnd(New Criteria(GetType(UserOrgAssignment), "UserInfo.ID", MatchType.Exact, UserInfoID))
            Dim aggr As Aggregate = New Aggregate(GetType(UserOrgAssignment), "ID", AggregateType.Count)
            Return (m_UserOrgAssignmentMapper.RetrieveScalar(aggr, crit) = 1)
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Public Sub DeleteFromDB(ByVal objDomain As UserOrgAssignment)
            Try
                m_UserOrgAssignmentMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw ex
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As UserOrgAssignment) As Integer
            Try
                Return m_UserOrgAssignmentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Sub Insert(ByVal listDomain As ArrayList)
            Dim nReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim nCounter As Integer
                    Dim mCounter As Integer
                    For nCounter = 0 To listDomain.Count - 1
                        Dim objUserOrgAssignment As UserOrgAssignment
                        objUserOrgAssignment = CType(listDomain.Item(nCounter), UserOrgAssignment)
                        m_TransactionManager.AddInsert(objUserOrgAssignment, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    nReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw ex
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

