
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
'// Generated on 10/07/2019 - 10:09:28
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
Imports System.Collections.Generic
Imports System.Linq


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class TrMRTCFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrMRTCMapper As IMapper
        Private ID_Insert As Integer
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TrMRTCMapper = MapperFactory.GetInstance.GetMapper(GetType(TrMRTC).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrMRTC
            Return CType(m_TrMRTCMapper.Retrieve(ID), TrMRTC)
        End Function

        Public Function Retrieve(ByVal Code As String) As TrMRTC
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Code", MatchType.Exact, Code))

            Dim TrMRTCColl As ArrayList = m_TrMRTCMapper.RetrieveByCriteria(criterias)
            If (TrMRTCColl.Count > 0) Then
                Return CType(TrMRTCColl(0), TrMRTC)
            End If
            Return New TrMRTC
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrMRTCMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrMRTCMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrMRTCMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrMRTC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrMRTCMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrMRTC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrMRTCMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrMRTC As ArrayList = m_TrMRTCMapper.RetrieveByCriteria(criterias)
            Return _TrMRTC
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrMRTCColl As ArrayList = m_TrMRTCMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrMRTCColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(TrMRTC), SortColumn, sortDirection))
            Dim TrMRTCColl As ArrayList = m_TrMRTCMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrMRTCColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrMRTCColl As ArrayList = m_TrMRTCMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrMRTCColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrMRTCColl As ArrayList = m_TrMRTCMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrMRTC), columnName, matchOperator, columnValue))
            Return TrMRTCColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrMRTC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTC), columnName, matchOperator, columnValue))

            Return m_TrMRTCMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTC), "TrMRTCCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TrMRTC), "TrMRTCCode", AggregateType.Count)
            Return CType(m_TrMRTCMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrMRTC) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TrMRTCMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrMRTC) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrMRTCMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TrMRTC)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrMRTCMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TrMRTC)
            Try
                m_TrMRTCMapper.Delete(objDomain)
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
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.TrMRTC) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TrMRTC).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TrMRTC).MarkLoaded()
                ID_Insert = InsertArg.ID
            End If
        End Sub

        Public Function Save(ByVal objMRTC As TrMRTC, ByVal listOfPIC As List(Of TrMRTCPIC), ByVal listOfDealer As List(Of TrMRTCDealer)) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objMRTC.ID = 0 Then
                        m_TransactionManager.AddInsert(objMRTC, m_userPrincipal.Identity.Name)

                        For Each pic As TrMRTCPIC In listOfPIC
                            pic.TrMRTC = objMRTC
                            m_TransactionManager.AddInsert(pic, m_userPrincipal.Identity.Name)
                        Next

                        For Each dealer As TrMRTCDealer In listOfDealer
                            dealer.TrMRTC = objMRTC
                            m_TransactionManager.AddInsert(dealer, m_userPrincipal.Identity.Name)
                        Next

                    Else

                        '=====================start maintain listPIC====================================

                        Dim oldListPIC As List(Of TrMRTCPIC) = GetOldListPIC(objMRTC.ID)

                        For Each oldPIC As TrMRTCPIC In oldListPIC
                            'cek data pic lama yang diinput lagi
                            Dim findIndex As Integer = listOfPIC.FindIndex(Function(x) x.TrTrainee.ID = oldPIC.TrTrainee.ID And x.Type = oldPIC.Type)

                            If findIndex <> -1 Then 'jika ada data lama yang dinput lagi
                                listOfPIC.RemoveAt(findIndex) 'tidak perlu di handle diproses berikutnya
                                If oldPIC.RowStatus = DBRowStatus.Deleted Then
                                    'Jika Data lama yang sudah tidak aktif diinput kembali , maka diaktifkan lagi dan tidak perlu insert baru
                                    oldPIC.RowStatus = DBRowStatus.Active
                                    m_TransactionManager.AddUpdate(oldPIC, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                If oldPIC.RowStatus = DBRowStatus.Active Then
                                    'jika data lama tidak dipakai lagi dan masih aktif, maka dinonaktifkan
                                    oldPIC.RowStatus = DBRowStatus.Deleted
                                    m_TransactionManager.AddUpdate(oldPIC, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        Next

                        For Each newPIC As TrMRTCPIC In listOfPIC
                            newPIC.TrMRTC = objMRTC
                            m_TransactionManager.AddInsert(newPIC, m_userPrincipal.Identity.Name)
                        Next

                    
                        ' ===================end maintain listPIC====================================


                        ' ===================start maintain listDealer =============================

                        Dim oldListDealer As List(Of TrMRTCDealer) = GetOldListDealer(objMRTC.ID)

                        For Each oldDealer As TrMRTCDealer In oldListDealer
                            'cek data dealer lama yang diinput lagi
                            Dim findDealerIndex As Integer = listOfDealer.FindIndex(Function(x) x.Dealer.ID = oldDealer.Dealer.ID)

                            If findDealerIndex <> -1 Then 'jika ada data lama yang dinput lagi
                                listOfDealer.RemoveAt(findDealerIndex) 'tidak perlu di handle diproses berikutnya
                                If oldDealer.RowStatus = DBRowStatus.Deleted Then
                                    'Jika Data lama yang sudah tidak aktif diinput kembali , maka diaktifkan lagi dan tidak perlu insert baru
                                    oldDealer.RowStatus = DBRowStatus.Active
                                    m_TransactionManager.AddUpdate(oldDealer, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                If oldDealer.RowStatus = DBRowStatus.Active Then
                                    'jika data lama tidak dipakai lagi dan masih aktif, maka dinonaktifkan
                                    oldDealer.RowStatus = DBRowStatus.Deleted
                                    m_TransactionManager.AddUpdate(oldDealer, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        Next

                        For Each newDealer As TrMRTCDealer In listOfDealer
                            newDealer.TrMRTC = objMRTC
                            m_TransactionManager.AddInsert(newDealer, m_userPrincipal.Identity.Name)
                        Next



                        ' ===================end maintain listDealer ================================

                        m_TransactionManager.AddUpdate(objMRTC, m_userPrincipal.Identity.Name)
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = ID_Insert
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Private Function GetOldListPIC(ByVal mrtcID As Integer) As List(Of TrMRTCPIC)
            Dim result As New List(Of TrMRTCPIC)
            Dim picFacade As TrMRTCPICFacade = New TrMRTCPICFacade(m_userPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTCPIC), "TrMRTC.ID", MatchType.Exact, mrtcID))

            Dim arlPIC As ArrayList = picFacade.Retrieve(criterias)

            If arlPIC.Count > 0 Then
                result = arlPIC.Cast(Of TrMRTCPIC).ToList()
            End If
            Return result
        End Function

        Private Function GetOldListDealer(ByVal mrtcID As Integer) As List(Of TrMRTCDealer)
            Dim result As New List(Of TrMRTCDealer)
            Dim facade As TrMRTCDealerFacade = New TrMRTCDealerFacade(m_userPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrMRTCDealer), "TrMRTC.ID", MatchType.Exact, mrtcID))

            Dim arl As ArrayList = facade.Retrieve(criterias)

            If arl.Count > 0 Then
                result = arl.Cast(Of TrMRTCDealer).ToList()
            End If
            Return result
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrMRTC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrCourseColl As ArrayList = m_TrMRTCMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrCourseColl
        End Function

#End Region

    End Class

End Namespace

