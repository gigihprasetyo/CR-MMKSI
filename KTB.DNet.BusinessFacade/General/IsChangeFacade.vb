

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.SAP
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region


Namespace KTB.DNet.BusinessFacade
    Public Class IsChangeFacade
        '  Inherits AbstractFacade

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ChassisMasterMapper As IMapper
        Private m_POHeaderMapper As IMapper
        Private m_PODetailMapper As IMapper
        Private m_SalesOrderMapper As IMapper
        Private m_SparePartPackingMapper As IMapper
        Private m_EndCustomerMapper As IMapper
        Private m_CustomerMapper As IMapper
        Private m_CustomerRequestMapper As IMapper
        Private m_DeliveryOrderMapper As IMapper
        Private m_SparePartDO As IMapper
        Private m_SparePartPOEstimateDetail As IMapper
        Private m_ChassisMasterLocation As IMapper
        Private m_TransactionManager As TransactionManager
        Private m_PDI As IMapper
        Private m_FreeService As IMapper
        Private m_PMHeader As IMapper
        Private m_SparePartPOEstimate As IMapper
        Private m_SparePartPO As IMapper
        Private m_SparePartBilling As IMapper
        Private m_SparePartBillingDetail As IMapper
        Private m_SparePartDOExpedition As IMapper
        Private m_SPOODMapper As IMapper
        Private m_SPOOMapper As IMapper
#End Region

        Private Sub LoadMapper()
            Me.m_SPOOMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartOutstandingOrder).ToString)
            Me.m_SPOODMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartOutstandingOrderDetail).ToString)
            Me.m_POHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(POHeader).ToString)
            Me.m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
            Me.m_SalesOrderMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesOrder).ToString)
            Me.m_ChassisMasterMapper = MapperFactory.GetInstance().GetMapper(GetType(ChassisMaster).ToString)
            Me.m_EndCustomerMapper = MapperFactory.GetInstance().GetMapper(GetType(EndCustomer).ToString)
            Me.m_CustomerMapper = MapperFactory.GetInstance().GetMapper(GetType(Customer).ToString)
            Me.m_CustomerRequestMapper = MapperFactory.GetInstance().GetMapper(GetType(CustomerRequest).ToString)
            Me.m_ChassisMasterLocation = MapperFactory.GetInstance().GetMapper(GetType(ChassisMasterLocation).ToString)
            Me.m_DeliveryOrderMapper = MapperFactory.GetInstance().GetMapper(GetType(DeliveryOrder).ToString)
            Me.m_SparePartPackingMapper = MapperFactory.GetInstance().GetMapper(GetType(SparePartPacking).ToString)
            Me.m_SparePartDO = MapperFactory.GetInstance().GetMapper(GetType(SparePartDO).ToString)
            Me.m_SparePartPOEstimateDetail = MapperFactory.GetInstance().GetMapper(GetType(SparePartPOEstimateDetail).ToString)
            Me.m_SparePartPOEstimate = MapperFactory.GetInstance().GetMapper(GetType(SparePartPOEstimate).ToString)
            Me.m_PMHeader = MapperFactory.GetInstance().GetMapper(GetType(PMHeader).ToString)
            Me.m_FreeService = MapperFactory.GetInstance().GetMapper(GetType(FreeService).ToString)
            Me.m_PDI = MapperFactory.GetInstance().GetMapper(GetType(PDI).ToString)
            Me.m_SparePartPO = MapperFactory.GetInstance().GetMapper(GetType(SparePartPO).ToString)
            Me.m_SparePartBilling = MapperFactory.GetInstance().GetMapper(GetType(SparePartBilling).ToString)
            Me.m_SparePartBillingDetail = MapperFactory.GetInstance().GetMapper(GetType(SparePartBillingDetail).ToString)
            Me.m_SparePartDOExpedition = MapperFactory.GetInstance().GetMapper(GetType(SparePartDOExpedition).ToString)
        End Sub


        Public Sub New()
            Me.m_userPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            LoadMapper()
        End Sub

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            LoadMapper()
        End Sub

