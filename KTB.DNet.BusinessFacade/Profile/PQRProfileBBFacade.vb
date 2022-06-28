
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

    Public Class PQRProfileBBFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PQRProfileBBMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PQRProfileBBMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRProfileBB).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PQRProfileBB))
        End Sub

#End Region

#Region "Retrieve"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PQRProfileBB) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRProfileBB).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRProfileBB).MarkLoaded()
            End If
        End Sub

        Public Function Retrieve(ByVal ID As Integer) As PQRProfileBB
            Return CType(m_PQRProfileBBMapper.Retrieve(ID), PQRProfileBB)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PQRProfileBBMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PQRProfileBBMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PQRProfileBBMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRProfileBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRProfileBBMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRProfileBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRProfileBBMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PQRProfileBB As ArrayList = m_PQRProfileBBMapper.RetrieveByCriteria(criterias)
            Return _PQRProfileBB
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRProfileBBColl As ArrayList = m_PQRProfileBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PQRProfileBBColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(PQRProfileBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim PQRProfileBBColl As ArrayList = m_PQRProfileBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PQRProfileBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRProfileBBColl As ArrayList = m_PQRProfileBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PQRProfileBB), columnName, matchOperator, columnValue))
            Return PQRProfileBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRProfileBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), columnName, matchOperator, columnValue))

            Return m_PQRProfileBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"

        Public Function GetProfileValue(ByVal ProfileHeaderCode As String, ByVal objPQRHeaderBB As PQRHeaderBB) As String
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRProfileBB), "PQRHeaderBB.ID", MatchType.Exact, objPQRHeaderBB.ID.ToString))
            'criterias.opAnd(New Criteria(GetType(PQRProfileBB), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(PQRProfileBB), "ProfileHeader.Code", MatchType.Exact, ProfileHeaderCode))

            Dim PQRProfileBBColl As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRProfileBB).ToString)

            PQRProfileBBColl = sProfileMapper.RetrieveByCriteria(criterias)
            If (PQRProfileBBColl.Count > 0) Then
                Return CType(PQRProfileBBColl(0), PQRProfileBB).ProfileValue
            End If
            Return ""
        End Function

        Public Function GetProfileValueDetail(ByVal ProfileHeaderCode As String, ByVal objPQRHeaderBB As PQRHeaderBB) As String
            Dim strProfileValue As String = GetProfileValue(ProfileHeaderCode, objPQRHeaderBB)
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


        Public Function GetPQRProfileBB(ByVal objPQRProfileBB As PQRProfileBB, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As PQRProfileBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRProfileBB), "PQRProfileBB.ID", MatchType.Exact, objPQRProfileBB.ID))
            criterias.opAnd(New Criteria(GetType(PQRProfileBB), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(PQRProfileBB), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))

            Dim PQRProfileBBColl As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRProfileBB).ToString)

            PQRProfileBBColl = sProfileMapper.RetrieveByCriteria(criterias)
            If (PQRProfileBBColl.Count > 0) Then
                Return CType(PQRProfileBBColl(0), PQRProfileBB)
            End If
            Return New PQRProfileBB
        End Function

        Public Function Insert(ByVal objDomain As PQRProfileBB) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PQRProfileBBMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As PQRProfileBB) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PQRProfileBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As PQRProfileBB)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PQRProfileBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As PQRProfileBB) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_PQRProfileBBMapper.Delete(objDomain)
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




