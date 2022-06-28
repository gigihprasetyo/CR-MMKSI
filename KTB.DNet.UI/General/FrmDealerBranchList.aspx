<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDealerBranchList.aspx.vb" Inherits="FrmDealerBranchList" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Cabang Dealer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
			function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtKodeDealer");
				txtDealer.value = selectedCode;
				txtDealer.focus();
			}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">DEALER - Cabang Dealer</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="28%">Kode Dealer</TD>
								<td style="WIDTH: 85px" width="85">:
								</td>
								<TD width="71%">
									<asp:textbox id="txtKodeDealer" Width="150px" Runat="server"></asp:textbox>
									<asp:label id="lblPopUpDealer" runat="server" width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
									</asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Cabang</TD>
								<td style="WIDTH: 85px" width="85">:
								<TD><asp:textbox id="txtName" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtName)"
										runat="server" Width="264px" MaxLength="50"></asp:textbox>&nbsp;<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Silahkan isi nama dealer  (tidak boleh kosong)"
										ControlToValidate="txtName" Display="None"></asp:requiredfieldvalidator>
									<asp:requiredfieldvalidator id="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField">Alamat</TD>
								<td style="WIDTH: 85px" width="85">:
								<TD><asp:textbox id="txtAddress" onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtAddress)"
										runat="server" Width="447px" MaxLength="100"></asp:textbox>
								</TD>
							</TR>
							<!--<TR>
								<TD class="titleField" style="HEIGHT: 22px">Provinsi</TD>
								<td style="WIDTH: 85px" width="85">:
								<TD style="HEIGHT: 22px"><asp:dropdownlist id="ddlProvince" runat="server" Width="150px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 18px">Kota</TD>
								<td style="WIDTH: 85px" width="85">:
								<TD style="HEIGHT: 18px"><asp:dropdownlist id="ddlCity" runat="server" Width="150px" Enabled="False"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Kode 
									Pos&nbsp;
									<asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtPostCode)" id="txtPostCode"
										runat="server" Width="50px" MaxLength="5"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator12" runat="server" ErrorMessage="Silahkan isi Kode Pos dengan 5 digit angka"
										ControlToValidate="txtPostCode" Display="None" ValidationExpression="\d{5}"></asp:regularexpressionvalidator>&nbsp;<asp:regularexpressionvalidator id="RegularExpressionValidator14" runat="server" ErrorMessage="*" ControlToValidate="txtPostCode"
										ValidationExpression="\d{5}"></asp:regularexpressionvalidator></TD>
							</TR>-->
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>
									<P>
										<asp:button id="btnCari" runat="server" Text="Cari" width="60px" CausesValidation="False"></asp:button>&nbsp;
										<asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px" Visible="false"></asp:button>&nbsp;
										<asp:button id="btnBatal" runat="server" Text="Batal" width="60px" CausesValidation="False"></asp:button>
									</P>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 380px"><asp:datagrid id="dtgDealerBranch" runat="server" Font-Names="MS Reference Sans Serif" CellSpacing="1"
								ForeColor="GhostWhite" Width="100%" PageSize="50" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" BorderColor="#CDCDCD" BorderStyle="None"
								BorderWidth="0px" BackColor="Gainsboro" CellPadding="3" GridLines="None" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    	<asp:BoundColumn DataField="DealerBranchCode" SortExpression="DealerBranchCode" HeaderText="Kode Cabang">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>

									<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Cabang">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Address" SortExpression="Address" HeaderText="Alamat">
										<HeaderStyle Width="40%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" Visible="false" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete"  Visible="false">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
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
