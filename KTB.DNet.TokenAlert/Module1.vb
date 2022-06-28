Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain
Imports KTB.DNet.Lib

Module Module1

    Sub Main()

       
        Dim _listTokenExpired As New ArrayList
        Dim _listTokenExpired2 As New ArrayList
        Dim _stringMessage As String = WebConfig.GetValue("StringMessageSMS")

        Dim _dayRemind As Integer = CType(WebConfig.GetValue("DayRemind1"), Integer)
        Dim _dayRemind2 As Integer = CType(WebConfig.GetValue("DayRemind2"), Integer)


        'Begin Run
        Console.WriteLine("-------------------------")
        Console.WriteLine("Begin Transmit Reminder")
        Console.WriteLine("-------------------------")

        Dim _controller As KTB.DNet.TokenAlert.TokenController = New KTB.DNet.TokenAlert.TokenController

        _controller.CreateLogFile()

        Try

        
        _listTokenExpired = _controller.GetListPhone(0, _dayRemind, "")
            _controller.SendAlertToAll(_listTokenExpired, _stringMessage)

        _listTokenExpired2 = _controller.GetListPhone((_dayRemind + 1), _dayRemind2, "")
            _controller.SendAlertToAll(_listTokenExpired2, _stringMessage)

        Catch ex As Exception
            KTB.DNet.TokenAlert.Utility.SaveTextToFile(ex.Message.ToString() & "_" & DateTime.Now.ToString(), "Error.log", True, "")
        End Try
        Console.WriteLine("User Expired In 14 Days : " + _listTokenExpired.Count.ToString())
        Console.WriteLine("User Expired In 2 Days : " + _listTokenExpired2.Count.ToString())

        Console.WriteLine("-------------------------")
        Console.WriteLine("Ending Transmit Reminder")
        Console.WriteLine("-------------------------")
        'Ending(Run)

        'Console.WriteLine(_listTokenExpired.Count)
        'Console.ReadLine()

    

    End Sub



End Module

#Region "TestEmail"
'Dim _controller As KTB.DNet.TokenAlert.TokenController = New KTB.DNet.TokenAlert.TokenController
'Dim _objectToken As v_GetTokenExpired = New v_GetTokenExpired
'        _objectToken.Email = "agus-m@bsi.co.id"
'        _objectToken.ID = 123
'        _objectToken.Name = "macan" '0

'        _objectToken.DealerCode = "XXX001" '4
'        _objectToken.ActivationCode = "YYY-ACT-ZZZ" '3
'        _objectToken.CreatedBy = "Tutul" '5
'        _objectToken.DateRemind = DateTime.Now '1
'        _objectToken.Handphone = "0892132142" '2

'        _controller.SendEmail(_objectToken, "JUST TEST SEND EMAIL")
'        Console.ReadLine()

#End Region


