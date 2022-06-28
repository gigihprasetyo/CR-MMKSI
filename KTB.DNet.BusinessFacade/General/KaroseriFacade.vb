
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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 2/28/2018 - 11:38:44 AM
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


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class KaroseriFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_KaroseriMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_KaroseriMapper = MapperFactory.GetInstance.GetMapper(GetType(Karoseri).ToString)


            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Karoseri
            Return CType(m_KaroseriMapper.Retrieve(ID), Karoseri)
        End Function

        Public Function GetKaroseri(ByVal KaroseriCode As String) As KTB.DNET.Domain.Karoseri
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.Karoseri), "Code", MatchType.Exact, KaroseriCode))
                Dim poColl As ArrayList = m_KaroseriMapper.RetrieveByCriteria(criterias)
                If (poColl.Count > 0) Then
                    Return CType(poColl(0), KTB.DNET.Domain.Karoseri)
                End If
                Return New KTB.DNET.Domain.Karoseri
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_KaroseriMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_KaroseriMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_KaroseriMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Karoseri), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_KaroseriMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Karoseri), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_KaroseriMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Karoseri), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Karoseri As ArrayList = m_KaroseriMapper.RetrieveByCriteria(criterias)
            Return _Karoseri
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Karoseri), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim KaroseriColl As ArrayList = m_KaroseriMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return KaroseriColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim KaroseriColl As ArrayList = m_KaroseriMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return KaroseriColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Karoseri), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim KaroseriColl As ArrayList = m_KaroseriMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Karoseri), columnName, matchOperator, columnValue))
            Return KaroseriColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Karoseri), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Karoseri), columnName, matchOperator, columnValue))

            Return m_KaroseriMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As Karoseri) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_KaroseriMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As Karoseri) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_KaroseriMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As Karoseri)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_KaroseriMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As Karoseri)
            Try
                m_KaroseriMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function ValidateKaroseri(ByVal Karoseri As Karoseri) As Karoseri
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Karoseri), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.Karoseri), "Code", MatchType.Exact, Karoseri.Code))
            Dim arlKaroseri As ArrayList = m_KaroseriMapper.RetrieveByCriteria(criteria)
            If arlKaroseri.Count > 0 Then
                Return CType(arlKaroseri(0), Karoseri)
            End If
            Return Nothing

        End Function


        Public Function InsertFromWebSevice(ByVal Karoseri As Karoseri) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim Karoseri_old As Karoseri = ValidateKaroseri(Karoseri)
                    If IsNothing(Karoseri_old) Then

                        m_TransactionManager.AddInsert(Karoseri, m_userPrincipal.Identity.Name)
                    Else
                        Karoseri_old.Code = Karoseri.Code
                        Karoseri_old.Name = Karoseri.Name
                        Karoseri_old.Alamat = Karoseri.Alamat
                        Karoseri_old.PostalCode = Karoseri.PostalCode
                        Karoseri_old.City = Karoseri.City
                        Karoseri_old.Province = Karoseri.Province
                        Karoseri_old.ContactPerson = Karoseri.ContactPerson
                        Karoseri_old.PhoneNo = Karoseri.PhoneNo
                        Karoseri_old.HP = Karoseri.HP
                        Karoseri_old.Email = Karoseri.Email

                        m_TransactionManager.AddUpdate(Karoseri_old, m_userPrincipal.Identity.Name)

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
                    'Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function
#End Region

    End Class

End Namespace

