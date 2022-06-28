<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPartsStock.aspx.vb" Inherits="FrmPartsStock" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<html>
<head>
    <title>FrmPartsStock</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script>
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionArea.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }

        function openWindowDownload() {
            var h = document.getElementById('hDownloadURL');
            var url = h.value
            window.location = url;
        }

        function ShowPPDealerBranchSelection() {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + txtDealerSelection.value, '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealerBranch) {
            var txtDealerBranchSelection = document.getElementById("txtKodeDealerBranch");
            txtDealerBranchSelection.value = selectedDealerBranch;
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Data Upload - S-Part Stock</td>
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
                    <table id="Table2" cellspacing="2" cellpadding="2">
                        <tr>
                            <td class="titleField" width="150px">
                                <asp:Label ID="lblKodeDealerDetail" Text="Kode Dealer" runat="server" /></td>
                            <td width="1%">
                                <asp:Label ID="lblKodeDealerDetailSeparator" Text=":" runat="server" /></td>
                            <td width="250px">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="150px">
                                <asp:Label ID="lblNamaDealerDetail" Text="Nama Dealer" runat="server" /></td>
                            <td>
                                <asp:Label ID="lblNamaDealerDetailSeparator" Text=":" runat="server" /></td>
                            <td>
                                <p>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label><asp:HiddenField ID="hAssistUploadLogID" runat="server" />
                                </p>
                                <asp:HiddenField ID="hDownloadURL" runat="server" />
                                <asp:HiddenField ID="hQuery" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 150px">
                                <asp:Label ID="lblKodeDealerMenu" Text="Kode Dealer" runat="server"></asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="lblKodeDealerSeparator" Text=":" runat="server" /></td>
                            <td style="width: 250px">
                                <asp:TextBox ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"
                                    Width="152px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 150px">
                                <asp:Label ID="lblKodeDealerBranch" Text="Kode Cabang" runat="server"></asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="lblKodeDealerBranchSeparator" Text=":" runat="server" /></td>
                            <td style="width: 250px">
                                <asp:TextBox ID="txtKodeDealerBranch" runat="server" TextMode="MultiLine" Width="152px"></asp:TextBox>
                                <asp:Label ID="lblSearchDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="150px">Bulan</td>
                            <td width="1%">:</td>
                            <td width="250px">
                                <asp:DropDownList ID="ddlMonth" runat="server" Width="100px"></asp:DropDownList></td>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlMonth" InitialValue=" "
                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 100px" class="titleField" />
                <td rowspan="2">
                    <asp:Panel ID="pnlMonitoring" runat="server">
                        <table cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="width: 150px" class="titleField">
                                    <asp:Label ID="Label16" runat="server"> Monitoring Upload Data</asp:Label>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 150px" class="titleField">
                                    <asp:Label ID="lblPartsStock" runat="server">Total Parts Stock</asp:Label>&nbsp;</td>
                                <td style="width: 12px">:</td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Font-Bold="True">Rp</asp:Label>&nbsp;<asp:Label ID="lblTotalPartsStockValue" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>

            </tr>
            <tr>
                <td class="titleField" width="150px">Tahun</td>
                <td width="1%">:</td>
                <td width="250px">
                    <asp:DropDownList ID="ddlYear" runat="server" Width="100px"></asp:DropDownList></td>
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlYear" InitialValue=" "
                    Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                 <td style="width: 100px" class="titleField" />

            </tr>
            <tr>
                <td width="150px" class="titleField">
                    <asp:Label ID="Label1" runat="server">Nomor Part</asp:Label></td>
                <td class="auto-style2">
                    <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                <td class="auto-style3">
                    <asp:TextBox onblur="alphaNumericPlusSpaceBlur(txtNoPart)" ID="txtNoPart" onkeypress="return alphaNumericPlusSpaceUniv(event)"
                        runat="server" Width="140px" MaxLength="20"></asp:TextBox></td>
            </tr>
            <%--                            <TR>
                                <TD class="titleField" style="WIDTH: 150px"><asp:label id="lblStatusMenu" Text="Status" runat="server"></asp:label></TD>
								<TD width="1%"><asp:label id="lblStatusSeparator" Text=":" runat="server"/></TD>
								<TD width="250px"><asp:dropdownlist id="ddlStatus" runat="server" Width="180px"></asp:dropdownlist></TD><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlStatus" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator></td>
							</TR>--%>
            <tr>
                <td class="titleField"></td>
                <td></td>
                <td>
                    <asp:Button ID="btnCari" runat="server" Text=" Cari " Width="60px" CausesValidation="False"></asp:Button>
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " Width="80px" CausesValidation="False"></asp:Button></td>
            </tr>
        </table>
        </TD>
				</TR>
				<tr>
                    <td>
                        <div id="div1" style="overflow: auto; height: 360px">
                            <asp:DataGrid ID="dtgPartStock" runat="server" Width="100%" PageSize="100" AllowCustomPaging="True"
                                AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
                                BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn Visible="False" HeaderText="ID">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Month" HeaderText="Bulan">
                                        <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Month")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Year" HeaderText="Tahun">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Year")%>' ID="Label5" NAME="Label5">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' ID="Label11" NAME="Label11">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="DealerBranchCode" HeaderText="Cabang Dealer">
                                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranchCode")%>' ID="lbldealerbranchcodeitem" NAME="lbldealerbranchcodeitem">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NoParts" HeaderText="No Parts">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoParts")%>' ID="Label6" NAME="Label6">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="JumlahStokAwal" HeaderText="Jumlah Stok Awal">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemStyle Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblJmlStokAwal" NAME="Label7">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="JumlahDatang" HeaderText="Jumlah Datang">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblJmlDatang" NAME="Label10">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="HargaBeli" HeaderText="Harga Beli">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.HargaBeli") ,"0.00")%>' ID="lblHargaBeli" NAME="lblHargaBeli">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Status">
                                        <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssistUploadLog.StatusDescription")%>' ID="Label22" NAME="Label22">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
        <tr>
            <td colspan="6" valign="bottom">
                <table id="tblOperator" style="width: 192px; height: 46px" cellspacing="1" cellpadding="1"
                    width="192" border="0" runat="server">
                    <tr>
                        <td valign="top">
                            <asp:Button ID="btnBack" Text="Kembali" runat="server"></asp:Button></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        </TABLE>

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
