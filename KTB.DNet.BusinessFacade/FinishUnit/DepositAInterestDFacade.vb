#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2008

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
'// Copyright  2008
'// ---------------------
'// $History      : $
'// Generated on 12/1/2008 - 16:07:00 PM
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

Public Class DepositAInterestDFacade
        Inherits AbstractFacade
    
#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositAInterestDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositAInterestDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.DepositAInterestD).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositAInterestD))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositAInterestD
            Return CType(m_DepositAInterestDetailMapper.Retrieve(ID), DepositAInterestD)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositAInterestDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_DepositAInterestDetailMapper.RetrieveScalar(agg, criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositAInterestDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositAInterestDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAInterestD), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAInterestDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAInterestD), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAInterestDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositAInterestD As ArrayList = m_DepositAInterestDetailMapper.RetrieveByCriteria(criterias)
            Return _DepositAInterestD
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAInterestDColl As ArrayList = m_DepositAInterestDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAInterestDColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositAInterestD), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositAInterestDColl As ArrayList = m_DepositAInterestDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositAInterestDColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositAInterestD), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositAInterestDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositAInterestDColl As ArrayList = m_DepositAInterestDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAInterestDColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAInterestDColl As ArrayList = m_DepositAInterestDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositAInterestD), columnName, matchOperator, columnValue))
            Return DepositAInterestDColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAInterestD), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAInterestD), columnName, matchOperator, columnValue))

            Return m_DepositAInterestDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Sub Insert(ByVal objDomain As DepositAInterestD)
        '    Dim iReturn As Integer = -2
        '    Try
        '        m_DepositAInterestDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim s As String = ex.Message
        '        iReturn = -1
        '    End Try

        'End Sub

        'Public Sub Update(ByVal objDomain As DepositAInterestD)
        '    Try
        '        m_DepositAInterestDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        'End Sub

#End Region

#Region "Custom Method"

        'Private Function RetrieveDepositAInterestD(ByVal objDetail As DepositAInterestD, ByVal headerID As Integer) As DepositAInterestD
        '    Dim _DepositAInterestDFacade As DepositAInterestDFacade
        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAInterestD), "DepositAInterestH.ID", MatchType.Exact, headerID))
        '    _DepositAInterestDFacade = New DepositAInterestDFacade(System.Threading.Thread.CurrentPrincipal)
        '    Dim arlDepositAInterestD As ArrayList = _DepositAInterestDFacade.Retrieve(criterias)
        '    If arlDepositAInterestD.Count > 0 Then
        '        Dim objDepositAInterestD As DepositAInterestD = CType(arlDepositAInterestD(0), DepositAInterestD)
        '        objDepositAInterestD.DealerCode = objDetail.DealerCode
        '        objDepositAInterestD.Month = objDetail.Month
        '        objDepositAInterestD.Year = objDetail.Year
        '        objDepositAInterestD.InterestAmount = objDetail.InterestAmount
        '        objDepositAInterestD.NettoAmount = objDetail.NettoAmount
        '        objDepositAInterestD.DealerCode = objDetail.DealerCode
        '        Return objDepositAInterestD
        '    Else
        '        Return objDetail
        '    End If
        'End Function

        'Public Sub SynchronizeDepositAInterestD(ByVal objDetail As DepositAInterestD, ByVal objHeader As DepositAInterestH)
        '    objDetail = RetrieveDepositAInterestD(objDetail, objHeader.ID)
        '    Try
        '        If objDetail.ID > 0 Then
        '            Update(objDetail)
        '        Else
        '            objDetail.DepositAInterestH = objHeader
        '            Insert(objDetail)
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Sub

#End Region

End Class

End Namespace
