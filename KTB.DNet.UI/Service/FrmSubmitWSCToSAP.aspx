<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSubmitWSCToSAP.aspx.vb" Inherits="FrmSubmitWSCToSAP" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSubmitWSCToSAP</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
			function userConfirm()
			{
				var a= confirm('Anda yakin mendownload data ini ?');
				
				return a;
			}
			
			function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtKodeDealer");
				txtDealer.value = selectedCode;
				txtDealer.focus();
			}
			
		</script>
	</HEAD>
	<body onfocus="return checkModal();" onclick="checkModal();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">WSC - Transfer WSC ke SAP
					</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
							<TR>
								<TD class="titleField" width="30%">Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:textbox id="txtKodeDealer" runat="server" Width="256px"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="30%">Tanggal Rilis WSC</TD>
								<TD width="1%">:</TD>
								<TD width="69%" align="left">
									<table border="0" cellPadding="0">
										<tr>
											<td align="left"><cc1:inticalendar id="icCreatedDateStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>s.d.</td>
											<td><cc1:inticalendar id="icCreatedDateEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="30%">Type WSC</TD>
								<TD width="1%">:</TD>
								<TD width="69%"><asp:dropdownlist id="ddlType" runat="server" Width="100px" AutoPostBack="False"></asp:dropdownlist></TD>
							</TR>
							<TR style="DISPLAY: none">
								<TD class="titleField" width="30%">Produk</TD>
								<TD width="1%">:</TD>
								<TD width="69%"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD><asp:checkbox id="chkdownload" runat="server" Text="Download Ulang"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;<asp:button id="btnFind" runat="server" Width="64px" Text="Cari"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div style="HEIGHT: 340px; OVERFLOW: auto" id="div1"><asp:datagrid id="dtgWSCInfo" runat="server" Width="100%" AllowSorting="True" PageSize="50" AllowPaging="True"
								AllowCustomPaging="True" CellPadding="3" BorderWidth="0px" CellSpacing="1" BorderColor="#CDCDCD" BackColor="#CDCDCD" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<FooterStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#F6F6F6"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server">No</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text='<%# Container.DataItem.DealerCode %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="35%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerName" runat="server" Text='<%# Container.DataItem.DealerName %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CityName" HeaderText="Kota">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKota" runat="server" Text='<%# Container.DataItem.CityName %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalText" runat="server" Font-Bold="True" Text="Total : "></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="WSCCount" HeaderText="Total WSC">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTotalWSC runat="server" Text="<%# Container.DataItem.WSCCount %>">0</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<asp:Label id="lblSumOfTotalWSC" runat="server">0</asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<br>
						<STRONG>Kategori:</STRONG>
						<asp:dropdownlist style="Z-INDEX: 0" id="ddlCategory" runat="server" Width="140px"></asp:dropdownlist><asp:button id="btnDownload" runat="server" Width="60px" Text="Download"></asp:button><asp:textbox id="txtDownload" runat="server"></asp:textbox></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			document.getElementById("txtDownload").style.visibility="hidden";
			
			if (document.getElementById("txtDownload").value != "")
			{
				var downloadURL = document.getElementById("txtDownload").value
				document.getElementById("txtDownload").value = ""
				document.location.href="../downloadlocal.aspx?file="+downloadURL;				
			}
		</script>
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
