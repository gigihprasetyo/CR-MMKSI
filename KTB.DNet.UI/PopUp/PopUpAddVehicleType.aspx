<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpAddVehicleType.aspx.vb" Inherits="PopUpAddVehicleType"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpVechileType</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
			function ShowPopUpVechileType()
			{
				showPopUp('PopUpVechileType.aspx','',500,760,VechileTypeSelection);
			}

			function VechileTypeSelection(SelectedVechileType)
			{
				var indek = GetCurrentInputIndex();
				var dgSAPCustomer = document.getElementById("dgSAPCustomer");
				var tempParam = SelectedVechileType.split(';');
				
				var txtAddVechileTypeCode = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[0];
				var txtAddDescription = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[1];			
							
				if(navigator.appName == "Microsoft Internet Explorer")
					{
					txtAddVechileTypeCode.innerText = tempParam[0];			
					txtAddDescription.innerText = tempParam[1];												
					}
				else
					{
					txtAddVechileTypeCode.innerText = tempParam[0];			
					txtAddDescription.innerText = tempParam[1];							
					}
			 }		
			 
			function GetCurrentInputIndex()
			{
				var dgSAPCustomer = document.getElementById("dgSAPCustomer");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dgSAPCustomer.rows.length; index++)
				{
					inputs = dgSAPCustomer.rows[index].getElementsByTagName("INPUT");
					
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
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">Kendaraan&nbsp;- Pencarian 
									Kendaraan</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px"><asp:datagrid id="dgSAPCustomer" runat="server" AllowPaging="True" PageSize="25" AllowCustomPaging="True"
											AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3"
											Width="100%">
											<SelectedItemStyle VerticalAlign="Top"></SelectedItemStyle>
											<EditItemStyle VerticalAlign="Top"></EditItemStyle>
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Kode">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblVechileTypeCode" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAddVechileTypeCode"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Runat="server"></asp:TextBox>
														<asp:Label id="Label4" onclick="ShowPopUpVechileType();" runat="server">
															<img style="cursor:hand" alt="Klik Disini untuk memilih Customer" src="../images/popup.gif"
																border="0"></asp:Label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtEditVechileTypeCode"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Runat="server"></asp:TextBox>
														<asp:Label id="Label1" onclick="ShowPopUpVechileType();" runat="server">
															<img style="cursor:hand" alt="Klik Disini untuk memilih Customer" src="../images/popup.gif"
																border="0"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Deskripsi">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDescription" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAddDescription" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"
															MaxLength="50" Runat="server"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtEditDescription"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Runat="server"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Qty" HeaderText="Qty">
													<HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblQty" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtAddQty" Runat="server" MaxLength="5" onkeypress="return NumericOnlyWith(event,'')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditQty" Runat="server" MaxLength="5" onkeypress="return NumericOnlyWith(event,'')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Aksi">
													<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
														<asp:LinkButton id="lbtnRegister" tabIndex="50" CommandName="Register" text="Register" Runat="server" CausesValidation="False" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.ID")%>'>
															<img src="../images/icon_customer.gif" border="0" alt="Pendaftaran Konsumen"></asp:LinkButton>
													</ItemTemplate>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" runat="server" CausesValidation="False" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="Save" text="Simpan" Runat="server" CausesValidation="False">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="Cancel" text="Batal" Runat="server" CausesValidation="False">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="getSelectedCourse()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
