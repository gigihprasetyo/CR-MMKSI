<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmPopUpTransferActual.aspx.vb" Inherits=".frmPopUpTransferActual" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpCustomerList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">

		    function GetSelectedCustomer() {
		        var table;
		        var bcheck = false;
		        table = document.getElementById("dtgCustomerSelection");
		        var Customer = '';
		        for (i = 1; i < table.rows.length; i++) {
		            var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		            if (radioBtn != null && radioBtn.checked) {
		                if (navigator.appName == "Microsoft Internet Explorer") {
		                    Customer = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    window.returnValue = Customer;
		                    bcheck = true;
		                }
		                else if (navigator.appName == "Netscape") {
		                    Customer = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    opener.dialogWin.returnFunc(Customer);
		                    bcheck = true;
		                }
		                else {
		                    Customer = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    opener.dialogWin.returnFunc(Customer);
		                    bcheck = true;
		                }
		                break;
		            }
		        }

		        if (bcheck) {
		            window.close();
		        }
		        else {
		            alert("Silahkan Pilih Customer terlebih dahulu");
		        }
		    }


		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">Detail Transfer Actual</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="30%">&nbsp;<asp:Label runat="server" ID="lblDeaelerCode"></asp:Label>  </TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%">Tujuan Pembayaran</TD>
								<TD width="1%">:</TD>
								<TD width="20%"><asp:Label ID="lblPaymentPurspose" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px">Nama Dealer</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px">&nbsp;<asp:Label runat="server" ID="lblNamaDealer"></asp:Label></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px"></TD>
								<TD class="titleField" style="HEIGHT: 20px">Total Amount</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px">&nbsp;<asp:Label runat="server" ID="lblTOtalAmount"></asp:Label></TD>
							</TR>

                            <TR>
								<TD class="titleField" style="HEIGHT: 20px">Nomor Reg</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px">&nbsp; &nbsp;<asp:Label runat="server" ID="lblNoreg"></asp:Label></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px"></TD>
								<TD class="titleField" style="HEIGHT: 20px">Total Transfer</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px">&nbsp;<asp:Label runat="server" ID="lblTotalTransfer"></asp:Label></TD>
							</TR>


							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgCustomerSelection" runat="server" Width="100%" AutoGenerateColumns="False"
											 llowPaging="False" AllowSorting="False">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle ForeColor="White"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														 <asp:Label ID="lblNo" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Ref Bank">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblRefbank" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Posting Date">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPostingDate" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

                                                	<asp:TemplateColumn HeaderText="Transfer Amount">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblAmount" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
										</Columns>		 
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD colspan="7" align="center">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
