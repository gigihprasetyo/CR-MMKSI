
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
'// Copyright  2017
'// ---------------------
'// $History      : $
'// Generated on 9/14/2017 - 9:39:05 AM
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

Namespace KTB.DNET.BusinessFacade.FinishUnit

    Public Class LogisticFeeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_LogisticFee As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_LogisticFee = MapperFactory.GetInstance.GetMapper(GetType(LogisticFee).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As LogisticFee
            Return CType(m_LogisticFee.Retrieve(ID), LogisticFee)
        End Function


        Public Function Retrieve(ByVal DealerID As Integer, Optional ByVal DebitChargeNo As String = "", Optional ByVal DebitMemoNo As String = "") As LogisticFee

            Dim oLogisticDN As LogisticDN

            If DebitMemoNo.Trim() <> String.Empty Then
                Dim cLDN As New CriteriaComposite(New Criteria(GetType(LogisticDN), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim oLDNFac As IMapper = MapperFactory.GetInstance.GetMapper(GetType(LogisticDN).ToString)
                Dim aLDNs As ArrayList
                Dim oLDN As New LogisticDN

                cLDN.opAnd(New Criteria(GetType(LogisticDN), "DebitMemoNo", MatchType.Exact, DebitMemoNo))
                'cLDN.opAnd(New Criteria(GetType(LogisticDN), "", MatchType.Exact, ""))
                aLDNs = oLDNFac.RetrieveByCriteria(cLDN)
                If aLDNs.Count > 0 Then
                    oLDN = aLDNs(0)
                End If
                oLogisticDN = oLDN
            Else
                If DebitChargeNo.Trim() <> String.Empty Then
                    Dim cLDN As New CriteriaComposite(New Criteria(GetType(LogisticDCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim oLDNFac As IMapper = MapperFactory.GetInstance.GetMapper(GetType(LogisticDCHeader).ToString)
                    Dim aLDNs As ArrayList
                    Dim oLDN As New LogisticDCHeader

                    cLDN.opAnd(New Criteria(GetType(LogisticDCHeader), "DebitChargeNo", MatchType.Exact, DebitChargeNo))

                    aLDNs = oLDNFac.RetrieveByCriteria(cLDN)
                    If aLDNs.Count > 0 Then
                        oLDN = aLDNs(0)
                        oLogisticDN = oLDN.LogisticDN
                    Else
                        oLogisticDN = New LogisticDN
                    End If
                End If
            End If

            If Not IsNothing(oLogisticDN) AndAlso oLogisticDN.ID > 0 Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(LogisticFee), "LogisticDN.ID", MatchType.Exact, oLogisticDN.ID))
                criterias.opAnd(New Criteria(GetType(LogisticFee), "Dealer.ID", MatchType.Exact, DealerID))

                Dim aPODs As ArrayList = m_LogisticFee.RetrieveByCriteria(criterias)
                If (aPODs.Count > 0) Then
                    Return CType(aPODs(0), LogisticFee)
                End If
            End If

            Return New LogisticFee
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_LogisticFee.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LogisticFee.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_LogisticFee.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LogisticFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LogisticFee.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LogisticFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LogisticFee.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _LogisticFee As ArrayList = m_LogisticFee.RetrieveByCriteria(criterias)
            Return _LogisticFee
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LogisticFeeColl As ArrayList = m_LogisticFee.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LogisticFeeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim LogisticFeeColl As ArrayList = m_LogisticFee.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return LogisticFeeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LogisticFeeColl As ArrayList = m_LogisticFee.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(LogisticFee), columnName, matchOperator, columnValue))
            Return LogisticFeeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LogisticFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticFee), columnName, matchOperator, columnValue))

            Return m_LogisticFee.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As LogisticFee) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_LogisticFee.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As LogisticFee) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_LogisticFee.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As LogisticFee)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_LogisticFee.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As LogisticFee)
            Try
                m_LogisticFee.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LogisticFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim LogisticFeeColl As ArrayList = m_LogisticFee.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return LogisticFeeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LogisticFee.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveScalar(ByVal agr As Aggregate, ByVal criterias As ICriteria) As Decimal
            Dim dr As Decimal
            dr = 0
            dr += m_LogisticFee.RetrieveScalar(agr, criterias)
            Return dr
        End Function
#End Region

    End Class

End Namespace

