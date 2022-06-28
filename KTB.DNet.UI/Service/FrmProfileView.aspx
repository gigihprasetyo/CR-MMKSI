<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmProfileView.aspx.vb" Inherits="FrmProfileView" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEditProfile</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<!--<script language="javascript" src="../WebResources/InputValidation.js"></script> -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="titlePage" style="HEIGHT: 19px">SERVICE - Profile Details</td>
					</tr>
					<tr>
						<td style="HEIGHT: 2px" background="../images/bg_hor.gif" height="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
					</tr>
					<tr>
						<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
					</tr>
					<tr>
						<td style="HEIGHT: 93px">
							<table cellSpacing="1" cellPadding="2" width="100%" border="0">
								<TR>
									<td class="titleField" width="20%">Kode</td>
									<TD width="1%">:</TD>
									<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtCode" onblur="omitSomeCharacter('txtCode','<>?*%$;')"
											Width="256px" Runat="server" ReadOnly="true"></asp:textbox></TD>
								</TR>
								<TR>
									<td class="titleField" width="20%">Deskripsi</td>
									<TD width="1%">:</TD>
									<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtDescription" onblur="omitSomeCharacter('txtDescription','<>?*%$;')"
											Width="256px" Runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<td class="titleField" width="20%">Tipe Data</td>
									<TD width="1%">:</TD>
									<TD width="79%"><asp:dropdownlist id="ddlDataType" Width="256px" Runat="server" Enabled="False" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 18px" width="20%">Tipe Kontrol</td>
									<TD style="HEIGHT: 18px" width="1%">:</TD>
									<TD style="HEIGHT: 18px" width="79%"><asp:dropdownlist id="ddlControlType" Width="256px" Runat="server" Enabled="False" AutoPostBack="True">
											<asp:ListItem Value="Piih">Piih</asp:ListItem>
										</asp:dropdownlist></TD>
								</TR>
								<TR>
									<td class="titleField" width="20%">Mandatory</td>
									<TD width="1%">:</TD>
									<TD width="79%"><asp:dropdownlist id="ddlMandatory" Width="256px" Runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<td class="titleField" width="20%">Status</td>
									<TD width="1%">:</TD>
									<TD width="79%"><asp:dropdownlist id="ddlStatus" Width="256px" Runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<td class="titleField" style="HEIGHT: 26px" width="20%"><asp:label id="lblLebar" Runat="server" Visible="False">Lebar Data</asp:label></td>
									<TD style="HEIGHT: 26px" width="1%"><asp:label id="lblttk2" Runat="server" Visible="False">:</asp:label></TD>
									<TD style="HEIGHT: 26px" width="79%"><asp:textbox onkeypress="return NumericOnlyWith(event,'')" id="txtDataLength" Width="256px" Runat="server"
											Visible="False" MaxLength="9"></asp:textbox></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD><asp:datagrid id="dtgProfile" runat="server" Width="100%" BackColor="#E0E0E0" ShowFooter="True"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="1px" CellPadding="3" BorderStyle="None">
								<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle VerticalAlign="Top"></HeaderStyle>
								<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Font-Size="Small"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode">
										<HeaderStyle Width="12%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblKode runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtFooterKode" runat="server" Width="168px" BackColor="White"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:Label id=lblEditKode runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Deskripsi">
										<HeaderStyle Width="10%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDeskripsi runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtFooterDeskripsi" runat="server" Width="176px" BackColor="White"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtEditDeskripsi" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
										CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
										EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
										<HeaderStyle Width="4%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:EditCommandColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="4%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>' style="display:none">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="4%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<ItemTemplate>
											<asp:Label ID="lblID" Runat="server" text='<%#DataBinder.Eval(Container,"DataItem.ID")%>'>
											</asp:Label>
											<asp:Label ID="lblHeaderID" Runat="server" text='<%#DataBinder.Eval(Container, "DataItem.ProfileHeader.ID")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<tr>
						<td><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnKembali" runat="server" Text="Kembali"></asp:button></td>
					</tr>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
