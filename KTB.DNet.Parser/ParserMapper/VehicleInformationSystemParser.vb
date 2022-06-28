#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.PO

Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class DOPrintParser
        Inherits AbstractParser

#Region "Private Variables"
        Private status As String
        Private _Stream As StreamReader
        Private VehicleInformationSystem As ArrayList
        Private EndCustomers As ArrayList
        Private _fileName As String
        Private Grammar As Regex
        Private errorSb As StringBuilder

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Private Methods"


        Private Sub ParseVehicleInformationSystem(ByVal ValParser As String)
            Dim _VehicleInformationSystem As ChassisMaster = New ChassisMaster
            Dim _EndCustomer As EndCustomer = New EndCustomer
            Dim _customer As Customer = New Customer
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim isIncludeEndCustomer As Boolean = False
            Dim ArrayCity As ArrayList
            Dim ArrayProvince As ArrayList
            Dim _province As Province
            sStart = 0
            nCount = 0
            errorSb = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp = "X" Then
                            isIncludeEndCustomer = True
                        End If
                    Case Is = 1
                        If sTemp <> "" Then
                            _VehicleInformationSystem.ChassisNumber = sTemp.Trim
                        Else
                            errorSb.Append("Invalid Chasis Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        Dim objCat As Category = New CategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If objCat.ID > 0 Then
                            _VehicleInformationSystem.Category = objCat
                        Else
                            errorSb.Append("Invalid Chasis Category" & Chr(13) & Chr(10))
                        End If

                    Case Is = 3
                        Dim objVechColor As VechileColor = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByMaterialNumber(sTemp)
                        If objVechColor.ID > 0 Then
                            _VehicleInformationSystem.VechileColor = objVechColor
                        Else
                            errorSb.Append("Invalid Chasis VechileColor" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(IIf(sTemp.Trim.ToString = "009899", "MKS", sTemp.Trim))
                        If objDealer.ID > 0 Then
                            _VehicleInformationSystem.Dealer = objDealer
                            _VehicleInformationSystem.StockDealer = objDealer.ID
                        Else
                            errorSb.Append("Invalid Chasis Dealer" & Chr(13) & Chr(10))
                        End If

                    Case Is = 5
                        Dim tgl As String
                        tgl = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                        Try
                            _VehicleInformationSystem.DODate = tgl
                            _VehicleInformationSystem.StockDate = tgl
                        Catch ex As Exception
                            errorSb.Append(ex.Message.ToString & Chr(13) & Chr(10))
                        End Try
                    Case Is = 6
                        If sTemp <> "" Then
                            _VehicleInformationSystem.DONumber = sTemp.Trim
                        Else
                            errorSb.Append("DO Number Invalid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 7
                        _VehicleInformationSystem.SONumber = sTemp.Trim
                    Case Is = 8
                        _VehicleInformationSystem.PONumber = sTemp.Trim
                    Case Is = 9
                        Dim _top As TermOfPayment = New TermOfPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If _top.ID > 0 Then
                            _VehicleInformationSystem.TermOfPayment = _top
                        Else
                            errorSb.Append("Invalid Term Of Payment" & Chr(13) & Chr(10))
                        End If
                    Case Is = 10
                        Try
                            _VehicleInformationSystem.DiscountAmount = CType(sTemp.Trim, Decimal)
                        Catch ex As Exception
                            errorSb.Append(ex.Message.ToString & Chr(13) & Chr(10))
                        End Try
                    Case Is = 11
                        _VehicleInformationSystem.EngineNumber = sTemp.Trim
                    Case Is = 12
                        _VehicleInformationSystem.SerialNumber = sTemp.Trim
                    Case Is = 13
                        If isIncludeEndCustomer Then
                            _customer = New CustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                            If _customer.ID > 0 Then
                                _EndCustomer.Customer = _customer
                                _EndCustomer = InitEndCustomer(_EndCustomer)
                                _EndCustomer.MarkLoaded()
                                _VehicleInformationSystem.EndCustomer = _EndCustomer
                            End If
                        End If
                    Case Is = 14
                        If (IsNumeric(sTemp) And sTemp.Length = 4) Then
                            _VehicleInformationSystem.ProductionYear = CInt(sTemp)
                        End If
                    Case Is = 15
                        Dim objChassisMasterLocation As ChassisMasterLocation = New ChassisMasterLocation
                        objChassisMasterLocation.Location = sTemp
                        _VehicleInformationSystem.ChassisMasterLocations.Add(objChassisMasterLocation)

                    Case Is = 16
                        Dim oPODFac As New PODestinationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        Dim regioncode As String = CType(sTemp, Integer).ToString()
                        Dim oPOD As PODestination = oPODFac.Retrieve(regioncode)
                        If Not IsNothing(oPOD) AndAlso oPOD.ID > 0 AndAlso _VehicleInformationSystem.ChassisMasterLocations.Count > 0 Then
                            Dim oCML As ChassisMasterLocation = _VehicleInformationSystem.ChassisMasterLocations(0)
                            oCML.PODestination = oPOD
                            _VehicleInformationSystem.ChassisMasterLocations(0) = oCML
                        End If

                    Case Is = 17
                        If sTemp = "X" Then
                            _VehicleInformationSystem.PendingDesc = "Block_Faktur_ Unit Merupakan unit Pengganti"
                        End If

                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            If errorSb.Length > 0 Then
                Throw New Exception(errorSb.ToString)
            End If

            'If isIncludeEndCustomer Then
            '    _EndCustomer = InitEndCustomer(_EndCustomer)
            '    Dim tempChasisMaster As ChassisMaster = IsInsert(_VehicleInformationSystem)
            '    If tempChasisMaster Is Nothing Then
            '        If _EndCustomer.Customer.ID > 0 Then
            '            Dim j As Integer = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(_EndCustomer)
            '            _EndCustomer = New EndCustomer(j)
            '            _VehicleInformationSystem.EndCustomer = _EndCustomer
            '        End If
            '    Else
            '        If tempChasisMaster.RowStatus = DBRowStatus.Deleted Then
            '            If _EndCustomer.Customer.ID > 0 Then
            '                Dim j As Integer = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(_EndCustomer)
            '                _EndCustomer = New EndCustomer(j)
            '                _VehicleInformationSystem.EndCustomer = _EndCustomer
            '            End If
            '        End If
            '    End If
            'End If

            _VehicleInformationSystem.GIDate = New Date(1900, 1, 1)
            _VehicleInformationSystem.FakturStatus = "0"
            VehicleInformationSystem.Add(_VehicleInformationSystem)
        End Sub

        Private Function InitEndCustomer(ByVal _EndCustomer As EndCustomer)
            _EndCustomer.OpenFakturDate = New Date(1900, 1, 1)
            _EndCustomer.FakturDate = New Date(1900, 1, 1)
            _EndCustomer.SaveTime = New Date(1900, 1, 1)
            _EndCustomer.ValidateTime = New Date(1900, 1, 1)
            _EndCustomer.ConfirmTime = New Date(1900, 1, 1)
            _EndCustomer.DownloadTime = New Date(1900, 1, 1)
            _EndCustomer.PrintedTime = New Date(1900, 1, 1)
            _EndCustomer.CreatedTime = New Date(1900, 1, 1)
            _EndCustomer.LastUpdateTime = New Date(1900, 1, 1)
            Return _EndCustomer
        End Function

        Private Function IsExistCode(ByVal sCode As String) As Boolean
            Dim objChassis As ChassisMasterFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return objChassis.ValidateCode(sCode) > 0
        End Function

        Private Function IsInsert(ByVal objChasisMaster As ChassisMaster) As ChassisMaster
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objChasisMaster.ChassisNumber))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, 0))
            Dim objChassisMasterList As ArrayList = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objChassisMasterList.Count < 1 Then
                Return Nothing
            Else
                Return objChassisMasterList.Item(0)
            End If
        End Function

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            VehicleInformationSystem = New ArrayList
            EndCustomers = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    ParseVehicleInformationSystem(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DOPrintParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.VechicleInformationSystemParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return VehicleInformationSystem
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim _chasisMaster As ChassisMaster
            Dim objChassisMaster As ChassisMaster
            Dim objSalesOrder As SalesOrder
            Dim objDeliveryOrder As New DeliveryOrder
            Dim arrChassis As ArrayList
            Dim j As Integer
            Dim x As Integer
            Dim intResult As Integer
            Dim intResultCMLoc As Integer
            Dim ischange As New IsChangeFacade
            For Each item As ChassisMaster In VehicleInformationSystem
                Try
                    j = 0
                    _chasisMaster = IsInsert(item)
                    If _chasisMaster Is Nothing Then
                        If Not item.EndCustomer Is Nothing Then
                            If item.EndCustomer.Customer.ID > 0 Then
                                j = 0
                                j = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item.EndCustomer)
                            End If
                        End If
                        If j > 0 Then
                            item.EndCustomer.ID = j
                        End If

                        objChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByField("DONumber", item.DONumber)
                        If objChassisMaster.ID > 0 Then
                            If objChassisMaster.EndCustomer Is Nothing Then
                                objChassisMaster.IsChangedWSM = False
                                If objChassisMaster.RowStatus <> -1 Then
                                    objChassisMaster.RowStatus = -1
                                    objChassisMaster.IsChangedWSM = True
                                End If
                                If objChassisMaster.IsChangedWSM Then
                                    If ischange.IsChangeChassisMaster(objChassisMaster) Then
                                        intResult = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objChassisMaster)
                                    End If

                                Else
                                    intResult = 1
                                End If
                                intResult = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item)
                            End If
                        Else
                            intResult = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item)
                        End If

                        If item.ChassisMasterLocations.Count > 0 Then
                            Dim objCM As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(intResult)
                            If objCM.ID > 0 Then
                                Dim objCMLocation As ChassisMasterLocation
                                objCMLocation = CType(item.ChassisMasterLocations.Item(0), ChassisMasterLocation)
                                objCMLocation.ChassisMaster = objCM
                                intResultCMLoc = New ChassisMasterLocationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(objCMLocation)
                            End If
                        End If



                    Else
                        If _chasisMaster.RowStatus = DBRowStatus.Deleted Then
                            ' Dim i As Integer = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).DeleteDB(_chasisMaster)
                            If Not _chasisMaster.EndCustomer Is Nothing Then
                                If item.EndCustomer.Customer.ID > 0 Then
                                    j = 0
                                    j = New EndCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item.EndCustomer)
                                End If
                            End If
                            If j > 0 Then
                                item.EndCustomer.ID = j
                            End If
                        End If

                        objChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByField("DONumber", item.DONumber)
                        If objChassisMaster.ID > 0 Then
                            If objChassisMaster.ChassisNumber <> item.ChassisNumber Then
                                If objChassisMaster.EndCustomer Is Nothing Then
                                    objChassisMaster.IsChangedWSM = False
                                    If objChassisMaster.RowStatus <> -1 Then
                                        objChassisMaster.RowStatus = -1
                                        objChassisMaster.IsChangedWSM = True
                                    End If
                                    If objChassisMaster.IsChangedWSM Then
                                        If ischange.IsChangeChassisMaster(objChassisMaster) Then
                                            intResult = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objChassisMaster)
                                        End If

                                    Else
                                        intResult = 1
                                    End If

                                End If
                            End If
                        End If

                        If _chasisMaster.EndCustomer Is Nothing Then
                            _chasisMaster.IsChangedWSM = False
                            If _chasisMaster.DONumber <> item.DONumber Then
                                _chasisMaster.DONumber = item.DONumber
                                _chasisMaster.IsChangedWSM = True
                            End If
                            If _chasisMaster.IsChangedWSM Then
                                If ischange.IsChangeChassisMaster(objChassisMaster) Then
                                    intResult = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(_chasisMaster)
                                End If

                            Else
                                intResult = 1
                            End If
                        End If

                        If item.ChassisMasterLocations.Count > 0 Then
                            If _chasisMaster.ChassisMasterLocations.Count > 0 Then
                                Dim objCMLocation As ChassisMasterLocation
                                objCMLocation = CType(_chasisMaster.ChassisMasterLocations.Item(0), ChassisMasterLocation)
                                objCMLocation.Location = CType(item.ChassisMasterLocations.Item(0), ChassisMasterLocation).Location
                                If ischange.ISchangeChassisMasterLocation(objCMLocation) Then
                                    intResultCMLoc = New ChassisMasterLocationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objCMLocation)
                                End If

                            Else
                                Dim objCMLocation As ChassisMasterLocation
                                objCMLocation = CType(item.ChassisMasterLocations.Item(0), ChassisMasterLocation)
                                objCMLocation.ChassisMaster = _chasisMaster
                                intResultCMLoc = New ChassisMasterLocationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(objCMLocation)
                            End If
                        Else
                            If _chasisMaster.ChassisMasterLocations.Count > 0 Then
                                Dim objCMLocation As ChassisMasterLocation
                                objCMLocation = CType(_chasisMaster.ChassisMasterLocations.Item(0), ChassisMasterLocation)
                                objCMLocation.RowStatus = DBRowStatus.Deleted
                                If ischange.ISchangeChassisMasterLocation(objCMLocation) Then
                                    intResultCMLoc = New ChassisMasterLocationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objCMLocation)
                                End If

                            End If
                        End If
                    End If

                    If intResult > -1 Then
                        objDeliveryOrder = New DeliveryOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(item.DONumber)
                        objDeliveryOrder.IsChangedWSM = False
                        If objDeliveryOrder.ID = 0 Then
                            objDeliveryOrder = New DeliveryOrder
                        End If
                        If objDeliveryOrder.DONumber <> item.DONumber Then
                            objDeliveryOrder.DONumber = item.DONumber
                            objDeliveryOrder.IsChangedWSM = True
                        End If

                        If objDeliveryOrder.DODate <> item.DODate Then
                            objDeliveryOrder.DODate = item.DODate
                            objDeliveryOrder.IsChangedWSM = True
                        End If

                        If _chasisMaster Is Nothing Then
                            objChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(intResult)
                        Else
                            objChassisMaster = _chasisMaster
                            'objChassisMaster = item
                        End If

                        If objChassisMaster.ID > 0 Then
                            If objDeliveryOrder.ChassisMaster Is Nothing OrElse objDeliveryOrder.ChassisMaster.ID <> objChassisMaster.ID Then
                                objDeliveryOrder.ChassisMaster = objChassisMaster
                                objDeliveryOrder.IsChangedWSM = True
                            End If
                            objSalesOrder = New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objChassisMaster.SONumber)
                            If objSalesOrder.ID > 0 Then
                                If objDeliveryOrder.SalesOrder Is Nothing OrElse objDeliveryOrder.SalesOrder.ID <> objSalesOrder.ID Then
                                    objDeliveryOrder.SalesOrder = objSalesOrder
                                    objDeliveryOrder.IsChangedWSM = True
                                End If
                                If objDeliveryOrder.ID > 0 Then
                                    If objDeliveryOrder.IsChangedWSM Then
                                        If ischange.ISchangeDeliveryOrder(objDeliveryOrder) Then
                                            x = New DeliveryOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objDeliveryOrder)
                                        End If

                                    End If
                                Else
                                    x = New DeliveryOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(objDeliveryOrder)
                                End If

                            End If
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DOPrintParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.VechicleInformationSystemParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _chasisMaster.ChassisNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

    End Class

End Namespace