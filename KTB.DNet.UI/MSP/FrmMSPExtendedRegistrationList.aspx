<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPExtendedRegistrationList.aspx.vb" Inherits=".FrmMSPExtendedRegistrationList" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Registrasi Konsumen</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript">
        function ShowPPDealerSelection(DealerGroupID, DealerCode) {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }

        function InputPasswordPlease() {
            showPPPassword();
        }

        function showPPPassword() {
            showPopUp('../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
        }
        function GotPassword(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnTransfertoSAP");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            btn.click();
        }

        function promptPassword() {
            var txt = document.getElementById("txtPass");
            var div = document.getElementById("divPassword");

            if (txt.value)

                div.style.display = "inherit";
            alert("Please, Enter Your SAP Password First!")
            txt.focus();
        }
        function onLoad() {
            var div = document.getElementById("divPassword");
            div.style.display = "none";
        }


        function ShowConfirm(msg, id) {
            var btn = document.getElementById(id);
            var hdConfirm = document.getElementById("hdConfirm");
            if (confirm(msg)) {
                hdConfirm.value = "0";
                btn.click();
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MSP Extended - Daftar Registrasi Konsumen</td>
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
                <td valign="top" align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 14px" width="20%">
                                <asp:Label ID="lblDealer" runat="server">Dealer</asp:Label>
                            </td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="29%">
                                <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                <asp:TextBox ID="txtKodeDealer" runat="server"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                            <td width="17%" class="titleField">
                                <asp:Label ID="lblMSPType" runat="server">Tipe MSP Extended</asp:Label>
                            </td>
                            <td width="1%">:</td>
                            <td width="32%">
                                <asp:DropDownList runat="server" ID="ddlMSPType"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 14px" width="20%">
                                <asp:Label ID="Label3" runat="server">No Reg Pembayaran</asp:Label></td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="29%">
                                <asp:TextBox ID="txtRegNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 14px" width="20%">
                                <asp:Label ID="lblMSPNo" runat="server">No MSP Extended</asp:Label></td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="29%">
                                <asp:TextBox ID="txtMSPNo" runat="server"></asp:TextBox>
                            </td>
                            <td width="17%" class="titleField">
                                <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
                            </td>
                            <td width="1%">:</td>
                            <td width="32%">
                                <asp:ListBox ID="lboxStatus" runat="server" Width="140px" Rows="3" SelectionMode="Multiple"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 14px" width="20%">
                                <asp:Label ID="lblChassisNumber" runat="server">No Rangka</asp:Label>
                            </td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="29%">
                                <asp:TextBox ID="txtChassisNumber" runat="server"></asp:TextBox>
                            </td>
                            <td width="17%" class="titleField">
                                <asp:CheckBox ID="chkRequestDate" runat="server"></asp:CheckBox>
                                <asp:Label ID="Label2" runat="server">Tgl Pengajuan</asp:Label>
                            </td>
                            <td width="1%">:</td>
                            <td width="32%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="DateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="DateTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 14px" width="20%">
                                <asp:Label ID="Label1" runat="server">Kategori</asp:Label>
                                &nbsp;Kendaraan</td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="29%">

                                <asp:DropDownList runat="server" ID="ddlCategoryV" Width="161px" AutoPostBack="True"></asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlVechileModel" Width="161px" AutoPostBack="True"></asp:DropDownList>

                            </td>
                            <td width="7%" class="titleField">Tipe Kendaraan</td>
                            <td width="1%">&nbsp;</td>
                            <td width="32%">

                                <asp:DropDownList runat="server" ID="ddlVechileType" Width="161px" AutoPostBack="True"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="14%"></td>
                            <td width="1%"></td>
                            <td style="width: 262px" width="29%">
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="70px"></asp:Button>
                                &nbsp;<asp:Button ID="btnDownload" runat="server" Text="Download" Width="90px"></asp:Button>
                            </td>
                            <td class="titleField" width="14%"></td>
                            <td width="1%"></td>
                            <td style="width: 262px" width="32%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto">
                        <asp:DataGrid ID="dtgMSPRegistrationList" runat="server" Width="100%" CellSpacing="1" AllowCustomPaging="true"
                            AllowSorting="True" PageSize="10" AllowPaging="True" GridLines="Vertical" CellPadding="3"
                            BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="false">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Check">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAll('chkSelect', document.forms[0].chkAllItems.checked)"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle CssClass="titleTableService" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                    <HeaderStyle CssClass="titleTableService" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Reg Pembayaran">
                                    <HeaderStyle CssClass="titleTableService" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No MSP Extended">
                                    <HeaderStyle CssClass="titleTableService" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSPCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Rangka">
                                    <HeaderStyle CssClass="titleTableService" Width="25%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChassisNumber" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nama Kendaraan">
                                    <HeaderStyle CssClass="titleTableService" Width="25%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVehicleDescription" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tipe MSP Extended">
                                    <HeaderStyle CssClass="titleTableService" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSPType" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tgl Pengajuan">
                                    <HeaderStyle CssClass="titleTableService" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tgl Debit Memo">
                                    <HeaderStyle CssClass="titleTableService" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocumentDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No. Debit Memo">
                                    <HeaderStyle CssClass="titleTableService" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDebitMemoNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Sertifikat">
                                    <HeaderStyle CssClass="titleTableService" Width="3%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download" CausesValidation="False" CommandName="Download" Visible="false">
										    <img src="../images/download.gif" border="0" alt="Cetak Sertifikat">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle CssClass="titleTableService" Width="3%"></HeaderStyle>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <div style="width: 70px; align-content: center">
                                            <asp:LinkButton ID="lbtnView" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
										    <img src="../images/detail.gif" border="0" alt="Lihat">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
										    <img src="../images/edit.gif" border="0" alt="Ubah">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnHistory" runat="server" Text="History" CausesValidation="False" CommandName="History">
										    <img src="../images/popup.gif" border="0" alt="History">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
											<img alt="Delete" onclick="return confirm('Yakin ingin menghapus data ini?');" src="../images/trash.gif" border="0">
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <br />
                    <div id="divPassword" style="display: none;">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtUser" runat="server" Width="171px"></asp:TextBox>
                                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="171px"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table id="tblOperator" cellspacing="1" cellpadding="1" border="0" runat="server">
            <tr>
                <td>
                    <asp:Label ID="lblChangeStatus" runat="server">Mengubah Status :</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlProses" runat="server" Width="80px">
                        <asp:ListItem Value="-1" Text="Silahkan Pilih"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Validasi"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td>
                    <input id="hdConfirm" type="hidden" value="-1" runat="server">
                    <asp:Button ID="btnProses" runat="server" Text="Proses" Width="90px"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
