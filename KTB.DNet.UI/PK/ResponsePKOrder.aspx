<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ResponsePKOrder.aspx.vb" Inherits="ResponsePKOrder" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ResponsePKOrder</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        var strDealaerBranch = '../PK/../PopUp/PopUpDealerBranchMultipleSelection.aspx';

        function ShowPPDealerBranchSelection() {

            var hdnTitle = document.getElementById('hdnTitle');

            var uri = strDealaerBranch;

            if (hdnTitle.value == "MKS" || 1==1) {

                var txtDealerSelection = document.getElementById("txtDealerCode");
                if (txtDealerSelection.value != '') {
                    uri = uri + "?DealerCode=" + txtDealerSelection.value;
                }

            }

            showPopUp(uri, '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealer) {

            var txtDealerSelection = document.getElementById("txtDealerBranchCode");
            txtDealerSelection.value = selectedDealer;

        }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtDealerCode");
            txtDealerSelection.value = selectedDealer;
        }

        function InputPasswordPlease() {
            //alert("Silahkan Masukkan Password SAP Anda")
            showPPPassword();
        }

        function InputPasswordTransferUlang() {
            showPPPasswordTranserUlang()
        }

        function showPPPassword() {
            showPopUp('../General/../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
        }

        function showPPPasswordTranserUlang() {
            showPopUp('../General/../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPasswordTransferUlang);
        }

        function GotPassword(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnTransferData");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            btn.click();
        }


        function GotPasswordTransferUlang(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnTransferUlang");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            btn.click();
        }

    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PESANAN KENDARAAN - Respon Pesanan Kendaraan</td>
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
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="titlefield">
                                    <asp:Label ID="Label15" runat="server">Kategori</asp:Label></td>
                                <td>
                                    <asp:Label ID="Label19" runat="server">:</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="56px" AutoPostBack="True"></asp:DropDownList>
                                    <asp:DropDownList Style="z-index: 0" ID="ddlSubCategory" runat="server"></asp:DropDownList></td>                                <td class="titleField" width="22%">
                                    <asp:Label ID="Label5" runat="server">Rencana Penebusan</asp:Label></td>
                                <td width="1%">
                                    <asp:Label ID="Label21" runat="server">:</asp:Label></td>
                                <td width="24%">
                                    <asp:DropDownList ID="ddlRencanaPenebusan" runat="server" Width="140"></asp:DropDownList></td>
                                <td width="6%"></td>
                            </tr>
                            <tr>
                                <td class="titlefield">
                                    <asp:Label ID="Label4" runat="server">Jenis Pesanan</asp:Label></td>
                                <td>
                                    <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlOrderType" runat="server" Width="140"></asp:DropDownList></td>
                                <td class="titlefield" width="22%">
                                    <asp:Label ID="Label3" runat="server">Kode Dealer</asp:Label></td>
                                <td width="1%">
                                    <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                                <td width="24%">
                                    <input style="display: none" type="text" name="fakeusernameremembered" />
<input style="display: none" type="password" name="fakepasswordremembered" />
                                    <asp:TextBox ID="txtDealerCode" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                                <td></td>
                            </tr>
                            <tr valign="top">
                                <td class="titlefield" style="height: 18px">
                                    <asp:Label ID="Label16" runat="server">Kondisi Pesanan</asp:Label></td>
                                <td style="height: 18px">
                                    <asp:Label ID="Label20" runat="server">:</asp:Label></td>
                                <td style="height: 18px">
                                    <asp:DropDownList ID="ddlPurpose" runat="server" Width="140"></asp:DropDownList></td>
                                <td class="titlefield">
                                   Cabang Dealer</td>
                                <td>
                                    <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                                <td>
                                    <asp:textbox id="txtDealerBranchCode"   Runat="server"></asp:textbox>
									<asp:label id="lblPopUpDealerBranch" runat="server" width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label><asp:HiddenField runat="server" ID="hdnTitle" Value="DEALER" /> </td>
                                <td style="height: 18px"></td>
                            </tr>
                            <tr valign="top">
                                <td class="titlefield" style="height: 18px">
                                    <asp:Label ID="Label6" runat="server">Status</asp:Label></td>
                                <td style="height: 18px">
                                     : </td>
                                <td style="height: 18px">
                                    
                                    <asp:ListBox ID="lboxStatus" runat="server" Width="136px" Rows="3" SelectionMode="Multiple">
                                        <asp:ListItem Value="0">Baru</asp:ListItem>
                                        <asp:ListItem Value="2">Validasi</asp:ListItem>
                                        <asp:ListItem Value="3">Konfirmasi</asp:ListItem>
                                        <asp:ListItem Value="10">Tunggu Diskon</asp:ListItem>
                                        <asp:ListItem Value="4">Rilis</asp:ListItem>
                                        <asp:ListItem Value="5">Ditolak</asp:ListItem>
                                        <asp:ListItem Value="8">Blok</asp:ListItem>
                                        <asp:ListItem Value="6">Setuju</asp:ListItem>
                                    </asp:ListBox></td>

                                <td class="titlefield" style="height: 18px">
                                    <asp:Label ID="Label14" runat="server">No Reg PK</asp:Label></td>
                                <td style="height: 18px">
                                    <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                                <td style="height: 18px">
                                    <asp:TextBox onkeypress="return alphaNumericPlusSpaceUniv(event)" ID="txtPKNumber" onblur="alphaNumericPlusSpaceBlur(txtPKNumber)"
                                        runat="server" MaxLength="20" size="22"></asp:TextBox></td>
                                <td valign="bottom"></td>
                            </tr>

                              <tr valign="top">
                                <td></td>
                                <td></td>
                                <td valign="top"><asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button>&nbsp
                                    <asp:Button ID="btnDownload" runat="server" Text="Download Pesanan"></asp:Button></td>
                                <td class="titlefield">
                                    Total Quantity
                                    <asp:Label ID="Label1" runat="server" Style="display:none"> Total Harga</asp:Label>
                                </td>
                                <td>:
                                    <asp:Label ID="Label2" runat="server" Style="display:none">:</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblQuantity" runat="server" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Style="display:none">Rp</asp:Label>&nbsp;<asp:Label ID="lblTotal" Style="display:none" runat="server" Font-Bold="True"></asp:Label><br>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="7" align="left">
                                    <table id="tblOperator" cellspacing="1" cellpadding="1" border="0" runat="server">
                                        <tr height="40">
                                            <td>
                                                <asp:Label ID="Label7" runat="server">Mengubah Status :</asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlProses" runat="server">
                                                    <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                                                    <asp:ListItem Value="0">Konfirmasi</asp:ListItem>
                                                    <asp:ListItem Value="1">Batal Konfirmasi</asp:ListItem>
                                                    <asp:ListItem Value="2">Rilis</asp:ListItem>
                                                    <asp:ListItem Value="3">Batal Rilis</asp:ListItem>
                                                    <asp:ListItem Value="4">Tolak</asp:ListItem>
                                                    <asp:ListItem Value="5">BatalTolak</asp:ListItem>
                                                    <asp:ListItem Value="6">Blok</asp:ListItem>
                                                    <asp:ListItem Value="7">Batal Blok</asp:ListItem>
                                                    <asp:ListItem Value="8">Batal Setuju</asp:ListItem>
                                                    <asp:ListItem Value="9">Tunggu Diskon</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:Button ID="btnProses" runat="server" Text="Proses"></asp:Button>&nbsp;
                                                <asp:Button ID="btnTransferData" runat="server" Text="Transfer Data SAP"></asp:Button>&nbsp;<asp:Button ID="btnTransferUlang" runat="server" Text="Transfer Ulang"></asp:Button>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td valign="top" colspan="7">
                                    <div id="div1" style="height: 310px; overflow: auto" designtimedragdrop="1940">
                                        <asp:DataGrid ID="dgListPK" runat="server" Width="100%" OnPageIndexChanged="dgListPK_PageIndexChanged"
                                            AllowPaging="True" PageSize="25" AllowCustomPaging="True" CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
                                            CellSpacing="1" AllowSorting="True">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                            <ItemStyle BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
                                            <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                            <Columns>
                                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="PKType" HeaderText="PKType"></asp:BoundColumn>
                                                <asp:TemplateColumn Visible="False" HeaderText="ID">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('ChkExport', document.all.chkAllItems.checked)"
                                                            type="checkbox">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkExport" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="PKStatus" HeaderText="Status">
                                                    <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PKStatus") %>' Visible="False">
                                                        </asp:Label>
                                                        <asp:Label ID="lblStatusString" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PKStatus") %>'>
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                                    <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>'>
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Dealer.SearchTerm1" HeaderText="Term Cari 1">
                                                    <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSearchTerm1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtSearchTerm1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")%>'>
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang Dealer">
                                                    <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerBranchCode" runat="server"  >
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="PKNumber" SortExpression="PKNumber" HeaderText="No Reg PK">
                                                    <HeaderStyle ForeColor="White" Width="9%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PKDate" SortExpression="PKDate" HeaderText="Tanggal PK" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn Visible="false" DataField="DealerPKNumber" SortExpression="DealerPKNumber" HeaderText="Nomor PK">
                                                    <HeaderStyle ForeColor="White" Width="9%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn Visible="false" SortExpression="Category.CategoryCode" HeaderText="Kategori">
                                                    <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.ID") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.ID") %>'>
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="OrderType" HeaderText="Jenis Pesanan">
                                                    <HeaderStyle ForeColor="White" Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderType" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="ProductionYear" SortExpression="ProductionYear" HeaderText="Tahun Perakitan">
                                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ProjectName" SortExpression="ProjectName" HeaderText="Nama Pesanan Khusus">
                                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn Visible="false" HeaderText="SubTotal Harga (Rp)">
                                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Total Unit">
                                                    <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSumTargetQty" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="edit">
																<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn visible="false">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblUnFreeze" runat="server" CommandName="lnkFreeze">
                                                            <img src="../images/lock.gif" style="cursor: hand" id="imgUnFreeze" runat="server">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:BoundColumn Visible="False" DataField="PKStatus" HeaderText="statusid"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" HeaderText="DealerCode"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Persetujuan Rilis">
                                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblAprove" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Button CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="AproveRilis" ID="btnAprove" runat="server" Text="Setuju utk rilis"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                </td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr style="display:none">
                <td><b>Keterangan :</b></td>
            </tr>
            <tr style="display:none">
                <td>
                    <table>
                        <tr>
                            <td style="background-color: red">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>:</td>
                            <td>Stock Ratio tidak memenuhi target</td>
                        </tr>
                    </table>
                </td>

            </tr>
        </table>
        <div id="divPassword" style="display: none;">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>SAP Password</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" Width="171px"></asp:TextBox>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="171px"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </div>
    </form>
    <script language="javascript">
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
    </script>
</body>
</html>
