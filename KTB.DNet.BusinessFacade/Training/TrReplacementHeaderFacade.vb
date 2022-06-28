
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
'// Generated on 08/07/2019 - 9:30:52
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Collections.Generic
Imports System.Linq
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

    Public Class TrReplacementHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrReplacementHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager
        Private ID_Insert As Integer

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TrReplacementHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(TrReplacementHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrReplacementHeader
            Return CType(m_TrReplacementHeaderMapper.Retrieve(ID), TrReplacementHeader)
        End Function

        Public Function Retrieve(ByVal CourseCode As String) As TrReplacementHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrReplacementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrReplacementHeader), "TrCourse.CourseCode", MatchType.Exact, CourseCode))

            Dim TrReplacementHeaderColl As ArrayList = m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias)
            If (TrReplacementHeaderColl.Count > 0) Then
                Return CType(TrReplacementHeaderColl(0), TrReplacementHeader)
            End If
            Return New TrReplacementHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrReplacementHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrReplacementHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrReplacementHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrReplacementHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrReplacementHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrReplacementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrReplacementHeader As ArrayList = m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias)
            Return _TrReplacementHeader
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrReplacementHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrCourseColl As ArrayList = m_TrReplacementHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrCourseColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrReplacementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrReplacementHeaderColl As ArrayList = m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrReplacementHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(TrReplacementHeader), SortColumn, sortDirection))
            Dim TrReplacementHeaderColl As ArrayList = m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrReplacementHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrReplacementHeaderColl As ArrayList = m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrReplacementHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrReplacementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrReplacementHeaderColl As ArrayList = m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrReplacementHeader), columnName, matchOperator, columnValue))
            Return TrReplacementHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrReplacementHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrReplacementHeader), columnName, matchOperator, columnValue))

            Return m_TrReplacementHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrReplacementHeader), "TrReplacementHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TrReplacementHeader), "TrReplacementHeaderCode", AggregateType.Count)
            Return CType(m_TrReplacementHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrReplacementHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TrReplacementHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrReplacementHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrReplacementHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TrReplacementHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrReplacementHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TrReplacementHeader)
            Try
                m_TrReplacementHeaderMapper.Delete(objDomain)
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
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.TrReplacementHeader) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.TrReplacementHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.TrReplacementHeader).MarkLoaded()
                ID_Insert = InsertArg.ID
            End If
        End Sub

        Public Function Save(ByVal objHeader As TrReplacementHeader, ByVal listOfDetail As List(Of TrReplacementDetail)) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objHeader.ID = 0 Then
                        m_TransactionManager.AddInsert(objHeader, m_userPrincipal.Identity.Name)

                        For Each detail As TrReplacementDetail In listOfDetail
                            detail.TrReplacementHeader = objHeader
                            m_TransactionManager.AddInsert(detail, m_userPrincipal.Identity.Name)
                        Next
                    Else


                        Dim oldListDetail As List(Of TrReplacementDetail) = GetOldListDetail(objHeader.ID)
                        Dim listOldDetailCourseID As List(Of String) = GetListOldDetailCourseID(oldListDetail)

                        For Each oldData As TrReplacementDetail In oldListDetail
                            'cek data lama yg diinput kembali
                            Dim findIndex As Integer = listOfDetail.FindIndex(Function(x) x.TrCourse.ID = oldData.TrCourse.ID)

                            If findIndex <> -1 Then 'jika ada data lama yang dinput lagi
                                listOfDetail.RemoveAt(findIndex) 'tidak perlu di handle diproses berikutnya
                                If oldData.RowStatus = DBRowStatus.Deleted Then
                                    'Jika Data lama yang sudah tidak aktif diinput kembali , maka diaktifkan lagi dan tidak perlu insert baru
                                    oldData.RowStatus = DBRowStatus.Active
                                    m_TransactionManager.AddUpdate(oldData, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                If oldData.RowStatus = DBRowStatus.Active Then
                                    'jika data lama tidak dipakai lagi dan masih aktif, maka dinonaktifkan
                                    oldData.RowStatus = DBRowStatus.Deleted
                                    m_TransactionManager.AddUpdate(oldData, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        Next

                        For Each newData As TrReplacementDetail In listOfDetail
                            newData.TrReplacementHeader = objHeader
                            m_TransactionManager.AddInsert(newData, m_userPrincipal.Identity.Name)
                        Next

                        m_TransactionManager.AddUpdate(objHeader, m_userPrincipal.Identity.Name)

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

        Private Function GetOldListDetail(ByVal headerID As Integer) As List(Of TrReplacementDetail)
            Dim result As New List(Of TrReplacementDetail)
            Dim detailFacade As TrReplacementDetailFacade = New TrReplacementDetailFacade(m_userPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrReplacementDetail), "TrReplacementHeader.ID", MatchType.Exact, headerID))

            Dim arlDetail As ArrayList = detailFacade.Retrieve(criterias)

            If arlDetail.Count > 0 Then
                result = arlDetail.Cast(Of TrReplacementDetail).ToList()
            End If
            Return result
        End Function

        Private Function GetListOldDetailCourseID(arlDetail As List(Of TrReplacementDetail)) As List(Of String)
            Dim result As New List(Of String)

            For Each detailData As TrReplacementDetail In arlDetail
                result.Add(detailData.TrCourse.ID.ToString())
            Next

            Return result
        End Function

#End Region

        

    End Class

End Namespace

