
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
'// Copyright  2015
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 11:19:01 AM
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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Benefit

    Public Class BenefitClaimReceiptFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitClaimReceiptMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitClaimReceiptMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitClaimReceipt).ToString)

            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BenefitClaimReceipt
            Return CType(m_BenefitClaimReceiptMapper.Retrieve(ID), BenefitClaimReceipt)
        End Function

        Public Function Retrieve(ByVal Code As String) As BenefitClaimReceipt
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimReceiptCode", MatchType.Exact, Code))

            Dim BenefitClaimReceiptColl As ArrayList = m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias)
            If (BenefitClaimReceiptColl.Count > 0) Then
                Return CType(BenefitClaimReceiptColl(0), BenefitClaimReceipt)
            End If
            Return New BenefitClaimReceipt
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitClaimReceiptMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitClaimReceiptMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitClaimReceiptMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BenefitClaimReceipt As ArrayList = m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias)
            Return _BenefitClaimReceipt
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitClaimReceiptColl As ArrayList = m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitClaimReceiptColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitClaimReceiptColl As ArrayList = m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitClaimReceiptColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitClaimReceiptColl As ArrayList = m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitClaimReceipt), columnName, matchOperator, columnValue))
            Return BenefitClaimReceiptColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), columnName, matchOperator, columnValue))

            Return m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimReceiptCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitClaimReceipt), "BenefitClaimReceiptCode", AggregateType.Count)
            Return CType(m_BenefitClaimReceiptMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function InsertReceipt(ByVal Obj As BenefitClaimReceipt, ByVal arrBenefitClaimDeductedHistory As ArrayList, ByVal arrBenefitClaimDeducted As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not Obj Is Nothing Then
                        m_TransactionManager.AddInsert(Obj, m_userPrincipal.Identity.Name)
                    End If

                    If Not IsNothing(arrBenefitClaimDeductedHistory) Then
                        If arrBenefitClaimDeductedHistory.Count > 0 Then
                            For Each item As BenefitClaimDeductedHistory In arrBenefitClaimDeductedHistory
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If
                    If Not IsNothing(arrBenefitClaimDeducted) Then
                        If arrBenefitClaimDeducted.Count > 0 Then
                            For Each item As BenefitClaimDeducted In arrBenefitClaimDeducted
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

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

        Public Function UpdateReceipt(ByVal Obj As BenefitClaimReceipt) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not Obj Is Nothing Then
                        m_TransactionManager.AddUpdate(Obj, m_userPrincipal.Identity.Name)
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

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

        Public Function RetrieveByClaimHeader(ByVal Code As String) As BenefitClaimReceipt
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.ID", MatchType.Exact, Code))

            Dim BenefitClaimReceiptColl As ArrayList = m_BenefitClaimReceiptMapper.RetrieveByCriteria(criterias)
            If (BenefitClaimReceiptColl.Count > 0) Then
                Return CType(BenefitClaimReceiptColl(0), BenefitClaimReceipt)
            End If
            Return New BenefitClaimReceipt
        End Function

        Public Function Terbilang(ByVal angka As Integer) As String
            Dim strterbilang As String = ""
            ' membuat array untuk mengubah 1 - 11 menjadi terbilang
            Dim a As String() = {"", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", _
                                 "Sepuluh", "Sebelas"}

            If (angka < 12) Then
                strterbilang = " " + a(angka)
            ElseIf (angka < 20) Then
                strterbilang = Me.Terbilang(angka - 10) + " belas"
            ElseIf (angka < 100) Then
                strterbilang = Me.Terbilang(Fix(angka / 10)) + " Puluh" + Me.Terbilang(angka Mod 10)
            ElseIf (angka < 200) Then
                strterbilang = " seratus" + Me.Terbilang(angka - 100)
            ElseIf (angka < 1000) Then
                strterbilang = Me.Terbilang(Fix(angka / 100)) + " Ratus" + Me.Terbilang(angka Mod 100)
            ElseIf (angka < 2000) Then
                strterbilang = " seribu" + Me.Terbilang(angka - 1000)
            ElseIf (angka < 1000000) Then
                strterbilang = Me.Terbilang(Fix(angka / 1000)) + " Ribu" + Me.Terbilang(angka Mod 1000)
            ElseIf (angka < 1000000000) Then
                strterbilang = Me.Terbilang(Fix(angka / 1000000)) + " Juta" + Me.Terbilang(angka Mod 1000000)
            End If

            strterbilang = System.Text.RegularExpressions.Regex.Replace(strterbilang, "^\s+|\s+$", " ")

            Return strterbilang
        End Function

        Public Function UpdateStatusTransfer(ByVal items As BenefitClaimReceipt) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    items.Status = 1  'Status Proses

                    m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

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

        Public Function UpdateStatusTransfer(ByVal arrayListObj As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each items As BenefitClaimReceipt In arrayListObj
                        items.Status = 1  'Status Proses
                        m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

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

