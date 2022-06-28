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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.Dnet.BusinessFacade.SparePart
    Public Class AccessoriesSaleFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_AccessoriesSaleMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

        Private m_AccessoriesSaleDetailMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_AccessoriesSaleMapper = MapperFactory.GetInstance().GetMapper(GetType(AccessoriesSale).ToString)

            Me.m_AccessoriesSaleDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(AccessoriesSaleDetail).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AccessoriesSale
            Return CType(m_AccessoriesSaleMapper.Retrieve(ID), AccessoriesSale)
        End Function

        Public Function Retrieve(ByVal AccessoriesSaleCode As String) As AccessoriesSale
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AccessoriesSale), "AccessoriesSaleCode", MatchType.Exact, AccessoriesSaleCode))

            Dim AccessoriesSaleColl As ArrayList = m_AccessoriesSaleMapper.RetrieveByCriteria(criterias)
            If (AccessoriesSaleColl.Count > 0) Then
                Return CType(AccessoriesSaleColl(0), AccessoriesSale)
            End If
            Return New AccessoriesSale
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AccessoriesSaleMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AccessoriesSaleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AccessoriesSaleMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AccessoriesSale), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AccessoriesSaleMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AccessoriesSale), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AccessoriesSaleMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("AccessoriesSaleCode")) Then
                sortColl.Add(New Sort(GetType(AccessoriesSale), "AccessoriesSaleCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _AccessoriesSale As ArrayList = m_AccessoriesSaleMapper.RetrieveByCriteria(criterias, sortColl)
            Return _AccessoriesSale
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AccessoriesSaleColl As ArrayList = m_AccessoriesSaleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AccessoriesSaleColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AccessoriesSaleColl As ArrayList = m_AccessoriesSaleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AccessoriesSaleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AccessoriesSale), columnName, matchOperator, columnValue))
            Dim AccessoriesSaleColl As ArrayList = m_AccessoriesSaleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AccessoriesSaleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AccessoriesSale), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AccessoriesSale), columnName, matchOperator, columnValue))

            Return m_AccessoriesSaleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AccessoriesSale), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AccessoriesSaleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As AccessoriesSale) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_AccessoriesSaleMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Insert(ByVal objDomain As AccessoriesSale, ByVal aASDs As ArrayList) As Integer
            Dim iReturn As Integer = -2
            Dim iDetail As Integer = -2
            Try
                iReturn = m_AccessoriesSaleMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                For Each oASD As AccessoriesSaleDetail In aASDs
                    Dim oAS As New AccessoriesSale
                    oAS.ID = iReturn
                    oASD.AccessoriesSale = oAS
                    Try
                        iDetail = m_AccessoriesSaleDetailMapper.Insert(oASD, m_userPrincipal.Identity.Name)
                    Catch ex As Exception
                        iDetail = -1
                    End Try
                Next
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As AccessoriesSale) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AccessoriesSaleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As AccessoriesSale, ByVal aASDs As ArrayList) As Integer
            Dim nResult As Integer = -1
            Dim iDetail As Integer = -1
            Dim aASDsOri As ArrayList
            Dim oASOri As AccessoriesSale
            Dim IsExist As Boolean = False

            Try

                nResult = m_AccessoriesSaleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                If nResult > 0 Then
                    nResult = objDomain.ID
                End If
                '--Check Deleted
                oASOri = m_AccessoriesSaleMapper.Retrieve(objDomain.ID)
                aASDsOri = oASOri.AccessoriesSaleDetails
                For Each oASDOri As AccessoriesSaleDetail In aASDsOri
                    IsExist = False
                    For Each oASD As AccessoriesSaleDetail In aASDs
                        If oASDOri.ID = oASD.ID Then
                            IsExist = True
                            Exit For
                        End If
                    Next
                    If IsExist = False Then
                        iDetail = m_AccessoriesSaleDetailMapper.Delete(oASDOri)
                    End If
                Next
                'Update Details
                For Each oASD As AccessoriesSaleDetail In aASDs
                    Try
                        If oasd.ID > 0 Then
                            iDetail = m_AccessoriesSaleDetailMapper.Update(oASD, m_userPrincipal.Identity.Name)
                        Else
                            iDetail = m_AccessoriesSaleDetailMapper.Insert(oASD, m_userPrincipal.Identity.Name)
                        End If
                    Catch ex As Exception
                        iDetail = -1
                    End Try
                Next
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Sub Delete(ByVal objDomain As AccessoriesSale)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_AccessoriesSaleMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As AccessoriesSale)
            Try
                m_AccessoriesSaleMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "AccessoriesSaleCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(AccessoriesSale), "AccessoriesSaleCode", AggregateType.Count)

            Return CType(m_AccessoriesSaleMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AccessoriesSaleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AccessoriesSale), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AccessoriesSaleColl As ArrayList = m_AccessoriesSaleMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AccessoriesSaleColl
        End Function
        Public Function IsExist(ByVal Name As String) As Boolean
            Dim Criterias As New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Criterias.opAnd(New Criteria(GetType(AccessoriesSale), "Name", MatchType.Exact, Name))
            Dim AccessoriesSaleColl As ArrayList

            AccessoriesSaleColl = m_AccessoriesSaleMapper.RetrieveByCriteria(Criterias)

            Return (AccessoriesSaleColl.Count > 0)
        End Function
#End Region

    End Class

End Namespace
