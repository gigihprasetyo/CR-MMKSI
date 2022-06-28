

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
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

    Public Class SPKDetailCustomerFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SPKDetailCustomerMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SPKDetailCustomerMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKDetailCustomer).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPKDetailCustomer))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.OCRIdentity))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPKDetailCustomerProfile))
        End Sub

#End Region

#Region "Retrieve"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.SPKDetailCustomer) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKDetailCustomer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKDetailCustomer).MarkLoaded()
            End If
        End Sub

        Public Function Retrieve(ByVal ID As Integer) As SPKDetailCustomer
            Return CType(m_SPKDetailCustomerMapper.Retrieve(ID), SPKDetailCustomer)
        End Function

        Public Function Retrieve(ByVal code As String) As SPKDetailCustomer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKDetailCustomer), "Code", MatchType.Exact, code))
            Dim SPKDetailCustomerColl As ArrayList = m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias)

            Return SPKDetailCustomerColl(0)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPKDetailCustomerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKDetailCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKDetailCustomerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKDetailCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKDetailCustomerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPKDetailCustomer As ArrayList = m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias)
            Return _SPKDetailCustomer
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKDetailCustomerColl As ArrayList = m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPKDetailCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SPKDetailCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SPKDetailCustomerColl As ArrayList = m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPKDetailCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim SPKDetailCustomerColl As ArrayList = m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias)
            Return SPKDetailCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKDetailCustomerColl As ArrayList = m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SPKDetailCustomer), columnName, matchOperator, columnValue))
            Return SPKDetailCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKDetailCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetailCustomer), columnName, matchOperator, columnValue))

            Return m_SPKDetailCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"



        Public Function Update(ByVal objDomain As SPKDetailCustomer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPKDetailCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SPKDetailCustomer)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPKDetailCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SPKDetailCustomer) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SPKDetailCustomerMapper.Delete(objDomain)
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
        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.SPKDetailCustomer, ByVal objListProfile As ArrayList, Optional ByVal ocr As OCRIdentity = Nothing) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If Not IsNothing(ocr) AndAlso ocr.JSon <> "" Then
                        ocr.SPKDetailCustomer = objDomain
                        m_TransactionManager.AddInsert(ocr, m_userPrincipal.Identity.Name)
                    End If
                    For Each item As SPKDetailCustomerProfile In objListProfile
                        item.SPKDetailCustomer = objDomain
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

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.SPKDetailCustomer) As Integer
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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.SPKDetailCustomer, ByVal objListProfile As ArrayList, ByVal objGroup As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper


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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.SPKDetailCustomer, _
            ByVal objListProfile1 As ArrayList, ByVal objListProfile2 As ArrayList, ByVal objListProfile3 As ArrayList, ByVal objListProfile4 As ArrayList, ByVal objListProfile5 As ArrayList, _
            ByVal objGroup1 As ProfileGroup, ByVal objGroup2 As ProfileGroup, ByVal objGroup3 As ProfileGroup, ByVal objGroup4 As ProfileGroup, ByVal objGroup5 As ProfileGroup, Optional ByVal ocr As OCRIdentity = Nothing) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As SPKDetailCustomerProfile In objListProfile1
                        item.SPKDetailCustomer = objDomain
                        Dim oldProfile1 As SPKDetailCustomerProfile = GetSPKDetailCustomerProfiles(objDomain, objGroup1, item.ProfileHeader)
                        If oldProfile1.ID > 0 Then
                            oldProfile1.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKDetailCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile1.ProfileValue = item.ProfileValue
                        End If

                    Next


                    For Each item As SPKDetailCustomerProfile In objListProfile2
                        item.SPKDetailCustomer = objDomain
                        Dim oldProfile2 As SPKDetailCustomerProfile = GetSPKDetailCustomerProfiles(objDomain, objGroup2, item.ProfileHeader)
                        If oldProfile2.ID > 0 Then
                            oldProfile2.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile2, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKDetailCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile2.ProfileValue = item.ProfileValue
                        End If

                    Next

                    For Each item As SPKDetailCustomerProfile In objListProfile3
                        item.SPKDetailCustomer = objDomain
                        Dim oldProfile3 As SPKDetailCustomerProfile = GetSPKDetailCustomerProfiles(objDomain, objGroup3, item.ProfileHeader)
                        If oldProfile3.ID > 0 Then
                            oldProfile3.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile3, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKDetailCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile3.ProfileValue = item.ProfileValue
                        End If

                    Next


                    For Each item As SPKDetailCustomerProfile In objListProfile4
                        item.SPKDetailCustomer = objDomain
                        Dim oldProfile4 As SPKDetailCustomerProfile = GetSPKDetailCustomerProfiles(objDomain, objGroup4, item.ProfileHeader)
                        If oldProfile4.ID > 0 Then
                            oldProfile4.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile4, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKDetailCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile4.ProfileValue = item.ProfileValue
                        End If

                    Next


                    For Each item As SPKDetailCustomerProfile In objListProfile5
                        item.SPKDetailCustomer = objDomain
                        Dim oldProfile5 As SPKDetailCustomerProfile = GetSPKDetailCustomerProfiles(objDomain, objGroup5, item.ProfileHeader)
                        If oldProfile5.ID > 0 Then
                            oldProfile5.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile5, m_userPrincipal.Identity.Name)
                        Else
                            item.SPKDetailCustomer = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            oldProfile5.ProfileValue = item.ProfileValue
                        End If

                    Next

                    If Not IsNothing(ocr) AndAlso ocr.JSon <> "" Then
                        If ocr.ID > 0 Then
                            ocr.SPKDetailCustomer = objDomain
                            m_TransactionManager.AddUpdate(ocr, m_userPrincipal.Identity.Name)
                        Else
                            ocr.SPKDetailCustomer = objDomain
                            m_TransactionManager.AddInsert(ocr, m_userPrincipal.Identity.Name)
                        End If

                    Else
                        Try
                            ocr = objDomain.OCRIdentity
                            ocr.SPKDetailCustomer = objDomain
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

        Public Function GetSPKDetailCustomerProfiles(ByVal objSPKDetailCustomer As SPKDetailCustomer, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As SPKDetailCustomerProfile
            Dim objFacade As SPKDetailCustomerProfileFacade = New SPKDetailCustomerProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetailCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "SPKDetailCustomer.ID", MatchType.Exact, objSPKDetailCustomer.ID))
            criterias.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
            Dim objListSPKDetailCustomerProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListSPKDetailCustomerProfile.Count > 0 Then
                Return CType(objListSPKDetailCustomerProfile(0), SPKDetailCustomerProfile)
            End If
            Return New SPKDetailCustomerProfile
        End Function

        Private Function GetSPKDetailCustomerProfiles(ByRef CR As SPKDetailCustomer) As ArrayList
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
            Dim oCRPFac As SPKDetailCustomerFacade = New SPKDetailCustomerFacade(System.Threading.Thread.CurrentPrincipal)
            Dim cCRP As CriteriaComposite
            Dim aCRP As ArrayList
            Dim oCRP As SPKDetailCustomerProfile
            Dim aPH As New ArrayList
            Dim aResult As New ArrayList

            For Each oPHTG As ProfileHeaderToGroup In oPG.ProfileHeaderToGroups
                aPH.Add(oPHTG.ProfileHeader)
            Next
            For Each oPH As ProfileHeader In aPH
                cCRP = New CriteriaComposite(New Criteria(GetType(SPKDetailCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cCRP.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
                cCRP.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "SPKDetailCustomer.ID", MatchType.Exact, CR.ID))
                cCRP.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
                aCRP = oCRPFac.Retrieve(cCRP)
                If aCRP.Count > 0 Then
                    aResult.Add(aCRP(0))
                Else
                    aResult.Add(New SPKDetailCustomerProfile)
                End If
            Next

            Return aResult

        End Function

        Public Function UpdateProfile(ByVal oCR As SPKDetailCustomer, ByVal sKTP As String, ByVal sNPWP As String, ByVal sSIUP As String, ByVal sTDP As String) As Integer
            Dim returnValue As Integer = -1
            Dim oCRInDB As SPKDetailCustomer = Me.Retrieve(oCR.ID)
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
                    Dim oCRPFac As SPKDetailCustomerProfileFacade = New SPKDetailCustomerProfileFacade(System.Threading.Thread.CurrentPrincipal)

                    aProfiles = GetSPKDetailCustomerProfiles(oCR)
                    For Each oCRP As SPKDetailCustomerProfile In aProfiles
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

        Public Function GetSPKDetailCustomer(ByVal SPKNumber As String, ByVal CityName As String, ByVal Name1 As String) As ArrayList
            Dim spName As String
            Dim Param As New List(Of SqlClient.SqlParameter)

            spName = "sp_GetSPKDetailCustomerList"
            Param.Add(New SqlClient.SqlParameter("@SPKNumber", SPKNumber))
            Param.Add(New SqlClient.SqlParameter("@CityName ", CityName))
            Param.Add(New SqlClient.SqlParameter("@Name1", Name1))
            Return m_SPKDetailCustomerMapper.RetrieveSP(spName, New ArrayList(Param))
        End Function

        Public Function GetSPKDetailCustomerByChassis(ByVal SPKNumber As String, ByVal Chassis As String, ByVal CityName As String, ByVal Name1 As String) As ArrayList
            Dim spName As String
            Dim Param As New List(Of SqlClient.SqlParameter)

            spName = "sp_GetSPKDetailCustomerListByChassis"
            Param.Add(New SqlClient.SqlParameter("@SPKNumber", SPKNumber))
            Param.Add(New SqlClient.SqlParameter("@CityName ", CityName))
            Param.Add(New SqlClient.SqlParameter("@Name1", Name1))
            Param.Add(New SqlClient.SqlParameter("@Chassis", Chassis))
            Return m_SPKDetailCustomerMapper.RetrieveSP(spName, New ArrayList(Param))
        End Function

#End Region

    End Class

End Namespace



