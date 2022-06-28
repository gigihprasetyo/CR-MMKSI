<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUserOrganizationAssignment.aspx.vb" Inherits="FrmUserOrganizationAssignment"	 smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmUserOrganizationAssignment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function ShowPPDealerSelection(DealerGroupID,DealerCode)
			{
				var obj = document.getElementById("lblSearchDealer")
				var enabled = obj.getAttribute("enabled")
				if (enabled == "1")
				{
					showPopUp('../General/../PopUp/PopUpDealerSelection.aspx?Group='+DealerGroupID+'&Dealer='+DealerCode,'',500,760,DealerSelection);
				}				
			}
			function DealerSelection(selectedDealer)
			{
				var txtDealerSelection = document.getElementById("txtSearchDealer");
				txtDealerSelection.value = selectedDealer;			
			}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">ADMIN SISTEM&nbsp;- User dan Organisasi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">ID</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblID" runat="server"></asp:label></TD>
								<TD width="20%"></TD>
								<TD width="1%"></TD>
								<TD width="29%"></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Login*</TD>
								<TD>:</TD>
								<TD><asp:label id="lblLogin" runat="server"></asp:label></TD>
								<TD class="titleField">Posisi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblPosition" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Depan*</TD>
								<TD>:</TD>
								<TD><asp:label id="lblName1" runat="server"></asp:label></TD>
								<TD class="titleField">Kode Organisasi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Belakang</TD>
								<TD>:</TD>
								<TD><asp:label id="lblName2" runat="server"></asp:label></TD>
								<TD class="titleField">Nama Organisasi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblDealerName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Tambah Organisasi</TD>
								<TD style="HEIGHT: 26px">:</TD>
								<TD style="HEIGHT: 26px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtSearchDealer" onblur="omitSomeCharacter('txtSearchDealer','<>?*%$')"
										runat="server" MaxLength="10"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtSearchDealer">*</asp:requiredfieldvalidator><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD style="HEIGHT: 26px"></TD>
								<TD style="HEIGHT: 26px"></TD>
								<TD style="HEIGHT: 26px"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD><asp:button id="btnSave" runat="server" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Text="Batal" Width="60px" CausesValidation="False"></asp:button><asp:button id="btnSearch" runat="server" Text="Cari" Width="59px" Visible="False" CausesValidation="False"></asp:button></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgUserOrgAssgnment" runat="server" Width="100%" AllowPaging="True" PageSize="25"
							AllowCustomPaging="True" AutoGenerateColumns="False" AllowSorting="True" BorderColor="#CDCDCD"
							CellSpacing="0" BorderWidth="1px" CellPadding="3" BorderStyle="Solid">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" BackColor="#FDC0C0"></SelectedItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Organisasi">
									<HeaderStyle ForeColor="White" Width="35%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDGDealerCode" runat="server" Text='<%# CType(Container.DataItem, UserOrgAssignment).Dealer.DealerCode %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Organisasi">
									<HeaderStyle ForeColor="White" Width="45%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDGDealerName" runat="server" Text='<%# CType(Container.DataItem, UserOrgAssignment).Dealer.DealerName %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnView" runat="server" Width="20px" CommandName="View" CausesValidation="False"
											Text="Lihat">
											<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" CommandName="Edit" CausesValidation="False"
											Text="Ubah">
											<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete" CausesValidation="False" Text="Hapus">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages" ForeColor="#4A3C8C" BackColor="#E7E7FF"></PagerStyle>
						</asp:datagrid>
						<asp:Button id="btnBack" runat="server" Text="Kembali" CausesValidation="False"></asp:Button></TD>
				</TR>
			</TABLE>
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
