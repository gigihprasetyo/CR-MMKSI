<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmChecklistMaintenance.aspx.vb" Inherits="FrmChecklistMaintenance" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function DealerSelection(selectedDealer) {
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            txtDealerCodeSelection.value = selectedDealer;

        }

    </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 17px;
        }

        .auto-style2 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 109px;
            height: 17px;
        }

        .auto-style3 {
            height: 17px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">SYSTEM&nbsp;MANAGEMENT - Check List 
						Maintenance</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlMain" runat="server">
                        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td style="width: 109px; height: 17px" width="109"></td>
                                <td style="height: 17px" width="25%"></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%">SMS</td>
                                <td class="titleField" style="width: 109px; height: 17px" width="109">To</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtSMSNumber" runat="server" Width="224px"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td class="titleField" style="width: 109px; height: 17px" valign="top" width="109">Message</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtSMSMessage" runat="server" Width="224px" TextMode="MultiLine" Height="48px"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td style="width: 109px; height: 17px" width="109"></td>
                                <td style="height: 17px" align="right" width="25%">
                                    <asp:Button ID="btnSMSSend" runat="server" Width="72px" Text="Send"></asp:Button>&nbsp;</td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%" colspan="6">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%">Email</td>
                                <td class="titleField" style="width: 109px; height: 17px" width="109">To</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtEmailAddress" runat="server" Width="224px"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td class="titleField" style="width: 109px; height: 17px" valign="top" width="109">Message</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtEmailMessage" runat="server" Width="224px" TextMode="MultiLine" Height="48px"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td style="width: 109px; height: 17px" width="109"></td>
                                <td style="height: 17px" align="right" width="25%">
                                    <asp:Button ID="btnEmailSend" runat="server" Width="72px" Text="Send"></asp:Button>&nbsp;</td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%" colspan="6">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%">Repository</td>
                                <td class="titleField" style="width: 109px; height: 17px" width="109">Server Path</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtServerPath" runat="server" Width="224px"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="auto-style1" width="15%"></td>
                                <td class="auto-style2" valign="top" width="109">File 
										Name</td>
                                <td width="25%" class="auto-style3">
                                    <asp:TextBox ID="txtFileName" runat="server" Width="224px"></asp:TextBox></td>
                                <td class="auto-style1" width="20%"></td>
                                <td width="1%" class="auto-style3"></td>
                                <td width="29%" class="auto-style3"></td>
                            </tr>

                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td style="width: 109px; height: 17px" width="109"></td>
                                <td style="height: 17px" align="right" width="25%">
                                    <asp:Button ID="btnDownload" runat="server" Width="72px" Text="Download"></asp:Button>&nbsp;</td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <!-- ali-->
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%" colspan="6">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%">Upload To Folder SAP</td>
                                <td class="titleField" style="height: 17px; width: 109px" width="109">Server Path</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtSAN" runat="server" Width="224px"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td class="titleField" style="height: 17px; width: 109px" width="109">File To 
										Upload</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtFileSap" runat="server" Width="224px"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td style="height: 17px; width: 109px" width="109"></td>
                                <td style="height: 17px" width="25%" align="right">
                                    <asp:Button ID="txtUpload" runat="server" Width="88px" Text="Upload To SAP"></asp:Button>&nbsp;</td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%" colspan="6">
                                    <hr>
                                </td>
                            </tr>
                            <!-- donwload Log-->
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%">Download Log WSM</td>
                                <td class="titleField" style="height: 17px; width: 109px" width="109">Server Path</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtLog" runat="server" Width="224px"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td class="titleField" style="height: 17px; width: 109px" width="109">File To 
										Download</td>
                                <td style="height: 17px" width="25%">
                                    <asp:TextBox ID="txtLogName" runat="server" Width="224px" ReadOnly="True"></asp:TextBox></td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%"></td>
                                <td style="height: 17px; width: 109px" width="109"></td>
                                <td style="height: 17px" width="25%" align="right">
                                    <asp:Button ID="btnDownloadLog" runat="server" Width="88px" Text="Download Log"></asp:Button>&nbsp;</td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>
                            <!-- End ali-->

                            <tr>
                                <td class="titleField" style="height: 17px" width="15%" colspan="6">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 17px" width="15%">Utility</td>
                                <td class="titleField" style="width: 109px; height: 17px" width="109"></td>
                                <td style="height: 17px" width="25%">
                                    <ul>
                                        <li>
                                            <asp:HyperLink NavigateUrl="../UserManagement/FrmUserActivationCode.aspx" ID="hlActivationCode" runat="server" Target="_self">Daftar Kode Aktifasi User</asp:HyperLink>
                                        </li>
                                        <li>
                                            <asp:HyperLink NavigateUrl="../General/FrmDealerSystems.aspx" ID="hlDealerSystems" runat="server" Target="_self">Daftar Dealer Systems</asp:HyperLink>
                                        </li>
                                        <li>
                                            <asp:HyperLink ID="hlAppConfig" runat="server" NavigateUrl="../General/FrmAppConfig.aspx" Target="_self">Pengaturan App Key</asp:HyperLink></li>
                                    </ul>

                                </td>
                                <td class="titleField" style="height: 17px" width="20%"></td>
                                <td style="height: 17px" width="1%"></td>
                                <td style="height: 17px" width="29%"></td>
                            </tr>

                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
    </form>
</body>
</html>
