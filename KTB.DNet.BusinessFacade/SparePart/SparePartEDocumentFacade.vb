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
'// Generated on 2/1/2021 - 9:39:02 AM
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

    Public Class SparePartEDocumentFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartEDocumentMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartEDocumentMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartEDocument).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartEDocument
            Return CType(m_SparePartEDocumentMapper.Retrieve(ID), SparePartEDocument)
        End Function

        Public Function Retrieve(ByVal Code As String) As SparePartEDocument
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, Code))

            Dim SparePartEDocumentColl As ArrayList = m_SparePartEDocumentMapper.RetrieveByCriteria(criterias)
            If (SparePartEDocumentColl.Count > 0) Then
                Return CType(SparePartEDocumentColl(0), SparePartEDocument)
            End If
            Return New SparePartEDocument
        End Function

        Public Function Retrieve(ByVal DocNumber As String, ByVal DocType As Short) As SparePartEDocument
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, DocNumber))
            criterias.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, CType(DocType, Short)))

            Dim SparePartEDocumentColl As ArrayList = m_SparePartEDocumentMapper.RetrieveByCriteria(criterias)
            If (SparePartEDocumentColl.Count > 0) Then
                Return CType(SparePartEDocumentColl(0), SparePartEDocument)
            End If
            Return New SparePartEDocument
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartEDocumentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartEDocumentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartEDocumentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartEDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartEDocumentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartEDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartEDocumentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartEDocument As ArrayList = m_SparePartEDocumentMapper.RetrieveByCriteria(criterias)
            Return _SparePartEDocument
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartEDocumentColl As ArrayList = m_SparePartEDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartEDocumentColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SparePartEDocument), SortColumn, sortDirection))
            Dim SparePartEDocumentColl As ArrayList = m_SparePartEDocumentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartEDocumentColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartEDocumentColl As ArrayList = m_SparePartEDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartEDocumentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartEDocumentColl As ArrayList = m_SparePartEDocumentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartEDocument), columnName, matchOperator, columnValue))
            Return SparePartEDocumentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartEDocument), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), columnName, matchOperator, columnValue))

            Return m_SparePartEDocumentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "SparePartEDocumentCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartEDocument), "SparePartEDocumentCode", AggregateType.Count)
            Return CType(m_SparePartEDocumentMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartEDocument) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartEDocumentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartEDocument) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartEDocumentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartEDocument)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_SparePartEDocumentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartEDocument)
            Try
                m_SparePartEDocumentMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function DoRetrieveDataset(ByVal strSql As String) As DataSet
            Dim ds As New DataSet()
            ds = m_SparePartEDocumentMapper.RetrieveDataSet(strSql)
            Return ds
        End Function
#End Region

    End Class

End Namespace
