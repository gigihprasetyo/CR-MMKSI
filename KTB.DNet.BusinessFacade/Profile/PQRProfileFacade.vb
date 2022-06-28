
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

Namespace KTB.DNet.BusinessFacade.Profile

    Public Class PQRProfileFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PQRProfileMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PQRProfileMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRProfile).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PQRProfile))
        End Sub

#End Region

#Region "Retrieve"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PQRProfile) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRProfile).MarkLoaded()
            End If
        End Sub

        Public Function Retrieve(ByVal ID As Integer) As PQRProfile
            Return CType(m_PQRProfileMapper.Retrieve(ID), PQRProfile)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PQRProfileMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PQRProfileMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PQRProfileMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRProfileMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRProfileMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PQRProfile As ArrayList = m_PQRProfileMapper.RetrieveByCriteria(criterias)
            Return _PQRProfile
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRProfileColl As ArrayList = m_PQRProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PQRProfileColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(PQRProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim PQRProfileColl As ArrayList = m_PQRProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PQRProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRProfileColl As ArrayList = m_PQRProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PQRProfile), columnName, matchOperator, columnValue))
            Return PQRProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfile), columnName, matchOperator, columnValue))

            Return m_PQRProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"

        Public Function GetProfileValue(ByVal ProfileHeaderCode As String, ByVal objPQRHeader As PQRHeader) As String
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRProfile), "PQRHeader.ID", MatchType.Exact, objPQRHeader.ID.ToString))
            'criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileHeader.Code", MatchType.Exact, ProfileHeaderCode))

            Dim PQRProfileColl As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRProfile).ToString)

            PQRProfileColl = sProfileMapper.RetrieveByCriteria(criterias)
            If (PQRProfileColl.Count > 0) Then
                Return CType(PQRProfileColl(0), PQRProfile).ProfileValue
            End If
            Return ""
        End Function

        Public Function GetProfileValueDetail(ByVal ProfileHeaderCode As String, ByVal objPQRHeader As PQRHeader) As String
            Dim strProfileValue As String = GetProfileValue(ProfileHeaderCode, objPQRHeader)
            If strProfileValue = "" Then
                Return ""
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.Code", MatchType.Exact, ProfileHeaderCode))
            criterias.opAnd(New Criteria(GetType(ProfileDetail), "Code", MatchType.Exact, strProfileValue))

            Dim ProfileDetailColl As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(ProfileDetail).ToString)

            ProfileDetailColl = sProfileMapper.RetrieveByCriteria(criterias)
            If (ProfileDetailColl.Count > 0) Then
                Return CType(ProfileDetailColl(0), ProfileDetail).Description
            End If
            Return ""


        End Function


        Public Function GetPQRProfile(ByVal objPQRProfile As PQRProfile, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As PQRProfile
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRProfile), "PQRProfile.ID", MatchType.Exact, objPQRProfile.ID))
            criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))

            Dim PQRProfileColl As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRProfile).ToString)

            PQRProfileColl = sProfileMapper.RetrieveByCriteria(criterias)
            If (PQRProfileColl.Count > 0) Then
                Return CType(PQRProfileColl(0), PQRProfile)
            End If
            Return New PQRProfile
        End Function

        Public Function Insert(ByVal objDomain As PQRProfile) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PQRProfileMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As PQRProfile) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PQRProfileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As PQRProfile)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PQRProfileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As PQRProfile) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_PQRProfileMapper.Delete(objDomain)
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

#End Region

    End Class

End Namespace




