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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 8/27/2007 - 1:17:22 PM
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

Namespace KTB.DNet.BusinessFacade.ShowroomAudit

    Public Class AuditScheduleDealerFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AuditScheduleDealerMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AuditScheduleDealerMapper = MapperFactory.GetInstance.GetMapper(GetType(AuditScheduleDealer).ToString)

            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AuditScheduleDealer
            Return CType(m_AuditScheduleDealerMapper.Retrieve(ID), AuditScheduleDealer)
        End Function

        Public Function Retrieve(ByVal Code As String) As AuditScheduleDealer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AuditScheduleDealer), "AuditScheduleDealerCode", MatchType.Exact, Code))

            Dim AuditScheduleDealerColl As ArrayList = m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias)
            If (AuditScheduleDealerColl.Count > 0) Then
                Return CType(AuditScheduleDealerColl(0), AuditScheduleDealer)
            End If
            Return New AuditScheduleDealer
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AuditScheduleDealerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditScheduleDealerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditScheduleDealerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AuditScheduleDealer As ArrayList = m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias)
            Return _AuditScheduleDealer
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditScheduleDealerColl As ArrayList = m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AuditScheduleDealerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AuditScheduleDealerColl As ArrayList = m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AuditScheduleDealerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditScheduleDealerColl As ArrayList = m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AuditScheduleDealer), columnName, matchOperator, columnValue))
            Return AuditScheduleDealerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), columnName, matchOperator, columnValue))

            Return m_AuditScheduleDealerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealer), sortColumn, CType(sortDirection, Short)))
            Else
                sortColl = Nothing
            End If

            Return m_AuditScheduleDealerMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "AuditScheduleDealerCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(AuditScheduleDealer), "AuditScheduleDealerCode", AggregateType.Count)
            Return CType(m_AuditScheduleDealerMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As AuditScheduleDealer) As Integer
            Dim iReturn As Integer = -2
            Try
                m_AuditScheduleDealerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
        Public Function UpdateTransaction(ByVal ArlDomain As ArrayList) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    For Each item As AuditScheduleDealer In ArlDomain
                        item.IsRilisReport = 1                        
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = 1
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

            Return returnVal
        End Function

        Public Function Delete(ByVal objDomain As AuditScheduleDealer) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = DBRowStatus.Deleted
                nResult = m_AuditScheduleDealerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As AuditScheduleDealer)
            Try
                m_AuditScheduleDealerMapper.Delete(objDomain)

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


        Public Function Update(ByVal objDomain As AuditScheduleDealer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AuditScheduleDealerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateDaftarAudit(ByVal objAuditScheduleDealer As AuditScheduleDealer) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    Dim userName As String = m_userPrincipal.Identity.Name

                    m_TransactionManager.AddUpdate(objAuditScheduleDealer, userName)

                    For Each auditPhoto As AuditParameterPhoto In objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos

                        m_TransactionManager.AddUpdate(auditPhoto, userName)

                        For Each auditReport As AuditScheduleDealerReport In auditPhoto.AuditScheduleDealerReports
                            If auditReport.ID = 0 Then
                                auditReport.AuditParameterPhotoID = auditPhoto.ID
                                auditReport.AuditScheduleDealerID = objAuditScheduleDealer.ID
                                m_TransactionManager.AddInsert(auditReport, userName)
                            Else
                                m_TransactionManager.AddUpdate(auditReport, userName)
                            End If
                        Next
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = 1
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

            Return returnVal
        End Function

        Public Function UpdateAuditAssesmentResult(ByVal objAuditScheduleDealer As AuditScheduleDealer, ByVal iseditphoto As Boolean) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    Dim userName As String = m_userPrincipal.Identity.Name

                    m_TransactionManager.AddUpdate(objAuditScheduleDealer, userName)

                    For Each auditPhoto As AuditParameterPhoto In objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos
                        For Each auditReport As AuditScheduleDealerReport In auditPhoto.AuditScheduleDealerReports
                            If auditReport.ID = 0 Then
                                auditReport.AuditParameterPhotoID = auditPhoto.ID
                                auditReport.AuditScheduleDealerID = objAuditScheduleDealer.ID
                                m_TransactionManager.AddInsert(auditReport, userName)
                            Else
                                If Not iseditphoto Then
                                    For Each auditFotoPerbaikan As AuditScheduleDealerReport In objAuditScheduleDealer.AuditScheduleDealerReports
                                        If auditFotoPerbaikan.ID = auditReport.ID Then
                                            auditReport.ItemImageReparation = auditFotoPerbaikan.ItemImageReparation
                                            Exit For
                                        End If
                                    Next
                                    m_TransactionManager.AddUpdate(auditReport, userName)
                                End If
                            End If
                        Next
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = 1
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

            Return returnVal
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

