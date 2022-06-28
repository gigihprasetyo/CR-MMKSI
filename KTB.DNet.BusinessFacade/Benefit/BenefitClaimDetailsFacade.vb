
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
'// Generated on 12/2/2015 - 11:17:20 AM
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
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNET.BusinessFacade.FinishUnit

#End Region

Namespace KTB.DNet.BusinessFacade.Benefit

    Public Class BenefitClaimDetailsFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitClaimDetailsMapper As IMapper

        Private m_TransactionManager As TransactionManager
        Private m_BenefitMasterDetailMapper As IMapper
        Private m_BenefitClaimHeadersMapper As IMapper
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitClaimDetailsMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitClaimDetails).ToString)

            Me.m_BenefitMasterDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(BenefitMasterDetail).ToString)
            Me.m_BenefitClaimHeadersMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitClaimHeader).ToString)

            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BenefitClaimDetails
            Return CType(m_BenefitClaimDetailsMapper.Retrieve(ID), BenefitClaimDetails)
        End Function

        Public Function Retrieve(ByVal Code As String) As BenefitClaimDetails
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimDetailsCode", MatchType.Exact, Code))

            Dim BenefitClaimDetailsColl As ArrayList = m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias)
            If (BenefitClaimDetailsColl.Count > 0) Then
                Return CType(BenefitClaimDetailsColl(0), BenefitClaimDetails)
            End If
            Return New BenefitClaimDetails
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitClaimDetailsMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimDetails), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitClaimDetailsMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimDetails), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitClaimDetailsMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BenefitClaimDetails As ArrayList = m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias)
            Return _BenefitClaimDetails
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitClaimDetailsColl As ArrayList = m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitClaimDetailsColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitClaimDetailsColl As ArrayList = m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitClaimDetailsColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitClaimDetailsColl As ArrayList = m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), columnName, matchOperator, columnValue))
            Return BenefitClaimDetailsColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitClaimDetails), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), columnName, matchOperator, columnValue))

            Return m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimDetailsCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitClaimDetails), "BenefitClaimDetailsCode", AggregateType.Count)
            Return CType(m_BenefitClaimDetailsMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function RetrieveByCriteriaNoPaging(ByVal criterias As ICriteria) As ArrayList
            Dim BenefitClaimDetailsColl As ArrayList = m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias)
            Return BenefitClaimDetailsColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim BenefitClaimDetailsColl As ArrayList = m_BenefitClaimDetailsMapper.RetrieveByCriteria(criterias, Nothing, Nothing, Nothing, Nothing)
            Return BenefitClaimDetailsColl
        End Function



        Public Function UpdateStatus(ByVal arrayListObj As ArrayList, ByVal arrayListCheck As ArrayList, ByVal status As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrayListObj.Count > 0 Then


                        For Each itemsstring1 As BenefitClaimDetails In arrayListObj
                            If Not status Is Nothing Then
                                itemsstring1.StatusUpload = CInt(status)
                                m_TransactionManager.AddUpdate(itemsstring1, m_userPrincipal.Identity.Name)
                            End If
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




        Public Function UpdateStatusUpload(ByVal arrayListObj As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim ObjBenefitCalimHeader As BenefitClaimHeader
                    Dim ObjHTBCH As New Hashtable
                    Dim ArrBenefitClaimHeader As New ArrayList

                    If arrayListObj.Count > 0 Then
                        For Each itemsstring1 As BenefitClaimDetails In arrayListObj
                            If itemsstring1.StatusUpload = 1 Then
                                itemsstring1.DetailStatus = 1
                            Else
                                itemsstring1.DetailStatus = 2
                            End If
                            m_TransactionManager.AddUpdate(itemsstring1, m_userPrincipal.Identity.Name)

                            If itemsstring1.ID > 0 AndAlso Not IsNothing(itemsstring1.BenefitClaimHeader) Then
                                ObjBenefitCalimHeader = New BenefitClaimHeader(itemsstring1.BenefitClaimHeader.ID)
                                If Not IsNothing(ObjHTBCH(itemsstring1.BenefitClaimHeader.ID)) Then

                                Else
                                    ObjHTBCH.Add(itemsstring1.BenefitClaimHeader.ID, itemsstring1.BenefitClaimHeader.ID)
                                    ArrBenefitClaimHeader.Add(itemsstring1.BenefitClaimHeader)
                                End If
                            End If
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()

                        For Each objBCH As BenefitClaimHeader In ArrBenefitClaimHeader

                            Dim IsNew As Boolean = False
                            Dim isOK As Boolean = False
                            Dim IsNotOK As Boolean = False

                            For Each ObBCD As BenefitClaimDetails In objBCH.BenefitClaimDetailss
                                If ObBCD.DetailStatus = 0 Then
                                    IsNew = True
                                End If

                                If ObBCD.DetailStatus = 1 Then
                                    isOK = True
                                End If

                                If ObBCD.DetailStatus = 2 Then
                                    IsNotOK = True
                                End If
                            Next
                             
                            objBCH = m_BenefitClaimHeadersMapper.Retrieve(objBCH.ID)

                            If IsNew Then
                                objBCH.Status = BenefitClaimHeaderEnumStatus.Status.Konfirmasi
                            ElseIf IsNew = False AndAlso isOK = False AndAlso IsNotOK Then
                                objBCH.Status = BenefitClaimHeaderEnumStatus.Status.Tolak
                            ElseIf IsNew = False AndAlso isOK Then
                                objBCH.Status = BenefitClaimHeaderEnumStatus.Status.Proses
                            End If

                            m_BenefitClaimHeadersMapper.Update(objBCH, m_userPrincipal.Identity.Name)

                        Next

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





        Public Function isExist(ByVal list As ArrayList, ByVal ChassisMasterID As Integer) As Boolean
            If list.Count > 0 Then
                For Each el As BenefitClaimDetails In list
                    If el.ChassisMaster.ID = ChassisMasterID Then
                        Return True
                    End If
                Next
            End If
            Return False
        End Function

        'if result or return is 1, message formula already exist in transaksi before
        'if result or return is 2, message formula is not exist in benefit formula
        Public Function checkAnotherClaim(ByVal formula As String, ByVal ChassisMaster As ChassisMaster, _
                                            ByVal objBenefitMasterDetail As BenefitMasterDetail) As Short



            Dim _arrList As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strSql As String = ""

            strSql = ""
            strSql += "   select ID from BenefitClaimDetails where "
            strSql += " RowStatus = 0 and ChassisMasterID = '" & ChassisMaster.ID & "' "
            strSql += "  and detailstatus = 1 "
            criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "ID", MatchType.InSet, "(" & strSql & ")"))


            _arrList = Retrieve(criterias)
            Dim myAL As New ArrayList()
            Dim alFormula As New ArrayList
            For Each item As String In formula.Replace(" ", "").Replace("&amp;", "").Replace("&", "").Replace(",", "").Split(".")
                If Not item Is Nothing And Not item = "" Then
                    alFormula.Add(item)
                End If
            Next

            Dim isCanAdd As Boolean = False
            Dim testExistFormula As String = "B"
            Dim testAddFormula As String = "C"

            If _arrList.Count > 0 Then

                ''cek full
                'For Each item As String In alFormula
                '    Dim mustCharacterIn As String = item 'inisial karakter yg hatus diisi

                '    For Each bcd As BenefitClaimDetails In _arrList
                '        Dim objectBenefitMasterDetail = m_BenefitMasterDetailMapper.Retrieve(CShort(bcd.BenefitMasterDetail.ID))
                '        If item.Contains(objectBenefitMasterDetail.FormulaID.ToString) = True Then
                '            mustCharacterIn = mustCharacterIn.Replace(objectBenefitMasterDetail.FormulaID.ToString, "")
                '        End If
                '    Next
                '    If mustCharacterIn.Length = 0 Then 'cek jika sudah full
                '        Return 3
                '    End If
                'Next


                ''cek aktif formula










                Dim index As Short = 0

                For Each item As String In alFormula

                    Dim mustCharacterIn As String = "-" 'inisial karakter yg hatus diisi
                    Dim tempFormulaIndex As String = item

                    mustCharacterIn = item

                    For Each bcd As BenefitClaimDetails In _arrList
                        Dim objectBenefitMasterDetail = m_BenefitMasterDetailMapper.Retrieve(CShort(bcd.BenefitMasterDetail.ID))
                        If objectBenefitMasterDetail.FormulaID = objBenefitMasterDetail.FormulaID Then
                            Return 1
                        End If
                        If Not objectBenefitMasterDetail.FormulaID.ToString = "" Then
                            If item.Contains(objectBenefitMasterDetail.FormulaID.ToString) = True Then
                                ' mustCharacterIn = mustCharacterIn.Replace("-", "")
                                tempFormulaIndex = tempFormulaIndex.Replace(objectBenefitMasterDetail.FormulaID.ToString, "")

                                ' mustCharacterIn = mustCharacterIn & tempFormulaIndex


                                mustCharacterIn = mustCharacterIn.Replace(objectBenefitMasterDetail.FormulaID.ToString, "")


                                index = index + 1
                            Else
                                mustCharacterIn = "-"
                                Exit For
                            End If
                        End If
                    Next

                    If mustCharacterIn.Length = 0 Then 'cek jika sudah full
                        Return 3
                    End If

                    'If mustCharacterIn.Contains("-") = False And mustCharacterIn.Contains(objBenefitMasterDetail.FormulaID.ToString) = False Then
                    '    Return 2
                    'End If

                    If mustCharacterIn.Contains(objBenefitMasterDetail.FormulaID.ToString) = True Then
                        Return 0
                        'isCanAdd = True
                    End If

                Next
            Else
                Return 0
            End If

            'If isCanAdd = True Then
            '    Return 0
            'End If

            Return 2
        End Function
