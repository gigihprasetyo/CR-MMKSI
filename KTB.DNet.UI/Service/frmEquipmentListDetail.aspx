<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmEquipmentListDetail.aspx.vb" Inherits="frmEquipmentListDetail" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>frmEquipmentListDetail</title> 
		<!--smartNavigation="True" "rs-edit" -->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript">
		//function Back()
		//{
		//var hidden = document.getElementById("Hidden1")
		//var i = hidden.value * -1
		//window.history.go(i);
		//}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="3">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">EQUIPMENT REPAIR - Daftar Equipment Detail</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="24%"><asp:label id="Label1" runat="server">Kode Equipment</asp:label></TD>
					<TD width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
					<TD width="75%"><asp:label id="lblEquipmentNumber" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label3" runat="server">Nama Equipment</asp:label></TD>
					<TD><asp:label id="Label5" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblDescription" runat="server"></asp:label></TD>
				</TR>
				<TR id="TRPhoto" runat="server" valign="top">
					<TD class="titleField"><asp:label id="Label4" runat="server">Photo</asp:label></TD>
					<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD><asp:image id="imgPhoto" runat="server"></asp:image></TD>
				</TR>
				<TR id="TRDeletePhoto" runat="server">
					<TD></TD>
					<TD></TD>
					<TD><asp:checkbox id="chkDeletePhoto" runat="server" AutoPostBack="True" Text="Hapus Photo" EnableViewState="False"></asp:checkbox></TD>
				</TR>
				<TR id="TRUploadPhoto" runat="server">
					<TD class="titleField"><asp:label id="Label7" runat="server">Pilih Lokasi Photo</asp:label></TD>
					<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
					<TD><INPUT id="filePhoto" type="file" name="File1" runat="server"></TD>
				</TR>
				<TR id="TRSpecification" vAlign="top" runat="server">
					<TD class="titleField"><asp:label id="Label8" runat="server">Spesifikasi</asp:label></TD>
					<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
					<TD><asp:textbox id="txtSpecification" runat="server" Height="136px" ReadOnly="True" MaxLength="500"
							TextMode="MultiLine" Width="384px"></asp:textbox></TD>
				</TR>
				<TR id="OpStatus" runat="server">
					<TD class="titleField">Status</TD>
					<TD><asp:label id="Label17" runat="server">:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlStatus" runat="server">
							<asp:ListItem Value="0">Aktif</asp:ListItem>
							<asp:ListItem Value="1">Tidak Aktif</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label13" runat="server" Width="152px">Perubahan Terakhir Oleh</asp:label></TD>
					<TD><asp:label id="Label14" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblLastUpdateBy" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label15" runat="server" Width="176px">Tanggal Perubahan Terakhir</asp:label></TD>
					<TD><asp:label id="Label16" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblLastUpdateTime" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD><asp:button id="btnSave" runat="server" Text="Simpan" Width="65px"></asp:button><asp:Button ID="btnBack" Runat="server" Text="Kembali"></asp:Button></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 200px"><asp:datagrid id="dgDetail" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
								BackColor="#CDCDCD" BorderColor="#CDCDCD" OnItemDataBound="ComputeSum" ShowFooter="True" AutoGenerateColumns="False">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" BackColor="Silver"></HeaderStyle>
								<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<%# container.itemindex+1 %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="EquipmentMaster.EquipmentNumber" HeaderText="Kode Equipment">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="EquipmentMaster.Description" HeaderText="Nama Equipment">
										<HeaderStyle ForeColor="White" Width="25%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Quantity" SortExpression="Quantity" HeaderText="Jumlah">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Harga" Visible=False >
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></div>
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
