<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmSPPOListForKTB.aspx.vb" Inherits="frmSPPOListForKTB"  smartNavigation="False" %>
<%@ Import Namespace="KTB.DNet.Domain"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pembatalan Pemesanan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function newLocation(loc)
			{
			window.location=loc
			}		
			
		
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtDealerCode");
			txtDealerSelection.value = selectedDealer;			
		}
		
		function SparePartPO(selectedCode)
		{
				alert("PO tidak ditemukan")
		}
		
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">PEMESANAN - Daftar Pesanan Dealer</td>
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
                            <tr>
                                <td class="titleField">Kode Dealer</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                        runat="server" Width="144px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Jenis Pesanan</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:DropDownList ID="ddlOrderType" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField">Status</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlProcessCode" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <label for="chkPODate">Tanggal Pesanan</label></td>
                                <td>:</td>
                                <td>
                                    <table cellspacing="0" cellpadding="2" border="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkPODate" runat="server" Checked="true" /></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <cc1:inticalendar id="icPODateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                            <td>&nbsp;s.d&nbsp;</td>
                                            <td>
                                                <cc1:inticalendar id="icPODateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField">
                                    <label for="chkSenDate">Tanggal Kirim</label></td>
                                <td>:</td>
                                <td>
                                    <table cellspacing="0" cellpadding="2" border="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkSenDate" runat="server" Checked="false" /></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <cc1:inticalendar id="icSendDateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                            <td>&nbsp;s.d&nbsp;</td>
                                            <td>
                                                <cc1:inticalendar id="icSendDateTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">Nomor Pesanan</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:TextBox ID="txtNomorPesanan" runat="server" Width="160px" Visible="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">Telah Ditransfer</td>
                                <td width="1%">:</td>
                            <td>
                                <asp:DropDownList runat="server" id ="ddlTelahDitransfer" Width="160px" ></asp:DropDownList>
                            </td>
                                </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnFind" runat="server" Text="Cari" Width="60px"></asp:Button>
                                            <asp:Button ID="btnDownload" runat="server" Text="Download" Width="60px"></asp:Button>
                                    <span style="right: 70px; position: absolute;"><asp:Label ID="lblGrandTotal" runat="server"></asp:Label></span>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="div1" style="overflow: auto; height: 320px">
                            <asp:DataGrid ID="dtgSPPO" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="25"
                                AllowPaging="True" AllowSorting="True" AllowCustomPaging="True" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BackColor="Gainsboro"
                                BorderWidth="0px">
                                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerCode" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).Dealer.DealerCode %>'>Label</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerName" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).Dealer.DealerName %>'>Label</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="PONumber" HeaderText="Nomor Pesanan">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPONumber" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).PONumber %>'>Label</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tanggal Pesanan">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPODate" runat="server"></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="SentPODate" HeaderText="Tanggal Kirim">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSentPODate" runat="server"></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="OrderType" HeaderText="Jenis Pesanan">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).OrderTypeDesc %>'>Label</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ProcessCode" HeaderText="Status">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcessCode" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).ProcessCodeDesc %>'>Label</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Nilai Pemesanan">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNilaiPemesanan" runat="server" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="TermOfPayment.ID" HeaderText="Cara Pembayaran">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label id="Label1" runat="server" Text='<%# CType(Container.DataItem, SparePartPO).TermOfPayment.Description%>'>Label</asp:Label>--%>
                                            <asp:Label ID="LabelTOP" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.TermOfPayment.Description"), String) = "", "", CType(DataBinder.Eval(Container, "DataItem.TermOfPayment.Description"), String))%>'>Label</asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="IsTransfer" HeaderText="Telah Ditransfer">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIstransfer" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="">
                                        <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkCheckAll" AutoPostBack="True" OnCheckedChanged="chkCheckAll_CheckedChanged" runat="server"></asp:CheckBox>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCanceled" runat="server"></asp:CheckBox>
                                            <asp:Label ID="lblDetail" runat="server">
												<img style="cursor:hand" alt="Rincian" src="../images/detail.gif" border="0" height="17px"
													width="17px">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnProcess" runat="server" Text="Proses Pembatalan Pesanan"></asp:Button>
                        <asp:Button ID="btnSend" runat="server" Text="Kirim Ke SAP"></asp:Button>
                    </td>
                </tr>
            </table>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
