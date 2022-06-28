<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPClaimEntry.aspx.vb" Inherits=".FrmMSPClaimEntry" %>
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
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">

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
		function GetCurrentSpanIndex()
		{
			var dgSPDetail = document.getElementById("dgEntryPM");
			var currentRow;
			var index = 0;
			var inputs;
			var indexInput;
			
			
			for (index = 0; index < dgSPDetail.rows.length; index++)
			{
				inputs = dgSPDetail.rows[index].getElementsByTagName("SPAN");
				
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
				
		function ShowReplacementPart()
		{
			showPopUp('../General/../PopUp/PopReplecementPart.aspx?ID='+0,'',500,500,ReplacementPartSelection);
			
		}
		function ReplacementPartSelection(selectedPart)
		{
			var txtPenggatianPart = document.getElementById("txtPenggatianPart");
			txtPenggatianPart.value = selectedPart;	
		}
		function ShowReplacementPart2()
		{
			
			var index = GetCurrentSpanIndex();
			inputs = dgEntryPM.rows[index].getElementsByTagName("SPAN")[1];
			alert(inputs.innerHTML);
			showPopUp('../General/../PopUp/PopReplecementPartSelection.aspx?ID='+inputs.innerHTML,'',500,500,ReplacementPartSelection);
		}
		</script>
	</HEAD>
	<body onload="firstFocus()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Mitsubishi Smart Package &nbsp; -&nbsp;Input Claim</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%" colspan="2"><asp:label id="lblDealer" runat="server">Kode Dealer :</asp:label></TD>
								<TD width="75%" colSpan="5"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" colspan="2">Nama Dealer :</TD>
								<TD colSpan="5"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="7" height="10"><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="No Rangka (tidak boleh kosong)"
										Display="None" ControlToValidate="txtChassisMaster"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Silahkan isi jarak tempuh (tidak boleh kosong)"
										Display="None" ControlToValidate="txtKM"></asp:requiredfieldvalidator> <asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Silahkan isi jarak tempuh dengan angka"
										Display="None" ControlToValidate="txtKM" ValidationExpression="\d{1,6}"></asp:regularexpressionvalidator>  </TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 2px" width="24%" colspan="2"></TD>
								<TD style="HEIGHT: 2px" width="20%" colspan="5"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
										DisplayMode="List"></asp:validationsummary></TD>
							</TR>
							<TR>
								<TD class="titleField">&nbsp;No. Rangka</TD>
								<TD class="titleField">No. Mesin</TD>
                                <td class="titleField">
                                    <asp:Label runat="server" ID="lblPMKind" Visible="false" Text="Jenis PM"></asp:Label>
                                </td>
								<TD class="titleField">Stand KM</TD>
								<td class="titleField"><asp:label id="LblTglServis" runat="server" Width="100px">Tgl.Service</asp:label></td>
								<td class="titleField"><asp:label id="lblVisitType" runat="server" Width="100px">Tipe Visit</asp:label></td>
							</TR>
							<TR>
								<TD><asp:textbox id="txtChassisMaster" runat="server" Width="160px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
										onblur="omitSomeCharacter('txtChassisMaster','<>?*%$;');" ToolTip="Silakan isi nomor rangka (tanpa spasi). Contoh: SA00FFF4K000008 dimana SA00FFF4K000008 adalah nomor rangka." AutoPostBack="true"
										MaxLength="20"></asp:textbox></TD>
								<TD>
                                    <asp:TextBox ID="txtEngineNo" runat="server" Width="160px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
										onblur="omitSomeCharacter('txtEngineNo','<>?*%$;');" ToolTip="Silakan isi nomor mesin (tanpa spasi). Contoh: 4G15-R66169 dimana 4D56C-R52049 adalah nomor mesin." AutoPostBack="true" 
										MaxLength="20"></asp:TextBox>
								</TD>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlPMKind" Visible="false"></asp:DropDownList>
                                </td>
								<TD><asp:textbox id="txtKM" runat="server" Width="100px" ToolTip="Harus dimasukan dengan angka" MaxLength="6"
										Height="18px" onkeypress="return NumericOnlyWith(event,'');"></asp:textbox></TD>
								<td> <cc1:inticalendar id="calTglService" runat="server" TextBoxWidth="70"></cc1:inticalendar> &nbsp;</td>

                                <td>
                                    <asp:DropDownList ID="ddlVisitType" runat="server">
                                        <asp:ListItem Value="WI">Walk In</asp:ListItem>
                                        <asp:ListItem Value="BO">Booking</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
								<td>&nbsp;<INPUT id="btnSimpan" style="WIDTH: 50px" type="button" value="Simpan" name="btnSimpan"
										runat="server" CausesValidation="true">&nbsp; <INPUT id="btnBatal" style="WIDTH: 50px" type="button" value="Batal" name="btnBatal" runat="server"
										CausesValidation="False"></td>
								<TD>&nbsp;</TD>
							</TR>
							<TR>
								<TD colSpan="6"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dgEntryPM" runat="server" Width="100%" PageSize="25" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical"
								AllowPaging="True" AllowCustomPaging="True">
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
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
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
									<asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No. Rangka ">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblChassisNo runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' Width="173px">
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="" HeaderText="Jenis Kind">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblPMKind" runat="server" Width="173px"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Left"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="StandKM" HeaderText="Jarak Tempuh KM">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDatKm runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.StandKM"),"#,##0") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tgl. PM Service">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VisitType" HeaderText="Tipe Visit">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblVisitTypeGrid" runat="server" Width="173px" Text='<%# DataBinder.Eval(Container, "DataItem.VisitType") %>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Keterangan">
										<HeaderStyle CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblMSPDescription" runat="server" Width="173px"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap=False></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lbtnPart" runat="server" style="display:none">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Add or Remove Part">
											</asp:Label>
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
						<asp:button id="btnRelease" runat="server" Width="60px" CausesValidation="False" Text="Rilis"></asp:button></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left">&nbsp;&nbsp;</TD>
				</TR>
			</TABLE>

            <asp:HiddenField Value=""  ID="hdnMsg" runat="server"/>
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

          <script type="text/javascript">
              var hdnMsg = document.getElementById("hdnMsg");

              if (hdnMsg != null && hdnMsg.value != "") {
                  var msg = hdnMsg.value;
                  hdnMsg.value = "";
                  alert(msg);

              }

    </script>
	</body>
</HTML>
