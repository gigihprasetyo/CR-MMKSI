<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryFreeServis.aspx.vb" Inherits="FrmEntryFreeServis" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Free Service</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">

		function firstFocus()
		{
			document.all.txtChassisMaster.focus();
		}
		
		function enter(controlAfter)
		{
			
			var charPressed = event.keyCode;
				if (charPressed == 13)
			{
				controlAfter.focus();
				return false;
			}
				
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
		
			function checkFSPeriod()
			{
				var d = new Date('<%= Now.toString("yyyy/M/d HH:mm:ss") %>');
				var a = new Date("2013/1/1 00:00:00");
				var b = new Date("2013/1/11 00:00:00");
				var str="";

				str=str+"<table width='100%'>";
				str=str+"	<tr height='100px'>";
				str=str+"		<td></td>";
				str=str+"	</tr>";
				str=str+"	<tr height='200px'>";
				str=str+"		<td align='center'>";
				str=str+"		Sehubungan dengan adanya perubahan proses pengiriman data kupon Free Service,<br>maka untuk sementara menu ini tidak dapat diakses mulai tanggal 1 Januari 2013 s/d 10 Januari 2013	";
				str=str+"		</td>";
				str=str+"	</tr>";
				str=str+"	<tr>";
				str=str+"		<td></td>";
				str=str+"	</tr>";
				str=str+"</table>";			

				//alert(d);
				if(d.valueOf()>a.valueOf())
				{
					if(d.valueOf()<b.valueOf())
					{
						//alert(str);
						document.write(str);
					}
				}
			}
		</script>
	</HEAD>
	<body onload="firstFocus();checkFSPeriod();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FREE SERVICE -&nbsp; Data Free Service</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%" colSpan="2"><asp:label id="lblDealer" runat="server">Kode Dealer </asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="75%" colSpan="4"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" colSpan="2">Nama Dealer</TD>
								<TD>:</TD>
								<TD colSpan="4"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="7" height="10"><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtChassisMaster"
										Display="None" ErrorMessage="Silahkan isi Kode Jenis Servis + No Rangka (tidak boleh kosong)"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtKM" Display="None"
										ErrorMessage="Silahkan isi jarak tempuh (tidak boleh kosong)"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtTglServis" Display="None"
										ErrorMessage="Silahkan isi tgl servis dengan format 'ddmmyyyy'"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtKM" Display="None"
										ErrorMessage="Silahkan isi jarak tempuh dengan angka" ValidationExpression="\d{1,6}"></asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtTglServis"
										Display="None" ErrorMessage="Silahkan isi tgl Servis dengan format  'ddMMyyyy' misal 01122005" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator4" runat="server" ControlToValidate="txtTglJual" Display="None"
										ErrorMessage="Silahkan isi tgl Jual dengan 8 digit angka," ValidationExpression="\d{8}"></asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ControlToValidate="txtTglJual" Display="None"
										ErrorMessage="Silahkan isi tgl Jual dengan format  'ddMMyyyy' mis 01122005" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 2px" width="24%" colSpan="2"></TD>
								<TD style="HEIGHT: 2px" width="1%"></TD>
								<TD style="HEIGHT: 2px" width="20%"><asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
										ShowSummary="False"></asp:validationsummary></TD>
								<TD style="HEIGHT: 2px" width="20%"></TD>
								<TD style="HEIGHT: 2px" width="20%"></TD>
								<TD style="HEIGHT: 2px" width="19%"><asp:button id="btnSave" runat="server" CausesValidation="True" Text="Simpan" Visible="False"
										Width="60"></asp:button><asp:button id="btnCancel" runat="server" CausesValidation="False" Text="Batal" Visible="False"
										Width="60"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField"  >Kind</TD>
                                <TD class="titleField"  >No. Rangka</TD>
								<TD></TD>
								<TD class="titleField"><asp:label id="lblKM" runat="server">Jarak Tempuh (KM)</asp:label></TD>
								<td class="titleField"><asp:label id="LblTglServis" runat="server" Width="73px">Tgl. Service</asp:label></td>
								<td class="titleField"><asp:label id="lblTgl" runat="server">Tgl. Penjualan</asp:label></td>
							</TR>
							<TR>
                                <td><asp:DropDownList ID="ddlKind" runat="server" ToolTip="Silahkan pilih Kode Kind"></asp:DropDownList></td>
								<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtChassisMaster" runat="server"
										onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" Width="174px" MaxLength="20" ToolTip="Silakan isi dengan nomor rangka (tanpa spasi). Contoh: SA00FFF4K000008"></asp:textbox></TD>
								<TD></TD>
								<TD><asp:textbox id="txtKM" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server"
										onblur="omitSomeCharacter('txtKM','<>?*%$;')" Width="120px" MaxLength="6" ToolTip="Harus dimasukan dengan angka"
										Height="18px"></asp:textbox></TD>
								<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtTglServis" runat="server"
										onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" Width="120px" MaxLength="8" Height="17px"
										ToolTip="Format tgl Servis adalah  'ddMMyyyy' misal 31122005 berarti 31-12-2005"></asp:textbox></td>
								<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtTglJual" onblur="omitSomeCharacter('txtTglJual','<>?*%$;')"
										runat="server" Width="120px" MaxLength="8" Height="18px" ToolTip="Format tgl Servis adalah  'ddMMyyyy' misal 31122005 berarti 31-12-2005"></asp:textbox></td>
								<TD><INPUT id="btnSimpan" style="WIDTH: 50px" type="button" value="Simpan" name="btnSimpan"
										runat="server" CausesValidation="False"> <INPUT id="btnBatal" style="WIDTH: 50px" type="button" value="Batal" name="btnBatal" runat="server"
										CausesValidation="False"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 169px; HEIGHT: 10px" colSpan="7"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" style="HEIGHT: 385px">
						<div id="div1" style="HEIGHT: 350px; OVERFLOW: auto"><asp:datagrid id="dgFreeServisEntry" runat="server" Width="100%" GridLines="Vertical" CellPadding="3"
								BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
								PageSize="1000">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" ReadOnly="True" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<HeaderTemplate>
											<INPUT id="chkAllItems" onclick="CheckAll('cbSelect',document.forms[0].chkAllItems.checked)"
												type="checkbox">
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="cbSelect" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="Dealer Code">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Width="53px" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FSKind.KindDescription" HeaderText="Jenis Free Servis">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblFSKind runat="server" Width="149px" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.KindDescription") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No. Rangka ">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblChassisNo runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' Width="173px">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn  Visible="False"  SortExpression="ChassisMaster.Category.ProductCategory.Code" HeaderText="Produk">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.ProductCategory.Code") %>' Width="173px">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn  Visible="false"  SortExpression="ChassisMaster.Category.CategoryCode" HeaderText="Kategori">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.CategoryCode") %>' Width="65px">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn SortExpression="MileAge" HeaderText="Jarak Tempuh KM">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDatKm runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MileAge") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tgl. Free Service">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SoldDate" HeaderText="Tgl. Penjualan">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=tglPenjualan runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SoldDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="FleetRequest.NoRegRequest" HeaderText="No Extended Free Service">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblNoFleetReq" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FleetRequest.NoRegRequest")%>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<BR>
						<asp:button id="btnRelease" runat="server" CausesValidation="False" Text="Rilis" Visible="False"
							Width="60px"></asp:button><INPUT id="btnRilis" style="WIDTH: 60px" type="button" value="Rilis" name="btnRilis" runat="server"
							CausesValidation="False"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left">&nbsp;&nbsp;</TD>
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