#Region "Check Cnages Reqcord Wih DB"
        Public Function IsChangePOHeader(ByVal tempPoh As POHeader) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbPoH As New POHeader
                dbPoH = m_POHeaderMapper.Retrieve(tempPoh.ID)

                If dbPoH.Dealer.ID <> tempPoh.Dealer.ID OrElse
                    dbPoH.PONumber <> tempPoh.PONumber OrElse
                    dbPoH.Status <> tempPoh.Status OrElse
                    dbPoH.ContractHeader.ID <> tempPoh.ContractHeader.ID OrElse
                    dbPoH.ReqAllocationDate <> tempPoh.ReqAllocationDate OrElse
                    dbPoH.ReqAllocationMonth <> tempPoh.ReqAllocationMonth OrElse
                    dbPoH.ReqAllocationYear <> tempPoh.ReqAllocationYear OrElse
                    dbPoH.ReqAllocationDateTime <> tempPoh.ReqAllocationDateTime OrElse
                    dbPoH.EffectiveDate <> tempPoh.EffectiveDate OrElse
                    dbPoH.DealerPONumber <> tempPoh.DealerPONumber OrElse
                    dbPoH.TermOfPayment.ID <> tempPoh.TermOfPayment.ID OrElse
                    dbPoH.POType <> tempPoh.POType OrElse
                    dbPoH.ReleaseDate <> tempPoh.ReleaseDate OrElse
                    dbPoH.ReleaseMonth <> tempPoh.ReleaseMonth OrElse
                    dbPoH.ReleaseYear <> tempPoh.ReleaseYear OrElse
                    dbPoH.SONumber <> tempPoh.SONumber OrElse
                    dbPoH.FreePPh22Indicator <> tempPoh.FreePPh22Indicator OrElse
                    dbPoH.PassTOP <> tempPoh.PassTOP OrElse
                    dbPoH.LastReqAllocationDateTime <> tempPoh.LastReqAllocationDateTime OrElse
                    dbPoH.RemarkStatus <> tempPoh.RemarkStatus OrElse
                    dbPoH.DOBlockHistory <> tempPoh.DOBlockHistory OrElse
                    dbPoH.Remark <> tempPoh.Remark OrElse
                    dbPoH.ChangedTime <> tempPoh.ChangedTime OrElse
                    dbPoH.ChangedBy <> tempPoh.ChangedBy OrElse
                    dbPoH.BlockedStatus <> tempPoh.BlockedStatus OrElse
                    dbPoH.IsFactoring <> tempPoh.IsFactoring OrElse
                    dbPoH.IsTransfer <> tempPoh.IsTransfer OrElse
                    dbPoH.PODestination.ID <> tempPoh.PODestination.ID Then
                    'dbPoH.SPL.ID <> tempPoh.SPL.ID OrElse 'take out SPL Return Null
                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function SparePartOutstandingOrderDetail(ByVal tempPoh As SparePartOutstandingOrderDetail) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbSPOD As New SparePartOutstandingOrderDetail
                dbSPOD = m_SPOODMapper.Retrieve(tempPoh.ID)

                If dbSPOD.AllocationQty <> tempPoh.AllocationQty OrElse
                    dbSPOD.AllocationAmount <> tempPoh.AllocationAmount OrElse
                    dbSPOD.OpenQty <> tempPoh.OpenQty OrElse
                    dbSPOD.OpenAmount <> tempPoh.OpenAmount OrElse
                    dbSPOD.SubtitutePartNumber <> tempPoh.SubtitutePartNumber OrElse
                    dbSPOD.EstimateFillDate <> tempPoh.EstimateFillDate OrElse
                    dbSPOD.EstimateFillQty <> tempPoh.EstimateFillQty Then
                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function


        Public Function SparePartOutstandingOrder(ByVal tempPoh As SparePartOutstandingOrder) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbSPO As New SparePartOutstandingOrder
                dbSPO = m_SPOOMapper.Retrieve(tempPoh.ID)

                If dbSPO.ValidTo <> tempPoh.ValidTo Then
                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function IsChangePODetail(ByVal tempCus As PODetail) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbCHass As New PODetail
                dbCHass = m_PODetailMapper.Retrieve(tempCus.ID)

                If dbCHass.POHeaderID <> tempCus.POHeaderID OrElse
                dbCHass.LineItem <> tempCus.LineItem OrElse
                dbCHass.ContractDetailID <> tempCus.ContractDetailID OrElse
                dbCHass.ReqQty <> tempCus.ReqQty OrElse
                dbCHass.ProposeQty <> tempCus.ProposeQty OrElse
                dbCHass.AllocQty <> tempCus.AllocQty OrElse
                dbCHass.Price <> tempCus.Price OrElse
                dbCHass.Discount <> tempCus.Discount OrElse
                dbCHass.Interest <> tempCus.Interest OrElse
                dbCHass.AllocationDateTime <> tempCus.AllocationDateTime OrElse
                dbCHass.DiscountReward <> tempCus.DiscountReward OrElse
                dbCHass.AmountReward <> tempCus.AmountReward OrElse
                dbCHass.AmountRewardDepA <> tempCus.AmountRewardDepA OrElse
                dbCHass.PPh22 <> tempCus.PPh22 OrElse
                dbCHass.LogisticCost <> tempCus.LogisticCost Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function IsChangeSO(ByVal tempCus As SalesOrder) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbCHass As New SalesOrder
                dbCHass = m_SalesOrderMapper.Retrieve(tempCus.ID)

                If dbCHass.SONumber <> tempCus.SONumber OrElse
                    dbCHass.SODate <> tempCus.SODate OrElse
                    dbCHass.Amount <> tempCus.Amount OrElse
                    dbCHass.POHeader.ID <> tempCus.POHeader.ID OrElse
                    dbCHass.PaymentRef <> tempCus.PaymentRef OrElse
                    dbCHass.SOType <> tempCus.SOType OrElse
                    dbCHass.CashPayment <> tempCus.CashPayment OrElse
                    dbCHass.TotalVH <> tempCus.TotalVH OrElse
                    dbCHass.TotalPP <> tempCus.TotalPP OrElse
                    dbCHass.TotalIT <> tempCus.TotalIT OrElse
                    dbCHass.LogisticDCHeader.ID <> tempCus.LogisticDCHeader.ID Then


                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

