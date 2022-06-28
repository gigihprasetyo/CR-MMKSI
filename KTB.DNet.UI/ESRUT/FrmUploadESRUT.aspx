<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmUploadESRUT.aspx.vb" Inherits=".FrmUploadESRUT" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCourse</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">

        function ShowPPErrorExcel() {
            showPopUp('../General/../PopUp/PopUpErrorexcel.aspx', '', 500, 760, null);
        }

        function DisableProcessButton()
        {
            var btnProcess = document.getElementById("btnProcess");
            var btnReupload = document.getElementById("btnReupload");

            btnProcess.value = "Harap tunggu..";
            btnReupload.value = "Harap tunggu..";

            btnProcess.disabled = true;
            btnReupload.disabled = true;
        }

    </script>

    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" Text="Upload E-SRUT" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td valign="top">
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <%--<tr>
                            <td class="titleField" width="24%">Tipe Upload</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblDisplayName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rowTemplate" runat="server">
                            <td class="titleField" style="height: 16px">Template</td>
                            <td style="height: 16px">:</td>
                            <td style="height: 16px">
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download Template"></asp:LinkButton>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="titleField" style="height: 24px">Upload excel VTA Online</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <input onkeypress="return false;" id="uplExcel" style="width: 300px; height: 20px" type="file" accept="application/vnd.ms-excel"
                                    size="46" name="fileUpload" runat="server">
                                * File Excel dengan ukuran maksimal 10Mb
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px">Upload pdf VTA Online</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <input onkeypress="return false;" id="uplPdf" style="width: 300px; height: 20px" type="file"
                                    size="46" name="fileUpload" runat="server" accept="application/pdf">
                                * File Pdf dengan ukuran maksimal 10Mb
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 24px"></td>
                            <td style="height: 24px"></td>
                            <td>
                                <asp:Button ID="btnProcess" runat="server" Text="Proses" Visible="true" Width="100px" OnClientClick ="DisableProcessButton();" UseSubmitBehavior="false" />
                                &nbsp;
                                 <asp:Button ID="btnReupload" runat="server" Text="Reupload" Visible="true" Width="100px" OnClientClick="DisableProcessButton();" UseSubmitBehavior="false" />
                            </td>

                        </tr>


                        <asp:Button ID="btnShowPopup" runat="server" CssClass="hidden" OnClientClick="ShowPPErrorExcel()" CausesValidation="false" />

                        <%--  <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <p>
                                    <asp:Button ID="btnUpload" runat="server" Width="60px" Text="Upload"></asp:Button>&nbsp;
										<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>

                                </p>
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>

        </table>
    </form>
    <script type="text/javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }

        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    </script>
</body>
</html>
