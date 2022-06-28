
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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 1:07:25 PM
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

Namespace KTB.Dnet.BusinessFacade.Sparepart

    Public Class PartIncidentalDetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PartIncidentalDetailMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PartIncidentalDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PartIncidentalDetail).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PartIncidentalDetail
            Return CType(m_PartIncidentalDetailMapper.Retrieve(ID), PartIncidentalDetail)
        End Function

        'Public Function Retrieve(ByVal Code As String) As PartIncidentalDetail
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalDetailCode", MatchType.Exact, Code))

        '    Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias)
        '    If (PartIncidentalDetailColl.Count > 0) Then
        '        Return CType(PartIncidentalDetailColl(0), PartIncidentalDetail)
        '    End If
        '    Return New PartIncidentalDetail
        'End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PartIncidentalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PartIncidentalDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PartIncidentalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PartIncidentalDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PartIncidentalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PartIncidentalDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveFilteredActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PartIncidentalDetail As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias)
            
            Return _PartIncidentalDetail
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PartIncidentalDetail As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias)
            Return _PartIncidentalDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PartIncidentalDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Integer) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PartIncidentalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PartIncidentalDetailColl
        End Function

        Public Function RetrieveFilteredActiveList(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Integer) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PartIncidentalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Dim FilteredList As ArrayList = New ArrayList
            For Each item As PartIncidentalDetail In PartIncidentalDetailColl
                If item.RemainQuantity > 0 Then
                    FilteredList.Add(item)
                End If
            Next
            Return FilteredList
        End Function

        Public Function RetrieveFilteredActiveList(ByVal criterias As CriteriaComposite) As ArrayList
            Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias)
            Dim FilteredList As ArrayList = New ArrayList
            For Each item As PartIncidentalDetail In PartIncidentalDetailColl
                If item.RemainQuantity > 0 Then
                    FilteredList.Add(item)
                End If
            Next
            Return FilteredList
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PartIncidentalDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PartIncidentalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, sortColl)
            Return PartIncidentalDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, _
            ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PartIncidentalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PartIncidentalDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PartIncidentalDetailColl As ArrayList = m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), columnName, matchOperator, columnValue))
            Return PartIncidentalDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PartIncidentalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), columnName, matchOperator, columnValue))

            Return m_PartIncidentalDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Insert(ByVal objDomain As PartIncidentalDetail)
            Dim iReturn As Integer = -2
            Try
                m_PartIncidentalDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try

        End Sub

        Public Sub Update(ByVal objDomain As PartIncidentalDetail)
            Try
                m_PartIncidentalDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Update(ByVal objDomain As ArrayList)
            Try
                For Each item As PartIncidentalDetail In objDomain
                    m_PartIncidentalDetailMapper.Update(item, m_userPrincipal.Identity.Name)
                Next
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As PartIncidentalDetail)
            Try
                m_PartIncidentalDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal arrList As ArrayList)
            Try
                For Each item As PartIncidentalDetail In arrList
                    m_PartIncidentalDetailMapper.Delete(item)
                Next
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PartIncidentalDetail), "PartIncidentalDetailCode", AggregateType.Count)
            Return CType(m_PartIncidentalDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

