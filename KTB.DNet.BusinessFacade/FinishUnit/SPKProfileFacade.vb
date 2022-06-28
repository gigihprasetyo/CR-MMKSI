

#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

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

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

    Public Class SPKProfileFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SPKProfileMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SPKProfileMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKProfile).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPKProfile))
        End Sub

#End Region

#Region "Retrieve"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.SPKProfile) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKProfile).MarkLoaded()
            End If
        End Sub

        Public Function Retrieve(ByVal ID As Integer) As SPKProfile
            Return CType(m_SPKProfileMapper.Retrieve(ID), SPKProfile)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPKProfileMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPKProfileMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPKProfileMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKProfileMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKProfileMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPKProfile As ArrayList = m_SPKProfileMapper.RetrieveByCriteria(criterias)
            Return _SPKProfile
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKProfileColl As ArrayList = m_SPKProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPKProfileColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SPKProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SPKProfileColl As ArrayList = m_SPKProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPKProfileColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim SPKProfileColl As ArrayList = m_SPKProfileMapper.RetrieveByCriteria(criterias)
            Return SPKProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKProfileColl As ArrayList = m_SPKProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SPKProfile), columnName, matchOperator, columnValue))
            Return SPKProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), columnName, matchOperator, columnValue))

            Return m_SPKProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"

        'Public Function GetSPKProfile(ByVal objSPKProfile As SPKProfile, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As SPKProfile
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKProfile.ID", MatchType.Exact, objSPKProfile.ID))
        '    criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
        '    criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))

        '    Dim SPKProfileColl As ArrayList = New ArrayList
        '    Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKProfile).ToString)

        '    SPKProfileColl = sProfileMapper.RetrieveByCriteria(criterias)
        '    If (SPKProfileColl.Count > 0) Then
        '        Return CType(SPKProfileColl(0), SPKProfile)
        '    End If
        '    Return New SPKProfile
        'End Function

        Public Function Insert(ByVal objDomain As SPKProfile) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SPKProfileMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As SPKProfile) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPKProfileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function GetSPKProfile(ByVal objSPKProfile As SPKProfile, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As SPKProfile
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetail.ID", MatchType.Exact, objSPKProfile.SPKDetail.ID))
            criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))

            If objSPKProfile.SPKDetailCustomerID > 0 Then
                criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomerID", MatchType.Exact, objSPKProfile.SPKDetailCustomerID))
            Else
                criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomerID", MatchType.IsNull))
            End If
            Dim Coll As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKProfile).ToString)

            Coll = sProfileMapper.RetrieveByCriteria(criterias)
            If (Coll.Count > 0) Then
                Return CType(Coll(0), SPKProfile)
            End If
            Return New SPKProfile
        End Function

        Public Function Update(ByVal objDomains As ArrayList, ByVal objSPKDetail As SPKDetail) As Integer
            Dim returnValue As Integer = -1
            Dim isProfileChange As Boolean
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If performTransaction Then

                        isProfileChange = False
                        Dim arrExistingProfile As ArrayList = New ArrayList
                        If Not IsNothing(objDomains) AndAlso objDomains.Count > 0 Then
                            If isProfileGroupChange(objSPKDetail, CType(objDomains(0), SPKProfile), arrExistingProfile) Then
                                For Each cmP As SPKProfile In arrExistingProfile
                                    Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKProfile).ToString)
                                    cmP.RowStatus = CInt(DBRowStatus.Deleted)
                                    sProfileMapper.Update(cmP, m_userPrincipal.Identity.Name)
                                Next
                            End If
                        End If
                        For Each item As SPKProfile In objDomains
                            Dim oldProfile As SPKProfile = GetSPKProfile(item, item.ProfileGroup, item.ProfileHeader)
                            If oldProfile.ID > 0 Then
                                oldProfile.ProfileValue = item.ProfileValue
                                m_TransactionManager.AddUpdate(oldProfile, m_userPrincipal.Identity.Name)
                            Else
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



        Public Sub Delete(ByVal objDomain As SPKProfile)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPKProfileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SPKProfile) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SPKProfileMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function



#End Region

#Region "Custom Method"
        Private Function isProfileGroupChange(ByVal obSPKDetail As SPKDetail, ByVal obSPKProfile As SPKProfile, Optional ByRef ObProf As ArrayList = Nothing) As Boolean
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetail.ID", MatchType.Exact, obSPKDetail.ID))
                If obSPKProfile.SPKDetailCustomerID > 0 Then
                    criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomerID", MatchType.Exact, obSPKProfile.SPKDetailCustomerID))
                Else
                    criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomerID", MatchType.IsNull))
                End If

                Dim Coll As ArrayList = New ArrayList
                Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKProfile).ToString)

                Coll = sProfileMapper.RetrieveByCriteria(criterias)

                ObProf = Coll
                If (Coll.Count > 0) Then
                    For Each oSP As SPKProfile In Coll
                        If oSP.ProfileGroup.ID <> obSPKProfile.ProfileGroup.ID Then
                            Return True
                            Exit For
                        End If
                    Next
                End If
            Catch ex As Exception
                Dim b = 0
            End Try

            Return False
        End Function
#End Region

    End Class

End Namespace



