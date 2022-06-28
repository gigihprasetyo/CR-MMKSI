<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSAPSalesmanValidation.aspx.vb" Inherits="FrmSAPSalesmanValidation" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSAPSalesmanValidation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
			function ShowPPSAP()
			{
				showPopUp('../SparePart/../PopUp/PopUpSAP.aspx?x=Territory','',500,760,SAPSelection);
			}
			
			function SAPSelection(selectedSAP)
			{
				var tempParam= selectedSAP.split(';');
				
				var txtSAPNo = document.getElementById("txtSAPNo");
				var lblDateFrom = document.getElementById("lblDateFrom");
				var lblDateUntil = document.getElementById("lblDateUntil");
						
				txtSAPNo.value= tempParam[0];
				lblDateFrom.value =tempParam[1];
				lblDateUntil.value =tempParam[2];
				
				
			}
			
		function GetSelectedItem()
			{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgSAPSalesmanValidation");
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					bcheck=true;
				}
			}
			if (bcheck)
			  {
				return true;
			  }
			else
			  {
				alert("Silahkan Pilih Data terlebih dahulu");	
				return false;
			  }
			  }
			  
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="6">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">SAP - Validasi</td>
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
				<TR valign=top>
					<TD class="titleField" width="10%">SAP No</TD>
					<td class="titleField" width="1%">:</td>
					<TD width="20%"><asp:textbox id="txtSAPNo" runat="server" Width="120px"></asp:textbox><asp:label id="lblSearchSAP" runat="server"> <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					<td class="titleField" width="20%">Periode SAP</td>
					<td class="titleField" width="1%">:</td>
					<td width="30%"><table border=0 cellpadding=0 cellspacing=0>
							<tr>
								<td><asp:textbox id="lblDateFrom" Width="60px" Wrap="True" BorderWidth="0" ReadOnly="True" BorderStyle="None"
										Runat="server"></asp:textbox></td>
								<td class="titleField">s/d
								</td>
								<td><asp:textbox id="lblDateUntil" Width="60px" BorderWidth="0" ReadOnly="True" BorderStyle="None"
										Runat="server"></asp:textbox></td>
							</tr>
						</table></td>
				</TR>
				<TR>
					<TD class="titleField" width="10%">Kode Dealer</TD>
					<td class="titleField" width="1%">:</td>
					<TD><asp:label id="lblDealerCode" Runat="server"></asp:label></TD>
					<TD class="titleField" width="20%">Salesman ID</TD>
					<td class="titleField" width="1%">:</td>
					<td width="30%"><asp:textbox id="txtSalesCode" Runat="server"></asp:textbox></td>
				</TR>
				<TR>
					<TD class="titleField" width="20%">Kategori</TD>
					<td class="titleField" width="1%">:</td>
					<TD width="30%"><asp:dropdownlist id="ddlcategori" Runat="server"></asp:dropdownlist></TD>
					<td class="titleField" width="20%">Salesman Name</td>
					<td class="titleField" width="1%">:</td>
					<td width="30%"><asp:textbox id="txtSalesName" Runat="server"></asp:textbox></td>
				</TR>
				<TR>
					<TD></TD>
					<td></td>
					<TD><asp:button id="btnSearch" runat="server" Width="96px" Text="Cari"></asp:button>&nbsp;</TD>
					<TD colSpan="3"></TD>
				</TR>
				<TR>
				<TR>
					<TD colSpan="6">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgSAPSalesmanValidation" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Salesman">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridSalesName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Salesman ID">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridSalesCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kategori">
										<HeaderStyle Width="30%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblGridCategori" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<tr>
					<td width="10%"><asp:button id="btnProcess" Width="104px" Runat="server" Text="Batal"></asp:button></td>
					<td width="1%"></td>
					<td colSpan="4"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
