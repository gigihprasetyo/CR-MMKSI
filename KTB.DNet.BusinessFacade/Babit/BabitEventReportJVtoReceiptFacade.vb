
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
'// Generated on 03/10/2019 - 13:55:23
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
Imports System.Linq


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class BabitEventReportJVtoReceiptFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitEventReportJVtoReceiptMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitEventReportJVtoReceiptMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitEventReportJVtoReceipt).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(BabitEventReportJV))
            Me.DomainTypeCollection.Add(GetType(BabitEventReportJVtoReceipt))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitEventReportJVtoReceipt
            Return CType(m_BabitEventReportJVtoReceiptMapper.Retrieve(ID), BabitEventReportJVtoReceipt)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitEventReportJVtoReceipt
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventReportJVtoReceipt), "BabitEventReportJVtoReceiptCode", MatchType.Exact, Code))

            Dim BabitEventReportJVtoReceiptColl As ArrayList = m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
            If (BabitEventReportJVtoReceiptColl.Count > 0) Then
                Return CType(BabitEventReportJVtoReceiptColl(0), BabitEventReportJVtoReceipt)
            End If
            Return New BabitEventReportJVtoReceipt
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitEventReportJVtoReceiptMapper.RetrieveList
        End Function

        Public Function RetrieveByBabitEventReportJV(ByVal BabitEventReportJVID As Integer) As BabitEventReportJVtoReceipt
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventReportJVtoReceipt), "BabitEventReportJV.ID", MatchType.Exact, BabitEventReportJVID))

            Dim BabitEventReportJVtoReceiptColl As ArrayList = m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
            If (BabitEventReportJVtoReceiptColl.Count > 0) Then
                Return CType(BabitEventReportJVtoReceiptColl(0), BabitEventReportJVtoReceipt)
            End If
            Return New BabitEventReportJVtoReceipt
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventReportJVtoReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitEventReportJVtoReceiptMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventReportJVtoReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitEventReportJVtoReceiptMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitEventReportJVtoReceipt As ArrayList = m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
            Return _BabitEventReportJVtoReceipt
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitEventReportJVtoReceiptColl As ArrayList = m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitEventReportJVtoReceiptColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitEventReportJVtoReceipt), SortColumn, sortDirection))
            Dim BabitEventReportJVtoReceiptColl As ArrayList = m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitEventReportJVtoReceiptColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitEventReportJVtoReceiptColl As ArrayList = m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitEventReportJVtoReceiptColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitEventReportJVtoReceiptColl As ArrayList = m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitEventReportJVtoReceipt), columnName, matchOperator, columnValue))
            Return BabitEventReportJVtoReceiptColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventReportJVtoReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), columnName, matchOperator, columnValue))

            Return m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "BabitEventReportJVtoReceiptCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitEventReportJVtoReceipt), "BabitEventReportJVtoReceiptCode", AggregateType.Count)
            Return CType(m_BabitEventReportJVtoReceiptMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitEventReportJVtoReceipt) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitEventReportJVtoReceiptMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitEventReportJVtoReceipt) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitEventReportJVtoReceiptMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitEventReportJVtoReceipt)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitEventReportJVtoReceiptMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitEventReportJVtoReceipt)
            Try
                m_BabitEventReportJVtoReceiptMapper.Delete(objDomain)
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
            If (TypeOf InsertArg.DomainObject Is BabitEventReportJV) Then
                CType(InsertArg.DomainObject, BabitEventReportJV).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitEventReportJV).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BabitEventReportJVtoReceipt) Then
                CType(InsertArg.DomainObject, BabitEventReportJVtoReceipt).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitEventReportJVtoReceipt).MarkLoaded()
            End If
        End Sub

        Public Function RetrieveArrBabitEventReportReceipt(ByVal BabitEventReportJVID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventReportJVtoReceipt), "BabitEventReportJV.ID", MatchType.Exact, BabitEventReportJVID))
            Return m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias)
        End Function

        Public Sub RetrieveOldestDate(ByVal BabitEventReportJVID As Integer, ByRef FakturPajakDate As Date, ByRef ReceiptDate As Date)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventReportJVtoReceipt), "BabitEventReportJV.ID", MatchType.Exact, BabitEventReportJVID))

            Dim BabitEventReportJVtoReceiptColl As ArrayList = m_BabitEventReportJVtoReceiptMapper.RetrieveByCriteria(criterias)

            Dim ASD = From jvToRecipe In BabitEventReportJVtoReceiptColl
                      Order By jvToRecipe.BabitEventReportReceipt.FakturPajakDate Ascending

            Dim ZXC = From jvToRecipe In BabitEventReportJVtoReceiptColl
                      Order By jvToRecipe.BabitEventReportReceipt.ReceiptDate Ascending

            Dim arrASD As New ArrayList
            For Each v As BabitEventReportJVtoReceipt In ASD
                arrASD.Add(v)
            Next

            Dim arrZXC As New ArrayList
            For Each v As BabitEventReportJVtoReceipt In ZXC
                arrZXC.Add(v)
            Next

            If (arrASD.Count > 0) Then
                FakturPajakDate = CType(arrASD(0), BabitEventReportJVtoReceipt).BabitEventReportReceipt.FakturPajakDate
            End If

            If (arrZXC.Count > 0) Then
                ReceiptDate = CType(arrZXC(0), BabitEventReportJVtoReceipt).BabitEventReportReceipt.ReceiptDate
            End If
        End Sub

        Public Function InsertTransaction(ByVal arrReceipt As ArrayList, ByVal arrJV As ArrayList, ByVal arrJVtoReceipt As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim oBabitEventReportJV As New BabitEventReportJV
                    If Not IsNothing(arrJV) Then
                        If arrJV.Count > 0 Then
                            For i As Integer = 0 To arrJV.Count - 1
                                oBabitEventReportJV = CType(arrJV(i), BabitEventReportJV)
                                m_TransactionManager.AddInsert(oBabitEventReportJV, m_userPrincipal.Identity.Name)

                                If Not IsNothing(arrJVtoReceipt) Then
                                    If arrJVtoReceipt.Count > 0 Then
                                        For j As Integer = i To arrJVtoReceipt.Count - 1
                                            CType(arrJVtoReceipt(j), BabitEventReportJVtoReceipt).BabitEventReportJV = oBabitEventReportJV
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
                            For Each oBabitEventReportReceipt As BabitEventReportReceipt In arrReceipt
                                m_TransactionManager.AddUpdate(oBabitEventReportReceipt, m_userPrincipal.Identity.Name)
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
#End Region

        

    End Class

End Namespace

