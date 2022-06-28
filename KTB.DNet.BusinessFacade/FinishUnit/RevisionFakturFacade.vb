#Region "Code Disclaimer"
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
'// Generated on 1/23/2018 - 8:44:11 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System.Security.Principal

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Profile


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class RevisionFakturFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RevisionFakturMapper As IMapper
        Private m_RevisionCHassisMasterProfileMapper As IMapper
        Private m_RevisionSPKFakturMapper As IMapper
        Private m_EndCustomerMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_RevisionFakturMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionFaktur).ToString)
            Me.m_RevisionCHassisMasterProfileMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionChassisMasterProfile).ToString)
            Me.m_RevisionSPKFakturMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionSPKFaktur).ToString)
            Me.m_EndCustomerMapper = MapperFactory.GetInstance.GetMapper(GetType(EndCustomer).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManagerInsert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.RevisionFaktur))
            Me.DomainTypeCollection.Add(GetType(Customer))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As RevisionFaktur
            Return CType(m_RevisionFakturMapper.Retrieve(ID), RevisionFaktur)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RevisionFakturMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionFakturMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RevisionFakturMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionFaktur), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionFakturMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionFaktur), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionFakturMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _RevisionFaktur As ArrayList = m_RevisionFakturMapper.RetrieveByCriteria(criterias)
            Return _RevisionFaktur
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionFakturColl As ArrayList = m_RevisionFakturMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RevisionFakturColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RevisionFakturColl As ArrayList = m_RevisionFakturMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return RevisionFakturColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionFakturColl As ArrayList = m_RevisionFakturMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), columnName, matchOperator, columnValue))
            Return RevisionFakturColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionFaktur), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), columnName, matchOperator, columnValue))

            Return m_RevisionFakturMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionFaktur), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim RevisionFakturColl As ArrayList = m_RevisionFakturMapper.RetrieveByCriteria(Criterias, sortColl)
            Return RevisionFakturColl
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Private Sub m_TransactionManagerInsert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is EndCustomer) Then
                CType(InsertArg.DomainObject, EndCustomer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EndCustomer).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is RevisionChassisMasterProfile) Then
                CType(InsertArg.DomainObject, RevisionChassisMasterProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.RevisionChassisMasterProfile).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is Customer) Then
                CType(InsertArg.DomainObject, Customer).ID = InsertArg.ID
            End If
        End Sub

        Public Function Insert(ByVal objDomain As RevisionFaktur) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_RevisionFakturMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Insert(ByVal objDomain As RevisionFaktur, ByVal objListProfile As ArrayList, ByVal objGroup As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim name As String = m_userPrincipal.Identity.Name

                    Dim newEndCustomer As EndCustomer = objDomain.EndCustomer
                    newEndCustomer.ID = 0
                    newEndCustomer.CreatedBy = name
                    newEndCustomer.LastUpdateBy = name
                    newEndCustomer.FakturNumber = String.Empty
                    m_TransactionManager.AddInsert(newEndCustomer, name)
                    newEndCustomer.MarkLoaded()

                    For Each item As ChassisMasterProfile In objListProfile
                        Dim revisionProfile As RevisionChassisMasterProfile = New RevisionChassisMasterProfile
                        revisionProfile.ChassisMaster = objDomain.ChassisMaster
                        revisionProfile.EndCustomer = newEndCustomer
                        revisionProfile.ProfileHeader = item.ProfileHeader
                        revisionProfile.ProfileGroup = item.ProfileGroup
                        revisionProfile.ProfileValue = item.ProfileValue
                        revisionProfile.RowStatus = 0
                        revisionProfile.CreatedBy = name
                        revisionProfile.LastUpdateBy = name

                        m_TransactionManager.AddInsert(revisionProfile, name)
                    Next

                    objDomain.CreatedBy = name
                    objDomain.LastUpdateBy = name
                    objDomain.RevisionStatus = 0
                    objDomain.RowStatus = 0
                    objDomain.RegNumber = String.Empty
                    objDomain.EndCustomer = newEndCustomer
                    objDomain.IsPay = -1
                    m_TransactionManager.AddInsert(objDomain, name)

                    m_TransactionManager.AddUpdate(objDomain.EndCustomer.Customer, m_userPrincipal.Identity.Name)

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

        Public Function Update(ByVal objDomain As RevisionFaktur, ByVal objListProfile As ArrayList, ByVal objGroup As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim name As String = m_userPrincipal.Identity.Name

                    Dim newEndCustomer As EndCustomer = objDomain.EndCustomer
                    newEndCustomer.LastUpdateBy = name
                    m_TransactionManager.AddUpdate(newEndCustomer, name)

                    objDomain.LastUpdateBy = name
                    m_TransactionManager.AddUpdate(objDomain, name)

                    m_TransactionManager.AddUpdate(objDomain.EndCustomer.Customer, m_userPrincipal.Identity.Name)

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

                returnValue = UpdateProfile(objDomain, objListProfile, objGroup)
            End If
            Return returnValue
        End Function

        Public Function Update(ByVal objDomain As RevisionFaktur) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RevisionFakturMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateTransaction(ByVal objDomainColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As RevisionFaktur In objDomainColl
                        If item.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Proses And item.IsPay = EnumDNET.enumPaymentOption.Bayar Then ' if payment method is Bayar
                            If Not IsNothing(item.RevisionPaymentDetails) Then
                                If item.RevisionPaymentDetails.Count > 0 Then
                                    Dim revPaymentDetail As RevisionPaymentDetail = item.RevisionPaymentDetails(0)
                                    Dim revpaymentHeader As RevisionPaymentHeader = revPaymentDetail.RevisionPaymentHeader
                                    revpaymentHeader.Status = EnumStatusRevisionPayment.Status.Selesai
                                    m_TransactionManager.AddUpdate(revpaymentHeader, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        End If
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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

        Public Sub Delete(ByVal objDomain As RevisionFaktur)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_RevisionFakturMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function CancelFaktur(ByVal objDomain As RevisionFaktur, ByVal objListProfile As ArrayList)
            Dim returnValue As Integer = -1
            Try
                Dim data As RevisionFaktur = objDomain
                Dim dataEndCustomer As EndCustomer = objDomain.EndCustomer

                For Each item As RevisionChassisMasterProfile In objListProfile
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                    returnValue = m_RevisionCHassisMasterProfileMapper.Update(item, m_userPrincipal.Identity.Name)
                Next

                If returnValue <> -1 Then
                    dataEndCustomer.RowStatus = CType(DBRowStatus.Deleted, Short)
                    returnValue = m_EndCustomerMapper.Update(dataEndCustomer, m_userPrincipal.Identity.Name)
                End If

                If returnValue <> -1 Then
                    If objDomain.RevisionType.ID = 2 Then
                        Dim dataRevisionSPKFaktur As RevisionSPKFaktur = objDomain.EndCustomer.RevisionSPKFaktur
                        If Not IsNothing(dataRevisionSPKFaktur) Then
                            dataRevisionSPKFaktur.RowStatus = CType(DBRowStatus.Deleted, Short)
                            returnValue = m_RevisionSPKFakturMapper.Update(dataRevisionSPKFaktur, m_userPrincipal.Identity.Name)
                        End If
                    End If
                End If

                If returnValue <> -1 Then
                    objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                    returnValue = Update(objDomain)
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

        Public Sub DeleteFromDB(ByVal objDomain As RevisionFaktur)
            Try
                m_RevisionFakturMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function SynchronizeSAPToDNET(ByVal objRevisionFaktur As RevisionFaktur) As Integer
            If (Me.IsTaskFree() Or 1 = 1) Then
                Try
                    If Not IsNothing(objRevisionFaktur.EndCustomer) AndAlso (objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Proses Or objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Selesai) Then
                        objRevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Selesai

                        ' farid update tanggal 20190621 request yuki---------------------------------------------------------------------------------------
                        'objRevisionFaktur.ChassisMaster.EndCustomer.PrintedTime = Date.Now
                        ' farid update tanggal 20190621 request yuki---------------------------------------------------------------------------------------

                        ' if oldendcustomer istemporary is 1,then set new endcustomer istemporary to normal (0)
                        objRevisionFaktur.ChassisMaster.EndCustomer.IsTemporary = 0

                        m_TransactionManager.AddUpdate(objRevisionFaktur.ChassisMaster.EndCustomer, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddUpdate(objRevisionFaktur.ChassisMaster, m_userPrincipal.Identity.Name)
                        Dim spkFaktur As SPKFaktur = objRevisionFaktur.OldEndCustomer.SPKFaktur
                        If Not IsNothing(spkFaktur) Then
                            spkFaktur.EndCustomer = objRevisionFaktur.ChassisMaster.EndCustomer
                            m_TransactionManager.AddUpdate(spkFaktur, m_userPrincipal.Identity.Name)
                        End If

                        m_TransactionManager.AddUpdate(objRevisionFaktur, m_userPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
        End Function

        Public Function UpdateRemark(ByVal objDomain As RevisionFaktur, ByVal remark As String) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.Remark = remark
                m_RevisionFakturMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                nResult = 0
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
#End Region

#Region "Custom Method"
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionFakturMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Private Function GetRevisionChassisMasterProfile(ByVal obj As RevisionFaktur, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As RevisionChassisMasterProfile
            Dim objFacade As RevisionChassisMasterProfileFacade = New RevisionChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, obj.ChassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "EndCustomer.ID", MatchType.Exact, obj.EndCustomer.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(RevisionChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
            Dim objList As ArrayList = objFacade.Retrieve(criterias)
            If objList.Count > 0 Then
                Return CType(objList(0), RevisionChassisMasterProfile)
            End If
            Return New RevisionChassisMasterProfile
        End Function

        Private Function GetListRevisionChassisMasterProfile(ByVal obj As RevisionFaktur) As ArrayList
            Dim objFacade As RevisionChassisMasterProfileFacade = New RevisionChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim objList As ArrayList = objFacade.GetRevisionChassisMasterProfileByChassisEndCustomer(obj.ChassisMaster.ID, obj.EndCustomer.ID)

            Return objList
        End Function

        Private Function UpdateProfile(ByVal objDomain As RevisionFaktur, ByVal objListProfile As ArrayList, ByVal objGroup As ProfileGroup) As Integer
            Dim nResult As Integer = -1
            Try
                For Each item As RevisionChassisMasterProfile In objListProfile
                    Dim oldProfile As RevisionChassisMasterProfile = GetRevisionChassisMasterProfile(objDomain, objGroup, item.ProfileHeader)
                    If oldProfile.ID > 0 Then
                        oldProfile.ProfileValue = item.ProfileValue
                        nResult = m_RevisionCHassisMasterProfileMapper.Update(oldProfile, m_userPrincipal.Identity.Name)
                    Else
                        item.EndCustomer = objDomain.EndCustomer
                        item.ChassisMaster = objDomain.ChassisMaster
                        nResult = m_RevisionCHassisMasterProfileMapper.Insert(item, m_userPrincipal.Identity.Name)
                    End If
                Next
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
#End Region

    End Class

End Namespace

