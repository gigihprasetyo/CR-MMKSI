
#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/18/2007 - 2:19:39 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Linq

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNet.BusinessFacade.Profile

    Public Class RevisionChassisMasterProfileFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RevisionChassisMasterProfileMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_RevisionChassisMasterProfileMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionChassisMasterProfile).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.RevisionChassisMasterProfile))
        End Sub

#End Region

#Region "Retrieve"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.RevisionChassisMasterProfile) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.RevisionChassisMasterProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.RevisionChassisMasterProfile).MarkLoaded()
            End If
        End Sub

        Public Function Retrieve(ByVal ID As Integer) As RevisionChassisMasterProfile
            Return CType(m_RevisionChassisMasterProfileMapper.Retrieve(ID), RevisionChassisMasterProfile)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RevisionChassisMasterProfileMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionChassisMasterProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionChassisMasterProfileMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionChassisMasterProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionChassisMasterProfileMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _RevisionChassisMasterProfile As ArrayList = m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias)
            Return _RevisionChassisMasterProfile
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionChassisMasterProfileColl As ArrayList = m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RevisionChassisMasterProfileColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(RevisionChassisMasterProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim RevisionChassisMasterProfileColl As ArrayList = m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return RevisionChassisMasterProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionChassisMasterProfileColl As ArrayList = m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), columnName, matchOperator, columnValue))
            Return RevisionChassisMasterProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionChassisMasterProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), columnName, matchOperator, columnValue))

            Return m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function GetRevisionChassisMasterProfileByChassisEndCustomer(ByVal cmID As Integer, ByVal endCustID As Integer) As ArrayList
            Dim result As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, cmID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "EndCustomer.ID", MatchType.Exact, endCustID))

            Dim Coll As ArrayList = New ArrayList

            result = m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias)

            Return result

        End Function

        Public Function GetRevisionChassisMasterProfileByOldEndcustomer(ByVal cmID As Integer, ByVal endCustID As Integer) As ArrayList
            Dim result As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, cmID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "OldEndCustomer.ID", MatchType.Exact, endCustID))

            Dim Coll As ArrayList = New ArrayList

            result = m_RevisionChassisMasterProfileMapper.RetrieveByCriteria(criterias)

            Return result

        End Function

        Public Function GetRevisionChassisMasterProfile(ByVal objRevisionChassisMasterProfile As RevisionChassisMasterProfile, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As RevisionChassisMasterProfile
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, objRevisionChassisMasterProfile.ChassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "EndCustomer.ID", MatchType.Exact, objRevisionChassisMasterProfile.EndCustomer.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))

            Dim Coll As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionChassisMasterProfile).ToString)

            Coll = sProfileMapper.RetrieveByCriteria(criterias)
            If (Coll.Count > 0) Then
                Return CType(Coll(0), RevisionChassisMasterProfile)
            End If
            Return New RevisionChassisMasterProfile
        End Function
#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"

        Public Function Insert(ByVal objDomain As RevisionChassisMasterProfile) As Integer
            Dim iReturn As Integer = -2
            Try
                m_RevisionChassisMasterProfileMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As RevisionChassisMasterProfile) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RevisionChassisMasterProfileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As RevisionChassisMasterProfile)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_RevisionChassisMasterProfileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As RevisionChassisMasterProfile) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_RevisionChassisMasterProfileMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomains As ArrayList, ByVal objRevFaktur As RevisionFaktur) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If performTransaction Then

                        Dim res As New ArrayList
                        res = GetRevisionChassisMasterProfileByChassisEndCustomer(objRevFaktur.ChassisMaster.ID, objRevFaktur.EndCustomer.ID)
                        Dim isChangeGRoup As Boolean = False
                        If Not IsNothing(res) AndAlso res.Count > 0 Then

                            For Each cpg As RevisionChassisMasterProfile In res

                                If cpg.ProfileGroup.ID <> CType(objDomains(0), RevisionChassisMasterProfile).ProfileGroup.ID Then
                                    isChangeGRoup = True
                                    Exit For
                                End If
                            Next

                            If isChangeGRoup Then
                                For Each cmP As RevisionChassisMasterProfile In res
                                    Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionChassisMasterProfile).ToString)
                                    cmP.RowStatus = CInt(DBRowStatus.Deleted)
                                    cmP.LastUpdateBy = m_userPrincipal.Identity.Name
                                    cmP.LastUpdateTime = Now
                                    sProfileMapper.Update(cmP, m_userPrincipal.Identity.Name)
                                Next
                            End If
                        End If

                        For Each item As RevisionChassisMasterProfile In objDomains
                            Dim oldProfile As RevisionChassisMasterProfile = New RevisionChassisMasterProfile
                            If Not IsNothing(res) AndAlso res.Count > 0 Then
                                oldProfile = CType((From e As RevisionChassisMasterProfile In res
                                        Where e.ChassisMaster.ID = objRevFaktur.ChassisMaster.ID And
                                            e.EndCustomer.ID = objRevFaktur.EndCustomer.ID And
                                            e.ProfileGroup.ID = item.ProfileGroup.ID And
                                            e.ProfileHeader.ID = item.ProfileHeader.ID
                                        Select e).FirstOrDefault, RevisionChassisMasterProfile)
                            End If

                            If oldProfile.ID > 0 Then
                                If oldProfile.ProfileValue <> item.ProfileValue Then
                                    oldProfile.ProfileValue = item.ProfileValue
                                    oldProfile.LastUpdateBy = m_userPrincipal.Identity.Name
                                    oldProfile.LastUpdateTime = Now
                                    m_TransactionManager.AddUpdate(oldProfile, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                item.CreatedBy = m_userPrincipal.Identity.Name
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next

                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace



