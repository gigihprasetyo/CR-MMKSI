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
'// Copyright ? 2005 
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
    Public Class WSCDamageRequestPartFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_WSCDamageRequestPartMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_WSCDamageRequestPartMapper = MapperFactory.GetInstance().GetMapper(GetType(WSCDamageRequestPart).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As WSCDamageRequestPart
            Return CType(m_WSCDamageRequestPartMapper.Retrieve(ID), WSCDamageRequestPart)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_WSCDamageRequestPartMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_WSCDamageRequestPartMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_WSCDamageRequestPartMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCDamageRequestPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_WSCDamageRequestPartMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCDamageRequestPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_WSCDamageRequestPartMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDamageRequestPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _WSCDamageRequestPart As ArrayList = m_WSCDamageRequestPartMapper.RetrieveByCriteria(criterias)
            Return _WSCDamageRequestPart
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDamageRequestPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim WSCDamageRequestPartColl As ArrayList = m_WSCDamageRequestPartMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return WSCDamageRequestPartColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim WSCDamageRequestPartColl As ArrayList = m_WSCDamageRequestPartMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return WSCDamageRequestPartColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDamageRequestPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCDamageRequestPart), columnName, matchOperator, columnValue))
            Dim WSCDamageRequestPartColl As ArrayList = m_WSCDamageRequestPartMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return WSCDamageRequestPartColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCDamageRequestPart), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDamageRequestPart), columnName, matchOperator, columnValue))

            Return m_WSCDamageRequestPartMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As WSCDamageRequestPart) As Integer
            Dim iReturn As Integer = -2
            Try
                m_WSCDamageRequestPartMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As WSCDamageRequestPart) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_WSCDamageRequestPartMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function




        Public Sub Delete(ByVal objDomain As WSCDamageRequestPart)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_WSCDamageRequestPartMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As WSCDamageRequestPart)
            Try
                m_WSCDamageRequestPartMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal sID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDamageRequestPart), "ID", MatchType.Exact, sID))
            Dim agg As Aggregate = New Aggregate(GetType(WSCDamageRequestPart), "ID", AggregateType.Count)

            Return CType(m_WSCDamageRequestPartMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        'Public Function UpdateWSCDamageRequestPartCollection(ByVal arrFS As ArrayList) As Integer
        '    Dim returnValue As Short = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            If arrFS.Count > 0 Then
        '                For Each objWSCDamageRequestPart As WSCDamageRequestPart In arrFS
        '                    If objWSCDamageRequestPart.WSCDamageRequestPartStatus = CType(EnumFSStatus.FSStatus.Rilis, String) Then
        '                        m_WSCDamageRequestPartMapper.Update(objWSCDamageRequestPart, m_userPrincipal.Identity.Name)
        '                        'objTransactionManager.AddUpdate(objWSCDamageRequestPart, m_userPrincipal.Identity.Name)
        '                    End If
        '                Next
        '            End If
        '            'objTransactionManager.PerformTransaction()
        '            returnValue = 0

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        Finally
        '            Me.RemoveTaskLocking()
        '        End Try
        '    End If
        '    Return returnValue
        'End Function
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