#End Region

#Region "Check Cnages Reqcord Wih DB"
        Public Function IsChangeCustomer(ByVal tempCus As Customer) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbCus As New Customer
                dbCus = m_CustomerMapper.Retrieve(tempCus.ID)

                If dbCus.Alamat <> tempCus.Alamat OrElse
                    dbCus.Attachment <> tempCus.Attachment OrElse
                    dbCus.City.ID <> tempCus.City.ID OrElse
                    dbCus.Code <> tempCus.Code OrElse
                    dbCus.CompleteName <> tempCus.CompleteName OrElse
                    dbCus.Email <> tempCus.Email OrElse
                    dbCus.Kecamatan <> tempCus.Kecamatan OrElse
                    dbCus.Kelurahan <> tempCus.Kelurahan OrElse
                    dbCus.Name1 <> tempCus.Name1 OrElse
                    dbCus.Name2 <> tempCus.Name2 OrElse
                    dbCus.Name3 <> tempCus.Name3 OrElse
                    dbCus.PhoneNo <> tempCus.PhoneNo OrElse
                    dbCus.PostalCode <> tempCus.PostalCode OrElse
                    dbCus.PreArea <> tempCus.PreArea OrElse
                    dbCus.Status <> tempCus.Status OrElse
                    dbCus.PrintRegion <> tempCus.PrintRegion OrElse
                    dbCus.ReffCode <> tempCus.ReffCode Then
                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function IsChangeEndCustomer(ByVal tempCus As EndCustomer) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbEndCus As New EndCustomer
                dbEndCus = m_EndCustomerMapper.Retrieve(tempCus.ID)

                If dbEndCus.ProjectIndicator <> tempCus.ProjectIndicator OrElse
                    dbEndCus.RefChassisNumberID <> tempCus.RefChassisNumberID OrElse
                    dbEndCus.Customer.ID <> tempCus.Customer.ID OrElse
                    dbEndCus.Name1 <> tempCus.Name1 OrElse
                    dbEndCus.FakturDate <> tempCus.FakturDate OrElse
                    dbEndCus.OpenFakturDate <> tempCus.OpenFakturDate OrElse
                    dbEndCus.FakturNumber <> tempCus.FakturNumber OrElse
                    dbEndCus.AreaViolationFlag <> tempCus.AreaViolationFlag OrElse
                    dbEndCus.AreaViolationPaymentMethodID <> tempCus.AreaViolationPaymentMethodID OrElse
                    dbEndCus.AreaViolationyAmount <> tempCus.AreaViolationyAmount OrElse
                    dbEndCus.AreaViolationBankName <> tempCus.AreaViolationBankName OrElse
                    dbEndCus.AreaViolationGyroNumber <> tempCus.AreaViolationGyroNumber OrElse
                    dbEndCus.PenaltyFlag <> tempCus.PenaltyFlag OrElse
                    dbEndCus.PenaltyPaymentMethodID <> tempCus.PenaltyPaymentMethodID OrElse
                    dbEndCus.PenaltyAmount <> tempCus.PenaltyAmount OrElse
                    dbEndCus.PenaltyBankName <> tempCus.PenaltyBankName OrElse
                    dbEndCus.PenaltyGyroNumber <> tempCus.PenaltyGyroNumber OrElse
                    dbEndCus.ReferenceLetterFlag <> tempCus.ReferenceLetterFlag OrElse
                    dbEndCus.ReferenceLetter <> tempCus.ReferenceLetter OrElse
                    dbEndCus.SaveBy <> tempCus.SaveBy OrElse
                    dbEndCus.SaveTime <> tempCus.SaveTime OrElse
                    dbEndCus.ValidateBy <> tempCus.ValidateBy OrElse
                    dbEndCus.ValidateTime <> tempCus.ValidateTime Then


                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function IsChangeChassisMaster(ByVal tempCus As ChassisMaster) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbCHass As New ChassisMaster
                dbCHass = m_ChassisMasterMapper.Retrieve(tempCus.ID)

                If dbCHass.EndCustomerID <> tempCus.EndCustomerID OrElse
                    dbCHass.ChassisNumber <> tempCus.ChassisNumber OrElse
                    dbCHass.Category.ID <> tempCus.Category.ID OrElse
                    dbCHass.VechileColor.ID <> tempCus.VechileColor.ID OrElse
                    dbCHass.DONumber <> tempCus.DONumber OrElse
                    dbCHass.VehicleKind.ID <> tempCus.VehicleKind.ID OrElse
                    dbCHass.Dealer.ID <> tempCus.Dealer.ID OrElse
                    dbCHass.SONumber <> tempCus.SONumber OrElse
                    dbCHass.TermOfPayment.ID <> tempCus.TermOfPayment.ID OrElse
                    dbCHass.DiscountAmount <> tempCus.DiscountAmount OrElse
                    dbCHass.PONumber <> tempCus.PONumber OrElse
                    dbCHass.EngineNumber <> tempCus.EngineNumber OrElse
                    dbCHass.SerialNumber <> tempCus.SerialNumber OrElse
                    dbCHass.DODate <> tempCus.DODate OrElse
                    dbCHass.GIDate <> tempCus.GIDate OrElse
                    dbCHass.ParkingDays <> tempCus.ParkingDays OrElse
                    dbCHass.ParkingAmount <> tempCus.ParkingAmount OrElse
                    dbCHass.FakturStatus <> tempCus.FakturStatus OrElse
                    dbCHass.PendingDesc <> tempCus.PendingDesc OrElse
                    dbCHass.IsSAPDownload <> tempCus.IsSAPDownload OrElse
                    dbCHass.StockDealer <> tempCus.StockDealer OrElse
                    dbCHass.StockDate <> tempCus.StockDate OrElse
                    dbCHass.ProductionYear <> tempCus.ProductionYear OrElse
                    dbCHass.StockStatus <> tempCus.StockStatus OrElse
                    dbCHass.LastUpdateProfile <> tempCus.LastUpdateProfile OrElse
                    dbCHass.AlreadySaled <> tempCus.AlreadySaled OrElse
                    dbCHass.AlreadySaledTime <> tempCus.AlreadySaledTime Then


                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeCustomerRequest(ByVal tempCusReq As CustomerRequest) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbCustReq As New CustomerRequest
                dbCustReq = m_CustomerRequestMapper.Retrieve(tempCusReq.ID)

                If dbCustReq.RequestNo <> tempCusReq.RequestNo OrElse
                    dbCustReq.RefRequestNo <> tempCusReq.RefRequestNo OrElse
                    dbCustReq.RequestType <> tempCusReq.RequestType OrElse
                    dbCustReq.Dealer.ID <> tempCusReq.Dealer.ID OrElse
                    dbCustReq.RequestUserID <> tempCusReq.RequestUserID OrElse
                    dbCustReq.RequestDate <> tempCusReq.RequestDate OrElse
                    dbCustReq.Status <> tempCusReq.Status OrElse
                    dbCustReq.ProcessUserID <> tempCusReq.ProcessUserID OrElse
                    dbCustReq.ProcessDate <> tempCusReq.ProcessDate OrElse
                    dbCustReq.CustomerCode <> tempCusReq.CustomerCode OrElse
                    dbCustReq.ReffCode <> tempCusReq.ReffCode OrElse
                    dbCustReq.Name1 <> tempCusReq.Name1 OrElse
                    dbCustReq.Name2 <> tempCusReq.Name2 OrElse
                    dbCustReq.Name3 <> tempCusReq.Name3 OrElse
                    dbCustReq.Alamat <> tempCusReq.Alamat OrElse
                    dbCustReq.Kelurahan <> tempCusReq.Kelurahan OrElse
                    dbCustReq.Kecamatan <> tempCusReq.Kecamatan OrElse
                    dbCustReq.PostalCode <> tempCusReq.PostalCode OrElse
                    dbCustReq.PreArea <> tempCusReq.PreArea OrElse
                    dbCustReq.CityID <> tempCusReq.CityID OrElse
                    dbCustReq.PrintRegion <> tempCusReq.PrintRegion OrElse
                    dbCustReq.PhoneNo <> tempCusReq.PhoneNo OrElse
                    dbCustReq.Email <> tempCusReq.Email OrElse
                    dbCustReq.Attachment <> tempCusReq.Attachment OrElse
                    dbCustReq.Status1 <> tempCusReq.Status1 OrElse
                    dbCustReq.TipePerusahaan <> tempCusReq.TipePerusahaan OrElse
                    dbCustReq.MCPStatus <> tempCusReq.MCPStatus OrElse
                    dbCustReq.LKPPStatus <> tempCusReq.LKPPStatus Then


                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

