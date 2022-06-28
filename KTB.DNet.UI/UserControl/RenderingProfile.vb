Imports KTB.DNet.WebCC
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Security.Principal
Imports KTB.DNet.Utility


Public Class RenderingProfile

#Region "Constructor"
    Public Sub New()
    End Sub

    Public Sub New(ByVal _isReadOnly As Boolean)
        IsReadOnly = _isReadOnly
    End Sub
#End Region

#Region "Private Variable"
    Private objHst As Hashtable = New Hashtable
    Private m_isReadOnly As Boolean = True
    Private m_SessionName As String = ""
#End Region

#Region "Property"
    Property IsReadOnly() As Boolean
        Get
            Return m_isReadOnly
        End Get
        Set(ByVal Value As Boolean)
            m_isReadOnly = Value
        End Set
    End Property
#End Region

#Region "Public Method"
    Public Function GeneratePanel(ByVal objID As Integer, ByVal objPanel As Panel, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal User As IPrincipal, Optional ByVal blnViewOnly As Boolean = False, Optional ByVal EnableClientScript As Boolean = True, Optional ByVal SessionName As String = "") As Panel
        If SessionName <> "" Then
            m_SessionName = SessionName
        End If
        Dim listProfileHeader As ArrayList = GetListProfileHeader(objID, objGroup, profileType)
        If Not listProfileHeader Is Nothing Then
            If listProfileHeader.Count > 0 Then
                If blnViewOnly <> True Then
                    RenderPanel(objPanel, listProfileHeader, objGroup, User, EnableClientScript)
                Else
                    RenderPanelView(objPanel, listProfileHeader, objGroup, User)
                End If

            End If
        End If
        Return objPanel
    End Function

    Public Function GeneratePanel(ByVal arrListProfile As ArrayList, ByVal objPanel As Panel, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal User As IPrincipal, Optional ByVal blnViewOnly As Boolean = False) As Panel
        If arrListProfile.Count > 0 Then
            If blnViewOnly <> True Then
                RenderPanel(objPanel, arrListProfile, objGroup, User)
            Else
                RenderPanelView(objPanel, arrListProfile, objGroup, User)
            End If
        End If
        Return objPanel
    End Function

    Public Function GetProfileData(ByVal objPage As Page, ByVal objGroup As ProfileGroup, Optional ByVal isRetrieveAll As Boolean = False) As ArrayList
        Dim hstPassing As Hashtable = CType(HttpContext.Current.Session.Item("PROFILE" & "_" & objGroup.ID), Hashtable)
        Dim listProfile As ArrayList = New ArrayList
        If Not hstPassing Is Nothing Then
            If hstPassing.Count > 0 Then
                For Each key As String In hstPassing.Keys
                    Select Case key.Split("-")(0)
                        Case EnumControlType.ControlType.Text
                            Dim ctr As TextBox = CType(objPage.FindControl(key.Split("-")(1)), TextBox)
                            'Dim objProfileHeader As ProfileHeader = CType(hstPassing.Item(key), ProfileHeader)
                            Dim objProfileHeader As ProfileHeaderToGroup = CType(hstPassing.Item(key), ProfileHeaderToGroup)

                            objProfileHeader.ProfileHeader.ProfileHeaderValue = ctr.Text
                            If objProfileHeader.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar Then
                                If ctr.Text = String.Empty Then
                                    If Not isRetrieveAll Then
                                        Return New ArrayList
                                    End If
                                    'MessageBox.Show("Data tidak boleh kosong")
                                End If
                            End If
                            listProfile.Add(objProfileHeader)
                        Case EnumControlType.ControlType.Calendar
                            'Dim ctr As Calendar = CType(objPage.FindControl(key.Split("-")(1)), Calendar)
                            Dim ctr As KTB.DNet.WebCC.IntiCalendar = CType(objPage.FindControl(key.Split("-")(1)), KTB.DNet.WebCC.IntiCalendar)
                            'Dim objProfileHeader As ProfileHeader = CType(hstPassing.Item(key), ProfileHeader)
                            Dim objProfileHeader As ProfileHeaderToGroup = CType(hstPassing.Item(key), ProfileHeaderToGroup)
                            'objProfileHeader.ProfileHeader.ProfileHeaderValue = ctr.SelectedDate
                            objProfileHeader.ProfileHeader.ProfileHeaderValue = ctr.Value
                            listProfile.Add(objProfileHeader)
                        Case EnumControlType.ControlType.CheckListBox
                            Dim ctr As CheckBoxList = CType(objPage.FindControl(key.Split("-")(1)), CheckBoxList)

                            'Dim objProfileHeader As ProfileHeader = CType(hstPassing.Item(key), ProfileHeader)
                            Dim objProfileHeader As ProfileHeaderToGroup = CType(hstPassing.Item(key), ProfileHeaderToGroup)
                            Dim calResult As Boolean = False
                            objProfileHeader.ProfileHeader.ProfileHeaderValue = String.Empty
                            For Each item As ListItem In ctr.Items
                                If item.Selected = True Then
                                    objProfileHeader.ProfileHeader.ProfileHeaderValue += item.Value & "-"
                                    calResult = True
                                End If
                            Next
                            If objProfileHeader.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar Then
                                If Not calResult Then
                                    'MessageBox.Show("Data belum di pilih.")
                                    Return New ArrayList
                                End If
                            End If
                            If Not objProfileHeader.ProfileHeader.ProfileHeaderValue Is Nothing Then
                                objProfileHeader.ProfileHeader.ProfileHeaderValue = objProfileHeader.ProfileHeader.ProfileHeaderValue.Trim("-")
                            End If
                            listProfile.Add(objProfileHeader)
                        Case EnumControlType.ControlType.List
                            Dim ctr As DropDownList = CType(objPage.FindControl(key.Split("-")(1)), DropDownList)
                            'Dim objProfileHeader As ProfileHeader = CType(hstPassing.Item(key), ProfileHeader)
                            Dim objProfileHeader As ProfileHeaderToGroup = CType(hstPassing.Item(key), ProfileHeaderToGroup)
                            Dim listResult As Boolean = False
                            If ctr.SelectedItem.Value <> "" Then
                                objProfileHeader.ProfileHeader.ProfileHeaderValue = ctr.SelectedItem.Value
                                listResult = True
                            End If
                            'For Each item As ListItem In ctr.Items
                            '    If item.Selected = True And item.Value <> "-1" Then
                            '        objProfileHeader.ProfileHeader.ProfileHeaderValue = item.Value
                            '        listResult = True
                            '    End If
                            'Next
                            If objProfileHeader.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar Then
                                If Not listResult Then
                                    If Not isRetrieveAll Then
                                        MessageBox.Show("Data belum di pilih.")
                                        Return New ArrayList
                                    End If
                                End If
                            End If
                            If Not objProfileHeader.ProfileHeader.ProfileHeaderValue Is Nothing Then
                                objProfileHeader.ProfileHeader.ProfileHeaderValue = objProfileHeader.ProfileHeader.ProfileHeaderValue.Trim("-")
                            End If

                            listProfile.Add(objProfileHeader)
                    End Select
                Next
            End If
        End If

        If listProfile Is Nothing Then
            listProfile = New ArrayList
        End If
        Return listProfile
    End Function

    Public Function RetrieveValueByProfileHeaderID(ByVal objPage As Page, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal User As IPrincipal, ByVal ProfileHeaderID As Integer) As String

        Dim listProfileHeaderList As ArrayList = GetProfileData(objPage, objGroup)
        Select Case profileType
            Case EnumProfileType.ProfileType.SALESMAN
                If listProfileHeaderList.Count > 0 Then
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        If objProHeader.ID = ProfileHeaderID Then
                            Return item.ProfileHeader.ProfileHeaderValue
                        End If
                    Next
                End If

        End Select
        Return ""
    End Function

    Public Function RetrieveProfileValue(ByVal objPage As Page, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal User As IPrincipal, Optional ByVal isRetrieveAll As Boolean = False) As ArrayList
        Dim listProfileHeaderList As ArrayList = GetProfileData(objPage, objGroup, isRetrieveAll)
        Select Case profileType
            Case EnumProfileType.ProfileType.CS
                Dim objSalesmanProfile As SalesmanProfile
                Dim objSalesmanProfileHistory As SalesmanProfileHistory
                Dim objListSalesmanProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        'For Each item As ProfileHeader In listProfileHeaderList
                        objSalesmanProfile = New SalesmanProfile
                        objSalesmanProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objSalesmanProfile.ProfileHeader = objProHeader
                        objSalesmanProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objSalesmanProfileHistory = New SalesmanProfileHistory
                        objSalesmanProfileHistory.ProvileValue = item.ProfileHeader.ProfileHeaderValue
                        objSalesmanProfileHistory.SalesmanProfile = objSalesmanProfile
                        objSalesmanProfile.SalesmanProfileHistorys.Add(objSalesmanProfileHistory)
                        objListSalesmanProfile.Add(objSalesmanProfile)
                    Next
                End If
                Return objListSalesmanProfile
            Case EnumProfileType.ProfileType.SALESMAN
                Dim objSalesmanProfile As SalesmanProfile
                Dim objSalesmanProfileHistory As SalesmanProfileHistory
                Dim objListSalesmanProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        'For Each item As ProfileHeader In listProfileHeaderList
                        objSalesmanProfile = New SalesmanProfile
                        objSalesmanProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objSalesmanProfile.ProfileHeader = objProHeader
                        objSalesmanProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objSalesmanProfileHistory = New SalesmanProfileHistory
                        objSalesmanProfileHistory.ProvileValue = item.ProfileHeader.ProfileHeaderValue
                        objSalesmanProfileHistory.SalesmanProfile = objSalesmanProfile
                        objSalesmanProfile.SalesmanProfileHistorys.Add(objSalesmanProfileHistory)
                        objListSalesmanProfile.Add(objSalesmanProfile)
                    Next
                End If
                Return objListSalesmanProfile
            Case EnumProfileType.ProfileType.CUSTOMER
                Dim objCustomerProfile As CustomerProfile
                Dim objCustomerProfileHistory As CustomerProfileHistory
                Dim objListCustomerProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objCustomerProfile = New CustomerProfile
                        objCustomerProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objCustomerProfile.ProfileHeader = objProHeader
                        objCustomerProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objCustomerProfileHistory = New CustomerProfileHistory
                        objCustomerProfileHistory.ProvileValue = item.ProfileHeader.ProfileHeaderValue
                        objCustomerProfileHistory.CustomerProfile = objCustomerProfile
                        objCustomerProfile.CustomerProfileHistorys.Add(objCustomerProfileHistory)
                        objListCustomerProfile.Add(objCustomerProfile)
                    Next
                End If
                Return objListCustomerProfile
            Case EnumProfileType.ProfileType.CUSTOMERREQUEST
                Dim objCustomerRequestProfile As CustomerRequestProfile
                Dim objCustomerRequestProfileHistory As CustomerRequestProfileHistory
                Dim objListCustomerRequestProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objCustomerRequestProfile = New CustomerRequestProfile
                        objCustomerRequestProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objCustomerRequestProfile.ProfileHeader = objProHeader
                        objCustomerRequestProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objCustomerRequestProfileHistory = New CustomerRequestProfileHistory
                        objCustomerRequestProfileHistory.ProvileValue = item.ProfileHeader.ProfileHeaderValue
                        objCustomerRequestProfileHistory.CustomerRequestProfile = objCustomerRequestProfile
                        objCustomerRequestProfile.CustomerRequestProfileHistorys.Add(objCustomerRequestProfileHistory)
                        objListCustomerRequestProfile.Add(objCustomerRequestProfile)
                    Next
                End If
                Return objListCustomerRequestProfile
            Case EnumProfileType.ProfileType.DEALER
                Dim objDealerProfile As DealerProfile
                Dim objDealerProfileHistory As DealerProfileHistory
                Dim objListDealerProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objDealerProfile = New DealerProfile
                        objDealerProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objDealerProfile.ProfileHeader = objProHeader
                        objDealerProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objDealerProfileHistory = New DealerProfileHistory
                        objDealerProfileHistory.ProvileValue = item.ProfileHeader.ProfileHeaderValue
                        objDealerProfileHistory.DealerProfile = objDealerProfile
                        objDealerProfile.DealerProfileHistorys.Add(objDealerProfileHistory)
                        objListDealerProfile.Add(objDealerProfile)
                    Next
                End If
                Return objListDealerProfile
            Case EnumProfileType.ProfileType.PQR
                Dim objPQRHeaderProfile As PQRProfile
                Dim objPQRHeaderProfileHistory As PQRProfileHistory
                Dim objListPQRHeaderProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objPQRHeaderProfile = New PQRProfile
                        objPQRHeaderProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objPQRHeaderProfile.ProfileHeader = objProHeader
                        objPQRHeaderProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objPQRHeaderProfileHistory = New PQRProfileHistory
                        objPQRHeaderProfileHistory.ProvileValue = item.ProfileHeader.ProfileHeaderValue
                        objPQRHeaderProfileHistory.PQRProfile = objPQRHeaderProfile
                        objPQRHeaderProfile.PQRProfileHistorys.Add(objPQRHeaderProfileHistory)
                        objListPQRHeaderProfile.Add(objPQRHeaderProfile)
                    Next
                End If
                Return objListPQRHeaderProfile
            Case EnumProfileType.ProfileType.PQRBB
                Dim objPQRHeaderProfileBB As PQRProfileBB
                Dim objPQRHeaderProfileHistoryBB As PQRProfileHistoryBB
                Dim objListPQRHeaderProfileBB As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objPQRHeaderProfileBB = New PQRProfileBB
                        objPQRHeaderProfileBB.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objPQRHeaderProfileBB.ProfileHeader = objProHeader
                        objPQRHeaderProfileBB.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objPQRHeaderProfileHistoryBB = New PQRProfileHistoryBB
                        objPQRHeaderProfileHistoryBB.ProvileValue = item.ProfileHeader.ProfileHeaderValue
                        objPQRHeaderProfileHistoryBB.PQRProfileBB = objPQRHeaderProfileBB
                        objPQRHeaderProfileBB.PQRProfileHistoryBBs.Add(objPQRHeaderProfileHistoryBB)
                        objListPQRHeaderProfileBB.Add(objPQRHeaderProfileBB)
                    Next
                End If
                Return objListPQRHeaderProfileBB
            Case EnumProfileType.ProfileType.CHASSISMASTER
                Dim objChassisMasterProfile As ChassisMasterProfile
                Dim objChassisMasterProfileHistory As ChassisMasterProfileHistory
                Dim objListChassisMasterProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objChassisMasterProfile = New ChassisMasterProfile
                        objChassisMasterProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objChassisMasterProfile.ProfileHeader = objProHeader
                        'objChassisMasterProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objChassisMasterProfile.ProfileValue = item.ProfileValue
                        objChassisMasterProfileHistory = New ChassisMasterProfileHistory
                        'objChassisMasterProfileHistory.ProvileValue = item.ProfileHeader.ProfileHeaderValue
                        objChassisMasterProfileHistory.ProvileValue = item.ProfileValue
                        objChassisMasterProfileHistory.ChassisMasterProfile = objChassisMasterProfile
                        objChassisMasterProfile.ChassisMasterProfileHistorys.Add(objChassisMasterProfileHistory)
                        objListChassisMasterProfile.Add(objChassisMasterProfile)
                    Next
                End If
                Return objListChassisMasterProfile
            Case EnumProfileType.ProfileType.SPKCUSTOMER
                Dim objSPKCustomerProfile As SPKCustomerProfile
                Dim objListSPKCustomerProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objSPKCustomerProfile = New SPKCustomerProfile
                        objSPKCustomerProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objSPKCustomerProfile.ProfileHeader = objProHeader
                        objSPKCustomerProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objListSPKCustomerProfile.Add(objSPKCustomerProfile)
                    Next
                End If
                Return objListSPKCustomerProfile
            Case EnumProfileType.ProfileType.SPKPROFILE
                Dim objSPKProfile As SPKProfile
                Dim objListSPKProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objSPKProfile = New SPKProfile
                        objSPKProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objSPKProfile.ProfileHeader = objProHeader
                        objSPKProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objListSPKProfile.Add(objSPKProfile)
                    Next
                End If
                Return objListSPKProfile

            Case EnumProfileType.ProfileType.SPKDETAILCUSTOMER
                Dim objSPKDetailCustomerProfile As SPKDetailCustomerProfile
                Dim objListSPKDetailCustomerProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objSPKDetailCustomerProfile = New SPKDetailCustomerProfile
                        objSPKDetailCustomerProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objSPKDetailCustomerProfile.ProfileHeader = objProHeader
                        objSPKDetailCustomerProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objListSPKDetailCustomerProfile.Add(objSPKDetailCustomerProfile)
                    Next
                End If
                Return objListSPKDetailCustomerProfile


            Case EnumProfileType.ProfileType.SPKDETAILCUSTOMERPROFILE
                Dim objSPKProfile As SPKProfile
                Dim objListSPKProfile As ArrayList = New ArrayList
                If listProfileHeaderList.Count > 0 Then
                    'For Each item As ProfileHeader In listProfileHeaderList
                    For Each item As ProfileHeaderToGroup In listProfileHeaderList
                        objSPKProfile = New SPKProfile
                        objSPKProfile.ProfileGroup = objGroup
                        Dim objProHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(item.ProfileHeader.ID)
                        objSPKProfile.ProfileHeader = objProHeader
                        objSPKProfile.ProfileValue = item.ProfileHeader.ProfileHeaderValue
                        objListSPKProfile.Add(objSPKProfile)
                    Next
                End If
                Return objListSPKProfile
        End Select
        Return Nothing
    End Function

