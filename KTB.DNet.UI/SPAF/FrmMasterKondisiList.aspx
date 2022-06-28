<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMasterKondisiList.aspx.vb" Inherits="FrmMasterKondisiList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Upload PM</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		function showDelete(deleteValue)
		{
			var imagDel= document.getElementById("imageDel");
			imagDel.Alt= deleteValue;
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">LEASING - Daftar Kondisi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 40px" vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR id="tblInput" runat="server">
								<TD class="titleField" WIDTH="20%"><asp:label id="lblFileName" runat="server" ToolTip="Tipe, Tgl Berlaku, Harga Retail, SPAF, Subsidy">Lokasi File </asp:label></TD>
								<td WIDTH="1%">:</td>
								<TD colspan="2" WIDTH="79%"><INPUT onkeypress="return false;" id="dfMasterKondisi" style="WIDTH: 224px; HEIGHT: 20px"
										type="file" size="18" name="dfMasterKondisi" runat="server">
									<asp:button id="btnUpload" runat="server" Height="19px" Text="Upload" Width="70px"></asp:button><asp:button id="btnSimpan" runat="server" Height="19px" Text="Simpan" Width="70px"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%"><asp:label id="lblTipeDocument" runat="server">Tipe Dokumen</asp:label></TD>
								<TD width="1%">:</TD>
								<td WIDTH="79%"><asp:dropdownlist id="ddlDocumentType" Runat="server"></asp:dropdownlist></td>
							</TR>
							<tr>
								<td class="titleField">Tgl Berlaku</td>
								<TD width="1%">:</TD>
								<td>
									<TABLE width="50%" cellPadding="0" border="0">
										<TR>
											<td><asp:checkbox id="chkTglBerlaku" runat="server" Height="4px" AutoPostBack="True"></asp:checkbox></td>
											<td><cc1:inticalendar id="icTglBerlakuFrom" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
											<TD>&nbsp;s.d&nbsp;</TD>
											<TD><cc1:inticalendar id="icTglBerlakuTo" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
							<TR>
								<TD class="titleField"><asp:label id="lblTipe" runat="server">Tipe Kendaraan</asp:label></TD>
								<TD>:</TD>
								<td>
									<asp:ListBox id="ctlLstTipeKendaraan" runat="server" SelectionMode="Multiple"></asp:ListBox></td>
							</TR>
							<TR>
								<TD class="titleField">Status</TD>
								<TD>:</TD>
								<td><asp:dropdownlist id="ddlStatus" Width="124px" Runat="server"></asp:dropdownlist><asp:button id="btnSearch" runat="server" Height="19px" Text="Cari" Width="70px"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="HEIGHT: 340px; OVERFLOW: auto">
							<asp:datagrid id="dgMasterKondisi" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								PageSize="50" AllowPaging="True" AllowCustomPaging="True" GridLines="Vertical" CellPadding="3"
								BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="Gainsboro" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Type">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblType runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileTypeCode")%>' Width="53px">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Nama Kendaraan">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblVechileTypeDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>' >
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ValidFrom" HeaderText="Tgl Berlaku">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTglBerlaku runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ValidFrom", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RetailPrice" HeaderText="Harga Retail">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRetailPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetailPrice", "{0:#,##0}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SPAF" HeaderText="Nilai SPAF (%)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSPAF" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SPAF", "{0:#,##0.##}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Subsidi" HeaderText="SPAF per Unit (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSubsidi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Subsidi", "{0:#,##0}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Subsidi" HeaderText="PPh">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPh", "{0:#,##0}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Subsidi" HeaderText="SPAF setelah PPh (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AfterPPh", "{0:#,##0}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Subsidi" HeaderText="PPN">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PPn", "{0:#,##0}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ErrorMessage" HeaderText="Pesan">
										<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblMessage runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ErrorMessage") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:ImageButton id="lnkbtnDelete" runat="server" ToolTip="delete" ImageUrl="../images/trash.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete" CausesValidation="False">
											</asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="left" height="40"><asp:button id="btnDownload" runat="server" Text="Download" Width="60px" Enabled="False"></asp:button>&nbsp;&nbsp;</TD>
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
