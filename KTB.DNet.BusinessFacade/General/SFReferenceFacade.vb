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
'// Generated on 19/12/2018 - 10:36:18
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

Namespace KTB.DNet.BusinessFacade.General

    Public Class SFReferenceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SFReferenceMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SFReferenceMapper = MapperFactory.GetInstance.GetMapper(GetType(SFReference).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Long) As SFReference
            Return CType(m_SFReferenceMapper.Retrieve(ID), SFReference)
        End Function

        Public Function Retrieve(ByVal Code As String) As SFReference
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFReference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SFReference), "SFReferenceCode", MatchType.Exact, Code))

            Dim SFReferenceColl As ArrayList = m_SFReferenceMapper.RetrieveByCriteria(criterias)
            If (SFReferenceColl.Count > 0) Then
                Return CType(SFReferenceColl(0), SFReference)
            End If
            Return New SFReference
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SFReferenceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SFReferenceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SFReferenceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFReference), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SFReferenceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFReference), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SFReferenceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFReference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SFReference As ArrayList = m_SFReferenceMapper.RetrieveByCriteria(criterias)
            Return _SFReference
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFReference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SFReferenceColl As ArrayList = m_SFReferenceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SFReferenceColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SFReference), SortColumn, sortDirection))
            Dim SFReferenceColl As ArrayList = m_SFReferenceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SFReferenceColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SFReferenceColl As ArrayList = m_SFReferenceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SFReferenceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFReference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SFReferenceColl As ArrayList = m_SFReferenceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SFReference), columnName, matchOperator, columnValue))
            Return SFReferenceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFReference), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFReference), columnName, matchOperator, columnValue))

            Return m_SFReferenceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFReference), "SFReferenceCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SFReference), "SFReferenceCode", AggregateType.Count)
            Return CType(m_SFReferenceMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SFReference) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SFReferenceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SFReference) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SFReferenceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SFReference)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SFReferenceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SFReference)
            Try
                m_SFReferenceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function RetrieveSP(intParam As Int16) As ArrayList
            Dim cri As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SFReference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(KTB.DNet.Domain.SFReference), "IsSend", MatchType.Exact, 0))

            If (intParam = 1) Then
                cri.opAnd(New Criteria(GetType(KTB.DNet.Domain.SFReference), "RefTable", MatchType.Exact, "Dealer"))
            Else
                cri.opAnd(New Criteria(GetType(KTB.DNet.Domain.SFReference), "RefTable", MatchType.Exact, "DealerBranch"))
            End If

            Dim arr As New ArrayList
            arr = m_SFReferenceMapper.RetrieveByCriteria(cri)

            If arr.Count > 0 Then
                Return arr
            Else
                Return New ArrayList
            End If

        End Function

        Public Function SPFunc(strRefTable As String) As ArrayList
            Dim cri As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SFReference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(KTB.DNet.Domain.SFReference), "IsSend", MatchType.Exact, 0))
            cri.opAnd(New Criteria(GetType(KTB.DNet.Domain.SFReference), "RefTable", MatchType.Exact, strRefTable))


            Dim arr As New ArrayList
            arr = m_SFReferenceMapper.RetrieveByCriteria(cri)

            If arr.Count = 0 Or arr Is Nothing Then
                arr = New ArrayList
            End If

            Return arr
        End Function

        Public Function SPFunc(strRefTable As String, strRefID As String) As ArrayList
            Dim cri As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SFReference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(KTB.DNet.Domain.SFReference), "IsSend", MatchType.Exact, 0))
            cri.opAnd(New Criteria(GetType(KTB.DNet.Domain.SFReference), "RefTable", MatchType.Exact, strRefTable))
            cri.opAnd(New Criteria(GetType(KTB.DNet.Domain.SFReference), "RefID", MatchType.InSet, strRefID))

            Dim arr As New ArrayList
            arr = m_SFReferenceMapper.RetrieveByCriteria(cri)

            If arr.Count = 0 Or arr Is Nothing Then
                arr = New ArrayList
            End If

            Return arr
        End Function

        Public Sub RetrieveSP()

            Dim strQuery As String

            strQuery = "exec up_InsertSFReferenceFromSPKDetail"

            m_SFReferenceMapper.RetrieveSP(strQuery)

            strQuery = "exec up_InsertSFReferenceFromSPKHeader"

            m_SFReferenceMapper.RetrieveSP(strQuery)

        End Sub

        Public Function RetrieveSP(str As String)
            Dim strQuery As String

            strQuery = "exec up_InsertSFReferenceFrom" + str

            m_SFReferenceMapper.RetrieveSP(strQuery)

        End Function
#End Region

    End Class

End Namespace

