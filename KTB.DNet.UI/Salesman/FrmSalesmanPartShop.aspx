<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanPartShop.aspx.vb" Inherits="FrmSalesmanPartShop" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function ShowPopUpPartShop() {
				//alert('test');
				showPopUp('../PopUp/PopUppartshop.aspx', '', 500, 600, PartShopSelection);
			}

			function PartShopSelection(SelectedPartShop) {
				var dgPartshop = document.getElementById("dgPartshop");
				var indek = GetCurrentInputIndex(dgPartshop);
				//alert (indek);
				var tempParam = SelectedPartShop.split(';');
				var Code = dgPartshop.rows[indek].getElementsByTagName("INPUT")[0];
				var Name = dgPartshop.rows[indek].getElementsByTagName("SPAN")[1];
				var City = dgPartshop.rows[indek].getElementsByTagName("SPAN")[2];

				if (navigator.appName == "Microsoft Internet Explorer") {
					Code.innerText = tempParam[0];
					Name.innerHTML = tempParam[1];
					City.innerHTML = tempParam[2];
				}
				else {
					Code.value = tempParam[0];
					Name.value = tempParam[1];
					City.value = tempParam[2];
				}
			}
			
			function GetCurrentInputIndex(dtg)
			{
				//var dtgArea = document.getElementById("dtgArea");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				for (index = 0; index < dtg.rows.length; index++)
				{
					inputs = dtg.rows[index].getElementsByTagName("INPUT");
					
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage"><asp:label id="lblPageTitle" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%">
									<asp:label id="lblDealer" runat="server" Width="220px"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Employee ID</TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<TD>
									<asp:label id="lblSalesmanCode" runat="server" Width="220px"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nama</TD>
								<TD style="HEIGHT: 23px" width="1%">:</TD>
								<TD align="left">
									<asp:label id="lblNama" runat="server" Width="220px"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<td></td>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px">
										<asp:datagrid id="dgPartshop" runat="server" Width="100%" BorderColor="Gainsboro" PageSize="25"
											CellSpacing="1" AllowSorting="true" BorderWidth="0px" CellPadding="3" ShowFooter="true" AutoGenerateColumns="False"
											BackColor="#CDCDCD" AllowPaging="true" AllowCustomPaging="true">
											<SelectedItemStyle Font-Bold="true" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="true" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titletableRsd"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PartShop.PartShopCode" HeaderText="Kode Part Shop">
													<HeaderStyle Width="20%" CssClass="titletableRsd"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblPartShopCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartShop.PartShopCode") %>'>
														</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtPartShopCodeE" TabIndex="10" runat="server" Width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
														<asp:Label ID="lblPartshopCodeE" TabIndex="20" runat="server" Height="10px">
															<img style="cursor:hand" alt="Klik disini untuk memilih Part Shop" src="../images/popup.gif"
																border="0"></asp:Label>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtPartShopCodeF" TabIndex="10" runat="server" Width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
														<asp:Label ID="lblPartshopCodeF" TabIndex="20" runat="server" Height="10px">
															<img style="cursor:hand" alt="Klik disini untuk memilih Part Shop" src="../images/popup.gif"
																border="0"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PartShop.Name" HeaderText="Nama Part Shop">
													<HeaderStyle HorizontalAlign="Right" Width="40%" CssClass="titletableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblPartshopName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartShop.Name") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label ID="lblPartshopNameF" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PartShop.CityPart.CityName" HeaderText="Kota">
													<HeaderStyle HorizontalAlign="Right" Width="30%" CssClass="titletableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblPartshopCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartShop.CityPart.CityName") %>'>
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:Label ID="lblPartshopCityF" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titletableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton ID="lbDeleteParthop" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton ID="lbAddParthop" CausesValidation="False" CommandName="add" Text="Tambah" runat="server">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</div>
								</TD>
							</TR>
							<TR>
								<td>
									<asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>
									<asp:button id="btnBack" runat="server" width="60px" Text="Kembali"></asp:button>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
