<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListVDH.aspx.vb" Inherits="FrmListVDH" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListVDH</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<base target="_self">
		<script language="javascript">
			/* Deddy H	validasi value *********************************** */
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			function ShowItemNoSelection()
			{
			showPopUp('../PopUp/PopUPItemNoSelection.aspx','',500,760,ItemSelection);
			}
			function ItemSelection(selectedItem)
			{				
				var txtItemSelection = document.getElementById("txtItemNo");
				txtItemSelection.value = selectedItem;
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblSelectionData" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DATA KENDARAAN LAMA&nbsp;- Daftar Kendaraan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table id="tblCriteria" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td colSpan="9"><asp:label id="lblSelectionData" runat="server"><!--:: SELECTION DATA--><br>
									</asp:label></td>
							</tr>
							<tr>
								<td align="left" width="10%"><asp:label id="lblItemNo" runat="server" CssClass="titleField">Item No</asp:label></td>
								<td width="1%">:</td>
								<td width="20%"><asp:textbox id="txtItemNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter(txtItemNo,'<>?*%$');" runat="server"
										tMaxLength="15"></asp:textbox>
									<asp:label id="lblItemSearch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
								<td align="left" width="10%"><asp:label id="lblChassisNo" runat="server" CssClass="titleField">Chassis</asp:label></td>
								<td width="1%">:</td>
								<td width="20%"><asp:textbox id="txtChassisNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtChassisNo');"
										runat="server" MaxLength="6"></asp:textbox></td>
								<td align="left" width="10%"><asp:label id="lblSerialNo" runat="server" CssClass="titleField">Serial No</asp:label></td>
								<td width="1%">:</td>
								<td width="20%"><asp:textbox id="txtSerialNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtSerialNo');" runat="server"
										MaxLength="6"></asp:textbox></td>
							</tr>
							<tr>
								<td align="left" width="10%"><asp:label id="lblNikNo" runat="server" CssClass="titleField">NIK No</asp:label></td>
								<td width="1%">:</td>
								<td width="20%"><asp:textbox id="txtNikNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtNikNo');" runat="server"
										MaxLength="17"></asp:textbox></td>
								<td align="left" width="10%"><asp:label id="lblEngineNo" runat="server" CssClass="titleField">Engine No</asp:label></td>
								<td width="1%">:</td>
								<td width="20%"><asp:textbox id="txtEngineNo" onkeypress="TxtKeypress();" onblur="TxtBlur('txtEngineNo');" runat="server"
										MaxLength="6"></asp:textbox></td>
								<td align="left" width="10%"><asp:label id="Label2" runat="server" CssClass="titleField">Production Year</asp:label></td>
								<td width="1%">:</td>
								<td width="20%">
									<asp:DropDownList id="ddlYear" runat="server"></asp:DropDownList></td>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td><asp:button id="btnSearch" tabIndex="50" runat="server" CausesValidation="False" Text="Cari"
										Width="60px"></asp:button><asp:button id="btnCancel" tabIndex="50" runat="server" CausesValidation="False" Text="Batal"
										Width="60px"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="titleField"><asp:label id="Label1" runat="server">:: Total Records</asp:label></td>
				</tr>
				<TR>
					<TD>
						<div id="divData" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 300px"><asp:datagrid id="dtgData" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
								CellPadding="3" CellSpacing="1" AllowSorting="True" AutoGenerateColumns="False" ShowFooter="True" AllowCustomPaging="True" PageSize="50" AllowPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle HorizontalAlign="Center" ForeColor="Black" VerticalAlign="Middle" BackColor="#FFFFFF"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ItemNo" HeaderText="Item No.">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="GridlblItemNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemNo") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisNo" HeaderText="Chasis No.">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridChassisNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNo") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Serial" HeaderText="Serial No.">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridserialNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="EngineNo" HeaderText="Engine No.">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblgridEngineNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EngineNo") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="NIKNo" HeaderText="NIK No.">
										<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblgridNikNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NikNo") %>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Display">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Button ID="btnVHF" runat="server" Text="VHF" CausesValidation="False" CommandName="VHF"></asp:Button>
											<asp:Button ID="btnFSC" runat="server" Text="FSC" CausesValidation="False" CommandName="FSC"></asp:Button>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</TD>
				</TR>
				<TR>
					<TD vAlign="middle" align="left"><asp:button id="btnDownload" tabIndex="50" runat="server" CausesValidation="False" Text="Download"
							Width="60px" Enabled="False"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
