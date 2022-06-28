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
'// Copyright  2021
'// ---------------------
'// $History      : $
'// Generated on 10/15/2021 - 9:43:02 AM
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class SFServiceHistoryBookletFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SFServiceHistoryBookletMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SFServiceHistoryBookletMapper = MapperFactory.GetInstance.GetMapper(GetType(SFServiceHistoryBooklet).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SFServiceHistoryBooklet
            Return CType(m_SFServiceHistoryBookletMapper.Retrieve(ID), SFServiceHistoryBooklet)
        End Function

        Public Function Retrieve(ByVal Code As String) As SFServiceHistoryBooklet
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFServiceHistoryBooklet), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SFServiceHistoryBooklet), "SFServiceHistoryBookletCode", MatchType.Exact, Code))

            Dim SFServiceHistoryBookletColl As ArrayList = m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias)
            If (SFServiceHistoryBookletColl.Count > 0) Then
                Return CType(SFServiceHistoryBookletColl(0), SFServiceHistoryBooklet)
            End If
            Return New SFServiceHistoryBooklet
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SFServiceHistoryBookletMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFServiceHistoryBooklet), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SFServiceHistoryBookletMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFServiceHistoryBooklet), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SFServiceHistoryBookletMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFServiceHistoryBooklet), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SFServiceHistoryBooklet As ArrayList = m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias)
            Return _SFServiceHistoryBooklet
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFServiceHistoryBooklet), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SFServiceHistoryBookletColl As ArrayList = m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SFServiceHistoryBookletColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SFServiceHistoryBooklet), SortColumn, sortDirection))
            Dim SFServiceHistoryBookletColl As ArrayList = m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SFServiceHistoryBookletColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SFServiceHistoryBookletColl As ArrayList = m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SFServiceHistoryBookletColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFServiceHistoryBooklet), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SFServiceHistoryBookletColl As ArrayList = m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SFServiceHistoryBooklet), columnName, matchOperator, columnValue))
            Return SFServiceHistoryBookletColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFServiceHistoryBooklet), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFServiceHistoryBooklet), columnName, matchOperator, columnValue))

            Return m_SFServiceHistoryBookletMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetieveListOfItemToSend(ByVal flag As Integer) As ArrayList
            Dim result As New ArrayList

            result = m_SFServiceHistoryBookletMapper.RetrieveSP(String.Format("sp_SF_ParamServiceHistoryBooklet_Retrieve {0}", flag))
            If Not IsNothing(result) Then
                Return result
            Else
                Return New ArrayList
            End If


        End Function
#End Region


#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFServiceHistoryBooklet), "SFServiceHistoryBookletCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SFServiceHistoryBooklet), "SFServiceHistoryBookletCode", AggregateType.Count)
            Return CType(m_SFServiceHistoryBookletMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SFServiceHistoryBooklet) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SFServiceHistoryBookletMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SFServiceHistoryBooklet) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SFServiceHistoryBookletMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SFServiceHistoryBooklet)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SFServiceHistoryBookletMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SFServiceHistoryBooklet)
            Try
                m_SFServiceHistoryBookletMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