#End Region

#Region "Custom Method"


        Public Function ChassisNumberValidation(ByVal chassisMasterID As Integer, ByVal leasingCompanyID As Integer) As BenefitClaimDetails

            Dim objDomainDetail As BenefitClaimDetails
            Dim oSCFac As New sp_checkInputClaimFacade(m_userPrincipal)
            Dim aSCs As ArrayList

            Try

                Dim oEndCustomerID As Integer = 0
                Dim oBenefitClaimDetailID As Integer = 0
                Dim oBenefitMasterHeaderID As Integer = 0
                Dim oBenefitTypeID As Integer = 0
                Dim oLeasingCompanyID As Integer = 0
                Dim oIsDebug As Integer = 0

                Dim objChassisMasterFacadeX As ChassisMasterFacade = New ChassisMasterFacade(m_userPrincipal)
                Dim objChassisMasterX As ChassisMaster = objChassisMasterFacadeX.Retrieve(chassisMasterID)
                If objChassisMasterX.ID > 0 Then
                    oEndCustomerID = objChassisMasterX.EndCustomer.ID
                End If

                'If ddlPilihanClaim.SelectedItem.Text.ToLower.IndexOf("leas") > -1 Then
                oLeasingCompanyID = leasingCompanyID
                'End If

                '  exec sp_checkInputClaim_Donas @EndCustomerID=1292976
                ', @BenefitClaimDetailID = 0 --0:Input Claim;>0:Edit
                ', @BenefitMasterHeaderID = 29 
                ', @BenefitTypeID=13
                ', @LeasingCompanyID = 8
                ', @IsDebug=1

                objDomainDetail = New BenefitClaimDetails
                objDomainDetail.ChassisMaster = objChassisMasterX
                aSCs = oSCFac.RetrieveFromSP_(oEndCustomerID, 0, 0, 0, oLeasingCompanyID, oIsDebug)

                If aSCs.Count > 0 Then
                    Dim objInputClaim As sp_checkInputClaim = CType(aSCs(0), sp_checkInputClaim)
                    If Not objInputClaim Is Nothing Then
                        If objInputClaim.IsValid <> 1 Then
                            'MessageBox.Show(objInputClaim.Message)
                            objDomainDetail.ErrorMessage = objInputClaim.Message
                        End If
                    End If
                End If
            Catch ex As Exception
                Dim str = ex.Message
            End Try


            Return objDomainDetail
        End Function


        Public Function ChassisNumberValidation(ByVal objDealerID As Integer, ByVal chassisMasterID As Integer, ByVal benefitMasterHeaderID As Integer _
                                                , ByVal benefitTypeID As Integer, ByVal arDetailSession As ArrayList, _
                                                ByVal leasingCompanyID As Integer, _
                                                Optional ByVal isUploadLeasing As Boolean = False, Optional ByVal RegEventID As Integer = 0, Optional ByVal oBenefitClaimDetailID As Integer = 0) As BenefitClaimDetails

            Dim objDomainDetail As BenefitClaimDetails
            Dim oSCFac As New sp_checkInputClaimFacade(m_userPrincipal)
            Dim aSCs As ArrayList

            Try

                Dim oEndCustomerID As Integer = 0
                Dim oBenefitMasterHeaderID As Integer = 0
                Dim oBenefitTypeID As Integer = 0
                Dim oLeasingCompanyID As Integer = 0
                Dim oIsDebug As Integer = 0

                Dim objChassisMasterFacadeX As ChassisMasterFacade = New ChassisMasterFacade(m_userPrincipal)
                Dim objChassisMasterX As ChassisMaster = objChassisMasterFacadeX.Retrieve(chassisMasterID)
                If objChassisMasterX.ID > 0 Then
                    oEndCustomerID = objChassisMasterX.EndCustomer.ID
                Else

                End If

                Dim objBenefitMasterHeaderX As BenefitMasterHeader = New BenefitMasterHeader
                Dim objBenefitMasterHeaderFacadeX As BenefitMasterHeaderFacade = New BenefitMasterHeaderFacade(m_userPrincipal)
                objBenefitMasterHeaderX = objBenefitMasterHeaderFacadeX.Retrieve(benefitMasterHeaderID)
                If objBenefitMasterHeaderX.ID > 0 Then
                    oBenefitMasterHeaderID = objBenefitMasterHeaderX.ID
                End If

                Dim objBenefitType As BenefitType = New BenefitTypeFacade(m_userPrincipal).Retrieve(CShort(benefitTypeID))

                Dim objBenefitMasterDetailX As BenefitMasterDetail = New BenefitMasterDetail
                For Each item As BenefitMasterDetail In objBenefitMasterHeaderX.BenefitMasterDetails
                    If Not item Is Nothing Then

                        'If item.BenefitType.ID = benefitTypeID And _
                        '   item.AssyYear = objChassisMasterX.ProductionYear And ((objChassisMasterX.EndCustomer.ValidateTime >= item.FakturValidationStart And _
                        '     objChassisMasterX.EndCustomer.ValidateTime <= item.FakturValidationEnd)) Then
                        'If item.BenefitType.ID = benefitTypeID And _
                        '  item.AssyYear = objChassisMasterX.ProductionYear Then
                        If item.BenefitType.ID = benefitTypeID Then

                            Dim cekVechille As Boolean = False
                            Dim cekLeasing As Boolean = False
                            Dim cekEventValidation As Boolean = False
                            Dim cekAssy As Boolean = False

                            If objBenefitType.AssyYearBox = 1 Then
                                If item.AssyYear = objChassisMasterX.ProductionYear Then
                                    cekAssy = True
                                End If
                            Else
                                cekAssy = True
                            End If

                            'Remve timeStamp
                            If item.BenefitType.EventValidation = 0 Then
                                If ((objChassisMasterX.EndCustomer.ValidateTime.Date >= item.FakturValidationStart And _
                                     objChassisMasterX.EndCustomer.ValidateTime.Date <= item.FakturValidationEnd)) Then
                                    cekEventValidation = True
                                End If
                            Else
                                If ((objChassisMasterX.EndCustomer.ValidateTime.Date >= item.FakturOpenStart And _
                                    objChassisMasterX.EndCustomer.ValidateTime.Date <= item.FakturOpenEnd)) Then
                                    cekEventValidation = True
                                End If
                            End If



                            For Each itemVehicleType As BenefitMasterVehicleType In item.BenefitMasterVehicleTypes
                                If itemVehicleType.VechileType.ID = objChassisMasterX.VechileColor.VechileType.ID Then
                                    cekVechille = True
                                    Exit For
                                End If
                            Next
                            If item.BenefitMasterLeasings.Count > 1 Then
                                For Each itemLeasing As BenefitMasterLeasing In item.BenefitMasterLeasings
                                    If itemLeasing.LeasingCompany.ID = leasingCompanyID Then
                                        cekLeasing = True
                                        Exit For
                                    End If
                                Next
                            Else
                                cekLeasing = True
                            End If


                            If cekAssy = True And cekEventValidation = True And cekVechille = True And cekLeasing = True Then
                                objBenefitMasterDetailX = item
                                Exit For
                            End If

                        End If


                    End If
                Next

                Dim objBenefitTypeFacadeX As BenefitTypeFacade = New BenefitTypeFacade(m_userPrincipal)
                Dim objBenefitTypeX As BenefitType = objBenefitTypeFacadeX.Retrieve(CShort(benefitTypeID))
                If Not IsNothing(objBenefitTypeX) Then
                    If objBenefitTypeX.ID > 0 Then
                        oBenefitTypeID = objBenefitTypeX.ID
                    End If
                End If

                'If ddlPilihanClaim.SelectedItem.Text.ToLower.IndexOf("leas") > -1 Then
                oLeasingCompanyID = leasingCompanyID
                'End If

                '  exec sp_checkInputClaim_Donas @EndCustomerID=1292976
                ', @BenefitClaimDetailID = 0 --0:Input Claim;>0:Edit
                ', @BenefitMasterHeaderID = 29 
                ', @BenefitTypeID=13
                ', @LeasingCompanyID = 8
                ', @IsDebug=1

                objDomainDetail = New BenefitClaimDetails
                objDomainDetail.ChassisMaster = objChassisMasterX
                objDomainDetail.BenefitMasterDetail = objBenefitMasterDetailX

                If IsNothing(objBenefitMasterDetailX) OrElse objBenefitMasterDetailX.ID = 0 Then
                    objDomainDetail.ErrorMessage = "Benefit Detail tidak terdaftar, "
                End If

                If objBenefitTypeX.EventValidation = 1 Then
                    Dim query = "SELECT g.SPKNumber " &
                            "FROM Chassismaster a " &
                            "JOIN EndCustomer b on b.ID = a.EndCustomerID " &
                            "JOIN Customer c on c.ID = b.CustomerID " &
                            "JOIN CustomerRequest d on d.CUstomerCode = c.Code " &
                            "JOIN SPKDetailCustomer e on e.CustomerRequestID = d.ID " &
                            "JOIN SPKDetail f on f.ID = e.SPKDetailID " &
                            "JOIN SPKHeader g on g.ID = f.SPKHeaderID " &
                            "WHERE a.ID = " & chassisMasterID

                    Dim criterias As New CriteriaComposite(New Criteria(GetType(BenefitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(BenefitEventDetail), "BenefitEventHeader.ID", MatchType.Exact, CShort(RegEventID)))
                    criterias.opAnd(New Criteria(GetType(BenefitEventDetail), "BenefitParticipant.Remarks", MatchType.InSet, "(" & query & ")"))
                    Dim objBenefitEventDetailX As ArrayList = New BenefitEventDetailFacade(m_userPrincipal).Retrieve(criterias)

                    If IsNothing(objBenefitEventDetailX) OrElse objBenefitEventDetailX.Count = 0 Then
                        objDomainDetail.ErrorMessage = "No Reg Event tidak terdaftar, "
                    End If
                End If

                'begin ubah adit buat check 1 claim 20150308
                Dim isExistOneClaim As Boolean = isExist(arDetailSession, chassisMasterID)
                If isExistOneClaim = True Then
                    objDomainDetail.ErrorMessage = "Nomor Rangka sudah ada dalam 1 claim"
                Else
                    If isUploadLeasing = True Then
                        aSCs = oSCFac.RetrieveFromSP_(oEndCustomerID, 0, 0, 0, oLeasingCompanyID, oIsDebug)
                    Else
                        aSCs = oSCFac.RetrieveFromSP_(oEndCustomerID, oBenefitClaimDetailID, oBenefitMasterHeaderID, oBenefitTypeID, oLeasingCompanyID, oIsDebug)
                    End If

                    If aSCs.Count > 0 Then
                        Dim objInputClaim As sp_checkInputClaim = CType(aSCs(0), sp_checkInputClaim)
                        If Not objInputClaim Is Nothing Then
                            If objInputClaim.IsValid <> 1 Then
                                'MessageBox.Show(objInputClaim.Message)
                                objDomainDetail.ErrorMessage = objInputClaim.Message
                            End If
                        End If
                    End If
                End If
                'end

                ''add new CR DSF
                'If Not IsNothing(objChassisMasterX.EndCustomer) AndAlso objChassisMasterX.EndCustomer.ID > 0 Then
                '    If objChassisMasterX.EndCustomer.IsTemporary > 0 Then
                '        objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + ", No rangka tidak dapat digunakan untuk pengajuan claim,"
                '    End If
                'End If
                'If leasingCompanyID = 1 Then        '-- Leasing DIPO
                '    If chassisMasterID > 0 Then
                '        Dim objDSFLeasingClaim As New DSFLeasingClaim
                '        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '        crit.opAnd(New Criteria(GetType(DSFLeasingClaim), "ChassisMaster.ID", MatchType.Exact, chassisMasterID))
                '        Dim arrl As ArrayList = New DSFLeasingClaimFacade(m_userPrincipal).Retrieve(crit)
                '        If Not IsNothing(arrl) AndAlso arrl.Count > 0 Then
                '            objDSFLeasingClaim = CType(arrl(0), DSFLeasingClaim)
                '            objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + ", No rangka sudah pernah diajukan claim oleh Leasing dengan nomor klaim " & objDSFLeasingClaim.RegNumber & ","
                '        End If
                '    End If
                'End If

                'chassis yang diupload harus dari dealer yang bersangkutan.
                If objDomainDetail.ChassisMaster.Dealer.ID <> objDealerID Then
                    objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + ", No rangka tidak terdaftar di dealer anda,"
                End If

                objDomainDetail.ID = oBenefitClaimDetailID

                'If isUploadLeasing = True Then
                '    aSCs = oSCFac.RetrieveFromSP_(oEndCustomerID, 0, 0, 0, oLeasingCompanyID, oIsDebug)
                'Else
                '    aSCs = oSCFac.RetrieveFromSP_(oEndCustomerID, oBenefitClaimDetailID, oBenefitMasterHeaderID, oBenefitTypeID, oLeasingCompanyID, oIsDebug)
                'End If

                'If aSCs.Count > 0 Then
                '    Dim objInputClaim As sp_checkInputClaim = CType(aSCs(0), sp_checkInputClaim)
                '    If Not objInputClaim Is Nothing Then
                '        If objInputClaim.IsValid <> 1 Then
                '            'MessageBox.Show(objInputClaim.Message)
                '            objDomainDetail.ErrorMessage = objInputClaim.Message
                '        End If
                '    End If
                'End If
            Catch ex As Exception
                Dim str = ex.Message
                objDomainDetail.ErrorMessage = objDomainDetail.ErrorMessage + " " + ex.Message
            End Try


            Return objDomainDetail
        End Function




        Public Function UpdateKeterangan(ByVal objBenefitClaimDetails As BenefitClaimDetails) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddUpdate(objBenefitClaimDetails, m_userPrincipal.Identity.Name)
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

