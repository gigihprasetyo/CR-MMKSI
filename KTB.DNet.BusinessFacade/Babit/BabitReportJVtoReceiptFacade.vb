
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
'// Generated on 02/10/2019 - 14:03:52
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic
Imports System.Linq

#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class BabitReportJVtoReceiptFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitReportJVtoReceiptMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitReportJVtoReceiptMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitReportJVtoReceipt).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(BabitReportJVtoReceipt))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitReportJVtoReceipt
            Return CType(m_BabitReportJVtoReceiptMapper.Retrieve(ID), BabitReportJVtoReceipt)
        End Function

        Public Function RetrieveByBabitReportJV(ByVal BabitReportJVID As Integer) As BabitReportJVtoReceipt
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportJV.ID", MatchType.Exact, BabitReportJVID))

            Dim BabitReportJVtoReceiptColl As ArrayList = m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
            If (BabitReportJVtoReceiptColl.Count > 0) Then
                Return CType(BabitReportJVtoReceiptColl(0), BabitReportJVtoReceipt)
            End If
            Return New BabitReportJVtoReceipt
        End Function

        Public Function RetrieveBabitReportReceipt(ByVal BabitReportReceiptID As Integer) As BabitReportJVtoReceipt
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportReceipt.ID", MatchType.Exact, BabitReportReceiptID))

            Dim BabitReportJVtoReceiptColl As ArrayList = m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
            If (BabitReportJVtoReceiptColl.Count > 0) Then
                Return CType(BabitReportJVtoReceiptColl(0), BabitReportJVtoReceipt)
            End If
            Return New BabitReportJVtoReceipt
        End Function

        Public Function RetrieveArrBabitReportReceipt(ByVal BabitReportJVID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportJV.ID", MatchType.Exact, BabitReportJVID))
            Return m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitReportJVtoReceiptMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportJVtoReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitReportJVtoReceiptMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportJVtoReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitReportJVtoReceiptMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitReportJVtoReceipt As ArrayList = m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
            Return _BabitReportJVtoReceipt
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitReportJVtoReceiptColl As ArrayList = m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitReportJVtoReceiptColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitReportJVtoReceipt), SortColumn, sortDirection))
            Dim BabitReportJVtoReceiptColl As ArrayList = m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitReportJVtoReceiptColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitReportJVtoReceiptColl As ArrayList = m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitReportJVtoReceiptColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitReportJVtoReceiptColl As ArrayList = m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), columnName, matchOperator, columnValue))
            Return BabitReportJVtoReceiptColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportJVtoReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), columnName, matchOperator, columnValue))

            Return m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportJVtoReceiptCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitReportJVtoReceipt), "BabitReportJVtoReceiptCode", AggregateType.Count)
            Return CType(m_BabitReportJVtoReceiptMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitReportJVtoReceipt) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitReportJVtoReceiptMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitReportJVtoReceipt) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitReportJVtoReceiptMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitReportJVtoReceipt)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitReportJVtoReceiptMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitReportJVtoReceipt)
            Try
                m_BabitReportJVtoReceiptMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is BabitReportJV) Then
                CType(InsertArg.DomainObject, BabitReportJV).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitReportJV).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BabitReportJVtoReceipt) Then
                CType(InsertArg.DomainObject, BabitReportJVtoReceipt).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitReportJVtoReceipt).MarkLoaded()
            End If
        End Sub

        Public Function InsertTransaction(ByVal arrReceipt As ArrayList, ByVal arrJV As ArrayList, ByVal arrJVtoReceipt As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim oBabitReportJV As New BabitReportJV
                    If Not IsNothing(arrJV) Then
                        If arrJV.Count > 0 Then
                            For i As Integer = 0 To arrJV.Count - 1
                                oBabitReportJV = CType(arrJV(i), BabitReportJV)
                                m_TransactionManager.AddInsert(oBabitReportJV, m_userPrincipal.Identity.Name)

                                If Not IsNothing(arrJVtoReceipt) Then
                                    If arrJVtoReceipt.Count > 0 Then
                                        For j As Integer = i To arrJVtoReceipt.Count - 1
                                            CType(arrJVtoReceipt(j), BabitReportJVtoReceipt).BabitReportJV = oBabitReportJV
                                            m_TransactionManager.AddInsert(arrJVtoReceipt(j), m_userPrincipal.Identity.Name)
                                            Exit For
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arrReceipt) Then
                        If arrReceipt.Count > 0 Then
                            For Each oBabitReportReceipt As BabitReportReceipt In arrReceipt
                                m_TransactionManager.AddUpdate(oBabitReportReceipt, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = 7
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Sub RetrieveOldestDate(ByVal BabitReportJVID As Integer, ByRef FakturPajakDate As Date, ByRef ReceiptDate As Date)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportJV.ID", MatchType.Exact, BabitReportJVID))

            Dim BabitReportJVtoReceiptColl As ArrayList = m_BabitReportJVtoReceiptMapper.RetrieveByCriteria(criterias)

            Dim ASD = From jvToRecipe In BabitReportJVtoReceiptColl
                      Order By jvToRecipe.BabitReportReceipt.FakturPajakDate Ascending

            Dim ZXC = From jvToRecipe In BabitReportJVtoReceiptColl
                      Order By jvToRecipe.BabitReportReceipt.ReceiptDate Ascending

            Dim arrASD As New ArrayList
            For Each v As BabitReportJVtoReceipt In ASD
                arrASD.Add(v)
            Next

            Dim arrZXC As New ArrayList
            For Each v As BabitReportJVtoReceipt In ZXC
                arrZXC.Add(v)
            Next

            If (arrASD.Count > 0) Then
                FakturPajakDate = CType(arrASD(0), BabitReportJVtoReceipt).BabitReportReceipt.FakturPajakDate
            End If

            If (arrZXC.Count > 0) Then
                ReceiptDate = CType(arrZXC(0), BabitReportJVtoReceipt).BabitReportReceipt.ReceiptDate
            End If
        End Sub
#End Region

    End Class

End Namespace

