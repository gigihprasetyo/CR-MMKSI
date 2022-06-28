
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
'// Generated on 7/24/2018 - 10:55:41 AM
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

Namespace KTB.DNET.BusinessFacade.PageHelper

    Public Class OTPFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_OTPMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_OTPMapper = MapperFactory.GetInstance.GetMapper(GetType(OTP).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As OTP
            Return CType(m_OTPMapper.Retrieve(ID), OTP)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_OTPMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_OTPMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_OTPMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal cri As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OTP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_OTPMapper.RetrieveByCriteria(Cri, sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OTP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_OTPMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OTP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_OTPMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.OTP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim _OTP As ArrayList = m_OTPMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return _OTP
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OTP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _OTP As ArrayList = m_OTPMapper.RetrieveByCriteria(criterias)
            Return _OTP
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OTP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim OTPColl As ArrayList = m_OTPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return OTPColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim OTPColl As ArrayList = m_OTPMapper.RetrieveByCriteria(criterias, Nothing, 1, 1, 10)
            Return OTPColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim OTPColl As ArrayList = m_OTPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return OTPColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OTP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim OTPColl As ArrayList = m_OTPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(OTP), columnName, matchOperator, columnValue))
            Return OTPColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OTP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OTP), columnName, matchOperator, columnValue))

            Return m_OTPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As OTP) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_OTPMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As OTP) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_OTPMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As OTP)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_OTPMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As OTP)
            Try
                m_OTPMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function CheckOTPValid(UserID As Integer, OTPCode As String) As Boolean
            Dim strQuery As String
            Dim arr As New DataSet
            strQuery = "Exec up_CheckValidOTP " & UserID.ToString & ",'" & OTPCode & "',0"

            arr = m_OTPMapper.RetrieveDataSet(strQuery)

            If arr.Tables.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Function UpdateStatusOTP(UserID As Integer, HandPhoneNo As String) As Integer
            Dim strQuery As String
            Dim arr As New ArrayList
            strQuery = "Exec up_UpdateStatusOTP 0, " & UserID.ToString & ",'" & HandPhoneNo & "'"

            arr = m_OTPMapper.RetrieveSP(strQuery)

            If arr.Count > 0 Then
                For Each row As Object In arr
                    Return CType(row, Integer)
                    Exit For
                Next
            Else
                Return 0
            End If
        End Function

#End Region

    End Class

End Namespace

