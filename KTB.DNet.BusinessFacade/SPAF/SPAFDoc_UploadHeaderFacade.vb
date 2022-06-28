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

Namespace KTB.DNet.BusinessFacade.SPAF
    Public Class SPAFDoc_UploadHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_SPAFDoc_UploadHeaderMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SPAFDoc_UploadHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(SPAFDoc_UploadHeader).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SPAFDoc_UploadHeader
            Return CType(m_SPAFDoc_UploadHeaderMapper.Retrieve(ID), SPAFDoc_UploadHeader)
        End Function

        Public Function Retrieve(ByVal CreditAccount As String, ByVal PaymentType As Byte) As SPAFDoc_UploadHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), "CreditAccount", MatchType.Exact, CreditAccount))
            criterias.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), "PaymentType", MatchType.Exact, PaymentType))

            Dim SPAFDoc_UploadHeaderColl As ArrayList = m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias)
            If (SPAFDoc_UploadHeaderColl.Count > 0) Then
                Return CType(SPAFDoc_UploadHeaderColl(0), SPAFDoc_UploadHeader)
            End If
            Return New SPAFDoc_UploadHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPAFDoc_UploadHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPAFDoc_UploadHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPAFDoc_UploadHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPAFDoc_UploadHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPAFDoc_UploadHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("SPAFDoc_UploadHeaderCode")) Then
                sortColl.Add(New Sort(GetType(SPAFDoc_UploadHeader), "SPAFDoc_UploadHeaderCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _SPAFDoc_UploadHeader As ArrayList = m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias, sortColl)
            Return _SPAFDoc_UploadHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPAFDoc_UploadHeaderColl As ArrayList = m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPAFDoc_UploadHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SPAFDoc_UploadHeaderColl As ArrayList = m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPAFDoc_UploadHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), columnName, matchOperator, columnValue))
            Dim SPAFDoc_UploadHeaderColl As ArrayList = m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPAFDoc_UploadHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPAFDoc_UploadHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), columnName, matchOperator, columnValue))

            Return m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPAFDoc_UploadHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As SPAFDoc_UploadHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SPAFDoc_UploadHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SPAFDoc_UploadHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPAFDoc_UploadHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SPAFDoc_UploadHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPAFDoc_UploadHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SPAFDoc_UploadHeader)
            Try
                m_SPAFDoc_UploadHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "SPAFDoc_UploadHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SPAFDoc_UploadHeader), "SPAFDoc_UploadHeaderCode", AggregateType.Count)

            Return CType(m_SPAFDoc_UploadHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPAFDoc_UploadHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPAFDoc_UploadHeaderColl As ArrayList = m_SPAFDoc_UploadHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPAFDoc_UploadHeaderColl
        End Function

#End Region

    End Class

End Namespace
