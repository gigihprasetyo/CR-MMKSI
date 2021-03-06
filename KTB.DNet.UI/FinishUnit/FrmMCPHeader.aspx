<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMCPHeader.aspx.vb" Inherits=".FrmMCPHeader" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Daftar MCP</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		    function ShowPPDealerSelection() {
		        showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {
		        var txtDealerCode = document.getElementById("txtDealerCode");
		        txtDealerCode.value = selectedDealer;
		    }

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">
						UMUM&nbsp;- Daftar MCP</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="20%"><asp:label id="lblName" runat="server">Kode Dealer</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" >
									<asp:textbox id="txtDealerCode" runat="server" MaxLength="20" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"></asp:textbox>
                                    <asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label>
								</TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"><asp:label id="lblCustName" runat="server">Nama Institusi Pemerintah</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="29%">
									<asp:textbox id="txtCustName" runat="server" MaxLength="50" size="50" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtCustName','<>?*%$;')"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"><asp:label id="lblSPLNumber" runat="server">Nomor MCP</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px">
									<asp:textbox id="txtMCPNumber" runat="server" MaxLength="22" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtSPLNumber','<>?*%$;')"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%">
									<asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%">
									<asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="29%">
									<asp:dropdownlist id="ddlStatus" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px" width="20%">Tanggal Surat</TD>
								<TD style="HEIGHT: 20px" width="1%">:</TD>
								<TD style="HEIGHT: 20px; white-space: nowrap;" >
                                    <table  cellSpacing="0" cellPadding="0" border="0">
							            <tr>
								            <td><cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								            <td>&nbsp;s.d&nbsp;</td>
								            <td><cc1:inticalendar id="icEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							            </tr>
						            </table>
								</TD>
								<TD class="titleField" style="HEIGHT: 20px" width="20%"></TD>
								<TD style="HEIGHT: 20px" width="1%"></TD>
								<TD style="HEIGHT: 20px" width="29%">
									<asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button></TD>
							</TR>
                            <tr>
                                <td colspan="6"></td>
                            </tr>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgMCPHeader" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
											BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Kode Dealer">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblDealerCode"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:BoundColumn DataField="ReferenceNumber" SortExpression="ReferenceNumber" HeaderText="Nomor MCP">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Tanggal Surat">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblLetterDate"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:BoundColumn DataField="GovInstName" SortExpression="GovInstName" HeaderText="Nama Institusi Pemerintah">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
                                                <asp:BoundColumn DataField="Attachment" SortExpression="Attachment" HeaderText="Attachment" Visible ="false">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Status">
													<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblStatus"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat SPL Detail"></asp:LinkButton>
														<asp:label id="lbtnDealer" runat="server" Width="20px" Text="Detail Dealer" Visible="false">
															<img src="../images/popup.gif" border="0" alt="Lihat Dealer"></asp:label>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Edit SPL Detail"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Delete" >
															<img src="../images/trash.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDownload" runat="server" Width="20px" Text="Download" CausesValidation="False" CommandName="Download">
															<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
                    
				</TR>
                <tr>
                    <td></td>
                </tr>
				<TR>
					<TD style="HEIGHT: 8px"><asp:Button id="btnDownLoad" runat="server" Width="96px" Text="Download"></asp:Button>&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
