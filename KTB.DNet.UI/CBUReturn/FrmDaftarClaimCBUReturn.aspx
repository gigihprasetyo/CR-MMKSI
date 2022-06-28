<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarClaimCBUReturn.aspx.vb" Inherits=".FrmDaftarClaimCBUReturn" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmBabitList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpDealer1() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 430, 800, DealerSSS1);
        }

        function DealerSSS1(selectedRefNumber) {
            var hdnTemporaryOutlet = document.getElementById("hdnDealerClaim");
            hdnTemporaryOutlet.value = selectedRefNumber;

            if (navigator.appName == "Microsoft Internet Explorer") {
                hdnTemporaryOutlet.blur();
            }
            else {
                hdnTemporaryOutlet.onchange();
            }
        }
        function ShowPopUpDealer2() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 430, 800, DealerSSS2);
        }

        function DealerSSS2(selectedRefNumber) {
            var hdnTemporaryOutlet = document.getElementById("hdnDealerAlokasi");
            hdnTemporaryOutlet.value = selectedRefNumber;

            if (navigator.appName == "Microsoft Internet Explorer") {
                hdnTemporaryOutlet.blur();
            }
            else {
                hdnTemporaryOutlet.onchange();
            }
        }

        function showPPPassword() {
            showPopUp('../General/../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
        }

        function showPPPassword2() {
            showPopUp('../General/../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword2);
        }

        function GotPassword(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnProcessReturn");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            //btn.click();
            __doPostBack("Registration", "Page_Load");
        }

        function GotPassword2(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnProcess");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            btn.click();
            //__doPostBack("Registration", "Page_Load");
        }
    </script>
</head>
<body ms_positioning="GridLayout" onfocus="return checkModal()" onclick="checkModal()">
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Klaim Pengembalian Kendaraan -&nbsp; Daftar Claim</td>
                <INPUT id="hdnValActive" type="hidden" value="-1" name="hdnValActive" runat="server">
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
                <td align="left">
                    <table id="Table5" width="100%" border="0">
                        <tr>
                            <td style="width: 100%" valign="top">
                                <table id="Table2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField">Nomor Claim</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoClaim"
                                                runat="server" MaxLength="50" Width="150px"></asp:TextBox></td>
                                        <td></td>
                                        <td class="titleField" width="30%" style="height: 18px">Status Claim</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatusClaim" runat="server"></asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Dealer Claim</td>
                                        <td>:</td>
                                        <td>
                                            <asp:RadioButton ID="rdoDealerClaim" GroupName="filterDealer" runat="server" />
                                            <asp:HiddenField ID="hdnDealerClaim" runat="server" />
                                            <asp:TextBox ID="txtDealerName" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                            <asp:LinkButton ID="lnkBtnPopUpDealer1" runat="server" Width="16px">
                                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:LinkButton>
                                        </td>
                                        <td></td>
                                        <td class="titleField">Nomor Chassis</td>
                                        <td>:</td>
                                        <td>
                                            <asp:HiddenField ID="hdnNoChassis" runat="server" />
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoChassis"
                                                runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnNoChassis" runat="server" Width="16px" Visible="false">
                                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:LinkButton>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="13%">Dealer Alokasi</td>
                                        <td width="1%">:</td>
                                        <td width="34%">
                                            <asp:RadioButton ID="rdoDealerAlokasi" GroupName="filterDealer" runat="server" />
                                            <asp:HiddenField ID="hdnDealerAlokasi" runat="server" />
                                            <asp:TextBox ID="txtDealerAlokasi" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                            <asp:LinkButton ID="lnkBtnPopUpDealer2" runat="server" Width="16px" >
                                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:LinkButton>
                                        </td>
                                        <td></td>
                                        <td class="titleField">Respons Claim</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlResponClaim" runat="server"></asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="30%" style="height: 18px">Periode Claim</td>
                                        <td>:</td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tr valign="top">
                                                    <td>
                                                        <asp:CheckBox ID="cbDate" runat="server" Width="2px"></asp:CheckBox>
                                                    </td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                    <td valign="Top">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td></td>
                                        <td class="titleField">Status Proses Retur</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatusProsesReturn" runat="server"></asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
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
                            <asp:TemplateColumn>
                                <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <HeaderTemplate>
                                    <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
                                        type="checkbox">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status Claim">
                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusClaim" runat="server"></asp:Label>
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
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealer" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nomor Chassis">
                                <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNoChassis" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Respon Claim">
                                <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblResponClaim" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Chassis Pengganti">
                                <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblChassisPengganti" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Model">
                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblModel" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status Proses Return">
                                <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusProsesReturn" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False">
												            <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnStatusClaim" runat="server" CommandName="StatusClaim" CausesValidation="False">
												            <img src="../images/popup.gif" border="0" alt="Status Claim"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnStatusReturn" runat="server" CommandName="StatusReturn" CausesValidation="False">
												            <img src="../images/alur_flow2.gif" border="0" alt="Status Return"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    <asp:TextBox ID="txtUser" runat="server" Width="171px"></asp:TextBox>
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="171px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlKonfirmasi" runat="server"></asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnProcess" runat="server" Width="60px" Text=" Proses "></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnProcessReturn" runat="server" Width="100px" Text=" Proses Retur "></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnDownload" runat="server" Width="80px" Text=" Download "></asp:Button>
                    &nbsp;
                    <asp:Button ID="ReTransfer" runat="server" Width="100px" Text=" Transfer Ulang "></asp:Button>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
    </form>
</body>
</html>
