<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmImportClaimCBUReturn.aspx.vb" Inherits=".FrmImportClaimCBUReturn" %>

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
    <script language="javascript" src="../WebResources/jquery.1.11.0.min.js"></script>
    <script language="javascript" src="../WebResources/jquery.MultiFile.min.js"></script>
    <script type="text/javascript">
        function ShowPPUploadDoc(headerID) {
            showPopUp('../PopUp/PopUpUploadDocumentCBUReturn.aspx?HeaderID=' + headerID, '', 500, 760, null);
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
                    <asp:Label ID="lblTitle" Text="Klaim Pengembalian Kendaraan - Import Claim" runat="server"></asp:Label>
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
                        <tr style="height: 50px">
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr id="rowTemplate" runat="server">
                            <td class="titleField" style="height: 16px">Template</td>
                            <td style="height: 16px">:</td>
                            <td style="height: 16px">
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download Template"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="rowUpload" runat="server">
                            <td class="titleField" style="height: 24px">Upload File</td>
                            <td style="height: 24px">:</td>
                            <td style="height: 24px">
                                <input onkeypress="return false;" id="DataFile" style="width: 216px; height: 20px" type="file" size="16" runat="server">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload"></asp:Button>
                            </td>
                        </tr>
                        <tr style="height: 50px">
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:DataGrid ID="dgCBUList" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                                    CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                                    PageSize="10" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                    <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                                    <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                                    <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                    <Columns>
                                        <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                            <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="No">
                                            <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tanggal Claim">
                                            <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTglClaim" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nomor Claim">
                                            <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoClaim" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Dealer">
                                            <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDealer" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Status Claim">
                                            <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatusClaim" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Respon Claim">
                                            <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblResponClaim" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nomor Chassis">
                                            <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoChassis" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="No DO">
                                            <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoDO" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="File Lampiran">
                                            <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileLampiran" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUploadLampiran" runat="server" CommandName="Upload" CausesValidation="False">
												            <img src="../images/dok.gif" border="0" alt="Upload"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False">
												            <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid>
                            </td>
                        </tr>
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
