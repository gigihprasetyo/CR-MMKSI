<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListPartIncidental.aspx.vb" Inherits="FrmListPartIncidental" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmListPartIncidental</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD colSpan="4">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">PERMINTAAN KHUSUS &nbsp;- Daftar Permintaan Khusus</td>
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
				<TR>
					<TD class="titleField" width="24%"><asp:label id="lblDealerCode" runat="server">Kode Dealer</asp:label></TD>
					<td width="1%"><asp:label id="Label2" runat="server">:</asp:label></td>
					<td width="40%"><asp:label id="lblDealerCodeValue" runat="server"></asp:label></td>
					<TD width="35%"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblDealerName" runat="server">Nama Dealer</asp:label></TD>
					<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblDealerNameValue" runat="server"></asp:label>/
						<asp:label id="lblDealerSerch" runat="server"></asp:label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblStatusEmail" runat="server">Status Email</asp:label></TD>
					<td><asp:label id="Label5" runat="server">:</asp:label></td>
					<TD><asp:dropdownlist id="ddlStatusEmail" runat="server" Width="120px"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 17px"></TD>
				</TR>
				<TR>
					<TD class="titleField">
						<asp:label id="Label4" runat="server">Status MKS</asp:label></TD>
					<TD>
						<asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD>
						<asp:dropdownlist id="ddlStatusKtb" runat="server" Width="120px"></asp:dropdownlist></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblTanggalInput" runat="server">Tanggal Pesanan</asp:label></TD>
					<td><asp:label id="Label3" runat="server">:</asp:label></td>
					<TD>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><cc1:inticalendar id="iccalFrom" runat="server"></cc1:inticalendar></td>
								<TD>&nbsp;<asp:label id="lblsd" runat="server">s/d</asp:label>&nbsp;</TD>
								<TD><cc1:inticalendar id="iccalTo" runat="server"></cc1:inticalendar></TD>
							</tr>
						</table>
					</TD>
					<TD><asp:button id="btnCari" runat="server" Width="50px" Text="Cari"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id="dtgListincidental" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True"
								AllowCustomPaging="True" PageSize="25" AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="id">
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('ChkExport',document.all.chkAllItems.checked)"
												type="checkbox">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="ChkExport" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="RequestNumber" SortExpression="RequestNumber" HeaderText="Nomor Permintaan ">
										<HeaderStyle ForeColor="White" Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Pesanan" 
 DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PoliceNumber" SortExpression="PoliceNumber" HeaderText="No Polisi">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WorkOrder" SortExpression="WorkOrder" HeaderText="WO">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DealerMailNumber" SortExpression="DealerMailNumber" HeaderText="Nomor Surat">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Status" SortExpression="Status" HeaderText="Status">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PIC" SortExpression="PIC" HeaderText="PIC">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="EmailStatus" HeaderText="Status Email">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatusEmail" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox4" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Status MMKSI">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="KTBStatus" HeaderText="Status MMKSI">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatusRemark" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox3" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete" Visible="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="TextBox5" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
					<TD style="WIDTH: 87px"></TD>
				</TR>
				<TR>
					<TD>
						<table>
							<tr>
								<td colSpan="2">Keterangan Status Email</td>
							</tr>
							<tr>
								<td><IMG src="../images/green.gif" border="0"></td>
								<td>Belum Dikirim
								</td>
							</tr>
							<tr>
								<td><IMG src="../images/red.gif" border="0"></td>
								<td>Sudah Dikirim
								</td>
							</tr>
						</table>
					</TD>
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