#End Region

        Public Function ISchangeChassisMasterLocation(ByVal tempCusReq As ChassisMasterLocation) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbCustReq As New ChassisMasterLocation
                dbCustReq = m_ChassisMasterLocation.Retrieve(tempCusReq.ID)

                If dbCustReq.ChassisMaster.ID <> tempCusReq.ChassisMaster.ID OrElse
                    dbCustReq.Location <> tempCusReq.Location OrElse
                     dbCustReq.RowStatus <> tempCusReq.RowStatus OrElse
                    dbCustReq.PODestination.ID <> tempCusReq.PODestination.ID Then


                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function


        Public Function ISchangeDeliveryOrder(ByVal tempCus As DeliveryOrder) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbCHass As New DeliveryOrder
                dbCHass = m_DeliveryOrderMapper.Retrieve(tempCus.ID)

                If dbCHass.DONumber <> tempCus.DONumber OrElse
dbCHass.DODate <> tempCus.DODate OrElse
dbCHass.ChassisMaster.ID <> tempCus.ChassisMaster.ID OrElse
dbCHass.SalesOrder.ID <> tempCus.SalesOrder.ID OrElse
dbCHass.RowStatus <> tempCus.RowStatus Then



                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

#Region "Service"

        Public Function ISchangePDI(ByVal tempPDI As PDI) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbPDI As New PDI
                dbPDI = m_ChassisMasterLocation.Retrieve(tempPDI.ID)

                If dbPDI.Kind <> tempPDI.Kind OrElse
                        dbPDI.PDIDate <> tempPDI.PDIDate OrElse
                        dbPDI.PDIStatus <> tempPDI.PDIStatus OrElse
                        dbPDI.ReleaseDate <> tempPDI.ReleaseDate Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangePDI(ByVal tempPDI As PDI, ByVal dbPDI As PDI) As Boolean
            Dim isChange As Boolean = False

            Try

                If dbPDI.Kind <> tempPDI.Kind OrElse
                        dbPDI.PDIDate <> tempPDI.PDIDate OrElse
                        dbPDI.PDIStatus <> tempPDI.PDIStatus OrElse
                        dbPDI.ReleaseDate <> tempPDI.ReleaseDate Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeFreeService(ByVal tempFreeService As FreeService, ByVal dbFreeService As FreeService) As Boolean
            Dim isChange As Boolean = False

            Try

                ' blok ini diperlukan untuk menjaga apakah ada field null.. yang arti data tetap akan di input
                'If dbFreeService.FleetRequest IsNot Nothing And tempFreeService.FleetRequest IsNot Nothing Then
                '    If dbFreeService.FleetRequest.ID <> tempFreeService.FleetRequest.ID Then
                '        isChange = True
                '    Else
                '        isChange = False
                '    End If
                'Else
                '    isChange = True
                'End If

                If dbFreeService.Reason IsNot Nothing AndAlso tempFreeService.Reason Is Nothing Then
                    isChange = True
                End If

                If dbFreeService.Reason Is Nothing AndAlso tempFreeService.Reason IsNot Nothing Then
                    isChange = True
                End If

                If dbFreeService.Reason IsNot Nothing AndAlso tempFreeService.Reason IsNot Nothing Then
                    If dbFreeService.Reason.ID <> tempFreeService.Reason.ID Then
                        isChange = True
                    Else
                        isChange = False
                    End If
                End If


                If dbFreeService.ServiceDate <> tempFreeService.ServiceDate OrElse
               dbFreeService.SoldDate <> tempFreeService.SoldDate OrElse
               dbFreeService.MileAge <> tempFreeService.MileAge OrElse
               dbFreeService.NotificationNumber <> tempFreeService.NotificationNumber OrElse
               dbFreeService.NotificationType <> tempFreeService.NotificationType OrElse
               dbFreeService.Reject <> tempFreeService.Reject OrElse
               dbFreeService.LabourAmount <> tempFreeService.LabourAmount OrElse
               dbFreeService.PartAmount <> tempFreeService.PartAmount OrElse
               dbFreeService.PPNAmount <> tempFreeService.PPNAmount OrElse
               dbFreeService.PPHAmount <> tempFreeService.PPHAmount OrElse
               dbFreeService.Status <> tempFreeService.Status OrElse
                dbFreeService.ReleaseDate <> tempFreeService.ReleaseDate OrElse
               dbFreeService.CashBack <> tempFreeService.CashBack Then

                    isChange = True

                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function


        Public Function ISchangePMHeader(ByVal tempPMHeader As PMHeader) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbPMHeader As New PMHeader
                dbPMHeader = m_PMHeader.Retrieve(tempPMHeader.ID)

                If dbPMHeader.PMKind IsNot Nothing AndAlso tempPMHeader.PMKind IsNot Nothing Then
                    If dbPMHeader.PMKind.ID <> tempPMHeader.PMKind.ID Then
                        isChange = True

                    End If
                End If

                If dbPMHeader.PMKind Is Nothing AndAlso tempPMHeader.PMKind IsNot Nothing Then
                    isChange = True
                End If

                If tempPMHeader.PMKind Is Nothing AndAlso dbPMHeader.PMKind IsNot Nothing Then
                    isChange = True
                End If

                If dbPMHeader.StandKM <> tempPMHeader.StandKM OrElse
                    dbPMHeader.ServiceDate <> tempPMHeader.ServiceDate OrElse
                     dbPMHeader.Remarks <> tempPMHeader.Remarks OrElse
                      dbPMHeader.EntryType <> tempPMHeader.EntryType OrElse
                      dbPMHeader.RowStatus <> tempPMHeader.RowStatus OrElse
                      dbPMHeader.VisitType <> tempPMHeader.VisitType OrElse
                    dbPMHeader.PMStatus <> tempPMHeader.PMStatus Then
                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangePMHeader(ByVal tempPMHeader As PMHeader, ByVal dbPMHeader As PMHeader) As Boolean
            Dim isChange As Boolean = False

            Try
                If dbPMHeader.StandKM <> tempPMHeader.StandKM OrElse dbPMHeader.ServiceDate <> tempPMHeader.ServiceDate Then
                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

