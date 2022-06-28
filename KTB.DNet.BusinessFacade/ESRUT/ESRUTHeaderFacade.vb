
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 10/07/2019 - 10:11:23
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

    Public Class ESRUTHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ESRUTHeaderMapper As IMapper
        Private ID_Insert As Integer
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ESRUTHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(ESRUTHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ESRUTHeader
            Return CType(m_ESRUTHeaderMapper.Retrieve(ID), ESRUTHeader)
        End Function

        Public Function Retrieve(ByVal NoPengajuan As String) As ESRUTHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ESRUTHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ESRUTHeader), "NoPengajuan", MatchType.Exact, NoPengajuan))

            Dim ESRUTHeaderColl As ArrayList = m_ESRUTHeaderMapper.RetrieveByCriteria(criterias)
            If (ESRUTHeaderColl.Count > 0) Then
                Return CType(ESRUTHeaderColl(0), ESRUTHeader)
            End If
            Return New ESRUTHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ESRUTHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ESRUTHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ESRUTHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ESRUTHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ESRUTHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ESRUTHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ESRUTHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ESRUTHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ESRUTHeader As ArrayList = m_ESRUTHeaderMapper.RetrieveByCriteria(criterias)
            Return _ESRUTHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ESRUTHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ESRUTHeaderColl As ArrayList = m_ESRUTHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ESRUTHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(ESRUTHeader), SortColumn, sortDirection))
            Dim ESRUTHeaderColl As ArrayList = m_ESRUTHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ESRUTHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ESRUTHeaderColl As ArrayList = m_ESRUTHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ESRUTHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ESRUTHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ESRUTHeaderColl As ArrayList = m_ESRUTHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ESRUTHeader), columnName, matchOperator, columnValue))
            Return ESRUTHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ESRUTHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ESRUTHeader), columnName, matchOperator, columnValue))

            Return m_ESRUTHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ESRUTHeader), "ESRUTHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ESRUTHeader), "ESRUTHeaderCode", AggregateType.Count)
            Return CType(m_ESRUTHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.ESRUTHeader) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.ESRUTHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.ESRUTHeader).MarkLoaded()
                ID_Insert = InsertArg.ID
            End If
        End Sub

        Public Function Insert(ByVal objDomain As ESRUTHeader) As Integer
            Dim iReturn As Integer = -2

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As ESRUTItem In objDomain.ListOfItem
                        item.ESRUTHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.PerformTransaction()
                    iReturn = ID_Insert

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        iReturn = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try


            End If

            Return iReturn

        End Function

        Public Function Reupload(ByVal objDomain As ESRUTHeader) As Integer
            Dim iReturn As Integer = -2

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As ESRUTItem In objDomain.ListOfItem
                        item.ESRUTHeader = objDomain
                        Dim existingValidItemData As ESRUTItem = GetExistingItemData(item.ChassisNumber)

                        'Jika sudah ada chassisnumber yg esrutnya diupload dan valid , tidak perlu diapa2kan
                        If existingValidItemData.ID = 0 Then
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Else
                            If existingValidItemData.Status <> CInt(EnumESRUT.ESRUTItemStatus.VALID) Then
                                existingValidItemData.RowStatus = CType(DBRowStatus.Deleted, Short)
                                item.IsRevision = True
                                m_TransactionManager.AddUpdate(existingValidItemData, m_userPrincipal.Identity.Name)
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        End If

                    Next

                    m_TransactionManager.PerformTransaction()
                    iReturn = ID_Insert

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        iReturn = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try


            End If

            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ESRUTHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ESRUTHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ESRUTHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ESRUTHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As ESRUTHeader)
            Try
                m_ESRUTHeaderMapper.Delete(objDomain)
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

        Private Function GetExistingItemData(ByVal chassisNumber As String) As ESRUTItem
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ESRUTItem), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ESRUTItem), "ChassisNumber", MatchType.Exact, chassisNumber))


            Dim itemFacade As ESRUTItemFacade = New ESRUTItemFacade(m_userPrincipal)

            Dim col As ArrayList = itemFacade.Retrieve(criterias)
            If (col.Count > 0) Then
                Return CType(col(0), ESRUTItem)
            End If
            Return New ESRUTItem
        End Function

    End Class

End Namespace

