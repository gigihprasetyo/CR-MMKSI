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
'// Generated on 8/9/2021 - 10:54:09 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Linq
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class InterestPPHHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_InterestPPHHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_InterestPPHHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(InterestPPHHeader).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As InterestPPHHeader
            Return CType(m_InterestPPHHeaderMapper.Retrieve(ID), InterestPPHHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As InterestPPHHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InterestPPHHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(InterestPPHHeader), "InterestPPHHeaderCode", MatchType.Exact, Code))

            Dim InterestPPHHeaderColl As ArrayList = m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias)
            If (InterestPPHHeaderColl.Count > 0) Then
                Return CType(InterestPPHHeaderColl(0), InterestPPHHeader)
            End If
            Return New InterestPPHHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_InterestPPHHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(InterestPPHHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_InterestPPHHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(InterestPPHHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_InterestPPHHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InterestPPHHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _InterestPPHHeader As ArrayList = m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias)
            Return _InterestPPHHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InterestPPHHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim InterestPPHHeaderColl As ArrayList = m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return InterestPPHHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(InterestPPHHeader), SortColumn, sortDirection))
            Dim InterestPPHHeaderColl As ArrayList = m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return InterestPPHHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim InterestPPHHeaderColl As ArrayList = m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return InterestPPHHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InterestPPHHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim InterestPPHHeaderColl As ArrayList = m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(InterestPPHHeader), columnName, matchOperator, columnValue))
            Return InterestPPHHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(InterestPPHHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InterestPPHHeader), columnName, matchOperator, columnValue))

            Return m_InterestPPHHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(InterestPPHHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim objColl As ArrayList = m_InterestPPHHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return objColl
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InterestPPHHeader), "InterestPPHHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(InterestPPHHeader), "InterestPPHHeaderCode", AggregateType.Count)
            Return CType(m_InterestPPHHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As InterestPPHHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_InterestPPHHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                If objDomain.InterestPPHDetails.Count > 0 And iReturn > 0 Then
                    For Each obj As InterestPPHDetail In objDomain.InterestPPHDetails
                        obj.InterestPPHHeader = New InterestPPHHeader(iReturn)
                        Dim res As Integer = New InterestPPHDetailFacade(m_userPrincipal).Insert(obj)
                    Next
                End If
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As InterestPPHHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_InterestPPHHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                If objDomain.InterestPPHDetailsNew.Count > 0 And nResult > 0 Then
                    Dim newInsert As New ArrayList()
                    Dim delete As New ArrayList()
                    'ambil data baru
                    For Each newobj As InterestPPHDetail In objDomain.InterestPPHDetailsNew
                        Dim newDataFlag = New ArrayList((From obj As InterestPPHDetail In objDomain.InterestPPHDetails.OfType(Of InterestPPHDetail)()
                                                  Where obj.SalesOrderInterest.ID = newobj.SalesOrderInterest.ID).ToList())
                        If newDataFlag.Count = 0 Then
                            newInsert.Add(newobj)
                        End If
                    Next
                    'ambil data yg dihapus
                    For Each delobj As InterestPPHDetail In objDomain.InterestPPHDetails
                        Dim newDataFlag = New ArrayList((From obj As InterestPPHDetail In objDomain.InterestPPHDetailsNew.OfType(Of InterestPPHDetail)()
                                                  Where obj.SalesOrderInterest.ID = delobj.SalesOrderInterest.ID).ToList())
                        If newDataFlag.Count = 0 Then
                            delete.Add(delobj)
                        End If
                    Next
                    Dim updateInsertCutOff As Integer = 0
                    'update data yg dihapus dengan SOInterest baru. secukupnya saja dan kalau ada yang dihapus. dan juga update SOInterest status ke -1
                    Dim arrUpdate As New ArrayList()
                    If delete.Count > 0 Then
                        For updateInsertCutOff = 0 To newInsert.Count - 1
                            If updateInsertCutOff < delete.Count Then
                                Dim objToUpdate As InterestPPHDetail = delete(updateInsertCutOff)
                                updateSOInterestStatus(objToUpdate.SalesOrderInterest, -1, Nothing)
                                objToUpdate.SalesOrderInterest = CType(newInsert(updateInsertCutOff), InterestPPHDetail).SalesOrderInterest
                                arrUpdate.Add(objToUpdate)
                            Else
                                Exit For
                            End If
                        Next
                    End If

                    Dim intPPHDetFacade As InterestPPHDetailFacade = New InterestPPHDetailFacade(m_userPrincipal)
                    'update data lama dengan SOInterest baru

                    For Each objUpdate As InterestPPHDetail In arrUpdate
                        Dim res = intPPHDetFacade.Update(objUpdate)
                        updateSOInterestStatus(objUpdate.SalesOrderInterest, 0, objDomain)
                    Next
                    'untuk data baru yg tidak teralokasi ke data lama maka kan insert baru.
                    If updateInsertCutOff < newInsert.Count Then
                        For i As Integer = updateInsertCutOff To newInsert.Count - 1
                            Dim objToInsert As InterestPPHDetail = CType(newInsert(i), InterestPPHDetail)
                            Dim res = intPPHDetFacade.Insert(objToInsert)
                            updateSOInterestStatus(objToInsert.SalesOrderInterest, 0, objDomain)
                        Next
                    End If
                    'jika hanya hapus saja
                    If newInsert.Count = 0 Then
                        For Each del As InterestPPHDetail In delete
                            intPPHDetFacade.Delete(del)
                        Next
                    End If

                End If
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As InterestPPHHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_InterestPPHHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As InterestPPHHeader)
            Try
                m_InterestPPHHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Function updateSOInterestStatus(ByVal soi As SalesOrderInterest, ByVal stat As Short, ByVal objIntPPHHeader As InterestPPHHeader) As Integer
            soi.Status = stat
            If stat = -1 Then
                'soi.InterestPPHHeader = Nothing
            Else
                soi.InterestPPHHeader = objIntPPHHeader
            End If
            Dim res = New SalesOrderInterestFacade(m_userPrincipal).Update(soi)
        End Function
#End Region

    End Class

End Namespace
