<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OutStandingMO.aspx.vb" Inherits="OutStandingMO" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>OutStandingMO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="6">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">PO HARIAN - Daftar Sisa O/C</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" colSpan="6" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td colSpan="6" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="24%"><asp:label id="lblDealer" runat="server">Kode Dealer</asp:label></TD>
					<TD width="1%"><asp:label id="Label8" runat="server">:</asp:label></TD>
					<TD width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
							runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblJenisPesanan" runat="server">Jenis O/C</asp:label></TD>
					<TD><asp:label id="Label1" runat="server">:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlJenisPesanan" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD class="titleField"><asp:label id="lblNomorKontrak" runat="server">Nomor O/C</asp:label></TD>
					<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
					<TD><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtNomorKOntrak" onblur="alphaNumericPlusBlur(txtNomorKOntrak)"
							runat="server" Width="140px" MaxLength="10"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 15px"><asp:label id="Label5" runat="server">Periode O/C</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:label id="Label6" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:dropdownlist id="ddlPeriodeKontrak" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD class="titleField" style="HEIGHT: 15px"><asp:label id="lblNomerPk" runat="server">Nomor PK</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:label id="Label12" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event)" id="txtDealerPKNumber" onblur="alphaNumericPlusSpaceBlur(txtDealerPKNumber)"
							runat="server" Width="140px" MaxLength="40"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 14px"><asp:label id="lblKondisiPesanan" runat="server">Kondisi Pesanan</asp:label></TD>
					<TD style="HEIGHT: 14px"><asp:label id="Label13" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 14px"><asp:dropdownlist id="ddlKondisiPesanan" runat="server" Width="140px"></asp:dropdownlist></TD>
					<TD class="titleField" style="HEIGHT: 14px"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
					<TD style="HEIGHT: 14px">:</TD>
					<TD style="HEIGHT: 14px"><asp:dropdownlist id="ddlKategori" runat="server" Width="140px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 23px"><asp:label id="Label2" runat="server">Total Sisa Tebus VH (Rp.)</asp:label></TD>
					<TD style="HEIGHT: 23px"><asp:label id="Label3" runat="server">:</asp:label></TD>
					<TD style="HEIGHT: 23px">&nbsp;
						<asp:label id="Label9" runat="server" Font-Bold="True">Rp</asp:label>&nbsp;
						<asp:label id="lblTotalSisaTebusVH" runat="server" Font-Bold="True"></asp:label></TD>
					<TD style="HEIGHT: 23px"></TD>
					<TD style="HEIGHT: 23px"></TD>
					<TD style="HEIGHT: 23px"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label4" runat="server">Total Sisa Tebus PP (Rp.)</asp:label></TD>
					<TD><asp:label id="Label7" runat="server">:</asp:label></TD>
					<TD>&nbsp;
						<asp:label id="Label10" runat="server" Font-Bold="True">Rp</asp:label>&nbsp;
						<asp:label id="lblTotalSisaTebus" runat="server" Font-Bold="True"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD><asp:checkbox id="chbxSisaMOQty" runat="server" Text="sisa unit > 0"></asp:checkbox>&nbsp;<asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="6">
						<div id="div1" style="HEIGHT: 280px; OVERFLOW: auto"><asp:datagrid id="dtgContract" runat="server" Width="100%" ShowFooter="True" PageSize="25" AllowCustomPaging="True"
								AllowPaging="True" AllowSorting="True" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
								OnItemDataBound="dtgContract_itemdataBound" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
								<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ContractHeader.Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="ContractHeader.ContractNumber" HeaderText="Nomor O/C">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Tipe/Warna">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="ContractHeader.SPLNumber" HeaderText="Nomer Aplikasi">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton ID="lbtnSPLNumber" Runat="server" CommandName="DetailSPL"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ContractHeader.ProjectName" HeaderText="Nama Pesanan Khusus">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton ID="lbtnProjectName" Runat="server" CommandName="DetailProjectName"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="TargetQty" SortExpression="TargetQty" HeaderText="O/C Qty">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sisa O/C Qty">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Jumlah Sisa Tebus VH (Rp.)">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Jumlah Sisa Tebus PP (Rp.)">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD colSpan="7"><asp:button id="btnDownload" Text="Download" Runat="server" style="Z-INDEX: 0"></asp:button>
						<asp:button style="Z-INDEX: 0" id="btnKembali" Text="Kembali" Runat="server"></asp:button></TD>
				<TR>
					<TD colSpan="7"><asp:label id="lblPerhatian" runat="server" Font-Bold="True" Visible="False">Perhatian :</asp:label><asp:label id="lblDokumen" runat="server" Visible="False">Dokumen ini merupakan bagian yang tidak terpisahkan dari Perjanjian Jual Beli No.</asp:label><asp:label id="lblspaNumber" runat="server" Visible="False"></asp:label>&nbsp;
						<asp:label id="lblTanggal" runat="server" Visible="False">Tanggal</asp:label>&nbsp;
						<asp:label id="lblspaDate" runat="server" Visible="False"></asp:label></TD>
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
