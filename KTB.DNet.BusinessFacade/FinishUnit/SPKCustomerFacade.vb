

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
'// Generated on 7/18/2007 - 2:19:39 PM
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
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

    Public Class SPKCustomerFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SPKCustomerMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SPKCustomerMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKCustomer).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPKCustomer))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.OCRIdentity))
        End Sub

#End Region

#Region "Retrieve"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.SPKCustomer) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKCustomer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKCustomer).MarkLoaded()
            End If
        End Sub

        Public Function Retrieve(ByVal ID As Integer) As SPKCustomer
            Return CType(m_SPKCustomerMapper.Retrieve(ID), SPKCustomer)
        End Function

        Public Function Retrieve(ByVal code As String) As SPKCustomer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKCustomer), "Code", MatchType.Exact, code))
            Dim SPKCustomerColl As ArrayList = m_SPKCustomerMapper.RetrieveByCriteria(criterias)

            Return SPKCustomerColl(0)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPKCustomerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPKCustomerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPKCustomerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKCustomerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKCustomerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPKCustomer As ArrayList = m_SPKCustomerMapper.RetrieveByCriteria(criterias)
            Return _SPKCustomer
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKCustomerColl As ArrayList = m_SPKCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPKCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SPKCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SPKCustomerColl As ArrayList = m_SPKCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPKCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim SPKCustomerColl As ArrayList = m_SPKCustomerMapper.RetrieveByCriteria(criterias)
            Return SPKCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKCustomerColl As ArrayList = m_SPKCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SPKCustomer), columnName, matchOperator, columnValue))
            Return SPKCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomer), columnName, matchOperator, columnValue))

            Return m_SPKCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"

        'Public Function GetSPKCustomer(ByVal objSPKCustomer As SPKCustomer, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As SPKCustomer
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(SPKCustomer), "SPKCustomer.ID", MatchType.Exact, objSPKCustomer.ID))
        '    criterias.opAnd(New Criteria(GetType(SPKCustomer), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
        '    criterias.opAnd(New Criteria(GetType(SPKCustomer), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))

        '    Dim SPKCustomerColl As ArrayList = New ArrayList
        '    Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKCustomer).ToString)

        '    SPKCustomerColl = sProfileMapper.RetrieveByCriteria(criterias)
        '    If (SPKCustomerColl.Count > 0) Then
        '        Return CType(SPKCustomerColl(0), SPKCustomer)
        '    End If
        '    Return New SPKCustomer
        'End Function

        'Public Function Insert(ByVal objDomain As SPKCustomer) As Integer
        '    Dim iReturn As Integer = -2
        '    Try
        '        m_SPKCustomerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim s As String = ex.Message
        '        iReturn = -1
        '    End Try
        '    Return iReturn
        'End Function

        Public Function Update(ByVal objDomain As SPKCustomer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPKCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SPKCustomer)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPKCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SPKCustomer) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SPKCustomerMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function



#End Region

