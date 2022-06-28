<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadSPItemSpecial.aspx.vb" Inherits="FrmUploadSPItemSpecial" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">SPECIAL ITEM - Upload Spesial Item</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table4" cellSpacing="1" cellPadding="2" border="0">
							<TR>
								<TD class="titleField" width="24%">Periode</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:label id="lblPeriod" runat="server"></asp:label></TD>
								<TD width="50%"><asp:label id="lblHeaderErr" runat="server" ForeColor="Red" Width="368px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Referensi</TD>
								<TD>:</TD>
								<TD><asp:label id="lblReference" runat="server"></asp:label></TD>
								<TD><asp:label id="lblSIExists" runat="server" ForeColor="Red" Width="368px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Catatan</TD>
								<TD>:</TD>
								<TD><asp:label id="lblRemark" runat="server"></asp:label></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 26px">Upload File</TD>
								<TD style="HEIGHT: 26px">:</TD>
								<TD style="HEIGHT: 26px"><INPUT onkeypress="return false;" id="DataFile" type="file" size="29" name="File1" runat="server"></TD>
								<TD style="HEIGHT: 26px"><asp:button id="btnUpload" runat="server" Width="70px" Text="Upload"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;</TD>
							<TR>
								<TD class="titleField">Copy File</TD>
								<TD>:</TD>
								<TD><INPUT onkeypress="return false;" id="CopyFile" type="file" size="29" name="File1" runat="server">&nbsp;</TD>
								<TD><asp:button id="btnCopy" runat="server" Width="70px" Text="Copy" Enabled="False"></asp:button><asp:label id="lblRefError" runat="server" ForeColor="Red" Width="295px" Visible="False">Nomor Ref tidak valid (NomorRev-Versi)</asp:label>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dgSpecialItem" runat="server" Width="760px" PageSize="25" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BorderColor="Gainsboro" BackColor="Gainsboro" BorderWidth="0px" CellPadding="3" CellSpacing="1"
								AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Text="No"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPartNo" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartName" HeaderText="Nama Barang">
										<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPartName" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ModelCode" HeaderText="Model">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblModel" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ExtMaterialGroup" HeaderText="Group">
										<HeaderStyle Width="12%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblGroup" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left">
						<TABLE id="Table5" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="50%" height="40"><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button>&nbsp;
									<asp:label id="lblDataErr" runat="server" ForeColor="Red" Width="88px"></asp:label></TD>
								<TD align="right" width="50%"><asp:label id="Label3" runat="server" ForeColor="#FFFFC0" BackColor="#FFFFC0">_____</asp:label>&nbsp;
									<asp:label id="Label4" runat="server">Barang Baru</asp:label><asp:label id="Label6" runat="server" ForeColor="#C0FFC0" BackColor="#C0FFC0">_____</asp:label><asp:label id="Label5" runat="server">Harga Baru</asp:label></TD>
							</TR>
						</TABLE>
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
