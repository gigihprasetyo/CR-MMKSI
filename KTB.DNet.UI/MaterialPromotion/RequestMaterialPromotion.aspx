<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RequestMaterialPromotion.aspx.vb" Inherits="RequestMaterialPromotion" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RequestMaterialPromotion</title>
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
						<td class="titlePage" style="HEIGHT: 31px">MATERIAL PROMOSI&nbsp;- Permintaan 
							Material Promosi</td>
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
								<TR>
									<td class="titleField" style="HEIGHT: 18px" width="30%">Nomor Permintaan</td>
									<TD style="HEIGHT: 18px" width="1%">:</TD>
									<TD style="HEIGHT: 18px" width="69%"><asp:label id="lblRequestNo" runat="server" Width="192px"></asp:label><asp:label id="lblSearchRequestNo" runat="server">
											<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
												border="0"></asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">Kode Dealer</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:label id="lblDealerCode" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 19px" width="30%">Nama Dealer</td>
									<TD style="HEIGHT: 19px" width="1%">:</TD>
									<TD style="HEIGHT: 19px" width="69%"><asp:label id="lblDealerName" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 18px" width="30%">Kota</td>
									<TD style="HEIGHT: 18px" width="1%">:</TD>
									<TD style="HEIGHT: 18px" width="69%"><asp:label id="lblCity" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">Tanggal Permintaan</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:label id="lblRequestDate" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR>
									<td class="titleField" width="30%">Status</td>
									<TD width="1%">:</TD>
									<TD width="69%"><asp:label id="lblStatus" runat="server" Width="240px">Label</asp:label></TD>
								</TR>
								<TR vAlign="top">
									<td class="titleField" style="HEIGHT: 26px" width="30%">Keterangan</td>
									<TD style="HEIGHT: 26px" width="1%">:</TD>
									<TD style="HEIGHT: 26px" width="69%"><asp:textbox id="txtDescription" runat="server" Width="264px" TextMode="MultiLine" Height="46px"></asp:textbox></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD>
							<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dtgMaterialPromotion" runat="server" Width="100%" AutoGenerateColumns="False"
									BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" BackColor="#E0E0E0" CellPadding="3" ShowFooter="True">
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
								</asp:datagrid></div>
						</TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<tr>
						<td><asp:button id="btnSimpan" runat="server" Width="64px" Text="Simpan" ></asp:button><asp:button id="btnNew" runat="server" Width="64px" Text="Baru"></asp:button><asp:button id="btnValidasi" runat="server" Width="64px" Text="Validasi" Visible="False"></asp:button><asp:button id="btnBack" runat="server" Width="64px" Text="Kembali"></asp:button><asp:button id="btnDownload" runat="server" Width="64px" Text="Download"></asp:button><asp:button id="btnCetak" runat="server" Width="72px" Text="Print Preview"></asp:button></td>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
