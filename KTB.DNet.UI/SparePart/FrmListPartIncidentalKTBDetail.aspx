<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListPartIncidentalKTBDetail.aspx.vb" Inherits="FrmListPartIncidentalKTBDetail" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListPartIncidentalKTBDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
        //function Back()
		//{
			//window.history.go(-1);
		//}
	 
			
			function GetCurrentInputIndex()
			{
				var dgPartListDetail = document.getElementById("dgPartListDetail");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dgPartListDetail.rows.length; index++)
				{
					inputs = dgPartListDetail.rows[index].getElementsByTagName("INPUT");
					
					if (inputs != null && inputs.length > 0)
					{
						for (indexInput = 0; indexInput < inputs.length; indexInput++)
						{	
							if (inputs[indexInput].type != "hidden")
							   
								return index;
						}
					}
				}				
				return -1;
			}
			
			function SparePart(selectedType)
			{
			 
				var tempParam = selectedType.split(';');
				//var indek = GetCurrentInputIndex();
				var indek = tempParam[3] * 1 + 1;
				var dgPartListDetail = document.getElementById("dgPartListDetail");
				var KodeTipe = dgPartListDetail.rows[indek].getElementsByTagName("INPUT")[0];
				 
				KodeTipe.value =  tempParam[0];
				 
			 
			}		
			
		</script>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="6">
						<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titlePage">PERMINTAAN KHUSUS - Daftar Pengajuan Khusus dari Dealer</TD>
							</TR>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="24%"><asp:label id="Label2" runat="server"> Kode Dealer</asp:label></TD>
					<TD width="1%"><asp:label id="Label7" runat="server">:</asp:label></TD>
					<TD width="30%"><asp:label id="lblKodeDealer" runat="server"></asp:label></TD>
					<TD width="15%"></TD>
					<TD width="1%"></TD>
					<TD width="29%"></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label15" runat="server">Nama Dealer</asp:label></TD>
					<TD><asp:label id="Label8" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblNama" runat="server"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label1" runat="server">Nomor Permintaan</asp:label></TD>
					<TD><asp:label id="Label20" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblNomorPermintaan" runat="server"></asp:label></TD>
					<TD><asp:label id="lblNomorSurat" runat="server" Font-Bold="True">Nomor Surat</asp:label></TD>
					<TD><asp:label id="Label23" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblNomorSuratValue" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label3" runat="server">Tanggal Pesanan</asp:label></TD>
					<TD><asp:label id="Label9" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblIncidentalDate" runat="server"></asp:label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="Label4" runat="server">Nomor Polisi</asp:label></TD>
					<TD><asp:label id="Label10" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblPoliceNumber" runat="server"></asp:label></TD>
					<TD class="titleField"><asp:label id="Label13" runat="server">W/O</asp:label></TD>
					<TD><asp:label id="Label14" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblWorkOrder" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblNoRangka" runat="server" Width="112px">Nomor Rangka</asp:label></TD>
					<TD><asp:label id="Label16" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblNoRangkaValue" runat="server"></asp:label></TD>
					<TD><asp:label id="Label5" runat="server" Font-Bold="True">Status</asp:label></TD>
					<TD><asp:label id="Label11" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblStatus" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblTipe" runat="server" Width="112px">Tipe</asp:label></TD>
					<TD><asp:label id="Label17" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblTipeValue" runat="server"></asp:label></TD>
					<TD><asp:label id="Label6" runat="server" Font-Bold="True">PIC</asp:label></TD>
					<TD><asp:label id="Label12" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblPIC" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"><asp:label id="lblTahunProduksi" runat="server" Width="112px">Tahun Produksi</asp:label></TD>
					<TD><asp:label id="Label18" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblTahunProduksiValue" runat="server"></asp:label></TD>
					<TD><asp:label id="lblTelp" runat="server" Font-Bold="True">Telp</asp:label></TD>
					<TD><asp:label id="Label19" runat="server">:</asp:label></TD>
					<TD><asp:label id="lblTelpValue" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField"></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titleField"></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="6"><asp:datagrid id="dgPartListDetail" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
							CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD">
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<Columns>
								<asp:TemplateColumn Visible="False" HeaderText="ID">
									<ItemTemplate>
										<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<%# container.itemindex+1 %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn HeaderText="Nomor Barang">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Nama Barang">
									<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Model">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Quantity" HeaderText="Jumlah">
									<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Keterangan">
									<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="txtRemark" Width="90%" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remark") %>' MaxLength="100" TextMode="MultiLine">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Substitution">
									<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle Wrap="False" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="txtPartSubstitute" runat="server" Width="67px"   ></asp:TextBox>
										<asp:Label id="lblPopUp" tabIndex="20" runat="server" height="10px">
											<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
												border="0"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Plan Date(dd/mm/yyyy)">
									<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="txtPlanDate" runat="server" Width="75px"></asp:TextBox>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox3" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="cbReject" runat="server"></asp:CheckBox>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox4" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnSave" runat="server" CommandName="Save">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 136px" colSpan="6">
						<TABLE id="Table2" style="WIDTH: 160px; HEIGHT: 32px" cellSpacing="1" cellPadding="1" width="160"
							border="0">
							<TR>
								<TD><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button></TD>
								<TD><asp:button id="btnBack" runat="server" Width="60px" Text="Kembali"></asp:button></TD>
								<TD style="WIDTH: 115px"><asp:button id="btnCetak1" runat="server" Width="60px" Text="Cetak"></asp:button></TD>
								<TD><asp:button id="btnPenyelesaian" runat="server" Width="128px" Text="Penyelesaian Proses"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<SCRIPT language="javascript">
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
		</SCRIPT>
	</BODY>
</HTML>
