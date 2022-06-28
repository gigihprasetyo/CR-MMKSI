<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPartShopEntry.aspx.vb" Inherits="FrmPartShopEntry" smartNavigation="False" %>
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
			function ShowPPDealerSelection()
			{
			showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}
					function DealerSelection(selectedDealer)
			{
				var txtDealer = document.getElementById("txtDealerCode");
				txtDealer.value = selectedDealer;				
			}
			
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}
			
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
			}
			
			function checkDoublecharDelimiter(txt) {
			    var txtval = '';
			    var chr = '';
			    var chr2 = '';
			    for (i = 0; i < txt.value.length; i++) {
			        chr = txt.value.substr(i, 1);
			        if (chr == ' ' || chr == '/' || chr == '-' || chr == '+') {
			            if (i > 0 && i < txt.value.length - 1) {
			                chr2 = txt.value.substr(i + 1, 1);
			                if (chr != chr2) {
			                    txtval += chr;
			                }
			            }
			            else
			                txtval += chr;
			        }
			        else
			            txtval += chr;
			    }

			    txt.value = txtval;
			}

			function checkCharSpaceOrSlashOnfront(txt, e) {
			    if (txt.value.substring(0, 1) == ' ' || txt.value.substring(0, 1) == '/' || txt.value.substring(0, 1) == '-') {
			        if (e.keyCode == 32 || e.keyCode == 191 || e.keyCode == 189) {
			            txt.value = txt.value.substring(1, txt.value.length);
			        }
			    }
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage"><asp:label id="lblPageTitle" runat="server"> lblPageTitle</asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<!--<TR>
								<TD class="titleField" width="20%">Kode Dealer</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
										runat="server" Width="250px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Dealer" src="../images/popup.gif" border="0" onclick="ShowPPDealerSelection();"></asp:label>
									<asp:label id="lblDealer" runat="server"></asp:label></TD>
							</TR>-->
							<TR>
								<TD class="titleField" width="20%">Part Shop ID</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox id="txtPartShopCode" Runat="server"></asp:textbox><asp:label id="lblPartShopID" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Nama</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox id="txtName" runat="server" Width="250px"></asp:textbox><asp:requiredfieldvalidator id="valName" runat="server" ControlToValidate="txtName" ErrorMessage="Nama Harus diisi"> Nama harus diisi</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Alamat</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox id="txtAddress" runat="server" Width="450px" Rows="2"></asp:textbox><asp:requiredfieldvalidator id="valAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Alamat Harus diisi"> Alamat Harus diisi</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Provinsi / Kota / Kota Part</TD>
								<TD style="HEIGHT: 28px" width="1%">:</TD>
								<TD><asp:dropdownlist id="ddlProvince" runat="server" AutoPostBack="True"></asp:dropdownlist>
                                    <asp:label id="lblSeparator" Runat="server">/</asp:label><asp:dropdownlist id="ddlCity" runat="server" AutoPostBack="True"></asp:dropdownlist>
                                    <asp:label id="Label3" Runat="server">/</asp:label><asp:dropdownlist id="ddlCityPart" runat="server"></asp:dropdownlist>
                                    <asp:requiredfieldvalidator id="valProvince" runat="server" ControlToValidate="ddlProvince" ErrorMessage="Propinsi Harus dipilih"> Propinsi Harus dipilih.</asp:requiredfieldvalidator>
                                    <asp:requiredfieldvalidator id="valCity" runat="server" ControlToValidate="ddlCity" ErrorMessage="Kota Harus dipilih">  Kota Harus dipilih</asp:requiredfieldvalidator>
                                    <asp:requiredfieldvalidator id="valCityPart" runat="server" ControlToValidate="ddlCityPart" ErrorMessage="Kota Part Harus dipilih">  Kota Part Harus dipilih</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Telp / Hp</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox id="txtPhone" onkeyup="checkCharSpaceOrSlashOnfront(this,event);" onblur="checkDoublecharDelimiter(this);" MaxLength="40" runat="server" Width="180px">
								</asp:textbox><asp:requiredfieldvalidator id="valPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="* Telephone Harus diisi"></asp:requiredfieldvalidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="* Minimum 8 digit" ControlToValidate="txtPhone"
                                             Display="Dynamic" ValidationExpression="^[\s\S]{8,40}$"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="* Harus diisi dengan angka" ControlToValidate="txtPhone"
                                             Display="Dynamic" ValidationExpression="^([0-9\ \/\-\+]*)$"></asp:RegularExpressionValidator>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="20%">Fax</TD>
								<TD style="HEIGHT: 17px" width="1%">:</TD>
								<TD width="79%"><asp:textbox id="txtFax" runat="server" Width="180px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="lblPeriode" runat="server">Periode Tanggal Buat</asp:label></TD>
								<TD><asp:label id="lblSprPeriode" runat="server">:</asp:label></TD>
								<td><asp:dropdownlist id="ddlMonth1" runat="server"></asp:dropdownlist><asp:dropdownlist id="ddlYear1" runat="server" Width="50px"></asp:dropdownlist><asp:label id="lblSprPeriode2" runat="server">s/d</asp:label><asp:dropdownlist id="ddlMonth2" runat="server"></asp:dropdownlist><asp:dropdownlist id="ddlYear2" runat="server" Width="50px"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD width="1%"></TD>
								<TD>
									<asp:button id="btnCari" runat="server" CausesValidation="False" Text="Cari" width="60px" Visible="False"></asp:button>
									<asp:button id="btnSave" runat="server" CausesValidation="True" Text="Simpan" width="60px"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD></TD>
							<TR>
								<TD class="titleField" width="20%"><asp:label id="Label1" runat="server" Visible="False">Jumlah Partshop</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label2" runat="server" Visible="False">:</asp:label></TD>
								<TD width="79%"><asp:label id="lblJumlah" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="3">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dgPartShop" runat="server" Width="100%" DESIGNTIMEDRAGDROP="57" AllowSorting="True"
											CellSpacing="1" AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" PageSize="25"
											BackColor="#CDCDCD" CellPadding="3">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="Status" ReadOnly="True" HeaderText="Status">
													<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="True" DataField="PartShopCode" ReadOnly="True" SortExpression="PartShopCode"
													HeaderText="Part Shop ID">
													<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Nama Part Shop" SortExpression="Name">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblNamaPartShop runat="server" NAME="lblSalesmanCode" Text='<%# DataBinder.Eval(Container.DataItem, "Name" ) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Alamat" SortExpression="Address">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblAlamat runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Address" )  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Propinsi">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPropinsi" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kota" SortExpression="City">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblKota" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Kota Part" SortExpression="City">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblKotaPart" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Telp / Hp" SortExpression="Phone">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTelephone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Phone" )  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Fax" SortExpression="Fax">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblFax" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Fax" )  %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status" SortExpression="Status">
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" Visible="True" Runat="server" text="Hapus" CommandName="edit" CausesValidation="False">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" Visible="True" Runat="server" text="Hapus" CommandName="delete"
															CausesValidation="False">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<tr>
								<td colspan="3">
									<asp:button id="btnRequestID" runat="server" CausesValidation="False" Text="Request ID" width="60px"></asp:button>
									<asp:button id="btnRegister" runat="server" CausesValidation="False" Text="Register" width="60px"></asp:button>

								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
