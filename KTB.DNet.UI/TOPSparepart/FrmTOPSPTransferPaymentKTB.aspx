<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTOPSPTransferPaymentKTB.aspx.vb" Inherits="FrmTOPSPTransferPaymentKTB" smartNavigation="False"%>
<%@ Register assembly="KTB.DNet.WebCC" namespace="KTB.DNet.WebCC" tagprefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Form TOP SparePart</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>

        <script>

		    //ini function yang di panggil oleh behind code saat label pop up di clik

            function ShowPPBillingNumber() {
                var lblNoRegPembayaran = document.getElementById("lblNoRegPembayaran");
                showPopUp('../General/../PopUp/PopUpTOPSPBilling.aspx?hrn=' + lblNoRegPembayaran.innerHTML, '', 500, 900, BillingSelection);
		    }

            function BillingSelection(selectedBilling) {

                var txtReturnBilling = document.getElementById("txtReturnBilling");
		        
                var strSelectedBilling = selectedBilling;
                txtReturnBilling.value = selectedBilling;
                //alert(txtReturnBilling.value)
                txtReturnBilling.onblur();
                 //alert(selectedBilling);

                //lblReturnBilling.i = selectedBilling;

                
		    }



		</script>


	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
					<td colspan="2" class="titlePage" style="HEIGHT: 8px"></td>
				</tr>
                <tr>
					<td colspan="2" class="titlePage" style="HEIGHT: 8px"></td>
				</tr>
				<tr>
					<td colspan="2" class="titlePage" style="HEIGHT: 8px">SparePart TOP Payment</td>
				</tr>
				<tr>
					<td colspan="2" style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td colspan="2" style="HEIGHT: 8px" height="8"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Credit Account</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblCreditAccount" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Nama Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblNamaDealer" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 29px" width="24%">No. Reg Pembayaran</TD>
								<TD style="HEIGHT: 29px" width="1%">:</TD>
								<TD style="WIDTH: 75%; HEIGHT: 29px">
									<asp:label id="lblNoRegPembayaran" runat="server"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 12px" width="24%">Status</TD>
								<TD style="HEIGHT: 12px" width="1%">:</TD>
								<TD style="WIDTH: 261px; HEIGHT: 12px" width="261"><asp:label id="lblStatus" runat="server"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 12px" width="24%">Billing Number</TD>
								<TD style="HEIGHT: 12px" width="1%">:</TD>
								<TD style="WIDTH: 261px; HEIGHT: 12px" width="261">
                                    <%--<asp:LinkButton ID="lbtnBillingNumber" runat="server"><asp:label id="lblBillingNumber" runat="server">
                                    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></asp:LinkButton>--%>
                                    <%--<asp:TextBox ID="txtReturnBilling" onblur="omitSomeCharacter('txtReturnBilling','<>?*%$')" runat ="server" style="display:none" AutoPostBack="true" ></asp:TextBox>--%>
								</TD>
							</TR>
							
							
						</TABLE>
					</TD>
                    <TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Tanggal Dibuat</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblTanggalDibuat" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Tanggal Transfer</TD>
								<TD width="1%">:</TD>
								<TD width="75%">
                                    <cc1:inticalendar id="calTanggalTransfer" runat="server" TextBoxWidth="150"></cc1:inticalendar>
                                </TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 29px" width="24%">Tanggal Jatuh Tempo</TD>
								<TD style="HEIGHT: 29px" width="1%">:</TD>
								<TD style="WIDTH: 75%; HEIGHT: 29px">
									<asp:label id="lblTglJatuhTempo" runat="server"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 12px" width="24%">&nbsp;</TD>
								<TD style="HEIGHT: 12px" width="1%">&nbsp;</TD>
								<TD style="WIDTH: 261px; HEIGHT: 12px" width="261">&nbsp;</TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 12px" width="24%">Total Transfer</TD>
								<TD style="HEIGHT: 12px" width="1%">:</TD>
								<TD style="WIDTH: 261px; HEIGHT: 12px" width="261"><asp:label id="lblTotalTransfer" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <tr>
                                <td valign="top" colspan="6">
                                    <div id="div1" style="overflow: auto; height: 330px; width: 100%;">
                                        <asp:DataGrid ID="dtgTOPSP" runat="server" Width="100%" AllowSorting="True" CellPadding="3" BorderWidth="0px"
                                            CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" PageSize="50">
                                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                            <ItemStyle BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateColumn Visible="False" HeaderText="ID">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTOPSPID" runat="server" Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn Visible="false" HeaderText="Status">
                                                    <HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowStatus" runat="server" Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn SortExpression="SparePartBilling.Dealer.DealerCode" HeaderText="Dealer Code">
                                                    <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn SortExpression="SparePartBilling.BillingNumber" HeaderText="Billing Number">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBillingNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartBIlling.BillingNumber")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Tanggal Billing" SortExpression="SparePartBilling.BillingDate">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBillingDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartBIlling.BillingDate", "{0:dd/MM/yyyy}")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                               <asp:TemplateColumn HeaderText="Amount Billing + Tax">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmountBillTax" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Amount C2">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmountC2" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Total Amount">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmount" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Total Amount" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmountHide" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <%--<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                        
														
													</ItemTemplate>
												</asp:TemplateColumn>--%>
                                                
                                            </Columns>
                                            <PagerStyle Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2" align="center" >
                        <asp:Button runat="server" ID="btnKembali" CausesValidation="false" Text="Kembali"/>
                        <asp:Button runat="server" ID="btnKonfirmasi" CausesValidation="false" Text="Konfirmasi"/>
                        <asp:Button runat="server" ID="btnTolak" CausesValidation="false" Text="Tolak"/>
                        <asp:Button runat="server" ID="btnValidasi" CausesValidation="false" Text="Validasi" />
                    </td>
                </tr>
			</TABLE>
            
		</form>
	</body>
</HTML>