#Region "Custom Method"
        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.SPKCustomer, ByVal objListProfile As ArrayList, Optional ByVal ocr As OCRIdentity = Nothing) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If Not IsNothing(ocr) AndAlso ocr.JSon <> "" Then
                        ocr.SPKCustomer = objDomain
                        m_TransactionManager.AddInsert(ocr, m_userPrincipal.Identity.Name)
                    End If
                    For Each item As SPKCustomerProfile In objListProfile
                        item.SPKCustomer = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.SPKCustomer) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.SPKCustomer, ByVal objListProfile As ArrayList, ByVal objGroup As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'For Each item As SPKProfile In objListProfile
                    '    item.SPKCustomer = objDomain
                    '    Dim oldProfile As SPKProfile = GetSPKProfile(objDomain, objGroup, item.ProfileHeader)
                    '    If oldProfile.ID > 0 Then
                    '        oldProfile.ProfileValue = item.ProfileValue
                    '        m_TransactionManager.AddUpdate(oldProfile, m_userPrincipal.Identity.Name)
                    '    Else
                    '        item.SPKCustomer = objDomain
                    '        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    '        oldProfile.ProfileValue = item.ProfileValue
                    '    End If

                    'Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.SPKCustomer, _
            ByVal objListProfile1 As ArrayList, ByVal objListProfile2 As ArrayList, ByVal objListProfile3 As ArrayList, ByVal objListProfile4 As ArrayList, ByVal objListProfile5 As ArrayList, _
            ByVal objGroup1 As ProfileGroup, ByVal objGroup2 As ProfileGroup, ByVal objGroup3 As ProfileGroup, ByVal objGroup4 As ProfileGroup, ByVal objGroup5 As ProfileGroup, Optional ByVal ocr As OCRIdentity = Nothing) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As SPKCustomerProfile In objListProfile1
                        item.SPKCustomer = objDomain
                        Dim oldProfile1 As SPKCustomerProfile = GetSPKCustomerProfiles(objDomain, objGroup1, item.ProfileHeader)
                        If oldProfile1.ID > 0 Then
                            oldProfile1.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile1.ProfileValue = item.ProfileValue
                        End If

                    Next


                    For Each item As SPKCustomerProfile In objListProfile2
                        item.SPKCustomer = objDomain
                        Dim oldProfile2 As SPKCustomerProfile = GetSPKCustomerProfiles(objDomain, objGroup2, item.ProfileHeader)
                        If oldProfile2.ID > 0 Then
                            oldProfile2.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile2, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile2.ProfileValue = item.ProfileValue
                        End If

                    Next

                    For Each item As SPKCustomerProfile In objListProfile3
                        item.SPKCustomer = objDomain
                        Dim oldProfile3 As SPKCustomerProfile = GetSPKCustomerProfiles(objDomain, objGroup3, item.ProfileHeader)
                        If oldProfile3.ID > 0 Then
                            oldProfile3.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile3, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile3.ProfileValue = item.ProfileValue
                        End If

                    Next


                    For Each item As SPKCustomerProfile In objListProfile4
                        item.SPKCustomer = objDomain
                        Dim oldProfile4 As SPKCustomerProfile = GetSPKCustomerProfiles(objDomain, objGroup4, item.ProfileHeader)
                        If oldProfile4.ID > 0 Then
                            oldProfile4.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile4, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile4.ProfileValue = item.ProfileValue
                        End If

                    Next


                    For Each item As SPKCustomerProfile In objListProfile5
                        item.SPKCustomer = objDomain
                        Dim oldProfile5 As SPKCustomerProfile = GetSPKCustomerProfiles(objDomain, objGroup5, item.ProfileHeader)
                        If oldProfile5.ID > 0 Then
                            oldProfile5.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile5, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile5.ProfileValue = item.ProfileValue
                        End If

                    Next

                    If Not IsNothing(ocr) AndAlso ocr.JSon <> "" Then
                        If ocr.ID > 0 Then
                            ocr.SPKCustomer = objDomain
                            m_TransactionManager.AddUpdate(ocr, m_userPrincipal.Identity.Name)
                        Else
                            ocr.SPKCustomer = objDomain
                            m_TransactionManager.AddInsert(ocr, m_userPrincipal.Identity.Name)
                        End If

                    Else
                        Try
                            ocr = objDomain.OCRIdentity
                            ocr.SPKCustomer = objDomain
                            ocr.RowStatus = DBRowStatus.Deleted
                            If Not IsNothing(ocr) AndAlso ocr.ID > 0 Then
                                m_TransactionManager.AddUpdate(ocr, m_userPrincipal.Identity.Name)
                            End If
                        Catch ex As Exception

                        End Try

                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Public Function GetSPKCustomerProfiles(ByVal objSPKCustomer As SPKCustomer, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As SPKCustomerProfile
            Dim objFacade As SPKCustomerProfileFacade = New SPKCustomerProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, objSPKCustomer.ID))
            criterias.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
            Dim objListSPKCustomerProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListSPKCustomerProfile.Count > 0 Then
                Return CType(objListSPKCustomerProfile(0), SPKCustomerProfile)
            End If
            Return New SPKCustomerProfile
        End Function

        Private Function GetSPKCustomerProfiles(ByRef CR As SPKCustomer) As ArrayList
            Dim ProfileGroupCode As String
            If CR.Status = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan Then
                ProfileGroupCode = "cust_dbs_2"
            ElseIf CR.Status = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
                ProfileGroupCode = "cust_dbs_3"
            ElseIf CR.Status = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.BUMN_Pemerintah Then
                ProfileGroupCode = "cust_dbs_4"
            ElseIf CR.Status = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Lainnya Then
                ProfileGroupCode = "cust_dbs_5"
            End If
            Dim oPG As ProfileGroup = New ProfileGroupFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(ProfileGroupCode)
            Dim oCRPFac As SPKCustomerFacade = New SPKCustomerFacade(System.Threading.Thread.CurrentPrincipal)
            Dim cCRP As CriteriaComposite
            Dim aCRP As ArrayList
            Dim oCRP As SPKCustomerProfile
            Dim aPH As New ArrayList
            Dim aResult As New ArrayList

            For Each oPHTG As ProfileHeaderToGroup In oPG.ProfileHeaderToGroups
                aPH.Add(oPHTG.ProfileHeader)
            Next
            For Each oPH As ProfileHeader In aPH
                cCRP = New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cCRP.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
                cCRP.opAnd(New Criteria(GetType(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, CR.ID))
                cCRP.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
                aCRP = oCRPFac.Retrieve(cCRP)
                If aCRP.Count > 0 Then
                    aResult.Add(aCRP(0))
                Else
                    aResult.Add(New SPKCustomerProfile)
                End If
            Next

            Return aResult

        End Function

        Public Function UpdateProfile(ByVal oCR As SPKCustomer, ByVal sKTP As String, ByVal sNPWP As String, ByVal sSIUP As String, ByVal sTDP As String) As Integer
            Dim returnValue As Integer = -1
            Dim oCRInDB As SPKCustomer = Me.Retrieve(oCR.ID)
            Dim sKTPOld As String
            Dim sNPWPOld As String
            Dim sSIUPOld As String
            Dim sTDPOld As String
            Dim aProfiles As New ArrayList

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim IsChanged As Boolean = False
                    Dim oCRPFac As SPKCustomerProfileFacade = New SPKCustomerProfileFacade(System.Threading.Thread.CurrentPrincipal)

                    aProfiles = GetSPKCustomerProfiles(oCR)
                    For Each oCRP As SPKCustomerProfile In aProfiles
                        If Not IsNothing(oCRP.ProfileHeader) Then
                            Select Case oCRP.ProfileHeader.Code.Trim.ToUpper
                                Case "NOKTP"
                                    sKTPOld = IIf(IsNothing(oCRP.ProfileValue), "", oCRP.ProfileValue)
                                    If sKTPOld <> sKTP Then
                                        oCRP.ProfileValue = sKTP
                                        oCRPFac.Update(oCRP)
                                    End If
                                Case "NONPWP"
                                    sNPWPOld = IIf(IsNothing(oCRP.ProfileValue), "", oCRP.ProfileValue)
                                    If sNPWPOld <> sNPWP Then
                                        oCRP.ProfileValue = sNPWP
                                        oCRPFac.Update(oCRP)
                                    End If
                                Case "NOSIUP"
                                    sSIUPOld = IIf(IsNothing(oCRP.ProfileValue), "", oCRP.ProfileValue)
                                    If sSIUPOld <> sSIUP Then
                                        oCRP.ProfileValue = sSIUP
                                        oCRPFac.Update(oCRP)
                                    End If
                                Case "NOTDP"
                                    sTDPOld = IIf(IsNothing(oCRP.ProfileValue), "", oCRP.ProfileValue)
                                    If sTDPOld <> sTDP Then
                                        oCRP.ProfileValue = sTDP
                                        oCRPFac.Update(oCRP)
                                    End If
                            End Select
                        End If
                    Next
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



