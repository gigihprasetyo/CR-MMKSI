<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFreeServisViaTextBB.aspx.vb" Inherits="FrmFreeServisBBViaTextBB" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Upload Free Service</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script>
		
		
			
			function checkFSPeriod()
			{
				var d = new Date('<%= Now.toString("yyyy/M/d HH:mm:ss") %>');
				var a = new Date("2013/1/1 00:00:00");
				var b = new Date("2013/1/11 00:00:00");
				var str="";

				str=str+"<table width='100%'>";
				str=str+"	<tr height='100px'>";
				str=str+"		<td></td>";
				str=str+"	</tr>";
				str=str+"	<tr height='200px'>";
				str=str+"		<td align='center'>";
				str=str+"		Sehubungan dengan adanya perubahan proses pengiriman data kupon Free Service,<br>maka untuk sementara menu ini tidak dapat diakses mulai tanggal 1 Januari 2013 s/d 10 Januari 2013	";
				str=str+"		</td>";
				str=str+"	</tr>";
				str=str+"	<tr>";
				str=str+"		<td></td>";
				str=str+"	</tr>";
				str=str+"</table>";			

				//alert(d);
				if(d.valueOf()>a.valueOf())
				{
					if(d.valueOf()<b.valueOf())
					{
						//alert(str);
						document.write(str);
					}
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="checkFSPeriod();">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FREE SERVICE - Upload Free Service Special</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left" style="HEIGHT: 40px">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblDealer" runat="server">Kode Dealer </asp:label></TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:label id="lblDealerCode" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px">Nama Dealer</TD>
								<TD style="HEIGHT: 19px">:</TD>
								<td style="HEIGHT: 19px"><asp:label id="lblDealerName" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblFileName" runat="server" ToolTip="KodeDealer, No.Rangka, Jenis FS, Tgl. Service (ddmmyyyy), Tgl. Jual (ddmmyyyy), JarakTempuh">File dengan pemisah koma (,)</asp:label></TD>
								<td>:</td>
								<TD><INPUT id="dfChassis" type="file" size="34" name="File1" runat="server" onkeypress="return false;"/>
									<asp:button id="btnUpload" runat="server" Width="70px" Text="Upload" Height="19px"></asp:button>
                                    <br /><asp:LinkButton ID="lnkTemplate" runat="server" Text="Download Template" /></TD>
							</TR>
						</TABLE>
						<asp:label id="Label2" runat="server">  &nbsp;</asp:label></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="HEIGHT: 380px; OVERFLOW: auto"><asp:datagrid id="dgFreeServisBBUpload" runat="server" Width="100%" AutoGenerateColumns="False"
								BorderColor="Gainsboro" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical" PageSize="50" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Kode Dealer ">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' Width="53px">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Kode Cabang Dealer ">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealerBranchCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode") %>' Width="53px">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No. Rangka ">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblChassisNo" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jenis Free Service">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblFSKind runat="server" Width="120px" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.KindDescription") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl. Free Service">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTglFS runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tgl. Penjualan">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTglJual runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SoldDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jarak Tempuh (km)">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblMileage runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MileAge", "{0:#,###}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tipe Visit">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblTipeVisit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VisitType") %>'>
											</asp:Label> 
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Nomor WO">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblWONumber runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber") %>'>
											</asp:Label> 
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pesan">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblMessage runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ErrorMessage") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="left" height="40"><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button>&nbsp;&nbsp;</TD>
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
