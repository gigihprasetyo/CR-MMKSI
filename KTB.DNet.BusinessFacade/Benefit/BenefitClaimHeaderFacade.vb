
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
'// Generated on 12/2/2015 - 11:18:20 AM
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

Imports Microsoft.CSharp
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports KTB.DNet.BusinessFacade.General

#End Region

Namespace KTB.DNet.BusinessFacade.Benefit

    Public Class BenefitClaimHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitClaimHeaderMapper As IMapper
        Private m_BenefitClaimDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager

        Private m_BenefitMasterDetailMapper As IMapper
        Private m_BenefitClaimJVMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitClaimHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitClaimHeader).ToString)
            Me.m_BenefitClaimDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitClaimDetails).ToString)

            Me.m_BenefitMasterDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(BenefitMasterDetail).ToString)
            Me.m_BenefitClaimJVMapper = MapperFactory.GetInstance().GetMapper(GetType(BenefitClaimJV).ToString)

            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BenefitClaimHeader
            Return CType(m_BenefitClaimHeaderMapper.Retrieve(ID), BenefitClaimHeader)
        End Function

        Public Function RetrieveByRegNumber(ByVal Code As String) As BenefitClaimHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ClaimRegNo", MatchType.Exact, Code))

            Dim BenefitClaimHeaderColl As ArrayList = m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias)
            If (BenefitClaimHeaderColl.Count > 0) Then
                Return CType(BenefitClaimHeaderColl(0), BenefitClaimHeader)
            End If
            Return New BenefitClaimHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitClaimHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitClaimHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitClaimHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BenefitClaimHeader As ArrayList = m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias)
            Return _BenefitClaimHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitClaimHeaderColl As ArrayList = m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitClaimHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitClaimHeaderColl As ArrayList = m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitClaimHeaderColl
        End Function

        Public Function RetrieveByCriteriaPage(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimHeader), sortColumn, sortDirection))
                'sortColl.Add(sortColumn & " " & sortDirection)
            Else
                sortColl = Nothing
            End If



            Dim BenefitClaimHeaderColl As ArrayList = m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BenefitClaimHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitClaimHeaderColl As ArrayList = m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), columnName, matchOperator, columnValue))
            Return BenefitClaimHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), columnName, matchOperator, columnValue))

            Return m_BenefitClaimHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "BenefitClaimHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitClaimHeader), "BenefitClaimHeaderCode", AggregateType.Count)
            Return CType(m_BenefitClaimHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function Rollback(ByVal arridheader As ArrayList, ByVal arriddetail As ArrayList) As Integer
            Dim returnValue As Integer = -1

            Try
                If Not arridheader Is Nothing Then
                    For Each items As Integer In arridheader
                        Dim objBenefitEventHeader As BenefitClaimHeader = Retrieve(items)
                        objBenefitEventHeader.RowStatus = -1
                        Dim idHeader As Integer = m_BenefitClaimHeaderMapper.Update(objBenefitEventHeader, m_userPrincipal.Identity.Name)
                    Next
                End If
                If Not arriddetail Is Nothing Then
                    For Each items As Integer In arriddetail
                        Dim objBenefitEventDetail As BenefitClaimDetails = CType(m_BenefitClaimDetailMapper.Retrieve(items), BenefitClaimDetails)
                        objBenefitEventDetail.RowStatus = -1
                        Dim idDetail As Integer = m_BenefitClaimDetailMapper.Update(objBenefitEventDetail, m_userPrincipal.Identity.Name)
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

        Public Function RollbackUpdate(ByVal arridheader As BenefitClaimHeader, ByVal arriddetail As ArrayList, _
                                       ByVal arriddetailOld As ArrayList) As Integer
            Dim returnValue As Integer = -1

            Try
                If Not arridheader Is Nothing Then

                    arridheader.RowStatus = 0
                    Dim idHeader As Integer = m_BenefitClaimHeaderMapper.Update(arridheader, m_userPrincipal.Identity.Name)

                End If



                If Not arriddetailOld Is Nothing Then
                    For Each items As Integer In arriddetailOld
                        Dim objBenefitEventDetail As BenefitEventDetail = CType(m_BenefitClaimDetailMapper.Retrieve(items), BenefitEventDetail)
                        objBenefitEventDetail.RowStatus = 0
                        Dim idDetail As Integer = m_BenefitClaimDetailMapper.Update(objBenefitEventDetail, m_userPrincipal.Identity.Name)
                    Next
                End If




                If Not arriddetail Is Nothing Then
                    For Each items As Integer In arriddetail
                        Dim objBenefitEventDetail As BenefitEventDetail = CType(m_BenefitClaimDetailMapper.Retrieve(items), BenefitEventDetail)
                        objBenefitEventDetail.RowStatus = -1
                        Dim idDetail As Integer = m_BenefitClaimDetailMapper.Update(objBenefitEventDetail, m_userPrincipal.Identity.Name)
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



        Public Function Insert(ByVal objDomain As BenefitClaimHeader, ByVal objDetails As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim aridheader As ArrayList = New ArrayList
            Dim ariddetail As ArrayList = New ArrayList
            If (Me.IsTaskFree()) Then
                Try
                    Dim countError As Integer = 0
                    If objDetails.Count > 0 Then
                        For Each items As BenefitClaimDetails In objDetails
                            If items.ErrorMessage <> "" Then
                                countError = countError + 1
                            End If
                        Next
                    End If

                    If countError < objDetails.Count Then
                        Me.SetTaskLocking()
                        Dim performTransaction As Boolean = True
                        Dim ObjMapper As IMapper


                        Dim countDetail As Integer = objDetails.Count
                        Dim countOk As Integer = 0
                        Dim countNotOk As Integer = 0

                        Dim idHeader As Integer = m_BenefitClaimHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                        aridheader.Add(idHeader)
                        Dim objBenefitClaimHeader As BenefitClaimHeader = Retrieve(idHeader)

                        If objDetails.Count > 0 Then
                            For Each items As BenefitClaimDetails In objDetails
                                items.BenefitClaimHeader = objBenefitClaimHeader
                                If items.ErrorMessage = "" Then
                                    Dim iddetail As Integer = m_BenefitClaimDetailMapper.Insert(items, m_userPrincipal.Identity.Name)
                                    If items.DetailStatus = 1 Then
                                        countOk = countOk + 1
                                    End If
                                    If items.DetailStatus = 2 Then
                                        countNotOk = countNotOk + 1
                                    End If
                                    ariddetail.Add(iddetail)
                                End If

                            Next
                        End If

                        'If countOk > 0 And countNotOk > 0 Then
                        '    objBenefitClaimHeader.Status = 4
                        'End If

                        ''If countDetail = countOk Then
                        ''    objBenefitClaimHeader.Status = 4
                        ''End If
                        'If countDetail = countNotOk Then
                        '    objBenefitClaimHeader.Status = 3
                        'End If

                        'If Not objDomain.JVNumber = "" Then
                        '    objBenefitClaimHeader.Status = 5
                        'End If

                        '  idHeader = m_BenefitClaimHeaderMapper.Update(objBenefitClaimHeader, m_userPrincipal.Identity.Name)

                        If performTransaction Then
                            m_TransactionManager.PerformTransaction()

                            'returnValue = 0
                            returnValue = idHeader
                        End If
                    End If

                Catch ex As Exception
                    Rollback(aridheader, ariddetail)
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



        Public Function Update(ByVal objDomain As BenefitClaimHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BenefitClaimHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As BenefitClaimHeader, ByVal arrDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim aridheader As ArrayList = New ArrayList
            Dim ariddetail As ArrayList = New ArrayList
            Dim ariddetailOld As ArrayList = New ArrayList
            Dim objBenefitClaimHeader As BenefitClaimHeader
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    objBenefitClaimHeader = Retrieve(objDomain.ID)
                    If Not objBenefitClaimHeader.BenefitClaimDetailss Is Nothing Then
                        For Each items As BenefitClaimDetails In objBenefitClaimHeader.BenefitClaimDetailss
                            ariddetailOld.Add(items.ID)
                        Next
                    End If


                    ' Dim idHeader As Integer = m_BenefitClaimHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    If objDomain.BenefitClaimDetailss.Count > 0 Then
                        For Each items As BenefitClaimDetails In objDomain.BenefitClaimDetailss
                            items.RowStatus = -1
                            Dim iddetail As Integer = m_BenefitClaimDetailMapper.Update(items, m_userPrincipal.Identity.Name)

                        Next
                    End If

                    Dim countDetail As Integer = arrDetail.Count
                    Dim countOk As Integer = 0
                    Dim countNotOk As Integer = 0
                    If arrDetail.Count > 0 Then
                        For Each items As BenefitClaimDetails In arrDetail
                            items.BenefitClaimHeader = objDomain
                            If items.ErrorMessage = "" Then
                                Dim iddetail1 As Integer = m_BenefitClaimDetailMapper.Insert(items, m_userPrincipal.Identity.Name)
                                ariddetail.Add(iddetail1)
                            End If

                        Next
                    End If

                    'If countOk > 0 And countNotOk > 0 Then
                    '    objBenefitClaimHeader.Status = 4
                    'End If

                    ''If countDetail = countOk Then
                    ''    objBenefitClaimHeader.Status = 4
                    ''End If
                    'If countDetail = countNotOk Then
                    '    objBenefitClaimHeader.Status = 3
                    'End If

                    'If Not objDomain.JVNumber = "" Then
                    '    objBenefitClaimHeader.Status = 5
                    'End If

                    'Dim idHeader As Integer = m_BenefitClaimHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                        'returnValue = idHeader
                    End If

                    ' RollbackUpdate(objBenefitEventHeader, ariddetail, aridparticipant, ariddetailOld, aridparticipantOld)

                Catch ex As Exception
                    RollbackUpdate(objBenefitClaimHeader, ariddetail, ariddetailOld)
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


        Public Function Delete(ByVal objDomain As BenefitClaimHeader) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not objDomain Is Nothing Then

                        objDomain.RowStatus = -1
                        m_BenefitClaimHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                        If Not objDomain.BenefitClaimDetailss Is Nothing Then
                            For Each objBenefitClaimDetailss As BenefitClaimDetails In objDomain.BenefitClaimDetailss
                                objBenefitClaimDetailss.RowStatus = -1
                                m_BenefitClaimDetailMapper.Update(objBenefitClaimDetailss, m_userPrincipal.Identity.Name)
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


        Public Function UpdateStatus(ByVal arrayListObj As ArrayList, ByVal arrayListCheck As ArrayList, ByVal Status As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrayListObj.Count > 0 Then

                        Dim objHeader As BenefitClaimHeader

                        Dim i As Integer = 1
                        For Each items As BenefitClaimDetails In arrayListObj
                            'Dim objBenefitEventParticipant As BenefitEventHeader = CType(m_BenefitEventHeaderMapper.Retrieve(items.ID), BenefitEventHeader)
                            'items.Status = 1

                            objHeader = items.BenefitClaimHeader

                            For Each itemsstring As String In arrayListCheck
                                If i = itemsstring Then
                                    items.DetailStatus = CInt(Status)
                                    m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)

                                End If
                            Next
                            i = i + 1
                        Next

                        If arrayListObj.Count = arrayListCheck.Count Then
                            If CShort(Status) = 1 Then
                                If Not objHeader Is Nothing Then
                                    objHeader.Status = 4
                                    m_TransactionManager.AddUpdate(objHeader, m_userPrincipal.Identity.Name)
                                End If
                            End If
                            If CShort(Status) = 2 Then
                                If Not objHeader Is Nothing Then
                                    objHeader.Status = 3
                                    m_TransactionManager.AddUpdate(objHeader, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        Else
                            If Not objHeader Is Nothing Then
                                objHeader.Status = 4
                                m_TransactionManager.AddUpdate(objHeader, m_userPrincipal.Identity.Name)
                            End If
                        End If


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

        Public Function UpdateStatus1(ByVal arrayListObj As ArrayList, ByVal arrayListCheck As ArrayList, ByVal accMonth As String, ByVal dateBayar As Date, ByVal status As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrayListObj.Count > 0 Then
                        Dim i As Integer = 1
                        For Each items As BenefitClaimHeader In arrayListObj
                            For Each itemsstring As String In arrayListCheck
                                If i = itemsstring Then


                                    If Not status = Nothing Then
                                        items.IsTransfer = status
                                    End If
                                    m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)



                                    'For Each itemsstring1 As BenefitClaimDetails In items.BenefitClaimDetailss
                                    '    If Not status Is Nothing Then
                                    '        itemsstring1.StatusUpload = CInt(status)
                                    '        m_TransactionManager.AddUpdate(itemsstring1, m_userPrincipal.Identity.Name)
                                    '    End If
                                    'Next
                                End If
                            Next
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


        Public Function UpdateStatus2(ByVal arrayListObj As ArrayList, ByVal arrayListCheck As ArrayList, ByVal status As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrayListObj.Count > 0 Then
                        Dim i As Integer = 1
                        For Each items As BenefitClaimHeader In arrayListObj
                            For Each itemsstring As String In arrayListCheck
                                If i = itemsstring Then
                                    If Not status = Nothing Then
                                        'check batal selesai:: 9 = batal selesai
                                        If CShort(status) = 9 Then
                                            If CShort(items.Status) = 5 Then
                                                items.Status = 4
                                                items.IsTransfer = 0
                                                m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                                            End If
                                        Else
                                            items.Status = status
                                            If CShort(status) = 3 Then
                                                For Each item2 As BenefitClaimDetails In items.BenefitClaimDetailss
                                                    item2.DetailStatus = 2
                                                    m_TransactionManager.AddUpdate(item2, m_userPrincipal.Identity.Name)
                                                Next
                                            End If

                                            If CShort(status) = 0 Then
                                                For Each item2 As BenefitClaimDetails In items.BenefitClaimDetailss
                                                    item2.DetailStatus = 0
                                                    m_TransactionManager.AddUpdate(item2, m_userPrincipal.Identity.Name)
                                                Next
                                            End If

                                            m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                                        End If
                                    End If
                                End If
                            Next
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

        Public Function UpdateStatus3(ByVal arrayListObj As ArrayList, ByVal arrayListCheck As ArrayList, ByVal status As String, ByRef errExist As Integer, ByVal arrBenefitClaimDeducted As ArrayList, ByVal arrBenefitClaimDeductedHistory As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrayListObj.Count > 0 Then
                        If Not IsNothing(arrBenefitClaimDeducted) Then
                            If arrBenefitClaimDeducted.Count > 0 Then
                                For Each obj As BenefitClaimDeducted In arrBenefitClaimDeducted
                                    If obj.ID <> 0 Then
                                        m_TransactionManager.AddUpdate(obj, m_userPrincipal.Identity.Name)
                                    Else
                                        m_TransactionManager.AddInsert(obj, m_userPrincipal.Identity.Name)
                                    End If
                                Next
                            End If
                        End If
                        If Not IsNothing(arrBenefitClaimDeductedHistory) Then
                            If arrBenefitClaimDeductedHistory.Count > 0 Then
                                For Each obj As BenefitClaimDeductedHistory In arrBenefitClaimDeductedHistory
                                    If obj.ID <> 0 Then
                                        m_TransactionManager.AddUpdate(obj, m_userPrincipal.Identity.Name)
                                    Else
                                        m_TransactionManager.AddInsert(obj, m_userPrincipal.Identity.Name)
                                    End If
                                Next
                            End If
                        End If

                        Dim i As Integer = 1
                        For Each items As BenefitClaimHeader In arrayListObj
                            For Each itemsstring As String In arrayListCheck
                                If i = itemsstring Then
                                    If Not status = Nothing Then
                                        'check batal selesai:: 9 = batal selesai
                                        If CShort(status) = 9 Then
                                            If CShort(items.Status) = 5 Then
                                                items.Status = 4
                                                items.IsTransfer = 0
                                                m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                                            Else
                                                errExist = 1
                                            End If
                                        Else
                                            items.Status = status
                                            If CShort(status) = 3 Then
                                                For Each item2 As BenefitClaimDetails In items.BenefitClaimDetailss
                                                    item2.DetailStatus = 2
                                                    m_TransactionManager.AddUpdate(item2, m_userPrincipal.Identity.Name)
                                                Next
                                            End If

                                            If CShort(status) = 0 Then
                                                For Each item2 As BenefitClaimDetails In items.BenefitClaimDetailss
                                                    item2.DetailStatus = 0
                                                    m_TransactionManager.AddUpdate(item2, m_userPrincipal.Identity.Name)
                                                Next
                                            End If

                                            m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                                        End If
                                    End If
                                End If
                            Next
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

        Public Function UpdateStatusTransfer(ByVal arrListObj As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each items As BenefitClaimReceipt In arrListObj
                        Dim item As BenefitClaimHeader = items.BenefitClaimHeader
                        item.IsTransfer = 1
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

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

        Public Function UpdateStatusTransfer(ByVal items As BenefitClaimHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    items.IsTransfer = 1

                    m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)


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


        Private Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
            Dim Generator As System.Random = New System.Random()
            Return Generator.Next(Min, Max)
        End Function

        '  Public Function Evaluate(ByVal MathExpression As String) As Decimal
        Public Function Evaluate(ByVal MathExpression As String) As String

            Dim tempMathExpression As String = ""
            Dim message As String = ""

            MathExpression = MathExpression.ToLower.Replace(" ", "")
            MathExpression = MathExpression.ToLower.Replace("true|true", "false")

            For Each c As Char In MathExpression.Replace(",", "")
                ' Count c            
                If c = "&" Then
                    tempMathExpression = tempMathExpression & " && "
                ElseIf c = "|" Then
                    tempMathExpression = tempMathExpression & " || "
                Else
                    tempMathExpression = tempMathExpression & c
                End If
            Next

            Dim values As String() = tempMathExpression.Split({"."}, StringSplitOptions.RemoveEmptyEntries)
            Dim result As Decimal = 0
            For i As Integer = 0 To values.Length - 1
                If Not values(i) = "" Then
                    Try
                        Dim codeProvider As CSharpCodeProvider = New CSharpCodeProvider()
                        Dim compilerParameters As CompilerParameters = New CompilerParameters
                        compilerParameters.GenerateExecutable = False
                        compilerParameters.GenerateInMemory = False

                        Dim tmpModuleSource As String = "namespace ns{"
                        tmpModuleSource = tmpModuleSource & "using System;"
                        tmpModuleSource = tmpModuleSource & "class class1{"
                        tmpModuleSource = tmpModuleSource & "    public static decimal Eval(){"
                        tmpModuleSource = tmpModuleSource & "          Boolean firstCheck = false;"
                        tmpModuleSource = tmpModuleSource & "          firstCheck = " & values(i) & ";"
                        tmpModuleSource = tmpModuleSource & "          if (firstCheck == true){ return 2; "
                        tmpModuleSource = tmpModuleSource & "          } else { return 1;"
                        tmpModuleSource = tmpModuleSource & "          } "
                        tmpModuleSource = tmpModuleSource & "     }"
                        tmpModuleSource = tmpModuleSource & "}} "

                        Dim CompilerResults As CompilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, tmpModuleSource)

                        If CompilerResults.Errors.Count > 0 Then
                            result = 0
                            message = "error"
                            Exit For
                        Else
                            Dim Methinfo As MethodInfo = CompilerResults.CompiledAssembly.GetType("ns.class1").GetMethod("Eval")
                            result = Convert.ToDecimal(Methinfo.Invoke(Nothing, Nothing))
                            message = result.ToString
                            If result = 2 Then

                                Exit For
                            End If
                        End If
                    Catch ex As Exception
                        result = 0
                        message = ex.Message & " - " & ex.ToString
                        Exit For
                    End Try
                End If
            Next

            Return message


        End Function


        Public Function SynchronizeSAPToDNET(ByVal objBCH As BenefitClaimHeader, ByVal objBCH2 As BenefitClaimHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(objBCH) Then
                        If Not IsNothing(objBCH2.BenefitClaimJVs) Then
                            If objBCH2.BenefitClaimJVs.Count > 0 Then
                                For Each objJV As BenefitClaimJV In objBCH2.BenefitClaimJVs
                                    objJV.JVNumber = objBCH.JVNumber
                                    m_TransactionManager.AddUpdate(objJV, m_userPrincipal.Identity.Name)
                                Next
                            End If
                        End If
                        objBCH2.JVNumber = objBCH.JVNumber

                        If objBCH.ActualPaymentDate > 0 Then
                            If objBCH2.BenefitClaimReceipts.Count > 0 Then
                                For Each obj As BenefitClaimReceipt In objBCH2.BenefitClaimReceipts
                                    obj.Status = 2   '--> Receipt Status = Selesai
                                    m_TransactionManager.AddUpdate(obj, m_userPrincipal.Identity.Name)
                                Next
                            End If

                            Dim _actualPaymentDate2 As New DateTime
                            Try
                                _actualPaymentDate2 = New DateTime(Left(objBCH.ActualPaymentDate, 4), Mid(objBCH.ActualPaymentDate, 5, 2), Mid(objBCH.ActualPaymentDate, 7, 2), 0, 0, 0)

                                If Not IsNothing(objBCH2.BenefitClaimJVs) Then
                                    If objBCH2.BenefitClaimJVs.Count > 0 Then
                                        For Each objJV As BenefitClaimJV In objBCH2.BenefitClaimJVs
                                            objJV.ActualPaymentDate = _actualPaymentDate2
                                            m_TransactionManager.AddUpdate(objJV, m_userPrincipal.Identity.Name)
                                        Next
                                    End If
                                End If
                            Catch ex As Exception
                            End Try

                            objBCH2.Status = BenefitClaimHeaderEnumStatus.Status.Selesai 'Selesai
                        End If
                    End If

                    If Not IsNothing(objBCH2.BenefitClaimReceipts) Then
                        m_TransactionManager.AddUpdate(objBCH2, m_userPrincipal.Identity.Name)
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

