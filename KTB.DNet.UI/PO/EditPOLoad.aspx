<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EditPOLoad.aspx.vb" Inherits="EditPOLoad" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EditPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		<form id="Form1" method="post" runat="server">
             <div runat="server" id="LoadTest">
            <table>
                <thead>
                    <tr>
                        <th>Info
                        </th>
                        <th></th>
                        <th>Value
                        </th>
                    </tr>
                </thead>
                <tbody>
                    
                    <tr>
                        <td>End Load Time</td>
                        <td><span>:</span></td>
                        <td>
                            <asp:Label ID="LbVal" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Start SAve</td>
                        <td><span>:</span></td>
                        <td>
                            <asp:Label ID="lblInit" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>End Save</td>
                        <td><span>:</span></td>
                        <td>
                            <asp:Label ID="lblSave" runat="server"></asp:Label>
                            <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="50000">
                            </asp:Timer>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PO Harian -&nbsp; Edit PO&nbsp;</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField"><asp:label id="Label1" runat="server">Kode Dealer</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblDealerCode" runat="server"></asp:label>&nbsp;/
									<asp:Label id="lblSearchTerm1" runat="server"></asp:Label></TD>
								<TD class="titleField"><asp:label id="label66" runat="server">Kota</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblCity" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label5" runat="server">Nama Dealer</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblName" runat="server"></asp:label></TD>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
								<TD class="titleField"><asp:label id="Label6" runat="server">Nomor O/C</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblContractNumber" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor Reg PO</TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblNoPO" runat="server"></asp:label></TD>
								<TD class="titleField"><asp:label id="Order" runat="server">Jenis O/C</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblOrderType" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label7" runat="server"> Nomor PO</asp:label></TD>
								<TD>:</TD>
								<TD><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event)" onblur="alphaNumericPlusSpaceBlur(txtDealerPONumber)"
										id="txtDealerPONumber" runat="server" MaxLength="20"></asp:textbox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Nomor PO Tidak Boleh Kosong"
										ControlToValidate="txtDealerPONumber">*</asp:RequiredFieldValidator></TD>
								<TD class="titleField"><asp:label id="Label8" runat="server"> Kategori</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblSalesOrg" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px">
									<asp:label id="lblFactoring" runat="server">Factoring</asp:label></TD>
								<TD style="HEIGHT: 22px">
									<asp:label id="lblFactoringColon" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 22px">
									<asp:CheckBox id="chkFactoring" runat="server" Enabled="False" Text=" " Width="16px" AutoPostBack="True"></asp:CheckBox></TD>
								<TD class="titleField" style="HEIGHT: 22px"></TD>
								<TD style="HEIGHT: 22px"></TD>
								<TD style="HEIGHT: 22px"></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label4" runat="server"> Cara Pembayaran</asp:label></TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlTermOfPayment" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField"><asp:label id="Label11" runat="server">Tahun Perakitan/Impor</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblProductYear" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="label24" runat="server">Jenis Order</asp:label></TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlOrderType" runat="server" Enabled="False"></asp:dropdownlist></TD>
								<TD class="titleField"><asp:label id="Label12" runat="server">Nama Pesanan Khusus</asp:label></TD>
								<TD>:</TD>
								<TD><asp:label id="lblProjectName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server"> Permintaan Kirim</asp:label></TD>
								<TD>:</TD>
								<TD><cc1:inticalendar id="icPermintaanKirim" runat="server"></cc1:inticalendar></TD>
								<TD class="titleField"><asp:label id="Total" runat="server"> Total Harga Tebus</asp:label></TD>
								<TD>
									<asp:Label id="Label10" runat="server">:</asp:Label></TD>
								<TD><b> </b>
									<asp:label id="Label9" runat="server" Font-Bold="True">Rp</asp:label>&nbsp;
									<asp:label id="lblTotal" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label3" runat="server" Font-Bold="True">Tanggal Jatuh Tempo</asp:label></TD>
								<TD>:</TD>
								<TD>
									<asp:label id="lblJatuhTempo" runat="server"></asp:label></TD>
								<TD>
									<asp:CheckBox id="chkFreePPh" runat="server" Font-Bold="True" Width="19px" Text=" " Enabled="False"></asp:CheckBox><STRONG>Bebas 
										PPh22</STRONG></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR style="VISIBILITY:visible">
								<TD>Av Ceiling</TD>
								<TD></TD>
								<TD>
									<asp:Label id="lblF1" runat="server" Width="144px">0</asp:Label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR style="VISIBILITY:visible">
								<TD>Last PO</TD>
								<TD></TD>
								<TD>
									<asp:Label id="lblF2" runat="server" Width="144px">0</asp:Label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR style="VISIBILITY:visible">
								<TD>Curr PO</TD>
								<TD></TD>
								<TD>
									<asp:Label id="lblF3" runat="server" Width="144px">0</asp:Label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dtgDetail" runat="server" CellSpacing="1" CellPadding="3" BorderWidth="0px"
								BorderStyle="None" BorderColor="#CDCDCD" BackColor="#CDCDCD" OnItemDataBound="dtgDetail_ItemDataBound" AutoGenerateColumns="False" Width="100%"
								ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#FFFFCC" VerticalAlign="Middle"
									BackColor="#990000"></HeaderStyle>
								<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Kode Tipe / Warna">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Model / Tipe / Warna">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Sisa O/C (unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Order (unit)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<ItemTemplate>
											<asp:TextBox id="TextBox1" runat="server" Width="54px" CssClass="textRight">0</asp:TextBox>
											<asp:RangeValidator id="RangeValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Permintaan Melebihi Sisa Unit"
												Type="Integer" MinimumValue="0" MaximumValue="10000">*</asp:RangeValidator>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Order Unit tidak boleh Kosong">*</asp:RequiredFieldValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Jaminan (Rp)">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Harga Unit (Rp)">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="PPH 22 (Rp)">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Interest (Rp)">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
							</asp:datagrid></div>
						<asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></TD>
				</TR>
				<TR>
					<TD vAlign="top" class="titleField"></TD>
				</TR>
				<TR>
					<TD height="60"><asp:button id="btnKirim" runat="server" Text="Simpan" Enabled="False" Visible="False"></asp:button><asp:button id="btnHitung" runat="server" Text="Hitung" Enabled="False" Visible="False"></asp:button><asp:button id="btnBatal" runat="server" Text="Hapus" CausesValidation="False" Enabled="False" Visible="False"></asp:button>
						<asp:Button ID="btnBack" Runat="server" Text="Kembali" Enabled="False" Visible="False"></asp:Button>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				//if (!navigator.appName=="Microsoft Internet Explorer")
				//{
				//  self.opener = null;
				//  self.close();
				//}
				//else
				//{
				//   this.name = "origWin";
				//   origWin= window.open(window.location, "origWin");
				//   window.opener = top;
                //   window.close();
				//}
			}	
		</script>
	</body>
</HTML>
