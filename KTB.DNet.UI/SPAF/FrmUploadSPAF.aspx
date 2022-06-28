<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadSPAF.aspx.vb" Inherits="FrmUploadSPAF" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmUploadText</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">
						UPLOAD&nbsp;-
						<asp:literal id="ltrTitle" runat="server"></asp:literal></TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Leasing</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="ltrCompanyName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Lokasi File</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><input id="fuUpload" type="file" runat="server">
									<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>
									<asp:Label id="lblUploadInfo" runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 380px"><asp:datagrid id="dgUploadData" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD"
								BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" AllowSorting="True">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn ReadOnly="True" HeaderText="No">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ReffLetter" HeaderText="No Kontrak" SortExpression="ReffLetter">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="DateLetter" HeaderText="Tgl Kontrak">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblLetterDate" runat="server" Text='<%# iif(Format(DataBinder.Eval(Container, "DataItem.DateLetter"),"dd/MM/yyyy")= "01/01/1753","",Format(DataBinder.Eval(Container, "DataItem.DateLetter"),"dd/MM/yyyy"))  %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="CustomerName" HeaderText="Nama Pelanggan" SortExpression="CustomerName">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DealerLeasing" HeaderText="Dealer Leasing" SortExpression="DealerLeasing">
										<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn  HeaderText="No Rangka">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNoChassis" runat="server" Text=''>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Dealer">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="RetailPrice" HeaderText="Harga Retail (Rp)">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblRetailPrice" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.RetailPrice"),"#,###0") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="SPAF" HeaderText="Assistance Fee (Rp)">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblAssistanceFee" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.SPAF"),"#,###0") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SellingType" HeaderText="Selling Type">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSellingType" runat="server" Text='<%# CType(DataBinder.Eval(Container, "DataItem.SellingType"), KTB.DNet.Domain.EnumSellingType.SellingType).ToString() %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Subsidi">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSubsidi" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Subsidi"),"#,###0") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Image ID="imgActive" Runat="server" ImageUrl='<%# Iif(DataBinder.Eval(Container, "DataItem.ErrorMessage") = "", "../images/aktif.gif", "../images/in-aktif.gif") %>'>
											</asp:Image>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Keterangan">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblMessage" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Button id="btnSimpan" runat="server" Text="Simpan"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
