<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmParameterAudit.aspx.vb" Inherits="FrmParameterAudit" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmParameterAudit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">SHOWROOM AUDIT - Input Petunjuk 
						Pelaksanaan
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>
			<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<asp:panel id="pnlHeader" runat="server">
					<TBODY>
						<TR>
							<TD class="titleField" width="24%">Kode Audit</TD>
							<TD width="1%">:</TD>
							<TD width="75%">
								<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAuditCode" onblur="omitSomeCharacter('txtAuditCode','<>?*%$;')"
									runat="server" Width="168px" MaxLength="10"></asp:textbox></TD>
						</TR>
						<TR>
							<TD class="titleField" width="24%">Periode</TD>
							<TD width="1%">:</TD>
							<TD width="75%">
								<asp:dropdownlist id="ddlYearPeriod" runat="server"></asp:dropdownlist></TD>
						</TR>

				</asp:panel>
				<TR>
					<TD colSpan="3"><asp:datagrid id="dtgPhotoList" runat="server" Width="100%" BackColor="#E0E0E0" ShowFooter="True"
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
								<asp:TemplateColumn HeaderText="Keterangan">
									<HeaderStyle Width="12%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblDesc runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterDesc" runat="server" Width="400px" BackColor="White" MaxLength="50"
											onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter(this.id,'<>?*%$;')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Textbox id=txtEditDesc runat="server" MaxLength="50" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>' onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter(this.id,'<>?*%$;')">
										</asp:Textbox>
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
										<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?')"></asp:LinkButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="ID">
									<ItemTemplate>
										<asp:Label ID="ID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
						<TR>
							<TD class="titleField" width="24%">Petunjuk/Pelaksana</TD>
							<TD width="1%">:</TD>
							<TD width="75%"><INPUT id="iDirection" style="WIDTH: 336px; HEIGHT: 20px" type="file" size="36" runat="server" NAME="iDirection">
							</TD>
						</TR>
						<TR>
							<TD class="titleField" width="24%">Item Penilaian</TD>
							<TD width="1%">:</TD>
							<TD width="75%"><INPUT id="iItemScore" style="WIDTH: 336px; HEIGHT: 20px" type="file" size="36" runat="server" NAME="iItemScore">
							</TD>
						</TR>
					<TR>
					<td></td>
					<td></td>
					<TD><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button><asp:button id="btnRilis" runat="server" Width="56px" Text="Rilis"></asp:button><asp:button id="btnBack" runat="server" Text="Kembali"></asp:button></TD>
				</TR>
				</TBODY></table>
		</form>
	</body>
</HTML>
