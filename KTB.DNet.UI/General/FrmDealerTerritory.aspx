<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDealerTerritory.aspx.vb" Inherits="FrmDealerTerritory" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MAINTENANCE - Dealer Territory</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function alphaNumericWith(event, addKey)
			{
				var pressedKey
				
				if(navigator.appName == "Microsoft Internet Explorer")	
					pressedKey = event.keyCode;
				else
				{
					pressedKey = event.charCode;
				}					
				if ( (isAccepted(pressedKey,addKey)) ||(pressedKey >=48 && pressedKey<=57) || (pressedKey >=97 && pressedKey <=122) || (pressedKey >=65 && pressedKey <=90) || (pressedKey == 0) )
				{
					return true;
				}
				else
					return false;
			}		
			
			function alphaNumericBlur(controlName)
			{	
				var key = controlName.value;
				var newValue = "";
				for (i=0;i<key.length;i++)	
				{
					if ((key.charCodeAt(i) >=48 && key.charCodeAt(i)<=57) || (key.charCodeAt(i) >=97 && key.charCodeAt(i) <=122) || (key.charCodeAt(i) >=65 && key.charCodeAt(i) <=90))
					{
						newValue = newValue + key.charAt(i);
					}				
				}			
				controlName.value = newValue;
			}
		
			function alphaNumericBlurWith(controlName, addKey)
			{
				var key = controlName.value;
				var newValue = "";
				for (i=0;i<key.length;i++)	
				{
					if ( (key.charCodeAt(i) >=48 && key.charCodeAt(i)<=57) || (key.charCodeAt(i) >=97 && key.charCodeAt(i) <=122) || (key.charCodeAt(i) >=65 && key.charCodeAt(i) <=90) || (key.charCodeAt(i) == 0) )
					{
						newValue = newValue + key.charAt(i);
					}				
				}			
				controlName.value = newValue;	
			}
		
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{
				var tempParam
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				var lblDealerName = document.getElementById("lblDealerName");
				var tName = document.getElementById("TempName");
				var lblSearchTerm1 = document.getElementById("lblSearchTerm1");
				var tSearch = document.getElementById("TempSearch");
				
				if (selectedDealer != "")
				{
					tempParam = selectedDealer.split(';');	
					txtDealerSelection.value = tempParam[0];					
					
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						lblDealerName.innerText = tempParam[1];
						tName.value = tempParam[1];
						if (tempParam[2] != "")
						{	lblSearchTerm1.innerText = tempParam[2];
							tSearch.value = tempParam[2];
						}
						else
						{	lblSearchTerm1.innerText = "";						
						}						
					}
					else
					{	
						lblDealerName.innerHTML = tempParam[1];
						tName.value = tempParam[1];
						if (tempParam[2] != "")
						{	lblSearchTerm1.innerHTML = tempParam[2];
							tSearch.value = tempParam[2];
						}
						else
						{	lblSearchTerm1.innerHTML = "";
						}						
					}
				}
				else
				{
					txtDealerSelection.value = selectedDealer;
					if(navigator.appName == "Microsoft Internet Explorer")
					{	
						lblDealerName.innerText = "";
						lblSearchTerm1.innerText = "";
						tName.value = "";
						tSearch.value = "";						
					}
					else
					{	
						lblDealerName.innerHTML = "";
						lblSearchTerm1.innerHTML = "";
						tName.value = "";
						tSearch.value = "";						
					}
				}
			}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">MAINTENANCE&nbsp;- Wilayah Dealer</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 137px">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox onkeypress="return alphaNumericWith(event,'');" onblur="alphaNumericBlurWith(txtKodeDealer,'');"
										id="txtKodeDealer" runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>&nbsp;
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtKodeDealer"></asp:requiredfieldvalidator>&nbsp;
									<asp:label id="lblDealerName" runat="server"></asp:label>&nbsp;/
									<asp:label id="lblSearchTerm1" runat="server"></asp:label><INPUT id="TempName" runat="server" type="hidden"><INPUT id="TempSearch" runat="server" type="hidden"></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 6px" width="24%">Propinsi</TD>
								<TD style="HEIGHT: 6px" width="1%"></TD>
								<TD style="HEIGHT: 6px" width="75%"><asp:dropdownlist id="ddlProvince" runat="server" AutoPostBack="True" Width="328px"></asp:dropdownlist>&nbsp;
									<asp:comparevalidator id="CompareValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlProvince"
										ValueToCompare="-1" Operator="NotEqual">*</asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Kota</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="ddlCity" runat="server" Width="328px"></asp:dropdownlist>&nbsp;
									<asp:comparevalidator id="CompareValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlCity"
										ValueToCompare="-1" Operator="NotEqual">*</asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD>
									<P><asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button>&nbsp;
										<asp:button id="btnSearch" runat="server" Width="60px" Text="Cari" CausesValidation="False"></asp:button></P>
								</TD>
							</TR>
							<TR>
								<TD colSpan="3"><asp:label id="lblErrMsg" runat="server" ForeColor="Red"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px">
							<asp:datagrid id="dtgDealerTerritory" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False"
								AllowCustomPaging="True" PageSize="100" AllowPaging="True" AllowSorting="True" BorderColor="#CDCDCD"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Dealer">
										<HeaderStyle Width="35%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblDealer runat="server"  >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="City.Province.ProvinceName" HeaderText="Propinsi">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblProvince runat="server"  >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
										<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblCity runat="server"    >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</TD>
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
