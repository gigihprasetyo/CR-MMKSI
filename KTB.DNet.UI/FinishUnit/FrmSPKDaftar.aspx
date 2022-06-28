<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSPKDaftar.aspx.vb" Inherits="FrmSPKDaftar" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<html>
<head>
    <title>SearchPK</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ShowPPDealerBranchSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealerBranch) {
            var txtKodeDealerBranch = document.getElementById("txtKodeDealerBranch");
            txtKodeDealerBranch.value = selectedDealer;
        }

        function ShowSalesmanSelection() {
            showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=1', '', 500, 760, SalemanSelection);

        }
        function SalemanSelection(selectedSales) {
            var temp = selectedSales.split(";")
            var txtSalesman = document.getElementById('txtSalesmanCode');
            var txtSalesNama = document.getElementById('lblNamaSalesman');
            var txtSalesLevel = document.getElementById('lblLevelSalesman');
            var txtSalesJabatan = document.getElementById('lblJabatan');
            txtSalesman.value = temp[0];
            txtSalesNama.innerHTML = temp[1];
            txtSalesLevel.innerHTML = temp[4];
            txtSalesJabatan.innerHTML = temp[3];
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
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">SURAT PESANAN KENDARAAN - Daftar Surat Pesanan Kendaraan</td>
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
                            <td class="titleField" style="width: 120px; height: 24px">Kode Dealer</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
                                    runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td class="titleField" style="width: 120px; height: 24px">Kategori</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:DropDownList ID="ddlKategori" runat="server" Width="120px" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 120px; height: 24px">No. Reg. SPK</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:TextBox ID="txtSPKRegistered" runat="server"></asp:TextBox>
                            <td class="titleField" style="width: 120px; height: 24px">Tipe</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:DropDownList ID="ddlTipe" runat="server" Width="120px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 120px; height: 24px">Reg. Indent Number</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:TextBox ID="txtIndentNumber" runat="server"></asp:TextBox>
                            <td class="titleField" style="width: 120px; height: 24px">Tipe Body</td>
                            <td style="width: 2px; height: 24px"></td>
                            <td style="height: 24px; width: 220px">
                                <asp:DropDownList ID="ddlTipeWarna" runat="server" Width="120px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 120px; height: 24px">No. SPK Dealer</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:TextBox ID="txtSPKDealer" runat="server"></asp:TextBox>
                            <td class="titleField" style="width: 120px; height: 24px">Salesman</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:TextBox ID="txtSalesmanCode" runat="server" Width="120px"></asp:TextBox><asp:Label ID="lblShowSalesman" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 120px; height: 24px">Tanggal SPK Dealer</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkFilterDealerSPKDate" runat="server" Checked="False"></asp:CheckBox></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icDealerSPKDateStart" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server">&nbsp;s.d&nbsp;</asp:Label></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icDealerSPKDateEnd" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField" style="width: 120px; height: 24px">
                                <asp:Label ID="lblStatusFaktur" runat="server">Status Faktur</asp:Label></td>
                            <td style="width: 2px; height: 24px">
                                <asp:Label ID="lblStatusFakturSpr" runat="server">:</asp:Label></td>
                            <td style="height: 24px; width: 220px">
                                <asp:CheckBox ID="chkValidate" runat="server" Text="Faktur Sudah Validasi"></asp:CheckBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 120px; height: 24px">Tanggal SPK Input</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkFilterDealerSPKInputDate" runat="server" Checked="True"></asp:CheckBox></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icDealerSPKInputDateStart" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server">&nbsp;s.d&nbsp;</asp:Label></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icDealerSPKInputDateEnd" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>

                            <td class="titleField" style="width: 120px; height: 24px">Propinsi</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:DropDownList ID="ddlPropinsi" runat="server" Width="130px" AutoPostBack="True"></asp:DropDownList></td>

                        </tr>
                        <tr>
                            <td class="titleField" style="width: 120px; height: 24px">
                                <asp:Label ID="lblSalesman" runat="server" Font-Bold="True">Jumlah Unit</asp:Label></td>
                            <td>:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:Label ID="lblTotalUnit" runat="server" Text="0"></asp:Label>&nbsp; Unit</td>

                            <td class="titleField" style="width: 120px; height: 24px">Kota</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:DropDownList ID="ddlKota" runat="server" Width="130px"></asp:DropDownList></td>

                        </tr>
                        <tr>
                            <td class="titleField" style="width: 120px; height: 24px" valign="top">Status SPK</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px" valign="center">
                                <asp:ListBox ID="lboxStatus" runat="server" Width="130px" Rows="3" SelectionMode="Multiple"></asp:ListBox>
                            </td>

                            <td class="titleField" style="width: 120px; height: 24px" valign="top">Status Pemenuhan</td>
                            <td style="width: 2px; height: 24px" valign="top">:</td>
                            <td style="height: 24px; width: 220px" valign="top">
                                <asp:DropDownList ID="ddlAlokasi" runat="server" Width="130px">
                                    <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                                    <asp:ListItem Value="0">Pemenuhan Sebagian</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="titleField" style="width: 120px; height: 24px; display: none">Status Konsumen</td>
                            <td style="width: 2px; height: 24px; display: none">:</td>
                            <td style="height: 24px; width: 220px; display: none">
                                <asp:CheckBox ID="chkKonsumen" runat="server" Text="Jadi Konsumen" Visible="false" />
                                <asp:DropDownList ID="ddlStatusKonsumen" runat="server" Width="130px">
                                    <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                                    <asp:ListItem Value="0">Belum Jadi Konsumen</asp:ListItem>
                                    <asp:ListItem Value="1">Sudah Jadi Konsumen</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td class="titleField" style="width: 120px; height: 24px">
                                <asp:Label ID="Label3" runat="server" Visible="false">Cabang Dealer</asp:Label></td>
                            <td style="width: 2px; height: 24px"></td>
                            <td style="height: 24px; width: 220px">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerBranch"
                                    onblur="omitSomeCharacter('txtDealerBranch','<>?*%$')"
                                    runat="server" Visible="false"></asp:TextBox>
                                <asp:Label ID="lblSearchDealerBranch" runat="server" Visible="false">
										            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                                <asp:Label ID="lblDealerBranchID" runat="server" Visible="false"></asp:Label>
                            </td>

                        </tr>
                        <tr style="visibility: hidden">
                            <td class="titleField" style="width: 120px; height: 24px">Jumlah Harga</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:Label ID="lblTotalHarga" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <asp:Button ID="btnCari" runat="server" Width="80px" Text=" Cari "></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; width: 100%; height: 280px">
                        <!--
								OnItemDataBound="dtgCari_ItemDataBound" 
								OnItemCommand="dtgCari_Edit"
								OnPageIndexChanged="dtgcari_PageIndexChanged"
							-->
                        <asp:DataGrid ID="dtgcari" runat="server" Width="100%" AllowSorting="True" HorizontalAlign="Center"
                            AllowPaging="True" BackColor="#E0E0E0" GridLines="None" BorderColor="#E0E0E0" AutoGenerateColumns="False"
                            CellPadding="3" CellSpacing="1" AllowCustomPaging="True" PageSize="25">
                            <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle" BackColor="Navy"></HeaderStyle>
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('cbCheck', document.all.chkAllItems.checked)"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbCheck" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status SPK">
                                    <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>' Visible="False">
                                        </asp:Label>
                                        <asp:Label ID="lblStatusString" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Status Pemenuhan">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusAlokasi" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="DealerBranch.Name" HeaderText="Cabang Dealer" Visible="true">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.Name")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:BoundColumn DataField="SPKNumber" SortExpression="SPKNumber" HeaderText="Nomor SPK">
                                    <HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
                                </asp:BoundColumn>--%>
                                <asp:TemplateColumn SortExpression="SPKNumber" HeaderText="Nomor SPK">
                                    <HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSPKNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SPKNumber")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SPKCustomer.Name1" HeaderText="Nama Customer">
                                    <HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SPKCustomer.Name1") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="IndentNumber" SortExpression="IndentNumber" HeaderText="No Reg Indent" Visible="false">
                                    <HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Pengajuan SPK">
                                    <HeaderStyle Width="6%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerSPKInputDate" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal SPK Dealer" Visible="True">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerSPKDate" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Periode Pengajuan Faktur" Visible="false">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kategori">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryCode" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Unit">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalUnit" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="Propinsi" Visible="false">
                                    <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Kota" Visible="false">
                                    <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Salesman">
                                    <HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGridSalesmanName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SalesmanHeader.JobPosition.Description" HeaderText="Posisi Salesman">
                                    <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGridSalesmanPosition" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.JobPosition.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanLevel.Description" HeaderText="Level Salesman" Visible="false">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGridSalesmanLevel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanLevel.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jadi Konsumen" Visible="false">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCustReq" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Konsumen Faktur">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalKonsumenFaktur" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Jadi Konsumen">
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalJadiKonsumen" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="RejectedReason" HeaderText="Alasan Batal">
                                    <HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlRejectedReason" runat="server" Width="120px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="File SPK">
                                    <HeaderStyle Width="4%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <%--<div id="imgbox">
                                            <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                        </div>--%>
                                        <asp:LinkButton ID="lnkFile" runat="server" CommandName="DownloadFile" Visible="true">
                                            <img src="../images/download.gif" style="cursor:hand" border="0" alt="Download File"/>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbnEdit" runat="server" CommandName="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="lbnView" runat="server" CommandName="View"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="8%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbnProfile" runat="server" CommandName="Profile"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHistoryStatus" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" alt="Perubahan Status">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tblOperator" cellspacing="1" cellpadding="1" border="0" runat="server">
                        <tr height="40">
                            <td>
                                <asp:Label ID="Label7" runat="server">Mengubah Status :</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlProses" runat="server">
                                    <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
                                    <%--<asp:ListItem Value="0">Awal</asp:ListItem>
                                    <asp:ListItem Value="7">Indent</asp:ListItem>
                                    <asp:ListItem Value="1">Tanda Jadi</asp:ListItem>
                                    <asp:ListItem Value="2">Lunas</asp:ListItem>
                                    <asp:ListItem Value="4">Pending</asp:ListItem>--%>
                                    <asp:ListItem Value="8">Pending Konsumen</asp:ListItem>
                                    <asp:ListItem Value="9">Tunggu Unit</asp:ListItem>
                                    <%--<asp:ListItem Value="10">Tunggu Unit (I)</asp:ListItem>
                                    <asp:ListItem Value="11">Tunggu Unit (II)</asp:ListItem>
                                    <asp:ListItem Value="12">Tunggu Unit (III)</asp:ListItem>
                                    <asp:ListItem Value="13">Tunggu Unit (IV)</asp:ListItem>
                                    <asp:ListItem Value="14">Tunggu Unit (V)</asp:ListItem>
                                    <asp:ListItem Value="15">Tunggu Unit (VI)</asp:ListItem>
                                    <asp:ListItem Value="16">Tunggu Unit (VII)</asp:ListItem>--%>
                                    <asp:ListItem Value="3">Batal</asp:ListItem>
                                    <%--<asp:ListItem Value="4">Pending</asp:ListItem>
                                    <asp:ListItem Value="5">Selesai</asp:ListItem>
                                    <asp:ListItem Value="6">Closed</asp:ListItem>--%>
                                </asp:DropDownList></td>
                            <td>
                                <input id="hdConfirm" runat="server" type="hidden" value="-1">
                                <asp:Button ID="btnProses" runat="server" Text="Proses"></asp:Button></td>
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Text="Download"></asp:Button></td>
                            <td>
                                <asp:HiddenField ID="HFKonsumenConfirmation" runat="server" />
                                <asp:HiddenField ID="HFKonsumenVerivy" runat="server" />
                                <asp:Button ID="btnSetKonsumen" runat="server" Text="Jadi Konsumen"></asp:Button></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="5">
                                <asp:Label ID="Label2" runat="server">Keterangan :</asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:TextBox ID="txt1" Width="60px" runat="server" BackColor="Yellow" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                            <td colspan="4">: Status belum update</td>
                        </tr>
                        <%--<tr>
                            <td align="right">
                                <asp:TextBox ID="Textbox1" Width="60px" runat="server" BackColor="#ffa500" BorderStyle="None"
                                    Enabled="False"></asp:TextBox></td>
                            <td colspan="4">: Selesai - belum jadi konsumen</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:TextBox ID="txtRed" Width="60px" runat="server" BackColor="Red" BorderStyle="None" Enabled="False"></asp:TextBox></td>
                            <td colspan="4">: Status tidak berubah lebih dari 14 hari</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:TextBox ID="txtYellow" Width="60px" runat="server" BackColor="Yellow" BorderStyle="None"
                                    Enabled="False"></asp:TextBox></td>
                            <td colspan="4">: Status tidak berubah lebih dari 7 hari dan kurang dari 14 hari</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:TextBox ID="txtGainsboro" Width="60px" runat="server" BackColor="Gainsboro" BorderStyle="None"
                                    Enabled="False"></asp:TextBox></td>
                            <td colspan="4">: Customer belum diverifikasi</td>
                        </tr>--%>
                    </table>
                </td>
                <td></td>
            </tr>
        </table>
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
