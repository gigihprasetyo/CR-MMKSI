<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmClaimArrival.aspx.vb" Inherits="FrmClaimArrival" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmClaimArrival</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
			function ShowPPClaimSelectionOne() 
			{ 
				showPopUp('../SparePart/../PopUp/PopUpClaimSelectionOne.aspx?x=Territory','',500,760,ClaimSelection);
			} 
			
			function ClaimSelection(selectedClaim) 
			{	
				var tempParam =	selectedClaim.split(';'); 
				var txtClaimNo = document.getElementById("txtClaimNo"); 
				var txtDealerCode = document.getElementById("txtDealerCode");
				var txtDealerName = document.getElementById("txtDealerName");
				txtClaimNo.value = tempParam[0]; 
				txtDealerCode.value = tempParam[4];
				txtDealerName.value = tempParam[5];
			}
			
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">CLAIM - Penerimaan Barang</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<TD class="titleField" width="20%"><asp:label id="Label2" runat="server">No Claim</asp:label></TD>
					<td width="1%">
					:
					<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtClaimNo" onblur="omitSomeCharacter('txtClaimNo','<>?*%$;')"
							runat="server" MaxLength="50" Width="176px"></asp:textbox><asp:label id="lblSearchClaim" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>&nbsp;
						<asp:requiredfieldvalidator id="valName" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtClaimNo">*</asp:requiredfieldvalidator></TD>
				</tr>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="Label1" runat="server">Tanggal Claim</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD><asp:checkbox id="chkClaimDate" runat="server"></asp:checkbox></TD>
								<TD><cc1:inticalendar id="icClaimDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="Label5" runat="server">Kode Dealer</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"
							runat="server" MaxLength="50" Width="176px"></asp:textbox></TD>
				</TR>
				<tr>
					<TD class="titleField" width="20%"><asp:label id="Label6" runat="server">Nama Dealer</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDealerName" onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"
							runat="server" MaxLength="50" Width="296px"></asp:textbox></TD>
				</tr>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="Label3" runat="server">Tanggal Terima</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%">
						<TABLE id="Table4" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD>
									<asp:checkbox id="chkClaimArrival" runat="server"></asp:checkbox></TD>
								<TD><cc1:inticalendar id="icClaimArrival" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD class="titleField" style="HEIGHT: 63px" vAlign="top" width="20%"><asp:label id="Label4" runat="server"> Catatan</asp:label></TD>
					<td style="HEIGHT: 63px" width="1%">:</td>
					<TD style="HEIGHT: 63px" width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDescription" onblur="omitSomeCharacter('txtDescription','<>?*%$;')"
							runat="server" MaxLength="50" Width="408px" Height="60px" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" width="20%"></TD>
					<td width="1%"></td>
					<TD width="79%"><asp:button id="btnSearch" runat="server" Width="56px" Text="Cari"></asp:button><asp:button id="btnSave" runat="server" Text="Simpan" Enabled="False"></asp:button><asp:button id="btnCancel" tabIndex="70" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3"></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 210px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgClaimArrival" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ClaimDate" HeaderText="Tgl. Claim" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign=Center></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="ClaimNo" HeaderText="No. Claim">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<u>
												<asp:LinkButton id="lblClaimNo" runat="server" CommandName="ClaimNo" CausesValidation="False"></asp:LinkButton></u>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dealer">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ReceivedDate" SortExpression="ReceivedDate" HeaderText="Tanggal Kedatangan Barang"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign=Center></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
		<SCRIPT language="javascript">
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
		</SCRIPT>
	</body>
</HTML>
