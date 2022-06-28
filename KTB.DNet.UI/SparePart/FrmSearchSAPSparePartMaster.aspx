<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSearchSAPSparePartMaster.aspx.vb" Inherits="FrmSearchSAPSparePartMaster" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmEntrySparePartPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
					 
			function GetCurrentInputIndex()
			{ 
				var dgPODetail = document.getElementById("dtgPODetail");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dgPODetail.rows.length; index++)
				{
					inputs = dgPODetail.rows[index].getElementsByTagName("INPUT");
					
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
			
			
			function SparePart(selectedCode)
			{
				var tempParam = selectedCode.split(';');	
				var indek = GetCurrentInputIndex();
				var dgPODetail = document.getElementById("dtgPODetail");
				var partCode = dgPODetail.rows[indek].getElementsByTagName("INPUT")[0];
				var partName = dgPODetail.rows[indek].getElementsByTagName("SPAN")[1];
				
					partCode.value = tempParam[0];
					partName.innerHTML = tempParam[1];				
				//	console.log(selectedCode);
			}
			
			
									
		</script>
</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">MASTER BARANG - Lihat Stok</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD><br>
						<asp:datagrid id="dtgPODetail" runat="server" BorderWidth="0px" BorderColor="Gainsboro" BackColor="#CDCDCD"
							CellSpacing="1" CellPadding="3" ShowFooter="True" AutoGenerateColumns="False" Width="100%">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn ReadOnly="True" HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="PartNumber" HeaderText="Nomor Barang">
									<HeaderStyle Width="13%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNumber") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtFPartNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"  onblur="omitSomeCharacter('txtFPartNumber','<>?*%$;')" tabIndex="10" runat="server" width="70"></asp:TextBox>
										<asp:Label id="lblFPopUpSparePart" tabIndex="20" runat="server" height="10px">
											<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
												border="0"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtEPartNumber title='<%# DataBinder.Eval(Container, "DataItem.PartNumber") %>' onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtEPartNumber','<>?*%$;')" tabIndex=10 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNumber") %>' width="70">
										</asp:TextBox>
										<asp:Label id="lblEPopUpSparePart" tabIndex="20" runat="server" height="10px">
											<img style="cursor:hand" alt="Klik Disini untuk memilih Part Number" src="../images/popup.gif"
												border="0"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PartName" HeaderText="Nama Barang">
									<HeaderStyle Width="22%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblPartname runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartName") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblFPartName" runat="server"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id="lblEPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartName") %>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>

                                	<asp:TemplateColumn SortExpression="AltPartNumber" HeaderText="Barang Pengganti">
									<HeaderStyle Width="25px" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblAltPartNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AltPartNumber")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblFAltPartNumber" runat="server"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id="lblEAltPartNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AltPartNumber") %>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>


								<asp:TemplateColumn HeaderText="Qty">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblQty runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MaxStock") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtFQty" runat="server" onkeypress="return numericOnlyUniv(event)" onkeyup="pic(this,this.value,'9999999999','N')"
										Width="40px" CssClass="textRight"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtEQty runat="server" onkeypress="return numericOnlyUniv(event)" onkeyup="pic(this,this.value,'9999999999','N')"
										Width="40px" Text='<%# DataBinder.Eval(Container, "DataItem.MaxStock") %>' CssClass="textRight">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PartCode" HeaderText="Status">
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="ModelCode" HeaderText="Model">
									<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ModelCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="RetalPrice" HeaderText="Harga Eceran (Rp)">
									<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblRetailPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetalPrice","{0:#,##0}") %>' CssClass="textRight">
										</asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<asp:Label id="lblFRetailPrice" runat="server" CssClass="textRight"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id=lblERetailPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetalPrice","{0:###}") %>' CssClass="textRight">
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Stok">
									<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStok" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StockSAP") %>' CssClass="textRight">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="aksi">
									<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEdit" CausesValidation="False" Runat="server" text="Ubah" CommandName="edit">
											<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" CausesValidation="False" Runat="server" text="Hapus" CommandName="delete">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAdd" tabIndex="40" Runat="server" text="Tambah" CommandName="add">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="lbtnSave" tabIndex="40" CausesValidation="False" Runat="server" text="Simpan" CommandName="save">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										<asp:LinkButton id="lbtnCancel" tabIndex="50" CausesValidation="False" Runat="server" text="Batal" CommandName="cancel">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblView" runat="server">
											<img style="cursor:hand" alt="Alternatif" src="../images/detail.gif" border="0" height="17px"
												width="17px">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 40px"><asp:button id="btnCancel" tabIndex="70" runat="server" Width="48px" Text="Cari" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD>* Pencarian nomor barang hanya diperbolehkan maksimal 20 item<BR>
					* Nomor barang dan Quantity harus diisi
					</TD>
					<TD></TD>
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
