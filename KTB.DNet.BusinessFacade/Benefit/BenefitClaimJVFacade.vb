
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

    Public Class BenefitClaimJVFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitClaimJVMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitClaimJVMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitClaimJV).ToString)

            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BenefitClaimJV
            Return CType(m_BenefitClaimJVMapper.Retrieve(ID), BenefitClaimJV)
        End Function

        Public Function Retrieve(ByVal Code As String) As BenefitClaimJV
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimJVCode", MatchType.Exact, Code))

            Dim BenefitClaimJVColl As ArrayList = m_BenefitClaimJVMapper.RetrieveByCriteria(criterias)
            If (BenefitClaimJVColl.Count > 0) Then
                Return CType(BenefitClaimJVColl(0), BenefitClaimJV)
            End If
            Return New BenefitClaimJV
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitClaimJVMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitClaimJVMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitClaimJVMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNET.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimJV), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitClaimJVMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNET.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimJV), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitClaimJVMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BenefitClaimJV As ArrayList = m_BenefitClaimJVMapper.RetrieveByCriteria(criterias)
            Return _BenefitClaimJV
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitClaimJVColl As ArrayList = m_BenefitClaimJVMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitClaimJVColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitClaimJVColl As ArrayList = m_BenefitClaimJVMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitClaimJVColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitClaimJVColl As ArrayList = m_BenefitClaimJVMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitClaimJV), columnName, matchOperator, columnValue))
            Return BenefitClaimJVColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNET.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimJV), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), columnName, matchOperator, columnValue))

            Return m_BenefitClaimJVMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_BenefitClaimJVMapper.RetrieveScalar(agg, criterias)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "BenefitClaimJVCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitClaimJV), "BenefitClaimJVCode", AggregateType.Count)
            Return CType(m_BenefitClaimJVMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function InsertTransaction(ByVal arrClaimJV As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(arrClaimJV) Then
                        If arrClaimJV.Count > 0 Then
                            For Each oClaimJV As BenefitClaimJV In arrClaimJV
                                m_TransactionManager.AddInsert(oClaimJV, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = 0

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

        Function UpdateTransaction(arrClaimJV As ArrayList, arlDelClaimJV As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arlDelClaimJV) Then
                        If arlDelClaimJV.Count > 0 Then
                            For Each item As BenefitClaimJV In arlDelClaimJV
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arrClaimJV) Then
                        If arrClaimJV.Count > 0 Then
                            For Each item As BenefitClaimJV In arrClaimJV
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


        Public Function RetrieveList(ByVal IDClaimHeader As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader", MatchType.Exact, IDClaimHeader))

            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection


            'sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimJV), "TipeAccount", Sort.SortDirection.ASC))
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimJV), "ID", Sort.SortDirection.ASC))


            Dim BenefitClaimJVColl As ArrayList = m_BenefitClaimJVMapper.RetrieveByCriteria(criterias, sortColl)
            Return BenefitClaimJVColl
        End Function

        Public Function InsertUpdateJV(ByVal Obj As ArrayList, ByVal objdomain As BenefitClaimHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    '  For Each items As BenefitClaimJV In Obj
                    For Each items As BenefitClaimJV In RetrieveList(objdomain.ID)
                        If Not items.ID = Nothing Then
                            m_TransactionManager.AddDelete(items)
                        End If
                    Next

                    Dim index As Integer = 0
                    For Each items As BenefitClaimJV In Obj

                        If index = 0 Then
                            Dim amount As Decimal = 0, nilaiPph As Decimal = 0, nilaiPpn As Decimal = 0
                            Dim tempamount As Decimal = 0
                            For Each items1 As BenefitClaimDetails In items.BenefitClaimHeader.BenefitClaimDetailss
                                If items1.DetailStatus = 1 Then
                                    amount += items1.BenefitMasterDetail.Amount
                                End If

                            Next
                            tempamount = amount
                            If items.BenefitClaimHeader.BenefitType.LeasingBox = 1 Then
                                nilaiPph = Math.Round(((amount / (1 - 0.14999999999999999)) - amount))
                            Else
                                nilaiPph = Math.Round(((amount / (1 - 0.02)) - amount))
                            End If
                            nilaiPpn = Math.Round((0.10000000000000001 * (nilaiPph + tempamount)))
                            ' items.Amount = amount + nilaiPph
                            items.Amount = amount + nilaiPpn
                        End If

                        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)

                        index = index + 1
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


        Public Function InsertJV(ByVal Obj As BenefitClaimJV) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not Obj Is Nothing Then
                        m_TransactionManager.AddInsert(Obj, m_userPrincipal.Identity.Name)
                        'm_TransactionManager.AddDelete(Obj, m_userPrincipal.Identity.Name)
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

        Public Function UpdateJV(ByVal Obj As BenefitClaimJV) As Integer
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

        Public Function RetrieveByClaimHeader(ByVal Code As String) As BenefitClaimJV
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader", MatchType.Exact, Code))

            Dim BenefitClaimJVColl As ArrayList = m_BenefitClaimJVMapper.RetrieveByCriteria(criterias)
            If (BenefitClaimJVColl.Count > 0) Then
                Return CType(BenefitClaimJVColl(0), BenefitClaimJV)
            End If
            Return New BenefitClaimJV
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

#End Region

    End Class

End Namespace

