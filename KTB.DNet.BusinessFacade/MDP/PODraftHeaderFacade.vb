
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
'// Generated on 12/14/2018 - 3:00:28 PM
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNET.BusinessFacade.PO
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.FinishUnit

#End Region

Namespace KTB.DNET.BusinessFacade.MDP

    Public Class PODraftHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PODraftHeaderMapper As IMapper
        Private m_PODraftDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PODraftHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(PODraftHeader).ToString)
            Me.m_PODraftDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODraftDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PODraftHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PODraftDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PODraftHeader
            Return CType(m_PODraftHeaderMapper.Retrieve(ID), PODraftHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As PODraftHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PODraftHeader), "PODraftHeaderCode", MatchType.Exact, Code))

            Dim PODraftHeaderColl As ArrayList = m_PODraftHeaderMapper.RetrieveByCriteria(criterias)
            If (PODraftHeaderColl.Count > 0) Then
                Return CType(PODraftHeaderColl(0), PODraftHeader)
            End If
            Return New PODraftHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PODraftHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PODraftHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PODraftHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PODraftHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PODraftHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PODraftHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PODraftHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PODraftHeader As ArrayList = m_PODraftHeaderMapper.RetrieveByCriteria(criterias)
            Return _PODraftHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PODraftHeaderColl As ArrayList = m_PODraftHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PODraftHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(SortColumn)) And (Not IsNothing(SortColumn)) Then
                sortColl.Add(New Search.Sort(GetType(PODraftHeader), SortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim PODraftHeaderColl As ArrayList = m_PODraftHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PODraftHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PODraftHeaderColl As ArrayList = m_PODraftHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PODraftHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PODraftHeaderColl As ArrayList = m_PODraftHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PODraftHeader), columnName, matchOperator, columnValue))
            Return PODraftHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PODraftHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftHeader), columnName, matchOperator, columnValue))

            Return m_PODraftHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftHeader), "PODraftHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PODraftHeader), "PODraftHeaderCode", AggregateType.Count)
            Return CType(m_PODraftHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.PODraftHeader) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.PODraftHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.PODraftHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is PODraftDetail) Then
                CType(InsertArg.DomainObject, PODraftDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function Insert(ByVal objDomain As PODraftHeader) As Integer
            Dim iReturn As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As PODraftDetail In objDomain.PODraftDetail
                        item.PODraftHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iReturn = objDomain.ID
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
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As PODraftHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objDomain.PODraftDetail.Count > 0 Then
                        For Each objPODraftDetail As PODraftDetail In objDomain.PODraftDetail
                            objPODraftDetail.PODraftHeader = objDomain
                            If m_userPrincipal.Identity.Name = "" Then
                                _user = "SAP"
                            Else
                                _user = m_userPrincipal.Identity.Name
                            End If
                            m_TransactionManager.AddUpdate(objPODraftDetail, _user)
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
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

        Public Sub Delete(ByVal objDomain As PODraftHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PODraftHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As PODraftHeader)
            Try
                For Each oPODD As PODraftDetail In objDomain.PODraftDetail
                    m_PODraftDetailMapper.Delete(oPODD)
                Next
                m_PODraftHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Shared Function GetMinTOPDaysByVehicleType(ByVal oPOH As PODraftHeader, ByVal aPODs As ArrayList, Optional ByVal IsFactoring As Boolean = False) As Integer
            Dim MinTOPDays As Integer = 10000
            Dim IsExist As Boolean = False
            Dim MinTemp As Integer
            Dim oMTDFac As New MaxTOPDayFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            For Each oPOD As PODraftDetail In aPODs
                MinTemp = oMTDFac.GetMinTOPDays(oPOH.Dealer.ID, oPOD.ContractDetail.VechileColor.VechileType.ID, IsFactoring)
                If MinTemp <= MinTOPDays Then
                    MinTOPDays = MinTemp
                    IsExist = True
                End If
            Next
            If IsExist = True Then
                Return MinTOPDays
            Else
                Return 0 'will be validated by number of days in current month
            End If
            Return MinTOPDays
        End Function

        Public Function IsEnabledCreditControl(ByVal objDealer As Dealer) As Boolean
            Dim objTransaction As TransactionControl = New DealerFacade(Me.m_userPrincipal).RetrieveTransactionControl(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.CreditControl).ToString)
            If (objTransaction Is Nothing) Then
                Return True
            Else
                If objTransaction.Status = EnumDealerStatus.DealerStatus.Aktive Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Shared Function IsCODValid(ByVal POH As PODraftHeader, ByRef msg As String) As Boolean
            msg = ""

            Try
                Dim oMTDFac As New MaxTOPDayFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                If POH.TermOfPayment.PaymentType = CInt(enumPaymentType.PaymentType.COD) Then
                    For Each pod As PODraftDetail In POH.PODraftDetail
                        Dim isCOD As New ArrayList


                        Dim cMTD As New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active.Active, Short)))
                        cMTD.opAnd(New Criteria(GetType(MaxTOPDay), "DealerID", MatchType.Exact, POH.Dealer.ID))
                        cMTD.opAnd(New Criteria(GetType(MaxTOPDay), "VechileTypeID", MatchType.Exact, pod.ContractDetail.VechileColor.VechileType.ID))

                        isCOD = oMTDFac.Retrieve(cMTD)

                        If Not IsNothing(isCOD) AndAlso isCOD.Count > 0 Then
                            If CType(isCOD(0), MaxTOPDay).IsCOD = 0 Then
                                msg = msg & " " & pod.ContractDetail.VechileColor.VechileType.VechileCodeDesc & " \n"
                            End If
                        End If

                    Next

                    If msg <> "" Then

                        msg = "Kendaraan \n " & msg & " tidak berhak COD"
                        Return False
                    End If
                End If
            Catch ex As Exception

            End Try


            Return True
        End Function

        Public Function GetPOEffectiveDate(ByVal ReqAllocDateTime As Date, ByVal PaymentType As Integer, Optional ByVal TOPValue As Integer = 0) As Date
            Dim EffDate As Date = Date.MinValue

            If PaymentType = enumPaymentType.PaymentType.COD Then
                EffDate = AddNWorkingDay(ReqAllocDateTime, 2)
            ElseIf PaymentType = enumPaymentType.PaymentType.TOP Then
                EffDate = AddNWorkingDay(ReqAllocDateTime.AddDays(TOPValue), 1)
            ElseIf PaymentType = enumPaymentType.PaymentType.RTGS Then
                EffDate = ReqAllocDateTime
            End If

            Return EffDate
        End Function

        Private Function AddNWorkingDay(ByVal StateDate As Date, ByVal nAdded As Integer, Optional ByVal IsBackWard As Boolean = False) As Date
            'it has the same logic in Utility.CommonFunction.AddNWorkingDay
            Dim objNHFac As NationalHolidayFacade = New NationalHolidayFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("asp"), Nothing)))
            Dim crtNH As CriteriaComposite
            Dim rslDate As Date
            Dim IsHoliday As Boolean = True
            Dim arlNH As ArrayList = New ArrayList
            Dim i As Integer = 0

            rslDate = StateDate
            For i = 1 To Math.Abs(nAdded)
                rslDate = rslDate.AddDays(IIf(IsBackWard, -1, 1))
                IsHoliday = True
                While IsHoliday = True
                    crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
                    arlNH = objNHFac.Retrieve(crtNH)
                    If arlNH.Count < 1 Then
                        IsHoliday = False
                    Else
                        rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
                    End If
                End While
            Next
            Return rslDate
        End Function

#End Region

    End Class

End Namespace

