<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSAPGradeWeight.aspx.vb" Inherits="FrmSAPGradeWeight" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSAPGradeWeight</title>
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
					<td class="titlePage">KONFIGURASI - Bobot Penilaian</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td><strong>Bobot Penjualan:</strong></td>
				</tr>
				<TR>
					<td><asp:datagrid id="dtgSales" runat="server" ShowFooter="True" Width="100%" CellPadding="3" BackColor="#E0E0E0"
							BorderWidth="1px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" PageSize="25">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNoSales" runat="server" text= '<%# container.itemindex+1 %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Font-Size="Small"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode">
									<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblKodeSales runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterKodeSales" runat="server" BackColor="White" Width="80px" onkeypress="return alphaNumericExcept(event,'<>?*%$;.,')" onblur="omitSomeCharacter('txtFooterKodeSales','<>?*%$;,.')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id=lblEditKodeSales runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Description" HeaderText="Penjualan">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblDeskripsiSales runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterDeskripsiSales" runat="server" Width="176px" BackColor="White" onkeypress="return alphaNumericExcept(event,'<>?*%$;,.')" onblur="omitSomeCharacter('txtFooterDeskripsiSales','<>?*%$;,.')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditDeskripsiSales" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>' onkeypress="return alphaNumericExcept(event,'<>?*%$;,.')" onblur="omitSomeCharacter('txtEditDeskripsiSales','<>?*%$;,.')">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Bobot" HeaderText="Bobot">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblBobotSales" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Bobot")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterBobotSales" runat="server" Width="176px" BackColor="White" onkeypress="return NumericOnlyWith(event,'')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditBobotSales" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Bobot")%>' onkeypress="return NumericOnlyWith(event,'')">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEditSales" runat="server" CommandName="Edit">
											<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
										<asp:LinkButton id="lbtnDeleteSales" runat="server" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?')"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAddSales" runat="server" CommandName="Add">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="lbtnSaveSales" runat="server" CommandName="Save">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										<asp:LinkButton id="lbtnCancelSales" runat="server" CommandName="Cancel">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>				
				<TR>
					<TD><strong>Bobot Prospecting:</strong></TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgProspecting" runat="server" ShowFooter="True" Width="100%" CellPadding="3"
							BackColor="#E0E0E0" BorderWidth="1px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
							PageSize="25">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNoProspect" runat="server" text= '<%# container.itemindex+1 %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Font-Size="Small"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode">
									<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblKodeProspect runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterKodeProspect" runat="server" BackColor="White" Width="72px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtFooterKodeProspect','<>?*%$;')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id=lblEditKodeProspect runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Description" HeaderText="Prospect">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblDeskripsiProspect runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterDeskripsiProspect" runat="server" Width="176px" BackColor="White" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtFooterDeskripsiProspect','<>?*%$;')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditDeskripsiProspect" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>' onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtEditDeskripsiProspect','<>?*%$;')">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Bobot" HeaderText="Bobot">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblBobotProspect" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Bobot")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterBobotProspect" runat="server" Width="176px" BackColor="White" onkeypress="return NumericOnlyWith(event,'')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditBobotProspect" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Bobot")%>' onkeypress="return NumericOnlyWith(event,'')">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEditProspect" runat="server" CommandName="Edit">
											<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
										<asp:LinkButton id="lbtnDeleteProspect" runat="server" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?')"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAddProspect" runat="server" CommandName="Add">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="lbtnSaveProspect" runat="server" CommandName="Save">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										<asp:LinkButton id="lbtnCancelProspect" runat="server" CommandName="Cancel">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>	
				<TR>
					<TD><strong>Bobot PKT:</strong></TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgPKT" runat="server" ShowFooter="True" Width="100%" CellPadding="3" BackColor="#E0E0E0"
							BorderWidth="1px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" PageSize="25">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNoPKT" runat="server" text= '<%# container.itemindex+1 %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Font-Size="Small"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode">
									<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblKodePKT runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterKodePKT" runat="server" BackColor="White" Width="72px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtFooterKodePKT','<>?*%$;')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id=lblEditKodePKT runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Description" HeaderText="PKT">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblDeskripsiPKT runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterDeskripsiPKT" runat="server" Width="176px" BackColor="White" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtFooterDeskripsiPKT','<>?*%$;')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditDeskripsiPKT" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>' onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtEditDeskripsiPKT','<>?*%$;')">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Bobot" HeaderText="Bobot">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblBobotPKT" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Bobot")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterBobotPKT" runat="server" Width="176px" BackColor="White" onkeypress="return NumericOnlyWith(event,'')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditBobotPKT" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Bobot")%>' onkeypress="return NumericOnlyWith(event,'')">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEditPKT" runat="server" CommandName="Edit">
											<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
										<asp:LinkButton id="lbtnDeletePKT" runat="server" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?')"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAddPKT" runat="server" CommandName="Add">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="lbtnSavePKT" runat="server" CommandName="Save">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										<asp:LinkButton id="lbtnCancelPKT" runat="server" CommandName="Cancel">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>	
				<TR>
					<TD><strong>Bobot Supervisor:</strong></TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dtgSupervisor" runat="server" ShowFooter="True" Width="100%" CellPadding="3"
							BackColor="#E0E0E0" BorderWidth="1px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
							PageSize="25">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNoSPV" runat="server" text= '<%# container.itemindex+1 %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Font-Size="Small"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Kode">
									<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblKodeSPV runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterKodeSPV" runat="server" BackColor="White" Width="80px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtFooterKodeSPV','<>?*%$;')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id=lblEditKodeSPV runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Code")%>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Description" HeaderText="Supervisor">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblDeskripsiSPV runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterDeskripsiSPV" runat="server" Width="176px" BackColor="White" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtFooterDeskripsiSPV','<>?*%$;')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditDeskripsiSPV" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>' onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtEditDeskripsiSPV','<>?*%$;')">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Bobot" HeaderText="Bobot">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblBobotSPV" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Bobot")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:TextBox id="txtFooterBobotSPV" runat="server" Width="176px" BackColor="White" onkeypress="return NumericOnlyWith(event,'')"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtEditBobotSPV" runat="server" Width="176px" BackColor="White" text='<%#DataBinder.Eval(Container,"DataItem.Bobot")%>' onkeypress="return NumericOnlyWith(event,'')">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEditSPV" runat="server" CommandName="Edit">
											<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
										<asp:LinkButton id="lbtnDeleteSPV" runat="server" CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?')"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAddSPV" runat="server" CommandName="Add">
											<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="lbtnSaveSPV" runat="server" CommandName="Save">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										<asp:LinkButton id="lbtnCancelSPV" runat="server" CommandName="Cancel">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