#End Region


#Region "Private Method"

    Public Function GetListProfileHeader(ByVal objID As Integer, ByVal objGroup As ProfileGroup, ByVal profileType As Short) As ArrayList
        Dim listProfile As ArrayList = objGroup.ProfileHeaderToGroups
        Dim objListProfileHeader As ArrayList = New ArrayList

        If Not listProfile Is Nothing Then
            If listProfile.Count > 0 Then
                For Each item As ProfileHeaderToGroup In listProfile
                    Select Case profileType
                        Case EnumProfileType.ProfileType.CS
                            Dim objFacade As SalesmanProfileFacade = New SalesmanProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListSalesmanProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListSalesmanProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListSalesmanProfile(0), SalesmanProfile).ProfileValue
                            End If
                        Case EnumProfileType.ProfileType.SALESMAN
                            Dim objFacade As SalesmanProfileFacade = New SalesmanProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListSalesmanProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListSalesmanProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListSalesmanProfile(0), SalesmanProfile).ProfileValue
                            End If
                        Case EnumProfileType.ProfileType.CUSTOMER
                            Dim objFacade As KTB.DNet.BusinessFacade.Profile.CustomerProfileFacade = New KTB.DNet.BusinessFacade.Profile.CustomerProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(CustomerProfile), "Customer.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(CustomerProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(CustomerProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListCustoPromerfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListCustoPromerfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListCustoPromerfile(0), CustomerProfile).ProfileValue
                            End If
                        Case EnumProfileType.ProfileType.CUSTOMERREQUEST
                            Dim objFacade As CustomerRequestProfileFacade = New CustomerRequestProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequestProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "CustomerRequest.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListCustomerRequestProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListCustomerRequestProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListCustomerRequestProfile(0), CustomerRequestProfile).ProfileValue
                            End If

                        Case EnumProfileType.ProfileType.SPKCUSTOMER
                            Dim objFacade As SPKCustomerProfileFacade = New SPKCustomerProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListSPKCustomerProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListSPKCustomerProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListSPKCustomerProfile(0), SPKCustomerProfile).ProfileValue
                            End If

                        Case EnumProfileType.ProfileType.SPKPROFILE
                            Dim objFacade As SPKProfileFacade = New SPKProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetail.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomerID", MatchType.IsNull))
                            Dim objListSPKProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListSPKProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListSPKProfile(0), SPKProfile).ProfileValue
                            End If

                        Case EnumProfileType.ProfileType.DEALER
                            Dim objFacade As DealerProfileFacade = New DealerProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "Dealer.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListDealerProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListDealerProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListDealerProfile(0), DealerProfile).ProfileValue
                            End If

                        Case EnumProfileType.ProfileType.PQR
                            Dim objFacade As PQRProfileFacade = New PQRProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(PQRProfile), "PQRHeader.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListPQRProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListPQRProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListPQRProfile(0), PQRProfile).ProfileValue
                            End If
                        Case EnumProfileType.ProfileType.PQRBB
                            Dim objFacade As PQRProfileBBFacade = New PQRProfileBBFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfileBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(PQRProfileBB), "PQRHeaderBB.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(PQRProfileBB), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(PQRProfileBB), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListPQRProfileBB As ArrayList = objFacade.Retrieve(criterias)
                            If objListPQRProfileBB.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListPQRProfileBB(0), PQRProfileBB).ProfileValue
                            End If
                        Case EnumProfileType.ProfileType.CHASSISMASTER
                            Dim objFacade As ChassisMasterProfileFacade = New ChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListChassisMasterProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListChassisMasterProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListChassisMasterProfile(0), ChassisMasterProfile).ProfileValue
                            End If


                        Case EnumProfileType.ProfileType.SPKDETAILCUSTOMER
                            Dim objFacade As SPKDetailCustomerProfileFacade = New SPKDetailCustomerProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKDetailCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "SPKDetailCustomer.ID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(SPKDetailCustomerProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))

                            Dim objListSPKDetailCustomerProfile As ArrayList = objFacade.Retrieve(criterias)

                            If m_SessionName <> "" Then
                                Dim ss As New SessionHelper
                                If Not IsNothing(ss.GetSession(m_SessionName)) Then
                                    Dim obspkDetailCustomer As SPKDetailCustomer = ss.GetSession(m_SessionName)

                                    For Each dr As SPKDetailCustomerProfile In obspkDetailCustomer.SPKDetailCustomerProfiles
                                        If dr.ProfileGroup.ID = objGroup.ID AndAlso dr.ProfileHeader.ID = item.ProfileHeader.ID Then
                                            item.ProfileHeader.ProfileHeaderValue = dr.ProfileValue
                                            Exit For
                                        End If
                                    Next
                                End If
                            Else
                                objListSPKDetailCustomerProfile = objFacade.Retrieve(criterias)
                                If objListSPKDetailCustomerProfile.Count > 0 Then
                                    item.ProfileHeader.ProfileHeaderValue = CType(objListSPKDetailCustomerProfile(0), SPKDetailCustomerProfile).ProfileValue
                                End If
                            End If

                        Case EnumProfileType.ProfileType.SPKDETAILCUSTOMERPROFILE
                            Dim objFacade As SPKProfileFacade = New SPKProfileFacade(System.Threading.Thread.CurrentPrincipal)
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomerID", MatchType.Exact, objID))
                            criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
                            criterias.opAnd(New Criteria(GetType(SPKProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
                            Dim objListSPKProfile As ArrayList = objFacade.Retrieve(criterias)
                            If objListSPKProfile.Count > 0 Then
                                item.ProfileHeader.ProfileHeaderValue = CType(objListSPKProfile(0), SPKProfile).ProfileValue
                            End If
                    End Select
                    objListProfileHeader.Add(item.ProfileHeader)
                Next
            End If
        End If
        Return objListProfileHeader
    End Function

    Public Function GetSortedData(ByVal listProfileHeader As ArrayList, ByVal objGroup As ProfileGroup, ByVal User As IPrincipal) As ArrayList
        Dim strProfileHeaderID As String = ""
        For Each count As ProfileHeader In listProfileHeader
            If strProfileHeaderID = "" Then
                strProfileHeaderID = count.ID
            Else
                strProfileHeaderID = strProfileHeaderID & ";" & count.ID
            End If
        Next

        ' mengambil data ProfileHeaderToGroup ybs
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
        If strProfileHeaderID <> "" Then
            criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileHeader.ID", MatchType.InSet, CommonFunction.GetStrValue(strProfileHeaderID, ";", ",")))
        End If
        criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))

        ' mengembalikan data component yg dibutuhkan & telah disort
        Dim arrProfileHeaderToGroup As ArrayList
        Dim objProfileHeaderToGroupFacade As ProfileHeaderToGroupFacade = New ProfileHeaderToGroupFacade(User)
        Dim totalRow As Integer = 0
        Dim pageNumber As Integer = 1
        Dim pageSize As Integer = 25
        Dim strCurrentSortColumn As String = "Priority"
        Dim strCurrentSortDirect As Integer = Sort.SortDirection.ASC
        arrProfileHeaderToGroup = objProfileHeaderToGroupFacade.RetrieveByCriteria(criterias, pageNumber, pageSize, totalRow, strCurrentSortColumn, strCurrentSortDirect)

        Dim arrTmp As ArrayList = New ArrayList ' digunakan untuk menampung profile value

        ' mengeset value yg bersangkutan
        For Each item As ProfileHeaderToGroup In arrProfileHeaderToGroup
            For Each itemHeader As ProfileHeader In listProfileHeader
                If item.ProfileHeader.ID = itemHeader.ID Then
                    item.ProfileValue = itemHeader.ProfileHeaderValue
                End If
            Next
            arrTmp.Add(item)
        Next

        Return arrTmp

    End Function


    Private Function RenderPanel(ByVal objPanel As Panel, ByVal listProfileHeader As ArrayList, ByVal objGroup As ProfileGroup, ByVal User As IPrincipal) As Panel
        Return RenderPanel(objPanel, listProfileHeader, objGroup, User, True)
    End Function

    Private Function RenderPanel(ByVal objPanel As Panel, ByVal listProfileHeader As ArrayList, ByVal objGroup As ProfileGroup, ByVal User As IPrincipal, ByVal EnableClientScript As Boolean) As Panel
        Dim arrProfileHeaderToGroup As ArrayList
        arrProfileHeaderToGroup = GetSortedData(listProfileHeader, objGroup, User)
        objHst = New Hashtable
        Dim varCount As Integer
        varCount = 0
        With objPanel.Controls
            .Add(New LiteralControl("<table border=0 width=100% cellspacing=1 cellpadding=2>"))
            For Each item As ProfileHeaderToGroup In arrProfileHeaderToGroup
                varCount = varCount + 1
                .Add(New LiteralControl("<tr valign=TOP>"))
                .Add(New LiteralControl("<td width=180 class=titleField>"))
                Dim myLabel As Label = New Label
                myLabel.ID = "LABEL" & item.ProfileHeader.ID & "_" & objGroup.ID
                myLabel.Text = item.ProfileHeader.Description
                .Add(myLabel)
                .Add(New LiteralControl("</td>"))
                .Add(New LiteralControl("<td width=1% align=right>:</td><td valign=TOP align=left noWrap> "))
                Select Case item.ProfileHeader.ControlType
                    Case EnumControlType.ControlType.Text
                        Dim txtBoxVirtual As TextBox = New TextBox
                        txtBoxVirtual.ID = "TEXTBOX" & item.ID & "_" & objGroup.ID
                        If item.ProfileHeader.DataLength > 0 Then
                            txtBoxVirtual.MaxLength = item.ProfileHeader.DataLength
                        End If
                        Select Case (item.ProfileHeader.DataType)
                            Case EnumDataType.DataType.Text
                                'case alphaNumeric
                                txtBoxVirtual.Attributes("onkeypress") = "return alphaNumericExcept(event,'<>?*%$;')"
                                txtBoxVirtual.Attributes("onblur") = "omitCharsOnCompsTxt(this,'<>?*%$;');"
                            Case EnumDataType.DataType.Numeric
                                'case Numeric
                                txtBoxVirtual.Attributes("onkeypress") = "return NumericOnlyWith(event,'');"
                                txtBoxVirtual.Attributes("onblur") = "NumOnlyBlurWithOnGridTxt(this,'');"
                        End Select
                        'txtBoxVirtual.Text = item.ProfileHeader.ProfileHeaderValue
                        txtBoxVirtual.Text = item.ProfileValue
                        If item.ProfileHeader.DataLength > 15 Then
                            txtBoxVirtual.Width = New Unit(120)
                        Else
                            txtBoxVirtual.Width = New Unit(item.ProfileHeader.DataLength * 8)
                        End If
                        objHst.Add(item.ProfileHeader.ControlType.ToString & "-" & txtBoxVirtual.ID, item)
                        txtBoxVirtual.Enabled = Not IsReadOnly
                        .Add(txtBoxVirtual)
                        If item.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar And IsReadOnly = False Then
                            Dim validator As RequiredFieldValidator = New RequiredFieldValidator
                            validator.Text = " * "
                            validator.ControlToValidate = txtBoxVirtual.ID
                            validator.ErrorMessage = item.ProfileHeader.Description & " Harus diisi"
                            validator.EnableClientScript = EnableClientScript
                            .Add(validator)
                        End If

                        If Not txtBoxVirtual.Enabled Then
                            txtBoxVirtual.BorderStyle = BorderStyle.None
                            txtBoxVirtual.BorderColor = System.Drawing.Color.White

                        End If

                    Case EnumControlType.ControlType.Calendar
                        'Dim calIntiVirtual As Calendar = New Calendar
                        Dim calIntiVirtual As KTB.DNet.WebCC.IntiCalendar = New KTB.DNet.WebCC.IntiCalendar
                        calIntiVirtual.ID = "CALENDAR" & item.ID & "_" & objGroup.ID
                        objHst.Add(item.ProfileHeader.ControlType.ToString & "-" & calIntiVirtual.ID, item)
                        'If Not item.ProfileHeader.ProfileHeaderValue Is Nothing Then
                        If Not item.ProfileValue Is Nothing Then
                            'calIntiVirtual.SelectedDate = item.ProfileHeader.ProfileHeaderValue
                            'calIntiVirtual.SelectedDate = item.ProfileValue
                            calIntiVirtual.Value = item.ProfileValue
                        Else
                            calIntiVirtual.Value = Today
                        End If
                        calIntiVirtual.Enabled = Not IsReadOnly
                        .Add(calIntiVirtual)
                    Case EnumControlType.ControlType.List
                        Dim lstVirtual As DropDownList = New DropDownList
                        lstVirtual.ID = "DDLIST" & item.ID & "_" & objGroup.ID
                        Dim li As ListItem
                        If item.ProfileHeader.ProfileDetails.Count > 0 Then
                            For Each objItem As ProfileDetail In item.ProfileHeader.ProfileDetails
                                li = New ListItem(objItem.Description, objItem.Code)
                                'If Not item.ProfileHeader.ProfileHeaderValue Is Nothing Then
                                If Not item.ProfileValue Is Nothing Then
                                    'For Each val As String In item.ProfileHeader.ProfileHeaderValue.Split("-")
                                    For Each val As String In item.ProfileValue.Split("-")
                                        If objItem.Code = val Then
                                            li.Selected = True
                                        End If
                                    Next
                                End If
                                lstVirtual.Items.Add(li)
                            Next
                            Dim listSilahkanPilih As ListItem = New ListItem("Silahkan Pilih", "")
                            lstVirtual.Items.Insert(0, listSilahkanPilih)
                        End If
                        objHst.Add(item.ProfileHeader.ControlType.ToString & "-" & lstVirtual.ID, item)
                        lstVirtual.Enabled = Not IsReadOnly
                        'start add rudi
                        If item.ProfileHeader.Status = "1" Then
                            lstVirtual.Enabled = Not IsReadOnly
                        Else
                            lstVirtual.Enabled = IsReadOnly
                        End If
                        'end add rudi

                        .Add(lstVirtual)
                        If item.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar And IsReadOnly = False Then
                            Dim validator As RequiredFieldValidator = New RequiredFieldValidator
                            validator.Text = " *"
                            validator.ControlToValidate = lstVirtual.ID
                            validator.ErrorMessage = item.ProfileHeader.Description & " Harus dipilih"
                            validator.EnableClientScript = EnableClientScript
                            .Add(validator)
                        End If
                    Case EnumControlType.ControlType.CheckListBox
                        '21-Nov-2007    Deddy H     Penambahan validator untuk komponen CheckListBox
                        Dim txtBoxVirHide As TextBox = New TextBox
                        If item.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar And IsReadOnly = False Then
                            ' perlu ditambahkan textbox invisible untuk temporary temp
                            txtBoxVirHide.ID = "TEXTBOX_HIDE" & varCount
                            txtBoxVirHide.Style.Add("display", "none")
                            .Add(txtBoxVirHide)

                            ' validator untuk textbox invisible
                            Dim validator As RequiredFieldValidator = New RequiredFieldValidator
                            validator.Text = " *"
                            validator.ControlToValidate = txtBoxVirHide.ID
                            validator.ErrorMessage = item.ProfileHeader.Description & " Harus dipilih"
                            validator.EnableClientScript = EnableClientScript
                            .Add(validator)

                            '' custom validator, akses ke javascript
                            'Dim custValidator As CustomValidator = New CustomValidator
                            'custValidator.Text = " *"
                            'custValidator.ErrorMessage = item.ProfileHeader.Description & " Harus dipilih - Custom"
                            'custValidator.ClientValidationFunction = "readListControl('" & chkVirtual.ID & "','" & txtBoxVirHide.ID & "');"
                            '.Add(custValidator)
                        End If

                        Dim chkVirtual As CheckBoxList = New CheckBoxList
                        chkVirtual.ID = "CHKLISTBOX" & item.ID & "_" & objGroup.ID
                        ' function only run if mandatory type
                        If item.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar And IsReadOnly = False Then
                            chkVirtual.Attributes("onClick") = "CheckClistVal('" & chkVirtual.ID & "','" & txtBoxVirHide.ID & "');"
                        End If

                        Dim li As ListItem
                        If item.ProfileHeader.ProfileDetails.Count > 0 Then
                            For Each objItem As ProfileDetail In item.ProfileHeader.ProfileDetails
                                li = New ListItem(objItem.Description, objItem.Code)
                                'If Not item.ProfileHeader.ProfileHeaderValue Is Nothing Then
                                If Not item.ProfileValue Is Nothing Then
                                    'For Each val As String In item.ProfileHeader.ProfileHeaderValue.Split("-")
                                    For Each val As String In item.ProfileValue.Split("-")
                                        If objItem.Code = val Then
                                            li.Selected = True
                                        End If
                                    Next
                                End If

                                chkVirtual.Items.Add(li)
                            Next
                        End If
                        objHst.Add(item.ProfileHeader.ControlType.ToString & "-" & chkVirtual.ID, item)
                        chkVirtual.Enabled = Not IsReadOnly


                        .Add(chkVirtual)


                End Select
                .Add(New LiteralControl("</td>"))
                .Add(New LiteralControl("</tr>"))
            Next
            .Add(New LiteralControl("</table>"))
        End With
        HttpContext.Current.Session.Add("PROFILE" & "_" & objGroup.ID, objHst)
        Return objPanel
    End Function


    '13-Aug-2007    Deddy H     Handle component yg view saja
    Private Function RenderPanelView(ByVal objPanel As Panel, ByVal listProfileHeader As ArrayList, ByVal objGroup As ProfileGroup, ByVal User As IPrincipal) As Panel
        Dim arrProfileHeaderToGroup As ArrayList
        arrProfileHeaderToGroup = GetSortedData(listProfileHeader, objGroup, User)

        objHst = New Hashtable
        With objPanel.Controls
            .Add(New LiteralControl("<table border =0.5 width=100% cellspacing=1 cellpadding=2>"))
            For Each item As ProfileHeaderToGroup In arrProfileHeaderToGroup
                .Add(New LiteralControl("<tr valign=top>"))
                .Add(New LiteralControl("<td width=210 class=titleField>"))
                Dim myLabel As Label = New Label
                myLabel.ID = "LABEL" & item.ID & "_" & objGroup.ID
                myLabel.Text = item.ProfileHeader.Description
                .Add(myLabel)
                .Add(New LiteralControl("</td>"))
                .Add(New LiteralControl("<td width=1% align=right>:</td><td valign=top align=left noWrap> "))
                Select Case item.ProfileHeader.ControlType
                    Case EnumControlType.ControlType.Text
                        Dim txtBoxVirtual As Label = New Label
                        txtBoxVirtual.ID = "TEXTBOX" & item.ID & "_" & objGroup.ID
                        'txtBoxVirtual.Text = item.ProfileHeader.ProfileHeaderValue
                        txtBoxVirtual.Text = item.ProfileValue
                        objHst.Add(item.ProfileHeader.ControlType.ToString & "-" & txtBoxVirtual.ID, item)
                        .Add(txtBoxVirtual)
                    Case EnumControlType.ControlType.Calendar
                        Dim calIntiVirtual As Label = New Label
                        calIntiVirtual.ID = "CALENDAR" & item.ID & "_" & objGroup.ID
                        objHst.Add(item.ProfileHeader.ControlType.ToString & "-" & calIntiVirtual.ID, item)
                        'If Not item.ProfileHeader.ProfileHeaderValue Is Nothing Then
                        If Not item.ProfileValue Is Nothing Then
                            'calIntiVirtual.Text = item.ProfileHeader.ProfileHeaderValue
                            calIntiVirtual.Text = item.ProfileValue
                        Else
                            calIntiVirtual.Text = CType(Today, String)
                        End If
                        .Add(calIntiVirtual)
                    Case EnumControlType.ControlType.List
                        Dim lstVirtual As Label = New Label
                        lstVirtual.ID = "DDLIST" & item.ID & "_" & objGroup.ID
                        'lstVirtual.Text = item.ProfileHeader.ProfileHeaderValue
                        lstVirtual.Text = item.ProfileValue
                        objHst.Add(item.ProfileHeader.ControlType.ToString & "-" & lstVirtual.ID, item)
                        .Add(lstVirtual)
                    Case EnumControlType.ControlType.CheckListBox
                        Dim chkVirtual As Label = New Label
                        chkVirtual.ID = "CHKLISTBOX" & item.ID & "_" & objGroup.ID
                        'chkVirtual.Text = item.ProfileHeader.ProfileHeaderValue
                        chkVirtual.Text = item.ProfileValue
                        objHst.Add(item.ProfileHeader.ControlType.ToString & "-" & chkVirtual.ID, item)
                        .Add(chkVirtual)
                End Select
                .Add(New LiteralControl("</td>"))
                .Add(New LiteralControl("</tr>"))
            Next
            .Add(New LiteralControl("</table>"))
        End With
        HttpContext.Current.Session.Add("PROFILE" & "_" & objGroup.ID, objHst)
        Return objPanel
    End Function

#End Region




End Class
