Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Globalization
Imports System.Text
Public Class PopUpMSPCertificate
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sortCols As SortCollection
    Private objUserInfo As UserInfo
    Private objLoginDealer As Dealer
    Private objMSPRegistration As MSPRegistration
    Private objMSPRegistrationHistory As MSPRegistrationHistory

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim _MSPRegHistoryID As String = Request.QueryString("MSPRegistrationHistoryID")
            If Not String.IsNullOrEmpty(_MSPRegHistoryID) Then
                objUserInfo = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                objLoginDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)

                objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(_MSPRegHistoryID))
                If Not IsNothing(objMSPRegistrationHistory) Then
                    lblVehicleType.Text = objMSPRegistrationHistory.MSPRegistration.ChassisMaster.VechileColor.VechileType.Description
                    lblChassisNumber.Text = objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber
                    lblEngineNumber.Text = objMSPRegistrationHistory.MSPRegistration.ChassisMaster.EngineNumber
                    lblDealerCode.Text = objMSPRegistrationHistory.MSPRegistration.Dealer.DealerCode
                    lblCityDealer.Text = objMSPRegistrationHistory.MSPRegistration.Dealer.City.CityName
                    lblRequestDate.Text = objMSPRegistrationHistory.RegistrationDate.ToString("dd/MM/yyyy")
                    lblPKTDate.Text = objMSPRegistrationHistory.MSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")

                    'If Not IsNothing(objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisMasterPKT) Then
                    '    lblPKTDate.Text = objMSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisMasterPKT.PKTDate '  
                    'End If


                    lblDuration.Text = objMSPRegistrationHistory.MSPMaster.Duration
                    lblMSPKM.Text = String.Format("{0:#,##0}", Convert.ToDouble(objMSPRegistrationHistory.MSPMaster.MSPKm))
                    lblMSPtypeDescription.Text = objMSPRegistrationHistory.MSPMaster.MSPType.Description

                    If objLoginDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                        If objMSPRegistrationHistory.IsDownloadCertificate = True Then
                            tblMain.Attributes.Add("Background", "../images/Copy.png")
                            tblMain.Attributes.Add("style", "background-repeat: no-repeat;background-position: center;")
                        Else
                            objMSPRegistrationHistory.PrintedDate = Now.ToString("yyyy-MM-dd hh:mm:ss")
                            objMSPRegistrationHistory.PrintedBy = objUserInfo.UserName
                            objMSPRegistrationHistory.IsDownloadCertificate = True
                            Dim int As Integer = New MSPRegistrationHistoryFacade(User).Update(objMSPRegistrationHistory)
                        End If
                    End If
                    
                End If
            End If
        End If
    End Sub

End Class