#End Region

#Region "Sparepart"

        Public Function ISchangeSparePartBillingDetail(ByVal itemDetail As SparePartBillingDetail, ByVal objDetail As SparePartBillingDetail) As Boolean
            Dim isChange As Boolean = False

            Try

                If objDetail.Quantity <> itemDetail.Quantity OrElse
                                    objDetail.ItemPrice <> itemDetail.ItemPrice OrElse
                                    objDetail.TotalPrice <> itemDetail.TotalPrice OrElse
                                    objDetail.Tax <> itemDetail.Tax Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try
            Return isChange
        End Function

        Public Function ISchangeSparePartBilling(ByVal spBill As SparePartBilling, ByVal spBill_DB As SparePartBilling) As Boolean
            Dim isChange As Boolean = False

            Try

                If spBill_DB.BillingDate <> spBill.BillingDate OrElse
                    spBill_DB.TotalAmount <> spBill.TotalAmount OrElse
                    spBill_DB.BillingNumber <> spBill.BillingNumber OrElse
                    spBill_DB.Tax <> spBill.Tax OrElse
                    IsNothing(spBill_DB.TermOfPayment) OrElse
                    (Not IsNothing(spBill_DB.TermOfPayment) AndAlso Not IsNothing(spBill.TermOfPayment) _
                     AndAlso spBill_DB.TermOfPayment.ID <> spBill.TermOfPayment.ID) Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try
            Return isChange
        End Function

        Public Function ISchangeSparePartPO(ByVal tempSPPO As SparePartPO, ByVal dbSPPO As SparePartPO) As Boolean
            Dim isChange As Boolean = False

            Try

                If tempSPPO.ID <> dbSPPO.ID OrElse
                            tempSPPO.PONumber <> dbSPPO.PONumber OrElse
                            tempSPPO.OrderType <> dbSPPO.OrderType OrElse
                            tempSPPO.Dealer.ID <> dbSPPO.Dealer.ID OrElse
                            tempSPPO.PODate <> dbSPPO.PODate OrElse
                            tempSPPO.DeliveryDate <> dbSPPO.DeliveryDate OrElse
                            tempSPPO.ProcessCode <> dbSPPO.ProcessCode OrElse
                            tempSPPO.CancelRequestBy <> dbSPPO.CancelRequestBy OrElse
                            tempSPPO.IndentTransfer <> dbSPPO.IndentTransfer OrElse
                            tempSPPO.PickingTicket <> dbSPPO.PickingTicket OrElse
                            tempSPPO.SentPODate <> dbSPPO.SentPODate OrElse
                            tempSPPO.IsTransfer <> dbSPPO.IsTransfer Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try
            Return isChange
        End Function


        Public Function ISchangeSparePartPOPendingOrder(ByVal tempSPPOHeader As SparePartPendingOrder, ByVal dbSPPOHeader As SparePartPendingOrder) As Boolean
            Dim isChange As Boolean = False

            Try

                If tempSPPOHeader.SONumber <> dbSPPOHeader.SONumber OrElse
                           tempSPPOHeader.SODate <> dbSPPOHeader.SODate OrElse
                           tempSPPOHeader.Amount <> dbSPPOHeader.Amount OrElse
                           tempSPPOHeader.Tax <> dbSPPOHeader.Tax OrElse
                          tempSPPOHeader.Total <> dbSPPOHeader.Total Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try
            Return isChange
        End Function

        Public Function ISchangeSparePartPOEstimate(ByVal tempSPPPOEst As SparePartPOEstimate, ByVal dbSPPPOEst As SparePartPOEstimate) As Boolean
            Dim isChange As Boolean = False

            Try

                If dbSPPPOEst.DeliveryDate <> tempSPPPOEst.DeliveryDate OrElse
                    dbSPPPOEst.SODate <> tempSPPPOEst.SODate Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function


        Public Function ISchangeDepositBDetail(ByVal tempDepBDtl As DepositBDetail, ByVal dbDepBDtl As DepositBDetail) As Boolean
            Dim isChange As Boolean = False

            Try

                If dbDepBDtl.Tipe <> tempDepBDtl.Tipe OrElse
                                            dbDepBDtl.StatusDebet <> tempDepBDtl.StatusDebet OrElse
                                            dbDepBDtl.Amount <> tempDepBDtl.Amount OrElse
                                            dbDepBDtl.Description <> tempDepBDtl.Description OrElse
                                            dbDepBDtl.Reff <> tempDepBDtl.Reff OrElse
                                            dbDepBDtl.TransactionDate <> tempDepBDtl.TransactionDate Then
                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeDepositB(ByVal tempDepB As DepositBHeader, ByVal dbDepB As DepositBHeader) As Boolean
            Dim isChange As Boolean = False

            Try

                If dbDepB.BeginingBalance <> tempDepB.BeginingBalance OrElse
                    dbDepB.EndBalance <> tempDepB.EndBalance OrElse
                    dbDepB.DebetAmount <> tempDepB.DebetAmount OrElse
                                dbDepB.CreditAmount <> tempDepB.CreditAmount Then
                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeSparePartPacking(ByVal tempSPPPa As SparePartPacking, ByVal dbSPPPa As SparePartPacking) As Boolean
            Dim isChange As Boolean = False

            Try

                If dbSPPPa.InternalHUNo <> tempSPPPa.InternalHUNo OrElse
                    dbSPPPa.PackMaterial <> tempSPPPa.PackMaterial OrElse
                    dbSPPPa.PackMaterialDesc <> tempSPPPa.PackMaterialDesc OrElse
                    dbSPPPa.LotCase <> tempSPPPa.LotCase OrElse
                    dbSPPPa.Weight <> tempSPPPa.Weight OrElse
                    dbSPPPa.Volume <> tempSPPPa.Volume OrElse
                    dbSPPPa.TotalItem <> tempSPPPa.TotalItem OrElse
                    dbSPPPa.TotalQty <> tempSPPPa.TotalQty Then

                    isChange = True
                ElseIf Not IsNothing(tempSPPPa.SparePartDOExpedition) Then
                    If Not IsNothing(dbSPPPa.SparePartDOExpedition) Then
                        If dbSPPPa.SparePartDOExpedition.ID <> tempSPPPa.SparePartDOExpedition.ID Then
                            isChange = True
                        End If
                    Else
                        isChange = True
                    End If
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeSparePartPacking(ByVal tempSPPPa As SparePartPacking) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbSPPPa As New SparePartPacking
                dbSPPPa = m_ChassisMasterLocation.Retrieve(tempSPPPa.ID)

                If dbSPPPa.InternalHUNo <> tempSPPPa.InternalHUNo OrElse
                                                                dbSPPPa.PackMaterial <> tempSPPPa.PackMaterial OrElse
                                                                dbSPPPa.PackMaterialDesc <> tempSPPPa.PackMaterialDesc OrElse
                                                                 dbSPPPa.LotCase <> tempSPPPa.LotCase OrElse
                                                                dbSPPPa.Weight <> tempSPPPa.Weight OrElse
                                                                dbSPPPa.Volume <> tempSPPPa.Volume OrElse
                                                                dbSPPPa.TotalItem <> tempSPPPa.TotalItem OrElse
                                                                dbSPPPa.TotalQty <> tempSPPPa.TotalQty Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeSparePartDOExpedition(ByVal itemDetail As SparePartDOExpedition, ByVal oSPDOEx As SparePartDOExpedition) As Boolean
            Dim isChange As Boolean = False

            Try

                If itemDetail.ExpeditionName <> oSPDOEx.ExpeditionName OrElse
                   itemDetail.ExpeditionNo <> oSPDOEx.ExpeditionNo OrElse
                   itemDetail.ATD <> oSPDOEx.ATD Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeSparePartDODetail(ByVal itemDetail As SparePartDODetail, ByVal oSPDODetDB As SparePartDODetail) As Boolean
            Dim isChange As Boolean = False

            Try

                If oSPDODetDB.Qty <> itemDetail.Qty OrElse
                                    oSPDODetDB.ItemNoDO <> itemDetail.ItemNoDO OrElse
                                    oSPDODetDB.ItemNoSO <> itemDetail.ItemNoSO Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeSparePartDO(ByVal tempSPPDO As SparePartDO, ByVal dbSPPDO As SparePartDO) As Boolean
            Dim isChange As Boolean = False

            Try

                If dbSPPDO.DoDate <> tempSPPDO.DoDate OrElse
                    dbSPPDO.EstmationDeliveryDate <> tempSPPDO.EstmationDeliveryDate OrElse
                    dbSPPDO.PickingDate <> tempSPPDO.PickingDate OrElse
                    dbSPPDO.PackingDate <> tempSPPDO.PackingDate OrElse
                    dbSPPDO.GoodIssueDate <> tempSPPDO.GoodIssueDate OrElse
                    dbSPPDO.PaymentDate <> tempSPPDO.PaymentDate OrElse
                    dbSPPDO.ReadyForDeliveryDate <> tempSPPDO.ReadyForDeliveryDate Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeSparePartDO(ByVal tempSPPDO As SparePartDO) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbSPPDO As New SparePartDO
                dbSPPDO = m_ChassisMasterLocation.Retrieve(tempSPPDO.ID)

                If dbSPPDO.DoDate <> tempSPPDO.DoDate OrElse
                    dbSPPDO.EstmationDeliveryDate <> tempSPPDO.EstmationDeliveryDate OrElse
                    dbSPPDO.PickingDate <> tempSPPDO.PickingDate OrElse
                    dbSPPDO.PackingDate <> tempSPPDO.PackingDate OrElse
                    dbSPPDO.GoodIssueDate <> tempSPPDO.GoodIssueDate OrElse
                    dbSPPDO.PaymentDate <> tempSPPDO.PaymentDate OrElse
                    dbSPPDO.ReadyForDeliveryDate <> tempSPPDO.ReadyForDeliveryDate Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeSparePartPOEstimateDetail(ByVal tempSPPPOEst As SparePartPOEstimateDetail, ByVal dbSPPPOEst As SparePartPOEstimateDetail) As Boolean
            Dim isChange As Boolean = False

            Try

                If dbSPPPOEst.OrderQty <> tempSPPPOEst.OrderQty OrElse
                    dbSPPPOEst.AllocQty <> tempSPPPOEst.AllocQty OrElse
                    dbSPPPOEst.AllocationQty <> tempSPPPOEst.AllocationQty OrElse
                                    dbSPPPOEst.OpenQty <> tempSPPPOEst.OpenQty OrElse
                                    dbSPPPOEst.RetailPrice <> tempSPPPOEst.RetailPrice OrElse
                                    dbSPPPOEst.AltPartNumber <> tempSPPPOEst.AltPartNumber OrElse
                                    dbSPPPOEst.Discount <> tempSPPPOEst.Discount Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function

        Public Function ISchangeSparePartPOEstimateDetail(ByVal tempSPPPOEst As SparePartPOEstimateDetail) As Boolean
            Dim isChange As Boolean = False

            Try
                Dim dbSPPPOEst As New SparePartPOEstimateDetail
                dbSPPPOEst = m_ChassisMasterLocation.Retrieve(tempSPPPOEst.ID)

                If dbSPPPOEst.OrderQty <> tempSPPPOEst.OrderQty OrElse
                    dbSPPPOEst.AllocQty <> tempSPPPOEst.AllocQty OrElse
                    dbSPPPOEst.AllocationQty <> tempSPPPOEst.AllocationQty OrElse
                                    dbSPPPOEst.OpenQty <> tempSPPPOEst.OpenQty OrElse
                                    dbSPPPOEst.RetailPrice <> tempSPPPOEst.RetailPrice OrElse
                                    dbSPPPOEst.AltPartNumber <> tempSPPPOEst.AltPartNumber OrElse
                                    dbSPPPOEst.Discount <> tempSPPPOEst.Discount OrElse
                                    dbSPPPOEst.CreatedBy <> tempSPPPOEst.CreatedBy OrElse
                                    dbSPPPOEst.ItemStatus <> tempSPPPOEst.ItemStatus Then

                    isChange = True
                End If
            Catch ex As Exception
                Return True
            End Try


            Return isChange
        End Function



#End Region
    End Class

End Namespace