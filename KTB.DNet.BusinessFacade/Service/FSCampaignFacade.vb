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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class FSCampaignFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FSCampaignMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FSCampaignMapper = MapperFactory.GetInstance.GetMapper(GetType(FSCampaign).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FSCampaign
            Return CType(m_FSCampaignMapper.Retrieve(ID), FSCampaign)
        End Function

        Public Function Retrieve(ByVal fsKind As FSKind, _
        ByVal vehicleType As VechileType, ByVal dateFrom As DateTime, _
        ByVal dateTo As DateTime) As FSCampaign
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaign), "FSKindID", MatchType.Exact, fsKind.ID))
            criterias.opAnd(New Criteria(GetType(FSCampaign), "VehicleType.ID", MatchType.Exact, vehicleType.ID))
            criterias.opAnd(New Criteria(GetType(FSCampaign), "DateFrom", MatchType.Exact, Format(dateFrom, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(FSCampaign), "DateTo", MatchType.Exact, Format(dateTo, "yyyy-MM-dd HH:mm:ss")))
            Dim FSCampaignColl As ArrayList = m_FSCampaignMapper.RetrieveByCriteria(criterias)
            If (FSCampaignColl.Count > 0) Then
                Return CType(FSCampaignColl(0), FSCampaign)
            Else
                Return New FSCampaign
            End If
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FSCampaignMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FSCampaignMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FSCampaignMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaign), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaign), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FSCampaign As ArrayList = m_FSCampaignMapper.RetrieveByCriteria(criterias)
            Return _FSCampaign
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FSCampaignColl As ArrayList = m_FSCampaignMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaign), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FSCampaignColl As ArrayList = m_FSCampaignMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaign), columnName, matchOperator, columnValue))
            Dim FSCampaignColl As ArrayList = m_FSCampaignMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaign), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), columnName, matchOperator, columnValue))

            Return m_FSCampaignMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveFSCampaign() As ArrayList
            Dim arlFSCampaign As ArrayList = New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaign), "Status", MatchType.Exact, 0))
            'criterias.opAnd(New Criteria(GetType(FSCampaign), "DateFrom", MatchType.LesserOrEqual, Format(Date.Now, "yyyy-MM-dd 00:00:00")))
            'criterias.opAnd(New Criteria(GetType(FSCampaign), "DateTo", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy-MM-dd 00:00:00")))
            arlFSCampaign = m_FSCampaignMapper.RetrieveByCriteria(criterias)
            Return arlFSCampaign
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FSCampaign) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_FSCampaignMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FSCampaign) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FSCampaignMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As FSCampaign) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FSCampaignMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As FSCampaign) As Integer
            Dim nResult As Integer = 1
            Try
                m_FSCampaignMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "KindCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(FSCampaign), "KindCode", AggregateType.Count)

            Return CType(m_FSCampaignMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Sub InsertIntoMSPRegistration(id As Integer)
            Dim strQuery As String = ""
            m_FSCampaignMapper.ExecuteSP("up_InsertMSPRegistrationFromFSCampaign @fscampaignID = " & id & "")
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
