<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmServiceMechanic.aspx.vb" Inherits="FrmServiceMechanic" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmServiceMechanic</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">

            function ShowPPModelSelection() {
                showPopUp('../General/../PopUp/PopUpVechileTypeModel.aspx', '', 520, 750, ModelSelection);
            }

            function ModelSelection(selectedDealer) {
                var txtModelSelection = document.getElementById("txtModel");
                txtModelSelection.value = selectedDealer;
            }

            function ShowPPDealerSelection() {
                showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            }

            function DealerSelection(selectedDealer) {
                var txtDealerSelection = document.getElementById("txtKodeDealer");
                txtDealerSelection.value = selectedDealer;
            }

		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Master - Daftar Pegawai After Sales</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="2" cellPadding="2">
							 <TR runat="server" id="trDealer">
								<TD class="titleField">Kode Dealer</TD>
								<TD>:</TD>
								<TD>
									<asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" style="Z-INDEX: 0" id="txtKodeDealer" Width="150px"
										onkeypress="return alphaNumericExcept(event,'<>?*%$')" runat="server"></asp:textbox>
									<asp:label style="Z-INDEX: 0" id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
                            <TR>
								<TD class="titleField" width="24%">Kode Service</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtKodeMekanik)" id="txtKodeMekanik"
										runat="server" MaxLength="50" Width="150px"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="rfvKode" runat="server" ErrorMessage="*" ControlToValidate="txtKodeMekanik" InitialValue=" "
										Width="16px" EnableClientScript="False"></asp:requiredfieldvalidator></td>
							</TR>
                            <TR>
								<TD class="titleField" width="24%">Posisi</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtPosisi)" id="txtPosisi"
										runat="server" MaxLength="50" Width="150px"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="txtPosisi" InitialValue=" "
										Width="16px" EnableClientScript="False"></asp:requiredfieldvalidator></td>
							</TR>
                            <TR>
								<TD class="titleField" width="24%">Nama</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtNama)" id="txtNama"
										runat="server" MaxLength="50" Width="150px"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="*" ControlToValidate="txtNama" InitialValue=" "
										Width="16px" EnableClientScript="False"></asp:requiredfieldvalidator></td>
							</TR>
							
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>
									<asp:Button id="btnCari" runat="server" Text=" Cari " width="60px" CausesValidation="False"></asp:Button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgServiceMechanic" runat="server" Width="100%" PageSize="25" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Email User" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Kode Dealer">
                                        <ItemStyle horizontalalign="Center"/>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
                                        <ItemTemplate>
											<asp:Label id=Label4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerName")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ID" HeaderText="Kode Service">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' ID="Label2" NAME="Label2">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ID" HeaderText="Posisi">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JobPosition")%>' ID="Label8" NAME="Label8">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                   <%-- <asp:TemplateColumn SortExpression="SalesmanCode" HeaderText="Kode Salesman Parts">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanCode")%>' ID="Label7" NAME="Label7">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>--%>
									<asp:TemplateColumn SortExpression="Name" HeaderText="Nama">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name")%>' ID="Label3" NAME="Label3">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    	<asp:TemplateColumn SortExpression="CityName" HeaderText="Kota">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CityName")%>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status")%>' ID="Label6" NAME="Label6">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
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
