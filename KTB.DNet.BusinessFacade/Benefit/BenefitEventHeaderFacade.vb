
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
'// Generated on 12/2/2015 - 11:21:03 AM
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

    Public Class BenefitEventHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitEventHeaderMapper As IMapper

        Private m_BenefitParticipantMapper As IMapper

        Private m_BenefitEventDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

        Private objBenefitParticipantFacade As BenefitParticipantFacade

        'Private objBenefitEventHeaderFacade As BenefitEventHeaderFacade



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitEventHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitEventHeader).ToString)

            Me.m_BenefitParticipantMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitParticipant).ToString)

            Me.m_BenefitEventDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitEventDetail).ToString)

            Me.objBenefitParticipantFacade = New BenefitParticipantFacade(userPrincipal)

            'Me.objBenefitEventHeaderFacade = New BenefitEventHeaderFacade(userPrincipal)


            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BenefitEventHeader
            Return CType(m_BenefitEventHeaderMapper.Retrieve(ID), BenefitEventHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As BenefitEventHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "BenefitEventHeaderCode", MatchType.Exact, Code))

            Dim BenefitEventHeaderColl As ArrayList = m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias)
            If (BenefitEventHeaderColl.Count > 0) Then
                Return CType(BenefitEventHeaderColl(0), BenefitEventHeader)
            End If
            Return New BenefitEventHeader
        End Function

        Public Function Retrieve(ByVal objBenefitEventHeader As BenefitEventHeader) As BenefitEventHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "EventRegNo", MatchType.Exact, objBenefitEventHeader.EventRegNo))
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "EventName", MatchType.Exact, objBenefitEventHeader.EventName))
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "Status", MatchType.Exact, objBenefitEventHeader.Status))
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "BenefitMasterHeader", MatchType.Exact, objBenefitEventHeader.BenefitMasterHeader.ID))

            Dim BenefitEventHeaderColl As ArrayList = m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias)
            If (BenefitEventHeaderColl.Count > 0) Then
                Return CType(BenefitEventHeaderColl(0), BenefitEventHeader)
            End If
            Return New BenefitEventHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitEventHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitEventHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitEventHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitEventHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitEventHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BenefitEventHeader As ArrayList = m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias)
            Return _BenefitEventHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitEventHeaderColl As ArrayList = m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitEventHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias..opAnd(New Criteria(GetType(AlertMaster), "BenefitMasterHeader.Benefitregno", MatchType.[Partial], txtCodeDealer.Text.Trim))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BenefitEventHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim BenefitEventHeaderColl As ArrayList = m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return BenefitEventHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitEventHeaderColl As ArrayList = m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitEventHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitEventHeaderColl As ArrayList = m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), columnName, matchOperator, columnValue))
            Return BenefitEventHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitEventHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), columnName, matchOperator, columnValue))

            Return m_BenefitEventHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "BenefitEventHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitEventHeader), "BenefitEventHeaderCode", AggregateType.Count)
            Return CType(m_BenefitEventHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function Insert(ByVal objDomain As BenefitEventHeader, ByVal objDetails As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If objDetails.Count > 0 Then
                        For Each items As BenefitEventDetail In objDetails
                            items.BenefitEventHeader = objDomain


                            Dim objBenefitParticipant As BenefitParticipant = items.BenefitParticipant
                            m_TransactionManager.AddInsert(items.BenefitParticipant, m_userPrincipal.Identity.Name)




                            'items.BenefitParticipant = objBenefitParticipant
                            'items.BenefitParticipant = items.BenefitParticipant
                            'm_TransactionManager.AddInsert(items.BenefitParticipant, m_userPrincipal.Identity.Name)

                            'items.BenefitParticipant = Nothing



                            'Dim idparticipant As Integer = m_BenefitParticipantMapper.Insert(items.BenefitParticipant, m_userPrincipal.Identity.Name)

                            'items.BenefitParticipant.ID = idparticipant
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)


                            'If items.BenefitMasterLeasings.Count > 0 Then
                            '    For Each items1 As BenefitMasterLeasing In items.BenefitMasterLeasings
                            '        items1.BenefitMasterDetail = items
                            '        m_TransactionManager.AddInsert(items1, m_userPrincipal.Identity.Name)
                            '    Next
                            'End If

                           


                        Next
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

       

        Public Function Insert(ByVal objDomain As BenefitEventHeader) As Integer
            Dim returnValue As Integer = -1
            Dim aridheader As ArrayList = New ArrayList
            Dim ariddetail As ArrayList = New ArrayList
            Dim aridparticipant As ArrayList = New ArrayList
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'm_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    Dim idHeader As Integer = m_BenefitEventHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                    aridheader.Add(idHeader)
                    Dim objBenefitEventHeader As BenefitEventHeader = Retrieve(idHeader)



                    If objDomain.BenefitEventDetails.Count > 0 Then
                        For Each items As BenefitEventDetail In objDomain.BenefitEventDetails
                            'items.BenefitEventHeader = objDomain
                            'Dim objBenefitParticipant As BenefitParticipant = items.BenefitParticipant
                            'm_TransactionManager.AddInsert(items.BenefitParticipant, m_userPrincipal.Identity.Name)
                            'm_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)


                            Dim idparticipant As Integer = m_BenefitParticipantMapper.Insert(items.BenefitParticipant, m_userPrincipal.Identity.Name)
                            aridparticipant.Add(idparticipant)
                            Dim objBenefitEventParticipant As BenefitParticipant = CType(m_BenefitParticipantMapper.Retrieve(idparticipant), BenefitParticipant)

                            items.BenefitEventHeader = objBenefitEventHeader
                            items.BenefitParticipant = objBenefitEventParticipant



                            Dim iddetail As Integer = m_BenefitEventDetailMapper.Insert(items, m_userPrincipal.Identity.Name)
                            ariddetail.Add(iddetail)
                        Next
                    End If


                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        'returnValue = 0
                        returnValue = idHeader
                    End If

                Catch ex As Exception
                    Rollback(aridheader, ariddetail, aridparticipant)
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


        Public Function Update(ByVal objBenefitEventHeader As KTB.DNet.Domain.BenefitEventHeader) As Integer
            Dim nUpdatedRow As Integer = -1
            Try
                nUpdatedRow = m_BenefitEventHeaderMapper.Update(objBenefitEventHeader, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return -1
            End Try
            Return nUpdatedRow
        End Function

        Public Function Update(ByVal BenefitEventHeaderList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If BenefitEventHeaderList.Count > 0 Then
                        For Each item As BenefitEventHeader In BenefitEventHeaderList
                            Dim iddetail As Integer = m_BenefitEventHeaderMapper.Update(item, m_userPrincipal.Identity.Name)
                        Next
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

        Public Function Update(ByVal objDomain As BenefitEventHeader, ByVal arrDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim aridheader As ArrayList = New ArrayList
            Dim ariddetail As ArrayList = New ArrayList
            Dim aridparticipant As ArrayList = New ArrayList
            Dim ariddetailOld As ArrayList = New ArrayList
            Dim aridparticipantOld As ArrayList = New ArrayList
            Dim objBenefitEventHeader As BenefitEventHeader
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    objBenefitEventHeader = Retrieve(objDomain.ID)
                    If Not objBenefitEventHeader.BenefitEventDetails Is Nothing Then
                        For Each items As BenefitEventDetail In objBenefitEventHeader.BenefitEventDetails
                            aridparticipantOld.Add(items.BenefitParticipant.ID)
                            ariddetailOld.Add(items.ID)
                        Next
                    End If


                    Dim idHeader As Integer = m_BenefitEventHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    If objDomain.BenefitEventDetails.Count > 0 Then
                        For Each items As BenefitEventDetail In objDomain.BenefitEventDetails

                            items.BenefitParticipant.RowStatus = -1
                            Dim idparticipant As Integer = m_BenefitParticipantMapper.Update(items.BenefitParticipant, m_userPrincipal.Identity.Name)
                            items.RowStatus = -1
                            Dim iddetail As Integer = m_BenefitEventDetailMapper.Update(items, m_userPrincipal.Identity.Name)

                        Next
                    End If

                    If arrDetail.Count > 0 Then
                        For Each items As BenefitEventDetail In arrDetail
                            Dim idparticipant As Integer = m_BenefitParticipantMapper.Insert(items.BenefitParticipant, m_userPrincipal.Identity.Name)
                            aridparticipant.Add(idparticipant)
                            Dim objBenefitEventParticipant As BenefitParticipant = CType(m_BenefitParticipantMapper.Retrieve(idparticipant), BenefitParticipant)

                            items.BenefitEventHeader = objDomain
                            items.BenefitParticipant = objBenefitEventParticipant

                            Dim iddetail1 As Integer = m_BenefitEventDetailMapper.Insert(items, m_userPrincipal.Identity.Name)
                            ariddetail.Add(iddetail1)
                        Next
                    End If


                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

                    ' RollbackUpdate(objBenefitEventHeader, ariddetail, aridparticipant, ariddetailOld, aridparticipantOld)

                Catch ex As Exception
                    RollbackUpdate(objBenefitEventHeader, ariddetail, aridparticipant, ariddetailOld, aridparticipantOld)
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


        Public Function Rollback(ByVal arridheader As ArrayList, ByVal arriddetail As ArrayList, ByVal arridparticipant As ArrayList) As Integer
            Dim returnValue As Integer = -1

            Try
                If Not arridheader Is Nothing Then
                    For Each items As Integer In arridheader
                        Dim objBenefitEventHeader As BenefitEventHeader = Retrieve(items)
                        objBenefitEventHeader.RowStatus = -1
                        Dim idHeader As Integer = m_BenefitEventHeaderMapper.Update(objBenefitEventHeader, m_userPrincipal.Identity.Name)
                    Next
                End If
                If Not arriddetail Is Nothing Then
                    For Each items As Integer In arriddetail
                        Dim objBenefitEventDetail As BenefitEventDetail = CType(m_BenefitEventDetailMapper.Retrieve(items), BenefitEventDetail)
                        objBenefitEventDetail.RowStatus = -1
                        Dim idDetail As Integer = m_BenefitEventDetailMapper.Update(objBenefitEventDetail, m_userPrincipal.Identity.Name)
                    Next
                End If
                If Not arridparticipant Is Nothing Then
                    For Each items As Integer In arridparticipant
                        Dim objBenefitParticipant As BenefitParticipant = CType(m_BenefitParticipantMapper.Retrieve(items), BenefitParticipant)
                        objBenefitParticipant.RowStatus = -1
                        Dim idDetail As Integer = m_BenefitParticipantMapper.Update(objBenefitParticipant, m_userPrincipal.Identity.Name)
                    Next
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            Finally

            End Try

            Return returnValue
        End Function

        Public Function RollbackUpdate(ByVal arridheader As BenefitEventHeader, ByVal arriddetail As ArrayList, _
                                       ByVal arridparticipant As ArrayList, ByVal arriddetailOld As ArrayList, _
                                       ByVal arridparticipantOld As ArrayList) As Integer
            Dim returnValue As Integer = -1

            Try
                If Not arridheader Is Nothing Then

                    arridheader.RowStatus = 0
                    Dim idHeader As Integer = m_BenefitEventHeaderMapper.Update(arridheader, m_userPrincipal.Identity.Name)

                End If

                'If Not arridheader.BenefitEventDetails Is Nothing Then
                '    For Each objBenefitEventDetail As BenefitEventDetail In arridheader.BenefitEventDetails
                '        objBenefitEventDetail.BenefitParticipant.RowStatus = 0
                '        Dim idDetail As Integer = m_BenefitParticipantMapper.Update(objBenefitEventDetail.BenefitParticipant, m_userPrincipal.Identity.Name)
                '        objBenefitEventDetail.RowStatus = 0
                '        Dim idDetail1 As Integer = m_BenefitEventDetailMapper.Update(objBenefitEventDetail, m_userPrincipal.Identity.Name)
                '    Next
                'End If

                If Not arriddetailOld Is Nothing Then
                    For Each items As Integer In arriddetailOld
                        Dim objBenefitEventDetail As BenefitEventDetail = CType(m_BenefitEventDetailMapper.Retrieve(items), BenefitEventDetail)
                        objBenefitEventDetail.RowStatus = 0
                        Dim idDetail As Integer = m_BenefitEventDetailMapper.Update(objBenefitEventDetail, m_userPrincipal.Identity.Name)
                    Next
                End If
                If Not arridparticipantOld Is Nothing Then
                    For Each items As Integer In arridparticipantOld
                        Dim objBenefitParticipant As BenefitParticipant = CType(m_BenefitParticipantMapper.Retrieve(items), BenefitParticipant)
                        objBenefitParticipant.RowStatus = 0
                        Dim idDetail As Integer = m_BenefitParticipantMapper.Update(objBenefitParticipant, m_userPrincipal.Identity.Name)
                    Next
                End If



                If Not arriddetail Is Nothing Then
                    For Each items As Integer In arriddetail
                        Dim objBenefitEventDetail As BenefitEventDetail = CType(m_BenefitEventDetailMapper.Retrieve(items), BenefitEventDetail)
                        objBenefitEventDetail.RowStatus = -1
                        Dim idDetail As Integer = m_BenefitEventDetailMapper.Update(objBenefitEventDetail, m_userPrincipal.Identity.Name)
                    Next
                End If
                If Not arridparticipant Is Nothing Then
                    For Each items As Integer In arridparticipant
                        Dim objBenefitParticipant As BenefitParticipant = CType(m_BenefitParticipantMapper.Retrieve(items), BenefitParticipant)
                        objBenefitParticipant.RowStatus = -1
                        Dim idDetail As Integer = m_BenefitParticipantMapper.Update(objBenefitParticipant, m_userPrincipal.Identity.Name)
                    Next
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            Finally

            End Try

            Return returnValue
        End Function



        Public Function Delete(ByVal objDomain As BenefitEventHeader) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not objDomain Is Nothing Then

                        objDomain.RowStatus = -1
                        m_BenefitEventHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                        If Not objDomain.BenefitEventDetails Is Nothing Then
                            For Each objBenefitEventDetail As BenefitEventDetail In objDomain.BenefitEventDetails
                                objBenefitEventDetail.BenefitParticipant.RowStatus = -1
                                m_BenefitParticipantMapper.Update(objBenefitEventDetail.BenefitParticipant, m_userPrincipal.Identity.Name)
                                objBenefitEventDetail.RowStatus = -1
                                m_BenefitEventDetailMapper.Update(objBenefitEventDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                   

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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


        Public Function Insert(ByVal objDomain As BenefitEventHeader, ByVal objDetails As ArrayList, ByVal objDetailsParticipant As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If objDetailsParticipant.Count > 0 Then
                        For Each items As BenefitParticipant In objDetailsParticipant

                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)

                            Dim objbenefitEventDetail As BenefitEventDetail = New BenefitEventDetail

                            Dim objBenefitParticipant As BenefitParticipant = objBenefitParticipantFacade.Retrieve(items.Nama, items.KTP, items.Alamat)

                            objbenefitEventDetail.BenefitParticipant = objBenefitParticipant
                            objbenefitEventDetail.Status = 0



                            Dim objBenefitEventHeader As BenefitEventHeader = Retrieve(objDomain)


                            'objbenefitEventDetail.BenefitEventHeader = objDomain
                            objbenefitEventDetail.BenefitEventHeader = objBenefitEventHeader
                            m_TransactionManager.AddInsert(objbenefitEventDetail, m_userPrincipal.Identity.Name)
                            'items.BenefitEventHeader = objDomain


                            'Dim objBenefitParticipant As BenefitParticipant = items.BenefitParticipant
                            'm_TransactionManager.AddInsert(items.BenefitParticipant, m_userPrincipal.Identity.Name)


                            'm_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)




                        Next
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




        Public Function UpdateStatus(ByVal arrayListObj As ArrayList, ByVal arrayListCheck As ArrayList, ByVal Status As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrayListObj.Count > 0 Then
                        Dim i As Integer = 1
                        For Each items As BenefitEventHeader In arrayListObj
                            'Dim objBenefitEventParticipant As BenefitEventHeader = CType(m_BenefitEventHeaderMapper.Retrieve(items.ID), BenefitEventHeader)
                            'items.Status = 1

                            For Each itemsstring As String In arrayListCheck
                                If i = itemsstring Then
                                    If CInt(Status) = 2 Then
                                        Status = 0
                                    End If
                                    If CInt(Status) = 4 Then
                                        Status = 1
                                    End If
                                    items.Status = CInt(Status)
                                    m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)

                                End If
                            Next


                            'items.Status = 0




                            i = i + 1
                        Next
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

#End Region

    End Class

End Namespace

