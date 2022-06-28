<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RequestMaterialPromotionPrintPreview.aspx.vb" Inherits="RequestMaterialPromotionPrintPreview" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title><!--RequestMaterialPromotion--></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			
			function RequestNo(selectedCode)
			{
				window.location.href = '../MaterialPromotion/RequestMaterialPromotion.aspx?ID='+selectedCode;
			}
			
			function MatPro(selectedCode)
			{
				var splited = selectedCode.split(';');
				var indek = GetCurrentInputIndex();
				var KodeBarang = dtgMaterialPromotion.rows[indek].getElementsByTagName("INPUT")[0];
				var NamaBarang = dtgMaterialPromotion.rows[indek].getElementsByTagName("SPAN")[1];
//				alert(splited[1]);
				KodeBarang.value = splited[0];
				NamaBarang.innerHTML = splited[1];
			}
			
			function GetCurrentInputIndex()
			{
				var dtgMaterialPromotion = document.getElementById("dtgMaterialPromotion");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dtgMaterialPromotion.rows.length; index++)
				{
					inputs = dtgMaterialPromotion.rows[index].getElementsByTagName("INPUT");
					
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

			
			function QtyValidate(source, arguments)
			{
				var indek = GetCurrentInputIndex();	
				var dgPODetail = document.getElementById("dtgPODetail");
				var partQTY =parseInt(dgPODetail.rows[indek].getElementsByTagName("INPUT")[1].value);							
				if (partQTY >0)
				{
					arguments.IsValid = true;
				}
				else
				{					
					arguments.IsValid = false;					
				}
			}
									
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="titlePage" style="COLOR: #000000; HEIGHT: 31px">
							<P><IMG src="../images/logo_ktb_detail.gif?p=1" border="0">
								<br>
								<div align="center"><FONT size="4">TANDA TERIMA MATERIAL PROMOSI</FONT></div>
								<br>
								PERMINTAAN MATERIAL PROMOSI
								<asp:label id="lblSearchRequestNo" runat="server" Visible="False">
									<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
										border="0"></asp:label>
						</td>
					</tr>
					<tr>
						<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
					</tr>
					<tr>
						<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
					</tr>
					<tr>
						<td style="HEIGHT: 93px">
							<table cellSpacing="1" cellPadding="2" width="100%" border="0">
								<thead>
									<tr>
										<td width="20%"></td>
										<td width="2%"></td>
										<td width="53%"></td>
										<td width="25%"></td>
									</tr>
								</thead>
								<TR>
									<td class="titleField" style=" HEIGHT: 32px">Nomor&nbsp;Permintaan</td>
									<TD style="HEIGHT: 32px" width="1%">:</TD>
									<TD style="WIDTH: 385px; HEIGHT: 32px" width="385"><asp:label id="lblRequestNo" runat="server" Width="400px" Height="16px"></asp:label></TD>
									<TD style="HEIGHT: 32px" width="15%" align="left">a) File 1 (asli) untuk Gudang 
										Promosi - MMKSI
									</TD>
								</TR>
								<TR>
									<td class="titleField" style="WIDTH: 130px" width="130">Kode Dealer</td>
									<TD width="1%">:</TD>
									<TD style="WIDTH: 385px" width="385"><asp:label id="lblDealerCode" runat="server" Width="240px">Label</asp:label></TD>
									<TD width="69%" align="left">b) File 2 (copy) untuk Security Office - MMKSI
									</TD>
								</TR>
								<TR>
									<td class="titleField" style="WIDTH: 130px; HEIGHT: 19px" width="130">Nama Dealer</td>
									<TD style="HEIGHT: 19px" width="1%">:</TD>
									<TD style="WIDTH: 385px; HEIGHT: 19px" width="385"><asp:label id="lblDealerName" runat="server" Width="240px">Label</asp:label></TD>
									<TD style="HEIGHT: 19px" width="69%" align="left">c) File 3 (copy) untuk Dealer 
										yang bersangkutan
									</TD>
								</TR>
								<TR>
									<td class="titleField" style="WIDTH: 130px; HEIGHT: 18px" width="130">Kota</td>
									<TD style="HEIGHT: 18px" width="1%">:</TD>
									<TD style="HEIGHT: 18px" width="69%" colSpan="2"><asp:label id="lblCity" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" style="WIDTH: 130px" width="130">
										Tanggal Permintaan</td>
									<TD width="1%">:</TD>
									<TD width="69%" colSpan="2"><asp:label id="lblRequestDate" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR>
									<TD class="titleField" width="180" noWrap colSpan="1" rowSpan="1">
										Tanggal Persetujuan MKS</TD>
									<TD width="1%">:</TD>
									<TD width="69%" colSpan="2">
										<asp:label id="lblApprovedDate" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" style="WIDTH: 130px" width="130">Status</td>
									<TD width="1%">:</TD>
									<TD width="69%" colSpan="2"><asp:label id="lblStatus" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR vAlign="top">
									<td class="titleField" style="WIDTH: 130px; HEIGHT: 26px" width="130">Keterangan</td>
									<TD style="HEIGHT: 26px" width="1%">:</TD>
									<TD style="HEIGHT: 26px" width="69%" colSpan="2"><asp:label id="lblDescription" runat="server" Width="376px" Height="48px"></asp:label><asp:textbox id="txtDescription" runat="server" Width="32px" Visible="False" Height="24px" TextMode="MultiLine"></asp:textbox></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD vAlign="top">
							<!-- <div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"> -->
							<asp:datagrid id="dtgMaterialPromotion" runat="server" Width="100%" ShowFooter="True" CellPadding="3"
									BackColor="#E0E0E0" BorderWidth="1px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
									<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
									<ItemStyle BackColor="White"></ItemStyle>
									<HeaderStyle VerticalAlign="Top"></HeaderStyle>
									<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
									<Columns>
										<asp:TemplateColumn Visible="False" HeaderText="id">
											<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="Label1" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Id")%>'>
												</asp:Label>
											</ItemTemplate>
											<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
											<FooterTemplate>
												<asp:Label id="Label2" runat="server"></asp:Label>
											</FooterTemplate>
											<EditItemTemplate>
												<asp:Label id="Label3" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Id")%>'>
												</asp:Label>
											</EditItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
												</asp:Label>
											</ItemTemplate>
											<FooterStyle Font-Size="Small"></FooterStyle>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Kode Barang">
											<HeaderStyle Width="12%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id=lblGoodsNo runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.GoodNo")%>'>
												</asp:Label>
											</ItemTemplate>
											<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
											<FooterTemplate>
												<asp:TextBox id="txtFooterGoodsNo" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
													onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Width="60px" BackColor="White"></asp:TextBox>
												<asp:Label id="lbtnPopUpFooter" tabIndex="20" runat="server" height="10px">
													<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Barang" src="../images/popup.gif"
														border="0"></asp:Label>
											</FooterTemplate>
											<EditItemTemplate>
												<asp:TextBox id=txtEditGoodsNo runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.GoodNo")%>'>
												</asp:TextBox>
												<asp:Label id="lbtnPopUpEdit" tabIndex="20" runat="server" height="10px">
													<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
														border="0"></asp:Label>
											</EditItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Nama Barang">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblName" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.Name")%>'>
												</asp:Label>
											</ItemTemplate>
											<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
											<FooterTemplate>
												<asp:Label id="lblFooterName" runat="server"></asp:Label>
											</FooterTemplate>
											<EditItemTemplate>
												<asp:Label id="lblEditName" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.MaterialPromotion.Name")%>'>
												</asp:Label>
											</EditItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Jumlah Alokasi">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id="lblAlokasiQuantity" runat="server"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Jumlah Permintaan">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id=Label4 runat="server" text='<%#DataBinder.Eval(Container,"DataItem.RequestQty")%>'>
												</asp:Label>
											</ItemTemplate>
											<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
											<FooterTemplate>
												<asp:TextBox MaxLength="7" onkeypress="return NumericOnlyWith(event,'');" id="txtFooterRequestQuantity"
													onkeyup="pic(this,this.value,'999999999','N')" runat="server" Width="60px" BackColor="White"
													onblur="omitSomeCharacter(this.id,'<>?*%$;-')"></asp:TextBox>
											</FooterTemplate>
											<EditItemTemplate>
												<asp:TextBox MaxLength="7" onkeypress="return NumericOnlyWith(event,'');" id=txtEditRequestQuantity onkeyup="pic(this,this.value,'999999999','N')" runat="server" Width="60px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.RequestQty")%>'>
												</asp:TextBox>
											</EditItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Jumlah Disetujui">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id=lblQuantity runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Qty")%>'>
												</asp:Label>
											</ItemTemplate>
											<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
											<FooterTemplate>
												<asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id="txtFooterQuantity" onkeyup="pic(this,this.value,'999999999','N')"
													runat="server" Width="60px" BackColor="White" onblur="omitSomeCharacter(this.id,'<>?*%$;-')"></asp:TextBox>
											</FooterTemplate>
											<EditItemTemplate>
												<asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtEditQuantity onkeyup="pic(this,this.value,'999999999','N')" runat="server" Width="60px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Qty")%>'>
												</asp:TextBox>
											</EditItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn HeaderText="Keterangan">
											<HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label id=lblDescription runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
												</asp:Label>
											</ItemTemplate>
											<FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
											<FooterTemplate>
												<asp:TextBox id="txtDescription1" runat="server" Width="60px" BackColor="White"></asp:TextBox>
											</FooterTemplate>
											<EditItemTemplate>
												<asp:TextBox id=txtDescription2 runat="server" Width="60px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
												</asp:TextBox>
											</EditItemTemplate>
										</asp:TemplateColumn>
										<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
											CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
											EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
											<HeaderStyle Width="4%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										</asp:EditCommandColumn>
										<asp:TemplateColumn>
											<HeaderStyle Width="4%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton id="lbtnDelete" runat="server" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>' CommandName="Delete">
													<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderStyle Width="4%" CssClass="titleTablePromo"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<FooterStyle HorizontalAlign="Center"></FooterStyle>
											<FooterTemplate>
												<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
													<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
											</FooterTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
								</asp:datagrid>
								<!-- </div> -->
							Catatan :
							<br>
							1) Material Promosi wajib diambil segera di MMKSI setelah permintaan dealer 
							disetujui oleh MMKSI, maksimal 3 (tiga) hari kerja setelah tanggal persetujuan 
							dari MMKSI.<br>
							2) Untuk pengambilan barang di Gudang Promosi agar petugas dealer terlebih 
							dahulu mengambil surat tanda terima pengambilan barang di Dealer Affair Sect.<br>
							<br>
							Jakarta, <asp:Label ID="lblThn" runat="server" > </asp:Label><br>
							<br>
							<P></P>
							<table width="90%" border="0">
								<tr>
									<td align="left">Diketahui Oleh</td>
									<td style="WIDTH: 248px" align="center">Diserahkan&nbsp;Oleh</td>
									<td align="center">Diterima Oleh</td>
								</tr>
								<tr>
									<td>
										<p align="left">Dealer Affair Sect.</p>
									</td>
									<td>
										<p align="center">Staff Gudang Promosi</p>
									</td>
									<td>
										<p align="center">Petugas Dealer</p>
									</td>
								<tr>
									<td colSpan="3">
										<p>&nbsp;</p>
									</td>
								<tr>
									<td colSpan="3">
										<p>&nbsp;</p>
									</td>
								<tr>
									<td colSpan="3">
										<p>&nbsp;</p>
									</td>
								<tr>
									<td align="left">(...................................)</td>
									<td style="WIDTH: 248px" align="center">(...................................)</td>
									<td align="center">(...................................)</td>
								</tr>
								<TR>
									<TD align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD style="WIDTH: 248px" align="center"></TD>
									<TD align="center">&nbsp;</TD>
								</TR>
								<TR>
									<TD align="left"></TD>
									<TD style="WIDTH: 248px" align="center"><INPUT class="hideButtonOnPrint" id="btnCetak" style="WIDTH: 48px; HEIGHT: 21px" onclick="document.getElementById('btnCetak').style.visibility='hidden';window.print();document.getElementById('btnCetak').style.visibility='visible';"
											type="button" value="Cetak" name="btnCetak"></TD>
									<TD align="center"></TD>
								</TR>
							</table>
							</SPAN>
						</TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<tr>
						<td align="center"><asp:button id="btnSimpan" runat="server" Visible="False" Text="Simpan"></asp:button><asp:button id="btnNew" runat="server" Visible="False" Text="Baru"></asp:button><asp:button id="btnValidasi" runat="server" Visible="False" Text="Validasi"></asp:button><asp:button id="btnBack" runat="server" Visible="False" Text="Kembali"></asp:button><asp:button id="btnDownload" runat="server" Visible="False" Text="Download"></asp:button></td>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
