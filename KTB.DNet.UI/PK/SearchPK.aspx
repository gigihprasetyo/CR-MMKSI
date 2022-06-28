<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SearchPK.aspx.vb" Inherits="SearchPK" SmartNavigation="False" %>

<html>
<head>
    <title>SearchPK</title>
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

                var txtDealerSelection = document.getElementById("txtKodeDealer");
                if (txtDealerSelection.value != '')
                {
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
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ViewDailyPKFlow()
        { }
        function ValidateData() {
            var ddl = document.getElementById("ddlRencanaPenebusan");
            var txt = document.getElementById("txtPKNumber");
            var lbl = document.getElementById("lblValidator");
            var rsl = true;

            lbl.style.visibility = "hidden";
            if (ddl.selectedIndex == 0) {
                if (txt.value == "") {
                    lbl.style.visibility = "visible";
                    rsl = false;
                }
            }
            //alert(rsl);
            return rsl;
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
    <form id="Form1" method="post" runat="server" autocomplete="off">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PESANAN KENDARAAN - Daftar Pesanan Kendaraan</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" colspan="3" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="txtKategori" runat="server" Font-Bold="True">Kategori</asp:Label></td>
                            <td>:</td>
                            <td style="width: 190px">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="88px" AutoPostBack="True"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSubCategory" runat="server"></asp:DropDownList></td>
                            <td style="height: 24px" width="20%">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True">Rencana Penebusan</asp:Label></td>
                            <td style="height: 24px" width="1%">:</td>
                            <td style="width: 155px; height: 24px" width="155">
                                <asp:DropDownList ID="ddlRencanaPenebusan" runat="server" Width="140px" Height="16px"></asp:DropDownList></td>
                            <td style="height: 24px" width="13%"></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True">Jenis Pesanan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlOrderType" runat="server" Width="140"></asp:DropDownList></td>
                            <td class="titleField" style="height: 24px" width="20%">Kode Dealer</td>
                            <td style="height: 24px" width="1%">:</td>
                            <td style="height: 24px" width="25%">
                                 <input style="display: none" type="text" name="fakeusernameremembered" />
<input style="display: none" type="password" name="fakepasswordremembered" />
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                                    runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">
                                <asp:Label ID="txtKondisiPesanan" runat="server" Font-Bold="True">Kondisi Pesanan</asp:Label></td>
                            <td style="width: 19px; height: 18px">:</td>
                            <td style="width: 155px; height: 18px">
                                <asp:DropDownList ID="ddlPurpose" runat="server" Width="140"></asp:DropDownList></td>
                            <td class="titleField">Cabang Dealer</td>
                            <td>:</td>
                            <td>
                                 <input style="display: none" type="text" name="fakeusernameremembered" />
<input style="display: none" type="password" name="fakepasswordremembered" />
                                <asp:TextBox ID="txtDealerBranchCode" Runat="server"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" runat="server" width="10"> 
										<img alt="Klik Popup" border="0" src="../images/popup.gif" style="cursor:hand">
									</img></asp:Label>
                                <asp:HiddenField runat="server" ID="hdnTitle" Value="DEALER" /> 
                            </td>
                            <td style="height: 18px"></td>
                        </tr>
                        <tr valign="top">
                            <td colspan="3"></td>
                            <td class="titleField" style="height: 18px">
                                No Reg PK</td>
                            <td style="height: 18px">:</td>
                            <td style="height: 18px">
                                <input style="display: none" type="text" name="fakeusernameremembered" />
<input style="display: none" type="password" name="fakepasswordremembered" />
                                <asp:TextBox onkeypress="return alphaNumericPlusSpaceUniv(event)" ID="txtPKNumber" onblur="alphaNumericPlusSpaceBlur(txtPKNumber)"
                                    runat="server" Width="140px" MaxLength="40" AutoCompleteType="None"  autocomplete="none"></asp:TextBox>
                                <asp:Label ID="lblValidator" Style="visibility: hidden" runat="server" ForeColor="Red">*</asp:Label><strong></strong>

                            </td>
                            <td valign="bottom"></td>
                        </tr>
                         <tr valign="top">
                            <td class="titleField" style="height: 24px" width="20%">
                                <asp:Label ID="txtStatus" runat="server" Font-Bold="True">Status</asp:Label></td>
                            <td style="height: 24px" width="1%">:</td>
                            <td style="height: 24px" width="25%">
                               
                                <asp:ListBox ID="lboxStatus" runat="server" Width="136px" Rows="3" SelectionMode="Multiple"></asp:ListBox>
                               
                            </td>
                            <td class="titleField">
                                <p>Total Quantity</p>
                                <p>
                                    <asp:Label ID="Label1" runat="server" Visible="false" Style="display:none">Total Harga</asp:Label></p>
                            </td>
                            <td style="width: 19px">
                                <p>:</p>
                                <p>
                                    <asp:Label ID="Label2" runat="server" Visible="false" Style="display:none">:</asp:Label></p>
                            </td>
                            <td style="width: 155px">
                                <p>
                                    <asp:Label ID="lblQuantity" runat="server" Font-Bold="True"></asp:Label></p>
                                <p>
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Visible="false" Style="display:none">Rp</asp:Label><asp:Label ID="lblTotal" runat="server" Visible="false" Style="display:none" Font-Bold="True"></asp:Label></p>
                            </td>
                            <td style="height: 24px" width="13%"></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td></td>
                            <td colspan="2"><asp:Button ID="BtnCari" runat="server" Width="60px" Text="Cari"></asp:Button>&nbsp;
                                <asp:Button ID="btnDownload" runat="server" Text="Download"></asp:Button>&nbsp;
                                <asp:Button ID="btnTransferData" runat="server" Text="Transfer Data SAP"></asp:Button>&nbsp;
                                <asp:Button ID="btnTransferUlang" runat="server" Text="Transfer Ulang"></asp:Button>&nbsp;
                                <asp:Button ID="btnDelete" runat="server" Text="Hapus"></asp:Button>
                            </td>
                            <td></td>
                            <td style="width: 155px">                               &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="width: 100%; height: 280px; overflow: auto">
                        <asp:DataGrid ID="dtgcari" runat="server" Width="100%" AllowSorting="True" HorizontalAlign="Center"
                            OnItemDataBound="dtgCari_ItemDataBound" GridLines="None" BorderColor="#E0E0E0" AutoGenerateColumns="False" OnItemCommand="dtgCari_Edit" BackColor="#E0E0E0"
                            CellPadding="3" CellSpacing="1" AllowCustomPaging="True" PageSize="25" AllowPaging="True" OnPageIndexChanged="dtgcari_PageIndexChanged">
                            <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle" BackColor="Navy"></HeaderStyle>
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
									<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableSales"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('ChkExport', document.all.chkAllItems.checked)"
											type="checkbox">
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox id="ChkExport" runat="server"></asp:CheckBox>
                                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Visible="false">
                                                </asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
                                 
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" DataField="PKType"></asp:BoundColumn>
                                <asp:BoundColumn SortExpression="PKStatus" HeaderText="Status">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                    <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
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
                                    <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerBranchCode" runat="server"  >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="PKNumber" SortExpression="PKNumber" HeaderText="No Reg PK">
                                    <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PKDate" SortExpression="PKDate" HeaderText="Tanggal PK" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="false" DataField="DealerPKNumber" SortExpression="DealerPKNumber" HeaderText="Nomor PK">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn Visible="false" SortExpression="Category.CategoryCode" HeaderText="Kategori">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Category.CategoryCode") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="OrderType" SortExpression="OrderType" ReadOnly="True" HeaderText="Jenis Pesanan">
                                    <HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProductionYear" SortExpression="ProductionYear" HeaderText="Tahun Perakitan">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ProjectName" SortExpression="ProjectName" HeaderText="Nama Pesanan Khusus">
                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="false" HeaderText="SubTotal Harga (Rp)">
                                    <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Total Unit">
                                    <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSumTargetQty" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <asp:LinkButton ID="lbnEdit" runat="server" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" HeaderText="Dealer Code"></asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="PKStatus" HeaderText="Status"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHistoryStatus" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" alt="Perubahan Status"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlow" runat="server">
												<img src="../images/alur_flow2.gif" style="cursor:hand" alt="Alur PK"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblUnFreeze" runat="server" CommandName="lnkFreeze">
                                            <img src="../images/lock.gif" style="cursor: hand" id="imgUnFreeze" runat="server">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="40">
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
