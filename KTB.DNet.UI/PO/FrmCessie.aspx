<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCessie.aspx.vb" Inherits="FrmCessie" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFactoring</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindowx.js"></script>
		<script language="javascript" src="../WebResources/InputValidationx.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
		
		<script language="javascript">
			function ShowDownloadCessie(IdCessie)
			{
				var str=IdCessie;
				showPopUp('../PopUp/PopUpDownloadCessie.aspx?IdCessie=' +IdCessie,'',200,400,AccountSelection);

			}
			
			function ShowDownloadCessieFile(filename,filetype)
			{
				var str=filename;
				alert(str);
				showPopUp('../PopUp/PopUpDownloadCessieFile.aspx?FileName=' +filename +'&FileType='+ filetype,'',50,50,AccountSelection);
			}
			
			function ShowSalesmanResign()
			{
			
			showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?Code=Resign' +'&Mode='+ txtMode.value,'',500,760,SalesmanResignSelection);
			}
			function AccountSelection(selectedAccount)
			{
				var txtCreditAccount = document.getElementById("txtCreditAccount");
				var txtDealerName = document.getElementById("txtDealerName");
				
				var str = selectedAccount.split(";");
				txtCreditAccount.value = str[0];			
				txtDealerName.value=str[1];
			}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage"><asp:label style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" id="Label2" runat="server"
							Font-Bold="True"></asp:label>FACTORING - Daftar Cessie</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<tr>
					<td>
						<table border="0" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td style="WIDTH: 85px"><b>Tanggal Cessie</b></td>
								<td style="WIDTH: 4px">:</td>
								<td style="WIDTH: 360px">
									<table border="0" cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td><cc1:inticalendar id="calStart" runat="server"></cc1:inticalendar></td>
											<td>&nbsp;s.d&nbsp;</td>
											<td><cc1:inticalendar id="calEnd" runat="server"></cc1:inticalendar></td>
										</tr>
									</table>
								</td>
								<td style="WIDTH: 258px"><STRONG>Total Nilai Piutang</STRONG></td>
								<td style="WIDTH: 8px">:</td>
								<td style="WIDTH: 127px" align="right">Rp. <asp:label id="lblTotalPiutang" runat="server" Font-Bold="True">0</asp:label></td>
								<TD></TD>
							</tr>
							<tr>
								<td style="WIDTH: 85px"><b>No Cessie</b></td>
								<td style="WIDTH: 4px">:</td>
								<td style="WIDTH: 360px"><asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" id="txtCreditAccount" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
										runat="server"></asp:textbox></td>
								<td style="WIDTH: 258px"><STRONG>Total Nilai Pembelian</STRONG></td>
								<td style="WIDTH: 8px">:</td>
								<td style="WIDTH: 127px" align="right">Rp. <asp:label id="lblTotalPurchase" runat="server" Font-Bold="True">0</asp:label></td>
								<TD></TD>
							</tr>
							<tr>
								<td style="WIDTH: 85px"><b>Produk</b></td>
								<td style="WIDTH: 4px">:</td>
								<td style="WIDTH: 360px" align="left">
									<asp:DropDownList Runat="server" ID="ddlProductCategory" Width="145px" ></asp:DropDownList>
								</td>
								<td style="WIDTH: 258px"><STRONG>Total Nilai Pembayaran</STRONG></td>
								<td style="WIDTH: 8px">:</td>
								<td style="WIDTH: 127px" align="right">Rp. <asp:label id="lblTotalPayment" runat="server" Font-Bold="True">0</asp:label></td>
								<TD align="right"></TD>
							</tr>
							<tr>
								<td style="WIDTH: 85px"></td>
								<td style="WIDTH: 4px"></td>
								<td style="WIDTH: 360px" align="left">
									<asp:button id="btnFind" runat="server" Text="Cari" Width="60px"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnUpload" runat="server" Text="Transfer Ke SAP" Width="112px"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnDownload" runat="server" Text="Download" Width="100px"></asp:button>
								</td>
								<td style="WIDTH: 258px"></td>
								<td style="WIDTH: 8px"></td>
								<td style="WIDTH: 127px" align="right"></td>
								<TD align="right"></TD>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<div style="OVERFLOW: auto; HEIGHT: 360px" id="divHidden">
							<asp:datagrid id="dtgMain" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
								CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
								AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
								DataKeyField="ID" ShowFooter="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="1px" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="" >
										<HeaderStyle ForeColor="White" Width="1px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkExport',document.all.chkAllItems.checked)">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkExport" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No" >
										<HeaderStyle ForeColor="White" Width="1px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblNo"><%# Container.ItemIndex+1 %></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:label Runat="server" ID="lblNoE"><%# Container.ItemIndex+1 %></asp:label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No Cessie">
										<HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label1"><%# DataBinder.Eval(Container.DataItem,"CessieNumber") %></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:label Runat="server" ID="Label3"><%# DataBinder.Eval(Container.DataItem,"CessieNumber") %></asp:label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Produk">
										<HeaderStyle ForeColor="White" Width="60px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblProductCategory"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:label Runat="server" ID="lblProductCategoryEdit"></asp:label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Cessie">
										<HeaderStyle ForeColor="White" Width="60px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label4"><%# String.Format("{0:d}",DataBinder.Eval(Container.DataItem,"CessieDate")) %></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:label Runat="server" ID="Label5"><%# String.Format("{0:d}",DataBinder.Eval(Container.DataItem,"CessieDate")) %></asp:label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Piutang<br/>(Rp) " ItemStyle-HorizontalAlign="Right">
										<HeaderStyle ForeColor="White" Width="80px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label6" ><%# String.Format("{0:#,###}", DataBinder.Eval(Container.DataItem,"Amount"))  %></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:label Runat="server" ID="Label7"><%# String.Format("{0:#,###}", DataBinder.Eval(Container.DataItem,"Amount"))  %></asp:label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nilai Pembelian<br/>(Rp)" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle ForeColor="White" Width="80px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label8"><%# String.Format("{0:#,###}",DataBinder.Eval(Container.DataItem,"PurchaseAmount")) %></asp:Label>
										</ItemTemplate>										
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Biaya Administrasi<br/>(Rp)" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle ForeColor="White" Width="80px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblAdminFee"><%# String.Format("{0:#,###}",DataBinder.Eval(Container.DataItem,"AdminFee")) %></asp:Label>
										</ItemTemplate>										
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jumlah Harus Dibayar<br/>(Rp)" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle ForeColor="White" Width="80px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblShouldToPay"><%# String.Format("{0:#,###}",DataBinder.Eval(Container.DataItem,"DifferenceAmount")) %></asp:Label>
										</ItemTemplate>										
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Pembayaran">
										<HeaderStyle ForeColor="White" Width="60px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="Label10">
												<%# String.Format("{0:d}",DataBinder.Eval(Container.DataItem,"PaymentDate")) %>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:label Runat="server" ID="Label11">
												<%# String.Format("{0:d}",DataBinder.Eval(Container.DataItem,"PaymentDate")) %>
											</asp:label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Bank">
										<HeaderStyle ForeColor="White" Width="100px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="LblBankName"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<table cellspacing=0 cellpadding =0 width="100%" border=0>
												<tr>
													<td><asp:TextBox Runat="server" ID="txtBankName" Width="100px"></asp:TextBox></td>
													<td><asp:Label runat="server" ID="LblBankNameEdit" Width=0/></td>
												</tr>
											</table>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No. Referensi">
										<HeaderStyle ForeColor="White" Width="200px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblRefNo"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>										
											<table cellspacing=0 cellpadding =0 width="100%" border=0>
												<tr>
													<td><asp:TextBox Runat="server" ID="txtRefNo" Width="100px"></asp:TextBox></td>
													<td><asp:Label runat="server" ID="lblRefNoEdit" Width="0px"/></td>
												</tr>
											</table>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No. Reg">
										<HeaderStyle ForeColor="White" Width="200px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblRegNumber"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>	
											<asp:Label runat="server" ID="lblRegNumberEdit"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tanggal Transfer">
										<HeaderStyle ForeColor="White" Width="60px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblTanggalTransfer"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<table cellspacing=0 cellpadding =0 width="100%" border=0>
												<tr>
													<td><cc1:inticalendar id="calDatePayment" runat="server" enabled=false></cc1:inticalendar></td>
													<td><asp:Label runat="server" ID="lblTanggalTransferEdit" Width="0px"/></td>
												</tr>
											</table>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
										CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
										EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
										<HeaderStyle Width="1px" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:EditCommandColumn>
									<asp:ButtonColumn CommandName="DownloadFilePdf" Text="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Download PDF&quot;&gt;">
									<HeaderStyle ForeColor="White" Width="1px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
								     </asp:ButtonColumn>
									<asp:ButtonColumn CommandName="DownloadFileTxt" Text="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Download Text&quot;&gt;">
									<HeaderStyle ForeColor="White" Width="1px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
									</asp:ButtonColumn>							
									<asp:TemplateColumn HeaderText="Lampiran">
										<HeaderStyle Width="1px" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="LblFileText" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem,"DownloadedBy") %>' />
																		
											<asp:Image Runat="server" ID="imgIsExistFileIndicator"></asp:Image>
											<img src="../images/alur_flow.gif" border="0" alt="Lihat" onclick="ShowDownloadCessie(<%# DataBinder.Eval(Container.DataItem,"ID") %>)">
										</ItemTemplate>
										<EditItemTemplate>
											<asp:Label runat="server" ID="Label13" Visible="False">
												<%# DataBinder.Eval(Container.DataItem,"DownloadedBy") %>
											</asp:Label>
											
											<asp:Image Runat="server" ID="imgIsExistFileIndicatorEdit"></asp:Image>
											<img src="../images/alur_flow.gif" border="0" alt="Lihat" onclick='ShowDownloadCessie(<%# DataBinder.Eval(Container.DataItem,"ID") %>)'>
											</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Jumlah<br/>(Rp)" ItemStyle-HorizontalAlign="Right" Visible=false>
										<HeaderStyle ForeColor="White" Width="80px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblJumlahPembayaran"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>										
											<table cellspacing=0 cellpadding =0 width="100%" border=0>
												<tr>
													<td><asp:TextBox Runat="server" ID="txtJumlahPembayaran" Enabled=False style="text-align:right;" Width="100px"></asp:TextBox></td>
													<td><asp:Label runat="server" ID="lblJumlahPembayaranEdit" Width="0px"/></td>
												</tr>
											</table>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
