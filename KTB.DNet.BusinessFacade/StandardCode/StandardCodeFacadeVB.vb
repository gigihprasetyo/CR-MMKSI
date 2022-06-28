
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
'// Copyright  2017
'// ---------------------
'// $History      : $
'// Generated on 8/29/2017 - 10:58:19 AM
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

    Public Class StandardCodeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_StandardCodeMapper As IMapper
        Private m_StandardCodeCharMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_StandardCodeMapper = MapperFactory.GetInstance.GetMapper(GetType(StandardCode).ToString)
            Me.m_StandardCodeCharMapper = MapperFactory.GetInstance.GetMapper(GetType(StandardCodeChar).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As StandardCode
            Return CType(m_StandardCodeMapper.Retrieve(ID), StandardCode)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_StandardCodeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveChar(ByVal criterias As ICriteria) As ArrayList
            Return m_StandardCodeCharMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_StandardCodeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_StandardCodeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StandardCode), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StandardCodeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StandardCode), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StandardCodeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _StandardCode As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias)
            Return _StandardCode
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim StandardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return StandardCodeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            If IsNothing(Criterias) Then
                Criterias = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            End If

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StandardCode), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim StandardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return StandardCodeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim StandardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return StandardCodeColl
        End Function

        Public Function RetrieveByValueId(ByVal ValueId As String, ByVal Category As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, ValueId))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, Category))

            Dim StandardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias)
            Return StandardCodeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim StandardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias)
            Return StandardCodeColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(StandardCode), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim StandardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return StandardCodeColl

        End Function
        Public Function RetrieveByCategory(ByVal Category As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, Category))

            Dim standardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias)
            Return standardCodeColl
        End Function

        Public Function RetrieveByValueIdCategory(ByVal ValueId As String, ByVal category As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, ValueId))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.StartsWith, category))

            Dim standardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias)
            Return standardCodeColl
        End Function
        'cr sfid
        Public Function RetrieveByValueType(ByVal ValueType As Integer, ByVal category As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueType", MatchType.Exact, ValueType))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.StartsWith, category))

            Dim standardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias)
            Return standardCodeColl
        End Function
        '

        Public Function RetrieveHashByCategory(ByVal Category As String) As Hashtable
            Dim hashResult As Hashtable = New Hashtable
            Dim stdCodeColl As ArrayList = Me.RetrieveByCategory(Category)
            For Each stdCode As StandardCode In stdCodeColl
                hashResult.Add(stdCode.ValueId, stdCode.ValueDesc)
            Next
            Return hashResult
        End Function
        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim StandardCodeColl As ArrayList = m_StandardCodeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(StandardCode), columnName, matchOperator, columnValue))
            Return StandardCodeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StandardCode), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), columnName, matchOperator, columnValue))

            Return m_StandardCodeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function GetByCategoryValue(ByVal category As String, ByVal value As String) As StandardCode
            'defaultfilter togetthe Active Row Status only
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, value))

            Dim result As StandardCode = Nothing
            Dim totalRow As Integer = 0
            Try
                Dim data As New ArrayList
                data = m_StandardCodeMapper.RetrieveByCriteria(criterias)
                If (data.Count > 0) Then
                    result = CType(data(0), StandardCode)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return result
        End Function

        Public Function GetByCategoryValueCode(ByVal category As String, ByVal value As String) As StandardCode
            'defaultfilter togetthe Active Row Status only
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, value))

            Dim result As StandardCode = Nothing
            Dim totalRow As Integer = 0
            Try
                Dim data As New ArrayList
                data = m_StandardCodeMapper.RetrieveByCriteria(criterias)
                If (data.Count > 0) Then
                    result = CType(data(0), StandardCode)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return result
        End Function


        Public Function IsExistByCategoryDesc(ByVal category As String, ByVal desc As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, desc))
            Dim result As StandardCode = New StandardCode
            Dim totalRow As Integer = 0

            Try
                Dim data = m_StandardCodeMapper.RetrieveByCriteria(criterias, Nothing, 1, 1, totalRow)
                If (data.Count > 0) Then
                    Return True
                End If
            Catch ex As Exception
                Return False
            End Try
            Return False
        End Function

        Public Function IsExist(ByVal category As String, ByVal value As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, value))
            Dim result As StandardCode = New StandardCode
            Dim totalRow As Integer = 0

            Try
                Dim data = m_StandardCodeMapper.RetrieveByCriteria(criterias, Nothing, 1, 1, totalRow)
                If (data.Count > 0) Then
                    Return True
                End If
            Catch ex As Exception
                Return False
            End Try
            Return False
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As StandardCode) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_StandardCodeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As StandardCode) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_StandardCodeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As StandardCode)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_StandardCodeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As StandardCode) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_StandardCodeMapper.Delete(objDomain)
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
        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "Code", MatchType.Exact, Code))
            crit.opAnd(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim agg As Aggregate = New Aggregate(GetType(StandardCode), "Code", AggregateType.Count)

            Return CType(m_StandardCodeMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region

    End Class

End Namespace

